using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.FileDialogPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x020002F0 RID: 752
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class FileDialogPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.FileDialogPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		// Token: 0x0600267F RID: 9855 RVA: 0x0008DC26 File Offset: 0x0008BE26
		public FileDialogPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets a value indicating whether permission to open files through the file dialog is declared.</summary>
		/// <returns>
		///   <see langword="true" /> if permission to open files through the file dialog is declared; otherwise, <see langword="false" />.</returns>
		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06002680 RID: 9856 RVA: 0x0008DC2F File Offset: 0x0008BE2F
		// (set) Token: 0x06002681 RID: 9857 RVA: 0x0008DC3C File Offset: 0x0008BE3C
		public bool Open
		{
			get
			{
				return (this.m_access & FileDialogPermissionAccess.Open) > FileDialogPermissionAccess.None;
			}
			set
			{
				this.m_access = (value ? (this.m_access | FileDialogPermissionAccess.Open) : (this.m_access & ~FileDialogPermissionAccess.Open));
			}
		}

		/// <summary>Gets or sets a value indicating whether permission to save files through the file dialog is declared.</summary>
		/// <returns>
		///   <see langword="true" /> if permission to save files through the file dialog is declared; otherwise, <see langword="false" />.</returns>
		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06002682 RID: 9858 RVA: 0x0008DC5A File Offset: 0x0008BE5A
		// (set) Token: 0x06002683 RID: 9859 RVA: 0x0008DC67 File Offset: 0x0008BE67
		public bool Save
		{
			get
			{
				return (this.m_access & FileDialogPermissionAccess.Save) > FileDialogPermissionAccess.None;
			}
			set
			{
				this.m_access = (value ? (this.m_access | FileDialogPermissionAccess.Save) : (this.m_access & ~FileDialogPermissionAccess.Save));
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.FileDialogPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.FileDialogPermission" /> that corresponds to this attribute.</returns>
		// Token: 0x06002684 RID: 9860 RVA: 0x0008DC85 File Offset: 0x0008BE85
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new FileDialogPermission(PermissionState.Unrestricted);
			}
			return new FileDialogPermission(this.m_access);
		}

		// Token: 0x04000EF2 RID: 3826
		private FileDialogPermissionAccess m_access;
	}
}
