using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.IsolatedStorageFilePermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x020002FE RID: 766
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class IsolatedStorageFilePermissionAttribute : IsolatedStoragePermissionAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.IsolatedStorageFilePermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		// Token: 0x06002714 RID: 10004 RVA: 0x0008E7B9 File Offset: 0x0008C9B9
		public IsolatedStorageFilePermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.IsolatedStorageFilePermission" />.</summary>
		/// <returns>An <see cref="T:System.Security.Permissions.IsolatedStorageFilePermission" /> that corresponds to this attribute.</returns>
		// Token: 0x06002715 RID: 10005 RVA: 0x0008E7C4 File Offset: 0x0008C9C4
		public override IPermission CreatePermission()
		{
			IsolatedStorageFilePermission isolatedStorageFilePermission;
			if (this.m_unrestricted)
			{
				isolatedStorageFilePermission = new IsolatedStorageFilePermission(PermissionState.Unrestricted);
			}
			else
			{
				isolatedStorageFilePermission = new IsolatedStorageFilePermission(PermissionState.None);
				isolatedStorageFilePermission.UserQuota = this.m_userQuota;
				isolatedStorageFilePermission.UsageAllowed = this.m_allowed;
			}
			return isolatedStorageFilePermission;
		}
	}
}
