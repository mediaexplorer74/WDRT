using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown for invalid casting or explicit conversion.</summary>
	// Token: 0x020000FF RID: 255
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class InvalidCastException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidCastException" /> class.</summary>
		// Token: 0x06000FAD RID: 4013 RVA: 0x000301C1 File Offset: 0x0002E3C1
		[__DynamicallyInvokable]
		public InvalidCastException()
			: base(Environment.GetResourceString("Arg_InvalidCastException"))
		{
			base.SetErrorCode(-2147467262);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidCastException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x06000FAE RID: 4014 RVA: 0x000301DE File Offset: 0x0002E3DE
		[__DynamicallyInvokable]
		public InvalidCastException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147467262);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidCastException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000FAF RID: 4015 RVA: 0x000301F2 File Offset: 0x0002E3F2
		[__DynamicallyInvokable]
		public InvalidCastException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147467262);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidCastException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06000FB0 RID: 4016 RVA: 0x00030207 File Offset: 0x0002E407
		protected InvalidCastException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidCastException" /> class with a specified message and error code.</summary>
		/// <param name="message">The message that indicates the reason the exception occurred.</param>
		/// <param name="errorCode">The error code (HRESULT) value associated with the exception.</param>
		// Token: 0x06000FB1 RID: 4017 RVA: 0x00030211 File Offset: 0x0002E411
		[__DynamicallyInvokable]
		public InvalidCastException(string message, int errorCode)
			: base(message)
		{
			base.SetErrorCode(errorCode);
		}
	}
}
