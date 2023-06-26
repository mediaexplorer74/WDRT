using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security.Policy
{
	/// <summary>The exception that is thrown when policy forbids code to run.</summary>
	// Token: 0x02000361 RID: 865
	[ComVisible(true)]
	[Serializable]
	public class PolicyException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.PolicyException" /> class with default properties.</summary>
		// Token: 0x06002AEA RID: 10986 RVA: 0x000A0057 File Offset: 0x0009E257
		public PolicyException()
			: base(Environment.GetResourceString("Policy_Default"))
		{
			base.HResult = -2146233322;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.PolicyException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06002AEB RID: 10987 RVA: 0x000A0074 File Offset: 0x0009E274
		public PolicyException(string message)
			: base(message)
		{
			base.HResult = -2146233322;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.PolicyException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="exception">The exception that is the cause of the current exception. If the <paramref name="exception" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06002AEC RID: 10988 RVA: 0x000A0088 File Offset: 0x0009E288
		public PolicyException(string message, Exception exception)
			: base(message, exception)
		{
			base.HResult = -2146233322;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.PolicyException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06002AED RID: 10989 RVA: 0x000A009D File Offset: 0x0009E29D
		protected PolicyException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06002AEE RID: 10990 RVA: 0x000A00A7 File Offset: 0x0009E2A7
		internal PolicyException(string message, int hresult)
			: base(message)
		{
			base.HResult = hresult;
		}

		// Token: 0x06002AEF RID: 10991 RVA: 0x000A00B7 File Offset: 0x0009E2B7
		internal PolicyException(string message, int hresult, Exception exception)
			: base(message, exception)
		{
			base.HResult = hresult;
		}
	}
}
