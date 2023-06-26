using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x020004AA RID: 1194
	internal class SystemDiagnosticsSection : ConfigurationSection
	{
		// Token: 0x06002C2C RID: 11308 RVA: 0x000C73F0 File Offset: 0x000C55F0
		static SystemDiagnosticsSection()
		{
			SystemDiagnosticsSection._properties.Add(SystemDiagnosticsSection._propAssert);
			SystemDiagnosticsSection._properties.Add(SystemDiagnosticsSection._propPerfCounters);
			SystemDiagnosticsSection._properties.Add(SystemDiagnosticsSection._propSources);
			SystemDiagnosticsSection._properties.Add(SystemDiagnosticsSection._propSharedListeners);
			SystemDiagnosticsSection._properties.Add(SystemDiagnosticsSection._propSwitches);
			SystemDiagnosticsSection._properties.Add(SystemDiagnosticsSection._propTrace);
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x06002C2D RID: 11309 RVA: 0x000C751B File Offset: 0x000C571B
		[ConfigurationProperty("assert")]
		public AssertSection Assert
		{
			get
			{
				return (AssertSection)base[SystemDiagnosticsSection._propAssert];
			}
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x06002C2E RID: 11310 RVA: 0x000C752D File Offset: 0x000C572D
		[ConfigurationProperty("performanceCounters")]
		public PerfCounterSection PerfCounters
		{
			get
			{
				return (PerfCounterSection)base[SystemDiagnosticsSection._propPerfCounters];
			}
		}

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x06002C2F RID: 11311 RVA: 0x000C753F File Offset: 0x000C573F
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SystemDiagnosticsSection._properties;
			}
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x06002C30 RID: 11312 RVA: 0x000C7546 File Offset: 0x000C5746
		[ConfigurationProperty("sources")]
		public SourceElementsCollection Sources
		{
			get
			{
				return (SourceElementsCollection)base[SystemDiagnosticsSection._propSources];
			}
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x06002C31 RID: 11313 RVA: 0x000C7558 File Offset: 0x000C5758
		[ConfigurationProperty("sharedListeners")]
		public ListenerElementsCollection SharedListeners
		{
			get
			{
				return (ListenerElementsCollection)base[SystemDiagnosticsSection._propSharedListeners];
			}
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x06002C32 RID: 11314 RVA: 0x000C756A File Offset: 0x000C576A
		[ConfigurationProperty("switches")]
		public SwitchElementsCollection Switches
		{
			get
			{
				return (SwitchElementsCollection)base[SystemDiagnosticsSection._propSwitches];
			}
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06002C33 RID: 11315 RVA: 0x000C757C File Offset: 0x000C577C
		[ConfigurationProperty("trace")]
		public TraceSection Trace
		{
			get
			{
				return (TraceSection)base[SystemDiagnosticsSection._propTrace];
			}
		}

		// Token: 0x06002C34 RID: 11316 RVA: 0x000C758E File Offset: 0x000C578E
		protected override void InitializeDefault()
		{
			this.Trace.Listeners.InitializeDefaultInternal();
		}

		// Token: 0x040026AC RID: 9900
		private static readonly ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x040026AD RID: 9901
		private static readonly ConfigurationProperty _propAssert = new ConfigurationProperty("assert", typeof(AssertSection), new AssertSection(), ConfigurationPropertyOptions.None);

		// Token: 0x040026AE RID: 9902
		private static readonly ConfigurationProperty _propPerfCounters = new ConfigurationProperty("performanceCounters", typeof(PerfCounterSection), new PerfCounterSection(), ConfigurationPropertyOptions.None);

		// Token: 0x040026AF RID: 9903
		private static readonly ConfigurationProperty _propSources = new ConfigurationProperty("sources", typeof(SourceElementsCollection), new SourceElementsCollection(), ConfigurationPropertyOptions.None);

		// Token: 0x040026B0 RID: 9904
		private static readonly ConfigurationProperty _propSharedListeners = new ConfigurationProperty("sharedListeners", typeof(SharedListenerElementsCollection), new SharedListenerElementsCollection(), ConfigurationPropertyOptions.None);

		// Token: 0x040026B1 RID: 9905
		private static readonly ConfigurationProperty _propSwitches = new ConfigurationProperty("switches", typeof(SwitchElementsCollection), new SwitchElementsCollection(), ConfigurationPropertyOptions.None);

		// Token: 0x040026B2 RID: 9906
		private static readonly ConfigurationProperty _propTrace = new ConfigurationProperty("trace", typeof(TraceSection), new TraceSection(), ConfigurationPropertyOptions.None);
	}
}
