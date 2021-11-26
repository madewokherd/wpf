// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Media;

namespace MS.Internal.Text.TextInterface
{
public class TextAnalyzer
{
	IDWriteFactory _factory;
	IDWriteTextAnalyzer _textAnalyzer;

    public TextAnalyzer(IDWriteFactory factory, IDWriteTextAnalyzer textAnalyzer)
    {
        _factory = factory;
        _textAnalyzer = textAnalyzer;
    }

    unsafe public IList<Span> Itemize(
        char* 							 text,
        uint                             length,
        CultureInfo                      culture,
        bool                             isRightToLeftParagraph,
        CultureInfo                      numberCulture,
        bool                             ignoreUserOverride,
        uint                             numberSubstitutionMethod,
        IClassification                  classificationUtility
        )
    {
        // If a text has zero length then we do not need to itemize.
        if (length > 0)
        {
            TextAnalyzerSink textAnalyzerSink = null;
            TextAnalyzerSource textAnalyzerSource = null;
			
			string numberSubstitutionLocaleName = numberCulture != null ? numberCulture.IetfLanguageTag : null;

			// NOTE: the text parameter is NOT copied inside TextAnalysisSource to improve perf.
			// This is ok as long as we use the TextAnalysisSource in the same scope as we hold ref to text.
			// If we are ever to change this pattern then this should be revisited in TextAnalysisSource in
			// PresentationNative.
			textAnalyzerSource = new TextAnalyzerSource(
											 text,
											 length,
											 culture.IetfLanguageTag,
											 _factory,
											 isRightToLeftParagraph,
											 numberSubstitutionLocaleName,
											 ignoreUserOverride,
											 numberSubstitutionMethod);
		
			textAnalyzerSink = new TextAnalyzerSink();

			// Analyze the script ranges.
			_textAnalyzer.AnalyzeScript(textAnalyzerSource,
										0,
										length,
										textAnalyzerSink);

			// Analyze the number substitution ranges.
			_textAnalyzer.AnalyzeNumberSubstitution(textAnalyzerSource,
													0,
													length,
													textAnalyzerSink);

			var dwriteScriptAnalysisList = textAnalyzerSink.ScriptAnalysis;
			var dwriteNumberSubstitutionList = textAnalyzerSink.NumberSubstitution;
			
			TextItemizer textItemizer = new TextItemizer(dwriteScriptAnalysisList, dwriteNumberSubstitutionList);
		
			return AnalyzeExtendedAndItemize(textItemizer, new IntPtr(text), length, numberCulture, classificationUtility);
        }
        else
        {
            return null;
        }

    }

    IList<Span> AnalyzeExtendedAndItemize(
        TextItemizer textItemizer, 
        IntPtr text, 
        uint length, 
        CultureInfo numberCulture, 
        IClassification classification
        )
    {
        Debug.Assert(length >= 0);

        CharAttribute[] pCharAttribute = new CharAttribute[length];

		// Analyze the extended character ranges.
		AnalyzeExtendedCharactersAndDigits(text, length, textItemizer, pCharAttribute, numberCulture, classification);
		return textItemizer.Itemize(numberCulture, pCharAttribute);
    }

    public void AnalyzeExtendedCharactersAndDigits(
        IntPtr                           text,
        uint                             length,
        TextItemizer                     textItemizer,
        CharAttribute[] 				 pCharAttribute,
        CultureInfo                      numberCulture,
        IClassification                  classificationUtility
        )
    {
        // Text will never be of zero length. This is enforced by Itemize().
        bool isCombining;
        bool needsCaretInfo;
        bool isIndic;
        bool isDigit;
        bool isLatin;
        bool isStrong;
        bool isExtended;

		char ch = (char)Marshal.ReadInt16(text);

        classificationUtility.GetCharAttribute(
            ch,
            out isCombining,
            out needsCaretInfo,
            out isIndic,
            out isDigit,
            out isLatin,
            out isStrong
            );

        isExtended = ItemizerHelper.IsExtendedCharacter(ch);

        uint isDigitRangeStart = 0;
        uint isDigitRangeEnd = 0;
        bool   previousIsDigitValue = (numberCulture == null) ? false : isDigit;
        bool   currentIsDigitValue;

        // pCharAttribute is assumed to have the same length as text. This is enforced by Itemize().
        pCharAttribute[0] = (CharAttribute)
                            (((isCombining)    ? CharAttribute.IsCombining    : CharAttribute.None)
                           | ((needsCaretInfo) ? CharAttribute.NeedsCaretInfo : CharAttribute.None)
                           | ((isLatin)        ? CharAttribute.IsLatin        : CharAttribute.None)
                           | ((isIndic)        ? CharAttribute.IsIndic        : CharAttribute.None)
                           | ((isStrong)       ? CharAttribute.IsStrong       : CharAttribute.None)
                           | ((isExtended)     ? CharAttribute.IsExtended     : CharAttribute.None));

		uint i;
        for (i = 1; i < length; ++i)
        {
			ch = (char)Marshal.ReadInt16(text, 2 * (int)i);

            classificationUtility.GetCharAttribute(
            ch,
            out isCombining,
            out needsCaretInfo,
            out isIndic,
            out isDigit,
            out isLatin,
            out isStrong
            );

            isExtended = ItemizerHelper.IsExtendedCharacter(ch);
            

            pCharAttribute[i] = (CharAttribute)
                                (((isCombining)    ? CharAttribute.IsCombining    : CharAttribute.None)
                               | ((needsCaretInfo) ? CharAttribute.NeedsCaretInfo : CharAttribute.None)
                               | ((isLatin)        ? CharAttribute.IsLatin        : CharAttribute.None)
                               | ((isIndic)        ? CharAttribute.IsIndic        : CharAttribute.None)
                               | ((isStrong)       ? CharAttribute.IsStrong       : CharAttribute.None)
                               | ((isExtended)     ? CharAttribute.IsExtended     : CharAttribute.None));


            currentIsDigitValue = (numberCulture == null) ? false : isDigit;
            if (previousIsDigitValue != currentIsDigitValue)
            {

                isDigitRangeEnd = i;
                textItemizer.SetIsDigit(isDigitRangeStart, isDigitRangeEnd - isDigitRangeStart, previousIsDigitValue);

                isDigitRangeStart = i;
                previousIsDigitValue = currentIsDigitValue;
            }
        }


        isDigitRangeEnd = i;
        textItemizer.SetIsDigit(isDigitRangeStart, isDigitRangeEnd - isDigitRangeStart, previousIsDigitValue);
    }

	public static char CharHyphen = (char)0x2d;

    unsafe void GetBlankGlyphsForControlCharacters(
        IntPtr                                       pTextString,
        uint                                         textLength,
        FontFace                                     fontFace,
        ushort                                       blankGlyphIndex,
        uint                                         maxGlyphCount,
        ushort*                                      clusterMap, /* ushort[textLength] */
        ushort*                                      glyphIndices, /* ushort[maxGlyphCount] */
        int*             							 pfCanGlyphAlone, /* int[textLength] */
        [Out] out uint 								 actualGlyphCount
        )
    {
        actualGlyphCount = textLength;
        // There is not enough buffer allocated. Signal to the caller the correct buffer size.
        if (maxGlyphCount < textLength)
        {
            return;
        }

        if (textLength > UInt16.MaxValue)
        {
			throw new ArgumentException("textLength must be no more than UInt16.MaxValue");
        }

        ushort textLengthShort = (ushort)textLength;

        uint softHyphen = (uint)CharHyphen;
        ushort hyphenGlyphIndex = 0;
        for (ushort i = 0; i < textLengthShort; ++i)
        {
            // LS will sometimes replace soft hyphens (which are invisible) with hyphens (which are visible).
            // So if we are in this code then LS actually did this replacement and we need to display the hyphen (x002D)
            if (Marshal.ReadInt16(pTextString, i*2) == CharHyphen)
            {
                if (hyphenGlyphIndex == 0)
                {
					fontFace.GetArrayOfGlyphIndices(&softHyphen, 1, &hyphenGlyphIndex);
                }
                glyphIndices[i] = hyphenGlyphIndex;
            }
            else
            {
                glyphIndices[i] = blankGlyphIndex;
            }
            clusterMap     [i] = i;
            if (pfCanGlyphAlone != null)
				pfCanGlyphAlone[i] = 1;
        }
    }

    public unsafe void GetGlyphs(
        IntPtr                                       textString,
    	uint                                         textLength,
        Font                                         font,
        ushort                                       blankGlyphIndex,
        bool                                         isSideways,
        bool                                         isRightToLeft,
        CultureInfo                                  cultureInfo,
        DWriteFontFeature[][]                        features,
        uint[]                                       featureRangeLengths,
        uint                                         maxGlyphCount,
        TextFormattingMode                           textFormattingMode,
        ItemProps                                    itemProps,
        ushort*             						 clusterMap, /* textLength */
        ushort*             						 textProps, /* textLength */
        ushort*          							 glyphIndices, /* maxGlyphCount */
        ushort*       								 glyphProps, /* maxGlyphCount */
        int*                						 pfCanGlyphAlone, /* textLength */
        [Out] out uint 								 actualGlyphCount
        )
    {
        // Shaping should not have taken place if ScriptAnalysis is NULL!
    	Debug.Assert(itemProps.ScriptAnalysis != null);

        // These are control characters and WPF will not display control characters.
        if (GetScriptShapes(itemProps) != DWriteScriptShapes.Default)
        {
            FontFace fontFace = font.GetFontFace();
            try
            {
                GetBlankGlyphsForControlCharacters(
                    textString,
                    textLength,
                    fontFace,
                    blankGlyphIndex,
                    maxGlyphCount,
                    clusterMap,
                    glyphIndices,
                    pfCanGlyphAlone,
                    out actualGlyphCount
                    );
            }
            finally
            {
                fontFace.Release();
            }
        }
        else
        {
            string localeName = cultureInfo.IetfLanguageTag;
			var featureRangeLengthsNonNull = (featureRangeLengths != null) ? featureRangeLengths : new uint[0];
			fixed (uint* pFeatureRangeLengthsPinned = featureRangeLengthsNonNull)
			{
				uint* pFeatureRangeLengths = null;

				uint featureRanges = 0;
				GCHandle[] dwriteFontFeaturesGCHandles = null;
				IntPtr[] dwriteTypographicFeatures = null;
				IntPtr dwriteTypographicFeaturesMemory = IntPtr.Zero;

				if (features != null)
				{
					pFeatureRangeLengths = pFeatureRangeLengthsPinned;
					featureRanges = (uint)featureRangeLengths.Length;
					dwriteFontFeaturesGCHandles = new GCHandle[featureRanges];
					dwriteTypographicFeatures = new IntPtr[featureRanges];
					dwriteTypographicFeaturesMemory = Marshal.AllocCoTaskMem(Marshal.SizeOf<DWriteTypographicFeatures>() * (int)featureRanges);
				}

				FontFace fontFace = font.GetFontFace();
				try
				{
					if (features != null)
					{
						for (uint i = 0; i < featureRanges; ++i)
						{
							dwriteFontFeaturesGCHandles[i] = GCHandle.Alloc(features[i], GCHandleType.Pinned);
							var new_feature = new DWriteTypographicFeatures();
							new_feature.features = dwriteFontFeaturesGCHandles[i].AddrOfPinnedObject();
							new_feature.featureCount = features[i].Length;
							dwriteTypographicFeatures[i] = dwriteTypographicFeaturesMemory + (int)i * Marshal.SizeOf<DWriteTypographicFeatures>();
							Marshal.StructureToPtr<DWriteTypographicFeatures>(new_feature, dwriteTypographicFeatures[i], false);
						}
					}


					uint glyphCount = 0;
					IntPtr dwriteScriptAnalysis = itemProps.ScriptAnalysisCoTaskMem();

					int hr = _textAnalyzer.GetGlyphs(
						textString,
						/*checked*/((uint)textLength),
						fontFace.DWriteFontFaceNoAddRef,
						isSideways,
						isRightToLeft,
						dwriteScriptAnalysis,
						localeName,
						itemProps.NumberSubstitution,
						dwriteTypographicFeatures,
						pFeatureRangeLengths,
						featureRanges,
						/*checked*/((uint)maxGlyphCount),
						clusterMap,
						textProps,
						glyphIndices,
						glyphProps,
						out glyphCount
						);

					if (unchecked((int)0x80070057) == hr) // E_INVALIDARG
					{
						// If pLocaleName is unsupported (e.g. "prs-af"), DWrite returns E_INVALIDARG.
						// Try again with the default mapping.
						hr = _textAnalyzer.GetGlyphs(
							textString,
							/*checked*/((uint)textLength),
							fontFace.DWriteFontFaceNoAddRef,
							isSideways,
							isRightToLeft,
							dwriteScriptAnalysis,
							null,
							itemProps.NumberSubstitution,
							dwriteTypographicFeatures,
							pFeatureRangeLengths,
							featureRanges,
							/*checked*/((uint)maxGlyphCount),
							clusterMap,
							textProps,
							glyphIndices,
							glyphProps,
							out glyphCount
							);
					}

					Marshal.FreeCoTaskMem(dwriteScriptAnalysis);

					if (features != null)
					{
						for (uint i = 0; i < featureRanges; ++i)
						{
							dwriteFontFeaturesGCHandles[i].Free();
						}
						Marshal.FreeCoTaskMem(dwriteTypographicFeaturesMemory);
					}

					if (hr == unchecked((int)0x8007007a)) // HRESULT_FROM_WIN32(ERROR_INSUFFICIENT_BUFFER)
					{
						// Actual glyph count is not returned by DWrite unless the call tp GetGlyphs succeeds.
						// It must be re-estimated in case the first estimate was not adequate.
						// The following calculation is a refactoring of DWrite's logic ( 3*stringLength/2 + 16) after 3 retries.
						// We'd rather go directly to the maximum buffer size we are willing to allocate than pay the cost of continuously retrying.
						actualGlyphCount = 27 * maxGlyphCount / 8 + 76;
					}
					else
					{
						Marshal.ThrowExceptionForHR(hr);
						if (pfCanGlyphAlone != null)
						{
							for (uint i = 0; i < textLength; ++i)
							{
								pfCanGlyphAlone[i] = (DWriteBitfieldUtils.ShapingText_IsShapedAlone(textProps[i])) ? 1 : 0;
							}
						}

						actualGlyphCount = glyphCount;
					}
				}
				finally
				{
					fontFace.Release();
				}
			}
		}
	}

    unsafe void GetGlyphPlacementsForControlCharacters(
        IntPtr pTextString,
        uint textLength,
        Font font,
        TextFormattingMode textFormattingMode,
        double fontEmSize,
        double scalingFactor,
        bool isSideways,
        float pixelsPerDip,
        uint glyphCount,
        ushort* pGlyphIndices, /* [in] glyphCount */
        int* glyphAdvances, /* glyphCount */
        [Out] out GlyphOffset[] glyphOffsets
        )
    {
        if (glyphCount != textLength)
        {
			throw new ArgumentException("glyphCount must equal textLength");
        }

        glyphOffsets = new GlyphOffset[textLength];
        FontFace fontFace = font.GetFontFace();

        try
        {
            int hyphenAdvanceWidth = -1;
            for (uint i = 0; i < textLength; ++i)
            {
                // LS will sometimes replace soft hyphens (which are invisible) with hyphens (which are visible).
                // So if we are in this code then LS actually did this replacement and we need to display the hyphen (x002D)
                if (Marshal.ReadInt16(pTextString, (int)i*2) == CharHyphen)
                {
                    if (hyphenAdvanceWidth == -1)
                    {
                        GlyphMetrics glyphMetrics;

						if (textFormattingMode == TextFormattingMode.Ideal)
						{
							fontFace.GetDesignGlyphMetrics(pGlyphIndices + i, 1, &glyphMetrics);
						}
						else
						{
							fontFace.GetDisplayGlyphMetrics(
								pGlyphIndices + i,
								1,
								&glyphMetrics,
								(float)fontEmSize,
								textFormattingMode != TextFormattingMode.Display,
								isSideways,
								pixelsPerDip);
						}
                        double approximatedHyphenAW = Math.Round(glyphMetrics.AdvanceWidth * fontEmSize / font.Metrics.DesignUnitsPerEm * pixelsPerDip) / pixelsPerDip;
                        hyphenAdvanceWidth = (int)Math.Round(approximatedHyphenAW * scalingFactor);
                    }

                    glyphAdvances[i] = hyphenAdvanceWidth;
                }
                else
                {
                    glyphAdvances[i] = 0;
                }
                glyphOffsets[i].du = 0;
                glyphOffsets[i].dv = 0;
            }
        }
        finally
        {
            fontFace.Release();
        }
    }

    unsafe public void GetGlyphPlacements(
        IntPtr                  textString,
        ushort*                 clusterMap, /* textLength */
        ushort*                 textProps, /* textLength */
        uint                    textLength,
        ushort*                 glyphIndices, /* glyphCount */
        ushort*                 glyphProps, /* glyphCount */
        uint                    glyphCount,
        Font                    font,
        double                  fontEmSize,
        double                  scalingFactor,
        bool                    isSideways,
        bool                    isRightToLeft,
        CultureInfo             cultureInfo,
        DWriteFontFeature[][]   features,
        uint[]                  featureRangeLengths,
        TextFormattingMode      textFormattingMode,
        ItemProps               itemProps,
        float                   pixelsPerDip,
        int*                    glyphAdvances, /* glyphCount */
        [Out] out GlyphOffset[] glyphOffsets
        )
    {
        // Shaping should not have taken place if ScriptAnalysis is NULL!
    	Debug.Assert(itemProps.ScriptAnalysis != null);

        // These are control characters and WPF will not display control characters.
        if (GetScriptShapes(itemProps) != DWriteScriptShapes.Default)
        {
            GetGlyphPlacementsForControlCharacters(
                textString,
                textLength,
                font,
                textFormattingMode,
                fontEmSize,
                scalingFactor,
                isSideways,
                pixelsPerDip,
                glyphCount,
                glyphIndices,
                glyphAdvances,
                out glyphOffsets
                );
        }
        else
        {
            float[] dwriteGlyphAdvances = new float[glyphCount];
            DWriteGlyphOffset[] dwriteGlyphOffsets = null;

			var featureRangeLengthsNonNull = featureRangeLengths != null ? featureRangeLengths : new uint[0];
			fixed (uint* pFeatureRangeLengthsPinned = featureRangeLengthsNonNull)
			{
				GCHandle[] dwriteFontFeaturesGCHandles = null;
				uint featureRanges = 0;
				IntPtr[] dwriteTypographicFeatures = null;
				IntPtr dwriteTypographicFeaturesMemory = IntPtr.Zero;
				uint* pFeatureRangeLengths = null;

				if (features != null)
				{
					pFeatureRangeLengths = pFeatureRangeLengthsPinned;
					featureRanges = (uint)featureRangeLengths.Length;
					dwriteTypographicFeatures = new IntPtr[featureRanges];
					dwriteFontFeaturesGCHandles = new GCHandle[featureRanges];
					dwriteTypographicFeatures = new IntPtr[featureRanges];
					dwriteTypographicFeaturesMemory = Marshal.AllocCoTaskMem(Marshal.SizeOf<DWriteTypographicFeatures>() * (int)featureRanges);
				}

				FontFace fontFace = font.GetFontFace();
				IntPtr scriptAnalysis = itemProps.ScriptAnalysisCoTaskMem ();
				try
				{
					string localeName = cultureInfo.IetfLanguageTag;
					DWriteMatrix transform = Factory.GetIdentityTransform();

					if (features != null)
					{
						for (uint i = 0; i < featureRanges; ++i)
						{
							dwriteFontFeaturesGCHandles[i] = GCHandle.Alloc(features[i], GCHandleType.Pinned);
							var new_feature = new DWriteTypographicFeatures();
							new_feature.features = dwriteFontFeaturesGCHandles[i].AddrOfPinnedObject();
							new_feature.featureCount = features[i].Length;
							dwriteTypographicFeatures[i] = dwriteTypographicFeaturesMemory + (int)i * Marshal.SizeOf<DWriteTypographicFeatures>();
							Marshal.StructureToPtr<DWriteTypographicFeatures>(new_feature, dwriteTypographicFeatures[i], false);
						}
					}

					float fontEmSizeFloat = (float)fontEmSize;

					dwriteGlyphOffsets = new DWriteGlyphOffset[glyphCount];
					if (textFormattingMode == TextFormattingMode.Ideal)
					{   
						try {
							_textAnalyzer.GetGlyphPlacements(
								textString,
								clusterMap,
								textProps,
								textLength,
								glyphIndices,
								glyphProps,
								glyphCount,
								fontFace.DWriteFontFaceNoAddRef,
								fontEmSizeFloat,
								isSideways,
								isRightToLeft,
								scriptAnalysis,
								localeName,
								dwriteTypographicFeatures,
								pFeatureRangeLengths,
								featureRanges,
								dwriteGlyphAdvances,
								dwriteGlyphOffsets
								);
						}
						catch (ArgumentException)
						{
							// If pLocaleName is unsupported (e.g. "prs-af"), DWrite returns E_INVALIDARG.
							// Try again with the default mapping.
							_textAnalyzer.GetGlyphPlacements(
								textString,
								clusterMap,
								textProps,
								textLength,
								glyphIndices,
								glyphProps,
								glyphCount,
								fontFace.DWriteFontFaceNoAddRef,
								fontEmSizeFloat,
								isSideways,
								isRightToLeft,
								scriptAnalysis,
								null,
								dwriteTypographicFeatures,
								pFeatureRangeLengths,
								featureRanges,
								dwriteGlyphAdvances,
								dwriteGlyphOffsets
								);
						}
						
					}
					else
					{
						Debug.Assert(textFormattingMode == TextFormattingMode.Display);

						try {
							_textAnalyzer.GetGdiCompatibleGlyphPlacements(
								textString,
								clusterMap,
								textProps,
								textLength,
								glyphIndices,
								glyphProps,
								glyphCount,
								fontFace.DWriteFontFaceNoAddRef,
								fontEmSizeFloat,
								pixelsPerDip,
								ref transform,
								false,
								isSideways,
								isRightToLeft,
								scriptAnalysis,
								localeName,
								dwriteTypographicFeatures,
								pFeatureRangeLengths,
								featureRanges,
								dwriteGlyphAdvances,
								dwriteGlyphOffsets
								);
						}
						catch (ArgumentException) {
							// If pLocaleName is unsupported (e.g. "prs-af"), DWrite returns E_INVALIDARG.
							// Try again with the default mapping.
							_textAnalyzer.GetGdiCompatibleGlyphPlacements(
								textString,
								clusterMap,
								textProps,
								textLength,
								glyphIndices,
								glyphProps,
								glyphCount,
								fontFace.DWriteFontFaceNoAddRef,
								fontEmSizeFloat,
								pixelsPerDip,
								ref transform,
								false,
								isSideways,
								isRightToLeft,
								scriptAnalysis,
								null,
								dwriteTypographicFeatures,
								pFeatureRangeLengths,
								featureRanges,
								dwriteGlyphAdvances,
								dwriteGlyphOffsets
								);
						}
					}

					glyphOffsets = new GlyphOffset[glyphCount];
					if (textFormattingMode == TextFormattingMode.Ideal)
					{
						for (uint i = 0; i < glyphCount; ++i)
						{
							glyphAdvances[i] = (int)Math.Round(dwriteGlyphAdvances[i] * fontEmSize * scalingFactor / fontEmSizeFloat);
							glyphOffsets[i].du = (int)(dwriteGlyphOffsets[i].AdvanceOffset * scalingFactor);
							glyphOffsets[i].dv = (int)(dwriteGlyphOffsets[i].AscenderOffset * scalingFactor);
						}
					}
					else
					{
						for (uint i = 0; i < glyphCount; ++i)
						{
							glyphAdvances[i] = (int)Math.Round(dwriteGlyphAdvances[i] * scalingFactor);
							glyphOffsets[i].du = (int)(dwriteGlyphOffsets[i].AdvanceOffset * scalingFactor);
							glyphOffsets[i].dv = (int)(dwriteGlyphOffsets[i].AscenderOffset * scalingFactor);
						}
					}                
				}
				finally
				{
					Marshal.FreeCoTaskMem(scriptAnalysis);

					if (features != null)
					{
						for (uint i = 0; i < featureRanges; ++i)
						{
							dwriteFontFeaturesGCHandles[i].Free();
						}
						Marshal.FreeCoTaskMem(dwriteTypographicFeaturesMemory);
					}

					fontFace.Release();
				}
			}
        }
    }

    unsafe public void GetGlyphsAndTheirPlacements(
        IntPtr textString,
        uint textLength,
        Font font,
        ushort blankGlyphIndex,
        bool isSideways,
        bool isRightToLeft,
        CultureInfo cultureInfo,
        DWriteFontFeature[][] features,
        uint[] featureRangeLengths,
        double fontEmSize,
        double scalingFactor,
        float pixelsPerDip,
        TextFormattingMode textFormattingMode,
        ItemProps     itemProps,
        [Out] out ushort[] clusterMap,
        [Out] out ushort[] glyphIndices,
        [Out] out int[] glyphAdvances,
        [Out] out GlyphOffset[] glyphOffsets
        )
    {
        uint maxGlyphCount = 3 * textLength;
        clusterMap = new ushort[textLength];
		ushort[] textProps = new ushort[textLength];
        fixed (ushort* pClusterMapPinned = clusterMap,
				pTextProps = textProps)
		{
			IntPtr glyphProps = IntPtr.Zero; // ushort*
			ushort[] glyphIndicesNative = null; // ushort*

			try
			{
				uint actualGlyphCount = maxGlyphCount + 1;

				// Loop and everytime increase the size of the GlyphIndices buffer.
				while(actualGlyphCount > maxGlyphCount)
				{
					maxGlyphCount = actualGlyphCount;
					if (glyphProps != IntPtr.Zero)
					{
						Marshal.FreeCoTaskMem(glyphProps);
						glyphProps = IntPtr.Zero;
					}
					glyphProps = Marshal.AllocCoTaskMem(2 * (int)maxGlyphCount);

					glyphIndicesNative = new ushort[maxGlyphCount];

					fixed (ushort* pGlyphIndicesNative = glyphIndicesNative)
					{
						GetGlyphs(
							textString,
							textLength,
							font,
							blankGlyphIndex,
							isSideways,
							isRightToLeft,
							cultureInfo,
							features,
							featureRangeLengths,
							maxGlyphCount,
							textFormattingMode,
							itemProps,
							pClusterMapPinned,
							pTextProps,
							pGlyphIndicesNative,
							(ushort*)glyphProps.ToPointer(),
							null,
							out actualGlyphCount
							);
					}
				}

				glyphIndices = new ushort[actualGlyphCount];
				Array.Copy(glyphIndicesNative, glyphIndices, actualGlyphCount);

				glyphAdvances = new int[actualGlyphCount];
				fixed (int* glyphAdvancesPinned = glyphAdvances) {
					fixed (ushort* pGlyphIndicesNative = glyphIndicesNative) {
						GetGlyphPlacements(
							textString,
							pClusterMapPinned,
							pTextProps,
							textLength,
							pGlyphIndicesNative,
							(ushort*)glyphProps.ToPointer(),
							actualGlyphCount,
							font,
							fontEmSize,
							scalingFactor,
							isSideways,
							isRightToLeft,
							cultureInfo,
							features,
							featureRangeLengths,
							textFormattingMode,
							itemProps,
							pixelsPerDip,
							glyphAdvancesPinned,
							out glyphOffsets
							);
					}
				}
			}
			finally
			{
				Marshal.FreeCoTaskMem(glyphProps);
			}
		}
    }

    DWriteScriptShapes GetScriptShapes(ItemProps itemProps)
    {
        return ((DWriteScriptAnalysis)itemProps.ScriptAnalysis).Shapes;
    }

	public LineBreakpoints AnalyzeLineBreakpoints(IntPtr[] text_ptrs, uint[] lengths, CultureInfo culture,
		Factory factory,
		uint range_start, uint range_length, bool isRightToLeft, CultureInfo numberCulture,
		bool ignoreUserOverride, uint numberSubstitutionMethod)
	{
		TextAnalyzerSink textAnalyzerSink = null;
		TextAnalyzerSource textAnalyzerSource = null;

		string numberSubstitutionLocaleName = numberCulture != null ? numberCulture.IetfLanguageTag : null;

		textAnalyzerSource = new TextAnalyzerSource(
										 text_ptrs,
										 lengths,
										 culture.IetfLanguageTag,
										 _factory,
										 isRightToLeft,
										 numberSubstitutionLocaleName,
										 ignoreUserOverride,
										 numberSubstitutionMethod);

		textAnalyzerSink = new TextAnalyzerSink();

		textAnalyzerSink.InitializeLineBreakpoints((int)range_start, (int)(range_start + range_length));

		_textAnalyzer.AnalyzeLineBreakpoints(textAnalyzerSource, range_start, range_length, textAnalyzerSink);

		return textAnalyzerSink.LineBreakpoints;
	}	
}
}
