using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x02000072 RID: 114
	public sealed class OpenPropertySegment : ODataPathSegment
	{
		// Token: 0x060002B5 RID: 693 RVA: 0x0000A744 File Offset: 0x00008944
		public OpenPropertySegment(string propertyName)
		{
			this.propertyName = propertyName;
			base.Identifier = propertyName;
			base.TargetEdmType = null;
			base.TargetKind = RequestTargetKind.OpenProperty;
			base.SingleResult = true;
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000A770 File Offset: 0x00008970
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000A778 File Offset: 0x00008978
		public override IEdmType EdmType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000A77B File Offset: 0x0000897B
		public override T Translate<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000A78F File Offset: 0x0000898F
		public override void Handle(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "translator");
			handler.Handle(this);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000A7A4 File Offset: 0x000089A4
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			OpenPropertySegment openPropertySegment = other as OpenPropertySegment;
			return openPropertySegment != null && openPropertySegment.PropertyName == this.PropertyName;
		}

		// Token: 0x040000BC RID: 188
		private readonly string propertyName;
	}
}
