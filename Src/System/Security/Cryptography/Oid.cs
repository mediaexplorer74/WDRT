using System;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Cryptography
{
	/// <summary>Represents a cryptographic object identifier. This class cannot be inherited.</summary>
	// Token: 0x0200045E RID: 1118
	public sealed class Oid
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Oid" /> class.</summary>
		// Token: 0x06002976 RID: 10614 RVA: 0x000BC490 File Offset: 0x000BA690
		public Oid()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Oid" /> class using a string value of an <see cref="T:System.Security.Cryptography.Oid" /> object.</summary>
		/// <param name="oid">An object identifier.</param>
		// Token: 0x06002977 RID: 10615 RVA: 0x000BC498 File Offset: 0x000BA698
		public Oid(string oid)
			: this(oid, OidGroup.All, true)
		{
		}

		// Token: 0x06002978 RID: 10616 RVA: 0x000BC4A4 File Offset: 0x000BA6A4
		internal Oid(string oid, OidGroup group, bool lookupFriendlyName)
		{
			if (lookupFriendlyName)
			{
				string text = System.Security.Cryptography.X509Certificates.X509Utils.FindOidInfoWithFallback(2U, oid, group);
				if (text == null)
				{
					text = oid;
				}
				this.Value = text;
			}
			else
			{
				this.Value = oid;
			}
			this.m_group = group;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Oid" /> class using the specified value and friendly name.</summary>
		/// <param name="value">The dotted number of the identifier.</param>
		/// <param name="friendlyName">The friendly name of the identifier.</param>
		// Token: 0x06002979 RID: 10617 RVA: 0x000BC4DF File Offset: 0x000BA6DF
		public Oid(string value, string friendlyName)
		{
			this.m_value = value;
			this.m_friendlyName = friendlyName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Oid" /> class using the specified <see cref="T:System.Security.Cryptography.Oid" /> object.</summary>
		/// <param name="oid">The object identifier information to use to create the new object identifier.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="oid" /> is <see langword="null" />.</exception>
		// Token: 0x0600297A RID: 10618 RVA: 0x000BC4F5 File Offset: 0x000BA6F5
		public Oid(Oid oid)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			this.m_value = oid.m_value;
			this.m_friendlyName = oid.m_friendlyName;
			this.m_group = oid.m_group;
		}

		// Token: 0x0600297B RID: 10619 RVA: 0x000BC52F File Offset: 0x000BA72F
		private Oid(string value, string friendlyName, OidGroup group)
		{
			this.m_value = value;
			this.m_friendlyName = friendlyName;
			this.m_group = group;
		}

		/// <summary>Creates an <see cref="T:System.Security.Cryptography.Oid" /> object from an OID friendly name by searching the specified group.</summary>
		/// <param name="friendlyName">The friendly name of the identifier.</param>
		/// <param name="group">The group to search in.</param>
		/// <returns>An object that represents the specified OID.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="friendlyName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The OID was not found.</exception>
		// Token: 0x0600297C RID: 10620 RVA: 0x000BC54C File Offset: 0x000BA74C
		public static Oid FromFriendlyName(string friendlyName, OidGroup group)
		{
			if (friendlyName == null)
			{
				throw new ArgumentNullException("friendlyName");
			}
			string text = System.Security.Cryptography.X509Certificates.X509Utils.FindOidInfo(2U, friendlyName, group);
			if (text == null)
			{
				throw new CryptographicException(System.SR.GetString("Cryptography_Oid_InvalidValue"));
			}
			return new Oid(text, friendlyName, group);
		}

		/// <summary>Creates an <see cref="T:System.Security.Cryptography.Oid" /> object by using the specified OID value and group.</summary>
		/// <param name="oidValue">The OID value.</param>
		/// <param name="group">The group to search in.</param>
		/// <returns>A new instance of an <see cref="T:System.Security.Cryptography.Oid" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="oidValue" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The friendly name for the OID value was not found.</exception>
		// Token: 0x0600297D RID: 10621 RVA: 0x000BC58C File Offset: 0x000BA78C
		public static Oid FromOidValue(string oidValue, OidGroup group)
		{
			if (oidValue == null)
			{
				throw new ArgumentNullException("oidValue");
			}
			string text = System.Security.Cryptography.X509Certificates.X509Utils.FindOidInfo(1U, oidValue, group);
			if (text == null)
			{
				throw new CryptographicException(System.SR.GetString("Cryptography_Oid_InvalidValue"));
			}
			return new Oid(oidValue, text, group);
		}

		/// <summary>Gets or sets the dotted number of the identifier.</summary>
		/// <returns>The dotted number of the identifier.</returns>
		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x0600297E RID: 10622 RVA: 0x000BC5CB File Offset: 0x000BA7CB
		// (set) Token: 0x0600297F RID: 10623 RVA: 0x000BC5D3 File Offset: 0x000BA7D3
		public string Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		/// <summary>Gets or sets the friendly name of the identifier.</summary>
		/// <returns>The friendly name of the identifier.</returns>
		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06002980 RID: 10624 RVA: 0x000BC5DC File Offset: 0x000BA7DC
		// (set) Token: 0x06002981 RID: 10625 RVA: 0x000BC60C File Offset: 0x000BA80C
		public string FriendlyName
		{
			get
			{
				if (this.m_friendlyName == null && this.m_value != null)
				{
					this.m_friendlyName = System.Security.Cryptography.X509Certificates.X509Utils.FindOidInfoWithFallback(1U, this.m_value, this.m_group);
				}
				return this.m_friendlyName;
			}
			set
			{
				this.m_friendlyName = value;
				if (this.m_friendlyName != null)
				{
					string text = System.Security.Cryptography.X509Certificates.X509Utils.FindOidInfoWithFallback(2U, this.m_friendlyName, this.m_group);
					if (text != null)
					{
						this.m_value = text;
					}
				}
			}
		}

		// Token: 0x04002583 RID: 9603
		private string m_value;

		// Token: 0x04002584 RID: 9604
		private string m_friendlyName;

		// Token: 0x04002585 RID: 9605
		private OidGroup m_group;
	}
}
