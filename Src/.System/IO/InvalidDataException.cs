using System;
using System.Runtime.Serialization;

namespace System.IO
{
	/// <summary>The exception that is thrown when a data stream is in an invalid format.</summary>
	// Token: 0x020003F9 RID: 1017
	[global::__DynamicallyInvokable]
	[Serializable]
	public sealed class InvalidDataException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.InvalidDataException" /> class.</summary>
		// Token: 0x06002644 RID: 9796 RVA: 0x000B08D7 File Offset: 0x000AEAD7
		[global::__DynamicallyInvokable]
		public InvalidDataException()
			: base(SR.GetString("GenericInvalidData"))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.InvalidDataException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06002645 RID: 9797 RVA: 0x000B08E9 File Offset: 0x000AEAE9
		[global::__DynamicallyInvokable]
		public InvalidDataException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.InvalidDataException" /> class with a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06002646 RID: 9798 RVA: 0x000B08F2 File Offset: 0x000AEAF2
		[global::__DynamicallyInvokable]
		public InvalidDataException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x000B08FC File Offset: 0x000AEAFC
		internal InvalidDataException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
