using System;
using System.Security;
using System.Security.Permissions;

namespace System.Net
{
	/// <summary>Specifies permission to access Internet resources. This class cannot be inherited.</summary>
	// Token: 0x02000185 RID: 389
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class WebPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebPermissionAttribute" /> class with a value that specifies the security actions that can be performed on this class.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="action" /> is not a valid <see cref="T:System.Security.Permissions.SecurityAction" /> value.</exception>
		// Token: 0x06000E6B RID: 3691 RVA: 0x0004B72E File Offset: 0x0004992E
		public WebPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets the URI connection string controlled by the current <see cref="T:System.Net.WebPermissionAttribute" />.</summary>
		/// <returns>A string containing the URI connection controlled by the current <see cref="T:System.Net.WebPermissionAttribute" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Net.WebPermissionAttribute.Connect" /> is not <see langword="null" /> when you attempt to set the value. If you wish to specify more than one Connect URI, use an additional attribute declaration statement.</exception>
		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x0004B737 File Offset: 0x00049937
		// (set) Token: 0x06000E6D RID: 3693 RVA: 0x0004B744 File Offset: 0x00049944
		public string Connect
		{
			get
			{
				return this.m_connect as string;
			}
			set
			{
				if (this.m_connect != null)
				{
					throw new ArgumentException(SR.GetString("net_perm_attrib_multi", new object[] { "Connect", value }), "value");
				}
				this.m_connect = value;
			}
		}

		/// <summary>Gets or sets the URI string accepted by the current <see cref="T:System.Net.WebPermissionAttribute" />.</summary>
		/// <returns>A string containing the URI accepted by the current <see cref="T:System.Net.WebPermissionAttribute" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Net.WebPermissionAttribute.Accept" /> is not <see langword="null" /> when you attempt to set the value. If you wish to specify more than one Accept URI, use an additional attribute declaration statement.</exception>
		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000E6E RID: 3694 RVA: 0x0004B77C File Offset: 0x0004997C
		// (set) Token: 0x06000E6F RID: 3695 RVA: 0x0004B789 File Offset: 0x00049989
		public string Accept
		{
			get
			{
				return this.m_accept as string;
			}
			set
			{
				if (this.m_accept != null)
				{
					throw new ArgumentException(SR.GetString("net_perm_attrib_multi", new object[] { "Accept", value }), "value");
				}
				this.m_accept = value;
			}
		}

		/// <summary>Gets or sets a regular expression pattern that describes the URI connection controlled by the current <see cref="T:System.Net.WebPermissionAttribute" />.</summary>
		/// <returns>A string containing a regular expression pattern that describes the URI connection controlled by this <see cref="T:System.Net.WebPermissionAttribute" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Net.WebPermissionAttribute.ConnectPattern" /> is not <see langword="null" /> when you attempt to set the value. If you wish to specify more than one connect URI, use an additional attribute declaration statement.</exception>
		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000E70 RID: 3696 RVA: 0x0004B7C1 File Offset: 0x000499C1
		// (set) Token: 0x06000E71 RID: 3697 RVA: 0x0004B800 File Offset: 0x00049A00
		public string ConnectPattern
		{
			get
			{
				if (this.m_connect is DelayedRegex)
				{
					return this.m_connect.ToString();
				}
				if (!(this.m_connect is bool) || !(bool)this.m_connect)
				{
					return null;
				}
				return ".*";
			}
			set
			{
				if (this.m_connect != null)
				{
					throw new ArgumentException(SR.GetString("net_perm_attrib_multi", new object[] { "ConnectPatern", value }), "value");
				}
				if (value == ".*")
				{
					this.m_connect = true;
					return;
				}
				this.m_connect = new DelayedRegex(value);
			}
		}

		/// <summary>Gets or sets a regular expression pattern that describes the URI accepted by the current <see cref="T:System.Net.WebPermissionAttribute" />.</summary>
		/// <returns>A string containing a regular expression pattern that describes the URI accepted by the current <see cref="T:System.Net.WebPermissionAttribute" />. This string must be escaped according to the rules for encoding a <see cref="T:System.Text.RegularExpressions.Regex" /> constructor string.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Net.WebPermissionAttribute.AcceptPattern" /> is not <see langword="null" /> when you attempt to set the value. If you wish to specify more than one Accept URI, use an additional attribute declaration statement.</exception>
		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000E72 RID: 3698 RVA: 0x0004B862 File Offset: 0x00049A62
		// (set) Token: 0x06000E73 RID: 3699 RVA: 0x0004B8A0 File Offset: 0x00049AA0
		public string AcceptPattern
		{
			get
			{
				if (this.m_accept is DelayedRegex)
				{
					return this.m_accept.ToString();
				}
				if (!(this.m_accept is bool) || !(bool)this.m_accept)
				{
					return null;
				}
				return ".*";
			}
			set
			{
				if (this.m_accept != null)
				{
					throw new ArgumentException(SR.GetString("net_perm_attrib_multi", new object[] { "AcceptPattern", value }), "value");
				}
				if (value == ".*")
				{
					this.m_accept = true;
					return;
				}
				this.m_accept = new DelayedRegex(value);
			}
		}

		/// <summary>Creates and returns a new instance of the <see cref="T:System.Net.WebPermission" /> class.</summary>
		/// <returns>A <see cref="T:System.Net.WebPermission" /> corresponding to the security declaration.</returns>
		// Token: 0x06000E74 RID: 3700 RVA: 0x0004B904 File Offset: 0x00049B04
		public override IPermission CreatePermission()
		{
			WebPermission webPermission;
			if (base.Unrestricted)
			{
				webPermission = new WebPermission(PermissionState.Unrestricted);
			}
			else
			{
				NetworkAccess networkAccess = (NetworkAccess)0;
				if (this.m_connect is bool)
				{
					if ((bool)this.m_connect)
					{
						networkAccess |= NetworkAccess.Connect;
					}
					this.m_connect = null;
				}
				if (this.m_accept is bool)
				{
					if ((bool)this.m_accept)
					{
						networkAccess |= NetworkAccess.Accept;
					}
					this.m_accept = null;
				}
				webPermission = new WebPermission(networkAccess);
				if (this.m_accept != null)
				{
					if (this.m_accept is DelayedRegex)
					{
						webPermission.AddAsPattern(NetworkAccess.Accept, (DelayedRegex)this.m_accept);
					}
					else
					{
						webPermission.AddPermission(NetworkAccess.Accept, (string)this.m_accept);
					}
				}
				if (this.m_connect != null)
				{
					if (this.m_connect is DelayedRegex)
					{
						webPermission.AddAsPattern(NetworkAccess.Connect, (DelayedRegex)this.m_connect);
					}
					else
					{
						webPermission.AddPermission(NetworkAccess.Connect, (string)this.m_connect);
					}
				}
			}
			return webPermission;
		}

		// Token: 0x04001261 RID: 4705
		private object m_accept;

		// Token: 0x04001262 RID: 4706
		private object m_connect;
	}
}
