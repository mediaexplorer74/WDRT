using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is an invalid attempt to access a private or protected field inside a class.</summary>
	// Token: 0x020000E2 RID: 226
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class FieldAccessException : MemberAccessException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.FieldAccessException" /> class.</summary>
		// Token: 0x06000E7E RID: 3710 RVA: 0x0002CC67 File Offset: 0x0002AE67
		[__DynamicallyInvokable]
		public FieldAccessException()
			: base(Environment.GetResourceString("Arg_FieldAccessException"))
		{
			base.SetErrorCode(-2146233081);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.FieldAccessException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06000E7F RID: 3711 RVA: 0x0002CC84 File Offset: 0x0002AE84
		[__DynamicallyInvokable]
		public FieldAccessException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233081);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.FieldAccessException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000E80 RID: 3712 RVA: 0x0002CC98 File Offset: 0x0002AE98
		[__DynamicallyInvokable]
		public FieldAccessException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233081);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.FieldAccessException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06000E81 RID: 3713 RVA: 0x0002CCAD File Offset: 0x0002AEAD
		protected FieldAccessException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
