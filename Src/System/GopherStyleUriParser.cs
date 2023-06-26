using System;

namespace System
{
	/// <summary>A customizable parser based on the Gopher scheme.</summary>
	// Token: 0x02000056 RID: 86
	public class GopherStyleUriParser : UriParser
	{
		/// <summary>Creates a customizable parser based on the Gopher scheme.</summary>
		// Token: 0x06000400 RID: 1024 RVA: 0x0001D47E File Offset: 0x0001B67E
		public GopherStyleUriParser()
			: base(UriParser.GopherUri.Flags)
		{
		}
	}
}
