using System;

namespace System.Threading.Tasks
{
	/// <summary>Provides data for the event that is raised when a faulted <see cref="T:System.Threading.Tasks.Task" />'s exception goes unobserved.</summary>
	// Token: 0x02000577 RID: 1399
	[__DynamicallyInvokable]
	public class UnobservedTaskExceptionEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.UnobservedTaskExceptionEventArgs" /> class with the unobserved exception.</summary>
		/// <param name="exception">The Exception that has gone unobserved.</param>
		// Token: 0x06004237 RID: 16951 RVA: 0x000F7CD3 File Offset: 0x000F5ED3
		[__DynamicallyInvokable]
		public UnobservedTaskExceptionEventArgs(AggregateException exception)
		{
			this.m_exception = exception;
		}

		/// <summary>Marks the <see cref="P:System.Threading.Tasks.UnobservedTaskExceptionEventArgs.Exception" /> as "observed," thus preventing it from triggering exception escalation policy which, by default, terminates the process.</summary>
		// Token: 0x06004238 RID: 16952 RVA: 0x000F7CE2 File Offset: 0x000F5EE2
		[__DynamicallyInvokable]
		public void SetObserved()
		{
			this.m_observed = true;
		}

		/// <summary>Gets whether this exception has been marked as "observed."</summary>
		/// <returns>true if this exception has been marked as "observed"; otherwise false.</returns>
		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06004239 RID: 16953 RVA: 0x000F7CEB File Offset: 0x000F5EEB
		[__DynamicallyInvokable]
		public bool Observed
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_observed;
			}
		}

		/// <summary>The Exception that went unobserved.</summary>
		/// <returns>The Exception that went unobserved.</returns>
		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x0600423A RID: 16954 RVA: 0x000F7CF3 File Offset: 0x000F5EF3
		[__DynamicallyInvokable]
		public AggregateException Exception
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_exception;
			}
		}

		// Token: 0x04001B7D RID: 7037
		private AggregateException m_exception;

		// Token: 0x04001B7E RID: 7038
		internal bool m_observed;
	}
}
