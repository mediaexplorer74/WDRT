using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an attempt to unload an application domain fails.</summary>
	// Token: 0x020000B5 RID: 181
	[ComVisible(true)]
	[Serializable]
	public class CannotUnloadAppDomainException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CannotUnloadAppDomainException" /> class.</summary>
		// Token: 0x06000A83 RID: 2691 RVA: 0x00021815 File Offset: 0x0001FA15
		public CannotUnloadAppDomainException()
			: base(Environment.GetResourceString("Arg_CannotUnloadAppDomainException"))
		{
			base.SetErrorCode(-2146234347);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CannotUnloadAppDomainException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error.</param>
		// Token: 0x06000A84 RID: 2692 RVA: 0x00021832 File Offset: 0x0001FA32
		public CannotUnloadAppDomainException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146234347);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CannotUnloadAppDomainException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000A85 RID: 2693 RVA: 0x00021846 File Offset: 0x0001FA46
		public CannotUnloadAppDomainException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146234347);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CannotUnloadAppDomainException" /> class from serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06000A86 RID: 2694 RVA: 0x0002185B File Offset: 0x0001FA5B
		protected CannotUnloadAppDomainException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
