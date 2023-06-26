using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Data.OData
{
	// Token: 0x020002A8 RID: 680
	internal static class ExceptionUtils
	{
		// Token: 0x060016EE RID: 5870 RVA: 0x000536A8 File Offset: 0x000518A8
		internal static bool IsCatchableExceptionType(Exception e)
		{
			Type type = e.GetType();
			return type != ExceptionUtils.ThreadAbortType && type != ExceptionUtils.StackOverflowType && type != ExceptionUtils.OutOfMemoryType;
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x000536E3 File Offset: 0x000518E3
		internal static void CheckArgumentNotNull<T>([ExceptionUtils.ValidatedNotNullAttribute] T value, string parameterName) where T : class
		{
			if (value == null)
			{
				throw Error.ArgumentNull(parameterName);
			}
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x000536F4 File Offset: 0x000518F4
		internal static void CheckArgumentStringNotEmpty(string value, string parameterName)
		{
			if (value != null && value.Length == 0)
			{
				throw new ArgumentException(Strings.ExceptionUtils_ArgumentStringEmpty, parameterName);
			}
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x0005370D File Offset: 0x0005190D
		internal static void CheckArgumentStringNotNullOrEmpty([ExceptionUtils.ValidatedNotNullAttribute] string value, string parameterName)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentNullException(parameterName, Strings.ExceptionUtils_ArgumentStringNullOrEmpty);
			}
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x00053723 File Offset: 0x00051923
		internal static void CheckIntegerNotNegative(int value, string parameterName)
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException(parameterName, Strings.ExceptionUtils_CheckIntegerNotNegative(value));
			}
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x0005373B File Offset: 0x0005193B
		internal static void CheckIntegerPositive(int value, string parameterName)
		{
			if (value <= 0)
			{
				throw new ArgumentOutOfRangeException(parameterName, Strings.ExceptionUtils_CheckIntegerPositive(value));
			}
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x00053753 File Offset: 0x00051953
		internal static void CheckLongPositive(long value, string parameterName)
		{
			if (value <= 0L)
			{
				throw new ArgumentOutOfRangeException(parameterName, Strings.ExceptionUtils_CheckLongPositive(value));
			}
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x0005376C File Offset: 0x0005196C
		internal static void CheckArgumentCollectionNotNullOrEmpty<T>(ICollection<T> value, string parameterName)
		{
			if (value == null)
			{
				throw Error.ArgumentNull(parameterName);
			}
			if (value.Count == 0)
			{
				throw new ArgumentException(Strings.ExceptionUtils_ArgumentStringEmpty, parameterName);
			}
		}

		// Token: 0x0400097B RID: 2427
		private static readonly Type OutOfMemoryType = typeof(OutOfMemoryException);

		// Token: 0x0400097C RID: 2428
		private static readonly Type StackOverflowType = typeof(StackOverflowException);

		// Token: 0x0400097D RID: 2429
		private static readonly Type ThreadAbortType = typeof(ThreadAbortException);

		// Token: 0x020002A9 RID: 681
		private sealed class ValidatedNotNullAttribute : Attribute
		{
		}
	}
}
