using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x020006A7 RID: 1703
	internal struct StoreOperationSetCanonicalizationContext
	{
		// Token: 0x06004FF4 RID: 20468 RVA: 0x0011E416 File Offset: 0x0011C616
		[SecurityCritical]
		public StoreOperationSetCanonicalizationContext(string Bases, string Exports)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationSetCanonicalizationContext));
			this.Flags = StoreOperationSetCanonicalizationContext.OpFlags.Nothing;
			this.BaseAddressFilePath = Bases;
			this.ExportsFilePath = Exports;
		}

		// Token: 0x06004FF5 RID: 20469 RVA: 0x0011E442 File Offset: 0x0011C642
		public void Destroy()
		{
		}

		// Token: 0x04002258 RID: 8792
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x04002259 RID: 8793
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationSetCanonicalizationContext.OpFlags Flags;

		// Token: 0x0400225A RID: 8794
		[MarshalAs(UnmanagedType.LPWStr)]
		public string BaseAddressFilePath;

		// Token: 0x0400225B RID: 8795
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ExportsFilePath;

		// Token: 0x02000C4D RID: 3149
		[Flags]
		public enum OpFlags
		{
			// Token: 0x04003788 RID: 14216
			Nothing = 0
		}
	}
}
