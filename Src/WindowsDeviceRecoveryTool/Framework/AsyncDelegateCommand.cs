using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.WindowsDeviceRecoveryTool.Common;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;

namespace Microsoft.WindowsDeviceRecoveryTool.Framework
{
	// Token: 0x0200007E RID: 126
	public class AsyncDelegateCommand<T> : IAsyncDelegateCommand, IDelegateCommand, ICommand, IDisposable
	{
		// Token: 0x06000448 RID: 1096 RVA: 0x000162FC File Offset: 0x000144FC
		public AsyncDelegateCommand(Action<T, CancellationToken> execute)
			: this(execute, null, null)
		{
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00016309 File Offset: 0x00014509
		public AsyncDelegateCommand(Action<T, CancellationToken> execute, Func<object, bool> canExecute)
			: this(execute, canExecute, null)
		{
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00016316 File Offset: 0x00014516
		public AsyncDelegateCommand(Action<T, CancellationToken> execute, KeyGesture keyGesture)
			: this(execute, null, keyGesture)
		{
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00016323 File Offset: 0x00014523
		public AsyncDelegateCommand(Action<T, CancellationToken> execute, Func<object, bool> canExecute, KeyGesture keyGesture)
		{
			this.execute = execute;
			this.canExecute = canExecute;
			this.KeyGesture = keyGesture;
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600044C RID: 1100 RVA: 0x00016344 File Offset: 0x00014544
		// (remove) Token: 0x0600044D RID: 1101 RVA: 0x0001637C File Offset: 0x0001457C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler CanExecuteChanged;

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x000163B1 File Offset: 0x000145B1
		// (set) Token: 0x0600044F RID: 1103 RVA: 0x000163B9 File Offset: 0x000145B9
		public KeyGesture KeyGesture { get; private set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x000163C4 File Offset: 0x000145C4
		public CancellationTokenSource CancellationTokenSource
		{
			get
			{
				return this.tokenSource;
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x000163DC File Offset: 0x000145DC
		public bool CanExecute(object parameter)
		{
			bool flag = this.canExecute != null;
			bool flag3;
			if (flag)
			{
				bool flag2 = false;
				try
				{
					flag2 = this.canExecute(parameter);
				}
				catch (Exception ex)
				{
					Tracer<IAsyncDelegateCommand>.WriteError(ex);
					throw;
				}
				flag3 = flag2;
			}
			else
			{
				flag3 = true;
			}
			return flag3;
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00016430 File Offset: 0x00014630
		public void Execute(object parameter)
		{
			bool flag = parameter is T;
			if (flag)
			{
				this.Execute((T)((object)parameter));
			}
			else
			{
				this.Execute(default(T));
			}
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00016470 File Offset: 0x00014670
		public virtual void Execute(T parameter)
		{
			this.tokenSource = new CancellationTokenSource();
			this.currentTask = Task.Run(delegate
			{
				this.SafeExecute(parameter);
			});
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x000164B4 File Offset: 0x000146B4
		public void Cancel()
		{
			bool flag = this.tokenSource != null;
			if (flag)
			{
				this.tokenSource.Cancel();
			}
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x000164E0 File Offset: 0x000146E0
		public void Wait()
		{
			bool flag = this.currentTask != null && !this.currentTask.IsCompleted;
			if (flag)
			{
				this.currentTask.Wait();
			}
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0001651C File Offset: 0x0001471C
		public void RaiseCanExecuteChanged()
		{
			bool flag = this.CanExecuteChanged != null;
			if (flag)
			{
				AppDispatcher.Execute(delegate
				{
					this.CanExecuteChanged(this, EventArgs.Empty);
				}, false);
			}
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0001654C File Offset: 0x0001474C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00016560 File Offset: 0x00014760
		private void Dispose(bool disposing)
		{
			bool flag = this.disposed;
			if (!flag)
			{
				if (disposing)
				{
					bool flag2 = this.tokenSource != null;
					if (flag2)
					{
						this.tokenSource.Dispose();
					}
				}
				this.disposed = true;
			}
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x000165A4 File Offset: 0x000147A4
		private void SafeExecute(T parameter)
		{
			try
			{
				this.execute(parameter, this.tokenSource.Token);
			}
			catch (Exception ex)
			{
				Exception ex3;
				Exception ex2 = ex3;
				Exception ex = ex2;
				Tracer<IAsyncDelegateCommand>.WriteError(ex);
				AppDispatcher.Execute(delegate
				{
					throw ex;
				}, false);
			}
		}

		// Token: 0x04000213 RID: 531
		private readonly Action<T, CancellationToken> execute;

		// Token: 0x04000214 RID: 532
		private readonly Func<object, bool> canExecute;

		// Token: 0x04000215 RID: 533
		private CancellationTokenSource tokenSource;

		// Token: 0x04000216 RID: 534
		private Task currentTask;

		// Token: 0x04000217 RID: 535
		private bool disposed;
	}
}
