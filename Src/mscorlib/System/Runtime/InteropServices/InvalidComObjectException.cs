using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	/// <summary>The exception thrown when an invalid COM object is used.</summary>
	// Token: 0x02000966 RID: 2406
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class InvalidComObjectException : SystemException
	{
		/// <summary>Initializes an instance of the <see langword="InvalidComObjectException" /> with default properties.</summary>
		// Token: 0x06006240 RID: 25152 RVA: 0x00150EE5 File Offset: 0x0014F0E5
		[__DynamicallyInvokable]
		public InvalidComObjectException()
			: base(Environment.GetResourceString("Arg_InvalidComObjectException"))
		{
			base.SetErrorCode(-2146233049);
		}

		/// <summary>Initializes an instance of the <see langword="InvalidComObjectException" /> with a message.</summary>
		/// <param name="message">The message that indicates the reason for the exception.</param>
		// Token: 0x06006241 RID: 25153 RVA: 0x00150F02 File Offset: 0x0014F102
		[__DynamicallyInvokable]
		public InvalidComObjectException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233049);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.InvalidComObjectException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06006242 RID: 25154 RVA: 0x00150F16 File Offset: 0x0014F116
		[__DynamicallyInvokable]
		public InvalidComObjectException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233049);
		}

		/// <summary>Initializes a new instance of the <see langword="COMException" /> class from serialization data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x06006243 RID: 25155 RVA: 0x00150F2B File Offset: 0x0014F12B
		protected InvalidComObjectException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
