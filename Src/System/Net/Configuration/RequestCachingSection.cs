using System;
using System.Configuration;
using System.Net.Cache;
using System.Xml;

namespace System.Net.Configuration
{
	/// <summary>Represents the configuration section for cache behavior. This class cannot be inherited.</summary>
	// Token: 0x0200033D RID: 829
	public sealed class RequestCachingSection : ConfigurationSection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.RequestCachingSection" /> class.</summary>
		// Token: 0x06001D87 RID: 7559 RVA: 0x0008BF14 File Offset: 0x0008A114
		public RequestCachingSection()
		{
			this.properties.Add(this.disableAllCaching);
			this.properties.Add(this.defaultPolicyLevel);
			this.properties.Add(this.isPrivateCache);
			this.properties.Add(this.defaultHttpCachePolicy);
			this.properties.Add(this.defaultFtpCachePolicy);
			this.properties.Add(this.unspecifiedMaximumAge);
		}

		/// <summary>Gets the default caching behavior for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.HttpCachePolicyElement" /> that defines the default cache policy.</returns>
		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06001D88 RID: 7560 RVA: 0x0008C061 File Offset: 0x0008A261
		[ConfigurationProperty("defaultHttpCachePolicy")]
		public HttpCachePolicyElement DefaultHttpCachePolicy
		{
			get
			{
				return (HttpCachePolicyElement)base[this.defaultHttpCachePolicy];
			}
		}

		/// <summary>Gets the default FTP caching behavior for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.FtpCachePolicyElement" /> that defines the default cache policy.</returns>
		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06001D89 RID: 7561 RVA: 0x0008C074 File Offset: 0x0008A274
		[ConfigurationProperty("defaultFtpCachePolicy")]
		public FtpCachePolicyElement DefaultFtpCachePolicy
		{
			get
			{
				return (FtpCachePolicyElement)base[this.defaultFtpCachePolicy];
			}
		}

		/// <summary>Gets or sets the default cache policy level.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.RequestCacheLevel" /> enumeration value.</returns>
		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06001D8A RID: 7562 RVA: 0x0008C087 File Offset: 0x0008A287
		// (set) Token: 0x06001D8B RID: 7563 RVA: 0x0008C09A File Offset: 0x0008A29A
		[ConfigurationProperty("defaultPolicyLevel", DefaultValue = RequestCacheLevel.BypassCache)]
		public RequestCacheLevel DefaultPolicyLevel
		{
			get
			{
				return (RequestCacheLevel)base[this.defaultPolicyLevel];
			}
			set
			{
				base[this.defaultPolicyLevel] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that enables caching on the local computer.</summary>
		/// <returns>
		///   <see langword="true" /> if caching is disabled on the local computer; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06001D8C RID: 7564 RVA: 0x0008C0AE File Offset: 0x0008A2AE
		// (set) Token: 0x06001D8D RID: 7565 RVA: 0x0008C0C1 File Offset: 0x0008A2C1
		[ConfigurationProperty("disableAllCaching", DefaultValue = false)]
		public bool DisableAllCaching
		{
			get
			{
				return (bool)base[this.disableAllCaching];
			}
			set
			{
				base[this.disableAllCaching] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that indicates whether the local computer cache is private.</summary>
		/// <returns>
		///   <see langword="true" /> if the cache provides user isolation; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06001D8E RID: 7566 RVA: 0x0008C0D5 File Offset: 0x0008A2D5
		// (set) Token: 0x06001D8F RID: 7567 RVA: 0x0008C0E8 File Offset: 0x0008A2E8
		[ConfigurationProperty("isPrivateCache", DefaultValue = true)]
		public bool IsPrivateCache
		{
			get
			{
				return (bool)base[this.isPrivateCache];
			}
			set
			{
				base[this.isPrivateCache] = value;
			}
		}

		/// <summary>Gets or sets a value used as the maximum age for cached resources that do not have expiration information.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that provides a default maximum age for cached resources.</returns>
		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06001D90 RID: 7568 RVA: 0x0008C0FC File Offset: 0x0008A2FC
		// (set) Token: 0x06001D91 RID: 7569 RVA: 0x0008C10F File Offset: 0x0008A30F
		[ConfigurationProperty("unspecifiedMaximumAge", DefaultValue = "1.00:00:00")]
		public TimeSpan UnspecifiedMaximumAge
		{
			get
			{
				return (TimeSpan)base[this.unspecifiedMaximumAge];
			}
			set
			{
				base[this.unspecifiedMaximumAge] = value;
			}
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x0008C124 File Offset: 0x0008A324
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			bool flag = this.DisableAllCaching;
			base.DeserializeElement(reader, serializeCollectionKey);
			if (flag)
			{
				this.DisableAllCaching = true;
			}
		}

		// Token: 0x06001D93 RID: 7571 RVA: 0x0008C14C File Offset: 0x0008A34C
		protected override void PostDeserialize()
		{
			if (base.EvaluationContext.IsMachineLevel)
			{
				return;
			}
			try
			{
				ExceptionHelper.WebPermissionUnrestricted.Demand();
			}
			catch (Exception ex)
			{
				throw new ConfigurationErrorsException(SR.GetString("net_config_section_permission", new object[] { "requestCaching" }), ex);
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06001D94 RID: 7572 RVA: 0x0008C1A4 File Offset: 0x0008A3A4
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04001C42 RID: 7234
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001C43 RID: 7235
		private readonly ConfigurationProperty defaultHttpCachePolicy = new ConfigurationProperty("defaultHttpCachePolicy", typeof(HttpCachePolicyElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C44 RID: 7236
		private readonly ConfigurationProperty defaultFtpCachePolicy = new ConfigurationProperty("defaultFtpCachePolicy", typeof(FtpCachePolicyElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C45 RID: 7237
		private readonly ConfigurationProperty defaultPolicyLevel = new ConfigurationProperty("defaultPolicyLevel", typeof(RequestCacheLevel), RequestCacheLevel.BypassCache, ConfigurationPropertyOptions.None);

		// Token: 0x04001C46 RID: 7238
		private readonly ConfigurationProperty disableAllCaching = new ConfigurationProperty("disableAllCaching", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04001C47 RID: 7239
		private readonly ConfigurationProperty isPrivateCache = new ConfigurationProperty("isPrivateCache", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04001C48 RID: 7240
		private readonly ConfigurationProperty unspecifiedMaximumAge = new ConfigurationProperty("unspecifiedMaximumAge", typeof(TimeSpan), TimeSpan.FromDays(1.0), ConfigurationPropertyOptions.None);
	}
}
