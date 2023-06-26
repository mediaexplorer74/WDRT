using System;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Diagnostics.Contracts
{
	/// <summary>Provides methods and data for the <see cref="E:System.Diagnostics.Contracts.Contract.ContractFailed" /> event.</summary>
	// Token: 0x02000415 RID: 1045
	[__DynamicallyInvokable]
	public sealed class ContractFailedEventArgs : EventArgs
	{
		/// <summary>Provides data for the <see cref="E:System.Diagnostics.Contracts.Contract.ContractFailed" /> event.</summary>
		/// <param name="failureKind">One of the enumeration values that specifies the contract that failed.</param>
		/// <param name="message">The message for the event.</param>
		/// <param name="condition">The condition for the event.</param>
		/// <param name="originalException">The exception that caused the event.</param>
		// Token: 0x06003433 RID: 13363 RVA: 0x000C8354 File Offset: 0x000C6554
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public ContractFailedEventArgs(ContractFailureKind failureKind, string message, string condition, Exception originalException)
		{
			this._failureKind = failureKind;
			this._message = message;
			this._condition = condition;
			this._originalException = originalException;
		}

		/// <summary>Gets the message that describes the <see cref="E:System.Diagnostics.Contracts.Contract.ContractFailed" /> event.</summary>
		/// <returns>The message that describes the event.</returns>
		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06003434 RID: 13364 RVA: 0x000C8379 File Offset: 0x000C6579
		[__DynamicallyInvokable]
		public string Message
		{
			[__DynamicallyInvokable]
			get
			{
				return this._message;
			}
		}

		/// <summary>Gets the condition for the failure of the contract.</summary>
		/// <returns>The condition for the failure.</returns>
		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06003435 RID: 13365 RVA: 0x000C8381 File Offset: 0x000C6581
		[__DynamicallyInvokable]
		public string Condition
		{
			[__DynamicallyInvokable]
			get
			{
				return this._condition;
			}
		}

		/// <summary>Gets the type of contract that failed.</summary>
		/// <returns>One of the enumeration values that specifies the type of contract that failed.</returns>
		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06003436 RID: 13366 RVA: 0x000C8389 File Offset: 0x000C6589
		[__DynamicallyInvokable]
		public ContractFailureKind FailureKind
		{
			[__DynamicallyInvokable]
			get
			{
				return this._failureKind;
			}
		}

		/// <summary>Gets the original exception that caused the <see cref="E:System.Diagnostics.Contracts.Contract.ContractFailed" /> event.</summary>
		/// <returns>The exception that caused the event.</returns>
		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06003437 RID: 13367 RVA: 0x000C8391 File Offset: 0x000C6591
		[__DynamicallyInvokable]
		public Exception OriginalException
		{
			[__DynamicallyInvokable]
			get
			{
				return this._originalException;
			}
		}

		/// <summary>Indicates whether the <see cref="E:System.Diagnostics.Contracts.Contract.ContractFailed" /> event has been handled.</summary>
		/// <returns>
		///   <see langword="true" /> if the event has been handled; otherwise, <see langword="false" />.</returns>
		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06003438 RID: 13368 RVA: 0x000C8399 File Offset: 0x000C6599
		[__DynamicallyInvokable]
		public bool Handled
		{
			[__DynamicallyInvokable]
			get
			{
				return this._handled;
			}
		}

		/// <summary>Sets the <see cref="P:System.Diagnostics.Contracts.ContractFailedEventArgs.Handled" /> property to <see langword="true" />.</summary>
		// Token: 0x06003439 RID: 13369 RVA: 0x000C83A1 File Offset: 0x000C65A1
		[SecurityCritical]
		[__DynamicallyInvokable]
		public void SetHandled()
		{
			this._handled = true;
		}

		/// <summary>Indicates whether the code contract escalation policy should be applied.</summary>
		/// <returns>
		///   <see langword="true" /> to apply the escalation policy; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x0600343A RID: 13370 RVA: 0x000C83AA File Offset: 0x000C65AA
		[__DynamicallyInvokable]
		public bool Unwind
		{
			[__DynamicallyInvokable]
			get
			{
				return this._unwind;
			}
		}

		/// <summary>Sets the <see cref="P:System.Diagnostics.Contracts.ContractFailedEventArgs.Unwind" /> property to <see langword="true" />.</summary>
		// Token: 0x0600343B RID: 13371 RVA: 0x000C83B2 File Offset: 0x000C65B2
		[SecurityCritical]
		[__DynamicallyInvokable]
		public void SetUnwind()
		{
			this._unwind = true;
		}

		// Token: 0x0400171F RID: 5919
		private ContractFailureKind _failureKind;

		// Token: 0x04001720 RID: 5920
		private string _message;

		// Token: 0x04001721 RID: 5921
		private string _condition;

		// Token: 0x04001722 RID: 5922
		private Exception _originalException;

		// Token: 0x04001723 RID: 5923
		private bool _handled;

		// Token: 0x04001724 RID: 5924
		private bool _unwind;

		// Token: 0x04001725 RID: 5925
		internal Exception thrownDuringHandler;
	}
}
