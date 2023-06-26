using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x02000050 RID: 80
	[DebuggerDisplay("{Message}")]
	[Serializable]
	public sealed class ODataUnrecognizedPathException : ODataException
	{
		// Token: 0x0600021D RID: 541 RVA: 0x00008218 File Offset: 0x00006418
		public ODataUnrecognizedPathException()
			: this(Strings.ODataUriParserException_GeneralError, null)
		{
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00008226 File Offset: 0x00006426
		public ODataUnrecognizedPathException(string message)
			: this(message, null)
		{
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00008230 File Offset: 0x00006430
		public ODataUnrecognizedPathException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000823A File Offset: 0x0000643A
		private ODataUnrecognizedPathException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
