using System;
using System.Collections;

namespace System.Net
{
	// Token: 0x02000183 RID: 387
	internal class CaseInsensitiveAscii : IEqualityComparer, IComparer
	{
		// Token: 0x06000E5C RID: 3676 RVA: 0x0004B488 File Offset: 0x00049688
		public int GetHashCode(object myObject)
		{
			string text = myObject as string;
			if (myObject == null)
			{
				return 0;
			}
			int length = text.Length;
			if (length == 0)
			{
				return 0;
			}
			return length ^ (((int)CaseInsensitiveAscii.AsciiToLower[(int)((byte)text[0])] << 24) ^ ((int)CaseInsensitiveAscii.AsciiToLower[(int)((byte)text[length - 1])] << 16));
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x0004B4D8 File Offset: 0x000496D8
		public int Compare(object firstObject, object secondObject)
		{
			string text = firstObject as string;
			string text2 = secondObject as string;
			if (text == null)
			{
				if (text2 != null)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (text2 == null)
				{
					return 1;
				}
				int num = text.Length - text2.Length;
				int num2 = ((num > 0) ? text2.Length : text.Length);
				for (int i = 0; i < num2; i++)
				{
					int num3 = (int)(CaseInsensitiveAscii.AsciiToLower[(int)text[i]] - CaseInsensitiveAscii.AsciiToLower[(int)text2[i]]);
					if (num3 != 0)
					{
						num = num3;
						break;
					}
				}
				return num;
			}
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x0004B55C File Offset: 0x0004975C
		private int FastGetHashCode(string myString)
		{
			int num = myString.Length;
			if (num != 0)
			{
				num ^= ((int)CaseInsensitiveAscii.AsciiToLower[(int)((byte)myString[0])] << 24) ^ ((int)CaseInsensitiveAscii.AsciiToLower[(int)((byte)myString[num - 1])] << 16);
			}
			return num;
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x0004B59C File Offset: 0x0004979C
		public bool Equals(object firstObject, object secondObject)
		{
			string text = firstObject as string;
			string text2 = secondObject as string;
			if (text == null)
			{
				return text2 == null;
			}
			if (text2 != null)
			{
				int i = text.Length;
				if (i == text2.Length && this.FastGetHashCode(text) == this.FastGetHashCode(text2))
				{
					int length = text.Length;
					while (i > 0)
					{
						i--;
						if (CaseInsensitiveAscii.AsciiToLower[(int)text[i]] != CaseInsensitiveAscii.AsciiToLower[(int)text2[i]])
						{
							return false;
						}
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400125C RID: 4700
		internal static readonly CaseInsensitiveAscii StaticInstance = new CaseInsensitiveAscii();

		// Token: 0x0400125D RID: 4701
		internal static readonly byte[] AsciiToLower = new byte[]
		{
			0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
			10, 11, 12, 13, 14, 15, 16, 17, 18, 19,
			20, 21, 22, 23, 24, 25, 26, 27, 28, 29,
			30, 31, 32, 33, 34, 35, 36, 37, 38, 39,
			40, 41, 42, 43, 44, 45, 46, 47, 48, 49,
			50, 51, 52, 53, 54, 55, 56, 57, 58, 59,
			60, 61, 62, 63, 64, 97, 98, 99, 100, 101,
			102, 103, 104, 105, 106, 107, 108, 109, 110, 111,
			112, 113, 114, 115, 116, 117, 118, 119, 120, 121,
			122, 91, 92, 93, 94, 95, 96, 97, 98, 99,
			100, 101, 102, 103, 104, 105, 106, 107, 108, 109,
			110, 111, 112, 113, 114, 115, 116, 117, 118, 119,
			120, 121, 122, 123, 124, 125, 126, 127, 128, 129,
			130, 131, 132, 133, 134, 135, 136, 137, 138, 139,
			140, 141, 142, 143, 144, 145, 146, 147, 148, 149,
			150, 151, 152, 153, 154, 155, 156, 157, 158, 159,
			160, 161, 162, 163, 164, 165, 166, 167, 168, 169,
			170, 171, 172, 173, 174, 175, 176, 177, 178, 179,
			180, 181, 182, 183, 184, 185, 186, 187, 188, 189,
			190, 191, 192, 193, 194, 195, 196, 197, 198, 199,
			200, 201, 202, 203, 204, 205, 206, 207, 208, 209,
			210, 211, 212, 213, 214, 215, 216, 217, 218, 219,
			220, 221, 222, 223, 224, 225, 226, 227, 228, 229,
			230, 231, 232, 233, 234, 235, 236, 237, 238, 239,
			240, 241, 242, 243, 244, 245, 246, 247, 248, 249,
			250, 251, 252, 253, 254, byte.MaxValue
		};
	}
}
