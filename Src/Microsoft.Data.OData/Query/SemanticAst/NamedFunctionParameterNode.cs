using System;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x02000018 RID: 24
	public class NamedFunctionParameterNode : QueryNode
	{
		// Token: 0x0600008C RID: 140 RVA: 0x00003741 File Offset: 0x00001941
		public NamedFunctionParameterNode(string name, QueryNode value)
		{
			this.name = name;
			this.value = value;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00003757 File Offset: 0x00001957
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600008E RID: 142 RVA: 0x0000375F File Offset: 0x0000195F
		public QueryNode Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00003767 File Offset: 0x00001967
		public override QueryNodeKind Kind
		{
			get
			{
				return (QueryNodeKind)this.InternalKind;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000090 RID: 144 RVA: 0x0000376F File Offset: 0x0000196F
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.NamedFunctionParameter;
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003773 File Offset: 0x00001973
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000037 RID: 55
		private readonly string name;

		// Token: 0x04000038 RID: 56
		private readonly QueryNode value;
	}
}
