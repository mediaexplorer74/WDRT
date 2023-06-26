using System;
using System.ComponentModel.Composition;
using Microsoft.WindowsDeviceRecoveryTool.Common;

namespace Microsoft.WindowsDeviceRecoveryTool.Framework
{
	// Token: 0x02000086 RID: 134
	[Export]
	public class BaseViewModel : NotificationObject, IDisposable, ICanHandle
	{
		// Token: 0x0600048B RID: 1163 RVA: 0x000170E2 File Offset: 0x000152E2
		protected BaseViewModel()
		{
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x000170F3 File Offset: 0x000152F3
		// (set) Token: 0x0600048D RID: 1165 RVA: 0x000170FB File Offset: 0x000152FB
		public bool IsStarted { get; set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x00017104 File Offset: 0x00015304
		// (set) Token: 0x0600048F RID: 1167 RVA: 0x0001711C File Offset: 0x0001531C
		[Import]
		public ICommandRepository Commands
		{
			get
			{
				return this.commands;
			}
			private set
			{
				base.SetValue<ICommandRepository>(() => this.Commands, ref this.commands, value);
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x0001715C File Offset: 0x0001535C
		// (set) Token: 0x06000491 RID: 1169 RVA: 0x00017174 File Offset: 0x00015374
		[Import]
		public EventAggregator EventAggregator
		{
			get
			{
				return this.eventAggregator;
			}
			private set
			{
				this.eventAggregator = value;
				bool flag = this.eventAggregator != null;
				if (flag)
				{
					this.eventAggregator.Subscribe(this);
				}
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x000171A8 File Offset: 0x000153A8
		public virtual string PreviousStateName
		{
			get
			{
				return "PreviousState";
			}
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x000171BF File Offset: 0x000153BF
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x000171D4 File Offset: 0x000153D4
		protected virtual void Dispose(bool disposing)
		{
			bool flag = this.disposed;
			if (!flag)
			{
				if (disposing)
				{
					this.ReleaseManagedObjects();
				}
				this.ReleaseUnmanagedObjects();
				this.disposed = true;
			}
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0001720B File Offset: 0x0001540B
		public virtual void OnStarted()
		{
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0001720B File Offset: 0x0001540B
		public virtual void OnStopped()
		{
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0001720B File Offset: 0x0001540B
		protected virtual void ReleaseManagedObjects()
		{
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0001720B File Offset: 0x0001540B
		protected virtual void ReleaseUnmanagedObjects()
		{
		}

		// Token: 0x04000220 RID: 544
		private ICommandRepository commands;

		// Token: 0x04000221 RID: 545
		private EventAggregator eventAggregator;

		// Token: 0x04000222 RID: 546
		private bool disposed = false;
	}
}
