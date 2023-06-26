using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x0200001E RID: 30
	public sealed class CollectionFunctionCallNode : CollectionNode
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x00003F6C File Offset: 0x0000216C
		public CollectionFunctionCallNode(string name, IEnumerable<IEdmFunctionImport> functionImports, IEnumerable<QueryNode> parameters, IEdmCollectionTypeReference returnedCollectionType, QueryNode source)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "name");
			ExceptionUtils.CheckArgumentNotNull<IEdmCollectionTypeReference>(returnedCollectionType, "returnedCollectionType");
			this.name = name;
			this.functionImports = new ReadOnlyCollection<IEdmFunctionImport>((functionImports == null) ? new List<IEdmFunctionImport>() : functionImports.ToList<IEdmFunctionImport>());
			this.parameters = new ReadOnlyCollection<QueryNode>((parameters == null) ? new List<QueryNode>() : parameters.ToList<QueryNode>());
			this.returnedCollectionType = returnedCollectionType;
			this.itemType = returnedCollectionType.ElementType();
			if (!this.itemType.IsPrimitive() && !this.itemType.IsComplex())
			{
				throw new ArgumentException(Strings.Nodes_CollectionFunctionCallNode_ItemTypeMustBePrimitiveOrComplex);
			}
			this.source = source;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00004015 File Offset: 0x00002215
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x0000401D File Offset: 0x0000221D
		public IEnumerable<IEdmFunctionImport> FunctionImports
		{
			get
			{
				return this.functionImports;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00004025 File Offset: 0x00002225
		public IEnumerable<QueryNode> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x0000402D File Offset: 0x0000222D
		public override IEdmTypeReference ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00004035 File Offset: 0x00002235
		public override IEdmCollectionTypeReference CollectionType
		{
			get
			{
				return this.returnedCollectionType;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000BA RID: 186 RVA: 0x0000403D File Offset: 0x0000223D
		public QueryNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00004045 File Offset: 0x00002245
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.CollectionFunctionCall;
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004049 File Offset: 0x00002249
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000048 RID: 72
		private readonly string name;

		// Token: 0x04000049 RID: 73
		private readonly ReadOnlyCollection<IEdmFunctionImport> functionImports;

		// Token: 0x0400004A RID: 74
		private readonly ReadOnlyCollection<QueryNode> parameters;

		// Token: 0x0400004B RID: 75
		private readonly IEdmTypeReference itemType;

		// Token: 0x0400004C RID: 76
		private readonly IEdmCollectionTypeReference returnedCollectionType;

		// Token: 0x0400004D RID: 77
		private readonly QueryNode source;
	}
}
