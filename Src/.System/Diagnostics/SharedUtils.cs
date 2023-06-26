using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Diagnostics
{
	// Token: 0x02000505 RID: 1285
	internal static class SharedUtils
	{
		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x060030D8 RID: 12504 RVA: 0x000DDB78 File Offset: 0x000DBD78
		private static object InternalSyncObject
		{
			get
			{
				if (SharedUtils.s_InternalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange(ref SharedUtils.s_InternalSyncObject, obj, null);
				}
				return SharedUtils.s_InternalSyncObject;
			}
		}

		// Token: 0x060030D9 RID: 12505 RVA: 0x000DDBA4 File Offset: 0x000DBDA4
		internal static Win32Exception CreateSafeWin32Exception()
		{
			return SharedUtils.CreateSafeWin32Exception(0);
		}

		// Token: 0x060030DA RID: 12506 RVA: 0x000DDBAC File Offset: 0x000DBDAC
		internal static Win32Exception CreateSafeWin32Exception(int error)
		{
			Win32Exception ex = null;
			SecurityPermission securityPermission = new SecurityPermission(PermissionState.Unrestricted);
			securityPermission.Assert();
			try
			{
				if (error == 0)
				{
					ex = new Win32Exception();
				}
				else
				{
					ex = new Win32Exception(error);
				}
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return ex;
		}

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x060030DB RID: 12507 RVA: 0x000DDBF4 File Offset: 0x000DBDF4
		internal static int CurrentEnvironment
		{
			get
			{
				if (SharedUtils.environment == 0)
				{
					object internalSyncObject = SharedUtils.InternalSyncObject;
					lock (internalSyncObject)
					{
						if (SharedUtils.environment == 0)
						{
							if (Environment.OSVersion.Platform == PlatformID.Win32NT)
							{
								if (Environment.OSVersion.Version.Major >= 5)
								{
									SharedUtils.environment = 1;
								}
								else
								{
									SharedUtils.environment = 2;
								}
							}
							else
							{
								SharedUtils.environment = 3;
							}
						}
					}
				}
				return SharedUtils.environment;
			}
		}

		// Token: 0x060030DC RID: 12508 RVA: 0x000DDC84 File Offset: 0x000DBE84
		internal static void CheckEnvironment()
		{
			if (SharedUtils.CurrentEnvironment == 3)
			{
				throw new PlatformNotSupportedException(SR.GetString("WinNTRequired"));
			}
		}

		// Token: 0x060030DD RID: 12509 RVA: 0x000DDC9E File Offset: 0x000DBE9E
		internal static void CheckNtEnvironment()
		{
			if (SharedUtils.CurrentEnvironment == 2)
			{
				throw new PlatformNotSupportedException(SR.GetString("Win2000Required"));
			}
		}

		// Token: 0x060030DE RID: 12510 RVA: 0x000DDCB8 File Offset: 0x000DBEB8
		internal static void EnterMutex(string name, ref Mutex mutex)
		{
			string text;
			if (SharedUtils.CurrentEnvironment == 1)
			{
				text = "Global\\" + name;
			}
			else
			{
				text = name;
			}
			SharedUtils.EnterMutexWithoutGlobal(text, ref mutex);
		}

		// Token: 0x060030DF RID: 12511 RVA: 0x000DDCE8 File Offset: 0x000DBEE8
		[SecurityPermission(SecurityAction.Assert, ControlPrincipal = true)]
		internal static void EnterMutexWithoutGlobal(string mutexName, ref Mutex mutex)
		{
			MutexSecurity mutexSecurity = new MutexSecurity();
			SecurityIdentifier securityIdentifier = new SecurityIdentifier(WellKnownSidType.AuthenticatedUserSid, null);
			mutexSecurity.AddAccessRule(new MutexAccessRule(securityIdentifier, MutexRights.Modify | MutexRights.Synchronize, AccessControlType.Allow));
			bool flag;
			Mutex mutex2 = new Mutex(false, mutexName, out flag, mutexSecurity);
			SharedUtils.SafeWaitForMutex(mutex2, ref mutex);
		}

		// Token: 0x060030E0 RID: 12512 RVA: 0x000DDD29 File Offset: 0x000DBF29
		private static bool SafeWaitForMutex(Mutex mutexIn, ref Mutex mutexOut)
		{
			while (SharedUtils.SafeWaitForMutexOnce(mutexIn, ref mutexOut))
			{
				if (mutexOut != null)
				{
					return true;
				}
				Thread.Sleep(0);
			}
			return false;
		}

		// Token: 0x060030E1 RID: 12513 RVA: 0x000DDD44 File Offset: 0x000DBF44
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static bool SafeWaitForMutexOnce(Mutex mutexIn, ref Mutex mutexOut)
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			bool flag;
			try
			{
			}
			finally
			{
				Thread.BeginCriticalRegion();
				Thread.BeginThreadAffinity();
				int num = SharedUtils.WaitForSingleObjectDontCallThis(mutexIn.SafeWaitHandle, 500);
				if (num != 0 && num != 128)
				{
					flag = num == 258;
				}
				else
				{
					mutexOut = mutexIn;
					flag = true;
				}
				if (mutexOut == null)
				{
					Thread.EndThreadAffinity();
					Thread.EndCriticalRegion();
				}
			}
			return flag;
		}

		// Token: 0x060030E2 RID: 12514
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("kernel32.dll", EntryPoint = "WaitForSingleObject", ExactSpelling = true, SetLastError = true)]
		private static extern int WaitForSingleObjectDontCallThis(SafeWaitHandle handle, int timeout);

		// Token: 0x060030E3 RID: 12515 RVA: 0x000DDDB8 File Offset: 0x000DBFB8
		internal static string GetLatestBuildDllDirectory(string machineName)
		{
			string text = "";
			RegistryKey registryKey = null;
			RegistryKey registryKey2 = null;
			RegistryPermission registryPermission = new RegistryPermission(PermissionState.Unrestricted);
			registryPermission.Assert();
			try
			{
				if (machineName.Equals("."))
				{
					return SharedUtils.GetLocalBuildDirectory();
				}
				registryKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, machineName);
				if (registryKey == null)
				{
					throw new InvalidOperationException(SR.GetString("RegKeyMissingShort", new object[] { "HKEY_LOCAL_MACHINE", machineName }));
				}
				registryKey2 = registryKey.OpenSubKey("SOFTWARE\\Microsoft\\.NETFramework");
				if (registryKey2 != null)
				{
					string text2 = (string)registryKey2.GetValue("InstallRoot");
					if (text2 != null && text2 != string.Empty)
					{
						string text3 = "v" + Environment.Version.Major.ToString() + "." + Environment.Version.Minor.ToString();
						RegistryKey registryKey3 = registryKey2.OpenSubKey("policy");
						string text4 = null;
						if (registryKey3 != null)
						{
							try
							{
								RegistryKey registryKey4 = registryKey3.OpenSubKey(text3);
								if (registryKey4 != null)
								{
									try
									{
										text4 = text3 + "." + SharedUtils.GetLargestBuildNumberFromKey(registryKey4).ToString();
										goto IL_284;
									}
									finally
									{
										registryKey4.Close();
									}
								}
								string[] subKeyNames = registryKey3.GetSubKeyNames();
								int[] array = new int[] { -1, -1, -1 };
								foreach (string text5 in subKeyNames)
								{
									if (text5.Length > 1 && text5[0] == 'v' && text5.Contains("."))
									{
										int[] array2 = new int[] { -1, -1, -1 };
										string[] array3 = text5.Substring(1).Split(new char[] { '.' });
										if (array3.Length == 2 && int.TryParse(array3[0], out array2[0]) && int.TryParse(array3[1], out array2[1]))
										{
											RegistryKey registryKey5 = registryKey3.OpenSubKey(text5);
											if (registryKey5 != null)
											{
												try
												{
													array2[2] = SharedUtils.GetLargestBuildNumberFromKey(registryKey5);
													if (array2[0] > array[0] || (array2[0] == array[0] && array2[1] > array[1]))
													{
														array = array2;
													}
												}
												finally
												{
													registryKey5.Close();
												}
											}
										}
									}
								}
								text4 = string.Concat(new string[]
								{
									"v",
									array[0].ToString(),
									".",
									array[1].ToString(),
									".",
									array[2].ToString()
								});
								IL_284:;
							}
							finally
							{
								registryKey3.Close();
							}
							if (text4 != null && text4 != string.Empty)
							{
								StringBuilder stringBuilder = new StringBuilder();
								stringBuilder.Append(text2);
								if (!text2.EndsWith("\\", StringComparison.Ordinal))
								{
									stringBuilder.Append("\\");
								}
								stringBuilder.Append(text4);
								text = stringBuilder.ToString();
							}
						}
					}
				}
			}
			catch
			{
			}
			finally
			{
				if (registryKey2 != null)
				{
					registryKey2.Close();
				}
				if (registryKey != null)
				{
					registryKey.Close();
				}
				CodeAccessPermission.RevertAssert();
			}
			return text;
		}

		// Token: 0x060030E4 RID: 12516 RVA: 0x000DE144 File Offset: 0x000DC344
		private static int GetLargestBuildNumberFromKey(RegistryKey rootKey)
		{
			int num = -1;
			string[] valueNames = rootKey.GetValueNames();
			for (int i = 0; i < valueNames.Length; i++)
			{
				int num2;
				if (int.TryParse(valueNames[i], out num2))
				{
					num = ((num > num2) ? num : num2);
				}
			}
			return num;
		}

		// Token: 0x060030E5 RID: 12517 RVA: 0x000DE17E File Offset: 0x000DC37E
		private static string GetLocalBuildDirectory()
		{
			return RuntimeEnvironment.GetRuntimeDirectory();
		}

		// Token: 0x040028BF RID: 10431
		internal const int UnknownEnvironment = 0;

		// Token: 0x040028C0 RID: 10432
		internal const int W2kEnvironment = 1;

		// Token: 0x040028C1 RID: 10433
		internal const int NtEnvironment = 2;

		// Token: 0x040028C2 RID: 10434
		internal const int NonNtEnvironment = 3;

		// Token: 0x040028C3 RID: 10435
		private static volatile int environment;

		// Token: 0x040028C4 RID: 10436
		private static object s_InternalSyncObject;
	}
}
