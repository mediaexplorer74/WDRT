using System;

namespace System.Configuration
{
	// Token: 0x02000071 RID: 113
	internal sealed class SchemeSettingInternal
	{
		// Token: 0x06000491 RID: 1169 RVA: 0x0001F463 File Offset: 0x0001D663
		public SchemeSettingInternal(string name, GenericUriParserOptions options)
		{
			this.name = name.ToLowerInvariant();
			this.options = options;
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x0001F47E File Offset: 0x0001D67E
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x0001F486 File Offset: 0x0001D686
		public GenericUriParserOptions Options
		{
			get
			{
				return this.options;
			}
		}

		// Token: 0x04000BE1 RID: 3041
		private string name;

		// Token: 0x04000BE2 RID: 3042
		private GenericUriParserOptions options;
	}
}
