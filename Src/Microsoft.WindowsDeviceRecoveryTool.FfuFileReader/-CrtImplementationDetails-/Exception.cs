using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x020002FB RID: 763
	[Serializable]
	internal class Exception : Exception
	{
		// Token: 0x060002AD RID: 685 RVA: 0x00012B2C File Offset: 0x00011F2C
		protected Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00012B14 File Offset: 0x00011F14
		public Exception(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00012B00 File Offset: 0x00011F00
		public Exception(string message)
			: base(message)
		{
		}
	}
}
