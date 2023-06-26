using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Identifies the configuration settings for Web proxy server. This class cannot be inherited.</summary>
	// Token: 0x0200033C RID: 828
	public sealed class ProxyElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.ProxyElement" /> class.</summary>
		// Token: 0x06001D7B RID: 7547 RVA: 0x0008BD04 File Offset: 0x00089F04
		public ProxyElement()
		{
			this.properties.Add(this.autoDetect);
			this.properties.Add(this.scriptLocation);
			this.properties.Add(this.bypassonlocal);
			this.properties.Add(this.proxyaddress);
			this.properties.Add(this.usesystemdefault);
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06001D7C RID: 7548 RVA: 0x0008BE50 File Offset: 0x0008A050
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		/// <summary>Gets or sets an <see cref="T:System.Net.Configuration.ProxyElement.AutoDetectValues" /> value that controls whether the Web proxy is automatically detected.</summary>
		/// <returns>
		///   <see cref="F:System.Net.Configuration.ProxyElement.AutoDetectValues.True" /> if the <see cref="T:System.Net.WebProxy" /> is automatically detected; <see cref="F:System.Net.Configuration.ProxyElement.AutoDetectValues.False" /> if the <see cref="T:System.Net.WebProxy" /> is not automatically detected; or <see cref="F:System.Net.Configuration.ProxyElement.AutoDetectValues.Unspecified" />.</returns>
		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06001D7D RID: 7549 RVA: 0x0008BE58 File Offset: 0x0008A058
		// (set) Token: 0x06001D7E RID: 7550 RVA: 0x0008BE6B File Offset: 0x0008A06B
		[ConfigurationProperty("autoDetect", DefaultValue = ProxyElement.AutoDetectValues.Unspecified)]
		public ProxyElement.AutoDetectValues AutoDetect
		{
			get
			{
				return (ProxyElement.AutoDetectValues)base[this.autoDetect];
			}
			set
			{
				base[this.autoDetect] = value;
			}
		}

		/// <summary>Gets or sets an <see cref="T:System.Uri" /> value that specifies the location of the automatic proxy detection script.</summary>
		/// <returns>A <see cref="T:System.Uri" /> specifying the location of the automatic proxy detection script.</returns>
		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06001D7F RID: 7551 RVA: 0x0008BE7F File Offset: 0x0008A07F
		// (set) Token: 0x06001D80 RID: 7552 RVA: 0x0008BE92 File Offset: 0x0008A092
		[ConfigurationProperty("scriptLocation")]
		public Uri ScriptLocation
		{
			get
			{
				return (Uri)base[this.scriptLocation];
			}
			set
			{
				base[this.scriptLocation] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether local resources are retrieved by using a Web proxy server.</summary>
		/// <returns>A value that indicates whether local resources are retrieved by using a Web proxy server.</returns>
		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06001D81 RID: 7553 RVA: 0x0008BEA1 File Offset: 0x0008A0A1
		// (set) Token: 0x06001D82 RID: 7554 RVA: 0x0008BEB4 File Offset: 0x0008A0B4
		[ConfigurationProperty("bypassonlocal", DefaultValue = ProxyElement.BypassOnLocalValues.Unspecified)]
		public ProxyElement.BypassOnLocalValues BypassOnLocal
		{
			get
			{
				return (ProxyElement.BypassOnLocalValues)base[this.bypassonlocal];
			}
			set
			{
				base[this.bypassonlocal] = value;
			}
		}

		/// <summary>Gets or sets the URI that identifies the Web proxy server to use.</summary>
		/// <returns>The URI that identifies the Web proxy server to use.</returns>
		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06001D83 RID: 7555 RVA: 0x0008BEC8 File Offset: 0x0008A0C8
		// (set) Token: 0x06001D84 RID: 7556 RVA: 0x0008BEDB File Offset: 0x0008A0DB
		[ConfigurationProperty("proxyaddress")]
		public Uri ProxyAddress
		{
			get
			{
				return (Uri)base[this.proxyaddress];
			}
			set
			{
				base[this.proxyaddress] = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that controls whether the Internet Explorer Web proxy settings are used.</summary>
		/// <returns>
		///   <see langword="true" /> if the Internet Explorer LAN settings are used to detect and configure the default <see cref="T:System.Net.WebProxy" /> used for requests; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06001D85 RID: 7557 RVA: 0x0008BEEA File Offset: 0x0008A0EA
		// (set) Token: 0x06001D86 RID: 7558 RVA: 0x0008BEFD File Offset: 0x0008A0FD
		[ConfigurationProperty("usesystemdefault", DefaultValue = ProxyElement.UseSystemDefaultValues.Unspecified)]
		public ProxyElement.UseSystemDefaultValues UseSystemDefault
		{
			get
			{
				return (ProxyElement.UseSystemDefaultValues)base[this.usesystemdefault];
			}
			set
			{
				base[this.usesystemdefault] = value;
			}
		}

		// Token: 0x04001C3C RID: 7228
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001C3D RID: 7229
		private readonly ConfigurationProperty autoDetect = new ConfigurationProperty("autoDetect", typeof(ProxyElement.AutoDetectValues), ProxyElement.AutoDetectValues.Unspecified, new EnumConverter(typeof(ProxyElement.AutoDetectValues)), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C3E RID: 7230
		private readonly ConfigurationProperty scriptLocation = new ConfigurationProperty("scriptLocation", typeof(Uri), null, new UriTypeConverter(UriKind.Absolute), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C3F RID: 7231
		private readonly ConfigurationProperty bypassonlocal = new ConfigurationProperty("bypassonlocal", typeof(ProxyElement.BypassOnLocalValues), ProxyElement.BypassOnLocalValues.Unspecified, new EnumConverter(typeof(ProxyElement.BypassOnLocalValues)), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C40 RID: 7232
		private readonly ConfigurationProperty proxyaddress = new ConfigurationProperty("proxyaddress", typeof(Uri), null, new UriTypeConverter(UriKind.Absolute), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C41 RID: 7233
		private readonly ConfigurationProperty usesystemdefault = new ConfigurationProperty("usesystemdefault", typeof(ProxyElement.UseSystemDefaultValues), ProxyElement.UseSystemDefaultValues.Unspecified, new EnumConverter(typeof(ProxyElement.UseSystemDefaultValues)), null, ConfigurationPropertyOptions.None);

		/// <summary>Specifies whether the proxy is bypassed for local resources.</summary>
		// Token: 0x020007C1 RID: 1985
		public enum BypassOnLocalValues
		{
			/// <summary>Unspecified.</summary>
			// Token: 0x04003454 RID: 13396
			Unspecified = -1,
			/// <summary>All requests for local resources should go through the proxy</summary>
			// Token: 0x04003455 RID: 13397
			False,
			/// <summary>Access local resources directly.</summary>
			// Token: 0x04003456 RID: 13398
			True
		}

		/// <summary>Specifies whether to use the local system proxy settings to determine whether the proxy is bypassed for local resources.</summary>
		// Token: 0x020007C2 RID: 1986
		public enum UseSystemDefaultValues
		{
			/// <summary>The system default proxy setting is unspecified.</summary>
			// Token: 0x04003458 RID: 13400
			Unspecified = -1,
			/// <summary>Do not use system default proxy setting values</summary>
			// Token: 0x04003459 RID: 13401
			False,
			/// <summary>Use system default proxy setting values.</summary>
			// Token: 0x0400345A RID: 13402
			True
		}

		/// <summary>Specifies whether the proxy is automatically detected.</summary>
		// Token: 0x020007C3 RID: 1987
		public enum AutoDetectValues
		{
			/// <summary>Unspecified.</summary>
			// Token: 0x0400345C RID: 13404
			Unspecified = -1,
			/// <summary>The proxy is not automatically detected.</summary>
			// Token: 0x0400345D RID: 13405
			False,
			/// <summary>The proxy is automatically detected.</summary>
			// Token: 0x0400345E RID: 13406
			True
		}
	}
}
