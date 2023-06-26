using System;
using System.Configuration;
using System.Net.Cache;
using System.Xml;

namespace System.Net.Configuration
{
	/// <summary>Represents the default FTP cache policy for network resources. This class cannot be inherited.</summary>
	// Token: 0x02000335 RID: 821
	public sealed class FtpCachePolicyElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.FtpCachePolicyElement" /> class.</summary>
		// Token: 0x06001D5A RID: 7514 RVA: 0x0008B9CC File Offset: 0x00089BCC
		public FtpCachePolicyElement()
		{
			this.properties.Add(this.policyLevel);
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06001D5B RID: 7515 RVA: 0x0008BA1C File Offset: 0x00089C1C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		/// <summary>Gets or sets FTP caching behavior for the local machine.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.RequestCacheLevel" /> value that specifies the cache behavior.</returns>
		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06001D5C RID: 7516 RVA: 0x0008BA24 File Offset: 0x00089C24
		// (set) Token: 0x06001D5D RID: 7517 RVA: 0x0008BA37 File Offset: 0x00089C37
		[ConfigurationProperty("policyLevel", DefaultValue = RequestCacheLevel.Default)]
		public RequestCacheLevel PolicyLevel
		{
			get
			{
				return (RequestCacheLevel)base[this.policyLevel];
			}
			set
			{
				base[this.policyLevel] = value;
			}
		}

		// Token: 0x06001D5E RID: 7518 RVA: 0x0008BA4B File Offset: 0x00089C4B
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			this.wasReadFromConfig = true;
			base.DeserializeElement(reader, serializeCollectionKey);
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x0008BA5C File Offset: 0x00089C5C
		protected override void Reset(ConfigurationElement parentElement)
		{
			if (parentElement != null)
			{
				FtpCachePolicyElement ftpCachePolicyElement = (FtpCachePolicyElement)parentElement;
				this.wasReadFromConfig = ftpCachePolicyElement.wasReadFromConfig;
			}
			base.Reset(parentElement);
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06001D60 RID: 7520 RVA: 0x0008BA86 File Offset: 0x00089C86
		internal bool WasReadFromConfig
		{
			get
			{
				return this.wasReadFromConfig;
			}
		}

		// Token: 0x04001C32 RID: 7218
		private bool wasReadFromConfig;

		// Token: 0x04001C33 RID: 7219
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001C34 RID: 7220
		private readonly ConfigurationProperty policyLevel = new ConfigurationProperty("policyLevel", typeof(RequestCacheLevel), RequestCacheLevel.Default, ConfigurationPropertyOptions.None);
	}
}
