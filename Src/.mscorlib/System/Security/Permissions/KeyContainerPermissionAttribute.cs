using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.KeyContainerPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x020002F2 RID: 754
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class KeyContainerPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.KeyContainerPermissionAttribute" /> class with the specified security action.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		// Token: 0x0600269B RID: 9883 RVA: 0x0008DE60 File Offset: 0x0008C060
		public KeyContainerPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets the name of the key store.</summary>
		/// <returns>The name of the key store. The default is "*".</returns>
		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x0600269C RID: 9884 RVA: 0x0008DE77 File Offset: 0x0008C077
		// (set) Token: 0x0600269D RID: 9885 RVA: 0x0008DE7F File Offset: 0x0008C07F
		public string KeyStore
		{
			get
			{
				return this.m_keyStore;
			}
			set
			{
				this.m_keyStore = value;
			}
		}

		/// <summary>Gets or sets the provider name.</summary>
		/// <returns>The name of the provider.</returns>
		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x0600269E RID: 9886 RVA: 0x0008DE88 File Offset: 0x0008C088
		// (set) Token: 0x0600269F RID: 9887 RVA: 0x0008DE90 File Offset: 0x0008C090
		public string ProviderName
		{
			get
			{
				return this.m_providerName;
			}
			set
			{
				this.m_providerName = value;
			}
		}

		/// <summary>Gets or sets the provider type.</summary>
		/// <returns>One of the PROV_ values defined in the Wincrypt.h header file.</returns>
		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x060026A0 RID: 9888 RVA: 0x0008DE99 File Offset: 0x0008C099
		// (set) Token: 0x060026A1 RID: 9889 RVA: 0x0008DEA1 File Offset: 0x0008C0A1
		public int ProviderType
		{
			get
			{
				return this.m_providerType;
			}
			set
			{
				this.m_providerType = value;
			}
		}

		/// <summary>Gets or sets the name of the key container.</summary>
		/// <returns>The name of the key container.</returns>
		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x060026A2 RID: 9890 RVA: 0x0008DEAA File Offset: 0x0008C0AA
		// (set) Token: 0x060026A3 RID: 9891 RVA: 0x0008DEB2 File Offset: 0x0008C0B2
		public string KeyContainerName
		{
			get
			{
				return this.m_keyContainerName;
			}
			set
			{
				this.m_keyContainerName = value;
			}
		}

		/// <summary>Gets or sets the key specification.</summary>
		/// <returns>One of the AT_ values defined in the Wincrypt.h header file.</returns>
		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x060026A4 RID: 9892 RVA: 0x0008DEBB File Offset: 0x0008C0BB
		// (set) Token: 0x060026A5 RID: 9893 RVA: 0x0008DEC3 File Offset: 0x0008C0C3
		public int KeySpec
		{
			get
			{
				return this.m_keySpec;
			}
			set
			{
				this.m_keySpec = value;
			}
		}

		/// <summary>Gets or sets the key container permissions.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> values. The default is <see cref="F:System.Security.Permissions.KeyContainerPermissionFlags.NoFlags" />.</returns>
		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x060026A6 RID: 9894 RVA: 0x0008DECC File Offset: 0x0008C0CC
		// (set) Token: 0x060026A7 RID: 9895 RVA: 0x0008DED4 File Offset: 0x0008C0D4
		public KeyContainerPermissionFlags Flags
		{
			get
			{
				return this.m_flags;
			}
			set
			{
				this.m_flags = value;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.KeyContainerPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.KeyContainerPermission" /> that corresponds to the attribute.</returns>
		// Token: 0x060026A8 RID: 9896 RVA: 0x0008DEE0 File Offset: 0x0008C0E0
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new KeyContainerPermission(PermissionState.Unrestricted);
			}
			if (KeyContainerPermissionAccessEntry.IsUnrestrictedEntry(this.m_keyStore, this.m_providerName, this.m_providerType, this.m_keyContainerName, this.m_keySpec))
			{
				return new KeyContainerPermission(this.m_flags);
			}
			KeyContainerPermission keyContainerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
			KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry = new KeyContainerPermissionAccessEntry(this.m_keyStore, this.m_providerName, this.m_providerType, this.m_keyContainerName, this.m_keySpec, this.m_flags);
			keyContainerPermission.AccessEntries.Add(keyContainerPermissionAccessEntry);
			return keyContainerPermission;
		}

		// Token: 0x04000EFB RID: 3835
		private KeyContainerPermissionFlags m_flags;

		// Token: 0x04000EFC RID: 3836
		private string m_keyStore;

		// Token: 0x04000EFD RID: 3837
		private string m_providerName;

		// Token: 0x04000EFE RID: 3838
		private int m_providerType = -1;

		// Token: 0x04000EFF RID: 3839
		private string m_keyContainerName;

		// Token: 0x04000F00 RID: 3840
		private int m_keySpec = -1;
	}
}
