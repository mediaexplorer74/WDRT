using System;

namespace System.Net
{
	// Token: 0x02000116 RID: 278
	internal static class NclConstants
	{
		// Token: 0x04000F55 RID: 3925
		internal static readonly object Sentinel = new object();

		// Token: 0x04000F56 RID: 3926
		internal static readonly object[] EmptyObjectArray = new object[0];

		// Token: 0x04000F57 RID: 3927
		internal static readonly Uri[] EmptyUriArray = new Uri[0];

		// Token: 0x04000F58 RID: 3928
		internal static readonly byte[] CRLF = new byte[] { 13, 10 };

		// Token: 0x04000F59 RID: 3929
		internal static readonly byte[] ChunkTerminator = new byte[] { 48, 13, 10, 13, 10 };
	}
}
