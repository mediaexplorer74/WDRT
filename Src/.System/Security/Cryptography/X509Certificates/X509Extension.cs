using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents an X509 extension.</summary>
	// Token: 0x02000474 RID: 1140
	public class X509Extension : AsnEncodedData
	{
		// Token: 0x06002A4B RID: 10827 RVA: 0x000C0EFC File Offset: 0x000BF0FC
		internal X509Extension(string oid)
			: base(new Oid(oid, OidGroup.ExtensionOrAttribute, false))
		{
		}

		// Token: 0x06002A4C RID: 10828 RVA: 0x000C0F0C File Offset: 0x000BF10C
		internal X509Extension(IntPtr pExtension)
		{
			CAPIBase.CERT_EXTENSION cert_EXTENSION = (CAPIBase.CERT_EXTENSION)Marshal.PtrToStructure(pExtension, typeof(CAPIBase.CERT_EXTENSION));
			this.m_critical = cert_EXTENSION.fCritical;
			string pszObjId = cert_EXTENSION.pszObjId;
			this.m_oid = new Oid(pszObjId, OidGroup.ExtensionOrAttribute, false);
			byte[] array = new byte[cert_EXTENSION.Value.cbData];
			if (cert_EXTENSION.Value.pbData != IntPtr.Zero)
			{
				Marshal.Copy(cert_EXTENSION.Value.pbData, array, 0, array.Length);
			}
			this.m_rawData = array;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> class.</summary>
		// Token: 0x06002A4D RID: 10829 RVA: 0x000C0F9A File Offset: 0x000BF19A
		protected X509Extension()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> class.</summary>
		/// <param name="oid">A string representing the object identifier.</param>
		/// <param name="rawData">The encoded data used to create the extension.</param>
		/// <param name="critical">
		///   <see langword="true" /> if the extension is critical; otherwise <see langword="false" />.</param>
		// Token: 0x06002A4E RID: 10830 RVA: 0x000C0FA2 File Offset: 0x000BF1A2
		public X509Extension(string oid, byte[] rawData, bool critical)
			: this(new Oid(oid, OidGroup.ExtensionOrAttribute, true), rawData, critical)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> class.</summary>
		/// <param name="encodedExtension">The encoded data to be used to create the extension.</param>
		/// <param name="critical">
		///   <see langword="true" /> if the extension is critical; otherwise <see langword="false" />.</param>
		// Token: 0x06002A4F RID: 10831 RVA: 0x000C0FB4 File Offset: 0x000BF1B4
		public X509Extension(AsnEncodedData encodedExtension, bool critical)
			: this(encodedExtension.Oid, encodedExtension.RawData, critical)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> class.</summary>
		/// <param name="oid">The object identifier used to identify the extension.</param>
		/// <param name="rawData">The encoded data used to create the extension.</param>
		/// <param name="critical">
		///   <see langword="true" /> if the extension is critical; otherwise <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="oid" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="oid" /> is an empty string ("").</exception>
		// Token: 0x06002A50 RID: 10832 RVA: 0x000C0FCC File Offset: 0x000BF1CC
		public X509Extension(Oid oid, byte[] rawData, bool critical)
			: base(oid, rawData)
		{
			if (base.Oid == null || base.Oid.Value == null)
			{
				throw new ArgumentNullException("oid");
			}
			if (base.Oid.Value.Length == 0)
			{
				throw new ArgumentException(SR.GetString("Arg_EmptyOrNullString"), "oid.Value");
			}
			this.m_critical = critical;
		}

		/// <summary>Gets a Boolean value indicating whether the extension is critical.</summary>
		/// <returns>
		///   <see langword="true" /> if the extension is critical; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x06002A51 RID: 10833 RVA: 0x000C102F File Offset: 0x000BF22F
		// (set) Token: 0x06002A52 RID: 10834 RVA: 0x000C1037 File Offset: 0x000BF237
		public bool Critical
		{
			get
			{
				return this.m_critical;
			}
			set
			{
				this.m_critical = value;
			}
		}

		/// <summary>Copies the extension properties of the specified <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</summary>
		/// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> to be copied.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asnEncodedData" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asnEncodedData" /> does not have a valid X.509 extension.</exception>
		// Token: 0x06002A53 RID: 10835 RVA: 0x000C1040 File Offset: 0x000BF240
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			if (asnEncodedData == null)
			{
				throw new ArgumentNullException("asnEncodedData");
			}
			X509Extension x509Extension = asnEncodedData as X509Extension;
			if (x509Extension == null)
			{
				throw new ArgumentException(SR.GetString("Cryptography_X509_ExtensionMismatch"));
			}
			base.CopyFrom(asnEncodedData);
			this.m_critical = x509Extension.Critical;
		}

		// Token: 0x04002612 RID: 9746
		private bool m_critical;
	}
}
