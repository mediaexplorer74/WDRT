using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents a combination of a user's identity, an access mask, and an access control type (allow or deny). An <see cref="T:System.Security.AccessControl.ObjectAccessRule" /> object also contains information about the type of object to which the rule applies, the type of child object that can inherit the rule, how the rule is inherited by child objects, and how that inheritance is propagated.</summary>
	// Token: 0x02000233 RID: 563
	public abstract class ObjectAccessRule : AccessRule
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.ObjectAccessRule" /> class with the specified values.</summary>
		/// <param name="identity">The identity to which the access rule applies.  It must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" />.</param>
		/// <param name="accessMask">The access mask of this rule. The access mask is a 32-bit collection of anonymous bits, the meaning of which is defined by the individual integrators.</param>
		/// <param name="isInherited">
		///   <see langword="true" /> if this rule is inherited from a parent container.</param>
		/// <param name="inheritanceFlags">Specifies the inheritance properties of the access rule.</param>
		/// <param name="propagationFlags">Specifies whether inherited access rules are automatically propagated. The propagation flags are ignored if <paramref name="inheritanceFlags" /> is set to <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.</param>
		/// <param name="objectType">The type of object to which the rule applies.</param>
		/// <param name="inheritedObjectType">The type of child object that can inherit the rule.</param>
		/// <param name="type">Specifies whether this rule allows or denies access.</param>
		/// <exception cref="T:System.ArgumentException">The value of the <paramref name="identity" /> parameter cannot be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" />, or the <paramref name="type" /> parameter contains an invalid value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <paramref name="accessMask" /> parameter is 0, or the <paramref name="inheritanceFlags" /> or <paramref name="propagationFlags" /> parameters contain unrecognized flag values.</exception>
		// Token: 0x0600203E RID: 8254 RVA: 0x0007140C File Offset: 0x0006F60C
		protected ObjectAccessRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, Guid objectType, Guid inheritedObjectType, AccessControlType type)
			: base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, type)
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

		/// <summary>Gets the type of object to which the <see cref="T:System.Security.AccessControl.ObjectAccessRule" /> applies.</summary>
		/// <returns>The type of object to which the <see cref="T:System.Security.AccessControl.ObjectAccessRule" /> applies.</returns>
		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x0600203F RID: 8255 RVA: 0x00071498 File Offset: 0x0006F698
		public Guid ObjectType
		{
			get
			{
				return this._objectType;
			}
		}

		/// <summary>Gets the type of child object that can inherit the <see cref="T:System.Security.AccessControl.ObjectAccessRule" /> object.</summary>
		/// <returns>The type of child object that can inherit the <see cref="T:System.Security.AccessControl.ObjectAccessRule" /> object.</returns>
		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06002040 RID: 8256 RVA: 0x000714A0 File Offset: 0x0006F6A0
		public Guid InheritedObjectType
		{
			get
			{
				return this._inheritedObjectType;
			}
		}

		/// <summary>Gets flags that specify if the <see cref="P:System.Security.AccessControl.ObjectAccessRule.ObjectType" /> and <see cref="P:System.Security.AccessControl.ObjectAccessRule.InheritedObjectType" /> properties of the <see cref="T:System.Security.AccessControl.ObjectAccessRule" /> object contain valid values.</summary>
		/// <returns>
		///   <see cref="F:System.Security.AccessControl.ObjectAceFlags.ObjectAceTypePresent" /> specifies that the <see cref="P:System.Security.AccessControl.ObjectAccessRule.ObjectType" /> property contains a valid value. <see cref="F:System.Security.AccessControl.ObjectAceFlags.InheritedObjectAceTypePresent" /> specifies that the <see cref="P:System.Security.AccessControl.ObjectAccessRule.InheritedObjectType" /> property contains a valid value. These values can be combined with a logical OR.</returns>
		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06002041 RID: 8257 RVA: 0x000714A8 File Offset: 0x0006F6A8
		public ObjectAceFlags ObjectFlags
		{
			get
			{
				return this._objectFlags;
			}
		}

		// Token: 0x04000BA8 RID: 2984
		private readonly Guid _objectType;

		// Token: 0x04000BA9 RID: 2985
		private readonly Guid _inheritedObjectType;

		// Token: 0x04000BAA RID: 2986
		private readonly ObjectAceFlags _objectFlags;
	}
}
