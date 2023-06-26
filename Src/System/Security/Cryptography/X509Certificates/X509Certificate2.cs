using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents an X.509 certificate.</summary>
	// Token: 0x02000466 RID: 1126
	[Serializable]
	public class X509Certificate2 : X509Certificate
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class.</summary>
		// Token: 0x060029A9 RID: 10665 RVA: 0x000BD00A File Offset: 0x000BB20A
		public X509Certificate2()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using information from a byte array.</summary>
		/// <param name="rawData">A byte array containing data from an X.509 certificate.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:  
		///
		/// The certificate file does not exist.  
		///
		/// The certificate is invalid.  
		///
		/// The certificate's password is incorrect.</exception>
		// Token: 0x060029AA RID: 10666 RVA: 0x000BD01D File Offset: 0x000BB21D
		public X509Certificate2(byte[] rawData)
			: base(rawData)
		{
			this.m_safeCertContext = CAPI.CertDuplicateCertificateContext(base.Handle);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using a byte array and a password.</summary>
		/// <param name="rawData">A byte array containing data from an X.509 certificate.</param>
		/// <param name="password">The password required to access the X.509 certificate data.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:  
		///
		/// The certificate file does not exist.  
		///
		/// The certificate is invalid.  
		///
		/// The certificate's password is incorrect.</exception>
		// Token: 0x060029AB RID: 10667 RVA: 0x000BD042 File Offset: 0x000BB242
		public X509Certificate2(byte[] rawData, string password)
			: base(rawData, password)
		{
			this.m_safeCertContext = CAPI.CertDuplicateCertificateContext(base.Handle);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using a byte array and a password.</summary>
		/// <param name="rawData">A byte array that contains data from an X.509 certificate.</param>
		/// <param name="password">The password required to access the X.509 certificate data.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:  
		///
		/// The certificate file does not exist.  
		///
		/// The certificate is invalid.  
		///
		/// The certificate's password is incorrect.</exception>
		// Token: 0x060029AC RID: 10668 RVA: 0x000BD068 File Offset: 0x000BB268
		public X509Certificate2(byte[] rawData, SecureString password)
			: base(rawData, password)
		{
			this.m_safeCertContext = CAPI.CertDuplicateCertificateContext(base.Handle);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using a byte array, a password, and a key storage flag.</summary>
		/// <param name="rawData">A byte array containing data from an X.509 certificate.</param>
		/// <param name="password">The password required to access the X.509 certificate data.</param>
		/// <param name="keyStorageFlags">A bitwise combination of the enumeration values that control where and how to import the certificate.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:  
		///
		/// The certificate file does not exist.  
		///
		/// The certificate is invalid.  
		///
		/// The certificate's password is incorrect.</exception>
		// Token: 0x060029AD RID: 10669 RVA: 0x000BD08E File Offset: 0x000BB28E
		public X509Certificate2(byte[] rawData, string password, X509KeyStorageFlags keyStorageFlags)
			: base(rawData, password, keyStorageFlags)
		{
			this.m_safeCertContext = CAPI.CertDuplicateCertificateContext(base.Handle);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using a byte array, a password, and a key storage flag.</summary>
		/// <param name="rawData">A byte array that contains data from an X.509 certificate.</param>
		/// <param name="password">The password required to access the X.509 certificate data.</param>
		/// <param name="keyStorageFlags">A bitwise combination of the enumeration values that control where and how to import the certificate.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:  
		///
		/// The certificate file does not exist.  
		///
		/// The certificate is invalid.  
		///
		/// The certificate's password is incorrect.</exception>
		// Token: 0x060029AE RID: 10670 RVA: 0x000BD0B5 File Offset: 0x000BB2B5
		public X509Certificate2(byte[] rawData, SecureString password, X509KeyStorageFlags keyStorageFlags)
			: base(rawData, password, keyStorageFlags)
		{
			this.m_safeCertContext = CAPI.CertDuplicateCertificateContext(base.Handle);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using a certificate file name.</summary>
		/// <param name="fileName">The name of a certificate file.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:  
		///
		/// The certificate file does not exist.  
		///
		/// The certificate is invalid.  
		///
		/// The certificate's password is incorrect.</exception>
		// Token: 0x060029AF RID: 10671 RVA: 0x000BD0DC File Offset: 0x000BB2DC
		public X509Certificate2(string fileName)
			: base(fileName)
		{
			this.m_safeCertContext = CAPI.CertDuplicateCertificateContext(base.Handle);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using a certificate file name and a password used to access the certificate.</summary>
		/// <param name="fileName">The name of a certificate file.</param>
		/// <param name="password">The password required to access the X.509 certificate data.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:  
		///
		/// The certificate file does not exist.  
		///
		/// The certificate is invalid.  
		///
		/// The certificate's password is incorrect.</exception>
		// Token: 0x060029B0 RID: 10672 RVA: 0x000BD101 File Offset: 0x000BB301
		public X509Certificate2(string fileName, string password)
			: base(fileName, password)
		{
			this.m_safeCertContext = CAPI.CertDuplicateCertificateContext(base.Handle);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using a certificate file name and a password.</summary>
		/// <param name="fileName">The name of a certificate file.</param>
		/// <param name="password">The password required to access the X.509 certificate data.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:  
		///
		/// The certificate file does not exist.  
		///
		/// The certificate is invalid.  
		///
		/// The certificate's password is incorrect.</exception>
		// Token: 0x060029B1 RID: 10673 RVA: 0x000BD127 File Offset: 0x000BB327
		public X509Certificate2(string fileName, SecureString password)
			: base(fileName, password)
		{
			this.m_safeCertContext = CAPI.CertDuplicateCertificateContext(base.Handle);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using a certificate file name, a password used to access the certificate, and a key storage flag.</summary>
		/// <param name="fileName">The name of a certificate file.</param>
		/// <param name="password">The password required to access the X.509 certificate data.</param>
		/// <param name="keyStorageFlags">A bitwise combination of the enumeration values that control where and how to import the certificate.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:  
		///
		/// The certificate file does not exist.  
		///
		/// The certificate is invalid.  
		///
		/// The certificate's password is incorrect.</exception>
		// Token: 0x060029B2 RID: 10674 RVA: 0x000BD14D File Offset: 0x000BB34D
		public X509Certificate2(string fileName, string password, X509KeyStorageFlags keyStorageFlags)
			: base(fileName, password, keyStorageFlags)
		{
			this.m_safeCertContext = CAPI.CertDuplicateCertificateContext(base.Handle);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using a certificate file name, a password, and a key storage flag.</summary>
		/// <param name="fileName">The name of a certificate file.</param>
		/// <param name="password">The password required to access the X.509 certificate data.</param>
		/// <param name="keyStorageFlags">A bitwise combination of the enumeration values that control where and how to import the certificate.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:  
		///
		/// The certificate file does not exist.  
		///
		/// The certificate is invalid.  
		///
		/// The certificate's password is incorrect.</exception>
		// Token: 0x060029B3 RID: 10675 RVA: 0x000BD174 File Offset: 0x000BB374
		public X509Certificate2(string fileName, SecureString password, X509KeyStorageFlags keyStorageFlags)
			: base(fileName, password, keyStorageFlags)
		{
			this.m_safeCertContext = CAPI.CertDuplicateCertificateContext(base.Handle);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using an unmanaged handle.</summary>
		/// <param name="handle">A pointer to a certificate context in unmanaged code. The C structure is called <see langword="PCCERT_CONTEXT" />.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:  
		///
		/// The certificate file does not exist.  
		///
		/// The certificate is invalid.  
		///
		/// The certificate's password is incorrect.</exception>
		// Token: 0x060029B4 RID: 10676 RVA: 0x000BD19B File Offset: 0x000BB39B
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public X509Certificate2(IntPtr handle)
			: base(handle)
		{
			this.m_safeCertContext = CAPI.CertDuplicateCertificateContext(base.Handle);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object.</summary>
		/// <param name="certificate">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:  
		///
		/// The certificate file does not exist.  
		///
		/// The certificate is invalid.  
		///
		/// The certificate's password is incorrect.</exception>
		// Token: 0x060029B5 RID: 10677 RVA: 0x000BD1C0 File Offset: 0x000BB3C0
		public X509Certificate2(X509Certificate certificate)
			: base(certificate)
		{
			this.m_safeCertContext = CAPI.CertDuplicateCertificateContext(base.Handle);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using the specified serialization and stream context information.</summary>
		/// <param name="info">The serialization information required to deserialize the new <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" />.</param>
		/// <param name="context">Contextual information about the source of the stream to be deserialized.</param>
		// Token: 0x060029B6 RID: 10678 RVA: 0x000BD1E5 File Offset: 0x000BB3E5
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected X509Certificate2(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.m_safeCertContext = CAPI.CertDuplicateCertificateContext(base.Handle);
		}

		/// <summary>Displays an X.509 certificate in text format.</summary>
		/// <returns>The certificate information.</returns>
		// Token: 0x060029B7 RID: 10679 RVA: 0x000BD20B File Offset: 0x000BB40B
		public override string ToString()
		{
			return base.ToString(true);
		}

		/// <summary>Displays an X.509 certificate in text format.</summary>
		/// <param name="verbose">
		///   <see langword="true" /> to display the public key, private key, extensions, and so forth; <see langword="false" /> to display information that is similar to the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class, including thumbprint, serial number, subject and issuer names, and so on.</param>
		/// <returns>The certificate information.</returns>
		// Token: 0x060029B8 RID: 10680 RVA: 0x000BD214 File Offset: 0x000BB414
		public override string ToString(bool verbose)
		{
			if (!verbose || this.m_safeCertContext.IsInvalid)
			{
				return this.ToString();
			}
			StringBuilder stringBuilder = new StringBuilder();
			string newLine = Environment.NewLine;
			string text = newLine + newLine;
			string text2 = newLine + "  ";
			stringBuilder.Append("[Version]");
			stringBuilder.Append(text2);
			stringBuilder.Append("V" + this.Version.ToString());
			stringBuilder.Append(text);
			stringBuilder.Append("[Subject]");
			stringBuilder.Append(text2);
			stringBuilder.Append(this.SubjectName.Name);
			string text3 = this.GetNameInfo(X509NameType.SimpleName, false);
			if (text3.Length > 0)
			{
				stringBuilder.Append(text2);
				stringBuilder.Append("Simple Name: ");
				stringBuilder.Append(text3);
			}
			string text4 = this.GetNameInfo(X509NameType.EmailName, false);
			if (text4.Length > 0)
			{
				stringBuilder.Append(text2);
				stringBuilder.Append("Email Name: ");
				stringBuilder.Append(text4);
			}
			string text5 = this.GetNameInfo(X509NameType.UpnName, false);
			if (text5.Length > 0)
			{
				stringBuilder.Append(text2);
				stringBuilder.Append("UPN Name: ");
				stringBuilder.Append(text5);
			}
			string text6 = this.GetNameInfo(X509NameType.DnsName, false);
			if (text6.Length > 0)
			{
				stringBuilder.Append(text2);
				stringBuilder.Append("DNS Name: ");
				stringBuilder.Append(text6);
			}
			stringBuilder.Append(text);
			stringBuilder.Append("[Issuer]");
			stringBuilder.Append(text2);
			stringBuilder.Append(this.IssuerName.Name);
			text3 = this.GetNameInfo(X509NameType.SimpleName, true);
			if (text3.Length > 0)
			{
				stringBuilder.Append(text2);
				stringBuilder.Append("Simple Name: ");
				stringBuilder.Append(text3);
			}
			text4 = this.GetNameInfo(X509NameType.EmailName, true);
			if (text4.Length > 0)
			{
				stringBuilder.Append(text2);
				stringBuilder.Append("Email Name: ");
				stringBuilder.Append(text4);
			}
			text5 = this.GetNameInfo(X509NameType.UpnName, true);
			if (text5.Length > 0)
			{
				stringBuilder.Append(text2);
				stringBuilder.Append("UPN Name: ");
				stringBuilder.Append(text5);
			}
			text6 = this.GetNameInfo(X509NameType.DnsName, true);
			if (text6.Length > 0)
			{
				stringBuilder.Append(text2);
				stringBuilder.Append("DNS Name: ");
				stringBuilder.Append(text6);
			}
			stringBuilder.Append(text);
			stringBuilder.Append("[Serial Number]");
			stringBuilder.Append(text2);
			stringBuilder.Append(this.SerialNumber);
			stringBuilder.Append(text);
			stringBuilder.Append("[Not Before]");
			stringBuilder.Append(text2);
			stringBuilder.Append(X509Certificate.FormatDate(this.NotBefore));
			stringBuilder.Append(text);
			stringBuilder.Append("[Not After]");
			stringBuilder.Append(text2);
			stringBuilder.Append(X509Certificate.FormatDate(this.NotAfter));
			stringBuilder.Append(text);
			stringBuilder.Append("[Thumbprint]");
			stringBuilder.Append(text2);
			stringBuilder.Append(this.Thumbprint);
			stringBuilder.Append(text);
			stringBuilder.Append("[Signature Algorithm]");
			stringBuilder.Append(text2);
			stringBuilder.Append(this.SignatureAlgorithm.FriendlyName + "(" + this.SignatureAlgorithm.Value + ")");
			stringBuilder.Append(text);
			stringBuilder.Append("[Public Key]");
			try
			{
				PublicKey publicKey = this.PublicKey;
				string text7 = publicKey.Oid.FriendlyName;
				stringBuilder.Append(text2);
				stringBuilder.Append("Algorithm: ");
				stringBuilder.Append(text7);
				try
				{
					text7 = publicKey.Key.KeySize.ToString();
					stringBuilder.Append(text2);
					stringBuilder.Append("Length: ");
					stringBuilder.Append(text7);
				}
				catch (NotSupportedException)
				{
				}
				text7 = publicKey.EncodedKeyValue.Format(true);
				stringBuilder.Append(text2);
				stringBuilder.Append("Key Blob: ");
				stringBuilder.Append(text7);
				text7 = publicKey.EncodedParameters.Format(true);
				stringBuilder.Append(text2);
				stringBuilder.Append("Parameters: ");
				stringBuilder.Append(text7);
			}
			catch (CryptographicException)
			{
			}
			this.AppendPrivateKeyInfo(stringBuilder);
			X509ExtensionCollection extensions = this.Extensions;
			if (extensions.Count > 0)
			{
				stringBuilder.Append(text);
				stringBuilder.Append("[Extensions]");
				foreach (X509Extension x509Extension in extensions)
				{
					try
					{
						string text8 = x509Extension.Oid.FriendlyName;
						stringBuilder.Append(newLine);
						stringBuilder.Append("* " + text8);
						stringBuilder.Append("(" + x509Extension.Oid.Value + "):");
						text8 = x509Extension.Format(true);
						stringBuilder.Append(text2);
						stringBuilder.Append(text8);
					}
					catch (CryptographicException)
					{
					}
				}
			}
			stringBuilder.Append(newLine);
			return stringBuilder.ToString();
		}

		/// <summary>Gets or sets a value indicating that an X.509 certificate is archived.</summary>
		/// <returns>
		///   <see langword="true" /> if the certificate is archived, <see langword="false" /> if the certificate is not archived.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate is unreadable.</exception>
		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x060029B9 RID: 10681 RVA: 0x000BD734 File Offset: 0x000BB934
		// (set) Token: 0x060029BA RID: 10682 RVA: 0x000BD77C File Offset: 0x000BB97C
		public bool Archived
		{
			get
			{
				if (this.m_safeCertContext.IsInvalid)
				{
					throw new CryptographicException(SR.GetString("Cryptography_InvalidHandle"), "m_safeCertContext");
				}
				uint num = 0U;
				return CAPISafe.CertGetCertificateContextProperty(this.m_safeCertContext, 19U, SafeLocalAllocHandle.InvalidHandle, ref num);
			}
			set
			{
				SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
				if (value)
				{
					safeLocalAllocHandle = CAPI.LocalAlloc(64U, new IntPtr(Marshal.SizeOf(typeof(CAPIBase.CRYPTOAPI_BLOB))));
				}
				if (!CAPI.CertSetCertificateContextProperty(this.m_safeCertContext, 19U, 0U, safeLocalAllocHandle))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
				safeLocalAllocHandle.Dispose();
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> objects.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate is unreadable.</exception>
		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x060029BB RID: 10683 RVA: 0x000BD7D0 File Offset: 0x000BB9D0
		public X509ExtensionCollection Extensions
		{
			get
			{
				if (this.m_safeCertContext.IsInvalid)
				{
					throw new CryptographicException(SR.GetString("Cryptography_InvalidHandle"), "m_safeCertContext");
				}
				if (this.m_extensions == null)
				{
					this.m_extensions = new X509ExtensionCollection(this.m_safeCertContext);
				}
				return this.m_extensions;
			}
		}

		/// <summary>Gets or sets the associated alias for a certificate.</summary>
		/// <returns>The certificate's friendly name.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate is unreadable.</exception>
		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x060029BC RID: 10684 RVA: 0x000BD820 File Offset: 0x000BBA20
		// (set) Token: 0x060029BD RID: 10685 RVA: 0x000BD8A8 File Offset: 0x000BBAA8
		public string FriendlyName
		{
			get
			{
				if (this.m_safeCertContext.IsInvalid)
				{
					throw new CryptographicException(SR.GetString("Cryptography_InvalidHandle"), "m_safeCertContext");
				}
				SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
				uint num = 0U;
				if (!CAPISafe.CertGetCertificateContextProperty(this.m_safeCertContext, 11U, safeLocalAllocHandle, ref num))
				{
					return string.Empty;
				}
				safeLocalAllocHandle = CAPI.LocalAlloc(0U, new IntPtr((long)((ulong)num)));
				if (!CAPISafe.CertGetCertificateContextProperty(this.m_safeCertContext, 11U, safeLocalAllocHandle, ref num))
				{
					return string.Empty;
				}
				string text = Marshal.PtrToStringUni(safeLocalAllocHandle.DangerousGetHandle());
				safeLocalAllocHandle.Dispose();
				return text;
			}
			set
			{
				if (this.m_safeCertContext.IsInvalid)
				{
					throw new CryptographicException(SR.GetString("Cryptography_InvalidHandle"), "m_safeCertContext");
				}
				if (value == null)
				{
					value = string.Empty;
				}
				X509Certificate2.SetFriendlyNameExtendedProperty(this.m_safeCertContext, value);
			}
		}

		/// <summary>Gets the distinguished name of the certificate issuer.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X500DistinguishedName" /> object that contains the name of the certificate issuer.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate context is invalid.</exception>
		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x060029BE RID: 10686 RVA: 0x000BD8E4 File Offset: 0x000BBAE4
		public unsafe X500DistinguishedName IssuerName
		{
			get
			{
				if (this.m_safeCertContext.IsInvalid)
				{
					throw new CryptographicException(SR.GetString("Cryptography_InvalidHandle"), "m_safeCertContext");
				}
				if (this.m_issuerName == null)
				{
					CAPIBase.CERT_CONTEXT cert_CONTEXT = *(CAPIBase.CERT_CONTEXT*)(void*)this.m_safeCertContext.DangerousGetHandle();
					CAPIBase.CERT_INFO cert_INFO = (CAPIBase.CERT_INFO)Marshal.PtrToStructure(cert_CONTEXT.pCertInfo, typeof(CAPIBase.CERT_INFO));
					this.m_issuerName = new X500DistinguishedName(cert_INFO.Issuer);
				}
				return this.m_issuerName;
			}
		}

		/// <summary>Gets the date in local time after which a certificate is no longer valid.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> object that represents the expiration date for the certificate.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate is unreadable.</exception>
		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x060029BF RID: 10687 RVA: 0x000BD964 File Offset: 0x000BBB64
		public unsafe DateTime NotAfter
		{
			get
			{
				if (this.m_safeCertContext.IsInvalid)
				{
					throw new CryptographicException(SR.GetString("Cryptography_InvalidHandle"), "m_safeCertContext");
				}
				if (this.m_notAfter == DateTime.MinValue)
				{
					CAPIBase.CERT_CONTEXT cert_CONTEXT = *(CAPIBase.CERT_CONTEXT*)(void*)this.m_safeCertContext.DangerousGetHandle();
					CAPIBase.CERT_INFO cert_INFO = (CAPIBase.CERT_INFO)Marshal.PtrToStructure(cert_CONTEXT.pCertInfo, typeof(CAPIBase.CERT_INFO));
					long num = (long)(((ulong)cert_INFO.NotAfter.dwHighDateTime << 32) | (ulong)cert_INFO.NotAfter.dwLowDateTime);
					this.m_notAfter = DateTime.FromFileTime(num);
				}
				return this.m_notAfter;
			}
		}

		/// <summary>Gets the date in local time on which a certificate becomes valid.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> object that represents the effective date of the certificate.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate is unreadable.</exception>
		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x060029C0 RID: 10688 RVA: 0x000BDA08 File Offset: 0x000BBC08
		public unsafe DateTime NotBefore
		{
			get
			{
				if (this.m_safeCertContext.IsInvalid)
				{
					throw new CryptographicException(SR.GetString("Cryptography_InvalidHandle"), "m_safeCertContext");
				}
				if (this.m_notBefore == DateTime.MinValue)
				{
					CAPIBase.CERT_CONTEXT cert_CONTEXT = *(CAPIBase.CERT_CONTEXT*)(void*)this.m_safeCertContext.DangerousGetHandle();
					CAPIBase.CERT_INFO cert_INFO = (CAPIBase.CERT_INFO)Marshal.PtrToStructure(cert_CONTEXT.pCertInfo, typeof(CAPIBase.CERT_INFO));
					long num = (long)(((ulong)cert_INFO.NotBefore.dwHighDateTime << 32) | (ulong)cert_INFO.NotBefore.dwLowDateTime);
					this.m_notBefore = DateTime.FromFileTime(num);
				}
				return this.m_notBefore;
			}
		}

		/// <summary>Gets a value that indicates whether an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object contains a private key.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object contains a private key; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate context is invalid.</exception>
		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x060029C1 RID: 10689 RVA: 0x000BDAAC File Offset: 0x000BBCAC
		public bool HasPrivateKey
		{
			get
			{
				if (this.m_safeCertContext.IsInvalid)
				{
					throw new CryptographicException(SR.GetString("Cryptography_InvalidHandle"), "m_safeCertContext");
				}
				uint num = 0U;
				bool flag;
				using (SafeLocalAllocHandle invalidHandle = SafeLocalAllocHandle.InvalidHandle)
				{
					flag = CAPISafe.CertGetCertificateContextProperty(this.m_safeCertContext, 5U, invalidHandle, ref num);
					if (!flag)
					{
						flag = CAPISafe.CertGetCertificateContextProperty(this.m_safeCertContext, 2U, invalidHandle, ref num);
					}
				}
				return flag;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> object that represents the private key associated with a certificate.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> object, which is either an RSA or DSA cryptographic service provider.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key value is not an RSA or DSA key, or the key is unreadable.</exception>
		/// <exception cref="T:System.ArgumentNullException">The value being set for this property is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The key algorithm for this private key is not supported.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">The X.509 keys do not match.</exception>
		/// <exception cref="T:System.ArgumentException">The cryptographic service provider key is <see langword="null" />.</exception>
		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x060029C2 RID: 10690 RVA: 0x000BDB24 File Offset: 0x000BBD24
		// (set) Token: 0x060029C3 RID: 10691 RVA: 0x000BDBC0 File Offset: 0x000BBDC0
		public AsymmetricAlgorithm PrivateKey
		{
			get
			{
				if (!this.HasPrivateKey)
				{
					return null;
				}
				if (this.m_privateKey == null)
				{
					CspParameters cspParameters = new CspParameters();
					if (!X509Certificate2.GetPrivateKeyInfo(this.m_safeCertContext, ref cspParameters))
					{
						return null;
					}
					cspParameters.Flags |= CspProviderFlags.UseExistingKey;
					uint algorithmId = this.PublicKey.AlgorithmId;
					if (algorithmId != 8704U)
					{
						if (algorithmId != 9216U && algorithmId != 41984U)
						{
							throw new NotSupportedException(SR.GetString("NotSupported_KeyAlgorithm"));
						}
						this.m_privateKey = new RSACryptoServiceProvider(cspParameters);
					}
					else
					{
						this.m_privateKey = new DSACryptoServiceProvider(cspParameters);
					}
				}
				return this.m_privateKey;
			}
			set
			{
				if (this.m_safeCertContext.IsInvalid)
				{
					throw new CryptographicException(SR.GetString("Cryptography_InvalidHandle"), "m_safeCertContext");
				}
				ICspAsymmetricAlgorithm cspAsymmetricAlgorithm = value as ICspAsymmetricAlgorithm;
				if (value != null && cspAsymmetricAlgorithm == null)
				{
					throw new NotSupportedException(SR.GetString("NotSupported_InvalidKeyImpl"));
				}
				if (cspAsymmetricAlgorithm != null)
				{
					if (cspAsymmetricAlgorithm.CspKeyContainerInfo == null)
					{
						throw new ArgumentException("CspKeyContainerInfo");
					}
					if (X509Certificate2.s_publicKeyOffset == 0)
					{
						X509Certificate2.s_publicKeyOffset = Marshal.SizeOf(typeof(CAPIBase.BLOBHEADER));
					}
					ICspAsymmetricAlgorithm cspAsymmetricAlgorithm2 = this.PublicKey.Key as ICspAsymmetricAlgorithm;
					byte[] array = cspAsymmetricAlgorithm2.ExportCspBlob(false);
					byte[] array2 = cspAsymmetricAlgorithm.ExportCspBlob(false);
					if (array == null || array2 == null || array.Length != array2.Length || array.Length <= X509Certificate2.s_publicKeyOffset)
					{
						throw new CryptographicUnexpectedOperationException(SR.GetString("Cryptography_X509_KeyMismatch"));
					}
					for (int i = X509Certificate2.s_publicKeyOffset; i < array.Length; i++)
					{
						if (array[i] != array2[i])
						{
							throw new CryptographicUnexpectedOperationException(SR.GetString("Cryptography_X509_KeyMismatch"));
						}
					}
				}
				X509Certificate2.SetPrivateKeyProperty(this.m_safeCertContext, cspAsymmetricAlgorithm);
				this.m_privateKey = value;
			}
		}

		/// <summary>Gets a <see cref="P:System.Security.Cryptography.X509Certificates.X509Certificate2.PublicKey" /> object associated with a certificate.</summary>
		/// <returns>A <see cref="P:System.Security.Cryptography.X509Certificates.X509Certificate2.PublicKey" /> object.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key value is not an RSA or DSA key, or the key is unreadable.</exception>
		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x060029C4 RID: 10692 RVA: 0x000BDCCC File Offset: 0x000BBECC
		public PublicKey PublicKey
		{
			get
			{
				if (this.m_safeCertContext.IsInvalid)
				{
					throw new CryptographicException(SR.GetString("Cryptography_InvalidHandle"), "m_safeCertContext");
				}
				if (this.m_publicKey == null)
				{
					string keyAlgorithm = this.GetKeyAlgorithm();
					byte[] keyAlgorithmParameters = this.GetKeyAlgorithmParameters();
					byte[] publicKey = this.GetPublicKey();
					Oid oid = new Oid(keyAlgorithm, OidGroup.PublicKeyAlgorithm, true);
					this.m_publicKey = new PublicKey(oid, new AsnEncodedData(oid, keyAlgorithmParameters), new AsnEncodedData(oid, publicKey));
				}
				return this.m_publicKey;
			}
		}

		/// <summary>Gets the raw data of a certificate.</summary>
		/// <returns>The raw data of the certificate as a byte array.</returns>
		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x060029C5 RID: 10693 RVA: 0x000BDD41 File Offset: 0x000BBF41
		public byte[] RawData
		{
			get
			{
				return this.GetRawCertData();
			}
		}

		/// <summary>Gets the serial number of a certificate as a big-endian hexadecimal string.</summary>
		/// <returns>The serial number of the certificate as a big-endian hexadecimal string.</returns>
		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x060029C6 RID: 10694 RVA: 0x000BDD49 File Offset: 0x000BBF49
		public string SerialNumber
		{
			get
			{
				return this.GetSerialNumberString();
			}
		}

		/// <summary>Gets the subject distinguished name from a certificate.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X500DistinguishedName" /> object that represents the name of the certificate subject.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate context is invalid.</exception>
		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x060029C7 RID: 10695 RVA: 0x000BDD54 File Offset: 0x000BBF54
		public unsafe X500DistinguishedName SubjectName
		{
			get
			{
				if (this.m_safeCertContext.IsInvalid)
				{
					throw new CryptographicException(SR.GetString("Cryptography_InvalidHandle"), "m_safeCertContext");
				}
				if (this.m_subjectName == null)
				{
					CAPIBase.CERT_CONTEXT cert_CONTEXT = *(CAPIBase.CERT_CONTEXT*)(void*)this.m_safeCertContext.DangerousGetHandle();
					CAPIBase.CERT_INFO cert_INFO = (CAPIBase.CERT_INFO)Marshal.PtrToStructure(cert_CONTEXT.pCertInfo, typeof(CAPIBase.CERT_INFO));
					this.m_subjectName = new X500DistinguishedName(cert_INFO.Subject);
				}
				return this.m_subjectName;
			}
		}

		/// <summary>Gets the algorithm used to create the signature of a certificate.</summary>
		/// <returns>The object identifier of the signature algorithm.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate is unreadable.</exception>
		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x060029C8 RID: 10696 RVA: 0x000BDDD4 File Offset: 0x000BBFD4
		public Oid SignatureAlgorithm
		{
			get
			{
				if (this.m_safeCertContext.IsInvalid)
				{
					throw new CryptographicException(SR.GetString("Cryptography_InvalidHandle"), "m_safeCertContext");
				}
				if (this.m_signatureAlgorithm == null)
				{
					this.m_signatureAlgorithm = X509Certificate2.GetSignatureAlgorithm(this.m_safeCertContext);
				}
				return this.m_signatureAlgorithm;
			}
		}

		/// <summary>Gets the thumbprint of a certificate.</summary>
		/// <returns>The thumbprint of the certificate.</returns>
		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x060029C9 RID: 10697 RVA: 0x000BDE22 File Offset: 0x000BC022
		public string Thumbprint
		{
			get
			{
				return this.GetCertHashString();
			}
		}

		/// <summary>Gets the X.509 format version of a certificate.</summary>
		/// <returns>The certificate format.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate is unreadable.</exception>
		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x060029CA RID: 10698 RVA: 0x000BDE2C File Offset: 0x000BC02C
		public int Version
		{
			get
			{
				if (this.m_safeCertContext.IsInvalid)
				{
					throw new CryptographicException(SR.GetString("Cryptography_InvalidHandle"), "m_safeCertContext");
				}
				if (this.m_version == 0)
				{
					this.m_version = (int)X509Certificate2.GetVersion(this.m_safeCertContext);
				}
				return this.m_version;
			}
		}

		/// <summary>Gets the subject and issuer names from a certificate.</summary>
		/// <param name="nameType">The <see cref="T:System.Security.Cryptography.X509Certificates.X509NameType" /> value for the subject.</param>
		/// <param name="forIssuer">
		///   <see langword="true" /> to include the issuer name; otherwise, <see langword="false" />.</param>
		/// <returns>The name of the certificate.</returns>
		// Token: 0x060029CB RID: 10699 RVA: 0x000BDE7C File Offset: 0x000BC07C
		public unsafe string GetNameInfo(X509NameType nameType, bool forIssuer)
		{
			uint num = (forIssuer ? 1U : 0U);
			uint num2 = X509Utils.MapNameType(nameType);
			if (num2 == 1U)
			{
				return CAPI.GetCertNameInfo(this.m_safeCertContext, num, num2);
			}
			if (num2 == 4U)
			{
				return CAPI.GetCertNameInfo(this.m_safeCertContext, num, num2);
			}
			string text = string.Empty;
			CAPIBase.CERT_CONTEXT cert_CONTEXT = *(CAPIBase.CERT_CONTEXT*)(void*)this.m_safeCertContext.DangerousGetHandle();
			CAPIBase.CERT_INFO cert_INFO = (CAPIBase.CERT_INFO)Marshal.PtrToStructure(cert_CONTEXT.pCertInfo, typeof(CAPIBase.CERT_INFO));
			IntPtr[] array = new IntPtr[]
			{
				CAPISafe.CertFindExtension(forIssuer ? "2.5.29.8" : "2.5.29.7", cert_INFO.cExtension, cert_INFO.rgExtension),
				CAPISafe.CertFindExtension(forIssuer ? "2.5.29.18" : "2.5.29.17", cert_INFO.cExtension, cert_INFO.rgExtension)
			};
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != IntPtr.Zero)
				{
					CAPIBase.CERT_EXTENSION cert_EXTENSION = (CAPIBase.CERT_EXTENSION)Marshal.PtrToStructure(array[i], typeof(CAPIBase.CERT_EXTENSION));
					byte[] array2 = new byte[cert_EXTENSION.Value.cbData];
					Marshal.Copy(cert_EXTENSION.Value.pbData, array2, 0, array2.Length);
					uint num3 = 0U;
					SafeLocalAllocHandle safeLocalAllocHandle = null;
					SafeLocalAllocHandle safeLocalAllocHandle2 = X509Utils.StringToAnsiPtr(cert_EXTENSION.pszObjId);
					bool flag = CAPI.DecodeObject(safeLocalAllocHandle2.DangerousGetHandle(), array2, out safeLocalAllocHandle, out num3);
					safeLocalAllocHandle2.Dispose();
					if (flag)
					{
						CAPIBase.CERT_ALT_NAME_INFO cert_ALT_NAME_INFO = (CAPIBase.CERT_ALT_NAME_INFO)Marshal.PtrToStructure(safeLocalAllocHandle.DangerousGetHandle(), typeof(CAPIBase.CERT_ALT_NAME_INFO));
						int num4 = 0;
						while ((long)num4 < (long)((ulong)cert_ALT_NAME_INFO.cAltEntry))
						{
							IntPtr intPtr = new IntPtr((long)cert_ALT_NAME_INFO.rgAltEntry + (long)(num4 * Marshal.SizeOf(typeof(CAPIBase.CERT_ALT_NAME_ENTRY))));
							CAPIBase.CERT_ALT_NAME_ENTRY cert_ALT_NAME_ENTRY = (CAPIBase.CERT_ALT_NAME_ENTRY)Marshal.PtrToStructure(intPtr, typeof(CAPIBase.CERT_ALT_NAME_ENTRY));
							switch (num2)
							{
							case 6U:
								if (cert_ALT_NAME_ENTRY.dwAltNameChoice == 3U)
								{
									text = Marshal.PtrToStringUni(cert_ALT_NAME_ENTRY.Value.pwszDNSName);
								}
								break;
							case 7U:
								if (cert_ALT_NAME_ENTRY.dwAltNameChoice == 7U)
								{
									text = Marshal.PtrToStringUni(cert_ALT_NAME_ENTRY.Value.pwszURL);
								}
								break;
							case 8U:
								if (cert_ALT_NAME_ENTRY.dwAltNameChoice == 1U)
								{
									CAPIBase.CERT_OTHER_NAME cert_OTHER_NAME = (CAPIBase.CERT_OTHER_NAME)Marshal.PtrToStructure(cert_ALT_NAME_ENTRY.Value.pOtherName, typeof(CAPIBase.CERT_OTHER_NAME));
									if (cert_OTHER_NAME.pszObjId == "1.3.6.1.4.1.311.20.2.3")
									{
										uint num5 = 0U;
										SafeLocalAllocHandle safeLocalAllocHandle3 = null;
										flag = CAPI.DecodeObject(new IntPtr(24L), X509Utils.PtrToByte(cert_OTHER_NAME.Value.pbData, cert_OTHER_NAME.Value.cbData), out safeLocalAllocHandle3, out num5);
										if (flag)
										{
											CAPIBase.CERT_NAME_VALUE cert_NAME_VALUE = (CAPIBase.CERT_NAME_VALUE)Marshal.PtrToStructure(safeLocalAllocHandle3.DangerousGetHandle(), typeof(CAPIBase.CERT_NAME_VALUE));
											if (X509Utils.IsCertRdnCharString(cert_NAME_VALUE.dwValueType))
											{
												text = Marshal.PtrToStringUni(cert_NAME_VALUE.Value.pbData);
											}
											safeLocalAllocHandle3.Dispose();
										}
									}
								}
								break;
							}
							num4++;
						}
						safeLocalAllocHandle.Dispose();
					}
				}
			}
			if (nameType == X509NameType.DnsName && (text == null || text.Length == 0))
			{
				text = CAPI.GetCertNameInfo(this.m_safeCertContext, num, 3U);
			}
			return text;
		}

		/// <summary>Populates an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object with data from a byte array.</summary>
		/// <param name="rawData">A byte array containing data from an X.509 certificate.</param>
		// Token: 0x060029CC RID: 10700 RVA: 0x000BE1AB File Offset: 0x000BC3AB
		[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
		[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public override void Import(byte[] rawData)
		{
			this.Reset();
			base.Import(rawData);
			this.m_safeCertContext = CAPI.CertDuplicateCertificateContext(base.Handle);
		}

		/// <summary>Populates an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object using data from a byte array, a password, and flags for determining how to import the private key.</summary>
		/// <param name="rawData">A byte array containing data from an X.509 certificate.</param>
		/// <param name="password">The password required to access the X.509 certificate data.</param>
		/// <param name="keyStorageFlags">A bitwise combination of the enumeration values that control where and how to import the certificate.</param>
		// Token: 0x060029CD RID: 10701 RVA: 0x000BE1CB File Offset: 0x000BC3CB
		[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
		[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public override void Import(byte[] rawData, string password, X509KeyStorageFlags keyStorageFlags)
		{
			this.Reset();
			base.Import(rawData, password, keyStorageFlags);
			this.m_safeCertContext = CAPI.CertDuplicateCertificateContext(base.Handle);
		}

		/// <summary>Populates an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object using data from a byte array, a password, and a key storage flag.</summary>
		/// <param name="rawData">A byte array that contains data from an X.509 certificate.</param>
		/// <param name="password">The password required to access the X.509 certificate data.</param>
		/// <param name="keyStorageFlags">A bitwise combination of the enumeration values that control where and how to import the certificate.</param>
		// Token: 0x060029CE RID: 10702 RVA: 0x000BE1ED File Offset: 0x000BC3ED
		[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
		[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public override void Import(byte[] rawData, SecureString password, X509KeyStorageFlags keyStorageFlags)
		{
			this.Reset();
			base.Import(rawData, password, keyStorageFlags);
			this.m_safeCertContext = CAPI.CertDuplicateCertificateContext(base.Handle);
		}

		/// <summary>Populates an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object with information from a certificate file.</summary>
		/// <param name="fileName">The name of a certificate.</param>
		// Token: 0x060029CF RID: 10703 RVA: 0x000BE20F File Offset: 0x000BC40F
		[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
		[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public override void Import(string fileName)
		{
			this.Reset();
			base.Import(fileName);
			this.m_safeCertContext = CAPI.CertDuplicateCertificateContext(base.Handle);
		}

		/// <summary>Populates an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object with information from a certificate file, a password, and a <see cref="T:System.Security.Cryptography.X509Certificates.X509KeyStorageFlags" /> value.</summary>
		/// <param name="fileName">The name of a certificate file.</param>
		/// <param name="password">The password required to access the X.509 certificate data.</param>
		/// <param name="keyStorageFlags">A bitwise combination of the enumeration values that control where and how to import the certificate.</param>
		// Token: 0x060029D0 RID: 10704 RVA: 0x000BE22F File Offset: 0x000BC42F
		[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
		[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public override void Import(string fileName, string password, X509KeyStorageFlags keyStorageFlags)
		{
			this.Reset();
			base.Import(fileName, password, keyStorageFlags);
			this.m_safeCertContext = CAPI.CertDuplicateCertificateContext(base.Handle);
		}

		/// <summary>Populates an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object with information from a certificate file, a password, and a key storage flag.</summary>
		/// <param name="fileName">The name of a certificate file.</param>
		/// <param name="password">The password required to access the X.509 certificate data.</param>
		/// <param name="keyStorageFlags">A bitwise combination of the enumeration values that control where and how to import the certificate.</param>
		// Token: 0x060029D1 RID: 10705 RVA: 0x000BE251 File Offset: 0x000BC451
		[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
		[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public override void Import(string fileName, SecureString password, X509KeyStorageFlags keyStorageFlags)
		{
			this.Reset();
			base.Import(fileName, password, keyStorageFlags);
			this.m_safeCertContext = CAPI.CertDuplicateCertificateContext(base.Handle);
		}

		/// <summary>Resets the state of an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object.</summary>
		// Token: 0x060029D2 RID: 10706 RVA: 0x000BE274 File Offset: 0x000BC474
		[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
		[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public override void Reset()
		{
			this.m_version = 0;
			this.m_notBefore = DateTime.MinValue;
			this.m_notAfter = DateTime.MinValue;
			this.m_privateKey = null;
			this.m_publicKey = null;
			this.m_extensions = null;
			this.m_signatureAlgorithm = null;
			this.m_subjectName = null;
			this.m_issuerName = null;
			if (!this.m_safeCertContext.IsInvalid)
			{
				this.m_safeCertContext.Dispose();
				this.m_safeCertContext = SafeCertContextHandle.InvalidHandle;
			}
			base.Reset();
		}

		/// <summary>Performs a X.509 chain validation using basic validation policy.</summary>
		/// <returns>
		///   <see langword="true" /> if the validation succeeds; <see langword="false" /> if the validation fails.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate is unreadable.</exception>
		// Token: 0x060029D3 RID: 10707 RVA: 0x000BE2F4 File Offset: 0x000BC4F4
		public bool Verify()
		{
			if (this.m_safeCertContext.IsInvalid)
			{
				throw new CryptographicException(SR.GetString("Cryptography_InvalidHandle"), "m_safeCertContext");
			}
			int num = X509Utils.VerifyCertificate(this.CertContext, null, null, X509RevocationMode.Online, X509RevocationFlag.ExcludeRoot, DateTime.Now, new TimeSpan(0, 0, 0), null, new IntPtr(1L), IntPtr.Zero);
			return num == 0;
		}

		/// <summary>Indicates the type of certificate contained in a byte array.</summary>
		/// <param name="rawData">A byte array containing data from an X.509 certificate.</param>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509ContentType" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rawData" /> has a zero length or is <see langword="null" />.</exception>
		// Token: 0x060029D4 RID: 10708 RVA: 0x000BE354 File Offset: 0x000BC554
		public static X509ContentType GetCertContentType(byte[] rawData)
		{
			if (rawData == null || rawData.Length == 0)
			{
				throw new ArgumentException(SR.GetString("Arg_EmptyOrNullArray"), "rawData");
			}
			uint num = X509Certificate2.QueryCertBlobType(rawData);
			return X509Utils.MapContentType(num);
		}

		/// <summary>Indicates the type of certificate contained in a file.</summary>
		/// <param name="fileName">The name of a certificate file.</param>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509ContentType" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is <see langword="null" />.</exception>
		// Token: 0x060029D5 RID: 10709 RVA: 0x000BE38C File Offset: 0x000BC58C
		public static X509ContentType GetCertContentType(string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			string fullPath = Path.GetFullPath(fileName);
			new FileIOPermission(FileIOPermissionAccess.Read, fullPath).Demand();
			uint num = X509Certificate2.QueryCertFileType(fileName);
			return X509Utils.MapContentType(num);
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x060029D6 RID: 10710 RVA: 0x000BE3C7 File Offset: 0x000BC5C7
		internal new SafeCertContextHandle CertContext
		{
			get
			{
				return this.m_safeCertContext;
			}
		}

		// Token: 0x060029D7 RID: 10711 RVA: 0x000BE3D0 File Offset: 0x000BC5D0
		internal static bool GetPrivateKeyInfo(SafeCertContextHandle safeCertContext, ref CspParameters parameters)
		{
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			uint num = 0U;
			if (!CAPISafe.CertGetCertificateContextProperty(safeCertContext, 2U, safeLocalAllocHandle, ref num))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error == -2146885628)
				{
					return false;
				}
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			else
			{
				safeLocalAllocHandle = CAPI.LocalAlloc(0U, new IntPtr((long)((ulong)num)));
				if (CAPISafe.CertGetCertificateContextProperty(safeCertContext, 2U, safeLocalAllocHandle, ref num))
				{
					CAPIBase.CRYPT_KEY_PROV_INFO crypt_KEY_PROV_INFO = (CAPIBase.CRYPT_KEY_PROV_INFO)Marshal.PtrToStructure(safeLocalAllocHandle.DangerousGetHandle(), typeof(CAPIBase.CRYPT_KEY_PROV_INFO));
					parameters.ProviderName = crypt_KEY_PROV_INFO.pwszProvName;
					parameters.KeyContainerName = crypt_KEY_PROV_INFO.pwszContainerName;
					parameters.ProviderType = (int)crypt_KEY_PROV_INFO.dwProvType;
					parameters.KeyNumber = (int)crypt_KEY_PROV_INFO.dwKeySpec;
					parameters.Flags = (((crypt_KEY_PROV_INFO.dwFlags & 32U) == 32U) ? CspProviderFlags.UseMachineKeyStore : CspProviderFlags.NoFlags);
					safeLocalAllocHandle.Dispose();
					return true;
				}
				int lastWin32Error2 = Marshal.GetLastWin32Error();
				if (lastWin32Error2 == -2146885628)
				{
					return false;
				}
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x060029D8 RID: 10712 RVA: 0x000BE4B4 File Offset: 0x000BC6B4
		private void AppendPrivateKeyInfo(StringBuilder sb)
		{
			if (!this.HasPrivateKey)
			{
				return;
			}
			CspKeyContainerInfo cspKeyContainerInfo = null;
			try
			{
				CspParameters cspParameters = new CspParameters();
				if (X509Certificate2.GetPrivateKeyInfo(this.m_safeCertContext, ref cspParameters))
				{
					cspKeyContainerInfo = new CspKeyContainerInfo(cspParameters);
				}
			}
			catch (SecurityException)
			{
			}
			catch (CryptographicException)
			{
			}
			sb.Append(Environment.NewLine + Environment.NewLine + "[Private Key]");
			if (cspKeyContainerInfo == null)
			{
				return;
			}
			sb.Append(Environment.NewLine + "  Key Store: ");
			sb.Append(cspKeyContainerInfo.MachineKeyStore ? "Machine" : "User");
			sb.Append(Environment.NewLine + "  Provider Name: ");
			sb.Append(cspKeyContainerInfo.ProviderName);
			sb.Append(Environment.NewLine + "  Provider type: ");
			sb.Append(cspKeyContainerInfo.ProviderType);
			sb.Append(Environment.NewLine + "  Key Spec: ");
			sb.Append(cspKeyContainerInfo.KeyNumber);
			sb.Append(Environment.NewLine + "  Key Container Name: ");
			sb.Append(cspKeyContainerInfo.KeyContainerName);
			try
			{
				string uniqueKeyContainerName = cspKeyContainerInfo.UniqueKeyContainerName;
				sb.Append(Environment.NewLine + "  Unique Key Container Name: ");
				sb.Append(uniqueKeyContainerName);
			}
			catch (CryptographicException)
			{
			}
			catch (NotSupportedException)
			{
			}
			try
			{
				bool flag = cspKeyContainerInfo.HardwareDevice;
				sb.Append(Environment.NewLine + "  Hardware Device: ");
				sb.Append(flag);
			}
			catch (CryptographicException)
			{
			}
			try
			{
				bool flag = cspKeyContainerInfo.Removable;
				sb.Append(Environment.NewLine + "  Removable: ");
				sb.Append(flag);
			}
			catch (CryptographicException)
			{
			}
			try
			{
				bool flag = cspKeyContainerInfo.Protected;
				sb.Append(Environment.NewLine + "  Protected: ");
				sb.Append(flag);
			}
			catch (CryptographicException)
			{
			}
			catch (NotSupportedException)
			{
			}
		}

		// Token: 0x060029D9 RID: 10713 RVA: 0x000BE6E8 File Offset: 0x000BC8E8
		private unsafe static Oid GetSignatureAlgorithm(SafeCertContextHandle safeCertContextHandle)
		{
			CAPIBase.CERT_CONTEXT cert_CONTEXT = *(CAPIBase.CERT_CONTEXT*)(void*)safeCertContextHandle.DangerousGetHandle();
			CAPIBase.CERT_INFO cert_INFO = (CAPIBase.CERT_INFO)Marshal.PtrToStructure(cert_CONTEXT.pCertInfo, typeof(CAPIBase.CERT_INFO));
			return new Oid(cert_INFO.SignatureAlgorithm.pszObjId, OidGroup.SignatureAlgorithm, false);
		}

		// Token: 0x060029DA RID: 10714 RVA: 0x000BE734 File Offset: 0x000BC934
		private unsafe static uint GetVersion(SafeCertContextHandle safeCertContextHandle)
		{
			CAPIBase.CERT_CONTEXT cert_CONTEXT = *(CAPIBase.CERT_CONTEXT*)(void*)safeCertContextHandle.DangerousGetHandle();
			CAPIBase.CERT_INFO cert_INFO = (CAPIBase.CERT_INFO)Marshal.PtrToStructure(cert_CONTEXT.pCertInfo, typeof(CAPIBase.CERT_INFO));
			return cert_INFO.dwVersion + 1U;
		}

		// Token: 0x060029DB RID: 10715 RVA: 0x000BE778 File Offset: 0x000BC978
		private unsafe static uint QueryCertBlobType(byte[] rawData)
		{
			uint num = 0U;
			if (!CAPI.CryptQueryObject(2U, rawData, 16382U, 14U, 0U, IntPtr.Zero, new IntPtr((void*)(&num)), IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			return num;
		}

		// Token: 0x060029DC RID: 10716 RVA: 0x000BE7C8 File Offset: 0x000BC9C8
		private unsafe static uint QueryCertFileType(string fileName)
		{
			uint num = 0U;
			if (!CAPI.CryptQueryObject(1U, fileName, 16382U, 14U, 0U, IntPtr.Zero, new IntPtr((void*)(&num)), IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			return num;
		}

		// Token: 0x060029DD RID: 10717 RVA: 0x000BE818 File Offset: 0x000BCA18
		private unsafe static void SetFriendlyNameExtendedProperty(SafeCertContextHandle safeCertContextHandle, string name)
		{
			SafeLocalAllocHandle safeLocalAllocHandle = X509Utils.StringToUniPtr(name);
			using (safeLocalAllocHandle)
			{
				CAPIBase.CRYPTOAPI_BLOB cryptoapi_BLOB = default(CAPIBase.CRYPTOAPI_BLOB);
				cryptoapi_BLOB.cbData = (uint)(2 * (name.Length + 1));
				cryptoapi_BLOB.pbData = safeLocalAllocHandle.DangerousGetHandle();
				if (!CAPI.CertSetCertificateContextProperty(safeCertContextHandle, 11U, 0U, new IntPtr((void*)(&cryptoapi_BLOB))))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
			}
		}

		// Token: 0x060029DE RID: 10718 RVA: 0x000BE890 File Offset: 0x000BCA90
		private static void SetPrivateKeyProperty(SafeCertContextHandle safeCertContextHandle, ICspAsymmetricAlgorithm asymmetricAlgorithm)
		{
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			if (asymmetricAlgorithm != null)
			{
				CAPIBase.CRYPT_KEY_PROV_INFO crypt_KEY_PROV_INFO = default(CAPIBase.CRYPT_KEY_PROV_INFO);
				crypt_KEY_PROV_INFO.pwszContainerName = asymmetricAlgorithm.CspKeyContainerInfo.KeyContainerName;
				crypt_KEY_PROV_INFO.pwszProvName = asymmetricAlgorithm.CspKeyContainerInfo.ProviderName;
				crypt_KEY_PROV_INFO.dwProvType = (uint)asymmetricAlgorithm.CspKeyContainerInfo.ProviderType;
				crypt_KEY_PROV_INFO.dwFlags = (asymmetricAlgorithm.CspKeyContainerInfo.MachineKeyStore ? 32U : 0U);
				crypt_KEY_PROV_INFO.cProvParam = 0U;
				crypt_KEY_PROV_INFO.rgProvParam = IntPtr.Zero;
				crypt_KEY_PROV_INFO.dwKeySpec = (uint)asymmetricAlgorithm.CspKeyContainerInfo.KeyNumber;
				safeLocalAllocHandle = CAPI.LocalAlloc(64U, new IntPtr(Marshal.SizeOf(typeof(CAPIBase.CRYPT_KEY_PROV_INFO))));
				Marshal.StructureToPtr(crypt_KEY_PROV_INFO, safeLocalAllocHandle.DangerousGetHandle(), false);
			}
			try
			{
				if (!CAPI.CertSetCertificateContextProperty(safeCertContextHandle, 2U, 0U, safeLocalAllocHandle))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
			}
			finally
			{
				if (!safeLocalAllocHandle.IsInvalid)
				{
					Marshal.DestroyStructure(safeLocalAllocHandle.DangerousGetHandle(), typeof(CAPIBase.CRYPT_KEY_PROV_INFO));
					safeLocalAllocHandle.Dispose();
				}
			}
		}

		// Token: 0x040025A7 RID: 9639
		private int m_version;

		// Token: 0x040025A8 RID: 9640
		private DateTime m_notBefore;

		// Token: 0x040025A9 RID: 9641
		private DateTime m_notAfter;

		// Token: 0x040025AA RID: 9642
		private AsymmetricAlgorithm m_privateKey;

		// Token: 0x040025AB RID: 9643
		private PublicKey m_publicKey;

		// Token: 0x040025AC RID: 9644
		private X509ExtensionCollection m_extensions;

		// Token: 0x040025AD RID: 9645
		private Oid m_signatureAlgorithm;

		// Token: 0x040025AE RID: 9646
		private X500DistinguishedName m_subjectName;

		// Token: 0x040025AF RID: 9647
		private X500DistinguishedName m_issuerName;

		// Token: 0x040025B0 RID: 9648
		private SafeCertContextHandle m_safeCertContext = SafeCertContextHandle.InvalidHandle;

		// Token: 0x040025B1 RID: 9649
		private static int s_publicKeyOffset;

		// Token: 0x040025B2 RID: 9650
		internal const X509KeyStorageFlags KeyStorageFlags47 = X509KeyStorageFlags.UserKeySet | X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable | X509KeyStorageFlags.UserProtected | X509KeyStorageFlags.PersistKeySet;

		// Token: 0x040025B3 RID: 9651
		internal new const X509KeyStorageFlags KeyStorageFlagsAll = X509KeyStorageFlags.UserKeySet | X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable | X509KeyStorageFlags.UserProtected | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.EphemeralKeySet;
	}
}
