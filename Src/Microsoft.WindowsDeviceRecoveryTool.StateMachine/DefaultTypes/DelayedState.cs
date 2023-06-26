using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.StateMachine.BaseTypes;

namespace Microsoft.WindowsDeviceRecoveryTool.StateMachine.DefaultTypes
{
	// Token: 0x02000008 RID: 8
	public class DelayedState : BaseState
	{
		// Token: 0x06000010 RID: 16 RVA: 0x0000211F File Offset: 0x0000031F
		public DelayedState(int minimumStateDuration)
		{
			this.minimumStateDuration = ((minimumStateDuration >= 0) ? minimumStateDuration : 0);
			this.stopwatch = new Stopwatch();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002142 File Offset: 0x00000342
		public override void Start()
		{
			base.Start();
			this.error = null;
			this.stopwatch.Restart();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002160 File Offset: 0x00000360
		protected override void RaiseStateFinished(TransitionEventArgs eventArgs)
		{
			this.stopwatch.Stop();
			this.transitionEventArgs = eventArgs;
			bool flag = this.stopwatch.ElapsedMilliseconds < (long)this.minimumStateDuration;
			if (flag)
			{
				this.ExtendStateVisibility();
			}
			else
			{
				base.RaiseStateFinished(this.transitionEventArgs);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000021B4 File Offset: 0x000003B4
		protected override void RaiseStateErrored(Error e)
		{
			this.stopwatch.Stop();
			this.error = e;
			bool flag = this.stopwatch.ElapsedMilliseconds < (long)this.minimumStateDuration;
			if (flag)
			{
				this.ExtendStateVisibility();
			}
			else
			{
				base.RaiseStateErrored(this.error);
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002208 File Offset: 0x00000408
		private void ExtendStateVisibility()
		{
			BackgroundWorker backgroundWorker = new BackgroundWorker();
			backgroundWorker.DoWork += this.ExtendStateVisibilityDoWork;
			backgroundWorker.RunWorkerCompleted += this.ExtendStateVisibilityCompleted;
			backgroundWorker.RunWorkerAsync();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000224C File Offset: 0x0000044C
		private void ExtendStateVisibilityDoWork(object sender, DoWorkEventArgs e)
		{
			int num = this.minimumStateDuration - (int)this.stopwatch.ElapsedMilliseconds;
			Thread.Sleep(num);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002278 File Offset: 0x00000478
		private void ExtendStateVisibilityCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			bool flag = e.Error != null;
			if (flag)
			{
				Tracer<DelayedState>.WriteError("Exception while waiting for delayed state to end!", new object[0]);
				Tracer<DelayedState>.WriteError(e.Error, e.Error.Message, new object[0]);
			}
			bool flag2 = this.error != null;
			if (flag2)
			{
				base.RaiseStateErrored(this.error);
			}
			else
			{
				base.RaiseStateFinished(this.transitionEventArgs);
			}
		}

		// Token: 0x04000003 RID: 3
		private readonly int minimumStateDuration;

		// Token: 0x04000004 RID: 4
		private readonly Stopwatch stopwatch;

		// Token: 0x04000005 RID: 5
		private Error error;

		// Token: 0x04000006 RID: 6
		private TransitionEventArgs transitionEventArgs;
	}
}
