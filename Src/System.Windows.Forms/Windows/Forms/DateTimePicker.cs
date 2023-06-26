using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.Layout;
using Microsoft.Win32;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows control that allows the user to select a date and a time and to display the date and time with a specified format.</summary>
	// Token: 0x02000229 RID: 553
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultProperty("Value")]
	[DefaultEvent("ValueChanged")]
	[DefaultBindingProperty("Value")]
	[Designer("System.Windows.Forms.Design.DateTimePickerDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionDateTimePicker")]
	public class DateTimePicker : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DateTimePicker" /> class.</summary>
		// Token: 0x060023D5 RID: 9173 RVA: 0x000AA9EC File Offset: 0x000A8BEC
		public DateTimePicker()
		{
			base.SetState2(2048, true);
			base.SetStyle(ControlStyles.FixedHeight, true);
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.StandardClick, false);
			this.format = DateTimePickerFormat.Long;
			if (AccessibilityImprovements.Level3)
			{
				base.SetStyle(ControlStyles.UseTextForAccessibility, false);
			}
		}

		/// <summary>Gets or sets a value indicating the background color of the <see cref="T:System.Windows.Forms.DateTimePicker" /> control.</summary>
		/// <returns>The background <see cref="T:System.Drawing.Color" /> of the <see cref="T:System.Windows.Forms.DateTimePicker" />.</returns>
		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x060023D6 RID: 9174 RVA: 0x00027DA7 File Offset: 0x00025FA7
		// (set) Token: 0x060023D7 RID: 9175 RVA: 0x00012D84 File Offset: 0x00010F84
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color BackColor
		{
			get
			{
				if (this.ShouldSerializeBackColor())
				{
					return base.BackColor;
				}
				return SystemColors.Window;
			}
			set
			{
				base.BackColor = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DateTimePicker.BackColor" /> property changes.</summary>
		// Token: 0x14000187 RID: 391
		// (add) Token: 0x060023D8 RID: 9176 RVA: 0x00058BF2 File Offset: 0x00056DF2
		// (remove) Token: 0x060023D9 RID: 9177 RVA: 0x00058BFB File Offset: 0x00056DFB
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackColorChanged
		{
			add
			{
				base.BackColorChanged += value;
			}
			remove
			{
				base.BackColorChanged -= value;
			}
		}

		/// <summary>Gets or sets the background image for the control.</summary>
		/// <returns>The background image for the control.</returns>
		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x060023DA RID: 9178 RVA: 0x0001187C File Offset: 0x0000FA7C
		// (set) Token: 0x060023DB RID: 9179 RVA: 0x00011884 File Offset: 0x0000FA84
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
			set
			{
				base.BackgroundImage = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DateTimePicker.BackgroundImage" /> property changes.</summary>
		// Token: 0x14000188 RID: 392
		// (add) Token: 0x060023DC RID: 9180 RVA: 0x0001188D File Offset: 0x0000FA8D
		// (remove) Token: 0x060023DD RID: 9181 RVA: 0x00011896 File Offset: 0x0000FA96
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageChanged
		{
			add
			{
				base.BackgroundImageChanged += value;
			}
			remove
			{
				base.BackgroundImageChanged -= value;
			}
		}

		/// <summary>Gets or sets the layout of the background image of the <see cref="T:System.Windows.Forms.DateTimePicker" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImageLayout" /> values.</returns>
		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x060023DE RID: 9182 RVA: 0x0001189F File Offset: 0x0000FA9F
		// (set) Token: 0x060023DF RID: 9183 RVA: 0x000118A7 File Offset: 0x0000FAA7
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ImageLayout BackgroundImageLayout
		{
			get
			{
				return base.BackgroundImageLayout;
			}
			set
			{
				base.BackgroundImageLayout = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DateTimePicker.BackgroundImageLayout" /> property changes.</summary>
		// Token: 0x14000189 RID: 393
		// (add) Token: 0x060023E0 RID: 9184 RVA: 0x000118B0 File Offset: 0x0000FAB0
		// (remove) Token: 0x060023E1 RID: 9185 RVA: 0x000118B9 File Offset: 0x0000FAB9
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageLayoutChanged
		{
			add
			{
				base.BackgroundImageLayoutChanged += value;
			}
			remove
			{
				base.BackgroundImageLayoutChanged -= value;
			}
		}

		/// <summary>Gets or sets the foreground color of the calendar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the calendar.</returns>
		/// <exception cref="T:System.ArgumentException">The value assigned is <see langword="null" />.</exception>
		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x060023E2 RID: 9186 RVA: 0x000AAAAB File Offset: 0x000A8CAB
		// (set) Token: 0x060023E3 RID: 9187 RVA: 0x000AAAB4 File Offset: 0x000A8CB4
		[SRCategory("CatAppearance")]
		[SRDescription("DateTimePickerCalendarForeColorDescr")]
		public Color CalendarForeColor
		{
			get
			{
				return this.calendarForeColor;
			}
			set
			{
				if (value.IsEmpty)
				{
					throw new ArgumentException(SR.GetString("InvalidNullArgument", new object[] { "value" }));
				}
				if (!value.Equals(this.calendarForeColor))
				{
					this.calendarForeColor = value;
					this.SetControlColor(1, value);
				}
			}
		}

		/// <summary>Gets or sets the font style applied to the calendar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Font" /> that represents the font style applied to the calendar.</returns>
		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x060023E4 RID: 9188 RVA: 0x000AAB11 File Offset: 0x000A8D11
		// (set) Token: 0x060023E5 RID: 9189 RVA: 0x000AAB28 File Offset: 0x000A8D28
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[AmbientValue(null)]
		[SRDescription("DateTimePickerCalendarFontDescr")]
		public Font CalendarFont
		{
			get
			{
				if (this.calendarFont == null)
				{
					return this.Font;
				}
				return this.calendarFont;
			}
			set
			{
				if ((value == null && this.calendarFont != null) || (value != null && !value.Equals(this.calendarFont)))
				{
					this.calendarFont = value;
					this.calendarFontHandleWrapper = null;
					this.SetControlCalendarFont();
				}
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x060023E6 RID: 9190 RVA: 0x000AAB5A File Offset: 0x000A8D5A
		private IntPtr CalendarFontHandle
		{
			get
			{
				if (this.calendarFont == null)
				{
					return base.FontHandle;
				}
				if (this.calendarFontHandleWrapper == null)
				{
					this.calendarFontHandleWrapper = new Control.FontHandleWrapper(this.CalendarFont);
				}
				return this.calendarFontHandleWrapper.Handle;
			}
		}

		/// <summary>Gets or sets the background color of the calendar title.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the calendar title.</returns>
		/// <exception cref="T:System.ArgumentException">The value assigned is <see langword="null" />.</exception>
		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x060023E7 RID: 9191 RVA: 0x000AAB8F File Offset: 0x000A8D8F
		// (set) Token: 0x060023E8 RID: 9192 RVA: 0x000AAB98 File Offset: 0x000A8D98
		[SRCategory("CatAppearance")]
		[SRDescription("DateTimePickerCalendarTitleBackColorDescr")]
		public Color CalendarTitleBackColor
		{
			get
			{
				return this.calendarTitleBackColor;
			}
			set
			{
				if (value.IsEmpty)
				{
					throw new ArgumentException(SR.GetString("InvalidNullArgument", new object[] { "value" }));
				}
				if (!value.Equals(this.calendarTitleBackColor))
				{
					this.calendarTitleBackColor = value;
					this.SetControlColor(2, value);
				}
			}
		}

		/// <summary>Gets or sets the foreground color of the calendar title.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the calendar title.</returns>
		/// <exception cref="T:System.ArgumentException">The value assigned is <see langword="null" />.</exception>
		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x060023E9 RID: 9193 RVA: 0x000AABF5 File Offset: 0x000A8DF5
		// (set) Token: 0x060023EA RID: 9194 RVA: 0x000AAC00 File Offset: 0x000A8E00
		[SRCategory("CatAppearance")]
		[SRDescription("DateTimePickerCalendarTitleForeColorDescr")]
		public Color CalendarTitleForeColor
		{
			get
			{
				return this.calendarTitleForeColor;
			}
			set
			{
				if (value.IsEmpty)
				{
					throw new ArgumentException(SR.GetString("InvalidNullArgument", new object[] { "value" }));
				}
				if (!value.Equals(this.calendarTitleForeColor))
				{
					this.calendarTitleForeColor = value;
					this.SetControlColor(3, value);
				}
			}
		}

		/// <summary>Gets or sets the foreground color of the calendar trailing dates.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the calendar trailing dates.</returns>
		/// <exception cref="T:System.ArgumentException">The value assigned is <see langword="null" />.</exception>
		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x060023EB RID: 9195 RVA: 0x000AAC5D File Offset: 0x000A8E5D
		// (set) Token: 0x060023EC RID: 9196 RVA: 0x000AAC68 File Offset: 0x000A8E68
		[SRCategory("CatAppearance")]
		[SRDescription("DateTimePickerCalendarTrailingForeColorDescr")]
		public Color CalendarTrailingForeColor
		{
			get
			{
				return this.calendarTrailingText;
			}
			set
			{
				if (value.IsEmpty)
				{
					throw new ArgumentException(SR.GetString("InvalidNullArgument", new object[] { "value" }));
				}
				if (!value.Equals(this.calendarTrailingText))
				{
					this.calendarTrailingText = value;
					this.SetControlColor(5, value);
				}
			}
		}

		/// <summary>Gets or sets the background color of the calendar month.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the calendar month.</returns>
		/// <exception cref="T:System.ArgumentException">The value assigned is <see langword="null" />.</exception>
		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x060023ED RID: 9197 RVA: 0x000AACC5 File Offset: 0x000A8EC5
		// (set) Token: 0x060023EE RID: 9198 RVA: 0x000AACD0 File Offset: 0x000A8ED0
		[SRCategory("CatAppearance")]
		[SRDescription("DateTimePickerCalendarMonthBackgroundDescr")]
		public Color CalendarMonthBackground
		{
			get
			{
				return this.calendarMonthBackground;
			}
			set
			{
				if (value.IsEmpty)
				{
					throw new ArgumentException(SR.GetString("InvalidNullArgument", new object[] { "value" }));
				}
				if (!value.Equals(this.calendarMonthBackground))
				{
					this.calendarMonthBackground = value;
					this.SetControlColor(4, value);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="P:System.Windows.Forms.DateTimePicker.Value" /> property has been set with a valid date/time value and the displayed value is able to be updated.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Windows.Forms.DateTimePicker.Value" /> property has been set with a valid <see cref="T:System.DateTime" /> value and the displayed value is able to be updated; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x060023EF RID: 9199 RVA: 0x000AAD30 File Offset: 0x000A8F30
		// (set) Token: 0x060023F0 RID: 9200 RVA: 0x000AAD7C File Offset: 0x000A8F7C
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[Bindable(true)]
		[SRDescription("DateTimePickerCheckedDescr")]
		public bool Checked
		{
			get
			{
				if (this.ShowCheckBox && base.IsHandleCreated)
				{
					NativeMethods.SYSTEMTIME systemtime = new NativeMethods.SYSTEMTIME();
					int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4097, 0, systemtime);
					return num == 0;
				}
				return this.validTime;
			}
			set
			{
				if (this.Checked != value)
				{
					if (this.ShowCheckBox && base.IsHandleCreated)
					{
						if (value)
						{
							int num = 0;
							NativeMethods.SYSTEMTIME systemtime = DateTimePicker.DateTimeToSysTime(this.Value);
							UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4098, num, systemtime);
						}
						else
						{
							int num2 = 1;
							NativeMethods.SYSTEMTIME systemtime2 = null;
							UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4098, num2, systemtime2);
						}
					}
					this.validTime = value;
				}
			}
		}

		/// <summary>Occurs when the control is clicked.</summary>
		// Token: 0x1400018A RID: 394
		// (add) Token: 0x060023F1 RID: 9201 RVA: 0x00012FD4 File Offset: 0x000111D4
		// (remove) Token: 0x060023F2 RID: 9202 RVA: 0x00012FDD File Offset: 0x000111DD
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler Click
		{
			add
			{
				base.Click += value;
			}
			remove
			{
				base.Click -= value;
			}
		}

		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x060023F3 RID: 9203 RVA: 0x000AADF4 File Offset: 0x000A8FF4
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ClassName = "SysDateTimePick32";
				createParams.Style |= this.style;
				DateTimePickerFormat dateTimePickerFormat = this.format;
				switch (dateTimePickerFormat)
				{
				case DateTimePickerFormat.Long:
					createParams.Style |= 4;
					break;
				case DateTimePickerFormat.Short:
				case (DateTimePickerFormat)3:
					break;
				case DateTimePickerFormat.Time:
					createParams.Style |= 8;
					break;
				default:
					if (dateTimePickerFormat != DateTimePickerFormat.Custom)
					{
					}
					break;
				}
				createParams.ExStyle |= 512;
				if (this.RightToLeft == RightToLeft.Yes && this.RightToLeftLayout)
				{
					createParams.ExStyle |= 4194304;
					createParams.ExStyle &= -28673;
				}
				return createParams;
			}
		}

		/// <summary>Gets or sets the custom date/time format string.</summary>
		/// <returns>A string that represents the custom date/time format. The default is <see langword="null" />.</returns>
		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x060023F4 RID: 9204 RVA: 0x000AAEB1 File Offset: 0x000A90B1
		// (set) Token: 0x060023F5 RID: 9205 RVA: 0x000AAEBC File Offset: 0x000A90BC
		[DefaultValue(null)]
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRCategory("CatBehavior")]
		[SRDescription("DateTimePickerCustomFormatDescr")]
		public string CustomFormat
		{
			get
			{
				return this.customFormat;
			}
			set
			{
				if ((value != null && !value.Equals(this.customFormat)) || (value == null && this.customFormat != null))
				{
					this.customFormat = value;
					if (base.IsHandleCreated && this.format == DateTimePickerFormat.Custom)
					{
						base.SendMessage(NativeMethods.DTM_SETFORMAT, 0, this.customFormat);
					}
				}
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x060023F6 RID: 9206 RVA: 0x000AAF10 File Offset: 0x000A9110
		protected override Size DefaultSize
		{
			get
			{
				return new Size(200, this.PreferredHeight);
			}
		}

		/// <summary>Gets or sets a value indicating whether the control should redraw its surface using a secondary buffer. Setting this property has no effect on the <see cref="T:System.Windows.Forms.DateTimePicker" /> control.</summary>
		/// <returns>
		///   <see langword="true" /> if the control should redraw its surface using a secondary buffer; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x060023F7 RID: 9207 RVA: 0x00012FC3 File Offset: 0x000111C3
		// (set) Token: 0x060023F8 RID: 9208 RVA: 0x00012FCB File Offset: 0x000111CB
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override bool DoubleBuffered
		{
			get
			{
				return base.DoubleBuffered;
			}
			set
			{
				base.DoubleBuffered = value;
			}
		}

		/// <summary>Occurs when the control is double-clicked.</summary>
		// Token: 0x1400018B RID: 395
		// (add) Token: 0x060023F9 RID: 9209 RVA: 0x00023757 File Offset: 0x00021957
		// (remove) Token: 0x060023FA RID: 9210 RVA: 0x00023760 File Offset: 0x00021960
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler DoubleClick
		{
			add
			{
				base.DoubleClick += value;
			}
			remove
			{
				base.DoubleClick -= value;
			}
		}

		/// <summary>Gets or sets the alignment of the drop-down calendar on the <see cref="T:System.Windows.Forms.DateTimePicker" /> control.</summary>
		/// <returns>The alignment of the drop-down calendar on the control. The default is <see cref="F:System.Windows.Forms.LeftRightAlignment.Left" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.LeftRightAlignment" /> values.</exception>
		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x060023FB RID: 9211 RVA: 0x000AAF22 File Offset: 0x000A9122
		// (set) Token: 0x060023FC RID: 9212 RVA: 0x000AAF32 File Offset: 0x000A9132
		[DefaultValue(LeftRightAlignment.Left)]
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[SRDescription("DateTimePickerDropDownAlignDescr")]
		public LeftRightAlignment DropDownAlign
		{
			get
			{
				if ((this.style & 32) == 0)
				{
					return LeftRightAlignment.Left;
				}
				return LeftRightAlignment.Right;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 1))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(LeftRightAlignment));
				}
				this.SetStyleBit(value == LeftRightAlignment.Right, 32);
			}
		}

		/// <summary>Gets or sets the foreground color of the <see cref="T:System.Windows.Forms.DateTimePicker" /> control.</summary>
		/// <returns>The foreground <see cref="T:System.Drawing.Color" /> of the <see cref="T:System.Windows.Forms.DateTimePicker" />.</returns>
		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x060023FD RID: 9213 RVA: 0x0001300E File Offset: 0x0001120E
		// (set) Token: 0x060023FE RID: 9214 RVA: 0x00013024 File Offset: 0x00011224
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color ForeColor
		{
			get
			{
				if (this.ShouldSerializeForeColor())
				{
					return base.ForeColor;
				}
				return SystemColors.WindowText;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DateTimePicker.ForeColor" /> property changes.</summary>
		// Token: 0x1400018C RID: 396
		// (add) Token: 0x060023FF RID: 9215 RVA: 0x0005A8EE File Offset: 0x00058AEE
		// (remove) Token: 0x06002400 RID: 9216 RVA: 0x0005A8F7 File Offset: 0x00058AF7
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ForeColorChanged
		{
			add
			{
				base.ForeColorChanged += value;
			}
			remove
			{
				base.ForeColorChanged -= value;
			}
		}

		/// <summary>Gets or sets the format of the date and time displayed in the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DateTimePickerFormat" /> values. The default is <see cref="F:System.Windows.Forms.DateTimePickerFormat.Long" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.DateTimePickerFormat" /> values.</exception>
		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06002401 RID: 9217 RVA: 0x000AAF66 File Offset: 0x000A9166
		// (set) Token: 0x06002402 RID: 9218 RVA: 0x000AAF70 File Offset: 0x000A9170
		[SRCategory("CatAppearance")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRDescription("DateTimePickerFormatDescr")]
		public DateTimePickerFormat Format
		{
			get
			{
				return this.format;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 1, 8, 1))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DateTimePickerFormat));
				}
				if (this.format != value)
				{
					this.format = value;
					base.RecreateHandle();
					this.OnFormatChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DateTimePicker.Format" /> property value has changed.</summary>
		// Token: 0x1400018D RID: 397
		// (add) Token: 0x06002403 RID: 9219 RVA: 0x000AAFC5 File Offset: 0x000A91C5
		// (remove) Token: 0x06002404 RID: 9220 RVA: 0x000AAFD8 File Offset: 0x000A91D8
		[SRCategory("CatPropertyChanged")]
		[SRDescription("DateTimePickerOnFormatChangedDescr")]
		public event EventHandler FormatChanged
		{
			add
			{
				base.Events.AddHandler(DateTimePicker.EVENT_FORMATCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(DateTimePicker.EVENT_FORMATCHANGED, value);
			}
		}

		/// <summary>Occurs when the control is redrawn.</summary>
		// Token: 0x1400018E RID: 398
		// (add) Token: 0x06002405 RID: 9221 RVA: 0x00013D73 File Offset: 0x00011F73
		// (remove) Token: 0x06002406 RID: 9222 RVA: 0x00013D7C File Offset: 0x00011F7C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event PaintEventHandler Paint
		{
			add
			{
				base.Paint += value;
			}
			remove
			{
				base.Paint -= value;
			}
		}

		// Token: 0x06002407 RID: 9223 RVA: 0x000AAFEC File Offset: 0x000A91EC
		internal static DateTime EffectiveMinDate(DateTime minDate)
		{
			DateTime minimumDateTime = DateTimePicker.MinimumDateTime;
			if (minDate < minimumDateTime)
			{
				return minimumDateTime;
			}
			return minDate;
		}

		// Token: 0x06002408 RID: 9224 RVA: 0x000AB00C File Offset: 0x000A920C
		internal static DateTime EffectiveMaxDate(DateTime maxDate)
		{
			DateTime maximumDateTime = DateTimePicker.MaximumDateTime;
			if (maxDate > maximumDateTime)
			{
				return maximumDateTime;
			}
			return maxDate;
		}

		/// <summary>Gets or sets the maximum date and time that can be selected in the control.</summary>
		/// <returns>The maximum date and time that can be selected in the control. The default is determined as the minimum of the CurrentCulture's Calendar's <see cref="P:System.Globalization.Calendar.MaxSupportedDateTime" /> property and December 31st  9998 12 am.</returns>
		/// <exception cref="T:System.ArgumentException">The value assigned is less than the <see cref="P:System.Windows.Forms.DateTimePicker.MinDate" /> value.</exception>
		/// <exception cref="T:System.SystemException">The value assigned is greater than the <see cref="F:System.Windows.Forms.DateTimePicker.MaxDateTime" /> value.</exception>
		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06002409 RID: 9225 RVA: 0x000AB02B File Offset: 0x000A922B
		// (set) Token: 0x0600240A RID: 9226 RVA: 0x000AB038 File Offset: 0x000A9238
		[SRCategory("CatBehavior")]
		[SRDescription("DateTimePickerMaxDateDescr")]
		public DateTime MaxDate
		{
			get
			{
				return DateTimePicker.EffectiveMaxDate(this.max);
			}
			set
			{
				if (value != this.max)
				{
					if (value < DateTimePicker.EffectiveMinDate(this.min))
					{
						throw new ArgumentOutOfRangeException("MaxDate", SR.GetString("InvalidLowBoundArgumentEx", new object[]
						{
							"MaxDate",
							DateTimePicker.FormatDateTime(value),
							"MinDate"
						}));
					}
					if (value > DateTimePicker.MaximumDateTime)
					{
						throw new ArgumentOutOfRangeException("MaxDate", SR.GetString("DateTimePickerMaxDate", new object[] { DateTimePicker.FormatDateTime(DateTimePicker.MaxDateTime) }));
					}
					this.max = value;
					this.SetRange();
					if (this.Value > this.max)
					{
						this.Value = this.max;
					}
				}
			}
		}

		/// <summary>Gets the maximum date value allowed for the <see cref="T:System.Windows.Forms.DateTimePicker" /> control.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> representing the maximum date value for the <see cref="P:System.Windows.Forms.DateTimePicker.MaximumDateTime" /> control.</returns>
		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x0600240B RID: 9227 RVA: 0x000AB100 File Offset: 0x000A9300
		public static DateTime MaximumDateTime
		{
			get
			{
				DateTime maxSupportedDateTime = CultureInfo.CurrentCulture.Calendar.MaxSupportedDateTime;
				if (maxSupportedDateTime.Year > DateTimePicker.MaxDateTime.Year)
				{
					return DateTimePicker.MaxDateTime;
				}
				return maxSupportedDateTime;
			}
		}

		/// <summary>Gets or sets the minimum date and time that can be selected in the control.</summary>
		/// <returns>The minimum date and time that can be selected in the control. The default is 1/1/1753 00:00:00.</returns>
		/// <exception cref="T:System.ArgumentException">The value assigned is not less than the <see cref="P:System.Windows.Forms.DateTimePicker.MaxDate" /> value.</exception>
		/// <exception cref="T:System.SystemException">The value assigned is less than the <see cref="F:System.Windows.Forms.DateTimePicker.MinDateTime" /> value.</exception>
		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x0600240C RID: 9228 RVA: 0x000AB13A File Offset: 0x000A933A
		// (set) Token: 0x0600240D RID: 9229 RVA: 0x000AB148 File Offset: 0x000A9348
		[SRCategory("CatBehavior")]
		[SRDescription("DateTimePickerMinDateDescr")]
		public DateTime MinDate
		{
			get
			{
				return DateTimePicker.EffectiveMinDate(this.min);
			}
			set
			{
				if (value != this.min)
				{
					if (value > DateTimePicker.EffectiveMaxDate(this.max))
					{
						throw new ArgumentOutOfRangeException("MinDate", SR.GetString("InvalidHighBoundArgument", new object[]
						{
							"MinDate",
							DateTimePicker.FormatDateTime(value),
							"MaxDate"
						}));
					}
					if (value < DateTimePicker.MinimumDateTime)
					{
						throw new ArgumentOutOfRangeException("MinDate", SR.GetString("DateTimePickerMinDate", new object[] { DateTimePicker.FormatDateTime(DateTimePicker.MinimumDateTime) }));
					}
					this.min = value;
					this.SetRange();
					if (this.Value < this.min)
					{
						this.Value = this.min;
					}
				}
			}
		}

		/// <summary>Gets the minimum date value allowed for the <see cref="T:System.Windows.Forms.DateTimePicker" /> control.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> representing the minimum date value for the <see cref="P:System.Windows.Forms.DateTimePicker.MaximumDateTime" /> control.</returns>
		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x0600240E RID: 9230 RVA: 0x000AB210 File Offset: 0x000A9410
		public static DateTime MinimumDateTime
		{
			get
			{
				DateTime minSupportedDateTime = CultureInfo.CurrentCulture.Calendar.MinSupportedDateTime;
				if (minSupportedDateTime.Year < 1753)
				{
					return new DateTime(1753, 1, 1);
				}
				return minSupportedDateTime;
			}
		}

		/// <summary>Occurs when the control is clicked with the mouse.</summary>
		// Token: 0x1400018F RID: 399
		// (add) Token: 0x0600240F RID: 9231 RVA: 0x00012FE6 File Offset: 0x000111E6
		// (remove) Token: 0x06002410 RID: 9232 RVA: 0x00012FEF File Offset: 0x000111EF
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event MouseEventHandler MouseClick
		{
			add
			{
				base.MouseClick += value;
			}
			remove
			{
				base.MouseClick -= value;
			}
		}

		/// <summary>Occurs when the control is double-clicked with the mouse.</summary>
		// Token: 0x14000190 RID: 400
		// (add) Token: 0x06002411 RID: 9233 RVA: 0x00023769 File Offset: 0x00021969
		// (remove) Token: 0x06002412 RID: 9234 RVA: 0x00023772 File Offset: 0x00021972
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event MouseEventHandler MouseDoubleClick
		{
			add
			{
				base.MouseDoubleClick += value;
			}
			remove
			{
				base.MouseDoubleClick -= value;
			}
		}

		/// <summary>Gets or sets the spacing between the contents of the <see cref="T:System.Windows.Forms.DateTimePicker" /> control and its edges.</summary>
		/// <returns>
		///   <see cref="F:System.Windows.Forms.Padding.Empty" /> in all cases.</returns>
		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06002413 RID: 9235 RVA: 0x00013442 File Offset: 0x00011642
		// (set) Token: 0x06002414 RID: 9236 RVA: 0x0001344A File Offset: 0x0001164A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Padding Padding
		{
			get
			{
				return base.Padding;
			}
			set
			{
				base.Padding = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DateTimePicker.Padding" /> property changes.</summary>
		// Token: 0x14000191 RID: 401
		// (add) Token: 0x06002415 RID: 9237 RVA: 0x00013453 File Offset: 0x00011653
		// (remove) Token: 0x06002416 RID: 9238 RVA: 0x0001345C File Offset: 0x0001165C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler PaddingChanged
		{
			add
			{
				base.PaddingChanged += value;
			}
			remove
			{
				base.PaddingChanged -= value;
			}
		}

		/// <summary>Gets the preferred height of the <see cref="T:System.Windows.Forms.DateTimePicker" /> control.</summary>
		/// <returns>The preferred height, in pixels, of the <see cref="T:System.Windows.Forms.DateTimePicker" /> control.</returns>
		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06002417 RID: 9239 RVA: 0x000AB24C File Offset: 0x000A944C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int PreferredHeight
		{
			get
			{
				if (this.prefHeightCache > -1)
				{
					return (int)this.prefHeightCache;
				}
				int num = base.FontHeight;
				num += SystemInformation.BorderSize.Height * 4 + 3;
				this.prefHeightCache = (short)num;
				return num;
			}
		}

		/// <summary>Gets or sets whether the contents of the <see cref="T:System.Windows.Forms.DateTimePicker" /> are laid out from right to left.</summary>
		/// <returns>
		///   <see langword="true" /> if the layout of the <see cref="T:System.Windows.Forms.DateTimePicker" /> contents is from right to left; otherwise, <see langword="false" />. The default is <see langword="false." /></returns>
		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x06002418 RID: 9240 RVA: 0x000AB28D File Offset: 0x000A948D
		// (set) Token: 0x06002419 RID: 9241 RVA: 0x000AB298 File Offset: 0x000A9498
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[DefaultValue(false)]
		[SRDescription("ControlRightToLeftLayoutDescr")]
		public virtual bool RightToLeftLayout
		{
			get
			{
				return this.rightToLeftLayout;
			}
			set
			{
				if (value != this.rightToLeftLayout)
				{
					this.rightToLeftLayout = value;
					using (new LayoutTransaction(this, this, PropertyNames.RightToLeftLayout))
					{
						this.OnRightToLeftLayoutChanged(EventArgs.Empty);
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether a check box is displayed to the left of the selected date.</summary>
		/// <returns>
		///   <see langword="true" /> if a check box is displayed to the left of the selected date; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x0600241A RID: 9242 RVA: 0x000AB2EC File Offset: 0x000A94EC
		// (set) Token: 0x0600241B RID: 9243 RVA: 0x000AB2F9 File Offset: 0x000A94F9
		[DefaultValue(false)]
		[SRCategory("CatAppearance")]
		[SRDescription("DateTimePickerShowNoneDescr")]
		public bool ShowCheckBox
		{
			get
			{
				return (this.style & 2) != 0;
			}
			set
			{
				this.SetStyleBit(value, 2);
			}
		}

		/// <summary>Gets or sets a value indicating whether a spin button control (also known as an up-down control) is used to adjust the date/time value.</summary>
		/// <returns>
		///   <see langword="true" /> if a spin button control is used to adjust the date/time value; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x0600241C RID: 9244 RVA: 0x000AB303 File Offset: 0x000A9503
		// (set) Token: 0x0600241D RID: 9245 RVA: 0x000AB310 File Offset: 0x000A9510
		[DefaultValue(false)]
		[SRCategory("CatAppearance")]
		[SRDescription("DateTimePickerShowUpDownDescr")]
		public bool ShowUpDown
		{
			get
			{
				return (this.style & 1) != 0;
			}
			set
			{
				if (this.ShowUpDown != value)
				{
					this.SetStyleBit(value, 1);
				}
			}
		}

		/// <summary>Gets or sets the text associated with this control.</summary>
		/// <returns>A string that represents the text associated with this control.</returns>
		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x0600241E RID: 9246 RVA: 0x00013814 File Offset: 0x00011A14
		// (set) Token: 0x0600241F RID: 9247 RVA: 0x000AB323 File Offset: 0x000A9523
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					this.ResetValue();
					return;
				}
				this.Value = DateTime.Parse(value, CultureInfo.CurrentCulture);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DateTimePicker.Text" /> property changes.</summary>
		// Token: 0x14000192 RID: 402
		// (add) Token: 0x06002420 RID: 9248 RVA: 0x00046591 File Offset: 0x00044791
		// (remove) Token: 0x06002421 RID: 9249 RVA: 0x0004659A File Offset: 0x0004479A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event EventHandler TextChanged
		{
			add
			{
				base.TextChanged += value;
			}
			remove
			{
				base.TextChanged -= value;
			}
		}

		/// <summary>Gets or sets the date/time value assigned to the control.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> value assign to the control.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The set value is less than <see cref="P:System.Windows.Forms.DateTimePicker.MinDate" /> or more than <see cref="P:System.Windows.Forms.DateTimePicker.MaxDate" />.</exception>
		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06002422 RID: 9250 RVA: 0x000AB348 File Offset: 0x000A9548
		// (set) Token: 0x06002423 RID: 9251 RVA: 0x000AB368 File Offset: 0x000A9568
		[SRCategory("CatBehavior")]
		[Bindable(true)]
		[RefreshProperties(RefreshProperties.All)]
		[SRDescription("DateTimePickerValueDescr")]
		public DateTime Value
		{
			get
			{
				if (!this.userHasSetValue && this.validTime)
				{
					return this.creationTime;
				}
				return this.value;
			}
			set
			{
				bool flag = !DateTime.Equals(this.Value, value);
				if (!this.userHasSetValue || flag)
				{
					if (value < this.MinDate || value > this.MaxDate)
					{
						throw new ArgumentOutOfRangeException("Value", SR.GetString("InvalidBoundArgument", new object[]
						{
							"Value",
							DateTimePicker.FormatDateTime(value),
							"'MinDate'",
							"'MaxDate'"
						}));
					}
					string text = this.Text;
					this.value = value;
					this.userHasSetValue = true;
					if (base.IsHandleCreated)
					{
						int num = 0;
						NativeMethods.SYSTEMTIME systemtime = DateTimePicker.DateTimeToSysTime(value);
						UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4098, num, systemtime);
					}
					if (flag)
					{
						this.OnValueChanged(EventArgs.Empty);
					}
					if (!text.Equals(this.Text))
					{
						this.OnTextChanged(EventArgs.Empty);
					}
				}
			}
		}

		/// <summary>Occurs when the drop-down calendar is dismissed and disappears.</summary>
		// Token: 0x14000193 RID: 403
		// (add) Token: 0x06002424 RID: 9252 RVA: 0x000AB453 File Offset: 0x000A9653
		// (remove) Token: 0x06002425 RID: 9253 RVA: 0x000AB46C File Offset: 0x000A966C
		[SRCategory("CatAction")]
		[SRDescription("DateTimePickerOnCloseUpDescr")]
		public event EventHandler CloseUp
		{
			add
			{
				this.onCloseUp = (EventHandler)Delegate.Combine(this.onCloseUp, value);
			}
			remove
			{
				this.onCloseUp = (EventHandler)Delegate.Remove(this.onCloseUp, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DateTimePicker.RightToLeftLayout" /> property changes.</summary>
		// Token: 0x14000194 RID: 404
		// (add) Token: 0x06002426 RID: 9254 RVA: 0x000AB485 File Offset: 0x000A9685
		// (remove) Token: 0x06002427 RID: 9255 RVA: 0x000AB49E File Offset: 0x000A969E
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnRightToLeftLayoutChangedDescr")]
		public event EventHandler RightToLeftLayoutChanged
		{
			add
			{
				this.onRightToLeftLayoutChanged = (EventHandler)Delegate.Combine(this.onRightToLeftLayoutChanged, value);
			}
			remove
			{
				this.onRightToLeftLayoutChanged = (EventHandler)Delegate.Remove(this.onRightToLeftLayoutChanged, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DateTimePicker.Value" /> property changes.</summary>
		// Token: 0x14000195 RID: 405
		// (add) Token: 0x06002428 RID: 9256 RVA: 0x000AB4B7 File Offset: 0x000A96B7
		// (remove) Token: 0x06002429 RID: 9257 RVA: 0x000AB4D0 File Offset: 0x000A96D0
		[SRCategory("CatAction")]
		[SRDescription("valueChangedEventDescr")]
		public event EventHandler ValueChanged
		{
			add
			{
				this.onValueChanged = (EventHandler)Delegate.Combine(this.onValueChanged, value);
			}
			remove
			{
				this.onValueChanged = (EventHandler)Delegate.Remove(this.onValueChanged, value);
			}
		}

		/// <summary>Occurs when the drop-down calendar is shown.</summary>
		// Token: 0x14000196 RID: 406
		// (add) Token: 0x0600242A RID: 9258 RVA: 0x000AB4E9 File Offset: 0x000A96E9
		// (remove) Token: 0x0600242B RID: 9259 RVA: 0x000AB502 File Offset: 0x000A9702
		[SRCategory("CatAction")]
		[SRDescription("DateTimePickerOnDropDownDescr")]
		public event EventHandler DropDown
		{
			add
			{
				this.onDropDown = (EventHandler)Delegate.Combine(this.onDropDown, value);
			}
			remove
			{
				this.onDropDown = (EventHandler)Delegate.Remove(this.onDropDown, value);
			}
		}

		/// <summary>Creates a new accessibility object for the <see cref="T:System.Windows.Forms.DateTimePicker" /> control.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.DateTimePicker.DateTimePickerAccessibleObject" /> for the control.</returns>
		// Token: 0x0600242C RID: 9260 RVA: 0x000AB51B File Offset: 0x000A971B
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new DateTimePicker.DateTimePickerAccessibleObject(this);
		}

		/// <summary>Creates the physical window handle.</summary>
		// Token: 0x0600242D RID: 9261 RVA: 0x000AB524 File Offset: 0x000A9724
		protected override void CreateHandle()
		{
			if (!base.RecreatingHandle)
			{
				IntPtr intPtr = UnsafeNativeMethods.ThemingScope.Activate();
				try
				{
					SafeNativeMethods.InitCommonControlsEx(new NativeMethods.INITCOMMONCONTROLSEX
					{
						dwICC = 256
					});
				}
				finally
				{
					UnsafeNativeMethods.ThemingScope.Deactivate(intPtr);
				}
			}
			this.creationTime = DateTime.Now;
			base.CreateHandle();
			if (this.userHasSetValue && this.validTime)
			{
				int num = 0;
				NativeMethods.SYSTEMTIME systemtime = DateTimePicker.DateTimeToSysTime(this.Value);
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4098, num, systemtime);
			}
			else if (!this.validTime)
			{
				int num2 = 1;
				NativeMethods.SYSTEMTIME systemtime2 = null;
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4098, num2, systemtime2);
			}
			if (this.format == DateTimePickerFormat.Custom)
			{
				base.SendMessage(NativeMethods.DTM_SETFORMAT, 0, this.customFormat);
			}
			this.UpdateUpDown();
			this.SetAllControlColors();
			this.SetControlCalendarFont();
			this.SetRange();
		}

		/// <summary>Destroys the physical window handle.</summary>
		// Token: 0x0600242E RID: 9262 RVA: 0x000AB618 File Offset: 0x000A9818
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected override void DestroyHandle()
		{
			this.value = this.Value;
			base.DestroyHandle();
		}

		// Token: 0x0600242F RID: 9263 RVA: 0x000AB62C File Offset: 0x000A982C
		private static string FormatDateTime(DateTime value)
		{
			return value.ToString("G", CultureInfo.CurrentCulture);
		}

		// Token: 0x06002430 RID: 9264 RVA: 0x000AB63F File Offset: 0x000A983F
		internal override Rectangle ApplyBoundsConstraints(int suggestedX, int suggestedY, int proposedWidth, int proposedHeight)
		{
			return base.ApplyBoundsConstraints(suggestedX, suggestedY, proposedWidth, this.PreferredHeight);
		}

		// Token: 0x06002431 RID: 9265 RVA: 0x000AB650 File Offset: 0x000A9850
		internal override Size GetPreferredSizeCore(Size proposedConstraints)
		{
			int preferredHeight = this.PreferredHeight;
			int width = CommonProperties.GetSpecifiedBounds(this).Width;
			return new Size(width, preferredHeight);
		}

		/// <summary>Determines whether the specified key is a regular input key or a special key that requires preprocessing.</summary>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values.</param>
		/// <returns>
		///   <see langword="true" /> if the specified key is a regular input key; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002432 RID: 9266 RVA: 0x000AB67C File Offset: 0x000A987C
		protected override bool IsInputKey(Keys keyData)
		{
			if ((keyData & Keys.Alt) == Keys.Alt)
			{
				return false;
			}
			Keys keys = keyData & Keys.KeyCode;
			return keys - Keys.Prior <= 3 || base.IsInputKey(keyData);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DateTimePicker.CloseUp" /> event.</summary>
		/// <param name="eventargs">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002433 RID: 9267 RVA: 0x000AB6B1 File Offset: 0x000A98B1
		protected virtual void OnCloseUp(EventArgs eventargs)
		{
			if (this.onCloseUp != null)
			{
				this.onCloseUp(this, eventargs);
			}
			this._expandCollapseState = UnsafeNativeMethods.ExpandCollapseState.Collapsed;
			if (AccessibilityImprovements.Level5 && base.IsAccessibilityObjectCreated)
			{
				base.AccessibilityObject.RaiseAutomationEvent(20005);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DateTimePicker.DropDown" /> event.</summary>
		/// <param name="eventargs">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002434 RID: 9268 RVA: 0x000AB6EF File Offset: 0x000A98EF
		protected virtual void OnDropDown(EventArgs eventargs)
		{
			if (this.onDropDown != null)
			{
				this.onDropDown(this, eventargs);
			}
			this._expandCollapseState = UnsafeNativeMethods.ExpandCollapseState.Expanded;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DateTimePicker.FormatChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002435 RID: 9269 RVA: 0x000AB710 File Offset: 0x000A9910
		protected virtual void OnFormatChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DateTimePicker.EVENT_FORMATCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002436 RID: 9270 RVA: 0x000AB73E File Offset: 0x000A993E
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			SystemEvents.UserPreferenceChanged += this.MarshaledUserPreferenceChanged;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002437 RID: 9271 RVA: 0x000AB758 File Offset: 0x000A9958
		protected override void OnHandleDestroyed(EventArgs e)
		{
			SystemEvents.UserPreferenceChanged -= this.MarshaledUserPreferenceChanged;
			base.OnHandleDestroyed(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DateTimePicker.ValueChanged" /> event.</summary>
		/// <param name="eventargs">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002438 RID: 9272 RVA: 0x000AB772 File Offset: 0x000A9972
		protected virtual void OnValueChanged(EventArgs eventargs)
		{
			if (this.onValueChanged != null)
			{
				this.onValueChanged(this, eventargs);
			}
		}

		/// <summary>Raises the <see cref="P:System.Windows.Forms.DateTimePicker.RightToLeftLayout" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002439 RID: 9273 RVA: 0x000AB789 File Offset: 0x000A9989
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnRightToLeftLayoutChanged(EventArgs e)
		{
			if (base.GetAnyDisposingInHierarchy())
			{
				return;
			}
			if (this.RightToLeft == RightToLeft.Yes)
			{
				base.RecreateHandle();
			}
			if (this.onRightToLeftLayoutChanged != null)
			{
				this.onRightToLeftLayoutChanged(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600243A RID: 9274 RVA: 0x000AB7B8 File Offset: 0x000A99B8
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.prefHeightCache = -1;
			base.Height = this.PreferredHeight;
			if (this.calendarFont == null)
			{
				this.calendarFontHandleWrapper = null;
				this.SetControlCalendarFont();
			}
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x000AB7E9 File Offset: 0x000A99E9
		private void ResetCalendarForeColor()
		{
			this.CalendarForeColor = Control.DefaultForeColor;
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x000AB7F6 File Offset: 0x000A99F6
		private void ResetCalendarFont()
		{
			this.CalendarFont = null;
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x000AB7FF File Offset: 0x000A99FF
		private void ResetCalendarMonthBackground()
		{
			this.CalendarMonthBackground = DateTimePicker.DefaultMonthBackColor;
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x000AB80C File Offset: 0x000A9A0C
		private void ResetCalendarTitleBackColor()
		{
			this.CalendarTitleBackColor = DateTimePicker.DefaultTitleBackColor;
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x000AB819 File Offset: 0x000A9A19
		private void ResetCalendarTitleForeColor()
		{
			this.CalendarTitleBackColor = Control.DefaultForeColor;
		}

		// Token: 0x06002440 RID: 9280 RVA: 0x000AB826 File Offset: 0x000A9A26
		private void ResetCalendarTrailingForeColor()
		{
			this.CalendarTrailingForeColor = DateTimePicker.DefaultTrailingForeColor;
		}

		// Token: 0x06002441 RID: 9281 RVA: 0x000AB833 File Offset: 0x000A9A33
		private void ResetFormat()
		{
			this.Format = DateTimePickerFormat.Long;
		}

		// Token: 0x06002442 RID: 9282 RVA: 0x000AB83C File Offset: 0x000A9A3C
		private void ResetMaxDate()
		{
			this.MaxDate = DateTimePicker.MaximumDateTime;
		}

		// Token: 0x06002443 RID: 9283 RVA: 0x000AB849 File Offset: 0x000A9A49
		private void ResetMinDate()
		{
			this.MinDate = DateTimePicker.MinimumDateTime;
		}

		// Token: 0x06002444 RID: 9284 RVA: 0x000AB858 File Offset: 0x000A9A58
		private void ResetValue()
		{
			this.value = DateTime.Now;
			this.userHasSetValue = false;
			if (base.IsHandleCreated)
			{
				int num = 0;
				NativeMethods.SYSTEMTIME systemtime = DateTimePicker.DateTimeToSysTime(this.value);
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4098, num, systemtime);
			}
			this.Checked = false;
			this.OnValueChanged(EventArgs.Empty);
			this.OnTextChanged(EventArgs.Empty);
		}

		// Token: 0x06002445 RID: 9285 RVA: 0x000AB8C3 File Offset: 0x000A9AC3
		private void SetControlColor(int colorIndex, Color value)
		{
			if (base.IsHandleCreated)
			{
				base.SendMessage(4102, colorIndex, ColorTranslator.ToWin32(value));
			}
		}

		// Token: 0x06002446 RID: 9286 RVA: 0x000AB8E0 File Offset: 0x000A9AE0
		private void SetControlCalendarFont()
		{
			if (base.IsHandleCreated)
			{
				base.SendMessage(4105, this.CalendarFontHandle, NativeMethods.InvalidIntPtr);
			}
		}

		// Token: 0x06002447 RID: 9287 RVA: 0x000AB904 File Offset: 0x000A9B04
		private void SetAllControlColors()
		{
			this.SetControlColor(4, this.calendarMonthBackground);
			this.SetControlColor(1, this.calendarForeColor);
			this.SetControlColor(2, this.calendarTitleBackColor);
			this.SetControlColor(3, this.calendarTitleForeColor);
			this.SetControlColor(5, this.calendarTrailingText);
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x000AB952 File Offset: 0x000A9B52
		private void SetRange()
		{
			this.SetRange(DateTimePicker.EffectiveMinDate(this.min), DateTimePicker.EffectiveMaxDate(this.max));
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x000AB970 File Offset: 0x000A9B70
		private void SetRange(DateTime min, DateTime max)
		{
			if (base.IsHandleCreated)
			{
				int num = 0;
				NativeMethods.SYSTEMTIMEARRAY systemtimearray = new NativeMethods.SYSTEMTIMEARRAY();
				num |= 3;
				NativeMethods.SYSTEMTIME systemtime = DateTimePicker.DateTimeToSysTime(min);
				systemtimearray.wYear1 = systemtime.wYear;
				systemtimearray.wMonth1 = systemtime.wMonth;
				systemtimearray.wDayOfWeek1 = systemtime.wDayOfWeek;
				systemtimearray.wDay1 = systemtime.wDay;
				systemtimearray.wHour1 = systemtime.wHour;
				systemtimearray.wMinute1 = systemtime.wMinute;
				systemtimearray.wSecond1 = systemtime.wSecond;
				systemtimearray.wMilliseconds1 = systemtime.wMilliseconds;
				systemtime = DateTimePicker.DateTimeToSysTime(max);
				systemtimearray.wYear2 = systemtime.wYear;
				systemtimearray.wMonth2 = systemtime.wMonth;
				systemtimearray.wDayOfWeek2 = systemtime.wDayOfWeek;
				systemtimearray.wDay2 = systemtime.wDay;
				systemtimearray.wHour2 = systemtime.wHour;
				systemtimearray.wMinute2 = systemtime.wMinute;
				systemtimearray.wSecond2 = systemtime.wSecond;
				systemtimearray.wMilliseconds2 = systemtime.wMilliseconds;
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4100, num, systemtimearray);
			}
		}

		// Token: 0x0600244A RID: 9290 RVA: 0x000ABA7C File Offset: 0x000A9C7C
		private void SetStyleBit(bool flag, int bit)
		{
			if ((this.style & bit) != 0 == flag)
			{
				return;
			}
			if (flag)
			{
				this.style |= bit;
			}
			else
			{
				this.style &= ~bit;
			}
			if (base.IsHandleCreated)
			{
				base.RecreateHandle();
				base.Invalidate();
				base.Update();
			}
		}

		// Token: 0x0600244B RID: 9291 RVA: 0x000ABAD4 File Offset: 0x000A9CD4
		private bool ShouldSerializeCalendarForeColor()
		{
			return !this.CalendarForeColor.Equals(Control.DefaultForeColor);
		}

		// Token: 0x0600244C RID: 9292 RVA: 0x000ABB02 File Offset: 0x000A9D02
		private bool ShouldSerializeCalendarFont()
		{
			return this.calendarFont != null;
		}

		// Token: 0x0600244D RID: 9293 RVA: 0x000ABB0D File Offset: 0x000A9D0D
		private bool ShouldSerializeCalendarTitleBackColor()
		{
			return !this.calendarTitleBackColor.Equals(DateTimePicker.DefaultTitleBackColor);
		}

		// Token: 0x0600244E RID: 9294 RVA: 0x000ABB2D File Offset: 0x000A9D2D
		private bool ShouldSerializeCalendarTitleForeColor()
		{
			return !this.calendarTitleForeColor.Equals(DateTimePicker.DefaultTitleForeColor);
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x000ABB4D File Offset: 0x000A9D4D
		private bool ShouldSerializeCalendarTrailingForeColor()
		{
			return !this.calendarTrailingText.Equals(DateTimePicker.DefaultTrailingForeColor);
		}

		// Token: 0x06002450 RID: 9296 RVA: 0x000ABB6D File Offset: 0x000A9D6D
		private bool ShouldSerializeCalendarMonthBackground()
		{
			return !this.calendarMonthBackground.Equals(DateTimePicker.DefaultMonthBackColor);
		}

		// Token: 0x06002451 RID: 9297 RVA: 0x000ABB8D File Offset: 0x000A9D8D
		private bool ShouldSerializeMaxDate()
		{
			return this.max != DateTimePicker.MaximumDateTime && this.max != DateTime.MaxValue;
		}

		// Token: 0x06002452 RID: 9298 RVA: 0x000ABBB3 File Offset: 0x000A9DB3
		private bool ShouldSerializeMinDate()
		{
			return this.min != DateTimePicker.MinimumDateTime && this.min != DateTime.MinValue;
		}

		// Token: 0x06002453 RID: 9299 RVA: 0x000ABBD9 File Offset: 0x000A9DD9
		private bool ShouldSerializeValue()
		{
			return this.userHasSetValue;
		}

		// Token: 0x06002454 RID: 9300 RVA: 0x000ABBE1 File Offset: 0x000A9DE1
		private bool ShouldSerializeFormat()
		{
			return this.Format != DateTimePickerFormat.Long;
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Windows.Forms.DateTimePicker" /> control.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.DateTimePicker" />. The string includes the type and the <see cref="P:System.Windows.Forms.DateTimePicker.Value" /> property of the control.</returns>
		// Token: 0x06002455 RID: 9301 RVA: 0x000ABBF0 File Offset: 0x000A9DF0
		public override string ToString()
		{
			string text = base.ToString();
			return text + ", Value: " + DateTimePicker.FormatDateTime(this.Value);
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x000ABC1C File Offset: 0x000A9E1C
		private void UpdateUpDown()
		{
			if (this.ShowUpDown)
			{
				DateTimePicker.EnumChildren enumChildren = new DateTimePicker.EnumChildren();
				NativeMethods.EnumChildrenCallback enumChildrenCallback = new NativeMethods.EnumChildrenCallback(enumChildren.enumChildren);
				UnsafeNativeMethods.EnumChildWindows(new HandleRef(this, base.Handle), enumChildrenCallback, NativeMethods.NullHandleRef);
				if (enumChildren.hwndFound != IntPtr.Zero)
				{
					SafeNativeMethods.InvalidateRect(new HandleRef(enumChildren, enumChildren.hwndFound), null, true);
					SafeNativeMethods.UpdateWindow(new HandleRef(enumChildren, enumChildren.hwndFound));
				}
			}
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x000ABC94 File Offset: 0x000A9E94
		private void MarshaledUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs pref)
		{
			try
			{
				base.BeginInvoke(new UserPreferenceChangedEventHandler(this.UserPreferenceChanged), new object[] { sender, pref });
			}
			catch (InvalidOperationException)
			{
			}
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x000ABCD8 File Offset: 0x000A9ED8
		private void UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs pref)
		{
			if (pref.Category == UserPreferenceCategory.Locale)
			{
				base.RecreateHandle();
			}
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x000ABCEA File Offset: 0x000A9EEA
		private void WmCloseUp(ref Message m)
		{
			this.OnCloseUp(EventArgs.Empty);
		}

		// Token: 0x0600245A RID: 9306 RVA: 0x000ABCF8 File Offset: 0x000A9EF8
		private void WmDateTimeChange(ref Message m)
		{
			NativeMethods.NMDATETIMECHANGE nmdatetimechange = (NativeMethods.NMDATETIMECHANGE)m.GetLParam(typeof(NativeMethods.NMDATETIMECHANGE));
			DateTime dateTime = this.value;
			bool flag = this.validTime;
			if (nmdatetimechange.dwFlags != 1)
			{
				this.validTime = true;
				this.value = DateTimePicker.SysTimeToDateTime(nmdatetimechange.st);
				this.userHasSetValue = true;
			}
			else
			{
				this.validTime = false;
			}
			if (this.value != dateTime || flag != this.validTime)
			{
				this.OnValueChanged(EventArgs.Empty);
				this.OnTextChanged(EventArgs.Empty);
			}
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x000ABD88 File Offset: 0x000A9F88
		private void WmDropDown(ref Message m)
		{
			if (this.RightToLeftLayout && this.RightToLeft == RightToLeft.Yes)
			{
				IntPtr intPtr = base.SendMessage(4104, 0, 0);
				if (intPtr != IntPtr.Zero)
				{
					int num = (int)(long)UnsafeNativeMethods.GetWindowLong(new HandleRef(this, intPtr), -20);
					num |= 5242880;
					num &= -12289;
					UnsafeNativeMethods.SetWindowLong(new HandleRef(this, intPtr), -20, new HandleRef(this, (IntPtr)num));
				}
			}
			this.OnDropDown(EventArgs.Empty);
		}

		/// <summary>Overrides the <see cref="M:System.Windows.Forms.Control.OnSystemColorsChanged(System.EventArgs)" /> method.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600245C RID: 9308 RVA: 0x000ABE0C File Offset: 0x000AA00C
		protected override void OnSystemColorsChanged(EventArgs e)
		{
			this.SetAllControlColors();
			base.OnSystemColorsChanged(e);
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x000ABE1C File Offset: 0x000AA01C
		private void WmReflectCommand(ref Message m)
		{
			if (m.HWnd == base.Handle)
			{
				NativeMethods.NMHDR nmhdr = (NativeMethods.NMHDR)m.GetLParam(typeof(NativeMethods.NMHDR));
				int code = nmhdr.code;
				if (code == -759)
				{
					this.WmDateTimeChange(ref m);
					return;
				}
				if (code != -754)
				{
					if (code == -753)
					{
						this.WmCloseUp(ref m);
						return;
					}
				}
				else
				{
					this.WmDropDown(ref m);
				}
			}
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x0600245E RID: 9310 RVA: 0x000ABE88 File Offset: 0x000AA088
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg != 71)
			{
				if (msg != 513)
				{
					if (msg == 8270)
					{
						this.WmReflectCommand(ref m);
						base.WndProc(ref m);
						return;
					}
					base.WndProc(ref m);
				}
				else
				{
					this.FocusInternal();
					if (!base.ValidationCancelled)
					{
						base.WndProc(ref m);
						return;
					}
				}
				return;
			}
			base.WndProc(ref m);
			this.UpdateUpDown();
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x000ABEF0 File Offset: 0x000AA0F0
		internal static NativeMethods.SYSTEMTIME DateTimeToSysTime(DateTime time)
		{
			return new NativeMethods.SYSTEMTIME
			{
				wYear = (short)time.Year,
				wMonth = (short)time.Month,
				wDayOfWeek = (short)time.DayOfWeek,
				wDay = (short)time.Day,
				wHour = (short)time.Hour,
				wMinute = (short)time.Minute,
				wSecond = (short)time.Second,
				wMilliseconds = 0
			};
		}

		// Token: 0x06002460 RID: 9312 RVA: 0x000ABF6D File Offset: 0x000AA16D
		internal static DateTime SysTimeToDateTime(NativeMethods.SYSTEMTIME s)
		{
			return new DateTime((int)s.wYear, (int)s.wMonth, (int)s.wDay, (int)s.wHour, (int)s.wMinute, (int)s.wSecond);
		}

		/// <summary>Specifies the default title back color of the <see cref="T:System.Windows.Forms.DateTimePicker" /> control. This field is read-only.</summary>
		// Token: 0x04000EAE RID: 3758
		protected static readonly Color DefaultTitleBackColor = SystemColors.ActiveCaption;

		/// <summary>Specifies the default title foreground color of the <see cref="T:System.Windows.Forms.DateTimePicker" /> control. This field is read-only.</summary>
		// Token: 0x04000EAF RID: 3759
		protected static readonly Color DefaultTitleForeColor = SystemColors.ActiveCaptionText;

		/// <summary>Specifies the default month background color of the <see cref="T:System.Windows.Forms.DateTimePicker" /> control. This field is read-only.</summary>
		// Token: 0x04000EB0 RID: 3760
		protected static readonly Color DefaultMonthBackColor = SystemColors.Window;

		/// <summary>Specifies the default trailing foreground color of the <see cref="T:System.Windows.Forms.DateTimePicker" /> control. This field is read-only.</summary>
		// Token: 0x04000EB1 RID: 3761
		protected static readonly Color DefaultTrailingForeColor = SystemColors.GrayText;

		// Token: 0x04000EB2 RID: 3762
		private static readonly object EVENT_FORMATCHANGED = new object();

		// Token: 0x04000EB3 RID: 3763
		private static readonly string DateTimePickerLocalizedControlTypeString = SR.GetString("DateTimePickerLocalizedControlType");

		// Token: 0x04000EB4 RID: 3764
		private const int TIMEFORMAT_NOUPDOWN = 8;

		// Token: 0x04000EB5 RID: 3765
		private EventHandler onCloseUp;

		// Token: 0x04000EB6 RID: 3766
		private EventHandler onDropDown;

		// Token: 0x04000EB7 RID: 3767
		private EventHandler onValueChanged;

		// Token: 0x04000EB8 RID: 3768
		private EventHandler onRightToLeftLayoutChanged;

		// Token: 0x04000EB9 RID: 3769
		private UnsafeNativeMethods.ExpandCollapseState _expandCollapseState;

		/// <summary>Gets the minimum date value of the <see cref="T:System.Windows.Forms.DateTimePicker" /> control.</summary>
		// Token: 0x04000EBA RID: 3770
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static readonly DateTime MinDateTime = new DateTime(1753, 1, 1);

		/// <summary>Specifies the maximum date value of the <see cref="T:System.Windows.Forms.DateTimePicker" /> control. This field is read-only.</summary>
		// Token: 0x04000EBB RID: 3771
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static readonly DateTime MaxDateTime = new DateTime(9998, 12, 31);

		// Token: 0x04000EBC RID: 3772
		private int style;

		// Token: 0x04000EBD RID: 3773
		private short prefHeightCache = -1;

		// Token: 0x04000EBE RID: 3774
		private bool validTime = true;

		// Token: 0x04000EBF RID: 3775
		private bool userHasSetValue;

		// Token: 0x04000EC0 RID: 3776
		private DateTime value = DateTime.Now;

		// Token: 0x04000EC1 RID: 3777
		private DateTime creationTime = DateTime.Now;

		// Token: 0x04000EC2 RID: 3778
		private DateTime max = DateTime.MaxValue;

		// Token: 0x04000EC3 RID: 3779
		private DateTime min = DateTime.MinValue;

		// Token: 0x04000EC4 RID: 3780
		private Color calendarForeColor = Control.DefaultForeColor;

		// Token: 0x04000EC5 RID: 3781
		private Color calendarTitleBackColor = DateTimePicker.DefaultTitleBackColor;

		// Token: 0x04000EC6 RID: 3782
		private Color calendarTitleForeColor = DateTimePicker.DefaultTitleForeColor;

		// Token: 0x04000EC7 RID: 3783
		private Color calendarMonthBackground = DateTimePicker.DefaultMonthBackColor;

		// Token: 0x04000EC8 RID: 3784
		private Color calendarTrailingText = DateTimePicker.DefaultTrailingForeColor;

		// Token: 0x04000EC9 RID: 3785
		private Font calendarFont;

		// Token: 0x04000ECA RID: 3786
		private Control.FontHandleWrapper calendarFontHandleWrapper;

		// Token: 0x04000ECB RID: 3787
		private string customFormat;

		// Token: 0x04000ECC RID: 3788
		private DateTimePickerFormat format;

		// Token: 0x04000ECD RID: 3789
		private bool rightToLeftLayout;

		// Token: 0x02000680 RID: 1664
		private sealed class EnumChildren
		{
			// Token: 0x060066EC RID: 26348 RVA: 0x00181014 File Offset: 0x0017F214
			public bool enumChildren(IntPtr hwnd, IntPtr lparam)
			{
				this.hwndFound = hwnd;
				return true;
			}

			// Token: 0x04003A7E RID: 14974
			public IntPtr hwndFound = IntPtr.Zero;
		}

		/// <summary>Provides information about the <see cref="T:System.Windows.Forms.DateTimePicker" /> control to accessibility client applications.</summary>
		// Token: 0x02000681 RID: 1665
		[ComVisible(true)]
		public class DateTimePickerAccessibleObject : Control.ControlAccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DateTimePicker.DateTimePickerAccessibleObject" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.DateTimePicker" /> that owns the <see cref="T:System.Windows.Forms.DateTimePicker.DateTimePickerAccessibleObject" />.</param>
			// Token: 0x060066EE RID: 26350 RVA: 0x0009B733 File Offset: 0x00099933
			public DateTimePickerAccessibleObject(DateTimePicker owner)
				: base(owner)
			{
			}

			/// <summary>Gets the shortcut key or access key for the accessible object.</summary>
			/// <returns>The shortcut key or access key for the accessible object.</returns>
			// Token: 0x1700166B RID: 5739
			// (get) Token: 0x060066EF RID: 26351 RVA: 0x00181034 File Offset: 0x0017F234
			public override string KeyboardShortcut
			{
				get
				{
					Label previousLabel = base.PreviousLabel;
					if (previousLabel != null)
					{
						char mnemonic = WindowsFormsUtils.GetMnemonic(previousLabel.Text, false);
						if (mnemonic != '\0')
						{
							return "Alt+" + mnemonic.ToString();
						}
					}
					string keyboardShortcut = base.KeyboardShortcut;
					if (keyboardShortcut == null || keyboardShortcut.Length == 0)
					{
						char mnemonic2 = WindowsFormsUtils.GetMnemonic(base.Owner.Text, false);
						if (mnemonic2 != '\0')
						{
							return "Alt+" + mnemonic2.ToString();
						}
					}
					return keyboardShortcut;
				}
			}

			/// <summary>Gets the value of an accessible object.</summary>
			/// <returns>The value of an accessible object, or <see langword="null" /> if the object has no value set.</returns>
			// Token: 0x1700166C RID: 5740
			// (get) Token: 0x060066F0 RID: 26352 RVA: 0x001810A8 File Offset: 0x0017F2A8
			public override string Value
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					string value = base.Value;
					if (value == null || value.Length == 0)
					{
						return base.Owner.Text;
					}
					return value;
				}
			}

			/// <summary>Gets the state of the accessible object.</summary>
			/// <returns>One of the <see cref="T:System.Windows.Forms.AccessibleStates" /> values indicating the state of the <see cref="T:System.Windows.Forms.DateTimePicker.DateTimePickerAccessibleObject" />.</returns>
			// Token: 0x1700166D RID: 5741
			// (get) Token: 0x060066F1 RID: 26353 RVA: 0x001810D4 File Offset: 0x0017F2D4
			public override AccessibleStates State
			{
				get
				{
					AccessibleStates accessibleStates = base.State;
					if (((DateTimePicker)base.Owner).ShowCheckBox && ((DateTimePicker)base.Owner).Checked)
					{
						accessibleStates |= AccessibleStates.Checked;
					}
					return accessibleStates;
				}
			}

			/// <summary>Gets the role of this accessible object.</summary>
			/// <returns>One of the <see cref="T:System.Windows.Forms.AccessibleRole" /> values indicating the role of the <see cref="T:System.Windows.Forms.DateTimePicker.DateTimePickerAccessibleObject" />.</returns>
			// Token: 0x1700166E RID: 5742
			// (get) Token: 0x060066F2 RID: 26354 RVA: 0x00181114 File Offset: 0x0017F314
			public override AccessibleRole Role
			{
				get
				{
					AccessibleRole accessibleRole = base.Owner.AccessibleRole;
					if (accessibleRole != AccessibleRole.Default)
					{
						return accessibleRole;
					}
					if (!AccessibilityImprovements.Level3)
					{
						return AccessibleRole.DropList;
					}
					return AccessibleRole.ComboBox;
				}
			}

			// Token: 0x060066F3 RID: 26355 RVA: 0x0009B73C File Offset: 0x0009993C
			internal override bool IsIAccessibleExSupported()
			{
				return AccessibilityImprovements.Level3 || base.IsIAccessibleExSupported();
			}

			// Token: 0x060066F4 RID: 26356 RVA: 0x00181140 File Offset: 0x0017F340
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30004)
				{
					return DateTimePicker.DateTimePickerLocalizedControlTypeString;
				}
				if (propertyID == 30028)
				{
					return this.IsPatternSupported(10005);
				}
				if (propertyID == 30041)
				{
					return this.IsPatternSupported(10015);
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x060066F5 RID: 26357 RVA: 0x00181194 File Offset: 0x0017F394
			internal override bool IsPatternSupported(int patternId)
			{
				if (patternId == 10005)
				{
					return AccessibilityImprovements.Level5;
				}
				if (patternId == 10015)
				{
					return ((DateTimePicker)base.Owner).ShowCheckBox;
				}
				return base.IsPatternSupported(patternId);
			}

			// Token: 0x1700166F RID: 5743
			// (get) Token: 0x060066F6 RID: 26358 RVA: 0x001811C4 File Offset: 0x0017F3C4
			internal override UnsafeNativeMethods.ToggleState ToggleState
			{
				get
				{
					if (!((DateTimePicker)base.Owner).Checked)
					{
						return UnsafeNativeMethods.ToggleState.ToggleState_Off;
					}
					return UnsafeNativeMethods.ToggleState.ToggleState_On;
				}
			}

			// Token: 0x060066F7 RID: 26359 RVA: 0x001811DB File Offset: 0x0017F3DB
			internal override void Toggle()
			{
				((DateTimePicker)base.Owner).Checked = !((DateTimePicker)base.Owner).Checked;
			}

			// Token: 0x060066F8 RID: 26360 RVA: 0x00181200 File Offset: 0x0017F400
			internal override void Collapse()
			{
				if (base.Owner.IsHandleCreated && this.ExpandCollapseState == UnsafeNativeMethods.ExpandCollapseState.Expanded)
				{
					base.Owner.SendMessage(4109, 0, 0);
				}
			}

			// Token: 0x060066F9 RID: 26361 RVA: 0x0018122B File Offset: 0x0017F42B
			internal override void Expand()
			{
				if (base.Owner.IsHandleCreated && this.ExpandCollapseState == UnsafeNativeMethods.ExpandCollapseState.Collapsed)
				{
					base.Owner.SendMessage(260, (IntPtr)40, 0);
				}
			}

			// Token: 0x17001670 RID: 5744
			// (get) Token: 0x060066FA RID: 26362 RVA: 0x0018125B File Offset: 0x0017F45B
			internal override UnsafeNativeMethods.ExpandCollapseState ExpandCollapseState
			{
				get
				{
					return ((DateTimePicker)base.Owner)._expandCollapseState;
				}
			}
		}
	}
}
