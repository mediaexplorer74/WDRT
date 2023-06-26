using System;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x02000044 RID: 68
	public abstract class PathSegmentHandler
	{
		// Token: 0x060001AC RID: 428 RVA: 0x00007694 File Offset: 0x00005894
		public virtual void Handle(TypeSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000769B File Offset: 0x0000589B
		public virtual void Handle(NavigationPropertySegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000076A2 File Offset: 0x000058A2
		public virtual void Handle(EntitySetSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000076A9 File Offset: 0x000058A9
		public virtual void Handle(KeySegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000076B0 File Offset: 0x000058B0
		public virtual void Handle(PropertySegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x000076B7 File Offset: 0x000058B7
		public virtual void Handle(OperationSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000076BE File Offset: 0x000058BE
		public virtual void Handle(OpenPropertySegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000076C5 File Offset: 0x000058C5
		public virtual void Handle(CountSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000076CC File Offset: 0x000058CC
		public virtual void Handle(NavigationPropertyLinkSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000076D3 File Offset: 0x000058D3
		public virtual void Handle(ValueSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000076DA File Offset: 0x000058DA
		public virtual void Handle(BatchSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000076E1 File Offset: 0x000058E1
		public virtual void Handle(BatchReferenceSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000076E8 File Offset: 0x000058E8
		public virtual void Handle(MetadataSegment segment)
		{
			throw new NotImplementedException();
		}
	}
}
