using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	/// <summary>The exception thrown when the rank of an incoming <see langword="SAFEARRAY" /> does not match the rank specified in the managed signature.</summary>
	// Token: 0x02000975 RID: 2421
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class SafeArrayRankMismatchException : SystemException
	{
		/// <summary>Initializes a new instance of the <see langword="SafeArrayTypeMismatchException" /> class with default values.</summary>
		// Token: 0x06006276 RID: 25206 RVA: 0x0015270A File Offset: 0x0015090A
		[__DynamicallyInvokable]
		public SafeArrayRankMismatchException()
			: base(Environment.GetResourceString("Arg_SafeArrayRankMismatchException"))
		{
			base.SetErrorCode(-2146233032);
		}

		/// <summary>Initializes a new instance of the <see langword="SafeArrayRankMismatchException" /> class with the specified message.</summary>
		/// <param name="message">The message that indicates the reason for the exception.</param>
		// Token: 0x06006277 RID: 25207 RVA: 0x00152727 File Offset: 0x00150927
		[__DynamicallyInvokable]
		public SafeArrayRankMismatchException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233032);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.SafeArrayRankMismatchException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06006278 RID: 25208 RVA: 0x0015273B File Offset: 0x0015093B
		[__DynamicallyInvokable]
		public SafeArrayRankMismatchException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233032);
		}

		/// <summary>Initializes a new instance of the <see langword="SafeArrayTypeMismatchException" /> class from serialization data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x06006279 RID: 25209 RVA: 0x00152750 File Offset: 0x00150950
		protected SafeArrayRankMismatchException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
