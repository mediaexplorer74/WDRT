using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Win32
{
	// Token: 0x02000011 RID: 17
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	internal static class NativeMethods
	{
		// Token: 0x06000154 RID: 340
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool GetExitCodeProcess(Microsoft.Win32.SafeHandles.SafeProcessHandle processHandle, out int exitCode);

		// Token: 0x06000155 RID: 341
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool GetProcessTimes(Microsoft.Win32.SafeHandles.SafeProcessHandle handle, out long creation, out long exit, out long kernel, out long user);

		// Token: 0x06000156 RID: 342
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool GetThreadTimes(Microsoft.Win32.SafeHandles.SafeThreadHandle handle, out long creation, out long exit, out long kernel, out long user);

		// Token: 0x06000157 RID: 343
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
		public static extern IntPtr GetStdHandle(int whichHandle);

		// Token: 0x06000158 RID: 344
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool CreatePipe(out SafeFileHandle hReadPipe, out SafeFileHandle hWritePipe, NativeMethods.SECURITY_ATTRIBUTES lpPipeAttributes, int nSize);

		// Token: 0x06000159 RID: 345
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool CreateProcess([MarshalAs(UnmanagedType.LPTStr)] string lpApplicationName, StringBuilder lpCommandLine, NativeMethods.SECURITY_ATTRIBUTES lpProcessAttributes, NativeMethods.SECURITY_ATTRIBUTES lpThreadAttributes, bool bInheritHandles, int dwCreationFlags, IntPtr lpEnvironment, [MarshalAs(UnmanagedType.LPTStr)] string lpCurrentDirectory, NativeMethods.STARTUPINFO lpStartupInfo, SafeNativeMethods.PROCESS_INFORMATION lpProcessInformation);

		// Token: 0x0600015A RID: 346
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool TerminateProcess(Microsoft.Win32.SafeHandles.SafeProcessHandle processHandle, int exitCode);

		// Token: 0x0600015B RID: 347
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern int GetCurrentProcessId();

		// Token: 0x0600015C RID: 348
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
		public static extern IntPtr GetCurrentProcess();

		// Token: 0x0600015D RID: 349
		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern uint SetNamedSecurityInfo(string pObjectName, uint ObjectType, uint SecurityInfo, IntPtr psidOwner, IntPtr psidGroup, SafeLocalMemHandle pDacl, IntPtr pSacl);

		// Token: 0x0600015E RID: 350 RVA: 0x0000D39B File Offset: 0x0000B59B
		internal static uint SetNamedSecurityInfo(string directory, SafeLocalMemHandle pDacl)
		{
			return NativeMethods.SetNamedSecurityInfo(directory, 1U, 20U, IntPtr.Zero, IntPtr.Zero, pDacl, IntPtr.Zero);
		}

		// Token: 0x0600015F RID: 351
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool CreateDirectory(string path, NativeMethods.SECURITY_ATTRIBUTES lpSecurityAttributes);

		// Token: 0x06000160 RID: 352 RVA: 0x0000D3B8 File Offset: 0x0000B5B8
		internal static void CreateDirectory(string path, SafeLocalMemHandle acl)
		{
			NativeMethods.SECURITY_ATTRIBUTES security_ATTRIBUTES = new NativeMethods.SECURITY_ATTRIBUTES();
			security_ATTRIBUTES.lpSecurityDescriptor = acl;
			security_ATTRIBUTES.nLength = Marshal.SizeOf(security_ATTRIBUTES);
			if (!NativeMethods.CreateDirectory(path, security_ATTRIBUTES))
			{
				throw new Win32Exception();
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000D3F0 File Offset: 0x0000B5F0
		internal static string GetLocalPath(string fileName)
		{
			Uri uri = new Uri(fileName);
			return uri.LocalPath + uri.Fragment;
		}

		// Token: 0x06000162 RID: 354
		[SuppressUnmanagedCodeSecurity]
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool CreateProcessAsUser(SafeHandle hToken, string lpApplicationName, string lpCommandLine, NativeMethods.SECURITY_ATTRIBUTES lpProcessAttributes, NativeMethods.SECURITY_ATTRIBUTES lpThreadAttributes, bool bInheritHandles, int dwCreationFlags, HandleRef lpEnvironment, string lpCurrentDirectory, NativeMethods.STARTUPINFO lpStartupInfo, SafeNativeMethods.PROCESS_INFORMATION lpProcessInformation);

		// Token: 0x06000163 RID: 355
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal static extern bool CreateProcessWithLogonW(string userName, string domain, IntPtr password, NativeMethods.LogonFlags logonFlags, [MarshalAs(UnmanagedType.LPTStr)] string appName, StringBuilder cmdLine, int creationFlags, IntPtr environmentBlock, [MarshalAs(UnmanagedType.LPTStr)] string lpCurrentDirectory, NativeMethods.STARTUPINFO lpStartupInfo, SafeNativeMethods.PROCESS_INFORMATION lpProcessInformation);

		// Token: 0x06000164 RID: 356
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern Microsoft.Win32.SafeHandles.SafeFileMappingHandle CreateFileMapping(IntPtr hFile, NativeMethods.SECURITY_ATTRIBUTES lpFileMappingAttributes, int flProtect, int dwMaximumSizeHigh, int dwMaximumSizeLow, string lpName);

		// Token: 0x06000165 RID: 357
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern Microsoft.Win32.SafeHandles.SafeFileMappingHandle OpenFileMapping(int dwDesiredAccess, bool bInheritHandle, string lpName);

		// Token: 0x06000166 RID: 358
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int WaitForInputIdle(Microsoft.Win32.SafeHandles.SafeProcessHandle handle, int milliseconds);

		// Token: 0x06000167 RID: 359
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern Microsoft.Win32.SafeHandles.SafeProcessHandle OpenProcess(int access, bool inherit, int processId);

		// Token: 0x06000168 RID: 360
		[DllImport("psapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool EnumProcessModules(Microsoft.Win32.SafeHandles.SafeProcessHandle handle, IntPtr modules, int size, ref int needed);

		// Token: 0x06000169 RID: 361
		[DllImport("psapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool EnumProcesses(int[] processIds, int size, out int needed);

		// Token: 0x0600016A RID: 362
		[DllImport("psapi.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetModuleFileNameEx(HandleRef processHandle, HandleRef moduleHandle, StringBuilder baseName, int size);

		// Token: 0x0600016B RID: 363
		[DllImport("psapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool GetModuleInformation(Microsoft.Win32.SafeHandles.SafeProcessHandle processHandle, HandleRef moduleHandle, NativeMethods.NtModuleInfo ntModuleInfo, int size);

		// Token: 0x0600016C RID: 364
		[DllImport("psapi.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetModuleBaseName(Microsoft.Win32.SafeHandles.SafeProcessHandle processHandle, HandleRef moduleHandle, StringBuilder baseName, int size);

		// Token: 0x0600016D RID: 365
		[DllImport("psapi.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetModuleFileNameEx(Microsoft.Win32.SafeHandles.SafeProcessHandle processHandle, HandleRef moduleHandle, StringBuilder baseName, int size);

		// Token: 0x0600016E RID: 366
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool SetProcessWorkingSetSize(Microsoft.Win32.SafeHandles.SafeProcessHandle handle, IntPtr min, IntPtr max);

		// Token: 0x0600016F RID: 367
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool GetProcessWorkingSetSize(Microsoft.Win32.SafeHandles.SafeProcessHandle handle, out IntPtr min, out IntPtr max);

		// Token: 0x06000170 RID: 368
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool SetProcessAffinityMask(Microsoft.Win32.SafeHandles.SafeProcessHandle handle, IntPtr mask);

		// Token: 0x06000171 RID: 369
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool GetProcessAffinityMask(Microsoft.Win32.SafeHandles.SafeProcessHandle handle, out IntPtr processMask, out IntPtr systemMask);

		// Token: 0x06000172 RID: 370
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool GetThreadPriorityBoost(Microsoft.Win32.SafeHandles.SafeThreadHandle handle, out bool disabled);

		// Token: 0x06000173 RID: 371
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool SetThreadPriorityBoost(Microsoft.Win32.SafeHandles.SafeThreadHandle handle, bool disabled);

		// Token: 0x06000174 RID: 372
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool GetProcessPriorityBoost(Microsoft.Win32.SafeHandles.SafeProcessHandle handle, out bool disabled);

		// Token: 0x06000175 RID: 373
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool SetProcessPriorityBoost(Microsoft.Win32.SafeHandles.SafeProcessHandle handle, bool disabled);

		// Token: 0x06000176 RID: 374
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern Microsoft.Win32.SafeHandles.SafeThreadHandle OpenThread(int access, bool inherit, int threadId);

		// Token: 0x06000177 RID: 375
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool SetThreadPriority(Microsoft.Win32.SafeHandles.SafeThreadHandle handle, int priority);

		// Token: 0x06000178 RID: 376
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetThreadPriority(Microsoft.Win32.SafeHandles.SafeThreadHandle handle);

		// Token: 0x06000179 RID: 377
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr SetThreadAffinityMask(Microsoft.Win32.SafeHandles.SafeThreadHandle handle, HandleRef mask);

		// Token: 0x0600017A RID: 378
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int SetThreadIdealProcessor(Microsoft.Win32.SafeHandles.SafeThreadHandle handle, int processor);

		// Token: 0x0600017B RID: 379
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr CreateToolhelp32Snapshot(int flags, int processId);

		// Token: 0x0600017C RID: 380
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool Process32First(HandleRef handle, IntPtr entry);

		// Token: 0x0600017D RID: 381
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool Process32Next(HandleRef handle, IntPtr entry);

		// Token: 0x0600017E RID: 382
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool Thread32First(HandleRef handle, NativeMethods.WinThreadEntry entry);

		// Token: 0x0600017F RID: 383
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool Thread32Next(HandleRef handle, NativeMethods.WinThreadEntry entry);

		// Token: 0x06000180 RID: 384
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool Module32First(HandleRef handle, IntPtr entry);

		// Token: 0x06000181 RID: 385
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool Module32Next(HandleRef handle, IntPtr entry);

		// Token: 0x06000182 RID: 386
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetPriorityClass(Microsoft.Win32.SafeHandles.SafeProcessHandle handle);

		// Token: 0x06000183 RID: 387
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool SetPriorityClass(Microsoft.Win32.SafeHandles.SafeProcessHandle handle, int priorityClass);

		// Token: 0x06000184 RID: 388
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool EnumWindows(NativeMethods.EnumThreadWindowsCallback callback, IntPtr extraData);

		// Token: 0x06000185 RID: 389
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetWindowThreadProcessId(HandleRef handle, out int processId);

		// Token: 0x06000186 RID: 390
		[DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool ShellExecuteEx(NativeMethods.ShellExecuteInfo info);

		// Token: 0x06000187 RID: 391
		[DllImport("ntdll.dll", CharSet = CharSet.Auto)]
		public static extern int NtQueryInformationProcess(Microsoft.Win32.SafeHandles.SafeProcessHandle processHandle, int query, NativeMethods.NtProcessBasicInfo info, int size, int[] returnedSize);

		// Token: 0x06000188 RID: 392
		[DllImport("ntdll.dll", CharSet = CharSet.Auto)]
		public static extern int NtQuerySystemInformation(int query, IntPtr dataPtr, int size, out int returnedSize);

		// Token: 0x06000189 RID: 393
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
		public static extern SafeFileHandle CreateFile(string lpFileName, int dwDesiredAccess, int dwShareMode, NativeMethods.SECURITY_ATTRIBUTES lpSecurityAttributes, int dwCreationDisposition, int dwFlagsAndAttributes, SafeFileHandle hTemplateFile);

		// Token: 0x0600018A RID: 394
		[DllImport("ntdll.dll")]
		public static extern int RtlGetVersion(out NativeMethods.RTL_OSVERSIONINFOEX lpVersionInformation);

		// Token: 0x0600018B RID: 395
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, SetLastError = true)]
		public static extern bool DuplicateHandle(HandleRef hSourceProcessHandle, SafeHandle hSourceHandle, HandleRef hTargetProcess, out SafeFileHandle targetHandle, int dwDesiredAccess, bool bInheritHandle, int dwOptions);

		// Token: 0x0600018C RID: 396
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, SetLastError = true)]
		public static extern bool DuplicateHandle(HandleRef hSourceProcessHandle, SafeHandle hSourceHandle, HandleRef hTargetProcess, out SafeWaitHandle targetHandle, int dwDesiredAccess, bool bInheritHandle, int dwOptions);

		// Token: 0x0600018D RID: 397
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool OpenProcessToken(HandleRef ProcessHandle, int DesiredAccess, out IntPtr TokenHandle);

		// Token: 0x0600018E RID: 398
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool LookupPrivilegeValue([MarshalAs(UnmanagedType.LPTStr)] string lpSystemName, [MarshalAs(UnmanagedType.LPTStr)] string lpName, out NativeMethods.LUID lpLuid);

		// Token: 0x0600018F RID: 399
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool AdjustTokenPrivileges(HandleRef TokenHandle, bool DisableAllPrivileges, NativeMethods.TokenPrivileges NewState, int BufferLength, IntPtr PreviousState, IntPtr ReturnLength);

		// Token: 0x06000190 RID: 400
		[DllImport("user32.dll", BestFitMapping = true, CharSet = CharSet.Auto)]
		public static extern int GetWindowText(HandleRef hWnd, StringBuilder lpString, int nMaxCount);

		// Token: 0x06000191 RID: 401
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int GetWindowTextLength(HandleRef hWnd);

		// Token: 0x06000192 RID: 402
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern bool IsWindowVisible(HandleRef hWnd);

		// Token: 0x06000193 RID: 403
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessageTimeout(HandleRef hWnd, int msg, IntPtr wParam, IntPtr lParam, int flags, int timeout, out IntPtr pdwResult);

		// Token: 0x06000194 RID: 404
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int GetWindowLong(HandleRef hWnd, int nIndex);

		// Token: 0x06000195 RID: 405
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int PostMessage(HandleRef hwnd, int msg, IntPtr wparam, IntPtr lparam);

		// Token: 0x06000196 RID: 406
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GetWindow(HandleRef hWnd, int uCmd);

		// Token: 0x06000197 RID: 407
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern IntPtr VirtualQuery(SafeFileMapViewHandle address, ref NativeMethods.MEMORY_BASIC_INFORMATION buffer, IntPtr sizeOfBuffer);

		// Token: 0x0400007A RID: 122
		public const int DEFAULT_GUI_FONT = 17;

		// Token: 0x0400007B RID: 123
		public const int SM_CYSCREEN = 1;

		// Token: 0x0400007C RID: 124
		public static readonly HandleRef NullHandleRef = new HandleRef(null, IntPtr.Zero);

		// Token: 0x0400007D RID: 125
		public static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

		// Token: 0x0400007E RID: 126
		public const int GENERIC_READ = -2147483648;

		// Token: 0x0400007F RID: 127
		public const int GENERIC_WRITE = 1073741824;

		// Token: 0x04000080 RID: 128
		public const int FILE_SHARE_READ = 1;

		// Token: 0x04000081 RID: 129
		public const int FILE_SHARE_WRITE = 2;

		// Token: 0x04000082 RID: 130
		public const int FILE_SHARE_DELETE = 4;

		// Token: 0x04000083 RID: 131
		public const int S_OK = 0;

		// Token: 0x04000084 RID: 132
		public const int E_ABORT = -2147467260;

		// Token: 0x04000085 RID: 133
		public const int E_NOTIMPL = -2147467263;

		// Token: 0x04000086 RID: 134
		public const int CREATE_ALWAYS = 2;

		// Token: 0x04000087 RID: 135
		public const int FILE_ATTRIBUTE_NORMAL = 128;

		// Token: 0x04000088 RID: 136
		public const int STARTF_USESTDHANDLES = 256;

		// Token: 0x04000089 RID: 137
		public const int STD_INPUT_HANDLE = -10;

		// Token: 0x0400008A RID: 138
		public const int STD_OUTPUT_HANDLE = -11;

		// Token: 0x0400008B RID: 139
		public const int STD_ERROR_HANDLE = -12;

		// Token: 0x0400008C RID: 140
		public const int STILL_ACTIVE = 259;

		// Token: 0x0400008D RID: 141
		public const int SW_HIDE = 0;

		// Token: 0x0400008E RID: 142
		public const int WAIT_OBJECT_0 = 0;

		// Token: 0x0400008F RID: 143
		public const int WAIT_FAILED = -1;

		// Token: 0x04000090 RID: 144
		public const int WAIT_TIMEOUT = 258;

		// Token: 0x04000091 RID: 145
		public const int WAIT_ABANDONED = 128;

		// Token: 0x04000092 RID: 146
		public const int WAIT_ABANDONED_0 = 128;

		// Token: 0x04000093 RID: 147
		public const int MOVEFILE_REPLACE_EXISTING = 1;

		// Token: 0x04000094 RID: 148
		public const int ERROR_CLASS_ALREADY_EXISTS = 1410;

		// Token: 0x04000095 RID: 149
		public const int ERROR_NONE_MAPPED = 1332;

		// Token: 0x04000096 RID: 150
		public const int ERROR_INSUFFICIENT_BUFFER = 122;

		// Token: 0x04000097 RID: 151
		public const int ERROR_INVALID_NAME = 123;

		// Token: 0x04000098 RID: 152
		public const int ERROR_PROC_NOT_FOUND = 127;

		// Token: 0x04000099 RID: 153
		public const int ERROR_BAD_EXE_FORMAT = 193;

		// Token: 0x0400009A RID: 154
		public const int ERROR_EXE_MACHINE_TYPE_MISMATCH = 216;

		// Token: 0x0400009B RID: 155
		public const int MAX_PATH = 260;

		// Token: 0x0400009C RID: 156
		public const int UIS_SET = 1;

		// Token: 0x0400009D RID: 157
		public const int WSF_VISIBLE = 1;

		// Token: 0x0400009E RID: 158
		public const int UIS_CLEAR = 2;

		// Token: 0x0400009F RID: 159
		public const int UISF_HIDEFOCUS = 1;

		// Token: 0x040000A0 RID: 160
		public const int UISF_HIDEACCEL = 2;

		// Token: 0x040000A1 RID: 161
		public const int USERCLASSTYPE_FULL = 1;

		// Token: 0x040000A2 RID: 162
		public const int UOI_FLAGS = 1;

		// Token: 0x040000A3 RID: 163
		public const int COLOR_WINDOW = 5;

		// Token: 0x040000A4 RID: 164
		public const int WS_POPUP = -2147483648;

		// Token: 0x040000A5 RID: 165
		public const int WS_VISIBLE = 268435456;

		// Token: 0x040000A6 RID: 166
		public const int WM_SETTINGCHANGE = 26;

		// Token: 0x040000A7 RID: 167
		public const int WM_SYSCOLORCHANGE = 21;

		// Token: 0x040000A8 RID: 168
		public const int WM_QUERYENDSESSION = 17;

		// Token: 0x040000A9 RID: 169
		public const int WM_QUIT = 18;

		// Token: 0x040000AA RID: 170
		public const int WM_ENDSESSION = 22;

		// Token: 0x040000AB RID: 171
		public const int WM_POWERBROADCAST = 536;

		// Token: 0x040000AC RID: 172
		public const int WM_COMPACTING = 65;

		// Token: 0x040000AD RID: 173
		public const int WM_DISPLAYCHANGE = 126;

		// Token: 0x040000AE RID: 174
		public const int WM_FONTCHANGE = 29;

		// Token: 0x040000AF RID: 175
		public const int WM_PALETTECHANGED = 785;

		// Token: 0x040000B0 RID: 176
		public const int WM_TIMECHANGE = 30;

		// Token: 0x040000B1 RID: 177
		public const int WM_THEMECHANGED = 794;

		// Token: 0x040000B2 RID: 178
		public const int WM_WTSSESSION_CHANGE = 689;

		// Token: 0x040000B3 RID: 179
		public const int ENDSESSION_LOGOFF = -2147483648;

		// Token: 0x040000B4 RID: 180
		public const int WM_TIMER = 275;

		// Token: 0x040000B5 RID: 181
		public const int WM_USER = 1024;

		// Token: 0x040000B6 RID: 182
		public const int WM_CREATETIMER = 1025;

		// Token: 0x040000B7 RID: 183
		public const int WM_KILLTIMER = 1026;

		// Token: 0x040000B8 RID: 184
		public const int WM_REFLECT = 8192;

		// Token: 0x040000B9 RID: 185
		public const int WTS_CONSOLE_CONNECT = 1;

		// Token: 0x040000BA RID: 186
		public const int WTS_CONSOLE_DISCONNECT = 2;

		// Token: 0x040000BB RID: 187
		public const int WTS_REMOTE_CONNECT = 3;

		// Token: 0x040000BC RID: 188
		public const int WTS_REMOTE_DISCONNECT = 4;

		// Token: 0x040000BD RID: 189
		public const int WTS_SESSION_LOGON = 5;

		// Token: 0x040000BE RID: 190
		public const int WTS_SESSION_LOGOFF = 6;

		// Token: 0x040000BF RID: 191
		public const int WTS_SESSION_LOCK = 7;

		// Token: 0x040000C0 RID: 192
		public const int WTS_SESSION_UNLOCK = 8;

		// Token: 0x040000C1 RID: 193
		public const int WTS_SESSION_REMOTE_CONTROL = 9;

		// Token: 0x040000C2 RID: 194
		public const int NOTIFY_FOR_THIS_SESSION = 0;

		// Token: 0x040000C3 RID: 195
		public const int CTRL_C_EVENT = 0;

		// Token: 0x040000C4 RID: 196
		public const int CTRL_BREAK_EVENT = 1;

		// Token: 0x040000C5 RID: 197
		public const int CTRL_CLOSE_EVENT = 2;

		// Token: 0x040000C6 RID: 198
		public const int CTRL_LOGOFF_EVENT = 5;

		// Token: 0x040000C7 RID: 199
		public const int CTRL_SHUTDOWN_EVENT = 6;

		// Token: 0x040000C8 RID: 200
		public const int SPI_GETBEEP = 1;

		// Token: 0x040000C9 RID: 201
		public const int SPI_SETBEEP = 2;

		// Token: 0x040000CA RID: 202
		public const int SPI_GETMOUSE = 3;

		// Token: 0x040000CB RID: 203
		public const int SPI_SETMOUSE = 4;

		// Token: 0x040000CC RID: 204
		public const int SPI_GETBORDER = 5;

		// Token: 0x040000CD RID: 205
		public const int SPI_SETBORDER = 6;

		// Token: 0x040000CE RID: 206
		public const int SPI_GETKEYBOARDSPEED = 10;

		// Token: 0x040000CF RID: 207
		public const int SPI_SETKEYBOARDSPEED = 11;

		// Token: 0x040000D0 RID: 208
		public const int SPI_LANGDRIVER = 12;

		// Token: 0x040000D1 RID: 209
		public const int SPI_ICONHORIZONTALSPACING = 13;

		// Token: 0x040000D2 RID: 210
		public const int SPI_GETSCREENSAVETIMEOUT = 14;

		// Token: 0x040000D3 RID: 211
		public const int SPI_SETSCREENSAVETIMEOUT = 15;

		// Token: 0x040000D4 RID: 212
		public const int SPI_GETSCREENSAVEACTIVE = 16;

		// Token: 0x040000D5 RID: 213
		public const int SPI_SETSCREENSAVEACTIVE = 17;

		// Token: 0x040000D6 RID: 214
		public const int SPI_GETGRIDGRANULARITY = 18;

		// Token: 0x040000D7 RID: 215
		public const int SPI_SETGRIDGRANULARITY = 19;

		// Token: 0x040000D8 RID: 216
		public const int SPI_SETDESKWALLPAPER = 20;

		// Token: 0x040000D9 RID: 217
		public const int SPI_SETDESKPATTERN = 21;

		// Token: 0x040000DA RID: 218
		public const int SPI_GETKEYBOARDDELAY = 22;

		// Token: 0x040000DB RID: 219
		public const int SPI_SETKEYBOARDDELAY = 23;

		// Token: 0x040000DC RID: 220
		public const int SPI_ICONVERTICALSPACING = 24;

		// Token: 0x040000DD RID: 221
		public const int SPI_GETICONTITLEWRAP = 25;

		// Token: 0x040000DE RID: 222
		public const int SPI_SETICONTITLEWRAP = 26;

		// Token: 0x040000DF RID: 223
		public const int SPI_GETMENUDROPALIGNMENT = 27;

		// Token: 0x040000E0 RID: 224
		public const int SPI_SETMENUDROPALIGNMENT = 28;

		// Token: 0x040000E1 RID: 225
		public const int SPI_SETDOUBLECLKWIDTH = 29;

		// Token: 0x040000E2 RID: 226
		public const int SPI_SETDOUBLECLKHEIGHT = 30;

		// Token: 0x040000E3 RID: 227
		public const int SPI_GETICONTITLELOGFONT = 31;

		// Token: 0x040000E4 RID: 228
		public const int SPI_SETDOUBLECLICKTIME = 32;

		// Token: 0x040000E5 RID: 229
		public const int SPI_SETMOUSEBUTTONSWAP = 33;

		// Token: 0x040000E6 RID: 230
		public const int SPI_SETICONTITLELOGFONT = 34;

		// Token: 0x040000E7 RID: 231
		public const int SPI_GETFASTTASKSWITCH = 35;

		// Token: 0x040000E8 RID: 232
		public const int SPI_SETFASTTASKSWITCH = 36;

		// Token: 0x040000E9 RID: 233
		public const int SPI_SETDRAGFULLWINDOWS = 37;

		// Token: 0x040000EA RID: 234
		public const int SPI_GETDRAGFULLWINDOWS = 38;

		// Token: 0x040000EB RID: 235
		public const int SPI_GETNONCLIENTMETRICS = 41;

		// Token: 0x040000EC RID: 236
		public const int SPI_SETNONCLIENTMETRICS = 42;

		// Token: 0x040000ED RID: 237
		public const int SPI_GETMINIMIZEDMETRICS = 43;

		// Token: 0x040000EE RID: 238
		public const int SPI_SETMINIMIZEDMETRICS = 44;

		// Token: 0x040000EF RID: 239
		public const int SPI_GETICONMETRICS = 45;

		// Token: 0x040000F0 RID: 240
		public const int SPI_SETICONMETRICS = 46;

		// Token: 0x040000F1 RID: 241
		public const int SPI_SETWORKAREA = 47;

		// Token: 0x040000F2 RID: 242
		public const int SPI_GETWORKAREA = 48;

		// Token: 0x040000F3 RID: 243
		public const int SPI_SETPENWINDOWS = 49;

		// Token: 0x040000F4 RID: 244
		public const int SPI_GETHIGHCONTRAST = 66;

		// Token: 0x040000F5 RID: 245
		public const int SPI_SETHIGHCONTRAST = 67;

		// Token: 0x040000F6 RID: 246
		public const int SPI_GETKEYBOARDPREF = 68;

		// Token: 0x040000F7 RID: 247
		public const int SPI_SETKEYBOARDPREF = 69;

		// Token: 0x040000F8 RID: 248
		public const int SPI_GETSCREENREADER = 70;

		// Token: 0x040000F9 RID: 249
		public const int SPI_SETSCREENREADER = 71;

		// Token: 0x040000FA RID: 250
		public const int SPI_GETANIMATION = 72;

		// Token: 0x040000FB RID: 251
		public const int SPI_SETANIMATION = 73;

		// Token: 0x040000FC RID: 252
		public const int SPI_GETFONTSMOOTHING = 74;

		// Token: 0x040000FD RID: 253
		public const int SPI_SETFONTSMOOTHING = 75;

		// Token: 0x040000FE RID: 254
		public const int SPI_SETDRAGWIDTH = 76;

		// Token: 0x040000FF RID: 255
		public const int SPI_SETDRAGHEIGHT = 77;

		// Token: 0x04000100 RID: 256
		public const int SPI_SETHANDHELD = 78;

		// Token: 0x04000101 RID: 257
		public const int SPI_GETLOWPOWERTIMEOUT = 79;

		// Token: 0x04000102 RID: 258
		public const int SPI_GETPOWEROFFTIMEOUT = 80;

		// Token: 0x04000103 RID: 259
		public const int SPI_SETLOWPOWERTIMEOUT = 81;

		// Token: 0x04000104 RID: 260
		public const int SPI_SETPOWEROFFTIMEOUT = 82;

		// Token: 0x04000105 RID: 261
		public const int SPI_GETLOWPOWERACTIVE = 83;

		// Token: 0x04000106 RID: 262
		public const int SPI_GETPOWEROFFACTIVE = 84;

		// Token: 0x04000107 RID: 263
		public const int SPI_SETLOWPOWERACTIVE = 85;

		// Token: 0x04000108 RID: 264
		public const int SPI_SETPOWEROFFACTIVE = 86;

		// Token: 0x04000109 RID: 265
		public const int SPI_SETCURSORS = 87;

		// Token: 0x0400010A RID: 266
		public const int SPI_SETICONS = 88;

		// Token: 0x0400010B RID: 267
		public const int SPI_GETDEFAULTINPUTLANG = 89;

		// Token: 0x0400010C RID: 268
		public const int SPI_SETDEFAULTINPUTLANG = 90;

		// Token: 0x0400010D RID: 269
		public const int SPI_SETLANGTOGGLE = 91;

		// Token: 0x0400010E RID: 270
		public const int SPI_GETWINDOWSEXTENSION = 92;

		// Token: 0x0400010F RID: 271
		public const int SPI_SETMOUSETRAILS = 93;

		// Token: 0x04000110 RID: 272
		public const int SPI_GETMOUSETRAILS = 94;

		// Token: 0x04000111 RID: 273
		public const int SPI_SETSCREENSAVERRUNNING = 97;

		// Token: 0x04000112 RID: 274
		public const int SPI_SCREENSAVERRUNNING = 97;

		// Token: 0x04000113 RID: 275
		public const int SPI_GETFILTERKEYS = 50;

		// Token: 0x04000114 RID: 276
		public const int SPI_SETFILTERKEYS = 51;

		// Token: 0x04000115 RID: 277
		public const int SPI_GETTOGGLEKEYS = 52;

		// Token: 0x04000116 RID: 278
		public const int SPI_SETTOGGLEKEYS = 53;

		// Token: 0x04000117 RID: 279
		public const int SPI_GETMOUSEKEYS = 54;

		// Token: 0x04000118 RID: 280
		public const int SPI_SETMOUSEKEYS = 55;

		// Token: 0x04000119 RID: 281
		public const int SPI_GETSHOWSOUNDS = 56;

		// Token: 0x0400011A RID: 282
		public const int SPI_SETSHOWSOUNDS = 57;

		// Token: 0x0400011B RID: 283
		public const int SPI_GETSTICKYKEYS = 58;

		// Token: 0x0400011C RID: 284
		public const int SPI_SETSTICKYKEYS = 59;

		// Token: 0x0400011D RID: 285
		public const int SPI_GETACCESSTIMEOUT = 60;

		// Token: 0x0400011E RID: 286
		public const int SPI_SETACCESSTIMEOUT = 61;

		// Token: 0x0400011F RID: 287
		public const int SPI_GETSERIALKEYS = 62;

		// Token: 0x04000120 RID: 288
		public const int SPI_SETSERIALKEYS = 63;

		// Token: 0x04000121 RID: 289
		public const int SPI_GETSOUNDSENTRY = 64;

		// Token: 0x04000122 RID: 290
		public const int SPI_SETSOUNDSENTRY = 65;

		// Token: 0x04000123 RID: 291
		public const int SPI_GETSNAPTODEFBUTTON = 95;

		// Token: 0x04000124 RID: 292
		public const int SPI_SETSNAPTODEFBUTTON = 96;

		// Token: 0x04000125 RID: 293
		public const int SPI_GETMOUSEHOVERWIDTH = 98;

		// Token: 0x04000126 RID: 294
		public const int SPI_SETMOUSEHOVERWIDTH = 99;

		// Token: 0x04000127 RID: 295
		public const int SPI_GETMOUSEHOVERHEIGHT = 100;

		// Token: 0x04000128 RID: 296
		public const int SPI_SETMOUSEHOVERHEIGHT = 101;

		// Token: 0x04000129 RID: 297
		public const int SPI_GETMOUSEHOVERTIME = 102;

		// Token: 0x0400012A RID: 298
		public const int SPI_SETMOUSEHOVERTIME = 103;

		// Token: 0x0400012B RID: 299
		public const int SPI_GETWHEELSCROLLLINES = 104;

		// Token: 0x0400012C RID: 300
		public const int SPI_SETWHEELSCROLLLINES = 105;

		// Token: 0x0400012D RID: 301
		public const int SPI_GETMENUSHOWDELAY = 106;

		// Token: 0x0400012E RID: 302
		public const int SPI_SETMENUSHOWDELAY = 107;

		// Token: 0x0400012F RID: 303
		public const int SPI_GETSHOWIMEUI = 110;

		// Token: 0x04000130 RID: 304
		public const int SPI_SETSHOWIMEUI = 111;

		// Token: 0x04000131 RID: 305
		public const int SPI_GETMOUSESPEED = 112;

		// Token: 0x04000132 RID: 306
		public const int SPI_SETMOUSESPEED = 113;

		// Token: 0x04000133 RID: 307
		public const int SPI_GETSCREENSAVERRUNNING = 114;

		// Token: 0x04000134 RID: 308
		public const int SPI_GETDESKWALLPAPER = 115;

		// Token: 0x04000135 RID: 309
		public const int SPI_GETACTIVEWINDOWTRACKING = 4096;

		// Token: 0x04000136 RID: 310
		public const int SPI_SETACTIVEWINDOWTRACKING = 4097;

		// Token: 0x04000137 RID: 311
		public const int SPI_GETMENUANIMATION = 4098;

		// Token: 0x04000138 RID: 312
		public const int SPI_SETMENUANIMATION = 4099;

		// Token: 0x04000139 RID: 313
		public const int SPI_GETCOMBOBOXANIMATION = 4100;

		// Token: 0x0400013A RID: 314
		public const int SPI_SETCOMBOBOXANIMATION = 4101;

		// Token: 0x0400013B RID: 315
		public const int SPI_GETLISTBOXSMOOTHSCROLLING = 4102;

		// Token: 0x0400013C RID: 316
		public const int SPI_SETLISTBOXSMOOTHSCROLLING = 4103;

		// Token: 0x0400013D RID: 317
		public const int SPI_GETGRADIENTCAPTIONS = 4104;

		// Token: 0x0400013E RID: 318
		public const int SPI_SETGRADIENTCAPTIONS = 4105;

		// Token: 0x0400013F RID: 319
		public const int SPI_GETKEYBOARDCUES = 4106;

		// Token: 0x04000140 RID: 320
		public const int SPI_SETKEYBOARDCUES = 4107;

		// Token: 0x04000141 RID: 321
		public const int SPI_GETMENUUNDERLINES = 4106;

		// Token: 0x04000142 RID: 322
		public const int SPI_SETMENUUNDERLINES = 4107;

		// Token: 0x04000143 RID: 323
		public const int SPI_GETACTIVEWNDTRKZORDER = 4108;

		// Token: 0x04000144 RID: 324
		public const int SPI_SETACTIVEWNDTRKZORDER = 4109;

		// Token: 0x04000145 RID: 325
		public const int SPI_GETHOTTRACKING = 4110;

		// Token: 0x04000146 RID: 326
		public const int SPI_SETHOTTRACKING = 4111;

		// Token: 0x04000147 RID: 327
		public const int SPI_GETMENUFADE = 4114;

		// Token: 0x04000148 RID: 328
		public const int SPI_SETMENUFADE = 4115;

		// Token: 0x04000149 RID: 329
		public const int SPI_GETSELECTIONFADE = 4116;

		// Token: 0x0400014A RID: 330
		public const int SPI_SETSELECTIONFADE = 4117;

		// Token: 0x0400014B RID: 331
		public const int SPI_GETTOOLTIPANIMATION = 4118;

		// Token: 0x0400014C RID: 332
		public const int SPI_SETTOOLTIPANIMATION = 4119;

		// Token: 0x0400014D RID: 333
		public const int SPI_GETTOOLTIPFADE = 4120;

		// Token: 0x0400014E RID: 334
		public const int SPI_SETTOOLTIPFADE = 4121;

		// Token: 0x0400014F RID: 335
		public const int SPI_GETCURSORSHADOW = 4122;

		// Token: 0x04000150 RID: 336
		public const int SPI_SETCURSORSHADOW = 4123;

		// Token: 0x04000151 RID: 337
		public const int SPI_GETUIEFFECTS = 4158;

		// Token: 0x04000152 RID: 338
		public const int SPI_SETUIEFFECTS = 4159;

		// Token: 0x04000153 RID: 339
		public const int SPI_GETFOREGROUNDLOCKTIMEOUT = 8192;

		// Token: 0x04000154 RID: 340
		public const int SPI_SETFOREGROUNDLOCKTIMEOUT = 8193;

		// Token: 0x04000155 RID: 341
		public const int SPI_GETACTIVEWNDTRKTIMEOUT = 8194;

		// Token: 0x04000156 RID: 342
		public const int SPI_SETACTIVEWNDTRKTIMEOUT = 8195;

		// Token: 0x04000157 RID: 343
		public const int SPI_GETFOREGROUNDFLASHCOUNT = 8196;

		// Token: 0x04000158 RID: 344
		public const int SPI_SETFOREGROUNDFLASHCOUNT = 8197;

		// Token: 0x04000159 RID: 345
		public const int SPI_GETCARETWIDTH = 8198;

		// Token: 0x0400015A RID: 346
		public const int SPI_SETCARETWIDTH = 8199;

		// Token: 0x0400015B RID: 347
		public const uint STATUS_INFO_LENGTH_MISMATCH = 3221225476U;

		// Token: 0x0400015C RID: 348
		public const int PBT_APMQUERYSUSPEND = 0;

		// Token: 0x0400015D RID: 349
		public const int PBT_APMQUERYSTANDBY = 1;

		// Token: 0x0400015E RID: 350
		public const int PBT_APMQUERYSUSPENDFAILED = 2;

		// Token: 0x0400015F RID: 351
		public const int PBT_APMQUERYSTANDBYFAILED = 3;

		// Token: 0x04000160 RID: 352
		public const int PBT_APMSUSPEND = 4;

		// Token: 0x04000161 RID: 353
		public const int PBT_APMSTANDBY = 5;

		// Token: 0x04000162 RID: 354
		public const int PBT_APMRESUMECRITICAL = 6;

		// Token: 0x04000163 RID: 355
		public const int PBT_APMRESUMESUSPEND = 7;

		// Token: 0x04000164 RID: 356
		public const int PBT_APMRESUMESTANDBY = 8;

		// Token: 0x04000165 RID: 357
		public const int PBT_APMBATTERYLOW = 9;

		// Token: 0x04000166 RID: 358
		public const int PBT_APMPOWERSTATUSCHANGE = 10;

		// Token: 0x04000167 RID: 359
		public const int PBT_APMOEMEVENT = 11;

		// Token: 0x04000168 RID: 360
		public const int STARTF_USESHOWWINDOW = 1;

		// Token: 0x04000169 RID: 361
		public const int FILE_MAP_WRITE = 2;

		// Token: 0x0400016A RID: 362
		public const int FILE_MAP_READ = 4;

		// Token: 0x0400016B RID: 363
		public const int PAGE_READWRITE = 4;

		// Token: 0x0400016C RID: 364
		public const int GENERIC_EXECUTE = 536870912;

		// Token: 0x0400016D RID: 365
		public const int GENERIC_ALL = 268435456;

		// Token: 0x0400016E RID: 366
		public const int ERROR_NOT_READY = 21;

		// Token: 0x0400016F RID: 367
		public const int ERROR_LOCK_FAILED = 167;

		// Token: 0x04000170 RID: 368
		public const int ERROR_BUSY = 170;

		// Token: 0x04000171 RID: 369
		public const int IMPERSONATION_LEVEL_SecurityAnonymous = 0;

		// Token: 0x04000172 RID: 370
		public const int IMPERSONATION_LEVEL_SecurityIdentification = 1;

		// Token: 0x04000173 RID: 371
		public const int IMPERSONATION_LEVEL_SecurityImpersonation = 2;

		// Token: 0x04000174 RID: 372
		public const int IMPERSONATION_LEVEL_SecurityDelegation = 3;

		// Token: 0x04000175 RID: 373
		public const int TOKEN_TYPE_TokenPrimary = 1;

		// Token: 0x04000176 RID: 374
		public const int TOKEN_TYPE_TokenImpersonation = 2;

		// Token: 0x04000177 RID: 375
		public const int TOKEN_ALL_ACCESS = 983551;

		// Token: 0x04000178 RID: 376
		public const int TOKEN_EXECUTE = 131072;

		// Token: 0x04000179 RID: 377
		public const int TOKEN_READ = 131080;

		// Token: 0x0400017A RID: 378
		public const int TOKEN_IMPERSONATE = 4;

		// Token: 0x0400017B RID: 379
		public const int PIPE_ACCESS_INBOUND = 1;

		// Token: 0x0400017C RID: 380
		public const int PIPE_ACCESS_OUTBOUND = 2;

		// Token: 0x0400017D RID: 381
		public const int PIPE_ACCESS_DUPLEX = 3;

		// Token: 0x0400017E RID: 382
		public const int PIPE_WAIT = 0;

		// Token: 0x0400017F RID: 383
		public const int PIPE_NOWAIT = 1;

		// Token: 0x04000180 RID: 384
		public const int PIPE_READMODE_BYTE = 0;

		// Token: 0x04000181 RID: 385
		public const int PIPE_READMODE_MESSAGE = 2;

		// Token: 0x04000182 RID: 386
		public const int PIPE_TYPE_BYTE = 0;

		// Token: 0x04000183 RID: 387
		public const int PIPE_TYPE_MESSAGE = 4;

		// Token: 0x04000184 RID: 388
		public const int PIPE_SINGLE_INSTANCES = 1;

		// Token: 0x04000185 RID: 389
		public const int PIPE_UNLIMITED_INSTANCES = 255;

		// Token: 0x04000186 RID: 390
		public const int FILE_FLAG_OVERLAPPED = 1073741824;

		// Token: 0x04000187 RID: 391
		public const int PM_REMOVE = 1;

		// Token: 0x04000188 RID: 392
		public const int QS_KEY = 1;

		// Token: 0x04000189 RID: 393
		public const int QS_MOUSEMOVE = 2;

		// Token: 0x0400018A RID: 394
		public const int QS_MOUSEBUTTON = 4;

		// Token: 0x0400018B RID: 395
		public const int QS_POSTMESSAGE = 8;

		// Token: 0x0400018C RID: 396
		public const int QS_TIMER = 16;

		// Token: 0x0400018D RID: 397
		public const int QS_PAINT = 32;

		// Token: 0x0400018E RID: 398
		public const int QS_SENDMESSAGE = 64;

		// Token: 0x0400018F RID: 399
		public const int QS_HOTKEY = 128;

		// Token: 0x04000190 RID: 400
		public const int QS_ALLPOSTMESSAGE = 256;

		// Token: 0x04000191 RID: 401
		public const int QS_MOUSE = 6;

		// Token: 0x04000192 RID: 402
		public const int QS_INPUT = 7;

		// Token: 0x04000193 RID: 403
		public const int QS_ALLEVENTS = 191;

		// Token: 0x04000194 RID: 404
		public const int QS_ALLINPUT = 255;

		// Token: 0x04000195 RID: 405
		public const int MWMO_INPUTAVAILABLE = 4;

		// Token: 0x04000196 RID: 406
		internal const byte ONESTOPBIT = 0;

		// Token: 0x04000197 RID: 407
		internal const byte ONE5STOPBITS = 1;

		// Token: 0x04000198 RID: 408
		internal const byte TWOSTOPBITS = 2;

		// Token: 0x04000199 RID: 409
		internal const int DTR_CONTROL_DISABLE = 0;

		// Token: 0x0400019A RID: 410
		internal const int DTR_CONTROL_ENABLE = 1;

		// Token: 0x0400019B RID: 411
		internal const int DTR_CONTROL_HANDSHAKE = 2;

		// Token: 0x0400019C RID: 412
		internal const int RTS_CONTROL_DISABLE = 0;

		// Token: 0x0400019D RID: 413
		internal const int RTS_CONTROL_ENABLE = 1;

		// Token: 0x0400019E RID: 414
		internal const int RTS_CONTROL_HANDSHAKE = 2;

		// Token: 0x0400019F RID: 415
		internal const int RTS_CONTROL_TOGGLE = 3;

		// Token: 0x040001A0 RID: 416
		internal const int MS_CTS_ON = 16;

		// Token: 0x040001A1 RID: 417
		internal const int MS_DSR_ON = 32;

		// Token: 0x040001A2 RID: 418
		internal const int MS_RING_ON = 64;

		// Token: 0x040001A3 RID: 419
		internal const int MS_RLSD_ON = 128;

		// Token: 0x040001A4 RID: 420
		internal const byte EOFCHAR = 26;

		// Token: 0x040001A5 RID: 421
		internal const int FBINARY = 0;

		// Token: 0x040001A6 RID: 422
		internal const int FPARITY = 1;

		// Token: 0x040001A7 RID: 423
		internal const int FOUTXCTSFLOW = 2;

		// Token: 0x040001A8 RID: 424
		internal const int FOUTXDSRFLOW = 3;

		// Token: 0x040001A9 RID: 425
		internal const int FDTRCONTROL = 4;

		// Token: 0x040001AA RID: 426
		internal const int FDSRSENSITIVITY = 6;

		// Token: 0x040001AB RID: 427
		internal const int FTXCONTINUEONXOFF = 7;

		// Token: 0x040001AC RID: 428
		internal const int FOUTX = 8;

		// Token: 0x040001AD RID: 429
		internal const int FINX = 9;

		// Token: 0x040001AE RID: 430
		internal const int FERRORCHAR = 10;

		// Token: 0x040001AF RID: 431
		internal const int FNULL = 11;

		// Token: 0x040001B0 RID: 432
		internal const int FRTSCONTROL = 12;

		// Token: 0x040001B1 RID: 433
		internal const int FABORTONOERROR = 14;

		// Token: 0x040001B2 RID: 434
		internal const int FDUMMY2 = 15;

		// Token: 0x040001B3 RID: 435
		internal const int PURGE_TXABORT = 1;

		// Token: 0x040001B4 RID: 436
		internal const int PURGE_RXABORT = 2;

		// Token: 0x040001B5 RID: 437
		internal const int PURGE_TXCLEAR = 4;

		// Token: 0x040001B6 RID: 438
		internal const int PURGE_RXCLEAR = 8;

		// Token: 0x040001B7 RID: 439
		internal const byte DEFAULTXONCHAR = 17;

		// Token: 0x040001B8 RID: 440
		internal const byte DEFAULTXOFFCHAR = 19;

		// Token: 0x040001B9 RID: 441
		internal const int SETRTS = 3;

		// Token: 0x040001BA RID: 442
		internal const int CLRRTS = 4;

		// Token: 0x040001BB RID: 443
		internal const int SETDTR = 5;

		// Token: 0x040001BC RID: 444
		internal const int CLRDTR = 6;

		// Token: 0x040001BD RID: 445
		internal const int EV_RXCHAR = 1;

		// Token: 0x040001BE RID: 446
		internal const int EV_RXFLAG = 2;

		// Token: 0x040001BF RID: 447
		internal const int EV_CTS = 8;

		// Token: 0x040001C0 RID: 448
		internal const int EV_DSR = 16;

		// Token: 0x040001C1 RID: 449
		internal const int EV_RLSD = 32;

		// Token: 0x040001C2 RID: 450
		internal const int EV_BREAK = 64;

		// Token: 0x040001C3 RID: 451
		internal const int EV_ERR = 128;

		// Token: 0x040001C4 RID: 452
		internal const int EV_RING = 256;

		// Token: 0x040001C5 RID: 453
		internal const int ALL_EVENTS = 507;

		// Token: 0x040001C6 RID: 454
		internal const int CE_RXOVER = 1;

		// Token: 0x040001C7 RID: 455
		internal const int CE_OVERRUN = 2;

		// Token: 0x040001C8 RID: 456
		internal const int CE_PARITY = 4;

		// Token: 0x040001C9 RID: 457
		internal const int CE_FRAME = 8;

		// Token: 0x040001CA RID: 458
		internal const int CE_BREAK = 16;

		// Token: 0x040001CB RID: 459
		internal const int CE_TXFULL = 256;

		// Token: 0x040001CC RID: 460
		internal const int MAXDWORD = -1;

		// Token: 0x040001CD RID: 461
		internal const int NOPARITY = 0;

		// Token: 0x040001CE RID: 462
		internal const int ODDPARITY = 1;

		// Token: 0x040001CF RID: 463
		internal const int EVENPARITY = 2;

		// Token: 0x040001D0 RID: 464
		internal const int MARKPARITY = 3;

		// Token: 0x040001D1 RID: 465
		internal const int SPACEPARITY = 4;

		// Token: 0x040001D2 RID: 466
		internal const int SDDL_REVISION_1 = 1;

		// Token: 0x040001D3 RID: 467
		public const int SECURITY_DESCRIPTOR_REVISION = 1;

		// Token: 0x040001D4 RID: 468
		public const int HKEY_PERFORMANCE_DATA = -2147483644;

		// Token: 0x040001D5 RID: 469
		public const int DWORD_SIZE = 4;

		// Token: 0x040001D6 RID: 470
		public const int LARGE_INTEGER_SIZE = 8;

		// Token: 0x040001D7 RID: 471
		public const int PERF_NO_INSTANCES = -1;

		// Token: 0x040001D8 RID: 472
		public const int PERF_SIZE_DWORD = 0;

		// Token: 0x040001D9 RID: 473
		public const int PERF_SIZE_LARGE = 256;

		// Token: 0x040001DA RID: 474
		public const int PERF_SIZE_ZERO = 512;

		// Token: 0x040001DB RID: 475
		public const int PERF_SIZE_VARIABLE_LEN = 768;

		// Token: 0x040001DC RID: 476
		public const int PERF_NO_UNIQUE_ID = -1;

		// Token: 0x040001DD RID: 477
		public const int PERF_TYPE_NUMBER = 0;

		// Token: 0x040001DE RID: 478
		public const int PERF_TYPE_COUNTER = 1024;

		// Token: 0x040001DF RID: 479
		public const int PERF_TYPE_TEXT = 2048;

		// Token: 0x040001E0 RID: 480
		public const int PERF_TYPE_ZERO = 3072;

		// Token: 0x040001E1 RID: 481
		public const int PERF_NUMBER_HEX = 0;

		// Token: 0x040001E2 RID: 482
		public const int PERF_NUMBER_DECIMAL = 65536;

		// Token: 0x040001E3 RID: 483
		public const int PERF_NUMBER_DEC_1000 = 131072;

		// Token: 0x040001E4 RID: 484
		public const int PERF_COUNTER_VALUE = 0;

		// Token: 0x040001E5 RID: 485
		public const int PERF_COUNTER_RATE = 65536;

		// Token: 0x040001E6 RID: 486
		public const int PERF_COUNTER_FRACTION = 131072;

		// Token: 0x040001E7 RID: 487
		public const int PERF_COUNTER_BASE = 196608;

		// Token: 0x040001E8 RID: 488
		public const int PERF_COUNTER_ELAPSED = 262144;

		// Token: 0x040001E9 RID: 489
		public const int PERF_COUNTER_QUEUELEN = 327680;

		// Token: 0x040001EA RID: 490
		public const int PERF_COUNTER_HISTOGRAM = 393216;

		// Token: 0x040001EB RID: 491
		public const int PERF_COUNTER_PRECISION = 458752;

		// Token: 0x040001EC RID: 492
		public const int PERF_TEXT_UNICODE = 0;

		// Token: 0x040001ED RID: 493
		public const int PERF_TEXT_ASCII = 65536;

		// Token: 0x040001EE RID: 494
		public const int PERF_TIMER_TICK = 0;

		// Token: 0x040001EF RID: 495
		public const int PERF_TIMER_100NS = 1048576;

		// Token: 0x040001F0 RID: 496
		public const int PERF_OBJECT_TIMER = 2097152;

		// Token: 0x040001F1 RID: 497
		public const int PERF_DELTA_COUNTER = 4194304;

		// Token: 0x040001F2 RID: 498
		public const int PERF_DELTA_BASE = 8388608;

		// Token: 0x040001F3 RID: 499
		public const int PERF_INVERSE_COUNTER = 16777216;

		// Token: 0x040001F4 RID: 500
		public const int PERF_MULTI_COUNTER = 33554432;

		// Token: 0x040001F5 RID: 501
		public const int PERF_DISPLAY_NO_SUFFIX = 0;

		// Token: 0x040001F6 RID: 502
		public const int PERF_DISPLAY_PER_SEC = 268435456;

		// Token: 0x040001F7 RID: 503
		public const int PERF_DISPLAY_PERCENT = 536870912;

		// Token: 0x040001F8 RID: 504
		public const int PERF_DISPLAY_SECONDS = 805306368;

		// Token: 0x040001F9 RID: 505
		public const int PERF_DISPLAY_NOSHOW = 1073741824;

		// Token: 0x040001FA RID: 506
		public const int PERF_COUNTER_COUNTER = 272696320;

		// Token: 0x040001FB RID: 507
		public const int PERF_COUNTER_TIMER = 541132032;

		// Token: 0x040001FC RID: 508
		public const int PERF_COUNTER_QUEUELEN_TYPE = 4523008;

		// Token: 0x040001FD RID: 509
		public const int PERF_COUNTER_LARGE_QUEUELEN_TYPE = 4523264;

		// Token: 0x040001FE RID: 510
		public const int PERF_COUNTER_100NS_QUEUELEN_TYPE = 5571840;

		// Token: 0x040001FF RID: 511
		public const int PERF_COUNTER_OBJ_TIME_QUEUELEN_TYPE = 6620416;

		// Token: 0x04000200 RID: 512
		public const int PERF_COUNTER_BULK_COUNT = 272696576;

		// Token: 0x04000201 RID: 513
		public const int PERF_COUNTER_TEXT = 2816;

		// Token: 0x04000202 RID: 514
		public const int PERF_COUNTER_RAWCOUNT = 65536;

		// Token: 0x04000203 RID: 515
		public const int PERF_COUNTER_LARGE_RAWCOUNT = 65792;

		// Token: 0x04000204 RID: 516
		public const int PERF_COUNTER_RAWCOUNT_HEX = 0;

		// Token: 0x04000205 RID: 517
		public const int PERF_COUNTER_LARGE_RAWCOUNT_HEX = 256;

		// Token: 0x04000206 RID: 518
		public const int PERF_SAMPLE_FRACTION = 549585920;

		// Token: 0x04000207 RID: 519
		public const int PERF_SAMPLE_COUNTER = 4260864;

		// Token: 0x04000208 RID: 520
		public const int PERF_COUNTER_NODATA = 1073742336;

		// Token: 0x04000209 RID: 521
		public const int PERF_COUNTER_TIMER_INV = 557909248;

		// Token: 0x0400020A RID: 522
		public const int PERF_SAMPLE_BASE = 1073939457;

		// Token: 0x0400020B RID: 523
		public const int PERF_AVERAGE_TIMER = 805438464;

		// Token: 0x0400020C RID: 524
		public const int PERF_AVERAGE_BASE = 1073939458;

		// Token: 0x0400020D RID: 525
		public const int PERF_OBJ_TIME_TIMER = 543229184;

		// Token: 0x0400020E RID: 526
		public const int PERF_AVERAGE_BULK = 1073874176;

		// Token: 0x0400020F RID: 527
		public const int PERF_OBJ_TIME_TIME = 543229184;

		// Token: 0x04000210 RID: 528
		public const int PERF_100NSEC_TIMER = 542180608;

		// Token: 0x04000211 RID: 529
		public const int PERF_100NSEC_TIMER_INV = 558957824;

		// Token: 0x04000212 RID: 530
		public const int PERF_COUNTER_MULTI_TIMER = 574686464;

		// Token: 0x04000213 RID: 531
		public const int PERF_COUNTER_MULTI_TIMER_INV = 591463680;

		// Token: 0x04000214 RID: 532
		public const int PERF_COUNTER_MULTI_BASE = 1107494144;

		// Token: 0x04000215 RID: 533
		public const int PERF_100NSEC_MULTI_TIMER = 575735040;

		// Token: 0x04000216 RID: 534
		public const int PERF_100NSEC_MULTI_TIMER_INV = 592512256;

		// Token: 0x04000217 RID: 535
		public const int PERF_RAW_FRACTION = 537003008;

		// Token: 0x04000218 RID: 536
		public const int PERF_LARGE_RAW_FRACTION = 537003264;

		// Token: 0x04000219 RID: 537
		public const int PERF_RAW_BASE = 1073939459;

		// Token: 0x0400021A RID: 538
		public const int PERF_LARGE_RAW_BASE = 1073939712;

		// Token: 0x0400021B RID: 539
		public const int PERF_ELAPSED_TIME = 807666944;

		// Token: 0x0400021C RID: 540
		public const int PERF_COUNTER_DELTA = 4195328;

		// Token: 0x0400021D RID: 541
		public const int PERF_COUNTER_LARGE_DELTA = 4195584;

		// Token: 0x0400021E RID: 542
		public const int PERF_PRECISION_SYSTEM_TIMER = 541525248;

		// Token: 0x0400021F RID: 543
		public const int PERF_PRECISION_100NS_TIMER = 542573824;

		// Token: 0x04000220 RID: 544
		public const int PERF_PRECISION_OBJECT_TIMER = 543622400;

		// Token: 0x04000221 RID: 545
		public const uint PDH_FMT_DOUBLE = 512U;

		// Token: 0x04000222 RID: 546
		public const uint PDH_FMT_NOSCALE = 4096U;

		// Token: 0x04000223 RID: 547
		public const uint PDH_FMT_NOCAP100 = 32768U;

		// Token: 0x04000224 RID: 548
		public const int PERF_DETAIL_NOVICE = 100;

		// Token: 0x04000225 RID: 549
		public const int PERF_DETAIL_ADVANCED = 200;

		// Token: 0x04000226 RID: 550
		public const int PERF_DETAIL_EXPERT = 300;

		// Token: 0x04000227 RID: 551
		public const int PERF_DETAIL_WIZARD = 400;

		// Token: 0x04000228 RID: 552
		public const int FORMAT_MESSAGE_ALLOCATE_BUFFER = 256;

		// Token: 0x04000229 RID: 553
		public const int FORMAT_MESSAGE_IGNORE_INSERTS = 512;

		// Token: 0x0400022A RID: 554
		public const int FORMAT_MESSAGE_FROM_STRING = 1024;

		// Token: 0x0400022B RID: 555
		public const int FORMAT_MESSAGE_FROM_HMODULE = 2048;

		// Token: 0x0400022C RID: 556
		public const int FORMAT_MESSAGE_FROM_SYSTEM = 4096;

		// Token: 0x0400022D RID: 557
		public const int FORMAT_MESSAGE_ARGUMENT_ARRAY = 8192;

		// Token: 0x0400022E RID: 558
		public const int FORMAT_MESSAGE_MAX_WIDTH_MASK = 255;

		// Token: 0x0400022F RID: 559
		public const int LOAD_WITH_ALTERED_SEARCH_PATH = 8;

		// Token: 0x04000230 RID: 560
		public const int LOAD_LIBRARY_AS_DATAFILE = 2;

		// Token: 0x04000231 RID: 561
		public const int SEEK_READ = 2;

		// Token: 0x04000232 RID: 562
		public const int FORWARDS_READ = 4;

		// Token: 0x04000233 RID: 563
		public const int BACKWARDS_READ = 8;

		// Token: 0x04000234 RID: 564
		public const int ERROR_EVENTLOG_FILE_CHANGED = 1503;

		// Token: 0x04000235 RID: 565
		public const int NtPerfCounterSizeDword = 0;

		// Token: 0x04000236 RID: 566
		public const int NtPerfCounterSizeLarge = 256;

		// Token: 0x04000237 RID: 567
		public const int SHGFI_USEFILEATTRIBUTES = 16;

		// Token: 0x04000238 RID: 568
		public const int SHGFI_TYPENAME = 1024;

		// Token: 0x04000239 RID: 569
		public const int NtQueryProcessBasicInfo = 0;

		// Token: 0x0400023A RID: 570
		public const int NtQuerySystemProcessInformation = 5;

		// Token: 0x0400023B RID: 571
		public const int SEE_MASK_CLASSNAME = 1;

		// Token: 0x0400023C RID: 572
		public const int SEE_MASK_CLASSKEY = 3;

		// Token: 0x0400023D RID: 573
		public const int SEE_MASK_IDLIST = 4;

		// Token: 0x0400023E RID: 574
		public const int SEE_MASK_INVOKEIDLIST = 12;

		// Token: 0x0400023F RID: 575
		public const int SEE_MASK_ICON = 16;

		// Token: 0x04000240 RID: 576
		public const int SEE_MASK_HOTKEY = 32;

		// Token: 0x04000241 RID: 577
		public const int SEE_MASK_NOCLOSEPROCESS = 64;

		// Token: 0x04000242 RID: 578
		public const int SEE_MASK_CONNECTNETDRV = 128;

		// Token: 0x04000243 RID: 579
		public const int SEE_MASK_FLAG_DDEWAIT = 256;

		// Token: 0x04000244 RID: 580
		public const int SEE_MASK_DOENVSUBST = 512;

		// Token: 0x04000245 RID: 581
		public const int SEE_MASK_FLAG_NO_UI = 1024;

		// Token: 0x04000246 RID: 582
		public const int SEE_MASK_UNICODE = 16384;

		// Token: 0x04000247 RID: 583
		public const int SEE_MASK_NO_CONSOLE = 32768;

		// Token: 0x04000248 RID: 584
		public const int SEE_MASK_ASYNCOK = 1048576;

		// Token: 0x04000249 RID: 585
		public const int TH32CS_SNAPHEAPLIST = 1;

		// Token: 0x0400024A RID: 586
		public const int TH32CS_SNAPPROCESS = 2;

		// Token: 0x0400024B RID: 587
		public const int TH32CS_SNAPTHREAD = 4;

		// Token: 0x0400024C RID: 588
		public const int TH32CS_SNAPMODULE = 8;

		// Token: 0x0400024D RID: 589
		public const int TH32CS_INHERIT = -2147483648;

		// Token: 0x0400024E RID: 590
		public const int PROCESS_TERMINATE = 1;

		// Token: 0x0400024F RID: 591
		public const int PROCESS_CREATE_THREAD = 2;

		// Token: 0x04000250 RID: 592
		public const int PROCESS_SET_SESSIONID = 4;

		// Token: 0x04000251 RID: 593
		public const int PROCESS_VM_OPERATION = 8;

		// Token: 0x04000252 RID: 594
		public const int PROCESS_VM_READ = 16;

		// Token: 0x04000253 RID: 595
		public const int PROCESS_VM_WRITE = 32;

		// Token: 0x04000254 RID: 596
		public const int PROCESS_DUP_HANDLE = 64;

		// Token: 0x04000255 RID: 597
		public const int PROCESS_CREATE_PROCESS = 128;

		// Token: 0x04000256 RID: 598
		public const int PROCESS_SET_QUOTA = 256;

		// Token: 0x04000257 RID: 599
		public const int PROCESS_SET_INFORMATION = 512;

		// Token: 0x04000258 RID: 600
		public const int PROCESS_QUERY_INFORMATION = 1024;

		// Token: 0x04000259 RID: 601
		public const int PROCESS_QUERY_LIMITED_INFORMATION = 4096;

		// Token: 0x0400025A RID: 602
		public const int STANDARD_RIGHTS_REQUIRED = 983040;

		// Token: 0x0400025B RID: 603
		public const int SYNCHRONIZE = 1048576;

		// Token: 0x0400025C RID: 604
		public const int PROCESS_ALL_ACCESS = 2035711;

		// Token: 0x0400025D RID: 605
		public const int THREAD_TERMINATE = 1;

		// Token: 0x0400025E RID: 606
		public const int THREAD_SUSPEND_RESUME = 2;

		// Token: 0x0400025F RID: 607
		public const int THREAD_GET_CONTEXT = 8;

		// Token: 0x04000260 RID: 608
		public const int THREAD_SET_CONTEXT = 16;

		// Token: 0x04000261 RID: 609
		public const int THREAD_SET_INFORMATION = 32;

		// Token: 0x04000262 RID: 610
		public const int THREAD_QUERY_INFORMATION = 64;

		// Token: 0x04000263 RID: 611
		public const int THREAD_SET_THREAD_TOKEN = 128;

		// Token: 0x04000264 RID: 612
		public const int THREAD_IMPERSONATE = 256;

		// Token: 0x04000265 RID: 613
		public const int THREAD_DIRECT_IMPERSONATION = 512;

		// Token: 0x04000266 RID: 614
		public static readonly IntPtr HKEY_LOCAL_MACHINE = (IntPtr)(-2147483646);

		// Token: 0x04000267 RID: 615
		public const int REG_BINARY = 3;

		// Token: 0x04000268 RID: 616
		public const int REG_MULTI_SZ = 7;

		// Token: 0x04000269 RID: 617
		public const int READ_CONTROL = 131072;

		// Token: 0x0400026A RID: 618
		public const int STANDARD_RIGHTS_READ = 131072;

		// Token: 0x0400026B RID: 619
		public const int KEY_QUERY_VALUE = 1;

		// Token: 0x0400026C RID: 620
		public const int KEY_ENUMERATE_SUB_KEYS = 8;

		// Token: 0x0400026D RID: 621
		public const int KEY_NOTIFY = 16;

		// Token: 0x0400026E RID: 622
		public const int KEY_READ = 131097;

		// Token: 0x0400026F RID: 623
		public const int ERROR_BROKEN_PIPE = 109;

		// Token: 0x04000270 RID: 624
		public const int ERROR_NO_DATA = 232;

		// Token: 0x04000271 RID: 625
		public const int ERROR_HANDLE_EOF = 38;

		// Token: 0x04000272 RID: 626
		public const int ERROR_IO_INCOMPLETE = 996;

		// Token: 0x04000273 RID: 627
		public const int ERROR_IO_PENDING = 997;

		// Token: 0x04000274 RID: 628
		public const int ERROR_FILE_EXISTS = 80;

		// Token: 0x04000275 RID: 629
		public const int ERROR_FILENAME_EXCED_RANGE = 206;

		// Token: 0x04000276 RID: 630
		public const int ERROR_MORE_DATA = 234;

		// Token: 0x04000277 RID: 631
		public const int ERROR_CANCELLED = 1223;

		// Token: 0x04000278 RID: 632
		public const int ERROR_FILE_NOT_FOUND = 2;

		// Token: 0x04000279 RID: 633
		public const int ERROR_PATH_NOT_FOUND = 3;

		// Token: 0x0400027A RID: 634
		public const int ERROR_ACCESS_DENIED = 5;

		// Token: 0x0400027B RID: 635
		public const int ERROR_INVALID_HANDLE = 6;

		// Token: 0x0400027C RID: 636
		public const int ERROR_NOT_ENOUGH_MEMORY = 8;

		// Token: 0x0400027D RID: 637
		public const int ERROR_BAD_COMMAND = 22;

		// Token: 0x0400027E RID: 638
		public const int ERROR_SHARING_VIOLATION = 32;

		// Token: 0x0400027F RID: 639
		public const int ERROR_OPERATION_ABORTED = 995;

		// Token: 0x04000280 RID: 640
		public const int ERROR_NO_ASSOCIATION = 1155;

		// Token: 0x04000281 RID: 641
		public const int ERROR_DLL_NOT_FOUND = 1157;

		// Token: 0x04000282 RID: 642
		public const int ERROR_DDE_FAIL = 1156;

		// Token: 0x04000283 RID: 643
		public const int ERROR_INVALID_PARAMETER = 87;

		// Token: 0x04000284 RID: 644
		public const int ERROR_PARTIAL_COPY = 299;

		// Token: 0x04000285 RID: 645
		public const int ERROR_SUCCESS = 0;

		// Token: 0x04000286 RID: 646
		public const int ERROR_ALREADY_EXISTS = 183;

		// Token: 0x04000287 RID: 647
		public const int ERROR_COUNTER_TIMEOUT = 1121;

		// Token: 0x04000288 RID: 648
		public const int DUPLICATE_CLOSE_SOURCE = 1;

		// Token: 0x04000289 RID: 649
		public const int DUPLICATE_SAME_ACCESS = 2;

		// Token: 0x0400028A RID: 650
		public const int RPC_S_SERVER_UNAVAILABLE = 1722;

		// Token: 0x0400028B RID: 651
		public const int RPC_S_CALL_FAILED = 1726;

		// Token: 0x0400028C RID: 652
		public const int PDH_NO_DATA = -2147481643;

		// Token: 0x0400028D RID: 653
		public const int PDH_CALC_NEGATIVE_DENOMINATOR = -2147481642;

		// Token: 0x0400028E RID: 654
		public const int PDH_CALC_NEGATIVE_VALUE = -2147481640;

		// Token: 0x0400028F RID: 655
		public const int SE_ERR_FNF = 2;

		// Token: 0x04000290 RID: 656
		public const int SE_ERR_PNF = 3;

		// Token: 0x04000291 RID: 657
		public const int SE_ERR_ACCESSDENIED = 5;

		// Token: 0x04000292 RID: 658
		public const int SE_ERR_OOM = 8;

		// Token: 0x04000293 RID: 659
		public const int SE_ERR_DLLNOTFOUND = 32;

		// Token: 0x04000294 RID: 660
		public const int SE_ERR_SHARE = 26;

		// Token: 0x04000295 RID: 661
		public const int SE_ERR_ASSOCINCOMPLETE = 27;

		// Token: 0x04000296 RID: 662
		public const int SE_ERR_DDETIMEOUT = 28;

		// Token: 0x04000297 RID: 663
		public const int SE_ERR_DDEFAIL = 29;

		// Token: 0x04000298 RID: 664
		public const int SE_ERR_DDEBUSY = 30;

		// Token: 0x04000299 RID: 665
		public const int SE_ERR_NOASSOC = 31;

		// Token: 0x0400029A RID: 666
		public const int SE_PRIVILEGE_ENABLED = 2;

		// Token: 0x0400029B RID: 667
		public const int LOGON32_LOGON_BATCH = 4;

		// Token: 0x0400029C RID: 668
		public const int LOGON32_PROVIDER_DEFAULT = 0;

		// Token: 0x0400029D RID: 669
		public const int LOGON32_LOGON_INTERACTIVE = 2;

		// Token: 0x0400029E RID: 670
		public const int TOKEN_ADJUST_PRIVILEGES = 32;

		// Token: 0x0400029F RID: 671
		public const int TOKEN_QUERY = 8;

		// Token: 0x040002A0 RID: 672
		public const int CREATE_NO_WINDOW = 134217728;

		// Token: 0x040002A1 RID: 673
		public const int CREATE_SUSPENDED = 4;

		// Token: 0x040002A2 RID: 674
		public const int CREATE_UNICODE_ENVIRONMENT = 1024;

		// Token: 0x040002A3 RID: 675
		public const int SMTO_ABORTIFHUNG = 2;

		// Token: 0x040002A4 RID: 676
		public const int GWL_STYLE = -16;

		// Token: 0x040002A5 RID: 677
		public const int GCL_WNDPROC = -24;

		// Token: 0x040002A6 RID: 678
		public const int GWL_WNDPROC = -4;

		// Token: 0x040002A7 RID: 679
		public const int WS_DISABLED = 134217728;

		// Token: 0x040002A8 RID: 680
		public const int WM_NULL = 0;

		// Token: 0x040002A9 RID: 681
		public const int WM_CLOSE = 16;

		// Token: 0x040002AA RID: 682
		public const int SW_SHOWNORMAL = 1;

		// Token: 0x040002AB RID: 683
		public const int SW_NORMAL = 1;

		// Token: 0x040002AC RID: 684
		public const int SW_SHOWMINIMIZED = 2;

		// Token: 0x040002AD RID: 685
		public const int SW_SHOWMAXIMIZED = 3;

		// Token: 0x040002AE RID: 686
		public const int SW_MAXIMIZE = 3;

		// Token: 0x040002AF RID: 687
		public const int SW_SHOWNOACTIVATE = 4;

		// Token: 0x040002B0 RID: 688
		public const int SW_SHOW = 5;

		// Token: 0x040002B1 RID: 689
		public const int SW_MINIMIZE = 6;

		// Token: 0x040002B2 RID: 690
		public const int SW_SHOWMINNOACTIVE = 7;

		// Token: 0x040002B3 RID: 691
		public const int SW_SHOWNA = 8;

		// Token: 0x040002B4 RID: 692
		public const int SW_RESTORE = 9;

		// Token: 0x040002B5 RID: 693
		public const int SW_SHOWDEFAULT = 10;

		// Token: 0x040002B6 RID: 694
		public const int SW_MAX = 10;

		// Token: 0x040002B7 RID: 695
		public const int GW_OWNER = 4;

		// Token: 0x040002B8 RID: 696
		public const int WHITENESS = 16711778;

		// Token: 0x040002B9 RID: 697
		public const int VS_FILE_INFO = 16;

		// Token: 0x040002BA RID: 698
		public const int VS_VERSION_INFO = 1;

		// Token: 0x040002BB RID: 699
		public const int VS_USER_DEFINED = 100;

		// Token: 0x040002BC RID: 700
		public const int VS_FFI_SIGNATURE = -17890115;

		// Token: 0x040002BD RID: 701
		public const int VS_FFI_STRUCVERSION = 65536;

		// Token: 0x040002BE RID: 702
		public const int VS_FFI_FILEFLAGSMASK = 63;

		// Token: 0x040002BF RID: 703
		public const int VS_FF_DEBUG = 1;

		// Token: 0x040002C0 RID: 704
		public const int VS_FF_PRERELEASE = 2;

		// Token: 0x040002C1 RID: 705
		public const int VS_FF_PATCHED = 4;

		// Token: 0x040002C2 RID: 706
		public const int VS_FF_PRIVATEBUILD = 8;

		// Token: 0x040002C3 RID: 707
		public const int VS_FF_INFOINFERRED = 16;

		// Token: 0x040002C4 RID: 708
		public const int VS_FF_SPECIALBUILD = 32;

		// Token: 0x040002C5 RID: 709
		public const int VFT_UNKNOWN = 0;

		// Token: 0x040002C6 RID: 710
		public const int VFT_APP = 1;

		// Token: 0x040002C7 RID: 711
		public const int VFT_DLL = 2;

		// Token: 0x040002C8 RID: 712
		public const int VFT_DRV = 3;

		// Token: 0x040002C9 RID: 713
		public const int VFT_FONT = 4;

		// Token: 0x040002CA RID: 714
		public const int VFT_VXD = 5;

		// Token: 0x040002CB RID: 715
		public const int VFT_STATIC_LIB = 7;

		// Token: 0x040002CC RID: 716
		public const int VFT2_UNKNOWN = 0;

		// Token: 0x040002CD RID: 717
		public const int VFT2_DRV_PRINTER = 1;

		// Token: 0x040002CE RID: 718
		public const int VFT2_DRV_KEYBOARD = 2;

		// Token: 0x040002CF RID: 719
		public const int VFT2_DRV_LANGUAGE = 3;

		// Token: 0x040002D0 RID: 720
		public const int VFT2_DRV_DISPLAY = 4;

		// Token: 0x040002D1 RID: 721
		public const int VFT2_DRV_MOUSE = 5;

		// Token: 0x040002D2 RID: 722
		public const int VFT2_DRV_NETWORK = 6;

		// Token: 0x040002D3 RID: 723
		public const int VFT2_DRV_SYSTEM = 7;

		// Token: 0x040002D4 RID: 724
		public const int VFT2_DRV_INSTALLABLE = 8;

		// Token: 0x040002D5 RID: 725
		public const int VFT2_DRV_SOUND = 9;

		// Token: 0x040002D6 RID: 726
		public const int VFT2_DRV_COMM = 10;

		// Token: 0x040002D7 RID: 727
		public const int VFT2_DRV_INPUTMETHOD = 11;

		// Token: 0x040002D8 RID: 728
		public const int VFT2_FONT_RASTER = 1;

		// Token: 0x040002D9 RID: 729
		public const int VFT2_FONT_VECTOR = 2;

		// Token: 0x040002DA RID: 730
		public const int VFT2_FONT_TRUETYPE = 3;

		// Token: 0x040002DB RID: 731
		public const int GMEM_FIXED = 0;

		// Token: 0x040002DC RID: 732
		public const int GMEM_MOVEABLE = 2;

		// Token: 0x040002DD RID: 733
		public const int GMEM_NOCOMPACT = 16;

		// Token: 0x040002DE RID: 734
		public const int GMEM_NODISCARD = 32;

		// Token: 0x040002DF RID: 735
		public const int GMEM_ZEROINIT = 64;

		// Token: 0x040002E0 RID: 736
		public const int GMEM_MODIFY = 128;

		// Token: 0x040002E1 RID: 737
		public const int GMEM_DISCARDABLE = 256;

		// Token: 0x040002E2 RID: 738
		public const int GMEM_NOT_BANKED = 4096;

		// Token: 0x040002E3 RID: 739
		public const int GMEM_SHARE = 8192;

		// Token: 0x040002E4 RID: 740
		public const int GMEM_DDESHARE = 8192;

		// Token: 0x040002E5 RID: 741
		public const int GMEM_NOTIFY = 16384;

		// Token: 0x040002E6 RID: 742
		public const int GMEM_LOWER = 4096;

		// Token: 0x040002E7 RID: 743
		public const int GMEM_VALID_FLAGS = 32626;

		// Token: 0x040002E8 RID: 744
		public const int GMEM_INVALID_HANDLE = 32768;

		// Token: 0x040002E9 RID: 745
		public const int GHND = 66;

		// Token: 0x040002EA RID: 746
		public const int GPTR = 64;

		// Token: 0x040002EB RID: 747
		public const int GMEM_DISCARDED = 16384;

		// Token: 0x040002EC RID: 748
		public const int GMEM_LOCKCOUNT = 255;

		// Token: 0x040002ED RID: 749
		public const int UOI_NAME = 2;

		// Token: 0x040002EE RID: 750
		public const int UOI_TYPE = 3;

		// Token: 0x040002EF RID: 751
		public const int UOI_USER_SID = 4;

		// Token: 0x040002F0 RID: 752
		public const int VER_PLATFORM_WIN32_NT = 2;

		// Token: 0x020006AF RID: 1711
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		internal class TEXTMETRIC
		{
			// Token: 0x04002EC3 RID: 11971
			public int tmHeight;

			// Token: 0x04002EC4 RID: 11972
			public int tmAscent;

			// Token: 0x04002EC5 RID: 11973
			public int tmDescent;

			// Token: 0x04002EC6 RID: 11974
			public int tmInternalLeading;

			// Token: 0x04002EC7 RID: 11975
			public int tmExternalLeading;

			// Token: 0x04002EC8 RID: 11976
			public int tmAveCharWidth;

			// Token: 0x04002EC9 RID: 11977
			public int tmMaxCharWidth;

			// Token: 0x04002ECA RID: 11978
			public int tmWeight;

			// Token: 0x04002ECB RID: 11979
			public int tmOverhang;

			// Token: 0x04002ECC RID: 11980
			public int tmDigitizedAspectX;

			// Token: 0x04002ECD RID: 11981
			public int tmDigitizedAspectY;

			// Token: 0x04002ECE RID: 11982
			public char tmFirstChar;

			// Token: 0x04002ECF RID: 11983
			public char tmLastChar;

			// Token: 0x04002ED0 RID: 11984
			public char tmDefaultChar;

			// Token: 0x04002ED1 RID: 11985
			public char tmBreakChar;

			// Token: 0x04002ED2 RID: 11986
			public byte tmItalic;

			// Token: 0x04002ED3 RID: 11987
			public byte tmUnderlined;

			// Token: 0x04002ED4 RID: 11988
			public byte tmStruckOut;

			// Token: 0x04002ED5 RID: 11989
			public byte tmPitchAndFamily;

			// Token: 0x04002ED6 RID: 11990
			public byte tmCharSet;
		}

		// Token: 0x020006B0 RID: 1712
		[StructLayout(LayoutKind.Sequential)]
		internal class STARTUPINFO
		{
			// Token: 0x06003FCF RID: 16335 RVA: 0x0010C18C File Offset: 0x0010A38C
			public STARTUPINFO()
			{
				this.cb = Marshal.SizeOf(this);
			}

			// Token: 0x06003FD0 RID: 16336 RVA: 0x0010C20C File Offset: 0x0010A40C
			public void Dispose()
			{
				if (this.hStdInput != null && !this.hStdInput.IsInvalid)
				{
					this.hStdInput.Close();
					this.hStdInput = null;
				}
				if (this.hStdOutput != null && !this.hStdOutput.IsInvalid)
				{
					this.hStdOutput.Close();
					this.hStdOutput = null;
				}
				if (this.hStdError != null && !this.hStdError.IsInvalid)
				{
					this.hStdError.Close();
					this.hStdError = null;
				}
			}

			// Token: 0x04002ED7 RID: 11991
			public int cb;

			// Token: 0x04002ED8 RID: 11992
			public IntPtr lpReserved = IntPtr.Zero;

			// Token: 0x04002ED9 RID: 11993
			public IntPtr lpDesktop = IntPtr.Zero;

			// Token: 0x04002EDA RID: 11994
			public IntPtr lpTitle = IntPtr.Zero;

			// Token: 0x04002EDB RID: 11995
			public int dwX;

			// Token: 0x04002EDC RID: 11996
			public int dwY;

			// Token: 0x04002EDD RID: 11997
			public int dwXSize;

			// Token: 0x04002EDE RID: 11998
			public int dwYSize;

			// Token: 0x04002EDF RID: 11999
			public int dwXCountChars;

			// Token: 0x04002EE0 RID: 12000
			public int dwYCountChars;

			// Token: 0x04002EE1 RID: 12001
			public int dwFillAttribute;

			// Token: 0x04002EE2 RID: 12002
			public int dwFlags;

			// Token: 0x04002EE3 RID: 12003
			public short wShowWindow;

			// Token: 0x04002EE4 RID: 12004
			public short cbReserved2;

			// Token: 0x04002EE5 RID: 12005
			public IntPtr lpReserved2 = IntPtr.Zero;

			// Token: 0x04002EE6 RID: 12006
			public SafeFileHandle hStdInput = new SafeFileHandle(IntPtr.Zero, false);

			// Token: 0x04002EE7 RID: 12007
			public SafeFileHandle hStdOutput = new SafeFileHandle(IntPtr.Zero, false);

			// Token: 0x04002EE8 RID: 12008
			public SafeFileHandle hStdError = new SafeFileHandle(IntPtr.Zero, false);
		}

		// Token: 0x020006B1 RID: 1713
		[StructLayout(LayoutKind.Sequential)]
		internal class SECURITY_ATTRIBUTES
		{
			// Token: 0x04002EE9 RID: 12009
			public int nLength = 12;

			// Token: 0x04002EEA RID: 12010
			public SafeLocalMemHandle lpSecurityDescriptor = new SafeLocalMemHandle(IntPtr.Zero, false);

			// Token: 0x04002EEB RID: 12011
			public bool bInheritHandle;
		}

		// Token: 0x020006B2 RID: 1714
		[Flags]
		internal enum LogonFlags
		{
			// Token: 0x04002EED RID: 12013
			LOGON_WITH_PROFILE = 1,
			// Token: 0x04002EEE RID: 12014
			LOGON_NETCREDENTIALS_ONLY = 2
		}

		// Token: 0x020006B3 RID: 1715
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		internal class WNDCLASS_I
		{
			// Token: 0x04002EEF RID: 12015
			public int style;

			// Token: 0x04002EF0 RID: 12016
			public IntPtr lpfnWndProc;

			// Token: 0x04002EF1 RID: 12017
			public int cbClsExtra;

			// Token: 0x04002EF2 RID: 12018
			public int cbWndExtra;

			// Token: 0x04002EF3 RID: 12019
			public IntPtr hInstance = IntPtr.Zero;

			// Token: 0x04002EF4 RID: 12020
			public IntPtr hIcon = IntPtr.Zero;

			// Token: 0x04002EF5 RID: 12021
			public IntPtr hCursor = IntPtr.Zero;

			// Token: 0x04002EF6 RID: 12022
			public IntPtr hbrBackground = IntPtr.Zero;

			// Token: 0x04002EF7 RID: 12023
			public IntPtr lpszMenuName = IntPtr.Zero;

			// Token: 0x04002EF8 RID: 12024
			public IntPtr lpszClassName = IntPtr.Zero;
		}

		// Token: 0x020006B4 RID: 1716
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		internal class WNDCLASS
		{
			// Token: 0x04002EF9 RID: 12025
			public int style;

			// Token: 0x04002EFA RID: 12026
			public NativeMethods.WndProc lpfnWndProc;

			// Token: 0x04002EFB RID: 12027
			public int cbClsExtra;

			// Token: 0x04002EFC RID: 12028
			public int cbWndExtra;

			// Token: 0x04002EFD RID: 12029
			public IntPtr hInstance = IntPtr.Zero;

			// Token: 0x04002EFE RID: 12030
			public IntPtr hIcon = IntPtr.Zero;

			// Token: 0x04002EFF RID: 12031
			public IntPtr hCursor = IntPtr.Zero;

			// Token: 0x04002F00 RID: 12032
			public IntPtr hbrBackground = IntPtr.Zero;

			// Token: 0x04002F01 RID: 12033
			public string lpszMenuName;

			// Token: 0x04002F02 RID: 12034
			public string lpszClassName;
		}

		// Token: 0x020006B5 RID: 1717
		public struct MSG
		{
			// Token: 0x04002F03 RID: 12035
			public IntPtr hwnd;

			// Token: 0x04002F04 RID: 12036
			public int message;

			// Token: 0x04002F05 RID: 12037
			public IntPtr wParam;

			// Token: 0x04002F06 RID: 12038
			public IntPtr lParam;

			// Token: 0x04002F07 RID: 12039
			public int time;

			// Token: 0x04002F08 RID: 12040
			public int pt_x;

			// Token: 0x04002F09 RID: 12041
			public int pt_y;
		}

		// Token: 0x020006B6 RID: 1718
		public enum StructFormatEnum
		{
			// Token: 0x04002F0B RID: 12043
			Ansi = 1,
			// Token: 0x04002F0C RID: 12044
			Unicode,
			// Token: 0x04002F0D RID: 12045
			Auto
		}

		// Token: 0x020006B7 RID: 1719
		public enum StructFormat
		{
			// Token: 0x04002F0F RID: 12047
			Ansi = 1,
			// Token: 0x04002F10 RID: 12048
			Unicode,
			// Token: 0x04002F11 RID: 12049
			Auto
		}

		// Token: 0x020006B8 RID: 1720
		public struct RTL_OSVERSIONINFOEX
		{
			// Token: 0x04002F12 RID: 12050
			internal uint dwOSVersionInfoSize;

			// Token: 0x04002F13 RID: 12051
			internal uint dwMajorVersion;

			// Token: 0x04002F14 RID: 12052
			internal uint dwMinorVersion;

			// Token: 0x04002F15 RID: 12053
			internal uint dwBuildNumber;

			// Token: 0x04002F16 RID: 12054
			internal uint dwPlatformId;

			// Token: 0x04002F17 RID: 12055
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			internal string szCSDVersion;
		}

		// Token: 0x020006B9 RID: 1721
		// (Invoke) Token: 0x06003FD5 RID: 16341
		public delegate IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x020006BA RID: 1722
		// (Invoke) Token: 0x06003FD9 RID: 16345
		public delegate int ConHndlr(int signalType);

		// Token: 0x020006BB RID: 1723
		[StructLayout(LayoutKind.Sequential)]
		public class PDH_RAW_COUNTER
		{
			// Token: 0x04002F18 RID: 12056
			public int CStatus;

			// Token: 0x04002F19 RID: 12057
			public long TimeStamp;

			// Token: 0x04002F1A RID: 12058
			public long FirstValue;

			// Token: 0x04002F1B RID: 12059
			public long SecondValue;

			// Token: 0x04002F1C RID: 12060
			public int MultiCount;
		}

		// Token: 0x020006BC RID: 1724
		[StructLayout(LayoutKind.Sequential)]
		public class PDH_FMT_COUNTERVALUE
		{
			// Token: 0x04002F1D RID: 12061
			public int CStatus;

			// Token: 0x04002F1E RID: 12062
			public double data;
		}

		// Token: 0x020006BD RID: 1725
		[StructLayout(LayoutKind.Sequential)]
		internal class PERF_COUNTER_BLOCK
		{
			// Token: 0x04002F1F RID: 12063
			public int ByteLength;
		}

		// Token: 0x020006BE RID: 1726
		[StructLayout(LayoutKind.Sequential)]
		internal class PERF_COUNTER_DEFINITION
		{
			// Token: 0x04002F20 RID: 12064
			public int ByteLength;

			// Token: 0x04002F21 RID: 12065
			public int CounterNameTitleIndex;

			// Token: 0x04002F22 RID: 12066
			public int CounterNameTitlePtr;

			// Token: 0x04002F23 RID: 12067
			public int CounterHelpTitleIndex;

			// Token: 0x04002F24 RID: 12068
			public int CounterHelpTitlePtr;

			// Token: 0x04002F25 RID: 12069
			public int DefaultScale;

			// Token: 0x04002F26 RID: 12070
			public int DetailLevel;

			// Token: 0x04002F27 RID: 12071
			public int CounterType;

			// Token: 0x04002F28 RID: 12072
			public int CounterSize;

			// Token: 0x04002F29 RID: 12073
			public int CounterOffset;
		}

		// Token: 0x020006BF RID: 1727
		[StructLayout(LayoutKind.Sequential)]
		internal class PERF_DATA_BLOCK
		{
			// Token: 0x04002F2A RID: 12074
			public int Signature1;

			// Token: 0x04002F2B RID: 12075
			public int Signature2;

			// Token: 0x04002F2C RID: 12076
			public int LittleEndian;

			// Token: 0x04002F2D RID: 12077
			public int Version;

			// Token: 0x04002F2E RID: 12078
			public int Revision;

			// Token: 0x04002F2F RID: 12079
			public int TotalByteLength;

			// Token: 0x04002F30 RID: 12080
			public int HeaderLength;

			// Token: 0x04002F31 RID: 12081
			public int NumObjectTypes;

			// Token: 0x04002F32 RID: 12082
			public int DefaultObject;

			// Token: 0x04002F33 RID: 12083
			public NativeMethods.SYSTEMTIME SystemTime;

			// Token: 0x04002F34 RID: 12084
			public int pad1;

			// Token: 0x04002F35 RID: 12085
			public long PerfTime;

			// Token: 0x04002F36 RID: 12086
			public long PerfFreq;

			// Token: 0x04002F37 RID: 12087
			public long PerfTime100nSec;

			// Token: 0x04002F38 RID: 12088
			public int SystemNameLength;

			// Token: 0x04002F39 RID: 12089
			public int SystemNameOffset;
		}

		// Token: 0x020006C0 RID: 1728
		[StructLayout(LayoutKind.Sequential)]
		internal class PERF_INSTANCE_DEFINITION
		{
			// Token: 0x04002F3A RID: 12090
			public int ByteLength;

			// Token: 0x04002F3B RID: 12091
			public int ParentObjectTitleIndex;

			// Token: 0x04002F3C RID: 12092
			public int ParentObjectInstance;

			// Token: 0x04002F3D RID: 12093
			public int UniqueID;

			// Token: 0x04002F3E RID: 12094
			public int NameOffset;

			// Token: 0x04002F3F RID: 12095
			public int NameLength;
		}

		// Token: 0x020006C1 RID: 1729
		[StructLayout(LayoutKind.Sequential)]
		internal class PERF_OBJECT_TYPE
		{
			// Token: 0x04002F40 RID: 12096
			public int TotalByteLength;

			// Token: 0x04002F41 RID: 12097
			public int DefinitionLength;

			// Token: 0x04002F42 RID: 12098
			public int HeaderLength;

			// Token: 0x04002F43 RID: 12099
			public int ObjectNameTitleIndex;

			// Token: 0x04002F44 RID: 12100
			public int ObjectNameTitlePtr;

			// Token: 0x04002F45 RID: 12101
			public int ObjectHelpTitleIndex;

			// Token: 0x04002F46 RID: 12102
			public int ObjectHelpTitlePtr;

			// Token: 0x04002F47 RID: 12103
			public int DetailLevel;

			// Token: 0x04002F48 RID: 12104
			public int NumCounters;

			// Token: 0x04002F49 RID: 12105
			public int DefaultCounter;

			// Token: 0x04002F4A RID: 12106
			public int NumInstances;

			// Token: 0x04002F4B RID: 12107
			public int CodePage;

			// Token: 0x04002F4C RID: 12108
			public long PerfTime;

			// Token: 0x04002F4D RID: 12109
			public long PerfFreq;
		}

		// Token: 0x020006C2 RID: 1730
		[StructLayout(LayoutKind.Sequential)]
		internal class NtModuleInfo
		{
			// Token: 0x04002F4E RID: 12110
			public IntPtr BaseOfDll = (IntPtr)0;

			// Token: 0x04002F4F RID: 12111
			public int SizeOfImage;

			// Token: 0x04002F50 RID: 12112
			public IntPtr EntryPoint = (IntPtr)0;
		}

		// Token: 0x020006C3 RID: 1731
		[StructLayout(LayoutKind.Sequential)]
		internal class WinProcessEntry
		{
			// Token: 0x04002F51 RID: 12113
			public int dwSize;

			// Token: 0x04002F52 RID: 12114
			public int cntUsage;

			// Token: 0x04002F53 RID: 12115
			public int th32ProcessID;

			// Token: 0x04002F54 RID: 12116
			public IntPtr th32DefaultHeapID = (IntPtr)0;

			// Token: 0x04002F55 RID: 12117
			public int th32ModuleID;

			// Token: 0x04002F56 RID: 12118
			public int cntThreads;

			// Token: 0x04002F57 RID: 12119
			public int th32ParentProcessID;

			// Token: 0x04002F58 RID: 12120
			public int pcPriClassBase;

			// Token: 0x04002F59 RID: 12121
			public int dwFlags;

			// Token: 0x04002F5A RID: 12122
			public const int sizeofFileName = 260;
		}

		// Token: 0x020006C4 RID: 1732
		[StructLayout(LayoutKind.Sequential)]
		internal class WinThreadEntry
		{
			// Token: 0x04002F5B RID: 12123
			public int dwSize;

			// Token: 0x04002F5C RID: 12124
			public int cntUsage;

			// Token: 0x04002F5D RID: 12125
			public int th32ThreadID;

			// Token: 0x04002F5E RID: 12126
			public int th32OwnerProcessID;

			// Token: 0x04002F5F RID: 12127
			public int tpBasePri;

			// Token: 0x04002F60 RID: 12128
			public int tpDeltaPri;

			// Token: 0x04002F61 RID: 12129
			public int dwFlags;
		}

		// Token: 0x020006C5 RID: 1733
		[StructLayout(LayoutKind.Sequential)]
		internal class WinModuleEntry
		{
			// Token: 0x04002F62 RID: 12130
			public int dwSize;

			// Token: 0x04002F63 RID: 12131
			public int th32ModuleID;

			// Token: 0x04002F64 RID: 12132
			public int th32ProcessID;

			// Token: 0x04002F65 RID: 12133
			public int GlblcntUsage;

			// Token: 0x04002F66 RID: 12134
			public int ProccntUsage;

			// Token: 0x04002F67 RID: 12135
			public IntPtr modBaseAddr = (IntPtr)0;

			// Token: 0x04002F68 RID: 12136
			public int modBaseSize;

			// Token: 0x04002F69 RID: 12137
			public IntPtr hModule = (IntPtr)0;

			// Token: 0x04002F6A RID: 12138
			public const int sizeofModuleName = 256;

			// Token: 0x04002F6B RID: 12139
			public const int sizeofFileName = 260;
		}

		// Token: 0x020006C6 RID: 1734
		[StructLayout(LayoutKind.Sequential)]
		internal class ShellExecuteInfo
		{
			// Token: 0x06003FE7 RID: 16359 RVA: 0x0010C3D0 File Offset: 0x0010A5D0
			public ShellExecuteInfo()
			{
				this.cbSize = Marshal.SizeOf(this);
			}

			// Token: 0x04002F6C RID: 12140
			public int cbSize;

			// Token: 0x04002F6D RID: 12141
			public int fMask;

			// Token: 0x04002F6E RID: 12142
			public IntPtr hwnd = (IntPtr)0;

			// Token: 0x04002F6F RID: 12143
			public IntPtr lpVerb = (IntPtr)0;

			// Token: 0x04002F70 RID: 12144
			public IntPtr lpFile = (IntPtr)0;

			// Token: 0x04002F71 RID: 12145
			public IntPtr lpParameters = (IntPtr)0;

			// Token: 0x04002F72 RID: 12146
			public IntPtr lpDirectory = (IntPtr)0;

			// Token: 0x04002F73 RID: 12147
			public int nShow;

			// Token: 0x04002F74 RID: 12148
			public IntPtr hInstApp = (IntPtr)0;

			// Token: 0x04002F75 RID: 12149
			public IntPtr lpIDList = (IntPtr)0;

			// Token: 0x04002F76 RID: 12150
			public IntPtr lpClass = (IntPtr)0;

			// Token: 0x04002F77 RID: 12151
			public IntPtr hkeyClass = (IntPtr)0;

			// Token: 0x04002F78 RID: 12152
			public int dwHotKey;

			// Token: 0x04002F79 RID: 12153
			public IntPtr hIcon = (IntPtr)0;

			// Token: 0x04002F7A RID: 12154
			public IntPtr hProcess = (IntPtr)0;
		}

		// Token: 0x020006C7 RID: 1735
		[StructLayout(LayoutKind.Sequential)]
		internal class NtProcessBasicInfo
		{
			// Token: 0x04002F7B RID: 12155
			public int ExitStatus;

			// Token: 0x04002F7C RID: 12156
			public IntPtr PebBaseAddress = (IntPtr)0;

			// Token: 0x04002F7D RID: 12157
			public IntPtr AffinityMask = (IntPtr)0;

			// Token: 0x04002F7E RID: 12158
			public int BasePriority;

			// Token: 0x04002F7F RID: 12159
			public IntPtr UniqueProcessId = (IntPtr)0;

			// Token: 0x04002F80 RID: 12160
			public IntPtr InheritedFromUniqueProcessId = (IntPtr)0;
		}

		// Token: 0x020006C8 RID: 1736
		internal struct LUID
		{
			// Token: 0x04002F81 RID: 12161
			public int LowPart;

			// Token: 0x04002F82 RID: 12162
			public int HighPart;
		}

		// Token: 0x020006C9 RID: 1737
		[StructLayout(LayoutKind.Sequential)]
		internal class TokenPrivileges
		{
			// Token: 0x04002F83 RID: 12163
			public int PrivilegeCount = 1;

			// Token: 0x04002F84 RID: 12164
			public NativeMethods.LUID Luid;

			// Token: 0x04002F85 RID: 12165
			public int Attributes;
		}

		// Token: 0x020006CA RID: 1738
		// (Invoke) Token: 0x06003FEB RID: 16363
		internal delegate bool EnumThreadWindowsCallback(IntPtr hWnd, IntPtr lParam);

		// Token: 0x020006CB RID: 1739
		[StructLayout(LayoutKind.Sequential)]
		internal class SYSTEMTIME
		{
			// Token: 0x06003FEE RID: 16366 RVA: 0x0010C4BC File Offset: 0x0010A6BC
			public override string ToString()
			{
				return string.Concat(new string[]
				{
					"[SYSTEMTIME: ",
					this.wDay.ToString(CultureInfo.CurrentCulture),
					"/",
					this.wMonth.ToString(CultureInfo.CurrentCulture),
					"/",
					this.wYear.ToString(CultureInfo.CurrentCulture),
					" ",
					this.wHour.ToString(CultureInfo.CurrentCulture),
					":",
					this.wMinute.ToString(CultureInfo.CurrentCulture),
					":",
					this.wSecond.ToString(CultureInfo.CurrentCulture),
					"]"
				});
			}

			// Token: 0x04002F86 RID: 12166
			public short wYear;

			// Token: 0x04002F87 RID: 12167
			public short wMonth;

			// Token: 0x04002F88 RID: 12168
			public short wDayOfWeek;

			// Token: 0x04002F89 RID: 12169
			public short wDay;

			// Token: 0x04002F8A RID: 12170
			public short wHour;

			// Token: 0x04002F8B RID: 12171
			public short wMinute;

			// Token: 0x04002F8C RID: 12172
			public short wSecond;

			// Token: 0x04002F8D RID: 12173
			public short wMilliseconds;
		}

		// Token: 0x020006CC RID: 1740
		[StructLayout(LayoutKind.Sequential)]
		internal class VS_FIXEDFILEINFO
		{
			// Token: 0x04002F8E RID: 12174
			public int dwSignature;

			// Token: 0x04002F8F RID: 12175
			public int dwStructVersion;

			// Token: 0x04002F90 RID: 12176
			public int dwFileVersionMS;

			// Token: 0x04002F91 RID: 12177
			public int dwFileVersionLS;

			// Token: 0x04002F92 RID: 12178
			public int dwProductVersionMS;

			// Token: 0x04002F93 RID: 12179
			public int dwProductVersionLS;

			// Token: 0x04002F94 RID: 12180
			public int dwFileFlagsMask;

			// Token: 0x04002F95 RID: 12181
			public int dwFileFlags;

			// Token: 0x04002F96 RID: 12182
			public int dwFileOS;

			// Token: 0x04002F97 RID: 12183
			public int dwFileType;

			// Token: 0x04002F98 RID: 12184
			public int dwFileSubtype;

			// Token: 0x04002F99 RID: 12185
			public int dwFileDateMS;

			// Token: 0x04002F9A RID: 12186
			public int dwFileDateLS;
		}

		// Token: 0x020006CD RID: 1741
		[StructLayout(LayoutKind.Sequential)]
		internal class USEROBJECTFLAGS
		{
			// Token: 0x04002F9B RID: 12187
			public int fInherit;

			// Token: 0x04002F9C RID: 12188
			public int fReserved;

			// Token: 0x04002F9D RID: 12189
			public int dwFlags;
		}

		// Token: 0x020006CE RID: 1742
		internal static class Util
		{
			// Token: 0x06003FF2 RID: 16370 RVA: 0x0010C59B File Offset: 0x0010A79B
			public static int HIWORD(int n)
			{
				return (n >> 16) & 65535;
			}

			// Token: 0x06003FF3 RID: 16371 RVA: 0x0010C5A7 File Offset: 0x0010A7A7
			public static int LOWORD(int n)
			{
				return n & 65535;
			}
		}

		// Token: 0x020006CF RID: 1743
		internal struct MEMORY_BASIC_INFORMATION
		{
			// Token: 0x04002F9E RID: 12190
			internal IntPtr BaseAddress;

			// Token: 0x04002F9F RID: 12191
			internal IntPtr AllocationBase;

			// Token: 0x04002FA0 RID: 12192
			internal uint AllocationProtect;

			// Token: 0x04002FA1 RID: 12193
			internal UIntPtr RegionSize;

			// Token: 0x04002FA2 RID: 12194
			internal uint State;

			// Token: 0x04002FA3 RID: 12195
			internal uint Protect;

			// Token: 0x04002FA4 RID: 12196
			internal uint Type;
		}
	}
}
