using System;
using Microsoft.Data.OData;

namespace System.Data.Services.Client
{
	// Token: 0x02000037 RID: 55
	internal class DataServiceClientMessageReaderSettingsShim : ODataMessageReaderSettingsBase
	{
		// Token: 0x0600019A RID: 410 RVA: 0x000090CA File Offset: 0x000072CA
		internal DataServiceClientMessageReaderSettingsShim(ODataMessageReaderSettingsBase settings)
		{
			this.settings = settings;
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600019B RID: 411 RVA: 0x000090D9 File Offset: 0x000072D9
		// (set) Token: 0x0600019C RID: 412 RVA: 0x000090E6 File Offset: 0x000072E6
		public override bool EnableAtomMetadataReading
		{
			get
			{
				return this.settings.EnableAtomMetadataReading;
			}
			set
			{
				this.settings.EnableAtomMetadataReading = value;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600019D RID: 413 RVA: 0x000090F4 File Offset: 0x000072F4
		// (set) Token: 0x0600019E RID: 414 RVA: 0x00009101 File Offset: 0x00007301
		public override bool CheckCharacters
		{
			get
			{
				return this.settings.CheckCharacters;
			}
			set
			{
				this.settings.CheckCharacters = value;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600019F RID: 415 RVA: 0x0000910F File Offset: 0x0000730F
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x0000911C File Offset: 0x0000731C
		public override ODataMessageQuotas MessageQuotas
		{
			get
			{
				return this.settings.MessageQuotas;
			}
			set
			{
				this.settings.MessageQuotas = value;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x0000912A File Offset: 0x0000732A
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x00009137 File Offset: 0x00007337
		public override Func<string, bool> ShouldIncludeAnnotation
		{
			get
			{
				return this.settings.ShouldIncludeAnnotation;
			}
			set
			{
				this.settings.ShouldIncludeAnnotation = value;
			}
		}

		// Token: 0x040001FE RID: 510
		private readonly ODataMessageReaderSettingsBase settings;
	}
}
