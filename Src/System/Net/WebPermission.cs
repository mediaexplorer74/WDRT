using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Text.RegularExpressions;

namespace System.Net
{
	/// <summary>Controls rights to access HTTP Internet resources.</summary>
	// Token: 0x02000187 RID: 391
	[Serializable]
	public sealed class WebPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000E79 RID: 3705 RVA: 0x0004BA9C File Offset: 0x00049C9C
		internal static Regex MatchAllRegex
		{
			get
			{
				if (WebPermission.s_MatchAllRegex == null)
				{
					WebPermission.s_MatchAllRegex = new Regex(".*");
				}
				return WebPermission.s_MatchAllRegex;
			}
		}

		/// <summary>This property returns an enumeration of a single connect permissions held by this <see cref="T:System.Net.WebPermission" />. The possible objects types contained in the returned enumeration are <see cref="T:System.String" /> and <see cref="T:System.Text.RegularExpressions.Regex" />.</summary>
		/// <returns>The <see cref="T:System.Collections.IEnumerator" /> interface that contains connect permissions.</returns>
		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000E7A RID: 3706 RVA: 0x0004BAC0 File Offset: 0x00049CC0
		public IEnumerator ConnectList
		{
			get
			{
				if (this.m_UnrestrictedConnect)
				{
					return new Regex[] { WebPermission.MatchAllRegex }.GetEnumerator();
				}
				ArrayList arrayList = new ArrayList(this.m_connectList.Count);
				for (int i = 0; i < this.m_connectList.Count; i++)
				{
					arrayList.Add((this.m_connectList[i] is DelayedRegex) ? ((DelayedRegex)this.m_connectList[i]).AsRegex : ((this.m_connectList[i] is Uri) ? ((Uri)this.m_connectList[i]).GetComponents(UriComponents.HttpRequestUrl, UriFormat.UriEscaped) : this.m_connectList[i]));
				}
				return arrayList.GetEnumerator();
			}
		}

		/// <summary>This property returns an enumeration of a single accept permissions held by this <see cref="T:System.Net.WebPermission" />. The possible objects types contained in the returned enumeration are <see cref="T:System.String" /> and <see cref="T:System.Text.RegularExpressions.Regex" />.</summary>
		/// <returns>The <see cref="T:System.Collections.IEnumerator" /> interface that contains accept permissions.</returns>
		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000E7B RID: 3707 RVA: 0x0004BB84 File Offset: 0x00049D84
		public IEnumerator AcceptList
		{
			get
			{
				if (this.m_UnrestrictedAccept)
				{
					return new Regex[] { WebPermission.MatchAllRegex }.GetEnumerator();
				}
				ArrayList arrayList = new ArrayList(this.m_acceptList.Count);
				for (int i = 0; i < this.m_acceptList.Count; i++)
				{
					arrayList.Add((this.m_acceptList[i] is DelayedRegex) ? ((DelayedRegex)this.m_acceptList[i]).AsRegex : ((this.m_acceptList[i] is Uri) ? ((Uri)this.m_acceptList[i]).GetComponents(UriComponents.HttpRequestUrl, UriFormat.UriEscaped) : this.m_acceptList[i]));
				}
				return arrayList.GetEnumerator();
			}
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.WebPermission" /> class that passes all demands or fails all demands.</summary>
		/// <param name="state">A <see cref="T:System.Security.Permissions.PermissionState" /> value.</param>
		// Token: 0x06000E7C RID: 3708 RVA: 0x0004BC46 File Offset: 0x00049E46
		public WebPermission(PermissionState state)
		{
			this.m_noRestriction = state == PermissionState.Unrestricted;
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x0004BC6E File Offset: 0x00049E6E
		internal WebPermission(bool unrestricted)
		{
			this.m_noRestriction = unrestricted;
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.WebPermission" /> class.</summary>
		// Token: 0x06000E7E RID: 3710 RVA: 0x0004BC93 File Offset: 0x00049E93
		public WebPermission()
		{
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x0004BCB1 File Offset: 0x00049EB1
		internal WebPermission(NetworkAccess access)
		{
			this.m_UnrestrictedConnect = (access & NetworkAccess.Connect) > (NetworkAccess)0;
			this.m_UnrestrictedAccept = (access & NetworkAccess.Accept) > (NetworkAccess)0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebPermission" /> class with the specified access rights for the specified URI regular expression.</summary>
		/// <param name="access">A <see cref="T:System.Net.NetworkAccess" /> value that indicates what kind of access to grant to the specified URI. <see cref="F:System.Net.NetworkAccess.Accept" /> indicates that the application is allowed to accept connections from the Internet on a local resource. <see cref="F:System.Net.NetworkAccess.Connect" /> indicates that the application is allowed to connect to specific Internet resources.</param>
		/// <param name="uriRegex">A regular expression that describes the URI to which access is to be granted.</param>
		// Token: 0x06000E80 RID: 3712 RVA: 0x0004BCEC File Offset: 0x00049EEC
		public WebPermission(NetworkAccess access, Regex uriRegex)
		{
			this.AddPermission(access, uriRegex);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebPermission" /> class with the specified access rights for the specified URI.</summary>
		/// <param name="access">A NetworkAccess value that indicates what kind of access to grant to the specified URI. <see cref="F:System.Net.NetworkAccess.Accept" /> indicates that the application is allowed to accept connections from the Internet on a local resource. <see cref="F:System.Net.NetworkAccess.Connect" /> indicates that the application is allowed to connect to specific Internet resources.</param>
		/// <param name="uriString">A URI string to which access rights are granted.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uriString" /> is <see langword="null" />.</exception>
		// Token: 0x06000E81 RID: 3713 RVA: 0x0004BD12 File Offset: 0x00049F12
		public WebPermission(NetworkAccess access, string uriString)
		{
			this.AddPermission(access, uriString);
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x0004BD38 File Offset: 0x00049F38
		internal WebPermission(NetworkAccess access, Uri uri)
		{
			this.AddPermission(access, uri);
		}

		/// <summary>Adds the specified URI string with the specified access rights to the current <see cref="T:System.Net.WebPermission" />.</summary>
		/// <param name="access">A <see cref="T:System.Net.NetworkAccess" /> that specifies the access rights that are granted to the URI.</param>
		/// <param name="uriString">A string that describes the URI to which access rights are granted.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uriString" /> is <see langword="null" />.</exception>
		// Token: 0x06000E83 RID: 3715 RVA: 0x0004BD60 File Offset: 0x00049F60
		public void AddPermission(NetworkAccess access, string uriString)
		{
			if (uriString == null)
			{
				throw new ArgumentNullException("uriString");
			}
			if (this.m_noRestriction)
			{
				return;
			}
			Uri uri;
			if (Uri.TryCreate(uriString, UriKind.Absolute, out uri))
			{
				this.AddPermission(access, uri);
				return;
			}
			ArrayList arrayList = new ArrayList();
			if ((access & NetworkAccess.Connect) != (NetworkAccess)0 && !this.m_UnrestrictedConnect)
			{
				arrayList.Add(this.m_connectList);
			}
			if ((access & NetworkAccess.Accept) != (NetworkAccess)0 && !this.m_UnrestrictedAccept)
			{
				arrayList.Add(this.m_acceptList);
			}
			foreach (object obj in arrayList)
			{
				ArrayList arrayList2 = (ArrayList)obj;
				bool flag = false;
				foreach (object obj2 in arrayList2)
				{
					string text = obj2 as string;
					if (text != null && string.Compare(text, uriString, StringComparison.OrdinalIgnoreCase) == 0)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					arrayList2.Add(uriString);
				}
			}
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x0004BE88 File Offset: 0x0004A088
		internal void AddPermission(NetworkAccess access, Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (this.m_noRestriction)
			{
				return;
			}
			ArrayList arrayList = new ArrayList();
			if ((access & NetworkAccess.Connect) != (NetworkAccess)0 && !this.m_UnrestrictedConnect)
			{
				arrayList.Add(this.m_connectList);
			}
			if ((access & NetworkAccess.Accept) != (NetworkAccess)0 && !this.m_UnrestrictedAccept)
			{
				arrayList.Add(this.m_acceptList);
			}
			foreach (object obj in arrayList)
			{
				ArrayList arrayList2 = (ArrayList)obj;
				bool flag = false;
				foreach (object obj2 in arrayList2)
				{
					if (obj2 is Uri && uri.Equals(obj2))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					arrayList2.Add(uri);
				}
			}
		}

		/// <summary>Adds the specified URI with the specified access rights to the current <see cref="T:System.Net.WebPermission" />.</summary>
		/// <param name="access">A NetworkAccess that specifies the access rights that are granted to the URI.</param>
		/// <param name="uriRegex">A regular expression that describes the set of URIs to which access rights are granted.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="uriRegex" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06000E85 RID: 3717 RVA: 0x0004BF98 File Offset: 0x0004A198
		public void AddPermission(NetworkAccess access, Regex uriRegex)
		{
			if (uriRegex == null)
			{
				throw new ArgumentNullException("uriRegex");
			}
			if (this.m_noRestriction)
			{
				return;
			}
			if (uriRegex.ToString() == ".*")
			{
				if (!this.m_UnrestrictedConnect && (access & NetworkAccess.Connect) != (NetworkAccess)0)
				{
					this.m_UnrestrictedConnect = true;
					this.m_connectList.Clear();
				}
				if (!this.m_UnrestrictedAccept && (access & NetworkAccess.Accept) != (NetworkAccess)0)
				{
					this.m_UnrestrictedAccept = true;
					this.m_acceptList.Clear();
				}
				return;
			}
			this.AddAsPattern(access, new DelayedRegex(uriRegex));
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x0004C020 File Offset: 0x0004A220
		internal void AddAsPattern(NetworkAccess access, DelayedRegex uriRegexPattern)
		{
			ArrayList arrayList = new ArrayList();
			if ((access & NetworkAccess.Connect) != (NetworkAccess)0 && !this.m_UnrestrictedConnect)
			{
				arrayList.Add(this.m_connectList);
			}
			if ((access & NetworkAccess.Accept) != (NetworkAccess)0 && !this.m_UnrestrictedAccept)
			{
				arrayList.Add(this.m_acceptList);
			}
			foreach (object obj in arrayList)
			{
				ArrayList arrayList2 = (ArrayList)obj;
				bool flag = false;
				foreach (object obj2 in arrayList2)
				{
					if (obj2 is DelayedRegex && string.Compare(uriRegexPattern.ToString(), obj2.ToString(), StringComparison.OrdinalIgnoreCase) == 0)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					arrayList2.Add(uriRegexPattern);
				}
			}
		}

		/// <summary>Checks the overall permission state of the <see cref="T:System.Net.WebPermission" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Net.WebPermission" /> was created with the <see cref="F:System.Security.Permissions.PermissionState.Unrestricted" /><see cref="T:System.Security.Permissions.PermissionState" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000E87 RID: 3719 RVA: 0x0004C120 File Offset: 0x0004A320
		public bool IsUnrestricted()
		{
			return this.m_noRestriction;
		}

		/// <summary>Creates a copy of a <see cref="T:System.Net.WebPermission" />.</summary>
		/// <returns>A new instance of the <see cref="T:System.Net.WebPermission" /> class that has the same values as the original.</returns>
		// Token: 0x06000E88 RID: 3720 RVA: 0x0004C128 File Offset: 0x0004A328
		public override IPermission Copy()
		{
			if (this.m_noRestriction)
			{
				return new WebPermission(true);
			}
			return new WebPermission((this.m_UnrestrictedConnect ? NetworkAccess.Connect : ((NetworkAccess)0)) | (this.m_UnrestrictedAccept ? NetworkAccess.Accept : ((NetworkAccess)0)))
			{
				m_acceptList = (ArrayList)this.m_acceptList.Clone(),
				m_connectList = (ArrayList)this.m_connectList.Clone()
			};
		}

		/// <summary>Determines whether the current <see cref="T:System.Net.WebPermission" /> is a subset of the specified object.</summary>
		/// <param name="target">The <see cref="T:System.Net.WebPermission" /> to compare to the current <see cref="T:System.Net.WebPermission" />.</param>
		/// <returns>
		///   <see langword="true" /> if the current instance is a subset of the <paramref name="target" /> parameter; otherwise, <see langword="false" />. If the target is <see langword="null" />, the method returns <see langword="true" /> for an empty current permission that is not unrestricted and <see langword="false" /> otherwise.</returns>
		/// <exception cref="T:System.ArgumentException">The target parameter is not an instance of <see cref="T:System.Net.WebPermission" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The current instance contains a Regex-encoded right and there is not exactly the same right found in the target instance.</exception>
		// Token: 0x06000E89 RID: 3721 RVA: 0x0004C198 File Offset: 0x0004A398
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return !this.m_noRestriction && !this.m_UnrestrictedConnect && !this.m_UnrestrictedAccept && this.m_connectList.Count == 0 && this.m_acceptList.Count == 0;
			}
			WebPermission webPermission = target as WebPermission;
			if (webPermission == null)
			{
				throw new ArgumentException(SR.GetString("net_perm_target"), "target");
			}
			if (webPermission.m_noRestriction)
			{
				return true;
			}
			if (this.m_noRestriction)
			{
				return false;
			}
			if (!webPermission.m_UnrestrictedAccept)
			{
				if (this.m_UnrestrictedAccept)
				{
					return false;
				}
				if (this.m_acceptList.Count != 0)
				{
					if (webPermission.m_acceptList.Count == 0)
					{
						return false;
					}
					foreach (object obj in this.m_acceptList)
					{
						DelayedRegex delayedRegex = obj as DelayedRegex;
						if (delayedRegex != null)
						{
							if (!WebPermission.isSpecialSubsetCase(obj.ToString(), webPermission.m_acceptList))
							{
								throw new NotSupportedException(SR.GetString("net_perm_both_regex"));
							}
						}
						else if (!WebPermission.isMatchedURI(obj, webPermission.m_acceptList))
						{
							return false;
						}
					}
				}
			}
			if (!webPermission.m_UnrestrictedConnect)
			{
				if (this.m_UnrestrictedConnect)
				{
					return false;
				}
				if (this.m_connectList.Count != 0)
				{
					if (webPermission.m_connectList.Count == 0)
					{
						return false;
					}
					foreach (object obj2 in this.m_connectList)
					{
						DelayedRegex delayedRegex = obj2 as DelayedRegex;
						if (delayedRegex != null)
						{
							if (!WebPermission.isSpecialSubsetCase(obj2.ToString(), webPermission.m_connectList))
							{
								throw new NotSupportedException(SR.GetString("net_perm_both_regex"));
							}
						}
						else if (!WebPermission.isMatchedURI(obj2, webPermission.m_connectList))
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x0004C38C File Offset: 0x0004A58C
		private static bool isSpecialSubsetCase(string regexToCheck, ArrayList permList)
		{
			foreach (object obj in permList)
			{
				DelayedRegex delayedRegex = obj as DelayedRegex;
				Uri uri;
				if (delayedRegex != null)
				{
					if (string.Compare(regexToCheck, delayedRegex.ToString(), StringComparison.OrdinalIgnoreCase) == 0)
					{
						return true;
					}
				}
				else if ((uri = obj as Uri) != null)
				{
					if (string.Compare(regexToCheck, Regex.Escape(uri.GetComponents(UriComponents.HttpRequestUrl, UriFormat.UriEscaped)), StringComparison.OrdinalIgnoreCase) == 0)
					{
						return true;
					}
				}
				else if (string.Compare(regexToCheck, Regex.Escape(obj.ToString()), StringComparison.OrdinalIgnoreCase) == 0)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Returns the logical union between two instances of the <see cref="T:System.Net.WebPermission" /> class.</summary>
		/// <param name="target">The <see cref="T:System.Net.WebPermission" /> to combine with the current <see cref="T:System.Net.WebPermission" />.</param>
		/// <returns>A <see cref="T:System.Net.WebPermission" /> that represents the union of the current instance and the <paramref name="target" /> parameter. If either <see langword="WebPermission" /> is <see cref="F:System.Security.Permissions.PermissionState.Unrestricted" />, the method returns a <see cref="T:System.Net.WebPermission" /> that is <see cref="F:System.Security.Permissions.PermissionState.Unrestricted" />. If the target is <see langword="null" />, the method returns a copy of the current <see cref="T:System.Net.WebPermission" />.</returns>
		/// <exception cref="T:System.ArgumentException">target is not <see langword="null" /> or of type <see cref="T:System.Net.WebPermission" />.</exception>
		// Token: 0x06000E8B RID: 3723 RVA: 0x0004C440 File Offset: 0x0004A640
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			WebPermission webPermission = target as WebPermission;
			if (webPermission == null)
			{
				throw new ArgumentException(SR.GetString("net_perm_target"), "target");
			}
			if (this.m_noRestriction || webPermission.m_noRestriction)
			{
				return new WebPermission(true);
			}
			WebPermission webPermission2 = new WebPermission();
			if (this.m_UnrestrictedConnect || webPermission.m_UnrestrictedConnect)
			{
				webPermission2.m_UnrestrictedConnect = true;
			}
			else
			{
				webPermission2.m_connectList = (ArrayList)webPermission.m_connectList.Clone();
				for (int i = 0; i < this.m_connectList.Count; i++)
				{
					DelayedRegex delayedRegex = this.m_connectList[i] as DelayedRegex;
					if (delayedRegex == null)
					{
						if (this.m_connectList[i] is string)
						{
							webPermission2.AddPermission(NetworkAccess.Connect, (string)this.m_connectList[i]);
						}
						else
						{
							webPermission2.AddPermission(NetworkAccess.Connect, (Uri)this.m_connectList[i]);
						}
					}
					else
					{
						webPermission2.AddAsPattern(NetworkAccess.Connect, delayedRegex);
					}
				}
			}
			if (this.m_UnrestrictedAccept || webPermission.m_UnrestrictedAccept)
			{
				webPermission2.m_UnrestrictedAccept = true;
			}
			else
			{
				webPermission2.m_acceptList = (ArrayList)webPermission.m_acceptList.Clone();
				for (int j = 0; j < this.m_acceptList.Count; j++)
				{
					DelayedRegex delayedRegex2 = this.m_acceptList[j] as DelayedRegex;
					if (delayedRegex2 == null)
					{
						if (this.m_acceptList[j] is string)
						{
							webPermission2.AddPermission(NetworkAccess.Accept, (string)this.m_acceptList[j]);
						}
						else
						{
							webPermission2.AddPermission(NetworkAccess.Accept, (Uri)this.m_acceptList[j]);
						}
					}
					else
					{
						webPermission2.AddAsPattern(NetworkAccess.Accept, delayedRegex2);
					}
				}
			}
			return webPermission2;
		}

		/// <summary>Returns the logical intersection of two <see cref="T:System.Net.WebPermission" /> instances.</summary>
		/// <param name="target">The <see cref="T:System.Net.WebPermission" /> to compare with the current instance.</param>
		/// <returns>A new <see cref="T:System.Net.WebPermission" /> that represents the intersection of the current instance and the <paramref name="target" /> parameter. If the intersection is empty, the method returns <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not <see langword="null" /> or of type <see cref="T:System.Net.WebPermission" /></exception>
		// Token: 0x06000E8C RID: 3724 RVA: 0x0004C60C File Offset: 0x0004A80C
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			WebPermission webPermission = target as WebPermission;
			if (webPermission == null)
			{
				throw new ArgumentException(SR.GetString("net_perm_target"), "target");
			}
			if (this.m_noRestriction)
			{
				return webPermission.Copy();
			}
			if (webPermission.m_noRestriction)
			{
				return this.Copy();
			}
			WebPermission webPermission2 = new WebPermission();
			if (this.m_UnrestrictedConnect && webPermission.m_UnrestrictedConnect)
			{
				webPermission2.m_UnrestrictedConnect = true;
			}
			else if (this.m_UnrestrictedConnect || webPermission.m_UnrestrictedConnect)
			{
				webPermission2.m_connectList = (ArrayList)(this.m_UnrestrictedConnect ? webPermission : this).m_connectList.Clone();
			}
			else
			{
				WebPermission.intersectList(this.m_connectList, webPermission.m_connectList, webPermission2.m_connectList);
			}
			if (this.m_UnrestrictedAccept && webPermission.m_UnrestrictedAccept)
			{
				webPermission2.m_UnrestrictedAccept = true;
			}
			else if (this.m_UnrestrictedAccept || webPermission.m_UnrestrictedAccept)
			{
				webPermission2.m_acceptList = (ArrayList)(this.m_UnrestrictedAccept ? webPermission : this).m_acceptList.Clone();
			}
			else
			{
				WebPermission.intersectList(this.m_acceptList, webPermission.m_acceptList, webPermission2.m_acceptList);
			}
			if (!webPermission2.m_UnrestrictedConnect && !webPermission2.m_UnrestrictedAccept && webPermission2.m_connectList.Count == 0 && webPermission2.m_acceptList.Count == 0)
			{
				return null;
			}
			return webPermission2;
		}

		/// <summary>Reconstructs a <see cref="T:System.Net.WebPermission" /> from an XML encoding.</summary>
		/// <param name="securityElement">The XML encoding from which to reconstruct the <see cref="T:System.Net.WebPermission" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="securityElement" /> parameter is <see langword="null." /></exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="securityElement" /> is not a permission element for this type.</exception>
		// Token: 0x06000E8D RID: 3725 RVA: 0x0004C754 File Offset: 0x0004A954
		public override void FromXml(SecurityElement securityElement)
		{
			if (securityElement == null)
			{
				throw new ArgumentNullException("securityElement");
			}
			if (!securityElement.Tag.Equals("IPermission"))
			{
				throw new ArgumentException(SR.GetString("net_not_ipermission"), "securityElement");
			}
			string text = securityElement.Attribute("class");
			if (text == null)
			{
				throw new ArgumentException(SR.GetString("net_no_classname"), "securityElement");
			}
			if (text.IndexOf(base.GetType().FullName) < 0)
			{
				throw new ArgumentException(SR.GetString("net_no_typename"), "securityElement");
			}
			string text2 = securityElement.Attribute("Unrestricted");
			this.m_connectList = new ArrayList();
			this.m_acceptList = new ArrayList();
			this.m_UnrestrictedAccept = (this.m_UnrestrictedConnect = false);
			if (text2 != null && string.Compare(text2, "true", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.m_noRestriction = true;
				return;
			}
			this.m_noRestriction = false;
			SecurityElement securityElement2 = securityElement.SearchForChildByTag("ConnectAccess");
			if (securityElement2 != null)
			{
				foreach (object obj in securityElement2.Children)
				{
					SecurityElement securityElement3 = (SecurityElement)obj;
					if (securityElement3.Tag.Equals("URI"))
					{
						string text3;
						try
						{
							text3 = securityElement3.Attribute("uri");
						}
						catch
						{
							text3 = null;
						}
						if (text3 == null)
						{
							throw new ArgumentException(SR.GetString("net_perm_invalid_val_in_element"), "ConnectAccess");
						}
						if (text3 == ".*")
						{
							this.m_UnrestrictedConnect = true;
							this.m_connectList = new ArrayList();
							break;
						}
						this.AddAsPattern(NetworkAccess.Connect, new DelayedRegex(text3));
					}
				}
			}
			securityElement2 = securityElement.SearchForChildByTag("AcceptAccess");
			if (securityElement2 != null)
			{
				foreach (object obj2 in securityElement2.Children)
				{
					SecurityElement securityElement4 = (SecurityElement)obj2;
					if (securityElement4.Tag.Equals("URI"))
					{
						string text3;
						try
						{
							text3 = securityElement4.Attribute("uri");
						}
						catch
						{
							text3 = null;
						}
						if (text3 == null)
						{
							throw new ArgumentException(SR.GetString("net_perm_invalid_val_in_element"), "AcceptAccess");
						}
						if (text3 == ".*")
						{
							this.m_UnrestrictedAccept = true;
							this.m_acceptList = new ArrayList();
							break;
						}
						this.AddAsPattern(NetworkAccess.Accept, new DelayedRegex(text3));
					}
				}
			}
		}

		/// <summary>Creates an XML encoding of a <see cref="T:System.Net.WebPermission" /> and its current state.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> that contains an XML-encoded representation of the <see cref="T:System.Net.WebPermission" />, including state information.</returns>
		// Token: 0x06000E8E RID: 3726 RVA: 0x0004C9F0 File Offset: 0x0004ABF0
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", base.GetType().FullName + ", " + base.GetType().Module.Assembly.FullName.Replace('"', '\''));
			securityElement.AddAttribute("version", "1");
			if (!this.IsUnrestricted())
			{
				if (this.m_UnrestrictedConnect || this.m_connectList.Count > 0)
				{
					SecurityElement securityElement2 = new SecurityElement("ConnectAccess");
					if (this.m_UnrestrictedConnect)
					{
						SecurityElement securityElement3 = new SecurityElement("URI");
						securityElement3.AddAttribute("uri", SecurityElement.Escape(".*"));
						securityElement2.AddChild(securityElement3);
					}
					else
					{
						foreach (object obj in this.m_connectList)
						{
							Uri uri = obj as Uri;
							string text;
							if (uri != null)
							{
								text = Regex.Escape(uri.GetComponents(UriComponents.HttpRequestUrl, UriFormat.UriEscaped));
							}
							else
							{
								text = obj.ToString();
							}
							if (obj is string)
							{
								text = Regex.Escape(text);
							}
							SecurityElement securityElement4 = new SecurityElement("URI");
							securityElement4.AddAttribute("uri", SecurityElement.Escape(text));
							securityElement2.AddChild(securityElement4);
						}
					}
					securityElement.AddChild(securityElement2);
				}
				if (this.m_UnrestrictedAccept || this.m_acceptList.Count > 0)
				{
					SecurityElement securityElement5 = new SecurityElement("AcceptAccess");
					if (this.m_UnrestrictedAccept)
					{
						SecurityElement securityElement6 = new SecurityElement("URI");
						securityElement6.AddAttribute("uri", SecurityElement.Escape(".*"));
						securityElement5.AddChild(securityElement6);
					}
					else
					{
						foreach (object obj2 in this.m_acceptList)
						{
							Uri uri2 = obj2 as Uri;
							string text;
							if (uri2 != null)
							{
								text = Regex.Escape(uri2.GetComponents(UriComponents.HttpRequestUrl, UriFormat.UriEscaped));
							}
							else
							{
								text = obj2.ToString();
							}
							if (obj2 is string)
							{
								text = Regex.Escape(text);
							}
							SecurityElement securityElement7 = new SecurityElement("URI");
							securityElement7.AddAttribute("uri", SecurityElement.Escape(text));
							securityElement5.AddChild(securityElement7);
						}
					}
					securityElement.AddChild(securityElement5);
				}
			}
			else
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x0004CC90 File Offset: 0x0004AE90
		private static bool isMatchedURI(object uriToCheck, ArrayList uriPatternList)
		{
			string text = uriToCheck as string;
			foreach (object obj in uriPatternList)
			{
				DelayedRegex delayedRegex = obj as DelayedRegex;
				if (delayedRegex == null)
				{
					if (uriToCheck.GetType() == obj.GetType())
					{
						if (text != null && string.Compare(text, (string)obj, StringComparison.OrdinalIgnoreCase) == 0)
						{
							return true;
						}
						if (text == null && uriToCheck.Equals(obj))
						{
							return true;
						}
					}
				}
				else
				{
					string text2 = ((text != null) ? text : ((Uri)uriToCheck).GetComponents(UriComponents.HttpRequestUrl, UriFormat.UriEscaped));
					Match match = delayedRegex.AsRegex.Match(text2);
					if (match != null && match.Index == 0 && match.Length == text2.Length)
					{
						return true;
					}
					if (text == null)
					{
						text2 = ((Uri)uriToCheck).GetComponents(UriComponents.HttpRequestUrl, UriFormat.SafeUnescaped);
						match = delayedRegex.AsRegex.Match(text2);
						if (match != null && match.Index == 0 && match.Length == text2.Length)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x0004CDCC File Offset: 0x0004AFCC
		private static void intersectList(ArrayList A, ArrayList B, ArrayList result)
		{
			bool[] array = new bool[A.Count];
			bool[] array2 = new bool[B.Count];
			int num = 0;
			foreach (object obj in A)
			{
				int num2 = 0;
				foreach (object obj2 in B)
				{
					if (!array2[num2] && obj.GetType() == obj2.GetType())
					{
						if (obj is Uri)
						{
							if (obj.Equals(obj2))
							{
								result.Add(obj);
								array[num] = (array2[num2] = true);
								break;
							}
						}
						else if (string.Compare(obj.ToString(), obj2.ToString(), StringComparison.OrdinalIgnoreCase) == 0)
						{
							result.Add(obj);
							array[num] = (array2[num2] = true);
							break;
						}
					}
					num2++;
				}
				num++;
			}
			num = 0;
			foreach (object obj3 in A)
			{
				if (!array[num])
				{
					int num2 = 0;
					foreach (object obj4 in B)
					{
						if (!array2[num2])
						{
							bool flag;
							object obj5 = WebPermission.intersectPair(obj3, obj4, out flag);
							if (obj5 != null)
							{
								bool flag2 = false;
								foreach (object obj6 in result)
								{
									if (flag == obj6 is Uri && (flag ? obj5.Equals(obj6) : (string.Compare(obj6.ToString(), obj5.ToString(), StringComparison.OrdinalIgnoreCase) == 0)))
									{
										flag2 = true;
										break;
									}
								}
								if (!flag2)
								{
									result.Add(obj5);
								}
							}
						}
						num2++;
					}
				}
				num++;
			}
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0004D03C File Offset: 0x0004B23C
		private static object intersectPair(object L, object R, out bool isUri)
		{
			isUri = false;
			DelayedRegex delayedRegex = L as DelayedRegex;
			DelayedRegex delayedRegex2 = R as DelayedRegex;
			if (delayedRegex != null && delayedRegex2 != null)
			{
				return new DelayedRegex(string.Concat(new string[]
				{
					"(?=(",
					delayedRegex.ToString(),
					"))(",
					delayedRegex2.ToString(),
					")"
				}));
			}
			if (delayedRegex != null && delayedRegex2 == null)
			{
				isUri = R is Uri;
				string text = (isUri ? ((Uri)R).GetComponents(UriComponents.HttpRequestUrl, UriFormat.UriEscaped) : R.ToString());
				Match match = delayedRegex.AsRegex.Match(text);
				if (match != null && match.Index == 0 && match.Length == text.Length)
				{
					return R;
				}
				return null;
			}
			else if (delayedRegex == null && delayedRegex2 != null)
			{
				isUri = L is Uri;
				string text2 = (isUri ? ((Uri)L).GetComponents(UriComponents.HttpRequestUrl, UriFormat.UriEscaped) : L.ToString());
				Match match2 = delayedRegex2.AsRegex.Match(text2);
				if (match2 != null && match2.Index == 0 && match2.Length == text2.Length)
				{
					return L;
				}
				return null;
			}
			else
			{
				isUri = L is Uri;
				if (isUri)
				{
					if (!L.Equals(R))
					{
						return null;
					}
					return L;
				}
				else
				{
					if (string.Compare(L.ToString(), R.ToString(), StringComparison.OrdinalIgnoreCase) != 0)
					{
						return null;
					}
					return L;
				}
			}
		}

		// Token: 0x04001265 RID: 4709
		private bool m_noRestriction;

		// Token: 0x04001266 RID: 4710
		[OptionalField]
		private bool m_UnrestrictedConnect;

		// Token: 0x04001267 RID: 4711
		[OptionalField]
		private bool m_UnrestrictedAccept;

		// Token: 0x04001268 RID: 4712
		private ArrayList m_connectList = new ArrayList();

		// Token: 0x04001269 RID: 4713
		private ArrayList m_acceptList = new ArrayList();

		// Token: 0x0400126A RID: 4714
		internal const string MatchAll = ".*";

		// Token: 0x0400126B RID: 4715
		private static volatile Regex s_MatchAllRegex;
	}
}
