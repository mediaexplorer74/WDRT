using System;
using System.Runtime.InteropServices;

namespace System.Net.Mail
{
	// Token: 0x02000260 RID: 608
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	internal struct MetadataRecord
	{
		// Token: 0x0400177A RID: 6010
		internal uint Identifier;

		// Token: 0x0400177B RID: 6011
		internal uint Attributes;

		// Token: 0x0400177C RID: 6012
		internal uint UserType;

		// Token: 0x0400177D RID: 6013
		internal uint DataType;

		// Token: 0x0400177E RID: 6014
		internal uint DataLen;

		// Token: 0x0400177F RID: 6015
		internal IntPtr DataBuf;

		// Token: 0x04001780 RID: 6016
		internal uint DataTag;
	}
}
