using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting
{
	/// <summary>The exception that is thrown when the server or the client cannot be reached for a previously specified period of time.</summary>
	// Token: 0x020007C7 RID: 1991
	[ComVisible(true)]
	[Serializable]
	public class RemotingTimeoutException : RemotingException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.RemotingTimeoutException" /> class with default properties.</summary>
		// Token: 0x06005634 RID: 22068 RVA: 0x00132D9D File Offset: 0x00130F9D
		public RemotingTimeoutException()
			: base(RemotingTimeoutException._nullMessage)
		{
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Runtime.Remoting.RemotingTimeoutException" /> class with a specified message.</summary>
		/// <param name="message">The message that indicates the reason why the exception occurred.</param>
		// Token: 0x06005635 RID: 22069 RVA: 0x00132DAA File Offset: 0x00130FAA
		public RemotingTimeoutException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233077);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.RemotingTimeoutException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="InnerException">The exception that is the cause of the current exception. If the <paramref name="InnerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06005636 RID: 22070 RVA: 0x00132DBE File Offset: 0x00130FBE
		public RemotingTimeoutException(string message, Exception InnerException)
			: base(message, InnerException)
		{
			base.SetErrorCode(-2146233077);
		}

		// Token: 0x06005637 RID: 22071 RVA: 0x00132DD3 File Offset: 0x00130FD3
		internal RemotingTimeoutException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x04002798 RID: 10136
		private static string _nullMessage = Environment.GetResourceString("Remoting_Default");
	}
}
