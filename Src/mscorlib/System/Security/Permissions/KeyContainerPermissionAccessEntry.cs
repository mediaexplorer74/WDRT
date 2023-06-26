using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace System.Security.Permissions
{
	/// <summary>Specifies access rights for specific key containers. This class cannot be inherited.</summary>
	// Token: 0x02000313 RID: 787
	[ComVisible(true)]
	[Serializable]
	public sealed class KeyContainerPermissionAccessEntry
	{
		// Token: 0x060027D0 RID: 10192 RVA: 0x00091F16 File Offset: 0x00090116
		internal KeyContainerPermissionAccessEntry(KeyContainerPermissionAccessEntry accessEntry)
			: this(accessEntry.KeyStore, accessEntry.ProviderName, accessEntry.ProviderType, accessEntry.KeyContainerName, accessEntry.KeySpec, accessEntry.Flags)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> class, using the specified key container name and access permissions.</summary>
		/// <param name="keyContainerName">The name of the key container.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The resulting entry would have unrestricted access.</exception>
		// Token: 0x060027D1 RID: 10193 RVA: 0x00091F42 File Offset: 0x00090142
		public KeyContainerPermissionAccessEntry(string keyContainerName, KeyContainerPermissionFlags flags)
			: this(null, null, -1, keyContainerName, -1, flags)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> class, using the specified cryptographic service provider (CSP) parameters and access permissions.</summary>
		/// <param name="parameters">A <see cref="T:System.Security.Cryptography.CspParameters" /> object that contains the cryptographic service provider (CSP) parameters.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The resulting entry would have unrestricted access.</exception>
		// Token: 0x060027D2 RID: 10194 RVA: 0x00091F50 File Offset: 0x00090150
		public KeyContainerPermissionAccessEntry(CspParameters parameters, KeyContainerPermissionFlags flags)
			: this(((parameters.Flags & CspProviderFlags.UseMachineKeyStore) == CspProviderFlags.UseMachineKeyStore) ? "Machine" : "User", parameters.ProviderName, parameters.ProviderType, parameters.KeyContainerName, parameters.KeyNumber, flags)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> class with the specified property values.</summary>
		/// <param name="keyStore">The name of the key store.</param>
		/// <param name="providerName">The name of the provider.</param>
		/// <param name="providerType">The type code for the provider. See the <see cref="P:System.Security.Permissions.KeyContainerPermissionAccessEntry.ProviderType" /> property for values.</param>
		/// <param name="keyContainerName">The name of the key container.</param>
		/// <param name="keySpec">The key specification. See the <see cref="P:System.Security.Permissions.KeyContainerPermissionAccessEntry.KeySpec" /> property for values.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The resulting entry would have unrestricted access.</exception>
		// Token: 0x060027D3 RID: 10195 RVA: 0x00091F88 File Offset: 0x00090188
		public KeyContainerPermissionAccessEntry(string keyStore, string providerName, int providerType, string keyContainerName, int keySpec, KeyContainerPermissionFlags flags)
		{
			this.m_providerName = ((providerName == null) ? "*" : providerName);
			this.m_providerType = providerType;
			this.m_keyContainerName = ((keyContainerName == null) ? "*" : keyContainerName);
			this.m_keySpec = keySpec;
			this.KeyStore = keyStore;
			this.Flags = flags;
		}

		/// <summary>Gets or sets the name of the key store.</summary>
		/// <returns>The name of the key store.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting entry would have unrestricted access.</exception>
		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x060027D4 RID: 10196 RVA: 0x00091FDD File Offset: 0x000901DD
		// (set) Token: 0x060027D5 RID: 10197 RVA: 0x00091FE8 File Offset: 0x000901E8
		public string KeyStore
		{
			get
			{
				return this.m_keyStore;
			}
			set
			{
				if (KeyContainerPermissionAccessEntry.IsUnrestrictedEntry(value, this.ProviderName, this.ProviderType, this.KeyContainerName, this.KeySpec))
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_InvalidAccessEntry"));
				}
				if (value == null)
				{
					this.m_keyStore = "*";
					return;
				}
				if (value != "User" && value != "Machine" && value != "*")
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidKeyStore", new object[] { value }), "value");
				}
				this.m_keyStore = value;
			}
		}

		/// <summary>Gets or sets the provider name.</summary>
		/// <returns>The name of the provider.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting entry would have unrestricted access.</exception>
		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x060027D6 RID: 10198 RVA: 0x00092081 File Offset: 0x00090281
		// (set) Token: 0x060027D7 RID: 10199 RVA: 0x0009208C File Offset: 0x0009028C
		public string ProviderName
		{
			get
			{
				return this.m_providerName;
			}
			set
			{
				if (KeyContainerPermissionAccessEntry.IsUnrestrictedEntry(this.KeyStore, value, this.ProviderType, this.KeyContainerName, this.KeySpec))
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_InvalidAccessEntry"));
				}
				if (value == null)
				{
					this.m_providerName = "*";
					return;
				}
				this.m_providerName = value;
			}
		}

		/// <summary>Gets or sets the provider type.</summary>
		/// <returns>One of the PROV_ values defined in the Wincrypt.h header file.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting entry would have unrestricted access.</exception>
		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x060027D8 RID: 10200 RVA: 0x000920DF File Offset: 0x000902DF
		// (set) Token: 0x060027D9 RID: 10201 RVA: 0x000920E7 File Offset: 0x000902E7
		public int ProviderType
		{
			get
			{
				return this.m_providerType;
			}
			set
			{
				if (KeyContainerPermissionAccessEntry.IsUnrestrictedEntry(this.KeyStore, this.ProviderName, value, this.KeyContainerName, this.KeySpec))
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_InvalidAccessEntry"));
				}
				this.m_providerType = value;
			}
		}

		/// <summary>Gets or sets the key container name.</summary>
		/// <returns>The name of the key container.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting entry would have unrestricted access.</exception>
		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x060027DA RID: 10202 RVA: 0x00092120 File Offset: 0x00090320
		// (set) Token: 0x060027DB RID: 10203 RVA: 0x00092128 File Offset: 0x00090328
		public string KeyContainerName
		{
			get
			{
				return this.m_keyContainerName;
			}
			set
			{
				if (KeyContainerPermissionAccessEntry.IsUnrestrictedEntry(this.KeyStore, this.ProviderName, this.ProviderType, value, this.KeySpec))
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_InvalidAccessEntry"));
				}
				if (value == null)
				{
					this.m_keyContainerName = "*";
					return;
				}
				this.m_keyContainerName = value;
			}
		}

		/// <summary>Gets or sets the key specification.</summary>
		/// <returns>One of the AT_ values defined in the Wincrypt.h header file.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting entry would have unrestricted access.</exception>
		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x060027DC RID: 10204 RVA: 0x0009217B File Offset: 0x0009037B
		// (set) Token: 0x060027DD RID: 10205 RVA: 0x00092183 File Offset: 0x00090383
		public int KeySpec
		{
			get
			{
				return this.m_keySpec;
			}
			set
			{
				if (KeyContainerPermissionAccessEntry.IsUnrestrictedEntry(this.KeyStore, this.ProviderName, this.ProviderType, this.KeyContainerName, value))
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_InvalidAccessEntry"));
				}
				this.m_keySpec = value;
			}
		}

		/// <summary>Gets or sets the key container permissions.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> values. The default is <see cref="F:System.Security.Permissions.KeyContainerPermissionFlags.NoFlags" />.</returns>
		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x060027DE RID: 10206 RVA: 0x000921BC File Offset: 0x000903BC
		// (set) Token: 0x060027DF RID: 10207 RVA: 0x000921C4 File Offset: 0x000903C4
		public KeyContainerPermissionFlags Flags
		{
			get
			{
				return this.m_flags;
			}
			set
			{
				KeyContainerPermission.VerifyFlags(value);
				this.m_flags = value;
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object is equal to the current instance.</summary>
		/// <param name="o">The <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object to compare with the currentinstance.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> is equal to the current <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x060027E0 RID: 10208 RVA: 0x000921D4 File Offset: 0x000903D4
		public override bool Equals(object o)
		{
			KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry = o as KeyContainerPermissionAccessEntry;
			return keyContainerPermissionAccessEntry != null && !(keyContainerPermissionAccessEntry.m_keyStore != this.m_keyStore) && !(keyContainerPermissionAccessEntry.m_providerName != this.m_providerName) && keyContainerPermissionAccessEntry.m_providerType == this.m_providerType && !(keyContainerPermissionAccessEntry.m_keyContainerName != this.m_keyContainerName) && keyContainerPermissionAccessEntry.m_keySpec == this.m_keySpec;
		}

		/// <summary>Gets a hash code for the current instance that is suitable for use in hashing algorithms and data structures such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object.</returns>
		// Token: 0x060027E1 RID: 10209 RVA: 0x00092250 File Offset: 0x00090450
		public override int GetHashCode()
		{
			int num = 0;
			num |= (this.m_keyStore.GetHashCode() & 255) << 24;
			num |= (this.m_providerName.GetHashCode() & 255) << 16;
			num |= (this.m_providerType & 15) << 12;
			num |= (this.m_keyContainerName.GetHashCode() & 255) << 4;
			return num | (this.m_keySpec & 15);
		}

		// Token: 0x060027E2 RID: 10210 RVA: 0x000922C0 File Offset: 0x000904C0
		internal bool IsSubsetOf(KeyContainerPermissionAccessEntry target)
		{
			return (!(target.m_keyStore != "*") || !(this.m_keyStore != target.m_keyStore)) && (!(target.m_providerName != "*") || !(this.m_providerName != target.m_providerName)) && (target.m_providerType == -1 || this.m_providerType == target.m_providerType) && (!(target.m_keyContainerName != "*") || !(this.m_keyContainerName != target.m_keyContainerName)) && (target.m_keySpec == -1 || this.m_keySpec == target.m_keySpec);
		}

		// Token: 0x060027E3 RID: 10211 RVA: 0x00092378 File Offset: 0x00090578
		internal static bool IsUnrestrictedEntry(string keyStore, string providerName, int providerType, string keyContainerName, int keySpec)
		{
			return (!(keyStore != "*") || keyStore == null) && (!(providerName != "*") || providerName == null) && providerType == -1 && (!(keyContainerName != "*") || keyContainerName == null) && keySpec == -1;
		}

		// Token: 0x04000F71 RID: 3953
		private string m_keyStore;

		// Token: 0x04000F72 RID: 3954
		private string m_providerName;

		// Token: 0x04000F73 RID: 3955
		private int m_providerType;

		// Token: 0x04000F74 RID: 3956
		private string m_keyContainerName;

		// Token: 0x04000F75 RID: 3957
		private int m_keySpec;

		// Token: 0x04000F76 RID: 3958
		private KeyContainerPermissionFlags m_flags;
	}
}
