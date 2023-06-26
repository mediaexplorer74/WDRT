using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Manages a list of <see cref="T:System.Windows.Forms.Binding" /> objects.</summary>
	// Token: 0x02000174 RID: 372
	public class CurrencyManager : BindingManagerBase
	{
		/// <summary>Occurs when the current item has been altered.</summary>
		// Token: 0x140000CA RID: 202
		// (add) Token: 0x0600139F RID: 5023 RVA: 0x00041541 File Offset: 0x0003F741
		// (remove) Token: 0x060013A0 RID: 5024 RVA: 0x0004155A File Offset: 0x0003F75A
		[SRCategory("CatData")]
		public event ItemChangedEventHandler ItemChanged
		{
			add
			{
				this.onItemChanged = (ItemChangedEventHandler)Delegate.Combine(this.onItemChanged, value);
			}
			remove
			{
				this.onItemChanged = (ItemChangedEventHandler)Delegate.Remove(this.onItemChanged, value);
			}
		}

		/// <summary>Occurs when the list changes or an item in the list changes.</summary>
		// Token: 0x140000CB RID: 203
		// (add) Token: 0x060013A1 RID: 5025 RVA: 0x00041573 File Offset: 0x0003F773
		// (remove) Token: 0x060013A2 RID: 5026 RVA: 0x0004158C File Offset: 0x0003F78C
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

		// Token: 0x060013A3 RID: 5027 RVA: 0x000415A5 File Offset: 0x0003F7A5
		internal CurrencyManager(object dataSource)
		{
			this.SetDataSource(dataSource);
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x060013A4 RID: 5028 RVA: 0x000415D8 File Offset: 0x0003F7D8
		internal bool AllowAdd
		{
			get
			{
				if (this.list is IBindingList)
				{
					return ((IBindingList)this.list).AllowNew;
				}
				return this.list != null && !this.list.IsReadOnly && !this.list.IsFixedSize;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x060013A5 RID: 5029 RVA: 0x0004162A File Offset: 0x0003F82A
		internal bool AllowEdit
		{
			get
			{
				if (this.list is IBindingList)
				{
					return ((IBindingList)this.list).AllowEdit;
				}
				return this.list != null && !this.list.IsReadOnly;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x060013A6 RID: 5030 RVA: 0x00041664 File Offset: 0x0003F864
		internal bool AllowRemove
		{
			get
			{
				if (this.list is IBindingList)
				{
					return ((IBindingList)this.list).AllowRemove;
				}
				return this.list != null && !this.list.IsReadOnly && !this.list.IsFixedSize;
			}
		}

		/// <summary>Gets the number of items in the list.</summary>
		/// <returns>The number of items in the list.</returns>
		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x060013A7 RID: 5031 RVA: 0x000416B6 File Offset: 0x0003F8B6
		public override int Count
		{
			get
			{
				if (this.list == null)
				{
					return 0;
				}
				return this.list.Count;
			}
		}

		/// <summary>Gets the current item in the list.</summary>
		/// <returns>A list item of type <see cref="T:System.Object" />.</returns>
		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x060013A8 RID: 5032 RVA: 0x000416CD File Offset: 0x0003F8CD
		public override object Current
		{
			get
			{
				return this[this.Position];
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x060013A9 RID: 5033 RVA: 0x000416DB File Offset: 0x0003F8DB
		internal override Type BindType
		{
			get
			{
				return ListBindingHelper.GetListItemType(this.List);
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x000416E8 File Offset: 0x0003F8E8
		internal override object DataSource
		{
			get
			{
				return this.dataSource;
			}
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x000416F0 File Offset: 0x0003F8F0
		internal override void SetDataSource(object dataSource)
		{
			if (this.dataSource == dataSource)
			{
				return;
			}
			this.Release();
			this.dataSource = dataSource;
			this.list = null;
			this.finalType = null;
			object obj = dataSource;
			if (obj is Array)
			{
				this.finalType = obj.GetType();
				obj = (Array)obj;
			}
			if (obj is IListSource)
			{
				obj = ((IListSource)obj).GetList();
			}
			if (obj is IList)
			{
				if (this.finalType == null)
				{
					this.finalType = obj.GetType();
				}
				this.list = (IList)obj;
				this.WireEvents(this.list);
				if (this.list.Count > 0)
				{
					this.listposition = 0;
				}
				else
				{
					this.listposition = -1;
				}
				this.OnItemChanged(this.resetEvent);
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1, -1));
				this.UpdateIsBinding();
				return;
			}
			if (obj == null)
			{
				throw new ArgumentNullException("dataSource");
			}
			throw new ArgumentException(SR.GetString("ListManagerSetDataSource", new object[] { obj.GetType().FullName }), "dataSource");
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x060013AC RID: 5036 RVA: 0x00041805 File Offset: 0x0003FA05
		internal override bool IsBinding
		{
			get
			{
				return this.bound;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x060013AD RID: 5037 RVA: 0x0004180D File Offset: 0x0003FA0D
		internal bool ShouldBind
		{
			get
			{
				return this.shouldBind;
			}
		}

		/// <summary>Gets the list for this <see cref="T:System.Windows.Forms.CurrencyManager" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> that contains the list.</returns>
		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x060013AE RID: 5038 RVA: 0x00041815 File Offset: 0x0003FA15
		public IList List
		{
			get
			{
				return this.list;
			}
		}

		/// <summary>Gets or sets the position you are at within the list.</summary>
		/// <returns>A number between 0 and <see cref="P:System.Windows.Forms.CurrencyManager.Count" /> minus 1.</returns>
		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x060013AF RID: 5039 RVA: 0x0004181D File Offset: 0x0003FA1D
		// (set) Token: 0x060013B0 RID: 5040 RVA: 0x00041828 File Offset: 0x0003FA28
		public override int Position
		{
			get
			{
				return this.listposition;
			}
			set
			{
				if (this.listposition == -1)
				{
					return;
				}
				if (value < 0)
				{
					value = 0;
				}
				int count = this.list.Count;
				if (value >= count)
				{
					value = count - 1;
				}
				this.ChangeRecordState(value, this.listposition != value, true, true, false);
			}
		}

		// Token: 0x17000477 RID: 1143
		internal object this[int index]
		{
			get
			{
				if (index < 0 || index >= this.list.Count)
				{
					throw new IndexOutOfRangeException(SR.GetString("ListManagerNoValue", new object[] { index.ToString(CultureInfo.CurrentCulture) }));
				}
				return this.list[index];
			}
			set
			{
				if (index < 0 || index >= this.list.Count)
				{
					throw new IndexOutOfRangeException(SR.GetString("ListManagerNoValue", new object[] { index.ToString(CultureInfo.CurrentCulture) }));
				}
				this.list[index] = value;
			}
		}

		/// <summary>Adds a new item to the underlying list.</summary>
		/// <exception cref="T:System.NotSupportedException">The underlying data source does not implement <see cref="T:System.ComponentModel.IBindingList" />, or the data source has thrown an exception because the user has attempted to add a row to a read-only or fixed-size <see cref="T:System.Data.DataView" />.</exception>
		// Token: 0x060013B3 RID: 5043 RVA: 0x00041918 File Offset: 0x0003FB18
		public override void AddNew()
		{
			IBindingList bindingList = this.list as IBindingList;
			if (bindingList != null)
			{
				bindingList.AddNew();
				this.ChangeRecordState(this.list.Count - 1, this.Position != this.list.Count - 1, this.Position != this.list.Count - 1, true, true);
				return;
			}
			throw new NotSupportedException(SR.GetString("CurrencyManagerCantAddNew"));
		}

		/// <summary>Cancels the current edit operation.</summary>
		// Token: 0x060013B4 RID: 5044 RVA: 0x00041994 File Offset: 0x0003FB94
		public override void CancelCurrentEdit()
		{
			if (this.Count > 0)
			{
				object obj = ((this.Position >= 0 && this.Position < this.list.Count) ? this.list[this.Position] : null);
				IEditableObject editableObject = obj as IEditableObject;
				if (editableObject != null)
				{
					editableObject.CancelEdit();
				}
				ICancelAddNew cancelAddNew = this.list as ICancelAddNew;
				if (cancelAddNew != null)
				{
					cancelAddNew.CancelNew(this.Position);
				}
				this.OnItemChanged(new ItemChangedEventArgs(this.Position));
				if (this.Position != -1)
				{
					this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, this.Position));
				}
			}
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x00041A38 File Offset: 0x0003FC38
		private void ChangeRecordState(int newPosition, bool validating, bool endCurrentEdit, bool firePositionChange, bool pullData)
		{
			if (newPosition == -1 && this.list.Count == 0)
			{
				if (this.listposition != -1)
				{
					this.listposition = -1;
					this.OnPositionChanged(EventArgs.Empty);
				}
				return;
			}
			if ((newPosition < 0 || newPosition >= this.Count) && this.IsBinding)
			{
				throw new IndexOutOfRangeException(SR.GetString("ListManagerBadPosition"));
			}
			int num = this.listposition;
			if (endCurrentEdit)
			{
				this.inChangeRecordState = true;
				try
				{
					this.EndCurrentEdit();
				}
				finally
				{
					this.inChangeRecordState = false;
				}
			}
			if (validating && pullData)
			{
				this.CurrencyManager_PullData();
			}
			this.listposition = Math.Min(newPosition, this.Count - 1);
			if (validating)
			{
				this.OnCurrentChanged(EventArgs.Empty);
			}
			bool flag = num != this.listposition;
			if (flag && firePositionChange)
			{
				this.OnPositionChanged(EventArgs.Empty);
			}
		}

		/// <summary>Throws an exception if there is no list, or the list is empty.</summary>
		/// <exception cref="T:System.Exception">There is no list, or the list is empty.</exception>
		// Token: 0x060013B6 RID: 5046 RVA: 0x00041B18 File Offset: 0x0003FD18
		protected void CheckEmpty()
		{
			if (this.dataSource == null || this.list == null || this.list.Count == 0)
			{
				throw new InvalidOperationException(SR.GetString("ListManagerEmptyList"));
			}
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x00041B48 File Offset: 0x0003FD48
		private bool CurrencyManager_PushData()
		{
			if (this.pullingData)
			{
				return false;
			}
			int num = this.listposition;
			if (this.lastGoodKnownRow == -1)
			{
				try
				{
					base.PushData();
				}
				catch (Exception ex)
				{
					base.OnDataError(ex);
					this.FindGoodRow();
				}
				this.lastGoodKnownRow = this.listposition;
			}
			else
			{
				try
				{
					base.PushData();
				}
				catch (Exception ex2)
				{
					base.OnDataError(ex2);
					this.listposition = this.lastGoodKnownRow;
					base.PushData();
				}
				this.lastGoodKnownRow = this.listposition;
			}
			return num != this.listposition;
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x00041BF0 File Offset: 0x0003FDF0
		private bool CurrencyManager_PullData()
		{
			bool flag = true;
			this.pullingData = true;
			try
			{
				base.PullData(out flag);
			}
			finally
			{
				this.pullingData = false;
			}
			return flag;
		}

		/// <summary>Removes the item at the specified index.</summary>
		/// <param name="index">The index of the item to remove from the list.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">There is no row at the specified <paramref name="index" />.</exception>
		// Token: 0x060013B9 RID: 5049 RVA: 0x00041C2C File Offset: 0x0003FE2C
		public override void RemoveAt(int index)
		{
			this.list.RemoveAt(index);
		}

		/// <summary>Ends the current edit operation.</summary>
		// Token: 0x060013BA RID: 5050 RVA: 0x00041C3C File Offset: 0x0003FE3C
		public override void EndCurrentEdit()
		{
			if (this.Count > 0)
			{
				bool flag = this.CurrencyManager_PullData();
				if (flag)
				{
					object obj = ((this.Position >= 0 && this.Position < this.list.Count) ? this.list[this.Position] : null);
					IEditableObject editableObject = obj as IEditableObject;
					if (editableObject != null)
					{
						editableObject.EndEdit();
					}
					ICancelAddNew cancelAddNew = this.list as ICancelAddNew;
					if (cancelAddNew != null)
					{
						cancelAddNew.EndNew(this.Position);
					}
				}
			}
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x00041CB8 File Offset: 0x0003FEB8
		private void FindGoodRow()
		{
			int count = this.list.Count;
			int i = 0;
			while (i < count)
			{
				this.listposition = i;
				try
				{
					base.PushData();
				}
				catch (Exception ex)
				{
					base.OnDataError(ex);
					goto IL_31;
				}
				goto IL_29;
				IL_31:
				i++;
				continue;
				IL_29:
				this.listposition = i;
				return;
			}
			this.SuspendBinding();
			throw new InvalidOperationException(SR.GetString("DataBindingPushDataException"));
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x00041D24 File Offset: 0x0003FF24
		internal void SetSort(PropertyDescriptor property, ListSortDirection sortDirection)
		{
			if (this.list is IBindingList && ((IBindingList)this.list).SupportsSorting)
			{
				((IBindingList)this.list).ApplySort(property, sortDirection);
			}
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x00041D57 File Offset: 0x0003FF57
		internal PropertyDescriptor GetSortProperty()
		{
			if (this.list is IBindingList && ((IBindingList)this.list).SupportsSorting)
			{
				return ((IBindingList)this.list).SortProperty;
			}
			return null;
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x00041D8A File Offset: 0x0003FF8A
		internal ListSortDirection GetSortDirection()
		{
			if (this.list is IBindingList && ((IBindingList)this.list).SupportsSorting)
			{
				return ((IBindingList)this.list).SortDirection;
			}
			return ListSortDirection.Ascending;
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x00041DC0 File Offset: 0x0003FFC0
		internal int Find(PropertyDescriptor property, object key, bool keepIndex)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (property != null && this.list is IBindingList && ((IBindingList)this.list).SupportsSearching)
			{
				return ((IBindingList)this.list).Find(property, key);
			}
			if (property != null)
			{
				for (int i = 0; i < this.list.Count; i++)
				{
					object value = property.GetValue(this.list[i]);
					if (key.Equals(value))
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x00041E48 File Offset: 0x00040048
		internal override string GetListName()
		{
			if (this.list is ITypedList)
			{
				return ((ITypedList)this.list).GetListName(null);
			}
			return this.finalType.Name;
		}

		/// <summary>Gets the name of the list supplying the data for the binding using the specified set of bound properties.</summary>
		/// <param name="listAccessors">An <see cref="T:System.Collections.ArrayList" /> of properties to be found in the data source.</param>
		/// <returns>If successful, a <see cref="T:System.String" /> containing name of the list supplying the data for the binding; otherwise, an <see cref="F:System.String.Empty" /> string.</returns>
		// Token: 0x060013C1 RID: 5057 RVA: 0x00041E74 File Offset: 0x00040074
		protected internal override string GetListName(ArrayList listAccessors)
		{
			if (this.list is ITypedList)
			{
				PropertyDescriptor[] array = new PropertyDescriptor[listAccessors.Count];
				listAccessors.CopyTo(array, 0);
				return ((ITypedList)this.list).GetListName(array);
			}
			return "";
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x00041EB9 File Offset: 0x000400B9
		internal override PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
		{
			return ListBindingHelper.GetListItemProperties(this.list, listAccessors);
		}

		/// <summary>Gets the property descriptor collection for the underlying list.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> for the list.</returns>
		// Token: 0x060013C3 RID: 5059 RVA: 0x0001FC0F File Offset: 0x0001DE0F
		public override PropertyDescriptorCollection GetItemProperties()
		{
			return this.GetItemProperties(null);
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x00041EC8 File Offset: 0x000400C8
		private void List_ListChanged(object sender, ListChangedEventArgs e)
		{
			ListChangedEventArgs listChangedEventArgs;
			if (e.ListChangedType == ListChangedType.ItemMoved && e.OldIndex < 0)
			{
				listChangedEventArgs = new ListChangedEventArgs(ListChangedType.ItemAdded, e.NewIndex, e.OldIndex);
			}
			else if (e.ListChangedType == ListChangedType.ItemMoved && e.NewIndex < 0)
			{
				listChangedEventArgs = new ListChangedEventArgs(ListChangedType.ItemDeleted, e.OldIndex, e.NewIndex);
			}
			else
			{
				listChangedEventArgs = e;
			}
			int num = this.listposition;
			this.UpdateLastGoodKnownRow(listChangedEventArgs);
			this.UpdateIsBinding();
			if (this.list.Count == 0)
			{
				this.listposition = -1;
				if (num != -1)
				{
					this.OnPositionChanged(EventArgs.Empty);
					this.OnCurrentChanged(EventArgs.Empty);
				}
				if (listChangedEventArgs.ListChangedType == ListChangedType.Reset && e.NewIndex == -1)
				{
					this.OnItemChanged(this.resetEvent);
				}
				if (listChangedEventArgs.ListChangedType == ListChangedType.ItemDeleted)
				{
					this.OnItemChanged(this.resetEvent);
				}
				if (e.ListChangedType == ListChangedType.PropertyDescriptorAdded || e.ListChangedType == ListChangedType.PropertyDescriptorDeleted || e.ListChangedType == ListChangedType.PropertyDescriptorChanged)
				{
					this.OnMetaDataChanged(EventArgs.Empty);
				}
				this.OnListChanged(listChangedEventArgs);
				return;
			}
			this.suspendPushDataInCurrentChanged = true;
			try
			{
				switch (listChangedEventArgs.ListChangedType)
				{
				case ListChangedType.Reset:
					if (this.listposition == -1 && this.list.Count > 0)
					{
						this.ChangeRecordState(0, true, false, true, false);
					}
					else
					{
						this.ChangeRecordState(Math.Min(this.listposition, this.list.Count - 1), true, false, true, false);
					}
					this.UpdateIsBinding(false);
					this.OnItemChanged(this.resetEvent);
					break;
				case ListChangedType.ItemAdded:
					if (listChangedEventArgs.NewIndex <= this.listposition && this.listposition < this.list.Count - 1)
					{
						this.ChangeRecordState(this.listposition + 1, true, true, this.listposition != this.list.Count - 2, false);
						this.UpdateIsBinding();
						this.OnItemChanged(this.resetEvent);
						if (this.listposition == this.list.Count - 1)
						{
							this.OnPositionChanged(EventArgs.Empty);
						}
					}
					else
					{
						if (listChangedEventArgs.NewIndex == this.listposition && this.listposition == this.list.Count - 1 && this.listposition != -1)
						{
							this.OnCurrentItemChanged(EventArgs.Empty);
						}
						if (this.listposition == -1)
						{
							this.ChangeRecordState(0, false, false, true, false);
						}
						this.UpdateIsBinding();
						this.OnItemChanged(this.resetEvent);
					}
					break;
				case ListChangedType.ItemDeleted:
					if (listChangedEventArgs.NewIndex == this.listposition)
					{
						this.ChangeRecordState(Math.Min(this.listposition, this.Count - 1), true, false, true, false);
						this.OnItemChanged(this.resetEvent);
					}
					else if (listChangedEventArgs.NewIndex < this.listposition)
					{
						this.ChangeRecordState(this.listposition - 1, true, false, true, false);
						this.OnItemChanged(this.resetEvent);
					}
					else
					{
						this.OnItemChanged(this.resetEvent);
					}
					break;
				case ListChangedType.ItemMoved:
					if (listChangedEventArgs.OldIndex == this.listposition)
					{
						this.ChangeRecordState(listChangedEventArgs.NewIndex, true, this.Position > -1 && this.Position < this.list.Count, true, false);
					}
					else if (listChangedEventArgs.NewIndex == this.listposition)
					{
						this.ChangeRecordState(listChangedEventArgs.OldIndex, true, this.Position > -1 && this.Position < this.list.Count, true, false);
					}
					this.OnItemChanged(this.resetEvent);
					break;
				case ListChangedType.ItemChanged:
					if (listChangedEventArgs.NewIndex == this.listposition)
					{
						this.OnCurrentItemChanged(EventArgs.Empty);
					}
					this.OnItemChanged(new ItemChangedEventArgs(listChangedEventArgs.NewIndex));
					break;
				case ListChangedType.PropertyDescriptorAdded:
				case ListChangedType.PropertyDescriptorDeleted:
				case ListChangedType.PropertyDescriptorChanged:
					this.lastGoodKnownRow = -1;
					if (this.listposition == -1 && this.list.Count > 0)
					{
						this.ChangeRecordState(0, true, false, true, false);
					}
					else if (this.listposition > this.list.Count - 1)
					{
						this.ChangeRecordState(this.list.Count - 1, true, false, true, false);
					}
					this.OnMetaDataChanged(EventArgs.Empty);
					break;
				}
				this.OnListChanged(listChangedEventArgs);
			}
			finally
			{
				this.suspendPushDataInCurrentChanged = false;
			}
		}

		/// <summary>Occurs when the metadata of the <see cref="P:System.Windows.Forms.CurrencyManager.List" /> has changed.</summary>
		// Token: 0x140000CC RID: 204
		// (add) Token: 0x060013C5 RID: 5061 RVA: 0x00042310 File Offset: 0x00040510
		// (remove) Token: 0x060013C6 RID: 5062 RVA: 0x00042329 File Offset: 0x00040529
		[SRCategory("CatData")]
		public event EventHandler MetaDataChanged
		{
			add
			{
				this.onMetaDataChangedHandler = (EventHandler)Delegate.Combine(this.onMetaDataChangedHandler, value);
			}
			remove
			{
				this.onMetaDataChangedHandler = (EventHandler)Delegate.Remove(this.onMetaDataChangedHandler, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingManagerBase.CurrentChanged" /> event.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060013C7 RID: 5063 RVA: 0x00042344 File Offset: 0x00040544
		protected internal override void OnCurrentChanged(EventArgs e)
		{
			if (!this.inChangeRecordState)
			{
				int num = this.lastGoodKnownRow;
				bool flag = false;
				if (!this.suspendPushDataInCurrentChanged)
				{
					flag = this.CurrencyManager_PushData();
				}
				if (this.Count > 0)
				{
					object obj = this.list[this.Position];
					if (obj is IEditableObject)
					{
						((IEditableObject)obj).BeginEdit();
					}
				}
				try
				{
					if (!flag || (flag && num != -1))
					{
						if (this.onCurrentChangedHandler != null)
						{
							this.onCurrentChangedHandler(this, e);
						}
						if (this.onCurrentItemChangedHandler != null)
						{
							this.onCurrentItemChangedHandler(this, e);
						}
					}
				}
				catch (Exception ex)
				{
					base.OnDataError(ex);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingManagerBase.CurrentItemChanged" /> event.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060013C8 RID: 5064 RVA: 0x000423F4 File Offset: 0x000405F4
		protected internal override void OnCurrentItemChanged(EventArgs e)
		{
			if (this.onCurrentItemChangedHandler != null)
			{
				this.onCurrentItemChangedHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.CurrencyManager.ItemChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Windows.Forms.ItemChangedEventArgs" /> that contains the event data.</param>
		// Token: 0x060013C9 RID: 5065 RVA: 0x0004240C File Offset: 0x0004060C
		protected virtual void OnItemChanged(ItemChangedEventArgs e)
		{
			bool flag = false;
			if ((e.Index == this.listposition || (e.Index == -1 && this.Position < this.Count)) && !this.inChangeRecordState)
			{
				flag = this.CurrencyManager_PushData();
			}
			try
			{
				if (this.onItemChanged != null)
				{
					this.onItemChanged(this, e);
				}
			}
			catch (Exception ex)
			{
				base.OnDataError(ex);
			}
			if (flag)
			{
				this.OnPositionChanged(EventArgs.Empty);
			}
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x00042490 File Offset: 0x00040690
		private void OnListChanged(ListChangedEventArgs e)
		{
			if (this.onListChanged != null)
			{
				this.onListChanged(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.CurrencyManager.MetaDataChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060013CB RID: 5067 RVA: 0x000424A7 File Offset: 0x000406A7
		protected internal void OnMetaDataChanged(EventArgs e)
		{
			if (this.onMetaDataChangedHandler != null)
			{
				this.onMetaDataChangedHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingManagerBase.PositionChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060013CC RID: 5068 RVA: 0x000424C0 File Offset: 0x000406C0
		protected virtual void OnPositionChanged(EventArgs e)
		{
			try
			{
				if (this.onPositionChangedHandler != null)
				{
					this.onPositionChangedHandler(this, e);
				}
			}
			catch (Exception ex)
			{
				base.OnDataError(ex);
			}
		}

		/// <summary>Forces a repopulation of the data-bound list.</summary>
		// Token: 0x060013CD RID: 5069 RVA: 0x00042500 File Offset: 0x00040700
		public void Refresh()
		{
			if (this.list.Count > 0)
			{
				if (this.listposition >= this.list.Count)
				{
					this.lastGoodKnownRow = -1;
					this.listposition = 0;
				}
			}
			else
			{
				this.listposition = -1;
			}
			this.List_ListChanged(this.list, new ListChangedEventArgs(ListChangedType.Reset, -1));
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x00042558 File Offset: 0x00040758
		internal void Release()
		{
			this.UnwireEvents(this.list);
		}

		/// <summary>Resumes data binding.</summary>
		// Token: 0x060013CF RID: 5071 RVA: 0x00042568 File Offset: 0x00040768
		public override void ResumeBinding()
		{
			this.lastGoodKnownRow = -1;
			try
			{
				if (!this.shouldBind)
				{
					this.shouldBind = true;
					this.listposition = ((this.list != null && this.list.Count != 0) ? 0 : (-1));
					this.UpdateIsBinding();
				}
			}
			catch
			{
				this.shouldBind = false;
				this.UpdateIsBinding();
				throw;
			}
		}

		/// <summary>Suspends data binding to prevents changes from updating the bound data source.</summary>
		// Token: 0x060013D0 RID: 5072 RVA: 0x000425D4 File Offset: 0x000407D4
		public override void SuspendBinding()
		{
			this.lastGoodKnownRow = -1;
			if (this.shouldBind)
			{
				this.shouldBind = false;
				this.UpdateIsBinding();
			}
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x000425F2 File Offset: 0x000407F2
		internal void UnwireEvents(IList list)
		{
			if (list is IBindingList && ((IBindingList)list).SupportsChangeNotification)
			{
				((IBindingList)list).ListChanged -= this.List_ListChanged;
			}
		}

		/// <summary>Updates the status of the binding.</summary>
		// Token: 0x060013D2 RID: 5074 RVA: 0x00042620 File Offset: 0x00040820
		protected override void UpdateIsBinding()
		{
			this.UpdateIsBinding(true);
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x0004262C File Offset: 0x0004082C
		private void UpdateIsBinding(bool raiseItemChangedEvent)
		{
			bool flag = this.list != null && this.list.Count > 0 && this.shouldBind && this.listposition != -1;
			if (this.list != null && this.bound != flag)
			{
				this.bound = flag;
				int num = (flag ? 0 : (-1));
				this.ChangeRecordState(num, this.bound, this.Position != num, true, false);
				int count = base.Bindings.Count;
				for (int i = 0; i < count; i++)
				{
					base.Bindings[i].UpdateIsBinding();
				}
				if (raiseItemChangedEvent)
				{
					this.OnItemChanged(this.resetEvent);
				}
			}
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x000426DC File Offset: 0x000408DC
		private void UpdateLastGoodKnownRow(ListChangedEventArgs e)
		{
			switch (e.ListChangedType)
			{
			case ListChangedType.Reset:
				this.lastGoodKnownRow = -1;
				return;
			case ListChangedType.ItemAdded:
				if (e.NewIndex <= this.lastGoodKnownRow && this.lastGoodKnownRow < this.List.Count - 1)
				{
					this.lastGoodKnownRow++;
					return;
				}
				break;
			case ListChangedType.ItemDeleted:
				if (e.NewIndex == this.lastGoodKnownRow)
				{
					this.lastGoodKnownRow = -1;
					return;
				}
				break;
			case ListChangedType.ItemMoved:
				if (e.OldIndex == this.lastGoodKnownRow)
				{
					this.lastGoodKnownRow = e.NewIndex;
					return;
				}
				break;
			case ListChangedType.ItemChanged:
				if (e.NewIndex == this.lastGoodKnownRow)
				{
					this.lastGoodKnownRow = -1;
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x0004278B File Offset: 0x0004098B
		internal void WireEvents(IList list)
		{
			if (list is IBindingList && ((IBindingList)list).SupportsChangeNotification)
			{
				((IBindingList)list).ListChanged += this.List_ListChanged;
			}
		}

		// Token: 0x04000938 RID: 2360
		private object dataSource;

		// Token: 0x04000939 RID: 2361
		private IList list;

		// Token: 0x0400093A RID: 2362
		private bool bound;

		// Token: 0x0400093B RID: 2363
		private bool shouldBind = true;

		/// <summary>Specifies the current position of the <see cref="T:System.Windows.Forms.CurrencyManager" /> in the list.</summary>
		// Token: 0x0400093C RID: 2364
		protected int listposition = -1;

		// Token: 0x0400093D RID: 2365
		private int lastGoodKnownRow = -1;

		// Token: 0x0400093E RID: 2366
		private bool pullingData;

		// Token: 0x0400093F RID: 2367
		private bool inChangeRecordState;

		// Token: 0x04000940 RID: 2368
		private bool suspendPushDataInCurrentChanged;

		// Token: 0x04000941 RID: 2369
		private ItemChangedEventHandler onItemChanged;

		// Token: 0x04000942 RID: 2370
		private ListChangedEventHandler onListChanged;

		// Token: 0x04000943 RID: 2371
		private ItemChangedEventArgs resetEvent = new ItemChangedEventArgs(-1);

		// Token: 0x04000944 RID: 2372
		private EventHandler onMetaDataChangedHandler;

		/// <summary>Specifies the data type of the list.</summary>
		// Token: 0x04000945 RID: 2373
		protected Type finalType;
	}
}
