using System;

namespace Microsoft.Data.OData
{
	// Token: 0x02000134 RID: 308
	public abstract class ODataMessageReaderSettingsBase
	{
		// Token: 0x0600080E RID: 2062 RVA: 0x0001A910 File Offset: 0x00018B10
		protected ODataMessageReaderSettingsBase()
		{
			this.checkCharacters = false;
			this.enableAtomMetadataReading = false;
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0001A928 File Offset: 0x00018B28
		protected ODataMessageReaderSettingsBase(ODataMessageReaderSettingsBase other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettingsBase>(other, "other");
			this.checkCharacters = other.checkCharacters;
			this.enableAtomMetadataReading = other.enableAtomMetadataReading;
			this.messageQuotas = new ODataMessageQuotas(other.MessageQuotas);
			this.shouldIncludeAnnotation = other.shouldIncludeAnnotation;
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x0001A97B File Offset: 0x00018B7B
		// (set) Token: 0x06000811 RID: 2065 RVA: 0x0001A983 File Offset: 0x00018B83
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

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x0001A98C File Offset: 0x00018B8C
		// (set) Token: 0x06000813 RID: 2067 RVA: 0x0001A994 File Offset: 0x00018B94
		public virtual bool EnableAtomMetadataReading
		{
			get
			{
				return this.enableAtomMetadataReading;
			}
			set
			{
				this.enableAtomMetadataReading = value;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x0001A99D File Offset: 0x00018B9D
		// (set) Token: 0x06000815 RID: 2069 RVA: 0x0001A9B8 File Offset: 0x00018BB8
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

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000816 RID: 2070 RVA: 0x0001A9C1 File Offset: 0x00018BC1
		// (set) Token: 0x06000817 RID: 2071 RVA: 0x0001A9C9 File Offset: 0x00018BC9
		public virtual Func<string, bool> ShouldIncludeAnnotation
		{
			get
			{
				return this.shouldIncludeAnnotation;
			}
			set
			{
				this.shouldIncludeAnnotation = value;
			}
		}

		// Token: 0x0400030F RID: 783
		private ODataMessageQuotas messageQuotas;

		// Token: 0x04000310 RID: 784
		private bool checkCharacters;

		// Token: 0x04000311 RID: 785
		private bool enableAtomMetadataReading;

		// Token: 0x04000312 RID: 786
		private Func<string, bool> shouldIncludeAnnotation;
	}
}
