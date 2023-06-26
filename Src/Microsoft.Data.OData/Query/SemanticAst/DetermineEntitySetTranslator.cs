using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x02000020 RID: 32
	internal sealed class DetermineEntitySetTranslator : PathSegmentTranslator<IEdmEntitySet>
	{
		// Token: 0x060000CB RID: 203 RVA: 0x000040C0 File Offset: 0x000022C0
		public override IEdmEntitySet Translate(NavigationPropertyLinkSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<NavigationPropertyLinkSegment>(segment, "segment");
			return segment.EntitySet;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000040D3 File Offset: 0x000022D3
		public override IEdmEntitySet Translate(TypeSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<TypeSegment>(segment, "segment");
			return segment.EntitySet;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000040E6 File Offset: 0x000022E6
		public override IEdmEntitySet Translate(NavigationPropertySegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<NavigationPropertySegment>(segment, "segment");
			return segment.EntitySet;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000040F9 File Offset: 0x000022F9
		public override IEdmEntitySet Translate(EntitySetSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<EntitySetSegment>(segment, "segment");
			return segment.EntitySet;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000410C File Offset: 0x0000230C
		public override IEdmEntitySet Translate(KeySegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<KeySegment>(segment, "segment");
			return segment.EntitySet;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000411F File Offset: 0x0000231F
		public override IEdmEntitySet Translate(PropertySegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<PropertySegment>(segment, "segment");
			return null;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000412D File Offset: 0x0000232D
		public override IEdmEntitySet Translate(OperationSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<OperationSegment>(segment, "segment");
			return segment.EntitySet;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004140 File Offset: 0x00002340
		public override IEdmEntitySet Translate(CountSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<CountSegment>(segment, "segment");
			return null;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000414E File Offset: 0x0000234E
		public override IEdmEntitySet Translate(OpenPropertySegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<OpenPropertySegment>(segment, "segment");
			return null;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000415C File Offset: 0x0000235C
		public override IEdmEntitySet Translate(ValueSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<ValueSegment>(segment, "segment");
			return null;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000416A File Offset: 0x0000236A
		public override IEdmEntitySet Translate(BatchSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<BatchSegment>(segment, "segment");
			return null;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004178 File Offset: 0x00002378
		public override IEdmEntitySet Translate(BatchReferenceSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<BatchReferenceSegment>(segment, "segment");
			return segment.EntitySet;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000418B File Offset: 0x0000238B
		public override IEdmEntitySet Translate(MetadataSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<MetadataSegment>(segment, "segment");
			return null;
		}
	}
}
