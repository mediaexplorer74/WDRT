using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.IO
{
	/// <summary>Provides static methods for the creation, copying, deletion, moving, and opening of a single file, and aids in the creation of <see cref="T:System.IO.FileStream" /> objects.</summary>
	// Token: 0x02000182 RID: 386
	[ComVisible(true)]
	public static class File
	{
		/// <summary>Opens an existing UTF-8 encoded text file for reading.</summary>
		/// <param name="path">The file to be opened for reading.</param>
		/// <returns>A <see cref="T:System.IO.StreamReader" /> on the specified path.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path" /> was not found.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		// Token: 0x0600179C RID: 6044 RVA: 0x0004BBA9 File Offset: 0x00049DA9
		public static StreamReader OpenText(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			return new StreamReader(path);
		}

		/// <summary>Creates or opens a file for writing UTF-8 encoded text. If the file already exists, its contents are overwritten.</summary>
		/// <param name="path">The file to be opened for writing.</param>
		/// <returns>A <see cref="T:System.IO.StreamWriter" /> that writes to the specified file using UTF-8 encoding.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		// Token: 0x0600179D RID: 6045 RVA: 0x0004BBBF File Offset: 0x00049DBF
		public static StreamWriter CreateText(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			return new StreamWriter(path, false);
		}

		/// <summary>Creates a <see cref="T:System.IO.StreamWriter" /> that appends UTF-8 encoded text to an existing file, or to a new file if the specified file does not exist.</summary>
		/// <param name="path">The path to the file to append to.</param>
		/// <returns>A stream writer that appends UTF-8 encoded text to the specified file or to a new file.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, the directory doesn't exist or it is on an unmapped drive).</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		// Token: 0x0600179E RID: 6046 RVA: 0x0004BBD6 File Offset: 0x00049DD6
		public static StreamWriter AppendText(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			return new StreamWriter(path, true);
		}

		/// <summary>Copies an existing file to a new file. Overwriting a file of the same name is not allowed.</summary>
		/// <param name="sourceFileName">The file to copy.</param>
		/// <param name="destFileName">The name of the destination file. This cannot be a directory or an existing file.</param>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="sourceFileName" /> or <paramref name="destFileName" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.  
		/// -or-  
		/// <paramref name="sourceFileName" /> or <paramref name="destFileName" /> specifies a directory.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceFileName" /> or <paramref name="destFileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path specified in <paramref name="sourceFileName" /> or <paramref name="destFileName" /> is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="sourceFileName" /> was not found.</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="destFileName" /> exists.  
		/// -or-  
		/// An I/O error has occurred.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="sourceFileName" /> or <paramref name="destFileName" /> is in an invalid format.</exception>
		// Token: 0x0600179F RID: 6047 RVA: 0x0004BBF0 File Offset: 0x00049DF0
		public static void Copy(string sourceFileName, string destFileName)
		{
			if (sourceFileName == null)
			{
				throw new ArgumentNullException("sourceFileName", Environment.GetResourceString("ArgumentNull_FileName"));
			}
			if (destFileName == null)
			{
				throw new ArgumentNullException("destFileName", Environment.GetResourceString("ArgumentNull_FileName"));
			}
			if (sourceFileName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "sourceFileName");
			}
			if (destFileName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "destFileName");
			}
			File.InternalCopy(sourceFileName, destFileName, false, true);
		}

		/// <summary>Copies an existing file to a new file. Overwriting a file of the same name is allowed.</summary>
		/// <param name="sourceFileName">The file to copy.</param>
		/// <param name="destFileName">The name of the destination file. This cannot be a directory.</param>
		/// <param name="overwrite">
		///   <see langword="true" /> if the destination file can be overwritten; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.  
		///  -or-  
		///  <paramref name="destFileName" /> is read-only.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="sourceFileName" /> or <paramref name="destFileName" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.  
		/// -or-  
		/// <paramref name="sourceFileName" /> or <paramref name="destFileName" /> specifies a directory.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceFileName" /> or <paramref name="destFileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path specified in <paramref name="sourceFileName" /> or <paramref name="destFileName" /> is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="sourceFileName" /> was not found.</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="destFileName" /> exists and <paramref name="overwrite" /> is <see langword="false" />.  
		/// -or-  
		/// An I/O error has occurred.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="sourceFileName" /> or <paramref name="destFileName" /> is in an invalid format.</exception>
		// Token: 0x060017A0 RID: 6048 RVA: 0x0004BC74 File Offset: 0x00049E74
		public static void Copy(string sourceFileName, string destFileName, bool overwrite)
		{
			if (sourceFileName == null)
			{
				throw new ArgumentNullException("sourceFileName", Environment.GetResourceString("ArgumentNull_FileName"));
			}
			if (destFileName == null)
			{
				throw new ArgumentNullException("destFileName", Environment.GetResourceString("ArgumentNull_FileName"));
			}
			if (sourceFileName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "sourceFileName");
			}
			if (destFileName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "destFileName");
			}
			File.InternalCopy(sourceFileName, destFileName, overwrite, true);
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x0004BCF8 File Offset: 0x00049EF8
		[SecurityCritical]
		internal static void UnsafeCopy(string sourceFileName, string destFileName, bool overwrite)
		{
			if (sourceFileName == null)
			{
				throw new ArgumentNullException("sourceFileName", Environment.GetResourceString("ArgumentNull_FileName"));
			}
			if (destFileName == null)
			{
				throw new ArgumentNullException("destFileName", Environment.GetResourceString("ArgumentNull_FileName"));
			}
			if (sourceFileName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "sourceFileName");
			}
			if (destFileName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "destFileName");
			}
			File.InternalCopy(sourceFileName, destFileName, overwrite, false);
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x0004BD7C File Offset: 0x00049F7C
		[SecuritySafeCritical]
		internal static string InternalCopy(string sourceFileName, string destFileName, bool overwrite, bool checkHost)
		{
			string fullPathInternal = Path.GetFullPathInternal(sourceFileName);
			string fullPathInternal2 = Path.GetFullPathInternal(destFileName);
			FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, fullPathInternal, false, false);
			FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, fullPathInternal2, false, false);
			if (!Win32Native.CopyFile(fullPathInternal, fullPathInternal2, !overwrite))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				string text = destFileName;
				if (lastWin32Error != 80)
				{
					using (SafeFileHandle safeFileHandle = Win32Native.UnsafeCreateFile(fullPathInternal, int.MinValue, FileShare.Read, null, FileMode.Open, 0, IntPtr.Zero))
					{
						if (safeFileHandle.IsInvalid)
						{
							text = sourceFileName;
						}
					}
					if (lastWin32Error == 5 && Directory.InternalExists(fullPathInternal2))
					{
						throw new IOException(Environment.GetResourceString("Arg_FileIsDirectory_Name", new object[] { destFileName }), 5, fullPathInternal2);
					}
				}
				__Error.WinIOError(lastWin32Error, text);
			}
			return fullPathInternal2;
		}

		/// <summary>Creates or overwrites a file in the specified path.</summary>
		/// <param name="path">The path and name of the file to create.</param>
		/// <returns>A <see cref="T:System.IO.FileStream" /> that provides read/write access to the file specified in <paramref name="path" />.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.  
		///  -or-  
		///  <paramref name="path" /> specified a file that is read-only.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while creating the file.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		// Token: 0x060017A3 RID: 6051 RVA: 0x0004BE38 File Offset: 0x0004A038
		public static FileStream Create(string path)
		{
			return File.Create(path, 4096);
		}

		/// <summary>Creates or overwrites the specified file.</summary>
		/// <param name="path">The name of the file.</param>
		/// <param name="bufferSize">The number of bytes buffered for reads and writes to the file.</param>
		/// <returns>A <see cref="T:System.IO.FileStream" /> with the specified buffer size that provides read/write access to the file specified in <paramref name="path" />.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.  
		///  -or-  
		///  <paramref name="path" /> specified a file that is read-only.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while creating the file.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		// Token: 0x060017A4 RID: 6052 RVA: 0x0004BE45 File Offset: 0x0004A045
		public static FileStream Create(string path, int bufferSize)
		{
			return new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None, bufferSize);
		}

		/// <summary>Creates or overwrites the specified file, specifying a buffer size and a <see cref="T:System.IO.FileOptions" /> value that describes how to create or overwrite the file.</summary>
		/// <param name="path">The name of the file.</param>
		/// <param name="bufferSize">The number of bytes buffered for reads and writes to the file.</param>
		/// <param name="options">One of the <see cref="T:System.IO.FileOptions" /> values that describes how to create or overwrite the file.</param>
		/// <returns>A new file with the specified buffer size.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.  
		///  -or-  
		///  <paramref name="path" /> specified a file that is read-only.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while creating the file.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		// Token: 0x060017A5 RID: 6053 RVA: 0x0004BE51 File Offset: 0x0004A051
		public static FileStream Create(string path, int bufferSize, FileOptions options)
		{
			return new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None, bufferSize, options);
		}

		/// <summary>Creates or overwrites the specified file with the specified buffer size, file options, and file security.</summary>
		/// <param name="path">The name of the file.</param>
		/// <param name="bufferSize">The number of bytes buffered for reads and writes to the file.</param>
		/// <param name="options">One of the <see cref="T:System.IO.FileOptions" /> values that describes how to create or overwrite the file.</param>
		/// <param name="fileSecurity">One of the <see cref="T:System.Security.AccessControl.FileSecurity" /> values that determines the access control and audit security for the file.</param>
		/// <returns>A new file with the specified buffer size, file options, and file security.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.  
		///  -or-  
		///  <paramref name="path" /> specified a file that is read-only.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while creating the file.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		// Token: 0x060017A6 RID: 6054 RVA: 0x0004BE5E File Offset: 0x0004A05E
		public static FileStream Create(string path, int bufferSize, FileOptions options, FileSecurity fileSecurity)
		{
			return new FileStream(path, FileMode.Create, FileSystemRights.ReadData | FileSystemRights.WriteData | FileSystemRights.AppendData | FileSystemRights.ReadExtendedAttributes | FileSystemRights.WriteExtendedAttributes | FileSystemRights.ReadAttributes | FileSystemRights.WriteAttributes | FileSystemRights.ReadPermissions, FileShare.None, bufferSize, options, fileSecurity);
		}

		/// <summary>Deletes the specified file.</summary>
		/// <param name="path">The name of the file to be deleted. Wildcard characters are not supported.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">The specified file is in use.  
		///  -or-  
		///  There is an open handle on the file, and the operating system is Windows XP or earlier. This open handle can result from enumerating directories and files. For more information, see How to: Enumerate Directories and Files.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.  
		///  -or-  
		///  The file is an executable file that is in use.  
		///  -or-  
		///  <paramref name="path" /> is a directory.  
		///  -or-  
		///  <paramref name="path" /> specified a read-only file.</exception>
		// Token: 0x060017A7 RID: 6055 RVA: 0x0004BE70 File Offset: 0x0004A070
		[SecuritySafeCritical]
		public static void Delete(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			File.InternalDelete(path, true);
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x0004BE87 File Offset: 0x0004A087
		[SecurityCritical]
		internal static void UnsafeDelete(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			File.InternalDelete(path, false);
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x0004BEA0 File Offset: 0x0004A0A0
		[SecurityCritical]
		internal static void InternalDelete(string path, bool checkHost)
		{
			string fullPathInternal = Path.GetFullPathInternal(path);
			FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, fullPathInternal, false, false);
			if (!Win32Native.DeleteFile(fullPathInternal))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error == 2)
				{
					return;
				}
				__Error.WinIOError(lastWin32Error, fullPathInternal);
			}
		}

		/// <summary>Decrypts a file that was encrypted by the current account using the <see cref="M:System.IO.File.Encrypt(System.String)" /> method.</summary>
		/// <param name="path">A path that describes a file to decrypt.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> parameter is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DriveNotFoundException">An invalid drive was specified.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file described by the <paramref name="path" /> parameter could not be found.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. For example, the encrypted file is already open.  
		///  -or-  
		///  This operation is not supported on the current platform.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		/// <exception cref="T:System.NotSupportedException">The file system is not NTFS.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="path" /> parameter specified a file that is read-only.  
		///  -or-  
		///  This operation is not supported on the current platform.  
		///  -or-  
		///  The <paramref name="path" /> parameter specified a directory.  
		///  -or-  
		///  The caller does not have the required permission.</exception>
		// Token: 0x060017AA RID: 6058 RVA: 0x0004BEDC File Offset: 0x0004A0DC
		[SecuritySafeCritical]
		public static void Decrypt(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			string fullPathInternal = Path.GetFullPathInternal(path);
			FileIOPermission.QuickDemand(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, fullPathInternal, false, false);
			if (!Win32Native.DecryptFile(fullPathInternal, 0))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error == 5)
				{
					DriveInfo driveInfo = new DriveInfo(Path.GetPathRoot(fullPathInternal));
					if (!string.Equals("NTFS", driveInfo.DriveFormat))
					{
						throw new NotSupportedException(Environment.GetResourceString("NotSupported_EncryptionNeedsNTFS"));
					}
				}
				__Error.WinIOError(lastWin32Error, fullPathInternal);
			}
		}

		/// <summary>Encrypts a file so that only the account used to encrypt the file can decrypt it.</summary>
		/// <param name="path">A path that describes a file to encrypt.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> parameter is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DriveNotFoundException">An invalid drive was specified.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file described by the <paramref name="path" /> parameter could not be found.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.  
		///  -or-  
		///  This operation is not supported on the current platform.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		/// <exception cref="T:System.NotSupportedException">The file system is not NTFS.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="path" /> parameter specified a file that is read-only.  
		///  -or-  
		///  This operation is not supported on the current platform.  
		///  -or-  
		///  The <paramref name="path" /> parameter specified a directory.  
		///  -or-  
		///  The caller does not have the required permission.</exception>
		// Token: 0x060017AB RID: 6059 RVA: 0x0004BF54 File Offset: 0x0004A154
		[SecuritySafeCritical]
		public static void Encrypt(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			string fullPathInternal = Path.GetFullPathInternal(path);
			FileIOPermission.QuickDemand(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, fullPathInternal, false, false);
			if (!Win32Native.EncryptFile(fullPathInternal))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error == 5)
				{
					DriveInfo driveInfo = new DriveInfo(Path.GetPathRoot(fullPathInternal));
					if (!string.Equals("NTFS", driveInfo.DriveFormat))
					{
						throw new NotSupportedException(Environment.GetResourceString("NotSupported_EncryptionNeedsNTFS"));
					}
				}
				__Error.WinIOError(lastWin32Error, fullPathInternal);
			}
		}

		/// <summary>Determines whether the specified file exists.</summary>
		/// <param name="path">The file to check.</param>
		/// <returns>
		///   <see langword="true" /> if the caller has the required permissions and <paramref name="path" /> contains the name of an existing file; otherwise, <see langword="false" />. This method also returns <see langword="false" /> if <paramref name="path" /> is <see langword="null" />, an invalid path, or a zero-length string. If the caller does not have sufficient permissions to read the specified file, no exception is thrown and the method returns <see langword="false" /> regardless of the existence of <paramref name="path" />.</returns>
		// Token: 0x060017AC RID: 6060 RVA: 0x0004BFC8 File Offset: 0x0004A1C8
		[SecuritySafeCritical]
		public static bool Exists(string path)
		{
			return File.InternalExistsHelper(path, true);
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x0004BFD1 File Offset: 0x0004A1D1
		[SecurityCritical]
		internal static bool UnsafeExists(string path)
		{
			return File.InternalExistsHelper(path, false);
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x0004BFDC File Offset: 0x0004A1DC
		[SecurityCritical]
		private static bool InternalExistsHelper(string path, bool checkHost)
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
				path = Path.GetFullPathInternal(path);
				if (path.Length > 0 && Path.IsDirectorySeparator(path[path.Length - 1]))
				{
					return false;
				}
				FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, path, false, false);
				return File.InternalExists(path);
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

		// Token: 0x060017AF RID: 6063 RVA: 0x0004C08C File Offset: 0x0004A28C
		[SecurityCritical]
		internal static bool InternalExists(string path)
		{
			Win32Native.WIN32_FILE_ATTRIBUTE_DATA win32_FILE_ATTRIBUTE_DATA = default(Win32Native.WIN32_FILE_ATTRIBUTE_DATA);
			return File.FillAttributeInfo(path, ref win32_FILE_ATTRIBUTE_DATA, false, true) == 0 && win32_FILE_ATTRIBUTE_DATA.fileAttributes != -1 && (win32_FILE_ATTRIBUTE_DATA.fileAttributes & 16) == 0;
		}

		/// <summary>Opens a <see cref="T:System.IO.FileStream" /> on the specified path with read/write access with no sharing.</summary>
		/// <param name="path">The file to open.</param>
		/// <param name="mode">A <see cref="T:System.IO.FileMode" /> value that specifies whether a file is created if one does not exist, and determines whether the contents of existing files are retained or overwritten.</param>
		/// <returns>A <see cref="T:System.IO.FileStream" /> opened in the specified mode and path, with read/write access and not shared.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> specified a file that is read-only.  
		/// -or-  
		/// This operation is not supported on the current platform.  
		/// -or-  
		/// <paramref name="path" /> specified a directory.  
		/// -or-  
		/// The caller does not have the required permission.  
		/// -or-  
		/// <paramref name="mode" /> is <see cref="F:System.IO.FileMode.Create" /> and the specified file is a hidden file.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="mode" /> specified an invalid value.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path" /> was not found.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		// Token: 0x060017B0 RID: 6064 RVA: 0x0004C0C6 File Offset: 0x0004A2C6
		public static FileStream Open(string path, FileMode mode)
		{
			return File.Open(path, mode, (mode == FileMode.Append) ? FileAccess.Write : FileAccess.ReadWrite, FileShare.None);
		}

		/// <summary>Opens a <see cref="T:System.IO.FileStream" /> on the specified path, with the specified mode and access with no sharing.</summary>
		/// <param name="path">The file to open.</param>
		/// <param name="mode">A <see cref="T:System.IO.FileMode" /> value that specifies whether a file is created if one does not exist, and determines whether the contents of existing files are retained or overwritten.</param>
		/// <param name="access">A <see cref="T:System.IO.FileAccess" /> value that specifies the operations that can be performed on the file.</param>
		/// <returns>An unshared <see cref="T:System.IO.FileStream" /> that provides access to the specified file, with the specified mode and access.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.  
		/// -or-  
		/// <paramref name="access" /> specified <see langword="Read" /> and <paramref name="mode" /> specified <see langword="Create" />, <see langword="CreateNew" />, <see langword="Truncate" />, or <see langword="Append" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> specified a file that is read-only and <paramref name="access" /> is not <see langword="Read" />.  
		/// -or-  
		/// <paramref name="path" /> specified a directory.  
		/// -or-  
		/// The caller does not have the required permission.  
		/// -or-  
		/// <paramref name="mode" /> is <see cref="F:System.IO.FileMode.Create" /> and the specified file is a hidden file.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="mode" /> or <paramref name="access" /> specified an invalid value.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path" /> was not found.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		// Token: 0x060017B1 RID: 6065 RVA: 0x0004C0D8 File Offset: 0x0004A2D8
		public static FileStream Open(string path, FileMode mode, FileAccess access)
		{
			return File.Open(path, mode, access, FileShare.None);
		}

		/// <summary>Opens a <see cref="T:System.IO.FileStream" /> on the specified path, having the specified mode with read, write, or read/write access and the specified sharing option.</summary>
		/// <param name="path">The file to open.</param>
		/// <param name="mode">A <see cref="T:System.IO.FileMode" /> value that specifies whether a file is created if one does not exist, and determines whether the contents of existing files are retained or overwritten.</param>
		/// <param name="access">A <see cref="T:System.IO.FileAccess" /> value that specifies the operations that can be performed on the file.</param>
		/// <param name="share">A <see cref="T:System.IO.FileShare" /> value specifying the type of access other threads have to the file.</param>
		/// <returns>A <see cref="T:System.IO.FileStream" /> on the specified path, having the specified mode with read, write, or read/write access and the specified sharing option.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.  
		/// -or-  
		/// <paramref name="access" /> specified <see langword="Read" /> and <paramref name="mode" /> specified <see langword="Create" />, <see langword="CreateNew" />, <see langword="Truncate" />, or <see langword="Append" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> specified a file that is read-only and <paramref name="access" /> is not <see langword="Read" />.  
		/// -or-  
		/// <paramref name="path" /> specified a directory.  
		/// -or-  
		/// The caller does not have the required permission.  
		/// -or-  
		/// <paramref name="mode" /> is <see cref="F:System.IO.FileMode.Create" /> and the specified file is a hidden file.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="mode" />, <paramref name="access" />, or <paramref name="share" /> specified an invalid value.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path" /> was not found.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		// Token: 0x060017B2 RID: 6066 RVA: 0x0004C0E3 File Offset: 0x0004A2E3
		public static FileStream Open(string path, FileMode mode, FileAccess access, FileShare share)
		{
			return new FileStream(path, mode, access, share);
		}

		/// <summary>Sets the date and time the file was created.</summary>
		/// <param name="path">The file for which to set the creation date and time information.</param>
		/// <param name="creationTime">A <see cref="T:System.DateTime" /> containing the value to set for the creation date and time of <paramref name="path" />. This value is expressed in local time.</param>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while performing the operation.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="creationTime" /> specifies a value outside the range of dates, times, or both permitted for this operation.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		// Token: 0x060017B3 RID: 6067 RVA: 0x0004C0EE File Offset: 0x0004A2EE
		public static void SetCreationTime(string path, DateTime creationTime)
		{
			File.SetCreationTimeUtc(path, creationTime.ToUniversalTime());
		}

		/// <summary>Sets the date and time, in coordinated universal time (UTC), that the file was created.</summary>
		/// <param name="path">The file for which to set the creation date and time information.</param>
		/// <param name="creationTimeUtc">A <see cref="T:System.DateTime" /> containing the value to set for the creation date and time of <paramref name="path" />. This value is expressed in UTC time.</param>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while performing the operation.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="creationTime" /> specifies a value outside the range of dates, times, or both permitted for this operation.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		// Token: 0x060017B4 RID: 6068 RVA: 0x0004C100 File Offset: 0x0004A300
		[SecuritySafeCritical]
		public unsafe static void SetCreationTimeUtc(string path, DateTime creationTimeUtc)
		{
			SafeFileHandle safeFileHandle;
			using (File.OpenFile(path, FileAccess.Write, out safeFileHandle))
			{
				Win32Native.FILE_TIME file_TIME = new Win32Native.FILE_TIME(creationTimeUtc.ToFileTimeUtc());
				if (!Win32Native.SetFileTime(safeFileHandle, &file_TIME, null, null))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					__Error.WinIOError(lastWin32Error, path);
				}
			}
		}

		/// <summary>Returns the creation date and time of the specified file or directory.</summary>
		/// <param name="path">The file or directory for which to obtain creation date and time information.</param>
		/// <returns>A <see cref="T:System.DateTime" /> structure set to the creation date and time for the specified file or directory. This value is expressed in local time.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		// Token: 0x060017B5 RID: 6069 RVA: 0x0004C164 File Offset: 0x0004A364
		[SecuritySafeCritical]
		public static DateTime GetCreationTime(string path)
		{
			return File.InternalGetCreationTimeUtc(path, true).ToLocalTime();
		}

		/// <summary>Returns the creation date and time, in coordinated universal time (UTC), of the specified file or directory.</summary>
		/// <param name="path">The file or directory for which to obtain creation date and time information.</param>
		/// <returns>A <see cref="T:System.DateTime" /> structure set to the creation date and time for the specified file or directory. This value is expressed in UTC time.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		// Token: 0x060017B6 RID: 6070 RVA: 0x0004C180 File Offset: 0x0004A380
		[SecuritySafeCritical]
		public static DateTime GetCreationTimeUtc(string path)
		{
			return File.InternalGetCreationTimeUtc(path, false);
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x0004C18C File Offset: 0x0004A38C
		[SecurityCritical]
		private static DateTime InternalGetCreationTimeUtc(string path, bool checkHost)
		{
			string fullPathInternal = Path.GetFullPathInternal(path);
			FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, fullPathInternal, false, false);
			Win32Native.WIN32_FILE_ATTRIBUTE_DATA win32_FILE_ATTRIBUTE_DATA = default(Win32Native.WIN32_FILE_ATTRIBUTE_DATA);
			int num = File.FillAttributeInfo(fullPathInternal, ref win32_FILE_ATTRIBUTE_DATA, false, false);
			if (num != 0)
			{
				__Error.WinIOError(num, fullPathInternal);
			}
			return DateTime.FromFileTimeUtc(win32_FILE_ATTRIBUTE_DATA.ftCreationTime.ToTicks());
		}

		/// <summary>Sets the date and time the specified file was last accessed.</summary>
		/// <param name="path">The file for which to set the access date and time information.</param>
		/// <param name="lastAccessTime">A <see cref="T:System.DateTime" /> containing the value to set for the last access date and time of <paramref name="path" />. This value is expressed in local time.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="lastAccessTime" /> specifies a value outside the range of dates or times permitted for this operation.</exception>
		// Token: 0x060017B8 RID: 6072 RVA: 0x0004C1D7 File Offset: 0x0004A3D7
		public static void SetLastAccessTime(string path, DateTime lastAccessTime)
		{
			File.SetLastAccessTimeUtc(path, lastAccessTime.ToUniversalTime());
		}

		/// <summary>Sets the date and time, in coordinated universal time (UTC), that the specified file was last accessed.</summary>
		/// <param name="path">The file for which to set the access date and time information.</param>
		/// <param name="lastAccessTimeUtc">A <see cref="T:System.DateTime" /> containing the value to set for the last access date and time of <paramref name="path" />. This value is expressed in UTC time.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="lastAccessTimeUtc" /> specifies a value outside the range of dates or times permitted for this operation.</exception>
		// Token: 0x060017B9 RID: 6073 RVA: 0x0004C1E8 File Offset: 0x0004A3E8
		[SecuritySafeCritical]
		public unsafe static void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc)
		{
			SafeFileHandle safeFileHandle;
			using (File.OpenFile(path, FileAccess.Write, out safeFileHandle))
			{
				Win32Native.FILE_TIME file_TIME = new Win32Native.FILE_TIME(lastAccessTimeUtc.ToFileTimeUtc());
				if (!Win32Native.SetFileTime(safeFileHandle, null, &file_TIME, null))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					__Error.WinIOError(lastWin32Error, path);
				}
			}
		}

		/// <summary>Returns the date and time the specified file or directory was last accessed.</summary>
		/// <param name="path">The file or directory for which to obtain access date and time information.</param>
		/// <returns>A <see cref="T:System.DateTime" /> structure set to the date and time that the specified file or directory was last accessed. This value is expressed in local time.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		// Token: 0x060017BA RID: 6074 RVA: 0x0004C24C File Offset: 0x0004A44C
		[SecuritySafeCritical]
		public static DateTime GetLastAccessTime(string path)
		{
			return File.InternalGetLastAccessTimeUtc(path, true).ToLocalTime();
		}

		/// <summary>Returns the date and time, in coordinated universal time (UTC), that the specified file or directory was last accessed.</summary>
		/// <param name="path">The file or directory for which to obtain access date and time information.</param>
		/// <returns>A <see cref="T:System.DateTime" /> structure set to the date and time that the specified file or directory was last accessed. This value is expressed in UTC time.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		// Token: 0x060017BB RID: 6075 RVA: 0x0004C268 File Offset: 0x0004A468
		[SecuritySafeCritical]
		public static DateTime GetLastAccessTimeUtc(string path)
		{
			return File.InternalGetLastAccessTimeUtc(path, false);
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x0004C274 File Offset: 0x0004A474
		[SecurityCritical]
		private static DateTime InternalGetLastAccessTimeUtc(string path, bool checkHost)
		{
			string fullPathInternal = Path.GetFullPathInternal(path);
			FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, fullPathInternal, false, false);
			Win32Native.WIN32_FILE_ATTRIBUTE_DATA win32_FILE_ATTRIBUTE_DATA = default(Win32Native.WIN32_FILE_ATTRIBUTE_DATA);
			int num = File.FillAttributeInfo(fullPathInternal, ref win32_FILE_ATTRIBUTE_DATA, false, false);
			if (num != 0)
			{
				__Error.WinIOError(num, fullPathInternal);
			}
			return DateTime.FromFileTimeUtc(win32_FILE_ATTRIBUTE_DATA.ftLastAccessTime.ToTicks());
		}

		/// <summary>Sets the date and time that the specified file was last written to.</summary>
		/// <param name="path">The file for which to set the date and time information.</param>
		/// <param name="lastWriteTime">A <see cref="T:System.DateTime" /> containing the value to set for the last write date and time of <paramref name="path" />. This value is expressed in local time.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="lastWriteTime" /> specifies a value outside the range of dates or times permitted for this operation.</exception>
		// Token: 0x060017BD RID: 6077 RVA: 0x0004C2BF File Offset: 0x0004A4BF
		public static void SetLastWriteTime(string path, DateTime lastWriteTime)
		{
			File.SetLastWriteTimeUtc(path, lastWriteTime.ToUniversalTime());
		}

		/// <summary>Sets the date and time, in coordinated universal time (UTC), that the specified file was last written to.</summary>
		/// <param name="path">The file for which to set the date and time information.</param>
		/// <param name="lastWriteTimeUtc">A <see cref="T:System.DateTime" /> containing the value to set for the last write date and time of <paramref name="path" />. This value is expressed in UTC time.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified path was not found.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="lastWriteTimeUtc" /> specifies a value outside the range of dates or times permitted for this operation.</exception>
		// Token: 0x060017BE RID: 6078 RVA: 0x0004C2D0 File Offset: 0x0004A4D0
		[SecuritySafeCritical]
		public unsafe static void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)
		{
			SafeFileHandle safeFileHandle;
			using (File.OpenFile(path, FileAccess.Write, out safeFileHandle))
			{
				Win32Native.FILE_TIME file_TIME = new Win32Native.FILE_TIME(lastWriteTimeUtc.ToFileTimeUtc());
				if (!Win32Native.SetFileTime(safeFileHandle, null, null, &file_TIME))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					__Error.WinIOError(lastWin32Error, path);
				}
			}
		}

		/// <summary>Returns the date and time the specified file or directory was last written to.</summary>
		/// <param name="path">The file or directory for which to obtain write date and time information.</param>
		/// <returns>A <see cref="T:System.DateTime" /> structure set to the date and time that the specified file or directory was last written to. This value is expressed in local time.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		// Token: 0x060017BF RID: 6079 RVA: 0x0004C334 File Offset: 0x0004A534
		[SecuritySafeCritical]
		public static DateTime GetLastWriteTime(string path)
		{
			return File.InternalGetLastWriteTimeUtc(path, true).ToLocalTime();
		}

		/// <summary>Returns the date and time, in coordinated universal time (UTC), that the specified file or directory was last written to.</summary>
		/// <param name="path">The file or directory for which to obtain write date and time information.</param>
		/// <returns>A <see cref="T:System.DateTime" /> structure set to the date and time that the specified file or directory was last written to. This value is expressed in UTC time.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		// Token: 0x060017C0 RID: 6080 RVA: 0x0004C350 File Offset: 0x0004A550
		[SecuritySafeCritical]
		public static DateTime GetLastWriteTimeUtc(string path)
		{
			return File.InternalGetLastWriteTimeUtc(path, false);
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x0004C35C File Offset: 0x0004A55C
		[SecurityCritical]
		private static DateTime InternalGetLastWriteTimeUtc(string path, bool checkHost)
		{
			string fullPathInternal = Path.GetFullPathInternal(path);
			FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, fullPathInternal, false, false);
			Win32Native.WIN32_FILE_ATTRIBUTE_DATA win32_FILE_ATTRIBUTE_DATA = default(Win32Native.WIN32_FILE_ATTRIBUTE_DATA);
			int num = File.FillAttributeInfo(fullPathInternal, ref win32_FILE_ATTRIBUTE_DATA, false, false);
			if (num != 0)
			{
				__Error.WinIOError(num, fullPathInternal);
			}
			return DateTime.FromFileTimeUtc(win32_FILE_ATTRIBUTE_DATA.ftLastWriteTime.ToTicks());
		}

		/// <summary>Gets the <see cref="T:System.IO.FileAttributes" /> of the file on the path.</summary>
		/// <param name="path">The path to the file.</param>
		/// <returns>The <see cref="T:System.IO.FileAttributes" /> of the file on the path.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is empty, contains only white spaces, or contains invalid characters.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="path" /> represents a file and is invalid, such as being on an unmapped drive, or the file cannot be found.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="path" /> represents a directory and is invalid, such as being on an unmapped drive, or the directory cannot be found.</exception>
		/// <exception cref="T:System.IO.IOException">This file is being used by another process.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		// Token: 0x060017C2 RID: 6082 RVA: 0x0004C3A8 File Offset: 0x0004A5A8
		[SecuritySafeCritical]
		public static FileAttributes GetAttributes(string path)
		{
			string fullPathInternal = Path.GetFullPathInternal(path);
			FileIOPermission.QuickDemand(FileIOPermissionAccess.Read, fullPathInternal, false, false);
			Win32Native.WIN32_FILE_ATTRIBUTE_DATA win32_FILE_ATTRIBUTE_DATA = default(Win32Native.WIN32_FILE_ATTRIBUTE_DATA);
			int num = File.FillAttributeInfo(fullPathInternal, ref win32_FILE_ATTRIBUTE_DATA, false, true);
			if (num != 0)
			{
				__Error.WinIOError(num, fullPathInternal);
			}
			return (FileAttributes)win32_FILE_ATTRIBUTE_DATA.fileAttributes;
		}

		/// <summary>Sets the specified <see cref="T:System.IO.FileAttributes" /> of the file on the specified path.</summary>
		/// <param name="path">The path to the file.</param>
		/// <param name="fileAttributes">A bitwise combination of the enumeration values.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is empty, contains only white spaces, contains invalid characters, or the file attribute is invalid.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> specified a file that is read-only.  
		/// -or-  
		/// This operation is not supported on the current platform.  
		/// -or-  
		/// <paramref name="path" /> specified a directory.  
		/// -or-  
		/// The caller does not have the required permission.</exception>
		// Token: 0x060017C3 RID: 6083 RVA: 0x0004C3E8 File Offset: 0x0004A5E8
		[SecuritySafeCritical]
		public static void SetAttributes(string path, FileAttributes fileAttributes)
		{
			string fullPathInternal = Path.GetFullPathInternal(path);
			FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, fullPathInternal, false, false);
			if (!Win32Native.SetFileAttributes(fullPathInternal, (int)fileAttributes))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error == 87)
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_InvalidFileAttrs"));
				}
				__Error.WinIOError(lastWin32Error, fullPathInternal);
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.AccessControl.FileSecurity" /> object that encapsulates the access control list (ACL) entries for a specified file.</summary>
		/// <param name="path">The path to a file containing a <see cref="T:System.Security.AccessControl.FileSecurity" /> object that describes the file's access control list (ACL) information.</param>
		/// <returns>A <see cref="T:System.Security.AccessControl.FileSecurity" /> object that encapsulates the access control rules for the file described by the <paramref name="path" /> parameter.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.SEHException">The <paramref name="path" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.SystemException">The file could not be found.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="path" /> parameter specified a file that is read-only.  
		///  -or-  
		///  This operation is not supported on the current platform.  
		///  -or-  
		///  The <paramref name="path" /> parameter specified a directory.  
		///  -or-  
		///  The caller does not have the required permission.</exception>
		// Token: 0x060017C4 RID: 6084 RVA: 0x0004C432 File Offset: 0x0004A632
		public static FileSecurity GetAccessControl(string path)
		{
			return File.GetAccessControl(path, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
		}

		/// <summary>Gets a <see cref="T:System.Security.AccessControl.FileSecurity" /> object that encapsulates the specified type of access control list (ACL) entries for a particular file.</summary>
		/// <param name="path">The path to a file containing a <see cref="T:System.Security.AccessControl.FileSecurity" /> object that describes the file's access control list (ACL) information.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> values that specifies the type of access control list (ACL) information to receive.</param>
		/// <returns>A <see cref="T:System.Security.AccessControl.FileSecurity" /> object that encapsulates the access control rules for the file described by the <paramref name="path" /> parameter.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.SEHException">The <paramref name="path" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.SystemException">The file could not be found.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="path" /> parameter specified a file that is read-only.  
		///  -or-  
		///  This operation is not supported on the current platform.  
		///  -or-  
		///  The <paramref name="path" /> parameter specified a directory.  
		///  -or-  
		///  The caller does not have the required permission.</exception>
		// Token: 0x060017C5 RID: 6085 RVA: 0x0004C43C File Offset: 0x0004A63C
		public static FileSecurity GetAccessControl(string path, AccessControlSections includeSections)
		{
			return new FileSecurity(path, includeSections);
		}

		/// <summary>Applies access control list (ACL) entries described by a <see cref="T:System.Security.AccessControl.FileSecurity" /> object to the specified file.</summary>
		/// <param name="path">A file to add or remove access control list (ACL) entries from.</param>
		/// <param name="fileSecurity">A <see cref="T:System.Security.AccessControl.FileSecurity" /> object that describes an ACL entry to apply to the file described by the <paramref name="path" /> parameter.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.SEHException">The <paramref name="path" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.SystemException">The file could not be found.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="path" /> parameter specified a file that is read-only.  
		///  -or-  
		///  This operation is not supported on the current platform.  
		///  -or-  
		///  The <paramref name="path" /> parameter specified a directory.  
		///  -or-  
		///  The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="fileSecurity" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060017C6 RID: 6086 RVA: 0x0004C448 File Offset: 0x0004A648
		[SecuritySafeCritical]
		public static void SetAccessControl(string path, FileSecurity fileSecurity)
		{
			if (fileSecurity == null)
			{
				throw new ArgumentNullException("fileSecurity");
			}
			string fullPathInternal = Path.GetFullPathInternal(path);
			fileSecurity.Persist(fullPathInternal);
		}

		/// <summary>Opens an existing file for reading.</summary>
		/// <param name="path">The file to be opened for reading.</param>
		/// <returns>A read-only <see cref="T:System.IO.FileStream" /> on the specified path.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> specified a directory.  
		/// -or-  
		/// The caller does not have the required permission.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path" /> was not found.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
		// Token: 0x060017C7 RID: 6087 RVA: 0x0004C471 File Offset: 0x0004A671
		public static FileStream OpenRead(string path)
		{
			return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
		}

		/// <summary>Opens an existing file or creates a new file for writing.</summary>
		/// <param name="path">The file to be opened for writing.</param>
		/// <returns>An unshared <see cref="T:System.IO.FileStream" /> object on the specified path with <see cref="F:System.IO.FileAccess.Write" /> access.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.  
		///  -or-  
		///  <paramref name="path" /> specified a read-only file or directory.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		// Token: 0x060017C8 RID: 6088 RVA: 0x0004C47C File Offset: 0x0004A67C
		public static FileStream OpenWrite(string path)
		{
			return new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
		}

		/// <summary>Opens a text file, reads all the text in the file, and then closes the file.</summary>
		/// <param name="path">The file to open for reading.</param>
		/// <returns>A string containing all the text in the file.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> specified a file that is read-only.  
		/// -or-  
		/// This operation is not supported on the current platform.  
		/// -or-  
		/// <paramref name="path" /> specified a directory.  
		/// -or-  
		/// The caller does not have the required permission.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path" /> was not found.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x060017C9 RID: 6089 RVA: 0x0004C487 File Offset: 0x0004A687
		[SecuritySafeCritical]
		public static string ReadAllText(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			return File.InternalReadAllText(path, Encoding.UTF8, true);
		}

		/// <summary>Opens a file, reads all text in the file with the specified encoding, and then closes the file.</summary>
		/// <param name="path">The file to open for reading.</param>
		/// <param name="encoding">The encoding applied to the contents of the file.</param>
		/// <returns>A string containing all text in the file.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> specified a file that is read-only.  
		/// -or-  
		/// This operation is not supported on the current platform.  
		/// -or-  
		/// <paramref name="path" /> specified a directory.  
		/// -or-  
		/// The caller does not have the required permission.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path" /> was not found.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x060017CA RID: 6090 RVA: 0x0004C4BB File Offset: 0x0004A6BB
		[SecuritySafeCritical]
		public static string ReadAllText(string path, Encoding encoding)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			return File.InternalReadAllText(path, encoding, true);
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x0004C4F9 File Offset: 0x0004A6F9
		[SecurityCritical]
		internal static string UnsafeReadAllText(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			return File.InternalReadAllText(path, Encoding.UTF8, false);
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x0004C530 File Offset: 0x0004A730
		[SecurityCritical]
		private static string InternalReadAllText(string path, Encoding encoding, bool checkHost)
		{
			string text;
			using (StreamReader streamReader = new StreamReader(path, encoding, true, StreamReader.DefaultBufferSize, checkHost))
			{
				text = streamReader.ReadToEnd();
			}
			return text;
		}

		/// <summary>Creates a new file, writes the specified string to the file, and then closes the file. If the target file already exists, it is overwritten.</summary>
		/// <param name="path">The file to write to.</param>
		/// <param name="contents">The string to write to the file.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" /> or <paramref name="contents" /> is empty.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> specified a file that is read-only.  
		/// -or-  
		/// This operation is not supported on the current platform.  
		/// -or-  
		/// <paramref name="path" /> specified a directory.  
		/// -or-  
		/// The caller does not have the required permission.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x060017CD RID: 6093 RVA: 0x0004C570 File Offset: 0x0004A770
		[SecuritySafeCritical]
		public static void WriteAllText(string path, string contents)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			File.InternalWriteAllText(path, contents, StreamWriter.UTF8NoBOM, true);
		}

		/// <summary>Creates a new file, writes the specified string to the file using the specified encoding, and then closes the file. If the target file already exists, it is overwritten.</summary>
		/// <param name="path">The file to write to.</param>
		/// <param name="contents">The string to write to the file.</param>
		/// <param name="encoding">The encoding to apply to the string.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" /> or <paramref name="contents" /> is empty.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> specified a file that is read-only.  
		/// -or-  
		/// This operation is not supported on the current platform.  
		/// -or-  
		/// <paramref name="path" /> specified a directory.  
		/// -or-  
		/// The caller does not have the required permission.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x060017CE RID: 6094 RVA: 0x0004C5A5 File Offset: 0x0004A7A5
		[SecuritySafeCritical]
		public static void WriteAllText(string path, string contents, Encoding encoding)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			File.InternalWriteAllText(path, contents, encoding, true);
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x0004C5E4 File Offset: 0x0004A7E4
		[SecurityCritical]
		internal static void UnsafeWriteAllText(string path, string contents)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			File.InternalWriteAllText(path, contents, StreamWriter.UTF8NoBOM, false);
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x0004C61C File Offset: 0x0004A81C
		[SecurityCritical]
		private static void InternalWriteAllText(string path, string contents, Encoding encoding, bool checkHost)
		{
			using (StreamWriter streamWriter = new StreamWriter(path, false, encoding, 1024, checkHost))
			{
				streamWriter.Write(contents);
			}
		}

		/// <summary>Opens a binary file, reads the contents of the file into a byte array, and then closes the file.</summary>
		/// <param name="path">The file to open for reading.</param>
		/// <returns>A byte array containing the contents of the file.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">This operation is not supported on the current platform.  
		///  -or-  
		///  <paramref name="path" /> specified a directory.  
		///  -or-  
		///  The caller does not have the required permission.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path" /> was not found.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x060017D1 RID: 6097 RVA: 0x0004C65C File Offset: 0x0004A85C
		[SecuritySafeCritical]
		public static byte[] ReadAllBytes(string path)
		{
			return File.InternalReadAllBytes(path, true);
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x0004C665 File Offset: 0x0004A865
		[SecurityCritical]
		internal static byte[] UnsafeReadAllBytes(string path)
		{
			return File.InternalReadAllBytes(path, false);
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x0004C670 File Offset: 0x0004A870
		[SecurityCritical]
		private static byte[] InternalReadAllBytes(string path, bool checkHost)
		{
			byte[] array;
			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.None, Path.GetFileName(path), false, false, checkHost))
			{
				int num = 0;
				long length = fileStream.Length;
				if (length > 2147483647L)
				{
					throw new IOException(Environment.GetResourceString("IO.IO_FileTooLong2GB"));
				}
				int i = (int)length;
				array = new byte[i];
				while (i > 0)
				{
					int num2 = fileStream.Read(array, num, i);
					if (num2 == 0)
					{
						__Error.EndOfFile();
					}
					num += num2;
					i -= num2;
				}
			}
			return array;
		}

		/// <summary>Creates a new file, writes the specified byte array to the file, and then closes the file. If the target file already exists, it is overwritten.</summary>
		/// <param name="path">The file to write to.</param>
		/// <param name="bytes">The bytes to write to the file.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" /> or the byte array is empty.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> specified a file that is read-only.  
		/// -or-  
		/// This operation is not supported on the current platform.  
		/// -or-  
		/// <paramref name="path" /> specified a directory.  
		/// -or-  
		/// The caller does not have the required permission.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x060017D4 RID: 6100 RVA: 0x0004C70C File Offset: 0x0004A90C
		[SecuritySafeCritical]
		public static void WriteAllBytes(string path, byte[] bytes)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path", Environment.GetResourceString("ArgumentNull_Path"));
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			File.InternalWriteAllBytes(path, bytes, true);
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x0004C760 File Offset: 0x0004A960
		[SecurityCritical]
		internal static void UnsafeWriteAllBytes(string path, byte[] bytes)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path", Environment.GetResourceString("ArgumentNull_Path"));
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			File.InternalWriteAllBytes(path, bytes, false);
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x0004C7B4 File Offset: 0x0004A9B4
		[SecurityCritical]
		private static void InternalWriteAllBytes(string path, byte[] bytes, bool checkHost)
		{
			using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read, 4096, FileOptions.None, Path.GetFileName(path), false, false, checkHost))
			{
				fileStream.Write(bytes, 0, bytes.Length);
			}
		}

		/// <summary>Opens a text file, reads all lines of the file, and then closes the file.</summary>
		/// <param name="path">The file to open for reading.</param>
		/// <returns>A string array containing all lines of the file.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> specified a file that is read-only.  
		/// -or-  
		/// This operation is not supported on the current platform.  
		/// -or-  
		/// <paramref name="path" /> specified a directory.  
		/// -or-  
		/// The caller does not have the required permission.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path" /> was not found.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x060017D7 RID: 6103 RVA: 0x0004C804 File Offset: 0x0004AA04
		public static string[] ReadAllLines(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			return File.InternalReadAllLines(path, Encoding.UTF8);
		}

		/// <summary>Opens a file, reads all lines of the file with the specified encoding, and then closes the file.</summary>
		/// <param name="path">The file to open for reading.</param>
		/// <param name="encoding">The encoding applied to the contents of the file.</param>
		/// <returns>A string array containing all lines of the file.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> specified a file that is read-only.  
		/// -or-  
		/// This operation is not supported on the current platform.  
		/// -or-  
		/// <paramref name="path" /> specified a directory.  
		/// -or-  
		/// The caller does not have the required permission.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="path" /> was not found.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x060017D8 RID: 6104 RVA: 0x0004C837 File Offset: 0x0004AA37
		public static string[] ReadAllLines(string path, Encoding encoding)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			return File.InternalReadAllLines(path, encoding);
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x0004C874 File Offset: 0x0004AA74
		private static string[] InternalReadAllLines(string path, Encoding encoding)
		{
			List<string> list = new List<string>();
			using (StreamReader streamReader = new StreamReader(path, encoding))
			{
				string text;
				while ((text = streamReader.ReadLine()) != null)
				{
					list.Add(text);
				}
			}
			return list.ToArray();
		}

		/// <summary>Reads the lines of a file.</summary>
		/// <param name="path">The file to read.</param>
		/// <returns>All the lines of the file, or the lines that are the result of a query.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="path" /> is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified by <paramref name="path" /> was not found.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">
		///   <paramref name="path" /> exceeds the system-defined maximum length.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> specifies a file that is read-only.  
		/// -or-  
		/// This operation is not supported on the current platform.  
		/// -or-  
		/// <paramref name="path" /> is a directory.  
		/// -or-  
		/// The caller does not have the required permission.</exception>
		// Token: 0x060017DA RID: 6106 RVA: 0x0004C8C4 File Offset: 0x0004AAC4
		public static IEnumerable<string> ReadLines(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), "path");
			}
			return ReadLinesIterator.CreateIterator(path, Encoding.UTF8);
		}

		/// <summary>Read the lines of a file that has a specified encoding.</summary>
		/// <param name="path">The file to read.</param>
		/// <param name="encoding">The encoding that is applied to the contents of the file.</param>
		/// <returns>All the lines of the file, or the lines that are the result of a query.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="path" /> is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified by <paramref name="path" /> was not found.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">
		///   <paramref name="path" /> exceeds the system-defined maximum length.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> specifies a file that is read-only.  
		/// -or-  
		/// This operation is not supported on the current platform.  
		/// -or-  
		/// <paramref name="path" /> is a directory.  
		/// -or-  
		/// The caller does not have the required permission.</exception>
		// Token: 0x060017DB RID: 6107 RVA: 0x0004C8FC File Offset: 0x0004AAFC
		public static IEnumerable<string> ReadLines(string path, Encoding encoding)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"), "path");
			}
			return ReadLinesIterator.CreateIterator(path, encoding);
		}

		/// <summary>Creates a new file, write the specified string array to the file, and then closes the file.</summary>
		/// <param name="path">The file to write to.</param>
		/// <param name="contents">The string array to write to the file.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">Either <paramref name="path" /> or <paramref name="contents" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> specified a file that is read-only.  
		/// -or-  
		/// This operation is not supported on the current platform.  
		/// -or-  
		/// <paramref name="path" /> specified a directory.  
		/// -or-  
		/// The caller does not have the required permission.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x060017DC RID: 6108 RVA: 0x0004C94C File Offset: 0x0004AB4C
		public static void WriteAllLines(string path, string[] contents)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (contents == null)
			{
				throw new ArgumentNullException("contents");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			File.InternalWriteAllLines(new StreamWriter(path, false, StreamWriter.UTF8NoBOM), contents);
		}

		/// <summary>Creates a new file, writes the specified string array to the file by using the specified encoding, and then closes the file.</summary>
		/// <param name="path">The file to write to.</param>
		/// <param name="contents">The string array to write to the file.</param>
		/// <param name="encoding">An <see cref="T:System.Text.Encoding" /> object that represents the character encoding applied to the string array.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">Either <paramref name="path" /> or <paramref name="contents" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> specified a file that is read-only.  
		/// -or-  
		/// This operation is not supported on the current platform.  
		/// -or-  
		/// <paramref name="path" /> specified a directory.  
		/// -or-  
		/// The caller does not have the required permission.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x060017DD RID: 6109 RVA: 0x0004C9A0 File Offset: 0x0004ABA0
		public static void WriteAllLines(string path, string[] contents, Encoding encoding)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (contents == null)
			{
				throw new ArgumentNullException("contents");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			File.InternalWriteAllLines(new StreamWriter(path, false, encoding), contents);
		}

		/// <summary>Creates a new file, writes a collection of strings to the file, and then closes the file.</summary>
		/// <param name="path">The file to write to.</param>
		/// <param name="contents">The lines to write to the file.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">Either <paramref name="path" /> or <paramref name="contents" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="path" /> is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">
		///   <paramref name="path" /> exceeds the system-defined maximum length.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> specifies a file that is read-only.  
		/// -or-  
		/// This operation is not supported on the current platform.  
		/// -or-  
		/// <paramref name="path" /> is a directory.  
		/// -or-  
		/// The caller does not have the required permission.</exception>
		// Token: 0x060017DE RID: 6110 RVA: 0x0004CA00 File Offset: 0x0004AC00
		public static void WriteAllLines(string path, IEnumerable<string> contents)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (contents == null)
			{
				throw new ArgumentNullException("contents");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			File.InternalWriteAllLines(new StreamWriter(path, false, StreamWriter.UTF8NoBOM), contents);
		}

		/// <summary>Creates a new file by using the specified encoding, writes a collection of strings to the file, and then closes the file.</summary>
		/// <param name="path">The file to write to.</param>
		/// <param name="contents">The lines to write to the file.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">Either <paramref name="path" />, <paramref name="contents" />, or <paramref name="encoding" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="path" /> is invalid (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">
		///   <paramref name="path" /> exceeds the system-defined maximum length.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> specifies a file that is read-only.  
		/// -or-  
		/// This operation is not supported on the current platform.  
		/// -or-  
		/// <paramref name="path" /> is a directory.  
		/// -or-  
		/// The caller does not have the required permission.</exception>
		// Token: 0x060017DF RID: 6111 RVA: 0x0004CA54 File Offset: 0x0004AC54
		public static void WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (contents == null)
			{
				throw new ArgumentNullException("contents");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			File.InternalWriteAllLines(new StreamWriter(path, false, encoding), contents);
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x0004CAB4 File Offset: 0x0004ACB4
		private static void InternalWriteAllLines(TextWriter writer, IEnumerable<string> contents)
		{
			try
			{
				foreach (string text in contents)
				{
					writer.WriteLine(text);
				}
			}
			finally
			{
				if (writer != null)
				{
					((IDisposable)writer).Dispose();
				}
			}
		}

		/// <summary>Opens a file, appends the specified string to the file, and then closes the file. If the file does not exist, this method creates a file, writes the specified string to the file, then closes the file.</summary>
		/// <param name="path">The file to append the specified string to.</param>
		/// <param name="contents">The string to append to the file.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, the directory doesn't exist or it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> specified a file that is read-only.  
		/// -or-  
		/// This operation is not supported on the current platform.  
		/// -or-  
		/// <paramref name="path" /> specified a directory.  
		/// -or-  
		/// The caller does not have the required permission.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x060017E1 RID: 6113 RVA: 0x0004CB14 File Offset: 0x0004AD14
		public static void AppendAllText(string path, string contents)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			File.InternalAppendAllText(path, contents, StreamWriter.UTF8NoBOM);
		}

		/// <summary>Appends the specified string to the file, creating the file if it does not already exist.</summary>
		/// <param name="path">The file to append the specified string to.</param>
		/// <param name="contents">The string to append to the file.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, the directory doesn't exist or it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> specified a file that is read-only.  
		/// -or-  
		/// This operation is not supported on the current platform.  
		/// -or-  
		/// <paramref name="path" /> specified a directory.  
		/// -or-  
		/// The caller does not have the required permission.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x060017E2 RID: 6114 RVA: 0x0004CB48 File Offset: 0x0004AD48
		public static void AppendAllText(string path, string contents, Encoding encoding)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			File.InternalAppendAllText(path, contents, encoding);
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x0004CB88 File Offset: 0x0004AD88
		private static void InternalAppendAllText(string path, string contents, Encoding encoding)
		{
			using (StreamWriter streamWriter = new StreamWriter(path, true, encoding))
			{
				streamWriter.Write(contents);
			}
		}

		/// <summary>Appends lines to a file, and then closes the file. If the specified file does not exist, this method creates a file, writes the specified lines to the file, and then closes the file.</summary>
		/// <param name="path">The file to append the lines to. The file is created if it doesn't already exist.</param>
		/// <param name="contents">The lines to append to the file.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">Either <paramref name="path" /> or <paramref name="contents" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="path" /> is invalid (for example, the directory doesn't exist or it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified by <paramref name="path" /> was not found.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">
		///   <paramref name="path" /> exceeds the system-defined maximum length.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have permission to write to the file.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> specifies a file that is read-only.  
		/// -or-  
		/// This operation is not supported on the current platform.  
		/// -or-  
		/// <paramref name="path" /> is a directory.</exception>
		// Token: 0x060017E4 RID: 6116 RVA: 0x0004CBC4 File Offset: 0x0004ADC4
		public static void AppendAllLines(string path, IEnumerable<string> contents)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (contents == null)
			{
				throw new ArgumentNullException("contents");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			File.InternalWriteAllLines(new StreamWriter(path, true, StreamWriter.UTF8NoBOM), contents);
		}

		/// <summary>Appends lines to a file by using a specified encoding, and then closes the file. If the specified file does not exist, this method creates a file, writes the specified lines to the file, and then closes the file.</summary>
		/// <param name="path">The file to append the lines to. The file is created if it doesn't already exist.</param>
		/// <param name="contents">The lines to append to the file.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one more invalid characters defined by the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">Either <paramref name="path" />, <paramref name="contents" />, or <paramref name="encoding" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">
		///   <paramref name="path" /> is invalid (for example, the directory doesn't exist or it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified by <paramref name="path" /> was not found.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">
		///   <paramref name="path" /> exceeds the system-defined maximum length.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> is in an invalid format.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///   <paramref name="path" /> specifies a file that is read-only.  
		/// -or-  
		/// This operation is not supported on the current platform.  
		/// -or-  
		/// <paramref name="path" /> is a directory.  
		/// -or-  
		/// The caller does not have the required permission.</exception>
		// Token: 0x060017E5 RID: 6117 RVA: 0x0004CC18 File Offset: 0x0004AE18
		public static void AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (contents == null)
			{
				throw new ArgumentNullException("contents");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			File.InternalWriteAllLines(new StreamWriter(path, true, encoding), contents);
		}

		/// <summary>Moves a specified file to a new location, providing the option to specify a new file name.</summary>
		/// <param name="sourceFileName">The name of the file to move. Can include a relative or absolute path.</param>
		/// <param name="destFileName">The new path and name for the file.</param>
		/// <exception cref="T:System.IO.IOException">The destination file already exists.  
		///  -or-  
		///  <paramref name="sourceFileName" /> was not found.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceFileName" /> or <paramref name="destFileName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="sourceFileName" /> or <paramref name="destFileName" /> is a zero-length string, contains only white space, or contains invalid characters as defined in <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path specified in <paramref name="sourceFileName" /> or <paramref name="destFileName" /> is invalid, (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="sourceFileName" /> or <paramref name="destFileName" /> is in an invalid format.</exception>
		// Token: 0x060017E6 RID: 6118 RVA: 0x0004CC75 File Offset: 0x0004AE75
		[SecuritySafeCritical]
		public static void Move(string sourceFileName, string destFileName)
		{
			File.InternalMove(sourceFileName, destFileName, true);
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x0004CC7F File Offset: 0x0004AE7F
		[SecurityCritical]
		internal static void UnsafeMove(string sourceFileName, string destFileName)
		{
			File.InternalMove(sourceFileName, destFileName, false);
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x0004CC8C File Offset: 0x0004AE8C
		[SecurityCritical]
		private static void InternalMove(string sourceFileName, string destFileName, bool checkHost)
		{
			if (sourceFileName == null)
			{
				throw new ArgumentNullException("sourceFileName", Environment.GetResourceString("ArgumentNull_FileName"));
			}
			if (destFileName == null)
			{
				throw new ArgumentNullException("destFileName", Environment.GetResourceString("ArgumentNull_FileName"));
			}
			if (sourceFileName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "sourceFileName");
			}
			if (destFileName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyFileName"), "destFileName");
			}
			string fullPathInternal = Path.GetFullPathInternal(sourceFileName);
			string fullPathInternal2 = Path.GetFullPathInternal(destFileName);
			FileIOPermission.QuickDemand(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, fullPathInternal, false, false);
			FileIOPermission.QuickDemand(FileIOPermissionAccess.Write, fullPathInternal2, false, false);
			if (!File.InternalExists(fullPathInternal))
			{
				__Error.WinIOError(2, fullPathInternal);
			}
			if (!Win32Native.MoveFile(fullPathInternal, fullPathInternal2))
			{
				__Error.WinIOError();
			}
		}

		/// <summary>Replaces the contents of a specified file with the contents of another file, deleting the original file, and creating a backup of the replaced file.</summary>
		/// <param name="sourceFileName">The name of a file that replaces the file specified by <paramref name="destinationFileName" />.</param>
		/// <param name="destinationFileName">The name of the file being replaced.</param>
		/// <param name="destinationBackupFileName">The name of the backup file.</param>
		/// <exception cref="T:System.ArgumentException">The path described by the <paramref name="destinationFileName" /> parameter was not of a legal form.  
		///  -or-  
		///  The path described by the <paramref name="destinationBackupFileName" /> parameter was not of a legal form.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="destinationFileName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DriveNotFoundException">An invalid drive was specified.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file described by the current <see cref="T:System.IO.FileInfo" /> object could not be found.  
		///  -or-  
		///  The file described by the <paramref name="destinationBackupFileName" /> parameter could not be found.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.  
		/// -or-
		///  The <paramref name="sourceFileName" /> and <paramref name="destinationFileName" /> parameters specify the same file.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The operating system is Windows 98 Second Edition or earlier and the files system is not NTFS.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="sourceFileName" /> or <paramref name="destinationFileName" /> parameter specifies a file that is read-only.  
		///  -or-  
		///  This operation is not supported on the current platform.  
		///  -or-  
		///  Source or destination parameters specify a directory instead of a file.  
		///  -or-  
		///  The caller does not have the required permission.</exception>
		// Token: 0x060017E9 RID: 6121 RVA: 0x0004CD40 File Offset: 0x0004AF40
		public static void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName)
		{
			if (sourceFileName == null)
			{
				throw new ArgumentNullException("sourceFileName");
			}
			if (destinationFileName == null)
			{
				throw new ArgumentNullException("destinationFileName");
			}
			File.InternalReplace(sourceFileName, destinationFileName, destinationBackupFileName, false);
		}

		/// <summary>Replaces the contents of a specified file with the contents of another file, deleting the original file, and creating a backup of the replaced file and optionally ignores merge errors.</summary>
		/// <param name="sourceFileName">The name of a file that replaces the file specified by <paramref name="destinationFileName" />.</param>
		/// <param name="destinationFileName">The name of the file being replaced.</param>
		/// <param name="destinationBackupFileName">The name of the backup file.</param>
		/// <param name="ignoreMetadataErrors">
		///   <see langword="true" /> to ignore merge errors (such as attributes and access control lists (ACLs)) from the replaced file to the replacement file; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentException">The path described by the <paramref name="destinationFileName" /> parameter was not of a legal form.  
		///  -or-  
		///  The path described by the <paramref name="destinationBackupFileName" /> parameter was not of a legal form.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="destinationFileName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DriveNotFoundException">An invalid drive was specified.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file described by the current <see cref="T:System.IO.FileInfo" /> object could not be found.  
		///  -or-  
		///  The file described by the <paramref name="destinationBackupFileName" /> parameter could not be found.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.  
		/// -or-
		///  The <paramref name="sourceFileName" /> and <paramref name="destinationFileName" /> parameters specify the same file.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The operating system is Windows 98 Second Edition or earlier and the files system is not NTFS.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="sourceFileName" /> or <paramref name="destinationFileName" /> parameter specifies a file that is read-only.  
		///  -or-  
		///  This operation is not supported on the current platform.  
		///  -or-  
		///  Source or destination parameters specify a directory instead of a file.  
		///  -or-  
		///  The caller does not have the required permission.</exception>
		// Token: 0x060017EA RID: 6122 RVA: 0x0004CD67 File Offset: 0x0004AF67
		public static void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
		{
			if (sourceFileName == null)
			{
				throw new ArgumentNullException("sourceFileName");
			}
			if (destinationFileName == null)
			{
				throw new ArgumentNullException("destinationFileName");
			}
			File.InternalReplace(sourceFileName, destinationFileName, destinationBackupFileName, ignoreMetadataErrors);
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x0004CD90 File Offset: 0x0004AF90
		[SecuritySafeCritical]
		private static void InternalReplace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
		{
			string fullPathInternal = Path.GetFullPathInternal(sourceFileName);
			string fullPathInternal2 = Path.GetFullPathInternal(destinationFileName);
			string text = null;
			if (destinationBackupFileName != null)
			{
				text = Path.GetFullPathInternal(destinationBackupFileName);
			}
			if (CodeAccessSecurityEngine.QuickCheckForAllDemands())
			{
				FileIOPermission.EmulateFileIOPermissionChecks(fullPathInternal);
				FileIOPermission.EmulateFileIOPermissionChecks(fullPathInternal2);
				if (text != null)
				{
					FileIOPermission.EmulateFileIOPermissionChecks(text);
				}
			}
			else
			{
				FileIOPermission fileIOPermission = new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write, new string[] { fullPathInternal, fullPathInternal2 });
				if (text != null)
				{
					fileIOPermission.AddPathList(FileIOPermissionAccess.Write, text);
				}
				fileIOPermission.Demand();
			}
			int num = 1;
			if (ignoreMetadataErrors)
			{
				num |= 2;
			}
			if (!Win32Native.ReplaceFile(fullPathInternal2, fullPathInternal, text, num, IntPtr.Zero, IntPtr.Zero))
			{
				__Error.WinIOError();
			}
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x0004CE28 File Offset: 0x0004B028
		[SecurityCritical]
		internal static int FillAttributeInfo(string path, ref Win32Native.WIN32_FILE_ATTRIBUTE_DATA data, bool tryagain, bool returnErrorOnNotFound)
		{
			int num = 0;
			if (tryagain)
			{
				Win32Native.WIN32_FIND_DATA win32_FIND_DATA = default(Win32Native.WIN32_FIND_DATA);
				string text = path.TrimEnd(new char[]
				{
					Path.DirectorySeparatorChar,
					Path.AltDirectorySeparatorChar
				});
				int num2 = Win32Native.SetErrorMode(1);
				try
				{
					bool flag = false;
					SafeFindHandle safeFindHandle = Win32Native.FindFirstFile(text, ref win32_FIND_DATA);
					try
					{
						if (safeFindHandle.IsInvalid)
						{
							flag = true;
							num = Marshal.GetLastWin32Error();
							if ((num == 2 || num == 3 || num == 21) && !returnErrorOnNotFound)
							{
								num = 0;
								data.fileAttributes = -1;
							}
							return num;
						}
					}
					finally
					{
						try
						{
							safeFindHandle.Close();
						}
						catch
						{
							if (!flag)
							{
								__Error.WinIOError();
							}
						}
					}
				}
				finally
				{
					Win32Native.SetErrorMode(num2);
				}
				data.PopulateFrom(ref win32_FIND_DATA);
				return num;
			}
			bool flag2 = false;
			int num3 = Win32Native.SetErrorMode(1);
			try
			{
				flag2 = Win32Native.GetFileAttributesEx(path, 0, ref data);
			}
			finally
			{
				Win32Native.SetErrorMode(num3);
			}
			if (!flag2)
			{
				num = Marshal.GetLastWin32Error();
				if (num != 2 && num != 3 && num != 21)
				{
					return File.FillAttributeInfo(path, ref data, true, returnErrorOnNotFound);
				}
				if (!returnErrorOnNotFound)
				{
					num = 0;
					data.fileAttributes = -1;
				}
			}
			return num;
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x0004CF58 File Offset: 0x0004B158
		[SecurityCritical]
		private static FileStream OpenFile(string path, FileAccess access, out SafeFileHandle handle)
		{
			FileStream fileStream = new FileStream(path, FileMode.Open, access, FileShare.ReadWrite, 1);
			handle = fileStream.SafeFileHandle;
			if (handle.IsInvalid)
			{
				int num = Marshal.GetLastWin32Error();
				string fullPathInternal = Path.GetFullPathInternal(path);
				if (num == 3 && fullPathInternal.Equals(Directory.GetDirectoryRoot(fullPathInternal)))
				{
					num = 5;
				}
				__Error.WinIOError(num, path);
			}
			return fileStream;
		}

		// Token: 0x0400082D RID: 2093
		private const int GetFileExInfoStandard = 0;

		// Token: 0x0400082E RID: 2094
		private const int ERROR_INVALID_PARAMETER = 87;

		// Token: 0x0400082F RID: 2095
		private const int ERROR_ACCESS_DENIED = 5;
	}
}
