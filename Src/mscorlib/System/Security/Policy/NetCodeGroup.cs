using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;

namespace System.Security.Policy
{
	/// <summary>Grants Web permission to the site from which the assembly was downloaded. This class cannot be inherited.</summary>
	// Token: 0x0200035D RID: 861
	[ComVisible(true)]
	[Serializable]
	public sealed class NetCodeGroup : CodeGroup, IUnionSemanticCodeGroup
	{
		// Token: 0x06002AB5 RID: 10933 RVA: 0x0009EDC9 File Offset: 0x0009CFC9
		[SecurityCritical]
		[Conditional("_DEBUG")]
		private static void DEBUG_OUT(string str)
		{
		}

		// Token: 0x06002AB6 RID: 10934 RVA: 0x0009EDCB File Offset: 0x0009CFCB
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
			this.m_schemesList = null;
			this.m_accessList = null;
		}

		// Token: 0x06002AB7 RID: 10935 RVA: 0x0009EDDB File Offset: 0x0009CFDB
		internal NetCodeGroup()
		{
			this.SetDefaults();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.NetCodeGroup" /> class.</summary>
		/// <param name="membershipCondition">A membership condition that tests evidence to determine whether this code group applies code access security policy.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="membershipCondition" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The type of the <paramref name="membershipCondition" /> parameter is not valid.</exception>
		// Token: 0x06002AB8 RID: 10936 RVA: 0x0009EDE9 File Offset: 0x0009CFE9
		public NetCodeGroup(IMembershipCondition membershipCondition)
			: base(membershipCondition, null)
		{
			this.SetDefaults();
		}

		/// <summary>Removes all connection access information for the current code group.</summary>
		// Token: 0x06002AB9 RID: 10937 RVA: 0x0009EDF9 File Offset: 0x0009CFF9
		public void ResetConnectAccess()
		{
			this.m_schemesList = null;
			this.m_accessList = null;
		}

		/// <summary>Adds the specified connection access to the current code group.</summary>
		/// <param name="originScheme">A <see cref="T:System.String" /> containing the scheme to match against the code's scheme.</param>
		/// <param name="connectAccess">A <see cref="T:System.Security.Policy.CodeConnectAccess" /> that specifies the scheme and port code can use to connect back to its origin server.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="originScheme" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="originScheme" /> contains characters that are not permitted in schemes.  
		/// -or-  
		/// <paramref name="originScheme" /> = <see cref="F:System.Security.Policy.NetCodeGroup.AbsentOriginScheme" /> and <paramref name="connectAccess" /> specifies <see cref="F:System.Security.Policy.CodeConnectAccess.OriginScheme" /> as its scheme.</exception>
		// Token: 0x06002ABA RID: 10938 RVA: 0x0009EE0C File Offset: 0x0009D00C
		public void AddConnectAccess(string originScheme, CodeConnectAccess connectAccess)
		{
			if (originScheme == null)
			{
				throw new ArgumentNullException("originScheme");
			}
			if (originScheme != NetCodeGroup.AbsentOriginScheme && originScheme != NetCodeGroup.AnyOtherOriginScheme && !CodeConnectAccess.IsValidScheme(originScheme))
			{
				throw new ArgumentOutOfRangeException("originScheme");
			}
			if (originScheme == NetCodeGroup.AbsentOriginScheme && connectAccess.IsOriginScheme)
			{
				throw new ArgumentOutOfRangeException("connectAccess");
			}
			if (this.m_schemesList == null)
			{
				this.m_schemesList = new ArrayList();
				this.m_accessList = new ArrayList();
			}
			originScheme = originScheme.ToLower(CultureInfo.InvariantCulture);
			int i = 0;
			while (i < this.m_schemesList.Count)
			{
				if ((string)this.m_schemesList[i] == originScheme)
				{
					if (connectAccess == null)
					{
						return;
					}
					ArrayList arrayList = (ArrayList)this.m_accessList[i];
					for (i = 0; i < arrayList.Count; i++)
					{
						if (((CodeConnectAccess)arrayList[i]).Equals(connectAccess))
						{
							return;
						}
					}
					arrayList.Add(connectAccess);
					return;
				}
				else
				{
					i++;
				}
			}
			this.m_schemesList.Add(originScheme);
			ArrayList arrayList2 = new ArrayList();
			this.m_accessList.Add(arrayList2);
			if (connectAccess != null)
			{
				arrayList2.Add(connectAccess);
			}
		}

		/// <summary>Gets the connection access information for the current code group.</summary>
		/// <returns>A <see cref="T:System.Collections.DictionaryEntry" /> array containing connection access information.</returns>
		// Token: 0x06002ABB RID: 10939 RVA: 0x0009EF40 File Offset: 0x0009D140
		public DictionaryEntry[] GetConnectAccessRules()
		{
			if (this.m_schemesList == null)
			{
				return null;
			}
			DictionaryEntry[] array = new DictionaryEntry[this.m_schemesList.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Key = this.m_schemesList[i];
				array[i].Value = ((ArrayList)this.m_accessList[i]).ToArray(typeof(CodeConnectAccess));
			}
			return array;
		}

		/// <summary>Resolves policy for the code group and its descendants for a set of evidence.</summary>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> for the assembly.</param>
		/// <returns>A <see cref="T:System.Security.Policy.PolicyStatement" /> that consists of the permissions granted by the code group with optional attributes, or <see langword="null" /> if the code group does not apply (the membership condition does not match the specified evidence).</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="evidence" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Policy.PolicyException">More than one code group (including the parent code group and any child code groups) is marked <see cref="F:System.Security.Policy.PolicyStatementAttribute.Exclusive" />.</exception>
		// Token: 0x06002ABC RID: 10940 RVA: 0x0009EFBC File Offset: 0x0009D1BC
		[SecuritySafeCritical]
		public override PolicyStatement Resolve(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			object obj = null;
			if (PolicyManager.CheckMembershipCondition(base.MembershipCondition, evidence, out obj))
			{
				PolicyStatement policyStatement = this.CalculateAssemblyPolicy(evidence);
				IDelayEvaluatedEvidence delayEvaluatedEvidence = obj as IDelayEvaluatedEvidence;
				bool flag = delayEvaluatedEvidence != null && !delayEvaluatedEvidence.IsVerified;
				if (flag)
				{
					policyStatement.AddDependentEvidence(delayEvaluatedEvidence);
				}
				bool flag2 = false;
				IEnumerator enumerator = base.Children.GetEnumerator();
				while (enumerator.MoveNext() && !flag2)
				{
					PolicyStatement policyStatement2 = PolicyManager.ResolveCodeGroup(enumerator.Current as CodeGroup, evidence);
					if (policyStatement2 != null)
					{
						policyStatement.InplaceUnion(policyStatement2);
						if ((policyStatement2.Attributes & PolicyStatementAttribute.Exclusive) == PolicyStatementAttribute.Exclusive)
						{
							flag2 = true;
						}
					}
				}
				return policyStatement;
			}
			return null;
		}

		// Token: 0x06002ABD RID: 10941 RVA: 0x0009F063 File Offset: 0x0009D263
		PolicyStatement IUnionSemanticCodeGroup.InternalResolve(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			if (base.MembershipCondition.Check(evidence))
			{
				return this.CalculateAssemblyPolicy(evidence);
			}
			return null;
		}

		/// <summary>Resolves matching code groups.</summary>
		/// <param name="evidence">The evidence for the assembly.</param>
		/// <returns>The complete set of code groups that were matched by the evidence.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="evidence" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002ABE RID: 10942 RVA: 0x0009F08C File Offset: 0x0009D28C
		public override CodeGroup ResolveMatchingCodeGroups(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			if (base.MembershipCondition.Check(evidence))
			{
				CodeGroup codeGroup = this.Copy();
				codeGroup.Children = new ArrayList();
				foreach (object obj in base.Children)
				{
					CodeGroup codeGroup2 = ((CodeGroup)obj).ResolveMatchingCodeGroups(evidence);
					if (codeGroup2 != null)
					{
						codeGroup.AddChild(codeGroup2);
					}
				}
				return codeGroup;
			}
			return null;
		}

		// Token: 0x06002ABF RID: 10943 RVA: 0x0009F0FC File Offset: 0x0009D2FC
		private string EscapeStringForRegex(string str)
		{
			int num = 0;
			StringBuilder stringBuilder = null;
			int num2;
			while (num < str.Length && (num2 = str.IndexOfAny(NetCodeGroup.c_SomeRegexChars, num)) != -1)
			{
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder(str.Length * 2);
				}
				stringBuilder.Append(str, num, num2 - num).Append('\\').Append(str[num2]);
				num = num2 + 1;
			}
			if (stringBuilder == null)
			{
				return str;
			}
			if (num < str.Length)
			{
				stringBuilder.Append(str, num, str.Length - num);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002AC0 RID: 10944 RVA: 0x0009F184 File Offset: 0x0009D384
		internal SecurityElement CreateWebPermission(string host, string scheme, string port, string assemblyOverride)
		{
			if (scheme == null)
			{
				scheme = string.Empty;
			}
			if (host == null || host.Length == 0)
			{
				return null;
			}
			host = host.ToLower(CultureInfo.InvariantCulture);
			scheme = scheme.ToLower(CultureInfo.InvariantCulture);
			int num = -1;
			if (port != null && port.Length != 0)
			{
				num = int.Parse(port, CultureInfo.InvariantCulture);
			}
			else
			{
				port = string.Empty;
			}
			CodeConnectAccess[] array = this.FindAccessRulesForScheme(scheme);
			if (array == null || array.Length == 0)
			{
				return null;
			}
			SecurityElement securityElement = new SecurityElement("IPermission");
			string text = ((assemblyOverride == null) ? "System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" : assemblyOverride);
			securityElement.AddAttribute("class", "System.Net.WebPermission, " + text);
			securityElement.AddAttribute("version", "1");
			SecurityElement securityElement2 = new SecurityElement("ConnectAccess");
			host = this.EscapeStringForRegex(host);
			scheme = this.EscapeStringForRegex(scheme);
			string text2 = this.TryPermissionAsOneString(array, scheme, host, num);
			if (text2 != null)
			{
				SecurityElement securityElement3 = new SecurityElement("URI");
				securityElement3.AddAttribute("uri", text2);
				securityElement2.AddChild(securityElement3);
			}
			else
			{
				if (port.Length != 0)
				{
					port = ":" + port;
				}
				for (int i = 0; i < array.Length; i++)
				{
					text2 = this.GetPermissionAccessElementString(array[i], scheme, host, port);
					SecurityElement securityElement4 = new SecurityElement("URI");
					securityElement4.AddAttribute("uri", text2);
					securityElement2.AddChild(securityElement4);
				}
			}
			securityElement.AddChild(securityElement2);
			return securityElement;
		}

		// Token: 0x06002AC1 RID: 10945 RVA: 0x0009F2EC File Offset: 0x0009D4EC
		private CodeConnectAccess[] FindAccessRulesForScheme(string lowerCaseScheme)
		{
			if (this.m_schemesList == null)
			{
				return null;
			}
			int num = this.m_schemesList.IndexOf(lowerCaseScheme);
			if (num == -1 && (lowerCaseScheme == NetCodeGroup.AbsentOriginScheme || (num = this.m_schemesList.IndexOf(NetCodeGroup.AnyOtherOriginScheme)) == -1))
			{
				return null;
			}
			ArrayList arrayList = (ArrayList)this.m_accessList[num];
			return (CodeConnectAccess[])arrayList.ToArray(typeof(CodeConnectAccess));
		}

		// Token: 0x06002AC2 RID: 10946 RVA: 0x0009F360 File Offset: 0x0009D560
		private string TryPermissionAsOneString(CodeConnectAccess[] access, string escapedScheme, string escapedHost, int intPort)
		{
			bool flag = true;
			bool flag2 = true;
			bool flag3 = false;
			int num = -2;
			for (int i = 0; i < access.Length; i++)
			{
				flag &= access[i].IsDefaultPort || (access[i].IsOriginPort && intPort == -1);
				flag2 &= access[i].IsOriginPort || access[i].Port == intPort;
				if (access[i].Port >= 0)
				{
					if (num == -2)
					{
						num = access[i].Port;
					}
					else if (access[i].Port != num)
					{
						num = -1;
					}
				}
				else
				{
					num = -1;
				}
				if (access[i].IsAnyScheme)
				{
					flag3 = true;
				}
			}
			if (!flag && !flag2 && num == -1)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder("([0-9a-z+\\-\\.]+)://".Length * access.Length + "".Length * 2 + escapedHost.Length);
			if (flag3)
			{
				stringBuilder.Append("([0-9a-z+\\-\\.]+)://");
			}
			else
			{
				stringBuilder.Append('(');
				for (int j = 0; j < access.Length; j++)
				{
					int num2 = 0;
					while (num2 < j && !(access[j].Scheme == access[num2].Scheme))
					{
						num2++;
					}
					if (num2 == j)
					{
						if (j != 0)
						{
							stringBuilder.Append('|');
						}
						stringBuilder.Append(access[j].IsOriginScheme ? escapedScheme : this.EscapeStringForRegex(access[j].Scheme));
					}
				}
				stringBuilder.Append(")://");
			}
			stringBuilder.Append("").Append(escapedHost);
			if (!flag)
			{
				if (flag2)
				{
					stringBuilder.Append(':').Append(intPort);
				}
				else
				{
					stringBuilder.Append(':').Append(num);
				}
			}
			stringBuilder.Append("/.*");
			return stringBuilder.ToString();
		}

		// Token: 0x06002AC3 RID: 10947 RVA: 0x0009F534 File Offset: 0x0009D734
		private string GetPermissionAccessElementString(CodeConnectAccess access, string escapedScheme, string escapedHost, string strPort)
		{
			StringBuilder stringBuilder = new StringBuilder("([0-9a-z+\\-\\.]+)://".Length * 2 + "".Length + escapedHost.Length);
			if (access.IsAnyScheme)
			{
				stringBuilder.Append("([0-9a-z+\\-\\.]+)://");
			}
			else if (access.IsOriginScheme)
			{
				stringBuilder.Append(escapedScheme).Append("://");
			}
			else
			{
				stringBuilder.Append(this.EscapeStringForRegex(access.Scheme)).Append("://");
			}
			stringBuilder.Append("").Append(escapedHost);
			if (!access.IsDefaultPort)
			{
				if (access.IsOriginPort)
				{
					stringBuilder.Append(strPort);
				}
				else
				{
					stringBuilder.Append(':').Append(access.StrPort);
				}
			}
			stringBuilder.Append("/.*");
			return stringBuilder.ToString();
		}

		// Token: 0x06002AC4 RID: 10948 RVA: 0x0009F608 File Offset: 0x0009D808
		internal PolicyStatement CalculatePolicy(string host, string scheme, string port)
		{
			SecurityElement securityElement = this.CreateWebPermission(host, scheme, port, null);
			SecurityElement securityElement2 = new SecurityElement("PolicyStatement");
			SecurityElement securityElement3 = new SecurityElement("PermissionSet");
			securityElement3.AddAttribute("class", "System.Security.PermissionSet");
			securityElement3.AddAttribute("version", "1");
			if (securityElement != null)
			{
				securityElement3.AddChild(securityElement);
			}
			securityElement2.AddChild(securityElement3);
			PolicyStatement policyStatement = new PolicyStatement();
			policyStatement.FromXml(securityElement2);
			return policyStatement;
		}

		// Token: 0x06002AC5 RID: 10949 RVA: 0x0009F678 File Offset: 0x0009D878
		private PolicyStatement CalculateAssemblyPolicy(Evidence evidence)
		{
			PolicyStatement policyStatement = null;
			Url hostEvidence = evidence.GetHostEvidence<Url>();
			if (hostEvidence != null)
			{
				policyStatement = this.CalculatePolicy(hostEvidence.GetURLString().Host, hostEvidence.GetURLString().Scheme, hostEvidence.GetURLString().Port);
			}
			if (policyStatement == null)
			{
				Site hostEvidence2 = evidence.GetHostEvidence<Site>();
				if (hostEvidence2 != null)
				{
					policyStatement = this.CalculatePolicy(hostEvidence2.Name, null, null);
				}
			}
			if (policyStatement == null)
			{
				policyStatement = new PolicyStatement(new PermissionSet(false), PolicyStatementAttribute.Nothing);
			}
			return policyStatement;
		}

		/// <summary>Makes a deep copy of the current code group.</summary>
		/// <returns>An equivalent copy of the current code group, including its membership conditions and child code groups.</returns>
		// Token: 0x06002AC6 RID: 10950 RVA: 0x0009F6E8 File Offset: 0x0009D8E8
		public override CodeGroup Copy()
		{
			NetCodeGroup netCodeGroup = new NetCodeGroup(base.MembershipCondition);
			netCodeGroup.Name = base.Name;
			netCodeGroup.Description = base.Description;
			if (this.m_schemesList != null)
			{
				netCodeGroup.m_schemesList = (ArrayList)this.m_schemesList.Clone();
				netCodeGroup.m_accessList = new ArrayList(this.m_accessList.Count);
				for (int i = 0; i < this.m_accessList.Count; i++)
				{
					netCodeGroup.m_accessList.Add(((ArrayList)this.m_accessList[i]).Clone());
				}
			}
			foreach (object obj in base.Children)
			{
				netCodeGroup.AddChild((CodeGroup)obj);
			}
			return netCodeGroup;
		}

		/// <summary>Gets the logic to use for merging groups.</summary>
		/// <returns>The string "Union".</returns>
		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06002AC7 RID: 10951 RVA: 0x0009F7AD File Offset: 0x0009D9AD
		public override string MergeLogic
		{
			get
			{
				return Environment.GetResourceString("MergeLogic_Union");
			}
		}

		/// <summary>Gets the name of the <see cref="T:System.Security.NamedPermissionSet" /> for the code group.</summary>
		/// <returns>Always the string "Same site Web."</returns>
		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06002AC8 RID: 10952 RVA: 0x0009F7B9 File Offset: 0x0009D9B9
		public override string PermissionSetName
		{
			get
			{
				return Environment.GetResourceString("NetCodeGroup_PermissionSet");
			}
		}

		/// <summary>Gets a string representation of the attributes of the policy statement for the code group.</summary>
		/// <returns>Always <see langword="null" />.</returns>
		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06002AC9 RID: 10953 RVA: 0x0009F7C5 File Offset: 0x0009D9C5
		public override string AttributeString
		{
			get
			{
				return null;
			}
		}

		/// <summary>Determines whether the specified code group is equivalent to the current code group.</summary>
		/// <param name="o">The <see cref="T:System.Security.Policy.NetCodeGroup" /> object to compare with the current code group.</param>
		/// <returns>
		///   <see langword="true" /> if the specified code group is equivalent to the current code group; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002ACA RID: 10954 RVA: 0x0009F7C8 File Offset: 0x0009D9C8
		public override bool Equals(object o)
		{
			if (this == o)
			{
				return true;
			}
			NetCodeGroup netCodeGroup = o as NetCodeGroup;
			if (netCodeGroup == null || !base.Equals(netCodeGroup))
			{
				return false;
			}
			if (this.m_schemesList == null != (netCodeGroup.m_schemesList == null))
			{
				return false;
			}
			if (this.m_schemesList == null)
			{
				return true;
			}
			if (this.m_schemesList.Count != netCodeGroup.m_schemesList.Count)
			{
				return false;
			}
			for (int i = 0; i < this.m_schemesList.Count; i++)
			{
				int num = netCodeGroup.m_schemesList.IndexOf(this.m_schemesList[i]);
				if (num == -1)
				{
					return false;
				}
				ArrayList arrayList = (ArrayList)this.m_accessList[i];
				ArrayList arrayList2 = (ArrayList)netCodeGroup.m_accessList[num];
				if (arrayList.Count != arrayList2.Count)
				{
					return false;
				}
				for (int j = 0; j < arrayList.Count; j++)
				{
					if (!arrayList2.Contains(arrayList[j]))
					{
						return false;
					}
				}
			}
			return true;
		}

		/// <summary>Gets the hash code of the current code group.</summary>
		/// <returns>The hash code of the current code group.</returns>
		// Token: 0x06002ACB RID: 10955 RVA: 0x0009F8C3 File Offset: 0x0009DAC3
		public override int GetHashCode()
		{
			return base.GetHashCode() + this.GetRulesHashCode();
		}

		// Token: 0x06002ACC RID: 10956 RVA: 0x0009F8D4 File Offset: 0x0009DAD4
		private int GetRulesHashCode()
		{
			if (this.m_schemesList == null)
			{
				return 0;
			}
			int num = 0;
			for (int i = 0; i < this.m_schemesList.Count; i++)
			{
				num += ((string)this.m_schemesList[i]).GetHashCode();
			}
			foreach (object obj in this.m_accessList)
			{
				ArrayList arrayList = (ArrayList)obj;
				for (int j = 0; j < arrayList.Count; j++)
				{
					num += ((CodeConnectAccess)arrayList[j]).GetHashCode();
				}
			}
			return num;
		}

		// Token: 0x06002ACD RID: 10957 RVA: 0x0009F990 File Offset: 0x0009DB90
		protected override void CreateXml(SecurityElement element, PolicyLevel level)
		{
			DictionaryEntry[] connectAccessRules = this.GetConnectAccessRules();
			if (connectAccessRules == null)
			{
				return;
			}
			SecurityElement securityElement = new SecurityElement("connectAccessRules");
			foreach (DictionaryEntry dictionaryEntry in connectAccessRules)
			{
				SecurityElement securityElement2 = new SecurityElement("codeOrigin");
				securityElement2.AddAttribute("scheme", (string)dictionaryEntry.Key);
				foreach (CodeConnectAccess codeConnectAccess in (CodeConnectAccess[])dictionaryEntry.Value)
				{
					SecurityElement securityElement3 = new SecurityElement("connectAccess");
					securityElement3.AddAttribute("scheme", codeConnectAccess.Scheme);
					securityElement3.AddAttribute("port", codeConnectAccess.StrPort);
					securityElement2.AddChild(securityElement3);
				}
				securityElement.AddChild(securityElement2);
			}
			element.AddChild(securityElement);
		}

		// Token: 0x06002ACE RID: 10958 RVA: 0x0009FA68 File Offset: 0x0009DC68
		protected override void ParseXml(SecurityElement e, PolicyLevel level)
		{
			this.ResetConnectAccess();
			SecurityElement securityElement = e.SearchForChildByTag("connectAccessRules");
			if (securityElement == null || securityElement.Children == null)
			{
				this.SetDefaults();
				return;
			}
			foreach (object obj in securityElement.Children)
			{
				SecurityElement securityElement2 = (SecurityElement)obj;
				if (securityElement2.Tag.Equals("codeOrigin"))
				{
					string text = securityElement2.Attribute("scheme");
					bool flag = false;
					if (securityElement2.Children != null)
					{
						foreach (object obj2 in securityElement2.Children)
						{
							SecurityElement securityElement3 = (SecurityElement)obj2;
							if (securityElement3.Tag.Equals("connectAccess"))
							{
								string text2 = securityElement3.Attribute("scheme");
								string text3 = securityElement3.Attribute("port");
								this.AddConnectAccess(text, new CodeConnectAccess(text2, text3));
								flag = true;
							}
						}
					}
					if (!flag)
					{
						this.AddConnectAccess(text, null);
					}
				}
			}
		}

		// Token: 0x06002ACF RID: 10959 RVA: 0x0009FBAC File Offset: 0x0009DDAC
		internal override string GetTypeName()
		{
			return "System.Security.Policy.NetCodeGroup";
		}

		// Token: 0x06002AD0 RID: 10960 RVA: 0x0009FBB4 File Offset: 0x0009DDB4
		private void SetDefaults()
		{
			this.AddConnectAccess("file", null);
			this.AddConnectAccess("http", new CodeConnectAccess("http", CodeConnectAccess.OriginPort));
			this.AddConnectAccess("http", new CodeConnectAccess("https", CodeConnectAccess.OriginPort));
			this.AddConnectAccess("https", new CodeConnectAccess("https", CodeConnectAccess.OriginPort));
			this.AddConnectAccess(NetCodeGroup.AbsentOriginScheme, CodeConnectAccess.CreateAnySchemeAccess(CodeConnectAccess.OriginPort));
			this.AddConnectAccess(NetCodeGroup.AnyOtherOriginScheme, CodeConnectAccess.CreateOriginSchemeAccess(CodeConnectAccess.OriginPort));
		}

		// Token: 0x04001160 RID: 4448
		[OptionalField(VersionAdded = 2)]
		private ArrayList m_schemesList;

		// Token: 0x04001161 RID: 4449
		[OptionalField(VersionAdded = 2)]
		private ArrayList m_accessList;

		// Token: 0x04001162 RID: 4450
		private const string c_IgnoreUserInfo = "";

		// Token: 0x04001163 RID: 4451
		private const string c_AnyScheme = "([0-9a-z+\\-\\.]+)://";

		// Token: 0x04001164 RID: 4452
		private static readonly char[] c_SomeRegexChars = new char[]
		{
			'.', '-', '+', '[', ']', '{', '$', '^', '#', ')',
			'(', ' '
		};

		/// <summary>Contains a value used to specify any other unspecified origin scheme.</summary>
		// Token: 0x04001165 RID: 4453
		public static readonly string AnyOtherOriginScheme = CodeConnectAccess.AnyScheme;

		/// <summary>Contains a value used to specify connection access for code with an unknown or unrecognized origin scheme.</summary>
		// Token: 0x04001166 RID: 4454
		public static readonly string AbsentOriginScheme = string.Empty;
	}
}
