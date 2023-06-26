using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents a Discretionary Access Control List (DACL).</summary>
	// Token: 0x0200020F RID: 527
	public sealed class DiscretionaryAcl : CommonAcl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> class with the specified values.</summary>
		/// <param name="isContainer">
		///   <see langword="true" /> if the new <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object is a container.</param>
		/// <param name="isDS">
		///   <see langword="true" /> if the new <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object is a directory object Access Control List (ACL).</param>
		/// <param name="capacity">The number of Access Control Entries (ACEs) this <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object can contain. This number is to be used only as a hint.</param>
		// Token: 0x06001EE2 RID: 7906 RVA: 0x0006CCFB File Offset: 0x0006AEFB
		public DiscretionaryAcl(bool isContainer, bool isDS, int capacity)
			: this(isContainer, isDS, isDS ? GenericAcl.AclRevisionDS : GenericAcl.AclRevision, capacity)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> class with the specified values.</summary>
		/// <param name="isContainer">
		///   <see langword="true" /> if the new <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object is a container.</param>
		/// <param name="isDS">
		///   <see langword="true" /> if the new <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object is a directory object Access Control List (ACL).</param>
		/// <param name="revision">The revision level of the new <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object.</param>
		/// <param name="capacity">The number of Access Control Entries (ACEs) this <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object can contain. This number is to be used only as a hint.</param>
		// Token: 0x06001EE3 RID: 7907 RVA: 0x0006CD15 File Offset: 0x0006AF15
		public DiscretionaryAcl(bool isContainer, bool isDS, byte revision, int capacity)
			: base(isContainer, isDS, revision, capacity)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> class with the specified values from the specified <see cref="T:System.Security.AccessControl.RawAcl" /> object.</summary>
		/// <param name="isContainer">
		///   <see langword="true" /> if the new <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object is a container.</param>
		/// <param name="isDS">
		///   <see langword="true" /> if the new <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object is a directory object Access Control List (ACL).</param>
		/// <param name="rawAcl">The underlying <see cref="T:System.Security.AccessControl.RawAcl" /> object for the new <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object. Specify <see langword="null" /> to create an empty ACL.</param>
		// Token: 0x06001EE4 RID: 7908 RVA: 0x0006CD22 File Offset: 0x0006AF22
		public DiscretionaryAcl(bool isContainer, bool isDS, RawAcl rawAcl)
			: this(isContainer, isDS, rawAcl, false)
		{
		}

		// Token: 0x06001EE5 RID: 7909 RVA: 0x0006CD2E File Offset: 0x0006AF2E
		internal DiscretionaryAcl(bool isContainer, bool isDS, RawAcl rawAcl, bool trusted)
			: base(isContainer, isDS, (rawAcl == null) ? new RawAcl(isDS ? GenericAcl.AclRevisionDS : GenericAcl.AclRevision, 0) : rawAcl, trusted, true)
		{
		}

		/// <summary>Adds an Access Control Entry (ACE) with the specified settings to the current <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object.</summary>
		/// <param name="accessType">The type of access control (allow or deny) to add.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> for which to add an ACE.</param>
		/// <param name="accessMask">The access rule for the new ACE.</param>
		/// <param name="inheritanceFlags">Flags that specify the inheritance properties of the new ACE.</param>
		/// <param name="propagationFlags">Flags that specify the inheritance propagation properties for the new ACE.</param>
		// Token: 0x06001EE6 RID: 7910 RVA: 0x0006CD56 File Offset: 0x0006AF56
		public void AddAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			base.CheckAccessType(accessType);
			base.CheckFlags(inheritanceFlags, propagationFlags);
			this.everyOneFullAccessForNullDacl = false;
			base.AddQualifiedAce(sid, (accessType == AccessControlType.Allow) ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, accessMask, GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), ObjectAceFlags.None, Guid.Empty, Guid.Empty);
		}

		/// <summary>Sets the specified access control for the specified <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
		/// <param name="accessType">The type of access control (allow or deny) to set.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> for which to set an ACE.</param>
		/// <param name="accessMask">The access rule for the new ACE.</param>
		/// <param name="inheritanceFlags">Flags that specify the inheritance properties of the new ACE.</param>
		/// <param name="propagationFlags">Flags that specify the inheritance propagation properties for the new ACE.</param>
		// Token: 0x06001EE7 RID: 7911 RVA: 0x0006CD93 File Offset: 0x0006AF93
		public void SetAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			base.CheckAccessType(accessType);
			base.CheckFlags(inheritanceFlags, propagationFlags);
			this.everyOneFullAccessForNullDacl = false;
			base.SetQualifiedAce(sid, (accessType == AccessControlType.Allow) ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, accessMask, GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), ObjectAceFlags.None, Guid.Empty, Guid.Empty);
		}

		/// <summary>Removes the specified access control rule from the current <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object.</summary>
		/// <param name="accessType">The type of access control (allow or deny) to remove.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> for which to remove an access control rule.</param>
		/// <param name="accessMask">The access mask for the rule to be removed.</param>
		/// <param name="inheritanceFlags">Flags that specify the inheritance properties of the rule to be removed.</param>
		/// <param name="propagationFlags">Flags that specify the inheritance propagation properties for the rule to be removed.</param>
		/// <returns>
		///   <see langword="true" /> if this method successfully removes the specified access; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001EE8 RID: 7912 RVA: 0x0006CDD0 File Offset: 0x0006AFD0
		public bool RemoveAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			base.CheckAccessType(accessType);
			this.everyOneFullAccessForNullDacl = false;
			return base.RemoveQualifiedAces(sid, (accessType == AccessControlType.Allow) ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, accessMask, GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), false, ObjectAceFlags.None, Guid.Empty, Guid.Empty);
		}

		/// <summary>Removes the specified Access Control Entry (ACE) from the current <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object.</summary>
		/// <param name="accessType">The type of access control (allow or deny) to remove.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> for which to remove an ACE.</param>
		/// <param name="accessMask">The access mask for the ACE to be removed.</param>
		/// <param name="inheritanceFlags">Flags that specify the inheritance properties of the ACE to be removed.</param>
		/// <param name="propagationFlags">Flags that specify the inheritance propagation properties for the ACE to be removed.</param>
		// Token: 0x06001EE9 RID: 7913 RVA: 0x0006CE0F File Offset: 0x0006B00F
		public void RemoveAccessSpecific(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			base.CheckAccessType(accessType);
			this.everyOneFullAccessForNullDacl = false;
			base.RemoveQualifiedAcesSpecific(sid, (accessType == AccessControlType.Allow) ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, accessMask, GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), ObjectAceFlags.None, Guid.Empty, Guid.Empty);
		}

		/// <summary>Adds an Access Control Entry (ACE) with the specified settings to the current <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object.</summary>
		/// <param name="accessType">The type of access control (allow or deny) to add.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> for which to add an ACE.</param>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.ObjectAccessRule" /> for the new access.</param>
		// Token: 0x06001EEA RID: 7914 RVA: 0x0006CE44 File Offset: 0x0006B044
		public void AddAccess(AccessControlType accessType, SecurityIdentifier sid, ObjectAccessRule rule)
		{
			this.AddAccess(accessType, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
		}

		/// <summary>Adds an Access Control Entry (ACE) with the specified settings to the current <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object. Use this method for directory object Access Control Lists (ACLs) when specifying the object type or the inherited object type for the new ACE.</summary>
		/// <param name="accessType">The type of access control (allow or deny) to add.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> for which to add an ACE.</param>
		/// <param name="accessMask">The access rule for the new ACE.</param>
		/// <param name="inheritanceFlags">Flags that specify the inheritance properties of the new ACE.</param>
		/// <param name="propagationFlags">Flags that specify the inheritance propagation properties for the new ACE.</param>
		/// <param name="objectFlags">Flags that specify if the <paramref name="objectType" /> and <paramref name="inheritedObjectType" /> parameters contain non-<see langword="null" /> values.</param>
		/// <param name="objectType">The identity of the class of objects to which the new ACE applies.</param>
		/// <param name="inheritedObjectType">The identity of the class of child objects which can inherit the new ACE.</param>
		// Token: 0x06001EEB RID: 7915 RVA: 0x0006CE80 File Offset: 0x0006B080
		public void AddAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			if (!base.IsDS)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_OnlyValidForDS"));
			}
			base.CheckAccessType(accessType);
			base.CheckFlags(inheritanceFlags, propagationFlags);
			this.everyOneFullAccessForNullDacl = false;
			base.AddQualifiedAce(sid, (accessType == AccessControlType.Allow) ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, accessMask, GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), objectFlags, objectType, inheritedObjectType);
		}

		/// <summary>Sets the specified access control for the specified <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
		/// <param name="accessType">The type of access control (allow or deny) to set.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> for which to set an ACE.</param>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.ObjectAccessRule" /> for which to set access.</param>
		// Token: 0x06001EEC RID: 7916 RVA: 0x0006CEDC File Offset: 0x0006B0DC
		public void SetAccess(AccessControlType accessType, SecurityIdentifier sid, ObjectAccessRule rule)
		{
			this.SetAccess(accessType, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
		}

		/// <summary>Sets the specified access control for the specified <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
		/// <param name="accessType">The type of access control (allow or deny) to set.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> for which to set an ACE.</param>
		/// <param name="accessMask">The access rule for the new ACE.</param>
		/// <param name="inheritanceFlags">Flags that specify the inheritance properties of the new ACE.</param>
		/// <param name="propagationFlags">Flags that specify the inheritance propagation properties for the new ACE.</param>
		/// <param name="objectFlags">Flags that specify if the <paramref name="objectType" /> and <paramref name="inheritedObjectType" /> parameters contain non-<see langword="null" /> values.</param>
		/// <param name="objectType">The identity of the class of objects to which the new ACE applies.</param>
		/// <param name="inheritedObjectType">The identity of the class of child objects which can inherit the new ACE.</param>
		// Token: 0x06001EED RID: 7917 RVA: 0x0006CF18 File Offset: 0x0006B118
		public void SetAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			if (!base.IsDS)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_OnlyValidForDS"));
			}
			base.CheckAccessType(accessType);
			base.CheckFlags(inheritanceFlags, propagationFlags);
			this.everyOneFullAccessForNullDacl = false;
			base.SetQualifiedAce(sid, (accessType == AccessControlType.Allow) ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, accessMask, GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), objectFlags, objectType, inheritedObjectType);
		}

		/// <summary>Removes the specified access control rule from the current <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object.</summary>
		/// <param name="accessType">The type of access control (allow or deny) to remove.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> for which to remove an access control rule.</param>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.ObjectAccessRule" /> for which to remove access.</param>
		/// <returns>Returns <see cref="T:System.Boolean" />.</returns>
		// Token: 0x06001EEE RID: 7918 RVA: 0x0006CF74 File Offset: 0x0006B174
		public bool RemoveAccess(AccessControlType accessType, SecurityIdentifier sid, ObjectAccessRule rule)
		{
			return this.RemoveAccess(accessType, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
		}

		/// <summary>Removes the specified access control rule from the current <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object. Use this method for directory object Access Control Lists (ACLs) when specifying the object type or the inherited object type.</summary>
		/// <param name="accessType">The type of access control (allow or deny) to remove.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> for which to remove an access control rule.</param>
		/// <param name="accessMask">The access mask for the access control rule to be removed.</param>
		/// <param name="inheritanceFlags">Flags that specify the inheritance properties of the access control rule to be removed.</param>
		/// <param name="propagationFlags">Flags that specify the inheritance propagation properties for the access control rule to be removed.</param>
		/// <param name="objectFlags">Flags that specify if the <paramref name="objectType" /> and <paramref name="inheritedObjectType" /> parameters contain non-<see langword="null" /> values.</param>
		/// <param name="objectType">The identity of the class of objects to which the removed access control rule applies.</param>
		/// <param name="inheritedObjectType">The identity of the class of child objects which can inherit the removed access control rule.</param>
		/// <returns>
		///   <see langword="true" /> if this method successfully removes the specified access; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001EEF RID: 7919 RVA: 0x0006CFB0 File Offset: 0x0006B1B0
		public bool RemoveAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			if (!base.IsDS)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_OnlyValidForDS"));
			}
			base.CheckAccessType(accessType);
			this.everyOneFullAccessForNullDacl = false;
			return base.RemoveQualifiedAces(sid, (accessType == AccessControlType.Allow) ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, accessMask, GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), false, objectFlags, objectType, inheritedObjectType);
		}

		/// <summary>Removes the specified Access Control Entry (ACE) from the current <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object.</summary>
		/// <param name="accessType">The type of access control (allow or deny) to remove.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> for which to remove an ACE.</param>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.ObjectAccessRule" /> for which to remove access.</param>
		// Token: 0x06001EF0 RID: 7920 RVA: 0x0006D004 File Offset: 0x0006B204
		public void RemoveAccessSpecific(AccessControlType accessType, SecurityIdentifier sid, ObjectAccessRule rule)
		{
			this.RemoveAccessSpecific(accessType, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
		}

		/// <summary>Removes the specified Access Control Entry (ACE) from the current <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> object. Use this method for directory object Access Control Lists (ACLs) when specifying the object type or the inherited object type for the ACE to be removed.</summary>
		/// <param name="accessType">The type of access control (allow or deny) to remove.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> for which to remove an ACE.</param>
		/// <param name="accessMask">The access mask for the ACE to be removed.</param>
		/// <param name="inheritanceFlags">Flags that specify the inheritance properties of the ACE to be removed.</param>
		/// <param name="propagationFlags">Flags that specify the inheritance propagation properties for the ACE to be removed.</param>
		/// <param name="objectFlags">Flags that specify if the <paramref name="objectType" /> and <paramref name="inheritedObjectType" /> parameters contain non-<see langword="null" /> values.</param>
		/// <param name="objectType">The identity of the class of objects to which the removed ACE applies.</param>
		/// <param name="inheritedObjectType">The identity of the class of child objects which can inherit the removed ACE.</param>
		// Token: 0x06001EF1 RID: 7921 RVA: 0x0006D040 File Offset: 0x0006B240
		public void RemoveAccessSpecific(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			if (!base.IsDS)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_OnlyValidForDS"));
			}
			base.CheckAccessType(accessType);
			this.everyOneFullAccessForNullDacl = false;
			base.RemoveQualifiedAcesSpecific(sid, (accessType == AccessControlType.Allow) ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, accessMask, GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), objectFlags, objectType, inheritedObjectType);
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06001EF2 RID: 7922 RVA: 0x0006D091 File Offset: 0x0006B291
		// (set) Token: 0x06001EF3 RID: 7923 RVA: 0x0006D099 File Offset: 0x0006B299
		internal bool EveryOneFullAccessForNullDacl
		{
			get
			{
				return this.everyOneFullAccessForNullDacl;
			}
			set
			{
				this.everyOneFullAccessForNullDacl = value;
			}
		}

		// Token: 0x06001EF4 RID: 7924 RVA: 0x0006D0A2 File Offset: 0x0006B2A2
		internal override void OnAclModificationTried()
		{
			this.everyOneFullAccessForNullDacl = false;
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x0006D0AC File Offset: 0x0006B2AC
		internal static DiscretionaryAcl CreateAllowEveryoneFullAccess(bool isDS, bool isContainer)
		{
			DiscretionaryAcl discretionaryAcl = new DiscretionaryAcl(isContainer, isDS, 1);
			discretionaryAcl.AddAccess(AccessControlType.Allow, DiscretionaryAcl._sidEveryone, -1, isContainer ? (InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit) : InheritanceFlags.None, PropagationFlags.None);
			discretionaryAcl.everyOneFullAccessForNullDacl = true;
			return discretionaryAcl;
		}

		// Token: 0x04000B12 RID: 2834
		private static SecurityIdentifier _sidEveryone = new SecurityIdentifier(WellKnownSidType.WorldSid, null);

		// Token: 0x04000B13 RID: 2835
		private bool everyOneFullAccessForNullDacl;
	}
}
