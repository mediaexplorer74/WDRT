using System;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x020000C8 RID: 200
	internal enum ExpressionTokenKind
	{
		// Token: 0x040001AF RID: 431
		Unknown,
		// Token: 0x040001B0 RID: 432
		End,
		// Token: 0x040001B1 RID: 433
		Equal,
		// Token: 0x040001B2 RID: 434
		Identifier,
		// Token: 0x040001B3 RID: 435
		NullLiteral,
		// Token: 0x040001B4 RID: 436
		BooleanLiteral,
		// Token: 0x040001B5 RID: 437
		StringLiteral,
		// Token: 0x040001B6 RID: 438
		IntegerLiteral,
		// Token: 0x040001B7 RID: 439
		Int64Literal,
		// Token: 0x040001B8 RID: 440
		SingleLiteral,
		// Token: 0x040001B9 RID: 441
		DateTimeLiteral,
		// Token: 0x040001BA RID: 442
		DateTimeOffsetLiteral,
		// Token: 0x040001BB RID: 443
		TimeLiteral,
		// Token: 0x040001BC RID: 444
		DecimalLiteral,
		// Token: 0x040001BD RID: 445
		DoubleLiteral,
		// Token: 0x040001BE RID: 446
		GuidLiteral,
		// Token: 0x040001BF RID: 447
		BinaryLiteral,
		// Token: 0x040001C0 RID: 448
		GeographyLiteral,
		// Token: 0x040001C1 RID: 449
		GeometryLiteral,
		// Token: 0x040001C2 RID: 450
		Exclamation,
		// Token: 0x040001C3 RID: 451
		OpenParen,
		// Token: 0x040001C4 RID: 452
		CloseParen,
		// Token: 0x040001C5 RID: 453
		Comma,
		// Token: 0x040001C6 RID: 454
		Colon,
		// Token: 0x040001C7 RID: 455
		Minus,
		// Token: 0x040001C8 RID: 456
		Slash,
		// Token: 0x040001C9 RID: 457
		Question,
		// Token: 0x040001CA RID: 458
		Dot,
		// Token: 0x040001CB RID: 459
		Star,
		// Token: 0x040001CC RID: 460
		SemiColon,
		// Token: 0x040001CD RID: 461
		ParameterAlias,
		// Token: 0x040001CE RID: 462
		BracketedExpression
	}
}
