using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace System.Collections.ObjectModel
{
	/// <summary>Represents a dynamic data collection that provides notifications when items get added, removed, or when the whole list is refreshed.</summary>
	/// <typeparam name="T">The type of elements in the collection.</typeparam>
	// Token: 0x020003BA RID: 954
	[TypeForwardedFrom("WindowsBase, Version=3.0.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	[global::__DynamicallyInvokable]
	[Serializable]
	public class ObservableCollection<T> : Collection<T>, INotifyCollectionChanged, INotifyPropertyChanged
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" /> class.</summary>
		// Token: 0x060023DE RID: 9182 RVA: 0x000A89EC File Offset: 0x000A6BEC
		[global::__DynamicallyInvokable]
		public ObservableCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" /> class that contains elements copied from the specified list.</summary>
		/// <param name="list">The list from which the elements are copied.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="list" /> parameter cannot be <see langword="null" />.</exception>
		// Token: 0x060023DF RID: 9183 RVA: 0x000A89FF File Offset: 0x000A6BFF
		public ObservableCollection(List<T> list)
			: base((list != null) ? new List<T>(list.Count) : list)
		{
			this.CopyFrom(list);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" /> class that contains elements copied from the specified collection.</summary>
		/// <param name="collection">The collection from which the elements are copied.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="collection" /> parameter cannot be <see langword="null" />.</exception>
		// Token: 0x060023E0 RID: 9184 RVA: 0x000A8A2A File Offset: 0x000A6C2A
		[global::__DynamicallyInvokable]
		public ObservableCollection(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.CopyFrom(collection);
		}

		// Token: 0x060023E1 RID: 9185 RVA: 0x000A8A54 File Offset: 0x000A6C54
		private void CopyFrom(IEnumerable<T> collection)
		{
			IList<T> items = base.Items;
			if (collection != null && items != null)
			{
				foreach (T t in collection)
				{
					items.Add(t);
				}
			}
		}

		/// <summary>Moves the item at the specified index to a new location in the collection.</summary>
		/// <param name="oldIndex">The zero-based index specifying the location of the item to be moved.</param>
		/// <param name="newIndex">The zero-based index specifying the new location of the item.</param>
		// Token: 0x060023E2 RID: 9186 RVA: 0x000A8AA8 File Offset: 0x000A6CA8
		[global::__DynamicallyInvokable]
		public void Move(int oldIndex, int newIndex)
		{
			this.MoveItem(oldIndex, newIndex);
		}

		/// <summary>Occurs when a property value changes.</summary>
		// Token: 0x1400002B RID: 43
		// (add) Token: 0x060023E3 RID: 9187 RVA: 0x000A8AB2 File Offset: 0x000A6CB2
		// (remove) Token: 0x060023E4 RID: 9188 RVA: 0x000A8ABB File Offset: 0x000A6CBB
		[global::__DynamicallyInvokable]
		event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
		{
			[global::__DynamicallyInvokable]
			add
			{
				this.PropertyChanged += value;
			}
			[global::__DynamicallyInvokable]
			remove
			{
				this.PropertyChanged -= value;
			}
		}

		/// <summary>Occurs when an item is added, removed, changed, moved, or the entire list is refreshed.</summary>
		// Token: 0x1400002C RID: 44
		// (add) Token: 0x060023E5 RID: 9189 RVA: 0x000A8AC4 File Offset: 0x000A6CC4
		// (remove) Token: 0x060023E6 RID: 9190 RVA: 0x000A8AFC File Offset: 0x000A6CFC
		[global::__DynamicallyInvokable]
		[method: global::__DynamicallyInvokable]
		[field: NonSerialized]
		public virtual event NotifyCollectionChangedEventHandler CollectionChanged;

		/// <summary>Removes all items from the collection.</summary>
		// Token: 0x060023E7 RID: 9191 RVA: 0x000A8B31 File Offset: 0x000A6D31
		[global::__DynamicallyInvokable]
		protected override void ClearItems()
		{
			this.CheckReentrancy();
			base.ClearItems();
			this.OnPropertyChanged("Count");
			this.OnPropertyChanged("Item[]");
			this.OnCollectionReset();
		}

		/// <summary>Removes the item at the specified index of the collection.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		// Token: 0x060023E8 RID: 9192 RVA: 0x000A8B5C File Offset: 0x000A6D5C
		[global::__DynamicallyInvokable]
		protected override void RemoveItem(int index)
		{
			this.CheckReentrancy();
			T t = base[index];
			base.RemoveItem(index);
			this.OnPropertyChanged("Count");
			this.OnPropertyChanged("Item[]");
			this.OnCollectionChanged(NotifyCollectionChangedAction.Remove, t, index);
		}

		/// <summary>Inserts an item into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
		/// <param name="item">The object to insert.</param>
		// Token: 0x060023E9 RID: 9193 RVA: 0x000A8BA2 File Offset: 0x000A6DA2
		[global::__DynamicallyInvokable]
		protected override void InsertItem(int index, T item)
		{
			this.CheckReentrancy();
			base.InsertItem(index, item);
			this.OnPropertyChanged("Count");
			this.OnPropertyChanged("Item[]");
			this.OnCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
		}

		/// <summary>Replaces the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to replace.</param>
		/// <param name="item">The new value for the element at the specified index.</param>
		// Token: 0x060023EA RID: 9194 RVA: 0x000A8BD8 File Offset: 0x000A6DD8
		[global::__DynamicallyInvokable]
		protected override void SetItem(int index, T item)
		{
			this.CheckReentrancy();
			T t = base[index];
			base.SetItem(index, item);
			this.OnPropertyChanged("Item[]");
			this.OnCollectionChanged(NotifyCollectionChangedAction.Replace, t, item, index);
		}

		/// <summary>Moves the item at the specified index to a new location in the collection.</summary>
		/// <param name="oldIndex">The zero-based index specifying the location of the item to be moved.</param>
		/// <param name="newIndex">The zero-based index specifying the new location of the item.</param>
		// Token: 0x060023EB RID: 9195 RVA: 0x000A8C1C File Offset: 0x000A6E1C
		[global::__DynamicallyInvokable]
		protected virtual void MoveItem(int oldIndex, int newIndex)
		{
			this.CheckReentrancy();
			T t = base[oldIndex];
			base.RemoveItem(oldIndex);
			base.InsertItem(newIndex, t);
			this.OnPropertyChanged("Item[]");
			this.OnCollectionChanged(NotifyCollectionChangedAction.Move, t, newIndex, oldIndex);
		}

		/// <summary>Raises the <see cref="E:System.Collections.ObjectModel.ObservableCollection`1.PropertyChanged" /> event with the provided arguments.</summary>
		/// <param name="e">Arguments of the event being raised.</param>
		// Token: 0x060023EC RID: 9196 RVA: 0x000A8C60 File Offset: 0x000A6E60
		[global::__DynamicallyInvokable]
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, e);
			}
		}

		/// <summary>Occurs when a property value changes.</summary>
		// Token: 0x1400002D RID: 45
		// (add) Token: 0x060023ED RID: 9197 RVA: 0x000A8C78 File Offset: 0x000A6E78
		// (remove) Token: 0x060023EE RID: 9198 RVA: 0x000A8CB0 File Offset: 0x000A6EB0
		[global::__DynamicallyInvokable]
		[method: global::__DynamicallyInvokable]
		[field: NonSerialized]
		protected virtual event PropertyChangedEventHandler PropertyChanged;

		/// <summary>Raises the <see cref="E:System.Collections.ObjectModel.ObservableCollection`1.CollectionChanged" /> event with the provided arguments.</summary>
		/// <param name="e">Arguments of the event being raised.</param>
		// Token: 0x060023EF RID: 9199 RVA: 0x000A8CE8 File Offset: 0x000A6EE8
		[global::__DynamicallyInvokable]
		protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			if (this.CollectionChanged != null)
			{
				using (this.BlockReentrancy())
				{
					this.CollectionChanged(this, e);
				}
			}
		}

		/// <summary>Disallows reentrant attempts to change this collection.</summary>
		/// <returns>An <see cref="T:System.IDisposable" /> object that can be used to dispose of the object.</returns>
		// Token: 0x060023F0 RID: 9200 RVA: 0x000A8D30 File Offset: 0x000A6F30
		[global::__DynamicallyInvokable]
		protected IDisposable BlockReentrancy()
		{
			this._monitor.Enter();
			return this._monitor;
		}

		/// <summary>Checks for reentrant attempts to change this collection.</summary>
		/// <exception cref="T:System.InvalidOperationException">If there was a call to <see cref="M:System.Collections.ObjectModel.ObservableCollection`1.BlockReentrancy" /> of which the <see cref="T:System.IDisposable" /> return value has not yet been disposed of. Typically, this means when there are additional attempts to change this collection during a <see cref="E:System.Collections.ObjectModel.ObservableCollection`1.CollectionChanged" /> event. However, it depends on when derived classes choose to call <see cref="M:System.Collections.ObjectModel.ObservableCollection`1.BlockReentrancy" />.</exception>
		// Token: 0x060023F1 RID: 9201 RVA: 0x000A8D43 File Offset: 0x000A6F43
		[global::__DynamicallyInvokable]
		protected void CheckReentrancy()
		{
			if (this._monitor.Busy && this.CollectionChanged != null && this.CollectionChanged.GetInvocationList().Length > 1)
			{
				throw new InvalidOperationException(SR.GetString("ObservableCollectionReentrancyNotAllowed"));
			}
		}

		// Token: 0x060023F2 RID: 9202 RVA: 0x000A8D7A File Offset: 0x000A6F7A
		private void OnPropertyChanged(string propertyName)
		{
			this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
		}

		// Token: 0x060023F3 RID: 9203 RVA: 0x000A8D88 File Offset: 0x000A6F88
		private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index)
		{
			this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));
		}

		// Token: 0x060023F4 RID: 9204 RVA: 0x000A8D98 File Offset: 0x000A6F98
		private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index, int oldIndex)
		{
			this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index, oldIndex));
		}

		// Token: 0x060023F5 RID: 9205 RVA: 0x000A8DAA File Offset: 0x000A6FAA
		private void OnCollectionChanged(NotifyCollectionChangedAction action, object oldItem, object newItem, int index)
		{
			this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, newItem, oldItem, index));
		}

		// Token: 0x060023F6 RID: 9206 RVA: 0x000A8DBC File Offset: 0x000A6FBC
		private void OnCollectionReset()
		{
			this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}

		// Token: 0x04001FE1 RID: 8161
		private const string CountString = "Count";

		// Token: 0x04001FE2 RID: 8162
		private const string IndexerName = "Item[]";

		// Token: 0x04001FE3 RID: 8163
		private ObservableCollection<T>.SimpleMonitor _monitor = new ObservableCollection<T>.SimpleMonitor();

		// Token: 0x020007F0 RID: 2032
		[TypeForwardedFrom("WindowsBase, Version=3.0.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
		[Serializable]
		private class SimpleMonitor : IDisposable
		{
			// Token: 0x060043FF RID: 17407 RVA: 0x0011D828 File Offset: 0x0011BA28
			public void Enter()
			{
				this._busyCount++;
			}

			// Token: 0x06004400 RID: 17408 RVA: 0x0011D838 File Offset: 0x0011BA38
			public void Dispose()
			{
				this._busyCount--;
			}

			// Token: 0x17000F6B RID: 3947
			// (get) Token: 0x06004401 RID: 17409 RVA: 0x0011D848 File Offset: 0x0011BA48
			public bool Busy
			{
				get
				{
					return this._busyCount > 0;
				}
			}

			// Token: 0x040034F2 RID: 13554
			private int _busyCount;
		}
	}
}
