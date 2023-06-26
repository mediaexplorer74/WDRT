using System;
using System.Data.Services.Client.Metadata;
using Microsoft.Data.Edm;

namespace System.Data.Services.Client.Materialization
{
	// Token: 0x02000053 RID: 83
	internal interface IODataMaterializerContext
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002AD RID: 685
		bool IgnoreMissingProperties { get; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002AE RID: 686
		ClientEdmModel Model { get; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002AF RID: 687
		DataServiceClientResponsePipelineConfiguration ResponsePipeline { get; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002B0 RID: 688
		bool AutoNullPropagation { get; }

		// Token: 0x060002B1 RID: 689
		ClientTypeAnnotation ResolveTypeForMaterialization(Type expectedType, string readerTypeName);

		// Token: 0x060002B2 RID: 690
		IEdmType ResolveExpectedTypeForReading(Type clientClrType);
	}
}
