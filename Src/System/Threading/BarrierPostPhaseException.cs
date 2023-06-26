using System;
using System.Runtime.Serialization;
using System.Security;

namespace System.Threading
{
	/// <summary>The exception that is thrown when the post-phase action of a <see cref="T:System.Threading.Barrier" /> fails</summary>
	// Token: 0x020003D5 RID: 981
	[global::__DynamicallyInvokable]
	[Serializable]
	public class BarrierPostPhaseException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.BarrierPostPhaseException" /> class with a system-supplied message that describes the error.</summary>
		// Token: 0x060025B1 RID: 9649 RVA: 0x000AF2D0 File Offset: 0x000AD4D0
		[global::__DynamicallyInvokable]
		public BarrierPostPhaseException()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.BarrierPostPhaseException" /> class with the specified inner exception.</summary>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		// Token: 0x060025B2 RID: 9650 RVA: 0x000AF2D9 File Offset: 0x000AD4D9
		[global::__DynamicallyInvokable]
		public BarrierPostPhaseException(Exception innerException)
			: this(null, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.BarrierPostPhaseException" /> class with a specified message that describes the error.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		// Token: 0x060025B3 RID: 9651 RVA: 0x000AF2E3 File Offset: 0x000AD4E3
		[global::__DynamicallyInvokable]
		public BarrierPostPhaseException(string message)
			: this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.BarrierPostPhaseException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x060025B4 RID: 9652 RVA: 0x000AF2ED File Offset: 0x000AD4ED
		[global::__DynamicallyInvokable]
		public BarrierPostPhaseException(string message, Exception innerException)
			: base((message == null) ? SR.GetString("BarrierPostPhaseException") : message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.BarrierPostPhaseException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x060025B5 RID: 9653 RVA: 0x000AF306 File Offset: 0x000AD506
		[SecurityCritical]
		protected BarrierPostPhaseException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
