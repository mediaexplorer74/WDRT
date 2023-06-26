using System;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x020000DA RID: 218
	internal enum QueryTokenKind
	{
		// Token: 0x0400022B RID: 555
		BinaryOperator = 3,
		// Token: 0x0400022C RID: 556
		UnaryOperator,
		// Token: 0x0400022D RID: 557
		Literal,
		// Token: 0x0400022E RID: 558
		FunctionCall,
		// Token: 0x0400022F RID: 559
		EndPath,
		// Token: 0x04000230 RID: 560
		OrderBy,
		// Token: 0x04000231 RID: 561
		CustomQueryOption,
		// Token: 0x04000232 RID: 562
		Select,
		// Token: 0x04000233 RID: 563
		Star,
		// Token: 0x04000234 RID: 564
		Expand = 13,
		// Token: 0x04000235 RID: 565
		TypeSegment,
		// Token: 0x04000236 RID: 566
		Any,
		// Token: 0x04000237 RID: 567
		InnerPath,
		// Token: 0x04000238 RID: 568
		DottedIdentifier,
		// Token: 0x04000239 RID: 569
		RangeVariable,
		// Token: 0x0400023A RID: 570
		All,
		// Token: 0x0400023B RID: 571
		ExpandTerm,
		// Token: 0x0400023C RID: 572
		FunctionParameter,
		// Token: 0x0400023D RID: 573
		FunctionParameterAlias,
		// Token: 0x0400023E RID: 574
		RawFunctionParameterValue
	}
}
