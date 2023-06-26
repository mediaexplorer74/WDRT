using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an attempt is made to store an element of the wrong type within an array.</summary>
	// Token: 0x020000AB RID: 171
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class ArrayTypeMismatchException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ArrayTypeMismatchException" /> class.</summary>
		// Token: 0x060009C3 RID: 2499 RVA: 0x0001F76A File Offset: 0x0001D96A
		[__DynamicallyInvokable]
		public ArrayTypeMismatchException()
			: base(Environment.GetResourceString("Arg_ArrayTypeMismatchException"))
		{
			base.SetErrorCode(-2146233085);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArrayTypeMismatchException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error.</param>
		// Token: 0x060009C4 RID: 2500 RVA: 0x0001F787 File Offset: 0x0001D987
		[__DynamicallyInvokable]
		public ArrayTypeMismatchException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233085);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArrayTypeMismatchException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x060009C5 RID: 2501 RVA: 0x0001F79B File Offset: 0x0001D99B
		[__DynamicallyInvokable]
		public ArrayTypeMismatchException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233085);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArrayTypeMismatchException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x060009C6 RID: 2502 RVA: 0x0001F7B0 File Offset: 0x0001D9B0
		protected ArrayTypeMismatchException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
