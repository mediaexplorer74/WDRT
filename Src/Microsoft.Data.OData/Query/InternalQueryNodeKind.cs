using System;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x020000D6 RID: 214
	internal enum InternalQueryNodeKind
	{
		// Token: 0x0400020F RID: 527
		None,
		// Token: 0x04000210 RID: 528
		Constant,
		// Token: 0x04000211 RID: 529
		Convert,
		// Token: 0x04000212 RID: 530
		NonentityRangeVariableReference,
		// Token: 0x04000213 RID: 531
		BinaryOperator,
		// Token: 0x04000214 RID: 532
		UnaryOperator,
		// Token: 0x04000215 RID: 533
		SingleValuePropertyAccess,
		// Token: 0x04000216 RID: 534
		CollectionPropertyAccess,
		// Token: 0x04000217 RID: 535
		SingleValueFunctionCall,
		// Token: 0x04000218 RID: 536
		Any,
		// Token: 0x04000219 RID: 537
		CollectionNavigationNode,
		// Token: 0x0400021A RID: 538
		SingleNavigationNode,
		// Token: 0x0400021B RID: 539
		SingleValueOpenPropertyAccess,
		// Token: 0x0400021C RID: 540
		SingleEntityCast,
		// Token: 0x0400021D RID: 541
		All,
		// Token: 0x0400021E RID: 542
		EntityCollectionCast,
		// Token: 0x0400021F RID: 543
		EntityRangeVariableReference,
		// Token: 0x04000220 RID: 544
		SingleEntityFunctionCall,
		// Token: 0x04000221 RID: 545
		CollectionFunctionCall,
		// Token: 0x04000222 RID: 546
		EntityCollectionFunctionCall,
		// Token: 0x04000223 RID: 547
		NamedFunctionParameter,
		// Token: 0x04000224 RID: 548
		EntitySet,
		// Token: 0x04000225 RID: 549
		KeyLookup
	}
}
