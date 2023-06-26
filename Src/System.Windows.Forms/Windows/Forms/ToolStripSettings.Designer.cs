using System;
using System.Configuration;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x02000400 RID: 1024
	internal partial class ToolStripSettings : ApplicationSettingsBase
	{
		// Token: 0x1700115E RID: 4446
		// (get) Token: 0x060046C3 RID: 18115 RVA: 0x00128BDB File Offset: 0x00126DDB
		// (set) Token: 0x060046C4 RID: 18116 RVA: 0x00128BED File Offset: 0x00126DED
		[UserScopedSetting]
		[DefaultSettingValue("true")]
		public bool IsDefault
		{
			get
			{
				return (bool)this["IsDefault"];
			}
			set
			{
				this["IsDefault"] = value;
			}
		}

		// Token: 0x17001161 RID: 4449
		// (get) Token: 0x060046C9 RID: 18121 RVA: 0x00128C40 File Offset: 0x00126E40
		// (set) Token: 0x060046CA RID: 18122 RVA: 0x00128C52 File Offset: 0x00126E52
		[UserScopedSetting]
		[DefaultSettingValue("0,0")]
		public Point Location
		{
			get
			{
				return (Point)this["Location"];
			}
			set
			{
				this["Location"] = value;
			}
		}

		// Token: 0x17001162 RID: 4450
		// (get) Token: 0x060046CB RID: 18123 RVA: 0x00128C65 File Offset: 0x00126E65
		// (set) Token: 0x060046CC RID: 18124 RVA: 0x00128C77 File Offset: 0x00126E77
		[UserScopedSetting]
		[DefaultSettingValue("0,0")]
		public Size Size
		{
			get
			{
				return (Size)this["Size"];
			}
			set
			{
				this["Size"] = value;
			}
		}

		// Token: 0x17001164 RID: 4452
		// (get) Token: 0x060046CF RID: 18127 RVA: 0x00128CAA File Offset: 0x00126EAA
		// (set) Token: 0x060046D0 RID: 18128 RVA: 0x00128CBC File Offset: 0x00126EBC
		[UserScopedSetting]
		[DefaultSettingValue("true")]
		public bool Visible
		{
			get
			{
				return (bool)this["Visible"];
			}
			set
			{
				this["Visible"] = value;
			}
		}
	}
}
