using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.Security.Util;

namespace System.Security.Permissions
{
	/// <summary>Controls the ability to access registry variables. This class cannot be inherited.</summary>
	// Token: 0x02000319 RID: 793
	[ComVisible(true)]
	[Serializable]
	public sealed class RegistryPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.RegistryPermission" /> class with either fully restricted or unrestricted permission as specified.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />.</exception>
		// Token: 0x06002819 RID: 10265 RVA: 0x000935D1 File Offset: 0x000917D1
		public RegistryPermission(PermissionState state)
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

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.RegistryPermission" /> class with the specified access to the specified registry variables.</summary>
		/// <param name="access">One of the <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> values.</param>
		/// <param name="pathList">A list of registry variables (semicolon-separated) to which access is granted.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.  
		///  -or-  
		///  The <paramref name="pathList" /> parameter is not a valid string.</exception>
		// Token: 0x0600281A RID: 10266 RVA: 0x000935FF File Offset: 0x000917FF
		public RegistryPermission(RegistryPermissionAccess access, string pathList)
		{
			this.SetPathList(access, pathList);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.RegistryPermission" /> class with the specified access to the specified registry variables and the specified access rights to registry control information.</summary>
		/// <param name="access">One of the <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> values.</param>
		/// <param name="control">A bitwise combination of the <see cref="T:System.Security.AccessControl.AccessControlActions" /> values.</param>
		/// <param name="pathList">A list of registry variables (semicolon-separated) to which access is granted.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.  
		///  -or-  
		///  The <paramref name="pathList" /> parameter is not a valid string.</exception>
		// Token: 0x0600281B RID: 10267 RVA: 0x0009360F File Offset: 0x0009180F
		public RegistryPermission(RegistryPermissionAccess access, AccessControlActions control, string pathList)
		{
			this.m_unrestricted = false;
			this.AddPathList(access, control, pathList);
		}

		/// <summary>Sets new access for the specified registry variable names to the existing state of the permission.</summary>
		/// <param name="access">One of the <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> values.</param>
		/// <param name="pathList">A list of registry variables (semicolon-separated).</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.  
		///  -or-  
		///  The <paramref name="pathList" /> parameter is not a valid string.</exception>
		// Token: 0x0600281C RID: 10268 RVA: 0x00093627 File Offset: 0x00091827
		public void SetPathList(RegistryPermissionAccess access, string pathList)
		{
			this.VerifyAccess(access);
			this.m_unrestricted = false;
			if ((access & RegistryPermissionAccess.Read) != RegistryPermissionAccess.NoAccess)
			{
				this.m_read = null;
			}
			if ((access & RegistryPermissionAccess.Write) != RegistryPermissionAccess.NoAccess)
			{
				this.m_write = null;
			}
			if ((access & RegistryPermissionAccess.Create) != RegistryPermissionAccess.NoAccess)
			{
				this.m_create = null;
			}
			this.AddPathList(access, pathList);
		}

		// Token: 0x0600281D RID: 10269 RVA: 0x00093663 File Offset: 0x00091863
		internal void SetPathList(AccessControlActions control, string pathList)
		{
			this.m_unrestricted = false;
			if ((control & AccessControlActions.View) != AccessControlActions.None)
			{
				this.m_viewAcl = null;
			}
			if ((control & AccessControlActions.Change) != AccessControlActions.None)
			{
				this.m_changeAcl = null;
			}
			this.AddPathList(RegistryPermissionAccess.NoAccess, control, pathList);
		}

		/// <summary>Adds access for the specified registry variables to the existing state of the permission.</summary>
		/// <param name="access">One of the <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> values.</param>
		/// <param name="pathList">A list of registry variables (semicolon-separated).</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.  
		///  -or-  
		///  The <paramref name="pathList" /> parameter is not a valid string.</exception>
		// Token: 0x0600281E RID: 10270 RVA: 0x0009368D File Offset: 0x0009188D
		public void AddPathList(RegistryPermissionAccess access, string pathList)
		{
			this.AddPathList(access, AccessControlActions.None, pathList);
		}

		/// <summary>Adds access for the specified registry variables to the existing state of the permission, specifying registry permission access and access control actions.</summary>
		/// <param name="access">One of the <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> values.</param>
		/// <param name="control">One of the <see cref="T:System.Security.AccessControl.AccessControlActions" /> values. </param>
		/// <param name="pathList">A list of registry variables (separated by semicolons).</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.  
		///  -or-  
		///  The <paramref name="pathList" /> parameter is not a valid string.</exception>
		// Token: 0x0600281F RID: 10271 RVA: 0x00093698 File Offset: 0x00091898
		[SecuritySafeCritical]
		public void AddPathList(RegistryPermissionAccess access, AccessControlActions control, string pathList)
		{
			this.VerifyAccess(access);
			if ((access & RegistryPermissionAccess.Read) != RegistryPermissionAccess.NoAccess)
			{
				if (this.m_read == null)
				{
					this.m_read = new StringExpressionSet();
				}
				this.m_read.AddExpressions(pathList);
			}
			if ((access & RegistryPermissionAccess.Write) != RegistryPermissionAccess.NoAccess)
			{
				if (this.m_write == null)
				{
					this.m_write = new StringExpressionSet();
				}
				this.m_write.AddExpressions(pathList);
			}
			if ((access & RegistryPermissionAccess.Create) != RegistryPermissionAccess.NoAccess)
			{
				if (this.m_create == null)
				{
					this.m_create = new StringExpressionSet();
				}
				this.m_create.AddExpressions(pathList);
			}
			if ((control & AccessControlActions.View) != AccessControlActions.None)
			{
				if (this.m_viewAcl == null)
				{
					this.m_viewAcl = new StringExpressionSet();
				}
				this.m_viewAcl.AddExpressions(pathList);
			}
			if ((control & AccessControlActions.Change) != AccessControlActions.None)
			{
				if (this.m_changeAcl == null)
				{
					this.m_changeAcl = new StringExpressionSet();
				}
				this.m_changeAcl.AddExpressions(pathList);
			}
		}

		/// <summary>Gets paths for all registry variables with the specified <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.</summary>
		/// <param name="access">One of the <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> values that represents a single type of registry variable access.</param>
		/// <returns>A list of the registry variables (semicolon-separated) with the specified <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="access" /> is not a valid value of <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.  
		/// -or-  
		/// <paramref name="access" /> is <see cref="F:System.Security.Permissions.RegistryPermissionAccess.AllAccess" />, which represents more than one type of registry variable access, or <see cref="F:System.Security.Permissions.RegistryPermissionAccess.NoAccess" />, which does not represent any type of registry variable access.</exception>
		// Token: 0x06002820 RID: 10272 RVA: 0x00093760 File Offset: 0x00091960
		[SecuritySafeCritical]
		public string GetPathList(RegistryPermissionAccess access)
		{
			this.VerifyAccess(access);
			this.ExclusiveAccess(access);
			if ((access & RegistryPermissionAccess.Read) != RegistryPermissionAccess.NoAccess)
			{
				if (this.m_read == null)
				{
					return "";
				}
				return this.m_read.UnsafeToString();
			}
			else if ((access & RegistryPermissionAccess.Write) != RegistryPermissionAccess.NoAccess)
			{
				if (this.m_write == null)
				{
					return "";
				}
				return this.m_write.UnsafeToString();
			}
			else
			{
				if ((access & RegistryPermissionAccess.Create) == RegistryPermissionAccess.NoAccess)
				{
					return "";
				}
				if (this.m_create == null)
				{
					return "";
				}
				return this.m_create.UnsafeToString();
			}
		}

		// Token: 0x06002821 RID: 10273 RVA: 0x000937DD File Offset: 0x000919DD
		private void VerifyAccess(RegistryPermissionAccess access)
		{
			if ((access & ~(RegistryPermissionAccess.Read | RegistryPermissionAccess.Write | RegistryPermissionAccess.Create)) != RegistryPermissionAccess.NoAccess)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[] { (int)access }));
			}
		}

		// Token: 0x06002822 RID: 10274 RVA: 0x00093804 File Offset: 0x00091A04
		private void ExclusiveAccess(RegistryPermissionAccess access)
		{
			if (access == RegistryPermissionAccess.NoAccess)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumNotSingleFlag"));
			}
			if ((access & (access - 1)) != RegistryPermissionAccess.NoAccess)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumNotSingleFlag"));
			}
		}

		// Token: 0x06002823 RID: 10275 RVA: 0x00093830 File Offset: 0x00091A30
		private bool IsEmpty()
		{
			return !this.m_unrestricted && (this.m_read == null || this.m_read.IsEmpty()) && (this.m_write == null || this.m_write.IsEmpty()) && (this.m_create == null || this.m_create.IsEmpty()) && (this.m_viewAcl == null || this.m_viewAcl.IsEmpty()) && (this.m_changeAcl == null || this.m_changeAcl.IsEmpty());
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>
		///   <see langword="true" /> if the current permission is unrestricted; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002824 RID: 10276 RVA: 0x000938B0 File Offset: 0x00091AB0
		public bool IsUnrestricted()
		{
			return this.m_unrestricted;
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission.</param>
		/// <returns>
		///   <see langword="true" /> if the current permission is a subset of the specified permission; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002825 RID: 10277 RVA: 0x000938B8 File Offset: 0x00091AB8
		[SecuritySafeCritical]
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.IsEmpty();
			}
			RegistryPermission registryPermission = target as RegistryPermission;
			if (registryPermission == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[] { base.GetType().FullName }));
			}
			return registryPermission.IsUnrestricted() || (!this.IsUnrestricted() && ((this.m_read == null || this.m_read.IsSubsetOf(registryPermission.m_read)) && (this.m_write == null || this.m_write.IsSubsetOf(registryPermission.m_write)) && (this.m_create == null || this.m_create.IsSubsetOf(registryPermission.m_create)) && (this.m_viewAcl == null || this.m_viewAcl.IsSubsetOf(registryPermission.m_viewAcl))) && (this.m_changeAcl == null || this.m_changeAcl.IsSubsetOf(registryPermission.m_changeAcl)));
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is <see langword="null" /> if the intersection is empty.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002826 RID: 10278 RVA: 0x0009399C File Offset: 0x00091B9C
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
			RegistryPermission registryPermission = (RegistryPermission)target;
			if (registryPermission.IsUnrestricted())
			{
				return this.Copy();
			}
			StringExpressionSet stringExpressionSet = ((this.m_read == null) ? null : this.m_read.Intersect(registryPermission.m_read));
			StringExpressionSet stringExpressionSet2 = ((this.m_write == null) ? null : this.m_write.Intersect(registryPermission.m_write));
			StringExpressionSet stringExpressionSet3 = ((this.m_create == null) ? null : this.m_create.Intersect(registryPermission.m_create));
			StringExpressionSet stringExpressionSet4 = ((this.m_viewAcl == null) ? null : this.m_viewAcl.Intersect(registryPermission.m_viewAcl));
			StringExpressionSet stringExpressionSet5 = ((this.m_changeAcl == null) ? null : this.m_changeAcl.Intersect(registryPermission.m_changeAcl));
			if ((stringExpressionSet == null || stringExpressionSet.IsEmpty()) && (stringExpressionSet2 == null || stringExpressionSet2.IsEmpty()) && (stringExpressionSet3 == null || stringExpressionSet3.IsEmpty()) && (stringExpressionSet4 == null || stringExpressionSet4.IsEmpty()) && (stringExpressionSet5 == null || stringExpressionSet5.IsEmpty()))
			{
				return null;
			}
			return new RegistryPermission(PermissionState.None)
			{
				m_unrestricted = false,
				m_read = stringExpressionSet,
				m_write = stringExpressionSet2,
				m_create = stringExpressionSet3,
				m_viewAcl = stringExpressionSet4,
				m_changeAcl = stringExpressionSet5
			};
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <param name="other">A permission to combine with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="other" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002827 RID: 10279 RVA: 0x00093B0C File Offset: 0x00091D0C
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
			RegistryPermission registryPermission = (RegistryPermission)other;
			if (this.IsUnrestricted() || registryPermission.IsUnrestricted())
			{
				return new RegistryPermission(PermissionState.Unrestricted);
			}
			StringExpressionSet stringExpressionSet = ((this.m_read == null) ? registryPermission.m_read : this.m_read.Union(registryPermission.m_read));
			StringExpressionSet stringExpressionSet2 = ((this.m_write == null) ? registryPermission.m_write : this.m_write.Union(registryPermission.m_write));
			StringExpressionSet stringExpressionSet3 = ((this.m_create == null) ? registryPermission.m_create : this.m_create.Union(registryPermission.m_create));
			StringExpressionSet stringExpressionSet4 = ((this.m_viewAcl == null) ? registryPermission.m_viewAcl : this.m_viewAcl.Union(registryPermission.m_viewAcl));
			StringExpressionSet stringExpressionSet5 = ((this.m_changeAcl == null) ? registryPermission.m_changeAcl : this.m_changeAcl.Union(registryPermission.m_changeAcl));
			if ((stringExpressionSet == null || stringExpressionSet.IsEmpty()) && (stringExpressionSet2 == null || stringExpressionSet2.IsEmpty()) && (stringExpressionSet3 == null || stringExpressionSet3.IsEmpty()) && (stringExpressionSet4 == null || stringExpressionSet4.IsEmpty()) && (stringExpressionSet5 == null || stringExpressionSet5.IsEmpty()))
			{
				return null;
			}
			return new RegistryPermission(PermissionState.None)
			{
				m_unrestricted = false,
				m_read = stringExpressionSet,
				m_write = stringExpressionSet2,
				m_create = stringExpressionSet3,
				m_viewAcl = stringExpressionSet4,
				m_changeAcl = stringExpressionSet5
			};
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06002828 RID: 10280 RVA: 0x00093C94 File Offset: 0x00091E94
		public override IPermission Copy()
		{
			RegistryPermission registryPermission = new RegistryPermission(PermissionState.None);
			if (this.m_unrestricted)
			{
				registryPermission.m_unrestricted = true;
			}
			else
			{
				registryPermission.m_unrestricted = false;
				if (this.m_read != null)
				{
					registryPermission.m_read = this.m_read.Copy();
				}
				if (this.m_write != null)
				{
					registryPermission.m_write = this.m_write.Copy();
				}
				if (this.m_create != null)
				{
					registryPermission.m_create = this.m_create.Copy();
				}
				if (this.m_viewAcl != null)
				{
					registryPermission.m_viewAcl = this.m_viewAcl.Copy();
				}
				if (this.m_changeAcl != null)
				{
					registryPermission.m_changeAcl = this.m_changeAcl.Copy();
				}
			}
			return registryPermission;
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x06002829 RID: 10281 RVA: 0x00093D44 File Offset: 0x00091F44
		[SecuritySafeCritical]
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = CodeAccessPermission.CreatePermissionElement(this, "System.Security.Permissions.RegistryPermission");
			if (!this.IsUnrestricted())
			{
				if (this.m_read != null && !this.m_read.IsEmpty())
				{
					securityElement.AddAttribute("Read", SecurityElement.Escape(this.m_read.UnsafeToString()));
				}
				if (this.m_write != null && !this.m_write.IsEmpty())
				{
					securityElement.AddAttribute("Write", SecurityElement.Escape(this.m_write.UnsafeToString()));
				}
				if (this.m_create != null && !this.m_create.IsEmpty())
				{
					securityElement.AddAttribute("Create", SecurityElement.Escape(this.m_create.UnsafeToString()));
				}
				if (this.m_viewAcl != null && !this.m_viewAcl.IsEmpty())
				{
					securityElement.AddAttribute("ViewAccessControl", SecurityElement.Escape(this.m_viewAcl.UnsafeToString()));
				}
				if (this.m_changeAcl != null && !this.m_changeAcl.IsEmpty())
				{
					securityElement.AddAttribute("ChangeAccessControl", SecurityElement.Escape(this.m_changeAcl.UnsafeToString()));
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
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="elem" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="elem" /> parameter is not a valid permission element.  
		///  -or-  
		///  The <paramref name="elem" /> parameter's version number is not valid.</exception>
		// Token: 0x0600282A RID: 10282 RVA: 0x00093E6C File Offset: 0x0009206C
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
			this.m_create = null;
			this.m_viewAcl = null;
			this.m_changeAcl = null;
			string text = esd.Attribute("Read");
			if (text != null)
			{
				this.m_read = new StringExpressionSet(text);
			}
			text = esd.Attribute("Write");
			if (text != null)
			{
				this.m_write = new StringExpressionSet(text);
			}
			text = esd.Attribute("Create");
			if (text != null)
			{
				this.m_create = new StringExpressionSet(text);
			}
			text = esd.Attribute("ViewAccessControl");
			if (text != null)
			{
				this.m_viewAcl = new StringExpressionSet(text);
			}
			text = esd.Attribute("ChangeAccessControl");
			if (text != null)
			{
				this.m_changeAcl = new StringExpressionSet(text);
			}
		}

		// Token: 0x0600282B RID: 10283 RVA: 0x00093F41 File Offset: 0x00092141
		int IBuiltInPermission.GetTokenIndex()
		{
			return RegistryPermission.GetTokenIndex();
		}

		// Token: 0x0600282C RID: 10284 RVA: 0x00093F48 File Offset: 0x00092148
		internal static int GetTokenIndex()
		{
			return 5;
		}

		// Token: 0x04000F85 RID: 3973
		private StringExpressionSet m_read;

		// Token: 0x04000F86 RID: 3974
		private StringExpressionSet m_write;

		// Token: 0x04000F87 RID: 3975
		private StringExpressionSet m_create;

		// Token: 0x04000F88 RID: 3976
		[OptionalField(VersionAdded = 2)]
		private StringExpressionSet m_viewAcl;

		// Token: 0x04000F89 RID: 3977
		[OptionalField(VersionAdded = 2)]
		private StringExpressionSet m_changeAcl;

		// Token: 0x04000F8A RID: 3978
		private bool m_unrestricted;
	}
}
