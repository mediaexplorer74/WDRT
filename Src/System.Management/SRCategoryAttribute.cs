using System;
using System.ComponentModel;

namespace System.Management
{
	// Token: 0x020000A9 RID: 169
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class SRCategoryAttribute : CategoryAttribute
	{
		// Token: 0x0600047C RID: 1148 RVA: 0x00021EED File Offset: 0x000200ED
		public SRCategoryAttribute(string category)
			: base(category)
		{
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00021EF6 File Offset: 0x000200F6
		protected override string GetLocalizedString(string value)
		{
			return SR.GetString(value);
		}
	}
}
