using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Util;
using System.Text;

namespace System.Security.Policy
{
	/// <summary>Represents the statement of a <see cref="T:System.Security.Policy.CodeGroup" /> describing the permissions and other information that apply to code with a particular set of evidence. This class cannot be inherited.</summary>
	// Token: 0x02000367 RID: 871
	[ComVisible(true)]
	[Serializable]
	public sealed class PolicyStatement : ISecurityPolicyEncodable, ISecurityEncodable
	{
		// Token: 0x06002B30 RID: 11056 RVA: 0x000A2208 File Offset: 0x000A0408
		internal PolicyStatement()
		{
			this.m_permSet = null;
			this.m_attributes = PolicyStatementAttribute.Nothing;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.PolicyStatement" /> class with the specified <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <param name="permSet">The <see cref="T:System.Security.PermissionSet" /> with which to initialize the new instance.</param>
		// Token: 0x06002B31 RID: 11057 RVA: 0x000A221E File Offset: 0x000A041E
		public PolicyStatement(PermissionSet permSet)
			: this(permSet, PolicyStatementAttribute.Nothing)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.PolicyStatement" /> class with the specified <see cref="T:System.Security.PermissionSet" /> and attributes.</summary>
		/// <param name="permSet">The <see cref="T:System.Security.PermissionSet" /> with which to initialize the new instance.</param>
		/// <param name="attributes">A bitwise combination of the <see cref="T:System.Security.Policy.PolicyStatementAttribute" /> values.</param>
		// Token: 0x06002B32 RID: 11058 RVA: 0x000A2228 File Offset: 0x000A0428
		public PolicyStatement(PermissionSet permSet, PolicyStatementAttribute attributes)
		{
			if (permSet == null)
			{
				this.m_permSet = new PermissionSet(false);
			}
			else
			{
				this.m_permSet = permSet.Copy();
			}
			if (PolicyStatement.ValidProperties(attributes))
			{
				this.m_attributes = attributes;
			}
		}

		// Token: 0x06002B33 RID: 11059 RVA: 0x000A225C File Offset: 0x000A045C
		private PolicyStatement(PermissionSet permSet, PolicyStatementAttribute attributes, bool copy)
		{
			if (permSet != null)
			{
				if (copy)
				{
					this.m_permSet = permSet.Copy();
				}
				else
				{
					this.m_permSet = permSet;
				}
			}
			else
			{
				this.m_permSet = new PermissionSet(false);
			}
			this.m_attributes = attributes;
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.PermissionSet" /> of the policy statement.</summary>
		/// <returns>The <see cref="T:System.Security.PermissionSet" /> of the policy statement.</returns>
		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06002B34 RID: 11060 RVA: 0x000A2294 File Offset: 0x000A0494
		// (set) Token: 0x06002B35 RID: 11061 RVA: 0x000A22D8 File Offset: 0x000A04D8
		public PermissionSet PermissionSet
		{
			get
			{
				PermissionSet permissionSet;
				lock (this)
				{
					permissionSet = this.m_permSet.Copy();
				}
				return permissionSet;
			}
			set
			{
				lock (this)
				{
					if (value == null)
					{
						this.m_permSet = new PermissionSet(false);
					}
					else
					{
						this.m_permSet = value.Copy();
					}
				}
			}
		}

		// Token: 0x06002B36 RID: 11062 RVA: 0x000A232C File Offset: 0x000A052C
		internal void SetPermissionSetNoCopy(PermissionSet permSet)
		{
			this.m_permSet = permSet;
		}

		// Token: 0x06002B37 RID: 11063 RVA: 0x000A2338 File Offset: 0x000A0538
		internal PermissionSet GetPermissionSetNoCopy()
		{
			PermissionSet permSet;
			lock (this)
			{
				permSet = this.m_permSet;
			}
			return permSet;
		}

		/// <summary>Gets or sets the attributes of the policy statement.</summary>
		/// <returns>The attributes of the policy statement.</returns>
		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06002B38 RID: 11064 RVA: 0x000A2378 File Offset: 0x000A0578
		// (set) Token: 0x06002B39 RID: 11065 RVA: 0x000A2380 File Offset: 0x000A0580
		public PolicyStatementAttribute Attributes
		{
			get
			{
				return this.m_attributes;
			}
			set
			{
				if (PolicyStatement.ValidProperties(value))
				{
					this.m_attributes = value;
				}
			}
		}

		/// <summary>Creates an equivalent copy of the current policy statement.</summary>
		/// <returns>A new copy of the <see cref="T:System.Security.Policy.PolicyStatement" /> with <see cref="P:System.Security.Policy.PolicyStatement.PermissionSet" /> and <see cref="P:System.Security.Policy.PolicyStatement.Attributes" /> identical to those of the current <see cref="T:System.Security.Policy.PolicyStatement" />.</returns>
		// Token: 0x06002B3A RID: 11066 RVA: 0x000A2394 File Offset: 0x000A0594
		public PolicyStatement Copy()
		{
			PolicyStatement policyStatement = new PolicyStatement(this.m_permSet, this.Attributes, true);
			if (this.HasDependentEvidence)
			{
				policyStatement.m_dependentEvidence = new List<IDelayEvaluatedEvidence>(this.m_dependentEvidence);
			}
			return policyStatement;
		}

		/// <summary>Gets a string representation of the attributes of the policy statement.</summary>
		/// <returns>A text string representing the attributes of the policy statement.</returns>
		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06002B3B RID: 11067 RVA: 0x000A23D0 File Offset: 0x000A05D0
		public string AttributeString
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				bool flag = true;
				if (this.GetFlag(1))
				{
					stringBuilder.Append("Exclusive");
					flag = false;
				}
				if (this.GetFlag(2))
				{
					if (!flag)
					{
						stringBuilder.Append(" ");
					}
					stringBuilder.Append("LevelFinal");
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06002B3C RID: 11068 RVA: 0x000A2426 File Offset: 0x000A0626
		private static bool ValidProperties(PolicyStatementAttribute attributes)
		{
			if ((attributes & ~(PolicyStatementAttribute.Exclusive | PolicyStatementAttribute.LevelFinal)) == PolicyStatementAttribute.Nothing)
			{
				return true;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"));
		}

		// Token: 0x06002B3D RID: 11069 RVA: 0x000A243F File Offset: 0x000A063F
		private bool GetFlag(int flag)
		{
			return (flag & (int)this.m_attributes) != 0;
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06002B3E RID: 11070 RVA: 0x000A244C File Offset: 0x000A064C
		internal IEnumerable<IDelayEvaluatedEvidence> DependentEvidence
		{
			get
			{
				return this.m_dependentEvidence.AsReadOnly();
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06002B3F RID: 11071 RVA: 0x000A2459 File Offset: 0x000A0659
		internal bool HasDependentEvidence
		{
			get
			{
				return this.m_dependentEvidence != null && this.m_dependentEvidence.Count > 0;
			}
		}

		// Token: 0x06002B40 RID: 11072 RVA: 0x000A2473 File Offset: 0x000A0673
		internal void AddDependentEvidence(IDelayEvaluatedEvidence dependentEvidence)
		{
			if (this.m_dependentEvidence == null)
			{
				this.m_dependentEvidence = new List<IDelayEvaluatedEvidence>();
			}
			this.m_dependentEvidence.Add(dependentEvidence);
		}

		// Token: 0x06002B41 RID: 11073 RVA: 0x000A2494 File Offset: 0x000A0694
		internal void InplaceUnion(PolicyStatement childPolicy)
		{
			if ((this.Attributes & childPolicy.Attributes & PolicyStatementAttribute.Exclusive) == PolicyStatementAttribute.Exclusive)
			{
				throw new PolicyException(Environment.GetResourceString("Policy_MultipleExclusive"));
			}
			if (childPolicy.HasDependentEvidence)
			{
				bool flag = this.m_permSet.IsSubsetOf(childPolicy.GetPermissionSetNoCopy()) && !childPolicy.GetPermissionSetNoCopy().IsSubsetOf(this.m_permSet);
				if (this.HasDependentEvidence || flag)
				{
					if (this.m_dependentEvidence == null)
					{
						this.m_dependentEvidence = new List<IDelayEvaluatedEvidence>();
					}
					this.m_dependentEvidence.AddRange(childPolicy.DependentEvidence);
				}
			}
			if ((childPolicy.Attributes & PolicyStatementAttribute.Exclusive) == PolicyStatementAttribute.Exclusive)
			{
				this.m_permSet = childPolicy.GetPermissionSetNoCopy();
				this.Attributes = childPolicy.Attributes;
				return;
			}
			this.m_permSet.InplaceUnion(childPolicy.GetPermissionSetNoCopy());
			this.Attributes |= childPolicy.Attributes;
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x06002B42 RID: 11074 RVA: 0x000A256C File Offset: 0x000A076C
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		/// <summary>Reconstructs a security object with a given state from an XML encoding.</summary>
		/// <param name="et">The XML encoding to use to reconstruct the security object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="et" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="et" /> parameter is not a valid <see cref="T:System.Security.Policy.PolicyStatement" /> encoding.</exception>
		// Token: 0x06002B43 RID: 11075 RVA: 0x000A2575 File Offset: 0x000A0775
		public void FromXml(SecurityElement et)
		{
			this.FromXml(et, null);
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <param name="level">The <see cref="T:System.Security.Policy.PolicyLevel" /> context for lookup of <see cref="T:System.Security.NamedPermissionSet" /> values.</param>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x06002B44 RID: 11076 RVA: 0x000A257F File Offset: 0x000A077F
		public SecurityElement ToXml(PolicyLevel level)
		{
			return this.ToXml(level, false);
		}

		// Token: 0x06002B45 RID: 11077 RVA: 0x000A258C File Offset: 0x000A078C
		internal SecurityElement ToXml(PolicyLevel level, bool useInternal)
		{
			SecurityElement securityElement = new SecurityElement("PolicyStatement");
			securityElement.AddAttribute("version", "1");
			if (this.m_attributes != PolicyStatementAttribute.Nothing)
			{
				securityElement.AddAttribute("Attributes", XMLUtil.BitFieldEnumToString(typeof(PolicyStatementAttribute), this.m_attributes));
			}
			lock (this)
			{
				if (this.m_permSet != null)
				{
					if (this.m_permSet is NamedPermissionSet)
					{
						NamedPermissionSet namedPermissionSet = (NamedPermissionSet)this.m_permSet;
						if (level != null && level.GetNamedPermissionSet(namedPermissionSet.Name) != null)
						{
							securityElement.AddAttribute("PermissionSetName", namedPermissionSet.Name);
						}
						else if (useInternal)
						{
							securityElement.AddChild(namedPermissionSet.InternalToXml());
						}
						else
						{
							securityElement.AddChild(namedPermissionSet.ToXml());
						}
					}
					else if (useInternal)
					{
						securityElement.AddChild(this.m_permSet.InternalToXml());
					}
					else
					{
						securityElement.AddChild(this.m_permSet.ToXml());
					}
				}
			}
			return securityElement;
		}

		/// <summary>Reconstructs a security object with a given state from an XML encoding.</summary>
		/// <param name="et">The XML encoding to use to reconstruct the security object.</param>
		/// <param name="level">The <see cref="T:System.Security.Policy.PolicyLevel" /> context for lookup of <see cref="T:System.Security.NamedPermissionSet" /> values.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="et" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="et" /> parameter is not a valid <see cref="T:System.Security.Policy.PolicyStatement" /> encoding.</exception>
		// Token: 0x06002B46 RID: 11078 RVA: 0x000A2698 File Offset: 0x000A0898
		[SecuritySafeCritical]
		public void FromXml(SecurityElement et, PolicyLevel level)
		{
			this.FromXml(et, level, false);
		}

		// Token: 0x06002B47 RID: 11079 RVA: 0x000A26A4 File Offset: 0x000A08A4
		[SecurityCritical]
		internal void FromXml(SecurityElement et, PolicyLevel level, bool allowInternalOnly)
		{
			if (et == null)
			{
				throw new ArgumentNullException("et");
			}
			if (!et.Tag.Equals("PolicyStatement"))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidXMLElement"), "PolicyStatement", base.GetType().FullName));
			}
			this.m_attributes = PolicyStatementAttribute.Nothing;
			string text = et.Attribute("Attributes");
			if (text != null)
			{
				this.m_attributes = (PolicyStatementAttribute)Enum.Parse(typeof(PolicyStatementAttribute), text);
			}
			lock (this)
			{
				this.m_permSet = null;
				if (level != null)
				{
					string text2 = et.Attribute("PermissionSetName");
					if (text2 != null)
					{
						this.m_permSet = level.GetNamedPermissionSetInternal(text2);
						if (this.m_permSet == null)
						{
							this.m_permSet = new PermissionSet(PermissionState.None);
						}
					}
				}
				if (this.m_permSet == null)
				{
					SecurityElement securityElement = et.SearchForChildByTag("PermissionSet");
					if (securityElement != null)
					{
						string text3 = securityElement.Attribute("class");
						if (text3 != null && (text3.Equals("NamedPermissionSet") || text3.Equals("System.Security.NamedPermissionSet")))
						{
							this.m_permSet = new NamedPermissionSet("DefaultName", PermissionState.None);
						}
						else
						{
							this.m_permSet = new PermissionSet(PermissionState.None);
						}
						try
						{
							this.m_permSet.FromXml(securityElement, allowInternalOnly, true);
							goto IL_14F;
						}
						catch
						{
							goto IL_14F;
						}
					}
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXML"));
				}
				IL_14F:
				if (this.m_permSet == null)
				{
					this.m_permSet = new PermissionSet(PermissionState.None);
				}
			}
		}

		// Token: 0x06002B48 RID: 11080 RVA: 0x000A283C File Offset: 0x000A0A3C
		[SecurityCritical]
		internal void FromXml(SecurityDocument doc, int position, PolicyLevel level, bool allowInternalOnly)
		{
			if (doc == null)
			{
				throw new ArgumentNullException("doc");
			}
			if (!doc.GetTagForElement(position).Equals("PolicyStatement"))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidXMLElement"), "PolicyStatement", base.GetType().FullName));
			}
			this.m_attributes = PolicyStatementAttribute.Nothing;
			string attributeForElement = doc.GetAttributeForElement(position, "Attributes");
			if (attributeForElement != null)
			{
				this.m_attributes = (PolicyStatementAttribute)Enum.Parse(typeof(PolicyStatementAttribute), attributeForElement);
			}
			lock (this)
			{
				this.m_permSet = null;
				if (level != null)
				{
					string attributeForElement2 = doc.GetAttributeForElement(position, "PermissionSetName");
					if (attributeForElement2 != null)
					{
						this.m_permSet = level.GetNamedPermissionSetInternal(attributeForElement2);
						if (this.m_permSet == null)
						{
							this.m_permSet = new PermissionSet(PermissionState.None);
						}
					}
				}
				if (this.m_permSet == null)
				{
					ArrayList childrenPositionForElement = doc.GetChildrenPositionForElement(position);
					int num = -1;
					for (int i = 0; i < childrenPositionForElement.Count; i++)
					{
						if (doc.GetTagForElement((int)childrenPositionForElement[i]).Equals("PermissionSet"))
						{
							num = (int)childrenPositionForElement[i];
						}
					}
					if (num == -1)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXML"));
					}
					string attributeForElement3 = doc.GetAttributeForElement(num, "class");
					if (attributeForElement3 != null && (attributeForElement3.Equals("NamedPermissionSet") || attributeForElement3.Equals("System.Security.NamedPermissionSet")))
					{
						this.m_permSet = new NamedPermissionSet("DefaultName", PermissionState.None);
					}
					else
					{
						this.m_permSet = new PermissionSet(PermissionState.None);
					}
					this.m_permSet.FromXml(doc, num, allowInternalOnly);
				}
				if (this.m_permSet == null)
				{
					this.m_permSet = new PermissionSet(PermissionState.None);
				}
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.Security.Policy.PolicyStatement" /> object is equal to the current <see cref="T:System.Security.Policy.PolicyStatement" />.</summary>
		/// <param name="obj">The <see cref="T:System.Security.Policy.PolicyStatement" /> object to compare with the current <see cref="T:System.Security.Policy.PolicyStatement" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Security.Policy.PolicyStatement" /> is equal to the current <see cref="T:System.Security.Policy.PolicyStatement" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002B49 RID: 11081 RVA: 0x000A2A1C File Offset: 0x000A0C1C
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			PolicyStatement policyStatement = obj as PolicyStatement;
			return policyStatement != null && this.m_attributes == policyStatement.m_attributes && object.Equals(this.m_permSet, policyStatement.m_permSet);
		}

		/// <summary>Gets a hash code for the <see cref="T:System.Security.Policy.PolicyStatement" /> object that is suitable for use in hashing algorithms and data structures such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Security.Policy.PolicyStatement" /> object.</returns>
		// Token: 0x06002B4A RID: 11082 RVA: 0x000A2A5C File Offset: 0x000A0C5C
		[ComVisible(false)]
		public override int GetHashCode()
		{
			int num = (int)this.m_attributes;
			if (this.m_permSet != null)
			{
				num ^= this.m_permSet.GetHashCode();
			}
			return num;
		}

		// Token: 0x0400119D RID: 4509
		internal PermissionSet m_permSet;

		// Token: 0x0400119E RID: 4510
		[NonSerialized]
		private List<IDelayEvaluatedEvidence> m_dependentEvidence;

		// Token: 0x0400119F RID: 4511
		internal PolicyStatementAttribute m_attributes;
	}
}
