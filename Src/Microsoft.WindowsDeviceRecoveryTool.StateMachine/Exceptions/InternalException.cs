using System;
using System.Runtime.Serialization;

namespace Microsoft.WindowsDeviceRecoveryTool.StateMachine.Exceptions
{
	// Token: 0x02000005 RID: 5
	[Serializable]
	public class InternalException : Exception
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020D3 File Offset: 0x000002D3
		public InternalException()
		{
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020DD File Offset: 0x000002DD
		public InternalException(string message)
			: base(message)
		{
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020E8 File Offset: 0x000002E8
		public InternalException(string message, Exception internalException)
			: base(message, internalException)
		{
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020F4 File Offset: 0x000002F4
		protected InternalException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
