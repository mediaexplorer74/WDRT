using System;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
	/// <summary>Describes a set of security permissions applied to code. This class cannot be inherited.</summary>
	// Token: 0x02000305 RID: 773
	[ComVisible(true)]
	[Serializable]
	public sealed class SecurityPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.SecurityPermission" /> class with either restricted or unrestricted permission as specified.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />.</exception>
		// Token: 0x0600274E RID: 10062 RVA: 0x0008F8A1 File Offset: 0x0008DAA1
		public SecurityPermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				this.SetUnrestricted(true);
				return;
			}
			if (state == PermissionState.None)
			{
				this.SetUnrestricted(false);
				this.Reset();
				return;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.SecurityPermission" /> class with the specified initial set state of the flags.</summary>
		/// <param name="flag">The initial state of the permission, represented by a bitwise OR combination of any permission bits defined by <see cref="T:System.Security.Permissions.SecurityPermissionFlag" />.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="flag" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.SecurityPermissionFlag" />.</exception>
		// Token: 0x0600274F RID: 10063 RVA: 0x0008F8D5 File Offset: 0x0008DAD5
		public SecurityPermission(SecurityPermissionFlag flag)
		{
			this.VerifyAccess(flag);
			this.SetUnrestricted(false);
			this.m_flags = flag;
		}

		// Token: 0x06002750 RID: 10064 RVA: 0x0008F8F2 File Offset: 0x0008DAF2
		private void SetUnrestricted(bool unrestricted)
		{
			if (unrestricted)
			{
				this.m_flags = SecurityPermissionFlag.AllFlags;
			}
		}

		// Token: 0x06002751 RID: 10065 RVA: 0x0008F902 File Offset: 0x0008DB02
		private void Reset()
		{
			this.m_flags = SecurityPermissionFlag.NoFlags;
		}

		/// <summary>Gets or sets the security permission flags.</summary>
		/// <returns>The state of the current permission, represented by a bitwise OR combination of any permission bits defined by <see cref="T:System.Security.Permissions.SecurityPermissionFlag" />.</returns>
		/// <exception cref="T:System.ArgumentException">An attempt is made to set this property to an invalid value. See <see cref="T:System.Security.Permissions.SecurityPermissionFlag" /> for the valid values.</exception>
		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06002753 RID: 10067 RVA: 0x0008F91B File Offset: 0x0008DB1B
		// (set) Token: 0x06002752 RID: 10066 RVA: 0x0008F90B File Offset: 0x0008DB0B
		public SecurityPermissionFlag Flags
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

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission.</param>
		/// <returns>
		///   <see langword="true" /> if the current permission is a subset of the specified permission; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002754 RID: 10068 RVA: 0x0008F924 File Offset: 0x0008DB24
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.m_flags == SecurityPermissionFlag.NoFlags;
			}
			SecurityPermission securityPermission = target as SecurityPermission;
			if (securityPermission != null)
			{
				return (this.m_flags & ~securityPermission.m_flags) == SecurityPermissionFlag.NoFlags;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[] { base.GetType().FullName }));
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002755 RID: 10069 RVA: 0x0008F980 File Offset: 0x0008DB80
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			if (!base.VerifyType(target))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[] { base.GetType().FullName }));
			}
			SecurityPermission securityPermission = (SecurityPermission)target;
			if (securityPermission.IsUnrestricted() || this.IsUnrestricted())
			{
				return new SecurityPermission(PermissionState.Unrestricted);
			}
			SecurityPermissionFlag securityPermissionFlag = this.m_flags | securityPermission.m_flags;
			return new SecurityPermission(securityPermissionFlag);
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission object that represents the intersection of the current permission and the specified permission. This new permission is <see langword="null" /> if the intersection is empty.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002756 RID: 10070 RVA: 0x0008F9F8 File Offset: 0x0008DBF8
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			if (!base.VerifyType(target))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[] { base.GetType().FullName }));
			}
			SecurityPermission securityPermission = (SecurityPermission)target;
			SecurityPermissionFlag securityPermissionFlag;
			if (securityPermission.IsUnrestricted())
			{
				if (this.IsUnrestricted())
				{
					return new SecurityPermission(PermissionState.Unrestricted);
				}
				securityPermissionFlag = this.m_flags;
			}
			else if (this.IsUnrestricted())
			{
				securityPermissionFlag = securityPermission.m_flags;
			}
			else
			{
				securityPermissionFlag = this.m_flags & securityPermission.m_flags;
			}
			if (securityPermissionFlag == SecurityPermissionFlag.NoFlags)
			{
				return null;
			}
			return new SecurityPermission(securityPermissionFlag);
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06002757 RID: 10071 RVA: 0x0008FA8A File Offset: 0x0008DC8A
		public override IPermission Copy()
		{
			if (this.IsUnrestricted())
			{
				return new SecurityPermission(PermissionState.Unrestricted);
			}
			return new SecurityPermission(this.m_flags);
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>
		///   <see langword="true" /> if the current permission is unrestricted; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002758 RID: 10072 RVA: 0x0008FAA6 File Offset: 0x0008DCA6
		public bool IsUnrestricted()
		{
			return this.m_flags == SecurityPermissionFlag.AllFlags;
		}

		// Token: 0x06002759 RID: 10073 RVA: 0x0008FAB5 File Offset: 0x0008DCB5
		private void VerifyAccess(SecurityPermissionFlag type)
		{
			if ((type & ~(SecurityPermissionFlag.Assertion | SecurityPermissionFlag.UnmanagedCode | SecurityPermissionFlag.SkipVerification | SecurityPermissionFlag.Execution | SecurityPermissionFlag.ControlThread | SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy | SecurityPermissionFlag.SerializationFormatter | SecurityPermissionFlag.ControlDomainPolicy | SecurityPermissionFlag.ControlPrincipal | SecurityPermissionFlag.ControlAppDomain | SecurityPermissionFlag.RemotingConfiguration | SecurityPermissionFlag.Infrastructure | SecurityPermissionFlag.BindingRedirects)) != SecurityPermissionFlag.NoFlags)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[] { (int)type }));
			}
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x0600275A RID: 10074 RVA: 0x0008FAE0 File Offset: 0x0008DCE0
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = CodeAccessPermission.CreatePermissionElement(this, "System.Security.Permissions.SecurityPermission");
			if (!this.IsUnrestricted())
			{
				securityElement.AddAttribute("Flags", XMLUtil.BitFieldEnumToString(typeof(SecurityPermissionFlag), this.m_flags));
			}
			else
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="esd">The XML encoding to use to reconstruct the permission.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="esd" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="esd" /> parameter is not a valid permission element.  
		///  -or-  
		///  The <paramref name="esd" /> parameter's version number is not supported.</exception>
		// Token: 0x0600275B RID: 10075 RVA: 0x0008FB3C File Offset: 0x0008DD3C
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.ValidateElement(esd, this);
			if (XMLUtil.IsUnrestricted(esd))
			{
				this.m_flags = SecurityPermissionFlag.AllFlags;
				return;
			}
			this.Reset();
			this.SetUnrestricted(false);
			string text = esd.Attribute("Flags");
			if (text != null)
			{
				this.m_flags = (SecurityPermissionFlag)Enum.Parse(typeof(SecurityPermissionFlag), text);
			}
		}

		// Token: 0x0600275C RID: 10076 RVA: 0x0008FB9B File Offset: 0x0008DD9B
		int IBuiltInPermission.GetTokenIndex()
		{
			return SecurityPermission.GetTokenIndex();
		}

		// Token: 0x0600275D RID: 10077 RVA: 0x0008FBA2 File Offset: 0x0008DDA2
		internal static int GetTokenIndex()
		{
			return 6;
		}

		// Token: 0x04000F3C RID: 3900
		private SecurityPermissionFlag m_flags;

		// Token: 0x04000F3D RID: 3901
		private const string _strHeaderAssertion = "Assertion";

		// Token: 0x04000F3E RID: 3902
		private const string _strHeaderUnmanagedCode = "UnmanagedCode";

		// Token: 0x04000F3F RID: 3903
		private const string _strHeaderExecution = "Execution";

		// Token: 0x04000F40 RID: 3904
		private const string _strHeaderSkipVerification = "SkipVerification";

		// Token: 0x04000F41 RID: 3905
		private const string _strHeaderControlThread = "ControlThread";

		// Token: 0x04000F42 RID: 3906
		private const string _strHeaderControlEvidence = "ControlEvidence";

		// Token: 0x04000F43 RID: 3907
		private const string _strHeaderControlPolicy = "ControlPolicy";

		// Token: 0x04000F44 RID: 3908
		private const string _strHeaderSerializationFormatter = "SerializationFormatter";

		// Token: 0x04000F45 RID: 3909
		private const string _strHeaderControlDomainPolicy = "ControlDomainPolicy";

		// Token: 0x04000F46 RID: 3910
		private const string _strHeaderControlPrincipal = "ControlPrincipal";

		// Token: 0x04000F47 RID: 3911
		private const string _strHeaderControlAppDomain = "ControlAppDomain";
	}
}
