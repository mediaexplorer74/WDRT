using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x0200012D RID: 301
	internal struct IssuerListInfoEx
	{
		// Token: 0x06000B35 RID: 2869 RVA: 0x0003DA28 File Offset: 0x0003BC28
		public unsafe IssuerListInfoEx(SafeHandle handle, byte[] nativeBuffer)
		{
			this.aIssuers = handle;
			fixed (byte[] array = nativeBuffer)
			{
				byte* ptr;
				if (nativeBuffer == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				this.cIssuers = *(uint*)(ptr + IntPtr.Size);
			}
		}

		// Token: 0x0400100C RID: 4108
		public SafeHandle aIssuers;

		// Token: 0x0400100D RID: 4109
		public uint cIssuers;
	}
}
