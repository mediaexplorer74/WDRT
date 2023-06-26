using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;
using Microsoft.Win32.SafeHandles;

namespace System.Security.AccessControl
{
	/// <summary>Represents the access control and audit security for a file or directory.</summary>
	// Token: 0x0200021B RID: 539
	public abstract class FileSystemSecurity : NativeObjectSecurity
	{
		// Token: 0x06001F42 RID: 8002 RVA: 0x0006D6F7 File Offset: 0x0006B8F7
		[SecurityCritical]
		internal FileSystemSecurity(bool isContainer)
			: base(isContainer, ResourceType.FileObject, new NativeObjectSecurity.ExceptionFromErrorCode(FileSystemSecurity._HandleErrorCode), isContainer)
		{
		}

		// Token: 0x06001F43 RID: 8003 RVA: 0x0006D713 File Offset: 0x0006B913
		[SecurityCritical]
		internal FileSystemSecurity(bool isContainer, string name, AccessControlSections includeSections, bool isDirectory)
			: base(isContainer, ResourceType.FileObject, name, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(FileSystemSecurity._HandleErrorCode), isDirectory)
		{
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x0006D732 File Offset: 0x0006B932
		[SecurityCritical]
		internal FileSystemSecurity(bool isContainer, SafeFileHandle handle, AccessControlSections includeSections, bool isDirectory)
			: base(isContainer, ResourceType.FileObject, handle, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(FileSystemSecurity._HandleErrorCode), isDirectory)
		{
		}

		// Token: 0x06001F45 RID: 8005 RVA: 0x0006D754 File Offset: 0x0006B954
		[SecurityCritical]
		private static Exception _HandleErrorCode(int errorCode, string name, SafeHandle handle, object context)
		{
			Exception ex = null;
			if (errorCode != 2)
			{
				if (errorCode != 6)
				{
					if (errorCode == 123)
					{
						ex = new ArgumentException(Environment.GetResourceString("Argument_InvalidName"), "name");
					}
				}
				else
				{
					ex = new ArgumentException(Environment.GetResourceString("AccessControl_InvalidHandle"));
				}
			}
			else if (context != null && context is bool && (bool)context)
			{
				if (name != null && name.Length != 0)
				{
					ex = new DirectoryNotFoundException(name);
				}
				else
				{
					ex = new DirectoryNotFoundException();
				}
			}
			else if (name != null && name.Length != 0)
			{
				ex = new FileNotFoundException(name);
			}
			else
			{
				ex = new FileNotFoundException();
			}
			return ex;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> class that represents a new access control rule for the specified user, with the specified access rights, access control, and flags.</summary>
		/// <param name="identityReference">An <see cref="T:System.Security.Principal.IdentityReference" /> object that represents a user account.</param>
		/// <param name="accessMask">An integer that specifies an access type.</param>
		/// <param name="isInherited">
		///   <see langword="true" /> if the access rule is inherited; otherwise, <see langword="false" />.</param>
		/// <param name="inheritanceFlags">One of the <see cref="T:System.Security.AccessControl.InheritanceFlags" /> values that specifies how to propagate access masks to child objects.</param>
		/// <param name="propagationFlags">One of the <see cref="T:System.Security.AccessControl.PropagationFlags" /> values that specifies how to propagate Access Control Entries (ACEs) to child objects.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> values that specifies whether access is allowed or denied.</param>
		/// <returns>A new <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> object that represents a new access control rule for the specified user, with the specified access rights, access control, and flags.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="accessMask" />, <paramref name="inheritanceFlags" />, <paramref name="propagationFlags" />, or <paramref name="type" /> parameters specify an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="identityReference" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="accessMask" /> parameter is zero.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="identityReference" /> parameter is neither of type <see cref="T:System.Security.Principal.SecurityIdentifier" />, nor of a type such as <see cref="T:System.Security.Principal.NTAccount" /> that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
		// Token: 0x06001F46 RID: 8006 RVA: 0x0006D7E3 File Offset: 0x0006B9E3
		public sealed override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			return new FileSystemAccessRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, type);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> class representing the specified audit rule for the specified user.</summary>
		/// <param name="identityReference">An <see cref="T:System.Security.Principal.IdentityReference" /> object that represents a user account.</param>
		/// <param name="accessMask">An integer that specifies an access type.</param>
		/// <param name="isInherited">
		///   <see langword="true" /> if the access rule is inherited; otherwise, <see langword="false" />.</param>
		/// <param name="inheritanceFlags">One of the <see cref="T:System.Security.AccessControl.InheritanceFlags" /> values that specifies how to propagate access masks to child objects.</param>
		/// <param name="propagationFlags">One of the <see cref="T:System.Security.AccessControl.PropagationFlags" /> values that specifies how to propagate Access Control Entries (ACEs) to child objects.</param>
		/// <param name="flags">One of the <see cref="T:System.Security.AccessControl.AuditFlags" /> values that specifies the type of auditing to perform.</param>
		/// <returns>A new <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> object representing the specified audit rule for the specified user.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="accessMask" />, <paramref name="inheritanceFlags" />, <paramref name="propagationFlags" />, or <paramref name="flags" /> properties specify an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="identityReference" /> property is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="accessMask" /> property is zero.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="identityReference" /> property is neither of type <see cref="T:System.Security.Principal.SecurityIdentifier" />, nor of a type such as <see cref="T:System.Security.Principal.NTAccount" /> that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
		// Token: 0x06001F47 RID: 8007 RVA: 0x0006D7F3 File Offset: 0x0006B9F3
		public sealed override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			return new FileSystemAuditRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, flags);
		}

		// Token: 0x06001F48 RID: 8008 RVA: 0x0006D804 File Offset: 0x0006BA04
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

		// Token: 0x06001F49 RID: 8009 RVA: 0x0006D844 File Offset: 0x0006BA44
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		internal void Persist(string fullPath)
		{
			FileIOPermission.QuickDemand(FileIOPermissionAccess.NoAccess, AccessControlActions.Change, fullPath, false, true);
			base.WriteLock();
			try
			{
				AccessControlSections accessControlSectionsFromChanges = this.GetAccessControlSectionsFromChanges();
				base.Persist(fullPath, accessControlSectionsFromChanges);
				base.OwnerModified = (base.GroupModified = (base.AuditRulesModified = (base.AccessRulesModified = false)));
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x0006D8AC File Offset: 0x0006BAAC
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		internal void Persist(SafeFileHandle handle, string fullPath)
		{
			if (fullPath != null)
			{
				FileIOPermission.QuickDemand(FileIOPermissionAccess.NoAccess, AccessControlActions.Change, fullPath, false, true);
			}
			else
			{
				FileIOPermission.QuickDemand(PermissionState.Unrestricted);
			}
			base.WriteLock();
			try
			{
				AccessControlSections accessControlSectionsFromChanges = this.GetAccessControlSectionsFromChanges();
				base.Persist(handle, accessControlSectionsFromChanges);
				base.OwnerModified = (base.GroupModified = (base.AuditRulesModified = (base.AccessRulesModified = false)));
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		/// <summary>Adds the specified access control list (ACL) permission to the current file or directory.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> object that represents an access control list (ACL) permission to add to a file or directory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06001F4B RID: 8011 RVA: 0x0006D920 File Offset: 0x0006BB20
		public void AddAccessRule(FileSystemAccessRule rule)
		{
			base.AddAccessRule(rule);
		}

		/// <summary>Sets the specified access control list (ACL) permission for the current file or directory.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> object that represents an access control list (ACL) permission to set for a file or directory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06001F4C RID: 8012 RVA: 0x0006D929 File Offset: 0x0006BB29
		public void SetAccessRule(FileSystemAccessRule rule)
		{
			base.SetAccessRule(rule);
		}

		/// <summary>Adds the specified access control list (ACL) permission to the current file or directory and removes all matching ACL permissions.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> object that represents an access control list (ACL) permission to add to a file or directory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06001F4D RID: 8013 RVA: 0x0006D932 File Offset: 0x0006BB32
		public void ResetAccessRule(FileSystemAccessRule rule)
		{
			base.ResetAccessRule(rule);
		}

		/// <summary>Removes all matching allow or deny access control list (ACL) permissions from the current file or directory.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> object that represents an access control list (ACL) permission to remove from a file or directory.</param>
		/// <returns>
		///   <see langword="true" /> if the access rule was removed; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06001F4E RID: 8014 RVA: 0x0006D93C File Offset: 0x0006BB3C
		public bool RemoveAccessRule(FileSystemAccessRule rule)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			AuthorizationRuleCollection accessRules = base.GetAccessRules(true, true, rule.IdentityReference.GetType());
			for (int i = 0; i < accessRules.Count; i++)
			{
				FileSystemAccessRule fileSystemAccessRule = accessRules[i] as FileSystemAccessRule;
				if (fileSystemAccessRule != null && fileSystemAccessRule.FileSystemRights == rule.FileSystemRights && fileSystemAccessRule.IdentityReference == rule.IdentityReference && fileSystemAccessRule.AccessControlType == rule.AccessControlType)
				{
					return base.RemoveAccessRule(rule);
				}
			}
			FileSystemAccessRule fileSystemAccessRule2 = new FileSystemAccessRule(rule.IdentityReference, FileSystemAccessRule.AccessMaskFromRights(rule.FileSystemRights, AccessControlType.Deny), rule.IsInherited, rule.InheritanceFlags, rule.PropagationFlags, rule.AccessControlType);
			return base.RemoveAccessRule(fileSystemAccessRule2);
		}

		/// <summary>Removes all access control list (ACL) permissions for the specified user from the current file or directory.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> object that specifies a user whose access control list (ACL) permissions should be removed from a file or directory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06001F4F RID: 8015 RVA: 0x0006D9FA File Offset: 0x0006BBFA
		public void RemoveAccessRuleAll(FileSystemAccessRule rule)
		{
			base.RemoveAccessRuleAll(rule);
		}

		/// <summary>Removes a single matching allow or deny access control list (ACL) permission from the current file or directory.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> object that specifies a user whose access control list (ACL) permissions should be removed from a file or directory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06001F50 RID: 8016 RVA: 0x0006DA04 File Offset: 0x0006BC04
		public void RemoveAccessRuleSpecific(FileSystemAccessRule rule)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			AuthorizationRuleCollection accessRules = base.GetAccessRules(true, true, rule.IdentityReference.GetType());
			for (int i = 0; i < accessRules.Count; i++)
			{
				FileSystemAccessRule fileSystemAccessRule = accessRules[i] as FileSystemAccessRule;
				if (fileSystemAccessRule != null && fileSystemAccessRule.FileSystemRights == rule.FileSystemRights && fileSystemAccessRule.IdentityReference == rule.IdentityReference && fileSystemAccessRule.AccessControlType == rule.AccessControlType)
				{
					base.RemoveAccessRuleSpecific(rule);
					return;
				}
			}
			FileSystemAccessRule fileSystemAccessRule2 = new FileSystemAccessRule(rule.IdentityReference, FileSystemAccessRule.AccessMaskFromRights(rule.FileSystemRights, AccessControlType.Deny), rule.IsInherited, rule.InheritanceFlags, rule.PropagationFlags, rule.AccessControlType);
			base.RemoveAccessRuleSpecific(fileSystemAccessRule2);
		}

		/// <summary>Adds the specified audit rule to the current file or directory.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> object that represents an audit rule to add to a file or directory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06001F51 RID: 8017 RVA: 0x0006DAC2 File Offset: 0x0006BCC2
		public void AddAuditRule(FileSystemAuditRule rule)
		{
			base.AddAuditRule(rule);
		}

		/// <summary>Sets the specified audit rule for the current file or directory.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> object that represents an audit rule to set for a file or directory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06001F52 RID: 8018 RVA: 0x0006DACB File Offset: 0x0006BCCB
		public void SetAuditRule(FileSystemAuditRule rule)
		{
			base.SetAuditRule(rule);
		}

		/// <summary>Removes all matching allow or deny audit rules from the current file or directory.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> object that represents an audit rule to remove from a file or directory.</param>
		/// <returns>
		///   <see langword="true" /> if the audit rule was removed; otherwise, <see langword="false" /></returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06001F53 RID: 8019 RVA: 0x0006DAD4 File Offset: 0x0006BCD4
		public bool RemoveAuditRule(FileSystemAuditRule rule)
		{
			return base.RemoveAuditRule(rule);
		}

		/// <summary>Removes all audit rules for the specified user from the current file or directory.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> object that specifies a user whose audit rules should be removed from a file or directory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06001F54 RID: 8020 RVA: 0x0006DADD File Offset: 0x0006BCDD
		public void RemoveAuditRuleAll(FileSystemAuditRule rule)
		{
			base.RemoveAuditRuleAll(rule);
		}

		/// <summary>Removes a single matching allow or deny audit rule from the current file or directory.</summary>
		/// <param name="rule">A <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> object that represents an audit rule to remove from a file or directory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06001F55 RID: 8021 RVA: 0x0006DAE6 File Offset: 0x0006BCE6
		public void RemoveAuditRuleSpecific(FileSystemAuditRule rule)
		{
			base.RemoveAuditRuleSpecific(rule);
		}

		/// <summary>Gets the enumeration that the <see cref="T:System.Security.AccessControl.FileSystemSecurity" /> class uses to represent access rights.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Security.AccessControl.FileSystemRights" /> enumeration.</returns>
		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001F56 RID: 8022 RVA: 0x0006DAEF File Offset: 0x0006BCEF
		public override Type AccessRightType
		{
			get
			{
				return typeof(FileSystemRights);
			}
		}

		/// <summary>Gets the enumeration that the <see cref="T:System.Security.AccessControl.FileSystemSecurity" /> class uses to represent access rules.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Security.AccessControl.FileSystemAccessRule" /> class.</returns>
		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06001F57 RID: 8023 RVA: 0x0006DAFB File Offset: 0x0006BCFB
		public override Type AccessRuleType
		{
			get
			{
				return typeof(FileSystemAccessRule);
			}
		}

		/// <summary>Gets the type that the <see cref="T:System.Security.AccessControl.FileSystemSecurity" /> class uses to represent audit rules.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Security.AccessControl.FileSystemAuditRule" /> class.</returns>
		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06001F58 RID: 8024 RVA: 0x0006DB07 File Offset: 0x0006BD07
		public override Type AuditRuleType
		{
			get
			{
				return typeof(FileSystemAuditRule);
			}
		}

		// Token: 0x04000B46 RID: 2886
		private const ResourceType s_ResourceType = ResourceType.FileObject;
	}
}
