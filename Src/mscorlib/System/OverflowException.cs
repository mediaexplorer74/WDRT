using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an arithmetic, casting, or conversion operation in a checked context results in an overflow.</summary>
	// Token: 0x0200011E RID: 286
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class OverflowException : ArithmeticException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.OverflowException" /> class.</summary>
		// Token: 0x060010D6 RID: 4310 RVA: 0x00032C7F File Offset: 0x00030E7F
		[__DynamicallyInvokable]
		public OverflowException()
			: base(Environment.GetResourceString("Arg_OverflowException"))
		{
			base.SetErrorCode(-2146233066);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OverflowException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x060010D7 RID: 4311 RVA: 0x00032C9C File Offset: 0x00030E9C
		[__DynamicallyInvokable]
		public OverflowException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233066);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OverflowException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x060010D8 RID: 4312 RVA: 0x00032CB0 File Offset: 0x00030EB0
		[__DynamicallyInvokable]
		public OverflowException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233066);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OverflowException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x060010D9 RID: 4313 RVA: 0x00032CC5 File Offset: 0x00030EC5
		protected OverflowException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
