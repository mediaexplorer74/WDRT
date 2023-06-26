using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x02000088 RID: 136
	public sealed class TypeSegment : ODataPathSegment
	{
		// Token: 0x0600033B RID: 827 RVA: 0x0000B5B0 File Offset: 0x000097B0
		public TypeSegment(IEdmType edmType, IEdmEntitySet entitySet)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(edmType, "type");
			this.edmType = edmType;
			this.entitySet = entitySet;
			base.TargetEdmType = edmType;
			base.TargetEdmEntitySet = entitySet;
			if (entitySet != null)
			{
				UriParserErrorHelper.ThrowIfTypesUnrelated(edmType, entitySet.ElementType, "TypeSegments");
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600033C RID: 828 RVA: 0x0000B5FE File Offset: 0x000097FE
		public override IEdmType EdmType
		{
			get
			{
				return this.edmType;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000B606 File Offset: 0x00009806
		public IEdmEntitySet EntitySet
		{
			get
			{
				return this.entitySet;
			}
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000B60E File Offset: 0x0000980E
		public override T Translate<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000B622 File Offset: 0x00009822
		public override void Handle(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "translator");
			handler.Handle(this);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000B638 File Offset: 0x00009838
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			TypeSegment typeSegment = other as TypeSegment;
			return typeSegment != null && typeSegment.EdmType == this.EdmType;
		}

		// Token: 0x040000F8 RID: 248
		private readonly IEdmType edmType;

		// Token: 0x040000F9 RID: 249
		private readonly IEdmEntitySet entitySet;
	}
}
