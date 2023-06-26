using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.CodeDom.Compiler
{
	/// <summary>Provides command execution functions for invoking compilers. This class cannot be inherited.</summary>
	// Token: 0x02000679 RID: 1657
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	public static class Executor
	{
		// Token: 0x06003D10 RID: 15632 RVA: 0x000FADF4 File Offset: 0x000F8FF4
		internal static string GetRuntimeInstallDirectory()
		{
			return RuntimeEnvironment.GetRuntimeDirectory();
		}

		// Token: 0x06003D11 RID: 15633 RVA: 0x000FADFB File Offset: 0x000F8FFB
		private static FileStream CreateInheritedFile(string file)
		{
			return new FileStream(file, FileMode.CreateNew, FileAccess.Write, FileShare.Read | FileShare.Inheritable);
		}

		/// <summary>Executes the command using the specified temporary files and waits for the call to return.</summary>
		/// <param name="cmd">The command to execute.</param>
		/// <param name="tempFiles">A <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> with which to manage and store references to intermediate files generated during compilation.</param>
		// Token: 0x06003D12 RID: 15634 RVA: 0x000FAE08 File Offset: 0x000F9008
		public static void ExecWait(string cmd, TempFileCollection tempFiles)
		{
			string text = null;
			string text2 = null;
			Executor.ExecWaitWithCapture(cmd, tempFiles, ref text, ref text2);
		}

		/// <summary>Executes the specified command using the specified temporary files and waits for the call to return, storing output and error information from the compiler in the specified strings.</summary>
		/// <param name="cmd">The command to execute.</param>
		/// <param name="tempFiles">A <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> with which to manage and store references to intermediate files generated during compilation.</param>
		/// <param name="outputName">A reference to a string that will store the compiler's message output.</param>
		/// <param name="errorName">A reference to a string that will store the name of the error or errors encountered.</param>
		/// <returns>The return value from the compiler.</returns>
		// Token: 0x06003D13 RID: 15635 RVA: 0x000FAE25 File Offset: 0x000F9025
		public static int ExecWaitWithCapture(string cmd, TempFileCollection tempFiles, ref string outputName, ref string errorName)
		{
			return Executor.ExecWaitWithCapture(null, cmd, Environment.CurrentDirectory, tempFiles, ref outputName, ref errorName, null);
		}

		/// <summary>Executes the specified command using the specified current directory and temporary files, and waits for the call to return, storing output and error information from the compiler in the specified strings.</summary>
		/// <param name="cmd">The command to execute.</param>
		/// <param name="currentDir">The current directory.</param>
		/// <param name="tempFiles">A <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> with which to manage and store references to intermediate files generated during compilation.</param>
		/// <param name="outputName">A reference to a string that will store the compiler's message output.</param>
		/// <param name="errorName">A reference to a string that will store the name of the error or errors encountered.</param>
		/// <returns>The return value from the compiler.</returns>
		// Token: 0x06003D14 RID: 15636 RVA: 0x000FAE37 File Offset: 0x000F9037
		public static int ExecWaitWithCapture(string cmd, string currentDir, TempFileCollection tempFiles, ref string outputName, ref string errorName)
		{
			return Executor.ExecWaitWithCapture(null, cmd, currentDir, tempFiles, ref outputName, ref errorName, null);
		}

		/// <summary>Executes the specified command using the specified user token and temporary files, and waits for the call to return, storing output and error information from the compiler in the specified strings.</summary>
		/// <param name="userToken">The token to start the compiler process with.</param>
		/// <param name="cmd">The command to execute.</param>
		/// <param name="tempFiles">A <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> with which to manage and store references to intermediate files generated during compilation.</param>
		/// <param name="outputName">A reference to a string that will store the compiler's message output.</param>
		/// <param name="errorName">A reference to a string that will store the name of the error or errors encountered.</param>
		/// <returns>The return value from the compiler.</returns>
		// Token: 0x06003D15 RID: 15637 RVA: 0x000FAE46 File Offset: 0x000F9046
		public static int ExecWaitWithCapture(IntPtr userToken, string cmd, TempFileCollection tempFiles, ref string outputName, ref string errorName)
		{
			return Executor.ExecWaitWithCapture(new SafeUserTokenHandle(userToken, false), cmd, Environment.CurrentDirectory, tempFiles, ref outputName, ref errorName, null);
		}

		/// <summary>Executes the specified command using the specified user token, current directory, and temporary files; then waits for the call to return, storing output and error information from the compiler in the specified strings.</summary>
		/// <param name="userToken">The token to start the compiler process with.</param>
		/// <param name="cmd">The command to execute.</param>
		/// <param name="currentDir">The directory to start the process in.</param>
		/// <param name="tempFiles">A <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> with which to manage and store references to intermediate files generated during compilation.</param>
		/// <param name="outputName">A reference to a string that will store the compiler's message output.</param>
		/// <param name="errorName">A reference to a string that will store the name of the error or errors encountered.</param>
		/// <returns>The return value from the compiler.</returns>
		// Token: 0x06003D16 RID: 15638 RVA: 0x000FAE5F File Offset: 0x000F905F
		public static int ExecWaitWithCapture(IntPtr userToken, string cmd, string currentDir, TempFileCollection tempFiles, ref string outputName, ref string errorName)
		{
			return Executor.ExecWaitWithCapture(new SafeUserTokenHandle(userToken, false), cmd, Environment.CurrentDirectory, tempFiles, ref outputName, ref errorName, null);
		}

		// Token: 0x06003D17 RID: 15639 RVA: 0x000FAE7C File Offset: 0x000F907C
		internal static int ExecWaitWithCapture(SafeUserTokenHandle userToken, string cmd, string currentDir, TempFileCollection tempFiles, ref string outputName, ref string errorName, string trueCmdLine)
		{
			int num = 0;
			try
			{
				WindowsImpersonationContext windowsImpersonationContext = Executor.RevertImpersonation();
				try
				{
					num = Executor.ExecWaitWithCaptureUnimpersonated(userToken, cmd, currentDir, tempFiles, ref outputName, ref errorName, trueCmdLine);
				}
				finally
				{
					Executor.ReImpersonate(windowsImpersonationContext);
				}
			}
			catch
			{
				throw;
			}
			return num;
		}

		// Token: 0x06003D18 RID: 15640 RVA: 0x000FAECC File Offset: 0x000F90CC
		private unsafe static int ExecWaitWithCaptureUnimpersonated(SafeUserTokenHandle userToken, string cmd, string currentDir, TempFileCollection tempFiles, ref string outputName, ref string errorName, string trueCmdLine)
		{
			IntSecurity.UnmanagedCode.Demand();
			if (outputName == null || outputName.Length == 0)
			{
				outputName = tempFiles.AddExtension("out");
			}
			if (errorName == null || errorName.Length == 0)
			{
				errorName = tempFiles.AddExtension("err");
			}
			FileStream fileStream = Executor.CreateInheritedFile(outputName);
			FileStream fileStream2 = Executor.CreateInheritedFile(errorName);
			bool flag = false;
			SafeNativeMethods.PROCESS_INFORMATION process_INFORMATION = new SafeNativeMethods.PROCESS_INFORMATION();
			Microsoft.Win32.SafeHandles.SafeProcessHandle safeProcessHandle = new Microsoft.Win32.SafeHandles.SafeProcessHandle();
			Microsoft.Win32.SafeHandles.SafeThreadHandle safeThreadHandle = new Microsoft.Win32.SafeHandles.SafeThreadHandle();
			SafeUserTokenHandle safeUserTokenHandle = null;
			try
			{
				StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8);
				streamWriter.Write(currentDir);
				streamWriter.Write("> ");
				streamWriter.WriteLine((trueCmdLine != null) ? trueCmdLine : cmd);
				streamWriter.WriteLine();
				streamWriter.WriteLine();
				streamWriter.Flush();
				Microsoft.Win32.NativeMethods.STARTUPINFO startupinfo = new Microsoft.Win32.NativeMethods.STARTUPINFO();
				startupinfo.cb = Marshal.SizeOf(startupinfo);
				startupinfo.dwFlags = 257;
				startupinfo.wShowWindow = 0;
				startupinfo.hStdOutput = fileStream.SafeFileHandle;
				startupinfo.hStdError = fileStream2.SafeFileHandle;
				startupinfo.hStdInput = new SafeFileHandle(Microsoft.Win32.UnsafeNativeMethods.GetStdHandle(-10), false);
				StringDictionary stringDictionary = new StringDictionary();
				foreach (object obj in Environment.GetEnvironmentVariables())
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					stringDictionary[(string)dictionaryEntry.Key] = (string)dictionaryEntry.Value;
				}
				stringDictionary["_ClrRestrictSecAttributes"] = "1";
				byte[] array = EnvironmentBlock.ToByteArray(stringDictionary, false);
				try
				{
					byte[] array2;
					byte* ptr;
					if ((array2 = array) == null || array2.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array2[0];
					}
					IntPtr intPtr = new IntPtr((void*)ptr);
					if (userToken == null || userToken.IsInvalid)
					{
						RuntimeHelpers.PrepareConstrainedRegions();
						try
						{
							goto IL_322;
						}
						finally
						{
							flag = Microsoft.Win32.NativeMethods.CreateProcess(null, new StringBuilder(cmd), null, null, true, 0, intPtr, currentDir, startupinfo, process_INFORMATION);
							if (process_INFORMATION.hProcess != (IntPtr)0 && process_INFORMATION.hProcess != Microsoft.Win32.NativeMethods.INVALID_HANDLE_VALUE)
							{
								safeProcessHandle.InitialSetHandle(process_INFORMATION.hProcess);
							}
							if (process_INFORMATION.hThread != (IntPtr)0 && process_INFORMATION.hThread != Microsoft.Win32.NativeMethods.INVALID_HANDLE_VALUE)
							{
								safeThreadHandle.InitialSetHandle(process_INFORMATION.hThread);
							}
						}
					}
					flag = SafeUserTokenHandle.DuplicateTokenEx(userToken, 983551, null, 2, 1, out safeUserTokenHandle);
					if (flag)
					{
						RuntimeHelpers.PrepareConstrainedRegions();
						try
						{
						}
						finally
						{
							flag = Microsoft.Win32.NativeMethods.CreateProcessAsUser(safeUserTokenHandle, null, cmd, null, null, true, 0, new HandleRef(null, intPtr), currentDir, startupinfo, process_INFORMATION);
							if (process_INFORMATION.hProcess != (IntPtr)0 && process_INFORMATION.hProcess != Microsoft.Win32.NativeMethods.INVALID_HANDLE_VALUE)
							{
								safeProcessHandle.InitialSetHandle(process_INFORMATION.hProcess);
							}
							if (process_INFORMATION.hThread != (IntPtr)0 && process_INFORMATION.hThread != Microsoft.Win32.NativeMethods.INVALID_HANDLE_VALUE)
							{
								safeThreadHandle.InitialSetHandle(process_INFORMATION.hThread);
							}
						}
					}
				}
				finally
				{
					byte[] array2 = null;
				}
			}
			finally
			{
				if (!flag && safeUserTokenHandle != null && !safeUserTokenHandle.IsInvalid)
				{
					safeUserTokenHandle.Close();
					safeUserTokenHandle = null;
				}
				fileStream.Close();
				fileStream2.Close();
			}
			IL_322:
			if (flag)
			{
				try
				{
					ProcessWaitHandle processWaitHandle = null;
					bool flag2;
					try
					{
						processWaitHandle = new ProcessWaitHandle(safeProcessHandle);
						flag2 = processWaitHandle.WaitOne(600000, false);
					}
					finally
					{
						if (processWaitHandle != null)
						{
							processWaitHandle.Close();
						}
					}
					if (!flag2)
					{
						throw new ExternalException(SR.GetString("ExecTimeout", new object[] { cmd }), 258);
					}
					int num = 259;
					if (!Microsoft.Win32.NativeMethods.GetExitCodeProcess(safeProcessHandle, out num))
					{
						throw new ExternalException(SR.GetString("ExecCantGetRetCode", new object[] { cmd }), Marshal.GetLastWin32Error());
					}
					return num;
				}
				finally
				{
					safeProcessHandle.Close();
					safeThreadHandle.Close();
					if (safeUserTokenHandle != null && !safeUserTokenHandle.IsInvalid)
					{
						safeUserTokenHandle.Close();
					}
				}
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (lastWin32Error == 8)
			{
				throw new OutOfMemoryException();
			}
			Win32Exception ex = new Win32Exception(lastWin32Error);
			ExternalException ex2 = new ExternalException(SR.GetString("ExecCantExec", new object[] { cmd }), ex);
			throw ex2;
		}

		// Token: 0x06003D19 RID: 15641 RVA: 0x000FB390 File Offset: 0x000F9590
		[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
		[SecurityPermission(SecurityAction.Assert, ControlPrincipal = true, UnmanagedCode = true)]
		internal static WindowsImpersonationContext RevertImpersonation()
		{
			return WindowsIdentity.Impersonate(new IntPtr(0));
		}

		// Token: 0x06003D1A RID: 15642 RVA: 0x000FB39D File Offset: 0x000F959D
		internal static void ReImpersonate(WindowsImpersonationContext impersonation)
		{
			impersonation.Undo();
		}

		// Token: 0x04002C80 RID: 11392
		private const int ProcessTimeOut = 600000;
	}
}
