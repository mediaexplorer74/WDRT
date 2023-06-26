using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	/// <summary>The exception that is thrown by methods invoked through reflection. This class cannot be inherited.</summary>
	// Token: 0x02000620 RID: 1568
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class TargetInvocationException : ApplicationException
	{
		// Token: 0x060048CE RID: 18638 RVA: 0x001090CD File Offset: 0x001072CD
		private TargetInvocationException()
			: base(Environment.GetResourceString("Arg_TargetInvocationException"))
		{
			base.SetErrorCode(-2146232828);
		}

		// Token: 0x060048CF RID: 18639 RVA: 0x001090EA File Offset: 0x001072EA
		private TargetInvocationException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146232828);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TargetInvocationException" /> class with a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x060048D0 RID: 18640 RVA: 0x001090FE File Offset: 0x001072FE
		[__DynamicallyInvokable]
		public TargetInvocationException(Exception inner)
			: base(Environment.GetResourceString("Arg_TargetInvocationException"), inner)
		{
			base.SetErrorCode(-2146232828);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TargetInvocationException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x060048D1 RID: 18641 RVA: 0x0010911C File Offset: 0x0010731C
		[__DynamicallyInvokable]
		public TargetInvocationException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146232828);
		}

		// Token: 0x060048D2 RID: 18642 RVA: 0x00109131 File Offset: 0x00107331
		internal TargetInvocationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
