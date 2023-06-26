using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides a common implementation of members for the <see cref="T:System.Windows.Forms.ListBox" /> and <see cref="T:System.Windows.Forms.ComboBox" /> classes.</summary>
	// Token: 0x020002CC RID: 716
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[LookupBindingProperties("DataSource", "DisplayMember", "ValueMember", "SelectedValue")]
	public abstract class ListControl : Control
	{
		/// <summary>Gets or sets the data source for this <see cref="T:System.Windows.Forms.ListControl" />.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IList" /> or <see cref="T:System.ComponentModel.IListSource" /> interfaces, such as a <see cref="T:System.Data.DataSet" /> or an <see cref="T:System.Array" />. The default is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">The assigned value does not implement the <see cref="T:System.Collections.IList" /> or <see cref="T:System.ComponentModel.IListSource" /> interfaces.</exception>
		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06002C67 RID: 11367 RVA: 0x000C799A File Offset: 0x000C5B9A
		// (set) Token: 0x06002C68 RID: 11368 RVA: 0x000C79A4 File Offset: 0x000C5BA4
		[SRCategory("CatData")]
		[DefaultValue(null)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[AttributeProvider(typeof(IListSource))]
		[SRDescription("ListControlDataSourceDescr")]
		public object DataSource
		{
			get
			{
				return this.dataSource;
			}
			set
			{
				if (value != null && !(value is IList) && !(value is IListSource))
				{
					throw new ArgumentException(SR.GetString("BadDataSourceForComplexBinding"));
				}
				if (this.dataSource == value)
				{
					return;
				}
				try
				{
					this.SetDataConnection(value, this.displayMember, false);
				}
				catch
				{
					this.DisplayMember = "";
				}
				if (value == null)
				{
					this.DisplayMember = "";
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ListControl.DataSource" /> changes.</summary>
		// Token: 0x14000200 RID: 512
		// (add) Token: 0x06002C69 RID: 11369 RVA: 0x000C7A1C File Offset: 0x000C5C1C
		// (remove) Token: 0x06002C6A RID: 11370 RVA: 0x000C7A2F File Offset: 0x000C5C2F
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ListControlOnDataSourceChangedDescr")]
		public event EventHandler DataSourceChanged
		{
			add
			{
				base.Events.AddHandler(ListControl.EVENT_DATASOURCECHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListControl.EVENT_DATASOURCECHANGED, value);
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.CurrencyManager" /> associated with this control.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.CurrencyManager" /> associated with this control. The default is <see langword="null" />.</returns>
		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06002C6B RID: 11371 RVA: 0x000C7A42 File Offset: 0x000C5C42
		protected CurrencyManager DataManager
		{
			get
			{
				return this.dataManager;
			}
		}

		/// <summary>Gets or sets the property to display for this <see cref="T:System.Windows.Forms.ListControl" />.</summary>
		/// <returns>A <see cref="T:System.String" /> specifying the name of an object property that is contained in the collection specified by the <see cref="P:System.Windows.Forms.ListControl.DataSource" /> property. The default is an empty string ("").</returns>
		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06002C6C RID: 11372 RVA: 0x000C7A4A File Offset: 0x000C5C4A
		// (set) Token: 0x06002C6D RID: 11373 RVA: 0x000C7A58 File Offset: 0x000C5C58
		[SRCategory("CatData")]
		[DefaultValue("")]
		[TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[SRDescription("ListControlDisplayMemberDescr")]
		public string DisplayMember
		{
			get
			{
				return this.displayMember.BindingMember;
			}
			set
			{
				BindingMemberInfo bindingMemberInfo = this.displayMember;
				try
				{
					this.SetDataConnection(this.dataSource, new BindingMemberInfo(value), false);
				}
				catch
				{
					this.displayMember = bindingMemberInfo;
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ListControl.DisplayMember" /> property changes.</summary>
		// Token: 0x14000201 RID: 513
		// (add) Token: 0x06002C6E RID: 11374 RVA: 0x000C7A9C File Offset: 0x000C5C9C
		// (remove) Token: 0x06002C6F RID: 11375 RVA: 0x000C7AAF File Offset: 0x000C5CAF
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ListControlOnDisplayMemberChangedDescr")]
		public event EventHandler DisplayMemberChanged
		{
			add
			{
				base.Events.AddHandler(ListControl.EVENT_DISPLAYMEMBERCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListControl.EVENT_DISPLAYMEMBERCHANGED, value);
			}
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x06002C70 RID: 11376 RVA: 0x000C7AC4 File Offset: 0x000C5CC4
		private TypeConverter DisplayMemberConverter
		{
			get
			{
				if (this.displayMemberConverter == null && this.DataManager != null)
				{
					BindingMemberInfo bindingMemberInfo = this.displayMember;
					PropertyDescriptorCollection itemProperties = this.DataManager.GetItemProperties();
					if (itemProperties != null)
					{
						PropertyDescriptor propertyDescriptor = itemProperties.Find(this.displayMember.BindingField, true);
						if (propertyDescriptor != null)
						{
							this.displayMemberConverter = propertyDescriptor.Converter;
						}
					}
				}
				return this.displayMemberConverter;
			}
		}

		/// <summary>Occurs when the control is bound to a data value.</summary>
		// Token: 0x14000202 RID: 514
		// (add) Token: 0x06002C71 RID: 11377 RVA: 0x000C7B1F File Offset: 0x000C5D1F
		// (remove) Token: 0x06002C72 RID: 11378 RVA: 0x000C7B38 File Offset: 0x000C5D38
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ListControlFormatDescr")]
		public event ListControlConvertEventHandler Format
		{
			add
			{
				base.Events.AddHandler(ListControl.EVENT_FORMAT, value);
				this.RefreshItems();
			}
			remove
			{
				base.Events.RemoveHandler(ListControl.EVENT_FORMAT, value);
				this.RefreshItems();
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.IFormatProvider" /> that provides custom formatting behavior.</summary>
		/// <returns>The <see cref="T:System.IFormatProvider" /> implementation that provides custom formatting behavior.</returns>
		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06002C73 RID: 11379 RVA: 0x000C7B51 File Offset: 0x000C5D51
		// (set) Token: 0x06002C74 RID: 11380 RVA: 0x000C7B59 File Offset: 0x000C5D59
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DefaultValue(null)]
		public IFormatProvider FormatInfo
		{
			get
			{
				return this.formatInfo;
			}
			set
			{
				if (value != this.formatInfo)
				{
					this.formatInfo = value;
					this.RefreshItems();
					this.OnFormatInfoChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ListControl.FormatInfo" /> property changes.</summary>
		// Token: 0x14000203 RID: 515
		// (add) Token: 0x06002C75 RID: 11381 RVA: 0x000C7B7C File Offset: 0x000C5D7C
		// (remove) Token: 0x06002C76 RID: 11382 RVA: 0x000C7B8F File Offset: 0x000C5D8F
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ListControlFormatInfoChangedDescr")]
		public event EventHandler FormatInfoChanged
		{
			add
			{
				base.Events.AddHandler(ListControl.EVENT_FORMATINFOCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListControl.EVENT_FORMATINFOCHANGED, value);
			}
		}

		/// <summary>Gets or sets the format-specifier characters that indicate how a value is to be displayed.</summary>
		/// <returns>The string of format-specifier characters that indicates how a value is to be displayed.</returns>
		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06002C77 RID: 11383 RVA: 0x000C7BA2 File Offset: 0x000C5DA2
		// (set) Token: 0x06002C78 RID: 11384 RVA: 0x000C7BAA File Offset: 0x000C5DAA
		[DefaultValue("")]
		[SRDescription("ListControlFormatStringDescr")]
		[Editor("System.Windows.Forms.Design.FormatStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[MergableProperty(false)]
		public string FormatString
		{
			get
			{
				return this.formatString;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (!value.Equals(this.formatString))
				{
					this.formatString = value;
					this.RefreshItems();
					this.OnFormatStringChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when value of the <see cref="P:System.Windows.Forms.ListControl.FormatString" /> property changes</summary>
		// Token: 0x14000204 RID: 516
		// (add) Token: 0x06002C79 RID: 11385 RVA: 0x000C7BDC File Offset: 0x000C5DDC
		// (remove) Token: 0x06002C7A RID: 11386 RVA: 0x000C7BEF File Offset: 0x000C5DEF
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ListControlFormatStringChangedDescr")]
		public event EventHandler FormatStringChanged
		{
			add
			{
				base.Events.AddHandler(ListControl.EVENT_FORMATSTRINGCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListControl.EVENT_FORMATSTRINGCHANGED, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether formatting is applied to the <see cref="P:System.Windows.Forms.ListControl.DisplayMember" /> property of the <see cref="T:System.Windows.Forms.ListControl" />.</summary>
		/// <returns>
		///   <see langword="true" /> if formatting of the <see cref="P:System.Windows.Forms.ListControl.DisplayMember" /> property is enabled; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06002C7B RID: 11387 RVA: 0x000C7C02 File Offset: 0x000C5E02
		// (set) Token: 0x06002C7C RID: 11388 RVA: 0x000C7C0A File Offset: 0x000C5E0A
		[DefaultValue(false)]
		[SRDescription("ListControlFormattingEnabledDescr")]
		public bool FormattingEnabled
		{
			get
			{
				return this.formattingEnabled;
			}
			set
			{
				if (value != this.formattingEnabled)
				{
					this.formattingEnabled = value;
					this.RefreshItems();
					this.OnFormattingEnabledChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ListControl.FormattingEnabled" /> property changes.</summary>
		// Token: 0x14000205 RID: 517
		// (add) Token: 0x06002C7D RID: 11389 RVA: 0x000C7C2D File Offset: 0x000C5E2D
		// (remove) Token: 0x06002C7E RID: 11390 RVA: 0x000C7C40 File Offset: 0x000C5E40
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ListControlFormattingEnabledChangedDescr")]
		public event EventHandler FormattingEnabledChanged
		{
			add
			{
				base.Events.AddHandler(ListControl.EVENT_FORMATTINGENABLEDCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListControl.EVENT_FORMATTINGENABLEDCHANGED, value);
			}
		}

		// Token: 0x06002C7F RID: 11391 RVA: 0x000C7C54 File Offset: 0x000C5E54
		private bool BindingMemberInfoInDataManager(BindingMemberInfo bindingMemberInfo)
		{
			if (this.dataManager == null)
			{
				return false;
			}
			PropertyDescriptorCollection itemProperties = this.dataManager.GetItemProperties();
			int count = itemProperties.Count;
			for (int i = 0; i < count; i++)
			{
				if (!typeof(IList).IsAssignableFrom(itemProperties[i].PropertyType) && itemProperties[i].Name.Equals(bindingMemberInfo.BindingField))
				{
					return true;
				}
			}
			for (int j = 0; j < count; j++)
			{
				if (!typeof(IList).IsAssignableFrom(itemProperties[j].PropertyType) && string.Compare(itemProperties[j].Name, bindingMemberInfo.BindingField, true, CultureInfo.CurrentCulture) == 0)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Gets or sets the path of the property to use as the actual value for the items in the <see cref="T:System.Windows.Forms.ListControl" />.</summary>
		/// <returns>A <see cref="T:System.String" /> representing a single property name of the <see cref="P:System.Windows.Forms.ListControl.DataSource" /> property value, or a hierarchy of period-delimited property names that resolves to a property name of the final data-bound object. The default is an empty string ("").</returns>
		/// <exception cref="T:System.ArgumentException">The specified property path cannot be resolved through the object specified by the <see cref="P:System.Windows.Forms.ListControl.DataSource" /> property.</exception>
		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x06002C80 RID: 11392 RVA: 0x000C7D0F File Offset: 0x000C5F0F
		// (set) Token: 0x06002C81 RID: 11393 RVA: 0x000C7D1C File Offset: 0x000C5F1C
		[SRCategory("CatData")]
		[DefaultValue("")]
		[Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[SRDescription("ListControlValueMemberDescr")]
		public string ValueMember
		{
			get
			{
				return this.valueMember.BindingMember;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				BindingMemberInfo bindingMemberInfo = new BindingMemberInfo(value);
				BindingMemberInfo bindingMemberInfo2 = this.valueMember;
				if (!bindingMemberInfo.Equals(this.valueMember))
				{
					if (this.DisplayMember.Length == 0)
					{
						this.SetDataConnection(this.DataSource, bindingMemberInfo, false);
					}
					if (this.dataManager != null && value != null && value.Length != 0 && !this.BindingMemberInfoInDataManager(bindingMemberInfo))
					{
						throw new ArgumentException(SR.GetString("ListControlWrongValueMember"), "value");
					}
					this.valueMember = bindingMemberInfo;
					this.OnValueMemberChanged(EventArgs.Empty);
					this.OnSelectedValueChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ListControl.ValueMember" /> property changes.</summary>
		// Token: 0x14000206 RID: 518
		// (add) Token: 0x06002C82 RID: 11394 RVA: 0x000C7DC5 File Offset: 0x000C5FC5
		// (remove) Token: 0x06002C83 RID: 11395 RVA: 0x000C7DD8 File Offset: 0x000C5FD8
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ListControlOnValueMemberChangedDescr")]
		public event EventHandler ValueMemberChanged
		{
			add
			{
				base.Events.AddHandler(ListControl.EVENT_VALUEMEMBERCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListControl.EVENT_VALUEMEMBERCHANGED, value);
			}
		}

		/// <summary>Gets a value indicating whether the list enables selection of list items.</summary>
		/// <returns>
		///   <see langword="true" /> if the list enables list item selection; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06002C84 RID: 11396 RVA: 0x00012E4E File Offset: 0x0001104E
		protected virtual bool AllowSelection
		{
			get
			{
				return true;
			}
		}

		/// <summary>When overridden in a derived class, gets or sets the zero-based index of the currently selected item.</summary>
		/// <returns>A zero-based index of the currently selected item. A value of negative one (-1) is returned if no item is selected.</returns>
		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06002C85 RID: 11397
		// (set) Token: 0x06002C86 RID: 11398
		public abstract int SelectedIndex { get; set; }

		/// <summary>Gets or sets the value of the member property specified by the <see cref="P:System.Windows.Forms.ListControl.ValueMember" /> property.</summary>
		/// <returns>An object containing the value of the member of the data source specified by the <see cref="P:System.Windows.Forms.ListControl.ValueMember" /> property.</returns>
		/// <exception cref="T:System.InvalidOperationException">The assigned value is <see langword="null" /> or the empty string ("").</exception>
		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06002C87 RID: 11399 RVA: 0x000C7DEC File Offset: 0x000C5FEC
		// (set) Token: 0x06002C88 RID: 11400 RVA: 0x000C7E34 File Offset: 0x000C6034
		[SRCategory("CatData")]
		[DefaultValue(null)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ListControlSelectedValueDescr")]
		[Bindable(true)]
		public object SelectedValue
		{
			get
			{
				if (this.SelectedIndex != -1 && this.dataManager != null)
				{
					object obj = this.dataManager[this.SelectedIndex];
					return this.FilterItemOnProperty(obj, this.valueMember.BindingField);
				}
				return null;
			}
			set
			{
				if (this.dataManager != null)
				{
					string bindingField = this.valueMember.BindingField;
					if (string.IsNullOrEmpty(bindingField))
					{
						throw new InvalidOperationException(SR.GetString("ListControlEmptyValueMemberInSettingSelectedValue"));
					}
					PropertyDescriptorCollection itemProperties = this.dataManager.GetItemProperties();
					PropertyDescriptor propertyDescriptor = itemProperties.Find(bindingField, true);
					int num = this.dataManager.Find(propertyDescriptor, value, true);
					this.SelectedIndex = num;
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ListControl.SelectedValue" /> property changes.</summary>
		// Token: 0x14000207 RID: 519
		// (add) Token: 0x06002C89 RID: 11401 RVA: 0x000C7E98 File Offset: 0x000C6098
		// (remove) Token: 0x06002C8A RID: 11402 RVA: 0x000C7EAB File Offset: 0x000C60AB
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ListControlOnSelectedValueChangedDescr")]
		public event EventHandler SelectedValueChanged
		{
			add
			{
				base.Events.AddHandler(ListControl.EVENT_SELECTEDVALUECHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListControl.EVENT_SELECTEDVALUECHANGED, value);
			}
		}

		// Token: 0x06002C8B RID: 11403 RVA: 0x000C7EBE File Offset: 0x000C60BE
		private void DataManager_PositionChanged(object sender, EventArgs e)
		{
			if (this.dataManager != null && this.AllowSelection)
			{
				this.SelectedIndex = this.dataManager.Position;
			}
		}

		// Token: 0x06002C8C RID: 11404 RVA: 0x000C7EE4 File Offset: 0x000C60E4
		private void DataManager_ItemChanged(object sender, ItemChangedEventArgs e)
		{
			if (this.dataManager != null)
			{
				if (e.Index == -1)
				{
					this.SetItemsCore(this.dataManager.List);
					if (this.AllowSelection)
					{
						this.SelectedIndex = this.dataManager.Position;
						return;
					}
				}
				else
				{
					this.SetItemCore(e.Index, this.dataManager[e.Index]);
				}
			}
		}

		/// <summary>Retrieves the current value of the <see cref="T:System.Windows.Forms.ListControl" /> item, if it is a property of an object, given the item.</summary>
		/// <param name="item">The object the <see cref="T:System.Windows.Forms.ListControl" /> item is bound to.</param>
		/// <returns>The filtered object.</returns>
		// Token: 0x06002C8D RID: 11405 RVA: 0x000C7F4A File Offset: 0x000C614A
		protected object FilterItemOnProperty(object item)
		{
			return this.FilterItemOnProperty(item, this.displayMember.BindingField);
		}

		/// <summary>Returns the current value of the <see cref="T:System.Windows.Forms.ListControl" /> item, if it is a property of an object given the item and the property name.</summary>
		/// <param name="item">The object the <see cref="T:System.Windows.Forms.ListControl" /> item is bound to.</param>
		/// <param name="field">The property name of the item the <see cref="T:System.Windows.Forms.ListControl" /> is bound to.</param>
		/// <returns>The filtered object.</returns>
		// Token: 0x06002C8E RID: 11406 RVA: 0x000C7F60 File Offset: 0x000C6160
		protected object FilterItemOnProperty(object item, string field)
		{
			if (item != null && field.Length > 0)
			{
				try
				{
					PropertyDescriptor propertyDescriptor;
					if (this.dataManager != null)
					{
						propertyDescriptor = this.dataManager.GetItemProperties().Find(field, true);
					}
					else
					{
						propertyDescriptor = TypeDescriptor.GetProperties(item).Find(field, true);
					}
					if (propertyDescriptor != null)
					{
						item = propertyDescriptor.GetValue(item);
					}
				}
				catch
				{
				}
			}
			return item;
		}

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06002C8F RID: 11407 RVA: 0x000C7FC8 File Offset: 0x000C61C8
		internal bool BindingFieldEmpty
		{
			get
			{
				return this.displayMember.BindingField.Length <= 0;
			}
		}

		// Token: 0x06002C90 RID: 11408 RVA: 0x000C7FE0 File Offset: 0x000C61E0
		internal int FindStringInternal(string str, IList items, int startIndex, bool exact)
		{
			return this.FindStringInternal(str, items, startIndex, exact, true);
		}

		// Token: 0x06002C91 RID: 11409 RVA: 0x000C7FF0 File Offset: 0x000C61F0
		internal int FindStringInternal(string str, IList items, int startIndex, bool exact, bool ignorecase)
		{
			if (str == null || items == null)
			{
				return -1;
			}
			if (startIndex < -1 || startIndex >= items.Count)
			{
				return -1;
			}
			int length = str.Length;
			int i = 0;
			int num = (startIndex + 1) % items.Count;
			while (i < items.Count)
			{
				i++;
				bool flag;
				if (exact)
				{
					flag = string.Compare(str, this.GetItemText(items[num]), ignorecase, CultureInfo.CurrentCulture) == 0;
				}
				else
				{
					flag = string.Compare(str, 0, this.GetItemText(items[num]), 0, length, ignorecase, CultureInfo.CurrentCulture) == 0;
				}
				if (flag)
				{
					return num;
				}
				num = (num + 1) % items.Count;
			}
			return -1;
		}

		/// <summary>Returns the text representation of the specified item.</summary>
		/// <param name="item">The object from which to get the contents to display.</param>
		/// <returns>If the <see cref="P:System.Windows.Forms.ListControl.DisplayMember" /> property is not specified, the value returned by <see cref="M:System.Windows.Forms.ListControl.GetItemText(System.Object)" /> is the value of the item's <see langword="ToString" /> method. Otherwise, the method returns the string value of the member specified in the <see cref="P:System.Windows.Forms.ListControl.DisplayMember" /> property for the object specified in the <paramref name="item" /> parameter.</returns>
		// Token: 0x06002C92 RID: 11410 RVA: 0x000C8090 File Offset: 0x000C6290
		public string GetItemText(object item)
		{
			if (!this.formattingEnabled)
			{
				if (item == null)
				{
					return string.Empty;
				}
				item = this.FilterItemOnProperty(item, this.displayMember.BindingField);
				if (item == null)
				{
					return "";
				}
				return Convert.ToString(item, CultureInfo.CurrentCulture);
			}
			else
			{
				object obj = this.FilterItemOnProperty(item, this.displayMember.BindingField);
				ListControlConvertEventArgs listControlConvertEventArgs = new ListControlConvertEventArgs(obj, typeof(string), item);
				this.OnFormat(listControlConvertEventArgs);
				if (listControlConvertEventArgs.Value != item && listControlConvertEventArgs.Value is string)
				{
					return (string)listControlConvertEventArgs.Value;
				}
				if (ListControl.stringTypeConverter == null)
				{
					ListControl.stringTypeConverter = TypeDescriptor.GetConverter(typeof(string));
				}
				string text;
				try
				{
					text = (string)Formatter.FormatObject(obj, typeof(string), this.DisplayMemberConverter, ListControl.stringTypeConverter, this.formatString, this.formatInfo, null, DBNull.Value);
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
					text = ((obj != null) ? Convert.ToString(item, CultureInfo.CurrentCulture) : "");
				}
				return text;
			}
		}

		/// <summary>Handles special input keys, such as PAGE UP, PAGE DOWN, HOME, END, and so on.</summary>
		/// <param name="keyData">One of the values of <see cref="T:System.Windows.Forms.Keys" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="keyData" /> parameter specifies the <see cref="F:System.Windows.Forms.Keys.End" />, <see cref="F:System.Windows.Forms.Keys.Home" />, <see cref="F:System.Windows.Forms.Keys.PageUp" />, or <see cref="F:System.Windows.Forms.Keys.PageDown" /> key; <see langword="false" /> if the <paramref name="keyData" /> parameter specifies <see cref="F:System.Windows.Forms.Keys.Alt" />.</returns>
		// Token: 0x06002C93 RID: 11411 RVA: 0x000C81AC File Offset: 0x000C63AC
		protected override bool IsInputKey(Keys keyData)
		{
			if ((keyData & Keys.Alt) == Keys.Alt)
			{
				return false;
			}
			Keys keys = keyData & Keys.KeyCode;
			return keys - Keys.Prior <= 3 || base.IsInputKey(keyData);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BindingContextChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002C94 RID: 11412 RVA: 0x000C81E1 File Offset: 0x000C63E1
		protected override void OnBindingContextChanged(EventArgs e)
		{
			this.SetDataConnection(this.dataSource, this.displayMember, true);
			base.OnBindingContextChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListControl.DataSourceChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002C95 RID: 11413 RVA: 0x000C8200 File Offset: 0x000C6400
		protected virtual void OnDataSourceChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[ListControl.EVENT_DATASOURCECHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListControl.DisplayMemberChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002C96 RID: 11414 RVA: 0x000C8230 File Offset: 0x000C6430
		protected virtual void OnDisplayMemberChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[ListControl.EVENT_DISPLAYMEMBERCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListControl.Format" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ListControlConvertEventArgs" /> that contains the event data.</param>
		// Token: 0x06002C97 RID: 11415 RVA: 0x000C8260 File Offset: 0x000C6460
		protected virtual void OnFormat(ListControlConvertEventArgs e)
		{
			ListControlConvertEventHandler listControlConvertEventHandler = base.Events[ListControl.EVENT_FORMAT] as ListControlConvertEventHandler;
			if (listControlConvertEventHandler != null)
			{
				listControlConvertEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListControl.FormatInfoChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002C98 RID: 11416 RVA: 0x000C8290 File Offset: 0x000C6490
		protected virtual void OnFormatInfoChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[ListControl.EVENT_FORMATINFOCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListControl.FormatStringChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002C99 RID: 11417 RVA: 0x000C82C0 File Offset: 0x000C64C0
		protected virtual void OnFormatStringChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[ListControl.EVENT_FORMATSTRINGCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListControl.FormattingEnabledChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002C9A RID: 11418 RVA: 0x000C82F0 File Offset: 0x000C64F0
		protected virtual void OnFormattingEnabledChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[ListControl.EVENT_FORMATTINGENABLEDCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListControl.SelectedValueChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002C9B RID: 11419 RVA: 0x000C831E File Offset: 0x000C651E
		protected virtual void OnSelectedIndexChanged(EventArgs e)
		{
			this.OnSelectedValueChanged(EventArgs.Empty);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListControl.ValueMemberChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002C9C RID: 11420 RVA: 0x000C832C File Offset: 0x000C652C
		protected virtual void OnValueMemberChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[ListControl.EVENT_VALUEMEMBERCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListControl.SelectedValueChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002C9D RID: 11421 RVA: 0x000C835C File Offset: 0x000C655C
		protected virtual void OnSelectedValueChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[ListControl.EVENT_SELECTEDVALUECHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>When overridden in a derived class, resynchronizes the data of the object at the specified index with the contents of the data source.</summary>
		/// <param name="index">The zero-based index of the item whose data to refresh.</param>
		// Token: 0x06002C9E RID: 11422
		protected abstract void RefreshItem(int index);

		/// <summary>When overridden in a derived class, resynchronizes the item data with the contents of the data source.</summary>
		// Token: 0x06002C9F RID: 11423 RVA: 0x000070A6 File Offset: 0x000052A6
		protected virtual void RefreshItems()
		{
		}

		// Token: 0x06002CA0 RID: 11424 RVA: 0x000C838A File Offset: 0x000C658A
		private void DataSourceDisposed(object sender, EventArgs e)
		{
			this.SetDataConnection(null, new BindingMemberInfo(""), true);
		}

		// Token: 0x06002CA1 RID: 11425 RVA: 0x000C83A0 File Offset: 0x000C65A0
		private void DataSourceInitialized(object sender, EventArgs e)
		{
			ISupportInitializeNotification supportInitializeNotification = this.dataSource as ISupportInitializeNotification;
			this.SetDataConnection(this.dataSource, this.displayMember, true);
		}

		// Token: 0x06002CA2 RID: 11426 RVA: 0x000C83CC File Offset: 0x000C65CC
		private void SetDataConnection(object newDataSource, BindingMemberInfo newDisplayMember, bool force)
		{
			bool flag = this.dataSource != newDataSource;
			bool flag2 = !this.displayMember.Equals(newDisplayMember);
			if (this.inSetDataConnection)
			{
				return;
			}
			try
			{
				if (force || flag || flag2)
				{
					this.inSetDataConnection = true;
					IList list = ((this.DataManager != null) ? this.DataManager.List : null);
					bool flag3 = this.DataManager == null;
					this.UnwireDataSource();
					this.dataSource = newDataSource;
					this.displayMember = newDisplayMember;
					this.WireDataSource();
					if (this.isDataSourceInitialized)
					{
						CurrencyManager currencyManager = null;
						if (newDataSource != null && this.BindingContext != null && newDataSource != Convert.DBNull)
						{
							currencyManager = (CurrencyManager)this.BindingContext[newDataSource, newDisplayMember.BindingPath];
						}
						if (this.dataManager != currencyManager)
						{
							if (this.dataManager != null)
							{
								this.dataManager.ItemChanged -= this.DataManager_ItemChanged;
								this.dataManager.PositionChanged -= this.DataManager_PositionChanged;
							}
							this.dataManager = currencyManager;
							if (this.dataManager != null)
							{
								this.dataManager.ItemChanged += this.DataManager_ItemChanged;
								this.dataManager.PositionChanged += this.DataManager_PositionChanged;
							}
						}
						if (this.dataManager != null && (flag2 || flag) && this.displayMember.BindingMember != null && this.displayMember.BindingMember.Length != 0 && !this.BindingMemberInfoInDataManager(this.displayMember))
						{
							throw new ArgumentException(SR.GetString("ListControlWrongDisplayMember"), "newDisplayMember");
						}
						if (this.dataManager != null && (flag || flag2 || force) && (flag2 || (force && (list != this.dataManager.List || flag3))))
						{
							this.DataManager_ItemChanged(this.dataManager, new ItemChangedEventArgs(-1));
						}
					}
					this.displayMemberConverter = null;
				}
				if (flag)
				{
					this.OnDataSourceChanged(EventArgs.Empty);
				}
				if (flag2)
				{
					this.OnDisplayMemberChanged(EventArgs.Empty);
				}
			}
			finally
			{
				this.inSetDataConnection = false;
			}
		}

		// Token: 0x06002CA3 RID: 11427 RVA: 0x000C85E4 File Offset: 0x000C67E4
		private void UnwireDataSource()
		{
			if (this.dataSource is IComponent)
			{
				((IComponent)this.dataSource).Disposed -= this.DataSourceDisposed;
			}
			ISupportInitializeNotification supportInitializeNotification = this.dataSource as ISupportInitializeNotification;
			if (supportInitializeNotification != null && this.isDataSourceInitEventHooked)
			{
				supportInitializeNotification.Initialized -= this.DataSourceInitialized;
				this.isDataSourceInitEventHooked = false;
			}
		}

		// Token: 0x06002CA4 RID: 11428 RVA: 0x000C864C File Offset: 0x000C684C
		private void WireDataSource()
		{
			if (this.dataSource is IComponent)
			{
				((IComponent)this.dataSource).Disposed += this.DataSourceDisposed;
			}
			ISupportInitializeNotification supportInitializeNotification = this.dataSource as ISupportInitializeNotification;
			if (supportInitializeNotification != null && !supportInitializeNotification.IsInitialized)
			{
				supportInitializeNotification.Initialized += this.DataSourceInitialized;
				this.isDataSourceInitEventHooked = true;
				this.isDataSourceInitialized = false;
				return;
			}
			this.isDataSourceInitialized = true;
		}

		/// <summary>When overridden in a derived class, sets the specified array of objects in a collection in the derived class.</summary>
		/// <param name="items">An array of items.</param>
		// Token: 0x06002CA5 RID: 11429
		protected abstract void SetItemsCore(IList items);

		/// <summary>When overridden in a derived class, sets the object with the specified index in the derived class.</summary>
		/// <param name="index">The array index of the object.</param>
		/// <param name="value">The object.</param>
		// Token: 0x06002CA6 RID: 11430 RVA: 0x000070A6 File Offset: 0x000052A6
		protected virtual void SetItemCore(int index, object value)
		{
		}

		// Token: 0x04001277 RID: 4727
		private static readonly object EVENT_DATASOURCECHANGED = new object();

		// Token: 0x04001278 RID: 4728
		private static readonly object EVENT_DISPLAYMEMBERCHANGED = new object();

		// Token: 0x04001279 RID: 4729
		private static readonly object EVENT_VALUEMEMBERCHANGED = new object();

		// Token: 0x0400127A RID: 4730
		private static readonly object EVENT_SELECTEDVALUECHANGED = new object();

		// Token: 0x0400127B RID: 4731
		private static readonly object EVENT_FORMATINFOCHANGED = new object();

		// Token: 0x0400127C RID: 4732
		private static readonly object EVENT_FORMATSTRINGCHANGED = new object();

		// Token: 0x0400127D RID: 4733
		private static readonly object EVENT_FORMATTINGENABLEDCHANGED = new object();

		// Token: 0x0400127E RID: 4734
		private object dataSource;

		// Token: 0x0400127F RID: 4735
		private CurrencyManager dataManager;

		// Token: 0x04001280 RID: 4736
		private BindingMemberInfo displayMember;

		// Token: 0x04001281 RID: 4737
		private BindingMemberInfo valueMember;

		// Token: 0x04001282 RID: 4738
		private string formatString = string.Empty;

		// Token: 0x04001283 RID: 4739
		private IFormatProvider formatInfo;

		// Token: 0x04001284 RID: 4740
		private bool formattingEnabled;

		// Token: 0x04001285 RID: 4741
		private static readonly object EVENT_FORMAT = new object();

		// Token: 0x04001286 RID: 4742
		private TypeConverter displayMemberConverter;

		// Token: 0x04001287 RID: 4743
		private static TypeConverter stringTypeConverter = null;

		// Token: 0x04001288 RID: 4744
		private bool isDataSourceInitialized;

		// Token: 0x04001289 RID: 4745
		private bool isDataSourceInitEventHooked;

		// Token: 0x0400128A RID: 4746
		private bool inSetDataConnection;
	}
}
