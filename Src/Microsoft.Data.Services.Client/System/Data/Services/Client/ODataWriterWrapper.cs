using System;
using Microsoft.Data.OData;

namespace System.Data.Services.Client
{
	// Token: 0x0200008A RID: 138
	internal class ODataWriterWrapper
	{
		// Token: 0x060004ED RID: 1261 RVA: 0x00013F7D File Offset: 0x0001217D
		private ODataWriterWrapper(ODataWriter odataWriter, DataServiceClientRequestPipelineConfiguration requestPipeline)
		{
			this.odataWriter = odataWriter;
			this.requestPipeline = requestPipeline;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00013F93 File Offset: 0x00012193
		internal static ODataWriterWrapper CreateForEntry(ODataMessageWriter messageWriter, DataServiceClientRequestPipelineConfiguration requestPipeline)
		{
			return new ODataWriterWrapper(messageWriter.CreateODataEntryWriter(), requestPipeline);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00013FA1 File Offset: 0x000121A1
		internal static ODataWriterWrapper CreateForEntryTest(ODataWriter writer, DataServiceClientRequestPipelineConfiguration requestPipeline)
		{
			return new ODataWriterWrapper(writer, requestPipeline);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00013FAA File Offset: 0x000121AA
		internal void WriteStart(ODataEntry entry, object entity)
		{
			this.requestPipeline.ExecuteOnEntryStartActions(entry, entity);
			this.odataWriter.WriteStart(entry);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00013FC5 File Offset: 0x000121C5
		internal void WriteEnd(ODataEntry entry, object entity)
		{
			this.requestPipeline.ExecuteOnEntryEndActions(entry, entity);
			this.odataWriter.WriteEnd();
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00013FDF File Offset: 0x000121DF
		internal void WriteEnd(ODataNavigationLink navlink, object source, object target)
		{
			this.requestPipeline.ExecuteOnNavigationLinkEndActions(navlink, source, target);
			this.odataWriter.WriteEnd();
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00013FFA File Offset: 0x000121FA
		internal void WriteNavigationLinkEnd(ODataNavigationLink navlink, object source, object target)
		{
			this.requestPipeline.ExecuteOnNavigationLinkEndActions(navlink, source, target);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0001400A File Offset: 0x0001220A
		internal void WriteNavigationLinksEnd()
		{
			this.odataWriter.WriteEnd();
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00014017 File Offset: 0x00012217
		internal void WriteStart(ODataNavigationLink navigationLink, object source, object target)
		{
			this.requestPipeline.ExecuteOnNavigationLinkStartActions(navigationLink, source, target);
			this.odataWriter.WriteStart(navigationLink);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00014033 File Offset: 0x00012233
		internal void WriteNavigationLinkStart(ODataNavigationLink navigationLink, object source, object target)
		{
			this.requestPipeline.ExecuteOnNavigationLinkStartActions(navigationLink, source, target);
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00014043 File Offset: 0x00012243
		internal void WriteNavigationLinksStart(ODataNavigationLink navigationLink)
		{
			this.odataWriter.WriteStart(navigationLink);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00014051 File Offset: 0x00012251
		internal void WriteEntityReferenceLink(ODataEntityReferenceLink referenceLink, object source, object target)
		{
			this.requestPipeline.ExecuteEntityReferenceLinkActions(referenceLink, source, target);
			this.odataWriter.WriteEntityReferenceLink(referenceLink);
		}

		// Token: 0x040002F7 RID: 759
		private readonly ODataWriter odataWriter;

		// Token: 0x040002F8 RID: 760
		private readonly DataServiceClientRequestPipelineConfiguration requestPipeline;
	}
}
