using System;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Helpers
{
	// Token: 0x02000038 RID: 56
	public static class VersionComparer
	{
		// Token: 0x06000351 RID: 849 RVA: 0x0000CE90 File Offset: 0x0000B090
		public static SwVersionComparisonResult CompareSoftwareVersions(string first, string second, params char[] splitChars)
		{
			bool flag = first == null || second == null;
			SwVersionComparisonResult swVersionComparisonResult;
			if (flag)
			{
				swVersionComparisonResult = SwVersionComparisonResult.UnableToCompare;
			}
			else
			{
				string[] array = first.Split(splitChars);
				string[] array2 = second.Split(splitChars);
				bool flag2 = array.Length != array2.Length;
				if (flag2)
				{
					swVersionComparisonResult = SwVersionComparisonResult.UnableToCompare;
				}
				else
				{
					for (int i = 0; i < array.Length; i++)
					{
						string text = array[i];
						string text2 = array2[i];
						int num;
						int num2;
						bool flag3 = int.TryParse(text, out num) && int.TryParse(text2, out num2);
						if (flag3)
						{
							bool flag4 = num2 > num;
							if (flag4)
							{
								return SwVersionComparisonResult.SecondIsGreater;
							}
							bool flag5 = num2 < num;
							if (flag5)
							{
								return SwVersionComparisonResult.FirstIsGreater;
							}
						}
					}
					swVersionComparisonResult = SwVersionComparisonResult.NumbersAreEqual;
				}
			}
			return swVersionComparisonResult;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000CF48 File Offset: 0x0000B148
		public static int CompareVersions(string a, string b)
		{
			int[] array = VersionComparer.ConvertVersionToTable(a);
			int[] array2 = VersionComparer.ConvertVersionToTable(b);
			for (int i = 0; i < Math.Min(array.Length, array2.Length); i++)
			{
				int num = array[i];
				int num2 = array2[i];
				bool flag = num != num2;
				if (flag)
				{
					return num - num2;
				}
			}
			return array.Length - array2.Length;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000CFB0 File Offset: 0x0000B1B0
		private static int[] ConvertVersionToTable(string version)
		{
			string[] array = version.Split(new char[] { '.' });
			int[] array2 = new int[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = int.Parse(array[i]);
			}
			return array2;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000D000 File Offset: 0x0000B200
		public static int Compare(string a, string b)
		{
			int[] array = VersionComparer.Convert(a);
			int[] array2 = VersionComparer.Convert(b);
			for (int i = 0; i < Math.Min(array.Length, array2.Length); i++)
			{
				int num = array[i];
				int num2 = array2[i];
				bool flag = num != num2;
				if (flag)
				{
					return num - num2;
				}
			}
			return array.Length - array2.Length;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000D068 File Offset: 0x0000B268
		private static int[] Convert(string version)
		{
			string[] array = version.Split(new char[] { '.' });
			int[] array2 = new int[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = int.Parse(array[i]);
			}
			return array2;
		}
	}
}
