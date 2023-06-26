using System;
using System.Globalization;

namespace System.Windows.Forms
{
	// Token: 0x0200037E RID: 894
	internal sealed class StringSorter
	{
		// Token: 0x06003A7C RID: 14972 RVA: 0x00101FA8 File Offset: 0x001001A8
		private StringSorter(CultureInfo culture, string[] keys, object[] items, int options)
		{
			if (keys == null)
			{
				if (items is string[])
				{
					keys = (string[])items;
					items = null;
				}
				else
				{
					keys = new string[items.Length];
					for (int i = 0; i < items.Length; i++)
					{
						object obj = items[i];
						if (obj != null)
						{
							keys[i] = obj.ToString();
						}
					}
				}
			}
			this.keys = keys;
			this.items = items;
			this.lcid = ((culture == null) ? SafeNativeMethods.GetThreadLocale() : culture.LCID);
			this.options = options & 200711;
			this.descending = (options & int.MinValue) != 0;
		}

		// Token: 0x06003A7D RID: 14973 RVA: 0x0010203E File Offset: 0x0010023E
		internal static int ArrayLength(object[] array)
		{
			if (array == null)
			{
				return 0;
			}
			return array.Length;
		}

		// Token: 0x06003A7E RID: 14974 RVA: 0x00102048 File Offset: 0x00100248
		public static int Compare(string s1, string s2)
		{
			return StringSorter.Compare(SafeNativeMethods.GetThreadLocale(), s1, s2, 0);
		}

		// Token: 0x06003A7F RID: 14975 RVA: 0x00102057 File Offset: 0x00100257
		public static int Compare(string s1, string s2, int options)
		{
			return StringSorter.Compare(SafeNativeMethods.GetThreadLocale(), s1, s2, options);
		}

		// Token: 0x06003A80 RID: 14976 RVA: 0x00102066 File Offset: 0x00100266
		public static int Compare(CultureInfo culture, string s1, string s2, int options)
		{
			return StringSorter.Compare(culture.LCID, s1, s2, options);
		}

		// Token: 0x06003A81 RID: 14977 RVA: 0x00102076 File Offset: 0x00100276
		private static int Compare(int lcid, string s1, string s2, int options)
		{
			if (s1 == null)
			{
				if (s2 != null)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (s2 == null)
				{
					return 1;
				}
				return string.Compare(s1, s2, false, CultureInfo.CurrentCulture);
			}
		}

		// Token: 0x06003A82 RID: 14978 RVA: 0x00102094 File Offset: 0x00100294
		private int CompareKeys(string s1, string s2)
		{
			int num = StringSorter.Compare(this.lcid, s1, s2, this.options);
			if (!this.descending)
			{
				return num;
			}
			return -num;
		}

		// Token: 0x06003A83 RID: 14979 RVA: 0x001020C4 File Offset: 0x001002C4
		private void QuickSort(int left, int right)
		{
			do
			{
				int num = left;
				int num2 = right;
				string text = this.keys[num + num2 >> 1];
				for (;;)
				{
					if (this.CompareKeys(this.keys[num], text) >= 0)
					{
						while (this.CompareKeys(text, this.keys[num2]) < 0)
						{
							num2--;
						}
						if (num > num2)
						{
							break;
						}
						if (num < num2)
						{
							string text2 = this.keys[num];
							this.keys[num] = this.keys[num2];
							this.keys[num2] = text2;
							if (this.items != null)
							{
								object obj = this.items[num];
								this.items[num] = this.items[num2];
								this.items[num2] = obj;
							}
						}
						num++;
						num2--;
						if (num > num2)
						{
							break;
						}
					}
					else
					{
						num++;
					}
				}
				if (num2 - left <= right - num)
				{
					if (left < num2)
					{
						this.QuickSort(left, num2);
					}
					left = num;
				}
				else
				{
					if (num < right)
					{
						this.QuickSort(num, right);
					}
					right = num2;
				}
			}
			while (left < right);
		}

		// Token: 0x06003A84 RID: 14980 RVA: 0x001021A6 File Offset: 0x001003A6
		public static void Sort(object[] items)
		{
			StringSorter.Sort(null, null, items, 0, StringSorter.ArrayLength(items), 0);
		}

		// Token: 0x06003A85 RID: 14981 RVA: 0x001021B8 File Offset: 0x001003B8
		public static void Sort(object[] items, int index, int count)
		{
			StringSorter.Sort(null, null, items, index, count, 0);
		}

		// Token: 0x06003A86 RID: 14982 RVA: 0x001021C5 File Offset: 0x001003C5
		public static void Sort(string[] keys, object[] items)
		{
			StringSorter.Sort(null, keys, items, 0, StringSorter.ArrayLength(items), 0);
		}

		// Token: 0x06003A87 RID: 14983 RVA: 0x001021D7 File Offset: 0x001003D7
		public static void Sort(string[] keys, object[] items, int index, int count)
		{
			StringSorter.Sort(null, keys, items, index, count, 0);
		}

		// Token: 0x06003A88 RID: 14984 RVA: 0x001021E4 File Offset: 0x001003E4
		public static void Sort(object[] items, int options)
		{
			StringSorter.Sort(null, null, items, 0, StringSorter.ArrayLength(items), options);
		}

		// Token: 0x06003A89 RID: 14985 RVA: 0x001021F6 File Offset: 0x001003F6
		public static void Sort(object[] items, int index, int count, int options)
		{
			StringSorter.Sort(null, null, items, index, count, options);
		}

		// Token: 0x06003A8A RID: 14986 RVA: 0x00102203 File Offset: 0x00100403
		public static void Sort(string[] keys, object[] items, int options)
		{
			StringSorter.Sort(null, keys, items, 0, StringSorter.ArrayLength(items), options);
		}

		// Token: 0x06003A8B RID: 14987 RVA: 0x00102215 File Offset: 0x00100415
		public static void Sort(string[] keys, object[] items, int index, int count, int options)
		{
			StringSorter.Sort(null, keys, items, index, count, options);
		}

		// Token: 0x06003A8C RID: 14988 RVA: 0x00102223 File Offset: 0x00100423
		public static void Sort(CultureInfo culture, object[] items, int options)
		{
			StringSorter.Sort(culture, null, items, 0, StringSorter.ArrayLength(items), options);
		}

		// Token: 0x06003A8D RID: 14989 RVA: 0x00102235 File Offset: 0x00100435
		public static void Sort(CultureInfo culture, object[] items, int index, int count, int options)
		{
			StringSorter.Sort(culture, null, items, index, count, options);
		}

		// Token: 0x06003A8E RID: 14990 RVA: 0x00102243 File Offset: 0x00100443
		public static void Sort(CultureInfo culture, string[] keys, object[] items, int options)
		{
			StringSorter.Sort(culture, keys, items, 0, StringSorter.ArrayLength(items), options);
		}

		// Token: 0x06003A8F RID: 14991 RVA: 0x00102258 File Offset: 0x00100458
		public static void Sort(CultureInfo culture, string[] keys, object[] items, int index, int count, int options)
		{
			if (items == null || (keys != null && keys.Length != items.Length))
			{
				throw new ArgumentException(SR.GetString("ArraysNotSameSize", new object[] { "keys", "items" }));
			}
			if (count > 1)
			{
				StringSorter stringSorter = new StringSorter(culture, keys, items, options);
				stringSorter.QuickSort(index, index + count - 1);
			}
		}

		// Token: 0x0400230A RID: 8970
		public const int IgnoreCase = 1;

		// Token: 0x0400230B RID: 8971
		public const int IgnoreKanaType = 65536;

		// Token: 0x0400230C RID: 8972
		public const int IgnoreNonSpace = 2;

		// Token: 0x0400230D RID: 8973
		public const int IgnoreSymbols = 4;

		// Token: 0x0400230E RID: 8974
		public const int IgnoreWidth = 131072;

		// Token: 0x0400230F RID: 8975
		public const int StringSort = 4096;

		// Token: 0x04002310 RID: 8976
		public const int Descending = -2147483648;

		// Token: 0x04002311 RID: 8977
		private const int CompareOptions = 200711;

		// Token: 0x04002312 RID: 8978
		private string[] keys;

		// Token: 0x04002313 RID: 8979
		private object[] items;

		// Token: 0x04002314 RID: 8980
		private int lcid;

		// Token: 0x04002315 RID: 8981
		private int options;

		// Token: 0x04002316 RID: 8982
		private bool descending;
	}
}
