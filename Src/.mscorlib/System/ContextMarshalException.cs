using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an attempt to marshal an object across a context boundary fails.</summary>
	// Token: 0x020000CA RID: 202
	[ComVisible(true)]
	[Serializable]
	public class ContextMarshalException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ContextMarshalException" /> class with default properties.</summary>
		// Token: 0x06000BA4 RID: 2980 RVA: 0x000250BF File Offset: 0x000232BF
		public ContextMarshalException()
			: base(Environment.GetResourceString("Arg_ContextMarshalException"))
		{
			base.SetErrorCode(-2146233084);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ContextMarshalException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06000BA5 RID: 2981 RVA: 0x000250DC File Offset: 0x000232DC
		public ContextMarshalException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233084);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ContextMarshalException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000BA6 RID: 2982 RVA: 0x000250F0 File Offset: 0x000232F0
		public ContextMarshalException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233084);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ContextMarshalException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		// Token: 0x06000BA7 RID: 2983 RVA: 0x00025105 File Offset: 0x00023305
		protected ContextMarshalException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
