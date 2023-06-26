using System;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x020000D5 RID: 213
	public enum QueryNodeKind
	{
		// Token: 0x040001F9 RID: 505
		None,
		// Token: 0x040001FA RID: 506
		Constant,
		// Token: 0x040001FB RID: 507
		Convert,
		// Token: 0x040001FC RID: 508
		NonentityRangeVariableReference,
		// Token: 0x040001FD RID: 509
		BinaryOperator,
		// Token: 0x040001FE RID: 510
		UnaryOperator,
		// Token: 0x040001FF RID: 511
		SingleValuePropertyAccess,
		// Token: 0x04000200 RID: 512
		CollectionPropertyAccess,
		// Token: 0x04000201 RID: 513
		SingleValueFunctionCall,
		// Token: 0x04000202 RID: 514
		Any,
		// Token: 0x04000203 RID: 515
		CollectionNavigationNode,
		// Token: 0x04000204 RID: 516
		SingleNavigationNode,
		// Token: 0x04000205 RID: 517
		SingleValueOpenPropertyAccess,
		// Token: 0x04000206 RID: 518
		SingleEntityCast,
		// Token: 0x04000207 RID: 519
		All,
		// Token: 0x04000208 RID: 520
		EntityCollectionCast,
		// Token: 0x04000209 RID: 521
		EntityRangeVariableReference,
		// Token: 0x0400020A RID: 522
		SingleEntityFunctionCall,
		// Token: 0x0400020B RID: 523
		CollectionFunctionCall,
		// Token: 0x0400020C RID: 524
		EntityCollectionFunctionCall,
		// Token: 0x0400020D RID: 525
		NamedFunctionParameter
	}
}
