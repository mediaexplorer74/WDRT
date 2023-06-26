using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Win32
{
	// Token: 0x02000021 RID: 33
	[SuppressUnmanagedCodeSecurity]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	internal static class UnsafeNativeMethods
	{
		// Token: 0x06000218 RID: 536
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr GetStdHandle(int type);

		// Token: 0x06000219 RID: 537
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

		// Token: 0x0600021A RID: 538
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr GetDC(IntPtr hWnd);

		// Token: 0x0600021B RID: 539
		[DllImport("gdi32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

		// Token: 0x0600021C RID: 540
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int GetSystemMetrics(int nIndex);

		// Token: 0x0600021D RID: 541
		[DllImport("user32.dll", ExactSpelling = true)]
		public static extern IntPtr GetProcessWindowStation();

		// Token: 0x0600021E RID: 542
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool GetUserObjectInformation(HandleRef hObj, int nIndex, [MarshalAs(UnmanagedType.LPStruct)] NativeMethods.USEROBJECTFLAGS pvBuffer, int nLength, ref int lpnLengthNeeded);

		// Token: 0x0600021F RID: 543
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
		public static extern IntPtr GetModuleHandle(string modName);

		// Token: 0x06000220 RID: 544
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr GetProcAddress(IntPtr hModule, string methodName);

		// Token: 0x06000221 RID: 545
		[DllImport("user32.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
		public static extern bool GetClassInfo(HandleRef hInst, string lpszClass, [In] [Out] NativeMethods.WNDCLASS_I wc);

		// Token: 0x06000222 RID: 546
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool IsWindow(HandleRef hWnd);

		// Token: 0x06000223 RID: 547
		[DllImport("wldp.dll", ExactSpelling = true)]
		public static extern int WldpIsDynamicCodePolicyEnabled(out int enabled);

		// Token: 0x06000224 RID: 548
		[DllImport("wldp.dll", ExactSpelling = true)]
		public static extern int WldpSetDynamicCodeTrust([In] SafeFileHandle fileHandle);

		// Token: 0x06000225 RID: 549
		[DllImport("wldp.dll", ExactSpelling = true)]
		public static extern int WldpQueryDynamicCodeTrust([In] SafeFileHandle fileHandle, [In] IntPtr image, [In] uint imageSize);

		// Token: 0x06000226 RID: 550 RVA: 0x0000F28A File Offset: 0x0000D48A
		public static IntPtr SetClassLong(HandleRef hWnd, int nIndex, IntPtr dwNewLong)
		{
			if (IntPtr.Size == 4)
			{
				return UnsafeNativeMethods.SetClassLongPtr32(hWnd, nIndex, dwNewLong);
			}
			return UnsafeNativeMethods.SetClassLongPtr64(hWnd, nIndex, dwNewLong);
		}

		// Token: 0x06000227 RID: 551
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SetClassLong")]
		public static extern IntPtr SetClassLongPtr32(HandleRef hwnd, int nIndex, IntPtr dwNewLong);

		// Token: 0x06000228 RID: 552
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SetClassLongPtr")]
		public static extern IntPtr SetClassLongPtr64(HandleRef hwnd, int nIndex, IntPtr dwNewLong);

		// Token: 0x06000229 RID: 553 RVA: 0x0000F2A5 File Offset: 0x0000D4A5
		public static IntPtr SetWindowLong(HandleRef hWnd, int nIndex, HandleRef dwNewLong)
		{
			if (IntPtr.Size == 4)
			{
				return UnsafeNativeMethods.SetWindowLongPtr32(hWnd, nIndex, dwNewLong);
			}
			return UnsafeNativeMethods.SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
		}

		// Token: 0x0600022A RID: 554
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowLong")]
		public static extern IntPtr SetWindowLongPtr32(HandleRef hWnd, int nIndex, HandleRef dwNewLong);

		// Token: 0x0600022B RID: 555
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowLongPtr")]
		public static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, HandleRef dwNewLong);

		// Token: 0x0600022C RID: 556
		[DllImport("user32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern short RegisterClass(NativeMethods.WNDCLASS wc);

		// Token: 0x0600022D RID: 557
		[DllImport("user32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern short UnregisterClass(string lpClassName, HandleRef hInstance);

		// Token: 0x0600022E RID: 558
		[DllImport("user32.dll", BestFitMapping = true, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr CreateWindowEx(int exStyle, string lpszClassName, string lpszWindowName, int style, int x, int y, int width, int height, HandleRef hWndParent, HandleRef hMenu, HandleRef hInst, [MarshalAs(UnmanagedType.AsAny)] object pvParam);

		// Token: 0x0600022F RID: 559
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x06000230 RID: 560
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern bool SetConsoleCtrlHandler(NativeMethods.ConHndlr handler, int add);

		// Token: 0x06000231 RID: 561
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr DefWindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x06000232 RID: 562
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern bool DestroyWindow(HandleRef hWnd);

		// Token: 0x06000233 RID: 563
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int MsgWaitForMultipleObjectsEx(int nCount, IntPtr pHandles, int dwMilliseconds, int dwWakeMask, int dwFlags);

		// Token: 0x06000234 RID: 564
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int DispatchMessage([In] ref NativeMethods.MSG msg);

		// Token: 0x06000235 RID: 565
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern bool PeekMessage([In] [Out] ref NativeMethods.MSG msg, HandleRef hwnd, int msgMin, int msgMax, int remove);

		// Token: 0x06000236 RID: 566
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SetTimer(HandleRef hWnd, HandleRef nIDEvent, int uElapse, HandleRef lpTimerProc);

		// Token: 0x06000237 RID: 567
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern bool KillTimer(HandleRef hwnd, HandleRef idEvent);

		// Token: 0x06000238 RID: 568
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern bool TranslateMessage([In] [Out] ref NativeMethods.MSG msg);

		// Token: 0x06000239 RID: 569
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Ansi)]
		public static extern IntPtr GetProcAddress(HandleRef hModule, string lpProcName);

		// Token: 0x0600023A RID: 570
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern bool PostMessage(HandleRef hwnd, int msg, IntPtr wparam, IntPtr lparam);

		// Token: 0x0600023B RID: 571
		[DllImport("wtsapi32.dll", CharSet = CharSet.Auto)]
		public static extern bool WTSRegisterSessionNotification(HandleRef hWnd, int dwFlags);

		// Token: 0x0600023C RID: 572
		[DllImport("wtsapi32.dll", CharSet = CharSet.Auto)]
		public static extern bool WTSUnRegisterSessionNotification(HandleRef hWnd);

		// Token: 0x0600023D RID: 573 RVA: 0x0000F2C0 File Offset: 0x0000D4C0
		private static IntPtr GetCurrentProcessToken()
		{
			return new IntPtr(-4);
		}

		// Token: 0x0600023E RID: 574
		[SecuritySafeCritical]
		[DllImport("kernel32.dll", EntryPoint = "AppPolicyGetClrCompat")]
		[return: MarshalAs(UnmanagedType.I4)]
		private static extern int _AppPolicyGetClrCompat(IntPtr processToken, out UnsafeNativeMethods.AppPolicyClrCompat appPolicyClrCompat);

		// Token: 0x0600023F RID: 575
		[SecuritySafeCritical]
		[DllImport("kernel32.dll", EntryPoint = "GetCurrentPackageId")]
		[return: MarshalAs(UnmanagedType.I4)]
		private static extern int _GetCurrentPackageId(ref int pBufferLength, byte[] pBuffer);

		// Token: 0x06000240 RID: 576 RVA: 0x0000F2CC File Offset: 0x0000D4CC
		[SecurityCritical]
		private static bool DoesWin32MethodExist(string moduleName, string methodName)
		{
			IntPtr moduleHandle = UnsafeNativeMethods.GetModuleHandle(moduleName);
			if (moduleHandle == IntPtr.Zero)
			{
				return false;
			}
			IntPtr procAddress = UnsafeNativeMethods.GetProcAddress(moduleHandle, methodName);
			return procAddress != IntPtr.Zero;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000F304 File Offset: 0x0000D504
		[SecuritySafeCritical]
		private static bool _IsPackagedProcess()
		{
			Version version = new Version(6, 2, 0, 0);
			OperatingSystem osversion = Environment.OSVersion;
			bool flag = osversion.Platform == PlatformID.Win32NT && osversion.Version >= version;
			if (flag && UnsafeNativeMethods.DoesWin32MethodExist("kernel32.dll", "AppPolicyGetClrCompat"))
			{
				UnsafeNativeMethods.AppPolicyClrCompat appPolicyClrCompat;
				return UnsafeNativeMethods._AppPolicyGetClrCompat(UnsafeNativeMethods.GetCurrentProcessToken(), out appPolicyClrCompat) == 0 && appPolicyClrCompat == UnsafeNativeMethods.AppPolicyClrCompat.AppPolicyClrCompat_Universal;
			}
			if (flag && UnsafeNativeMethods.DoesWin32MethodExist("kernel32.dll", "GetCurrentPackageId"))
			{
				int num = 0;
				return UnsafeNativeMethods._GetCurrentPackageId(ref num, null) == 122;
			}
			return false;
		}

		// Token: 0x06000242 RID: 578
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int LookupAccountSid(string systemName, byte[] pSid, StringBuilder szUserName, ref int userNameSize, StringBuilder szDomainName, ref int domainNameSize, ref int eUse);

		// Token: 0x06000243 RID: 579
		[DllImport("version.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetFileVersionInfoSize(string lptstrFilename, out int handle);

		// Token: 0x06000244 RID: 580
		[DllImport("version.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
		public static extern bool GetFileVersionInfo(string lptstrFilename, int dwHandle, int dwLen, HandleRef lpData);

		// Token: 0x06000245 RID: 581
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern int GetModuleFileName(HandleRef hModule, StringBuilder buffer, int length);

		// Token: 0x06000246 RID: 582
		[DllImport("version.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
		public static extern bool VerQueryValue(HandleRef pBlock, string lpSubBlock, [In] [Out] ref IntPtr lplpBuffer, out int len);

		// Token: 0x06000247 RID: 583
		[DllImport("version.dll", BestFitMapping = true, CharSet = CharSet.Auto)]
		public static extern int VerLanguageName(int langID, StringBuilder lpBuffer, int nSize);

		// Token: 0x06000248 RID: 584
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern bool ReportEvent(SafeHandle hEventLog, short type, ushort category, uint eventID, byte[] userSID, short numStrings, int dataLen, HandleRef strings, byte[] rawData);

		// Token: 0x06000249 RID: 585
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern bool ClearEventLog(SafeHandle hEventLog, HandleRef lpctstrBackupFileName);

		// Token: 0x0600024A RID: 586
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern bool GetNumberOfEventLogRecords(SafeHandle hEventLog, out int count);

		// Token: 0x0600024B RID: 587
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetOldestEventLogRecord(SafeHandle hEventLog, out int number);

		// Token: 0x0600024C RID: 588
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool ReadEventLog(SafeHandle hEventLog, int dwReadFlags, int dwRecordOffset, byte[] buffer, int numberOfBytesToRead, out int bytesRead, out int minNumOfBytesNeeded);

		// Token: 0x0600024D RID: 589
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool NotifyChangeEventLog(SafeHandle hEventLog, SafeWaitHandle hEvent);

		// Token: 0x0600024E RID: 590
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public unsafe static extern bool ReadDirectoryChangesW(SafeFileHandle hDirectory, HandleRef lpBuffer, int nBufferLength, int bWatchSubtree, int dwNotifyFilter, out int lpBytesReturned, NativeOverlapped* overlappedPointer, HandleRef lpCompletionRoutine);

		// Token: 0x0600024F RID: 591
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern SafeFileHandle CreateFile(string lpFileName, int dwDesiredAccess, int dwShareMode, IntPtr securityAttrs, int dwCreationDisposition, int dwFlagsAndAttributes, IntPtr hTemplateFile);

		// Token: 0x06000250 RID: 592
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool GetCommState(SafeFileHandle hFile, ref UnsafeNativeMethods.DCB lpDCB);

		// Token: 0x06000251 RID: 593
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool SetCommState(SafeFileHandle hFile, ref UnsafeNativeMethods.DCB lpDCB);

		// Token: 0x06000252 RID: 594
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool GetCommModemStatus(SafeFileHandle hFile, ref int lpModemStat);

		// Token: 0x06000253 RID: 595
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool SetupComm(SafeFileHandle hFile, int dwInQueue, int dwOutQueue);

		// Token: 0x06000254 RID: 596
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool SetCommTimeouts(SafeFileHandle hFile, ref UnsafeNativeMethods.COMMTIMEOUTS lpCommTimeouts);

		// Token: 0x06000255 RID: 597
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool SetCommBreak(SafeFileHandle hFile);

		// Token: 0x06000256 RID: 598
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool ClearCommBreak(SafeFileHandle hFile);

		// Token: 0x06000257 RID: 599
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool ClearCommError(SafeFileHandle hFile, ref int lpErrors, ref UnsafeNativeMethods.COMSTAT lpStat);

		// Token: 0x06000258 RID: 600
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool ClearCommError(SafeFileHandle hFile, ref int lpErrors, IntPtr lpStat);

		// Token: 0x06000259 RID: 601
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool PurgeComm(SafeFileHandle hFile, uint dwFlags);

		// Token: 0x0600025A RID: 602
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool FlushFileBuffers(SafeFileHandle hFile);

		// Token: 0x0600025B RID: 603
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool GetCommProperties(SafeFileHandle hFile, ref UnsafeNativeMethods.COMMPROP lpCommProp);

		// Token: 0x0600025C RID: 604
		[DllImport("kernel32.dll", SetLastError = true)]
		internal unsafe static extern int ReadFile(SafeFileHandle handle, byte* bytes, int numBytesToRead, IntPtr numBytesRead, NativeOverlapped* overlapped);

		// Token: 0x0600025D RID: 605
		[DllImport("kernel32.dll", SetLastError = true)]
		internal unsafe static extern int ReadFile(SafeFileHandle handle, byte* bytes, int numBytesToRead, out int numBytesRead, IntPtr overlapped);

		// Token: 0x0600025E RID: 606
		[DllImport("kernel32.dll", SetLastError = true)]
		internal unsafe static extern int WriteFile(SafeFileHandle handle, byte* bytes, int numBytesToWrite, IntPtr numBytesWritten, NativeOverlapped* lpOverlapped);

		// Token: 0x0600025F RID: 607
		[DllImport("kernel32.dll", SetLastError = true)]
		internal unsafe static extern int WriteFile(SafeFileHandle handle, byte* bytes, int numBytesToWrite, out int numBytesWritten, IntPtr lpOverlapped);

		// Token: 0x06000260 RID: 608
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern int GetFileType(SafeFileHandle hFile);

		// Token: 0x06000261 RID: 609
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool EscapeCommFunction(SafeFileHandle hFile, int dwFunc);

		// Token: 0x06000262 RID: 610
		[DllImport("kernel32.dll", SetLastError = true)]
		internal unsafe static extern bool WaitCommEvent(SafeFileHandle hFile, int* lpEvtMask, NativeOverlapped* lpOverlapped);

		// Token: 0x06000263 RID: 611
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool SetCommMask(SafeFileHandle hFile, int dwEvtMask);

		// Token: 0x06000264 RID: 612
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal unsafe static extern bool GetOverlappedResult(SafeFileHandle hFile, NativeOverlapped* lpOverlapped, ref int lpNumberOfBytesTransferred, bool bWait);

		// Token: 0x06000265 RID: 613
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool GetTokenInformation([In] IntPtr TokenHandle, [In] uint TokenInformationClass, [In] IntPtr TokenInformation, [In] uint TokenInformationLength, out uint ReturnLength);

		// Token: 0x06000266 RID: 614
		[DllImport("ole32.dll")]
		internal static extern int CoGetStandardMarshal(ref Guid riid, IntPtr pv, int dwDestContext, IntPtr pvDestContext, int mshlflags, out IntPtr ppMarshal);

		// Token: 0x0400033B RID: 827
		private const int ERROR_SUCCESS = 0;

		// Token: 0x0400033C RID: 828
		private const int ERROR_INSUFFICIENT_BUFFER = 122;

		// Token: 0x0400033D RID: 829
		private const int ERROR_NO_PACKAGE_IDENTITY = 15700;

		// Token: 0x0400033E RID: 830
		[SecuritySafeCritical]
		internal static Lazy<bool> IsPackagedProcess = new Lazy<bool>(() => UnsafeNativeMethods._IsPackagedProcess());

		// Token: 0x0400033F RID: 831
		public const int FILE_READ_DATA = 1;

		// Token: 0x04000340 RID: 832
		public const int FILE_LIST_DIRECTORY = 1;

		// Token: 0x04000341 RID: 833
		public const int FILE_WRITE_DATA = 2;

		// Token: 0x04000342 RID: 834
		public const int FILE_ADD_FILE = 2;

		// Token: 0x04000343 RID: 835
		public const int FILE_APPEND_DATA = 4;

		// Token: 0x04000344 RID: 836
		public const int FILE_ADD_SUBDIRECTORY = 4;

		// Token: 0x04000345 RID: 837
		public const int FILE_CREATE_PIPE_INSTANCE = 4;

		// Token: 0x04000346 RID: 838
		public const int FILE_READ_EA = 8;

		// Token: 0x04000347 RID: 839
		public const int FILE_WRITE_EA = 16;

		// Token: 0x04000348 RID: 840
		public const int FILE_EXECUTE = 32;

		// Token: 0x04000349 RID: 841
		public const int FILE_TRAVERSE = 32;

		// Token: 0x0400034A RID: 842
		public const int FILE_DELETE_CHILD = 64;

		// Token: 0x0400034B RID: 843
		public const int FILE_READ_ATTRIBUTES = 128;

		// Token: 0x0400034C RID: 844
		public const int FILE_WRITE_ATTRIBUTES = 256;

		// Token: 0x0400034D RID: 845
		public const int FILE_SHARE_READ = 1;

		// Token: 0x0400034E RID: 846
		public const int FILE_SHARE_WRITE = 2;

		// Token: 0x0400034F RID: 847
		public const int FILE_SHARE_DELETE = 4;

		// Token: 0x04000350 RID: 848
		public const int FILE_ATTRIBUTE_READONLY = 1;

		// Token: 0x04000351 RID: 849
		public const int FILE_ATTRIBUTE_HIDDEN = 2;

		// Token: 0x04000352 RID: 850
		public const int FILE_ATTRIBUTE_SYSTEM = 4;

		// Token: 0x04000353 RID: 851
		public const int FILE_ATTRIBUTE_DIRECTORY = 16;

		// Token: 0x04000354 RID: 852
		public const int FILE_ATTRIBUTE_ARCHIVE = 32;

		// Token: 0x04000355 RID: 853
		public const int FILE_ATTRIBUTE_NORMAL = 128;

		// Token: 0x04000356 RID: 854
		public const int FILE_ATTRIBUTE_TEMPORARY = 256;

		// Token: 0x04000357 RID: 855
		public const int FILE_ATTRIBUTE_COMPRESSED = 2048;

		// Token: 0x04000358 RID: 856
		public const int FILE_ATTRIBUTE_OFFLINE = 4096;

		// Token: 0x04000359 RID: 857
		public const int FILE_NOTIFY_CHANGE_FILE_NAME = 1;

		// Token: 0x0400035A RID: 858
		public const int FILE_NOTIFY_CHANGE_DIR_NAME = 2;

		// Token: 0x0400035B RID: 859
		public const int FILE_NOTIFY_CHANGE_ATTRIBUTES = 4;

		// Token: 0x0400035C RID: 860
		public const int FILE_NOTIFY_CHANGE_SIZE = 8;

		// Token: 0x0400035D RID: 861
		public const int FILE_NOTIFY_CHANGE_LAST_WRITE = 16;

		// Token: 0x0400035E RID: 862
		public const int FILE_NOTIFY_CHANGE_LAST_ACCESS = 32;

		// Token: 0x0400035F RID: 863
		public const int FILE_NOTIFY_CHANGE_CREATION = 64;

		// Token: 0x04000360 RID: 864
		public const int FILE_NOTIFY_CHANGE_SECURITY = 256;

		// Token: 0x04000361 RID: 865
		public const int FILE_ACTION_ADDED = 1;

		// Token: 0x04000362 RID: 866
		public const int FILE_ACTION_REMOVED = 2;

		// Token: 0x04000363 RID: 867
		public const int FILE_ACTION_MODIFIED = 3;

		// Token: 0x04000364 RID: 868
		public const int FILE_ACTION_RENAMED_OLD_NAME = 4;

		// Token: 0x04000365 RID: 869
		public const int FILE_ACTION_RENAMED_NEW_NAME = 5;

		// Token: 0x04000366 RID: 870
		public const int FILE_CASE_SENSITIVE_SEARCH = 1;

		// Token: 0x04000367 RID: 871
		public const int FILE_CASE_PRESERVED_NAMES = 2;

		// Token: 0x04000368 RID: 872
		public const int FILE_UNICODE_ON_DISK = 4;

		// Token: 0x04000369 RID: 873
		public const int FILE_PERSISTENT_ACLS = 8;

		// Token: 0x0400036A RID: 874
		public const int FILE_FILE_COMPRESSION = 16;

		// Token: 0x0400036B RID: 875
		public const int OPEN_EXISTING = 3;

		// Token: 0x0400036C RID: 876
		public const int OPEN_ALWAYS = 4;

		// Token: 0x0400036D RID: 877
		public const int FILE_FLAG_WRITE_THROUGH = -2147483648;

		// Token: 0x0400036E RID: 878
		public const int FILE_FLAG_OVERLAPPED = 1073741824;

		// Token: 0x0400036F RID: 879
		public const int FILE_FLAG_NO_BUFFERING = 536870912;

		// Token: 0x04000370 RID: 880
		public const int FILE_FLAG_RANDOM_ACCESS = 268435456;

		// Token: 0x04000371 RID: 881
		public const int FILE_FLAG_SEQUENTIAL_SCAN = 134217728;

		// Token: 0x04000372 RID: 882
		public const int FILE_FLAG_DELETE_ON_CLOSE = 67108864;

		// Token: 0x04000373 RID: 883
		public const int FILE_FLAG_BACKUP_SEMANTICS = 33554432;

		// Token: 0x04000374 RID: 884
		public const int FILE_FLAG_POSIX_SEMANTICS = 16777216;

		// Token: 0x04000375 RID: 885
		public const int FILE_TYPE_UNKNOWN = 0;

		// Token: 0x04000376 RID: 886
		public const int FILE_TYPE_DISK = 1;

		// Token: 0x04000377 RID: 887
		public const int FILE_TYPE_CHAR = 2;

		// Token: 0x04000378 RID: 888
		public const int FILE_TYPE_PIPE = 3;

		// Token: 0x04000379 RID: 889
		public const int FILE_TYPE_REMOTE = 32768;

		// Token: 0x0400037A RID: 890
		public const int FILE_VOLUME_IS_COMPRESSED = 32768;

		// Token: 0x0400037B RID: 891
		public const int GetFileExInfoStandard = 0;

		// Token: 0x0400037C RID: 892
		internal const int TokenIsAppContainer = 29;

		// Token: 0x020006D2 RID: 1746
		private enum AppPolicyClrCompat
		{
			// Token: 0x04002FAC RID: 12204
			AppPolicyClrCompat_Others,
			// Token: 0x04002FAD RID: 12205
			AppPolicyClrCompat_ClassicDesktop,
			// Token: 0x04002FAE RID: 12206
			AppPolicyClrCompat_Universal,
			// Token: 0x04002FAF RID: 12207
			AppPolicyClrCompat_PackagedDesktop
		}

		// Token: 0x020006D3 RID: 1747
		public struct WIN32_FILE_ATTRIBUTE_DATA
		{
			// Token: 0x04002FB0 RID: 12208
			internal int fileAttributes;

			// Token: 0x04002FB1 RID: 12209
			internal uint ftCreationTimeLow;

			// Token: 0x04002FB2 RID: 12210
			internal uint ftCreationTimeHigh;

			// Token: 0x04002FB3 RID: 12211
			internal uint ftLastAccessTimeLow;

			// Token: 0x04002FB4 RID: 12212
			internal uint ftLastAccessTimeHigh;

			// Token: 0x04002FB5 RID: 12213
			internal uint ftLastWriteTimeLow;

			// Token: 0x04002FB6 RID: 12214
			internal uint ftLastWriteTimeHigh;

			// Token: 0x04002FB7 RID: 12215
			internal uint fileSizeHigh;

			// Token: 0x04002FB8 RID: 12216
			internal uint fileSizeLow;
		}

		// Token: 0x020006D4 RID: 1748
		internal struct DCB
		{
			// Token: 0x04002FB9 RID: 12217
			public uint DCBlength;

			// Token: 0x04002FBA RID: 12218
			public uint BaudRate;

			// Token: 0x04002FBB RID: 12219
			public uint Flags;

			// Token: 0x04002FBC RID: 12220
			public ushort wReserved;

			// Token: 0x04002FBD RID: 12221
			public ushort XonLim;

			// Token: 0x04002FBE RID: 12222
			public ushort XoffLim;

			// Token: 0x04002FBF RID: 12223
			public byte ByteSize;

			// Token: 0x04002FC0 RID: 12224
			public byte Parity;

			// Token: 0x04002FC1 RID: 12225
			public byte StopBits;

			// Token: 0x04002FC2 RID: 12226
			public byte XonChar;

			// Token: 0x04002FC3 RID: 12227
			public byte XoffChar;

			// Token: 0x04002FC4 RID: 12228
			public byte ErrorChar;

			// Token: 0x04002FC5 RID: 12229
			public byte EofChar;

			// Token: 0x04002FC6 RID: 12230
			public byte EvtChar;

			// Token: 0x04002FC7 RID: 12231
			public ushort wReserved1;
		}

		// Token: 0x020006D5 RID: 1749
		internal struct COMSTAT
		{
			// Token: 0x04002FC8 RID: 12232
			public uint Flags;

			// Token: 0x04002FC9 RID: 12233
			public uint cbInQue;

			// Token: 0x04002FCA RID: 12234
			public uint cbOutQue;
		}

		// Token: 0x020006D6 RID: 1750
		internal struct COMMTIMEOUTS
		{
			// Token: 0x04002FCB RID: 12235
			public int ReadIntervalTimeout;

			// Token: 0x04002FCC RID: 12236
			public int ReadTotalTimeoutMultiplier;

			// Token: 0x04002FCD RID: 12237
			public int ReadTotalTimeoutConstant;

			// Token: 0x04002FCE RID: 12238
			public int WriteTotalTimeoutMultiplier;

			// Token: 0x04002FCF RID: 12239
			public int WriteTotalTimeoutConstant;
		}

		// Token: 0x020006D7 RID: 1751
		internal struct COMMPROP
		{
			// Token: 0x04002FD0 RID: 12240
			public ushort wPacketLength;

			// Token: 0x04002FD1 RID: 12241
			public ushort wPacketVersion;

			// Token: 0x04002FD2 RID: 12242
			public int dwServiceMask;

			// Token: 0x04002FD3 RID: 12243
			public int dwReserved1;

			// Token: 0x04002FD4 RID: 12244
			public int dwMaxTxQueue;

			// Token: 0x04002FD5 RID: 12245
			public int dwMaxRxQueue;

			// Token: 0x04002FD6 RID: 12246
			public int dwMaxBaud;

			// Token: 0x04002FD7 RID: 12247
			public int dwProvSubType;

			// Token: 0x04002FD8 RID: 12248
			public int dwProvCapabilities;

			// Token: 0x04002FD9 RID: 12249
			public int dwSettableParams;

			// Token: 0x04002FDA RID: 12250
			public int dwSettableBaud;

			// Token: 0x04002FDB RID: 12251
			public ushort wSettableData;

			// Token: 0x04002FDC RID: 12252
			public ushort wSettableStopParity;

			// Token: 0x04002FDD RID: 12253
			public int dwCurrentTxQueue;

			// Token: 0x04002FDE RID: 12254
			public int dwCurrentRxQueue;

			// Token: 0x04002FDF RID: 12255
			public int dwProvSpec1;

			// Token: 0x04002FE0 RID: 12256
			public int dwProvSpec2;

			// Token: 0x04002FE1 RID: 12257
			public char wcProvChar;
		}

		// Token: 0x020006D8 RID: 1752
		[Guid("00000003-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IMarshal
		{
			// Token: 0x06003FFA RID: 16378
			[PreserveSig]
			int GetUnmarshalClass(ref Guid riid, IntPtr pv, int dwDestContext, IntPtr pvDestContext, int mshlflags, out Guid pCid);

			// Token: 0x06003FFB RID: 16379
			[PreserveSig]
			int GetMarshalSizeMax(ref Guid riid, IntPtr pv, int dwDestContext, IntPtr pvDestContext, int mshlflags, out int pSize);

			// Token: 0x06003FFC RID: 16380
			[PreserveSig]
			int MarshalInterface(IntPtr pStm, ref Guid riid, IntPtr pv, int dwDestContext, IntPtr pvDestContext, int mshlflags);

			// Token: 0x06003FFD RID: 16381
			[PreserveSig]
			int UnmarshalInterface(IntPtr pStm, ref Guid riid, out IntPtr ppv);

			// Token: 0x06003FFE RID: 16382
			[PreserveSig]
			int ReleaseMarshalData(IntPtr pStm);

			// Token: 0x06003FFF RID: 16383
			[PreserveSig]
			int DisconnectObject(int dwReserved);
		}
	}
}
