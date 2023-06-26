using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Util;

namespace System.Security.Policy
{
	/// <summary>Determines whether an assembly belongs to a code group by testing its strong name. This class cannot be inherited.</summary>
	// Token: 0x0200036B RID: 875
	[ComVisible(true)]
	[Serializable]
	public sealed class StrongNameMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition, IReportMatchMembershipCondition
	{
		// Token: 0x06002B7B RID: 11131 RVA: 0x000A3341 File Offset: 0x000A1541
		internal StrongNameMembershipCondition()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> class with the strong name public key blob, name, and version number that determine membership.</summary>
		/// <param name="blob">The strong name public key blob of the software publisher.</param>
		/// <param name="name">The simple name section of the strong name.</param>
		/// <param name="version">The version number of the strong name.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="blob" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="name" /> parameter is an empty string ("").</exception>
		// Token: 0x06002B7C RID: 11132 RVA: 0x000A334C File Offset: 0x000A154C
		public StrongNameMembershipCondition(StrongNamePublicKeyBlob blob, string name, Version version)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			if (name != null && name.Equals(""))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyStrongName"));
			}
			this.m_publicKeyBlob = blob;
			this.m_name = name;
			this.m_version = version;
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" /> of the <see cref="T:System.Security.Policy.StrongName" /> for which the membership condition tests.</summary>
		/// <returns>The <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" /> of the <see cref="T:System.Security.Policy.StrongName" /> for which the membership condition tests.</returns>
		/// <exception cref="T:System.ArgumentNullException">An attempt is made to set the <see cref="P:System.Security.Policy.StrongNameMembershipCondition.PublicKey" /> to <see langword="null" />.</exception>
		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06002B7E RID: 11134 RVA: 0x000A33B9 File Offset: 0x000A15B9
		// (set) Token: 0x06002B7D RID: 11133 RVA: 0x000A33A2 File Offset: 0x000A15A2
		public StrongNamePublicKeyBlob PublicKey
		{
			get
			{
				if (this.m_publicKeyBlob == null && this.m_element != null)
				{
					this.ParseKeyBlob();
				}
				return this.m_publicKeyBlob;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("PublicKey");
				}
				this.m_publicKeyBlob = value;
			}
		}

		/// <summary>Gets or sets the simple name of the <see cref="T:System.Security.Policy.StrongName" /> for which the membership condition tests.</summary>
		/// <returns>The simple name of the <see cref="T:System.Security.Policy.StrongName" /> for which the membership condition tests.</returns>
		/// <exception cref="T:System.ArgumentException">The value is <see langword="null" />.  
		///  -or-  
		///  The value is an empty string ("").</exception>
		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06002B80 RID: 11136 RVA: 0x000A343C File Offset: 0x000A163C
		// (set) Token: 0x06002B7F RID: 11135 RVA: 0x000A33D8 File Offset: 0x000A15D8
		public string Name
		{
			get
			{
				if (this.m_name == null && this.m_element != null)
				{
					this.ParseName();
				}
				return this.m_name;
			}
			set
			{
				if (value == null)
				{
					if (this.m_publicKeyBlob == null && this.m_element != null)
					{
						this.ParseKeyBlob();
					}
					if (this.m_version == null && this.m_element != null)
					{
						this.ParseVersion();
					}
					this.m_element = null;
				}
				else if (value.Length == 0)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"));
				}
				this.m_name = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Version" /> of the <see cref="T:System.Security.Policy.StrongName" /> for which the membership condition tests.</summary>
		/// <returns>The <see cref="T:System.Version" /> of the <see cref="T:System.Security.Policy.StrongName" /> for which the membership condition tests.</returns>
		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06002B82 RID: 11138 RVA: 0x000A34AC File Offset: 0x000A16AC
		// (set) Token: 0x06002B81 RID: 11137 RVA: 0x000A345C File Offset: 0x000A165C
		public Version Version
		{
			get
			{
				if (this.m_version == null && this.m_element != null)
				{
					this.ParseVersion();
				}
				return this.m_version;
			}
			set
			{
				if (value == null)
				{
					if (this.m_name == null && this.m_element != null)
					{
						this.ParseName();
					}
					if (this.m_publicKeyBlob == null && this.m_element != null)
					{
						this.ParseKeyBlob();
					}
					this.m_element = null;
				}
				this.m_version = value;
			}
		}

		/// <summary>Determines whether the specified evidence satisfies the membership condition.</summary>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> against which to make the test.</param>
		/// <returns>
		///   <see langword="true" /> if the specified evidence satisfies the membership condition; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002B83 RID: 11139 RVA: 0x000A34CC File Offset: 0x000A16CC
		public bool Check(Evidence evidence)
		{
			object obj = null;
			return ((IReportMatchMembershipCondition)this).Check(evidence, out obj);
		}

		// Token: 0x06002B84 RID: 11140 RVA: 0x000A34E4 File Offset: 0x000A16E4
		bool IReportMatchMembershipCondition.Check(Evidence evidence, out object usedEvidence)
		{
			usedEvidence = null;
			if (evidence == null)
			{
				return false;
			}
			StrongName delayEvaluatedHostEvidence = evidence.GetDelayEvaluatedHostEvidence<StrongName>();
			if (delayEvaluatedHostEvidence != null)
			{
				bool flag = this.PublicKey != null && this.PublicKey.Equals(delayEvaluatedHostEvidence.PublicKey);
				bool flag2 = this.Name == null || (delayEvaluatedHostEvidence.Name != null && StrongName.CompareNames(delayEvaluatedHostEvidence.Name, this.Name));
				bool flag3 = this.Version == null || (delayEvaluatedHostEvidence.Version != null && delayEvaluatedHostEvidence.Version.CompareTo(this.Version) == 0);
				if (flag && flag2 && flag3)
				{
					usedEvidence = delayEvaluatedHostEvidence;
					return true;
				}
			}
			return false;
		}

		/// <summary>Creates an equivalent copy of the current <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />.</summary>
		/// <returns>A new, identical copy of the current <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /></returns>
		// Token: 0x06002B85 RID: 11141 RVA: 0x000A3580 File Offset: 0x000A1780
		public IMembershipCondition Copy()
		{
			return new StrongNameMembershipCondition(this.PublicKey, this.Name, this.Version);
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x06002B86 RID: 11142 RVA: 0x000A3599 File Offset: 0x000A1799
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object.</param>
		// Token: 0x06002B87 RID: 11143 RVA: 0x000A35A2 File Offset: 0x000A17A2
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		/// <summary>Creates an XML encoding of the security object and its current state with the specified <see cref="T:System.Security.Policy.PolicyLevel" />.</summary>
		/// <param name="level">The <see cref="T:System.Security.Policy.PolicyLevel" /> context, which is used to resolve <see cref="T:System.Security.NamedPermissionSet" /> references.</param>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x06002B88 RID: 11144 RVA: 0x000A35AC File Offset: 0x000A17AC
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = new SecurityElement("IMembershipCondition");
			XMLUtil.AddClassAttribute(securityElement, base.GetType(), "System.Security.Policy.StrongNameMembershipCondition");
			securityElement.AddAttribute("version", "1");
			if (this.PublicKey != null)
			{
				securityElement.AddAttribute("PublicKeyBlob", Hex.EncodeHexString(this.PublicKey.PublicKey));
			}
			if (this.Name != null)
			{
				securityElement.AddAttribute("Name", this.Name);
			}
			if (this.Version != null)
			{
				securityElement.AddAttribute("AssemblyVersion", this.Version.ToString());
			}
			return securityElement;
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object.</param>
		/// <param name="level">The <see cref="T:System.Security.Policy.PolicyLevel" /> context, used to resolve <see cref="T:System.Security.NamedPermissionSet" /> references.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="e" /> parameter is not a valid membership condition element.</exception>
		// Token: 0x06002B89 RID: 11145 RVA: 0x000A3640 File Offset: 0x000A1840
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
				this.m_name = null;
				this.m_publicKeyBlob = null;
				this.m_version = null;
				this.m_element = e;
			}
		}

		// Token: 0x06002B8A RID: 11146 RVA: 0x000A36C4 File Offset: 0x000A18C4
		private void ParseName()
		{
			lock (this)
			{
				if (this.m_element != null)
				{
					string text = this.m_element.Attribute("Name");
					this.m_name = ((text == null) ? null : text);
					if (this.m_version != null && this.m_name != null && this.m_publicKeyBlob != null)
					{
						this.m_element = null;
					}
				}
			}
		}

		// Token: 0x06002B8B RID: 11147 RVA: 0x000A3740 File Offset: 0x000A1940
		private void ParseKeyBlob()
		{
			lock (this)
			{
				if (this.m_element != null)
				{
					string text = this.m_element.Attribute("PublicKeyBlob");
					StrongNamePublicKeyBlob strongNamePublicKeyBlob = new StrongNamePublicKeyBlob();
					if (text == null)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_BlobCannotBeNull"));
					}
					strongNamePublicKeyBlob.PublicKey = Hex.DecodeHexString(text);
					this.m_publicKeyBlob = strongNamePublicKeyBlob;
					if (this.m_version != null && this.m_name != null && this.m_publicKeyBlob != null)
					{
						this.m_element = null;
					}
				}
			}
		}

		// Token: 0x06002B8C RID: 11148 RVA: 0x000A37E0 File Offset: 0x000A19E0
		private void ParseVersion()
		{
			lock (this)
			{
				if (this.m_element != null)
				{
					string text = this.m_element.Attribute("AssemblyVersion");
					this.m_version = ((text == null) ? null : new Version(text));
					if (this.m_version != null && this.m_name != null && this.m_publicKeyBlob != null)
					{
						this.m_element = null;
					}
				}
			}
		}

		/// <summary>Creates and returns a string representation of the current <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />.</summary>
		/// <returns>A representation of the current <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />.</returns>
		// Token: 0x06002B8D RID: 11149 RVA: 0x000A3864 File Offset: 0x000A1A64
		public override string ToString()
		{
			string text = "";
			string text2 = "";
			if (this.Name != null)
			{
				text = " " + string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("StrongName_Name"), this.Name);
			}
			if (this.Version != null)
			{
				text2 = " " + string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("StrongName_Version"), this.Version);
			}
			return string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("StrongName_ToString"), Hex.EncodeHexString(this.PublicKey.PublicKey), text, text2);
		}

		/// <summary>Determines whether the <see cref="T:System.Security.Policy.StrongName" /> from the specified object is equivalent to the <see cref="T:System.Security.Policy.StrongName" /> contained in the current <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />.</summary>
		/// <param name="o">The object to compare to the current <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Security.Policy.StrongName" /> from the specified object is equivalent to the <see cref="T:System.Security.Policy.StrongName" /> contained in the current <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.StrongNameMembershipCondition.PublicKey" /> property of the current object or the specified object is <see langword="null" />.</exception>
		// Token: 0x06002B8E RID: 11150 RVA: 0x000A3900 File Offset: 0x000A1B00
		public override bool Equals(object o)
		{
			StrongNameMembershipCondition strongNameMembershipCondition = o as StrongNameMembershipCondition;
			if (strongNameMembershipCondition != null)
			{
				if (this.m_publicKeyBlob == null && this.m_element != null)
				{
					this.ParseKeyBlob();
				}
				if (strongNameMembershipCondition.m_publicKeyBlob == null && strongNameMembershipCondition.m_element != null)
				{
					strongNameMembershipCondition.ParseKeyBlob();
				}
				if (object.Equals(this.m_publicKeyBlob, strongNameMembershipCondition.m_publicKeyBlob))
				{
					if (this.m_name == null && this.m_element != null)
					{
						this.ParseName();
					}
					if (strongNameMembershipCondition.m_name == null && strongNameMembershipCondition.m_element != null)
					{
						strongNameMembershipCondition.ParseName();
					}
					if (object.Equals(this.m_name, strongNameMembershipCondition.m_name))
					{
						if (this.m_version == null && this.m_element != null)
						{
							this.ParseVersion();
						}
						if (strongNameMembershipCondition.m_version == null && strongNameMembershipCondition.m_element != null)
						{
							strongNameMembershipCondition.ParseVersion();
						}
						if (object.Equals(this.m_version, strongNameMembershipCondition.m_version))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		/// <summary>Returns the hash code for the current <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />.</summary>
		/// <returns>The hash code for the current <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.StrongNameMembershipCondition.PublicKey" /> property is <see langword="null" />.</exception>
		// Token: 0x06002B8F RID: 11151 RVA: 0x000A39EC File Offset: 0x000A1BEC
		public override int GetHashCode()
		{
			if (this.m_publicKeyBlob == null && this.m_element != null)
			{
				this.ParseKeyBlob();
			}
			if (this.m_publicKeyBlob != null)
			{
				return this.m_publicKeyBlob.GetHashCode();
			}
			if (this.m_name == null && this.m_element != null)
			{
				this.ParseName();
			}
			if (this.m_version == null && this.m_element != null)
			{
				this.ParseVersion();
			}
			if (this.m_name != null || this.m_version != null)
			{
				return ((this.m_name == null) ? 0 : this.m_name.GetHashCode()) + ((this.m_version == null) ? 0 : this.m_version.GetHashCode());
			}
			return typeof(StrongNameMembershipCondition).GetHashCode();
		}

		// Token: 0x040011A8 RID: 4520
		private StrongNamePublicKeyBlob m_publicKeyBlob;

		// Token: 0x040011A9 RID: 4521
		private string m_name;

		// Token: 0x040011AA RID: 4522
		private Version m_version;

		// Token: 0x040011AB RID: 4523
		private SecurityElement m_element;

		// Token: 0x040011AC RID: 4524
		private const string s_tagName = "Name";

		// Token: 0x040011AD RID: 4525
		private const string s_tagVersion = "AssemblyVersion";

		// Token: 0x040011AE RID: 4526
		private const string s_tagPublicKeyBlob = "PublicKeyBlob";
	}
}
