using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000210 RID: 528
	[StructLayout(LayoutKind.Sequential)]
	internal class SecSizes
	{
		// Token: 0x060013B2 RID: 5042 RVA: 0x0006831C File Offset: 0x0006651C
		internal unsafe SecSizes(byte[] memory)
		{
			fixed (byte[] array = memory)
			{
				void* ptr;
				if (memory == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = (void*)(&array[0]);
				}
				IntPtr intPtr = new IntPtr(ptr);
				checked
				{
					try
					{
						this.MaxToken = (int)((uint)Marshal.ReadInt32(intPtr));
						this.MaxSignature = (int)((uint)Marshal.ReadInt32(intPtr, 4));
						this.BlockSize = (int)((uint)Marshal.ReadInt32(intPtr, 8));
						this.SecurityTrailer = (int)((uint)Marshal.ReadInt32(intPtr, 12));
					}
					catch (OverflowException)
					{
						throw;
					}
				}
			}
		}

		// Token: 0x04001561 RID: 5473
		public readonly int MaxToken;

		// Token: 0x04001562 RID: 5474
		public readonly int MaxSignature;

		// Token: 0x04001563 RID: 5475
		public readonly int BlockSize;

		// Token: 0x04001564 RID: 5476
		public readonly int SecurityTrailer;

		// Token: 0x04001565 RID: 5477
		public static readonly int SizeOf = Marshal.SizeOf(typeof(SecSizes));
	}
}
