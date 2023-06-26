using System;

namespace Microsoft.Data.OData
{
	// Token: 0x0200012D RID: 301
	internal abstract class AnnotationFilterPattern : IComparable<AnnotationFilterPattern>
	{
		// Token: 0x060007EF RID: 2031 RVA: 0x0001A533 File Offset: 0x00018733
		private AnnotationFilterPattern(string pattern, bool isExclude)
		{
			this.isExclude = isExclude;
			this.Pattern = pattern;
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x0001A549 File Offset: 0x00018749
		internal virtual bool IsExclude
		{
			get
			{
				return this.isExclude;
			}
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x0001A554 File Offset: 0x00018754
		public int CompareTo(AnnotationFilterPattern other)
		{
			ExceptionUtils.CheckArgumentNotNull<AnnotationFilterPattern>(other, "other");
			int num = AnnotationFilterPattern.ComparePatternPriority(this.Pattern, other.Pattern);
			if (num != 0)
			{
				return num;
			}
			if (this.IsExclude == other.IsExclude)
			{
				return 0;
			}
			if (!this.IsExclude)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0001A5A0 File Offset: 0x000187A0
		internal static AnnotationFilterPattern Create(string pattern)
		{
			AnnotationFilterPattern.ValidatePattern(pattern);
			bool flag = AnnotationFilterPattern.RemoveExcludeOperator(ref pattern);
			if (pattern == "*")
			{
				if (!flag)
				{
					return AnnotationFilterPattern.IncludeAllPattern;
				}
				return AnnotationFilterPattern.ExcludeAllPattern;
			}
			else
			{
				if (pattern.EndsWith(".*", StringComparison.Ordinal))
				{
					return new AnnotationFilterPattern.StartsWithPattern(pattern.Substring(0, pattern.Length - 1), flag);
				}
				return new AnnotationFilterPattern.ExactMatchPattern(pattern, flag);
			}
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0001A602 File Offset: 0x00018802
		internal static void Sort(AnnotationFilterPattern[] pattersToSort)
		{
			Array.Sort<AnnotationFilterPattern>(pattersToSort);
		}

		// Token: 0x060007F4 RID: 2036
		internal abstract bool Matches(string annotationName);

		// Token: 0x060007F5 RID: 2037 RVA: 0x0001A60C File Offset: 0x0001880C
		private static int ComparePatternPriority(string pattern1, string pattern2)
		{
			if (pattern1 == pattern2)
			{
				return 0;
			}
			if (pattern1 == "*")
			{
				return 1;
			}
			if (pattern2 == "*")
			{
				return -1;
			}
			if (pattern1.StartsWith(pattern2, StringComparison.Ordinal))
			{
				return -1;
			}
			if (pattern2.StartsWith(pattern1, StringComparison.Ordinal))
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0001A65B File Offset: 0x0001885B
		private static bool RemoveExcludeOperator(ref string pattern)
		{
			if (pattern[0] == '-')
			{
				pattern = pattern.Substring(1);
				return true;
			}
			return false;
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0001A678 File Offset: 0x00018878
		private static void ValidatePattern(string pattern)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(pattern, "pattern");
			string text = pattern;
			AnnotationFilterPattern.RemoveExcludeOperator(ref text);
			if (text == "*")
			{
				return;
			}
			string[] array = text.Split(new char[] { '.' });
			int num = array.Length;
			if (num == 1)
			{
				throw new ArgumentException(Strings.AnnotationFilterPattern_InvalidPatternMissingDot(pattern));
			}
			for (int i = 0; i < num; i++)
			{
				string text2 = array[i];
				if (string.IsNullOrEmpty(text2))
				{
					throw new ArgumentException(Strings.AnnotationFilterPattern_InvalidPatternEmptySegment(pattern));
				}
				if (text2 != "*" && text2.Contains("*"))
				{
					throw new ArgumentException(Strings.AnnotationFilterPattern_InvalidPatternWildCardInSegment(pattern));
				}
				bool flag = i + 1 == num;
				if (text2 == "*" && !flag)
				{
					throw new ArgumentException(Strings.AnnotationFilterPattern_InvalidPatternWildCardMustBeInLastSegment(pattern));
				}
			}
		}

		// Token: 0x04000305 RID: 773
		private const char NamespaceSeparator = '.';

		// Token: 0x04000306 RID: 774
		private const char ExcludeOperator = '-';

		// Token: 0x04000307 RID: 775
		private const string WildCard = "*";

		// Token: 0x04000308 RID: 776
		private const string DotStar = ".*";

		// Token: 0x04000309 RID: 777
		internal static readonly AnnotationFilterPattern IncludeAllPattern = new AnnotationFilterPattern.WildCardPattern(false);

		// Token: 0x0400030A RID: 778
		internal static readonly AnnotationFilterPattern ExcludeAllPattern = new AnnotationFilterPattern.WildCardPattern(true);

		// Token: 0x0400030B RID: 779
		protected readonly string Pattern;

		// Token: 0x0400030C RID: 780
		private readonly bool isExclude;

		// Token: 0x0200012E RID: 302
		private sealed class WildCardPattern : AnnotationFilterPattern
		{
			// Token: 0x060007F9 RID: 2041 RVA: 0x0001A75F File Offset: 0x0001895F
			internal WildCardPattern(bool isExclude)
				: base("*", isExclude)
			{
			}

			// Token: 0x060007FA RID: 2042 RVA: 0x0001A76D File Offset: 0x0001896D
			internal override bool Matches(string annotationName)
			{
				return true;
			}
		}

		// Token: 0x0200012F RID: 303
		private sealed class StartsWithPattern : AnnotationFilterPattern
		{
			// Token: 0x060007FB RID: 2043 RVA: 0x0001A770 File Offset: 0x00018970
			internal StartsWithPattern(string pattern, bool isExclude)
				: base(pattern, isExclude)
			{
			}

			// Token: 0x060007FC RID: 2044 RVA: 0x0001A77A File Offset: 0x0001897A
			internal override bool Matches(string annotationName)
			{
				return annotationName.StartsWith(this.Pattern, StringComparison.Ordinal);
			}
		}

		// Token: 0x02000130 RID: 304
		private sealed class ExactMatchPattern : AnnotationFilterPattern
		{
			// Token: 0x060007FD RID: 2045 RVA: 0x0001A789 File Offset: 0x00018989
			internal ExactMatchPattern(string pattern, bool isExclude)
				: base(pattern, isExclude)
			{
			}

			// Token: 0x060007FE RID: 2046 RVA: 0x0001A793 File Offset: 0x00018993
			internal override bool Matches(string annotationName)
			{
				return annotationName.Equals(this.Pattern, StringComparison.Ordinal);
			}
		}
	}
}
