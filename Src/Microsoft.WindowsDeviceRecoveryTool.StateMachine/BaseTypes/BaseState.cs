using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.StateMachine.DefaultTypes;

namespace Microsoft.WindowsDeviceRecoveryTool.StateMachine.BaseTypes
{
	// Token: 0x02000010 RID: 16
	public class BaseState
	{
		// Token: 0x06000038 RID: 56 RVA: 0x000026E7 File Offset: 0x000008E7
		protected BaseState()
		{
			this.ConditionalTransitions = new Collection<BaseTransition>();
			this.ErrorTransitions = new Dictionary<Type, ErrorTransition>();
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000039 RID: 57 RVA: 0x0000270C File Offset: 0x0000090C
		// (remove) Token: 0x0600003A RID: 58 RVA: 0x00002744 File Offset: 0x00000944
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<EventArgs> StateStarted;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600003B RID: 59 RVA: 0x0000277C File Offset: 0x0000097C
		// (remove) Token: 0x0600003C RID: 60 RVA: 0x000027B4 File Offset: 0x000009B4
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<TransitionEventArgs> Finished;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600003D RID: 61 RVA: 0x000027EC File Offset: 0x000009EC
		// (remove) Token: 0x0600003E RID: 62 RVA: 0x00002824 File Offset: 0x00000A24
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<Error> Errored;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600003F RID: 63 RVA: 0x0000285C File Offset: 0x00000A5C
		// (remove) Token: 0x06000040 RID: 64 RVA: 0x00002894 File Offset: 0x00000A94
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler Closing;

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000028C9 File Offset: 0x00000AC9
		// (set) Token: 0x06000042 RID: 66 RVA: 0x000028D1 File Offset: 0x00000AD1
		public DefaultTransition DefaultTransition { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000028DA File Offset: 0x00000ADA
		// (set) Token: 0x06000044 RID: 68 RVA: 0x000028E2 File Offset: 0x00000AE2
		public Collection<BaseTransition> ConditionalTransitions { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000028EB File Offset: 0x00000AEB
		// (set) Token: 0x06000046 RID: 70 RVA: 0x000028F3 File Offset: 0x00000AF3
		public ErrorTransition DefaultErrorTransition { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000028FC File Offset: 0x00000AFC
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002904 File Offset: 0x00000B04
		public Dictionary<Type, ErrorTransition> ErrorTransitions { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000049 RID: 73 RVA: 0x0000290D File Offset: 0x00000B0D
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002915 File Offset: 0x00000B15
		public bool Started { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600004B RID: 75 RVA: 0x0000291E File Offset: 0x00000B1E
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00002926 File Offset: 0x00000B26
		public string MachineName { get; set; }

		// Token: 0x0600004D RID: 77 RVA: 0x00002930 File Offset: 0x00000B30
		public static BaseState NullObject()
		{
			return new BaseState();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002947 File Offset: 0x00000B47
		public void AddConditionalTransition(BaseTransition transition)
		{
			this.ConditionalTransitions.Add(transition);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002957 File Offset: 0x00000B57
		public void AddErrorTransition(ErrorTransition transition, Exception exception)
		{
			this.ErrorTransitions.Add(exception.GetType(), transition);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002970 File Offset: 0x00000B70
		public virtual void Start()
		{
			bool flag = !this.Started;
			if (flag)
			{
				Tracer<BaseState>.WriteInformation(string.Format("Started state: {0} ({1})", this.ToString(), this.MachineName));
				this.Started = true;
				this.RaiseStateStarted(EventArgs.Empty);
			}
			else
			{
				Tracer<BaseState>.WriteWarning("Trying to start state {0} ({1}) which is already started!", new object[]
				{
					this.ToString(),
					this.MachineName
				});
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000029E4 File Offset: 0x00000BE4
		public virtual void Stop()
		{
			bool started = this.Started;
			if (started)
			{
				Tracer<BaseState>.WriteInformation(string.Format("Stopped state: {0} ({1})", this.ToString(), this.MachineName));
				this.Started = false;
			}
			else
			{
				Tracer<BaseState>.WriteWarning("Trying to stop state {0} ({1}) which is already stopped!", new object[]
				{
					this.ToString(),
					this.MachineName
				});
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002A4C File Offset: 0x00000C4C
		public virtual BaseState NextState(TransitionEventArgs eventArgs)
		{
			BaseState baseState = this;
			BaseTransition baseTransition = this.DefaultTransition;
			Tracer<BaseState>.WriteInformation(string.Format("Getting Next state of {0} ({1})", this.ToString(), this.MachineName));
			try
			{
				foreach (BaseTransition baseTransition2 in this.ConditionalTransitions)
				{
					bool flag = baseTransition2.ConditionsAreMet(this, eventArgs);
					if (flag)
					{
						baseTransition = baseTransition2;
						Tracer<BaseState>.WriteInformation("Conditions are met for {0}", new object[] { baseTransition2.ToString() });
						break;
					}
				}
			}
			catch (Exception ex)
			{
				Tracer<BaseState>.WriteError(ex, "Checking transitions is failed: unexpected error", new object[0]);
				return this.HandleTransitionException(baseState, ex);
			}
			bool flag2 = baseTransition != null;
			if (flag2)
			{
				bool flag3 = baseTransition == this.DefaultTransition;
				if (flag3)
				{
					Tracer<BaseState>.WriteInformation("Selecting Default transition {0}", new object[] { baseTransition.Next.ToString() });
				}
				Tracer<BaseState>.WriteInformation(string.Format("Next state of {0} is {1}", this.ToString(), baseTransition.Next));
				baseState = baseTransition.Next;
			}
			return baseState;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002B84 File Offset: 0x00000D84
		public void Finish(string status)
		{
			this.RaiseStateFinished(string.IsNullOrEmpty(status) ? TransitionEventArgs.Empty : new TransitionEventArgs(status));
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002BA3 File Offset: 0x00000DA3
		public void Error(Exception exception)
		{
			this.RaiseStateErrored(new Error(exception));
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002BB4 File Offset: 0x00000DB4
		private BaseState HandleTransitionException(BaseState state, Exception exception)
		{
			Error error = new Error(exception);
			BaseErrorState baseErrorState = this.NextErrorState(error);
			bool flag = baseErrorState != null;
			if (flag)
			{
				baseErrorState.Start(error);
				state = baseErrorState;
			}
			return state;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002BEC File Offset: 0x00000DEC
		public virtual BaseErrorState NextErrorState(Error error)
		{
			Tracer<BaseState>.WriteInformation(string.Format("Getting Next Error state of {0}, error code: {1}", this.ToString(), error.Message));
			bool flag = this.ErrorTransitions.ContainsKey(error.ExceptionType);
			BaseErrorState baseErrorState;
			if (flag)
			{
				baseErrorState = this.ErrorTransitions[error.ExceptionType].Next;
			}
			else
			{
				bool flag2 = this.DefaultErrorTransition != null;
				if (!flag2)
				{
					string text = "There is no error transition for code: ";
					Type exceptionType = error.ExceptionType;
					throw new InvalidOperationException(text + ((exceptionType != null) ? exceptionType.ToString() : null));
				}
				baseErrorState = this.DefaultErrorTransition.Next;
				Tracer<BaseState>.WriteInformation("Selecting Default error state {0}", new object[] { baseErrorState.ToString() });
			}
			bool flag3 = baseErrorState != null;
			if (flag3)
			{
				Tracer<BaseState>.WriteInformation(string.Format("Next Error state of {0} is {1}", this.ToString(), baseErrorState));
			}
			return baseErrorState;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002CC8 File Offset: 0x00000EC8
		protected virtual void RaiseStateStarted(EventArgs eventArgs)
		{
			EventHandler<EventArgs> stateStarted = this.StateStarted;
			bool flag = stateStarted != null;
			if (flag)
			{
				stateStarted(this, eventArgs);
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002CF0 File Offset: 0x00000EF0
		protected virtual void RaiseStateFinished(TransitionEventArgs eventArgs)
		{
			EventHandler<TransitionEventArgs> finished = this.Finished;
			bool flag = finished != null;
			if (flag)
			{
				finished(this, eventArgs);
			}
			else
			{
				Tracer<BaseState>.WriteWarning("Finishing state without handler {0} ({1}), status: {2}", new object[]
				{
					this.ToString(),
					this.MachineName,
					(eventArgs == null || string.IsNullOrEmpty(eventArgs.Status)) ? "empty" : eventArgs.Status
				});
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002D60 File Offset: 0x00000F60
		protected virtual void RaiseStateErrored(Error error)
		{
			EventHandler<Error> errored = this.Errored;
			bool flag = errored != null;
			if (flag)
			{
				errored(this, error);
			}
			else
			{
				Tracer<BaseState>.WriteWarning("Error in state without handler {0} ({1})", new object[]
				{
					this.ToString(),
					this.MachineName
				});
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002DB0 File Offset: 0x00000FB0
		public override string ToString()
		{
			return base.ToString().Substring(base.ToString().LastIndexOf('.') + 1);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002DDC File Offset: 0x00000FDC
		protected void SendClosingEvent()
		{
			EventHandler closing = this.Closing;
			bool flag = closing != null;
			if (flag)
			{
				closing(this, EventArgs.Empty);
			}
		}
	}
}
