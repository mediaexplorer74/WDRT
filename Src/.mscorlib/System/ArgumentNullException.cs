using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>The exception that is thrown when a null reference (<see langword="Nothing" /> in Visual Basic) is passed to a method that does not accept it as a valid argument.</summary>
	// Token: 0x020000A7 RID: 167
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class ArgumentNullException : ArgumentException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentNullException" /> class.</summary>
		// Token: 0x060009A2 RID: 2466 RVA: 0x0001F490 File Offset: 0x0001D690
		[__DynamicallyInvokable]
		public ArgumentNullException()
			: base(Environment.GetResourceString("ArgumentNull_Generic"))
		{
			base.SetErrorCode(-2147467261);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentNullException" /> class with the name of the parameter that causes this exception.</summary>
		/// <param name="paramName">The name of the parameter that caused the exception.</param>
		// Token: 0x060009A3 RID: 2467 RVA: 0x0001F4AD File Offset: 0x0001D6AD
		[__DynamicallyInvokable]
		public ArgumentNullException(string paramName)
			: base(Environment.GetResourceString("ArgumentNull_Generic"), paramName)
		{
			base.SetErrorCode(-2147467261);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentNullException" /> class with a specified error message and the exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for this exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified.</param>
		// Token: 0x060009A4 RID: 2468 RVA: 0x0001F4CB File Offset: 0x0001D6CB
		[__DynamicallyInvokable]
		public ArgumentNullException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2147467261);
		}

		/// <summary>Initializes an instance of the <see cref="T:System.ArgumentNullException" /> class with a specified error message and the name of the parameter that causes this exception.</summary>
		/// <param name="paramName">The name of the parameter that caused the exception.</param>
		/// <param name="message">A message that describes the error.</param>
		// Token: 0x060009A5 RID: 2469 RVA: 0x0001F4E0 File Offset: 0x0001D6E0
		[__DynamicallyInvokable]
		public ArgumentNullException(string paramName, string message)
			: base(message, paramName)
		{
			base.SetErrorCode(-2147467261);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgumentNullException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">An object that describes the source or destination of the serialized data.</param>
		// Token: 0x060009A6 RID: 2470 RVA: 0x0001F4F5 File Offset: 0x0001D6F5
		[SecurityCritical]
		protected ArgumentNullException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
