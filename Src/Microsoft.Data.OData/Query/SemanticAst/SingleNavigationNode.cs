using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x020000E2 RID: 226
	public sealed class SingleNavigationNode : SingleEntityNode
	{
		// Token: 0x06000580 RID: 1408 RVA: 0x00013718 File Offset: 0x00011918
		public SingleNavigationNode(IEdmNavigationProperty navigationProperty, SingleEntityNode source)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmNavigationProperty>(navigationProperty, "navigationProperty");
			ExceptionUtils.CheckArgumentNotNull<SingleEntityNode>(source, "source");
			EdmMultiplicity edmMultiplicity = navigationProperty.TargetMultiplicityTemporary();
			if (edmMultiplicity != EdmMultiplicity.One && edmMultiplicity != EdmMultiplicity.ZeroOrOne)
			{
				throw new ArgumentException(Strings.Nodes_CollectionNavigationNode_MustHaveSingleMultiplicity);
			}
			this.source = source;
			this.navigationProperty = navigationProperty;
			this.entityTypeReference = (IEdmEntityTypeReference)this.NavigationProperty.Type;
			this.entitySet = ((source.EntitySet != null) ? source.EntitySet.FindNavigationTarget(navigationProperty) : null);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0001379C File Offset: 0x0001199C
		public SingleNavigationNode(IEdmNavigationProperty navigationProperty, IEdmEntitySet sourceSet)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmNavigationProperty>(navigationProperty, "navigationProperty");
			EdmMultiplicity edmMultiplicity = navigationProperty.TargetMultiplicityTemporary();
			if (edmMultiplicity != EdmMultiplicity.One && edmMultiplicity != EdmMultiplicity.ZeroOrOne)
			{
				throw new ArgumentException(Strings.Nodes_CollectionNavigationNode_MustHaveSingleMultiplicity);
			}
			this.navigationProperty = navigationProperty;
			this.entityTypeReference = (IEdmEntityTypeReference)this.NavigationProperty.Type;
			this.entitySet = ((sourceSet != null) ? sourceSet.FindNavigationTarget(navigationProperty) : null);
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x00013804 File Offset: 0x00011A04
		public SingleEntityNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x0001380C File Offset: 0x00011A0C
		public IEdmNavigationProperty NavigationProperty
		{
			get
			{
				return this.navigationProperty;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x00013814 File Offset: 0x00011A14
		public EdmMultiplicity TargetMultiplicity
		{
			get
			{
				return this.NavigationProperty.TargetMultiplicityTemporary();
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x00013821 File Offset: 0x00011A21
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x00013829 File Offset: 0x00011A29
		public override IEdmEntityTypeReference EntityTypeReference
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x00013831 File Offset: 0x00011A31
		public override IEdmEntitySet EntitySet
		{
			get
			{
				return this.entitySet;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x00013839 File Offset: 0x00011A39
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.SingleNavigationNode;
			}
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0001383D File Offset: 0x00011A3D
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000258 RID: 600
		private readonly IEdmEntitySet entitySet;

		// Token: 0x04000259 RID: 601
		private readonly SingleEntityNode source;

		// Token: 0x0400025A RID: 602
		private readonly IEdmNavigationProperty navigationProperty;

		// Token: 0x0400025B RID: 603
		private readonly IEdmEntityTypeReference entityTypeReference;
	}
}
