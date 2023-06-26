using System;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using Microsoft.Win32;

namespace System
{
	/// <summary>Provides information about, and means to manipulate, the current environment and platform. This class cannot be inherited.</summary>
	// Token: 0x020000DE RID: 222
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public static class Environment
	{
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x0002BCDC File Offset: 0x00029EDC
		private static object InternalSyncObject
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			get
			{
				if (Environment.s_InternalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange<object>(ref Environment.s_InternalSyncObject, obj, null);
				}
				return Environment.s_InternalSyncObject;
			}
		}

		/// <summary>Gets the number of milliseconds elapsed since the system started.</summary>
		/// <returns>A 32-bit signed integer containing the amount of time in milliseconds that has passed since the last time the computer was started.</returns>
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000E31 RID: 3633
		[__DynamicallyInvokable]
		public static extern int TickCount
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000E32 RID: 3634
		internal static extern long TickCount64
		{
			[SecuritySafeCritical]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000E33 RID: 3635
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void _Exit(int exitCode);

		/// <summary>Terminates this process and returns an exit code to the operating system.</summary>
		/// <param name="exitCode">The exit code to return to the operating system. Use 0 (zero) to indicate that the process completed successfully.</param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have sufficient security permission to perform this function.</exception>
		// Token: 0x06000E34 RID: 3636 RVA: 0x0002BD08 File Offset: 0x00029F08
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static void Exit(int exitCode)
		{
			Environment._Exit(exitCode);
		}

		/// <summary>Gets or sets the exit code of the process.</summary>
		/// <returns>A 32-bit signed integer containing the exit code. The default value is 0 (zero), which indicates that the process completed successfully.</returns>
		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000E35 RID: 3637
		// (set) Token: 0x06000E36 RID: 3638
		public static extern int ExitCode
		{
			[SecuritySafeCritical]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[SecuritySafeCritical]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		/// <summary>Immediately terminates a process after writing a message to the Windows Application event log, and then includes the message in error reporting to Microsoft.</summary>
		/// <param name="message">A message that explains why the process was terminated, or <see langword="null" /> if no explanation is provided.</param>
		// Token: 0x06000E37 RID: 3639
		[SecurityCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void FailFast(string message);

		// Token: 0x06000E38 RID: 3640
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void FailFast(string message, uint exitCode);

		/// <summary>Immediately terminates a process after writing a message to the Windows Application event log, and then includes the message and exception information in error reporting to Microsoft.</summary>
		/// <param name="message">A message that explains why the process was terminated, or <see langword="null" /> if no explanation is provided.</param>
		/// <param name="exception">An exception that represents the error that caused the termination. This is typically the exception in a <see langword="catch" /> block.</param>
		// Token: 0x06000E39 RID: 3641
		[SecurityCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void FailFast(string message, Exception exception);

		// Token: 0x06000E3A RID: 3642
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void TriggerCodeContractFailure(ContractFailureKind failureKind, string message, string condition, string exceptionAsString);

		// Token: 0x06000E3B RID: 3643
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GetIsCLRHosted();

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000E3C RID: 3644 RVA: 0x0002BD10 File Offset: 0x00029F10
		internal static bool IsCLRHosted
		{
			[SecuritySafeCritical]
			get
			{
				return Environment.GetIsCLRHosted();
			}
		}

		/// <summary>Gets the command line for this process.</summary>
		/// <returns>A string containing command-line arguments.</returns>
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000E3D RID: 3645 RVA: 0x0002BD18 File Offset: 0x00029F18
		public static string CommandLine
		{
			[SecuritySafeCritical]
			get
			{
				new EnvironmentPermission(EnvironmentPermissionAccess.Read, "Path").Demand();
				string text = null;
				Environment.GetCommandLine(JitHelpers.GetStringHandleOnStack(ref text));
				return text;
			}
		}

		// Token: 0x06000E3E RID: 3646
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetCommandLine(StringHandleOnStack retString);

		/// <summary>Gets or sets the fully qualified path of the current working directory.</summary>
		/// <returns>A string containing a directory path.</returns>
		/// <exception cref="T:System.ArgumentException">Attempted to set to an empty string ("").</exception>
		/// <exception cref="T:System.ArgumentNullException">Attempted to set to <see langword="null." /></exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">Attempted to set a local path that cannot be found.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the appropriate permission.</exception>
		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000E3F RID: 3647 RVA: 0x0002BD44 File Offset: 0x00029F44
		// (set) Token: 0x06000E40 RID: 3648 RVA: 0x0002BD4B File Offset: 0x00029F4B
		public static string CurrentDirectory
		{
			get
			{
				return Directory.GetCurrentDirectory();
			}
			set
			{
				Directory.SetCurrentDirectory(value);
			}
		}

		/// <summary>Gets the fully qualified path of the system directory.</summary>
		/// <returns>A string containing a directory path.</returns>
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000E41 RID: 3649 RVA: 0x0002BD54 File Offset: 0x00029F54
		public static string SystemDirectory
		{
			[SecuritySafeCritical]
			get
			{
				StringBuilder stringBuilder = new StringBuilder(260);
				if (Win32Native.GetSystemDirectory(stringBuilder, 260) == 0)
				{
					__Error.WinIOError();
				}
				string text = stringBuilder.ToString();
				FileIOPermission.QuickDemand(FileIOPermissionAccess.PathDiscovery, text, false, true);
				return text;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000E42 RID: 3650 RVA: 0x0002BD94 File Offset: 0x00029F94
		internal static string InternalWindowsDirectory
		{
			[SecurityCritical]
			get
			{
				StringBuilder stringBuilder = new StringBuilder(260);
				if (Win32Native.GetWindowsDirectory(stringBuilder, 260) == 0)
				{
					__Error.WinIOError();
				}
				return stringBuilder.ToString();
			}
		}

		/// <summary>Replaces the name of each environment variable embedded in the specified string with the string equivalent of the value of the variable, then returns the resulting string.</summary>
		/// <param name="name">A string containing the names of zero or more environment variables. Each environment variable is quoted with the percent sign character (%).</param>
		/// <returns>A string with each environment variable replaced by its value.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x06000E43 RID: 3651 RVA: 0x0002BDC8 File Offset: 0x00029FC8
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static string ExpandEnvironmentVariables(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				return name;
			}
			if (AppDomain.IsAppXModel() && !AppDomain.IsAppXDesignMode())
			{
				return name;
			}
			int num = 100;
			StringBuilder stringBuilder = new StringBuilder(num);
			bool flag = CodeAccessSecurityEngine.QuickCheckForAllDemands();
			string[] array = name.Split(new char[] { '%' });
			StringBuilder stringBuilder2 = (flag ? null : new StringBuilder());
			bool flag2 = false;
			int j;
			for (int i = 1; i < array.Length - 1; i++)
			{
				if (array[i].Length == 0 || flag2)
				{
					flag2 = false;
				}
				else
				{
					stringBuilder.Length = 0;
					string text = "%" + array[i] + "%";
					j = Win32Native.ExpandEnvironmentStrings(text, stringBuilder, num);
					if (j == 0)
					{
						Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
					}
					while (j > num)
					{
						num = j;
						stringBuilder.Capacity = num;
						stringBuilder.Length = 0;
						j = Win32Native.ExpandEnvironmentStrings(text, stringBuilder, num);
						if (j == 0)
						{
							Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
						}
					}
					if (!flag)
					{
						string text2 = stringBuilder.ToString();
						flag2 = text2 != text;
						if (flag2)
						{
							stringBuilder2.Append(array[i]);
							stringBuilder2.Append(';');
						}
					}
				}
			}
			if (!flag)
			{
				new EnvironmentPermission(EnvironmentPermissionAccess.Read, stringBuilder2.ToString()).Demand();
			}
			stringBuilder.Length = 0;
			j = Win32Native.ExpandEnvironmentStrings(name, stringBuilder, num);
			if (j == 0)
			{
				Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
			}
			while (j > num)
			{
				num = j;
				stringBuilder.Capacity = num;
				stringBuilder.Length = 0;
				j = Win32Native.ExpandEnvironmentStrings(name, stringBuilder, num);
				if (j == 0)
				{
					Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
				}
			}
			return stringBuilder.ToString();
		}

		/// <summary>Gets the NetBIOS name of this local computer.</summary>
		/// <returns>A string containing the name of this computer.</returns>
		/// <exception cref="T:System.InvalidOperationException">The name of this computer cannot be obtained.</exception>
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000E44 RID: 3652 RVA: 0x0002BF5C File Offset: 0x0002A15C
		public static string MachineName
		{
			[SecuritySafeCritical]
			get
			{
				new EnvironmentPermission(EnvironmentPermissionAccess.Read, "COMPUTERNAME").Demand();
				StringBuilder stringBuilder = new StringBuilder(256);
				int num = 256;
				if (Win32Native.GetComputerName(stringBuilder, ref num) == 0)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ComputerName"));
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06000E45 RID: 3653
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern int GetProcessorCount();

		/// <summary>Gets the number of processors on the current machine.</summary>
		/// <returns>The 32-bit signed integer that specifies the number of processors on the current machine. There is no default. If the current machine contains multiple processor groups, this property returns the number of logical processors that are available for use by the common language runtime (CLR).</returns>
		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000E46 RID: 3654 RVA: 0x0002BFAA File Offset: 0x0002A1AA
		[__DynamicallyInvokable]
		public static int ProcessorCount
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				return Environment.GetProcessorCount();
			}
		}

		/// <summary>Gets the number of bytes in the operating system's memory page.</summary>
		/// <returns>The number of bytes in the system memory page.</returns>
		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000E47 RID: 3655 RVA: 0x0002BFB4 File Offset: 0x0002A1B4
		public static int SystemPageSize
		{
			[SecuritySafeCritical]
			get
			{
				new EnvironmentPermission(PermissionState.Unrestricted).Demand();
				Win32Native.SYSTEM_INFO system_INFO = default(Win32Native.SYSTEM_INFO);
				Win32Native.GetSystemInfo(ref system_INFO);
				return system_INFO.dwPageSize;
			}
		}

		/// <summary>Returns a string array containing the command-line arguments for the current process.</summary>
		/// <returns>An array of string where each element contains a command-line argument. The first element is the executable file name, and the following zero or more elements contain the remaining command-line arguments.</returns>
		/// <exception cref="T:System.NotSupportedException">The system does not support command-line arguments.</exception>
		// Token: 0x06000E48 RID: 3656 RVA: 0x0002BFE1 File Offset: 0x0002A1E1
		[SecuritySafeCritical]
		public static string[] GetCommandLineArgs()
		{
			new EnvironmentPermission(EnvironmentPermissionAccess.Read, "Path").Demand();
			return Environment.GetCommandLineArgsNative();
		}

		// Token: 0x06000E49 RID: 3657
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetCommandLineArgsNative();

		// Token: 0x06000E4A RID: 3658
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string nativeGetEnvironmentVariable(string variable);

		/// <summary>Retrieves the value of an environment variable from the current process.</summary>
		/// <param name="variable">The name of the environment variable.</param>
		/// <returns>The value of the environment variable specified by <paramref name="variable" />, or <see langword="null" /> if the environment variable is not found.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="variable" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission to perform this operation.</exception>
		// Token: 0x06000E4B RID: 3659 RVA: 0x0002BFF8 File Offset: 0x0002A1F8
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static string GetEnvironmentVariable(string variable)
		{
			if (variable == null)
			{
				throw new ArgumentNullException("variable");
			}
			if (AppDomain.IsAppXModel() && !AppDomain.IsAppXDesignMode())
			{
				return null;
			}
			new EnvironmentPermission(EnvironmentPermissionAccess.Read, variable).Demand();
			StringBuilder stringBuilder = StringBuilderCache.Acquire(128);
			int i = Win32Native.GetEnvironmentVariable(variable, stringBuilder, stringBuilder.Capacity);
			if (i == 0 && Marshal.GetLastWin32Error() == 203)
			{
				StringBuilderCache.Release(stringBuilder);
				return null;
			}
			while (i > stringBuilder.Capacity)
			{
				stringBuilder.Capacity = i;
				stringBuilder.Length = 0;
				i = Win32Native.GetEnvironmentVariable(variable, stringBuilder, stringBuilder.Capacity);
			}
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		/// <summary>Retrieves the value of an environment variable from the current process or from the Windows operating system registry key for the current user or local machine.</summary>
		/// <param name="variable">The name of an environment variable.</param>
		/// <param name="target">One of the <see cref="T:System.EnvironmentVariableTarget" /> values.</param>
		/// <returns>The value of the environment variable specified by the <paramref name="variable" /> and <paramref name="target" /> parameters, or <see langword="null" /> if the environment variable is not found.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="variable" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not a valid <see cref="T:System.EnvironmentVariableTarget" /> value.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission to perform this operation.</exception>
		// Token: 0x06000E4C RID: 3660 RVA: 0x0002C08C File Offset: 0x0002A28C
		[SecuritySafeCritical]
		public static string GetEnvironmentVariable(string variable, EnvironmentVariableTarget target)
		{
			if (variable == null)
			{
				throw new ArgumentNullException("variable");
			}
			if (target == EnvironmentVariableTarget.Process)
			{
				return Environment.GetEnvironmentVariable(variable);
			}
			new EnvironmentPermission(PermissionState.Unrestricted).Demand();
			if (target == EnvironmentVariableTarget.Machine)
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Control\\Session Manager\\Environment", false))
				{
					if (registryKey == null)
					{
						return null;
					}
					return registryKey.GetValue(variable) as string;
				}
			}
			if (target == EnvironmentVariableTarget.User)
			{
				using (RegistryKey registryKey2 = Registry.CurrentUser.OpenSubKey("Environment", false))
				{
					if (registryKey2 == null)
					{
						return null;
					}
					return registryKey2.GetValue(variable) as string;
				}
			}
			throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[] { (int)target }));
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x0002C168 File Offset: 0x0002A368
		[SecurityCritical]
		private unsafe static char[] GetEnvironmentCharArray()
		{
			char[] array = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				char* ptr = null;
				try
				{
					ptr = Win32Native.GetEnvironmentStrings();
					if (ptr == null)
					{
						throw new OutOfMemoryException();
					}
					char* ptr2 = ptr;
					while (*ptr2 != '\0' || ptr2[1] != '\0')
					{
						ptr2++;
					}
					int num = (int)((long)(ptr2 - ptr) + 1L);
					array = new char[num];
					try
					{
						char[] array2;
						char* ptr3;
						if ((array2 = array) == null || array2.Length == 0)
						{
							ptr3 = null;
						}
						else
						{
							ptr3 = &array2[0];
						}
						string.wstrcpy(ptr3, ptr, num);
					}
					finally
					{
						char[] array2 = null;
					}
				}
				finally
				{
					if (ptr != null)
					{
						Win32Native.FreeEnvironmentStrings(ptr);
					}
				}
			}
			return array;
		}

		/// <summary>Retrieves all environment variable names and their values from the current process.</summary>
		/// <returns>A dictionary that contains all environment variable names and their values; otherwise, an empty dictionary if no environment variables are found.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission to perform this operation.</exception>
		/// <exception cref="T:System.OutOfMemoryException">The buffer is out of memory.</exception>
		// Token: 0x06000E4E RID: 3662 RVA: 0x0002C21C File Offset: 0x0002A41C
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static IDictionary GetEnvironmentVariables()
		{
			if (AppDomain.IsAppXModel() && !AppDomain.IsAppXDesignMode())
			{
				return new Hashtable(0);
			}
			bool flag = CodeAccessSecurityEngine.QuickCheckForAllDemands();
			StringBuilder stringBuilder = (flag ? null : new StringBuilder());
			bool flag2 = true;
			char[] environmentCharArray = Environment.GetEnvironmentCharArray();
			Hashtable hashtable = new Hashtable(20);
			for (int i = 0; i < environmentCharArray.Length; i++)
			{
				int num = i;
				while (environmentCharArray[i] != '=' && environmentCharArray[i] != '\0')
				{
					i++;
				}
				if (environmentCharArray[i] != '\0')
				{
					if (i - num == 0)
					{
						while (environmentCharArray[i] != '\0')
						{
							i++;
						}
					}
					else
					{
						string text = new string(environmentCharArray, num, i - num);
						i++;
						int num2 = i;
						while (environmentCharArray[i] != '\0')
						{
							i++;
						}
						string text2 = new string(environmentCharArray, num2, i - num2);
						hashtable[text] = text2;
						if (!flag)
						{
							if (flag2)
							{
								flag2 = false;
							}
							else
							{
								stringBuilder.Append(';');
							}
							stringBuilder.Append(text);
						}
					}
				}
			}
			if (!flag)
			{
				new EnvironmentPermission(EnvironmentPermissionAccess.Read, stringBuilder.ToString()).Demand();
			}
			return hashtable;
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x0002C324 File Offset: 0x0002A524
		internal static IDictionary GetRegistryKeyNameValuePairs(RegistryKey registryKey)
		{
			Hashtable hashtable = new Hashtable(20);
			if (registryKey != null)
			{
				string[] valueNames = registryKey.GetValueNames();
				foreach (string text in valueNames)
				{
					string text2 = registryKey.GetValue(text, "").ToString();
					hashtable.Add(text, text2);
				}
			}
			return hashtable;
		}

		/// <summary>Retrieves all environment variable names and their values from the current process, or from the Windows operating system registry key for the current user or local machine.</summary>
		/// <param name="target">One of the <see cref="T:System.EnvironmentVariableTarget" /> values.</param>
		/// <returns>A dictionary that contains all environment variable names and their values from the source specified by the <paramref name="target" /> parameter; otherwise, an empty dictionary if no environment variables are found.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission to perform this operation for the specified value of <paramref name="target" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> contains an illegal value.</exception>
		// Token: 0x06000E50 RID: 3664 RVA: 0x0002C378 File Offset: 0x0002A578
		[SecuritySafeCritical]
		public static IDictionary GetEnvironmentVariables(EnvironmentVariableTarget target)
		{
			if (target == EnvironmentVariableTarget.Process)
			{
				return Environment.GetEnvironmentVariables();
			}
			new EnvironmentPermission(PermissionState.Unrestricted).Demand();
			if (target == EnvironmentVariableTarget.Machine)
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Control\\Session Manager\\Environment", false))
				{
					return Environment.GetRegistryKeyNameValuePairs(registryKey);
				}
			}
			if (target == EnvironmentVariableTarget.User)
			{
				using (RegistryKey registryKey2 = Registry.CurrentUser.OpenSubKey("Environment", false))
				{
					return Environment.GetRegistryKeyNameValuePairs(registryKey2);
				}
			}
			throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[] { (int)target }));
		}

		/// <summary>Creates, modifies, or deletes an environment variable stored in the current process.</summary>
		/// <param name="variable">The name of an environment variable.</param>
		/// <param name="value">A value to assign to <paramref name="variable" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="variable" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="variable" /> contains a zero-length string, an initial hexadecimal zero character (0x00), or an equal sign ("=").  
		/// -or-  
		/// The length of <paramref name="variable" /> or <paramref name="value" /> is greater than or equal to 32,767 characters.  
		/// -or-  
		/// An error occurred during the execution of this operation.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission to perform this operation.</exception>
		// Token: 0x06000E51 RID: 3665 RVA: 0x0002C428 File Offset: 0x0002A628
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static void SetEnvironmentVariable(string variable, string value)
		{
			Environment.CheckEnvironmentVariableName(variable);
			new EnvironmentPermission(PermissionState.Unrestricted).Demand();
			if (string.IsNullOrEmpty(value) || value[0] == '\0')
			{
				value = null;
			}
			else if (value.Length >= 32767)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_LongEnvVarValue"));
			}
			if (AppDomain.IsAppXModel() && !AppDomain.IsAppXDesignMode())
			{
				throw new PlatformNotSupportedException();
			}
			if (Win32Native.SetEnvironmentVariable(variable, value))
			{
				return;
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (lastWin32Error == 203)
			{
				return;
			}
			if (lastWin32Error == 206)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_LongEnvVarValue"));
			}
			throw new ArgumentException(Win32Native.GetMessage(lastWin32Error));
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x0002C4CC File Offset: 0x0002A6CC
		private static void CheckEnvironmentVariableName(string variable)
		{
			if (variable == null)
			{
				throw new ArgumentNullException("variable");
			}
			if (variable.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_StringZeroLength"), "variable");
			}
			if (variable[0] == '\0')
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_StringFirstCharIsZero"), "variable");
			}
			if (variable.Length >= 32767)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_LongEnvVarValue"));
			}
			if (variable.IndexOf('=') != -1)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_IllegalEnvVarName"));
			}
		}

		/// <summary>Creates, modifies, or deletes an environment variable stored in the current process or in the Windows operating system registry key reserved for the current user or local machine.</summary>
		/// <param name="variable">The name of an environment variable.</param>
		/// <param name="value">A value to assign to <paramref name="variable" />.</param>
		/// <param name="target">One of the enumeration values that specifies the location of the environment variable.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="variable" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="variable" /> contains a zero-length string, an initial hexadecimal zero character (0x00), or an equal sign ("=").  
		/// -or-  
		/// The length of <paramref name="variable" /> is greater than or equal to 32,767 characters.  
		/// -or-  
		/// <paramref name="target" /> is not a member of the <see cref="T:System.EnvironmentVariableTarget" /> enumeration.  
		/// -or-  
		/// <paramref name="target" /> is <see cref="F:System.EnvironmentVariableTarget.Machine" /> or <see cref="F:System.EnvironmentVariableTarget.User" />, and the length of <paramref name="variable" /> is greater than or equal to 255.  
		/// -or-  
		/// <paramref name="target" /> is <see cref="F:System.EnvironmentVariableTarget.Process" /> and the length of <paramref name="value" /> is greater than or equal to 32,767 characters.  
		/// -or-  
		/// An error occurred during the execution of this operation.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission to perform this operation.</exception>
		// Token: 0x06000E53 RID: 3667 RVA: 0x0002C55C File Offset: 0x0002A75C
		[SecuritySafeCritical]
		public static void SetEnvironmentVariable(string variable, string value, EnvironmentVariableTarget target)
		{
			if (target == EnvironmentVariableTarget.Process)
			{
				Environment.SetEnvironmentVariable(variable, value);
				return;
			}
			Environment.CheckEnvironmentVariableName(variable);
			if (variable.Length >= 1024)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_LongEnvVarName"));
			}
			new EnvironmentPermission(PermissionState.Unrestricted).Demand();
			if (string.IsNullOrEmpty(value) || value[0] == '\0')
			{
				value = null;
			}
			if (target == EnvironmentVariableTarget.Machine)
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Control\\Session Manager\\Environment", true))
				{
					if (registryKey != null)
					{
						if (value == null)
						{
							registryKey.DeleteValue(variable, false);
						}
						else
						{
							registryKey.SetValue(variable, value);
						}
					}
					goto IL_FE;
				}
			}
			if (target == EnvironmentVariableTarget.User)
			{
				if (variable.Length >= 255)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_LongEnvVarValue"));
				}
				using (RegistryKey registryKey2 = Registry.CurrentUser.OpenSubKey("Environment", true))
				{
					if (registryKey2 != null)
					{
						if (value == null)
						{
							registryKey2.DeleteValue(variable, false);
						}
						else
						{
							registryKey2.SetValue(variable, value);
						}
					}
					goto IL_FE;
				}
			}
			throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[] { (int)target }));
			IL_FE:
			IntPtr intPtr = Win32Native.SendMessageTimeout(new IntPtr(65535), 26, IntPtr.Zero, "Environment", 0U, 1000U, IntPtr.Zero);
			intPtr == IntPtr.Zero;
		}

		/// <summary>Returns an array of string containing the names of the logical drives on the current computer.</summary>
		/// <returns>An array of strings where each element contains the name of a logical drive. For example, if the computer's hard drive is the first logical drive, the first element returned is "C:\".</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permissions.</exception>
		// Token: 0x06000E54 RID: 3668 RVA: 0x0002C6B8 File Offset: 0x0002A8B8
		[SecuritySafeCritical]
		public static string[] GetLogicalDrives()
		{
			new EnvironmentPermission(PermissionState.Unrestricted).Demand();
			int logicalDrives = Win32Native.GetLogicalDrives();
			if (logicalDrives == 0)
			{
				__Error.WinIOError();
			}
			uint num = (uint)logicalDrives;
			int num2 = 0;
			while (num != 0U)
			{
				if ((num & 1U) != 0U)
				{
					num2++;
				}
				num >>= 1;
			}
			string[] array = new string[num2];
			char[] array2 = new char[] { 'A', ':', '\\' };
			num = (uint)logicalDrives;
			num2 = 0;
			while (num != 0U)
			{
				if ((num & 1U) != 0U)
				{
					array[num2++] = new string(array2);
				}
				num >>= 1;
				char[] array3 = array2;
				int num3 = 0;
				array3[num3] += '\u0001';
			}
			return array;
		}

		/// <summary>Gets the newline string defined for this environment.</summary>
		/// <returns>A string containing "\r\n" for non-Unix platforms, or a string containing "\n" for Unix platforms.</returns>
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x0002C73D File Offset: 0x0002A93D
		[__DynamicallyInvokable]
		public static string NewLine
		{
			[__DynamicallyInvokable]
			get
			{
				return "\r\n";
			}
		}

		/// <summary>Gets a <see cref="T:System.Version" /> object that describes the major, minor, build, and revision numbers of the common language runtime.</summary>
		/// <returns>An object that displays the version of the common language runtime.</returns>
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000E56 RID: 3670 RVA: 0x0002C744 File Offset: 0x0002A944
		public static Version Version
		{
			get
			{
				return new Version(4, 0, 30319, 42000);
			}
		}

		// Token: 0x06000E57 RID: 3671
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern long GetWorkingSet();

		/// <summary>Gets the amount of physical memory mapped to the process context.</summary>
		/// <returns>A 64-bit signed integer containing the number of bytes of physical memory mapped to the process context.</returns>
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000E58 RID: 3672 RVA: 0x0002C757 File Offset: 0x0002A957
		public static long WorkingSet
		{
			[SecuritySafeCritical]
			get
			{
				new EnvironmentPermission(PermissionState.Unrestricted).Demand();
				return Environment.GetWorkingSet();
			}
		}

		/// <summary>Gets an <see cref="T:System.OperatingSystem" /> object that contains the current platform identifier and version number.</summary>
		/// <returns>An object that contains the platform identifier and version number.</returns>
		/// <exception cref="T:System.InvalidOperationException">This property was unable to obtain the system version.  
		///  -or-  
		///  The obtained platform identifier is not a member of <see cref="T:System.PlatformID" /></exception>
		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000E59 RID: 3673 RVA: 0x0002C76C File Offset: 0x0002A96C
		public static OperatingSystem OSVersion
		{
			[SecuritySafeCritical]
			get
			{
				if (Environment.m_os == null)
				{
					Win32Native.OSVERSIONINFO osversioninfo = new Win32Native.OSVERSIONINFO();
					if (!Environment.GetVersion(osversioninfo))
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_GetVersion"));
					}
					Win32Native.OSVERSIONINFOEX osversioninfoex = new Win32Native.OSVERSIONINFOEX();
					if (!Environment.GetVersionEx(osversioninfoex))
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_GetVersion"));
					}
					PlatformID platformID = PlatformID.Win32NT;
					Version version = new Version(osversioninfo.MajorVersion, osversioninfo.MinorVersion, osversioninfo.BuildNumber, ((int)osversioninfoex.ServicePackMajor << 16) | (int)osversioninfoex.ServicePackMinor);
					Environment.m_os = new OperatingSystem(platformID, version, osversioninfo.CSDVersion);
				}
				return Environment.m_os;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000E5A RID: 3674 RVA: 0x0002C804 File Offset: 0x0002AA04
		internal static bool IsWindows8OrAbove
		{
			get
			{
				if (!Environment.s_CheckedOSWin8OrAbove)
				{
					OperatingSystem osversion = Environment.OSVersion;
					Environment.s_IsWindows8OrAbove = osversion.Platform == PlatformID.Win32NT && ((osversion.Version.Major == 6 && osversion.Version.Minor >= 2) || osversion.Version.Major > 6);
					Environment.s_CheckedOSWin8OrAbove = true;
				}
				return Environment.s_IsWindows8OrAbove;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000E5B RID: 3675 RVA: 0x0002C86F File Offset: 0x0002AA6F
		internal static bool IsWinRTSupported
		{
			[SecuritySafeCritical]
			get
			{
				if (!Environment.s_CheckedWinRT)
				{
					Environment.s_WinRTSupported = Environment.WinRTSupported();
					Environment.s_CheckedWinRT = true;
				}
				return Environment.s_WinRTSupported;
			}
		}

		// Token: 0x06000E5C RID: 3676
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool WinRTSupported();

		// Token: 0x06000E5D RID: 3677
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool GetVersion(Win32Native.OSVERSIONINFO osVer);

		// Token: 0x06000E5E RID: 3678
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool GetVersionEx(Win32Native.OSVERSIONINFOEX osVer);

		/// <summary>Gets current stack trace information.</summary>
		/// <returns>A string containing stack trace information. This value can be <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000E5F RID: 3679 RVA: 0x0002C895 File Offset: 0x0002AA95
		[__DynamicallyInvokable]
		public static string StackTrace
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				new EnvironmentPermission(PermissionState.Unrestricted).Demand();
				return Environment.GetStackTrace(null, true);
			}
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x0002C8AC File Offset: 0x0002AAAC
		internal static string GetStackTrace(Exception e, bool needFileInfo)
		{
			StackTrace stackTrace;
			if (e == null)
			{
				stackTrace = new StackTrace(needFileInfo);
			}
			else
			{
				stackTrace = new StackTrace(e, needFileInfo);
			}
			return stackTrace.ToString(System.Diagnostics.StackTrace.TraceFormat.Normal);
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x0002C8D4 File Offset: 0x0002AAD4
		[SecuritySafeCritical]
		private static void InitResourceHelper()
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Monitor.Enter(Environment.InternalSyncObject, ref flag);
				if (Environment.m_resHelper == null)
				{
					Environment.ResourceHelper resourceHelper = new Environment.ResourceHelper("mscorlib");
					Thread.MemoryBarrier();
					Environment.m_resHelper = resourceHelper;
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(Environment.InternalSyncObject);
				}
			}
		}

		// Token: 0x06000E62 RID: 3682
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetResourceFromDefault(string key);

		// Token: 0x06000E63 RID: 3683 RVA: 0x0002C938 File Offset: 0x0002AB38
		internal static string GetResourceStringLocal(string key)
		{
			if (Environment.m_resHelper == null)
			{
				Environment.InitResourceHelper();
			}
			return Environment.m_resHelper.GetResourceString(key);
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x0002C955 File Offset: 0x0002AB55
		[SecuritySafeCritical]
		internal static string GetResourceString(string key)
		{
			return Environment.GetResourceFromDefault(key);
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x0002C960 File Offset: 0x0002AB60
		[SecuritySafeCritical]
		internal static string GetResourceString(string key, params object[] values)
		{
			string resourceString = Environment.GetResourceString(key);
			return string.Format(CultureInfo.CurrentCulture, resourceString, values);
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x0002C980 File Offset: 0x0002AB80
		internal static string GetRuntimeResourceString(string key)
		{
			return Environment.GetResourceString(key);
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x0002C988 File Offset: 0x0002AB88
		internal static string GetRuntimeResourceString(string key, params object[] values)
		{
			return Environment.GetResourceString(key, values);
		}

		/// <summary>Determines whether the current process is a 64-bit process.</summary>
		/// <returns>
		///   <see langword="true" /> if the process is 64-bit; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x0002C991 File Offset: 0x0002AB91
		public static bool Is64BitProcess
		{
			get
			{
				return true;
			}
		}

		/// <summary>Determines whether the current operating system is a 64-bit operating system.</summary>
		/// <returns>
		///   <see langword="true" /> if the operating system is 64-bit; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000E69 RID: 3689 RVA: 0x0002C994 File Offset: 0x0002AB94
		public static bool Is64BitOperatingSystem
		{
			[SecuritySafeCritical]
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value that indicates whether the current application domain is being unloaded or the common language runtime (CLR) is shutting down.</summary>
		/// <returns>
		///   <see langword="true" /> if the current application domain is being unloaded or the CLR is shutting down; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000E6A RID: 3690
		[__DynamicallyInvokable]
		public static extern bool HasShutdownStarted
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000E6B RID: 3691
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool GetCompatibilityFlag(CompatibilityFlag flag);

		/// <summary>Gets the user name of the person who is currently logged on to the operating system.</summary>
		/// <returns>The user name of the person who is logged on to the operating system.</returns>
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x0002C998 File Offset: 0x0002AB98
		public static string UserName
		{
			[SecuritySafeCritical]
			get
			{
				new EnvironmentPermission(EnvironmentPermissionAccess.Read, "UserName").Demand();
				StringBuilder stringBuilder = new StringBuilder(256);
				int capacity = stringBuilder.Capacity;
				if (Win32Native.GetUserName(stringBuilder, ref capacity))
				{
					return stringBuilder.ToString();
				}
				return string.Empty;
			}
		}

		/// <summary>Gets a value indicating whether the current process is running in user interactive mode.</summary>
		/// <returns>
		///   <see langword="true" /> if the current process is running in user interactive mode; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000E6D RID: 3693 RVA: 0x0002C9E0 File Offset: 0x0002ABE0
		public static bool UserInteractive
		{
			[SecuritySafeCritical]
			get
			{
				IntPtr processWindowStation = Win32Native.GetProcessWindowStation();
				if (processWindowStation != IntPtr.Zero && Environment.processWinStation != processWindowStation)
				{
					int num = 0;
					Win32Native.USEROBJECTFLAGS userobjectflags = new Win32Native.USEROBJECTFLAGS();
					if (Win32Native.GetUserObjectInformation(processWindowStation, 1, userobjectflags, Marshal.SizeOf<Win32Native.USEROBJECTFLAGS>(userobjectflags), ref num) && (userobjectflags.dwFlags & 1) == 0)
					{
						Environment.isUserNonInteractive = true;
					}
					Environment.processWinStation = processWindowStation;
				}
				return !Environment.isUserNonInteractive;
			}
		}

		/// <summary>Gets the path to the system special folder that is identified by the specified enumeration.</summary>
		/// <param name="folder">One of enumeration values that identifies a system special folder.</param>
		/// <returns>The path to the specified system special folder, if that folder physically exists on your computer; otherwise, an empty string ("").  
		///  A folder will not physically exist if the operating system did not create it, the existing folder was deleted, or the folder is a virtual directory, such as My Computer, which does not correspond to a physical path.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="folder" /> is not a member of <see cref="T:System.Environment.SpecialFolder" />.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current platform is not supported.</exception>
		// Token: 0x06000E6E RID: 3694 RVA: 0x0002CA4D File Offset: 0x0002AC4D
		[SecuritySafeCritical]
		public static string GetFolderPath(Environment.SpecialFolder folder)
		{
			if (!Enum.IsDefined(typeof(Environment.SpecialFolder), folder))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[] { (int)folder }));
			}
			return Environment.InternalGetFolderPath(folder, Environment.SpecialFolderOption.None, false);
		}

		/// <summary>Gets the path to the system special folder that is identified by the specified enumeration, and uses a specified option for accessing special folders.</summary>
		/// <param name="folder">One of the enumeration values that identifies a system special folder.</param>
		/// <param name="option">One of the enumeration values taht specifies options to use for accessing a special folder.</param>
		/// <returns>The path to the specified system special folder, if that folder physically exists on your computer; otherwise, an empty string ("").  
		///  A folder will not physically exist if the operating system did not create it, the existing folder was deleted, or the folder is a virtual directory, such as My Computer, which does not correspond to a physical path.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="folder" /> is not a member of <see cref="T:System.Environment.SpecialFolder" />.
		/// -or-
		/// <paramref name="options" /> is not a member of <see cref="T:System.Environment.SpecialFolderOption" />.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current platform is not supported.</exception>
		// Token: 0x06000E6F RID: 3695 RVA: 0x0002CA90 File Offset: 0x0002AC90
		[SecuritySafeCritical]
		public static string GetFolderPath(Environment.SpecialFolder folder, Environment.SpecialFolderOption option)
		{
			if (!Enum.IsDefined(typeof(Environment.SpecialFolder), folder))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[] { (int)folder }));
			}
			if (!Enum.IsDefined(typeof(Environment.SpecialFolderOption), option))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[] { (int)option }));
			}
			return Environment.InternalGetFolderPath(folder, option, false);
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x0002CB11 File Offset: 0x0002AD11
		[SecurityCritical]
		internal static string UnsafeGetFolderPath(Environment.SpecialFolder folder)
		{
			return Environment.InternalGetFolderPath(folder, Environment.SpecialFolderOption.None, true);
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x0002CB1C File Offset: 0x0002AD1C
		[SecurityCritical]
		private static string InternalGetFolderPath(Environment.SpecialFolder folder, Environment.SpecialFolderOption option, bool suppressSecurityChecks = false)
		{
			if (option == Environment.SpecialFolderOption.Create && !suppressSecurityChecks)
			{
				new FileIOPermission(PermissionState.None)
				{
					AllFiles = FileIOPermissionAccess.Write
				}.Demand();
			}
			StringBuilder stringBuilder = new StringBuilder(260);
			int num = Win32Native.SHGetFolderPath(IntPtr.Zero, (int)(folder | (Environment.SpecialFolder)option), IntPtr.Zero, 0, stringBuilder);
			string text;
			if (num < 0)
			{
				if (num == -2146233031)
				{
					throw new PlatformNotSupportedException();
				}
				text = string.Empty;
			}
			else
			{
				text = stringBuilder.ToString();
			}
			if (!suppressSecurityChecks)
			{
				new FileIOPermission(FileIOPermissionAccess.PathDiscovery, text).Demand();
			}
			return text;
		}

		/// <summary>Gets the network domain name associated with the current user.</summary>
		/// <returns>The network domain name associated with the current user.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The operating system does not support retrieving the network domain name.</exception>
		/// <exception cref="T:System.InvalidOperationException">The network domain name cannot be retrieved.</exception>
		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000E72 RID: 3698 RVA: 0x0002CB9C File Offset: 0x0002AD9C
		public static string UserDomainName
		{
			[SecuritySafeCritical]
			get
			{
				new EnvironmentPermission(EnvironmentPermissionAccess.Read, "UserDomain").Demand();
				byte[] array = new byte[1024];
				int num = array.Length;
				StringBuilder stringBuilder = new StringBuilder(1024);
				uint num2 = (uint)stringBuilder.Capacity;
				byte userNameEx = Win32Native.GetUserNameEx(2, stringBuilder, ref num2);
				if (userNameEx == 1)
				{
					string text = stringBuilder.ToString();
					int num3 = text.IndexOf('\\');
					if (num3 != -1)
					{
						return text.Substring(0, num3);
					}
				}
				num2 = (uint)stringBuilder.Capacity;
				int num4;
				if (!Win32Native.LookupAccountName(null, Environment.UserName, array, ref num, stringBuilder, ref num2, out num4))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					throw new InvalidOperationException(Win32Native.GetMessage(lastWin32Error));
				}
				return stringBuilder.ToString();
			}
		}

		/// <summary>Gets a unique identifier for the current managed thread.</summary>
		/// <returns>An integer that represents a unique identifier for this managed thread.</returns>
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x0002CC47 File Offset: 0x0002AE47
		[__DynamicallyInvokable]
		public static int CurrentManagedThreadId
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[__DynamicallyInvokable]
			get
			{
				return Thread.CurrentThread.ManagedThreadId;
			}
		}

		// Token: 0x0400056F RID: 1391
		private const int MaxEnvVariableValueLength = 32767;

		// Token: 0x04000570 RID: 1392
		private const int MaxSystemEnvVariableLength = 1024;

		// Token: 0x04000571 RID: 1393
		private const int MaxUserEnvVariableLength = 255;

		// Token: 0x04000572 RID: 1394
		private static volatile Environment.ResourceHelper m_resHelper;

		// Token: 0x04000573 RID: 1395
		private const int MaxMachineNameLength = 256;

		// Token: 0x04000574 RID: 1396
		private static object s_InternalSyncObject;

		// Token: 0x04000575 RID: 1397
		private static volatile OperatingSystem m_os;

		// Token: 0x04000576 RID: 1398
		private static volatile bool s_IsWindows8OrAbove;

		// Token: 0x04000577 RID: 1399
		private static volatile bool s_CheckedOSWin8OrAbove;

		// Token: 0x04000578 RID: 1400
		private static volatile bool s_WinRTSupported;

		// Token: 0x04000579 RID: 1401
		private static volatile bool s_CheckedWinRT;

		// Token: 0x0400057A RID: 1402
		private static volatile IntPtr processWinStation;

		// Token: 0x0400057B RID: 1403
		private static volatile bool isUserNonInteractive;

		// Token: 0x02000AE3 RID: 2787
		internal sealed class ResourceHelper
		{
			// Token: 0x06006A18 RID: 27160 RVA: 0x0016E7BF File Offset: 0x0016C9BF
			internal ResourceHelper(string name)
			{
				this.m_name = name;
			}

			// Token: 0x06006A19 RID: 27161 RVA: 0x0016E7CE File Offset: 0x0016C9CE
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			internal string GetResourceString(string key)
			{
				if (key == null || key.Length == 0)
				{
					return "[Resource lookup failed - null or empty resource name]";
				}
				return this.GetResourceString(key, null);
			}

			// Token: 0x06006A1A RID: 27162 RVA: 0x0016E7EC File Offset: 0x0016C9EC
			[SecuritySafeCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			internal string GetResourceString(string key, CultureInfo culture)
			{
				if (key == null || key.Length == 0)
				{
					return "[Resource lookup failed - null or empty resource name]";
				}
				Environment.ResourceHelper.GetResourceStringUserData getResourceStringUserData = new Environment.ResourceHelper.GetResourceStringUserData(this, key, culture);
				RuntimeHelpers.TryCode tryCode = new RuntimeHelpers.TryCode(this.GetResourceStringCode);
				RuntimeHelpers.CleanupCode cleanupCode = new RuntimeHelpers.CleanupCode(this.GetResourceStringBackoutCode);
				RuntimeHelpers.ExecuteCodeWithGuaranteedCleanup(tryCode, cleanupCode, getResourceStringUserData);
				return getResourceStringUserData.m_retVal;
			}

			// Token: 0x06006A1B RID: 27163 RVA: 0x0016E83C File Offset: 0x0016CA3C
			[SecuritySafeCritical]
			private void GetResourceStringCode(object userDataIn)
			{
				Environment.ResourceHelper.GetResourceStringUserData getResourceStringUserData = (Environment.ResourceHelper.GetResourceStringUserData)userDataIn;
				Environment.ResourceHelper resourceHelper = getResourceStringUserData.m_resourceHelper;
				string key = getResourceStringUserData.m_key;
				CultureInfo culture = getResourceStringUserData.m_culture;
				Monitor.Enter(resourceHelper, ref getResourceStringUserData.m_lockWasTaken);
				if (resourceHelper.currentlyLoading != null && resourceHelper.currentlyLoading.Count > 0 && resourceHelper.currentlyLoading.Contains(key))
				{
					if (resourceHelper.infinitelyRecursingCount > 0)
					{
						getResourceStringUserData.m_retVal = "[Resource lookup failed - infinite recursion or critical failure detected.]";
						return;
					}
					resourceHelper.infinitelyRecursingCount++;
					string text = "Infinite recursion during resource lookup within mscorlib.  This may be a bug in mscorlib, or potentially in certain extensibility points such as assembly resolve events or CultureInfo names.  Resource name: " + key;
					Assert.Fail("[mscorlib recursive resource lookup bug]", text, -2146232797, System.Diagnostics.StackTrace.TraceFormat.NoResourceLookup);
					Environment.FailFast(text);
				}
				if (resourceHelper.currentlyLoading == null)
				{
					resourceHelper.currentlyLoading = new Stack(4);
				}
				if (!resourceHelper.resourceManagerInited)
				{
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
					}
					finally
					{
						RuntimeHelpers.RunClassConstructor(typeof(ResourceManager).TypeHandle);
						RuntimeHelpers.RunClassConstructor(typeof(ResourceReader).TypeHandle);
						RuntimeHelpers.RunClassConstructor(typeof(RuntimeResourceSet).TypeHandle);
						RuntimeHelpers.RunClassConstructor(typeof(BinaryReader).TypeHandle);
						resourceHelper.resourceManagerInited = true;
					}
				}
				resourceHelper.currentlyLoading.Push(key);
				if (resourceHelper.SystemResMgr == null)
				{
					resourceHelper.SystemResMgr = new ResourceManager(this.m_name, typeof(object).Assembly);
				}
				string @string = resourceHelper.SystemResMgr.GetString(key, null);
				resourceHelper.currentlyLoading.Pop();
				getResourceStringUserData.m_retVal = @string;
			}

			// Token: 0x06006A1C RID: 27164 RVA: 0x0016E9C0 File Offset: 0x0016CBC0
			[PrePrepareMethod]
			private void GetResourceStringBackoutCode(object userDataIn, bool exceptionThrown)
			{
				Environment.ResourceHelper.GetResourceStringUserData getResourceStringUserData = (Environment.ResourceHelper.GetResourceStringUserData)userDataIn;
				Environment.ResourceHelper resourceHelper = getResourceStringUserData.m_resourceHelper;
				if (exceptionThrown && getResourceStringUserData.m_lockWasTaken)
				{
					resourceHelper.SystemResMgr = null;
					resourceHelper.currentlyLoading = null;
				}
				if (getResourceStringUserData.m_lockWasTaken)
				{
					Monitor.Exit(resourceHelper);
				}
			}

			// Token: 0x0400314D RID: 12621
			private string m_name;

			// Token: 0x0400314E RID: 12622
			private ResourceManager SystemResMgr;

			// Token: 0x0400314F RID: 12623
			private Stack currentlyLoading;

			// Token: 0x04003150 RID: 12624
			internal bool resourceManagerInited;

			// Token: 0x04003151 RID: 12625
			private int infinitelyRecursingCount;

			// Token: 0x02000CF5 RID: 3317
			internal class GetResourceStringUserData
			{
				// Token: 0x060071E4 RID: 29156 RVA: 0x00188CE1 File Offset: 0x00186EE1
				public GetResourceStringUserData(Environment.ResourceHelper resourceHelper, string key, CultureInfo culture)
				{
					this.m_resourceHelper = resourceHelper;
					this.m_key = key;
					this.m_culture = culture;
				}

				// Token: 0x0400390C RID: 14604
				public Environment.ResourceHelper m_resourceHelper;

				// Token: 0x0400390D RID: 14605
				public string m_key;

				// Token: 0x0400390E RID: 14606
				public CultureInfo m_culture;

				// Token: 0x0400390F RID: 14607
				public string m_retVal;

				// Token: 0x04003910 RID: 14608
				public bool m_lockWasTaken;
			}
		}

		/// <summary>Specifies options to use for getting the path to a special folder.</summary>
		// Token: 0x02000AE4 RID: 2788
		public enum SpecialFolderOption
		{
			/// <summary>The path to the folder is verified. If the folder exists, the path is returned. If the folder does not exist, an empty string is returned. This is the default behavior.</summary>
			// Token: 0x04003153 RID: 12627
			None,
			/// <summary>The path to the folder is created if it does not already exist.</summary>
			// Token: 0x04003154 RID: 12628
			Create = 32768,
			/// <summary>The path to the folder is returned without verifying whether the path exists. If the folder is located on a network, specifying this option can reduce lag time.</summary>
			// Token: 0x04003155 RID: 12629
			DoNotVerify = 16384
		}

		/// <summary>Specifies enumerated constants used to retrieve directory paths to system special folders.</summary>
		// Token: 0x02000AE5 RID: 2789
		[ComVisible(true)]
		public enum SpecialFolder
		{
			/// <summary>The directory that serves as a common repository for application-specific data for the current roaming user.</summary>
			// Token: 0x04003157 RID: 12631
			ApplicationData = 26,
			/// <summary>The directory that serves as a common repository for application-specific data that is used by all users.</summary>
			// Token: 0x04003158 RID: 12632
			CommonApplicationData = 35,
			/// <summary>The directory that serves as a common repository for application-specific data that is used by the current, non-roaming user.</summary>
			// Token: 0x04003159 RID: 12633
			LocalApplicationData = 28,
			/// <summary>The directory that serves as a common repository for Internet cookies.</summary>
			// Token: 0x0400315A RID: 12634
			Cookies = 33,
			/// <summary>The logical Desktop rather than the physical file system location.</summary>
			// Token: 0x0400315B RID: 12635
			Desktop = 0,
			/// <summary>The directory that serves as a common repository for the user's favorite items.</summary>
			// Token: 0x0400315C RID: 12636
			Favorites = 6,
			/// <summary>The directory that serves as a common repository for Internet history items.</summary>
			// Token: 0x0400315D RID: 12637
			History = 34,
			/// <summary>The directory that serves as a common repository for temporary Internet files.</summary>
			// Token: 0x0400315E RID: 12638
			InternetCache = 32,
			/// <summary>The directory that contains the user's program groups.</summary>
			// Token: 0x0400315F RID: 12639
			Programs = 2,
			/// <summary>The My Computer folder.</summary>
			// Token: 0x04003160 RID: 12640
			MyComputer = 17,
			/// <summary>The My Music folder.</summary>
			// Token: 0x04003161 RID: 12641
			MyMusic = 13,
			/// <summary>The My Pictures folder.</summary>
			// Token: 0x04003162 RID: 12642
			MyPictures = 39,
			/// <summary>The file system directory that serves as a repository for videos that belong to a user.  Added in the .NET Framework 4.</summary>
			// Token: 0x04003163 RID: 12643
			MyVideos = 14,
			/// <summary>The directory that contains the user's most recently used documents.</summary>
			// Token: 0x04003164 RID: 12644
			Recent = 8,
			/// <summary>The directory that contains the Send To menu items.</summary>
			// Token: 0x04003165 RID: 12645
			SendTo,
			/// <summary>The directory that contains the Start menu items.</summary>
			// Token: 0x04003166 RID: 12646
			StartMenu = 11,
			/// <summary>The directory that corresponds to the user's Startup program group.</summary>
			// Token: 0x04003167 RID: 12647
			Startup = 7,
			/// <summary>The System directory.</summary>
			// Token: 0x04003168 RID: 12648
			System = 37,
			/// <summary>The directory that serves as a common repository for document templates.</summary>
			// Token: 0x04003169 RID: 12649
			Templates = 21,
			/// <summary>The directory used to physically store file objects on the desktop.</summary>
			// Token: 0x0400316A RID: 12650
			DesktopDirectory = 16,
			/// <summary>The directory that serves as a common repository for documents.</summary>
			// Token: 0x0400316B RID: 12651
			Personal = 5,
			/// <summary>The My Documents folder.</summary>
			// Token: 0x0400316C RID: 12652
			MyDocuments = 5,
			/// <summary>The program files directory.  
			///  On a non-x86 system, passing <see cref="F:System.Environment.SpecialFolder.ProgramFiles" /> to the <see cref="M:System.Environment.GetFolderPath(System.Environment.SpecialFolder)" /> method returns the path for non-x86 programs. To get the x86 program files directory on a non-x86 system, use the <see cref="F:System.Environment.SpecialFolder.ProgramFilesX86" /> member.</summary>
			// Token: 0x0400316D RID: 12653
			ProgramFiles = 38,
			/// <summary>The directory for components that are shared across applications.  
			///  To get the x86 common program files directory on a non-x86 system, use the <see cref="F:System.Environment.SpecialFolder.ProgramFilesX86" /> member.</summary>
			// Token: 0x0400316E RID: 12654
			CommonProgramFiles = 43,
			/// <summary>The file system directory that is used to store administrative tools for an individual user. The Microsoft Management Console (MMC) will save customized consoles to this directory, and it will roam with the user. Added in the .NET Framework 4.</summary>
			// Token: 0x0400316F RID: 12655
			AdminTools = 48,
			/// <summary>The file system directory that acts as a staging area for files waiting to be written to a CD. Added in the .NET Framework 4.</summary>
			// Token: 0x04003170 RID: 12656
			CDBurning = 59,
			/// <summary>The file system directory that contains administrative tools for all users of the computer. Added in the .NET Framework 4.</summary>
			// Token: 0x04003171 RID: 12657
			CommonAdminTools = 47,
			/// <summary>The file system directory that contains documents that are common to all users. This special folder is valid for Windows NT systems, Windows 95, and Windows 98 systems with Shfolder.dll installed. Added in the .NET Framework 4.</summary>
			// Token: 0x04003172 RID: 12658
			CommonDocuments = 46,
			/// <summary>The file system directory that serves as a repository for music files common to all users. Added in the .NET Framework 4.</summary>
			// Token: 0x04003173 RID: 12659
			CommonMusic = 53,
			/// <summary>This value is recognized in Windows Vista for backward compatibility, but the special folder itself is no longer used. Added in the .NET Framework 4.</summary>
			// Token: 0x04003174 RID: 12660
			CommonOemLinks = 58,
			/// <summary>The file system directory that serves as a repository for image files common to all users. Added in the .NET Framework 4.</summary>
			// Token: 0x04003175 RID: 12661
			CommonPictures = 54,
			/// <summary>The file system directory that contains the programs and folders that appear on the Start menu for all users. This special folder is valid only for Windows NT systems. Added in the .NET Framework 4.</summary>
			// Token: 0x04003176 RID: 12662
			CommonStartMenu = 22,
			/// <summary>A folder for components that are shared across applications. This special folder is valid only for Windows NT, Windows 2000, and Windows XP systems. Added in the .NET Framework 4.</summary>
			// Token: 0x04003177 RID: 12663
			CommonPrograms,
			/// <summary>The file system directory that contains the programs that appear in the Startup folder for all users. This special folder is valid only for Windows NT systems. Added in the .NET Framework 4.</summary>
			// Token: 0x04003178 RID: 12664
			CommonStartup,
			/// <summary>The file system directory that contains files and folders that appear on the desktop for all users. This special folder is valid only for Windows NT systems. Added in the .NET Framework 4.</summary>
			// Token: 0x04003179 RID: 12665
			CommonDesktopDirectory,
			/// <summary>The file system directory that contains the templates that are available to all users. This special folder is valid only for Windows NT systems.  Added in the .NET Framework 4.</summary>
			// Token: 0x0400317A RID: 12666
			CommonTemplates = 45,
			/// <summary>The file system directory that serves as a repository for video files common to all users. Added in the .NET Framework 4.</summary>
			// Token: 0x0400317B RID: 12667
			CommonVideos = 55,
			/// <summary>A virtual folder that contains fonts. Added in the .NET Framework 4.</summary>
			// Token: 0x0400317C RID: 12668
			Fonts = 20,
			/// <summary>A file system directory that contains the link objects that may exist in the My Network Places virtual folder. Added in the .NET Framework 4.</summary>
			// Token: 0x0400317D RID: 12669
			NetworkShortcuts = 19,
			/// <summary>The file system directory that contains the link objects that can exist in the Printers virtual folder. Added in the .NET Framework 4.</summary>
			// Token: 0x0400317E RID: 12670
			PrinterShortcuts = 27,
			/// <summary>The user's profile folder. Applications should not create files or folders at this level; they should put their data under the locations referred to by <see cref="F:System.Environment.SpecialFolder.ApplicationData" />. Added in the .NET Framework 4.</summary>
			// Token: 0x0400317F RID: 12671
			UserProfile = 40,
			/// <summary>The Program Files folder. Added in the .NET Framework 4.</summary>
			// Token: 0x04003180 RID: 12672
			CommonProgramFilesX86 = 44,
			/// <summary>The x86 Program Files folder. Added in the .NET Framework 4.</summary>
			// Token: 0x04003181 RID: 12673
			ProgramFilesX86 = 42,
			/// <summary>The file system directory that contains resource data. Added in the .NET Framework 4.</summary>
			// Token: 0x04003182 RID: 12674
			Resources = 56,
			/// <summary>The file system directory that contains localized resource data. Added in the .NET Framework 4.</summary>
			// Token: 0x04003183 RID: 12675
			LocalizedResources,
			/// <summary>The Windows System folder. Added in the .NET Framework 4.</summary>
			// Token: 0x04003184 RID: 12676
			SystemX86 = 41,
			/// <summary>The Windows directory or SYSROOT. This corresponds to the %windir% or %SYSTEMROOT% environment variables. Added in the .NET Framework 4.</summary>
			// Token: 0x04003185 RID: 12677
			Windows = 36
		}
	}
}
