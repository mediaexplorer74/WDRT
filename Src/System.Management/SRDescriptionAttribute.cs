using System;
using System.ComponentModel;

namespace System.Management
{
	// Token: 0x020000A8 RID: 168
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class SRDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x0600047A RID: 1146 RVA: 0x00021EBC File Offset: 0x000200BC
		public SRDescriptionAttribute(string description)
			: base(description)
		{
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x00021EC5 File Offset: 0x000200C5
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

		// Token: 0x04000498 RID: 1176
		private bool replaced;
	}
}
