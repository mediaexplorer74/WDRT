using System;
using System.Data.Services.Client.Metadata;
using Microsoft.Data.Edm;
using Microsoft.Data.OData;

namespace System.Data.Services.Client.Materialization
{
	// Token: 0x02000054 RID: 84
	internal class ODataMaterializerContext : IODataMaterializerContext
	{
		// Token: 0x060002B3 RID: 691 RVA: 0x0000CEEC File Offset: 0x0000B0EC
		internal ODataMaterializerContext(ResponseInfo responseInfo)
		{
			this.ResponseInfo = responseInfo;
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000CEFB File Offset: 0x0000B0FB
		public bool IgnoreMissingProperties
		{
			get
			{
				return this.ResponseInfo.UndeclaredPropertyBehaviorKinds.HasFlag(ODataUndeclaredPropertyBehaviorKinds.IgnoreUndeclaredValueProperty) || this.ResponseInfo.UndeclaredPropertyBehaviorKinds.HasFlag(ODataUndeclaredPropertyBehaviorKinds.SupportUndeclaredValueProperty);
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000CF37 File Offset: 0x0000B137
		public ClientEdmModel Model
		{
			get
			{
				return this.ResponseInfo.Model;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000CF44 File Offset: 0x0000B144
		public DataServiceClientResponsePipelineConfiguration ResponsePipeline
		{
			get
			{
				return this.ResponseInfo.ResponsePipeline;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000CF51 File Offset: 0x0000B151
		public bool AutoNullPropagation
		{
			get
			{
				return this.ResponseInfo.AutoNullPropagation;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000CF5E File Offset: 0x0000B15E
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x0000CF66 File Offset: 0x0000B166
		private protected ResponseInfo ResponseInfo { protected get; private set; }

		// Token: 0x060002BA RID: 698 RVA: 0x0000CF6F File Offset: 0x0000B16F
		public ClientTypeAnnotation ResolveTypeForMaterialization(Type expectedType, string wireTypeName)
		{
			return this.ResponseInfo.TypeResolver.ResolveTypeForMaterialization(expectedType, wireTypeName);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000CF83 File Offset: 0x0000B183
		public IEdmType ResolveExpectedTypeForReading(Type expectedType)
		{
			return this.ResponseInfo.TypeResolver.ResolveExpectedTypeForReading(expectedType);
		}
	}
}
