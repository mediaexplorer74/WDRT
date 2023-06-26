using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Policy
{
	/// <summary>Represents the abstract base class from which all implementations of code groups must derive.</summary>
	// Token: 0x02000349 RID: 841
	[ComVisible(true)]
	[Serializable]
	public abstract class CodeGroup
	{
		// Token: 0x060029EC RID: 10732 RVA: 0x0009C009 File Offset: 0x0009A209
		internal CodeGroup()
		{
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x0009C014 File Offset: 0x0009A214
		internal CodeGroup(IMembershipCondition membershipCondition, PermissionSet permSet)
		{
			this.m_membershipCondition = membershipCondition;
			this.m_policy = new PolicyStatement();
			this.m_policy.SetPermissionSetNoCopy(permSet);
			this.m_children = ArrayList.Synchronized(new ArrayList());
			this.m_element = null;
			this.m_parentLevel = null;
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Security.Policy.CodeGroup" />.</summary>
		/// <param name="membershipCondition">A membership condition that tests evidence to determine whether this code group applies policy.</param>
		/// <param name="policy">The policy statement for the code group in the form of a permission set and attributes to grant code that matches the membership condition.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="membershipCondition" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The type of the <paramref name="membershipCondition" /> parameter is not valid.  
		///  -or-  
		///  The type of the <paramref name="policy" /> parameter is not valid.</exception>
		// Token: 0x060029EE RID: 10734 RVA: 0x0009C064 File Offset: 0x0009A264
		protected CodeGroup(IMembershipCondition membershipCondition, PolicyStatement policy)
		{
			if (membershipCondition == null)
			{
				throw new ArgumentNullException("membershipCondition");
			}
			if (policy == null)
			{
				this.m_policy = null;
			}
			else
			{
				this.m_policy = policy.Copy();
			}
			this.m_membershipCondition = membershipCondition.Copy();
			this.m_children = ArrayList.Synchronized(new ArrayList());
			this.m_element = null;
			this.m_parentLevel = null;
		}

		/// <summary>Adds a child code group to the current code group.</summary>
		/// <param name="group">The code group to be added as a child. This new child code group is added to the end of the list.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="group" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="group" /> parameter is not a valid code group.</exception>
		// Token: 0x060029EF RID: 10735 RVA: 0x0009C0C8 File Offset: 0x0009A2C8
		[SecuritySafeCritical]
		public void AddChild(CodeGroup group)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			if (this.m_children == null)
			{
				this.ParseChildren();
			}
			lock (this)
			{
				this.m_children.Add(group.Copy());
			}
		}

		// Token: 0x060029F0 RID: 10736 RVA: 0x0009C12C File Offset: 0x0009A32C
		[SecurityCritical]
		internal void AddChildInternal(CodeGroup group)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			if (this.m_children == null)
			{
				this.ParseChildren();
			}
			lock (this)
			{
				this.m_children.Add(group);
			}
		}

		/// <summary>Removes the specified child code group.</summary>
		/// <param name="group">The code group to be removed as a child.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="group" /> parameter is not an immediate child code group of the current code group.</exception>
		// Token: 0x060029F1 RID: 10737 RVA: 0x0009C18C File Offset: 0x0009A38C
		[SecuritySafeCritical]
		public void RemoveChild(CodeGroup group)
		{
			if (group == null)
			{
				return;
			}
			if (this.m_children == null)
			{
				this.ParseChildren();
			}
			lock (this)
			{
				int num = this.m_children.IndexOf(group);
				if (num != -1)
				{
					this.m_children.RemoveAt(num);
				}
			}
		}

		/// <summary>Gets or sets an ordered list of the child code groups of a code group.</summary>
		/// <returns>A list of child code groups.</returns>
		/// <exception cref="T:System.ArgumentNullException">An attempt is made to set this property to <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">An attempt is made to set this property with a list of children that are not <see cref="T:System.Security.Policy.CodeGroup" /> objects.</exception>
		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x060029F2 RID: 10738 RVA: 0x0009C1F0 File Offset: 0x0009A3F0
		// (set) Token: 0x060029F3 RID: 10739 RVA: 0x0009C278 File Offset: 0x0009A478
		public IList Children
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_children == null)
				{
					this.ParseChildren();
				}
				IList list2;
				lock (this)
				{
					IList list = new ArrayList(this.m_children.Count);
					foreach (object obj in this.m_children)
					{
						list.Add(((CodeGroup)obj).Copy());
					}
					list2 = list;
				}
				return list2;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Children");
				}
				ArrayList arrayList = ArrayList.Synchronized(new ArrayList(value.Count));
				foreach (object obj in value)
				{
					CodeGroup codeGroup = obj as CodeGroup;
					if (codeGroup == null)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_CodeGroupChildrenMustBeCodeGroups"));
					}
					arrayList.Add(codeGroup.Copy());
				}
				this.m_children = arrayList;
			}
		}

		// Token: 0x060029F4 RID: 10740 RVA: 0x0009C2E8 File Offset: 0x0009A4E8
		[SecurityCritical]
		internal IList GetChildrenInternal()
		{
			if (this.m_children == null)
			{
				this.ParseChildren();
			}
			return this.m_children;
		}

		/// <summary>Gets or sets the code group's membership condition.</summary>
		/// <returns>The membership condition that determines to which evidence the code group is applicable.</returns>
		/// <exception cref="T:System.ArgumentNullException">An attempt is made to set this parameter to <see langword="null" />.</exception>
		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x060029F5 RID: 10741 RVA: 0x0009C2FE File Offset: 0x0009A4FE
		// (set) Token: 0x060029F6 RID: 10742 RVA: 0x0009C321 File Offset: 0x0009A521
		public IMembershipCondition MembershipCondition
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_membershipCondition == null && this.m_element != null)
				{
					this.ParseMembershipCondition();
				}
				return this.m_membershipCondition.Copy();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("MembershipCondition");
				}
				this.m_membershipCondition = value.Copy();
			}
		}

		/// <summary>Gets or sets the policy statement associated with the code group.</summary>
		/// <returns>The policy statement for the code group.</returns>
		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x060029F7 RID: 10743 RVA: 0x0009C33D File Offset: 0x0009A53D
		// (set) Token: 0x060029F8 RID: 10744 RVA: 0x0009C36A File Offset: 0x0009A56A
		public PolicyStatement PolicyStatement
		{
			get
			{
				if (this.m_policy == null && this.m_element != null)
				{
					this.ParsePolicy();
				}
				if (this.m_policy != null)
				{
					return this.m_policy.Copy();
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					this.m_policy = value.Copy();
					return;
				}
				this.m_policy = null;
			}
		}

		/// <summary>Gets or sets the name of the code group.</summary>
		/// <returns>The name of the code group.</returns>
		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x060029F9 RID: 10745 RVA: 0x0009C383 File Offset: 0x0009A583
		// (set) Token: 0x060029FA RID: 10746 RVA: 0x0009C38B File Offset: 0x0009A58B
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		/// <summary>Gets or sets the description of the code group.</summary>
		/// <returns>The description of the code group.</returns>
		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x060029FB RID: 10747 RVA: 0x0009C394 File Offset: 0x0009A594
		// (set) Token: 0x060029FC RID: 10748 RVA: 0x0009C39C File Offset: 0x0009A59C
		public string Description
		{
			get
			{
				return this.m_description;
			}
			set
			{
				this.m_description = value;
			}
		}

		/// <summary>When overridden in a derived class, resolves policy for the code group and its descendants for a set of evidence.</summary>
		/// <param name="evidence">The evidence for the assembly.</param>
		/// <returns>A policy statement that consists of the permissions granted by the code group with optional attributes, or <see langword="null" /> if the code group does not apply (the membership condition does not match the specified evidence).</returns>
		// Token: 0x060029FD RID: 10749
		public abstract PolicyStatement Resolve(Evidence evidence);

		/// <summary>When overridden in a derived class, resolves matching code groups.</summary>
		/// <param name="evidence">The evidence for the assembly.</param>
		/// <returns>A <see cref="T:System.Security.Policy.CodeGroup" /> that is the root of the tree of matching code groups.</returns>
		// Token: 0x060029FE RID: 10750
		public abstract CodeGroup ResolveMatchingCodeGroups(Evidence evidence);

		/// <summary>When overridden in a derived class, makes a deep copy of the current code group.</summary>
		/// <returns>An equivalent copy of the current code group, including its membership conditions and child code groups.</returns>
		// Token: 0x060029FF RID: 10751
		public abstract CodeGroup Copy();

		/// <summary>Gets the name of the named permission set for the code group.</summary>
		/// <returns>The name of a named permission set of the policy level.</returns>
		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06002A00 RID: 10752 RVA: 0x0009C3A8 File Offset: 0x0009A5A8
		public virtual string PermissionSetName
		{
			get
			{
				if (this.m_policy == null && this.m_element != null)
				{
					this.ParsePolicy();
				}
				if (this.m_policy == null)
				{
					return null;
				}
				NamedPermissionSet namedPermissionSet = this.m_policy.GetPermissionSetNoCopy() as NamedPermissionSet;
				if (namedPermissionSet != null)
				{
					return namedPermissionSet.Name;
				}
				return null;
			}
		}

		/// <summary>Gets a string representation of the attributes of the policy statement for the code group.</summary>
		/// <returns>A string representation of the attributes of the policy statement for the code group.</returns>
		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06002A01 RID: 10753 RVA: 0x0009C3F1 File Offset: 0x0009A5F1
		public virtual string AttributeString
		{
			get
			{
				if (this.m_policy == null && this.m_element != null)
				{
					this.ParsePolicy();
				}
				if (this.m_policy != null)
				{
					return this.m_policy.AttributeString;
				}
				return null;
			}
		}

		/// <summary>When overridden in a derived class, gets the merge logic for the code group.</summary>
		/// <returns>A description of the merge logic for the code group.</returns>
		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06002A02 RID: 10754
		public abstract string MergeLogic { get; }

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x06002A03 RID: 10755 RVA: 0x0009C41E File Offset: 0x0009A61E
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		/// <summary>Reconstructs a security object with a given state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002A04 RID: 10756 RVA: 0x0009C427 File Offset: 0x0009A627
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		/// <summary>Creates an XML encoding of the security object, its current state, and the policy level within which the code exists.</summary>
		/// <param name="level">The policy level within which the code group exists.</param>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x06002A05 RID: 10757 RVA: 0x0009C431 File Offset: 0x0009A631
		[SecuritySafeCritical]
		public SecurityElement ToXml(PolicyLevel level)
		{
			return this.ToXml(level, this.GetTypeName());
		}

		// Token: 0x06002A06 RID: 10758 RVA: 0x0009C440 File Offset: 0x0009A640
		internal virtual string GetTypeName()
		{
			return base.GetType().FullName;
		}

		// Token: 0x06002A07 RID: 10759 RVA: 0x0009C450 File Offset: 0x0009A650
		[SecurityCritical]
		internal SecurityElement ToXml(PolicyLevel level, string policyClassName)
		{
			if (this.m_membershipCondition == null && this.m_element != null)
			{
				this.ParseMembershipCondition();
			}
			if (this.m_children == null)
			{
				this.ParseChildren();
			}
			if (this.m_policy == null && this.m_element != null)
			{
				this.ParsePolicy();
			}
			SecurityElement securityElement = new SecurityElement("CodeGroup");
			XMLUtil.AddClassAttribute(securityElement, base.GetType(), policyClassName);
			securityElement.AddAttribute("version", "1");
			securityElement.AddChild(this.m_membershipCondition.ToXml(level));
			if (this.m_policy != null)
			{
				PermissionSet permissionSetNoCopy = this.m_policy.GetPermissionSetNoCopy();
				NamedPermissionSet namedPermissionSet = permissionSetNoCopy as NamedPermissionSet;
				if (namedPermissionSet != null && level != null && level.GetNamedPermissionSetInternal(namedPermissionSet.Name) != null)
				{
					securityElement.AddAttribute("PermissionSetName", namedPermissionSet.Name);
				}
				else if (!permissionSetNoCopy.IsEmpty())
				{
					securityElement.AddChild(permissionSetNoCopy.ToXml());
				}
				if (this.m_policy.Attributes != PolicyStatementAttribute.Nothing)
				{
					securityElement.AddAttribute("Attributes", XMLUtil.BitFieldEnumToString(typeof(PolicyStatementAttribute), this.m_policy.Attributes));
				}
			}
			if (this.m_children.Count > 0)
			{
				lock (this)
				{
					foreach (object obj in this.m_children)
					{
						securityElement.AddChild(((CodeGroup)obj).ToXml(level));
					}
				}
			}
			if (this.m_name != null)
			{
				securityElement.AddAttribute("Name", SecurityElement.Escape(this.m_name));
			}
			if (this.m_description != null)
			{
				securityElement.AddAttribute("Description", SecurityElement.Escape(this.m_description));
			}
			this.CreateXml(securityElement, level);
			return securityElement;
		}

		/// <summary>When overridden in a derived class, serializes properties and internal state specific to a derived code group and adds the serialization to the specified <see cref="T:System.Security.SecurityElement" />.</summary>
		/// <param name="element">The XML encoding to which to add the serialization.</param>
		/// <param name="level">The policy level within which the code group exists.</param>
		// Token: 0x06002A08 RID: 10760 RVA: 0x0009C610 File Offset: 0x0009A810
		protected virtual void CreateXml(SecurityElement element, PolicyLevel level)
		{
		}

		/// <summary>Reconstructs a security object with a given state and policy level from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object.</param>
		/// <param name="level">The policy level within which the code group exists.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002A09 RID: 10761 RVA: 0x0009C614 File Offset: 0x0009A814
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			lock (this)
			{
				this.m_element = e;
				this.m_parentLevel = level;
				this.m_children = null;
				this.m_membershipCondition = null;
				this.m_policy = null;
				this.m_name = e.Attribute("Name");
				this.m_description = e.Attribute("Description");
				this.ParseXml(e, level);
			}
		}

		/// <summary>When overridden in a derived class, reconstructs properties and internal state specific to a derived code group from the specified <see cref="T:System.Security.SecurityElement" />.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object.</param>
		/// <param name="level">The policy level within which the code group exists.</param>
		// Token: 0x06002A0A RID: 10762 RVA: 0x0009C6A4 File Offset: 0x0009A8A4
		protected virtual void ParseXml(SecurityElement e, PolicyLevel level)
		{
		}

		// Token: 0x06002A0B RID: 10763 RVA: 0x0009C6A8 File Offset: 0x0009A8A8
		[SecurityCritical]
		private bool ParseMembershipCondition(bool safeLoad)
		{
			bool flag2;
			lock (this)
			{
				IMembershipCondition membershipCondition = null;
				SecurityElement securityElement = this.m_element.SearchForChildByTag("IMembershipCondition");
				if (securityElement == null)
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidXMLElement"), "IMembershipCondition", base.GetType().FullName));
				}
				try
				{
					membershipCondition = XMLUtil.CreateMembershipCondition(securityElement);
					if (membershipCondition == null)
					{
						return false;
					}
				}
				catch (Exception ex)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_MembershipConditionElement"), ex);
				}
				membershipCondition.FromXml(securityElement, this.m_parentLevel);
				this.m_membershipCondition = membershipCondition;
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06002A0C RID: 10764 RVA: 0x0009C76C File Offset: 0x0009A96C
		[SecurityCritical]
		private void ParseMembershipCondition()
		{
			this.ParseMembershipCondition(false);
		}

		// Token: 0x06002A0D RID: 10765 RVA: 0x0009C778 File Offset: 0x0009A978
		[SecurityCritical]
		internal void ParseChildren()
		{
			lock (this)
			{
				ArrayList arrayList = ArrayList.Synchronized(new ArrayList());
				if (this.m_element != null && this.m_element.InternalChildren != null)
				{
					this.m_element.Children = (ArrayList)this.m_element.InternalChildren.Clone();
					ArrayList arrayList2 = ArrayList.Synchronized(new ArrayList());
					Evidence evidence = new Evidence();
					int num = this.m_element.InternalChildren.Count;
					int i = 0;
					while (i < num)
					{
						SecurityElement securityElement = (SecurityElement)this.m_element.Children[i];
						if (securityElement.Tag.Equals("CodeGroup"))
						{
							CodeGroup codeGroup = XMLUtil.CreateCodeGroup(securityElement);
							if (codeGroup != null)
							{
								codeGroup.FromXml(securityElement, this.m_parentLevel);
								if (this.ParseMembershipCondition(true))
								{
									codeGroup.Resolve(evidence);
									codeGroup.MembershipCondition.Check(evidence);
									arrayList.Add(codeGroup);
									i++;
								}
								else
								{
									this.m_element.InternalChildren.RemoveAt(i);
									num = this.m_element.InternalChildren.Count;
									arrayList2.Add(new CodeGroupPositionMarker(i, arrayList.Count, securityElement));
								}
							}
							else
							{
								this.m_element.InternalChildren.RemoveAt(i);
								num = this.m_element.InternalChildren.Count;
								arrayList2.Add(new CodeGroupPositionMarker(i, arrayList.Count, securityElement));
							}
						}
						else
						{
							i++;
						}
					}
					foreach (object obj in arrayList2)
					{
						CodeGroupPositionMarker codeGroupPositionMarker = (CodeGroupPositionMarker)obj;
						CodeGroup codeGroup2 = XMLUtil.CreateCodeGroup(codeGroupPositionMarker.element);
						if (codeGroup2 == null)
						{
							throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_FailedCodeGroup"), codeGroupPositionMarker.element.Attribute("class")));
						}
						codeGroup2.FromXml(codeGroupPositionMarker.element, this.m_parentLevel);
						codeGroup2.Resolve(evidence);
						codeGroup2.MembershipCondition.Check(evidence);
						arrayList.Insert(codeGroupPositionMarker.groupIndex, codeGroup2);
						this.m_element.InternalChildren.Insert(codeGroupPositionMarker.elementIndex, codeGroupPositionMarker.element);
					}
				}
				this.m_children = arrayList;
			}
		}

		// Token: 0x06002A0E RID: 10766 RVA: 0x0009C9F8 File Offset: 0x0009ABF8
		private void ParsePolicy()
		{
			for (;;)
			{
				PolicyStatement policyStatement = new PolicyStatement();
				bool flag = false;
				SecurityElement securityElement = new SecurityElement("PolicyStatement");
				securityElement.AddAttribute("version", "1");
				SecurityElement element = this.m_element;
				lock (this)
				{
					if (this.m_element != null)
					{
						string text = this.m_element.Attribute("PermissionSetName");
						if (text != null)
						{
							securityElement.AddAttribute("PermissionSetName", text);
							flag = true;
						}
						else
						{
							SecurityElement securityElement2 = this.m_element.SearchForChildByTag("PermissionSet");
							if (securityElement2 != null)
							{
								securityElement.AddChild(securityElement2);
								flag = true;
							}
							else
							{
								securityElement.AddChild(new PermissionSet(false).ToXml());
								flag = true;
							}
						}
						string text2 = this.m_element.Attribute("Attributes");
						if (text2 != null)
						{
							securityElement.AddAttribute("Attributes", text2);
							flag = true;
						}
					}
				}
				if (flag)
				{
					policyStatement.FromXml(securityElement, this.m_parentLevel);
				}
				else
				{
					policyStatement.PermissionSet = null;
				}
				lock (this)
				{
					if (element == this.m_element && this.m_policy == null)
					{
						this.m_policy = policyStatement;
					}
					else if (this.m_policy == null)
					{
						continue;
					}
				}
				break;
			}
			if (this.m_policy != null && this.m_children != null)
			{
				IMembershipCondition membershipCondition = this.m_membershipCondition;
			}
		}

		/// <summary>Determines whether the specified code group is equivalent to the current code group.</summary>
		/// <param name="o">The code group to compare with the current code group.</param>
		/// <returns>
		///   <see langword="true" /> if the specified code group is equivalent to the current code group; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002A0F RID: 10767 RVA: 0x0009CB68 File Offset: 0x0009AD68
		[SecuritySafeCritical]
		public override bool Equals(object o)
		{
			CodeGroup codeGroup = o as CodeGroup;
			if (codeGroup != null && base.GetType().Equals(codeGroup.GetType()) && object.Equals(this.m_name, codeGroup.m_name) && object.Equals(this.m_description, codeGroup.m_description))
			{
				if (this.m_membershipCondition == null && this.m_element != null)
				{
					this.ParseMembershipCondition();
				}
				if (codeGroup.m_membershipCondition == null && codeGroup.m_element != null)
				{
					codeGroup.ParseMembershipCondition();
				}
				if (object.Equals(this.m_membershipCondition, codeGroup.m_membershipCondition))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Determines whether the specified code group is equivalent to the current code group, checking the child code groups as well, if specified.</summary>
		/// <param name="cg">The code group to compare with the current code group.</param>
		/// <param name="compareChildren">
		///   <see langword="true" /> to compare child code groups, as well; otherwise, <see langword="false" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified code group is equivalent to the current code group; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002A10 RID: 10768 RVA: 0x0009CBFC File Offset: 0x0009ADFC
		[SecuritySafeCritical]
		public bool Equals(CodeGroup cg, bool compareChildren)
		{
			if (!this.Equals(cg))
			{
				return false;
			}
			if (compareChildren)
			{
				if (this.m_children == null)
				{
					this.ParseChildren();
				}
				if (cg.m_children == null)
				{
					cg.ParseChildren();
				}
				ArrayList arrayList = new ArrayList(this.m_children);
				ArrayList arrayList2 = new ArrayList(cg.m_children);
				if (arrayList.Count != arrayList2.Count)
				{
					return false;
				}
				for (int i = 0; i < arrayList.Count; i++)
				{
					if (!((CodeGroup)arrayList[i]).Equals((CodeGroup)arrayList2[i], true))
					{
						return false;
					}
				}
			}
			return true;
		}

		/// <summary>Gets the hash code of the current code group.</summary>
		/// <returns>The hash code of the current code group.</returns>
		// Token: 0x06002A11 RID: 10769 RVA: 0x0009CC90 File Offset: 0x0009AE90
		[SecuritySafeCritical]
		public override int GetHashCode()
		{
			if (this.m_membershipCondition == null && this.m_element != null)
			{
				this.ParseMembershipCondition();
			}
			if (this.m_name != null || this.m_membershipCondition != null)
			{
				return ((this.m_name == null) ? 0 : this.m_name.GetHashCode()) + ((this.m_membershipCondition == null) ? 0 : this.m_membershipCondition.GetHashCode());
			}
			return base.GetType().GetHashCode();
		}

		// Token: 0x0400112F RID: 4399
		private IMembershipCondition m_membershipCondition;

		// Token: 0x04001130 RID: 4400
		private IList m_children;

		// Token: 0x04001131 RID: 4401
		private PolicyStatement m_policy;

		// Token: 0x04001132 RID: 4402
		private SecurityElement m_element;

		// Token: 0x04001133 RID: 4403
		private PolicyLevel m_parentLevel;

		// Token: 0x04001134 RID: 4404
		private string m_name;

		// Token: 0x04001135 RID: 4405
		private string m_description;
	}
}
