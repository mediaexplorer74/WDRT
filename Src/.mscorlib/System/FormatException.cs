using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when the format of an argument is invalid, or when a composite format string is not well formed.</summary>
	// Token: 0x020000E4 RID: 228
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class FormatException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.FormatException" /> class.</summary>
		// Token: 0x06000E83 RID: 3715 RVA: 0x0002CCBF File Offset: 0x0002AEBF
		[__DynamicallyInvokable]
		public FormatException()
			: base(Environment.GetResourceString("Arg_FormatException"))
		{
			base.SetErrorCode(-2146233033);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.FormatException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x06000E84 RID: 3716 RVA: 0x0002CCDC File Offset: 0x0002AEDC
		[__DynamicallyInvokable]
		public FormatException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233033);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.FormatException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000E85 RID: 3717 RVA: 0x0002CCF0 File Offset: 0x0002AEF0
		[__DynamicallyInvokable]
		public FormatException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233033);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.FormatException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06000E86 RID: 3718 RVA: 0x0002CD05 File Offset: 0x0002AF05
		protected FormatException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
