using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>Encapsulates the data source for a form.</summary>
	// Token: 0x0200013A RID: 314
	[DefaultProperty("DataSource")]
	[DefaultEvent("CurrentChanged")]
	[ComplexBindingProperties("DataSource", "DataMember")]
	[Designer("System.Windows.Forms.Design.BindingSourceDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionBindingSource")]
	public class BindingSource : Component, IBindingListView, IBindingList, IList, ICollection, IEnumerable, ITypedList, ICancelAddNew, ISupportInitializeNotification, ISupportInitialize, ICurrencyManagerProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingSource" /> class to the default property values.</summary>
		// Token: 0x06000BA3 RID: 2979 RVA: 0x00021272 File Offset: 0x0001F472
		public BindingSource()
			: this(null, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingSource" /> class with the specified data source and data member.</summary>
		/// <param name="dataSource">The data source for the <see cref="T:System.Windows.Forms.BindingSource" />.</param>
		/// <param name="dataMember">The specific column or list name within the data source to bind to.</param>
		// Token: 0x06000BA4 RID: 2980 RVA: 0x00021280 File Offset: 0x0001F480
		public BindingSource(object dataSource, string dataMember)
		{
			this.dataSource = dataSource;
			this.dataMember = dataMember;
			this._innerList = new ArrayList();
			this.currencyManager = new CurrencyManager(this);
			this.WireCurrencyManager(this.currencyManager);
			this.listItemPropertyChangedHandler = new EventHandler(this.ListItem_PropertyChanged);
			this.ResetList();
			this.WireDataSource();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingSource" /> class and adds the <see cref="T:System.Windows.Forms.BindingSource" /> to the specified container.</summary>
		/// <param name="container">The <see cref="T:System.ComponentModel.IContainer" /> to add the current <see cref="T:System.Windows.Forms.BindingSource" /> to.</param>
		// Token: 0x06000BA5 RID: 2981 RVA: 0x00021302 File Offset: 0x0001F502
		public BindingSource(IContainer container)
			: this()
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			container.Add(this);
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x00021320 File Offset: 0x0001F520
		private bool AllowNewInternal(bool checkconstructor)
		{
			if (this.disposedOrFinalized)
			{
				return false;
			}
			if (this.allowNewIsSet)
			{
				return this.allowNewSetValue;
			}
			if (this.listExtractedFromEnumerable)
			{
				return false;
			}
			if (this.isBindingList)
			{
				return ((IBindingList)this.List).AllowNew;
			}
			return this.IsListWriteable(checkconstructor);
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x00021370 File Offset: 0x0001F570
		private bool IsListWriteable(bool checkconstructor)
		{
			return !this.List.IsReadOnly && !this.List.IsFixedSize && (!checkconstructor || this.itemConstructor != null);
		}

		/// <summary>Gets the currency manager associated with this <see cref="T:System.Windows.Forms.BindingSource" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.CurrencyManager" /> associated with this <see cref="T:System.Windows.Forms.BindingSource" />.</returns>
		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x0002139F File Offset: 0x0001F59F
		[Browsable(false)]
		public virtual CurrencyManager CurrencyManager
		{
			get
			{
				return ((ICurrencyManagerProvider)this).GetRelatedCurrencyManager(null);
			}
		}

		/// <summary>Gets the related currency manager for the specified data member.</summary>
		/// <param name="dataMember">The name of column or list, within the data source to retrieve the currency manager for.</param>
		/// <returns>The related <see cref="T:System.Windows.Forms.CurrencyManager" /> for the specified data member.</returns>
		// Token: 0x06000BA9 RID: 2985 RVA: 0x000213A8 File Offset: 0x0001F5A8
		public virtual CurrencyManager GetRelatedCurrencyManager(string dataMember)
		{
			this.EnsureInnerList();
			if (string.IsNullOrEmpty(dataMember))
			{
				return this.currencyManager;
			}
			if (dataMember.IndexOf(".") != -1)
			{
				return null;
			}
			BindingSource relatedBindingSource = this.GetRelatedBindingSource(dataMember);
			return ((ICurrencyManagerProvider)relatedBindingSource).CurrencyManager;
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x000213E8 File Offset: 0x0001F5E8
		private BindingSource GetRelatedBindingSource(string dataMember)
		{
			if (this.relatedBindingSources == null)
			{
				this.relatedBindingSources = new Dictionary<string, BindingSource>();
			}
			foreach (string text in this.relatedBindingSources.Keys)
			{
				if (string.Equals(text, dataMember, StringComparison.OrdinalIgnoreCase))
				{
					return this.relatedBindingSources[text];
				}
			}
			BindingSource bindingSource = new BindingSource(this, dataMember);
			this.relatedBindingSources[dataMember] = bindingSource;
			return bindingSource;
		}

		/// <summary>Gets the current item in the list.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the current item in the underlying list represented by the <see cref="P:System.Windows.Forms.BindingSource.List" /> property, or <see langword="null" /> if the list has no items.</returns>
		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x00021480 File Offset: 0x0001F680
		[Browsable(false)]
		public object Current
		{
			get
			{
				if (this.currencyManager.Count <= 0)
				{
					return null;
				}
				return this.currencyManager.Current;
			}
		}

		/// <summary>Gets or sets the specific list in the data source to which the connector currently binds to.</summary>
		/// <returns>The name of a list (or row) in the <see cref="P:System.Windows.Forms.BindingSource.DataSource" />. The default is an empty string ("").</returns>
		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x0002149D File Offset: 0x0001F69D
		// (set) Token: 0x06000BAD RID: 2989 RVA: 0x000214A5 File Offset: 0x0001F6A5
		[SRCategory("CatData")]
		[DefaultValue("")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[SRDescription("BindingSourceDataMemberDescr")]
		public string DataMember
		{
			get
			{
				return this.dataMember;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (!this.dataMember.Equals(value))
				{
					this.dataMember = value;
					this.ResetList();
					this.OnDataMemberChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the data source that the connector binds to.</summary>
		/// <returns>An <see cref="T:System.Object" /> that acts as a data source. The default is <see langword="null" />.</returns>
		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x000214D7 File Offset: 0x0001F6D7
		// (set) Token: 0x06000BAF RID: 2991 RVA: 0x000214DF File Offset: 0x0001F6DF
		[SRCategory("CatData")]
		[DefaultValue(null)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[AttributeProvider(typeof(IListSource))]
		[SRDescription("BindingSourceDataSourceDescr")]
		public object DataSource
		{
			get
			{
				return this.dataSource;
			}
			set
			{
				if (this.dataSource != value)
				{
					this.ThrowIfBindingSourceRecursionDetected(value);
					this.UnwireDataSource();
					this.dataSource = value;
					this.ClearInvalidDataMember();
					this.ResetList();
					this.WireDataSource();
					this.OnDataSourceChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x0002151C File Offset: 0x0001F71C
		// (set) Token: 0x06000BB1 RID: 2993 RVA: 0x0002154C File Offset: 0x0001F74C
		private string InnerListFilter
		{
			get
			{
				IBindingListView bindingListView = this.List as IBindingListView;
				if (bindingListView != null && bindingListView.SupportsFiltering)
				{
					return bindingListView.Filter;
				}
				return string.Empty;
			}
			set
			{
				if (this.initializing || base.DesignMode)
				{
					return;
				}
				if (string.Equals(value, this.InnerListFilter, StringComparison.Ordinal))
				{
					return;
				}
				IBindingListView bindingListView = this.List as IBindingListView;
				if (bindingListView != null && bindingListView.SupportsFiltering)
				{
					bindingListView.Filter = value;
				}
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x00021598 File Offset: 0x0001F798
		// (set) Token: 0x06000BB3 RID: 2995 RVA: 0x00021610 File Offset: 0x0001F810
		private string InnerListSort
		{
			get
			{
				ListSortDescriptionCollection listSortDescriptionCollection = null;
				IBindingListView bindingListView = this.List as IBindingListView;
				IBindingList bindingList = this.List as IBindingList;
				if (bindingListView != null && bindingListView.SupportsAdvancedSorting)
				{
					listSortDescriptionCollection = bindingListView.SortDescriptions;
				}
				else if (bindingList != null && bindingList.SupportsSorting && bindingList.IsSorted)
				{
					listSortDescriptionCollection = new ListSortDescriptionCollection(new ListSortDescription[]
					{
						new ListSortDescription(bindingList.SortProperty, bindingList.SortDirection)
					});
				}
				return BindingSource.BuildSortString(listSortDescriptionCollection);
			}
			set
			{
				if (this.initializing || base.DesignMode)
				{
					return;
				}
				if (string.Compare(value, this.InnerListSort, false, CultureInfo.InvariantCulture) == 0)
				{
					return;
				}
				ListSortDescriptionCollection listSortDescriptionCollection = this.ParseSortString(value);
				IBindingListView bindingListView = this.List as IBindingListView;
				IBindingList bindingList = this.List as IBindingList;
				if (bindingListView == null || !bindingListView.SupportsAdvancedSorting)
				{
					if (bindingList != null && bindingList.SupportsSorting)
					{
						if (listSortDescriptionCollection.Count == 0)
						{
							bindingList.RemoveSort();
							return;
						}
						if (listSortDescriptionCollection.Count == 1)
						{
							bindingList.ApplySort(listSortDescriptionCollection[0].PropertyDescriptor, listSortDescriptionCollection[0].SortDirection);
							return;
						}
					}
					return;
				}
				if (listSortDescriptionCollection.Count == 0)
				{
					bindingListView.RemoveSort();
					return;
				}
				bindingListView.ApplySort(listSortDescriptionCollection);
			}
		}

		/// <summary>Gets a value indicating whether the list binding is suspended.</summary>
		/// <returns>
		///   <see langword="true" /> to indicate the binding is suspended; otherwise, <see langword="false" />.</returns>
		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x000216C7 File Offset: 0x0001F8C7
		[Browsable(false)]
		public bool IsBindingSuspended
		{
			get
			{
				return this.currencyManager.IsBindingSuspended;
			}
		}

		/// <summary>Gets the list that the connector is bound to.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> that represents the list, or <see langword="null" /> if there is no underlying list associated with this <see cref="T:System.Windows.Forms.BindingSource" />.</returns>
		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000BB5 RID: 2997 RVA: 0x000216D4 File Offset: 0x0001F8D4
		[Browsable(false)]
		public IList List
		{
			get
			{
				this.EnsureInnerList();
				return this._innerList;
			}
		}

		/// <summary>Gets or sets the index of the current item in the underlying list.</summary>
		/// <returns>A zero-based index that specifies the position of the current item in the underlying list.</returns>
		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000BB6 RID: 2998 RVA: 0x000216E2 File Offset: 0x0001F8E2
		// (set) Token: 0x06000BB7 RID: 2999 RVA: 0x000216EF File Offset: 0x0001F8EF
		[DefaultValue(-1)]
		[Browsable(false)]
		public int Position
		{
			get
			{
				return this.currencyManager.Position;
			}
			set
			{
				if (this.currencyManager.Position != value)
				{
					this.currencyManager.Position = value;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether <see cref="E:System.Windows.Forms.BindingSource.ListChanged" /> events should be raised.</summary>
		/// <returns>
		///   <see langword="true" /> if <see cref="E:System.Windows.Forms.BindingSource.ListChanged" /> events should be raised; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x0002170B File Offset: 0x0001F90B
		// (set) Token: 0x06000BB9 RID: 3001 RVA: 0x00021713 File Offset: 0x0001F913
		[DefaultValue(true)]
		[Browsable(false)]
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

		/// <summary>Gets or sets the column names used for sorting, and the sort order for viewing the rows in the data source.</summary>
		/// <returns>A case-sensitive string containing the column name followed by "ASC" (for ascending) or "DESC" (for descending). The default is <see langword="null" />.</returns>
		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x00021725 File Offset: 0x0001F925
		// (set) Token: 0x06000BBB RID: 3003 RVA: 0x0002172D File Offset: 0x0001F92D
		[SRCategory("CatData")]
		[DefaultValue(null)]
		[SRDescription("BindingSourceSortDescr")]
		public string Sort
		{
			get
			{
				return this.sort;
			}
			set
			{
				this.sort = value;
				this.InnerListSort = value;
			}
		}

		/// <summary>Occurs before an item is added to the underlying list.</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.ComponentModel.AddingNewEventArgs.NewObject" /> is not the same type as the type contained in the list.</exception>
		// Token: 0x14000058 RID: 88
		// (add) Token: 0x06000BBC RID: 3004 RVA: 0x0002173D File Offset: 0x0001F93D
		// (remove) Token: 0x06000BBD RID: 3005 RVA: 0x00021750 File Offset: 0x0001F950
		[SRCategory("CatData")]
		[SRDescription("BindingSourceAddingNewEventHandlerDescr")]
		public event AddingNewEventHandler AddingNew
		{
			add
			{
				base.Events.AddHandler(BindingSource.EVENT_ADDINGNEW, value);
			}
			remove
			{
				base.Events.RemoveHandler(BindingSource.EVENT_ADDINGNEW, value);
			}
		}

		/// <summary>Occurs when all the clients have been bound to this <see cref="T:System.Windows.Forms.BindingSource" />.</summary>
		// Token: 0x14000059 RID: 89
		// (add) Token: 0x06000BBE RID: 3006 RVA: 0x00021763 File Offset: 0x0001F963
		// (remove) Token: 0x06000BBF RID: 3007 RVA: 0x00021776 File Offset: 0x0001F976
		[SRCategory("CatData")]
		[SRDescription("BindingSourceBindingCompleteEventHandlerDescr")]
		public event BindingCompleteEventHandler BindingComplete
		{
			add
			{
				base.Events.AddHandler(BindingSource.EVENT_BINDINGCOMPLETE, value);
			}
			remove
			{
				base.Events.RemoveHandler(BindingSource.EVENT_BINDINGCOMPLETE, value);
			}
		}

		/// <summary>Occurs when a currency-related exception is silently handled by the <see cref="T:System.Windows.Forms.BindingSource" />.</summary>
		// Token: 0x1400005A RID: 90
		// (add) Token: 0x06000BC0 RID: 3008 RVA: 0x00021789 File Offset: 0x0001F989
		// (remove) Token: 0x06000BC1 RID: 3009 RVA: 0x0002179C File Offset: 0x0001F99C
		[SRCategory("CatData")]
		[SRDescription("BindingSourceDataErrorEventHandlerDescr")]
		public event BindingManagerDataErrorEventHandler DataError
		{
			add
			{
				base.Events.AddHandler(BindingSource.EVENT_DATAERROR, value);
			}
			remove
			{
				base.Events.RemoveHandler(BindingSource.EVENT_DATAERROR, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.BindingSource.DataSource" /> property value has changed.</summary>
		// Token: 0x1400005B RID: 91
		// (add) Token: 0x06000BC2 RID: 3010 RVA: 0x000217AF File Offset: 0x0001F9AF
		// (remove) Token: 0x06000BC3 RID: 3011 RVA: 0x000217C2 File Offset: 0x0001F9C2
		[SRCategory("CatData")]
		[SRDescription("BindingSourceDataSourceChangedEventHandlerDescr")]
		public event EventHandler DataSourceChanged
		{
			add
			{
				base.Events.AddHandler(BindingSource.EVENT_DATASOURCECHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(BindingSource.EVENT_DATASOURCECHANGED, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.BindingSource.DataMember" /> property value has changed.</summary>
		// Token: 0x1400005C RID: 92
		// (add) Token: 0x06000BC4 RID: 3012 RVA: 0x000217D5 File Offset: 0x0001F9D5
		// (remove) Token: 0x06000BC5 RID: 3013 RVA: 0x000217E8 File Offset: 0x0001F9E8
		[SRCategory("CatData")]
		[SRDescription("BindingSourceDataMemberChangedEventHandlerDescr")]
		public event EventHandler DataMemberChanged
		{
			add
			{
				base.Events.AddHandler(BindingSource.EVENT_DATAMEMBERCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(BindingSource.EVENT_DATAMEMBERCHANGED, value);
			}
		}

		/// <summary>Occurs when the currently bound item changes.</summary>
		// Token: 0x1400005D RID: 93
		// (add) Token: 0x06000BC6 RID: 3014 RVA: 0x000217FB File Offset: 0x0001F9FB
		// (remove) Token: 0x06000BC7 RID: 3015 RVA: 0x0002180E File Offset: 0x0001FA0E
		[SRCategory("CatData")]
		[SRDescription("BindingSourceCurrentChangedEventHandlerDescr")]
		public event EventHandler CurrentChanged
		{
			add
			{
				base.Events.AddHandler(BindingSource.EVENT_CURRENTCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(BindingSource.EVENT_CURRENTCHANGED, value);
			}
		}

		/// <summary>Occurs when a property value of the <see cref="P:System.Windows.Forms.BindingSource.Current" /> property has changed.</summary>
		// Token: 0x1400005E RID: 94
		// (add) Token: 0x06000BC8 RID: 3016 RVA: 0x00021821 File Offset: 0x0001FA21
		// (remove) Token: 0x06000BC9 RID: 3017 RVA: 0x00021834 File Offset: 0x0001FA34
		[SRCategory("CatData")]
		[SRDescription("BindingSourceCurrentItemChangedEventHandlerDescr")]
		public event EventHandler CurrentItemChanged
		{
			add
			{
				base.Events.AddHandler(BindingSource.EVENT_CURRENTITEMCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(BindingSource.EVENT_CURRENTITEMCHANGED, value);
			}
		}

		/// <summary>Occurs when the underlying list changes or an item in the list changes.</summary>
		// Token: 0x1400005F RID: 95
		// (add) Token: 0x06000BCA RID: 3018 RVA: 0x00021847 File Offset: 0x0001FA47
		// (remove) Token: 0x06000BCB RID: 3019 RVA: 0x0002185A File Offset: 0x0001FA5A
		[SRCategory("CatData")]
		[SRDescription("BindingSourceListChangedEventHandlerDescr")]
		public event ListChangedEventHandler ListChanged
		{
			add
			{
				base.Events.AddHandler(BindingSource.EVENT_LISTCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(BindingSource.EVENT_LISTCHANGED, value);
			}
		}

		/// <summary>Occurs after the value of the <see cref="P:System.Windows.Forms.BindingSource.Position" /> property has changed.</summary>
		// Token: 0x14000060 RID: 96
		// (add) Token: 0x06000BCC RID: 3020 RVA: 0x0002186D File Offset: 0x0001FA6D
		// (remove) Token: 0x06000BCD RID: 3021 RVA: 0x00021880 File Offset: 0x0001FA80
		[SRCategory("CatData")]
		[SRDescription("BindingSourcePositionChangedEventHandlerDescr")]
		public event EventHandler PositionChanged
		{
			add
			{
				base.Events.AddHandler(BindingSource.EVENT_POSITIONCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(BindingSource.EVENT_POSITIONCHANGED, value);
			}
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x00021894 File Offset: 0x0001FA94
		private static string BuildSortString(ListSortDescriptionCollection sortsColln)
		{
			if (sortsColln == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(sortsColln.Count);
			for (int i = 0; i < sortsColln.Count; i++)
			{
				stringBuilder.Append(sortsColln[i].PropertyDescriptor.Name + ((sortsColln[i].SortDirection == ListSortDirection.Ascending) ? " ASC" : " DESC") + ((i < sortsColln.Count - 1) ? "," : string.Empty));
			}
			return stringBuilder.ToString();
		}

		/// <summary>Cancels the current edit operation.</summary>
		// Token: 0x06000BCF RID: 3023 RVA: 0x0002191B File Offset: 0x0001FB1B
		public void CancelEdit()
		{
			this.currencyManager.CancelCurrentEdit();
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x00021928 File Offset: 0x0001FB28
		private void ThrowIfBindingSourceRecursionDetected(object newDataSource)
		{
			for (BindingSource bindingSource = newDataSource as BindingSource; bindingSource != null; bindingSource = bindingSource.DataSource as BindingSource)
			{
				if (bindingSource == this)
				{
					throw new InvalidOperationException(SR.GetString("BindingSourceRecursionDetected"));
				}
			}
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x00021961 File Offset: 0x0001FB61
		private void ClearInvalidDataMember()
		{
			if (!this.IsDataMemberValid())
			{
				this.dataMember = "";
				this.OnDataMemberChanged(EventArgs.Empty);
			}
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x00021984 File Offset: 0x0001FB84
		private static IList CreateBindingList(Type type)
		{
			Type typeFromHandle = typeof(BindingList<>);
			Type type2 = typeFromHandle.MakeGenericType(new Type[] { type });
			return (IList)SecurityUtils.SecureCreateInstance(type2);
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x000219B8 File Offset: 0x0001FBB8
		private static object CreateInstanceOfType(Type type)
		{
			object obj = null;
			Exception ex = null;
			try
			{
				obj = SecurityUtils.SecureCreateInstance(type);
			}
			catch (TargetInvocationException ex2)
			{
				ex = ex2;
			}
			catch (MethodAccessException ex3)
			{
				ex = ex3;
			}
			catch (MissingMethodException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				throw new NotSupportedException(SR.GetString("BindingSourceInstanceError"), ex);
			}
			return obj;
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00021A20 File Offset: 0x0001FC20
		private void CurrencyManager_PositionChanged(object sender, EventArgs e)
		{
			this.OnPositionChanged(e);
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x00021A29 File Offset: 0x0001FC29
		private void CurrencyManager_CurrentChanged(object sender, EventArgs e)
		{
			this.OnCurrentChanged(EventArgs.Empty);
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x00021A36 File Offset: 0x0001FC36
		private void CurrencyManager_CurrentItemChanged(object sender, EventArgs e)
		{
			this.OnCurrentItemChanged(EventArgs.Empty);
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x00021A43 File Offset: 0x0001FC43
		private void CurrencyManager_BindingComplete(object sender, BindingCompleteEventArgs e)
		{
			this.OnBindingComplete(e);
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x00021A4C File Offset: 0x0001FC4C
		private void CurrencyManager_DataError(object sender, BindingManagerDataErrorEventArgs e)
		{
			this.OnDataError(e);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.BindingSource" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06000BD9 RID: 3033 RVA: 0x00021A58 File Offset: 0x0001FC58
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.UnwireDataSource();
				this.UnwireInnerList();
				this.UnhookItemChangedEventsForOldCurrent();
				this.UnwireCurrencyManager(this.currencyManager);
				this.dataSource = null;
				this.sort = null;
				this.dataMember = null;
				this._innerList = null;
				this.isBindingList = false;
				this.needToSetList = true;
				this.raiseListChangedEvents = false;
			}
			this.disposedOrFinalized = true;
			base.Dispose(disposing);
		}

		/// <summary>Applies pending changes to the underlying data source.</summary>
		// Token: 0x06000BDA RID: 3034 RVA: 0x00021AC8 File Offset: 0x0001FCC8
		public void EndEdit()
		{
			if (this.endingEdit)
			{
				return;
			}
			try
			{
				this.endingEdit = true;
				this.currencyManager.EndCurrentEdit();
			}
			finally
			{
				this.endingEdit = false;
			}
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x00021B0C File Offset: 0x0001FD0C
		private void EnsureInnerList()
		{
			if (!this.initializing && this.needToSetList)
			{
				this.needToSetList = false;
				this.ResetList();
			}
		}

		/// <summary>Returns the index of the item in the list with the specified property name and value.</summary>
		/// <param name="propertyName">The name of the property to search for.</param>
		/// <param name="key">The value of the item with the specified <paramref name="propertyName" /> to find.</param>
		/// <returns>The zero-based index of the item with the specified property name and value.</returns>
		/// <exception cref="T:System.InvalidOperationException">The underlying list is not a <see cref="T:System.ComponentModel.IBindingList" /> with searching functionality implemented.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="propertyName" /> does not match a property in the list.</exception>
		// Token: 0x06000BDC RID: 3036 RVA: 0x00021B2C File Offset: 0x0001FD2C
		public int Find(string propertyName, object key)
		{
			PropertyDescriptor propertyDescriptor = ((this.itemShape == null) ? null : this.itemShape.Find(propertyName, true));
			if (propertyDescriptor == null)
			{
				throw new ArgumentException(SR.GetString("DataSourceDataMemberPropNotFound", new object[] { propertyName }));
			}
			return ((IBindingList)this).Find(propertyDescriptor, key);
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x00021B78 File Offset: 0x0001FD78
		private static IList GetListFromType(Type type)
		{
			IList list;
			if (typeof(ITypedList).IsAssignableFrom(type) && typeof(IList).IsAssignableFrom(type))
			{
				list = BindingSource.CreateInstanceOfType(type) as IList;
			}
			else if (typeof(IListSource).IsAssignableFrom(type))
			{
				list = (BindingSource.CreateInstanceOfType(type) as IListSource).GetList();
			}
			else
			{
				list = BindingSource.CreateBindingList(ListBindingHelper.GetListItemType(type));
			}
			return list;
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x00021BEC File Offset: 0x0001FDEC
		private static IList GetListFromEnumerable(IEnumerable enumerable)
		{
			IList list = null;
			foreach (object obj in enumerable)
			{
				if (list == null)
				{
					list = BindingSource.CreateBindingList(obj.GetType());
				}
				list.Add(obj);
			}
			return list;
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x00021C50 File Offset: 0x0001FE50
		private bool IsDataMemberValid()
		{
			if (this.initializing)
			{
				return true;
			}
			if (string.IsNullOrEmpty(this.dataMember))
			{
				return true;
			}
			PropertyDescriptorCollection listItemProperties = ListBindingHelper.GetListItemProperties(this.dataSource);
			return listItemProperties[this.dataMember] != null;
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x00021C98 File Offset: 0x0001FE98
		private void InnerList_ListChanged(object sender, ListChangedEventArgs e)
		{
			if (!this.innerListChanging)
			{
				try
				{
					this.innerListChanging = true;
					this.OnListChanged(e);
				}
				finally
				{
					this.innerListChanging = false;
				}
			}
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x00021CD8 File Offset: 0x0001FED8
		private void ListItem_PropertyChanged(object sender, EventArgs e)
		{
			int num;
			if (sender == this.currentItemHookedForItemChange)
			{
				num = this.Position;
			}
			else
			{
				num = ((IList)this).IndexOf(sender);
			}
			this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, num));
		}

		/// <summary>Moves to the first item in the list.</summary>
		// Token: 0x06000BE2 RID: 3042 RVA: 0x00021D0C File Offset: 0x0001FF0C
		public void MoveFirst()
		{
			this.Position = 0;
		}

		/// <summary>Moves to the last item in the list.</summary>
		// Token: 0x06000BE3 RID: 3043 RVA: 0x00021D15 File Offset: 0x0001FF15
		public void MoveLast()
		{
			this.Position = this.Count - 1;
		}

		/// <summary>Moves to the next item in the list.</summary>
		// Token: 0x06000BE4 RID: 3044 RVA: 0x00021D28 File Offset: 0x0001FF28
		public void MoveNext()
		{
			int num = this.Position + 1;
			this.Position = num;
		}

		/// <summary>Moves to the previous item in the list.</summary>
		// Token: 0x06000BE5 RID: 3045 RVA: 0x00021D48 File Offset: 0x0001FF48
		public void MovePrevious()
		{
			int num = this.Position - 1;
			this.Position = num;
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x00021D65 File Offset: 0x0001FF65
		private void OnSimpleListChanged(ListChangedType listChangedType, int newIndex)
		{
			if (!this.isBindingList)
			{
				this.OnListChanged(new ListChangedEventArgs(listChangedType, newIndex));
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingSource.AddingNew" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000BE7 RID: 3047 RVA: 0x00021D7C File Offset: 0x0001FF7C
		protected virtual void OnAddingNew(AddingNewEventArgs e)
		{
			AddingNewEventHandler addingNewEventHandler = (AddingNewEventHandler)base.Events[BindingSource.EVENT_ADDINGNEW];
			if (addingNewEventHandler != null)
			{
				addingNewEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingSource.BindingComplete" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.BindingCompleteEventArgs" /> that contains the event data.</param>
		// Token: 0x06000BE8 RID: 3048 RVA: 0x00021DAC File Offset: 0x0001FFAC
		protected virtual void OnBindingComplete(BindingCompleteEventArgs e)
		{
			BindingCompleteEventHandler bindingCompleteEventHandler = (BindingCompleteEventHandler)base.Events[BindingSource.EVENT_BINDINGCOMPLETE];
			if (bindingCompleteEventHandler != null)
			{
				bindingCompleteEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingSource.CurrentChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000BE9 RID: 3049 RVA: 0x00021DDC File Offset: 0x0001FFDC
		protected virtual void OnCurrentChanged(EventArgs e)
		{
			this.UnhookItemChangedEventsForOldCurrent();
			this.HookItemChangedEventsForNewCurrent();
			EventHandler eventHandler = (EventHandler)base.Events[BindingSource.EVENT_CURRENTCHANGED];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingSource.CurrentItemChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000BEA RID: 3050 RVA: 0x00021E18 File Offset: 0x00020018
		protected virtual void OnCurrentItemChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[BindingSource.EVENT_CURRENTITEMCHANGED];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingSource.DataError" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.BindingManagerDataErrorEventArgs" /> that contains the event data.</param>
		// Token: 0x06000BEB RID: 3051 RVA: 0x00021E48 File Offset: 0x00020048
		protected virtual void OnDataError(BindingManagerDataErrorEventArgs e)
		{
			BindingManagerDataErrorEventHandler bindingManagerDataErrorEventHandler = base.Events[BindingSource.EVENT_DATAERROR] as BindingManagerDataErrorEventHandler;
			if (bindingManagerDataErrorEventHandler != null)
			{
				bindingManagerDataErrorEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingSource.DataMemberChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000BEC RID: 3052 RVA: 0x00021E78 File Offset: 0x00020078
		protected virtual void OnDataMemberChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[BindingSource.EVENT_DATAMEMBERCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingSource.DataSourceChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000BED RID: 3053 RVA: 0x00021EA8 File Offset: 0x000200A8
		protected virtual void OnDataSourceChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[BindingSource.EVENT_DATASOURCECHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingSource.ListChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000BEE RID: 3054 RVA: 0x00021ED8 File Offset: 0x000200D8
		protected virtual void OnListChanged(ListChangedEventArgs e)
		{
			if (!this.raiseListChangedEvents || this.initializing)
			{
				return;
			}
			ListChangedEventHandler listChangedEventHandler = (ListChangedEventHandler)base.Events[BindingSource.EVENT_LISTCHANGED];
			if (listChangedEventHandler != null)
			{
				listChangedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.BindingSource.PositionChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.ListChangedEventArgs" /> that contains the event data.</param>
		// Token: 0x06000BEF RID: 3055 RVA: 0x00021F18 File Offset: 0x00020118
		protected virtual void OnPositionChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[BindingSource.EVENT_POSITIONCHANGED];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x00021F48 File Offset: 0x00020148
		private void ParentCurrencyManager_CurrentItemChanged(object sender, EventArgs e)
		{
			if (this.initializing)
			{
				return;
			}
			if (this.parentsCurrentItemChanging)
			{
				return;
			}
			try
			{
				this.parentsCurrentItemChanging = true;
				bool flag;
				this.currencyManager.PullData(out flag);
			}
			finally
			{
				this.parentsCurrentItemChanging = false;
			}
			CurrencyManager currencyManager = (CurrencyManager)sender;
			if (!string.IsNullOrEmpty(this.dataMember))
			{
				object obj = null;
				IList list = null;
				if (currencyManager.Count > 0)
				{
					PropertyDescriptorCollection itemProperties = currencyManager.GetItemProperties();
					PropertyDescriptor propertyDescriptor = itemProperties[this.dataMember];
					if (propertyDescriptor != null)
					{
						obj = ListBindingHelper.GetList(propertyDescriptor.GetValue(currencyManager.Current));
						list = obj as IList;
					}
				}
				if (list != null)
				{
					this.SetList(list, false, true);
				}
				else if (obj != null)
				{
					this.SetList(BindingSource.WrapObjectInBindingList(obj), false, false);
				}
				else
				{
					this.SetList(BindingSource.CreateBindingList(this.itemType), false, false);
				}
				bool flag2 = this.lastCurrentItem == null || currencyManager.Count == 0 || this.lastCurrentItem != currencyManager.Current || this.Position >= this.Count;
				this.lastCurrentItem = ((currencyManager.Count > 0) ? currencyManager.Current : null);
				if (flag2)
				{
					this.Position = ((this.Count > 0) ? 0 : (-1));
				}
			}
			this.OnCurrentItemChanged(EventArgs.Empty);
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x00022098 File Offset: 0x00020298
		private void ParentCurrencyManager_MetaDataChanged(object sender, EventArgs e)
		{
			this.ClearInvalidDataMember();
			this.ResetList();
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x000220A8 File Offset: 0x000202A8
		private ListSortDescriptionCollection ParseSortString(string sortString)
		{
			if (string.IsNullOrEmpty(sortString))
			{
				return new ListSortDescriptionCollection();
			}
			ArrayList arrayList = new ArrayList();
			PropertyDescriptorCollection itemProperties = this.currencyManager.GetItemProperties();
			string[] array = sortString.Split(new char[] { ',' });
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i].Trim();
				int length = text.Length;
				bool flag = true;
				if (length >= 5 && string.Compare(text, length - 4, " ASC", 0, 4, true, CultureInfo.InvariantCulture) == 0)
				{
					text = text.Substring(0, length - 4).Trim();
				}
				else if (length >= 6 && string.Compare(text, length - 5, " DESC", 0, 5, true, CultureInfo.InvariantCulture) == 0)
				{
					flag = false;
					text = text.Substring(0, length - 5).Trim();
				}
				if (text.StartsWith("["))
				{
					if (!text.EndsWith("]"))
					{
						throw new ArgumentException(SR.GetString("BindingSourceBadSortString"));
					}
					text = text.Substring(1, text.Length - 2);
				}
				PropertyDescriptor propertyDescriptor = itemProperties.Find(text, true);
				if (propertyDescriptor == null)
				{
					throw new ArgumentException(SR.GetString("BindingSourceSortStringPropertyNotInIBindingList"));
				}
				arrayList.Add(new ListSortDescription(propertyDescriptor, flag ? ListSortDirection.Ascending : ListSortDirection.Descending));
			}
			ListSortDescription[] array2 = new ListSortDescription[arrayList.Count];
			arrayList.CopyTo(array2);
			return new ListSortDescriptionCollection(array2);
		}

		/// <summary>Removes the current item from the list.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.BindingSource.AllowRemove" /> property is <see langword="false" />.  
		///  -or-  
		///  <see cref="P:System.Windows.Forms.BindingSource.Position" /> is less than zero or greater than <see cref="P:System.Windows.Forms.BindingSource.Count" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The underlying list represented by the <see cref="P:System.Windows.Forms.BindingSource.List" /> property is read-only or has a fixed size.</exception>
		// Token: 0x06000BF3 RID: 3059 RVA: 0x00022210 File Offset: 0x00020410
		public void RemoveCurrent()
		{
			if (!((IBindingList)this).AllowRemove)
			{
				throw new InvalidOperationException(SR.GetString("BindingSourceRemoveCurrentNotAllowed"));
			}
			if (this.Position < 0 || this.Position >= this.Count)
			{
				throw new InvalidOperationException(SR.GetString("BindingSourceRemoveCurrentNoCurrentItem"));
			}
			this.RemoveAt(this.Position);
		}

		/// <summary>Reinitializes the <see cref="P:System.Windows.Forms.BindingSource.AllowNew" /> property.</summary>
		// Token: 0x06000BF4 RID: 3060 RVA: 0x00022268 File Offset: 0x00020468
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual void ResetAllowNew()
		{
			this.allowNewIsSet = false;
			this.allowNewSetValue = true;
		}

		/// <summary>Causes a control bound to the <see cref="T:System.Windows.Forms.BindingSource" /> to reread all the items in the list and refresh their displayed values.</summary>
		/// <param name="metadataChanged">
		///   <see langword="true" /> if the data schema has changed; <see langword="false" /> if only values have changed.</param>
		// Token: 0x06000BF5 RID: 3061 RVA: 0x00022278 File Offset: 0x00020478
		public void ResetBindings(bool metadataChanged)
		{
			if (metadataChanged)
			{
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, null));
			}
			this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
		}

		/// <summary>Causes a control bound to the <see cref="T:System.Windows.Forms.BindingSource" /> to reread the currently selected item and refresh its displayed value.</summary>
		// Token: 0x06000BF6 RID: 3062 RVA: 0x00022297 File Offset: 0x00020497
		public void ResetCurrentItem()
		{
			this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, this.Position));
		}

		/// <summary>Causes a control bound to the <see cref="T:System.Windows.Forms.BindingSource" /> to reread the item at the specified index, and refresh its displayed value.</summary>
		/// <param name="itemIndex">The zero-based index of the item that has changed.</param>
		// Token: 0x06000BF7 RID: 3063 RVA: 0x000222AB File Offset: 0x000204AB
		public void ResetItem(int itemIndex)
		{
			this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, itemIndex));
		}

		/// <summary>Resumes data binding.</summary>
		// Token: 0x06000BF8 RID: 3064 RVA: 0x000222BA File Offset: 0x000204BA
		public void ResumeBinding()
		{
			this.currencyManager.ResumeBinding();
		}

		/// <summary>Suspends data binding to prevent changes from updating the bound data source.</summary>
		// Token: 0x06000BF9 RID: 3065 RVA: 0x000222C7 File Offset: 0x000204C7
		public void SuspendBinding()
		{
			this.currencyManager.SuspendBinding();
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x000222D4 File Offset: 0x000204D4
		private void ResetList()
		{
			if (this.initializing)
			{
				this.needToSetList = true;
				return;
			}
			this.needToSetList = false;
			object obj = ((this.dataSource is Type) ? BindingSource.GetListFromType(this.dataSource as Type) : this.dataSource);
			object list = ListBindingHelper.GetList(obj, this.dataMember);
			this.listExtractedFromEnumerable = false;
			IList list2 = null;
			if (list is IList)
			{
				list2 = list as IList;
			}
			else
			{
				if (list is IListSource)
				{
					list2 = (list as IListSource).GetList();
				}
				else if (list is IEnumerable)
				{
					list2 = BindingSource.GetListFromEnumerable(list as IEnumerable);
					if (list2 != null)
					{
						this.listExtractedFromEnumerable = true;
					}
				}
				if (list2 == null)
				{
					if (list != null)
					{
						list2 = BindingSource.WrapObjectInBindingList(list);
					}
					else
					{
						Type listItemType = ListBindingHelper.GetListItemType(this.dataSource, this.dataMember);
						list2 = BindingSource.GetListFromType(listItemType);
						if (list2 == null)
						{
							list2 = BindingSource.CreateBindingList(listItemType);
						}
					}
				}
			}
			this.SetList(list2, true, true);
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x000223B4 File Offset: 0x000205B4
		private void SetList(IList list, bool metaDataChanged, bool applySortAndFilter)
		{
			if (list == null)
			{
				list = BindingSource.CreateBindingList(this.itemType);
			}
			this.UnwireInnerList();
			this.UnhookItemChangedEventsForOldCurrent();
			IList list2 = ListBindingHelper.GetList(list) as IList;
			if (list2 == null)
			{
				list2 = list;
			}
			this._innerList = list2;
			this.isBindingList = list2 is IBindingList;
			if (list2 is IRaiseItemChangedEvents)
			{
				this.listRaisesItemChangedEvents = (list2 as IRaiseItemChangedEvents).RaisesItemChangedEvents;
			}
			else
			{
				this.listRaisesItemChangedEvents = this.isBindingList;
			}
			if (metaDataChanged)
			{
				this.itemType = ListBindingHelper.GetListItemType(this.List);
				this.itemShape = ListBindingHelper.GetListItemProperties(this.List);
				this.itemConstructor = this.itemType.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, new Type[0], null);
			}
			this.WireInnerList();
			this.HookItemChangedEventsForNewCurrent();
			this.ResetBindings(metaDataChanged);
			if (applySortAndFilter)
			{
				if (this.Sort != null)
				{
					this.InnerListSort = this.Sort;
				}
				if (this.Filter != null)
				{
					this.InnerListFilter = this.Filter;
				}
			}
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x000224AC File Offset: 0x000206AC
		private static IList WrapObjectInBindingList(object obj)
		{
			IList list = BindingSource.CreateBindingList(obj.GetType());
			list.Add(obj);
			return list;
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x000224CE File Offset: 0x000206CE
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeAllowNew()
		{
			return this.allowNewIsSet;
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x000224D8 File Offset: 0x000206D8
		private void HookItemChangedEventsForNewCurrent()
		{
			if (!this.listRaisesItemChangedEvents)
			{
				if (this.Position >= 0 && this.Position <= this.Count - 1)
				{
					this.currentItemHookedForItemChange = this.Current;
					this.WirePropertyChangedEvents(this.currentItemHookedForItemChange);
					return;
				}
				this.currentItemHookedForItemChange = null;
			}
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x00022526 File Offset: 0x00020726
		private void UnhookItemChangedEventsForOldCurrent()
		{
			if (!this.listRaisesItemChangedEvents)
			{
				this.UnwirePropertyChangedEvents(this.currentItemHookedForItemChange);
				this.currentItemHookedForItemChange = null;
			}
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x00022544 File Offset: 0x00020744
		private void WireCurrencyManager(CurrencyManager cm)
		{
			if (cm != null)
			{
				cm.PositionChanged += this.CurrencyManager_PositionChanged;
				cm.CurrentChanged += this.CurrencyManager_CurrentChanged;
				cm.CurrentItemChanged += this.CurrencyManager_CurrentItemChanged;
				cm.BindingComplete += this.CurrencyManager_BindingComplete;
				cm.DataError += this.CurrencyManager_DataError;
			}
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x000225B0 File Offset: 0x000207B0
		private void UnwireCurrencyManager(CurrencyManager cm)
		{
			if (cm != null)
			{
				cm.PositionChanged -= this.CurrencyManager_PositionChanged;
				cm.CurrentChanged -= this.CurrencyManager_CurrentChanged;
				cm.CurrentItemChanged -= this.CurrencyManager_CurrentItemChanged;
				cm.BindingComplete -= this.CurrencyManager_BindingComplete;
				cm.DataError -= this.CurrencyManager_DataError;
			}
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x0002261C File Offset: 0x0002081C
		private void WireDataSource()
		{
			if (this.dataSource is ICurrencyManagerProvider)
			{
				CurrencyManager currencyManager = (this.dataSource as ICurrencyManagerProvider).CurrencyManager;
				currencyManager.CurrentItemChanged += this.ParentCurrencyManager_CurrentItemChanged;
				currencyManager.MetaDataChanged += this.ParentCurrencyManager_MetaDataChanged;
			}
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x0002266C File Offset: 0x0002086C
		private void UnwireDataSource()
		{
			if (this.dataSource is ICurrencyManagerProvider)
			{
				CurrencyManager currencyManager = (this.dataSource as ICurrencyManagerProvider).CurrencyManager;
				currencyManager.CurrentItemChanged -= this.ParentCurrencyManager_CurrentItemChanged;
				currencyManager.MetaDataChanged -= this.ParentCurrencyManager_MetaDataChanged;
			}
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x000226BC File Offset: 0x000208BC
		private void WireInnerList()
		{
			if (this._innerList is IBindingList)
			{
				IBindingList bindingList = this._innerList as IBindingList;
				bindingList.ListChanged += this.InnerList_ListChanged;
			}
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x000226F4 File Offset: 0x000208F4
		private void UnwireInnerList()
		{
			if (this._innerList is IBindingList)
			{
				IBindingList bindingList = this._innerList as IBindingList;
				bindingList.ListChanged -= this.InnerList_ListChanged;
			}
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x0002272C File Offset: 0x0002092C
		private void WirePropertyChangedEvents(object item)
		{
			if (item != null && this.itemShape != null)
			{
				for (int i = 0; i < this.itemShape.Count; i++)
				{
					this.itemShape[i].AddValueChanged(item, this.listItemPropertyChangedHandler);
				}
			}
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x00022774 File Offset: 0x00020974
		private void UnwirePropertyChangedEvents(object item)
		{
			if (item != null && this.itemShape != null)
			{
				for (int i = 0; i < this.itemShape.Count; i++)
				{
					this.itemShape[i].RemoveValueChanged(item, this.listItemPropertyChangedHandler);
				}
			}
		}

		/// <summary>Signals the <see cref="T:System.Windows.Forms.BindingSource" /> that initialization is starting.</summary>
		// Token: 0x06000C08 RID: 3080 RVA: 0x000227BA File Offset: 0x000209BA
		void ISupportInitialize.BeginInit()
		{
			this.initializing = true;
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x000227C3 File Offset: 0x000209C3
		private void EndInitCore()
		{
			this.initializing = false;
			this.EnsureInnerList();
			this.OnInitialized();
		}

		/// <summary>Signals the <see cref="T:System.Windows.Forms.BindingSource" /> that initialization is complete.</summary>
		// Token: 0x06000C0A RID: 3082 RVA: 0x000227D8 File Offset: 0x000209D8
		void ISupportInitialize.EndInit()
		{
			ISupportInitializeNotification supportInitializeNotification = this.DataSource as ISupportInitializeNotification;
			if (supportInitializeNotification != null && !supportInitializeNotification.IsInitialized)
			{
				supportInitializeNotification.Initialized += this.DataSource_Initialized;
				return;
			}
			this.EndInitCore();
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x00022818 File Offset: 0x00020A18
		private void DataSource_Initialized(object sender, EventArgs e)
		{
			ISupportInitializeNotification supportInitializeNotification = this.DataSource as ISupportInitializeNotification;
			if (supportInitializeNotification != null)
			{
				supportInitializeNotification.Initialized -= this.DataSource_Initialized;
			}
			this.EndInitCore();
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.BindingSource" /> is initialized.</summary>
		/// <returns>
		///   <see langword="true" /> to indicate the <see cref="T:System.Windows.Forms.BindingSource" /> is initialized; otherwise, <see langword="false" />.</returns>
		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x0002284C File Offset: 0x00020A4C
		bool ISupportInitializeNotification.IsInitialized
		{
			get
			{
				return !this.initializing;
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.BindingSource" /> is initialized.</summary>
		// Token: 0x14000061 RID: 97
		// (add) Token: 0x06000C0D RID: 3085 RVA: 0x00022857 File Offset: 0x00020A57
		// (remove) Token: 0x06000C0E RID: 3086 RVA: 0x0002286A File Offset: 0x00020A6A
		event EventHandler ISupportInitializeNotification.Initialized
		{
			add
			{
				base.Events.AddHandler(BindingSource.EVENT_INITIALIZED, value);
			}
			remove
			{
				base.Events.RemoveHandler(BindingSource.EVENT_INITIALIZED, value);
			}
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x00022880 File Offset: 0x00020A80
		private void OnInitialized()
		{
			EventHandler eventHandler = (EventHandler)base.Events[BindingSource.EVENT_INITIALIZED];
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		/// <summary>Retrieves an enumerator for the <see cref="P:System.Windows.Forms.BindingSource.List" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="P:System.Windows.Forms.BindingSource.List" />.</returns>
		// Token: 0x06000C10 RID: 3088 RVA: 0x000228B2 File Offset: 0x00020AB2
		public virtual IEnumerator GetEnumerator()
		{
			return this.List.GetEnumerator();
		}

		/// <summary>Copies the contents of the <see cref="P:System.Windows.Forms.BindingSource.List" /> to the specified array, starting at the specified index value.</summary>
		/// <param name="arr">The destination array.</param>
		/// <param name="index">The index in the destination array at which to start the copy operation.</param>
		// Token: 0x06000C11 RID: 3089 RVA: 0x000228BF File Offset: 0x00020ABF
		public virtual void CopyTo(Array arr, int index)
		{
			this.List.CopyTo(arr, index);
		}

		/// <summary>Gets the total number of items in the underlying list, taking the current <see cref="P:System.Windows.Forms.BindingSource.Filter" /> value into consideration.</summary>
		/// <returns>The total number of filtered items in the underlying list.</returns>
		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000C12 RID: 3090 RVA: 0x000228D0 File Offset: 0x00020AD0
		[Browsable(false)]
		public virtual int Count
		{
			get
			{
				int num;
				try
				{
					if (this.disposedOrFinalized)
					{
						num = 0;
					}
					else
					{
						if (this.recursionDetectionFlag)
						{
							throw new InvalidOperationException(SR.GetString("BindingSourceRecursionDetected"));
						}
						this.recursionDetectionFlag = true;
						num = this.List.Count;
					}
				}
				finally
				{
					this.recursionDetectionFlag = false;
				}
				return num;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> to indicate the list is synchronized; otherwise, <see langword="false" />.</returns>
		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000C13 RID: 3091 RVA: 0x00022930 File Offset: 0x00020B30
		[Browsable(false)]
		public virtual bool IsSynchronized
		{
			get
			{
				return this.List.IsSynchronized;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the underlying list.</summary>
		/// <returns>An object that can be used to synchronize access to the underlying list.</returns>
		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x0002293D File Offset: 0x00020B3D
		[Browsable(false)]
		public virtual object SyncRoot
		{
			get
			{
				return this.List.SyncRoot;
			}
		}

		/// <summary>Adds an existing item to the internal list.</summary>
		/// <param name="value">An <see cref="T:System.Object" /> to be added to the internal list.</param>
		/// <returns>The zero-based index at which <paramref name="value" /> was added to the underlying list represented by the <see cref="P:System.Windows.Forms.BindingSource.List" /> property.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="value" /> differs in type from the existing items in the underlying list.</exception>
		// Token: 0x06000C15 RID: 3093 RVA: 0x0002294C File Offset: 0x00020B4C
		public virtual int Add(object value)
		{
			if (this.dataSource == null && this.List.Count == 0)
			{
				this.SetList(BindingSource.CreateBindingList((value == null) ? typeof(object) : value.GetType()), true, true);
			}
			if (value != null && !this.itemType.IsAssignableFrom(value.GetType()))
			{
				throw new InvalidOperationException(SR.GetString("BindingSourceItemTypeMismatchOnAdd"));
			}
			if (value == null && this.itemType.IsValueType)
			{
				throw new InvalidOperationException(SR.GetString("BindingSourceItemTypeIsValueType"));
			}
			int num = this.List.Add(value);
			this.OnSimpleListChanged(ListChangedType.ItemAdded, num);
			return num;
		}

		/// <summary>Removes all elements from the list.</summary>
		// Token: 0x06000C16 RID: 3094 RVA: 0x000229EE File Offset: 0x00020BEE
		public virtual void Clear()
		{
			this.UnhookItemChangedEventsForOldCurrent();
			this.List.Clear();
			this.OnSimpleListChanged(ListChangedType.Reset, -1);
		}

		/// <summary>Determines whether an object is an item in the list.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the underlying list represented by the <see cref="P:System.Windows.Forms.BindingSource.List" /> property. The value can be <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="value" /> parameter is found in the <see cref="P:System.Windows.Forms.BindingSource.List" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000C17 RID: 3095 RVA: 0x00022A09 File Offset: 0x00020C09
		public virtual bool Contains(object value)
		{
			return this.List.Contains(value);
		}

		/// <summary>Searches for the specified object and returns the index of the first occurrence within the entire list.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the underlying list represented by the <see cref="P:System.Windows.Forms.BindingSource.List" /> property. The value can be <see langword="null" />.</param>
		/// <returns>The zero-based index of the first occurrence of the <paramref name="value" /> parameter; otherwise, -1 if <paramref name="value" /> is not in the list.</returns>
		// Token: 0x06000C18 RID: 3096 RVA: 0x00022A17 File Offset: 0x00020C17
		public virtual int IndexOf(object value)
		{
			return this.List.IndexOf(value);
		}

		/// <summary>Inserts an item into the list at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to insert. The value can be <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than <see cref="P:System.Windows.Forms.BindingSource.Count" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The list is read-only or has a fixed size.</exception>
		// Token: 0x06000C19 RID: 3097 RVA: 0x00022A25 File Offset: 0x00020C25
		public virtual void Insert(int index, object value)
		{
			this.List.Insert(index, value);
			this.OnSimpleListChanged(ListChangedType.ItemAdded, index);
		}

		/// <summary>Removes the specified item from the list.</summary>
		/// <param name="value">The item to remove from the underlying list represented by the <see cref="P:System.Windows.Forms.BindingSource.List" /> property.</param>
		/// <exception cref="T:System.NotSupportedException">The underlying list has a fixed size or is read-only.</exception>
		// Token: 0x06000C1A RID: 3098 RVA: 0x00022A3C File Offset: 0x00020C3C
		public virtual void Remove(object value)
		{
			int num = ((IList)this).IndexOf(value);
			this.List.Remove(value);
			if (num != -1)
			{
				this.OnSimpleListChanged(ListChangedType.ItemDeleted, num);
			}
		}

		/// <summary>Removes the item at the specified index in the list.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the value of the <see cref="P:System.Windows.Forms.BindingSource.Count" /> property.</exception>
		/// <exception cref="T:System.NotSupportedException">The underlying list represented by the <see cref="P:System.Windows.Forms.BindingSource.List" /> property is read-only or has a fixed size.</exception>
		// Token: 0x06000C1B RID: 3099 RVA: 0x00022A6C File Offset: 0x00020C6C
		public virtual void RemoveAt(int index)
		{
			object obj = ((IList)this)[index];
			this.List.RemoveAt(index);
			this.OnSimpleListChanged(ListChangedType.ItemDeleted, index);
		}

		/// <summary>Gets or sets the list element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to retrieve.</param>
		/// <returns>The element at the specified index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or is equal to or greater than <see cref="P:System.Windows.Forms.BindingSource.Count" />.</exception>
		// Token: 0x170002EF RID: 751
		[Browsable(false)]
		public virtual object this[int index]
		{
			get
			{
				return this.List[index];
			}
			set
			{
				this.List[index] = value;
				if (!this.isBindingList)
				{
					this.OnSimpleListChanged(ListChangedType.ItemChanged, index);
				}
			}
		}

		/// <summary>Gets a value indicating whether the underlying list has a fixed size.</summary>
		/// <returns>
		///   <see langword="true" /> if the underlying list has a fixed size; otherwise, <see langword="false" />.</returns>
		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000C1E RID: 3102 RVA: 0x00022AC2 File Offset: 0x00020CC2
		[Browsable(false)]
		public virtual bool IsFixedSize
		{
			get
			{
				return this.List.IsFixedSize;
			}
		}

		/// <summary>Gets a value indicating whether the underlying list is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the list is read-only; otherwise, <see langword="false" />.</returns>
		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000C1F RID: 3103 RVA: 0x00022ACF File Offset: 0x00020CCF
		[Browsable(false)]
		public virtual bool IsReadOnly
		{
			get
			{
				return this.List.IsReadOnly;
			}
		}

		/// <summary>Gets the name of the list supplying data for the binding.</summary>
		/// <param name="listAccessors">An array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects to find in the list as bindable.</param>
		/// <returns>The name of the list supplying the data for binding.</returns>
		// Token: 0x06000C20 RID: 3104 RVA: 0x00022ADC File Offset: 0x00020CDC
		public virtual string GetListName(PropertyDescriptor[] listAccessors)
		{
			return ListBindingHelper.GetListName(this.List, listAccessors);
		}

		/// <summary>Retrieves an array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects representing the bindable properties of the data source list type.</summary>
		/// <param name="listAccessors">An array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects to find in the list as bindable.</param>
		/// <returns>An array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects that represents the properties on this list type used to bind data.</returns>
		// Token: 0x06000C21 RID: 3105 RVA: 0x00022AEC File Offset: 0x00020CEC
		public virtual PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
		{
			object list = ListBindingHelper.GetList(this.dataSource);
			if (list is ITypedList && !string.IsNullOrEmpty(this.dataMember))
			{
				return ListBindingHelper.GetListItemProperties(list, this.dataMember, listAccessors);
			}
			return ListBindingHelper.GetListItemProperties(this.List, listAccessors);
		}

		/// <summary>Adds a new item to the underlying list.</summary>
		/// <returns>The <see cref="T:System.Object" /> that was created and added to the list.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.BindingSource.AllowNew" /> property is set to <see langword="false" />.  
		///  -or-  
		///  A public default constructor could not be found for the current item type.</exception>
		// Token: 0x06000C22 RID: 3106 RVA: 0x00022B34 File Offset: 0x00020D34
		public virtual object AddNew()
		{
			if (!this.AllowNewInternal(false))
			{
				throw new InvalidOperationException(SR.GetString("BindingSourceBindingListWrapperAddToReadOnlyList"));
			}
			if (!this.AllowNewInternal(true))
			{
				throw new InvalidOperationException(SR.GetString("BindingSourceBindingListWrapperNeedToSetAllowNew", new object[] { (this.itemType == null) ? "(null)" : this.itemType.FullName }));
			}
			int num = this.addNewPos;
			this.EndEdit();
			if (num != -1)
			{
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, num));
			}
			AddingNewEventArgs addingNewEventArgs = new AddingNewEventArgs();
			int count = this.List.Count;
			this.OnAddingNew(addingNewEventArgs);
			object obj = addingNewEventArgs.NewObject;
			if (obj == null)
			{
				if (this.isBindingList)
				{
					obj = (this.List as IBindingList).AddNew();
					this.Position = this.Count - 1;
					return obj;
				}
				if (this.itemConstructor == null)
				{
					throw new InvalidOperationException(SR.GetString("BindingSourceBindingListWrapperNeedAParameterlessConstructor", new object[] { (this.itemType == null) ? "(null)" : this.itemType.FullName }));
				}
				obj = this.itemConstructor.Invoke(null);
			}
			if (this.List.Count > count)
			{
				this.addNewPos = this.Position;
			}
			else
			{
				this.addNewPos = this.Add(obj);
				this.Position = this.addNewPos;
			}
			return obj;
		}

		/// <summary>Gets a value indicating whether items in the underlying list can be edited.</summary>
		/// <returns>
		///   <see langword="true" /> to indicate list items can be edited; otherwise, <see langword="false" />.</returns>
		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000C23 RID: 3107 RVA: 0x00022C90 File Offset: 0x00020E90
		[Browsable(false)]
		public virtual bool AllowEdit
		{
			get
			{
				if (this.isBindingList)
				{
					return ((IBindingList)this.List).AllowEdit;
				}
				return !this.List.IsReadOnly;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="M:System.Windows.Forms.BindingSource.AddNew" /> method can be used to add items to the list.</summary>
		/// <returns>
		///   <see langword="true" /> if <see cref="M:System.Windows.Forms.BindingSource.AddNew" /> can be used to add items to the list; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">This property is set to <see langword="true" /> when the underlying list represented by the <see cref="P:System.Windows.Forms.BindingSource.List" /> property has a fixed size or is read-only.</exception>
		/// <exception cref="T:System.MissingMethodException">The property is set to <see langword="true" /> and the <see cref="E:System.Windows.Forms.BindingSource.AddingNew" /> event is not handled when the underlying list type does not have a default constructor.</exception>
		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x00022CB9 File Offset: 0x00020EB9
		// (set) Token: 0x06000C25 RID: 3109 RVA: 0x00022CC4 File Offset: 0x00020EC4
		[SRCategory("CatBehavior")]
		[SRDescription("BindingSourceAllowNewDescr")]
		public virtual bool AllowNew
		{
			get
			{
				return this.AllowNewInternal(true);
			}
			set
			{
				if (this.allowNewIsSet && value == this.allowNewSetValue)
				{
					return;
				}
				if (value && !this.isBindingList && !this.IsListWriteable(false))
				{
					throw new InvalidOperationException(SR.GetString("NoAllowNewOnReadOnlyList"));
				}
				this.allowNewIsSet = true;
				this.allowNewSetValue = value;
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
			}
		}

		/// <summary>Gets a value indicating whether items can be removed from the underlying list.</summary>
		/// <returns>
		///   <see langword="true" /> to indicate list items can be removed from the list; otherwise, <see langword="false" />.</returns>
		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x00022D22 File Offset: 0x00020F22
		[Browsable(false)]
		public virtual bool AllowRemove
		{
			get
			{
				if (this.isBindingList)
				{
					return ((IBindingList)this.List).AllowRemove;
				}
				return !this.List.IsReadOnly && !this.List.IsFixedSize;
			}
		}

		/// <summary>Gets a value indicating whether the data source supports change notification.</summary>
		/// <returns>
		///   <see langword="true" /> in all cases.</returns>
		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x00012E4E File Offset: 0x0001104E
		[Browsable(false)]
		public virtual bool SupportsChangeNotification
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether the data source supports searching with the <see cref="M:System.Windows.Forms.BindingSource.Find(System.ComponentModel.PropertyDescriptor,System.Object)" /> method.</summary>
		/// <returns>
		///   <see langword="true" /> if the list is a <see cref="T:System.ComponentModel.IBindingList" /> and supports the searching with the <see cref="Overload:System.Windows.Forms.BindingSource.Find" /> method; otherwise, <see langword="false" />.</returns>
		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x00022D5A File Offset: 0x00020F5A
		[Browsable(false)]
		public virtual bool SupportsSearching
		{
			get
			{
				return this.isBindingList && ((IBindingList)this.List).SupportsSearching;
			}
		}

		/// <summary>Gets a value indicating whether the data source supports sorting.</summary>
		/// <returns>
		///   <see langword="true" /> if the data source is an <see cref="T:System.ComponentModel.IBindingList" /> and supports sorting; otherwise, <see langword="false" />.</returns>
		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000C29 RID: 3113 RVA: 0x00022D76 File Offset: 0x00020F76
		[Browsable(false)]
		public virtual bool SupportsSorting
		{
			get
			{
				return this.isBindingList && ((IBindingList)this.List).SupportsSorting;
			}
		}

		/// <summary>Gets a value indicating whether the items in the underlying list are sorted.</summary>
		/// <returns>
		///   <see langword="true" /> if the list is an <see cref="T:System.ComponentModel.IBindingList" /> and is sorted; otherwise, <see langword="false" />.</returns>
		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x00022D92 File Offset: 0x00020F92
		[Browsable(false)]
		public virtual bool IsSorted
		{
			get
			{
				return this.isBindingList && ((IBindingList)this.List).IsSorted;
			}
		}

		/// <summary>Gets the <see cref="T:System.ComponentModel.PropertyDescriptor" /> that is being used for sorting the list.</summary>
		/// <returns>If the list is an <see cref="T:System.ComponentModel.IBindingList" />, the <see cref="T:System.ComponentModel.PropertyDescriptor" /> that is being used for sorting; otherwise, <see langword="null" />.</returns>
		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000C2B RID: 3115 RVA: 0x00022DAE File Offset: 0x00020FAE
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual PropertyDescriptor SortProperty
		{
			get
			{
				if (this.isBindingList)
				{
					return ((IBindingList)this.List).SortProperty;
				}
				return null;
			}
		}

		/// <summary>Gets the direction the items in the list are sorted.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values indicating the direction the list is sorted.</returns>
		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x00022DCA File Offset: 0x00020FCA
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual ListSortDirection SortDirection
		{
			get
			{
				if (this.isBindingList)
				{
					return ((IBindingList)this.List).SortDirection;
				}
				return ListSortDirection.Ascending;
			}
		}

		/// <summary>Adds the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to the indexes used for searching.</summary>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to add to the indexes used for searching.</param>
		/// <exception cref="T:System.NotSupportedException">The underlying list is not an <see cref="T:System.ComponentModel.IBindingList" />.</exception>
		// Token: 0x06000C2D RID: 3117 RVA: 0x00022DE6 File Offset: 0x00020FE6
		void IBindingList.AddIndex(PropertyDescriptor property)
		{
			if (this.isBindingList)
			{
				((IBindingList)this.List).AddIndex(property);
				return;
			}
			throw new NotSupportedException(SR.GetString("OperationRequiresIBindingList"));
		}

		/// <summary>Sorts the data source using the specified property descriptor and sort direction.</summary>
		/// <param name="property">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that describes the property by which to sort the data source.</param>
		/// <param name="sort">A <see cref="T:System.ComponentModel.ListSortDirection" /> indicating how the list should be sorted.</param>
		/// <exception cref="T:System.NotSupportedException">The data source is not an <see cref="T:System.ComponentModel.IBindingList" />.</exception>
		// Token: 0x06000C2E RID: 3118 RVA: 0x00022E11 File Offset: 0x00021011
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ApplySort(PropertyDescriptor property, ListSortDirection sort)
		{
			if (this.isBindingList)
			{
				((IBindingList)this.List).ApplySort(property, sort);
				return;
			}
			throw new NotSupportedException(SR.GetString("OperationRequiresIBindingList"));
		}

		/// <summary>Searches for the index of the item that has the given property descriptor.</summary>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to search for.</param>
		/// <param name="key">The value of <paramref name="prop" /> to match.</param>
		/// <returns>The zero-based index of the item that has the given value for <see cref="T:System.ComponentModel.PropertyDescriptor" />.</returns>
		/// <exception cref="T:System.NotSupportedException">The underlying list is not of type <see cref="T:System.ComponentModel.IBindingList" />.</exception>
		// Token: 0x06000C2F RID: 3119 RVA: 0x00022E3D File Offset: 0x0002103D
		public virtual int Find(PropertyDescriptor prop, object key)
		{
			if (this.isBindingList)
			{
				return ((IBindingList)this.List).Find(prop, key);
			}
			throw new NotSupportedException(SR.GetString("OperationRequiresIBindingList"));
		}

		/// <summary>Removes the <see cref="T:System.ComponentModel.PropertyDescriptor" /> from the indexes used for searching.</summary>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to remove from the indexes used for searching.</param>
		// Token: 0x06000C30 RID: 3120 RVA: 0x00022E69 File Offset: 0x00021069
		void IBindingList.RemoveIndex(PropertyDescriptor prop)
		{
			if (this.isBindingList)
			{
				((IBindingList)this.List).RemoveIndex(prop);
				return;
			}
			throw new NotSupportedException(SR.GetString("OperationRequiresIBindingList"));
		}

		/// <summary>Removes the sort associated with the <see cref="T:System.Windows.Forms.BindingSource" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The underlying list does not support sorting.</exception>
		// Token: 0x06000C31 RID: 3121 RVA: 0x00022E94 File Offset: 0x00021094
		public virtual void RemoveSort()
		{
			this.sort = null;
			if (this.isBindingList)
			{
				((IBindingList)this.List).RemoveSort();
			}
		}

		/// <summary>Sorts the data source with the specified sort descriptions.</summary>
		/// <param name="sorts">A <see cref="T:System.ComponentModel.ListSortDescriptionCollection" /> containing the sort descriptions to apply to the data source.</param>
		/// <exception cref="T:System.NotSupportedException">The data source is not an <see cref="T:System.ComponentModel.IBindingListView" />.</exception>
		// Token: 0x06000C32 RID: 3122 RVA: 0x00022EB8 File Offset: 0x000210B8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ApplySort(ListSortDescriptionCollection sorts)
		{
			IBindingListView bindingListView = this.List as IBindingListView;
			if (bindingListView != null)
			{
				bindingListView.ApplySort(sorts);
				return;
			}
			throw new NotSupportedException(SR.GetString("OperationRequiresIBindingListView"));
		}

		/// <summary>Gets the collection of sort descriptions applied to the data source.</summary>
		/// <returns>If the data source is an <see cref="T:System.ComponentModel.IBindingListView" />, a <see cref="T:System.ComponentModel.ListSortDescriptionCollection" /> that contains the sort descriptions applied to the list; otherwise, <see langword="null" />.</returns>
		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x00022EEC File Offset: 0x000210EC
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual ListSortDescriptionCollection SortDescriptions
		{
			get
			{
				IBindingListView bindingListView = this.List as IBindingListView;
				if (bindingListView != null)
				{
					return bindingListView.SortDescriptions;
				}
				return null;
			}
		}

		/// <summary>Gets or sets the expression used to filter which rows are viewed.</summary>
		/// <returns>A string that specifies how rows are to be filtered. The default is <see langword="null" />.</returns>
		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x00022F10 File Offset: 0x00021110
		// (set) Token: 0x06000C35 RID: 3125 RVA: 0x00022F18 File Offset: 0x00021118
		[SRCategory("CatData")]
		[DefaultValue(null)]
		[SRDescription("BindingSourceFilterDescr")]
		public virtual string Filter
		{
			get
			{
				return this.filter;
			}
			set
			{
				this.filter = value;
				this.InnerListFilter = value;
			}
		}

		/// <summary>Removes the filter associated with the <see cref="T:System.Windows.Forms.BindingSource" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The underlying list does not support filtering.</exception>
		// Token: 0x06000C36 RID: 3126 RVA: 0x00022F28 File Offset: 0x00021128
		public virtual void RemoveFilter()
		{
			this.filter = null;
			IBindingListView bindingListView = this.List as IBindingListView;
			if (bindingListView != null)
			{
				bindingListView.RemoveFilter();
			}
		}

		/// <summary>Gets a value indicating whether the data source supports multi-column sorting.</summary>
		/// <returns>
		///   <see langword="true" /> if the list is an <see cref="T:System.ComponentModel.IBindingListView" /> and supports multi-column sorting; otherwise, <see langword="false" />.</returns>
		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000C37 RID: 3127 RVA: 0x00022F54 File Offset: 0x00021154
		[Browsable(false)]
		public virtual bool SupportsAdvancedSorting
		{
			get
			{
				IBindingListView bindingListView = this.List as IBindingListView;
				return bindingListView != null && bindingListView.SupportsAdvancedSorting;
			}
		}

		/// <summary>Gets a value indicating whether the data source supports filtering.</summary>
		/// <returns>
		///   <see langword="true" /> if the list is an <see cref="T:System.ComponentModel.IBindingListView" /> and supports filtering; otherwise, <see langword="false" />.</returns>
		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000C38 RID: 3128 RVA: 0x00022F78 File Offset: 0x00021178
		[Browsable(false)]
		public virtual bool SupportsFiltering
		{
			get
			{
				IBindingListView bindingListView = this.List as IBindingListView;
				return bindingListView != null && bindingListView.SupportsFiltering;
			}
		}

		/// <summary>Discards a pending new item from the collection.</summary>
		/// <param name="position">The index of the item that was added to the collection.</param>
		// Token: 0x06000C39 RID: 3129 RVA: 0x00022F9C File Offset: 0x0002119C
		void ICancelAddNew.CancelNew(int position)
		{
			if (this.addNewPos >= 0 && this.addNewPos == position)
			{
				this.RemoveAt(this.addNewPos);
				this.addNewPos = -1;
				return;
			}
			ICancelAddNew cancelAddNew = this.List as ICancelAddNew;
			if (cancelAddNew != null)
			{
				cancelAddNew.CancelNew(position);
			}
		}

		/// <summary>Commits a pending new item to the collection.</summary>
		/// <param name="position">The index of the item that was added to the collection.</param>
		// Token: 0x06000C3A RID: 3130 RVA: 0x00022FE8 File Offset: 0x000211E8
		void ICancelAddNew.EndNew(int position)
		{
			if (this.addNewPos >= 0 && this.addNewPos == position)
			{
				this.addNewPos = -1;
				return;
			}
			ICancelAddNew cancelAddNew = this.List as ICancelAddNew;
			if (cancelAddNew != null)
			{
				cancelAddNew.EndNew(position);
			}
		}

		// Token: 0x040006CF RID: 1743
		private static readonly object EVENT_ADDINGNEW = new object();

		// Token: 0x040006D0 RID: 1744
		private static readonly object EVENT_BINDINGCOMPLETE = new object();

		// Token: 0x040006D1 RID: 1745
		private static readonly object EVENT_CURRENTCHANGED = new object();

		// Token: 0x040006D2 RID: 1746
		private static readonly object EVENT_CURRENTITEMCHANGED = new object();

		// Token: 0x040006D3 RID: 1747
		private static readonly object EVENT_DATAERROR = new object();

		// Token: 0x040006D4 RID: 1748
		private static readonly object EVENT_DATAMEMBERCHANGED = new object();

		// Token: 0x040006D5 RID: 1749
		private static readonly object EVENT_DATASOURCECHANGED = new object();

		// Token: 0x040006D6 RID: 1750
		private static readonly object EVENT_LISTCHANGED = new object();

		// Token: 0x040006D7 RID: 1751
		private static readonly object EVENT_POSITIONCHANGED = new object();

		// Token: 0x040006D8 RID: 1752
		private static readonly object EVENT_INITIALIZED = new object();

		// Token: 0x040006D9 RID: 1753
		private object dataSource;

		// Token: 0x040006DA RID: 1754
		private string dataMember = string.Empty;

		// Token: 0x040006DB RID: 1755
		private string sort;

		// Token: 0x040006DC RID: 1756
		private string filter;

		// Token: 0x040006DD RID: 1757
		private CurrencyManager currencyManager;

		// Token: 0x040006DE RID: 1758
		private bool raiseListChangedEvents = true;

		// Token: 0x040006DF RID: 1759
		private bool parentsCurrentItemChanging;

		// Token: 0x040006E0 RID: 1760
		private bool disposedOrFinalized;

		// Token: 0x040006E1 RID: 1761
		private IList _innerList;

		// Token: 0x040006E2 RID: 1762
		private bool isBindingList;

		// Token: 0x040006E3 RID: 1763
		private bool listRaisesItemChangedEvents;

		// Token: 0x040006E4 RID: 1764
		private bool listExtractedFromEnumerable;

		// Token: 0x040006E5 RID: 1765
		private Type itemType;

		// Token: 0x040006E6 RID: 1766
		private ConstructorInfo itemConstructor;

		// Token: 0x040006E7 RID: 1767
		private PropertyDescriptorCollection itemShape;

		// Token: 0x040006E8 RID: 1768
		private Dictionary<string, BindingSource> relatedBindingSources;

		// Token: 0x040006E9 RID: 1769
		private bool allowNewIsSet;

		// Token: 0x040006EA RID: 1770
		private bool allowNewSetValue = true;

		// Token: 0x040006EB RID: 1771
		private object currentItemHookedForItemChange;

		// Token: 0x040006EC RID: 1772
		private object lastCurrentItem;

		// Token: 0x040006ED RID: 1773
		private EventHandler listItemPropertyChangedHandler;

		// Token: 0x040006EE RID: 1774
		private int addNewPos = -1;

		// Token: 0x040006EF RID: 1775
		private bool initializing;

		// Token: 0x040006F0 RID: 1776
		private bool needToSetList;

		// Token: 0x040006F1 RID: 1777
		private bool recursionDetectionFlag;

		// Token: 0x040006F2 RID: 1778
		private bool innerListChanging;

		// Token: 0x040006F3 RID: 1779
		private bool endingEdit;
	}
}
