using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is an attempt to dereference a null object reference.</summary>
	// Token: 0x02000117 RID: 279
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class NullReferenceException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.NullReferenceException" /> class, setting the <see cref="P:System.Exception.Message" /> property of the new instance to a system-supplied message that describes the error, such as "The value 'null' was found where an instance of an object was required." This message takes into account the current system culture.</summary>
		// Token: 0x06001086 RID: 4230 RVA: 0x00031691 File Offset: 0x0002F891
		[__DynamicallyInvokable]
		public NullReferenceException()
			: base(Environment.GetResourceString("Arg_NullReferenceException"))
		{
			base.SetErrorCode(-2147467261);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NullReferenceException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		// Token: 0x06001087 RID: 4231 RVA: 0x000316AE File Offset: 0x0002F8AE
		[__DynamicallyInvokable]
		public NullReferenceException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147467261);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NullReferenceException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06001088 RID: 4232 RVA: 0x000316C2 File Offset: 0x0002F8C2
		[__DynamicallyInvokable]
		public NullReferenceException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147467261);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NullReferenceException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06001089 RID: 4233 RVA: 0x000316D7 File Offset: 0x0002F8D7
		protected NullReferenceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
