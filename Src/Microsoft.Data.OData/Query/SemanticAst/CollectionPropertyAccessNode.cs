using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x02000086 RID: 134
	public sealed class CollectionPropertyAccessNode : CollectionNode
	{
		// Token: 0x06000331 RID: 817 RVA: 0x0000B4C0 File Offset: 0x000096C0
		public CollectionPropertyAccessNode(SingleValueNode source, IEdmProperty property)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(source, "source");
			ExceptionUtils.CheckArgumentNotNull<IEdmProperty>(property, "property");
			if (property.PropertyKind != EdmPropertyKind.Structural)
			{
				throw new ArgumentException(Strings.Nodes_PropertyAccessShouldBeNonEntityProperty(property.Name));
			}
			if (!property.Type.IsCollection())
			{
				throw new ArgumentException(Strings.Nodes_PropertyAccessTypeMustBeCollection(property.Name));
			}
			this.source = source;
			this.property = property;
			this.collectionTypeReference = property.Type.AsCollection();
			this.itemType = this.collectionTypeReference.ElementType();
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000B550 File Offset: 0x00009750
		public SingleValueNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000B558 File Offset: 0x00009758
		public IEdmProperty Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000B560 File Offset: 0x00009760
		public override IEdmTypeReference ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0000B568 File Offset: 0x00009768
		public override IEdmCollectionTypeReference CollectionType
		{
			get
			{
				return this.collectionTypeReference;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0000B570 File Offset: 0x00009770
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.CollectionPropertyAccess;
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000B573 File Offset: 0x00009773
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040000F3 RID: 243
		private readonly SingleValueNode source;

		// Token: 0x040000F4 RID: 244
		private readonly IEdmProperty property;

		// Token: 0x040000F5 RID: 245
		private readonly IEdmTypeReference itemType;

		// Token: 0x040000F6 RID: 246
		private readonly IEdmCollectionTypeReference collectionTypeReference;
	}
}
