using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData
{
	// Token: 0x020000EA RID: 234
	internal abstract class ODataDeserializer
	{
		// Token: 0x060005D0 RID: 1488 RVA: 0x0001447D File Offset: 0x0001267D
		protected ODataDeserializer(ODataInputContext inputContext)
		{
			this.inputContext = inputContext;
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x0001448C File Offset: 0x0001268C
		internal bool UseClientFormatBehavior
		{
			get
			{
				return this.inputContext.UseClientFormatBehavior;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x00014499 File Offset: 0x00012699
		internal bool UseServerFormatBehavior
		{
			get
			{
				return this.inputContext.UseServerFormatBehavior;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x000144A6 File Offset: 0x000126A6
		internal bool UseDefaultFormatBehavior
		{
			get
			{
				return this.inputContext.UseDefaultFormatBehavior;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x000144B3 File Offset: 0x000126B3
		internal ODataMessageReaderSettings MessageReaderSettings
		{
			get
			{
				return this.inputContext.MessageReaderSettings;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x000144C0 File Offset: 0x000126C0
		internal ODataVersion Version
		{
			get
			{
				return this.inputContext.Version;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x000144CD File Offset: 0x000126CD
		internal bool ReadingResponse
		{
			get
			{
				return this.inputContext.ReadingResponse;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x000144DA File Offset: 0x000126DA
		internal IEdmModel Model
		{
			get
			{
				return this.inputContext.Model;
			}
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x000144E7 File Offset: 0x000126E7
		internal DuplicatePropertyNamesChecker CreateDuplicatePropertyNamesChecker()
		{
			return this.inputContext.CreateDuplicatePropertyNamesChecker();
		}

		// Token: 0x04000269 RID: 617
		private readonly ODataInputContext inputContext;
	}
}
