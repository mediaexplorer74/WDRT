using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x0200020F RID: 527
	[StructLayout(LayoutKind.Sequential)]
	internal class StreamSizes
	{
		// Token: 0x060013B0 RID: 5040 RVA: 0x00068270 File Offset: 0x00066470
		internal unsafe StreamSizes(byte[] memory)
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
						this.header = (int)((uint)Marshal.ReadInt32(intPtr));
						this.trailer = (int)((uint)Marshal.ReadInt32(intPtr, 4));
						this.maximumMessage = (int)((uint)Marshal.ReadInt32(intPtr, 8));
						this.buffersCount = (int)((uint)Marshal.ReadInt32(intPtr, 12));
						this.blockSize = (int)((uint)Marshal.ReadInt32(intPtr, 16));
					}
					catch (OverflowException)
					{
						throw;
					}
				}
			}
		}

		// Token: 0x0400155B RID: 5467
		public int header;

		// Token: 0x0400155C RID: 5468
		public int trailer;

		// Token: 0x0400155D RID: 5469
		public int maximumMessage;

		// Token: 0x0400155E RID: 5470
		public int buffersCount;

		// Token: 0x0400155F RID: 5471
		public int blockSize;

		// Token: 0x04001560 RID: 5472
		public static readonly int SizeOf = Marshal.SizeOf(typeof(StreamSizes));
	}
}
