using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a requested method or operation is not implemented.</summary>
	// Token: 0x02000115 RID: 277
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class NotImplementedException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.NotImplementedException" /> class with default properties.</summary>
		// Token: 0x0600107E RID: 4222 RVA: 0x000315F1 File Offset: 0x0002F7F1
		[__DynamicallyInvokable]
		public NotImplementedException()
			: base(Environment.GetResourceString("Arg_NotImplementedException"))
		{
			base.SetErrorCode(-2147467263);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotImplementedException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x0600107F RID: 4223 RVA: 0x0003160E File Offset: 0x0002F80E
		[__DynamicallyInvokable]
		public NotImplementedException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147467263);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotImplementedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06001080 RID: 4224 RVA: 0x00031622 File Offset: 0x0002F822
		[__DynamicallyInvokable]
		public NotImplementedException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2147467263);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotImplementedException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		// Token: 0x06001081 RID: 4225 RVA: 0x00031637 File Offset: 0x0002F837
		protected NotImplementedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
