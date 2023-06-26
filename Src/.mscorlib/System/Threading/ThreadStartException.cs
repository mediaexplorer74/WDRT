using System;
using System.Runtime.Serialization;

namespace System.Threading
{
	/// <summary>The exception that is thrown when a failure occurs in a managed thread after the underlying operating system thread has been started, but before the thread is ready to execute user code.</summary>
	// Token: 0x02000529 RID: 1321
	[Serializable]
	public sealed class ThreadStartException : SystemException
	{
		// Token: 0x06003E44 RID: 15940 RVA: 0x000E93E4 File Offset: 0x000E75E4
		private ThreadStartException()
			: base(Environment.GetResourceString("Arg_ThreadStartException"))
		{
			base.SetErrorCode(-2146233051);
		}

		// Token: 0x06003E45 RID: 15941 RVA: 0x000E9401 File Offset: 0x000E7601
		private ThreadStartException(Exception reason)
			: base(Environment.GetResourceString("Arg_ThreadStartException"), reason)
		{
			base.SetErrorCode(-2146233051);
		}

		// Token: 0x06003E46 RID: 15942 RVA: 0x000E941F File Offset: 0x000E761F
		internal ThreadStartException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
