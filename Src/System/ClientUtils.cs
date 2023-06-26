using System;
using System.Security;
using System.Threading;

namespace System
{
	// Token: 0x0200005B RID: 91
	internal static class ClientUtils
	{
		// Token: 0x06000404 RID: 1028 RVA: 0x0001D4C6 File Offset: 0x0001B6C6
		public static bool IsCriticalException(Exception ex)
		{
			return ex is NullReferenceException || ex is StackOverflowException || ex is OutOfMemoryException || ex is ThreadAbortException || ex is ExecutionEngineException || ex is IndexOutOfRangeException || ex is AccessViolationException;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0001D503 File Offset: 0x0001B703
		public static bool IsSecurityOrCriticalException(Exception ex)
		{
			return ex is SecurityException || ClientUtils.IsCriticalException(ex);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0001D518 File Offset: 0x0001B718
		public static int GetBitCount(uint x)
		{
			int num = 0;
			while (x > 0U)
			{
				x &= x - 1U;
				num++;
			}
			return num;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0001D53C File Offset: 0x0001B73C
		public static bool IsEnumValid(Enum enumValue, int value, int minValue, int maxValue)
		{
			return value >= minValue && value <= maxValue;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0001D55C File Offset: 0x0001B75C
		public static bool IsEnumValid(Enum enumValue, int value, int minValue, int maxValue, int maxNumberOfBitsOn)
		{
			bool flag = value >= minValue && value <= maxValue;
			return flag && ClientUtils.GetBitCount((uint)value) <= maxNumberOfBitsOn;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0001D590 File Offset: 0x0001B790
		public static bool IsEnumValid_Masked(Enum enumValue, int value, uint mask)
		{
			return ((long)value & (long)((ulong)mask)) == (long)value;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0001D5A8 File Offset: 0x0001B7A8
		public static bool IsEnumValid_NotSequential(Enum enumValue, int value, params int[] enumValues)
		{
			for (int i = 0; i < enumValues.Length; i++)
			{
				if (enumValues[i] == value)
				{
					return true;
				}
			}
			return false;
		}
	}
}
