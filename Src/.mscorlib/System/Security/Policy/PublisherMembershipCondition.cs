using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Util;

namespace System.Security.Policy
{
	/// <summary>Determines whether an assembly belongs to a code group by testing its software publisher's Authenticode X.509v3 certificate. This class cannot be inherited.</summary>
	// Token: 0x02000376 RID: 886
	[ComVisible(true)]
	[Serializable]
	public sealed class PublisherMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition, IReportMatchMembershipCondition
	{
		// Token: 0x06002C23 RID: 11299 RVA: 0x000A5847 File Offset: 0x000A3A47
		internal PublisherMembershipCondition()
		{
			this.m_element = null;
			this.m_certificate = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.PublisherMembershipCondition" /> class with the Authenticode X.509v3 certificate that determines membership.</summary>
		/// <param name="certificate">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> that contains the software publisher's public key.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="certificate" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002C24 RID: 11300 RVA: 0x000A585D File Offset: 0x000A3A5D
		public PublisherMembershipCondition(X509Certificate certificate)
		{
			PublisherMembershipCondition.CheckCertificate(certificate);
			this.m_certificate = new X509Certificate(certificate);
		}

		// Token: 0x06002C25 RID: 11301 RVA: 0x000A5877 File Offset: 0x000A3A77
		private static void CheckCertificate(X509Certificate certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
		}

		/// <summary>Gets or sets the Authenticode X.509v3 certificate for which the membership condition tests.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> for which the membership condition tests.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property value is <see langword="null" />.</exception>
		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06002C27 RID: 11303 RVA: 0x000A589B File Offset: 0x000A3A9B
		// (set) Token: 0x06002C26 RID: 11302 RVA: 0x000A5887 File Offset: 0x000A3A87
		public X509Certificate Certificate
		{
			get
			{
				if (this.m_certificate == null && this.m_element != null)
				{
					this.ParseCertificate();
				}
				if (this.m_certificate != null)
				{
					return new X509Certificate(this.m_certificate);
				}
				return null;
			}
			set
			{
				PublisherMembershipCondition.CheckCertificate(value);
				this.m_certificate = new X509Certificate(value);
			}
		}

		/// <summary>Creates and returns a string representation of the <see cref="T:System.Security.Policy.PublisherMembershipCondition" />.</summary>
		/// <returns>A representation of the <see cref="T:System.Security.Policy.PublisherMembershipCondition" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.PublisherMembershipCondition.Certificate" /> property is <see langword="null" />.</exception>
		// Token: 0x06002C28 RID: 11304 RVA: 0x000A58C8 File Offset: 0x000A3AC8
		public override string ToString()
		{
			if (this.m_certificate == null && this.m_element != null)
			{
				this.ParseCertificate();
			}
			if (this.m_certificate == null)
			{
				return Environment.GetResourceString("Publisher_ToString");
			}
			string subject = this.m_certificate.Subject;
			if (subject != null)
			{
				return string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Publisher_ToStringArg"), Hex.EncodeHexString(this.m_certificate.GetPublicKey()));
			}
			return Environment.GetResourceString("Publisher_ToString");
		}

		/// <summary>Determines whether the specified evidence satisfies the membership condition.</summary>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> against which to make the test.</param>
		/// <returns>
		///   <see langword="true" /> if the specified evidence satisfies the membership condition; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.PublisherMembershipCondition.Certificate" /> property is <see langword="null" />.</exception>
		// Token: 0x06002C29 RID: 11305 RVA: 0x000A593C File Offset: 0x000A3B3C
		public bool Check(Evidence evidence)
		{
			object obj = null;
			return ((IReportMatchMembershipCondition)this).Check(evidence, out obj);
		}

		// Token: 0x06002C2A RID: 11306 RVA: 0x000A5954 File Offset: 0x000A3B54
		bool IReportMatchMembershipCondition.Check(Evidence evidence, out object usedEvidence)
		{
			usedEvidence = null;
			if (evidence == null)
			{
				return false;
			}
			Publisher hostEvidence = evidence.GetHostEvidence<Publisher>();
			if (hostEvidence != null)
			{
				if (this.m_certificate == null && this.m_element != null)
				{
					this.ParseCertificate();
				}
				if (hostEvidence.Equals(new Publisher(this.m_certificate)))
				{
					usedEvidence = hostEvidence;
					return true;
				}
			}
			return false;
		}

		/// <summary>Creates an equivalent copy of the membership condition.</summary>
		/// <returns>A new, identical copy of the current membership condition.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.PublisherMembershipCondition.Certificate" /> property is <see langword="null" />.</exception>
		// Token: 0x06002C2B RID: 11307 RVA: 0x000A59A2 File Offset: 0x000A3BA2
		public IMembershipCondition Copy()
		{
			if (this.m_certificate == null && this.m_element != null)
			{
				this.ParseCertificate();
			}
			return new PublisherMembershipCondition(this.m_certificate);
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.PublisherMembershipCondition.Certificate" /> property is <see langword="null" />.</exception>
		// Token: 0x06002C2C RID: 11308 RVA: 0x000A59C5 File Offset: 0x000A3BC5
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="e" /> parameter is not a valid membership condition element.</exception>
		// Token: 0x06002C2D RID: 11309 RVA: 0x000A59CE File Offset: 0x000A3BCE
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		/// <summary>Creates an XML encoding of the security object and its current state with the specified <see cref="T:System.Security.Policy.PolicyLevel" />.</summary>
		/// <param name="level">The <see cref="T:System.Security.Policy.PolicyLevel" /> context, which is used to resolve <see cref="T:System.Security.NamedPermissionSet" /> references.</param>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.PublisherMembershipCondition.Certificate" /> property is <see langword="null" />.</exception>
		// Token: 0x06002C2E RID: 11310 RVA: 0x000A59D8 File Offset: 0x000A3BD8
		public SecurityElement ToXml(PolicyLevel level)
		{
			if (this.m_certificate == null && this.m_element != null)
			{
				this.ParseCertificate();
			}
			SecurityElement securityElement = new SecurityElement("IMembershipCondition");
			XMLUtil.AddClassAttribute(securityElement, base.GetType(), "System.Security.Policy.PublisherMembershipCondition");
			securityElement.AddAttribute("version", "1");
			if (this.m_certificate != null)
			{
				securityElement.AddAttribute("X509Certificate", this.m_certificate.GetRawCertDataString());
			}
			return securityElement;
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object.</param>
		/// <param name="level">The <see cref="T:System.Security.Policy.PolicyLevel" /> context, used to resolve <see cref="T:System.Security.NamedPermissionSet" /> references.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="e" /> parameter is not a valid membership condition element.</exception>
		// Token: 0x06002C2F RID: 11311 RVA: 0x000A5A48 File Offset: 0x000A3C48
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			if (!e.Tag.Equals("IMembershipCondition"))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MembershipConditionElement"));
			}
			lock (this)
			{
				this.m_element = e;
				this.m_certificate = null;
			}
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x000A5ABC File Offset: 0x000A3CBC
		private void ParseCertificate()
		{
			lock (this)
			{
				if (this.m_element != null)
				{
					string text = this.m_element.Attribute("X509Certificate");
					this.m_certificate = ((text == null) ? null : new X509Certificate(Hex.DecodeHexString(text)));
					PublisherMembershipCondition.CheckCertificate(this.m_certificate);
					this.m_element = null;
				}
			}
		}

		/// <summary>Determines whether the publisher certificate from the specified object is equivalent to the publisher certificate contained in the current <see cref="T:System.Security.Policy.PublisherMembershipCondition" />.</summary>
		/// <param name="o">The object to compare to the current <see cref="T:System.Security.Policy.PublisherMembershipCondition" />.</param>
		/// <returns>
		///   <see langword="true" /> if the publisher certificate from the specified object is equivalent to the publisher certificate contained in the current <see cref="T:System.Security.Policy.PublisherMembershipCondition" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.PublisherMembershipCondition.Certificate" /> property is <see langword="null" />.</exception>
		// Token: 0x06002C31 RID: 11313 RVA: 0x000A5B38 File Offset: 0x000A3D38
		public override bool Equals(object o)
		{
			PublisherMembershipCondition publisherMembershipCondition = o as PublisherMembershipCondition;
			if (publisherMembershipCondition != null)
			{
				if (this.m_certificate == null && this.m_element != null)
				{
					this.ParseCertificate();
				}
				if (publisherMembershipCondition.m_certificate == null && publisherMembershipCondition.m_element != null)
				{
					publisherMembershipCondition.ParseCertificate();
				}
				if (Publisher.PublicKeyEquals(this.m_certificate, publisherMembershipCondition.m_certificate))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Gets the hash code for the current membership condition.</summary>
		/// <returns>The hash code for the current membership condition.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.PublisherMembershipCondition.Certificate" /> property is <see langword="null" />.</exception>
		// Token: 0x06002C32 RID: 11314 RVA: 0x000A5B91 File Offset: 0x000A3D91
		public override int GetHashCode()
		{
			if (this.m_certificate == null && this.m_element != null)
			{
				this.ParseCertificate();
			}
			if (this.m_certificate != null)
			{
				return this.m_certificate.GetHashCode();
			}
			return typeof(PublisherMembershipCondition).GetHashCode();
		}

		// Token: 0x040011C2 RID: 4546
		private X509Certificate m_certificate;

		// Token: 0x040011C3 RID: 4547
		private SecurityElement m_element;
	}
}
