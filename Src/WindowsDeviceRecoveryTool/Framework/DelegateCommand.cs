using System;
using System.Diagnostics;
using System.Windows.Input;
using Microsoft.WindowsDeviceRecoveryTool.Common;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;

namespace Microsoft.WindowsDeviceRecoveryTool.Framework
{
	// Token: 0x02000089 RID: 137
	public class DelegateCommand<T> : IDelegateCommand, ICommand
	{
		// Token: 0x060004A2 RID: 1186 RVA: 0x0001727F File Offset: 0x0001547F
		public DelegateCommand(Action<T> execute)
			: this(execute, null, null)
		{
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0001728C File Offset: 0x0001548C
		public DelegateCommand(Action<T> execute, Func<object, bool> canExecute)
			: this(execute, canExecute, null)
		{
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00017299 File Offset: 0x00015499
		public DelegateCommand(Action<T> execute, KeyGesture keyGesture)
			: this(execute, null, keyGesture)
		{
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x000172A6 File Offset: 0x000154A6
		public DelegateCommand(Action<T> execute, Func<object, bool> canExecute, KeyGesture keyGesture)
		{
			this.execute = execute;
			this.canExecute = canExecute;
			this.KeyGesture = keyGesture;
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060004A6 RID: 1190 RVA: 0x000172C8 File Offset: 0x000154C8
		// (remove) Token: 0x060004A7 RID: 1191 RVA: 0x00017300 File Offset: 0x00015500
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler CanExecuteChanged;

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x00017335 File Offset: 0x00015535
		// (set) Token: 0x060004A9 RID: 1193 RVA: 0x0001733D File Offset: 0x0001553D
		public KeyGesture KeyGesture { get; private set; }

		// Token: 0x060004AA RID: 1194 RVA: 0x00017348 File Offset: 0x00015548
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
					Tracer<IDelegateCommand>.WriteError(ex);
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

		// Token: 0x060004AB RID: 1195 RVA: 0x0001739C File Offset: 0x0001559C
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

		// Token: 0x060004AC RID: 1196 RVA: 0x000173DC File Offset: 0x000155DC
		public virtual void Execute(T parameter)
		{
			try
			{
				AppDispatcher.Execute(delegate
				{
					this.execute(parameter);
				}, false);
			}
			catch (Exception ex)
			{
				Tracer<IDelegateCommand>.WriteError(ex);
				throw;
			}
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00017430 File Offset: 0x00015630
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

		// Token: 0x04000226 RID: 550
		private readonly Action<T> execute;

		// Token: 0x04000227 RID: 551
		private readonly Func<object, bool> canExecute;
	}
}
