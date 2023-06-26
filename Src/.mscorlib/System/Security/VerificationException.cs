using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security
{
	/// <summary>The exception that is thrown when the security policy requires code to be type safe and the verification process is unable to verify that the code is type safe.</summary>
	// Token: 0x020001F6 RID: 502
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class VerificationException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.VerificationException" /> class with default properties.</summary>
		// Token: 0x06001E3B RID: 7739 RVA: 0x00069AEA File Offset: 0x00067CEA
		[__DynamicallyInvokable]
		public VerificationException()
			: base(Environment.GetResourceString("Verification_Exception"))
		{
			base.SetErrorCode(-2146233075);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.VerificationException" /> class with an explanatory message.</summary>
		/// <param name="message">A message indicating the reason the exception occurred.</param>
		// Token: 0x06001E3C RID: 7740 RVA: 0x00069B07 File Offset: 0x00067D07
		[__DynamicallyInvokable]
		public VerificationException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233075);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.VerificationException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06001E3D RID: 7741 RVA: 0x00069B1B File Offset: 0x00067D1B
		[__DynamicallyInvokable]
		public VerificationException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233075);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.VerificationException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06001E3E RID: 7742 RVA: 0x00069B30 File Offset: 0x00067D30
		protected VerificationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
