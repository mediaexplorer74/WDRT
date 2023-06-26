using System;
using System.Diagnostics;
using System.Globalization;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x020000C7 RID: 199
	[DebuggerDisplay("{InternalKind} @ {Position}: [{Text}]")]
	internal struct ExpressionToken
	{
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x000111CC File Offset: 0x0000F3CC
		internal bool IsComparisonOperator
		{
			get
			{
				return this.Kind == ExpressionTokenKind.Identifier && (this.Text == "eq" || this.Text == "ne" || this.Text == "lt" || this.Text == "gt" || this.Text == "le" || this.Text == "ge");
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x00011250 File Offset: 0x0000F450
		internal bool IsEqualityOperator
		{
			get
			{
				return this.Kind == ExpressionTokenKind.Identifier && (this.Text == "eq" || this.Text == "ne");
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x00011284 File Offset: 0x0000F484
		internal bool IsKeyValueToken
		{
			get
			{
				return this.Kind == ExpressionTokenKind.BinaryLiteral || this.Kind == ExpressionTokenKind.BooleanLiteral || this.Kind == ExpressionTokenKind.DateTimeLiteral || this.Kind == ExpressionTokenKind.DateTimeOffsetLiteral || this.Kind == ExpressionTokenKind.TimeLiteral || this.Kind == ExpressionTokenKind.GuidLiteral || this.Kind == ExpressionTokenKind.StringLiteral || this.Kind == ExpressionTokenKind.GeographyLiteral || this.Kind == ExpressionTokenKind.GeometryLiteral || ExpressionLexerUtils.IsNumeric(this.Kind);
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x000112F6 File Offset: 0x0000F4F6
		internal bool IsFunctionParameterToken
		{
			get
			{
				return this.IsKeyValueToken || this.Kind == ExpressionTokenKind.BracketedExpression || this.Kind == ExpressionTokenKind.NullLiteral;
			}
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00011318 File Offset: 0x0000F518
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0} @ {1}: [{2}]", new object[] { this.Kind, this.Position, this.Text });
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00011364 File Offset: 0x0000F564
		internal string GetIdentifier()
		{
			if (this.Kind != ExpressionTokenKind.Identifier)
			{
				string text = Strings.ExpressionToken_IdentifierExpected(this.Position);
				throw new ODataException(text);
			}
			return this.Text;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00011398 File Offset: 0x0000F598
		internal bool IdentifierIs(string id)
		{
			return this.Kind == ExpressionTokenKind.Identifier && this.Text == id;
		}

		// Token: 0x040001A8 RID: 424
		internal static readonly ExpressionToken GreaterThan = new ExpressionToken
		{
			Text = "gt",
			Kind = ExpressionTokenKind.Identifier,
			Position = 0
		};

		// Token: 0x040001A9 RID: 425
		internal static readonly ExpressionToken EqualsTo = new ExpressionToken
		{
			Text = "eq",
			Kind = ExpressionTokenKind.Identifier,
			Position = 0
		};

		// Token: 0x040001AA RID: 426
		internal static readonly ExpressionToken LessThan = new ExpressionToken
		{
			Text = "lt",
			Kind = ExpressionTokenKind.Identifier,
			Position = 0
		};

		// Token: 0x040001AB RID: 427
		internal ExpressionTokenKind Kind;

		// Token: 0x040001AC RID: 428
		internal string Text;

		// Token: 0x040001AD RID: 429
		internal int Position;
	}
}
