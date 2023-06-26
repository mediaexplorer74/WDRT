using System;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x02000496 RID: 1174
	internal abstract class Com2ExtendedBrowsingHandler
	{
		// Token: 0x17001349 RID: 4937
		// (get) Token: 0x06004E75 RID: 20085
		public abstract Type Interface { get; }

		// Token: 0x06004E76 RID: 20086 RVA: 0x00142FC1 File Offset: 0x001411C1
		public virtual void SetupPropertyHandlers(Com2PropertyDescriptor propDesc)
		{
			this.SetupPropertyHandlers(new Com2PropertyDescriptor[] { propDesc });
		}

		// Token: 0x06004E77 RID: 20087
		public abstract void SetupPropertyHandlers(Com2PropertyDescriptor[] propDesc);
	}
}
