using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Defines a string that identifies a certificate's subject key identifier (SKI). This class cannot be inherited.</summary>
	// Token: 0x0200047A RID: 1146
	public sealed class X509SubjectKeyIdentifierExtension : X509Extension
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509SubjectKeyIdentifierExtension" /> class.</summary>
		// Token: 0x06002A6B RID: 10859 RVA: 0x000C1630 File Offset: 0x000BF830
		public X509SubjectKeyIdentifierExtension()
			: base("2.5.29.14")
		{
			this.m_subjectKeyIdentifier = null;
			this.m_decoded = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509SubjectKeyIdentifierExtension" /> class using a string and a value that identifies whether the extension is critical.</summary>
		/// <param name="subjectKeyIdentifier">A string, encoded in hexadecimal format, that represents the subject key identifier (SKI) for a certificate.</param>
		/// <param name="critical">
		///   <see langword="true" /> if the extension is critical; otherwise, <see langword="false" />.</param>
		// Token: 0x06002A6C RID: 10860 RVA: 0x000C164B File Offset: 0x000BF84B
		public X509SubjectKeyIdentifierExtension(string subjectKeyIdentifier, bool critical)
			: base("2.5.29.14", X509SubjectKeyIdentifierExtension.EncodeExtension(subjectKeyIdentifier), critical)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509SubjectKeyIdentifierExtension" /> class using a byte array and a value that identifies whether the extension is critical.</summary>
		/// <param name="subjectKeyIdentifier">A byte array that represents data to use to create the extension.</param>
		/// <param name="critical">
		///   <see langword="true" /> if the extension is critical; otherwise, <see langword="false" />.</param>
		// Token: 0x06002A6D RID: 10861 RVA: 0x000C165F File Offset: 0x000BF85F
		public X509SubjectKeyIdentifierExtension(byte[] subjectKeyIdentifier, bool critical)
			: base("2.5.29.14", X509SubjectKeyIdentifierExtension.EncodeExtension(subjectKeyIdentifier), critical)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509SubjectKeyIdentifierExtension" /> class using encoded data and a value that identifies whether the extension is critical.</summary>
		/// <param name="encodedSubjectKeyIdentifier">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object to use to create the extension.</param>
		/// <param name="critical">
		///   <see langword="true" /> if the extension is critical; otherwise, <see langword="false" />.</param>
		// Token: 0x06002A6E RID: 10862 RVA: 0x000C1673 File Offset: 0x000BF873
		public X509SubjectKeyIdentifierExtension(AsnEncodedData encodedSubjectKeyIdentifier, bool critical)
			: base("2.5.29.14", encodedSubjectKeyIdentifier.RawData, critical)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509SubjectKeyIdentifierExtension" /> class using a public key and a value indicating whether the extension is critical.</summary>
		/// <param name="key">A <see cref="T:System.Security.Cryptography.X509Certificates.PublicKey" /> object to create a subject key identifier (SKI) from.</param>
		/// <param name="critical">
		///   <see langword="true" /> if the extension is critical; otherwise, <see langword="false" />.</param>
		// Token: 0x06002A6F RID: 10863 RVA: 0x000C1687 File Offset: 0x000BF887
		public X509SubjectKeyIdentifierExtension(PublicKey key, bool critical)
			: base("2.5.29.14", X509SubjectKeyIdentifierExtension.EncodePublicKey(key, X509SubjectKeyIdentifierHashAlgorithm.Sha1), critical)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509SubjectKeyIdentifierExtension" /> class using a public key, a hash algorithm identifier, and a value indicating whether the extension is critical.</summary>
		/// <param name="key">A <see cref="T:System.Security.Cryptography.X509Certificates.PublicKey" /> object to create a subject key identifier (SKI) from.</param>
		/// <param name="algorithm">One of the <see cref="T:System.Security.Cryptography.X509Certificates.X509SubjectKeyIdentifierHashAlgorithm" /> values that identifies which hash algorithm to use.</param>
		/// <param name="critical">
		///   <see langword="true" /> if the extension is critical; otherwise, <see langword="false" />.</param>
		// Token: 0x06002A70 RID: 10864 RVA: 0x000C169C File Offset: 0x000BF89C
		public X509SubjectKeyIdentifierExtension(PublicKey key, X509SubjectKeyIdentifierHashAlgorithm algorithm, bool critical)
			: base("2.5.29.14", X509SubjectKeyIdentifierExtension.EncodePublicKey(key, algorithm), critical)
		{
		}

		/// <summary>Gets a string that represents the subject key identifier (SKI) for a certificate.</summary>
		/// <returns>A string, encoded in hexadecimal format, that represents the subject key identifier (SKI).</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The extension cannot be decoded.</exception>
		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x06002A71 RID: 10865 RVA: 0x000C16B1 File Offset: 0x000BF8B1
		public string SubjectKeyIdentifier
		{
			get
			{
				if (!this.m_decoded)
				{
					this.DecodeExtension();
				}
				return this.m_subjectKeyIdentifier;
			}
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509SubjectKeyIdentifierExtension" /> class by copying information from encoded data.</summary>
		/// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object to use to create the extension.</param>
		// Token: 0x06002A72 RID: 10866 RVA: 0x000C16C7 File Offset: 0x000BF8C7
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			base.CopyFrom(asnEncodedData);
			this.m_decoded = false;
		}

		// Token: 0x06002A73 RID: 10867 RVA: 0x000C16D8 File Offset: 0x000BF8D8
		private void DecodeExtension()
		{
			uint num = 0U;
			SafeLocalAllocHandle safeLocalAllocHandle = null;
			SafeLocalAllocHandle safeLocalAllocHandle2 = X509Utils.StringToAnsiPtr("2.5.29.14");
			if (!CAPI.DecodeObject(safeLocalAllocHandle2.DangerousGetHandle(), this.m_rawData, out safeLocalAllocHandle, out num))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			CAPIBase.CRYPTOAPI_BLOB cryptoapi_BLOB = (CAPIBase.CRYPTOAPI_BLOB)Marshal.PtrToStructure(safeLocalAllocHandle.DangerousGetHandle(), typeof(CAPIBase.CRYPTOAPI_BLOB));
			byte[] array = CAPI.BlobToByteArray(cryptoapi_BLOB);
			this.m_subjectKeyIdentifier = X509Utils.EncodeHexString(array);
			this.m_decoded = true;
			safeLocalAllocHandle.Dispose();
			safeLocalAllocHandle2.Dispose();
		}

		// Token: 0x06002A74 RID: 10868 RVA: 0x000C175D File Offset: 0x000BF95D
		private static byte[] EncodeExtension(string subjectKeyIdentifier)
		{
			if (subjectKeyIdentifier == null)
			{
				throw new ArgumentNullException("subjectKeyIdentifier");
			}
			return X509SubjectKeyIdentifierExtension.EncodeExtension(X509Utils.DecodeHexString(subjectKeyIdentifier));
		}

		// Token: 0x06002A75 RID: 10869 RVA: 0x000C1778 File Offset: 0x000BF978
		private unsafe static byte[] EncodeExtension(byte[] subjectKeyIdentifier)
		{
			if (subjectKeyIdentifier == null)
			{
				throw new ArgumentNullException("subjectKeyIdentifier");
			}
			if (subjectKeyIdentifier.Length == 0)
			{
				throw new ArgumentException("subjectKeyIdentifier");
			}
			byte[] array = null;
			fixed (byte[] array2 = subjectKeyIdentifier)
			{
				byte* ptr;
				if (subjectKeyIdentifier == null || array2.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array2[0];
				}
				CAPIBase.CRYPTOAPI_BLOB cryptoapi_BLOB = default(CAPIBase.CRYPTOAPI_BLOB);
				cryptoapi_BLOB.pbData = new IntPtr((void*)ptr);
				cryptoapi_BLOB.cbData = (uint)subjectKeyIdentifier.Length;
				if (!CAPI.EncodeObject("2.5.29.14", new IntPtr((void*)(&cryptoapi_BLOB)), out array))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
			}
			return array;
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x000C1800 File Offset: 0x000BFA00
		private unsafe static SafeLocalAllocHandle EncodePublicKey(PublicKey key)
		{
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			CAPIBase.CERT_PUBLIC_KEY_INFO2* ptr = null;
			string value = key.Oid.Value;
			byte[] rawData = key.EncodedParameters.RawData;
			byte[] rawData2 = key.EncodedKeyValue.RawData;
			uint num = (uint)((long)Marshal.SizeOf(typeof(CAPIBase.CERT_PUBLIC_KEY_INFO2)) + (long)((ulong)X509Utils.AlignedLength((uint)(value.Length + 1))) + (long)((ulong)X509Utils.AlignedLength((uint)rawData.Length)) + (long)rawData2.Length);
			safeLocalAllocHandle = CAPI.LocalAlloc(64U, new IntPtr((long)((ulong)num)));
			ptr = (CAPIBase.CERT_PUBLIC_KEY_INFO2*)(void*)safeLocalAllocHandle.DangerousGetHandle();
			IntPtr intPtr = new IntPtr(ptr + (long)Marshal.SizeOf(typeof(CAPIBase.CERT_PUBLIC_KEY_INFO2)) / (long)sizeof(CAPIBase.CERT_PUBLIC_KEY_INFO2));
			IntPtr intPtr2 = new IntPtr((long)intPtr + (long)((ulong)X509Utils.AlignedLength((uint)(value.Length + 1))));
			IntPtr intPtr3 = new IntPtr((long)intPtr2 + (long)((ulong)X509Utils.AlignedLength((uint)rawData.Length)));
			ptr->Algorithm.pszObjId = intPtr;
			byte[] array = new byte[value.Length + 1];
			Encoding.ASCII.GetBytes(value, 0, value.Length, array, 0);
			Marshal.Copy(array, 0, intPtr, array.Length);
			if (rawData.Length != 0)
			{
				ptr->Algorithm.Parameters.cbData = (uint)rawData.Length;
				ptr->Algorithm.Parameters.pbData = intPtr2;
				Marshal.Copy(rawData, 0, intPtr2, rawData.Length);
			}
			ptr->PublicKey.cbData = (uint)rawData2.Length;
			ptr->PublicKey.pbData = intPtr3;
			Marshal.Copy(rawData2, 0, intPtr3, rawData2.Length);
			return safeLocalAllocHandle;
		}

		// Token: 0x06002A77 RID: 10871 RVA: 0x000C1978 File Offset: 0x000BFB78
		private unsafe static byte[] EncodePublicKey(PublicKey key, X509SubjectKeyIdentifierHashAlgorithm algorithm)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			SafeLocalAllocHandle safeLocalAllocHandle = X509SubjectKeyIdentifierExtension.EncodePublicKey(key);
			CAPIBase.CERT_PUBLIC_KEY_INFO2* ptr = (CAPIBase.CERT_PUBLIC_KEY_INFO2*)(void*)safeLocalAllocHandle.DangerousGetHandle();
			byte[] array = new byte[20];
			byte[] array2 = null;
			byte[] array3;
			byte* ptr2;
			if ((array3 = array) == null || array3.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array3[0];
			}
			uint num = (uint)array.Length;
			IntPtr intPtr = new IntPtr((void*)ptr2);
			try
			{
				if (algorithm == X509SubjectKeyIdentifierHashAlgorithm.Sha1 || X509SubjectKeyIdentifierHashAlgorithm.ShortSha1 == algorithm)
				{
					if (!CAPISafe.CryptHashCertificate(IntPtr.Zero, 32772U, 0U, ptr->PublicKey.pbData, ptr->PublicKey.cbData, intPtr, new IntPtr((void*)(&num))))
					{
						throw new CryptographicException(Marshal.GetHRForLastWin32Error());
					}
				}
				else
				{
					if (X509SubjectKeyIdentifierHashAlgorithm.CapiSha1 != algorithm)
					{
						throw new ArgumentException("algorithm");
					}
					if (!CAPISafe.CryptHashPublicKeyInfo(IntPtr.Zero, 32772U, 0U, 1U, new IntPtr((void*)ptr), intPtr, new IntPtr((void*)(&num))))
					{
						throw new CryptographicException(Marshal.GetHRForLastWin32Error());
					}
				}
				if (X509SubjectKeyIdentifierHashAlgorithm.ShortSha1 == algorithm)
				{
					array2 = new byte[8];
					Array.Copy(array, array.Length - 8, array2, 0, array2.Length);
					byte[] array4 = array2;
					int num2 = 0;
					array4[num2] &= 15;
					byte[] array5 = array2;
					int num3 = 0;
					array5[num3] |= 64;
				}
				else
				{
					array2 = array;
					if (array.Length > (int)num)
					{
						array2 = new byte[num];
						Array.Copy(array, 0, array2, 0, array2.Length);
					}
				}
			}
			finally
			{
				safeLocalAllocHandle.Dispose();
			}
			array3 = null;
			return X509SubjectKeyIdentifierExtension.EncodeExtension(array2);
		}

		// Token: 0x0400262A RID: 9770
		private string m_subjectKeyIdentifier;

		// Token: 0x0400262B RID: 9771
		private bool m_decoded;
	}
}
