using System;

namespace System.Security.Permissions
{
	/// <summary>Determines the permission flags that apply to a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
	// Token: 0x02000488 RID: 1160
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class TypeDescriptorPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.TypeDescriptorPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		// Token: 0x06002AF9 RID: 11001 RVA: 0x000C3843 File Offset: 0x000C1A43
		public TypeDescriptorPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Permissions.TypeDescriptorPermissionFlags" /> for the <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
		/// <returns>The <see cref="T:System.Security.Permissions.TypeDescriptorPermissionFlags" /> for the <see cref="T:System.ComponentModel.TypeDescriptor" />.</returns>
		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06002AFA RID: 11002 RVA: 0x000C384C File Offset: 0x000C1A4C
		// (set) Token: 0x06002AFB RID: 11003 RVA: 0x000C3854 File Offset: 0x000C1A54
		public TypeDescriptorPermissionFlags Flags
		{
			get
			{
				return this.m_flags;
			}
			set
			{
				TypeDescriptorPermission.VerifyFlags(value);
				this.m_flags = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the type descriptor can be accessed from partial trust.</summary>
		/// <returns>
		///   <see langword="true" /> if the type descriptor can be accessed from partial trust; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06002AFC RID: 11004 RVA: 0x000C3863 File Offset: 0x000C1A63
		// (set) Token: 0x06002AFD RID: 11005 RVA: 0x000C3870 File Offset: 0x000C1A70
		public bool RestrictedRegistrationAccess
		{
			get
			{
				return (this.m_flags & TypeDescriptorPermissionFlags.RestrictedRegistrationAccess) > TypeDescriptorPermissionFlags.NoFlags;
			}
			set
			{
				this.m_flags = (value ? (this.m_flags | TypeDescriptorPermissionFlags.RestrictedRegistrationAccess) : (this.m_flags & ~TypeDescriptorPermissionFlags.RestrictedRegistrationAccess));
			}
		}

		/// <summary>When overridden in a derived class, creates a permission object that can then be serialized into binary form and persistently stored along with the <see cref="T:System.Security.Permissions.SecurityAction" /> in an assembly's metadata.</summary>
		/// <returns>A serializable permission object.</returns>
		// Token: 0x06002AFE RID: 11006 RVA: 0x000C388E File Offset: 0x000C1A8E
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new TypeDescriptorPermission(PermissionState.Unrestricted);
			}
			return new TypeDescriptorPermission(this.m_flags);
		}

		// Token: 0x04002655 RID: 9813
		private TypeDescriptorPermissionFlags m_flags;
	}
}
