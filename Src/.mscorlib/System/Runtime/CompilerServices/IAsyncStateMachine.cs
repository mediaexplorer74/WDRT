using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Represents state machines that are generated for asynchronous methods. This type is intended for compiler use only.</summary>
	// Token: 0x020008F1 RID: 2289
	[__DynamicallyInvokable]
	public interface IAsyncStateMachine
	{
		/// <summary>Moves the state machine to its next state.</summary>
		// Token: 0x06005E4F RID: 24143
		[__DynamicallyInvokable]
		void MoveNext();

		/// <summary>Configures the state machine with a heap-allocated replica.</summary>
		/// <param name="stateMachine">The heap-allocated replica.</param>
		// Token: 0x06005E50 RID: 24144
		[__DynamicallyInvokable]
		void SetStateMachine(IAsyncStateMachine stateMachine);
	}
}
