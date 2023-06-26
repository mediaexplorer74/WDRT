using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a method call is invalid for the object's current state.</summary>
	// Token: 0x02000100 RID: 256
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class InvalidOperationException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidOperationException" /> class.</summary>
		// Token: 0x06000FB2 RID: 4018 RVA: 0x00030221 File Offset: 0x0002E421
		[__DynamicallyInvokable]
		public InvalidOperationException()
			: base(Environment.GetResourceString("Arg_InvalidOperationException"))
		{
			base.SetErrorCode(-2146233079);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidOperationException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x06000FB3 RID: 4019 RVA: 0x0003023E File Offset: 0x0002E43E
		[__DynamicallyInvokable]
		public InvalidOperationException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233079);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidOperationException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000FB4 RID: 4020 RVA: 0x00030252 File Offset: 0x0002E452
		[__DynamicallyInvokable]
		public InvalidOperationException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233079);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidOperationException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06000FB5 RID: 4021 RVA: 0x00030267 File Offset: 0x0002E467
		protected InvalidOperationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
