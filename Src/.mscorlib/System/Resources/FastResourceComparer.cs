using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;

namespace System.Resources
{
	// Token: 0x02000388 RID: 904
	internal sealed class FastResourceComparer : IComparer, IEqualityComparer, IComparer<string>, IEqualityComparer<string>
	{
		// Token: 0x06002CF3 RID: 11507 RVA: 0x000AA988 File Offset: 0x000A8B88
		public int GetHashCode(object key)
		{
			string text = (string)key;
			return FastResourceComparer.HashFunction(text);
		}

		// Token: 0x06002CF4 RID: 11508 RVA: 0x000AA9A2 File Offset: 0x000A8BA2
		public int GetHashCode(string key)
		{
			return FastResourceComparer.HashFunction(key);
		}

		// Token: 0x06002CF5 RID: 11509 RVA: 0x000AA9AC File Offset: 0x000A8BAC
		internal static int HashFunction(string key)
		{
			uint num = 5381U;
			for (int i = 0; i < key.Length; i++)
			{
				num = ((num << 5) + num) ^ (uint)key[i];
			}
			return (int)num;
		}

		// Token: 0x06002CF6 RID: 11510 RVA: 0x000AA9E0 File Offset: 0x000A8BE0
		public int Compare(object a, object b)
		{
			if (a == b)
			{
				return 0;
			}
			string text = (string)a;
			string text2 = (string)b;
			return string.CompareOrdinal(text, text2);
		}

		// Token: 0x06002CF7 RID: 11511 RVA: 0x000AAA08 File Offset: 0x000A8C08
		public int Compare(string a, string b)
		{
			return string.CompareOrdinal(a, b);
		}

		// Token: 0x06002CF8 RID: 11512 RVA: 0x000AAA11 File Offset: 0x000A8C11
		public bool Equals(string a, string b)
		{
			return string.Equals(a, b);
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x000AAA1C File Offset: 0x000A8C1C
		public bool Equals(object a, object b)
		{
			if (a == b)
			{
				return true;
			}
			string text = (string)a;
			string text2 = (string)b;
			return string.Equals(text, text2);
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x000AAA44 File Offset: 0x000A8C44
		[SecurityCritical]
		public unsafe static int CompareOrdinal(string a, byte[] bytes, int bCharLength)
		{
			int num = 0;
			int num2 = 0;
			int num3 = a.Length;
			if (num3 > bCharLength)
			{
				num3 = bCharLength;
			}
			if (bCharLength == 0)
			{
				if (a.Length != 0)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				fixed (byte[] array = bytes)
				{
					byte* ptr;
					if (bytes == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
					byte* ptr2 = ptr;
					while (num < num3 && num2 == 0)
					{
						int num4 = (int)(*ptr2) | ((int)ptr2[1] << 8);
						num2 = (int)a[num++] - num4;
						ptr2 += 2;
					}
				}
				if (num2 != 0)
				{
					return num2;
				}
				return a.Length - bCharLength;
			}
		}

		// Token: 0x06002CFB RID: 11515 RVA: 0x000AAACA File Offset: 0x000A8CCA
		[SecurityCritical]
		public static int CompareOrdinal(byte[] bytes, int aCharLength, string b)
		{
			return -FastResourceComparer.CompareOrdinal(b, bytes, aCharLength);
		}

		// Token: 0x06002CFC RID: 11516 RVA: 0x000AAAD8 File Offset: 0x000A8CD8
		[SecurityCritical]
		internal unsafe static int CompareOrdinal(byte* a, int byteLen, string b)
		{
			int num = 0;
			int num2 = 0;
			int num3 = byteLen >> 1;
			if (num3 > b.Length)
			{
				num3 = b.Length;
			}
			while (num2 < num3 && num == 0)
			{
				char c = (char)((int)(*(a++)) | ((int)(*(a++)) << 8));
				num = (int)(c - b[num2++]);
			}
			if (num != 0)
			{
				return num;
			}
			return byteLen - b.Length * 2;
		}

		// Token: 0x04001231 RID: 4657
		internal static readonly FastResourceComparer Default = new FastResourceComparer();
	}
}
