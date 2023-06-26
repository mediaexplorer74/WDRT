using System;
using System.Collections;

namespace System.Diagnostics
{
	// Token: 0x020004F3 RID: 1267
	internal class OrdinalCaseInsensitiveComparer : IComparer
	{
		// Token: 0x06003011 RID: 12305 RVA: 0x000D93C8 File Offset: 0x000D75C8
		public int Compare(object a, object b)
		{
			string text = a as string;
			string text2 = b as string;
			if (text != null && text2 != null)
			{
				return string.Compare(text, text2, StringComparison.OrdinalIgnoreCase);
			}
			return Comparer.Default.Compare(a, b);
		}

		// Token: 0x0400286A RID: 10346
		internal static readonly OrdinalCaseInsensitiveComparer Default = new OrdinalCaseInsensitiveComparer();
	}
}
