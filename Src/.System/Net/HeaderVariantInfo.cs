using System;

namespace System.Net
{
	// Token: 0x020000D7 RID: 215
	internal struct HeaderVariantInfo
	{
		// Token: 0x06000746 RID: 1862 RVA: 0x0002821E File Offset: 0x0002641E
		internal HeaderVariantInfo(string name, CookieVariant variant)
		{
			this.m_name = name;
			this.m_variant = variant;
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x0002822E File Offset: 0x0002642E
		internal string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x00028236 File Offset: 0x00026436
		internal CookieVariant Variant
		{
			get
			{
				return this.m_variant;
			}
		}

		// Token: 0x04000D07 RID: 3335
		private string m_name;

		// Token: 0x04000D08 RID: 3336
		private CookieVariant m_variant;
	}
}
