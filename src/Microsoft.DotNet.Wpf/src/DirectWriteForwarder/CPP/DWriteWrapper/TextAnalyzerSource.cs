using System;
using System.Runtime.InteropServices;

namespace MS.Internal.Text.TextInterface
{
	internal unsafe class TextAnalyzerSource : IDWriteTextAnalysisSource
	{
		struct TextSegment
		{
			internal IntPtr ptr;
			internal uint start;
			internal uint end;
		}

		uint TextLength;

		IDWriteFactory _factory;
		TextSegment[] TextSegments;
		GCHandle PinnedLocaleName;
		DWriteReadingDirection ReadingDirection;

		string _numberCulture;
		bool _ignoreUserOverride;
		uint _numberSubstitutionMethod;

		internal TextAnalyzerSource(char* text, uint length, string culture,
			IDWriteFactory factory, bool isRightToLeft, string numberCulture,
			bool ignoreUserOverride, uint numberSubstitutionMethod)
		{
			var segment = new TextSegment();
			segment.ptr = new IntPtr(text);
			segment.start = 0;
			segment.end = length;
			TextSegments = new TextSegment[] { segment };
			TextLength = length;
			PinnedLocaleName = GCHandle.Alloc(culture, GCHandleType.Pinned);
			ReadingDirection = isRightToLeft ? DWriteReadingDirection.RightToLeft : DWriteReadingDirection.LeftToRight;
			_factory = factory;
			_numberCulture = numberCulture;
			_ignoreUserOverride = ignoreUserOverride;
			_numberSubstitutionMethod = numberSubstitutionMethod;
		}

		internal TextAnalyzerSource(IntPtr[] text_ptrs, uint[] lengths, string culture,
			IDWriteFactory factory, bool isRightToLeft, string numberCulture,
			bool ignoreUserOverride, uint numberSubstitutionMethod)
		{
			TextSegments = new TextSegment[text_ptrs.Length];
			uint pos=0;
			for (int i=0; i < text_ptrs.Length; i++)
			{
				var segment = new TextSegment();
				segment.ptr = text_ptrs[i];
				segment.start = pos;
				pos += lengths[i];
				segment.end = pos;
				TextSegments[i] = segment;
			}
			TextLength = pos;
			PinnedLocaleName = GCHandle.Alloc(culture, GCHandleType.Pinned);
			ReadingDirection = isRightToLeft ? DWriteReadingDirection.RightToLeft : DWriteReadingDirection.LeftToRight;
			_factory = factory;
			_numberCulture = numberCulture;
			_ignoreUserOverride = ignoreUserOverride;
			_numberSubstitutionMethod = numberSubstitutionMethod;
		}

		private bool FindSegmentAtPosition(uint position, out int segment_index)
		{
			int first = 0;
			int last = TextSegments.Length - 1;

			if (position < 0 || position >= TextSegments[last].end)
			{
				segment_index = -1;
				return false;
			}

			// binary search
			while (last > first)
			{
				int candidate = (first + last) / 2;
				var segment = TextSegments[candidate];
				if (position < segment.start)
					last = candidate - 1;
				else if (position >= segment.end)
					first = candidate + 1;
				else
				{
					segment_index = candidate;
					return true;
				}
			}

			// The array of segments covers the full range, so we can assume we found it.
			segment_index = first;
			return true;
		}

		public void GetTextAtPosition(uint position, out IntPtr text, out uint text_len)
		{
			int index;
			if (FindSegmentAtPosition(position, out index))
			{
				var segment = TextSegments[index];
				text = IntPtr.Add(segment.ptr, (int)(position - segment.start));
				text_len = segment.end - position;
			}
			else
			{
				text = IntPtr.Zero;
				text_len = 0;
			}
		}

		public void GetTextBeforePosition(uint position, out IntPtr text, out uint text_len)
		{
			int index;
			if (position != 0 && FindSegmentAtPosition(position-1, out index))
			{
				var segment = TextSegments[index];
				text = segment.ptr;
				text_len = position - segment.start;
			}
			else
			{
				text = IntPtr.Zero;
				text_len = 0;
			}
		}

		public DWriteReadingDirection GetParagraphReadingDirection()
		{
			return ReadingDirection;
		}

		public void GetLocaleName(uint position, out uint text_len, out IntPtr locale)
		{
			text_len = TextLength - position;
			locale = PinnedLocaleName.AddrOfPinnedObject();
		}

		public void GetNumberSubstitution(uint position, out uint text_len, out IDWriteNumberSubstitution substitution)
		{
			text_len = TextLength - position;
			substitution = _factory.CreateNumberSubstitution(_numberSubstitutionMethod, _numberCulture, _ignoreUserOverride);
		}
	}
}
