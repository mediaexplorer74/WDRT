using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x020000AC RID: 172
	public sealed class ConvertNode : SingleValueNode
	{
		// Token: 0x06000406 RID: 1030 RVA: 0x0000D338 File Offset: 0x0000B538
		public ConvertNode(SingleValueNode source, IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(source, "source");
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			this.source = source;
			this.typeReference = typeReference;
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x0000D364 File Offset: 0x0000B564
		public SingleValueNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x0000D36C File Offset: 0x0000B56C
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.typeReference;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x0000D374 File Offset: 0x0000B574
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.Convert;
			}
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000D377 File Offset: 0x0000B577
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000153 RID: 339
		private readonly SingleValueNode source;

		// Token: 0x04000154 RID: 340
		private readonly IEdmTypeReference typeReference;
	}
}
