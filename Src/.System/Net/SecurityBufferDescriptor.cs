using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000133 RID: 307
	[StructLayout(LayoutKind.Sequential)]
	internal class SecurityBufferDescriptor
	{
		// Token: 0x06000B3F RID: 2879 RVA: 0x0003DD0E File Offset: 0x0003BF0E
		public SecurityBufferDescriptor(int count)
		{
			this.Version = 0;
			this.Count = count;
			this.UnmanagedPointer = null;
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x0003DD2C File Offset: 0x0003BF2C
		[Conditional("TRAVE")]
		internal void DebugDump()
		{
		}

		// Token: 0x04001038 RID: 4152
		public readonly int Version;

		// Token: 0x04001039 RID: 4153
		public readonly int Count;

		// Token: 0x0400103A RID: 4154
		public unsafe void* UnmanagedPointer;
	}
}
