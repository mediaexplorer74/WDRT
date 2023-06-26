using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x0200049E RID: 1182
	internal class PerfCounterSection : ConfigurationElement
	{
		// Token: 0x06002BD5 RID: 11221 RVA: 0x000C6628 File Offset: 0x000C4828
		static PerfCounterSection()
		{
			PerfCounterSection._properties.Add(PerfCounterSection._propFileMappingSize);
		}

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x06002BD6 RID: 11222 RVA: 0x000C6667 File Offset: 0x000C4867
		[ConfigurationProperty("filemappingsize", DefaultValue = 524288)]
		public int FileMappingSize
		{
			get
			{
				return (int)base[PerfCounterSection._propFileMappingSize];
			}
		}

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06002BD7 RID: 11223 RVA: 0x000C6679 File Offset: 0x000C4879
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return PerfCounterSection._properties;
			}
		}

		// Token: 0x04002685 RID: 9861
		private static readonly ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x04002686 RID: 9862
		private static readonly ConfigurationProperty _propFileMappingSize = new ConfigurationProperty("filemappingsize", typeof(int), 524288, ConfigurationPropertyOptions.None);
	}
}
