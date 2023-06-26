using System;
using System.IO;
using System.Security.Permissions;

namespace System.Security.AccessControl
{
	/// <summary>Represents the access control and audit security for a directory. This class cannot be inherited.</summary>
	// Token: 0x0200021D RID: 541
	public sealed class DirectorySecurity : FileSystemSecurity
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.DirectorySecurity" /> class.</summary>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Microsoft Windows 2000 or later.</exception>
		// Token: 0x06001F5C RID: 8028 RVA: 0x0006DB64 File Offset: 0x0006BD64
		[SecuritySafeCritical]
		public DirectorySecurity()
			: base(true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.DirectorySecurity" /> class from a specified directory using the specified values of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> enumeration.</summary>
		/// <param name="name">The location of a directory to create a <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object from.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> values that specifies the type of access control list (ACL) information to retrieve.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, (for example, it is on an unmapped drive).</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified in the <paramref name="name" /> parameter was not found.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the directory.</exception>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="name" /> parameter is in an invalid format.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Microsoft Windows 2000 or later.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.Security.AccessControl.PrivilegeNotHeldException">The current system account does not have administrative privileges.</exception>
		/// <exception cref="T:System.SystemException">The directory could not be found.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="name" /> parameter specified a directory that is read-only.  
		///  -or-  
		///  This operation is not supported on the current platform.  
		///  -or-  
		///  The caller does not have the required permission.</exception>
		// Token: 0x06001F5D RID: 8029 RVA: 0x0006DB70 File Offset: 0x0006BD70
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		public DirectorySecurity(string name, AccessControlSections includeSections)
			: base(true, name, includeSections, true)
		{
			string fullPathInternal = Path.GetFullPathInternal(name);
			FileIOPermission.QuickDemand(FileIOPermissionAccess.NoAccess, AccessControlActions.View, fullPathInternal, false, false);
		}
	}
}
