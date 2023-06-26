using System;
using System.Collections.Generic;

namespace System.Configuration
{
	// Token: 0x02000073 RID: 115
	internal sealed class UriSectionData
	{
		// Token: 0x0600049B RID: 1179 RVA: 0x0001F4F3 File Offset: 0x0001D6F3
		public UriSectionData()
		{
			this.schemeSettings = new Dictionary<string, SchemeSettingInternal>();
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x0001F506 File Offset: 0x0001D706
		// (set) Token: 0x0600049D RID: 1181 RVA: 0x0001F50E File Offset: 0x0001D70E
		public UriIdnScope? IdnScope
		{
			get
			{
				return this.idnScope;
			}
			set
			{
				this.idnScope = value;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x0001F517 File Offset: 0x0001D717
		// (set) Token: 0x0600049F RID: 1183 RVA: 0x0001F51F File Offset: 0x0001D71F
		public bool? IriParsing
		{
			get
			{
				return this.iriParsing;
			}
			set
			{
				this.iriParsing = value;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x0001F528 File Offset: 0x0001D728
		public Dictionary<string, SchemeSettingInternal> SchemeSettings
		{
			get
			{
				return this.schemeSettings;
			}
		}

		// Token: 0x04000BE6 RID: 3046
		private UriIdnScope? idnScope;

		// Token: 0x04000BE7 RID: 3047
		private bool? iriParsing;

		// Token: 0x04000BE8 RID: 3048
		private Dictionary<string, SchemeSettingInternal> schemeSettings;
	}
}
