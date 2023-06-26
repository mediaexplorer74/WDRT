using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Represents a panel that dynamically lays out its contents in a grid composed of rows and columns.</summary>
	// Token: 0x0200038C RID: 908
	[ProvideProperty("ColumnSpan", typeof(Control))]
	[ProvideProperty("RowSpan", typeof(Control))]
	[ProvideProperty("Row", typeof(Control))]
	[ProvideProperty("Column", typeof(Control))]
	[ProvideProperty("CellPosition", typeof(Control))]
	[DefaultProperty("ColumnCount")]
	[DesignerSerializer("System.Windows.Forms.Design.TableLayoutPanelCodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Docking(DockingBehavior.Never)]
	[Designer("System.Windows.Forms.Design.TableLayoutPanelDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[SRDescription("DescriptionTableLayoutPanel")]
	public class TableLayoutPanel : Panel, IExtenderProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TableLayoutPanel" /> class.</summary>
		// Token: 0x06003BAE RID: 15278 RVA: 0x00105505 File Offset: 0x00103705
		public TableLayoutPanel()
		{
			this._tableLayoutSettings = TableLayout.CreateSettings(this);
		}

		/// <summary>Gets a cached instance of the panel's layout engine.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Layout.LayoutEngine" /> for the panel's contents.</returns>
		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x06003BAF RID: 15279 RVA: 0x00105519 File Offset: 0x00103719
		public override LayoutEngine LayoutEngine
		{
			get
			{
				return TableLayout.Instance;
			}
		}

		/// <summary>Gets or sets a value representing the table layout settings.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TableLayoutSettings" /> containing the table layout settings.</returns>
		/// <exception cref="T:System.NotSupportedException">The property value is <see langword="null" />, or an attempt was made to set <see cref="T:System.Windows.Forms.TableLayoutSettings" /> directly, which is not supported; instead, set individual properties.</exception>
		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x06003BB0 RID: 15280 RVA: 0x00105520 File Offset: 0x00103720
		// (set) Token: 0x06003BB1 RID: 15281 RVA: 0x00105528 File Offset: 0x00103728
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TableLayoutSettings LayoutSettings
		{
			get
			{
				return this._tableLayoutSettings;
			}
			set
			{
				if (value != null && value.IsStub)
				{
					using (new LayoutTransaction(this, this, PropertyNames.LayoutSettings))
					{
						this._tableLayoutSettings.ApplySettings(value);
						return;
					}
				}
				throw new NotSupportedException(SR.GetString("TableLayoutSettingSettingsIsNotSupported"));
			}
		}

		/// <summary>Gets or sets the border style for the panel.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> values describing the style of the border of the panel. The default is <see cref="F:System.Windows.Forms.BorderStyle.None" />.</returns>
		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x06003BB2 RID: 15282 RVA: 0x000FFC3D File Offset: 0x000FDE3D
		// (set) Token: 0x06003BB3 RID: 15283 RVA: 0x000FFC45 File Offset: 0x000FDE45
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Localizable(true)]
		public new BorderStyle BorderStyle
		{
			get
			{
				return base.BorderStyle;
			}
			set
			{
				base.BorderStyle = value;
			}
		}

		/// <summary>Gets or sets the style of the cell borders.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.TableLayoutPanelCellBorderStyle" /> values describing the style of all the cell borders in the table. The default is <see cref="F:System.Windows.Forms.TableLayoutPanelCellBorderStyle.None" />.</returns>
		// Token: 0x17000E8A RID: 3722
		// (get) Token: 0x06003BB4 RID: 15284 RVA: 0x00105588 File Offset: 0x00103788
		// (set) Token: 0x06003BB5 RID: 15285 RVA: 0x00105595 File Offset: 0x00103795
		[DefaultValue(TableLayoutPanelCellBorderStyle.None)]
		[SRCategory("CatAppearance")]
		[SRDescription("TableLayoutPanelCellBorderStyleDescr")]
		[Localizable(true)]
		public TableLayoutPanelCellBorderStyle CellBorderStyle
		{
			get
			{
				return this._tableLayoutSettings.CellBorderStyle;
			}
			set
			{
				this._tableLayoutSettings.CellBorderStyle = value;
				if (value != TableLayoutPanelCellBorderStyle.None)
				{
					base.SetStyle(ControlStyles.ResizeRedraw, true);
				}
				base.Invalidate();
			}
		}

		// Token: 0x17000E8B RID: 3723
		// (get) Token: 0x06003BB6 RID: 15286 RVA: 0x001055B5 File Offset: 0x001037B5
		private int CellBorderWidth
		{
			get
			{
				return this._tableLayoutSettings.CellBorderWidth;
			}
		}

		/// <summary>Gets the collection of controls contained within the <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TableLayoutControlCollection" /> containing the controls associated with the current <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</returns>
		// Token: 0x17000E8C RID: 3724
		// (get) Token: 0x06003BB7 RID: 15287 RVA: 0x001055C2 File Offset: 0x001037C2
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[SRDescription("ControlControlsDescr")]
		public new TableLayoutControlCollection Controls
		{
			get
			{
				return (TableLayoutControlCollection)base.Controls;
			}
		}

		/// <summary>Gets or sets the number of columns in the table.</summary>
		/// <returns>The number of columns in the <see cref="T:System.Windows.Forms.TableLayoutPanel" /> control. The default is 0.</returns>
		// Token: 0x17000E8D RID: 3725
		// (get) Token: 0x06003BB8 RID: 15288 RVA: 0x001055CF File Offset: 0x001037CF
		// (set) Token: 0x06003BB9 RID: 15289 RVA: 0x001055DC File Offset: 0x001037DC
		[SRDescription("GridPanelColumnsDescr")]
		[SRCategory("CatLayout")]
		[DefaultValue(0)]
		[Localizable(true)]
		public int ColumnCount
		{
			get
			{
				return this._tableLayoutSettings.ColumnCount;
			}
			set
			{
				this._tableLayoutSettings.ColumnCount = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.TableLayoutPanel" /> control should expand to accommodate new cells when all existing cells are occupied.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TableLayoutPanelGrowStyle" /> indicating the growth scheme. The default is <see cref="F:System.Windows.Forms.TableLayoutPanelGrowStyle.AddRows" />.</returns>
		/// <exception cref="T:System.ArgumentException">The property value is invalid for the <see cref="T:System.Windows.Forms.TableLayoutPanelGrowStyle" /> enumeration.</exception>
		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x06003BBA RID: 15290 RVA: 0x001055EA File Offset: 0x001037EA
		// (set) Token: 0x06003BBB RID: 15291 RVA: 0x001055F7 File Offset: 0x001037F7
		[SRDescription("TableLayoutPanelGrowStyleDescr")]
		[SRCategory("CatLayout")]
		[DefaultValue(TableLayoutPanelGrowStyle.AddRows)]
		public TableLayoutPanelGrowStyle GrowStyle
		{
			get
			{
				return this._tableLayoutSettings.GrowStyle;
			}
			set
			{
				this._tableLayoutSettings.GrowStyle = value;
			}
		}

		/// <summary>Gets or sets the number of rows in the table.</summary>
		/// <returns>The number of rows in the <see cref="T:System.Windows.Forms.TableLayoutPanel" /> control. The default is 0.</returns>
		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x06003BBC RID: 15292 RVA: 0x00105605 File Offset: 0x00103805
		// (set) Token: 0x06003BBD RID: 15293 RVA: 0x00105612 File Offset: 0x00103812
		[SRDescription("GridPanelRowsDescr")]
		[SRCategory("CatLayout")]
		[DefaultValue(0)]
		[Localizable(true)]
		public int RowCount
		{
			get
			{
				return this._tableLayoutSettings.RowCount;
			}
			set
			{
				this._tableLayoutSettings.RowCount = value;
			}
		}

		/// <summary>Gets a collection of row styles for the <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TableLayoutRowStyleCollection" /> containing a <see cref="T:System.Windows.Forms.RowStyle" /> for each row in the <see cref="T:System.Windows.Forms.TableLayoutPanel" /> control.</returns>
		// Token: 0x17000E90 RID: 3728
		// (get) Token: 0x06003BBE RID: 15294 RVA: 0x00105620 File Offset: 0x00103820
		[SRDescription("GridPanelRowStylesDescr")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[SRCategory("CatLayout")]
		[DisplayName("Rows")]
		[MergableProperty(false)]
		[Browsable(false)]
		public TableLayoutRowStyleCollection RowStyles
		{
			get
			{
				return this._tableLayoutSettings.RowStyles;
			}
		}

		/// <summary>Gets a collection of column styles for the <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TableLayoutColumnStyleCollection" /> containing a <see cref="T:System.Windows.Forms.ColumnStyle" /> for each column in the <see cref="T:System.Windows.Forms.TableLayoutPanel" /> control.</returns>
		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x06003BBF RID: 15295 RVA: 0x0010562D File Offset: 0x0010382D
		[SRDescription("GridPanelColumnStylesDescr")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[SRCategory("CatLayout")]
		[DisplayName("Columns")]
		[Browsable(false)]
		[MergableProperty(false)]
		public TableLayoutColumnStyleCollection ColumnStyles
		{
			get
			{
				return this._tableLayoutSettings.ColumnStyles;
			}
		}

		/// <summary>Creates a new instance of the control collection for the control.</summary>
		/// <returns>A new instance of <see cref="T:System.Windows.Forms.Control.ControlCollection" /> assigned to the control.</returns>
		// Token: 0x06003BC0 RID: 15296 RVA: 0x0010563A File Offset: 0x0010383A
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override Control.ControlCollection CreateControlsInstance()
		{
			return new TableLayoutControlCollection(this);
		}

		// Token: 0x06003BC1 RID: 15297 RVA: 0x00105644 File Offset: 0x00103844
		private bool ShouldSerializeControls()
		{
			TableLayoutControlCollection controls = this.Controls;
			return controls != null && controls.Count > 0;
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IExtenderProvider.CanExtend(System.Object)" />.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> to receive the extender properties.</param>
		/// <returns>
		///   <see langword="true" /> if this object can provide extender properties to the specified object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003BC2 RID: 15298 RVA: 0x00105668 File Offset: 0x00103868
		bool IExtenderProvider.CanExtend(object obj)
		{
			Control control = obj as Control;
			return control != null && control.Parent == this;
		}

		/// <summary>Returns the number of columns spanned by the specified child control.</summary>
		/// <param name="control">A child control of the <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</param>
		/// <returns>The number of columns spanned by the child control. The default is 1.</returns>
		// Token: 0x06003BC3 RID: 15299 RVA: 0x0010568A File Offset: 0x0010388A
		[SRDescription("GridPanelGetColumnSpanDescr")]
		[DefaultValue(1)]
		[SRCategory("CatLayout")]
		[DisplayName("ColumnSpan")]
		public int GetColumnSpan(Control control)
		{
			return this._tableLayoutSettings.GetColumnSpan(control);
		}

		/// <summary>Sets the number of columns spanned by the child control.</summary>
		/// <param name="control">A child control of the <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</param>
		/// <param name="value">The number of columns to span.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> is less than 1.</exception>
		// Token: 0x06003BC4 RID: 15300 RVA: 0x00105698 File Offset: 0x00103898
		public void SetColumnSpan(Control control, int value)
		{
			this._tableLayoutSettings.SetColumnSpan(control, value);
		}

		/// <summary>Returns the number of rows spanned by the specified child control.</summary>
		/// <param name="control">A child control of the <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</param>
		/// <returns>The number of rows spanned by the child control. The default is 1.</returns>
		// Token: 0x06003BC5 RID: 15301 RVA: 0x001056A7 File Offset: 0x001038A7
		[SRDescription("GridPanelGetRowSpanDescr")]
		[DefaultValue(1)]
		[SRCategory("CatLayout")]
		[DisplayName("RowSpan")]
		public int GetRowSpan(Control control)
		{
			return this._tableLayoutSettings.GetRowSpan(control);
		}

		/// <summary>Sets the number of rows spanned by the child control.</summary>
		/// <param name="control">A child control of the <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</param>
		/// <param name="value">The number of rows to span.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> is less than 1.</exception>
		// Token: 0x06003BC6 RID: 15302 RVA: 0x001056B5 File Offset: 0x001038B5
		public void SetRowSpan(Control control, int value)
		{
			this._tableLayoutSettings.SetRowSpan(control, value);
		}

		/// <summary>Returns the row position of the specified child control.</summary>
		/// <param name="control">A child control of the <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</param>
		/// <returns>The row position of <paramref name="control" />, or -1 if the position of <paramref name="control" /> is determined by <see cref="P:System.Windows.Forms.TableLayoutPanel.LayoutEngine" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="control" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="control" /> is not a type that can be arranged by this <see cref="T:System.Windows.Forms.Layout.LayoutEngine" />.</exception>
		// Token: 0x06003BC7 RID: 15303 RVA: 0x001056C4 File Offset: 0x001038C4
		[DefaultValue(-1)]
		[SRDescription("GridPanelRowDescr")]
		[SRCategory("CatLayout")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DisplayName("Row")]
		public int GetRow(Control control)
		{
			return this._tableLayoutSettings.GetRow(control);
		}

		/// <summary>Sets the row position of the specified child control.</summary>
		/// <param name="control">The control to move to another row.</param>
		/// <param name="row">The row to which <paramref name="control" /> will be moved.</param>
		// Token: 0x06003BC8 RID: 15304 RVA: 0x001056D2 File Offset: 0x001038D2
		public void SetRow(Control control, int row)
		{
			this._tableLayoutSettings.SetRow(control, row);
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> that represents the row and the column of the cell.</summary>
		/// <param name="control">A control contained within a cell.</param>
		/// <returns>A <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> that represents the cell position.</returns>
		// Token: 0x06003BC9 RID: 15305 RVA: 0x001056E1 File Offset: 0x001038E1
		[DefaultValue(typeof(TableLayoutPanelCellPosition), "-1,-1")]
		[SRDescription("GridPanelCellPositionDescr")]
		[SRCategory("CatLayout")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DisplayName("Cell")]
		public TableLayoutPanelCellPosition GetCellPosition(Control control)
		{
			return this._tableLayoutSettings.GetCellPosition(control);
		}

		/// <summary>Sets the <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> that represents the row and the column of the cell.</summary>
		/// <param name="control">A control contained within a cell.</param>
		/// <param name="position">A <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> that represents the row and the column of the cell.</param>
		// Token: 0x06003BCA RID: 15306 RVA: 0x001056EF File Offset: 0x001038EF
		public void SetCellPosition(Control control, TableLayoutPanelCellPosition position)
		{
			this._tableLayoutSettings.SetCellPosition(control, position);
		}

		/// <summary>Returns the column position of the specified child control.</summary>
		/// <param name="control">A child control of the <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</param>
		/// <returns>The column position of the specified child control, or -1 if the position of <paramref name="control" /> is determined by <see cref="P:System.Windows.Forms.TableLayoutPanel.LayoutEngine" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="control" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="control" /> is not a type that can be arranged by this <see cref="T:System.Windows.Forms.Layout.LayoutEngine" />.</exception>
		// Token: 0x06003BCB RID: 15307 RVA: 0x001056FE File Offset: 0x001038FE
		[DefaultValue(-1)]
		[SRDescription("GridPanelColumnDescr")]
		[SRCategory("CatLayout")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DisplayName("Column")]
		public int GetColumn(Control control)
		{
			return this._tableLayoutSettings.GetColumn(control);
		}

		/// <summary>Sets the column position of the specified child control.</summary>
		/// <param name="control">The control to move to another column.</param>
		/// <param name="column">The column to which <paramref name="control" /> will be moved.</param>
		// Token: 0x06003BCC RID: 15308 RVA: 0x0010570C File Offset: 0x0010390C
		public void SetColumn(Control control, int column)
		{
			this._tableLayoutSettings.SetColumn(control, column);
		}

		/// <summary>Returns the child control occupying the specified position.</summary>
		/// <param name="column">The column position of the control to retrieve.</param>
		/// <param name="row">The row position of the control to retrieve.</param>
		/// <returns>The child control occupying the specified cell; otherwise, <see langword="null" /> if no control exists at the specified column and row, or if the control has its <see cref="P:System.Windows.Forms.Control.Visible" /> property set to <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">Either <paramref name="column" /> or <paramref name="row" /> (or both) is less than 0.</exception>
		// Token: 0x06003BCD RID: 15309 RVA: 0x0010571B File Offset: 0x0010391B
		public Control GetControlFromPosition(int column, int row)
		{
			return (Control)this._tableLayoutSettings.GetControlFromPosition(column, row);
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> that represents the row and the column of the cell that contains the control.</summary>
		/// <param name="control">A control contained within a cell.</param>
		/// <returns>A <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> that represents the cell position.</returns>
		// Token: 0x06003BCE RID: 15310 RVA: 0x0010572F File Offset: 0x0010392F
		public TableLayoutPanelCellPosition GetPositionFromControl(Control control)
		{
			return this._tableLayoutSettings.GetPositionFromControl(control);
		}

		/// <summary>Returns an array representing the widths, in pixels, of the columns in the <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</summary>
		/// <returns>An array of type <see cref="T:System.Int32" /> that contains the widths, in pixels, of the columns in the <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</returns>
		// Token: 0x06003BCF RID: 15311 RVA: 0x00105740 File Offset: 0x00103940
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int[] GetColumnWidths()
		{
			TableLayout.ContainerInfo containerInfo = TableLayout.GetContainerInfo(this);
			if (containerInfo.Columns == null)
			{
				return new int[0];
			}
			int[] array = new int[containerInfo.Columns.Length];
			for (int i = 0; i < containerInfo.Columns.Length; i++)
			{
				array[i] = containerInfo.Columns[i].MinSize;
			}
			return array;
		}

		/// <summary>Returns an array representing the heights, in pixels, of the rows in the <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</summary>
		/// <returns>An array of type <see cref="T:System.Int32" /> that contains the heights, in pixels, of the rows in the <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</returns>
		// Token: 0x06003BD0 RID: 15312 RVA: 0x0010579C File Offset: 0x0010399C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int[] GetRowHeights()
		{
			TableLayout.ContainerInfo containerInfo = TableLayout.GetContainerInfo(this);
			if (containerInfo.Rows == null)
			{
				return new int[0];
			}
			int[] array = new int[containerInfo.Rows.Length];
			for (int i = 0; i < containerInfo.Rows.Length; i++)
			{
				array[i] = containerInfo.Rows[i].MinSize;
			}
			return array;
		}

		/// <summary>Occurs when the cell is redrawn.</summary>
		// Token: 0x140002DE RID: 734
		// (add) Token: 0x06003BD1 RID: 15313 RVA: 0x001057F5 File Offset: 0x001039F5
		// (remove) Token: 0x06003BD2 RID: 15314 RVA: 0x00105808 File Offset: 0x00103A08
		[SRCategory("CatAppearance")]
		[SRDescription("TableLayoutPanelOnPaintCellDescr")]
		public event TableLayoutCellPaintEventHandler CellPaint
		{
			add
			{
				base.Events.AddHandler(TableLayoutPanel.EventCellPaint, value);
			}
			remove
			{
				base.Events.RemoveHandler(TableLayoutPanel.EventCellPaint, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
		/// <param name="levent">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
		// Token: 0x06003BD3 RID: 15315 RVA: 0x0010581B File Offset: 0x00103A1B
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnLayout(LayoutEventArgs levent)
		{
			base.OnLayout(levent);
			base.Invalidate();
		}

		/// <summary>Receives a call when the cell should be refreshed.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TableLayoutCellPaintEventArgs" /> that provides data for the event.</param>
		// Token: 0x06003BD4 RID: 15316 RVA: 0x0010582C File Offset: 0x00103A2C
		protected virtual void OnCellPaint(TableLayoutCellPaintEventArgs e)
		{
			TableLayoutCellPaintEventHandler tableLayoutCellPaintEventHandler = (TableLayoutCellPaintEventHandler)base.Events[TableLayoutPanel.EventCellPaint];
			if (tableLayoutCellPaintEventHandler != null)
			{
				tableLayoutCellPaintEventHandler(this, e);
			}
		}

		/// <summary>Paints the background of the panel.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains information about the panel to paint.</param>
		// Token: 0x06003BD5 RID: 15317 RVA: 0x0010585C File Offset: 0x00103A5C
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);
			int cellBorderWidth = this.CellBorderWidth;
			TableLayout.ContainerInfo containerInfo = TableLayout.GetContainerInfo(this);
			TableLayout.Strip[] columns = containerInfo.Columns;
			TableLayout.Strip[] rows = containerInfo.Rows;
			TableLayoutPanelCellBorderStyle cellBorderStyle = this.CellBorderStyle;
			if (columns == null || rows == null)
			{
				return;
			}
			int num = columns.Length;
			int num2 = rows.Length;
			int num3 = 0;
			int num4 = 0;
			Graphics graphics = e.Graphics;
			Rectangle displayRectangle = this.DisplayRectangle;
			Rectangle clipRectangle = e.ClipRectangle;
			bool flag = this.RightToLeft == RightToLeft.Yes;
			int num5;
			if (flag)
			{
				num5 = displayRectangle.Right - cellBorderWidth / 2;
			}
			else
			{
				num5 = displayRectangle.X + cellBorderWidth / 2;
			}
			for (int i = 0; i < num; i++)
			{
				int num6 = displayRectangle.Y + cellBorderWidth / 2;
				if (flag)
				{
					num5 -= columns[i].MinSize;
				}
				for (int j = 0; j < num2; j++)
				{
					int num7 = num5;
					int num8 = num6;
					TableLayout.Strip strip = columns[i];
					int minSize = strip.MinSize;
					strip = rows[j];
					Rectangle rectangle = new Rectangle(num7, num8, minSize, strip.MinSize);
					Rectangle rectangle2 = new Rectangle(rectangle.X + (cellBorderWidth + 1) / 2, rectangle.Y + (cellBorderWidth + 1) / 2, rectangle.Width - (cellBorderWidth + 1) / 2, rectangle.Height - (cellBorderWidth + 1) / 2);
					if (clipRectangle.IntersectsWith(rectangle2))
					{
						using (TableLayoutCellPaintEventArgs tableLayoutCellPaintEventArgs = new TableLayoutCellPaintEventArgs(graphics, clipRectangle, rectangle2, i, j))
						{
							this.OnCellPaint(tableLayoutCellPaintEventArgs);
						}
						ControlPaint.PaintTableCellBorder(cellBorderStyle, graphics, rectangle);
					}
					num6 += rows[j].MinSize;
					if (i == 0)
					{
						num4 += rows[j].MinSize;
					}
				}
				if (!flag)
				{
					num5 += columns[i].MinSize;
				}
				num3 += columns[i].MinSize;
			}
			if (!base.HScroll && !base.VScroll && cellBorderStyle != TableLayoutPanelCellBorderStyle.None)
			{
				Rectangle rectangle3 = new Rectangle(cellBorderWidth / 2 + displayRectangle.X, cellBorderWidth / 2 + displayRectangle.Y, displayRectangle.Width - cellBorderWidth, displayRectangle.Height - cellBorderWidth);
				if (cellBorderStyle == TableLayoutPanelCellBorderStyle.Inset)
				{
					graphics.DrawLine(SystemPens.ControlDark, rectangle3.Right, rectangle3.Y, rectangle3.Right, rectangle3.Bottom);
					graphics.DrawLine(SystemPens.ControlDark, rectangle3.X, rectangle3.Y + rectangle3.Height - 1, rectangle3.X + rectangle3.Width - 1, rectangle3.Y + rectangle3.Height - 1);
				}
				else
				{
					if (cellBorderStyle == TableLayoutPanelCellBorderStyle.Outset)
					{
						using (Pen pen = new Pen(SystemColors.Window))
						{
							graphics.DrawLine(pen, rectangle3.X + rectangle3.Width - 1, rectangle3.Y, rectangle3.X + rectangle3.Width - 1, rectangle3.Y + rectangle3.Height - 1);
							graphics.DrawLine(pen, rectangle3.X, rectangle3.Y + rectangle3.Height - 1, rectangle3.X + rectangle3.Width - 1, rectangle3.Y + rectangle3.Height - 1);
							goto IL_342;
						}
					}
					ControlPaint.PaintTableCellBorder(cellBorderStyle, graphics, rectangle3);
				}
				IL_342:
				ControlPaint.PaintTableControlBorder(cellBorderStyle, graphics, displayRectangle);
				return;
			}
			ControlPaint.PaintTableControlBorder(cellBorderStyle, graphics, displayRectangle);
		}

		/// <summary>Performs the work of scaling the entire panel and any child controls.</summary>
		/// <param name="dx">The ratio by which to scale the control horizontally.</param>
		/// <param name="dy">The ratio by which to scale the control vertically</param>
		// Token: 0x06003BD6 RID: 15318 RVA: 0x00105BE0 File Offset: 0x00103DE0
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void ScaleCore(float dx, float dy)
		{
			base.ScaleCore(dx, dy);
			this.ScaleAbsoluteStyles(new SizeF(dx, dy));
		}

		/// <summary>Scales a control's location, size, padding and margin.</summary>
		/// <param name="factor">The height and width of the control's bounds.</param>
		/// <param name="specified">One of the values of <see cref="T:System.Windows.Forms.BoundsSpecified" /> that specifies the bounds of the control to use when defining its size and position.</param>
		// Token: 0x06003BD7 RID: 15319 RVA: 0x00105BF7 File Offset: 0x00103DF7
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			base.ScaleControl(factor, specified);
			this.ScaleAbsoluteStyles(factor);
		}

		// Token: 0x06003BD8 RID: 15320 RVA: 0x00105C08 File Offset: 0x00103E08
		private void ScaleAbsoluteStyles(SizeF factor)
		{
			TableLayout.ContainerInfo containerInfo = TableLayout.GetContainerInfo(this);
			int num = 0;
			int num2 = -1;
			int num3 = containerInfo.Rows.Length - 1;
			if (containerInfo.Rows.Length != 0)
			{
				num2 = containerInfo.Rows[num3].MinSize;
			}
			int num4 = -1;
			int num5 = containerInfo.Columns.Length - 1;
			if (containerInfo.Columns.Length != 0)
			{
				num4 = containerInfo.Columns[containerInfo.Columns.Length - 1].MinSize;
			}
			foreach (object obj in ((IEnumerable)this.ColumnStyles))
			{
				ColumnStyle columnStyle = (ColumnStyle)obj;
				if (columnStyle.SizeType == SizeType.Absolute)
				{
					if (num == num5 && num4 > 0)
					{
						columnStyle.Width = (float)Math.Round((double)((float)num4 * factor.Width));
					}
					else
					{
						columnStyle.Width = (float)Math.Round((double)(columnStyle.Width * factor.Width));
					}
				}
				num++;
			}
			num = 0;
			foreach (object obj2 in ((IEnumerable)this.RowStyles))
			{
				RowStyle rowStyle = (RowStyle)obj2;
				if (rowStyle.SizeType == SizeType.Absolute)
				{
					if (num == num3 && num2 > 0)
					{
						rowStyle.Height = (float)Math.Round((double)((float)num2 * factor.Height));
					}
					else
					{
						rowStyle.Height = (float)Math.Round((double)(rowStyle.Height * factor.Height));
					}
				}
			}
		}

		// Token: 0x04002375 RID: 9077
		private TableLayoutSettings _tableLayoutSettings;

		// Token: 0x04002376 RID: 9078
		private static readonly object EventCellPaint = new object();
	}
}
