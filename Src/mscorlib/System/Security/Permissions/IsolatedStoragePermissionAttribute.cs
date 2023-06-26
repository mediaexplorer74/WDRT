using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.IsolatedStoragePermission" /> to be applied to code using declarative security.</summary>
	// Token: 0x020002FD RID: 765
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public abstract class IsolatedStoragePermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.IsolatedStoragePermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		// Token: 0x0600270F RID: 9999 RVA: 0x0008E78E File Offset: 0x0008C98E
		protected IsolatedStoragePermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets the maximum user storage quota size.</summary>
		/// <returns>The maximum user storage quota size in bytes.</returns>
		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06002711 RID: 10001 RVA: 0x0008E7A0 File Offset: 0x0008C9A0
		// (set) Token: 0x06002710 RID: 10000 RVA: 0x0008E797 File Offset: 0x0008C997
		public long UserQuota
		{
			get
			{
				return this.m_userQuota;
			}
			set
			{
				this.m_userQuota = value;
			}
		}

		/// <summary>Gets or sets the level of isolated storage that should be declared.</summary>
		/// <returns>One of the <see cref="T:System.Security.Permissions.IsolatedStorageContainment" /> values.</returns>
		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06002713 RID: 10003 RVA: 0x0008E7B1 File Offset: 0x0008C9B1
		// (set) Token: 0x06002712 RID: 10002 RVA: 0x0008E7A8 File Offset: 0x0008C9A8
		public IsolatedStorageContainment UsageAllowed
		{
			get
			{
				return this.m_allowed;
			}
			set
			{
				this.m_allowed = value;
			}
		}

		// Token: 0x04000F16 RID: 3862
		internal long m_userQuota;

		// Token: 0x04000F17 RID: 3863
		internal IsolatedStorageContainment m_allowed;
	}
}
