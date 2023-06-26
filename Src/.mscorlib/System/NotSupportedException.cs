using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an invoked method is not supported, or when there is an attempt to read, seek, or write to a stream that does not support the invoked functionality.</summary>
	// Token: 0x02000116 RID: 278
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class NotSupportedException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.NotSupportedException" /> class, setting the <see cref="P:System.Exception.Message" /> property of the new instance to a system-supplied message that describes the error. This message takes into account the current system culture.</summary>
		// Token: 0x06001082 RID: 4226 RVA: 0x00031641 File Offset: 0x0002F841
		[__DynamicallyInvokable]
		public NotSupportedException()
			: base(Environment.GetResourceString("Arg_NotSupportedException"))
		{
			base.SetErrorCode(-2146233067);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotSupportedException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		// Token: 0x06001083 RID: 4227 RVA: 0x0003165E File Offset: 0x0002F85E
		[__DynamicallyInvokable]
		public NotSupportedException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233067);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotSupportedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06001084 RID: 4228 RVA: 0x00031672 File Offset: 0x0002F872
		[__DynamicallyInvokable]
		public NotSupportedException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233067);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotSupportedException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06001085 RID: 4229 RVA: 0x00031687 File Offset: 0x0002F887
		protected NotSupportedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
