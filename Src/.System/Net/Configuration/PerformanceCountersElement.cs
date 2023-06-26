using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the performance counter element in the <see langword="System.Net" /> configuration file that determines whether networking performance counters are enabled. This class cannot be inherited.</summary>
	// Token: 0x0200033B RID: 827
	public sealed class PerformanceCountersElement : ConfigurationElement
	{
		/// <summary>Instantiates a <see cref="T:System.Net.Configuration.PerformanceCountersElement" /> object.</summary>
		// Token: 0x06001D77 RID: 7543 RVA: 0x0008BC84 File Offset: 0x00089E84
		public PerformanceCountersElement()
		{
			this.properties.Add(this.enabled);
		}

		/// <summary>Gets or sets whether performance counters are enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if performance counters are enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06001D78 RID: 7544 RVA: 0x0008BCD4 File Offset: 0x00089ED4
		// (set) Token: 0x06001D79 RID: 7545 RVA: 0x0008BCE7 File Offset: 0x00089EE7
		[ConfigurationProperty("enabled", DefaultValue = false)]
		public bool Enabled
		{
			get
			{
				return (bool)base[this.enabled];
			}
			set
			{
				base[this.enabled] = value;
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06001D7A RID: 7546 RVA: 0x0008BCFB File Offset: 0x00089EFB
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04001C3A RID: 7226
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001C3B RID: 7227
		private readonly ConfigurationProperty enabled = new ConfigurationProperty("enabled", typeof(bool), false, ConfigurationPropertyOptions.None);
	}
}
