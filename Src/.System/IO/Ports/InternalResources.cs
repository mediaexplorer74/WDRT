using System;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;

namespace System.IO.Ports
{
	// Token: 0x02000409 RID: 1033
	internal static class InternalResources
	{
		// Token: 0x060026A4 RID: 9892 RVA: 0x000B1CC5 File Offset: 0x000AFEC5
		internal static void EndOfFile()
		{
			throw new EndOfStreamException(SR.GetString("IO_EOF_ReadBeyondEOF"));
		}

		// Token: 0x060026A5 RID: 9893 RVA: 0x000B1CD8 File Offset: 0x000AFED8
		internal static string GetMessage(int errorCode)
		{
			StringBuilder stringBuilder = new StringBuilder(512);
			int num = SafeNativeMethods.FormatMessage(12800, IntPtr.Zero, (uint)errorCode, 0, stringBuilder, stringBuilder.Capacity, null);
			if (num != 0)
			{
				return stringBuilder.ToString();
			}
			return SR.GetString("IO_UnknownError", new object[] { errorCode });
		}

		// Token: 0x060026A6 RID: 9894 RVA: 0x000B1D2F File Offset: 0x000AFF2F
		internal static void FileNotOpen()
		{
			throw new ObjectDisposedException(null, SR.GetString("Port_not_open"));
		}

		// Token: 0x060026A7 RID: 9895 RVA: 0x000B1D41 File Offset: 0x000AFF41
		internal static void WrongAsyncResult()
		{
			throw new ArgumentException(SR.GetString("Arg_WrongAsyncResult"));
		}

		// Token: 0x060026A8 RID: 9896 RVA: 0x000B1D52 File Offset: 0x000AFF52
		internal static void EndReadCalledTwice()
		{
			throw new ArgumentException(SR.GetString("InvalidOperation_EndReadCalledMultiple"));
		}

		// Token: 0x060026A9 RID: 9897 RVA: 0x000B1D63 File Offset: 0x000AFF63
		internal static void EndWriteCalledTwice()
		{
			throw new ArgumentException(SR.GetString("InvalidOperation_EndWriteCalledMultiple"));
		}

		// Token: 0x060026AA RID: 9898 RVA: 0x000B1D74 File Offset: 0x000AFF74
		internal static void WinIOError()
		{
			int lastWin32Error = Marshal.GetLastWin32Error();
			InternalResources.WinIOError(lastWin32Error, string.Empty);
		}

		// Token: 0x060026AB RID: 9899 RVA: 0x000B1D94 File Offset: 0x000AFF94
		internal static void WinIOError(string str)
		{
			int lastWin32Error = Marshal.GetLastWin32Error();
			InternalResources.WinIOError(lastWin32Error, str);
		}

		// Token: 0x060026AC RID: 9900 RVA: 0x000B1DB0 File Offset: 0x000AFFB0
		internal static void WinIOError(int errorCode, string str)
		{
			if (errorCode <= 5)
			{
				if (errorCode - 2 > 1)
				{
					if (errorCode == 5)
					{
						if (str.Length == 0)
						{
							throw new UnauthorizedAccessException(SR.GetString("UnauthorizedAccess_IODenied_NoPathName"));
						}
						throw new UnauthorizedAccessException(SR.GetString("UnauthorizedAccess_IODenied_Path", new object[] { str }));
					}
				}
				else
				{
					if (str.Length == 0)
					{
						throw new IOException(SR.GetString("IO_PortNotFound"));
					}
					throw new IOException(SR.GetString("IO_PortNotFoundFileName", new object[] { str }));
				}
			}
			else if (errorCode != 32)
			{
				if (errorCode == 206)
				{
					throw new PathTooLongException(SR.GetString("IO_PathTooLong"));
				}
			}
			else
			{
				if (str.Length == 0)
				{
					throw new IOException(SR.GetString("IO_SharingViolation_NoFileName"));
				}
				throw new IOException(SR.GetString("IO_SharingViolation_File", new object[] { str }));
			}
			throw new IOException(InternalResources.GetMessage(errorCode), InternalResources.MakeHRFromErrorCode(errorCode));
		}

		// Token: 0x060026AD RID: 9901 RVA: 0x000B1E9C File Offset: 0x000B009C
		internal static int MakeHRFromErrorCode(int errorCode)
		{
			return -2147024896 | errorCode;
		}
	}
}
