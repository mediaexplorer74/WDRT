using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO
{
	/// <summary>The exception that is thrown when reading is attempted past the end of a stream.</summary>
	// Token: 0x02000181 RID: 385
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class EndOfStreamException : IOException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.EndOfStreamException" /> class with its message string set to a system-supplied message and its HRESULT set to COR_E_ENDOFSTREAM.</summary>
		// Token: 0x06001798 RID: 6040 RVA: 0x0004BB59 File Offset: 0x00049D59
		[__DynamicallyInvokable]
		public EndOfStreamException()
			: base(Environment.GetResourceString("Arg_EndOfStreamException"))
		{
			base.SetErrorCode(-2147024858);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.EndOfStreamException" /> class with its message string set to <paramref name="message" /> and its HRESULT set to COR_E_ENDOFSTREAM.</summary>
		/// <param name="message">A string that describes the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		// Token: 0x06001799 RID: 6041 RVA: 0x0004BB76 File Offset: 0x00049D76
		[__DynamicallyInvokable]
		public EndOfStreamException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147024858);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.EndOfStreamException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">A string that describes the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x0600179A RID: 6042 RVA: 0x0004BB8A File Offset: 0x00049D8A
		[__DynamicallyInvokable]
		public EndOfStreamException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147024858);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.EndOfStreamException" /> class with the specified serialization and context information.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		// Token: 0x0600179B RID: 6043 RVA: 0x0004BB9F File Offset: 0x00049D9F
		protected EndOfStreamException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
