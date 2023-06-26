using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x020001A8 RID: 424
	internal class CallbackClosure
	{
		// Token: 0x060010AB RID: 4267 RVA: 0x000599C9 File Offset: 0x00057BC9
		internal CallbackClosure(ExecutionContext context, AsyncCallback callback)
		{
			if (callback != null)
			{
				this.savedCallback = callback;
				this.savedContext = context;
			}
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x000599E2 File Offset: 0x00057BE2
		internal bool IsCompatible(AsyncCallback callback)
		{
			return callback != null && this.savedCallback != null && object.Equals(this.savedCallback, callback);
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x060010AD RID: 4269 RVA: 0x00059A02 File Offset: 0x00057C02
		internal AsyncCallback AsyncCallback
		{
			get
			{
				return this.savedCallback;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x060010AE RID: 4270 RVA: 0x00059A0A File Offset: 0x00057C0A
		internal ExecutionContext Context
		{
			get
			{
				return this.savedContext;
			}
		}

		// Token: 0x04001394 RID: 5012
		private AsyncCallback savedCallback;

		// Token: 0x04001395 RID: 5013
		private ExecutionContext savedContext;
	}
}
