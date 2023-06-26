using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace System.Windows.Forms
{
	/// <summary>Specifies a column in which each cell contains a check box for representing a Boolean value.</summary>
	// Token: 0x0200017B RID: 379
	public class DataGridBoolColumn : DataGridColumnStyle
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridBoolColumn" /> class.</summary>
		// Token: 0x060015DD RID: 5597 RVA: 0x0004EF20 File Offset: 0x0004D120
		public DataGridBoolColumn()
		{
		}

		/// <summary>Initializes a new instance of a <see cref="T:System.Windows.Forms.DataGridBoolColumn" /> with the specified <see cref="T:System.ComponentModel.PropertyDescriptor" />.</summary>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> associated with the column.</param>
		// Token: 0x060015DE RID: 5598 RVA: 0x0004EF70 File Offset: 0x0004D170
		public DataGridBoolColumn(PropertyDescriptor prop)
			: base(prop)
		{
		}

		/// <summary>Initializes a new instance of a <see cref="T:System.Windows.Forms.DataGridBoolColumn" /> with the specified <see cref="T:System.ComponentModel.PropertyDescriptor" />, and specifying whether the column style is a default column.</summary>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> associated with the column.</param>
		/// <param name="isDefault">
		///   <see langword="true" /> to specify the column as the default; otherwise, <see langword="false" />.</param>
		// Token: 0x060015DF RID: 5599 RVA: 0x0004EFC0 File Offset: 0x0004D1C0
		public DataGridBoolColumn(PropertyDescriptor prop, bool isDefault)
			: base(prop, isDefault)
		{
		}

		/// <summary>Gets or sets the actual value used when setting the value of the column to <see langword="true" />.</summary>
		/// <returns>The value, typed as <see cref="T:System.Object" />.</returns>
		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x060015E0 RID: 5600 RVA: 0x0004F011 File Offset: 0x0004D211
		// (set) Token: 0x060015E1 RID: 5601 RVA: 0x0004F019 File Offset: 0x0004D219
		[TypeConverter(typeof(StringConverter))]
		[DefaultValue(true)]
		public object TrueValue
		{
			get
			{
				return this.trueValue;
			}
			set
			{
				if (!this.trueValue.Equals(value))
				{
					this.trueValue = value;
					this.OnTrueValueChanged(EventArgs.Empty);
					this.Invalidate();
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridBoolColumn.TrueValue" /> property value is changed.</summary>
		// Token: 0x140000E1 RID: 225
		// (add) Token: 0x060015E2 RID: 5602 RVA: 0x0004F041 File Offset: 0x0004D241
		// (remove) Token: 0x060015E3 RID: 5603 RVA: 0x0004F054 File Offset: 0x0004D254
		public event EventHandler TrueValueChanged
		{
			add
			{
				base.Events.AddHandler(DataGridBoolColumn.EventTrueValue, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridBoolColumn.EventTrueValue, value);
			}
		}

		/// <summary>Gets or sets the actual value used when setting the value of the column to <see langword="false" />.</summary>
		/// <returns>The value, typed as <see cref="T:System.Object" />.</returns>
		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x060015E4 RID: 5604 RVA: 0x0004F067 File Offset: 0x0004D267
		// (set) Token: 0x060015E5 RID: 5605 RVA: 0x0004F06F File Offset: 0x0004D26F
		[TypeConverter(typeof(StringConverter))]
		[DefaultValue(false)]
		public object FalseValue
		{
			get
			{
				return this.falseValue;
			}
			set
			{
				if (!this.falseValue.Equals(value))
				{
					this.falseValue = value;
					this.OnFalseValueChanged(EventArgs.Empty);
					this.Invalidate();
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridBoolColumn.FalseValue" /> property is changed.</summary>
		// Token: 0x140000E2 RID: 226
		// (add) Token: 0x060015E6 RID: 5606 RVA: 0x0004F097 File Offset: 0x0004D297
		// (remove) Token: 0x060015E7 RID: 5607 RVA: 0x0004F0AA File Offset: 0x0004D2AA
		public event EventHandler FalseValueChanged
		{
			add
			{
				base.Events.AddHandler(DataGridBoolColumn.EventFalseValue, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridBoolColumn.EventFalseValue, value);
			}
		}

		/// <summary>Gets or sets the actual value used when setting the value of the column to <see cref="F:System.DBNull.Value" />.</summary>
		/// <returns>The value, typed as <see cref="T:System.Object" />.</returns>
		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x060015E8 RID: 5608 RVA: 0x0004F0BD File Offset: 0x0004D2BD
		// (set) Token: 0x060015E9 RID: 5609 RVA: 0x0004F0C5 File Offset: 0x0004D2C5
		[TypeConverter(typeof(StringConverter))]
		public object NullValue
		{
			get
			{
				return this.nullValue;
			}
			set
			{
				if (!this.nullValue.Equals(value))
				{
					this.nullValue = value;
					this.OnFalseValueChanged(EventArgs.Empty);
					this.Invalidate();
				}
			}
		}

		/// <summary>Notifies a column that it must relinquish the focus to the control it is hosting.</summary>
		// Token: 0x060015EA RID: 5610 RVA: 0x0004F0ED File Offset: 0x0004D2ED
		protected internal override void ConcedeFocus()
		{
			base.ConcedeFocus();
			this.isSelected = false;
			this.isEditing = false;
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x0004F104 File Offset: 0x0004D304
		private Rectangle GetCheckBoxBounds(Rectangle bounds, bool alignToRight)
		{
			if (alignToRight)
			{
				return new Rectangle(bounds.X + (bounds.Width - DataGridBoolColumn.idealCheckSize) / 2, bounds.Y + (bounds.Height - DataGridBoolColumn.idealCheckSize) / 2, (bounds.Width < DataGridBoolColumn.idealCheckSize) ? bounds.Width : DataGridBoolColumn.idealCheckSize, DataGridBoolColumn.idealCheckSize);
			}
			return new Rectangle(Math.Max(0, bounds.X + (bounds.Width - DataGridBoolColumn.idealCheckSize) / 2), Math.Max(0, bounds.Y + (bounds.Height - DataGridBoolColumn.idealCheckSize) / 2), (bounds.Width < DataGridBoolColumn.idealCheckSize) ? bounds.Width : DataGridBoolColumn.idealCheckSize, DataGridBoolColumn.idealCheckSize);
		}

		/// <summary>Gets the value at the specified row.</summary>
		/// <param name="lm">The <see cref="T:System.Windows.Forms.CurrencyManager" /> for the column.</param>
		/// <param name="row">The row number.</param>
		/// <returns>The value, typed as <see cref="T:System.Object" />.</returns>
		// Token: 0x060015EC RID: 5612 RVA: 0x0004F1CC File Offset: 0x0004D3CC
		protected internal override object GetColumnValueAtRow(CurrencyManager lm, int row)
		{
			object columnValueAtRow = base.GetColumnValueAtRow(lm, row);
			object obj = Convert.DBNull;
			if (columnValueAtRow.Equals(this.trueValue))
			{
				obj = true;
			}
			else if (columnValueAtRow.Equals(this.falseValue))
			{
				obj = false;
			}
			return obj;
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x0004F218 File Offset: 0x0004D418
		private bool IsReadOnly()
		{
			bool flag = this.ReadOnly;
			if (this.DataGridTableStyle != null)
			{
				flag = flag || this.DataGridTableStyle.ReadOnly;
				if (this.DataGridTableStyle.DataGrid != null)
				{
					flag = flag || this.DataGridTableStyle.DataGrid.ReadOnly;
				}
			}
			return flag;
		}

		/// <summary>Sets the value of a specified row.</summary>
		/// <param name="lm">The <see cref="T:System.Windows.Forms.CurrencyManager" /> for the column.</param>
		/// <param name="row">The row number.</param>
		/// <param name="value">The value to set, typed as <see cref="T:System.Object" />.</param>
		// Token: 0x060015EE RID: 5614 RVA: 0x0004F26C File Offset: 0x0004D46C
		protected internal override void SetColumnValueAtRow(CurrencyManager lm, int row, object value)
		{
			object obj = null;
			if (true.Equals(value))
			{
				obj = this.TrueValue;
			}
			else if (false.Equals(value))
			{
				obj = this.FalseValue;
			}
			else if (Convert.IsDBNull(value))
			{
				obj = this.NullValue;
			}
			this.currentValue = obj;
			base.SetColumnValueAtRow(lm, row, obj);
		}

		/// <summary>Gets the optimum width and height of a cell given a specific value to contain.</summary>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> that draws the cell.</param>
		/// <param name="value">The value that must fit in the cell.</param>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that contains the drawing information for the cell.</returns>
		// Token: 0x060015EF RID: 5615 RVA: 0x0004F2C4 File Offset: 0x0004D4C4
		protected internal override Size GetPreferredSize(Graphics g, object value)
		{
			return new Size(DataGridBoolColumn.idealCheckSize + 2, DataGridBoolColumn.idealCheckSize + 2);
		}

		/// <summary>Gets the height of a cell in a column.</summary>
		/// <returns>The height of the column. The default is 16.</returns>
		// Token: 0x060015F0 RID: 5616 RVA: 0x0004F2D9 File Offset: 0x0004D4D9
		protected internal override int GetMinimumHeight()
		{
			return DataGridBoolColumn.idealCheckSize + 2;
		}

		/// <summary>Gets the height used when resizing columns.</summary>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> that draws on the screen.</param>
		/// <param name="value">An <see cref="T:System.Object" /> that contains the value to be drawn to the screen.</param>
		/// <returns>The height used to automatically resize cells in a column.</returns>
		// Token: 0x060015F1 RID: 5617 RVA: 0x0004F2D9 File Offset: 0x0004D4D9
		protected internal override int GetPreferredHeight(Graphics g, object value)
		{
			return DataGridBoolColumn.idealCheckSize + 2;
		}

		/// <summary>Initiates a request to interrupt an edit procedure.</summary>
		/// <param name="rowNum">The number of the row in which an operation is being interrupted.</param>
		// Token: 0x060015F2 RID: 5618 RVA: 0x0004F2E2 File Offset: 0x0004D4E2
		protected internal override void Abort(int rowNum)
		{
			this.isSelected = false;
			this.isEditing = false;
			this.Invalidate();
		}

		/// <summary>Initiates a request to complete an editing procedure.</summary>
		/// <param name="dataSource">The <see cref="T:System.Data.DataView" /> of the edited column.</param>
		/// <param name="rowNum">The number of the edited row.</param>
		/// <returns>
		///   <see langword="true" /> if the editing procedure committed successfully; otherwise, <see langword="false" />.</returns>
		// Token: 0x060015F3 RID: 5619 RVA: 0x0004F2F8 File Offset: 0x0004D4F8
		protected internal override bool Commit(CurrencyManager dataSource, int rowNum)
		{
			this.isSelected = false;
			this.Invalidate();
			if (!this.isEditing)
			{
				return true;
			}
			this.SetColumnValueAtRow(dataSource, rowNum, this.currentValue);
			this.isEditing = false;
			return true;
		}

		/// <summary>Prepares the cell for editing a value.</summary>
		/// <param name="source">The <see cref="T:System.Data.DataView" /> of the edited cell.</param>
		/// <param name="rowNum">The row number of the edited cell.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> in which the control is to be sited.</param>
		/// <param name="readOnly">
		///   <see langword="true" /> if the value is read only; otherwise, <see langword="false" />.</param>
		/// <param name="displayText">The text to display in the cell.</param>
		/// <param name="cellIsVisible">
		///   <see langword="true" /> to show the cell; otherwise, <see langword="false" />.</param>
		// Token: 0x060015F4 RID: 5620 RVA: 0x0004F328 File Offset: 0x0004D528
		protected internal override void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly, string displayText, bool cellIsVisible)
		{
			this.isSelected = true;
			DataGrid dataGrid = this.DataGridTableStyle.DataGrid;
			if (!dataGrid.Focused)
			{
				dataGrid.FocusInternal();
			}
			if (!readOnly && !this.IsReadOnly())
			{
				this.editingRow = rowNum;
				this.currentValue = this.GetColumnValueAtRow(source, rowNum);
			}
			base.Invalidate();
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x0004F37E File Offset: 0x0004D57E
		internal override bool KeyPress(int rowNum, Keys keyData)
		{
			if (this.isSelected && this.editingRow == rowNum && !this.IsReadOnly() && (keyData & Keys.KeyCode) == Keys.Space)
			{
				this.ToggleValue();
				this.Invalidate();
				return true;
			}
			return base.KeyPress(rowNum, keyData);
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x0004F3BA File Offset: 0x0004D5BA
		internal override bool MouseDown(int rowNum, int x, int y)
		{
			base.MouseDown(rowNum, x, y);
			if (this.isSelected && this.editingRow == rowNum && !this.IsReadOnly())
			{
				this.ToggleValue();
				this.Invalidate();
				return true;
			}
			return false;
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x0004F3F0 File Offset: 0x0004D5F0
		private void OnTrueValueChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGridBoolColumn.EventTrueValue] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x0004F420 File Offset: 0x0004D620
		private void OnFalseValueChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGridBoolColumn.EventFalseValue] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x0004F450 File Offset: 0x0004D650
		private void OnAllowNullChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGridBoolColumn.EventAllowNull] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Draws the <see cref="T:System.Windows.Forms.DataGridBoolColumn" /> with the given <see cref="T:System.Drawing.Graphics" />, <see cref="T:System.Drawing.Rectangle" /> and row number.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to draw to.</param>
		/// <param name="bounds">The bounding <see cref="T:System.Drawing.Rectangle" /> to paint into.</param>
		/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> of the column.</param>
		/// <param name="rowNum">The number of the row referred to in the underlying data.</param>
		// Token: 0x060015FA RID: 5626 RVA: 0x0004F47E File Offset: 0x0004D67E
		protected internal override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum)
		{
			this.Paint(g, bounds, source, rowNum, false);
		}

		/// <summary>Draws the <see cref="T:System.Windows.Forms.DataGridBoolColumn" /> with the given <see cref="T:System.Drawing.Graphics" />, <see cref="T:System.Drawing.Rectangle" />, row number, and alignment settings.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to draw to.</param>
		/// <param name="bounds">The bounding <see cref="T:System.Drawing.Rectangle" /> to paint into.</param>
		/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> of the column.</param>
		/// <param name="rowNum">The number of the row in the underlying data table being referred to.</param>
		/// <param name="alignToRight">A value indicating whether to align the content to the right. <see langword="true" /> if the content is aligned to the right, otherwise, <see langword="false" />.</param>
		// Token: 0x060015FB RID: 5627 RVA: 0x0004F48C File Offset: 0x0004D68C
		protected internal override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, bool alignToRight)
		{
			this.Paint(g, bounds, source, rowNum, this.DataGridTableStyle.BackBrush, this.DataGridTableStyle.ForeBrush, alignToRight);
		}

		/// <summary>Draws the <see cref="T:System.Windows.Forms.DataGridBoolColumn" /> with the given <see cref="T:System.Drawing.Graphics" />, <see cref="T:System.Drawing.Rectangle" />, row number, <see cref="T:System.Drawing.Brush" />, and <see cref="T:System.Drawing.Color" />.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to draw to.</param>
		/// <param name="bounds">The bounding <see cref="T:System.Drawing.Rectangle" /> to paint into.</param>
		/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> of the column.</param>
		/// <param name="rowNum">The number of the row in the underlying data table being referred to.</param>
		/// <param name="backBrush">A <see cref="T:System.Drawing.Brush" /> used to paint the background color.</param>
		/// <param name="foreBrush">A <see cref="T:System.Drawing.Color" /> used to paint the foreground color.</param>
		/// <param name="alignToRight">A value indicating whether to align the content to the right. <see langword="true" /> if the content is aligned to the right, otherwise, <see langword="false" />.</param>
		// Token: 0x060015FC RID: 5628 RVA: 0x0004F4B4 File Offset: 0x0004D6B4
		protected internal override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, Brush backBrush, Brush foreBrush, bool alignToRight)
		{
			object obj = ((this.isEditing && this.editingRow == rowNum) ? this.currentValue : this.GetColumnValueAtRow(source, rowNum));
			ButtonState buttonState = ButtonState.Inactive;
			if (!Convert.IsDBNull(obj))
			{
				buttonState = (((bool)obj) ? ButtonState.Checked : ButtonState.Normal);
			}
			Rectangle checkBoxBounds = this.GetCheckBoxBounds(bounds, alignToRight);
			Region clip = g.Clip;
			g.ExcludeClip(checkBoxBounds);
			Brush brush = (this.DataGridTableStyle.IsDefault ? this.DataGridTableStyle.DataGrid.SelectionBackBrush : this.DataGridTableStyle.SelectionBackBrush);
			if (this.isSelected && this.editingRow == rowNum && !this.IsReadOnly())
			{
				g.FillRectangle(brush, bounds);
			}
			else
			{
				g.FillRectangle(backBrush, bounds);
			}
			g.Clip = clip;
			if (buttonState == ButtonState.Inactive)
			{
				ControlPaint.DrawMixedCheckBox(g, checkBoxBounds, ButtonState.Checked);
			}
			else
			{
				ControlPaint.DrawCheckBox(g, checkBoxBounds, buttonState);
			}
			if (this.IsReadOnly() && this.isSelected && source.Position == rowNum)
			{
				bounds.Inflate(-1, -1);
				Pen pen = new Pen(brush);
				pen.DashStyle = DashStyle.Dash;
				g.DrawRectangle(pen, bounds);
				pen.Dispose();
				bounds.Inflate(1, 1);
			}
		}

		/// <summary>Gets or sets a value indicating whether null values are allowed.</summary>
		/// <returns>
		///   <see langword="true" /> if null values are allowed, otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x060015FD RID: 5629 RVA: 0x0004F5E8 File Offset: 0x0004D7E8
		// (set) Token: 0x060015FE RID: 5630 RVA: 0x0004F5F0 File Offset: 0x0004D7F0
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("DataGridBoolColumnAllowNullValue")]
		public bool AllowNull
		{
			get
			{
				return this.allowNull;
			}
			set
			{
				if (this.allowNull != value)
				{
					this.allowNull = value;
					if (!value && Convert.IsDBNull(this.currentValue))
					{
						this.currentValue = false;
						this.Invalidate();
					}
					this.OnAllowNullChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridBoolColumn.AllowNull" /> property is changed.</summary>
		// Token: 0x140000E3 RID: 227
		// (add) Token: 0x060015FF RID: 5631 RVA: 0x0004F62F File Offset: 0x0004D82F
		// (remove) Token: 0x06001600 RID: 5632 RVA: 0x0004F642 File Offset: 0x0004D842
		public event EventHandler AllowNullChanged
		{
			add
			{
				base.Events.AddHandler(DataGridBoolColumn.EventAllowNull, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridBoolColumn.EventAllowNull, value);
			}
		}

		/// <summary>Enters a <see cref="F:System.DBNull.Value" /> into the column.</summary>
		/// <exception cref="T:System.Exception">The <see cref="P:System.Windows.Forms.DataGridBoolColumn.AllowNull" /> property is set to <see langword="false" />.</exception>
		// Token: 0x06001601 RID: 5633 RVA: 0x0004F655 File Offset: 0x0004D855
		protected internal override void EnterNullValue()
		{
			if (!this.AllowNull || this.IsReadOnly())
			{
				return;
			}
			if (this.currentValue != Convert.DBNull)
			{
				this.currentValue = Convert.DBNull;
				this.Invalidate();
			}
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x0004F686 File Offset: 0x0004D886
		private void ResetNullValue()
		{
			this.NullValue = Convert.DBNull;
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x0004F693 File Offset: 0x0004D893
		private bool ShouldSerializeNullValue()
		{
			return this.nullValue != Convert.DBNull;
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x0004F6A8 File Offset: 0x0004D8A8
		private void ToggleValue()
		{
			if (this.currentValue is bool && !(bool)this.currentValue)
			{
				this.currentValue = true;
			}
			else if (this.AllowNull)
			{
				if (Convert.IsDBNull(this.currentValue))
				{
					this.currentValue = false;
				}
				else
				{
					this.currentValue = Convert.DBNull;
				}
			}
			else
			{
				this.currentValue = false;
			}
			this.isEditing = true;
			this.DataGridTableStyle.DataGrid.ColumnStartedEditing(Rectangle.Empty);
		}

		// Token: 0x04000A04 RID: 2564
		private static readonly int idealCheckSize = 14;

		// Token: 0x04000A05 RID: 2565
		private bool isEditing;

		// Token: 0x04000A06 RID: 2566
		private bool isSelected;

		// Token: 0x04000A07 RID: 2567
		private bool allowNull = true;

		// Token: 0x04000A08 RID: 2568
		private int editingRow = -1;

		// Token: 0x04000A09 RID: 2569
		private object currentValue = Convert.DBNull;

		// Token: 0x04000A0A RID: 2570
		private object trueValue = true;

		// Token: 0x04000A0B RID: 2571
		private object falseValue = false;

		// Token: 0x04000A0C RID: 2572
		private object nullValue = Convert.DBNull;

		// Token: 0x04000A0D RID: 2573
		private static readonly object EventTrueValue = new object();

		// Token: 0x04000A0E RID: 2574
		private static readonly object EventFalseValue = new object();

		// Token: 0x04000A0F RID: 2575
		private static readonly object EventAllowNull = new object();
	}
}
