using System;
using System.Xml.Linq;

namespace System.Data.Services.Client
{
	// Token: 0x02000123 RID: 291
	internal sealed class WritingEntityInfo
	{
		// Token: 0x060009B5 RID: 2485 RVA: 0x00027C20 File Offset: 0x00025E20
		internal WritingEntityInfo(object entity, RequestInfo requestInfo)
		{
			this.Entity = entity;
			this.EntryPayload = new XDocument();
			this.RequestInfo = requestInfo;
		}

		// Token: 0x04000599 RID: 1433
		internal readonly object Entity;

		// Token: 0x0400059A RID: 1434
		internal readonly XDocument EntryPayload;

		// Token: 0x0400059B RID: 1435
		internal readonly RequestInfo RequestInfo;
	}
}
