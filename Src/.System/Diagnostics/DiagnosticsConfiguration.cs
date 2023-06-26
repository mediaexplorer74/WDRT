using System;
using System.Configuration;
using System.Threading;

namespace System.Diagnostics
{
	// Token: 0x02000499 RID: 1177
	internal static class DiagnosticsConfiguration
	{
		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x06002BA1 RID: 11169 RVA: 0x000C58C4 File Offset: 0x000C3AC4
		internal static SwitchElementsCollection SwitchSettings
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null)
				{
					return systemDiagnosticsSection.Switches;
				}
				return null;
			}
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06002BA2 RID: 11170 RVA: 0x000C58EC File Offset: 0x000C3AEC
		internal static bool AssertUIEnabled
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				return systemDiagnosticsSection == null || systemDiagnosticsSection.Assert == null || systemDiagnosticsSection.Assert.AssertUIEnabled;
			}
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x06002BA3 RID: 11171 RVA: 0x000C5920 File Offset: 0x000C3B20
		internal static string ConfigFilePath
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null)
				{
					return systemDiagnosticsSection.ElementInformation.Source;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06002BA4 RID: 11172 RVA: 0x000C5950 File Offset: 0x000C3B50
		internal static string LogFileName
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null && systemDiagnosticsSection.Assert != null)
				{
					return systemDiagnosticsSection.Assert.LogFileName;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06002BA5 RID: 11173 RVA: 0x000C5988 File Offset: 0x000C3B88
		internal static bool AutoFlush
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				return systemDiagnosticsSection != null && systemDiagnosticsSection.Trace != null && systemDiagnosticsSection.Trace.AutoFlush;
			}
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06002BA6 RID: 11174 RVA: 0x000C59BC File Offset: 0x000C3BBC
		internal static bool UseGlobalLock
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				return systemDiagnosticsSection == null || systemDiagnosticsSection.Trace == null || systemDiagnosticsSection.Trace.UseGlobalLock;
			}
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x06002BA7 RID: 11175 RVA: 0x000C59F0 File Offset: 0x000C3BF0
		internal static int IndentSize
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null && systemDiagnosticsSection.Trace != null)
				{
					return systemDiagnosticsSection.Trace.IndentSize;
				}
				return 4;
			}
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x06002BA8 RID: 11176 RVA: 0x000C5A24 File Offset: 0x000C3C24
		internal static int PerfomanceCountersFileMappingSize
		{
			get
			{
				int num = 0;
				while (!DiagnosticsConfiguration.CanInitialize() && num <= 5)
				{
					if (num == 5)
					{
						return 524288;
					}
					Thread.Sleep(200);
					num++;
				}
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null && systemDiagnosticsSection.PerfCounters != null)
				{
					int num2 = systemDiagnosticsSection.PerfCounters.FileMappingSize;
					if (num2 < 32768)
					{
						num2 = 32768;
					}
					if (num2 > 33554432)
					{
						num2 = 33554432;
					}
					return num2;
				}
				return 524288;
			}
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06002BA9 RID: 11177 RVA: 0x000C5AA0 File Offset: 0x000C3CA0
		internal static ListenerElementsCollection SharedListeners
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null)
				{
					return systemDiagnosticsSection.SharedListeners;
				}
				return null;
			}
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06002BAA RID: 11178 RVA: 0x000C5AC8 File Offset: 0x000C3CC8
		internal static SourceElementsCollection Sources
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
				if (systemDiagnosticsSection != null && systemDiagnosticsSection.Sources != null)
				{
					return systemDiagnosticsSection.Sources;
				}
				return null;
			}
		}

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x06002BAB RID: 11179 RVA: 0x000C5AF5 File Offset: 0x000C3CF5
		internal static SystemDiagnosticsSection SystemDiagnosticsSection
		{
			get
			{
				DiagnosticsConfiguration.Initialize();
				return DiagnosticsConfiguration.configSection;
			}
		}

		// Token: 0x06002BAC RID: 11180 RVA: 0x000C5B04 File Offset: 0x000C3D04
		private static SystemDiagnosticsSection GetConfigSection()
		{
			return (SystemDiagnosticsSection)PrivilegedConfigurationManager.GetSection("system.diagnostics");
		}

		// Token: 0x06002BAD RID: 11181 RVA: 0x000C5B22 File Offset: 0x000C3D22
		internal static bool IsInitializing()
		{
			return DiagnosticsConfiguration.initState == InitState.Initializing;
		}

		// Token: 0x06002BAE RID: 11182 RVA: 0x000C5B2E File Offset: 0x000C3D2E
		internal static bool IsInitialized()
		{
			return DiagnosticsConfiguration.initState == InitState.Initialized;
		}

		// Token: 0x06002BAF RID: 11183 RVA: 0x000C5B3A File Offset: 0x000C3D3A
		internal static bool CanInitialize()
		{
			return DiagnosticsConfiguration.initState != InitState.Initializing && !ConfigurationManagerInternalFactory.Instance.SetConfigurationSystemInProgress;
		}

		// Token: 0x06002BB0 RID: 11184 RVA: 0x000C5B58 File Offset: 0x000C3D58
		internal static void Initialize()
		{
			object critSec = TraceInternal.critSec;
			lock (critSec)
			{
				if (DiagnosticsConfiguration.initState == InitState.NotInitialized && !ConfigurationManagerInternalFactory.Instance.SetConfigurationSystemInProgress)
				{
					DiagnosticsConfiguration.initState = InitState.Initializing;
					try
					{
						DiagnosticsConfiguration.configSection = DiagnosticsConfiguration.GetConfigSection();
					}
					finally
					{
						DiagnosticsConfiguration.initState = InitState.Initialized;
					}
				}
			}
		}

		// Token: 0x06002BB1 RID: 11185 RVA: 0x000C5BD4 File Offset: 0x000C3DD4
		internal static void Refresh()
		{
			ConfigurationManager.RefreshSection("system.diagnostics");
			SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.configSection;
			if (systemDiagnosticsSection != null)
			{
				if (systemDiagnosticsSection.Switches != null)
				{
					foreach (object obj in systemDiagnosticsSection.Switches)
					{
						SwitchElement switchElement = (SwitchElement)obj;
						switchElement.ResetProperties();
					}
				}
				if (systemDiagnosticsSection.SharedListeners != null)
				{
					foreach (object obj2 in systemDiagnosticsSection.SharedListeners)
					{
						ListenerElement listenerElement = (ListenerElement)obj2;
						listenerElement.ResetProperties();
					}
				}
				if (systemDiagnosticsSection.Sources != null)
				{
					foreach (object obj3 in systemDiagnosticsSection.Sources)
					{
						SourceElement sourceElement = (SourceElement)obj3;
						sourceElement.ResetProperties();
					}
				}
			}
			DiagnosticsConfiguration.configSection = null;
			DiagnosticsConfiguration.initState = InitState.NotInitialized;
			DiagnosticsConfiguration.Initialize();
		}

		// Token: 0x0400267C RID: 9852
		private static volatile SystemDiagnosticsSection configSection;

		// Token: 0x0400267D RID: 9853
		private static volatile InitState initState;
	}
}
