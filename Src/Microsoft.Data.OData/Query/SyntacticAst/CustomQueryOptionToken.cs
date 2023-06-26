using System;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x020000B5 RID: 181
	internal sealed class CustomQueryOptionToken : QueryToken
	{
		// Token: 0x0600046F RID: 1135 RVA: 0x0000E95C File Offset: 0x0000CB5C
		public CustomQueryOptionToken(string name, string value)
		{
			this.name = name;
			this.value = value;
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x0000E972 File Offset: 0x0000CB72
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.CustomQueryOption;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x0000E976 File Offset: 0x0000CB76
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x0000E97E File Offset: 0x0000CB7E
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000E986 File Offset: 0x0000CB86
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0400017F RID: 383
		private readonly string name;

		// Token: 0x04000180 RID: 384
		private readonly string value;
	}
}
