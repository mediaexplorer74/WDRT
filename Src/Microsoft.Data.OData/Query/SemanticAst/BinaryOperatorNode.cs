using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x020000AD RID: 173
	public sealed class BinaryOperatorNode : SingleValueNode
	{
		// Token: 0x0600040B RID: 1035 RVA: 0x0000D38C File Offset: 0x0000B58C
		public BinaryOperatorNode(BinaryOperatorKind operatorKind, SingleValueNode left, SingleValueNode right)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(left, "left");
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(right, "right");
			this.operatorKind = operatorKind;
			this.left = left;
			this.right = right;
			if (this.Left == null || this.Right == null || this.Left.TypeReference == null || this.Right.TypeReference == null)
			{
				this.typeReference = null;
				return;
			}
			if (!this.Left.TypeReference.Definition.IsEquivalentTo(this.Right.TypeReference.Definition))
			{
				throw new ODataException(Strings.BinaryOperatorQueryNode_OperandsMustHaveSameTypes(this.Left.TypeReference.ODataFullName(), this.Right.TypeReference.ODataFullName()));
			}
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference = this.Left.TypeReference.AsPrimitive();
			this.typeReference = QueryNodeUtils.GetBinaryOperatorResultType(edmPrimitiveTypeReference, this.OperatorKind);
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x0000D471 File Offset: 0x0000B671
		public BinaryOperatorKind OperatorKind
		{
			get
			{
				return this.operatorKind;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x0000D479 File Offset: 0x0000B679
		public SingleValueNode Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0000D481 File Offset: 0x0000B681
		public SingleValueNode Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x0000D489 File Offset: 0x0000B689
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.typeReference;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x0000D491 File Offset: 0x0000B691
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.BinaryOperator;
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000D494 File Offset: 0x0000B694
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000155 RID: 341
		private readonly BinaryOperatorKind operatorKind;

		// Token: 0x04000156 RID: 342
		private readonly SingleValueNode left;

		// Token: 0x04000157 RID: 343
		private readonly SingleValueNode right;

		// Token: 0x04000158 RID: 344
		private IEdmTypeReference typeReference;
	}
}
