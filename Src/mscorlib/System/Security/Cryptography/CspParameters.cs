using System;
using System.Runtime.InteropServices;
using System.Security.AccessControl;

namespace System.Security.Cryptography
{
	/// <summary>Contains parameters that are passed to the cryptographic service provider (CSP) that performs cryptographic computations. This class cannot be inherited.</summary>
	// Token: 0x02000254 RID: 596
	[ComVisible(true)]
	public sealed class CspParameters
	{
		/// <summary>Represents the flags for <see cref="T:System.Security.Cryptography.CspParameters" /> that modify the behavior of the cryptographic service provider (CSP).</summary>
		/// <returns>An enumeration value, or a bitwise combination of enumeration values.</returns>
		/// <exception cref="T:System.ArgumentException">Value is not a valid enumeration value.</exception>
		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06002118 RID: 8472 RVA: 0x00073889 File Offset: 0x00071A89
		// (set) Token: 0x06002119 RID: 8473 RVA: 0x00073894 File Offset: 0x00071A94
		public CspProviderFlags Flags
		{
			get
			{
				return (CspProviderFlags)this.m_flags;
			}
			set
			{
				int num = 255;
				if ((value & (CspProviderFlags)(~(CspProviderFlags)num)) != CspProviderFlags.NoFlags)
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[] { (int)value }), "value");
				}
				this.m_flags = (int)value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object that represents access rights and audit rules for a container.</summary>
		/// <returns>A <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object that represents access rights and audit rules for a container.</returns>
		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x0600211A RID: 8474 RVA: 0x000738DA File Offset: 0x00071ADA
		// (set) Token: 0x0600211B RID: 8475 RVA: 0x000738E2 File Offset: 0x00071AE2
		public CryptoKeySecurity CryptoKeySecurity
		{
			get
			{
				return this.m_cryptoKeySecurity;
			}
			set
			{
				this.m_cryptoKeySecurity = value;
			}
		}

		/// <summary>Gets or sets a password associated with a smart card key.</summary>
		/// <returns>A password associated with a smart card key.</returns>
		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x0600211C RID: 8476 RVA: 0x000738EB File Offset: 0x00071AEB
		// (set) Token: 0x0600211D RID: 8477 RVA: 0x000738F3 File Offset: 0x00071AF3
		public SecureString KeyPassword
		{
			get
			{
				return this.m_keyPassword;
			}
			set
			{
				this.m_keyPassword = value;
				this.m_parentWindowHandle = IntPtr.Zero;
			}
		}

		/// <summary>Gets or sets a handle to the unmanaged parent window for a smart card password dialog box.</summary>
		/// <returns>A handle to the parent window for a smart card password dialog box.</returns>
		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x0600211E RID: 8478 RVA: 0x00073907 File Offset: 0x00071B07
		// (set) Token: 0x0600211F RID: 8479 RVA: 0x0007390F File Offset: 0x00071B0F
		public IntPtr ParentWindowHandle
		{
			get
			{
				return this.m_parentWindowHandle;
			}
			set
			{
				this.m_parentWindowHandle = value;
				this.m_keyPassword = null;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CspParameters" /> class.</summary>
		// Token: 0x06002120 RID: 8480 RVA: 0x0007391F File Offset: 0x00071B1F
		public CspParameters()
			: this(24, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CspParameters" /> class with the specified provider type code.</summary>
		/// <param name="dwTypeIn">A provider type code that specifies the kind of provider to create.</param>
		// Token: 0x06002121 RID: 8481 RVA: 0x0007392B File Offset: 0x00071B2B
		public CspParameters(int dwTypeIn)
			: this(dwTypeIn, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CspParameters" /> class with the specified provider type code and name.</summary>
		/// <param name="dwTypeIn">A provider type code that specifies the kind of provider to create.</param>
		/// <param name="strProviderNameIn">A provider name.</param>
		// Token: 0x06002122 RID: 8482 RVA: 0x00073936 File Offset: 0x00071B36
		public CspParameters(int dwTypeIn, string strProviderNameIn)
			: this(dwTypeIn, strProviderNameIn, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CspParameters" /> class with the specified provider type code and name, and the specified container name.</summary>
		/// <param name="dwTypeIn">The provider type code that specifies the kind of provider to create.</param>
		/// <param name="strProviderNameIn">A provider name.</param>
		/// <param name="strContainerNameIn">A container name.</param>
		// Token: 0x06002123 RID: 8483 RVA: 0x00073941 File Offset: 0x00071B41
		public CspParameters(int dwTypeIn, string strProviderNameIn, string strContainerNameIn)
			: this(dwTypeIn, strProviderNameIn, strContainerNameIn, CspProviderFlags.NoFlags)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CspParameters" /> class using a provider type, a provider name, a container name, access information, and a password associated with a smart card key.</summary>
		/// <param name="providerType">The provider type code that specifies the kind of provider to create.</param>
		/// <param name="providerName">A provider name.</param>
		/// <param name="keyContainerName">A container name.</param>
		/// <param name="cryptoKeySecurity">An object that represents access rights and audit rules for a container.</param>
		/// <param name="keyPassword">A password associated with a smart card key.</param>
		// Token: 0x06002124 RID: 8484 RVA: 0x0007394D File Offset: 0x00071B4D
		public CspParameters(int providerType, string providerName, string keyContainerName, CryptoKeySecurity cryptoKeySecurity, SecureString keyPassword)
			: this(providerType, providerName, keyContainerName)
		{
			this.m_cryptoKeySecurity = cryptoKeySecurity;
			this.m_keyPassword = keyPassword;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CspParameters" /> class using a provider type, a provider name, a container name, access information, and a handle to an unmanaged smart card password dialog.</summary>
		/// <param name="providerType">The provider type code that specifies the kind of provider to create.</param>
		/// <param name="providerName">A provider name.</param>
		/// <param name="keyContainerName">A container name.</param>
		/// <param name="cryptoKeySecurity">An object that represents access rights and audit rules for the container.</param>
		/// <param name="parentWindowHandle">A handle to the parent window for a smart card password dialog.</param>
		// Token: 0x06002125 RID: 8485 RVA: 0x00073968 File Offset: 0x00071B68
		public CspParameters(int providerType, string providerName, string keyContainerName, CryptoKeySecurity cryptoKeySecurity, IntPtr parentWindowHandle)
			: this(providerType, providerName, keyContainerName)
		{
			this.m_cryptoKeySecurity = cryptoKeySecurity;
			this.m_parentWindowHandle = parentWindowHandle;
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x00073983 File Offset: 0x00071B83
		internal CspParameters(int providerType, string providerName, string keyContainerName, CspProviderFlags flags)
		{
			this.ProviderType = providerType;
			this.ProviderName = providerName;
			this.KeyContainerName = keyContainerName;
			this.KeyNumber = -1;
			this.Flags = flags;
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x000739B0 File Offset: 0x00071BB0
		internal CspParameters(CspParameters parameters)
		{
			this.ProviderType = parameters.ProviderType;
			this.ProviderName = parameters.ProviderName;
			this.KeyContainerName = parameters.KeyContainerName;
			this.KeyNumber = parameters.KeyNumber;
			this.Flags = parameters.Flags;
			this.m_cryptoKeySecurity = parameters.m_cryptoKeySecurity;
			this.m_keyPassword = parameters.m_keyPassword;
			this.m_parentWindowHandle = parameters.m_parentWindowHandle;
		}

		/// <summary>Represents the provider type code for <see cref="T:System.Security.Cryptography.CspParameters" />.</summary>
		// Token: 0x04000C06 RID: 3078
		public int ProviderType;

		/// <summary>Represents the provider name for <see cref="T:System.Security.Cryptography.CspParameters" />.</summary>
		// Token: 0x04000C07 RID: 3079
		public string ProviderName;

		/// <summary>Represents the key container name for <see cref="T:System.Security.Cryptography.CspParameters" />.</summary>
		// Token: 0x04000C08 RID: 3080
		public string KeyContainerName;

		/// <summary>Specifies whether an asymmetric key is created as a signature key or an exchange key.</summary>
		// Token: 0x04000C09 RID: 3081
		public int KeyNumber;

		// Token: 0x04000C0A RID: 3082
		private int m_flags;

		// Token: 0x04000C0B RID: 3083
		private CryptoKeySecurity m_cryptoKeySecurity;

		// Token: 0x04000C0C RID: 3084
		private SecureString m_keyPassword;

		// Token: 0x04000C0D RID: 3085
		private IntPtr m_parentWindowHandle;
	}
}
