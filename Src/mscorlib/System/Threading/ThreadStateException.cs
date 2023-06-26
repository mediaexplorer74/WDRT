using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	/// <summary>The exception that is thrown when a <see cref="T:System.Threading.Thread" /> is in an invalid <see cref="P:System.Threading.Thread.ThreadState" /> for the method call.</summary>
	// Token: 0x02000528 RID: 1320
	[ComVisible(true)]
	[Serializable]
	public class ThreadStateException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ThreadStateException" /> class with default properties.</summary>
		// Token: 0x06003E40 RID: 15936 RVA: 0x000E9394 File Offset: 0x000E7594
		public ThreadStateException()
			: base(Environment.GetResourceString("Arg_ThreadStateException"))
		{
			base.SetErrorCode(-2146233056);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ThreadStateException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06003E41 RID: 15937 RVA: 0x000E93B1 File Offset: 0x000E75B1
		public ThreadStateException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233056);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ThreadStateException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06003E42 RID: 15938 RVA: 0x000E93C5 File Offset: 0x000E75C5
		public ThreadStateException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233056);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ThreadStateException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		// Token: 0x06003E43 RID: 15939 RVA: 0x000E93DA File Offset: 0x000E75DA
		protected ThreadStateException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
