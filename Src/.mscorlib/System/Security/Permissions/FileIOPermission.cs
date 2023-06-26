using System;
using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.Security.Util;

namespace System.Security.Permissions
{
	/// <summary>Controls the ability to access files and folders. This class cannot be inherited.</summary>
	// Token: 0x020002E0 RID: 736
	[ComVisible(true)]
	[Serializable]
	public sealed class FileIOPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.FileIOPermission" /> class with fully restricted or unrestricted permission as specified.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> enumeration values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />.</exception>
		// Token: 0x060025F2 RID: 9714 RVA: 0x0008B6D8 File Offset: 0x000898D8
		public FileIOPermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				this.m_unrestricted = true;
				return;
			}
			if (state == PermissionState.None)
			{
				this.m_unrestricted = false;
				return;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.FileIOPermission" /> class with the specified access to the designated file or directory.</summary>
		/// <param name="access">A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> enumeration values.</param>
		/// <param name="path">The absolute path of the file or directory.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.  
		///  -or-  
		///  The <paramref name="path" /> parameter is not a valid string.  
		///  -or-  
		///  The <paramref name="path" /> parameter does not specify the absolute path to the file or directory.</exception>
		// Token: 0x060025F3 RID: 9715 RVA: 0x0008B708 File Offset: 0x00089908
		[SecuritySafeCritical]
		public FileIOPermission(FileIOPermissionAccess access, string path)
		{
			FileIOPermission.VerifyAccess(access);
			string[] array = new string[] { path };
			this.AddPathList(access, array, false, true, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.FileIOPermission" /> class with the specified access to the designated files and directories.</summary>
		/// <param name="access">A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> enumeration values.</param>
		/// <param name="pathList">An array containing the absolute paths of the files and directories.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.  
		///  -or-  
		///  An entry in the <paramref name="pathList" /> array is not a valid string.</exception>
		// Token: 0x060025F4 RID: 9716 RVA: 0x0008B737 File Offset: 0x00089937
		[SecuritySafeCritical]
		public FileIOPermission(FileIOPermissionAccess access, string[] pathList)
		{
			FileIOPermission.VerifyAccess(access);
			this.AddPathList(access, pathList, false, true, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.FileIOPermission" /> class with the specified access to the designated file or directory and the specified access rights to file control information.</summary>
		/// <param name="access">A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> enumeration values.</param>
		/// <param name="control">A bitwise combination of the <see cref="T:System.Security.AccessControl.AccessControlActions" />  enumeration values.</param>
		/// <param name="path">The absolute path of the file or directory.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.  
		///  -or-  
		///  The <paramref name="path" /> parameter is not a valid string.  
		///  -or-  
		///  The <paramref name="path" /> parameter does not specify the absolute path to the file or directory.</exception>
		// Token: 0x060025F5 RID: 9717 RVA: 0x0008B750 File Offset: 0x00089950
		[SecuritySafeCritical]
		public FileIOPermission(FileIOPermissionAccess access, AccessControlActions control, string path)
		{
			FileIOPermission.VerifyAccess(access);
			string[] array = new string[] { path };
			this.AddPathList(access, control, array, false, true, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.FileIOPermission" /> class with the specified access to the designated files and directories and the specified access rights to file control information.</summary>
		/// <param name="access">A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> enumeration values.</param>
		/// <param name="control">A bitwise combination of the <see cref="T:System.Security.AccessControl.AccessControlActions" />  enumeration values.</param>
		/// <param name="pathList">An array containing the absolute paths of the files and directories.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.  
		///  -or-  
		///  An entry in the <paramref name="pathList" /> array is not a valid string.</exception>
		// Token: 0x060025F6 RID: 9718 RVA: 0x0008B780 File Offset: 0x00089980
		[SecuritySafeCritical]
		public FileIOPermission(FileIOPermissionAccess access, AccessControlActions control, string[] pathList)
			: this(access, control, pathList, true, true)
		{
		}

		// Token: 0x060025F7 RID: 9719 RVA: 0x0008B78D File Offset: 0x0008998D
		[SecurityCritical]
		internal FileIOPermission(FileIOPermissionAccess access, string[] pathList, bool checkForDuplicates, bool needFullPath)
		{
			FileIOPermission.VerifyAccess(access);
			this.AddPathList(access, pathList, checkForDuplicates, needFullPath, true);
		}

		// Token: 0x060025F8 RID: 9720 RVA: 0x0008B7A7 File Offset: 0x000899A7
		[SecurityCritical]
		internal FileIOPermission(FileIOPermissionAccess access, AccessControlActions control, string[] pathList, bool checkForDuplicates, bool needFullPath)
		{
			FileIOPermission.VerifyAccess(access);
			this.AddPathList(access, control, pathList, checkForDuplicates, needFullPath, true);
		}

		/// <summary>Sets the specified access to the specified file or directory, replacing the existing state of the permission.</summary>
		/// <param name="access">A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> values.</param>
		/// <param name="path">The absolute path of the file or directory.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.  
		///  -or-  
		///  The <paramref name="path" /> parameter is not a valid string.  
		///  -or-  
		///  The <paramref name="path" /> parameter did not specify the absolute path to the file or directory.</exception>
		// Token: 0x060025F9 RID: 9721 RVA: 0x0008B7C4 File Offset: 0x000899C4
		public void SetPathList(FileIOPermissionAccess access, string path)
		{
			string[] array;
			if (path == null)
			{
				array = new string[0];
			}
			else
			{
				array = new string[] { path };
			}
			this.SetPathList(access, array, false);
		}

		/// <summary>Sets the specified access to the specified files and directories, replacing the current state for the specified access with the new set of paths.</summary>
		/// <param name="access">A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> values.</param>
		/// <param name="pathList">An array containing the absolute paths of the files and directories.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.  
		///  -or-  
		///  An entry in the <paramref name="pathList" /> parameter is not a valid string.</exception>
		// Token: 0x060025FA RID: 9722 RVA: 0x0008B7F1 File Offset: 0x000899F1
		public void SetPathList(FileIOPermissionAccess access, string[] pathList)
		{
			this.SetPathList(access, pathList, true);
		}

		// Token: 0x060025FB RID: 9723 RVA: 0x0008B7FC File Offset: 0x000899FC
		internal void SetPathList(FileIOPermissionAccess access, string[] pathList, bool checkForDuplicates)
		{
			this.SetPathList(access, AccessControlActions.None, pathList, checkForDuplicates);
		}

		// Token: 0x060025FC RID: 9724 RVA: 0x0008B808 File Offset: 0x00089A08
		[SecuritySafeCritical]
		internal void SetPathList(FileIOPermissionAccess access, AccessControlActions control, string[] pathList, bool checkForDuplicates)
		{
			FileIOPermission.VerifyAccess(access);
			if ((access & FileIOPermissionAccess.Read) != FileIOPermissionAccess.NoAccess)
			{
				this.m_read = null;
			}
			if ((access & FileIOPermissionAccess.Write) != FileIOPermissionAccess.NoAccess)
			{
				this.m_write = null;
			}
			if ((access & FileIOPermissionAccess.Append) != FileIOPermissionAccess.NoAccess)
			{
				this.m_append = null;
			}
			if ((access & FileIOPermissionAccess.PathDiscovery) != FileIOPermissionAccess.NoAccess)
			{
				this.m_pathDiscovery = null;
			}
			if ((control & AccessControlActions.View) != AccessControlActions.None)
			{
				this.m_viewAcl = null;
			}
			if ((control & AccessControlActions.Change) != AccessControlActions.None)
			{
				this.m_changeAcl = null;
			}
			this.m_unrestricted = false;
			this.AddPathList(access, control, pathList, checkForDuplicates, true, true);
		}

		/// <summary>Adds access for the specified file or directory to the existing state of the permission.</summary>
		/// <param name="access">A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> values.</param>
		/// <param name="path">The absolute path of a file or directory.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.  
		///  -or-  
		///  The <paramref name="path" /> parameter is not a valid string.  
		///  -or-  
		///  The <paramref name="path" /> parameter did not specify the absolute path to the file or directory.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="path" /> parameter has an invalid format.</exception>
		// Token: 0x060025FD RID: 9725 RVA: 0x0008B878 File Offset: 0x00089A78
		[SecuritySafeCritical]
		public void AddPathList(FileIOPermissionAccess access, string path)
		{
			string[] array;
			if (path == null)
			{
				array = new string[0];
			}
			else
			{
				array = new string[] { path };
			}
			this.AddPathList(access, array, false, true, false);
		}

		/// <summary>Adds access for the specified files and directories to the existing state of the permission.</summary>
		/// <param name="access">A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> values.</param>
		/// <param name="pathList">An array containing the absolute paths of the files and directories.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.  
		///  -or-  
		///  An entry in the <paramref name="pathList" /> array is not valid.</exception>
		/// <exception cref="T:System.NotSupportedException">An entry in the <paramref name="pathList" /> array has an invalid format.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="pathList" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060025FE RID: 9726 RVA: 0x0008B8A7 File Offset: 0x00089AA7
		[SecuritySafeCritical]
		public void AddPathList(FileIOPermissionAccess access, string[] pathList)
		{
			this.AddPathList(access, pathList, true, true, true);
		}

		// Token: 0x060025FF RID: 9727 RVA: 0x0008B8B4 File Offset: 0x00089AB4
		[SecurityCritical]
		internal void AddPathList(FileIOPermissionAccess access, string[] pathListOrig, bool checkForDuplicates, bool needFullPath, bool copyPathList)
		{
			this.AddPathList(access, AccessControlActions.None, pathListOrig, checkForDuplicates, needFullPath, copyPathList);
		}

		// Token: 0x06002600 RID: 9728 RVA: 0x0008B8C4 File Offset: 0x00089AC4
		[SecurityCritical]
		internal void AddPathList(FileIOPermissionAccess access, AccessControlActions control, string[] pathListOrig, bool checkForDuplicates, bool needFullPath, bool copyPathList)
		{
			if (pathListOrig == null)
			{
				throw new ArgumentNullException("pathList");
			}
			if (pathListOrig.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			FileIOPermission.VerifyAccess(access);
			if (this.m_unrestricted)
			{
				return;
			}
			string[] array = pathListOrig;
			if (copyPathList)
			{
				array = new string[pathListOrig.Length];
				Array.Copy(pathListOrig, array, pathListOrig.Length);
			}
			FileIOPermission.CheckIllegalCharacters(array, needFullPath);
			ArrayList arrayList = StringExpressionSet.CreateListFromExpressions(array, needFullPath);
			if ((access & FileIOPermissionAccess.Read) != FileIOPermissionAccess.NoAccess)
			{
				if (this.m_read == null)
				{
					this.m_read = new FileIOAccess();
				}
				this.m_read.AddExpressions(arrayList, checkForDuplicates);
			}
			if ((access & FileIOPermissionAccess.Write) != FileIOPermissionAccess.NoAccess)
			{
				if (this.m_write == null)
				{
					this.m_write = new FileIOAccess();
				}
				this.m_write.AddExpressions(arrayList, checkForDuplicates);
			}
			if ((access & FileIOPermissionAccess.Append) != FileIOPermissionAccess.NoAccess)
			{
				if (this.m_append == null)
				{
					this.m_append = new FileIOAccess();
				}
				this.m_append.AddExpressions(arrayList, checkForDuplicates);
			}
			if ((access & FileIOPermissionAccess.PathDiscovery) != FileIOPermissionAccess.NoAccess)
			{
				if (this.m_pathDiscovery == null)
				{
					this.m_pathDiscovery = new FileIOAccess(true);
				}
				this.m_pathDiscovery.AddExpressions(arrayList, checkForDuplicates);
			}
			if ((control & AccessControlActions.View) != AccessControlActions.None)
			{
				if (this.m_viewAcl == null)
				{
					this.m_viewAcl = new FileIOAccess();
				}
				this.m_viewAcl.AddExpressions(arrayList, checkForDuplicates);
			}
			if ((control & AccessControlActions.Change) != AccessControlActions.None)
			{
				if (this.m_changeAcl == null)
				{
					this.m_changeAcl = new FileIOAccess();
				}
				this.m_changeAcl.AddExpressions(arrayList, checkForDuplicates);
			}
		}

		/// <summary>Gets all files and directories with the specified <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.</summary>
		/// <param name="access">One of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> values that represents a single type of file access.</param>
		/// <returns>An array containing the paths of the files and directories to which access specified by the <paramref name="access" /> parameter is granted.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="access" /> is not a valid value of <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.  
		/// -or-  
		/// <paramref name="access" /> is <see cref="F:System.Security.Permissions.FileIOPermissionAccess.AllAccess" />, which represents more than one type of file access, or <see cref="F:System.Security.Permissions.FileIOPermissionAccess.NoAccess" />, which does not represent any type of file access.</exception>
		// Token: 0x06002601 RID: 9729 RVA: 0x0008BA14 File Offset: 0x00089C14
		[SecuritySafeCritical]
		public string[] GetPathList(FileIOPermissionAccess access)
		{
			FileIOPermission.VerifyAccess(access);
			FileIOPermission.ExclusiveAccess(access);
			if (FileIOPermission.AccessIsSet(access, FileIOPermissionAccess.Read))
			{
				if (this.m_read == null)
				{
					return null;
				}
				return this.m_read.ToStringArray();
			}
			else if (FileIOPermission.AccessIsSet(access, FileIOPermissionAccess.Write))
			{
				if (this.m_write == null)
				{
					return null;
				}
				return this.m_write.ToStringArray();
			}
			else if (FileIOPermission.AccessIsSet(access, FileIOPermissionAccess.Append))
			{
				if (this.m_append == null)
				{
					return null;
				}
				return this.m_append.ToStringArray();
			}
			else
			{
				if (!FileIOPermission.AccessIsSet(access, FileIOPermissionAccess.PathDiscovery))
				{
					return null;
				}
				if (this.m_pathDiscovery == null)
				{
					return null;
				}
				return this.m_pathDiscovery.ToStringArray();
			}
		}

		/// <summary>Gets or sets the permitted access to all local files.</summary>
		/// <returns>The set of file I/O flags for all local files.</returns>
		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06002602 RID: 9730 RVA: 0x0008BAAC File Offset: 0x00089CAC
		// (set) Token: 0x06002603 RID: 9731 RVA: 0x0008BB2C File Offset: 0x00089D2C
		public FileIOPermissionAccess AllLocalFiles
		{
			get
			{
				if (this.m_unrestricted)
				{
					return FileIOPermissionAccess.AllAccess;
				}
				FileIOPermissionAccess fileIOPermissionAccess = FileIOPermissionAccess.NoAccess;
				if (this.m_read != null && this.m_read.AllLocalFiles)
				{
					fileIOPermissionAccess |= FileIOPermissionAccess.Read;
				}
				if (this.m_write != null && this.m_write.AllLocalFiles)
				{
					fileIOPermissionAccess |= FileIOPermissionAccess.Write;
				}
				if (this.m_append != null && this.m_append.AllLocalFiles)
				{
					fileIOPermissionAccess |= FileIOPermissionAccess.Append;
				}
				if (this.m_pathDiscovery != null && this.m_pathDiscovery.AllLocalFiles)
				{
					fileIOPermissionAccess |= FileIOPermissionAccess.PathDiscovery;
				}
				return fileIOPermissionAccess;
			}
			set
			{
				if ((value & FileIOPermissionAccess.Read) != FileIOPermissionAccess.NoAccess)
				{
					if (this.m_read == null)
					{
						this.m_read = new FileIOAccess();
					}
					this.m_read.AllLocalFiles = true;
				}
				else if (this.m_read != null)
				{
					this.m_read.AllLocalFiles = false;
				}
				if ((value & FileIOPermissionAccess.Write) != FileIOPermissionAccess.NoAccess)
				{
					if (this.m_write == null)
					{
						this.m_write = new FileIOAccess();
					}
					this.m_write.AllLocalFiles = true;
				}
				else if (this.m_write != null)
				{
					this.m_write.AllLocalFiles = false;
				}
				if ((value & FileIOPermissionAccess.Append) != FileIOPermissionAccess.NoAccess)
				{
					if (this.m_append == null)
					{
						this.m_append = new FileIOAccess();
					}
					this.m_append.AllLocalFiles = true;
				}
				else if (this.m_append != null)
				{
					this.m_append.AllLocalFiles = false;
				}
				if ((value & FileIOPermissionAccess.PathDiscovery) != FileIOPermissionAccess.NoAccess)
				{
					if (this.m_pathDiscovery == null)
					{
						this.m_pathDiscovery = new FileIOAccess(true);
					}
					this.m_pathDiscovery.AllLocalFiles = true;
					return;
				}
				if (this.m_pathDiscovery != null)
				{
					this.m_pathDiscovery.AllLocalFiles = false;
				}
			}
		}

		/// <summary>Gets or sets the permitted access to all files.</summary>
		/// <returns>The set of file I/O flags for all files.</returns>
		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06002604 RID: 9732 RVA: 0x0008BC24 File Offset: 0x00089E24
		// (set) Token: 0x06002605 RID: 9733 RVA: 0x0008BCA4 File Offset: 0x00089EA4
		public FileIOPermissionAccess AllFiles
		{
			get
			{
				if (this.m_unrestricted)
				{
					return FileIOPermissionAccess.AllAccess;
				}
				FileIOPermissionAccess fileIOPermissionAccess = FileIOPermissionAccess.NoAccess;
				if (this.m_read != null && this.m_read.AllFiles)
				{
					fileIOPermissionAccess |= FileIOPermissionAccess.Read;
				}
				if (this.m_write != null && this.m_write.AllFiles)
				{
					fileIOPermissionAccess |= FileIOPermissionAccess.Write;
				}
				if (this.m_append != null && this.m_append.AllFiles)
				{
					fileIOPermissionAccess |= FileIOPermissionAccess.Append;
				}
				if (this.m_pathDiscovery != null && this.m_pathDiscovery.AllFiles)
				{
					fileIOPermissionAccess |= FileIOPermissionAccess.PathDiscovery;
				}
				return fileIOPermissionAccess;
			}
			set
			{
				if (value == FileIOPermissionAccess.AllAccess)
				{
					this.m_unrestricted = true;
					return;
				}
				if ((value & FileIOPermissionAccess.Read) != FileIOPermissionAccess.NoAccess)
				{
					if (this.m_read == null)
					{
						this.m_read = new FileIOAccess();
					}
					this.m_read.AllFiles = true;
				}
				else if (this.m_read != null)
				{
					this.m_read.AllFiles = false;
				}
				if ((value & FileIOPermissionAccess.Write) != FileIOPermissionAccess.NoAccess)
				{
					if (this.m_write == null)
					{
						this.m_write = new FileIOAccess();
					}
					this.m_write.AllFiles = true;
				}
				else if (this.m_write != null)
				{
					this.m_write.AllFiles = false;
				}
				if ((value & FileIOPermissionAccess.Append) != FileIOPermissionAccess.NoAccess)
				{
					if (this.m_append == null)
					{
						this.m_append = new FileIOAccess();
					}
					this.m_append.AllFiles = true;
				}
				else if (this.m_append != null)
				{
					this.m_append.AllFiles = false;
				}
				if ((value & FileIOPermissionAccess.PathDiscovery) != FileIOPermissionAccess.NoAccess)
				{
					if (this.m_pathDiscovery == null)
					{
						this.m_pathDiscovery = new FileIOAccess(true);
					}
					this.m_pathDiscovery.AllFiles = true;
					return;
				}
				if (this.m_pathDiscovery != null)
				{
					this.m_pathDiscovery.AllFiles = false;
				}
			}
		}

		// Token: 0x06002606 RID: 9734 RVA: 0x0008BDA6 File Offset: 0x00089FA6
		private static void VerifyAccess(FileIOPermissionAccess access)
		{
			if ((access & ~(FileIOPermissionAccess.Read | FileIOPermissionAccess.Write | FileIOPermissionAccess.Append | FileIOPermissionAccess.PathDiscovery)) != FileIOPermissionAccess.NoAccess)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[] { (int)access }));
			}
		}

		// Token: 0x06002607 RID: 9735 RVA: 0x0008BDCD File Offset: 0x00089FCD
		private static void ExclusiveAccess(FileIOPermissionAccess access)
		{
			if (access == FileIOPermissionAccess.NoAccess)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumNotSingleFlag"));
			}
			if ((access & (access - 1)) != FileIOPermissionAccess.NoAccess)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumNotSingleFlag"));
			}
		}

		// Token: 0x06002608 RID: 9736 RVA: 0x0008BDFC File Offset: 0x00089FFC
		private static void CheckIllegalCharacters(string[] str, bool onlyCheckExtras)
		{
			for (int i = 0; i < str.Length; i++)
			{
				if (str[i] == null)
				{
					throw new ArgumentNullException("path");
				}
				if (FileIOPermission.CheckExtraPathCharacters(str[i]))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPathChars"));
				}
				if (!onlyCheckExtras)
				{
					Path.CheckInvalidPathChars(str[i], false);
				}
			}
		}

		// Token: 0x06002609 RID: 9737 RVA: 0x0008BE50 File Offset: 0x0008A050
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool CheckExtraPathCharacters(string path)
		{
			bool flag = CodeAccessSecurityEngine.QuickCheckForAllDemands() && !AppContextSwitches.UseLegacyPathHandling;
			int num = ((!flag) ? 0 : (PathInternal.IsDevice(path) ? 4 : 0));
			for (int i = num; i < path.Length; i++)
			{
				char c = path[i];
				if (c == '*' || c == '?' || c == '\0')
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600260A RID: 9738 RVA: 0x0008BEAC File Offset: 0x0008A0AC
		private static bool AccessIsSet(FileIOPermissionAccess access, FileIOPermissionAccess question)
		{
			return (access & question) > FileIOPermissionAccess.NoAccess;
		}

		// Token: 0x0600260B RID: 9739 RVA: 0x0008BEB4 File Offset: 0x0008A0B4
		private bool IsEmpty()
		{
			return !this.m_unrestricted && (this.m_read == null || this.m_read.IsEmpty()) && (this.m_write == null || this.m_write.IsEmpty()) && (this.m_append == null || this.m_append.IsEmpty()) && (this.m_pathDiscovery == null || this.m_pathDiscovery.IsEmpty()) && (this.m_viewAcl == null || this.m_viewAcl.IsEmpty()) && (this.m_changeAcl == null || this.m_changeAcl.IsEmpty());
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>
		///   <see langword="true" /> if the current permission is unrestricted; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600260C RID: 9740 RVA: 0x0008BF49 File Offset: 0x0008A149
		public bool IsUnrestricted()
		{
			return this.m_unrestricted;
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be the same type as the current permission.</param>
		/// <returns>
		///   <see langword="true" /> if the current permission is a subset of the specified permission; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x0600260D RID: 9741 RVA: 0x0008BF54 File Offset: 0x0008A154
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.IsEmpty();
			}
			FileIOPermission fileIOPermission = target as FileIOPermission;
			if (fileIOPermission == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[] { base.GetType().FullName }));
			}
			return fileIOPermission.IsUnrestricted() || (!this.IsUnrestricted() && ((this.m_read == null || this.m_read.IsSubsetOf(fileIOPermission.m_read)) && (this.m_write == null || this.m_write.IsSubsetOf(fileIOPermission.m_write)) && (this.m_append == null || this.m_append.IsSubsetOf(fileIOPermission.m_append)) && (this.m_pathDiscovery == null || this.m_pathDiscovery.IsSubsetOf(fileIOPermission.m_pathDiscovery)) && (this.m_viewAcl == null || this.m_viewAcl.IsSubsetOf(fileIOPermission.m_viewAcl))) && (this.m_changeAcl == null || this.m_changeAcl.IsSubsetOf(fileIOPermission.m_changeAcl)));
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to intersect with the current permission. It must be the same type as the current permission.</param>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is <see langword="null" /> if the intersection is empty.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x0600260E RID: 9742 RVA: 0x0008C054 File Offset: 0x0008A254
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			FileIOPermission fileIOPermission = target as FileIOPermission;
			if (fileIOPermission == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[] { base.GetType().FullName }));
			}
			if (this.IsUnrestricted())
			{
				return target.Copy();
			}
			if (fileIOPermission.IsUnrestricted())
			{
				return this.Copy();
			}
			FileIOAccess fileIOAccess = ((this.m_read == null) ? null : this.m_read.Intersect(fileIOPermission.m_read));
			FileIOAccess fileIOAccess2 = ((this.m_write == null) ? null : this.m_write.Intersect(fileIOPermission.m_write));
			FileIOAccess fileIOAccess3 = ((this.m_append == null) ? null : this.m_append.Intersect(fileIOPermission.m_append));
			FileIOAccess fileIOAccess4 = ((this.m_pathDiscovery == null) ? null : this.m_pathDiscovery.Intersect(fileIOPermission.m_pathDiscovery));
			FileIOAccess fileIOAccess5 = ((this.m_viewAcl == null) ? null : this.m_viewAcl.Intersect(fileIOPermission.m_viewAcl));
			FileIOAccess fileIOAccess6 = ((this.m_changeAcl == null) ? null : this.m_changeAcl.Intersect(fileIOPermission.m_changeAcl));
			if ((fileIOAccess == null || fileIOAccess.IsEmpty()) && (fileIOAccess2 == null || fileIOAccess2.IsEmpty()) && (fileIOAccess3 == null || fileIOAccess3.IsEmpty()) && (fileIOAccess4 == null || fileIOAccess4.IsEmpty()) && (fileIOAccess5 == null || fileIOAccess5.IsEmpty()) && (fileIOAccess6 == null || fileIOAccess6.IsEmpty()))
			{
				return null;
			}
			return new FileIOPermission(PermissionState.None)
			{
				m_unrestricted = false,
				m_read = fileIOAccess,
				m_write = fileIOAccess2,
				m_append = fileIOAccess3,
				m_pathDiscovery = fileIOAccess4,
				m_viewAcl = fileIOAccess5,
				m_changeAcl = fileIOAccess6
			};
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <param name="other">A permission to combine with the current permission. It must be the same type as the current permission.</param>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="other" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x0600260F RID: 9743 RVA: 0x0008C1F4 File Offset: 0x0008A3F4
		public override IPermission Union(IPermission other)
		{
			if (other == null)
			{
				return this.Copy();
			}
			FileIOPermission fileIOPermission = other as FileIOPermission;
			if (fileIOPermission == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[] { base.GetType().FullName }));
			}
			if (this.IsUnrestricted() || fileIOPermission.IsUnrestricted())
			{
				return new FileIOPermission(PermissionState.Unrestricted);
			}
			FileIOAccess fileIOAccess = ((this.m_read == null) ? fileIOPermission.m_read : this.m_read.Union(fileIOPermission.m_read));
			FileIOAccess fileIOAccess2 = ((this.m_write == null) ? fileIOPermission.m_write : this.m_write.Union(fileIOPermission.m_write));
			FileIOAccess fileIOAccess3 = ((this.m_append == null) ? fileIOPermission.m_append : this.m_append.Union(fileIOPermission.m_append));
			FileIOAccess fileIOAccess4 = ((this.m_pathDiscovery == null) ? fileIOPermission.m_pathDiscovery : this.m_pathDiscovery.Union(fileIOPermission.m_pathDiscovery));
			FileIOAccess fileIOAccess5 = ((this.m_viewAcl == null) ? fileIOPermission.m_viewAcl : this.m_viewAcl.Union(fileIOPermission.m_viewAcl));
			FileIOAccess fileIOAccess6 = ((this.m_changeAcl == null) ? fileIOPermission.m_changeAcl : this.m_changeAcl.Union(fileIOPermission.m_changeAcl));
			if ((fileIOAccess == null || fileIOAccess.IsEmpty()) && (fileIOAccess2 == null || fileIOAccess2.IsEmpty()) && (fileIOAccess3 == null || fileIOAccess3.IsEmpty()) && (fileIOAccess4 == null || fileIOAccess4.IsEmpty()) && (fileIOAccess5 == null || fileIOAccess5.IsEmpty()) && (fileIOAccess6 == null || fileIOAccess6.IsEmpty()))
			{
				return null;
			}
			return new FileIOPermission(PermissionState.None)
			{
				m_unrestricted = false,
				m_read = fileIOAccess,
				m_write = fileIOAccess2,
				m_append = fileIOAccess3,
				m_pathDiscovery = fileIOAccess4,
				m_viewAcl = fileIOAccess5,
				m_changeAcl = fileIOAccess6
			};
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06002610 RID: 9744 RVA: 0x0008C3B0 File Offset: 0x0008A5B0
		public override IPermission Copy()
		{
			FileIOPermission fileIOPermission = new FileIOPermission(PermissionState.None);
			if (this.m_unrestricted)
			{
				fileIOPermission.m_unrestricted = true;
			}
			else
			{
				fileIOPermission.m_unrestricted = false;
				if (this.m_read != null)
				{
					fileIOPermission.m_read = this.m_read.Copy();
				}
				if (this.m_write != null)
				{
					fileIOPermission.m_write = this.m_write.Copy();
				}
				if (this.m_append != null)
				{
					fileIOPermission.m_append = this.m_append.Copy();
				}
				if (this.m_pathDiscovery != null)
				{
					fileIOPermission.m_pathDiscovery = this.m_pathDiscovery.Copy();
				}
				if (this.m_viewAcl != null)
				{
					fileIOPermission.m_viewAcl = this.m_viewAcl.Copy();
				}
				if (this.m_changeAcl != null)
				{
					fileIOPermission.m_changeAcl = this.m_changeAcl.Copy();
				}
			}
			return fileIOPermission;
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x06002611 RID: 9745 RVA: 0x0008C478 File Offset: 0x0008A678
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = CodeAccessPermission.CreatePermissionElement(this, "System.Security.Permissions.FileIOPermission");
			if (!this.IsUnrestricted())
			{
				if (this.m_read != null && !this.m_read.IsEmpty())
				{
					securityElement.AddAttribute("Read", SecurityElement.Escape(this.m_read.ToString()));
				}
				if (this.m_write != null && !this.m_write.IsEmpty())
				{
					securityElement.AddAttribute("Write", SecurityElement.Escape(this.m_write.ToString()));
				}
				if (this.m_append != null && !this.m_append.IsEmpty())
				{
					securityElement.AddAttribute("Append", SecurityElement.Escape(this.m_append.ToString()));
				}
				if (this.m_pathDiscovery != null && !this.m_pathDiscovery.IsEmpty())
				{
					securityElement.AddAttribute("PathDiscovery", SecurityElement.Escape(this.m_pathDiscovery.ToString()));
				}
				if (this.m_viewAcl != null && !this.m_viewAcl.IsEmpty())
				{
					securityElement.AddAttribute("ViewAcl", SecurityElement.Escape(this.m_viewAcl.ToString()));
				}
				if (this.m_changeAcl != null && !this.m_changeAcl.IsEmpty())
				{
					securityElement.AddAttribute("ChangeAcl", SecurityElement.Escape(this.m_changeAcl.ToString()));
				}
			}
			else
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="esd">The XML encoding used to reconstruct the permission.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="esd" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="esd" /> parameter is not a valid permission element.  
		///  -or-  
		///  The <paramref name="esd" /> parameter's version number is not compatible.</exception>
		// Token: 0x06002612 RID: 9746 RVA: 0x0008C5D0 File Offset: 0x0008A7D0
		[SecuritySafeCritical]
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.ValidateElement(esd, this);
			if (XMLUtil.IsUnrestricted(esd))
			{
				this.m_unrestricted = true;
				return;
			}
			this.m_unrestricted = false;
			string text = esd.Attribute("Read");
			if (text != null)
			{
				this.m_read = new FileIOAccess(text);
			}
			else
			{
				this.m_read = null;
			}
			text = esd.Attribute("Write");
			if (text != null)
			{
				this.m_write = new FileIOAccess(text);
			}
			else
			{
				this.m_write = null;
			}
			text = esd.Attribute("Append");
			if (text != null)
			{
				this.m_append = new FileIOAccess(text);
			}
			else
			{
				this.m_append = null;
			}
			text = esd.Attribute("PathDiscovery");
			if (text != null)
			{
				this.m_pathDiscovery = new FileIOAccess(text);
				this.m_pathDiscovery.PathDiscovery = true;
			}
			else
			{
				this.m_pathDiscovery = null;
			}
			text = esd.Attribute("ViewAcl");
			if (text != null)
			{
				this.m_viewAcl = new FileIOAccess(text);
			}
			else
			{
				this.m_viewAcl = null;
			}
			text = esd.Attribute("ChangeAcl");
			if (text != null)
			{
				this.m_changeAcl = new FileIOAccess(text);
				return;
			}
			this.m_changeAcl = null;
		}

		// Token: 0x06002613 RID: 9747 RVA: 0x0008C6DE File Offset: 0x0008A8DE
		int IBuiltInPermission.GetTokenIndex()
		{
			return FileIOPermission.GetTokenIndex();
		}

		// Token: 0x06002614 RID: 9748 RVA: 0x0008C6E5 File Offset: 0x0008A8E5
		internal static int GetTokenIndex()
		{
			return 2;
		}

		/// <summary>Determines whether the specified <see cref="T:System.Security.Permissions.FileIOPermission" /> object is equal to the current <see cref="T:System.Security.Permissions.FileIOPermission" />.</summary>
		/// <param name="obj">The <see cref="T:System.Security.Permissions.FileIOPermission" /> object to compare with the current <see cref="T:System.Security.Permissions.FileIOPermission" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Security.Permissions.FileIOPermission" /> is equal to the current <see cref="T:System.Security.Permissions.FileIOPermission" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002615 RID: 9749 RVA: 0x0008C6E8 File Offset: 0x0008A8E8
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			FileIOPermission fileIOPermission = obj as FileIOPermission;
			if (fileIOPermission == null)
			{
				return false;
			}
			if (this.m_unrestricted && fileIOPermission.m_unrestricted)
			{
				return true;
			}
			if (this.m_unrestricted != fileIOPermission.m_unrestricted)
			{
				return false;
			}
			if (this.m_read == null)
			{
				if (fileIOPermission.m_read != null && !fileIOPermission.m_read.IsEmpty())
				{
					return false;
				}
			}
			else if (!this.m_read.Equals(fileIOPermission.m_read))
			{
				return false;
			}
			if (this.m_write == null)
			{
				if (fileIOPermission.m_write != null && !fileIOPermission.m_write.IsEmpty())
				{
					return false;
				}
			}
			else if (!this.m_write.Equals(fileIOPermission.m_write))
			{
				return false;
			}
			if (this.m_append == null)
			{
				if (fileIOPermission.m_append != null && !fileIOPermission.m_append.IsEmpty())
				{
					return false;
				}
			}
			else if (!this.m_append.Equals(fileIOPermission.m_append))
			{
				return false;
			}
			if (this.m_pathDiscovery == null)
			{
				if (fileIOPermission.m_pathDiscovery != null && !fileIOPermission.m_pathDiscovery.IsEmpty())
				{
					return false;
				}
			}
			else if (!this.m_pathDiscovery.Equals(fileIOPermission.m_pathDiscovery))
			{
				return false;
			}
			if (this.m_viewAcl == null)
			{
				if (fileIOPermission.m_viewAcl != null && !fileIOPermission.m_viewAcl.IsEmpty())
				{
					return false;
				}
			}
			else if (!this.m_viewAcl.Equals(fileIOPermission.m_viewAcl))
			{
				return false;
			}
			if (this.m_changeAcl == null)
			{
				if (fileIOPermission.m_changeAcl != null && !fileIOPermission.m_changeAcl.IsEmpty())
				{
					return false;
				}
			}
			else if (!this.m_changeAcl.Equals(fileIOPermission.m_changeAcl))
			{
				return false;
			}
			return true;
		}

		/// <summary>Gets a hash code for the <see cref="T:System.Security.Permissions.FileIOPermission" /> object that is suitable for use in hashing algorithms and data structures such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Security.Permissions.FileIOPermission" /> object.</returns>
		// Token: 0x06002616 RID: 9750 RVA: 0x0008C85C File Offset: 0x0008AA5C
		[ComVisible(false)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06002617 RID: 9751 RVA: 0x0008C864 File Offset: 0x0008AA64
		[SecuritySafeCritical]
		internal static void QuickDemand(FileIOPermissionAccess access, string fullPath, bool checkForDuplicates = false, bool needFullPath = true)
		{
			if (!CodeAccessSecurityEngine.QuickCheckForAllDemands())
			{
				new FileIOPermission(access, new string[] { fullPath }, checkForDuplicates, needFullPath).Demand();
				return;
			}
			FileIOPermission.EmulateFileIOPermissionChecks(fullPath);
		}

		// Token: 0x06002618 RID: 9752 RVA: 0x0008C88C File Offset: 0x0008AA8C
		[SecuritySafeCritical]
		internal static void QuickDemand(FileIOPermissionAccess access, string[] fullPathList, bool checkForDuplicates = false, bool needFullPath = true)
		{
			if (!CodeAccessSecurityEngine.QuickCheckForAllDemands())
			{
				new FileIOPermission(access, fullPathList, checkForDuplicates, needFullPath).Demand();
				return;
			}
			foreach (string text in fullPathList)
			{
				FileIOPermission.EmulateFileIOPermissionChecks(text);
			}
		}

		// Token: 0x06002619 RID: 9753 RVA: 0x0008C8C9 File Offset: 0x0008AAC9
		[SecuritySafeCritical]
		internal static void QuickDemand(PermissionState state)
		{
			if (!CodeAccessSecurityEngine.QuickCheckForAllDemands())
			{
				new FileIOPermission(state).Demand();
			}
		}

		// Token: 0x0600261A RID: 9754 RVA: 0x0008C8DD File Offset: 0x0008AADD
		[SecuritySafeCritical]
		internal static void QuickDemand(FileIOPermissionAccess access, AccessControlActions control, string fullPath, bool checkForDuplicates = false, bool needFullPath = true)
		{
			if (!CodeAccessSecurityEngine.QuickCheckForAllDemands())
			{
				new FileIOPermission(access, control, new string[] { fullPath }, checkForDuplicates, needFullPath).Demand();
				return;
			}
			FileIOPermission.EmulateFileIOPermissionChecks(fullPath);
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x0008C908 File Offset: 0x0008AB08
		[SecuritySafeCritical]
		internal static void QuickDemand(FileIOPermissionAccess access, AccessControlActions control, string[] fullPathList, bool checkForDuplicates = true, bool needFullPath = true)
		{
			if (!CodeAccessSecurityEngine.QuickCheckForAllDemands())
			{
				new FileIOPermission(access, control, fullPathList, checkForDuplicates, needFullPath).Demand();
				return;
			}
			foreach (string text in fullPathList)
			{
				FileIOPermission.EmulateFileIOPermissionChecks(text);
			}
		}

		// Token: 0x0600261C RID: 9756 RVA: 0x0008C948 File Offset: 0x0008AB48
		internal static void EmulateFileIOPermissionChecks(string fullPath)
		{
			if (AppContextSwitches.UseLegacyPathHandling || !PathInternal.IsDevice(fullPath))
			{
				if (PathInternal.HasWildCardCharacters(fullPath))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPathChars"));
				}
				if (PathInternal.HasInvalidVolumeSeparator(fullPath))
				{
					throw new NotSupportedException(Environment.GetResourceString("Argument_PathFormatNotSupported"));
				}
			}
		}

		// Token: 0x04000E8B RID: 3723
		private FileIOAccess m_read;

		// Token: 0x04000E8C RID: 3724
		private FileIOAccess m_write;

		// Token: 0x04000E8D RID: 3725
		private FileIOAccess m_append;

		// Token: 0x04000E8E RID: 3726
		private FileIOAccess m_pathDiscovery;

		// Token: 0x04000E8F RID: 3727
		[OptionalField(VersionAdded = 2)]
		private FileIOAccess m_viewAcl;

		// Token: 0x04000E90 RID: 3728
		[OptionalField(VersionAdded = 2)]
		private FileIOAccess m_changeAcl;

		// Token: 0x04000E91 RID: 3729
		private bool m_unrestricted;
	}
}
