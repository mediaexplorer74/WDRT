using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002F7 RID: 759
	internal struct FixedInfo
	{
		// Token: 0x06001AB4 RID: 6836 RVA: 0x00080CB4 File Offset: 0x0007EEB4
		internal FixedInfo(FIXED_INFO info)
		{
			this.info = info;
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001AB5 RID: 6837 RVA: 0x00080CBD File Offset: 0x0007EEBD
		internal string HostName
		{
			get
			{
				return this.info.hostName;
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001AB6 RID: 6838 RVA: 0x00080CCA File Offset: 0x0007EECA
		internal string DomainName
		{
			get
			{
				return this.info.domainName;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001AB7 RID: 6839 RVA: 0x00080CD7 File Offset: 0x0007EED7
		internal NetBiosNodeType NodeType
		{
			get
			{
				return this.info.nodeType;
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06001AB8 RID: 6840 RVA: 0x00080CE4 File Offset: 0x0007EEE4
		internal string ScopeId
		{
			get
			{
				return this.info.scopeId;
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001AB9 RID: 6841 RVA: 0x00080CF1 File Offset: 0x0007EEF1
		internal bool EnableRouting
		{
			get
			{
				return this.info.enableRouting;
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001ABA RID: 6842 RVA: 0x00080CFE File Offset: 0x0007EEFE
		internal bool EnableProxy
		{
			get
			{
				return this.info.enableProxy;
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001ABB RID: 6843 RVA: 0x00080D0B File Offset: 0x0007EF0B
		internal bool EnableDns
		{
			get
			{
				return this.info.enableDns;
			}
		}

		// Token: 0x04001A9F RID: 6815
		internal FIXED_INFO info;
	}
}
