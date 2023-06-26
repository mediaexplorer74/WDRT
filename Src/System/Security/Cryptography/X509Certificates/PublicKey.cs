using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents a certificate's public key information. This class cannot be inherited.</summary>
	// Token: 0x02000465 RID: 1125
	public sealed class PublicKey
	{
		// Token: 0x0600299E RID: 10654 RVA: 0x000BCB62 File Offset: 0x000BAD62
		private PublicKey()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.PublicKey" /> class using an object identifier (OID) object of the public key, an ASN.1-encoded representation of the public key parameters, and an ASN.1-encoded representation of the public key value.</summary>
		/// <param name="oid">An object identifier (OID) object that represents the public key.</param>
		/// <param name="parameters">An ASN.1-encoded representation of the public key parameters.</param>
		/// <param name="keyValue">An ASN.1-encoded representation of the public key value.</param>
		// Token: 0x0600299F RID: 10655 RVA: 0x000BCB6A File Offset: 0x000BAD6A
		public PublicKey(Oid oid, AsnEncodedData parameters, AsnEncodedData keyValue)
		{
			this.m_oid = new Oid(oid);
			this.m_encodedParameters = new AsnEncodedData(parameters);
			this.m_encodedKeyValue = new AsnEncodedData(keyValue);
		}

		// Token: 0x060029A0 RID: 10656 RVA: 0x000BCB96 File Offset: 0x000BAD96
		internal PublicKey(PublicKey publicKey)
		{
			this.m_oid = new Oid(publicKey.m_oid);
			this.m_encodedParameters = new AsnEncodedData(publicKey.m_encodedParameters);
			this.m_encodedKeyValue = new AsnEncodedData(publicKey.m_encodedKeyValue);
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x060029A1 RID: 10657 RVA: 0x000BCBD1 File Offset: 0x000BADD1
		internal uint AlgorithmId
		{
			get
			{
				if (this.m_aiPubKey == 0U)
				{
					this.m_aiPubKey = X509Utils.OidToAlgId(this.m_oid.Value);
				}
				return this.m_aiPubKey;
			}
		}

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x060029A2 RID: 10658 RVA: 0x000BCBF7 File Offset: 0x000BADF7
		private byte[] CspBlobData
		{
			get
			{
				if (this.m_cspBlobData == null)
				{
					PublicKey.DecodePublicKeyObject(this.AlgorithmId, this.m_encodedKeyValue.RawData, this.m_encodedParameters.RawData, out this.m_cspBlobData);
				}
				return this.m_cspBlobData;
			}
		}

		/// <summary>Gets an <see cref="T:System.Security.Cryptography.RSA" /> derived object or a <see cref="T:System.Security.Cryptography.DSA" /> derived object representing the public key.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> object representing the public key.</returns>
		/// <exception cref="T:System.NotSupportedException">The key algorithm is not supported.</exception>
		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x060029A3 RID: 10659 RVA: 0x000BCC30 File Offset: 0x000BAE30
		public AsymmetricAlgorithm Key
		{
			get
			{
				if (this.m_key == null)
				{
					uint algorithmId = this.AlgorithmId;
					if (algorithmId != 8704U)
					{
						if (algorithmId != 9216U && algorithmId != 41984U)
						{
							throw new NotSupportedException(SR.GetString("NotSupported_KeyAlgorithm"));
						}
						RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider();
						rsacryptoServiceProvider.ImportCspBlob(this.CspBlobData);
						this.m_key = rsacryptoServiceProvider;
					}
					else
					{
						DSACryptoServiceProvider dsacryptoServiceProvider = new DSACryptoServiceProvider();
						dsacryptoServiceProvider.ImportCspBlob(this.CspBlobData);
						this.m_key = dsacryptoServiceProvider;
					}
				}
				return this.m_key;
			}
		}

		/// <summary>Gets an object identifier (OID) object of the public key.</summary>
		/// <returns>An object identifier (OID) object of the public key.</returns>
		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x060029A4 RID: 10660 RVA: 0x000BCCB0 File Offset: 0x000BAEB0
		public Oid Oid
		{
			get
			{
				return new Oid(this.m_oid);
			}
		}

		/// <summary>Gets the ASN.1-encoded representation of the public key value.</summary>
		/// <returns>The ASN.1-encoded representation of the public key value.</returns>
		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x060029A5 RID: 10661 RVA: 0x000BCCBD File Offset: 0x000BAEBD
		public AsnEncodedData EncodedKeyValue
		{
			get
			{
				return this.m_encodedKeyValue;
			}
		}

		/// <summary>Gets the ASN.1-encoded representation of the public key parameters.</summary>
		/// <returns>The ASN.1-encoded representation of the public key parameters.</returns>
		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x060029A6 RID: 10662 RVA: 0x000BCCC5 File Offset: 0x000BAEC5
		public AsnEncodedData EncodedParameters
		{
			get
			{
				return this.m_encodedParameters;
			}
		}

		// Token: 0x060029A7 RID: 10663 RVA: 0x000BCCD0 File Offset: 0x000BAED0
		private static void DecodePublicKeyObject(uint aiPubKey, byte[] encodedKeyValue, byte[] encodedParameters, out byte[] decodedData)
		{
			decodedData = null;
			IntPtr zero = IntPtr.Zero;
			if (aiPubKey <= 9216U)
			{
				if (aiPubKey == 8704U)
				{
					zero = new IntPtr(38L);
					goto IL_6F;
				}
				if (aiPubKey != 9216U)
				{
					goto IL_5F;
				}
			}
			else if (aiPubKey != 41984U)
			{
				if (aiPubKey - 43521U > 1U)
				{
					goto IL_5F;
				}
				throw new NotSupportedException(SR.GetString("NotSupported_KeyAlgorithm"));
			}
			zero = new IntPtr(19L);
			goto IL_6F;
			IL_5F:
			throw new NotSupportedException(SR.GetString("NotSupported_KeyAlgorithm"));
			IL_6F:
			SafeLocalAllocHandle safeLocalAllocHandle = null;
			uint num = 0U;
			if (!CAPI.DecodeObject(zero, encodedKeyValue, out safeLocalAllocHandle, out num))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			if ((int)zero == 19)
			{
				decodedData = new byte[num];
				Marshal.Copy(safeLocalAllocHandle.DangerousGetHandle(), decodedData, 0, decodedData.Length);
			}
			else if ((int)zero == 38)
			{
				SafeLocalAllocHandle safeLocalAllocHandle2 = null;
				uint num2 = 0U;
				if (!CAPI.DecodeObject(new IntPtr(39L), encodedParameters, out safeLocalAllocHandle2, out num2))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
				decodedData = PublicKey.ConstructDSSPubKeyCspBlob(safeLocalAllocHandle, safeLocalAllocHandle2);
				safeLocalAllocHandle2.Dispose();
			}
			safeLocalAllocHandle.Dispose();
		}

		// Token: 0x060029A8 RID: 10664 RVA: 0x000BCDD8 File Offset: 0x000BAFD8
		private static byte[] ConstructDSSPubKeyCspBlob(SafeLocalAllocHandle decodedKeyValue, SafeLocalAllocHandle decodedParameters)
		{
			CAPIBase.CRYPTOAPI_BLOB cryptoapi_BLOB = (CAPIBase.CRYPTOAPI_BLOB)Marshal.PtrToStructure(decodedKeyValue.DangerousGetHandle(), typeof(CAPIBase.CRYPTOAPI_BLOB));
			CAPIBase.CERT_DSS_PARAMETERS cert_DSS_PARAMETERS = (CAPIBase.CERT_DSS_PARAMETERS)Marshal.PtrToStructure(decodedParameters.DangerousGetHandle(), typeof(CAPIBase.CERT_DSS_PARAMETERS));
			uint cbData = cert_DSS_PARAMETERS.p.cbData;
			if (cbData == 0U)
			{
				throw new CryptographicException(-2146893803);
			}
			uint num = 16U + cbData + 20U + cbData + cbData + 24U;
			MemoryStream memoryStream = new MemoryStream((int)num);
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			binaryWriter.Write(6);
			binaryWriter.Write(2);
			binaryWriter.Write(0);
			binaryWriter.Write(8704U);
			binaryWriter.Write(827544388U);
			binaryWriter.Write(cbData * 8U);
			byte[] array = new byte[cert_DSS_PARAMETERS.p.cbData];
			Marshal.Copy(cert_DSS_PARAMETERS.p.pbData, array, 0, array.Length);
			binaryWriter.Write(array);
			uint num2 = cert_DSS_PARAMETERS.q.cbData;
			if (num2 == 0U || num2 > 20U)
			{
				throw new CryptographicException(-2146893803);
			}
			byte[] array2 = new byte[cert_DSS_PARAMETERS.q.cbData];
			Marshal.Copy(cert_DSS_PARAMETERS.q.pbData, array2, 0, array2.Length);
			binaryWriter.Write(array2);
			if (20U > num2)
			{
				binaryWriter.Write(new byte[20U - num2]);
			}
			num2 = cert_DSS_PARAMETERS.g.cbData;
			if (num2 == 0U || num2 > cbData)
			{
				throw new CryptographicException(-2146893803);
			}
			byte[] array3 = new byte[cert_DSS_PARAMETERS.g.cbData];
			Marshal.Copy(cert_DSS_PARAMETERS.g.pbData, array3, 0, array3.Length);
			binaryWriter.Write(array3);
			if (cbData > num2)
			{
				binaryWriter.Write(new byte[cbData - num2]);
			}
			num2 = cryptoapi_BLOB.cbData;
			if (num2 == 0U || num2 > cbData)
			{
				throw new CryptographicException(-2146893803);
			}
			byte[] array4 = new byte[cryptoapi_BLOB.cbData];
			Marshal.Copy(cryptoapi_BLOB.pbData, array4, 0, array4.Length);
			binaryWriter.Write(array4);
			if (cbData > num2)
			{
				binaryWriter.Write(new byte[cbData - num2]);
			}
			binaryWriter.Write(uint.MaxValue);
			binaryWriter.Write(new byte[20]);
			return memoryStream.ToArray();
		}

		// Token: 0x040025A1 RID: 9633
		private AsnEncodedData m_encodedKeyValue;

		// Token: 0x040025A2 RID: 9634
		private AsnEncodedData m_encodedParameters;

		// Token: 0x040025A3 RID: 9635
		private Oid m_oid;

		// Token: 0x040025A4 RID: 9636
		private uint m_aiPubKey;

		// Token: 0x040025A5 RID: 9637
		private byte[] m_cspBlobData;

		// Token: 0x040025A6 RID: 9638
		private AsymmetricAlgorithm m_key;
	}
}
