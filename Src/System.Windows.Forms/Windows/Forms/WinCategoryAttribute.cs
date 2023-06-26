using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x02000440 RID: 1088
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class WinCategoryAttribute : CategoryAttribute
	{
		// Token: 0x06004BAC RID: 19372 RVA: 0x0013AC14 File Offset: 0x00138E14
		public WinCategoryAttribute(string category)
			: base(category)
		{
		}

		// Token: 0x06004BAD RID: 19373 RVA: 0x0013AC20 File Offset: 0x00138E20
		protected override string GetLocalizedString(string value)
		{
			string text = base.GetLocalizedString(value);
			if (text == null)
			{
				text = (string)SR.GetObject("WinFormsCategory" + value);
			}
			return text;
		}
	}
}
