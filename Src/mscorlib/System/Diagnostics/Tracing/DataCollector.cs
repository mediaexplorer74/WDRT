using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200043B RID: 1083
	[SecurityCritical]
	internal struct DataCollector
	{
		// Token: 0x060035FA RID: 13818 RVA: 0x000D3580 File Offset: 0x000D1780
		internal unsafe void Enable(byte* scratch, int scratchSize, EventSource.EventData* datas, int dataCount, GCHandle* pins, int pinCount)
		{
			this.datasStart = datas;
			this.scratchEnd = scratch + scratchSize;
			this.datasEnd = datas + dataCount;
			this.pinsEnd = pins + pinCount;
			this.scratch = scratch;
			this.datas = datas;
			this.pins = pins;
			this.writingScalars = false;
		}

		// Token: 0x060035FB RID: 13819 RVA: 0x000D35DF File Offset: 0x000D17DF
		internal void Disable()
		{
			this = default(DataCollector);
		}

		// Token: 0x060035FC RID: 13820 RVA: 0x000D35E8 File Offset: 0x000D17E8
		internal unsafe EventSource.EventData* Finish()
		{
			this.ScalarsEnd();
			return this.datas;
		}

		// Token: 0x060035FD RID: 13821 RVA: 0x000D35F8 File Offset: 0x000D17F8
		internal unsafe void AddScalar(void* value, int size)
		{
			if (this.bufferNesting != 0)
			{
				int num = this.bufferPos;
				int num2;
				checked
				{
					this.bufferPos += size;
					this.EnsureBuffer();
					num2 = 0;
				}
				while (num2 != size)
				{
					this.buffer[num] = ((byte*)value)[num2];
					num2++;
					num++;
				}
				return;
			}
			byte* ptr = this.scratch;
			byte* ptr2 = ptr + size;
			if (this.scratchEnd < ptr2)
			{
				throw new IndexOutOfRangeException(Environment.GetResourceString("EventSource_AddScalarOutOfRange"));
			}
			this.ScalarsBegin();
			this.scratch = ptr2;
			for (int num3 = 0; num3 != size; num3++)
			{
				ptr[num3] = ((byte*)value)[num3];
			}
		}

		// Token: 0x060035FE RID: 13822 RVA: 0x000D3698 File Offset: 0x000D1898
		internal unsafe void AddBinary(string value, int size)
		{
			if (size > 65535)
			{
				size = 65534;
			}
			if (this.bufferNesting != 0)
			{
				this.EnsureBuffer(size + 2);
			}
			this.AddScalar((void*)(&size), 2);
			if (size != 0)
			{
				if (this.bufferNesting == 0)
				{
					this.ScalarsEnd();
					this.PinArray(value, size);
					return;
				}
				int num = this.bufferPos;
				checked
				{
					this.bufferPos += size;
					this.EnsureBuffer();
				}
				fixed (string text = value)
				{
					void* ptr = text;
					if (ptr != null)
					{
						ptr = (void*)((byte*)ptr + RuntimeHelpers.OffsetToStringData);
					}
					Marshal.Copy((IntPtr)ptr, this.buffer, num, size);
				}
			}
		}

		// Token: 0x060035FF RID: 13823 RVA: 0x000D3729 File Offset: 0x000D1929
		internal void AddBinary(Array value, int size)
		{
			this.AddArray(value, size, 1);
		}

		// Token: 0x06003600 RID: 13824 RVA: 0x000D3734 File Offset: 0x000D1934
		internal unsafe void AddArray(Array value, int length, int itemSize)
		{
			if (length > 65535)
			{
				length = 65535;
			}
			int num = length * itemSize;
			if (this.bufferNesting != 0)
			{
				this.EnsureBuffer(num + 2);
			}
			this.AddScalar((void*)(&length), 2);
			checked
			{
				if (length != 0)
				{
					if (this.bufferNesting == 0)
					{
						this.ScalarsEnd();
						this.PinArray(value, num);
						return;
					}
					int num2 = this.bufferPos;
					this.bufferPos += num;
					this.EnsureBuffer();
					Buffer.BlockCopy(value, 0, this.buffer, num2, num);
				}
			}
		}

		// Token: 0x06003601 RID: 13825 RVA: 0x000D37B3 File Offset: 0x000D19B3
		internal int BeginBufferedArray()
		{
			this.BeginBuffered();
			this.bufferPos += 2;
			return this.bufferPos;
		}

		// Token: 0x06003602 RID: 13826 RVA: 0x000D37CF File Offset: 0x000D19CF
		internal void EndBufferedArray(int bookmark, int count)
		{
			this.EnsureBuffer();
			this.buffer[bookmark - 2] = (byte)count;
			this.buffer[bookmark - 1] = (byte)(count >> 8);
			this.EndBuffered();
		}

		// Token: 0x06003603 RID: 13827 RVA: 0x000D37F7 File Offset: 0x000D19F7
		internal void BeginBuffered()
		{
			this.ScalarsEnd();
			this.bufferNesting++;
		}

		// Token: 0x06003604 RID: 13828 RVA: 0x000D380D File Offset: 0x000D1A0D
		internal void EndBuffered()
		{
			this.bufferNesting--;
			if (this.bufferNesting == 0)
			{
				this.EnsureBuffer();
				this.PinArray(this.buffer, this.bufferPos);
				this.buffer = null;
				this.bufferPos = 0;
			}
		}

		// Token: 0x06003605 RID: 13829 RVA: 0x000D384C File Offset: 0x000D1A4C
		private void EnsureBuffer()
		{
			int num = this.bufferPos;
			if (this.buffer == null || this.buffer.Length < num)
			{
				this.GrowBuffer(num);
			}
		}

		// Token: 0x06003606 RID: 13830 RVA: 0x000D387C File Offset: 0x000D1A7C
		private void EnsureBuffer(int additionalSize)
		{
			int num = this.bufferPos + additionalSize;
			if (this.buffer == null || this.buffer.Length < num)
			{
				this.GrowBuffer(num);
			}
		}

		// Token: 0x06003607 RID: 13831 RVA: 0x000D38AC File Offset: 0x000D1AAC
		private void GrowBuffer(int required)
		{
			int num = ((this.buffer == null) ? 64 : this.buffer.Length);
			do
			{
				num *= 2;
			}
			while (num < required);
			Array.Resize<byte>(ref this.buffer, num);
		}

		// Token: 0x06003608 RID: 13832 RVA: 0x000D38E4 File Offset: 0x000D1AE4
		private unsafe void PinArray(object value, int size)
		{
			GCHandle* ptr = this.pins;
			if (this.pinsEnd == ptr)
			{
				throw new IndexOutOfRangeException(Environment.GetResourceString("EventSource_PinArrayOutOfRange"));
			}
			EventSource.EventData* ptr2 = this.datas;
			if (this.datasEnd == ptr2)
			{
				throw new IndexOutOfRangeException(Environment.GetResourceString("EventSource_DataDescriptorsOutOfRange"));
			}
			this.pins = ptr + 1;
			this.datas = ptr2 + 1;
			*ptr = GCHandle.Alloc(value, GCHandleType.Pinned);
			ptr2->DataPointer = ptr->AddrOfPinnedObject();
			ptr2->m_Size = size;
		}

		// Token: 0x06003609 RID: 13833 RVA: 0x000D3970 File Offset: 0x000D1B70
		private unsafe void ScalarsBegin()
		{
			if (!this.writingScalars)
			{
				EventSource.EventData* ptr = this.datas;
				if (this.datasEnd == ptr)
				{
					throw new IndexOutOfRangeException(Environment.GetResourceString("EventSource_DataDescriptorsOutOfRange"));
				}
				ptr->DataPointer = (IntPtr)((void*)this.scratch);
				this.writingScalars = true;
			}
		}

		// Token: 0x0600360A RID: 13834 RVA: 0x000D39C0 File Offset: 0x000D1BC0
		private unsafe void ScalarsEnd()
		{
			if (this.writingScalars)
			{
				EventSource.EventData* ptr = this.datas;
				ptr->m_Size = (this.scratch - checked((UIntPtr)ptr->m_Ptr)) / 1;
				this.datas = ptr + 1;
				this.writingScalars = false;
			}
		}

		// Token: 0x04001817 RID: 6167
		[ThreadStatic]
		internal static DataCollector ThreadInstance;

		// Token: 0x04001818 RID: 6168
		private unsafe byte* scratchEnd;

		// Token: 0x04001819 RID: 6169
		private unsafe EventSource.EventData* datasEnd;

		// Token: 0x0400181A RID: 6170
		private unsafe GCHandle* pinsEnd;

		// Token: 0x0400181B RID: 6171
		private unsafe EventSource.EventData* datasStart;

		// Token: 0x0400181C RID: 6172
		private unsafe byte* scratch;

		// Token: 0x0400181D RID: 6173
		private unsafe EventSource.EventData* datas;

		// Token: 0x0400181E RID: 6174
		private unsafe GCHandle* pins;

		// Token: 0x0400181F RID: 6175
		private byte[] buffer;

		// Token: 0x04001820 RID: 6176
		private int bufferPos;

		// Token: 0x04001821 RID: 6177
		private int bufferNesting;

		// Token: 0x04001822 RID: 6178
		private bool writingScalars;
	}
}
