using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x02000082 RID: 130
	public sealed class NonentityRangeVariableReferenceNode : SingleValueNode
	{
		// Token: 0x06000312 RID: 786 RVA: 0x0000B07C File Offset: 0x0000927C
		public NonentityRangeVariableReferenceNode(string name, NonentityRangeVariable rangeVariable)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "name");
			ExceptionUtils.CheckArgumentNotNull<NonentityRangeVariable>(rangeVariable, "rangeVariable");
			this.name = name;
			this.typeReference = rangeVariable.TypeReference;
			this.rangeVariable = rangeVariable;
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000313 RID: 787 RVA: 0x0000B0B4 File Offset: 0x000092B4
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000314 RID: 788 RVA: 0x0000B0BC File Offset: 0x000092BC
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.typeReference;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0000B0C4 File Offset: 0x000092C4
		public NonentityRangeVariable RangeVariable
		{
			get
			{
				return this.rangeVariable;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0000B0CC File Offset: 0x000092CC
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.NonentityRangeVariableReference;
			}
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000B0CF File Offset: 0x000092CF
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040000E2 RID: 226
		private readonly string name;

		// Token: 0x040000E3 RID: 227
		private readonly IEdmTypeReference typeReference;

		// Token: 0x040000E4 RID: 228
		private readonly NonentityRangeVariable rangeVariable;
	}
}
