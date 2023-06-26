using System;
using System.IO;

namespace System.Windows.Forms
{
	// Token: 0x02000224 RID: 548
	internal class DataStreamFromComStream : Stream
	{
		// Token: 0x060023B5 RID: 9141 RVA: 0x000AA7AE File Offset: 0x000A89AE
		public DataStreamFromComStream(UnsafeNativeMethods.IStream comStream)
		{
			this.comStream = comStream;
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x060023B6 RID: 9142 RVA: 0x000AA7BD File Offset: 0x000A89BD
		// (set) Token: 0x060023B7 RID: 9143 RVA: 0x000AA7C8 File Offset: 0x000A89C8
		public override long Position
		{
			get
			{
				return this.Seek(0L, SeekOrigin.Current);
			}
			set
			{
				this.Seek(value, SeekOrigin.Begin);
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x060023B8 RID: 9144 RVA: 0x00012E4E File Offset: 0x0001104E
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x060023B9 RID: 9145 RVA: 0x00012E4E File Offset: 0x0001104E
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x060023BA RID: 9146 RVA: 0x00012E4E File Offset: 0x0001104E
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x060023BB RID: 9147 RVA: 0x000AA7D4 File Offset: 0x000A89D4
		public override long Length
		{
			get
			{
				long position = this.Position;
				long num = this.Seek(0L, SeekOrigin.End);
				this.Position = position;
				return num - position;
			}
		}

		// Token: 0x060023BC RID: 9148 RVA: 0x000AA7FC File Offset: 0x000A89FC
		private unsafe int _Read(void* handle, int bytes)
		{
			return this.comStream.Read((IntPtr)handle, bytes);
		}

		// Token: 0x060023BD RID: 9149 RVA: 0x000AA810 File Offset: 0x000A8A10
		private unsafe int _Write(void* handle, int bytes)
		{
			return this.comStream.Write((IntPtr)handle, bytes);
		}

		// Token: 0x060023BE RID: 9150 RVA: 0x000070A6 File Offset: 0x000052A6
		public override void Flush()
		{
		}

		// Token: 0x060023BF RID: 9151 RVA: 0x000AA824 File Offset: 0x000A8A24
		public unsafe override int Read(byte[] buffer, int index, int count)
		{
			int num = 0;
			if (count > 0 && index >= 0 && count + index <= buffer.Length)
			{
				fixed (byte[] array = buffer)
				{
					byte* ptr;
					if (buffer == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
					num = this._Read((void*)(ptr + index), count);
				}
			}
			return num;
		}

		// Token: 0x060023C0 RID: 9152 RVA: 0x000AA869 File Offset: 0x000A8A69
		public override void SetLength(long value)
		{
			this.comStream.SetSize(value);
		}

		// Token: 0x060023C1 RID: 9153 RVA: 0x000AA877 File Offset: 0x000A8A77
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.comStream.Seek(offset, (int)origin);
		}

		// Token: 0x060023C2 RID: 9154 RVA: 0x000AA888 File Offset: 0x000A8A88
		public unsafe override void Write(byte[] buffer, int index, int count)
		{
			int num = 0;
			if (count > 0 && index >= 0 && count + index <= buffer.Length)
			{
				try
				{
					try
					{
						fixed (byte[] array = buffer)
						{
							byte* ptr;
							if (buffer == null || array.Length == 0)
							{
								ptr = null;
							}
							else
							{
								ptr = &array[0];
							}
							num = this._Write((void*)(ptr + index), count);
						}
					}
					finally
					{
						byte[] array = null;
					}
				}
				catch
				{
				}
			}
			if (num < count)
			{
				throw new IOException(SR.GetString("DataStreamWrite"));
			}
		}

		// Token: 0x060023C3 RID: 9155 RVA: 0x000AA904 File Offset: 0x000A8B04
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this.comStream != null)
				{
					try
					{
						this.comStream.Commit(0);
					}
					catch (Exception)
					{
					}
				}
				this.comStream = null;
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060023C4 RID: 9156 RVA: 0x000AA95C File Offset: 0x000A8B5C
		~DataStreamFromComStream()
		{
			this.Dispose(false);
		}

		// Token: 0x04000EA8 RID: 3752
		private UnsafeNativeMethods.IStream comStream;
	}
}
