using System;
using System.Configuration;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x02000400 RID: 1024
	internal partial class ToolStripSettings : ApplicationSettingsBase
	{
		// Token: 0x060046C2 RID: 18114 RVA: 0x00128BD2 File Offset: 0x00126DD2
		internal ToolStripSettings(string settingsKey)
			: base(settingsKey)
		{
		}

		// Token: 0x1700115F RID: 4447
		// (get) Token: 0x060046C5 RID: 18117 RVA: 0x00128C00 File Offset: 0x00126E00
		// (set) Token: 0x060046C6 RID: 18118 RVA: 0x00128C12 File Offset: 0x00126E12
		[UserScopedSetting]
		public string ItemOrder
		{
			get
			{
				return this["ItemOrder"] as string;
			}
			set
			{
				this["ItemOrder"] = value;
			}
		}

		// Token: 0x17001160 RID: 4448
		// (get) Token: 0x060046C7 RID: 18119 RVA: 0x00128C20 File Offset: 0x00126E20
		// (set) Token: 0x060046C8 RID: 18120 RVA: 0x00128C32 File Offset: 0x00126E32
		[UserScopedSetting]
		public string Name
		{
			get
			{
				return this["Name"] as string;
			}
			set
			{
				this["Name"] = value;
			}
		}

		// Token: 0x17001163 RID: 4451
		// (get) Token: 0x060046CD RID: 18125 RVA: 0x00128C8A File Offset: 0x00126E8A
		// (set) Token: 0x060046CE RID: 18126 RVA: 0x00128C9C File Offset: 0x00126E9C
		[UserScopedSetting]
		public string ToolStripPanelName
		{
			get
			{
				return this["ToolStripPanelName"] as string;
			}
			set
			{
				this["ToolStripPanelName"] = value;
			}
		}

		// Token: 0x060046D1 RID: 18129 RVA: 0x00128CCF File Offset: 0x00126ECF
		public override void Save()
		{
			this.IsDefault = false;
			base.Save();
		}
	}
}
