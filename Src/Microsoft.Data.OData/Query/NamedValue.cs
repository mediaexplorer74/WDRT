using System;
using Microsoft.Data.OData.Query.SyntacticAst;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x020000CC RID: 204
	internal sealed class NamedValue
	{
		// Token: 0x060004FE RID: 1278 RVA: 0x0001159F File Offset: 0x0000F79F
		public NamedValue(string name, LiteralToken value)
		{
			ExceptionUtils.CheckArgumentNotNull<LiteralToken>(value, "value");
			this.name = name;
			this.value = value;
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x000115C0 File Offset: 0x0000F7C0
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x000115C8 File Offset: 0x0000F7C8
		public LiteralToken Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x040001D9 RID: 473
		private readonly string name;

		// Token: 0x040001DA RID: 474
		private readonly LiteralToken value;
	}
}
