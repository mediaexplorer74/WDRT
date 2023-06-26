using System;
using System.Linq;

namespace Microsoft.Data.OData
{
	// Token: 0x0200012A RID: 298
	internal class AnnotationFilter
	{
		// Token: 0x060007E6 RID: 2022 RVA: 0x0001A3E3 File Offset: 0x000185E3
		private AnnotationFilter(AnnotationFilterPattern[] prioritizedPatternsToMatch)
		{
			this.prioritizedPatternsToMatch = prioritizedPatternsToMatch;
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x0001A400 File Offset: 0x00018600
		internal static AnnotationFilter Create(string filter)
		{
			if (string.IsNullOrEmpty(filter))
			{
				return AnnotationFilter.ExcludeAll;
			}
			AnnotationFilterPattern[] array = (from pattern in filter.Split(AnnotationFilter.AnnotationFilterPatternSeparator)
				select AnnotationFilterPattern.Create(pattern.Trim())).ToArray<AnnotationFilterPattern>();
			AnnotationFilterPattern.Sort(array);
			if (array[0] == AnnotationFilterPattern.IncludeAllPattern)
			{
				return AnnotationFilter.IncludeAll;
			}
			if (array[0] == AnnotationFilterPattern.ExcludeAllPattern)
			{
				return AnnotationFilter.ExcludeAll;
			}
			return new AnnotationFilter(array);
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0001A47C File Offset: 0x0001867C
		internal virtual bool Matches(string annotationName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(annotationName, "annotationName");
			foreach (AnnotationFilterPattern annotationFilterPattern in this.prioritizedPatternsToMatch)
			{
				if (annotationFilterPattern.Matches(annotationName))
				{
					return !annotationFilterPattern.IsExclude;
				}
			}
			return false;
		}

		// Token: 0x04000300 RID: 768
		private static readonly AnnotationFilter IncludeAll = new AnnotationFilter.IncludeAllFilter();

		// Token: 0x04000301 RID: 769
		private static readonly AnnotationFilter ExcludeAll = new AnnotationFilter.ExcludeAllFilter();

		// Token: 0x04000302 RID: 770
		private static readonly char[] AnnotationFilterPatternSeparator = new char[] { ',' };

		// Token: 0x04000303 RID: 771
		private readonly AnnotationFilterPattern[] prioritizedPatternsToMatch;

		// Token: 0x0200012B RID: 299
		private sealed class IncludeAllFilter : AnnotationFilter
		{
			// Token: 0x060007EB RID: 2027 RVA: 0x0001A4FB File Offset: 0x000186FB
			internal IncludeAllFilter()
				: base(new AnnotationFilterPattern[0])
			{
			}

			// Token: 0x060007EC RID: 2028 RVA: 0x0001A509 File Offset: 0x00018709
			internal override bool Matches(string annotationName)
			{
				ExceptionUtils.CheckArgumentStringNotNullOrEmpty(annotationName, "annotationName");
				return true;
			}
		}

		// Token: 0x0200012C RID: 300
		private sealed class ExcludeAllFilter : AnnotationFilter
		{
			// Token: 0x060007ED RID: 2029 RVA: 0x0001A517 File Offset: 0x00018717
			internal ExcludeAllFilter()
				: base(new AnnotationFilterPattern[0])
			{
			}

			// Token: 0x060007EE RID: 2030 RVA: 0x0001A525 File Offset: 0x00018725
			internal override bool Matches(string annotationName)
			{
				ExceptionUtils.CheckArgumentStringNotNullOrEmpty(annotationName, "annotationName");
				return false;
			}
		}
	}
}
