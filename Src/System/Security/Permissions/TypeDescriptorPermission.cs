using System;
using System.Globalization;

namespace System.Security.Permissions
{
	/// <summary>Defines partial-trust access to the <see cref="T:System.ComponentModel.TypeDescriptor" /> class.</summary>
	// Token: 0x02000487 RID: 1159
	[Serializable]
	public sealed class TypeDescriptorPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.TypeDescriptorPermission" /> class.</summary>
		/// <param name="state">The <see cref="T:System.Security.Permissions.PermissionState" /> to request. Only <see cref="F:System.Security.Permissions.PermissionState.Unrestricted" /> and <see cref="F:System.Security.Permissions.PermissionState.None" /> are valid.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="state" /> is not a valid permission state. Only <see cref="F:System.Security.Permissions.PermissionState.Unrestricted" /> and <see cref="F:System.Security.Permissions.PermissionState.None" /> are valid.</exception>
		// Token: 0x06002AEA RID: 10986 RVA: 0x000C3480 File Offset: 0x000C1680
		public TypeDescriptorPermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				this.SetUnrestricted(true);
				return;
			}
			if (state == PermissionState.None)
			{
				this.SetUnrestricted(false);
				return;
			}
			throw new ArgumentException(SR.GetString("Argument_InvalidPermissionState"));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.TypeDescriptorPermission" /> class with the specified permission flags.</summary>
		/// <param name="flag">The permission flags to request.</param>
		// Token: 0x06002AEB RID: 10987 RVA: 0x000C34AE File Offset: 0x000C16AE
		public TypeDescriptorPermission(TypeDescriptorPermissionFlags flag)
		{
			this.VerifyAccess(flag);
			this.SetUnrestricted(false);
			this.m_flags = flag;
		}

		// Token: 0x06002AEC RID: 10988 RVA: 0x000C34CB File Offset: 0x000C16CB
		private void SetUnrestricted(bool unrestricted)
		{
			if (unrestricted)
			{
				this.m_flags = TypeDescriptorPermissionFlags.RestrictedRegistrationAccess;
				return;
			}
			this.Reset();
		}

		// Token: 0x06002AED RID: 10989 RVA: 0x000C34DE File Offset: 0x000C16DE
		private void Reset()
		{
			this.m_flags = TypeDescriptorPermissionFlags.NoFlags;
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Permissions.TypeDescriptorPermissionFlags" /> for the type descriptor.</summary>
		/// <returns>The <see cref="T:System.Security.Permissions.TypeDescriptorPermissionFlags" /> for the type descriptor.</returns>
		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x06002AEF RID: 10991 RVA: 0x000C34F7 File Offset: 0x000C16F7
		// (set) Token: 0x06002AEE RID: 10990 RVA: 0x000C34E7 File Offset: 0x000C16E7
		public TypeDescriptorPermissionFlags Flags
		{
			get
			{
				return this.m_flags;
			}
			set
			{
				this.VerifyAccess(value);
				this.m_flags = value;
			}
		}

		/// <summary>Gets a value that indicates whether the type descriptor may be called from partially trusted code.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Security.Permissions.TypeDescriptorPermission.Flags" /> property is set to <see cref="F:System.Security.Permissions.TypeDescriptorPermissionFlags.RestrictedRegistrationAccess" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002AF0 RID: 10992 RVA: 0x000C34FF File Offset: 0x000C16FF
		public bool IsUnrestricted()
		{
			return this.m_flags == TypeDescriptorPermissionFlags.RestrictedRegistrationAccess;
		}

		/// <summary>When overridden in a derived class, creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		// Token: 0x06002AF1 RID: 10993 RVA: 0x000C350C File Offset: 0x000C170C
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			IPermission permission;
			try
			{
				TypeDescriptorPermission typeDescriptorPermission = (TypeDescriptorPermission)target;
				TypeDescriptorPermissionFlags typeDescriptorPermissionFlags = this.m_flags | typeDescriptorPermission.m_flags;
				if (typeDescriptorPermissionFlags == TypeDescriptorPermissionFlags.NoFlags)
				{
					permission = null;
				}
				else
				{
					permission = new TypeDescriptorPermission(typeDescriptorPermissionFlags);
				}
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Argument_WrongType"), new object[] { base.GetType().FullName }));
			}
			return permission;
		}

		/// <summary>When implemented by a derived class, determines whether the current permission is a subset of the specified permission.</summary>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission.</param>
		/// <returns>
		///   <see langword="true" /> if the current permission is a subset of the specified permission; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002AF2 RID: 10994 RVA: 0x000C358C File Offset: 0x000C178C
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.m_flags == TypeDescriptorPermissionFlags.NoFlags;
			}
			bool flag;
			try
			{
				TypeDescriptorPermission typeDescriptorPermission = (TypeDescriptorPermission)target;
				TypeDescriptorPermissionFlags flags = this.m_flags;
				TypeDescriptorPermissionFlags flags2 = typeDescriptorPermission.m_flags;
				flag = (flags & flags2) == flags;
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Argument_WrongType"), new object[] { base.GetType().FullName }));
			}
			return flag;
		}

		/// <summary>When implemented by a derived class, creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is <see langword="null" /> if the intersection is empty.</returns>
		// Token: 0x06002AF3 RID: 10995 RVA: 0x000C3608 File Offset: 0x000C1808
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			IPermission permission;
			try
			{
				TypeDescriptorPermission typeDescriptorPermission = (TypeDescriptorPermission)target;
				TypeDescriptorPermissionFlags typeDescriptorPermissionFlags = typeDescriptorPermission.m_flags & this.m_flags;
				if (typeDescriptorPermissionFlags == TypeDescriptorPermissionFlags.NoFlags)
				{
					permission = null;
				}
				else
				{
					permission = new TypeDescriptorPermission(typeDescriptorPermissionFlags);
				}
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Argument_WrongType"), new object[] { base.GetType().FullName }));
			}
			return permission;
		}

		/// <summary>When implemented by a derived class, creates and returns an identical copy of the current permission object.</summary>
		/// <returns>A copy of the current permission object.</returns>
		// Token: 0x06002AF4 RID: 10996 RVA: 0x000C3680 File Offset: 0x000C1880
		public override IPermission Copy()
		{
			return new TypeDescriptorPermission(this.m_flags);
		}

		// Token: 0x06002AF5 RID: 10997 RVA: 0x000C368D File Offset: 0x000C188D
		private void VerifyAccess(TypeDescriptorPermissionFlags type)
		{
			if ((type & ~TypeDescriptorPermissionFlags.RestrictedRegistrationAccess) != TypeDescriptorPermissionFlags.NoFlags)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Arg_EnumIllegalVal"), new object[] { (int)type }));
			}
		}

		/// <summary>When overridden in a derived class, creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x06002AF6 RID: 10998 RVA: 0x000C36C0 File Offset: 0x000C18C0
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", base.GetType().FullName + ", " + base.GetType().Module.Assembly.FullName.Replace('"', '\''));
			securityElement.AddAttribute("version", "1");
			if (!this.IsUnrestricted())
			{
				securityElement.AddAttribute("Flags", this.m_flags.ToString());
			}
			else
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		/// <summary>When overridden in a derived class, reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="securityElement">The XML encoding to use to reconstruct the security object.</param>
		// Token: 0x06002AF7 RID: 10999 RVA: 0x000C3760 File Offset: 0x000C1960
		public override void FromXml(SecurityElement securityElement)
		{
			if (securityElement == null)
			{
				throw new ArgumentNullException("securityElement");
			}
			string text = securityElement.Attribute("class");
			if (text == null || text.IndexOf(base.GetType().FullName, StringComparison.Ordinal) == -1)
			{
				throw new ArgumentException(SR.GetString("Argument_InvalidClassAttribute"), "securityElement");
			}
			string text2 = securityElement.Attribute("Unrestricted");
			if (text2 != null && string.Compare(text2, "true", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.m_flags = TypeDescriptorPermissionFlags.RestrictedRegistrationAccess;
				return;
			}
			this.m_flags = TypeDescriptorPermissionFlags.NoFlags;
			string text3 = securityElement.Attribute("Flags");
			if (text3 != null)
			{
				TypeDescriptorPermissionFlags typeDescriptorPermissionFlags = (TypeDescriptorPermissionFlags)Enum.Parse(typeof(TypeDescriptorPermissionFlags), text3);
				TypeDescriptorPermission.VerifyFlags(typeDescriptorPermissionFlags);
				this.m_flags = typeDescriptorPermissionFlags;
			}
		}

		// Token: 0x06002AF8 RID: 11000 RVA: 0x000C3812 File Offset: 0x000C1A12
		internal static void VerifyFlags(TypeDescriptorPermissionFlags flags)
		{
			if ((flags & ~TypeDescriptorPermissionFlags.RestrictedRegistrationAccess) != TypeDescriptorPermissionFlags.NoFlags)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Arg_EnumIllegalVal"), new object[] { (int)flags }));
			}
		}

		// Token: 0x04002654 RID: 9812
		private TypeDescriptorPermissionFlags m_flags;
	}
}
