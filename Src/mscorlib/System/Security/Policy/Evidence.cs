using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Hosting;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Threading;

namespace System.Security.Policy
{
	/// <summary>Defines the set of information that constitutes input to security policy decisions. This class cannot be inherited.</summary>
	// Token: 0x0200034B RID: 843
	[ComVisible(true)]
	[Serializable]
	public sealed class Evidence : ICollection, IEnumerable
	{
		/// <summary>Initializes a new empty instance of the <see cref="T:System.Security.Policy.Evidence" /> class.</summary>
		// Token: 0x06002A13 RID: 10771 RVA: 0x0009CD19 File Offset: 0x0009AF19
		public Evidence()
		{
			this.m_evidence = new Dictionary<Type, EvidenceTypeDescriptor>();
			this.m_evidenceLock = new ReaderWriterLock();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.Evidence" /> class from a shallow copy of an existing one.</summary>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> instance from which to create the new instance. This instance is not deep-copied.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="evidence" /> parameter is not a valid instance of <see cref="T:System.Security.Policy.Evidence" />.</exception>
		// Token: 0x06002A14 RID: 10772 RVA: 0x0009CD38 File Offset: 0x0009AF38
		public Evidence(Evidence evidence)
		{
			this.m_evidence = new Dictionary<Type, EvidenceTypeDescriptor>();
			if (evidence != null)
			{
				using (new Evidence.EvidenceLockHolder(evidence, Evidence.EvidenceLockHolder.LockType.Reader))
				{
					foreach (KeyValuePair<Type, EvidenceTypeDescriptor> keyValuePair in evidence.m_evidence)
					{
						EvidenceTypeDescriptor evidenceTypeDescriptor = keyValuePair.Value;
						if (evidenceTypeDescriptor != null)
						{
							evidenceTypeDescriptor = evidenceTypeDescriptor.Clone();
						}
						this.m_evidence[keyValuePair.Key] = evidenceTypeDescriptor;
					}
					this.m_target = evidence.m_target;
					this.m_locked = evidence.m_locked;
					this.m_deserializedTargetEvidence = evidence.m_deserializedTargetEvidence;
					if (evidence.Target != null)
					{
						this.m_cloneOrigin = new WeakReference(evidence);
					}
				}
			}
			this.m_evidenceLock = new ReaderWriterLock();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.Evidence" /> class from multiple sets of host and assembly evidence.</summary>
		/// <param name="hostEvidence">The host evidence from which to create the new instance.</param>
		/// <param name="assemblyEvidence">The assembly evidence from which to create the new instance.</param>
		// Token: 0x06002A15 RID: 10773 RVA: 0x0009CE24 File Offset: 0x0009B024
		[Obsolete("This constructor is obsolete. Please use the constructor which takes arrays of EvidenceBase instead.")]
		public Evidence(object[] hostEvidence, object[] assemblyEvidence)
		{
			this.m_evidence = new Dictionary<Type, EvidenceTypeDescriptor>();
			if (hostEvidence != null)
			{
				foreach (object obj in hostEvidence)
				{
					this.AddHost(obj);
				}
			}
			if (assemblyEvidence != null)
			{
				foreach (object obj2 in assemblyEvidence)
				{
					this.AddAssembly(obj2);
				}
			}
			this.m_evidenceLock = new ReaderWriterLock();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.Evidence" /> class from multiple sets of host and assembly evidence.</summary>
		/// <param name="hostEvidence">The host evidence from which to create the new instance.</param>
		/// <param name="assemblyEvidence">The assembly evidence from which to create the new instance.</param>
		// Token: 0x06002A16 RID: 10774 RVA: 0x0009CE90 File Offset: 0x0009B090
		public Evidence(EvidenceBase[] hostEvidence, EvidenceBase[] assemblyEvidence)
		{
			this.m_evidence = new Dictionary<Type, EvidenceTypeDescriptor>();
			if (hostEvidence != null)
			{
				foreach (EvidenceBase evidenceBase in hostEvidence)
				{
					this.AddHostEvidence(evidenceBase, Evidence.GetEvidenceIndexType(evidenceBase), Evidence.DuplicateEvidenceAction.Throw);
				}
			}
			if (assemblyEvidence != null)
			{
				foreach (EvidenceBase evidenceBase2 in assemblyEvidence)
				{
					this.AddAssemblyEvidence(evidenceBase2, Evidence.GetEvidenceIndexType(evidenceBase2), Evidence.DuplicateEvidenceAction.Throw);
				}
			}
			this.m_evidenceLock = new ReaderWriterLock();
		}

		// Token: 0x06002A17 RID: 10775 RVA: 0x0009CF0C File Offset: 0x0009B10C
		[SecuritySafeCritical]
		internal Evidence(IRuntimeEvidenceFactory target)
		{
			this.m_evidence = new Dictionary<Type, EvidenceTypeDescriptor>();
			this.m_target = target;
			foreach (Type type in Evidence.RuntimeEvidenceTypes)
			{
				this.m_evidence[type] = null;
			}
			this.QueryHostForPossibleEvidenceTypes();
			this.m_evidenceLock = new ReaderWriterLock();
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06002A18 RID: 10776 RVA: 0x0009CF68 File Offset: 0x0009B168
		internal static Type[] RuntimeEvidenceTypes
		{
			get
			{
				if (Evidence.s_runtimeEvidenceTypes == null)
				{
					Type[] array = new Type[]
					{
						typeof(ActivationArguments),
						typeof(ApplicationDirectory),
						typeof(ApplicationTrust),
						typeof(GacInstalled),
						typeof(Hash),
						typeof(Publisher),
						typeof(Site),
						typeof(StrongName),
						typeof(Url),
						typeof(Zone)
					};
					if (AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
					{
						int num = array.Length;
						Array.Resize<Type>(ref array, num + 1);
						array[num] = typeof(PermissionRequestEvidence);
					}
					Evidence.s_runtimeEvidenceTypes = array;
				}
				return Evidence.s_runtimeEvidenceTypes;
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06002A19 RID: 10777 RVA: 0x0009D042 File Offset: 0x0009B242
		private bool IsReaderLockHeld
		{
			get
			{
				return this.m_evidenceLock == null || this.m_evidenceLock.IsReaderLockHeld;
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06002A1A RID: 10778 RVA: 0x0009D059 File Offset: 0x0009B259
		private bool IsWriterLockHeld
		{
			get
			{
				return this.m_evidenceLock == null || this.m_evidenceLock.IsWriterLockHeld;
			}
		}

		// Token: 0x06002A1B RID: 10779 RVA: 0x0009D070 File Offset: 0x0009B270
		private void AcquireReaderLock()
		{
			if (this.m_evidenceLock != null)
			{
				this.m_evidenceLock.AcquireReaderLock(5000);
			}
		}

		// Token: 0x06002A1C RID: 10780 RVA: 0x0009D08A File Offset: 0x0009B28A
		private void AcquireWriterlock()
		{
			if (this.m_evidenceLock != null)
			{
				this.m_evidenceLock.AcquireWriterLock(5000);
			}
		}

		// Token: 0x06002A1D RID: 10781 RVA: 0x0009D0A4 File Offset: 0x0009B2A4
		private void DowngradeFromWriterLock(ref LockCookie lockCookie)
		{
			if (this.m_evidenceLock != null)
			{
				this.m_evidenceLock.DowngradeFromWriterLock(ref lockCookie);
			}
		}

		// Token: 0x06002A1E RID: 10782 RVA: 0x0009D0BC File Offset: 0x0009B2BC
		private LockCookie UpgradeToWriterLock()
		{
			if (this.m_evidenceLock == null)
			{
				return default(LockCookie);
			}
			return this.m_evidenceLock.UpgradeToWriterLock(5000);
		}

		// Token: 0x06002A1F RID: 10783 RVA: 0x0009D0EB File Offset: 0x0009B2EB
		private void ReleaseReaderLock()
		{
			if (this.m_evidenceLock != null)
			{
				this.m_evidenceLock.ReleaseReaderLock();
			}
		}

		// Token: 0x06002A20 RID: 10784 RVA: 0x0009D100 File Offset: 0x0009B300
		private void ReleaseWriterLock()
		{
			if (this.m_evidenceLock != null)
			{
				this.m_evidenceLock.ReleaseWriterLock();
			}
		}

		/// <summary>Adds the specified evidence supplied by the host to the evidence set.</summary>
		/// <param name="id">Any evidence object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="id" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="id" /> is not serializable.</exception>
		// Token: 0x06002A21 RID: 10785 RVA: 0x0009D118 File Offset: 0x0009B318
		[Obsolete("This method is obsolete. Please use AddHostEvidence instead.")]
		[SecuritySafeCritical]
		public void AddHost(object id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (!id.GetType().IsSerializable)
			{
				throw new ArgumentException(Environment.GetResourceString("Policy_EvidenceMustBeSerializable"), "id");
			}
			if (this.m_locked)
			{
				new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Demand();
			}
			EvidenceBase evidenceBase = Evidence.WrapLegacyEvidence(id);
			Type evidenceIndexType = Evidence.GetEvidenceIndexType(evidenceBase);
			this.AddHostEvidence(evidenceBase, evidenceIndexType, Evidence.DuplicateEvidenceAction.Merge);
		}

		/// <summary>Adds the specified assembly evidence to the evidence set.</summary>
		/// <param name="id">Any evidence object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="id" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="id" /> is not serializable.</exception>
		// Token: 0x06002A22 RID: 10786 RVA: 0x0009D180 File Offset: 0x0009B380
		[Obsolete("This method is obsolete. Please use AddAssemblyEvidence instead.")]
		public void AddAssembly(object id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (!id.GetType().IsSerializable)
			{
				throw new ArgumentException(Environment.GetResourceString("Policy_EvidenceMustBeSerializable"), "id");
			}
			EvidenceBase evidenceBase = Evidence.WrapLegacyEvidence(id);
			Type evidenceIndexType = Evidence.GetEvidenceIndexType(evidenceBase);
			this.AddAssemblyEvidence(evidenceBase, evidenceIndexType, Evidence.DuplicateEvidenceAction.Merge);
		}

		/// <summary>Adds an evidence object of the specified type to the assembly-supplied evidence list.</summary>
		/// <param name="evidence">The assembly evidence to add.</param>
		/// <typeparam name="T">The type of the object in <paramref name="evidence" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="evidence" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">Evidence of type <paramref name="T" /> is already in the list.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="evidence" /> is not serializable.</exception>
		// Token: 0x06002A23 RID: 10787 RVA: 0x0009D1D4 File Offset: 0x0009B3D4
		[ComVisible(false)]
		public void AddAssemblyEvidence<T>(T evidence) where T : EvidenceBase
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			Type type = typeof(T);
			if (typeof(T) == typeof(EvidenceBase) || evidence is ILegacyEvidenceAdapter)
			{
				type = Evidence.GetEvidenceIndexType(evidence);
			}
			this.AddAssemblyEvidence(evidence, type, Evidence.DuplicateEvidenceAction.Throw);
		}

		// Token: 0x06002A24 RID: 10788 RVA: 0x0009D244 File Offset: 0x0009B444
		private void AddAssemblyEvidence(EvidenceBase evidence, Type evidenceType, Evidence.DuplicateEvidenceAction duplicateAction)
		{
			using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Writer))
			{
				this.AddAssemblyEvidenceNoLock(evidence, evidenceType, duplicateAction);
			}
		}

		// Token: 0x06002A25 RID: 10789 RVA: 0x0009D280 File Offset: 0x0009B480
		private void AddAssemblyEvidenceNoLock(EvidenceBase evidence, Type evidenceType, Evidence.DuplicateEvidenceAction duplicateAction)
		{
			this.DeserializeTargetEvidence();
			EvidenceTypeDescriptor evidenceTypeDescriptor = this.GetEvidenceTypeDescriptor(evidenceType, true);
			this.m_version += 1U;
			if (evidenceTypeDescriptor.AssemblyEvidence == null)
			{
				evidenceTypeDescriptor.AssemblyEvidence = evidence;
				return;
			}
			evidenceTypeDescriptor.AssemblyEvidence = Evidence.HandleDuplicateEvidence(evidenceTypeDescriptor.AssemblyEvidence, evidence, duplicateAction);
		}

		/// <summary>Adds host evidence of the specified type to the host evidence collection.</summary>
		/// <param name="evidence">The host evidence to add.</param>
		/// <typeparam name="T">The type of the object in <paramref name="evidence" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="evidence" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">Evidence of type <paramref name="T" /> is already in the list.</exception>
		// Token: 0x06002A26 RID: 10790 RVA: 0x0009D2D0 File Offset: 0x0009B4D0
		[ComVisible(false)]
		public void AddHostEvidence<T>(T evidence) where T : EvidenceBase
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			Type type = typeof(T);
			if (typeof(T) == typeof(EvidenceBase) || evidence is ILegacyEvidenceAdapter)
			{
				type = Evidence.GetEvidenceIndexType(evidence);
			}
			this.AddHostEvidence(evidence, type, Evidence.DuplicateEvidenceAction.Throw);
		}

		// Token: 0x06002A27 RID: 10791 RVA: 0x0009D340 File Offset: 0x0009B540
		[SecuritySafeCritical]
		private void AddHostEvidence(EvidenceBase evidence, Type evidenceType, Evidence.DuplicateEvidenceAction duplicateAction)
		{
			if (this.Locked)
			{
				new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Demand();
			}
			using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Writer))
			{
				this.AddHostEvidenceNoLock(evidence, evidenceType, duplicateAction);
			}
		}

		// Token: 0x06002A28 RID: 10792 RVA: 0x0009D390 File Offset: 0x0009B590
		private void AddHostEvidenceNoLock(EvidenceBase evidence, Type evidenceType, Evidence.DuplicateEvidenceAction duplicateAction)
		{
			EvidenceTypeDescriptor evidenceTypeDescriptor = this.GetEvidenceTypeDescriptor(evidenceType, true);
			this.m_version += 1U;
			if (evidenceTypeDescriptor.HostEvidence == null)
			{
				evidenceTypeDescriptor.HostEvidence = evidence;
				return;
			}
			evidenceTypeDescriptor.HostEvidence = Evidence.HandleDuplicateEvidence(evidenceTypeDescriptor.HostEvidence, evidence, duplicateAction);
		}

		// Token: 0x06002A29 RID: 10793 RVA: 0x0009D3D8 File Offset: 0x0009B5D8
		[SecurityCritical]
		private void QueryHostForPossibleEvidenceTypes()
		{
			if (AppDomain.CurrentDomain.DomainManager != null)
			{
				HostSecurityManager hostSecurityManager = AppDomain.CurrentDomain.DomainManager.HostSecurityManager;
				if (hostSecurityManager != null)
				{
					Type[] array = null;
					AppDomain appDomain = this.m_target.Target as AppDomain;
					Assembly assembly = this.m_target.Target as Assembly;
					if (assembly != null && (hostSecurityManager.Flags & HostSecurityManagerOptions.HostAssemblyEvidence) == HostSecurityManagerOptions.HostAssemblyEvidence)
					{
						array = hostSecurityManager.GetHostSuppliedAssemblyEvidenceTypes(assembly);
					}
					else if (appDomain != null && (hostSecurityManager.Flags & HostSecurityManagerOptions.HostAppDomainEvidence) == HostSecurityManagerOptions.HostAppDomainEvidence)
					{
						array = hostSecurityManager.GetHostSuppliedAppDomainEvidenceTypes();
					}
					if (array != null)
					{
						foreach (Type type in array)
						{
							EvidenceTypeDescriptor evidenceTypeDescriptor = this.GetEvidenceTypeDescriptor(type, true);
							evidenceTypeDescriptor.HostCanGenerate = true;
						}
					}
				}
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06002A2A RID: 10794 RVA: 0x0009D494 File Offset: 0x0009B694
		internal bool IsUnmodified
		{
			get
			{
				return this.m_version == 0U;
			}
		}

		/// <summary>Gets or sets a value indicating whether the evidence is locked.</summary>
		/// <returns>
		///   <see langword="true" /> if the evidence is locked; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06002A2B RID: 10795 RVA: 0x0009D49F File Offset: 0x0009B69F
		// (set) Token: 0x06002A2C RID: 10796 RVA: 0x0009D4A7 File Offset: 0x0009B6A7
		public bool Locked
		{
			get
			{
				return this.m_locked;
			}
			[SecuritySafeCritical]
			set
			{
				if (!value)
				{
					new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Demand();
					this.m_locked = false;
					return;
				}
				this.m_locked = true;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06002A2D RID: 10797 RVA: 0x0009D4C7 File Offset: 0x0009B6C7
		// (set) Token: 0x06002A2E RID: 10798 RVA: 0x0009D4D0 File Offset: 0x0009B6D0
		internal IRuntimeEvidenceFactory Target
		{
			get
			{
				return this.m_target;
			}
			[SecurityCritical]
			set
			{
				using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Writer))
				{
					this.m_target = value;
					this.QueryHostForPossibleEvidenceTypes();
				}
			}
		}

		// Token: 0x06002A2F RID: 10799 RVA: 0x0009D510 File Offset: 0x0009B710
		private static Type GetEvidenceIndexType(EvidenceBase evidence)
		{
			ILegacyEvidenceAdapter legacyEvidenceAdapter = evidence as ILegacyEvidenceAdapter;
			if (legacyEvidenceAdapter != null)
			{
				return legacyEvidenceAdapter.EvidenceType;
			}
			return evidence.GetType();
		}

		// Token: 0x06002A30 RID: 10800 RVA: 0x0009D534 File Offset: 0x0009B734
		internal EvidenceTypeDescriptor GetEvidenceTypeDescriptor(Type evidenceType)
		{
			return this.GetEvidenceTypeDescriptor(evidenceType, false);
		}

		// Token: 0x06002A31 RID: 10801 RVA: 0x0009D540 File Offset: 0x0009B740
		private EvidenceTypeDescriptor GetEvidenceTypeDescriptor(Type evidenceType, bool addIfNotExist)
		{
			EvidenceTypeDescriptor evidenceTypeDescriptor = null;
			if (!this.m_evidence.TryGetValue(evidenceType, out evidenceTypeDescriptor) && !addIfNotExist)
			{
				return null;
			}
			if (evidenceTypeDescriptor == null)
			{
				evidenceTypeDescriptor = new EvidenceTypeDescriptor();
				bool flag = false;
				LockCookie lockCookie = default(LockCookie);
				try
				{
					if (!this.IsWriterLockHeld)
					{
						lockCookie = this.UpgradeToWriterLock();
						flag = true;
					}
					this.m_evidence[evidenceType] = evidenceTypeDescriptor;
				}
				finally
				{
					if (flag)
					{
						this.DowngradeFromWriterLock(ref lockCookie);
					}
				}
			}
			return evidenceTypeDescriptor;
		}

		// Token: 0x06002A32 RID: 10802 RVA: 0x0009D5B4 File Offset: 0x0009B7B4
		private static EvidenceBase HandleDuplicateEvidence(EvidenceBase original, EvidenceBase duplicate, Evidence.DuplicateEvidenceAction action)
		{
			switch (action)
			{
			case Evidence.DuplicateEvidenceAction.Throw:
				throw new InvalidOperationException(Environment.GetResourceString("Policy_DuplicateEvidence", new object[] { duplicate.GetType().FullName }));
			case Evidence.DuplicateEvidenceAction.Merge:
			{
				LegacyEvidenceList legacyEvidenceList = original as LegacyEvidenceList;
				if (legacyEvidenceList == null)
				{
					legacyEvidenceList = new LegacyEvidenceList();
					legacyEvidenceList.Add(original);
				}
				legacyEvidenceList.Add(duplicate);
				return legacyEvidenceList;
			}
			case Evidence.DuplicateEvidenceAction.SelectNewObject:
				return duplicate;
			default:
				return null;
			}
		}

		// Token: 0x06002A33 RID: 10803 RVA: 0x0009D61C File Offset: 0x0009B81C
		private static EvidenceBase WrapLegacyEvidence(object evidence)
		{
			EvidenceBase evidenceBase = evidence as EvidenceBase;
			if (evidenceBase == null)
			{
				evidenceBase = new LegacyEvidenceWrapper(evidence);
			}
			return evidenceBase;
		}

		// Token: 0x06002A34 RID: 10804 RVA: 0x0009D63C File Offset: 0x0009B83C
		private static object UnwrapEvidence(EvidenceBase evidence)
		{
			ILegacyEvidenceAdapter legacyEvidenceAdapter = evidence as ILegacyEvidenceAdapter;
			if (legacyEvidenceAdapter != null)
			{
				return legacyEvidenceAdapter.EvidenceObject;
			}
			return evidence;
		}

		/// <summary>Merges the specified evidence set into the current evidence set.</summary>
		/// <param name="evidence">The evidence set to be merged into the current evidence set.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="evidence" /> parameter is not a valid instance of <see cref="T:System.Security.Policy.Evidence" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="P:System.Security.Policy.Evidence.Locked" /> is <see langword="true" />, the code that calls this method does not have <see cref="F:System.Security.Permissions.SecurityPermissionFlag.ControlEvidence" />, and the <paramref name="evidence" /> parameter has a host list that is not empty.</exception>
		// Token: 0x06002A35 RID: 10805 RVA: 0x0009D65C File Offset: 0x0009B85C
		[SecuritySafeCritical]
		public void Merge(Evidence evidence)
		{
			if (evidence == null)
			{
				return;
			}
			using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Writer))
			{
				bool flag = false;
				IEnumerator hostEnumerator = evidence.GetHostEnumerator();
				while (hostEnumerator.MoveNext())
				{
					if (this.Locked && !flag)
					{
						new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Demand();
						flag = true;
					}
					Type type = hostEnumerator.Current.GetType();
					if (this.m_evidence.ContainsKey(type))
					{
						this.GetHostEvidenceNoLock(type);
					}
					EvidenceBase evidenceBase = Evidence.WrapLegacyEvidence(hostEnumerator.Current);
					this.AddHostEvidenceNoLock(evidenceBase, Evidence.GetEvidenceIndexType(evidenceBase), Evidence.DuplicateEvidenceAction.Merge);
				}
				IEnumerator assemblyEnumerator = evidence.GetAssemblyEnumerator();
				while (assemblyEnumerator.MoveNext())
				{
					object obj = assemblyEnumerator.Current;
					EvidenceBase evidenceBase2 = Evidence.WrapLegacyEvidence(obj);
					this.AddAssemblyEvidenceNoLock(evidenceBase2, Evidence.GetEvidenceIndexType(evidenceBase2), Evidence.DuplicateEvidenceAction.Merge);
				}
			}
		}

		// Token: 0x06002A36 RID: 10806 RVA: 0x0009D730 File Offset: 0x0009B930
		internal void MergeWithNoDuplicates(Evidence evidence)
		{
			if (evidence == null)
			{
				return;
			}
			using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Writer))
			{
				IEnumerator hostEnumerator = evidence.GetHostEnumerator();
				while (hostEnumerator.MoveNext())
				{
					object obj = hostEnumerator.Current;
					EvidenceBase evidenceBase = Evidence.WrapLegacyEvidence(obj);
					this.AddHostEvidenceNoLock(evidenceBase, Evidence.GetEvidenceIndexType(evidenceBase), Evidence.DuplicateEvidenceAction.SelectNewObject);
				}
				IEnumerator assemblyEnumerator = evidence.GetAssemblyEnumerator();
				while (assemblyEnumerator.MoveNext())
				{
					object obj2 = assemblyEnumerator.Current;
					EvidenceBase evidenceBase2 = Evidence.WrapLegacyEvidence(obj2);
					this.AddAssemblyEvidenceNoLock(evidenceBase2, Evidence.GetEvidenceIndexType(evidenceBase2), Evidence.DuplicateEvidenceAction.SelectNewObject);
				}
			}
		}

		// Token: 0x06002A37 RID: 10807 RVA: 0x0009D7C0 File Offset: 0x0009B9C0
		[ComVisible(false)]
		[OnSerializing]
		[SecurityCritical]
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		private void OnSerializing(StreamingContext context)
		{
			using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Reader))
			{
				foreach (Type type in new List<Type>(this.m_evidence.Keys))
				{
					this.GetHostEvidenceNoLock(type);
				}
				this.DeserializeTargetEvidence();
			}
			ArrayList arrayList = new ArrayList();
			IEnumerator hostEnumerator = this.GetHostEnumerator();
			while (hostEnumerator.MoveNext())
			{
				object obj = hostEnumerator.Current;
				arrayList.Add(obj);
			}
			this.m_hostList = arrayList;
			ArrayList arrayList2 = new ArrayList();
			IEnumerator assemblyEnumerator = this.GetAssemblyEnumerator();
			while (assemblyEnumerator.MoveNext())
			{
				object obj2 = assemblyEnumerator.Current;
				arrayList2.Add(obj2);
			}
			this.m_assemblyList = arrayList2;
		}

		// Token: 0x06002A38 RID: 10808 RVA: 0x0009D8A4 File Offset: 0x0009BAA4
		[ComVisible(false)]
		[OnDeserialized]
		[SecurityCritical]
		private void OnDeserialized(StreamingContext context)
		{
			if (this.m_evidence == null)
			{
				this.m_evidence = new Dictionary<Type, EvidenceTypeDescriptor>();
				if (this.m_hostList != null)
				{
					foreach (object obj in this.m_hostList)
					{
						if (obj != null)
						{
							this.AddHost(obj);
						}
					}
					this.m_hostList = null;
				}
				if (this.m_assemblyList != null)
				{
					foreach (object obj2 in this.m_assemblyList)
					{
						if (obj2 != null)
						{
							this.AddAssembly(obj2);
						}
					}
					this.m_assemblyList = null;
				}
			}
			this.m_evidenceLock = new ReaderWriterLock();
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x0009D990 File Offset: 0x0009BB90
		private void DeserializeTargetEvidence()
		{
			if (this.m_target != null && !this.m_deserializedTargetEvidence)
			{
				bool flag = false;
				LockCookie lockCookie = default(LockCookie);
				try
				{
					if (!this.IsWriterLockHeld)
					{
						lockCookie = this.UpgradeToWriterLock();
						flag = true;
					}
					this.m_deserializedTargetEvidence = true;
					foreach (EvidenceBase evidenceBase in this.m_target.GetFactorySuppliedEvidence())
					{
						this.AddAssemblyEvidenceNoLock(evidenceBase, Evidence.GetEvidenceIndexType(evidenceBase), Evidence.DuplicateEvidenceAction.Throw);
					}
				}
				finally
				{
					if (flag)
					{
						this.DowngradeFromWriterLock(ref lockCookie);
					}
				}
			}
		}

		// Token: 0x06002A3A RID: 10810 RVA: 0x0009DA34 File Offset: 0x0009BC34
		[SecurityCritical]
		internal byte[] RawSerialize()
		{
			byte[] array;
			try
			{
				using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Reader))
				{
					Dictionary<Type, EvidenceBase> dictionary = new Dictionary<Type, EvidenceBase>();
					foreach (KeyValuePair<Type, EvidenceTypeDescriptor> keyValuePair in this.m_evidence)
					{
						if (keyValuePair.Value != null && keyValuePair.Value.HostEvidence != null)
						{
							dictionary[keyValuePair.Key] = keyValuePair.Value.HostEvidence;
						}
					}
					using (MemoryStream memoryStream = new MemoryStream())
					{
						BinaryFormatter binaryFormatter = new BinaryFormatter();
						binaryFormatter.Serialize(memoryStream, dictionary);
						array = memoryStream.ToArray();
					}
				}
			}
			catch (SecurityException)
			{
				array = null;
			}
			return array;
		}

		/// <summary>Copies evidence objects to an <see cref="T:System.Array" />.</summary>
		/// <param name="array">The target array to which to copy evidence objects.</param>
		/// <param name="index">The zero-based position in the array to which to begin copying evidence objects.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of the target array.</exception>
		// Token: 0x06002A3B RID: 10811 RVA: 0x0009DB24 File Offset: 0x0009BD24
		[Obsolete("Evidence should not be treated as an ICollection. Please use the GetHostEnumerator and GetAssemblyEnumerator methods rather than using CopyTo.")]
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0 || index > array.Length - this.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			int num = index;
			IEnumerator hostEnumerator = this.GetHostEnumerator();
			while (hostEnumerator.MoveNext())
			{
				object obj = hostEnumerator.Current;
				array.SetValue(obj, num);
				num++;
			}
			IEnumerator assemblyEnumerator = this.GetAssemblyEnumerator();
			while (assemblyEnumerator.MoveNext())
			{
				object obj2 = assemblyEnumerator.Current;
				array.SetValue(obj2, num);
				num++;
			}
		}

		/// <summary>Enumerates evidence supplied by the host.</summary>
		/// <returns>An enumerator for evidence added by the <see cref="M:System.Security.Policy.Evidence.AddHost(System.Object)" /> method.</returns>
		// Token: 0x06002A3C RID: 10812 RVA: 0x0009DBA4 File Offset: 0x0009BDA4
		public IEnumerator GetHostEnumerator()
		{
			IEnumerator enumerator;
			using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Reader))
			{
				enumerator = new Evidence.EvidenceEnumerator(this, Evidence.EvidenceEnumerator.Category.Host);
			}
			return enumerator;
		}

		/// <summary>Enumerates evidence provided by the assembly.</summary>
		/// <returns>An enumerator for evidence added by the <see cref="M:System.Security.Policy.Evidence.AddAssembly(System.Object)" /> method.</returns>
		// Token: 0x06002A3D RID: 10813 RVA: 0x0009DBE0 File Offset: 0x0009BDE0
		public IEnumerator GetAssemblyEnumerator()
		{
			IEnumerator enumerator;
			using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Reader))
			{
				this.DeserializeTargetEvidence();
				enumerator = new Evidence.EvidenceEnumerator(this, Evidence.EvidenceEnumerator.Category.Assembly);
			}
			return enumerator;
		}

		// Token: 0x06002A3E RID: 10814 RVA: 0x0009DC20 File Offset: 0x0009BE20
		internal Evidence.RawEvidenceEnumerator GetRawAssemblyEvidenceEnumerator()
		{
			this.DeserializeTargetEvidence();
			return new Evidence.RawEvidenceEnumerator(this, new List<Type>(this.m_evidence.Keys), false);
		}

		// Token: 0x06002A3F RID: 10815 RVA: 0x0009DC3F File Offset: 0x0009BE3F
		internal Evidence.RawEvidenceEnumerator GetRawHostEvidenceEnumerator()
		{
			return new Evidence.RawEvidenceEnumerator(this, new List<Type>(this.m_evidence.Keys), true);
		}

		/// <summary>Enumerates all evidence in the set, both that provided by the host and that provided by the assembly.</summary>
		/// <returns>An enumerator for evidence added by both the <see cref="M:System.Security.Policy.Evidence.AddHost(System.Object)" /> method and the <see cref="M:System.Security.Policy.Evidence.AddAssembly(System.Object)" /> method.</returns>
		// Token: 0x06002A40 RID: 10816 RVA: 0x0009DC58 File Offset: 0x0009BE58
		[Obsolete("GetEnumerator is obsolete. Please use GetAssemblyEnumerator and GetHostEnumerator instead.")]
		public IEnumerator GetEnumerator()
		{
			IEnumerator enumerator;
			using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Reader))
			{
				enumerator = new Evidence.EvidenceEnumerator(this, Evidence.EvidenceEnumerator.Category.Host | Evidence.EvidenceEnumerator.Category.Assembly);
			}
			return enumerator;
		}

		/// <summary>Gets assembly evidence of the specified type from the collection.</summary>
		/// <typeparam name="T">The type of the evidence to get.</typeparam>
		/// <returns>Evidence of type <paramref name="T" /> in the assembly evidence collection.</returns>
		// Token: 0x06002A41 RID: 10817 RVA: 0x0009DC94 File Offset: 0x0009BE94
		[ComVisible(false)]
		public T GetAssemblyEvidence<T>() where T : EvidenceBase
		{
			return Evidence.UnwrapEvidence(this.GetAssemblyEvidence(typeof(T))) as T;
		}

		// Token: 0x06002A42 RID: 10818 RVA: 0x0009DCB8 File Offset: 0x0009BEB8
		internal EvidenceBase GetAssemblyEvidence(Type type)
		{
			EvidenceBase assemblyEvidenceNoLock;
			using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Reader))
			{
				assemblyEvidenceNoLock = this.GetAssemblyEvidenceNoLock(type);
			}
			return assemblyEvidenceNoLock;
		}

		// Token: 0x06002A43 RID: 10819 RVA: 0x0009DCF4 File Offset: 0x0009BEF4
		private EvidenceBase GetAssemblyEvidenceNoLock(Type type)
		{
			this.DeserializeTargetEvidence();
			EvidenceTypeDescriptor evidenceTypeDescriptor = this.GetEvidenceTypeDescriptor(type);
			if (evidenceTypeDescriptor != null)
			{
				return evidenceTypeDescriptor.AssemblyEvidence;
			}
			return null;
		}

		/// <summary>Gets host evidence of the specified type from the collection.</summary>
		/// <typeparam name="T">The type of the evidence to get.</typeparam>
		/// <returns>Evidence of type <paramref name="T" /> in the host evidence collection.</returns>
		// Token: 0x06002A44 RID: 10820 RVA: 0x0009DD1A File Offset: 0x0009BF1A
		[ComVisible(false)]
		public T GetHostEvidence<T>() where T : EvidenceBase
		{
			return Evidence.UnwrapEvidence(this.GetHostEvidence(typeof(T))) as T;
		}

		// Token: 0x06002A45 RID: 10821 RVA: 0x0009DD3B File Offset: 0x0009BF3B
		internal T GetDelayEvaluatedHostEvidence<T>() where T : EvidenceBase, IDelayEvaluatedEvidence
		{
			return Evidence.UnwrapEvidence(this.GetHostEvidence(typeof(T), false)) as T;
		}

		// Token: 0x06002A46 RID: 10822 RVA: 0x0009DD5D File Offset: 0x0009BF5D
		internal EvidenceBase GetHostEvidence(Type type)
		{
			return this.GetHostEvidence(type, true);
		}

		// Token: 0x06002A47 RID: 10823 RVA: 0x0009DD68 File Offset: 0x0009BF68
		[SecuritySafeCritical]
		private EvidenceBase GetHostEvidence(Type type, bool markDelayEvaluatedEvidenceUsed)
		{
			EvidenceBase evidenceBase;
			using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Reader))
			{
				EvidenceBase hostEvidenceNoLock = this.GetHostEvidenceNoLock(type);
				if (markDelayEvaluatedEvidenceUsed)
				{
					IDelayEvaluatedEvidence delayEvaluatedEvidence = hostEvidenceNoLock as IDelayEvaluatedEvidence;
					if (delayEvaluatedEvidence != null)
					{
						delayEvaluatedEvidence.MarkUsed();
					}
				}
				evidenceBase = hostEvidenceNoLock;
			}
			return evidenceBase;
		}

		// Token: 0x06002A48 RID: 10824 RVA: 0x0009DDB8 File Offset: 0x0009BFB8
		[SecurityCritical]
		private EvidenceBase GetHostEvidenceNoLock(Type type)
		{
			EvidenceTypeDescriptor evidenceTypeDescriptor = this.GetEvidenceTypeDescriptor(type);
			if (evidenceTypeDescriptor == null)
			{
				return null;
			}
			if (evidenceTypeDescriptor.HostEvidence != null)
			{
				return evidenceTypeDescriptor.HostEvidence;
			}
			if (this.m_target != null && !evidenceTypeDescriptor.Generated)
			{
				using (new Evidence.EvidenceUpgradeLockHolder(this))
				{
					evidenceTypeDescriptor.Generated = true;
					EvidenceBase evidenceBase = this.GenerateHostEvidence(type, evidenceTypeDescriptor.HostCanGenerate);
					if (evidenceBase != null)
					{
						evidenceTypeDescriptor.HostEvidence = evidenceBase;
						Evidence evidence = ((this.m_cloneOrigin != null) ? (this.m_cloneOrigin.Target as Evidence) : null);
						if (evidence != null)
						{
							using (new Evidence.EvidenceLockHolder(evidence, Evidence.EvidenceLockHolder.LockType.Writer))
							{
								EvidenceTypeDescriptor evidenceTypeDescriptor2 = evidence.GetEvidenceTypeDescriptor(type);
								if (evidenceTypeDescriptor2 != null && evidenceTypeDescriptor2.HostEvidence == null)
								{
									evidenceTypeDescriptor2.HostEvidence = evidenceBase.Clone();
								}
							}
						}
					}
					return evidenceBase;
				}
			}
			return null;
		}

		// Token: 0x06002A49 RID: 10825 RVA: 0x0009DEA8 File Offset: 0x0009C0A8
		[SecurityCritical]
		private EvidenceBase GenerateHostEvidence(Type type, bool hostCanGenerate)
		{
			if (hostCanGenerate)
			{
				AppDomain appDomain = this.m_target.Target as AppDomain;
				Assembly assembly = this.m_target.Target as Assembly;
				EvidenceBase evidenceBase = null;
				if (appDomain != null)
				{
					evidenceBase = AppDomain.CurrentDomain.HostSecurityManager.GenerateAppDomainEvidence(type);
				}
				else if (assembly != null)
				{
					evidenceBase = AppDomain.CurrentDomain.HostSecurityManager.GenerateAssemblyEvidence(type, assembly);
				}
				if (evidenceBase != null)
				{
					if (!type.IsAssignableFrom(evidenceBase.GetType()))
					{
						string fullName = AppDomain.CurrentDomain.HostSecurityManager.GetType().FullName;
						string fullName2 = evidenceBase.GetType().FullName;
						string fullName3 = type.FullName;
						throw new InvalidOperationException(Environment.GetResourceString("Policy_IncorrectHostEvidence", new object[] { fullName, fullName2, fullName3 }));
					}
					return evidenceBase;
				}
			}
			return this.m_target.GenerateEvidence(type);
		}

		/// <summary>Gets the number of evidence objects in the evidence set.</summary>
		/// <returns>The number of evidence objects in the evidence set.</returns>
		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06002A4A RID: 10826 RVA: 0x0009DF80 File Offset: 0x0009C180
		[Obsolete("Evidence should not be treated as an ICollection. Please use GetHostEnumerator and GetAssemblyEnumerator to iterate over the evidence to collect a count.")]
		public int Count
		{
			get
			{
				int num = 0;
				IEnumerator hostEnumerator = this.GetHostEnumerator();
				while (hostEnumerator.MoveNext())
				{
					num++;
				}
				IEnumerator assemblyEnumerator = this.GetAssemblyEnumerator();
				while (assemblyEnumerator.MoveNext())
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06002A4B RID: 10827 RVA: 0x0009DFBC File Offset: 0x0009C1BC
		[ComVisible(false)]
		internal int RawCount
		{
			get
			{
				int num = 0;
				using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Reader))
				{
					foreach (Type type in new List<Type>(this.m_evidence.Keys))
					{
						EvidenceTypeDescriptor evidenceTypeDescriptor = this.GetEvidenceTypeDescriptor(type);
						if (evidenceTypeDescriptor != null)
						{
							if (evidenceTypeDescriptor.AssemblyEvidence != null)
							{
								num++;
							}
							if (evidenceTypeDescriptor.HostEvidence != null)
							{
								num++;
							}
						}
					}
				}
				return num;
			}
		}

		/// <summary>Gets the synchronization root.</summary>
		/// <returns>Always <see langword="this" /> (<see langword="Me" /> in Visual Basic), because synchronization of evidence sets is not supported.</returns>
		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06002A4C RID: 10828 RVA: 0x0009E05C File Offset: 0x0009C25C
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets a value indicating whether the evidence set is thread-safe.</summary>
		/// <returns>Always <see langword="false" /> because thread-safe evidence sets are not supported.</returns>
		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06002A4D RID: 10829 RVA: 0x0009E05F File Offset: 0x0009C25F
		public bool IsSynchronized
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether the evidence set is read-only.</summary>
		/// <returns>Always <see langword="false" />, because read-only evidence sets are not supported.</returns>
		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06002A4E RID: 10830 RVA: 0x0009E062 File Offset: 0x0009C262
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Returns a duplicate copy of this evidence object.</summary>
		/// <returns>A duplicate copy of this evidence object.</returns>
		// Token: 0x06002A4F RID: 10831 RVA: 0x0009E065 File Offset: 0x0009C265
		[ComVisible(false)]
		public Evidence Clone()
		{
			return new Evidence(this);
		}

		/// <summary>Removes the host and assembly evidence from the evidence set.</summary>
		// Token: 0x06002A50 RID: 10832 RVA: 0x0009E070 File Offset: 0x0009C270
		[ComVisible(false)]
		[SecuritySafeCritical]
		public void Clear()
		{
			if (this.Locked)
			{
				new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Demand();
			}
			using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Writer))
			{
				this.m_version += 1U;
				this.m_evidence.Clear();
			}
		}

		/// <summary>Removes the evidence for a given type from the host and assembly enumerations.</summary>
		/// <param name="t">The type of the evidence to be removed.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="t" /> is null.</exception>
		// Token: 0x06002A51 RID: 10833 RVA: 0x0009E0D0 File Offset: 0x0009C2D0
		[ComVisible(false)]
		[SecuritySafeCritical]
		public void RemoveType(Type t)
		{
			if (t == null)
			{
				throw new ArgumentNullException("t");
			}
			using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Writer))
			{
				EvidenceTypeDescriptor evidenceTypeDescriptor = this.GetEvidenceTypeDescriptor(t);
				if (evidenceTypeDescriptor != null)
				{
					this.m_version += 1U;
					if (this.Locked && (evidenceTypeDescriptor.HostEvidence != null || evidenceTypeDescriptor.HostCanGenerate))
					{
						new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Demand();
					}
					this.m_evidence.Remove(t);
				}
			}
		}

		// Token: 0x06002A52 RID: 10834 RVA: 0x0009E160 File Offset: 0x0009C360
		internal void MarkAllEvidenceAsUsed()
		{
			using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Reader))
			{
				foreach (KeyValuePair<Type, EvidenceTypeDescriptor> keyValuePair in this.m_evidence)
				{
					if (keyValuePair.Value != null)
					{
						IDelayEvaluatedEvidence delayEvaluatedEvidence = keyValuePair.Value.HostEvidence as IDelayEvaluatedEvidence;
						if (delayEvaluatedEvidence != null)
						{
							delayEvaluatedEvidence.MarkUsed();
						}
						IDelayEvaluatedEvidence delayEvaluatedEvidence2 = keyValuePair.Value.AssemblyEvidence as IDelayEvaluatedEvidence;
						if (delayEvaluatedEvidence2 != null)
						{
							delayEvaluatedEvidence2.MarkUsed();
						}
					}
				}
			}
		}

		// Token: 0x06002A53 RID: 10835 RVA: 0x0009E20C File Offset: 0x0009C40C
		private bool WasStrongNameEvidenceUsed()
		{
			bool flag;
			using (new Evidence.EvidenceLockHolder(this, Evidence.EvidenceLockHolder.LockType.Reader))
			{
				EvidenceTypeDescriptor evidenceTypeDescriptor = this.GetEvidenceTypeDescriptor(typeof(StrongName));
				if (evidenceTypeDescriptor != null)
				{
					IDelayEvaluatedEvidence delayEvaluatedEvidence = evidenceTypeDescriptor.HostEvidence as IDelayEvaluatedEvidence;
					flag = delayEvaluatedEvidence != null && delayEvaluatedEvidence.WasUsed;
				}
				else
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x04001139 RID: 4409
		[OptionalField(VersionAdded = 4)]
		private Dictionary<Type, EvidenceTypeDescriptor> m_evidence;

		// Token: 0x0400113A RID: 4410
		[OptionalField(VersionAdded = 4)]
		private bool m_deserializedTargetEvidence;

		// Token: 0x0400113B RID: 4411
		private volatile ArrayList m_hostList;

		// Token: 0x0400113C RID: 4412
		private volatile ArrayList m_assemblyList;

		// Token: 0x0400113D RID: 4413
		[NonSerialized]
		private ReaderWriterLock m_evidenceLock;

		// Token: 0x0400113E RID: 4414
		[NonSerialized]
		private uint m_version;

		// Token: 0x0400113F RID: 4415
		[NonSerialized]
		private IRuntimeEvidenceFactory m_target;

		// Token: 0x04001140 RID: 4416
		private bool m_locked;

		// Token: 0x04001141 RID: 4417
		[NonSerialized]
		private WeakReference m_cloneOrigin;

		// Token: 0x04001142 RID: 4418
		private static volatile Type[] s_runtimeEvidenceTypes;

		// Token: 0x04001143 RID: 4419
		private const int LockTimeout = 5000;

		// Token: 0x02000B54 RID: 2900
		private enum DuplicateEvidenceAction
		{
			// Token: 0x04003418 RID: 13336
			Throw,
			// Token: 0x04003419 RID: 13337
			Merge,
			// Token: 0x0400341A RID: 13338
			SelectNewObject
		}

		// Token: 0x02000B55 RID: 2901
		private class EvidenceLockHolder : IDisposable
		{
			// Token: 0x06006BFC RID: 27644 RVA: 0x00176DB3 File Offset: 0x00174FB3
			public EvidenceLockHolder(Evidence target, Evidence.EvidenceLockHolder.LockType lockType)
			{
				this.m_target = target;
				this.m_lockType = lockType;
				if (this.m_lockType == Evidence.EvidenceLockHolder.LockType.Reader)
				{
					this.m_target.AcquireReaderLock();
					return;
				}
				this.m_target.AcquireWriterlock();
			}

			// Token: 0x06006BFD RID: 27645 RVA: 0x00176DE8 File Offset: 0x00174FE8
			public void Dispose()
			{
				if (this.m_lockType == Evidence.EvidenceLockHolder.LockType.Reader && this.m_target.IsReaderLockHeld)
				{
					this.m_target.ReleaseReaderLock();
					return;
				}
				if (this.m_lockType == Evidence.EvidenceLockHolder.LockType.Writer && this.m_target.IsWriterLockHeld)
				{
					this.m_target.ReleaseWriterLock();
				}
			}

			// Token: 0x0400341B RID: 13339
			private Evidence m_target;

			// Token: 0x0400341C RID: 13340
			private Evidence.EvidenceLockHolder.LockType m_lockType;

			// Token: 0x02000CFB RID: 3323
			public enum LockType
			{
				// Token: 0x04003929 RID: 14633
				Reader,
				// Token: 0x0400392A RID: 14634
				Writer
			}
		}

		// Token: 0x02000B56 RID: 2902
		private class EvidenceUpgradeLockHolder : IDisposable
		{
			// Token: 0x06006BFE RID: 27646 RVA: 0x00176E37 File Offset: 0x00175037
			public EvidenceUpgradeLockHolder(Evidence target)
			{
				this.m_target = target;
				this.m_cookie = this.m_target.UpgradeToWriterLock();
			}

			// Token: 0x06006BFF RID: 27647 RVA: 0x00176E57 File Offset: 0x00175057
			public void Dispose()
			{
				if (this.m_target.IsWriterLockHeld)
				{
					this.m_target.DowngradeFromWriterLock(ref this.m_cookie);
				}
			}

			// Token: 0x0400341D RID: 13341
			private Evidence m_target;

			// Token: 0x0400341E RID: 13342
			private LockCookie m_cookie;
		}

		// Token: 0x02000B57 RID: 2903
		internal sealed class RawEvidenceEnumerator : IEnumerator<EvidenceBase>, IDisposable, IEnumerator
		{
			// Token: 0x06006C00 RID: 27648 RVA: 0x00176E77 File Offset: 0x00175077
			public RawEvidenceEnumerator(Evidence evidence, IEnumerable<Type> evidenceTypes, bool hostEnumerator)
			{
				this.m_evidence = evidence;
				this.m_hostEnumerator = hostEnumerator;
				this.m_evidenceTypes = Evidence.RawEvidenceEnumerator.GenerateEvidenceTypes(evidence, evidenceTypes, hostEnumerator);
				this.m_evidenceVersion = evidence.m_version;
				this.Reset();
			}

			// Token: 0x17001235 RID: 4661
			// (get) Token: 0x06006C01 RID: 27649 RVA: 0x00176EAD File Offset: 0x001750AD
			public EvidenceBase Current
			{
				get
				{
					if (this.m_evidence.m_version != this.m_evidenceVersion)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
					}
					return this.m_currentEvidence;
				}
			}

			// Token: 0x17001236 RID: 4662
			// (get) Token: 0x06006C02 RID: 27650 RVA: 0x00176ED8 File Offset: 0x001750D8
			object IEnumerator.Current
			{
				get
				{
					if (this.m_evidence.m_version != this.m_evidenceVersion)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
					}
					return this.m_currentEvidence;
				}
			}

			// Token: 0x17001237 RID: 4663
			// (get) Token: 0x06006C03 RID: 27651 RVA: 0x00176F04 File Offset: 0x00175104
			private static List<Type> ExpensiveEvidence
			{
				get
				{
					if (Evidence.RawEvidenceEnumerator.s_expensiveEvidence == null)
					{
						Evidence.RawEvidenceEnumerator.s_expensiveEvidence = new List<Type>
						{
							typeof(Hash),
							typeof(Publisher)
						};
					}
					return Evidence.RawEvidenceEnumerator.s_expensiveEvidence;
				}
			}

			// Token: 0x06006C04 RID: 27652 RVA: 0x00176F4F File Offset: 0x0017514F
			public void Dispose()
			{
			}

			// Token: 0x06006C05 RID: 27653 RVA: 0x00176F54 File Offset: 0x00175154
			private static Type[] GenerateEvidenceTypes(Evidence evidence, IEnumerable<Type> evidenceTypes, bool hostEvidence)
			{
				List<Type> list = new List<Type>();
				List<Type> list2 = new List<Type>();
				List<Type> list3 = new List<Type>(Evidence.RawEvidenceEnumerator.ExpensiveEvidence.Count);
				foreach (Type type in evidenceTypes)
				{
					EvidenceTypeDescriptor evidenceTypeDescriptor = evidence.GetEvidenceTypeDescriptor(type);
					bool flag = (hostEvidence && evidenceTypeDescriptor.HostEvidence != null) || (!hostEvidence && evidenceTypeDescriptor.AssemblyEvidence != null);
					if (flag)
					{
						list.Add(type);
					}
					else if (Evidence.RawEvidenceEnumerator.ExpensiveEvidence.Contains(type))
					{
						list3.Add(type);
					}
					else
					{
						list2.Add(type);
					}
				}
				Type[] array = new Type[list.Count + list2.Count + list3.Count];
				list.CopyTo(array, 0);
				list2.CopyTo(array, list.Count);
				list3.CopyTo(array, list.Count + list2.Count);
				return array;
			}

			// Token: 0x06006C06 RID: 27654 RVA: 0x00177054 File Offset: 0x00175254
			[SecuritySafeCritical]
			public bool MoveNext()
			{
				using (new Evidence.EvidenceLockHolder(this.m_evidence, Evidence.EvidenceLockHolder.LockType.Reader))
				{
					if (this.m_evidence.m_version != this.m_evidenceVersion)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
					}
					this.m_currentEvidence = null;
					do
					{
						this.m_typeIndex++;
						if (this.m_typeIndex < this.m_evidenceTypes.Length)
						{
							if (this.m_hostEnumerator)
							{
								this.m_currentEvidence = this.m_evidence.GetHostEvidenceNoLock(this.m_evidenceTypes[this.m_typeIndex]);
							}
							else
							{
								this.m_currentEvidence = this.m_evidence.GetAssemblyEvidenceNoLock(this.m_evidenceTypes[this.m_typeIndex]);
							}
						}
					}
					while (this.m_typeIndex < this.m_evidenceTypes.Length && this.m_currentEvidence == null);
				}
				return this.m_currentEvidence != null;
			}

			// Token: 0x06006C07 RID: 27655 RVA: 0x0017713C File Offset: 0x0017533C
			public void Reset()
			{
				if (this.m_evidence.m_version != this.m_evidenceVersion)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
				this.m_typeIndex = -1;
				this.m_currentEvidence = null;
			}

			// Token: 0x0400341F RID: 13343
			private Evidence m_evidence;

			// Token: 0x04003420 RID: 13344
			private bool m_hostEnumerator;

			// Token: 0x04003421 RID: 13345
			private uint m_evidenceVersion;

			// Token: 0x04003422 RID: 13346
			private Type[] m_evidenceTypes;

			// Token: 0x04003423 RID: 13347
			private int m_typeIndex;

			// Token: 0x04003424 RID: 13348
			private EvidenceBase m_currentEvidence;

			// Token: 0x04003425 RID: 13349
			private static volatile List<Type> s_expensiveEvidence;
		}

		// Token: 0x02000B58 RID: 2904
		private sealed class EvidenceEnumerator : IEnumerator
		{
			// Token: 0x06006C08 RID: 27656 RVA: 0x0017716F File Offset: 0x0017536F
			internal EvidenceEnumerator(Evidence evidence, Evidence.EvidenceEnumerator.Category category)
			{
				this.m_evidence = evidence;
				this.m_category = category;
				this.ResetNoLock();
			}

			// Token: 0x06006C09 RID: 27657 RVA: 0x0017718C File Offset: 0x0017538C
			public bool MoveNext()
			{
				IEnumerator currentEnumerator = this.CurrentEnumerator;
				if (currentEnumerator == null)
				{
					this.m_currentEvidence = null;
					return false;
				}
				if (currentEnumerator.MoveNext())
				{
					LegacyEvidenceWrapper legacyEvidenceWrapper = currentEnumerator.Current as LegacyEvidenceWrapper;
					LegacyEvidenceList legacyEvidenceList = currentEnumerator.Current as LegacyEvidenceList;
					if (legacyEvidenceWrapper != null)
					{
						this.m_currentEvidence = legacyEvidenceWrapper.EvidenceObject;
					}
					else if (legacyEvidenceList != null)
					{
						IEnumerator enumerator = legacyEvidenceList.GetEnumerator();
						this.m_enumerators.Push(enumerator);
						this.MoveNext();
					}
					else
					{
						this.m_currentEvidence = currentEnumerator.Current;
					}
					return true;
				}
				this.m_enumerators.Pop();
				return this.MoveNext();
			}

			// Token: 0x17001238 RID: 4664
			// (get) Token: 0x06006C0A RID: 27658 RVA: 0x0017721C File Offset: 0x0017541C
			public object Current
			{
				get
				{
					return this.m_currentEvidence;
				}
			}

			// Token: 0x17001239 RID: 4665
			// (get) Token: 0x06006C0B RID: 27659 RVA: 0x00177224 File Offset: 0x00175424
			private IEnumerator CurrentEnumerator
			{
				get
				{
					if (this.m_enumerators.Count <= 0)
					{
						return null;
					}
					return this.m_enumerators.Peek() as IEnumerator;
				}
			}

			// Token: 0x06006C0C RID: 27660 RVA: 0x00177248 File Offset: 0x00175448
			public void Reset()
			{
				using (new Evidence.EvidenceLockHolder(this.m_evidence, Evidence.EvidenceLockHolder.LockType.Reader))
				{
					this.ResetNoLock();
				}
			}

			// Token: 0x06006C0D RID: 27661 RVA: 0x00177284 File Offset: 0x00175484
			private void ResetNoLock()
			{
				this.m_currentEvidence = null;
				this.m_enumerators = new Stack();
				if ((this.m_category & Evidence.EvidenceEnumerator.Category.Host) == Evidence.EvidenceEnumerator.Category.Host)
				{
					this.m_enumerators.Push(this.m_evidence.GetRawHostEvidenceEnumerator());
				}
				if ((this.m_category & Evidence.EvidenceEnumerator.Category.Assembly) == Evidence.EvidenceEnumerator.Category.Assembly)
				{
					this.m_enumerators.Push(this.m_evidence.GetRawAssemblyEvidenceEnumerator());
				}
			}

			// Token: 0x04003426 RID: 13350
			private Evidence m_evidence;

			// Token: 0x04003427 RID: 13351
			private Evidence.EvidenceEnumerator.Category m_category;

			// Token: 0x04003428 RID: 13352
			private Stack m_enumerators;

			// Token: 0x04003429 RID: 13353
			private object m_currentEvidence;

			// Token: 0x02000CFC RID: 3324
			[Flags]
			internal enum Category
			{
				// Token: 0x0400392C RID: 14636
				Host = 1,
				// Token: 0x0400392D RID: 14637
				Assembly = 2
			}
		}
	}
}
