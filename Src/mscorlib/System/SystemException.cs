using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>Serves as the base class for system exceptions namespace.</summary>
	// Token: 0x02000080 RID: 128
	[ComVisible(true)]
	[Serializable]
	public class SystemException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.SystemException" /> class.</summary>
		// Token: 0x060006C3 RID: 1731 RVA: 0x000178AA File Offset: 0x00015AAA
		public SystemException()
			: base(Environment.GetResourceString("Arg_SystemException"))
		{
			base.SetErrorCode(-2146233087);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.SystemException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x060006C4 RID: 1732 RVA: 0x000178C7 File Offset: 0x00015AC7
		public SystemException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233087);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.SystemException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x060006C5 RID: 1733 RVA: 0x000178DB File Offset: 0x00015ADB
		public SystemException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233087);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.SystemException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x060006C6 RID: 1734 RVA: 0x000178F0 File Offset: 0x00015AF0
		protected SystemException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
