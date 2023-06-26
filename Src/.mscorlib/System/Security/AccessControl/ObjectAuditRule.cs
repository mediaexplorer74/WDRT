using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents a combination of a user's identity, an access mask, and audit conditions. An <see cref="T:System.Security.AccessControl.ObjectAuditRule" /> object also contains information about the type of object to which the rule applies, the type of child object that can inherit the rule, how the rule is inherited by child objects, and how that inheritance is propagated.</summary>
	// Token: 0x02000235 RID: 565
	public abstract class ObjectAuditRule : AuditRule
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.ObjectAuditRule" /> class.</summary>
		/// <param name="identity">The identity to which the access rule applies.  It must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" />.</param>
		/// <param name="accessMask">The access mask of this rule. The access mask is a 32-bit collection of anonymous bits, the meaning of which is defined by the individual integrators.</param>
		/// <param name="isInherited">
		///   <see langword="true" /> if this rule is inherited from a parent container.</param>
		/// <param name="inheritanceFlags">Specifies the inheritance properties of the access rule.</param>
		/// <param name="propagationFlags">Whether inherited access rules are automatically propagated. The propagation flags are ignored if <paramref name="inheritanceFlags" /> is set to <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.</param>
		/// <param name="objectType">The type of object to which the rule applies.</param>
		/// <param name="inheritedObjectType">The type of child object that can inherit the rule.</param>
		/// <param name="auditFlags">The audit conditions.</param>
		/// <exception cref="T:System.ArgumentException">The value of the <paramref name="identity" /> parameter cannot be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" />, or the <paramref name="type" /> parameter contains an invalid value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <paramref name="accessMask" /> parameter is 0, or the <paramref name="inheritanceFlags" /> or <paramref name="propagationFlags" /> parameters contain unrecognized flag values.</exception>
		// Token: 0x06002044 RID: 8260 RVA: 0x00071510 File Offset: 0x0006F710
		protected ObjectAuditRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, Guid objectType, Guid inheritedObjectType, AuditFlags auditFlags)
			: base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, auditFlags)
		{
			if (!objectType.Equals(Guid.Empty) && (accessMask & ObjectAce.AccessMaskWithObjectType) != 0)
			{
				this._objectType = objectType;
				this._objectFlags |= ObjectAceFlags.ObjectAceTypePresent;
			}
			else
			{
				this._objectType = Guid.Empty;
			}
			if (!inheritedObjectType.Equals(Guid.Empty) && (inheritanceFlags & InheritanceFlags.ContainerInherit) != InheritanceFlags.None)
			{
				this._inheritedObjectType = inheritedObjectType;
				this._objectFlags |= ObjectAceFlags.InheritedObjectAceTypePresent;
				return;
			}
			this._inheritedObjectType = Guid.Empty;
		}

		/// <summary>Gets the type of object to which the <see cref="T:System.Security.AccessControl.ObjectAuditRule" /> applies.</summary>
		/// <returns>The type of object to which the <see cref="T:System.Security.AccessControl.ObjectAuditRule" /> applies.</returns>
		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06002045 RID: 8261 RVA: 0x0007159C File Offset: 0x0006F79C
		public Guid ObjectType
		{
			get
			{
				return this._objectType;
			}
		}

		/// <summary>Gets the type of child object that can inherit the <see cref="T:System.Security.AccessControl.ObjectAuditRule" /> object.</summary>
		/// <returns>The type of child object that can inherit the <see cref="T:System.Security.AccessControl.ObjectAuditRule" /> object.</returns>
		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06002046 RID: 8262 RVA: 0x000715A4 File Offset: 0x0006F7A4
		public Guid InheritedObjectType
		{
			get
			{
				return this._inheritedObjectType;
			}
		}

		/// <summary>
		///   <see cref="P:System.Security.AccessControl.ObjectAuditRule.ObjectType" /> and <see cref="P:System.Security.AccessControl.ObjectAuditRule.InheritedObjectType" /> properties of the <see cref="T:System.Security.AccessControl.ObjectAuditRule" /> object contain valid values.</summary>
		/// <returns>
		///   <see cref="F:System.Security.AccessControl.ObjectAceFlags.ObjectAceTypePresent" /> specifies that the <see cref="P:System.Security.AccessControl.ObjectAuditRule.ObjectType" /> property contains a valid value. <see cref="F:System.Security.AccessControl.ObjectAceFlags.InheritedObjectAceTypePresent" /> specifies that the <see cref="P:System.Security.AccessControl.ObjectAuditRule.InheritedObjectType" /> property contains a valid value. These values can be combined with a logical OR.</returns>
		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06002047 RID: 8263 RVA: 0x000715AC File Offset: 0x0006F7AC
		public ObjectAceFlags ObjectFlags
		{
			get
			{
				return this._objectFlags;
			}
		}

		// Token: 0x04000BAC RID: 2988
		private readonly Guid _objectType;

		// Token: 0x04000BAD RID: 2989
		private readonly Guid _inheritedObjectType;

		// Token: 0x04000BAE RID: 2990
		private readonly ObjectAceFlags _objectFlags;
	}
}
