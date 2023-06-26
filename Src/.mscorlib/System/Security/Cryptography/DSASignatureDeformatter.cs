using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Cryptography
{
	/// <summary>Verifies a Digital Signature Algorithm (<see cref="T:System.Security.Cryptography.DSA" />) PKCS#1 v1.5 signature.</summary>
	// Token: 0x0200025F RID: 607
	[ComVisible(true)]
	public class DSASignatureDeformatter : AsymmetricSignatureDeformatter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.DSASignatureDeformatter" /> class.</summary>
		// Token: 0x0600219E RID: 8606 RVA: 0x00077205 File Offset: 0x00075405
		public DSASignatureDeformatter()
		{
			this._oid = CryptoConfig.MapNameToOID("SHA1", OidGroup.HashAlgorithm);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.DSASignatureDeformatter" /> class with the specified key.</summary>
		/// <param name="key">The instance of Digital Signature Algorithm (<see cref="T:System.Security.Cryptography.DSA" />) that holds the key.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x0600219F RID: 8607 RVA: 0x0007721E File Offset: 0x0007541E
		public DSASignatureDeformatter(AsymmetricAlgorithm key)
			: this()
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._dsaKey = (DSA)key;
		}

		/// <summary>Specifies the key to be used for the Digital Signature Algorithm (<see cref="T:System.Security.Cryptography.DSA" />) signature deformatter.</summary>
		/// <param name="key">The instance of <see cref="T:System.Security.Cryptography.DSA" /> that holds the key.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x060021A0 RID: 8608 RVA: 0x00077240 File Offset: 0x00075440
		public override void SetKey(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._dsaKey = (DSA)key;
		}

		/// <summary>Specifies the hash algorithm for the Digital Signature Algorithm (<see cref="T:System.Security.Cryptography.DSA" />) signature deformatter.</summary>
		/// <param name="strName">The name of the hash algorithm to use for the signature deformatter.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">The <paramref name="strName" /> parameter does not map to the <see cref="T:System.Security.Cryptography.SHA1" /> hash algorithm.</exception>
		// Token: 0x060021A1 RID: 8609 RVA: 0x0007725C File Offset: 0x0007545C
		public override void SetHashAlgorithm(string strName)
		{
			if (CryptoConfig.MapNameToOID(strName, OidGroup.HashAlgorithm) != this._oid)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_InvalidOperation"));
			}
		}

		/// <summary>Verifies the Digital Signature Algorithm (<see cref="T:System.Security.Cryptography.DSA" />) signature on the data.</summary>
		/// <param name="rgbHash">The data signed with <paramref name="rgbSignature" />.</param>
		/// <param name="rgbSignature">The signature to be verified for <paramref name="rgbHash" />.</param>
		/// <returns>
		///   <see langword="true" /> if the signature is valid for the data; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rgbHash" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="rgbSignature" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">The DSA key is missing.</exception>
		// Token: 0x060021A2 RID: 8610 RVA: 0x00077284 File Offset: 0x00075484
		public override bool VerifySignature(byte[] rgbHash, byte[] rgbSignature)
		{
			if (rgbHash == null)
			{
				throw new ArgumentNullException("rgbHash");
			}
			if (rgbSignature == null)
			{
				throw new ArgumentNullException("rgbSignature");
			}
			if (this._dsaKey == null)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_MissingKey"));
			}
			return this._dsaKey.VerifySignature(rgbHash, rgbSignature);
		}

		// Token: 0x04000C42 RID: 3138
		private DSA _dsaKey;

		// Token: 0x04000C43 RID: 3139
		private string _oid;
	}
}
