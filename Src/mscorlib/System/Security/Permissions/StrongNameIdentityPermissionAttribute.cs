using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x020002F9 RID: 761
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class StrongNameIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.StrongNameIdentityPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		// Token: 0x060026F7 RID: 9975 RVA: 0x0008E562 File Offset: 0x0008C762
		public StrongNameIdentityPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets the name of the strong name identity.</summary>
		/// <returns>A name to compare against the name specified by the security provider.</returns>
		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060026F8 RID: 9976 RVA: 0x0008E56B File Offset: 0x0008C76B
		// (set) Token: 0x060026F9 RID: 9977 RVA: 0x0008E573 File Offset: 0x0008C773
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		/// <summary>Gets or sets the version of the strong name identity.</summary>
		/// <returns>The version number of the strong name identity.</returns>
		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x060026FA RID: 9978 RVA: 0x0008E57C File Offset: 0x0008C77C
		// (set) Token: 0x060026FB RID: 9979 RVA: 0x0008E584 File Offset: 0x0008C784
		public string Version
		{
			get
			{
				return this.m_version;
			}
			set
			{
				this.m_version = value;
			}
		}

		/// <summary>Gets or sets the public key value of the strong name identity expressed as a hexadecimal string.</summary>
		/// <returns>The public key value of the strong name identity expressed as a hexadecimal string.</returns>
		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x060026FC RID: 9980 RVA: 0x0008E58D File Offset: 0x0008C78D
		// (set) Token: 0x060026FD RID: 9981 RVA: 0x0008E595 File Offset: 0x0008C795
		public string PublicKey
		{
			get
			{
				return this.m_blob;
			}
			set
			{
				this.m_blob = value;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> that corresponds to this attribute.</returns>
		/// <exception cref="T:System.ArgumentException">The method failed because the key is <see langword="null" />.</exception>
		// Token: 0x060026FE RID: 9982 RVA: 0x0008E5A0 File Offset: 0x0008C7A0
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new StrongNameIdentityPermission(PermissionState.Unrestricted);
			}
			if (this.m_blob == null && this.m_name == null && this.m_version == null)
			{
				return new StrongNameIdentityPermission(PermissionState.None);
			}
			if (this.m_blob == null)
			{
				throw new ArgumentException(Environment.GetResourceString("ArgumentNull_Key"));
			}
			StrongNamePublicKeyBlob strongNamePublicKeyBlob = new StrongNamePublicKeyBlob(this.m_blob);
			if (this.m_version == null || this.m_version.Equals(string.Empty))
			{
				return new StrongNameIdentityPermission(strongNamePublicKeyBlob, this.m_name, null);
			}
			return new StrongNameIdentityPermission(strongNamePublicKeyBlob, this.m_name, new Version(this.m_version));
		}

		// Token: 0x04000F0E RID: 3854
		private string m_name;

		// Token: 0x04000F0F RID: 3855
		private string m_version;

		// Token: 0x04000F10 RID: 3856
		private string m_blob;
	}
}
