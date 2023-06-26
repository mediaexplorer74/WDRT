using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an attempt to access a class member fails.</summary>
	// Token: 0x0200008D RID: 141
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class MemberAccessException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.MemberAccessException" /> class.</summary>
		// Token: 0x06000742 RID: 1858 RVA: 0x000198CC File Offset: 0x00017ACC
		[__DynamicallyInvokable]
		public MemberAccessException()
			: base(Environment.GetResourceString("Arg_AccessException"))
		{
			base.SetErrorCode(-2146233062);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MemberAccessException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x06000743 RID: 1859 RVA: 0x000198E9 File Offset: 0x00017AE9
		[__DynamicallyInvokable]
		public MemberAccessException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233062);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MemberAccessException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000744 RID: 1860 RVA: 0x000198FD File Offset: 0x00017AFD
		[__DynamicallyInvokable]
		public MemberAccessException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233062);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MemberAccessException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06000745 RID: 1861 RVA: 0x00019912 File Offset: 0x00017B12
		protected MemberAccessException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
