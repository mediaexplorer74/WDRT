using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;

namespace System.Runtime.Serialization.Formatters
{
	/// <summary>Contains information for a server fault. This class cannot be inherited.</summary>
	// Token: 0x02000765 RID: 1893
	[SoapType(Embedded = true)]
	[ComVisible(true)]
	[Serializable]
	public sealed class ServerFault
	{
		// Token: 0x06005345 RID: 21317 RVA: 0x00125699 File Offset: 0x00123899
		internal ServerFault(Exception exception)
		{
			this.exception = exception;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Formatters.ServerFault" /> class.</summary>
		/// <param name="exceptionType">The type of the exception that occurred on the server.</param>
		/// <param name="message">The message that accompanied the exception.</param>
		/// <param name="stackTrace">The stack trace of the thread that threw the exception on the server.</param>
		// Token: 0x06005346 RID: 21318 RVA: 0x001256A8 File Offset: 0x001238A8
		public ServerFault(string exceptionType, string message, string stackTrace)
		{
			this.exceptionType = exceptionType;
			this.message = message;
			this.stackTrace = stackTrace;
		}

		/// <summary>Gets or sets the type of exception that was thrown by the server.</summary>
		/// <returns>The type of exception that was thrown by the server.</returns>
		// Token: 0x17000DCA RID: 3530
		// (get) Token: 0x06005347 RID: 21319 RVA: 0x001256C5 File Offset: 0x001238C5
		// (set) Token: 0x06005348 RID: 21320 RVA: 0x001256CD File Offset: 0x001238CD
		public string ExceptionType
		{
			get
			{
				return this.exceptionType;
			}
			set
			{
				this.exceptionType = value;
			}
		}

		/// <summary>Gets or sets the exception message that accompanied the exception thrown on the server.</summary>
		/// <returns>The exception message that accompanied the exception thrown on the server.</returns>
		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x06005349 RID: 21321 RVA: 0x001256D6 File Offset: 0x001238D6
		// (set) Token: 0x0600534A RID: 21322 RVA: 0x001256DE File Offset: 0x001238DE
		public string ExceptionMessage
		{
			get
			{
				return this.message;
			}
			set
			{
				this.message = value;
			}
		}

		/// <summary>Gets or sets the stack trace of the thread that threw the exception on the server.</summary>
		/// <returns>The stack trace of the thread that threw the exception on the server.</returns>
		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x0600534B RID: 21323 RVA: 0x001256E7 File Offset: 0x001238E7
		// (set) Token: 0x0600534C RID: 21324 RVA: 0x001256EF File Offset: 0x001238EF
		public string StackTrace
		{
			get
			{
				return this.stackTrace;
			}
			set
			{
				this.stackTrace = value;
			}
		}

		// Token: 0x17000DCD RID: 3533
		// (get) Token: 0x0600534D RID: 21325 RVA: 0x001256F8 File Offset: 0x001238F8
		internal Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x040024E8 RID: 9448
		private string exceptionType;

		// Token: 0x040024E9 RID: 9449
		private string message;

		// Token: 0x040024EA RID: 9450
		private string stackTrace;

		// Token: 0x040024EB RID: 9451
		private Exception exception;
	}
}
