using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.WindowsDeviceRecoveryTool.Styles.Assets;

namespace Microsoft.WindowsDeviceRecoveryTool.Properties
{
	// Token: 0x0200007B RID: 123
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.4.0.0")]
	public sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x00015D94 File Offset: 0x00013F94
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x00015DAC File Offset: 0x00013FAC
		// (set) Token: 0x0600041A RID: 1050 RVA: 0x00015DCE File Offset: 0x00013FCE
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("AccentColorIndigo")]
		public string Style
		{
			get
			{
				return (string)this["Style"];
			}
			set
			{
				this["Style"] = value;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x00015DE0 File Offset: 0x00013FE0
		// (set) Token: 0x0600041C RID: 1052 RVA: 0x00015E02 File Offset: 0x00014002
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		public string Login
		{
			get
			{
				return (string)this["Login"];
			}
			set
			{
				this["Login"] = value;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x00015E14 File Offset: 0x00014014
		// (set) Token: 0x0600041E RID: 1054 RVA: 0x00015E36 File Offset: 0x00014036
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		public string Password
		{
			get
			{
				return (string)this["Password"];
			}
			set
			{
				this["Password"] = value;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x00015E48 File Offset: 0x00014048
		// (set) Token: 0x06000420 RID: 1056 RVA: 0x00015E6A File Offset: 0x0001406A
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("False")]
		public string SaveCredentials
		{
			get
			{
				return (string)this["SaveCredentials"];
			}
			set
			{
				this["SaveCredentials"] = value;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x00015E7C File Offset: 0x0001407C
		// (set) Token: 0x06000422 RID: 1058 RVA: 0x00015E9E File Offset: 0x0001409E
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		public string UserGroup
		{
			get
			{
				return (string)this["UserGroup"];
			}
			set
			{
				this["UserGroup"] = value;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x00015EB0 File Offset: 0x000140B0
		// (set) Token: 0x06000424 RID: 1060 RVA: 0x00015ED2 File Offset: 0x000140D2
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		public string ProxyPassword
		{
			get
			{
				return (string)this["ProxyPassword"];
			}
			set
			{
				this["ProxyPassword"] = value;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x00015EE4 File Offset: 0x000140E4
		// (set) Token: 0x06000426 RID: 1062 RVA: 0x00015F06 File Offset: 0x00014106
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("True")]
		public bool TraceEnabled
		{
			get
			{
				return (bool)this["TraceEnabled"];
			}
			set
			{
				this["TraceEnabled"] = value;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x00015F1C File Offset: 0x0001411C
		// (set) Token: 0x06000428 RID: 1064 RVA: 0x00015F3E File Offset: 0x0001413E
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("%temp%")]
		public string ZipFilePath
		{
			get
			{
				return (string)this["ZipFilePath"];
			}
			set
			{
				this["ZipFilePath"] = value;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x00015F50 File Offset: 0x00014150
		// (set) Token: 0x0600042A RID: 1066 RVA: 0x00015F72 File Offset: 0x00014172
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		public string ProxyUsername
		{
			get
			{
				return (string)this["ProxyUsername"];
			}
			set
			{
				this["ProxyUsername"] = value;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x00015F84 File Offset: 0x00014184
		// (set) Token: 0x0600042C RID: 1068 RVA: 0x00015FA6 File Offset: 0x000141A6
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		public string ProxyAddress
		{
			get
			{
				return (string)this["ProxyAddress"];
			}
			set
			{
				this["ProxyAddress"] = value;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x00015FB8 File Offset: 0x000141B8
		// (set) Token: 0x0600042E RID: 1070 RVA: 0x00015FDA File Offset: 0x000141DA
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("8080")]
		public int ProxyPort
		{
			get
			{
				return (int)this["ProxyPort"];
			}
			set
			{
				this["ProxyPort"] = value;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x00015FF0 File Offset: 0x000141F0
		// (set) Token: 0x06000430 RID: 1072 RVA: 0x00016012 File Offset: 0x00014212
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("False")]
		public bool UseManualProxy
		{
			get
			{
				return (bool)this["UseManualProxy"];
			}
			set
			{
				this["UseManualProxy"] = value;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x00016028 File Offset: 0x00014228
		// (set) Token: 0x06000432 RID: 1074 RVA: 0x0001604A File Offset: 0x0001424A
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("1")]
		public string DaysToCollectLogFiles
		{
			get
			{
				return (string)this["DaysToCollectLogFiles"];
			}
			set
			{
				this["DaysToCollectLogFiles"] = value;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x0001605C File Offset: 0x0001425C
		// (set) Token: 0x06000434 RID: 1076 RVA: 0x0001607E File Offset: 0x0001427E
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		public string RememberedUsergroups
		{
			get
			{
				return (string)this["RememberedUsergroups"];
			}
			set
			{
				this["RememberedUsergroups"] = value;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x00016090 File Offset: 0x00014290
		// (set) Token: 0x06000436 RID: 1078 RVA: 0x000160B2 File Offset: 0x000142B2
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("True")]
		public bool CallUpgrade
		{
			get
			{
				return (bool)this["CallUpgrade"];
			}
			set
			{
				this["CallUpgrade"] = value;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x000160C8 File Offset: 0x000142C8
		// (set) Token: 0x06000438 RID: 1080 RVA: 0x000160EA File Offset: 0x000142EA
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("ThemeLight")]
		public string Theme
		{
			get
			{
				return (string)this["Theme"];
			}
			set
			{
				this["Theme"] = value;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x000160FC File Offset: 0x000142FC
		// (set) Token: 0x0600043A RID: 1082 RVA: 0x0001611E File Offset: 0x0001431E
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("False")]
		public string ExtendedTraceEnabled
		{
			get
			{
				return (string)this["ExtendedTraceEnabled"];
			}
			set
			{
				this["ExtendedTraceEnabled"] = value;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x00016130 File Offset: 0x00014330
		// (set) Token: 0x0600043C RID: 1084 RVA: 0x00016152 File Offset: 0x00014352
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("False")]
		public bool CustomPackagesPathEnabled
		{
			get
			{
				return (bool)this["CustomPackagesPathEnabled"];
			}
			set
			{
				this["CustomPackagesPathEnabled"] = value;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x00016168 File Offset: 0x00014368
		// (set) Token: 0x0600043E RID: 1086 RVA: 0x0001618A File Offset: 0x0001438A
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		public string PackagesPath
		{
			get
			{
				return (string)this["PackagesPath"];
			}
			set
			{
				this["PackagesPath"] = value;
			}
		}

		// Token: 0x040001D2 RID: 466
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
