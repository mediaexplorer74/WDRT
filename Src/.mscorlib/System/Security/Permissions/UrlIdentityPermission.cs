using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Util;

namespace System.Security.Permissions
{
	/// <summary>Defines the identity permission for the URL from which the code originates. This class cannot be inherited.</summary>
	// Token: 0x0200030D RID: 781
	[ComVisible(true)]
	[Serializable]
	public sealed class UrlIdentityPermission : CodeAccessPermission, IBuiltInPermission
	{
		// Token: 0x060027A0 RID: 10144 RVA: 0x00091230 File Offset: 0x0008F430
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			if (this.m_serializedPermission != null)
			{
				this.FromXml(SecurityElement.FromString(this.m_serializedPermission));
				this.m_serializedPermission = null;
				return;
			}
			if (this.m_url != null)
			{
				this.m_unrestricted = false;
				this.m_urls = new URLString[1];
				this.m_urls[0] = this.m_url;
				this.m_url = null;
			}
		}

		// Token: 0x060027A1 RID: 10145 RVA: 0x00091290 File Offset: 0x0008F490
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) != (StreamingContextStates)0)
			{
				this.m_serializedPermission = this.ToXml().ToString();
				if (this.m_urls != null && this.m_urls.Length == 1)
				{
					this.m_url = this.m_urls[0];
				}
			}
		}

		// Token: 0x060027A2 RID: 10146 RVA: 0x000912DE File Offset: 0x0008F4DE
		[OnSerialized]
		private void OnSerialized(StreamingContext ctx)
		{
			if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) != (StreamingContextStates)0)
			{
				this.m_serializedPermission = null;
				this.m_url = null;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.UrlIdentityPermission" /> class with the specified <see cref="T:System.Security.Permissions.PermissionState" />.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />.</exception>
		// Token: 0x060027A3 RID: 10147 RVA: 0x000912FD File Offset: 0x0008F4FD
		public UrlIdentityPermission(PermissionState state)
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

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.UrlIdentityPermission" /> class to represent the URL identity described by <paramref name="site" />.</summary>
		/// <param name="site">A URL or wildcard expression.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="site" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">The length of the <paramref name="site" /> parameter is zero.</exception>
		/// <exception cref="T:System.ArgumentException">The URL, directory, or site portion of the <paramref name="site" /> parameter is not valid.</exception>
		// Token: 0x060027A4 RID: 10148 RVA: 0x0009132B File Offset: 0x0008F52B
		public UrlIdentityPermission(string site)
		{
			if (site == null)
			{
				throw new ArgumentNullException("site");
			}
			this.Url = site;
		}

		// Token: 0x060027A5 RID: 10149 RVA: 0x00091348 File Offset: 0x0008F548
		internal UrlIdentityPermission(URLString site)
		{
			this.m_unrestricted = false;
			this.m_urls = new URLString[1];
			this.m_urls[0] = site;
		}

		// Token: 0x060027A6 RID: 10150 RVA: 0x0009136C File Offset: 0x0008F56C
		internal void AppendOrigin(ArrayList originList)
		{
			if (this.m_urls == null)
			{
				originList.Add("");
				return;
			}
			for (int i = 0; i < this.m_urls.Length; i++)
			{
				originList.Add(this.m_urls[i].ToString());
			}
		}

		/// <summary>Gets or sets a URL representing the identity of Internet code.</summary>
		/// <returns>A URL representing the identity of Internet code.</returns>
		/// <exception cref="T:System.NotSupportedException">The URL cannot be retrieved because it has an ambiguous identity.</exception>
		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x060027A8 RID: 10152 RVA: 0x000913EB File Offset: 0x0008F5EB
		// (set) Token: 0x060027A7 RID: 10151 RVA: 0x000913B5 File Offset: 0x0008F5B5
		public string Url
		{
			get
			{
				if (this.m_urls == null)
				{
					return "";
				}
				if (this.m_urls.Length == 1)
				{
					return this.m_urls[0].ToString();
				}
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_AmbiguousIdentity"));
			}
			set
			{
				this.m_unrestricted = false;
				if (value == null || value.Length == 0)
				{
					this.m_urls = null;
					return;
				}
				this.m_urls = new URLString[1];
				this.m_urls[0] = new URLString(value);
			}
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x060027A9 RID: 10153 RVA: 0x00091424 File Offset: 0x0008F624
		public override IPermission Copy()
		{
			UrlIdentityPermission urlIdentityPermission = new UrlIdentityPermission(PermissionState.None);
			urlIdentityPermission.m_unrestricted = this.m_unrestricted;
			if (this.m_urls != null)
			{
				urlIdentityPermission.m_urls = new URLString[this.m_urls.Length];
				for (int i = 0; i < this.m_urls.Length; i++)
				{
					urlIdentityPermission.m_urls[i] = (URLString)this.m_urls[i].Copy();
				}
			}
			return urlIdentityPermission;
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission.</param>
		/// <returns>
		///   <see langword="true" /> if the current permission is a subset of the specified permission; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.  
		///  -or-  
		///  The Url property is not a valid URL.</exception>
		// Token: 0x060027AA RID: 10154 RVA: 0x00091490 File Offset: 0x0008F690
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return !this.m_unrestricted && (this.m_urls == null || this.m_urls.Length == 0);
			}
			UrlIdentityPermission urlIdentityPermission = target as UrlIdentityPermission;
			if (urlIdentityPermission == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[] { base.GetType().FullName }));
			}
			if (urlIdentityPermission.m_unrestricted)
			{
				return true;
			}
			if (this.m_unrestricted)
			{
				return false;
			}
			if (this.m_urls != null)
			{
				foreach (URLString urlstring in this.m_urls)
				{
					bool flag = false;
					if (urlIdentityPermission.m_urls != null)
					{
						foreach (URLString urlstring2 in urlIdentityPermission.m_urls)
						{
							if (urlstring.IsSubsetOf(urlstring2))
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
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.  
		///  -or-  
		///  The Url property is not a valid URL.</exception>
		// Token: 0x060027AB RID: 10155 RVA: 0x00091568 File Offset: 0x0008F768
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			UrlIdentityPermission urlIdentityPermission = target as UrlIdentityPermission;
			if (urlIdentityPermission == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[] { base.GetType().FullName }));
			}
			if (this.m_unrestricted && urlIdentityPermission.m_unrestricted)
			{
				return new UrlIdentityPermission(PermissionState.None)
				{
					m_unrestricted = true
				};
			}
			if (this.m_unrestricted)
			{
				return urlIdentityPermission.Copy();
			}
			if (urlIdentityPermission.m_unrestricted)
			{
				return this.Copy();
			}
			if (this.m_urls == null || urlIdentityPermission.m_urls == null || this.m_urls.Length == 0 || urlIdentityPermission.m_urls.Length == 0)
			{
				return null;
			}
			List<URLString> list = new List<URLString>();
			foreach (URLString urlstring in this.m_urls)
			{
				foreach (URLString urlstring2 in urlIdentityPermission.m_urls)
				{
					URLString urlstring3 = (URLString)urlstring.Intersect(urlstring2);
					if (urlstring3 != null)
					{
						list.Add(urlstring3);
					}
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			return new UrlIdentityPermission(PermissionState.None)
			{
				m_urls = list.ToArray()
			};
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.  
		///  -or-  
		///  The <see cref="P:System.Security.Permissions.UrlIdentityPermission.Url" /> property is not a valid URL.  
		///  -or-  
		///  The two permissions are not equal and one is not a subset of the other.</exception>
		/// <exception cref="T:System.NotSupportedException">The operation is ambiguous because the permission represents multiple identities.</exception>
		// Token: 0x060027AC RID: 10156 RVA: 0x00091690 File Offset: 0x0008F890
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				if ((this.m_urls == null || this.m_urls.Length == 0) && !this.m_unrestricted)
				{
					return null;
				}
				return this.Copy();
			}
			else
			{
				UrlIdentityPermission urlIdentityPermission = target as UrlIdentityPermission;
				if (urlIdentityPermission == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[] { base.GetType().FullName }));
				}
				if (this.m_unrestricted || urlIdentityPermission.m_unrestricted)
				{
					return new UrlIdentityPermission(PermissionState.None)
					{
						m_unrestricted = true
					};
				}
				if (this.m_urls == null || this.m_urls.Length == 0)
				{
					if (urlIdentityPermission.m_urls == null || urlIdentityPermission.m_urls.Length == 0)
					{
						return null;
					}
					return urlIdentityPermission.Copy();
				}
				else
				{
					if (urlIdentityPermission.m_urls == null || urlIdentityPermission.m_urls.Length == 0)
					{
						return this.Copy();
					}
					List<URLString> list = new List<URLString>();
					foreach (URLString urlstring in this.m_urls)
					{
						list.Add(urlstring);
					}
					foreach (URLString urlstring2 in urlIdentityPermission.m_urls)
					{
						bool flag = false;
						foreach (URLString urlstring3 in list)
						{
							if (urlstring2.Equals(urlstring3))
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							list.Add(urlstring2);
						}
					}
					return new UrlIdentityPermission(PermissionState.None)
					{
						m_urls = list.ToArray()
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
		// Token: 0x060027AD RID: 10157 RVA: 0x00091814 File Offset: 0x0008FA14
		public override void FromXml(SecurityElement esd)
		{
			this.m_unrestricted = false;
			this.m_urls = null;
			CodeAccessPermission.ValidateElement(esd, this);
			string text = esd.Attribute("Unrestricted");
			if (text != null && string.Compare(text, "true", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.m_unrestricted = true;
				return;
			}
			string text2 = esd.Attribute("Url");
			List<URLString> list = new List<URLString>();
			if (text2 != null)
			{
				list.Add(new URLString(text2, true));
			}
			ArrayList children = esd.Children;
			if (children != null)
			{
				foreach (object obj in children)
				{
					SecurityElement securityElement = (SecurityElement)obj;
					text2 = securityElement.Attribute("Url");
					if (text2 != null)
					{
						list.Add(new URLString(text2, true));
					}
				}
			}
			if (list.Count != 0)
			{
				this.m_urls = list.ToArray();
			}
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x060027AE RID: 10158 RVA: 0x00091900 File Offset: 0x0008FB00
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = CodeAccessPermission.CreatePermissionElement(this, "System.Security.Permissions.UrlIdentityPermission");
			if (this.m_unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else if (this.m_urls != null)
			{
				if (this.m_urls.Length == 1)
				{
					securityElement.AddAttribute("Url", this.m_urls[0].ToString());
				}
				else
				{
					for (int i = 0; i < this.m_urls.Length; i++)
					{
						SecurityElement securityElement2 = new SecurityElement("Url");
						securityElement2.AddAttribute("Url", this.m_urls[i].ToString());
						securityElement.AddChild(securityElement2);
					}
				}
			}
			return securityElement;
		}

		// Token: 0x060027AF RID: 10159 RVA: 0x0009199E File Offset: 0x0008FB9E
		int IBuiltInPermission.GetTokenIndex()
		{
			return UrlIdentityPermission.GetTokenIndex();
		}

		// Token: 0x060027B0 RID: 10160 RVA: 0x000919A5 File Offset: 0x0008FBA5
		internal static int GetTokenIndex()
		{
			return 13;
		}

		// Token: 0x04000F5D RID: 3933
		[OptionalField(VersionAdded = 2)]
		private bool m_unrestricted;

		// Token: 0x04000F5E RID: 3934
		[OptionalField(VersionAdded = 2)]
		private URLString[] m_urls;

		// Token: 0x04000F5F RID: 3935
		[OptionalField(VersionAdded = 2)]
		private string m_serializedPermission;

		// Token: 0x04000F60 RID: 3936
		private URLString m_url;
	}
}
