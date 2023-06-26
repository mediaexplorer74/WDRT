using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.StateMachine.DefaultTypes;
using Microsoft.WindowsDeviceRecoveryTool.StateMachine.Exceptions;

namespace Microsoft.WindowsDeviceRecoveryTool.StateMachine.BaseTypes
{
	// Token: 0x02000011 RID: 17
	public class BaseStateMachine
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00002E08 File Offset: 0x00001008
		public BaseStateMachine()
		{
			this.CurrentState = BaseState.NullObject();
			this.states = new List<BaseState>();
			this.machineState = BaseStateMachine.StateMachineState.Stopped;
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600005D RID: 93 RVA: 0x00002E3C File Offset: 0x0000103C
		// (remove) Token: 0x0600005E RID: 94 RVA: 0x00002E74 File Offset: 0x00001074
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler MachineStarted;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600005F RID: 95 RVA: 0x00002EAC File Offset: 0x000010AC
		// (remove) Token: 0x06000060 RID: 96 RVA: 0x00002EE4 File Offset: 0x000010E4
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<TransitionEventArgs> MachineEnded;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000061 RID: 97 RVA: 0x00002F1C File Offset: 0x0000111C
		// (remove) Token: 0x06000062 RID: 98 RVA: 0x00002F54 File Offset: 0x00001154
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler MachineStopped;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000063 RID: 99 RVA: 0x00002F8C File Offset: 0x0000118C
		// (remove) Token: 0x06000064 RID: 100 RVA: 0x00002FC4 File Offset: 0x000011C4
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<BaseStateMachineErrorEventArgs> MachineErrored;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000065 RID: 101 RVA: 0x00002FFC File Offset: 0x000011FC
		// (remove) Token: 0x06000066 RID: 102 RVA: 0x00003034 File Offset: 0x00001234
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<BaseState, BaseState> CurrentStateChanged;

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000067 RID: 103 RVA: 0x0000306C File Offset: 0x0000126C
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00003084 File Offset: 0x00001284
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
			set
			{
				this.machineName = value.Substring(value.LastIndexOf('.') + 1);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000030A0 File Offset: 0x000012A0
		// (set) Token: 0x0600006A RID: 106 RVA: 0x000030B8 File Offset: 0x000012B8
		public BaseState CurrentState
		{
			get
			{
				return this.currentState;
			}
			private set
			{
				BaseState baseState = this.currentState;
				this.currentState = value;
				this.RaiseCurrentStateChanged(baseState, this.currentState);
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000030E4 File Offset: 0x000012E4
		public void AddState(BaseState state)
		{
			bool flag = !this.CheckIsRunning();
			if (flag)
			{
				state.MachineName = this.MachineName;
				this.states.Add(state);
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000311B File Offset: 0x0000131B
		public void SetStartState(BaseState state)
		{
			this.states.Remove(state);
			this.states.Insert(0, state);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000313C File Offset: 0x0000133C
		public virtual void Start()
		{
			object obj = this.sync;
			lock (obj)
			{
				bool flag2 = !this.CheckIsRunning();
				if (flag2)
				{
					this.RaiseStateMachineEvent(this.MachineStarted);
					this.StartStateMachine();
				}
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000031A0 File Offset: 0x000013A0
		public virtual void Stop()
		{
			object obj = this.sync;
			lock (obj)
			{
				bool flag2 = this.CheckIsRunning();
				if (flag2)
				{
					this.StopStateMachine();
					this.RaiseStateMachineEvent(this.MachineStopped);
				}
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003200 File Offset: 0x00001400
		public virtual void NextState()
		{
			this.NextState(TransitionEventArgs.Empty);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003210 File Offset: 0x00001410
		public bool CheckIsRunning()
		{
			return this.machineState == BaseStateMachine.StateMachineState.Running;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000322C File Offset: 0x0000142C
		private void RaiseCurrentStateChanged(BaseState previousState, BaseState newState)
		{
			Action<BaseState, BaseState> currentStateChanged = this.CurrentStateChanged;
			bool flag = currentStateChanged != null;
			if (flag)
			{
				currentStateChanged(previousState, newState);
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003254 File Offset: 0x00001454
		private void NextState(TransitionEventArgs eventArgs)
		{
			object obj = this.sync;
			lock (obj)
			{
				bool flag2 = this.CheckIsRunning();
				if (flag2)
				{
					this.SetNextState(eventArgs);
				}
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000032A8 File Offset: 0x000014A8
		private void ErroredState(Error error)
		{
			object obj = this.sync;
			lock (obj)
			{
				bool flag2 = this.CheckIsRunning();
				if (flag2)
				{
					this.SetErrorState(error);
				}
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000032FC File Offset: 0x000014FC
		private void StartStateMachine()
		{
			bool flag = this.states.Count == 0;
			if (flag)
			{
				UnexpectedErrorException ex = new UnexpectedErrorException("No states to run state machine!");
				Tracer<BaseStateMachine>.WriteError(ex);
				throw ex;
			}
			this.machineState = BaseStateMachine.StateMachineState.Running;
			this.CurrentState = this.states[0];
			this.CurrentState.Finished += this.CurrentStateFinished;
			this.CurrentState.Errored += this.CurrentStateErrored;
			this.CurrentState.Start();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003388 File Offset: 0x00001588
		private string StopStateMachine()
		{
			this.machineState = BaseStateMachine.StateMachineState.Stopped;
			string text = string.Empty;
			EndState endState = this.CurrentState as EndState;
			bool flag = endState != null;
			if (flag)
			{
				text = endState.Status;
			}
			this.CurrentState.Finished -= this.CurrentStateFinished;
			this.CurrentState.Errored -= this.CurrentStateErrored;
			this.CurrentState.Stop();
			this.CurrentState = BaseState.NullObject();
			return text;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003410 File Offset: 0x00001610
		private void SetNextState(TransitionEventArgs eventArgs)
		{
			bool flag = !this.CheckIsRunning();
			if (!flag)
			{
				BaseState baseState = this.CurrentState.NextState(eventArgs);
				this.CurrentState.Stop();
				this.CurrentState.Finished -= this.CurrentStateFinished;
				this.CurrentState.Errored -= this.CurrentStateErrored;
				this.CurrentState = baseState;
				this.CurrentState.Finished += this.CurrentStateFinished;
				this.CurrentState.Errored += this.CurrentStateErrored;
				try
				{
					this.CurrentState.Start();
				}
				catch (OutOfMemoryException ex)
				{
					Tracer<BaseStateMachine>.WriteError(ex, "Cannot start a state", new object[0]);
					this.CurrentStateErrored(this.CurrentState, new Error(ex));
				}
				catch (Exception ex2)
				{
					Tracer<BaseStateMachine>.WriteError(ex2, "Cannot start a state", new object[0]);
					this.CurrentStateErrored(this.CurrentState, new Error(new InternalException(string.Empty, ex2)));
				}
				bool flag2 = this.IsCurrentStateEndState();
				if (flag2)
				{
					string text = this.StopStateMachine();
					EventHandler<TransitionEventArgs> machineEnded = this.MachineEnded;
					bool flag3 = machineEnded != null;
					if (flag3)
					{
						machineEnded(this, new TransitionEventArgs(text));
					}
				}
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003578 File Offset: 0x00001778
		private void SetErrorState(Error error)
		{
			bool flag = !this.CheckIsRunning();
			if (!flag)
			{
				bool flag2 = this.IsCurrentStateEndErrorState();
				if (flag2)
				{
					this.StopStateMachine();
					this.RaiseStateMachineErroredEvent(error);
				}
				else
				{
					BaseErrorState baseErrorState = this.CurrentState.NextErrorState(error);
					bool flag3 = baseErrorState != this.CurrentState;
					if (flag3)
					{
						this.CurrentState.Stop();
						this.CurrentState.Finished -= this.CurrentStateFinished;
						this.CurrentState.Errored -= this.CurrentStateErrored;
						this.CurrentState = baseErrorState;
						baseErrorState.Finished += this.CurrentStateFinished;
						baseErrorState.Errored += this.CurrentStateErrored;
						baseErrorState.Start(error);
					}
				}
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000364C File Offset: 0x0000184C
		private void CurrentStateFinished(object sender, TransitionEventArgs eventArgs)
		{
			BaseState baseState = sender as BaseState;
			object obj = this.sync;
			lock (obj)
			{
				bool flag2 = this.CurrentState == baseState;
				if (flag2)
				{
					this.NextState(eventArgs);
				}
				else
				{
					string text = "Blocked state change attempt from:";
					BaseState baseState2 = this.CurrentState;
					string text2 = ((baseState2 != null) ? baseState2.ToString() : null);
					string text3 = " to:";
					BaseState baseState3 = baseState;
					Tracer<BaseStateMachine>.WriteWarning(text + text2 + text3 + ((baseState3 != null) ? baseState3.ToString() : null), new object[0]);
				}
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000036E8 File Offset: 0x000018E8
		private void CurrentStateErrored(object sender, Error error)
		{
			BaseState baseState = sender as BaseState;
			object obj = this.sync;
			lock (obj)
			{
				bool flag2 = this.CurrentState == baseState;
				if (flag2)
				{
					this.ErroredState(error);
				}
				else
				{
					string text = "Blocked state change attempt from:";
					BaseState baseState2 = this.CurrentState;
					string text2 = ((baseState2 != null) ? baseState2.ToString() : null);
					string text3 = " to:";
					BaseState baseState3 = baseState;
					Tracer<BaseStateMachine>.WriteWarning(text + text2 + text3 + ((baseState3 != null) ? baseState3.ToString() : null), new object[0]);
				}
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003784 File Offset: 0x00001984
		private bool IsCurrentStateEndState()
		{
			return this.CurrentState.DefaultTransition == null && this.CurrentState.ConditionalTransitions.Count == 0;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000037BC File Offset: 0x000019BC
		private bool IsCurrentStateEndErrorState()
		{
			return this.CurrentState is ErrorEndState;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000037DC File Offset: 0x000019DC
		private void RaiseStateMachineEvent(EventHandler eventHandler)
		{
			bool flag = eventHandler != null;
			if (flag)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003804 File Offset: 0x00001A04
		private void RaiseStateMachineErroredEvent(Error error)
		{
			EventHandler<BaseStateMachineErrorEventArgs> machineErrored = this.MachineErrored;
			bool flag = machineErrored != null;
			if (flag)
			{
				machineErrored(this, new BaseStateMachineErrorEventArgs(error));
			}
		}

		// Token: 0x04000016 RID: 22
		private readonly object sync = new object();

		// Token: 0x04000017 RID: 23
		private readonly List<BaseState> states;

		// Token: 0x04000018 RID: 24
		private BaseStateMachine.StateMachineState machineState;

		// Token: 0x04000019 RID: 25
		private BaseState currentState;

		// Token: 0x0400001A RID: 26
		private string machineName;

		// Token: 0x02000018 RID: 24
		private enum StateMachineState
		{
			// Token: 0x04000027 RID: 39
			Running,
			// Token: 0x04000028 RID: 40
			Stopped
		}
	}
}
