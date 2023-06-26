using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200076B RID: 1899
	[Serializable]
	internal enum InternalParseTypeE
	{
		// Token: 0x0400251C RID: 9500
		Empty,
		// Token: 0x0400251D RID: 9501
		SerializedStreamHeader,
		// Token: 0x0400251E RID: 9502
		Object,
		// Token: 0x0400251F RID: 9503
		Member,
		// Token: 0x04002520 RID: 9504
		ObjectEnd,
		// Token: 0x04002521 RID: 9505
		MemberEnd,
		// Token: 0x04002522 RID: 9506
		Headers,
		// Token: 0x04002523 RID: 9507
		HeadersEnd,
		// Token: 0x04002524 RID: 9508
		SerializedStreamHeaderEnd,
		// Token: 0x04002525 RID: 9509
		Envelope,
		// Token: 0x04002526 RID: 9510
		EnvelopeEnd,
		// Token: 0x04002527 RID: 9511
		Body,
		// Token: 0x04002528 RID: 9512
		BodyEnd
	}
}
