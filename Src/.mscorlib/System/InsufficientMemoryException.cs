using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a check for sufficient available memory fails. This class cannot be inherited.</summary>
	// Token: 0x020000F5 RID: 245
	[Serializable]
	public sealed class InsufficientMemoryException : OutOfMemoryException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.InsufficientMemoryException" /> class with a system-supplied message that describes the error.</summary>
		// Token: 0x06000F09 RID: 3849 RVA: 0x0002EDA5 File Offset: 0x0002CFA5
		public InsufficientMemoryException()
			: base(Exception.GetMessageFromNativeResources(Exception.ExceptionMessageKind.OutOfMemory))
		{
			base.SetErrorCode(-2146233027);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InsufficientMemoryException" /> class with a specified message that describes the error.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		// Token: 0x06000F0A RID: 3850 RVA: 0x0002EDBE File Offset: 0x0002CFBE
		public InsufficientMemoryException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233027);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InsufficientMemoryException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000F0B RID: 3851 RVA: 0x0002EDD2 File Offset: 0x0002CFD2
		public InsufficientMemoryException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233027);
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x0002EDE7 File Offset: 0x0002CFE7
		private InsufficientMemoryException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
