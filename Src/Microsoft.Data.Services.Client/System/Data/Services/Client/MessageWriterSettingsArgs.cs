using System;
using Microsoft.Data.OData;

namespace System.Data.Services.Client
{
	// Token: 0x0200003F RID: 63
	public class MessageWriterSettingsArgs
	{
		// Token: 0x06000203 RID: 515 RVA: 0x0000AD2A File Offset: 0x00008F2A
		public MessageWriterSettingsArgs(ODataMessageWriterSettingsBase settings)
		{
			WebUtil.CheckArgumentNull<ODataMessageWriterSettingsBase>(settings, "settings");
			this.Settings = settings;
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000204 RID: 516 RVA: 0x0000AD45 File Offset: 0x00008F45
		// (set) Token: 0x06000205 RID: 517 RVA: 0x0000AD4D File Offset: 0x00008F4D
		public ODataMessageWriterSettingsBase Settings { get; private set; }
	}
}
