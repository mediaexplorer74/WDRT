using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000958 RID: 2392
	internal class StringBuffer : NativeBuffer
	{
		// Token: 0x06006207 RID: 25095 RVA: 0x00150730 File Offset: 0x0014E930
		public StringBuffer(uint initialCapacity = 0U)
			: base((ulong)initialCapacity * 2UL)
		{
		}

		// Token: 0x06006208 RID: 25096 RVA: 0x0015073D File Offset: 0x0014E93D
		public StringBuffer(string initialContents)
			: base(0UL)
		{
			if (initialContents != null)
			{
				this.Append(initialContents, 0, -1);
			}
		}

		// Token: 0x06006209 RID: 25097 RVA: 0x00150753 File Offset: 0x0014E953
		public StringBuffer(StringBuffer initialContents)
			: base(0UL)
		{
			if (initialContents != null)
			{
				this.Append(initialContents, 0U);
			}
		}

		// Token: 0x17001108 RID: 4360
		public unsafe char this[uint index]
		{
			[SecuritySafeCritical]
			get
			{
				if (index >= this._length)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.CharPointer[(ulong)index * 2UL / 2UL];
			}
			[SecuritySafeCritical]
			set
			{
				if (index >= this._length)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this.CharPointer[(ulong)index * 2UL / 2UL] = value;
			}
		}

		// Token: 0x17001109 RID: 4361
		// (get) Token: 0x0600620C RID: 25100 RVA: 0x001507B4 File Offset: 0x0014E9B4
		public uint CharCapacity
		{
			[SecuritySafeCritical]
			get
			{
				ulong byteCapacity = base.ByteCapacity;
				ulong num = ((byteCapacity == 0UL) ? 0UL : (byteCapacity / 2UL));
				if (num <= (ulong)(-1))
				{
					return (uint)num;
				}
				return uint.MaxValue;
			}
		}

		// Token: 0x0600620D RID: 25101 RVA: 0x001507DD File Offset: 0x0014E9DD
		[SecuritySafeCritical]
		public void EnsureCharCapacity(uint minCapacity)
		{
			base.EnsureByteCapacity((ulong)minCapacity * 2UL);
		}

		// Token: 0x1700110A RID: 4362
		// (get) Token: 0x0600620E RID: 25102 RVA: 0x001507EA File Offset: 0x0014E9EA
		// (set) Token: 0x0600620F RID: 25103 RVA: 0x001507F2 File Offset: 0x0014E9F2
		public unsafe uint Length
		{
			get
			{
				return this._length;
			}
			[SecuritySafeCritical]
			set
			{
				if (value == 4294967295U)
				{
					throw new ArgumentOutOfRangeException("Length");
				}
				this.EnsureCharCapacity(value + 1U);
				this.CharPointer[(ulong)value * 2UL / 2UL] = '\0';
				this._length = value;
			}
		}

		// Token: 0x06006210 RID: 25104 RVA: 0x00150824 File Offset: 0x0014EA24
		[SecuritySafeCritical]
		public unsafe void SetLengthToFirstNull()
		{
			char* charPointer = this.CharPointer;
			uint charCapacity = this.CharCapacity;
			for (uint num = 0U; num < charCapacity; num += 1U)
			{
				if (charPointer[(ulong)num * 2UL / 2UL] == '\0')
				{
					this._length = num;
					return;
				}
			}
		}

		// Token: 0x1700110B RID: 4363
		// (get) Token: 0x06006211 RID: 25105 RVA: 0x0015085E File Offset: 0x0014EA5E
		internal unsafe char* CharPointer
		{
			[SecurityCritical]
			get
			{
				return (char*)base.VoidPointer;
			}
		}

		// Token: 0x06006212 RID: 25106 RVA: 0x00150868 File Offset: 0x0014EA68
		[SecurityCritical]
		public unsafe bool Contains(char value)
		{
			char* charPointer = this.CharPointer;
			uint length = this._length;
			for (uint num = 0U; num < length; num += 1U)
			{
				if (*(charPointer++) == value)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006213 RID: 25107 RVA: 0x0015089B File Offset: 0x0014EA9B
		[SecuritySafeCritical]
		public bool StartsWith(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return this._length >= (uint)value.Length && this.SubstringEquals(value, 0U, value.Length);
		}

		// Token: 0x06006214 RID: 25108 RVA: 0x001508CC File Offset: 0x0014EACC
		[SecuritySafeCritical]
		public unsafe bool SubstringEquals(string value, uint startIndex = 0U, int count = -1)
		{
			if (value == null)
			{
				return false;
			}
			if (count < -1)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (startIndex > this._length)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			uint num = ((count == -1) ? (this._length - startIndex) : ((uint)count));
			if (checked(startIndex + num) > this._length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			int length = value.Length;
			if (num != (uint)length)
			{
				return false;
			}
			fixed (string text = value)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				char* ptr2 = this.CharPointer + (ulong)startIndex * 2UL / 2UL;
				for (int i = 0; i < length; i++)
				{
					if (*(ptr2++) != ptr[i])
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06006215 RID: 25109 RVA: 0x0015097A File Offset: 0x0014EB7A
		[SecuritySafeCritical]
		public void Append(string value, int startIndex = 0, int count = -1)
		{
			this.CopyFrom(this._length, value, startIndex, count);
		}

		// Token: 0x06006216 RID: 25110 RVA: 0x0015098B File Offset: 0x0014EB8B
		public void Append(StringBuffer value, uint startIndex = 0U)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.Length == 0U)
			{
				return;
			}
			value.CopyTo(startIndex, this, this._length, value.Length);
		}

		// Token: 0x06006217 RID: 25111 RVA: 0x001509B8 File Offset: 0x0014EBB8
		public void Append(StringBuffer value, uint startIndex, uint count)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (count == 0U)
			{
				return;
			}
			value.CopyTo(startIndex, this, this._length, count);
		}

		// Token: 0x06006218 RID: 25112 RVA: 0x001509DC File Offset: 0x0014EBDC
		[SecuritySafeCritical]
		public unsafe void CopyTo(uint bufferIndex, StringBuffer destination, uint destinationIndex, uint count)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (destinationIndex > destination._length)
			{
				throw new ArgumentOutOfRangeException("destinationIndex");
			}
			if (bufferIndex >= this._length)
			{
				throw new ArgumentOutOfRangeException("bufferIndex");
			}
			checked
			{
				if (this._length < bufferIndex + count)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				if (count == 0U)
				{
					return;
				}
				uint num = destinationIndex + count;
				if (destination._length < num)
				{
					destination.Length = num;
				}
				Buffer.MemoryCopy((void*)(unchecked(this.CharPointer + (ulong)bufferIndex * 2UL / 2UL)), (void*)(unchecked(destination.CharPointer + (ulong)destinationIndex * 2UL / 2UL)), (long)(destination.ByteCapacity - unchecked((ulong)(checked(destinationIndex * 2U)))), (long)(unchecked((ulong)count) * 2UL));
			}
		}

		// Token: 0x06006219 RID: 25113 RVA: 0x00150A84 File Offset: 0x0014EC84
		[SecuritySafeCritical]
		public unsafe void CopyFrom(uint bufferIndex, string source, int sourceIndex = 0, int count = -1)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (bufferIndex > this._length)
			{
				throw new ArgumentOutOfRangeException("bufferIndex");
			}
			if (sourceIndex < 0 || sourceIndex >= source.Length)
			{
				throw new ArgumentOutOfRangeException("sourceIndex");
			}
			if (count == -1)
			{
				count = source.Length - sourceIndex;
			}
			if (count < 0 || source.Length - count < sourceIndex)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (count == 0)
			{
				return;
			}
			uint num = bufferIndex + (uint)count;
			if (this._length < num)
			{
				this.Length = num;
			}
			fixed (string text = source)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				Buffer.MemoryCopy((void*)(ptr + sourceIndex), (void*)(this.CharPointer + (ulong)bufferIndex * 2UL / 2UL), checked((long)(base.ByteCapacity - unchecked((ulong)(checked(bufferIndex * 2U))))), (long)count * 2L);
			}
		}

		// Token: 0x0600621A RID: 25114 RVA: 0x00150B4C File Offset: 0x0014ED4C
		[SecuritySafeCritical]
		public unsafe void TrimEnd(char[] values)
		{
			if (values == null || values.Length == 0 || this._length == 0U)
			{
				return;
			}
			char* ptr = this.CharPointer + (ulong)this._length * 2UL / 2UL - 1;
			while (this._length > 0U && Array.IndexOf<char>(values, *ptr) >= 0)
			{
				this.Length = this._length - 1U;
				ptr--;
			}
		}

		// Token: 0x0600621B RID: 25115 RVA: 0x00150BA6 File Offset: 0x0014EDA6
		[SecuritySafeCritical]
		public override string ToString()
		{
			if (this._length == 0U)
			{
				return string.Empty;
			}
			if (this._length > 2147483647U)
			{
				throw new InvalidOperationException();
			}
			return new string(this.CharPointer, 0, (int)this._length);
		}

		// Token: 0x0600621C RID: 25116 RVA: 0x00150BDC File Offset: 0x0014EDDC
		[SecuritySafeCritical]
		public string Substring(uint startIndex, int count = -1)
		{
			if (startIndex > ((this._length == 0U) ? 0U : (this._length - 1U)))
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			if (count < -1)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			uint num = ((count == -1) ? (this._length - startIndex) : ((uint)count));
			if (num > 2147483647U || checked(startIndex + num) > this._length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (num == 0U)
			{
				return string.Empty;
			}
			return new string(this.CharPointer + (ulong)startIndex * 2UL / 2UL, 0, (int)num);
		}

		// Token: 0x0600621D RID: 25117 RVA: 0x00150C64 File Offset: 0x0014EE64
		[SecuritySafeCritical]
		public override void Free()
		{
			base.Free();
			this._length = 0U;
		}

		// Token: 0x04002B8E RID: 11150
		private uint _length;
	}
}
