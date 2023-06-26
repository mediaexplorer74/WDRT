using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents an X.509 store, which is a physical store where certificates are persisted and managed. This class cannot be inherited.</summary>
	// Token: 0x02000480 RID: 1152
	public sealed class X509Store : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Store" /> class using the personal certificates of the current user store.</summary>
		// Token: 0x06002A8A RID: 10890 RVA: 0x000C1E03 File Offset: 0x000C0003
		public X509Store()
			: this("MY", StoreLocation.CurrentUser)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Store" /> class using the specified store name.</summary>
		/// <param name="storeName">A string value that represents the store name. See <see cref="T:System.Security.Cryptography.X509Certificates.StoreName" /> for more information.</param>
		// Token: 0x06002A8B RID: 10891 RVA: 0x000C1E11 File Offset: 0x000C0011
		public X509Store(string storeName)
			: this(storeName, StoreLocation.CurrentUser)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Store" /> class using the specified <see cref="T:System.Security.Cryptography.X509Certificates.StoreName" /> value.</summary>
		/// <param name="storeName">One of the enumeration values that specifies the name of the X.509 certificate store.</param>
		// Token: 0x06002A8C RID: 10892 RVA: 0x000C1E1B File Offset: 0x000C001B
		public X509Store(StoreName storeName)
			: this(storeName, StoreLocation.CurrentUser)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Store" /> class using the specified <see cref="T:System.Security.Cryptography.X509Certificates.StoreLocation" /> value.</summary>
		/// <param name="storeLocation">One of the enumeration values that specifies the location of the X.509 certificate store.</param>
		// Token: 0x06002A8D RID: 10893 RVA: 0x000C1E25 File Offset: 0x000C0025
		public X509Store(StoreLocation storeLocation)
			: this("MY", storeLocation)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Store" /> class using the specified <see cref="T:System.Security.Cryptography.X509Certificates.StoreName" /> and <see cref="T:System.Security.Cryptography.X509Certificates.StoreLocation" /> values.</summary>
		/// <param name="storeName">One of the enumeration values that specifies the name of the X.509 certificate store.</param>
		/// <param name="storeLocation">One of the enumeration values that specifies the location of the X.509 certificate store.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="storeLocation" /> is not a valid location or <paramref name="storeName" /> is not a valid name.</exception>
		// Token: 0x06002A8E RID: 10894 RVA: 0x000C1E34 File Offset: 0x000C0034
		public X509Store(StoreName storeName, StoreLocation storeLocation)
		{
			this.m_safeCertStoreHandle = SafeCertStoreHandle.InvalidHandle;
			base..ctor();
			if (storeLocation != StoreLocation.CurrentUser && storeLocation != StoreLocation.LocalMachine)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Arg_EnumIllegalVal"), new object[] { "storeLocation" }));
			}
			switch (storeName)
			{
			case StoreName.AddressBook:
				this.m_storeName = "AddressBook";
				break;
			case StoreName.AuthRoot:
				this.m_storeName = "AuthRoot";
				break;
			case StoreName.CertificateAuthority:
				this.m_storeName = "CA";
				break;
			case StoreName.Disallowed:
				this.m_storeName = "Disallowed";
				break;
			case StoreName.My:
				this.m_storeName = "My";
				break;
			case StoreName.Root:
				this.m_storeName = "Root";
				break;
			case StoreName.TrustedPeople:
				this.m_storeName = "TrustedPeople";
				break;
			case StoreName.TrustedPublisher:
				this.m_storeName = "TrustedPublisher";
				break;
			default:
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Arg_EnumIllegalVal"), new object[] { "storeName" }));
			}
			this.m_location = storeLocation;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Store" /> class using a string that represents a value from the <see cref="T:System.Security.Cryptography.X509Certificates.StoreName" /> enumeration and a value from the <see cref="T:System.Security.Cryptography.X509Certificates.StoreLocation" /> enumeration.</summary>
		/// <param name="storeName">A string that represents a value from the <see cref="T:System.Security.Cryptography.X509Certificates.StoreName" /> enumeration.</param>
		/// <param name="storeLocation">One of the enumeration values that specifies the location of the X.509 certificate store.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="storeLocation" /> contains invalid values.</exception>
		// Token: 0x06002A8F RID: 10895 RVA: 0x000C1F48 File Offset: 0x000C0148
		public X509Store(string storeName, StoreLocation storeLocation)
		{
			this.m_safeCertStoreHandle = SafeCertStoreHandle.InvalidHandle;
			base..ctor();
			if (storeLocation != StoreLocation.CurrentUser && storeLocation != StoreLocation.LocalMachine)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Arg_EnumIllegalVal"), new object[] { "storeLocation" }));
			}
			this.m_storeName = storeName;
			this.m_location = storeLocation;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Store" /> class using an Intptr handle to an <see langword="HCERTSTORE" /> store.</summary>
		/// <param name="storeHandle">A handle to an <see langword="HCERTSTORE" /> store.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="storeHandle" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="storeHandle" /> parameter points to an invalid context.</exception>
		// Token: 0x06002A90 RID: 10896 RVA: 0x000C1FA4 File Offset: 0x000C01A4
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public X509Store(IntPtr storeHandle)
		{
			this.m_safeCertStoreHandle = SafeCertStoreHandle.InvalidHandle;
			base..ctor();
			if (storeHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("storeHandle");
			}
			this.m_safeCertStoreHandle = CAPISafe.CertDuplicateStore(storeHandle);
			if (this.m_safeCertStoreHandle == null || this.m_safeCertStoreHandle.IsInvalid)
			{
				throw new CryptographicException(SR.GetString("Cryptography_InvalidStoreHandle"), "storeHandle");
			}
		}

		/// <summary>Gets an <see cref="T:System.IntPtr" /> handle to an <see langword="HCERTSTORE" /> store.</summary>
		/// <returns>A handle to an <see langword="HCERTSTORE" /> store.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The store is not open.</exception>
		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x06002A91 RID: 10897 RVA: 0x000C2010 File Offset: 0x000C0210
		public IntPtr StoreHandle
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				if (this.m_safeCertStoreHandle == null || this.m_safeCertStoreHandle.IsInvalid || this.m_safeCertStoreHandle.IsClosed)
				{
					throw new CryptographicException(SR.GetString("Cryptography_X509_StoreNotOpen"));
				}
				return this.m_safeCertStoreHandle.DangerousGetHandle();
			}
		}

		/// <summary>Gets the location of the X.509 certificate store.</summary>
		/// <returns>The location of the certificate store.</returns>
		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06002A92 RID: 10898 RVA: 0x000C204F File Offset: 0x000C024F
		public StoreLocation Location
		{
			get
			{
				return this.m_location;
			}
		}

		/// <summary>Gets the name of the X.509 certificate store.</summary>
		/// <returns>The name of the certificate store.</returns>
		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06002A93 RID: 10899 RVA: 0x000C2057 File Offset: 0x000C0257
		public string Name
		{
			get
			{
				return this.m_storeName;
			}
		}

		/// <summary>Opens an X.509 certificate store or creates a new store, depending on <see cref="T:System.Security.Cryptography.X509Certificates.OpenFlags" /> flag settings.</summary>
		/// <param name="flags">A bitwise combination of enumeration values that specifies the way to open the X.509 certificate store.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The store is unreadable.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ArgumentException">The store contains invalid values.</exception>
		// Token: 0x06002A94 RID: 10900 RVA: 0x000C2060 File Offset: 0x000C0260
		public void Open(OpenFlags flags)
		{
			if (this.m_location != StoreLocation.CurrentUser && this.m_location != StoreLocation.LocalMachine)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Arg_EnumIllegalVal"), new object[] { "m_location" }));
			}
			uint num = X509Utils.MapX509StoreFlags(this.m_location, flags);
			if (!this.m_safeCertStoreHandle.IsInvalid)
			{
				this.m_safeCertStoreHandle.Dispose();
			}
			this.m_safeCertStoreHandle = CAPI.CertOpenStore(new IntPtr(10L), 65537U, IntPtr.Zero, num, this.m_storeName);
			if (this.m_safeCertStoreHandle == null || this.m_safeCertStoreHandle.IsInvalid)
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			CAPISafe.CertControlStore(this.m_safeCertStoreHandle, 0U, 4U, IntPtr.Zero);
		}

		/// <summary>Releases the resources used by this <see cref="T:System.Security.Cryptography.X509Certificates.X509Store" />.</summary>
		// Token: 0x06002A95 RID: 10901 RVA: 0x000C2123 File Offset: 0x000C0323
		public void Dispose()
		{
			this.Close();
		}

		/// <summary>Closes an X.509 certificate store.</summary>
		// Token: 0x06002A96 RID: 10902 RVA: 0x000C212B File Offset: 0x000C032B
		public void Close()
		{
			if (this.m_safeCertStoreHandle != null && !this.m_safeCertStoreHandle.IsClosed)
			{
				this.m_safeCertStoreHandle.Dispose();
			}
		}

		/// <summary>Adds a certificate to an X.509 certificate store.</summary>
		/// <param name="certificate">The certificate to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificate" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate could not be added to the store.</exception>
		// Token: 0x06002A97 RID: 10903 RVA: 0x000C2150 File Offset: 0x000C0350
		public void Add(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			if (this.m_safeCertStoreHandle == null || this.m_safeCertStoreHandle.IsInvalid || this.m_safeCertStoreHandle.IsClosed)
			{
				throw new CryptographicException(SR.GetString("Cryptography_X509_StoreNotOpen"));
			}
			if (!CAPI.CertAddCertificateContextToStore(this.m_safeCertStoreHandle, certificate.CertContext, 5U, SafeCertContextHandle.InvalidHandle))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
		}

		/// <summary>Adds a collection of certificates to an X.509 certificate store.</summary>
		/// <param name="certificates">The collection of certificates to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificates" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06002A98 RID: 10904 RVA: 0x000C21C4 File Offset: 0x000C03C4
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

		/// <summary>Removes a certificate from an X.509 certificate store.</summary>
		/// <param name="certificate">The certificate to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificate" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06002A99 RID: 10905 RVA: 0x000C2234 File Offset: 0x000C0434
		public void Remove(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			X509Store.RemoveCertificateFromStore(this.m_safeCertStoreHandle, certificate.CertContext);
		}

		/// <summary>Removes a range of certificates from an X.509 certificate store.</summary>
		/// <param name="certificates">A range of certificates to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificates" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06002A9A RID: 10906 RVA: 0x000C2258 File Offset: 0x000C0458
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

		/// <summary>Returns a collection of certificates located in an X.509 certificate store.</summary>
		/// <returns>A collection of certificates.</returns>
		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06002A9B RID: 10907 RVA: 0x000C22C8 File Offset: 0x000C04C8
		public X509Certificate2Collection Certificates
		{
			get
			{
				if (this.m_safeCertStoreHandle.IsInvalid || this.m_safeCertStoreHandle.IsClosed)
				{
					return new X509Certificate2Collection();
				}
				return X509Utils.GetCertificates(this.m_safeCertStoreHandle);
			}
		}

		// Token: 0x06002A9C RID: 10908 RVA: 0x000C22F8 File Offset: 0x000C04F8
		private static void RemoveCertificateFromStore(SafeCertStoreHandle safeCertStoreHandle, SafeCertContextHandle safeCertContext)
		{
			if (safeCertContext == null || safeCertContext.IsInvalid)
			{
				return;
			}
			if (safeCertStoreHandle == null || safeCertStoreHandle.IsInvalid || safeCertStoreHandle.IsClosed)
			{
				throw new CryptographicException(SR.GetString("Cryptography_X509_StoreNotOpen"));
			}
			SafeCertContextHandle safeCertContextHandle = CAPI.CertFindCertificateInStore(safeCertStoreHandle, 65537U, 0U, 851968U, safeCertContext.DangerousGetHandle(), SafeCertContextHandle.InvalidHandle);
			if (safeCertContextHandle == null || safeCertContextHandle.IsInvalid)
			{
				return;
			}
			GC.SuppressFinalize(safeCertContextHandle);
			if (!CAPI.CertDeleteCertificateFromStore(safeCertContextHandle))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x04002641 RID: 9793
		private string m_storeName;

		// Token: 0x04002642 RID: 9794
		private StoreLocation m_location;

		// Token: 0x04002643 RID: 9795
		private SafeCertStoreHandle m_safeCertStoreHandle;
	}
}
