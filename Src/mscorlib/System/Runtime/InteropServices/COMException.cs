using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security;
using Microsoft.Win32;

namespace System.Runtime.InteropServices
{
	/// <summary>The exception that is thrown when an unrecognized HRESULT is returned from a COM method call.</summary>
	// Token: 0x02000942 RID: 2370
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class COMException : ExternalException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.COMException" /> class with default values.</summary>
		// Token: 0x0600607C RID: 24700 RVA: 0x0014D755 File Offset: 0x0014B955
		[__DynamicallyInvokable]
		public COMException()
			: base(Environment.GetResourceString("Arg_COMException"))
		{
			base.SetErrorCode(-2147467259);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.COMException" /> class with a specified message.</summary>
		/// <param name="message">The message that indicates the reason for the exception.</param>
		// Token: 0x0600607D RID: 24701 RVA: 0x0014D772 File Offset: 0x0014B972
		[__DynamicallyInvokable]
		public COMException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147467259);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.COMException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x0600607E RID: 24702 RVA: 0x0014D786 File Offset: 0x0014B986
		[__DynamicallyInvokable]
		public COMException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2147467259);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.COMException" /> class with a specified message and error code.</summary>
		/// <param name="message">The message that indicates the reason the exception occurred.</param>
		/// <param name="errorCode">The error code (HRESULT) value associated with this exception.</param>
		// Token: 0x0600607F RID: 24703 RVA: 0x0014D79B File Offset: 0x0014B99B
		[__DynamicallyInvokable]
		public COMException(string message, int errorCode)
			: base(message)
		{
			base.SetErrorCode(errorCode);
		}

		// Token: 0x06006080 RID: 24704 RVA: 0x0014D7AB File Offset: 0x0014B9AB
		[SecuritySafeCritical]
		internal COMException(int hresult)
			: base(Win32Native.GetMessage(hresult))
		{
			base.SetErrorCode(hresult);
		}

		// Token: 0x06006081 RID: 24705 RVA: 0x0014D7C0 File Offset: 0x0014B9C0
		internal COMException(string message, int hresult, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(hresult);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.COMException" /> class from serialization data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that supplies the contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x06006082 RID: 24706 RVA: 0x0014D7D1 File Offset: 0x0014B9D1
		protected COMException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Converts the contents of the exception to a string.</summary>
		/// <returns>A string containing the <see cref="P:System.Exception.HResult" />, <see cref="P:System.Exception.Message" />, <see cref="P:System.Exception.InnerException" />, and <see cref="P:System.Exception.StackTrace" /> properties of the exception.</returns>
		// Token: 0x06006083 RID: 24707 RVA: 0x0014D7DC File Offset: 0x0014B9DC
		public override string ToString()
		{
			string message = this.Message;
			string text = base.GetType().ToString();
			string text2 = text + " (0x" + base.HResult.ToString("X8", CultureInfo.InvariantCulture) + ")";
			if (message != null && message.Length > 0)
			{
				text2 = text2 + ": " + message;
			}
			Exception innerException = base.InnerException;
			if (innerException != null)
			{
				text2 = text2 + " ---> " + innerException.ToString();
			}
			if (this.StackTrace != null)
			{
				text2 = text2 + Environment.NewLine + this.StackTrace;
			}
			return text2;
		}
	}
}
