using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting
{
	/// <summary>The exception that is thrown to communicate errors to the client when the client connects to non-.NET Framework applications that cannot throw exceptions.</summary>
	// Token: 0x020007C6 RID: 1990
	[ComVisible(true)]
	[Serializable]
	public class ServerException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.ServerException" /> class with default properties.</summary>
		// Token: 0x0600562F RID: 22063 RVA: 0x00132D41 File Offset: 0x00130F41
		public ServerException()
			: base(ServerException._nullMessage)
		{
			base.SetErrorCode(-2146233074);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.ServerException" /> class with a specified message.</summary>
		/// <param name="message">The message that describes the exception</param>
		// Token: 0x06005630 RID: 22064 RVA: 0x00132D59 File Offset: 0x00130F59
		public ServerException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233074);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.ServerException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="InnerException">The exception that is the cause of the current exception. If the <paramref name="InnerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06005631 RID: 22065 RVA: 0x00132D6D File Offset: 0x00130F6D
		public ServerException(string message, Exception InnerException)
			: base(message, InnerException)
		{
			base.SetErrorCode(-2146233074);
		}

		// Token: 0x06005632 RID: 22066 RVA: 0x00132D82 File Offset: 0x00130F82
		internal ServerException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x04002797 RID: 10135
		private static string _nullMessage = Environment.GetResourceString("Remoting_Default");
	}
}
