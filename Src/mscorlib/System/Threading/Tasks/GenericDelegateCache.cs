using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200054D RID: 1357
	internal static class GenericDelegateCache<TAntecedentResult, TResult>
	{
		// Token: 0x04001ACE RID: 6862
		internal static Func<Task<Task>, object, TResult> CWAnyFuncDelegate = delegate(Task<Task> wrappedWinner, object state)
		{
			Func<Task<TAntecedentResult>, TResult> func = (Func<Task<TAntecedentResult>, TResult>)state;
			Task<TAntecedentResult> task = (Task<TAntecedentResult>)wrappedWinner.Result;
			return func(task);
		};

		// Token: 0x04001ACF RID: 6863
		internal static Func<Task<Task>, object, TResult> CWAnyActionDelegate = delegate(Task<Task> wrappedWinner, object state)
		{
			Action<Task<TAntecedentResult>> action = (Action<Task<TAntecedentResult>>)state;
			Task<TAntecedentResult> task2 = (Task<TAntecedentResult>)wrappedWinner.Result;
			action(task2);
			return default(TResult);
		};

		// Token: 0x04001AD0 RID: 6864
		internal static Func<Task<Task<TAntecedentResult>[]>, object, TResult> CWAllFuncDelegate = delegate(Task<Task<TAntecedentResult>[]> wrappedAntecedents, object state)
		{
			wrappedAntecedents.NotifyDebuggerOfWaitCompletionIfNecessary();
			Func<Task<TAntecedentResult>[], TResult> func2 = (Func<Task<TAntecedentResult>[], TResult>)state;
			return func2(wrappedAntecedents.Result);
		};

		// Token: 0x04001AD1 RID: 6865
		internal static Func<Task<Task<TAntecedentResult>[]>, object, TResult> CWAllActionDelegate = delegate(Task<Task<TAntecedentResult>[]> wrappedAntecedents, object state)
		{
			wrappedAntecedents.NotifyDebuggerOfWaitCompletionIfNecessary();
			Action<Task<TAntecedentResult>[]> action2 = (Action<Task<TAntecedentResult>[]>)state;
			action2(wrappedAntecedents.Result);
			return default(TResult);
		};
	}
}
