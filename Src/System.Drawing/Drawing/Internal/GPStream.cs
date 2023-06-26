using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Drawing.Internal
{
	// Token: 0x020000E9 RID: 233
	internal class GPStream : UnsafeNativeMethods.IStream
	{
		// Token: 0x06000C3E RID: 3134 RVA: 0x0002B6C8 File Offset: 0x000298C8
		internal GPStream(Stream stream)
		{
			if (!stream.CanSeek)
			{
				byte[] array = new byte[256];
				int num = 0;
				int num2;
				do
				{
					if (array.Length < num + 256)
					{
						byte[] array2 = new byte[array.Length * 2];
						Array.Copy(array, array2, array.Length);
						array = array2;
					}
					num2 = stream.Read(array, num, 256);
					num += num2;
				}
				while (num2 != 0);
				this.dataStream = new MemoryStream(array);
				return;
			}
			this.dataStream = stream;
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x0002B744 File Offset: 0x00029944
		private void ActualizeVirtualPosition()
		{
			if (this.virtualPosition == -1L)
			{
				return;
			}
			if (this.virtualPosition > this.dataStream.Length)
			{
				this.dataStream.SetLength(this.virtualPosition);
			}
			this.dataStream.Position = this.virtualPosition;
			this.virtualPosition = -1L;
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x0002B799 File Offset: 0x00029999
		public virtual UnsafeNativeMethods.IStream Clone()
		{
			GPStream.NotImplemented();
			return null;
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x0002B7A1 File Offset: 0x000299A1
		public virtual void Commit(int grfCommitFlags)
		{
			this.dataStream.Flush();
			this.ActualizeVirtualPosition();
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x0002B7B4 File Offset: 0x000299B4
		[UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.AllWindows)]
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public virtual long CopyTo(UnsafeNativeMethods.IStream pstm, long cb, long[] pcbRead)
		{
			int num = 4096;
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			if (intPtr == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			long num2 = 0L;
			try
			{
				while (num2 < cb)
				{
					int num3 = num;
					if (num2 + (long)num3 > cb)
					{
						num3 = (int)(cb - num2);
					}
					int num4 = this.Read(intPtr, num3);
					if (num4 == 0)
					{
						break;
					}
					if (pstm.Write(intPtr, num4) != num4)
					{
						throw GPStream.EFail("Wrote an incorrect number of bytes");
					}
					num2 += (long)num4;
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			if (pcbRead != null && pcbRead.Length != 0)
			{
				pcbRead[0] = num2;
			}
			return num2;
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x0002B84C File Offset: 0x00029A4C
		public virtual Stream GetDataStream()
		{
			return this.dataStream;
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x00015259 File Offset: 0x00013459
		public virtual void LockRegion(long libOffset, long cb, int dwLockType)
		{
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x0002B854 File Offset: 0x00029A54
		protected static ExternalException EFail(string msg)
		{
			throw new ExternalException(msg, -2147467259);
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x0002B861 File Offset: 0x00029A61
		protected static void NotImplemented()
		{
			throw new ExternalException(SR.GetString("NotImplemented"), -2147467263);
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x0002B878 File Offset: 0x00029A78
		public virtual int Read(IntPtr buf, int length)
		{
			byte[] array = new byte[length];
			int num = this.Read(array, length);
			Marshal.Copy(array, 0, buf, length);
			return num;
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x0002B89F File Offset: 0x00029A9F
		public virtual int Read(byte[] buffer, int length)
		{
			this.ActualizeVirtualPosition();
			return this.dataStream.Read(buffer, 0, length);
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x0002B8B5 File Offset: 0x00029AB5
		public virtual void Revert()
		{
			GPStream.NotImplemented();
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x0002B8BC File Offset: 0x00029ABC
		public virtual long Seek(long offset, int origin)
		{
			long position = this.virtualPosition;
			if (this.virtualPosition == -1L)
			{
				position = this.dataStream.Position;
			}
			long length = this.dataStream.Length;
			switch (origin)
			{
			case 0:
				if (offset <= length)
				{
					this.dataStream.Position = offset;
					this.virtualPosition = -1L;
				}
				else
				{
					this.virtualPosition = offset;
				}
				break;
			case 1:
				if (offset + position <= length)
				{
					this.dataStream.Position = position + offset;
					this.virtualPosition = -1L;
				}
				else
				{
					this.virtualPosition = offset + position;
				}
				break;
			case 2:
				if (offset <= 0L)
				{
					this.dataStream.Position = length + offset;
					this.virtualPosition = -1L;
				}
				else
				{
					this.virtualPosition = length + offset;
				}
				break;
			}
			if (this.virtualPosition != -1L)
			{
				return this.virtualPosition;
			}
			return this.dataStream.Position;
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x0002B994 File Offset: 0x00029B94
		public virtual void SetSize(long value)
		{
			this.dataStream.SetLength(value);
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x0002B9A4 File Offset: 0x00029BA4
		public void Stat(IntPtr pstatstg, int grfStatFlag)
		{
			Marshal.StructureToPtr(new GPStream.STATSTG
			{
				cbSize = this.dataStream.Length
			}, pstatstg, true);
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x00015259 File Offset: 0x00013459
		public virtual void UnlockRegion(long libOffset, long cb, int dwLockType)
		{
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x0002B9D0 File Offset: 0x00029BD0
		public virtual int Write(IntPtr buf, int length)
		{
			byte[] array = new byte[length];
			Marshal.Copy(buf, array, 0, length);
			return this.Write(array, length);
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x0002B9F5 File Offset: 0x00029BF5
		public virtual int Write(byte[] buffer, int length)
		{
			this.ActualizeVirtualPosition();
			this.dataStream.Write(buffer, 0, length);
			return length;
		}

		// Token: 0x04000AC8 RID: 2760
		protected Stream dataStream;

		// Token: 0x04000AC9 RID: 2761
		private long virtualPosition = -1L;

		// Token: 0x02000130 RID: 304
		[StructLayout(LayoutKind.Sequential)]
		public class STATSTG
		{
			// Token: 0x04000CC4 RID: 3268
			public IntPtr pwcsName = IntPtr.Zero;

			// Token: 0x04000CC5 RID: 3269
			public int type;

			// Token: 0x04000CC6 RID: 3270
			[MarshalAs(UnmanagedType.I8)]
			public long cbSize;

			// Token: 0x04000CC7 RID: 3271
			[MarshalAs(UnmanagedType.I8)]
			public long mtime;

			// Token: 0x04000CC8 RID: 3272
			[MarshalAs(UnmanagedType.I8)]
			public long ctime;

			// Token: 0x04000CC9 RID: 3273
			[MarshalAs(UnmanagedType.I8)]
			public long atime;

			// Token: 0x04000CCA RID: 3274
			[MarshalAs(UnmanagedType.I4)]
			public int grfMode;

			// Token: 0x04000CCB RID: 3275
			[MarshalAs(UnmanagedType.I4)]
			public int grfLocksSupported;

			// Token: 0x04000CCC RID: 3276
			public int clsid_data1;

			// Token: 0x04000CCD RID: 3277
			[MarshalAs(UnmanagedType.I2)]
			public short clsid_data2;

			// Token: 0x04000CCE RID: 3278
			[MarshalAs(UnmanagedType.I2)]
			public short clsid_data3;

			// Token: 0x04000CCF RID: 3279
			[MarshalAs(UnmanagedType.U1)]
			public byte clsid_b0;

			// Token: 0x04000CD0 RID: 3280
			[MarshalAs(UnmanagedType.U1)]
			public byte clsid_b1;

			// Token: 0x04000CD1 RID: 3281
			[MarshalAs(UnmanagedType.U1)]
			public byte clsid_b2;

			// Token: 0x04000CD2 RID: 3282
			[MarshalAs(UnmanagedType.U1)]
			public byte clsid_b3;

			// Token: 0x04000CD3 RID: 3283
			[MarshalAs(UnmanagedType.U1)]
			public byte clsid_b4;

			// Token: 0x04000CD4 RID: 3284
			[MarshalAs(UnmanagedType.U1)]
			public byte clsid_b5;

			// Token: 0x04000CD5 RID: 3285
			[MarshalAs(UnmanagedType.U1)]
			public byte clsid_b6;

			// Token: 0x04000CD6 RID: 3286
			[MarshalAs(UnmanagedType.U1)]
			public byte clsid_b7;

			// Token: 0x04000CD7 RID: 3287
			[MarshalAs(UnmanagedType.I4)]
			public int grfStateBits;

			// Token: 0x04000CD8 RID: 3288
			[MarshalAs(UnmanagedType.I4)]
			public int reserved;
		}
	}
}
