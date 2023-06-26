using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x0200044F RID: 1103
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class SRDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x06004D55 RID: 19797 RVA: 0x0013F943 File Offset: 0x0013DB43
		public SRDescriptionAttribute(string description)
			: base(description)
		{
		}

		// Token: 0x170012F8 RID: 4856
		// (get) Token: 0x06004D56 RID: 19798 RVA: 0x0013F94C File Offset: 0x0013DB4C
		public override string Description
		{
			get
			{
				if (!this.replaced)
				{
					this.replaced = true;
					base.DescriptionValue = SR.GetString(base.Description);
				}
				return base.Description;
			}
		}

		// Token: 0x040028CA RID: 10442
		private bool replaced;
	}
}
