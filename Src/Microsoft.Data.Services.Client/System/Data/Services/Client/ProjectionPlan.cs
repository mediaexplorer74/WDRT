using System;
using System.Data.Services.Client.Materialization;
using Microsoft.Data.OData;

namespace System.Data.Services.Client
{
	// Token: 0x02000081 RID: 129
	internal class ProjectionPlan
	{
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x00011EC8 File Offset: 0x000100C8
		// (set) Token: 0x0600045D RID: 1117 RVA: 0x00011ED0 File Offset: 0x000100D0
		internal Type LastSegmentType { get; set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x00011ED9 File Offset: 0x000100D9
		// (set) Token: 0x0600045F RID: 1119 RVA: 0x00011EE1 File Offset: 0x000100E1
		internal Func<object, object, Type, object> Plan { get; set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x00011EEA File Offset: 0x000100EA
		// (set) Token: 0x06000461 RID: 1121 RVA: 0x00011EF2 File Offset: 0x000100F2
		internal Type ProjectedType { get; set; }

		// Token: 0x06000462 RID: 1122 RVA: 0x00011EFB File Offset: 0x000100FB
		internal object Run(ODataEntityMaterializer materializer, ODataEntry entry, Type expectedType)
		{
			return this.Plan(materializer, entry, expectedType);
		}
	}
}
