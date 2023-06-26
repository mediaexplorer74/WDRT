using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Indicates the status of the server message processing.</summary>
	// Token: 0x02000844 RID: 2116
	[ComVisible(true)]
	[Serializable]
	public enum ServerProcessing
	{
		/// <summary>The server synchronously processed the message.</summary>
		// Token: 0x04002900 RID: 10496
		Complete,
		/// <summary>The message was dispatched and no response can be sent.</summary>
		// Token: 0x04002901 RID: 10497
		OneWay,
		/// <summary>The call was dispatched asynchronously, which indicates that the sink must store response data on the stack for later processing.</summary>
		// Token: 0x04002902 RID: 10498
		Async
	}
}
