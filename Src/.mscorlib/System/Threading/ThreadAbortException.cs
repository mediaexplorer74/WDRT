using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Threading
{
	/// <summary>The exception that is thrown when a call is made to the <see cref="M:System.Threading.Thread.Abort(System.Object)" /> method. This class cannot be inherited.</summary>
	// Token: 0x02000516 RID: 1302
	[ComVisible(true)]
	[Serializable]
	public sealed class ThreadAbortException : SystemException
	{
		// Token: 0x06003DD0 RID: 15824 RVA: 0x000E8547 File Offset: 0x000E6747
		internal ThreadAbortException()
			: base(Exception.GetMessageFromNativeResources(Exception.ExceptionMessageKind.ThreadAbort))
		{
			base.SetErrorCode(-2146233040);
		}

		// Token: 0x06003DD1 RID: 15825 RVA: 0x000E8560 File Offset: 0x000E6760
		internal ThreadAbortException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Gets an object that contains application-specific information related to the thread abort.</summary>
		/// <returns>An object containing application-specific information.</returns>
		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06003DD2 RID: 15826 RVA: 0x000E856A File Offset: 0x000E676A
		public object ExceptionState
		{
			[SecuritySafeCritical]
			get
			{
				return Thread.CurrentThread.AbortReason;
			}
		}
	}
}
