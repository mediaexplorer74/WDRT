using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Microsoft.WindowsDeviceRecoveryTool.Common
{
	// Token: 0x02000007 RID: 7
	[CompilerGenerated]
	public class NotificationObject : INotifyPropertyChanged
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000010 RID: 16 RVA: 0x0000223C File Offset: 0x0000043C
		// (remove) Token: 0x06000011 RID: 17 RVA: 0x00002274 File Offset: 0x00000474
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000012 RID: 18 RVA: 0x000022AC File Offset: 0x000004AC
		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			this.RaisePropertyChanged(propertyChanged, propertyName);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022CC File Offset: 0x000004CC
		protected virtual void RaisePropertyChanged(PropertyChangedEventHandler handler, string propertyName)
		{
			bool flag = handler != null;
			if (flag)
			{
				AppDispatcher.Execute(delegate
				{
					handler(this, new PropertyChangedEventArgs(propertyName));
				}, false);
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002318 File Offset: 0x00000518
		protected void RaisePropertyChanged<T>(Expression<Func<T>> expression)
		{
			string name = ReflectionHelper.GetName<T>(expression);
			this.OnPropertyChanged(name);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002338 File Offset: 0x00000538
		protected void SetValue<T>(Expression<Func<T>> expression, Action setValueAction)
		{
			bool flag = setValueAction != null;
			if (flag)
			{
				setValueAction();
			}
			this.RaisePropertyChanged<T>(expression);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000235F File Offset: 0x0000055F
		protected void SetValue<T>(Expression<Func<T>> expression, ref T backfield, T value)
		{
			backfield = value;
			this.RaisePropertyChanged<T>(expression);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002374 File Offset: 0x00000574
		protected void SetValue<T>(Expression<Func<T>> expression, Action<T> setValueAction, T value)
		{
			bool flag = setValueAction != null;
			if (flag)
			{
				setValueAction(value);
				this.RaisePropertyChanged<T>(expression);
			}
		}
	}
}
