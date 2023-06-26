using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x0200048F RID: 1167
	internal class AssertSection : ConfigurationElement
	{
		// Token: 0x06002B3E RID: 11070 RVA: 0x000C4904 File Offset: 0x000C2B04
		static AssertSection()
		{
			AssertSection._properties.Add(AssertSection._propAssertUIEnabled);
			AssertSection._properties.Add(AssertSection._propLogFile);
		}

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06002B3F RID: 11071 RVA: 0x000C4978 File Offset: 0x000C2B78
		[ConfigurationProperty("assertuienabled", DefaultValue = true)]
		public bool AssertUIEnabled
		{
			get
			{
				return (bool)base[AssertSection._propAssertUIEnabled];
			}
		}

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06002B40 RID: 11072 RVA: 0x000C498A File Offset: 0x000C2B8A
		[ConfigurationProperty("logfilename", DefaultValue = "")]
		public string LogFileName
		{
			get
			{
				return (string)base[AssertSection._propLogFile];
			}
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06002B41 RID: 11073 RVA: 0x000C499C File Offset: 0x000C2B9C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return AssertSection._properties;
			}
		}

		// Token: 0x04002667 RID: 9831
		private static readonly ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x04002668 RID: 9832
		private static readonly ConfigurationProperty _propAssertUIEnabled = new ConfigurationProperty("assertuienabled", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04002669 RID: 9833
		private static readonly ConfigurationProperty _propLogFile = new ConfigurationProperty("logfilename", typeof(string), string.Empty, ConfigurationPropertyOptions.None);
	}
}
