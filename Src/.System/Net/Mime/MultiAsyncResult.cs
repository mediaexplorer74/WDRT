using System;
using System.Threading;

namespace System.Net.Mime
{
	// Token: 0x0200024E RID: 590
	internal class MultiAsyncResult : LazyAsyncResult
	{
		// Token: 0x06001657 RID: 5719 RVA: 0x00073CF4 File Offset: 0x00071EF4
		internal MultiAsyncResult(object context, AsyncCallback callback, object state)
			: base(context, state, callback)
		{
			this.context = context;
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001658 RID: 5720 RVA: 0x00073D06 File Offset: 0x00071F06
		internal object Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x00073D0E File Offset: 0x00071F0E
		internal void Enter()
		{
			this.Increment();
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x00073D16 File Offset: 0x00071F16
		internal void Leave()
		{
			this.Decrement();
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x00073D1E File Offset: 0x00071F1E
		internal void Leave(object result)
		{
			base.Result = result;
			this.Decrement();
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x00073D2D File Offset: 0x00071F2D
		private void Decrement()
		{
			if (Interlocked.Decrement(ref this.outstanding) == -1)
			{
				base.InvokeCallback(base.Result);
			}
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x00073D49 File Offset: 0x00071F49
		private void Increment()
		{
			Interlocked.Increment(ref this.outstanding);
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x00073D57 File Offset: 0x00071F57
		internal void CompleteSequence()
		{
			this.Decrement();
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x00073D60 File Offset: 0x00071F60
		internal static object End(IAsyncResult result)
		{
			MultiAsyncResult multiAsyncResult = (MultiAsyncResult)result;
			multiAsyncResult.InternalWaitForCompletion();
			return multiAsyncResult.Result;
		}

		// Token: 0x04001728 RID: 5928
		private int outstanding;

		// Token: 0x04001729 RID: 5929
		private object context;
	}
}
