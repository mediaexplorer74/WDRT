using System;
using System.ComponentModel;

namespace System.Drawing
{
	// Token: 0x02000050 RID: 80
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class SRCategoryAttribute : CategoryAttribute
	{
		// Token: 0x060006F7 RID: 1783 RVA: 0x0001C42D File Offset: 0x0001A62D
		public SRCategoryAttribute(string category)
			: base(category)
		{
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0001C436 File Offset: 0x0001A636
		protected override string GetLocalizedString(string value)
		{
			return SR.GetString(value);
		}
	}
}
