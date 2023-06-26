using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an attempt is made to access an element of an array or collection with an index that is outside its bounds.</summary>
	// Token: 0x020000F1 RID: 241
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class IndexOutOfRangeException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IndexOutOfRangeException" /> class.</summary>
		// Token: 0x06000F00 RID: 3840 RVA: 0x0002ED55 File Offset: 0x0002CF55
		[__DynamicallyInvokable]
		public IndexOutOfRangeException()
			: base(Environment.GetResourceString("Arg_IndexOutOfRangeException"))
		{
			base.SetErrorCode(-2146233080);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IndexOutOfRangeException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x06000F01 RID: 3841 RVA: 0x0002ED72 File Offset: 0x0002CF72
		[__DynamicallyInvokable]
		public IndexOutOfRangeException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233080);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IndexOutOfRangeException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000F02 RID: 3842 RVA: 0x0002ED86 File Offset: 0x0002CF86
		[__DynamicallyInvokable]
		public IndexOutOfRangeException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233080);
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x0002ED9B File Offset: 0x0002CF9B
		internal IndexOutOfRangeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
