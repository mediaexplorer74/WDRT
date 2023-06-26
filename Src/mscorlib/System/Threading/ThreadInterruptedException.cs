using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	/// <summary>The exception that is thrown when a <see cref="T:System.Threading.Thread" /> is interrupted while it is in a waiting state.</summary>
	// Token: 0x02000517 RID: 1303
	[ComVisible(true)]
	[Serializable]
	public class ThreadInterruptedException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ThreadInterruptedException" /> class with default properties.</summary>
		// Token: 0x06003DD3 RID: 15827 RVA: 0x000E8576 File Offset: 0x000E6776
		public ThreadInterruptedException()
			: base(Exception.GetMessageFromNativeResources(Exception.ExceptionMessageKind.ThreadInterrupted))
		{
			base.SetErrorCode(-2146233063);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ThreadInterruptedException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06003DD4 RID: 15828 RVA: 0x000E858F File Offset: 0x000E678F
		public ThreadInterruptedException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233063);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ThreadInterruptedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06003DD5 RID: 15829 RVA: 0x000E85A3 File Offset: 0x000E67A3
		public ThreadInterruptedException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233063);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ThreadInterruptedException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		// Token: 0x06003DD6 RID: 15830 RVA: 0x000E85B8 File Offset: 0x000E67B8
		protected ThreadInterruptedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
