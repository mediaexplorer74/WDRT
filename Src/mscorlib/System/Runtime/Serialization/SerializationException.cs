using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>The exception thrown when an error occurs during serialization or deserialization.</summary>
	// Token: 0x0200073D RID: 1853
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class SerializationException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.SerializationException" /> class with default properties.</summary>
		// Token: 0x060051EB RID: 20971 RVA: 0x001215B3 File Offset: 0x0011F7B3
		[__DynamicallyInvokable]
		public SerializationException()
			: base(SerializationException._nullMessage)
		{
			base.SetErrorCode(-2146233076);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.SerializationException" /> class with a specified message.</summary>
		/// <param name="message">Indicates the reason why the exception occurred.</param>
		// Token: 0x060051EC RID: 20972 RVA: 0x001215CB File Offset: 0x0011F7CB
		[__DynamicallyInvokable]
		public SerializationException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233076);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.SerializationException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x060051ED RID: 20973 RVA: 0x001215DF File Offset: 0x0011F7DF
		[__DynamicallyInvokable]
		public SerializationException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233076);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.SerializationException" /> class from serialized data.</summary>
		/// <param name="info">The serialization information object holding the serialized object data in the name-value form.</param>
		/// <param name="context">The contextual information about the source or destination of the exception.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060051EE RID: 20974 RVA: 0x001215F4 File Offset: 0x0011F7F4
		protected SerializationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0400244C RID: 9292
		private static string _nullMessage = Environment.GetResourceString("Arg_SerializationException");
	}
}
