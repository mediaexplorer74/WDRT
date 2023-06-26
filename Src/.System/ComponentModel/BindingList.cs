using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a generic collection that supports data binding.</summary>
	/// <typeparam name="T">The type of elements in the list.</typeparam>
	// Token: 0x0200051C RID: 1308
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[Serializable]
	public class BindingList<T> : Collection<T>, IBindingList, IList, ICollection, IEnumerable, ICancelAddNew, IRaiseItemChangedEvents
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BindingList`1" /> class using default values.</summary>
		// Token: 0x06003175 RID: 12661 RVA: 0x000DF34B File Offset: 0x000DD54B
		public BindingList()
		{
			this.Initialize();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BindingList`1" /> class with the specified list.</summary>
		/// <param name="list">An <see cref="T:System.Collections.Generic.IList`1" /> of items to be contained in the <see cref="T:System.ComponentModel.BindingList`1" />.</param>
		// Token: 0x06003176 RID: 12662 RVA: 0x000DF383 File Offset: 0x000DD583
		public BindingList(IList<T> list)
			: base(list)
		{
			this.Initialize();
		}

		// Token: 0x06003177 RID: 12663 RVA: 0x000DF3BC File Offset: 0x000DD5BC
		private void Initialize()
		{
			this.allowNew = this.ItemTypeHasDefaultConstructor;
			if (typeof(INotifyPropertyChanged).IsAssignableFrom(typeof(T)))
			{
				this.raiseItemChangedEvents = true;
				foreach (T t in base.Items)
				{
					this.HookPropertyChanged(t);
				}
			}
		}

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x06003178 RID: 12664 RVA: 0x000DF438 File Offset: 0x000DD638
		private bool ItemTypeHasDefaultConstructor
		{
			get
			{
				Type typeFromHandle = typeof(T);
				return typeFromHandle.IsPrimitive || typeFromHandle.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, new Type[0], null) != null;
			}
		}

		/// <summary>Occurs before an item is added to the list.</summary>
		// Token: 0x14000048 RID: 72
		// (add) Token: 0x06003179 RID: 12665 RVA: 0x000DF478 File Offset: 0x000DD678
		// (remove) Token: 0x0600317A RID: 12666 RVA: 0x000DF4B4 File Offset: 0x000DD6B4
		public event AddingNewEventHandler AddingNew
		{
			add
			{
				bool flag = this.AllowNew;
				this.onAddingNew = (AddingNewEventHandler)Delegate.Combine(this.onAddingNew, value);
				if (flag != this.AllowNew)
				{
					this.FireListChanged(ListChangedType.Reset, -1);
				}
			}
			remove
			{
				bool flag = this.AllowNew;
				this.onAddingNew = (AddingNewEventHandler)Delegate.Remove(this.onAddingNew, value);
				if (flag != this.AllowNew)
				{
					this.FireListChanged(ListChangedType.Reset, -1);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BindingList`1.AddingNew" /> event.</summary>
		/// <param name="e">An <see cref="T:System.ComponentModel.AddingNewEventArgs" /> that contains the event data.</param>
		// Token: 0x0600317B RID: 12667 RVA: 0x000DF4F0 File Offset: 0x000DD6F0
		protected virtual void OnAddingNew(AddingNewEventArgs e)
		{
			if (this.onAddingNew != null)
			{
				this.onAddingNew(this, e);
			}
		}

		// Token: 0x0600317C RID: 12668 RVA: 0x000DF508 File Offset: 0x000DD708
		private object FireAddingNew()
		{
			AddingNewEventArgs addingNewEventArgs = new AddingNewEventArgs(null);
			this.OnAddingNew(addingNewEventArgs);
			return addingNewEventArgs.NewObject;
		}

		/// <summary>Occurs when the list or an item in the list changes.</summary>
		// Token: 0x14000049 RID: 73
		// (add) Token: 0x0600317D RID: 12669 RVA: 0x000DF529 File Offset: 0x000DD729
		// (remove) Token: 0x0600317E RID: 12670 RVA: 0x000DF542 File Offset: 0x000DD742
		public event ListChangedEventHandler ListChanged
		{
			add
			{
				this.onListChanged = (ListChangedEventHandler)Delegate.Combine(this.onListChanged, value);
			}
			remove
			{
				this.onListChanged = (ListChangedEventHandler)Delegate.Remove(this.onListChanged, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.ListChangedEventArgs" /> that contains the event data.</param>
		// Token: 0x0600317F RID: 12671 RVA: 0x000DF55B File Offset: 0x000DD75B
		protected virtual void OnListChanged(ListChangedEventArgs e)
		{
			if (this.onListChanged != null)
			{
				this.onListChanged(this, e);
			}
		}

		/// <summary>Gets or sets a value indicating whether adding or removing items within the list raises <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> events.</summary>
		/// <returns>
		///   <see langword="true" /> if adding or removing items raises <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> events; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x06003180 RID: 12672 RVA: 0x000DF572 File Offset: 0x000DD772
		// (set) Token: 0x06003181 RID: 12673 RVA: 0x000DF57A File Offset: 0x000DD77A
		public bool RaiseListChangedEvents
		{
			get
			{
				return this.raiseListChangedEvents;
			}
			set
			{
				if (this.raiseListChangedEvents != value)
				{
					this.raiseListChangedEvents = value;
				}
			}
		}

		/// <summary>Raises a <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> event of type <see cref="F:System.ComponentModel.ListChangedType.Reset" />.</summary>
		// Token: 0x06003182 RID: 12674 RVA: 0x000DF58C File Offset: 0x000DD78C
		public void ResetBindings()
		{
			this.FireListChanged(ListChangedType.Reset, -1);
		}

		/// <summary>Raises a <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> event of type <see cref="F:System.ComponentModel.ListChangedType.ItemChanged" /> for the item at the specified position.</summary>
		/// <param name="position">A zero-based index of the item to be reset.</param>
		// Token: 0x06003183 RID: 12675 RVA: 0x000DF596 File Offset: 0x000DD796
		public void ResetItem(int position)
		{
			this.FireListChanged(ListChangedType.ItemChanged, position);
		}

		// Token: 0x06003184 RID: 12676 RVA: 0x000DF5A0 File Offset: 0x000DD7A0
		private void FireListChanged(ListChangedType type, int index)
		{
			if (this.raiseListChangedEvents)
			{
				this.OnListChanged(new ListChangedEventArgs(type, index));
			}
		}

		/// <summary>Removes all elements from the collection.</summary>
		// Token: 0x06003185 RID: 12677 RVA: 0x000DF5B8 File Offset: 0x000DD7B8
		protected override void ClearItems()
		{
			this.EndNew(this.addNewPos);
			if (this.raiseItemChangedEvents)
			{
				foreach (T t in base.Items)
				{
					this.UnhookPropertyChanged(t);
				}
			}
			base.ClearItems();
			this.FireListChanged(ListChangedType.Reset, -1);
		}

		/// <summary>Inserts the specified item in the list at the specified index.</summary>
		/// <param name="index">The zero-based index where the item is to be inserted.</param>
		/// <param name="item">The item to insert in the list.</param>
		// Token: 0x06003186 RID: 12678 RVA: 0x000DF628 File Offset: 0x000DD828
		protected override void InsertItem(int index, T item)
		{
			this.EndNew(this.addNewPos);
			base.InsertItem(index, item);
			if (this.raiseItemChangedEvents)
			{
				this.HookPropertyChanged(item);
			}
			this.FireListChanged(ListChangedType.ItemAdded, index);
		}

		/// <summary>Removes the item at the specified index.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		/// <exception cref="T:System.NotSupportedException">You are removing a newly added item and <see cref="P:System.ComponentModel.IBindingList.AllowRemove" /> is set to <see langword="false" />.</exception>
		// Token: 0x06003187 RID: 12679 RVA: 0x000DF658 File Offset: 0x000DD858
		protected override void RemoveItem(int index)
		{
			if (!this.allowRemove && (this.addNewPos < 0 || this.addNewPos != index))
			{
				throw new NotSupportedException();
			}
			this.EndNew(this.addNewPos);
			if (this.raiseItemChangedEvents)
			{
				this.UnhookPropertyChanged(base[index]);
			}
			base.RemoveItem(index);
			this.FireListChanged(ListChangedType.ItemDeleted, index);
		}

		/// <summary>Replaces the item at the specified index with the specified item.</summary>
		/// <param name="index">The zero-based index of the item to replace.</param>
		/// <param name="item">The new value for the item at the specified index. The value can be <see langword="null" /> for reference types.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.</exception>
		// Token: 0x06003188 RID: 12680 RVA: 0x000DF6B5 File Offset: 0x000DD8B5
		protected override void SetItem(int index, T item)
		{
			if (this.raiseItemChangedEvents)
			{
				this.UnhookPropertyChanged(base[index]);
			}
			base.SetItem(index, item);
			if (this.raiseItemChangedEvents)
			{
				this.HookPropertyChanged(item);
			}
			this.FireListChanged(ListChangedType.ItemChanged, index);
		}

		/// <summary>Discards a pending new item.</summary>
		/// <param name="itemIndex">The index of the of the new item to be added</param>
		// Token: 0x06003189 RID: 12681 RVA: 0x000DF6EB File Offset: 0x000DD8EB
		public virtual void CancelNew(int itemIndex)
		{
			if (this.addNewPos >= 0 && this.addNewPos == itemIndex)
			{
				this.RemoveItem(this.addNewPos);
				this.addNewPos = -1;
			}
		}

		/// <summary>Commits a pending new item to the collection.</summary>
		/// <param name="itemIndex">The index of the new item to be added.</param>
		// Token: 0x0600318A RID: 12682 RVA: 0x000DF712 File Offset: 0x000DD912
		public virtual void EndNew(int itemIndex)
		{
			if (this.addNewPos >= 0 && this.addNewPos == itemIndex)
			{
				this.addNewPos = -1;
			}
		}

		/// <summary>Adds a new item to the collection.</summary>
		/// <returns>The item added to the list.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.BindingSource.AllowNew" /> property is set to <see langword="false" />.  
		///  -or-  
		///  A public default constructor could not be found for the current item type.</exception>
		// Token: 0x0600318B RID: 12683 RVA: 0x000DF72D File Offset: 0x000DD92D
		public T AddNew()
		{
			return (T)((object)((IBindingList)this).AddNew());
		}

		/// <summary>Adds a new item to the list. For more information, see <see cref="M:System.ComponentModel.IBindingList.AddNew" />.</summary>
		/// <returns>The item added to the list.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x0600318C RID: 12684 RVA: 0x000DF73C File Offset: 0x000DD93C
		object IBindingList.AddNew()
		{
			object obj = this.AddNewCore();
			this.addNewPos = ((obj != null) ? base.IndexOf((T)((object)obj)) : (-1));
			return obj;
		}

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x0600318D RID: 12685 RVA: 0x000DF769 File Offset: 0x000DD969
		private bool AddingNewHandled
		{
			get
			{
				return this.onAddingNew != null && this.onAddingNew.GetInvocationList().Length != 0;
			}
		}

		/// <summary>Adds a new item to the end of the collection.</summary>
		/// <returns>The item that was added to the collection.</returns>
		/// <exception cref="T:System.InvalidCastException">The new item is not the same type as the objects contained in the <see cref="T:System.ComponentModel.BindingList`1" />.</exception>
		// Token: 0x0600318E RID: 12686 RVA: 0x000DF784 File Offset: 0x000DD984
		protected virtual object AddNewCore()
		{
			object obj = this.FireAddingNew();
			if (obj == null)
			{
				Type typeFromHandle = typeof(T);
				obj = SecurityUtils.SecureCreateInstance(typeFromHandle);
			}
			base.Add((T)((object)obj));
			return obj;
		}

		/// <summary>Gets or sets a value indicating whether you can add items to the list using the <see cref="M:System.ComponentModel.BindingList`1.AddNew" /> method.</summary>
		/// <returns>
		///   <see langword="true" /> if you can add items to the list with the <see cref="M:System.ComponentModel.BindingList`1.AddNew" /> method; otherwise, <see langword="false" />. The default depends on the underlying type contained in the list.</returns>
		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x0600318F RID: 12687 RVA: 0x000DF7BA File Offset: 0x000DD9BA
		// (set) Token: 0x06003190 RID: 12688 RVA: 0x000DF7DC File Offset: 0x000DD9DC
		public bool AllowNew
		{
			get
			{
				if (this.userSetAllowNew || this.allowNew)
				{
					return this.allowNew;
				}
				return this.AddingNewHandled;
			}
			set
			{
				bool flag = this.AllowNew;
				this.userSetAllowNew = true;
				this.allowNew = value;
				if (flag != value)
				{
					this.FireListChanged(ListChangedType.Reset, -1);
				}
			}
		}

		/// <summary>Gets a value indicating whether new items can be added to the list using the <see cref="M:System.ComponentModel.BindingList`1.AddNew" /> method.</summary>
		/// <returns>
		///   <see langword="true" /> if you can add items to the list with the <see cref="M:System.ComponentModel.BindingList`1.AddNew" /> method; otherwise, <see langword="false" />. The default depends on the underlying type contained in the list.</returns>
		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x06003191 RID: 12689 RVA: 0x000DF80A File Offset: 0x000DDA0A
		bool IBindingList.AllowNew
		{
			get
			{
				return this.AllowNew;
			}
		}

		/// <summary>Gets or sets a value indicating whether items in the list can be edited.</summary>
		/// <returns>
		///   <see langword="true" /> if list items can be edited; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x06003192 RID: 12690 RVA: 0x000DF812 File Offset: 0x000DDA12
		// (set) Token: 0x06003193 RID: 12691 RVA: 0x000DF81A File Offset: 0x000DDA1A
		public bool AllowEdit
		{
			get
			{
				return this.allowEdit;
			}
			set
			{
				if (this.allowEdit != value)
				{
					this.allowEdit = value;
					this.FireListChanged(ListChangedType.Reset, -1);
				}
			}
		}

		/// <summary>Gets a value indicating whether items in the list can be edited.</summary>
		/// <returns>
		///   <see langword="true" /> if list items can be edited; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x06003194 RID: 12692 RVA: 0x000DF834 File Offset: 0x000DDA34
		bool IBindingList.AllowEdit
		{
			get
			{
				return this.AllowEdit;
			}
		}

		/// <summary>Gets or sets a value indicating whether you can remove items from the collection.</summary>
		/// <returns>
		///   <see langword="true" /> if you can remove items from the list with the <see cref="M:System.ComponentModel.BindingList`1.RemoveItem(System.Int32)" /> method otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x06003195 RID: 12693 RVA: 0x000DF83C File Offset: 0x000DDA3C
		// (set) Token: 0x06003196 RID: 12694 RVA: 0x000DF844 File Offset: 0x000DDA44
		public bool AllowRemove
		{
			get
			{
				return this.allowRemove;
			}
			set
			{
				if (this.allowRemove != value)
				{
					this.allowRemove = value;
					this.FireListChanged(ListChangedType.Reset, -1);
				}
			}
		}

		/// <summary>Gets a value indicating whether items can be removed from the list.</summary>
		/// <returns>
		///   <see langword="true" /> if you can remove items from the list with the <see cref="M:System.ComponentModel.BindingList`1.RemoveItem(System.Int32)" /> method; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x06003197 RID: 12695 RVA: 0x000DF85E File Offset: 0x000DDA5E
		bool IBindingList.AllowRemove
		{
			get
			{
				return this.AllowRemove;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsChangeNotification" />.</summary>
		/// <returns>
		///   <see langword="true" /> if a <see cref="E:System.ComponentModel.IBindingList.ListChanged" /> event is raised when the list changes or when an item changes; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x06003198 RID: 12696 RVA: 0x000DF866 File Offset: 0x000DDA66
		bool IBindingList.SupportsChangeNotification
		{
			get
			{
				return this.SupportsChangeNotificationCore;
			}
		}

		/// <summary>Gets a value indicating whether <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> events are enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> events are supported; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x06003199 RID: 12697 RVA: 0x000DF86E File Offset: 0x000DDA6E
		protected virtual bool SupportsChangeNotificationCore
		{
			get
			{
				return true;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsSearching" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the list supports searching using the <see cref="M:System.ComponentModel.IBindingList.Find(System.ComponentModel.PropertyDescriptor,System.Object)" /> method; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x0600319A RID: 12698 RVA: 0x000DF871 File Offset: 0x000DDA71
		bool IBindingList.SupportsSearching
		{
			get
			{
				return this.SupportsSearchingCore;
			}
		}

		/// <summary>Gets a value indicating whether the list supports searching.</summary>
		/// <returns>
		///   <see langword="true" /> if the list supports searching; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x0600319B RID: 12699 RVA: 0x000DF879 File Offset: 0x000DDA79
		protected virtual bool SupportsSearchingCore
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the list supports sorting; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x0600319C RID: 12700 RVA: 0x000DF87C File Offset: 0x000DDA7C
		bool IBindingList.SupportsSorting
		{
			get
			{
				return this.SupportsSortingCore;
			}
		}

		/// <summary>Gets a value indicating whether the list supports sorting.</summary>
		/// <returns>
		///   <see langword="true" /> if the list supports sorting; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x0600319D RID: 12701 RVA: 0x000DF884 File Offset: 0x000DDA84
		protected virtual bool SupportsSortingCore
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.IsSorted" />.</summary>
		/// <returns>
		///   <see langword="true" /> if <see cref="M:System.ComponentModel.IBindingListView.ApplySort(System.ComponentModel.ListSortDescriptionCollection)" /> has been called and <see cref="M:System.ComponentModel.IBindingList.RemoveSort" /> has not been called; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x0600319E RID: 12702 RVA: 0x000DF887 File Offset: 0x000DDA87
		bool IBindingList.IsSorted
		{
			get
			{
				return this.IsSortedCore;
			}
		}

		/// <summary>Gets a value indicating whether the list is sorted.</summary>
		/// <returns>
		///   <see langword="true" /> if the list is sorted; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x0600319F RID: 12703 RVA: 0x000DF88F File Offset: 0x000DDA8F
		protected virtual bool IsSortedCore
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SortProperty" />.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> that is being used for sorting.</returns>
		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x060031A0 RID: 12704 RVA: 0x000DF892 File Offset: 0x000DDA92
		PropertyDescriptor IBindingList.SortProperty
		{
			get
			{
				return this.SortPropertyCore;
			}
		}

		/// <summary>Gets the property descriptor that is used for sorting the list if sorting is implemented in a derived class; otherwise, returns <see langword="null" />.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> used for sorting the list.</returns>
		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x060031A1 RID: 12705 RVA: 0x000DF89A File Offset: 0x000DDA9A
		protected virtual PropertyDescriptor SortPropertyCore
		{
			get
			{
				return null;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SortDirection" />.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values.</returns>
		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x060031A2 RID: 12706 RVA: 0x000DF89D File Offset: 0x000DDA9D
		ListSortDirection IBindingList.SortDirection
		{
			get
			{
				return this.SortDirectionCore;
			}
		}

		/// <summary>Gets the direction the list is sorted.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values. The default is <see cref="F:System.ComponentModel.ListSortDirection.Ascending" />.</returns>
		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x060031A3 RID: 12707 RVA: 0x000DF8A5 File Offset: 0x000DDAA5
		protected virtual ListSortDirection SortDirectionCore
		{
			get
			{
				return ListSortDirection.Ascending;
			}
		}

		/// <summary>Sorts the list based on a <see cref="T:System.ComponentModel.PropertyDescriptor" /> and a <see cref="T:System.ComponentModel.ListSortDirection" />. For a complete description of this member, see <see cref="M:System.ComponentModel.IBindingList.ApplySort(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)" />.</summary>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to sort by.</param>
		/// <param name="direction">One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values.</param>
		// Token: 0x060031A4 RID: 12708 RVA: 0x000DF8A8 File Offset: 0x000DDAA8
		void IBindingList.ApplySort(PropertyDescriptor prop, ListSortDirection direction)
		{
			this.ApplySortCore(prop, direction);
		}

		/// <summary>Sorts the items if overridden in a derived class; otherwise, throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="prop">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that specifies the property to sort on.</param>
		/// <param name="direction">One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values.</param>
		/// <exception cref="T:System.NotSupportedException">Method is not overridden in a derived class.</exception>
		// Token: 0x060031A5 RID: 12709 RVA: 0x000DF8B2 File Offset: 0x000DDAB2
		protected virtual void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
		{
			throw new NotSupportedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.RemoveSort" /></summary>
		// Token: 0x060031A6 RID: 12710 RVA: 0x000DF8B9 File Offset: 0x000DDAB9
		void IBindingList.RemoveSort()
		{
			this.RemoveSortCore();
		}

		/// <summary>Removes any sort applied with <see cref="M:System.ComponentModel.BindingList`1.ApplySortCore(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)" /> if sorting is implemented in a derived class; otherwise, raises <see cref="T:System.NotSupportedException" />.</summary>
		/// <exception cref="T:System.NotSupportedException">Method is not overridden in a derived class.</exception>
		// Token: 0x060031A7 RID: 12711 RVA: 0x000DF8C1 File Offset: 0x000DDAC1
		protected virtual void RemoveSortCore()
		{
			throw new NotSupportedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.Find(System.ComponentModel.PropertyDescriptor,System.Object)" />.</summary>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to search on.</param>
		/// <param name="key">The value of the <paramref name="prop" /> parameter to search for.</param>
		/// <returns>The index of the row that has the given <see cref="T:System.ComponentModel.PropertyDescriptor" />.</returns>
		// Token: 0x060031A8 RID: 12712 RVA: 0x000DF8C8 File Offset: 0x000DDAC8
		int IBindingList.Find(PropertyDescriptor prop, object key)
		{
			return this.FindCore(prop, key);
		}

		/// <summary>Searches for the index of the item that has the specified property descriptor with the specified value, if searching is implemented in a derived class; otherwise, a <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to search for.</param>
		/// <param name="key">The value of <paramref name="prop" /> to match.</param>
		/// <returns>The zero-based index of the item that matches the property descriptor and contains the specified value.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="M:System.ComponentModel.BindingList`1.FindCore(System.ComponentModel.PropertyDescriptor,System.Object)" /> is not overridden in a derived class.</exception>
		// Token: 0x060031A9 RID: 12713 RVA: 0x000DF8D2 File Offset: 0x000DDAD2
		protected virtual int FindCore(PropertyDescriptor prop, object key)
		{
			throw new NotSupportedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.AddIndex(System.ComponentModel.PropertyDescriptor)" />.</summary>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to add as a search criteria.</param>
		// Token: 0x060031AA RID: 12714 RVA: 0x000DF8D9 File Offset: 0x000DDAD9
		void IBindingList.AddIndex(PropertyDescriptor prop)
		{
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.RemoveIndex(System.ComponentModel.PropertyDescriptor)" />.</summary>
		/// <param name="prop">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> to remove from the indexes used for searching.</param>
		// Token: 0x060031AB RID: 12715 RVA: 0x000DF8DB File Offset: 0x000DDADB
		void IBindingList.RemoveIndex(PropertyDescriptor prop)
		{
		}

		// Token: 0x060031AC RID: 12716 RVA: 0x000DF8E0 File Offset: 0x000DDAE0
		private void HookPropertyChanged(T item)
		{
			INotifyPropertyChanged notifyPropertyChanged = item as INotifyPropertyChanged;
			if (notifyPropertyChanged != null)
			{
				if (this.propertyChangedEventHandler == null)
				{
					this.propertyChangedEventHandler = new PropertyChangedEventHandler(this.Child_PropertyChanged);
				}
				notifyPropertyChanged.PropertyChanged += this.propertyChangedEventHandler;
			}
		}

		// Token: 0x060031AD RID: 12717 RVA: 0x000DF924 File Offset: 0x000DDB24
		private void UnhookPropertyChanged(T item)
		{
			INotifyPropertyChanged notifyPropertyChanged = item as INotifyPropertyChanged;
			if (notifyPropertyChanged != null && this.propertyChangedEventHandler != null)
			{
				notifyPropertyChanged.PropertyChanged -= this.propertyChangedEventHandler;
			}
		}

		// Token: 0x060031AE RID: 12718 RVA: 0x000DF954 File Offset: 0x000DDB54
		private void Child_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (this.RaiseListChangedEvents)
			{
				if (sender == null || e == null || string.IsNullOrEmpty(e.PropertyName))
				{
					this.ResetBindings();
					return;
				}
				T t;
				try
				{
					t = (T)((object)sender);
				}
				catch (InvalidCastException)
				{
					this.ResetBindings();
					return;
				}
				int num = this.lastChangeIndex;
				if (num >= 0 && num < base.Count)
				{
					T t2 = base[num];
					if (t2.Equals(t))
					{
						goto IL_7B;
					}
				}
				num = base.IndexOf(t);
				this.lastChangeIndex = num;
				IL_7B:
				if (num == -1)
				{
					this.UnhookPropertyChanged(t);
					this.ResetBindings();
					return;
				}
				if (this.itemTypeProperties == null)
				{
					this.itemTypeProperties = TypeDescriptor.GetProperties(typeof(T));
				}
				PropertyDescriptor propertyDescriptor = this.itemTypeProperties.Find(e.PropertyName, true);
				ListChangedEventArgs listChangedEventArgs = new ListChangedEventArgs(ListChangedType.ItemChanged, num, propertyDescriptor);
				this.OnListChanged(listChangedEventArgs);
			}
		}

		/// <summary>Gets a value indicating whether item property value changes raise <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> events of type <see cref="F:System.ComponentModel.ListChangedType.ItemChanged" />. This member cannot be overridden in a derived class.</summary>
		/// <returns>
		///   <see langword="true" /> if the list type implements <see cref="T:System.ComponentModel.INotifyPropertyChanged" />, otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x060031AF RID: 12719 RVA: 0x000DFA40 File Offset: 0x000DDC40
		bool IRaiseItemChangedEvents.RaisesItemChangedEvents
		{
			get
			{
				return this.raiseItemChangedEvents;
			}
		}

		// Token: 0x04002919 RID: 10521
		private int addNewPos = -1;

		// Token: 0x0400291A RID: 10522
		private bool raiseListChangedEvents = true;

		// Token: 0x0400291B RID: 10523
		private bool raiseItemChangedEvents;

		// Token: 0x0400291C RID: 10524
		[NonSerialized]
		private PropertyDescriptorCollection itemTypeProperties;

		// Token: 0x0400291D RID: 10525
		[NonSerialized]
		private PropertyChangedEventHandler propertyChangedEventHandler;

		// Token: 0x0400291E RID: 10526
		[NonSerialized]
		private AddingNewEventHandler onAddingNew;

		// Token: 0x0400291F RID: 10527
		[NonSerialized]
		private ListChangedEventHandler onListChanged;

		// Token: 0x04002920 RID: 10528
		[NonSerialized]
		private int lastChangeIndex = -1;

		// Token: 0x04002921 RID: 10529
		private bool allowNew = true;

		// Token: 0x04002922 RID: 10530
		private bool allowEdit = true;

		// Token: 0x04002923 RID: 10531
		private bool allowRemove = true;

		// Token: 0x04002924 RID: 10532
		private bool userSetAllowNew;
	}
}
