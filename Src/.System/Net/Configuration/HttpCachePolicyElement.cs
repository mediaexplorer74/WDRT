using System;
using System.Configuration;
using System.Net.Cache;
using System.Xml;

namespace System.Net.Configuration
{
	/// <summary>Represents the default HTTP cache policy for network resources. This class cannot be inherited.</summary>
	// Token: 0x02000334 RID: 820
	public sealed class HttpCachePolicyElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.HttpCachePolicyElement" /> class.</summary>
		// Token: 0x06001D4D RID: 7501 RVA: 0x0008B7F0 File Offset: 0x000899F0
		public HttpCachePolicyElement()
		{
			this.properties.Add(this.maximumAge);
			this.properties.Add(this.maximumStale);
			this.properties.Add(this.minimumFresh);
			this.properties.Add(this.policyLevel);
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06001D4E RID: 7502 RVA: 0x0008B8E2 File Offset: 0x00089AE2
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		/// <summary>Gets or sets the maximum age permitted for a resource returned from the cache.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that specifies the maximum age for cached resources specified in the configuration file.</returns>
		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06001D4F RID: 7503 RVA: 0x0008B8EA File Offset: 0x00089AEA
		// (set) Token: 0x06001D50 RID: 7504 RVA: 0x0008B8FD File Offset: 0x00089AFD
		[ConfigurationProperty("maximumAge", DefaultValue = "10675199.02:48:05.4775807")]
		public TimeSpan MaximumAge
		{
			get
			{
				return (TimeSpan)base[this.maximumAge];
			}
			set
			{
				base[this.maximumAge] = value;
			}
		}

		/// <summary>Gets or sets the maximum staleness value permitted for a resource returned from the cache.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that is set to the maximum staleness value specified in the configuration file.</returns>
		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06001D51 RID: 7505 RVA: 0x0008B911 File Offset: 0x00089B11
		// (set) Token: 0x06001D52 RID: 7506 RVA: 0x0008B924 File Offset: 0x00089B24
		[ConfigurationProperty("maximumStale", DefaultValue = "-10675199.02:48:05.4775808")]
		public TimeSpan MaximumStale
		{
			get
			{
				return (TimeSpan)base[this.maximumStale];
			}
			set
			{
				base[this.maximumStale] = value;
			}
		}

		/// <summary>Gets or sets the minimum freshness permitted for a resource returned from the cache.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that specifies the minimum freshness specified in the configuration file.</returns>
		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06001D53 RID: 7507 RVA: 0x0008B938 File Offset: 0x00089B38
		// (set) Token: 0x06001D54 RID: 7508 RVA: 0x0008B94B File Offset: 0x00089B4B
		[ConfigurationProperty("minimumFresh", DefaultValue = "-10675199.02:48:05.4775808")]
		public TimeSpan MinimumFresh
		{
			get
			{
				return (TimeSpan)base[this.minimumFresh];
			}
			set
			{
				base[this.minimumFresh] = value;
			}
		}

		/// <summary>Gets or sets HTTP caching behavior for the local machine.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.HttpRequestCacheLevel" /> value that specifies the cache behavior.</returns>
		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06001D55 RID: 7509 RVA: 0x0008B95F File Offset: 0x00089B5F
		// (set) Token: 0x06001D56 RID: 7510 RVA: 0x0008B972 File Offset: 0x00089B72
		[ConfigurationProperty("policyLevel", IsRequired = true, DefaultValue = HttpRequestCacheLevel.Default)]
		public HttpRequestCacheLevel PolicyLevel
		{
			get
			{
				return (HttpRequestCacheLevel)base[this.policyLevel];
			}
			set
			{
				base[this.policyLevel] = value;
			}
		}

		// Token: 0x06001D57 RID: 7511 RVA: 0x0008B986 File Offset: 0x00089B86
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			this.wasReadFromConfig = true;
			base.DeserializeElement(reader, serializeCollectionKey);
		}

		// Token: 0x06001D58 RID: 7512 RVA: 0x0008B998 File Offset: 0x00089B98
		protected override void Reset(ConfigurationElement parentElement)
		{
			if (parentElement != null)
			{
				HttpCachePolicyElement httpCachePolicyElement = (HttpCachePolicyElement)parentElement;
				this.wasReadFromConfig = httpCachePolicyElement.wasReadFromConfig;
			}
			base.Reset(parentElement);
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06001D59 RID: 7513 RVA: 0x0008B9C2 File Offset: 0x00089BC2
		internal bool WasReadFromConfig
		{
			get
			{
				return this.wasReadFromConfig;
			}
		}

		// Token: 0x04001C2C RID: 7212
		private bool wasReadFromConfig;

		// Token: 0x04001C2D RID: 7213
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001C2E RID: 7214
		private readonly ConfigurationProperty maximumAge = new ConfigurationProperty("maximumAge", typeof(TimeSpan), TimeSpan.MaxValue, ConfigurationPropertyOptions.None);

		// Token: 0x04001C2F RID: 7215
		private readonly ConfigurationProperty maximumStale = new ConfigurationProperty("maximumStale", typeof(TimeSpan), TimeSpan.MinValue, ConfigurationPropertyOptions.None);

		// Token: 0x04001C30 RID: 7216
		private readonly ConfigurationProperty minimumFresh = new ConfigurationProperty("minimumFresh", typeof(TimeSpan), TimeSpan.MinValue, ConfigurationPropertyOptions.None);

		// Token: 0x04001C31 RID: 7217
		private readonly ConfigurationProperty policyLevel = new ConfigurationProperty("policyLevel", typeof(HttpRequestCacheLevel), HttpRequestCacheLevel.Default, ConfigurationPropertyOptions.None);
	}
}
