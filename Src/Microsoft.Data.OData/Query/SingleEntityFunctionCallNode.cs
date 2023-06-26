using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Query.SemanticAst;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x02000085 RID: 133
	public sealed class SingleEntityFunctionCallNode : SingleEntityNode
	{
		// Token: 0x06000326 RID: 806 RVA: 0x0000B3F3 File Offset: 0x000095F3
		public SingleEntityFunctionCallNode(string name, IEnumerable<QueryNode> arguments, IEdmEntityTypeReference entityTypeReference, IEdmEntitySet entitySet)
			: this(name, null, arguments, entityTypeReference, entitySet, null)
		{
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000B404 File Offset: 0x00009604
		public SingleEntityFunctionCallNode(string name, IEnumerable<IEdmFunctionImport> functionImports, IEnumerable<QueryNode> arguments, IEdmEntityTypeReference entityTypeReference, IEdmEntitySet entitySet, QueryNode source)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "name");
			ExceptionUtils.CheckArgumentNotNull<IEdmEntityTypeReference>(entityTypeReference, "typeReference");
			this.name = name;
			this.functionImports = new ReadOnlyCollection<IEdmFunctionImport>((functionImports != null) ? functionImports.ToList<IEdmFunctionImport>() : new List<IEdmFunctionImport>());
			this.arguments = arguments;
			this.entityTypeReference = entityTypeReference;
			this.entitySet = entitySet;
			this.source = source;
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000B46F File Offset: 0x0000966F
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000329 RID: 809 RVA: 0x0000B477 File Offset: 0x00009677
		public IEnumerable<IEdmFunctionImport> FunctionImports
		{
			get
			{
				return this.functionImports;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000B47F File Offset: 0x0000967F
		public IEnumerable<QueryNode> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600032B RID: 811 RVA: 0x0000B487 File Offset: 0x00009687
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0000B48F File Offset: 0x0000968F
		public override IEdmEntitySet EntitySet
		{
			get
			{
				return this.entitySet;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000B497 File Offset: 0x00009697
		public override IEdmEntityTypeReference EntityTypeReference
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000B49F File Offset: 0x0000969F
		public QueryNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000B4A7 File Offset: 0x000096A7
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.SingleEntityFunctionCall;
			}
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000B4AB File Offset: 0x000096AB
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040000ED RID: 237
		private readonly string name;

		// Token: 0x040000EE RID: 238
		private readonly ReadOnlyCollection<IEdmFunctionImport> functionImports;

		// Token: 0x040000EF RID: 239
		private readonly IEnumerable<QueryNode> arguments;

		// Token: 0x040000F0 RID: 240
		private readonly IEdmEntityTypeReference entityTypeReference;

		// Token: 0x040000F1 RID: 241
		private readonly IEdmEntitySet entitySet;

		// Token: 0x040000F2 RID: 242
		private readonly QueryNode source;
	}
}
