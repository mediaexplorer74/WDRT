using System;

namespace System
{
	/// <summary>A customizable parser based on the HTTP scheme.</summary>
	// Token: 0x02000052 RID: 82
	public class HttpStyleUriParser : UriParser
	{
		/// <summary>Create a customizable parser based on the HTTP scheme.</summary>
		// Token: 0x060003FC RID: 1020 RVA: 0x0001D436 File Offset: 0x0001B636
		public HttpStyleUriParser()
			: base(UriParser.HttpUri.Flags)
		{
		}
	}
}
