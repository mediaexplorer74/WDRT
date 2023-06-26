using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>Serves as the base class for application-defined exceptions.</summary>
	// Token: 0x02000090 RID: 144
	[ComVisible(true)]
	[Serializable]
	public class ApplicationException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ApplicationException" /> class.</summary>
		// Token: 0x0600076D RID: 1901 RVA: 0x00019F8F File Offset: 0x0001818F
		public ApplicationException()
			: base(Environment.GetResourceString("Arg_ApplicationException"))
		{
			base.SetErrorCode(-2146232832);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ApplicationException" /> class with a specified error message.</summary>
		/// <param name="message">A message that describes the error.</param>
		// Token: 0x0600076E RID: 1902 RVA: 0x00019FAC File Offset: 0x000181AC
		public ApplicationException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146232832);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ApplicationException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x0600076F RID: 1903 RVA: 0x00019FC0 File Offset: 0x000181C0
		public ApplicationException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146232832);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ApplicationException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06000770 RID: 1904 RVA: 0x00019FD5 File Offset: 0x000181D5
		protected ApplicationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
