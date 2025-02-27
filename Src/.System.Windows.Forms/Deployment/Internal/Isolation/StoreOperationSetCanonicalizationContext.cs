﻿using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000058 RID: 88
	internal struct StoreOperationSetCanonicalizationContext
	{
		// Token: 0x06000198 RID: 408 RVA: 0x00007573 File Offset: 0x00005773
		[SecurityCritical]
		public StoreOperationSetCanonicalizationContext(string Bases, string Exports)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationSetCanonicalizationContext));
			this.Flags = StoreOperationSetCanonicalizationContext.OpFlags.Nothing;
			this.BaseAddressFilePath = Bases;
			this.ExportsFilePath = Exports;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000070A6 File Offset: 0x000052A6
		public void Destroy()
		{
		}

		// Token: 0x04000178 RID: 376
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x04000179 RID: 377
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationSetCanonicalizationContext.OpFlags Flags;

		// Token: 0x0400017A RID: 378
		[MarshalAs(UnmanagedType.LPWStr)]
		public string BaseAddressFilePath;

		// Token: 0x0400017B RID: 379
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ExportsFilePath;

		// Token: 0x0200052E RID: 1326
		[Flags]
		public enum OpFlags
		{
			// Token: 0x040037BE RID: 14270
			Nothing = 0
		}
	}
}
