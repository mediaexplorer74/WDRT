using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown for errors in an arithmetic, casting, or conversion operation.</summary>
	// Token: 0x020000AA RID: 170
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class ArithmeticException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ArithmeticException" /> class.</summary>
		// Token: 0x060009BF RID: 2495 RVA: 0x0001F71A File Offset: 0x0001D91A
		[__DynamicallyInvokable]
		public ArithmeticException()
			: base(Environment.GetResourceString("Arg_ArithmeticException"))
		{
			base.SetErrorCode(-2147024362);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArithmeticException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error.</param>
		// Token: 0x060009C0 RID: 2496 RVA: 0x0001F737 File Offset: 0x0001D937
		[__DynamicallyInvokable]
		public ArithmeticException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147024362);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArithmeticException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x060009C1 RID: 2497 RVA: 0x0001F74B File Offset: 0x0001D94B
		[__DynamicallyInvokable]
		public ArithmeticException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147024362);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArithmeticException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x060009C2 RID: 2498 RVA: 0x0001F760 File Offset: 0x0001D960
		protected ArithmeticException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
