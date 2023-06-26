using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x0200011A RID: 282
	internal static class ValidationHelper
	{
		// Token: 0x06000B1C RID: 2844 RVA: 0x0003D6A8 File Offset: 0x0003B8A8
		public static string[] MakeEmptyArrayNull(string[] stringArray)
		{
			if (stringArray == null || stringArray.Length == 0)
			{
				return null;
			}
			return stringArray;
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0003D6B4 File Offset: 0x0003B8B4
		public static string MakeStringNull(string stringValue)
		{
			if (stringValue == null || stringValue.Length == 0)
			{
				return null;
			}
			return stringValue;
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0003D6C4 File Offset: 0x0003B8C4
		public static string ExceptionMessage(Exception exception)
		{
			if (exception == null)
			{
				return string.Empty;
			}
			if (exception.InnerException == null)
			{
				return exception.Message;
			}
			return exception.Message + " (" + ValidationHelper.ExceptionMessage(exception.InnerException) + ")";
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0003D700 File Offset: 0x0003B900
		public static string ToString(object objectValue)
		{
			if (objectValue == null)
			{
				return "(null)";
			}
			if (objectValue is string && ((string)objectValue).Length == 0)
			{
				return "(string.empty)";
			}
			if (objectValue is Exception)
			{
				return ValidationHelper.ExceptionMessage(objectValue as Exception);
			}
			if (objectValue is IntPtr)
			{
				return "0x" + ((IntPtr)objectValue).ToString("x");
			}
			return objectValue.ToString();
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0003D774 File Offset: 0x0003B974
		public static string HashString(object objectValue)
		{
			if (objectValue == null)
			{
				return "(null)";
			}
			if (objectValue is string && ((string)objectValue).Length == 0)
			{
				return "(string.empty)";
			}
			return objectValue.GetHashCode().ToString(NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x0003D7B8 File Offset: 0x0003B9B8
		public static bool IsInvalidHttpString(string stringValue)
		{
			return stringValue.IndexOfAny(ValidationHelper.InvalidParamChars) != -1;
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0003D7CB File Offset: 0x0003B9CB
		public static bool IsBlankString(string stringValue)
		{
			return stringValue == null || stringValue.Length == 0;
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0003D7DB File Offset: 0x0003B9DB
		public static bool ValidateTcpPort(int port)
		{
			return port >= 0 && port <= 65535;
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x0003D7EE File Offset: 0x0003B9EE
		public static bool ValidateRange(int actual, int fromAllowed, int toAllowed)
		{
			return actual >= fromAllowed && actual <= toAllowed;
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x0003D800 File Offset: 0x0003BA00
		internal static void ValidateSegment(ArraySegment<byte> segment)
		{
			if (segment.Array == null)
			{
				throw new ArgumentNullException("segment");
			}
			if (segment.Offset < 0 || segment.Count < 0 || segment.Count > segment.Array.Length - segment.Offset)
			{
				throw new ArgumentOutOfRangeException("segment");
			}
		}

		// Token: 0x04000F70 RID: 3952
		public static string[] EmptyArray = new string[0];

		// Token: 0x04000F71 RID: 3953
		internal static readonly char[] InvalidMethodChars = new char[] { ' ', '\r', '\n', '\t' };

		// Token: 0x04000F72 RID: 3954
		internal static readonly char[] InvalidParamChars = new char[]
		{
			'(', ')', '<', '>', '@', ',', ';', ':', '\\', '"',
			'\'', '/', '[', ']', '?', '=', '{', '}', ' ', '\t',
			'\r', '\n'
		};
	}
}
