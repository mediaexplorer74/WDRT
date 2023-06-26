using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;
using Microsoft.Data.OData.Query.SemanticAst;
using Microsoft.Data.OData.Query.SyntacticAst;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x020000E1 RID: 225
	internal sealed class UnaryOperatorBinder
	{
		// Token: 0x0600057C RID: 1404 RVA: 0x00013634 File Offset: 0x00011834
		internal UnaryOperatorBinder(Func<QueryToken, QueryNode> bindMethod)
		{
			this.bindMethod = bindMethod;
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00013644 File Offset: 0x00011844
		internal QueryNode BindUnaryOperator(UnaryOperatorToken unaryOperatorToken)
		{
			ExceptionUtils.CheckArgumentNotNull<UnaryOperatorToken>(unaryOperatorToken, "unaryOperatorToken");
			SingleValueNode singleValueNode = this.GetOperandFromToken(unaryOperatorToken);
			IEdmTypeReference edmTypeReference = UnaryOperatorBinder.PromoteOperandType(singleValueNode, unaryOperatorToken.OperatorKind);
			singleValueNode = MetadataBindingUtils.ConvertToTypeIfNeeded(singleValueNode, edmTypeReference);
			return new UnaryOperatorNode(unaryOperatorToken.OperatorKind, singleValueNode);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00013688 File Offset: 0x00011888
		private static IEdmTypeReference PromoteOperandType(SingleValueNode operand, UnaryOperatorKind unaryOperatorKind)
		{
			IEdmTypeReference typeReference = operand.TypeReference;
			if (!TypePromotionUtils.PromoteOperandType(unaryOperatorKind, ref typeReference))
			{
				string text = ((operand.TypeReference == null) ? "<null>" : operand.TypeReference.ODataFullName());
				throw new ODataException(Strings.MetadataBinder_IncompatibleOperandError(text, unaryOperatorKind));
			}
			return typeReference;
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x000136D4 File Offset: 0x000118D4
		private SingleValueNode GetOperandFromToken(UnaryOperatorToken unaryOperatorToken)
		{
			SingleValueNode singleValueNode = this.bindMethod(unaryOperatorToken.Operand) as SingleValueNode;
			if (singleValueNode == null)
			{
				throw new ODataException(Strings.MetadataBinder_UnaryOperatorOperandNotSingleValue(unaryOperatorToken.OperatorKind.ToString()));
			}
			return singleValueNode;
		}

		// Token: 0x04000257 RID: 599
		private readonly Func<QueryToken, QueryNode> bindMethod;
	}
}
