using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Defines the constraints set on a certificate. This class cannot be inherited.</summary>
	// Token: 0x02000477 RID: 1143
	public sealed class X509BasicConstraintsExtension : X509Extension
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509BasicConstraintsExtension" /> class.</summary>
		// Token: 0x06002A5B RID: 10843 RVA: 0x000C11F0 File Offset: 0x000BF3F0
		public X509BasicConstraintsExtension()
			: base("2.5.29.19")
		{
			this.m_decoded = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509BasicConstraintsExtension" /> class. Parameters specify a value that indicates whether a certificate is a certificate authority (CA) certificate, a value that indicates whether the certificate has a restriction on the number of path levels it allows, the number of levels allowed in a certificate's path, and a value that indicates whether the extension is critical.</summary>
		/// <param name="certificateAuthority">
		///   <see langword="true" /> if the certificate is a certificate authority (CA) certificate; otherwise, <see langword="false" />.</param>
		/// <param name="hasPathLengthConstraint">
		///   <see langword="true" /> if the certificate has a restriction on the number of path levels it allows; otherwise, <see langword="false" />.</param>
		/// <param name="pathLengthConstraint">The number of levels allowed in a certificate's path.</param>
		/// <param name="critical">
		///   <see langword="true" /> if the extension is critical; otherwise, <see langword="false" />.</param>
		// Token: 0x06002A5C RID: 10844 RVA: 0x000C1204 File Offset: 0x000BF404
		public X509BasicConstraintsExtension(bool certificateAuthority, bool hasPathLengthConstraint, int pathLengthConstraint, bool critical)
			: base("2.5.29.19", X509BasicConstraintsExtension.EncodeExtension(certificateAuthority, hasPathLengthConstraint, pathLengthConstraint), critical)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509BasicConstraintsExtension" /> class using an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object and a value that identifies whether the extension is critical.</summary>
		/// <param name="encodedBasicConstraints">The encoded data to use to create the extension.</param>
		/// <param name="critical">
		///   <see langword="true" /> if the extension is critical; otherwise, <see langword="false" />.</param>
		// Token: 0x06002A5D RID: 10845 RVA: 0x000C121B File Offset: 0x000BF41B
		public X509BasicConstraintsExtension(AsnEncodedData encodedBasicConstraints, bool critical)
			: base("2.5.29.19", encodedBasicConstraints.RawData, critical)
		{
		}

		/// <summary>Gets a value indicating whether a certificate is a certificate authority (CA) certificate.</summary>
		/// <returns>
		///   <see langword="true" /> if the certificate is a certificate authority (CA) certificate, otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06002A5E RID: 10846 RVA: 0x000C122F File Offset: 0x000BF42F
		public bool CertificateAuthority
		{
			get
			{
				if (!this.m_decoded)
				{
					this.DecodeExtension();
				}
				return this.m_isCA;
			}
		}

		/// <summary>Gets a value indicating whether a certificate has a restriction on the number of path levels it allows.</summary>
		/// <returns>
		///   <see langword="true" /> if the certificate has a restriction on the number of path levels it allows, otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The extension cannot be decoded.</exception>
		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06002A5F RID: 10847 RVA: 0x000C1245 File Offset: 0x000BF445
		public bool HasPathLengthConstraint
		{
			get
			{
				if (!this.m_decoded)
				{
					this.DecodeExtension();
				}
				return this.m_hasPathLenConstraint;
			}
		}

		/// <summary>Gets the number of levels allowed in a certificate's path.</summary>
		/// <returns>An integer indicating the number of levels allowed in a certificate's path.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The extension cannot be decoded.</exception>
		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x06002A60 RID: 10848 RVA: 0x000C125B File Offset: 0x000BF45B
		public int PathLengthConstraint
		{
			get
			{
				if (!this.m_decoded)
				{
					this.DecodeExtension();
				}
				return this.m_pathLenConstraint;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509BasicConstraintsExtension" /> class using an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</summary>
		/// <param name="asnEncodedData">The encoded data to use to create the extension.</param>
		// Token: 0x06002A61 RID: 10849 RVA: 0x000C1271 File Offset: 0x000BF471
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			base.CopyFrom(asnEncodedData);
			this.m_decoded = false;
		}

		// Token: 0x06002A62 RID: 10850 RVA: 0x000C1284 File Offset: 0x000BF484
		private void DecodeExtension()
		{
			uint num = 0U;
			SafeLocalAllocHandle safeLocalAllocHandle = null;
			if (base.Oid.Value == "2.5.29.10")
			{
				if (!CAPI.DecodeObject(new IntPtr(13L), this.m_rawData, out safeLocalAllocHandle, out num))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
				CAPIBase.CERT_BASIC_CONSTRAINTS_INFO cert_BASIC_CONSTRAINTS_INFO = (CAPIBase.CERT_BASIC_CONSTRAINTS_INFO)Marshal.PtrToStructure(safeLocalAllocHandle.DangerousGetHandle(), typeof(CAPIBase.CERT_BASIC_CONSTRAINTS_INFO));
				byte[] array = new byte[1];
				Marshal.Copy(cert_BASIC_CONSTRAINTS_INFO.SubjectType.pbData, array, 0, 1);
				this.m_isCA = (array[0] & 128) != 0;
				this.m_hasPathLenConstraint = cert_BASIC_CONSTRAINTS_INFO.fPathLenConstraint;
				this.m_pathLenConstraint = (int)cert_BASIC_CONSTRAINTS_INFO.dwPathLenConstraint;
			}
			else
			{
				if (!CAPI.DecodeObject(new IntPtr(15L), this.m_rawData, out safeLocalAllocHandle, out num))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
				CAPIBase.CERT_BASIC_CONSTRAINTS2_INFO cert_BASIC_CONSTRAINTS2_INFO = (CAPIBase.CERT_BASIC_CONSTRAINTS2_INFO)Marshal.PtrToStructure(safeLocalAllocHandle.DangerousGetHandle(), typeof(CAPIBase.CERT_BASIC_CONSTRAINTS2_INFO));
				this.m_isCA = cert_BASIC_CONSTRAINTS2_INFO.fCA != 0;
				this.m_hasPathLenConstraint = cert_BASIC_CONSTRAINTS2_INFO.fPathLenConstraint != 0;
				this.m_pathLenConstraint = (int)cert_BASIC_CONSTRAINTS2_INFO.dwPathLenConstraint;
			}
			this.m_decoded = true;
			safeLocalAllocHandle.Dispose();
		}

		// Token: 0x06002A63 RID: 10851 RVA: 0x000C13C0 File Offset: 0x000BF5C0
		private unsafe static byte[] EncodeExtension(bool certificateAuthority, bool hasPathLengthConstraint, int pathLengthConstraint)
		{
			CAPIBase.CERT_BASIC_CONSTRAINTS2_INFO cert_BASIC_CONSTRAINTS2_INFO = default(CAPIBase.CERT_BASIC_CONSTRAINTS2_INFO);
			cert_BASIC_CONSTRAINTS2_INFO.fCA = (certificateAuthority ? 1 : 0);
			cert_BASIC_CONSTRAINTS2_INFO.fPathLenConstraint = (hasPathLengthConstraint ? 1 : 0);
			if (hasPathLengthConstraint)
			{
				if (pathLengthConstraint < 0)
				{
					throw new ArgumentOutOfRangeException("pathLengthConstraint", SR.GetString("Arg_OutOfRange_NeedNonNegNum"));
				}
				cert_BASIC_CONSTRAINTS2_INFO.dwPathLenConstraint = (uint)pathLengthConstraint;
			}
			byte[] array = null;
			if (!CAPI.EncodeObject("2.5.29.19", new IntPtr((void*)(&cert_BASIC_CONSTRAINTS2_INFO)), out array))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			return array;
		}

		// Token: 0x04002620 RID: 9760
		private bool m_isCA;

		// Token: 0x04002621 RID: 9761
		private bool m_hasPathLenConstraint;

		// Token: 0x04002622 RID: 9762
		private int m_pathLenConstraint;

		// Token: 0x04002623 RID: 9763
		private bool m_decoded;
	}
}
