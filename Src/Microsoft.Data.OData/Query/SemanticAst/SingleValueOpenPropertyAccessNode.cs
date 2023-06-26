using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x020001EC RID: 492
	public sealed class SingleValueOpenPropertyAccessNode : SingleValueNode
	{
		// Token: 0x06000F2D RID: 3885 RVA: 0x00036A9E File Offset: 0x00034C9E
		public SingleValueOpenPropertyAccessNode(SingleValueNode source, string openPropertyName)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(source, "source");
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(openPropertyName, "openPropertyName");
			this.name = openPropertyName;
			this.source = source;
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000F2E RID: 3886 RVA: 0x00036ACA File Offset: 0x00034CCA
		public SingleValueNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000F2F RID: 3887 RVA: 0x00036AD2 File Offset: 0x00034CD2
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000F30 RID: 3888 RVA: 0x00036ADA File Offset: 0x00034CDA
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000F31 RID: 3889 RVA: 0x00036ADD File Offset: 0x00034CDD
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.SingleValueOpenPropertyAccess;
			}
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x00036AE1 File Offset: 0x00034CE1
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000538 RID: 1336
		private readonly SingleValueNode source;

		// Token: 0x04000539 RID: 1337
		private readonly string name;
	}
}
