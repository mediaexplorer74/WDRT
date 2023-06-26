using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Security.Util;
using System.Text;
using System.Threading;

namespace System.Security.Policy
{
	/// <summary>Represents the security policy levels for the common language runtime. This class cannot be inherited.</summary>
	// Token: 0x02000363 RID: 867
	[ComVisible(true)]
	[Serializable]
	public sealed class PolicyLevel
	{
		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06002AF1 RID: 10993 RVA: 0x000A01DC File Offset: 0x0009E3DC
		private static object InternalSyncObject
		{
			get
			{
				if (PolicyLevel.s_InternalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange(ref PolicyLevel.s_InternalSyncObject, obj, null);
				}
				return PolicyLevel.s_InternalSyncObject;
			}
		}

		// Token: 0x06002AF2 RID: 10994 RVA: 0x000A0208 File Offset: 0x0009E408
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			if (this.m_label != null)
			{
				this.DeriveTypeFromLabel();
			}
		}

		// Token: 0x06002AF3 RID: 10995 RVA: 0x000A0218 File Offset: 0x0009E418
		private void DeriveTypeFromLabel()
		{
			if (this.m_label.Equals(Environment.GetResourceString("Policy_PL_User")))
			{
				this.m_type = PolicyLevelType.User;
				return;
			}
			if (this.m_label.Equals(Environment.GetResourceString("Policy_PL_Machine")))
			{
				this.m_type = PolicyLevelType.Machine;
				return;
			}
			if (this.m_label.Equals(Environment.GetResourceString("Policy_PL_Enterprise")))
			{
				this.m_type = PolicyLevelType.Enterprise;
				return;
			}
			if (this.m_label.Equals(Environment.GetResourceString("Policy_PL_AppDomain")))
			{
				this.m_type = PolicyLevelType.AppDomain;
				return;
			}
			throw new ArgumentException(Environment.GetResourceString("Policy_Default"));
		}

		// Token: 0x06002AF4 RID: 10996 RVA: 0x000A02B0 File Offset: 0x0009E4B0
		private string DeriveLabelFromType()
		{
			switch (this.m_type)
			{
			case PolicyLevelType.User:
				return Environment.GetResourceString("Policy_PL_User");
			case PolicyLevelType.Machine:
				return Environment.GetResourceString("Policy_PL_Machine");
			case PolicyLevelType.Enterprise:
				return Environment.GetResourceString("Policy_PL_Enterprise");
			case PolicyLevelType.AppDomain:
				return Environment.GetResourceString("Policy_PL_AppDomain");
			default:
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[] { (int)this.m_type }));
			}
		}

		// Token: 0x06002AF5 RID: 10997 RVA: 0x000A032B File Offset: 0x0009E52B
		private PolicyLevel()
		{
		}

		// Token: 0x06002AF6 RID: 10998 RVA: 0x000A0333 File Offset: 0x0009E533
		[SecurityCritical]
		internal PolicyLevel(PolicyLevelType type)
			: this(type, PolicyLevel.GetLocationFromType(type))
		{
		}

		// Token: 0x06002AF7 RID: 10999 RVA: 0x000A0342 File Offset: 0x0009E542
		internal PolicyLevel(PolicyLevelType type, string path)
			: this(type, path, ConfigId.None)
		{
		}

		// Token: 0x06002AF8 RID: 11000 RVA: 0x000A0350 File Offset: 0x0009E550
		internal PolicyLevel(PolicyLevelType type, string path, ConfigId configId)
		{
			this.m_type = type;
			this.m_path = path;
			this.m_loaded = path == null;
			if (this.m_path == null)
			{
				this.m_rootCodeGroup = this.CreateDefaultAllGroup();
				this.SetFactoryPermissionSets();
				this.SetDefaultFullTrustAssemblies();
			}
			this.m_configId = configId;
		}

		// Token: 0x06002AF9 RID: 11001 RVA: 0x000A03A4 File Offset: 0x0009E5A4
		[SecurityCritical]
		internal static string GetLocationFromType(PolicyLevelType type)
		{
			switch (type)
			{
			case PolicyLevelType.User:
				return Config.UserDirectory + "security.config";
			case PolicyLevelType.Machine:
				return Config.MachineDirectory + "security.config";
			case PolicyLevelType.Enterprise:
				return Config.MachineDirectory + "enterprisesec.config";
			default:
				return null;
			}
		}

		/// <summary>Creates a new policy level for use at the application domain policy level.</summary>
		/// <returns>The newly created <see cref="T:System.Security.Policy.PolicyLevel" />.</returns>
		// Token: 0x06002AFA RID: 11002 RVA: 0x000A03F6 File Offset: 0x0009E5F6
		[SecuritySafeCritical]
		[Obsolete("AppDomain policy levels are obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		public static PolicyLevel CreateAppDomainLevel()
		{
			return new PolicyLevel(PolicyLevelType.AppDomain);
		}

		/// <summary>Gets a descriptive label for the policy level.</summary>
		/// <returns>The label associated with the policy level.</returns>
		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06002AFB RID: 11003 RVA: 0x000A03FE File Offset: 0x0009E5FE
		public string Label
		{
			get
			{
				if (this.m_label == null)
				{
					this.m_label = this.DeriveLabelFromType();
				}
				return this.m_label;
			}
		}

		/// <summary>Gets the type of the policy level.</summary>
		/// <returns>One of the <see cref="T:System.Security.PolicyLevelType" /> values.</returns>
		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06002AFC RID: 11004 RVA: 0x000A041A File Offset: 0x0009E61A
		[ComVisible(false)]
		public PolicyLevelType Type
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06002AFD RID: 11005 RVA: 0x000A0422 File Offset: 0x0009E622
		internal ConfigId ConfigId
		{
			get
			{
				return this.m_configId;
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06002AFE RID: 11006 RVA: 0x000A042A File Offset: 0x0009E62A
		internal string Path
		{
			get
			{
				return this.m_path;
			}
		}

		/// <summary>Gets the path where the policy file is stored.</summary>
		/// <returns>The path where the policy file is stored, or <see langword="null" /> if the <see cref="T:System.Security.Policy.PolicyLevel" /> does not have a storage location.</returns>
		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06002AFF RID: 11007 RVA: 0x000A0432 File Offset: 0x0009E632
		public string StoreLocation
		{
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPolicy)]
			get
			{
				return PolicyLevel.GetLocationFromType(this.m_type);
			}
		}

		/// <summary>Gets or sets the root code group for the policy level.</summary>
		/// <returns>The <see cref="T:System.Security.Policy.CodeGroup" /> that is the root of the tree of policy level code groups.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value for <see cref="P:System.Security.Policy.PolicyLevel.RootCodeGroup" /> is <see langword="null" />.</exception>
		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06002B00 RID: 11008 RVA: 0x000A043F File Offset: 0x0009E63F
		// (set) Token: 0x06002B01 RID: 11009 RVA: 0x000A044D File Offset: 0x0009E64D
		public CodeGroup RootCodeGroup
		{
			[SecuritySafeCritical]
			get
			{
				this.CheckLoaded();
				return this.m_rootCodeGroup;
			}
			[SecuritySafeCritical]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("RootCodeGroup");
				}
				this.CheckLoaded();
				this.m_rootCodeGroup = value.Copy();
			}
		}

		/// <summary>Gets a list of named permission sets defined for the policy level.</summary>
		/// <returns>A list of named permission sets defined for the policy level.</returns>
		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06002B02 RID: 11010 RVA: 0x000A0470 File Offset: 0x0009E670
		public IList NamedPermissionSets
		{
			[SecuritySafeCritical]
			get
			{
				this.CheckLoaded();
				this.LoadAllPermissionSets();
				ArrayList arrayList = new ArrayList(this.m_namedPermissionSets.Count);
				foreach (object obj in this.m_namedPermissionSets)
				{
					arrayList.Add(((NamedPermissionSet)obj).Copy());
				}
				return arrayList;
			}
		}

		/// <summary>Resolves policy at the policy level and returns the root of a code group tree that matches the evidence.</summary>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> used to resolve policy.</param>
		/// <returns>A <see cref="T:System.Security.Policy.CodeGroup" /> representing the root of a tree of code groups matching the specified evidence.</returns>
		/// <exception cref="T:System.Security.Policy.PolicyException">The policy level contains multiple matching code groups marked as exclusive.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="evidence" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002B03 RID: 11011 RVA: 0x000A04C8 File Offset: 0x0009E6C8
		public CodeGroup ResolveMatchingCodeGroups(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			return this.RootCodeGroup.ResolveMatchingCodeGroups(evidence);
		}

		/// <summary>Adds a <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> corresponding to the specified <see cref="T:System.Security.Policy.StrongName" /> to the list of <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> objects used to determine whether an assembly is a member of the group of assemblies that should not be evaluated.</summary>
		/// <param name="sn">The <see cref="T:System.Security.Policy.StrongName" /> used to create the <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> to add to the list of <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> objects used to determine whether an assembly is a member of the group of assemblies that should not be evaluated.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="sn" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Security.Policy.StrongName" /> specified by the <paramref name="sn" /> parameter already has full trust.</exception>
		// Token: 0x06002B04 RID: 11012 RVA: 0x000A04E4 File Offset: 0x0009E6E4
		[Obsolete("Because all GAC assemblies always get full trust, the full trust list is no longer meaningful. You should install any assemblies that are used in security policy in the GAC to ensure they are trusted.")]
		public void AddFullTrustAssembly(StrongName sn)
		{
			if (sn == null)
			{
				throw new ArgumentNullException("sn");
			}
			this.AddFullTrustAssembly(new StrongNameMembershipCondition(sn.PublicKey, sn.Name, sn.Version));
		}

		/// <summary>Adds the specified <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> to the list of <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> objects used to determine whether an assembly is a member of the group of assemblies that should not be evaluated.</summary>
		/// <param name="snMC">The <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> to add to the list of <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> objects used to determine whether an assembly is a member of the group of assemblies that should not be evaluated.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="snMC" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> specified by the <paramref name="snMC" /> parameter already has full trust.</exception>
		// Token: 0x06002B05 RID: 11013 RVA: 0x000A0514 File Offset: 0x0009E714
		[SecuritySafeCritical]
		[Obsolete("Because all GAC assemblies always get full trust, the full trust list is no longer meaningful. You should install any assemblies that are used in security policy in the GAC to ensure they are trusted.")]
		public void AddFullTrustAssembly(StrongNameMembershipCondition snMC)
		{
			if (snMC == null)
			{
				throw new ArgumentNullException("snMC");
			}
			this.CheckLoaded();
			IEnumerator enumerator = this.m_fullTrustAssemblies.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (((StrongNameMembershipCondition)enumerator.Current).Equals(snMC))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_AssemblyAlreadyFullTrust"));
				}
			}
			ArrayList fullTrustAssemblies = this.m_fullTrustAssemblies;
			lock (fullTrustAssemblies)
			{
				this.m_fullTrustAssemblies.Add(snMC);
			}
		}

		/// <summary>Removes an assembly with the specified <see cref="T:System.Security.Policy.StrongName" /> from the list of assemblies the policy level uses to evaluate policy.</summary>
		/// <param name="sn">The <see cref="T:System.Security.Policy.StrongName" /> of the assembly to remove from the list of assemblies used to evaluate policy.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="sn" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The assembly with the <see cref="T:System.Security.Policy.StrongName" /> specified by the <paramref name="sn" /> parameter does not have full trust.</exception>
		// Token: 0x06002B06 RID: 11014 RVA: 0x000A05A8 File Offset: 0x0009E7A8
		[Obsolete("Because all GAC assemblies always get full trust, the full trust list is no longer meaningful. You should install any assemblies that are used in security policy in the GAC to ensure they are trusted.")]
		public void RemoveFullTrustAssembly(StrongName sn)
		{
			if (sn == null)
			{
				throw new ArgumentNullException("assembly");
			}
			this.RemoveFullTrustAssembly(new StrongNameMembershipCondition(sn.PublicKey, sn.Name, sn.Version));
		}

		/// <summary>Removes an assembly with the specified <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> from the list of assemblies the policy level uses to evaluate policy.</summary>
		/// <param name="snMC">The <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> of the assembly to remove from the list of assemblies used to evaluate policy.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="snMC" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> specified by the <paramref name="snMC" /> parameter does not have full trust.</exception>
		// Token: 0x06002B07 RID: 11015 RVA: 0x000A05D8 File Offset: 0x0009E7D8
		[SecuritySafeCritical]
		[Obsolete("Because all GAC assemblies always get full trust, the full trust list is no longer meaningful. You should install any assemblies that are used in security policy in the GAC to ensure they are trusted.")]
		public void RemoveFullTrustAssembly(StrongNameMembershipCondition snMC)
		{
			if (snMC == null)
			{
				throw new ArgumentNullException("snMC");
			}
			this.CheckLoaded();
			object obj = null;
			IEnumerator enumerator = this.m_fullTrustAssemblies.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (((StrongNameMembershipCondition)enumerator.Current).Equals(snMC))
				{
					obj = enumerator.Current;
					break;
				}
			}
			if (obj == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_AssemblyNotFullTrust"));
			}
			ArrayList fullTrustAssemblies = this.m_fullTrustAssemblies;
			lock (fullTrustAssemblies)
			{
				this.m_fullTrustAssemblies.Remove(obj);
			}
		}

		/// <summary>Gets a list of <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> objects used to determine whether an assembly is a member of the group of assemblies used to evaluate security policy.</summary>
		/// <returns>A list of <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> objects used to determine whether an assembly is a member of the group of assemblies used to evaluate security policy. These assemblies are granted full trust during security policy evaluation of assemblies not in the list.</returns>
		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06002B08 RID: 11016 RVA: 0x000A067C File Offset: 0x0009E87C
		[Obsolete("Because all GAC assemblies always get full trust, the full trust list is no longer meaningful. You should install any assemblies that are used in security policy in the GAC to ensure they are trusted.")]
		public IList FullTrustAssemblies
		{
			[SecuritySafeCritical]
			get
			{
				this.CheckLoaded();
				return new ArrayList(this.m_fullTrustAssemblies);
			}
		}

		/// <summary>Adds a <see cref="T:System.Security.NamedPermissionSet" /> to the current policy level.</summary>
		/// <param name="permSet">The <see cref="T:System.Security.NamedPermissionSet" /> to add to the current policy level.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="permSet" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="permSet" /> parameter has the same name as an existing <see cref="T:System.Security.NamedPermissionSet" /> in the <see cref="T:System.Security.Policy.PolicyLevel" />.</exception>
		// Token: 0x06002B09 RID: 11017 RVA: 0x000A0690 File Offset: 0x0009E890
		[SecuritySafeCritical]
		public void AddNamedPermissionSet(NamedPermissionSet permSet)
		{
			if (permSet == null)
			{
				throw new ArgumentNullException("permSet");
			}
			this.CheckLoaded();
			this.LoadAllPermissionSets();
			lock (this)
			{
				IEnumerator enumerator = this.m_namedPermissionSets.GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (((NamedPermissionSet)enumerator.Current).Name.Equals(permSet.Name))
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_DuplicateName"));
					}
				}
				NamedPermissionSet namedPermissionSet = (NamedPermissionSet)permSet.Copy();
				namedPermissionSet.IgnoreTypeLoadFailures = true;
				this.m_namedPermissionSets.Add(namedPermissionSet);
			}
		}

		/// <summary>Removes the specified <see cref="T:System.Security.NamedPermissionSet" /> from the current policy level.</summary>
		/// <param name="permSet">The <see cref="T:System.Security.NamedPermissionSet" /> to remove from the current policy level.</param>
		/// <returns>The <see cref="T:System.Security.NamedPermissionSet" /> that was removed.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Security.NamedPermissionSet" /> specified by the <paramref name="permSet" /> parameter was not found.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="permSet" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002B0A RID: 11018 RVA: 0x000A0744 File Offset: 0x0009E944
		public NamedPermissionSet RemoveNamedPermissionSet(NamedPermissionSet permSet)
		{
			if (permSet == null)
			{
				throw new ArgumentNullException("permSet");
			}
			return this.RemoveNamedPermissionSet(permSet.Name);
		}

		/// <summary>Removes the <see cref="T:System.Security.NamedPermissionSet" /> with the specified name from the current policy level.</summary>
		/// <param name="name">The name of the <see cref="T:System.Security.NamedPermissionSet" /> to remove.</param>
		/// <returns>The <see cref="T:System.Security.NamedPermissionSet" /> that was removed.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is equal to the name of a reserved permission set.  
		///  -or-  
		///  A <see cref="T:System.Security.NamedPermissionSet" /> with the specified name cannot be found.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002B0B RID: 11019 RVA: 0x000A0760 File Offset: 0x0009E960
		[SecuritySafeCritical]
		public NamedPermissionSet RemoveNamedPermissionSet(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.CheckLoaded();
			this.LoadAllPermissionSets();
			int num = -1;
			for (int i = 0; i < PolicyLevel.s_reservedNamedPermissionSets.Length; i++)
			{
				if (PolicyLevel.s_reservedNamedPermissionSets[i].Equals(name))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_ReservedNPMS", new object[] { name }));
				}
			}
			ArrayList namedPermissionSets = this.m_namedPermissionSets;
			for (int j = 0; j < namedPermissionSets.Count; j++)
			{
				if (((NamedPermissionSet)namedPermissionSets[j]).Name.Equals(name))
				{
					num = j;
					break;
				}
			}
			if (num == -1)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NoNPMS"));
			}
			ArrayList arrayList = new ArrayList();
			arrayList.Add(this.m_rootCodeGroup);
			for (int k = 0; k < arrayList.Count; k++)
			{
				CodeGroup codeGroup = (CodeGroup)arrayList[k];
				if (codeGroup.PermissionSetName != null && codeGroup.PermissionSetName.Equals(name))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_NPMSInUse", new object[] { name }));
				}
				IEnumerator enumerator = codeGroup.Children.GetEnumerator();
				if (enumerator != null)
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						arrayList.Add(obj);
					}
				}
			}
			NamedPermissionSet namedPermissionSet = (NamedPermissionSet)namedPermissionSets[num];
			namedPermissionSets.RemoveAt(num);
			return namedPermissionSet;
		}

		/// <summary>Replaces a <see cref="T:System.Security.NamedPermissionSet" /> in the current policy level with the specified <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <param name="name">The name of the <see cref="T:System.Security.NamedPermissionSet" /> to replace.</param>
		/// <param name="pSet">The <see cref="T:System.Security.PermissionSet" /> that replaces the <see cref="T:System.Security.NamedPermissionSet" /> specified by the <paramref name="name" /> parameter.</param>
		/// <returns>A copy of the <see cref="T:System.Security.NamedPermissionSet" /> that was replaced.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="pSet" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is equal to the name of a reserved permission set.  
		///  -or-  
		///  The <see cref="T:System.Security.PermissionSet" /> specified by the <paramref name="pSet" /> parameter cannot be found.</exception>
		// Token: 0x06002B0C RID: 11020 RVA: 0x000A08C4 File Offset: 0x0009EAC4
		[SecuritySafeCritical]
		public NamedPermissionSet ChangeNamedPermissionSet(string name, PermissionSet pSet)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (pSet == null)
			{
				throw new ArgumentNullException("pSet");
			}
			for (int i = 0; i < PolicyLevel.s_reservedNamedPermissionSets.Length; i++)
			{
				if (PolicyLevel.s_reservedNamedPermissionSets[i].Equals(name))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_ReservedNPMS", new object[] { name }));
				}
			}
			NamedPermissionSet namedPermissionSetInternal = this.GetNamedPermissionSetInternal(name);
			if (namedPermissionSetInternal == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NoNPMS"));
			}
			NamedPermissionSet namedPermissionSet = (NamedPermissionSet)namedPermissionSetInternal.Copy();
			namedPermissionSetInternal.Reset();
			namedPermissionSetInternal.SetUnrestricted(pSet.IsUnrestricted());
			foreach (object obj in pSet)
			{
				namedPermissionSetInternal.SetPermission(((IPermission)obj).Copy());
			}
			if (pSet is NamedPermissionSet)
			{
				namedPermissionSetInternal.Description = ((NamedPermissionSet)pSet).Description;
			}
			return namedPermissionSet;
		}

		/// <summary>Returns the <see cref="T:System.Security.NamedPermissionSet" /> in the current policy level with the specified name.</summary>
		/// <param name="name">The name of the <see cref="T:System.Security.NamedPermissionSet" /> to find.</param>
		/// <returns>The <see cref="T:System.Security.NamedPermissionSet" /> in the current policy level with the specified name, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002B0D RID: 11021 RVA: 0x000A09A4 File Offset: 0x0009EBA4
		[SecuritySafeCritical]
		public NamedPermissionSet GetNamedPermissionSet(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			NamedPermissionSet namedPermissionSetInternal = this.GetNamedPermissionSetInternal(name);
			if (namedPermissionSetInternal != null)
			{
				return new NamedPermissionSet(namedPermissionSetInternal);
			}
			return null;
		}

		/// <summary>Replaces the configuration file for this <see cref="T:System.Security.Policy.PolicyLevel" /> with the last backup (reflecting the state of policy prior to the last time it was saved) and returns it to the state of the last save.</summary>
		/// <exception cref="T:System.Security.Policy.PolicyException">The policy level does not have a valid configuration file.</exception>
		// Token: 0x06002B0E RID: 11022 RVA: 0x000A09D4 File Offset: 0x0009EBD4
		[SecuritySafeCritical]
		public void Recover()
		{
			if (this.m_configId == ConfigId.None)
			{
				throw new PolicyException(Environment.GetResourceString("Policy_RecoverNotFileBased"));
			}
			lock (this)
			{
				if (!Config.RecoverData(this.m_configId))
				{
					throw new PolicyException(Environment.GetResourceString("Policy_RecoverNoConfigFile"));
				}
				this.m_loaded = false;
				this.m_rootCodeGroup = null;
				this.m_namedPermissionSets = null;
				this.m_fullTrustAssemblies = new ArrayList();
			}
		}

		/// <summary>Returns the current policy level to the default state.</summary>
		// Token: 0x06002B0F RID: 11023 RVA: 0x000A0A60 File Offset: 0x0009EC60
		[SecuritySafeCritical]
		public void Reset()
		{
			this.SetDefault();
		}

		/// <summary>Resolves policy based on evidence for the policy level, and returns the resulting <see cref="T:System.Security.Policy.PolicyStatement" />.</summary>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> used to resolve the <see cref="T:System.Security.Policy.PolicyLevel" />.</param>
		/// <returns>The resulting <see cref="T:System.Security.Policy.PolicyStatement" />.</returns>
		/// <exception cref="T:System.Security.Policy.PolicyException">The policy level contains multiple matching code groups marked as exclusive.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="evidence" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002B10 RID: 11024 RVA: 0x000A0A68 File Offset: 0x0009EC68
		[SecuritySafeCritical]
		public PolicyStatement Resolve(Evidence evidence)
		{
			return this.Resolve(evidence, 0, null);
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x06002B11 RID: 11025 RVA: 0x000A0A74 File Offset: 0x0009EC74
		[SecuritySafeCritical]
		public SecurityElement ToXml()
		{
			this.CheckLoaded();
			this.LoadAllPermissionSets();
			SecurityElement securityElement = new SecurityElement("PolicyLevel");
			securityElement.AddAttribute("version", "1");
			Hashtable hashtable = new Hashtable();
			lock (this)
			{
				SecurityElement securityElement2 = new SecurityElement("NamedPermissionSets");
				foreach (object obj in this.m_namedPermissionSets)
				{
					securityElement2.AddChild(this.NormalizeClassDeep(((NamedPermissionSet)obj).ToXml(), hashtable));
				}
				SecurityElement securityElement3 = this.NormalizeClassDeep(this.m_rootCodeGroup.ToXml(this), hashtable);
				SecurityElement securityElement4 = new SecurityElement("FullTrustAssemblies");
				foreach (object obj2 in this.m_fullTrustAssemblies)
				{
					securityElement4.AddChild(this.NormalizeClassDeep(((StrongNameMembershipCondition)obj2).ToXml(), hashtable));
				}
				SecurityElement securityElement5 = new SecurityElement("SecurityClasses");
				IDictionaryEnumerator enumerator2 = hashtable.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					SecurityElement securityElement6 = new SecurityElement("SecurityClass");
					securityElement6.AddAttribute("Name", (string)enumerator2.Value);
					securityElement6.AddAttribute("Description", (string)enumerator2.Key);
					securityElement5.AddChild(securityElement6);
				}
				securityElement.AddChild(securityElement5);
				securityElement.AddChild(securityElement2);
				securityElement.AddChild(securityElement3);
				securityElement.AddChild(securityElement4);
			}
			return securityElement;
		}

		/// <summary>Reconstructs a security object with a given state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Security.SecurityElement" /> specified by the <paramref name="e" /> parameter is invalid.</exception>
		// Token: 0x06002B12 RID: 11026 RVA: 0x000A0C00 File Offset: 0x0009EE00
		public void FromXml(SecurityElement e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			lock (this)
			{
				ArrayList arrayList = new ArrayList();
				SecurityElement securityElement = e.SearchForChildByTag("SecurityClasses");
				Hashtable hashtable;
				if (securityElement != null)
				{
					hashtable = new Hashtable();
					foreach (object obj in securityElement.Children)
					{
						SecurityElement securityElement2 = (SecurityElement)obj;
						if (securityElement2.Tag.Equals("SecurityClass"))
						{
							string text = securityElement2.Attribute("Name");
							string text2 = securityElement2.Attribute("Description");
							if (text != null && text2 != null)
							{
								hashtable.Add(text, text2);
							}
						}
					}
				}
				else
				{
					hashtable = null;
				}
				SecurityElement securityElement3 = e.SearchForChildByTag("FullTrustAssemblies");
				if (securityElement3 != null && securityElement3.InternalChildren != null)
				{
					string assemblyQualifiedName = typeof(StrongNameMembershipCondition).AssemblyQualifiedName;
					IEnumerator enumerator2 = securityElement3.Children.GetEnumerator();
					while (enumerator2.MoveNext())
					{
						StrongNameMembershipCondition strongNameMembershipCondition = new StrongNameMembershipCondition();
						strongNameMembershipCondition.FromXml((SecurityElement)enumerator2.Current);
						arrayList.Add(strongNameMembershipCondition);
					}
				}
				this.m_fullTrustAssemblies = arrayList;
				ArrayList arrayList2 = new ArrayList();
				SecurityElement securityElement4 = e.SearchForChildByTag("NamedPermissionSets");
				SecurityElement securityElement5 = null;
				if (securityElement4 != null && securityElement4.InternalChildren != null)
				{
					securityElement5 = this.UnnormalizeClassDeep(securityElement4, hashtable);
					foreach (string text3 in PolicyLevel.s_reservedNamedPermissionSets)
					{
						this.FindElement(securityElement5, text3);
					}
				}
				if (securityElement5 == null)
				{
					securityElement5 = new SecurityElement("NamedPermissionSets");
				}
				arrayList2.Add(BuiltInPermissionSets.FullTrust);
				arrayList2.Add(BuiltInPermissionSets.Everything);
				arrayList2.Add(BuiltInPermissionSets.SkipVerification);
				arrayList2.Add(BuiltInPermissionSets.Execution);
				arrayList2.Add(BuiltInPermissionSets.Nothing);
				arrayList2.Add(BuiltInPermissionSets.Internet);
				arrayList2.Add(BuiltInPermissionSets.LocalIntranet);
				foreach (object obj2 in arrayList2)
				{
					PermissionSet permissionSet = (PermissionSet)obj2;
					permissionSet.IgnoreTypeLoadFailures = true;
				}
				this.m_namedPermissionSets = arrayList2;
				this.m_permSetElement = securityElement5;
				SecurityElement securityElement6 = e.SearchForChildByTag("CodeGroup");
				if (securityElement6 == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXMLElement", new object[]
					{
						"CodeGroup",
						base.GetType().FullName
					}));
				}
				CodeGroup codeGroup = XMLUtil.CreateCodeGroup(this.UnnormalizeClassDeep(securityElement6, hashtable));
				if (codeGroup == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXMLElement", new object[]
					{
						"CodeGroup",
						base.GetType().FullName
					}));
				}
				codeGroup.FromXml(securityElement6, this);
				this.m_rootCodeGroup = codeGroup;
			}
		}

		// Token: 0x06002B13 RID: 11027 RVA: 0x000A0F04 File Offset: 0x0009F104
		[SecurityCritical]
		internal static PermissionSet GetBuiltInSet(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return null;
			}
			if (name.Equals("FullTrust"))
			{
				return BuiltInPermissionSets.FullTrust;
			}
			if (name.Equals("Nothing"))
			{
				return BuiltInPermissionSets.Nothing;
			}
			if (name.Equals("Execution"))
			{
				return BuiltInPermissionSets.Execution;
			}
			if (name.Equals("SkipVerification"))
			{
				return BuiltInPermissionSets.SkipVerification;
			}
			if (name.Equals("Internet"))
			{
				return BuiltInPermissionSets.Internet;
			}
			if (name.Equals("LocalIntranet"))
			{
				return BuiltInPermissionSets.LocalIntranet;
			}
			return null;
		}

		// Token: 0x06002B14 RID: 11028 RVA: 0x000A0F90 File Offset: 0x0009F190
		[SecurityCritical]
		internal NamedPermissionSet GetNamedPermissionSetInternal(string name)
		{
			this.CheckLoaded();
			object internalSyncObject = PolicyLevel.InternalSyncObject;
			lock (internalSyncObject)
			{
				foreach (object obj in this.m_namedPermissionSets)
				{
					NamedPermissionSet namedPermissionSet = (NamedPermissionSet)obj;
					if (namedPermissionSet.Name.Equals(name))
					{
						return namedPermissionSet;
					}
				}
				if (this.m_permSetElement != null)
				{
					SecurityElement securityElement = this.FindElement(this.m_permSetElement, name);
					if (securityElement != null)
					{
						NamedPermissionSet namedPermissionSet2 = new NamedPermissionSet();
						namedPermissionSet2.Name = name;
						this.m_namedPermissionSets.Add(namedPermissionSet2);
						try
						{
							namedPermissionSet2.FromXml(securityElement, false, true);
						}
						catch
						{
							this.m_namedPermissionSets.Remove(namedPermissionSet2);
							return null;
						}
						if (namedPermissionSet2.Name != null)
						{
							return namedPermissionSet2;
						}
						this.m_namedPermissionSets.Remove(namedPermissionSet2);
					}
				}
			}
			return null;
		}

		// Token: 0x06002B15 RID: 11029 RVA: 0x000A10B0 File Offset: 0x0009F2B0
		[SecurityCritical]
		internal PolicyStatement Resolve(Evidence evidence, int count, byte[] serializedEvidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			PolicyStatement policyStatement = null;
			if (serializedEvidence != null)
			{
				policyStatement = this.CheckCache(count, serializedEvidence);
			}
			if (policyStatement == null)
			{
				this.CheckLoaded();
				bool flag = this.m_fullTrustAssemblies != null && PolicyLevel.IsFullTrustAssembly(this.m_fullTrustAssemblies, evidence);
				bool flag2;
				if (flag)
				{
					policyStatement = new PolicyStatement(new PermissionSet(true), PolicyStatementAttribute.Nothing);
					flag2 = true;
				}
				else
				{
					ArrayList arrayList = this.GenericResolve(evidence, out flag2);
					policyStatement = new PolicyStatement();
					policyStatement.PermissionSet = null;
					foreach (object obj in arrayList)
					{
						PolicyStatement policy = ((CodeGroupStackFrame)obj).policy;
						if (policy != null)
						{
							policyStatement.GetPermissionSetNoCopy().InplaceUnion(policy.GetPermissionSetNoCopy());
							policyStatement.Attributes |= policy.Attributes;
							if (policy.HasDependentEvidence)
							{
								foreach (IDelayEvaluatedEvidence delayEvaluatedEvidence in policy.DependentEvidence)
								{
									delayEvaluatedEvidence.MarkUsed();
								}
							}
						}
					}
				}
				if (flag2)
				{
					this.Cache(count, evidence.RawSerialize(), policyStatement);
				}
			}
			return policyStatement;
		}

		// Token: 0x06002B16 RID: 11030 RVA: 0x000A11E0 File Offset: 0x0009F3E0
		[SecurityCritical]
		private void CheckLoaded()
		{
			if (!this.m_loaded)
			{
				object internalSyncObject = PolicyLevel.InternalSyncObject;
				lock (internalSyncObject)
				{
					if (!this.m_loaded)
					{
						this.LoadPolicyLevel();
					}
				}
			}
		}

		// Token: 0x06002B17 RID: 11031 RVA: 0x000A1230 File Offset: 0x0009F430
		private static byte[] ReadFile(string fileName)
		{
			byte[] array2;
			using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
			{
				int num = (int)fileStream.Length;
				byte[] array = new byte[num];
				num = fileStream.Read(array, 0, num);
				fileStream.Close();
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06002B18 RID: 11032 RVA: 0x000A1284 File Offset: 0x0009F484
		[SecurityCritical]
		private void LoadPolicyLevel()
		{
			Exception ex = null;
			CodeAccessPermission.Assert(true);
			if (File.InternalExists(this.m_path))
			{
				Encoding utf = Encoding.UTF8;
				SecurityElement securityElement;
				try
				{
					string @string = utf.GetString(PolicyLevel.ReadFile(this.m_path));
					securityElement = SecurityElement.FromString(@string);
				}
				catch (Exception ex2)
				{
					string text;
					if (!string.IsNullOrEmpty(ex2.Message))
					{
						text = ex2.Message;
					}
					else
					{
						text = ex2.GetType().AssemblyQualifiedName;
					}
					ex = this.LoadError(Environment.GetResourceString("Error_SecurityPolicyFileParseEx", new object[] { this.Label, text }));
					goto IL_1BD;
				}
				if (securityElement == null)
				{
					ex = this.LoadError(Environment.GetResourceString("Error_SecurityPolicyFileParse", new object[] { this.Label }));
				}
				else
				{
					SecurityElement securityElement2 = securityElement.SearchForChildByTag("mscorlib");
					if (securityElement2 == null)
					{
						ex = this.LoadError(Environment.GetResourceString("Error_SecurityPolicyFileParse", new object[] { this.Label }));
					}
					else
					{
						SecurityElement securityElement3 = securityElement2.SearchForChildByTag("security");
						if (securityElement3 == null)
						{
							ex = this.LoadError(Environment.GetResourceString("Error_SecurityPolicyFileParse", new object[] { this.Label }));
						}
						else
						{
							SecurityElement securityElement4 = securityElement3.SearchForChildByTag("policy");
							if (securityElement4 == null)
							{
								ex = this.LoadError(Environment.GetResourceString("Error_SecurityPolicyFileParse", new object[] { this.Label }));
							}
							else
							{
								SecurityElement securityElement5 = securityElement4.SearchForChildByTag("PolicyLevel");
								if (securityElement5 != null)
								{
									try
									{
										this.FromXml(securityElement5);
										goto IL_1B5;
									}
									catch (Exception)
									{
										ex = this.LoadError(Environment.GetResourceString("Error_SecurityPolicyFileParse", new object[] { this.Label }));
										goto IL_1BD;
									}
									goto IL_193;
									IL_1B5:
									this.m_loaded = true;
									return;
								}
								IL_193:
								ex = this.LoadError(Environment.GetResourceString("Error_SecurityPolicyFileParse", new object[] { this.Label }));
							}
						}
					}
				}
			}
			IL_1BD:
			this.SetDefault();
			this.m_loaded = true;
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x06002B19 RID: 11033 RVA: 0x000A147C File Offset: 0x0009F67C
		[SecurityCritical]
		private Exception LoadError(string message)
		{
			if (this.m_type != PolicyLevelType.User && this.m_type != PolicyLevelType.Machine && this.m_type != PolicyLevelType.Enterprise)
			{
				return new ArgumentException(message);
			}
			Config.WriteToEventLog(message);
			return null;
		}

		// Token: 0x06002B1A RID: 11034 RVA: 0x000A14A8 File Offset: 0x0009F6A8
		[SecurityCritical]
		private void Cache(int count, byte[] serializedEvidence, PolicyStatement policy)
		{
			if (this.m_configId == ConfigId.None)
			{
				return;
			}
			if (serializedEvidence == null)
			{
				return;
			}
			byte[] data = new SecurityDocument(policy.ToXml(null, true)).m_data;
			Config.AddCacheEntry(this.m_configId, count, serializedEvidence, data);
		}

		// Token: 0x06002B1B RID: 11035 RVA: 0x000A14E4 File Offset: 0x0009F6E4
		[SecurityCritical]
		private PolicyStatement CheckCache(int count, byte[] serializedEvidence)
		{
			if (this.m_configId == ConfigId.None)
			{
				return null;
			}
			if (serializedEvidence == null)
			{
				return null;
			}
			byte[] array;
			if (!Config.GetCacheEntry(this.m_configId, count, serializedEvidence, out array))
			{
				return null;
			}
			PolicyStatement policyStatement = new PolicyStatement();
			SecurityDocument securityDocument = new SecurityDocument(array);
			policyStatement.FromXml(securityDocument, 0, null, true);
			return policyStatement;
		}

		// Token: 0x06002B1C RID: 11036 RVA: 0x000A152C File Offset: 0x0009F72C
		[SecurityCritical]
		private static bool IsFullTrustAssembly(ArrayList fullTrustAssemblies, Evidence evidence)
		{
			if (fullTrustAssemblies.Count == 0)
			{
				return false;
			}
			if (evidence != null)
			{
				lock (fullTrustAssemblies)
				{
					foreach (object obj in fullTrustAssemblies)
					{
						StrongNameMembershipCondition strongNameMembershipCondition = (StrongNameMembershipCondition)obj;
						if (strongNameMembershipCondition.Check(evidence))
						{
							if (Environment.GetCompatibilityFlag(CompatibilityFlag.FullTrustListAssembliesInGac))
							{
								if (new ZoneMembershipCondition().Check(evidence))
								{
									return true;
								}
							}
							else if (new GacMembershipCondition().Check(evidence))
							{
								return true;
							}
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06002B1D RID: 11037 RVA: 0x000A15C4 File Offset: 0x0009F7C4
		private CodeGroup CreateDefaultAllGroup()
		{
			UnionCodeGroup unionCodeGroup = new UnionCodeGroup();
			unionCodeGroup.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "FullTrust", new AllMembershipCondition().ToXml()), this);
			unionCodeGroup.Name = Environment.GetResourceString("Policy_AllCode_Name");
			unionCodeGroup.Description = Environment.GetResourceString("Policy_AllCode_DescriptionFullTrust");
			return unionCodeGroup;
		}

		// Token: 0x06002B1E RID: 11038 RVA: 0x000A1618 File Offset: 0x0009F818
		[SecurityCritical]
		private CodeGroup CreateDefaultMachinePolicy()
		{
			UnionCodeGroup unionCodeGroup = new UnionCodeGroup();
			unionCodeGroup.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "Nothing", new AllMembershipCondition().ToXml()), this);
			unionCodeGroup.Name = Environment.GetResourceString("Policy_AllCode_Name");
			unionCodeGroup.Description = Environment.GetResourceString("Policy_AllCode_DescriptionNothing");
			UnionCodeGroup unionCodeGroup2 = new UnionCodeGroup();
			unionCodeGroup2.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "FullTrust", new ZoneMembershipCondition(SecurityZone.MyComputer).ToXml()), this);
			unionCodeGroup2.Name = Environment.GetResourceString("Policy_MyComputer_Name");
			unionCodeGroup2.Description = Environment.GetResourceString("Policy_MyComputer_Description");
			StrongNamePublicKeyBlob strongNamePublicKeyBlob = new StrongNamePublicKeyBlob("002400000480000094000000060200000024000052534131000400000100010007D1FA57C4AED9F0A32E84AA0FAEFD0DE9E8FD6AEC8F87FB03766C834C99921EB23BE79AD9D5DCC1DD9AD236132102900B723CF980957FC4E177108FC607774F29E8320E92EA05ECE4E821C0A5EFE8F1645C4C0C93C1AB99285D622CAA652C1DFAD63D745D6F2DE5F17E5EAF0FC4963D261C8A12436518206DC093344D5AD293");
			UnionCodeGroup unionCodeGroup3 = new UnionCodeGroup();
			unionCodeGroup3.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "FullTrust", new StrongNameMembershipCondition(strongNamePublicKeyBlob, null, null).ToXml()), this);
			unionCodeGroup3.Name = Environment.GetResourceString("Policy_Microsoft_Name");
			unionCodeGroup3.Description = Environment.GetResourceString("Policy_Microsoft_Description");
			unionCodeGroup2.AddChildInternal(unionCodeGroup3);
			strongNamePublicKeyBlob = new StrongNamePublicKeyBlob("00000000000000000400000000000000");
			UnionCodeGroup unionCodeGroup4 = new UnionCodeGroup();
			unionCodeGroup4.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "FullTrust", new StrongNameMembershipCondition(strongNamePublicKeyBlob, null, null).ToXml()), this);
			unionCodeGroup4.Name = Environment.GetResourceString("Policy_Ecma_Name");
			unionCodeGroup4.Description = Environment.GetResourceString("Policy_Ecma_Description");
			unionCodeGroup2.AddChildInternal(unionCodeGroup4);
			unionCodeGroup.AddChildInternal(unionCodeGroup2);
			CodeGroup codeGroup = new UnionCodeGroup();
			codeGroup.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "LocalIntranet", new ZoneMembershipCondition(SecurityZone.Intranet).ToXml()), this);
			codeGroup.Name = Environment.GetResourceString("Policy_Intranet_Name");
			codeGroup.Description = Environment.GetResourceString("Policy_Intranet_Description");
			codeGroup.AddChildInternal(new NetCodeGroup(new AllMembershipCondition())
			{
				Name = Environment.GetResourceString("Policy_IntranetNet_Name"),
				Description = Environment.GetResourceString("Policy_IntranetNet_Description")
			});
			codeGroup.AddChildInternal(new FileCodeGroup(new AllMembershipCondition(), FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery)
			{
				Name = Environment.GetResourceString("Policy_IntranetFile_Name"),
				Description = Environment.GetResourceString("Policy_IntranetFile_Description")
			});
			unionCodeGroup.AddChildInternal(codeGroup);
			CodeGroup codeGroup2 = new UnionCodeGroup();
			codeGroup2.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "Internet", new ZoneMembershipCondition(SecurityZone.Internet).ToXml()), this);
			codeGroup2.Name = Environment.GetResourceString("Policy_Internet_Name");
			codeGroup2.Description = Environment.GetResourceString("Policy_Internet_Description");
			codeGroup2.AddChildInternal(new NetCodeGroup(new AllMembershipCondition())
			{
				Name = Environment.GetResourceString("Policy_InternetNet_Name"),
				Description = Environment.GetResourceString("Policy_InternetNet_Description")
			});
			unionCodeGroup.AddChildInternal(codeGroup2);
			CodeGroup codeGroup3 = new UnionCodeGroup();
			codeGroup3.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "Nothing", new ZoneMembershipCondition(SecurityZone.Untrusted).ToXml()), this);
			codeGroup3.Name = Environment.GetResourceString("Policy_Untrusted_Name");
			codeGroup3.Description = Environment.GetResourceString("Policy_Untrusted_Description");
			unionCodeGroup.AddChildInternal(codeGroup3);
			CodeGroup codeGroup4 = new UnionCodeGroup();
			codeGroup4.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "Internet", new ZoneMembershipCondition(SecurityZone.Trusted).ToXml()), this);
			codeGroup4.Name = Environment.GetResourceString("Policy_Trusted_Name");
			codeGroup4.Description = Environment.GetResourceString("Policy_Trusted_Description");
			codeGroup4.AddChildInternal(new NetCodeGroup(new AllMembershipCondition())
			{
				Name = Environment.GetResourceString("Policy_TrustedNet_Name"),
				Description = Environment.GetResourceString("Policy_TrustedNet_Description")
			});
			unionCodeGroup.AddChildInternal(codeGroup4);
			return unionCodeGroup;
		}

		// Token: 0x06002B1F RID: 11039 RVA: 0x000A19A0 File Offset: 0x0009FBA0
		private static SecurityElement CreateCodeGroupElement(string codeGroupType, string permissionSetName, SecurityElement mshipElement)
		{
			SecurityElement securityElement = new SecurityElement("CodeGroup");
			securityElement.AddAttribute("class", "System.Security." + codeGroupType + ", mscorlib, Version={VERSION}, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			securityElement.AddAttribute("version", "1");
			securityElement.AddAttribute("PermissionSetName", permissionSetName);
			securityElement.AddChild(mshipElement);
			return securityElement;
		}

		// Token: 0x06002B20 RID: 11040 RVA: 0x000A19F8 File Offset: 0x0009FBF8
		private void SetDefaultFullTrustAssemblies()
		{
			this.m_fullTrustAssemblies = new ArrayList();
			StrongNamePublicKeyBlob strongNamePublicKeyBlob = new StrongNamePublicKeyBlob("00000000000000000400000000000000");
			for (int i = 0; i < PolicyLevel.EcmaFullTrustAssemblies.Length; i++)
			{
				StrongNameMembershipCondition strongNameMembershipCondition = new StrongNameMembershipCondition(strongNamePublicKeyBlob, PolicyLevel.EcmaFullTrustAssemblies[i], new Version("4.0.0.0"));
				this.m_fullTrustAssemblies.Add(strongNameMembershipCondition);
			}
			StrongNamePublicKeyBlob strongNamePublicKeyBlob2 = new StrongNamePublicKeyBlob("002400000480000094000000060200000024000052534131000400000100010007D1FA57C4AED9F0A32E84AA0FAEFD0DE9E8FD6AEC8F87FB03766C834C99921EB23BE79AD9D5DCC1DD9AD236132102900B723CF980957FC4E177108FC607774F29E8320E92EA05ECE4E821C0A5EFE8F1645C4C0C93C1AB99285D622CAA652C1DFAD63D745D6F2DE5F17E5EAF0FC4963D261C8A12436518206DC093344D5AD293");
			for (int j = 0; j < PolicyLevel.MicrosoftFullTrustAssemblies.Length; j++)
			{
				StrongNameMembershipCondition strongNameMembershipCondition2 = new StrongNameMembershipCondition(strongNamePublicKeyBlob2, PolicyLevel.MicrosoftFullTrustAssemblies[j], new Version("4.0.0.0"));
				this.m_fullTrustAssemblies.Add(strongNameMembershipCondition2);
			}
		}

		// Token: 0x06002B21 RID: 11041 RVA: 0x000A1A9C File Offset: 0x0009FC9C
		[SecurityCritical]
		private void SetDefault()
		{
			lock (this)
			{
				string text = PolicyLevel.GetLocationFromType(this.m_type) + ".default";
				if (File.InternalExists(text))
				{
					PolicyLevel policyLevel = new PolicyLevel(this.m_type, text);
					this.m_rootCodeGroup = policyLevel.RootCodeGroup;
					this.m_namedPermissionSets = (ArrayList)policyLevel.NamedPermissionSets;
					this.m_fullTrustAssemblies = (ArrayList)policyLevel.FullTrustAssemblies;
					this.m_loaded = true;
				}
				else
				{
					this.m_namedPermissionSets = null;
					this.m_rootCodeGroup = null;
					this.m_permSetElement = null;
					this.m_rootCodeGroup = ((this.m_type == PolicyLevelType.Machine) ? this.CreateDefaultMachinePolicy() : this.CreateDefaultAllGroup());
					this.SetFactoryPermissionSets();
					this.SetDefaultFullTrustAssemblies();
					this.m_loaded = true;
				}
			}
		}

		// Token: 0x06002B22 RID: 11042 RVA: 0x000A1B78 File Offset: 0x0009FD78
		private void SetFactoryPermissionSets()
		{
			object internalSyncObject = PolicyLevel.InternalSyncObject;
			lock (internalSyncObject)
			{
				this.m_namedPermissionSets = new ArrayList();
				this.m_namedPermissionSets.Add(BuiltInPermissionSets.FullTrust);
				this.m_namedPermissionSets.Add(BuiltInPermissionSets.Everything);
				this.m_namedPermissionSets.Add(BuiltInPermissionSets.Nothing);
				this.m_namedPermissionSets.Add(BuiltInPermissionSets.SkipVerification);
				this.m_namedPermissionSets.Add(BuiltInPermissionSets.Execution);
				this.m_namedPermissionSets.Add(BuiltInPermissionSets.Internet);
				this.m_namedPermissionSets.Add(BuiltInPermissionSets.LocalIntranet);
			}
		}

		// Token: 0x06002B23 RID: 11043 RVA: 0x000A1C34 File Offset: 0x0009FE34
		private SecurityElement FindElement(SecurityElement element, string name)
		{
			foreach (object obj in element.Children)
			{
				SecurityElement securityElement = (SecurityElement)obj;
				if (securityElement.Tag.Equals("PermissionSet"))
				{
					string text = securityElement.Attribute("Name");
					if (text != null && text.Equals(name))
					{
						element.InternalChildren.Remove(securityElement);
						return securityElement;
					}
				}
			}
			return null;
		}

		// Token: 0x06002B24 RID: 11044 RVA: 0x000A1C9C File Offset: 0x0009FE9C
		[SecurityCritical]
		private void LoadAllPermissionSets()
		{
			if (this.m_permSetElement != null && this.m_permSetElement.InternalChildren != null)
			{
				object internalSyncObject = PolicyLevel.InternalSyncObject;
				lock (internalSyncObject)
				{
					while (this.m_permSetElement != null && this.m_permSetElement.InternalChildren.Count != 0)
					{
						SecurityElement securityElement = (SecurityElement)this.m_permSetElement.Children[this.m_permSetElement.InternalChildren.Count - 1];
						this.m_permSetElement.InternalChildren.RemoveAt(this.m_permSetElement.InternalChildren.Count - 1);
						if (securityElement.Tag.Equals("PermissionSet") && securityElement.Attribute("class").Equals("System.Security.NamedPermissionSet"))
						{
							NamedPermissionSet namedPermissionSet = new NamedPermissionSet();
							namedPermissionSet.FromXmlNameOnly(securityElement);
							if (namedPermissionSet.Name != null)
							{
								this.m_namedPermissionSets.Add(namedPermissionSet);
								try
								{
									namedPermissionSet.FromXml(securityElement, false, true);
								}
								catch
								{
									this.m_namedPermissionSets.Remove(namedPermissionSet);
								}
							}
						}
					}
					this.m_permSetElement = null;
				}
			}
		}

		// Token: 0x06002B25 RID: 11045 RVA: 0x000A1DD4 File Offset: 0x0009FFD4
		[SecurityCritical]
		private ArrayList GenericResolve(Evidence evidence, out bool allConst)
		{
			CodeGroupStack codeGroupStack = new CodeGroupStack();
			CodeGroup rootCodeGroup = this.m_rootCodeGroup;
			if (rootCodeGroup == null)
			{
				throw new PolicyException(Environment.GetResourceString("Policy_NonFullTrustAssembly"));
			}
			CodeGroupStackFrame codeGroupStackFrame = new CodeGroupStackFrame();
			codeGroupStackFrame.current = rootCodeGroup;
			codeGroupStackFrame.parent = null;
			codeGroupStack.Push(codeGroupStackFrame);
			ArrayList arrayList = new ArrayList();
			bool flag = false;
			allConst = true;
			Exception ex = null;
			while (!codeGroupStack.IsEmpty())
			{
				codeGroupStackFrame = codeGroupStack.Pop();
				FirstMatchCodeGroup firstMatchCodeGroup = codeGroupStackFrame.current as FirstMatchCodeGroup;
				UnionCodeGroup unionCodeGroup = codeGroupStackFrame.current as UnionCodeGroup;
				if (!(codeGroupStackFrame.current.MembershipCondition is IConstantMembershipCondition) || (unionCodeGroup == null && firstMatchCodeGroup == null))
				{
					allConst = false;
				}
				try
				{
					codeGroupStackFrame.policy = PolicyManager.ResolveCodeGroup(codeGroupStackFrame.current, evidence);
				}
				catch (Exception ex2)
				{
					if (ex == null)
					{
						ex = ex2;
					}
				}
				if (codeGroupStackFrame.policy != null)
				{
					if ((codeGroupStackFrame.policy.Attributes & PolicyStatementAttribute.Exclusive) != PolicyStatementAttribute.Nothing)
					{
						if (flag)
						{
							throw new PolicyException(Environment.GetResourceString("Policy_MultipleExclusive"));
						}
						arrayList.RemoveRange(0, arrayList.Count);
						arrayList.Add(codeGroupStackFrame);
						flag = true;
					}
					if (!flag)
					{
						arrayList.Add(codeGroupStackFrame);
					}
				}
			}
			if (ex != null)
			{
				throw ex;
			}
			return arrayList;
		}

		// Token: 0x06002B26 RID: 11046 RVA: 0x000A1F04 File Offset: 0x000A0104
		private static string GenerateFriendlyName(string className, Hashtable classes)
		{
			if (classes.ContainsKey(className))
			{
				return (string)classes[className];
			}
			Type type = System.Type.GetType(className, false, false);
			if (type != null && !type.IsVisible)
			{
				type = null;
			}
			if (type == null)
			{
				return className;
			}
			if (!classes.ContainsValue(type.Name))
			{
				classes.Add(className, type.Name);
				return type.Name;
			}
			if (!classes.ContainsValue(type.FullName))
			{
				classes.Add(className, type.FullName);
				return type.FullName;
			}
			classes.Add(className, type.AssemblyQualifiedName);
			return type.AssemblyQualifiedName;
		}

		// Token: 0x06002B27 RID: 11047 RVA: 0x000A1FA8 File Offset: 0x000A01A8
		private SecurityElement NormalizeClassDeep(SecurityElement elem, Hashtable classes)
		{
			this.NormalizeClass(elem, classes);
			if (elem.InternalChildren != null && elem.InternalChildren.Count > 0)
			{
				foreach (object obj in elem.Children)
				{
					this.NormalizeClassDeep((SecurityElement)obj, classes);
				}
			}
			return elem;
		}

		// Token: 0x06002B28 RID: 11048 RVA: 0x000A2000 File Offset: 0x000A0200
		private SecurityElement NormalizeClass(SecurityElement elem, Hashtable classes)
		{
			if (elem.m_lAttributes == null || elem.m_lAttributes.Count == 0)
			{
				return elem;
			}
			int count = elem.m_lAttributes.Count;
			for (int i = 0; i < count; i += 2)
			{
				string text = (string)elem.m_lAttributes[i];
				if (text.Equals("class"))
				{
					string text2 = (string)elem.m_lAttributes[i + 1];
					elem.m_lAttributes[i + 1] = PolicyLevel.GenerateFriendlyName(text2, classes);
					break;
				}
			}
			return elem;
		}

		// Token: 0x06002B29 RID: 11049 RVA: 0x000A2088 File Offset: 0x000A0288
		private SecurityElement UnnormalizeClassDeep(SecurityElement elem, Hashtable classes)
		{
			this.UnnormalizeClass(elem, classes);
			if (elem.InternalChildren != null && elem.InternalChildren.Count > 0)
			{
				foreach (object obj in elem.Children)
				{
					this.UnnormalizeClassDeep((SecurityElement)obj, classes);
				}
			}
			return elem;
		}

		// Token: 0x06002B2A RID: 11050 RVA: 0x000A20E0 File Offset: 0x000A02E0
		private SecurityElement UnnormalizeClass(SecurityElement elem, Hashtable classes)
		{
			if (classes == null || elem.m_lAttributes == null || elem.m_lAttributes.Count == 0)
			{
				return elem;
			}
			int count = elem.m_lAttributes.Count;
			int i = 0;
			while (i < count)
			{
				string text = (string)elem.m_lAttributes[i];
				if (text.Equals("class"))
				{
					string text2 = (string)elem.m_lAttributes[i + 1];
					string text3 = (string)classes[text2];
					if (text3 != null)
					{
						elem.m_lAttributes[i + 1] = text3;
						break;
					}
					break;
				}
				else
				{
					i += 2;
				}
			}
			return elem;
		}

		// Token: 0x04001182 RID: 4482
		private ArrayList m_fullTrustAssemblies;

		// Token: 0x04001183 RID: 4483
		private ArrayList m_namedPermissionSets;

		// Token: 0x04001184 RID: 4484
		private CodeGroup m_rootCodeGroup;

		// Token: 0x04001185 RID: 4485
		private string m_label;

		// Token: 0x04001186 RID: 4486
		[OptionalField(VersionAdded = 2)]
		private PolicyLevelType m_type;

		// Token: 0x04001187 RID: 4487
		private ConfigId m_configId;

		// Token: 0x04001188 RID: 4488
		private bool m_useDefaultCodeGroupsOnReset;

		// Token: 0x04001189 RID: 4489
		private bool m_generateQuickCacheOnLoad;

		// Token: 0x0400118A RID: 4490
		private bool m_caching;

		// Token: 0x0400118B RID: 4491
		private bool m_throwOnLoadError;

		// Token: 0x0400118C RID: 4492
		private Encoding m_encoding;

		// Token: 0x0400118D RID: 4493
		private bool m_loaded;

		// Token: 0x0400118E RID: 4494
		private SecurityElement m_permSetElement;

		// Token: 0x0400118F RID: 4495
		private string m_path;

		// Token: 0x04001190 RID: 4496
		private static object s_InternalSyncObject;

		// Token: 0x04001191 RID: 4497
		private static readonly string[] s_reservedNamedPermissionSets = new string[] { "FullTrust", "Nothing", "Execution", "SkipVerification", "Internet", "LocalIntranet", "Everything" };

		// Token: 0x04001192 RID: 4498
		private static string[] EcmaFullTrustAssemblies = new string[] { "mscorlib.resources", "System", "System.resources", "System.Xml", "System.Xml.resources", "System.Windows.Forms", "System.Windows.Forms.resources", "System.Data", "System.Data.resources" };

		// Token: 0x04001193 RID: 4499
		private static string[] MicrosoftFullTrustAssemblies = new string[]
		{
			"System.Security", "System.Security.resources", "System.Drawing", "System.Drawing.resources", "System.Messaging", "System.Messaging.resources", "System.ServiceProcess", "System.ServiceProcess.resources", "System.DirectoryServices", "System.DirectoryServices.resources",
			"System.Deployment", "System.Deployment.resources"
		};
	}
}
