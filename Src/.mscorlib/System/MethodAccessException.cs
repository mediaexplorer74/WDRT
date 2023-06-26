using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is an invalid attempt to access a method, such as accessing a private method from partially trusted code.</summary>
	// Token: 0x0200010D RID: 269
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class MethodAccessException : MemberAccessException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.MethodAccessException" /> class, setting the <see cref="P:System.Exception.Message" /> property of the new instance to a system-supplied message that describes the error, such as "Attempt to access the method failed." This message takes into account the current system culture.</summary>
		// Token: 0x06001053 RID: 4179 RVA: 0x00031085 File Offset: 0x0002F285
		[__DynamicallyInvokable]
		public MethodAccessException()
			: base(Environment.GetResourceString("Arg_MethodAccessException"))
		{
			base.SetErrorCode(-2146233072);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MethodAccessException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error.</param>
		// Token: 0x06001054 RID: 4180 RVA: 0x000310A2 File Offset: 0x0002F2A2
		[__DynamicallyInvokable]
		public MethodAccessException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233072);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MethodAccessException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06001055 RID: 4181 RVA: 0x000310B6 File Offset: 0x0002F2B6
		[__DynamicallyInvokable]
		public MethodAccessException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233072);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MethodAccessException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06001056 RID: 4182 RVA: 0x000310CB File Offset: 0x0002F2CB
		protected MethodAccessException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
