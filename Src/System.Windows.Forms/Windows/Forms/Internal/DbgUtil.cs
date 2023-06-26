using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace System.Windows.Forms.Internal
{
	// Token: 0x020004ED RID: 1261
	[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
	[EnvironmentPermission(SecurityAction.Assert, Unrestricted = true)]
	[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
	[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
	[UIPermission(SecurityAction.Assert, Unrestricted = true)]
	internal sealed class DbgUtil
	{
		// Token: 0x0600522D RID: 21037
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetUserDefaultLCID();

		// Token: 0x0600522E RID: 21038
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int FormatMessage(int dwFlags, HandleRef lpSource, int dwMessageId, int dwLanguageId, StringBuilder lpBuffer, int nSize, HandleRef arguments);

		// Token: 0x0600522F RID: 21039 RVA: 0x000070A6 File Offset: 0x000052A6
		[Conditional("DEBUG")]
		public static void AssertFinalization(object obj, bool disposing)
		{
		}

		// Token: 0x06005230 RID: 21040 RVA: 0x000070A6 File Offset: 0x000052A6
		[Conditional("DEBUG")]
		public static void AssertWin32(bool expression, string message)
		{
		}

		// Token: 0x06005231 RID: 21041 RVA: 0x000070A6 File Offset: 0x000052A6
		[Conditional("DEBUG")]
		public static void AssertWin32(bool expression, string format, object arg1)
		{
		}

		// Token: 0x06005232 RID: 21042 RVA: 0x000070A6 File Offset: 0x000052A6
		[Conditional("DEBUG")]
		public static void AssertWin32(bool expression, string format, object arg1, object arg2)
		{
		}

		// Token: 0x06005233 RID: 21043 RVA: 0x000070A6 File Offset: 0x000052A6
		[Conditional("DEBUG")]
		public static void AssertWin32(bool expression, string format, object arg1, object arg2, object arg3)
		{
		}

		// Token: 0x06005234 RID: 21044 RVA: 0x000070A6 File Offset: 0x000052A6
		[Conditional("DEBUG")]
		public static void AssertWin32(bool expression, string format, object arg1, object arg2, object arg3, object arg4)
		{
		}

		// Token: 0x06005235 RID: 21045 RVA: 0x000070A6 File Offset: 0x000052A6
		[Conditional("DEBUG")]
		public static void AssertWin32(bool expression, string format, object arg1, object arg2, object arg3, object arg4, object arg5)
		{
		}

		// Token: 0x06005236 RID: 21046 RVA: 0x000070A6 File Offset: 0x000052A6
		[Conditional("DEBUG")]
		private static void AssertWin32Impl(bool expression, string format, object[] args)
		{
		}

		// Token: 0x06005237 RID: 21047 RVA: 0x001552F8 File Offset: 0x001534F8
		public static string GetLastErrorStr()
		{
			int num = 255;
			StringBuilder stringBuilder = new StringBuilder(num);
			string text = string.Empty;
			int num2 = 0;
			try
			{
				num2 = Marshal.GetLastWin32Error();
				text = ((DbgUtil.FormatMessage(4608, new HandleRef(null, IntPtr.Zero), num2, DbgUtil.GetUserDefaultLCID(), stringBuilder, num, new HandleRef(null, IntPtr.Zero)) != 0) ? stringBuilder.ToString() : "<error returned>");
			}
			catch (Exception ex)
			{
				if (DbgUtil.IsCriticalException(ex))
				{
					throw;
				}
				text = ex.ToString();
			}
			return string.Format(CultureInfo.CurrentCulture, "0x{0:x8} - {1}", new object[] { num2, text });
		}

		// Token: 0x06005238 RID: 21048 RVA: 0x001553A8 File Offset: 0x001535A8
		private static bool IsCriticalException(Exception ex)
		{
			return ex is StackOverflowException || ex is OutOfMemoryException || ex is ThreadAbortException;
		}

		// Token: 0x170013B7 RID: 5047
		// (get) Token: 0x06005239 RID: 21049 RVA: 0x001553C5 File Offset: 0x001535C5
		public static string StackTrace
		{
			get
			{
				return Environment.StackTrace;
			}
		}

		// Token: 0x0600523A RID: 21050 RVA: 0x001553CC File Offset: 0x001535CC
		public static string StackFramesToStr(int maxFrameCount)
		{
			string text = string.Empty;
			try
			{
				StackTrace stackTrace = new StackTrace(true);
				int i;
				for (i = 0; i < stackTrace.FrameCount; i++)
				{
					StackFrame frame = stackTrace.GetFrame(i);
					if (frame == null || frame.GetMethod().DeclaringType != typeof(DbgUtil))
					{
						break;
					}
				}
				maxFrameCount += i;
				if (maxFrameCount > stackTrace.FrameCount)
				{
					maxFrameCount = stackTrace.FrameCount;
				}
				for (int j = i; j < maxFrameCount; j++)
				{
					StackFrame frame2 = stackTrace.GetFrame(j);
					if (frame2 != null)
					{
						MethodBase method = frame2.GetMethod();
						if (!(method == null))
						{
							string text2 = string.Empty;
							string text3 = frame2.GetFileName();
							int num = ((text3 == null) ? (-1) : text3.LastIndexOf('\\'));
							if (num != -1)
							{
								text3 = text3.Substring(num + 1, text3.Length - num - 1);
							}
							foreach (ParameterInfo parameterInfo in method.GetParameters())
							{
								text2 = text2 + parameterInfo.ParameterType.Name + ", ";
							}
							if (text2.Length > 0)
							{
								text2 = text2.Substring(0, text2.Length - 2);
							}
							text += string.Format(CultureInfo.CurrentCulture, "at {0} {1}.{2}({3})\r\n", new object[] { text3, method.DeclaringType, method.Name, text2 });
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (DbgUtil.IsCriticalException(ex))
				{
					throw;
				}
				text += ex.ToString();
			}
			return text.ToString();
		}

		// Token: 0x0600523B RID: 21051 RVA: 0x00155588 File Offset: 0x00153788
		public static string StackFramesToStr()
		{
			return DbgUtil.StackFramesToStr(DbgUtil.gdipInitMaxFrameCount);
		}

		// Token: 0x0600523C RID: 21052 RVA: 0x00155594 File Offset: 0x00153794
		public static string StackTraceToStr(string message, int frameCount)
		{
			return string.Format(CultureInfo.CurrentCulture, "{0}\r\nTop Stack Trace:\r\n{1}", new object[]
			{
				message,
				DbgUtil.StackFramesToStr(frameCount)
			});
		}

		// Token: 0x0600523D RID: 21053 RVA: 0x001555B8 File Offset: 0x001537B8
		public static string StackTraceToStr(string message)
		{
			return DbgUtil.StackTraceToStr(message, DbgUtil.gdipInitMaxFrameCount);
		}

		// Token: 0x04003615 RID: 13845
		public const int FORMAT_MESSAGE_ALLOCATE_BUFFER = 256;

		// Token: 0x04003616 RID: 13846
		public const int FORMAT_MESSAGE_IGNORE_INSERTS = 512;

		// Token: 0x04003617 RID: 13847
		public const int FORMAT_MESSAGE_FROM_SYSTEM = 4096;

		// Token: 0x04003618 RID: 13848
		public const int FORMAT_MESSAGE_DEFAULT = 4608;

		// Token: 0x04003619 RID: 13849
		public static int gdipInitMaxFrameCount = 8;

		// Token: 0x0400361A RID: 13850
		public static int gdiUseMaxFrameCount = 8;

		// Token: 0x0400361B RID: 13851
		public static int finalizeMaxFrameCount = 5;
	}
}
