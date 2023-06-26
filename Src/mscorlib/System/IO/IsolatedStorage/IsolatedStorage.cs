using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Security.Policy;
using System.Threading;

namespace System.IO.IsolatedStorage
{
	/// <summary>Represents the abstract base class from which all isolated storage implementations must derive.</summary>
	// Token: 0x020001AF RID: 431
	[ComVisible(true)]
	public abstract class IsolatedStorage : MarshalByRefObject
	{
		// Token: 0x06001AF8 RID: 6904 RVA: 0x0005B362 File Offset: 0x00059562
		internal static bool IsRoaming(IsolatedStorageScope scope)
		{
			return (scope & IsolatedStorageScope.Roaming) > IsolatedStorageScope.None;
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x0005B36A File Offset: 0x0005956A
		internal bool IsRoaming()
		{
			return (this.m_Scope & IsolatedStorageScope.Roaming) > IsolatedStorageScope.None;
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x0005B377 File Offset: 0x00059577
		internal static bool IsDomain(IsolatedStorageScope scope)
		{
			return (scope & IsolatedStorageScope.Domain) > IsolatedStorageScope.None;
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x0005B37F File Offset: 0x0005957F
		internal bool IsDomain()
		{
			return (this.m_Scope & IsolatedStorageScope.Domain) > IsolatedStorageScope.None;
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x0005B38C File Offset: 0x0005958C
		internal static bool IsMachine(IsolatedStorageScope scope)
		{
			return (scope & IsolatedStorageScope.Machine) > IsolatedStorageScope.None;
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x0005B395 File Offset: 0x00059595
		internal bool IsAssembly()
		{
			return (this.m_Scope & IsolatedStorageScope.Assembly) > IsolatedStorageScope.None;
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x0005B3A2 File Offset: 0x000595A2
		internal static bool IsApp(IsolatedStorageScope scope)
		{
			return (scope & IsolatedStorageScope.Application) > IsolatedStorageScope.None;
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x0005B3AB File Offset: 0x000595AB
		internal bool IsApp()
		{
			return (this.m_Scope & IsolatedStorageScope.Application) > IsolatedStorageScope.None;
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x0005B3BC File Offset: 0x000595BC
		private string GetNameFromID(string typeID, string instanceID)
		{
			return typeID + this.SeparatorInternal.ToString() + instanceID;
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x0005B3E0 File Offset: 0x000595E0
		private static string GetPredefinedTypeName(object o)
		{
			if (o is Publisher)
			{
				return "Publisher";
			}
			if (o is StrongName)
			{
				return "StrongName";
			}
			if (o is Url)
			{
				return "Url";
			}
			if (o is Site)
			{
				return "Site";
			}
			if (o is Zone)
			{
				return "Zone";
			}
			return null;
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x0005B434 File Offset: 0x00059634
		internal static string GetHash(Stream s)
		{
			string text;
			using (SHA1 sha = new SHA1CryptoServiceProvider())
			{
				byte[] array = sha.ComputeHash(s);
				text = Path.ToBase32StringSuitableForDirName(array);
			}
			return text;
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x0005B474 File Offset: 0x00059674
		private static bool IsValidName(string s)
		{
			for (int i = 0; i < s.Length; i++)
			{
				if (!char.IsLetter(s[i]) && !char.IsDigit(s[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x0005B4B1 File Offset: 0x000596B1
		private static SecurityPermission GetControlEvidencePermission()
		{
			if (IsolatedStorage.s_PermControlEvidence == null)
			{
				IsolatedStorage.s_PermControlEvidence = new SecurityPermission(SecurityPermissionFlag.ControlEvidence);
			}
			return IsolatedStorage.s_PermControlEvidence;
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x0005B4D1 File Offset: 0x000596D1
		private static PermissionSet GetUnrestricted()
		{
			if (IsolatedStorage.s_PermUnrestricted == null)
			{
				IsolatedStorage.s_PermUnrestricted = new PermissionSet(PermissionState.Unrestricted);
			}
			return IsolatedStorage.s_PermUnrestricted;
		}

		/// <summary>Gets a backslash character that can be used in a directory string. When overridden in a derived class, another character might be returned.</summary>
		/// <returns>The default implementation returns the '\' (backslash) character.</returns>
		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06001B06 RID: 6918 RVA: 0x0005B4F0 File Offset: 0x000596F0
		protected virtual char SeparatorExternal
		{
			get
			{
				return '\\';
			}
		}

		/// <summary>Gets a period character that can be used in a directory string. When overridden in a derived class, another character might be returned.</summary>
		/// <returns>The default implementation returns the '.' (period) character.</returns>
		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06001B07 RID: 6919 RVA: 0x0005B4F4 File Offset: 0x000596F4
		protected virtual char SeparatorInternal
		{
			get
			{
				return '.';
			}
		}

		/// <summary>Gets a value representing the maximum amount of space available for isolated storage. When overridden in a derived class, this value can take different units of measure.</summary>
		/// <returns>The maximum amount of isolated storage space in bytes. Derived classes can return different units of value.</returns>
		/// <exception cref="T:System.InvalidOperationException">The quota has not been defined.</exception>
		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06001B08 RID: 6920 RVA: 0x0005B4F8 File Offset: 0x000596F8
		[CLSCompliant(false)]
		[Obsolete("IsolatedStorage.MaximumSize has been deprecated because it is not CLS Compliant.  To get the maximum size use IsolatedStorage.Quota")]
		public virtual ulong MaximumSize
		{
			get
			{
				if (this.m_ValidQuota)
				{
					return this.m_Quota;
				}
				throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_QuotaIsUndefined", new object[] { "MaximumSize" }));
			}
		}

		/// <summary>Gets a value representing the current size of isolated storage.</summary>
		/// <returns>The number of storage units currently used within the isolated storage scope.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current size of the isolated store is undefined.</exception>
		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06001B09 RID: 6921 RVA: 0x0005B526 File Offset: 0x00059726
		[CLSCompliant(false)]
		[Obsolete("IsolatedStorage.CurrentSize has been deprecated because it is not CLS Compliant.  To get the current size use IsolatedStorage.UsedSize")]
		public virtual ulong CurrentSize
		{
			get
			{
				throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_CurrentSizeUndefined", new object[] { "CurrentSize" }));
			}
		}

		/// <summary>When overridden in a derived class, gets a value that represents the amount of the space used for isolated storage.</summary>
		/// <returns>The used amount of isolated storage space, in bytes.</returns>
		/// <exception cref="T:System.InvalidOperationException">An operation was performed that requires access to <see cref="P:System.IO.IsolatedStorage.IsolatedStorage.UsedSize" />, but that property is not defined for this store. Stores that are obtained by using enumerations do not have a well-defined <see cref="P:System.IO.IsolatedStorage.IsolatedStorage.UsedSize" /> property, because partial evidence is used to open the store.</exception>
		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06001B0A RID: 6922 RVA: 0x0005B545 File Offset: 0x00059745
		[ComVisible(false)]
		public virtual long UsedSize
		{
			get
			{
				throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_CurrentSizeUndefined", new object[] { "UsedSize" }));
			}
		}

		/// <summary>When overridden in a derived class, gets a value that represents the maximum amount of space available for isolated storage.</summary>
		/// <returns>The limit of isolated storage space, in bytes.</returns>
		/// <exception cref="T:System.InvalidOperationException">An operation was performed that requires access to <see cref="P:System.IO.IsolatedStorage.IsolatedStorage.Quota" />, but that property is not defined for this store. Stores that are obtained by using enumerations do not have a well-defined <see cref="P:System.IO.IsolatedStorage.IsolatedStorage.Quota" /> property, because partial evidence is used to open the store.</exception>
		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06001B0B RID: 6923 RVA: 0x0005B564 File Offset: 0x00059764
		// (set) Token: 0x06001B0C RID: 6924 RVA: 0x0005B592 File Offset: 0x00059792
		[ComVisible(false)]
		public virtual long Quota
		{
			get
			{
				if (this.m_ValidQuota)
				{
					return (long)this.m_Quota;
				}
				throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_QuotaIsUndefined", new object[] { "Quota" }));
			}
			internal set
			{
				this.m_Quota = (ulong)value;
				this.m_ValidQuota = true;
			}
		}

		/// <summary>When overridden in a derived class, gets the available free space for isolated storage, in bytes.</summary>
		/// <returns>The available free space for isolated storage, in bytes.</returns>
		/// <exception cref="T:System.InvalidOperationException">An operation was performed that requires access to <see cref="P:System.IO.IsolatedStorage.IsolatedStorage.AvailableFreeSpace" />, but that property is not defined for this store. Stores that are obtained by using enumerations do not have a well-defined <see cref="P:System.IO.IsolatedStorage.IsolatedStorage.AvailableFreeSpace" /> property, because partial evidence is used to open the store.</exception>
		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06001B0D RID: 6925 RVA: 0x0005B5A2 File Offset: 0x000597A2
		[ComVisible(false)]
		public virtual long AvailableFreeSpace
		{
			get
			{
				throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_QuotaIsUndefined", new object[] { "AvailableFreeSpace" }));
			}
		}

		/// <summary>Gets a domain identity that scopes isolated storage.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Domain" /> identity.</returns>
		/// <exception cref="T:System.Security.SecurityException">The code lacks the required <see cref="T:System.Security.Permissions.SecurityPermission" /> to access this object. These permissions are granted by the runtime based on security policy.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> object is not isolated by the domain <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" />.</exception>
		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06001B0E RID: 6926 RVA: 0x0005B5C1 File Offset: 0x000597C1
		public object DomainIdentity
		{
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPolicy)]
			get
			{
				if (this.IsDomain())
				{
					return this.m_DomainIdentity;
				}
				throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_DomainUndefined"));
			}
		}

		/// <summary>Gets an application identity that scopes isolated storage.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the <see cref="F:System.IO.IsolatedStorage.IsolatedStorageScope.Application" /> identity.</returns>
		/// <exception cref="T:System.Security.SecurityException">The code lacks the required <see cref="T:System.Security.Permissions.SecurityPermission" /> to access this object. These permissions are granted by the runtime based on security policy.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> object is not isolated by the application <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" />.</exception>
		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06001B0F RID: 6927 RVA: 0x0005B5E1 File Offset: 0x000597E1
		[ComVisible(false)]
		public object ApplicationIdentity
		{
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPolicy)]
			get
			{
				if (this.IsApp())
				{
					return this.m_AppIdentity;
				}
				throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_ApplicationUndefined"));
			}
		}

		/// <summary>Gets an assembly identity used to scope isolated storage.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the <see cref="T:System.Reflection.Assembly" /> identity.</returns>
		/// <exception cref="T:System.Security.SecurityException">The code lacks the required <see cref="T:System.Security.Permissions.SecurityPermission" /> to access this object.</exception>
		/// <exception cref="T:System.InvalidOperationException">The assembly is not defined.</exception>
		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06001B10 RID: 6928 RVA: 0x0005B601 File Offset: 0x00059801
		public object AssemblyIdentity
		{
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPolicy)]
			get
			{
				if (this.IsAssembly())
				{
					return this.m_AssemIdentity;
				}
				throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_AssemblyUndefined"));
			}
		}

		/// <summary>When overridden in a derived class, prompts a user to approve a larger quota size, in bytes, for isolated storage.</summary>
		/// <param name="newQuotaSize">The requested new quota size, in bytes, for the user to approve.</param>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x06001B11 RID: 6929 RVA: 0x0005B621 File Offset: 0x00059821
		[ComVisible(false)]
		public virtual bool IncreaseQuotaTo(long newQuotaSize)
		{
			return false;
		}

		// Token: 0x06001B12 RID: 6930 RVA: 0x0005B624 File Offset: 0x00059824
		[SecurityCritical]
		internal MemoryStream GetIdentityStream(IsolatedStorageScope scope)
		{
			IsolatedStorage.GetUnrestricted().Assert();
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			MemoryStream memoryStream = new MemoryStream();
			object obj;
			if (IsolatedStorage.IsApp(scope))
			{
				obj = this.m_AppIdentity;
			}
			else if (IsolatedStorage.IsDomain(scope))
			{
				obj = this.m_DomainIdentity;
			}
			else
			{
				obj = this.m_AssemIdentity;
			}
			if (obj != null)
			{
				binaryFormatter.Serialize(memoryStream, obj);
			}
			memoryStream.Position = 0L;
			return memoryStream;
		}

		/// <summary>Gets an <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" /> enumeration value specifying the scope used to isolate the store.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" /> values specifying the scope used to isolate the store.</returns>
		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06001B13 RID: 6931 RVA: 0x0005B684 File Offset: 0x00059884
		public IsolatedStorageScope Scope
		{
			get
			{
				return this.m_Scope;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06001B14 RID: 6932 RVA: 0x0005B68C File Offset: 0x0005988C
		internal string DomainName
		{
			get
			{
				if (this.IsDomain())
				{
					return this.m_DomainName;
				}
				throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_DomainUndefined"));
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06001B15 RID: 6933 RVA: 0x0005B6AC File Offset: 0x000598AC
		internal string AssemName
		{
			get
			{
				if (this.IsAssembly())
				{
					return this.m_AssemName;
				}
				throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_AssemblyUndefined"));
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06001B16 RID: 6934 RVA: 0x0005B6CC File Offset: 0x000598CC
		internal string AppName
		{
			get
			{
				if (this.IsApp())
				{
					return this.m_AppName;
				}
				throw new InvalidOperationException(Environment.GetResourceString("IsolatedStorage_ApplicationUndefined"));
			}
		}

		/// <summary>Initializes a new <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> object.</summary>
		/// <param name="scope">A bitwise combination of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" /> values.</param>
		/// <param name="domainEvidenceType">The type of <see cref="T:System.Security.Policy.Evidence" /> that you can choose from the list of <see cref="T:System.Security.Policy.Evidence" /> present in the domain of the calling application. <see langword="null" /> lets the <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> object choose the evidence.</param>
		/// <param name="assemblyEvidenceType">The type of <see cref="T:System.Security.Policy.Evidence" /> that you can choose from the list of <see cref="T:System.Security.Policy.Evidence" /> present in the assembly of the calling application. <see langword="null" /> lets the <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> object choose the evidence.</param>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The assembly specified has insufficient permissions to create isolated stores.</exception>
		// Token: 0x06001B17 RID: 6935 RVA: 0x0005B6EC File Offset: 0x000598EC
		[SecuritySafeCritical]
		protected void InitStore(IsolatedStorageScope scope, Type domainEvidenceType, Type assemblyEvidenceType)
		{
			PermissionSet permissionSet = null;
			PermissionSet permissionSet2 = null;
			RuntimeAssembly caller = IsolatedStorage.GetCaller();
			IsolatedStorage.GetControlEvidencePermission().Assert();
			if (IsolatedStorage.IsDomain(scope))
			{
				AppDomain domain = Thread.GetDomain();
				if (!IsolatedStorage.IsRoaming(scope))
				{
					permissionSet = domain.PermissionSet;
					if (permissionSet == null)
					{
						throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_DomainGrantSet"));
					}
				}
				this._InitStore(scope, domain.Evidence, domainEvidenceType, caller.Evidence, assemblyEvidenceType, null, null);
			}
			else
			{
				if (!IsolatedStorage.IsRoaming(scope))
				{
					caller.GetGrantSet(out permissionSet, out permissionSet2);
					if (permissionSet == null)
					{
						throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_AssemblyGrantSet"));
					}
				}
				this._InitStore(scope, null, null, caller.Evidence, assemblyEvidenceType, null, null);
			}
			this.SetQuota(permissionSet, permissionSet2);
		}

		/// <summary>Initializes a new <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> object.</summary>
		/// <param name="scope">A bitwise combination of the <see cref="T:System.IO.IsolatedStorage.IsolatedStorageScope" /> values.</param>
		/// <param name="appEvidenceType">The type of <see cref="T:System.Security.Policy.Evidence" /> that you can choose from the list of <see cref="T:System.Security.Policy.Evidence" /> for the calling application. <see langword="null" /> lets the <see cref="T:System.IO.IsolatedStorage.IsolatedStorage" /> object choose the evidence.</param>
		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The assembly specified has insufficient permissions to create isolated stores.</exception>
		// Token: 0x06001B18 RID: 6936 RVA: 0x0005B798 File Offset: 0x00059998
		[SecuritySafeCritical]
		protected void InitStore(IsolatedStorageScope scope, Type appEvidenceType)
		{
			PermissionSet permissionSet = null;
			PermissionSet permissionSet2 = null;
			Assembly caller = IsolatedStorage.GetCaller();
			IsolatedStorage.GetControlEvidencePermission().Assert();
			if (IsolatedStorage.IsApp(scope))
			{
				AppDomain domain = Thread.GetDomain();
				if (!IsolatedStorage.IsRoaming(scope))
				{
					permissionSet = domain.PermissionSet;
					if (permissionSet == null)
					{
						throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_DomainGrantSet"));
					}
				}
				ActivationContext activationContext = AppDomain.CurrentDomain.ActivationContext;
				if (activationContext == null)
				{
					throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_ApplicationMissingIdentity"));
				}
				ApplicationSecurityInfo applicationSecurityInfo = new ApplicationSecurityInfo(activationContext);
				this._InitStore(scope, null, null, null, null, applicationSecurityInfo.ApplicationEvidence, appEvidenceType);
			}
			this.SetQuota(permissionSet, permissionSet2);
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x0005B830 File Offset: 0x00059A30
		[SecuritySafeCritical]
		internal void InitStore(IsolatedStorageScope scope, object domain, object assem, object app)
		{
			PermissionSet permissionSet = null;
			PermissionSet permissionSet2 = null;
			Evidence evidence = null;
			Evidence evidence2 = null;
			Evidence evidence3 = null;
			if (IsolatedStorage.IsApp(scope))
			{
				EvidenceBase evidenceBase = app as EvidenceBase;
				if (evidenceBase == null)
				{
					evidenceBase = new LegacyEvidenceWrapper(app);
				}
				evidence3 = new Evidence();
				evidence3.AddHostEvidence<EvidenceBase>(evidenceBase);
			}
			else
			{
				EvidenceBase evidenceBase2 = assem as EvidenceBase;
				if (evidenceBase2 == null)
				{
					evidenceBase2 = new LegacyEvidenceWrapper(assem);
				}
				evidence2 = new Evidence();
				evidence2.AddHostEvidence<EvidenceBase>(evidenceBase2);
				if (IsolatedStorage.IsDomain(scope))
				{
					EvidenceBase evidenceBase3 = domain as EvidenceBase;
					if (evidenceBase3 == null)
					{
						evidenceBase3 = new LegacyEvidenceWrapper(domain);
					}
					evidence = new Evidence();
					evidence.AddHostEvidence<EvidenceBase>(evidenceBase3);
				}
			}
			this._InitStore(scope, evidence, null, evidence2, null, evidence3, null);
			if (!IsolatedStorage.IsRoaming(scope))
			{
				RuntimeAssembly caller = IsolatedStorage.GetCaller();
				IsolatedStorage.GetControlEvidencePermission().Assert();
				caller.GetGrantSet(out permissionSet, out permissionSet2);
				if (permissionSet == null)
				{
					throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_AssemblyGrantSet"));
				}
			}
			this.SetQuota(permissionSet, permissionSet2);
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x0005B914 File Offset: 0x00059B14
		[SecurityCritical]
		internal void InitStore(IsolatedStorageScope scope, Evidence domainEv, Type domainEvidenceType, Evidence assemEv, Type assemEvidenceType, Evidence appEv, Type appEvidenceType)
		{
			PermissionSet permissionSet = null;
			if (!IsolatedStorage.IsRoaming(scope))
			{
				if (IsolatedStorage.IsApp(scope))
				{
					permissionSet = SecurityManager.GetStandardSandbox(appEv);
				}
				else if (IsolatedStorage.IsDomain(scope))
				{
					permissionSet = SecurityManager.GetStandardSandbox(domainEv);
				}
				else
				{
					permissionSet = SecurityManager.GetStandardSandbox(assemEv);
				}
			}
			this._InitStore(scope, domainEv, domainEvidenceType, assemEv, assemEvidenceType, appEv, appEvidenceType);
			this.SetQuota(permissionSet, null);
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x0005B970 File Offset: 0x00059B70
		[SecuritySafeCritical]
		internal bool InitStore(IsolatedStorageScope scope, Stream domain, Stream assem, Stream app, string domainName, string assemName, string appName)
		{
			try
			{
				IsolatedStorage.GetUnrestricted().Assert();
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				if (IsolatedStorage.IsApp(scope))
				{
					this.m_AppIdentity = binaryFormatter.Deserialize(app);
					this.m_AppName = appName;
				}
				else
				{
					this.m_AssemIdentity = binaryFormatter.Deserialize(assem);
					this.m_AssemName = assemName;
					if (IsolatedStorage.IsDomain(scope))
					{
						this.m_DomainIdentity = binaryFormatter.Deserialize(domain);
						this.m_DomainName = domainName;
					}
				}
			}
			catch
			{
				return false;
			}
			this.m_Scope = scope;
			return true;
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x0005BA00 File Offset: 0x00059C00
		[SecurityCritical]
		private void _InitStore(IsolatedStorageScope scope, Evidence domainEv, Type domainEvidenceType, Evidence assemEv, Type assemblyEvidenceType, Evidence appEv, Type appEvidenceType)
		{
			IsolatedStorage.VerifyScope(scope);
			if (IsolatedStorage.IsApp(scope))
			{
				if (appEv == null)
				{
					throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_ApplicationMissingIdentity"));
				}
			}
			else
			{
				if (assemEv == null)
				{
					throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_AssemblyMissingIdentity"));
				}
				if (IsolatedStorage.IsDomain(scope) && domainEv == null)
				{
					throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_DomainMissingIdentity"));
				}
			}
			IsolatedStorage.DemandPermission(scope);
			string text = null;
			string text2 = null;
			if (IsolatedStorage.IsApp(scope))
			{
				this.m_AppIdentity = IsolatedStorage.GetAccountingInfo(appEv, appEvidenceType, IsolatedStorageScope.Application, out text, out text2);
				this.m_AppName = this.GetNameFromID(text, text2);
			}
			else
			{
				this.m_AssemIdentity = IsolatedStorage.GetAccountingInfo(assemEv, assemblyEvidenceType, IsolatedStorageScope.Assembly, out text, out text2);
				this.m_AssemName = this.GetNameFromID(text, text2);
				if (IsolatedStorage.IsDomain(scope))
				{
					this.m_DomainIdentity = IsolatedStorage.GetAccountingInfo(domainEv, domainEvidenceType, IsolatedStorageScope.Domain, out text, out text2);
					this.m_DomainName = this.GetNameFromID(text, text2);
				}
			}
			this.m_Scope = scope;
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x0005BAE8 File Offset: 0x00059CE8
		[SecurityCritical]
		private static object GetAccountingInfo(Evidence evidence, Type evidenceType, IsolatedStorageScope fAssmDomApp, out string typeName, out string instanceName)
		{
			object obj = null;
			object obj2 = IsolatedStorage._GetAccountingInfo(evidence, evidenceType, fAssmDomApp, out obj);
			typeName = IsolatedStorage.GetPredefinedTypeName(obj2);
			if (typeName == null)
			{
				IsolatedStorage.GetUnrestricted().Assert();
				MemoryStream memoryStream = new MemoryStream();
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				binaryFormatter.Serialize(memoryStream, obj2.GetType());
				memoryStream.Position = 0L;
				typeName = IsolatedStorage.GetHash(memoryStream);
				CodeAccessPermission.RevertAssert();
			}
			instanceName = null;
			if (obj != null)
			{
				if (obj is Stream)
				{
					instanceName = IsolatedStorage.GetHash((Stream)obj);
				}
				else if (obj is string)
				{
					if (IsolatedStorage.IsValidName((string)obj))
					{
						instanceName = (string)obj;
					}
					else
					{
						MemoryStream memoryStream = new MemoryStream();
						BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
						binaryWriter.Write((string)obj);
						memoryStream.Position = 0L;
						instanceName = IsolatedStorage.GetHash(memoryStream);
					}
				}
			}
			else
			{
				obj = obj2;
			}
			if (instanceName == null)
			{
				IsolatedStorage.GetUnrestricted().Assert();
				MemoryStream memoryStream = new MemoryStream();
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				binaryFormatter.Serialize(memoryStream, obj);
				memoryStream.Position = 0L;
				instanceName = IsolatedStorage.GetHash(memoryStream);
				CodeAccessPermission.RevertAssert();
			}
			return obj2;
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x0005BBF4 File Offset: 0x00059DF4
		private static object _GetAccountingInfo(Evidence evidence, Type evidenceType, IsolatedStorageScope fAssmDomApp, out object oNormalized)
		{
			object obj;
			if (evidenceType == null)
			{
				obj = evidence.GetHostEvidence<Publisher>();
				if (obj == null)
				{
					obj = evidence.GetHostEvidence<StrongName>();
				}
				if (obj == null)
				{
					obj = evidence.GetHostEvidence<Url>();
				}
				if (obj == null)
				{
					obj = evidence.GetHostEvidence<Site>();
				}
				if (obj == null)
				{
					obj = evidence.GetHostEvidence<Zone>();
				}
				if (obj == null)
				{
					if (fAssmDomApp == IsolatedStorageScope.Domain)
					{
						throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_DomainNoEvidence"));
					}
					if (fAssmDomApp == IsolatedStorageScope.Application)
					{
						throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_ApplicationNoEvidence"));
					}
					throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_AssemblyNoEvidence"));
				}
			}
			else
			{
				obj = evidence.GetHostEvidence(evidenceType);
				if (obj == null)
				{
					if (fAssmDomApp == IsolatedStorageScope.Domain)
					{
						throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_DomainNoEvidence"));
					}
					if (fAssmDomApp == IsolatedStorageScope.Application)
					{
						throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_ApplicationNoEvidence"));
					}
					throw new IsolatedStorageException(Environment.GetResourceString("IsolatedStorage_AssemblyNoEvidence"));
				}
			}
			if (obj is INormalizeForIsolatedStorage)
			{
				oNormalized = ((INormalizeForIsolatedStorage)obj).Normalize();
			}
			else if (obj is Publisher)
			{
				oNormalized = ((Publisher)obj).Normalize();
			}
			else if (obj is StrongName)
			{
				oNormalized = ((StrongName)obj).Normalize();
			}
			else if (obj is Url)
			{
				oNormalized = ((Url)obj).Normalize();
			}
			else if (obj is Site)
			{
				oNormalized = ((Site)obj).Normalize();
			}
			else if (obj is Zone)
			{
				oNormalized = ((Zone)obj).Normalize();
			}
			else
			{
				oNormalized = null;
			}
			return obj;
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x0005BD4C File Offset: 0x00059F4C
		[SecurityCritical]
		private static void DemandPermission(IsolatedStorageScope scope)
		{
			IsolatedStorageFilePermission isolatedStorageFilePermission = null;
			if (scope <= (IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly | IsolatedStorageScope.Roaming))
			{
				if (scope <= (IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly))
				{
					if (scope != (IsolatedStorageScope.User | IsolatedStorageScope.Assembly))
					{
						if (scope == (IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly))
						{
							if (IsolatedStorage.s_PermDomain == null)
							{
								IsolatedStorage.s_PermDomain = new IsolatedStorageFilePermission(IsolatedStorageContainment.DomainIsolationByUser, 0L, false);
							}
							isolatedStorageFilePermission = IsolatedStorage.s_PermDomain;
						}
					}
					else
					{
						if (IsolatedStorage.s_PermAssem == null)
						{
							IsolatedStorage.s_PermAssem = new IsolatedStorageFilePermission(IsolatedStorageContainment.AssemblyIsolationByUser, 0L, false);
						}
						isolatedStorageFilePermission = IsolatedStorage.s_PermAssem;
					}
				}
				else if (scope != (IsolatedStorageScope.User | IsolatedStorageScope.Assembly | IsolatedStorageScope.Roaming))
				{
					if (scope == (IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly | IsolatedStorageScope.Roaming))
					{
						if (IsolatedStorage.s_PermDomainRoaming == null)
						{
							IsolatedStorage.s_PermDomainRoaming = new IsolatedStorageFilePermission(IsolatedStorageContainment.DomainIsolationByRoamingUser, 0L, false);
						}
						isolatedStorageFilePermission = IsolatedStorage.s_PermDomainRoaming;
					}
				}
				else
				{
					if (IsolatedStorage.s_PermAssemRoaming == null)
					{
						IsolatedStorage.s_PermAssemRoaming = new IsolatedStorageFilePermission(IsolatedStorageContainment.AssemblyIsolationByRoamingUser, 0L, false);
					}
					isolatedStorageFilePermission = IsolatedStorage.s_PermAssemRoaming;
				}
			}
			else if (scope <= (IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly | IsolatedStorageScope.Machine))
			{
				if (scope != (IsolatedStorageScope.Assembly | IsolatedStorageScope.Machine))
				{
					if (scope == (IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly | IsolatedStorageScope.Machine))
					{
						if (IsolatedStorage.s_PermMachineDomain == null)
						{
							IsolatedStorage.s_PermMachineDomain = new IsolatedStorageFilePermission(IsolatedStorageContainment.DomainIsolationByMachine, 0L, false);
						}
						isolatedStorageFilePermission = IsolatedStorage.s_PermMachineDomain;
					}
				}
				else
				{
					if (IsolatedStorage.s_PermMachineAssem == null)
					{
						IsolatedStorage.s_PermMachineAssem = new IsolatedStorageFilePermission(IsolatedStorageContainment.AssemblyIsolationByMachine, 0L, false);
					}
					isolatedStorageFilePermission = IsolatedStorage.s_PermMachineAssem;
				}
			}
			else if (scope != (IsolatedStorageScope.User | IsolatedStorageScope.Application))
			{
				if (scope != (IsolatedStorageScope.User | IsolatedStorageScope.Roaming | IsolatedStorageScope.Application))
				{
					if (scope == (IsolatedStorageScope.Machine | IsolatedStorageScope.Application))
					{
						if (IsolatedStorage.s_PermAppMachine == null)
						{
							IsolatedStorage.s_PermAppMachine = new IsolatedStorageFilePermission(IsolatedStorageContainment.ApplicationIsolationByMachine, 0L, false);
						}
						isolatedStorageFilePermission = IsolatedStorage.s_PermAppMachine;
					}
				}
				else
				{
					if (IsolatedStorage.s_PermAppUserRoaming == null)
					{
						IsolatedStorage.s_PermAppUserRoaming = new IsolatedStorageFilePermission(IsolatedStorageContainment.ApplicationIsolationByRoamingUser, 0L, false);
					}
					isolatedStorageFilePermission = IsolatedStorage.s_PermAppUserRoaming;
				}
			}
			else
			{
				if (IsolatedStorage.s_PermAppUser == null)
				{
					IsolatedStorage.s_PermAppUser = new IsolatedStorageFilePermission(IsolatedStorageContainment.ApplicationIsolationByUser, 0L, false);
				}
				isolatedStorageFilePermission = IsolatedStorage.s_PermAppUser;
			}
			isolatedStorageFilePermission.Demand();
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x0005BF14 File Offset: 0x0005A114
		internal static void VerifyScope(IsolatedStorageScope scope)
		{
			if (scope == (IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly) || scope == (IsolatedStorageScope.User | IsolatedStorageScope.Assembly) || scope == (IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly | IsolatedStorageScope.Roaming) || scope == (IsolatedStorageScope.User | IsolatedStorageScope.Assembly | IsolatedStorageScope.Roaming) || scope == (IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly | IsolatedStorageScope.Machine) || scope == (IsolatedStorageScope.Assembly | IsolatedStorageScope.Machine) || scope == (IsolatedStorageScope.User | IsolatedStorageScope.Application) || scope == (IsolatedStorageScope.Machine | IsolatedStorageScope.Application) || scope == (IsolatedStorageScope.User | IsolatedStorageScope.Roaming | IsolatedStorageScope.Application))
			{
				return;
			}
			throw new ArgumentException(Environment.GetResourceString("IsolatedStorage_Scope_Invalid"));
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x0005BF54 File Offset: 0x0005A154
		[SecurityCritical]
		internal virtual void SetQuota(PermissionSet psAllowed, PermissionSet psDenied)
		{
			IsolatedStoragePermission permission = this.GetPermission(psAllowed);
			this.m_Quota = 0UL;
			if (permission != null)
			{
				if (permission.IsUnrestricted())
				{
					this.m_Quota = 9223372036854775807UL;
				}
				else
				{
					this.m_Quota = (ulong)permission.UserQuota;
				}
			}
			if (psDenied != null)
			{
				IsolatedStoragePermission permission2 = this.GetPermission(psDenied);
				if (permission2 != null)
				{
					if (permission2.IsUnrestricted())
					{
						this.m_Quota = 0UL;
					}
					else
					{
						ulong userQuota = (ulong)permission2.UserQuota;
						if (userQuota > this.m_Quota)
						{
							this.m_Quota = 0UL;
						}
						else
						{
							this.m_Quota -= userQuota;
						}
					}
				}
			}
			this.m_ValidQuota = true;
		}

		/// <summary>When overridden in a derived class, removes the individual isolated store and all contained data.</summary>
		// Token: 0x06001B22 RID: 6946
		public abstract void Remove();

		/// <summary>When implemented by a derived class, returns a permission that represents access to isolated storage from within a permission set.</summary>
		/// <param name="ps">The <see cref="T:System.Security.PermissionSet" /> object that contains the set of permissions granted to code attempting to use isolated storage.</param>
		/// <returns>An <see cref="T:System.Security.Permissions.IsolatedStoragePermission" /> object.</returns>
		// Token: 0x06001B23 RID: 6947
		protected abstract IsolatedStoragePermission GetPermission(PermissionSet ps);

		// Token: 0x06001B24 RID: 6948 RVA: 0x0005BFE8 File Offset: 0x0005A1E8
		[SecuritySafeCritical]
		internal static RuntimeAssembly GetCaller()
		{
			RuntimeAssembly runtimeAssembly = null;
			IsolatedStorage.GetCaller(JitHelpers.GetObjectHandleOnStack<RuntimeAssembly>(ref runtimeAssembly));
			return runtimeAssembly;
		}

		// Token: 0x06001B25 RID: 6949
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetCaller(ObjectHandleOnStack retAssembly);

		// Token: 0x0400094B RID: 2379
		internal const IsolatedStorageScope c_Assembly = IsolatedStorageScope.User | IsolatedStorageScope.Assembly;

		// Token: 0x0400094C RID: 2380
		internal const IsolatedStorageScope c_Domain = IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly;

		// Token: 0x0400094D RID: 2381
		internal const IsolatedStorageScope c_AssemblyRoaming = IsolatedStorageScope.User | IsolatedStorageScope.Assembly | IsolatedStorageScope.Roaming;

		// Token: 0x0400094E RID: 2382
		internal const IsolatedStorageScope c_DomainRoaming = IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly | IsolatedStorageScope.Roaming;

		// Token: 0x0400094F RID: 2383
		internal const IsolatedStorageScope c_MachineAssembly = IsolatedStorageScope.Assembly | IsolatedStorageScope.Machine;

		// Token: 0x04000950 RID: 2384
		internal const IsolatedStorageScope c_MachineDomain = IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly | IsolatedStorageScope.Machine;

		// Token: 0x04000951 RID: 2385
		internal const IsolatedStorageScope c_AppUser = IsolatedStorageScope.User | IsolatedStorageScope.Application;

		// Token: 0x04000952 RID: 2386
		internal const IsolatedStorageScope c_AppMachine = IsolatedStorageScope.Machine | IsolatedStorageScope.Application;

		// Token: 0x04000953 RID: 2387
		internal const IsolatedStorageScope c_AppUserRoaming = IsolatedStorageScope.User | IsolatedStorageScope.Roaming | IsolatedStorageScope.Application;

		// Token: 0x04000954 RID: 2388
		private const string s_Publisher = "Publisher";

		// Token: 0x04000955 RID: 2389
		private const string s_StrongName = "StrongName";

		// Token: 0x04000956 RID: 2390
		private const string s_Site = "Site";

		// Token: 0x04000957 RID: 2391
		private const string s_Url = "Url";

		// Token: 0x04000958 RID: 2392
		private const string s_Zone = "Zone";

		// Token: 0x04000959 RID: 2393
		private ulong m_Quota;

		// Token: 0x0400095A RID: 2394
		private bool m_ValidQuota;

		// Token: 0x0400095B RID: 2395
		private object m_DomainIdentity;

		// Token: 0x0400095C RID: 2396
		private object m_AssemIdentity;

		// Token: 0x0400095D RID: 2397
		private object m_AppIdentity;

		// Token: 0x0400095E RID: 2398
		private string m_DomainName;

		// Token: 0x0400095F RID: 2399
		private string m_AssemName;

		// Token: 0x04000960 RID: 2400
		private string m_AppName;

		// Token: 0x04000961 RID: 2401
		private IsolatedStorageScope m_Scope;

		// Token: 0x04000962 RID: 2402
		private static volatile IsolatedStorageFilePermission s_PermDomain;

		// Token: 0x04000963 RID: 2403
		private static volatile IsolatedStorageFilePermission s_PermMachineDomain;

		// Token: 0x04000964 RID: 2404
		private static volatile IsolatedStorageFilePermission s_PermDomainRoaming;

		// Token: 0x04000965 RID: 2405
		private static volatile IsolatedStorageFilePermission s_PermAssem;

		// Token: 0x04000966 RID: 2406
		private static volatile IsolatedStorageFilePermission s_PermMachineAssem;

		// Token: 0x04000967 RID: 2407
		private static volatile IsolatedStorageFilePermission s_PermAssemRoaming;

		// Token: 0x04000968 RID: 2408
		private static volatile IsolatedStorageFilePermission s_PermAppUser;

		// Token: 0x04000969 RID: 2409
		private static volatile IsolatedStorageFilePermission s_PermAppMachine;

		// Token: 0x0400096A RID: 2410
		private static volatile IsolatedStorageFilePermission s_PermAppUserRoaming;

		// Token: 0x0400096B RID: 2411
		private static volatile SecurityPermission s_PermControlEvidence;

		// Token: 0x0400096C RID: 2412
		private static volatile PermissionSet s_PermUnrestricted;
	}
}
