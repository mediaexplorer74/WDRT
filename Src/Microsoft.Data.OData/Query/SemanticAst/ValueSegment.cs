using System;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x0200008D RID: 141
	public sealed class ValueSegment : ODataPathSegment
	{
		// Token: 0x0600034B RID: 843 RVA: 0x0000B728 File Offset: 0x00009928
		public ValueSegment(IEdmType previousType)
		{
			base.Identifier = "$value";
			base.SingleResult = true;
			if (previousType is IEdmCollectionType)
			{
				throw new ODataException(Strings.PathParser_CannotUseValueOnCollection);
			}
			if (previousType is IEdmEntityType)
			{
				this.edmType = EdmCoreModel.Instance.GetStream(false).Definition;
				return;
			}
			this.edmType = previousType;
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0000B786 File Offset: 0x00009986
		public override IEdmType EdmType
		{
			get
			{
				return this.edmType;
			}
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000B78E File Offset: 0x0000998E
		public override T Translate<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000B7A2 File Offset: 0x000099A2
		public override void Handle(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "translator");
			handler.Handle(this);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000B7B8 File Offset: 0x000099B8
		internal override bool Equals(ODataPathSegment other)
		{
			ValueSegment valueSegment = other as ValueSegment;
			return valueSegment != null && valueSegment.EdmType == this.edmType;
		}

		// Token: 0x040000FD RID: 253
		private readonly IEdmType edmType;
	}
}
