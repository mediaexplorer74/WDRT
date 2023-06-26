using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32;

namespace System.IO
{
	/// <summary>Performs operations on <see cref="T:System.String" /> instances that contain file or directory path information. These operations are performed in a cross-platform manner.</summary>
	// Token: 0x0200019A RID: 410
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public static class Path
	{
		/// <summary>Changes the extension of a path string.</summary>
		/// <param name="path">The path information to modify. The path cannot contain any of the characters defined in <see cref="M:System.IO.Path.GetInvalidPathChars" />.</param>
		/// <param name="extension">The new extension (with or without a leading period). Specify <see langword="null" /> to remove an existing extension from <paramref name="path" />.</param>
		/// <returns>The modified path information.  
		///  On Windows-based desktop platforms, if <paramref name="path" /> is <see langword="null" /> or an empty string (""), the path information is returned unmodified. If <paramref name="extension" /> is <see langword="null" />, the returned string contains the specified path with its extension removed. If <paramref name="path" /> has no extension, and <paramref name="extension" /> is not <see langword="null" />, the returned path string contains <paramref name="extension" /> appended to the end of <paramref name="path" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> contains one or more of the invalid characters defined in <see cref="M:System.IO.Path.GetInvalidPathChars" />.</exception>
		// Token: 0x06001904 RID: 6404 RVA: 0x0005247C File Offset: 0x0005067C
		[__DynamicallyInvokable]
		public static string ChangeExtension(string path, string extension)
		{
			if (path != null)
			{
				Path.CheckInvalidPathChars(path, false);
				string text = path;
				int num = path.Length;
				while (--num >= 0)
				{
					char c = path[num];
					if (c == '.')
					{
						text = path.Substring(0, num);
						break;
					}
					if (c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar || c == Path.VolumeSeparatorChar)
					{
						break;
					}
				}
				if (extension != null && path.Length != 0)
				{
					if (extension.Length == 0 || extension[0] != '.')
					{
						text += ".";
					}
					text += extension;
				}
				return text;
			}
			return null;
		}

		/// <summary>Returns the directory information for the specified path string.</summary>
		/// <param name="path">The path of a file or directory.</param>
		/// <returns>Directory information for <paramref name="path" />, or <see langword="null" /> if <paramref name="path" /> denotes a root directory or is null. Returns <see cref="F:System.String.Empty" /> if <paramref name="path" /> does not contain directory information.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> parameter contains invalid characters, is empty, or contains only white spaces.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.IO.IOException" />, instead.  
		///
		///
		///
		///
		///  The <paramref name="path" /> parameter is longer than the system-defined maximum length.</exception>
		// Token: 0x06001905 RID: 6405 RVA: 0x0005250B File Offset: 0x0005070B
		[__DynamicallyInvokable]
		public static string GetDirectoryName(string path)
		{
			return Path.InternalGetDirectoryName(path);
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x00052514 File Offset: 0x00050714
		[SecuritySafeCritical]
		private static string InternalGetDirectoryName(string path)
		{
			if (path != null)
			{
				Path.CheckInvalidPathChars(path, false);
				string text = Path.NormalizePath(path, false, AppContextSwitches.UseLegacyPathHandling);
				if (path.Length > 0 && !CodeAccessSecurityEngine.QuickCheckForAllDemands())
				{
					try
					{
						string text2 = Path.RemoveLongPathPrefix(path);
						int num = 0;
						while (num < text2.Length && text2[num] != '?' && text2[num] != '*')
						{
							num++;
						}
						if (num > 0)
						{
							Path.GetFullPath(text2.Substring(0, num));
						}
					}
					catch (SecurityException)
					{
						if (path.IndexOf("~", StringComparison.Ordinal) != -1)
						{
							text = Path.NormalizePath(path, false, false);
						}
					}
					catch (PathTooLongException)
					{
					}
					catch (NotSupportedException)
					{
					}
					catch (IOException)
					{
					}
					catch (ArgumentException)
					{
					}
				}
				path = text;
				int rootLength = Path.GetRootLength(path);
				int num2 = path.Length;
				if (num2 > rootLength)
				{
					num2 = path.Length;
					if (num2 == rootLength)
					{
						return null;
					}
					while (num2 > rootLength && path[--num2] != Path.DirectorySeparatorChar && path[num2] != Path.AltDirectorySeparatorChar)
					{
					}
					return path.Substring(0, num2);
				}
			}
			return null;
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x00052650 File Offset: 0x00050850
		internal static int GetRootLength(string path)
		{
			Path.CheckInvalidPathChars(path, false);
			if (AppContextSwitches.UseLegacyPathHandling)
			{
				return Path.LegacyGetRootLength(path);
			}
			return PathInternal.GetRootLength(path);
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x00052670 File Offset: 0x00050870
		private static int LegacyGetRootLength(string path)
		{
			int i = 0;
			int length = path.Length;
			if (length >= 1 && Path.IsDirectorySeparator(path[0]))
			{
				i = 1;
				if (length >= 2 && Path.IsDirectorySeparator(path[1]))
				{
					i = 2;
					int num = 2;
					while (i < length)
					{
						if ((path[i] == Path.DirectorySeparatorChar || path[i] == Path.AltDirectorySeparatorChar) && --num <= 0)
						{
							break;
						}
						i++;
					}
				}
			}
			else if (length >= 2 && path[1] == Path.VolumeSeparatorChar)
			{
				i = 2;
				if (length >= 3 && Path.IsDirectorySeparator(path[2]))
				{
					i++;
				}
			}
			return i;
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x0005270B File Offset: 0x0005090B
		internal static bool IsDirectorySeparator(char c)
		{
			return c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar;
		}

		/// <summary>Gets an array containing the characters that are not allowed in path names.</summary>
		/// <returns>An array containing the characters that are not allowed in path names.</returns>
		// Token: 0x0600190A RID: 6410 RVA: 0x0005271F File Offset: 0x0005091F
		[__DynamicallyInvokable]
		public static char[] GetInvalidPathChars()
		{
			return (char[])Path.RealInvalidPathChars.Clone();
		}

		/// <summary>Gets an array containing the characters that are not allowed in file names.</summary>
		/// <returns>An array containing the characters that are not allowed in file names.</returns>
		// Token: 0x0600190B RID: 6411 RVA: 0x00052730 File Offset: 0x00050930
		[__DynamicallyInvokable]
		public static char[] GetInvalidFileNameChars()
		{
			return (char[])Path.InvalidFileNameChars.Clone();
		}

		/// <summary>Returns the extension of the specified path string.</summary>
		/// <param name="path">The path string from which to get the extension.</param>
		/// <returns>The extension of the specified path (including the period "."), or <see langword="null" />, or <see cref="F:System.String.Empty" />. If <paramref name="path" /> is <see langword="null" />, <see cref="M:System.IO.Path.GetExtension(System.String)" /> returns <see langword="null" />. If <paramref name="path" /> does not have extension information, <see cref="M:System.IO.Path.GetExtension(System.String)" /> returns <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> contains one or more of the invalid characters defined in <see cref="M:System.IO.Path.GetInvalidPathChars" />.</exception>
		// Token: 0x0600190C RID: 6412 RVA: 0x00052744 File Offset: 0x00050944
		[__DynamicallyInvokable]
		public static string GetExtension(string path)
		{
			if (path == null)
			{
				return null;
			}
			Path.CheckInvalidPathChars(path, false);
			int length = path.Length;
			int num = length;
			while (--num >= 0)
			{
				char c = path[num];
				if (c == '.')
				{
					if (num != length - 1)
					{
						return path.Substring(num, length - num);
					}
					return string.Empty;
				}
				else if (c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar || c == Path.VolumeSeparatorChar)
				{
					break;
				}
			}
			return string.Empty;
		}

		/// <summary>Returns the absolute path for the specified path string.</summary>
		/// <param name="path">The file or directory for which to obtain absolute path information.</param>
		/// <returns>The fully qualified location of <paramref name="path" />, such as "C:\MyFile.txt".</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more of the invalid characters defined in <see cref="M:System.IO.Path.GetInvalidPathChars" />.  
		/// -or-  
		/// The system could not retrieve the absolute path.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permissions.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> contains a colon (":") that is not part of a volume identifier (for example, "c:\").</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		// Token: 0x0600190D RID: 6413 RVA: 0x000527B4 File Offset: 0x000509B4
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static string GetFullPath(string path)
		{
			string fullPathInternal = Path.GetFullPathInternal(path);
			FileIOPermission.QuickDemand(FileIOPermissionAccess.PathDiscovery, fullPathInternal, false, false);
			return fullPathInternal;
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x000527D4 File Offset: 0x000509D4
		[SecurityCritical]
		internal static string UnsafeGetFullPath(string path)
		{
			string fullPathInternal = Path.GetFullPathInternal(path);
			FileIOPermission.QuickDemand(FileIOPermissionAccess.PathDiscovery, fullPathInternal, false, false);
			return fullPathInternal;
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x000527F4 File Offset: 0x000509F4
		internal static string GetFullPathInternal(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			return Path.NormalizePath(path, true);
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x00052818 File Offset: 0x00050A18
		[SecuritySafeCritical]
		internal static string NormalizePath(string path, bool fullCheck)
		{
			return Path.NormalizePath(path, fullCheck, AppContextSwitches.BlockLongPaths ? 260 : 32767);
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x00052834 File Offset: 0x00050A34
		[SecuritySafeCritical]
		internal static string NormalizePath(string path, bool fullCheck, bool expandShortPaths)
		{
			return Path.NormalizePath(path, fullCheck, Path.MaxPath, expandShortPaths);
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x00052843 File Offset: 0x00050A43
		[SecuritySafeCritical]
		internal static string NormalizePath(string path, bool fullCheck, int maxPathLength)
		{
			return Path.NormalizePath(path, fullCheck, maxPathLength, true);
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x00052850 File Offset: 0x00050A50
		[SecuritySafeCritical]
		internal static string NormalizePath(string path, bool fullCheck, int maxPathLength, bool expandShortPaths)
		{
			if (AppContextSwitches.UseLegacyPathHandling)
			{
				return Path.LegacyNormalizePath(path, fullCheck, maxPathLength, expandShortPaths);
			}
			if (PathInternal.IsExtended(path))
			{
				return path;
			}
			string text;
			if (!fullCheck)
			{
				text = Path.NewNormalizePathLimitedChecks(path, maxPathLength, expandShortPaths);
			}
			else
			{
				text = Path.NewNormalizePath(path, maxPathLength, true);
			}
			if (string.IsNullOrWhiteSpace(text))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
			}
			return text;
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x000528AC File Offset: 0x00050AAC
		[SecuritySafeCritical]
		private static string NewNormalizePathLimitedChecks(string path, int maxPathLength, bool expandShortPaths)
		{
			string text = PathInternal.NormalizeDirectorySeparators(path);
			if (PathInternal.IsPathTooLong(text) || PathInternal.AreSegmentsTooLong(text))
			{
				throw new PathTooLongException();
			}
			if (expandShortPaths && text.IndexOf('~') != -1)
			{
				try
				{
					return LongPathHelper.GetLongPathName(text);
				}
				catch
				{
				}
				return text;
			}
			return text;
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x00052904 File Offset: 0x00050B04
		[SecuritySafeCritical]
		private static string NewNormalizePath(string path, int maxPathLength, bool expandShortPaths)
		{
			if (path.IndexOf('\0') != -1)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPathChars"));
			}
			if (string.IsNullOrWhiteSpace(path))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
			}
			return LongPathHelper.Normalize(path, (uint)maxPathLength, !PathInternal.IsDevice(path), expandShortPaths);
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x00052954 File Offset: 0x00050B54
		[SecurityCritical]
		internal unsafe static string LegacyNormalizePath(string path, bool fullCheck, int maxPathLength, bool expandShortPaths)
		{
			if (fullCheck)
			{
				path = path.TrimEnd(Path.TrimEndChars);
				if (PathInternal.AnyPathHasIllegalCharacters(path, false))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPathChars"));
				}
			}
			int i = 0;
			PathHelper pathHelper;
			if (path.Length + 1 <= Path.MaxPath)
			{
				checked
				{
					char* ptr = stackalloc char[unchecked((UIntPtr)Path.MaxPath) * 2];
					pathHelper = new PathHelper(ptr, Path.MaxPath);
				}
			}
			else
			{
				pathHelper = new PathHelper(path.Length + Path.MaxPath, maxPathLength);
			}
			uint num = 0U;
			uint num2 = 0U;
			bool flag = false;
			uint num3 = 0U;
			int num4 = -1;
			bool flag2 = false;
			bool flag3 = true;
			int num5 = 0;
			bool flag4 = false;
			if (path.Length > 0 && (path[0] == Path.DirectorySeparatorChar || path[0] == Path.AltDirectorySeparatorChar))
			{
				pathHelper.Append('\\');
				i++;
				num4 = 0;
			}
			while (i < path.Length)
			{
				char c = path[i];
				if (c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar)
				{
					if (num3 == 0U)
					{
						if (num2 > 0U)
						{
							int num6 = num4 + 1;
							if (path[num6] != '.')
							{
								throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
							}
							if (num2 >= 2U)
							{
								if (flag2 && num2 > 2U)
								{
									throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
								}
								if (path[num6 + 1] == '.')
								{
									int num7 = num6 + 2;
									while ((long)num7 < (long)num6 + (long)((ulong)num2))
									{
										if (path[num7] != '.')
										{
											throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
										}
										num7++;
									}
									num2 = 2U;
								}
								else
								{
									if (num2 > 1U)
									{
										throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
									}
									num2 = 1U;
								}
							}
							if (num2 == 2U)
							{
								pathHelper.Append('.');
							}
							pathHelper.Append('.');
							flag = false;
						}
						if (num > 0U && flag3 && i + 1 < path.Length && (path[i + 1] == Path.DirectorySeparatorChar || path[i + 1] == Path.AltDirectorySeparatorChar))
						{
							pathHelper.Append(Path.DirectorySeparatorChar);
						}
					}
					num2 = 0U;
					num = 0U;
					if (!flag)
					{
						flag = true;
						pathHelper.Append(Path.DirectorySeparatorChar);
					}
					num3 = 0U;
					num4 = i;
					flag2 = false;
					flag3 = false;
					if (flag4)
					{
						pathHelper.TryExpandShortFileName();
						flag4 = false;
					}
					int num8 = pathHelper.Length - 1;
					if (num8 - num5 > Path.MaxDirectoryLength)
					{
						throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
					}
					num5 = num8;
				}
				else if (c == '.')
				{
					num2 += 1U;
				}
				else if (c == ' ')
				{
					num += 1U;
				}
				else
				{
					if (c == '~' && expandShortPaths)
					{
						flag4 = true;
					}
					flag = false;
					if (flag3 && c == Path.VolumeSeparatorChar)
					{
						char c2 = ((i > 0) ? path[i - 1] : ' ');
						if (num2 != 0U || num3 < 1U || c2 == ' ')
						{
							throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
						}
						flag2 = true;
						if (num3 > 1U)
						{
							int num9 = 0;
							while (num9 < pathHelper.Length && pathHelper[num9] == ' ')
							{
								num9++;
							}
							if ((ulong)num3 - (ulong)((long)num9) == 1UL)
							{
								pathHelper.Length = 0;
								pathHelper.Append(c2);
							}
						}
						num3 = 0U;
					}
					else
					{
						num3 += 1U + num2 + num;
					}
					if (num2 > 0U || num > 0U)
					{
						int num10 = ((num4 >= 0) ? (i - num4 - 1) : i);
						if (num10 > 0)
						{
							for (int j = 0; j < num10; j++)
							{
								pathHelper.Append(path[num4 + 1 + j]);
							}
						}
						num2 = 0U;
						num = 0U;
					}
					pathHelper.Append(c);
					num4 = i;
				}
				i++;
			}
			if (pathHelper.Length - 1 - num5 > Path.MaxDirectoryLength)
			{
				throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
			}
			if (num3 == 0U && num2 > 0U)
			{
				int num11 = num4 + 1;
				if (path[num11] != '.')
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
				}
				if (num2 >= 2U)
				{
					if (flag2 && num2 > 2U)
					{
						throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
					}
					if (path[num11 + 1] == '.')
					{
						int num12 = num11 + 2;
						while ((long)num12 < (long)num11 + (long)((ulong)num2))
						{
							if (path[num12] != '.')
							{
								throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
							}
							num12++;
						}
						num2 = 2U;
					}
					else
					{
						if (num2 > 1U)
						{
							throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
						}
						num2 = 1U;
					}
				}
				if (num2 == 2U)
				{
					pathHelper.Append('.');
				}
				pathHelper.Append('.');
			}
			if (pathHelper.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
			}
			if (fullCheck && (pathHelper.OrdinalStartsWith("http:", false) || pathHelper.OrdinalStartsWith("file:", false)))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_PathUriFormatNotSupported"));
			}
			if (flag4)
			{
				pathHelper.TryExpandShortFileName();
			}
			int num13 = 1;
			if (fullCheck)
			{
				num13 = pathHelper.GetFullPathName();
				flag4 = false;
				int num14 = 0;
				while (num14 < pathHelper.Length && !flag4)
				{
					if (pathHelper[num14] == '~' && expandShortPaths)
					{
						flag4 = true;
					}
					num14++;
				}
				if (flag4 && !pathHelper.TryExpandShortFileName())
				{
					int num15 = -1;
					for (int k = pathHelper.Length - 1; k >= 0; k--)
					{
						if (pathHelper[k] == Path.DirectorySeparatorChar)
						{
							num15 = k;
							break;
						}
					}
					if (num15 >= 0)
					{
						if (pathHelper.Length >= maxPathLength)
						{
							throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
						}
						int num16 = pathHelper.Length - num15 - 1;
						pathHelper.Fixup(num16, num15);
					}
				}
			}
			if (num13 != 0 && pathHelper.Length > 1 && pathHelper[0] == '\\' && pathHelper[1] == '\\')
			{
				int l;
				for (l = 2; l < num13; l++)
				{
					if (pathHelper[l] == '\\')
					{
						l++;
						break;
					}
				}
				if (l == num13)
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegalUNC"));
				}
				if (pathHelper.OrdinalStartsWith("\\\\?\\globalroot", true))
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_PathGlobalRoot"));
				}
			}
			if (pathHelper.Length >= maxPathLength)
			{
				throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
			}
			if (num13 == 0)
			{
				int num17 = Marshal.GetLastWin32Error();
				if (num17 == 0)
				{
					num17 = 161;
				}
				__Error.WinIOError(num17, path);
				return null;
			}
			string text = pathHelper.ToString();
			if (string.Equals(text, path, StringComparison.Ordinal))
			{
				text = path;
			}
			return text;
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x00052FAB File Offset: 0x000511AB
		internal static bool HasLongPathPrefix(string path)
		{
			if (AppContextSwitches.UseLegacyPathHandling)
			{
				return path.StartsWith("\\\\?\\", StringComparison.Ordinal);
			}
			return PathInternal.IsExtended(path);
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x00052FC8 File Offset: 0x000511C8
		internal static string AddLongPathPrefix(string path)
		{
			if (!AppContextSwitches.UseLegacyPathHandling)
			{
				return PathInternal.EnsureExtendedPrefix(path);
			}
			if (path.StartsWith("\\\\?\\", StringComparison.Ordinal))
			{
				return path;
			}
			if (path.StartsWith("\\\\", StringComparison.Ordinal))
			{
				return path.Insert(2, "?\\UNC\\");
			}
			return "\\\\?\\" + path;
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x00053019 File Offset: 0x00051219
		internal static string RemoveLongPathPrefix(string path)
		{
			if (!AppContextSwitches.UseLegacyPathHandling)
			{
				return PathInternal.RemoveExtendedPrefix(path);
			}
			if (!path.StartsWith("\\\\?\\", StringComparison.Ordinal))
			{
				return path;
			}
			if (path.StartsWith("\\\\?\\UNC\\", StringComparison.OrdinalIgnoreCase))
			{
				return path.Remove(2, 6);
			}
			return path.Substring(4);
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x00053057 File Offset: 0x00051257
		internal static StringBuilder RemoveLongPathPrefix(StringBuilder pathSB)
		{
			if (!AppContextSwitches.UseLegacyPathHandling)
			{
				return PathInternal.RemoveExtendedPrefix(pathSB);
			}
			if (!PathInternal.StartsWithOrdinal(pathSB, "\\\\?\\", false))
			{
				return pathSB;
			}
			if (PathInternal.StartsWithOrdinal(pathSB, "\\\\?\\UNC\\", true))
			{
				return pathSB.Remove(2, 6);
			}
			return pathSB.Remove(0, 4);
		}

		/// <summary>Returns the file name and extension of the specified path string.</summary>
		/// <param name="path">The path string from which to obtain the file name and extension.</param>
		/// <returns>The characters after the last directory character in <paramref name="path" />. If the last character of <paramref name="path" /> is a directory or volume separator character, this method returns <see cref="F:System.String.Empty" />. If <paramref name="path" /> is <see langword="null" />, this method returns <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> contains one or more of the invalid characters defined in <see cref="M:System.IO.Path.GetInvalidPathChars" />.</exception>
		// Token: 0x0600191B RID: 6427 RVA: 0x00053098 File Offset: 0x00051298
		[__DynamicallyInvokable]
		public static string GetFileName(string path)
		{
			if (path != null)
			{
				Path.CheckInvalidPathChars(path, false);
				int length = path.Length;
				int num = length;
				while (--num >= 0)
				{
					char c = path[num];
					if (c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar || c == Path.VolumeSeparatorChar)
					{
						return path.Substring(num + 1, length - num - 1);
					}
				}
			}
			return path;
		}

		/// <summary>Returns the file name of the specified path string without the extension.</summary>
		/// <param name="path">The path of the file.</param>
		/// <returns>The string returned by <see cref="M:System.IO.Path.GetFileName(System.String)" />, minus the last period (.) and all characters following it.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> contains one or more of the invalid characters defined in <see cref="M:System.IO.Path.GetInvalidPathChars" />.</exception>
		// Token: 0x0600191C RID: 6428 RVA: 0x000530F4 File Offset: 0x000512F4
		[__DynamicallyInvokable]
		public static string GetFileNameWithoutExtension(string path)
		{
			path = Path.GetFileName(path);
			if (path == null)
			{
				return null;
			}
			int num;
			if ((num = path.LastIndexOf('.')) == -1)
			{
				return path;
			}
			return path.Substring(0, num);
		}

		/// <summary>Gets the root directory information of the specified path.</summary>
		/// <param name="path">The path from which to obtain root directory information.</param>
		/// <returns>The root directory of <paramref name="path" />, or <see langword="null" /> if <paramref name="path" /> is <see langword="null" />, or an empty string if <paramref name="path" /> does not contain root directory information.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> contains one or more of the invalid characters defined in <see cref="M:System.IO.Path.GetInvalidPathChars" />.  
		/// -or-  
		/// <see cref="F:System.String.Empty" /> was passed to <paramref name="path" />.</exception>
		// Token: 0x0600191D RID: 6429 RVA: 0x00053125 File Offset: 0x00051325
		[__DynamicallyInvokable]
		public static string GetPathRoot(string path)
		{
			if (path == null)
			{
				return null;
			}
			path = Path.NormalizePath(path, false, false);
			return path.Substring(0, Path.GetRootLength(path));
		}

		/// <summary>Returns the path of the current user's temporary folder.</summary>
		/// <returns>The path to the temporary folder, ending with a backslash.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permissions.</exception>
		// Token: 0x0600191E RID: 6430 RVA: 0x00053144 File Offset: 0x00051344
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static string GetTempPath()
		{
			new EnvironmentPermission(PermissionState.Unrestricted).Demand();
			StringBuilder stringBuilder = new StringBuilder(260);
			uint tempPath = Win32Native.GetTempPath(260, stringBuilder);
			string text = stringBuilder.ToString();
			if (tempPath == 0U)
			{
				__Error.WinIOError();
			}
			return Path.GetFullPathInternal(text);
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x0005318A File Offset: 0x0005138A
		internal static bool IsRelative(string path)
		{
			return PathInternal.IsPartiallyQualified(path);
		}

		/// <summary>Returns a random folder name or file name.</summary>
		/// <returns>A random folder name or file name.</returns>
		// Token: 0x06001920 RID: 6432 RVA: 0x00053194 File Offset: 0x00051394
		[__DynamicallyInvokable]
		public static string GetRandomFileName()
		{
			byte[] array = new byte[10];
			RNGCryptoServiceProvider rngcryptoServiceProvider = null;
			string text;
			try
			{
				rngcryptoServiceProvider = new RNGCryptoServiceProvider();
				rngcryptoServiceProvider.GetBytes(array);
				char[] array2 = Path.ToBase32StringSuitableForDirName(array).ToCharArray();
				array2[8] = '.';
				text = new string(array2, 0, 12);
			}
			finally
			{
				if (rngcryptoServiceProvider != null)
				{
					rngcryptoServiceProvider.Dispose();
				}
			}
			return text;
		}

		/// <summary>Creates a uniquely named, zero-byte temporary file on disk and returns the full path of that file.</summary>
		/// <returns>The full path of the temporary file.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs, such as no unique temporary file name is available.  
		/// -or-
		///  This method was unable to create a temporary file.</exception>
		// Token: 0x06001921 RID: 6433 RVA: 0x000531F0 File Offset: 0x000513F0
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static string GetTempFileName()
		{
			return Path.InternalGetTempFileName(true);
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x000531F8 File Offset: 0x000513F8
		[SecurityCritical]
		internal static string UnsafeGetTempFileName()
		{
			return Path.InternalGetTempFileName(false);
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x00053200 File Offset: 0x00051400
		[SecurityCritical]
		private static string InternalGetTempFileName(bool checkHost)
		{
			string tempPath = Path.GetTempPath();
			FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, tempPath, false, true);
			StringBuilder stringBuilder = new StringBuilder(260);
			if (Win32Native.GetTempFileName(tempPath, "tmp", 0U, stringBuilder) == 0U)
			{
				__Error.WinIOError();
			}
			return stringBuilder.ToString();
		}

		/// <summary>Determines whether a path includes a file name extension.</summary>
		/// <param name="path">The path to search for an extension.</param>
		/// <returns>
		///   <see langword="true" /> if the characters that follow the last directory separator (\\ or /) or volume separator (:) in the path include a period (.) followed by one or more characters; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> contains one or more of the invalid characters defined in <see cref="M:System.IO.Path.GetInvalidPathChars" />.</exception>
		// Token: 0x06001924 RID: 6436 RVA: 0x00053244 File Offset: 0x00051444
		[__DynamicallyInvokable]
		public static bool HasExtension(string path)
		{
			if (path != null)
			{
				Path.CheckInvalidPathChars(path, false);
				int num = path.Length;
				while (--num >= 0)
				{
					char c = path[num];
					if (c == '.')
					{
						return num != path.Length - 1;
					}
					if (c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar || c == Path.VolumeSeparatorChar)
					{
						break;
					}
				}
			}
			return false;
		}

		/// <summary>Gets a value indicating whether the specified path string contains a root.</summary>
		/// <param name="path">The path to test.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="path" /> contains a root; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> contains one or more of the invalid characters defined in <see cref="M:System.IO.Path.GetInvalidPathChars" />.</exception>
		// Token: 0x06001925 RID: 6437 RVA: 0x000532A4 File Offset: 0x000514A4
		[__DynamicallyInvokable]
		public static bool IsPathRooted(string path)
		{
			if (path != null)
			{
				Path.CheckInvalidPathChars(path, false);
				int length = path.Length;
				if ((length >= 1 && (path[0] == Path.DirectorySeparatorChar || path[0] == Path.AltDirectorySeparatorChar)) || (length >= 2 && path[1] == Path.VolumeSeparatorChar))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Combines two strings into a path.</summary>
		/// <param name="path1">The first path to combine.</param>
		/// <param name="path2">The second path to combine.</param>
		/// <returns>The combined paths. If one of the specified paths is a zero-length string, this method returns the other path. If <paramref name="path2" /> contains an absolute path, this method returns <paramref name="path2" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path1" /> or <paramref name="path2" /> contains one or more of the invalid characters defined in <see cref="M:System.IO.Path.GetInvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path1" /> or <paramref name="path2" /> is <see langword="null" />.</exception>
		// Token: 0x06001926 RID: 6438 RVA: 0x000532F7 File Offset: 0x000514F7
		[__DynamicallyInvokable]
		public static string Combine(string path1, string path2)
		{
			if (path1 == null || path2 == null)
			{
				throw new ArgumentNullException((path1 == null) ? "path1" : "path2");
			}
			Path.CheckInvalidPathChars(path1, false);
			Path.CheckInvalidPathChars(path2, false);
			return Path.CombineNoChecks(path1, path2);
		}

		/// <summary>Combines three strings into a path.</summary>
		/// <param name="path1">The first path to combine.</param>
		/// <param name="path2">The second path to combine.</param>
		/// <param name="path3">The third path to combine.</param>
		/// <returns>The combined paths.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path1" />, <paramref name="path2" />, or <paramref name="path3" /> contains one or more of the invalid characters defined in <see cref="M:System.IO.Path.GetInvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path1" />, <paramref name="path2" />, or <paramref name="path3" /> is <see langword="null" />.</exception>
		// Token: 0x06001927 RID: 6439 RVA: 0x0005332C File Offset: 0x0005152C
		[__DynamicallyInvokable]
		public static string Combine(string path1, string path2, string path3)
		{
			if (path1 == null || path2 == null || path3 == null)
			{
				throw new ArgumentNullException((path1 == null) ? "path1" : ((path2 == null) ? "path2" : "path3"));
			}
			Path.CheckInvalidPathChars(path1, false);
			Path.CheckInvalidPathChars(path2, false);
			Path.CheckInvalidPathChars(path3, false);
			return Path.CombineNoChecks(Path.CombineNoChecks(path1, path2), path3);
		}

		/// <summary>Combines four strings into a path.</summary>
		/// <param name="path1">The first path to combine.</param>
		/// <param name="path2">The second path to combine.</param>
		/// <param name="path3">The third path to combine.</param>
		/// <param name="path4">The fourth path to combine.</param>
		/// <returns>The combined paths.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path1" />, <paramref name="path2" />, <paramref name="path3" />, or <paramref name="path4" /> contains one or more of the invalid characters defined in <see cref="M:System.IO.Path.GetInvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path1" />, <paramref name="path2" />, <paramref name="path3" />, or <paramref name="path4" /> is <see langword="null" />.</exception>
		// Token: 0x06001928 RID: 6440 RVA: 0x00053384 File Offset: 0x00051584
		public static string Combine(string path1, string path2, string path3, string path4)
		{
			if (path1 == null || path2 == null || path3 == null || path4 == null)
			{
				throw new ArgumentNullException((path1 == null) ? "path1" : ((path2 == null) ? "path2" : ((path3 == null) ? "path3" : "path4")));
			}
			Path.CheckInvalidPathChars(path1, false);
			Path.CheckInvalidPathChars(path2, false);
			Path.CheckInvalidPathChars(path3, false);
			Path.CheckInvalidPathChars(path4, false);
			return Path.CombineNoChecks(Path.CombineNoChecks(Path.CombineNoChecks(path1, path2), path3), path4);
		}

		/// <summary>Combines an array of strings into a path.</summary>
		/// <param name="paths">An array of parts of the path.</param>
		/// <returns>The combined paths.</returns>
		/// <exception cref="T:System.ArgumentException">One of the strings in the array contains one or more of the invalid characters defined in <see cref="M:System.IO.Path.GetInvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">One of the strings in the array is <see langword="null" />.</exception>
		// Token: 0x06001929 RID: 6441 RVA: 0x000533F8 File Offset: 0x000515F8
		[__DynamicallyInvokable]
		public static string Combine(params string[] paths)
		{
			if (paths == null)
			{
				throw new ArgumentNullException("paths");
			}
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < paths.Length; i++)
			{
				if (paths[i] == null)
				{
					throw new ArgumentNullException("paths");
				}
				if (paths[i].Length != 0)
				{
					Path.CheckInvalidPathChars(paths[i], false);
					if (Path.IsPathRooted(paths[i]))
					{
						num2 = i;
						num = paths[i].Length;
					}
					else
					{
						num += paths[i].Length;
					}
					char c = paths[i][paths[i].Length - 1];
					if (c != Path.DirectorySeparatorChar && c != Path.AltDirectorySeparatorChar && c != Path.VolumeSeparatorChar)
					{
						num++;
					}
				}
			}
			StringBuilder stringBuilder = StringBuilderCache.Acquire(num);
			for (int j = num2; j < paths.Length; j++)
			{
				if (paths[j].Length != 0)
				{
					if (stringBuilder.Length == 0)
					{
						stringBuilder.Append(paths[j]);
					}
					else
					{
						char c2 = stringBuilder[stringBuilder.Length - 1];
						if (c2 != Path.DirectorySeparatorChar && c2 != Path.AltDirectorySeparatorChar && c2 != Path.VolumeSeparatorChar)
						{
							stringBuilder.Append(Path.DirectorySeparatorChar);
						}
						stringBuilder.Append(paths[j]);
					}
				}
			}
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x00053524 File Offset: 0x00051724
		internal static string CombineNoChecks(string path1, string path2)
		{
			if (path2.Length == 0)
			{
				return path1;
			}
			if (path1.Length == 0)
			{
				return path2;
			}
			if (Path.IsPathRooted(path2))
			{
				return path2;
			}
			char c = path1[path1.Length - 1];
			if (c != Path.DirectorySeparatorChar && c != Path.AltDirectorySeparatorChar && c != Path.VolumeSeparatorChar)
			{
				return path1 + "\\" + path2;
			}
			return path1 + path2;
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x0005358C File Offset: 0x0005178C
		internal static string ToBase32StringSuitableForDirName(byte[] buff)
		{
			StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
			int num = buff.Length;
			int num2 = 0;
			do
			{
				byte b = ((num2 < num) ? buff[num2++] : 0);
				byte b2 = ((num2 < num) ? buff[num2++] : 0);
				byte b3 = ((num2 < num) ? buff[num2++] : 0);
				byte b4 = ((num2 < num) ? buff[num2++] : 0);
				byte b5 = ((num2 < num) ? buff[num2++] : 0);
				stringBuilder.Append(Path.s_Base32Char[(int)(b & 31)]);
				stringBuilder.Append(Path.s_Base32Char[(int)(b2 & 31)]);
				stringBuilder.Append(Path.s_Base32Char[(int)(b3 & 31)]);
				stringBuilder.Append(Path.s_Base32Char[(int)(b4 & 31)]);
				stringBuilder.Append(Path.s_Base32Char[(int)(b5 & 31)]);
				stringBuilder.Append(Path.s_Base32Char[((b & 224) >> 5) | ((b4 & 96) >> 2)]);
				stringBuilder.Append(Path.s_Base32Char[((b2 & 224) >> 5) | ((b5 & 96) >> 2)]);
				b3 = (byte)(b3 >> 5);
				if ((b4 & 128) != 0)
				{
					b3 |= 8;
				}
				if ((b5 & 128) != 0)
				{
					b3 |= 16;
				}
				stringBuilder.Append(Path.s_Base32Char[(int)b3]);
			}
			while (num2 < num);
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x000536E0 File Offset: 0x000518E0
		internal static void CheckSearchPattern(string searchPattern)
		{
			int num;
			while ((num = searchPattern.IndexOf("..", StringComparison.Ordinal)) != -1)
			{
				if (num + 2 == searchPattern.Length)
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_InvalidSearchPattern"));
				}
				if (searchPattern[num + 2] == Path.DirectorySeparatorChar || searchPattern[num + 2] == Path.AltDirectorySeparatorChar)
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_InvalidSearchPattern"));
				}
				searchPattern = searchPattern.Substring(num + 2);
			}
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x00053756 File Offset: 0x00051956
		internal static void CheckInvalidPathChars(string path, bool checkAdditional = false)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (PathInternal.HasIllegalCharacters(path, checkAdditional))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPathChars"));
			}
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x00053780 File Offset: 0x00051980
		internal static string InternalCombine(string path1, string path2)
		{
			if (path1 == null || path2 == null)
			{
				throw new ArgumentNullException((path1 == null) ? "path1" : "path2");
			}
			Path.CheckInvalidPathChars(path1, false);
			Path.CheckInvalidPathChars(path2, false);
			if (path2.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_PathEmpty"), "path2");
			}
			if (Path.IsPathRooted(path2))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_Path2IsRooted"), "path2");
			}
			int length = path1.Length;
			if (length == 0)
			{
				return path2;
			}
			char c = path1[length - 1];
			if (c != Path.DirectorySeparatorChar && c != Path.AltDirectorySeparatorChar && c != Path.VolumeSeparatorChar)
			{
				return path1 + "\\" + path2;
			}
			return path1 + path2;
		}

		/// <summary>Provides a platform-specific character used to separate directory levels in a path string that reflects a hierarchical file system organization.</summary>
		// Token: 0x040008C4 RID: 2244
		[__DynamicallyInvokable]
		public static readonly char DirectorySeparatorChar = '\\';

		// Token: 0x040008C5 RID: 2245
		internal const string DirectorySeparatorCharAsString = "\\";

		/// <summary>Provides a platform-specific alternate character used to separate directory levels in a path string that reflects a hierarchical file system organization.</summary>
		// Token: 0x040008C6 RID: 2246
		[__DynamicallyInvokable]
		public static readonly char AltDirectorySeparatorChar = '/';

		/// <summary>Provides a platform-specific volume separator character.</summary>
		// Token: 0x040008C7 RID: 2247
		[__DynamicallyInvokable]
		public static readonly char VolumeSeparatorChar = ':';

		/// <summary>Provides a platform-specific array of characters that cannot be specified in path string arguments passed to members of the <see cref="T:System.IO.Path" /> class.</summary>
		// Token: 0x040008C8 RID: 2248
		[Obsolete("Please use GetInvalidPathChars or GetInvalidFileNameChars instead.")]
		public static readonly char[] InvalidPathChars = new char[]
		{
			'"', '<', '>', '|', '\0', '\u0001', '\u0002', '\u0003', '\u0004', '\u0005',
			'\u0006', '\a', '\b', '\t', '\n', '\v', '\f', '\r', '\u000e', '\u000f',
			'\u0010', '\u0011', '\u0012', '\u0013', '\u0014', '\u0015', '\u0016', '\u0017', '\u0018', '\u0019',
			'\u001a', '\u001b', '\u001c', '\u001d', '\u001e', '\u001f'
		};

		// Token: 0x040008C9 RID: 2249
		internal static readonly char[] TrimEndChars = LongPathHelper.s_trimEndChars;

		// Token: 0x040008CA RID: 2250
		private static readonly char[] RealInvalidPathChars = PathInternal.InvalidPathChars;

		// Token: 0x040008CB RID: 2251
		private static readonly char[] InvalidPathCharsWithAdditionalChecks = new char[]
		{
			'"', '<', '>', '|', '\0', '\u0001', '\u0002', '\u0003', '\u0004', '\u0005',
			'\u0006', '\a', '\b', '\t', '\n', '\v', '\f', '\r', '\u000e', '\u000f',
			'\u0010', '\u0011', '\u0012', '\u0013', '\u0014', '\u0015', '\u0016', '\u0017', '\u0018', '\u0019',
			'\u001a', '\u001b', '\u001c', '\u001d', '\u001e', '\u001f', '*', '?'
		};

		// Token: 0x040008CC RID: 2252
		private static readonly char[] InvalidFileNameChars = new char[]
		{
			'"', '<', '>', '|', '\0', '\u0001', '\u0002', '\u0003', '\u0004', '\u0005',
			'\u0006', '\a', '\b', '\t', '\n', '\v', '\f', '\r', '\u000e', '\u000f',
			'\u0010', '\u0011', '\u0012', '\u0013', '\u0014', '\u0015', '\u0016', '\u0017', '\u0018', '\u0019',
			'\u001a', '\u001b', '\u001c', '\u001d', '\u001e', '\u001f', ':', '*', '?', '\\',
			'/'
		};

		/// <summary>A platform-specific separator character used to separate path strings in environment variables.</summary>
		// Token: 0x040008CD RID: 2253
		[__DynamicallyInvokable]
		public static readonly char PathSeparator = ';';

		// Token: 0x040008CE RID: 2254
		internal static readonly int MaxPath = 260;

		// Token: 0x040008CF RID: 2255
		private static readonly int MaxDirectoryLength = PathInternal.MaxComponentLength;

		// Token: 0x040008D0 RID: 2256
		internal const int MAX_PATH = 260;

		// Token: 0x040008D1 RID: 2257
		internal const int MAX_DIRECTORY_PATH = 248;

		// Token: 0x040008D2 RID: 2258
		internal const int MaxLongPath = 32767;

		// Token: 0x040008D3 RID: 2259
		private const string LongPathPrefix = "\\\\?\\";

		// Token: 0x040008D4 RID: 2260
		private const string UNCPathPrefix = "\\\\";

		// Token: 0x040008D5 RID: 2261
		private const string UNCLongPathPrefixToInsert = "?\\UNC\\";

		// Token: 0x040008D6 RID: 2262
		private const string UNCLongPathPrefix = "\\\\?\\UNC\\";

		// Token: 0x040008D7 RID: 2263
		private static readonly char[] s_Base32Char = new char[]
		{
			'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
			'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
			'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3',
			'4', '5'
		};
	}
}
