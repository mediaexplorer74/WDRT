using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Data.OData
{
	// Token: 0x020001E8 RID: 488
	[DebuggerDisplay("{Message}")]
	[Serializable]
	public class ODataContentTypeException : ODataException
	{
		// Token: 0x06000F10 RID: 3856 RVA: 0x00035D09 File Offset: 0x00033F09
		public ODataContentTypeException()
			: this(Strings.ODataException_GeneralError)
		{
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x00035D16 File Offset: 0x00033F16
		public ODataContentTypeException(string message)
			: this(message, null)
		{
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x00035D20 File Offset: 0x00033F20
		public ODataContentTypeException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x00035D2A File Offset: 0x00033F2A
		protected ODataContentTypeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
