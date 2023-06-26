using System;
using System.Security;

namespace System.Collections.Generic
{
	// Token: 0x020003CF RID: 975
	internal class BitHelper
	{
		// Token: 0x0600253D RID: 9533 RVA: 0x000AD5F2 File Offset: 0x000AB7F2
		[SecurityCritical]
		internal unsafe BitHelper(int* bitArrayPtr, int length)
		{
			this.m_arrayPtr = bitArrayPtr;
			this.m_length = length;
			this.useStackAlloc = true;
		}

		// Token: 0x0600253E RID: 9534 RVA: 0x000AD60F File Offset: 0x000AB80F
		internal BitHelper(int[] bitArray, int length)
		{
			this.m_array = bitArray;
			this.m_length = length;
		}

		// Token: 0x0600253F RID: 9535 RVA: 0x000AD628 File Offset: 0x000AB828
		[SecurityCritical]
		internal unsafe void MarkBit(int bitPosition)
		{
			if (this.useStackAlloc)
			{
				int num = bitPosition / 32;
				if (num < this.m_length && num >= 0)
				{
					this.m_arrayPtr[num] |= 1 << bitPosition % 32;
					return;
				}
			}
			else
			{
				int num2 = bitPosition / 32;
				if (num2 < this.m_length && num2 >= 0)
				{
					this.m_array[num2] |= 1 << bitPosition % 32;
				}
			}
		}

		// Token: 0x06002540 RID: 9536 RVA: 0x000AD694 File Offset: 0x000AB894
		[SecurityCritical]
		internal unsafe bool IsMarked(int bitPosition)
		{
			if (this.useStackAlloc)
			{
				int num = bitPosition / 32;
				return num < this.m_length && num >= 0 && (this.m_arrayPtr[num] & (1 << bitPosition % 32)) != 0;
			}
			int num2 = bitPosition / 32;
			return num2 < this.m_length && num2 >= 0 && (this.m_array[num2] & (1 << bitPosition % 32)) != 0;
		}

		// Token: 0x06002541 RID: 9537 RVA: 0x000AD700 File Offset: 0x000AB900
		internal static int ToIntArrayLength(int n)
		{
			if (n <= 0)
			{
				return 0;
			}
			return (n - 1) / 32 + 1;
		}

		// Token: 0x04002034 RID: 8244
		private const byte MarkedBitFlag = 1;

		// Token: 0x04002035 RID: 8245
		private const byte IntSize = 32;

		// Token: 0x04002036 RID: 8246
		private int m_length;

		// Token: 0x04002037 RID: 8247
		[SecurityCritical]
		private unsafe int* m_arrayPtr;

		// Token: 0x04002038 RID: 8248
		private int[] m_array;

		// Token: 0x04002039 RID: 8249
		private bool useStackAlloc;
	}
}
