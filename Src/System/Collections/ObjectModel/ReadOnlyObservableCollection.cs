using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace System.Collections.ObjectModel
{
	/// <summary>Represents a read-only <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" />.</summary>
	/// <typeparam name="T">The type of elements in the collection.</typeparam>
	// Token: 0x020003BB RID: 955
	[TypeForwardedFrom("WindowsBase, Version=3.0.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	[global::__DynamicallyInvokable]
	[Serializable]
	public class ReadOnlyObservableCollection<T> : ReadOnlyCollection<T>, INotifyCollectionChanged, INotifyPropertyChanged
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.ReadOnlyObservableCollection`1" /> class that serves as a wrapper around the specified <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" />.</summary>
		/// <param name="list">The <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" /> with which to create this instance of the <see cref="T:System.Collections.ObjectModel.ReadOnlyObservableCollection`1" /> class.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is <see langword="null" />.</exception>
		// Token: 0x060023F7 RID: 9207 RVA: 0x000A8DCC File Offset: 0x000A6FCC
		[global::__DynamicallyInvokable]
		public ReadOnlyObservableCollection(ObservableCollection<T> list)
			: base(list)
		{
			((INotifyCollectionChanged)base.Items).CollectionChanged += this.HandleCollectionChanged;
			((INotifyPropertyChanged)base.Items).PropertyChanged += this.HandlePropertyChanged;
		}

		/// <summary>Occurs when the collection changes.</summary>
		// Token: 0x1400002E RID: 46
		// (add) Token: 0x060023F8 RID: 9208 RVA: 0x000A8E18 File Offset: 0x000A7018
		// (remove) Token: 0x060023F9 RID: 9209 RVA: 0x000A8E21 File Offset: 0x000A7021
		[global::__DynamicallyInvokable]
		event NotifyCollectionChangedEventHandler INotifyCollectionChanged.CollectionChanged
		{
			[global::__DynamicallyInvokable]
			add
			{
				this.CollectionChanged += value;
			}
			[global::__DynamicallyInvokable]
			remove
			{
				this.CollectionChanged -= value;
			}
		}

		/// <summary>Occurs when an item is added or removed.</summary>
		// Token: 0x1400002F RID: 47
		// (add) Token: 0x060023FA RID: 9210 RVA: 0x000A8E2C File Offset: 0x000A702C
		// (remove) Token: 0x060023FB RID: 9211 RVA: 0x000A8E64 File Offset: 0x000A7064
		[global::__DynamicallyInvokable]
		[method: global::__DynamicallyInvokable]
		[field: NonSerialized]
		protected virtual event NotifyCollectionChangedEventHandler CollectionChanged;

		/// <summary>Raises the <see cref="E:System.Collections.ObjectModel.ReadOnlyObservableCollection`1.CollectionChanged" /> event using the provided arguments.</summary>
		/// <param name="args">Arguments of the event being raised.</param>
		// Token: 0x060023FC RID: 9212 RVA: 0x000A8E99 File Offset: 0x000A7099
		[global::__DynamicallyInvokable]
		protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
		{
			if (this.CollectionChanged != null)
			{
				this.CollectionChanged(this, args);
			}
		}

		/// <summary>Occurs when a property value changes.</summary>
		// Token: 0x14000030 RID: 48
		// (add) Token: 0x060023FD RID: 9213 RVA: 0x000A8EB0 File Offset: 0x000A70B0
		// (remove) Token: 0x060023FE RID: 9214 RVA: 0x000A8EB9 File Offset: 0x000A70B9
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

		/// <summary>Occurs when a property value changes.</summary>
		// Token: 0x14000031 RID: 49
		// (add) Token: 0x060023FF RID: 9215 RVA: 0x000A8EC4 File Offset: 0x000A70C4
		// (remove) Token: 0x06002400 RID: 9216 RVA: 0x000A8EFC File Offset: 0x000A70FC
		[global::__DynamicallyInvokable]
		[method: global::__DynamicallyInvokable]
		[field: NonSerialized]
		protected virtual event PropertyChangedEventHandler PropertyChanged;

		/// <summary>Raises the <see cref="E:System.Collections.ObjectModel.ReadOnlyObservableCollection`1.PropertyChanged" /> event using the provided arguments.</summary>
		/// <param name="args">Arguments of the event being raised.</param>
		// Token: 0x06002401 RID: 9217 RVA: 0x000A8F31 File Offset: 0x000A7131
		[global::__DynamicallyInvokable]
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, args);
			}
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x000A8F48 File Offset: 0x000A7148
		private void HandleCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			this.OnCollectionChanged(e);
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x000A8F51 File Offset: 0x000A7151
		private void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			this.OnPropertyChanged(e);
		}
	}
}
