using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	/// <summary>The exception that is thrown when the number of parameters for an invocation does not match the number expected. This class cannot be inherited.</summary>
	// Token: 0x02000621 RID: 1569
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class TargetParameterCountException : ApplicationException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TargetParameterCountException" /> class with an empty message string and the root cause of the exception.</summary>
		// Token: 0x060048D3 RID: 18643 RVA: 0x0010913B File Offset: 0x0010733B
		[__DynamicallyInvokable]
		public TargetParameterCountException()
			: base(Environment.GetResourceString("Arg_TargetParameterCountException"))
		{
			base.SetErrorCode(-2147352562);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TargetParameterCountException" /> class with its message string set to the given message and the root cause exception.</summary>
		/// <param name="message">A <see langword="String" /> describing the reason this exception was thrown.</param>
		// Token: 0x060048D4 RID: 18644 RVA: 0x00109158 File Offset: 0x00107358
		[__DynamicallyInvokable]
		public TargetParameterCountException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147352562);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TargetParameterCountException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x060048D5 RID: 18645 RVA: 0x0010916C File Offset: 0x0010736C
		[__DynamicallyInvokable]
		public TargetParameterCountException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2147352562);
		}

		// Token: 0x060048D6 RID: 18646 RVA: 0x00109181 File Offset: 0x00107381
		internal TargetParameterCountException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
