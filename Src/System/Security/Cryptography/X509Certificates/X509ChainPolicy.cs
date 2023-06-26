using System;
using System.Globalization;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents the chain policy to be applied when building an X509 certificate chain. This class cannot be inherited.</summary>
	// Token: 0x02000473 RID: 1139
	public sealed class X509ChainPolicy
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainPolicy" /> class.</summary>
		// Token: 0x06002A3C RID: 10812 RVA: 0x000C0D8F File Offset: 0x000BEF8F
		public X509ChainPolicy()
		{
			this.Reset();
		}

		/// <summary>Gets a collection of object identifiers (OIDs) specifying which application policies or enhanced key usages (EKUs) the certificate must support.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.OidCollection" /> object.</returns>
		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06002A3D RID: 10813 RVA: 0x000C0D9D File Offset: 0x000BEF9D
		public OidCollection ApplicationPolicy
		{
			get
			{
				return this.m_applicationPolicy;
			}
		}

		/// <summary>Gets a collection of object identifiers (OIDs) specifying which certificate policies the certificate must support.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.OidCollection" /> object.</returns>
		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x06002A3E RID: 10814 RVA: 0x000C0DA5 File Offset: 0x000BEFA5
		public OidCollection CertificatePolicy
		{
			get
			{
				return this.m_certificatePolicy;
			}
		}

		/// <summary>Gets or sets values for X509 certificate revocation mode.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509RevocationMode" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Security.Cryptography.X509Certificates.X509RevocationMode" /> value supplied is not a valid flag.</exception>
		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x06002A3F RID: 10815 RVA: 0x000C0DAD File Offset: 0x000BEFAD
		// (set) Token: 0x06002A40 RID: 10816 RVA: 0x000C0DB5 File Offset: 0x000BEFB5
		public X509RevocationMode RevocationMode
		{
			get
			{
				return this.m_revocationMode;
			}
			set
			{
				if (value < X509RevocationMode.NoCheck || value > X509RevocationMode.Offline)
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Arg_EnumIllegalVal"), new object[] { "value" }));
				}
				this.m_revocationMode = value;
			}
		}

		/// <summary>Gets or sets values for X509 revocation flags.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509RevocationFlag" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Security.Cryptography.X509Certificates.X509RevocationFlag" /> value supplied is not a valid flag.</exception>
		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x06002A41 RID: 10817 RVA: 0x000C0DEE File Offset: 0x000BEFEE
		// (set) Token: 0x06002A42 RID: 10818 RVA: 0x000C0DF6 File Offset: 0x000BEFF6
		public X509RevocationFlag RevocationFlag
		{
			get
			{
				return this.m_revocationFlag;
			}
			set
			{
				if (value < X509RevocationFlag.EndCertificateOnly || value > X509RevocationFlag.ExcludeRoot)
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Arg_EnumIllegalVal"), new object[] { "value" }));
				}
				this.m_revocationFlag = value;
			}
		}

		/// <summary>Gets verification flags for the certificate.</summary>
		/// <returns>A value from the <see cref="T:System.Security.Cryptography.X509Certificates.X509VerificationFlags" /> enumeration.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Security.Cryptography.X509Certificates.X509VerificationFlags" /> value supplied is not a valid flag. <see cref="F:System.Security.Cryptography.X509Certificates.X509VerificationFlags.NoFlag" /> is the default value.</exception>
		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06002A43 RID: 10819 RVA: 0x000C0E2F File Offset: 0x000BF02F
		// (set) Token: 0x06002A44 RID: 10820 RVA: 0x000C0E37 File Offset: 0x000BF037
		public X509VerificationFlags VerificationFlags
		{
			get
			{
				return this.m_verificationFlags;
			}
			set
			{
				if (value < X509VerificationFlags.NoFlag || value > X509VerificationFlags.AllFlags)
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Arg_EnumIllegalVal"), new object[] { "value" }));
				}
				this.m_verificationFlags = value;
			}
		}

		/// <summary>Gets or sets the time for which the chain is to be validated.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> object.</returns>
		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06002A45 RID: 10821 RVA: 0x000C0E74 File Offset: 0x000BF074
		// (set) Token: 0x06002A46 RID: 10822 RVA: 0x000C0E7C File Offset: 0x000BF07C
		public DateTime VerificationTime
		{
			get
			{
				return this.m_verificationTime;
			}
			set
			{
				this.m_verificationTime = value;
			}
		}

		/// <summary>Gets or sets the maximum amount of time to be spent during online revocation verification or downloading the certificate revocation list (CRL). A value of <see cref="F:System.TimeSpan.Zero" /> means there are no limits.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> object.</returns>
		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x06002A47 RID: 10823 RVA: 0x000C0E85 File Offset: 0x000BF085
		// (set) Token: 0x06002A48 RID: 10824 RVA: 0x000C0E8D File Offset: 0x000BF08D
		public TimeSpan UrlRetrievalTimeout
		{
			get
			{
				return this.m_timeout;
			}
			set
			{
				this.m_timeout = value;
			}
		}

		/// <summary>Gets an object that represents an additional collection of certificates that can be searched by the chaining engine when validating a certificate chain.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</returns>
		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x06002A49 RID: 10825 RVA: 0x000C0E96 File Offset: 0x000BF096
		public X509Certificate2Collection ExtraStore
		{
			get
			{
				return this.m_extraStore;
			}
		}

		/// <summary>Resets the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainPolicy" /> members to their default values.</summary>
		// Token: 0x06002A4A RID: 10826 RVA: 0x000C0EA0 File Offset: 0x000BF0A0
		public void Reset()
		{
			this.m_applicationPolicy = new OidCollection();
			this.m_certificatePolicy = new OidCollection();
			this.m_revocationMode = X509RevocationMode.Online;
			this.m_revocationFlag = X509RevocationFlag.ExcludeRoot;
			this.m_verificationFlags = X509VerificationFlags.NoFlag;
			this.m_verificationTime = DateTime.Now;
			this.m_timeout = new TimeSpan(0, 0, 0);
			this.m_extraStore = new X509Certificate2Collection();
		}

		// Token: 0x0400260A RID: 9738
		private OidCollection m_applicationPolicy;

		// Token: 0x0400260B RID: 9739
		private OidCollection m_certificatePolicy;

		// Token: 0x0400260C RID: 9740
		private X509RevocationMode m_revocationMode;

		// Token: 0x0400260D RID: 9741
		private X509RevocationFlag m_revocationFlag;

		// Token: 0x0400260E RID: 9742
		private DateTime m_verificationTime;

		// Token: 0x0400260F RID: 9743
		private TimeSpan m_timeout;

		// Token: 0x04002610 RID: 9744
		private X509Certificate2Collection m_extraStore;

		// Token: 0x04002611 RID: 9745
		private X509VerificationFlags m_verificationFlags;
	}
}
