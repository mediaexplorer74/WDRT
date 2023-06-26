using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData
{
	// Token: 0x020000EE RID: 238
	internal abstract class ODataSerializer
	{
		// Token: 0x060005F7 RID: 1527 RVA: 0x0001509D File Offset: 0x0001329D
		protected ODataSerializer(ODataOutputContext outputContext)
		{
			this.outputContext = outputContext;
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x000150AC File Offset: 0x000132AC
		internal bool UseClientFormatBehavior
		{
			get
			{
				return this.outputContext.UseClientFormatBehavior;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x000150B9 File Offset: 0x000132B9
		internal bool UseServerFormatBehavior
		{
			get
			{
				return this.outputContext.UseServerFormatBehavior;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x000150C6 File Offset: 0x000132C6
		internal bool UseDefaultFormatBehavior
		{
			get
			{
				return this.outputContext.UseDefaultFormatBehavior;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x000150D3 File Offset: 0x000132D3
		internal ODataMessageWriterSettings MessageWriterSettings
		{
			get
			{
				return this.outputContext.MessageWriterSettings;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x000150E0 File Offset: 0x000132E0
		internal IODataUrlResolver UrlResolver
		{
			get
			{
				return this.outputContext.UrlResolver;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x000150ED File Offset: 0x000132ED
		internal ODataVersion Version
		{
			get
			{
				return this.outputContext.Version;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x000150FA File Offset: 0x000132FA
		internal bool WritingResponse
		{
			get
			{
				return this.outputContext.WritingResponse;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x00015107 File Offset: 0x00013307
		internal IEdmModel Model
		{
			get
			{
				return this.outputContext.Model;
			}
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00015114 File Offset: 0x00013314
		internal DuplicatePropertyNamesChecker CreateDuplicatePropertyNamesChecker()
		{
			return new DuplicatePropertyNamesChecker(this.MessageWriterSettings.WriterBehavior.AllowDuplicatePropertyNames, this.WritingResponse);
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00015131 File Offset: 0x00013331
		protected void ValidateAssociationLink(ODataAssociationLink associationLink, IEdmEntityType entryEntityType)
		{
			WriterValidationUtils.ValidateAssociationLink(associationLink, this.Version, this.WritingResponse);
			WriterValidationUtils.ValidateNavigationPropertyDefined(associationLink.Name, entryEntityType, this.outputContext.MessageWriterSettings.UndeclaredPropertyBehaviorKinds);
		}

		// Token: 0x04000272 RID: 626
		private readonly ODataOutputContext outputContext;
	}
}
