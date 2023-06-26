using System;
using System.Configuration;
using System.Net.Cache;

namespace System.Net.Configuration
{
	/// <summary>Represents the configuration section for sockets, IPv6, response headers, and service points. This class cannot be inherited.</summary>
	// Token: 0x0200033F RID: 831
	public sealed class SettingsSection : ConfigurationSection
	{
		// Token: 0x06001DA2 RID: 7586 RVA: 0x0008C3CC File Offset: 0x0008A5CC
		internal static void EnsureConfigLoaded()
		{
			try
			{
				AuthenticationManager.EnsureConfigLoaded();
				object obj = RequestCacheManager.IsCachingEnabled;
				obj = System.Net.ServicePointManager.DefaultConnectionLimit;
				obj = System.Net.ServicePointManager.Expect100Continue;
				obj = WebRequest.PrefixList;
				obj = WebRequest.InternalDefaultWebProxy;
			}
			catch
			{
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.ConnectionManagementSection" /> class.</summary>
		// Token: 0x06001DA3 RID: 7587 RVA: 0x0008C420 File Offset: 0x0008A620
		public SettingsSection()
		{
			this.properties.Add(this.httpWebRequest);
			this.properties.Add(this.ipv6);
			this.properties.Add(this.servicePointManager);
			this.properties.Add(this.socket);
			this.properties.Add(this.webProxyScript);
			this.properties.Add(this.performanceCounters);
			this.properties.Add(this.httpListener);
			this.properties.Add(this.webUtility);
			this.properties.Add(this.windowsAuthentication);
		}

		/// <summary>Gets the configuration element that controls the settings used by an <see cref="T:System.Net.HttpWebRequest" /> object.</summary>
		/// <returns>The configuration element that controls the maximum response header length and other settings used by an <see cref="T:System.Net.HttpWebRequest" /> object.</returns>
		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06001DA4 RID: 7588 RVA: 0x0008C5D3 File Offset: 0x0008A7D3
		[ConfigurationProperty("httpWebRequest")]
		public HttpWebRequestElement HttpWebRequest
		{
			get
			{
				return (HttpWebRequestElement)base[this.httpWebRequest];
			}
		}

		/// <summary>Gets the configuration element that enables Internet Protocol version 6 (IPv6).</summary>
		/// <returns>The configuration element that controls the setting used by IPv6.</returns>
		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06001DA5 RID: 7589 RVA: 0x0008C5E6 File Offset: 0x0008A7E6
		[ConfigurationProperty("ipv6")]
		public Ipv6Element Ipv6
		{
			get
			{
				return (Ipv6Element)base[this.ipv6];
			}
		}

		/// <summary>Gets the configuration element that controls settings for connections to remote host computers.</summary>
		/// <returns>The configuration element that controls settings for connections to remote host computers.</returns>
		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06001DA6 RID: 7590 RVA: 0x0008C5F9 File Offset: 0x0008A7F9
		[ConfigurationProperty("servicePointManager")]
		public ServicePointManagerElement ServicePointManager
		{
			get
			{
				return (ServicePointManagerElement)base[this.servicePointManager];
			}
		}

		/// <summary>Gets the configuration element that controls settings for sockets.</summary>
		/// <returns>The configuration element that controls settings for sockets.</returns>
		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06001DA7 RID: 7591 RVA: 0x0008C60C File Offset: 0x0008A80C
		[ConfigurationProperty("socket")]
		public SocketElement Socket
		{
			get
			{
				return (SocketElement)base[this.socket];
			}
		}

		/// <summary>Gets the configuration element that controls the execution timeout and download timeout of Web proxy scripts.</summary>
		/// <returns>The configuration element that controls settings for the execution timeout and download timeout used by the Web proxy scripts.</returns>
		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06001DA8 RID: 7592 RVA: 0x0008C61F File Offset: 0x0008A81F
		[ConfigurationProperty("webProxyScript")]
		public WebProxyScriptElement WebProxyScript
		{
			get
			{
				return (WebProxyScriptElement)base[this.webProxyScript];
			}
		}

		/// <summary>Gets the configuration element that controls whether network performance counters are enabled.</summary>
		/// <returns>The configuration element that controls usage of network performance counters.</returns>
		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06001DA9 RID: 7593 RVA: 0x0008C632 File Offset: 0x0008A832
		[ConfigurationProperty("performanceCounters")]
		public PerformanceCountersElement PerformanceCounters
		{
			get
			{
				return (PerformanceCountersElement)base[this.performanceCounters];
			}
		}

		/// <summary>Gets the configuration element that controls the settings used by an <see cref="T:System.Net.HttpListener" /> object.</summary>
		/// <returns>An <see cref="T:System.Net.Configuration.HttpListenerElement" /> object.  
		///  The configuration element that controls the settings used by an <see cref="T:System.Net.HttpListener" /> object.</returns>
		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06001DAA RID: 7594 RVA: 0x0008C645 File Offset: 0x0008A845
		[ConfigurationProperty("httpListener")]
		public HttpListenerElement HttpListener
		{
			get
			{
				return (HttpListenerElement)base[this.httpListener];
			}
		}

		/// <summary>Gets the configuration element that controls the settings used by an <see cref="T:System.Net.WebUtility" /> object.</summary>
		/// <returns>The configuration element that controls the settings used by a <see cref="T:System.Net.WebUtility" /> object.</returns>
		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06001DAB RID: 7595 RVA: 0x0008C658 File Offset: 0x0008A858
		[ConfigurationProperty("webUtility")]
		public WebUtilityElement WebUtility
		{
			get
			{
				return (WebUtilityElement)base[this.webUtility];
			}
		}

		/// <summary>Gets the configuration element that controls the number of handles for default network credentials.</summary>
		/// <returns>The configuration element that controls the number of handles for default network credentials.</returns>
		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06001DAC RID: 7596 RVA: 0x0008C66B File Offset: 0x0008A86B
		[ConfigurationProperty("windowsAuthentication")]
		public WindowsAuthenticationElement WindowsAuthentication
		{
			get
			{
				return (WindowsAuthenticationElement)base[this.windowsAuthentication];
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06001DAD RID: 7597 RVA: 0x0008C67E File Offset: 0x0008A87E
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04001C53 RID: 7251
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001C54 RID: 7252
		private readonly ConfigurationProperty httpWebRequest = new ConfigurationProperty("httpWebRequest", typeof(HttpWebRequestElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C55 RID: 7253
		private readonly ConfigurationProperty ipv6 = new ConfigurationProperty("ipv6", typeof(Ipv6Element), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C56 RID: 7254
		private readonly ConfigurationProperty servicePointManager = new ConfigurationProperty("servicePointManager", typeof(ServicePointManagerElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C57 RID: 7255
		private readonly ConfigurationProperty socket = new ConfigurationProperty("socket", typeof(SocketElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C58 RID: 7256
		private readonly ConfigurationProperty webProxyScript = new ConfigurationProperty("webProxyScript", typeof(WebProxyScriptElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C59 RID: 7257
		private readonly ConfigurationProperty performanceCounters = new ConfigurationProperty("performanceCounters", typeof(PerformanceCountersElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C5A RID: 7258
		private readonly ConfigurationProperty httpListener = new ConfigurationProperty("httpListener", typeof(HttpListenerElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C5B RID: 7259
		private readonly ConfigurationProperty webUtility = new ConfigurationProperty("webUtility", typeof(WebUtilityElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C5C RID: 7260
		private readonly ConfigurationProperty windowsAuthentication = new ConfigurationProperty("windowsAuthentication", typeof(WindowsAuthenticationElement), null, ConfigurationPropertyOptions.None);
	}
}
