using System;

namespace System
{
	/// <summary>A parser based on the NetTcp scheme for the "Indigo" system.</summary>
	// Token: 0x02000059 RID: 89
	public class NetTcpStyleUriParser : UriParser
	{
		/// <summary>Create a parser based on the NetTcp scheme for the "Indigo" system.</summary>
		// Token: 0x06000403 RID: 1027 RVA: 0x0001D4B4 File Offset: 0x0001B6B4
		public NetTcpStyleUriParser()
			: base(UriParser.NetTcpUri.Flags)
		{
		}
	}
}
