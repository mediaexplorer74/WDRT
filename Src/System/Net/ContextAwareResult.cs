using System;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;

namespace System.Net
{
	// Token: 0x020001A9 RID: 425
	internal class ContextAwareResult : LazyAsyncResult
	{
		// Token: 0x060010AF RID: 4271 RVA: 0x00059A12 File Offset: 0x00057C12
		internal ContextAwareResult(object myObject, object myState, AsyncCallback myCallBack)
			: this(false, false, myObject, myState, myCallBack)
		{
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x00059A1F File Offset: 0x00057C1F
		internal ContextAwareResult(bool captureIdentity, bool forceCaptureContext, object myObject, object myState, AsyncCallback myCallBack)
			: this(captureIdentity, forceCaptureContext, false, myObject, myState, myCallBack)
		{
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x00059A2F File Offset: 0x00057C2F
		internal ContextAwareResult(bool captureIdentity, bool forceCaptureContext, bool threadSafeContextCopy, object myObject, object myState, AsyncCallback myCallBack)
			: base(myObject, myState, myCallBack)
		{
			if (forceCaptureContext)
			{
				this._Flags = ContextAwareResult.StateFlags.CaptureContext;
			}
			if (captureIdentity)
			{
				this._Flags |= ContextAwareResult.StateFlags.CaptureIdentity;
			}
			if (threadSafeContextCopy)
			{
				this._Flags |= ContextAwareResult.StateFlags.ThreadSafeContextCopy;
			}
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x00059A69 File Offset: 0x00057C69
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.ControlPrincipal)]
		private void SafeCaptureIdenity()
		{
			this._Wi = WindowsIdentity.GetCurrent();
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x060010B3 RID: 4275 RVA: 0x00059A78 File Offset: 0x00057C78
		internal ExecutionContext ContextCopy
		{
			get
			{
				if (base.InternalPeekCompleted)
				{
					throw new InvalidOperationException(SR.GetString("net_completed_result"));
				}
				ExecutionContext executionContext = this._Context;
				if (executionContext != null)
				{
					return executionContext.CreateCopy();
				}
				if ((this._Flags & ContextAwareResult.StateFlags.PostBlockFinished) == ContextAwareResult.StateFlags.None)
				{
					object @lock = this._Lock;
					lock (@lock)
					{
					}
				}
				if (base.InternalPeekCompleted)
				{
					throw new InvalidOperationException(SR.GetString("net_completed_result"));
				}
				executionContext = this._Context;
				if (executionContext != null)
				{
					return executionContext.CreateCopy();
				}
				return null;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x060010B4 RID: 4276 RVA: 0x00059B14 File Offset: 0x00057D14
		internal WindowsIdentity Identity
		{
			get
			{
				if (base.InternalPeekCompleted)
				{
					throw new InvalidOperationException(SR.GetString("net_completed_result"));
				}
				if (this._Wi != null)
				{
					return this._Wi;
				}
				if ((this._Flags & ContextAwareResult.StateFlags.PostBlockFinished) == ContextAwareResult.StateFlags.None)
				{
					object @lock = this._Lock;
					lock (@lock)
					{
					}
				}
				if (base.InternalPeekCompleted)
				{
					throw new InvalidOperationException(SR.GetString("net_completed_result"));
				}
				return this._Wi;
			}
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x00059BA0 File Offset: 0x00057DA0
		internal object StartPostingAsyncOp()
		{
			return this.StartPostingAsyncOp(true);
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x00059BA9 File Offset: 0x00057DA9
		internal object StartPostingAsyncOp(bool lockCapture)
		{
			this._Lock = (lockCapture ? new object() : null);
			this._Flags |= ContextAwareResult.StateFlags.PostBlockStarted;
			return this._Lock;
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x00059BD0 File Offset: 0x00057DD0
		internal bool FinishPostingAsyncOp()
		{
			if ((this._Flags & (ContextAwareResult.StateFlags.PostBlockStarted | ContextAwareResult.StateFlags.PostBlockFinished)) != ContextAwareResult.StateFlags.PostBlockStarted)
			{
				return false;
			}
			this._Flags |= ContextAwareResult.StateFlags.PostBlockFinished;
			ExecutionContext executionContext = null;
			return this.CaptureOrComplete(ref executionContext, false);
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x00059C08 File Offset: 0x00057E08
		internal bool FinishPostingAsyncOp(ref CallbackClosure closure)
		{
			if ((this._Flags & (ContextAwareResult.StateFlags.PostBlockStarted | ContextAwareResult.StateFlags.PostBlockFinished)) != ContextAwareResult.StateFlags.PostBlockStarted)
			{
				return false;
			}
			this._Flags |= ContextAwareResult.StateFlags.PostBlockFinished;
			CallbackClosure callbackClosure = closure;
			ExecutionContext executionContext;
			if (callbackClosure == null)
			{
				executionContext = null;
			}
			else if (!callbackClosure.IsCompatible(base.AsyncCallback))
			{
				closure = null;
				executionContext = null;
			}
			else
			{
				base.AsyncCallback = callbackClosure.AsyncCallback;
				executionContext = callbackClosure.Context;
			}
			bool flag = this.CaptureOrComplete(ref executionContext, true);
			if (closure == null && base.AsyncCallback != null && executionContext != null)
			{
				closure = new CallbackClosure(executionContext, base.AsyncCallback);
			}
			return flag;
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x00059C8C File Offset: 0x00057E8C
		protected override void Cleanup()
		{
			base.Cleanup();
			if (this._Wi != null)
			{
				this._Wi.Dispose();
				this._Wi = null;
			}
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x00059CB0 File Offset: 0x00057EB0
		private bool CaptureOrComplete(ref ExecutionContext cachedContext, bool returnContext)
		{
			bool flag = base.AsyncCallback != null || (this._Flags & ContextAwareResult.StateFlags.CaptureContext) > ContextAwareResult.StateFlags.None;
			if ((this._Flags & ContextAwareResult.StateFlags.CaptureIdentity) != ContextAwareResult.StateFlags.None && !base.InternalPeekCompleted && (!flag || SecurityContext.IsWindowsIdentityFlowSuppressed()))
			{
				this.SafeCaptureIdenity();
			}
			if (flag && !base.InternalPeekCompleted)
			{
				if (cachedContext == null)
				{
					cachedContext = ExecutionContext.Capture();
				}
				if (cachedContext != null)
				{
					if (!returnContext)
					{
						this._Context = cachedContext;
						cachedContext = null;
					}
					else
					{
						this._Context = cachedContext.CreateCopy();
					}
				}
			}
			else
			{
				cachedContext = null;
			}
			if (base.CompletedSynchronously)
			{
				base.Complete(IntPtr.Zero);
				return true;
			}
			return false;
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x00059D4C File Offset: 0x00057F4C
		protected override void Complete(IntPtr userToken)
		{
			if ((this._Flags & ContextAwareResult.StateFlags.PostBlockStarted) == ContextAwareResult.StateFlags.None)
			{
				base.Complete(userToken);
				return;
			}
			if (base.CompletedSynchronously)
			{
				return;
			}
			ExecutionContext context = this._Context;
			if (userToken != IntPtr.Zero || context == null)
			{
				base.Complete(userToken);
				return;
			}
			ExecutionContext.Run(((this._Flags & ContextAwareResult.StateFlags.ThreadSafeContextCopy) != ContextAwareResult.StateFlags.None) ? context.CreateCopy() : context, new ContextCallback(this.CompleteCallback), null);
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x00059DBA File Offset: 0x00057FBA
		private void CompleteCallback(object state)
		{
			base.Complete(IntPtr.Zero);
		}

		// Token: 0x04001396 RID: 5014
		private volatile ExecutionContext _Context;

		// Token: 0x04001397 RID: 5015
		private object _Lock;

		// Token: 0x04001398 RID: 5016
		private ContextAwareResult.StateFlags _Flags;

		// Token: 0x04001399 RID: 5017
		private WindowsIdentity _Wi;

		// Token: 0x0200074C RID: 1868
		[Flags]
		private enum StateFlags
		{
			// Token: 0x040031D7 RID: 12759
			None = 0,
			// Token: 0x040031D8 RID: 12760
			CaptureIdentity = 1,
			// Token: 0x040031D9 RID: 12761
			CaptureContext = 2,
			// Token: 0x040031DA RID: 12762
			ThreadSafeContextCopy = 4,
			// Token: 0x040031DB RID: 12763
			PostBlockStarted = 8,
			// Token: 0x040031DC RID: 12764
			PostBlockFinished = 16
		}
	}
}
