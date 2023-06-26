using System;

namespace System.Data.Services.Client
{
	// Token: 0x020000AF RID: 175
	internal class TextPrimitiveParserToken : PrimitiveParserToken
	{
		// Token: 0x060005B0 RID: 1456 RVA: 0x00015BBA File Offset: 0x00013DBA
		internal TextPrimitiveParserToken(string text)
		{
			this.Text = text;
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x00015BC9 File Offset: 0x00013DC9
		// (set) Token: 0x060005B2 RID: 1458 RVA: 0x00015BD1 File Offset: 0x00013DD1
		internal string Text { get; private set; }

		// Token: 0x060005B3 RID: 1459 RVA: 0x00015BDA File Offset: 0x00013DDA
		internal override object Materialize(Type clrType)
		{
			return ClientConvert.ChangeType(this.Text, clrType);
		}
	}
}
