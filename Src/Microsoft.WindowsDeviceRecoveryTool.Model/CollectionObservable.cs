using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using Microsoft.WindowsDeviceRecoveryTool.Common;

namespace Microsoft.WindowsDeviceRecoveryTool.Model
{
	// Token: 0x02000005 RID: 5
	public sealed class CollectionObservable<T> : ObservableCollection<T> where T : NotificationObject
	{
		// Token: 0x0600002A RID: 42 RVA: 0x000027F1 File Offset: 0x000009F1
		public CollectionObservable()
		{
			this.CollectionChanged += this.CollectionObservableCollectionChanged;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002810 File Offset: 0x00000A10
		private void CollectionObservableCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			bool flag = e.NewItems != null;
			if (flag)
			{
				foreach (object obj in e.NewItems)
				{
					INotifyPropertyChanged notifyPropertyChanged = obj as INotifyPropertyChanged;
					bool flag2 = notifyPropertyChanged != null;
					if (flag2)
					{
						notifyPropertyChanged.PropertyChanged += this.ItemPropertyChanged;
					}
				}
			}
			bool flag3 = e.OldItems != null;
			if (flag3)
			{
				foreach (object obj2 in e.OldItems)
				{
					INotifyPropertyChanged notifyPropertyChanged2 = obj2 as INotifyPropertyChanged;
					bool flag4 = notifyPropertyChanged2 != null;
					if (flag4)
					{
						notifyPropertyChanged2.PropertyChanged -= this.ItemPropertyChanged;
					}
				}
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002920 File Offset: 0x00000B20
		private void ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
			this.OnCollectionChanged(notifyCollectionChangedEventArgs);
		}
	}
}
