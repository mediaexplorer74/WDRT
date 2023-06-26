using System;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
	/// <summary>Controls access to system and user environment variables. This class cannot be inherited.</summary>
	// Token: 0x020002DC RID: 732
	[ComVisible(true)]
	[Serializable]
	public sealed class EnvironmentPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.EnvironmentPermission" /> class with either restricted or unrestricted permission as specified.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />.</exception>
		// Token: 0x060025D0 RID: 9680 RVA: 0x0008AE52 File Offset: 0x00089052
		public EnvironmentPermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				this.m_unrestricted = true;
				return;
			}
			if (state == PermissionState.None)
			{
				this.m_unrestricted = false;
				return;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.EnvironmentPermission" /> class with the specified access to the specified environment variables.</summary>
		/// <param name="flag">One of the <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" /> values.</param>
		/// <param name="pathList">A list of environment variables (semicolon-separated) to which access is granted.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="pathList" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="flag" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" />.</exception>
		// Token: 0x060025D1 RID: 9681 RVA: 0x0008AE80 File Offset: 0x00089080
		public EnvironmentPermission(EnvironmentPermissionAccess flag, string pathList)
		{
			this.SetPathList(flag, pathList);
		}

		/// <summary>Sets the specified access to the specified environment variables to the existing state of the permission.</summary>
		/// <param name="flag">One of the <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" /> values.</param>
		/// <param name="pathList">A list of environment variables (semicolon-separated).</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="pathList" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="flag" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" />.</exception>
		// Token: 0x060025D2 RID: 9682 RVA: 0x0008AE90 File Offset: 0x00089090
		public void SetPathList(EnvironmentPermissionAccess flag, string pathList)
		{
			this.VerifyFlag(flag);
			this.m_unrestricted = false;
			if ((flag & EnvironmentPermissionAccess.Read) != EnvironmentPermissionAccess.NoAccess)
			{
				this.m_read = null;
			}
			if ((flag & EnvironmentPermissionAccess.Write) != EnvironmentPermissionAccess.NoAccess)
			{
				this.m_write = null;
			}
			this.AddPathList(flag, pathList);
		}

		/// <summary>Adds access for the specified environment variables to the existing state of the permission.</summary>
		/// <param name="flag">One of the <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" /> values.</param>
		/// <param name="pathList">A list of environment variables (semicolon-separated).</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="pathList" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="flag" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" />.</exception>
		// Token: 0x060025D3 RID: 9683 RVA: 0x0008AEC0 File Offset: 0x000890C0
		[SecuritySafeCritical]
		public void AddPathList(EnvironmentPermissionAccess flag, string pathList)
		{
			this.VerifyFlag(flag);
			if (this.FlagIsSet(flag, EnvironmentPermissionAccess.Read))
			{
				if (this.m_read == null)
				{
					this.m_read = new EnvironmentStringExpressionSet();
				}
				this.m_read.AddExpressions(pathList);
			}
			if (this.FlagIsSet(flag, EnvironmentPermissionAccess.Write))
			{
				if (this.m_write == null)
				{
					this.m_write = new EnvironmentStringExpressionSet();
				}
				this.m_write.AddExpressions(pathList);
			}
		}

		/// <summary>Gets all environment variables with the specified <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" />.</summary>
		/// <param name="flag">One of the <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" /> values that represents a single type of environment variable access.</param>
		/// <returns>A list of environment variables (semicolon-separated) for the selected flag.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="flag" /> is not a valid value of <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" />.  
		/// -or-  
		/// <paramref name="flag" /> is <see cref="F:System.Security.Permissions.EnvironmentPermissionAccess.AllAccess" />, which represents more than one type of environment variable access, or <see cref="F:System.Security.Permissions.EnvironmentPermissionAccess.NoAccess" />, which does not represent any type of environment variable access.</exception>
		// Token: 0x060025D4 RID: 9684 RVA: 0x0008AF28 File Offset: 0x00089128
		public string GetPathList(EnvironmentPermissionAccess flag)
		{
			this.VerifyFlag(flag);
			this.ExclusiveFlag(flag);
			if (this.FlagIsSet(flag, EnvironmentPermissionAccess.Read))
			{
				if (this.m_read == null)
				{
					return "";
				}
				return this.m_read.ToString();
			}
			else
			{
				if (!this.FlagIsSet(flag, EnvironmentPermissionAccess.Write))
				{
					return "";
				}
				if (this.m_write == null)
				{
					return "";
				}
				return this.m_write.ToString();
			}
		}

		// Token: 0x060025D5 RID: 9685 RVA: 0x0008AF90 File Offset: 0x00089190
		private void VerifyFlag(EnvironmentPermissionAccess flag)
		{
			if ((flag & ~(EnvironmentPermissionAccess.Read | EnvironmentPermissionAccess.Write)) != EnvironmentPermissionAccess.NoAccess)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[] { (int)flag }));
			}
		}

		// Token: 0x060025D6 RID: 9686 RVA: 0x0008AFB7 File Offset: 0x000891B7
		private void ExclusiveFlag(EnvironmentPermissionAccess flag)
		{
			if (flag == EnvironmentPermissionAccess.NoAccess)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumNotSingleFlag"));
			}
			if ((flag & (flag - 1)) != EnvironmentPermissionAccess.NoAccess)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumNotSingleFlag"));
			}
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x0008AFE3 File Offset: 0x000891E3
		private bool FlagIsSet(EnvironmentPermissionAccess flag, EnvironmentPermissionAccess question)
		{
			return (flag & question) > EnvironmentPermissionAccess.NoAccess;
		}

		// Token: 0x060025D8 RID: 9688 RVA: 0x0008AFEB File Offset: 0x000891EB
		private bool IsEmpty()
		{
			return !this.m_unrestricted && (this.m_read == null || this.m_read.IsEmpty()) && (this.m_write == null || this.m_write.IsEmpty());
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>
		///   <see langword="true" /> if the current permission is unrestricted; otherwise, <see langword="false" />.</returns>
		// Token: 0x060025D9 RID: 9689 RVA: 0x0008B021 File Offset: 0x00089221
		public bool IsUnrestricted()
		{
			return this.m_unrestricted;
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission.</param>
		/// <returns>
		///   <see langword="true" /> if the current permission is a subset of the specified permission; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x060025DA RID: 9690 RVA: 0x0008B02C File Offset: 0x0008922C
		[SecuritySafeCritical]
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.IsEmpty();
			}
			bool flag;
			try
			{
				EnvironmentPermission environmentPermission = (EnvironmentPermission)target;
				if (environmentPermission.IsUnrestricted())
				{
					flag = true;
				}
				else if (this.IsUnrestricted())
				{
					flag = false;
				}
				else
				{
					flag = (this.m_read == null || this.m_read.IsSubsetOf(environmentPermission.m_read)) && (this.m_write == null || this.m_write.IsSubsetOf(environmentPermission.m_write));
				}
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[] { base.GetType().FullName }));
			}
			return flag;
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is <see langword="null" /> if the intersection is empty.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x060025DB RID: 9691 RVA: 0x0008B0D8 File Offset: 0x000892D8
		[SecuritySafeCritical]
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
			if (this.IsUnrestricted())
			{
				return target.Copy();
			}
			EnvironmentPermission environmentPermission = (EnvironmentPermission)target;
			if (environmentPermission.IsUnrestricted())
			{
				return this.Copy();
			}
			StringExpressionSet stringExpressionSet = ((this.m_read == null) ? null : this.m_read.Intersect(environmentPermission.m_read));
			StringExpressionSet stringExpressionSet2 = ((this.m_write == null) ? null : this.m_write.Intersect(environmentPermission.m_write));
			if ((stringExpressionSet == null || stringExpressionSet.IsEmpty()) && (stringExpressionSet2 == null || stringExpressionSet2.IsEmpty()))
			{
				return null;
			}
			return new EnvironmentPermission(PermissionState.None)
			{
				m_unrestricted = false,
				m_read = stringExpressionSet,
				m_write = stringExpressionSet2
			};
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <param name="other">A permission to combine with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="other" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x060025DC RID: 9692 RVA: 0x0008B1AC File Offset: 0x000893AC
		[SecuritySafeCritical]
		public override IPermission Union(IPermission other)
		{
			if (other == null)
			{
				return this.Copy();
			}
			if (!base.VerifyType(other))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[] { base.GetType().FullName }));
			}
			EnvironmentPermission environmentPermission = (EnvironmentPermission)other;
			if (this.IsUnrestricted() || environmentPermission.IsUnrestricted())
			{
				return new EnvironmentPermission(PermissionState.Unrestricted);
			}
			StringExpressionSet stringExpressionSet = ((this.m_read == null) ? environmentPermission.m_read : this.m_read.Union(environmentPermission.m_read));
			StringExpressionSet stringExpressionSet2 = ((this.m_write == null) ? environmentPermission.m_write : this.m_write.Union(environmentPermission.m_write));
			if ((stringExpressionSet == null || stringExpressionSet.IsEmpty()) && (stringExpressionSet2 == null || stringExpressionSet2.IsEmpty()))
			{
				return null;
			}
			return new EnvironmentPermission(PermissionState.None)
			{
				m_unrestricted = false,
				m_read = stringExpressionSet,
				m_write = stringExpressionSet2
			};
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x060025DD RID: 9693 RVA: 0x0008B288 File Offset: 0x00089488
		public override IPermission Copy()
		{
			EnvironmentPermission environmentPermission = new EnvironmentPermission(PermissionState.None);
			if (this.m_unrestricted)
			{
				environmentPermission.m_unrestricted = true;
			}
			else
			{
				environmentPermission.m_unrestricted = false;
				if (this.m_read != null)
				{
					environmentPermission.m_read = this.m_read.Copy();
				}
				if (this.m_write != null)
				{
					environmentPermission.m_write = this.m_write.Copy();
				}
			}
			return environmentPermission;
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x060025DE RID: 9694 RVA: 0x0008B2E8 File Offset: 0x000894E8
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = CodeAccessPermission.CreatePermissionElement(this, "System.Security.Permissions.EnvironmentPermission");
			if (!this.IsUnrestricted())
			{
				if (this.m_read != null && !this.m_read.IsEmpty())
				{
					securityElement.AddAttribute("Read", SecurityElement.Escape(this.m_read.ToString()));
				}
				if (this.m_write != null && !this.m_write.IsEmpty())
				{
					securityElement.AddAttribute("Write", SecurityElement.Escape(this.m_write.ToString()));
				}
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
		///  The <paramref name="esd" /> parameter's version number is not valid.</exception>
		// Token: 0x060025DF RID: 9695 RVA: 0x0008B37C File Offset: 0x0008957C
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.ValidateElement(esd, this);
			if (XMLUtil.IsUnrestricted(esd))
			{
				this.m_unrestricted = true;
				return;
			}
			this.m_unrestricted = false;
			this.m_read = null;
			this.m_write = null;
			string text = esd.Attribute("Read");
			if (text != null)
			{
				this.m_read = new EnvironmentStringExpressionSet(text);
			}
			text = esd.Attribute("Write");
			if (text != null)
			{
				this.m_write = new EnvironmentStringExpressionSet(text);
			}
		}

		// Token: 0x060025E0 RID: 9696 RVA: 0x0008B3EB File Offset: 0x000895EB
		int IBuiltInPermission.GetTokenIndex()
		{
			return EnvironmentPermission.GetTokenIndex();
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x0008B3F2 File Offset: 0x000895F2
		internal static int GetTokenIndex()
		{
			return 0;
		}

		// Token: 0x04000E7B RID: 3707
		private StringExpressionSet m_read;

		// Token: 0x04000E7C RID: 3708
		private StringExpressionSet m_write;

		// Token: 0x04000E7D RID: 3709
		private bool m_unrestricted;
	}
}
