using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace System.Security.AccessControl
{
	/// <summary>Represents the Windows access control security for a named semaphore. This class cannot be inherited.</summary>
	// Token: 0x0200048E RID: 1166
	[ComVisible(false)]
	public sealed class SemaphoreSecurity : NativeObjectSecurity
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.SemaphoreSecurity" /> class with default values.</summary>
		/// <exception cref="T:System.NotSupportedException">This class is not supported on Windows 98 or Windows Millennium Edition.</exception>
		// Token: 0x06002B28 RID: 11048 RVA: 0x000C472D File Offset: 0x000C292D
		public SemaphoreSecurity()
			: base(true, ResourceType.KernelObject)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.SemaphoreSecurity" /> class with the specified sections of the access control security rules from the system semaphore with the specified name.</summary>
		/// <param name="name">The name of the system semaphore whose access control security rules are to be retrieved.</param>
		/// <param name="includeSections">A combination of <see cref="T:System.Security.AccessControl.AccessControlSections" /> flags specifying the sections to retrieve.</param>
		/// <exception cref="T:System.NotSupportedException">This class is not supported on Windows 98 or Windows Millennium Edition.</exception>
		// Token: 0x06002B29 RID: 11049 RVA: 0x000C4737 File Offset: 0x000C2937
		public SemaphoreSecurity(string name, AccessControlSections includeSections)
			: base(true, ResourceType.KernelObject, name, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(SemaphoreSecurity._HandleErrorCode), null)
		{
		}

		// Token: 0x06002B2A RID: 11050 RVA: 0x000C4750 File Offset: 0x000C2950
		internal SemaphoreSecurity(SafeWaitHandle handle, AccessControlSections includeSections)
			: base(true, ResourceType.KernelObject, handle, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(SemaphoreSecurity._HandleErrorCode), null)
		{
		}

		// Token: 0x06002B2B RID: 11051 RVA: 0x000C476C File Offset: 0x000C296C
		private static Exception _HandleErrorCode(int errorCode, string name, SafeHandle handle, object context)
		{
			Exception ex = null;
			if (errorCode == 2 || errorCode == 6 || errorCode == 123)
			{
				if (name != null && name.Length != 0)
				{
					ex = new WaitHandleCannotBeOpenedException(SR.GetString("WaitHandleCannotBeOpenedException_InvalidHandle", new object[] { name }));
				}
				else
				{
					ex = new WaitHandleCannotBeOpenedException();
				}
			}
			return ex;
		}

		/// <summary>Creates a new access control rule for the specified user, with the specified access rights, access control, and flags.</summary>
		/// <param name="identityReference">An <see cref="T:System.Security.Principal.IdentityReference" /> that identifies the user or group the rule applies to.</param>
		/// <param name="accessMask">A bitwise combination of <see cref="T:System.Security.AccessControl.SemaphoreRights" /> values specifying the access rights to allow or deny, cast to an integer.</param>
		/// <param name="isInherited">Meaningless for named semaphores, because they have no hierarchy.</param>
		/// <param name="inheritanceFlags">Meaningless for named semaphores, because they have no hierarchy.</param>
		/// <param name="propagationFlags">Meaningless for named semaphores, because they have no hierarchy.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> values specifying whether the rights are allowed or denied.</param>
		/// <returns>A <see cref="T:System.Security.AccessControl.SemaphoreAccessRule" /> object representing the specified rights for the specified user.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="accessMask" />, <paramref name="inheritanceFlags" />, <paramref name="propagationFlags" />, or <paramref name="type" /> specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identityReference" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="accessMask" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="identityReference" /> is neither of type <see cref="T:System.Security.Principal.SecurityIdentifier" />, nor of a type such as <see cref="T:System.Security.Principal.NTAccount" /> that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
		// Token: 0x06002B2C RID: 11052 RVA: 0x000C47B6 File Offset: 0x000C29B6
		public override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			return new SemaphoreAccessRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, type);
		}

		/// <summary>Creates a new audit rule, specifying the user the rule applies to, the access rights to audit, and the outcome that triggers the audit rule.</summary>
		/// <param name="identityReference">An <see cref="T:System.Security.Principal.IdentityReference" /> that identifies the user or group the rule applies to.</param>
		/// <param name="accessMask">A bitwise combination of <see cref="T:System.Security.AccessControl.SemaphoreRights" /> values specifying the access rights to audit, cast to an integer.</param>
		/// <param name="isInherited">Meaningless for named wait handles, because they have no hierarchy.</param>
		/// <param name="inheritanceFlags">Meaningless for named wait handles, because they have no hierarchy.</param>
		/// <param name="propagationFlags">Meaningless for named wait handles, because they have no hierarchy.</param>
		/// <param name="flags">A bitwise combination of <see cref="T:System.Security.AccessControl.AuditFlags" /> values that specify whether to audit successful access, failed access, or both.</param>
		/// <returns>A <see cref="T:System.Security.AccessControl.SemaphoreAuditRule" /> object representing the specified audit rule for the specified user. The return type of the method is the base class, <see cref="T:System.Security.AccessControl.AuditRule" />, but the return value can be cast safely to the derived class.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="accessMask" />, <paramref name="inheritanceFlags" />, <paramref name="propagationFlags" />, or <paramref name="flags" /> specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identityReference" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="accessMask" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="identityReference" /> is neither of type <see cref="T:System.Security.Principal.SecurityIdentifier" />, nor of a type such as <see cref="T:System.Security.Principal.NTAccount" /> that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
		// Token: 0x06002B2D RID: 11053 RVA: 0x000C47C6 File Offset: 0x000C29C6
		public override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			return new SemaphoreAuditRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, flags);
		}

		// Token: 0x06002B2E RID: 11054 RVA: 0x000C47D8 File Offset: 0x000C29D8
		internal AccessControlSections GetAccessControlSectionsFromChanges()
		{
			AccessControlSections accessControlSections = AccessControlSections.None;
			if (base.AccessRulesModified)
			{
				accessControlSections = AccessControlSections.Access;
			}
			if (base.AuditRulesModified)
			{
				accessControlSections |= AccessControlSections.Audit;
			}
			if (base.OwnerModified)
			{
				accessControlSections |= AccessControlSections.Owner;
			}
			if (base.GroupModified)
			{
				accessControlSections |= AccessControlSections.Group;
			}
			return accessControlSections;
		}

		// Token: 0x06002B2F RID: 11055 RVA: 0x000C4818 File Offset: 0x000C2A18
		internal void Persist(SafeWaitHandle handle)
		{
			base.WriteLock();
			try
			{
				AccessControlSections accessControlSectionsFromChanges = this.GetAccessControlSectionsFromChanges();
				if (accessControlSectionsFromChanges != AccessControlSections.None)
				{
					base.Persist(handle, accessControlSectionsFromChanges);
					base.OwnerModified = (base.GroupModified = (base.AuditRulesModified = (base.AccessRulesModified = false)));
				}
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		/// <summary>Searches for a matching rule with which the new rule can be merged. If none are found, adds the new rule.</summary>
		/// <param name="rule">The access control rule to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is <see langword="null" />.</exception>
		// Token: 0x06002B30 RID: 11056 RVA: 0x000C487C File Offset: 0x000C2A7C
		public void AddAccessRule(SemaphoreAccessRule rule)
		{
			base.AddAccessRule(rule);
		}

		/// <summary>Removes all access control rules with the same user and <see cref="T:System.Security.AccessControl.AccessControlType" /> (allow or deny) as the specified rule, and then adds the specified rule.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.SemaphoreAccessRule" /> to add. The user and <see cref="T:System.Security.AccessControl.AccessControlType" /> of this rule determine the rules to remove before this rule is added.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is <see langword="null" />.</exception>
		// Token: 0x06002B31 RID: 11057 RVA: 0x000C4885 File Offset: 0x000C2A85
		public void SetAccessRule(SemaphoreAccessRule rule)
		{
			base.SetAccessRule(rule);
		}

		/// <summary>Removes all access control rules with the same user as the specified rule, regardless of <see cref="T:System.Security.AccessControl.AccessControlType" />, and then adds the specified rule.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.SemaphoreAccessRule" /> to add. The user specified by this rule determines the rules to remove before this rule is added.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is <see langword="null" />.</exception>
		// Token: 0x06002B32 RID: 11058 RVA: 0x000C488E File Offset: 0x000C2A8E
		public void ResetAccessRule(SemaphoreAccessRule rule)
		{
			base.ResetAccessRule(rule);
		}

		/// <summary>Searches for an access control rule with the same user and <see cref="T:System.Security.AccessControl.AccessControlType" /> (allow or deny) as the specified rule, and with compatible inheritance and propagation flags; if such a rule is found, the rights contained in the specified access rule are removed from it.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.SemaphoreAccessRule" /> that specifies the user and <see cref="T:System.Security.AccessControl.AccessControlType" /> to search for, and a set of inheritance and propagation flags that a matching rule, if found, must be compatible with. Specifies the rights to remove from the compatible rule, if found.</param>
		/// <returns>
		///   <see langword="true" /> if a compatible rule is found; otherwise <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is <see langword="null" />.</exception>
		// Token: 0x06002B33 RID: 11059 RVA: 0x000C4897 File Offset: 0x000C2A97
		public bool RemoveAccessRule(SemaphoreAccessRule rule)
		{
			return base.RemoveAccessRule(rule);
		}

		/// <summary>Searches for all access control rules with the same user and <see cref="T:System.Security.AccessControl.AccessControlType" /> (allow or deny) as the specified rule and, if found, removes them.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.SemaphoreAccessRule" /> that specifies the user and <see cref="T:System.Security.AccessControl.AccessControlType" /> to search for. Any rights specified by this rule are ignored.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is <see langword="null" />.</exception>
		// Token: 0x06002B34 RID: 11060 RVA: 0x000C48A0 File Offset: 0x000C2AA0
		public void RemoveAccessRuleAll(SemaphoreAccessRule rule)
		{
			base.RemoveAccessRuleAll(rule);
		}

		/// <summary>Searches for an access control rule that exactly matches the specified rule and, if found, removes it.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.SemaphoreAccessRule" /> to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is <see langword="null" />.</exception>
		// Token: 0x06002B35 RID: 11061 RVA: 0x000C48A9 File Offset: 0x000C2AA9
		public void RemoveAccessRuleSpecific(SemaphoreAccessRule rule)
		{
			base.RemoveAccessRuleSpecific(rule);
		}

		/// <summary>Searches for an audit rule with which the new rule can be merged. If none are found, adds the new rule.</summary>
		/// <param name="rule">The audit rule to add. The user specified by this rule determines the search.</param>
		// Token: 0x06002B36 RID: 11062 RVA: 0x000C48B2 File Offset: 0x000C2AB2
		public void AddAuditRule(SemaphoreAuditRule rule)
		{
			base.AddAuditRule(rule);
		}

		/// <summary>Removes all audit rules with the same user as the specified rule, regardless of the <see cref="T:System.Security.AccessControl.AuditFlags" /> value, and then adds the specified rule.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.SemaphoreAuditRule" /> to add. The user specified by this rule determines the rules to remove before this rule is added.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is <see langword="null" />.</exception>
		// Token: 0x06002B37 RID: 11063 RVA: 0x000C48BB File Offset: 0x000C2ABB
		public void SetAuditRule(SemaphoreAuditRule rule)
		{
			base.SetAuditRule(rule);
		}

		/// <summary>Searches for an audit control rule with the same user as the specified rule, and with compatible inheritance and propagation flags; if a compatible rule is found, the rights contained in the specified rule are removed from it.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.SemaphoreAuditRule" /> that specifies the user to search for, and a set of inheritance and propagation flags that a matching rule, if found, must be compatible with. Specifies the rights to remove from the compatible rule, if found.</param>
		/// <returns>
		///   <see langword="true" /> if a compatible rule is found; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is <see langword="null" />.</exception>
		// Token: 0x06002B38 RID: 11064 RVA: 0x000C48C4 File Offset: 0x000C2AC4
		public bool RemoveAuditRule(SemaphoreAuditRule rule)
		{
			return base.RemoveAuditRule(rule);
		}

		/// <summary>Searches for all audit rules with the same user as the specified rule and, if found, removes them.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.SemaphoreAuditRule" /> that specifies the user to search for. Any rights specified by this rule are ignored.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is <see langword="null" />.</exception>
		// Token: 0x06002B39 RID: 11065 RVA: 0x000C48CD File Offset: 0x000C2ACD
		public void RemoveAuditRuleAll(SemaphoreAuditRule rule)
		{
			base.RemoveAuditRuleAll(rule);
		}

		/// <summary>Searches for an audit rule that exactly matches the specified rule and, if found, removes it.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.SemaphoreAuditRule" /> to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is <see langword="null" />.</exception>
		// Token: 0x06002B3A RID: 11066 RVA: 0x000C48D6 File Offset: 0x000C2AD6
		public void RemoveAuditRuleSpecific(SemaphoreAuditRule rule)
		{
			base.RemoveAuditRuleSpecific(rule);
		}

		/// <summary>Gets the enumeration that the <see cref="T:System.Security.AccessControl.SemaphoreSecurity" /> class uses to represent access rights.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Security.AccessControl.SemaphoreRights" /> enumeration.</returns>
		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06002B3B RID: 11067 RVA: 0x000C48DF File Offset: 0x000C2ADF
		public override Type AccessRightType
		{
			get
			{
				return typeof(SemaphoreRights);
			}
		}

		/// <summary>Gets the type that the <see cref="T:System.Security.AccessControl.SemaphoreSecurity" /> class uses to represent access rules.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Security.AccessControl.SemaphoreAccessRule" /> class.</returns>
		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06002B3C RID: 11068 RVA: 0x000C48EB File Offset: 0x000C2AEB
		public override Type AccessRuleType
		{
			get
			{
				return typeof(SemaphoreAccessRule);
			}
		}

		/// <summary>Gets the type that the <see cref="T:System.Security.AccessControl.SemaphoreSecurity" /> class uses to represent audit rules.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Security.AccessControl.SemaphoreAuditRule" /> class.</returns>
		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x06002B3D RID: 11069 RVA: 0x000C48F7 File Offset: 0x000C2AF7
		public override Type AuditRuleType
		{
			get
			{
				return typeof(SemaphoreAuditRule);
			}
		}
	}
}
