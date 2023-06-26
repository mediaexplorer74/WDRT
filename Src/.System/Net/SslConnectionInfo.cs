using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000213 RID: 531
	[StructLayout(LayoutKind.Sequential)]
	internal class SslConnectionInfo
	{
		// Token: 0x060013B4 RID: 5044 RVA: 0x000683B8 File Offset: 0x000665B8
		internal unsafe SslConnectionInfo(byte[] nativeBuffer)
		{
			fixed (byte[] array = nativeBuffer)
			{
				void* ptr;
				if (nativeBuffer == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = (void*)(&array[0]);
				}
				IntPtr intPtr = new IntPtr(ptr);
				this.Protocol = Marshal.ReadInt32(intPtr);
				this.DataCipherAlg = Marshal.ReadInt32(intPtr, 4);
				this.DataKeySize = Marshal.ReadInt32(intPtr, 8);
				this.DataHashAlg = Marshal.ReadInt32(intPtr, 12);
				this.DataHashKeySize = Marshal.ReadInt32(intPtr, 16);
				this.KeyExchangeAlg = Marshal.ReadInt32(intPtr, 20);
				this.KeyExchKeySize = Marshal.ReadInt32(intPtr, 24);
			}
		}

		// Token: 0x0400159B RID: 5531
		public readonly int Protocol;

		// Token: 0x0400159C RID: 5532
		public readonly int DataCipherAlg;

		// Token: 0x0400159D RID: 5533
		public readonly int DataKeySize;

		// Token: 0x0400159E RID: 5534
		public readonly int DataHashAlg;

		// Token: 0x0400159F RID: 5535
		public readonly int DataHashKeySize;

		// Token: 0x040015A0 RID: 5536
		public readonly int KeyExchangeAlg;

		// Token: 0x040015A1 RID: 5537
		public readonly int KeyExchKeySize;
	}
}
