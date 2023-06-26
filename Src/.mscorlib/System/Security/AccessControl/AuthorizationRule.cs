using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Determines access to securable objects. The derived classes <see cref="T:System.Security.AccessControl.AccessRule" /> and <see cref="T:System.Security.AccessControl.AuditRule" /> offer specializations for access and audit functionality.</summary>
	// Token: 0x02000231 RID: 561
	public abstract class AuthorizationRule
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.AccessRule" /> class by using the specified values.</summary>
		/// <param name="identity">The identity to which the access rule applies. This parameter must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" />.</param>
		/// <param name="accessMask">The access mask of this rule. The access mask is a 32-bit collection of anonymous bits, the meaning of which is defined by the individual integrators.</param>
		/// <param name="isInherited">
		///   <see langword="true" /> to inherit this rule from a parent container.</param>
		/// <param name="inheritanceFlags">The inheritance properties of the access rule.</param>
		/// <param name="propagationFlags">Whether inherited access rules are automatically propagated. The propagation flags are ignored if <paramref name="inheritanceFlags" /> is set to <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.</param>
		/// <exception cref="T:System.ArgumentException">The value of the <paramref name="identity" /> parameter cannot be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <paramref name="accessMask" /> parameter is zero, or the <paramref name="inheritanceFlags" /> or <paramref name="propagationFlags" /> parameters contain unrecognized flag values.</exception>
		// Token: 0x06002036 RID: 8246 RVA: 0x00071224 File Offset: 0x0006F424
		protected internal AuthorizationRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			if (accessMask == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ArgumentZero"), "accessMask");
			}
			if ((inheritanceFlags < InheritanceFlags.None) || inheritanceFlags > (InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit))
			{
				throw new ArgumentOutOfRangeException("inheritanceFlags", Environment.GetResourceString("Argument_InvalidEnumValue", new object[] { inheritanceFlags, "InheritanceFlags" }));
			}
			if ((propagationFlags < PropagationFlags.None) || propagationFlags > (PropagationFlags.NoPropagateInherit | PropagationFlags.InheritOnly))
			{
				throw new ArgumentOutOfRangeException("propagationFlags", Environment.GetResourceString("Argument_InvalidEnumValue", new object[] { inheritanceFlags, "PropagationFlags" }));
			}
			if (!identity.IsValidTargetType(typeof(SecurityIdentifier)))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeIdentityReferenceType"), "identity");
			}
			this._identity = identity;
			this._accessMask = accessMask;
			this._isInherited = isInherited;
			this._inheritanceFlags = inheritanceFlags;
			if (inheritanceFlags != InheritanceFlags.None)
			{
				this._propagationFlags = propagationFlags;
				return;
			}
			this._propagationFlags = PropagationFlags.None;
		}

		/// <summary>Gets the <see cref="T:System.Security.Principal.IdentityReference" /> to which this rule applies.</summary>
		/// <returns>The <see cref="T:System.Security.Principal.IdentityReference" /> to which this rule applies.</returns>
		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06002037 RID: 8247 RVA: 0x00071329 File Offset: 0x0006F529
		public IdentityReference IdentityReference
		{
			get
			{
				return this._identity;
			}
		}

		/// <summary>Gets the access mask for this rule.</summary>
		/// <returns>The access mask for this rule.</returns>
		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06002038 RID: 8248 RVA: 0x00071331 File Offset: 0x0006F531
		protected internal int AccessMask
		{
			get
			{
				return this._accessMask;
			}
		}

		/// <summary>Gets a value indicating whether this rule is explicitly set or is inherited from a parent container object.</summary>
		/// <returns>
		///   <see langword="true" /> if this rule is not explicitly set but is instead inherited from a parent container.</returns>
		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06002039 RID: 8249 RVA: 0x00071339 File Offset: 0x0006F539
		public bool IsInherited
		{
			get
			{
				return this._isInherited;
			}
		}

		/// <summary>Gets the value of flags that determine how this rule is inherited by child objects.</summary>
		/// <returns>A bitwise combination of the enumeration values.</returns>
		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x0600203A RID: 8250 RVA: 0x00071341 File Offset: 0x0006F541
		public InheritanceFlags InheritanceFlags
		{
			get
			{
				return this._inheritanceFlags;
			}
		}

		/// <summary>Gets the value of the propagation flags, which determine how inheritance of this rule is propagated to child objects. This property is significant only when the value of the <see cref="T:System.Security.AccessControl.InheritanceFlags" /> enumeration is not <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.</summary>
		/// <returns>A bitwise combination of the enumeration values.</returns>
		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x0600203B RID: 8251 RVA: 0x00071349 File Offset: 0x0006F549
		public PropagationFlags PropagationFlags
		{
			get
			{
				return this._propagationFlags;
			}
		}

		// Token: 0x04000BA2 RID: 2978
		private readonly IdentityReference _identity;

		// Token: 0x04000BA3 RID: 2979
		private readonly int _accessMask;

		// Token: 0x04000BA4 RID: 2980
		private readonly bool _isInherited;

		// Token: 0x04000BA5 RID: 2981
		private readonly InheritanceFlags _inheritanceFlags;

		// Token: 0x04000BA6 RID: 2982
		private readonly PropagationFlags _propagationFlags;
	}
}
