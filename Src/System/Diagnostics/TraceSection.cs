using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x020004B5 RID: 1205
	internal class TraceSection : ConfigurationElement
	{
		// Token: 0x06002CF2 RID: 11506 RVA: 0x000C9DDC File Offset: 0x000C7FDC
		static TraceSection()
		{
			TraceSection._properties.Add(TraceSection._propListeners);
			TraceSection._properties.Add(TraceSection._propAutoFlush);
			TraceSection._properties.Add(TraceSection._propIndentSize);
			TraceSection._properties.Add(TraceSection._propUseGlobalLock);
		}

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x06002CF3 RID: 11507 RVA: 0x000C9EAE File Offset: 0x000C80AE
		[ConfigurationProperty("autoflush", DefaultValue = false)]
		public bool AutoFlush
		{
			get
			{
				return (bool)base[TraceSection._propAutoFlush];
			}
		}

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x06002CF4 RID: 11508 RVA: 0x000C9EC0 File Offset: 0x000C80C0
		[ConfigurationProperty("indentsize", DefaultValue = 4)]
		public int IndentSize
		{
			get
			{
				return (int)base[TraceSection._propIndentSize];
			}
		}

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x06002CF5 RID: 11509 RVA: 0x000C9ED2 File Offset: 0x000C80D2
		[ConfigurationProperty("listeners")]
		public ListenerElementsCollection Listeners
		{
			get
			{
				return (ListenerElementsCollection)base[TraceSection._propListeners];
			}
		}

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06002CF6 RID: 11510 RVA: 0x000C9EE4 File Offset: 0x000C80E4
		[ConfigurationProperty("useGlobalLock", DefaultValue = true)]
		public bool UseGlobalLock
		{
			get
			{
				return (bool)base[TraceSection._propUseGlobalLock];
			}
		}

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06002CF7 RID: 11511 RVA: 0x000C9EF6 File Offset: 0x000C80F6
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return TraceSection._properties;
			}
		}

		// Token: 0x040026E7 RID: 9959
		private static readonly ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x040026E8 RID: 9960
		private static readonly ConfigurationProperty _propListeners = new ConfigurationProperty("listeners", typeof(ListenerElementsCollection), new ListenerElementsCollection(), ConfigurationPropertyOptions.None);

		// Token: 0x040026E9 RID: 9961
		private static readonly ConfigurationProperty _propAutoFlush = new ConfigurationProperty("autoflush", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x040026EA RID: 9962
		private static readonly ConfigurationProperty _propIndentSize = new ConfigurationProperty("indentsize", typeof(int), 4, ConfigurationPropertyOptions.None);

		// Token: 0x040026EB RID: 9963
		private static readonly ConfigurationProperty _propUseGlobalLock = new ConfigurationProperty("useGlobalLock", typeof(bool), true, ConfigurationPropertyOptions.None);
	}
}
