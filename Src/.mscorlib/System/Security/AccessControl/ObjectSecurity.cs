using System;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;

namespace System.Security.AccessControl
{
	/// <summary>Provides the ability to control access to objects without direct manipulation of Access Control Lists (ACLs). This class is the abstract base class for the <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> and <see cref="T:System.Security.AccessControl.DirectoryObjectSecurity" /> classes.</summary>
	// Token: 0x02000224 RID: 548
	public abstract class ObjectSecurity
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.ObjectSecurity" /> class.</summary>
		// Token: 0x06001F89 RID: 8073 RVA: 0x0006E318 File Offset: 0x0006C518
		protected ObjectSecurity()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.ObjectSecurity" /> class.</summary>
		/// <param name="isContainer">
		///   <see langword="true" /> if the new <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object is a container object.</param>
		/// <param name="isDS">True if the new <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object is a directory object.</param>
		// Token: 0x06001F8A RID: 8074 RVA: 0x0006E32C File Offset: 0x0006C52C
		protected ObjectSecurity(bool isContainer, bool isDS)
			: this()
		{
			DiscretionaryAcl discretionaryAcl = new DiscretionaryAcl(isContainer, isDS, 5);
			this._securityDescriptor = new CommonSecurityDescriptor(isContainer, isDS, ControlFlags.None, null, null, null, discretionaryAcl);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.ObjectSecurity" /> class.</summary>
		/// <param name="securityDescriptor">The <see cref="T:System.Security.AccessControl.CommonSecurityDescriptor" /> of the new <see cref="T:System.Security.AccessControl.CommonObjectSecurity" /> instance.</param>
		// Token: 0x06001F8B RID: 8075 RVA: 0x0006E35A File Offset: 0x0006C55A
		protected ObjectSecurity(CommonSecurityDescriptor securityDescriptor)
			: this()
		{
			if (securityDescriptor == null)
			{
				throw new ArgumentNullException("securityDescriptor");
			}
			this._securityDescriptor = securityDescriptor;
		}

		// Token: 0x06001F8C RID: 8076 RVA: 0x0006E378 File Offset: 0x0006C578
		private void UpdateWithNewSecurityDescriptor(RawSecurityDescriptor newOne, AccessControlSections includeSections)
		{
			if ((includeSections & AccessControlSections.Owner) != AccessControlSections.None)
			{
				this._ownerModified = true;
				this._securityDescriptor.Owner = newOne.Owner;
			}
			if ((includeSections & AccessControlSections.Group) != AccessControlSections.None)
			{
				this._groupModified = true;
				this._securityDescriptor.Group = newOne.Group;
			}
			if ((includeSections & AccessControlSections.Audit) != AccessControlSections.None)
			{
				this._saclModified = true;
				if (newOne.SystemAcl != null)
				{
					this._securityDescriptor.SystemAcl = new SystemAcl(this.IsContainer, this.IsDS, newOne.SystemAcl, true);
				}
				else
				{
					this._securityDescriptor.SystemAcl = null;
				}
				this._securityDescriptor.UpdateControlFlags(ObjectSecurity.SACL_CONTROL_FLAGS, newOne.ControlFlags & ObjectSecurity.SACL_CONTROL_FLAGS);
			}
			if ((includeSections & AccessControlSections.Access) != AccessControlSections.None)
			{
				this._daclModified = true;
				if (newOne.DiscretionaryAcl != null)
				{
					this._securityDescriptor.DiscretionaryAcl = new DiscretionaryAcl(this.IsContainer, this.IsDS, newOne.DiscretionaryAcl, true);
				}
				else
				{
					this._securityDescriptor.DiscretionaryAcl = null;
				}
				ControlFlags controlFlags = this._securityDescriptor.ControlFlags & ControlFlags.DiscretionaryAclPresent;
				this._securityDescriptor.UpdateControlFlags(ObjectSecurity.DACL_CONTROL_FLAGS, (newOne.ControlFlags | controlFlags) & ObjectSecurity.DACL_CONTROL_FLAGS);
			}
		}

		/// <summary>Locks this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object for read access.</summary>
		// Token: 0x06001F8D RID: 8077 RVA: 0x0006E491 File Offset: 0x0006C691
		protected void ReadLock()
		{
			this._lock.AcquireReaderLock(-1);
		}

		/// <summary>Unlocks this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object for read access.</summary>
		// Token: 0x06001F8E RID: 8078 RVA: 0x0006E49F File Offset: 0x0006C69F
		protected void ReadUnlock()
		{
			this._lock.ReleaseReaderLock();
		}

		/// <summary>Locks this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object for write access.</summary>
		// Token: 0x06001F8F RID: 8079 RVA: 0x0006E4AC File Offset: 0x0006C6AC
		protected void WriteLock()
		{
			this._lock.AcquireWriterLock(-1);
		}

		/// <summary>Unlocks this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object for write access.</summary>
		// Token: 0x06001F90 RID: 8080 RVA: 0x0006E4BA File Offset: 0x0006C6BA
		protected void WriteUnlock()
		{
			this._lock.ReleaseWriterLock();
		}

		/// <summary>Gets or sets a Boolean value that specifies whether the owner of the securable object has been modified.</summary>
		/// <returns>
		///   <see langword="true" /> if the owner of the securable object has been modified; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06001F91 RID: 8081 RVA: 0x0006E4C7 File Offset: 0x0006C6C7
		// (set) Token: 0x06001F92 RID: 8082 RVA: 0x0006E4F9 File Offset: 0x0006C6F9
		protected bool OwnerModified
		{
			get
			{
				if (!this._lock.IsReaderLockHeld && !this._lock.IsWriterLockHeld)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustLockForReadOrWrite"));
				}
				return this._ownerModified;
			}
			set
			{
				if (!this._lock.IsWriterLockHeld)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustLockForWrite"));
				}
				this._ownerModified = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that specifies whether the group associated with the securable object has been modified.</summary>
		/// <returns>
		///   <see langword="true" /> if the group associated with the securable object has been modified; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06001F93 RID: 8083 RVA: 0x0006E51F File Offset: 0x0006C71F
		// (set) Token: 0x06001F94 RID: 8084 RVA: 0x0006E551 File Offset: 0x0006C751
		protected bool GroupModified
		{
			get
			{
				if (!this._lock.IsReaderLockHeld && !this._lock.IsWriterLockHeld)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustLockForReadOrWrite"));
				}
				return this._groupModified;
			}
			set
			{
				if (!this._lock.IsWriterLockHeld)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustLockForWrite"));
				}
				this._groupModified = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that specifies whether the audit rules associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object have been modified.</summary>
		/// <returns>
		///   <see langword="true" /> if the audit rules associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object have been modified; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06001F95 RID: 8085 RVA: 0x0006E577 File Offset: 0x0006C777
		// (set) Token: 0x06001F96 RID: 8086 RVA: 0x0006E5A9 File Offset: 0x0006C7A9
		protected bool AuditRulesModified
		{
			get
			{
				if (!this._lock.IsReaderLockHeld && !this._lock.IsWriterLockHeld)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustLockForReadOrWrite"));
				}
				return this._saclModified;
			}
			set
			{
				if (!this._lock.IsWriterLockHeld)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustLockForWrite"));
				}
				this._saclModified = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that specifies whether the access rules associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object have been modified.</summary>
		/// <returns>
		///   <see langword="true" /> if the access rules associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object have been modified; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06001F97 RID: 8087 RVA: 0x0006E5CF File Offset: 0x0006C7CF
		// (set) Token: 0x06001F98 RID: 8088 RVA: 0x0006E601 File Offset: 0x0006C801
		protected bool AccessRulesModified
		{
			get
			{
				if (!this._lock.IsReaderLockHeld && !this._lock.IsWriterLockHeld)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustLockForReadOrWrite"));
				}
				return this._daclModified;
			}
			set
			{
				if (!this._lock.IsWriterLockHeld)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustLockForWrite"));
				}
				this._daclModified = value;
			}
		}

		/// <summary>Gets a Boolean value that specifies whether this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object is a container object.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object is a container object; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06001F99 RID: 8089 RVA: 0x0006E627 File Offset: 0x0006C827
		protected bool IsContainer
		{
			get
			{
				return this._securityDescriptor.IsContainer;
			}
		}

		/// <summary>Gets a Boolean value that specifies whether this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object is a directory object.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object is a directory object; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06001F9A RID: 8090 RVA: 0x0006E634 File Offset: 0x0006C834
		protected bool IsDS
		{
			get
			{
				return this._securityDescriptor.IsDS;
			}
		}

		/// <summary>Saves the specified sections of the security descriptor associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object to permanent storage. We recommend that the values of the <paramref name="includeSections" /> parameters passed to the constructor and persist methods be identical.</summary>
		/// <param name="name">The name used to retrieve the persisted information.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> enumeration values that specifies the sections of the security descriptor (access rules, audit rules, owner, primary group) of the securable object to save.</param>
		// Token: 0x06001F9B RID: 8091 RVA: 0x0006E641 File Offset: 0x0006C841
		protected virtual void Persist(string name, AccessControlSections includeSections)
		{
			throw new NotImplementedException();
		}

		/// <summary>Saves the specified sections of the security descriptor associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object to permanent storage. We recommend that the values of the <paramref name="includeSections" /> parameters passed to the constructor and persist methods be identical.</summary>
		/// <param name="enableOwnershipPrivilege">
		///   <see langword="true" /> to enable the privilege that allows the caller to take ownership of the object.</param>
		/// <param name="name">The name used to retrieve the persisted information.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> enumeration values that specifies the sections of the security descriptor (access rules, audit rules, owner, primary group) of the securable object to save.</param>
		// Token: 0x06001F9C RID: 8092 RVA: 0x0006E648 File Offset: 0x0006C848
		[SecuritySafeCritical]
		[HandleProcessCorruptedStateExceptions]
		protected virtual void Persist(bool enableOwnershipPrivilege, string name, AccessControlSections includeSections)
		{
			Privilege privilege = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (enableOwnershipPrivilege)
				{
					privilege = new Privilege("SeTakeOwnershipPrivilege");
					try
					{
						privilege.Enable();
					}
					catch (PrivilegeNotHeldException)
					{
					}
				}
				this.Persist(name, includeSections);
			}
			catch
			{
				if (privilege != null)
				{
					privilege.Revert();
				}
				throw;
			}
			finally
			{
				if (privilege != null)
				{
					privilege.Revert();
				}
			}
		}

		/// <summary>Saves the specified sections of the security descriptor associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object to permanent storage. We recommend that the values of the <paramref name="includeSections" /> parameters passed to the constructor and persist methods be identical.</summary>
		/// <param name="handle">The handle used to retrieve the persisted information.</param>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> enumeration values that specifies the sections of the security descriptor (access rules, audit rules, owner, primary group) of the securable object to save.</param>
		// Token: 0x06001F9D RID: 8093 RVA: 0x0006E6C0 File Offset: 0x0006C8C0
		[SecuritySafeCritical]
		protected virtual void Persist(SafeHandle handle, AccessControlSections includeSections)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the owner associated with the specified primary group.</summary>
		/// <param name="targetType">The primary group for which to get the owner.</param>
		/// <returns>The owner associated with the specified group.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="targetType" /> is not an <see cref="T:System.Security.Principal.IdentityReference" /> type.</exception>
		/// <exception cref="T:System.Security.Principal.IdentityNotMappedException">Some or all identity references could not be translated.</exception>
		/// <exception cref="T:System.SystemException">A Win32 error code was returned.</exception>
		// Token: 0x06001F9E RID: 8094 RVA: 0x0006E6C8 File Offset: 0x0006C8C8
		public IdentityReference GetOwner(Type targetType)
		{
			this.ReadLock();
			IdentityReference identityReference;
			try
			{
				if (this._securityDescriptor.Owner == null)
				{
					identityReference = null;
				}
				else
				{
					identityReference = this._securityDescriptor.Owner.Translate(targetType);
				}
			}
			finally
			{
				this.ReadUnlock();
			}
			return identityReference;
		}

		/// <summary>Sets the owner for the security descriptor associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</summary>
		/// <param name="identity">The owner to set.</param>
		// Token: 0x06001F9F RID: 8095 RVA: 0x0006E720 File Offset: 0x0006C920
		public void SetOwner(IdentityReference identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this.WriteLock();
			try
			{
				this._securityDescriptor.Owner = identity.Translate(typeof(SecurityIdentifier)) as SecurityIdentifier;
				this._ownerModified = true;
			}
			finally
			{
				this.WriteUnlock();
			}
		}

		/// <summary>Gets the primary group associated with the specified owner.</summary>
		/// <param name="targetType">The owner for which to get the primary group.</param>
		/// <returns>The primary group associated with the specified owner.</returns>
		// Token: 0x06001FA0 RID: 8096 RVA: 0x0006E788 File Offset: 0x0006C988
		public IdentityReference GetGroup(Type targetType)
		{
			this.ReadLock();
			IdentityReference identityReference;
			try
			{
				if (this._securityDescriptor.Group == null)
				{
					identityReference = null;
				}
				else
				{
					identityReference = this._securityDescriptor.Group.Translate(targetType);
				}
			}
			finally
			{
				this.ReadUnlock();
			}
			return identityReference;
		}

		/// <summary>Sets the primary group for the security descriptor associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</summary>
		/// <param name="identity">The primary group to set.</param>
		// Token: 0x06001FA1 RID: 8097 RVA: 0x0006E7E0 File Offset: 0x0006C9E0
		public void SetGroup(IdentityReference identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this.WriteLock();
			try
			{
				this._securityDescriptor.Group = identity.Translate(typeof(SecurityIdentifier)) as SecurityIdentifier;
				this._groupModified = true;
			}
			finally
			{
				this.WriteUnlock();
			}
		}

		/// <summary>Removes all access rules associated with the specified <see cref="T:System.Security.Principal.IdentityReference" />.</summary>
		/// <param name="identity">The <see cref="T:System.Security.Principal.IdentityReference" /> for which to remove all access rules.</param>
		/// <exception cref="T:System.InvalidOperationException">All access rules are not in canonical order.</exception>
		// Token: 0x06001FA2 RID: 8098 RVA: 0x0006E848 File Offset: 0x0006CA48
		public virtual void PurgeAccessRules(IdentityReference identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this.WriteLock();
			try
			{
				this._securityDescriptor.PurgeAccessControl(identity.Translate(typeof(SecurityIdentifier)) as SecurityIdentifier);
				this._daclModified = true;
			}
			finally
			{
				this.WriteUnlock();
			}
		}

		/// <summary>Removes all audit rules associated with the specified <see cref="T:System.Security.Principal.IdentityReference" />.</summary>
		/// <param name="identity">The <see cref="T:System.Security.Principal.IdentityReference" /> for which to remove all audit rules.</param>
		/// <exception cref="T:System.InvalidOperationException">All audit rules are not in canonical order.</exception>
		// Token: 0x06001FA3 RID: 8099 RVA: 0x0006E8B0 File Offset: 0x0006CAB0
		public virtual void PurgeAuditRules(IdentityReference identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this.WriteLock();
			try
			{
				this._securityDescriptor.PurgeAudit(identity.Translate(typeof(SecurityIdentifier)) as SecurityIdentifier);
				this._saclModified = true;
			}
			finally
			{
				this.WriteUnlock();
			}
		}

		/// <summary>Gets a Boolean value that specifies whether the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object is protected.</summary>
		/// <returns>
		///   <see langword="true" /> if the DACL is protected; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06001FA4 RID: 8100 RVA: 0x0006E918 File Offset: 0x0006CB18
		public bool AreAccessRulesProtected
		{
			get
			{
				this.ReadLock();
				bool flag;
				try
				{
					flag = (this._securityDescriptor.ControlFlags & ControlFlags.DiscretionaryAclProtected) > ControlFlags.None;
				}
				finally
				{
					this.ReadUnlock();
				}
				return flag;
			}
		}

		/// <summary>Sets or removes protection of the access rules associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object. Protected access rules cannot be modified by parent objects through inheritance.</summary>
		/// <param name="isProtected">
		///   <see langword="true" /> to protect the access rules associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object from inheritance; <see langword="false" /> to allow inheritance.</param>
		/// <param name="preserveInheritance">
		///   <see langword="true" /> to preserve inherited access rules; <see langword="false" /> to remove inherited access rules. This parameter is ignored if <paramref name="isProtected" /> is <see langword="false" />.</param>
		/// <exception cref="T:System.InvalidOperationException">This method attempts to remove inherited rules from a non-canonical Discretionary Access Control List (DACL).</exception>
		// Token: 0x06001FA5 RID: 8101 RVA: 0x0006E95C File Offset: 0x0006CB5C
		public void SetAccessRuleProtection(bool isProtected, bool preserveInheritance)
		{
			this.WriteLock();
			try
			{
				this._securityDescriptor.SetDiscretionaryAclProtection(isProtected, preserveInheritance);
				this._daclModified = true;
			}
			finally
			{
				this.WriteUnlock();
			}
		}

		/// <summary>Gets a Boolean value that specifies whether the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object is protected.</summary>
		/// <returns>
		///   <see langword="true" /> if the SACL is protected; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06001FA6 RID: 8102 RVA: 0x0006E99C File Offset: 0x0006CB9C
		public bool AreAuditRulesProtected
		{
			get
			{
				this.ReadLock();
				bool flag;
				try
				{
					flag = (this._securityDescriptor.ControlFlags & ControlFlags.SystemAclProtected) > ControlFlags.None;
				}
				finally
				{
					this.ReadUnlock();
				}
				return flag;
			}
		}

		/// <summary>Sets or removes protection of the audit rules associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object. Protected audit rules cannot be modified by parent objects through inheritance.</summary>
		/// <param name="isProtected">
		///   <see langword="true" /> to protect the audit rules associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object from inheritance; <see langword="false" /> to allow inheritance.</param>
		/// <param name="preserveInheritance">
		///   <see langword="true" /> to preserve inherited audit rules; <see langword="false" /> to remove inherited audit rules. This parameter is ignored if <paramref name="isProtected" /> is <see langword="false" />.</param>
		/// <exception cref="T:System.InvalidOperationException">This method attempts to remove inherited rules from a non-canonical System Access Control List (SACL).</exception>
		// Token: 0x06001FA7 RID: 8103 RVA: 0x0006E9E0 File Offset: 0x0006CBE0
		public void SetAuditRuleProtection(bool isProtected, bool preserveInheritance)
		{
			this.WriteLock();
			try
			{
				this._securityDescriptor.SetSystemAclProtection(isProtected, preserveInheritance);
				this._saclModified = true;
			}
			finally
			{
				this.WriteUnlock();
			}
		}

		/// <summary>Gets a Boolean value that specifies whether the access rules associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object are in canonical order.</summary>
		/// <returns>
		///   <see langword="true" /> if the access rules are in canonical order; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06001FA8 RID: 8104 RVA: 0x0006EA20 File Offset: 0x0006CC20
		public bool AreAccessRulesCanonical
		{
			get
			{
				this.ReadLock();
				bool isDiscretionaryAclCanonical;
				try
				{
					isDiscretionaryAclCanonical = this._securityDescriptor.IsDiscretionaryAclCanonical;
				}
				finally
				{
					this.ReadUnlock();
				}
				return isDiscretionaryAclCanonical;
			}
		}

		/// <summary>Gets a Boolean value that specifies whether the audit rules associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object are in canonical order.</summary>
		/// <returns>
		///   <see langword="true" /> if the audit rules are in canonical order; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06001FA9 RID: 8105 RVA: 0x0006EA5C File Offset: 0x0006CC5C
		public bool AreAuditRulesCanonical
		{
			get
			{
				this.ReadLock();
				bool isSystemAclCanonical;
				try
				{
					isSystemAclCanonical = this._securityDescriptor.IsSystemAclCanonical;
				}
				finally
				{
					this.ReadUnlock();
				}
				return isSystemAclCanonical;
			}
		}

		/// <summary>Returns a Boolean value that specifies whether the security descriptor associated with this  <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object can be converted to the Security Descriptor Definition Language (SDDL) format.</summary>
		/// <returns>
		///   <see langword="true" /> if the security descriptor associated with this  <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object can be converted to the Security Descriptor Definition Language (SDDL) format; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001FAA RID: 8106 RVA: 0x0006EA98 File Offset: 0x0006CC98
		public static bool IsSddlConversionSupported()
		{
			return true;
		}

		/// <summary>Returns the Security Descriptor Definition Language (SDDL) representation of the specified sections of the security descriptor associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</summary>
		/// <param name="includeSections">Specifies which sections (access rules, audit rules, primary group, owner) of the security descriptor to get.</param>
		/// <returns>The SDDL representation of the specified sections of the security descriptor associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</returns>
		// Token: 0x06001FAB RID: 8107 RVA: 0x0006EA9C File Offset: 0x0006CC9C
		public string GetSecurityDescriptorSddlForm(AccessControlSections includeSections)
		{
			this.ReadLock();
			string sddlForm;
			try
			{
				sddlForm = this._securityDescriptor.GetSddlForm(includeSections);
			}
			finally
			{
				this.ReadUnlock();
			}
			return sddlForm;
		}

		/// <summary>Sets the security descriptor for this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object from the specified Security Descriptor Definition Language (SDDL) string.</summary>
		/// <param name="sddlForm">The SDDL string from which to set the security descriptor.</param>
		// Token: 0x06001FAC RID: 8108 RVA: 0x0006EAD8 File Offset: 0x0006CCD8
		public void SetSecurityDescriptorSddlForm(string sddlForm)
		{
			this.SetSecurityDescriptorSddlForm(sddlForm, AccessControlSections.All);
		}

		/// <summary>Sets the specified sections of the security descriptor for this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object from the specified Security Descriptor Definition Language (SDDL) string.</summary>
		/// <param name="sddlForm">The SDDL string from which to set the security descriptor.</param>
		/// <param name="includeSections">The sections (access rules, audit rules, owner, primary group) of the security descriptor to set.</param>
		// Token: 0x06001FAD RID: 8109 RVA: 0x0006EAE4 File Offset: 0x0006CCE4
		public void SetSecurityDescriptorSddlForm(string sddlForm, AccessControlSections includeSections)
		{
			if (sddlForm == null)
			{
				throw new ArgumentNullException("sddlForm");
			}
			if ((includeSections & AccessControlSections.All) == AccessControlSections.None)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumAtLeastOneFlag"), "includeSections");
			}
			this.WriteLock();
			try
			{
				this.UpdateWithNewSecurityDescriptor(new RawSecurityDescriptor(sddlForm), includeSections);
			}
			finally
			{
				this.WriteUnlock();
			}
		}

		/// <summary>Returns an array of byte values that represents the security descriptor information for this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</summary>
		/// <returns>An array of byte values that represents the security descriptor for this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object. This method returns <see langword="null" /> if there is no security information in this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</returns>
		// Token: 0x06001FAE RID: 8110 RVA: 0x0006EB48 File Offset: 0x0006CD48
		public byte[] GetSecurityDescriptorBinaryForm()
		{
			this.ReadLock();
			byte[] array2;
			try
			{
				byte[] array = new byte[this._securityDescriptor.BinaryLength];
				this._securityDescriptor.GetBinaryForm(array, 0);
				array2 = array;
			}
			finally
			{
				this.ReadUnlock();
			}
			return array2;
		}

		/// <summary>Sets the security descriptor for this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object from the specified array of byte values.</summary>
		/// <param name="binaryForm">The array of bytes from which to set the security descriptor.</param>
		// Token: 0x06001FAF RID: 8111 RVA: 0x0006EB98 File Offset: 0x0006CD98
		public void SetSecurityDescriptorBinaryForm(byte[] binaryForm)
		{
			this.SetSecurityDescriptorBinaryForm(binaryForm, AccessControlSections.All);
		}

		/// <summary>Sets the specified sections of the security descriptor for this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object from the specified array of byte values.</summary>
		/// <param name="binaryForm">The array of bytes from which to set the security descriptor.</param>
		/// <param name="includeSections">The sections (access rules, audit rules, owner, primary group) of the security descriptor to set.</param>
		// Token: 0x06001FB0 RID: 8112 RVA: 0x0006EBA4 File Offset: 0x0006CDA4
		public void SetSecurityDescriptorBinaryForm(byte[] binaryForm, AccessControlSections includeSections)
		{
			if (binaryForm == null)
			{
				throw new ArgumentNullException("binaryForm");
			}
			if ((includeSections & AccessControlSections.All) == AccessControlSections.None)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumAtLeastOneFlag"), "includeSections");
			}
			this.WriteLock();
			try
			{
				this.UpdateWithNewSecurityDescriptor(new RawSecurityDescriptor(binaryForm, 0), includeSections);
			}
			finally
			{
				this.WriteUnlock();
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the securable object associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</summary>
		/// <returns>The type of the securable object associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</returns>
		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06001FB1 RID: 8113
		public abstract Type AccessRightType { get; }

		/// <summary>Gets the <see cref="T:System.Type" /> of the object associated with the access rules of this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object. The <see cref="T:System.Type" /> object must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
		/// <returns>The type of the object associated with the access rules of this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</returns>
		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06001FB2 RID: 8114
		public abstract Type AccessRuleType { get; }

		/// <summary>Gets the <see cref="T:System.Type" /> object associated with the audit rules of this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object. The <see cref="T:System.Type" /> object must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
		/// <returns>The type of the object associated with the audit rules of this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</returns>
		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06001FB3 RID: 8115
		public abstract Type AuditRuleType { get; }

		/// <summary>Applies the specified modification to the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</summary>
		/// <param name="modification">The modification to apply to the DACL.</param>
		/// <param name="rule">The access rule to modify.</param>
		/// <param name="modified">
		///   <see langword="true" /> if the DACL is successfully modified; otherwise, <see langword="false" />.</param>
		/// <returns>
		///   <see langword="true" /> if the DACL is successfully modified; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001FB4 RID: 8116
		protected abstract bool ModifyAccess(AccessControlModification modification, AccessRule rule, out bool modified);

		/// <summary>Applies the specified modification to the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</summary>
		/// <param name="modification">The modification to apply to the SACL.</param>
		/// <param name="rule">The audit rule to modify.</param>
		/// <param name="modified">
		///   <see langword="true" /> if the SACL is successfully modified; otherwise, <see langword="false" />.</param>
		/// <returns>
		///   <see langword="true" /> if the SACL is successfully modified; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001FB5 RID: 8117
		protected abstract bool ModifyAudit(AccessControlModification modification, AuditRule rule, out bool modified);

		/// <summary>Applies the specified modification to the Discretionary Access Control List (DACL) associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</summary>
		/// <param name="modification">The modification to apply to the DACL.</param>
		/// <param name="rule">The access rule to modify.</param>
		/// <param name="modified">
		///   <see langword="true" /> if the DACL is successfully modified; otherwise, <see langword="false" />.</param>
		/// <returns>
		///   <see langword="true" /> if the DACL is successfully modified; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001FB6 RID: 8118 RVA: 0x0006EC08 File Offset: 0x0006CE08
		public virtual bool ModifyAccessRule(AccessControlModification modification, AccessRule rule, out bool modified)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			if (!this.AccessRuleType.IsAssignableFrom(rule.GetType()))
			{
				throw new ArgumentException(Environment.GetResourceString("AccessControl_InvalidAccessRuleType"), "rule");
			}
			this.WriteLock();
			bool flag;
			try
			{
				flag = this.ModifyAccess(modification, rule, out modified);
			}
			finally
			{
				this.WriteUnlock();
			}
			return flag;
		}

		/// <summary>Applies the specified modification to the System Access Control List (SACL) associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</summary>
		/// <param name="modification">The modification to apply to the SACL.</param>
		/// <param name="rule">The audit rule to modify.</param>
		/// <param name="modified">
		///   <see langword="true" /> if the SACL is successfully modified; otherwise, <see langword="false" />.</param>
		/// <returns>
		///   <see langword="true" /> if the SACL is successfully modified; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001FB7 RID: 8119 RVA: 0x0006EC78 File Offset: 0x0006CE78
		public virtual bool ModifyAuditRule(AccessControlModification modification, AuditRule rule, out bool modified)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			if (!this.AuditRuleType.IsAssignableFrom(rule.GetType()))
			{
				throw new ArgumentException(Environment.GetResourceString("AccessControl_InvalidAuditRuleType"), "rule");
			}
			this.WriteLock();
			bool flag;
			try
			{
				flag = this.ModifyAudit(modification, rule, out modified);
			}
			finally
			{
				this.WriteUnlock();
			}
			return flag;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.AccessRule" /> class with the specified values.</summary>
		/// <param name="identityReference">The identity to which the access rule applies.  It must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" />.</param>
		/// <param name="accessMask">The access mask of this rule. The access mask is a 32-bit collection of anonymous bits, the meaning of which is defined by the individual integrators.</param>
		/// <param name="isInherited">true if this rule is inherited from a parent container.</param>
		/// <param name="inheritanceFlags">Specifies the inheritance properties of the access rule.</param>
		/// <param name="propagationFlags">Specifies whether inherited access rules are automatically propagated. The propagation flags are ignored if <paramref name="inheritanceFlags" /> is set to <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.</param>
		/// <param name="type">Specifies the valid access control type.</param>
		/// <returns>The <see cref="T:System.Security.AccessControl.AccessRule" /> object that this method creates.</returns>
		// Token: 0x06001FB8 RID: 8120
		public abstract AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type);

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.AuditRule" /> class with the specified values.</summary>
		/// <param name="identityReference">The identity to which the audit rule applies.  It must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" />.</param>
		/// <param name="accessMask">The access mask of this rule. The access mask is a 32-bit collection of anonymous bits, the meaning of which is defined by the individual integrators.</param>
		/// <param name="isInherited">
		///   <see langword="true" /> if this rule is inherited from a parent container.</param>
		/// <param name="inheritanceFlags">Specifies the inheritance properties of the audit rule.</param>
		/// <param name="propagationFlags">Specifies whether inherited audit rules are automatically propagated. The propagation flags are ignored if <paramref name="inheritanceFlags" /> is set to <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.</param>
		/// <param name="flags">Specifies the conditions for which the rule is audited.</param>
		/// <returns>The <see cref="T:System.Security.AccessControl.AuditRule" /> object that this method creates.</returns>
		// Token: 0x06001FB9 RID: 8121
		public abstract AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags);

		// Token: 0x04000B5D RID: 2909
		private readonly ReaderWriterLock _lock = new ReaderWriterLock();

		// Token: 0x04000B5E RID: 2910
		internal CommonSecurityDescriptor _securityDescriptor;

		// Token: 0x04000B5F RID: 2911
		private bool _ownerModified;

		// Token: 0x04000B60 RID: 2912
		private bool _groupModified;

		// Token: 0x04000B61 RID: 2913
		private bool _saclModified;

		// Token: 0x04000B62 RID: 2914
		private bool _daclModified;

		// Token: 0x04000B63 RID: 2915
		private static readonly ControlFlags SACL_CONTROL_FLAGS = ControlFlags.SystemAclPresent | ControlFlags.SystemAclAutoInherited | ControlFlags.SystemAclProtected;

		// Token: 0x04000B64 RID: 2916
		private static readonly ControlFlags DACL_CONTROL_FLAGS = ControlFlags.DiscretionaryAclPresent | ControlFlags.DiscretionaryAclAutoInherited | ControlFlags.DiscretionaryAclProtected;
	}
}
