using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	/// <summary>The base exception type for all COM interop exceptions and structured exception handling (SEH) exceptions.</summary>
	// Token: 0x02000944 RID: 2372
	[ComVisible(true)]
	[Serializable]
	public class ExternalException : SystemException
	{
		/// <summary>Initializes a new instance of the <see langword="ExternalException" /> class with default properties.</summary>
		// Token: 0x06006090 RID: 24720 RVA: 0x0014D93C File Offset: 0x0014BB3C
		public ExternalException()
			: base(Environment.GetResourceString("Arg_ExternalException"))
		{
			base.SetErrorCode(-2147467259);
		}

		/// <summary>Initializes a new instance of the <see langword="ExternalException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that specifies the reason for the exception.</param>
		// Token: 0x06006091 RID: 24721 RVA: 0x0014D959 File Offset: 0x0014BB59
		public ExternalException(string message)
			: base(message)
		{
			base.SetErrorCode(-2147467259);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ExternalException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06006092 RID: 24722 RVA: 0x0014D96D File Offset: 0x0014BB6D
		public ExternalException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2147467259);
		}

		/// <summary>Initializes a new instance of the <see langword="ExternalException" /> class with a specified error message and the HRESULT of the error.</summary>
		/// <param name="message">The error message that specifies the reason for the exception.</param>
		/// <param name="errorCode">The HRESULT of the error.</param>
		// Token: 0x06006093 RID: 24723 RVA: 0x0014D982 File Offset: 0x0014BB82
		public ExternalException(string message, int errorCode)
			: base(message)
		{
			base.SetErrorCode(errorCode);
		}

		/// <summary>Initializes a new instance of the <see langword="ExternalException" /> class from serialization data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x06006094 RID: 24724 RVA: 0x0014D992 File Offset: 0x0014BB92
		protected ExternalException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Gets the <see langword="HRESULT" /> of the error.</summary>
		/// <returns>The <see langword="HRESULT" /> of the error.</returns>
		// Token: 0x170010F8 RID: 4344
		// (get) Token: 0x06006095 RID: 24725 RVA: 0x0014D99C File Offset: 0x0014BB9C
		public virtual int ErrorCode
		{
			get
			{
				return base.HResult;
			}
		}

		/// <summary>Returns a string that contains the HRESULT of the error.</summary>
		/// <returns>A string that represents the HRESULT.</returns>
		// Token: 0x06006096 RID: 24726 RVA: 0x0014D9A4 File Offset: 0x0014BBA4
		public override string ToString()
		{
			string message = this.Message;
			string text = base.GetType().ToString();
			string text2 = text + " (0x" + base.HResult.ToString("X8", CultureInfo.InvariantCulture) + ")";
			if (!string.IsNullOrEmpty(message))
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
