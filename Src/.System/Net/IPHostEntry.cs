using System;

namespace System.Net
{
	/// <summary>Provides a container class for Internet host address information.</summary>
	// Token: 0x0200014D RID: 333
	public class IPHostEntry
	{
		/// <summary>Gets or sets the DNS name of the host.</summary>
		/// <returns>A string that contains the primary host name for the server.</returns>
		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x0003F4DB File Offset: 0x0003D6DB
		// (set) Token: 0x06000B9C RID: 2972 RVA: 0x0003F4E3 File Offset: 0x0003D6E3
		public string HostName
		{
			get
			{
				return this.hostName;
			}
			set
			{
				this.hostName = value;
			}
		}

		/// <summary>Gets or sets a list of aliases that are associated with a host.</summary>
		/// <returns>An array of strings that contain DNS names that resolve to the IP addresses in the <see cref="P:System.Net.IPHostEntry.AddressList" /> property.</returns>
		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x0003F4EC File Offset: 0x0003D6EC
		// (set) Token: 0x06000B9E RID: 2974 RVA: 0x0003F4F4 File Offset: 0x0003D6F4
		public string[] Aliases
		{
			get
			{
				return this.aliases;
			}
			set
			{
				this.aliases = value;
			}
		}

		/// <summary>Gets or sets a list of IP addresses that are associated with a host.</summary>
		/// <returns>An array of type <see cref="T:System.Net.IPAddress" /> that contains IP addresses that resolve to the host names that are contained in the <see cref="P:System.Net.IPHostEntry.Aliases" /> property.</returns>
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x0003F4FD File Offset: 0x0003D6FD
		// (set) Token: 0x06000BA0 RID: 2976 RVA: 0x0003F505 File Offset: 0x0003D705
		public IPAddress[] AddressList
		{
			get
			{
				return this.addressList;
			}
			set
			{
				this.addressList = value;
			}
		}

		// Token: 0x04001102 RID: 4354
		private string hostName;

		// Token: 0x04001103 RID: 4355
		private string[] aliases;

		// Token: 0x04001104 RID: 4356
		private IPAddress[] addressList;

		// Token: 0x04001105 RID: 4357
		internal bool isTrustedHost = true;
	}
}
