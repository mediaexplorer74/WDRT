using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is an attempt to divide an integral or <see cref="T:System.Decimal" /> value by zero.</summary>
	// Token: 0x020000D6 RID: 214
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class DivideByZeroException : ArithmeticException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.DivideByZeroException" /> class.</summary>
		// Token: 0x06000DAF RID: 3503 RVA: 0x0002A492 File Offset: 0x00028692
		[__DynamicallyInvokable]
		public DivideByZeroException()
			: base(Environment.GetResourceString("Arg_DivideByZero"))
		{
			base.SetErrorCode(-2147352558);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DivideByZeroException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error.</param>
		// Token: 0x06000DB0 RID: 3504 RVA: 0x0002A4AF File Offset: 0x000286AF
		[__DynamicallyInvokable]
		public DivideByZeroException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147352558);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DivideByZeroException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000DB1 RID: 3505 RVA: 0x0002A4C3 File Offset: 0x000286C3
		[__DynamicallyInvokable]
		public DivideByZeroException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147352558);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DivideByZeroException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06000DB2 RID: 3506 RVA: 0x0002A4D8 File Offset: 0x000286D8
		protected DivideByZeroException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
