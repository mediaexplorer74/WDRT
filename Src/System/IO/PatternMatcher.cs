using System;

namespace System.IO
{
	// Token: 0x02000403 RID: 1027
	internal static class PatternMatcher
	{
		// Token: 0x06002691 RID: 9873 RVA: 0x000B18D8 File Offset: 0x000AFAD8
		public static bool StrictMatchPattern(string expression, string name)
		{
			char c = '\0';
			int[] array = new int[16];
			int[] array2 = new int[16];
			bool flag = false;
			if (name == null || name.Length == 0 || expression == null || expression.Length == 0)
			{
				return false;
			}
			if (expression.Equals("*") || expression.Equals("*.*"))
			{
				return true;
			}
			if (expression[0] == '*' && expression.IndexOf('*', 1) == -1)
			{
				int num = expression.Length - 1;
				if (name.Length >= num && string.Compare(expression, 1, name, name.Length - num, num, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return true;
				}
			}
			array[0] = 0;
			int num2 = 1;
			int num3 = 0;
			int num4 = expression.Length * 2;
			int num8;
			while (!flag)
			{
				if (num3 < name.Length)
				{
					c = name[num3];
					num3++;
				}
				else
				{
					flag = true;
					if (array[num2 - 1] == num4)
					{
						break;
					}
				}
				int i = 0;
				int num5 = 0;
				int j = 0;
				while (i < num2)
				{
					int num6 = (array[i++] + 1) / 2;
					int num7 = 0;
					while (num6 != expression.Length)
					{
						num6 += num7;
						num8 = num6 * 2;
						if (num6 == expression.Length)
						{
							array2[num5++] = num4;
							break;
						}
						char c2 = expression[num6];
						num7 = 1;
						if (num5 >= 14)
						{
							int num9 = array2.Length * 2;
							int[] array3 = new int[num9];
							Array.Copy(array2, array3, array2.Length);
							array2 = array3;
							array3 = new int[num9];
							Array.Copy(array, array3, array.Length);
							array = array3;
						}
						if (c2 == '*')
						{
							array2[num5++] = num8;
							array2[num5++] = num8 + 1;
						}
						else if (c2 == '>')
						{
							bool flag2 = false;
							if (!flag && c == '.')
							{
								int length = name.Length;
								for (int k = num3; k < length; k++)
								{
									char c3 = name[k];
									num7 = 1;
									if (c3 == '.')
									{
										flag2 = true;
										break;
									}
								}
							}
							if (flag || c != '.' || flag2)
							{
								array2[num5++] = num8;
								array2[num5++] = num8 + 1;
							}
							else
							{
								array2[num5++] = num8 + 1;
							}
						}
						else
						{
							num8 += num7 * 2;
							if (c2 == '<')
							{
								if (!flag && c != '.')
								{
									array2[num5++] = num8;
									break;
								}
							}
							else
							{
								if (c2 == '"')
								{
									if (flag)
									{
										continue;
									}
									if (c == '.')
									{
										array2[num5++] = num8;
										break;
									}
								}
								if (flag)
								{
									break;
								}
								if (c2 == '?')
								{
									array2[num5++] = num8;
									break;
								}
								if (c2 == c)
								{
									array2[num5++] = num8;
									break;
								}
								break;
							}
						}
					}
					if (i < num2 && j < num5)
					{
						while (j < num5)
						{
							int num10 = array.Length;
							while (i < num10 && array[i] < array2[j])
							{
								i++;
							}
							j++;
						}
					}
				}
				if (num5 == 0)
				{
					return false;
				}
				int[] array4 = array;
				array = array2;
				array2 = array4;
				num2 = num5;
			}
			num8 = array[num2 - 1];
			return num8 == num4;
		}

		// Token: 0x040020CB RID: 8395
		private const int MATCHES_ARRAY_SIZE = 16;

		// Token: 0x040020CC RID: 8396
		private const char ANSI_DOS_STAR = '>';

		// Token: 0x040020CD RID: 8397
		private const char ANSI_DOS_QM = '<';

		// Token: 0x040020CE RID: 8398
		private const char DOS_DOT = '"';
	}
}
