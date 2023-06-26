using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace System.Security.AccessControl
{
	/// <summary>Represents the Windows access control security for a named mutex. This class cannot be inherited.</summary>
	// Token: 0x02000221 RID: 545
	public sealed class MutexSecurity : NativeObjectSecurity
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.MutexSecurity" /> class with default values.</summary>
		/// <exception cref="T:System.NotSupportedException">This class is not supported on Windows 98 or Windows Millennium Edition.</exception>
		// Token: 0x06001F65 RID: 8037 RVA: 0x0006DBF9 File Offset: 0x0006BDF9
		public MutexSecurity()
			: base(true, ResourceType.KernelObject)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.MutexSecurity" /> class with the specified sections of the access control security rules from the system mutex with the specified name.</summary>
		/// <param name="name">The name of the system mutex whose access control security rules are to be retrieved.</param>
		/// <param name="includeSections">A combination of <see cref="T:System.Security.AccessControl.AccessControlSections" /> flags specifying the sections to retrieve.</param>
		/// <exception cref="T:System.IO.FileNotFoundException">There is no system object with the specified name.</exception>
		/// <exception cref="T:System.NotSupportedException">This class is not supported on Windows 98 or Windows Millennium Edition.</exception>
		// Token: 0x06001F66 RID: 8038 RVA: 0x0006DC03 File Offset: 0x0006BE03
		[SecuritySafeCritical]
		public MutexSecurity(string name, AccessControlSections includeSections)
			: base(true, ResourceType.KernelObject, name, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(MutexSecurity._HandleErrorCode), null)
		{
		}

		// Token: 0x06001F67 RID: 8039 RVA: 0x0006DC1C File Offset: 0x0006BE1C
		[SecurityCritical]
		internal MutexSecurity(SafeWaitHandle handle, AccessControlSections includeSections)
			: base(true, ResourceType.KernelObject, handle, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(MutexSecurity._HandleErrorCode), null)
		{
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x0006DC38 File Offset: 0x0006BE38
		[SecurityCritical]
		private static Exception _HandleErrorCode(int errorCode, string name, SafeHandle handle, object context)
		{
			Exception ex = null;
			if (errorCode == 2 || errorCode == 6 || errorCode == 123)
			{
				if (name != null && name.Length != 0)
				{
					ex = new WaitHandleCannotBeOpenedException(Environment.GetResourceString("Threading.WaitHandleCannotBeOpenedException_InvalidHandle", new object[] { name }));
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
		/// <param name="accessMask">A bitwise combination of <see cref="T:System.Security.AccessControl.MutexRights" /> values specifying the access rights to allow or deny, cast to an integer.</param>
		/// <param name="isInherited">Meaningless for named mutexes, because they have no hierarchy.</param>
		/// <param name="inheritanceFlags">Meaningless for named mutexes, because they have no hierarchy.</param>
		/// <param name="propagationFlags">Meaningless for named mutexes, because they have no hierarchy.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> values specifying whether the rights are allowed or denied.</param>
		/// <returns>A <see cref="T:System.Security.AccessControl.MutexAccessRule" /> object representing the specified rights for the specified user.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="accessMask" />, <paramref name="inheritanceFlags" />, <paramref name="propagationFlags" />, or <paramref name="type" /> specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identityReference" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="accessMask" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="identityReference" /> is neither of type <see cref="T:System.Security.Principal.SecurityIdentifier" />, nor of a type such as <see cref="T:System.Security.Principal.NTAccount" /> that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
		// Token: 0x06001F69 RID: 8041 RVA: 0x0006DC82 File Offset: 0x0006BE82
		public override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			return new MutexAccessRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, type);
		}

		/// <summary>Creates a new audit rule, specifying the user the rule applies to, the access rights to audit, and the outcome that triggers the audit rule.</summary>
		/// <param name="identityReference">An <see cref="T:System.Security.Principal.IdentityReference" /> that identifies the user or group the rule applies to.</param>
		/// <param name="accessMask">A bitwise combination of <see cref="T:System.Security.AccessControl.MutexRights" /> values specifying the access rights to audit, cast to an integer.</param>
		/// <param name="isInherited">Meaningless for named wait handles, because they have no hierarchy.</param>
		/// <param name="inheritanceFlags">Meaningless for named wait handles, because they have no hierarchy.</param>
		/// <param name="propagationFlags">Meaningless for named wait handles, because they have no hierarchy.</param>
		/// <param name="flags">A bitwise combination of <see cref="T:System.Security.AccessControl.AuditFlags" /> values that specify whether to audit successful access, failed access, or both.</param>
		/// <returns>A <see cref="T:System.Security.AccessControl.MutexAuditRule" /> object representing the specified audit rule for the specified user. The return type of the method is the base class, <see cref="T:System.Security.AccessControl.AuditRule" />, but the return value can be cast safely to the derived class.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="accessMask" />, <paramref name="inheritanceFlags" />, <paramref name="propagationFlags" />, or <paramref name="flags" /> specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identityReference" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="accessMask" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="identityReference" /> is neither of type <see cref="T:System.Security.Principal.SecurityIdentifier" />, nor of a type such as <see cref="T:System.Security.Principal.NTAccount" /> that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
		// Token: 0x06001F6A RID: 8042 RVA: 0x0006DC92 File Offset: 0x0006BE92
		public override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			return new MutexAuditRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, flags);
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x0006DCA4 File Offset: 0x0006BEA4
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

		// Token: 0x06001F6C RID: 8044 RVA: 0x0006DCE4 File Offset: 0x0006BEE4
		[SecurityCritical]
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

		/// <summary>Searches for a matching access control rule with which the new rule can be merged. If none are found, adds the new rule.</summary>
		/// <param name="rule">The access control rule to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Principal.IdentityNotMappedException">
		///   <paramref name="rule" /> cannot be mapped to a known identity.</exception>
		// Token: 0x06001F6D RID: 8045 RVA: 0x0006DD48 File Offset: 0x0006BF48
		public void AddAccessRule(MutexAccessRule rule)
		{
			base.AddAccessRule(rule);
		}

		/// <summary>Removes all access control rules with the same user and <see cref="T:System.Security.AccessControl.AccessControlType" /> (allow or deny) as the specified rule, and then adds the specified rule.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.MutexAccessRule" /> to add. The user and <see cref="T:System.Security.AccessControl.AccessControlType" /> of this rule determine the rules to remove before this rule is added.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is <see langword="null" />.</exception>
		// Token: 0x06001F6E RID: 8046 RVA: 0x0006DD51 File Offset: 0x0006BF51
		public void SetAccessRule(MutexAccessRule rule)
		{
			base.SetAccessRule(rule);
		}

		/// <summary>Removes all access control rules with the same user as the specified rule, regardless of <see cref="T:System.Security.AccessControl.AccessControlType" />, and then adds the specified rule.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.MutexAccessRule" /> to add. The user specified by this rule determines the rules to remove before this rule is added.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is <see langword="null" />.</exception>
		// Token: 0x06001F6F RID: 8047 RVA: 0x0006DD5A File Offset: 0x0006BF5A
		public void ResetAccessRule(MutexAccessRule rule)
		{
			base.ResetAccessRule(rule);
		}

		/// <summary>Searches for an access control rule with the same user and <see cref="T:System.Security.AccessControl.AccessControlType" /> (allow or deny) as the specified rule, and with compatible inheritance and propagation flags; if such a rule is found, the rights contained in the specified access rule are removed from it.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.MutexAccessRule" /> that specifies the user and <see cref="T:System.Security.AccessControl.AccessControlType" /> to search for, and a set of inheritance and propagation flags that a matching rule, if found, must be compatible with. Specifies the rights to remove from the compatible rule, if found.</param>
		/// <returns>
		///   <see langword="true" /> if a compatible rule is found; otherwise <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is <see langword="null" />.</exception>
		// Token: 0x06001F70 RID: 8048 RVA: 0x0006DD63 File Offset: 0x0006BF63
		public bool RemoveAccessRule(MutexAccessRule rule)
		{
			return base.RemoveAccessRule(rule);
		}

		/// <summary>Searches for all access control rules with the same user and <see cref="T:System.Security.AccessControl.AccessControlType" /> (allow or deny) as the specified rule and, if found, removes them.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.MutexAccessRule" /> that specifies the user and <see cref="T:System.Security.AccessControl.AccessControlType" /> to search for. Any rights specified by this rule are ignored.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is <see langword="null" />.</exception>
		// Token: 0x06001F71 RID: 8049 RVA: 0x0006DD6C File Offset: 0x0006BF6C
		public void RemoveAccessRuleAll(MutexAccessRule rule)
		{
			base.RemoveAccessRuleAll(rule);
		}

		/// <summary>Searches for an access control rule that exactly matches the specified rule and, if found, removes it.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.MutexAccessRule" /> to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is <see langword="null" />.</exception>
		// Token: 0x06001F72 RID: 8050 RVA: 0x0006DD75 File Offset: 0x0006BF75
		public void RemoveAccessRuleSpecific(MutexAccessRule rule)
		{
			base.RemoveAccessRuleSpecific(rule);
		}

		/// <summary>Searches for an audit rule with which the new rule can be merged. If none are found, adds the new rule.</summary>
		/// <param name="rule">The audit rule to add. The user specified by this rule determines the search.</param>
		// Token: 0x06001F73 RID: 8051 RVA: 0x0006DD7E File Offset: 0x0006BF7E
		public void AddAuditRule(MutexAuditRule rule)
		{
			base.AddAuditRule(rule);
		}

		/// <summary>Removes all audit rules with the same user as the specified rule, regardless of the <see cref="T:System.Security.AccessControl.AuditFlags" /> value, and then adds the specified rule.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.MutexAuditRule" /> to add. The user specified by this rule determines the rules to remove before this rule is added.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is <see langword="null" />.</exception>
		// Token: 0x06001F74 RID: 8052 RVA: 0x0006DD87 File Offset: 0x0006BF87
		public void SetAuditRule(MutexAuditRule rule)
		{
			base.SetAuditRule(rule);
		}

		/// <summary>Searches for an audit control rule with the same user as the specified rule, and with compatible inheritance and propagation flags; if a compatible rule is found, the rights contained in the specified rule are removed from it.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.MutexAuditRule" /> that specifies the user to search for, and a set of inheritance and propagation flags that a matching rule, if found, must be compatible with. Specifies the rights to remove from the compatible rule, if found.</param>
		/// <returns>
		///   <see langword="true" /> if a compatible rule is found; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is <see langword="null" />.</exception>
		// Token: 0x06001F75 RID: 8053 RVA: 0x0006DD90 File Offset: 0x0006BF90
		public bool RemoveAuditRule(MutexAuditRule rule)
		{
			return base.RemoveAuditRule(rule);
		}

		/// <summary>Searches for all audit rules with the same user as the specified rule and, if found, removes them.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.MutexAuditRule" /> that specifies the user to search for. Any rights specified by this rule are ignored.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is <see langword="null" />.</exception>
		// Token: 0x06001F76 RID: 8054 RVA: 0x0006DD99 File Offset: 0x0006BF99
		public void RemoveAuditRuleAll(MutexAuditRule rule)
		{
			base.RemoveAuditRuleAll(rule);
		}

		/// <summary>Searches for an audit rule that exactly matches the specified rule and, if found, removes it.</summary>
		/// <param name="rule">The <see cref="T:System.Security.AccessControl.MutexAuditRule" /> to be removed.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rule" /> is <see langword="null" />.</exception>
		// Token: 0x06001F77 RID: 8055 RVA: 0x0006DDA2 File Offset: 0x0006BFA2
		public void RemoveAuditRuleSpecific(MutexAuditRule rule)
		{
			base.RemoveAuditRuleSpecific(rule);
		}

		/// <summary>Gets the enumeration that the <see cref="T:System.Security.AccessControl.MutexSecurity" /> class uses to represent access rights.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Security.AccessControl.MutexRights" /> enumeration.</returns>
		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06001F78 RID: 8056 RVA: 0x0006DDAB File Offset: 0x0006BFAB
		public override Type AccessRightType
		{
			get
			{
				return typeof(MutexRights);
			}
		}

		/// <summary>Gets the type that the <see cref="T:System.Security.AccessControl.MutexSecurity" /> class uses to represent access rules.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Security.AccessControl.MutexAccessRule" /> class.</returns>
		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06001F79 RID: 8057 RVA: 0x0006DDB7 File Offset: 0x0006BFB7
		public override Type AccessRuleType
		{
			get
			{
				return typeof(MutexAccessRule);
			}
		}

		/// <summary>Gets the type that the <see cref="T:System.Security.AccessControl.MutexSecurity" /> class uses to represent audit rules.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Security.AccessControl.MutexAuditRule" /> class.</returns>
		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06001F7A RID: 8058 RVA: 0x0006DDC3 File Offset: 0x0006BFC3
		public override Type AuditRuleType
		{
			get
			{
				return typeof(MutexAuditRule);
			}
		}
	}
}
