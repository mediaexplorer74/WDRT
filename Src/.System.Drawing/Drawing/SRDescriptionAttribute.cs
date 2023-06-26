using System;
using System.ComponentModel;

namespace System.Drawing
{
	// Token: 0x0200004F RID: 79
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class SRDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x060006F5 RID: 1781 RVA: 0x0001C3FC File Offset: 0x0001A5FC
		public SRDescriptionAttribute(string description)
			: base(description)
		{
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x0001C405 File Offset: 0x0001A605
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

		// Token: 0x040005A8 RID: 1448
		private bool replaced;
	}
}
