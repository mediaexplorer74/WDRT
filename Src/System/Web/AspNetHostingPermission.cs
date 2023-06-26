using System;
using System.Security;
using System.Security.Permissions;

namespace System.Web
{
	/// <summary>Controls access permissions in ASP.NET hosted environments. This class cannot be inherited.</summary>
	// Token: 0x0200006A RID: 106
	[Serializable]
	public sealed class AspNetHostingPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		// Token: 0x06000461 RID: 1121 RVA: 0x0001EAA2 File Offset: 0x0001CCA2
		internal static void VerifyAspNetHostingPermissionLevel(AspNetHostingPermissionLevel level, string arg)
		{
			if (level <= AspNetHostingPermissionLevel.Low)
			{
				if (level == AspNetHostingPermissionLevel.None || level == AspNetHostingPermissionLevel.Minimal || level == AspNetHostingPermissionLevel.Low)
				{
					return;
				}
			}
			else if (level == AspNetHostingPermissionLevel.Medium || level == AspNetHostingPermissionLevel.High || level == AspNetHostingPermissionLevel.Unrestricted)
			{
				return;
			}
			throw new ArgumentException(arg);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.AspNetHostingPermission" /> class with the specified <see cref="T:System.Security.Permissions.PermissionState" /> enumeration value.</summary>
		/// <param name="state">A <see cref="T:System.Security.Permissions.PermissionState" /> enumeration value.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="state" /> is not set to one of the <see cref="T:System.Security.Permissions.PermissionState" /> enumeration values.</exception>
		// Token: 0x06000462 RID: 1122 RVA: 0x0001EAE4 File Offset: 0x0001CCE4
		public AspNetHostingPermission(PermissionState state)
		{
			if (state == PermissionState.None)
			{
				this._level = AspNetHostingPermissionLevel.None;
				return;
			}
			if (state == PermissionState.Unrestricted)
			{
				this._level = AspNetHostingPermissionLevel.Unrestricted;
				return;
			}
			throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
			{
				state.ToString(),
				"state"
			}));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.AspNetHostingPermission" /> class with the specified permission level.</summary>
		/// <param name="level">An <see cref="T:System.Web.AspNetHostingPermissionLevel" /> enumeration value.</param>
		// Token: 0x06000463 RID: 1123 RVA: 0x0001EB40 File Offset: 0x0001CD40
		public AspNetHostingPermission(AspNetHostingPermissionLevel level)
		{
			AspNetHostingPermission.VerifyAspNetHostingPermissionLevel(level, "level");
			this._level = level;
		}

		/// <summary>Gets or sets the current hosting permission level for an ASP.NET application.</summary>
		/// <returns>One of the <see cref="T:System.Web.AspNetHostingPermissionLevel" /> enumeration values.</returns>
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x0001EB5A File Offset: 0x0001CD5A
		// (set) Token: 0x06000465 RID: 1125 RVA: 0x0001EB62 File Offset: 0x0001CD62
		public AspNetHostingPermissionLevel Level
		{
			get
			{
				return this._level;
			}
			set
			{
				AspNetHostingPermission.VerifyAspNetHostingPermissionLevel(value, "Level");
				this._level = value;
			}
		}

		/// <summary>Returns a value indicating whether unrestricted access to the resource that is protected by the current permission is allowed.</summary>
		/// <returns>
		///   <see langword="true" /> if unrestricted use of the resource protected by the permission is allowed; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000466 RID: 1126 RVA: 0x0001EB76 File Offset: 0x0001CD76
		public bool IsUnrestricted()
		{
			return this._level == AspNetHostingPermissionLevel.Unrestricted;
		}

		/// <summary>When implemented by a derived class, creates and returns an identical copy of the current permission object.</summary>
		/// <returns>A copy of the current permission object.</returns>
		// Token: 0x06000467 RID: 1127 RVA: 0x0001EB85 File Offset: 0x0001CD85
		public override IPermission Copy()
		{
			return new AspNetHostingPermission(this._level);
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>An <see cref="T:System.Security.IPermission" /> that represents the union of the current permission and the specified permission.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not an <see cref="T:System.Web.AspNetHostingPermission" />.</exception>
		// Token: 0x06000468 RID: 1128 RVA: 0x0001EB94 File Offset: 0x0001CD94
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			if (target.GetType() != typeof(AspNetHostingPermission))
			{
				throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
				{
					(target == null) ? "null" : target.ToString(),
					"target"
				}));
			}
			AspNetHostingPermission aspNetHostingPermission = (AspNetHostingPermission)target;
			if (this.Level >= aspNetHostingPermission.Level)
			{
				return new AspNetHostingPermission(this.Level);
			}
			return new AspNetHostingPermission(aspNetHostingPermission.Level);
		}

		/// <summary>When implemented by a derived class, creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>An <see cref="T:System.Security.IPermission" /> that represents the intersection of the current permission and the specified permission; otherwise, <see langword="null" /> if the intersection is empty.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not an <see cref="T:System.Web.AspNetHostingPermission" />.</exception>
		// Token: 0x06000469 RID: 1129 RVA: 0x0001EC20 File Offset: 0x0001CE20
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			if (target.GetType() != typeof(AspNetHostingPermission))
			{
				throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
				{
					(target == null) ? "null" : target.ToString(),
					"target"
				}));
			}
			AspNetHostingPermission aspNetHostingPermission = (AspNetHostingPermission)target;
			if (this.Level <= aspNetHostingPermission.Level)
			{
				return new AspNetHostingPermission(this.Level);
			}
			return new AspNetHostingPermission(aspNetHostingPermission.Level);
		}

		/// <summary>Returns a value indicating whether the current permission is a subset of the specified permission.</summary>
		/// <param name="target">The <see cref="T:System.Security.IPermission" /> to combine with the current permission. It must be of the same type as the current <see cref="T:System.Security.IPermission" />.</param>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Security.IPermission" /> is a subset of the specified <see cref="T:System.Security.IPermission" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not an <see cref="T:System.Web.AspNetHostingPermission" />.</exception>
		// Token: 0x0600046A RID: 1130 RVA: 0x0001ECA8 File Offset: 0x0001CEA8
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this._level == AspNetHostingPermissionLevel.None;
			}
			if (target.GetType() != typeof(AspNetHostingPermission))
			{
				throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
				{
					(target == null) ? "null" : target.ToString(),
					"target"
				}));
			}
			AspNetHostingPermission aspNetHostingPermission = (AspNetHostingPermission)target;
			return this.Level <= aspNetHostingPermission.Level;
		}

		/// <summary>Reconstructs a permission object with a specified state from an XML encoding.</summary>
		/// <param name="securityElement">The <see cref="T:System.Security.SecurityElement" /> containing the XML encoding to use to reconstruct the permission object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="securityElement" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.SecurityElement.Tag" /> property of <paramref name="securityElement" /> is not equal to "IPermission".  
		/// -or-  
		///  The class <see cref="M:System.Security.SecurityElement.Attribute(System.String)" /> of <paramref name="securityElement" /> is <see langword="null" /> or an empty string ("").</exception>
		// Token: 0x0600046B RID: 1131 RVA: 0x0001ED24 File Offset: 0x0001CF24
		public override void FromXml(SecurityElement securityElement)
		{
			if (securityElement == null)
			{
				throw new ArgumentNullException(SR.GetString("AspNetHostingPermissionBadXml", new object[] { "securityElement" }));
			}
			if (!securityElement.Tag.Equals("IPermission"))
			{
				throw new ArgumentException(SR.GetString("AspNetHostingPermissionBadXml", new object[] { "securityElement" }));
			}
			string text = securityElement.Attribute("class");
			if (text == null)
			{
				throw new ArgumentException(SR.GetString("AspNetHostingPermissionBadXml", new object[] { "securityElement" }));
			}
			if (text.IndexOf(base.GetType().FullName, StringComparison.Ordinal) < 0)
			{
				throw new ArgumentException(SR.GetString("AspNetHostingPermissionBadXml", new object[] { "securityElement" }));
			}
			string text2 = securityElement.Attribute("version");
			if (string.Compare(text2, "1", StringComparison.OrdinalIgnoreCase) != 0)
			{
				throw new ArgumentException(SR.GetString("AspNetHostingPermissionBadXml", new object[] { "version" }));
			}
			string text3 = securityElement.Attribute("Level");
			if (text3 == null)
			{
				this._level = AspNetHostingPermissionLevel.None;
				return;
			}
			this._level = (AspNetHostingPermissionLevel)Enum.Parse(typeof(AspNetHostingPermissionLevel), text3);
		}

		/// <summary>Creates an XML encoding of the permission object and its current state.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> containing the XML encoding of the permission object, including any state information.</returns>
		// Token: 0x0600046C RID: 1132 RVA: 0x0001EE50 File Offset: 0x0001D050
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", base.GetType().FullName + ", " + base.GetType().Module.Assembly.FullName.Replace('"', '\''));
			securityElement.AddAttribute("version", "1");
			securityElement.AddAttribute("Level", Enum.GetName(typeof(AspNetHostingPermissionLevel), this._level));
			if (this.IsUnrestricted())
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		// Token: 0x04000BC9 RID: 3017
		private AspNetHostingPermissionLevel _level;
	}
}
