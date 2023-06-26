using System;
using Microsoft.Data.OData;

namespace System.Data.Services.Client
{
	// Token: 0x0200003E RID: 62
	public class MessageReaderSettingsArgs
	{
		// Token: 0x06000200 RID: 512 RVA: 0x0000ACFE File Offset: 0x00008EFE
		public MessageReaderSettingsArgs(ODataMessageReaderSettingsBase settings)
		{
			WebUtil.CheckArgumentNull<ODataMessageReaderSettingsBase>(settings, "settings");
			this.Settings = settings;
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000201 RID: 513 RVA: 0x0000AD19 File Offset: 0x00008F19
		// (set) Token: 0x06000202 RID: 514 RVA: 0x0000AD21 File Offset: 0x00008F21
		public ODataMessageReaderSettingsBase Settings { get; private set; }
	}
}
