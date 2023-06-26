using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	/// <summary>The exception that is thrown by the marshaler when it encounters a <see cref="T:System.Runtime.InteropServices.MarshalAsAttribute" /> it does not support.</summary>
	// Token: 0x02000950 RID: 2384
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class MarshalDirectiveException : SystemException
	{
		/// <summary>Initializes a new instance of the <see langword="MarshalDirectiveException" /> class with default properties.</summary>
		// Token: 0x060061C4 RID: 25028 RVA: 0x0014FCA0 File Offset: 0x0014DEA0
		[__DynamicallyInvokable]
		public MarshalDirectiveException()
			: base(Environment.GetResourceString("Arg_MarshalDirectiveException"))
		{
			base.SetErrorCode(-2146233035);
		}

		/// <summary>Initializes a new instance of the <see langword="MarshalDirectiveException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that specifies the reason for the exception.</param>
		// Token: 0x060061C5 RID: 25029 RVA: 0x0014FCBD File Offset: 0x0014DEBD
		[__DynamicallyInvokable]
		public MarshalDirectiveException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233035);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.MarshalDirectiveException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x060061C6 RID: 25030 RVA: 0x0014FCD1 File Offset: 0x0014DED1
		[__DynamicallyInvokable]
		public MarshalDirectiveException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233035);
		}

		/// <summary>Initializes a new instance of the <see langword="MarshalDirectiveException" /> class from serialization data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x060061C7 RID: 25031 RVA: 0x0014FCE6 File Offset: 0x0014DEE6
		protected MarshalDirectiveException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
