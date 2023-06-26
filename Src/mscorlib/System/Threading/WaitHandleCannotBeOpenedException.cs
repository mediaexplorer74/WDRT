using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	/// <summary>The exception that is thrown when an attempt is made to open a system mutex, semaphore, or event wait handle that does not exist.</summary>
	// Token: 0x02000533 RID: 1331
	[ComVisible(false)]
	[__DynamicallyInvokable]
	[Serializable]
	public class WaitHandleCannotBeOpenedException : ApplicationException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.WaitHandleCannotBeOpenedException" /> class with default values.</summary>
		// Token: 0x06003EBF RID: 16063 RVA: 0x000EAA35 File Offset: 0x000E8C35
		[__DynamicallyInvokable]
		public WaitHandleCannotBeOpenedException()
			: base(Environment.GetResourceString("Threading.WaitHandleCannotBeOpenedException"))
		{
			base.SetErrorCode(-2146233044);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.WaitHandleCannotBeOpenedException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06003EC0 RID: 16064 RVA: 0x000EAA52 File Offset: 0x000E8C52
		[__DynamicallyInvokable]
		public WaitHandleCannotBeOpenedException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233044);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.WaitHandleCannotBeOpenedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06003EC1 RID: 16065 RVA: 0x000EAA66 File Offset: 0x000E8C66
		[__DynamicallyInvokable]
		public WaitHandleCannotBeOpenedException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233044);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.WaitHandleCannotBeOpenedException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination.</param>
		// Token: 0x06003EC2 RID: 16066 RVA: 0x000EAA7B File Offset: 0x000E8C7B
		protected WaitHandleCannotBeOpenedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
