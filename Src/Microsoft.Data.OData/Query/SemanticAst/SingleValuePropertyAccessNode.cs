using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x020000C0 RID: 192
	public sealed class SingleValuePropertyAccessNode : SingleValueNode
	{
		// Token: 0x060004A6 RID: 1190 RVA: 0x0000FDE4 File Offset: 0x0000DFE4
		public SingleValuePropertyAccessNode(SingleValueNode source, IEdmProperty property)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(source, "source");
			ExceptionUtils.CheckArgumentNotNull<IEdmProperty>(property, "property");
			if (property.PropertyKind != EdmPropertyKind.Structural)
			{
				throw new ArgumentException(Strings.Nodes_PropertyAccessShouldBeNonEntityProperty(property.Name));
			}
			if (property.Type.IsCollection())
			{
				throw new ArgumentException(Strings.Nodes_PropertyAccessTypeShouldNotBeCollection(property.Name));
			}
			this.source = source;
			this.property = property;
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x0000FE52 File Offset: 0x0000E052
		public SingleValueNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x0000FE5A File Offset: 0x0000E05A
		public IEdmProperty Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x0000FE62 File Offset: 0x0000E062
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.Property.Type;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x0000FE6F File Offset: 0x0000E06F
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.SingleValuePropertyAccess;
			}
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0000FE72 File Offset: 0x0000E072
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x04000195 RID: 405
		private readonly SingleValueNode source;

		// Token: 0x04000196 RID: 406
		private readonly IEdmProperty property;
	}
}
