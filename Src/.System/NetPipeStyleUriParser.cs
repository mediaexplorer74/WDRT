using System;

namespace System
{
	/// <summary>A parser based on the NetPipe scheme for the "Indigo" system.</summary>
	// Token: 0x02000058 RID: 88
	public class NetPipeStyleUriParser : UriParser
	{
		/// <summary>Create a parser based on the NetPipe scheme for the "Indigo" system.</summary>
		// Token: 0x06000402 RID: 1026 RVA: 0x0001D4A2 File Offset: 0x0001B6A2
		public NetPipeStyleUriParser()
			: base(UriParser.NetPipeUri.Flags)
		{
		}
	}
}
