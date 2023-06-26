using System;
using System.Globalization;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Net
{
	/// <summary>Specifies security actions to control <see cref="T:System.Net.Sockets.Socket" /> connections. This class cannot be inherited.</summary>
	// Token: 0x02000162 RID: 354
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class SocketPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.SocketPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" /> value.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="action" /> is not a valid <see cref="T:System.Security.Permissions.SecurityAction" /> value.</exception>
		// Token: 0x06000CC0 RID: 3264 RVA: 0x00043FF1 File Offset: 0x000421F1
		public SocketPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets the network access method that is allowed by this <see cref="T:System.Net.SocketPermissionAttribute" />.</summary>
		/// <returns>A string that contains the network access method that is allowed by this instance of <see cref="T:System.Net.SocketPermissionAttribute" />. Valid values are "Accept" and "Connect."</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Net.SocketPermissionAttribute.Access" /> property is not <see langword="null" /> when you attempt to set the value. To specify more than one Access method, use an additional attribute declaration statement.</exception>
		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x00043FFA File Offset: 0x000421FA
		// (set) Token: 0x06000CC2 RID: 3266 RVA: 0x00044002 File Offset: 0x00042202
		public string Access
		{
			get
			{
				return this.m_access;
			}
			set
			{
				if (this.m_access != null)
				{
					throw new ArgumentException(SR.GetString("net_perm_attrib_multi", new object[] { "Access", value }), "value");
				}
				this.m_access = value;
			}
		}

		/// <summary>Gets or sets the DNS host name or IP address that is specified by this <see cref="T:System.Net.SocketPermissionAttribute" />.</summary>
		/// <returns>A string that contains the DNS host name or IP address that is associated with this instance of <see cref="T:System.Net.SocketPermissionAttribute" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Net.SocketPermissionAttribute.Host" /> is not <see langword="null" /> when you attempt to set the value. To specify more than one host, use an additional attribute declaration statement.</exception>
		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x0004403A File Offset: 0x0004223A
		// (set) Token: 0x06000CC4 RID: 3268 RVA: 0x00044042 File Offset: 0x00042242
		public string Host
		{
			get
			{
				return this.m_host;
			}
			set
			{
				if (this.m_host != null)
				{
					throw new ArgumentException(SR.GetString("net_perm_attrib_multi", new object[] { "Host", value }), "value");
				}
				this.m_host = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Net.TransportType" /> that is specified by this <see cref="T:System.Net.SocketPermissionAttribute" />.</summary>
		/// <returns>A string that contains the <see cref="T:System.Net.TransportType" /> that is associated with this <see cref="T:System.Net.SocketPermissionAttribute" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Net.SocketPermissionAttribute.Transport" /> is not <see langword="null" /> when you attempt to set the value. To specify more than one transport type, use an additional attribute declaration statement.</exception>
		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x0004407A File Offset: 0x0004227A
		// (set) Token: 0x06000CC6 RID: 3270 RVA: 0x00044082 File Offset: 0x00042282
		public string Transport
		{
			get
			{
				return this.m_transport;
			}
			set
			{
				if (this.m_transport != null)
				{
					throw new ArgumentException(SR.GetString("net_perm_attrib_multi", new object[] { "Transport", value }), "value");
				}
				this.m_transport = value;
			}
		}

		/// <summary>Gets or sets the port number that is associated with this <see cref="T:System.Net.SocketPermissionAttribute" />.</summary>
		/// <returns>A string that contains the port number that is associated with this instance of <see cref="T:System.Net.SocketPermissionAttribute" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Net.SocketPermissionAttribute.Port" /> property is <see langword="null" /> when you attempt to set the value. To specify more than one port, use an additional attribute declaration statement.</exception>
		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x000440BA File Offset: 0x000422BA
		// (set) Token: 0x06000CC8 RID: 3272 RVA: 0x000440C2 File Offset: 0x000422C2
		public string Port
		{
			get
			{
				return this.m_port;
			}
			set
			{
				if (this.m_port != null)
				{
					throw new ArgumentException(SR.GetString("net_perm_attrib_multi", new object[] { "Port", value }), "value");
				}
				this.m_port = value;
			}
		}

		/// <summary>Creates and returns a new instance of the <see cref="T:System.Net.SocketPermission" /> class.</summary>
		/// <returns>An instance of the <see cref="T:System.Net.SocketPermission" /> class that corresponds to the security declaration.</returns>
		/// <exception cref="T:System.ArgumentException">One or more of the current instance's <see cref="P:System.Net.SocketPermissionAttribute.Access" />, <see cref="P:System.Net.SocketPermissionAttribute.Host" />, <see cref="P:System.Net.SocketPermissionAttribute.Transport" />, or <see cref="P:System.Net.SocketPermissionAttribute.Port" /> properties is <see langword="null" />.</exception>
		// Token: 0x06000CC9 RID: 3273 RVA: 0x000440FC File Offset: 0x000422FC
		public override IPermission CreatePermission()
		{
			SocketPermission socketPermission;
			if (base.Unrestricted)
			{
				socketPermission = new SocketPermission(PermissionState.Unrestricted);
			}
			else
			{
				socketPermission = new SocketPermission(PermissionState.None);
				if (this.m_access == null)
				{
					throw new ArgumentException(SR.GetString("net_perm_attrib_count", new object[] { "Access" }));
				}
				if (this.m_host == null)
				{
					throw new ArgumentException(SR.GetString("net_perm_attrib_count", new object[] { "Host" }));
				}
				if (this.m_transport == null)
				{
					throw new ArgumentException(SR.GetString("net_perm_attrib_count", new object[] { "Transport" }));
				}
				if (this.m_port == null)
				{
					throw new ArgumentException(SR.GetString("net_perm_attrib_count", new object[] { "Port" }));
				}
				this.ParseAddPermissions(socketPermission);
			}
			return socketPermission;
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x000441C8 File Offset: 0x000423C8
		private void ParseAddPermissions(SocketPermission perm)
		{
			NetworkAccess networkAccess;
			if (string.Compare(this.m_access, "Connect", StringComparison.OrdinalIgnoreCase) == 0)
			{
				networkAccess = NetworkAccess.Connect;
			}
			else
			{
				if (string.Compare(this.m_access, "Accept", StringComparison.OrdinalIgnoreCase) != 0)
				{
					throw new ArgumentException(SR.GetString("net_perm_invalid_val", new object[] { "Access", this.m_access }));
				}
				networkAccess = NetworkAccess.Accept;
			}
			TransportType transportType;
			try
			{
				transportType = (TransportType)Enum.Parse(typeof(TransportType), this.m_transport, true);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new ArgumentException(SR.GetString("net_perm_invalid_val", new object[] { "Transport", this.m_transport }), ex);
			}
			if (string.Compare(this.m_port, "All", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.m_port = "-1";
			}
			int num;
			try
			{
				num = int.Parse(this.m_port, NumberFormatInfo.InvariantInfo);
			}
			catch (Exception ex2)
			{
				if (ex2 is ThreadAbortException || ex2 is StackOverflowException || ex2 is OutOfMemoryException)
				{
					throw;
				}
				throw new ArgumentException(SR.GetString("net_perm_invalid_val", new object[] { "Port", this.m_port }), ex2);
			}
			if (!ValidationHelper.ValidateTcpPort(num) && num != -1)
			{
				throw new ArgumentOutOfRangeException("port", num, SR.GetString("net_perm_invalid_val", new object[] { "Port", this.m_port }));
			}
			perm.AddPermission(networkAccess, transportType, this.m_host, num);
		}

		// Token: 0x040011AC RID: 4524
		private string m_access;

		// Token: 0x040011AD RID: 4525
		private string m_host;

		// Token: 0x040011AE RID: 4526
		private string m_port;

		// Token: 0x040011AF RID: 4527
		private string m_transport;

		// Token: 0x040011B0 RID: 4528
		private const string strAccess = "Access";

		// Token: 0x040011B1 RID: 4529
		private const string strConnect = "Connect";

		// Token: 0x040011B2 RID: 4530
		private const string strAccept = "Accept";

		// Token: 0x040011B3 RID: 4531
		private const string strHost = "Host";

		// Token: 0x040011B4 RID: 4532
		private const string strTransport = "Transport";

		// Token: 0x040011B5 RID: 4533
		private const string strPort = "Port";
	}
}
