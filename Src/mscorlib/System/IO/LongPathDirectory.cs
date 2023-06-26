using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.IO
{
	// Token: 0x020001AD RID: 429
	[ComVisible(false)]
	internal static class LongPathDirectory
	{
		// Token: 0x06001AED RID: 6893 RVA: 0x0005AAB4 File Offset: 0x00058CB4
		[SecurityCritical]
		internal static void CreateDirectory(string path)
		{
			string text = LongPath.NormalizePath(path);
			string demandDir = LongPathDirectory.GetDemandDir(text, true);
			FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, demandDir, false, false);
			LongPathDirectory.InternalCreateDirectory(text, path, null);
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x0005AAE4 File Offset: 0x00058CE4
		[SecurityCritical]
		private unsafe static void InternalCreateDirectory(string fullPath, string path, object dirSecurityObj)
		{
			DirectorySecurity directorySecurity = (DirectorySecurity)dirSecurityObj;
			int num = fullPath.Length;
			if (num >= 2 && Path.IsDirectorySeparator(fullPath[num - 1]))
			{
				num--;
			}
			int rootLength = LongPath.GetRootLength(fullPath);
			if (num == 2 && Path.IsDirectorySeparator(fullPath[1]))
			{
				throw new IOException(Environment.GetResourceString("IO.IO_CannotCreateDirectory", new object[] { path }));
			}
			List<string> list = new List<string>();
			bool flag = false;
			if (num > rootLength)
			{
				int num2 = num - 1;
				while (num2 >= rootLength && !flag)
				{
					string text = fullPath.Substring(0, num2 + 1);
					if (!LongPathDirectory.InternalExists(text))
					{
						list.Add(text);
					}
					else
					{
						flag = true;
					}
					while (num2 > rootLength && fullPath[num2] != Path.DirectorySeparatorChar && fullPath[num2] != Path.AltDirectorySeparatorChar)
					{
						num2--;
					}
					num2--;
				}
			}
			int count = list.Count;
			if (list.Count != 0 && !CodeAccessSecurityEngine.QuickCheckForAllDemands())
			{
				string[] array = new string[list.Count];
				list.CopyTo(array, 0);
				for (int i = 0; i < array.Length; i++)
				{
					string[] array2 = array;
					int num3 = i;
					array2[num3] += "\\.";
				}
				AccessControlActions accessControlActions = ((directorySecurity == null) ? AccessControlActions.None : AccessControlActions.Change);
				FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, accessControlActions, array, false, false);
			}
			Win32Native.SECURITY_ATTRIBUTES security_ATTRIBUTES = null;
			if (directorySecurity != null)
			{
				security_ATTRIBUTES = new Win32Native.SECURITY_ATTRIBUTES();
				security_ATTRIBUTES.nLength = Marshal.SizeOf<Win32Native.SECURITY_ATTRIBUTES>(security_ATTRIBUTES);
				byte[] securityDescriptorBinaryForm = directorySecurity.GetSecurityDescriptorBinaryForm();
				byte* ptr = stackalloc byte[(UIntPtr)securityDescriptorBinaryForm.Length];
				Buffer.Memcpy(ptr, 0, securityDescriptorBinaryForm, 0, securityDescriptorBinaryForm.Length);
				security_ATTRIBUTES.pSecurityDescriptor = ptr;
			}
			bool flag2 = true;
			int num4 = 0;
			string text2 = path;
			while (list.Count > 0)
			{
				string text3 = list[list.Count - 1];
				list.RemoveAt(list.Count - 1);
				if (text3.Length >= 32767)
				{
					throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
				}
				flag2 = Win32Native.CreateDirectory(PathInternal.EnsureExtendedPrefix(text3), security_ATTRIBUTES);
				if (!flag2 && num4 == 0)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (lastWin32Error != 183)
					{
						num4 = lastWin32Error;
					}
					else if (LongPathFile.InternalExists(text3) || (!LongPathDirectory.InternalExists(text3, out lastWin32Error) && lastWin32Error == 5))
					{
						num4 = lastWin32Error;
						try
						{
							FileIOPermission.QuickDemand(FileIOPermissionAccess.PathDiscovery, LongPathDirectory.GetDemandDir(text3, true), false, false);
							text2 = text3;
						}
						catch (SecurityException)
						{
						}
					}
				}
			}
			if (count == 0 && !flag)
			{
				string text4 = LongPathDirectory.InternalGetDirectoryRoot(fullPath);
				if (!LongPathDirectory.InternalExists(text4))
				{
					__Error.WinIOError(3, LongPathDirectory.InternalGetDirectoryRoot(path));
				}
				return;
			}
			if (!flag2 && num4 != 0)
			{
				__Error.WinIOError(num4, text2);
			}
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x0005AD6C File Offset: 0x00058F6C
		[SecurityCritical]
		internal static void Move(string sourceDirName, string destDirName)
		{
			string text = LongPath.NormalizePath(sourceDirName);
			string demandDir = LongPathDirectory.GetDemandDir(text, false);
			if (demandDir.Length >= 32767)
			{
				throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
			}
			string text2 = LongPath.NormalizePath(destDirName);
			string demandDir2 = LongPathDirectory.GetDemandDir(text2, false);
			if (demandDir2.Length >= 32767)
			{
				throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
			}
			FileIOPermission.QuickDemand(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, demandDir, false, false);
			FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, demandDir2, false, false);
			if (string.Compare(demandDir, demandDir2, StringComparison.OrdinalIgnoreCase) == 0)
			{
				throw new IOException(Environment.GetResourceString("IO.IO_SourceDestMustBeDifferent"));
			}
			string pathRoot = LongPath.GetPathRoot(demandDir);
			string pathRoot2 = LongPath.GetPathRoot(demandDir2);
			if (string.Compare(pathRoot, pathRoot2, StringComparison.OrdinalIgnoreCase) != 0)
			{
				throw new IOException(Environment.GetResourceString("IO.IO_SourceDestMustHaveSameRoot"));
			}
			string text3 = PathInternal.EnsureExtendedPrefix(sourceDirName);
			string text4 = PathInternal.EnsureExtendedPrefix(destDirName);
			if (!Win32Native.MoveFile(text3, text4))
			{
				int num = Marshal.GetLastWin32Error();
				if (num == 2)
				{
					num = 3;
					__Error.WinIOError(num, text);
				}
				if (num == 5)
				{
					throw new IOException(Environment.GetResourceString("UnauthorizedAccess_IODenied_Path", new object[] { sourceDirName }), Win32Native.MakeHRFromErrorCode(num));
				}
				__Error.WinIOError(num, string.Empty);
			}
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x0005AE90 File Offset: 0x00059090
		[SecurityCritical]
		internal static void Delete(string path, bool recursive)
		{
			string text = LongPath.NormalizePath(path);
			LongPathDirectory.InternalDelete(text, path, recursive);
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x0005AEAC File Offset: 0x000590AC
		[SecurityCritical]
		private static void InternalDelete(string fullPath, string userPath, bool recursive)
		{
			string demandDir = LongPathDirectory.GetDemandDir(fullPath, !recursive);
			FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, demandDir, false, false);
			string text = Path.AddLongPathPrefix(fullPath);
			Win32Native.WIN32_FILE_ATTRIBUTE_DATA win32_FILE_ATTRIBUTE_DATA = default(Win32Native.WIN32_FILE_ATTRIBUTE_DATA);
			int num = File.FillAttributeInfo(text, ref win32_FILE_ATTRIBUTE_DATA, false, true);
			if (num != 0)
			{
				if (num == 2)
				{
					num = 3;
				}
				__Error.WinIOError(num, fullPath);
			}
			if ((win32_FILE_ATTRIBUTE_DATA.fileAttributes & 1024) != 0)
			{
				recursive = false;
			}
			LongPathDirectory.DeleteHelper(text, userPath, recursive, true);
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x0005AF14 File Offset: 0x00059114
		[SecurityCritical]
		private static void DeleteHelper(string fullPath, string userPath, bool recursive, bool throwOnTopLevelDirectoryNotFound)
		{
			Exception ex = null;
			if (recursive)
			{
				Win32Native.WIN32_FIND_DATA win32_FIND_DATA = default(Win32Native.WIN32_FIND_DATA);
				string text;
				if (fullPath.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal))
				{
					text = fullPath + "*";
				}
				else
				{
					text = fullPath + Path.DirectorySeparatorChar.ToString() + "*";
				}
				int num;
				using (SafeFindHandle safeFindHandle = Win32Native.FindFirstFile(text, ref win32_FIND_DATA))
				{
					if (safeFindHandle.IsInvalid)
					{
						num = Marshal.GetLastWin32Error();
						__Error.WinIOError(num, userPath);
					}
					for (;;)
					{
						bool flag = (win32_FIND_DATA.dwFileAttributes & 16) != 0;
						if (!flag)
						{
							goto IL_180;
						}
						if (!win32_FIND_DATA.IsRelativeDirectory)
						{
							bool flag2 = (win32_FIND_DATA.dwFileAttributes & 1024) == 0;
							if (flag2)
							{
								string text2 = LongPath.InternalCombine(fullPath, win32_FIND_DATA.cFileName);
								string text3 = LongPath.InternalCombine(userPath, win32_FIND_DATA.cFileName);
								try
								{
									LongPathDirectory.DeleteHelper(text2, text3, recursive, false);
									goto IL_1BD;
								}
								catch (Exception ex2)
								{
									if (ex == null)
									{
										ex = ex2;
									}
									goto IL_1BD;
								}
							}
							if (win32_FIND_DATA.dwReserved0 == -1610612733)
							{
								string text4 = LongPath.InternalCombine(fullPath, win32_FIND_DATA.cFileName + Path.DirectorySeparatorChar.ToString());
								if (!Win32Native.DeleteVolumeMountPoint(text4))
								{
									num = Marshal.GetLastWin32Error();
									if (num != 3)
									{
										try
										{
											__Error.WinIOError(num, win32_FIND_DATA.cFileName);
										}
										catch (Exception ex3)
										{
											if (ex == null)
											{
												ex = ex3;
											}
										}
									}
								}
							}
							string text5 = LongPath.InternalCombine(fullPath, win32_FIND_DATA.cFileName);
							if (!Win32Native.RemoveDirectory(text5))
							{
								num = Marshal.GetLastWin32Error();
								if (num != 3)
								{
									try
									{
										__Error.WinIOError(num, win32_FIND_DATA.cFileName);
										goto IL_1BD;
									}
									catch (Exception ex4)
									{
										if (ex == null)
										{
											ex = ex4;
										}
										goto IL_1BD;
									}
									goto IL_180;
								}
							}
						}
						IL_1BD:
						if (!Win32Native.FindNextFile(safeFindHandle, ref win32_FIND_DATA))
						{
							break;
						}
						continue;
						IL_180:
						string text6 = LongPath.InternalCombine(fullPath, win32_FIND_DATA.cFileName);
						if (Win32Native.DeleteFile(text6))
						{
							goto IL_1BD;
						}
						num = Marshal.GetLastWin32Error();
						if (num != 2)
						{
							try
							{
								__Error.WinIOError(num, win32_FIND_DATA.cFileName);
							}
							catch (Exception ex5)
							{
								if (ex == null)
								{
									ex = ex5;
								}
							}
							goto IL_1BD;
						}
						goto IL_1BD;
					}
					num = Marshal.GetLastWin32Error();
				}
				if (ex != null)
				{
					throw ex;
				}
				if (num != 0 && num != 18)
				{
					__Error.WinIOError(num, userPath);
				}
			}
			if (!Win32Native.RemoveDirectory(fullPath))
			{
				int num = Marshal.GetLastWin32Error();
				if (num == 2)
				{
					num = 3;
				}
				if (num == 5)
				{
					throw new IOException(Environment.GetResourceString("UnauthorizedAccess_IODenied_Path", new object[] { userPath }));
				}
				if (num == 3 && !throwOnTopLevelDirectoryNotFound)
				{
					return;
				}
				__Error.WinIOError(num, userPath);
			}
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x0005B1D4 File Offset: 0x000593D4
		[SecurityCritical]
		internal static bool Exists(string path)
		{
			try
			{
				if (path == null)
				{
					return false;
				}
				if (path.Length == 0)
				{
					return false;
				}
				string text = LongPath.NormalizePath(path);
				string demandDir = LongPathDirectory.GetDemandDir(text, true);
				FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, demandDir, false, false);
				return LongPathDirectory.InternalExists(text);
			}
			catch (ArgumentException)
			{
			}
			catch (NotSupportedException)
			{
			}
			catch (SecurityException)
			{
			}
			catch (IOException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
			return false;
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x0005B268 File Offset: 0x00059468
		[SecurityCritical]
		internal static bool InternalExists(string path)
		{
			int num = 0;
			return LongPathDirectory.InternalExists(path, out num);
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x0005B280 File Offset: 0x00059480
		[SecurityCritical]
		internal static bool InternalExists(string path, out int lastError)
		{
			string text = Path.AddLongPathPrefix(path);
			return Directory.InternalExists(text, out lastError);
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x0005B29C File Offset: 0x0005949C
		private static string GetDemandDir(string fullPath, bool thisDirOnly)
		{
			fullPath = Path.RemoveLongPathPrefix(fullPath);
			string text;
			if (thisDirOnly)
			{
				if (fullPath.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal) || fullPath.EndsWith(Path.AltDirectorySeparatorChar.ToString(), StringComparison.Ordinal))
				{
					text = fullPath + ".";
				}
				else
				{
					text = fullPath + Path.DirectorySeparatorChar.ToString() + ".";
				}
			}
			else if (!fullPath.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal) && !fullPath.EndsWith(Path.AltDirectorySeparatorChar.ToString(), StringComparison.Ordinal))
			{
				text = fullPath + Path.DirectorySeparatorChar.ToString();
			}
			else
			{
				text = fullPath;
			}
			return text;
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x0005B34E File Offset: 0x0005954E
		private static string InternalGetDirectoryRoot(string path)
		{
			if (path == null)
			{
				return null;
			}
			return path.Substring(0, LongPath.GetRootLength(path));
		}
	}
}
