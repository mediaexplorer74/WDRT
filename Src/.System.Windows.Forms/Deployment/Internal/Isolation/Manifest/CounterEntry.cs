﻿using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000E2 RID: 226
	[StructLayout(LayoutKind.Sequential)]
	internal class CounterEntry
	{
		// Token: 0x04000393 RID: 915
		public Guid CounterSetGuid;

		// Token: 0x04000394 RID: 916
		public uint CounterId;

		// Token: 0x04000395 RID: 917
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Name;

		// Token: 0x04000396 RID: 918
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Description;

		// Token: 0x04000397 RID: 919
		public uint CounterType;

		// Token: 0x04000398 RID: 920
		public ulong Attributes;

		// Token: 0x04000399 RID: 921
		public uint BaseId;

		// Token: 0x0400039A RID: 922
		public uint DefaultScale;
	}
}
