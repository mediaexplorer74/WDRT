using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Collects the characteristics associated with table layouts.</summary>
	// Token: 0x02000392 RID: 914
	[TypeConverter(typeof(TableLayoutSettingsTypeConverter))]
	[Serializable]
	public sealed class TableLayoutSettings : LayoutSettings, ISerializable
	{
		// Token: 0x06003BF0 RID: 15344 RVA: 0x00106156 File Offset: 0x00104356
		internal TableLayoutSettings()
			: base(null)
		{
			this._stub = new TableLayoutSettings.TableLayoutSettingsStub();
		}

		// Token: 0x06003BF1 RID: 15345 RVA: 0x000AF9F3 File Offset: 0x000ADBF3
		internal TableLayoutSettings(IArrangedElement owner)
			: base(owner)
		{
		}

		// Token: 0x06003BF2 RID: 15346 RVA: 0x0010616C File Offset: 0x0010436C
		internal TableLayoutSettings(SerializationInfo serializationInfo, StreamingContext context)
			: this()
		{
			TypeConverter converter = TypeDescriptor.GetConverter(this);
			string @string = serializationInfo.GetString("SerializedString");
			if (!string.IsNullOrEmpty(@string) && converter != null)
			{
				TableLayoutSettings tableLayoutSettings = converter.ConvertFromInvariantString(@string) as TableLayoutSettings;
				if (tableLayoutSettings != null)
				{
					this.ApplySettings(tableLayoutSettings);
				}
			}
		}

		/// <summary>Gets the current table layout engine.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Layout.LayoutEngine" /> currently being used.</returns>
		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x06003BF3 RID: 15347 RVA: 0x00105519 File Offset: 0x00103719
		public override LayoutEngine LayoutEngine
		{
			get
			{
				return TableLayout.Instance;
			}
		}

		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x06003BF4 RID: 15348 RVA: 0x001061B4 File Offset: 0x001043B4
		private TableLayout TableLayout
		{
			get
			{
				return (TableLayout)this.LayoutEngine;
			}
		}

		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x06003BF5 RID: 15349 RVA: 0x001061C1 File Offset: 0x001043C1
		// (set) Token: 0x06003BF6 RID: 15350 RVA: 0x001061CC File Offset: 0x001043CC
		[DefaultValue(TableLayoutPanelCellBorderStyle.None)]
		[SRCategory("CatAppearance")]
		[SRDescription("TableLayoutPanelCellBorderStyleDescr")]
		internal TableLayoutPanelCellBorderStyle CellBorderStyle
		{
			get
			{
				return this._borderStyle;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 6))
				{
					throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
					{
						"CellBorderStyle",
						value.ToString()
					}));
				}
				this._borderStyle = value;
				TableLayout.ContainerInfo containerInfo = TableLayout.GetContainerInfo(base.Owner);
				containerInfo.CellBorderWidth = TableLayoutSettings.borderStyleToOffset[(int)value];
				LayoutTransaction.DoLayout(base.Owner, base.Owner, PropertyNames.CellBorderStyle);
			}
		}

		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x06003BF7 RID: 15351 RVA: 0x0010624D File Offset: 0x0010444D
		[DefaultValue(0)]
		internal int CellBorderWidth
		{
			get
			{
				return TableLayout.GetContainerInfo(base.Owner).CellBorderWidth;
			}
		}

		/// <summary>Gets or sets the maximum number of columns allowed in the table layout.</summary>
		/// <returns>The maximum number of columns allowed in the table layout. The default is 0.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property value is less than 0.</exception>
		// Token: 0x17000E99 RID: 3737
		// (get) Token: 0x06003BF8 RID: 15352 RVA: 0x00106260 File Offset: 0x00104460
		// (set) Token: 0x06003BF9 RID: 15353 RVA: 0x00106280 File Offset: 0x00104480
		[SRDescription("GridPanelColumnsDescr")]
		[SRCategory("CatLayout")]
		[DefaultValue(0)]
		public int ColumnCount
		{
			get
			{
				TableLayout.ContainerInfo containerInfo = TableLayout.GetContainerInfo(base.Owner);
				return containerInfo.MaxColumns;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("ColumnCount", value, SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"ColumnCount",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture)
					}));
				}
				TableLayout.ContainerInfo containerInfo = TableLayout.GetContainerInfo(base.Owner);
				containerInfo.MaxColumns = value;
				LayoutTransaction.DoLayout(base.Owner, base.Owner, PropertyNames.Columns);
			}
		}

		/// <summary>Gets or sets the maximum number of rows allowed in the table layout.</summary>
		/// <returns>The maximum number of rows allowed in the table layout. The default is 0.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property value is less than 0.</exception>
		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x06003BFA RID: 15354 RVA: 0x00106304 File Offset: 0x00104504
		// (set) Token: 0x06003BFB RID: 15355 RVA: 0x00106324 File Offset: 0x00104524
		[SRDescription("GridPanelRowsDescr")]
		[SRCategory("CatLayout")]
		[DefaultValue(0)]
		public int RowCount
		{
			get
			{
				TableLayout.ContainerInfo containerInfo = TableLayout.GetContainerInfo(base.Owner);
				return containerInfo.MaxRows;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("RowCount", value, SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"RowCount",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture)
					}));
				}
				TableLayout.ContainerInfo containerInfo = TableLayout.GetContainerInfo(base.Owner);
				containerInfo.MaxRows = value;
				LayoutTransaction.DoLayout(base.Owner, base.Owner, PropertyNames.Rows);
			}
		}

		/// <summary>Gets the collection of styles used to determine the look and feel of the table layout rows.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TableLayoutRowStyleCollection" /> that contains the row styles for the layout table.</returns>
		// Token: 0x17000E9B RID: 3739
		// (get) Token: 0x06003BFC RID: 15356 RVA: 0x001063A8 File Offset: 0x001045A8
		[SRDescription("GridPanelRowStylesDescr")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[SRCategory("CatLayout")]
		public TableLayoutRowStyleCollection RowStyles
		{
			get
			{
				if (this.IsStub)
				{
					return this._stub.RowStyles;
				}
				TableLayout.ContainerInfo containerInfo = TableLayout.GetContainerInfo(base.Owner);
				return containerInfo.RowStyles;
			}
		}

		/// <summary>Gets the collection of styles used to determine the look and feel of the table layout columns.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TableLayoutColumnStyleCollection" /> that contains the column styles for the layout table.</returns>
		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x06003BFD RID: 15357 RVA: 0x001063DC File Offset: 0x001045DC
		[SRDescription("GridPanelColumnStylesDescr")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[SRCategory("CatLayout")]
		public TableLayoutColumnStyleCollection ColumnStyles
		{
			get
			{
				if (this.IsStub)
				{
					return this._stub.ColumnStyles;
				}
				TableLayout.ContainerInfo containerInfo = TableLayout.GetContainerInfo(base.Owner);
				return containerInfo.ColumnStyles;
			}
		}

		/// <summary>Gets or sets a value indicating how the table layout should expand to accommodate new cells when all existing cells are occupied.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.TableLayoutPanelGrowStyle" /> values. The default is <see cref="F:System.Windows.Forms.TableLayoutPanelGrowStyle.AddRows" />.</returns>
		/// <exception cref="T:System.ArgumentException">The property value is not valid for the enumeration type.</exception>
		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x06003BFE RID: 15358 RVA: 0x0010640F File Offset: 0x0010460F
		// (set) Token: 0x06003BFF RID: 15359 RVA: 0x00106424 File Offset: 0x00104624
		[SRDescription("TableLayoutPanelGrowStyleDescr")]
		[SRCategory("CatLayout")]
		[DefaultValue(TableLayoutPanelGrowStyle.AddRows)]
		public TableLayoutPanelGrowStyle GrowStyle
		{
			get
			{
				return TableLayout.GetContainerInfo(base.Owner).GrowStyle;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
					{
						"GrowStyle",
						value.ToString()
					}));
				}
				TableLayout.ContainerInfo containerInfo = TableLayout.GetContainerInfo(base.Owner);
				if (containerInfo.GrowStyle != value)
				{
					containerInfo.GrowStyle = value;
					LayoutTransaction.DoLayout(base.Owner, base.Owner, PropertyNames.GrowStyle);
				}
			}
		}

		// Token: 0x17000E9E RID: 3742
		// (get) Token: 0x06003C00 RID: 15360 RVA: 0x001064A1 File Offset: 0x001046A1
		internal bool IsStub
		{
			get
			{
				return this._stub != null;
			}
		}

		// Token: 0x06003C01 RID: 15361 RVA: 0x001064AE File Offset: 0x001046AE
		internal void ApplySettings(TableLayoutSettings settings)
		{
			if (settings.IsStub)
			{
				if (!this.IsStub)
				{
					settings._stub.ApplySettings(this);
					return;
				}
				this._stub = settings._stub;
			}
		}

		/// <summary>Gets the number of columns that the cell containing the child control spans.</summary>
		/// <param name="control">A control contained within a cell.</param>
		/// <returns>The number of columns that the cell containing the child control spans.</returns>
		// Token: 0x06003C02 RID: 15362 RVA: 0x001064DC File Offset: 0x001046DC
		public int GetColumnSpan(object control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (this.IsStub)
			{
				return this._stub.GetColumnSpan(control);
			}
			IArrangedElement arrangedElement = this.LayoutEngine.CastToArrangedElement(control);
			return TableLayout.GetLayoutInfo(arrangedElement).ColumnSpan;
		}

		/// <summary>Sets the number of columns that the cell containing the child control spans.</summary>
		/// <param name="control">A control contained within a cell.</param>
		/// <param name="value">The number of columns that the cell containing the child control spans.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="control" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> is less than 1.</exception>
		// Token: 0x06003C03 RID: 15363 RVA: 0x00106524 File Offset: 0x00104724
		public void SetColumnSpan(object control, int value)
		{
			if (value < 1)
			{
				throw new ArgumentOutOfRangeException("ColumnSpan", SR.GetString("InvalidArgument", new object[]
				{
					"ColumnSpan",
					value.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (this.IsStub)
			{
				this._stub.SetColumnSpan(control, value);
				return;
			}
			IArrangedElement arrangedElement = this.LayoutEngine.CastToArrangedElement(control);
			if (arrangedElement.Container != null)
			{
				TableLayout.ClearCachedAssignments(TableLayout.GetContainerInfo(arrangedElement.Container));
			}
			TableLayout.GetLayoutInfo(arrangedElement).ColumnSpan = value;
			LayoutTransaction.DoLayout(arrangedElement.Container, arrangedElement, PropertyNames.ColumnSpan);
		}

		/// <summary>Gets the number of rows that the cell containing the child control spans.</summary>
		/// <param name="control">A control contained within a cell.</param>
		/// <returns>The number of rows that the cell containing the child control spans.</returns>
		// Token: 0x06003C04 RID: 15364 RVA: 0x001065C0 File Offset: 0x001047C0
		public int GetRowSpan(object control)
		{
			if (this.IsStub)
			{
				return this._stub.GetRowSpan(control);
			}
			IArrangedElement arrangedElement = this.LayoutEngine.CastToArrangedElement(control);
			return TableLayout.GetLayoutInfo(arrangedElement).RowSpan;
		}

		/// <summary>Sets the number of rows that the cell containing the child control spans.</summary>
		/// <param name="control">A control contained within a cell.</param>
		/// <param name="value">The number of rows that the cell containing the child control spans.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="control" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> is less than 1.</exception>
		// Token: 0x06003C05 RID: 15365 RVA: 0x001065FC File Offset: 0x001047FC
		public void SetRowSpan(object control, int value)
		{
			if (value < 1)
			{
				throw new ArgumentOutOfRangeException("RowSpan", SR.GetString("InvalidArgument", new object[]
				{
					"RowSpan",
					value.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (this.IsStub)
			{
				this._stub.SetRowSpan(control, value);
				return;
			}
			IArrangedElement arrangedElement = this.LayoutEngine.CastToArrangedElement(control);
			if (arrangedElement.Container != null)
			{
				TableLayout.ClearCachedAssignments(TableLayout.GetContainerInfo(arrangedElement.Container));
			}
			TableLayout.GetLayoutInfo(arrangedElement).RowSpan = value;
			LayoutTransaction.DoLayout(arrangedElement.Container, arrangedElement, PropertyNames.RowSpan);
		}

		/// <summary>Gets the row position of the specified child control.</summary>
		/// <param name="control">A control contained within a cell.</param>
		/// <returns>The row position of the specified child control.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="control" /> is <see langword="null" />.</exception>
		// Token: 0x06003C06 RID: 15366 RVA: 0x001066A8 File Offset: 0x001048A8
		[SRDescription("GridPanelRowDescr")]
		[SRCategory("CatLayout")]
		[DefaultValue(-1)]
		public int GetRow(object control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (this.IsStub)
			{
				return this._stub.GetRow(control);
			}
			IArrangedElement arrangedElement = this.LayoutEngine.CastToArrangedElement(control);
			TableLayout.LayoutInfo layoutInfo = TableLayout.GetLayoutInfo(arrangedElement);
			return layoutInfo.RowPosition;
		}

		/// <summary>Sets the row position of the specified child control.</summary>
		/// <param name="control">A control contained within a cell.</param>
		/// <param name="row">The row position of the specified child control.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="control" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="row" /> is less than -1.</exception>
		// Token: 0x06003C07 RID: 15367 RVA: 0x001066F4 File Offset: 0x001048F4
		public void SetRow(object control, int row)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (row < -1)
			{
				throw new ArgumentOutOfRangeException("Row", SR.GetString("InvalidArgument", new object[]
				{
					"Row",
					row.ToString(CultureInfo.CurrentCulture)
				}));
			}
			this.SetCellPosition(control, row, -1, true, false);
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> that represents the row and the column of the cell.</summary>
		/// <param name="control">A control contained within a cell.</param>
		/// <returns>A <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> that represents the cell position.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="control" /> is <see langword="null" />.</exception>
		// Token: 0x06003C08 RID: 15368 RVA: 0x00106750 File Offset: 0x00104950
		[SRDescription("TableLayoutSettingsGetCellPositionDescr")]
		[SRCategory("CatLayout")]
		[DefaultValue(-1)]
		public TableLayoutPanelCellPosition GetCellPosition(object control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			return new TableLayoutPanelCellPosition(this.GetColumn(control), this.GetRow(control));
		}

		/// <summary>Sets the <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> that represents the row and the column of the cell.</summary>
		/// <param name="control">A control contained within a cell.</param>
		/// <param name="cellPosition">A <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> that represents the cell position.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="control" /> is <see langword="null" />.</exception>
		// Token: 0x06003C09 RID: 15369 RVA: 0x00106773 File Offset: 0x00104973
		[SRDescription("TableLayoutSettingsSetCellPositionDescr")]
		[SRCategory("CatLayout")]
		[DefaultValue(-1)]
		public void SetCellPosition(object control, TableLayoutPanelCellPosition cellPosition)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			this.SetCellPosition(control, cellPosition.Row, cellPosition.Column, true, true);
		}

		/// <summary>Gets the column position of the specified child control.</summary>
		/// <param name="control">A control contained within a cell.</param>
		/// <returns>The column position of the specified child control.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="control" /> is <see langword="null" />.</exception>
		// Token: 0x06003C0A RID: 15370 RVA: 0x0010679C File Offset: 0x0010499C
		[SRDescription("GridPanelColumnDescr")]
		[SRCategory("CatLayout")]
		[DefaultValue(-1)]
		public int GetColumn(object control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (this.IsStub)
			{
				return this._stub.GetColumn(control);
			}
			IArrangedElement arrangedElement = this.LayoutEngine.CastToArrangedElement(control);
			TableLayout.LayoutInfo layoutInfo = TableLayout.GetLayoutInfo(arrangedElement);
			return layoutInfo.ColumnPosition;
		}

		/// <summary>Sets the column position for the specified child control.</summary>
		/// <param name="control">A control contained within a cell.</param>
		/// <param name="column">The column position for the specified child control.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="control" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="column" /> is less than -1.</exception>
		// Token: 0x06003C0B RID: 15371 RVA: 0x001067E8 File Offset: 0x001049E8
		public void SetColumn(object control, int column)
		{
			if (column < -1)
			{
				throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
				{
					"Column",
					column.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (this.IsStub)
			{
				this._stub.SetColumn(control, column);
				return;
			}
			this.SetCellPosition(control, -1, column, false, true);
		}

		// Token: 0x06003C0C RID: 15372 RVA: 0x00106848 File Offset: 0x00104A48
		private void SetCellPosition(object control, int row, int column, bool rowSpecified, bool colSpecified)
		{
			if (this.IsStub)
			{
				if (colSpecified)
				{
					this._stub.SetColumn(control, column);
				}
				if (rowSpecified)
				{
					this._stub.SetRow(control, row);
					return;
				}
			}
			else
			{
				IArrangedElement arrangedElement = this.LayoutEngine.CastToArrangedElement(control);
				if (arrangedElement.Container != null)
				{
					TableLayout.ClearCachedAssignments(TableLayout.GetContainerInfo(arrangedElement.Container));
				}
				TableLayout.LayoutInfo layoutInfo = TableLayout.GetLayoutInfo(arrangedElement);
				if (colSpecified)
				{
					layoutInfo.ColumnPosition = column;
				}
				if (rowSpecified)
				{
					layoutInfo.RowPosition = row;
				}
				LayoutTransaction.DoLayout(arrangedElement.Container, arrangedElement, PropertyNames.TableIndex);
			}
		}

		// Token: 0x06003C0D RID: 15373 RVA: 0x001068D3 File Offset: 0x00104AD3
		internal IArrangedElement GetControlFromPosition(int column, int row)
		{
			return this.TableLayout.GetControlFromPosition(base.Owner, column, row);
		}

		// Token: 0x06003C0E RID: 15374 RVA: 0x001068E8 File Offset: 0x00104AE8
		internal TableLayoutPanelCellPosition GetPositionFromControl(IArrangedElement element)
		{
			return this.TableLayout.GetPositionFromControl(base.Owner, element);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" />.</summary>
		/// <param name="si">The object to be populated with serialization information.</param>
		/// <param name="context">The destination context of the serialization.</param>
		// Token: 0x06003C0F RID: 15375 RVA: 0x001068FC File Offset: 0x00104AFC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			TypeConverter converter = TypeDescriptor.GetConverter(this);
			string text = ((converter != null) ? converter.ConvertToInvariantString(this) : null);
			if (!string.IsNullOrEmpty(text))
			{
				si.AddValue("SerializedString", text);
			}
		}

		// Token: 0x06003C10 RID: 15376 RVA: 0x00106934 File Offset: 0x00104B34
		internal List<TableLayoutSettings.ControlInformation> GetControlsInformation()
		{
			if (this.IsStub)
			{
				return this._stub.GetControlsInformation();
			}
			List<TableLayoutSettings.ControlInformation> list = new List<TableLayoutSettings.ControlInformation>(base.Owner.Children.Count);
			foreach (object obj in base.Owner.Children)
			{
				IArrangedElement arrangedElement = (IArrangedElement)obj;
				Control control = arrangedElement as Control;
				if (control != null)
				{
					TableLayoutSettings.ControlInformation controlInformation = default(TableLayoutSettings.ControlInformation);
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(control)["Name"];
					if (propertyDescriptor != null && propertyDescriptor.PropertyType == typeof(string))
					{
						controlInformation.Name = propertyDescriptor.GetValue(control);
					}
					controlInformation.Row = this.GetRow(control);
					controlInformation.RowSpan = this.GetRowSpan(control);
					controlInformation.Column = this.GetColumn(control);
					controlInformation.ColumnSpan = this.GetColumnSpan(control);
					list.Add(controlInformation);
				}
			}
			return list;
		}

		// Token: 0x04002386 RID: 9094
		private static int[] borderStyleToOffset = new int[] { 0, 1, 2, 3, 2, 3, 3 };

		// Token: 0x04002387 RID: 9095
		private TableLayoutPanelCellBorderStyle _borderStyle;

		// Token: 0x04002388 RID: 9096
		private TableLayoutSettings.TableLayoutSettingsStub _stub;

		// Token: 0x020007EE RID: 2030
		internal struct ControlInformation
		{
			// Token: 0x06006E32 RID: 28210 RVA: 0x0019383F File Offset: 0x00191A3F
			internal ControlInformation(object name, int row, int column, int rowSpan, int columnSpan)
			{
				this.Name = name;
				this.Row = row;
				this.Column = column;
				this.RowSpan = rowSpan;
				this.ColumnSpan = columnSpan;
			}

			// Token: 0x040042CE RID: 17102
			internal object Name;

			// Token: 0x040042CF RID: 17103
			internal int Row;

			// Token: 0x040042D0 RID: 17104
			internal int Column;

			// Token: 0x040042D1 RID: 17105
			internal int RowSpan;

			// Token: 0x040042D2 RID: 17106
			internal int ColumnSpan;
		}

		// Token: 0x020007EF RID: 2031
		private class TableLayoutSettingsStub
		{
			// Token: 0x06006E34 RID: 28212 RVA: 0x00193878 File Offset: 0x00191A78
			internal void ApplySettings(TableLayoutSettings settings)
			{
				TableLayout.ContainerInfo containerInfo = TableLayout.GetContainerInfo(settings.Owner);
				Control control = containerInfo.Container as Control;
				if (control != null && this.controlsInfo != null)
				{
					foreach (object obj in this.controlsInfo.Keys)
					{
						TableLayoutSettings.ControlInformation controlInformation = this.controlsInfo[obj];
						foreach (object obj2 in control.Controls)
						{
							Control control2 = (Control)obj2;
							if (control2 != null)
							{
								string text = null;
								PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(control2)["Name"];
								if (propertyDescriptor != null && propertyDescriptor.PropertyType == typeof(string))
								{
									text = propertyDescriptor.GetValue(control2) as string;
								}
								if (WindowsFormsUtils.SafeCompareStrings(text, obj as string, false))
								{
									settings.SetRow(control2, controlInformation.Row);
									settings.SetColumn(control2, controlInformation.Column);
									settings.SetRowSpan(control2, controlInformation.RowSpan);
									settings.SetColumnSpan(control2, controlInformation.ColumnSpan);
									break;
								}
							}
						}
					}
				}
				containerInfo.RowStyles = this.rowStyles;
				containerInfo.ColumnStyles = this.columnStyles;
				this.columnStyles = null;
				this.rowStyles = null;
				this.isValid = false;
			}

			// Token: 0x17001818 RID: 6168
			// (get) Token: 0x06006E35 RID: 28213 RVA: 0x00193A34 File Offset: 0x00191C34
			public TableLayoutColumnStyleCollection ColumnStyles
			{
				get
				{
					if (this.columnStyles == null)
					{
						this.columnStyles = new TableLayoutColumnStyleCollection();
					}
					return this.columnStyles;
				}
			}

			// Token: 0x17001819 RID: 6169
			// (get) Token: 0x06006E36 RID: 28214 RVA: 0x00193A4F File Offset: 0x00191C4F
			public bool IsValid
			{
				get
				{
					return this.isValid;
				}
			}

			// Token: 0x1700181A RID: 6170
			// (get) Token: 0x06006E37 RID: 28215 RVA: 0x00193A57 File Offset: 0x00191C57
			public TableLayoutRowStyleCollection RowStyles
			{
				get
				{
					if (this.rowStyles == null)
					{
						this.rowStyles = new TableLayoutRowStyleCollection();
					}
					return this.rowStyles;
				}
			}

			// Token: 0x06006E38 RID: 28216 RVA: 0x00193A74 File Offset: 0x00191C74
			internal List<TableLayoutSettings.ControlInformation> GetControlsInformation()
			{
				if (this.controlsInfo == null)
				{
					return new List<TableLayoutSettings.ControlInformation>();
				}
				List<TableLayoutSettings.ControlInformation> list = new List<TableLayoutSettings.ControlInformation>(this.controlsInfo.Count);
				foreach (object obj in this.controlsInfo.Keys)
				{
					TableLayoutSettings.ControlInformation controlInformation = this.controlsInfo[obj];
					controlInformation.Name = obj;
					list.Add(controlInformation);
				}
				return list;
			}

			// Token: 0x06006E39 RID: 28217 RVA: 0x00193B04 File Offset: 0x00191D04
			private TableLayoutSettings.ControlInformation GetControlInformation(object controlName)
			{
				if (this.controlsInfo == null)
				{
					return TableLayoutSettings.TableLayoutSettingsStub.DefaultControlInfo;
				}
				if (!this.controlsInfo.ContainsKey(controlName))
				{
					return TableLayoutSettings.TableLayoutSettingsStub.DefaultControlInfo;
				}
				return this.controlsInfo[controlName];
			}

			// Token: 0x06006E3A RID: 28218 RVA: 0x00193B34 File Offset: 0x00191D34
			public int GetColumn(object controlName)
			{
				return this.GetControlInformation(controlName).Column;
			}

			// Token: 0x06006E3B RID: 28219 RVA: 0x00193B42 File Offset: 0x00191D42
			public int GetColumnSpan(object controlName)
			{
				return this.GetControlInformation(controlName).ColumnSpan;
			}

			// Token: 0x06006E3C RID: 28220 RVA: 0x00193B50 File Offset: 0x00191D50
			public int GetRow(object controlName)
			{
				return this.GetControlInformation(controlName).Row;
			}

			// Token: 0x06006E3D RID: 28221 RVA: 0x00193B5E File Offset: 0x00191D5E
			public int GetRowSpan(object controlName)
			{
				return this.GetControlInformation(controlName).RowSpan;
			}

			// Token: 0x06006E3E RID: 28222 RVA: 0x00193B6C File Offset: 0x00191D6C
			private void SetControlInformation(object controlName, TableLayoutSettings.ControlInformation info)
			{
				if (this.controlsInfo == null)
				{
					this.controlsInfo = new Dictionary<object, TableLayoutSettings.ControlInformation>();
				}
				this.controlsInfo[controlName] = info;
			}

			// Token: 0x06006E3F RID: 28223 RVA: 0x00193B90 File Offset: 0x00191D90
			public void SetColumn(object controlName, int column)
			{
				if (this.GetColumn(controlName) != column)
				{
					TableLayoutSettings.ControlInformation controlInformation = this.GetControlInformation(controlName);
					controlInformation.Column = column;
					this.SetControlInformation(controlName, controlInformation);
				}
			}

			// Token: 0x06006E40 RID: 28224 RVA: 0x00193BC0 File Offset: 0x00191DC0
			public void SetColumnSpan(object controlName, int value)
			{
				if (this.GetColumnSpan(controlName) != value)
				{
					TableLayoutSettings.ControlInformation controlInformation = this.GetControlInformation(controlName);
					controlInformation.ColumnSpan = value;
					this.SetControlInformation(controlName, controlInformation);
				}
			}

			// Token: 0x06006E41 RID: 28225 RVA: 0x00193BF0 File Offset: 0x00191DF0
			public void SetRow(object controlName, int row)
			{
				if (this.GetRow(controlName) != row)
				{
					TableLayoutSettings.ControlInformation controlInformation = this.GetControlInformation(controlName);
					controlInformation.Row = row;
					this.SetControlInformation(controlName, controlInformation);
				}
			}

			// Token: 0x06006E42 RID: 28226 RVA: 0x00193C20 File Offset: 0x00191E20
			public void SetRowSpan(object controlName, int value)
			{
				if (this.GetRowSpan(controlName) != value)
				{
					TableLayoutSettings.ControlInformation controlInformation = this.GetControlInformation(controlName);
					controlInformation.RowSpan = value;
					this.SetControlInformation(controlName, controlInformation);
				}
			}

			// Token: 0x040042D3 RID: 17107
			private static TableLayoutSettings.ControlInformation DefaultControlInfo = new TableLayoutSettings.ControlInformation(null, -1, -1, 1, 1);

			// Token: 0x040042D4 RID: 17108
			private TableLayoutColumnStyleCollection columnStyles;

			// Token: 0x040042D5 RID: 17109
			private TableLayoutRowStyleCollection rowStyles;

			// Token: 0x040042D6 RID: 17110
			private Dictionary<object, TableLayoutSettings.ControlInformation> controlsInfo;

			// Token: 0x040042D7 RID: 17111
			private bool isValid = true;
		}

		// Token: 0x020007F0 RID: 2032
		internal class StyleConverter : TypeConverter
		{
			// Token: 0x06006E44 RID: 28228 RVA: 0x0002792C File Offset: 0x00025B2C
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
			}

			// Token: 0x06006E45 RID: 28229 RVA: 0x00193C60 File Offset: 0x00191E60
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == null)
				{
					throw new ArgumentNullException("destinationType");
				}
				if (destinationType == typeof(InstanceDescriptor) && value is TableLayoutStyle)
				{
					TableLayoutStyle tableLayoutStyle = (TableLayoutStyle)value;
					SizeType sizeType = tableLayoutStyle.SizeType;
					if (sizeType == SizeType.AutoSize)
					{
						return new InstanceDescriptor(tableLayoutStyle.GetType().GetConstructor(new Type[0]), new object[0]);
					}
					if (sizeType - SizeType.Absolute <= 1)
					{
						return new InstanceDescriptor(tableLayoutStyle.GetType().GetConstructor(new Type[]
						{
							typeof(SizeType),
							typeof(int)
						}), new object[] { tableLayoutStyle.SizeType, tableLayoutStyle.Size });
					}
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
	}
}
