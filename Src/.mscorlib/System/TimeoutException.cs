using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when the time allotted for a process or operation has expired.</summary>
	// Token: 0x02000142 RID: 322
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class TimeoutException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.TimeoutException" /> class.</summary>
		// Token: 0x06001341 RID: 4929 RVA: 0x000387DA File Offset: 0x000369DA
		[__DynamicallyInvokable]
		public TimeoutException()
			: base(Environment.GetResourceString("Arg_TimeoutException"))
		{
			base.SetErrorCode(-2146233083);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TimeoutException" /> class with the specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x06001342 RID: 4930 RVA: 0x000387F7 File Offset: 0x000369F7
		[__DynamicallyInvokable]
		public TimeoutException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233083);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TimeoutException" /> class with the specified error message and inner exception.</summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06001343 RID: 4931 RVA: 0x0003880B File Offset: 0x00036A0B
		[__DynamicallyInvokable]
		public TimeoutException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233083);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TimeoutException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination. The <paramref name="context" /> parameter is reserved for future use, and can be specified as <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is <see langword="null" />, or <see cref="P:System.Exception.HResult" /> is zero (0).</exception>
		// Token: 0x06001344 RID: 4932 RVA: 0x00038820 File Offset: 0x00036A20
		protected TimeoutException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
