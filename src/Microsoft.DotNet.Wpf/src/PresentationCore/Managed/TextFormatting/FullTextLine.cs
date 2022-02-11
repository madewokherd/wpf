// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

//
//
//
//  Contents:  Complex implementation of TextLine
//
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;
using MS.Internal;
using MS.Internal.Text.TextInterface;
using MS.Internal.Shaping;
using Common.TextFormatting;
using ITextMetrics = MS.Internal.TextFormatting.ITextMetrics;

using SR = MS.Internal.PresentationCore.SR;
using SRID = MS.Internal.PresentationCore.SRID;


namespace Managed.TextFormatting
{
    /// <remarks>
    /// Make FullTextLine nested type of TextMetrics to allow full access to TextMetrics private members
    /// </remarks>
    internal partial struct TextMetrics : ITextMetrics
    {
        /// <summary>
        /// Complex implementation of TextLine
        ///
        /// TextLine implementation around
        ///     o   Line Services
        ///     o   OpenType Services Library
        ///     o   Implementation of Unicode Bidirectional algorithm
        ///     o   Complex script itemizer
        ///     o   Composite font with generic glyph hunting algorithm
        /// </summary>
        internal class FullTextLine : TextLine
        {
            private TextMetrics                         _metrics;                       // Text metrics
            private int                                 _paragraphWidth;                // paragraph width
            private StatusFlags                         _statusFlags;                   // status flags of the line
            private FullTextState                       _fullText;                      // full text state kept for collapsing purpose (only have it when StatusFlags.HasOverflowed is set)
            private TextDecorationCollection            _paragraphTextDecorations;      // Paragraph-level text decorations (or null if none)
            private Brush                               _defaultTextDecorationsBrush;   // Default brush for paragraph text decorations
            private TextFormattingMode                  _textFormattingMode;            // The TextFormattingMode of the line (Ideal or Display).
			private List<TextSpan<TextRun>>				_textRunSpans;					// List of TextRun objects in this line and their lengths

            [Flags]
            private enum StatusFlags
            {
                None                = 0,
                IsDisposed          = 0x00000001,
                HasOverflowed       = 0x00000002,
                BoundingBoxComputed = 0x00000004,
                RightToLeft         = 0x00000008,
                HasCollapsed        = 0x00000010,
                KeepState           = 0x00000020,
                IsTruncated         = 0x00000040,
                IsJustified         = 0x00000080, // Indicates whether the text alignment is set to justified
                                                  // This flag is needed later to decide how the metrics
                                                  // will be rounded in display mode when converted
                                                  // from ideal to real values.
            }

            internal FullTextLine(
                FormatSettings          settings,
                int                     cpFirst,
                int                     lineLength,
                int                     paragraphWidth,
                LineFlags               lineFlags
                )
                : this(settings.TextFormattingMode, settings.Pap.Justify, settings.TextSource.PixelsPerDip)
			{
                if (    (lineFlags & LineFlags.KeepState) != 0
                    ||  settings.Pap.AlwaysCollapsible)
                {
                    _statusFlags |= StatusFlags.KeepState;
                }

                int finiteFormatWidth = settings.GetFiniteFormatWidth(paragraphWidth);

                FullTextState fullText = FullTextState.Create(settings, cpFirst, finiteFormatWidth);

                // formatting the line
                FormatLine(
                    fullText,
                    cpFirst,
                    lineLength,
                    fullText.FormatWidth,
                    finiteFormatWidth,
                    paragraphWidth,
                    lineFlags,
                    null    // collapsingSymbol
                    );
			}

            /// <summary>
            /// Empty private constructor
            /// </summary>
            private FullTextLine(TextFormattingMode textFormattingMode, bool justify, double pixelsPerDip) : base(pixelsPerDip)
            {
                _textFormattingMode = textFormattingMode;
                if (justify)
                {
                    _statusFlags |= StatusFlags.IsJustified;
                }
                _metrics = new TextMetrics();
                _metrics._pixelsPerDip = pixelsPerDip;
            }

			~FullTextLine()
			{
				DisposeInternal(true);
			}

			public override void Dispose()
			{
				DisposeInternal(false);
				GC.SuppressFinalize(this);
			}

			private void DisposeInternal(bool finalizing)
			{
			}

			// For a given line length, return TextMetrics for as many characters fit in the width
			private TextMetrics GetLineMetrics(
                FullTextState           fullText,
                int                     cpFirst,
                int                     lineLength,
                int                     formatWidth,
                int                     finiteFormatWidth,
                int                     paragraphWidth,
                LineFlags               lineFlags,
                FormattedTextSymbols    collapsingSymbol
                )
			{
                TextMetrics result = new TextMetrics();
				result._formatter = fullText.Formatter;
                Debug.Assert(result._formatter != null);

                TextStore store = fullText.TextStore;
                FormatSettings settings = store.Settings;
                ParaProp pap = settings.Pap;
				var pixelsPerDip = settings.TextSource.PixelsPerDip;

				int pos = cpFirst;
				int content_ascent = 0;
				int content_descent = 0;

				result._textWidth = settings.TextIndent;

				while (lineLength <= 0 || cpFirst + lineLength > pos)
				{
					TextRun textRun;
					int runLength;
					CharacterBufferRange chars = settings.FetchTextRun(pos, cpFirst, out textRun, out runLength);

					if (lineLength > 0 && pos + chars.Length > cpFirst + lineLength)
					{
						runLength = cpFirst + lineLength - pos;
						chars = new CharacterBufferRange(chars, 0, runLength);
					}

					if (textRun is TextEndOfParagraph || textRun is TextEndOfLine)
					{
						pos += runLength;
						result._cchNewline = runLength;
						break;
					}

					TextMetrics runMetrics = GetRunMetrics(fullText, textRun, chars, paragraphWidth - result._textWidth);

					if (content_ascent < runMetrics._textAscent)
						content_ascent = runMetrics._textAscent;

					if (content_descent < runMetrics._textHeight - runMetrics._textAscent)
						content_descent = runMetrics._textHeight - runMetrics._textAscent;

					result._textWidth += runMetrics._textWidth;

					pos += runLength;
				}

				result._pixelsPerDip = pixelsPerDip;
				result._cchLength = pos - cpFirst;
				result._textWidthAtTrailing = result._textWidth; // will be set later by FormatLine

				if (pap.LineHeight > 0)
				{
                    // Host specifies line height, honor it.
                    result._height = pap.LineHeight;
                    result._baselineOffset = (int)Math.Round(
                        result._height
                        * pap.DefaultTypeface.Baseline(pap.EmSize, Constants.DefaultIdealToReal, pixelsPerDip, _textFormattingMode)
                        / pap.DefaultTypeface.LineSpacing(pap.EmSize, Constants.DefaultIdealToReal, pixelsPerDip, _textFormattingMode)
                        );
				}

				if (content_ascent > 0)
				{
					result._textAscent = content_ascent;
					result._textHeight = content_ascent + content_descent;

					// TODO: VerticalAdjust
				}
				else
				{
                    // Line is empty so text height and text baseline are based on the default typeface;
                    // it doesn't make sense even for an emtpy line to have zero text height
                    result._textAscent = (int)Math.Round(pap.DefaultTypeface.Baseline(pap.EmSize, Constants.DefaultIdealToReal, pixelsPerDip, _textFormattingMode));
                    result._textHeight = (int)Math.Round(pap.DefaultTypeface.LineSpacing(pap.EmSize, Constants.DefaultIdealToReal, pixelsPerDip, _textFormattingMode));
				}

				if (result._height <= 0)
				{
                    result._height = result._textHeight;
                    result._baselineOffset = result._textAscent;
				}

				return result;
			}

            /// <summary>
            /// format text line using LS
            /// </summary>
            /// <param name="fullText">state of the full text backing store</param>
            /// <param name="cpFirst">first cp to format</param>
            /// <param name="lineLength">character length of the line</param>
            /// <param name="formatWidth">width used to format</param>
            /// <param name="finiteFormatWidth">width used to detect overflowing of format result</param>
            /// <param name="paragraphWidth">paragraph width</param>
            /// <param name="lineFlags">line formatting control flags</param>
            /// <param name="collapsingSymbol">line end collapsing symbol</param>
            private void FormatLine(
                FullTextState           fullText,
                int                     cpFirst,
                int                     lineLength,
                int                     formatWidth,
                int                     finiteFormatWidth,
                int                     paragraphWidth,
                LineFlags               lineFlags,
                FormattedTextSymbols    collapsingSymbol
                )
            {
				_fullText = fullText;
				_paragraphWidth = paragraphWidth;

                TextStore store = fullText.TextStore;
                FormatSettings settings = store.Settings;
                ParaProp pap = settings.Pap;

                _paragraphTextDecorations = pap.TextDecorations;
                if (_paragraphTextDecorations != null)
                {
                    if (_paragraphTextDecorations.Count != 0)
                    {
                        _defaultTextDecorationsBrush = pap.DefaultTextDecorationsBrush;
                    }
                    else
                    {
                        _paragraphTextDecorations = null;
                    }
                }

				_metrics = GetLineMetrics(fullText, cpFirst, lineLength, formatWidth, finiteFormatWidth, paragraphWidth, lineFlags, collapsingSymbol);

				lineLength = _metrics._cchLength;
				if (lineLength > _metrics._cchNewline)
				{
					// check for line wrap
					if (pap.Wrap && _metrics._textStart + _metrics._textWidthAtTrailing > finiteFormatWidth)
					{
						var lineBreakpoints = store.FindLineBreakpoints(cpFirst, lineLength-_metrics._cchNewline);

						// binary search for the longest fitting line - this assumes line lengths at breakpoints are increasing

						double widthAtMax = _metrics._textWidthAtTrailing;
						int min = 1;
						int max = lineLength - _metrics._cchNewline-1;
						TextMetrics? trimmedMetricsAtMin = null;

						while (min <= max)
						{
							// Try an estimate based on known widths
							int mid = (int)((finiteFormatWidth - _metrics._textStart) * (max / widthAtMax));
							if (mid <= min)
							{
								if (min < max)
									mid = min+1;
								else
									mid = min;
							}
							else if (mid > max)
								mid = (min + max + 1) / 2;

							// Find the next index where we can linebreak
							int i;
							for (i = mid; i <= max; i++)
							{
								if (lineBreakpoints.GetBreakConditionBefore(i+cpFirst) == DWriteBreakCondition.CanBreak)
									break;
							}
							if (i > max)
							{
								// No breakpoints between here and max
								max = mid-1;
								continue;
							}
							var trailingWhitespace = lineBreakpoints.WhitespaceLengthBefore(i+cpFirst);
							TextMetrics trimmedMetrics;
							if (i == min && trimmedMetricsAtMin is TextMetrics val)
								trimmedMetrics = val;
							else
								trimmedMetrics = GetLineMetrics(fullText, cpFirst, i - trailingWhitespace, formatWidth, finiteFormatWidth, paragraphWidth, lineFlags, collapsingSymbol);
							if (trimmedMetrics._textStart + trimmedMetrics._textWidthAtTrailing <= finiteFormatWidth)
							{
								// Can break here
								min = i;
								if (min == max)
								{
									// Found the best possible breakpoint.
									_metrics = GetLineMetrics(fullText, cpFirst, i, formatWidth, finiteFormatWidth, paragraphWidth, lineFlags, collapsingSymbol);
									_metrics._cchTrailing = trailingWhitespace;
									_metrics._textWidthAtTrailing = trimmedMetrics._textWidth;
									break;
								}
								trimmedMetricsAtMin = trimmedMetrics;
								continue;
							}
							// Segment is too long to break here
							widthAtMax = trimmedMetrics._textWidthAtTrailing;
							max = mid - 1;
						}
					}
				}

                if (collapsingSymbol == null)
                {
                    // overflow detection for potential collapsible line
                    if (_metrics._textStart + _metrics._textWidthAtTrailing > finiteFormatWidth)
                    {
                        bool hasOverflowed = true;
                        if (_textFormattingMode == TextFormattingMode.Display)
                        {
                            // apply display-mode rounding before checking for overflow
                            double realWidth = Width;
                            double realFormatWidth = _metrics._formatter.IdealToReal(finiteFormatWidth, PixelsPerDip);
                            hasOverflowed = (TextFormatterImp.CompareReal(realWidth, realFormatWidth, PixelsPerDip, _textFormattingMode) > 0);
                        }

                        if (hasOverflowed)
                        {
                            // line has overflowed
                            _statusFlags |= StatusFlags.HasOverflowed;
                        }
                    }
                }
			}

			private int GetShapeableSymbolsWidth(TextShapeableSymbols textRun)
			{
				int result = 0;
				int text_length = textRun.Length;
				int[] advance_widths = new int[text_length];
				GCHandle pin_handle;
				CharacterBuffer char_buf = textRun.CharacterBufferReference.CharacterBuffer;
				unsafe
				{
					IntPtr run_characters = char_buf.PinAndGetCharacterPointer(textRun.CharacterBufferReference.OffsetToFirstChar, out pin_handle);
					try
					{
						fixed (int* widths_ptr = advance_widths)
						{
							textRun.GetAdvanceWidthsUnshaped((char*)run_characters.ToPointer(), text_length, TextFormatterImp.ToIdeal, widths_ptr);
						}
					}
					finally
					{
						char_buf.UnpinCharacterPointer(pin_handle);
					}
				}

				for (int i=0; i<text_length; i++)
				{
					result += advance_widths[i];
				}

				return result;
			}

			private FormattedTextSymbols GetFormattedTextSymbols(TextCharacters textRun, CharacterBufferRange chars, int bidiLevel)
			{
				return new FormattedTextSymbols(
					_fullText.Formatter.GlyphingCache,
					textRun,
					chars,
					(bidiLevel & 1) == 1, //rightToLeft
					TextFormatterImp.ToIdeal, // scalingFactor
					(float)_fullText.TextStore.Settings.TextSource.PixelsPerDip,
					_textFormattingMode,
					false); // isSideways
			}

			private TextMetrics GetRunMetrics(FullTextState textState, TextRun textRun, CharacterBufferRange chars, int remainingParagraphWidth)
			{
				if (textRun is TextCharacters)
				{
					var textChars = (TextCharacters)textRun;
					var result = new TextMetrics();
					// FIXME: Call TextModifier.ModifyProperties when applicable
					var props = textChars.Properties;
					var typeface = props.Typeface;
					var ideal_emsize = TextFormatterImp.RealToIdeal(props.FontRenderingEmSize);

                    result._textAscent = (int)Math.Round(typeface.Baseline(ideal_emsize, Constants.DefaultIdealToReal, props.PixelsPerDip, _textFormattingMode));
                    result._textHeight = (int)Math.Round(typeface.LineSpacing(ideal_emsize, Constants.DefaultIdealToReal, props.PixelsPerDip, _textFormattingMode));
					result._height = result._textHeight;
					result._baselineOffset = result._textAscent;

					// width calculation
					var formatted = GetFormattedTextSymbols(textChars, chars, 0); // isSideways
					result._textWidth = formatted.UnscaledWidth;

					return result;
				}
				else if (textRun is TextEmbeddedObject)
				{
					var result = new TextMetrics();
					var metric = ((TextEmbeddedObject)textRun).Format((double)remainingParagraphWidth / TextFormatterImp.ToIdeal);

					result._textHeight = (int)Math.Round(metric.Height * TextFormatterImp.ToIdeal);
					result._textWidth = (int)Math.Round(metric.Width * TextFormatterImp.ToIdeal);
					result._baselineOffset = (int)Math.Round(metric.Baseline * TextFormatterImp.ToIdeal);
					result._height = result._textHeight;
					result._textAscent = result._baselineOffset;
					return result;
				}
				else if (textRun is TextHidden)
				{
					var result = new TextMetrics();
					// Default should be empty
					return result;
				}
				else if (textRun is TextModifier || textRun is TextEndOfSegment)
				{
					// This just modifies the following runs until TextEndOfSegment, it doesn't display anything on its own.
					var result = new TextMetrics();
					return result;
				}
				else
				{
					throw new NotImplementedException(String.Format("Managed.TextFormatting.FullTextLine.GetRunMetrics for {0}", textRun.GetType().FullName));
				}
			}

			private Point GetOffsetToFirstRun()
			{
				return new Point(_metrics._formatter.IdealToReal(_fullText.TextStore.Settings.TextIndent, PixelsPerDip), 0);
			}

			private Point AdjustOffset(Point orig)
			{
				var ofs = GetOffsetToFirstRun();
				return new Point(orig.X + ofs.X, orig.Y + ofs.Y);
			}

			public override void Draw(DrawingContext drawingContext, Point origin, InvertAxes inversion)
			{
                if (drawingContext == null)
                {
                    throw new ArgumentNullException("drawingContext");
                }

                if ((_statusFlags & StatusFlags.IsDisposed) != 0)
                {
                    throw new ObjectDisposedException(SR.Get(SRID.TextLineHasBeenDisposed));
                }

                MatrixTransform antiInversion = TextFormatterImp.CreateAntiInversionTransform(
                    inversion,
                    _metrics._formatter.IdealToReal(_paragraphWidth, PixelsPerDip),
                    _metrics._formatter.IdealToReal(_metrics._height, PixelsPerDip)
                    );

				origin = AdjustOffset(origin);

                if (antiInversion == null)
                {
                    DrawTextLine(drawingContext, origin, null);
                }
                else
                {
                    // Apply anti-inversion transform to correct the visual
                    drawingContext.PushTransform(antiInversion);
                    try
                    {
                        DrawTextLine(drawingContext, origin, antiInversion);
                    }
                    finally
                    {
                        drawingContext.Pop();
                    }
                }
			}

			internal struct OrderedTextRun
			{
				internal int BidiLevel;
				internal int CpFirst;
				internal int Length;
				internal TextRun TextRun;
				internal CharacterBufferRange Range;
			}

			private List<OrderedTextRun> ReorderRuns()
			{
				var result = new List<OrderedTextRun> ();
				var store = _fullText.TextStore;
				var settings = store.Settings;
				int cpFirst = store.CpFirst;
				int pos = cpFirst;
				int remaining_length = Length;

				// FIXME: Fetch bidi levels, reset to 0 for trailing whitespace

				while (remaining_length > 0)
				{
					TextRun run;
					int runLength;
					CharacterBufferRange chars = settings.FetchTextRun(pos, cpFirst, out run, out runLength);

					if (chars.Length > remaining_length)
					{
						runLength = remaining_length;
						chars = new CharacterBufferRange(chars, 0, runLength);
					}

					// FIXME: determine bidi level and shorten runLength to keep bidi level constant

					var ordered = new OrderedTextRun();
					ordered.BidiLevel = 0;
					ordered.TextRun = run;
					ordered.Range = chars;
					ordered.CpFirst = pos;
					ordered.Length = runLength;
					result.Add(ordered);

					remaining_length -= runLength;
					pos += runLength;
				}

				// FIXME: reverse bidi levels

				return result;
			}

            /// <summary>
            /// Draw complex text line
            /// </summary>
            /// <param name="drawingContext">drawing surface</param>
            /// <param name="origin">offset to the line origin</param>
            /// <param name="antiInversion">anti-inversion transform applied on the surface</param>
            private void DrawTextLine(
                DrawingContext      drawingContext,
                Point               origin,
                MatrixTransform     antiInversion
                )
            {
				origin = AdjustOffset(origin);
				origin.Y += Baseline;
				double paragraphWidth = (double)_paragraphWidth / TextFormatterImp.ToIdeal;

				foreach (var ordered in ReorderRuns())
				{
					if (ordered.TextRun is TextEndOfParagraph || ordered.TextRun is TextEndOfLine)
					{
						break;
					}
					else if (ordered.TextRun is TextCharacters)
					{
						var textChars = (TextCharacters)ordered.TextRun;
						//FIXME: Rendering loses precision compared to measurement in Ideal units
						var formatted = GetFormattedTextSymbols(textChars, ordered.Range, ordered.BidiLevel);

						formatted.Draw(drawingContext, origin);

						origin.X += formatted.Width;
					}
					else if (ordered.TextRun is TextEmbeddedObject)
					{
						var embedded = (TextEmbeddedObject)ordered.TextRun;

						embedded.Draw(drawingContext, origin, (ordered.BidiLevel & 1) == 1, false);
						origin.X += embedded.Format(paragraphWidth - origin.X).Width;
					}
					else if (ordered.TextRun is TextHidden)
					{
						// Nothing to do.
					}
					else if (ordered.TextRun is TextModifier || ordered.TextRun is TextEndOfSegment)
					{
						// Nothing to do.
					}
					else
					{
						throw new NotImplementedException(String.Format("Managed.TextFormatting.FullTextLine.Draw for {0}", ordered.TextRun.GetType().FullName));
					}
				}

				//FIXME: collapsingsymbol & overhang calculation
            }

			public override TextLine Collapse(params TextCollapsingProperties[] collapsingPropertiesList)
			{
				throw new NotImplementedException("Managed.TextFormatting.FullTextLine.Collapse");
			}

			public override IList<TextCollapsedRange> GetTextCollapsedRanges()
			{
				throw new NotImplementedException("Managed.TextFormatting.FullTextLine.GetTextCollapsedRanges");
			}

			public override CharacterHit GetCharacterHitFromDistance(double distance)
			{
				if (distance < 0)
					return new CharacterHit(_fullText.TextStore.CpFirst, 0);

				Point origin = new Point(0, 0);

				int index = 0;
				int length = 0;
				double x = GetOffsetToFirstRun().X;
				double paragraphWidth = (double)_paragraphWidth / TextFormatterImp.ToIdeal;

				foreach (var ordered in ReorderRuns())
				{
					if (ordered.TextRun is TextEndOfLine)
					{
						break;
					}
					else if (ordered.TextRun is TextCharacters)
					{
						var textChars = (TextCharacters)ordered.TextRun;
						var formatted = GetFormattedTextSymbols(textChars, ordered.Range, ordered.BidiLevel);
						var cpFirst = ordered.CpFirst;
						bool found = false;

						foreach (var glyphrun in formatted.GetGlyphRuns(ref origin))
						{
							bool isInside;

							var glyphCharacterHit = glyphrun.GetCaretCharacterHitFromDistance(distance - x, out isInside);

							index = glyphCharacterHit.FirstCharacterIndex + cpFirst;
							length = glyphCharacterHit.TrailingLength;

							if (isInside)
							{
								found = true;
								break;
							}

							x += glyphrun.ComputeAlignmentBox().Width;
							cpFirst += glyphrun.Characters.Count;
						}
						if (found)
							break;
					}
					else if (ordered.TextRun is TextEmbeddedObject)
					{
						var width = ((TextEmbeddedObject)ordered.TextRun).Format(paragraphWidth - x).Width;

						index = ordered.CpFirst;
						length = ordered.Length;  // trailing edge

						if (distance - x < width)
						{
							if (distance - x < width * 0.5)
								length = 0;
							break;
						}

						x += width;
					}
					else if (ordered.TextRun is TextHidden)
					{
						// Nothing to do.
					}
					else if (ordered.TextRun is TextModifier || ordered.TextRun is TextEndOfSegment)
					{
						// Nothing to do.
					}
					else
					{
						throw new NotImplementedException(String.Format("Managed.TextFormatting.FullTextLine.GetCharacterHitFromDistance for {0}", ordered.TextRun.GetType().FullName));
					}
				}
				
				return new CharacterHit(index, length);
			}

			public override double GetDistanceFromCharacterHit(CharacterHit characterHit)
			{
				throw new NotImplementedException("Managed.TextFormatting.FullTextLine.GetDistanceFromCharacterHit");
			}

			private bool GetRunContainingCp(int index, out OrderedTextRun run)
			{
				foreach (var ordered in ReorderRuns())
				{
					if (index >= ordered.CpFirst && index < ordered.CpFirst + ordered.Length && !(ordered.TextRun is TextEndOfLine))
					{
						run = ordered;
						return true;
					}
				}
				run = default(OrderedTextRun);
				return false;
			}

			public override CharacterHit GetNextCaretCharacterHit(CharacterHit characterHit)
			{
				int desiredIndex = characterHit.FirstCharacterIndex + characterHit.TrailingLength;
				OrderedTextRun run;
				if (!GetRunContainingCp(desiredIndex, out run))
				{
					// If it's out of bounds, get the last valid character hit.
					foreach (var ordered in ReorderRuns())
					{
						if (run.TextRun is null || (ordered.CpFirst > run.CpFirst && !(ordered.TextRun is TextEndOfLine)))
							run = ordered;
					}
				}
				if (run.TextRun is null || run.TextRun is TextEndOfLine)
				{
					// No valid character hit on this line.
					return new CharacterHit(0, 0);
				}
				else if (run.TextRun is TextCharacters)
				{
					var textChars = (TextCharacters)run.TextRun;
					var formatted = GetFormattedTextSymbols(textChars, run.Range, run.BidiLevel);
					int glyphrun_start = run.CpFirst;
					CharacterHit result = new CharacterHit(0, 0);
					Point origin = new Point();

					foreach (var glyphrun in formatted.GetGlyphRuns(ref origin))
					{
						result = glyphrun.GetNextCaretCharacterHit(
							new CharacterHit(characterHit.FirstCharacterIndex - glyphrun_start,
								characterHit.TrailingLength));
						result = new CharacterHit(result.FirstCharacterIndex + glyphrun_start, result.TrailingLength);

						var glyphrun_length = glyphrun.Characters.Count;

						if (desiredIndex < glyphrun_start + glyphrun_length)
							break;

						glyphrun_start += glyphrun_length;
					}
					return result;
				}
				else if (run.TextRun is TextEmbeddedObject)
				{
					return new CharacterHit(run.CpFirst, run.Length);
				}
				else
				{
					throw new NotImplementedException(String.Format("Managed.TextFormatting.FullTextLine.GetNextCharacterHit for {0}", run.TextRun.GetType().FullName));
				}
			}

			public override CharacterHit GetPreviousCaretCharacterHit(CharacterHit characterHit)
			{
				int desiredIndex = characterHit.FirstCharacterIndex + characterHit.TrailingLength - 1;
				OrderedTextRun run;
				if (!GetRunContainingCp(desiredIndex, out run))
				{
					// If it's out of bounds, get the first valid character hit.
					foreach (var ordered in ReorderRuns())
					{
						if (run.TextRun is null || (ordered.CpFirst < run.CpFirst && !(ordered.TextRun is TextEndOfLine)))
							run = ordered;
					}
				}
				if (run.TextRun is null || run.TextRun is TextEndOfLine)
				{
					// No valid character hit on this line.
					return new CharacterHit(0, 0);
				}
				else if (run.TextRun is TextCharacters)
				{
					var textChars = (TextCharacters)run.TextRun;
					var formatted = GetFormattedTextSymbols(textChars, run.Range, run.BidiLevel);
					int glyphrun_start = run.CpFirst;
					CharacterHit result = new CharacterHit(run.CpFirst, 0);
					Point origin = new Point();

					foreach (var glyphrun in formatted.GetGlyphRuns(ref origin))
					{
						var glyphrun_length = glyphrun.Characters.Count;

						if (desiredIndex < glyphrun_start + glyphrun_length)
						{
							result = glyphrun.GetPreviousCaretCharacterHit(
								new CharacterHit(characterHit.FirstCharacterIndex - glyphrun_start,
									characterHit.TrailingLength));
							result = new CharacterHit(result.FirstCharacterIndex + glyphrun_start, result.TrailingLength);
						}

						glyphrun_start += glyphrun_length;
					}
					return result;
				}
				else if (run.TextRun is TextEmbeddedObject)
				{
					return new CharacterHit(run.CpFirst, 0);
				}
				else
				{
					throw new NotImplementedException(String.Format("Managed.TextFormatting.FullTextLine.GetPreviousCharacterHit for {0}", run.TextRun.GetType().FullName));
				}
			}

			public override CharacterHit GetBackspaceCaretCharacterHit(CharacterHit characterHit)
			{
				int desiredIndex = characterHit.FirstCharacterIndex + characterHit.TrailingLength - 1;
				OrderedTextRun run;
				if (!GetRunContainingCp(desiredIndex, out run))
				{
					// If it's out of bounds, get the first valid character hit.
					foreach (var ordered in ReorderRuns())
					{
						if (run.TextRun is null || (ordered.CpFirst < run.CpFirst && !(ordered.TextRun is TextEndOfLine)))
							run = ordered;
					}
				}
				if (run.TextRun is null || run.TextRun is TextEndOfLine)
				{
					// No valid character hit on this line.
					return new CharacterHit(0, 0);
				}
				else if (run.TextRun is TextCharacters)
				{
					var textChars = (TextCharacters)run.TextRun;
					int glyphrun_start = run.CpFirst;

					if (desiredIndex < glyphrun_start)
						return new CharacterHit(glyphrun_start, 0);
					else
						return new CharacterHit(desiredIndex, 0);
				}
				else if (run.TextRun is TextEmbeddedObject)
				{
					return new CharacterHit(run.CpFirst, 0);
				}
				else
				{
					throw new NotImplementedException(String.Format("Managed.TextFormatting.FullTextLine.GetPreviousCharacterHit for {0}", run.TextRun.GetType().FullName));
				}
			}

			public override IList<TextBounds> GetTextBounds(int firstTextSourceCharacterIndex, int textLength)
			{
				var result = new List<TextBounds>();
				double x = GetOffsetToFirstRun().X;
				double paragraphWidth = (double)_paragraphWidth / TextFormatterImp.ToIdeal;

				foreach (var ordered in ReorderRuns())
				{
					if (ordered.TextRun is TextEndOfLine)
					{
						break;
					}
					else if (ordered.TextRun is TextCharacters)
					{
						var textChars = (TextCharacters)ordered.TextRun;
						var formatted = GetFormattedTextSymbols(textChars, ordered.Range, ordered.BidiLevel);

						if (ordered.CpFirst <= firstTextSourceCharacterIndex + textLength - 1 &&
							ordered.CpFirst + ordered.Length >= firstTextSourceCharacterIndex)
						{
							var flowDirection = (ordered.BidiLevel & 1) == 1 ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
							Rect? rectangle = null;
							var runBounds = new List<TextRunBounds> ();
							int glyphrun_start = ordered.CpFirst;
							Point origin = new Point(0,0);
							double glyphrun_x = 0;

							foreach (var glyphrun in formatted.GetGlyphRuns(ref origin))
							{
								var glyphAdvances = glyphrun.AdvanceWidths;

								var glyphBox = glyphrun.ComputeAlignmentBox();

								int glyphrun_length = glyphrun.Characters.Count;
								int subrange_start = Math.Max(firstTextSourceCharacterIndex, glyphrun_start);
								int subrange_end = Math.Min(firstTextSourceCharacterIndex + textLength,
									glyphrun_start + glyphrun_length);

								if (subrange_end <= subrange_start)
								{
									glyphrun_start += glyphrun_length;
									glyphrun_x += glyphBox.Width;
									continue;
								}

								double subrange_x = glyphrun.GetDistanceFromCaretCharacterHit(
									new CharacterHit(subrange_start - glyphrun_start, 0));
								double subrange_width = glyphrun.GetDistanceFromCaretCharacterHit(
									new CharacterHit(subrange_end - glyphrun_start - 1, 1)) - subrange_x;

								var subrange_rect = new Rect(x + glyphrun_x + subrange_x, 0, subrange_width, Height);

								if (rectangle is Rect rectangleValue)
								{
									rectangle = Rect.Intersect(subrange_rect, rectangleValue);
								}
								else
								{
									rectangle = subrange_rect;
								}

								runBounds.Add(new TextRunBounds(subrange_rect, subrange_start, subrange_end, ordered.TextRun));

								glyphrun_start += glyphrun_length;
								glyphrun_x += glyphBox.Width;
							}

							{ // extra scope so I can reuse this variable name and then complain about it
							if (rectangle is Rect rectangleValue)
							{
								result.Add(new TextBounds(rectangleValue, flowDirection, runBounds));
							}
							}
						}

						x += formatted.Width;
					}
					else if (ordered.TextRun is TextEmbeddedObject)
					{
						var width = ((TextEmbeddedObject)ordered.TextRun).Format(paragraphWidth - x).Width;

						if (ordered.CpFirst <= firstTextSourceCharacterIndex + textLength - 1 &&
							ordered.CpFirst + ordered.Length >= firstTextSourceCharacterIndex)
						{
							var flowDirection = (ordered.BidiLevel & 1) == 1 ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
							var runBounds = new List<TextRunBounds>();
							var rectangle = new Rect(x, 0, width, Height);

							runBounds.Add(new TextRunBounds(rectangle, ordered.CpFirst, ordered.CpFirst + ordered.Length, ordered.TextRun));
							result.Add(new TextBounds(rectangle, flowDirection, runBounds));
						}
						x += width;
					}
					else if (ordered.TextRun is TextHidden)
					{
						// Nothing to do.
					}
					else if (ordered.TextRun is TextModifier || ordered.TextRun is TextEndOfSegment)
					{
						// Nothing to do.
					}
					else
					{
						throw new NotImplementedException(String.Format("Managed.TextFormatting.FullTextLine.GetTextBounds for {0}", ordered.TextRun.GetType().FullName));
					}
				}
				
				if (result.Count == 0)
				{
					result.Add(new TextBounds(new Rect(0,0,0,Height), FlowDirection.LeftToRight, null));
				}

				return result;
			}

			public override IList<TextSpan<TextRun>> GetTextRunSpans()
			{
                if ((_statusFlags & StatusFlags.IsDisposed) != 0)
                {
                    throw new ObjectDisposedException(SR.Get(SRID.TextLineHasBeenDisposed));
                }

				if (_textRunSpans is null)
				{
					var store = _fullText.TextStore;
					var source = store.Settings.TextSource;
					int cpFirst = store.CpFirst;
					int pos = cpFirst;
					int endPos = pos + _metrics._cchLength;
					_textRunSpans = new List<TextSpan<TextRun>> ();
					while (pos < endPos)
					{
						TextRun textRun;
						int runLength;

						// Can't use TextStore here as the cache may give us a run containing the run we need
						textRun = source.GetTextRun(pos);
						runLength = textRun.Length;

						if (pos + runLength > endPos)
							runLength = endPos - pos;
						pos += runLength;
						_textRunSpans.Add(new TextSpan<TextRun>(runLength, textRun));
					}
				}
				return new List<TextSpan<TextRun>> (_textRunSpans);
			}

			public override IEnumerable<IndexedGlyphRun> GetIndexedGlyphRuns()
			{
				List<IndexedGlyphRun> result = new List<IndexedGlyphRun>();

				Point origin = GetOffsetToFirstRun();
				foreach (var ordered in ReorderRuns())
				{
					if (ordered.TextRun is TextEndOfLine)
					{
						break;
					}
					else if (ordered.TextRun is TextCharacters)
					{
						var textChars = (TextCharacters)ordered.TextRun;
						var formatted = GetFormattedTextSymbols(textChars, ordered.Range, ordered.BidiLevel);
						var cpFirst = ordered.CpFirst;

						foreach (var glyphrun in formatted.GetGlyphRuns(ref origin))
						{
							result.Add(new IndexedGlyphRun(cpFirst, glyphrun.Characters.Count, glyphrun));
							cpFirst += glyphrun.Characters.Count;
						}
					}
					else if (ordered.TextRun is TextEmbeddedObject)
					{
						// Nothing to do.
					}
					else if (ordered.TextRun is TextHidden)
					{
						// Nothing to do.
					}
					else if (ordered.TextRun is TextModifier || ordered.TextRun is TextEndOfSegment)
					{
						// Nothing to do.
					}
					else
					{
						throw new NotImplementedException(String.Format("Managed.TextFormatting.FullTextLine.GetIndexedGlyphRuns for {0}", ordered.TextRun.GetType().FullName));
					}
				}

				return result;
			}

			public override TextLineBreak GetTextLineBreak()
			{
				// FIXME: Under what circumstances does this return non-null?
				// Apparently, specifying a too small width isn't enough.
				return null;
			}

			public override int TrailingWhitespaceLength
			{
				get
				{
					return _metrics.TrailingWhitespaceLength;
				}
			}

			public override int Length
			{
				get
				{
					return _metrics.Length;
				}
			}

			public override int DependentLength
			{
				get
				{
					throw new NotImplementedException("Managed.TextFormatting.FullTextLine.get_DependentLength");
				}
			}

			public override int NewlineLength
			{
				get
				{
					return _metrics.NewlineLength;
				}
			}

			public override double Start
			{
				get
				{
					return _metrics.Start;
				}
			}

			public override double Width
			{
				get
				{
					return _metrics.Width;
				}
			}

			public override double WidthIncludingTrailingWhitespace
			{
				get
				{
					return _metrics.WidthIncludingTrailingWhitespace;
				}
			}

			public override double Height
			{
				get
				{
					return _metrics.Height;
				}
			}

			public override double TextHeight
			{
				get
				{
					throw new NotImplementedException("Managed.TextFormatting.FullTextLine.get_TextHeight");
				}
			}

			public override double Baseline
			{
				get
				{
					return _metrics.Baseline;
				}
			}

			public override double TextBaseline
			{
				get
				{
					throw new NotImplementedException("Managed.TextFormatting.FullTextLine.get_TextBaseline");
				}
			}

			public override double MarkerBaseline
			{
				get
				{
					throw new NotImplementedException("Managed.TextFormatting.FullTextLine.get_MarkerBaseline");
				}
			}

			public override double MarkerHeight
			{
				get
				{
					throw new NotImplementedException("Managed.TextFormatting.FullTextLine.get_MarkerHeight");
				}
			}

			public override double Extent
			{
				get
				{
					throw new NotImplementedException("Managed.TextFormatting.FullTextLine.get_Extent");
				}
			}

			public override double OverhangLeading
			{
				get
				{
					throw new NotImplementedException("Managed.TextFormatting.FullTextLine.get_OverhangLeading");
				}
			}

			public override double OverhangTrailing
			{
				get
				{
					throw new NotImplementedException("Managed.TextFormatting.FullTextLine.get_OverhangTrailing");
				}
			}

			public override double OverhangAfter
			{
				get
				{
					throw new NotImplementedException("Managed.TextFormatting.FullTextLine.get_OverhangAfter");
				}
			}

			public override bool HasOverflowed
			{
                get { return (_statusFlags & StatusFlags.HasOverflowed) != 0; }
			}

			public override bool HasCollapsed
			{
				get
				{
					throw new NotImplementedException("Managed.TextFormatting.FullTextLine.get_HasCollapsed");
				}
			}

			public override bool IsTruncated
			{
				get
				{
					throw new NotImplementedException("Managed.TextFormatting.FullTextLine.get_IsTruncated");
				}
			}
        }
    }
}

