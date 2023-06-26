using System;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x02000097 RID: 151
	internal abstract class SyntacticTreeVisitor<T> : ISyntacticTreeVisitor<T>
	{
		// Token: 0x0600038F RID: 911 RVA: 0x0000BBB2 File Offset: 0x00009DB2
		public virtual T Visit(AllToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000BBB9 File Offset: 0x00009DB9
		public virtual T Visit(AnyToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000BBC0 File Offset: 0x00009DC0
		public virtual T Visit(BinaryOperatorToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000BBC7 File Offset: 0x00009DC7
		public virtual T Visit(DottedIdentifierToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000BBCE File Offset: 0x00009DCE
		public virtual T Visit(ExpandToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000BBD5 File Offset: 0x00009DD5
		public virtual T Visit(ExpandTermToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000BBDC File Offset: 0x00009DDC
		public virtual T Visit(FunctionCallToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000BBE3 File Offset: 0x00009DE3
		public virtual T Visit(LiteralToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000BBEA File Offset: 0x00009DEA
		public virtual T Visit(LambdaToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000BBF1 File Offset: 0x00009DF1
		public virtual T Visit(InnerPathToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000BBF8 File Offset: 0x00009DF8
		public virtual T Visit(OrderByToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000BBFF File Offset: 0x00009DFF
		public virtual T Visit(EndPathToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000BC06 File Offset: 0x00009E06
		public virtual T Visit(CustomQueryOptionToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000BC0D File Offset: 0x00009E0D
		public virtual T Visit(RangeVariableToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000BC14 File Offset: 0x00009E14
		public virtual T Visit(SelectToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000BC1B File Offset: 0x00009E1B
		public virtual T Visit(StarToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000BC22 File Offset: 0x00009E22
		public virtual T Visit(UnaryOperatorToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000BC29 File Offset: 0x00009E29
		public virtual T Visit(FunctionParameterToken tokenIn)
		{
			throw new NotImplementedException();
		}
	}
}
