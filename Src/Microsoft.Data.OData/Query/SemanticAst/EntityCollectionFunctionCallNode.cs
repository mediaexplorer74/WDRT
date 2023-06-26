using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x02000046 RID: 70
	public sealed class EntityCollectionFunctionCallNode : EntityCollectionNode
	{
		// Token: 0x060001BD RID: 445 RVA: 0x00007700 File Offset: 0x00005900
		public EntityCollectionFunctionCallNode(string name, IEnumerable<IEdmFunctionImport> functionImports, IEnumerable<QueryNode> parameters, IEdmCollectionTypeReference returnedCollectionTypeReference, IEdmEntitySet entitySet, QueryNode source)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "name");
			ExceptionUtils.CheckArgumentNotNull<IEdmCollectionTypeReference>(returnedCollectionTypeReference, "returnedCollectionTypeReference");
			this.name = name;
			this.functionImports = new ReadOnlyCollection<IEdmFunctionImport>((functionImports == null) ? new List<IEdmFunctionImport>() : functionImports.ToList<IEdmFunctionImport>());
			this.parameters = new ReadOnlyCollection<QueryNode>((parameters == null) ? new List<QueryNode>() : parameters.ToList<QueryNode>());
			this.returnedCollectionTypeReference = returnedCollectionTypeReference;
			this.entitySet = entitySet;
			this.entityTypeReference = returnedCollectionTypeReference.ElementType().AsEntityOrNull();
			if (this.entityTypeReference == null)
			{
				throw new ArgumentException(Strings.Nodes_EntityCollectionFunctionCallNode_ItemTypeMustBeAnEntity);
			}
			this.source = source;
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001BE RID: 446 RVA: 0x000077A4 File Offset: 0x000059A4
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001BF RID: 447 RVA: 0x000077AC File Offset: 0x000059AC
		public IEnumerable<IEdmFunctionImport> FunctionImports
		{
			get
			{
				return this.functionImports;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x000077B4 File Offset: 0x000059B4
		public IEnumerable<QueryNode> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x000077BC File Offset: 0x000059BC
		public override IEdmTypeReference ItemType
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x000077C4 File Offset: 0x000059C4
		public override IEdmCollectionTypeReference CollectionType
		{
			get
			{
				return this.returnedCollectionTypeReference;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x000077CC File Offset: 0x000059CC
		public override IEdmEntityTypeReference EntityItemType
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x000077D4 File Offset: 0x000059D4
		public override IEdmEntitySet EntitySet
		{
			get
			{
				return this.entitySet;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x000077DC File Offset: 0x000059DC
		public QueryNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x000077E4 File Offset: 0x000059E4
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.EntityCollectionFunctionCall;
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000077E8 File Offset: 0x000059E8
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000075 RID: 117
		private readonly string name;

		// Token: 0x04000076 RID: 118
		private readonly ReadOnlyCollection<IEdmFunctionImport> functionImports;

		// Token: 0x04000077 RID: 119
		private readonly ReadOnlyCollection<QueryNode> parameters;

		// Token: 0x04000078 RID: 120
		private readonly IEdmEntityTypeReference entityTypeReference;

		// Token: 0x04000079 RID: 121
		private readonly IEdmCollectionTypeReference returnedCollectionTypeReference;

		// Token: 0x0400007A RID: 122
		private readonly IEdmEntitySet entitySet;

		// Token: 0x0400007B RID: 123
		private readonly QueryNode source;
	}
}
