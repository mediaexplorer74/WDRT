using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Reflection;

namespace System.Windows.Forms
{
	/// <summary>Hosts a <see cref="T:System.Windows.Forms.TextBox" /> control in a cell of a <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> for editing strings.</summary>
	// Token: 0x0200018C RID: 396
	public class DataGridTextBoxColumn : DataGridColumnStyle
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" /> class.</summary>
		// Token: 0x06001846 RID: 6214 RVA: 0x0005725D File Offset: 0x0005545D
		public DataGridTextBoxColumn()
			: this(null, null)
		{
		}

		/// <summary>Initializes a new instance of a <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" /> with a specified <see cref="T:System.ComponentModel.PropertyDescriptor" />.</summary>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> for the column with which the <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" /> will be associated.</param>
		// Token: 0x06001847 RID: 6215 RVA: 0x00057267 File Offset: 0x00055467
		public DataGridTextBoxColumn(PropertyDescriptor prop)
			: this(prop, null, false)
		{
		}

		/// <summary>Initializes a new instance of a <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" /> with the specified <see cref="T:System.ComponentModel.PropertyDescriptor" /> and format.</summary>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> for the column with which the <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" /> will be associated.</param>
		/// <param name="format">The format used to format the column values.</param>
		// Token: 0x06001848 RID: 6216 RVA: 0x00057272 File Offset: 0x00055472
		public DataGridTextBoxColumn(PropertyDescriptor prop, string format)
			: this(prop, format, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" /> class with a specified <see cref="T:System.ComponentModel.PropertyDescriptor" /> and format. Specifies whether the column is the default column.</summary>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to be associated with the <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" />.</param>
		/// <param name="format">The format used.</param>
		/// <param name="isDefault">Specifies whether the <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" /> is the default column.</param>
		// Token: 0x06001849 RID: 6217 RVA: 0x00057280 File Offset: 0x00055480
		public DataGridTextBoxColumn(PropertyDescriptor prop, string format, bool isDefault)
			: base(prop, isDefault)
		{
			this.edit = new DataGridTextBox();
			this.edit.BorderStyle = BorderStyle.None;
			this.edit.Multiline = true;
			this.edit.AcceptsReturn = true;
			this.edit.Visible = false;
			this.Format = format;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" /> class using the specified <see cref="T:System.ComponentModel.PropertyDescriptor" />. Specifies whether the <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" /> is a default column.</summary>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to be associated with the <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" />.</param>
		/// <param name="isDefault">Specifies whether the <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" /> is a default column.</param>
		// Token: 0x0600184A RID: 6218 RVA: 0x000572EC File Offset: 0x000554EC
		public DataGridTextBoxColumn(PropertyDescriptor prop, bool isDefault)
			: this(prop, null, isDefault)
		{
		}

		/// <summary>Gets the hosted <see cref="T:System.Windows.Forms.TextBox" /> control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TextBox" /> control hosted by the column.</returns>
		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x0600184B RID: 6219 RVA: 0x000572F7 File Offset: 0x000554F7
		[Browsable(false)]
		public virtual TextBox TextBox
		{
			get
			{
				return this.edit;
			}
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x000572FF File Offset: 0x000554FF
		internal override bool KeyPress(int rowNum, Keys keyData)
		{
			return this.edit.IsInEditOrNavigateMode && base.KeyPress(rowNum, keyData);
		}

		/// <summary>Adds a <see cref="T:System.Windows.Forms.TextBox" /> control to the <see cref="T:System.Windows.Forms.DataGrid" /> control's <see cref="T:System.Windows.Forms.Control.ControlCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.DataGrid" /> control the <see cref="T:System.Windows.Forms.TextBox" /> control is added to.</param>
		// Token: 0x0600184D RID: 6221 RVA: 0x00057318 File Offset: 0x00055518
		protected override void SetDataGridInColumn(DataGrid value)
		{
			base.SetDataGridInColumn(value);
			if (this.edit.ParentInternal != null)
			{
				this.edit.ParentInternal.Controls.Remove(this.edit);
			}
			if (value != null)
			{
				value.Controls.Add(this.edit);
			}
			this.edit.SetDataGrid(value);
		}

		/// <summary>Gets or sets the <see cref="T:System.ComponentModel.PropertyDescriptor" /> for the <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" />.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that formats the values displayed in the column.</returns>
		// Token: 0x17000579 RID: 1401
		// (set) Token: 0x0600184E RID: 6222 RVA: 0x00057374 File Offset: 0x00055574
		[SRDescription("FormatControlFormatDescr")]
		[DefaultValue(null)]
		public override PropertyDescriptor PropertyDescriptor
		{
			set
			{
				base.PropertyDescriptor = value;
				if (this.PropertyDescriptor != null && this.PropertyDescriptor.PropertyType != typeof(object))
				{
					this.typeConverter = TypeDescriptor.GetConverter(this.PropertyDescriptor.PropertyType);
					this.parseMethod = this.PropertyDescriptor.PropertyType.GetMethod("Parse", new Type[]
					{
						typeof(string),
						typeof(IFormatProvider)
					});
				}
			}
		}

		/// <summary>Gets or sets the character(s) that specify how text is formatted.</summary>
		/// <returns>The character or characters that specify how text is formatted.</returns>
		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x0600184F RID: 6223 RVA: 0x000573FD File Offset: 0x000555FD
		// (set) Token: 0x06001850 RID: 6224 RVA: 0x00057408 File Offset: 0x00055608
		[DefaultValue(null)]
		[Editor("System.Windows.Forms.Design.DataGridColumnStyleFormatEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string Format
		{
			get
			{
				return this.format;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (this.format == null || !this.format.Equals(value))
				{
					this.format = value;
					if (this.format.Length == 0 && this.typeConverter != null && !this.typeConverter.CanConvertFrom(typeof(string)))
					{
						this.ReadOnly = true;
					}
					this.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the culture specific information used to determine how values are formatted.</summary>
		/// <returns>An object that implements the <see cref="T:System.IFormatProvider" /> interface, such as the <see cref="T:System.Globalization.CultureInfo" /> class.</returns>
		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001851 RID: 6225 RVA: 0x00057475 File Offset: 0x00055675
		// (set) Token: 0x06001852 RID: 6226 RVA: 0x0005747D File Offset: 0x0005567D
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IFormatProvider FormatInfo
		{
			get
			{
				return this.formatInfo;
			}
			set
			{
				if (this.formatInfo == null || !this.formatInfo.Equals(value))
				{
					this.formatInfo = value;
				}
			}
		}

		/// <summary>Sets a value indicating whether the text box column is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the text box column is read-only; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001853 RID: 6227 RVA: 0x0005749C File Offset: 0x0005569C
		// (set) Token: 0x06001854 RID: 6228 RVA: 0x000574A4 File Offset: 0x000556A4
		public override bool ReadOnly
		{
			get
			{
				return base.ReadOnly;
			}
			set
			{
				if (!value && (this.format == null || this.format.Length == 0) && this.typeConverter != null && !this.typeConverter.CanConvertFrom(typeof(string)))
				{
					return;
				}
				base.ReadOnly = value;
			}
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x000070A6 File Offset: 0x000052A6
		private void DebugOut(string s)
		{
		}

		/// <summary>Informs the column that the focus is being conceded.</summary>
		// Token: 0x06001856 RID: 6230 RVA: 0x000574F0 File Offset: 0x000556F0
		protected internal override void ConcedeFocus()
		{
			this.edit.Bounds = Rectangle.Empty;
		}

		/// <summary>Hides the <see cref="T:System.Windows.Forms.DataGridTextBox" /> control and moves the focus to the <see cref="T:System.Windows.Forms.DataGrid" /> control.</summary>
		// Token: 0x06001857 RID: 6231 RVA: 0x00057504 File Offset: 0x00055704
		protected void HideEditBox()
		{
			bool focused = this.edit.Focused;
			this.edit.Visible = false;
			if (focused && this.DataGridTableStyle != null && this.DataGridTableStyle.DataGrid != null && this.DataGridTableStyle.DataGrid.CanFocus)
			{
				this.DataGridTableStyle.DataGrid.FocusInternal();
			}
		}

		/// <summary>Updates the user interface.</summary>
		/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> that supplies the data.</param>
		/// <param name="rowNum">The index of the row to update.</param>
		/// <param name="displayText">The text to display in the cell.</param>
		// Token: 0x06001858 RID: 6232 RVA: 0x00057564 File Offset: 0x00055764
		protected internal override void UpdateUI(CurrencyManager source, int rowNum, string displayText)
		{
			this.edit.Text = this.GetText(this.GetColumnValueAtRow(source, rowNum));
			if (!this.edit.ReadOnly && displayText != null)
			{
				this.edit.Text = displayText;
			}
		}

		/// <summary>Ends an edit operation on the <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</summary>
		// Token: 0x06001859 RID: 6233 RVA: 0x0005759B File Offset: 0x0005579B
		protected void EndEdit()
		{
			this.edit.IsInEditOrNavigateMode = true;
			this.DebugOut("Ending Edit");
			this.Invalidate();
		}

		/// <summary>Returns the optimum width and height of the cell in a specified row relative to the specified value.</summary>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> object used to draw shapes on the screen.</param>
		/// <param name="value">The value to draw.</param>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that contains the dimensions of the cell.</returns>
		// Token: 0x0600185A RID: 6234 RVA: 0x000575BC File Offset: 0x000557BC
		protected internal override Size GetPreferredSize(Graphics g, object value)
		{
			Size size = Size.Ceiling(g.MeasureString(this.GetText(value), this.DataGridTableStyle.DataGrid.Font));
			size.Width += this.xMargin * 2 + this.DataGridTableStyle.GridLineWidth;
			size.Height += this.yMargin;
			return size;
		}

		/// <summary>Gets the height of a cell in a <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</summary>
		/// <returns>The height of a cell.</returns>
		// Token: 0x0600185B RID: 6235 RVA: 0x00057623 File Offset: 0x00055823
		protected internal override int GetMinimumHeight()
		{
			return base.FontHeight + this.yMargin + 3;
		}

		/// <summary>Gets the height to be used in for automatically resizing columns.</summary>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> object used to draw shapes on the screen.</param>
		/// <param name="value">The value to draw.</param>
		/// <returns>The height the cells automatically resize to.</returns>
		// Token: 0x0600185C RID: 6236 RVA: 0x00057634 File Offset: 0x00055834
		protected internal override int GetPreferredHeight(Graphics g, object value)
		{
			int num = 0;
			int num2 = 0;
			string text = this.GetText(value);
			while (num != -1 && num < text.Length)
			{
				num = text.IndexOf("\r\n", num + 1);
				num2++;
			}
			return base.FontHeight * num2 + this.yMargin;
		}

		/// <summary>Initiates a request to interrupt an edit procedure.</summary>
		/// <param name="rowNum">The number of the row in which an edit operation is being interrupted.</param>
		// Token: 0x0600185D RID: 6237 RVA: 0x0005767E File Offset: 0x0005587E
		protected internal override void Abort(int rowNum)
		{
			this.RollBack();
			this.HideEditBox();
			this.EndEdit();
		}

		/// <summary>Enters a <see cref="F:System.DBNull.Value" /> in the column.</summary>
		// Token: 0x0600185E RID: 6238 RVA: 0x00057694 File Offset: 0x00055894
		protected internal override void EnterNullValue()
		{
			if (this.ReadOnly)
			{
				return;
			}
			if (!this.edit.Visible)
			{
				return;
			}
			if (!this.edit.IsInEditOrNavigateMode)
			{
				return;
			}
			this.edit.Text = this.NullText;
			this.edit.IsInEditOrNavigateMode = false;
			if (this.DataGridTableStyle != null && this.DataGridTableStyle.DataGrid != null)
			{
				this.DataGridTableStyle.DataGrid.ColumnStartedEditing(this.edit.Bounds);
			}
		}

		/// <summary>Inititates a request to complete an editing procedure.</summary>
		/// <param name="dataSource">The <see cref="T:System.Windows.Forms.CurrencyManager" /> of the <see cref="T:System.Windows.Forms.DataGrid" /> control the column belongs to.</param>
		/// <param name="rowNum">The number of the edited row.</param>
		/// <returns>
		///   <see langword="true" /> if the value was successfully committed; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600185F RID: 6239 RVA: 0x00057714 File Offset: 0x00055914
		protected internal override bool Commit(CurrencyManager dataSource, int rowNum)
		{
			this.edit.Bounds = Rectangle.Empty;
			if (this.edit.IsInEditOrNavigateMode)
			{
				return true;
			}
			try
			{
				object obj = this.edit.Text;
				if (this.NullText.Equals(obj))
				{
					obj = Convert.DBNull;
					this.edit.Text = this.NullText;
				}
				else if (this.format != null && this.format.Length != 0 && this.parseMethod != null && this.FormatInfo != null)
				{
					obj = this.parseMethod.Invoke(null, new object[]
					{
						this.edit.Text,
						this.FormatInfo
					});
					if (obj is IFormattable)
					{
						this.edit.Text = ((IFormattable)obj).ToString(this.format, this.formatInfo);
					}
					else
					{
						this.edit.Text = obj.ToString();
					}
				}
				else if (this.typeConverter != null && this.typeConverter.CanConvertFrom(typeof(string)))
				{
					obj = this.typeConverter.ConvertFromString(this.edit.Text);
					this.edit.Text = this.typeConverter.ConvertToString(obj);
				}
				this.SetColumnValueAtRow(dataSource, rowNum, obj);
			}
			catch
			{
				this.RollBack();
				return false;
			}
			this.DebugOut("OnCommit completed without Exception.");
			this.EndEdit();
			return true;
		}

		/// <summary>Prepares a cell for editing.</summary>
		/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> of the <see cref="T:System.Windows.Forms.DataGrid" /> control the column belongs to.</param>
		/// <param name="rowNum">The row number in this column being edited.</param>
		/// <param name="bounds">The bounding <see cref="T:System.Drawing.Rectangle" /> in which the control is to be sited.</param>
		/// <param name="readOnly">A value indicating whether the column is a read-only. <see langword="true" /> if the value is read-only; otherwise, <see langword="false" />.</param>
		/// <param name="displayText">The text to display in the control.</param>
		/// <param name="cellIsVisible">A value indicating whether the cell is visible. <see langword="true" /> if the cell is visible; otherwise, <see langword="false" />.</param>
		// Token: 0x06001860 RID: 6240 RVA: 0x000578A4 File Offset: 0x00055AA4
		protected internal override void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly, string displayText, bool cellIsVisible)
		{
			this.DebugOut("Begining Edit, rowNum :" + rowNum.ToString(CultureInfo.InvariantCulture));
			Rectangle rectangle = bounds;
			this.edit.ReadOnly = readOnly || this.ReadOnly || this.DataGridTableStyle.ReadOnly;
			this.edit.Text = this.GetText(this.GetColumnValueAtRow(source, rowNum));
			if (!this.edit.ReadOnly && displayText != null)
			{
				this.DataGridTableStyle.DataGrid.ColumnStartedEditing(bounds);
				this.edit.IsInEditOrNavigateMode = false;
				this.edit.Text = displayText;
			}
			if (cellIsVisible)
			{
				bounds.Offset(this.xMargin, 2 * this.yMargin);
				bounds.Width -= this.xMargin;
				bounds.Height -= 2 * this.yMargin;
				this.DebugOut("edit bounds: " + bounds.ToString());
				this.edit.Bounds = bounds;
				this.edit.Visible = true;
				this.edit.TextAlign = this.Alignment;
			}
			else
			{
				this.edit.Bounds = Rectangle.Empty;
			}
			this.edit.RightToLeft = this.DataGridTableStyle.DataGrid.RightToLeft;
			this.edit.FocusInternal();
			this.editRow = rowNum;
			if (!this.edit.ReadOnly)
			{
				this.oldValue = this.edit.Text;
			}
			if (displayText == null)
			{
				this.edit.SelectAll();
			}
			else
			{
				int length = this.edit.Text.Length;
				this.edit.Select(length, 0);
			}
			if (this.edit.Visible)
			{
				this.DataGridTableStyle.DataGrid.Invalidate(rectangle);
			}
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x00057A7F File Offset: 0x00055C7F
		internal override string GetDisplayText(object value)
		{
			return this.GetText(value);
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x00057A88 File Offset: 0x00055C88
		private string GetText(object value)
		{
			if (value is DBNull)
			{
				return this.NullText;
			}
			if (this.format != null && this.format.Length != 0 && value is IFormattable)
			{
				try
				{
					return ((IFormattable)value).ToString(this.format, this.formatInfo);
				}
				catch
				{
					goto IL_84;
				}
			}
			if (this.typeConverter != null && this.typeConverter.CanConvertTo(typeof(string)))
			{
				return (string)this.typeConverter.ConvertTo(value, typeof(string));
			}
			IL_84:
			if (value == null)
			{
				return "";
			}
			return value.ToString();
		}

		/// <summary>Paints the a <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> with the specified <see cref="T:System.Drawing.Graphics" />, <see cref="T:System.Drawing.Rectangle" />, <see cref="T:System.Windows.Forms.CurrencyManager" />, and row number.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> object to draw to.</param>
		/// <param name="bounds">The bounding <see cref="T:System.Drawing.Rectangle" /> to paint into.</param>
		/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> of the <see cref="T:System.Windows.Forms.DataGrid" /> the that contains the column.</param>
		/// <param name="rowNum">The number of the row in the underlying data table.</param>
		// Token: 0x06001863 RID: 6243 RVA: 0x0004F47E File Offset: 0x0004D67E
		protected internal override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum)
		{
			this.Paint(g, bounds, source, rowNum, false);
		}

		/// <summary>Paints a <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> with the specified <see cref="T:System.Drawing.Graphics" />, <see cref="T:System.Drawing.Rectangle" />, <see cref="T:System.Windows.Forms.CurrencyManager" />, row number, and alignment.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> object to draw to.</param>
		/// <param name="bounds">The bounding <see cref="T:System.Drawing.Rectangle" /> to paint into.</param>
		/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> of the <see cref="T:System.Windows.Forms.DataGrid" /> the that contains the column.</param>
		/// <param name="rowNum">The number of the row in the underlying data table.</param>
		/// <param name="alignToRight">A value indicating whether to align the column's content to the right. <see langword="true" /> if the content should be aligned to the right; otherwise, <see langword="false" />.</param>
		// Token: 0x06001864 RID: 6244 RVA: 0x00057B3C File Offset: 0x00055D3C
		protected internal override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, bool alignToRight)
		{
			string text = this.GetText(this.GetColumnValueAtRow(source, rowNum));
			this.PaintText(g, bounds, text, alignToRight);
		}

		/// <summary>Paints a <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> with the specified <see cref="T:System.Drawing.Graphics" />, <see cref="T:System.Drawing.Rectangle" />, <see cref="T:System.Windows.Forms.CurrencyManager" />, row number, <see cref="T:System.Drawing.Brush" />, and foreground color.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> object to draw to.</param>
		/// <param name="bounds">The bounding <see cref="T:System.Drawing.Rectangle" /> to paint into.</param>
		/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> of the <see cref="T:System.Windows.Forms.DataGrid" /> the that contains the column.</param>
		/// <param name="rowNum">The number of the row in the underlying data table.</param>
		/// <param name="backBrush">A <see cref="T:System.Drawing.Brush" /> that paints the background.</param>
		/// <param name="foreBrush">A <see cref="T:System.Drawing.Brush" /> that paints the foreground color.</param>
		/// <param name="alignToRight">A value indicating whether to align the column's content to the right. <see langword="true" /> if the content should be aligned to the right; otherwise, <see langword="false" />.</param>
		// Token: 0x06001865 RID: 6245 RVA: 0x00057B64 File Offset: 0x00055D64
		protected internal override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, Brush backBrush, Brush foreBrush, bool alignToRight)
		{
			string text = this.GetText(this.GetColumnValueAtRow(source, rowNum));
			this.PaintText(g, bounds, text, backBrush, foreBrush, alignToRight);
		}

		/// <summary>Draws the text and rectangle at the given location with the specified alignment.</summary>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> object used to draw the string.</param>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> which contains the boundary data of the rectangle.</param>
		/// <param name="text">The string to be drawn to the screen.</param>
		/// <param name="alignToRight">A value indicating whether the text is right-aligned.</param>
		// Token: 0x06001866 RID: 6246 RVA: 0x00057B90 File Offset: 0x00055D90
		protected void PaintText(Graphics g, Rectangle bounds, string text, bool alignToRight)
		{
			this.PaintText(g, bounds, text, this.DataGridTableStyle.BackBrush, this.DataGridTableStyle.ForeBrush, alignToRight);
		}

		/// <summary>Draws the text and rectangle at the specified location with the specified colors and alignment.</summary>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> object used to draw the string.</param>
		/// <param name="textBounds">A <see cref="T:System.Drawing.Rectangle" /> which contains the boundary data of the rectangle.</param>
		/// <param name="text">The string to be drawn to the screen.</param>
		/// <param name="backBrush">A <see cref="T:System.Drawing.Brush" /> that determines the rectangle's background color</param>
		/// <param name="foreBrush">A <see cref="T:System.Drawing.Brush" /> that determines the rectangles foreground color.</param>
		/// <param name="alignToRight">A value indicating whether the text is right-aligned.</param>
		// Token: 0x06001867 RID: 6247 RVA: 0x00057BB4 File Offset: 0x00055DB4
		protected void PaintText(Graphics g, Rectangle textBounds, string text, Brush backBrush, Brush foreBrush, bool alignToRight)
		{
			Rectangle rectangle = textBounds;
			StringFormat stringFormat = new StringFormat();
			if (alignToRight)
			{
				stringFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
			}
			stringFormat.Alignment = ((this.Alignment == HorizontalAlignment.Left) ? StringAlignment.Near : ((this.Alignment == HorizontalAlignment.Center) ? StringAlignment.Center : StringAlignment.Far));
			stringFormat.FormatFlags |= StringFormatFlags.NoWrap;
			g.FillRectangle(backBrush, rectangle);
			rectangle.Offset(0, 2 * this.yMargin);
			rectangle.Height -= 2 * this.yMargin;
			g.DrawString(text, this.DataGridTableStyle.DataGrid.Font, foreBrush, rectangle, stringFormat);
			stringFormat.Dispose();
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x00057C60 File Offset: 0x00055E60
		private void RollBack()
		{
			this.edit.Text = this.oldValue;
		}

		/// <summary>Removes the reference that the <see cref="T:System.Windows.Forms.DataGrid" /> holds to the control used to edit data.</summary>
		// Token: 0x06001869 RID: 6249 RVA: 0x00057C73 File Offset: 0x00055E73
		protected internal override void ReleaseHostedControl()
		{
			if (this.edit.ParentInternal != null)
			{
				this.edit.ParentInternal.Controls.Remove(this.edit);
			}
		}

		// Token: 0x04000AC5 RID: 2757
		private int xMargin = 2;

		// Token: 0x04000AC6 RID: 2758
		private int yMargin = 1;

		// Token: 0x04000AC7 RID: 2759
		private string format;

		// Token: 0x04000AC8 RID: 2760
		private TypeConverter typeConverter;

		// Token: 0x04000AC9 RID: 2761
		private IFormatProvider formatInfo;

		// Token: 0x04000ACA RID: 2762
		private MethodInfo parseMethod;

		// Token: 0x04000ACB RID: 2763
		private DataGridTextBox edit;

		// Token: 0x04000ACC RID: 2764
		private string oldValue;

		// Token: 0x04000ACD RID: 2765
		private int editRow = -1;
	}
}
