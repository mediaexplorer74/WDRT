using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Data.OData
{
	// Token: 0x0200004F RID: 79
	[DebuggerDisplay("{Message}")]
	[Serializable]
	public class ODataException : InvalidOperationException
	{
		// Token: 0x06000219 RID: 537 RVA: 0x000081ED File Offset: 0x000063ED
		public ODataException()
			: this(Strings.ODataException_GeneralError)
		{
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000081FA File Offset: 0x000063FA
		public ODataException(string message)
			: this(message, null)
		{
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00008204 File Offset: 0x00006404
		public ODataException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000820E File Offset: 0x0000640E
		protected ODataException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
