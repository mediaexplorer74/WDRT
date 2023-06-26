using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Contains information about the properties of a digital signature.</summary>
	// Token: 0x02000298 RID: 664
	[ComVisible(true)]
	public class SignatureDescription
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SignatureDescription" /> class.</summary>
		// Token: 0x06002390 RID: 9104 RVA: 0x00081E66 File Offset: 0x00080066
		public SignatureDescription()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SignatureDescription" /> class from the specified <see cref="T:System.Security.SecurityElement" />.</summary>
		/// <param name="el">The <see cref="T:System.Security.SecurityElement" /> from which to get the algorithms for the signature description.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="el" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002391 RID: 9105 RVA: 0x00081E70 File Offset: 0x00080070
		public SignatureDescription(SecurityElement el)
		{
			if (el == null)
			{
				throw new ArgumentNullException("el");
			}
			this._strKey = el.SearchForTextOfTag("Key");
			this._strDigest = el.SearchForTextOfTag("Digest");
			this._strFormatter = el.SearchForTextOfTag("Formatter");
			this._strDeformatter = el.SearchForTextOfTag("Deformatter");
		}

		/// <summary>Gets or sets the key algorithm for the signature description.</summary>
		/// <returns>The key algorithm for the signature description.</returns>
		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06002392 RID: 9106 RVA: 0x00081ED5 File Offset: 0x000800D5
		// (set) Token: 0x06002393 RID: 9107 RVA: 0x00081EDD File Offset: 0x000800DD
		public string KeyAlgorithm
		{
			get
			{
				return this._strKey;
			}
			set
			{
				this._strKey = value;
			}
		}

		/// <summary>Gets or sets the digest algorithm for the signature description.</summary>
		/// <returns>The digest algorithm for the signature description.</returns>
		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06002394 RID: 9108 RVA: 0x00081EE6 File Offset: 0x000800E6
		// (set) Token: 0x06002395 RID: 9109 RVA: 0x00081EEE File Offset: 0x000800EE
		public string DigestAlgorithm
		{
			get
			{
				return this._strDigest;
			}
			set
			{
				this._strDigest = value;
			}
		}

		/// <summary>Gets or sets the formatter algorithm for the signature description.</summary>
		/// <returns>The formatter algorithm for the signature description.</returns>
		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06002396 RID: 9110 RVA: 0x00081EF7 File Offset: 0x000800F7
		// (set) Token: 0x06002397 RID: 9111 RVA: 0x00081EFF File Offset: 0x000800FF
		public string FormatterAlgorithm
		{
			get
			{
				return this._strFormatter;
			}
			set
			{
				this._strFormatter = value;
			}
		}

		/// <summary>Gets or sets the deformatter algorithm for the signature description.</summary>
		/// <returns>The deformatter algorithm for the signature description.</returns>
		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06002398 RID: 9112 RVA: 0x00081F08 File Offset: 0x00080108
		// (set) Token: 0x06002399 RID: 9113 RVA: 0x00081F10 File Offset: 0x00080110
		public string DeformatterAlgorithm
		{
			get
			{
				return this._strDeformatter;
			}
			set
			{
				this._strDeformatter = value;
			}
		}

		/// <summary>Creates an <see cref="T:System.Security.Cryptography.AsymmetricSignatureDeformatter" /> instance with the specified key using the <see cref="P:System.Security.Cryptography.SignatureDescription.DeformatterAlgorithm" /> property.</summary>
		/// <param name="key">The key to use in the <see cref="T:System.Security.Cryptography.AsymmetricSignatureDeformatter" />.</param>
		/// <returns>The newly created <see cref="T:System.Security.Cryptography.AsymmetricSignatureDeformatter" /> instance.</returns>
		// Token: 0x0600239A RID: 9114 RVA: 0x00081F1C File Offset: 0x0008011C
		public virtual AsymmetricSignatureDeformatter CreateDeformatter(AsymmetricAlgorithm key)
		{
			AsymmetricSignatureDeformatter asymmetricSignatureDeformatter = (AsymmetricSignatureDeformatter)CryptoConfig.CreateFromName(this._strDeformatter);
			asymmetricSignatureDeformatter.SetKey(key);
			return asymmetricSignatureDeformatter;
		}

		/// <summary>Creates an <see cref="T:System.Security.Cryptography.AsymmetricSignatureFormatter" /> instance with the specified key using the <see cref="P:System.Security.Cryptography.SignatureDescription.FormatterAlgorithm" /> property.</summary>
		/// <param name="key">The key to use in the <see cref="T:System.Security.Cryptography.AsymmetricSignatureFormatter" />.</param>
		/// <returns>The newly created <see cref="T:System.Security.Cryptography.AsymmetricSignatureFormatter" /> instance.</returns>
		// Token: 0x0600239B RID: 9115 RVA: 0x00081F44 File Offset: 0x00080144
		public virtual AsymmetricSignatureFormatter CreateFormatter(AsymmetricAlgorithm key)
		{
			AsymmetricSignatureFormatter asymmetricSignatureFormatter = (AsymmetricSignatureFormatter)CryptoConfig.CreateFromName(this._strFormatter);
			asymmetricSignatureFormatter.SetKey(key);
			return asymmetricSignatureFormatter;
		}

		/// <summary>Creates a <see cref="T:System.Security.Cryptography.HashAlgorithm" /> instance using the <see cref="P:System.Security.Cryptography.SignatureDescription.DigestAlgorithm" /> property.</summary>
		/// <returns>The newly created <see cref="T:System.Security.Cryptography.HashAlgorithm" /> instance.</returns>
		// Token: 0x0600239C RID: 9116 RVA: 0x00081F6A File Offset: 0x0008016A
		public virtual HashAlgorithm CreateDigest()
		{
			return (HashAlgorithm)CryptoConfig.CreateFromName(this._strDigest);
		}

		// Token: 0x04000CF2 RID: 3314
		private string _strKey;

		// Token: 0x04000CF3 RID: 3315
		private string _strDigest;

		// Token: 0x04000CF4 RID: 3316
		private string _strFormatter;

		// Token: 0x04000CF5 RID: 3317
		private string _strDeformatter;
	}
}
