using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security.Cryptography
{
	/// <summary>The exception that is thrown when an unexpected operation occurs during a cryptographic operation.</summary>
	// Token: 0x02000244 RID: 580
	[ComVisible(true)]
	[Serializable]
	public class CryptographicUnexpectedOperationException : CryptographicException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException" /> class with default properties.</summary>
		// Token: 0x060020B1 RID: 8369 RVA: 0x00072920 File Offset: 0x00070B20
		public CryptographicUnexpectedOperationException()
		{
			base.SetErrorCode(-2146233295);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x060020B2 RID: 8370 RVA: 0x00072933 File Offset: 0x00070B33
		public CryptographicUnexpectedOperationException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233295);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException" /> class with a specified error message in the specified format.</summary>
		/// <param name="format">The format used to output the error message.</param>
		/// <param name="insert">The error message that explains the reason for the exception.</param>
		// Token: 0x060020B3 RID: 8371 RVA: 0x00072947 File Offset: 0x00070B47
		public CryptographicUnexpectedOperationException(string format, string insert)
			: base(string.Format(CultureInfo.CurrentCulture, format, insert))
		{
			base.SetErrorCode(-2146233295);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x060020B4 RID: 8372 RVA: 0x00072966 File Offset: 0x00070B66
		public CryptographicUnexpectedOperationException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233295);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x060020B5 RID: 8373 RVA: 0x0007297B File Offset: 0x00070B7B
		protected CryptographicUnexpectedOperationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
