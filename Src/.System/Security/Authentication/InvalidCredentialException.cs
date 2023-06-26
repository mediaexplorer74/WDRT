using System;
using System.Runtime.Serialization;

namespace System.Security.Authentication
{
	/// <summary>The exception that is thrown when authentication fails for an authentication stream and cannot be retried.</summary>
	// Token: 0x0200043B RID: 1083
	[Serializable]
	public class InvalidCredentialException : AuthenticationException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.InvalidCredentialException" /> class with no message.</summary>
		// Token: 0x06002871 RID: 10353 RVA: 0x000B9C93 File Offset: 0x000B7E93
		public InvalidCredentialException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.InvalidCredentialException" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance that contains the information required to deserialize the new <see cref="T:System.Security.Authentication.InvalidCredentialException" /> instance.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> instance.</param>
		// Token: 0x06002872 RID: 10354 RVA: 0x000B9C9B File Offset: 0x000B7E9B
		protected InvalidCredentialException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.InvalidCredentialException" /> class with the specified message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the authentication failure.</param>
		// Token: 0x06002873 RID: 10355 RVA: 0x000B9CA5 File Offset: 0x000B7EA5
		public InvalidCredentialException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.InvalidCredentialException" /> class with the specified message and inner exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the authentication failure.</param>
		/// <param name="innerException">The <see cref="T:System.Exception" /> that is the cause of the current exception.</param>
		// Token: 0x06002874 RID: 10356 RVA: 0x000B9CAE File Offset: 0x000B7EAE
		public InvalidCredentialException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
