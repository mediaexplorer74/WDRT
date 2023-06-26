using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO
{
	/// <summary>The exception that is thrown when a path or fully qualified file name is longer than the system-defined maximum length.</summary>
	// Token: 0x0200019D RID: 413
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class PathTooLongException : IOException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.PathTooLongException" /> class with its HRESULT set to COR_E_PATHTOOLONG.</summary>
		// Token: 0x0600195E RID: 6494 RVA: 0x00054868 File Offset: 0x00052A68
		[__DynamicallyInvokable]
		public PathTooLongException()
			: base(Environment.GetResourceString("IO.PathTooLong"))
		{
			base.SetErrorCode(-2147024690);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.PathTooLongException" /> class with its message string set to <paramref name="message" /> and its HRESULT set to COR_E_PATHTOOLONG.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		// Token: 0x0600195F RID: 6495 RVA: 0x00054885 File Offset: 0x00052A85
		[__DynamicallyInvokable]
		public PathTooLongException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147024690);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.PathTooLongException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06001960 RID: 6496 RVA: 0x00054899 File Offset: 0x00052A99
		[__DynamicallyInvokable]
		public PathTooLongException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147024690);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.PathTooLongException" /> class with the specified serialization and context information.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		// Token: 0x06001961 RID: 6497 RVA: 0x000548AE File Offset: 0x00052AAE
		protected PathTooLongException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
