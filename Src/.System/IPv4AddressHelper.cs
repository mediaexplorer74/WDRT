using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000043 RID: 67
	internal static class IPv4AddressHelper
	{
		// Token: 0x060003CF RID: 975 RVA: 0x0001AFF4 File Offset: 0x000191F4
		internal unsafe static string ParseCanonicalName(string str, int start, int end, ref bool isLoopback)
		{
			byte* ptr = stackalloc byte[(UIntPtr)4];
			isLoopback = IPv4AddressHelper.Parse(str, ptr, start, end);
			return string.Concat(new string[]
			{
				ptr->ToString(),
				".",
				ptr[1].ToString(),
				".",
				ptr[2].ToString(),
				".",
				ptr[3].ToString()
			});
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0001B060 File Offset: 0x00019260
		internal unsafe static int ParseHostNumber(string str, int start, int end)
		{
			byte* ptr = stackalloc byte[(UIntPtr)4];
			IPv4AddressHelper.ParseCanonical(str, ptr, start, end);
			return ((int)(*ptr) << 24) + ((int)ptr[1] << 16) + ((int)ptr[2] << 8) + (int)ptr[3];
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0001B095 File Offset: 0x00019295
		internal unsafe static bool IsValid(char* name, int start, ref int end, bool allowIPv6, bool notImplicitFile, bool unknownScheme)
		{
			if (allowIPv6 || unknownScheme)
			{
				return IPv4AddressHelper.IsValidCanonical(name, start, ref end, allowIPv6, notImplicitFile);
			}
			return IPv4AddressHelper.ParseNonCanonical(name, start, ref end, notImplicitFile) != -1L;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0001B0BC File Offset: 0x000192BC
		internal unsafe static bool IsValidCanonical(char* name, int start, ref int end, bool allowIPv6, bool notImplicitFile)
		{
			int num = 0;
			int num2 = 0;
			bool flag = false;
			bool flag2 = false;
			while (start < end)
			{
				char c = name[start];
				if (allowIPv6)
				{
					if (c == ']' || c == '/')
					{
						break;
					}
					if (c == '%')
					{
						break;
					}
				}
				else if (c == '/' || c == '\\' || (notImplicitFile && (c == ':' || c == '?' || c == '#')))
				{
					break;
				}
				if (c <= '9' && c >= '0')
				{
					if (!flag && c == '0')
					{
						if (start + 1 < end && name[start + 1] == '0')
						{
							return false;
						}
						flag2 = true;
					}
					flag = true;
					num2 = num2 * 10 + (int)(name[start] - '0');
					if (num2 > 255)
					{
						return false;
					}
				}
				else
				{
					if (c != '.')
					{
						return false;
					}
					if (!flag || (num2 > 0 && flag2))
					{
						return false;
					}
					num++;
					flag = false;
					num2 = 0;
					flag2 = false;
				}
				start++;
			}
			bool flag3 = num == 3 && flag;
			if (flag3)
			{
				end = start;
			}
			return flag3;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0001B1B4 File Offset: 0x000193B4
		internal unsafe static long ParseNonCanonical(char* name, int start, ref int end, bool notImplicitFile)
		{
			long[] array = new long[4];
			long num = 0L;
			bool flag = false;
			int num2 = 0;
			int i;
			for (i = start; i < end; i++)
			{
				char c = name[i];
				num = 0L;
				int num3 = 10;
				if (c == '0')
				{
					num3 = 8;
					i++;
					flag = true;
					if (i < end)
					{
						c = name[i];
						if (c == 'x' || c == 'X')
						{
							num3 = 16;
							i++;
							flag = false;
						}
					}
				}
				while (i < end)
				{
					c = name[i];
					int num4;
					if ((num3 == 10 || num3 == 16) && '0' <= c && c <= '9')
					{
						num4 = (int)(c - '0');
					}
					else if (num3 == 8 && '0' <= c && c <= '7')
					{
						num4 = (int)(c - '0');
					}
					else if (num3 == 16 && 'a' <= c && c <= 'f')
					{
						num4 = (int)(c + '\n' - 'a');
					}
					else
					{
						if (num3 != 16 || 'A' > c || c > 'F')
						{
							break;
						}
						num4 = (int)(c + '\n' - 'A');
					}
					num = num * (long)num3 + (long)num4;
					if (num > (long)((ulong)(-1)))
					{
						return -1L;
					}
					flag = true;
					i++;
				}
				if (i >= end || name[i] != '.')
				{
					break;
				}
				if (num2 >= 3 || !flag || num > 255L)
				{
					return -1L;
				}
				array[num2] = num;
				num2++;
				flag = false;
			}
			if (!flag)
			{
				return -1L;
			}
			if (i < end)
			{
				char c;
				if ((c = name[i]) != '/' && c != '\\' && (!notImplicitFile || (c != ':' && c != '?' && c != '#')))
				{
					return -1L;
				}
				end = i;
			}
			array[num2] = num;
			switch (num2)
			{
			case 0:
				if (array[0] > (long)((ulong)(-1)))
				{
					return -1L;
				}
				return array[0];
			case 1:
				if (array[1] > 16777215L)
				{
					return -1L;
				}
				return (array[0] << 24) | (array[1] & 16777215L);
			case 2:
				if (array[2] > 65535L)
				{
					return -1L;
				}
				return (array[0] << 24) | ((array[1] & 255L) << 16) | (array[2] & 65535L);
			case 3:
				if (array[3] > 255L)
				{
					return -1L;
				}
				return (array[0] << 24) | ((array[1] & 255L) << 16) | ((array[2] & 255L) << 8) | (array[3] & 255L);
			default:
				return -1L;
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0001B3F8 File Offset: 0x000195F8
		private unsafe static bool Parse(string name, byte* numbers, int start, int end)
		{
			fixed (string text = name)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				int num = end;
				long num2 = IPv4AddressHelper.ParseNonCanonical(ptr, start, ref num, true);
				*numbers = (byte)(num2 >> 24);
				numbers[1] = (byte)(num2 >> 16);
				numbers[2] = (byte)(num2 >> 8);
				numbers[3] = (byte)num2;
			}
			return *numbers == 127;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0001B448 File Offset: 0x00019648
		private unsafe static bool ParseCanonical(string name, byte* numbers, int start, int end)
		{
			for (int i = 0; i < 4; i++)
			{
				byte b = 0;
				char c;
				while (start < end && (c = name[start]) != '.' && c != ':')
				{
					b = b * 10 + (byte)(c - '0');
					start++;
				}
				numbers[i] = b;
				start++;
			}
			return *numbers == 127;
		}

		// Token: 0x0400044C RID: 1100
		internal const long Invalid = -1L;

		// Token: 0x0400044D RID: 1101
		private const long MaxIPv4Value = 4294967295L;

		// Token: 0x0400044E RID: 1102
		private const int Octal = 8;

		// Token: 0x0400044F RID: 1103
		private const int Decimal = 10;

		// Token: 0x04000450 RID: 1104
		private const int Hex = 16;

		// Token: 0x04000451 RID: 1105
		private const int NumberOfLabels = 4;
	}
}
