using System;
using System.Runtime.Serialization;

namespace System.Net
{
	// Token: 0x02000114 RID: 276
	internal class InternalException : SystemException
	{
		// Token: 0x06000B00 RID: 2816 RVA: 0x0003CBE7 File Offset: 0x0003ADE7
		internal InternalException()
		{
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0003CBEF File Offset: 0x0003ADEF
		internal InternalException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}
	}
}
