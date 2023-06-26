using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;

namespace System.Diagnostics
{
	/// <summary>Correlates traces that are part of a logical transaction.</summary>
	// Token: 0x02000494 RID: 1172
	public class CorrelationManager
	{
		// Token: 0x06002B54 RID: 11092 RVA: 0x000C4CD2 File Offset: 0x000C2ED2
		internal CorrelationManager()
		{
		}

		/// <summary>Gets or sets the identity for a global activity.</summary>
		/// <returns>A <see cref="T:System.Guid" /> structure that identifies the global activity.</returns>
		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06002B55 RID: 11093 RVA: 0x000C4CDC File Offset: 0x000C2EDC
		// (set) Token: 0x06002B56 RID: 11094 RVA: 0x000C4D03 File Offset: 0x000C2F03
		public Guid ActivityId
		{
			get
			{
				object obj = CallContext.LogicalGetData("E2ETrace.ActivityID");
				if (obj != null)
				{
					return (Guid)obj;
				}
				return Guid.Empty;
			}
			set
			{
				CallContext.LogicalSetData("E2ETrace.ActivityID", value);
			}
		}

		/// <summary>Gets the logical operation stack from the call context.</summary>
		/// <returns>A <see cref="T:System.Collections.Stack" /> object that represents the logical operation stack for the call context.</returns>
		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06002B57 RID: 11095 RVA: 0x000C4D15 File Offset: 0x000C2F15
		public Stack LogicalOperationStack
		{
			get
			{
				return this.GetLogicalOperationStack();
			}
		}

		/// <summary>Starts a logical operation with the specified identity on a thread.</summary>
		/// <param name="operationId">An object identifying the operation.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="operationId" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002B58 RID: 11096 RVA: 0x000C4D20 File Offset: 0x000C2F20
		public void StartLogicalOperation(object operationId)
		{
			if (operationId == null)
			{
				throw new ArgumentNullException("operationId");
			}
			Stack logicalOperationStack = this.GetLogicalOperationStack();
			logicalOperationStack.Push(operationId);
		}

		/// <summary>Starts a logical operation on a thread.</summary>
		// Token: 0x06002B59 RID: 11097 RVA: 0x000C4D49 File Offset: 0x000C2F49
		public void StartLogicalOperation()
		{
			this.StartLogicalOperation(Guid.NewGuid());
		}

		/// <summary>Stops the current logical operation.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.CorrelationManager.LogicalOperationStack" /> property is an empty stack.</exception>
		// Token: 0x06002B5A RID: 11098 RVA: 0x000C4D5C File Offset: 0x000C2F5C
		public void StopLogicalOperation()
		{
			Stack logicalOperationStack = this.GetLogicalOperationStack();
			logicalOperationStack.Pop();
		}

		// Token: 0x06002B5B RID: 11099 RVA: 0x000C4D78 File Offset: 0x000C2F78
		private Stack GetLogicalOperationStack()
		{
			Stack stack = CallContext.LogicalGetData("System.Diagnostics.Trace.CorrelationManagerSlot") as Stack;
			if (stack == null)
			{
				stack = new Stack();
				CallContext.LogicalSetData("System.Diagnostics.Trace.CorrelationManagerSlot", stack);
			}
			return stack;
		}

		// Token: 0x0400266F RID: 9839
		private const string transactionSlotName = "System.Diagnostics.Trace.CorrelationManagerSlot";

		// Token: 0x04002670 RID: 9840
		private const string activityIdSlotName = "E2ETrace.ActivityID";
	}
}
