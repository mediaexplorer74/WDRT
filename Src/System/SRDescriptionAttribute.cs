using System;
using System.ComponentModel;

namespace System
{
	// Token: 0x02000064 RID: 100
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class SRDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x06000450 RID: 1104 RVA: 0x0001E8D2 File Offset: 0x0001CAD2
		public SRDescriptionAttribute(string description)
			: base(description)
		{
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x0001E8DB File Offset: 0x0001CADB
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

		// Token: 0x04000530 RID: 1328
		private bool replaced;
	}
}
