using System;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	/// <summary>Represents the simple binding between the property value of an object and the property value of a control.</summary>
	// Token: 0x0200012E RID: 302
	[TypeConverter(typeof(ListBindingConverter))]
	public class Binding
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Binding" /> class that simple-binds the indicated control property to the specified data member of the data source.</summary>
		/// <param name="propertyName">The name of the control property to bind.</param>
		/// <param name="dataSource">An <see cref="T:System.Object" /> that represents the data source.</param>
		/// <param name="dataMember">The property or list to bind to.</param>
		/// <exception cref="T:System.Exception">
		///   <paramref name="propertyName" /> is neither a valid property of a control nor an empty string ("").</exception>
		/// <exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName" /> does not exist on the control.</exception>
		// Token: 0x06000AB6 RID: 2742 RVA: 0x0001E474 File Offset: 0x0001C674
		public Binding(string propertyName, object dataSource, string dataMember)
			: this(propertyName, dataSource, dataMember, false, DataSourceUpdateMode.OnValidation, null, string.Empty, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Binding" /> class that binds the indicated control property to the specified data member of the data source, and optionally enables formatting to be applied.</summary>
		/// <param name="propertyName">The name of the control property to bind.</param>
		/// <param name="dataSource">An <see cref="T:System.Object" /> that represents the data source.</param>
		/// <param name="dataMember">The property or list to bind to.</param>
		/// <param name="formattingEnabled">
		///   <see langword="true" /> to format the displayed data; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName" /> does not exist on the control.  
		///  -or-  
		///  The property given is a read-only property.</exception>
		/// <exception cref="T:System.Exception">Formatting is disabled and <paramref name="propertyName" /> is neither a valid property of a control nor an empty string ("").</exception>
		// Token: 0x06000AB7 RID: 2743 RVA: 0x0001E494 File Offset: 0x0001C694
		public Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled)
			: this(propertyName, dataSource, dataMember, formattingEnabled, DataSourceUpdateMode.OnValidation, null, string.Empty, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Binding" /> class that binds the specified control property to the specified data member of the specified data source. Optionally enables formatting and propagates values to the data source based on the specified update setting.</summary>
		/// <param name="propertyName">The name of the control property to bind.</param>
		/// <param name="dataSource">An <see cref="T:System.Object" /> representing the data source.</param>
		/// <param name="dataMember">The property or list to bind to.</param>
		/// <param name="formattingEnabled">
		///   <see langword="true" /> to format the displayed data; otherwise, <see langword="false" />.</param>
		/// <param name="dataSourceUpdateMode">One of the <see cref="T:System.Windows.Forms.DataSourceUpdateMode" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName" /> does not exist on the control.  
		///  -or-  
		///  The data source or data member or control property specified are associated with another binding in the collection.</exception>
		// Token: 0x06000AB8 RID: 2744 RVA: 0x0001E4B4 File Offset: 0x0001C6B4
		public Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode)
			: this(propertyName, dataSource, dataMember, formattingEnabled, dataSourceUpdateMode, null, string.Empty, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Binding" /> class that binds the indicated control property to the specified data member of the specified data source. Optionally enables formatting, propagates values to the data source based on the specified update setting, and sets the property to the specified value when a <see cref="T:System.DBNull" /> is returned from the data source.</summary>
		/// <param name="propertyName">The name of the control property to bind.</param>
		/// <param name="dataSource">An <see cref="T:System.Object" /> representing the data source.</param>
		/// <param name="dataMember">The property or list to bind to.</param>
		/// <param name="formattingEnabled">
		///   <see langword="true" /> to format the displayed data; otherwise, <see langword="false" />.</param>
		/// <param name="dataSourceUpdateMode">One of the <see cref="T:System.Windows.Forms.DataSourceUpdateMode" /> values.</param>
		/// <param name="nullValue">The <see cref="T:System.Object" /> to be applied to the bound control property if the data source value is <see cref="T:System.DBNull" />.</param>
		/// <exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName" /> does not exist on the control.  
		///  -or-  
		///  The data source or data member or control property specified are associated with another binding in the collection.</exception>
		// Token: 0x06000AB9 RID: 2745 RVA: 0x0001E4D8 File Offset: 0x0001C6D8
		public Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode, object nullValue)
			: this(propertyName, dataSource, dataMember, formattingEnabled, dataSourceUpdateMode, nullValue, string.Empty, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Binding" /> class that binds the specified control property to the specified data member of the specified data source. Optionally enables formatting with the specified format string; propagates values to the data source based on the specified update setting; and sets the property to the specified value when a <see cref="T:System.DBNull" /> is returned from the data source.</summary>
		/// <param name="propertyName">The name of the control property to bind.</param>
		/// <param name="dataSource">An <see cref="T:System.Object" /> representing the data source.</param>
		/// <param name="dataMember">The property or list to bind to.</param>
		/// <param name="formattingEnabled">
		///   <see langword="true" /> to format the displayed data; otherwise, <see langword="false" />.</param>
		/// <param name="dataSourceUpdateMode">One of the <see cref="T:System.Windows.Forms.DataSourceUpdateMode" /> values.</param>
		/// <param name="nullValue">The <see cref="T:System.Object" /> to be applied to the bound control property if the data source value is <see cref="T:System.DBNull" />.</param>
		/// <param name="formatString">One or more format specifier characters that indicate how a value is to be displayed.</param>
		/// <exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName" /> does not exist on the control.  
		///  -or-  
		///  The data source or data member or control property specified are associated with another binding in the collection.</exception>
		// Token: 0x06000ABA RID: 2746 RVA: 0x0001E4FC File Offset: 0x0001C6FC
		public Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode, object nullValue, string formatString)
			: this(propertyName, dataSource, dataMember, formattingEnabled, dataSourceUpdateMode, nullValue, formatString, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Binding" /> class with the specified control property to the specified data member of the specified data source. Optionally enables formatting with the specified format string; propagates values to the data source based on the specified update setting; enables formatting with the specified format string; sets the property to the specified value when a <see cref="T:System.DBNull" /> is returned from the data source; and sets the specified format provider.</summary>
		/// <param name="propertyName">The name of the control property to bind.</param>
		/// <param name="dataSource">An <see cref="T:System.Object" /> representing the data source.</param>
		/// <param name="dataMember">The property or list to bind to.</param>
		/// <param name="formattingEnabled">
		///   <see langword="true" /> to format the displayed data; otherwise, <see langword="false" />.</param>
		/// <param name="dataSourceUpdateMode">One of the <see cref="T:System.Windows.Forms.DataSourceUpdateMode" /> values.</param>
		/// <param name="nullValue">The <see cref="T:System.Object" /> to be applied to the bound control property if the data source value is <see cref="T:System.DBNull" />.</param>
		/// <param name="formatString">One or more format specifier characters that indicate how a value is to be displayed.</param>
		/// <param name="formatInfo">An implementation of <see cref="T:System.IFormatProvider" /> to override default formatting behavior.</param>
		/// <exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName" /> does not exist on the control.  
		///  -or-  
		///  The data source or data member or control property specified are associated with another binding in the collection.</exception>
		// Token: 0x06000ABB RID: 2747 RVA: 0x0001E51C File Offset: 0x0001C71C
		public Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode, object nullValue, string formatString, IFormatProvider formatInfo)
		{
			this.propertyName = "";
			this.formatString = string.Empty;
			this.dsNullValue = Formatter.GetDefaultDataSourceNullValue(null);
			base..ctor();
			this.bindToObject = new BindToObject(this, dataSource, dataMember);
			this.propertyName = propertyName;
			this.formattingEnabled = formattingEnabled;
			this.formatString = formatString;
			this.nullValue = nullValue;
			this.formatInfo = formatInfo;
			this.formattingEnabled = formattingEnabled;
			this.dataSourceUpdateMode = dataSourceUpdateMode;
			this.CheckBinding();
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x0001E59C File Offset: 0x0001C79C
		private Binding()
		{
			this.propertyName = "";
			this.formatString = string.Empty;
			this.dsNullValue = Formatter.GetDefaultDataSourceNullValue(null);
			base..ctor();
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x0001E5C6 File Offset: 0x0001C7C6
		internal BindToObject BindToObject
		{
			get
			{
				return this.bindToObject;
			}
		}

		/// <summary>Gets the data source for this binding.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the data source.</returns>
		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x0001E5CE File Offset: 0x0001C7CE
		public object DataSource
		{
			get
			{
				return this.bindToObject.DataSource;
			}
		}

		/// <summary>Gets an object that contains information about this binding based on the <paramref name="dataMember" /> parameter in the <see cref="Overload:System.Windows.Forms.Binding.#ctor" /> constructor.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.BindingMemberInfo" /> that contains information about this <see cref="T:System.Windows.Forms.Binding" />.</returns>
		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x0001E5DB File Offset: 0x0001C7DB
		public BindingMemberInfo BindingMemberInfo
		{
			get
			{
				return this.bindToObject.BindingMemberInfo;
			}
		}

		/// <summary>Gets the control the <see cref="T:System.Windows.Forms.Binding" /> is associated with.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.IBindableComponent" /> the <see cref="T:System.Windows.Forms.Binding" /> is associated with.</returns>
		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x0001E5E8 File Offset: 0x0001C7E8
		[DefaultValue(null)]
		public IBindableComponent BindableComponent
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				return this.control;
			}
		}

		/// <summary>Gets the control that the binding belongs to.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that the binding belongs to.</returns>
		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x0001E5F0 File Offset: 0x0001C7F0
		[DefaultValue(null)]
		public Control Control
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				return this.control as Control;
			}
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0001E600 File Offset: 0x0001C800
		internal static bool IsComponentCreated(IBindableComponent component)
		{
			Control control = component as Control;
			return control == null || control.Created;
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x0001E61F File Offset: 0x0001C81F
		internal bool ComponentCreated
		{
			get
			{
				return Binding.IsComponentCreated(this.control);
			}
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0001E62C File Offset: 0x0001C82C
		private void FormLoaded(object sender, EventArgs e)
		{
			this.CheckBinding();
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0001E634 File Offset: 0x0001C834
		internal void SetBindableComponent(IBindableComponent value)
		{
			if (this.control != value)
			{
				IBindableComponent bindableComponent = this.control;
				this.BindTarget(false);
				this.control = value;
				this.BindTarget(true);
				try
				{
					this.CheckBinding();
				}
				catch
				{
					this.BindTarget(false);
					this.control = bindableComponent;
					this.BindTarget(true);
					throw;
				}
				BindingContext.UpdateBinding((this.control != null && Binding.IsComponentCreated(this.control)) ? this.control.BindingContext : null, this);
				Form form = value as Form;
				if (form != null)
				{
					form.Load += this.FormLoaded;
				}
			}
		}

		/// <summary>Gets a value indicating whether the binding is active.</summary>
		/// <returns>
		///   <see langword="true" /> if the binding is active; otherwise, <see langword="false" />.</returns>
		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x0001E6E0 File Offset: 0x0001C8E0
		public bool IsBinding
		{
			get
			{
				return this.bound;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.BindingManagerBase" /> for this <see cref="T:System.Windows.Forms.Binding" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.BindingManagerBase" /> that manages this <see cref="T:System.Windows.Forms.Binding" />.</returns>
		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x0001E6E8 File Offset: 0x0001C8E8
		public BindingManagerBase BindingManagerBase
		{
			get
			{
				return this.bindingManagerBase;
			}
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0001E6F0 File Offset: 0x0001C8F0
		internal void SetListManager(BindingManagerBase bindingManagerBase)
		{
			if (this.bindingManagerBase is CurrencyManager)
			{
				((CurrencyManager)this.bindingManagerBase).MetaDataChanged -= this.binding_MetaDataChanged;
			}
			this.bindingManagerBase = bindingManagerBase;
			if (this.bindingManagerBase is CurrencyManager)
			{
				((CurrencyManager)this.bindingManagerBase).MetaDataChanged += this.binding_MetaDataChanged;
			}
			this.BindToObject.SetBindingManagerBase(bindingManagerBase);
			this.CheckBinding();
		}

		/// <summary>Gets or sets the name of the control's data-bound property.</summary>
		/// <returns>The name of a control property to bind to.</returns>
		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x0001E768 File Offset: 0x0001C968
		[DefaultValue("")]
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Binding.FormattingEnabled" /> property is set to <see langword="true" /> and a binding operation is complete, such as when data is pushed from the control to the data source or vice versa</summary>
		// Token: 0x1400004C RID: 76
		// (add) Token: 0x06000ACA RID: 2762 RVA: 0x0001E770 File Offset: 0x0001C970
		// (remove) Token: 0x06000ACB RID: 2763 RVA: 0x0001E789 File Offset: 0x0001C989
		public event BindingCompleteEventHandler BindingComplete
		{
			add
			{
				this.onComplete = (BindingCompleteEventHandler)Delegate.Combine(this.onComplete, value);
			}
			remove
			{
				this.onComplete = (BindingCompleteEventHandler)Delegate.Remove(this.onComplete, value);
			}
		}

		/// <summary>Occurs when the value of a data-bound control changes.</summary>
		// Token: 0x1400004D RID: 77
		// (add) Token: 0x06000ACC RID: 2764 RVA: 0x0001E7A2 File Offset: 0x0001C9A2
		// (remove) Token: 0x06000ACD RID: 2765 RVA: 0x0001E7BB File Offset: 0x0001C9BB
		public event ConvertEventHandler Parse
		{
			add
			{
				this.onParse = (ConvertEventHandler)Delegate.Combine(this.onParse, value);
			}
			remove
			{
				this.onParse = (ConvertEventHandler)Delegate.Remove(this.onParse, value);
			}
		}

		/// <summary>Occurs when the property of a control is bound to a data value.</summary>
		// Token: 0x1400004E RID: 78
		// (add) Token: 0x06000ACE RID: 2766 RVA: 0x0001E7D4 File Offset: 0x0001C9D4
		// (remove) Token: 0x06000ACF RID: 2767 RVA: 0x0001E7ED File Offset: 0x0001C9ED
		public event ConvertEventHandler Format
		{
			add
			{
				this.onFormat = (ConvertEventHandler)Delegate.Combine(this.onFormat, value);
			}
			remove
			{
				this.onFormat = (ConvertEventHandler)Delegate.Remove(this.onFormat, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether type conversion and formatting is applied to the control property data.</summary>
		/// <returns>
		///   <see langword="true" /> if type conversion and formatting of control property data is enabled; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x0001E806 File Offset: 0x0001CA06
		// (set) Token: 0x06000AD1 RID: 2769 RVA: 0x0001E80E File Offset: 0x0001CA0E
		[DefaultValue(false)]
		public bool FormattingEnabled
		{
			get
			{
				return this.formattingEnabled;
			}
			set
			{
				if (this.formattingEnabled != value)
				{
					this.formattingEnabled = value;
					if (this.IsBinding)
					{
						this.PushData();
					}
				}
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.IFormatProvider" /> that provides custom formatting behavior.</summary>
		/// <returns>The <see cref="T:System.IFormatProvider" /> implementation that provides custom formatting behavior.</returns>
		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x0001E82F File Offset: 0x0001CA2F
		// (set) Token: 0x06000AD3 RID: 2771 RVA: 0x0001E837 File Offset: 0x0001CA37
		[DefaultValue(null)]
		public IFormatProvider FormatInfo
		{
			get
			{
				return this.formatInfo;
			}
			set
			{
				if (this.formatInfo != value)
				{
					this.formatInfo = value;
					if (this.IsBinding)
					{
						this.PushData();
					}
				}
			}
		}

		/// <summary>Gets or sets the format specifier characters that indicate how a value is to be displayed.</summary>
		/// <returns>The string of format specifier characters that indicate how a value is to be displayed.</returns>
		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x0001E858 File Offset: 0x0001CA58
		// (set) Token: 0x06000AD5 RID: 2773 RVA: 0x0001E860 File Offset: 0x0001CA60
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
					if (this.IsBinding)
					{
						this.PushData();
					}
				}
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Object" /> to be set as the control property when the data source contains a <see cref="T:System.DBNull" /> value.</summary>
		/// <returns>The <see cref="T:System.Object" /> to be set as the control property when the data source contains a <see cref="T:System.DBNull" /> value. The default is <see langword="null" />.</returns>
		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x0001E890 File Offset: 0x0001CA90
		// (set) Token: 0x06000AD7 RID: 2775 RVA: 0x0001E898 File Offset: 0x0001CA98
		public object NullValue
		{
			get
			{
				return this.nullValue;
			}
			set
			{
				if (!object.Equals(this.nullValue, value))
				{
					this.nullValue = value;
					if (this.IsBinding && Formatter.IsNullData(this.bindToObject.GetValue(), this.dsNullValue))
					{
						this.PushData();
					}
				}
			}
		}

		/// <summary>Gets or sets the value to be stored in the data source if the control value is <see langword="null" /> or empty.</summary>
		/// <returns>The <see cref="T:System.Object" /> to be stored in the data source when the control property is empty or <see langword="null" />. The default is <see cref="T:System.DBNull" /> for value types and <see langword="null" /> for non-value types.</returns>
		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x0001E8D6 File Offset: 0x0001CAD6
		// (set) Token: 0x06000AD9 RID: 2777 RVA: 0x0001E8E0 File Offset: 0x0001CAE0
		public object DataSourceNullValue
		{
			get
			{
				return this.dsNullValue;
			}
			set
			{
				if (!object.Equals(this.dsNullValue, value))
				{
					object obj = this.dsNullValue;
					this.dsNullValue = value;
					this.dsNullValueSet = true;
					if (this.IsBinding)
					{
						object value2 = this.bindToObject.GetValue();
						if (Formatter.IsNullData(value2, obj))
						{
							this.WriteValue();
						}
						if (Formatter.IsNullData(value2, value))
						{
							this.ReadValue();
						}
					}
				}
			}
		}

		/// <summary>Gets or sets when changes to the data source are propagated to the bound control property.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ControlUpdateMode" /> values. The default is <see cref="F:System.Windows.Forms.ControlUpdateMode.OnPropertyChanged" />.</returns>
		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x0001E942 File Offset: 0x0001CB42
		// (set) Token: 0x06000ADB RID: 2779 RVA: 0x0001E94A File Offset: 0x0001CB4A
		[DefaultValue(ControlUpdateMode.OnPropertyChanged)]
		public ControlUpdateMode ControlUpdateMode
		{
			get
			{
				return this.controlUpdateMode;
			}
			set
			{
				if (this.controlUpdateMode != value)
				{
					this.controlUpdateMode = value;
					if (this.IsBinding)
					{
						this.PushData();
					}
				}
			}
		}

		/// <summary>Gets or sets a value that indicates when changes to the bound control property are propagated to the data source.</summary>
		/// <returns>A value that indicates when changes are propagated. The default is <see cref="F:System.Windows.Forms.DataSourceUpdateMode.OnValidation" />.</returns>
		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x0001E96B File Offset: 0x0001CB6B
		// (set) Token: 0x06000ADD RID: 2781 RVA: 0x0001E973 File Offset: 0x0001CB73
		[DefaultValue(DataSourceUpdateMode.OnValidation)]
		public DataSourceUpdateMode DataSourceUpdateMode
		{
			get
			{
				return this.dataSourceUpdateMode;
			}
			set
			{
				if (this.dataSourceUpdateMode != value)
				{
					this.dataSourceUpdateMode = value;
				}
			}
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0001E988 File Offset: 0x0001CB88
		private void BindTarget(bool bind)
		{
			if (bind)
			{
				if (this.IsBinding)
				{
					if (this.propInfo != null && this.control != null)
					{
						EventHandler eventHandler = new EventHandler(this.Target_PropertyChanged);
						this.propInfo.AddValueChanged(this.control, eventHandler);
					}
					if (this.validateInfo != null)
					{
						CancelEventHandler cancelEventHandler = new CancelEventHandler(this.Target_Validate);
						this.validateInfo.AddEventHandler(this.control, cancelEventHandler);
						return;
					}
				}
			}
			else
			{
				if (this.propInfo != null && this.control != null)
				{
					EventHandler eventHandler2 = new EventHandler(this.Target_PropertyChanged);
					this.propInfo.RemoveValueChanged(this.control, eventHandler2);
				}
				if (this.validateInfo != null)
				{
					CancelEventHandler cancelEventHandler2 = new CancelEventHandler(this.Target_Validate);
					this.validateInfo.RemoveEventHandler(this.control, cancelEventHandler2);
				}
			}
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0001E62C File Offset: 0x0001C82C
		private void binding_MetaDataChanged(object sender, EventArgs e)
		{
			this.CheckBinding();
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0001EA50 File Offset: 0x0001CC50
		private void CheckBinding()
		{
			this.bindToObject.CheckBinding();
			if (this.control != null && this.propertyName.Length > 0)
			{
				this.control.DataBindings.CheckDuplicates(this);
				Type type = this.control.GetType();
				string text = this.propertyName + "IsNull";
				PropertyDescriptor propertyDescriptor = null;
				PropertyDescriptor propertyDescriptor2 = null;
				InheritanceAttribute inheritanceAttribute = (InheritanceAttribute)TypeDescriptor.GetAttributes(this.control)[typeof(InheritanceAttribute)];
				PropertyDescriptorCollection propertyDescriptorCollection;
				if (inheritanceAttribute != null && inheritanceAttribute.InheritanceLevel != InheritanceLevel.NotInherited)
				{
					propertyDescriptorCollection = TypeDescriptor.GetProperties(type);
				}
				else
				{
					propertyDescriptorCollection = TypeDescriptor.GetProperties(this.control);
				}
				for (int i = 0; i < propertyDescriptorCollection.Count; i++)
				{
					if (propertyDescriptor == null && string.Equals(propertyDescriptorCollection[i].Name, this.propertyName, StringComparison.OrdinalIgnoreCase))
					{
						propertyDescriptor = propertyDescriptorCollection[i];
						if (propertyDescriptor2 != null)
						{
							break;
						}
					}
					if (propertyDescriptor2 == null && string.Equals(propertyDescriptorCollection[i].Name, text, StringComparison.OrdinalIgnoreCase))
					{
						propertyDescriptor2 = propertyDescriptorCollection[i];
						if (propertyDescriptor != null)
						{
							break;
						}
					}
				}
				if (propertyDescriptor == null)
				{
					throw new ArgumentException(SR.GetString("ListBindingBindProperty", new object[] { this.propertyName }), "PropertyName");
				}
				if (propertyDescriptor.IsReadOnly && this.controlUpdateMode != ControlUpdateMode.Never)
				{
					throw new ArgumentException(SR.GetString("ListBindingBindPropertyReadOnly", new object[] { this.propertyName }), "PropertyName");
				}
				this.propInfo = propertyDescriptor;
				Type propertyType = this.propInfo.PropertyType;
				this.propInfoConverter = this.propInfo.Converter;
				if (propertyDescriptor2 != null && propertyDescriptor2.PropertyType == typeof(bool) && !propertyDescriptor2.IsReadOnly)
				{
					this.propIsNullInfo = propertyDescriptor2;
				}
				EventDescriptor eventDescriptor = null;
				string text2 = "Validating";
				EventDescriptorCollection events = TypeDescriptor.GetEvents(this.control);
				for (int j = 0; j < events.Count; j++)
				{
					if (eventDescriptor == null && string.Equals(events[j].Name, text2, StringComparison.OrdinalIgnoreCase))
					{
						eventDescriptor = events[j];
						break;
					}
				}
				this.validateInfo = eventDescriptor;
			}
			else
			{
				this.propInfo = null;
				this.validateInfo = null;
			}
			this.UpdateIsBinding();
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0001EC8C File Offset: 0x0001CE8C
		internal bool ControlAtDesignTime()
		{
			IComponent component = this.control;
			if (component == null)
			{
				return false;
			}
			ISite site = component.Site;
			return site != null && site.DesignMode;
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0001ECB7 File Offset: 0x0001CEB7
		private object GetDataSourceNullValue(Type type)
		{
			if (!this.dsNullValueSet)
			{
				return Formatter.GetDefaultDataSourceNullValue(type);
			}
			return this.dsNullValue;
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0001ECD0 File Offset: 0x0001CED0
		private object GetPropValue()
		{
			bool flag = false;
			if (this.propIsNullInfo != null)
			{
				flag = (bool)this.propIsNullInfo.GetValue(this.control);
			}
			object obj;
			if (flag)
			{
				obj = this.DataSourceNullValue;
			}
			else
			{
				obj = this.propInfo.GetValue(this.control);
				if (obj == null)
				{
					obj = this.DataSourceNullValue;
				}
			}
			return obj;
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0001ED28 File Offset: 0x0001CF28
		private BindingCompleteEventArgs CreateBindingCompleteEventArgs(BindingCompleteContext context, Exception ex)
		{
			bool flag = false;
			string text = string.Empty;
			BindingCompleteState bindingCompleteState = BindingCompleteState.Success;
			if (ex != null)
			{
				text = ex.Message;
				bindingCompleteState = BindingCompleteState.Exception;
				flag = true;
			}
			else
			{
				text = this.BindToObject.DataErrorText;
				if (!string.IsNullOrEmpty(text))
				{
					bindingCompleteState = BindingCompleteState.DataError;
				}
			}
			return new BindingCompleteEventArgs(this, bindingCompleteState, context, text, ex, flag);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Binding.BindingComplete" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.BindingCompleteEventArgs" /> that contains the event data.</param>
		// Token: 0x06000AE5 RID: 2789 RVA: 0x0001ED70 File Offset: 0x0001CF70
		protected virtual void OnBindingComplete(BindingCompleteEventArgs e)
		{
			if (!this.inOnBindingComplete)
			{
				try
				{
					this.inOnBindingComplete = true;
					if (this.onComplete != null)
					{
						this.onComplete(this, e);
					}
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
					e.Cancel = true;
				}
				finally
				{
					this.inOnBindingComplete = false;
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Binding.Parse" /> event.</summary>
		/// <param name="cevent">A <see cref="T:System.Windows.Forms.ConvertEventArgs" /> that contains the event data.</param>
		// Token: 0x06000AE6 RID: 2790 RVA: 0x0001EDDC File Offset: 0x0001CFDC
		protected virtual void OnParse(ConvertEventArgs cevent)
		{
			if (this.onParse != null)
			{
				this.onParse(this, cevent);
			}
			if (!this.formattingEnabled && !(cevent.Value is DBNull) && cevent.Value != null && cevent.DesiredType != null && !cevent.DesiredType.IsInstanceOfType(cevent.Value) && cevent.Value is IConvertible)
			{
				cevent.Value = Convert.ChangeType(cevent.Value, cevent.DesiredType, CultureInfo.CurrentCulture);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Binding.Format" /> event.</summary>
		/// <param name="cevent">A <see cref="T:System.Windows.Forms.ConvertEventArgs" /> that contains the event data.</param>
		// Token: 0x06000AE7 RID: 2791 RVA: 0x0001EE68 File Offset: 0x0001D068
		protected virtual void OnFormat(ConvertEventArgs cevent)
		{
			if (this.onFormat != null)
			{
				this.onFormat(this, cevent);
			}
			if (!this.formattingEnabled && !(cevent.Value is DBNull) && cevent.DesiredType != null && !cevent.DesiredType.IsInstanceOfType(cevent.Value) && cevent.Value is IConvertible)
			{
				cevent.Value = Convert.ChangeType(cevent.Value, cevent.DesiredType, CultureInfo.CurrentCulture);
			}
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0001EEEC File Offset: 0x0001D0EC
		private object ParseObject(object value)
		{
			Type bindToType = this.bindToObject.BindToType;
			if (this.formattingEnabled)
			{
				ConvertEventArgs convertEventArgs = new ConvertEventArgs(value, bindToType);
				this.OnParse(convertEventArgs);
				object value2 = convertEventArgs.Value;
				if (!object.Equals(value, value2))
				{
					return value2;
				}
				TypeConverter typeConverter = null;
				if (this.bindToObject.FieldInfo != null)
				{
					typeConverter = this.bindToObject.FieldInfo.Converter;
				}
				return Formatter.ParseObject(value, bindToType, (value == null) ? this.propInfo.PropertyType : value.GetType(), typeConverter, this.propInfoConverter, this.formatInfo, this.nullValue, this.GetDataSourceNullValue(bindToType));
			}
			else
			{
				ConvertEventArgs convertEventArgs2 = new ConvertEventArgs(value, bindToType);
				this.OnParse(convertEventArgs2);
				if (convertEventArgs2.Value != null && (convertEventArgs2.Value.GetType().IsSubclassOf(bindToType) || convertEventArgs2.Value.GetType() == bindToType || convertEventArgs2.Value is DBNull))
				{
					return convertEventArgs2.Value;
				}
				TypeConverter converter = TypeDescriptor.GetConverter((value != null) ? value.GetType() : typeof(object));
				if (converter != null && converter.CanConvertTo(bindToType))
				{
					return converter.ConvertTo(value, bindToType);
				}
				if (value is IConvertible)
				{
					object obj = Convert.ChangeType(value, bindToType, CultureInfo.CurrentCulture);
					if (obj != null && (obj.GetType().IsSubclassOf(bindToType) || obj.GetType() == bindToType))
					{
						return obj;
					}
				}
				return null;
			}
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0001F050 File Offset: 0x0001D250
		private object FormatObject(object value)
		{
			if (this.ControlAtDesignTime())
			{
				return value;
			}
			Type propertyType = this.propInfo.PropertyType;
			if (this.formattingEnabled)
			{
				ConvertEventArgs convertEventArgs = new ConvertEventArgs(value, propertyType);
				this.OnFormat(convertEventArgs);
				if (convertEventArgs.Value != value)
				{
					return convertEventArgs.Value;
				}
				TypeConverter typeConverter = null;
				if (this.bindToObject.FieldInfo != null)
				{
					typeConverter = this.bindToObject.FieldInfo.Converter;
				}
				return Formatter.FormatObject(value, propertyType, typeConverter, this.propInfoConverter, this.formatString, this.formatInfo, this.nullValue, this.dsNullValue);
			}
			else
			{
				ConvertEventArgs convertEventArgs2 = new ConvertEventArgs(value, propertyType);
				this.OnFormat(convertEventArgs2);
				object obj = convertEventArgs2.Value;
				if (propertyType == typeof(object))
				{
					return value;
				}
				if (obj != null && (obj.GetType().IsSubclassOf(propertyType) || obj.GetType() == propertyType))
				{
					return obj;
				}
				TypeConverter converter = TypeDescriptor.GetConverter((value != null) ? value.GetType() : typeof(object));
				if (converter != null && converter.CanConvertTo(propertyType))
				{
					return converter.ConvertTo(value, propertyType);
				}
				if (value is IConvertible)
				{
					obj = Convert.ChangeType(value, propertyType, CultureInfo.CurrentCulture);
					if (obj != null && (obj.GetType().IsSubclassOf(propertyType) || obj.GetType() == propertyType))
					{
						return obj;
					}
				}
				throw new FormatException(SR.GetString("ListBindingFormatFailed"));
			}
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0001F1B3 File Offset: 0x0001D3B3
		internal bool PullData()
		{
			return this.PullData(true, false);
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0001F1BD File Offset: 0x0001D3BD
		internal bool PullData(bool reformat)
		{
			return this.PullData(reformat, false);
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0001F1C8 File Offset: 0x0001D3C8
		internal bool PullData(bool reformat, bool force)
		{
			if (this.ControlUpdateMode == ControlUpdateMode.Never)
			{
				reformat = false;
			}
			bool flag = false;
			object obj = null;
			Exception ex = null;
			if (!this.IsBinding)
			{
				return false;
			}
			if (!force)
			{
				if (this.propInfo.SupportsChangeEvents && !this.modified)
				{
					return false;
				}
				if (this.DataSourceUpdateMode == DataSourceUpdateMode.Never)
				{
					return false;
				}
			}
			if (this.inPushOrPull && this.formattingEnabled)
			{
				return false;
			}
			this.inPushOrPull = true;
			object propValue = this.GetPropValue();
			try
			{
				obj = this.ParseObject(propValue);
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			try
			{
				if (ex != null || (!this.FormattingEnabled && obj == null))
				{
					flag = true;
					obj = this.bindToObject.GetValue();
				}
				if (reformat && (!this.FormattingEnabled || !flag))
				{
					object obj2 = this.FormatObject(obj);
					if (force || !this.FormattingEnabled || !object.Equals(obj2, propValue))
					{
						this.SetPropValue(obj2);
					}
				}
				if (!flag)
				{
					this.bindToObject.SetValue(obj);
				}
			}
			catch (Exception ex3)
			{
				ex = ex3;
				if (!this.FormattingEnabled)
				{
					throw;
				}
			}
			finally
			{
				this.inPushOrPull = false;
			}
			if (this.FormattingEnabled)
			{
				BindingCompleteEventArgs bindingCompleteEventArgs = this.CreateBindingCompleteEventArgs(BindingCompleteContext.DataSourceUpdate, ex);
				this.OnBindingComplete(bindingCompleteEventArgs);
				if (bindingCompleteEventArgs.BindingCompleteState == BindingCompleteState.Success && !bindingCompleteEventArgs.Cancel)
				{
					this.modified = false;
				}
				return bindingCompleteEventArgs.Cancel;
			}
			this.modified = false;
			return false;
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0001F32C File Offset: 0x0001D52C
		internal bool PushData()
		{
			return this.PushData(false);
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0001F338 File Offset: 0x0001D538
		internal bool PushData(bool force)
		{
			Exception ex = null;
			if (!force && this.ControlUpdateMode == ControlUpdateMode.Never)
			{
				return false;
			}
			if (this.inPushOrPull && this.formattingEnabled)
			{
				return false;
			}
			this.inPushOrPull = true;
			try
			{
				if (this.IsBinding)
				{
					object value = this.bindToObject.GetValue();
					object obj = this.FormatObject(value);
					this.SetPropValue(obj);
					this.modified = false;
				}
				else
				{
					this.SetPropValue(null);
				}
			}
			catch (Exception ex2)
			{
				ex = ex2;
				if (!this.FormattingEnabled)
				{
					throw;
				}
			}
			finally
			{
				this.inPushOrPull = false;
			}
			if (this.FormattingEnabled)
			{
				BindingCompleteEventArgs bindingCompleteEventArgs = this.CreateBindingCompleteEventArgs(BindingCompleteContext.ControlUpdate, ex);
				this.OnBindingComplete(bindingCompleteEventArgs);
				return bindingCompleteEventArgs.Cancel;
			}
			return false;
		}

		/// <summary>Sets the control property to the value read from the data source.</summary>
		// Token: 0x06000AEF RID: 2799 RVA: 0x0001F3FC File Offset: 0x0001D5FC
		public void ReadValue()
		{
			this.PushData(true);
		}

		/// <summary>Reads the current value from the control property and writes it to the data source.</summary>
		// Token: 0x06000AF0 RID: 2800 RVA: 0x0001F406 File Offset: 0x0001D606
		public void WriteValue()
		{
			this.PullData(true, true);
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0001F414 File Offset: 0x0001D614
		private void SetPropValue(object value)
		{
			if (this.ControlAtDesignTime())
			{
				return;
			}
			this.inSetPropValue = true;
			try
			{
				bool flag = value == null || Formatter.IsNullData(value, this.DataSourceNullValue);
				if (flag)
				{
					if (this.propIsNullInfo != null)
					{
						this.propIsNullInfo.SetValue(this.control, true);
					}
					else if (this.propInfo.PropertyType == typeof(object))
					{
						this.propInfo.SetValue(this.control, this.DataSourceNullValue);
					}
					else
					{
						this.propInfo.SetValue(this.control, null);
					}
				}
				else
				{
					this.propInfo.SetValue(this.control, value);
				}
			}
			finally
			{
				this.inSetPropValue = false;
			}
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0001F4E0 File Offset: 0x0001D6E0
		private bool ShouldSerializeFormatString()
		{
			return this.formatString != null && this.formatString.Length > 0;
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0001F4FA File Offset: 0x0001D6FA
		private bool ShouldSerializeNullValue()
		{
			return this.nullValue != null;
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0001F505 File Offset: 0x0001D705
		private bool ShouldSerializeDataSourceNullValue()
		{
			return this.dsNullValueSet && this.dsNullValue != Formatter.GetDefaultDataSourceNullValue(null);
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0001F522 File Offset: 0x0001D722
		private void Target_PropertyChanged(object sender, EventArgs e)
		{
			if (this.inSetPropValue)
			{
				return;
			}
			if (this.IsBinding)
			{
				this.modified = true;
				if (this.DataSourceUpdateMode == DataSourceUpdateMode.OnPropertyChanged)
				{
					this.PullData(false);
					this.modified = true;
				}
			}
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0001F554 File Offset: 0x0001D754
		private void Target_Validate(object sender, CancelEventArgs e)
		{
			try
			{
				if (this.PullData(true))
				{
					e.Cancel = true;
				}
			}
			catch
			{
				e.Cancel = true;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x0001F590 File Offset: 0x0001D790
		internal bool IsBindable
		{
			get
			{
				return this.control != null && this.propertyName.Length > 0 && this.bindToObject.DataSource != null && this.bindingManagerBase != null;
			}
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0001F5C0 File Offset: 0x0001D7C0
		internal void UpdateIsBinding()
		{
			bool flag = this.IsBindable && this.ComponentCreated && this.bindingManagerBase.IsBinding;
			if (this.bound != flag)
			{
				this.bound = flag;
				this.BindTarget(flag);
				if (this.bound)
				{
					if (this.controlUpdateMode == ControlUpdateMode.Never)
					{
						this.PullData(false, true);
						return;
					}
					this.PushData();
				}
			}
		}

		// Token: 0x0400068F RID: 1679
		private IBindableComponent control;

		// Token: 0x04000690 RID: 1680
		private BindingManagerBase bindingManagerBase;

		// Token: 0x04000691 RID: 1681
		private BindToObject bindToObject;

		// Token: 0x04000692 RID: 1682
		private string propertyName;

		// Token: 0x04000693 RID: 1683
		private PropertyDescriptor propInfo;

		// Token: 0x04000694 RID: 1684
		private PropertyDescriptor propIsNullInfo;

		// Token: 0x04000695 RID: 1685
		private EventDescriptor validateInfo;

		// Token: 0x04000696 RID: 1686
		private TypeConverter propInfoConverter;

		// Token: 0x04000697 RID: 1687
		private bool formattingEnabled;

		// Token: 0x04000698 RID: 1688
		private bool bound;

		// Token: 0x04000699 RID: 1689
		private bool modified;

		// Token: 0x0400069A RID: 1690
		private bool inSetPropValue;

		// Token: 0x0400069B RID: 1691
		private bool inPushOrPull;

		// Token: 0x0400069C RID: 1692
		private bool inOnBindingComplete;

		// Token: 0x0400069D RID: 1693
		private string formatString;

		// Token: 0x0400069E RID: 1694
		private IFormatProvider formatInfo;

		// Token: 0x0400069F RID: 1695
		private object nullValue;

		// Token: 0x040006A0 RID: 1696
		private object dsNullValue;

		// Token: 0x040006A1 RID: 1697
		private bool dsNullValueSet;

		// Token: 0x040006A2 RID: 1698
		private ConvertEventHandler onParse;

		// Token: 0x040006A3 RID: 1699
		private ConvertEventHandler onFormat;

		// Token: 0x040006A4 RID: 1700
		private ControlUpdateMode controlUpdateMode;

		// Token: 0x040006A5 RID: 1701
		private DataSourceUpdateMode dataSourceUpdateMode;

		// Token: 0x040006A6 RID: 1702
		private BindingCompleteEventHandler onComplete;
	}
}
