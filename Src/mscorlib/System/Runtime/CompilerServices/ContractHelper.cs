using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Runtime.CompilerServices
{
	/// <summary>Provides methods that the binary rewriter uses to handle contract failures.</summary>
	// Token: 0x0200089F RID: 2207
	[__DynamicallyInvokable]
	public static class ContractHelper
	{
		/// <summary>Used by the binary rewriter to activate the default failure behavior.</summary>
		/// <param name="failureKind">One of the enumeration values that specifies the type of failure.</param>
		/// <param name="userMessage">Additional user information.</param>
		/// <param name="conditionText">The description of the condition that caused the failure.</param>
		/// <param name="innerException">The inner exception that caused the current exception.</param>
		/// <returns>A null reference (<see langword="Nothing" /> in Visual Basic) if the event was handled and should not trigger a failure; otherwise, returns the localized failure message.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="failureKind" /> is not a valid <see cref="T:System.Diagnostics.Contracts.ContractFailureKind" /> value.</exception>
		// Token: 0x06005D85 RID: 23941 RVA: 0x0014A758 File Offset: 0x00148958
		[DebuggerNonUserCode]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static string RaiseContractFailedEvent(ContractFailureKind failureKind, string userMessage, string conditionText, Exception innerException)
		{
			string text = "Contract failed";
			ContractHelper.RaiseContractFailedEventImplementation(failureKind, userMessage, conditionText, innerException, ref text);
			return text;
		}

		/// <summary>Triggers the default failure behavior.</summary>
		/// <param name="kind">One of the enumeration values that specifies the type of failure.</param>
		/// <param name="displayMessage">The message to display.</param>
		/// <param name="userMessage">Additional user information.</param>
		/// <param name="conditionText">The description of the condition that caused the failure.</param>
		/// <param name="innerException">The inner exception that caused the current exception.</param>
		// Token: 0x06005D86 RID: 23942 RVA: 0x0014A777 File Offset: 0x00148977
		[DebuggerNonUserCode]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static void TriggerFailure(ContractFailureKind kind, string displayMessage, string userMessage, string conditionText, Exception innerException)
		{
			ContractHelper.TriggerFailureImplementation(kind, displayMessage, userMessage, conditionText, innerException);
		}

		// Token: 0x06005D87 RID: 23943 RVA: 0x0014A784 File Offset: 0x00148984
		[DebuggerNonUserCode]
		[SecuritySafeCritical]
		private static void RaiseContractFailedEventImplementation(ContractFailureKind failureKind, string userMessage, string conditionText, Exception innerException, ref string resultFailureMessage)
		{
			if (failureKind < ContractFailureKind.Precondition || failureKind > ContractFailureKind.Assume)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[] { failureKind }), "failureKind");
			}
			string text = "contract failed.";
			ContractFailedEventArgs contractFailedEventArgs = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			string text2;
			try
			{
				text = ContractHelper.GetDisplayMessage(failureKind, userMessage, conditionText);
				EventHandler<ContractFailedEventArgs> eventHandler = ContractHelper.contractFailedEvent;
				if (eventHandler != null)
				{
					contractFailedEventArgs = new ContractFailedEventArgs(failureKind, text, conditionText, innerException);
					foreach (EventHandler<ContractFailedEventArgs> eventHandler2 in eventHandler.GetInvocationList())
					{
						try
						{
							eventHandler2(null, contractFailedEventArgs);
						}
						catch (Exception ex)
						{
							contractFailedEventArgs.thrownDuringHandler = ex;
							contractFailedEventArgs.SetUnwind();
						}
					}
					if (contractFailedEventArgs.Unwind)
					{
						if (Environment.IsCLRHosted)
						{
							ContractHelper.TriggerCodeContractEscalationPolicy(failureKind, text, conditionText, innerException);
						}
						if (innerException == null)
						{
							innerException = contractFailedEventArgs.thrownDuringHandler;
						}
						throw new ContractException(failureKind, text, userMessage, conditionText, innerException);
					}
				}
			}
			finally
			{
				if (contractFailedEventArgs != null && contractFailedEventArgs.Handled)
				{
					text2 = null;
				}
				else
				{
					text2 = text;
				}
			}
			resultFailureMessage = text2;
		}

		// Token: 0x06005D88 RID: 23944 RVA: 0x0014A890 File Offset: 0x00148A90
		[DebuggerNonUserCode]
		[SecuritySafeCritical]
		private static void TriggerFailureImplementation(ContractFailureKind kind, string displayMessage, string userMessage, string conditionText, Exception innerException)
		{
			if (Environment.IsCLRHosted)
			{
				ContractHelper.TriggerCodeContractEscalationPolicy(kind, displayMessage, conditionText, innerException);
				throw new ContractException(kind, displayMessage, userMessage, conditionText, innerException);
			}
			if (!Environment.UserInteractive)
			{
				throw new ContractException(kind, displayMessage, userMessage, conditionText, innerException);
			}
			string resourceString = Environment.GetResourceString(ContractHelper.GetResourceNameForFailure(kind));
			Assert.Fail(conditionText, displayMessage, resourceString, -2146233022, StackTrace.TraceFormat.Normal, 2);
		}

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06005D89 RID: 23945 RVA: 0x0014A8E8 File Offset: 0x00148AE8
		// (remove) Token: 0x06005D8A RID: 23946 RVA: 0x0014A940 File Offset: 0x00148B40
		internal static event EventHandler<ContractFailedEventArgs> InternalContractFailed
		{
			[SecurityCritical]
			add
			{
				RuntimeHelpers.PrepareContractedDelegate(value);
				object obj = ContractHelper.lockObject;
				lock (obj)
				{
					ContractHelper.contractFailedEvent = (EventHandler<ContractFailedEventArgs>)Delegate.Combine(ContractHelper.contractFailedEvent, value);
				}
			}
			[SecurityCritical]
			remove
			{
				object obj = ContractHelper.lockObject;
				lock (obj)
				{
					ContractHelper.contractFailedEvent = (EventHandler<ContractFailedEventArgs>)Delegate.Remove(ContractHelper.contractFailedEvent, value);
				}
			}
		}

		// Token: 0x06005D8B RID: 23947 RVA: 0x0014A994 File Offset: 0x00148B94
		private static string GetResourceNameForFailure(ContractFailureKind failureKind)
		{
			string text;
			switch (failureKind)
			{
			case ContractFailureKind.Precondition:
				text = "PreconditionFailed";
				break;
			case ContractFailureKind.Postcondition:
				text = "PostconditionFailed";
				break;
			case ContractFailureKind.PostconditionOnException:
				text = "PostconditionOnExceptionFailed";
				break;
			case ContractFailureKind.Invariant:
				text = "InvariantFailed";
				break;
			case ContractFailureKind.Assert:
				text = "AssertionFailed";
				break;
			case ContractFailureKind.Assume:
				text = "AssumptionFailed";
				break;
			default:
				Contract.Assume(false, "Unreachable code");
				text = "AssumptionFailed";
				break;
			}
			return text;
		}

		// Token: 0x06005D8C RID: 23948 RVA: 0x0014AA08 File Offset: 0x00148C08
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		private static string GetDisplayMessage(ContractFailureKind failureKind, string userMessage, string conditionText)
		{
			string text = ContractHelper.GetResourceNameForFailure(failureKind);
			string text2;
			if (!string.IsNullOrEmpty(conditionText))
			{
				text += "_Cnd";
				text2 = Environment.GetResourceString(text, new object[] { conditionText });
			}
			else
			{
				text2 = Environment.GetResourceString(text);
			}
			if (!string.IsNullOrEmpty(userMessage))
			{
				return text2 + "  " + userMessage;
			}
			return text2;
		}

		// Token: 0x06005D8D RID: 23949 RVA: 0x0014AA60 File Offset: 0x00148C60
		[SecuritySafeCritical]
		[DebuggerNonUserCode]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private static void TriggerCodeContractEscalationPolicy(ContractFailureKind failureKind, string message, string conditionText, Exception innerException)
		{
			string text = null;
			if (innerException != null)
			{
				text = innerException.ToString();
			}
			Environment.TriggerCodeContractFailure(failureKind, message, conditionText, text);
		}

		// Token: 0x04002A14 RID: 10772
		private static volatile EventHandler<ContractFailedEventArgs> contractFailedEvent;

		// Token: 0x04002A15 RID: 10773
		private static readonly object lockObject = new object();

		// Token: 0x04002A16 RID: 10774
		internal const int COR_E_CODECONTRACTFAILED = -2146233022;
	}
}
