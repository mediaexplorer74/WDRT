using System;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x02000080 RID: 128
	internal struct StoredSetting
	{
		// Token: 0x0600050B RID: 1291 RVA: 0x00021110 File Offset: 0x0001F310
		internal StoredSetting(SettingsSerializeAs serializeAs, XmlNode value)
		{
			this.SerializeAs = serializeAs;
			this.Value = value;
		}

		// Token: 0x04000C12 RID: 3090
		internal SettingsSerializeAs SerializeAs;

		// Token: 0x04000C13 RID: 3091
		internal XmlNode Value;
	}
}
