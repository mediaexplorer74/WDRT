using System;

namespace System
{
	/// <summary>A customizable parser based on the File scheme.</summary>
	// Token: 0x02000054 RID: 84
	public class FileStyleUriParser : UriParser
	{
		/// <summary>Creates a customizable parser based on the File scheme.</summary>
		// Token: 0x060003FE RID: 1022 RVA: 0x0001D45A File Offset: 0x0001B65A
		public FileStyleUriParser()
			: base(UriParser.FileUri.Flags)
		{
		}
	}
}
