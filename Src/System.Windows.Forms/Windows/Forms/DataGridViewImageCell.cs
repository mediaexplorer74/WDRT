using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	/// <summary>Displays a graphic in a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
	// Token: 0x020001FD RID: 509
	public class DataGridViewImageCell : DataGridViewCell
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewImageCell" /> class, configuring it for use with cell values other than <see cref="T:System.Drawing.Icon" /> objects.</summary>
		// Token: 0x06002137 RID: 8503 RVA: 0x0009C52E File Offset: 0x0009A72E
		public DataGridViewImageCell()
			: this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewImageCell" /> class, optionally configuring it for use with <see cref="T:System.Drawing.Icon" /> cell values.</summary>
		/// <param name="valueIsIcon">The cell will display an <see cref="T:System.Drawing.Icon" /> value.</param>
		// Token: 0x06002138 RID: 8504 RVA: 0x0009C537 File Offset: 0x0009A737
		public DataGridViewImageCell(bool valueIsIcon)
		{
			if (valueIsIcon)
			{
				this.flags = 1;
			}
		}

		/// <summary>Gets the default value that is used when creating a new row.</summary>
		/// <returns>An object containing a default image placeholder, or <see langword="null" /> to display an empty cell.</returns>
		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06002139 RID: 8505 RVA: 0x0009C549 File Offset: 0x0009A749
		public override object DefaultNewRowValue
		{
			get
			{
				if (DataGridViewImageCell.defaultTypeImage.IsAssignableFrom(this.ValueType))
				{
					return DataGridViewImageCell.ErrorBitmap;
				}
				if (DataGridViewImageCell.defaultTypeIcon.IsAssignableFrom(this.ValueType))
				{
					return DataGridViewImageCell.ErrorIcon;
				}
				return null;
			}
		}

		/// <summary>Gets or sets the text associated with the image.</summary>
		/// <returns>The text associated with the image displayed in the cell.</returns>
		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x0600213A RID: 8506 RVA: 0x0009C57C File Offset: 0x0009A77C
		// (set) Token: 0x0600213B RID: 8507 RVA: 0x0009C5A9 File Offset: 0x0009A7A9
		[DefaultValue("")]
		public string Description
		{
			get
			{
				object @object = base.Properties.GetObject(DataGridViewImageCell.PropImageCellDescription);
				if (@object != null)
				{
					return (string)@object;
				}
				return string.Empty;
			}
			set
			{
				if (!string.IsNullOrEmpty(value) || base.Properties.ContainsObject(DataGridViewImageCell.PropImageCellDescription))
				{
					base.Properties.SetObject(DataGridViewImageCell.PropImageCellDescription, value);
				}
			}
		}

		/// <summary>Gets the type of the cell's hosted editing control.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the underlying editing control. As implemented in this class, this property is always <see langword="null" />.</returns>
		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x0600213C RID: 8508 RVA: 0x00015C90 File Offset: 0x00013E90
		public override Type EditType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x0600213D RID: 8509 RVA: 0x0009C5D6 File Offset: 0x0009A7D6
		internal static Bitmap ErrorBitmap
		{
			get
			{
				if (DataGridViewImageCell.errorBmp == null)
				{
					DataGridViewImageCell.errorBmp = new Bitmap(typeof(DataGridView), "ImageInError.bmp");
				}
				return DataGridViewImageCell.errorBmp;
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x0600213E RID: 8510 RVA: 0x0009C5FD File Offset: 0x0009A7FD
		internal static Icon ErrorIcon
		{
			get
			{
				if (DataGridViewImageCell.errorIco == null)
				{
					DataGridViewImageCell.errorIco = new Icon(typeof(DataGridView), "IconInError.ico");
				}
				return DataGridViewImageCell.errorIco;
			}
		}

		/// <summary>Gets the type of the formatted value associated with the cell.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing display value type of the cell, which is the <see cref="T:System.Drawing.Image" /> type if the <see cref="P:System.Windows.Forms.DataGridViewImageCell.ValueIsIcon" /> property is set to <see langword="false" /> or the <see cref="T:System.Drawing.Icon" /> type otherwise.</returns>
		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x0600213F RID: 8511 RVA: 0x0009C624 File Offset: 0x0009A824
		public override Type FormattedValueType
		{
			get
			{
				if (this.ValueIsIcon)
				{
					return DataGridViewImageCell.defaultTypeIcon;
				}
				return DataGridViewImageCell.defaultTypeImage;
			}
		}

		/// <summary>Gets or sets the graphics layout for the cell.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewImageCellLayout" /> for this cell. The default is <see cref="F:System.Windows.Forms.DataGridViewImageCellLayout.NotSet" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The supplied <see cref="T:System.Windows.Forms.DataGridViewImageCellLayout" /> value is invalid.</exception>
		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06002140 RID: 8512 RVA: 0x0009C63C File Offset: 0x0009A83C
		// (set) Token: 0x06002141 RID: 8513 RVA: 0x0009C664 File Offset: 0x0009A864
		[DefaultValue(DataGridViewImageCellLayout.NotSet)]
		public DataGridViewImageCellLayout ImageLayout
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(DataGridViewImageCell.PropImageCellLayout, out flag);
				if (flag)
				{
					return (DataGridViewImageCellLayout)integer;
				}
				return DataGridViewImageCellLayout.Normal;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DataGridViewImageCellLayout));
				}
				if (this.ImageLayout != value)
				{
					base.Properties.SetInteger(DataGridViewImageCell.PropImageCellLayout, (int)value);
					base.OnCommonChange();
				}
			}
		}

		// Token: 0x17000776 RID: 1910
		// (set) Token: 0x06002142 RID: 8514 RVA: 0x0009C6B7 File Offset: 0x0009A8B7
		internal DataGridViewImageCellLayout ImageLayoutInternal
		{
			set
			{
				if (this.ImageLayout != value)
				{
					base.Properties.SetInteger(DataGridViewImageCell.PropImageCellLayout, (int)value);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether this cell displays an <see cref="T:System.Drawing.Icon" /> value.</summary>
		/// <returns>
		///   <see langword="true" /> if this cell displays an <see cref="T:System.Drawing.Icon" /> value; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06002143 RID: 8515 RVA: 0x0009C6D3 File Offset: 0x0009A8D3
		// (set) Token: 0x06002144 RID: 8516 RVA: 0x0009C6E0 File Offset: 0x0009A8E0
		[DefaultValue(false)]
		public bool ValueIsIcon
		{
			get
			{
				return (this.flags & 1) > 0;
			}
			set
			{
				if (this.ValueIsIcon != value)
				{
					this.ValueIsIconInternal = value;
					if (base.DataGridView != null)
					{
						if (base.RowIndex != -1)
						{
							base.DataGridView.InvalidateCell(this);
							return;
						}
						base.DataGridView.InvalidateColumnInternal(base.ColumnIndex);
					}
				}
			}
		}

		// Token: 0x17000778 RID: 1912
		// (set) Token: 0x06002145 RID: 8517 RVA: 0x0009C72C File Offset: 0x0009A92C
		internal bool ValueIsIconInternal
		{
			set
			{
				if (this.ValueIsIcon != value)
				{
					if (value)
					{
						this.flags |= 1;
					}
					else
					{
						this.flags = (byte)((int)this.flags & -2);
					}
					if (base.DataGridView != null && base.RowIndex != -1 && base.DataGridView.NewRowIndex == base.RowIndex && !base.DataGridView.VirtualMode && ((value && base.Value == DataGridViewImageCell.ErrorBitmap) || (!value && base.Value == DataGridViewImageCell.ErrorIcon)))
					{
						base.Value = this.DefaultNewRowValue;
					}
				}
			}
		}

		/// <summary>Gets or sets the data type of the values in the cell.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the cell's value.</returns>
		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06002146 RID: 8518 RVA: 0x0009C7C8 File Offset: 0x0009A9C8
		// (set) Token: 0x06002147 RID: 8519 RVA: 0x0009C7FA File Offset: 0x0009A9FA
		public override Type ValueType
		{
			get
			{
				Type valueType = base.ValueType;
				if (valueType != null)
				{
					return valueType;
				}
				if (this.ValueIsIcon)
				{
					return DataGridViewImageCell.defaultTypeIcon;
				}
				return DataGridViewImageCell.defaultTypeImage;
			}
			set
			{
				base.ValueType = value;
				this.ValueIsIcon = value != null && DataGridViewImageCell.defaultTypeIcon.IsAssignableFrom(value);
			}
		}

		/// <summary>Creates an exact copy of this cell.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewImageCell" />.</returns>
		// Token: 0x06002148 RID: 8520 RVA: 0x0009C820 File Offset: 0x0009AA20
		public override object Clone()
		{
			Type type = base.GetType();
			DataGridViewImageCell dataGridViewImageCell;
			if (type == DataGridViewImageCell.cellType)
			{
				dataGridViewImageCell = new DataGridViewImageCell();
			}
			else
			{
				dataGridViewImageCell = (DataGridViewImageCell)Activator.CreateInstance(type);
			}
			base.CloneInternal(dataGridViewImageCell);
			dataGridViewImageCell.ValueIsIconInternal = this.ValueIsIcon;
			dataGridViewImageCell.Description = this.Description;
			dataGridViewImageCell.ImageLayoutInternal = this.ImageLayout;
			return dataGridViewImageCell;
		}

		/// <summary>Creates a new accessible object for the <see cref="T:System.Windows.Forms.DataGridViewImageCell" />.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.DataGridViewImageCell.DataGridViewImageCellAccessibleObject" /> for the <see cref="T:System.Windows.Forms.DataGridViewImageCell" />.</returns>
		// Token: 0x06002149 RID: 8521 RVA: 0x0009C881 File Offset: 0x0009AA81
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new DataGridViewImageCell.DataGridViewImageCellAccessibleObject(this);
		}

		/// <summary>Returns the bounding rectangle that encloses the cell's content area, which is calculated using the specified <see cref="T:System.Drawing.Graphics" /> and cell style.</summary>
		/// <param name="graphics">The graphics context for the cell.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's contents.</returns>
		// Token: 0x0600214A RID: 8522 RVA: 0x0009C88C File Offset: 0x0009AA8C
		protected override Rectangle GetContentBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
		{
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			if (base.DataGridView == null || rowIndex < 0 || base.OwningColumn == null)
			{
				return Rectangle.Empty;
			}
			object value = this.GetValue(rowIndex);
			object formattedValue = this.GetFormattedValue(value, rowIndex, ref cellStyle, null, null, DataGridViewDataErrorContexts.Formatting);
			DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyle;
			DataGridViewElementStates dataGridViewElementStates;
			Rectangle rectangle;
			base.ComputeBorderStyleCellStateAndCellBounds(rowIndex, out dataGridViewAdvancedBorderStyle, out dataGridViewElementStates, out rectangle);
			return this.PaintPrivate(graphics, rectangle, rectangle, rowIndex, dataGridViewElementStates, formattedValue, null, cellStyle, dataGridViewAdvancedBorderStyle, DataGridViewPaintParts.ContentForeground, true, false, false);
		}

		/// <summary>Returns the bounding rectangle that encloses the cell's error icon, if one is displayed.</summary>
		/// <param name="graphics">The graphics context for the cell.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's error icon, if one is displayed; otherwise, <see cref="F:System.Drawing.Rectangle.Empty" />.</returns>
		// Token: 0x0600214B RID: 8523 RVA: 0x0009C900 File Offset: 0x0009AB00
		protected override Rectangle GetErrorIconBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
		{
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			if (base.DataGridView == null || rowIndex < 0 || base.OwningColumn == null || !base.DataGridView.ShowCellErrors || string.IsNullOrEmpty(this.GetErrorText(rowIndex)))
			{
				return Rectangle.Empty;
			}
			object value = this.GetValue(rowIndex);
			object formattedValue = this.GetFormattedValue(value, rowIndex, ref cellStyle, null, null, DataGridViewDataErrorContexts.Formatting);
			DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyle;
			DataGridViewElementStates dataGridViewElementStates;
			Rectangle rectangle;
			base.ComputeBorderStyleCellStateAndCellBounds(rowIndex, out dataGridViewAdvancedBorderStyle, out dataGridViewElementStates, out rectangle);
			return this.PaintPrivate(graphics, rectangle, rectangle, rowIndex, dataGridViewElementStates, formattedValue, this.GetErrorText(rowIndex), cellStyle, dataGridViewAdvancedBorderStyle, DataGridViewPaintParts.ContentForeground, false, true, false);
		}

		/// <summary>Returns a graphic as it would be displayed in the cell.</summary>
		/// <param name="value">The value to be formatted.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> in effect for the cell.</param>
		/// <param name="valueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> associated with the value type that provides custom conversion to the formatted value type, or <see langword="null" /> if no such custom conversion is needed.</param>
		/// <param name="formattedValueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> associated with the formatted value type that provides custom conversion from the value type, or <see langword="null" /> if no such custom conversion is needed.</param>
		/// <param name="context">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts" /> values describing the context in which the formatted value is needed.</param>
		/// <returns>An object that represents the formatted image.</returns>
		// Token: 0x0600214C RID: 8524 RVA: 0x0009C994 File Offset: 0x0009AB94
		protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
		{
			if ((context & DataGridViewDataErrorContexts.ClipboardContent) != (DataGridViewDataErrorContexts)0)
			{
				return this.Description;
			}
			object formattedValue = base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
			if (formattedValue == null && cellStyle.NullValue == null)
			{
				return null;
			}
			if (this.ValueIsIcon)
			{
				Icon icon = formattedValue as Icon;
				if (icon == null)
				{
					icon = DataGridViewImageCell.ErrorIcon;
				}
				return icon;
			}
			Image image = formattedValue as Image;
			if (image == null)
			{
				image = DataGridViewImageCell.ErrorBitmap;
			}
			return image;
		}

		/// <summary>Calculates the preferred size, in pixels, of the cell.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to draw the cell.</param>
		/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the style of the cell.</param>
		/// <param name="rowIndex">The zero-based row index of the cell.</param>
		/// <param name="constraintSize">The cell's maximum allowable size.</param>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the preferred size, in pixels, of the cell.</returns>
		// Token: 0x0600214D RID: 8525 RVA: 0x0009C9FC File Offset: 0x0009ABFC
		protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
		{
			if (base.DataGridView == null)
			{
				return new Size(-1, -1);
			}
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			Rectangle stdBorderWidths = base.StdBorderWidths;
			int num = stdBorderWidths.Left + stdBorderWidths.Width + cellStyle.Padding.Horizontal;
			int num2 = stdBorderWidths.Top + stdBorderWidths.Height + cellStyle.Padding.Vertical;
			DataGridViewFreeDimension freeDimensionFromConstraint = DataGridViewCell.GetFreeDimensionFromConstraint(constraintSize);
			object formattedValue = base.GetFormattedValue(rowIndex, ref cellStyle, DataGridViewDataErrorContexts.Formatting | DataGridViewDataErrorContexts.PreferredSize);
			Image image = formattedValue as Image;
			Icon icon = null;
			if (image == null)
			{
				icon = formattedValue as Icon;
			}
			Size size;
			if (freeDimensionFromConstraint == DataGridViewFreeDimension.Height && this.ImageLayout == DataGridViewImageCellLayout.Zoom)
			{
				if (image != null || icon != null)
				{
					if (image != null)
					{
						int num3 = constraintSize.Width - num;
						if (num3 <= 0 || image.Width == 0)
						{
							size = Size.Empty;
						}
						else
						{
							size = new Size(0, Math.Min(image.Height, decimal.ToInt32(image.Height * num3 / image.Width)));
						}
					}
					else
					{
						int num4 = constraintSize.Width - num;
						if (num4 <= 0 || icon.Width == 0)
						{
							size = Size.Empty;
						}
						else
						{
							size = new Size(0, Math.Min(icon.Height, decimal.ToInt32(icon.Height * num4 / icon.Width)));
						}
					}
				}
				else
				{
					size = new Size(0, 1);
				}
			}
			else if (freeDimensionFromConstraint == DataGridViewFreeDimension.Width && this.ImageLayout == DataGridViewImageCellLayout.Zoom)
			{
				if (image != null || icon != null)
				{
					if (image != null)
					{
						int num5 = constraintSize.Height - num2;
						if (num5 <= 0 || image.Height == 0)
						{
							size = Size.Empty;
						}
						else
						{
							size = new Size(Math.Min(image.Width, decimal.ToInt32(image.Width * num5 / image.Height)), 0);
						}
					}
					else
					{
						int num6 = constraintSize.Height - num2;
						if (num6 <= 0 || icon.Height == 0)
						{
							size = Size.Empty;
						}
						else
						{
							size = new Size(Math.Min(icon.Width, decimal.ToInt32(icon.Width * num6 / icon.Height)), 0);
						}
					}
				}
				else
				{
					size = new Size(1, 0);
				}
			}
			else
			{
				if (image != null)
				{
					size = new Size(image.Width, image.Height);
				}
				else if (icon != null)
				{
					size = new Size(icon.Width, icon.Height);
				}
				else
				{
					size = new Size(1, 1);
				}
				if (freeDimensionFromConstraint == DataGridViewFreeDimension.Height)
				{
					size.Width = 0;
				}
				else if (freeDimensionFromConstraint == DataGridViewFreeDimension.Width)
				{
					size.Height = 0;
				}
			}
			if (freeDimensionFromConstraint != DataGridViewFreeDimension.Height)
			{
				size.Width += num;
				if (base.DataGridView.ShowCellErrors)
				{
					size.Width = Math.Max(size.Width, num + 8 + (int)DataGridViewCell.iconsWidth);
				}
			}
			if (freeDimensionFromConstraint != DataGridViewFreeDimension.Width)
			{
				size.Height += num2;
				if (base.DataGridView.ShowCellErrors)
				{
					size.Height = Math.Max(size.Height, num2 + 8 + (int)DataGridViewCell.iconsHeight);
				}
			}
			return size;
		}

		/// <summary>Gets the value of the cell.</summary>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <returns>The value contained in the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
		// Token: 0x0600214E RID: 8526 RVA: 0x0009CD7C File Offset: 0x0009AF7C
		protected override object GetValue(int rowIndex)
		{
			object value = base.GetValue(rowIndex);
			if (value == null)
			{
				DataGridViewImageColumn dataGridViewImageColumn = base.OwningColumn as DataGridViewImageColumn;
				if (dataGridViewImageColumn != null)
				{
					if (DataGridViewImageCell.defaultTypeImage.IsAssignableFrom(this.ValueType))
					{
						Image image = dataGridViewImageColumn.Image;
						if (image != null)
						{
							return image;
						}
					}
					else if (DataGridViewImageCell.defaultTypeIcon.IsAssignableFrom(this.ValueType))
					{
						Icon icon = dataGridViewImageColumn.Icon;
						if (icon != null)
						{
							return icon;
						}
					}
				}
			}
			return value;
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x0009CDE0 File Offset: 0x0009AFE0
		private Rectangle ImgBounds(Rectangle bounds, int imgWidth, int imgHeight, DataGridViewImageCellLayout imageLayout, DataGridViewCellStyle cellStyle)
		{
			Rectangle empty = Rectangle.Empty;
			if (imageLayout > DataGridViewImageCellLayout.Normal)
			{
				if (imageLayout == DataGridViewImageCellLayout.Zoom)
				{
					if (imgWidth * bounds.Height < imgHeight * bounds.Width)
					{
						empty = new Rectangle(bounds.X, bounds.Y, decimal.ToInt32(imgWidth * bounds.Height / imgHeight), bounds.Height);
					}
					else
					{
						empty = new Rectangle(bounds.X, bounds.Y, bounds.Width, decimal.ToInt32(imgHeight * bounds.Width / imgWidth));
					}
				}
			}
			else
			{
				empty = new Rectangle(bounds.X, bounds.Y, imgWidth, imgHeight);
			}
			if (base.DataGridView.RightToLeftInternal)
			{
				DataGridViewContentAlignment alignment = cellStyle.Alignment;
				if (alignment <= DataGridViewContentAlignment.MiddleLeft)
				{
					if (alignment != DataGridViewContentAlignment.TopLeft)
					{
						if (alignment != DataGridViewContentAlignment.TopRight)
						{
							if (alignment == DataGridViewContentAlignment.MiddleLeft)
							{
								empty.X = bounds.Right - empty.Width;
							}
						}
						else
						{
							empty.X = bounds.X;
						}
					}
					else
					{
						empty.X = bounds.Right - empty.Width;
					}
				}
				else if (alignment != DataGridViewContentAlignment.MiddleRight)
				{
					if (alignment != DataGridViewContentAlignment.BottomLeft)
					{
						if (alignment == DataGridViewContentAlignment.BottomRight)
						{
							empty.X = bounds.X;
						}
					}
					else
					{
						empty.X = bounds.Right - empty.Width;
					}
				}
				else
				{
					empty.X = bounds.X;
				}
			}
			else
			{
				DataGridViewContentAlignment alignment2 = cellStyle.Alignment;
				if (alignment2 <= DataGridViewContentAlignment.MiddleLeft)
				{
					if (alignment2 != DataGridViewContentAlignment.TopLeft)
					{
						if (alignment2 != DataGridViewContentAlignment.TopRight)
						{
							if (alignment2 == DataGridViewContentAlignment.MiddleLeft)
							{
								empty.X = bounds.X;
							}
						}
						else
						{
							empty.X = bounds.Right - empty.Width;
						}
					}
					else
					{
						empty.X = bounds.X;
					}
				}
				else if (alignment2 != DataGridViewContentAlignment.MiddleRight)
				{
					if (alignment2 != DataGridViewContentAlignment.BottomLeft)
					{
						if (alignment2 == DataGridViewContentAlignment.BottomRight)
						{
							empty.X = bounds.Right - empty.Width;
						}
					}
					else
					{
						empty.X = bounds.X;
					}
				}
				else
				{
					empty.X = bounds.Right - empty.Width;
				}
			}
			DataGridViewContentAlignment alignment3 = cellStyle.Alignment;
			if (alignment3 == DataGridViewContentAlignment.TopCenter || alignment3 == DataGridViewContentAlignment.MiddleCenter || alignment3 == DataGridViewContentAlignment.BottomCenter)
			{
				empty.X = bounds.X + (bounds.Width - empty.Width) / 2;
			}
			DataGridViewContentAlignment alignment4 = cellStyle.Alignment;
			if (alignment4 > DataGridViewContentAlignment.MiddleCenter)
			{
				if (alignment4 <= DataGridViewContentAlignment.BottomLeft)
				{
					if (alignment4 == DataGridViewContentAlignment.MiddleRight)
					{
						goto IL_2F6;
					}
					if (alignment4 != DataGridViewContentAlignment.BottomLeft)
					{
						return empty;
					}
				}
				else if (alignment4 != DataGridViewContentAlignment.BottomCenter && alignment4 != DataGridViewContentAlignment.BottomRight)
				{
					return empty;
				}
				empty.Y = bounds.Bottom - empty.Height;
				return empty;
			}
			if (alignment4 <= DataGridViewContentAlignment.TopRight)
			{
				if (alignment4 - DataGridViewContentAlignment.TopLeft > 1 && alignment4 != DataGridViewContentAlignment.TopRight)
				{
					return empty;
				}
				empty.Y = bounds.Y;
				return empty;
			}
			else if (alignment4 != DataGridViewContentAlignment.MiddleLeft && alignment4 != DataGridViewContentAlignment.MiddleCenter)
			{
				return empty;
			}
			IL_2F6:
			empty.Y = bounds.Y + (bounds.Height - empty.Height) / 2;
			return empty;
		}

		/// <summary>Paints the current <see cref="T:System.Windows.Forms.DataGridViewImageCell" />.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the cell.</param>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</param>
		/// <param name="cellBounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the bounds of the cell that is being painted.</param>
		/// <param name="rowIndex">The row index of the cell that is being painted.</param>
		/// <param name="elementState">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that specifies the state of the cell.</param>
		/// <param name="value">The data of the cell that is being painted.</param>
		/// <param name="formattedValue">The formatted data of the cell that is being painted.</param>
		/// <param name="errorText">An error message that is associated with the cell.</param>
		/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that contains formatting and style information about the cell.</param>
		/// <param name="advancedBorderStyle">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that contains border styles for the cell that is being painted.</param>
		/// <param name="paintParts">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values that specifies which parts of the cell need to be painted.</param>
		// Token: 0x06002150 RID: 8528 RVA: 0x0009D11C File Offset: 0x0009B31C
		protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			this.PaintPrivate(graphics, clipBounds, cellBounds, rowIndex, elementState, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts, false, false, true);
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x0009D154 File Offset: 0x0009B354
		private Rectangle PaintPrivate(Graphics g, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts, bool computeContentBounds, bool computeErrorIconBounds, bool paint)
		{
			if (paint && DataGridViewCell.PaintBorder(paintParts))
			{
				this.PaintBorder(g, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
			}
			Rectangle rectangle = cellBounds;
			Rectangle rectangle2 = this.BorderWidths(advancedBorderStyle);
			rectangle.Offset(rectangle2.X, rectangle2.Y);
			rectangle.Width -= rectangle2.Right;
			rectangle.Height -= rectangle2.Bottom;
			Rectangle rectangle4;
			if (rectangle.Width > 0 && rectangle.Height > 0 && (paint || computeContentBounds))
			{
				Rectangle rectangle3 = rectangle;
				if (cellStyle.Padding != Padding.Empty)
				{
					if (base.DataGridView.RightToLeftInternal)
					{
						rectangle3.Offset(cellStyle.Padding.Right, cellStyle.Padding.Top);
					}
					else
					{
						rectangle3.Offset(cellStyle.Padding.Left, cellStyle.Padding.Top);
					}
					rectangle3.Width -= cellStyle.Padding.Horizontal;
					rectangle3.Height -= cellStyle.Padding.Vertical;
				}
				bool flag = (elementState & DataGridViewElementStates.Selected) > DataGridViewElementStates.None;
				SolidBrush cachedBrush = base.DataGridView.GetCachedBrush((DataGridViewCell.PaintSelectionBackground(paintParts) && flag) ? cellStyle.SelectionBackColor : cellStyle.BackColor);
				if (rectangle3.Width > 0 && rectangle3.Height > 0)
				{
					Image image = formattedValue as Image;
					Icon icon = null;
					if (image == null)
					{
						icon = formattedValue as Icon;
					}
					if (icon != null || image != null)
					{
						DataGridViewImageCellLayout dataGridViewImageCellLayout = this.ImageLayout;
						if (dataGridViewImageCellLayout == DataGridViewImageCellLayout.NotSet)
						{
							if (base.OwningColumn is DataGridViewImageColumn)
							{
								dataGridViewImageCellLayout = ((DataGridViewImageColumn)base.OwningColumn).ImageLayout;
							}
							else
							{
								dataGridViewImageCellLayout = DataGridViewImageCellLayout.Normal;
							}
						}
						if (dataGridViewImageCellLayout == DataGridViewImageCellLayout.Stretch)
						{
							if (paint)
							{
								if (DataGridViewCell.PaintBackground(paintParts))
								{
									DataGridViewCell.PaintPadding(g, rectangle, cellStyle, cachedBrush, base.DataGridView.RightToLeftInternal);
								}
								if (DataGridViewCell.PaintContentForeground(paintParts))
								{
									if (image != null)
									{
										ImageAttributes imageAttributes = new ImageAttributes();
										imageAttributes.SetWrapMode(WrapMode.TileFlipXY);
										g.DrawImage(image, rectangle3, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes);
										imageAttributes.Dispose();
									}
									else
									{
										g.DrawIcon(icon, rectangle3);
									}
								}
							}
							rectangle4 = rectangle3;
						}
						else
						{
							Rectangle rectangle5 = this.ImgBounds(rectangle3, (image == null) ? icon.Width : image.Width, (image == null) ? icon.Height : image.Height, dataGridViewImageCellLayout, cellStyle);
							rectangle4 = rectangle5;
							if (paint)
							{
								if (DataGridViewCell.PaintBackground(paintParts) && cachedBrush.Color.A == 255)
								{
									g.FillRectangle(cachedBrush, rectangle);
								}
								if (DataGridViewCell.PaintContentForeground(paintParts))
								{
									Region clip = g.Clip;
									g.SetClip(Rectangle.Intersect(Rectangle.Intersect(rectangle5, rectangle3), Rectangle.Truncate(g.VisibleClipBounds)));
									if (image != null)
									{
										g.DrawImage(image, rectangle5);
									}
									else
									{
										g.DrawIconUnstretched(icon, rectangle5);
									}
									g.Clip = clip;
								}
							}
						}
					}
					else
					{
						if (paint && DataGridViewCell.PaintBackground(paintParts) && cachedBrush.Color.A == 255)
						{
							g.FillRectangle(cachedBrush, rectangle);
						}
						rectangle4 = Rectangle.Empty;
					}
				}
				else
				{
					if (paint && DataGridViewCell.PaintBackground(paintParts) && cachedBrush.Color.A == 255)
					{
						g.FillRectangle(cachedBrush, rectangle);
					}
					rectangle4 = Rectangle.Empty;
				}
				Point currentCellAddress = base.DataGridView.CurrentCellAddress;
				if (paint && DataGridViewCell.PaintFocus(paintParts) && currentCellAddress.X == base.ColumnIndex && currentCellAddress.Y == rowIndex && base.DataGridView.ShowFocusCues && base.DataGridView.Focused)
				{
					ControlPaint.DrawFocusRectangle(g, rectangle, Color.Empty, cachedBrush.Color);
				}
				if (base.DataGridView.ShowCellErrors && paint && DataGridViewCell.PaintErrorIcon(paintParts))
				{
					base.PaintErrorIcon(g, cellStyle, rowIndex, cellBounds, rectangle, errorText);
				}
			}
			else if (computeErrorIconBounds)
			{
				if (!string.IsNullOrEmpty(errorText))
				{
					rectangle4 = base.ComputeErrorIconBounds(rectangle);
				}
				else
				{
					rectangle4 = Rectangle.Empty;
				}
			}
			else
			{
				rectangle4 = Rectangle.Empty;
			}
			return rectangle4;
		}

		/// <summary>Returns a string that describes the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		// Token: 0x06002152 RID: 8530 RVA: 0x0009D58C File Offset: 0x0009B78C
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"DataGridViewImageCell { ColumnIndex=",
				base.ColumnIndex.ToString(CultureInfo.CurrentCulture),
				", RowIndex=",
				base.RowIndex.ToString(CultureInfo.CurrentCulture),
				" }"
			});
		}

		// Token: 0x04000DDA RID: 3546
		private static ColorMap[] colorMap = new ColorMap[]
		{
			new ColorMap()
		};

		// Token: 0x04000DDB RID: 3547
		private static readonly int PropImageCellDescription = PropertyStore.CreateKey();

		// Token: 0x04000DDC RID: 3548
		private static readonly int PropImageCellLayout = PropertyStore.CreateKey();

		// Token: 0x04000DDD RID: 3549
		private static Type defaultTypeImage = typeof(Image);

		// Token: 0x04000DDE RID: 3550
		private static Type defaultTypeIcon = typeof(Icon);

		// Token: 0x04000DDF RID: 3551
		private static Type cellType = typeof(DataGridViewImageCell);

		// Token: 0x04000DE0 RID: 3552
		private static Bitmap errorBmp = null;

		// Token: 0x04000DE1 RID: 3553
		private static Icon errorIco = null;

		// Token: 0x04000DE2 RID: 3554
		private const byte DATAGRIDVIEWIMAGECELL_valueIsIcon = 1;

		// Token: 0x04000DE3 RID: 3555
		private byte flags;

		/// <summary>Provides information about a <see cref="T:System.Windows.Forms.DataGridViewImageCell" /> to accessibility client applications.</summary>
		// Token: 0x02000671 RID: 1649
		protected class DataGridViewImageCellAccessibleObject : DataGridViewCell.DataGridViewCellAccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewImageCell.DataGridViewImageCellAccessibleObject" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.DataGridViewCell" /> that owns the <see cref="T:System.Windows.Forms.DataGridViewImageCell.DataGridViewImageCellAccessibleObject" />.</param>
			// Token: 0x0600665A RID: 26202 RVA: 0x0017BC46 File Offset: 0x00179E46
			public DataGridViewImageCellAccessibleObject(DataGridViewCell owner)
				: base(owner)
			{
			}

			/// <summary>Gets a string that represents the default action of the <see cref="T:System.Windows.Forms.DataGridViewImageCell" />.</summary>
			/// <returns>An empty string ("").</returns>
			// Token: 0x17001645 RID: 5701
			// (get) Token: 0x0600665B RID: 26203 RVA: 0x0017E1A1 File Offset: 0x0017C3A1
			public override string DefaultAction
			{
				get
				{
					return string.Empty;
				}
			}

			/// <summary>Gets the text associated with the image in the image cell.</summary>
			/// <returns>The text associated with the image in the image cell.</returns>
			// Token: 0x17001646 RID: 5702
			// (get) Token: 0x0600665C RID: 26204 RVA: 0x0017E1A8 File Offset: 0x0017C3A8
			public override string Description
			{
				get
				{
					DataGridViewImageCell dataGridViewImageCell = base.Owner as DataGridViewImageCell;
					if (dataGridViewImageCell != null)
					{
						return dataGridViewImageCell.Description;
					}
					return null;
				}
			}

			/// <summary>Gets a string representing the formatted value of the owning cell.</summary>
			/// <returns>A <see cref="T:System.String" /> representation of the cell value.</returns>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is <see langword="null" />.</exception>
			// Token: 0x17001647 RID: 5703
			// (get) Token: 0x0600665D RID: 26205 RVA: 0x0017E1CC File Offset: 0x0017C3CC
			// (set) Token: 0x0600665E RID: 26206 RVA: 0x000070A6 File Offset: 0x000052A6
			public override string Value
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return base.Value;
				}
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				set
				{
				}
			}

			/// <summary>Performs the default action of the <see cref="T:System.Windows.Forms.DataGridViewImageCell.DataGridViewImageCellAccessibleObject" />.</summary>
			// Token: 0x0600665F RID: 26207 RVA: 0x0017E1D4 File Offset: 0x0017C3D4
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void DoDefaultAction()
			{
				if (AccessibilityImprovements.Level3)
				{
					DataGridViewImageCell dataGridViewImageCell = (DataGridViewImageCell)base.Owner;
					DataGridView dataGridView = dataGridViewImageCell.DataGridView;
					if (dataGridView != null && dataGridViewImageCell.RowIndex != -1 && dataGridViewImageCell.OwningColumn != null && dataGridViewImageCell.OwningRow != null)
					{
						dataGridView.OnCellContentClickInternal(new DataGridViewCellEventArgs(dataGridViewImageCell.ColumnIndex, dataGridViewImageCell.RowIndex));
					}
				}
			}

			/// <summary>Gets the number of child accessible objects that belong to the <see cref="T:System.Windows.Forms.DataGridViewImageCell.DataGridViewImageCellAccessibleObject" />.</summary>
			/// <returns>The value -1.</returns>
			// Token: 0x06006660 RID: 26208 RVA: 0x0001180C File Offset: 0x0000FA0C
			public override int GetChildCount()
			{
				return 0;
			}

			// Token: 0x06006661 RID: 26209 RVA: 0x0017BCD6 File Offset: 0x00179ED6
			internal override bool IsIAccessibleExSupported()
			{
				return AccessibilityImprovements.Level2 || base.IsIAccessibleExSupported();
			}

			// Token: 0x06006662 RID: 26210 RVA: 0x0017E22E File Offset: 0x0017C42E
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30003)
				{
					return 50006;
				}
				if (AccessibilityImprovements.Level3 && propertyID == 30031)
				{
					return true;
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x06006663 RID: 26211 RVA: 0x0017E260 File Offset: 0x0017C460
			internal override bool IsPatternSupported(int patternId)
			{
				return (AccessibilityImprovements.Level3 && patternId == 10000) || base.IsPatternSupported(patternId);
			}
		}
	}
}
