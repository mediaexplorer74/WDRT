using System;
using Microsoft.Data.OData.Query.SyntacticAst;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x02000024 RID: 36
	internal interface ISelectExpandTermParser
	{
		// Token: 0x060000EA RID: 234
		SelectToken ParseSelect();

		// Token: 0x060000EB RID: 235
		ExpandToken ParseExpand();

		// Token: 0x060000EC RID: 236
		PathSegmentToken ParseSingleSelectTerm(bool isInnerTerm);

		// Token: 0x060000ED RID: 237
		ExpandTermToken ParseSingleExpandTerm(bool isInnerTerm);
	}
}
