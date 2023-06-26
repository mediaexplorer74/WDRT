using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x02000586 RID: 1414
	internal sealed class BeginEndAwaitableAdapter : ICriticalNotifyCompletion, INotifyCompletion
	{
		// Token: 0x060042B3 RID: 17075 RVA: 0x000F9BA2 File Offset: 0x000F7DA2
		public BeginEndAwaitableAdapter GetAwaiter()
		{
			return this;
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x060042B4 RID: 17076 RVA: 0x000F9BA5 File Offset: 0x000F7DA5
		public bool IsCompleted
		{
			get
			{
				return this._continuation == BeginEndAwaitableAdapter.CALLBACK_RAN;
			}
		}

		// Token: 0x060042B5 RID: 17077 RVA: 0x000F9BB7 File Offset: 0x000F7DB7
		[SecurityCritical]
		public void UnsafeOnCompleted(Action continuation)
		{
			this.OnCompleted(continuation);
		}

		// Token: 0x060042B6 RID: 17078 RVA: 0x000F9BC0 File Offset: 0x000F7DC0
		public void OnCompleted(Action continuation)
		{
			if (this._continuation == BeginEndAwaitableAdapter.CALLBACK_RAN || Interlocked.CompareExchange<Action>(ref this._continuation, continuation, null) == BeginEndAwaitableAdapter.CALLBACK_RAN)
			{
				Task.Run(continuation);
			}
		}

		// Token: 0x060042B7 RID: 17079 RVA: 0x000F9BF4 File Offset: 0x000F7DF4
		public IAsyncResult GetResult()
		{
			IAsyncResult asyncResult = this._asyncResult;
			this._asyncResult = null;
			this._continuation = null;
			return asyncResult;
		}

		// Token: 0x04001BC0 RID: 7104
		private static readonly Action CALLBACK_RAN = delegate
		{
		};

		// Token: 0x04001BC1 RID: 7105
		private IAsyncResult _asyncResult;

		// Token: 0x04001BC2 RID: 7106
		private Action _continuation;

		// Token: 0x04001BC3 RID: 7107
		public static readonly AsyncCallback Callback = delegate(IAsyncResult asyncResult)
		{
			BeginEndAwaitableAdapter beginEndAwaitableAdapter = (BeginEndAwaitableAdapter)asyncResult.AsyncState;
			beginEndAwaitableAdapter._asyncResult = asyncResult;
			Action action = Interlocked.Exchange<Action>(ref beginEndAwaitableAdapter._continuation, BeginEndAwaitableAdapter.CALLBACK_RAN);
			if (action != null)
			{
				action();
			}
		};
	}
}
