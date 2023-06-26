using System;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x0200001F RID: 31
	public abstract class PathSegmentTranslator<T>
	{
		// Token: 0x060000BD RID: 189 RVA: 0x0000405D File Offset: 0x0000225D
		public virtual T Translate(TypeSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004064 File Offset: 0x00002264
		public virtual T Translate(NavigationPropertySegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000406B File Offset: 0x0000226B
		public virtual T Translate(EntitySetSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004072 File Offset: 0x00002272
		public virtual T Translate(KeySegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004079 File Offset: 0x00002279
		public virtual T Translate(PropertySegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004080 File Offset: 0x00002280
		public virtual T Translate(OperationSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004087 File Offset: 0x00002287
		public virtual T Translate(OpenPropertySegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000408E File Offset: 0x0000228E
		public virtual T Translate(CountSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004095 File Offset: 0x00002295
		public virtual T Translate(NavigationPropertyLinkSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000409C File Offset: 0x0000229C
		public virtual T Translate(ValueSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000040A3 File Offset: 0x000022A3
		public virtual T Translate(BatchSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000040AA File Offset: 0x000022AA
		public virtual T Translate(BatchReferenceSegment segment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000040B1 File Offset: 0x000022B1
		public virtual T Translate(MetadataSegment segment)
		{
			throw new NotImplementedException();
		}
	}
}
