using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x0200006E RID: 110
	public sealed class MetadataSegment : ODataPathSegment
	{
		// Token: 0x060002A3 RID: 675 RVA: 0x0000A4F7 File Offset: 0x000086F7
		private MetadataSegment()
		{
			base.Identifier = "$metadata";
			base.TargetKind = RequestTargetKind.Metadata;
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000A511 File Offset: 0x00008711
		public override IEdmType EdmType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000A514 File Offset: 0x00008714
		public override T Translate<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000A528 File Offset: 0x00008728
		public override void Handle(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "translator");
			handler.Handle(this);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000A53C File Offset: 0x0000873C
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			return other is MetadataSegment;
		}

		// Token: 0x040000B9 RID: 185
		public static readonly MetadataSegment Instance = new MetadataSegment();
	}
}
