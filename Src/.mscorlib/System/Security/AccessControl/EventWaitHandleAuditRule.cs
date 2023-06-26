using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents a set of access rights to be audited for a user or group. This class cannot be inherited.</summary>
	// Token: 0x02000216 RID: 534
	public sealed class EventWaitHandleAuditRule : AuditRule
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.EventWaitHandleAuditRule" /> class, specifying the user or group to audit, the rights to audit, and whether to audit success, failure, or both.</summary>
		/// <param name="identity">The user or group the rule applies to. Must be of type <see cref="T:System.Security.Principal.SecurityIdentifier" /> or a type such as <see cref="T:System.Security.Principal.NTAccount" /> that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</param>
		/// <param name="eventRights">A bitwise combination of <see cref="T:System.Security.AccessControl.EventWaitHandleRights" /> values specifying the kinds of access to audit.</param>
		/// <param name="flags">A bitwise combination of <see cref="T:System.Security.AccessControl.AuditFlags" /> values specifying whether to audit success, failure, or both.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="eventRights" /> specifies an invalid value.  
		/// -or-  
		/// <paramref name="flags" /> specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identity" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="eventRights" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="identity" /> is neither of type <see cref="T:System.Security.Principal.SecurityIdentifier" /> nor of a type such as <see cref="T:System.Security.Principal.NTAccount" /> that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
		// Token: 0x06001F1A RID: 7962 RVA: 0x0006D35E File Offset: 0x0006B55E
		public EventWaitHandleAuditRule(IdentityReference identity, EventWaitHandleRights eventRights, AuditFlags flags)
			: this(identity, (int)eventRights, false, InheritanceFlags.None, PropagationFlags.None, flags)
		{
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x0006D36C File Offset: 0x0006B56C
		internal EventWaitHandleAuditRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
			: base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, flags)
		{
		}

		/// <summary>Gets the access rights affected by the audit rule.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Security.AccessControl.EventWaitHandleRights" /> values that indicates the rights affected by the audit rule.</returns>
		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06001F1C RID: 7964 RVA: 0x0006D37D File Offset: 0x0006B57D
		public EventWaitHandleRights EventWaitHandleRights
		{
			get
			{
				return (EventWaitHandleRights)base.AccessMask;
			}
		}
	}
}
