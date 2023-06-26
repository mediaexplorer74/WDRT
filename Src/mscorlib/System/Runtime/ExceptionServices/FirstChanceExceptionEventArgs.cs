using System;
using System.Runtime.ConstrainedExecution;

namespace System.Runtime.ExceptionServices
{
	/// <summary>Provides data for the notification event that is raised when a managed exception first occurs, before the common language runtime begins searching for event handlers.</summary>
	// Token: 0x020007A7 RID: 1959
	public class FirstChanceExceptionEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs" /> class with a specified exception.</summary>
		/// <param name="exception">The exception that was just thrown by managed code, and that will be examined by the <see cref="E:System.AppDomain.UnhandledException" /> event.</param>
		// Token: 0x06005519 RID: 21785 RVA: 0x0012F9B3 File Offset: 0x0012DBB3
		public FirstChanceExceptionEventArgs(Exception exception)
		{
			this.m_Exception = exception;
		}

		/// <summary>The managed exception object that corresponds to the exception thrown in managed code.</summary>
		/// <returns>The newly thrown exception.</returns>
		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x0600551A RID: 21786 RVA: 0x0012F9C2 File Offset: 0x0012DBC2
		public Exception Exception
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this.m_Exception;
			}
		}

		// Token: 0x04002727 RID: 10023
		private Exception m_Exception;
	}
}
