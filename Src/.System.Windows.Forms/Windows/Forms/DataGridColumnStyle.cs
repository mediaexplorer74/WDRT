using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	/// <summary>Specifies the appearance, text formatting, and behavior of a <see cref="T:System.Windows.Forms.DataGrid" /> control column. This class is abstract.</summary>
	// Token: 0x0200017F RID: 383
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	[DefaultProperty("Header")]
	public abstract class DataGridColumnStyle : Component, IDataGridColumnStyleEditingNotificationService
	{
		/// <summary>In a derived class, initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> class.</summary>
		// Token: 0x06001655 RID: 5717 RVA: 0x000506E8 File Offset: 0x0004E8E8
		public DataGridColumnStyle()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> class with the specified <see cref="T:System.ComponentModel.PropertyDescriptor" />.</summary>
		/// <param name="prop">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that provides the attributes for the column.</param>
		// Token: 0x06001656 RID: 5718 RVA: 0x00050724 File Offset: 0x0004E924
		public DataGridColumnStyle(PropertyDescriptor prop)
			: this()
		{
			this.PropertyDescriptor = prop;
			if (prop != null)
			{
				this.readOnly = prop.IsReadOnly;
			}
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x00050742 File Offset: 0x0004E942
		internal DataGridColumnStyle(PropertyDescriptor prop, bool isDefault)
			: this(prop)
		{
			this.isDefault = isDefault;
			if (isDefault)
			{
				this.headerName = prop.Name;
				this.mappingName = prop.Name;
			}
		}

		/// <summary>Gets or sets the alignment of text in a column.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values. The default is <see langword="Left" />. Valid options include <see langword="Left" />, <see langword="Center" />, and <see langword="Right" />.</returns>
		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06001658 RID: 5720 RVA: 0x0005076D File Offset: 0x0004E96D
		// (set) Token: 0x06001659 RID: 5721 RVA: 0x00050778 File Offset: 0x0004E978
		[SRCategory("CatDisplay")]
		[Localizable(true)]
		[DefaultValue(HorizontalAlignment.Left)]
		public virtual HorizontalAlignment Alignment
		{
			get
			{
				return this.alignment;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DataGridLineStyle));
				}
				if (this.alignment != value)
				{
					this.alignment = value;
					this.OnAlignmentChanged(EventArgs.Empty);
					this.Invalidate();
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridColumnStyle.Alignment" /> property value changes.</summary>
		// Token: 0x140000E7 RID: 231
		// (add) Token: 0x0600165A RID: 5722 RVA: 0x000507CC File Offset: 0x0004E9CC
		// (remove) Token: 0x0600165B RID: 5723 RVA: 0x000507DF File Offset: 0x0004E9DF
		public event EventHandler AlignmentChanged
		{
			add
			{
				base.Events.AddHandler(DataGridColumnStyle.EventAlignment, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridColumnStyle.EventAlignment, value);
			}
		}

		/// <summary>Updates the value of a specified row with the given text.</summary>
		/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> associated with the <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</param>
		/// <param name="rowNum">The row to update.</param>
		/// <param name="displayText">The new value.</param>
		// Token: 0x0600165C RID: 5724 RVA: 0x000070A6 File Offset: 0x000052A6
		protected internal virtual void UpdateUI(CurrencyManager source, int rowNum, string displayText)
		{
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.AccessibleObject" /> for the column.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> for the column.</returns>
		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x0600165D RID: 5725 RVA: 0x000507F2 File Offset: 0x0004E9F2
		[Browsable(false)]
		public AccessibleObject HeaderAccessibleObject
		{
			get
			{
				if (this.headerAccessibleObject == null)
				{
					this.headerAccessibleObject = this.CreateHeaderAccessibleObject();
				}
				return this.headerAccessibleObject;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.ComponentModel.PropertyDescriptor" /> that determines the attributes of data displayed by the <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that contains data about the attributes of the column.</returns>
		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x0600165E RID: 5726 RVA: 0x0005080E File Offset: 0x0004EA0E
		// (set) Token: 0x0600165F RID: 5727 RVA: 0x00050816 File Offset: 0x0004EA16
		[DefaultValue(null)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual PropertyDescriptor PropertyDescriptor
		{
			get
			{
				return this.propertyDescriptor;
			}
			set
			{
				if (this.propertyDescriptor != value)
				{
					this.propertyDescriptor = value;
					this.OnPropertyDescriptorChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridColumnStyle.PropertyDescriptor" /> property value changes.</summary>
		// Token: 0x140000E8 RID: 232
		// (add) Token: 0x06001660 RID: 5728 RVA: 0x00050833 File Offset: 0x0004EA33
		// (remove) Token: 0x06001661 RID: 5729 RVA: 0x00050846 File Offset: 0x0004EA46
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public event EventHandler PropertyDescriptorChanged
		{
			add
			{
				base.Events.AddHandler(DataGridColumnStyle.EventPropertyDescriptor, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridColumnStyle.EventPropertyDescriptor, value);
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.AccessibleObject" /> for the column.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> for the column.</returns>
		// Token: 0x06001662 RID: 5730 RVA: 0x00050859 File Offset: 0x0004EA59
		protected virtual AccessibleObject CreateHeaderAccessibleObject()
		{
			return new DataGridColumnStyle.DataGridColumnHeaderAccessibleObject(this);
		}

		/// <summary>Sets the <see cref="T:System.Windows.Forms.DataGrid" /> control that this column belongs to.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.DataGrid" /> control that this column belongs to.</param>
		// Token: 0x06001663 RID: 5731 RVA: 0x00050861 File Offset: 0x0004EA61
		protected virtual void SetDataGrid(DataGrid value)
		{
			this.SetDataGridInColumn(value);
		}

		/// <summary>Sets the <see cref="T:System.Windows.Forms.DataGrid" /> for the column.</summary>
		/// <param name="value">A <see cref="T:System.Windows.Forms.DataGrid" />.</param>
		// Token: 0x06001664 RID: 5732 RVA: 0x0005086C File Offset: 0x0004EA6C
		protected virtual void SetDataGridInColumn(DataGrid value)
		{
			if (this.PropertyDescriptor == null && value != null)
			{
				CurrencyManager listManager = value.ListManager;
				if (listManager == null)
				{
					return;
				}
				PropertyDescriptorCollection itemProperties = listManager.GetItemProperties();
				int count = itemProperties.Count;
				for (int i = 0; i < itemProperties.Count; i++)
				{
					PropertyDescriptor propertyDescriptor = itemProperties[i];
					if (!typeof(IList).IsAssignableFrom(propertyDescriptor.PropertyType) && propertyDescriptor.Name.Equals(this.HeaderText))
					{
						this.PropertyDescriptor = propertyDescriptor;
						return;
					}
				}
			}
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x000508EC File Offset: 0x0004EAEC
		internal void SetDataGridInternalInColumn(DataGrid value)
		{
			if (value == null || value.Initializing)
			{
				return;
			}
			this.SetDataGridInColumn(value);
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridTableStyle" /> for the column.</summary>
		/// <returns>The <see cref="P:System.Windows.Forms.DataGridColumnStyle.DataGridTableStyle" /> that contains the current <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</returns>
		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001666 RID: 5734 RVA: 0x00050901 File Offset: 0x0004EB01
		[Browsable(false)]
		public virtual DataGridTableStyle DataGridTableStyle
		{
			get
			{
				return this.dataGridTableStyle;
			}
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x0005090C File Offset: 0x0004EB0C
		internal void SetDataGridTableInColumn(DataGridTableStyle value, bool force)
		{
			if (this.dataGridTableStyle != null && this.dataGridTableStyle.Equals(value) && !force)
			{
				return;
			}
			if (value != null && value.DataGrid != null && !value.DataGrid.Initializing)
			{
				this.SetDataGridInColumn(value.DataGrid);
			}
			this.dataGridTableStyle = value;
		}

		/// <summary>Gets the height of the column's font.</summary>
		/// <returns>The height of the font, in pixels. If no font height has been set, the property returns the <see cref="T:System.Windows.Forms.DataGrid" /> control's font height; if that property hasn't been set, the default font height value for the <see cref="T:System.Windows.Forms.DataGrid" /> control is returned.</returns>
		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06001668 RID: 5736 RVA: 0x0005095E File Offset: 0x0004EB5E
		protected int FontHeight
		{
			get
			{
				if (this.fontHeight != -1)
				{
					return this.fontHeight;
				}
				if (this.DataGridTableStyle != null)
				{
					return this.DataGridTableStyle.DataGrid.FontHeight;
				}
				return DataGridTableStyle.defaultFontHeight;
			}
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x0005098E File Offset: 0x0004EB8E
		private bool ShouldSerializeFont()
		{
			return this.font != null;
		}

		/// <summary>Occurs when the column's font changes.</summary>
		// Token: 0x140000E9 RID: 233
		// (add) Token: 0x0600166A RID: 5738 RVA: 0x000070A6 File Offset: 0x000052A6
		// (remove) Token: 0x0600166B RID: 5739 RVA: 0x000070A6 File Offset: 0x000052A6
		public event EventHandler FontChanged
		{
			add
			{
			}
			remove
			{
			}
		}

		/// <summary>Gets or sets the text of the column header.</summary>
		/// <returns>A string that is displayed as the column header. If it is created by the <see cref="T:System.Windows.Forms.DataGrid" />, the default value is the name of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> used to create the column. If it is created by the user, the default is an empty string ("").</returns>
		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x0600166C RID: 5740 RVA: 0x00050999 File Offset: 0x0004EB99
		// (set) Token: 0x0600166D RID: 5741 RVA: 0x000509A1 File Offset: 0x0004EBA1
		[Localizable(true)]
		[SRCategory("CatDisplay")]
		public virtual string HeaderText
		{
			get
			{
				return this.headerName;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (this.headerName.Equals(value))
				{
					return;
				}
				this.headerName = value;
				this.OnHeaderTextChanged(EventArgs.Empty);
				if (this.PropertyDescriptor != null)
				{
					this.Invalidate();
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridColumnStyle.HeaderText" /> property value changes.</summary>
		// Token: 0x140000EA RID: 234
		// (add) Token: 0x0600166E RID: 5742 RVA: 0x000509DC File Offset: 0x0004EBDC
		// (remove) Token: 0x0600166F RID: 5743 RVA: 0x000509EF File Offset: 0x0004EBEF
		public event EventHandler HeaderTextChanged
		{
			add
			{
				base.Events.AddHandler(DataGridColumnStyle.EventHeaderText, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridColumnStyle.EventHeaderText, value);
			}
		}

		/// <summary>Gets or sets the name of the data member to map the column style to.</summary>
		/// <returns>The name of the data member to map the column style to.</returns>
		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06001670 RID: 5744 RVA: 0x00050A02 File Offset: 0x0004EC02
		// (set) Token: 0x06001671 RID: 5745 RVA: 0x00050A0C File Offset: 0x0004EC0C
		[Editor("System.Windows.Forms.Design.DataGridColumnStyleMappingNameEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Localizable(true)]
		[DefaultValue("")]
		public string MappingName
		{
			get
			{
				return this.mappingName;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (this.mappingName.Equals(value))
				{
					return;
				}
				string text = this.mappingName;
				this.mappingName = value;
				try
				{
					if (this.dataGridTableStyle != null)
					{
						this.dataGridTableStyle.GridColumnStyles.CheckForMappingNameDuplicates(this);
					}
				}
				catch
				{
					this.mappingName = text;
					throw;
				}
				this.OnMappingNameChanged(EventArgs.Empty);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridColumnStyle.MappingName" /> value changes.</summary>
		// Token: 0x140000EB RID: 235
		// (add) Token: 0x06001672 RID: 5746 RVA: 0x00050A80 File Offset: 0x0004EC80
		// (remove) Token: 0x06001673 RID: 5747 RVA: 0x00050A93 File Offset: 0x0004EC93
		public event EventHandler MappingNameChanged
		{
			add
			{
				base.Events.AddHandler(DataGridColumnStyle.EventMappingName, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridColumnStyle.EventMappingName, value);
			}
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x00050AA6 File Offset: 0x0004ECA6
		private bool ShouldSerializeHeaderText()
		{
			return this.headerName.Length != 0;
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGridColumnStyle.HeaderText" /> to its default value, <see langword="null" />.</summary>
		// Token: 0x06001675 RID: 5749 RVA: 0x00050AB6 File Offset: 0x0004ECB6
		public void ResetHeaderText()
		{
			this.HeaderText = "";
		}

		/// <summary>Gets or sets the text that is displayed when the column contains <see langword="null" />.</summary>
		/// <returns>A string displayed in a column containing a <see cref="F:System.DBNull.Value" />.</returns>
		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06001676 RID: 5750 RVA: 0x00050AC3 File Offset: 0x0004ECC3
		// (set) Token: 0x06001677 RID: 5751 RVA: 0x00050ACB File Offset: 0x0004ECCB
		[Localizable(true)]
		[SRCategory("CatDisplay")]
		public virtual string NullText
		{
			get
			{
				return this.nullText;
			}
			set
			{
				if (this.nullText != null && this.nullText.Equals(value))
				{
					return;
				}
				this.nullText = value;
				this.OnNullTextChanged(EventArgs.Empty);
				this.Invalidate();
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridColumnStyle.NullText" /> value changes.</summary>
		// Token: 0x140000EC RID: 236
		// (add) Token: 0x06001678 RID: 5752 RVA: 0x00050AFC File Offset: 0x0004ECFC
		// (remove) Token: 0x06001679 RID: 5753 RVA: 0x00050B0F File Offset: 0x0004ED0F
		public event EventHandler NullTextChanged
		{
			add
			{
				base.Events.AddHandler(DataGridColumnStyle.EventNullText, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridColumnStyle.EventNullText, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the data in the column can be edited.</summary>
		/// <returns>
		///   <see langword="true" />, if the data cannot be edited; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x0600167A RID: 5754 RVA: 0x00050B22 File Offset: 0x0004ED22
		// (set) Token: 0x0600167B RID: 5755 RVA: 0x00050B2A File Offset: 0x0004ED2A
		[DefaultValue(false)]
		public virtual bool ReadOnly
		{
			get
			{
				return this.readOnly;
			}
			set
			{
				if (this.readOnly != value)
				{
					this.readOnly = value;
					this.OnReadOnlyChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridColumnStyle.ReadOnly" /> property value changes.</summary>
		// Token: 0x140000ED RID: 237
		// (add) Token: 0x0600167C RID: 5756 RVA: 0x00050B47 File Offset: 0x0004ED47
		// (remove) Token: 0x0600167D RID: 5757 RVA: 0x00050B5A File Offset: 0x0004ED5A
		public event EventHandler ReadOnlyChanged
		{
			add
			{
				base.Events.AddHandler(DataGridColumnStyle.EventReadOnly, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridColumnStyle.EventReadOnly, value);
			}
		}

		/// <summary>Gets or sets the width of the column.</summary>
		/// <returns>The width of the column, in pixels.</returns>
		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x0600167E RID: 5758 RVA: 0x00050B6D File Offset: 0x0004ED6D
		// (set) Token: 0x0600167F RID: 5759 RVA: 0x00050B78 File Offset: 0x0004ED78
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[DefaultValue(100)]
		public virtual int Width
		{
			get
			{
				return this.width;
			}
			set
			{
				if (this.width != value)
				{
					this.width = value;
					DataGrid dataGrid = ((this.DataGridTableStyle == null) ? null : this.DataGridTableStyle.DataGrid);
					if (dataGrid != null)
					{
						dataGrid.PerformLayout();
						dataGrid.InvalidateInside();
					}
					this.OnWidthChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridColumnStyle.Width" /> property value changes.</summary>
		// Token: 0x140000EE RID: 238
		// (add) Token: 0x06001680 RID: 5760 RVA: 0x00050BC6 File Offset: 0x0004EDC6
		// (remove) Token: 0x06001681 RID: 5761 RVA: 0x00050BD9 File Offset: 0x0004EDD9
		public event EventHandler WidthChanged
		{
			add
			{
				base.Events.AddHandler(DataGridColumnStyle.EventWidth, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridColumnStyle.EventWidth, value);
			}
		}

		/// <summary>Suspends the painting of the column until the <see cref="M:System.Windows.Forms.DataGridColumnStyle.EndUpdate" /> method is called.</summary>
		// Token: 0x06001682 RID: 5762 RVA: 0x00050BEC File Offset: 0x0004EDEC
		protected void BeginUpdate()
		{
			this.updating = true;
		}

		/// <summary>Resumes the painting of columns suspended by calling the <see cref="M:System.Windows.Forms.DataGridColumnStyle.BeginUpdate" /> method.</summary>
		// Token: 0x06001683 RID: 5763 RVA: 0x00050BF5 File Offset: 0x0004EDF5
		protected void EndUpdate()
		{
			this.updating = false;
			if (this.invalid)
			{
				this.invalid = false;
				this.Invalidate();
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001684 RID: 5764 RVA: 0x0001180C File Offset: 0x0000FA0C
		internal virtual bool WantArrows
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x00050C13 File Offset: 0x0004EE13
		internal virtual string GetDisplayText(object value)
		{
			return value.ToString();
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x00050C1B File Offset: 0x0004EE1B
		private void ResetNullText()
		{
			this.NullText = SR.GetString("DataGridNullText");
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x00050C2D File Offset: 0x0004EE2D
		private bool ShouldSerializeNullText()
		{
			return !SR.GetString("DataGridNullText").Equals(this.nullText);
		}

		/// <summary>When overridden in a derived class, gets the width and height of the specified value. The width and height are used when the user navigates to <see cref="T:System.Windows.Forms.DataGridTableStyle" /> using the <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</summary>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> object.</param>
		/// <param name="value">An object value for which you want to know the screen height and width.</param>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that contains the dimensions of the cell.</returns>
		// Token: 0x06001688 RID: 5768
		protected internal abstract Size GetPreferredSize(Graphics g, object value);

		/// <summary>When overridden in a derived class, gets the minimum height of a row.</summary>
		/// <returns>The minimum height of a row.</returns>
		// Token: 0x06001689 RID: 5769
		protected internal abstract int GetMinimumHeight();

		/// <summary>When overridden in a derived class, gets the height used for automatically resizing columns.</summary>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> object.</param>
		/// <param name="value">An object value for which you want to know the screen height and width.</param>
		/// <returns>The height used for auto resizing a cell.</returns>
		// Token: 0x0600168A RID: 5770
		protected internal abstract int GetPreferredHeight(Graphics g, object value);

		/// <summary>Gets the value in the specified row from the specified <see cref="T:System.Windows.Forms.CurrencyManager" />.</summary>
		/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> containing the data.</param>
		/// <param name="rowNum">The row number containing the data.</param>
		/// <returns>An <see cref="T:System.Object" /> containing the value.</returns>
		/// <exception cref="T:System.ApplicationException">The <see cref="T:System.Data.DataColumn" /> for this <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> hasn't been set yet.</exception>
		// Token: 0x0600168B RID: 5771 RVA: 0x00050C48 File Offset: 0x0004EE48
		protected internal virtual object GetColumnValueAtRow(CurrencyManager source, int rowNum)
		{
			this.CheckValidDataSource(source);
			if (this.PropertyDescriptor == null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridColumnNoPropertyDescriptor"));
			}
			return this.PropertyDescriptor.GetValue(source[rowNum]);
		}

		/// <summary>Redraws the column and causes a paint message to be sent to the control.</summary>
		// Token: 0x0600168C RID: 5772 RVA: 0x00050C88 File Offset: 0x0004EE88
		protected virtual void Invalidate()
		{
			if (this.updating)
			{
				this.invalid = true;
				return;
			}
			DataGridTableStyle dataGridTableStyle = this.DataGridTableStyle;
			if (dataGridTableStyle != null)
			{
				dataGridTableStyle.InvalidateColumn(this);
			}
		}

		/// <summary>Throws an exception if the <see cref="T:System.Windows.Forms.DataGrid" /> does not have a valid data source, or if this column is not mapped to a valid property in the data source.</summary>
		/// <param name="value">A <see cref="T:System.Windows.Forms.CurrencyManager" /> to check.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ApplicationException">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> for this column is <see langword="null" />.</exception>
		// Token: 0x0600168D RID: 5773 RVA: 0x00050CB8 File Offset: 0x0004EEB8
		protected void CheckValidDataSource(CurrencyManager value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value", "DataGridColumnStyle.CheckValidDataSource(DataSource value), value == null");
			}
			if (this.PropertyDescriptor == null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridColumnUnbound", new object[] { this.HeaderText }));
			}
		}

		/// <summary>When overridden in a derived class, initiates a request to interrupt an edit procedure.</summary>
		/// <param name="rowNum">The row number upon which an operation is being interrupted.</param>
		// Token: 0x0600168E RID: 5774
		protected internal abstract void Abort(int rowNum);

		/// <summary>When overridden in a derived class, initiates a request to complete an editing procedure.</summary>
		/// <param name="dataSource">The <see cref="T:System.Windows.Forms.CurrencyManager" /> for the <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</param>
		/// <param name="rowNum">The number of the row being edited.</param>
		/// <returns>
		///   <see langword="true" /> if the editing procedure committed successfully; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600168F RID: 5775
		protected internal abstract bool Commit(CurrencyManager dataSource, int rowNum);

		/// <summary>Prepares a cell for editing.</summary>
		/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> for the <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</param>
		/// <param name="rowNum">The row number to edit.</param>
		/// <param name="bounds">The bounding <see cref="T:System.Drawing.Rectangle" /> in which the control is to be sited.</param>
		/// <param name="readOnly">A value indicating whether the column is a read-only. <see langword="true" /> if the value is read-only; otherwise, <see langword="false" />.</param>
		// Token: 0x06001690 RID: 5776 RVA: 0x00050D01 File Offset: 0x0004EF01
		protected internal virtual void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly)
		{
			this.Edit(source, rowNum, bounds, readOnly, null, true);
		}

		/// <summary>Prepares the cell for editing using the specified <see cref="T:System.Windows.Forms.CurrencyManager" />, row number, and <see cref="T:System.Drawing.Rectangle" /> parameters.</summary>
		/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> for the <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</param>
		/// <param name="rowNum">The row number in this column which is being edited.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> in which the control is to be sited.</param>
		/// <param name="readOnly">A value indicating whether the column is a read-only. <see langword="true" /> if the value is read-only; otherwise, <see langword="false" />.</param>
		/// <param name="displayText">The text to display in the control.</param>
		// Token: 0x06001691 RID: 5777 RVA: 0x00050D10 File Offset: 0x0004EF10
		protected internal virtual void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly, string displayText)
		{
			this.Edit(source, rowNum, bounds, readOnly, displayText, true);
		}

		/// <summary>When overridden in a deriving class, prepares a cell for editing.</summary>
		/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> for the <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</param>
		/// <param name="rowNum">The row number in this column which is being edited.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> in which the control is to be sited.</param>
		/// <param name="readOnly">A value indicating whether the column is a read-only. <see langword="true" /> if the value is read-only; otherwise, <see langword="false" />.</param>
		/// <param name="displayText">The text to display in the control.</param>
		/// <param name="cellIsVisible">A value indicating whether the cell is visible. <see langword="true" /> if the cell is visible; otherwise, <see langword="false" />.</param>
		// Token: 0x06001692 RID: 5778
		protected internal abstract void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly, string displayText, bool cellIsVisible);

		// Token: 0x06001693 RID: 5779 RVA: 0x0001180C File Offset: 0x0000FA0C
		internal virtual bool MouseDown(int rowNum, int x, int y)
		{
			return false;
		}

		/// <summary>Enters a <see cref="F:System.DBNull.Value" /> into the column.</summary>
		// Token: 0x06001694 RID: 5780 RVA: 0x000070A6 File Offset: 0x000052A6
		protected internal virtual void EnterNullValue()
		{
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x00050D20 File Offset: 0x0004EF20
		internal virtual bool KeyPress(int rowNum, Keys keyData)
		{
			if (this.ReadOnly || (this.DataGridTableStyle != null && this.DataGridTableStyle.DataGrid != null && this.DataGridTableStyle.DataGrid.ReadOnly))
			{
				return false;
			}
			if (keyData == (Keys)131168 || keyData == (Keys.ShiftKey | Keys.Space | Keys.Control))
			{
				this.EnterNullValue();
				return true;
			}
			return false;
		}

		/// <summary>Notifies a column that it must relinquish the focus to the control it is hosting.</summary>
		// Token: 0x06001696 RID: 5782 RVA: 0x000070A6 File Offset: 0x000052A6
		protected internal virtual void ConcedeFocus()
		{
		}

		/// <summary>Paints the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> with the specified <see cref="T:System.Drawing.Graphics" />, <see cref="T:System.Drawing.Rectangle" />, <see cref="T:System.Windows.Forms.CurrencyManager" />, and row number.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to draw to.</param>
		/// <param name="bounds">The bounding <see cref="T:System.Drawing.Rectangle" /> to paint into.</param>
		/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> of the <see cref="T:System.Windows.Forms.DataGrid" /> control the column belongs to.</param>
		/// <param name="rowNum">The number of the row in the underlying data being referred to.</param>
		// Token: 0x06001697 RID: 5783
		protected internal abstract void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum);

		/// <summary>When overridden in a derived class, paints a <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> with the specified <see cref="T:System.Drawing.Graphics" />, <see cref="T:System.Drawing.Rectangle" />, <see cref="T:System.Windows.Forms.CurrencyManager" />, row number, and alignment.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to draw to.</param>
		/// <param name="bounds">The bounding <see cref="T:System.Drawing.Rectangle" /> to paint into.</param>
		/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> of the <see cref="T:System.Windows.Forms.DataGrid" /> control the column belongs to.</param>
		/// <param name="rowNum">The number of the row in the underlying data being referred to.</param>
		/// <param name="alignToRight">A value indicating whether to align the column's content to the right. <see langword="true" /> if the content should be aligned to the right; otherwise <see langword="false" />.</param>
		// Token: 0x06001698 RID: 5784
		protected internal abstract void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, bool alignToRight);

		/// <summary>Paints a <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> with the specified <see cref="T:System.Drawing.Graphics" />, <see cref="T:System.Drawing.Rectangle" />, <see cref="T:System.Windows.Forms.CurrencyManager" />, row number, background color, foreground color, and alignment.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to draw to.</param>
		/// <param name="bounds">The bounding <see cref="T:System.Drawing.Rectangle" /> to paint into.</param>
		/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> of the <see cref="T:System.Windows.Forms.DataGrid" /> control the column belongs to.</param>
		/// <param name="rowNum">The number of the row in the underlying data table being referred to.</param>
		/// <param name="backBrush">A <see cref="T:System.Drawing.Brush" /> used to paint the background color.</param>
		/// <param name="foreBrush">A <see cref="T:System.Drawing.Color" /> used to paint the foreground color.</param>
		/// <param name="alignToRight">A value indicating whether to align the content to the right. <see langword="true" /> if the content is aligned to the right, otherwise, <see langword="false" />.</param>
		// Token: 0x06001699 RID: 5785 RVA: 0x00050D77 File Offset: 0x0004EF77
		protected internal virtual void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, Brush backBrush, Brush foreBrush, bool alignToRight)
		{
			this.Paint(g, bounds, source, rowNum, alignToRight);
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x00050D88 File Offset: 0x0004EF88
		private void OnPropertyDescriptorChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGridColumnStyle.EventPropertyDescriptor] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x00050DB8 File Offset: 0x0004EFB8
		private void OnAlignmentChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGridColumnStyle.EventAlignment] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x00050DE8 File Offset: 0x0004EFE8
		private void OnHeaderTextChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGridColumnStyle.EventHeaderText] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x00050E18 File Offset: 0x0004F018
		private void OnMappingNameChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGridColumnStyle.EventMappingName] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x00050E48 File Offset: 0x0004F048
		private void OnReadOnlyChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGridColumnStyle.EventReadOnly] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x00050E78 File Offset: 0x0004F078
		private void OnNullTextChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGridColumnStyle.EventNullText] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x00050EA8 File Offset: 0x0004F0A8
		private void OnWidthChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGridColumnStyle.EventWidth] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Sets the value in a specified row with the value from a specified <see cref="T:System.Windows.Forms.CurrencyManager" />.</summary>
		/// <param name="source">A <see cref="T:System.Windows.Forms.CurrencyManager" /> associated with the <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</param>
		/// <param name="rowNum">The number of the row.</param>
		/// <param name="value">The value to set.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Windows.Forms.CurrencyManager" /> object's <see cref="P:System.Windows.Forms.BindingManagerBase.Position" /> does not match <paramref name="rowNum" />.</exception>
		// Token: 0x060016A1 RID: 5793 RVA: 0x00050ED8 File Offset: 0x0004F0D8
		protected internal virtual void SetColumnValueAtRow(CurrencyManager source, int rowNum, object value)
		{
			this.CheckValidDataSource(source);
			if (source.Position != rowNum)
			{
				throw new ArgumentException(SR.GetString("DataGridColumnListManagerPosition"), "rowNum");
			}
			if (source[rowNum] is IEditableObject)
			{
				((IEditableObject)source[rowNum]).BeginEdit();
			}
			this.PropertyDescriptor.SetValue(source[rowNum], value);
		}

		/// <summary>Informs the <see cref="T:System.Windows.Forms.DataGrid" /> that the user has begun editing the column.</summary>
		/// <param name="editingControl">The <see cref="T:System.Windows.Forms.Control" /> that hosted by the column.</param>
		// Token: 0x060016A2 RID: 5794 RVA: 0x00050F3C File Offset: 0x0004F13C
		protected internal virtual void ColumnStartedEditing(Control editingControl)
		{
			this.DataGridTableStyle.DataGrid.ColumnStartedEditing(editingControl);
		}

		/// <summary>Informs the <see cref="T:System.Windows.Forms.DataGrid" /> control that the user has begun editing the column.</summary>
		/// <param name="editingControl">The <see cref="T:System.Windows.Forms.Control" /> that is editing the column.</param>
		// Token: 0x060016A3 RID: 5795 RVA: 0x00050F4F File Offset: 0x0004F14F
		void IDataGridColumnStyleEditingNotificationService.ColumnStartedEditing(Control editingControl)
		{
			this.ColumnStartedEditing(editingControl);
		}

		/// <summary>Allows the column to free resources when the control it hosts is not needed.</summary>
		// Token: 0x060016A4 RID: 5796 RVA: 0x000070A6 File Offset: 0x000052A6
		protected internal virtual void ReleaseHostedControl()
		{
		}

		// Token: 0x04000A33 RID: 2611
		private HorizontalAlignment alignment;

		// Token: 0x04000A34 RID: 2612
		private PropertyDescriptor propertyDescriptor;

		// Token: 0x04000A35 RID: 2613
		private DataGridTableStyle dataGridTableStyle;

		// Token: 0x04000A36 RID: 2614
		private Font font;

		// Token: 0x04000A37 RID: 2615
		internal int fontHeight = -1;

		// Token: 0x04000A38 RID: 2616
		private string mappingName = "";

		// Token: 0x04000A39 RID: 2617
		private string headerName = "";

		// Token: 0x04000A3A RID: 2618
		private bool invalid;

		// Token: 0x04000A3B RID: 2619
		private string nullText = SR.GetString("DataGridNullText");

		// Token: 0x04000A3C RID: 2620
		private bool readOnly;

		// Token: 0x04000A3D RID: 2621
		private bool updating;

		// Token: 0x04000A3E RID: 2622
		internal int width = -1;

		// Token: 0x04000A3F RID: 2623
		private bool isDefault;

		// Token: 0x04000A40 RID: 2624
		private AccessibleObject headerAccessibleObject;

		// Token: 0x04000A41 RID: 2625
		private static readonly object EventAlignment = new object();

		// Token: 0x04000A42 RID: 2626
		private static readonly object EventPropertyDescriptor = new object();

		// Token: 0x04000A43 RID: 2627
		private static readonly object EventHeaderText = new object();

		// Token: 0x04000A44 RID: 2628
		private static readonly object EventMappingName = new object();

		// Token: 0x04000A45 RID: 2629
		private static readonly object EventNullText = new object();

		// Token: 0x04000A46 RID: 2630
		private static readonly object EventReadOnly = new object();

		// Token: 0x04000A47 RID: 2631
		private static readonly object EventWidth = new object();

		/// <summary>Contains a <see cref="T:System.Diagnostics.TraceSwitch" /> that is used by the .NET Framework infrastructure.</summary>
		// Token: 0x0200064B RID: 1611
		protected class CompModSwitches
		{
			/// <summary>Gets a <see cref="T:System.Diagnostics.TraceSwitch" />.</summary>
			/// <returns>A <see cref="T:System.Diagnostics.TraceSwitch" /> used by the .NET Framework infrastructure.</returns>
			// Token: 0x170015A0 RID: 5536
			// (get) Token: 0x060064C8 RID: 25800 RVA: 0x0017727D File Offset: 0x0017547D
			public static TraceSwitch DGEditColumnEditing
			{
				get
				{
					if (DataGridColumnStyle.CompModSwitches.dgEditColumnEditing == null)
					{
						DataGridColumnStyle.CompModSwitches.dgEditColumnEditing = new TraceSwitch("DGEditColumnEditing", "Editing related tracing");
					}
					return DataGridColumnStyle.CompModSwitches.dgEditColumnEditing;
				}
			}

			// Token: 0x040039CD RID: 14797
			private static TraceSwitch dgEditColumnEditing;
		}

		/// <summary>Provides an implementation for an object that can be inspected by an accessibility application.</summary>
		// Token: 0x0200064C RID: 1612
		[ComVisible(true)]
		protected class DataGridColumnHeaderAccessibleObject : AccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridColumnStyle.DataGridColumnHeaderAccessibleObject" /> class and specifies the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> that hosts the object.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> that hosts the object.</param>
			// Token: 0x060064CA RID: 25802 RVA: 0x0017729F File Offset: 0x0017549F
			public DataGridColumnHeaderAccessibleObject(DataGridColumnStyle owner)
				: this()
			{
				this.owner = owner;
			}

			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridColumnStyle.DataGridColumnHeaderAccessibleObject" /> class without specifying a <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> host for the object.</summary>
			// Token: 0x060064CB RID: 25803 RVA: 0x001772AE File Offset: 0x001754AE
			public DataGridColumnHeaderAccessibleObject()
			{
			}

			/// <summary>Gets the bounding rectangle of a column.</summary>
			/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that contains the bounding values of the column.</returns>
			// Token: 0x170015A1 RID: 5537
			// (get) Token: 0x060064CC RID: 25804 RVA: 0x001772B8 File Offset: 0x001754B8
			public override Rectangle Bounds
			{
				get
				{
					if (this.owner.PropertyDescriptor == null)
					{
						return Rectangle.Empty;
					}
					DataGrid dataGrid = this.DataGrid;
					if (dataGrid.DataGridRowsLength == 0)
					{
						return Rectangle.Empty;
					}
					GridColumnStylesCollection gridColumnStyles = this.owner.dataGridTableStyle.GridColumnStyles;
					int num = -1;
					for (int i = 0; i < gridColumnStyles.Count; i++)
					{
						if (gridColumnStyles[i] == this.owner)
						{
							num = i;
							break;
						}
					}
					Rectangle cellBounds = dataGrid.GetCellBounds(0, num);
					cellBounds.Y = dataGrid.GetColumnHeadersRect().Y;
					return dataGrid.RectangleToScreen(cellBounds);
				}
			}

			/// <summary>Gets the name of the column that owns the accessibility object.</summary>
			/// <returns>The name of the column that owns the accessibility object.</returns>
			// Token: 0x170015A2 RID: 5538
			// (get) Token: 0x060064CD RID: 25805 RVA: 0x00177350 File Offset: 0x00175550
			public override string Name
			{
				get
				{
					return this.Owner.headerName;
				}
			}

			/// <summary>Gets the column style object that owns the accessibility object.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> that owns the accessibility object.</returns>
			// Token: 0x170015A3 RID: 5539
			// (get) Token: 0x060064CE RID: 25806 RVA: 0x0017735D File Offset: 0x0017555D
			protected DataGridColumnStyle Owner
			{
				get
				{
					return this.owner;
				}
			}

			/// <summary>Gets the parent accessibility object.</summary>
			/// <returns>The parent <see cref="T:System.Windows.Forms.AccessibleObject" /> of the column style object.</returns>
			// Token: 0x170015A4 RID: 5540
			// (get) Token: 0x060064CF RID: 25807 RVA: 0x00177365 File Offset: 0x00175565
			public override AccessibleObject Parent
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return this.DataGrid.AccessibilityObject;
				}
			}

			// Token: 0x170015A5 RID: 5541
			// (get) Token: 0x060064D0 RID: 25808 RVA: 0x00177372 File Offset: 0x00175572
			private DataGrid DataGrid
			{
				get
				{
					return this.owner.dataGridTableStyle.dataGrid;
				}
			}

			/// <summary>Gets the role of the accessibility object.</summary>
			/// <returns>The <see langword="AccessibleRole" /> object of the accessibility object.</returns>
			// Token: 0x170015A6 RID: 5542
			// (get) Token: 0x060064D1 RID: 25809 RVA: 0x00177384 File Offset: 0x00175584
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.ColumnHeader;
				}
			}

			/// <summary>Enables navigation to another object.</summary>
			/// <param name="navdir">One of the <see cref="T:System.Windows.Forms.AccessibleNavigation" /> values.</param>
			/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> specified by the <paramref name="navdir" /> parameter.</returns>
			// Token: 0x060064D2 RID: 25810 RVA: 0x00177388 File Offset: 0x00175588
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override AccessibleObject Navigate(AccessibleNavigation navdir)
			{
				switch (navdir)
				{
				case AccessibleNavigation.Up:
				case AccessibleNavigation.Left:
				case AccessibleNavigation.Previous:
					return this.Parent.GetChild(1 + this.Owner.dataGridTableStyle.GridColumnStyles.IndexOf(this.Owner) - 1);
				case AccessibleNavigation.Down:
				case AccessibleNavigation.Right:
				case AccessibleNavigation.Next:
					return this.Parent.GetChild(1 + this.Owner.dataGridTableStyle.GridColumnStyles.IndexOf(this.Owner) + 1);
				default:
					return null;
				}
			}

			// Token: 0x040039CE RID: 14798
			private DataGridColumnStyle owner;
		}
	}
}
