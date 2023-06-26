using System;

namespace System
{
	/// <summary>A customizable parser based on the news scheme using the Network News Transfer Protocol (NNTP).</summary>
	// Token: 0x02000055 RID: 85
	public class NewsStyleUriParser : UriParser
	{
		/// <summary>Create a customizable parser based on the news scheme using the Network News Transfer Protocol (NNTP).</summary>
		// Token: 0x060003FF RID: 1023 RVA: 0x0001D46C File Offset: 0x0001B66C
		public NewsStyleUriParser()
			: base(UriParser.NewsUri.Flags)
		{
		}
	}
}
