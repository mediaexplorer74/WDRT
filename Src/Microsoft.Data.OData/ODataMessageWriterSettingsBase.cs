using System;

namespace Microsoft.Data.OData
{
	// Token: 0x02000135 RID: 309
	public abstract class ODataMessageWriterSettingsBase
	{
		// Token: 0x06000818 RID: 2072 RVA: 0x0001A9D2 File Offset: 0x00018BD2
		protected ODataMessageWriterSettingsBase()
		{
			this.checkCharacters = false;
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0001A9E1 File Offset: 0x00018BE1
		protected ODataMessageWriterSettingsBase(ODataMessageWriterSettingsBase other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriterSettingsBase>(other, "other");
			this.checkCharacters = other.checkCharacters;
			this.indent = other.indent;
			this.messageQuotas = new ODataMessageQuotas(other.MessageQuotas);
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x0001AA1D File Offset: 0x00018C1D
		// (set) Token: 0x0600081B RID: 2075 RVA: 0x0001AA25 File Offset: 0x00018C25
		public virtual bool Indent
		{
			get
			{
				return this.indent;
			}
			set
			{
				this.indent = value;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x0001AA2E File Offset: 0x00018C2E
		// (set) Token: 0x0600081D RID: 2077 RVA: 0x0001AA36 File Offset: 0x00018C36
		public virtual bool CheckCharacters
		{
			get
			{
				return this.checkCharacters;
			}
			set
			{
				this.checkCharacters = value;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x0001AA3F File Offset: 0x00018C3F
		// (set) Token: 0x0600081F RID: 2079 RVA: 0x0001AA5A File Offset: 0x00018C5A
		public virtual ODataMessageQuotas MessageQuotas
		{
			get
			{
				if (this.messageQuotas == null)
				{
					this.messageQuotas = new ODataMessageQuotas();
				}
				return this.messageQuotas;
			}
			set
			{
				this.messageQuotas = value;
			}
		}

		// Token: 0x04000313 RID: 787
		private ODataMessageQuotas messageQuotas;

		// Token: 0x04000314 RID: 788
		private bool checkCharacters;

		// Token: 0x04000315 RID: 789
		private bool indent;
	}
}
