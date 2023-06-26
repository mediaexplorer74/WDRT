using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is an internal error in the execution engine of the common language runtime. This class cannot be inherited.</summary>
	// Token: 0x02000084 RID: 132
	[Obsolete("This type previously indicated an unspecified fatal error in the runtime. The runtime no longer raises this exception so this type is obsolete.")]
	[ComVisible(true)]
	[Serializable]
	public sealed class ExecutionEngineException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ExecutionEngineException" /> class.</summary>
		// Token: 0x060006D3 RID: 1747 RVA: 0x000179E6 File Offset: 0x00015BE6
		public ExecutionEngineException()
			: base(Environment.GetResourceString("Arg_ExecutionEngineException"))
		{
			base.SetErrorCode(-2146233082);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ExecutionEngineException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x060006D4 RID: 1748 RVA: 0x00017A03 File Offset: 0x00015C03
		public ExecutionEngineException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233082);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ExecutionEngineException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x060006D5 RID: 1749 RVA: 0x00017A17 File Offset: 0x00015C17
		public ExecutionEngineException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233082);
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00017A2C File Offset: 0x00015C2C
		internal ExecutionEngineException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
