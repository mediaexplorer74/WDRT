using System;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x02000068 RID: 104
	public sealed class CountSegment : ODataPathSegment
	{
		// Token: 0x06000283 RID: 643 RVA: 0x0000A1AB File Offset: 0x000083AB
		private CountSegment()
		{
			base.Identifier = "$count";
			base.SingleResult = true;
			base.TargetKind = RequestTargetKind.PrimitiveValue;
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000A1CC File Offset: 0x000083CC
		public override IEdmType EdmType
		{
			get
			{
				return EdmCoreModel.Instance.GetInt32(false).Definition;
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000A1DE File Offset: 0x000083DE
		public override T Translate<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000A1F2 File Offset: 0x000083F2
		public override void Handle(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "translator");
			handler.Handle(this);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000A206 File Offset: 0x00008406
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			return other is CountSegment;
		}

		// Token: 0x040000AA RID: 170
		public static readonly CountSegment Instance = new CountSegment();
	}
}
