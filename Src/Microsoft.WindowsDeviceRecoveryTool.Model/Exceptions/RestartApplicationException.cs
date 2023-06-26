using System;
using System.Runtime.Serialization;

namespace Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions
{
	// Token: 0x02000029 RID: 41
	[Serializable]
	public class RestartApplicationException : Exception
	{
		// Token: 0x060001FF RID: 511 RVA: 0x00006346 File Offset: 0x00004546
		public RestartApplicationException()
		{
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00006350 File Offset: 0x00004550
		public RestartApplicationException(string message)
			: base(message)
		{
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000635B File Offset: 0x0000455B
		public RestartApplicationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00006367 File Offset: 0x00004567
		protected RestartApplicationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
