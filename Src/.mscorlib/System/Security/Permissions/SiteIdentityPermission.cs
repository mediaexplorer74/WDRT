using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Util;

namespace System.Security.Permissions
{
	/// <summary>Defines the identity permission for the Web site from which the code originates. This class cannot be inherited.</summary>
	// Token: 0x02000306 RID: 774
	[ComVisible(true)]
	[Serializable]
	public sealed class SiteIdentityPermission : CodeAccessPermission, IBuiltInPermission
	{
		// Token: 0x0600275E RID: 10078 RVA: 0x0008FBA8 File Offset: 0x0008DDA8
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			if (this.m_serializedPermission != null)
			{
				this.FromXml(SecurityElement.FromString(this.m_serializedPermission));
				this.m_serializedPermission = null;
				return;
			}
			if (this.m_site != null)
			{
				this.m_unrestricted = false;
				this.m_sites = new SiteString[1];
				this.m_sites[0] = this.m_site;
				this.m_site = null;
			}
		}

		// Token: 0x0600275F RID: 10079 RVA: 0x0008FC08 File Offset: 0x0008DE08
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) != (StreamingContextStates)0)
			{
				this.m_serializedPermission = this.ToXml().ToString();
				if (this.m_sites != null && this.m_sites.Length == 1)
				{
					this.m_site = this.m_sites[0];
				}
			}
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x0008FC56 File Offset: 0x0008DE56
		[OnSerialized]
		private void OnSerialized(StreamingContext ctx)
		{
			if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) != (StreamingContextStates)0)
			{
				this.m_serializedPermission = null;
				this.m_site = null;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.SiteIdentityPermission" /> class with the specified <see cref="T:System.Security.Permissions.PermissionState" />.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />.</exception>
		// Token: 0x06002761 RID: 10081 RVA: 0x0008FC75 File Offset: 0x0008DE75
		public SiteIdentityPermission(PermissionState state)
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

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.SiteIdentityPermission" /> class to represent the specified site identity.</summary>
		/// <param name="site">The site name or wildcard expression.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="site" /> parameter is not a valid string, or does not match a valid wildcard site name.</exception>
		// Token: 0x06002762 RID: 10082 RVA: 0x0008FCA3 File Offset: 0x0008DEA3
		public SiteIdentityPermission(string site)
		{
			this.Site = site;
		}

		/// <summary>Gets or sets the current site.</summary>
		/// <returns>The current site.</returns>
		/// <exception cref="T:System.NotSupportedException">The site identity cannot be retrieved because it has an ambiguous identity.</exception>
		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06002764 RID: 10084 RVA: 0x0008FCD5 File Offset: 0x0008DED5
		// (set) Token: 0x06002763 RID: 10083 RVA: 0x0008FCB2 File Offset: 0x0008DEB2
		public string Site
		{
			get
			{
				if (this.m_sites == null)
				{
					return "";
				}
				if (this.m_sites.Length == 1)
				{
					return this.m_sites[0].ToString();
				}
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_AmbiguousIdentity"));
			}
			set
			{
				this.m_unrestricted = false;
				this.m_sites = new SiteString[1];
				this.m_sites[0] = new SiteString(value);
			}
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06002765 RID: 10085 RVA: 0x0008FD10 File Offset: 0x0008DF10
		public override IPermission Copy()
		{
			SiteIdentityPermission siteIdentityPermission = new SiteIdentityPermission(PermissionState.None);
			siteIdentityPermission.m_unrestricted = this.m_unrestricted;
			if (this.m_sites != null)
			{
				siteIdentityPermission.m_sites = new SiteString[this.m_sites.Length];
				for (int i = 0; i < this.m_sites.Length; i++)
				{
					siteIdentityPermission.m_sites[i] = this.m_sites[i].Copy();
				}
			}
			return siteIdentityPermission;
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission.</param>
		/// <returns>
		///   <see langword="true" /> if the current permission is a subset of the specified permission; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002766 RID: 10086 RVA: 0x0008FD74 File Offset: 0x0008DF74
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return !this.m_unrestricted && (this.m_sites == null || this.m_sites.Length == 0);
			}
			SiteIdentityPermission siteIdentityPermission = target as SiteIdentityPermission;
			if (siteIdentityPermission == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[] { base.GetType().FullName }));
			}
			if (siteIdentityPermission.m_unrestricted)
			{
				return true;
			}
			if (this.m_unrestricted)
			{
				return false;
			}
			if (this.m_sites != null)
			{
				foreach (SiteString siteString in this.m_sites)
				{
					bool flag = false;
					if (siteIdentityPermission.m_sites != null)
					{
						foreach (SiteString siteString2 in siteIdentityPermission.m_sites)
						{
							if (siteString.IsSubsetOf(siteString2))
							{
								flag = true;
								break;
							}
						}
					}
					if (!flag)
					{
						return false;
					}
				}
			}
			return true;
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is <see langword="null" /> if the intersection is empty.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002767 RID: 10087 RVA: 0x0008FE4C File Offset: 0x0008E04C
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			SiteIdentityPermission siteIdentityPermission = target as SiteIdentityPermission;
			if (siteIdentityPermission == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[] { base.GetType().FullName }));
			}
			if (this.m_unrestricted && siteIdentityPermission.m_unrestricted)
			{
				return new SiteIdentityPermission(PermissionState.None)
				{
					m_unrestricted = true
				};
			}
			if (this.m_unrestricted)
			{
				return siteIdentityPermission.Copy();
			}
			if (siteIdentityPermission.m_unrestricted)
			{
				return this.Copy();
			}
			if (this.m_sites == null || siteIdentityPermission.m_sites == null || this.m_sites.Length == 0 || siteIdentityPermission.m_sites.Length == 0)
			{
				return null;
			}
			List<SiteString> list = new List<SiteString>();
			foreach (SiteString siteString in this.m_sites)
			{
				foreach (SiteString siteString2 in siteIdentityPermission.m_sites)
				{
					SiteString siteString3 = siteString.Intersect(siteString2);
					if (siteString3 != null)
					{
						list.Add(siteString3);
					}
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			return new SiteIdentityPermission(PermissionState.None)
			{
				m_sites = list.ToArray()
			};
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.  
		///  -or-  
		///  The permissions are not equal and one is not a subset of the other.</exception>
		// Token: 0x06002768 RID: 10088 RVA: 0x0008FF70 File Offset: 0x0008E170
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				if ((this.m_sites == null || this.m_sites.Length == 0) && !this.m_unrestricted)
				{
					return null;
				}
				return this.Copy();
			}
			else
			{
				SiteIdentityPermission siteIdentityPermission = target as SiteIdentityPermission;
				if (siteIdentityPermission == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[] { base.GetType().FullName }));
				}
				if (this.m_unrestricted || siteIdentityPermission.m_unrestricted)
				{
					return new SiteIdentityPermission(PermissionState.None)
					{
						m_unrestricted = true
					};
				}
				if (this.m_sites == null || this.m_sites.Length == 0)
				{
					if (siteIdentityPermission.m_sites == null || siteIdentityPermission.m_sites.Length == 0)
					{
						return null;
					}
					return siteIdentityPermission.Copy();
				}
				else
				{
					if (siteIdentityPermission.m_sites == null || siteIdentityPermission.m_sites.Length == 0)
					{
						return this.Copy();
					}
					List<SiteString> list = new List<SiteString>();
					foreach (SiteString siteString in this.m_sites)
					{
						list.Add(siteString);
					}
					foreach (SiteString siteString2 in siteIdentityPermission.m_sites)
					{
						bool flag = false;
						foreach (SiteString siteString3 in list)
						{
							if (siteString2.Equals(siteString3))
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							list.Add(siteString2);
						}
					}
					return new SiteIdentityPermission(PermissionState.None)
					{
						m_sites = list.ToArray()
					};
				}
			}
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="esd">The XML encoding to use to reconstruct the permission.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="esd" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="esd" /> parameter is not a valid permission element.  
		///  -or-  
		///  The <paramref name="esd" /> parameter's version number is not valid.</exception>
		// Token: 0x06002769 RID: 10089 RVA: 0x000900F4 File Offset: 0x0008E2F4
		public override void FromXml(SecurityElement esd)
		{
			this.m_unrestricted = false;
			this.m_sites = null;
			CodeAccessPermission.ValidateElement(esd, this);
			string text = esd.Attribute("Unrestricted");
			if (text != null && string.Compare(text, "true", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.m_unrestricted = true;
				return;
			}
			string text2 = esd.Attribute("Site");
			List<SiteString> list = new List<SiteString>();
			if (text2 != null)
			{
				list.Add(new SiteString(text2));
			}
			ArrayList children = esd.Children;
			if (children != null)
			{
				foreach (object obj in children)
				{
					SecurityElement securityElement = (SecurityElement)obj;
					text2 = securityElement.Attribute("Site");
					if (text2 != null)
					{
						list.Add(new SiteString(text2));
					}
				}
			}
			if (list.Count != 0)
			{
				this.m_sites = list.ToArray();
			}
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x0600276A RID: 10090 RVA: 0x000901E0 File Offset: 0x0008E3E0
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = CodeAccessPermission.CreatePermissionElement(this, "System.Security.Permissions.SiteIdentityPermission");
			if (this.m_unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else if (this.m_sites != null)
			{
				if (this.m_sites.Length == 1)
				{
					securityElement.AddAttribute("Site", this.m_sites[0].ToString());
				}
				else
				{
					for (int i = 0; i < this.m_sites.Length; i++)
					{
						SecurityElement securityElement2 = new SecurityElement("Site");
						securityElement2.AddAttribute("Site", this.m_sites[i].ToString());
						securityElement.AddChild(securityElement2);
					}
				}
			}
			return securityElement;
		}

		// Token: 0x0600276B RID: 10091 RVA: 0x0009027E File Offset: 0x0008E47E
		int IBuiltInPermission.GetTokenIndex()
		{
			return SiteIdentityPermission.GetTokenIndex();
		}

		// Token: 0x0600276C RID: 10092 RVA: 0x00090285 File Offset: 0x0008E485
		internal static int GetTokenIndex()
		{
			return 11;
		}

		// Token: 0x04000F48 RID: 3912
		[OptionalField(VersionAdded = 2)]
		private bool m_unrestricted;

		// Token: 0x04000F49 RID: 3913
		[OptionalField(VersionAdded = 2)]
		private SiteString[] m_sites;

		// Token: 0x04000F4A RID: 3914
		[OptionalField(VersionAdded = 2)]
		private string m_serializedPermission;

		// Token: 0x04000F4B RID: 3915
		private SiteString m_site;
	}
}
