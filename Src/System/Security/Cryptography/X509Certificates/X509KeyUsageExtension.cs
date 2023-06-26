using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Defines the usage of a key contained within an X.509 certificate.  This class cannot be inherited.</summary>
	// Token: 0x02000476 RID: 1142
	public sealed class X509KeyUsageExtension : X509Extension
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509KeyUsageExtension" /> class.</summary>
		// Token: 0x06002A54 RID: 10836 RVA: 0x000C1088 File Offset: 0x000BF288
		public X509KeyUsageExtension()
			: base("2.5.29.15")
		{
			this.m_decoded = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509KeyUsageExtension" /> class using the specified <see cref="T:System.Security.Cryptography.X509Certificates.X509KeyUsageFlags" /> value and a value that identifies whether the extension is critical.</summary>
		/// <param name="keyUsages">One of the <see cref="T:System.Security.Cryptography.X509Certificates.X509KeyUsageFlags" /> values that describes how to use the key.</param>
		/// <param name="critical">
		///   <see langword="true" /> if the extension is critical; otherwise, <see langword="false" />.</param>
		// Token: 0x06002A55 RID: 10837 RVA: 0x000C109C File Offset: 0x000BF29C
		public X509KeyUsageExtension(X509KeyUsageFlags keyUsages, bool critical)
			: base("2.5.29.15", X509KeyUsageExtension.EncodeExtension(keyUsages), critical)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509KeyUsageExtension" /> class using an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object and a value that identifies whether the extension is critical.</summary>
		/// <param name="encodedKeyUsage">The encoded data to use to create the extension.</param>
		/// <param name="critical">
		///   <see langword="true" /> if the extension is critical; otherwise, <see langword="false" />.</param>
		// Token: 0x06002A56 RID: 10838 RVA: 0x000C10B0 File Offset: 0x000BF2B0
		public X509KeyUsageExtension(AsnEncodedData encodedKeyUsage, bool critical)
			: base("2.5.29.15", encodedKeyUsage.RawData, critical)
		{
		}

		/// <summary>Gets the key usage flag associated with the certificate.</summary>
		/// <returns>One of the <see cref="P:System.Security.Cryptography.X509Certificates.X509KeyUsageExtension.KeyUsages" /> values.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The extension cannot be decoded.</exception>
		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x06002A57 RID: 10839 RVA: 0x000C10C4 File Offset: 0x000BF2C4
		public X509KeyUsageFlags KeyUsages
		{
			get
			{
				if (!this.m_decoded)
				{
					this.DecodeExtension();
				}
				return (X509KeyUsageFlags)this.m_keyUsages;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509KeyUsageExtension" /> class using an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</summary>
		/// <param name="asnEncodedData">The encoded data to use to create the extension.</param>
		// Token: 0x06002A58 RID: 10840 RVA: 0x000C10DA File Offset: 0x000BF2DA
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			base.CopyFrom(asnEncodedData);
			this.m_decoded = false;
		}

		// Token: 0x06002A59 RID: 10841 RVA: 0x000C10EC File Offset: 0x000BF2EC
		private void DecodeExtension()
		{
			uint num = 0U;
			SafeLocalAllocHandle safeLocalAllocHandle = null;
			if (!CAPI.DecodeObject(new IntPtr(14L), this.m_rawData, out safeLocalAllocHandle, out num))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			CAPIBase.CRYPTOAPI_BLOB cryptoapi_BLOB = (CAPIBase.CRYPTOAPI_BLOB)Marshal.PtrToStructure(safeLocalAllocHandle.DangerousGetHandle(), typeof(CAPIBase.CRYPTOAPI_BLOB));
			if (cryptoapi_BLOB.cbData > 4U)
			{
				cryptoapi_BLOB.cbData = 4U;
			}
			byte[] array = new byte[4];
			if (cryptoapi_BLOB.pbData != IntPtr.Zero)
			{
				Marshal.Copy(cryptoapi_BLOB.pbData, array, 0, (int)cryptoapi_BLOB.cbData);
			}
			this.m_keyUsages = BitConverter.ToUInt32(array, 0);
			this.m_decoded = true;
			safeLocalAllocHandle.Dispose();
		}

		// Token: 0x06002A5A RID: 10842 RVA: 0x000C1198 File Offset: 0x000BF398
		private unsafe static byte[] EncodeExtension(X509KeyUsageFlags keyUsages)
		{
			CAPIBase.CRYPT_BIT_BLOB crypt_BIT_BLOB = default(CAPIBase.CRYPT_BIT_BLOB);
			crypt_BIT_BLOB.cbData = 2U;
			crypt_BIT_BLOB.pbData = new IntPtr((void*)(&keyUsages));
			crypt_BIT_BLOB.cUnusedBits = 0U;
			byte[] array = null;
			if (!CAPI.EncodeObject("2.5.29.15", new IntPtr((void*)(&crypt_BIT_BLOB)), out array))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			return array;
		}

		// Token: 0x0400261E RID: 9758
		private uint m_keyUsages;

		// Token: 0x0400261F RID: 9759
		private bool m_decoded;
	}
}
