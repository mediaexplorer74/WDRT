using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an array with the wrong number of dimensions is passed to a method.</summary>
	// Token: 0x02000127 RID: 295
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class RankException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.RankException" /> class.</summary>
		// Token: 0x06001101 RID: 4353 RVA: 0x00033289 File Offset: 0x00031489
		[__DynamicallyInvokable]
		public RankException()
			: base(Environment.GetResourceString("Arg_RankException"))
		{
			base.SetErrorCode(-2146233065);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.RankException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error.</param>
		// Token: 0x06001102 RID: 4354 RVA: 0x000332A6 File Offset: 0x000314A6
		[__DynamicallyInvokable]
		public RankException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233065);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.RankException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06001103 RID: 4355 RVA: 0x000332BA File Offset: 0x000314BA
		[__DynamicallyInvokable]
		public RankException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233065);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.RankException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06001104 RID: 4356 RVA: 0x000332CF File Offset: 0x000314CF
		protected RankException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
