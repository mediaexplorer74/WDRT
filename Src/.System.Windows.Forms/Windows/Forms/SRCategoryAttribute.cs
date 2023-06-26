using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x02000450 RID: 1104
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class SRCategoryAttribute : CategoryAttribute
	{
		// Token: 0x06004D57 RID: 19799 RVA: 0x0013AC14 File Offset: 0x00138E14
		public SRCategoryAttribute(string category)
			: base(category)
		{
		}

		// Token: 0x06004D58 RID: 19800 RVA: 0x0013F974 File Offset: 0x0013DB74
		protected override string GetLocalizedString(string value)
		{
			return SR.GetString(value);
		}
	}
}
