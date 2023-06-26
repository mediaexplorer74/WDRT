using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is not enough memory to continue the execution of a program.</summary>
	// Token: 0x02000081 RID: 129
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class OutOfMemoryException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.OutOfMemoryException" /> class.</summary>
		// Token: 0x060006C7 RID: 1735 RVA: 0x000178FA File Offset: 0x00015AFA
		[__DynamicallyInvokable]
		public OutOfMemoryException()
			: base(Exception.GetMessageFromNativeResources(Exception.ExceptionMessageKind.OutOfMemory))
		{
			base.SetErrorCode(-2147024882);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OutOfMemoryException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x060006C8 RID: 1736 RVA: 0x00017913 File Offset: 0x00015B13
		[__DynamicallyInvokable]
		public OutOfMemoryException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147024882);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OutOfMemoryException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x060006C9 RID: 1737 RVA: 0x00017927 File Offset: 0x00015B27
		[__DynamicallyInvokable]
		public OutOfMemoryException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147024882);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OutOfMemoryException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x060006CA RID: 1738 RVA: 0x0001793C File Offset: 0x00015B3C
		protected OutOfMemoryException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
