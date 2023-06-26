using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x02000047 RID: 71
	internal sealed class IsCollectionTranslator : PathSegmentTranslator<bool>
	{
		// Token: 0x060001C8 RID: 456 RVA: 0x000077FC File Offset: 0x000059FC
		public override bool Translate(NavigationPropertySegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<NavigationPropertySegment>(segment, "segment");
			return segment.NavigationProperty.Type.IsCollection();
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00007819 File Offset: 0x00005A19
		public override bool Translate(EntitySetSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<EntitySetSegment>(segment, "segment");
			return true;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00007827 File Offset: 0x00005A27
		public override bool Translate(KeySegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<KeySegment>(segment, "segment");
			return false;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00007835 File Offset: 0x00005A35
		public override bool Translate(PropertySegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<PropertySegment>(segment, "segment");
			return false;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00007843 File Offset: 0x00005A43
		public override bool Translate(OpenPropertySegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<OpenPropertySegment>(segment, "segment");
			return false;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00007851 File Offset: 0x00005A51
		public override bool Translate(CountSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<CountSegment>(segment, "segment");
			return false;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000785F File Offset: 0x00005A5F
		public override bool Translate(NavigationPropertyLinkSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<NavigationPropertyLinkSegment>(segment, "segment");
			return false;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000786D File Offset: 0x00005A6D
		public override bool Translate(BatchSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<BatchSegment>(segment, "segment");
			return false;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000787B File Offset: 0x00005A7B
		public override bool Translate(BatchReferenceSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<BatchReferenceSegment>(segment, "segment");
			return false;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00007889 File Offset: 0x00005A89
		public override bool Translate(ValueSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<ValueSegment>(segment, "segment");
			throw new NotImplementedException(segment.ToString());
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x000078A1 File Offset: 0x00005AA1
		public override bool Translate(MetadataSegment segment)
		{
			ExceptionUtils.CheckArgumentNotNull<MetadataSegment>(segment, "segment");
			return false;
		}
	}
}
