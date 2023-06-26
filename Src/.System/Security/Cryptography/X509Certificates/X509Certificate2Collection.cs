using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Permissions;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents a collection of <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> objects. This class cannot be inherited.</summary>
	// Token: 0x02000468 RID: 1128
	public class X509Certificate2Collection : X509CertificateCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> class without any <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> information.</summary>
		// Token: 0x060029DF RID: 10719 RVA: 0x000BE9A4 File Offset: 0x000BCBA4
		public X509Certificate2Collection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> class using an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object.</summary>
		/// <param name="certificate">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object to start the collection from.</param>
		// Token: 0x060029E0 RID: 10720 RVA: 0x000BE9AC File Offset: 0x000BCBAC
		public X509Certificate2Collection(X509Certificate2 certificate)
		{
			this.Add(certificate);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> class using the specified certificate collection.</summary>
		/// <param name="certificates">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</param>
		// Token: 0x060029E1 RID: 10721 RVA: 0x000BE9BC File Offset: 0x000BCBBC
		public X509Certificate2Collection(X509Certificate2Collection certificates)
		{
			this.AddRange(certificates);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> class using an array of <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> objects.</summary>
		/// <param name="certificates">An array of <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> objects.</param>
		// Token: 0x060029E2 RID: 10722 RVA: 0x000BE9CB File Offset: 0x000BCBCB
		public X509Certificate2Collection(X509Certificate2[] certificates)
		{
			this.AddRange(certificates);
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <returns>The element at the specified index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than the <see cref="P:System.Collections.CollectionBase.Count" /> property.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="index" /> is <see langword="null" />.</exception>
		// Token: 0x17000A2C RID: 2604
		public X509Certificate2 this[int index]
		{
			get
			{
				return (X509Certificate2)base.List[index];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.List[index] = value;
			}
		}

		/// <summary>Adds an object to the end of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" />.</summary>
		/// <param name="certificate">An X.509 certificate represented as an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object.</param>
		/// <returns>The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> index at which the <paramref name="certificate" /> has been added.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificate" /> is <see langword="null" />.</exception>
		// Token: 0x060029E5 RID: 10725 RVA: 0x000BEA0A File Offset: 0x000BCC0A
		public int Add(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			return base.List.Add(certificate);
		}

		/// <summary>Adds multiple <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> objects in an array to the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <param name="certificates">An array of <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> objects.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificates" /> is <see langword="null" />.</exception>
		// Token: 0x060029E6 RID: 10726 RVA: 0x000BEA28 File Offset: 0x000BCC28
		public void AddRange(X509Certificate2[] certificates)
		{
			if (certificates == null)
			{
				throw new ArgumentNullException("certificates");
			}
			int i = 0;
			try
			{
				while (i < certificates.Length)
				{
					this.Add(certificates[i]);
					i++;
				}
			}
			catch
			{
				for (int j = 0; j < i; j++)
				{
					this.Remove(certificates[j]);
				}
				throw;
			}
		}

		/// <summary>Adds multiple <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> objects in an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object to another <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <param name="certificates">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificates" /> is <see langword="null" />.</exception>
		// Token: 0x060029E7 RID: 10727 RVA: 0x000BEA88 File Offset: 0x000BCC88
		public void AddRange(X509Certificate2Collection certificates)
		{
			if (certificates == null)
			{
				throw new ArgumentNullException("certificates");
			}
			int num = 0;
			try
			{
				foreach (X509Certificate2 x509Certificate in certificates)
				{
					this.Add(x509Certificate);
					num++;
				}
			}
			catch
			{
				for (int i = 0; i < num; i++)
				{
					this.Remove(certificates[i]);
				}
				throw;
			}
		}

		/// <summary>Determines whether the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object contains a specific certificate.</summary>
		/// <param name="certificate">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object to locate in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> contains the specified <paramref name="certificate" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificate" /> is <see langword="null" />.</exception>
		// Token: 0x060029E8 RID: 10728 RVA: 0x000BEAF8 File Offset: 0x000BCCF8
		public bool Contains(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			return base.List.Contains(certificate);
		}

		/// <summary>Inserts an object into the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object at the specified index.</summary>
		/// <param name="index">The zero-based index at which to insert <paramref name="certificate" />.</param>
		/// <param name="certificate">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object to insert.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is greater than the <see cref="P:System.Collections.CollectionBase.Count" /> property.</exception>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.  
		///  -or-  
		///  The collection has a fixed size.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificate" /> is <see langword="null" />.</exception>
		// Token: 0x060029E9 RID: 10729 RVA: 0x000BEB14 File Offset: 0x000BCD14
		public void Insert(int index, X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			base.List.Insert(index, certificate);
		}

		/// <summary>Returns an enumerator that can iterate through a <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Enumerator" /> object that can iterate through the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</returns>
		// Token: 0x060029EA RID: 10730 RVA: 0x000BEB31 File Offset: 0x000BCD31
		public new X509Certificate2Enumerator GetEnumerator()
		{
			return new X509Certificate2Enumerator(this);
		}

		/// <summary>Removes the first occurrence of a certificate from the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <param name="certificate">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object to be removed from the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificate" /> is <see langword="null" />.</exception>
		// Token: 0x060029EB RID: 10731 RVA: 0x000BEB39 File Offset: 0x000BCD39
		public void Remove(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			base.List.Remove(certificate);
		}

		/// <summary>Removes multiple <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> objects in an array from an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <param name="certificates">An array of <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> objects.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificates" /> is <see langword="null" />.</exception>
		// Token: 0x060029EC RID: 10732 RVA: 0x000BEB58 File Offset: 0x000BCD58
		public void RemoveRange(X509Certificate2[] certificates)
		{
			if (certificates == null)
			{
				throw new ArgumentNullException("certificates");
			}
			int i = 0;
			try
			{
				while (i < certificates.Length)
				{
					this.Remove(certificates[i]);
					i++;
				}
			}
			catch
			{
				for (int j = 0; j < i; j++)
				{
					this.Add(certificates[j]);
				}
				throw;
			}
		}

		/// <summary>Removes multiple <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> objects in an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object from another <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <param name="certificates">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificates" /> is <see langword="null" />.</exception>
		// Token: 0x060029ED RID: 10733 RVA: 0x000BEBB8 File Offset: 0x000BCDB8
		public void RemoveRange(X509Certificate2Collection certificates)
		{
			if (certificates == null)
			{
				throw new ArgumentNullException("certificates");
			}
			int num = 0;
			try
			{
				foreach (X509Certificate2 x509Certificate in certificates)
				{
					this.Remove(x509Certificate);
					num++;
				}
			}
			catch
			{
				for (int i = 0; i < num; i++)
				{
					this.Add(certificates[i]);
				}
				throw;
			}
		}

		/// <summary>Searches an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object using the search criteria specified by the <see cref="T:System.Security.Cryptography.X509Certificates.X509FindType" /> enumeration and the <paramref name="findValue" /> object.</summary>
		/// <param name="findType">One of the <see cref="T:System.Security.Cryptography.X509Certificates.X509FindType" /> values.</param>
		/// <param name="findValue">The search criteria as an object.</param>
		/// <param name="validOnly">
		///   <see langword="true" /> to allow only valid certificates to be returned from the search; otherwise, <see langword="false" />.</param>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///   <paramref name="findType" /> is invalid.</exception>
		// Token: 0x060029EE RID: 10734 RVA: 0x000BEC28 File Offset: 0x000BCE28
		public X509Certificate2Collection Find(X509FindType findType, object findValue, bool validOnly)
		{
			StorePermission storePermission = new StorePermission(StorePermissionFlags.AllFlags);
			storePermission.Assert();
			SafeCertStoreHandle safeCertStoreHandle = X509Utils.ExportToMemoryStore(this);
			SafeCertStoreHandle safeCertStoreHandle2 = X509Certificate2Collection.FindCertInStore(safeCertStoreHandle, findType, findValue, validOnly);
			X509Certificate2Collection certificates = X509Utils.GetCertificates(safeCertStoreHandle2);
			safeCertStoreHandle2.Dispose();
			safeCertStoreHandle.Dispose();
			return certificates;
		}

		/// <summary>Imports a certificate in the form of a byte array into a <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <param name="rawData">A byte array containing data from an X.509 certificate.</param>
		// Token: 0x060029EF RID: 10735 RVA: 0x000BEC6B File Offset: 0x000BCE6B
		public void Import(byte[] rawData)
		{
			this.ImportFromBlob(rawData, null, X509KeyStorageFlags.DefaultKeySet, false);
		}

		/// <summary>Imports a certificate, in the form of a byte array that requires a password to access the certificate, into a <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <param name="rawData">A byte array containing data from an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object.</param>
		/// <param name="password">The password required to access the certificate information.</param>
		/// <param name="keyStorageFlags">A bitwise combination of the enumeration values that control how and where the certificate is imported.</param>
		// Token: 0x060029F0 RID: 10736 RVA: 0x000BEC77 File Offset: 0x000BCE77
		public void Import(byte[] rawData, string password, X509KeyStorageFlags keyStorageFlags)
		{
			this.ImportFromBlob(rawData, password, keyStorageFlags, true);
		}

		// Token: 0x060029F1 RID: 10737 RVA: 0x000BEC84 File Offset: 0x000BCE84
		private void ImportFromBlob(byte[] rawData, string password, X509KeyStorageFlags keyStorageFlags, bool passwordProvided)
		{
			uint num = X509Utils.MapKeyStorageFlags(keyStorageFlags);
			SafeCertStoreHandle safeCertStoreHandle = SafeCertStoreHandle.InvalidHandle;
			StorePermission storePermission = new StorePermission(StorePermissionFlags.AllFlags);
			storePermission.Assert();
			safeCertStoreHandle = X509Certificate2Collection.LoadStoreFromBlob(rawData, password, num, (keyStorageFlags & X509KeyStorageFlags.PersistKeySet) > X509KeyStorageFlags.DefaultKeySet, passwordProvided);
			X509Certificate2Collection certificates = X509Utils.GetCertificates(safeCertStoreHandle);
			safeCertStoreHandle.Dispose();
			X509Certificate2[] array = new X509Certificate2[certificates.Count];
			X509CertificateCollection x509CertificateCollection = certificates;
			X509Certificate[] array2 = array;
			x509CertificateCollection.CopyTo(array2, 0);
			this.AddRange(array);
		}

		/// <summary>Imports a certificate file into a <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <param name="fileName">The name of the file containing the certificate information.</param>
		// Token: 0x060029F2 RID: 10738 RVA: 0x000BECF0 File Offset: 0x000BCEF0
		public void Import(string fileName)
		{
			this.Import(fileName, null, X509KeyStorageFlags.DefaultKeySet);
		}

		/// <summary>Imports a certificate file that requires a password into a <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <param name="fileName">The name of the file containing the certificate information.</param>
		/// <param name="password">The password required to access the certificate information.</param>
		/// <param name="keyStorageFlags">A bitwise combination of the enumeration values that control how and where the certificate is imported.</param>
		// Token: 0x060029F3 RID: 10739 RVA: 0x000BECFC File Offset: 0x000BCEFC
		public void Import(string fileName, string password, X509KeyStorageFlags keyStorageFlags)
		{
			uint num = X509Utils.MapKeyStorageFlags(keyStorageFlags);
			SafeCertStoreHandle safeCertStoreHandle = SafeCertStoreHandle.InvalidHandle;
			StorePermission storePermission = new StorePermission(StorePermissionFlags.AllFlags);
			storePermission.Assert();
			safeCertStoreHandle = X509Certificate2Collection.LoadStoreFromFile(fileName, password, num, (keyStorageFlags & X509KeyStorageFlags.PersistKeySet) > X509KeyStorageFlags.DefaultKeySet);
			X509Certificate2Collection certificates = X509Utils.GetCertificates(safeCertStoreHandle);
			safeCertStoreHandle.Dispose();
			X509Certificate2[] array = new X509Certificate2[certificates.Count];
			X509CertificateCollection x509CertificateCollection = certificates;
			X509Certificate[] array2 = array;
			x509CertificateCollection.CopyTo(array2, 0);
			this.AddRange(array);
		}

		/// <summary>Exports X.509 certificate information into a byte array.</summary>
		/// <param name="contentType">A supported <see cref="T:System.Security.Cryptography.X509Certificates.X509ContentType" /> object.</param>
		/// <returns>X.509 certificate information in a byte array.</returns>
		// Token: 0x060029F4 RID: 10740 RVA: 0x000BED66 File Offset: 0x000BCF66
		public byte[] Export(X509ContentType contentType)
		{
			return this.Export(contentType, null);
		}

		/// <summary>Exports X.509 certificate information into a byte array using a password.</summary>
		/// <param name="contentType">A supported <see cref="T:System.Security.Cryptography.X509Certificates.X509ContentType" /> object.</param>
		/// <param name="password">A string used to protect the byte array.</param>
		/// <returns>X.509 certificate information in a byte array.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate is unreadable, the content is invalid or, in the case of a certificate requiring a password, the private key could not be exported because the password provided was incorrect.</exception>
		// Token: 0x060029F5 RID: 10741 RVA: 0x000BED70 File Offset: 0x000BCF70
		public byte[] Export(X509ContentType contentType, string password)
		{
			StorePermission storePermission = new StorePermission(StorePermissionFlags.AllFlags);
			storePermission.Assert();
			SafeCertStoreHandle safeCertStoreHandle = X509Utils.ExportToMemoryStore(this);
			byte[] array = X509Certificate2Collection.ExportCertificatesToBlob(safeCertStoreHandle, contentType, password);
			safeCertStoreHandle.Dispose();
			return array;
		}

		// Token: 0x060029F6 RID: 10742 RVA: 0x000BEDA8 File Offset: 0x000BCFA8
		private unsafe static byte[] ExportCertificatesToBlob(SafeCertStoreHandle safeCertStoreHandle, X509ContentType contentType, string password)
		{
			SafeCertContextHandle safeCertContextHandle = SafeCertContextHandle.InvalidHandle;
			uint num = 2U;
			byte[] array = null;
			CAPIBase.CRYPTOAPI_BLOB cryptoapi_BLOB = default(CAPIBase.CRYPTOAPI_BLOB);
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			switch (contentType)
			{
			case X509ContentType.Cert:
				safeCertContextHandle = CAPI.CertEnumCertificatesInStore(safeCertStoreHandle, safeCertContextHandle);
				if (safeCertContextHandle != null && !safeCertContextHandle.IsInvalid)
				{
					CAPIBase.CERT_CONTEXT cert_CONTEXT = *(CAPIBase.CERT_CONTEXT*)(void*)safeCertContextHandle.DangerousGetHandle();
					array = new byte[cert_CONTEXT.cbCertEncoded];
					Marshal.Copy(cert_CONTEXT.pbCertEncoded, array, 0, array.Length);
				}
				break;
			case X509ContentType.SerializedCert:
			{
				safeCertContextHandle = CAPI.CertEnumCertificatesInStore(safeCertStoreHandle, safeCertContextHandle);
				uint num2 = 0U;
				if (safeCertContextHandle != null && !safeCertContextHandle.IsInvalid)
				{
					if (!CAPISafe.CertSerializeCertificateStoreElement(safeCertContextHandle, 0U, safeLocalAllocHandle, new IntPtr((void*)(&num2))))
					{
						throw new CryptographicException(Marshal.GetLastWin32Error());
					}
					safeLocalAllocHandle = CAPI.LocalAlloc(0U, new IntPtr((long)((ulong)num2)));
					if (!CAPISafe.CertSerializeCertificateStoreElement(safeCertContextHandle, 0U, safeLocalAllocHandle, new IntPtr((void*)(&num2))))
					{
						throw new CryptographicException(Marshal.GetLastWin32Error());
					}
					array = new byte[num2];
					Marshal.Copy(safeLocalAllocHandle.DangerousGetHandle(), array, 0, array.Length);
				}
				break;
			}
			case X509ContentType.Pfx:
				if (!CAPI.PFXExportCertStore(safeCertStoreHandle, new IntPtr((void*)(&cryptoapi_BLOB)), password, 6U))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
				safeLocalAllocHandle = CAPI.LocalAlloc(0U, new IntPtr((long)((ulong)cryptoapi_BLOB.cbData)));
				cryptoapi_BLOB.pbData = safeLocalAllocHandle.DangerousGetHandle();
				if (!CAPI.PFXExportCertStore(safeCertStoreHandle, new IntPtr((void*)(&cryptoapi_BLOB)), password, 6U))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
				array = new byte[cryptoapi_BLOB.cbData];
				Marshal.Copy(cryptoapi_BLOB.pbData, array, 0, array.Length);
				break;
			case X509ContentType.SerializedStore:
			case X509ContentType.Pkcs7:
				if (contentType == X509ContentType.SerializedStore)
				{
					num = 1U;
				}
				if (!CAPI.CertSaveStore(safeCertStoreHandle, 65537U, num, 2U, new IntPtr((void*)(&cryptoapi_BLOB)), 0U))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
				safeLocalAllocHandle = CAPI.LocalAlloc(0U, new IntPtr((long)((ulong)cryptoapi_BLOB.cbData)));
				cryptoapi_BLOB.pbData = safeLocalAllocHandle.DangerousGetHandle();
				if (!CAPI.CertSaveStore(safeCertStoreHandle, 65537U, num, 2U, new IntPtr((void*)(&cryptoapi_BLOB)), 0U))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
				array = new byte[cryptoapi_BLOB.cbData];
				Marshal.Copy(cryptoapi_BLOB.pbData, array, 0, array.Length);
				break;
			default:
				throw new CryptographicException(SR.GetString("Cryptography_X509_InvalidContentType"));
			}
			safeLocalAllocHandle.Dispose();
			safeCertContextHandle.Dispose();
			return array;
		}

		// Token: 0x060029F7 RID: 10743 RVA: 0x000BEFEC File Offset: 0x000BD1EC
		private unsafe static SafeCertStoreHandle FindCertInStore(SafeCertStoreHandle safeSourceStoreHandle, X509FindType findType, object findValue, bool validOnly)
		{
			if (findValue == null)
			{
				throw new ArgumentNullException("findValue");
			}
			IntPtr intPtr = IntPtr.Zero;
			object obj = null;
			object obj2 = null;
			X509Certificate2Collection.FindProcDelegate findProcDelegate = null;
			X509Certificate2Collection.FindProcDelegate findProcDelegate2 = null;
			uint num = 0U;
			CAPIBase.CRYPTOAPI_BLOB cryptoapi_BLOB = default(CAPIBase.CRYPTOAPI_BLOB);
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			System.Runtime.InteropServices.ComTypes.FILETIME filetime = default(System.Runtime.InteropServices.ComTypes.FILETIME);
			switch (findType)
			{
			case X509FindType.FindByThumbprint:
			{
				if (findValue.GetType() != typeof(string))
				{
					throw new CryptographicException(SR.GetString("Cryptography_X509_InvalidFindValue"));
				}
				byte[] array = X509Utils.DecodeHexString((string)findValue);
				safeLocalAllocHandle = X509Utils.ByteToPtr(array);
				cryptoapi_BLOB.pbData = safeLocalAllocHandle.DangerousGetHandle();
				cryptoapi_BLOB.cbData = (uint)array.Length;
				num = 65536U;
				intPtr = new IntPtr((void*)(&cryptoapi_BLOB));
				break;
			}
			case X509FindType.FindBySubjectName:
			{
				if (findValue.GetType() != typeof(string))
				{
					throw new CryptographicException(SR.GetString("Cryptography_X509_InvalidFindValue"));
				}
				string text = (string)findValue;
				num = 524295U;
				safeLocalAllocHandle = X509Utils.StringToUniPtr(text);
				intPtr = safeLocalAllocHandle.DangerousGetHandle();
				break;
			}
			case X509FindType.FindBySubjectDistinguishedName:
			{
				if (findValue.GetType() != typeof(string))
				{
					throw new CryptographicException(SR.GetString("Cryptography_X509_InvalidFindValue"));
				}
				string text = (string)findValue;
				findProcDelegate = new X509Certificate2Collection.FindProcDelegate(X509Certificate2Collection.FindSubjectDistinguishedNameCallback);
				obj = text;
				break;
			}
			case X509FindType.FindByIssuerName:
			{
				if (findValue.GetType() != typeof(string))
				{
					throw new CryptographicException(SR.GetString("Cryptography_X509_InvalidFindValue"));
				}
				string text2 = (string)findValue;
				num = 524292U;
				safeLocalAllocHandle = X509Utils.StringToUniPtr(text2);
				intPtr = safeLocalAllocHandle.DangerousGetHandle();
				break;
			}
			case X509FindType.FindByIssuerDistinguishedName:
			{
				if (findValue.GetType() != typeof(string))
				{
					throw new CryptographicException(SR.GetString("Cryptography_X509_InvalidFindValue"));
				}
				string text2 = (string)findValue;
				findProcDelegate = new X509Certificate2Collection.FindProcDelegate(X509Certificate2Collection.FindIssuerDistinguishedNameCallback);
				obj = text2;
				break;
			}
			case X509FindType.FindBySerialNumber:
			{
				if (findValue.GetType() != typeof(string))
				{
					throw new CryptographicException(SR.GetString("Cryptography_X509_InvalidFindValue"));
				}
				findProcDelegate = new X509Certificate2Collection.FindProcDelegate(X509Certificate2Collection.FindSerialNumberCallback);
				findProcDelegate2 = new X509Certificate2Collection.FindProcDelegate(X509Certificate2Collection.FindSerialNumberCallback);
				BigInt bigInt = new BigInt();
				bigInt.FromHexadecimal((string)findValue);
				obj = bigInt.ToByteArray();
				bigInt.FromDecimal((string)findValue);
				obj2 = bigInt.ToByteArray();
				break;
			}
			case X509FindType.FindByTimeValid:
				if (findValue.GetType() != typeof(DateTime))
				{
					throw new CryptographicException(SR.GetString("Cryptography_X509_InvalidFindValue"));
				}
				*(long*)(&filetime) = ((DateTime)findValue).ToFileTime();
				findProcDelegate = new X509Certificate2Collection.FindProcDelegate(X509Certificate2Collection.FindTimeValidCallback);
				obj = filetime;
				break;
			case X509FindType.FindByTimeNotYetValid:
				if (findValue.GetType() != typeof(DateTime))
				{
					throw new CryptographicException(SR.GetString("Cryptography_X509_InvalidFindValue"));
				}
				*(long*)(&filetime) = ((DateTime)findValue).ToFileTime();
				findProcDelegate = new X509Certificate2Collection.FindProcDelegate(X509Certificate2Collection.FindTimeNotBeforeCallback);
				obj = filetime;
				break;
			case X509FindType.FindByTimeExpired:
				if (findValue.GetType() != typeof(DateTime))
				{
					throw new CryptographicException(SR.GetString("Cryptography_X509_InvalidFindValue"));
				}
				*(long*)(&filetime) = ((DateTime)findValue).ToFileTime();
				findProcDelegate = new X509Certificate2Collection.FindProcDelegate(X509Certificate2Collection.FindTimeNotAfterCallback);
				obj = filetime;
				break;
			case X509FindType.FindByTemplateName:
				if (findValue.GetType() != typeof(string))
				{
					throw new CryptographicException(SR.GetString("Cryptography_X509_InvalidFindValue"));
				}
				obj = (string)findValue;
				findProcDelegate = new X509Certificate2Collection.FindProcDelegate(X509Certificate2Collection.FindTemplateNameCallback);
				break;
			case X509FindType.FindByApplicationPolicy:
			{
				if (findValue.GetType() != typeof(string))
				{
					throw new CryptographicException(SR.GetString("Cryptography_X509_InvalidFindValue"));
				}
				string text3 = X509Utils.FindOidInfoWithFallback(2U, (string)findValue, OidGroup.Policy);
				if (text3 == null)
				{
					text3 = (string)findValue;
					X509Utils.ValidateOidValue(text3);
				}
				obj = text3;
				findProcDelegate = new X509Certificate2Collection.FindProcDelegate(X509Certificate2Collection.FindApplicationPolicyCallback);
				break;
			}
			case X509FindType.FindByCertificatePolicy:
			{
				if (findValue.GetType() != typeof(string))
				{
					throw new CryptographicException(SR.GetString("Cryptography_X509_InvalidFindValue"));
				}
				string text3 = X509Utils.FindOidInfoWithFallback(2U, (string)findValue, OidGroup.Policy);
				if (text3 == null)
				{
					text3 = (string)findValue;
					X509Utils.ValidateOidValue(text3);
				}
				obj = text3;
				findProcDelegate = new X509Certificate2Collection.FindProcDelegate(X509Certificate2Collection.FindCertificatePolicyCallback);
				break;
			}
			case X509FindType.FindByExtension:
			{
				if (findValue.GetType() != typeof(string))
				{
					throw new CryptographicException(SR.GetString("Cryptography_X509_InvalidFindValue"));
				}
				string text3 = X509Utils.FindOidInfoWithFallback(2U, (string)findValue, OidGroup.ExtensionOrAttribute);
				if (text3 == null)
				{
					text3 = (string)findValue;
					X509Utils.ValidateOidValue(text3);
				}
				obj = text3;
				findProcDelegate = new X509Certificate2Collection.FindProcDelegate(X509Certificate2Collection.FindExtensionCallback);
				break;
			}
			case X509FindType.FindByKeyUsage:
				if (findValue.GetType() == typeof(string))
				{
					CAPIBase.KEY_USAGE_STRUCT[] array2 = new CAPIBase.KEY_USAGE_STRUCT[]
					{
						new CAPIBase.KEY_USAGE_STRUCT("DigitalSignature", 128U),
						new CAPIBase.KEY_USAGE_STRUCT("NonRepudiation", 64U),
						new CAPIBase.KEY_USAGE_STRUCT("KeyEncipherment", 32U),
						new CAPIBase.KEY_USAGE_STRUCT("DataEncipherment", 16U),
						new CAPIBase.KEY_USAGE_STRUCT("KeyAgreement", 8U),
						new CAPIBase.KEY_USAGE_STRUCT("KeyCertSign", 4U),
						new CAPIBase.KEY_USAGE_STRUCT("CrlSign", 2U),
						new CAPIBase.KEY_USAGE_STRUCT("EncipherOnly", 1U),
						new CAPIBase.KEY_USAGE_STRUCT("DecipherOnly", 32768U)
					};
					uint num2 = 0U;
					while ((ulong)num2 < (ulong)((long)array2.Length))
					{
						if (string.Compare(array2[(int)num2].pwszKeyUsage, (string)findValue, StringComparison.OrdinalIgnoreCase) == 0)
						{
							obj = array2[(int)num2].dwKeyUsageBit;
							break;
						}
						num2 += 1U;
					}
					if (obj == null)
					{
						throw new CryptographicException(SR.GetString("Cryptography_X509_InvalidFindType"));
					}
				}
				else if (findValue.GetType() == typeof(X509KeyUsageFlags))
				{
					obj = findValue;
				}
				else
				{
					if (!(findValue.GetType() == typeof(uint)) && !(findValue.GetType() == typeof(int)))
					{
						throw new CryptographicException(SR.GetString("Cryptography_X509_InvalidFindType"));
					}
					obj = findValue;
				}
				findProcDelegate = new X509Certificate2Collection.FindProcDelegate(X509Certificate2Collection.FindKeyUsageCallback);
				break;
			case X509FindType.FindBySubjectKeyIdentifier:
				if (findValue.GetType() != typeof(string))
				{
					throw new CryptographicException(SR.GetString("Cryptography_X509_InvalidFindValue"));
				}
				obj = X509Utils.DecodeHexString((string)findValue);
				findProcDelegate = new X509Certificate2Collection.FindProcDelegate(X509Certificate2Collection.FindSubjectKeyIdentifierCallback);
				break;
			default:
				throw new CryptographicException(SR.GetString("Cryptography_X509_InvalidFindType"));
			}
			SafeCertStoreHandle safeCertStoreHandle = CAPI.CertOpenStore(new IntPtr(2L), 65537U, IntPtr.Zero, 8704U, null);
			if (safeCertStoreHandle == null || safeCertStoreHandle.IsInvalid)
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			X509Certificate2Collection.FindByCert(safeSourceStoreHandle, num, intPtr, validOnly, findProcDelegate, findProcDelegate2, obj, obj2, safeCertStoreHandle);
			safeLocalAllocHandle.Dispose();
			return safeCertStoreHandle;
		}

		// Token: 0x060029F8 RID: 10744 RVA: 0x000BF70C File Offset: 0x000BD90C
		private static void FindByCert(SafeCertStoreHandle safeSourceStoreHandle, uint dwFindType, IntPtr pvFindPara, bool validOnly, X509Certificate2Collection.FindProcDelegate pfnCertCallback1, X509Certificate2Collection.FindProcDelegate pfnCertCallback2, object pvCallbackData1, object pvCallbackData2, SafeCertStoreHandle safeTargetStoreHandle)
		{
			int num = 0;
			SafeCertContextHandle safeCertContextHandle = SafeCertContextHandle.InvalidHandle;
			safeCertContextHandle = CAPI.CertFindCertificateInStore(safeSourceStoreHandle, 65537U, 0U, dwFindType, pvFindPara, safeCertContextHandle);
			while (safeCertContextHandle != null && !safeCertContextHandle.IsInvalid)
			{
				if (pfnCertCallback1 == null)
				{
					goto IL_46;
				}
				num = pfnCertCallback1(safeCertContextHandle, pvCallbackData1);
				if (num == 1)
				{
					if (pfnCertCallback2 != null)
					{
						num = pfnCertCallback2(safeCertContextHandle, pvCallbackData2);
					}
					if (num == 1)
					{
						goto IL_8D;
					}
				}
				if (num == 0)
				{
					goto IL_46;
				}
				break;
				IL_8D:
				GC.SuppressFinalize(safeCertContextHandle);
				safeCertContextHandle = CAPI.CertFindCertificateInStore(safeSourceStoreHandle, 65537U, 0U, dwFindType, pvFindPara, safeCertContextHandle);
				continue;
				IL_46:
				if (validOnly)
				{
					num = X509Utils.VerifyCertificate(safeCertContextHandle, null, null, X509RevocationMode.NoCheck, X509RevocationFlag.ExcludeRoot, DateTime.Now, new TimeSpan(0, 0, 0), null, new IntPtr(1L), IntPtr.Zero);
					if (num == 1)
					{
						goto IL_8D;
					}
					if (num != 0)
					{
						break;
					}
				}
				if (!CAPI.CertAddCertificateLinkToStore(safeTargetStoreHandle, safeCertContextHandle, 4U, SafeCertContextHandle.InvalidHandle))
				{
					num = Marshal.GetHRForLastWin32Error();
					break;
				}
				goto IL_8D;
			}
			if (safeCertContextHandle != null && !safeCertContextHandle.IsInvalid)
			{
				safeCertContextHandle.Dispose();
			}
			if (num != 1 && num != 0)
			{
				throw new CryptographicException(num);
			}
		}

		// Token: 0x060029F9 RID: 10745 RVA: 0x000BF7EC File Offset: 0x000BD9EC
		private static int FindSubjectDistinguishedNameCallback(SafeCertContextHandle safeCertContextHandle, object pvCallbackData)
		{
			string certNameInfo = CAPI.GetCertNameInfo(safeCertContextHandle, 0U, 2U);
			if (string.Compare(certNameInfo, (string)pvCallbackData, StringComparison.OrdinalIgnoreCase) != 0)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x060029FA RID: 10746 RVA: 0x000BF814 File Offset: 0x000BDA14
		private static int FindIssuerDistinguishedNameCallback(SafeCertContextHandle safeCertContextHandle, object pvCallbackData)
		{
			string certNameInfo = CAPI.GetCertNameInfo(safeCertContextHandle, 1U, 2U);
			if (string.Compare(certNameInfo, (string)pvCallbackData, StringComparison.OrdinalIgnoreCase) != 0)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x060029FB RID: 10747 RVA: 0x000BF83C File Offset: 0x000BDA3C
		private unsafe static int FindSerialNumberCallback(SafeCertContextHandle safeCertContextHandle, object pvCallbackData)
		{
			CAPIBase.CERT_CONTEXT cert_CONTEXT = *(CAPIBase.CERT_CONTEXT*)(void*)safeCertContextHandle.DangerousGetHandle();
			CAPIBase.CERT_INFO cert_INFO = (CAPIBase.CERT_INFO)Marshal.PtrToStructure(cert_CONTEXT.pCertInfo, typeof(CAPIBase.CERT_INFO));
			byte[] array = new byte[cert_INFO.SerialNumber.cbData];
			Marshal.Copy(cert_INFO.SerialNumber.pbData, array, 0, array.Length);
			int hexArraySize = X509Utils.GetHexArraySize(array);
			byte[] array2 = (byte[])pvCallbackData;
			if (array2.Length != hexArraySize)
			{
				return 1;
			}
			for (int i = 0; i < array2.Length; i++)
			{
				if (array2[i] != array[i])
				{
					return 1;
				}
			}
			return 0;
		}

		// Token: 0x060029FC RID: 10748 RVA: 0x000BF8D4 File Offset: 0x000BDAD4
		private unsafe static int FindTimeValidCallback(SafeCertContextHandle safeCertContextHandle, object pvCallbackData)
		{
			System.Runtime.InteropServices.ComTypes.FILETIME filetime = (System.Runtime.InteropServices.ComTypes.FILETIME)pvCallbackData;
			CAPIBase.CERT_CONTEXT cert_CONTEXT = *(CAPIBase.CERT_CONTEXT*)(void*)safeCertContextHandle.DangerousGetHandle();
			if (CAPISafe.CertVerifyTimeValidity(ref filetime, cert_CONTEXT.pCertInfo) == 0)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x060029FD RID: 10749 RVA: 0x000BF90C File Offset: 0x000BDB0C
		private unsafe static int FindTimeNotAfterCallback(SafeCertContextHandle safeCertContextHandle, object pvCallbackData)
		{
			System.Runtime.InteropServices.ComTypes.FILETIME filetime = (System.Runtime.InteropServices.ComTypes.FILETIME)pvCallbackData;
			CAPIBase.CERT_CONTEXT cert_CONTEXT = *(CAPIBase.CERT_CONTEXT*)(void*)safeCertContextHandle.DangerousGetHandle();
			if (CAPISafe.CertVerifyTimeValidity(ref filetime, cert_CONTEXT.pCertInfo) == 1)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x060029FE RID: 10750 RVA: 0x000BF944 File Offset: 0x000BDB44
		private unsafe static int FindTimeNotBeforeCallback(SafeCertContextHandle safeCertContextHandle, object pvCallbackData)
		{
			System.Runtime.InteropServices.ComTypes.FILETIME filetime = (System.Runtime.InteropServices.ComTypes.FILETIME)pvCallbackData;
			CAPIBase.CERT_CONTEXT cert_CONTEXT = *(CAPIBase.CERT_CONTEXT*)(void*)safeCertContextHandle.DangerousGetHandle();
			if (CAPISafe.CertVerifyTimeValidity(ref filetime, cert_CONTEXT.pCertInfo) == -1)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x060029FF RID: 10751 RVA: 0x000BF97C File Offset: 0x000BDB7C
		private unsafe static int FindTemplateNameCallback(SafeCertContextHandle safeCertContextHandle, object pvCallbackData)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			CAPIBase.CERT_CONTEXT cert_CONTEXT = *(CAPIBase.CERT_CONTEXT*)(void*)safeCertContextHandle.DangerousGetHandle();
			CAPIBase.CERT_INFO cert_INFO = (CAPIBase.CERT_INFO)Marshal.PtrToStructure(cert_CONTEXT.pCertInfo, typeof(CAPIBase.CERT_INFO));
			intPtr = CAPISafe.CertFindExtension("1.3.6.1.4.1.311.20.2", cert_INFO.cExtension, cert_INFO.rgExtension);
			intPtr2 = CAPISafe.CertFindExtension("1.3.6.1.4.1.311.21.7", cert_INFO.cExtension, cert_INFO.rgExtension);
			if (intPtr == IntPtr.Zero && intPtr2 == IntPtr.Zero)
			{
				return 1;
			}
			if (intPtr != IntPtr.Zero)
			{
				CAPIBase.CERT_EXTENSION cert_EXTENSION = (CAPIBase.CERT_EXTENSION)Marshal.PtrToStructure(intPtr, typeof(CAPIBase.CERT_EXTENSION));
				byte[] array = new byte[cert_EXTENSION.Value.cbData];
				Marshal.Copy(cert_EXTENSION.Value.pbData, array, 0, array.Length);
				uint num = 0U;
				SafeLocalAllocHandle safeLocalAllocHandle = null;
				bool flag = CAPI.DecodeObject(new IntPtr(24L), array, out safeLocalAllocHandle, out num);
				if (flag)
				{
					CAPIBase.CERT_NAME_VALUE cert_NAME_VALUE = (CAPIBase.CERT_NAME_VALUE)Marshal.PtrToStructure(safeLocalAllocHandle.DangerousGetHandle(), typeof(CAPIBase.CERT_NAME_VALUE));
					string text = Marshal.PtrToStringUni(cert_NAME_VALUE.Value.pbData);
					if (string.Compare(text, (string)pvCallbackData, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return 0;
					}
				}
			}
			if (intPtr2 != IntPtr.Zero)
			{
				CAPIBase.CERT_EXTENSION cert_EXTENSION2 = (CAPIBase.CERT_EXTENSION)Marshal.PtrToStructure(intPtr2, typeof(CAPIBase.CERT_EXTENSION));
				byte[] array2 = new byte[cert_EXTENSION2.Value.cbData];
				Marshal.Copy(cert_EXTENSION2.Value.pbData, array2, 0, array2.Length);
				uint num2 = 0U;
				SafeLocalAllocHandle safeLocalAllocHandle2 = null;
				bool flag2 = CAPI.DecodeObject(new IntPtr(64L), array2, out safeLocalAllocHandle2, out num2);
				if (flag2)
				{
					CAPIBase.CERT_TEMPLATE_EXT cert_TEMPLATE_EXT = (CAPIBase.CERT_TEMPLATE_EXT)Marshal.PtrToStructure(safeLocalAllocHandle2.DangerousGetHandle(), typeof(CAPIBase.CERT_TEMPLATE_EXT));
					string text2 = X509Utils.FindOidInfoWithFallback(2U, (string)pvCallbackData, OidGroup.Template);
					if (text2 == null)
					{
						text2 = (string)pvCallbackData;
					}
					if (string.Compare(cert_TEMPLATE_EXT.pszObjId, text2, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return 0;
					}
				}
			}
			return 1;
		}

		// Token: 0x06002A00 RID: 10752 RVA: 0x000BFB7C File Offset: 0x000BDD7C
		private unsafe static int FindApplicationPolicyCallback(SafeCertContextHandle safeCertContextHandle, object pvCallbackData)
		{
			string text = (string)pvCallbackData;
			if (text.Length == 0)
			{
				return 1;
			}
			IntPtr intPtr = safeCertContextHandle.DangerousGetHandle();
			int num = 0;
			uint num2 = 0U;
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			if (!CAPISafe.CertGetValidUsages(1U, new IntPtr((void*)(&intPtr)), new IntPtr((void*)(&num)), safeLocalAllocHandle, new IntPtr((void*)(&num2))))
			{
				return 1;
			}
			safeLocalAllocHandle = CAPI.LocalAlloc(0U, new IntPtr((long)((ulong)num2)));
			if (!CAPISafe.CertGetValidUsages(1U, new IntPtr((void*)(&intPtr)), new IntPtr((void*)(&num)), safeLocalAllocHandle, new IntPtr((void*)(&num2))))
			{
				return 1;
			}
			if (num == -1)
			{
				return 0;
			}
			for (int i = 0; i < num; i++)
			{
				IntPtr intPtr2 = Marshal.ReadIntPtr(new IntPtr((long)safeLocalAllocHandle.DangerousGetHandle() + (long)(i * Marshal.SizeOf(typeof(IntPtr)))));
				string text2 = Marshal.PtrToStringAnsi(intPtr2);
				if (string.Compare(text, text2, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return 0;
				}
			}
			return 1;
		}

		// Token: 0x06002A01 RID: 10753 RVA: 0x000BFC5C File Offset: 0x000BDE5C
		private unsafe static int FindCertificatePolicyCallback(SafeCertContextHandle safeCertContextHandle, object pvCallbackData)
		{
			string text = (string)pvCallbackData;
			if (text.Length == 0)
			{
				return 1;
			}
			CAPIBase.CERT_CONTEXT cert_CONTEXT = *(CAPIBase.CERT_CONTEXT*)(void*)safeCertContextHandle.DangerousGetHandle();
			CAPIBase.CERT_INFO cert_INFO = (CAPIBase.CERT_INFO)Marshal.PtrToStructure(cert_CONTEXT.pCertInfo, typeof(CAPIBase.CERT_INFO));
			IntPtr intPtr = CAPISafe.CertFindExtension("2.5.29.32", cert_INFO.cExtension, cert_INFO.rgExtension);
			if (intPtr == IntPtr.Zero)
			{
				return 1;
			}
			CAPIBase.CERT_EXTENSION cert_EXTENSION = (CAPIBase.CERT_EXTENSION)Marshal.PtrToStructure(intPtr, typeof(CAPIBase.CERT_EXTENSION));
			byte[] array = new byte[cert_EXTENSION.Value.cbData];
			Marshal.Copy(cert_EXTENSION.Value.pbData, array, 0, array.Length);
			uint num = 0U;
			SafeLocalAllocHandle safeLocalAllocHandle = null;
			bool flag = CAPI.DecodeObject(new IntPtr(16L), array, out safeLocalAllocHandle, out num);
			if (flag)
			{
				CAPIBase.CERT_POLICIES_INFO cert_POLICIES_INFO = (CAPIBase.CERT_POLICIES_INFO)Marshal.PtrToStructure(safeLocalAllocHandle.DangerousGetHandle(), typeof(CAPIBase.CERT_POLICIES_INFO));
				int num2 = 0;
				while ((long)num2 < (long)((ulong)cert_POLICIES_INFO.cPolicyInfo))
				{
					IntPtr intPtr2 = new IntPtr((long)cert_POLICIES_INFO.rgPolicyInfo + (long)(num2 * Marshal.SizeOf(typeof(CAPIBase.CERT_POLICY_INFO))));
					CAPIBase.CERT_POLICY_INFO cert_POLICY_INFO = (CAPIBase.CERT_POLICY_INFO)Marshal.PtrToStructure(intPtr2, typeof(CAPIBase.CERT_POLICY_INFO));
					if (string.Compare(text, cert_POLICY_INFO.pszPolicyIdentifier, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return 0;
					}
					num2++;
				}
			}
			return 1;
		}

		// Token: 0x06002A02 RID: 10754 RVA: 0x000BFDB8 File Offset: 0x000BDFB8
		private unsafe static int FindExtensionCallback(SafeCertContextHandle safeCertContextHandle, object pvCallbackData)
		{
			CAPIBase.CERT_CONTEXT cert_CONTEXT = *(CAPIBase.CERT_CONTEXT*)(void*)safeCertContextHandle.DangerousGetHandle();
			CAPIBase.CERT_INFO cert_INFO = (CAPIBase.CERT_INFO)Marshal.PtrToStructure(cert_CONTEXT.pCertInfo, typeof(CAPIBase.CERT_INFO));
			IntPtr intPtr = CAPISafe.CertFindExtension((string)pvCallbackData, cert_INFO.cExtension, cert_INFO.rgExtension);
			if (intPtr == IntPtr.Zero)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06002A03 RID: 10755 RVA: 0x000BFE1C File Offset: 0x000BE01C
		private unsafe static int FindKeyUsageCallback(SafeCertContextHandle safeCertContextHandle, object pvCallbackData)
		{
			CAPIBase.CERT_CONTEXT cert_CONTEXT = *(CAPIBase.CERT_CONTEXT*)(void*)safeCertContextHandle.DangerousGetHandle();
			uint num = 0U;
			if (!CAPISafe.CertGetIntendedKeyUsage(65537U, cert_CONTEXT.pCertInfo, new IntPtr((void*)(&num)), 4U))
			{
				return 0;
			}
			uint num2 = Convert.ToUInt32(pvCallbackData, null);
			if ((num & num2) == num2)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06002A04 RID: 10756 RVA: 0x000BFE6C File Offset: 0x000BE06C
		private static int FindSubjectKeyIdentifierCallback(SafeCertContextHandle safeCertContextHandle, object pvCallbackData)
		{
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			uint num = 0U;
			if (!CAPISafe.CertGetCertificateContextProperty(safeCertContextHandle, 20U, safeLocalAllocHandle, ref num))
			{
				return 1;
			}
			safeLocalAllocHandle = CAPI.LocalAlloc(0U, new IntPtr((long)((ulong)num)));
			if (!CAPISafe.CertGetCertificateContextProperty(safeCertContextHandle, 20U, safeLocalAllocHandle, ref num))
			{
				return 1;
			}
			byte[] array = (byte[])pvCallbackData;
			if ((long)array.Length != (long)((ulong)num))
			{
				return 1;
			}
			byte[] array2 = new byte[num];
			Marshal.Copy(safeLocalAllocHandle.DangerousGetHandle(), array2, 0, array2.Length);
			safeLocalAllocHandle.Dispose();
			for (uint num2 = 0U; num2 < num; num2 += 1U)
			{
				if (array[(int)num2] != array2[(int)num2])
				{
					return 1;
				}
			}
			return 0;
		}

		// Token: 0x06002A05 RID: 10757 RVA: 0x000BFEF8 File Offset: 0x000BE0F8
		private unsafe static SafeCertStoreHandle LoadStoreFromBlob(byte[] rawData, string password, uint dwFlags, bool persistKeyContainers, bool passwordProvided)
		{
			uint num = 0U;
			SafeCertStoreHandle safeCertStoreHandle = SafeCertStoreHandle.InvalidHandle;
			if (!CAPI.CryptQueryObject(2U, rawData, 5938U, 14U, 0U, IntPtr.Zero, new IntPtr((void*)(&num)), IntPtr.Zero, ref safeCertStoreHandle, IntPtr.Zero, IntPtr.Zero))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			if (num == 12U)
			{
				X509Certificate2Collection.EnforceIterationCountLimit(rawData, false, passwordProvided);
				safeCertStoreHandle.Dispose();
				safeCertStoreHandle = CAPI.PFXImportCertStore(2U, rawData, password, dwFlags, persistKeyContainers);
			}
			if (safeCertStoreHandle == null || safeCertStoreHandle.IsInvalid)
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			return safeCertStoreHandle;
		}

		// Token: 0x06002A06 RID: 10758 RVA: 0x000BFF80 File Offset: 0x000BE180
		private unsafe static SafeCertStoreHandle LoadStoreFromFile(string fileName, string password, uint dwFlags, bool persistKeyContainers)
		{
			uint num = 0U;
			SafeCertStoreHandle safeCertStoreHandle = SafeCertStoreHandle.InvalidHandle;
			if (!CAPI.CryptQueryObject(1U, fileName, 5938U, 14U, 0U, IntPtr.Zero, new IntPtr((void*)(&num)), IntPtr.Zero, ref safeCertStoreHandle, IntPtr.Zero, IntPtr.Zero))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			if (num == 12U)
			{
				safeCertStoreHandle.Dispose();
				safeCertStoreHandle = CAPI.PFXImportCertStore(1U, fileName, password, dwFlags, persistKeyContainers);
			}
			if (safeCertStoreHandle == null || safeCertStoreHandle.IsInvalid)
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			return safeCertStoreHandle;
		}

		// Token: 0x06002A07 RID: 10759 RVA: 0x000BFFFC File Offset: 0x000BE1FC
		private static void EnforceIterationCountLimit(byte[] pkcs12, bool readingFromFile, bool passwordProvided)
		{
			if (!X509Certificate2Collection.s_enforceIterationCountLimitMethodInitialized)
			{
				X509Certificate2Collection.InitializeEnforceIterationCountLimitMethod();
			}
			if (X509Certificate2Collection.s_enforceIterationCountLimitDelegate != null)
			{
				X509Certificate2Collection.s_enforceIterationCountLimitDelegate(pkcs12, readingFromFile, passwordProvided);
			}
		}

		// Token: 0x06002A08 RID: 10760 RVA: 0x000C0020 File Offset: 0x000BE220
		[ReflectionPermission(SecurityAction.Assert, Unrestricted = true)]
		private static void InitializeEnforceIterationCountLimitMethod()
		{
			Assembly assembly = typeof(X509Certificate).Assembly;
			if (assembly != null)
			{
				Type type = assembly.GetType("System.Security.Cryptography.X509Certificates.IterationCountLimitEnforcer");
				if (type != null)
				{
					BindingFlags bindingFlags = BindingFlags.Static | BindingFlags.NonPublic;
					Type[] array = new Type[]
					{
						typeof(byte[]),
						typeof(bool),
						typeof(bool)
					};
					MethodInfo method = type.GetMethod("EnforceIterationCountLimit", bindingFlags, null, array, null);
					if (method != null)
					{
						X509Certificate2Collection.s_enforceIterationCountLimitDelegate = (X509Certificate2Collection.EnforceIterationCountLimitDelegate)Delegate.CreateDelegate(typeof(X509Certificate2Collection.EnforceIterationCountLimitDelegate), method);
					}
				}
			}
			X509Certificate2Collection.s_enforceIterationCountLimitMethodInitialized = true;
		}

		// Token: 0x040025C4 RID: 9668
		private const uint X509_STORE_CONTENT_FLAGS = 5938U;

		// Token: 0x040025C5 RID: 9669
		private static X509Certificate2Collection.EnforceIterationCountLimitDelegate s_enforceIterationCountLimitDelegate;

		// Token: 0x040025C6 RID: 9670
		private static volatile bool s_enforceIterationCountLimitMethodInitialized;

		// Token: 0x02000875 RID: 2165
		// (Invoke) Token: 0x06004540 RID: 17728
		internal delegate int FindProcDelegate(SafeCertContextHandle safeCertContextHandle, object pvCallbackData);

		// Token: 0x02000876 RID: 2166
		// (Invoke) Token: 0x06004544 RID: 17732
		private delegate void EnforceIterationCountLimitDelegate(byte[] pkcs12, bool readingFromFile, bool passwordProvided);
	}
}
