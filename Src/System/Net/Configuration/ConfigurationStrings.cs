using System;
using System.Globalization;

namespace System.Net.Configuration
{
	// Token: 0x0200032A RID: 810
	internal static class ConfigurationStrings
	{
		// Token: 0x06001CFE RID: 7422 RVA: 0x0008A8E5 File Offset: 0x00088AE5
		private static string GetSectionPath(string sectionName)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[] { "system.net", sectionName });
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x0008A908 File Offset: 0x00088B08
		private static string GetSectionPath(string sectionName, string subSectionName)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}/{1}/{2}", new object[] { "system.net", sectionName, subSectionName });
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06001D00 RID: 7424 RVA: 0x0008A92F File Offset: 0x00088B2F
		internal static string AuthenticationModulesSectionPath
		{
			get
			{
				return ConfigurationStrings.GetSectionPath("authenticationModules");
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06001D01 RID: 7425 RVA: 0x0008A93B File Offset: 0x00088B3B
		internal static string ConnectionManagementSectionPath
		{
			get
			{
				return ConfigurationStrings.GetSectionPath("connectionManagement");
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06001D02 RID: 7426 RVA: 0x0008A947 File Offset: 0x00088B47
		internal static string DefaultProxySectionPath
		{
			get
			{
				return ConfigurationStrings.GetSectionPath("defaultProxy");
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06001D03 RID: 7427 RVA: 0x0008A953 File Offset: 0x00088B53
		internal static string SmtpSectionPath
		{
			get
			{
				return ConfigurationStrings.GetSectionPath("mailSettings", "smtp");
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06001D04 RID: 7428 RVA: 0x0008A964 File Offset: 0x00088B64
		internal static string RequestCachingSectionPath
		{
			get
			{
				return ConfigurationStrings.GetSectionPath("requestCaching");
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06001D05 RID: 7429 RVA: 0x0008A970 File Offset: 0x00088B70
		internal static string SettingsSectionPath
		{
			get
			{
				return ConfigurationStrings.GetSectionPath("settings");
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06001D06 RID: 7430 RVA: 0x0008A97C File Offset: 0x00088B7C
		internal static string WebRequestModulesSectionPath
		{
			get
			{
				return ConfigurationStrings.GetSectionPath("webRequestModules");
			}
		}

		// Token: 0x04001BB4 RID: 7092
		internal const string Address = "address";

		// Token: 0x04001BB5 RID: 7093
		internal const string AutoConfigUrlRetryInterval = "autoConfigUrlRetryInterval";

		// Token: 0x04001BB6 RID: 7094
		internal const string AutoDetect = "autoDetect";

		// Token: 0x04001BB7 RID: 7095
		internal const string AlwaysUseCompletionPortsForAccept = "alwaysUseCompletionPortsForAccept";

		// Token: 0x04001BB8 RID: 7096
		internal const string AlwaysUseCompletionPortsForConnect = "alwaysUseCompletionPortsForConnect";

		// Token: 0x04001BB9 RID: 7097
		internal const string AuthenticationModulesSectionName = "authenticationModules";

		// Token: 0x04001BBA RID: 7098
		internal const string BypassList = "bypasslist";

		// Token: 0x04001BBB RID: 7099
		internal const string BypassOnLocal = "bypassonlocal";

		// Token: 0x04001BBC RID: 7100
		internal const string CheckCertificateName = "checkCertificateName";

		// Token: 0x04001BBD RID: 7101
		internal const string CheckCertificateRevocationList = "checkCertificateRevocationList";

		// Token: 0x04001BBE RID: 7102
		internal const string ClientDomain = "clientDomain";

		// Token: 0x04001BBF RID: 7103
		internal const string ConnectionManagementSectionName = "connectionManagement";

		// Token: 0x04001BC0 RID: 7104
		internal const string DefaultCredentials = "defaultCredentials";

		// Token: 0x04001BC1 RID: 7105
		internal const string DefaultCredentialsHandleCacheSize = "defaultCredentialsHandleCacheSize";

		// Token: 0x04001BC2 RID: 7106
		internal const string DefaultHttpCachePolicy = "defaultHttpCachePolicy";

		// Token: 0x04001BC3 RID: 7107
		internal const string DefaultFtpCachePolicy = "defaultFtpCachePolicy";

		// Token: 0x04001BC4 RID: 7108
		internal const string DefaultPolicyLevel = "defaultPolicyLevel";

		// Token: 0x04001BC5 RID: 7109
		internal const string DefaultProxySectionName = "defaultProxy";

		// Token: 0x04001BC6 RID: 7110
		internal const string DeliveryMethod = "deliveryMethod";

		// Token: 0x04001BC7 RID: 7111
		internal const string DeliveryFormat = "deliveryFormat";

		// Token: 0x04001BC8 RID: 7112
		internal const string DisableAllCaching = "disableAllCaching";

		// Token: 0x04001BC9 RID: 7113
		internal const string DnsRefreshTimeout = "dnsRefreshTimeout";

		// Token: 0x04001BCA RID: 7114
		internal const string DownloadTimeout = "downloadTimeout";

		// Token: 0x04001BCB RID: 7115
		internal const string Enabled = "enabled";

		// Token: 0x04001BCC RID: 7116
		internal const string EnableDnsRoundRobin = "enableDnsRoundRobin";

		// Token: 0x04001BCD RID: 7117
		internal const string EnableSsl = "enableSsl";

		// Token: 0x04001BCE RID: 7118
		internal const string EncryptionPolicy = "encryptionPolicy";

		// Token: 0x04001BCF RID: 7119
		internal const string Expect100Continue = "expect100Continue";

		// Token: 0x04001BD0 RID: 7120
		internal const string File = "file:";

		// Token: 0x04001BD1 RID: 7121
		internal const string From = "from";

		// Token: 0x04001BD2 RID: 7122
		internal const string Ftp = "ftp:";

		// Token: 0x04001BD3 RID: 7123
		internal const string Host = "host";

		// Token: 0x04001BD4 RID: 7124
		internal const string HttpWebRequest = "httpWebRequest";

		// Token: 0x04001BD5 RID: 7125
		internal const string HttpListener = "httpListener";

		// Token: 0x04001BD6 RID: 7126
		internal const string Http = "http:";

		// Token: 0x04001BD7 RID: 7127
		internal const string Https = "https:";

		// Token: 0x04001BD8 RID: 7128
		internal const string Ipv6 = "ipv6";

		// Token: 0x04001BD9 RID: 7129
		internal const string IsPrivateCache = "isPrivateCache";

		// Token: 0x04001BDA RID: 7130
		internal const string IPProtectionLevel = "ipProtectionLevel";

		// Token: 0x04001BDB RID: 7131
		internal const string MailSettingsSectionName = "mailSettings";

		// Token: 0x04001BDC RID: 7132
		internal const string MaxConnection = "maxconnection";

		// Token: 0x04001BDD RID: 7133
		internal const string MaximumAge = "maximumAge";

		// Token: 0x04001BDE RID: 7134
		internal const string MaximumStale = "maximumStale";

		// Token: 0x04001BDF RID: 7135
		internal const string MaximumResponseHeadersLength = "maximumResponseHeadersLength";

		// Token: 0x04001BE0 RID: 7136
		internal const string MaximumErrorResponseLength = "maximumErrorResponseLength";

		// Token: 0x04001BE1 RID: 7137
		internal const string MinimumFresh = "minimumFresh";

		// Token: 0x04001BE2 RID: 7138
		internal const string Module = "module";

		// Token: 0x04001BE3 RID: 7139
		internal const string Name = "name";

		// Token: 0x04001BE4 RID: 7140
		internal const string Network = "network";

		// Token: 0x04001BE5 RID: 7141
		internal const string Password = "password";

		// Token: 0x04001BE6 RID: 7142
		internal const string PerformanceCounters = "performanceCounters";

		// Token: 0x04001BE7 RID: 7143
		internal const string PickupDirectoryFromIis = "pickupDirectoryFromIis";

		// Token: 0x04001BE8 RID: 7144
		internal const string PickupDirectoryLocation = "pickupDirectoryLocation";

		// Token: 0x04001BE9 RID: 7145
		internal const string PolicyLevel = "policyLevel";

		// Token: 0x04001BEA RID: 7146
		internal const string Port = "port";

		// Token: 0x04001BEB RID: 7147
		internal const string Prefix = "prefix";

		// Token: 0x04001BEC RID: 7148
		internal const string Proxy = "proxy";

		// Token: 0x04001BED RID: 7149
		internal const string ProxyAddress = "proxyaddress";

		// Token: 0x04001BEE RID: 7150
		internal const string RequestCachingSectionName = "requestCaching";

		// Token: 0x04001BEF RID: 7151
		internal const string ScriptLocation = "scriptLocation";

		// Token: 0x04001BF0 RID: 7152
		internal const string SectionGroupName = "system.net";

		// Token: 0x04001BF1 RID: 7153
		internal const string ServicePointManager = "servicePointManager";

		// Token: 0x04001BF2 RID: 7154
		internal const string SettingsSectionName = "settings";

		// Token: 0x04001BF3 RID: 7155
		internal const string SmtpSectionName = "smtp";

		// Token: 0x04001BF4 RID: 7156
		internal const string Socket = "socket";

		// Token: 0x04001BF5 RID: 7157
		internal const string SpecifiedPickupDirectory = "specifiedPickupDirectory";

		// Token: 0x04001BF6 RID: 7158
		internal const string TargetName = "targetName";

		// Token: 0x04001BF7 RID: 7159
		internal const string Type = "type";

		// Token: 0x04001BF8 RID: 7160
		internal const string UnicodeDecodingConformance = "unicodeDecodingConformance";

		// Token: 0x04001BF9 RID: 7161
		internal const string UnicodeEncodingConformance = "unicodeEncodingConformance";

		// Token: 0x04001BFA RID: 7162
		internal const string UnspecifiedMaximumAge = "unspecifiedMaximumAge";

		// Token: 0x04001BFB RID: 7163
		internal const string UseDefaultCredentials = "useDefaultCredentials";

		// Token: 0x04001BFC RID: 7164
		internal const string UseNagleAlgorithm = "useNagleAlgorithm";

		// Token: 0x04001BFD RID: 7165
		internal const string UseSystemDefault = "usesystemdefault";

		// Token: 0x04001BFE RID: 7166
		internal const string UseUnsafeHeaderParsing = "useUnsafeHeaderParsing";

		// Token: 0x04001BFF RID: 7167
		internal const string UserName = "userName";

		// Token: 0x04001C00 RID: 7168
		internal const string WebProxyScript = "webProxyScript";

		// Token: 0x04001C01 RID: 7169
		internal const string WebRequestModulesSectionName = "webRequestModules";

		// Token: 0x04001C02 RID: 7170
		internal const string WebUtility = "webUtility";

		// Token: 0x04001C03 RID: 7171
		internal const string WindowsAuthentication = "windowsAuthentication";

		// Token: 0x04001C04 RID: 7172
		internal const string maximumUnauthorizedUploadLength = "maximumUnauthorizedUploadLength";

		// Token: 0x04001C05 RID: 7173
		internal const string UnescapeRequestUrl = "unescapeRequestUrl";

		// Token: 0x04001C06 RID: 7174
		internal const string Timeouts = "timeouts";

		// Token: 0x04001C07 RID: 7175
		internal const string EntityBody = "entityBody";

		// Token: 0x04001C08 RID: 7176
		internal const string DrainEntityBody = "drainEntityBody";

		// Token: 0x04001C09 RID: 7177
		internal const string RequestQueue = "requestQueue";

		// Token: 0x04001C0A RID: 7178
		internal const string IdleConnection = "idleConnection";

		// Token: 0x04001C0B RID: 7179
		internal const string HeaderWait = "headerWait";

		// Token: 0x04001C0C RID: 7180
		internal const string MinSendBytesPerSecond = "minSendBytesPerSecond";
	}
}
