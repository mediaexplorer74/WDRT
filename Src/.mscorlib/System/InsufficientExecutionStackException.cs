using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is insufficient execution stack available to allow most methods to execute.</summary>
	// Token: 0x020000F6 RID: 246
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class InsufficientExecutionStackException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.InsufficientExecutionStackException" /> class.</summary>
		// Token: 0x06000F0D RID: 3853 RVA: 0x0002EDF1 File Offset: 0x0002CFF1
		[__DynamicallyInvokable]
		public InsufficientExecutionStackException()
			: base(Environment.GetResourceString("Arg_InsufficientExecutionStackException"))
		{
			base.SetErrorCode(-2146232968);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InsufficientExecutionStackException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06000F0E RID: 3854 RVA: 0x0002EE0E File Offset: 0x0002D00E
		[__DynamicallyInvokable]
		public InsufficientExecutionStackException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146232968);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InsufficientExecutionStackException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the inner parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000F0F RID: 3855 RVA: 0x0002EE22 File Offset: 0x0002D022
		[__DynamicallyInvokable]
		public InsufficientExecutionStackException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146232968);
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x0002EE37 File Offset: 0x0002D037
		private InsufficientExecutionStackException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
