using System;
using System.Configuration;
using System.Net.Security;
using System.Net.Sockets;
using System.Threading;

namespace System.Net.Configuration
{
	// Token: 0x02000340 RID: 832
	internal sealed class SettingsSectionInternal
	{
		// Token: 0x06001DAE RID: 7598 RVA: 0x0008C688 File Offset: 0x0008A888
		internal SettingsSectionInternal(SettingsSection section)
		{
			if (section == null)
			{
				section = new SettingsSection();
			}
			this.alwaysUseCompletionPortsForConnect = section.Socket.AlwaysUseCompletionPortsForConnect;
			this.alwaysUseCompletionPortsForAccept = section.Socket.AlwaysUseCompletionPortsForAccept;
			this.checkCertificateName = section.ServicePointManager.CheckCertificateName;
			this.checkCertificateRevocationList = section.ServicePointManager.CheckCertificateRevocationList;
			this.dnsRefreshTimeout = section.ServicePointManager.DnsRefreshTimeout;
			this.ipProtectionLevel = section.Socket.IPProtectionLevel;
			this.ipv6Enabled = section.Ipv6.Enabled;
			this.enableDnsRoundRobin = section.ServicePointManager.EnableDnsRoundRobin;
			this.encryptionPolicy = section.ServicePointManager.EncryptionPolicy;
			this.expect100Continue = section.ServicePointManager.Expect100Continue;
			this.maximumUnauthorizedUploadLength = section.HttpWebRequest.MaximumUnauthorizedUploadLength;
			this.maximumResponseHeadersLength = section.HttpWebRequest.MaximumResponseHeadersLength;
			this.maximumErrorResponseLength = section.HttpWebRequest.MaximumErrorResponseLength;
			this.useUnsafeHeaderParsing = section.HttpWebRequest.UseUnsafeHeaderParsing;
			this.useNagleAlgorithm = section.ServicePointManager.UseNagleAlgorithm;
			this.autoConfigUrlRetryInterval = section.WebProxyScript.AutoConfigUrlRetryInterval;
			TimeSpan timeSpan = section.WebProxyScript.DownloadTimeout;
			this.downloadTimeout = ((timeSpan == TimeSpan.MaxValue || timeSpan == TimeSpan.Zero) ? (-1) : ((int)timeSpan.TotalMilliseconds));
			this.performanceCountersEnabled = section.PerformanceCounters.Enabled;
			this.httpListenerUnescapeRequestUrl = section.HttpListener.UnescapeRequestUrl;
			this.httpListenerTimeouts = section.HttpListener.Timeouts.GetTimeouts();
			this.defaultCredentialsHandleCacheSize = section.WindowsAuthentication.DefaultCredentialsHandleCacheSize;
			WebUtilityElement webUtility = section.WebUtility;
			this.WebUtilityUnicodeDecodingConformance = webUtility.UnicodeDecodingConformance;
			this.WebUtilityUnicodeEncodingConformance = webUtility.UnicodeEncodingConformance;
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06001DAF RID: 7599 RVA: 0x0008C854 File Offset: 0x0008AA54
		internal static SettingsSectionInternal Section
		{
			get
			{
				if (SettingsSectionInternal.s_settings == null)
				{
					object internalSyncObject = SettingsSectionInternal.InternalSyncObject;
					lock (internalSyncObject)
					{
						if (SettingsSectionInternal.s_settings == null)
						{
							SettingsSectionInternal.s_settings = new SettingsSectionInternal((SettingsSection)PrivilegedConfigurationManager.GetSection(ConfigurationStrings.SettingsSectionPath));
						}
					}
				}
				return SettingsSectionInternal.s_settings;
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06001DB0 RID: 7600 RVA: 0x0008C8C4 File Offset: 0x0008AAC4
		private static object InternalSyncObject
		{
			get
			{
				if (SettingsSectionInternal.s_InternalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange(ref SettingsSectionInternal.s_InternalSyncObject, obj, null);
				}
				return SettingsSectionInternal.s_InternalSyncObject;
			}
		}

		// Token: 0x06001DB1 RID: 7601 RVA: 0x0008C8F0 File Offset: 0x0008AAF0
		internal static SettingsSectionInternal GetSection()
		{
			return new SettingsSectionInternal((SettingsSection)PrivilegedConfigurationManager.GetSection(ConfigurationStrings.SettingsSectionPath));
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06001DB2 RID: 7602 RVA: 0x0008C906 File Offset: 0x0008AB06
		internal bool AlwaysUseCompletionPortsForAccept
		{
			get
			{
				return this.alwaysUseCompletionPortsForAccept;
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06001DB3 RID: 7603 RVA: 0x0008C90E File Offset: 0x0008AB0E
		internal bool AlwaysUseCompletionPortsForConnect
		{
			get
			{
				return this.alwaysUseCompletionPortsForConnect;
			}
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06001DB4 RID: 7604 RVA: 0x0008C916 File Offset: 0x0008AB16
		internal int AutoConfigUrlRetryInterval
		{
			get
			{
				return this.autoConfigUrlRetryInterval;
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06001DB5 RID: 7605 RVA: 0x0008C91E File Offset: 0x0008AB1E
		internal bool CheckCertificateName
		{
			get
			{
				return this.checkCertificateName;
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06001DB6 RID: 7606 RVA: 0x0008C926 File Offset: 0x0008AB26
		// (set) Token: 0x06001DB7 RID: 7607 RVA: 0x0008C92E File Offset: 0x0008AB2E
		internal bool CheckCertificateRevocationList
		{
			get
			{
				return this.checkCertificateRevocationList;
			}
			set
			{
				this.checkCertificateRevocationList = value;
			}
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06001DB8 RID: 7608 RVA: 0x0008C937 File Offset: 0x0008AB37
		// (set) Token: 0x06001DB9 RID: 7609 RVA: 0x0008C93F File Offset: 0x0008AB3F
		internal int DefaultCredentialsHandleCacheSize
		{
			get
			{
				return this.defaultCredentialsHandleCacheSize;
			}
			set
			{
				this.defaultCredentialsHandleCacheSize = value;
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06001DBA RID: 7610 RVA: 0x0008C948 File Offset: 0x0008AB48
		// (set) Token: 0x06001DBB RID: 7611 RVA: 0x0008C950 File Offset: 0x0008AB50
		internal int DnsRefreshTimeout
		{
			get
			{
				return this.dnsRefreshTimeout;
			}
			set
			{
				this.dnsRefreshTimeout = value;
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06001DBC RID: 7612 RVA: 0x0008C959 File Offset: 0x0008AB59
		internal int DownloadTimeout
		{
			get
			{
				return this.downloadTimeout;
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06001DBD RID: 7613 RVA: 0x0008C961 File Offset: 0x0008AB61
		// (set) Token: 0x06001DBE RID: 7614 RVA: 0x0008C969 File Offset: 0x0008AB69
		internal bool EnableDnsRoundRobin
		{
			get
			{
				return this.enableDnsRoundRobin;
			}
			set
			{
				this.enableDnsRoundRobin = value;
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06001DBF RID: 7615 RVA: 0x0008C972 File Offset: 0x0008AB72
		internal EncryptionPolicy EncryptionPolicy
		{
			get
			{
				return this.encryptionPolicy;
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06001DC0 RID: 7616 RVA: 0x0008C97A File Offset: 0x0008AB7A
		// (set) Token: 0x06001DC1 RID: 7617 RVA: 0x0008C982 File Offset: 0x0008AB82
		internal bool Expect100Continue
		{
			get
			{
				return this.expect100Continue;
			}
			set
			{
				this.expect100Continue = value;
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06001DC2 RID: 7618 RVA: 0x0008C98B File Offset: 0x0008AB8B
		internal IPProtectionLevel IPProtectionLevel
		{
			get
			{
				return this.ipProtectionLevel;
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06001DC3 RID: 7619 RVA: 0x0008C993 File Offset: 0x0008AB93
		internal bool Ipv6Enabled
		{
			get
			{
				return this.ipv6Enabled;
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06001DC4 RID: 7620 RVA: 0x0008C99B File Offset: 0x0008AB9B
		// (set) Token: 0x06001DC5 RID: 7621 RVA: 0x0008C9A3 File Offset: 0x0008ABA3
		internal int MaximumResponseHeadersLength
		{
			get
			{
				return this.maximumResponseHeadersLength;
			}
			set
			{
				this.maximumResponseHeadersLength = value;
			}
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06001DC6 RID: 7622 RVA: 0x0008C9AC File Offset: 0x0008ABAC
		internal int MaximumUnauthorizedUploadLength
		{
			get
			{
				return this.maximumUnauthorizedUploadLength;
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06001DC7 RID: 7623 RVA: 0x0008C9B4 File Offset: 0x0008ABB4
		// (set) Token: 0x06001DC8 RID: 7624 RVA: 0x0008C9BC File Offset: 0x0008ABBC
		internal int MaximumErrorResponseLength
		{
			get
			{
				return this.maximumErrorResponseLength;
			}
			set
			{
				this.maximumErrorResponseLength = value;
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06001DC9 RID: 7625 RVA: 0x0008C9C5 File Offset: 0x0008ABC5
		internal bool UseUnsafeHeaderParsing
		{
			get
			{
				return this.useUnsafeHeaderParsing;
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06001DCA RID: 7626 RVA: 0x0008C9CD File Offset: 0x0008ABCD
		// (set) Token: 0x06001DCB RID: 7627 RVA: 0x0008C9D5 File Offset: 0x0008ABD5
		internal bool UseNagleAlgorithm
		{
			get
			{
				return this.useNagleAlgorithm;
			}
			set
			{
				this.useNagleAlgorithm = value;
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06001DCC RID: 7628 RVA: 0x0008C9DE File Offset: 0x0008ABDE
		internal bool PerformanceCountersEnabled
		{
			get
			{
				return this.performanceCountersEnabled;
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06001DCD RID: 7629 RVA: 0x0008C9E6 File Offset: 0x0008ABE6
		internal bool HttpListenerUnescapeRequestUrl
		{
			get
			{
				return this.httpListenerUnescapeRequestUrl;
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06001DCE RID: 7630 RVA: 0x0008C9EE File Offset: 0x0008ABEE
		internal long[] HttpListenerTimeouts
		{
			get
			{
				return this.httpListenerTimeouts;
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06001DCF RID: 7631 RVA: 0x0008C9F6 File Offset: 0x0008ABF6
		// (set) Token: 0x06001DD0 RID: 7632 RVA: 0x0008C9FE File Offset: 0x0008ABFE
		internal UnicodeDecodingConformance WebUtilityUnicodeDecodingConformance { get; private set; }

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06001DD1 RID: 7633 RVA: 0x0008CA07 File Offset: 0x0008AC07
		// (set) Token: 0x06001DD2 RID: 7634 RVA: 0x0008CA0F File Offset: 0x0008AC0F
		internal UnicodeEncodingConformance WebUtilityUnicodeEncodingConformance { get; private set; }

		// Token: 0x04001C5D RID: 7261
		private static object s_InternalSyncObject;

		// Token: 0x04001C60 RID: 7264
		private static volatile SettingsSectionInternal s_settings;

		// Token: 0x04001C61 RID: 7265
		private bool alwaysUseCompletionPortsForAccept;

		// Token: 0x04001C62 RID: 7266
		private bool alwaysUseCompletionPortsForConnect;

		// Token: 0x04001C63 RID: 7267
		private bool checkCertificateName;

		// Token: 0x04001C64 RID: 7268
		private bool checkCertificateRevocationList;

		// Token: 0x04001C65 RID: 7269
		private int defaultCredentialsHandleCacheSize;

		// Token: 0x04001C66 RID: 7270
		private int autoConfigUrlRetryInterval;

		// Token: 0x04001C67 RID: 7271
		private int downloadTimeout;

		// Token: 0x04001C68 RID: 7272
		private int dnsRefreshTimeout;

		// Token: 0x04001C69 RID: 7273
		private bool enableDnsRoundRobin;

		// Token: 0x04001C6A RID: 7274
		private EncryptionPolicy encryptionPolicy;

		// Token: 0x04001C6B RID: 7275
		private bool expect100Continue;

		// Token: 0x04001C6C RID: 7276
		private IPProtectionLevel ipProtectionLevel;

		// Token: 0x04001C6D RID: 7277
		private bool ipv6Enabled;

		// Token: 0x04001C6E RID: 7278
		private int maximumResponseHeadersLength;

		// Token: 0x04001C6F RID: 7279
		private int maximumErrorResponseLength;

		// Token: 0x04001C70 RID: 7280
		private int maximumUnauthorizedUploadLength;

		// Token: 0x04001C71 RID: 7281
		private bool useUnsafeHeaderParsing;

		// Token: 0x04001C72 RID: 7282
		private bool useNagleAlgorithm;

		// Token: 0x04001C73 RID: 7283
		private bool performanceCountersEnabled;

		// Token: 0x04001C74 RID: 7284
		private bool httpListenerUnescapeRequestUrl;

		// Token: 0x04001C75 RID: 7285
		private long[] httpListenerTimeouts;
	}
}
