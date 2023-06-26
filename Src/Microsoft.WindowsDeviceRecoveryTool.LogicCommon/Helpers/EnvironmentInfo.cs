using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.NetworkInformation;
using Microsoft.WindowsDeviceRecoveryTool.Model;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Helpers
{
	// Token: 0x02000030 RID: 48
	internal sealed class EnvironmentInfo : IEnvironmentInfo
	{
		// Token: 0x06000310 RID: 784 RVA: 0x0000C4FC File Offset: 0x0000A6FC
		public EnvironmentInfo(ApplicationInfo applicationInfo)
		{
			bool flag = applicationInfo == null;
			if (flag)
			{
				throw new ArgumentNullException("applicationInfo");
			}
			this.applicationInfo = applicationInfo;
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0000C52C File Offset: 0x0000A72C
		public string UserSiteLanguage
		{
			get
			{
				return CultureInfo.CurrentUICulture.Name;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000312 RID: 786 RVA: 0x0000C548 File Offset: 0x0000A748
		public PhysicalAddress[] PhysicalAddressList
		{
			get
			{
				NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
				List<PhysicalAddress> list = new List<PhysicalAddress>();
				foreach (NetworkInterface networkInterface in allNetworkInterfaces)
				{
					list.Add(networkInterface.GetPhysicalAddress());
				}
				return list.ToArray();
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000313 RID: 787 RVA: 0x0000C594 File Offset: 0x0000A794
		public IPAddress[] IPAddressList
		{
			get
			{
				string hostName = Dns.GetHostName();
				IPHostEntry hostEntry = Dns.GetHostEntry(hostName);
				return hostEntry.AddressList;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000314 RID: 788 RVA: 0x0000C5BC File Offset: 0x0000A7BC
		public string ApplicationName
		{
			get
			{
				return this.applicationInfo.ApplicationDisplayName;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0000C5DC File Offset: 0x0000A7DC
		public string ApplicationVersion
		{
			get
			{
				return this.applicationInfo.ApplicationVersion;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0000C5FC File Offset: 0x0000A7FC
		public string ApplicationVendor
		{
			get
			{
				return this.applicationInfo.CompanyName;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000317 RID: 791 RVA: 0x0000C61C File Offset: 0x0000A81C
		public string NasVersion
		{
			get
			{
				return "Unknown";
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0000C634 File Offset: 0x0000A834
		public string PapiVersion
		{
			get
			{
				return "Unknown";
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000319 RID: 793 RVA: 0x0000C64C File Offset: 0x0000A84C
		public string CapiVersion
		{
			get
			{
				return "Unknown";
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600031A RID: 794 RVA: 0x0000C664 File Offset: 0x0000A864
		public string FuseVersion
		{
			get
			{
				return "Unknown";
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000C67C File Offset: 0x0000A87C
		public string OSVersion
		{
			get
			{
				return Environment.OSVersion.VersionString;
			}
		}

		// Token: 0x0400013F RID: 319
		private readonly ApplicationInfo applicationInfo;
	}
}
