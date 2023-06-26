using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>Represents the formatting and style information applied to individual cells within a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
	// Token: 0x020001B1 RID: 433
	[TypeConverter(typeof(DataGridViewCellStyleConverter))]
	[Editor("System.Windows.Forms.Design.DataGridViewCellStyleEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public class DataGridViewCellStyle : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> class using default property values.</summary>
		// Token: 0x06001E70 RID: 7792 RVA: 0x0008F760 File Offset: 0x0008D960
		public DataGridViewCellStyle()
		{
			this.propertyStore = new PropertyStore();
			this.scope = DataGridViewCellStyleScopes.None;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> class using the property values of the specified <see cref="T:System.Windows.Forms.DataGridViewCellStyle" />.</summary>
		/// <param name="dataGridViewCellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> used as a template to provide initial property values.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridViewCellStyle" /> is <see langword="null" />.</exception>
		// Token: 0x06001E71 RID: 7793 RVA: 0x0008F77C File Offset: 0x0008D97C
		public DataGridViewCellStyle(DataGridViewCellStyle dataGridViewCellStyle)
		{
			if (dataGridViewCellStyle == null)
			{
				throw new ArgumentNullException("dataGridViewCellStyle");
			}
			this.propertyStore = new PropertyStore();
			this.scope = DataGridViewCellStyleScopes.None;
			this.BackColor = dataGridViewCellStyle.BackColor;
			this.ForeColor = dataGridViewCellStyle.ForeColor;
			this.SelectionBackColor = dataGridViewCellStyle.SelectionBackColor;
			this.SelectionForeColor = dataGridViewCellStyle.SelectionForeColor;
			this.Font = dataGridViewCellStyle.Font;
			this.NullValue = dataGridViewCellStyle.NullValue;
			this.DataSourceNullValue = dataGridViewCellStyle.DataSourceNullValue;
			this.Format = dataGridViewCellStyle.Format;
			if (!dataGridViewCellStyle.IsFormatProviderDefault)
			{
				this.FormatProvider = dataGridViewCellStyle.FormatProvider;
			}
			this.AlignmentInternal = dataGridViewCellStyle.Alignment;
			this.WrapModeInternal = dataGridViewCellStyle.WrapMode;
			this.Tag = dataGridViewCellStyle.Tag;
			this.PaddingInternal = dataGridViewCellStyle.Padding;
		}

		/// <summary>Gets or sets a value indicating the position of the cell content within a <see cref="T:System.Windows.Forms.DataGridView" /> cell.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewContentAlignment" /> values. The default is <see cref="F:System.Windows.Forms.DataGridViewContentAlignment.NotSet" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The property value is not a valid <see cref="T:System.Windows.Forms.DataGridViewContentAlignment" /> value.</exception>
		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06001E72 RID: 7794 RVA: 0x0008F854 File Offset: 0x0008DA54
		// (set) Token: 0x06001E73 RID: 7795 RVA: 0x0008F87C File Offset: 0x0008DA7C
		[SRDescription("DataGridViewCellStyleAlignmentDescr")]
		[DefaultValue(DataGridViewContentAlignment.NotSet)]
		[SRCategory("CatLayout")]
		public DataGridViewContentAlignment Alignment
		{
			get
			{
				bool flag;
				int integer = this.Properties.GetInteger(DataGridViewCellStyle.PropAlignment, out flag);
				if (flag)
				{
					return (DataGridViewContentAlignment)integer;
				}
				return DataGridViewContentAlignment.NotSet;
			}
			set
			{
				if (value <= DataGridViewContentAlignment.MiddleCenter)
				{
					if (value <= DataGridViewContentAlignment.TopRight)
					{
						if (value <= DataGridViewContentAlignment.TopCenter || value == DataGridViewContentAlignment.TopRight)
						{
							goto IL_5C;
						}
					}
					else if (value == DataGridViewContentAlignment.MiddleLeft || value == DataGridViewContentAlignment.MiddleCenter)
					{
						goto IL_5C;
					}
				}
				else if (value <= DataGridViewContentAlignment.BottomLeft)
				{
					if (value == DataGridViewContentAlignment.MiddleRight || value == DataGridViewContentAlignment.BottomLeft)
					{
						goto IL_5C;
					}
				}
				else if (value == DataGridViewContentAlignment.BottomCenter || value == DataGridViewContentAlignment.BottomRight)
				{
					goto IL_5C;
				}
				throw new InvalidEnumArgumentException("value", (int)value, typeof(DataGridViewContentAlignment));
				IL_5C:
				this.AlignmentInternal = value;
			}
		}

		// Token: 0x170006AF RID: 1711
		// (set) Token: 0x06001E74 RID: 7796 RVA: 0x0008F8EC File Offset: 0x0008DAEC
		internal DataGridViewContentAlignment AlignmentInternal
		{
			set
			{
				if (this.Alignment != value)
				{
					this.Properties.SetInteger(DataGridViewCellStyle.PropAlignment, (int)value);
					this.OnPropertyChanged(DataGridViewCellStyle.DataGridViewCellStylePropertyInternal.Other);
				}
			}
		}

		/// <summary>Gets or sets the background color of a <see cref="T:System.Windows.Forms.DataGridView" /> cell.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of a cell. The default is <see cref="F:System.Drawing.Color.Empty" />.</returns>
		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06001E75 RID: 7797 RVA: 0x0008F90F File Offset: 0x0008DB0F
		// (set) Token: 0x06001E76 RID: 7798 RVA: 0x0008F924 File Offset: 0x0008DB24
		[SRCategory("CatAppearance")]
		public Color BackColor
		{
			get
			{
				return this.Properties.GetColor(DataGridViewCellStyle.PropBackColor);
			}
			set
			{
				Color backColor = this.BackColor;
				if (!value.IsEmpty || this.Properties.ContainsObject(DataGridViewCellStyle.PropBackColor))
				{
					this.Properties.SetColor(DataGridViewCellStyle.PropBackColor, value);
				}
				if (!backColor.Equals(this.BackColor))
				{
					this.OnPropertyChanged(DataGridViewCellStyle.DataGridViewCellStylePropertyInternal.Color);
				}
			}
		}

		/// <summary>Gets or sets the value saved to the data source when the user enters a null value into a cell.</summary>
		/// <returns>The value saved to the data source when the user specifies a null cell value. The default is <see cref="F:System.DBNull.Value" />.</returns>
		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06001E77 RID: 7799 RVA: 0x0008F985 File Offset: 0x0008DB85
		// (set) Token: 0x06001E78 RID: 7800 RVA: 0x0008F9B0 File Offset: 0x0008DBB0
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object DataSourceNullValue
		{
			get
			{
				if (this.Properties.ContainsObject(DataGridViewCellStyle.PropDataSourceNullValue))
				{
					return this.Properties.GetObject(DataGridViewCellStyle.PropDataSourceNullValue);
				}
				return DBNull.Value;
			}
			set
			{
				object dataSourceNullValue = this.DataSourceNullValue;
				if (dataSourceNullValue == value || (dataSourceNullValue != null && dataSourceNullValue.Equals(value)))
				{
					return;
				}
				if (value == DBNull.Value && this.Properties.ContainsObject(DataGridViewCellStyle.PropDataSourceNullValue))
				{
					this.Properties.RemoveObject(DataGridViewCellStyle.PropDataSourceNullValue);
				}
				else
				{
					this.Properties.SetObject(DataGridViewCellStyle.PropDataSourceNullValue, value);
				}
				this.OnPropertyChanged(DataGridViewCellStyle.DataGridViewCellStylePropertyInternal.Other);
			}
		}

		/// <summary>Gets or sets the font applied to the textual content of a <see cref="T:System.Windows.Forms.DataGridView" /> cell.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> applied to the cell text. The default is <see langword="null" />.</returns>
		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06001E79 RID: 7801 RVA: 0x0008FA19 File Offset: 0x0008DC19
		// (set) Token: 0x06001E7A RID: 7802 RVA: 0x0008FA30 File Offset: 0x0008DC30
		[SRCategory("CatAppearance")]
		public Font Font
		{
			get
			{
				return (Font)this.Properties.GetObject(DataGridViewCellStyle.PropFont);
			}
			set
			{
				Font font = this.Font;
				if (value != null || this.Properties.ContainsObject(DataGridViewCellStyle.PropFont))
				{
					this.Properties.SetObject(DataGridViewCellStyle.PropFont, value);
				}
				if ((font == null && value != null) || (font != null && value == null) || (font != null && value != null && !font.Equals(this.Font)))
				{
					this.OnPropertyChanged(DataGridViewCellStyle.DataGridViewCellStylePropertyInternal.Font);
				}
			}
		}

		/// <summary>Gets or sets the foreground color of a <see cref="T:System.Windows.Forms.DataGridView" /> cell.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of a cell. The default is <see cref="F:System.Drawing.Color.Empty" />.</returns>
		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06001E7B RID: 7803 RVA: 0x0008FA91 File Offset: 0x0008DC91
		// (set) Token: 0x06001E7C RID: 7804 RVA: 0x0008FAA4 File Offset: 0x0008DCA4
		[SRCategory("CatAppearance")]
		public Color ForeColor
		{
			get
			{
				return this.Properties.GetColor(DataGridViewCellStyle.PropForeColor);
			}
			set
			{
				Color foreColor = this.ForeColor;
				if (!value.IsEmpty || this.Properties.ContainsObject(DataGridViewCellStyle.PropForeColor))
				{
					this.Properties.SetColor(DataGridViewCellStyle.PropForeColor, value);
				}
				if (!foreColor.Equals(this.ForeColor))
				{
					this.OnPropertyChanged(DataGridViewCellStyle.DataGridViewCellStylePropertyInternal.ForeColor);
				}
			}
		}

		/// <summary>Gets or sets the format string applied to the textual content of a <see cref="T:System.Windows.Forms.DataGridView" /> cell.</summary>
		/// <returns>A string that indicates the format of the cell value. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06001E7D RID: 7805 RVA: 0x0008FB08 File Offset: 0x0008DD08
		// (set) Token: 0x06001E7E RID: 7806 RVA: 0x0008FB38 File Offset: 0x0008DD38
		[DefaultValue("")]
		[Editor("System.Windows.Forms.Design.FormatStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[SRCategory("CatBehavior")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string Format
		{
			get
			{
				object @object = this.Properties.GetObject(DataGridViewCellStyle.PropFormat);
				if (@object == null)
				{
					return string.Empty;
				}
				return (string)@object;
			}
			set
			{
				string format = this.Format;
				if ((value != null && value.Length > 0) || this.Properties.ContainsObject(DataGridViewCellStyle.PropFormat))
				{
					this.Properties.SetObject(DataGridViewCellStyle.PropFormat, value);
				}
				if (!format.Equals(this.Format))
				{
					this.OnPropertyChanged(DataGridViewCellStyle.DataGridViewCellStylePropertyInternal.Other);
				}
			}
		}

		/// <summary>Gets or sets the object used to provide culture-specific formatting of <see cref="T:System.Windows.Forms.DataGridView" /> cell values.</summary>
		/// <returns>An <see cref="T:System.IFormatProvider" /> used for cell formatting. The default is <see cref="P:System.Globalization.CultureInfo.CurrentUICulture" />.</returns>
		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06001E7F RID: 7807 RVA: 0x0008FB90 File Offset: 0x0008DD90
		// (set) Token: 0x06001E80 RID: 7808 RVA: 0x0008FBC0 File Offset: 0x0008DDC0
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IFormatProvider FormatProvider
		{
			get
			{
				object @object = this.Properties.GetObject(DataGridViewCellStyle.PropFormatProvider);
				if (@object == null)
				{
					return CultureInfo.CurrentCulture;
				}
				return (IFormatProvider)@object;
			}
			set
			{
				object @object = this.Properties.GetObject(DataGridViewCellStyle.PropFormatProvider);
				this.Properties.SetObject(DataGridViewCellStyle.PropFormatProvider, value);
				if (value != @object)
				{
					this.OnPropertyChanged(DataGridViewCellStyle.DataGridViewCellStylePropertyInternal.Other);
				}
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.DataSourceNullValue" /> property has been set.</summary>
		/// <returns>
		///   <see langword="true" /> if the value of the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.DataSourceNullValue" /> property is the default value; otherwise, <see langword="false" />.</returns>
		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06001E81 RID: 7809 RVA: 0x0008FBFA File Offset: 0x0008DDFA
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool IsDataSourceNullValueDefault
		{
			get
			{
				return !this.Properties.ContainsObject(DataGridViewCellStyle.PropDataSourceNullValue) || this.Properties.GetObject(DataGridViewCellStyle.PropDataSourceNullValue) == DBNull.Value;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.FormatProvider" /> property has been set.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.FormatProvider" /> property is the default value; otherwise, <see langword="false" />.</returns>
		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06001E82 RID: 7810 RVA: 0x0008FC27 File Offset: 0x0008DE27
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool IsFormatProviderDefault
		{
			get
			{
				return this.Properties.GetObject(DataGridViewCellStyle.PropFormatProvider) == null;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.NullValue" /> property has been set.</summary>
		/// <returns>
		///   <see langword="true" /> if the value of the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.NullValue" /> property is the default value; otherwise, <see langword="false" />.</returns>
		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06001E83 RID: 7811 RVA: 0x0008FC3C File Offset: 0x0008DE3C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool IsNullValueDefault
		{
			get
			{
				if (!this.Properties.ContainsObject(DataGridViewCellStyle.PropNullValue))
				{
					return true;
				}
				object @object = this.Properties.GetObject(DataGridViewCellStyle.PropNullValue);
				return @object is string && @object.Equals("");
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.DataGridView" /> cell display value corresponding to a cell value of <see cref="F:System.DBNull.Value" /> or <see langword="null" />.</summary>
		/// <returns>The object used to indicate a null value in a cell. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06001E84 RID: 7812 RVA: 0x0008FC83 File Offset: 0x0008DE83
		// (set) Token: 0x06001E85 RID: 7813 RVA: 0x0008FCB0 File Offset: 0x0008DEB0
		[DefaultValue("")]
		[TypeConverter(typeof(StringConverter))]
		[SRCategory("CatData")]
		public object NullValue
		{
			get
			{
				if (this.Properties.ContainsObject(DataGridViewCellStyle.PropNullValue))
				{
					return this.Properties.GetObject(DataGridViewCellStyle.PropNullValue);
				}
				return "";
			}
			set
			{
				object nullValue = this.NullValue;
				if (nullValue == value || (nullValue != null && nullValue.Equals(value)))
				{
					return;
				}
				if (value is string && value.Equals("") && this.Properties.ContainsObject(DataGridViewCellStyle.PropNullValue))
				{
					this.Properties.RemoveObject(DataGridViewCellStyle.PropNullValue);
				}
				else
				{
					this.Properties.SetObject(DataGridViewCellStyle.PropNullValue, value);
				}
				this.OnPropertyChanged(DataGridViewCellStyle.DataGridViewCellStylePropertyInternal.Other);
			}
		}

		/// <summary>Gets or sets the space between the edge of a <see cref="T:System.Windows.Forms.DataGridViewCell" /> and its content.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> that represents the space between the edge of a <see cref="T:System.Windows.Forms.DataGridViewCell" /> and its content.</returns>
		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06001E86 RID: 7814 RVA: 0x0008FD26 File Offset: 0x0008DF26
		// (set) Token: 0x06001E87 RID: 7815 RVA: 0x0008FD38 File Offset: 0x0008DF38
		[SRCategory("CatLayout")]
		public Padding Padding
		{
			get
			{
				return this.Properties.GetPadding(DataGridViewCellStyle.PropPadding);
			}
			set
			{
				if (value.Left < 0 || value.Right < 0 || value.Top < 0 || value.Bottom < 0)
				{
					if (value.All != -1)
					{
						value.All = 0;
					}
					else
					{
						value.Left = Math.Max(0, value.Left);
						value.Right = Math.Max(0, value.Right);
						value.Top = Math.Max(0, value.Top);
						value.Bottom = Math.Max(0, value.Bottom);
					}
				}
				this.PaddingInternal = value;
			}
		}

		// Token: 0x170006BB RID: 1723
		// (set) Token: 0x06001E88 RID: 7816 RVA: 0x0008FDD8 File Offset: 0x0008DFD8
		internal Padding PaddingInternal
		{
			set
			{
				if (value != this.Padding)
				{
					this.Properties.SetPadding(DataGridViewCellStyle.PropPadding, value);
					this.OnPropertyChanged(DataGridViewCellStyle.DataGridViewCellStylePropertyInternal.Other);
				}
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06001E89 RID: 7817 RVA: 0x0008FE00 File Offset: 0x0008E000
		internal PropertyStore Properties
		{
			get
			{
				return this.propertyStore;
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06001E8A RID: 7818 RVA: 0x0008FE08 File Offset: 0x0008E008
		// (set) Token: 0x06001E8B RID: 7819 RVA: 0x0008FE10 File Offset: 0x0008E010
		internal DataGridViewCellStyleScopes Scope
		{
			get
			{
				return this.scope;
			}
			set
			{
				this.scope = value;
			}
		}

		/// <summary>Gets or sets the background color used by a <see cref="T:System.Windows.Forms.DataGridView" /> cell when it is selected.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of a selected cell. The default is <see cref="F:System.Drawing.Color.Empty" />.</returns>
		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06001E8C RID: 7820 RVA: 0x0008FE19 File Offset: 0x0008E019
		// (set) Token: 0x06001E8D RID: 7821 RVA: 0x0008FE2C File Offset: 0x0008E02C
		[SRCategory("CatAppearance")]
		public Color SelectionBackColor
		{
			get
			{
				return this.Properties.GetColor(DataGridViewCellStyle.PropSelectionBackColor);
			}
			set
			{
				Color selectionBackColor = this.SelectionBackColor;
				if (!value.IsEmpty || this.Properties.ContainsObject(DataGridViewCellStyle.PropSelectionBackColor))
				{
					this.Properties.SetColor(DataGridViewCellStyle.PropSelectionBackColor, value);
				}
				if (!selectionBackColor.Equals(this.SelectionBackColor))
				{
					this.OnPropertyChanged(DataGridViewCellStyle.DataGridViewCellStylePropertyInternal.Color);
				}
			}
		}

		/// <summary>Gets or sets the foreground color used by a <see cref="T:System.Windows.Forms.DataGridView" /> cell when it is selected.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of a selected cell. The default is <see cref="F:System.Drawing.Color.Empty" />.</returns>
		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06001E8E RID: 7822 RVA: 0x0008FE8D File Offset: 0x0008E08D
		// (set) Token: 0x06001E8F RID: 7823 RVA: 0x0008FEA0 File Offset: 0x0008E0A0
		[SRCategory("CatAppearance")]
		public Color SelectionForeColor
		{
			get
			{
				return this.Properties.GetColor(DataGridViewCellStyle.PropSelectionForeColor);
			}
			set
			{
				Color selectionForeColor = this.SelectionForeColor;
				if (!value.IsEmpty || this.Properties.ContainsObject(DataGridViewCellStyle.PropSelectionForeColor))
				{
					this.Properties.SetColor(DataGridViewCellStyle.PropSelectionForeColor, value);
				}
				if (!selectionForeColor.Equals(this.SelectionForeColor))
				{
					this.OnPropertyChanged(DataGridViewCellStyle.DataGridViewCellStylePropertyInternal.Color);
				}
			}
		}

		/// <summary>Gets or sets an object that contains additional data related to the <see cref="T:System.Windows.Forms.DataGridViewCellStyle" />.</summary>
		/// <returns>An object that contains additional data. The default is <see langword="null" />.</returns>
		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06001E90 RID: 7824 RVA: 0x0008FF01 File Offset: 0x0008E101
		// (set) Token: 0x06001E91 RID: 7825 RVA: 0x0008FF13 File Offset: 0x0008E113
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object Tag
		{
			get
			{
				return this.Properties.GetObject(DataGridViewCellStyle.PropTag);
			}
			set
			{
				if (value != null || this.Properties.ContainsObject(DataGridViewCellStyle.PropTag))
				{
					this.Properties.SetObject(DataGridViewCellStyle.PropTag, value);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether textual content in a <see cref="T:System.Windows.Forms.DataGridView" /> cell is wrapped to subsequent lines or truncated when it is too long to fit on a single line.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewTriState" /> values. The default is <see cref="F:System.Windows.Forms.DataGridViewTriState.NotSet" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The property value is not a valid <see cref="T:System.Windows.Forms.DataGridViewTriState" /> value.</exception>
		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06001E92 RID: 7826 RVA: 0x0008FF3C File Offset: 0x0008E13C
		// (set) Token: 0x06001E93 RID: 7827 RVA: 0x0008FF62 File Offset: 0x0008E162
		[DefaultValue(DataGridViewTriState.NotSet)]
		[SRCategory("CatLayout")]
		public DataGridViewTriState WrapMode
		{
			get
			{
				bool flag;
				int integer = this.Properties.GetInteger(DataGridViewCellStyle.PropWrapMode, out flag);
				if (flag)
				{
					return (DataGridViewTriState)integer;
				}
				return DataGridViewTriState.NotSet;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DataGridViewTriState));
				}
				this.WrapModeInternal = value;
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (set) Token: 0x06001E94 RID: 7828 RVA: 0x0008FF91 File Offset: 0x0008E191
		internal DataGridViewTriState WrapModeInternal
		{
			set
			{
				if (this.WrapMode != value)
				{
					this.Properties.SetInteger(DataGridViewCellStyle.PropWrapMode, (int)value);
					this.OnPropertyChanged(DataGridViewCellStyle.DataGridViewCellStylePropertyInternal.Other);
				}
			}
		}

		// Token: 0x06001E95 RID: 7829 RVA: 0x0008FFB4 File Offset: 0x0008E1B4
		internal void AddScope(DataGridView dataGridView, DataGridViewCellStyleScopes scope)
		{
			this.scope |= scope;
			this.dataGridView = dataGridView;
		}

		/// <summary>Applies the specified <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to the current <see cref="T:System.Windows.Forms.DataGridViewCellStyle" />.</summary>
		/// <param name="dataGridViewCellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to apply to the current <see cref="T:System.Windows.Forms.DataGridViewCellStyle" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridViewCellStyle" /> is <see langword="null" />.</exception>
		// Token: 0x06001E96 RID: 7830 RVA: 0x0008FFCC File Offset: 0x0008E1CC
		public virtual void ApplyStyle(DataGridViewCellStyle dataGridViewCellStyle)
		{
			if (dataGridViewCellStyle == null)
			{
				throw new ArgumentNullException("dataGridViewCellStyle");
			}
			if (!dataGridViewCellStyle.BackColor.IsEmpty)
			{
				this.BackColor = dataGridViewCellStyle.BackColor;
			}
			if (!dataGridViewCellStyle.ForeColor.IsEmpty)
			{
				this.ForeColor = dataGridViewCellStyle.ForeColor;
			}
			if (!dataGridViewCellStyle.SelectionBackColor.IsEmpty)
			{
				this.SelectionBackColor = dataGridViewCellStyle.SelectionBackColor;
			}
			if (!dataGridViewCellStyle.SelectionForeColor.IsEmpty)
			{
				this.SelectionForeColor = dataGridViewCellStyle.SelectionForeColor;
			}
			if (dataGridViewCellStyle.Font != null)
			{
				this.Font = dataGridViewCellStyle.Font;
			}
			if (!dataGridViewCellStyle.IsNullValueDefault)
			{
				this.NullValue = dataGridViewCellStyle.NullValue;
			}
			if (!dataGridViewCellStyle.IsDataSourceNullValueDefault)
			{
				this.DataSourceNullValue = dataGridViewCellStyle.DataSourceNullValue;
			}
			if (dataGridViewCellStyle.Format.Length != 0)
			{
				this.Format = dataGridViewCellStyle.Format;
			}
			if (!dataGridViewCellStyle.IsFormatProviderDefault)
			{
				this.FormatProvider = dataGridViewCellStyle.FormatProvider;
			}
			if (dataGridViewCellStyle.Alignment != DataGridViewContentAlignment.NotSet)
			{
				this.AlignmentInternal = dataGridViewCellStyle.Alignment;
			}
			if (dataGridViewCellStyle.WrapMode != DataGridViewTriState.NotSet)
			{
				this.WrapModeInternal = dataGridViewCellStyle.WrapMode;
			}
			if (dataGridViewCellStyle.Tag != null)
			{
				this.Tag = dataGridViewCellStyle.Tag;
			}
			if (dataGridViewCellStyle.Padding != Padding.Empty)
			{
				this.PaddingInternal = dataGridViewCellStyle.Padding;
			}
		}

		/// <summary>Creates an exact copy of this <see cref="T:System.Windows.Forms.DataGridViewCellStyle" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents an exact copy of this cell style.</returns>
		// Token: 0x06001E97 RID: 7831 RVA: 0x0009011A File Offset: 0x0008E31A
		public virtual DataGridViewCellStyle Clone()
		{
			return new DataGridViewCellStyle(this);
		}

		/// <summary>Returns a value indicating whether this instance is equivalent to the specified object.</summary>
		/// <param name="o">An object to compare with this instance, or <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="o" /> is a <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> and has the same property values as this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001E98 RID: 7832 RVA: 0x00090124 File Offset: 0x0008E324
		public override bool Equals(object o)
		{
			DataGridViewCellStyle dataGridViewCellStyle = o as DataGridViewCellStyle;
			return dataGridViewCellStyle != null && this.GetDifferencesFrom(dataGridViewCellStyle) == DataGridViewCellStyleDifferences.None;
		}

		// Token: 0x06001E99 RID: 7833 RVA: 0x00090148 File Offset: 0x0008E348
		internal DataGridViewCellStyleDifferences GetDifferencesFrom(DataGridViewCellStyle dgvcs)
		{
			bool flag = dgvcs.Alignment != this.Alignment || dgvcs.DataSourceNullValue != this.DataSourceNullValue || dgvcs.Font != this.Font || dgvcs.Format != this.Format || dgvcs.FormatProvider != this.FormatProvider || dgvcs.NullValue != this.NullValue || dgvcs.Padding != this.Padding || dgvcs.Tag != this.Tag || dgvcs.WrapMode != this.WrapMode;
			bool flag2 = dgvcs.BackColor != this.BackColor || dgvcs.ForeColor != this.ForeColor || dgvcs.SelectionBackColor != this.SelectionBackColor || dgvcs.SelectionForeColor != this.SelectionForeColor;
			if (flag)
			{
				return DataGridViewCellStyleDifferences.AffectPreferredSize;
			}
			if (flag2)
			{
				return DataGridViewCellStyleDifferences.DoNotAffectPreferredSize;
			}
			return DataGridViewCellStyleDifferences.None;
		}

		/// <summary>Serves as a hash function for a particular type.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
		// Token: 0x06001E9A RID: 7834 RVA: 0x00090240 File Offset: 0x0008E440
		public override int GetHashCode()
		{
			return WindowsFormsUtils.GetCombinedHashCodes(new int[]
			{
				(int)this.Alignment,
				(int)this.WrapMode,
				this.Padding.GetHashCode(),
				this.Format.GetHashCode(),
				this.BackColor.GetHashCode(),
				this.ForeColor.GetHashCode(),
				this.SelectionBackColor.GetHashCode(),
				this.SelectionForeColor.GetHashCode(),
				(this.Font == null) ? 1 : this.Font.GetHashCode(),
				(this.NullValue == null) ? 1 : this.NullValue.GetHashCode(),
				(this.DataSourceNullValue == null) ? 1 : this.DataSourceNullValue.GetHashCode(),
				(this.Tag == null) ? 1 : this.Tag.GetHashCode()
			});
		}

		// Token: 0x06001E9B RID: 7835 RVA: 0x00090353 File Offset: 0x0008E553
		private void OnPropertyChanged(DataGridViewCellStyle.DataGridViewCellStylePropertyInternal property)
		{
			if (this.dataGridView != null && this.scope != DataGridViewCellStyleScopes.None)
			{
				this.dataGridView.OnCellStyleContentChanged(this, property);
			}
		}

		// Token: 0x06001E9C RID: 7836 RVA: 0x00090372 File Offset: 0x0008E572
		internal void RemoveScope(DataGridViewCellStyleScopes scope)
		{
			this.scope &= ~scope;
			if (this.scope == DataGridViewCellStyleScopes.None)
			{
				this.dataGridView = null;
			}
		}

		// Token: 0x06001E9D RID: 7837 RVA: 0x00090394 File Offset: 0x0008E594
		private bool ShouldSerializeBackColor()
		{
			bool flag;
			this.Properties.GetColor(DataGridViewCellStyle.PropBackColor, out flag);
			return flag;
		}

		// Token: 0x06001E9E RID: 7838 RVA: 0x000903B5 File Offset: 0x0008E5B5
		private bool ShouldSerializeFont()
		{
			return this.Properties.GetObject(DataGridViewCellStyle.PropFont) != null;
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x000903CC File Offset: 0x0008E5CC
		private bool ShouldSerializeForeColor()
		{
			bool flag;
			this.Properties.GetColor(DataGridViewCellStyle.PropForeColor, out flag);
			return flag;
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x000903ED File Offset: 0x0008E5ED
		private bool ShouldSerializeFormatProvider()
		{
			return this.Properties.GetObject(DataGridViewCellStyle.PropFormatProvider) != null;
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x00090402 File Offset: 0x0008E602
		private bool ShouldSerializePadding()
		{
			return this.Padding != Padding.Empty;
		}

		// Token: 0x06001EA2 RID: 7842 RVA: 0x00090414 File Offset: 0x0008E614
		private bool ShouldSerializeSelectionBackColor()
		{
			bool flag;
			this.Properties.GetObject(DataGridViewCellStyle.PropSelectionBackColor, out flag);
			return flag;
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x00090438 File Offset: 0x0008E638
		private bool ShouldSerializeSelectionForeColor()
		{
			bool flag;
			this.Properties.GetColor(DataGridViewCellStyle.PropSelectionForeColor, out flag);
			return flag;
		}

		/// <summary>Returns a string indicating the current property settings of the <see cref="T:System.Windows.Forms.DataGridViewCellStyle" />.</summary>
		/// <returns>A string indicating the current property settings of the <see cref="T:System.Windows.Forms.DataGridViewCellStyle" />.</returns>
		// Token: 0x06001EA4 RID: 7844 RVA: 0x0009045C File Offset: 0x0008E65C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			stringBuilder.Append("DataGridViewCellStyle {");
			bool flag = true;
			if (this.BackColor != Color.Empty)
			{
				stringBuilder.Append(" BackColor=" + this.BackColor.ToString());
				flag = false;
			}
			if (this.ForeColor != Color.Empty)
			{
				if (!flag)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(" ForeColor=" + this.ForeColor.ToString());
				flag = false;
			}
			if (this.SelectionBackColor != Color.Empty)
			{
				if (!flag)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(" SelectionBackColor=" + this.SelectionBackColor.ToString());
				flag = false;
			}
			if (this.SelectionForeColor != Color.Empty)
			{
				if (!flag)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(" SelectionForeColor=" + this.SelectionForeColor.ToString());
				flag = false;
			}
			if (this.Font != null)
			{
				if (!flag)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(" Font=" + this.Font.ToString());
				flag = false;
			}
			if (!this.IsNullValueDefault && this.NullValue != null)
			{
				if (!flag)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(" NullValue=" + this.NullValue.ToString());
				flag = false;
			}
			if (!this.IsDataSourceNullValueDefault && this.DataSourceNullValue != null)
			{
				if (!flag)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(" DataSourceNullValue=" + this.DataSourceNullValue.ToString());
				flag = false;
			}
			if (!string.IsNullOrEmpty(this.Format))
			{
				if (!flag)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(" Format=" + this.Format);
				flag = false;
			}
			if (this.WrapMode != DataGridViewTriState.NotSet)
			{
				if (!flag)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(" WrapMode=" + this.WrapMode.ToString());
				flag = false;
			}
			if (this.Alignment != DataGridViewContentAlignment.NotSet)
			{
				if (!flag)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(" Alignment=" + this.Alignment.ToString());
				flag = false;
			}
			if (this.Padding != Padding.Empty)
			{
				if (!flag)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(" Padding=" + this.Padding.ToString());
				flag = false;
			}
			if (this.Tag != null)
			{
				if (!flag)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(" Tag=" + this.Tag.ToString());
			}
			stringBuilder.Append(" }");
			return stringBuilder.ToString();
		}

		/// <summary>Creates an exact copy of this <see cref="T:System.Windows.Forms.DataGridViewCellStyle" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents an exact copy of this cell style.</returns>
		// Token: 0x06001EA5 RID: 7845 RVA: 0x00090784 File Offset: 0x0008E984
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x04000CDA RID: 3290
		private static readonly int PropAlignment = PropertyStore.CreateKey();

		// Token: 0x04000CDB RID: 3291
		private static readonly int PropBackColor = PropertyStore.CreateKey();

		// Token: 0x04000CDC RID: 3292
		private static readonly int PropDataSourceNullValue = PropertyStore.CreateKey();

		// Token: 0x04000CDD RID: 3293
		private static readonly int PropFont = PropertyStore.CreateKey();

		// Token: 0x04000CDE RID: 3294
		private static readonly int PropForeColor = PropertyStore.CreateKey();

		// Token: 0x04000CDF RID: 3295
		private static readonly int PropFormat = PropertyStore.CreateKey();

		// Token: 0x04000CE0 RID: 3296
		private static readonly int PropFormatProvider = PropertyStore.CreateKey();

		// Token: 0x04000CE1 RID: 3297
		private static readonly int PropNullValue = PropertyStore.CreateKey();

		// Token: 0x04000CE2 RID: 3298
		private static readonly int PropPadding = PropertyStore.CreateKey();

		// Token: 0x04000CE3 RID: 3299
		private static readonly int PropSelectionBackColor = PropertyStore.CreateKey();

		// Token: 0x04000CE4 RID: 3300
		private static readonly int PropSelectionForeColor = PropertyStore.CreateKey();

		// Token: 0x04000CE5 RID: 3301
		private static readonly int PropTag = PropertyStore.CreateKey();

		// Token: 0x04000CE6 RID: 3302
		private static readonly int PropWrapMode = PropertyStore.CreateKey();

		// Token: 0x04000CE7 RID: 3303
		private const string DATAGRIDVIEWCELLSTYLE_nullText = "";

		// Token: 0x04000CE8 RID: 3304
		private DataGridViewCellStyleScopes scope;

		// Token: 0x04000CE9 RID: 3305
		private PropertyStore propertyStore;

		// Token: 0x04000CEA RID: 3306
		private DataGridView dataGridView;

		// Token: 0x02000666 RID: 1638
		internal enum DataGridViewCellStylePropertyInternal
		{
			// Token: 0x04003A57 RID: 14935
			Color,
			// Token: 0x04003A58 RID: 14936
			Other,
			// Token: 0x04003A59 RID: 14937
			Font,
			// Token: 0x04003A5A RID: 14938
			ForeColor
		}
	}
}
