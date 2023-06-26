using System;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.StateMachine.BaseTypes;

namespace Microsoft.WindowsDeviceRecoveryTool.StateMachine.DefaultTypes
{
	// Token: 0x0200000D RID: 13
	public abstract class StateMachineState : BaseState
	{
		// Token: 0x06000025 RID: 37 RVA: 0x00002389 File Offset: 0x00000589
		protected StateMachineState()
		{
			this.Machine = new BaseStateMachine
			{
				MachineName = this.ToString()
			};
			this.Machine.CurrentStateChanged += this.OnCurrentStateChanged;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000023C5 File Offset: 0x000005C5
		// (set) Token: 0x06000027 RID: 39 RVA: 0x000023CD File Offset: 0x000005CD
		public new bool Started { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000023D8 File Offset: 0x000005D8
		public BaseState CurrentState
		{
			get
			{
				return (this.Machine != null) ? this.Machine.CurrentState : null;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002400 File Offset: 0x00000600
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002408 File Offset: 0x00000608
		protected BaseStateMachine Machine { get; set; }

		// Token: 0x0600002B RID: 43 RVA: 0x00002414 File Offset: 0x00000614
		public sealed override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000242C File Offset: 0x0000062C
		public override void Start()
		{
			bool flag = !this.Started;
			if (flag)
			{
				Tracer<BaseState>.WriteInformation("Started state: {0} ({1})", new object[]
				{
					this.ToString(),
					base.MachineName
				});
				this.Started = true;
				this.Machine.MachineStarted += this.MachineStarted;
				this.Machine.MachineStopped += this.MachineStopped;
				this.Machine.MachineEnded += this.MachineEnded;
				this.Machine.MachineErrored += this.MachineErrored;
				this.Machine.Start();
			}
			else
			{
				Tracer<BaseState>.WriteWarning("Trying to start state {0} which is already started!", new object[] { this.ToString() });
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002504 File Offset: 0x00000704
		public override void Stop()
		{
			bool started = this.Started;
			if (started)
			{
				Tracer<BaseState>.WriteInformation("Stopped state: {0} ({1})", new object[]
				{
					this.ToString(),
					base.MachineName
				});
				this.Started = false;
				this.Machine.Stop();
				this.Machine.MachineStarted -= this.MachineStarted;
				this.Machine.MachineStopped -= this.MachineStopped;
				this.Machine.MachineEnded -= this.MachineEnded;
				this.Machine.MachineErrored -= this.MachineErrored;
			}
			else
			{
				Tracer<BaseState>.WriteWarning("Trying to stop state {0} which is already stopped!", new object[] { this.ToString() });
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000025D8 File Offset: 0x000007D8
		public void ClearStateMachine()
		{
			this.Machine = new BaseStateMachine();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002329 File Offset: 0x00000529
		protected virtual void OnCurrentStateChanged(BaseState oldValue, BaseState newValue)
		{
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000025E7 File Offset: 0x000007E7
		protected virtual void MachineEnded(object sender, TransitionEventArgs args)
		{
			Tracer<StateMachineState>.WriteInformation("Machine Ended {0} ({1})", new object[]
			{
				this.ToString(),
				base.MachineName
			});
			this.RaiseStateFinished(args);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002615 File Offset: 0x00000815
		protected virtual void MachineErrored(object sender, BaseStateMachineErrorEventArgs eventArgs)
		{
			Tracer<StateMachineState>.WriteInformation("Machine Errored {0} ({1})", new object[]
			{
				this.ToString(),
				base.MachineName
			});
			this.RaiseStateErrored(eventArgs.Error);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002648 File Offset: 0x00000848
		protected virtual void MachineStopped(object sender, EventArgs args)
		{
			Tracer<StateMachineState>.WriteInformation("Machine Stopped {0} ({1})", new object[]
			{
				this.ToString(),
				base.MachineName
			});
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000266E File Offset: 0x0000086E
		protected virtual void MachineStarted(object sender, EventArgs args)
		{
			Tracer<StateMachineState>.WriteInformation("Machine Started {0} ({1})", new object[]
			{
				this.ToString(),
				base.MachineName
			});
			this.RaiseStateStarted(args);
		}
	}
}
