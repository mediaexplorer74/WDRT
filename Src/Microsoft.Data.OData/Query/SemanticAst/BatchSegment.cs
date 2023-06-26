using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x02000065 RID: 101
	public sealed class BatchSegment : ODataPathSegment
	{
		// Token: 0x0600027A RID: 634 RVA: 0x0000A11A File Offset: 0x0000831A
		private BatchSegment()
		{
			base.Identifier = "$batch";
			base.TargetKind = RequestTargetKind.Batch;
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000A134 File Offset: 0x00008334
		public override IEdmType EdmType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000A137 File Offset: 0x00008337
		public override T Translate<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000A14B File Offset: 0x0000834B
		public override void Handle(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "translator");
			handler.Handle(this);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000A15F File Offset: 0x0000835F
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			return other is BatchSegment;
		}

		// Token: 0x040000A8 RID: 168
		public static readonly BatchSegment Instance = new BatchSegment();
	}
}
