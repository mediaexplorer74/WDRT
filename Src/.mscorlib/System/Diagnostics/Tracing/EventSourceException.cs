using System;
using System.Runtime.Serialization;

namespace System.Diagnostics.Tracing
{
	/// <summary>The exception that is thrown when an error occurs during event tracing for Windows (ETW).</summary>
	// Token: 0x02000431 RID: 1073
	[__DynamicallyInvokable]
	[Serializable]
	public class EventSourceException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Tracing.EventSourceException" /> class.</summary>
		// Token: 0x060035A4 RID: 13732 RVA: 0x000D290C File Offset: 0x000D0B0C
		[__DynamicallyInvokable]
		public EventSourceException()
			: base(Environment.GetResourceString("EventSource_ListenerWriteFailure"))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Tracing.EventSourceException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x060035A5 RID: 13733 RVA: 0x000D291E File Offset: 0x000D0B1E
		[__DynamicallyInvokable]
		public EventSourceException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Tracing.EventSourceException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or <see langword="null" /> if no inner exception is specified.</param>
		// Token: 0x060035A6 RID: 13734 RVA: 0x000D2927 File Offset: 0x000D0B27
		[__DynamicallyInvokable]
		public EventSourceException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Tracing.EventSourceException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x060035A7 RID: 13735 RVA: 0x000D2931 File Offset: 0x000D0B31
		protected EventSourceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060035A8 RID: 13736 RVA: 0x000D293B File Offset: 0x000D0B3B
		internal EventSourceException(Exception innerException)
			: base(Environment.GetResourceString("EventSource_ListenerWriteFailure"), innerException)
		{
		}
	}
}
