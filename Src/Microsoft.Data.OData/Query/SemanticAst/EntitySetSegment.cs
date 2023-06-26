using System;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x0200006A RID: 106
	public sealed class EntitySetSegment : ODataPathSegment
	{
		// Token: 0x0600028A RID: 650 RVA: 0x0000A25C File Offset: 0x0000845C
		public EntitySetSegment(IEdmEntitySet entitySet)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmEntitySet>(entitySet, "entitySet");
			this.entitySet = entitySet;
			this.type = new EdmCollectionType(new EdmEntityTypeReference(this.entitySet.ElementType, false));
			base.TargetEdmEntitySet = entitySet;
			base.TargetEdmType = entitySet.ElementType;
			base.TargetKind = RequestTargetKind.Resource;
			base.SingleResult = false;
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000A2BE File Offset: 0x000084BE
		public IEdmEntitySet EntitySet
		{
			get
			{
				return this.entitySet;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000A2C6 File Offset: 0x000084C6
		public override IEdmType EdmType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000A2CE File Offset: 0x000084CE
		public override T Translate<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000A2E2 File Offset: 0x000084E2
		public override void Handle(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000A2F8 File Offset: 0x000084F8
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			EntitySetSegment entitySetSegment = other as EntitySetSegment;
			return entitySetSegment != null && entitySetSegment.EntitySet == this.EntitySet;
		}

		// Token: 0x040000AB RID: 171
		private readonly IEdmEntitySet entitySet;

		// Token: 0x040000AC RID: 172
		private readonly IEdmType type;
	}
}
