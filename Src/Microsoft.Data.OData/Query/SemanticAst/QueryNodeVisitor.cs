using System;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x02000090 RID: 144
	public abstract class QueryNodeVisitor<T>
	{
		// Token: 0x06000353 RID: 851 RVA: 0x0000B898 File Offset: 0x00009A98
		public virtual T Visit(AllNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000B89F File Offset: 0x00009A9F
		public virtual T Visit(AnyNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000B8A6 File Offset: 0x00009AA6
		public virtual T Visit(BinaryOperatorNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000B8AD File Offset: 0x00009AAD
		public virtual T Visit(CollectionNavigationNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000B8B4 File Offset: 0x00009AB4
		public virtual T Visit(CollectionPropertyAccessNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000B8BB File Offset: 0x00009ABB
		public virtual T Visit(ConstantNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000B8C2 File Offset: 0x00009AC2
		public virtual T Visit(ConvertNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000B8C9 File Offset: 0x00009AC9
		public virtual T Visit(EntityCollectionCastNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000B8D0 File Offset: 0x00009AD0
		public virtual T Visit(EntityRangeVariableReferenceNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000B8D7 File Offset: 0x00009AD7
		public virtual T Visit(NonentityRangeVariableReferenceNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000B8DE File Offset: 0x00009ADE
		public virtual T Visit(SingleEntityCastNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000B8E5 File Offset: 0x00009AE5
		public virtual T Visit(SingleNavigationNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000B8EC File Offset: 0x00009AEC
		public virtual T Visit(SingleEntityFunctionCallNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000B8F3 File Offset: 0x00009AF3
		public virtual T Visit(SingleValueFunctionCallNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000B8FA File Offset: 0x00009AFA
		public virtual T Visit(EntityCollectionFunctionCallNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000B901 File Offset: 0x00009B01
		public virtual T Visit(CollectionFunctionCallNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000B908 File Offset: 0x00009B08
		public virtual T Visit(SingleValueOpenPropertyAccessNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000B90F File Offset: 0x00009B0F
		public virtual T Visit(SingleValuePropertyAccessNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000B916 File Offset: 0x00009B16
		public virtual T Visit(UnaryOperatorNode nodeIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000B91D File Offset: 0x00009B1D
		public virtual T Visit(NamedFunctionParameterNode nodeIn)
		{
			throw new NotImplementedException();
		}
	}
}
