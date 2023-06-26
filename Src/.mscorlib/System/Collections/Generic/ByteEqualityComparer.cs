using System;
using System.Security;

namespace System.Collections.Generic
{
	// Token: 0x020004C2 RID: 1218
	[Serializable]
	internal class ByteEqualityComparer : EqualityComparer<byte>
	{
		// Token: 0x06003AA6 RID: 15014 RVA: 0x000E0C36 File Offset: 0x000DEE36
		public override bool Equals(byte x, byte y)
		{
			return x == y;
		}

		// Token: 0x06003AA7 RID: 15015 RVA: 0x000E0C3C File Offset: 0x000DEE3C
		public override int GetHashCode(byte b)
		{
			return b.GetHashCode();
		}

		// Token: 0x06003AA8 RID: 15016 RVA: 0x000E0C48 File Offset: 0x000DEE48
		[SecuritySafeCritical]
		internal unsafe override int IndexOf(byte[] array, byte value, int startIndex, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
			}
			if (count > array.Length - startIndex)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (count == 0)
			{
				return -1;
			}
			byte* ptr;
			if (array == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			return Buffer.IndexOfByte(ptr, value, startIndex, count);
		}

		// Token: 0x06003AA9 RID: 15017 RVA: 0x000E0CD8 File Offset: 0x000DEED8
		internal override int LastIndexOf(byte[] array, byte value, int startIndex, int count)
		{
			int num = startIndex - count + 1;
			for (int i = startIndex; i >= num; i--)
			{
				if (array[i] == value)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06003AAA RID: 15018 RVA: 0x000E0D04 File Offset: 0x000DEF04
		public override bool Equals(object obj)
		{
			ByteEqualityComparer byteEqualityComparer = obj as ByteEqualityComparer;
			return byteEqualityComparer != null;
		}

		// Token: 0x06003AAB RID: 15019 RVA: 0x000E0D1C File Offset: 0x000DEF1C
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}
