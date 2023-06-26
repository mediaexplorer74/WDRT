using System;

namespace System
{
	/// <summary>A customizable parser based on the File Transfer Protocol (FTP) scheme.</summary>
	// Token: 0x02000053 RID: 83
	public class FtpStyleUriParser : UriParser
	{
		/// <summary>Creates a customizable parser based on the File Transfer Protocol (FTP) scheme.</summary>
		// Token: 0x060003FD RID: 1021 RVA: 0x0001D448 File Offset: 0x0001B648
		public FtpStyleUriParser()
			: base(UriParser.FtpUri.Flags)
		{
		}
	}
}
