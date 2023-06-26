using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Defines the collection of object identifiers (OIDs) that indicates the applications that use the key. This class cannot be inherited.</summary>
	// Token: 0x02000478 RID: 1144
	public sealed class X509EnhancedKeyUsageExtension : X509Extension
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509EnhancedKeyUsageExtension" /> class.</summary>
		// Token: 0x06002A64 RID: 10852 RVA: 0x000C1439 File Offset: 0x000BF639
		public X509EnhancedKeyUsageExtension()
			: base("2.5.29.37")
		{
			this.m_enhancedKeyUsages = new OidCollection();
			this.m_decoded = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509EnhancedKeyUsageExtension" /> class using an <see cref="T:System.Security.Cryptography.OidCollection" /> and a value that identifies whether the extension is critical.</summary>
		/// <param name="enhancedKeyUsages">An <see cref="T:System.Security.Cryptography.OidCollection" /> collection.</param>
		/// <param name="critical">
		///   <see langword="true" /> if the extension is critical; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The specified <see cref="T:System.Security.Cryptography.OidCollection" /> contains one or more corrupt values.</exception>
		// Token: 0x06002A65 RID: 10853 RVA: 0x000C1458 File Offset: 0x000BF658
		public X509EnhancedKeyUsageExtension(OidCollection enhancedKeyUsages, bool critical)
			: base("2.5.29.37", X509EnhancedKeyUsageExtension.EncodeExtension(enhancedKeyUsages), critical)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509EnhancedKeyUsageExtension" /> class using an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object and a value that identifies whether the extension is critical.</summary>
		/// <param name="encodedEnhancedKeyUsages">The encoded data to use to create the extension.</param>
		/// <param name="critical">
		///   <see langword="true" /> if the extension is critical; otherwise, <see langword="false" />.</param>
		// Token: 0x06002A66 RID: 10854 RVA: 0x000C146C File Offset: 0x000BF66C
		public X509EnhancedKeyUsageExtension(AsnEncodedData encodedEnhancedKeyUsages, bool critical)
			: base("2.5.29.37", encodedEnhancedKeyUsages.RawData, critical)
		{
		}

		/// <summary>Gets the collection of object identifiers (OIDs) that indicate the applications that use the key.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.OidCollection" /> object indicating the applications that use the key.</returns>
		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x06002A67 RID: 10855 RVA: 0x000C1480 File Offset: 0x000BF680
		public OidCollection EnhancedKeyUsages
		{
			get
			{
				if (!this.m_decoded)
				{
					this.DecodeExtension();
				}
				OidCollection oidCollection = new OidCollection();
				foreach (Oid oid in this.m_enhancedKeyUsages)
				{
					oidCollection.Add(oid);
				}
				return oidCollection;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509EnhancedKeyUsageExtension" /> class using an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</summary>
		/// <param name="asnEncodedData">The encoded data to use to create the extension.</param>
		// Token: 0x06002A68 RID: 10856 RVA: 0x000C14C7 File Offset: 0x000BF6C7
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			base.CopyFrom(asnEncodedData);
			this.m_decoded = false;
		}

		// Token: 0x06002A69 RID: 10857 RVA: 0x000C14D8 File Offset: 0x000BF6D8
		private void DecodeExtension()
		{
			uint num = 0U;
			SafeLocalAllocHandle safeLocalAllocHandle = null;
			if (!CAPI.DecodeObject(new IntPtr(36L), this.m_rawData, out safeLocalAllocHandle, out num))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			CAPIBase.CERT_ENHKEY_USAGE cert_ENHKEY_USAGE = (CAPIBase.CERT_ENHKEY_USAGE)Marshal.PtrToStructure(safeLocalAllocHandle.DangerousGetHandle(), typeof(CAPIBase.CERT_ENHKEY_USAGE));
			this.m_enhancedKeyUsages = new OidCollection();
			int num2 = 0;
			while ((long)num2 < (long)((ulong)cert_ENHKEY_USAGE.cUsageIdentifier))
			{
				IntPtr intPtr = Marshal.ReadIntPtr(new IntPtr((long)cert_ENHKEY_USAGE.rgpszUsageIdentifier + (long)(num2 * Marshal.SizeOf(typeof(IntPtr)))));
				string text = Marshal.PtrToStringAnsi(intPtr);
				Oid oid = new Oid(text, OidGroup.ExtensionOrAttribute, false);
				this.m_enhancedKeyUsages.Add(oid);
				num2++;
			}
			this.m_decoded = true;
			safeLocalAllocHandle.Dispose();
		}

		// Token: 0x06002A6A RID: 10858 RVA: 0x000C15A8 File Offset: 0x000BF7A8
		private unsafe static byte[] EncodeExtension(OidCollection enhancedKeyUsages)
		{
			if (enhancedKeyUsages == null)
			{
				throw new ArgumentNullException("enhancedKeyUsages");
			}
			SafeLocalAllocHandle safeLocalAllocHandle = X509Utils.CopyOidsToUnmanagedMemory(enhancedKeyUsages);
			byte[] array = null;
			using (safeLocalAllocHandle)
			{
				CAPIBase.CERT_ENHKEY_USAGE cert_ENHKEY_USAGE = default(CAPIBase.CERT_ENHKEY_USAGE);
				cert_ENHKEY_USAGE.cUsageIdentifier = (uint)enhancedKeyUsages.Count;
				cert_ENHKEY_USAGE.rgpszUsageIdentifier = safeLocalAllocHandle.DangerousGetHandle();
				if (!CAPI.EncodeObject("2.5.29.37", new IntPtr((void*)(&cert_ENHKEY_USAGE)), out array))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
			}
			return array;
		}

		// Token: 0x04002624 RID: 9764
		private OidCollection m_enhancedKeyUsages;

		// Token: 0x04002625 RID: 9765
		private bool m_decoded;
	}
}
