using System;
using Microsoft.Data.OData.Query.SyntacticAst;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x020000A9 RID: 169
	internal sealed class BinaryOperatorUriBuilder
	{
		// Token: 0x060003EE RID: 1006 RVA: 0x0000C599 File Offset: 0x0000A799
		public BinaryOperatorUriBuilder(ODataUriBuilder builder)
		{
			this.builder = builder;
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000C5A8 File Offset: 0x0000A7A8
		public void Write(BinaryOperatorToken binary)
		{
			this.Write(false, binary);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000C5B4 File Offset: 0x0000A7B4
		private static bool NeedParenthesesLeft(BinaryOperator currentOperator, BinaryOperatorToken leftSubtree)
		{
			BinaryOperator @operator = BinaryOperator.GetOperator(leftSubtree.OperatorKind);
			return @operator.Precedence < currentOperator.Precedence;
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000C5DC File Offset: 0x0000A7DC
		private static bool NeedParenthesesRight(BinaryOperator currentOperator, BinaryOperatorToken rightSubtree)
		{
			BinaryOperator @operator = BinaryOperator.GetOperator(rightSubtree.OperatorKind);
			return currentOperator.Precedence >= @operator.Precedence && (currentOperator.Precedence > @operator.Precedence || currentOperator.NeedParenEvenWhenTheSame);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000C61C File Offset: 0x0000A81C
		private void Write(bool needParenthesis, BinaryOperatorToken binary)
		{
			if (needParenthesis)
			{
				this.builder.Append("(");
			}
			BinaryOperator @operator = BinaryOperator.GetOperator(binary.OperatorKind);
			BinaryOperatorToken binaryOperatorToken = binary.Left as BinaryOperatorToken;
			if (binaryOperatorToken != null)
			{
				this.Write(BinaryOperatorUriBuilder.NeedParenthesesLeft(@operator, binaryOperatorToken), binaryOperatorToken);
			}
			else
			{
				this.builder.WriteQuery(binary.Left);
			}
			this.builder.Append("%20");
			this.builder.Append(@operator.Text);
			this.builder.Append("%20");
			BinaryOperatorToken binaryOperatorToken2 = binary.Right as BinaryOperatorToken;
			if (binaryOperatorToken2 != null)
			{
				this.Write(BinaryOperatorUriBuilder.NeedParenthesesRight(@operator, binaryOperatorToken2), binaryOperatorToken2);
			}
			else
			{
				this.builder.WriteQuery(binary.Right);
			}
			if (needParenthesis)
			{
				this.builder.Append(")");
			}
		}

		// Token: 0x0400014E RID: 334
		private readonly ODataUriBuilder builder;
	}
}
