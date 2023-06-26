using System;
using System.Drawing;
using System.Internal;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms.Automation;
using Accessibility;

namespace System.Windows.Forms
{
	// Token: 0x0200010B RID: 267
	[SuppressUnmanagedCodeSecurity]
	internal static class UnsafeNativeMethods
	{
		// Token: 0x0600053C RID: 1340
		[DllImport("shlwapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern uint SHLoadIndirectString(string pszSource, StringBuilder pszOutBuf, uint cchOutBuf, IntPtr ppvReserved);

		// Token: 0x0600053D RID: 1341
		[DllImport("ole32.dll")]
		public static extern int ReadClassStg(HandleRef pStg, [In] [Out] ref Guid pclsid);

		// Token: 0x0600053E RID: 1342
		[DllImport("ole32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		internal static extern void CoTaskMemFree(IntPtr pv);

		// Token: 0x0600053F RID: 1343
		[DllImport("user32.dll")]
		public static extern int GetClassName(HandleRef hwnd, StringBuilder lpClassName, int nMaxCount);

		// Token: 0x06000540 RID: 1344 RVA: 0x0001244A File Offset: 0x0001064A
		public static IntPtr SetClassLong(HandleRef hWnd, int nIndex, IntPtr dwNewLong)
		{
			if (IntPtr.Size == 4)
			{
				return UnsafeNativeMethods.SetClassLongPtr32(hWnd, nIndex, dwNewLong);
			}
			return UnsafeNativeMethods.SetClassLongPtr64(hWnd, nIndex, dwNewLong);
		}

		// Token: 0x06000541 RID: 1345
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SetClassLong")]
		public static extern IntPtr SetClassLongPtr32(HandleRef hwnd, int nIndex, IntPtr dwNewLong);

		// Token: 0x06000542 RID: 1346
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SetClassLongPtr")]
		public static extern IntPtr SetClassLongPtr64(HandleRef hwnd, int nIndex, IntPtr dwNewLong);

		// Token: 0x06000543 RID: 1347
		[DllImport("ole32.dll", ExactSpelling = true, PreserveSig = false)]
		public static extern UnsafeNativeMethods.IClassFactory2 CoGetClassObject([In] ref Guid clsid, int dwContext, int serverInfo, [In] ref Guid refiid);

		// Token: 0x06000544 RID: 1348
		[DllImport("ole32.dll", ExactSpelling = true, PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.Interface)]
		public static extern object CoCreateInstance([In] ref Guid clsid, [MarshalAs(UnmanagedType.Interface)] object punkOuter, int context, [In] ref Guid iid);

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x00012468 File Offset: 0x00010668
		internal static bool IsVista
		{
			get
			{
				OperatingSystem osversion = Environment.OSVersion;
				return osversion != null && osversion.Platform == PlatformID.Win32NT && osversion.Version.CompareTo(UnsafeNativeMethods.VistaOSVersion) >= 0;
			}
		}

		// Token: 0x06000546 RID: 1350
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern int GetLocaleInfo(int Locale, int LCType, StringBuilder lpLCData, int cchData);

		// Token: 0x06000547 RID: 1351
		[DllImport("ole32.dll")]
		public static extern int WriteClassStm(UnsafeNativeMethods.IStream pStream, ref Guid clsid);

		// Token: 0x06000548 RID: 1352
		[DllImport("ole32.dll")]
		public static extern int ReadClassStg(UnsafeNativeMethods.IStorage pStorage, out Guid clsid);

		// Token: 0x06000549 RID: 1353
		[DllImport("ole32.dll")]
		public static extern int ReadClassStm(UnsafeNativeMethods.IStream pStream, out Guid clsid);

		// Token: 0x0600054A RID: 1354
		[DllImport("ole32.dll")]
		public static extern int OleLoadFromStream(UnsafeNativeMethods.IStream pStorage, ref Guid iid, out UnsafeNativeMethods.IOleObject pObject);

		// Token: 0x0600054B RID: 1355
		[DllImport("ole32.dll")]
		public static extern int OleSaveToStream(UnsafeNativeMethods.IPersistStream pPersistStream, UnsafeNativeMethods.IStream pStream);

		// Token: 0x0600054C RID: 1356
		[DllImport("ole32.dll")]
		public static extern int CoGetMalloc(int dwReserved, out UnsafeNativeMethods.IMalloc pMalloc);

		// Token: 0x0600054D RID: 1357
		[DllImport("comdlg32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool PageSetupDlg([In] [Out] NativeMethods.PAGESETUPDLG lppsd);

		// Token: 0x0600054E RID: 1358
		[DllImport("comdlg32.dll", CharSet = CharSet.Auto, EntryPoint = "PrintDlg", SetLastError = true)]
		public static extern bool PrintDlg_32([In] [Out] NativeMethods.PRINTDLG_32 lppd);

		// Token: 0x0600054F RID: 1359
		[DllImport("comdlg32.dll", CharSet = CharSet.Auto, EntryPoint = "PrintDlg", SetLastError = true)]
		public static extern bool PrintDlg_64([In] [Out] NativeMethods.PRINTDLG_64 lppd);

		// Token: 0x06000550 RID: 1360 RVA: 0x000124A4 File Offset: 0x000106A4
		public static bool PrintDlg([In] [Out] NativeMethods.PRINTDLG lppd)
		{
			if (IntPtr.Size == 4)
			{
				NativeMethods.PRINTDLG_32 printdlg_ = lppd as NativeMethods.PRINTDLG_32;
				if (printdlg_ == null)
				{
					throw new NullReferenceException("PRINTDLG data is null");
				}
				return UnsafeNativeMethods.PrintDlg_32(printdlg_);
			}
			else
			{
				NativeMethods.PRINTDLG_64 printdlg_2 = lppd as NativeMethods.PRINTDLG_64;
				if (printdlg_2 == null)
				{
					throw new NullReferenceException("PRINTDLG data is null");
				}
				return UnsafeNativeMethods.PrintDlg_64(printdlg_2);
			}
		}

		// Token: 0x06000551 RID: 1361
		[DllImport("comdlg32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int PrintDlgEx([In] [Out] NativeMethods.PRINTDLGEX lppdex);

		// Token: 0x06000552 RID: 1362
		[DllImport("ole32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int OleGetClipboard(ref IDataObject data);

		// Token: 0x06000553 RID: 1363
		[DllImport("ole32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int OleSetClipboard(IDataObject pDataObj);

		// Token: 0x06000554 RID: 1364
		[DllImport("ole32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int OleFlushClipboard();

		// Token: 0x06000555 RID: 1365
		[DllImport("oleaut32.dll", ExactSpelling = true)]
		public static extern void OleCreatePropertyFrameIndirect(NativeMethods.OCPFIPARAMS p);

		// Token: 0x06000556 RID: 1366
		[DllImport("oleaut32.dll", EntryPoint = "OleCreateFontIndirect", ExactSpelling = true, PreserveSig = false)]
		public static extern UnsafeNativeMethods.IFont OleCreateIFontIndirect(NativeMethods.FONTDESC fd, ref Guid iid);

		// Token: 0x06000557 RID: 1367
		[DllImport("oleaut32.dll", EntryPoint = "OleCreatePictureIndirect", ExactSpelling = true, PreserveSig = false)]
		public static extern UnsafeNativeMethods.IPicture OleCreateIPictureIndirect([MarshalAs(UnmanagedType.AsAny)] object pictdesc, ref Guid iid, bool fOwn);

		// Token: 0x06000558 RID: 1368
		[DllImport("oleaut32.dll", EntryPoint = "OleCreatePictureIndirect", ExactSpelling = true, PreserveSig = false)]
		public static extern UnsafeNativeMethods.IPictureDisp OleCreateIPictureDispIndirect([MarshalAs(UnmanagedType.AsAny)] object pictdesc, ref Guid iid, bool fOwn);

		// Token: 0x06000559 RID: 1369
		[DllImport("oleaut32.dll", PreserveSig = false)]
		public static extern UnsafeNativeMethods.IPicture OleCreatePictureIndirect(NativeMethods.PICTDESC pictdesc, [In] ref Guid refiid, bool fOwn);

		// Token: 0x0600055A RID: 1370
		[DllImport("oleaut32.dll", PreserveSig = false)]
		public static extern UnsafeNativeMethods.IFont OleCreateFontIndirect(NativeMethods.tagFONTDESC fontdesc, [In] ref Guid refiid);

		// Token: 0x0600055B RID: 1371
		[DllImport("oleaut32.dll", ExactSpelling = true)]
		public static extern int VarFormat(ref object pvarIn, HandleRef pstrFormat, int iFirstDay, int iFirstWeek, uint dwFlags, [In] [Out] ref IntPtr pbstr);

		// Token: 0x0600055C RID: 1372
		[DllImport("shell32.dll", CharSet = CharSet.Auto)]
		public static extern int DragQueryFile(HandleRef hDrop, int iFile, StringBuilder lpszFile, int cch);

		// Token: 0x0600055D RID: 1373 RVA: 0x000124F0 File Offset: 0x000106F0
		public static int DragQueryFileLongPath(HandleRef hDrop, int iFile, StringBuilder lpszFile)
		{
			if (lpszFile != null && lpszFile.Capacity != 0 && iFile != -1)
			{
				int num;
				if ((num = UnsafeNativeMethods.DragQueryFile(hDrop, iFile, lpszFile, lpszFile.Capacity)) == lpszFile.Capacity)
				{
					int num2 = UnsafeNativeMethods.DragQueryFile(hDrop, iFile, null, 0);
					if (num2 < 32767)
					{
						lpszFile.EnsureCapacity(num2);
						num = UnsafeNativeMethods.DragQueryFile(hDrop, iFile, lpszFile, num2);
					}
					else
					{
						num = 0;
					}
				}
				lpszFile.Length = num;
				return num;
			}
			return UnsafeNativeMethods.DragQueryFile(hDrop, iFile, lpszFile, lpszFile.Capacity);
		}

		// Token: 0x0600055E RID: 1374
		[DllImport("user32.dll", ExactSpelling = true)]
		public static extern bool EnumChildWindows(HandleRef hwndParent, NativeMethods.EnumChildrenCallback lpEnumFunc, HandleRef lParam);

		// Token: 0x0600055F RID: 1375
		[DllImport("shell32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr ShellExecute(HandleRef hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, int nShowCmd);

		// Token: 0x06000560 RID: 1376
		[DllImport("shell32.dll", BestFitMapping = false, CharSet = CharSet.Auto, EntryPoint = "ShellExecute")]
		public static extern IntPtr ShellExecute_NoBFM(HandleRef hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, int nShowCmd);

		// Token: 0x06000561 RID: 1377
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int SetScrollPos(HandleRef hWnd, int nBar, int nPos, bool bRedraw);

		// Token: 0x06000562 RID: 1378
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern bool EnableScrollBar(HandleRef hWnd, int nBar, int value);

		// Token: 0x06000563 RID: 1379
		[DllImport("shell32.dll", CharSet = CharSet.Auto)]
		public static extern int Shell_NotifyIcon(int message, NativeMethods.NOTIFYICONDATA pnid);

		// Token: 0x06000564 RID: 1380
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern bool InsertMenuItem(HandleRef hMenu, int uItem, bool fByPosition, NativeMethods.MENUITEMINFO_T lpmii);

		// Token: 0x06000565 RID: 1381
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr GetMenu(HandleRef hWnd);

		// Token: 0x06000566 RID: 1382
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern bool GetMenuItemInfo(HandleRef hMenu, int uItem, bool fByPosition, [In] [Out] NativeMethods.MENUITEMINFO_T lpmii);

		// Token: 0x06000567 RID: 1383
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern bool GetMenuItemInfo(HandleRef hMenu, int uItem, bool fByPosition, [In] [Out] NativeMethods.MENUITEMINFO_T_RW lpmii);

		// Token: 0x06000568 RID: 1384
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern bool SetMenuItemInfo(HandleRef hMenu, int uItem, bool fByPosition, NativeMethods.MENUITEMINFO_T lpmii);

		// Token: 0x06000569 RID: 1385
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateMenu", ExactSpelling = true)]
		private static extern IntPtr IntCreateMenu();

		// Token: 0x0600056A RID: 1386 RVA: 0x00012565 File Offset: 0x00010765
		public static IntPtr CreateMenu()
		{
			return System.Internal.HandleCollector.Add(UnsafeNativeMethods.IntCreateMenu(), NativeMethods.CommonHandles.Menu);
		}

		// Token: 0x0600056B RID: 1387
		[DllImport("comdlg32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool GetOpenFileName([In] [Out] NativeMethods.OPENFILENAME_I ofn);

		// Token: 0x0600056C RID: 1388
		[DllImport("user32.dll", ExactSpelling = true)]
		public static extern bool EndDialog(HandleRef hWnd, IntPtr result);

		// Token: 0x0600056D RID: 1389
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		public static extern int MultiByteToWideChar(int CodePage, int dwFlags, byte[] lpMultiByteStr, int cchMultiByte, char[] lpWideCharStr, int cchWideChar);

		// Token: 0x0600056E RID: 1390
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int WideCharToMultiByte(int codePage, int flags, [MarshalAs(UnmanagedType.LPWStr)] string wideStr, int chars, [In] [Out] byte[] pOutBytes, int bufferBytes, IntPtr defaultChar, IntPtr pDefaultUsed);

		// Token: 0x0600056F RID: 1391
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, EntryPoint = "RtlMoveMemory", ExactSpelling = true, SetLastError = true)]
		public static extern void CopyMemory(HandleRef destData, HandleRef srcData, int size);

		// Token: 0x06000570 RID: 1392
		[DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory", ExactSpelling = true)]
		public static extern void CopyMemory(IntPtr pdst, byte[] psrc, int cb);

		// Token: 0x06000571 RID: 1393
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "RtlMoveMemory", ExactSpelling = true)]
		public static extern void CopyMemoryW(IntPtr pdst, string psrc, int cb);

		// Token: 0x06000572 RID: 1394
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "RtlMoveMemory", ExactSpelling = true)]
		public static extern void CopyMemoryW(IntPtr pdst, char[] psrc, int cb);

		// Token: 0x06000573 RID: 1395
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, EntryPoint = "RtlMoveMemory", ExactSpelling = true)]
		public static extern void CopyMemoryA(IntPtr pdst, string psrc, int cb);

		// Token: 0x06000574 RID: 1396
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, EntryPoint = "RtlMoveMemory", ExactSpelling = true)]
		public static extern void CopyMemoryA(IntPtr pdst, char[] psrc, int cb);

		// Token: 0x06000575 RID: 1397
		[DllImport("kernel32.dll", EntryPoint = "DuplicateHandle", ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr IntDuplicateHandle(HandleRef processSource, HandleRef handleSource, HandleRef processTarget, ref IntPtr handleTarget, int desiredAccess, bool inheritHandle, int options);

		// Token: 0x06000576 RID: 1398 RVA: 0x00012578 File Offset: 0x00010778
		public static IntPtr DuplicateHandle(HandleRef processSource, HandleRef handleSource, HandleRef processTarget, ref IntPtr handleTarget, int desiredAccess, bool inheritHandle, int options)
		{
			IntPtr intPtr = UnsafeNativeMethods.IntDuplicateHandle(processSource, handleSource, processTarget, ref handleTarget, desiredAccess, inheritHandle, options);
			System.Internal.HandleCollector.Add(handleTarget, NativeMethods.CommonHandles.Kernel);
			return intPtr;
		}

		// Token: 0x06000577 RID: 1399
		[DllImport("ole32.dll", PreserveSig = false)]
		public static extern UnsafeNativeMethods.IStorage StgOpenStorageOnILockBytes(UnsafeNativeMethods.ILockBytes iLockBytes, UnsafeNativeMethods.IStorage pStgPriority, int grfMode, int sndExcluded, int reserved);

		// Token: 0x06000578 RID: 1400
		[DllImport("ole32.dll", PreserveSig = false)]
		public static extern IntPtr GetHGlobalFromILockBytes(UnsafeNativeMethods.ILockBytes pLkbyt);

		// Token: 0x06000579 RID: 1401
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SetWindowsHookEx(int hookid, NativeMethods.HookProc pfnhook, HandleRef hinst, int threadid);

		// Token: 0x0600057A RID: 1402
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int GetKeyboardState(byte[] keystate);

		// Token: 0x0600057B RID: 1403
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "keybd_event", ExactSpelling = true)]
		public static extern void Keybd_event(byte vk, byte scan, int flags, int extrainfo);

		// Token: 0x0600057C RID: 1404
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int SetKeyboardState(byte[] keystate);

		// Token: 0x0600057D RID: 1405
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool UnhookWindowsHookEx(HandleRef hhook);

		// Token: 0x0600057E RID: 1406
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern short GetAsyncKeyState(int vkey);

		// Token: 0x0600057F RID: 1407
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr CallNextHookEx(HandleRef hhook, int code, IntPtr wparam, IntPtr lparam);

		// Token: 0x06000580 RID: 1408
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int ScreenToClient(HandleRef hWnd, [In] [Out] NativeMethods.POINT pt);

		// Token: 0x06000581 RID: 1409
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetModuleFileName(HandleRef hModule, StringBuilder buffer, int length);

		// Token: 0x06000582 RID: 1410 RVA: 0x000125A4 File Offset: 0x000107A4
		public static StringBuilder GetModuleFileNameLongPath(HandleRef hModule)
		{
			StringBuilder stringBuilder = new StringBuilder(260);
			int num = 1;
			int moduleFileName;
			while ((moduleFileName = UnsafeNativeMethods.GetModuleFileName(hModule, stringBuilder, stringBuilder.Capacity)) == stringBuilder.Capacity && Marshal.GetLastWin32Error() == 122 && stringBuilder.Capacity < 32767)
			{
				num += 2;
				int num2 = ((num * 260 < 32767) ? (num * 260) : 32767);
				stringBuilder.EnsureCapacity(num2);
			}
			stringBuilder.Length = moduleFileName;
			return stringBuilder;
		}

		// Token: 0x06000583 RID: 1411
		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		public static extern bool IsDialogMessage(HandleRef hWndDlg, [In] [Out] ref NativeMethods.MSG msg);

		// Token: 0x06000584 RID: 1412
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool TranslateMessage([In] [Out] ref NativeMethods.MSG msg);

		// Token: 0x06000585 RID: 1413
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr DispatchMessage([In] ref NativeMethods.MSG msg);

		// Token: 0x06000586 RID: 1414
		[DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern IntPtr DispatchMessageA([In] ref NativeMethods.MSG msg);

		// Token: 0x06000587 RID: 1415
		[DllImport("user32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern IntPtr DispatchMessageW([In] ref NativeMethods.MSG msg);

		// Token: 0x06000588 RID: 1416
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int PostThreadMessage(int id, int msg, IntPtr wparam, IntPtr lparam);

		// Token: 0x06000589 RID: 1417
		[DllImport("ole32.dll", ExactSpelling = true)]
		public static extern int CoRegisterMessageFilter(HandleRef newFilter, ref IntPtr oldMsgFilter);

		// Token: 0x0600058A RID: 1418
		[DllImport("ole32.dll", EntryPoint = "OleInitialize", ExactSpelling = true, SetLastError = true)]
		private static extern int IntOleInitialize(int val);

		// Token: 0x0600058B RID: 1419 RVA: 0x00012620 File Offset: 0x00010820
		public static int OleInitialize()
		{
			return UnsafeNativeMethods.IntOleInitialize(0);
		}

		// Token: 0x0600058C RID: 1420
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool EnumThreadWindows(int dwThreadId, NativeMethods.EnumThreadWindowsCallback lpfn, HandleRef lParam);

		// Token: 0x0600058D RID: 1421
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetExitCodeThread(IntPtr hThread, out uint lpExitCode);

		// Token: 0x0600058E RID: 1422
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendDlgItemMessage(HandleRef hDlg, int nIDDlgItem, int Msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x0600058F RID: 1423
		[DllImport("ole32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int OleUninitialize();

		// Token: 0x06000590 RID: 1424
		[DllImport("comdlg32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool GetSaveFileName([In] [Out] NativeMethods.OPENFILENAME_I ofn);

		// Token: 0x06000591 RID: 1425
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "ChildWindowFromPointEx", ExactSpelling = true)]
		private static extern IntPtr _ChildWindowFromPointEx(HandleRef hwndParent, UnsafeNativeMethods.POINTSTRUCT pt, int uFlags);

		// Token: 0x06000592 RID: 1426 RVA: 0x00012628 File Offset: 0x00010828
		public static IntPtr ChildWindowFromPointEx(HandleRef hwndParent, int x, int y, int uFlags)
		{
			UnsafeNativeMethods.POINTSTRUCT pointstruct = new UnsafeNativeMethods.POINTSTRUCT(x, y);
			return UnsafeNativeMethods._ChildWindowFromPointEx(hwndParent, pointstruct, uFlags);
		}

		// Token: 0x06000593 RID: 1427
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, EntryPoint = "CloseHandle", ExactSpelling = true, SetLastError = true)]
		private static extern bool IntCloseHandle(HandleRef handle);

		// Token: 0x06000594 RID: 1428 RVA: 0x00012646 File Offset: 0x00010846
		public static bool CloseHandle(HandleRef handle)
		{
			System.Internal.HandleCollector.Remove((IntPtr)handle, NativeMethods.CommonHandles.Kernel);
			return UnsafeNativeMethods.IntCloseHandle(handle);
		}

		// Token: 0x06000595 RID: 1429
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateCompatibleDC", ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr IntCreateCompatibleDC(HandleRef hDC);

		// Token: 0x06000596 RID: 1430 RVA: 0x0001265F File Offset: 0x0001085F
		public static IntPtr CreateCompatibleDC(HandleRef hDC)
		{
			return System.Internal.HandleCollector.Add(UnsafeNativeMethods.IntCreateCompatibleDC(hDC), NativeMethods.CommonHandles.CompatibleHDC);
		}

		// Token: 0x06000597 RID: 1431
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool BlockInput([MarshalAs(UnmanagedType.Bool)] [In] bool fBlockIt);

		// Token: 0x06000598 RID: 1432
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern uint SendInput(uint nInputs, NativeMethods.INPUT[] pInputs, int cbSize);

		// Token: 0x06000599 RID: 1433
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, EntryPoint = "MapViewOfFile", ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr IntMapViewOfFile(HandleRef hFileMapping, int dwDesiredAccess, int dwFileOffsetHigh, int dwFileOffsetLow, int dwNumberOfBytesToMap);

		// Token: 0x0600059A RID: 1434 RVA: 0x00012671 File Offset: 0x00010871
		public static IntPtr MapViewOfFile(HandleRef hFileMapping, int dwDesiredAccess, int dwFileOffsetHigh, int dwFileOffsetLow, int dwNumberOfBytesToMap)
		{
			return System.Internal.HandleCollector.Add(UnsafeNativeMethods.IntMapViewOfFile(hFileMapping, dwDesiredAccess, dwFileOffsetHigh, dwFileOffsetLow, dwNumberOfBytesToMap), NativeMethods.CommonHandles.Kernel);
		}

		// Token: 0x0600059B RID: 1435
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, EntryPoint = "UnmapViewOfFile", ExactSpelling = true, SetLastError = true)]
		private static extern bool IntUnmapViewOfFile(HandleRef pvBaseAddress);

		// Token: 0x0600059C RID: 1436 RVA: 0x00012688 File Offset: 0x00010888
		public static bool UnmapViewOfFile(HandleRef pvBaseAddress)
		{
			System.Internal.HandleCollector.Remove((IntPtr)pvBaseAddress, NativeMethods.CommonHandles.Kernel);
			return UnsafeNativeMethods.IntUnmapViewOfFile(pvBaseAddress);
		}

		// Token: 0x0600059D RID: 1437
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "GetDCEx", ExactSpelling = true)]
		private static extern IntPtr IntGetDCEx(HandleRef hWnd, HandleRef hrgnClip, int flags);

		// Token: 0x0600059E RID: 1438 RVA: 0x000126A1 File Offset: 0x000108A1
		public static IntPtr GetDCEx(HandleRef hWnd, HandleRef hrgnClip, int flags)
		{
			return System.Internal.HandleCollector.Add(UnsafeNativeMethods.IntGetDCEx(hWnd, hrgnClip, flags), NativeMethods.CommonHandles.HDC);
		}

		// Token: 0x0600059F RID: 1439
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetObject(HandleRef hObject, int nSize, [In] [Out] NativeMethods.BITMAP bm);

		// Token: 0x060005A0 RID: 1440
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetObject(HandleRef hObject, int nSize, [In] [Out] NativeMethods.LOGPEN lp);

		// Token: 0x060005A1 RID: 1441 RVA: 0x000126B5 File Offset: 0x000108B5
		public static int GetObject(HandleRef hObject, NativeMethods.LOGPEN lp)
		{
			return UnsafeNativeMethods.GetObject(hObject, Marshal.SizeOf(typeof(NativeMethods.LOGPEN)), lp);
		}

		// Token: 0x060005A2 RID: 1442
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetObject(HandleRef hObject, int nSize, [In] [Out] NativeMethods.LOGBRUSH lb);

		// Token: 0x060005A3 RID: 1443 RVA: 0x000126CD File Offset: 0x000108CD
		public static int GetObject(HandleRef hObject, NativeMethods.LOGBRUSH lb)
		{
			return UnsafeNativeMethods.GetObject(hObject, Marshal.SizeOf(typeof(NativeMethods.LOGBRUSH)), lb);
		}

		// Token: 0x060005A4 RID: 1444
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetObject(HandleRef hObject, int nSize, [In] [Out] NativeMethods.LOGFONT lf);

		// Token: 0x060005A5 RID: 1445 RVA: 0x000126E5 File Offset: 0x000108E5
		public static int GetObject(HandleRef hObject, NativeMethods.LOGFONT lp)
		{
			return UnsafeNativeMethods.GetObject(hObject, Marshal.SizeOf(typeof(NativeMethods.LOGFONT)), lp);
		}

		// Token: 0x060005A6 RID: 1446
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetObject(HandleRef hObject, int nSize, ref int nEntries);

		// Token: 0x060005A7 RID: 1447
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetObject(HandleRef hObject, int nSize, int[] nEntries);

		// Token: 0x060005A8 RID: 1448
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int GetObjectType(HandleRef hObject);

		// Token: 0x060005A9 RID: 1449
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateAcceleratorTable")]
		private static extern IntPtr IntCreateAcceleratorTable(HandleRef pentries, int cCount);

		// Token: 0x060005AA RID: 1450 RVA: 0x000126FD File Offset: 0x000108FD
		public static IntPtr CreateAcceleratorTable(HandleRef pentries, int cCount)
		{
			return System.Internal.HandleCollector.Add(UnsafeNativeMethods.IntCreateAcceleratorTable(pentries, cCount), NativeMethods.CommonHandles.Accelerator);
		}

		// Token: 0x060005AB RID: 1451
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "DestroyAcceleratorTable", ExactSpelling = true)]
		private static extern bool IntDestroyAcceleratorTable(HandleRef hAccel);

		// Token: 0x060005AC RID: 1452 RVA: 0x00012710 File Offset: 0x00010910
		public static bool DestroyAcceleratorTable(HandleRef hAccel)
		{
			System.Internal.HandleCollector.Remove((IntPtr)hAccel, NativeMethods.CommonHandles.Accelerator);
			return UnsafeNativeMethods.IntDestroyAcceleratorTable(hAccel);
		}

		// Token: 0x060005AD RID: 1453
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern short VkKeyScan(char key);

		// Token: 0x060005AE RID: 1454
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GetCapture();

		// Token: 0x060005AF RID: 1455
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr SetCapture(HandleRef hwnd);

		// Token: 0x060005B0 RID: 1456
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GetFocus();

		// Token: 0x060005B1 RID: 1457
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool GetCursorPos([In] [Out] NativeMethods.POINT pt);

		// Token: 0x060005B2 RID: 1458
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern short GetKeyState(int keyCode);

		// Token: 0x060005B3 RID: 1459
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern uint GetShortPathName(string lpszLongPath, StringBuilder lpszShortPath, uint cchBuffer);

		// Token: 0x060005B4 RID: 1460
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowRgn", ExactSpelling = true)]
		private static extern int IntSetWindowRgn(HandleRef hwnd, HandleRef hrgn, bool fRedraw);

		// Token: 0x060005B5 RID: 1461 RVA: 0x0001272C File Offset: 0x0001092C
		public static int SetWindowRgn(HandleRef hwnd, HandleRef hrgn, bool fRedraw)
		{
			int num = UnsafeNativeMethods.IntSetWindowRgn(hwnd, hrgn, fRedraw);
			if (num != 0)
			{
				System.Internal.HandleCollector.Remove((IntPtr)hrgn, NativeMethods.CommonHandles.GDI);
			}
			return num;
		}

		// Token: 0x060005B6 RID: 1462
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int GetWindowText(HandleRef hWnd, StringBuilder lpString, int nMaxCount);

		// Token: 0x060005B7 RID: 1463
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern void GetTempFileName(string tempDirName, string prefixName, int unique, StringBuilder sb);

		// Token: 0x060005B8 RID: 1464
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern bool SetWindowText(HandleRef hWnd, string text);

		// Token: 0x060005B9 RID: 1465
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GlobalAlloc(int uFlags, int dwBytes);

		// Token: 0x060005BA RID: 1466
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GlobalReAlloc(HandleRef handle, int bytes, int flags);

		// Token: 0x060005BB RID: 1467
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GlobalLock(HandleRef handle);

		// Token: 0x060005BC RID: 1468
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool GlobalUnlock(HandleRef handle);

		// Token: 0x060005BD RID: 1469
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GlobalFree(HandleRef handle);

		// Token: 0x060005BE RID: 1470
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int GlobalSize(HandleRef handle);

		// Token: 0x060005BF RID: 1471
		[DllImport("imm32.dll", CharSet = CharSet.Auto)]
		public static extern bool ImmSetConversionStatus(HandleRef hIMC, int conversion, int sentence);

		// Token: 0x060005C0 RID: 1472
		[DllImport("imm32.dll", CharSet = CharSet.Auto)]
		public static extern bool ImmGetConversionStatus(HandleRef hIMC, ref int conversion, ref int sentence);

		// Token: 0x060005C1 RID: 1473
		[DllImport("imm32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr ImmGetContext(HandleRef hWnd);

		// Token: 0x060005C2 RID: 1474
		[DllImport("imm32.dll", CharSet = CharSet.Auto)]
		public static extern bool ImmReleaseContext(HandleRef hWnd, HandleRef hIMC);

		// Token: 0x060005C3 RID: 1475
		[DllImport("imm32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr ImmAssociateContext(HandleRef hWnd, HandleRef hIMC);

		// Token: 0x060005C4 RID: 1476
		[DllImport("imm32.dll", CharSet = CharSet.Auto)]
		public static extern bool ImmDestroyContext(HandleRef hIMC);

		// Token: 0x060005C5 RID: 1477
		[DllImport("imm32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr ImmCreateContext();

		// Token: 0x060005C6 RID: 1478
		[DllImport("imm32.dll", CharSet = CharSet.Auto)]
		public static extern bool ImmSetOpenStatus(HandleRef hIMC, bool open);

		// Token: 0x060005C7 RID: 1479
		[DllImport("imm32.dll", CharSet = CharSet.Auto)]
		public static extern bool ImmGetOpenStatus(HandleRef hIMC);

		// Token: 0x060005C8 RID: 1480
		[DllImport("imm32.dll", CharSet = CharSet.Auto)]
		public static extern bool ImmNotifyIME(HandleRef hIMC, int dwAction, int dwIndex, int dwValue);

		// Token: 0x060005C9 RID: 1481
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr SetFocus(HandleRef hWnd);

		// Token: 0x060005CA RID: 1482
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GetParent(HandleRef hWnd);

		// Token: 0x060005CB RID: 1483
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GetAncestor(HandleRef hWnd, int flags);

		// Token: 0x060005CC RID: 1484
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool IsChild(HandleRef hWndParent, HandleRef hwnd);

		// Token: 0x060005CD RID: 1485
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool IsZoomed(HandleRef hWnd);

		// Token: 0x060005CE RID: 1486
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr FindWindow(string className, string windowName);

		// Token: 0x060005CF RID: 1487
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int MapWindowPoints(HandleRef hWndFrom, HandleRef hWndTo, [In] [Out] ref NativeMethods.RECT rect, int cPoints);

		// Token: 0x060005D0 RID: 1488
		[DllImport("user32.dll", ExactSpelling = true)]
		public unsafe static extern int MapWindowPoints(IntPtr hWndFrom, IntPtr hWndTo, Point* lpPoints, uint cPoints);

		// Token: 0x060005D1 RID: 1489 RVA: 0x00012758 File Offset: 0x00010958
		public unsafe static int MapWindowPoint(IntPtr hWndFrom, IntPtr hWndTo, ref Point lpPoints)
		{
			fixed (Point* ptr = &lpPoints)
			{
				Point* ptr2 = ptr;
				int num = UnsafeNativeMethods.MapWindowPoints(hWndFrom, hWndTo, ptr2, 1U);
				GC.KeepAlive(hWndTo);
				return num;
			}
		}

		// Token: 0x060005D2 RID: 1490
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int MapWindowPoints(HandleRef hWndFrom, HandleRef hWndTo, [In] [Out] NativeMethods.POINT pt, int cPoints);

		// Token: 0x060005D3 RID: 1491
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, bool wParam, int lParam);

		// Token: 0x060005D4 RID: 1492
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, int[] lParam);

		// Token: 0x060005D5 RID: 1493
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int[] wParam, int[] lParam);

		// Token: 0x060005D6 RID: 1494
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, ref int wParam, ref int lParam);

		// Token: 0x060005D7 RID: 1495
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, string lParam);

		// Token: 0x060005D8 RID: 1496
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, IntPtr wParam, string lParam);

		// Token: 0x060005D9 RID: 1497
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, StringBuilder lParam);

		// Token: 0x060005DA RID: 1498
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.TOOLINFO_T lParam);

		// Token: 0x060005DB RID: 1499
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.TOOLINFO_TOOLTIP lParam);

		// Token: 0x060005DC RID: 1500
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, ref NativeMethods.TBBUTTON lParam);

		// Token: 0x060005DD RID: 1501
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, ref NativeMethods.TBBUTTONINFO lParam);

		// Token: 0x060005DE RID: 1502
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, ref NativeMethods.TV_ITEM lParam);

		// Token: 0x060005DF RID: 1503
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, ref NativeMethods.TV_INSERTSTRUCT lParam);

		// Token: 0x060005E0 RID: 1504
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.TV_HITTESTINFO lParam);

		// Token: 0x060005E1 RID: 1505
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.LVBKIMAGE lParam);

		// Token: 0x060005E2 RID: 1506
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int SendMessage(HandleRef hWnd, int msg, int wParam, ref NativeMethods.LVHITTESTINFO lParam);

		// Token: 0x060005E3 RID: 1507
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.TCITEM_T lParam);

		// Token: 0x060005E4 RID: 1508
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, ref NativeMethods.HDLAYOUT hdlayout);

		// Token: 0x060005E5 RID: 1509
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, HandleRef wParam, int lParam);

		// Token: 0x060005E6 RID: 1510
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, HandleRef lParam);

		// Token: 0x060005E7 RID: 1511
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPStruct)] [In] [Out] NativeMethods.PARAFORMAT lParam);

		// Token: 0x060005E8 RID: 1512
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPStruct)] [In] [Out] NativeMethods.CHARFORMATA lParam);

		// Token: 0x060005E9 RID: 1513
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPStruct)] [In] [Out] NativeMethods.CHARFORMAT2A lParam);

		// Token: 0x060005EA RID: 1514
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPStruct)] [In] [Out] NativeMethods.CHARFORMATW lParam);

		// Token: 0x060005EB RID: 1515
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int SendMessage(HandleRef hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.IUnknown)] out object editOle);

		// Token: 0x060005EC RID: 1516
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.CHARRANGE lParam);

		// Token: 0x060005ED RID: 1517
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.FINDTEXT lParam);

		// Token: 0x060005EE RID: 1518
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.TEXTRANGE lParam);

		// Token: 0x060005EF RID: 1519
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.POINT lParam);

		// Token: 0x060005F0 RID: 1520
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, NativeMethods.POINT wParam, int lParam);

		// Token: 0x060005F1 RID: 1521
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.REPASTESPECIAL lParam);

		// Token: 0x060005F2 RID: 1522
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.EDITSTREAM lParam);

		// Token: 0x060005F3 RID: 1523
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.EDITSTREAM64 lParam);

		// Token: 0x060005F4 RID: 1524
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, NativeMethods.GETTEXTLENGTHEX wParam, int lParam);

		// Token: 0x060005F5 RID: 1525
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, [In] [Out] NativeMethods.SIZE lParam);

		// Token: 0x060005F6 RID: 1526
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, [In] [Out] ref NativeMethods.LVFINDINFO lParam);

		// Token: 0x060005F7 RID: 1527
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.LVHITTESTINFO lParam);

		// Token: 0x060005F8 RID: 1528
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.LVCOLUMN_T lParam);

		// Token: 0x060005F9 RID: 1529
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, [In] [Out] ref NativeMethods.LVITEM lParam);

		// Token: 0x060005FA RID: 1530
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.LVCOLUMN lParam);

		// Token: 0x060005FB RID: 1531
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.LVGROUP lParam);

		// Token: 0x060005FC RID: 1532
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, NativeMethods.POINT wParam, [In] [Out] NativeMethods.LVINSERTMARK lParam);

		// Token: 0x060005FD RID: 1533
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern bool SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.LVINSERTMARK lParam);

		// Token: 0x060005FE RID: 1534
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern bool SendMessage(HandleRef hWnd, int msg, int wParam, [In] [Out] NativeMethods.LVTILEVIEWINFO lParam);

		// Token: 0x060005FF RID: 1535
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, [In] [Out] ref NativeMethods.MCHITTESTINFOLEVEL5 lParam);

		// Token: 0x06000600 RID: 1536
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.MCHITTESTINFO lParam);

		// Token: 0x06000601 RID: 1537
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, [In] [Out] ref NativeMethods.MCGRIDINFO lParam);

		// Token: 0x06000602 RID: 1538
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.SYSTEMTIME lParam);

		// Token: 0x06000603 RID: 1539
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.SYSTEMTIMEARRAY lParam);

		// Token: 0x06000604 RID: 1540
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, [In] [Out] NativeMethods.LOGFONT lParam);

		// Token: 0x06000605 RID: 1541
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.MSG lParam);

		// Token: 0x06000606 RID: 1542
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, int lParam);

		// Token: 0x06000607 RID: 1543
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x06000608 RID: 1544
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int Msg, IntPtr wParam, [In] [Out] ref NativeMethods.RECT lParam);

		// Token: 0x06000609 RID: 1545
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int Msg, ref short wParam, ref short lParam);

		// Token: 0x0600060A RID: 1546
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int Msg, [MarshalAs(UnmanagedType.Bool)] [In] [Out] ref bool wParam, IntPtr lParam);

		// Token: 0x0600060B RID: 1547
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int Msg, int wParam, IntPtr lParam);

		// Token: 0x0600060C RID: 1548
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int Msg, int wParam, [In] [Out] ref NativeMethods.RECT lParam);

		// Token: 0x0600060D RID: 1549
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int Msg, int wParam, [In] [Out] ref Rectangle lParam);

		// Token: 0x0600060E RID: 1550
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int Msg, IntPtr wParam, NativeMethods.ListViewCompareCallback pfnCompare);

		// Token: 0x0600060F RID: 1551
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessageTimeout(HandleRef hWnd, int msg, IntPtr wParam, IntPtr lParam, int flags, int timeout, out IntPtr pdwResult);

		// Token: 0x06000610 RID: 1552
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr SetParent(HandleRef hWnd, HandleRef hWndParent);

		// Token: 0x06000611 RID: 1553
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool GetWindowRect(HandleRef hWnd, [In] [Out] ref NativeMethods.RECT rect);

		// Token: 0x06000612 RID: 1554
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GetWindow(HandleRef hWnd, int uCmd);

		// Token: 0x06000613 RID: 1555
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GetDlgItem(HandleRef hWnd, int nIDDlgItem);

		// Token: 0x06000614 RID: 1556
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr GetModuleHandle(string modName);

		// Token: 0x06000615 RID: 1557
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr DefWindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x06000616 RID: 1558
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr DefMDIChildProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x06000617 RID: 1559
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr CallWindowProc(IntPtr wndProc, IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x06000618 RID: 1560
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern short GlobalDeleteAtom(short atom);

		// Token: 0x06000619 RID: 1561
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern IntPtr GetProcAddress(HandleRef hModule, string lpProcName);

		// Token: 0x0600061A RID: 1562
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern bool GetClassInfo(HandleRef hInst, string lpszClass, [In] [Out] NativeMethods.WNDCLASS_I wc);

		// Token: 0x0600061B RID: 1563
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern bool GetClassInfo(HandleRef hInst, string lpszClass, IntPtr h);

		// Token: 0x0600061C RID: 1564
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int GetSystemMetrics(int nIndex);

		// Token: 0x0600061D RID: 1565
		[DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern UnsafeNativeMethods.BOOL GetPhysicalCursorPos(ref Point lpPoint);

		// Token: 0x0600061E RID: 1566
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int GetSystemMetricsForDpi(int nIndex, uint dpi);

		// Token: 0x0600061F RID: 1567 RVA: 0x00012780 File Offset: 0x00010980
		public static int TryGetSystemMetricsForDpi(int nIndex, uint dpi)
		{
			if (ApiHelper.IsApiAvailable("user32.dll", "GetSystemMetricsForDpi"))
			{
				return UnsafeNativeMethods.GetSystemMetricsForDpi(nIndex, dpi);
			}
			return UnsafeNativeMethods.GetSystemMetrics(nIndex);
		}

		// Token: 0x06000620 RID: 1568
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern bool SystemParametersInfo(int nAction, int nParam, ref NativeMethods.RECT rc, int nUpdate);

		// Token: 0x06000621 RID: 1569
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern bool SystemParametersInfo(int nAction, int nParam, ref int value, int ignore);

		// Token: 0x06000622 RID: 1570
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern bool SystemParametersInfo(int nAction, int nParam, ref bool value, int ignore);

		// Token: 0x06000623 RID: 1571
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern bool SystemParametersInfo(int nAction, int nParam, ref NativeMethods.HIGHCONTRAST_I rc, int nUpdate);

		// Token: 0x06000624 RID: 1572
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern bool SystemParametersInfo(int nAction, int nParam, [In] [Out] NativeMethods.NONCLIENTMETRICS metrics, int nUpdate);

		// Token: 0x06000625 RID: 1573
		[DllImport("user32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool SystemParametersInfoForDpi(int nAction, int nParam, [In] [Out] NativeMethods.NONCLIENTMETRICS metrics, int nUpdate, uint dpi);

		// Token: 0x06000626 RID: 1574 RVA: 0x000127A1 File Offset: 0x000109A1
		public static bool TrySystemParametersInfoForDpi(int nAction, int nParam, [In] [Out] NativeMethods.NONCLIENTMETRICS metrics, int nUpdate, uint dpi)
		{
			if (ApiHelper.IsApiAvailable("user32.dll", "SystemParametersInfoForDpi"))
			{
				return UnsafeNativeMethods.SystemParametersInfoForDpi(nAction, nParam, metrics, nUpdate, dpi);
			}
			return UnsafeNativeMethods.SystemParametersInfo(nAction, nParam, metrics, nUpdate);
		}

		// Token: 0x06000627 RID: 1575
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern bool SystemParametersInfo(int nAction, int nParam, [In] [Out] NativeMethods.LOGFONT font, int nUpdate);

		// Token: 0x06000628 RID: 1576
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern bool SystemParametersInfo(int nAction, int nParam, bool[] flag, bool nUpdate);

		// Token: 0x06000629 RID: 1577
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern bool GetComputerName(StringBuilder lpBuffer, int[] nSize);

		// Token: 0x0600062A RID: 1578
		[DllImport("advapi32.dll", CharSet = CharSet.Auto)]
		public static extern bool GetUserName(StringBuilder lpBuffer, int[] nSize);

		// Token: 0x0600062B RID: 1579
		[DllImport("user32.dll", ExactSpelling = true)]
		public static extern IntPtr GetProcessWindowStation();

		// Token: 0x0600062C RID: 1580
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool GetUserObjectInformation(HandleRef hObj, int nIndex, [MarshalAs(UnmanagedType.LPStruct)] NativeMethods.USEROBJECTFLAGS pvBuffer, int nLength, ref int lpnLengthNeeded);

		// Token: 0x0600062D RID: 1581
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int ClientToScreen(HandleRef hWnd, [In] [Out] NativeMethods.POINT pt);

		// Token: 0x0600062E RID: 1582
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GetForegroundWindow();

		// Token: 0x0600062F RID: 1583
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int MsgWaitForMultipleObjectsEx(int nCount, IntPtr pHandles, int dwMilliseconds, int dwWakeMask, int dwFlags);

		// Token: 0x06000630 RID: 1584
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GetDesktopWindow();

		// Token: 0x06000631 RID: 1585
		[DllImport("ole32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int RegisterDragDrop(HandleRef hwnd, UnsafeNativeMethods.IOleDropTarget target);

		// Token: 0x06000632 RID: 1586
		[DllImport("ole32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int RevokeDragDrop(HandleRef hwnd);

		// Token: 0x06000633 RID: 1587
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern bool PeekMessage([In] [Out] ref NativeMethods.MSG msg, HandleRef hwnd, int msgMin, int msgMax, int remove);

		// Token: 0x06000634 RID: 1588
		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		public static extern bool PeekMessageW([In] [Out] ref NativeMethods.MSG msg, HandleRef hwnd, int msgMin, int msgMax, int remove);

		// Token: 0x06000635 RID: 1589
		[DllImport("user32.dll", CharSet = CharSet.Ansi)]
		public static extern bool PeekMessageA([In] [Out] ref NativeMethods.MSG msg, HandleRef hwnd, int msgMin, int msgMax, int remove);

		// Token: 0x06000636 RID: 1590
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern bool PostMessage(HandleRef hwnd, int msg, IntPtr wparam, IntPtr lparam);

		// Token: 0x06000637 RID: 1591
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern short GlobalAddAtom(string atomName);

		// Token: 0x06000638 RID: 1592
		[DllImport("oleacc.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr LresultFromObject(ref Guid refiid, IntPtr wParam, HandleRef pAcc);

		// Token: 0x06000639 RID: 1593
		[DllImport("oleacc.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int CreateStdAccessibleObject(HandleRef hWnd, int objID, ref Guid refiid, [MarshalAs(UnmanagedType.Interface)] [In] [Out] ref object pAcc);

		// Token: 0x0600063A RID: 1594
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern void NotifyWinEvent(int winEvent, HandleRef hwnd, int objType, int objID);

		// Token: 0x0600063B RID: 1595
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int GetMenuItemID(HandleRef hMenu, int nPos);

		// Token: 0x0600063C RID: 1596
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GetSubMenu(HandleRef hwnd, int index);

		// Token: 0x0600063D RID: 1597
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int GetMenuItemCount(HandleRef hMenu);

		// Token: 0x0600063E RID: 1598
		[DllImport("oleaut32.dll", PreserveSig = false)]
		public static extern void GetErrorInfo(int reserved, [In] [Out] ref UnsafeNativeMethods.IErrorInfo errorInfo);

		// Token: 0x0600063F RID: 1599
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "BeginPaint", ExactSpelling = true)]
		private static extern IntPtr IntBeginPaint(HandleRef hWnd, [In] [Out] ref NativeMethods.PAINTSTRUCT lpPaint);

		// Token: 0x06000640 RID: 1600 RVA: 0x000127C9 File Offset: 0x000109C9
		public static IntPtr BeginPaint(HandleRef hWnd, [MarshalAs(UnmanagedType.LPStruct)] [In] [Out] ref NativeMethods.PAINTSTRUCT lpPaint)
		{
			return System.Internal.HandleCollector.Add(UnsafeNativeMethods.IntBeginPaint(hWnd, ref lpPaint), NativeMethods.CommonHandles.HDC);
		}

		// Token: 0x06000641 RID: 1601
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "EndPaint", ExactSpelling = true)]
		private static extern bool IntEndPaint(HandleRef hWnd, ref NativeMethods.PAINTSTRUCT lpPaint);

		// Token: 0x06000642 RID: 1602 RVA: 0x000127DC File Offset: 0x000109DC
		public static bool EndPaint(HandleRef hWnd, [MarshalAs(UnmanagedType.LPStruct)] [In] ref NativeMethods.PAINTSTRUCT lpPaint)
		{
			System.Internal.HandleCollector.Remove(lpPaint.hdc, NativeMethods.CommonHandles.HDC);
			return UnsafeNativeMethods.IntEndPaint(hWnd, ref lpPaint);
		}

		// Token: 0x06000643 RID: 1603
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "GetDC", ExactSpelling = true)]
		private static extern IntPtr IntGetDC(HandleRef hWnd);

		// Token: 0x06000644 RID: 1604 RVA: 0x000127F6 File Offset: 0x000109F6
		public static IntPtr GetDC(HandleRef hWnd)
		{
			return System.Internal.HandleCollector.Add(UnsafeNativeMethods.IntGetDC(hWnd), NativeMethods.CommonHandles.HDC);
		}

		// Token: 0x06000645 RID: 1605
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "GetWindowDC", ExactSpelling = true)]
		private static extern IntPtr IntGetWindowDC(HandleRef hWnd);

		// Token: 0x06000646 RID: 1606 RVA: 0x00012808 File Offset: 0x00010A08
		public static IntPtr GetWindowDC(HandleRef hWnd)
		{
			return System.Internal.HandleCollector.Add(UnsafeNativeMethods.IntGetWindowDC(hWnd), NativeMethods.CommonHandles.HDC);
		}

		// Token: 0x06000647 RID: 1607
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "ReleaseDC", ExactSpelling = true)]
		private static extern int IntReleaseDC(HandleRef hWnd, HandleRef hDC);

		// Token: 0x06000648 RID: 1608 RVA: 0x0001281A File Offset: 0x00010A1A
		public static int ReleaseDC(HandleRef hWnd, HandleRef hDC)
		{
			System.Internal.HandleCollector.Remove((IntPtr)hDC, NativeMethods.CommonHandles.HDC);
			return UnsafeNativeMethods.IntReleaseDC(hWnd, hDC);
		}

		// Token: 0x06000649 RID: 1609
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateDC", SetLastError = true)]
		private static extern IntPtr IntCreateDC(string lpszDriver, string lpszDeviceName, string lpszOutput, HandleRef devMode);

		// Token: 0x0600064A RID: 1610 RVA: 0x00012834 File Offset: 0x00010A34
		public static IntPtr CreateDC(string lpszDriver)
		{
			return System.Internal.HandleCollector.Add(UnsafeNativeMethods.IntCreateDC(lpszDriver, null, null, NativeMethods.NullHandleRef), NativeMethods.CommonHandles.HDC);
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0001284D File Offset: 0x00010A4D
		public static IntPtr CreateDC(string lpszDriverName, string lpszDeviceName, string lpszOutput, HandleRef lpInitData)
		{
			return System.Internal.HandleCollector.Add(UnsafeNativeMethods.IntCreateDC(lpszDriverName, lpszDeviceName, lpszOutput, lpInitData), NativeMethods.CommonHandles.HDC);
		}

		// Token: 0x0600064C RID: 1612
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern bool SystemParametersInfo(int nAction, int nParam, [In] [Out] IntPtr[] rc, int nUpdate);

		// Token: 0x0600064D RID: 1613
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SendMessage")]
		public static extern IntPtr SendCallbackMessage(HandleRef hWnd, int Msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x0600064E RID: 1614
		[DllImport("shell32.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern void DragAcceptFiles(HandleRef hWnd, bool fAccept);

		// Token: 0x0600064F RID: 1615
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int GetDeviceCaps(HandleRef hDC, int nIndex);

		// Token: 0x06000650 RID: 1616
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool GetScrollInfo(HandleRef hWnd, int fnBar, NativeMethods.SCROLLINFO si);

		// Token: 0x06000651 RID: 1617
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int SetScrollInfo(HandleRef hWnd, int fnBar, NativeMethods.SCROLLINFO si, bool redraw);

		// Token: 0x06000652 RID: 1618
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GetActiveWindow();

		// Token: 0x06000653 RID: 1619
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr LoadLibrary(string libname);

		// Token: 0x06000654 RID: 1620
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr LoadLibraryEx(string lpModuleName, IntPtr hFile, uint dwFlags);

		// Token: 0x06000655 RID: 1621
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool FreeLibrary(HandleRef hModule);

		// Token: 0x06000656 RID: 1622 RVA: 0x00012862 File Offset: 0x00010A62
		public static IntPtr GetWindowLong(HandleRef hWnd, int nIndex)
		{
			if (IntPtr.Size == 4)
			{
				return UnsafeNativeMethods.GetWindowLong32(hWnd, nIndex);
			}
			return UnsafeNativeMethods.GetWindowLongPtr64(hWnd, nIndex);
		}

		// Token: 0x06000657 RID: 1623
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "GetWindowLong")]
		public static extern IntPtr GetWindowLong32(HandleRef hWnd, int nIndex);

		// Token: 0x06000658 RID: 1624
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "GetWindowLongPtr")]
		public static extern IntPtr GetWindowLongPtr64(HandleRef hWnd, int nIndex);

		// Token: 0x06000659 RID: 1625 RVA: 0x0001287B File Offset: 0x00010A7B
		public static IntPtr SetWindowLong(HandleRef hWnd, int nIndex, HandleRef dwNewLong)
		{
			if (IntPtr.Size == 4)
			{
				return UnsafeNativeMethods.SetWindowLongPtr32(hWnd, nIndex, dwNewLong);
			}
			return UnsafeNativeMethods.SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
		}

		// Token: 0x0600065A RID: 1626
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowLong")]
		public static extern IntPtr SetWindowLongPtr32(HandleRef hWnd, int nIndex, HandleRef dwNewLong);

		// Token: 0x0600065B RID: 1627
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowLongPtr")]
		public static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, HandleRef dwNewLong);

		// Token: 0x0600065C RID: 1628 RVA: 0x00012896 File Offset: 0x00010A96
		public static IntPtr SetWindowLong(HandleRef hWnd, int nIndex, NativeMethods.WndProc wndproc)
		{
			if (IntPtr.Size == 4)
			{
				return UnsafeNativeMethods.SetWindowLongPtr32(hWnd, nIndex, wndproc);
			}
			return UnsafeNativeMethods.SetWindowLongPtr64(hWnd, nIndex, wndproc);
		}

		// Token: 0x0600065D RID: 1629
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowLong")]
		public static extern IntPtr SetWindowLongPtr32(HandleRef hWnd, int nIndex, NativeMethods.WndProc wndproc);

		// Token: 0x0600065E RID: 1630
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowLongPtr")]
		public static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, NativeMethods.WndProc wndproc);

		// Token: 0x0600065F RID: 1631
		[DllImport("ole32.dll", PreserveSig = false)]
		public static extern UnsafeNativeMethods.ILockBytes CreateILockBytesOnHGlobal(HandleRef hGlobal, bool fDeleteOnRelease);

		// Token: 0x06000660 RID: 1632
		[DllImport("ole32.dll", PreserveSig = false)]
		public static extern UnsafeNativeMethods.IStorage StgCreateDocfileOnILockBytes(UnsafeNativeMethods.ILockBytes iLockBytes, int grfMode, int reserved);

		// Token: 0x06000661 RID: 1633
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "CreatePopupMenu", ExactSpelling = true)]
		private static extern IntPtr IntCreatePopupMenu();

		// Token: 0x06000662 RID: 1634 RVA: 0x000128B1 File Offset: 0x00010AB1
		public static IntPtr CreatePopupMenu()
		{
			return System.Internal.HandleCollector.Add(UnsafeNativeMethods.IntCreatePopupMenu(), NativeMethods.CommonHandles.Menu);
		}

		// Token: 0x06000663 RID: 1635
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool RemoveMenu(HandleRef hMenu, int uPosition, int uFlags);

		// Token: 0x06000664 RID: 1636
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "DestroyMenu", ExactSpelling = true)]
		private static extern bool IntDestroyMenu(HandleRef hMenu);

		// Token: 0x06000665 RID: 1637 RVA: 0x000128C2 File Offset: 0x00010AC2
		public static bool DestroyMenu(HandleRef hMenu)
		{
			System.Internal.HandleCollector.Remove((IntPtr)hMenu, NativeMethods.CommonHandles.Menu);
			return UnsafeNativeMethods.IntDestroyMenu(hMenu);
		}

		// Token: 0x06000666 RID: 1638
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool SetForegroundWindow(HandleRef hWnd);

		// Token: 0x06000667 RID: 1639
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GetSystemMenu(HandleRef hWnd, bool bRevert);

		// Token: 0x06000668 RID: 1640
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr DefFrameProc(IntPtr hWnd, IntPtr hWndClient, int msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x06000669 RID: 1641
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool TranslateMDISysAccel(IntPtr hWndClient, [In] [Out] ref NativeMethods.MSG msg);

		// Token: 0x0600066A RID: 1642
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern bool SetLayeredWindowAttributes(HandleRef hwnd, int crKey, byte bAlpha, int dwFlags);

		// Token: 0x0600066B RID: 1643
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool SetMenu(HandleRef hWnd, HandleRef hMenu);

		// Token: 0x0600066C RID: 1644
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int GetWindowPlacement(HandleRef hWnd, ref NativeMethods.WINDOWPLACEMENT placement);

		// Token: 0x0600066D RID: 1645
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern void GetStartupInfo([In] [Out] NativeMethods.STARTUPINFO_I startupinfo_i);

		// Token: 0x0600066E RID: 1646
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool SetMenuDefaultItem(HandleRef hwnd, int nIndex, bool pos);

		// Token: 0x0600066F RID: 1647
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool EnableMenuItem(HandleRef hMenu, int UIDEnabledItem, int uEnable);

		// Token: 0x06000670 RID: 1648
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr SetActiveWindow(HandleRef hWnd);

		// Token: 0x06000671 RID: 1649
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateIC", SetLastError = true)]
		private static extern IntPtr IntCreateIC(string lpszDriverName, string lpszDeviceName, string lpszOutput, HandleRef lpInitData);

		// Token: 0x06000672 RID: 1650 RVA: 0x000128DB File Offset: 0x00010ADB
		public static IntPtr CreateIC(string lpszDriverName, string lpszDeviceName, string lpszOutput, HandleRef lpInitData)
		{
			return System.Internal.HandleCollector.Add(UnsafeNativeMethods.IntCreateIC(lpszDriverName, lpszDeviceName, lpszOutput, lpInitData), NativeMethods.CommonHandles.HDC);
		}

		// Token: 0x06000673 RID: 1651
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool ClipCursor(ref NativeMethods.RECT rcClip);

		// Token: 0x06000674 RID: 1652
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool ClipCursor(NativeMethods.COMRECT rcClip);

		// Token: 0x06000675 RID: 1653
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr SetCursor(HandleRef hcursor);

		// Token: 0x06000676 RID: 1654
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool SetCursorPos(int x, int y);

		// Token: 0x06000677 RID: 1655
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int ShowCursor(bool bShow);

		// Token: 0x06000678 RID: 1656
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "DestroyCursor", ExactSpelling = true)]
		private static extern bool IntDestroyCursor(HandleRef hCurs);

		// Token: 0x06000679 RID: 1657 RVA: 0x000128F0 File Offset: 0x00010AF0
		public static bool DestroyCursor(HandleRef hCurs)
		{
			System.Internal.HandleCollector.Remove((IntPtr)hCurs, NativeMethods.CommonHandles.Cursor);
			return UnsafeNativeMethods.IntDestroyCursor(hCurs);
		}

		// Token: 0x0600067A RID: 1658
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool IsWindow(HandleRef hWnd);

		// Token: 0x0600067B RID: 1659
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "DeleteDC", ExactSpelling = true, SetLastError = true)]
		private static extern bool IntDeleteDC(HandleRef hDC);

		// Token: 0x0600067C RID: 1660 RVA: 0x00012909 File Offset: 0x00010B09
		public static bool DeleteDC(HandleRef hDC)
		{
			System.Internal.HandleCollector.Remove((IntPtr)hDC, NativeMethods.CommonHandles.HDC);
			return UnsafeNativeMethods.IntDeleteDC(hDC);
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00012922 File Offset: 0x00010B22
		public static bool DeleteCompatibleDC(HandleRef hDC)
		{
			System.Internal.HandleCollector.Remove((IntPtr)hDC, NativeMethods.CommonHandles.CompatibleHDC);
			return UnsafeNativeMethods.IntDeleteDC(hDC);
		}

		// Token: 0x0600067E RID: 1662
		[DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern bool GetMessageA([In] [Out] ref NativeMethods.MSG msg, HandleRef hWnd, int uMsgFilterMin, int uMsgFilterMax);

		// Token: 0x0600067F RID: 1663
		[DllImport("user32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern bool GetMessageW([In] [Out] ref NativeMethods.MSG msg, HandleRef hWnd, int uMsgFilterMin, int uMsgFilterMax);

		// Token: 0x06000680 RID: 1664
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr PostMessage(HandleRef hwnd, int msg, int wparam, int lparam);

		// Token: 0x06000681 RID: 1665
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr PostMessage(HandleRef hwnd, int msg, int wparam, IntPtr lparam);

		// Token: 0x06000682 RID: 1666
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool GetClientRect(HandleRef hWnd, [In] [Out] ref NativeMethods.RECT rect);

		// Token: 0x06000683 RID: 1667
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool GetClientRect(HandleRef hWnd, IntPtr rect);

		// Token: 0x06000684 RID: 1668
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "WindowFromPoint", ExactSpelling = true)]
		private static extern IntPtr _WindowFromPoint(UnsafeNativeMethods.POINTSTRUCT pt);

		// Token: 0x06000685 RID: 1669 RVA: 0x0001293C File Offset: 0x00010B3C
		public static IntPtr WindowFromPoint(int x, int y)
		{
			UnsafeNativeMethods.POINTSTRUCT pointstruct = new UnsafeNativeMethods.POINTSTRUCT(x, y);
			return UnsafeNativeMethods._WindowFromPoint(pointstruct);
		}

		// Token: 0x06000686 RID: 1670
		[DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr WindowFromDC(HandleRef hDC);

		// Token: 0x06000687 RID: 1671
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateWindowEx", SetLastError = true)]
		public static extern IntPtr IntCreateWindowEx(int dwExStyle, string lpszClassName, string lpszWindowName, int style, int x, int y, int width, int height, HandleRef hWndParent, HandleRef hMenu, HandleRef hInst, [MarshalAs(UnmanagedType.AsAny)] object pvParam);

		// Token: 0x06000688 RID: 1672 RVA: 0x00012958 File Offset: 0x00010B58
		public static IntPtr CreateWindowEx(int dwExStyle, string lpszClassName, string lpszWindowName, int style, int x, int y, int width, int height, HandleRef hWndParent, HandleRef hMenu, HandleRef hInst, [MarshalAs(UnmanagedType.AsAny)] object pvParam)
		{
			return UnsafeNativeMethods.IntCreateWindowEx(dwExStyle, lpszClassName, lpszWindowName, style, x, y, width, height, hWndParent, hMenu, hInst, pvParam);
		}

		// Token: 0x06000689 RID: 1673
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "DestroyWindow", ExactSpelling = true)]
		public static extern bool IntDestroyWindow(HandleRef hWnd);

		// Token: 0x0600068A RID: 1674 RVA: 0x0001297E File Offset: 0x00010B7E
		public static bool DestroyWindow(HandleRef hWnd)
		{
			return UnsafeNativeMethods.IntDestroyWindow(hWnd);
		}

		// Token: 0x0600068B RID: 1675
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool UnregisterClass(string className, HandleRef hInstance);

		// Token: 0x0600068C RID: 1676
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr GetStockObject(int nIndex);

		// Token: 0x0600068D RID: 1677
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern short RegisterClass(NativeMethods.WNDCLASS_D wc);

		// Token: 0x0600068E RID: 1678
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern void PostQuitMessage(int nExitCode);

		// Token: 0x0600068F RID: 1679
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern void WaitMessage();

		// Token: 0x06000690 RID: 1680
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool SetWindowPlacement(HandleRef hWnd, [In] ref NativeMethods.WINDOWPLACEMENT placement);

		// Token: 0x06000691 RID: 1681
		[DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern uint GetDpiForWindow(HandleRef hWnd);

		// Token: 0x06000692 RID: 1682
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool GetSystemPowerStatus([In] [Out] ref NativeMethods.SYSTEM_POWER_STATUS systemPowerStatus);

		// Token: 0x06000693 RID: 1683
		[DllImport("Powrprof.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool SetSuspendState(bool hiberate, bool forceCritical, bool disableWakeEvent);

		// Token: 0x06000694 RID: 1684
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int GetRegionData(HandleRef hRgn, int size, IntPtr lpRgnData);

		// Token: 0x06000695 RID: 1685 RVA: 0x00012988 File Offset: 0x00010B88
		public unsafe static NativeMethods.RECT[] GetRectsFromRegion(IntPtr hRgn)
		{
			NativeMethods.RECT[] array = null;
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				int regionData = UnsafeNativeMethods.GetRegionData(new HandleRef(null, hRgn), 0, IntPtr.Zero);
				if (regionData != 0)
				{
					intPtr = Marshal.AllocCoTaskMem(regionData);
					int regionData2 = UnsafeNativeMethods.GetRegionData(new HandleRef(null, hRgn), regionData, intPtr);
					if (regionData2 == regionData)
					{
						NativeMethods.RGNDATAHEADER* ptr = (NativeMethods.RGNDATAHEADER*)(void*)intPtr;
						if (ptr->iType == 1)
						{
							array = new NativeMethods.RECT[ptr->nCount];
							int cbSizeOfStruct = ptr->cbSizeOfStruct;
							for (int i = 0; i < ptr->nCount; i++)
							{
								array[i] = *(NativeMethods.RECT*)((byte*)((byte*)(void*)intPtr + cbSizeOfStruct) + Marshal.SizeOf(typeof(NativeMethods.RECT)) * i);
							}
						}
					}
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(intPtr);
				}
			}
			return array;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x00012A60 File Offset: 0x00010C60
		internal static bool IsComObject(object o)
		{
			return Marshal.IsComObject(o);
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00012A68 File Offset: 0x00010C68
		internal static int ReleaseComObject(object objToRelease)
		{
			return Marshal.ReleaseComObject(objToRelease);
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00012A70 File Offset: 0x00010C70
		[ReflectionPermission(SecurityAction.Assert, Unrestricted = true)]
		public static object PtrToStructure(IntPtr lparam, Type cls)
		{
			return Marshal.PtrToStructure(lparam, cls);
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00012A79 File Offset: 0x00010C79
		[ReflectionPermission(SecurityAction.Assert, Unrestricted = true)]
		public static void PtrToStructure(IntPtr lparam, object data)
		{
			Marshal.PtrToStructure(lparam, data);
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00012A82 File Offset: 0x00010C82
		internal static int SizeOf(Type t)
		{
			return Marshal.SizeOf(t);
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00012A8A File Offset: 0x00010C8A
		internal static void ThrowExceptionForHR(int errorCode)
		{
			Marshal.ThrowExceptionForHR(errorCode);
		}

		// Token: 0x0600069C RID: 1692
		[DllImport("clr.dll", BestFitMapping = false, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = false)]
		internal static extern void CorLaunchApplication(uint hostType, string applicationFullName, int manifestPathsCount, string[] manifestPaths, int activationDataCount, string[] activationData, UnsafeNativeMethods.PROCESS_INFORMATION processInformation);

		// Token: 0x0600069D RID: 1693
		[DllImport("UIAutomationCore.dll", CharSet = CharSet.Unicode)]
		internal static extern int UiaHostProviderFromHwnd(HandleRef hwnd, out UnsafeNativeMethods.IRawElementProviderSimple provider);

		// Token: 0x0600069E RID: 1694
		[DllImport("UIAutomationCore.dll", CharSet = CharSet.Unicode)]
		internal static extern IntPtr UiaReturnRawElementProvider(HandleRef hwnd, IntPtr wParam, IntPtr lParam, UnsafeNativeMethods.IRawElementProviderSimple el);

		// Token: 0x0600069F RID: 1695
		[DllImport("UIAutomationCore.dll", CharSet = CharSet.Unicode)]
		public static extern int UiaDisconnectProvider(UnsafeNativeMethods.IRawElementProviderSimple provider);

		// Token: 0x060006A0 RID: 1696
		[DllImport("UIAutomationCore.dll", CharSet = CharSet.Unicode)]
		internal static extern bool UiaClientsAreListening();

		// Token: 0x060006A1 RID: 1697
		[DllImport("UIAutomationCore.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern int UiaRaiseAutomationEvent(UnsafeNativeMethods.IRawElementProviderSimple provider, int id);

		// Token: 0x060006A2 RID: 1698
		[DllImport("UIAutomationCore.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern int UiaRaiseAutomationPropertyChangedEvent(UnsafeNativeMethods.IRawElementProviderSimple provider, int id, object oldValue, object newValue);

		// Token: 0x060006A3 RID: 1699
		[DllImport("UIAutomationCore.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern int UiaRaiseNotificationEvent(UnsafeNativeMethods.IRawElementProviderSimple provider, AutomationNotificationKind notificationKind, AutomationNotificationProcessing notificationProcessing, string notificationText, string activityId);

		// Token: 0x060006A4 RID: 1700
		[DllImport("UIAutomationCore.dll", CharSet = CharSet.Unicode)]
		internal static extern int UiaRaiseStructureChangedEvent(UnsafeNativeMethods.IRawElementProviderSimple provider, UnsafeNativeMethods.StructureChangeType structureChangeType, int[] runtimeId, int runtimeIdLen);

		// Token: 0x060006A5 RID: 1701 RVA: 0x00012A94 File Offset: 0x00010C94
		public static IntPtr LoadLibraryFromSystemPathIfAvailable(string libraryName)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr moduleHandle = UnsafeNativeMethods.GetModuleHandle("kernel32.dll");
			if (moduleHandle != IntPtr.Zero)
			{
				if (UnsafeNativeMethods.GetProcAddress(new HandleRef(null, moduleHandle), "AddDllDirectory") != IntPtr.Zero)
				{
					intPtr = UnsafeNativeMethods.LoadLibraryEx(libraryName, IntPtr.Zero, 2048U);
				}
				else
				{
					intPtr = UnsafeNativeMethods.LoadLibrary(libraryName);
				}
			}
			return intPtr;
		}

		// Token: 0x060006A6 RID: 1702
		[DllImport("wldp.dll", ExactSpelling = true)]
		private static extern int WldpIsDynamicCodePolicyEnabled(out int enabled);

		// Token: 0x060006A7 RID: 1703 RVA: 0x00012AF8 File Offset: 0x00010CF8
		internal static bool IsDynamicCodePolicyEnabled()
		{
			if (!ApiHelper.IsApiAvailable("wldp.dll", "WldpIsDynamicCodePolicyEnabled"))
			{
				return false;
			}
			int num = 0;
			return UnsafeNativeMethods.WldpIsDynamicCodePolicyEnabled(out num) == 0 && num != 0;
		}

		// Token: 0x040004BA RID: 1210
		private static readonly Version VistaOSVersion = new Version(6, 0);

		// Token: 0x040004BB RID: 1211
		public const int MB_PRECOMPOSED = 1;

		// Token: 0x040004BC RID: 1212
		public const int SMTO_ABORTIFHUNG = 2;

		// Token: 0x040004BD RID: 1213
		public const int LAYOUT_RTL = 1;

		// Token: 0x040004BE RID: 1214
		public const int LAYOUT_BITMAPORIENTATIONPRESERVED = 8;

		// Token: 0x040004BF RID: 1215
		public static readonly Guid guid_IAccessibleEx = new Guid("{F8B80ADA-2C44-48D0-89BE-5FF23C9CD875}");

		// Token: 0x02000559 RID: 1369
		internal enum BOOL
		{
			// Token: 0x04003833 RID: 14387
			FALSE,
			// Token: 0x04003834 RID: 14388
			TRUE
		}

		// Token: 0x0200055A RID: 1370
		internal static class User32
		{
			// Token: 0x060055C0 RID: 21952
			[DllImport("user32.dll", ExactSpelling = true)]
			public static extern UnsafeNativeMethods.BOOL GetCaretPos(out Point pt);
		}

		// Token: 0x0200055B RID: 1371
		internal static class UiaCore
		{
			// Token: 0x060055C1 RID: 21953
			[DllImport("UIAutomationCore.dll", ExactSpelling = true)]
			private static extern int UiaGetReservedNotSupportedValue([MarshalAs(UnmanagedType.IUnknown)] out object notSupportedValue);

			// Token: 0x060055C2 RID: 21954 RVA: 0x0016778B File Offset: 0x0016598B
			public static object UiaGetReservedNotSupportedValue()
			{
				if (UnsafeNativeMethods.UiaCore.s_notSupportedValue == null)
				{
					UnsafeNativeMethods.UiaCore.UiaGetReservedNotSupportedValue(out UnsafeNativeMethods.UiaCore.s_notSupportedValue);
				}
				return UnsafeNativeMethods.UiaCore.s_notSupportedValue;
			}

			// Token: 0x04003835 RID: 14389
			private static object s_notSupportedValue;

			// Token: 0x020008A7 RID: 2215
			public enum TextAttributeIdentifier
			{
				// Token: 0x040044DB RID: 17627
				AnimationStyleAttributeId = 40000,
				// Token: 0x040044DC RID: 17628
				BackgroundColorAttributeId,
				// Token: 0x040044DD RID: 17629
				BulletStyleAttributeId,
				// Token: 0x040044DE RID: 17630
				CapStyleAttributeId,
				// Token: 0x040044DF RID: 17631
				CultureAttributeId,
				// Token: 0x040044E0 RID: 17632
				FontNameAttributeId,
				// Token: 0x040044E1 RID: 17633
				FontSizeAttributeId,
				// Token: 0x040044E2 RID: 17634
				FontWeightAttributeId,
				// Token: 0x040044E3 RID: 17635
				ForegroundColorAttributeId,
				// Token: 0x040044E4 RID: 17636
				HorizontalTextAlignmentAttributeId,
				// Token: 0x040044E5 RID: 17637
				IndentationFirstLineAttributeId,
				// Token: 0x040044E6 RID: 17638
				IndentationLeadingAttributeId,
				// Token: 0x040044E7 RID: 17639
				IndentationTrailingAttributeId,
				// Token: 0x040044E8 RID: 17640
				IsHiddenAttributeId,
				// Token: 0x040044E9 RID: 17641
				IsItalicAttributeId,
				// Token: 0x040044EA RID: 17642
				IsReadOnlyAttributeId,
				// Token: 0x040044EB RID: 17643
				IsSubscriptAttributeId,
				// Token: 0x040044EC RID: 17644
				IsSuperscriptAttributeId,
				// Token: 0x040044ED RID: 17645
				MarginBottomAttributeId,
				// Token: 0x040044EE RID: 17646
				MarginLeadingAttributeId,
				// Token: 0x040044EF RID: 17647
				MarginTopAttributeId,
				// Token: 0x040044F0 RID: 17648
				MarginTrailingAttributeId,
				// Token: 0x040044F1 RID: 17649
				OutlineStylesAttributeId,
				// Token: 0x040044F2 RID: 17650
				OverlineColorAttributeId,
				// Token: 0x040044F3 RID: 17651
				OverlineStyleAttributeId,
				// Token: 0x040044F4 RID: 17652
				StrikethroughColorAttributeId,
				// Token: 0x040044F5 RID: 17653
				StrikethroughStyleAttributeId,
				// Token: 0x040044F6 RID: 17654
				TabsAttributeId,
				// Token: 0x040044F7 RID: 17655
				TextFlowDirectionsAttributeId,
				// Token: 0x040044F8 RID: 17656
				UnderlineColorAttributeId,
				// Token: 0x040044F9 RID: 17657
				UnderlineStyleAttributeId,
				// Token: 0x040044FA RID: 17658
				AnnotationTypesAttributeId,
				// Token: 0x040044FB RID: 17659
				AnnotationObjectsAttributeId,
				// Token: 0x040044FC RID: 17660
				StyleNameAttributeId,
				// Token: 0x040044FD RID: 17661
				StyleIdAttributeId,
				// Token: 0x040044FE RID: 17662
				LinkAttributeId,
				// Token: 0x040044FF RID: 17663
				IsActiveAttributeId,
				// Token: 0x04004500 RID: 17664
				SelectionActiveEndAttributeId,
				// Token: 0x04004501 RID: 17665
				CaretPositionAttributeId,
				// Token: 0x04004502 RID: 17666
				CaretBidiModeAttributeId,
				// Token: 0x04004503 RID: 17667
				LineSpacingAttributeId,
				// Token: 0x04004504 RID: 17668
				BeforeParagraphSpacingAttributeId,
				// Token: 0x04004505 RID: 17669
				AfterParagraphSpacingAttributeId
			}

			// Token: 0x020008A8 RID: 2216
			public enum TextPatternRangeEndpoint
			{
				// Token: 0x04004507 RID: 17671
				Start,
				// Token: 0x04004508 RID: 17672
				End
			}

			// Token: 0x020008A9 RID: 2217
			public enum TextUnit
			{
				// Token: 0x0400450A RID: 17674
				Character,
				// Token: 0x0400450B RID: 17675
				Format,
				// Token: 0x0400450C RID: 17676
				Word,
				// Token: 0x0400450D RID: 17677
				Line,
				// Token: 0x0400450E RID: 17678
				Paragraph,
				// Token: 0x0400450F RID: 17679
				Page,
				// Token: 0x04004510 RID: 17680
				Document
			}

			// Token: 0x020008AA RID: 2218
			[Guid("3589c92c-63f3-4367-99bb-ada653b77cf2")]
			[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
			[ComImport]
			public interface ITextProvider
			{
				// Token: 0x06007234 RID: 29236
				UnsafeNativeMethods.UiaCore.ITextRangeProvider[] GetSelection();

				// Token: 0x06007235 RID: 29237
				UnsafeNativeMethods.UiaCore.ITextRangeProvider[] GetVisibleRanges();

				// Token: 0x06007236 RID: 29238
				UnsafeNativeMethods.UiaCore.ITextRangeProvider RangeFromChild(UnsafeNativeMethods.IRawElementProviderSimple childElement);

				// Token: 0x06007237 RID: 29239
				UnsafeNativeMethods.UiaCore.ITextRangeProvider RangeFromPoint(Point screenLocation);

				// Token: 0x1700191A RID: 6426
				// (get) Token: 0x06007238 RID: 29240
				UnsafeNativeMethods.UiaCore.ITextRangeProvider DocumentRange { get; }

				// Token: 0x1700191B RID: 6427
				// (get) Token: 0x06007239 RID: 29241
				UnsafeNativeMethods.UiaCore.SupportedTextSelection SupportedTextSelection { get; }
			}

			// Token: 0x020008AB RID: 2219
			[Guid("0dc5e6ed-3e16-4bf1-8f9a-a979878bc195")]
			[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
			[ComImport]
			public interface ITextProvider2 : UnsafeNativeMethods.UiaCore.ITextProvider
			{
				// Token: 0x0600723A RID: 29242
				UnsafeNativeMethods.UiaCore.ITextRangeProvider[] GetSelection();

				// Token: 0x0600723B RID: 29243
				UnsafeNativeMethods.UiaCore.ITextRangeProvider[] GetVisibleRanges();

				// Token: 0x0600723C RID: 29244
				UnsafeNativeMethods.UiaCore.ITextRangeProvider RangeFromChild(UnsafeNativeMethods.IRawElementProviderSimple childElement);

				// Token: 0x0600723D RID: 29245
				UnsafeNativeMethods.UiaCore.ITextRangeProvider RangeFromPoint(Point screenLocation);

				// Token: 0x1700191C RID: 6428
				// (get) Token: 0x0600723E RID: 29246
				UnsafeNativeMethods.UiaCore.ITextRangeProvider DocumentRange { get; }

				// Token: 0x1700191D RID: 6429
				// (get) Token: 0x0600723F RID: 29247
				UnsafeNativeMethods.UiaCore.SupportedTextSelection SupportedTextSelection { get; }

				// Token: 0x06007240 RID: 29248
				UnsafeNativeMethods.UiaCore.ITextRangeProvider RangeFromAnnotation(UnsafeNativeMethods.IRawElementProviderSimple annotation);

				// Token: 0x06007241 RID: 29249
				UnsafeNativeMethods.UiaCore.ITextRangeProvider GetCaretRange(out UnsafeNativeMethods.BOOL isActive);
			}

			// Token: 0x020008AC RID: 2220
			[Guid("5347ad7b-c355-46f8-aff5-909033582f63")]
			[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
			[ComImport]
			public interface ITextRangeProvider
			{
				// Token: 0x06007242 RID: 29250
				UnsafeNativeMethods.UiaCore.ITextRangeProvider Clone();

				// Token: 0x06007243 RID: 29251
				UnsafeNativeMethods.BOOL Compare(UnsafeNativeMethods.UiaCore.ITextRangeProvider range);

				// Token: 0x06007244 RID: 29252
				int CompareEndpoints(UnsafeNativeMethods.UiaCore.TextPatternRangeEndpoint endpoint, UnsafeNativeMethods.UiaCore.ITextRangeProvider targetRange, UnsafeNativeMethods.UiaCore.TextPatternRangeEndpoint targetEndpoint);

				// Token: 0x06007245 RID: 29253
				void ExpandToEnclosingUnit(UnsafeNativeMethods.UiaCore.TextUnit unit);

				// Token: 0x06007246 RID: 29254
				UnsafeNativeMethods.UiaCore.ITextRangeProvider FindAttribute(int attribute, object value, UnsafeNativeMethods.BOOL backward);

				// Token: 0x06007247 RID: 29255
				UnsafeNativeMethods.UiaCore.ITextRangeProvider FindText(string text, UnsafeNativeMethods.BOOL backward, UnsafeNativeMethods.BOOL ignoreCase);

				// Token: 0x06007248 RID: 29256
				object GetAttributeValue(int attribute);

				// Token: 0x06007249 RID: 29257
				double[] GetBoundingRectangles();

				// Token: 0x0600724A RID: 29258
				UnsafeNativeMethods.IRawElementProviderSimple GetEnclosingElement();

				// Token: 0x0600724B RID: 29259
				string GetText(int maxLength);

				// Token: 0x0600724C RID: 29260
				int Move(UnsafeNativeMethods.UiaCore.TextUnit unit, int count);

				// Token: 0x0600724D RID: 29261
				int MoveEndpointByUnit(UnsafeNativeMethods.UiaCore.TextPatternRangeEndpoint endpoint, UnsafeNativeMethods.UiaCore.TextUnit unit, int count);

				// Token: 0x0600724E RID: 29262
				void MoveEndpointByRange(UnsafeNativeMethods.UiaCore.TextPatternRangeEndpoint endpoint, UnsafeNativeMethods.UiaCore.ITextRangeProvider targetRange, UnsafeNativeMethods.UiaCore.TextPatternRangeEndpoint targetEndpoint);

				// Token: 0x0600724F RID: 29263
				void Select();

				// Token: 0x06007250 RID: 29264
				void AddToSelection();

				// Token: 0x06007251 RID: 29265
				void RemoveFromSelection();

				// Token: 0x06007252 RID: 29266
				void ScrollIntoView(UnsafeNativeMethods.BOOL alignToTop);

				// Token: 0x06007253 RID: 29267
				UnsafeNativeMethods.IRawElementProviderSimple[] GetChildren();
			}

			// Token: 0x020008AD RID: 2221
			[Flags]
			public enum SupportedTextSelection
			{
				// Token: 0x04004512 RID: 17682
				None = 0,
				// Token: 0x04004513 RID: 17683
				Single = 1,
				// Token: 0x04004514 RID: 17684
				Multiple = 2
			}
		}

		// Token: 0x0200055C RID: 1372
		internal struct POINTSTRUCT
		{
			// Token: 0x060055C3 RID: 21955 RVA: 0x001677A4 File Offset: 0x001659A4
			public POINTSTRUCT(int x, int y)
			{
				this.x = x;
				this.y = y;
			}

			// Token: 0x04003836 RID: 14390
			public int x;

			// Token: 0x04003837 RID: 14391
			public int y;
		}

		// Token: 0x0200055D RID: 1373
		[Guid("00000122-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IOleDropTarget
		{
			// Token: 0x060055C4 RID: 21956
			[PreserveSig]
			int OleDragEnter([MarshalAs(UnmanagedType.Interface)] [In] object pDataObj, [MarshalAs(UnmanagedType.U4)] [In] int grfKeyState, [In] UnsafeNativeMethods.POINTSTRUCT pt, [In] [Out] ref int pdwEffect);

			// Token: 0x060055C5 RID: 21957
			[PreserveSig]
			int OleDragOver([MarshalAs(UnmanagedType.U4)] [In] int grfKeyState, [In] UnsafeNativeMethods.POINTSTRUCT pt, [In] [Out] ref int pdwEffect);

			// Token: 0x060055C6 RID: 21958
			[PreserveSig]
			int OleDragLeave();

			// Token: 0x060055C7 RID: 21959
			[PreserveSig]
			int OleDrop([MarshalAs(UnmanagedType.Interface)] [In] object pDataObj, [MarshalAs(UnmanagedType.U4)] [In] int grfKeyState, [In] UnsafeNativeMethods.POINTSTRUCT pt, [In] [Out] ref int pdwEffect);
		}

		// Token: 0x0200055E RID: 1374
		[Guid("00000121-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IOleDropSource
		{
			// Token: 0x060055C8 RID: 21960
			[PreserveSig]
			int OleQueryContinueDrag(int fEscapePressed, [MarshalAs(UnmanagedType.U4)] [In] int grfKeyState);

			// Token: 0x060055C9 RID: 21961
			[PreserveSig]
			int OleGiveFeedback([MarshalAs(UnmanagedType.U4)] [In] int dwEffect);
		}

		// Token: 0x0200055F RID: 1375
		[Guid("00000016-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IOleMessageFilter
		{
			// Token: 0x060055CA RID: 21962
			[PreserveSig]
			int HandleInComingCall(int dwCallType, IntPtr hTaskCaller, int dwTickCount, IntPtr lpInterfaceInfo);

			// Token: 0x060055CB RID: 21963
			[PreserveSig]
			int RetryRejectedCall(IntPtr hTaskCallee, int dwTickCount, int dwRejectType);

			// Token: 0x060055CC RID: 21964
			[PreserveSig]
			int MessagePending(IntPtr hTaskCallee, int dwTickCount, int dwPendingType);
		}

		// Token: 0x02000560 RID: 1376
		[Guid("B196B289-BAB4-101A-B69C-00AA00341D07")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IOleControlSite
		{
			// Token: 0x060055CD RID: 21965
			[PreserveSig]
			int OnControlInfoChanged();

			// Token: 0x060055CE RID: 21966
			[PreserveSig]
			int LockInPlaceActive(int fLock);

			// Token: 0x060055CF RID: 21967
			[PreserveSig]
			int GetExtendedControl([MarshalAs(UnmanagedType.IDispatch)] out object ppDisp);

			// Token: 0x060055D0 RID: 21968
			[PreserveSig]
			int TransformCoords([In] [Out] NativeMethods._POINTL pPtlHimetric, [In] [Out] NativeMethods.tagPOINTF pPtfContainer, [MarshalAs(UnmanagedType.U4)] [In] int dwFlags);

			// Token: 0x060055D1 RID: 21969
			[PreserveSig]
			int TranslateAccelerator([In] ref NativeMethods.MSG pMsg, [MarshalAs(UnmanagedType.U4)] [In] int grfModifiers);

			// Token: 0x060055D2 RID: 21970
			[PreserveSig]
			int OnFocus(int fGotFocus);

			// Token: 0x060055D3 RID: 21971
			[PreserveSig]
			int ShowPropertyFrame();
		}

		// Token: 0x02000561 RID: 1377
		[Guid("00000118-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IOleClientSite
		{
			// Token: 0x060055D4 RID: 21972
			[PreserveSig]
			int SaveObject();

			// Token: 0x060055D5 RID: 21973
			[PreserveSig]
			int GetMoniker([MarshalAs(UnmanagedType.U4)] [In] int dwAssign, [MarshalAs(UnmanagedType.U4)] [In] int dwWhichMoniker, [MarshalAs(UnmanagedType.Interface)] out object moniker);

			// Token: 0x060055D6 RID: 21974
			[PreserveSig]
			int GetContainer(out UnsafeNativeMethods.IOleContainer container);

			// Token: 0x060055D7 RID: 21975
			[PreserveSig]
			int ShowObject();

			// Token: 0x060055D8 RID: 21976
			[PreserveSig]
			int OnShowWindow(int fShow);

			// Token: 0x060055D9 RID: 21977
			[PreserveSig]
			int RequestNewObjectLayout();
		}

		// Token: 0x02000562 RID: 1378
		[Guid("00000119-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IOleInPlaceSite
		{
			// Token: 0x060055DA RID: 21978
			IntPtr GetWindow();

			// Token: 0x060055DB RID: 21979
			[PreserveSig]
			int ContextSensitiveHelp(int fEnterMode);

			// Token: 0x060055DC RID: 21980
			[PreserveSig]
			int CanInPlaceActivate();

			// Token: 0x060055DD RID: 21981
			[PreserveSig]
			int OnInPlaceActivate();

			// Token: 0x060055DE RID: 21982
			[PreserveSig]
			int OnUIActivate();

			// Token: 0x060055DF RID: 21983
			[PreserveSig]
			int GetWindowContext([MarshalAs(UnmanagedType.Interface)] out UnsafeNativeMethods.IOleInPlaceFrame ppFrame, [MarshalAs(UnmanagedType.Interface)] out UnsafeNativeMethods.IOleInPlaceUIWindow ppDoc, [Out] NativeMethods.COMRECT lprcPosRect, [Out] NativeMethods.COMRECT lprcClipRect, [In] [Out] NativeMethods.tagOIFI lpFrameInfo);

			// Token: 0x060055E0 RID: 21984
			[PreserveSig]
			int Scroll(NativeMethods.tagSIZE scrollExtant);

			// Token: 0x060055E1 RID: 21985
			[PreserveSig]
			int OnUIDeactivate(int fUndoable);

			// Token: 0x060055E2 RID: 21986
			[PreserveSig]
			int OnInPlaceDeactivate();

			// Token: 0x060055E3 RID: 21987
			[PreserveSig]
			int DiscardUndoState();

			// Token: 0x060055E4 RID: 21988
			[PreserveSig]
			int DeactivateAndUndo();

			// Token: 0x060055E5 RID: 21989
			[PreserveSig]
			int OnPosRectChange([In] NativeMethods.COMRECT lprcPosRect);
		}

		// Token: 0x02000563 RID: 1379
		[Guid("742B0E01-14E6-101B-914E-00AA00300CAB")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface ISimpleFrameSite
		{
			// Token: 0x060055E6 RID: 21990
			[PreserveSig]
			int PreMessageFilter(IntPtr hwnd, [MarshalAs(UnmanagedType.U4)] [In] int msg, IntPtr wp, IntPtr lp, [In] [Out] ref IntPtr plResult, [MarshalAs(UnmanagedType.U4)] [In] [Out] ref int pdwCookie);

			// Token: 0x060055E7 RID: 21991
			[PreserveSig]
			int PostMessageFilter(IntPtr hwnd, [MarshalAs(UnmanagedType.U4)] [In] int msg, IntPtr wp, IntPtr lp, [In] [Out] ref IntPtr plResult, [MarshalAs(UnmanagedType.U4)] [In] int dwCookie);
		}

		// Token: 0x02000564 RID: 1380
		[Guid("40A050A0-3C31-101B-A82E-08002B2B2337")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IVBGetControl
		{
			// Token: 0x060055E8 RID: 21992
			[PreserveSig]
			int EnumControls(int dwOleContF, int dwWhich, out UnsafeNativeMethods.IEnumUnknown ppenum);
		}

		// Token: 0x02000565 RID: 1381
		[Guid("91733A60-3F4C-101B-A3F6-00AA0034E4E9")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IGetVBAObject
		{
			// Token: 0x060055E9 RID: 21993
			[PreserveSig]
			int GetObject([In] ref Guid riid, [MarshalAs(UnmanagedType.LPArray)] [Out] UnsafeNativeMethods.IVBFormat[] rval, int dwReserved);
		}

		// Token: 0x02000566 RID: 1382
		[Guid("9BFBBC02-EFF1-101A-84ED-00AA00341D07")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IPropertyNotifySink
		{
			// Token: 0x060055EA RID: 21994
			void OnChanged(int dispID);

			// Token: 0x060055EB RID: 21995
			[PreserveSig]
			int OnRequestEdit(int dispID);
		}

		// Token: 0x02000567 RID: 1383
		[Guid("9849FD60-3768-101B-8D72-AE6164FFE3CF")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IVBFormat
		{
			// Token: 0x060055EC RID: 21996
			[PreserveSig]
			int Format([In] ref object var, IntPtr pszFormat, IntPtr lpBuffer, short cpBuffer, int lcid, short firstD, short firstW, [MarshalAs(UnmanagedType.LPArray)] [Out] short[] result);
		}

		// Token: 0x02000568 RID: 1384
		[Guid("00000100-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IEnumUnknown
		{
			// Token: 0x060055ED RID: 21997
			[PreserveSig]
			int Next([MarshalAs(UnmanagedType.U4)] [In] int celt, [Out] IntPtr rgelt, IntPtr pceltFetched);

			// Token: 0x060055EE RID: 21998
			[PreserveSig]
			int Skip([MarshalAs(UnmanagedType.U4)] [In] int celt);

			// Token: 0x060055EF RID: 21999
			void Reset();

			// Token: 0x060055F0 RID: 22000
			void Clone(out UnsafeNativeMethods.IEnumUnknown ppenum);
		}

		// Token: 0x02000569 RID: 1385
		[Guid("0000011B-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IOleContainer
		{
			// Token: 0x060055F1 RID: 22001
			[PreserveSig]
			int ParseDisplayName([MarshalAs(UnmanagedType.Interface)] [In] object pbc, [MarshalAs(UnmanagedType.BStr)] [In] string pszDisplayName, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] pchEaten, [MarshalAs(UnmanagedType.LPArray)] [Out] object[] ppmkOut);

			// Token: 0x060055F2 RID: 22002
			[PreserveSig]
			int EnumObjects([MarshalAs(UnmanagedType.U4)] [In] int grfFlags, out UnsafeNativeMethods.IEnumUnknown ppenum);

			// Token: 0x060055F3 RID: 22003
			[PreserveSig]
			int LockContainer(bool fLock);
		}

		// Token: 0x0200056A RID: 1386
		[Guid("00000116-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IOleInPlaceFrame
		{
			// Token: 0x060055F4 RID: 22004
			IntPtr GetWindow();

			// Token: 0x060055F5 RID: 22005
			[PreserveSig]
			int ContextSensitiveHelp(int fEnterMode);

			// Token: 0x060055F6 RID: 22006
			[PreserveSig]
			int GetBorder([Out] NativeMethods.COMRECT lprectBorder);

			// Token: 0x060055F7 RID: 22007
			[PreserveSig]
			int RequestBorderSpace([In] NativeMethods.COMRECT pborderwidths);

			// Token: 0x060055F8 RID: 22008
			[PreserveSig]
			int SetBorderSpace([In] NativeMethods.COMRECT pborderwidths);

			// Token: 0x060055F9 RID: 22009
			[PreserveSig]
			int SetActiveObject([MarshalAs(UnmanagedType.Interface)] [In] UnsafeNativeMethods.IOleInPlaceActiveObject pActiveObject, [MarshalAs(UnmanagedType.LPWStr)] [In] string pszObjName);

			// Token: 0x060055FA RID: 22010
			[PreserveSig]
			int InsertMenus([In] IntPtr hmenuShared, [In] [Out] NativeMethods.tagOleMenuGroupWidths lpMenuWidths);

			// Token: 0x060055FB RID: 22011
			[PreserveSig]
			int SetMenu([In] IntPtr hmenuShared, [In] IntPtr holemenu, [In] IntPtr hwndActiveObject);

			// Token: 0x060055FC RID: 22012
			[PreserveSig]
			int RemoveMenus([In] IntPtr hmenuShared);

			// Token: 0x060055FD RID: 22013
			[PreserveSig]
			int SetStatusText([MarshalAs(UnmanagedType.LPWStr)] [In] string pszStatusText);

			// Token: 0x060055FE RID: 22014
			[PreserveSig]
			int EnableModeless(bool fEnable);

			// Token: 0x060055FF RID: 22015
			[PreserveSig]
			int TranslateAccelerator([In] ref NativeMethods.MSG lpmsg, [MarshalAs(UnmanagedType.U2)] [In] short wID);
		}

		// Token: 0x0200056B RID: 1387
		[ComVisible(true)]
		[Guid("BD3F23C0-D43E-11CF-893B-00AA00BDCE1A")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IDocHostUIHandler
		{
			// Token: 0x06005600 RID: 22016
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int ShowContextMenu([MarshalAs(UnmanagedType.U4)] [In] int dwID, [In] NativeMethods.POINT pt, [MarshalAs(UnmanagedType.Interface)] [In] object pcmdtReserved, [MarshalAs(UnmanagedType.Interface)] [In] object pdispReserved);

			// Token: 0x06005601 RID: 22017
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int GetHostInfo([In] [Out] NativeMethods.DOCHOSTUIINFO info);

			// Token: 0x06005602 RID: 22018
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int ShowUI([MarshalAs(UnmanagedType.I4)] [In] int dwID, [In] UnsafeNativeMethods.IOleInPlaceActiveObject activeObject, [In] NativeMethods.IOleCommandTarget commandTarget, [In] UnsafeNativeMethods.IOleInPlaceFrame frame, [In] UnsafeNativeMethods.IOleInPlaceUIWindow doc);

			// Token: 0x06005603 RID: 22019
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int HideUI();

			// Token: 0x06005604 RID: 22020
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int UpdateUI();

			// Token: 0x06005605 RID: 22021
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int EnableModeless([MarshalAs(UnmanagedType.Bool)] [In] bool fEnable);

			// Token: 0x06005606 RID: 22022
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int OnDocWindowActivate([MarshalAs(UnmanagedType.Bool)] [In] bool fActivate);

			// Token: 0x06005607 RID: 22023
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int OnFrameWindowActivate([MarshalAs(UnmanagedType.Bool)] [In] bool fActivate);

			// Token: 0x06005608 RID: 22024
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int ResizeBorder([In] NativeMethods.COMRECT rect, [In] UnsafeNativeMethods.IOleInPlaceUIWindow doc, bool fFrameWindow);

			// Token: 0x06005609 RID: 22025
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int TranslateAccelerator([In] ref NativeMethods.MSG msg, [In] ref Guid group, [MarshalAs(UnmanagedType.I4)] [In] int nCmdID);

			// Token: 0x0600560A RID: 22026
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int GetOptionKeyPath([MarshalAs(UnmanagedType.LPArray)] [Out] string[] pbstrKey, [MarshalAs(UnmanagedType.U4)] [In] int dw);

			// Token: 0x0600560B RID: 22027
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int GetDropTarget([MarshalAs(UnmanagedType.Interface)] [In] UnsafeNativeMethods.IOleDropTarget pDropTarget, [MarshalAs(UnmanagedType.Interface)] out UnsafeNativeMethods.IOleDropTarget ppDropTarget);

			// Token: 0x0600560C RID: 22028
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int GetExternal([MarshalAs(UnmanagedType.Interface)] out object ppDispatch);

			// Token: 0x0600560D RID: 22029
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int TranslateUrl([MarshalAs(UnmanagedType.U4)] [In] int dwTranslate, [MarshalAs(UnmanagedType.LPWStr)] [In] string strURLIn, [MarshalAs(UnmanagedType.LPWStr)] out string pstrURLOut);

			// Token: 0x0600560E RID: 22030
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int FilterDataObject(IDataObject pDO, out IDataObject ppDORet);
		}

		// Token: 0x0200056C RID: 1388
		[SuppressUnmanagedCodeSecurity]
		[Guid("D30C1661-CDAF-11d0-8A3E-00C04FC9E26E")]
		[TypeLibType(TypeLibTypeFlags.FHidden | TypeLibTypeFlags.FDual | TypeLibTypeFlags.FOleAutomation)]
		[ComImport]
		public interface IWebBrowser2
		{
			// Token: 0x0600560F RID: 22031
			[DispId(100)]
			void GoBack();

			// Token: 0x06005610 RID: 22032
			[DispId(101)]
			void GoForward();

			// Token: 0x06005611 RID: 22033
			[DispId(102)]
			void GoHome();

			// Token: 0x06005612 RID: 22034
			[DispId(103)]
			void GoSearch();

			// Token: 0x06005613 RID: 22035
			[DispId(104)]
			void Navigate([In] string Url, [In] ref object flags, [In] ref object targetFrameName, [In] ref object postData, [In] ref object headers);

			// Token: 0x06005614 RID: 22036
			[DispId(-550)]
			void Refresh();

			// Token: 0x06005615 RID: 22037
			[DispId(105)]
			void Refresh2([In] ref object level);

			// Token: 0x06005616 RID: 22038
			[DispId(106)]
			void Stop();

			// Token: 0x17001491 RID: 5265
			// (get) Token: 0x06005617 RID: 22039
			[DispId(200)]
			object Application
			{
				[return: MarshalAs(UnmanagedType.IDispatch)]
				get;
			}

			// Token: 0x17001492 RID: 5266
			// (get) Token: 0x06005618 RID: 22040
			[DispId(201)]
			object Parent
			{
				[return: MarshalAs(UnmanagedType.IDispatch)]
				get;
			}

			// Token: 0x17001493 RID: 5267
			// (get) Token: 0x06005619 RID: 22041
			[DispId(202)]
			object Container
			{
				[return: MarshalAs(UnmanagedType.IDispatch)]
				get;
			}

			// Token: 0x17001494 RID: 5268
			// (get) Token: 0x0600561A RID: 22042
			[DispId(203)]
			object Document
			{
				[return: MarshalAs(UnmanagedType.IDispatch)]
				get;
			}

			// Token: 0x17001495 RID: 5269
			// (get) Token: 0x0600561B RID: 22043
			[DispId(204)]
			bool TopLevelContainer { get; }

			// Token: 0x17001496 RID: 5270
			// (get) Token: 0x0600561C RID: 22044
			[DispId(205)]
			string Type { get; }

			// Token: 0x17001497 RID: 5271
			// (get) Token: 0x0600561D RID: 22045
			// (set) Token: 0x0600561E RID: 22046
			[DispId(206)]
			int Left { get; set; }

			// Token: 0x17001498 RID: 5272
			// (get) Token: 0x0600561F RID: 22047
			// (set) Token: 0x06005620 RID: 22048
			[DispId(207)]
			int Top { get; set; }

			// Token: 0x17001499 RID: 5273
			// (get) Token: 0x06005621 RID: 22049
			// (set) Token: 0x06005622 RID: 22050
			[DispId(208)]
			int Width { get; set; }

			// Token: 0x1700149A RID: 5274
			// (get) Token: 0x06005623 RID: 22051
			// (set) Token: 0x06005624 RID: 22052
			[DispId(209)]
			int Height { get; set; }

			// Token: 0x1700149B RID: 5275
			// (get) Token: 0x06005625 RID: 22053
			[DispId(210)]
			string LocationName { get; }

			// Token: 0x1700149C RID: 5276
			// (get) Token: 0x06005626 RID: 22054
			[DispId(211)]
			string LocationURL { get; }

			// Token: 0x1700149D RID: 5277
			// (get) Token: 0x06005627 RID: 22055
			[DispId(212)]
			bool Busy { get; }

			// Token: 0x06005628 RID: 22056
			[DispId(300)]
			void Quit();

			// Token: 0x06005629 RID: 22057
			[DispId(301)]
			void ClientToWindow(out int pcx, out int pcy);

			// Token: 0x0600562A RID: 22058
			[DispId(302)]
			void PutProperty([In] string property, [In] object vtValue);

			// Token: 0x0600562B RID: 22059
			[DispId(303)]
			object GetProperty([In] string property);

			// Token: 0x1700149E RID: 5278
			// (get) Token: 0x0600562C RID: 22060
			[DispId(0)]
			string Name { get; }

			// Token: 0x1700149F RID: 5279
			// (get) Token: 0x0600562D RID: 22061
			[DispId(-515)]
			int HWND { get; }

			// Token: 0x170014A0 RID: 5280
			// (get) Token: 0x0600562E RID: 22062
			[DispId(400)]
			string FullName { get; }

			// Token: 0x170014A1 RID: 5281
			// (get) Token: 0x0600562F RID: 22063
			[DispId(401)]
			string Path { get; }

			// Token: 0x170014A2 RID: 5282
			// (get) Token: 0x06005630 RID: 22064
			// (set) Token: 0x06005631 RID: 22065
			[DispId(402)]
			bool Visible { get; set; }

			// Token: 0x170014A3 RID: 5283
			// (get) Token: 0x06005632 RID: 22066
			// (set) Token: 0x06005633 RID: 22067
			[DispId(403)]
			bool StatusBar { get; set; }

			// Token: 0x170014A4 RID: 5284
			// (get) Token: 0x06005634 RID: 22068
			// (set) Token: 0x06005635 RID: 22069
			[DispId(404)]
			string StatusText { get; set; }

			// Token: 0x170014A5 RID: 5285
			// (get) Token: 0x06005636 RID: 22070
			// (set) Token: 0x06005637 RID: 22071
			[DispId(405)]
			int ToolBar { get; set; }

			// Token: 0x170014A6 RID: 5286
			// (get) Token: 0x06005638 RID: 22072
			// (set) Token: 0x06005639 RID: 22073
			[DispId(406)]
			bool MenuBar { get; set; }

			// Token: 0x170014A7 RID: 5287
			// (get) Token: 0x0600563A RID: 22074
			// (set) Token: 0x0600563B RID: 22075
			[DispId(407)]
			bool FullScreen { get; set; }

			// Token: 0x0600563C RID: 22076
			[DispId(500)]
			void Navigate2([In] ref object URL, [In] ref object flags, [In] ref object targetFrameName, [In] ref object postData, [In] ref object headers);

			// Token: 0x0600563D RID: 22077
			[DispId(501)]
			NativeMethods.OLECMDF QueryStatusWB([In] NativeMethods.OLECMDID cmdID);

			// Token: 0x0600563E RID: 22078
			[DispId(502)]
			void ExecWB([In] NativeMethods.OLECMDID cmdID, [In] NativeMethods.OLECMDEXECOPT cmdexecopt, ref object pvaIn, IntPtr pvaOut);

			// Token: 0x0600563F RID: 22079
			[DispId(503)]
			void ShowBrowserBar([In] ref object pvaClsid, [In] ref object pvarShow, [In] ref object pvarSize);

			// Token: 0x170014A8 RID: 5288
			// (get) Token: 0x06005640 RID: 22080
			[DispId(-525)]
			WebBrowserReadyState ReadyState { get; }

			// Token: 0x170014A9 RID: 5289
			// (get) Token: 0x06005641 RID: 22081
			// (set) Token: 0x06005642 RID: 22082
			[DispId(550)]
			bool Offline { get; set; }

			// Token: 0x170014AA RID: 5290
			// (get) Token: 0x06005643 RID: 22083
			// (set) Token: 0x06005644 RID: 22084
			[DispId(551)]
			bool Silent { get; set; }

			// Token: 0x170014AB RID: 5291
			// (get) Token: 0x06005645 RID: 22085
			// (set) Token: 0x06005646 RID: 22086
			[DispId(552)]
			bool RegisterAsBrowser { get; set; }

			// Token: 0x170014AC RID: 5292
			// (get) Token: 0x06005647 RID: 22087
			// (set) Token: 0x06005648 RID: 22088
			[DispId(553)]
			bool RegisterAsDropTarget { get; set; }

			// Token: 0x170014AD RID: 5293
			// (get) Token: 0x06005649 RID: 22089
			// (set) Token: 0x0600564A RID: 22090
			[DispId(554)]
			bool TheaterMode { get; set; }

			// Token: 0x170014AE RID: 5294
			// (get) Token: 0x0600564B RID: 22091
			// (set) Token: 0x0600564C RID: 22092
			[DispId(555)]
			bool AddressBar { get; set; }

			// Token: 0x170014AF RID: 5295
			// (get) Token: 0x0600564D RID: 22093
			// (set) Token: 0x0600564E RID: 22094
			[DispId(556)]
			bool Resizable { get; set; }
		}

		// Token: 0x0200056D RID: 1389
		[Guid("34A715A0-6587-11D0-924A-0020AFC7AC4D")]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		[TypeLibType(TypeLibTypeFlags.FHidden)]
		[ComImport]
		public interface DWebBrowserEvents2
		{
			// Token: 0x0600564F RID: 22095
			[DispId(102)]
			void StatusTextChange([In] string text);

			// Token: 0x06005650 RID: 22096
			[DispId(108)]
			void ProgressChange([In] int progress, [In] int progressMax);

			// Token: 0x06005651 RID: 22097
			[DispId(105)]
			void CommandStateChange([In] long command, [In] bool enable);

			// Token: 0x06005652 RID: 22098
			[DispId(106)]
			void DownloadBegin();

			// Token: 0x06005653 RID: 22099
			[DispId(104)]
			void DownloadComplete();

			// Token: 0x06005654 RID: 22100
			[DispId(113)]
			void TitleChange([In] string text);

			// Token: 0x06005655 RID: 22101
			[DispId(112)]
			void PropertyChange([In] string szProperty);

			// Token: 0x06005656 RID: 22102
			[DispId(250)]
			void BeforeNavigate2([MarshalAs(UnmanagedType.IDispatch)] [In] object pDisp, [In] ref object URL, [In] ref object flags, [In] ref object targetFrameName, [In] ref object postData, [In] ref object headers, [In] [Out] ref bool cancel);

			// Token: 0x06005657 RID: 22103
			[DispId(251)]
			void NewWindow2([MarshalAs(UnmanagedType.IDispatch)] [In] [Out] ref object pDisp, [In] [Out] ref bool cancel);

			// Token: 0x06005658 RID: 22104
			[DispId(252)]
			void NavigateComplete2([MarshalAs(UnmanagedType.IDispatch)] [In] object pDisp, [In] ref object URL);

			// Token: 0x06005659 RID: 22105
			[DispId(259)]
			void DocumentComplete([MarshalAs(UnmanagedType.IDispatch)] [In] object pDisp, [In] ref object URL);

			// Token: 0x0600565A RID: 22106
			[DispId(253)]
			void OnQuit();

			// Token: 0x0600565B RID: 22107
			[DispId(254)]
			void OnVisible([In] bool visible);

			// Token: 0x0600565C RID: 22108
			[DispId(255)]
			void OnToolBar([In] bool toolBar);

			// Token: 0x0600565D RID: 22109
			[DispId(256)]
			void OnMenuBar([In] bool menuBar);

			// Token: 0x0600565E RID: 22110
			[DispId(257)]
			void OnStatusBar([In] bool statusBar);

			// Token: 0x0600565F RID: 22111
			[DispId(258)]
			void OnFullScreen([In] bool fullScreen);

			// Token: 0x06005660 RID: 22112
			[DispId(260)]
			void OnTheaterMode([In] bool theaterMode);

			// Token: 0x06005661 RID: 22113
			[DispId(262)]
			void WindowSetResizable([In] bool resizable);

			// Token: 0x06005662 RID: 22114
			[DispId(264)]
			void WindowSetLeft([In] int left);

			// Token: 0x06005663 RID: 22115
			[DispId(265)]
			void WindowSetTop([In] int top);

			// Token: 0x06005664 RID: 22116
			[DispId(266)]
			void WindowSetWidth([In] int width);

			// Token: 0x06005665 RID: 22117
			[DispId(267)]
			void WindowSetHeight([In] int height);

			// Token: 0x06005666 RID: 22118
			[DispId(263)]
			void WindowClosing([In] bool isChildWindow, [In] [Out] ref bool cancel);

			// Token: 0x06005667 RID: 22119
			[DispId(268)]
			void ClientToHostWindow([In] [Out] ref long cx, [In] [Out] ref long cy);

			// Token: 0x06005668 RID: 22120
			[DispId(269)]
			void SetSecureLockIcon([In] int secureLockIcon);

			// Token: 0x06005669 RID: 22121
			[DispId(270)]
			void FileDownload([In] [Out] ref bool cancel);

			// Token: 0x0600566A RID: 22122
			[DispId(271)]
			void NavigateError([MarshalAs(UnmanagedType.IDispatch)] [In] object pDisp, [In] ref object URL, [In] ref object frame, [In] ref object statusCode, [In] [Out] ref bool cancel);

			// Token: 0x0600566B RID: 22123
			[DispId(225)]
			void PrintTemplateInstantiation([MarshalAs(UnmanagedType.IDispatch)] [In] object pDisp);

			// Token: 0x0600566C RID: 22124
			[DispId(226)]
			void PrintTemplateTeardown([MarshalAs(UnmanagedType.IDispatch)] [In] object pDisp);

			// Token: 0x0600566D RID: 22125
			[DispId(227)]
			void UpdatePageStatus([MarshalAs(UnmanagedType.IDispatch)] [In] object pDisp, [In] ref object nPage, [In] ref object fDone);

			// Token: 0x0600566E RID: 22126
			[DispId(272)]
			void PrivacyImpactedStateChange([In] bool bImpacted);
		}

		// Token: 0x0200056E RID: 1390
		[SuppressUnmanagedCodeSecurity]
		[ComVisible(true)]
		[Guid("626FC520-A41E-11cf-A731-00A0C9082637")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLDocument
		{
			// Token: 0x0600566F RID: 22127
			[return: MarshalAs(UnmanagedType.IDispatch)]
			object GetScript();
		}

		// Token: 0x0200056F RID: 1391
		[SuppressUnmanagedCodeSecurity]
		[ComVisible(true)]
		[Guid("332C4425-26CB-11D0-B483-00C04FD90119")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLDocument2
		{
			// Token: 0x06005670 RID: 22128
			[return: MarshalAs(UnmanagedType.IDispatch)]
			object GetScript();

			// Token: 0x06005671 RID: 22129
			UnsafeNativeMethods.IHTMLElementCollection GetAll();

			// Token: 0x06005672 RID: 22130
			UnsafeNativeMethods.IHTMLElement GetBody();

			// Token: 0x06005673 RID: 22131
			UnsafeNativeMethods.IHTMLElement GetActiveElement();

			// Token: 0x06005674 RID: 22132
			UnsafeNativeMethods.IHTMLElementCollection GetImages();

			// Token: 0x06005675 RID: 22133
			UnsafeNativeMethods.IHTMLElementCollection GetApplets();

			// Token: 0x06005676 RID: 22134
			UnsafeNativeMethods.IHTMLElementCollection GetLinks();

			// Token: 0x06005677 RID: 22135
			UnsafeNativeMethods.IHTMLElementCollection GetForms();

			// Token: 0x06005678 RID: 22136
			UnsafeNativeMethods.IHTMLElementCollection GetAnchors();

			// Token: 0x06005679 RID: 22137
			void SetTitle(string p);

			// Token: 0x0600567A RID: 22138
			string GetTitle();

			// Token: 0x0600567B RID: 22139
			UnsafeNativeMethods.IHTMLElementCollection GetScripts();

			// Token: 0x0600567C RID: 22140
			void SetDesignMode(string p);

			// Token: 0x0600567D RID: 22141
			string GetDesignMode();

			// Token: 0x0600567E RID: 22142
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetSelection();

			// Token: 0x0600567F RID: 22143
			string GetReadyState();

			// Token: 0x06005680 RID: 22144
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetFrames();

			// Token: 0x06005681 RID: 22145
			UnsafeNativeMethods.IHTMLElementCollection GetEmbeds();

			// Token: 0x06005682 RID: 22146
			UnsafeNativeMethods.IHTMLElementCollection GetPlugins();

			// Token: 0x06005683 RID: 22147
			void SetAlinkColor(object c);

			// Token: 0x06005684 RID: 22148
			object GetAlinkColor();

			// Token: 0x06005685 RID: 22149
			void SetBgColor(object c);

			// Token: 0x06005686 RID: 22150
			object GetBgColor();

			// Token: 0x06005687 RID: 22151
			void SetFgColor(object c);

			// Token: 0x06005688 RID: 22152
			object GetFgColor();

			// Token: 0x06005689 RID: 22153
			void SetLinkColor(object c);

			// Token: 0x0600568A RID: 22154
			object GetLinkColor();

			// Token: 0x0600568B RID: 22155
			void SetVlinkColor(object c);

			// Token: 0x0600568C RID: 22156
			object GetVlinkColor();

			// Token: 0x0600568D RID: 22157
			string GetReferrer();

			// Token: 0x0600568E RID: 22158
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.IHTMLLocation GetLocation();

			// Token: 0x0600568F RID: 22159
			string GetLastModified();

			// Token: 0x06005690 RID: 22160
			void SetUrl(string p);

			// Token: 0x06005691 RID: 22161
			string GetUrl();

			// Token: 0x06005692 RID: 22162
			void SetDomain(string p);

			// Token: 0x06005693 RID: 22163
			string GetDomain();

			// Token: 0x06005694 RID: 22164
			void SetCookie(string p);

			// Token: 0x06005695 RID: 22165
			string GetCookie();

			// Token: 0x06005696 RID: 22166
			void SetExpando(bool p);

			// Token: 0x06005697 RID: 22167
			bool GetExpando();

			// Token: 0x06005698 RID: 22168
			void SetCharset(string p);

			// Token: 0x06005699 RID: 22169
			string GetCharset();

			// Token: 0x0600569A RID: 22170
			void SetDefaultCharset(string p);

			// Token: 0x0600569B RID: 22171
			string GetDefaultCharset();

			// Token: 0x0600569C RID: 22172
			string GetMimeType();

			// Token: 0x0600569D RID: 22173
			string GetFileSize();

			// Token: 0x0600569E RID: 22174
			string GetFileCreatedDate();

			// Token: 0x0600569F RID: 22175
			string GetFileModifiedDate();

			// Token: 0x060056A0 RID: 22176
			string GetFileUpdatedDate();

			// Token: 0x060056A1 RID: 22177
			string GetSecurity();

			// Token: 0x060056A2 RID: 22178
			string GetProtocol();

			// Token: 0x060056A3 RID: 22179
			string GetNameProp();

			// Token: 0x060056A4 RID: 22180
			int Write([MarshalAs(UnmanagedType.SafeArray)] [In] object[] psarray);

			// Token: 0x060056A5 RID: 22181
			int WriteLine([MarshalAs(UnmanagedType.SafeArray)] [In] object[] psarray);

			// Token: 0x060056A6 RID: 22182
			[return: MarshalAs(UnmanagedType.Interface)]
			object Open(string mimeExtension, object name, object features, object replace);

			// Token: 0x060056A7 RID: 22183
			void Close();

			// Token: 0x060056A8 RID: 22184
			void Clear();

			// Token: 0x060056A9 RID: 22185
			bool QueryCommandSupported(string cmdID);

			// Token: 0x060056AA RID: 22186
			bool QueryCommandEnabled(string cmdID);

			// Token: 0x060056AB RID: 22187
			bool QueryCommandState(string cmdID);

			// Token: 0x060056AC RID: 22188
			bool QueryCommandIndeterm(string cmdID);

			// Token: 0x060056AD RID: 22189
			string QueryCommandText(string cmdID);

			// Token: 0x060056AE RID: 22190
			object QueryCommandValue(string cmdID);

			// Token: 0x060056AF RID: 22191
			bool ExecCommand(string cmdID, bool showUI, object value);

			// Token: 0x060056B0 RID: 22192
			bool ExecCommandShowHelp(string cmdID);

			// Token: 0x060056B1 RID: 22193
			UnsafeNativeMethods.IHTMLElement CreateElement(string eTag);

			// Token: 0x060056B2 RID: 22194
			void SetOnhelp(object p);

			// Token: 0x060056B3 RID: 22195
			object GetOnhelp();

			// Token: 0x060056B4 RID: 22196
			void SetOnclick(object p);

			// Token: 0x060056B5 RID: 22197
			object GetOnclick();

			// Token: 0x060056B6 RID: 22198
			void SetOndblclick(object p);

			// Token: 0x060056B7 RID: 22199
			object GetOndblclick();

			// Token: 0x060056B8 RID: 22200
			void SetOnkeyup(object p);

			// Token: 0x060056B9 RID: 22201
			object GetOnkeyup();

			// Token: 0x060056BA RID: 22202
			void SetOnkeydown(object p);

			// Token: 0x060056BB RID: 22203
			object GetOnkeydown();

			// Token: 0x060056BC RID: 22204
			void SetOnkeypress(object p);

			// Token: 0x060056BD RID: 22205
			object GetOnkeypress();

			// Token: 0x060056BE RID: 22206
			void SetOnmouseup(object p);

			// Token: 0x060056BF RID: 22207
			object GetOnmouseup();

			// Token: 0x060056C0 RID: 22208
			void SetOnmousedown(object p);

			// Token: 0x060056C1 RID: 22209
			object GetOnmousedown();

			// Token: 0x060056C2 RID: 22210
			void SetOnmousemove(object p);

			// Token: 0x060056C3 RID: 22211
			object GetOnmousemove();

			// Token: 0x060056C4 RID: 22212
			void SetOnmouseout(object p);

			// Token: 0x060056C5 RID: 22213
			object GetOnmouseout();

			// Token: 0x060056C6 RID: 22214
			void SetOnmouseover(object p);

			// Token: 0x060056C7 RID: 22215
			object GetOnmouseover();

			// Token: 0x060056C8 RID: 22216
			void SetOnreadystatechange(object p);

			// Token: 0x060056C9 RID: 22217
			object GetOnreadystatechange();

			// Token: 0x060056CA RID: 22218
			void SetOnafterupdate(object p);

			// Token: 0x060056CB RID: 22219
			object GetOnafterupdate();

			// Token: 0x060056CC RID: 22220
			void SetOnrowexit(object p);

			// Token: 0x060056CD RID: 22221
			object GetOnrowexit();

			// Token: 0x060056CE RID: 22222
			void SetOnrowenter(object p);

			// Token: 0x060056CF RID: 22223
			object GetOnrowenter();

			// Token: 0x060056D0 RID: 22224
			void SetOndragstart(object p);

			// Token: 0x060056D1 RID: 22225
			object GetOndragstart();

			// Token: 0x060056D2 RID: 22226
			void SetOnselectstart(object p);

			// Token: 0x060056D3 RID: 22227
			object GetOnselectstart();

			// Token: 0x060056D4 RID: 22228
			UnsafeNativeMethods.IHTMLElement ElementFromPoint(int x, int y);

			// Token: 0x060056D5 RID: 22229
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.IHTMLWindow2 GetParentWindow();

			// Token: 0x060056D6 RID: 22230
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetStyleSheets();

			// Token: 0x060056D7 RID: 22231
			void SetOnbeforeupdate(object p);

			// Token: 0x060056D8 RID: 22232
			object GetOnbeforeupdate();

			// Token: 0x060056D9 RID: 22233
			void SetOnerrorupdate(object p);

			// Token: 0x060056DA RID: 22234
			object GetOnerrorupdate();

			// Token: 0x060056DB RID: 22235
			string toString();

			// Token: 0x060056DC RID: 22236
			[return: MarshalAs(UnmanagedType.Interface)]
			object CreateStyleSheet(string bstrHref, int lIndex);
		}

		// Token: 0x02000570 RID: 1392
		[SuppressUnmanagedCodeSecurity]
		[ComVisible(true)]
		[Guid("3050F485-98B5-11CF-BB82-00AA00BDCE0B")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLDocument3
		{
			// Token: 0x060056DD RID: 22237
			void ReleaseCapture();

			// Token: 0x060056DE RID: 22238
			void Recalc([In] bool fForce);

			// Token: 0x060056DF RID: 22239
			object CreateTextNode([In] string text);

			// Token: 0x060056E0 RID: 22240
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.IHTMLElement GetDocumentElement();

			// Token: 0x060056E1 RID: 22241
			string GetUniqueID();

			// Token: 0x060056E2 RID: 22242
			bool AttachEvent([In] string ev, [MarshalAs(UnmanagedType.IDispatch)] [In] object pdisp);

			// Token: 0x060056E3 RID: 22243
			void DetachEvent([In] string ev, [MarshalAs(UnmanagedType.IDispatch)] [In] object pdisp);

			// Token: 0x060056E4 RID: 22244
			void SetOnrowsdelete([In] object p);

			// Token: 0x060056E5 RID: 22245
			object GetOnrowsdelete();

			// Token: 0x060056E6 RID: 22246
			void SetOnrowsinserted([In] object p);

			// Token: 0x060056E7 RID: 22247
			object GetOnrowsinserted();

			// Token: 0x060056E8 RID: 22248
			void SetOncellchange([In] object p);

			// Token: 0x060056E9 RID: 22249
			object GetOncellchange();

			// Token: 0x060056EA RID: 22250
			void SetOndatasetchanged([In] object p);

			// Token: 0x060056EB RID: 22251
			object GetOndatasetchanged();

			// Token: 0x060056EC RID: 22252
			void SetOndataavailable([In] object p);

			// Token: 0x060056ED RID: 22253
			object GetOndataavailable();

			// Token: 0x060056EE RID: 22254
			void SetOndatasetcomplete([In] object p);

			// Token: 0x060056EF RID: 22255
			object GetOndatasetcomplete();

			// Token: 0x060056F0 RID: 22256
			void SetOnpropertychange([In] object p);

			// Token: 0x060056F1 RID: 22257
			object GetOnpropertychange();

			// Token: 0x060056F2 RID: 22258
			void SetDir([In] string p);

			// Token: 0x060056F3 RID: 22259
			string GetDir();

			// Token: 0x060056F4 RID: 22260
			void SetOncontextmenu([In] object p);

			// Token: 0x060056F5 RID: 22261
			object GetOncontextmenu();

			// Token: 0x060056F6 RID: 22262
			void SetOnstop([In] object p);

			// Token: 0x060056F7 RID: 22263
			object GetOnstop();

			// Token: 0x060056F8 RID: 22264
			object CreateDocumentFragment();

			// Token: 0x060056F9 RID: 22265
			object GetParentDocument();

			// Token: 0x060056FA RID: 22266
			void SetEnableDownload([In] bool p);

			// Token: 0x060056FB RID: 22267
			bool GetEnableDownload();

			// Token: 0x060056FC RID: 22268
			void SetBaseUrl([In] string p);

			// Token: 0x060056FD RID: 22269
			string GetBaseUrl();

			// Token: 0x060056FE RID: 22270
			[return: MarshalAs(UnmanagedType.IDispatch)]
			object GetChildNodes();

			// Token: 0x060056FF RID: 22271
			void SetInheritStyleSheets([In] bool p);

			// Token: 0x06005700 RID: 22272
			bool GetInheritStyleSheets();

			// Token: 0x06005701 RID: 22273
			void SetOnbeforeeditfocus([In] object p);

			// Token: 0x06005702 RID: 22274
			object GetOnbeforeeditfocus();

			// Token: 0x06005703 RID: 22275
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.IHTMLElementCollection GetElementsByName([In] string v);

			// Token: 0x06005704 RID: 22276
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.IHTMLElement GetElementById([In] string v);

			// Token: 0x06005705 RID: 22277
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.IHTMLElementCollection GetElementsByTagName([In] string v);
		}

		// Token: 0x02000571 RID: 1393
		[SuppressUnmanagedCodeSecurity]
		[ComVisible(true)]
		[Guid("3050F69A-98B5-11CF-BB82-00AA00BDCE0B")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLDocument4
		{
			// Token: 0x06005706 RID: 22278
			void Focus();

			// Token: 0x06005707 RID: 22279
			bool HasFocus();

			// Token: 0x06005708 RID: 22280
			void SetOnselectionchange(object p);

			// Token: 0x06005709 RID: 22281
			object GetOnselectionchange();

			// Token: 0x0600570A RID: 22282
			object GetNamespaces();

			// Token: 0x0600570B RID: 22283
			object createDocumentFromUrl(string bstrUrl, string bstrOptions);

			// Token: 0x0600570C RID: 22284
			void SetMedia(string bstrMedia);

			// Token: 0x0600570D RID: 22285
			string GetMedia();

			// Token: 0x0600570E RID: 22286
			object CreateEventObject([In] [Optional] ref object eventObject);

			// Token: 0x0600570F RID: 22287
			bool FireEvent(string eventName);

			// Token: 0x06005710 RID: 22288
			object CreateRenderStyle(string bstr);

			// Token: 0x06005711 RID: 22289
			void SetOncontrolselect(object p);

			// Token: 0x06005712 RID: 22290
			object GetOncontrolselect();

			// Token: 0x06005713 RID: 22291
			string GetURLUnencoded();
		}

		// Token: 0x02000572 RID: 1394
		[Guid("3050f613-98b5-11cf-bb82-00aa00bdce0b")]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		[TypeLibType(TypeLibTypeFlags.FHidden)]
		[ComImport]
		public interface DHTMLDocumentEvents2
		{
			// Token: 0x06005714 RID: 22292
			[DispId(-2147418102)]
			bool onhelp(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005715 RID: 22293
			[DispId(-600)]
			bool onclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005716 RID: 22294
			[DispId(-601)]
			bool ondblclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005717 RID: 22295
			[DispId(-602)]
			void onkeydown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005718 RID: 22296
			[DispId(-604)]
			void onkeyup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005719 RID: 22297
			[DispId(-603)]
			bool onkeypress(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600571A RID: 22298
			[DispId(-605)]
			void onmousedown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600571B RID: 22299
			[DispId(-606)]
			void onmousemove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600571C RID: 22300
			[DispId(-607)]
			void onmouseup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600571D RID: 22301
			[DispId(-2147418103)]
			void onmouseout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600571E RID: 22302
			[DispId(-2147418104)]
			void onmouseover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600571F RID: 22303
			[DispId(-609)]
			void onreadystatechange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005720 RID: 22304
			[DispId(-2147418108)]
			bool onbeforeupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005721 RID: 22305
			[DispId(-2147418107)]
			void onafterupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005722 RID: 22306
			[DispId(-2147418106)]
			bool onrowexit(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005723 RID: 22307
			[DispId(-2147418105)]
			void onrowenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005724 RID: 22308
			[DispId(-2147418101)]
			bool ondragstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005725 RID: 22309
			[DispId(-2147418100)]
			bool onselectstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005726 RID: 22310
			[DispId(-2147418099)]
			bool onerrorupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005727 RID: 22311
			[DispId(1023)]
			bool oncontextmenu(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005728 RID: 22312
			[DispId(1026)]
			bool onstop(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005729 RID: 22313
			[DispId(-2147418080)]
			void onrowsdelete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600572A RID: 22314
			[DispId(-2147418079)]
			void onrowsinserted(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600572B RID: 22315
			[DispId(-2147418078)]
			void oncellchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600572C RID: 22316
			[DispId(-2147418093)]
			void onpropertychange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600572D RID: 22317
			[DispId(-2147418098)]
			void ondatasetchanged(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600572E RID: 22318
			[DispId(-2147418097)]
			void ondataavailable(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600572F RID: 22319
			[DispId(-2147418096)]
			void ondatasetcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005730 RID: 22320
			[DispId(1027)]
			void onbeforeeditfocus(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005731 RID: 22321
			[DispId(1037)]
			void onselectionchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005732 RID: 22322
			[DispId(1036)]
			bool oncontrolselect(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005733 RID: 22323
			[DispId(1033)]
			bool onmousewheel(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005734 RID: 22324
			[DispId(1048)]
			void onfocusin(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005735 RID: 22325
			[DispId(1049)]
			void onfocusout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005736 RID: 22326
			[DispId(1044)]
			void onactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005737 RID: 22327
			[DispId(1045)]
			void ondeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005738 RID: 22328
			[DispId(1047)]
			bool onbeforeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005739 RID: 22329
			[DispId(1034)]
			bool onbeforedeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);
		}

		// Token: 0x02000573 RID: 1395
		[SuppressUnmanagedCodeSecurity]
		[ComVisible(true)]
		[Guid("332C4426-26CB-11D0-B483-00C04FD90119")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLFramesCollection2
		{
			// Token: 0x0600573A RID: 22330
			object Item(ref object idOrName);

			// Token: 0x0600573B RID: 22331
			int GetLength();
		}

		// Token: 0x02000574 RID: 1396
		[SuppressUnmanagedCodeSecurity]
		[ComVisible(true)]
		[Guid("332C4427-26CB-11D0-B483-00C04FD90119")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		public interface IHTMLWindow2
		{
			// Token: 0x0600573C RID: 22332
			[return: MarshalAs(UnmanagedType.IDispatch)]
			object Item([In] ref object pvarIndex);

			// Token: 0x0600573D RID: 22333
			int GetLength();

			// Token: 0x0600573E RID: 22334
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.IHTMLFramesCollection2 GetFrames();

			// Token: 0x0600573F RID: 22335
			void SetDefaultStatus([In] string p);

			// Token: 0x06005740 RID: 22336
			string GetDefaultStatus();

			// Token: 0x06005741 RID: 22337
			void SetStatus([In] string p);

			// Token: 0x06005742 RID: 22338
			string GetStatus();

			// Token: 0x06005743 RID: 22339
			int SetTimeout([In] string expression, [In] int msec, [In] ref object language);

			// Token: 0x06005744 RID: 22340
			void ClearTimeout([In] int timerID);

			// Token: 0x06005745 RID: 22341
			void Alert([In] string message);

			// Token: 0x06005746 RID: 22342
			bool Confirm([In] string message);

			// Token: 0x06005747 RID: 22343
			[return: MarshalAs(UnmanagedType.Struct)]
			object Prompt([In] string message, [In] string defstr);

			// Token: 0x06005748 RID: 22344
			object GetImage();

			// Token: 0x06005749 RID: 22345
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.IHTMLLocation GetLocation();

			// Token: 0x0600574A RID: 22346
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.IOmHistory GetHistory();

			// Token: 0x0600574B RID: 22347
			void Close();

			// Token: 0x0600574C RID: 22348
			void SetOpener([In] object p);

			// Token: 0x0600574D RID: 22349
			[return: MarshalAs(UnmanagedType.IDispatch)]
			object GetOpener();

			// Token: 0x0600574E RID: 22350
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.IOmNavigator GetNavigator();

			// Token: 0x0600574F RID: 22351
			void SetName([In] string p);

			// Token: 0x06005750 RID: 22352
			string GetName();

			// Token: 0x06005751 RID: 22353
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.IHTMLWindow2 GetParent();

			// Token: 0x06005752 RID: 22354
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.IHTMLWindow2 Open([In] string URL, [In] string name, [In] string features, [In] bool replace);

			// Token: 0x06005753 RID: 22355
			object GetSelf();

			// Token: 0x06005754 RID: 22356
			object GetTop();

			// Token: 0x06005755 RID: 22357
			object GetWindow();

			// Token: 0x06005756 RID: 22358
			void Navigate([In] string URL);

			// Token: 0x06005757 RID: 22359
			void SetOnfocus([In] object p);

			// Token: 0x06005758 RID: 22360
			object GetOnfocus();

			// Token: 0x06005759 RID: 22361
			void SetOnblur([In] object p);

			// Token: 0x0600575A RID: 22362
			object GetOnblur();

			// Token: 0x0600575B RID: 22363
			void SetOnload([In] object p);

			// Token: 0x0600575C RID: 22364
			object GetOnload();

			// Token: 0x0600575D RID: 22365
			void SetOnbeforeunload(object p);

			// Token: 0x0600575E RID: 22366
			object GetOnbeforeunload();

			// Token: 0x0600575F RID: 22367
			void SetOnunload([In] object p);

			// Token: 0x06005760 RID: 22368
			object GetOnunload();

			// Token: 0x06005761 RID: 22369
			void SetOnhelp(object p);

			// Token: 0x06005762 RID: 22370
			object GetOnhelp();

			// Token: 0x06005763 RID: 22371
			void SetOnerror([In] object p);

			// Token: 0x06005764 RID: 22372
			object GetOnerror();

			// Token: 0x06005765 RID: 22373
			void SetOnresize([In] object p);

			// Token: 0x06005766 RID: 22374
			object GetOnresize();

			// Token: 0x06005767 RID: 22375
			void SetOnscroll([In] object p);

			// Token: 0x06005768 RID: 22376
			object GetOnscroll();

			// Token: 0x06005769 RID: 22377
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.IHTMLDocument2 GetDocument();

			// Token: 0x0600576A RID: 22378
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.IHTMLEventObj GetEvent();

			// Token: 0x0600576B RID: 22379
			object Get_newEnum();

			// Token: 0x0600576C RID: 22380
			object ShowModalDialog([In] string dialog, [In] ref object varArgIn, [In] ref object varOptions);

			// Token: 0x0600576D RID: 22381
			void ShowHelp([In] string helpURL, [In] object helpArg, [In] string features);

			// Token: 0x0600576E RID: 22382
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.IHTMLScreen GetScreen();

			// Token: 0x0600576F RID: 22383
			object GetOption();

			// Token: 0x06005770 RID: 22384
			void Focus();

			// Token: 0x06005771 RID: 22385
			bool GetClosed();

			// Token: 0x06005772 RID: 22386
			void Blur();

			// Token: 0x06005773 RID: 22387
			void Scroll([In] int x, [In] int y);

			// Token: 0x06005774 RID: 22388
			object GetClientInformation();

			// Token: 0x06005775 RID: 22389
			int SetInterval([In] string expression, [In] int msec, [In] ref object language);

			// Token: 0x06005776 RID: 22390
			void ClearInterval([In] int timerID);

			// Token: 0x06005777 RID: 22391
			void SetOffscreenBuffering([In] object p);

			// Token: 0x06005778 RID: 22392
			object GetOffscreenBuffering();

			// Token: 0x06005779 RID: 22393
			[return: MarshalAs(UnmanagedType.Struct)]
			object ExecScript([In] string code, [In] string language);

			// Token: 0x0600577A RID: 22394
			string toString();

			// Token: 0x0600577B RID: 22395
			void ScrollBy([In] int x, [In] int y);

			// Token: 0x0600577C RID: 22396
			void ScrollTo([In] int x, [In] int y);

			// Token: 0x0600577D RID: 22397
			void MoveTo([In] int x, [In] int y);

			// Token: 0x0600577E RID: 22398
			void MoveBy([In] int x, [In] int y);

			// Token: 0x0600577F RID: 22399
			void ResizeTo([In] int x, [In] int y);

			// Token: 0x06005780 RID: 22400
			void ResizeBy([In] int x, [In] int y);

			// Token: 0x06005781 RID: 22401
			object GetExternal();
		}

		// Token: 0x02000575 RID: 1397
		[SuppressUnmanagedCodeSecurity]
		[ComVisible(true)]
		[Guid("3050f4ae-98b5-11cf-bb82-00aa00bdce0b")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		public interface IHTMLWindow3
		{
			// Token: 0x06005782 RID: 22402
			int GetScreenLeft();

			// Token: 0x06005783 RID: 22403
			int GetScreenTop();

			// Token: 0x06005784 RID: 22404
			bool AttachEvent(string ev, [MarshalAs(UnmanagedType.IDispatch)] [In] object pdisp);

			// Token: 0x06005785 RID: 22405
			void DetachEvent(string ev, [MarshalAs(UnmanagedType.IDispatch)] [In] object pdisp);

			// Token: 0x06005786 RID: 22406
			int SetTimeout([In] ref object expression, int msec, [In] ref object language);

			// Token: 0x06005787 RID: 22407
			int SetInterval([In] ref object expression, int msec, [In] ref object language);

			// Token: 0x06005788 RID: 22408
			void Print();

			// Token: 0x06005789 RID: 22409
			void SetBeforePrint(object o);

			// Token: 0x0600578A RID: 22410
			object GetBeforePrint();

			// Token: 0x0600578B RID: 22411
			void SetAfterPrint(object o);

			// Token: 0x0600578C RID: 22412
			object GetAfterPrint();

			// Token: 0x0600578D RID: 22413
			object GetClipboardData();

			// Token: 0x0600578E RID: 22414
			object ShowModelessDialog(string url, object varArgIn, object options);
		}

		// Token: 0x02000576 RID: 1398
		[SuppressUnmanagedCodeSecurity]
		[ComVisible(true)]
		[Guid("3050f6cf-98b5-11cf-bb82-00aa00bdce0b")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		public interface IHTMLWindow4
		{
			// Token: 0x0600578F RID: 22415
			[return: MarshalAs(UnmanagedType.IDispatch)]
			object CreatePopup([In] ref object reserved);

			// Token: 0x06005790 RID: 22416
			[return: MarshalAs(UnmanagedType.Interface)]
			object frameElement();
		}

		// Token: 0x02000577 RID: 1399
		[Guid("3050f625-98b5-11cf-bb82-00aa00bdce0b")]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		[TypeLibType(TypeLibTypeFlags.FHidden)]
		[ComImport]
		public interface DHTMLWindowEvents2
		{
			// Token: 0x06005791 RID: 22417
			[DispId(1003)]
			void onload(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005792 RID: 22418
			[DispId(1008)]
			void onunload(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005793 RID: 22419
			[DispId(-2147418102)]
			bool onhelp(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005794 RID: 22420
			[DispId(-2147418111)]
			void onfocus(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005795 RID: 22421
			[DispId(-2147418112)]
			void onblur(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005796 RID: 22422
			[DispId(1002)]
			bool onerror(string description, string url, int line);

			// Token: 0x06005797 RID: 22423
			[DispId(1016)]
			void onresize(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005798 RID: 22424
			[DispId(1014)]
			void onscroll(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005799 RID: 22425
			[DispId(1017)]
			void onbeforeunload(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600579A RID: 22426
			[DispId(1024)]
			void onbeforeprint(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600579B RID: 22427
			[DispId(1025)]
			void onafterprint(UnsafeNativeMethods.IHTMLEventObj evtObj);
		}

		// Token: 0x02000578 RID: 1400
		[SuppressUnmanagedCodeSecurity]
		[ComVisible(true)]
		[Guid("3050f666-98b5-11cf-bb82-00aa00bdce0b")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		public interface IHTMLPopup
		{
			// Token: 0x0600579C RID: 22428
			void show(int x, int y, int w, int h, ref object element);

			// Token: 0x0600579D RID: 22429
			void hide();

			// Token: 0x0600579E RID: 22430
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.IHTMLDocument GetDocument();

			// Token: 0x0600579F RID: 22431
			bool IsOpen();
		}

		// Token: 0x02000579 RID: 1401
		[SuppressUnmanagedCodeSecurity]
		[ComVisible(true)]
		[Guid("3050f35c-98b5-11cf-bb82-00aa00bdce0b")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		public interface IHTMLScreen
		{
			// Token: 0x060057A0 RID: 22432
			int GetColorDepth();

			// Token: 0x060057A1 RID: 22433
			void SetBufferDepth(int d);

			// Token: 0x060057A2 RID: 22434
			int GetBufferDepth();

			// Token: 0x060057A3 RID: 22435
			int GetWidth();

			// Token: 0x060057A4 RID: 22436
			int GetHeight();

			// Token: 0x060057A5 RID: 22437
			void SetUpdateInterval(int i);

			// Token: 0x060057A6 RID: 22438
			int GetUpdateInterval();

			// Token: 0x060057A7 RID: 22439
			int GetAvailHeight();

			// Token: 0x060057A8 RID: 22440
			int GetAvailWidth();

			// Token: 0x060057A9 RID: 22441
			bool GetFontSmoothingEnabled();
		}

		// Token: 0x0200057A RID: 1402
		[SuppressUnmanagedCodeSecurity]
		[ComVisible(true)]
		[Guid("163BB1E0-6E00-11CF-837A-48DC04C10000")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLLocation
		{
			// Token: 0x060057AA RID: 22442
			void SetHref([In] string p);

			// Token: 0x060057AB RID: 22443
			string GetHref();

			// Token: 0x060057AC RID: 22444
			void SetProtocol([In] string p);

			// Token: 0x060057AD RID: 22445
			string GetProtocol();

			// Token: 0x060057AE RID: 22446
			void SetHost([In] string p);

			// Token: 0x060057AF RID: 22447
			string GetHost();

			// Token: 0x060057B0 RID: 22448
			void SetHostname([In] string p);

			// Token: 0x060057B1 RID: 22449
			string GetHostname();

			// Token: 0x060057B2 RID: 22450
			void SetPort([In] string p);

			// Token: 0x060057B3 RID: 22451
			string GetPort();

			// Token: 0x060057B4 RID: 22452
			void SetPathname([In] string p);

			// Token: 0x060057B5 RID: 22453
			string GetPathname();

			// Token: 0x060057B6 RID: 22454
			void SetSearch([In] string p);

			// Token: 0x060057B7 RID: 22455
			string GetSearch();

			// Token: 0x060057B8 RID: 22456
			void SetHash([In] string p);

			// Token: 0x060057B9 RID: 22457
			string GetHash();

			// Token: 0x060057BA RID: 22458
			void Reload([In] bool flag);

			// Token: 0x060057BB RID: 22459
			void Replace([In] string bstr);

			// Token: 0x060057BC RID: 22460
			void Assign([In] string bstr);
		}

		// Token: 0x0200057B RID: 1403
		[SuppressUnmanagedCodeSecurity]
		[ComVisible(true)]
		[Guid("FECEAAA2-8405-11CF-8BA1-00AA00476DA6")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		internal interface IOmHistory
		{
			// Token: 0x060057BD RID: 22461
			short GetLength();

			// Token: 0x060057BE RID: 22462
			void Back();

			// Token: 0x060057BF RID: 22463
			void Forward();

			// Token: 0x060057C0 RID: 22464
			void Go([In] ref object pvargdistance);
		}

		// Token: 0x0200057C RID: 1404
		[SuppressUnmanagedCodeSecurity]
		[ComVisible(true)]
		[Guid("FECEAAA5-8405-11CF-8BA1-00AA00476DA6")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		internal interface IOmNavigator
		{
			// Token: 0x060057C1 RID: 22465
			string GetAppCodeName();

			// Token: 0x060057C2 RID: 22466
			string GetAppName();

			// Token: 0x060057C3 RID: 22467
			string GetAppVersion();

			// Token: 0x060057C4 RID: 22468
			string GetUserAgent();

			// Token: 0x060057C5 RID: 22469
			bool JavaEnabled();

			// Token: 0x060057C6 RID: 22470
			bool TaintEnabled();

			// Token: 0x060057C7 RID: 22471
			object GetMimeTypes();

			// Token: 0x060057C8 RID: 22472
			object GetPlugins();

			// Token: 0x060057C9 RID: 22473
			bool GetCookieEnabled();

			// Token: 0x060057CA RID: 22474
			object GetOpsProfile();

			// Token: 0x060057CB RID: 22475
			string GetCpuClass();

			// Token: 0x060057CC RID: 22476
			string GetSystemLanguage();

			// Token: 0x060057CD RID: 22477
			string GetBrowserLanguage();

			// Token: 0x060057CE RID: 22478
			string GetUserLanguage();

			// Token: 0x060057CF RID: 22479
			string GetPlatform();

			// Token: 0x060057D0 RID: 22480
			string GetAppMinorVersion();

			// Token: 0x060057D1 RID: 22481
			int GetConnectionSpeed();

			// Token: 0x060057D2 RID: 22482
			bool GetOnLine();

			// Token: 0x060057D3 RID: 22483
			object GetUserProfile();
		}

		// Token: 0x0200057D RID: 1405
		[SuppressUnmanagedCodeSecurity]
		[ComVisible(true)]
		[Guid("3050F32D-98B5-11CF-BB82-00AA00BDCE0B")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLEventObj
		{
			// Token: 0x060057D4 RID: 22484
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.IHTMLElement GetSrcElement();

			// Token: 0x060057D5 RID: 22485
			bool GetAltKey();

			// Token: 0x060057D6 RID: 22486
			bool GetCtrlKey();

			// Token: 0x060057D7 RID: 22487
			bool GetShiftKey();

			// Token: 0x060057D8 RID: 22488
			void SetReturnValue(object p);

			// Token: 0x060057D9 RID: 22489
			object GetReturnValue();

			// Token: 0x060057DA RID: 22490
			void SetCancelBubble(bool p);

			// Token: 0x060057DB RID: 22491
			bool GetCancelBubble();

			// Token: 0x060057DC RID: 22492
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.IHTMLElement GetFromElement();

			// Token: 0x060057DD RID: 22493
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.IHTMLElement GetToElement();

			// Token: 0x060057DE RID: 22494
			void SetKeyCode([In] int p);

			// Token: 0x060057DF RID: 22495
			int GetKeyCode();

			// Token: 0x060057E0 RID: 22496
			int GetButton();

			// Token: 0x060057E1 RID: 22497
			string GetEventType();

			// Token: 0x060057E2 RID: 22498
			string GetQualifier();

			// Token: 0x060057E3 RID: 22499
			int GetReason();

			// Token: 0x060057E4 RID: 22500
			int GetX();

			// Token: 0x060057E5 RID: 22501
			int GetY();

			// Token: 0x060057E6 RID: 22502
			int GetClientX();

			// Token: 0x060057E7 RID: 22503
			int GetClientY();

			// Token: 0x060057E8 RID: 22504
			int GetOffsetX();

			// Token: 0x060057E9 RID: 22505
			int GetOffsetY();

			// Token: 0x060057EA RID: 22506
			int GetScreenX();

			// Token: 0x060057EB RID: 22507
			int GetScreenY();

			// Token: 0x060057EC RID: 22508
			object GetSrcFilter();
		}

		// Token: 0x0200057E RID: 1406
		[SuppressUnmanagedCodeSecurity]
		[ComVisible(true)]
		[Guid("3050f48B-98b5-11cf-bb82-00aa00bdce0b")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLEventObj2
		{
			// Token: 0x060057ED RID: 22509
			void SetAttribute(string attributeName, object attributeValue, int lFlags);

			// Token: 0x060057EE RID: 22510
			object GetAttribute(string attributeName, int lFlags);

			// Token: 0x060057EF RID: 22511
			bool RemoveAttribute(string attributeName, int lFlags);

			// Token: 0x060057F0 RID: 22512
			void SetPropertyName(string name);

			// Token: 0x060057F1 RID: 22513
			string GetPropertyName();

			// Token: 0x060057F2 RID: 22514
			void SetBookmarks(ref object bm);

			// Token: 0x060057F3 RID: 22515
			object GetBookmarks();

			// Token: 0x060057F4 RID: 22516
			void SetRecordset(ref object rs);

			// Token: 0x060057F5 RID: 22517
			object GetRecordset();

			// Token: 0x060057F6 RID: 22518
			void SetDataFld(string df);

			// Token: 0x060057F7 RID: 22519
			string GetDataFld();

			// Token: 0x060057F8 RID: 22520
			void SetBoundElements(ref object be);

			// Token: 0x060057F9 RID: 22521
			object GetBoundElements();

			// Token: 0x060057FA RID: 22522
			void SetRepeat(bool r);

			// Token: 0x060057FB RID: 22523
			bool GetRepeat();

			// Token: 0x060057FC RID: 22524
			void SetSrcUrn(string urn);

			// Token: 0x060057FD RID: 22525
			string GetSrcUrn();

			// Token: 0x060057FE RID: 22526
			void SetSrcElement(ref object se);

			// Token: 0x060057FF RID: 22527
			object GetSrcElement();

			// Token: 0x06005800 RID: 22528
			void SetAltKey(bool alt);

			// Token: 0x06005801 RID: 22529
			bool GetAltKey();

			// Token: 0x06005802 RID: 22530
			void SetCtrlKey(bool ctrl);

			// Token: 0x06005803 RID: 22531
			bool GetCtrlKey();

			// Token: 0x06005804 RID: 22532
			void SetShiftKey(bool shift);

			// Token: 0x06005805 RID: 22533
			bool GetShiftKey();

			// Token: 0x06005806 RID: 22534
			void SetFromElement(ref object element);

			// Token: 0x06005807 RID: 22535
			object GetFromElement();

			// Token: 0x06005808 RID: 22536
			void SetToElement(ref object element);

			// Token: 0x06005809 RID: 22537
			object GetToElement();

			// Token: 0x0600580A RID: 22538
			void SetButton(int b);

			// Token: 0x0600580B RID: 22539
			int GetButton();

			// Token: 0x0600580C RID: 22540
			void SetType(string type);

			// Token: 0x0600580D RID: 22541
			string GetType();

			// Token: 0x0600580E RID: 22542
			void SetQualifier(string q);

			// Token: 0x0600580F RID: 22543
			string GetQualifier();

			// Token: 0x06005810 RID: 22544
			void SetReason(int r);

			// Token: 0x06005811 RID: 22545
			int GetReason();

			// Token: 0x06005812 RID: 22546
			void SetX(int x);

			// Token: 0x06005813 RID: 22547
			int GetX();

			// Token: 0x06005814 RID: 22548
			void SetY(int y);

			// Token: 0x06005815 RID: 22549
			int GetY();

			// Token: 0x06005816 RID: 22550
			void SetClientX(int x);

			// Token: 0x06005817 RID: 22551
			int GetClientX();

			// Token: 0x06005818 RID: 22552
			void SetClientY(int y);

			// Token: 0x06005819 RID: 22553
			int GetClientY();

			// Token: 0x0600581A RID: 22554
			void SetOffsetX(int x);

			// Token: 0x0600581B RID: 22555
			int GetOffsetX();

			// Token: 0x0600581C RID: 22556
			void SetOffsetY(int y);

			// Token: 0x0600581D RID: 22557
			int GetOffsetY();

			// Token: 0x0600581E RID: 22558
			void SetScreenX(int x);

			// Token: 0x0600581F RID: 22559
			int GetScreenX();

			// Token: 0x06005820 RID: 22560
			void SetScreenY(int y);

			// Token: 0x06005821 RID: 22561
			int GetScreenY();

			// Token: 0x06005822 RID: 22562
			void SetSrcFilter(ref object filter);

			// Token: 0x06005823 RID: 22563
			object GetSrcFilter();

			// Token: 0x06005824 RID: 22564
			object GetDataTransfer();
		}

		// Token: 0x0200057F RID: 1407
		[SuppressUnmanagedCodeSecurity]
		[ComVisible(true)]
		[Guid("3050f814-98b5-11cf-bb82-00aa00bdce0b")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLEventObj4
		{
			// Token: 0x06005825 RID: 22565
			int GetWheelDelta();
		}

		// Token: 0x02000580 RID: 1408
		[SuppressUnmanagedCodeSecurity]
		[ComVisible(true)]
		[Guid("3050F21F-98B5-11CF-BB82-00AA00BDCE0B")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLElementCollection
		{
			// Token: 0x06005826 RID: 22566
			string toString();

			// Token: 0x06005827 RID: 22567
			void SetLength(int p);

			// Token: 0x06005828 RID: 22568
			int GetLength();

			// Token: 0x06005829 RID: 22569
			[return: MarshalAs(UnmanagedType.Interface)]
			object Get_newEnum();

			// Token: 0x0600582A RID: 22570
			[return: MarshalAs(UnmanagedType.IDispatch)]
			object Item(object idOrName, object index);

			// Token: 0x0600582B RID: 22571
			[return: MarshalAs(UnmanagedType.Interface)]
			object Tags(object tagName);
		}

		// Token: 0x02000581 RID: 1409
		[SuppressUnmanagedCodeSecurity]
		[ComVisible(true)]
		[Guid("3050F1FF-98B5-11CF-BB82-00AA00BDCE0B")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLElement
		{
			// Token: 0x0600582C RID: 22572
			void SetAttribute(string attributeName, object attributeValue, int lFlags);

			// Token: 0x0600582D RID: 22573
			object GetAttribute(string attributeName, int lFlags);

			// Token: 0x0600582E RID: 22574
			bool RemoveAttribute(string strAttributeName, int lFlags);

			// Token: 0x0600582F RID: 22575
			void SetClassName(string p);

			// Token: 0x06005830 RID: 22576
			string GetClassName();

			// Token: 0x06005831 RID: 22577
			void SetId(string p);

			// Token: 0x06005832 RID: 22578
			string GetId();

			// Token: 0x06005833 RID: 22579
			string GetTagName();

			// Token: 0x06005834 RID: 22580
			UnsafeNativeMethods.IHTMLElement GetParentElement();

			// Token: 0x06005835 RID: 22581
			UnsafeNativeMethods.IHTMLStyle GetStyle();

			// Token: 0x06005836 RID: 22582
			void SetOnhelp(object p);

			// Token: 0x06005837 RID: 22583
			object GetOnhelp();

			// Token: 0x06005838 RID: 22584
			void SetOnclick(object p);

			// Token: 0x06005839 RID: 22585
			object GetOnclick();

			// Token: 0x0600583A RID: 22586
			void SetOndblclick(object p);

			// Token: 0x0600583B RID: 22587
			object GetOndblclick();

			// Token: 0x0600583C RID: 22588
			void SetOnkeydown(object p);

			// Token: 0x0600583D RID: 22589
			object GetOnkeydown();

			// Token: 0x0600583E RID: 22590
			void SetOnkeyup(object p);

			// Token: 0x0600583F RID: 22591
			object GetOnkeyup();

			// Token: 0x06005840 RID: 22592
			void SetOnkeypress(object p);

			// Token: 0x06005841 RID: 22593
			object GetOnkeypress();

			// Token: 0x06005842 RID: 22594
			void SetOnmouseout(object p);

			// Token: 0x06005843 RID: 22595
			object GetOnmouseout();

			// Token: 0x06005844 RID: 22596
			void SetOnmouseover(object p);

			// Token: 0x06005845 RID: 22597
			object GetOnmouseover();

			// Token: 0x06005846 RID: 22598
			void SetOnmousemove(object p);

			// Token: 0x06005847 RID: 22599
			object GetOnmousemove();

			// Token: 0x06005848 RID: 22600
			void SetOnmousedown(object p);

			// Token: 0x06005849 RID: 22601
			object GetOnmousedown();

			// Token: 0x0600584A RID: 22602
			void SetOnmouseup(object p);

			// Token: 0x0600584B RID: 22603
			object GetOnmouseup();

			// Token: 0x0600584C RID: 22604
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.IHTMLDocument2 GetDocument();

			// Token: 0x0600584D RID: 22605
			void SetTitle(string p);

			// Token: 0x0600584E RID: 22606
			string GetTitle();

			// Token: 0x0600584F RID: 22607
			void SetLanguage(string p);

			// Token: 0x06005850 RID: 22608
			string GetLanguage();

			// Token: 0x06005851 RID: 22609
			void SetOnselectstart(object p);

			// Token: 0x06005852 RID: 22610
			object GetOnselectstart();

			// Token: 0x06005853 RID: 22611
			void ScrollIntoView(object varargStart);

			// Token: 0x06005854 RID: 22612
			bool Contains(UnsafeNativeMethods.IHTMLElement pChild);

			// Token: 0x06005855 RID: 22613
			int GetSourceIndex();

			// Token: 0x06005856 RID: 22614
			object GetRecordNumber();

			// Token: 0x06005857 RID: 22615
			void SetLang(string p);

			// Token: 0x06005858 RID: 22616
			string GetLang();

			// Token: 0x06005859 RID: 22617
			int GetOffsetLeft();

			// Token: 0x0600585A RID: 22618
			int GetOffsetTop();

			// Token: 0x0600585B RID: 22619
			int GetOffsetWidth();

			// Token: 0x0600585C RID: 22620
			int GetOffsetHeight();

			// Token: 0x0600585D RID: 22621
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.IHTMLElement GetOffsetParent();

			// Token: 0x0600585E RID: 22622
			void SetInnerHTML(string p);

			// Token: 0x0600585F RID: 22623
			string GetInnerHTML();

			// Token: 0x06005860 RID: 22624
			void SetInnerText(string p);

			// Token: 0x06005861 RID: 22625
			string GetInnerText();

			// Token: 0x06005862 RID: 22626
			void SetOuterHTML(string p);

			// Token: 0x06005863 RID: 22627
			string GetOuterHTML();

			// Token: 0x06005864 RID: 22628
			void SetOuterText(string p);

			// Token: 0x06005865 RID: 22629
			string GetOuterText();

			// Token: 0x06005866 RID: 22630
			void InsertAdjacentHTML(string where, string html);

			// Token: 0x06005867 RID: 22631
			void InsertAdjacentText(string where, string text);

			// Token: 0x06005868 RID: 22632
			UnsafeNativeMethods.IHTMLElement GetParentTextEdit();

			// Token: 0x06005869 RID: 22633
			bool GetIsTextEdit();

			// Token: 0x0600586A RID: 22634
			void Click();

			// Token: 0x0600586B RID: 22635
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetFilters();

			// Token: 0x0600586C RID: 22636
			void SetOndragstart(object p);

			// Token: 0x0600586D RID: 22637
			object GetOndragstart();

			// Token: 0x0600586E RID: 22638
			string toString();

			// Token: 0x0600586F RID: 22639
			void SetOnbeforeupdate(object p);

			// Token: 0x06005870 RID: 22640
			object GetOnbeforeupdate();

			// Token: 0x06005871 RID: 22641
			void SetOnafterupdate(object p);

			// Token: 0x06005872 RID: 22642
			object GetOnafterupdate();

			// Token: 0x06005873 RID: 22643
			void SetOnerrorupdate(object p);

			// Token: 0x06005874 RID: 22644
			object GetOnerrorupdate();

			// Token: 0x06005875 RID: 22645
			void SetOnrowexit(object p);

			// Token: 0x06005876 RID: 22646
			object GetOnrowexit();

			// Token: 0x06005877 RID: 22647
			void SetOnrowenter(object p);

			// Token: 0x06005878 RID: 22648
			object GetOnrowenter();

			// Token: 0x06005879 RID: 22649
			void SetOndatasetchanged(object p);

			// Token: 0x0600587A RID: 22650
			object GetOndatasetchanged();

			// Token: 0x0600587B RID: 22651
			void SetOndataavailable(object p);

			// Token: 0x0600587C RID: 22652
			object GetOndataavailable();

			// Token: 0x0600587D RID: 22653
			void SetOndatasetcomplete(object p);

			// Token: 0x0600587E RID: 22654
			object GetOndatasetcomplete();

			// Token: 0x0600587F RID: 22655
			void SetOnfilterchange(object p);

			// Token: 0x06005880 RID: 22656
			object GetOnfilterchange();

			// Token: 0x06005881 RID: 22657
			[return: MarshalAs(UnmanagedType.IDispatch)]
			object GetChildren();

			// Token: 0x06005882 RID: 22658
			[return: MarshalAs(UnmanagedType.IDispatch)]
			object GetAll();
		}

		// Token: 0x02000582 RID: 1410
		[SuppressUnmanagedCodeSecurity]
		[ComVisible(true)]
		[Guid("3050f434-98b5-11cf-bb82-00aa00bdce0b")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLElement2
		{
			// Token: 0x06005883 RID: 22659
			string ScopeName();

			// Token: 0x06005884 RID: 22660
			void SetCapture(bool containerCapture);

			// Token: 0x06005885 RID: 22661
			void ReleaseCapture();

			// Token: 0x06005886 RID: 22662
			void SetOnLoseCapture(object v);

			// Token: 0x06005887 RID: 22663
			object GetOnLoseCapture();

			// Token: 0x06005888 RID: 22664
			string GetComponentFromPoint(int x, int y);

			// Token: 0x06005889 RID: 22665
			void DoScroll(object component);

			// Token: 0x0600588A RID: 22666
			void SetOnScroll(object v);

			// Token: 0x0600588B RID: 22667
			object GetOnScroll();

			// Token: 0x0600588C RID: 22668
			void SetOnDrag(object v);

			// Token: 0x0600588D RID: 22669
			object GetOnDrag();

			// Token: 0x0600588E RID: 22670
			void SetOnDragEnd(object v);

			// Token: 0x0600588F RID: 22671
			object GetOnDragEnd();

			// Token: 0x06005890 RID: 22672
			void SetOnDragEnter(object v);

			// Token: 0x06005891 RID: 22673
			object GetOnDragEnter();

			// Token: 0x06005892 RID: 22674
			void SetOnDragOver(object v);

			// Token: 0x06005893 RID: 22675
			object GetOnDragOver();

			// Token: 0x06005894 RID: 22676
			void SetOnDragleave(object v);

			// Token: 0x06005895 RID: 22677
			object GetOnDragLeave();

			// Token: 0x06005896 RID: 22678
			void SetOnDrop(object v);

			// Token: 0x06005897 RID: 22679
			object GetOnDrop();

			// Token: 0x06005898 RID: 22680
			void SetOnBeforeCut(object v);

			// Token: 0x06005899 RID: 22681
			object GetOnBeforeCut();

			// Token: 0x0600589A RID: 22682
			void SetOnCut(object v);

			// Token: 0x0600589B RID: 22683
			object GetOnCut();

			// Token: 0x0600589C RID: 22684
			void SetOnBeforeCopy(object v);

			// Token: 0x0600589D RID: 22685
			object GetOnBeforeCopy();

			// Token: 0x0600589E RID: 22686
			void SetOnCopy(object v);

			// Token: 0x0600589F RID: 22687
			object GetOnCopy(object p);

			// Token: 0x060058A0 RID: 22688
			void SetOnBeforePaste(object v);

			// Token: 0x060058A1 RID: 22689
			object GetOnBeforePaste(object p);

			// Token: 0x060058A2 RID: 22690
			void SetOnPaste(object v);

			// Token: 0x060058A3 RID: 22691
			object GetOnPaste(object p);

			// Token: 0x060058A4 RID: 22692
			object GetCurrentStyle();

			// Token: 0x060058A5 RID: 22693
			void SetOnPropertyChange(object v);

			// Token: 0x060058A6 RID: 22694
			object GetOnPropertyChange(object p);

			// Token: 0x060058A7 RID: 22695
			object GetClientRects();

			// Token: 0x060058A8 RID: 22696
			object GetBoundingClientRect();

			// Token: 0x060058A9 RID: 22697
			void SetExpression(string propName, string expression, string language);

			// Token: 0x060058AA RID: 22698
			object GetExpression(string propName);

			// Token: 0x060058AB RID: 22699
			bool RemoveExpression(string propName);

			// Token: 0x060058AC RID: 22700
			void SetTabIndex(int v);

			// Token: 0x060058AD RID: 22701
			short GetTabIndex();

			// Token: 0x060058AE RID: 22702
			void Focus();

			// Token: 0x060058AF RID: 22703
			void SetAccessKey(string v);

			// Token: 0x060058B0 RID: 22704
			string GetAccessKey();

			// Token: 0x060058B1 RID: 22705
			void SetOnBlur(object v);

			// Token: 0x060058B2 RID: 22706
			object GetOnBlur();

			// Token: 0x060058B3 RID: 22707
			void SetOnFocus(object v);

			// Token: 0x060058B4 RID: 22708
			object GetOnFocus();

			// Token: 0x060058B5 RID: 22709
			void SetOnResize(object v);

			// Token: 0x060058B6 RID: 22710
			object GetOnResize();

			// Token: 0x060058B7 RID: 22711
			void Blur();

			// Token: 0x060058B8 RID: 22712
			void AddFilter(object pUnk);

			// Token: 0x060058B9 RID: 22713
			void RemoveFilter(object pUnk);

			// Token: 0x060058BA RID: 22714
			int ClientHeight();

			// Token: 0x060058BB RID: 22715
			int ClientWidth();

			// Token: 0x060058BC RID: 22716
			int ClientTop();

			// Token: 0x060058BD RID: 22717
			int ClientLeft();

			// Token: 0x060058BE RID: 22718
			bool AttachEvent(string ev, [MarshalAs(UnmanagedType.IDispatch)] [In] object pdisp);

			// Token: 0x060058BF RID: 22719
			void DetachEvent(string ev, [MarshalAs(UnmanagedType.IDispatch)] [In] object pdisp);

			// Token: 0x060058C0 RID: 22720
			object ReadyState();

			// Token: 0x060058C1 RID: 22721
			void SetOnReadyStateChange(object v);

			// Token: 0x060058C2 RID: 22722
			object GetOnReadyStateChange();

			// Token: 0x060058C3 RID: 22723
			void SetOnRowsDelete(object v);

			// Token: 0x060058C4 RID: 22724
			object GetOnRowsDelete();

			// Token: 0x060058C5 RID: 22725
			void SetOnRowsInserted(object v);

			// Token: 0x060058C6 RID: 22726
			object GetOnRowsInserted();

			// Token: 0x060058C7 RID: 22727
			void SetOnCellChange(object v);

			// Token: 0x060058C8 RID: 22728
			object GetOnCellChange();

			// Token: 0x060058C9 RID: 22729
			void SetDir(string v);

			// Token: 0x060058CA RID: 22730
			string GetDir();

			// Token: 0x060058CB RID: 22731
			object CreateControlRange();

			// Token: 0x060058CC RID: 22732
			int GetScrollHeight();

			// Token: 0x060058CD RID: 22733
			int GetScrollWidth();

			// Token: 0x060058CE RID: 22734
			void SetScrollTop(int v);

			// Token: 0x060058CF RID: 22735
			int GetScrollTop();

			// Token: 0x060058D0 RID: 22736
			void SetScrollLeft(int v);

			// Token: 0x060058D1 RID: 22737
			int GetScrollLeft();

			// Token: 0x060058D2 RID: 22738
			void ClearAttributes();

			// Token: 0x060058D3 RID: 22739
			void MergeAttributes(object mergeThis);

			// Token: 0x060058D4 RID: 22740
			void SetOnContextMenu(object v);

			// Token: 0x060058D5 RID: 22741
			object GetOnContextMenu();

			// Token: 0x060058D6 RID: 22742
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.IHTMLElement InsertAdjacentElement(string where, [MarshalAs(UnmanagedType.Interface)] [In] UnsafeNativeMethods.IHTMLElement insertedElement);

			// Token: 0x060058D7 RID: 22743
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.IHTMLElement applyElement([MarshalAs(UnmanagedType.Interface)] [In] UnsafeNativeMethods.IHTMLElement apply, string where);

			// Token: 0x060058D8 RID: 22744
			string GetAdjacentText(string where);

			// Token: 0x060058D9 RID: 22745
			string ReplaceAdjacentText(string where, string newText);

			// Token: 0x060058DA RID: 22746
			bool CanHaveChildren();

			// Token: 0x060058DB RID: 22747
			int AddBehavior(string url, ref object oFactory);

			// Token: 0x060058DC RID: 22748
			bool RemoveBehavior(int cookie);

			// Token: 0x060058DD RID: 22749
			object GetRuntimeStyle();

			// Token: 0x060058DE RID: 22750
			object GetBehaviorUrns();

			// Token: 0x060058DF RID: 22751
			void SetTagUrn(string v);

			// Token: 0x060058E0 RID: 22752
			string GetTagUrn();

			// Token: 0x060058E1 RID: 22753
			void SetOnBeforeEditFocus(object v);

			// Token: 0x060058E2 RID: 22754
			object GetOnBeforeEditFocus();

			// Token: 0x060058E3 RID: 22755
			int GetReadyStateValue();

			// Token: 0x060058E4 RID: 22756
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.IHTMLElementCollection GetElementsByTagName(string v);
		}

		// Token: 0x02000583 RID: 1411
		[SuppressUnmanagedCodeSecurity]
		[ComVisible(true)]
		[Guid("3050f673-98b5-11cf-bb82-00aa00bdce0b")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLElement3
		{
			// Token: 0x060058E5 RID: 22757
			void MergeAttributes(object mergeThis, object pvarFlags);

			// Token: 0x060058E6 RID: 22758
			bool IsMultiLine();

			// Token: 0x060058E7 RID: 22759
			bool CanHaveHTML();

			// Token: 0x060058E8 RID: 22760
			void SetOnLayoutComplete(object v);

			// Token: 0x060058E9 RID: 22761
			object GetOnLayoutComplete();

			// Token: 0x060058EA RID: 22762
			void SetOnPage(object v);

			// Token: 0x060058EB RID: 22763
			object GetOnPage();

			// Token: 0x060058EC RID: 22764
			void SetInflateBlock(bool v);

			// Token: 0x060058ED RID: 22765
			bool GetInflateBlock();

			// Token: 0x060058EE RID: 22766
			void SetOnBeforeDeactivate(object v);

			// Token: 0x060058EF RID: 22767
			object GetOnBeforeDeactivate();

			// Token: 0x060058F0 RID: 22768
			void SetActive();

			// Token: 0x060058F1 RID: 22769
			void SetContentEditable(string v);

			// Token: 0x060058F2 RID: 22770
			string GetContentEditable();

			// Token: 0x060058F3 RID: 22771
			bool IsContentEditable();

			// Token: 0x060058F4 RID: 22772
			void SetHideFocus(bool v);

			// Token: 0x060058F5 RID: 22773
			bool GetHideFocus();

			// Token: 0x060058F6 RID: 22774
			void SetDisabled(bool v);

			// Token: 0x060058F7 RID: 22775
			bool GetDisabled();

			// Token: 0x060058F8 RID: 22776
			bool IsDisabled();

			// Token: 0x060058F9 RID: 22777
			void SetOnMove(object v);

			// Token: 0x060058FA RID: 22778
			object GetOnMove();

			// Token: 0x060058FB RID: 22779
			void SetOnControlSelect(object v);

			// Token: 0x060058FC RID: 22780
			object GetOnControlSelect();

			// Token: 0x060058FD RID: 22781
			bool FireEvent(string bstrEventName, IntPtr pvarEventObject);

			// Token: 0x060058FE RID: 22782
			void SetOnResizeStart(object v);

			// Token: 0x060058FF RID: 22783
			object GetOnResizeStart();

			// Token: 0x06005900 RID: 22784
			void SetOnResizeEnd(object v);

			// Token: 0x06005901 RID: 22785
			object GetOnResizeEnd();

			// Token: 0x06005902 RID: 22786
			void SetOnMoveStart(object v);

			// Token: 0x06005903 RID: 22787
			object GetOnMoveStart();

			// Token: 0x06005904 RID: 22788
			void SetOnMoveEnd(object v);

			// Token: 0x06005905 RID: 22789
			object GetOnMoveEnd();

			// Token: 0x06005906 RID: 22790
			void SetOnMouseEnter(object v);

			// Token: 0x06005907 RID: 22791
			object GetOnMouseEnter();

			// Token: 0x06005908 RID: 22792
			void SetOnMouseLeave(object v);

			// Token: 0x06005909 RID: 22793
			object GetOnMouseLeave();

			// Token: 0x0600590A RID: 22794
			void SetOnActivate(object v);

			// Token: 0x0600590B RID: 22795
			object GetOnActivate();

			// Token: 0x0600590C RID: 22796
			void SetOnDeactivate(object v);

			// Token: 0x0600590D RID: 22797
			object GetOnDeactivate();

			// Token: 0x0600590E RID: 22798
			bool DragDrop();

			// Token: 0x0600590F RID: 22799
			int GlyphMode();
		}

		// Token: 0x02000584 RID: 1412
		[SuppressUnmanagedCodeSecurity]
		[ComVisible(true)]
		[Guid("3050f5da-98b5-11cf-bb82-00aa00bdce0b")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		public interface IHTMLDOMNode
		{
			// Token: 0x06005910 RID: 22800
			long GetNodeType();

			// Token: 0x06005911 RID: 22801
			UnsafeNativeMethods.IHTMLDOMNode GetParentNode();

			// Token: 0x06005912 RID: 22802
			bool HasChildNodes();

			// Token: 0x06005913 RID: 22803
			object GetChildNodes();

			// Token: 0x06005914 RID: 22804
			object GetAttributes();

			// Token: 0x06005915 RID: 22805
			UnsafeNativeMethods.IHTMLDOMNode InsertBefore(UnsafeNativeMethods.IHTMLDOMNode newChild, object refChild);

			// Token: 0x06005916 RID: 22806
			UnsafeNativeMethods.IHTMLDOMNode RemoveChild(UnsafeNativeMethods.IHTMLDOMNode oldChild);

			// Token: 0x06005917 RID: 22807
			UnsafeNativeMethods.IHTMLDOMNode ReplaceChild(UnsafeNativeMethods.IHTMLDOMNode newChild, UnsafeNativeMethods.IHTMLDOMNode oldChild);

			// Token: 0x06005918 RID: 22808
			UnsafeNativeMethods.IHTMLDOMNode CloneNode(bool fDeep);

			// Token: 0x06005919 RID: 22809
			UnsafeNativeMethods.IHTMLDOMNode RemoveNode(bool fDeep);

			// Token: 0x0600591A RID: 22810
			UnsafeNativeMethods.IHTMLDOMNode SwapNode(UnsafeNativeMethods.IHTMLDOMNode otherNode);

			// Token: 0x0600591B RID: 22811
			UnsafeNativeMethods.IHTMLDOMNode ReplaceNode(UnsafeNativeMethods.IHTMLDOMNode replacement);

			// Token: 0x0600591C RID: 22812
			UnsafeNativeMethods.IHTMLDOMNode AppendChild(UnsafeNativeMethods.IHTMLDOMNode newChild);

			// Token: 0x0600591D RID: 22813
			string NodeName();

			// Token: 0x0600591E RID: 22814
			void SetNodeValue(object v);

			// Token: 0x0600591F RID: 22815
			object GetNodeValue();

			// Token: 0x06005920 RID: 22816
			UnsafeNativeMethods.IHTMLDOMNode FirstChild();

			// Token: 0x06005921 RID: 22817
			UnsafeNativeMethods.IHTMLDOMNode LastChild();

			// Token: 0x06005922 RID: 22818
			UnsafeNativeMethods.IHTMLDOMNode PreviousSibling();

			// Token: 0x06005923 RID: 22819
			UnsafeNativeMethods.IHTMLDOMNode NextSibling();
		}

		// Token: 0x02000585 RID: 1413
		[Guid("3050f60f-98b5-11cf-bb82-00aa00bdce0b")]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		[TypeLibType(TypeLibTypeFlags.FHidden)]
		[ComImport]
		public interface DHTMLElementEvents2
		{
			// Token: 0x06005924 RID: 22820
			[DispId(-2147418102)]
			bool onhelp(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005925 RID: 22821
			[DispId(-600)]
			bool onclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005926 RID: 22822
			[DispId(-601)]
			bool ondblclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005927 RID: 22823
			[DispId(-603)]
			bool onkeypress(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005928 RID: 22824
			[DispId(-602)]
			void onkeydown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005929 RID: 22825
			[DispId(-604)]
			void onkeyup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600592A RID: 22826
			[DispId(-2147418103)]
			void onmouseout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600592B RID: 22827
			[DispId(-2147418104)]
			void onmouseover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600592C RID: 22828
			[DispId(-606)]
			void onmousemove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600592D RID: 22829
			[DispId(-605)]
			void onmousedown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600592E RID: 22830
			[DispId(-607)]
			void onmouseup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600592F RID: 22831
			[DispId(-2147418100)]
			bool onselectstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005930 RID: 22832
			[DispId(-2147418095)]
			void onfilterchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005931 RID: 22833
			[DispId(-2147418101)]
			bool ondragstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005932 RID: 22834
			[DispId(-2147418108)]
			bool onbeforeupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005933 RID: 22835
			[DispId(-2147418107)]
			void onafterupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005934 RID: 22836
			[DispId(-2147418099)]
			bool onerrorupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005935 RID: 22837
			[DispId(-2147418106)]
			bool onrowexit(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005936 RID: 22838
			[DispId(-2147418105)]
			void onrowenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005937 RID: 22839
			[DispId(-2147418098)]
			void ondatasetchanged(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005938 RID: 22840
			[DispId(-2147418097)]
			void ondataavailable(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005939 RID: 22841
			[DispId(-2147418096)]
			void ondatasetcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600593A RID: 22842
			[DispId(-2147418094)]
			void onlosecapture(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600593B RID: 22843
			[DispId(-2147418093)]
			void onpropertychange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600593C RID: 22844
			[DispId(1014)]
			void onscroll(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600593D RID: 22845
			[DispId(-2147418111)]
			void onfocus(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600593E RID: 22846
			[DispId(-2147418112)]
			void onblur(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600593F RID: 22847
			[DispId(1016)]
			void onresize(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005940 RID: 22848
			[DispId(-2147418092)]
			bool ondrag(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005941 RID: 22849
			[DispId(-2147418091)]
			void ondragend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005942 RID: 22850
			[DispId(-2147418090)]
			bool ondragenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005943 RID: 22851
			[DispId(-2147418089)]
			bool ondragover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005944 RID: 22852
			[DispId(-2147418088)]
			void ondragleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005945 RID: 22853
			[DispId(-2147418087)]
			bool ondrop(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005946 RID: 22854
			[DispId(-2147418083)]
			bool onbeforecut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005947 RID: 22855
			[DispId(-2147418086)]
			bool oncut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005948 RID: 22856
			[DispId(-2147418082)]
			bool onbeforecopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005949 RID: 22857
			[DispId(-2147418085)]
			bool oncopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600594A RID: 22858
			[DispId(-2147418081)]
			bool onbeforepaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600594B RID: 22859
			[DispId(-2147418084)]
			bool onpaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600594C RID: 22860
			[DispId(1023)]
			bool oncontextmenu(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600594D RID: 22861
			[DispId(-2147418080)]
			void onrowsdelete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600594E RID: 22862
			[DispId(-2147418079)]
			void onrowsinserted(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600594F RID: 22863
			[DispId(-2147418078)]
			void oncellchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005950 RID: 22864
			[DispId(-609)]
			void onreadystatechange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005951 RID: 22865
			[DispId(1030)]
			void onlayoutcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005952 RID: 22866
			[DispId(1031)]
			void onpage(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005953 RID: 22867
			[DispId(1042)]
			void onmouseenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005954 RID: 22868
			[DispId(1043)]
			void onmouseleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005955 RID: 22869
			[DispId(1044)]
			void onactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005956 RID: 22870
			[DispId(1045)]
			void ondeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005957 RID: 22871
			[DispId(1034)]
			bool onbeforedeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005958 RID: 22872
			[DispId(1047)]
			bool onbeforeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005959 RID: 22873
			[DispId(1048)]
			void onfocusin(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600595A RID: 22874
			[DispId(1049)]
			void onfocusout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600595B RID: 22875
			[DispId(1035)]
			void onmove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600595C RID: 22876
			[DispId(1036)]
			bool oncontrolselect(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600595D RID: 22877
			[DispId(1038)]
			bool onmovestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600595E RID: 22878
			[DispId(1039)]
			void onmoveend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600595F RID: 22879
			[DispId(1040)]
			bool onresizestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005960 RID: 22880
			[DispId(1041)]
			void onresizeend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005961 RID: 22881
			[DispId(1033)]
			bool onmousewheel(UnsafeNativeMethods.IHTMLEventObj evtObj);
		}

		// Token: 0x02000586 RID: 1414
		[Guid("3050f610-98b5-11cf-bb82-00aa00bdce0b")]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		[TypeLibType(TypeLibTypeFlags.FHidden)]
		[ComImport]
		public interface DHTMLAnchorEvents2
		{
			// Token: 0x06005962 RID: 22882
			[DispId(-2147418102)]
			bool onhelp(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005963 RID: 22883
			[DispId(-600)]
			bool onclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005964 RID: 22884
			[DispId(-601)]
			bool ondblclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005965 RID: 22885
			[DispId(-603)]
			bool onkeypress(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005966 RID: 22886
			[DispId(-602)]
			void onkeydown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005967 RID: 22887
			[DispId(-604)]
			void onkeyup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005968 RID: 22888
			[DispId(-2147418103)]
			void onmouseout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005969 RID: 22889
			[DispId(-2147418104)]
			void onmouseover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600596A RID: 22890
			[DispId(-606)]
			void onmousemove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600596B RID: 22891
			[DispId(-605)]
			void onmousedown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600596C RID: 22892
			[DispId(-607)]
			void onmouseup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600596D RID: 22893
			[DispId(-2147418100)]
			bool onselectstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600596E RID: 22894
			[DispId(-2147418095)]
			void onfilterchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600596F RID: 22895
			[DispId(-2147418101)]
			bool ondragstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005970 RID: 22896
			[DispId(-2147418108)]
			bool onbeforeupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005971 RID: 22897
			[DispId(-2147418107)]
			void onafterupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005972 RID: 22898
			[DispId(-2147418099)]
			bool onerrorupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005973 RID: 22899
			[DispId(-2147418106)]
			bool onrowexit(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005974 RID: 22900
			[DispId(-2147418105)]
			void onrowenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005975 RID: 22901
			[DispId(-2147418098)]
			void ondatasetchanged(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005976 RID: 22902
			[DispId(-2147418097)]
			void ondataavailable(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005977 RID: 22903
			[DispId(-2147418096)]
			void ondatasetcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005978 RID: 22904
			[DispId(-2147418094)]
			void onlosecapture(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005979 RID: 22905
			[DispId(-2147418093)]
			void onpropertychange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600597A RID: 22906
			[DispId(1014)]
			void onscroll(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600597B RID: 22907
			[DispId(-2147418111)]
			void onfocus(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600597C RID: 22908
			[DispId(-2147418112)]
			void onblur(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600597D RID: 22909
			[DispId(1016)]
			void onresize(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600597E RID: 22910
			[DispId(-2147418092)]
			bool ondrag(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600597F RID: 22911
			[DispId(-2147418091)]
			void ondragend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005980 RID: 22912
			[DispId(-2147418090)]
			bool ondragenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005981 RID: 22913
			[DispId(-2147418089)]
			bool ondragover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005982 RID: 22914
			[DispId(-2147418088)]
			void ondragleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005983 RID: 22915
			[DispId(-2147418087)]
			bool ondrop(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005984 RID: 22916
			[DispId(-2147418083)]
			bool onbeforecut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005985 RID: 22917
			[DispId(-2147418086)]
			bool oncut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005986 RID: 22918
			[DispId(-2147418082)]
			bool onbeforecopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005987 RID: 22919
			[DispId(-2147418085)]
			bool oncopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005988 RID: 22920
			[DispId(-2147418081)]
			bool onbeforepaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005989 RID: 22921
			[DispId(-2147418084)]
			bool onpaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600598A RID: 22922
			[DispId(1023)]
			bool oncontextmenu(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600598B RID: 22923
			[DispId(-2147418080)]
			void onrowsdelete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600598C RID: 22924
			[DispId(-2147418079)]
			void onrowsinserted(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600598D RID: 22925
			[DispId(-2147418078)]
			void oncellchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600598E RID: 22926
			[DispId(-609)]
			void onreadystatechange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600598F RID: 22927
			[DispId(1030)]
			void onlayoutcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005990 RID: 22928
			[DispId(1031)]
			void onpage(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005991 RID: 22929
			[DispId(1042)]
			void onmouseenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005992 RID: 22930
			[DispId(1043)]
			void onmouseleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005993 RID: 22931
			[DispId(1044)]
			void onactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005994 RID: 22932
			[DispId(1045)]
			void ondeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005995 RID: 22933
			[DispId(1034)]
			bool onbeforedeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005996 RID: 22934
			[DispId(1047)]
			bool onbeforeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005997 RID: 22935
			[DispId(1048)]
			void onfocusin(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005998 RID: 22936
			[DispId(1049)]
			void onfocusout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005999 RID: 22937
			[DispId(1035)]
			void onmove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600599A RID: 22938
			[DispId(1036)]
			bool oncontrolselect(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600599B RID: 22939
			[DispId(1038)]
			bool onmovestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600599C RID: 22940
			[DispId(1039)]
			void onmoveend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600599D RID: 22941
			[DispId(1040)]
			bool onresizestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600599E RID: 22942
			[DispId(1041)]
			void onresizeend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x0600599F RID: 22943
			[DispId(1033)]
			bool onmousewheel(UnsafeNativeMethods.IHTMLEventObj evtObj);
		}

		// Token: 0x02000587 RID: 1415
		[Guid("3050f611-98b5-11cf-bb82-00aa00bdce0b")]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		[TypeLibType(TypeLibTypeFlags.FHidden)]
		[ComImport]
		public interface DHTMLAreaEvents2
		{
			// Token: 0x060059A0 RID: 22944
			[DispId(-2147418102)]
			bool onhelp(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059A1 RID: 22945
			[DispId(-600)]
			bool onclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059A2 RID: 22946
			[DispId(-601)]
			bool ondblclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059A3 RID: 22947
			[DispId(-603)]
			bool onkeypress(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059A4 RID: 22948
			[DispId(-602)]
			void onkeydown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059A5 RID: 22949
			[DispId(-604)]
			void onkeyup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059A6 RID: 22950
			[DispId(-2147418103)]
			void onmouseout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059A7 RID: 22951
			[DispId(-2147418104)]
			void onmouseover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059A8 RID: 22952
			[DispId(-606)]
			void onmousemove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059A9 RID: 22953
			[DispId(-605)]
			void onmousedown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059AA RID: 22954
			[DispId(-607)]
			void onmouseup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059AB RID: 22955
			[DispId(-2147418100)]
			bool onselectstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059AC RID: 22956
			[DispId(-2147418095)]
			void onfilterchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059AD RID: 22957
			[DispId(-2147418101)]
			bool ondragstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059AE RID: 22958
			[DispId(-2147418108)]
			bool onbeforeupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059AF RID: 22959
			[DispId(-2147418107)]
			void onafterupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059B0 RID: 22960
			[DispId(-2147418099)]
			bool onerrorupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059B1 RID: 22961
			[DispId(-2147418106)]
			bool onrowexit(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059B2 RID: 22962
			[DispId(-2147418105)]
			void onrowenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059B3 RID: 22963
			[DispId(-2147418098)]
			void ondatasetchanged(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059B4 RID: 22964
			[DispId(-2147418097)]
			void ondataavailable(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059B5 RID: 22965
			[DispId(-2147418096)]
			void ondatasetcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059B6 RID: 22966
			[DispId(-2147418094)]
			void onlosecapture(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059B7 RID: 22967
			[DispId(-2147418093)]
			void onpropertychange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059B8 RID: 22968
			[DispId(1014)]
			void onscroll(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059B9 RID: 22969
			[DispId(-2147418111)]
			void onfocus(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059BA RID: 22970
			[DispId(-2147418112)]
			void onblur(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059BB RID: 22971
			[DispId(1016)]
			void onresize(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059BC RID: 22972
			[DispId(-2147418092)]
			bool ondrag(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059BD RID: 22973
			[DispId(-2147418091)]
			void ondragend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059BE RID: 22974
			[DispId(-2147418090)]
			bool ondragenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059BF RID: 22975
			[DispId(-2147418089)]
			bool ondragover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059C0 RID: 22976
			[DispId(-2147418088)]
			void ondragleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059C1 RID: 22977
			[DispId(-2147418087)]
			bool ondrop(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059C2 RID: 22978
			[DispId(-2147418083)]
			bool onbeforecut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059C3 RID: 22979
			[DispId(-2147418086)]
			bool oncut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059C4 RID: 22980
			[DispId(-2147418082)]
			bool onbeforecopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059C5 RID: 22981
			[DispId(-2147418085)]
			bool oncopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059C6 RID: 22982
			[DispId(-2147418081)]
			bool onbeforepaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059C7 RID: 22983
			[DispId(-2147418084)]
			bool onpaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059C8 RID: 22984
			[DispId(1023)]
			bool oncontextmenu(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059C9 RID: 22985
			[DispId(-2147418080)]
			void onrowsdelete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059CA RID: 22986
			[DispId(-2147418079)]
			void onrowsinserted(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059CB RID: 22987
			[DispId(-2147418078)]
			void oncellchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059CC RID: 22988
			[DispId(-609)]
			void onreadystatechange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059CD RID: 22989
			[DispId(1030)]
			void onlayoutcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059CE RID: 22990
			[DispId(1031)]
			void onpage(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059CF RID: 22991
			[DispId(1042)]
			void onmouseenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059D0 RID: 22992
			[DispId(1043)]
			void onmouseleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059D1 RID: 22993
			[DispId(1044)]
			void onactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059D2 RID: 22994
			[DispId(1045)]
			void ondeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059D3 RID: 22995
			[DispId(1034)]
			bool onbeforedeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059D4 RID: 22996
			[DispId(1047)]
			bool onbeforeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059D5 RID: 22997
			[DispId(1048)]
			void onfocusin(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059D6 RID: 22998
			[DispId(1049)]
			void onfocusout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059D7 RID: 22999
			[DispId(1035)]
			void onmove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059D8 RID: 23000
			[DispId(1036)]
			bool oncontrolselect(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059D9 RID: 23001
			[DispId(1038)]
			bool onmovestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059DA RID: 23002
			[DispId(1039)]
			void onmoveend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059DB RID: 23003
			[DispId(1040)]
			bool onresizestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059DC RID: 23004
			[DispId(1041)]
			void onresizeend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059DD RID: 23005
			[DispId(1033)]
			bool onmousewheel(UnsafeNativeMethods.IHTMLEventObj evtObj);
		}

		// Token: 0x02000588 RID: 1416
		[Guid("3050f617-98b5-11cf-bb82-00aa00bdce0b")]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		[TypeLibType(TypeLibTypeFlags.FHidden)]
		[ComImport]
		public interface DHTMLButtonElementEvents2
		{
			// Token: 0x060059DE RID: 23006
			[DispId(-2147418102)]
			bool onhelp(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059DF RID: 23007
			[DispId(-600)]
			bool onclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059E0 RID: 23008
			[DispId(-601)]
			bool ondblclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059E1 RID: 23009
			[DispId(-603)]
			bool onkeypress(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059E2 RID: 23010
			[DispId(-602)]
			void onkeydown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059E3 RID: 23011
			[DispId(-604)]
			void onkeyup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059E4 RID: 23012
			[DispId(-2147418103)]
			void onmouseout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059E5 RID: 23013
			[DispId(-2147418104)]
			void onmouseover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059E6 RID: 23014
			[DispId(-606)]
			void onmousemove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059E7 RID: 23015
			[DispId(-605)]
			void onmousedown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059E8 RID: 23016
			[DispId(-607)]
			void onmouseup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059E9 RID: 23017
			[DispId(-2147418100)]
			bool onselectstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059EA RID: 23018
			[DispId(-2147418095)]
			void onfilterchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059EB RID: 23019
			[DispId(-2147418101)]
			bool ondragstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059EC RID: 23020
			[DispId(-2147418108)]
			bool onbeforeupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059ED RID: 23021
			[DispId(-2147418107)]
			void onafterupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059EE RID: 23022
			[DispId(-2147418099)]
			bool onerrorupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059EF RID: 23023
			[DispId(-2147418106)]
			bool onrowexit(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059F0 RID: 23024
			[DispId(-2147418105)]
			void onrowenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059F1 RID: 23025
			[DispId(-2147418098)]
			void ondatasetchanged(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059F2 RID: 23026
			[DispId(-2147418097)]
			void ondataavailable(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059F3 RID: 23027
			[DispId(-2147418096)]
			void ondatasetcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059F4 RID: 23028
			[DispId(-2147418094)]
			void onlosecapture(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059F5 RID: 23029
			[DispId(-2147418093)]
			void onpropertychange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059F6 RID: 23030
			[DispId(1014)]
			void onscroll(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059F7 RID: 23031
			[DispId(-2147418111)]
			void onfocus(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059F8 RID: 23032
			[DispId(-2147418112)]
			void onblur(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059F9 RID: 23033
			[DispId(1016)]
			void onresize(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059FA RID: 23034
			[DispId(-2147418092)]
			bool ondrag(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059FB RID: 23035
			[DispId(-2147418091)]
			void ondragend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059FC RID: 23036
			[DispId(-2147418090)]
			bool ondragenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059FD RID: 23037
			[DispId(-2147418089)]
			bool ondragover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059FE RID: 23038
			[DispId(-2147418088)]
			void ondragleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x060059FF RID: 23039
			[DispId(-2147418087)]
			bool ondrop(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A00 RID: 23040
			[DispId(-2147418083)]
			bool onbeforecut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A01 RID: 23041
			[DispId(-2147418086)]
			bool oncut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A02 RID: 23042
			[DispId(-2147418082)]
			bool onbeforecopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A03 RID: 23043
			[DispId(-2147418085)]
			bool oncopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A04 RID: 23044
			[DispId(-2147418081)]
			bool onbeforepaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A05 RID: 23045
			[DispId(-2147418084)]
			bool onpaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A06 RID: 23046
			[DispId(1023)]
			bool oncontextmenu(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A07 RID: 23047
			[DispId(-2147418080)]
			void onrowsdelete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A08 RID: 23048
			[DispId(-2147418079)]
			void onrowsinserted(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A09 RID: 23049
			[DispId(-2147418078)]
			void oncellchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A0A RID: 23050
			[DispId(-609)]
			void onreadystatechange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A0B RID: 23051
			[DispId(1030)]
			void onlayoutcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A0C RID: 23052
			[DispId(1031)]
			void onpage(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A0D RID: 23053
			[DispId(1042)]
			void onmouseenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A0E RID: 23054
			[DispId(1043)]
			void onmouseleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A0F RID: 23055
			[DispId(1044)]
			void onactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A10 RID: 23056
			[DispId(1045)]
			void ondeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A11 RID: 23057
			[DispId(1034)]
			bool onbeforedeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A12 RID: 23058
			[DispId(1047)]
			bool onbeforeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A13 RID: 23059
			[DispId(1048)]
			void onfocusin(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A14 RID: 23060
			[DispId(1049)]
			void onfocusout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A15 RID: 23061
			[DispId(1035)]
			void onmove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A16 RID: 23062
			[DispId(1036)]
			bool oncontrolselect(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A17 RID: 23063
			[DispId(1038)]
			bool onmovestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A18 RID: 23064
			[DispId(1039)]
			void onmoveend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A19 RID: 23065
			[DispId(1040)]
			bool onresizestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A1A RID: 23066
			[DispId(1041)]
			void onresizeend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A1B RID: 23067
			[DispId(1033)]
			bool onmousewheel(UnsafeNativeMethods.IHTMLEventObj evtObj);
		}

		// Token: 0x02000589 RID: 1417
		[Guid("3050f612-98b5-11cf-bb82-00aa00bdce0b")]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		[TypeLibType(TypeLibTypeFlags.FHidden)]
		[ComImport]
		public interface DHTMLControlElementEvents2
		{
			// Token: 0x06005A1C RID: 23068
			[DispId(-2147418102)]
			bool onhelp(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A1D RID: 23069
			[DispId(-600)]
			bool onclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A1E RID: 23070
			[DispId(-601)]
			bool ondblclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A1F RID: 23071
			[DispId(-603)]
			bool onkeypress(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A20 RID: 23072
			[DispId(-602)]
			void onkeydown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A21 RID: 23073
			[DispId(-604)]
			void onkeyup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A22 RID: 23074
			[DispId(-2147418103)]
			void onmouseout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A23 RID: 23075
			[DispId(-2147418104)]
			void onmouseover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A24 RID: 23076
			[DispId(-606)]
			void onmousemove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A25 RID: 23077
			[DispId(-605)]
			void onmousedown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A26 RID: 23078
			[DispId(-607)]
			void onmouseup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A27 RID: 23079
			[DispId(-2147418100)]
			bool onselectstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A28 RID: 23080
			[DispId(-2147418095)]
			void onfilterchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A29 RID: 23081
			[DispId(-2147418101)]
			bool ondragstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A2A RID: 23082
			[DispId(-2147418108)]
			bool onbeforeupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A2B RID: 23083
			[DispId(-2147418107)]
			void onafterupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A2C RID: 23084
			[DispId(-2147418099)]
			bool onerrorupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A2D RID: 23085
			[DispId(-2147418106)]
			bool onrowexit(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A2E RID: 23086
			[DispId(-2147418105)]
			void onrowenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A2F RID: 23087
			[DispId(-2147418098)]
			void ondatasetchanged(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A30 RID: 23088
			[DispId(-2147418097)]
			void ondataavailable(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A31 RID: 23089
			[DispId(-2147418096)]
			void ondatasetcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A32 RID: 23090
			[DispId(-2147418094)]
			void onlosecapture(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A33 RID: 23091
			[DispId(-2147418093)]
			void onpropertychange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A34 RID: 23092
			[DispId(1014)]
			void onscroll(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A35 RID: 23093
			[DispId(-2147418111)]
			void onfocus(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A36 RID: 23094
			[DispId(-2147418112)]
			void onblur(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A37 RID: 23095
			[DispId(1016)]
			void onresize(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A38 RID: 23096
			[DispId(-2147418092)]
			bool ondrag(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A39 RID: 23097
			[DispId(-2147418091)]
			void ondragend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A3A RID: 23098
			[DispId(-2147418090)]
			bool ondragenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A3B RID: 23099
			[DispId(-2147418089)]
			bool ondragover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A3C RID: 23100
			[DispId(-2147418088)]
			void ondragleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A3D RID: 23101
			[DispId(-2147418087)]
			bool ondrop(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A3E RID: 23102
			[DispId(-2147418083)]
			bool onbeforecut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A3F RID: 23103
			[DispId(-2147418086)]
			bool oncut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A40 RID: 23104
			[DispId(-2147418082)]
			bool onbeforecopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A41 RID: 23105
			[DispId(-2147418085)]
			bool oncopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A42 RID: 23106
			[DispId(-2147418081)]
			bool onbeforepaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A43 RID: 23107
			[DispId(-2147418084)]
			bool onpaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A44 RID: 23108
			[DispId(1023)]
			bool oncontextmenu(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A45 RID: 23109
			[DispId(-2147418080)]
			void onrowsdelete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A46 RID: 23110
			[DispId(-2147418079)]
			void onrowsinserted(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A47 RID: 23111
			[DispId(-2147418078)]
			void oncellchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A48 RID: 23112
			[DispId(-609)]
			void onreadystatechange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A49 RID: 23113
			[DispId(1030)]
			void onlayoutcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A4A RID: 23114
			[DispId(1031)]
			void onpage(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A4B RID: 23115
			[DispId(1042)]
			void onmouseenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A4C RID: 23116
			[DispId(1043)]
			void onmouseleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A4D RID: 23117
			[DispId(1044)]
			void onactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A4E RID: 23118
			[DispId(1045)]
			void ondeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A4F RID: 23119
			[DispId(1034)]
			bool onbeforedeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A50 RID: 23120
			[DispId(1047)]
			bool onbeforeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A51 RID: 23121
			[DispId(1048)]
			void onfocusin(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A52 RID: 23122
			[DispId(1049)]
			void onfocusout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A53 RID: 23123
			[DispId(1035)]
			void onmove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A54 RID: 23124
			[DispId(1036)]
			bool oncontrolselect(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A55 RID: 23125
			[DispId(1038)]
			bool onmovestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A56 RID: 23126
			[DispId(1039)]
			void onmoveend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A57 RID: 23127
			[DispId(1040)]
			bool onresizestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A58 RID: 23128
			[DispId(1041)]
			void onresizeend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A59 RID: 23129
			[DispId(1033)]
			bool onmousewheel(UnsafeNativeMethods.IHTMLEventObj evtObj);
		}

		// Token: 0x0200058A RID: 1418
		[Guid("3050f614-98b5-11cf-bb82-00aa00bdce0b")]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		[TypeLibType(TypeLibTypeFlags.FHidden)]
		[ComImport]
		public interface DHTMLFormElementEvents2
		{
			// Token: 0x06005A5A RID: 23130
			[DispId(-2147418102)]
			bool onhelp(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A5B RID: 23131
			[DispId(-600)]
			bool onclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A5C RID: 23132
			[DispId(-601)]
			bool ondblclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A5D RID: 23133
			[DispId(-603)]
			bool onkeypress(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A5E RID: 23134
			[DispId(-602)]
			void onkeydown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A5F RID: 23135
			[DispId(-604)]
			void onkeyup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A60 RID: 23136
			[DispId(-2147418103)]
			void onmouseout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A61 RID: 23137
			[DispId(-2147418104)]
			void onmouseover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A62 RID: 23138
			[DispId(-606)]
			void onmousemove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A63 RID: 23139
			[DispId(-605)]
			void onmousedown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A64 RID: 23140
			[DispId(-607)]
			void onmouseup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A65 RID: 23141
			[DispId(-2147418100)]
			bool onselectstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A66 RID: 23142
			[DispId(-2147418095)]
			void onfilterchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A67 RID: 23143
			[DispId(-2147418101)]
			bool ondragstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A68 RID: 23144
			[DispId(-2147418108)]
			bool onbeforeupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A69 RID: 23145
			[DispId(-2147418107)]
			void onafterupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A6A RID: 23146
			[DispId(-2147418099)]
			bool onerrorupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A6B RID: 23147
			[DispId(-2147418106)]
			bool onrowexit(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A6C RID: 23148
			[DispId(-2147418105)]
			void onrowenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A6D RID: 23149
			[DispId(-2147418098)]
			void ondatasetchanged(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A6E RID: 23150
			[DispId(-2147418097)]
			void ondataavailable(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A6F RID: 23151
			[DispId(-2147418096)]
			void ondatasetcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A70 RID: 23152
			[DispId(-2147418094)]
			void onlosecapture(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A71 RID: 23153
			[DispId(-2147418093)]
			void onpropertychange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A72 RID: 23154
			[DispId(1014)]
			void onscroll(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A73 RID: 23155
			[DispId(-2147418111)]
			void onfocus(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A74 RID: 23156
			[DispId(-2147418112)]
			void onblur(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A75 RID: 23157
			[DispId(1016)]
			void onresize(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A76 RID: 23158
			[DispId(-2147418092)]
			bool ondrag(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A77 RID: 23159
			[DispId(-2147418091)]
			void ondragend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A78 RID: 23160
			[DispId(-2147418090)]
			bool ondragenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A79 RID: 23161
			[DispId(-2147418089)]
			bool ondragover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A7A RID: 23162
			[DispId(-2147418088)]
			void ondragleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A7B RID: 23163
			[DispId(-2147418087)]
			bool ondrop(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A7C RID: 23164
			[DispId(-2147418083)]
			bool onbeforecut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A7D RID: 23165
			[DispId(-2147418086)]
			bool oncut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A7E RID: 23166
			[DispId(-2147418082)]
			bool onbeforecopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A7F RID: 23167
			[DispId(-2147418085)]
			bool oncopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A80 RID: 23168
			[DispId(-2147418081)]
			bool onbeforepaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A81 RID: 23169
			[DispId(-2147418084)]
			bool onpaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A82 RID: 23170
			[DispId(1023)]
			bool oncontextmenu(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A83 RID: 23171
			[DispId(-2147418080)]
			void onrowsdelete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A84 RID: 23172
			[DispId(-2147418079)]
			void onrowsinserted(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A85 RID: 23173
			[DispId(-2147418078)]
			void oncellchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A86 RID: 23174
			[DispId(-609)]
			void onreadystatechange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A87 RID: 23175
			[DispId(1030)]
			void onlayoutcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A88 RID: 23176
			[DispId(1031)]
			void onpage(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A89 RID: 23177
			[DispId(1042)]
			void onmouseenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A8A RID: 23178
			[DispId(1043)]
			void onmouseleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A8B RID: 23179
			[DispId(1044)]
			void onactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A8C RID: 23180
			[DispId(1045)]
			void ondeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A8D RID: 23181
			[DispId(1034)]
			bool onbeforedeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A8E RID: 23182
			[DispId(1047)]
			bool onbeforeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A8F RID: 23183
			[DispId(1048)]
			void onfocusin(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A90 RID: 23184
			[DispId(1049)]
			void onfocusout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A91 RID: 23185
			[DispId(1035)]
			void onmove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A92 RID: 23186
			[DispId(1036)]
			bool oncontrolselect(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A93 RID: 23187
			[DispId(1038)]
			bool onmovestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A94 RID: 23188
			[DispId(1039)]
			void onmoveend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A95 RID: 23189
			[DispId(1040)]
			bool onresizestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A96 RID: 23190
			[DispId(1041)]
			void onresizeend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A97 RID: 23191
			[DispId(1033)]
			bool onmousewheel(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A98 RID: 23192
			[DispId(1007)]
			bool onsubmit(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A99 RID: 23193
			[DispId(1015)]
			bool onreset(UnsafeNativeMethods.IHTMLEventObj evtObj);
		}

		// Token: 0x0200058B RID: 1419
		[Guid("3050f7ff-98b5-11cf-bb82-00aa00bdce0b")]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		[TypeLibType(TypeLibTypeFlags.FHidden)]
		[ComImport]
		public interface DHTMLFrameSiteEvents2
		{
			// Token: 0x06005A9A RID: 23194
			[DispId(-2147418102)]
			bool onhelp(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A9B RID: 23195
			[DispId(-600)]
			bool onclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A9C RID: 23196
			[DispId(-601)]
			bool ondblclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A9D RID: 23197
			[DispId(-603)]
			bool onkeypress(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A9E RID: 23198
			[DispId(-602)]
			void onkeydown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005A9F RID: 23199
			[DispId(-604)]
			void onkeyup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AA0 RID: 23200
			[DispId(-2147418103)]
			void onmouseout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AA1 RID: 23201
			[DispId(-2147418104)]
			void onmouseover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AA2 RID: 23202
			[DispId(-606)]
			void onmousemove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AA3 RID: 23203
			[DispId(-605)]
			void onmousedown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AA4 RID: 23204
			[DispId(-607)]
			void onmouseup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AA5 RID: 23205
			[DispId(-2147418100)]
			bool onselectstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AA6 RID: 23206
			[DispId(-2147418095)]
			void onfilterchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AA7 RID: 23207
			[DispId(-2147418101)]
			bool ondragstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AA8 RID: 23208
			[DispId(-2147418108)]
			bool onbeforeupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AA9 RID: 23209
			[DispId(-2147418107)]
			void onafterupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AAA RID: 23210
			[DispId(-2147418099)]
			bool onerrorupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AAB RID: 23211
			[DispId(-2147418106)]
			bool onrowexit(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AAC RID: 23212
			[DispId(-2147418105)]
			void onrowenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AAD RID: 23213
			[DispId(-2147418098)]
			void ondatasetchanged(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AAE RID: 23214
			[DispId(-2147418097)]
			void ondataavailable(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AAF RID: 23215
			[DispId(-2147418096)]
			void ondatasetcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AB0 RID: 23216
			[DispId(-2147418094)]
			void onlosecapture(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AB1 RID: 23217
			[DispId(-2147418093)]
			void onpropertychange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AB2 RID: 23218
			[DispId(1014)]
			void onscroll(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AB3 RID: 23219
			[DispId(-2147418111)]
			void onfocus(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AB4 RID: 23220
			[DispId(-2147418112)]
			void onblur(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AB5 RID: 23221
			[DispId(1016)]
			void onresize(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AB6 RID: 23222
			[DispId(-2147418092)]
			bool ondrag(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AB7 RID: 23223
			[DispId(-2147418091)]
			void ondragend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AB8 RID: 23224
			[DispId(-2147418090)]
			bool ondragenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AB9 RID: 23225
			[DispId(-2147418089)]
			bool ondragover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005ABA RID: 23226
			[DispId(-2147418088)]
			void ondragleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005ABB RID: 23227
			[DispId(-2147418087)]
			bool ondrop(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005ABC RID: 23228
			[DispId(-2147418083)]
			bool onbeforecut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005ABD RID: 23229
			[DispId(-2147418086)]
			bool oncut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005ABE RID: 23230
			[DispId(-2147418082)]
			bool onbeforecopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005ABF RID: 23231
			[DispId(-2147418085)]
			bool oncopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AC0 RID: 23232
			[DispId(-2147418081)]
			bool onbeforepaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AC1 RID: 23233
			[DispId(-2147418084)]
			bool onpaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AC2 RID: 23234
			[DispId(1023)]
			bool oncontextmenu(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AC3 RID: 23235
			[DispId(-2147418080)]
			void onrowsdelete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AC4 RID: 23236
			[DispId(-2147418079)]
			void onrowsinserted(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AC5 RID: 23237
			[DispId(-2147418078)]
			void oncellchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AC6 RID: 23238
			[DispId(-609)]
			void onreadystatechange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AC7 RID: 23239
			[DispId(1030)]
			void onlayoutcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AC8 RID: 23240
			[DispId(1031)]
			void onpage(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AC9 RID: 23241
			[DispId(1042)]
			void onmouseenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005ACA RID: 23242
			[DispId(1043)]
			void onmouseleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005ACB RID: 23243
			[DispId(1044)]
			void onactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005ACC RID: 23244
			[DispId(1045)]
			void ondeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005ACD RID: 23245
			[DispId(1034)]
			bool onbeforedeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005ACE RID: 23246
			[DispId(1047)]
			bool onbeforeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005ACF RID: 23247
			[DispId(1048)]
			void onfocusin(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AD0 RID: 23248
			[DispId(1049)]
			void onfocusout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AD1 RID: 23249
			[DispId(1035)]
			void onmove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AD2 RID: 23250
			[DispId(1036)]
			bool oncontrolselect(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AD3 RID: 23251
			[DispId(1038)]
			bool onmovestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AD4 RID: 23252
			[DispId(1039)]
			void onmoveend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AD5 RID: 23253
			[DispId(1040)]
			bool onresizestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AD6 RID: 23254
			[DispId(1041)]
			void onresizeend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AD7 RID: 23255
			[DispId(1033)]
			bool onmousewheel(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AD8 RID: 23256
			[DispId(1003)]
			void onload(UnsafeNativeMethods.IHTMLEventObj evtObj);
		}

		// Token: 0x0200058C RID: 1420
		[Guid("3050f616-98b5-11cf-bb82-00aa00bdce0b")]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		[TypeLibType(TypeLibTypeFlags.FHidden)]
		[ComImport]
		public interface DHTMLImgEvents2
		{
			// Token: 0x06005AD9 RID: 23257
			[DispId(-2147418102)]
			bool onhelp(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005ADA RID: 23258
			[DispId(-600)]
			bool onclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005ADB RID: 23259
			[DispId(-601)]
			bool ondblclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005ADC RID: 23260
			[DispId(-603)]
			bool onkeypress(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005ADD RID: 23261
			[DispId(-602)]
			void onkeydown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005ADE RID: 23262
			[DispId(-604)]
			void onkeyup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005ADF RID: 23263
			[DispId(-2147418103)]
			void onmouseout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AE0 RID: 23264
			[DispId(-2147418104)]
			void onmouseover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AE1 RID: 23265
			[DispId(-606)]
			void onmousemove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AE2 RID: 23266
			[DispId(-605)]
			void onmousedown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AE3 RID: 23267
			[DispId(-607)]
			void onmouseup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AE4 RID: 23268
			[DispId(-2147418100)]
			bool onselectstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AE5 RID: 23269
			[DispId(-2147418095)]
			void onfilterchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AE6 RID: 23270
			[DispId(-2147418101)]
			bool ondragstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AE7 RID: 23271
			[DispId(-2147418108)]
			bool onbeforeupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AE8 RID: 23272
			[DispId(-2147418107)]
			void onafterupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AE9 RID: 23273
			[DispId(-2147418099)]
			bool onerrorupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AEA RID: 23274
			[DispId(-2147418106)]
			bool onrowexit(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AEB RID: 23275
			[DispId(-2147418105)]
			void onrowenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AEC RID: 23276
			[DispId(-2147418098)]
			void ondatasetchanged(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AED RID: 23277
			[DispId(-2147418097)]
			void ondataavailable(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AEE RID: 23278
			[DispId(-2147418096)]
			void ondatasetcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AEF RID: 23279
			[DispId(-2147418094)]
			void onlosecapture(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AF0 RID: 23280
			[DispId(-2147418093)]
			void onpropertychange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AF1 RID: 23281
			[DispId(1014)]
			void onscroll(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AF2 RID: 23282
			[DispId(-2147418111)]
			void onfocus(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AF3 RID: 23283
			[DispId(-2147418112)]
			void onblur(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AF4 RID: 23284
			[DispId(1016)]
			void onresize(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AF5 RID: 23285
			[DispId(-2147418092)]
			bool ondrag(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AF6 RID: 23286
			[DispId(-2147418091)]
			void ondragend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AF7 RID: 23287
			[DispId(-2147418090)]
			bool ondragenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AF8 RID: 23288
			[DispId(-2147418089)]
			bool ondragover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AF9 RID: 23289
			[DispId(-2147418088)]
			void ondragleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AFA RID: 23290
			[DispId(-2147418087)]
			bool ondrop(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AFB RID: 23291
			[DispId(-2147418083)]
			bool onbeforecut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AFC RID: 23292
			[DispId(-2147418086)]
			bool oncut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AFD RID: 23293
			[DispId(-2147418082)]
			bool onbeforecopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AFE RID: 23294
			[DispId(-2147418085)]
			bool oncopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005AFF RID: 23295
			[DispId(-2147418081)]
			bool onbeforepaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B00 RID: 23296
			[DispId(-2147418084)]
			bool onpaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B01 RID: 23297
			[DispId(1023)]
			bool oncontextmenu(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B02 RID: 23298
			[DispId(-2147418080)]
			void onrowsdelete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B03 RID: 23299
			[DispId(-2147418079)]
			void onrowsinserted(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B04 RID: 23300
			[DispId(-2147418078)]
			void oncellchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B05 RID: 23301
			[DispId(-609)]
			void onreadystatechange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B06 RID: 23302
			[DispId(1030)]
			void onlayoutcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B07 RID: 23303
			[DispId(1031)]
			void onpage(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B08 RID: 23304
			[DispId(1042)]
			void onmouseenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B09 RID: 23305
			[DispId(1043)]
			void onmouseleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B0A RID: 23306
			[DispId(1044)]
			void onactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B0B RID: 23307
			[DispId(1045)]
			void ondeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B0C RID: 23308
			[DispId(1034)]
			bool onbeforedeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B0D RID: 23309
			[DispId(1047)]
			bool onbeforeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B0E RID: 23310
			[DispId(1048)]
			void onfocusin(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B0F RID: 23311
			[DispId(1049)]
			void onfocusout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B10 RID: 23312
			[DispId(1035)]
			void onmove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B11 RID: 23313
			[DispId(1036)]
			bool oncontrolselect(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B12 RID: 23314
			[DispId(1038)]
			bool onmovestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B13 RID: 23315
			[DispId(1039)]
			void onmoveend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B14 RID: 23316
			[DispId(1040)]
			bool onresizestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B15 RID: 23317
			[DispId(1041)]
			void onresizeend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B16 RID: 23318
			[DispId(1033)]
			bool onmousewheel(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B17 RID: 23319
			[DispId(1003)]
			void onload(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B18 RID: 23320
			[DispId(1002)]
			void onerror(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B19 RID: 23321
			[DispId(1000)]
			void onabort(UnsafeNativeMethods.IHTMLEventObj evtObj);
		}

		// Token: 0x0200058D RID: 1421
		[Guid("3050f61a-98b5-11cf-bb82-00aa00bdce0b")]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		[TypeLibType(TypeLibTypeFlags.FHidden)]
		[ComImport]
		public interface DHTMLInputFileElementEvents2
		{
			// Token: 0x06005B1A RID: 23322
			[DispId(-2147418102)]
			bool onhelp(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B1B RID: 23323
			[DispId(-600)]
			bool onclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B1C RID: 23324
			[DispId(-601)]
			bool ondblclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B1D RID: 23325
			[DispId(-603)]
			bool onkeypress(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B1E RID: 23326
			[DispId(-602)]
			void onkeydown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B1F RID: 23327
			[DispId(-604)]
			void onkeyup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B20 RID: 23328
			[DispId(-2147418103)]
			void onmouseout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B21 RID: 23329
			[DispId(-2147418104)]
			void onmouseover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B22 RID: 23330
			[DispId(-606)]
			void onmousemove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B23 RID: 23331
			[DispId(-605)]
			void onmousedown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B24 RID: 23332
			[DispId(-607)]
			void onmouseup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B25 RID: 23333
			[DispId(-2147418100)]
			bool onselectstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B26 RID: 23334
			[DispId(-2147418095)]
			void onfilterchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B27 RID: 23335
			[DispId(-2147418101)]
			bool ondragstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B28 RID: 23336
			[DispId(-2147418108)]
			bool onbeforeupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B29 RID: 23337
			[DispId(-2147418107)]
			void onafterupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B2A RID: 23338
			[DispId(-2147418099)]
			bool onerrorupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B2B RID: 23339
			[DispId(-2147418106)]
			bool onrowexit(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B2C RID: 23340
			[DispId(-2147418105)]
			void onrowenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B2D RID: 23341
			[DispId(-2147418098)]
			void ondatasetchanged(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B2E RID: 23342
			[DispId(-2147418097)]
			void ondataavailable(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B2F RID: 23343
			[DispId(-2147418096)]
			void ondatasetcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B30 RID: 23344
			[DispId(-2147418094)]
			void onlosecapture(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B31 RID: 23345
			[DispId(-2147418093)]
			void onpropertychange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B32 RID: 23346
			[DispId(1014)]
			void onscroll(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B33 RID: 23347
			[DispId(-2147418111)]
			void onfocus(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B34 RID: 23348
			[DispId(-2147418112)]
			void onblur(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B35 RID: 23349
			[DispId(1016)]
			void onresize(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B36 RID: 23350
			[DispId(-2147418092)]
			bool ondrag(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B37 RID: 23351
			[DispId(-2147418091)]
			void ondragend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B38 RID: 23352
			[DispId(-2147418090)]
			bool ondragenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B39 RID: 23353
			[DispId(-2147418089)]
			bool ondragover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B3A RID: 23354
			[DispId(-2147418088)]
			void ondragleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B3B RID: 23355
			[DispId(-2147418087)]
			bool ondrop(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B3C RID: 23356
			[DispId(-2147418083)]
			bool onbeforecut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B3D RID: 23357
			[DispId(-2147418086)]
			bool oncut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B3E RID: 23358
			[DispId(-2147418082)]
			bool onbeforecopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B3F RID: 23359
			[DispId(-2147418085)]
			bool oncopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B40 RID: 23360
			[DispId(-2147418081)]
			bool onbeforepaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B41 RID: 23361
			[DispId(-2147418084)]
			bool onpaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B42 RID: 23362
			[DispId(1023)]
			bool oncontextmenu(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B43 RID: 23363
			[DispId(-2147418080)]
			void onrowsdelete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B44 RID: 23364
			[DispId(-2147418079)]
			void onrowsinserted(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B45 RID: 23365
			[DispId(-2147418078)]
			void oncellchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B46 RID: 23366
			[DispId(-609)]
			void onreadystatechange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B47 RID: 23367
			[DispId(1030)]
			void onlayoutcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B48 RID: 23368
			[DispId(1031)]
			void onpage(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B49 RID: 23369
			[DispId(1042)]
			void onmouseenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B4A RID: 23370
			[DispId(1043)]
			void onmouseleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B4B RID: 23371
			[DispId(1044)]
			void onactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B4C RID: 23372
			[DispId(1045)]
			void ondeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B4D RID: 23373
			[DispId(1034)]
			bool onbeforedeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B4E RID: 23374
			[DispId(1047)]
			bool onbeforeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B4F RID: 23375
			[DispId(1048)]
			void onfocusin(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B50 RID: 23376
			[DispId(1049)]
			void onfocusout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B51 RID: 23377
			[DispId(1035)]
			void onmove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B52 RID: 23378
			[DispId(1036)]
			bool oncontrolselect(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B53 RID: 23379
			[DispId(1038)]
			bool onmovestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B54 RID: 23380
			[DispId(1039)]
			void onmoveend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B55 RID: 23381
			[DispId(1040)]
			bool onresizestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B56 RID: 23382
			[DispId(1041)]
			void onresizeend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B57 RID: 23383
			[DispId(1033)]
			bool onmousewheel(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B58 RID: 23384
			[DispId(-2147412082)]
			bool onchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B59 RID: 23385
			[DispId(-2147412102)]
			void onselect(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B5A RID: 23386
			[DispId(1003)]
			void onload(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B5B RID: 23387
			[DispId(1002)]
			void onerror(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B5C RID: 23388
			[DispId(1000)]
			void onabort(UnsafeNativeMethods.IHTMLEventObj evtObj);
		}

		// Token: 0x0200058E RID: 1422
		[Guid("3050f61b-98b5-11cf-bb82-00aa00bdce0b")]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		[TypeLibType(TypeLibTypeFlags.FHidden)]
		[ComImport]
		public interface DHTMLInputImageEvents2
		{
			// Token: 0x06005B5D RID: 23389
			[DispId(-2147418102)]
			bool onhelp(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B5E RID: 23390
			[DispId(-600)]
			bool onclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B5F RID: 23391
			[DispId(-601)]
			bool ondblclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B60 RID: 23392
			[DispId(-603)]
			bool onkeypress(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B61 RID: 23393
			[DispId(-602)]
			void onkeydown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B62 RID: 23394
			[DispId(-604)]
			void onkeyup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B63 RID: 23395
			[DispId(-2147418103)]
			void onmouseout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B64 RID: 23396
			[DispId(-2147418104)]
			void onmouseover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B65 RID: 23397
			[DispId(-606)]
			void onmousemove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B66 RID: 23398
			[DispId(-605)]
			void onmousedown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B67 RID: 23399
			[DispId(-607)]
			void onmouseup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B68 RID: 23400
			[DispId(-2147418100)]
			bool onselectstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B69 RID: 23401
			[DispId(-2147418095)]
			void onfilterchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B6A RID: 23402
			[DispId(-2147418101)]
			bool ondragstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B6B RID: 23403
			[DispId(-2147418108)]
			bool onbeforeupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B6C RID: 23404
			[DispId(-2147418107)]
			void onafterupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B6D RID: 23405
			[DispId(-2147418099)]
			bool onerrorupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B6E RID: 23406
			[DispId(-2147418106)]
			bool onrowexit(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B6F RID: 23407
			[DispId(-2147418105)]
			void onrowenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B70 RID: 23408
			[DispId(-2147418098)]
			void ondatasetchanged(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B71 RID: 23409
			[DispId(-2147418097)]
			void ondataavailable(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B72 RID: 23410
			[DispId(-2147418096)]
			void ondatasetcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B73 RID: 23411
			[DispId(-2147418094)]
			void onlosecapture(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B74 RID: 23412
			[DispId(-2147418093)]
			void onpropertychange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B75 RID: 23413
			[DispId(1014)]
			void onscroll(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B76 RID: 23414
			[DispId(-2147418111)]
			void onfocus(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B77 RID: 23415
			[DispId(-2147418112)]
			void onblur(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B78 RID: 23416
			[DispId(1016)]
			void onresize(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B79 RID: 23417
			[DispId(-2147418092)]
			bool ondrag(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B7A RID: 23418
			[DispId(-2147418091)]
			void ondragend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B7B RID: 23419
			[DispId(-2147418090)]
			bool ondragenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B7C RID: 23420
			[DispId(-2147418089)]
			bool ondragover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B7D RID: 23421
			[DispId(-2147418088)]
			void ondragleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B7E RID: 23422
			[DispId(-2147418087)]
			bool ondrop(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B7F RID: 23423
			[DispId(-2147418083)]
			bool onbeforecut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B80 RID: 23424
			[DispId(-2147418086)]
			bool oncut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B81 RID: 23425
			[DispId(-2147418082)]
			bool onbeforecopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B82 RID: 23426
			[DispId(-2147418085)]
			bool oncopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B83 RID: 23427
			[DispId(-2147418081)]
			bool onbeforepaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B84 RID: 23428
			[DispId(-2147418084)]
			bool onpaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B85 RID: 23429
			[DispId(1023)]
			bool oncontextmenu(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B86 RID: 23430
			[DispId(-2147418080)]
			void onrowsdelete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B87 RID: 23431
			[DispId(-2147418079)]
			void onrowsinserted(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B88 RID: 23432
			[DispId(-2147418078)]
			void oncellchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B89 RID: 23433
			[DispId(-609)]
			void onreadystatechange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B8A RID: 23434
			[DispId(1030)]
			void onlayoutcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B8B RID: 23435
			[DispId(1031)]
			void onpage(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B8C RID: 23436
			[DispId(1042)]
			void onmouseenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B8D RID: 23437
			[DispId(1043)]
			void onmouseleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B8E RID: 23438
			[DispId(1044)]
			void onactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B8F RID: 23439
			[DispId(1045)]
			void ondeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B90 RID: 23440
			[DispId(1034)]
			bool onbeforedeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B91 RID: 23441
			[DispId(1047)]
			bool onbeforeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B92 RID: 23442
			[DispId(1048)]
			void onfocusin(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B93 RID: 23443
			[DispId(1049)]
			void onfocusout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B94 RID: 23444
			[DispId(1035)]
			void onmove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B95 RID: 23445
			[DispId(1036)]
			bool oncontrolselect(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B96 RID: 23446
			[DispId(1038)]
			bool onmovestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B97 RID: 23447
			[DispId(1039)]
			void onmoveend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B98 RID: 23448
			[DispId(1040)]
			bool onresizestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B99 RID: 23449
			[DispId(1041)]
			void onresizeend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B9A RID: 23450
			[DispId(1033)]
			bool onmousewheel(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B9B RID: 23451
			[DispId(-2147412080)]
			void onload(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B9C RID: 23452
			[DispId(-2147412083)]
			void onerror(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B9D RID: 23453
			[DispId(-2147412084)]
			void onabort(UnsafeNativeMethods.IHTMLEventObj evtObj);
		}

		// Token: 0x0200058F RID: 1423
		[Guid("3050f618-98b5-11cf-bb82-00aa00bdce0b")]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		[TypeLibType(TypeLibTypeFlags.FHidden)]
		[ComImport]
		public interface DHTMLInputTextElementEvents2
		{
			// Token: 0x06005B9E RID: 23454
			[DispId(-2147418102)]
			bool onhelp(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005B9F RID: 23455
			[DispId(-600)]
			bool onclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BA0 RID: 23456
			[DispId(-601)]
			bool ondblclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BA1 RID: 23457
			[DispId(-603)]
			bool onkeypress(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BA2 RID: 23458
			[DispId(-602)]
			void onkeydown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BA3 RID: 23459
			[DispId(-604)]
			void onkeyup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BA4 RID: 23460
			[DispId(-2147418103)]
			void onmouseout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BA5 RID: 23461
			[DispId(-2147418104)]
			void onmouseover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BA6 RID: 23462
			[DispId(-606)]
			void onmousemove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BA7 RID: 23463
			[DispId(-605)]
			void onmousedown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BA8 RID: 23464
			[DispId(-607)]
			void onmouseup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BA9 RID: 23465
			[DispId(-2147418100)]
			bool onselectstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BAA RID: 23466
			[DispId(-2147418095)]
			void onfilterchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BAB RID: 23467
			[DispId(-2147418101)]
			bool ondragstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BAC RID: 23468
			[DispId(-2147418108)]
			bool onbeforeupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BAD RID: 23469
			[DispId(-2147418107)]
			void onafterupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BAE RID: 23470
			[DispId(-2147418099)]
			bool onerrorupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BAF RID: 23471
			[DispId(-2147418106)]
			bool onrowexit(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BB0 RID: 23472
			[DispId(-2147418105)]
			void onrowenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BB1 RID: 23473
			[DispId(-2147418098)]
			void ondatasetchanged(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BB2 RID: 23474
			[DispId(-2147418097)]
			void ondataavailable(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BB3 RID: 23475
			[DispId(-2147418096)]
			void ondatasetcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BB4 RID: 23476
			[DispId(-2147418094)]
			void onlosecapture(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BB5 RID: 23477
			[DispId(-2147418093)]
			void onpropertychange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BB6 RID: 23478
			[DispId(1014)]
			void onscroll(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BB7 RID: 23479
			[DispId(-2147418111)]
			void onfocus(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BB8 RID: 23480
			[DispId(-2147418112)]
			void onblur(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BB9 RID: 23481
			[DispId(1016)]
			void onresize(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BBA RID: 23482
			[DispId(-2147418092)]
			bool ondrag(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BBB RID: 23483
			[DispId(-2147418091)]
			void ondragend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BBC RID: 23484
			[DispId(-2147418090)]
			bool ondragenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BBD RID: 23485
			[DispId(-2147418089)]
			bool ondragover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BBE RID: 23486
			[DispId(-2147418088)]
			void ondragleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BBF RID: 23487
			[DispId(-2147418087)]
			bool ondrop(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BC0 RID: 23488
			[DispId(-2147418083)]
			bool onbeforecut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BC1 RID: 23489
			[DispId(-2147418086)]
			bool oncut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BC2 RID: 23490
			[DispId(-2147418082)]
			bool onbeforecopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BC3 RID: 23491
			[DispId(-2147418085)]
			bool oncopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BC4 RID: 23492
			[DispId(-2147418081)]
			bool onbeforepaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BC5 RID: 23493
			[DispId(-2147418084)]
			bool onpaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BC6 RID: 23494
			[DispId(1023)]
			bool oncontextmenu(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BC7 RID: 23495
			[DispId(-2147418080)]
			void onrowsdelete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BC8 RID: 23496
			[DispId(-2147418079)]
			void onrowsinserted(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BC9 RID: 23497
			[DispId(-2147418078)]
			void oncellchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BCA RID: 23498
			[DispId(-609)]
			void onreadystatechange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BCB RID: 23499
			[DispId(1030)]
			void onlayoutcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BCC RID: 23500
			[DispId(1031)]
			void onpage(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BCD RID: 23501
			[DispId(1042)]
			void onmouseenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BCE RID: 23502
			[DispId(1043)]
			void onmouseleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BCF RID: 23503
			[DispId(1044)]
			void onactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BD0 RID: 23504
			[DispId(1045)]
			void ondeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BD1 RID: 23505
			[DispId(1034)]
			bool onbeforedeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BD2 RID: 23506
			[DispId(1047)]
			bool onbeforeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BD3 RID: 23507
			[DispId(1048)]
			void onfocusin(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BD4 RID: 23508
			[DispId(1049)]
			void onfocusout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BD5 RID: 23509
			[DispId(1035)]
			void onmove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BD6 RID: 23510
			[DispId(1036)]
			bool oncontrolselect(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BD7 RID: 23511
			[DispId(1038)]
			bool onmovestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BD8 RID: 23512
			[DispId(1039)]
			void onmoveend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BD9 RID: 23513
			[DispId(1040)]
			bool onresizestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BDA RID: 23514
			[DispId(1041)]
			void onresizeend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BDB RID: 23515
			[DispId(1033)]
			bool onmousewheel(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BDC RID: 23516
			[DispId(1001)]
			bool onchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BDD RID: 23517
			[DispId(1006)]
			void onselect(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BDE RID: 23518
			[DispId(1003)]
			void onload(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BDF RID: 23519
			[DispId(1002)]
			void onerror(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BE0 RID: 23520
			[DispId(1001)]
			void onabort(UnsafeNativeMethods.IHTMLEventObj evtObj);
		}

		// Token: 0x02000590 RID: 1424
		[Guid("3050f61c-98b5-11cf-bb82-00aa00bdce0b")]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		[TypeLibType(TypeLibTypeFlags.FHidden)]
		[ComImport]
		public interface DHTMLLabelEvents2
		{
			// Token: 0x06005BE1 RID: 23521
			[DispId(-2147418102)]
			bool onhelp(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BE2 RID: 23522
			[DispId(-600)]
			bool onclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BE3 RID: 23523
			[DispId(-601)]
			bool ondblclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BE4 RID: 23524
			[DispId(-603)]
			bool onkeypress(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BE5 RID: 23525
			[DispId(-602)]
			void onkeydown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BE6 RID: 23526
			[DispId(-604)]
			void onkeyup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BE7 RID: 23527
			[DispId(-2147418103)]
			void onmouseout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BE8 RID: 23528
			[DispId(-2147418104)]
			void onmouseover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BE9 RID: 23529
			[DispId(-606)]
			void onmousemove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BEA RID: 23530
			[DispId(-605)]
			void onmousedown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BEB RID: 23531
			[DispId(-607)]
			void onmouseup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BEC RID: 23532
			[DispId(-2147418100)]
			bool onselectstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BED RID: 23533
			[DispId(-2147418095)]
			void onfilterchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BEE RID: 23534
			[DispId(-2147418101)]
			bool ondragstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BEF RID: 23535
			[DispId(-2147418108)]
			bool onbeforeupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BF0 RID: 23536
			[DispId(-2147418107)]
			void onafterupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BF1 RID: 23537
			[DispId(-2147418099)]
			bool onerrorupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BF2 RID: 23538
			[DispId(-2147418106)]
			bool onrowexit(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BF3 RID: 23539
			[DispId(-2147418105)]
			void onrowenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BF4 RID: 23540
			[DispId(-2147418098)]
			void ondatasetchanged(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BF5 RID: 23541
			[DispId(-2147418097)]
			void ondataavailable(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BF6 RID: 23542
			[DispId(-2147418096)]
			void ondatasetcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BF7 RID: 23543
			[DispId(-2147418094)]
			void onlosecapture(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BF8 RID: 23544
			[DispId(-2147418093)]
			void onpropertychange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BF9 RID: 23545
			[DispId(1014)]
			void onscroll(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BFA RID: 23546
			[DispId(-2147418111)]
			void onfocus(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BFB RID: 23547
			[DispId(-2147418112)]
			void onblur(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BFC RID: 23548
			[DispId(1016)]
			void onresize(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BFD RID: 23549
			[DispId(-2147418092)]
			bool ondrag(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BFE RID: 23550
			[DispId(-2147418091)]
			void ondragend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005BFF RID: 23551
			[DispId(-2147418090)]
			bool ondragenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C00 RID: 23552
			[DispId(-2147418089)]
			bool ondragover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C01 RID: 23553
			[DispId(-2147418088)]
			void ondragleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C02 RID: 23554
			[DispId(-2147418087)]
			bool ondrop(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C03 RID: 23555
			[DispId(-2147418083)]
			bool onbeforecut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C04 RID: 23556
			[DispId(-2147418086)]
			bool oncut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C05 RID: 23557
			[DispId(-2147418082)]
			bool onbeforecopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C06 RID: 23558
			[DispId(-2147418085)]
			bool oncopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C07 RID: 23559
			[DispId(-2147418081)]
			bool onbeforepaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C08 RID: 23560
			[DispId(-2147418084)]
			bool onpaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C09 RID: 23561
			[DispId(1023)]
			bool oncontextmenu(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C0A RID: 23562
			[DispId(-2147418080)]
			void onrowsdelete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C0B RID: 23563
			[DispId(-2147418079)]
			void onrowsinserted(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C0C RID: 23564
			[DispId(-2147418078)]
			void oncellchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C0D RID: 23565
			[DispId(-609)]
			void onreadystatechange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C0E RID: 23566
			[DispId(1030)]
			void onlayoutcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C0F RID: 23567
			[DispId(1031)]
			void onpage(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C10 RID: 23568
			[DispId(1042)]
			void onmouseenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C11 RID: 23569
			[DispId(1043)]
			void onmouseleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C12 RID: 23570
			[DispId(1044)]
			void onactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C13 RID: 23571
			[DispId(1045)]
			void ondeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C14 RID: 23572
			[DispId(1034)]
			bool onbeforedeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C15 RID: 23573
			[DispId(1047)]
			bool onbeforeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C16 RID: 23574
			[DispId(1048)]
			void onfocusin(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C17 RID: 23575
			[DispId(1049)]
			void onfocusout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C18 RID: 23576
			[DispId(1035)]
			void onmove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C19 RID: 23577
			[DispId(1036)]
			bool oncontrolselect(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C1A RID: 23578
			[DispId(1038)]
			bool onmovestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C1B RID: 23579
			[DispId(1039)]
			void onmoveend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C1C RID: 23580
			[DispId(1040)]
			bool onresizestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C1D RID: 23581
			[DispId(1041)]
			void onresizeend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C1E RID: 23582
			[DispId(1033)]
			bool onmousewheel(UnsafeNativeMethods.IHTMLEventObj evtObj);
		}

		// Token: 0x02000591 RID: 1425
		[Guid("3050f61d-98b5-11cf-bb82-00aa00bdce0b")]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		[TypeLibType(TypeLibTypeFlags.FHidden)]
		[ComImport]
		public interface DHTMLLinkElementEvents2
		{
			// Token: 0x06005C1F RID: 23583
			[DispId(-2147418102)]
			bool onhelp(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C20 RID: 23584
			[DispId(-600)]
			bool onclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C21 RID: 23585
			[DispId(-601)]
			bool ondblclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C22 RID: 23586
			[DispId(-603)]
			bool onkeypress(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C23 RID: 23587
			[DispId(-602)]
			void onkeydown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C24 RID: 23588
			[DispId(-604)]
			void onkeyup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C25 RID: 23589
			[DispId(-2147418103)]
			void onmouseout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C26 RID: 23590
			[DispId(-2147418104)]
			void onmouseover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C27 RID: 23591
			[DispId(-606)]
			void onmousemove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C28 RID: 23592
			[DispId(-605)]
			void onmousedown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C29 RID: 23593
			[DispId(-607)]
			void onmouseup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C2A RID: 23594
			[DispId(-2147418100)]
			bool onselectstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C2B RID: 23595
			[DispId(-2147418095)]
			void onfilterchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C2C RID: 23596
			[DispId(-2147418101)]
			bool ondragstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C2D RID: 23597
			[DispId(-2147418108)]
			bool onbeforeupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C2E RID: 23598
			[DispId(-2147418107)]
			void onafterupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C2F RID: 23599
			[DispId(-2147418099)]
			bool onerrorupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C30 RID: 23600
			[DispId(-2147418106)]
			bool onrowexit(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C31 RID: 23601
			[DispId(-2147418105)]
			void onrowenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C32 RID: 23602
			[DispId(-2147418098)]
			void ondatasetchanged(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C33 RID: 23603
			[DispId(-2147418097)]
			void ondataavailable(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C34 RID: 23604
			[DispId(-2147418096)]
			void ondatasetcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C35 RID: 23605
			[DispId(-2147418094)]
			void onlosecapture(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C36 RID: 23606
			[DispId(-2147418093)]
			void onpropertychange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C37 RID: 23607
			[DispId(1014)]
			void onscroll(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C38 RID: 23608
			[DispId(-2147418111)]
			void onfocus(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C39 RID: 23609
			[DispId(-2147418112)]
			void onblur(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C3A RID: 23610
			[DispId(1016)]
			void onresize(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C3B RID: 23611
			[DispId(-2147418092)]
			bool ondrag(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C3C RID: 23612
			[DispId(-2147418091)]
			void ondragend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C3D RID: 23613
			[DispId(-2147418090)]
			bool ondragenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C3E RID: 23614
			[DispId(-2147418089)]
			bool ondragover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C3F RID: 23615
			[DispId(-2147418088)]
			void ondragleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C40 RID: 23616
			[DispId(-2147418087)]
			bool ondrop(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C41 RID: 23617
			[DispId(-2147418083)]
			bool onbeforecut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C42 RID: 23618
			[DispId(-2147418086)]
			bool oncut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C43 RID: 23619
			[DispId(-2147418082)]
			bool onbeforecopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C44 RID: 23620
			[DispId(-2147418085)]
			bool oncopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C45 RID: 23621
			[DispId(-2147418081)]
			bool onbeforepaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C46 RID: 23622
			[DispId(-2147418084)]
			bool onpaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C47 RID: 23623
			[DispId(1023)]
			bool oncontextmenu(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C48 RID: 23624
			[DispId(-2147418080)]
			void onrowsdelete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C49 RID: 23625
			[DispId(-2147418079)]
			void onrowsinserted(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C4A RID: 23626
			[DispId(-2147418078)]
			void oncellchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C4B RID: 23627
			[DispId(-609)]
			void onreadystatechange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C4C RID: 23628
			[DispId(1030)]
			void onlayoutcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C4D RID: 23629
			[DispId(1031)]
			void onpage(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C4E RID: 23630
			[DispId(1042)]
			void onmouseenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C4F RID: 23631
			[DispId(1043)]
			void onmouseleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C50 RID: 23632
			[DispId(1044)]
			void onactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C51 RID: 23633
			[DispId(1045)]
			void ondeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C52 RID: 23634
			[DispId(1034)]
			bool onbeforedeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C53 RID: 23635
			[DispId(1047)]
			bool onbeforeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C54 RID: 23636
			[DispId(1048)]
			void onfocusin(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C55 RID: 23637
			[DispId(1049)]
			void onfocusout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C56 RID: 23638
			[DispId(1035)]
			void onmove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C57 RID: 23639
			[DispId(1036)]
			bool oncontrolselect(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C58 RID: 23640
			[DispId(1038)]
			bool onmovestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C59 RID: 23641
			[DispId(1039)]
			void onmoveend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C5A RID: 23642
			[DispId(1040)]
			bool onresizestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C5B RID: 23643
			[DispId(1041)]
			void onresizeend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C5C RID: 23644
			[DispId(1033)]
			bool onmousewheel(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C5D RID: 23645
			[DispId(-2147412080)]
			void onload(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C5E RID: 23646
			[DispId(-2147412083)]
			void onerror(UnsafeNativeMethods.IHTMLEventObj evtObj);
		}

		// Token: 0x02000592 RID: 1426
		[Guid("3050f61e-98b5-11cf-bb82-00aa00bdce0b")]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		[TypeLibType(TypeLibTypeFlags.FHidden)]
		[ComImport]
		public interface DHTMLMapEvents2
		{
			// Token: 0x06005C5F RID: 23647
			[DispId(-2147418102)]
			bool onhelp(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C60 RID: 23648
			[DispId(-600)]
			bool onclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C61 RID: 23649
			[DispId(-601)]
			bool ondblclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C62 RID: 23650
			[DispId(-603)]
			bool onkeypress(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C63 RID: 23651
			[DispId(-602)]
			void onkeydown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C64 RID: 23652
			[DispId(-604)]
			void onkeyup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C65 RID: 23653
			[DispId(-2147418103)]
			void onmouseout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C66 RID: 23654
			[DispId(-2147418104)]
			void onmouseover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C67 RID: 23655
			[DispId(-606)]
			void onmousemove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C68 RID: 23656
			[DispId(-605)]
			void onmousedown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C69 RID: 23657
			[DispId(-607)]
			void onmouseup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C6A RID: 23658
			[DispId(-2147418100)]
			bool onselectstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C6B RID: 23659
			[DispId(-2147418095)]
			void onfilterchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C6C RID: 23660
			[DispId(-2147418101)]
			bool ondragstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C6D RID: 23661
			[DispId(-2147418108)]
			bool onbeforeupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C6E RID: 23662
			[DispId(-2147418107)]
			void onafterupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C6F RID: 23663
			[DispId(-2147418099)]
			bool onerrorupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C70 RID: 23664
			[DispId(-2147418106)]
			bool onrowexit(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C71 RID: 23665
			[DispId(-2147418105)]
			void onrowenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C72 RID: 23666
			[DispId(-2147418098)]
			void ondatasetchanged(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C73 RID: 23667
			[DispId(-2147418097)]
			void ondataavailable(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C74 RID: 23668
			[DispId(-2147418096)]
			void ondatasetcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C75 RID: 23669
			[DispId(-2147418094)]
			void onlosecapture(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C76 RID: 23670
			[DispId(-2147418093)]
			void onpropertychange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C77 RID: 23671
			[DispId(1014)]
			void onscroll(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C78 RID: 23672
			[DispId(-2147418111)]
			void onfocus(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C79 RID: 23673
			[DispId(-2147418112)]
			void onblur(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C7A RID: 23674
			[DispId(1016)]
			void onresize(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C7B RID: 23675
			[DispId(-2147418092)]
			bool ondrag(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C7C RID: 23676
			[DispId(-2147418091)]
			void ondragend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C7D RID: 23677
			[DispId(-2147418090)]
			bool ondragenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C7E RID: 23678
			[DispId(-2147418089)]
			bool ondragover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C7F RID: 23679
			[DispId(-2147418088)]
			void ondragleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C80 RID: 23680
			[DispId(-2147418087)]
			bool ondrop(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C81 RID: 23681
			[DispId(-2147418083)]
			bool onbeforecut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C82 RID: 23682
			[DispId(-2147418086)]
			bool oncut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C83 RID: 23683
			[DispId(-2147418082)]
			bool onbeforecopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C84 RID: 23684
			[DispId(-2147418085)]
			bool oncopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C85 RID: 23685
			[DispId(-2147418081)]
			bool onbeforepaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C86 RID: 23686
			[DispId(-2147418084)]
			bool onpaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C87 RID: 23687
			[DispId(1023)]
			bool oncontextmenu(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C88 RID: 23688
			[DispId(-2147418080)]
			void onrowsdelete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C89 RID: 23689
			[DispId(-2147418079)]
			void onrowsinserted(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C8A RID: 23690
			[DispId(-2147418078)]
			void oncellchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C8B RID: 23691
			[DispId(-609)]
			void onreadystatechange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C8C RID: 23692
			[DispId(1030)]
			void onlayoutcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C8D RID: 23693
			[DispId(1031)]
			void onpage(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C8E RID: 23694
			[DispId(1042)]
			void onmouseenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C8F RID: 23695
			[DispId(1043)]
			void onmouseleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C90 RID: 23696
			[DispId(1044)]
			void onactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C91 RID: 23697
			[DispId(1045)]
			void ondeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C92 RID: 23698
			[DispId(1034)]
			bool onbeforedeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C93 RID: 23699
			[DispId(1047)]
			bool onbeforeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C94 RID: 23700
			[DispId(1048)]
			void onfocusin(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C95 RID: 23701
			[DispId(1049)]
			void onfocusout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C96 RID: 23702
			[DispId(1035)]
			void onmove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C97 RID: 23703
			[DispId(1036)]
			bool oncontrolselect(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C98 RID: 23704
			[DispId(1038)]
			bool onmovestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C99 RID: 23705
			[DispId(1039)]
			void onmoveend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C9A RID: 23706
			[DispId(1040)]
			bool onresizestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C9B RID: 23707
			[DispId(1041)]
			void onresizeend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C9C RID: 23708
			[DispId(1033)]
			bool onmousewheel(UnsafeNativeMethods.IHTMLEventObj evtObj);
		}

		// Token: 0x02000593 RID: 1427
		[Guid("3050f61f-98b5-11cf-bb82-00aa00bdce0b")]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		[TypeLibType(TypeLibTypeFlags.FHidden)]
		[ComImport]
		public interface DHTMLMarqueeElementEvents2
		{
			// Token: 0x06005C9D RID: 23709
			[DispId(-2147418102)]
			bool onhelp(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C9E RID: 23710
			[DispId(-600)]
			bool onclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005C9F RID: 23711
			[DispId(-601)]
			bool ondblclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CA0 RID: 23712
			[DispId(-603)]
			bool onkeypress(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CA1 RID: 23713
			[DispId(-602)]
			void onkeydown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CA2 RID: 23714
			[DispId(-604)]
			void onkeyup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CA3 RID: 23715
			[DispId(-2147418103)]
			void onmouseout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CA4 RID: 23716
			[DispId(-2147418104)]
			void onmouseover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CA5 RID: 23717
			[DispId(-606)]
			void onmousemove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CA6 RID: 23718
			[DispId(-605)]
			void onmousedown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CA7 RID: 23719
			[DispId(-607)]
			void onmouseup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CA8 RID: 23720
			[DispId(-2147418100)]
			bool onselectstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CA9 RID: 23721
			[DispId(-2147418095)]
			void onfilterchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CAA RID: 23722
			[DispId(-2147418101)]
			bool ondragstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CAB RID: 23723
			[DispId(-2147418108)]
			bool onbeforeupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CAC RID: 23724
			[DispId(-2147418107)]
			void onafterupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CAD RID: 23725
			[DispId(-2147418099)]
			bool onerrorupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CAE RID: 23726
			[DispId(-2147418106)]
			bool onrowexit(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CAF RID: 23727
			[DispId(-2147418105)]
			void onrowenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CB0 RID: 23728
			[DispId(-2147418098)]
			void ondatasetchanged(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CB1 RID: 23729
			[DispId(-2147418097)]
			void ondataavailable(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CB2 RID: 23730
			[DispId(-2147418096)]
			void ondatasetcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CB3 RID: 23731
			[DispId(-2147418094)]
			void onlosecapture(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CB4 RID: 23732
			[DispId(-2147418093)]
			void onpropertychange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CB5 RID: 23733
			[DispId(1014)]
			void onscroll(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CB6 RID: 23734
			[DispId(-2147418111)]
			void onfocus(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CB7 RID: 23735
			[DispId(-2147418112)]
			void onblur(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CB8 RID: 23736
			[DispId(1016)]
			void onresize(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CB9 RID: 23737
			[DispId(-2147418092)]
			bool ondrag(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CBA RID: 23738
			[DispId(-2147418091)]
			void ondragend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CBB RID: 23739
			[DispId(-2147418090)]
			bool ondragenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CBC RID: 23740
			[DispId(-2147418089)]
			bool ondragover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CBD RID: 23741
			[DispId(-2147418088)]
			void ondragleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CBE RID: 23742
			[DispId(-2147418087)]
			bool ondrop(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CBF RID: 23743
			[DispId(-2147418083)]
			bool onbeforecut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CC0 RID: 23744
			[DispId(-2147418086)]
			bool oncut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CC1 RID: 23745
			[DispId(-2147418082)]
			bool onbeforecopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CC2 RID: 23746
			[DispId(-2147418085)]
			bool oncopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CC3 RID: 23747
			[DispId(-2147418081)]
			bool onbeforepaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CC4 RID: 23748
			[DispId(-2147418084)]
			bool onpaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CC5 RID: 23749
			[DispId(1023)]
			bool oncontextmenu(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CC6 RID: 23750
			[DispId(-2147418080)]
			void onrowsdelete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CC7 RID: 23751
			[DispId(-2147418079)]
			void onrowsinserted(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CC8 RID: 23752
			[DispId(-2147418078)]
			void oncellchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CC9 RID: 23753
			[DispId(-609)]
			void onreadystatechange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CCA RID: 23754
			[DispId(1030)]
			void onlayoutcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CCB RID: 23755
			[DispId(1031)]
			void onpage(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CCC RID: 23756
			[DispId(1042)]
			void onmouseenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CCD RID: 23757
			[DispId(1043)]
			void onmouseleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CCE RID: 23758
			[DispId(1044)]
			void onactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CCF RID: 23759
			[DispId(1045)]
			void ondeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CD0 RID: 23760
			[DispId(1034)]
			bool onbeforedeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CD1 RID: 23761
			[DispId(1047)]
			bool onbeforeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CD2 RID: 23762
			[DispId(1048)]
			void onfocusin(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CD3 RID: 23763
			[DispId(1049)]
			void onfocusout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CD4 RID: 23764
			[DispId(1035)]
			void onmove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CD5 RID: 23765
			[DispId(1036)]
			bool oncontrolselect(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CD6 RID: 23766
			[DispId(1038)]
			bool onmovestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CD7 RID: 23767
			[DispId(1039)]
			void onmoveend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CD8 RID: 23768
			[DispId(1040)]
			bool onresizestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CD9 RID: 23769
			[DispId(1041)]
			void onresizeend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CDA RID: 23770
			[DispId(1033)]
			bool onmousewheel(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CDB RID: 23771
			[DispId(-2147412092)]
			void onbounce(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CDC RID: 23772
			[DispId(-2147412086)]
			void onfinish(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CDD RID: 23773
			[DispId(-2147412085)]
			void onstart(UnsafeNativeMethods.IHTMLEventObj evtObj);
		}

		// Token: 0x02000594 RID: 1428
		[Guid("3050f619-98b5-11cf-bb82-00aa00bdce0b")]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		[TypeLibType(TypeLibTypeFlags.FHidden)]
		[ComImport]
		public interface DHTMLOptionButtonElementEvents2
		{
			// Token: 0x06005CDE RID: 23774
			[DispId(-2147418102)]
			bool onhelp(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CDF RID: 23775
			[DispId(-600)]
			bool onclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CE0 RID: 23776
			[DispId(-601)]
			bool ondblclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CE1 RID: 23777
			[DispId(-603)]
			bool onkeypress(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CE2 RID: 23778
			[DispId(-602)]
			void onkeydown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CE3 RID: 23779
			[DispId(-604)]
			void onkeyup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CE4 RID: 23780
			[DispId(-2147418103)]
			void onmouseout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CE5 RID: 23781
			[DispId(-2147418104)]
			void onmouseover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CE6 RID: 23782
			[DispId(-606)]
			void onmousemove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CE7 RID: 23783
			[DispId(-605)]
			void onmousedown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CE8 RID: 23784
			[DispId(-607)]
			void onmouseup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CE9 RID: 23785
			[DispId(-2147418100)]
			bool onselectstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CEA RID: 23786
			[DispId(-2147418095)]
			void onfilterchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CEB RID: 23787
			[DispId(-2147418101)]
			bool ondragstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CEC RID: 23788
			[DispId(-2147418108)]
			bool onbeforeupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CED RID: 23789
			[DispId(-2147418107)]
			void onafterupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CEE RID: 23790
			[DispId(-2147418099)]
			bool onerrorupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CEF RID: 23791
			[DispId(-2147418106)]
			bool onrowexit(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CF0 RID: 23792
			[DispId(-2147418105)]
			void onrowenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CF1 RID: 23793
			[DispId(-2147418098)]
			void ondatasetchanged(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CF2 RID: 23794
			[DispId(-2147418097)]
			void ondataavailable(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CF3 RID: 23795
			[DispId(-2147418096)]
			void ondatasetcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CF4 RID: 23796
			[DispId(-2147418094)]
			void onlosecapture(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CF5 RID: 23797
			[DispId(-2147418093)]
			void onpropertychange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CF6 RID: 23798
			[DispId(1014)]
			void onscroll(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CF7 RID: 23799
			[DispId(-2147418111)]
			void onfocus(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CF8 RID: 23800
			[DispId(-2147418112)]
			void onblur(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CF9 RID: 23801
			[DispId(1016)]
			void onresize(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CFA RID: 23802
			[DispId(-2147418092)]
			bool ondrag(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CFB RID: 23803
			[DispId(-2147418091)]
			void ondragend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CFC RID: 23804
			[DispId(-2147418090)]
			bool ondragenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CFD RID: 23805
			[DispId(-2147418089)]
			bool ondragover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CFE RID: 23806
			[DispId(-2147418088)]
			void ondragleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005CFF RID: 23807
			[DispId(-2147418087)]
			bool ondrop(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D00 RID: 23808
			[DispId(-2147418083)]
			bool onbeforecut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D01 RID: 23809
			[DispId(-2147418086)]
			bool oncut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D02 RID: 23810
			[DispId(-2147418082)]
			bool onbeforecopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D03 RID: 23811
			[DispId(-2147418085)]
			bool oncopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D04 RID: 23812
			[DispId(-2147418081)]
			bool onbeforepaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D05 RID: 23813
			[DispId(-2147418084)]
			bool onpaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D06 RID: 23814
			[DispId(1023)]
			bool oncontextmenu(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D07 RID: 23815
			[DispId(-2147418080)]
			void onrowsdelete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D08 RID: 23816
			[DispId(-2147418079)]
			void onrowsinserted(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D09 RID: 23817
			[DispId(-2147418078)]
			void oncellchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D0A RID: 23818
			[DispId(-609)]
			void onreadystatechange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D0B RID: 23819
			[DispId(1030)]
			void onlayoutcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D0C RID: 23820
			[DispId(1031)]
			void onpage(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D0D RID: 23821
			[DispId(1042)]
			void onmouseenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D0E RID: 23822
			[DispId(1043)]
			void onmouseleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D0F RID: 23823
			[DispId(1044)]
			void onactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D10 RID: 23824
			[DispId(1045)]
			void ondeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D11 RID: 23825
			[DispId(1034)]
			bool onbeforedeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D12 RID: 23826
			[DispId(1047)]
			bool onbeforeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D13 RID: 23827
			[DispId(1048)]
			void onfocusin(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D14 RID: 23828
			[DispId(1049)]
			void onfocusout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D15 RID: 23829
			[DispId(1035)]
			void onmove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D16 RID: 23830
			[DispId(1036)]
			bool oncontrolselect(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D17 RID: 23831
			[DispId(1038)]
			bool onmovestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D18 RID: 23832
			[DispId(1039)]
			void onmoveend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D19 RID: 23833
			[DispId(1040)]
			bool onresizestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D1A RID: 23834
			[DispId(1041)]
			void onresizeend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D1B RID: 23835
			[DispId(1033)]
			bool onmousewheel(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D1C RID: 23836
			[DispId(-2147412082)]
			bool onchange(UnsafeNativeMethods.IHTMLEventObj evtObj);
		}

		// Token: 0x02000595 RID: 1429
		[Guid("3050f622-98b5-11cf-bb82-00aa00bdce0b")]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		[TypeLibType(TypeLibTypeFlags.FHidden)]
		[ComImport]
		public interface DHTMLSelectElementEvents2
		{
			// Token: 0x06005D1D RID: 23837
			[DispId(-2147418102)]
			bool onhelp(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D1E RID: 23838
			[DispId(-600)]
			bool onclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D1F RID: 23839
			[DispId(-601)]
			bool ondblclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D20 RID: 23840
			[DispId(-603)]
			bool onkeypress(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D21 RID: 23841
			[DispId(-602)]
			void onkeydown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D22 RID: 23842
			[DispId(-604)]
			void onkeyup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D23 RID: 23843
			[DispId(-2147418103)]
			void onmouseout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D24 RID: 23844
			[DispId(-2147418104)]
			void onmouseover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D25 RID: 23845
			[DispId(-606)]
			void onmousemove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D26 RID: 23846
			[DispId(-605)]
			void onmousedown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D27 RID: 23847
			[DispId(-607)]
			void onmouseup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D28 RID: 23848
			[DispId(-2147418100)]
			bool onselectstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D29 RID: 23849
			[DispId(-2147418095)]
			void onfilterchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D2A RID: 23850
			[DispId(-2147418101)]
			bool ondragstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D2B RID: 23851
			[DispId(-2147418108)]
			bool onbeforeupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D2C RID: 23852
			[DispId(-2147418107)]
			void onafterupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D2D RID: 23853
			[DispId(-2147418099)]
			bool onerrorupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D2E RID: 23854
			[DispId(-2147418106)]
			bool onrowexit(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D2F RID: 23855
			[DispId(-2147418105)]
			void onrowenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D30 RID: 23856
			[DispId(-2147418098)]
			void ondatasetchanged(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D31 RID: 23857
			[DispId(-2147418097)]
			void ondataavailable(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D32 RID: 23858
			[DispId(-2147418096)]
			void ondatasetcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D33 RID: 23859
			[DispId(-2147418094)]
			void onlosecapture(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D34 RID: 23860
			[DispId(-2147418093)]
			void onpropertychange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D35 RID: 23861
			[DispId(1014)]
			void onscroll(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D36 RID: 23862
			[DispId(-2147418111)]
			void onfocus(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D37 RID: 23863
			[DispId(-2147418112)]
			void onblur(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D38 RID: 23864
			[DispId(1016)]
			void onresize(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D39 RID: 23865
			[DispId(-2147418092)]
			bool ondrag(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D3A RID: 23866
			[DispId(-2147418091)]
			void ondragend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D3B RID: 23867
			[DispId(-2147418090)]
			bool ondragenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D3C RID: 23868
			[DispId(-2147418089)]
			bool ondragover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D3D RID: 23869
			[DispId(-2147418088)]
			void ondragleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D3E RID: 23870
			[DispId(-2147418087)]
			bool ondrop(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D3F RID: 23871
			[DispId(-2147418083)]
			bool onbeforecut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D40 RID: 23872
			[DispId(-2147418086)]
			bool oncut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D41 RID: 23873
			[DispId(-2147418082)]
			bool onbeforecopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D42 RID: 23874
			[DispId(-2147418085)]
			bool oncopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D43 RID: 23875
			[DispId(-2147418081)]
			bool onbeforepaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D44 RID: 23876
			[DispId(-2147418084)]
			bool onpaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D45 RID: 23877
			[DispId(1023)]
			bool oncontextmenu(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D46 RID: 23878
			[DispId(-2147418080)]
			void onrowsdelete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D47 RID: 23879
			[DispId(-2147418079)]
			void onrowsinserted(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D48 RID: 23880
			[DispId(-2147418078)]
			void oncellchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D49 RID: 23881
			[DispId(-609)]
			void onreadystatechange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D4A RID: 23882
			[DispId(1030)]
			void onlayoutcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D4B RID: 23883
			[DispId(1031)]
			void onpage(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D4C RID: 23884
			[DispId(1042)]
			void onmouseenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D4D RID: 23885
			[DispId(1043)]
			void onmouseleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D4E RID: 23886
			[DispId(1044)]
			void onactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D4F RID: 23887
			[DispId(1045)]
			void ondeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D50 RID: 23888
			[DispId(1034)]
			bool onbeforedeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D51 RID: 23889
			[DispId(1047)]
			bool onbeforeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D52 RID: 23890
			[DispId(1048)]
			void onfocusin(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D53 RID: 23891
			[DispId(1049)]
			void onfocusout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D54 RID: 23892
			[DispId(1035)]
			void onmove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D55 RID: 23893
			[DispId(1036)]
			bool oncontrolselect(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D56 RID: 23894
			[DispId(1038)]
			bool onmovestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D57 RID: 23895
			[DispId(1039)]
			void onmoveend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D58 RID: 23896
			[DispId(1040)]
			bool onresizestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D59 RID: 23897
			[DispId(1041)]
			void onresizeend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D5A RID: 23898
			[DispId(1033)]
			bool onmousewheel(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D5B RID: 23899
			[DispId(-2147412082)]
			void onchange_void(UnsafeNativeMethods.IHTMLEventObj evtObj);
		}

		// Token: 0x02000596 RID: 1430
		[Guid("3050f615-98b5-11cf-bb82-00aa00bdce0b")]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		[TypeLibType(TypeLibTypeFlags.FHidden)]
		[ComImport]
		public interface DHTMLStyleElementEvents2
		{
			// Token: 0x06005D5C RID: 23900
			[DispId(-2147418102)]
			bool onhelp(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D5D RID: 23901
			[DispId(-600)]
			bool onclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D5E RID: 23902
			[DispId(-601)]
			bool ondblclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D5F RID: 23903
			[DispId(-603)]
			bool onkeypress(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D60 RID: 23904
			[DispId(-602)]
			void onkeydown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D61 RID: 23905
			[DispId(-604)]
			void onkeyup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D62 RID: 23906
			[DispId(-2147418103)]
			void onmouseout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D63 RID: 23907
			[DispId(-2147418104)]
			void onmouseover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D64 RID: 23908
			[DispId(-606)]
			void onmousemove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D65 RID: 23909
			[DispId(-605)]
			void onmousedown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D66 RID: 23910
			[DispId(-607)]
			void onmouseup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D67 RID: 23911
			[DispId(-2147418100)]
			bool onselectstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D68 RID: 23912
			[DispId(-2147418095)]
			void onfilterchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D69 RID: 23913
			[DispId(-2147418101)]
			bool ondragstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D6A RID: 23914
			[DispId(-2147418108)]
			bool onbeforeupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D6B RID: 23915
			[DispId(-2147418107)]
			void onafterupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D6C RID: 23916
			[DispId(-2147418099)]
			bool onerrorupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D6D RID: 23917
			[DispId(-2147418106)]
			bool onrowexit(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D6E RID: 23918
			[DispId(-2147418105)]
			void onrowenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D6F RID: 23919
			[DispId(-2147418098)]
			void ondatasetchanged(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D70 RID: 23920
			[DispId(-2147418097)]
			void ondataavailable(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D71 RID: 23921
			[DispId(-2147418096)]
			void ondatasetcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D72 RID: 23922
			[DispId(-2147418094)]
			void onlosecapture(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D73 RID: 23923
			[DispId(-2147418093)]
			void onpropertychange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D74 RID: 23924
			[DispId(1014)]
			void onscroll(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D75 RID: 23925
			[DispId(-2147418111)]
			void onfocus(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D76 RID: 23926
			[DispId(-2147418112)]
			void onblur(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D77 RID: 23927
			[DispId(1016)]
			void onresize(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D78 RID: 23928
			[DispId(-2147418092)]
			bool ondrag(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D79 RID: 23929
			[DispId(-2147418091)]
			void ondragend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D7A RID: 23930
			[DispId(-2147418090)]
			bool ondragenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D7B RID: 23931
			[DispId(-2147418089)]
			bool ondragover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D7C RID: 23932
			[DispId(-2147418088)]
			void ondragleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D7D RID: 23933
			[DispId(-2147418087)]
			bool ondrop(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D7E RID: 23934
			[DispId(-2147418083)]
			bool onbeforecut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D7F RID: 23935
			[DispId(-2147418086)]
			bool oncut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D80 RID: 23936
			[DispId(-2147418082)]
			bool onbeforecopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D81 RID: 23937
			[DispId(-2147418085)]
			bool oncopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D82 RID: 23938
			[DispId(-2147418081)]
			bool onbeforepaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D83 RID: 23939
			[DispId(-2147418084)]
			bool onpaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D84 RID: 23940
			[DispId(1023)]
			bool oncontextmenu(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D85 RID: 23941
			[DispId(-2147418080)]
			void onrowsdelete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D86 RID: 23942
			[DispId(-2147418079)]
			void onrowsinserted(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D87 RID: 23943
			[DispId(-2147418078)]
			void oncellchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D88 RID: 23944
			[DispId(-609)]
			void onreadystatechange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D89 RID: 23945
			[DispId(1030)]
			void onlayoutcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D8A RID: 23946
			[DispId(1031)]
			void onpage(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D8B RID: 23947
			[DispId(1042)]
			void onmouseenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D8C RID: 23948
			[DispId(1043)]
			void onmouseleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D8D RID: 23949
			[DispId(1044)]
			void onactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D8E RID: 23950
			[DispId(1045)]
			void ondeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D8F RID: 23951
			[DispId(1034)]
			bool onbeforedeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D90 RID: 23952
			[DispId(1047)]
			bool onbeforeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D91 RID: 23953
			[DispId(1048)]
			void onfocusin(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D92 RID: 23954
			[DispId(1049)]
			void onfocusout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D93 RID: 23955
			[DispId(1035)]
			void onmove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D94 RID: 23956
			[DispId(1036)]
			bool oncontrolselect(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D95 RID: 23957
			[DispId(1038)]
			bool onmovestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D96 RID: 23958
			[DispId(1039)]
			void onmoveend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D97 RID: 23959
			[DispId(1040)]
			bool onresizestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D98 RID: 23960
			[DispId(1041)]
			void onresizeend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D99 RID: 23961
			[DispId(1033)]
			bool onmousewheel(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D9A RID: 23962
			[DispId(1003)]
			void onload(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D9B RID: 23963
			[DispId(1002)]
			void onerror(UnsafeNativeMethods.IHTMLEventObj evtObj);
		}

		// Token: 0x02000597 RID: 1431
		[Guid("3050f623-98b5-11cf-bb82-00aa00bdce0b")]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		[TypeLibType(TypeLibTypeFlags.FHidden)]
		[ComImport]
		public interface DHTMLTableEvents2
		{
			// Token: 0x06005D9C RID: 23964
			[DispId(-2147418102)]
			bool onhelp(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D9D RID: 23965
			[DispId(-600)]
			bool onclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D9E RID: 23966
			[DispId(-601)]
			bool ondblclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005D9F RID: 23967
			[DispId(-603)]
			bool onkeypress(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DA0 RID: 23968
			[DispId(-602)]
			void onkeydown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DA1 RID: 23969
			[DispId(-604)]
			void onkeyup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DA2 RID: 23970
			[DispId(-2147418103)]
			void onmouseout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DA3 RID: 23971
			[DispId(-2147418104)]
			void onmouseover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DA4 RID: 23972
			[DispId(-606)]
			void onmousemove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DA5 RID: 23973
			[DispId(-605)]
			void onmousedown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DA6 RID: 23974
			[DispId(-607)]
			void onmouseup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DA7 RID: 23975
			[DispId(-2147418100)]
			bool onselectstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DA8 RID: 23976
			[DispId(-2147418095)]
			void onfilterchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DA9 RID: 23977
			[DispId(-2147418101)]
			bool ondragstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DAA RID: 23978
			[DispId(-2147418108)]
			bool onbeforeupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DAB RID: 23979
			[DispId(-2147418107)]
			void onafterupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DAC RID: 23980
			[DispId(-2147418099)]
			bool onerrorupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DAD RID: 23981
			[DispId(-2147418106)]
			bool onrowexit(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DAE RID: 23982
			[DispId(-2147418105)]
			void onrowenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DAF RID: 23983
			[DispId(-2147418098)]
			void ondatasetchanged(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DB0 RID: 23984
			[DispId(-2147418097)]
			void ondataavailable(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DB1 RID: 23985
			[DispId(-2147418096)]
			void ondatasetcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DB2 RID: 23986
			[DispId(-2147418094)]
			void onlosecapture(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DB3 RID: 23987
			[DispId(-2147418093)]
			void onpropertychange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DB4 RID: 23988
			[DispId(1014)]
			void onscroll(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DB5 RID: 23989
			[DispId(-2147418111)]
			void onfocus(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DB6 RID: 23990
			[DispId(-2147418112)]
			void onblur(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DB7 RID: 23991
			[DispId(1016)]
			void onresize(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DB8 RID: 23992
			[DispId(-2147418092)]
			bool ondrag(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DB9 RID: 23993
			[DispId(-2147418091)]
			void ondragend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DBA RID: 23994
			[DispId(-2147418090)]
			bool ondragenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DBB RID: 23995
			[DispId(-2147418089)]
			bool ondragover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DBC RID: 23996
			[DispId(-2147418088)]
			void ondragleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DBD RID: 23997
			[DispId(-2147418087)]
			bool ondrop(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DBE RID: 23998
			[DispId(-2147418083)]
			bool onbeforecut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DBF RID: 23999
			[DispId(-2147418086)]
			bool oncut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DC0 RID: 24000
			[DispId(-2147418082)]
			bool onbeforecopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DC1 RID: 24001
			[DispId(-2147418085)]
			bool oncopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DC2 RID: 24002
			[DispId(-2147418081)]
			bool onbeforepaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DC3 RID: 24003
			[DispId(-2147418084)]
			bool onpaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DC4 RID: 24004
			[DispId(1023)]
			bool oncontextmenu(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DC5 RID: 24005
			[DispId(-2147418080)]
			void onrowsdelete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DC6 RID: 24006
			[DispId(-2147418079)]
			void onrowsinserted(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DC7 RID: 24007
			[DispId(-2147418078)]
			void oncellchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DC8 RID: 24008
			[DispId(-609)]
			void onreadystatechange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DC9 RID: 24009
			[DispId(1030)]
			void onlayoutcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DCA RID: 24010
			[DispId(1031)]
			void onpage(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DCB RID: 24011
			[DispId(1042)]
			void onmouseenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DCC RID: 24012
			[DispId(1043)]
			void onmouseleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DCD RID: 24013
			[DispId(1044)]
			void onactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DCE RID: 24014
			[DispId(1045)]
			void ondeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DCF RID: 24015
			[DispId(1034)]
			bool onbeforedeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DD0 RID: 24016
			[DispId(1047)]
			bool onbeforeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DD1 RID: 24017
			[DispId(1048)]
			void onfocusin(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DD2 RID: 24018
			[DispId(1049)]
			void onfocusout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DD3 RID: 24019
			[DispId(1035)]
			void onmove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DD4 RID: 24020
			[DispId(1036)]
			bool oncontrolselect(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DD5 RID: 24021
			[DispId(1038)]
			bool onmovestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DD6 RID: 24022
			[DispId(1039)]
			void onmoveend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DD7 RID: 24023
			[DispId(1040)]
			bool onresizestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DD8 RID: 24024
			[DispId(1041)]
			void onresizeend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DD9 RID: 24025
			[DispId(1033)]
			bool onmousewheel(UnsafeNativeMethods.IHTMLEventObj evtObj);
		}

		// Token: 0x02000598 RID: 1432
		[Guid("3050f624-98b5-11cf-bb82-00aa00bdce0b")]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		[TypeLibType(TypeLibTypeFlags.FHidden)]
		[ComImport]
		public interface DHTMLTextContainerEvents2
		{
			// Token: 0x06005DDA RID: 24026
			[DispId(-2147418102)]
			bool onhelp(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DDB RID: 24027
			[DispId(-600)]
			bool onclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DDC RID: 24028
			[DispId(-601)]
			bool ondblclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DDD RID: 24029
			[DispId(-603)]
			bool onkeypress(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DDE RID: 24030
			[DispId(-602)]
			void onkeydown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DDF RID: 24031
			[DispId(-604)]
			void onkeyup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DE0 RID: 24032
			[DispId(-2147418103)]
			void onmouseout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DE1 RID: 24033
			[DispId(-2147418104)]
			void onmouseover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DE2 RID: 24034
			[DispId(-606)]
			void onmousemove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DE3 RID: 24035
			[DispId(-605)]
			void onmousedown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DE4 RID: 24036
			[DispId(-607)]
			void onmouseup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DE5 RID: 24037
			[DispId(-2147418100)]
			bool onselectstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DE6 RID: 24038
			[DispId(-2147418095)]
			void onfilterchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DE7 RID: 24039
			[DispId(-2147418101)]
			bool ondragstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DE8 RID: 24040
			[DispId(-2147418108)]
			bool onbeforeupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DE9 RID: 24041
			[DispId(-2147418107)]
			void onafterupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DEA RID: 24042
			[DispId(-2147418099)]
			bool onerrorupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DEB RID: 24043
			[DispId(-2147418106)]
			bool onrowexit(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DEC RID: 24044
			[DispId(-2147418105)]
			void onrowenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DED RID: 24045
			[DispId(-2147418098)]
			void ondatasetchanged(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DEE RID: 24046
			[DispId(-2147418097)]
			void ondataavailable(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DEF RID: 24047
			[DispId(-2147418096)]
			void ondatasetcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DF0 RID: 24048
			[DispId(-2147418094)]
			void onlosecapture(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DF1 RID: 24049
			[DispId(-2147418093)]
			void onpropertychange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DF2 RID: 24050
			[DispId(1014)]
			void onscroll(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DF3 RID: 24051
			[DispId(-2147418111)]
			void onfocus(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DF4 RID: 24052
			[DispId(-2147418112)]
			void onblur(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DF5 RID: 24053
			[DispId(1016)]
			void onresize(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DF6 RID: 24054
			[DispId(-2147418092)]
			bool ondrag(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DF7 RID: 24055
			[DispId(-2147418091)]
			void ondragend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DF8 RID: 24056
			[DispId(-2147418090)]
			bool ondragenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DF9 RID: 24057
			[DispId(-2147418089)]
			bool ondragover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DFA RID: 24058
			[DispId(-2147418088)]
			void ondragleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DFB RID: 24059
			[DispId(-2147418087)]
			bool ondrop(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DFC RID: 24060
			[DispId(-2147418083)]
			bool onbeforecut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DFD RID: 24061
			[DispId(-2147418086)]
			bool oncut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DFE RID: 24062
			[DispId(-2147418082)]
			bool onbeforecopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005DFF RID: 24063
			[DispId(-2147418085)]
			bool oncopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E00 RID: 24064
			[DispId(-2147418081)]
			bool onbeforepaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E01 RID: 24065
			[DispId(-2147418084)]
			bool onpaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E02 RID: 24066
			[DispId(1023)]
			bool oncontextmenu(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E03 RID: 24067
			[DispId(-2147418080)]
			void onrowsdelete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E04 RID: 24068
			[DispId(-2147418079)]
			void onrowsinserted(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E05 RID: 24069
			[DispId(-2147418078)]
			void oncellchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E06 RID: 24070
			[DispId(-609)]
			void onreadystatechange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E07 RID: 24071
			[DispId(1030)]
			void onlayoutcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E08 RID: 24072
			[DispId(1031)]
			void onpage(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E09 RID: 24073
			[DispId(1042)]
			void onmouseenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E0A RID: 24074
			[DispId(1043)]
			void onmouseleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E0B RID: 24075
			[DispId(1044)]
			void onactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E0C RID: 24076
			[DispId(1045)]
			void ondeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E0D RID: 24077
			[DispId(1034)]
			bool onbeforedeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E0E RID: 24078
			[DispId(1047)]
			bool onbeforeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E0F RID: 24079
			[DispId(1048)]
			void onfocusin(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E10 RID: 24080
			[DispId(1049)]
			void onfocusout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E11 RID: 24081
			[DispId(1035)]
			void onmove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E12 RID: 24082
			[DispId(1036)]
			bool oncontrolselect(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E13 RID: 24083
			[DispId(1038)]
			bool onmovestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E14 RID: 24084
			[DispId(1039)]
			void onmoveend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E15 RID: 24085
			[DispId(1040)]
			bool onresizestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E16 RID: 24086
			[DispId(1041)]
			void onresizeend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E17 RID: 24087
			[DispId(1033)]
			bool onmousewheel(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E18 RID: 24088
			[DispId(1001)]
			void onchange_void(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E19 RID: 24089
			[DispId(1006)]
			void onselect(UnsafeNativeMethods.IHTMLEventObj evtObj);
		}

		// Token: 0x02000599 RID: 1433
		[Guid("3050f621-98b5-11cf-bb82-00aa00bdce0b")]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		[TypeLibType(TypeLibTypeFlags.FHidden)]
		[ComImport]
		public interface DHTMLScriptEvents2
		{
			// Token: 0x06005E1A RID: 24090
			[DispId(-2147418102)]
			bool onhelp(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E1B RID: 24091
			[DispId(-600)]
			bool onclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E1C RID: 24092
			[DispId(-601)]
			bool ondblclick(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E1D RID: 24093
			[DispId(-603)]
			bool onkeypress(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E1E RID: 24094
			[DispId(-602)]
			void onkeydown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E1F RID: 24095
			[DispId(-604)]
			void onkeyup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E20 RID: 24096
			[DispId(-2147418103)]
			void onmouseout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E21 RID: 24097
			[DispId(-2147418104)]
			void onmouseover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E22 RID: 24098
			[DispId(-606)]
			void onmousemove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E23 RID: 24099
			[DispId(-605)]
			void onmousedown(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E24 RID: 24100
			[DispId(-607)]
			void onmouseup(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E25 RID: 24101
			[DispId(-2147418100)]
			bool onselectstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E26 RID: 24102
			[DispId(-2147418095)]
			void onfilterchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E27 RID: 24103
			[DispId(-2147418101)]
			bool ondragstart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E28 RID: 24104
			[DispId(-2147418108)]
			bool onbeforeupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E29 RID: 24105
			[DispId(-2147418107)]
			void onafterupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E2A RID: 24106
			[DispId(-2147418099)]
			bool onerrorupdate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E2B RID: 24107
			[DispId(-2147418106)]
			bool onrowexit(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E2C RID: 24108
			[DispId(-2147418105)]
			void onrowenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E2D RID: 24109
			[DispId(-2147418098)]
			void ondatasetchanged(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E2E RID: 24110
			[DispId(-2147418097)]
			void ondataavailable(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E2F RID: 24111
			[DispId(-2147418096)]
			void ondatasetcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E30 RID: 24112
			[DispId(-2147418094)]
			void onlosecapture(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E31 RID: 24113
			[DispId(-2147418093)]
			void onpropertychange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E32 RID: 24114
			[DispId(1014)]
			void onscroll(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E33 RID: 24115
			[DispId(-2147418111)]
			void onfocus(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E34 RID: 24116
			[DispId(-2147418112)]
			void onblur(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E35 RID: 24117
			[DispId(1016)]
			void onresize(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E36 RID: 24118
			[DispId(-2147418092)]
			bool ondrag(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E37 RID: 24119
			[DispId(-2147418091)]
			void ondragend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E38 RID: 24120
			[DispId(-2147418090)]
			bool ondragenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E39 RID: 24121
			[DispId(-2147418089)]
			bool ondragover(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E3A RID: 24122
			[DispId(-2147418088)]
			void ondragleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E3B RID: 24123
			[DispId(-2147418087)]
			bool ondrop(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E3C RID: 24124
			[DispId(-2147418083)]
			bool onbeforecut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E3D RID: 24125
			[DispId(-2147418086)]
			bool oncut(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E3E RID: 24126
			[DispId(-2147418082)]
			bool onbeforecopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E3F RID: 24127
			[DispId(-2147418085)]
			bool oncopy(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E40 RID: 24128
			[DispId(-2147418081)]
			bool onbeforepaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E41 RID: 24129
			[DispId(-2147418084)]
			bool onpaste(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E42 RID: 24130
			[DispId(1023)]
			bool oncontextmenu(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E43 RID: 24131
			[DispId(-2147418080)]
			void onrowsdelete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E44 RID: 24132
			[DispId(-2147418079)]
			void onrowsinserted(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E45 RID: 24133
			[DispId(-2147418078)]
			void oncellchange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E46 RID: 24134
			[DispId(-609)]
			void onreadystatechange(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E47 RID: 24135
			[DispId(1030)]
			void onlayoutcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E48 RID: 24136
			[DispId(1031)]
			void onpage(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E49 RID: 24137
			[DispId(1042)]
			void onmouseenter(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E4A RID: 24138
			[DispId(1043)]
			void onmouseleave(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E4B RID: 24139
			[DispId(1044)]
			void onactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E4C RID: 24140
			[DispId(1045)]
			void ondeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E4D RID: 24141
			[DispId(1034)]
			bool onbeforedeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E4E RID: 24142
			[DispId(1047)]
			bool onbeforeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E4F RID: 24143
			[DispId(1048)]
			void onfocusin(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E50 RID: 24144
			[DispId(1049)]
			void onfocusout(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E51 RID: 24145
			[DispId(1035)]
			void onmove(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E52 RID: 24146
			[DispId(1036)]
			bool oncontrolselect(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E53 RID: 24147
			[DispId(1038)]
			bool onmovestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E54 RID: 24148
			[DispId(1039)]
			void onmoveend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E55 RID: 24149
			[DispId(1040)]
			bool onresizestart(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E56 RID: 24150
			[DispId(1041)]
			void onresizeend(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E57 RID: 24151
			[DispId(1033)]
			bool onmousewheel(UnsafeNativeMethods.IHTMLEventObj evtObj);

			// Token: 0x06005E58 RID: 24152
			[DispId(1002)]
			void onerror(UnsafeNativeMethods.IHTMLEventObj evtObj);
		}

		// Token: 0x0200059A RID: 1434
		[SuppressUnmanagedCodeSecurity]
		[ComVisible(true)]
		[Guid("3050F25E-98B5-11CF-BB82-00AA00BDCE0B")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		internal interface IHTMLStyle
		{
			// Token: 0x06005E59 RID: 24153
			void SetFontFamily(string p);

			// Token: 0x06005E5A RID: 24154
			string GetFontFamily();

			// Token: 0x06005E5B RID: 24155
			void SetFontStyle(string p);

			// Token: 0x06005E5C RID: 24156
			string GetFontStyle();

			// Token: 0x06005E5D RID: 24157
			void SetFontObject(string p);

			// Token: 0x06005E5E RID: 24158
			string GetFontObject();

			// Token: 0x06005E5F RID: 24159
			void SetFontWeight(string p);

			// Token: 0x06005E60 RID: 24160
			string GetFontWeight();

			// Token: 0x06005E61 RID: 24161
			void SetFontSize(object p);

			// Token: 0x06005E62 RID: 24162
			object GetFontSize();

			// Token: 0x06005E63 RID: 24163
			void SetFont(string p);

			// Token: 0x06005E64 RID: 24164
			string GetFont();

			// Token: 0x06005E65 RID: 24165
			void SetColor(object p);

			// Token: 0x06005E66 RID: 24166
			object GetColor();

			// Token: 0x06005E67 RID: 24167
			void SetBackground(string p);

			// Token: 0x06005E68 RID: 24168
			string GetBackground();

			// Token: 0x06005E69 RID: 24169
			void SetBackgroundColor(object p);

			// Token: 0x06005E6A RID: 24170
			object GetBackgroundColor();

			// Token: 0x06005E6B RID: 24171
			void SetBackgroundImage(string p);

			// Token: 0x06005E6C RID: 24172
			string GetBackgroundImage();

			// Token: 0x06005E6D RID: 24173
			void SetBackgroundRepeat(string p);

			// Token: 0x06005E6E RID: 24174
			string GetBackgroundRepeat();

			// Token: 0x06005E6F RID: 24175
			void SetBackgroundAttachment(string p);

			// Token: 0x06005E70 RID: 24176
			string GetBackgroundAttachment();

			// Token: 0x06005E71 RID: 24177
			void SetBackgroundPosition(string p);

			// Token: 0x06005E72 RID: 24178
			string GetBackgroundPosition();

			// Token: 0x06005E73 RID: 24179
			void SetBackgroundPositionX(object p);

			// Token: 0x06005E74 RID: 24180
			object GetBackgroundPositionX();

			// Token: 0x06005E75 RID: 24181
			void SetBackgroundPositionY(object p);

			// Token: 0x06005E76 RID: 24182
			object GetBackgroundPositionY();

			// Token: 0x06005E77 RID: 24183
			void SetWordSpacing(object p);

			// Token: 0x06005E78 RID: 24184
			object GetWordSpacing();

			// Token: 0x06005E79 RID: 24185
			void SetLetterSpacing(object p);

			// Token: 0x06005E7A RID: 24186
			object GetLetterSpacing();

			// Token: 0x06005E7B RID: 24187
			void SetTextDecoration(string p);

			// Token: 0x06005E7C RID: 24188
			string GetTextDecoration();

			// Token: 0x06005E7D RID: 24189
			void SetTextDecorationNone(bool p);

			// Token: 0x06005E7E RID: 24190
			bool GetTextDecorationNone();

			// Token: 0x06005E7F RID: 24191
			void SetTextDecorationUnderline(bool p);

			// Token: 0x06005E80 RID: 24192
			bool GetTextDecorationUnderline();

			// Token: 0x06005E81 RID: 24193
			void SetTextDecorationOverline(bool p);

			// Token: 0x06005E82 RID: 24194
			bool GetTextDecorationOverline();

			// Token: 0x06005E83 RID: 24195
			void SetTextDecorationLineThrough(bool p);

			// Token: 0x06005E84 RID: 24196
			bool GetTextDecorationLineThrough();

			// Token: 0x06005E85 RID: 24197
			void SetTextDecorationBlink(bool p);

			// Token: 0x06005E86 RID: 24198
			bool GetTextDecorationBlink();

			// Token: 0x06005E87 RID: 24199
			void SetVerticalAlign(object p);

			// Token: 0x06005E88 RID: 24200
			object GetVerticalAlign();

			// Token: 0x06005E89 RID: 24201
			void SetTextTransform(string p);

			// Token: 0x06005E8A RID: 24202
			string GetTextTransform();

			// Token: 0x06005E8B RID: 24203
			void SetTextAlign(string p);

			// Token: 0x06005E8C RID: 24204
			string GetTextAlign();

			// Token: 0x06005E8D RID: 24205
			void SetTextIndent(object p);

			// Token: 0x06005E8E RID: 24206
			object GetTextIndent();

			// Token: 0x06005E8F RID: 24207
			void SetLineHeight(object p);

			// Token: 0x06005E90 RID: 24208
			object GetLineHeight();

			// Token: 0x06005E91 RID: 24209
			void SetMarginTop(object p);

			// Token: 0x06005E92 RID: 24210
			object GetMarginTop();

			// Token: 0x06005E93 RID: 24211
			void SetMarginRight(object p);

			// Token: 0x06005E94 RID: 24212
			object GetMarginRight();

			// Token: 0x06005E95 RID: 24213
			void SetMarginBottom(object p);

			// Token: 0x06005E96 RID: 24214
			object GetMarginBottom();

			// Token: 0x06005E97 RID: 24215
			void SetMarginLeft(object p);

			// Token: 0x06005E98 RID: 24216
			object GetMarginLeft();

			// Token: 0x06005E99 RID: 24217
			void SetMargin(string p);

			// Token: 0x06005E9A RID: 24218
			string GetMargin();

			// Token: 0x06005E9B RID: 24219
			void SetPaddingTop(object p);

			// Token: 0x06005E9C RID: 24220
			object GetPaddingTop();

			// Token: 0x06005E9D RID: 24221
			void SetPaddingRight(object p);

			// Token: 0x06005E9E RID: 24222
			object GetPaddingRight();

			// Token: 0x06005E9F RID: 24223
			void SetPaddingBottom(object p);

			// Token: 0x06005EA0 RID: 24224
			object GetPaddingBottom();

			// Token: 0x06005EA1 RID: 24225
			void SetPaddingLeft(object p);

			// Token: 0x06005EA2 RID: 24226
			object GetPaddingLeft();

			// Token: 0x06005EA3 RID: 24227
			void SetPadding(string p);

			// Token: 0x06005EA4 RID: 24228
			string GetPadding();

			// Token: 0x06005EA5 RID: 24229
			void SetBorder(string p);

			// Token: 0x06005EA6 RID: 24230
			string GetBorder();

			// Token: 0x06005EA7 RID: 24231
			void SetBorderTop(string p);

			// Token: 0x06005EA8 RID: 24232
			string GetBorderTop();

			// Token: 0x06005EA9 RID: 24233
			void SetBorderRight(string p);

			// Token: 0x06005EAA RID: 24234
			string GetBorderRight();

			// Token: 0x06005EAB RID: 24235
			void SetBorderBottom(string p);

			// Token: 0x06005EAC RID: 24236
			string GetBorderBottom();

			// Token: 0x06005EAD RID: 24237
			void SetBorderLeft(string p);

			// Token: 0x06005EAE RID: 24238
			string GetBorderLeft();

			// Token: 0x06005EAF RID: 24239
			void SetBorderColor(string p);

			// Token: 0x06005EB0 RID: 24240
			string GetBorderColor();

			// Token: 0x06005EB1 RID: 24241
			void SetBorderTopColor(object p);

			// Token: 0x06005EB2 RID: 24242
			object GetBorderTopColor();

			// Token: 0x06005EB3 RID: 24243
			void SetBorderRightColor(object p);

			// Token: 0x06005EB4 RID: 24244
			object GetBorderRightColor();

			// Token: 0x06005EB5 RID: 24245
			void SetBorderBottomColor(object p);

			// Token: 0x06005EB6 RID: 24246
			object GetBorderBottomColor();

			// Token: 0x06005EB7 RID: 24247
			void SetBorderLeftColor(object p);

			// Token: 0x06005EB8 RID: 24248
			object GetBorderLeftColor();

			// Token: 0x06005EB9 RID: 24249
			void SetBorderWidth(string p);

			// Token: 0x06005EBA RID: 24250
			string GetBorderWidth();

			// Token: 0x06005EBB RID: 24251
			void SetBorderTopWidth(object p);

			// Token: 0x06005EBC RID: 24252
			object GetBorderTopWidth();

			// Token: 0x06005EBD RID: 24253
			void SetBorderRightWidth(object p);

			// Token: 0x06005EBE RID: 24254
			object GetBorderRightWidth();

			// Token: 0x06005EBF RID: 24255
			void SetBorderBottomWidth(object p);

			// Token: 0x06005EC0 RID: 24256
			object GetBorderBottomWidth();

			// Token: 0x06005EC1 RID: 24257
			void SetBorderLeftWidth(object p);

			// Token: 0x06005EC2 RID: 24258
			object GetBorderLeftWidth();

			// Token: 0x06005EC3 RID: 24259
			void SetBorderStyle(string p);

			// Token: 0x06005EC4 RID: 24260
			string GetBorderStyle();

			// Token: 0x06005EC5 RID: 24261
			void SetBorderTopStyle(string p);

			// Token: 0x06005EC6 RID: 24262
			string GetBorderTopStyle();

			// Token: 0x06005EC7 RID: 24263
			void SetBorderRightStyle(string p);

			// Token: 0x06005EC8 RID: 24264
			string GetBorderRightStyle();

			// Token: 0x06005EC9 RID: 24265
			void SetBorderBottomStyle(string p);

			// Token: 0x06005ECA RID: 24266
			string GetBorderBottomStyle();

			// Token: 0x06005ECB RID: 24267
			void SetBorderLeftStyle(string p);

			// Token: 0x06005ECC RID: 24268
			string GetBorderLeftStyle();

			// Token: 0x06005ECD RID: 24269
			void SetWidth(object p);

			// Token: 0x06005ECE RID: 24270
			object GetWidth();

			// Token: 0x06005ECF RID: 24271
			void SetHeight(object p);

			// Token: 0x06005ED0 RID: 24272
			object GetHeight();

			// Token: 0x06005ED1 RID: 24273
			void SetStyleFloat(string p);

			// Token: 0x06005ED2 RID: 24274
			string GetStyleFloat();

			// Token: 0x06005ED3 RID: 24275
			void SetClear(string p);

			// Token: 0x06005ED4 RID: 24276
			string GetClear();

			// Token: 0x06005ED5 RID: 24277
			void SetDisplay(string p);

			// Token: 0x06005ED6 RID: 24278
			string GetDisplay();

			// Token: 0x06005ED7 RID: 24279
			void SetVisibility(string p);

			// Token: 0x06005ED8 RID: 24280
			string GetVisibility();

			// Token: 0x06005ED9 RID: 24281
			void SetListStyleType(string p);

			// Token: 0x06005EDA RID: 24282
			string GetListStyleType();

			// Token: 0x06005EDB RID: 24283
			void SetListStylePosition(string p);

			// Token: 0x06005EDC RID: 24284
			string GetListStylePosition();

			// Token: 0x06005EDD RID: 24285
			void SetListStyleImage(string p);

			// Token: 0x06005EDE RID: 24286
			string GetListStyleImage();

			// Token: 0x06005EDF RID: 24287
			void SetListStyle(string p);

			// Token: 0x06005EE0 RID: 24288
			string GetListStyle();

			// Token: 0x06005EE1 RID: 24289
			void SetWhiteSpace(string p);

			// Token: 0x06005EE2 RID: 24290
			string GetWhiteSpace();

			// Token: 0x06005EE3 RID: 24291
			void SetTop(object p);

			// Token: 0x06005EE4 RID: 24292
			object GetTop();

			// Token: 0x06005EE5 RID: 24293
			void SetLeft(object p);

			// Token: 0x06005EE6 RID: 24294
			object GetLeft();

			// Token: 0x06005EE7 RID: 24295
			string GetPosition();

			// Token: 0x06005EE8 RID: 24296
			void SetZIndex(object p);

			// Token: 0x06005EE9 RID: 24297
			object GetZIndex();

			// Token: 0x06005EEA RID: 24298
			void SetOverflow(string p);

			// Token: 0x06005EEB RID: 24299
			string GetOverflow();

			// Token: 0x06005EEC RID: 24300
			void SetPageBreakBefore(string p);

			// Token: 0x06005EED RID: 24301
			string GetPageBreakBefore();

			// Token: 0x06005EEE RID: 24302
			void SetPageBreakAfter(string p);

			// Token: 0x06005EEF RID: 24303
			string GetPageBreakAfter();

			// Token: 0x06005EF0 RID: 24304
			void SetCssText(string p);

			// Token: 0x06005EF1 RID: 24305
			string GetCssText();

			// Token: 0x06005EF2 RID: 24306
			void SetPixelTop(int p);

			// Token: 0x06005EF3 RID: 24307
			int GetPixelTop();

			// Token: 0x06005EF4 RID: 24308
			void SetPixelLeft(int p);

			// Token: 0x06005EF5 RID: 24309
			int GetPixelLeft();

			// Token: 0x06005EF6 RID: 24310
			void SetPixelWidth(int p);

			// Token: 0x06005EF7 RID: 24311
			int GetPixelWidth();

			// Token: 0x06005EF8 RID: 24312
			void SetPixelHeight(int p);

			// Token: 0x06005EF9 RID: 24313
			int GetPixelHeight();

			// Token: 0x06005EFA RID: 24314
			void SetPosTop(float p);

			// Token: 0x06005EFB RID: 24315
			float GetPosTop();

			// Token: 0x06005EFC RID: 24316
			void SetPosLeft(float p);

			// Token: 0x06005EFD RID: 24317
			float GetPosLeft();

			// Token: 0x06005EFE RID: 24318
			void SetPosWidth(float p);

			// Token: 0x06005EFF RID: 24319
			float GetPosWidth();

			// Token: 0x06005F00 RID: 24320
			void SetPosHeight(float p);

			// Token: 0x06005F01 RID: 24321
			float GetPosHeight();

			// Token: 0x06005F02 RID: 24322
			void SetCursor(string p);

			// Token: 0x06005F03 RID: 24323
			string GetCursor();

			// Token: 0x06005F04 RID: 24324
			void SetClip(string p);

			// Token: 0x06005F05 RID: 24325
			string GetClip();

			// Token: 0x06005F06 RID: 24326
			void SetFilter(string p);

			// Token: 0x06005F07 RID: 24327
			string GetFilter();

			// Token: 0x06005F08 RID: 24328
			void SetAttribute(string strAttributeName, object AttributeValue, int lFlags);

			// Token: 0x06005F09 RID: 24329
			object GetAttribute(string strAttributeName, int lFlags);

			// Token: 0x06005F0A RID: 24330
			bool RemoveAttribute(string strAttributeName, int lFlags);
		}

		// Token: 0x0200059B RID: 1435
		[Guid("39088D7E-B71E-11D1-8F39-00C04FD946D0")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IExtender
		{
			// Token: 0x170014B0 RID: 5296
			// (get) Token: 0x06005F0B RID: 24331
			// (set) Token: 0x06005F0C RID: 24332
			int Align { get; set; }

			// Token: 0x170014B1 RID: 5297
			// (get) Token: 0x06005F0D RID: 24333
			// (set) Token: 0x06005F0E RID: 24334
			bool Enabled { get; set; }

			// Token: 0x170014B2 RID: 5298
			// (get) Token: 0x06005F0F RID: 24335
			// (set) Token: 0x06005F10 RID: 24336
			int Height { get; set; }

			// Token: 0x170014B3 RID: 5299
			// (get) Token: 0x06005F11 RID: 24337
			// (set) Token: 0x06005F12 RID: 24338
			int Left { get; set; }

			// Token: 0x170014B4 RID: 5300
			// (get) Token: 0x06005F13 RID: 24339
			// (set) Token: 0x06005F14 RID: 24340
			bool TabStop { get; set; }

			// Token: 0x170014B5 RID: 5301
			// (get) Token: 0x06005F15 RID: 24341
			// (set) Token: 0x06005F16 RID: 24342
			int Top { get; set; }

			// Token: 0x170014B6 RID: 5302
			// (get) Token: 0x06005F17 RID: 24343
			// (set) Token: 0x06005F18 RID: 24344
			bool Visible { get; set; }

			// Token: 0x170014B7 RID: 5303
			// (get) Token: 0x06005F19 RID: 24345
			// (set) Token: 0x06005F1A RID: 24346
			int Width { get; set; }

			// Token: 0x170014B8 RID: 5304
			// (get) Token: 0x06005F1B RID: 24347
			string Name
			{
				[return: MarshalAs(UnmanagedType.BStr)]
				get;
			}

			// Token: 0x170014B9 RID: 5305
			// (get) Token: 0x06005F1C RID: 24348
			object Parent
			{
				[return: MarshalAs(UnmanagedType.Interface)]
				get;
			}

			// Token: 0x170014BA RID: 5306
			// (get) Token: 0x06005F1D RID: 24349
			IntPtr Hwnd { get; }

			// Token: 0x170014BB RID: 5307
			// (get) Token: 0x06005F1E RID: 24350
			object Container
			{
				[return: MarshalAs(UnmanagedType.Interface)]
				get;
			}

			// Token: 0x06005F1F RID: 24351
			void Move([MarshalAs(UnmanagedType.Interface)] [In] object left, [MarshalAs(UnmanagedType.Interface)] [In] object top, [MarshalAs(UnmanagedType.Interface)] [In] object width, [MarshalAs(UnmanagedType.Interface)] [In] object height);
		}

		// Token: 0x0200059C RID: 1436
		[Guid("8A701DA0-4FEB-101B-A82E-08002B2B2337")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IGetOleObject
		{
			// Token: 0x06005F20 RID: 24352
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetOleObject(ref Guid riid);
		}

		// Token: 0x0200059D RID: 1437
		[Guid("CB2F6722-AB3A-11d2-9C40-00C04FA30A3E")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface ICorRuntimeHost
		{
			// Token: 0x06005F21 RID: 24353
			[PreserveSig]
			int CreateLogicalThreadState();

			// Token: 0x06005F22 RID: 24354
			[PreserveSig]
			int DeleteLogicalThreadState();

			// Token: 0x06005F23 RID: 24355
			[PreserveSig]
			int SwitchInLogicalThreadState([In] ref uint pFiberCookie);

			// Token: 0x06005F24 RID: 24356
			[PreserveSig]
			int SwitchOutLogicalThreadState(out uint FiberCookie);

			// Token: 0x06005F25 RID: 24357
			[PreserveSig]
			int LocksHeldByLogicalThread(out uint pCount);

			// Token: 0x06005F26 RID: 24358
			[PreserveSig]
			int MapFile(IntPtr hFile, out IntPtr hMapAddress);

			// Token: 0x06005F27 RID: 24359
			[PreserveSig]
			int GetConfiguration([MarshalAs(UnmanagedType.IUnknown)] out object pConfiguration);

			// Token: 0x06005F28 RID: 24360
			[PreserveSig]
			int Start();

			// Token: 0x06005F29 RID: 24361
			[PreserveSig]
			int Stop();

			// Token: 0x06005F2A RID: 24362
			[PreserveSig]
			int CreateDomain(string pwzFriendlyName, [MarshalAs(UnmanagedType.IUnknown)] object pIdentityArray, [MarshalAs(UnmanagedType.IUnknown)] out object pAppDomain);

			// Token: 0x06005F2B RID: 24363
			[PreserveSig]
			int GetDefaultDomain([MarshalAs(UnmanagedType.IUnknown)] out object pAppDomain);

			// Token: 0x06005F2C RID: 24364
			[PreserveSig]
			int EnumDomains(out IntPtr hEnum);

			// Token: 0x06005F2D RID: 24365
			[PreserveSig]
			int NextDomain(IntPtr hEnum, [MarshalAs(UnmanagedType.IUnknown)] out object pAppDomain);

			// Token: 0x06005F2E RID: 24366
			[PreserveSig]
			int CloseEnum(IntPtr hEnum);

			// Token: 0x06005F2F RID: 24367
			[PreserveSig]
			int CreateDomainEx(string pwzFriendlyName, [MarshalAs(UnmanagedType.IUnknown)] object pSetup, [MarshalAs(UnmanagedType.IUnknown)] object pEvidence, [MarshalAs(UnmanagedType.IUnknown)] out object pAppDomain);

			// Token: 0x06005F30 RID: 24368
			[PreserveSig]
			int CreateDomainSetup([MarshalAs(UnmanagedType.IUnknown)] out object pAppDomainSetup);

			// Token: 0x06005F31 RID: 24369
			[PreserveSig]
			int CreateEvidence([MarshalAs(UnmanagedType.IUnknown)] out object pEvidence);

			// Token: 0x06005F32 RID: 24370
			[PreserveSig]
			int UnloadDomain([MarshalAs(UnmanagedType.IUnknown)] object pAppDomain);

			// Token: 0x06005F33 RID: 24371
			[PreserveSig]
			int CurrentDomain([MarshalAs(UnmanagedType.IUnknown)] out object pAppDomain);
		}

		// Token: 0x0200059E RID: 1438
		[Guid("CB2F6723-AB3A-11d2-9C40-00C04FA30A3E")]
		[ComImport]
		internal class CorRuntimeHost
		{
			// Token: 0x06005F34 RID: 24372
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			public extern CorRuntimeHost();
		}

		// Token: 0x0200059F RID: 1439
		[Guid("000C0601-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IMsoComponentManager
		{
			// Token: 0x06005F35 RID: 24373
			[PreserveSig]
			int QueryService(ref Guid guidService, ref Guid iid, [MarshalAs(UnmanagedType.Interface)] out object ppvObj);

			// Token: 0x06005F36 RID: 24374
			[PreserveSig]
			bool FDebugMessage(IntPtr hInst, int msg, IntPtr wParam, IntPtr lParam);

			// Token: 0x06005F37 RID: 24375
			[PreserveSig]
			bool FRegisterComponent(UnsafeNativeMethods.IMsoComponent component, NativeMethods.MSOCRINFOSTRUCT pcrinfo, out IntPtr dwComponentID);

			// Token: 0x06005F38 RID: 24376
			[PreserveSig]
			bool FRevokeComponent(IntPtr dwComponentID);

			// Token: 0x06005F39 RID: 24377
			[PreserveSig]
			bool FUpdateComponentRegistration(IntPtr dwComponentID, NativeMethods.MSOCRINFOSTRUCT pcrinfo);

			// Token: 0x06005F3A RID: 24378
			[PreserveSig]
			bool FOnComponentActivate(IntPtr dwComponentID);

			// Token: 0x06005F3B RID: 24379
			[PreserveSig]
			bool FSetTrackingComponent(IntPtr dwComponentID, [MarshalAs(UnmanagedType.Bool)] [In] bool fTrack);

			// Token: 0x06005F3C RID: 24380
			[PreserveSig]
			void OnComponentEnterState(IntPtr dwComponentID, int uStateID, int uContext, int cpicmExclude, int rgpicmExclude, int dwReserved);

			// Token: 0x06005F3D RID: 24381
			[PreserveSig]
			bool FOnComponentExitState(IntPtr dwComponentID, int uStateID, int uContext, int cpicmExclude, int rgpicmExclude);

			// Token: 0x06005F3E RID: 24382
			[PreserveSig]
			bool FInState(int uStateID, IntPtr pvoid);

			// Token: 0x06005F3F RID: 24383
			[PreserveSig]
			bool FContinueIdle();

			// Token: 0x06005F40 RID: 24384
			[PreserveSig]
			bool FPushMessageLoop(IntPtr dwComponentID, int uReason, int pvLoopData);

			// Token: 0x06005F41 RID: 24385
			[PreserveSig]
			bool FCreateSubComponentManager([MarshalAs(UnmanagedType.Interface)] object punkOuter, [MarshalAs(UnmanagedType.Interface)] object punkServProv, ref Guid riid, out IntPtr ppvObj);

			// Token: 0x06005F42 RID: 24386
			[PreserveSig]
			bool FGetParentComponentManager(out UnsafeNativeMethods.IMsoComponentManager ppicm);

			// Token: 0x06005F43 RID: 24387
			[PreserveSig]
			bool FGetActiveComponent(int dwgac, [MarshalAs(UnmanagedType.LPArray)] [Out] UnsafeNativeMethods.IMsoComponent[] ppic, NativeMethods.MSOCRINFOSTRUCT pcrinfo, int dwReserved);
		}

		// Token: 0x020005A0 RID: 1440
		[Guid("000C0600-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IMsoComponent
		{
			// Token: 0x06005F44 RID: 24388
			[PreserveSig]
			bool FDebugMessage(IntPtr hInst, int msg, IntPtr wParam, IntPtr lParam);

			// Token: 0x06005F45 RID: 24389
			[PreserveSig]
			bool FPreTranslateMessage(ref NativeMethods.MSG msg);

			// Token: 0x06005F46 RID: 24390
			[PreserveSig]
			void OnEnterState(int uStateID, bool fEnter);

			// Token: 0x06005F47 RID: 24391
			[PreserveSig]
			void OnAppActivate(bool fActive, int dwOtherThreadID);

			// Token: 0x06005F48 RID: 24392
			[PreserveSig]
			void OnLoseActivation();

			// Token: 0x06005F49 RID: 24393
			[PreserveSig]
			void OnActivationChange(UnsafeNativeMethods.IMsoComponent component, bool fSameComponent, int pcrinfo, bool fHostIsActivating, int pchostinfo, int dwReserved);

			// Token: 0x06005F4A RID: 24394
			[PreserveSig]
			bool FDoIdle(int grfidlef);

			// Token: 0x06005F4B RID: 24395
			[PreserveSig]
			bool FContinueMessageLoop(int uReason, int pvLoopData, [MarshalAs(UnmanagedType.LPArray)] NativeMethods.MSG[] pMsgPeeked);

			// Token: 0x06005F4C RID: 24396
			[PreserveSig]
			bool FQueryTerminate(bool fPromptUser);

			// Token: 0x06005F4D RID: 24397
			[PreserveSig]
			void Terminate();

			// Token: 0x06005F4E RID: 24398
			[PreserveSig]
			IntPtr HwndGetWindow(int dwWhich, int dwReserved);
		}

		// Token: 0x020005A1 RID: 1441
		[ComVisible(true)]
		[Guid("8CC497C0-A1DF-11ce-8098-00AA0047BE5D")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		[SuppressUnmanagedCodeSecurity]
		public interface ITextDocument
		{
			// Token: 0x06005F4F RID: 24399
			string GetName();

			// Token: 0x06005F50 RID: 24400
			object GetSelection();

			// Token: 0x06005F51 RID: 24401
			int GetStoryCount();

			// Token: 0x06005F52 RID: 24402
			object GetStoryRanges();

			// Token: 0x06005F53 RID: 24403
			int GetSaved();

			// Token: 0x06005F54 RID: 24404
			void SetSaved(int value);

			// Token: 0x06005F55 RID: 24405
			object GetDefaultTabStop();

			// Token: 0x06005F56 RID: 24406
			void SetDefaultTabStop(object value);

			// Token: 0x06005F57 RID: 24407
			void New();

			// Token: 0x06005F58 RID: 24408
			void Open(object pVar, int flags, int codePage);

			// Token: 0x06005F59 RID: 24409
			void Save(object pVar, int flags, int codePage);

			// Token: 0x06005F5A RID: 24410
			int Freeze();

			// Token: 0x06005F5B RID: 24411
			int Unfreeze();

			// Token: 0x06005F5C RID: 24412
			void BeginEditCollection();

			// Token: 0x06005F5D RID: 24413
			void EndEditCollection();

			// Token: 0x06005F5E RID: 24414
			int Undo(int count);

			// Token: 0x06005F5F RID: 24415
			int Redo(int count);

			// Token: 0x06005F60 RID: 24416
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.ITextRange Range(int cp1, int cp2);

			// Token: 0x06005F61 RID: 24417
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.ITextRange RangeFromPoint(int x, int y);
		}

		// Token: 0x020005A2 RID: 1442
		[ComVisible(true)]
		[Guid("8CC497C2-A1DF-11ce-8098-00AA0047BE5D")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		[SuppressUnmanagedCodeSecurity]
		public interface ITextRange
		{
			// Token: 0x06005F62 RID: 24418
			string GetText();

			// Token: 0x06005F63 RID: 24419
			void SetText(string text);

			// Token: 0x06005F64 RID: 24420
			object GetChar();

			// Token: 0x06005F65 RID: 24421
			void SetChar(object ch);

			// Token: 0x06005F66 RID: 24422
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.ITextRange GetDuplicate();

			// Token: 0x06005F67 RID: 24423
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.ITextRange GetFormattedText();

			// Token: 0x06005F68 RID: 24424
			void SetFormattedText([MarshalAs(UnmanagedType.Interface)] [In] UnsafeNativeMethods.ITextRange range);

			// Token: 0x06005F69 RID: 24425
			int GetStart();

			// Token: 0x06005F6A RID: 24426
			void SetStart(int cpFirst);

			// Token: 0x06005F6B RID: 24427
			int GetEnd();

			// Token: 0x06005F6C RID: 24428
			void SetEnd(int cpLim);

			// Token: 0x06005F6D RID: 24429
			object GetFont();

			// Token: 0x06005F6E RID: 24430
			void SetFont(object font);

			// Token: 0x06005F6F RID: 24431
			object GetPara();

			// Token: 0x06005F70 RID: 24432
			void SetPara(object para);

			// Token: 0x06005F71 RID: 24433
			int GetStoryLength();

			// Token: 0x06005F72 RID: 24434
			int GetStoryType();

			// Token: 0x06005F73 RID: 24435
			void Collapse(int start);

			// Token: 0x06005F74 RID: 24436
			int Expand(int unit);

			// Token: 0x06005F75 RID: 24437
			int GetIndex(int unit);

			// Token: 0x06005F76 RID: 24438
			void SetIndex(int unit, int index, int extend);

			// Token: 0x06005F77 RID: 24439
			void SetRange(int cpActive, int cpOther);

			// Token: 0x06005F78 RID: 24440
			int InRange([MarshalAs(UnmanagedType.Interface)] [In] UnsafeNativeMethods.ITextRange range);

			// Token: 0x06005F79 RID: 24441
			int InStory([MarshalAs(UnmanagedType.Interface)] [In] UnsafeNativeMethods.ITextRange range);

			// Token: 0x06005F7A RID: 24442
			int IsEqual([MarshalAs(UnmanagedType.Interface)] [In] UnsafeNativeMethods.ITextRange range);

			// Token: 0x06005F7B RID: 24443
			void Select();

			// Token: 0x06005F7C RID: 24444
			int StartOf(int unit, int extend);

			// Token: 0x06005F7D RID: 24445
			int EndOf(int unit, int extend);

			// Token: 0x06005F7E RID: 24446
			int Move(int unit, int count);

			// Token: 0x06005F7F RID: 24447
			int MoveStart(int unit, int count);

			// Token: 0x06005F80 RID: 24448
			int MoveEnd(int unit, int count);

			// Token: 0x06005F81 RID: 24449
			int MoveWhile(object cset, int count);

			// Token: 0x06005F82 RID: 24450
			int MoveStartWhile(object cset, int count);

			// Token: 0x06005F83 RID: 24451
			int MoveEndWhile(object cset, int count);

			// Token: 0x06005F84 RID: 24452
			int MoveUntil(object cset, int count);

			// Token: 0x06005F85 RID: 24453
			int MoveStartUntil(object cset, int count);

			// Token: 0x06005F86 RID: 24454
			int MoveEndUntil(object cset, int count);

			// Token: 0x06005F87 RID: 24455
			int FindText(string text, int cch, int flags);

			// Token: 0x06005F88 RID: 24456
			int FindTextStart(string text, int cch, int flags);

			// Token: 0x06005F89 RID: 24457
			int FindTextEnd(string text, int cch, int flags);

			// Token: 0x06005F8A RID: 24458
			int Delete(int unit, int count);

			// Token: 0x06005F8B RID: 24459
			void Cut(out object pVar);

			// Token: 0x06005F8C RID: 24460
			void Copy(out object pVar);

			// Token: 0x06005F8D RID: 24461
			void Paste(object pVar, int format);

			// Token: 0x06005F8E RID: 24462
			int CanPaste(object pVar, int format);

			// Token: 0x06005F8F RID: 24463
			int CanEdit();

			// Token: 0x06005F90 RID: 24464
			void ChangeCase(int type);

			// Token: 0x06005F91 RID: 24465
			void GetPoint(int type, out int x, out int y);

			// Token: 0x06005F92 RID: 24466
			void SetPoint(int x, int y, int type, int extend);

			// Token: 0x06005F93 RID: 24467
			void ScrollIntoView(int value);

			// Token: 0x06005F94 RID: 24468
			object GetEmbeddedObject();
		}

		// Token: 0x020005A3 RID: 1443
		[Guid("00020D03-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IRichEditOleCallback
		{
			// Token: 0x06005F95 RID: 24469
			[PreserveSig]
			int GetNewStorage(out UnsafeNativeMethods.IStorage ret);

			// Token: 0x06005F96 RID: 24470
			[PreserveSig]
			int GetInPlaceContext(IntPtr lplpFrame, IntPtr lplpDoc, IntPtr lpFrameInfo);

			// Token: 0x06005F97 RID: 24471
			[PreserveSig]
			int ShowContainerUI(int fShow);

			// Token: 0x06005F98 RID: 24472
			[PreserveSig]
			int QueryInsertObject(ref Guid lpclsid, IntPtr lpstg, int cp);

			// Token: 0x06005F99 RID: 24473
			[PreserveSig]
			int DeleteObject(IntPtr lpoleobj);

			// Token: 0x06005F9A RID: 24474
			[PreserveSig]
			int QueryAcceptData(IDataObject lpdataobj, IntPtr lpcfFormat, int reco, int fReally, IntPtr hMetaPict);

			// Token: 0x06005F9B RID: 24475
			[PreserveSig]
			int ContextSensitiveHelp(int fEnterMode);

			// Token: 0x06005F9C RID: 24476
			[PreserveSig]
			int GetClipboardData(NativeMethods.CHARRANGE lpchrg, int reco, IntPtr lplpdataobj);

			// Token: 0x06005F9D RID: 24477
			[PreserveSig]
			int GetDragDropEffect(bool fDrag, int grfKeyState, ref int pdwEffect);

			// Token: 0x06005F9E RID: 24478
			[PreserveSig]
			int GetContextMenu(short seltype, IntPtr lpoleobj, NativeMethods.CHARRANGE lpchrg, out IntPtr hmenu);
		}

		// Token: 0x020005A4 RID: 1444
		[Guid("00000115-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IOleInPlaceUIWindow
		{
			// Token: 0x06005F9F RID: 24479
			IntPtr GetWindow();

			// Token: 0x06005FA0 RID: 24480
			[PreserveSig]
			int ContextSensitiveHelp(int fEnterMode);

			// Token: 0x06005FA1 RID: 24481
			[PreserveSig]
			int GetBorder([Out] NativeMethods.COMRECT lprectBorder);

			// Token: 0x06005FA2 RID: 24482
			[PreserveSig]
			int RequestBorderSpace([In] NativeMethods.COMRECT pborderwidths);

			// Token: 0x06005FA3 RID: 24483
			[PreserveSig]
			int SetBorderSpace([In] NativeMethods.COMRECT pborderwidths);

			// Token: 0x06005FA4 RID: 24484
			void SetActiveObject([MarshalAs(UnmanagedType.Interface)] [In] UnsafeNativeMethods.IOleInPlaceActiveObject pActiveObject, [MarshalAs(UnmanagedType.LPWStr)] [In] string pszObjName);
		}

		// Token: 0x020005A5 RID: 1445
		[SuppressUnmanagedCodeSecurity]
		[Guid("00000117-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IOleInPlaceActiveObject
		{
			// Token: 0x06005FA5 RID: 24485
			[PreserveSig]
			int GetWindow(out IntPtr hwnd);

			// Token: 0x06005FA6 RID: 24486
			void ContextSensitiveHelp(int fEnterMode);

			// Token: 0x06005FA7 RID: 24487
			[PreserveSig]
			int TranslateAccelerator([In] ref NativeMethods.MSG lpmsg);

			// Token: 0x06005FA8 RID: 24488
			void OnFrameWindowActivate(bool fActivate);

			// Token: 0x06005FA9 RID: 24489
			void OnDocWindowActivate(int fActivate);

			// Token: 0x06005FAA RID: 24490
			void ResizeBorder([In] NativeMethods.COMRECT prcBorder, [In] UnsafeNativeMethods.IOleInPlaceUIWindow pUIWindow, bool fFrameWindow);

			// Token: 0x06005FAB RID: 24491
			void EnableModeless(int fEnable);
		}

		// Token: 0x020005A6 RID: 1446
		[Guid("00000114-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IOleWindow
		{
			// Token: 0x06005FAC RID: 24492
			[PreserveSig]
			int GetWindow(out IntPtr hwnd);

			// Token: 0x06005FAD RID: 24493
			void ContextSensitiveHelp(int fEnterMode);
		}

		// Token: 0x020005A7 RID: 1447
		[SuppressUnmanagedCodeSecurity]
		[Guid("00000113-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IOleInPlaceObject
		{
			// Token: 0x06005FAE RID: 24494
			[PreserveSig]
			int GetWindow(out IntPtr hwnd);

			// Token: 0x06005FAF RID: 24495
			void ContextSensitiveHelp(int fEnterMode);

			// Token: 0x06005FB0 RID: 24496
			void InPlaceDeactivate();

			// Token: 0x06005FB1 RID: 24497
			[PreserveSig]
			int UIDeactivate();

			// Token: 0x06005FB2 RID: 24498
			void SetObjectRects([In] NativeMethods.COMRECT lprcPosRect, [In] NativeMethods.COMRECT lprcClipRect);

			// Token: 0x06005FB3 RID: 24499
			void ReactivateAndUndo();
		}

		// Token: 0x020005A8 RID: 1448
		[SuppressUnmanagedCodeSecurity]
		[Guid("00000112-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IOleObject
		{
			// Token: 0x06005FB4 RID: 24500
			[PreserveSig]
			int SetClientSite([MarshalAs(UnmanagedType.Interface)] [In] UnsafeNativeMethods.IOleClientSite pClientSite);

			// Token: 0x06005FB5 RID: 24501
			UnsafeNativeMethods.IOleClientSite GetClientSite();

			// Token: 0x06005FB6 RID: 24502
			[PreserveSig]
			int SetHostNames([MarshalAs(UnmanagedType.LPWStr)] [In] string szContainerApp, [MarshalAs(UnmanagedType.LPWStr)] [In] string szContainerObj);

			// Token: 0x06005FB7 RID: 24503
			[PreserveSig]
			int Close(int dwSaveOption);

			// Token: 0x06005FB8 RID: 24504
			[PreserveSig]
			int SetMoniker([MarshalAs(UnmanagedType.U4)] [In] int dwWhichMoniker, [MarshalAs(UnmanagedType.Interface)] [In] object pmk);

			// Token: 0x06005FB9 RID: 24505
			[PreserveSig]
			int GetMoniker([MarshalAs(UnmanagedType.U4)] [In] int dwAssign, [MarshalAs(UnmanagedType.U4)] [In] int dwWhichMoniker, [MarshalAs(UnmanagedType.Interface)] out object moniker);

			// Token: 0x06005FBA RID: 24506
			[PreserveSig]
			int InitFromData([MarshalAs(UnmanagedType.Interface)] [In] IDataObject pDataObject, int fCreation, [MarshalAs(UnmanagedType.U4)] [In] int dwReserved);

			// Token: 0x06005FBB RID: 24507
			[PreserveSig]
			int GetClipboardData([MarshalAs(UnmanagedType.U4)] [In] int dwReserved, out IDataObject data);

			// Token: 0x06005FBC RID: 24508
			[PreserveSig]
			int DoVerb(int iVerb, [In] IntPtr lpmsg, [MarshalAs(UnmanagedType.Interface)] [In] UnsafeNativeMethods.IOleClientSite pActiveSite, int lindex, IntPtr hwndParent, [In] NativeMethods.COMRECT lprcPosRect);

			// Token: 0x06005FBD RID: 24509
			[PreserveSig]
			int EnumVerbs(out UnsafeNativeMethods.IEnumOLEVERB e);

			// Token: 0x06005FBE RID: 24510
			[PreserveSig]
			int OleUpdate();

			// Token: 0x06005FBF RID: 24511
			[PreserveSig]
			int IsUpToDate();

			// Token: 0x06005FC0 RID: 24512
			[PreserveSig]
			int GetUserClassID([In] [Out] ref Guid pClsid);

			// Token: 0x06005FC1 RID: 24513
			[PreserveSig]
			int GetUserType([MarshalAs(UnmanagedType.U4)] [In] int dwFormOfType, [MarshalAs(UnmanagedType.LPWStr)] out string userType);

			// Token: 0x06005FC2 RID: 24514
			[PreserveSig]
			int SetExtent([MarshalAs(UnmanagedType.U4)] [In] int dwDrawAspect, [In] NativeMethods.tagSIZEL pSizel);

			// Token: 0x06005FC3 RID: 24515
			[PreserveSig]
			int GetExtent([MarshalAs(UnmanagedType.U4)] [In] int dwDrawAspect, [Out] NativeMethods.tagSIZEL pSizel);

			// Token: 0x06005FC4 RID: 24516
			[PreserveSig]
			int Advise(IAdviseSink pAdvSink, out int cookie);

			// Token: 0x06005FC5 RID: 24517
			[PreserveSig]
			int Unadvise([MarshalAs(UnmanagedType.U4)] [In] int dwConnection);

			// Token: 0x06005FC6 RID: 24518
			[PreserveSig]
			int EnumAdvise(out IEnumSTATDATA e);

			// Token: 0x06005FC7 RID: 24519
			[PreserveSig]
			int GetMiscStatus([MarshalAs(UnmanagedType.U4)] [In] int dwAspect, out int misc);

			// Token: 0x06005FC8 RID: 24520
			[PreserveSig]
			int SetColorScheme([In] NativeMethods.tagLOGPALETTE pLogpal);
		}

		// Token: 0x020005A9 RID: 1449
		[Guid("1C2056CC-5EF4-101B-8BC8-00AA003E3B29")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IOleInPlaceObjectWindowless
		{
			// Token: 0x06005FC9 RID: 24521
			[PreserveSig]
			int SetClientSite([MarshalAs(UnmanagedType.Interface)] [In] UnsafeNativeMethods.IOleClientSite pClientSite);

			// Token: 0x06005FCA RID: 24522
			[PreserveSig]
			int GetClientSite(out UnsafeNativeMethods.IOleClientSite site);

			// Token: 0x06005FCB RID: 24523
			[PreserveSig]
			int SetHostNames([MarshalAs(UnmanagedType.LPWStr)] [In] string szContainerApp, [MarshalAs(UnmanagedType.LPWStr)] [In] string szContainerObj);

			// Token: 0x06005FCC RID: 24524
			[PreserveSig]
			int Close(int dwSaveOption);

			// Token: 0x06005FCD RID: 24525
			[PreserveSig]
			int SetMoniker([MarshalAs(UnmanagedType.U4)] [In] int dwWhichMoniker, [MarshalAs(UnmanagedType.Interface)] [In] object pmk);

			// Token: 0x06005FCE RID: 24526
			[PreserveSig]
			int GetMoniker([MarshalAs(UnmanagedType.U4)] [In] int dwAssign, [MarshalAs(UnmanagedType.U4)] [In] int dwWhichMoniker, [MarshalAs(UnmanagedType.Interface)] out object moniker);

			// Token: 0x06005FCF RID: 24527
			[PreserveSig]
			int InitFromData([MarshalAs(UnmanagedType.Interface)] [In] IDataObject pDataObject, int fCreation, [MarshalAs(UnmanagedType.U4)] [In] int dwReserved);

			// Token: 0x06005FD0 RID: 24528
			[PreserveSig]
			int GetClipboardData([MarshalAs(UnmanagedType.U4)] [In] int dwReserved, out IDataObject data);

			// Token: 0x06005FD1 RID: 24529
			[PreserveSig]
			int DoVerb(int iVerb, [In] IntPtr lpmsg, [MarshalAs(UnmanagedType.Interface)] [In] UnsafeNativeMethods.IOleClientSite pActiveSite, int lindex, IntPtr hwndParent, [In] NativeMethods.COMRECT lprcPosRect);

			// Token: 0x06005FD2 RID: 24530
			[PreserveSig]
			int EnumVerbs(out UnsafeNativeMethods.IEnumOLEVERB e);

			// Token: 0x06005FD3 RID: 24531
			[PreserveSig]
			int OleUpdate();

			// Token: 0x06005FD4 RID: 24532
			[PreserveSig]
			int IsUpToDate();

			// Token: 0x06005FD5 RID: 24533
			[PreserveSig]
			int GetUserClassID([In] [Out] ref Guid pClsid);

			// Token: 0x06005FD6 RID: 24534
			[PreserveSig]
			int GetUserType([MarshalAs(UnmanagedType.U4)] [In] int dwFormOfType, [MarshalAs(UnmanagedType.LPWStr)] out string userType);

			// Token: 0x06005FD7 RID: 24535
			[PreserveSig]
			int SetExtent([MarshalAs(UnmanagedType.U4)] [In] int dwDrawAspect, [In] NativeMethods.tagSIZEL pSizel);

			// Token: 0x06005FD8 RID: 24536
			[PreserveSig]
			int GetExtent([MarshalAs(UnmanagedType.U4)] [In] int dwDrawAspect, [Out] NativeMethods.tagSIZEL pSizel);

			// Token: 0x06005FD9 RID: 24537
			[PreserveSig]
			int Advise([MarshalAs(UnmanagedType.Interface)] [In] IAdviseSink pAdvSink, out int cookie);

			// Token: 0x06005FDA RID: 24538
			[PreserveSig]
			int Unadvise([MarshalAs(UnmanagedType.U4)] [In] int dwConnection);

			// Token: 0x06005FDB RID: 24539
			[PreserveSig]
			int EnumAdvise(out IEnumSTATDATA e);

			// Token: 0x06005FDC RID: 24540
			[PreserveSig]
			int GetMiscStatus([MarshalAs(UnmanagedType.U4)] [In] int dwAspect, out int misc);

			// Token: 0x06005FDD RID: 24541
			[PreserveSig]
			int SetColorScheme([In] NativeMethods.tagLOGPALETTE pLogpal);

			// Token: 0x06005FDE RID: 24542
			[PreserveSig]
			int OnWindowMessage([MarshalAs(UnmanagedType.U4)] [In] int msg, [MarshalAs(UnmanagedType.U4)] [In] int wParam, [MarshalAs(UnmanagedType.U4)] [In] int lParam, [MarshalAs(UnmanagedType.U4)] [Out] int plResult);

			// Token: 0x06005FDF RID: 24543
			[PreserveSig]
			int GetDropTarget([MarshalAs(UnmanagedType.Interface)] [Out] object ppDropTarget);
		}

		// Token: 0x020005AA RID: 1450
		[SuppressUnmanagedCodeSecurity]
		[Guid("B196B288-BAB4-101A-B69C-00AA00341D07")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IOleControl
		{
			// Token: 0x06005FE0 RID: 24544
			[PreserveSig]
			int GetControlInfo([Out] NativeMethods.tagCONTROLINFO pCI);

			// Token: 0x06005FE1 RID: 24545
			[PreserveSig]
			int OnMnemonic([In] ref NativeMethods.MSG pMsg);

			// Token: 0x06005FE2 RID: 24546
			[PreserveSig]
			int OnAmbientPropertyChange(int dispID);

			// Token: 0x06005FE3 RID: 24547
			[PreserveSig]
			int FreezeEvents(int bFreeze);
		}

		// Token: 0x020005AB RID: 1451
		[Guid("6D5140C1-7436-11CE-8034-00AA006009FA")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IOleServiceProvider
		{
			// Token: 0x06005FE4 RID: 24548
			[PreserveSig]
			int QueryService([In] ref Guid guidService, [In] ref Guid riid, out IntPtr ppvObject);
		}

		// Token: 0x020005AC RID: 1452
		[Guid("0000010d-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IViewObject
		{
			// Token: 0x06005FE5 RID: 24549
			[PreserveSig]
			int Draw([MarshalAs(UnmanagedType.U4)] [In] int dwDrawAspect, int lindex, IntPtr pvAspect, [In] NativeMethods.tagDVTARGETDEVICE ptd, IntPtr hdcTargetDev, IntPtr hdcDraw, [In] NativeMethods.COMRECT lprcBounds, [In] NativeMethods.COMRECT lprcWBounds, IntPtr pfnContinue, [In] int dwContinue);

			// Token: 0x06005FE6 RID: 24550
			[PreserveSig]
			int GetColorSet([MarshalAs(UnmanagedType.U4)] [In] int dwDrawAspect, int lindex, IntPtr pvAspect, [In] NativeMethods.tagDVTARGETDEVICE ptd, IntPtr hicTargetDev, [Out] NativeMethods.tagLOGPALETTE ppColorSet);

			// Token: 0x06005FE7 RID: 24551
			[PreserveSig]
			int Freeze([MarshalAs(UnmanagedType.U4)] [In] int dwDrawAspect, int lindex, IntPtr pvAspect, [Out] IntPtr pdwFreeze);

			// Token: 0x06005FE8 RID: 24552
			[PreserveSig]
			int Unfreeze([MarshalAs(UnmanagedType.U4)] [In] int dwFreeze);

			// Token: 0x06005FE9 RID: 24553
			void SetAdvise([MarshalAs(UnmanagedType.U4)] [In] int aspects, [MarshalAs(UnmanagedType.U4)] [In] int advf, [MarshalAs(UnmanagedType.Interface)] [In] IAdviseSink pAdvSink);

			// Token: 0x06005FEA RID: 24554
			void GetAdvise([MarshalAs(UnmanagedType.LPArray)] [In] [Out] int[] paspects, [MarshalAs(UnmanagedType.LPArray)] [In] [Out] int[] advf, [MarshalAs(UnmanagedType.LPArray)] [In] [Out] IAdviseSink[] pAdvSink);
		}

		// Token: 0x020005AD RID: 1453
		[Guid("00000127-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IViewObject2
		{
			// Token: 0x06005FEB RID: 24555
			void Draw([MarshalAs(UnmanagedType.U4)] [In] int dwDrawAspect, int lindex, IntPtr pvAspect, [In] NativeMethods.tagDVTARGETDEVICE ptd, IntPtr hdcTargetDev, IntPtr hdcDraw, [In] NativeMethods.COMRECT lprcBounds, [In] NativeMethods.COMRECT lprcWBounds, IntPtr pfnContinue, [In] int dwContinue);

			// Token: 0x06005FEC RID: 24556
			[PreserveSig]
			int GetColorSet([MarshalAs(UnmanagedType.U4)] [In] int dwDrawAspect, int lindex, IntPtr pvAspect, [In] NativeMethods.tagDVTARGETDEVICE ptd, IntPtr hicTargetDev, [Out] NativeMethods.tagLOGPALETTE ppColorSet);

			// Token: 0x06005FED RID: 24557
			[PreserveSig]
			int Freeze([MarshalAs(UnmanagedType.U4)] [In] int dwDrawAspect, int lindex, IntPtr pvAspect, [Out] IntPtr pdwFreeze);

			// Token: 0x06005FEE RID: 24558
			[PreserveSig]
			int Unfreeze([MarshalAs(UnmanagedType.U4)] [In] int dwFreeze);

			// Token: 0x06005FEF RID: 24559
			void SetAdvise([MarshalAs(UnmanagedType.U4)] [In] int aspects, [MarshalAs(UnmanagedType.U4)] [In] int advf, [MarshalAs(UnmanagedType.Interface)] [In] IAdviseSink pAdvSink);

			// Token: 0x06005FF0 RID: 24560
			void GetAdvise([MarshalAs(UnmanagedType.LPArray)] [In] [Out] int[] paspects, [MarshalAs(UnmanagedType.LPArray)] [In] [Out] int[] advf, [MarshalAs(UnmanagedType.LPArray)] [In] [Out] IAdviseSink[] pAdvSink);

			// Token: 0x06005FF1 RID: 24561
			void GetExtent([MarshalAs(UnmanagedType.U4)] [In] int dwDrawAspect, int lindex, [In] NativeMethods.tagDVTARGETDEVICE ptd, [Out] NativeMethods.tagSIZEL lpsizel);
		}

		// Token: 0x020005AE RID: 1454
		[Guid("0000010C-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IPersist
		{
			// Token: 0x06005FF2 RID: 24562
			[SuppressUnmanagedCodeSecurity]
			void GetClassID(out Guid pClassID);
		}

		// Token: 0x020005AF RID: 1455
		[Guid("37D84F60-42CB-11CE-8135-00AA004BB851")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IPersistPropertyBag
		{
			// Token: 0x06005FF3 RID: 24563
			void GetClassID(out Guid pClassID);

			// Token: 0x06005FF4 RID: 24564
			void InitNew();

			// Token: 0x06005FF5 RID: 24565
			void Load([MarshalAs(UnmanagedType.Interface)] [In] UnsafeNativeMethods.IPropertyBag pPropBag, [MarshalAs(UnmanagedType.Interface)] [In] UnsafeNativeMethods.IErrorLog pErrorLog);

			// Token: 0x06005FF6 RID: 24566
			void Save([MarshalAs(UnmanagedType.Interface)] [In] UnsafeNativeMethods.IPropertyBag pPropBag, [MarshalAs(UnmanagedType.Bool)] [In] bool fClearDirty, [MarshalAs(UnmanagedType.Bool)] [In] bool fSaveAllProperties);
		}

		// Token: 0x020005B0 RID: 1456
		[Guid("CF51ED10-62FE-11CF-BF86-00A0C9034836")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IQuickActivate
		{
			// Token: 0x06005FF7 RID: 24567
			void QuickActivate([In] UnsafeNativeMethods.tagQACONTAINER pQaContainer, [Out] UnsafeNativeMethods.tagQACONTROL pQaControl);

			// Token: 0x06005FF8 RID: 24568
			void SetContentExtent([In] NativeMethods.tagSIZEL pSizel);

			// Token: 0x06005FF9 RID: 24569
			void GetContentExtent([Out] NativeMethods.tagSIZEL pSizel);
		}

		// Token: 0x020005B1 RID: 1457
		[Guid("000C060B-0000-0000-C000-000000000046")]
		[ComImport]
		public class SMsoComponentManager
		{
			// Token: 0x06005FFA RID: 24570
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			public extern SMsoComponentManager();
		}

		// Token: 0x020005B2 RID: 1458
		[Guid("55272A00-42CB-11CE-8135-00AA004BB851")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IPropertyBag
		{
			// Token: 0x06005FFB RID: 24571
			[PreserveSig]
			int Read([MarshalAs(UnmanagedType.LPWStr)] [In] string pszPropName, [In] [Out] ref object pVar, [In] UnsafeNativeMethods.IErrorLog pErrorLog);

			// Token: 0x06005FFC RID: 24572
			[PreserveSig]
			int Write([MarshalAs(UnmanagedType.LPWStr)] [In] string pszPropName, [In] ref object pVar);
		}

		// Token: 0x020005B3 RID: 1459
		[Guid("3127CA40-446E-11CE-8135-00AA004BB851")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IErrorLog
		{
			// Token: 0x06005FFD RID: 24573
			void AddError([MarshalAs(UnmanagedType.LPWStr)] [In] string pszPropName_p0, [MarshalAs(UnmanagedType.Struct)] [In] NativeMethods.tagEXCEPINFO pExcepInfo_p1);
		}

		// Token: 0x020005B4 RID: 1460
		[Guid("00000109-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IPersistStream
		{
			// Token: 0x06005FFE RID: 24574
			void GetClassID(out Guid pClassId);

			// Token: 0x06005FFF RID: 24575
			[PreserveSig]
			int IsDirty();

			// Token: 0x06006000 RID: 24576
			void Load([MarshalAs(UnmanagedType.Interface)] [In] UnsafeNativeMethods.IStream pstm);

			// Token: 0x06006001 RID: 24577
			void Save([MarshalAs(UnmanagedType.Interface)] [In] UnsafeNativeMethods.IStream pstm, [MarshalAs(UnmanagedType.Bool)] [In] bool fClearDirty);

			// Token: 0x06006002 RID: 24578
			long GetSizeMax();
		}

		// Token: 0x020005B5 RID: 1461
		[SuppressUnmanagedCodeSecurity]
		[Guid("7FD52380-4E07-101B-AE2D-08002B2EC713")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IPersistStreamInit
		{
			// Token: 0x06006003 RID: 24579
			void GetClassID(out Guid pClassID);

			// Token: 0x06006004 RID: 24580
			[PreserveSig]
			int IsDirty();

			// Token: 0x06006005 RID: 24581
			void Load([MarshalAs(UnmanagedType.Interface)] [In] UnsafeNativeMethods.IStream pstm);

			// Token: 0x06006006 RID: 24582
			void Save([MarshalAs(UnmanagedType.Interface)] [In] UnsafeNativeMethods.IStream pstm, [MarshalAs(UnmanagedType.Bool)] [In] bool fClearDirty);

			// Token: 0x06006007 RID: 24583
			void GetSizeMax([MarshalAs(UnmanagedType.LPArray)] [Out] long pcbSize);

			// Token: 0x06006008 RID: 24584
			void InitNew();
		}

		// Token: 0x020005B6 RID: 1462
		[SuppressUnmanagedCodeSecurity]
		[Guid("B196B286-BAB4-101A-B69C-00AA00341D07")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IConnectionPoint
		{
			// Token: 0x06006009 RID: 24585
			[PreserveSig]
			int GetConnectionInterface(out Guid iid);

			// Token: 0x0600600A RID: 24586
			[PreserveSig]
			int GetConnectionPointContainer([MarshalAs(UnmanagedType.Interface)] ref UnsafeNativeMethods.IConnectionPointContainer pContainer);

			// Token: 0x0600600B RID: 24587
			[PreserveSig]
			int Advise([MarshalAs(UnmanagedType.Interface)] [In] object pUnkSink, ref int cookie);

			// Token: 0x0600600C RID: 24588
			[PreserveSig]
			int Unadvise(int cookie);

			// Token: 0x0600600D RID: 24589
			[PreserveSig]
			int EnumConnections(out object pEnum);
		}

		// Token: 0x020005B7 RID: 1463
		[Guid("0000010A-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IPersistStorage
		{
			// Token: 0x0600600E RID: 24590
			void GetClassID(out Guid pClassID);

			// Token: 0x0600600F RID: 24591
			[PreserveSig]
			int IsDirty();

			// Token: 0x06006010 RID: 24592
			void InitNew(UnsafeNativeMethods.IStorage pstg);

			// Token: 0x06006011 RID: 24593
			[PreserveSig]
			int Load(UnsafeNativeMethods.IStorage pstg);

			// Token: 0x06006012 RID: 24594
			void Save(UnsafeNativeMethods.IStorage pStgSave, bool fSameAsLoad);

			// Token: 0x06006013 RID: 24595
			void SaveCompleted(UnsafeNativeMethods.IStorage pStgNew);

			// Token: 0x06006014 RID: 24596
			void HandsOffStorage();
		}

		// Token: 0x020005B8 RID: 1464
		[Guid("00020404-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IEnumVariant
		{
			// Token: 0x06006015 RID: 24597
			[PreserveSig]
			int Next([MarshalAs(UnmanagedType.U4)] [In] int celt, [In] [Out] IntPtr rgvar, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] pceltFetched);

			// Token: 0x06006016 RID: 24598
			void Skip([MarshalAs(UnmanagedType.U4)] [In] int celt);

			// Token: 0x06006017 RID: 24599
			void Reset();

			// Token: 0x06006018 RID: 24600
			void Clone([MarshalAs(UnmanagedType.LPArray)] [Out] UnsafeNativeMethods.IEnumVariant[] ppenum);
		}

		// Token: 0x020005B9 RID: 1465
		[Guid("00000104-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IEnumOLEVERB
		{
			// Token: 0x06006019 RID: 24601
			[PreserveSig]
			int Next([MarshalAs(UnmanagedType.U4)] int celt, [Out] NativeMethods.tagOLEVERB rgelt, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] pceltFetched);

			// Token: 0x0600601A RID: 24602
			[PreserveSig]
			int Skip([MarshalAs(UnmanagedType.U4)] [In] int celt);

			// Token: 0x0600601B RID: 24603
			void Reset();

			// Token: 0x0600601C RID: 24604
			void Clone(out UnsafeNativeMethods.IEnumOLEVERB ppenum);
		}

		// Token: 0x020005BA RID: 1466
		[SuppressUnmanagedCodeSecurity]
		[Guid("00bb2762-6a77-11d0-a535-00c04fd7d062")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IAutoComplete
		{
			// Token: 0x0600601D RID: 24605
			int Init([In] HandleRef hwndEdit, [In] IEnumString punkACL, [In] string pwszRegKeyPath, [In] string pwszQuickComplete);

			// Token: 0x0600601E RID: 24606
			void Enable([In] bool fEnable);
		}

		// Token: 0x020005BB RID: 1467
		[SuppressUnmanagedCodeSecurity]
		[Guid("EAC04BC0-3791-11d2-BB95-0060977B464C")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IAutoComplete2
		{
			// Token: 0x0600601F RID: 24607
			int Init([In] HandleRef hwndEdit, [In] IEnumString punkACL, [In] string pwszRegKeyPath, [In] string pwszQuickComplete);

			// Token: 0x06006020 RID: 24608
			void Enable([In] bool fEnable);

			// Token: 0x06006021 RID: 24609
			int SetOptions([In] int dwFlag);

			// Token: 0x06006022 RID: 24610
			void GetOptions([Out] IntPtr pdwFlag);
		}

		// Token: 0x020005BC RID: 1468
		[SuppressUnmanagedCodeSecurity]
		[Guid("0000000C-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IStream
		{
			// Token: 0x06006023 RID: 24611
			int Read(IntPtr buf, int len);

			// Token: 0x06006024 RID: 24612
			int Write(IntPtr buf, int len);

			// Token: 0x06006025 RID: 24613
			[return: MarshalAs(UnmanagedType.I8)]
			long Seek([MarshalAs(UnmanagedType.I8)] [In] long dlibMove, int dwOrigin);

			// Token: 0x06006026 RID: 24614
			void SetSize([MarshalAs(UnmanagedType.I8)] [In] long libNewSize);

			// Token: 0x06006027 RID: 24615
			[return: MarshalAs(UnmanagedType.I8)]
			long CopyTo([MarshalAs(UnmanagedType.Interface)] [In] UnsafeNativeMethods.IStream pstm, [MarshalAs(UnmanagedType.I8)] [In] long cb, [MarshalAs(UnmanagedType.LPArray)] [Out] long[] pcbRead);

			// Token: 0x06006028 RID: 24616
			void Commit(int grfCommitFlags);

			// Token: 0x06006029 RID: 24617
			void Revert();

			// Token: 0x0600602A RID: 24618
			void LockRegion([MarshalAs(UnmanagedType.I8)] [In] long libOffset, [MarshalAs(UnmanagedType.I8)] [In] long cb, int dwLockType);

			// Token: 0x0600602B RID: 24619
			void UnlockRegion([MarshalAs(UnmanagedType.I8)] [In] long libOffset, [MarshalAs(UnmanagedType.I8)] [In] long cb, int dwLockType);

			// Token: 0x0600602C RID: 24620
			void Stat([Out] NativeMethods.STATSTG pStatstg, int grfStatFlag);

			// Token: 0x0600602D RID: 24621
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.IStream Clone();
		}

		// Token: 0x020005BD RID: 1469
		public abstract class CharBuffer
		{
			// Token: 0x0600602E RID: 24622 RVA: 0x001677B4 File Offset: 0x001659B4
			public static UnsafeNativeMethods.CharBuffer CreateBuffer(int size)
			{
				if (Marshal.SystemDefaultCharSize == 1)
				{
					return new UnsafeNativeMethods.AnsiCharBuffer(size);
				}
				return new UnsafeNativeMethods.UnicodeCharBuffer(size);
			}

			// Token: 0x0600602F RID: 24623
			public abstract IntPtr AllocCoTaskMem();

			// Token: 0x06006030 RID: 24624
			public abstract string GetString();

			// Token: 0x06006031 RID: 24625
			public abstract void PutCoTaskMem(IntPtr ptr);

			// Token: 0x06006032 RID: 24626
			public abstract void PutString(string s);
		}

		// Token: 0x020005BE RID: 1470
		public class AnsiCharBuffer : UnsafeNativeMethods.CharBuffer
		{
			// Token: 0x06006034 RID: 24628 RVA: 0x001677CB File Offset: 0x001659CB
			public AnsiCharBuffer(int size)
			{
				this.buffer = new byte[size];
			}

			// Token: 0x06006035 RID: 24629 RVA: 0x001677E0 File Offset: 0x001659E0
			public override IntPtr AllocCoTaskMem()
			{
				IntPtr intPtr = Marshal.AllocCoTaskMem(this.buffer.Length);
				Marshal.Copy(this.buffer, 0, intPtr, this.buffer.Length);
				return intPtr;
			}

			// Token: 0x06006036 RID: 24630 RVA: 0x00167814 File Offset: 0x00165A14
			public override string GetString()
			{
				int num = this.offset;
				while (num < this.buffer.Length && this.buffer[num] != 0)
				{
					num++;
				}
				string @string = Encoding.Default.GetString(this.buffer, this.offset, num - this.offset);
				if (num < this.buffer.Length)
				{
					num++;
				}
				this.offset = num;
				return @string;
			}

			// Token: 0x06006037 RID: 24631 RVA: 0x00167879 File Offset: 0x00165A79
			public override void PutCoTaskMem(IntPtr ptr)
			{
				Marshal.Copy(ptr, this.buffer, 0, this.buffer.Length);
				this.offset = 0;
			}

			// Token: 0x06006038 RID: 24632 RVA: 0x00167898 File Offset: 0x00165A98
			public override void PutString(string s)
			{
				byte[] bytes = Encoding.Default.GetBytes(s);
				int num = Math.Min(bytes.Length, this.buffer.Length - this.offset);
				Array.Copy(bytes, 0, this.buffer, this.offset, num);
				this.offset += num;
				if (this.offset < this.buffer.Length)
				{
					byte[] array = this.buffer;
					int num2 = this.offset;
					this.offset = num2 + 1;
					array[num2] = 0;
				}
			}

			// Token: 0x04003838 RID: 14392
			internal byte[] buffer;

			// Token: 0x04003839 RID: 14393
			internal int offset;
		}

		// Token: 0x020005BF RID: 1471
		public class UnicodeCharBuffer : UnsafeNativeMethods.CharBuffer
		{
			// Token: 0x06006039 RID: 24633 RVA: 0x00167914 File Offset: 0x00165B14
			public UnicodeCharBuffer(int size)
			{
				this.buffer = new char[size];
			}

			// Token: 0x0600603A RID: 24634 RVA: 0x00167928 File Offset: 0x00165B28
			public override IntPtr AllocCoTaskMem()
			{
				IntPtr intPtr = Marshal.AllocCoTaskMem(this.buffer.Length * 2);
				Marshal.Copy(this.buffer, 0, intPtr, this.buffer.Length);
				return intPtr;
			}

			// Token: 0x0600603B RID: 24635 RVA: 0x0016795C File Offset: 0x00165B5C
			public override string GetString()
			{
				int num = this.offset;
				while (num < this.buffer.Length && this.buffer[num] != '\0')
				{
					num++;
				}
				string text = new string(this.buffer, this.offset, num - this.offset);
				if (num < this.buffer.Length)
				{
					num++;
				}
				this.offset = num;
				return text;
			}

			// Token: 0x0600603C RID: 24636 RVA: 0x001679BC File Offset: 0x00165BBC
			public override void PutCoTaskMem(IntPtr ptr)
			{
				Marshal.Copy(ptr, this.buffer, 0, this.buffer.Length);
				this.offset = 0;
			}

			// Token: 0x0600603D RID: 24637 RVA: 0x001679DC File Offset: 0x00165BDC
			public override void PutString(string s)
			{
				int num = Math.Min(s.Length, this.buffer.Length - this.offset);
				s.CopyTo(0, this.buffer, this.offset, num);
				this.offset += num;
				if (this.offset < this.buffer.Length)
				{
					char[] array = this.buffer;
					int num2 = this.offset;
					this.offset = num2 + 1;
					array[num2] = 0;
				}
			}

			// Token: 0x0400383A RID: 14394
			internal char[] buffer;

			// Token: 0x0400383B RID: 14395
			internal int offset;
		}

		// Token: 0x020005C0 RID: 1472
		public class ComStreamFromDataStream : UnsafeNativeMethods.IStream
		{
			// Token: 0x0600603E RID: 24638 RVA: 0x00167A4F File Offset: 0x00165C4F
			public ComStreamFromDataStream(Stream dataStream)
			{
				if (dataStream == null)
				{
					throw new ArgumentNullException("dataStream");
				}
				this.dataStream = dataStream;
			}

			// Token: 0x0600603F RID: 24639 RVA: 0x00167A74 File Offset: 0x00165C74
			private void ActualizeVirtualPosition()
			{
				if (this.virtualPosition == -1L)
				{
					return;
				}
				if (this.virtualPosition > this.dataStream.Length)
				{
					this.dataStream.SetLength(this.virtualPosition);
				}
				this.dataStream.Position = this.virtualPosition;
				this.virtualPosition = -1L;
			}

			// Token: 0x06006040 RID: 24640 RVA: 0x00167AC9 File Offset: 0x00165CC9
			public UnsafeNativeMethods.IStream Clone()
			{
				UnsafeNativeMethods.ComStreamFromDataStream.NotImplemented();
				return null;
			}

			// Token: 0x06006041 RID: 24641 RVA: 0x00167AD1 File Offset: 0x00165CD1
			public void Commit(int grfCommitFlags)
			{
				this.dataStream.Flush();
				this.ActualizeVirtualPosition();
			}

			// Token: 0x06006042 RID: 24642 RVA: 0x00167AE4 File Offset: 0x00165CE4
			public long CopyTo(UnsafeNativeMethods.IStream pstm, long cb, long[] pcbRead)
			{
				int num = 4096;
				IntPtr intPtr = Marshal.AllocHGlobal(num);
				if (intPtr == IntPtr.Zero)
				{
					throw new OutOfMemoryException();
				}
				long num2 = 0L;
				try
				{
					while (num2 < cb)
					{
						int num3 = num;
						if (num2 + (long)num3 > cb)
						{
							num3 = (int)(cb - num2);
						}
						int num4 = this.Read(intPtr, num3);
						if (num4 == 0)
						{
							break;
						}
						if (pstm.Write(intPtr, num4) != num4)
						{
							throw UnsafeNativeMethods.ComStreamFromDataStream.EFail("Wrote an incorrect number of bytes");
						}
						num2 += (long)num4;
					}
				}
				finally
				{
					Marshal.FreeHGlobal(intPtr);
				}
				if (pcbRead != null && pcbRead.Length != 0)
				{
					pcbRead[0] = num2;
				}
				return num2;
			}

			// Token: 0x06006043 RID: 24643 RVA: 0x00167B7C File Offset: 0x00165D7C
			public Stream GetDataStream()
			{
				return this.dataStream;
			}

			// Token: 0x06006044 RID: 24644 RVA: 0x000070A6 File Offset: 0x000052A6
			public void LockRegion(long libOffset, long cb, int dwLockType)
			{
			}

			// Token: 0x06006045 RID: 24645 RVA: 0x00167B84 File Offset: 0x00165D84
			protected static ExternalException EFail(string msg)
			{
				ExternalException ex = new ExternalException(msg, -2147467259);
				throw ex;
			}

			// Token: 0x06006046 RID: 24646 RVA: 0x00167BA0 File Offset: 0x00165DA0
			protected static void NotImplemented()
			{
				ExternalException ex = new ExternalException(SR.GetString("UnsafeNativeMethodsNotImplemented"), -2147467263);
				throw ex;
			}

			// Token: 0x06006047 RID: 24647 RVA: 0x00167BC4 File Offset: 0x00165DC4
			public int Read(IntPtr buf, int length)
			{
				byte[] array = new byte[length];
				int num = this.Read(array, length);
				Marshal.Copy(array, 0, buf, num);
				return num;
			}

			// Token: 0x06006048 RID: 24648 RVA: 0x00167BEB File Offset: 0x00165DEB
			public int Read(byte[] buffer, int length)
			{
				this.ActualizeVirtualPosition();
				return this.dataStream.Read(buffer, 0, length);
			}

			// Token: 0x06006049 RID: 24649 RVA: 0x00167C01 File Offset: 0x00165E01
			public void Revert()
			{
				UnsafeNativeMethods.ComStreamFromDataStream.NotImplemented();
			}

			// Token: 0x0600604A RID: 24650 RVA: 0x00167C08 File Offset: 0x00165E08
			public long Seek(long offset, int origin)
			{
				long position = this.virtualPosition;
				if (this.virtualPosition == -1L)
				{
					position = this.dataStream.Position;
				}
				long length = this.dataStream.Length;
				switch (origin)
				{
				case 0:
					if (offset <= length)
					{
						this.dataStream.Position = offset;
						this.virtualPosition = -1L;
					}
					else
					{
						this.virtualPosition = offset;
					}
					break;
				case 1:
					if (offset + position <= length)
					{
						this.dataStream.Position = position + offset;
						this.virtualPosition = -1L;
					}
					else
					{
						this.virtualPosition = offset + position;
					}
					break;
				case 2:
					if (offset <= 0L)
					{
						this.dataStream.Position = length + offset;
						this.virtualPosition = -1L;
					}
					else
					{
						this.virtualPosition = length + offset;
					}
					break;
				}
				if (this.virtualPosition != -1L)
				{
					return this.virtualPosition;
				}
				return this.dataStream.Position;
			}

			// Token: 0x0600604B RID: 24651 RVA: 0x00167CE0 File Offset: 0x00165EE0
			public void SetSize(long value)
			{
				this.dataStream.SetLength(value);
			}

			// Token: 0x0600604C RID: 24652 RVA: 0x00167CEE File Offset: 0x00165EEE
			public void Stat(NativeMethods.STATSTG pstatstg, int grfStatFlag)
			{
				pstatstg.type = 2;
				pstatstg.cbSize = this.dataStream.Length;
				pstatstg.grfLocksSupported = 2;
			}

			// Token: 0x0600604D RID: 24653 RVA: 0x000070A6 File Offset: 0x000052A6
			public void UnlockRegion(long libOffset, long cb, int dwLockType)
			{
			}

			// Token: 0x0600604E RID: 24654 RVA: 0x00167D10 File Offset: 0x00165F10
			public int Write(IntPtr buf, int length)
			{
				byte[] array = new byte[length];
				Marshal.Copy(buf, array, 0, length);
				return this.Write(array, length);
			}

			// Token: 0x0600604F RID: 24655 RVA: 0x00167D35 File Offset: 0x00165F35
			public int Write(byte[] buffer, int length)
			{
				this.ActualizeVirtualPosition();
				this.dataStream.Write(buffer, 0, length);
				return length;
			}

			// Token: 0x0400383C RID: 14396
			protected Stream dataStream;

			// Token: 0x0400383D RID: 14397
			private long virtualPosition = -1L;
		}

		// Token: 0x020005C1 RID: 1473
		[Guid("0000000B-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IStorage
		{
			// Token: 0x06006050 RID: 24656
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.IStream CreateStream([MarshalAs(UnmanagedType.BStr)] [In] string pwcsName, [MarshalAs(UnmanagedType.U4)] [In] int grfMode, [MarshalAs(UnmanagedType.U4)] [In] int reserved1, [MarshalAs(UnmanagedType.U4)] [In] int reserved2);

			// Token: 0x06006051 RID: 24657
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.IStream OpenStream([MarshalAs(UnmanagedType.BStr)] [In] string pwcsName, IntPtr reserved1, [MarshalAs(UnmanagedType.U4)] [In] int grfMode, [MarshalAs(UnmanagedType.U4)] [In] int reserved2);

			// Token: 0x06006052 RID: 24658
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.IStorage CreateStorage([MarshalAs(UnmanagedType.BStr)] [In] string pwcsName, [MarshalAs(UnmanagedType.U4)] [In] int grfMode, [MarshalAs(UnmanagedType.U4)] [In] int reserved1, [MarshalAs(UnmanagedType.U4)] [In] int reserved2);

			// Token: 0x06006053 RID: 24659
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.IStorage OpenStorage([MarshalAs(UnmanagedType.BStr)] [In] string pwcsName, IntPtr pstgPriority, [MarshalAs(UnmanagedType.U4)] [In] int grfMode, IntPtr snbExclude, [MarshalAs(UnmanagedType.U4)] [In] int reserved);

			// Token: 0x06006054 RID: 24660
			void CopyTo(int ciidExclude, [MarshalAs(UnmanagedType.LPArray)] [In] Guid[] pIIDExclude, IntPtr snbExclude, [MarshalAs(UnmanagedType.Interface)] [In] UnsafeNativeMethods.IStorage stgDest);

			// Token: 0x06006055 RID: 24661
			void MoveElementTo([MarshalAs(UnmanagedType.BStr)] [In] string pwcsName, [MarshalAs(UnmanagedType.Interface)] [In] UnsafeNativeMethods.IStorage stgDest, [MarshalAs(UnmanagedType.BStr)] [In] string pwcsNewName, [MarshalAs(UnmanagedType.U4)] [In] int grfFlags);

			// Token: 0x06006056 RID: 24662
			void Commit(int grfCommitFlags);

			// Token: 0x06006057 RID: 24663
			void Revert();

			// Token: 0x06006058 RID: 24664
			void EnumElements([MarshalAs(UnmanagedType.U4)] [In] int reserved1, IntPtr reserved2, [MarshalAs(UnmanagedType.U4)] [In] int reserved3, [MarshalAs(UnmanagedType.Interface)] out object ppVal);

			// Token: 0x06006059 RID: 24665
			void DestroyElement([MarshalAs(UnmanagedType.BStr)] [In] string pwcsName);

			// Token: 0x0600605A RID: 24666
			void RenameElement([MarshalAs(UnmanagedType.BStr)] [In] string pwcsOldName, [MarshalAs(UnmanagedType.BStr)] [In] string pwcsNewName);

			// Token: 0x0600605B RID: 24667
			void SetElementTimes([MarshalAs(UnmanagedType.BStr)] [In] string pwcsName, [In] NativeMethods.FILETIME pctime, [In] NativeMethods.FILETIME patime, [In] NativeMethods.FILETIME pmtime);

			// Token: 0x0600605C RID: 24668
			void SetClass([In] ref Guid clsid);

			// Token: 0x0600605D RID: 24669
			void SetStateBits(int grfStateBits, int grfMask);

			// Token: 0x0600605E RID: 24670
			void Stat([Out] NativeMethods.STATSTG pStatStg, int grfStatFlag);
		}

		// Token: 0x020005C2 RID: 1474
		[Guid("B196B28F-BAB4-101A-B69C-00AA00341D07")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IClassFactory2
		{
			// Token: 0x0600605F RID: 24671
			void CreateInstance([MarshalAs(UnmanagedType.Interface)] [In] object unused, [In] ref Guid refiid, [MarshalAs(UnmanagedType.LPArray)] [Out] object[] ppunk);

			// Token: 0x06006060 RID: 24672
			void LockServer(int fLock);

			// Token: 0x06006061 RID: 24673
			void GetLicInfo([Out] NativeMethods.tagLICINFO licInfo);

			// Token: 0x06006062 RID: 24674
			void RequestLicKey([MarshalAs(UnmanagedType.U4)] [In] int dwReserved, [MarshalAs(UnmanagedType.LPArray)] [Out] string[] pBstrKey);

			// Token: 0x06006063 RID: 24675
			void CreateInstanceLic([MarshalAs(UnmanagedType.Interface)] [In] object pUnkOuter, [MarshalAs(UnmanagedType.Interface)] [In] object pUnkReserved, [In] ref Guid riid, [MarshalAs(UnmanagedType.BStr)] [In] string bstrKey, [MarshalAs(UnmanagedType.Interface)] out object ppVal);
		}

		// Token: 0x020005C3 RID: 1475
		[SuppressUnmanagedCodeSecurity]
		[Guid("B196B284-BAB4-101A-B69C-00AA00341D07")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IConnectionPointContainer
		{
			// Token: 0x06006064 RID: 24676
			[return: MarshalAs(UnmanagedType.Interface)]
			object EnumConnectionPoints();

			// Token: 0x06006065 RID: 24677
			[PreserveSig]
			int FindConnectionPoint([In] ref Guid guid, [MarshalAs(UnmanagedType.Interface)] out UnsafeNativeMethods.IConnectionPoint ppCP);
		}

		// Token: 0x020005C4 RID: 1476
		[Guid("B196B285-BAB4-101A-B69C-00AA00341D07")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IEnumConnectionPoints
		{
			// Token: 0x06006066 RID: 24678
			[PreserveSig]
			int Next(int cConnections, out UnsafeNativeMethods.IConnectionPoint pCp, out int pcFetched);

			// Token: 0x06006067 RID: 24679
			[PreserveSig]
			int Skip(int cSkip);

			// Token: 0x06006068 RID: 24680
			void Reset();

			// Token: 0x06006069 RID: 24681
			UnsafeNativeMethods.IEnumConnectionPoints Clone();
		}

		// Token: 0x020005C5 RID: 1477
		[Guid("00020400-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IDispatch
		{
			// Token: 0x0600606A RID: 24682
			int GetTypeInfoCount();

			// Token: 0x0600606B RID: 24683
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.ITypeInfo GetTypeInfo([MarshalAs(UnmanagedType.U4)] [In] int iTInfo, [MarshalAs(UnmanagedType.U4)] [In] int lcid);

			// Token: 0x0600606C RID: 24684
			[PreserveSig]
			int GetIDsOfNames([In] ref Guid riid, [MarshalAs(UnmanagedType.LPArray)] [In] string[] rgszNames, [MarshalAs(UnmanagedType.U4)] [In] int cNames, [MarshalAs(UnmanagedType.U4)] [In] int lcid, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] rgDispId);

			// Token: 0x0600606D RID: 24685
			[PreserveSig]
			int Invoke(int dispIdMember, [In] ref Guid riid, [MarshalAs(UnmanagedType.U4)] [In] int lcid, [MarshalAs(UnmanagedType.U4)] [In] int dwFlags, [In] [Out] NativeMethods.tagDISPPARAMS pDispParams, [MarshalAs(UnmanagedType.LPArray)] [Out] object[] pVarResult, [In] [Out] NativeMethods.tagEXCEPINFO pExcepInfo, [MarshalAs(UnmanagedType.LPArray)] [Out] IntPtr[] pArgErr);
		}

		// Token: 0x020005C6 RID: 1478
		[Guid("00020401-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface ITypeInfo
		{
			// Token: 0x0600606E RID: 24686
			[PreserveSig]
			int GetTypeAttr(ref IntPtr pTypeAttr);

			// Token: 0x0600606F RID: 24687
			[PreserveSig]
			int GetTypeComp([MarshalAs(UnmanagedType.LPArray)] [Out] UnsafeNativeMethods.ITypeComp[] ppTComp);

			// Token: 0x06006070 RID: 24688
			[PreserveSig]
			int GetFuncDesc([MarshalAs(UnmanagedType.U4)] [In] int index, ref IntPtr pFuncDesc);

			// Token: 0x06006071 RID: 24689
			[PreserveSig]
			int GetVarDesc([MarshalAs(UnmanagedType.U4)] [In] int index, ref IntPtr pVarDesc);

			// Token: 0x06006072 RID: 24690
			[PreserveSig]
			int GetNames(int memid, [MarshalAs(UnmanagedType.LPArray)] [Out] string[] rgBstrNames, [MarshalAs(UnmanagedType.U4)] [In] int cMaxNames, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] pcNames);

			// Token: 0x06006073 RID: 24691
			[PreserveSig]
			int GetRefTypeOfImplType([MarshalAs(UnmanagedType.U4)] [In] int index, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] pRefType);

			// Token: 0x06006074 RID: 24692
			[PreserveSig]
			int GetImplTypeFlags([MarshalAs(UnmanagedType.U4)] [In] int index, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] pImplTypeFlags);

			// Token: 0x06006075 RID: 24693
			[PreserveSig]
			int GetIDsOfNames(IntPtr rgszNames, int cNames, IntPtr pMemId);

			// Token: 0x06006076 RID: 24694
			[PreserveSig]
			int Invoke();

			// Token: 0x06006077 RID: 24695
			[PreserveSig]
			int GetDocumentation(int memid, ref string pBstrName, ref string pBstrDocString, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] pdwHelpContext, [MarshalAs(UnmanagedType.LPArray)] [Out] string[] pBstrHelpFile);

			// Token: 0x06006078 RID: 24696
			[PreserveSig]
			int GetDllEntry(int memid, NativeMethods.tagINVOKEKIND invkind, [MarshalAs(UnmanagedType.LPArray)] [Out] string[] pBstrDllName, [MarshalAs(UnmanagedType.LPArray)] [Out] string[] pBstrName, [MarshalAs(UnmanagedType.LPArray)] [Out] short[] pwOrdinal);

			// Token: 0x06006079 RID: 24697
			[PreserveSig]
			int GetRefTypeInfo(IntPtr hreftype, ref UnsafeNativeMethods.ITypeInfo pTypeInfo);

			// Token: 0x0600607A RID: 24698
			[PreserveSig]
			int AddressOfMember();

			// Token: 0x0600607B RID: 24699
			[PreserveSig]
			int CreateInstance([In] ref Guid riid, [MarshalAs(UnmanagedType.LPArray)] [Out] object[] ppvObj);

			// Token: 0x0600607C RID: 24700
			[PreserveSig]
			int GetMops(int memid, [MarshalAs(UnmanagedType.LPArray)] [Out] string[] pBstrMops);

			// Token: 0x0600607D RID: 24701
			[PreserveSig]
			int GetContainingTypeLib([MarshalAs(UnmanagedType.LPArray)] [Out] UnsafeNativeMethods.ITypeLib[] ppTLib, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] pIndex);

			// Token: 0x0600607E RID: 24702
			[PreserveSig]
			void ReleaseTypeAttr(IntPtr typeAttr);

			// Token: 0x0600607F RID: 24703
			[PreserveSig]
			void ReleaseFuncDesc(IntPtr funcDesc);

			// Token: 0x06006080 RID: 24704
			[PreserveSig]
			void ReleaseVarDesc(IntPtr varDesc);
		}

		// Token: 0x020005C7 RID: 1479
		[Guid("00020403-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface ITypeComp
		{
			// Token: 0x06006081 RID: 24705
			void RemoteBind([MarshalAs(UnmanagedType.LPWStr)] [In] string szName, [MarshalAs(UnmanagedType.U4)] [In] int lHashVal, [MarshalAs(UnmanagedType.U2)] [In] short wFlags, [MarshalAs(UnmanagedType.LPArray)] [Out] UnsafeNativeMethods.ITypeInfo[] ppTInfo, [MarshalAs(UnmanagedType.LPArray)] [Out] NativeMethods.tagDESCKIND[] pDescKind, [MarshalAs(UnmanagedType.LPArray)] [Out] NativeMethods.tagFUNCDESC[] ppFuncDesc, [MarshalAs(UnmanagedType.LPArray)] [Out] NativeMethods.tagVARDESC[] ppVarDesc, [MarshalAs(UnmanagedType.LPArray)] [Out] UnsafeNativeMethods.ITypeComp[] ppTypeComp, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] pDummy);

			// Token: 0x06006082 RID: 24706
			void RemoteBindType([MarshalAs(UnmanagedType.LPWStr)] [In] string szName, [MarshalAs(UnmanagedType.U4)] [In] int lHashVal, [MarshalAs(UnmanagedType.LPArray)] [Out] UnsafeNativeMethods.ITypeInfo[] ppTInfo);
		}

		// Token: 0x020005C8 RID: 1480
		[Guid("00020402-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface ITypeLib
		{
			// Token: 0x06006083 RID: 24707
			void RemoteGetTypeInfoCount([MarshalAs(UnmanagedType.LPArray)] [Out] int[] pcTInfo);

			// Token: 0x06006084 RID: 24708
			void GetTypeInfo([MarshalAs(UnmanagedType.U4)] [In] int index, [MarshalAs(UnmanagedType.LPArray)] [Out] UnsafeNativeMethods.ITypeInfo[] ppTInfo);

			// Token: 0x06006085 RID: 24709
			void GetTypeInfoType([MarshalAs(UnmanagedType.U4)] [In] int index, [MarshalAs(UnmanagedType.LPArray)] [Out] NativeMethods.tagTYPEKIND[] pTKind);

			// Token: 0x06006086 RID: 24710
			void GetTypeInfoOfGuid([In] ref Guid guid, [MarshalAs(UnmanagedType.LPArray)] [Out] UnsafeNativeMethods.ITypeInfo[] ppTInfo);

			// Token: 0x06006087 RID: 24711
			void RemoteGetLibAttr(IntPtr ppTLibAttr, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] pDummy);

			// Token: 0x06006088 RID: 24712
			void GetTypeComp([MarshalAs(UnmanagedType.LPArray)] [Out] UnsafeNativeMethods.ITypeComp[] ppTComp);

			// Token: 0x06006089 RID: 24713
			void RemoteGetDocumentation(int index, [MarshalAs(UnmanagedType.U4)] [In] int refPtrFlags, [MarshalAs(UnmanagedType.LPArray)] [Out] string[] pBstrName, [MarshalAs(UnmanagedType.LPArray)] [Out] string[] pBstrDocString, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] pdwHelpContext, [MarshalAs(UnmanagedType.LPArray)] [Out] string[] pBstrHelpFile);

			// Token: 0x0600608A RID: 24714
			void RemoteIsName([MarshalAs(UnmanagedType.LPWStr)] [In] string szNameBuf, [MarshalAs(UnmanagedType.U4)] [In] int lHashVal, [MarshalAs(UnmanagedType.LPArray)] [Out] IntPtr[] pfName, [MarshalAs(UnmanagedType.LPArray)] [Out] string[] pBstrLibName);

			// Token: 0x0600608B RID: 24715
			void RemoteFindName([MarshalAs(UnmanagedType.LPWStr)] [In] string szNameBuf, [MarshalAs(UnmanagedType.U4)] [In] int lHashVal, [MarshalAs(UnmanagedType.LPArray)] [Out] UnsafeNativeMethods.ITypeInfo[] ppTInfo, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] rgMemId, [MarshalAs(UnmanagedType.LPArray)] [In] [Out] short[] pcFound, [MarshalAs(UnmanagedType.LPArray)] [Out] string[] pBstrLibName);

			// Token: 0x0600608C RID: 24716
			void LocalReleaseTLibAttr();
		}

		// Token: 0x020005C9 RID: 1481
		[Guid("DF0B3D60-548F-101B-8E65-08002B2BD119")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface ISupportErrorInfo
		{
			// Token: 0x0600608D RID: 24717
			int InterfaceSupportsErrorInfo([In] ref Guid riid);
		}

		// Token: 0x020005CA RID: 1482
		[Guid("1CF2B120-547D-101B-8E65-08002B2BD119")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IErrorInfo
		{
			// Token: 0x0600608E RID: 24718
			[SuppressUnmanagedCodeSecurity]
			[PreserveSig]
			int GetGUID(out Guid pguid);

			// Token: 0x0600608F RID: 24719
			[SuppressUnmanagedCodeSecurity]
			[PreserveSig]
			int GetSource([MarshalAs(UnmanagedType.BStr)] [In] [Out] ref string pBstrSource);

			// Token: 0x06006090 RID: 24720
			[SuppressUnmanagedCodeSecurity]
			[PreserveSig]
			int GetDescription([MarshalAs(UnmanagedType.BStr)] [In] [Out] ref string pBstrDescription);

			// Token: 0x06006091 RID: 24721
			[SuppressUnmanagedCodeSecurity]
			[PreserveSig]
			int GetHelpFile([MarshalAs(UnmanagedType.BStr)] [In] [Out] ref string pBstrHelpFile);

			// Token: 0x06006092 RID: 24722
			[SuppressUnmanagedCodeSecurity]
			[PreserveSig]
			int GetHelpContext([MarshalAs(UnmanagedType.U4)] [In] [Out] ref int pdwHelpContext);
		}

		// Token: 0x020005CB RID: 1483
		[StructLayout(LayoutKind.Sequential)]
		public sealed class tagQACONTAINER
		{
			// Token: 0x0400383E RID: 14398
			[MarshalAs(UnmanagedType.U4)]
			public int cbSize = Marshal.SizeOf(typeof(UnsafeNativeMethods.tagQACONTAINER));

			// Token: 0x0400383F RID: 14399
			public UnsafeNativeMethods.IOleClientSite pClientSite;

			// Token: 0x04003840 RID: 14400
			[MarshalAs(UnmanagedType.Interface)]
			public object pAdviseSink;

			// Token: 0x04003841 RID: 14401
			public UnsafeNativeMethods.IPropertyNotifySink pPropertyNotifySink;

			// Token: 0x04003842 RID: 14402
			[MarshalAs(UnmanagedType.Interface)]
			public object pUnkEventSink;

			// Token: 0x04003843 RID: 14403
			[MarshalAs(UnmanagedType.U4)]
			public int dwAmbientFlags;

			// Token: 0x04003844 RID: 14404
			[MarshalAs(UnmanagedType.U4)]
			public uint colorFore;

			// Token: 0x04003845 RID: 14405
			[MarshalAs(UnmanagedType.U4)]
			public uint colorBack;

			// Token: 0x04003846 RID: 14406
			[MarshalAs(UnmanagedType.Interface)]
			public object pFont;

			// Token: 0x04003847 RID: 14407
			[MarshalAs(UnmanagedType.Interface)]
			public object pUndoMgr;

			// Token: 0x04003848 RID: 14408
			[MarshalAs(UnmanagedType.U4)]
			public int dwAppearance;

			// Token: 0x04003849 RID: 14409
			public int lcid;

			// Token: 0x0400384A RID: 14410
			public IntPtr hpal = IntPtr.Zero;

			// Token: 0x0400384B RID: 14411
			[MarshalAs(UnmanagedType.Interface)]
			public object pBindHost;
		}

		// Token: 0x020005CC RID: 1484
		[StructLayout(LayoutKind.Sequential)]
		public sealed class tagQACONTROL
		{
			// Token: 0x0400384C RID: 14412
			[MarshalAs(UnmanagedType.U4)]
			public int cbSize = Marshal.SizeOf(typeof(UnsafeNativeMethods.tagQACONTROL));

			// Token: 0x0400384D RID: 14413
			[MarshalAs(UnmanagedType.U4)]
			public int dwMiscStatus;

			// Token: 0x0400384E RID: 14414
			[MarshalAs(UnmanagedType.U4)]
			public int dwViewStatus;

			// Token: 0x0400384F RID: 14415
			[MarshalAs(UnmanagedType.U4)]
			public int dwEventCookie;

			// Token: 0x04003850 RID: 14416
			[MarshalAs(UnmanagedType.U4)]
			public int dwPropNotifyCookie;

			// Token: 0x04003851 RID: 14417
			[MarshalAs(UnmanagedType.U4)]
			public int dwPointerActivationPolicy;
		}

		// Token: 0x020005CD RID: 1485
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("E44C3566-915D-4070-99C6-047BFF5A08F5")]
		[ComVisible(true)]
		[ComImport]
		public interface ILegacyIAccessibleProvider
		{
			// Token: 0x06006095 RID: 24725
			void Select(int flagsSelect);

			// Token: 0x06006096 RID: 24726
			void DoDefaultAction();

			// Token: 0x06006097 RID: 24727
			void SetValue([MarshalAs(UnmanagedType.LPWStr)] string szValue);

			// Token: 0x06006098 RID: 24728
			[return: MarshalAs(UnmanagedType.Interface)]
			IAccessible GetIAccessible();

			// Token: 0x170014BC RID: 5308
			// (get) Token: 0x06006099 RID: 24729
			int ChildId { get; }

			// Token: 0x170014BD RID: 5309
			// (get) Token: 0x0600609A RID: 24730
			string Name { get; }

			// Token: 0x170014BE RID: 5310
			// (get) Token: 0x0600609B RID: 24731
			string Value { get; }

			// Token: 0x170014BF RID: 5311
			// (get) Token: 0x0600609C RID: 24732
			string Description { get; }

			// Token: 0x170014C0 RID: 5312
			// (get) Token: 0x0600609D RID: 24733
			uint Role { get; }

			// Token: 0x170014C1 RID: 5313
			// (get) Token: 0x0600609E RID: 24734
			uint State { get; }

			// Token: 0x170014C2 RID: 5314
			// (get) Token: 0x0600609F RID: 24735
			string Help { get; }

			// Token: 0x170014C3 RID: 5315
			// (get) Token: 0x060060A0 RID: 24736
			string KeyboardShortcut { get; }

			// Token: 0x060060A1 RID: 24737
			object[] GetSelection();

			// Token: 0x170014C4 RID: 5316
			// (get) Token: 0x060060A2 RID: 24738
			string DefaultAction { get; }
		}

		// Token: 0x020005CE RID: 1486
		[Guid("0000000A-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface ILockBytes
		{
			// Token: 0x060060A3 RID: 24739
			void ReadAt([MarshalAs(UnmanagedType.U8)] [In] long ulOffset, [Out] IntPtr pv, [MarshalAs(UnmanagedType.U4)] [In] int cb, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] pcbRead);

			// Token: 0x060060A4 RID: 24740
			void WriteAt([MarshalAs(UnmanagedType.U8)] [In] long ulOffset, IntPtr pv, [MarshalAs(UnmanagedType.U4)] [In] int cb, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] pcbWritten);

			// Token: 0x060060A5 RID: 24741
			void Flush();

			// Token: 0x060060A6 RID: 24742
			void SetSize([MarshalAs(UnmanagedType.U8)] [In] long cb);

			// Token: 0x060060A7 RID: 24743
			void LockRegion([MarshalAs(UnmanagedType.U8)] [In] long libOffset, [MarshalAs(UnmanagedType.U8)] [In] long cb, [MarshalAs(UnmanagedType.U4)] [In] int dwLockType);

			// Token: 0x060060A8 RID: 24744
			void UnlockRegion([MarshalAs(UnmanagedType.U8)] [In] long libOffset, [MarshalAs(UnmanagedType.U8)] [In] long cb, [MarshalAs(UnmanagedType.U4)] [In] int dwLockType);

			// Token: 0x060060A9 RID: 24745
			void Stat([Out] NativeMethods.STATSTG pstatstg, [MarshalAs(UnmanagedType.U4)] [In] int grfStatFlag);
		}

		// Token: 0x020005CF RID: 1487
		[SuppressUnmanagedCodeSecurity]
		[StructLayout(LayoutKind.Sequential)]
		public class OFNOTIFY
		{
			// Token: 0x04003852 RID: 14418
			public IntPtr hdr_hwndFrom = IntPtr.Zero;

			// Token: 0x04003853 RID: 14419
			public IntPtr hdr_idFrom = IntPtr.Zero;

			// Token: 0x04003854 RID: 14420
			public int hdr_code;

			// Token: 0x04003855 RID: 14421
			public IntPtr lpOFN = IntPtr.Zero;

			// Token: 0x04003856 RID: 14422
			public IntPtr pszFile = IntPtr.Zero;
		}

		// Token: 0x020005D0 RID: 1488
		// (Invoke) Token: 0x060060AC RID: 24748
		public delegate int BrowseCallbackProc(IntPtr hwnd, int msg, IntPtr lParam, IntPtr lpData);

		// Token: 0x020005D1 RID: 1489
		[Flags]
		public enum BrowseInfos
		{
			// Token: 0x04003858 RID: 14424
			NewDialogStyle = 64,
			// Token: 0x04003859 RID: 14425
			HideNewFolderButton = 512
		}

		// Token: 0x020005D2 RID: 1490
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class BROWSEINFO
		{
			// Token: 0x0400385A RID: 14426
			public IntPtr hwndOwner;

			// Token: 0x0400385B RID: 14427
			public IntPtr pidlRoot;

			// Token: 0x0400385C RID: 14428
			public IntPtr pszDisplayName;

			// Token: 0x0400385D RID: 14429
			public string lpszTitle;

			// Token: 0x0400385E RID: 14430
			public int ulFlags;

			// Token: 0x0400385F RID: 14431
			public UnsafeNativeMethods.BrowseCallbackProc lpfn;

			// Token: 0x04003860 RID: 14432
			public IntPtr lParam;

			// Token: 0x04003861 RID: 14433
			public int iImage;
		}

		// Token: 0x020005D3 RID: 1491
		[SuppressUnmanagedCodeSecurity]
		internal class Shell32
		{
			// Token: 0x060060B0 RID: 24752
			[DllImport("shell32.dll")]
			public static extern int SHGetSpecialFolderLocation(IntPtr hwnd, int csidl, ref IntPtr ppidl);

			// Token: 0x060060B1 RID: 24753
			[DllImport("shell32.dll", CharSet = CharSet.Auto)]
			private static extern bool SHGetPathFromIDListEx(IntPtr pidl, IntPtr pszPath, int cchPath, int flags);

			// Token: 0x060060B2 RID: 24754 RVA: 0x00167DC8 File Offset: 0x00165FC8
			public static bool SHGetPathFromIDListLongPath(IntPtr pidl, ref IntPtr pszPath)
			{
				int num = 1;
				int num2 = 260 * Marshal.SystemDefaultCharSize;
				int num3 = 260;
				bool flag;
				while (!(flag = UnsafeNativeMethods.Shell32.SHGetPathFromIDListEx(pidl, pszPath, num3, 0)) && num3 < 32767)
				{
					string text = Marshal.PtrToStringAuto(pszPath);
					if (text.Length != 0 && text.Length < num3)
					{
						break;
					}
					num += 2;
					num3 = ((num * num3 >= 32767) ? 32767 : (num * num3));
					pszPath = Marshal.ReAllocHGlobal(pszPath, (IntPtr)((num3 + 1) * Marshal.SystemDefaultCharSize));
				}
				return flag;
			}

			// Token: 0x060060B3 RID: 24755
			[DllImport("shell32.dll", CharSet = CharSet.Auto)]
			public static extern IntPtr SHBrowseForFolder([In] UnsafeNativeMethods.BROWSEINFO lpbi);

			// Token: 0x060060B4 RID: 24756
			[DllImport("shell32.dll")]
			public static extern int SHGetMalloc([MarshalAs(UnmanagedType.LPArray)] [Out] UnsafeNativeMethods.IMalloc[] ppMalloc);

			// Token: 0x060060B5 RID: 24757
			[DllImport("shell32.dll")]
			private static extern int SHGetKnownFolderPath(ref Guid rfid, uint dwFlags, IntPtr hToken, out IntPtr pszPath);

			// Token: 0x060060B6 RID: 24758 RVA: 0x00167E50 File Offset: 0x00166050
			public static int SHGetFolderPathEx(ref Guid rfid, uint dwFlags, IntPtr hToken, StringBuilder pszPath)
			{
				if (UnsafeNativeMethods.IsVista)
				{
					IntPtr zero = IntPtr.Zero;
					int num;
					if ((num = UnsafeNativeMethods.Shell32.SHGetKnownFolderPath(ref rfid, dwFlags, hToken, out zero)) == 0)
					{
						pszPath.Append(Marshal.PtrToStringAuto(zero));
						UnsafeNativeMethods.CoTaskMemFree(zero);
					}
					return num;
				}
				throw new NotSupportedException();
			}

			// Token: 0x060060B7 RID: 24759
			[DllImport("shell32.dll")]
			public static extern int SHCreateShellItem(IntPtr pidlParent, IntPtr psfParent, IntPtr pidl, out FileDialogNative.IShellItem ppsi);

			// Token: 0x060060B8 RID: 24760
			[DllImport("shell32.dll")]
			public static extern int SHILCreateFromPath([MarshalAs(UnmanagedType.LPWStr)] string pszPath, out IntPtr ppIdl, ref uint rgflnOut);
		}

		// Token: 0x020005D4 RID: 1492
		[Guid("00000002-0000-0000-c000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[SuppressUnmanagedCodeSecurity]
		[ComImport]
		public interface IMalloc
		{
			// Token: 0x060060BA RID: 24762
			[PreserveSig]
			IntPtr Alloc(int cb);

			// Token: 0x060060BB RID: 24763
			[PreserveSig]
			IntPtr Realloc(IntPtr pv, int cb);

			// Token: 0x060060BC RID: 24764
			[PreserveSig]
			void Free(IntPtr pv);

			// Token: 0x060060BD RID: 24765
			[PreserveSig]
			int GetSize(IntPtr pv);

			// Token: 0x060060BE RID: 24766
			[PreserveSig]
			int DidAlloc(IntPtr pv);

			// Token: 0x060060BF RID: 24767
			[PreserveSig]
			void HeapMinimize();
		}

		// Token: 0x020005D5 RID: 1493
		[Guid("00000126-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IRunnableObject
		{
			// Token: 0x060060C0 RID: 24768
			void GetRunningClass(out Guid guid);

			// Token: 0x060060C1 RID: 24769
			[PreserveSig]
			int Run(IntPtr lpBindContext);

			// Token: 0x060060C2 RID: 24770
			bool IsRunning();

			// Token: 0x060060C3 RID: 24771
			void LockRunning(bool fLock, bool fLastUnlockCloses);

			// Token: 0x060060C4 RID: 24772
			void SetContainedObject(bool fContained);
		}

		// Token: 0x020005D6 RID: 1494
		[ComVisible(true)]
		[Guid("B722BCC7-4E68-101B-A2BC-00AA00404770")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IOleDocumentSite
		{
			// Token: 0x060060C5 RID: 24773
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int ActivateMe([MarshalAs(UnmanagedType.Interface)] [In] UnsafeNativeMethods.IOleDocumentView pViewToActivate);
		}

		// Token: 0x020005D7 RID: 1495
		[ComVisible(true)]
		[Guid("B722BCC6-4E68-101B-A2BC-00AA00404770")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		public interface IOleDocumentView
		{
			// Token: 0x060060C6 RID: 24774
			void SetInPlaceSite([MarshalAs(UnmanagedType.Interface)] [In] UnsafeNativeMethods.IOleInPlaceSite pIPSite);

			// Token: 0x060060C7 RID: 24775
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.IOleInPlaceSite GetInPlaceSite();

			// Token: 0x060060C8 RID: 24776
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetDocument();

			// Token: 0x060060C9 RID: 24777
			void SetRect([In] ref NativeMethods.RECT prcView);

			// Token: 0x060060CA RID: 24778
			void GetRect([In] [Out] ref NativeMethods.RECT prcView);

			// Token: 0x060060CB RID: 24779
			void SetRectComplex([In] NativeMethods.RECT prcView, [In] NativeMethods.RECT prcHScroll, [In] NativeMethods.RECT prcVScroll, [In] NativeMethods.RECT prcSizeBox);

			// Token: 0x060060CC RID: 24780
			void Show(bool fShow);

			// Token: 0x060060CD RID: 24781
			[PreserveSig]
			int UIActivate(bool fUIActivate);

			// Token: 0x060060CE RID: 24782
			void Open();

			// Token: 0x060060CF RID: 24783
			[PreserveSig]
			int Close([MarshalAs(UnmanagedType.U4)] [In] int dwReserved);

			// Token: 0x060060D0 RID: 24784
			void SaveViewState([MarshalAs(UnmanagedType.Interface)] [In] UnsafeNativeMethods.IStream pstm);

			// Token: 0x060060D1 RID: 24785
			void ApplyViewState([MarshalAs(UnmanagedType.Interface)] [In] UnsafeNativeMethods.IStream pstm);

			// Token: 0x060060D2 RID: 24786
			void Clone([MarshalAs(UnmanagedType.Interface)] [In] UnsafeNativeMethods.IOleInPlaceSite pIPSiteNew, [MarshalAs(UnmanagedType.LPArray)] [Out] UnsafeNativeMethods.IOleDocumentView[] ppViewNew);
		}

		// Token: 0x020005D8 RID: 1496
		[Guid("b722bcc5-4e68-101b-a2bc-00aa00404770")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IOleDocument
		{
			// Token: 0x060060D3 RID: 24787
			[PreserveSig]
			int CreateView(UnsafeNativeMethods.IOleInPlaceSite pIPSite, UnsafeNativeMethods.IStream pstm, int dwReserved, out UnsafeNativeMethods.IOleDocumentView ppView);

			// Token: 0x060060D4 RID: 24788
			[PreserveSig]
			int GetDocMiscStatus(out int pdwStatus);

			// Token: 0x060060D5 RID: 24789
			int EnumViews(out object ppEnum, out UnsafeNativeMethods.IOleDocumentView ppView);
		}

		// Token: 0x020005D9 RID: 1497
		[Guid("0000011e-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IOleCache
		{
			// Token: 0x060060D6 RID: 24790
			int Cache(ref FORMATETC pformatetc, int advf);

			// Token: 0x060060D7 RID: 24791
			void Uncache(int dwConnection);

			// Token: 0x060060D8 RID: 24792
			object EnumCache();

			// Token: 0x060060D9 RID: 24793
			void InitCache(IDataObject pDataObject);

			// Token: 0x060060DA RID: 24794
			void SetData(ref FORMATETC pformatetc, ref STGMEDIUM pmedium, bool fRelease);
		}

		// Token: 0x020005DA RID: 1498
		[TypeLibType(4176)]
		[Guid("618736E0-3C3D-11CF-810C-00AA00389B71")]
		[ComImport]
		public interface IAccessibleInternal
		{
			// Token: 0x060060DB RID: 24795
			[DispId(-5000)]
			[TypeLibFunc(64)]
			[return: MarshalAs(UnmanagedType.IDispatch)]
			object get_accParent();

			// Token: 0x060060DC RID: 24796
			[DispId(-5001)]
			[TypeLibFunc(64)]
			int get_accChildCount();

			// Token: 0x060060DD RID: 24797
			[TypeLibFunc(64)]
			[DispId(-5002)]
			[return: MarshalAs(UnmanagedType.IDispatch)]
			object get_accChild([MarshalAs(UnmanagedType.Struct)] [In] object varChild);

			// Token: 0x060060DE RID: 24798
			[DispId(-5003)]
			[TypeLibFunc(64)]
			[return: MarshalAs(UnmanagedType.BStr)]
			string get_accName([MarshalAs(UnmanagedType.Struct)] [In] [Optional] object varChild);

			// Token: 0x060060DF RID: 24799
			[TypeLibFunc(64)]
			[DispId(-5004)]
			[return: MarshalAs(UnmanagedType.BStr)]
			string get_accValue([MarshalAs(UnmanagedType.Struct)] [In] [Optional] object varChild);

			// Token: 0x060060E0 RID: 24800
			[DispId(-5005)]
			[TypeLibFunc(64)]
			[return: MarshalAs(UnmanagedType.BStr)]
			string get_accDescription([MarshalAs(UnmanagedType.Struct)] [In] [Optional] object varChild);

			// Token: 0x060060E1 RID: 24801
			[DispId(-5006)]
			[TypeLibFunc(64)]
			[return: MarshalAs(UnmanagedType.Struct)]
			object get_accRole([MarshalAs(UnmanagedType.Struct)] [In] [Optional] object varChild);

			// Token: 0x060060E2 RID: 24802
			[TypeLibFunc(64)]
			[DispId(-5007)]
			[return: MarshalAs(UnmanagedType.Struct)]
			object get_accState([MarshalAs(UnmanagedType.Struct)] [In] [Optional] object varChild);

			// Token: 0x060060E3 RID: 24803
			[TypeLibFunc(64)]
			[DispId(-5008)]
			[return: MarshalAs(UnmanagedType.BStr)]
			string get_accHelp([MarshalAs(UnmanagedType.Struct)] [In] [Optional] object varChild);

			// Token: 0x060060E4 RID: 24804
			[DispId(-5009)]
			[TypeLibFunc(64)]
			int get_accHelpTopic([MarshalAs(UnmanagedType.BStr)] out string pszHelpFile, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object varChild);

			// Token: 0x060060E5 RID: 24805
			[DispId(-5010)]
			[TypeLibFunc(64)]
			[return: MarshalAs(UnmanagedType.BStr)]
			string get_accKeyboardShortcut([MarshalAs(UnmanagedType.Struct)] [In] [Optional] object varChild);

			// Token: 0x060060E6 RID: 24806
			[DispId(-5011)]
			[TypeLibFunc(64)]
			[return: MarshalAs(UnmanagedType.Struct)]
			object get_accFocus();

			// Token: 0x060060E7 RID: 24807
			[DispId(-5012)]
			[TypeLibFunc(64)]
			[return: MarshalAs(UnmanagedType.Struct)]
			object get_accSelection();

			// Token: 0x060060E8 RID: 24808
			[TypeLibFunc(64)]
			[DispId(-5013)]
			[return: MarshalAs(UnmanagedType.BStr)]
			string get_accDefaultAction([MarshalAs(UnmanagedType.Struct)] [In] [Optional] object varChild);

			// Token: 0x060060E9 RID: 24809
			[DispId(-5014)]
			[TypeLibFunc(64)]
			void accSelect([In] int flagsSelect, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object varChild);

			// Token: 0x060060EA RID: 24810
			[DispId(-5015)]
			[TypeLibFunc(64)]
			void accLocation(out int pxLeft, out int pyTop, out int pcxWidth, out int pcyHeight, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object varChild);

			// Token: 0x060060EB RID: 24811
			[TypeLibFunc(64)]
			[DispId(-5016)]
			[return: MarshalAs(UnmanagedType.Struct)]
			object accNavigate([In] int navDir, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object varStart);

			// Token: 0x060060EC RID: 24812
			[TypeLibFunc(64)]
			[DispId(-5017)]
			[return: MarshalAs(UnmanagedType.Struct)]
			object accHitTest([In] int xLeft, [In] int yTop);

			// Token: 0x060060ED RID: 24813
			[TypeLibFunc(64)]
			[DispId(-5018)]
			void accDoDefaultAction([MarshalAs(UnmanagedType.Struct)] [In] [Optional] object varChild);

			// Token: 0x060060EE RID: 24814
			[TypeLibFunc(64)]
			[DispId(-5003)]
			void set_accName([MarshalAs(UnmanagedType.Struct)] [In] [Optional] object varChild, [MarshalAs(UnmanagedType.BStr)] [In] string pszName);

			// Token: 0x060060EF RID: 24815
			[TypeLibFunc(64)]
			[DispId(-5004)]
			void set_accValue([MarshalAs(UnmanagedType.Struct)] [In] [Optional] object varChild, [MarshalAs(UnmanagedType.BStr)] [In] string pszValue);
		}

		// Token: 0x020005DB RID: 1499
		[Guid("BEF6E002-A874-101A-8BBA-00AA00300CAB")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IFont
		{
			// Token: 0x060060F0 RID: 24816
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetName();

			// Token: 0x060060F1 RID: 24817
			void SetName([MarshalAs(UnmanagedType.BStr)] [In] string pname);

			// Token: 0x060060F2 RID: 24818
			[return: MarshalAs(UnmanagedType.U8)]
			long GetSize();

			// Token: 0x060060F3 RID: 24819
			void SetSize([MarshalAs(UnmanagedType.U8)] [In] long psize);

			// Token: 0x060060F4 RID: 24820
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetBold();

			// Token: 0x060060F5 RID: 24821
			void SetBold([MarshalAs(UnmanagedType.Bool)] [In] bool pbold);

			// Token: 0x060060F6 RID: 24822
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetItalic();

			// Token: 0x060060F7 RID: 24823
			void SetItalic([MarshalAs(UnmanagedType.Bool)] [In] bool pitalic);

			// Token: 0x060060F8 RID: 24824
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetUnderline();

			// Token: 0x060060F9 RID: 24825
			void SetUnderline([MarshalAs(UnmanagedType.Bool)] [In] bool punderline);

			// Token: 0x060060FA RID: 24826
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetStrikethrough();

			// Token: 0x060060FB RID: 24827
			void SetStrikethrough([MarshalAs(UnmanagedType.Bool)] [In] bool pstrikethrough);

			// Token: 0x060060FC RID: 24828
			[return: MarshalAs(UnmanagedType.I2)]
			short GetWeight();

			// Token: 0x060060FD RID: 24829
			void SetWeight([MarshalAs(UnmanagedType.I2)] [In] short pweight);

			// Token: 0x060060FE RID: 24830
			[return: MarshalAs(UnmanagedType.I2)]
			short GetCharset();

			// Token: 0x060060FF RID: 24831
			void SetCharset([MarshalAs(UnmanagedType.I2)] [In] short pcharset);

			// Token: 0x06006100 RID: 24832
			IntPtr GetHFont();

			// Token: 0x06006101 RID: 24833
			void Clone(out UnsafeNativeMethods.IFont ppfont);

			// Token: 0x06006102 RID: 24834
			[PreserveSig]
			int IsEqual([MarshalAs(UnmanagedType.Interface)] [In] UnsafeNativeMethods.IFont pfontOther);

			// Token: 0x06006103 RID: 24835
			void SetRatio(int cyLogical, int cyHimetric);

			// Token: 0x06006104 RID: 24836
			void QueryTextMetrics(out IntPtr ptm);

			// Token: 0x06006105 RID: 24837
			void AddRefHfont(IntPtr hFont);

			// Token: 0x06006106 RID: 24838
			void ReleaseHfont(IntPtr hFont);

			// Token: 0x06006107 RID: 24839
			void SetHdc(IntPtr hdc);
		}

		// Token: 0x020005DC RID: 1500
		[Guid("7BF80980-BF32-101A-8BBB-00AA00300CAB")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IPicture
		{
			// Token: 0x06006108 RID: 24840
			IntPtr GetHandle();

			// Token: 0x06006109 RID: 24841
			IntPtr GetHPal();

			// Token: 0x0600610A RID: 24842
			[return: MarshalAs(UnmanagedType.I2)]
			short GetPictureType();

			// Token: 0x0600610B RID: 24843
			int GetWidth();

			// Token: 0x0600610C RID: 24844
			int GetHeight();

			// Token: 0x0600610D RID: 24845
			void Render(IntPtr hDC, int x, int y, int cx, int cy, int xSrc, int ySrc, int cxSrc, int cySrc, IntPtr rcBounds);

			// Token: 0x0600610E RID: 24846
			void SetHPal(IntPtr phpal);

			// Token: 0x0600610F RID: 24847
			IntPtr GetCurDC();

			// Token: 0x06006110 RID: 24848
			void SelectPicture(IntPtr hdcIn, [MarshalAs(UnmanagedType.LPArray)] [Out] IntPtr[] phdcOut, [MarshalAs(UnmanagedType.LPArray)] [Out] IntPtr[] phbmpOut);

			// Token: 0x06006111 RID: 24849
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetKeepOriginalFormat();

			// Token: 0x06006112 RID: 24850
			void SetKeepOriginalFormat([MarshalAs(UnmanagedType.Bool)] [In] bool pfkeep);

			// Token: 0x06006113 RID: 24851
			void PictureChanged();

			// Token: 0x06006114 RID: 24852
			[PreserveSig]
			int SaveAsFile([MarshalAs(UnmanagedType.Interface)] [In] UnsafeNativeMethods.IStream pstm, int fSaveMemCopy, out int pcbSize);

			// Token: 0x06006115 RID: 24853
			int GetAttributes();
		}

		// Token: 0x020005DD RID: 1501
		[Guid("7BF80981-BF32-101A-8BBB-00AA00300CAB")]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		[ComImport]
		public interface IPictureDisp
		{
			// Token: 0x170014C5 RID: 5317
			// (get) Token: 0x06006116 RID: 24854
			IntPtr Handle { get; }

			// Token: 0x170014C6 RID: 5318
			// (get) Token: 0x06006117 RID: 24855
			IntPtr HPal { get; }

			// Token: 0x170014C7 RID: 5319
			// (get) Token: 0x06006118 RID: 24856
			short PictureType { get; }

			// Token: 0x170014C8 RID: 5320
			// (get) Token: 0x06006119 RID: 24857
			int Width { get; }

			// Token: 0x170014C9 RID: 5321
			// (get) Token: 0x0600611A RID: 24858
			int Height { get; }

			// Token: 0x0600611B RID: 24859
			void Render(IntPtr hdc, int x, int y, int cx, int cy, int xSrc, int ySrc, int cxSrc, int cySrc);
		}

		// Token: 0x020005DE RID: 1502
		[SuppressUnmanagedCodeSecurity]
		internal class ThemingScope
		{
			// Token: 0x0600611C RID: 24860 RVA: 0x00167E94 File Offset: 0x00166094
			private static bool IsContextActive()
			{
				IntPtr zero = IntPtr.Zero;
				return UnsafeNativeMethods.ThemingScope.contextCreationSucceeded && UnsafeNativeMethods.ThemingScope.GetCurrentActCtx(out zero) && zero == UnsafeNativeMethods.ThemingScope.hActCtx;
			}

			// Token: 0x0600611D RID: 24861 RVA: 0x00167EC4 File Offset: 0x001660C4
			public static IntPtr Activate()
			{
				IntPtr intPtr = IntPtr.Zero;
				if (Application.UseVisualStyles && UnsafeNativeMethods.ThemingScope.contextCreationSucceeded && OSFeature.Feature.IsPresent(OSFeature.Themes) && !UnsafeNativeMethods.ThemingScope.IsContextActive() && !UnsafeNativeMethods.ThemingScope.ActivateActCtx(UnsafeNativeMethods.ThemingScope.hActCtx, out intPtr))
				{
					intPtr = IntPtr.Zero;
				}
				return intPtr;
			}

			// Token: 0x0600611E RID: 24862 RVA: 0x00167F12 File Offset: 0x00166112
			public static IntPtr Deactivate(IntPtr userCookie)
			{
				if (userCookie != IntPtr.Zero && OSFeature.Feature.IsPresent(OSFeature.Themes) && UnsafeNativeMethods.ThemingScope.DeactivateActCtx(0, userCookie))
				{
					userCookie = IntPtr.Zero;
				}
				return userCookie;
			}

			// Token: 0x0600611F RID: 24863 RVA: 0x00167F44 File Offset: 0x00166144
			public static bool CreateActivationContext(string dllPath, int nativeResourceManifestID)
			{
				Type typeFromHandle = typeof(UnsafeNativeMethods.ThemingScope);
				bool flag2;
				lock (typeFromHandle)
				{
					if (!UnsafeNativeMethods.ThemingScope.contextCreationSucceeded && OSFeature.Feature.IsPresent(OSFeature.Themes))
					{
						UnsafeNativeMethods.ThemingScope.enableThemingActivationContext = default(UnsafeNativeMethods.ThemingScope.ACTCTX);
						UnsafeNativeMethods.ThemingScope.enableThemingActivationContext.cbSize = Marshal.SizeOf(typeof(UnsafeNativeMethods.ThemingScope.ACTCTX));
						UnsafeNativeMethods.ThemingScope.enableThemingActivationContext.lpSource = dllPath;
						UnsafeNativeMethods.ThemingScope.enableThemingActivationContext.lpResourceName = (IntPtr)nativeResourceManifestID;
						UnsafeNativeMethods.ThemingScope.enableThemingActivationContext.dwFlags = 8U;
						UnsafeNativeMethods.ThemingScope.hActCtx = UnsafeNativeMethods.ThemingScope.CreateActCtx(ref UnsafeNativeMethods.ThemingScope.enableThemingActivationContext);
						UnsafeNativeMethods.ThemingScope.contextCreationSucceeded = UnsafeNativeMethods.ThemingScope.hActCtx != new IntPtr(-1);
					}
					flag2 = UnsafeNativeMethods.ThemingScope.contextCreationSucceeded;
				}
				return flag2;
			}

			// Token: 0x06006120 RID: 24864
			[DllImport("kernel32.dll")]
			private static extern IntPtr CreateActCtx(ref UnsafeNativeMethods.ThemingScope.ACTCTX actctx);

			// Token: 0x06006121 RID: 24865
			[DllImport("kernel32.dll")]
			private static extern bool ActivateActCtx(IntPtr hActCtx, out IntPtr lpCookie);

			// Token: 0x06006122 RID: 24866
			[DllImport("kernel32.dll")]
			private static extern bool DeactivateActCtx(int dwFlags, IntPtr lpCookie);

			// Token: 0x06006123 RID: 24867
			[DllImport("kernel32.dll")]
			private static extern bool GetCurrentActCtx(out IntPtr handle);

			// Token: 0x04003862 RID: 14434
			private static UnsafeNativeMethods.ThemingScope.ACTCTX enableThemingActivationContext;

			// Token: 0x04003863 RID: 14435
			private static IntPtr hActCtx;

			// Token: 0x04003864 RID: 14436
			private static bool contextCreationSucceeded;

			// Token: 0x04003865 RID: 14437
			private const int ACTCTX_FLAG_ASSEMBLY_DIRECTORY_VALID = 4;

			// Token: 0x04003866 RID: 14438
			private const int ACTCTX_FLAG_RESOURCE_NAME_VALID = 8;

			// Token: 0x020008AE RID: 2222
			private struct ACTCTX
			{
				// Token: 0x04004515 RID: 17685
				public int cbSize;

				// Token: 0x04004516 RID: 17686
				public uint dwFlags;

				// Token: 0x04004517 RID: 17687
				public string lpSource;

				// Token: 0x04004518 RID: 17688
				public ushort wProcessorArchitecture;

				// Token: 0x04004519 RID: 17689
				public ushort wLangId;

				// Token: 0x0400451A RID: 17690
				public string lpAssemblyDirectory;

				// Token: 0x0400451B RID: 17691
				public IntPtr lpResourceName;

				// Token: 0x0400451C RID: 17692
				public string lpApplicationName;
			}
		}

		// Token: 0x020005DF RID: 1503
		[SuppressUnmanagedCodeSecurity]
		[StructLayout(LayoutKind.Sequential)]
		internal class PROCESS_INFORMATION
		{
			// Token: 0x06006125 RID: 24869 RVA: 0x00168010 File Offset: 0x00166210
			~PROCESS_INFORMATION()
			{
				this.Close();
			}

			// Token: 0x06006126 RID: 24870 RVA: 0x0016803C File Offset: 0x0016623C
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			internal void Close()
			{
				if (this.hProcess != (IntPtr)0 && this.hProcess != UnsafeNativeMethods.PROCESS_INFORMATION.INVALID_HANDLE_VALUE)
				{
					UnsafeNativeMethods.PROCESS_INFORMATION.CloseHandle(new HandleRef(this, this.hProcess));
					this.hProcess = UnsafeNativeMethods.PROCESS_INFORMATION.INVALID_HANDLE_VALUE;
				}
				if (this.hThread != (IntPtr)0 && this.hThread != UnsafeNativeMethods.PROCESS_INFORMATION.INVALID_HANDLE_VALUE)
				{
					UnsafeNativeMethods.PROCESS_INFORMATION.CloseHandle(new HandleRef(this, this.hThread));
					this.hThread = UnsafeNativeMethods.PROCESS_INFORMATION.INVALID_HANDLE_VALUE;
				}
			}

			// Token: 0x06006127 RID: 24871
			[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
			private static extern bool CloseHandle(HandleRef handle);

			// Token: 0x04003867 RID: 14439
			public IntPtr hProcess = IntPtr.Zero;

			// Token: 0x04003868 RID: 14440
			public IntPtr hThread = IntPtr.Zero;

			// Token: 0x04003869 RID: 14441
			public int dwProcessId;

			// Token: 0x0400386A RID: 14442
			public int dwThreadId;

			// Token: 0x0400386B RID: 14443
			private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);
		}

		// Token: 0x020005E0 RID: 1504
		[ComVisible(true)]
		[Guid("e4cfef41-071d-472c-a65c-c14f59ea81eb")]
		public enum StructureChangeType
		{
			// Token: 0x0400386D RID: 14445
			ChildAdded,
			// Token: 0x0400386E RID: 14446
			ChildRemoved,
			// Token: 0x0400386F RID: 14447
			ChildrenInvalidated,
			// Token: 0x04003870 RID: 14448
			ChildrenBulkAdded,
			// Token: 0x04003871 RID: 14449
			ChildrenBulkRemoved,
			// Token: 0x04003872 RID: 14450
			ChildrenReordered
		}

		// Token: 0x020005E1 RID: 1505
		[ComVisible(true)]
		[Guid("76d12d7e-b227-4417-9ce2-42642ffa896a")]
		public enum ExpandCollapseState
		{
			// Token: 0x04003874 RID: 14452
			Collapsed,
			// Token: 0x04003875 RID: 14453
			Expanded,
			// Token: 0x04003876 RID: 14454
			PartiallyExpanded,
			// Token: 0x04003877 RID: 14455
			LeafNode
		}

		// Token: 0x020005E2 RID: 1506
		[Flags]
		public enum ProviderOptions
		{
			// Token: 0x04003879 RID: 14457
			ClientSideProvider = 1,
			// Token: 0x0400387A RID: 14458
			ServerSideProvider = 2,
			// Token: 0x0400387B RID: 14459
			NonClientAreaProvider = 4,
			// Token: 0x0400387C RID: 14460
			OverrideProvider = 8,
			// Token: 0x0400387D RID: 14461
			ProviderOwnsSetFocus = 16,
			// Token: 0x0400387E RID: 14462
			UseComThreading = 32
		}

		// Token: 0x020005E3 RID: 1507
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[ComVisible(true)]
		[Guid("fb8b03af-3bdf-48d4-bd36-1a65793be168")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface ISelectionProvider
		{
			// Token: 0x0600612A RID: 24874
			[return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UNKNOWN)]
			object[] GetSelection();

			// Token: 0x170014CA RID: 5322
			// (get) Token: 0x0600612B RID: 24875
			bool CanSelectMultiple
			{
				[return: MarshalAs(UnmanagedType.Bool)]
				get;
			}

			// Token: 0x170014CB RID: 5323
			// (get) Token: 0x0600612C RID: 24876
			bool IsSelectionRequired
			{
				[return: MarshalAs(UnmanagedType.Bool)]
				get;
			}
		}

		// Token: 0x020005E4 RID: 1508
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComVisible(true)]
		[Guid("2acad808-b2d4-452d-a407-91ff1ad167b2")]
		[ComImport]
		public interface ISelectionItemProvider
		{
			// Token: 0x0600612D RID: 24877
			void Select();

			// Token: 0x0600612E RID: 24878
			void AddToSelection();

			// Token: 0x0600612F RID: 24879
			void RemoveFromSelection();

			// Token: 0x170014CC RID: 5324
			// (get) Token: 0x06006130 RID: 24880
			bool IsSelected
			{
				[return: MarshalAs(UnmanagedType.Bool)]
				get;
			}

			// Token: 0x170014CD RID: 5325
			// (get) Token: 0x06006131 RID: 24881
			UnsafeNativeMethods.IRawElementProviderSimple SelectionContainer
			{
				[return: MarshalAs(UnmanagedType.Interface)]
				get;
			}
		}

		// Token: 0x020005E5 RID: 1509
		[ComVisible(true)]
		[Guid("1d5df27c-8947-4425-b8d9-79787bb460b8")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		public interface IRawElementProviderHwndOverride : UnsafeNativeMethods.IRawElementProviderSimple
		{
			// Token: 0x06006132 RID: 24882
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.IRawElementProviderSimple GetOverrideProviderForHwnd(IntPtr hwnd);
		}

		// Token: 0x020005E6 RID: 1510
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[Guid("6D5140C1-7436-11CE-8034-00AA006009FA")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IServiceProvider
		{
			// Token: 0x06006133 RID: 24883
			[SuppressUnmanagedCodeSecurity]
			[SecurityCritical]
			[PreserveSig]
			int QueryService(ref Guid service, ref Guid riid, out IntPtr ppvObj);
		}

		// Token: 0x020005E7 RID: 1511
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[ComVisible(true)]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[CLSCompliant(false)]
		[Guid("F8B80ADA-2C44-48D0-89BE-5FF23C9CD875")]
		[ComImport]
		internal interface IAccessibleEx
		{
			// Token: 0x06006134 RID: 24884
			[return: MarshalAs(UnmanagedType.IUnknown)]
			object GetObjectForChild(int idChild);

			// Token: 0x06006135 RID: 24885
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int GetIAccessiblePair([MarshalAs(UnmanagedType.Interface)] out object ppAcc, out int pidChild);

			// Token: 0x06006136 RID: 24886
			[return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_I4)]
			int[] GetRuntimeId();

			// Token: 0x06006137 RID: 24887
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int ConvertReturnedElement([MarshalAs(UnmanagedType.Interface)] [In] object pIn, [MarshalAs(UnmanagedType.Interface)] out object ppRetValOut);
		}

		// Token: 0x020005E8 RID: 1512
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[ComVisible(true)]
		[Guid("d847d3a5-cab0-4a98-8c32-ecb45c59ad24")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IExpandCollapseProvider
		{
			// Token: 0x06006138 RID: 24888
			void Expand();

			// Token: 0x06006139 RID: 24889
			void Collapse();

			// Token: 0x170014CE RID: 5326
			// (get) Token: 0x0600613A RID: 24890
			UnsafeNativeMethods.ExpandCollapseState ExpandCollapseState { get; }
		}

		// Token: 0x020005E9 RID: 1513
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[ComVisible(true)]
		[Guid("c7935180-6fb3-4201-b174-7df73adbf64a")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IValueProvider
		{
			// Token: 0x0600613B RID: 24891
			void SetValue([MarshalAs(UnmanagedType.LPWStr)] string value);

			// Token: 0x170014CF RID: 5327
			// (get) Token: 0x0600613C RID: 24892
			string Value { get; }

			// Token: 0x170014D0 RID: 5328
			// (get) Token: 0x0600613D RID: 24893
			bool IsReadOnly
			{
				[return: MarshalAs(UnmanagedType.Bool)]
				get;
			}
		}

		// Token: 0x020005EA RID: 1514
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[ComVisible(true)]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("36dc7aef-33e6-4691-afe1-2be7274b3d33")]
		[ComImport]
		public interface IRangeValueProvider
		{
			// Token: 0x0600613E RID: 24894
			void SetValue(double value);

			// Token: 0x170014D1 RID: 5329
			// (get) Token: 0x0600613F RID: 24895
			double Value { get; }

			// Token: 0x170014D2 RID: 5330
			// (get) Token: 0x06006140 RID: 24896
			bool IsReadOnly
			{
				[return: MarshalAs(UnmanagedType.Bool)]
				get;
			}

			// Token: 0x170014D3 RID: 5331
			// (get) Token: 0x06006141 RID: 24897
			double Maximum { get; }

			// Token: 0x170014D4 RID: 5332
			// (get) Token: 0x06006142 RID: 24898
			double Minimum { get; }

			// Token: 0x170014D5 RID: 5333
			// (get) Token: 0x06006143 RID: 24899
			double LargeChange { get; }

			// Token: 0x170014D6 RID: 5334
			// (get) Token: 0x06006144 RID: 24900
			double SmallChange { get; }
		}

		// Token: 0x020005EB RID: 1515
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[ComVisible(true)]
		[Guid("D6DD68D1-86FD-4332-8666-9ABEDEA2D24C")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[CLSCompliant(false)]
		[ComImport]
		public interface IRawElementProviderSimple
		{
			// Token: 0x170014D7 RID: 5335
			// (get) Token: 0x06006145 RID: 24901
			UnsafeNativeMethods.ProviderOptions ProviderOptions { get; }

			// Token: 0x06006146 RID: 24902
			[return: MarshalAs(UnmanagedType.IUnknown)]
			object GetPatternProvider(int patternId);

			// Token: 0x06006147 RID: 24903
			object GetPropertyValue(int propertyId);

			// Token: 0x170014D8 RID: 5336
			// (get) Token: 0x06006148 RID: 24904
			UnsafeNativeMethods.IRawElementProviderSimple HostRawElementProvider { get; }
		}

		// Token: 0x020005EC RID: 1516
		[ComVisible(true)]
		[Guid("670c3006-bf4c-428b-8534-e1848f645122")]
		public enum NavigateDirection
		{
			// Token: 0x04003880 RID: 14464
			Parent,
			// Token: 0x04003881 RID: 14465
			NextSibling,
			// Token: 0x04003882 RID: 14466
			PreviousSibling,
			// Token: 0x04003883 RID: 14467
			FirstChild,
			// Token: 0x04003884 RID: 14468
			LastChild
		}

		// Token: 0x020005ED RID: 1517
		[ComVisible(true)]
		[Guid("f7063da8-8359-439c-9297-bbc5299a7d87")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[CLSCompliant(false)]
		[ComImport]
		public interface IRawElementProviderFragment : UnsafeNativeMethods.IRawElementProviderSimple
		{
			// Token: 0x06006149 RID: 24905
			[return: MarshalAs(UnmanagedType.IUnknown)]
			object Navigate(UnsafeNativeMethods.NavigateDirection direction);

			// Token: 0x0600614A RID: 24906
			int[] GetRuntimeId();

			// Token: 0x170014D9 RID: 5337
			// (get) Token: 0x0600614B RID: 24907
			NativeMethods.UiaRect BoundingRectangle { get; }

			// Token: 0x0600614C RID: 24908
			[return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UNKNOWN)]
			object[] GetEmbeddedFragmentRoots();

			// Token: 0x0600614D RID: 24909
			void SetFocus();

			// Token: 0x170014DA RID: 5338
			// (get) Token: 0x0600614E RID: 24910
			UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
			{
				[return: MarshalAs(UnmanagedType.Interface)]
				get;
			}
		}

		// Token: 0x020005EE RID: 1518
		[ComVisible(true)]
		[Guid("620ce2a5-ab8f-40a9-86cb-de3c75599b58")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[CLSCompliant(false)]
		[ComImport]
		public interface IRawElementProviderFragmentRoot : UnsafeNativeMethods.IRawElementProviderFragment, UnsafeNativeMethods.IRawElementProviderSimple
		{
			// Token: 0x0600614F RID: 24911
			[return: MarshalAs(UnmanagedType.IUnknown)]
			object ElementProviderFromPoint(double x, double y);

			// Token: 0x06006150 RID: 24912
			[return: MarshalAs(UnmanagedType.IUnknown)]
			object GetFocus();
		}

		// Token: 0x020005EF RID: 1519
		[Flags]
		public enum ToggleState
		{
			// Token: 0x04003886 RID: 14470
			ToggleState_Off = 0,
			// Token: 0x04003887 RID: 14471
			ToggleState_On = 1,
			// Token: 0x04003888 RID: 14472
			ToggleState_Indeterminate = 2
		}

		// Token: 0x020005F0 RID: 1520
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[ComVisible(true)]
		[Guid("56D00BD0-C4F4-433C-A836-1A52A57E0892")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[CLSCompliant(false)]
		[ComImport]
		public interface IToggleProvider
		{
			// Token: 0x06006151 RID: 24913
			void Toggle();

			// Token: 0x170014DB RID: 5339
			// (get) Token: 0x06006152 RID: 24914
			UnsafeNativeMethods.ToggleState ToggleState { get; }
		}

		// Token: 0x020005F1 RID: 1521
		[Flags]
		public enum RowOrColumnMajor
		{
			// Token: 0x0400388A RID: 14474
			RowOrColumnMajor_RowMajor = 0,
			// Token: 0x0400388B RID: 14475
			RowOrColumnMajor_ColumnMajor = 1,
			// Token: 0x0400388C RID: 14476
			RowOrColumnMajor_Indeterminate = 2
		}

		// Token: 0x020005F2 RID: 1522
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[ComVisible(true)]
		[Guid("9c860395-97b3-490a-b52a-858cc22af166")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[CLSCompliant(false)]
		[ComImport]
		public interface ITableProvider
		{
			// Token: 0x06006153 RID: 24915
			[return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UNKNOWN)]
			object[] GetRowHeaders();

			// Token: 0x06006154 RID: 24916
			[return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UNKNOWN)]
			object[] GetColumnHeaders();

			// Token: 0x170014DC RID: 5340
			// (get) Token: 0x06006155 RID: 24917
			UnsafeNativeMethods.RowOrColumnMajor RowOrColumnMajor { get; }
		}

		// Token: 0x020005F3 RID: 1523
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[ComVisible(true)]
		[Guid("b9734fa6-771f-4d78-9c90-2517999349cd")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[CLSCompliant(false)]
		[ComImport]
		public interface ITableItemProvider
		{
			// Token: 0x06006156 RID: 24918
			[return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UNKNOWN)]
			object[] GetRowHeaderItems();

			// Token: 0x06006157 RID: 24919
			[return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UNKNOWN)]
			object[] GetColumnHeaderItems();
		}

		// Token: 0x020005F4 RID: 1524
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[ComVisible(true)]
		[Guid("b17d6187-0907-464b-a168-0ef17a1572b1")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[CLSCompliant(false)]
		[ComImport]
		public interface IGridProvider
		{
			// Token: 0x06006158 RID: 24920
			[return: MarshalAs(UnmanagedType.IUnknown)]
			object GetItem(int row, int column);

			// Token: 0x170014DD RID: 5341
			// (get) Token: 0x06006159 RID: 24921
			int RowCount
			{
				[return: MarshalAs(UnmanagedType.I4)]
				get;
			}

			// Token: 0x170014DE RID: 5342
			// (get) Token: 0x0600615A RID: 24922
			int ColumnCount
			{
				[return: MarshalAs(UnmanagedType.I4)]
				get;
			}
		}

		// Token: 0x020005F5 RID: 1525
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[ComVisible(true)]
		[Guid("d02541f1-fb81-4d64-ae32-f520f8a6dbd1")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[CLSCompliant(false)]
		[ComImport]
		public interface IGridItemProvider
		{
			// Token: 0x170014DF RID: 5343
			// (get) Token: 0x0600615B RID: 24923
			int Row
			{
				[return: MarshalAs(UnmanagedType.I4)]
				get;
			}

			// Token: 0x170014E0 RID: 5344
			// (get) Token: 0x0600615C RID: 24924
			int Column
			{
				[return: MarshalAs(UnmanagedType.I4)]
				get;
			}

			// Token: 0x170014E1 RID: 5345
			// (get) Token: 0x0600615D RID: 24925
			int RowSpan
			{
				[return: MarshalAs(UnmanagedType.I4)]
				get;
			}

			// Token: 0x170014E2 RID: 5346
			// (get) Token: 0x0600615E RID: 24926
			int ColumnSpan
			{
				[return: MarshalAs(UnmanagedType.I4)]
				get;
			}

			// Token: 0x170014E3 RID: 5347
			// (get) Token: 0x0600615F RID: 24927
			UnsafeNativeMethods.IRawElementProviderSimple ContainingGrid
			{
				[return: MarshalAs(UnmanagedType.Interface)]
				get;
			}
		}

		// Token: 0x020005F6 RID: 1526
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[ComVisible(true)]
		[Guid("54fcb24b-e18e-47a2-b4d3-eccbe77599a2")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[CLSCompliant(false)]
		[ComImport]
		public interface IInvokeProvider
		{
			// Token: 0x06006160 RID: 24928
			void Invoke();
		}

		// Token: 0x020005F7 RID: 1527
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[ComVisible(true)]
		[Guid("2360c714-4bf1-4b26-ba65-9b21316127eb")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[CLSCompliant(false)]
		[ComImport]
		public interface IScrollItemProvider
		{
			// Token: 0x06006161 RID: 24929
			void ScrollIntoView();
		}
	}
}
