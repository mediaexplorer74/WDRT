using System;
using System.ComponentModel;

namespace System
{
	// Token: 0x02000065 RID: 101
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class SRCategoryAttribute : CategoryAttribute
	{
		// Token: 0x06000452 RID: 1106 RVA: 0x0001E903 File Offset: 0x0001CB03
		public SRCategoryAttribute(string category)
			: base(category)
		{
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0001E90C File Offset: 0x0001CB0C
		protected override string GetLocalizedString(string value)
		{
			return SR.GetString(value);
		}
	}
}
