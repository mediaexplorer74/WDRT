using System;
using Microsoft.Data.OData;

namespace System.Data.Services.Client
{
	// Token: 0x02000036 RID: 54
	internal class DataServiceClientMessageWriterSettingsShim : ODataMessageWriterSettingsBase
	{
		// Token: 0x06000193 RID: 403 RVA: 0x0000906A File Offset: 0x0000726A
		internal DataServiceClientMessageWriterSettingsShim(ODataMessageWriterSettingsBase settings)
		{
			this.settings = settings;
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00009079 File Offset: 0x00007279
		// (set) Token: 0x06000195 RID: 405 RVA: 0x00009086 File Offset: 0x00007286
		public override bool Indent
		{
			get
			{
				return this.settings.Indent;
			}
			set
			{
				this.settings.Indent = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00009094 File Offset: 0x00007294
		// (set) Token: 0x06000197 RID: 407 RVA: 0x000090A1 File Offset: 0x000072A1
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

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000198 RID: 408 RVA: 0x000090AF File Offset: 0x000072AF
		// (set) Token: 0x06000199 RID: 409 RVA: 0x000090BC File Offset: 0x000072BC
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

		// Token: 0x040001FD RID: 509
		private readonly ODataMessageWriterSettingsBase settings;
	}
}
