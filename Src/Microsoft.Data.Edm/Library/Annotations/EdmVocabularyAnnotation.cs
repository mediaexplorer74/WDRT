using System;
using Microsoft.Data.Edm.Annotations;

namespace Microsoft.Data.Edm.Library.Annotations
{
	// Token: 0x0200017D RID: 381
	public abstract class EdmVocabularyAnnotation : EdmElement, IEdmVocabularyAnnotation, IEdmElement
	{
		// Token: 0x0600086F RID: 2159 RVA: 0x00017DD7 File Offset: 0x00015FD7
		protected EdmVocabularyAnnotation(IEdmVocabularyAnnotatable target, IEdmTerm term, string qualifier)
		{
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(target, "target");
			EdmUtil.CheckArgumentNull<IEdmTerm>(term, "term");
			this.target = target;
			this.term = term;
			this.qualifier = qualifier;
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000870 RID: 2160 RVA: 0x00017E0C File Offset: 0x0001600C
		public IEdmVocabularyAnnotatable Target
		{
			get
			{
				return this.target;
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000871 RID: 2161 RVA: 0x00017E14 File Offset: 0x00016014
		public IEdmTerm Term
		{
			get
			{
				return this.term;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x00017E1C File Offset: 0x0001601C
		public string Qualifier
		{
			get
			{
				return this.qualifier;
			}
		}

		// Token: 0x04000434 RID: 1076
		private readonly IEdmVocabularyAnnotatable target;

		// Token: 0x04000435 RID: 1077
		private readonly IEdmTerm term;

		// Token: 0x04000436 RID: 1078
		private readonly string qualifier;
	}
}
