using System;
using System.Runtime.Serialization;

namespace Microsoft.WindowsDeviceRecoveryTool.StateMachine.Exceptions
{
	// Token: 0x02000006 RID: 6
	[Serializable]
	public class UnexpectedErrorException : Exception
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000020D3 File Offset: 0x000002D3
		public UnexpectedErrorException()
		{
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020DD File Offset: 0x000002DD
		public UnexpectedErrorException(string message)
			: base(message)
		{
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000020E8 File Offset: 0x000002E8
		public UnexpectedErrorException(string message, Exception internalException)
			: base(message, internalException)
		{
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000020F4 File Offset: 0x000002F4
		protected UnexpectedErrorException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
