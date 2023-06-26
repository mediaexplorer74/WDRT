using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Forms.Internal;
using System.Windows.Forms.Layout;
using Microsoft.Win32;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows control that enables the user to select a date using a visual monthly calendar display.</summary>
	// Token: 0x020002FF RID: 767
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultProperty("SelectionRange")]
	[DefaultEvent("DateChanged")]
	[DefaultBindingProperty("SelectionRange")]
	[Designer("System.Windows.Forms.Design.MonthCalendarDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionMonthCalendar")]
	public class MonthCalendar : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MonthCalendar" /> class.</summary>
		// Token: 0x0600306E RID: 12398 RVA: 0x000DABEC File Offset: 0x000D8DEC
		public MonthCalendar()
		{
			this.PrepareForDrawing();
			this.selectionStart = this.todayDate;
			this.selectionEnd = this.todayDate;
			this._focusedDate = this.todayDate;
			base.SetStyle(ControlStyles.UserPaint, false);
			base.SetStyle(ControlStyles.StandardClick, false);
			base.TabStop = true;
			if (MonthCalendar.restrictUnmanagedCode == null)
			{
				bool flag = false;
				try
				{
					IntSecurity.UnmanagedCode.Demand();
					MonthCalendar.restrictUnmanagedCode = new bool?(false);
				}
				catch
				{
					flag = true;
				}
				if (flag)
				{
					new RegistryPermission(PermissionState.Unrestricted).Assert();
					try
					{
						RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\.NETFramework");
						if (registryKey != null)
						{
							object value = registryKey.GetValue("AllowWindowsFormsReentrantDestroy");
							if (value != null && value is int && (int)value == 1)
							{
								MonthCalendar.restrictUnmanagedCode = new bool?(false);
							}
							else
							{
								MonthCalendar.restrictUnmanagedCode = new bool?(true);
							}
						}
						else
						{
							MonthCalendar.restrictUnmanagedCode = new bool?(true);
						}
					}
					catch
					{
						MonthCalendar.restrictUnmanagedCode = new bool?(true);
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
			}
		}

		/// <summary>Creates a new accessibility object for the current <see cref="T:System.Windows.Forms.MonthCalendar" /> instance.</summary>
		/// <returns>A new accessibility object for the control.</returns>
		// Token: 0x0600306F RID: 12399 RVA: 0x000DADC8 File Offset: 0x000D8FC8
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level5)
			{
				return new MonthCalendar.MonthCalendarAccessibleObjectLevel5(this);
			}
			if (AccessibilityImprovements.Level1)
			{
				return new MonthCalendar.MonthCalendarAccessibleObject(this);
			}
			return base.CreateAccessibilityInstance();
		}

		/// <summary>Provides constants for rescaling the control when a DPI change occurs.</summary>
		/// <param name="deviceDpiOld">The DPI value prior to the change.</param>
		/// <param name="deviceDpiNew">The DPI value after the change.</param>
		// Token: 0x06003070 RID: 12400 RVA: 0x000DADEC File Offset: 0x000D8FEC
		protected override void RescaleConstantsForDpi(int deviceDpiOld, int deviceDpiNew)
		{
			base.RescaleConstantsForDpi(deviceDpiOld, deviceDpiNew);
			this.PrepareForDrawing();
		}

		// Token: 0x06003071 RID: 12401 RVA: 0x000DADFC File Offset: 0x000D8FFC
		private void PrepareForDrawing()
		{
			if (DpiHelper.EnableMonthCalendarHighDpiImprovements)
			{
				this.scaledExtraPadding = base.LogicalToDeviceUnits(2);
			}
		}

		/// <summary>Gets or sets the array of <see cref="T:System.DateTime" /> objects that determines which annual days are displayed in bold.</summary>
		/// <returns>An array of <see cref="T:System.DateTime" /> objects.</returns>
		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x06003072 RID: 12402 RVA: 0x000DAE14 File Offset: 0x000D9014
		// (set) Token: 0x06003073 RID: 12403 RVA: 0x000DAE64 File Offset: 0x000D9064
		[Localizable(true)]
		[SRDescription("MonthCalendarAnnuallyBoldedDatesDescr")]
		public DateTime[] AnnuallyBoldedDates
		{
			get
			{
				DateTime[] array = new DateTime[this.annualArrayOfDates.Count];
				for (int i = 0; i < this.annualArrayOfDates.Count; i++)
				{
					array[i] = (DateTime)this.annualArrayOfDates[i];
				}
				return array;
			}
			set
			{
				this.annualArrayOfDates.Clear();
				for (int i = 0; i < 12; i++)
				{
					this.monthsOfYear[i] = 0;
				}
				if (value != null && value.Length != 0)
				{
					for (int j = 0; j < value.Length; j++)
					{
						this.annualArrayOfDates.Add(value[j]);
					}
					for (int k = 0; k < value.Length; k++)
					{
						this.monthsOfYear[value[k].Month - 1] |= 1 << value[k].Day - 1;
					}
				}
				base.RecreateHandle();
			}
		}

		/// <summary>Gets or sets the background color for the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.</returns>
		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x06003074 RID: 12404 RVA: 0x00027DA7 File Offset: 0x00025FA7
		// (set) Token: 0x06003075 RID: 12405 RVA: 0x00012D84 File Offset: 0x00010F84
		[SRDescription("MonthCalendarMonthBackColorDescr")]
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

		/// <summary>Gets or sets the background image for the <see cref="T:System.Windows.Forms.MonthCalendar" /></summary>
		/// <returns>The <see cref="T:System.Drawing.Image" /> that is the background image for the <see cref="T:System.Windows.Forms.MonthCalendar" /> control.</returns>
		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x06003076 RID: 12406 RVA: 0x0001187C File Offset: 0x0000FA7C
		// (set) Token: 0x06003077 RID: 12407 RVA: 0x00011884 File Offset: 0x0000FA84
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.MonthCalendar.BackgroundImage" /> property changes.</summary>
		// Token: 0x14000230 RID: 560
		// (add) Token: 0x06003078 RID: 12408 RVA: 0x0001188D File Offset: 0x0000FA8D
		// (remove) Token: 0x06003079 RID: 12409 RVA: 0x00011896 File Offset: 0x0000FA96
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

		/// <summary>Gets or sets a value indicating the layout for the <see cref="P:System.Windows.Forms.MonthCalendar.BackgroundImage" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImageLayout" /> values.</returns>
		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x0600307A RID: 12410 RVA: 0x0001189F File Offset: 0x0000FA9F
		// (set) Token: 0x0600307B RID: 12411 RVA: 0x000118A7 File Offset: 0x0000FAA7
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.MonthCalendar.BackgroundImageLayout" /> property changes.</summary>
		// Token: 0x14000231 RID: 561
		// (add) Token: 0x0600307C RID: 12412 RVA: 0x000118B0 File Offset: 0x0000FAB0
		// (remove) Token: 0x0600307D RID: 12413 RVA: 0x000118B9 File Offset: 0x0000FAB9
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

		/// <summary>Gets or sets the array of <see cref="T:System.DateTime" /> objects that determines which nonrecurring dates are displayed in bold.</summary>
		/// <returns>The array of bold dates.</returns>
		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x0600307E RID: 12414 RVA: 0x000DAF04 File Offset: 0x000D9104
		// (set) Token: 0x0600307F RID: 12415 RVA: 0x000DAF54 File Offset: 0x000D9154
		[Localizable(true)]
		public DateTime[] BoldedDates
		{
			get
			{
				DateTime[] array = new DateTime[this.arrayOfDates.Count];
				for (int i = 0; i < this.arrayOfDates.Count; i++)
				{
					array[i] = (DateTime)this.arrayOfDates[i];
				}
				return array;
			}
			set
			{
				this.arrayOfDates.Clear();
				if (value != null && value.Length != 0)
				{
					for (int i = 0; i < value.Length; i++)
					{
						this.arrayOfDates.Add(value[i]);
					}
				}
				base.RecreateHandle();
			}
		}

		/// <summary>Gets or sets the number of columns and rows of months displayed.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> with the number of columns and rows to use to display the calendar.</returns>
		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x06003080 RID: 12416 RVA: 0x000DAF9F File Offset: 0x000D919F
		// (set) Token: 0x06003081 RID: 12417 RVA: 0x000DAFA7 File Offset: 0x000D91A7
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[SRDescription("MonthCalendarDimensionsDescr")]
		public Size CalendarDimensions
		{
			get
			{
				return this.dimensions;
			}
			set
			{
				if (!this.dimensions.Equals(value))
				{
					this.SetCalendarDimensions(value.Width, value.Height);
				}
			}
		}

		/// <summary>Gets a <see cref="T:System.Windows.Forms.CreateParams" /> for creating a <see cref="T:System.Windows.Forms.MonthCalendar" /> control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> with the information for creating a <see cref="T:System.Windows.Forms.MonthCalendar" /> control.</returns>
		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x06003082 RID: 12418 RVA: 0x000DAFD8 File Offset: 0x000D91D8
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ClassName = "SysMonthCal32";
				createParams.Style |= 3;
				if (!this.showToday)
				{
					createParams.Style |= 16;
				}
				if (!this.showTodayCircle)
				{
					createParams.Style |= 8;
				}
				if (this.showWeekNumbers)
				{
					createParams.Style |= 4;
				}
				if (this.RightToLeft == RightToLeft.Yes && this.RightToLeftLayout)
				{
					createParams.ExStyle |= 4194304;
					createParams.ExStyle &= -28673;
				}
				return createParams;
			}
		}

		/// <summary>Gets a value indicating the input method editor for the <see cref="T:System.Windows.Forms.MonthCalendar" />.</summary>
		/// <returns>As implemented for this object, always <see cref="F:System.Windows.Forms.ImeMode.Disable" />.</returns>
		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x06003083 RID: 12419 RVA: 0x00023BD7 File Offset: 0x00021DD7
		protected override ImeMode DefaultImeMode
		{
			get
			{
				return ImeMode.Disable;
			}
		}

		/// <summary>Gets the default margin settings for the <see cref="T:System.Windows.Forms.MonthCalendar" /> control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> structure with a padding size of 9 pixels, for all of its edges.</returns>
		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x06003084 RID: 12420 RVA: 0x000DB07E File Offset: 0x000D927E
		protected override Padding DefaultMargin
		{
			get
			{
				return new Padding(9);
			}
		}

		/// <summary>Gets the default size of the calendar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> specifying the height and width, in pixels, of the control.</returns>
		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x06003085 RID: 12421 RVA: 0x000DB087 File Offset: 0x000D9287
		protected override Size DefaultSize
		{
			get
			{
				return this.GetMinReqRect();
			}
		}

		/// <summary>Gets or sets a value indicating whether the control should redraw its surface using a secondary buffer.</summary>
		/// <returns>
		///   <see langword="true" /> if the control should use a secondary buffer to redraw; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x06003086 RID: 12422 RVA: 0x00012FC3 File Offset: 0x000111C3
		// (set) Token: 0x06003087 RID: 12423 RVA: 0x00012FCB File Offset: 0x000111CB
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

		/// <summary>Gets or sets the first day of the week as displayed in the month calendar.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.Day" /> values. The default is <see cref="F:System.Windows.Forms.Day.Default" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.Day" /> enumeration members.</exception>
		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x06003088 RID: 12424 RVA: 0x000DB08F File Offset: 0x000D928F
		// (set) Token: 0x06003089 RID: 12425 RVA: 0x000DB098 File Offset: 0x000D9298
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[DefaultValue(Day.Default)]
		[SRDescription("MonthCalendarFirstDayOfWeekDescr")]
		public Day FirstDayOfWeek
		{
			get
			{
				return this.firstDayOfWeek;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 7))
				{
					throw new InvalidEnumArgumentException("FirstDayOfWeek", (int)value, typeof(Day));
				}
				if (value != this.firstDayOfWeek)
				{
					this.firstDayOfWeek = value;
					if (base.IsHandleCreated)
					{
						if (value == Day.Default)
						{
							base.RecreateHandle();
						}
						else
						{
							base.SendMessage(4111, 0, (int)value);
						}
						if (AccessibilityImprovements.Level5)
						{
							this.UpdateDisplayRange();
							this.OnDisplayRangeChanged(EventArgs.Empty);
						}
					}
				}
			}
		}

		/// <summary>Gets or sets the foreground color of the control.</summary>
		/// <returns>The foreground <see cref="T:System.Drawing.Color" /> of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultForeColor" /> property.</returns>
		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x0600308A RID: 12426 RVA: 0x0001300E File Offset: 0x0001120E
		// (set) Token: 0x0600308B RID: 12427 RVA: 0x00013024 File Offset: 0x00011224
		[SRDescription("MonthCalendarForeColorDescr")]
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

		/// <summary>Gets or sets the Input Method Editor (IME) mode supported by this control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x0600308C RID: 12428 RVA: 0x0001A051 File Offset: 0x00018251
		// (set) Token: 0x0600308D RID: 12429 RVA: 0x0001A059 File Offset: 0x00018259
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new ImeMode ImeMode
		{
			get
			{
				return base.ImeMode;
			}
			set
			{
				base.ImeMode = value;
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.MonthCalendar.ImeMode" /> property has changed.</summary>
		// Token: 0x14000232 RID: 562
		// (add) Token: 0x0600308E RID: 12430 RVA: 0x00023F70 File Offset: 0x00022170
		// (remove) Token: 0x0600308F RID: 12431 RVA: 0x00023F79 File Offset: 0x00022179
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ImeModeChanged
		{
			add
			{
				base.ImeModeChanged += value;
			}
			remove
			{
				base.ImeModeChanged -= value;
			}
		}

		/// <summary>Gets or sets the maximum allowable date.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> representing the maximum allowable date. The default is 12/31/9998.</returns>
		/// <exception cref="T:System.ArgumentException">The value is less than the <see cref="P:System.Windows.Forms.MonthCalendar.MinDate" />.</exception>
		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x06003090 RID: 12432 RVA: 0x000DB115 File Offset: 0x000D9315
		// (set) Token: 0x06003091 RID: 12433 RVA: 0x000DB124 File Offset: 0x000D9324
		[SRCategory("CatBehavior")]
		[SRDescription("MonthCalendarMaxDateDescr")]
		public DateTime MaxDate
		{
			get
			{
				return DateTimePicker.EffectiveMaxDate(this.maxDate);
			}
			set
			{
				if (value != this.maxDate)
				{
					if (value < DateTimePicker.EffectiveMinDate(this.minDate))
					{
						throw new ArgumentOutOfRangeException("MaxDate", SR.GetString("InvalidLowBoundArgumentEx", new object[]
						{
							"MaxDate",
							MonthCalendar.FormatDate(value),
							"MinDate"
						}));
					}
					this.maxDate = value;
					this.SetRange();
				}
			}
		}

		/// <summary>Gets or sets the maximum number of days that can be selected in a month calendar control.</summary>
		/// <returns>The maximum number of days that you can select. The default is 7.</returns>
		/// <exception cref="T:System.ArgumentException">The value is less than 1.  
		///  -or-  
		///  The <see cref="P:System.Windows.Forms.MonthCalendar.MaxSelectionCount" /> cannot be set.</exception>
		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x06003092 RID: 12434 RVA: 0x000DB193 File Offset: 0x000D9393
		// (set) Token: 0x06003093 RID: 12435 RVA: 0x000DB19C File Offset: 0x000D939C
		[SRCategory("CatBehavior")]
		[DefaultValue(7)]
		[SRDescription("MonthCalendarMaxSelectionCountDescr")]
		public int MaxSelectionCount
		{
			get
			{
				return this.maxSelectionCount;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("MaxSelectionCount", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"MaxSelectionCount",
						value.ToString("D", CultureInfo.CurrentCulture),
						1.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (value != this.maxSelectionCount)
				{
					if (base.IsHandleCreated && (int)(long)base.SendMessage(4100, value, 0) == 0)
					{
						throw new ArgumentException(SR.GetString("MonthCalendarMaxSelCount", new object[] { value.ToString("D", CultureInfo.CurrentCulture) }), "MaxSelectionCount");
					}
					this.maxSelectionCount = value;
				}
			}
		}

		/// <summary>Gets or sets the minimum allowable date.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> representing the minimum allowable date. The default is 01/01/1753.</returns>
		/// <exception cref="T:System.ArgumentException">The date set is greater than the <see cref="P:System.Windows.Forms.MonthCalendar.MaxDate" />.  
		///  -or-  
		///  The date set is earlier than 01/01/1753.</exception>
		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x06003094 RID: 12436 RVA: 0x000DB251 File Offset: 0x000D9451
		// (set) Token: 0x06003095 RID: 12437 RVA: 0x000DB260 File Offset: 0x000D9460
		[SRCategory("CatBehavior")]
		[SRDescription("MonthCalendarMinDateDescr")]
		public DateTime MinDate
		{
			get
			{
				return DateTimePicker.EffectiveMinDate(this.minDate);
			}
			set
			{
				if (value != this.minDate)
				{
					if (value > DateTimePicker.EffectiveMaxDate(this.maxDate))
					{
						throw new ArgumentOutOfRangeException("MinDate", SR.GetString("InvalidHighBoundArgument", new object[]
						{
							"MinDate",
							MonthCalendar.FormatDate(value),
							"MaxDate"
						}));
					}
					if (value < DateTimePicker.MinimumDateTime)
					{
						throw new ArgumentOutOfRangeException("MinDate", SR.GetString("InvalidLowBoundArgumentEx", new object[]
						{
							"MinDate",
							MonthCalendar.FormatDate(value),
							MonthCalendar.FormatDate(DateTimePicker.MinimumDateTime)
						}));
					}
					this.minDate = value;
					this.SetRange();
				}
			}
		}

		/// <summary>Gets or sets the array of <see cref="T:System.DateTime" /> objects that determine which monthly days to bold.</summary>
		/// <returns>An array of <see cref="T:System.DateTime" /> objects.</returns>
		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x06003096 RID: 12438 RVA: 0x000DB318 File Offset: 0x000D9518
		// (set) Token: 0x06003097 RID: 12439 RVA: 0x000DB368 File Offset: 0x000D9568
		[Localizable(true)]
		[SRDescription("MonthCalendarMonthlyBoldedDatesDescr")]
		public DateTime[] MonthlyBoldedDates
		{
			get
			{
				DateTime[] array = new DateTime[this.monthlyArrayOfDates.Count];
				for (int i = 0; i < this.monthlyArrayOfDates.Count; i++)
				{
					array[i] = (DateTime)this.monthlyArrayOfDates[i];
				}
				return array;
			}
			set
			{
				this.monthlyArrayOfDates.Clear();
				this.datesToBoldMonthly = 0;
				if (value != null && value.Length != 0)
				{
					for (int i = 0; i < value.Length; i++)
					{
						this.monthlyArrayOfDates.Add(value[i]);
					}
					for (int j = 0; j < value.Length; j++)
					{
						this.datesToBoldMonthly |= 1 << value[j].Day - 1;
					}
				}
				base.RecreateHandle();
			}
		}

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x06003098 RID: 12440 RVA: 0x000DB3E8 File Offset: 0x000D95E8
		private DateTime Now
		{
			get
			{
				return DateTime.Now.Date;
			}
		}

		/// <summary>Gets or sets the space between the edges of a <see cref="T:System.Windows.Forms.MonthCalendar" /> control and its contents.</summary>
		/// <returns>
		///   <see cref="F:System.Windows.Forms.Padding.Empty" /> in all cases.</returns>
		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x06003099 RID: 12441 RVA: 0x00013442 File Offset: 0x00011642
		// (set) Token: 0x0600309A RID: 12442 RVA: 0x0001344A File Offset: 0x0001164A
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.MonthCalendar.Padding" /> property changes.</summary>
		// Token: 0x14000233 RID: 563
		// (add) Token: 0x0600309B RID: 12443 RVA: 0x00013453 File Offset: 0x00011653
		// (remove) Token: 0x0600309C RID: 12444 RVA: 0x0001345C File Offset: 0x0001165C
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

		/// <summary>Gets or sets a value indicating whether the control is laid out from right to left.</summary>
		/// <returns>
		///   <see langword="true" /> if the control is laid out from right to left; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x0600309D RID: 12445 RVA: 0x000DB402 File Offset: 0x000D9602
		// (set) Token: 0x0600309E RID: 12446 RVA: 0x000DB40C File Offset: 0x000D960C
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

		/// <summary>Gets or sets the scroll rate for a month calendar control.</summary>
		/// <returns>A positive number representing the current scroll rate in number of months moved. The default is the number of months currently displayed. The maximum is 20,000.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than 0.  
		///  -or-  
		///  The value is greater than 20,000.</exception>
		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x0600309F RID: 12447 RVA: 0x000DB460 File Offset: 0x000D9660
		// (set) Token: 0x060030A0 RID: 12448 RVA: 0x000DB468 File Offset: 0x000D9668
		[SRCategory("CatBehavior")]
		[DefaultValue(0)]
		[SRDescription("MonthCalendarScrollChangeDescr")]
		public int ScrollChange
		{
			get
			{
				return this.scrollChange;
			}
			set
			{
				if (this.scrollChange != value)
				{
					if (value < 0)
					{
						throw new ArgumentOutOfRangeException("ScrollChange", SR.GetString("InvalidLowBoundArgumentEx", new object[]
						{
							"ScrollChange",
							value.ToString("D", CultureInfo.CurrentCulture),
							0.ToString(CultureInfo.CurrentCulture)
						}));
					}
					if (value > 20000)
					{
						throw new ArgumentOutOfRangeException("ScrollChange", SR.GetString("InvalidHighBoundArgumentEx", new object[]
						{
							"ScrollChange",
							value.ToString("D", CultureInfo.CurrentCulture),
							20000.ToString("D", CultureInfo.CurrentCulture)
						}));
					}
					if (base.IsHandleCreated)
					{
						base.SendMessage(4116, value, 0);
					}
					this.scrollChange = value;
				}
			}
		}

		/// <summary>Gets or sets the end date of the selected range of dates.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> indicating the last date in the selection range.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The date value is less than the <see cref="P:System.Windows.Forms.MonthCalendar.MinDate" /> value.  
		///  -or-  
		///  The date value is greater than the <see cref="P:System.Windows.Forms.MonthCalendar.MaxDate" /> value.</exception>
		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x060030A1 RID: 12449 RVA: 0x000DB543 File Offset: 0x000D9743
		// (set) Token: 0x060030A2 RID: 12450 RVA: 0x000DB54C File Offset: 0x000D974C
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("MonthCalendarSelectionEndDescr")]
		public DateTime SelectionEnd
		{
			get
			{
				return this.selectionEnd;
			}
			set
			{
				if (this.selectionEnd != value)
				{
					if (value < this.MinDate)
					{
						throw new ArgumentOutOfRangeException("SelectionEnd", SR.GetString("InvalidLowBoundArgumentEx", new object[]
						{
							"SelectionEnd",
							MonthCalendar.FormatDate(value),
							"MinDate"
						}));
					}
					if (value > this.MaxDate)
					{
						throw new ArgumentOutOfRangeException("SelectionEnd", SR.GetString("InvalidHighBoundArgumentEx", new object[]
						{
							"SelectionEnd",
							MonthCalendar.FormatDate(value),
							"MaxDate"
						}));
					}
					if (this.selectionStart > value)
					{
						this.selectionStart = value;
					}
					if ((value - this.selectionStart).Days >= this.maxSelectionCount)
					{
						this.selectionStart = value.AddDays((double)(1 - this.maxSelectionCount));
					}
					this.SetSelRange(this.selectionStart, value);
				}
			}
		}

		/// <summary>Gets or sets the start date of the selected range of dates.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> indicating the first date in the selection range.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The date value is less than <see cref="P:System.Windows.Forms.MonthCalendar.MinDate" />.  
		///  -or-  
		///  The date value is greater than <see cref="P:System.Windows.Forms.MonthCalendar.MaxDate" />.</exception>
		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x060030A3 RID: 12451 RVA: 0x000DB642 File Offset: 0x000D9842
		// (set) Token: 0x060030A4 RID: 12452 RVA: 0x000DB64C File Offset: 0x000D984C
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("MonthCalendarSelectionStartDescr")]
		public DateTime SelectionStart
		{
			get
			{
				return this.selectionStart;
			}
			set
			{
				if (this.selectionStart != value)
				{
					if (value < this.minDate)
					{
						throw new ArgumentOutOfRangeException("SelectionStart", SR.GetString("InvalidLowBoundArgumentEx", new object[]
						{
							"SelectionStart",
							MonthCalendar.FormatDate(value),
							"MinDate"
						}));
					}
					if (value > this.maxDate)
					{
						throw new ArgumentOutOfRangeException("SelectionStart", SR.GetString("InvalidHighBoundArgumentEx", new object[]
						{
							"SelectionStart",
							MonthCalendar.FormatDate(value),
							"MaxDate"
						}));
					}
					if (this.selectionEnd < value)
					{
						this.selectionEnd = value;
					}
					if ((this.selectionEnd - value).Days >= this.maxSelectionCount)
					{
						this.selectionEnd = value.AddDays((double)(this.maxSelectionCount - 1));
					}
					this.SetSelRange(value, this.selectionEnd);
				}
			}
		}

		/// <summary>Gets or sets the selected range of dates for a month calendar control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.SelectionRange" /> with the start and end dates of the selected range.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Windows.Forms.SelectionRange.Start" /> value of the assigned <see cref="T:System.Windows.Forms.SelectionRange" /> is less than the minimum date allowable for a month calendar control.  
		///  -or-  
		///  The <see cref="P:System.Windows.Forms.SelectionRange.Start" /> value of the assigned <see cref="T:System.Windows.Forms.SelectionRange" /> is greater than the maximum allowable date for a month calendar control.  
		///  -or-  
		///  The <see cref="P:System.Windows.Forms.SelectionRange.End" /> value of the assigned <see cref="T:System.Windows.Forms.SelectionRange" /> is less than the minimum date allowable for a month calendar control.  
		///  -or-  
		///  The <see cref="P:System.Windows.Forms.SelectionRange.End" /> value of the assigned <see cref="T:System.Windows.Forms.SelectionRange" /> is greater than the maximum allowable date for a month calendar control.</exception>
		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x060030A5 RID: 12453 RVA: 0x000DB742 File Offset: 0x000D9942
		// (set) Token: 0x060030A6 RID: 12454 RVA: 0x000DB755 File Offset: 0x000D9955
		[SRCategory("CatBehavior")]
		[SRDescription("MonthCalendarSelectionRangeDescr")]
		[Bindable(true)]
		public SelectionRange SelectionRange
		{
			get
			{
				return new SelectionRange(this.SelectionStart, this.SelectionEnd);
			}
			set
			{
				this.SetSelectionRange(value.Start, value.End);
			}
		}

		/// <summary>Gets or sets a value indicating whether the date represented by the <see cref="P:System.Windows.Forms.MonthCalendar.TodayDate" /> property is displayed at the bottom of the control.</summary>
		/// <returns>
		///   <see langword="true" /> if today's date is displayed; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x060030A7 RID: 12455 RVA: 0x000DB769 File Offset: 0x000D9969
		// (set) Token: 0x060030A8 RID: 12456 RVA: 0x000DB771 File Offset: 0x000D9971
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("MonthCalendarShowTodayDescr")]
		public bool ShowToday
		{
			get
			{
				return this.showToday;
			}
			set
			{
				if (this.showToday != value)
				{
					this.showToday = value;
					base.UpdateStyles();
					this.AdjustSize();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether today's date is identified with a circle or a square.</summary>
		/// <returns>
		///   <see langword="true" /> if today's date is identified with a circle or a square; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x060030A9 RID: 12457 RVA: 0x000DB78F File Offset: 0x000D998F
		// (set) Token: 0x060030AA RID: 12458 RVA: 0x000DB797 File Offset: 0x000D9997
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("MonthCalendarShowTodayCircleDescr")]
		public bool ShowTodayCircle
		{
			get
			{
				return this.showTodayCircle;
			}
			set
			{
				if (this.showTodayCircle != value)
				{
					this.showTodayCircle = value;
					base.UpdateStyles();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the month calendar control displays week numbers (1-52) to the left of each row of days.</summary>
		/// <returns>
		///   <see langword="true" /> if the week numbers are displayed; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x060030AB RID: 12459 RVA: 0x000DB7AF File Offset: 0x000D99AF
		// (set) Token: 0x060030AC RID: 12460 RVA: 0x000DB7B7 File Offset: 0x000D99B7
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[DefaultValue(false)]
		[SRDescription("MonthCalendarShowWeekNumbersDescr")]
		public bool ShowWeekNumbers
		{
			get
			{
				return this.showWeekNumbers;
			}
			set
			{
				if (this.showWeekNumbers != value)
				{
					this.showWeekNumbers = value;
					base.UpdateStyles();
					this.AdjustSize();
				}
			}
		}

		/// <summary>Gets the minimum size to display one month of the calendar.</summary>
		/// <returns>The size, in pixels, necessary to fully display one month in the calendar.</returns>
		/// <exception cref="T:System.InvalidOperationException">The dimensions cannot be retrieved.</exception>
		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x060030AD RID: 12461 RVA: 0x000DB7D8 File Offset: 0x000D99D8
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("MonthCalendarSingleMonthSizeDescr")]
		public Size SingleMonthSize
		{
			get
			{
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				if (!base.IsHandleCreated)
				{
					return MonthCalendar.DefaultSingleMonthSize;
				}
				if ((int)(long)base.SendMessage(4105, 0, ref rect) == 0)
				{
					throw new InvalidOperationException(SR.GetString("InvalidSingleMonthSize"));
				}
				return new Size(rect.right, rect.bottom);
			}
		}

		/// <summary>Gets or sets the size of the <see cref="T:System.Windows.Forms.MonthCalendar" /> control.</summary>
		/// <returns>The <see cref="T:System.Drawing.Size" /> of the <see cref="T:System.Windows.Forms.MonthCalendar" /> control.</returns>
		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x060030AE RID: 12462 RVA: 0x000B22AF File Offset: 0x000B04AF
		// (set) Token: 0x060030AF RID: 12463 RVA: 0x000B22B7 File Offset: 0x000B04B7
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Localizable(false)]
		public new Size Size
		{
			get
			{
				return base.Size;
			}
			set
			{
				base.Size = value;
			}
		}

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x060030B0 RID: 12464 RVA: 0x000DB832 File Offset: 0x000D9A32
		internal override bool SupportsUiaProviders
		{
			get
			{
				return AccessibilityImprovements.Level5 && !base.DesignMode;
			}
		}

		/// <summary>Gets or sets the text to display on the <see cref="T:System.Windows.Forms.MonthCalendar" />.</summary>
		/// <returns>
		///   <see langword="Null" />.</returns>
		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x060030B1 RID: 12465 RVA: 0x00013814 File Offset: 0x00011A14
		// (set) Token: 0x060030B2 RID: 12466 RVA: 0x00023FE9 File Offset: 0x000221E9
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Bindable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.MonthCalendar.Text" /> property changes.</summary>
		// Token: 0x14000234 RID: 564
		// (add) Token: 0x060030B3 RID: 12467 RVA: 0x00046591 File Offset: 0x00044791
		// (remove) Token: 0x060030B4 RID: 12468 RVA: 0x0004659A File Offset: 0x0004479A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		/// <summary>Gets or sets the value that is used by <see cref="T:System.Windows.Forms.MonthCalendar" /> as today's date.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> representing today's date. The default value is the current system date.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than the minimum allowable date.  
		///  -or-  
		///  The value is greater than the maximum allowable date.</exception>
		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x060030B5 RID: 12469 RVA: 0x000DB848 File Offset: 0x000D9A48
		// (set) Token: 0x060030B6 RID: 12470 RVA: 0x000DB8B0 File Offset: 0x000D9AB0
		[SRCategory("CatBehavior")]
		[SRDescription("MonthCalendarTodayDateDescr")]
		public DateTime TodayDate
		{
			get
			{
				if (this.todayDateSet)
				{
					return this.todayDate;
				}
				if (base.IsHandleCreated)
				{
					NativeMethods.SYSTEMTIME systemtime = new NativeMethods.SYSTEMTIME();
					int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4109, 0, systemtime);
					return DateTimePicker.SysTimeToDateTime(systemtime).Date;
				}
				return this.Now.Date;
			}
			set
			{
				if (!this.todayDateSet || DateTime.Compare(value, this.todayDate) != 0)
				{
					if (DateTime.Compare(value, this.maxDate) > 0)
					{
						throw new ArgumentOutOfRangeException("TodayDate", SR.GetString("InvalidHighBoundArgumentEx", new object[]
						{
							"TodayDate",
							MonthCalendar.FormatDate(value),
							MonthCalendar.FormatDate(this.maxDate)
						}));
					}
					if (DateTime.Compare(value, this.minDate) < 0)
					{
						throw new ArgumentOutOfRangeException("TodayDate", SR.GetString("InvalidLowBoundArgument", new object[]
						{
							"TodayDate",
							MonthCalendar.FormatDate(value),
							MonthCalendar.FormatDate(this.minDate)
						}));
					}
					this.todayDate = value.Date;
					this.todayDateSet = true;
					this.UpdateTodayDate();
				}
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="P:System.Windows.Forms.MonthCalendar.TodayDate" /> property has been explicitly set.</summary>
		/// <returns>
		///   <see langword="true" /> if the value for the <see cref="P:System.Windows.Forms.MonthCalendar.TodayDate" /> property has been explicitly set; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x060030B7 RID: 12471 RVA: 0x000DB982 File Offset: 0x000D9B82
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("MonthCalendarTodayDateSetDescr")]
		public bool TodayDateSet
		{
			get
			{
				return this.todayDateSet;
			}
		}

		/// <summary>Gets or sets a value indicating the background color of the title area of the calendar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" />. The default is the system color for active captions.</returns>
		/// <exception cref="T:System.ArgumentException">The value is not a valid <see cref="T:System.Drawing.Color" />.</exception>
		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x060030B8 RID: 12472 RVA: 0x000DB98A File Offset: 0x000D9B8A
		// (set) Token: 0x060030B9 RID: 12473 RVA: 0x000DB992 File Offset: 0x000D9B92
		[SRCategory("CatAppearance")]
		[SRDescription("MonthCalendarTitleBackColorDescr")]
		public Color TitleBackColor
		{
			get
			{
				return this.titleBackColor;
			}
			set
			{
				if (value.IsEmpty)
				{
					throw new ArgumentException(SR.GetString("InvalidNullArgument", new object[] { "value" }));
				}
				this.titleBackColor = value;
				this.SetControlColor(2, value);
			}
		}

		/// <summary>Gets or sets a value indicating the foreground color of the title area of the calendar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" />. The default is the system color for active caption text.</returns>
		/// <exception cref="T:System.ArgumentException">The value is not a valid <see cref="T:System.Drawing.Color" />.</exception>
		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x060030BA RID: 12474 RVA: 0x000DB9CA File Offset: 0x000D9BCA
		// (set) Token: 0x060030BB RID: 12475 RVA: 0x000DB9D2 File Offset: 0x000D9BD2
		[SRCategory("CatAppearance")]
		[SRDescription("MonthCalendarTitleForeColorDescr")]
		public Color TitleForeColor
		{
			get
			{
				return this.titleForeColor;
			}
			set
			{
				if (value.IsEmpty)
				{
					throw new ArgumentException(SR.GetString("InvalidNullArgument", new object[] { "value" }));
				}
				this.titleForeColor = value;
				this.SetControlColor(3, value);
			}
		}

		/// <summary>Gets or sets a value indicating the color of days in months that are not fully displayed in the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" />. The default is <see cref="P:System.Drawing.Color.Gray" />.</returns>
		/// <exception cref="T:System.ArgumentException">The value is not a valid <see cref="T:System.Drawing.Color" />.</exception>
		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x060030BC RID: 12476 RVA: 0x000DBA0A File Offset: 0x000D9C0A
		// (set) Token: 0x060030BD RID: 12477 RVA: 0x000DBA12 File Offset: 0x000D9C12
		[SRCategory("CatAppearance")]
		[SRDescription("MonthCalendarTrailingForeColorDescr")]
		public Color TrailingForeColor
		{
			get
			{
				return this.trailingForeColor;
			}
			set
			{
				if (value.IsEmpty)
				{
					throw new ArgumentException(SR.GetString("InvalidNullArgument", new object[] { "value" }));
				}
				this.trailingForeColor = value;
				this.SetControlColor(5, value);
			}
		}

		/// <summary>Adds a day that is displayed in bold on an annual basis in the month calendar.</summary>
		/// <param name="date">The date to be displayed in bold.</param>
		// Token: 0x060030BE RID: 12478 RVA: 0x000DBA4A File Offset: 0x000D9C4A
		public void AddAnnuallyBoldedDate(DateTime date)
		{
			this.annualArrayOfDates.Add(date);
			this.monthsOfYear[date.Month - 1] |= 1 << date.Day - 1;
		}

		/// <summary>Adds a day to be displayed in bold in the month calendar.</summary>
		/// <param name="date">The date to be displayed in bold.</param>
		// Token: 0x060030BF RID: 12479 RVA: 0x000DBA84 File Offset: 0x000D9C84
		public void AddBoldedDate(DateTime date)
		{
			if (!this.arrayOfDates.Contains(date))
			{
				this.arrayOfDates.Add(date);
			}
		}

		/// <summary>Adds a day that is displayed in bold on a monthly basis in the month calendar.</summary>
		/// <param name="date">The date to be displayed in bold.</param>
		// Token: 0x060030C0 RID: 12480 RVA: 0x000DBAAB File Offset: 0x000D9CAB
		public void AddMonthlyBoldedDate(DateTime date)
		{
			this.monthlyArrayOfDates.Add(date);
			this.datesToBoldMonthly |= 1 << date.Day - 1;
		}

		// Token: 0x14000235 RID: 565
		// (add) Token: 0x060030C1 RID: 12481 RVA: 0x000DBADA File Offset: 0x000D9CDA
		// (remove) Token: 0x060030C2 RID: 12482 RVA: 0x000DBAF3 File Offset: 0x000D9CF3
		private event EventHandler CalendarViewChanged
		{
			add
			{
				this._onCalendarViewChanged = (EventHandler)Delegate.Combine(this._onCalendarViewChanged, value);
			}
			remove
			{
				this._onCalendarViewChanged = (EventHandler)Delegate.Remove(this._onCalendarViewChanged, value);
			}
		}

		/// <summary>Occurs when the user clicks the <see cref="T:System.Windows.Forms.MonthCalendar" /> control.</summary>
		// Token: 0x14000236 RID: 566
		// (add) Token: 0x060030C3 RID: 12483 RVA: 0x00012FD4 File Offset: 0x000111D4
		// (remove) Token: 0x060030C4 RID: 12484 RVA: 0x00012FDD File Offset: 0x000111DD
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

		/// <summary>Occurs when the date selected in the <see cref="T:System.Windows.Forms.MonthCalendar" /> changes.</summary>
		// Token: 0x14000237 RID: 567
		// (add) Token: 0x060030C5 RID: 12485 RVA: 0x000DBB0C File Offset: 0x000D9D0C
		// (remove) Token: 0x060030C6 RID: 12486 RVA: 0x000DBB25 File Offset: 0x000D9D25
		[SRCategory("CatAction")]
		[SRDescription("MonthCalendarOnDateChangedDescr")]
		public event DateRangeEventHandler DateChanged
		{
			add
			{
				this.onDateChanged = (DateRangeEventHandler)Delegate.Combine(this.onDateChanged, value);
			}
			remove
			{
				this.onDateChanged = (DateRangeEventHandler)Delegate.Remove(this.onDateChanged, value);
			}
		}

		/// <summary>Occurs when the user makes an explicit date selection using the mouse.</summary>
		// Token: 0x14000238 RID: 568
		// (add) Token: 0x060030C7 RID: 12487 RVA: 0x000DBB3E File Offset: 0x000D9D3E
		// (remove) Token: 0x060030C8 RID: 12488 RVA: 0x000DBB57 File Offset: 0x000D9D57
		[SRCategory("CatAction")]
		[SRDescription("MonthCalendarOnDateSelectedDescr")]
		public event DateRangeEventHandler DateSelected
		{
			add
			{
				this.onDateSelected = (DateRangeEventHandler)Delegate.Combine(this.onDateSelected, value);
			}
			remove
			{
				this.onDateSelected = (DateRangeEventHandler)Delegate.Remove(this.onDateSelected, value);
			}
		}

		// Token: 0x14000239 RID: 569
		// (add) Token: 0x060030C9 RID: 12489 RVA: 0x000DBB70 File Offset: 0x000D9D70
		// (remove) Token: 0x060030CA RID: 12490 RVA: 0x000DBB89 File Offset: 0x000D9D89
		private event EventHandler DisplayRangeChanged
		{
			add
			{
				this._onDisplayRangeChanged = (EventHandler)Delegate.Combine(this._onDisplayRangeChanged, value);
			}
			remove
			{
				this._onDisplayRangeChanged = (EventHandler)Delegate.Remove(this._onDisplayRangeChanged, value);
			}
		}

		/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.MonthCalendar" /> control.</summary>
		// Token: 0x1400023A RID: 570
		// (add) Token: 0x060030CB RID: 12491 RVA: 0x00023757 File Offset: 0x00021957
		// (remove) Token: 0x060030CC RID: 12492 RVA: 0x00023760 File Offset: 0x00021960
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

		/// <summary>Occurs when the user clicks the <see cref="T:System.Windows.Forms.MonthCalendar" /> control with the mouse.</summary>
		// Token: 0x1400023B RID: 571
		// (add) Token: 0x060030CD RID: 12493 RVA: 0x00012FE6 File Offset: 0x000111E6
		// (remove) Token: 0x060030CE RID: 12494 RVA: 0x00012FEF File Offset: 0x000111EF
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

		/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.MonthCalendar" /> control with the mouse.</summary>
		// Token: 0x1400023C RID: 572
		// (add) Token: 0x060030CF RID: 12495 RVA: 0x00023769 File Offset: 0x00021969
		// (remove) Token: 0x060030D0 RID: 12496 RVA: 0x00023772 File Offset: 0x00021972
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

		/// <summary>Occurs when the control is redrawn.</summary>
		// Token: 0x1400023D RID: 573
		// (add) Token: 0x060030D1 RID: 12497 RVA: 0x00013D73 File Offset: 0x00011F73
		// (remove) Token: 0x060030D2 RID: 12498 RVA: 0x00013D7C File Offset: 0x00011F7C
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.MonthCalendar.RightToLeftLayout" /> property changes.</summary>
		// Token: 0x1400023E RID: 574
		// (add) Token: 0x060030D3 RID: 12499 RVA: 0x000DBBA2 File Offset: 0x000D9DA2
		// (remove) Token: 0x060030D4 RID: 12500 RVA: 0x000DBBBB File Offset: 0x000D9DBB
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

		// Token: 0x060030D5 RID: 12501 RVA: 0x000DBBD4 File Offset: 0x000D9DD4
		private void AdjustSize()
		{
			Size minReqRect = this.GetMinReqRect();
			this.Size = minReqRect;
		}

		// Token: 0x060030D6 RID: 12502 RVA: 0x000DBBF0 File Offset: 0x000D9DF0
		private void BoldDates(DateBoldEventArgs e)
		{
			int size = e.Size;
			e.DaysToBold = new int[size];
			SelectionRange displayRange = this.GetDisplayRange(false);
			int num = displayRange.Start.Month;
			int year = displayRange.Start.Year;
			int count = this.arrayOfDates.Count;
			for (int i = 0; i < count; i++)
			{
				DateTime dateTime = (DateTime)this.arrayOfDates[i];
				if (DateTime.Compare(dateTime, displayRange.Start) >= 0 && DateTime.Compare(dateTime, displayRange.End) <= 0)
				{
					int month = dateTime.Month;
					int year2 = dateTime.Year;
					int num2 = ((year2 == year) ? (month - num) : (month + year2 * 12 - year * 12 - num));
					e.DaysToBold[num2] |= 1 << dateTime.Day - 1;
				}
			}
			num--;
			int j = 0;
			while (j < size)
			{
				e.DaysToBold[j] |= this.monthsOfYear[num % 12] | this.datesToBoldMonthly;
				j++;
				num++;
			}
		}

		// Token: 0x060030D7 RID: 12503 RVA: 0x000DBD18 File Offset: 0x000D9F18
		private bool CompareDayAndMonth(DateTime t1, DateTime t2)
		{
			return t1.Day == t2.Day && t1.Month == t2.Month;
		}

		/// <summary>Overrides the <see cref="M:System.Windows.Forms.Control.CreateHandle" /> method.</summary>
		// Token: 0x060030D8 RID: 12504 RVA: 0x000DBD3C File Offset: 0x000D9F3C
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
			base.CreateHandle();
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Windows.Forms.MonthCalendar" />.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x060030D9 RID: 12505 RVA: 0x000DBD90 File Offset: 0x000D9F90
		protected override void Dispose(bool disposing)
		{
			if (this.mdsBuffer != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.mdsBuffer);
				this.mdsBuffer = IntPtr.Zero;
			}
			base.Dispose(disposing);
		}

		// Token: 0x060030DA RID: 12506 RVA: 0x000DBDC1 File Offset: 0x000D9FC1
		private static string FormatDate(DateTime value)
		{
			return value.ToString("d", CultureInfo.CurrentCulture);
		}

		/// <summary>Retrieves date information that represents the low and high limits of the displayed dates of the control.</summary>
		/// <param name="visible">
		///   <see langword="true" /> to retrieve only the dates that are fully contained in displayed months; otherwise, <see langword="false" />.</param>
		/// <returns>The begin and end dates of the displayed calendar.</returns>
		// Token: 0x060030DB RID: 12507 RVA: 0x000DBDD4 File Offset: 0x000D9FD4
		public SelectionRange GetDisplayRange(bool visible)
		{
			if (visible)
			{
				return this.GetMonthRange(0);
			}
			return this.GetMonthRange(1);
		}

		// Token: 0x060030DC RID: 12508 RVA: 0x000DBDE8 File Offset: 0x000D9FE8
		private MonthCalendar.HitArea GetHitArea(int hit)
		{
			if (hit <= 196608)
			{
				switch (hit)
				{
				case 65536:
					return MonthCalendar.HitArea.TitleBackground;
				case 65537:
					return MonthCalendar.HitArea.TitleMonth;
				case 65538:
					return MonthCalendar.HitArea.TitleYear;
				default:
					switch (hit)
					{
					case 131072:
						return MonthCalendar.HitArea.CalendarBackground;
					case 131073:
						return MonthCalendar.HitArea.Date;
					case 131074:
						return MonthCalendar.HitArea.DayOfWeek;
					case 131075:
						return MonthCalendar.HitArea.WeekNumbers;
					default:
						if (hit == 196608)
						{
							return MonthCalendar.HitArea.TodayLink;
						}
						break;
					}
					break;
				}
			}
			else if (hit <= 16908289)
			{
				if (hit == 16842755)
				{
					return MonthCalendar.HitArea.NextMonthButton;
				}
				if (hit == 16908289)
				{
					return MonthCalendar.HitArea.NextMonthDate;
				}
			}
			else
			{
				if (hit == 33619971)
				{
					return MonthCalendar.HitArea.PrevMonthButton;
				}
				if (hit == 33685505)
				{
					return MonthCalendar.HitArea.PrevMonthDate;
				}
			}
			return MonthCalendar.HitArea.Nowhere;
		}

		// Token: 0x060030DD RID: 12509 RVA: 0x000DBE84 File Offset: 0x000DA084
		private Size GetMinReqRect()
		{
			return this.GetMinReqRect(0, false, false);
		}

		// Token: 0x060030DE RID: 12510 RVA: 0x000DBE90 File Offset: 0x000DA090
		private Size GetMinReqRect(int newDimensionLength, bool updateRows, bool updateCols)
		{
			Size singleMonthSize = this.SingleMonthSize;
			Size textExtent;
			using (WindowsFont windowsFont = WindowsFont.FromFont(this.Font))
			{
				textExtent = WindowsGraphicsCacheManager.MeasurementGraphics.GetTextExtent(DateTime.Now.ToShortDateString(), windowsFont);
			}
			int num = textExtent.Height + 4;
			int num2 = singleMonthSize.Height;
			if (this.ShowToday)
			{
				num2 -= num;
			}
			if (updateRows)
			{
				int num3 = (newDimensionLength - num + 6) / (num2 + 6);
				this.dimensions.Height = ((num3 < 1) ? 1 : num3);
			}
			if (updateCols)
			{
				int num4 = (newDimensionLength - this.scaledExtraPadding) / singleMonthSize.Width;
				this.dimensions.Width = ((num4 < 1) ? 1 : num4);
			}
			singleMonthSize.Width = (singleMonthSize.Width + 6) * this.dimensions.Width - 6;
			singleMonthSize.Height = (num2 + 6) * this.dimensions.Height - 6 + num;
			if (base.IsHandleCreated)
			{
				int num5 = (int)(long)base.SendMessage(4117, 0, 0);
				if (num5 > singleMonthSize.Width)
				{
					singleMonthSize.Width = num5;
				}
			}
			singleMonthSize.Width += this.scaledExtraPadding;
			singleMonthSize.Height += this.scaledExtraPadding;
			return singleMonthSize;
		}

		// Token: 0x060030DF RID: 12511 RVA: 0x000DBFE8 File Offset: 0x000DA1E8
		private SelectionRange GetMonthRange(int flag)
		{
			NativeMethods.SYSTEMTIMEARRAY systemtimearray = new NativeMethods.SYSTEMTIMEARRAY();
			SelectionRange selectionRange = new SelectionRange();
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4103, flag, systemtimearray);
			NativeMethods.SYSTEMTIME systemtime = new NativeMethods.SYSTEMTIME();
			systemtime.wYear = systemtimearray.wYear1;
			systemtime.wMonth = systemtimearray.wMonth1;
			systemtime.wDayOfWeek = systemtimearray.wDayOfWeek1;
			systemtime.wDay = systemtimearray.wDay1;
			selectionRange.Start = DateTimePicker.SysTimeToDateTime(systemtime);
			systemtime.wYear = systemtimearray.wYear2;
			systemtime.wMonth = systemtimearray.wMonth2;
			systemtime.wDayOfWeek = systemtimearray.wDayOfWeek2;
			systemtime.wDay = systemtimearray.wDay2;
			selectionRange.End = DateTimePicker.SysTimeToDateTime(systemtime);
			return selectionRange;
		}

		// Token: 0x060030E0 RID: 12512 RVA: 0x000DC09C File Offset: 0x000DA29C
		private int GetPreferredHeight(int height, bool updateRows)
		{
			return this.GetMinReqRect(height, updateRows, false).Height;
		}

		// Token: 0x060030E1 RID: 12513 RVA: 0x000DC0BC File Offset: 0x000DA2BC
		private int GetPreferredWidth(int width, bool updateCols)
		{
			return this.GetMinReqRect(width, false, updateCols).Width;
		}

		/// <summary>Returns a <see cref="T:System.Windows.Forms.MonthCalendar.HitTestInfo" /> with information on which portion of a month calendar control is at a specified x- and y-coordinate.</summary>
		/// <param name="x">The <see cref="P:System.Drawing.Point.X" /> coordinate of the point to be hit tested.</param>
		/// <param name="y">The <see cref="P:System.Drawing.Point.Y" /> coordinate of the point to be hit tested.</param>
		/// <returns>A <see cref="T:System.Windows.Forms.MonthCalendar.HitTestInfo" /> that contains information about the specified point on the <see cref="T:System.Windows.Forms.MonthCalendar" />.</returns>
		// Token: 0x060030E2 RID: 12514 RVA: 0x000DC0DC File Offset: 0x000DA2DC
		public MonthCalendar.HitTestInfo HitTest(int x, int y)
		{
			NativeMethods.MCHITTESTINFO mchittestinfo = new NativeMethods.MCHITTESTINFO();
			mchittestinfo.pt_x = x;
			mchittestinfo.pt_y = y;
			mchittestinfo.cbSize = Marshal.SizeOf(typeof(NativeMethods.MCHITTESTINFO));
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4110, 0, mchittestinfo);
			MonthCalendar.HitArea hitArea = this.GetHitArea(mchittestinfo.uHit);
			if (MonthCalendar.HitTestInfo.HitAreaHasValidDateTime(hitArea))
			{
				NativeMethods.SYSTEMTIME systemtime = new NativeMethods.SYSTEMTIME();
				systemtime.wYear = mchittestinfo.st_wYear;
				systemtime.wMonth = mchittestinfo.st_wMonth;
				systemtime.wDayOfWeek = mchittestinfo.st_wDayOfWeek;
				systemtime.wDay = mchittestinfo.st_wDay;
				systemtime.wHour = mchittestinfo.st_wHour;
				systemtime.wMinute = mchittestinfo.st_wMinute;
				systemtime.wSecond = mchittestinfo.st_wSecond;
				systemtime.wMilliseconds = mchittestinfo.st_wMilliseconds;
				return new MonthCalendar.HitTestInfo(new Point(mchittestinfo.pt_x, mchittestinfo.pt_y), hitArea, DateTimePicker.SysTimeToDateTime(systemtime));
			}
			return new MonthCalendar.HitTestInfo(new Point(mchittestinfo.pt_x, mchittestinfo.pt_y), hitArea);
		}

		/// <summary>Returns an object with information on which portion of a month calendar control is at a location specified by a <see cref="T:System.Drawing.Point" />.</summary>
		/// <param name="point">A <see cref="T:System.Drawing.Point" /> containing the <see cref="P:System.Drawing.Point.X" /> and <see cref="P:System.Drawing.Point.Y" /> coordinates of the point to be hit tested.</param>
		/// <returns>A <see cref="T:System.Windows.Forms.MonthCalendar.HitTestInfo" /> that contains information about the specified point on the <see cref="T:System.Windows.Forms.MonthCalendar" />.</returns>
		// Token: 0x060030E3 RID: 12515 RVA: 0x000DC1DE File Offset: 0x000DA3DE
		public MonthCalendar.HitTestInfo HitTest(Point point)
		{
			return this.HitTest(point.X, point.Y);
		}

		/// <summary>Determines whether the specified key is a regular input key or a special key that requires preprocessing.</summary>
		/// <param name="keyData">One of the Keys values.</param>
		/// <returns>
		///   <see langword="true" /> if the specified key is a regular input key; otherwise, <see langword="false" />.</returns>
		// Token: 0x060030E4 RID: 12516 RVA: 0x000DC1F4 File Offset: 0x000DA3F4
		protected override bool IsInputKey(Keys keyData)
		{
			if ((keyData & Keys.Alt) == Keys.Alt)
			{
				return false;
			}
			Keys keys = keyData & Keys.KeyCode;
			return keys - Keys.Prior <= 3 || base.IsInputKey(keyData);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060030E5 RID: 12517 RVA: 0x000DC22C File Offset: 0x000DA42C
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			this.SetSelRange(this.selectionStart, this.selectionEnd);
			if (this.maxSelectionCount != 7)
			{
				base.SendMessage(4100, this.maxSelectionCount, 0);
			}
			this.AdjustSize();
			if (this.todayDateSet)
			{
				NativeMethods.SYSTEMTIME systemtime = DateTimePicker.DateTimeToSysTime(this.todayDate);
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4108, 0, systemtime);
			}
			this.SetControlColor(1, this.ForeColor);
			this.SetControlColor(4, this.BackColor);
			this.SetControlColor(2, this.titleBackColor);
			this.SetControlColor(3, this.titleForeColor);
			this.SetControlColor(5, this.trailingForeColor);
			int num;
			if (this.firstDayOfWeek == Day.Default)
			{
				num = 4108;
			}
			else
			{
				num = (int)this.firstDayOfWeek;
			}
			base.SendMessage(4111, 0, num);
			this.SetRange();
			if (this.scrollChange != 0)
			{
				base.SendMessage(4116, this.scrollChange, 0);
			}
			SystemEvents.UserPreferenceChanged += this.MarshaledUserPreferenceChanged;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060030E6 RID: 12518 RVA: 0x000DC33A File Offset: 0x000DA53A
		protected override void OnHandleDestroyed(EventArgs e)
		{
			SystemEvents.UserPreferenceChanged -= this.MarshaledUserPreferenceChanged;
			base.OnHandleDestroyed(e);
		}

		// Token: 0x060030E7 RID: 12519 RVA: 0x000DC354 File Offset: 0x000DA554
		private void OnCalendarViewChanged(EventArgs e)
		{
			EventHandler onCalendarViewChanged = this._onCalendarViewChanged;
			if (onCalendarViewChanged == null)
			{
				return;
			}
			onCalendarViewChanged(this, e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.MonthCalendar.DateChanged" /> event.</summary>
		/// <param name="drevent">A <see cref="T:System.Windows.Forms.DateRangeEventArgs" /> that contains the event data.</param>
		// Token: 0x060030E8 RID: 12520 RVA: 0x000DC368 File Offset: 0x000DA568
		protected virtual void OnDateChanged(DateRangeEventArgs drevent)
		{
			if (this.onDateChanged != null)
			{
				this.onDateChanged(this, drevent);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.MonthCalendar.DateSelected" /> event.</summary>
		/// <param name="drevent">A <see cref="T:System.Windows.Forms.DateRangeEventArgs" /> that contains the event data.</param>
		// Token: 0x060030E9 RID: 12521 RVA: 0x000DC37F File Offset: 0x000DA57F
		protected virtual void OnDateSelected(DateRangeEventArgs drevent)
		{
			if (this.onDateSelected != null)
			{
				this.onDateSelected(this, drevent);
			}
		}

		// Token: 0x060030EA RID: 12522 RVA: 0x000DC396 File Offset: 0x000DA596
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			if (AccessibilityImprovements.Level5 && base.IsAccessibilityObjectCreated)
			{
				MonthCalendar.CalendarCellAccessibleObject focusedCell = ((MonthCalendar.MonthCalendarAccessibleObjectLevel5)base.AccessibilityObject).FocusedCell;
				if (focusedCell == null)
				{
					return;
				}
				focusedCell.RaiseAutomationEvent(20005);
			}
		}

		// Token: 0x060030EB RID: 12523 RVA: 0x000DC3CE File Offset: 0x000DA5CE
		private void OnDisplayRangeChanged(EventArgs e)
		{
			EventHandler onDisplayRangeChanged = this._onDisplayRangeChanged;
			if (onDisplayRangeChanged == null)
			{
				return;
			}
			onDisplayRangeChanged(this, e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060030EC RID: 12524 RVA: 0x000DC3E2 File Offset: 0x000DA5E2
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.AdjustSize();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ForeColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060030ED RID: 12525 RVA: 0x000DC3F1 File Offset: 0x000DA5F1
		protected override void OnForeColorChanged(EventArgs e)
		{
			base.OnForeColorChanged(e);
			this.SetControlColor(1, this.ForeColor);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060030EE RID: 12526 RVA: 0x000DC407 File Offset: 0x000DA607
		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);
			this.SetControlColor(4, this.BackColor);
		}

		// Token: 0x060030EF RID: 12527 RVA: 0x000DC41D File Offset: 0x000DA61D
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if (AccessibilityImprovements.Level5)
			{
				this.UpdateDisplayRange();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.MonthCalendar.RightToLeftLayoutChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060030F0 RID: 12528 RVA: 0x000DC433 File Offset: 0x000DA633
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

		/// <summary>Removes all the annually bold dates.</summary>
		// Token: 0x060030F1 RID: 12529 RVA: 0x000DC464 File Offset: 0x000DA664
		public void RemoveAllAnnuallyBoldedDates()
		{
			this.annualArrayOfDates.Clear();
			for (int i = 0; i < 12; i++)
			{
				this.monthsOfYear[i] = 0;
			}
		}

		/// <summary>Removes all the nonrecurring bold dates.</summary>
		// Token: 0x060030F2 RID: 12530 RVA: 0x000DC492 File Offset: 0x000DA692
		public void RemoveAllBoldedDates()
		{
			this.arrayOfDates.Clear();
		}

		/// <summary>Removes all the monthly bold dates.</summary>
		// Token: 0x060030F3 RID: 12531 RVA: 0x000DC49F File Offset: 0x000DA69F
		public void RemoveAllMonthlyBoldedDates()
		{
			this.monthlyArrayOfDates.Clear();
			this.datesToBoldMonthly = 0;
		}

		/// <summary>Removes the specified date from the list of annually bold dates.</summary>
		/// <param name="date">The date to remove from the date list.</param>
		// Token: 0x060030F4 RID: 12532 RVA: 0x000DC4B4 File Offset: 0x000DA6B4
		public void RemoveAnnuallyBoldedDate(DateTime date)
		{
			int num = this.annualArrayOfDates.Count;
			int i;
			for (i = 0; i < num; i++)
			{
				if (this.CompareDayAndMonth((DateTime)this.annualArrayOfDates[i], date))
				{
					this.annualArrayOfDates.RemoveAt(i);
					break;
				}
			}
			num--;
			for (int j = i; j < num; j++)
			{
				if (this.CompareDayAndMonth((DateTime)this.annualArrayOfDates[j], date))
				{
					return;
				}
			}
			this.monthsOfYear[date.Month - 1] &= ~(1 << date.Day - 1);
		}

		/// <summary>Removes the specified date from the list of nonrecurring bold dates.</summary>
		/// <param name="date">The date to remove from the date list.</param>
		// Token: 0x060030F5 RID: 12533 RVA: 0x000DC554 File Offset: 0x000DA754
		public void RemoveBoldedDate(DateTime date)
		{
			int count = this.arrayOfDates.Count;
			for (int i = 0; i < count; i++)
			{
				if (DateTime.Compare(((DateTime)this.arrayOfDates[i]).Date, date.Date) == 0)
				{
					this.arrayOfDates.RemoveAt(i);
					base.Invalidate();
					return;
				}
			}
		}

		/// <summary>Removes the specified date from the list of monthly bolded dates.</summary>
		/// <param name="date">The date to remove from the date list.</param>
		// Token: 0x060030F6 RID: 12534 RVA: 0x000DC5B4 File Offset: 0x000DA7B4
		public void RemoveMonthlyBoldedDate(DateTime date)
		{
			int num = this.monthlyArrayOfDates.Count;
			int i;
			for (i = 0; i < num; i++)
			{
				if (this.CompareDayAndMonth((DateTime)this.monthlyArrayOfDates[i], date))
				{
					this.monthlyArrayOfDates.RemoveAt(i);
					break;
				}
			}
			num--;
			for (int j = i; j < num; j++)
			{
				if (this.CompareDayAndMonth((DateTime)this.monthlyArrayOfDates[j], date))
				{
					return;
				}
			}
			this.datesToBoldMonthly &= ~(1 << date.Day - 1);
		}

		// Token: 0x060030F7 RID: 12535 RVA: 0x000DC648 File Offset: 0x000DA848
		private void ResetAnnuallyBoldedDates()
		{
			this.annualArrayOfDates.Clear();
		}

		// Token: 0x060030F8 RID: 12536 RVA: 0x000DC492 File Offset: 0x000DA692
		private void ResetBoldedDates()
		{
			this.arrayOfDates.Clear();
		}

		// Token: 0x060030F9 RID: 12537 RVA: 0x000DC655 File Offset: 0x000DA855
		private void ResetCalendarDimensions()
		{
			this.CalendarDimensions = new Size(1, 1);
		}

		// Token: 0x060030FA RID: 12538 RVA: 0x000DC664 File Offset: 0x000DA864
		private void ResetMaxDate()
		{
			this.MaxDate = DateTime.MaxValue;
		}

		// Token: 0x060030FB RID: 12539 RVA: 0x000DC671 File Offset: 0x000DA871
		private void ResetMinDate()
		{
			this.MinDate = DateTime.MinValue;
		}

		// Token: 0x060030FC RID: 12540 RVA: 0x000DC67E File Offset: 0x000DA87E
		private void ResetMonthlyBoldedDates()
		{
			this.monthlyArrayOfDates.Clear();
		}

		// Token: 0x060030FD RID: 12541 RVA: 0x000DC68B File Offset: 0x000DA88B
		private void ResetSelectionRange()
		{
			this.SetSelectionRange(this.Now, this.Now);
		}

		// Token: 0x060030FE RID: 12542 RVA: 0x000DC69F File Offset: 0x000DA89F
		private void ResetTrailingForeColor()
		{
			this.TrailingForeColor = MonthCalendar.DEFAULT_TRAILING_FORE_COLOR;
		}

		// Token: 0x060030FF RID: 12543 RVA: 0x000DC6AC File Offset: 0x000DA8AC
		private void ResetTitleForeColor()
		{
			this.TitleForeColor = MonthCalendar.DEFAULT_TITLE_FORE_COLOR;
		}

		// Token: 0x06003100 RID: 12544 RVA: 0x000DC6B9 File Offset: 0x000DA8B9
		private void ResetTitleBackColor()
		{
			this.TitleBackColor = MonthCalendar.DEFAULT_TITLE_BACK_COLOR;
		}

		// Token: 0x06003101 RID: 12545 RVA: 0x000DC6C6 File Offset: 0x000DA8C6
		private void ResetTodayDate()
		{
			this.todayDateSet = false;
			this.UpdateTodayDate();
		}

		// Token: 0x06003102 RID: 12546 RVA: 0x000DC6D8 File Offset: 0x000DA8D8
		private IntPtr RequestBuffer(int reqSize)
		{
			int num = 4;
			if (reqSize * num > this.mdsBufferSize)
			{
				if (this.mdsBuffer != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.mdsBuffer);
					this.mdsBuffer = IntPtr.Zero;
				}
				float num2 = (float)(reqSize - 1) / 12f;
				int num3 = (int)(num2 + 1f) * 12;
				this.mdsBufferSize = num3 * num;
				this.mdsBuffer = Marshal.AllocHGlobal(this.mdsBufferSize);
				return this.mdsBuffer;
			}
			return this.mdsBuffer;
		}

		/// <summary>Overrides the <see cref="M:System.Windows.Forms.Control.SetBoundsCore(System.Int32,System.Int32,System.Int32,System.Int32,System.Windows.Forms.BoundsSpecified)" /> method.</summary>
		/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control.</param>
		/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Right" /> property value of the control.</param>
		/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control.</param>
		/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control.</param>
		/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values.</param>
		// Token: 0x06003103 RID: 12547 RVA: 0x000DC758 File Offset: 0x000DA958
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			Rectangle bounds = base.Bounds;
			Size maxWindowTrackSize = SystemInformation.MaxWindowTrackSize;
			bool flag = !DpiHelper.EnableMonthCalendarHighDpiImprovements || !base.IsCurrentlyBeingScaled;
			if (width != bounds.Width)
			{
				if (width > maxWindowTrackSize.Width)
				{
					width = maxWindowTrackSize.Width;
				}
				width = this.GetPreferredWidth(width, flag);
			}
			if (height != bounds.Height)
			{
				if (height > maxWindowTrackSize.Height)
				{
					height = maxWindowTrackSize.Height;
				}
				height = this.GetPreferredHeight(height, flag);
			}
			base.SetBoundsCore(x, y, width, height, specified);
		}

		// Token: 0x06003104 RID: 12548 RVA: 0x000DC7E4 File Offset: 0x000DA9E4
		private void SetControlColor(int colorIndex, Color value)
		{
			if (base.IsHandleCreated)
			{
				base.SendMessage(4106, colorIndex, ColorTranslator.ToWin32(value));
			}
		}

		// Token: 0x06003105 RID: 12549 RVA: 0x000DC801 File Offset: 0x000DAA01
		private void SetRange()
		{
			this.SetRange(DateTimePicker.EffectiveMinDate(this.minDate), DateTimePicker.EffectiveMaxDate(this.maxDate));
		}

		// Token: 0x06003106 RID: 12550 RVA: 0x000DC820 File Offset: 0x000DAA20
		private void SetRange(DateTime minDate, DateTime maxDate)
		{
			if (this.selectionStart < minDate)
			{
				this.selectionStart = minDate;
			}
			if (this.selectionStart > maxDate)
			{
				this.selectionStart = maxDate;
			}
			if (this.selectionEnd < minDate)
			{
				this.selectionEnd = minDate;
			}
			if (this.selectionEnd > maxDate)
			{
				this.selectionEnd = maxDate;
			}
			if (AccessibilityImprovements.Level5)
			{
				if (this.selectionStart > this._focusedDate)
				{
					this._focusedDate = this.selectionStart.Date;
				}
				if (this.selectionEnd < this._focusedDate)
				{
					this._focusedDate = this.selectionEnd.Date;
				}
			}
			this.SetSelRange(this.selectionStart, this.selectionEnd);
			if (base.IsHandleCreated)
			{
				int num = 0;
				NativeMethods.SYSTEMTIMEARRAY systemtimearray = new NativeMethods.SYSTEMTIMEARRAY();
				num |= 3;
				NativeMethods.SYSTEMTIME systemtime = DateTimePicker.DateTimeToSysTime(minDate);
				systemtimearray.wYear1 = systemtime.wYear;
				systemtimearray.wMonth1 = systemtime.wMonth;
				systemtimearray.wDayOfWeek1 = systemtime.wDayOfWeek;
				systemtimearray.wDay1 = systemtime.wDay;
				systemtime = DateTimePicker.DateTimeToSysTime(maxDate);
				systemtimearray.wYear2 = systemtime.wYear;
				systemtimearray.wMonth2 = systemtime.wMonth;
				systemtimearray.wDayOfWeek2 = systemtime.wDayOfWeek;
				systemtimearray.wDay2 = systemtime.wDay;
				if ((int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4114, num, systemtimearray) == 0)
				{
					throw new InvalidOperationException(SR.GetString("MonthCalendarRange", new object[]
					{
						minDate.ToShortDateString(),
						maxDate.ToShortDateString()
					}));
				}
				if (AccessibilityImprovements.Level5)
				{
					this.UpdateDisplayRange();
				}
			}
		}

		/// <summary>Sets the number of columns and rows of months to display.</summary>
		/// <param name="x">The number of columns.</param>
		/// <param name="y">The number of rows.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="x" /> or <paramref name="y" /> is less than 1.</exception>
		// Token: 0x06003107 RID: 12551 RVA: 0x000DC9C0 File Offset: 0x000DABC0
		public void SetCalendarDimensions(int x, int y)
		{
			if (x < 1)
			{
				throw new ArgumentOutOfRangeException("x", SR.GetString("MonthCalendarInvalidDimensions", new object[]
				{
					x.ToString("D", CultureInfo.CurrentCulture),
					y.ToString("D", CultureInfo.CurrentCulture)
				}));
			}
			if (y < 1)
			{
				throw new ArgumentOutOfRangeException("y", SR.GetString("MonthCalendarInvalidDimensions", new object[]
				{
					x.ToString("D", CultureInfo.CurrentCulture),
					y.ToString("D", CultureInfo.CurrentCulture)
				}));
			}
			while (x * y > 12)
			{
				if (x > y)
				{
					x--;
				}
				else
				{
					y--;
				}
			}
			if (this.dimensions.Width != x || this.dimensions.Height != y)
			{
				this.dimensions.Width = x;
				this.dimensions.Height = y;
				this.AdjustSize();
			}
		}

		/// <summary>Sets a date as the currently selected date.</summary>
		/// <param name="date">The date to be selected.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than the minimum allowable date.  
		///  -or-  
		///  The value is greater than the maximum allowable date.  
		///  This exception will only be thrown if <see cref="P:System.Windows.Forms.MonthCalendar.MinDate" /> or <see cref="P:System.Windows.Forms.MonthCalendar.MaxDate" /> have been set explicitly.</exception>
		// Token: 0x06003108 RID: 12552 RVA: 0x000DCAAC File Offset: 0x000DACAC
		public void SetDate(DateTime date)
		{
			if (date.Ticks < this.minDate.Ticks)
			{
				throw new ArgumentOutOfRangeException("date", SR.GetString("InvalidLowBoundArgumentEx", new object[]
				{
					"date",
					MonthCalendar.FormatDate(date),
					"MinDate"
				}));
			}
			if (date.Ticks > this.maxDate.Ticks)
			{
				throw new ArgumentOutOfRangeException("date", SR.GetString("InvalidHighBoundArgumentEx", new object[]
				{
					"date",
					MonthCalendar.FormatDate(date),
					"MaxDate"
				}));
			}
			this.SetSelectionRange(date, date);
		}

		/// <summary>Sets the selected dates in a month calendar control to the specified date range.</summary>
		/// <param name="date1">The beginning date of the selection range.</param>
		/// <param name="date2">The end date of the selection range.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="date1" /> is less than the minimum date allowable for a month calendar control.  
		/// -or-  
		/// <paramref name="date1" /> is greater than the maximum allowable date for a month calendar control.  
		/// -or-  
		/// <paramref name="date2" /> is less than the minimum date allowable for a month calendar control.  
		/// -or-  
		/// <paramref name="date2" /> is greater than the maximum allowable date for a month calendar control.  
		/// This exception will only be thrown if <see cref="P:System.Windows.Forms.MonthCalendar.MinDate" /> or <see cref="P:System.Windows.Forms.MonthCalendar.MaxDate" /> have been set explicitly.</exception>
		// Token: 0x06003109 RID: 12553 RVA: 0x000DCB54 File Offset: 0x000DAD54
		public void SetSelectionRange(DateTime date1, DateTime date2)
		{
			if (date1.Ticks < this.minDate.Ticks)
			{
				throw new ArgumentOutOfRangeException("date1", SR.GetString("InvalidLowBoundArgumentEx", new object[]
				{
					"SelectionStart",
					MonthCalendar.FormatDate(date1),
					"MinDate"
				}));
			}
			if (date1.Ticks > this.maxDate.Ticks)
			{
				throw new ArgumentOutOfRangeException("date1", SR.GetString("InvalidHighBoundArgumentEx", new object[]
				{
					"SelectionEnd",
					MonthCalendar.FormatDate(date1),
					"MaxDate"
				}));
			}
			if (date2.Ticks < this.minDate.Ticks)
			{
				throw new ArgumentOutOfRangeException("date2", SR.GetString("InvalidLowBoundArgumentEx", new object[]
				{
					"SelectionStart",
					MonthCalendar.FormatDate(date2),
					"MinDate"
				}));
			}
			if (date2.Ticks > this.maxDate.Ticks)
			{
				throw new ArgumentOutOfRangeException("date2", SR.GetString("InvalidHighBoundArgumentEx", new object[]
				{
					"SelectionEnd",
					MonthCalendar.FormatDate(date2),
					"MaxDate"
				}));
			}
			if (date1 > date2)
			{
				date2 = date1;
			}
			if ((date2 - date1).Days >= this.maxSelectionCount)
			{
				if (date1.Ticks == this.selectionStart.Ticks)
				{
					date1 = date2.AddDays((double)(1 - this.maxSelectionCount));
				}
				else
				{
					date2 = date1.AddDays((double)(this.maxSelectionCount - 1));
				}
			}
			this.SetSelRange(date1, date2);
		}

		// Token: 0x0600310A RID: 12554 RVA: 0x000DCCE8 File Offset: 0x000DAEE8
		private void SetSelRange(DateTime lower, DateTime upper)
		{
			bool flag = false;
			if (this.selectionStart != lower || this.selectionEnd != upper)
			{
				flag = true;
				this.selectionStart = lower;
				this.selectionEnd = upper;
			}
			if (base.IsHandleCreated)
			{
				NativeMethods.SYSTEMTIMEARRAY systemtimearray = new NativeMethods.SYSTEMTIMEARRAY();
				NativeMethods.SYSTEMTIME systemtime = DateTimePicker.DateTimeToSysTime(lower);
				systemtimearray.wYear1 = systemtime.wYear;
				systemtimearray.wMonth1 = systemtime.wMonth;
				systemtimearray.wDayOfWeek1 = systemtime.wDayOfWeek;
				systemtimearray.wDay1 = systemtime.wDay;
				systemtime = DateTimePicker.DateTimeToSysTime(upper);
				systemtimearray.wYear2 = systemtime.wYear;
				systemtimearray.wMonth2 = systemtime.wMonth;
				systemtimearray.wDayOfWeek2 = systemtime.wDayOfWeek;
				systemtimearray.wDay2 = systemtime.wDay;
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4102, 0, systemtimearray);
			}
			if (flag)
			{
				this.OnDateChanged(new DateRangeEventArgs(lower, upper));
			}
		}

		// Token: 0x0600310B RID: 12555 RVA: 0x000DCDCB File Offset: 0x000DAFCB
		private bool ShouldSerializeAnnuallyBoldedDates()
		{
			return this.annualArrayOfDates.Count > 0;
		}

		// Token: 0x0600310C RID: 12556 RVA: 0x000DCDDB File Offset: 0x000DAFDB
		private bool ShouldSerializeBoldedDates()
		{
			return this.arrayOfDates.Count > 0;
		}

		// Token: 0x0600310D RID: 12557 RVA: 0x000DCDEB File Offset: 0x000DAFEB
		private bool ShouldSerializeCalendarDimensions()
		{
			return !this.dimensions.Equals(new Size(1, 1));
		}

		// Token: 0x0600310E RID: 12558 RVA: 0x000DCE10 File Offset: 0x000DB010
		private bool ShouldSerializeTrailingForeColor()
		{
			return !this.TrailingForeColor.Equals(MonthCalendar.DEFAULT_TRAILING_FORE_COLOR);
		}

		// Token: 0x0600310F RID: 12559 RVA: 0x000DCE40 File Offset: 0x000DB040
		private bool ShouldSerializeTitleForeColor()
		{
			return !this.TitleForeColor.Equals(MonthCalendar.DEFAULT_TITLE_FORE_COLOR);
		}

		// Token: 0x06003110 RID: 12560 RVA: 0x000DCE70 File Offset: 0x000DB070
		private bool ShouldSerializeTitleBackColor()
		{
			return !this.TitleBackColor.Equals(MonthCalendar.DEFAULT_TITLE_BACK_COLOR);
		}

		// Token: 0x06003111 RID: 12561 RVA: 0x000DCE9E File Offset: 0x000DB09E
		private bool ShouldSerializeMonthlyBoldedDates()
		{
			return this.monthlyArrayOfDates.Count > 0;
		}

		// Token: 0x06003112 RID: 12562 RVA: 0x000DCEAE File Offset: 0x000DB0AE
		private bool ShouldSerializeMaxDate()
		{
			return this.maxDate != DateTimePicker.MaximumDateTime && this.maxDate != DateTime.MaxValue;
		}

		// Token: 0x06003113 RID: 12563 RVA: 0x000DCED4 File Offset: 0x000DB0D4
		private bool ShouldSerializeMinDate()
		{
			return this.minDate != DateTimePicker.MinimumDateTime && this.minDate != DateTime.MinValue;
		}

		// Token: 0x06003114 RID: 12564 RVA: 0x000DCEFA File Offset: 0x000DB0FA
		private bool ShouldSerializeSelectionRange()
		{
			return !DateTime.Equals(this.selectionEnd, this.selectionStart);
		}

		// Token: 0x06003115 RID: 12565 RVA: 0x000DB982 File Offset: 0x000D9B82
		private bool ShouldSerializeTodayDate()
		{
			return this.todayDateSet;
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.MonthCalendar" /> control.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.MonthCalendar" />.</returns>
		// Token: 0x06003116 RID: 12566 RVA: 0x000DCF10 File Offset: 0x000DB110
		public override string ToString()
		{
			string text = base.ToString();
			return text + ", " + this.SelectionRange.ToString();
		}

		/// <summary>Repaints the bold dates to reflect the dates set in the lists of bold dates.</summary>
		// Token: 0x06003117 RID: 12567 RVA: 0x000DCF3A File Offset: 0x000DB13A
		public void UpdateBoldedDates()
		{
			base.RecreateHandle();
		}

		// Token: 0x06003118 RID: 12568 RVA: 0x000DCF44 File Offset: 0x000DB144
		private void UpdateDisplayRange()
		{
			if (!base.IsHandleCreated)
			{
				return;
			}
			SelectionRange displayRange = this.GetDisplayRange(false);
			if (this._currentDisplayRange == null)
			{
				this._currentDisplayRange = displayRange;
				return;
			}
			if (this._currentDisplayRange.Start != displayRange.Start || this._currentDisplayRange.End != displayRange.End)
			{
				this._currentDisplayRange = displayRange;
				this.OnDisplayRangeChanged(EventArgs.Empty);
			}
		}

		// Token: 0x06003119 RID: 12569 RVA: 0x000DCFB4 File Offset: 0x000DB1B4
		private void UpdateTodayDate()
		{
			if (base.IsHandleCreated)
			{
				NativeMethods.SYSTEMTIME systemtime = null;
				if (this.todayDateSet)
				{
					systemtime = DateTimePicker.DateTimeToSysTime(this.todayDate);
				}
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4108, 0, systemtime);
			}
		}

		// Token: 0x0600311A RID: 12570 RVA: 0x000DCFF8 File Offset: 0x000DB1F8
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

		// Token: 0x0600311B RID: 12571 RVA: 0x000ABCD8 File Offset: 0x000A9ED8
		private void UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs pref)
		{
			if (pref.Category == UserPreferenceCategory.Locale)
			{
				base.RecreateHandle();
			}
		}

		// Token: 0x0600311C RID: 12572 RVA: 0x000DD03C File Offset: 0x000DB23C
		private void WmDateChanged(ref Message m)
		{
			NativeMethods.NMSELCHANGE nmselchange = (NativeMethods.NMSELCHANGE)m.GetLParam(typeof(NativeMethods.NMSELCHANGE));
			DateTime dateTime;
			DateTime dateTime2;
			if (AccessibilityImprovements.Level5)
			{
				dateTime = nmselchange.stSelStart;
				dateTime2 = nmselchange.stSelEnd;
				this._focusedDate = ((dateTime == this.selectionStart) ? dateTime2.Date : dateTime.Date);
				this.selectionStart = dateTime;
				this.selectionEnd = dateTime2;
			}
			else
			{
				dateTime = (this.selectionStart = DateTimePicker.SysTimeToDateTime(nmselchange.stSelStart));
				dateTime2 = (this.selectionEnd = DateTimePicker.SysTimeToDateTime(nmselchange.stSelEnd));
			}
			if (AccessibilityImprovements.Level1)
			{
				base.AccessibilityNotifyClients(AccessibleEvents.NameChange, -1);
				base.AccessibilityNotifyClients(AccessibleEvents.ValueChange, -1);
			}
			if (dateTime.Ticks < this.minDate.Ticks || dateTime2.Ticks < this.minDate.Ticks)
			{
				this.SetSelRange(this.minDate, this.minDate);
			}
			else if (dateTime.Ticks > this.maxDate.Ticks || dateTime2.Ticks > this.maxDate.Ticks)
			{
				this.SetSelRange(this.maxDate, this.maxDate);
			}
			if (AccessibilityImprovements.Level5 && base.IsAccessibilityObjectCreated)
			{
				this.UpdateDisplayRange();
				MonthCalendar.MonthCalendarAccessibleObjectLevel5 monthCalendarAccessibleObjectLevel = (MonthCalendar.MonthCalendarAccessibleObjectLevel5)base.AccessibilityObject;
				monthCalendarAccessibleObjectLevel.RaiseAutomationEventForChild(20005);
			}
			this.OnDateChanged(new DateRangeEventArgs(dateTime, dateTime2));
		}

		// Token: 0x0600311D RID: 12573 RVA: 0x000DD1AC File Offset: 0x000DB3AC
		private void WmDateBold(ref Message m)
		{
			NativeMethods.NMDAYSTATE nmdaystate = (NativeMethods.NMDAYSTATE)m.GetLParam(typeof(NativeMethods.NMDAYSTATE));
			DateTime dateTime = DateTimePicker.SysTimeToDateTime(nmdaystate.stStart);
			DateBoldEventArgs dateBoldEventArgs = new DateBoldEventArgs(dateTime, nmdaystate.cDayState);
			this.BoldDates(dateBoldEventArgs);
			this.mdsBuffer = this.RequestBuffer(dateBoldEventArgs.Size);
			Marshal.Copy(dateBoldEventArgs.DaysToBold, 0, this.mdsBuffer, dateBoldEventArgs.Size);
			nmdaystate.prgDayState = this.mdsBuffer;
			Marshal.StructureToPtr(nmdaystate, m.LParam, false);
		}

		// Token: 0x0600311E RID: 12574 RVA: 0x000DD234 File Offset: 0x000DB434
		private void WmCalViewChanged(ref Message m)
		{
			NativeMethods.NMVIEWCHANGE nmviewchange = (NativeMethods.NMVIEWCHANGE)m.GetLParam(typeof(NativeMethods.NMVIEWCHANGE));
			if (this.mcCurView != (NativeMethods.MONTCALENDAR_VIEW_MODE)nmviewchange.uNewView)
			{
				this.mcOldView = this.mcCurView;
				this.mcCurView = (NativeMethods.MONTCALENDAR_VIEW_MODE)nmviewchange.uNewView;
				if (AccessibilityImprovements.Level5)
				{
					this.OnCalendarViewChanged(EventArgs.Empty);
				}
				if (AccessibilityImprovements.Level1)
				{
					base.AccessibilityNotifyClients(AccessibleEvents.ValueChange, -1);
					base.AccessibilityNotifyClients(AccessibleEvents.NameChange, -1);
				}
			}
		}

		// Token: 0x0600311F RID: 12575 RVA: 0x000DD2B0 File Offset: 0x000DB4B0
		private void WmDateSelected(ref Message m)
		{
			NativeMethods.NMSELCHANGE nmselchange = (NativeMethods.NMSELCHANGE)m.GetLParam(typeof(NativeMethods.NMSELCHANGE));
			DateTime dateTime = (this.selectionStart = DateTimePicker.SysTimeToDateTime(nmselchange.stSelStart));
			DateTime dateTime2 = (this.selectionEnd = DateTimePicker.SysTimeToDateTime(nmselchange.stSelEnd));
			if (AccessibilityImprovements.Level1)
			{
				base.AccessibilityNotifyClients(AccessibleEvents.NameChange, -1);
				base.AccessibilityNotifyClients(AccessibleEvents.ValueChange, -1);
			}
			if (dateTime.Ticks < this.minDate.Ticks || dateTime2.Ticks < this.minDate.Ticks)
			{
				this.SetSelRange(this.minDate, this.minDate);
			}
			else if (dateTime.Ticks > this.maxDate.Ticks || dateTime2.Ticks > this.maxDate.Ticks)
			{
				this.SetSelRange(this.maxDate, this.maxDate);
			}
			this.OnDateSelected(new DateRangeEventArgs(dateTime, dateTime2));
		}

		// Token: 0x06003120 RID: 12576 RVA: 0x000DD39F File Offset: 0x000DB59F
		private void WmGetDlgCode(ref Message m)
		{
			m.Result = (IntPtr)1;
		}

		// Token: 0x06003121 RID: 12577 RVA: 0x000DD3B0 File Offset: 0x000DB5B0
		private void WmReflectCommand(ref Message m)
		{
			if (m.HWnd == base.Handle)
			{
				NativeMethods.NMHDR nmhdr = (NativeMethods.NMHDR)m.GetLParam(typeof(NativeMethods.NMHDR));
				switch (nmhdr.code)
				{
				case -750:
					if (AccessibilityImprovements.Level1)
					{
						this.WmCalViewChanged(ref m);
					}
					break;
				case -749:
					this.WmDateChanged(ref m);
					return;
				case -748:
					break;
				case -747:
					this.WmDateBold(ref m);
					if (AccessibilityImprovements.Level5)
					{
						this.UpdateDisplayRange();
						return;
					}
					break;
				case -746:
					this.WmDateSelected(ref m);
					return;
				default:
					return;
				}
			}
		}

		/// <summary>Overrides the <see cref="M:System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@)" /> method.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x06003122 RID: 12578 RVA: 0x000DD444 File Offset: 0x000DB644
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg <= 15)
			{
				if (msg != 2)
				{
					if (msg == 15)
					{
						base.WndProc(ref m);
						if (AccessibilityImprovements.Level5 && this.mcCurView != NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_MONTH)
						{
							this.UpdateDisplayRange();
							return;
						}
						return;
					}
				}
				else
				{
					bool? flag = MonthCalendar.restrictUnmanagedCode;
					bool flag2 = true;
					if (((flag.GetValueOrDefault() == flag2) & (flag != null)) && this.nativeWndProcCount > 0)
					{
						throw new InvalidOperationException();
					}
					base.WndProc(ref m);
					if (!AccessibilityImprovements.Level5 || !base.IsAccessibilityObjectCreated)
					{
						return;
					}
					if (base.IsHandleCreated)
					{
						UnsafeNativeMethods.UiaReturnRawElementProvider(new HandleRef(this, base.Handle), IntPtr.Zero, IntPtr.Zero, null);
					}
					if (ApiHelper.IsApiAvailable("UIAutomationCore.dll", "UiaDisconnectProvider"))
					{
						((MonthCalendar.MonthCalendarAccessibleObjectLevel5)base.AccessibilityObject).DisconnectChildren();
						int num = UnsafeNativeMethods.UiaDisconnectProvider(base.AccessibilityObject);
						return;
					}
					return;
				}
			}
			else
			{
				if (msg == 135)
				{
					this.WmGetDlgCode(ref m);
					return;
				}
				if (msg != 513)
				{
					if (msg == 8270)
					{
						this.WmReflectCommand(ref m);
						base.WndProc(ref m);
						return;
					}
				}
				else
				{
					this.FocusInternal();
					if (!base.ValidationCancelled)
					{
						base.WndProc(ref m);
						return;
					}
					return;
				}
			}
			base.WndProc(ref m);
		}

		/// <summary>Overrides the <see cref="M:System.Windows.Forms.Control.DefWndProc(System.Windows.Forms.Message@)" /> method.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x06003123 RID: 12579 RVA: 0x000DD574 File Offset: 0x000DB774
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void DefWndProc(ref Message m)
		{
			bool? flag = MonthCalendar.restrictUnmanagedCode;
			bool flag2 = true;
			if ((flag.GetValueOrDefault() == flag2) & (flag != null))
			{
				this.nativeWndProcCount++;
				try
				{
					base.DefWndProc(ref m);
				}
				finally
				{
					this.nativeWndProcCount--;
				}
				return;
			}
			base.DefWndProc(ref m);
		}

		// Token: 0x04001403 RID: 5123
		private const long DAYS_TO_1601 = 548229L;

		// Token: 0x04001404 RID: 5124
		private const long DAYS_TO_10000 = 3615900L;

		// Token: 0x04001405 RID: 5125
		private static readonly Color DEFAULT_TITLE_BACK_COLOR = SystemColors.ActiveCaption;

		// Token: 0x04001406 RID: 5126
		private static readonly Color DEFAULT_TITLE_FORE_COLOR = SystemColors.ActiveCaptionText;

		// Token: 0x04001407 RID: 5127
		private static readonly Color DEFAULT_TRAILING_FORE_COLOR = SystemColors.GrayText;

		// Token: 0x04001408 RID: 5128
		private const int MINIMUM_ALLOC_SIZE = 12;

		// Token: 0x04001409 RID: 5129
		private const int MONTHS_IN_YEAR = 12;

		// Token: 0x0400140A RID: 5130
		private const int INSERT_WIDTH_SIZE = 6;

		// Token: 0x0400140B RID: 5131
		private const int INSERT_HEIGHT_SIZE = 6;

		// Token: 0x0400140C RID: 5132
		private const Day DEFAULT_FIRST_DAY_OF_WEEK = Day.Default;

		// Token: 0x0400140D RID: 5133
		private const int DEFAULT_MAX_SELECTION_COUNT = 7;

		// Token: 0x0400140E RID: 5134
		private const int DEFAULT_SCROLL_CHANGE = 0;

		// Token: 0x0400140F RID: 5135
		private const int UNIQUE_DATE = 0;

		// Token: 0x04001410 RID: 5136
		private const int ANNUAL_DATE = 1;

		// Token: 0x04001411 RID: 5137
		private const int MONTHLY_DATE = 2;

		// Token: 0x04001412 RID: 5138
		private static readonly Size DefaultSingleMonthSize = new Size(176, 153);

		// Token: 0x04001413 RID: 5139
		private const int MaxScrollChange = 20000;

		// Token: 0x04001414 RID: 5140
		private const int ExtraPadding = 2;

		// Token: 0x04001415 RID: 5141
		private int scaledExtraPadding = 2;

		// Token: 0x04001416 RID: 5142
		private IntPtr mdsBuffer = IntPtr.Zero;

		// Token: 0x04001417 RID: 5143
		private int mdsBufferSize;

		// Token: 0x04001418 RID: 5144
		private Color titleBackColor = MonthCalendar.DEFAULT_TITLE_BACK_COLOR;

		// Token: 0x04001419 RID: 5145
		private Color titleForeColor = MonthCalendar.DEFAULT_TITLE_FORE_COLOR;

		// Token: 0x0400141A RID: 5146
		private Color trailingForeColor = MonthCalendar.DEFAULT_TRAILING_FORE_COLOR;

		// Token: 0x0400141B RID: 5147
		private bool showToday = true;

		// Token: 0x0400141C RID: 5148
		private bool showTodayCircle = true;

		// Token: 0x0400141D RID: 5149
		private bool showWeekNumbers;

		// Token: 0x0400141E RID: 5150
		private bool rightToLeftLayout;

		// Token: 0x0400141F RID: 5151
		private Size dimensions = new Size(1, 1);

		// Token: 0x04001420 RID: 5152
		private int maxSelectionCount = 7;

		// Token: 0x04001421 RID: 5153
		private DateTime maxDate = DateTime.MaxValue;

		// Token: 0x04001422 RID: 5154
		private DateTime minDate = DateTime.MinValue;

		// Token: 0x04001423 RID: 5155
		private int scrollChange;

		// Token: 0x04001424 RID: 5156
		private bool todayDateSet;

		// Token: 0x04001425 RID: 5157
		private DateTime todayDate = DateTime.Now.Date;

		// Token: 0x04001426 RID: 5158
		private DateTime selectionStart;

		// Token: 0x04001427 RID: 5159
		private DateTime selectionEnd;

		// Token: 0x04001428 RID: 5160
		private DateTime _focusedDate;

		// Token: 0x04001429 RID: 5161
		private SelectionRange _currentDisplayRange;

		// Token: 0x0400142A RID: 5162
		private Day firstDayOfWeek = Day.Default;

		// Token: 0x0400142B RID: 5163
		private NativeMethods.MONTCALENDAR_VIEW_MODE mcCurView;

		// Token: 0x0400142C RID: 5164
		private NativeMethods.MONTCALENDAR_VIEW_MODE mcOldView;

		// Token: 0x0400142D RID: 5165
		private int[] monthsOfYear = new int[12];

		// Token: 0x0400142E RID: 5166
		private int datesToBoldMonthly;

		// Token: 0x0400142F RID: 5167
		private ArrayList arrayOfDates = new ArrayList();

		// Token: 0x04001430 RID: 5168
		private ArrayList annualArrayOfDates = new ArrayList();

		// Token: 0x04001431 RID: 5169
		private ArrayList monthlyArrayOfDates = new ArrayList();

		// Token: 0x04001432 RID: 5170
		private DateRangeEventHandler onDateChanged;

		// Token: 0x04001433 RID: 5171
		private DateRangeEventHandler onDateSelected;

		// Token: 0x04001434 RID: 5172
		private EventHandler onRightToLeftLayoutChanged;

		// Token: 0x04001435 RID: 5173
		private EventHandler _onCalendarViewChanged;

		// Token: 0x04001436 RID: 5174
		private EventHandler _onDisplayRangeChanged;

		// Token: 0x04001437 RID: 5175
		private int nativeWndProcCount;

		// Token: 0x04001438 RID: 5176
		private static bool? restrictUnmanagedCode;

		/// <summary>Contains information about an area of a <see cref="T:System.Windows.Forms.MonthCalendar" /> control. This class cannot be inherited.</summary>
		// Token: 0x020006DC RID: 1756
		public sealed class HitTestInfo
		{
			// Token: 0x06006AFE RID: 27390 RVA: 0x0018BC83 File Offset: 0x00189E83
			internal HitTestInfo(Point pt, MonthCalendar.HitArea area, DateTime time)
			{
				this.point = pt;
				this.hitArea = area;
				this.time = time;
			}

			// Token: 0x06006AFF RID: 27391 RVA: 0x0018BCA0 File Offset: 0x00189EA0
			internal HitTestInfo(Point pt, MonthCalendar.HitArea area)
			{
				this.point = pt;
				this.hitArea = area;
			}

			/// <summary>Gets the point that was hit-tested.</summary>
			/// <returns>A <see cref="T:System.Drawing.Point" /> containing the <see cref="P:System.Drawing.Point.X" /> and <see cref="P:System.Drawing.Point.Y" /> values tested.</returns>
			// Token: 0x1700173B RID: 5947
			// (get) Token: 0x06006B00 RID: 27392 RVA: 0x0018BCB6 File Offset: 0x00189EB6
			public Point Point
			{
				get
				{
					return this.point;
				}
			}

			/// <summary>Gets the <see cref="T:System.Windows.Forms.MonthCalendar.HitArea" /> that represents the area of the calendar evaluated by the hit-test operation.</summary>
			/// <returns>One of the <see cref="T:System.Windows.Forms.MonthCalendar.HitArea" /> values. The default is <see cref="F:System.Windows.Forms.MonthCalendar.HitArea.Nowhere" />.</returns>
			// Token: 0x1700173C RID: 5948
			// (get) Token: 0x06006B01 RID: 27393 RVA: 0x0018BCBE File Offset: 0x00189EBE
			public MonthCalendar.HitArea HitArea
			{
				get
				{
					return this.hitArea;
				}
			}

			/// <summary>Gets the time information specific to the location that was hit-tested.</summary>
			/// <returns>The time information specific to the location that was hit-tested.</returns>
			// Token: 0x1700173D RID: 5949
			// (get) Token: 0x06006B02 RID: 27394 RVA: 0x0018BCC6 File Offset: 0x00189EC6
			public DateTime Time
			{
				get
				{
					return this.time;
				}
			}

			// Token: 0x06006B03 RID: 27395 RVA: 0x0018BCCE File Offset: 0x00189ECE
			internal static bool HitAreaHasValidDateTime(MonthCalendar.HitArea hitArea)
			{
				return hitArea == MonthCalendar.HitArea.Date || hitArea == MonthCalendar.HitArea.WeekNumbers;
			}

			// Token: 0x04003B5D RID: 15197
			private readonly Point point;

			// Token: 0x04003B5E RID: 15198
			private readonly MonthCalendar.HitArea hitArea;

			// Token: 0x04003B5F RID: 15199
			private readonly DateTime time;
		}

		/// <summary>Defines constants that represent areas in a <see cref="T:System.Windows.Forms.MonthCalendar" /> control.</summary>
		// Token: 0x020006DD RID: 1757
		public enum HitArea
		{
			/// <summary>The specified point is either not on the month calendar control, or it is in an inactive portion of the control.</summary>
			// Token: 0x04003B61 RID: 15201
			Nowhere,
			/// <summary>The specified point is over the background of a month's title.</summary>
			// Token: 0x04003B62 RID: 15202
			TitleBackground,
			/// <summary>The specified point is in a month's title bar, over a month name.</summary>
			// Token: 0x04003B63 RID: 15203
			TitleMonth,
			/// <summary>The specified point is in a month's title bar, over the year value.</summary>
			// Token: 0x04003B64 RID: 15204
			TitleYear,
			/// <summary>The specified point is over the button at the upper-right corner of the control. If the user clicks here, the month calendar scrolls its display to the next month or set of months.</summary>
			// Token: 0x04003B65 RID: 15205
			NextMonthButton,
			/// <summary>The specified point is over the button at the upper-left corner of the control. If the user clicks here, the month calendar scrolls its display to the previous month or set of months.</summary>
			// Token: 0x04003B66 RID: 15206
			PrevMonthButton,
			/// <summary>The specified point is part of the calendar's background.</summary>
			// Token: 0x04003B67 RID: 15207
			CalendarBackground,
			/// <summary>The specified point is on a date within the calendar. The <see cref="P:System.Windows.Forms.MonthCalendar.HitTestInfo.Time" /> property of <see cref="T:System.Windows.Forms.MonthCalendar.HitTestInfo" /> is set to the date at the specified point.</summary>
			// Token: 0x04003B68 RID: 15208
			Date,
			/// <summary>The specified point is over a date from the next month (partially displayed at the top of the currently displayed month). If the user clicks here, the month calendar scrolls its display to the next month or set of months.</summary>
			// Token: 0x04003B69 RID: 15209
			NextMonthDate,
			/// <summary>The specified point is over a date from the previous month (partially displayed at the top of the currently displayed month). If the user clicks here, the month calendar scrolls its display to the previous month or set of months.</summary>
			// Token: 0x04003B6A RID: 15210
			PrevMonthDate,
			/// <summary>The specified point is over a day abbreviation ("Fri", for example). The <see cref="P:System.Windows.Forms.MonthCalendar.HitTestInfo.Time" /> property of <see cref="T:System.Windows.Forms.MonthCalendar.HitTestInfo" /> is set to January 1, 0001.</summary>
			// Token: 0x04003B6B RID: 15211
			DayOfWeek,
			/// <summary>The specified point is over a week number. This occurs only if the <see cref="P:System.Windows.Forms.MonthCalendar.ShowWeekNumbers" /> property of <see cref="T:System.Windows.Forms.MonthCalendar" /> is enabled. The <see cref="P:System.Windows.Forms.MonthCalendar.HitTestInfo.Time" /> property of <see cref="T:System.Windows.Forms.MonthCalendar.HitTestInfo" /> is set to the corresponding date in the leftmost column.</summary>
			// Token: 0x04003B6C RID: 15212
			WeekNumbers,
			/// <summary>The specified point is on the today link at the bottom of the month calendar control.</summary>
			// Token: 0x04003B6D RID: 15213
			TodayLink
		}

		// Token: 0x020006DE RID: 1758
		[ComVisible(true)]
		internal class MonthCalendarAccessibleObject : Control.ControlAccessibleObject
		{
			// Token: 0x06006B04 RID: 27396 RVA: 0x0018BCDC File Offset: 0x00189EDC
			public MonthCalendarAccessibleObject(Control owner)
				: base(owner)
			{
				this.calendar = owner as MonthCalendar;
			}

			// Token: 0x1700173E RID: 5950
			// (get) Token: 0x06006B05 RID: 27397 RVA: 0x0018BCF4 File Offset: 0x00189EF4
			public override AccessibleRole Role
			{
				get
				{
					if (this.calendar != null)
					{
						AccessibleRole accessibleRole = this.calendar.AccessibleRole;
						if (accessibleRole != AccessibleRole.Default)
						{
							return accessibleRole;
						}
					}
					return AccessibleRole.Table;
				}
			}

			// Token: 0x1700173F RID: 5951
			// (get) Token: 0x06006B06 RID: 27398 RVA: 0x0018BD20 File Offset: 0x00189F20
			public override string Help
			{
				get
				{
					string help = base.Help;
					if (help != null)
					{
						return help;
					}
					if (this.calendar != null)
					{
						return this.calendar.GetType().Name + "(" + this.calendar.GetType().BaseType.Name + ")";
					}
					return string.Empty;
				}
			}

			// Token: 0x17001740 RID: 5952
			// (get) Token: 0x06006B07 RID: 27399 RVA: 0x0018BD7C File Offset: 0x00189F7C
			public override string Name
			{
				get
				{
					string text = base.Name;
					if (text != null)
					{
						return text;
					}
					if (this.calendar != null)
					{
						if (this.calendar.mcCurView == NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_MONTH)
						{
							if (DateTime.Equals(this.calendar.SelectionStart.Date, this.calendar.SelectionEnd.Date))
							{
								text = SR.GetString("MonthCalendarSingleDateSelected", new object[] { this.calendar.SelectionStart.ToLongDateString() });
							}
							else
							{
								text = SR.GetString("MonthCalendarRangeSelected", new object[]
								{
									this.calendar.SelectionStart.ToLongDateString(),
									this.calendar.SelectionEnd.ToLongDateString()
								});
							}
						}
						else if (this.calendar.mcCurView == NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_YEAR)
						{
							if (object.Equals(this.calendar.SelectionStart.Month, this.calendar.SelectionEnd.Month))
							{
								text = SR.GetString("MonthCalendarSingleDateSelected", new object[] { this.calendar.SelectionStart.ToString("y") });
							}
							else
							{
								text = SR.GetString("MonthCalendarRangeSelected", new object[]
								{
									this.calendar.SelectionStart.ToString("y"),
									this.calendar.SelectionEnd.ToString("y")
								});
							}
						}
						else if (this.calendar.mcCurView == NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_DECADE)
						{
							if (object.Equals(this.calendar.SelectionStart.Year, this.calendar.SelectionEnd.Year))
							{
								text = SR.GetString("MonthCalendarSingleYearSelected", new object[] { this.calendar.SelectionStart.ToString("yyyy") });
							}
							else
							{
								text = SR.GetString("MonthCalendarYearRangeSelected", new object[]
								{
									this.calendar.SelectionStart.ToString("yyyy"),
									this.calendar.SelectionEnd.ToString("yyyy")
								});
							}
						}
						else if (this.calendar.mcCurView == NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_CENTURY)
						{
							text = SR.GetString("MonthCalendarSingleDecadeSelected", new object[] { this.calendar.SelectionStart.ToString("yyyy") });
						}
					}
					return text;
				}
			}

			// Token: 0x17001741 RID: 5953
			// (get) Token: 0x06006B08 RID: 27400 RVA: 0x0018C010 File Offset: 0x0018A210
			// (set) Token: 0x06006B09 RID: 27401 RVA: 0x0018C1BC File Offset: 0x0018A3BC
			public override string Value
			{
				get
				{
					string text = string.Empty;
					try
					{
						if (this.calendar != null)
						{
							if (this.calendar.mcCurView == NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_MONTH)
							{
								if (DateTime.Equals(this.calendar.SelectionStart.Date, this.calendar.SelectionEnd.Date))
								{
									text = this.calendar.SelectionStart.ToLongDateString();
								}
								else
								{
									text = string.Format("{0} - {1}", this.calendar.SelectionStart.ToLongDateString(), this.calendar.SelectionEnd.ToLongDateString());
								}
							}
							else if (this.calendar.mcCurView == NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_YEAR)
							{
								if (object.Equals(this.calendar.SelectionStart.Month, this.calendar.SelectionEnd.Month))
								{
									text = this.calendar.SelectionStart.ToString("y");
								}
								else
								{
									text = string.Format("{0} - {1}", this.calendar.SelectionStart.ToString("y"), this.calendar.SelectionEnd.ToString("y"));
								}
							}
							else
							{
								text = string.Format("{0} - {1}", this.calendar.SelectionRange.Start.ToString(), this.calendar.SelectionRange.End.ToString());
							}
						}
					}
					catch
					{
						text = base.Value;
					}
					return text;
				}
				set
				{
					base.Value = value;
				}
			}

			// Token: 0x04003B6E RID: 15214
			protected MonthCalendar calendar;
		}

		// Token: 0x020006DF RID: 1759
		internal class CalendarAccessibleObject : MonthCalendar.MonthCalendarChildAccessibleObject
		{
			// Token: 0x06006B0A RID: 27402 RVA: 0x0018C1C5 File Offset: 0x0018A3C5
			public CalendarAccessibleObject(MonthCalendar.MonthCalendarAccessibleObjectLevel5 calendarAccessibleObject, int calendarIndex, string initName)
				: base(calendarAccessibleObject)
			{
				this._monthCalendarAccessibleObject = calendarAccessibleObject;
				this._calendarIndex = calendarIndex;
				this._initName = initName;
			}

			// Token: 0x06006B0B RID: 27403 RVA: 0x0018C1E4 File Offset: 0x0018A3E4
			internal void DisconnectChildren()
			{
				int num = UnsafeNativeMethods.UiaDisconnectProvider(this._calendarHeaderAccessibleObject);
				if (this._calendarBodyAccessibleObject != null)
				{
					this._calendarBodyAccessibleObject.DisconnectChildren();
					num = UnsafeNativeMethods.UiaDisconnectProvider(this._calendarBodyAccessibleObject);
				}
			}

			// Token: 0x17001742 RID: 5954
			// (get) Token: 0x06006B0C RID: 27404 RVA: 0x0018C21C File Offset: 0x0018A41C
			public override Rectangle Bounds
			{
				get
				{
					return this._monthCalendarAccessibleObject.GetCalendarPartRectangle(4U, this._calendarIndex, 0, 0);
				}
			}

			// Token: 0x17001743 RID: 5955
			// (get) Token: 0x06006B0D RID: 27405 RVA: 0x0018C237 File Offset: 0x0018A437
			internal MonthCalendar.CalendarBodyAccessibleObject CalendarBodyAccessibleObject
			{
				get
				{
					if (this._calendarBodyAccessibleObject == null)
					{
						this._calendarBodyAccessibleObject = new MonthCalendar.CalendarBodyAccessibleObject(this, this._monthCalendarAccessibleObject, this._calendarIndex);
					}
					return this._calendarBodyAccessibleObject;
				}
			}

			// Token: 0x17001744 RID: 5956
			// (get) Token: 0x06006B0E RID: 27406 RVA: 0x0018C25F File Offset: 0x0018A45F
			internal MonthCalendar.CalendarHeaderAccessibleObject CalendarHeaderAccessibleObject
			{
				get
				{
					if (this._calendarHeaderAccessibleObject == null)
					{
						this._calendarHeaderAccessibleObject = new MonthCalendar.CalendarHeaderAccessibleObject(this, this._monthCalendarAccessibleObject, this._calendarIndex);
					}
					return this._calendarHeaderAccessibleObject;
				}
			}

			// Token: 0x17001745 RID: 5957
			// (get) Token: 0x06006B0F RID: 27407 RVA: 0x0018C287 File Offset: 0x0018A487
			internal override int Column
			{
				get
				{
					if (!this._monthCalendarAccessibleObject.IsHandleCreated)
					{
						return -1;
					}
					return this._calendarIndex % this._monthCalendarAccessibleObject.ColumnCount;
				}
			}

			// Token: 0x17001746 RID: 5958
			// (get) Token: 0x06006B10 RID: 27408 RVA: 0x0018C2AA File Offset: 0x0018A4AA
			internal override UnsafeNativeMethods.IRawElementProviderSimple ContainingGrid
			{
				get
				{
					return this._monthCalendarAccessibleObject;
				}
			}

			// Token: 0x17001747 RID: 5959
			// (get) Token: 0x06006B11 RID: 27409 RVA: 0x0018C2B4 File Offset: 0x0018A4B4
			internal SelectionRange DateRange
			{
				get
				{
					if (this._dateRange == null && this._monthCalendarAccessibleObject.IsHandleCreated)
					{
						SelectionRange calendarPartDateRange = this._monthCalendarAccessibleObject.GetCalendarPartDateRange(4U, this._calendarIndex, 0, 0);
						if (calendarPartDateRange == null)
						{
							return null;
						}
						SelectionRange displayRange = this._monthCalendarAccessibleObject.GetDisplayRange(false);
						if (displayRange == null)
						{
							return null;
						}
						if (this._calendarIndex == 0 && displayRange.Start < calendarPartDateRange.Start)
						{
							calendarPartDateRange.Start = displayRange.Start;
						}
						LinkedList<MonthCalendar.CalendarAccessibleObject> calendarsAccessibleObjects = this._monthCalendarAccessibleObject.CalendarsAccessibleObjects;
						MonthCalendar.CalendarAccessibleObject calendarAccessibleObject;
						if (calendarsAccessibleObjects == null)
						{
							calendarAccessibleObject = null;
						}
						else
						{
							LinkedListNode<MonthCalendar.CalendarAccessibleObject> last = calendarsAccessibleObjects.Last;
							calendarAccessibleObject = ((last != null) ? last.Value : null);
						}
						if (calendarAccessibleObject == this && displayRange.End > calendarPartDateRange.End)
						{
							calendarPartDateRange.End = displayRange.End;
						}
						this._dateRange = calendarPartDateRange;
					}
					return this._dateRange;
				}
			}

			// Token: 0x06006B12 RID: 27410 RVA: 0x0018C384 File Offset: 0x0018A584
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				switch (direction)
				{
				case UnsafeNativeMethods.NavigateDirection.NextSibling:
				{
					LinkedList<MonthCalendar.CalendarAccessibleObject> calendarsAccessibleObjects = this._monthCalendarAccessibleObject.CalendarsAccessibleObjects;
					MonthCalendar.CalendarTodayLinkAccessibleObject calendarTodayLinkAccessibleObject;
					if (calendarsAccessibleObjects == null)
					{
						calendarTodayLinkAccessibleObject = null;
					}
					else
					{
						LinkedListNode<MonthCalendar.CalendarAccessibleObject> linkedListNode = calendarsAccessibleObjects.Find(this);
						if (linkedListNode == null)
						{
							calendarTodayLinkAccessibleObject = null;
						}
						else
						{
							LinkedListNode<MonthCalendar.CalendarAccessibleObject> next = linkedListNode.Next;
							calendarTodayLinkAccessibleObject = ((next != null) ? next.Value : null);
						}
					}
					MonthCalendar.CalendarTodayLinkAccessibleObject calendarTodayLinkAccessibleObject2;
					if ((calendarTodayLinkAccessibleObject2 = calendarTodayLinkAccessibleObject) == null)
					{
						if (!this._monthCalendarAccessibleObject.ShowToday)
						{
							return null;
						}
						calendarTodayLinkAccessibleObject2 = this._monthCalendarAccessibleObject.TodayLinkAccessibleObject;
					}
					return calendarTodayLinkAccessibleObject2;
				}
				case UnsafeNativeMethods.NavigateDirection.PreviousSibling:
				{
					if (this._calendarIndex == 0)
					{
						return this._monthCalendarAccessibleObject.NextButtonAccessibleObject;
					}
					LinkedList<MonthCalendar.CalendarAccessibleObject> calendarsAccessibleObjects2 = this._monthCalendarAccessibleObject.CalendarsAccessibleObjects;
					if (calendarsAccessibleObjects2 == null)
					{
						return null;
					}
					LinkedListNode<MonthCalendar.CalendarAccessibleObject> linkedListNode2 = calendarsAccessibleObjects2.Find(this);
					if (linkedListNode2 == null)
					{
						return null;
					}
					LinkedListNode<MonthCalendar.CalendarAccessibleObject> previous = linkedListNode2.Previous;
					if (previous == null)
					{
						return null;
					}
					return previous.Value;
				}
				case UnsafeNativeMethods.NavigateDirection.FirstChild:
					return this.CalendarHeaderAccessibleObject;
				case UnsafeNativeMethods.NavigateDirection.LastChild:
					return this.CalendarBodyAccessibleObject;
				default:
					return base.FragmentNavigate(direction);
				}
			}

			// Token: 0x06006B13 RID: 27411 RVA: 0x0018C454 File Offset: 0x0018A654
			internal override int GetChildId()
			{
				return 3 + this._calendarIndex;
			}

			// Token: 0x06006B14 RID: 27412 RVA: 0x00015C90 File Offset: 0x00013E90
			internal override UnsafeNativeMethods.IRawElementProviderSimple[] GetColumnHeaderItems()
			{
				return null;
			}

			// Token: 0x06006B15 RID: 27413 RVA: 0x0018C460 File Offset: 0x0018A660
			internal MonthCalendar.MonthCalendarChildAccessibleObject GetChildFromPoint(NativeMethods.MCHITTESTINFOLEVEL5 hitTestInfo)
			{
				if (!this._monthCalendarAccessibleObject.IsHandleCreated || this.CalendarBodyAccessibleObject.RowsAccessibleObjects == null)
				{
					return this;
				}
				MonthCalendar.CalendarRowAccessibleObject calendarRowAccessibleObject = null;
				foreach (MonthCalendar.CalendarRowAccessibleObject calendarRowAccessibleObject2 in this.CalendarBodyAccessibleObject.RowsAccessibleObjects)
				{
					if (calendarRowAccessibleObject2.Row == hitTestInfo.iRow)
					{
						calendarRowAccessibleObject = calendarRowAccessibleObject2;
						break;
					}
				}
				if (calendarRowAccessibleObject == null)
				{
					return this;
				}
				if (hitTestInfo.uHit == 131075)
				{
					return calendarRowAccessibleObject.WeekNumberCellAccessibleObject ?? this;
				}
				if (calendarRowAccessibleObject.CellsAccessibleObjects == null)
				{
					return this;
				}
				MonthCalendar.CalendarCellAccessibleObject calendarCellAccessibleObject = null;
				foreach (MonthCalendar.CalendarCellAccessibleObject calendarCellAccessibleObject2 in calendarRowAccessibleObject.CellsAccessibleObjects)
				{
					if (calendarCellAccessibleObject2.Column == hitTestInfo.iCol)
					{
						calendarCellAccessibleObject = calendarCellAccessibleObject2;
						break;
					}
				}
				if (calendarCellAccessibleObject == null)
				{
					return this;
				}
				return calendarCellAccessibleObject;
			}

			// Token: 0x06006B16 RID: 27414 RVA: 0x0018C564 File Offset: 0x0018A764
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30003)
				{
					return 50033;
				}
				if (propertyID != 30009)
				{
					return base.GetPropertyValue(propertyID);
				}
				return this.IsEnabled;
			}

			// Token: 0x06006B17 RID: 27415 RVA: 0x00015C90 File Offset: 0x00013E90
			internal override UnsafeNativeMethods.IRawElementProviderSimple[] GetRowHeaderItems()
			{
				return null;
			}

			// Token: 0x17001748 RID: 5960
			// (get) Token: 0x06006B18 RID: 27416 RVA: 0x0018C598 File Offset: 0x0018A798
			internal override bool HasKeyboardFocus
			{
				get
				{
					if (this._monthCalendarAccessibleObject.Focused)
					{
						MonthCalendar.CalendarCellAccessibleObject focusedCell = this._monthCalendarAccessibleObject.FocusedCell;
						int? num = ((focusedCell != null) ? new int?(focusedCell.CalendarIndex) : null);
						int calendarIndex = this._calendarIndex;
						return (num.GetValueOrDefault() == calendarIndex) & (num != null);
					}
					return false;
				}
			}

			// Token: 0x06006B19 RID: 27417 RVA: 0x0018C5F2 File Offset: 0x0018A7F2
			internal override bool IsPatternSupported(int patternId)
			{
				return patternId == 10007 || patternId == 10013 || base.IsPatternSupported(patternId);
			}

			// Token: 0x17001749 RID: 5961
			// (get) Token: 0x06006B1A RID: 27418 RVA: 0x0018C611 File Offset: 0x0018A811
			public override string Name
			{
				get
				{
					return this._initName;
				}
			}

			// Token: 0x1700174A RID: 5962
			// (get) Token: 0x06006B1B RID: 27419 RVA: 0x0018C2AA File Offset: 0x0018A4AA
			public override AccessibleObject Parent
			{
				get
				{
					return this._monthCalendarAccessibleObject;
				}
			}

			// Token: 0x1700174B RID: 5963
			// (get) Token: 0x06006B1C RID: 27420 RVA: 0x00159254 File Offset: 0x00157454
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.Client;
				}
			}

			// Token: 0x1700174C RID: 5964
			// (get) Token: 0x06006B1D RID: 27421 RVA: 0x0018C619 File Offset: 0x0018A819
			internal override int Row
			{
				get
				{
					if (!this._monthCalendarAccessibleObject.IsHandleCreated)
					{
						return -1;
					}
					return this._calendarIndex / this._monthCalendarAccessibleObject.ColumnCount;
				}
			}

			// Token: 0x06006B1E RID: 27422 RVA: 0x0018C63C File Offset: 0x0018A83C
			internal override void SetFocus()
			{
				MonthCalendar.CalendarCellAccessibleObject focusedCell = this._monthCalendarAccessibleObject.FocusedCell;
				int? num = ((focusedCell != null) ? new int?(focusedCell.CalendarIndex) : null);
				int calendarIndex = this._calendarIndex;
				if ((num.GetValueOrDefault() == calendarIndex) & (num != null))
				{
					focusedCell.RaiseAutomationEvent(20005);
				}
			}

			// Token: 0x1700174D RID: 5965
			// (get) Token: 0x06006B1F RID: 27423 RVA: 0x0018C698 File Offset: 0x0018A898
			public override AccessibleStates State
			{
				get
				{
					if (!this.IsEnabled)
					{
						return AccessibleStates.None;
					}
					AccessibleStates accessibleStates = AccessibleStates.Focusable | AccessibleStates.Selectable;
					if (this.HasKeyboardFocus)
					{
						accessibleStates |= AccessibleStates.Selected | AccessibleStates.Focused;
					}
					return accessibleStates;
				}
			}

			// Token: 0x04003B6F RID: 15215
			private const int ChildIdIncrement = 3;

			// Token: 0x04003B70 RID: 15216
			private readonly MonthCalendar.MonthCalendarAccessibleObjectLevel5 _monthCalendarAccessibleObject;

			// Token: 0x04003B71 RID: 15217
			private readonly int _calendarIndex;

			// Token: 0x04003B72 RID: 15218
			private readonly string _initName;

			// Token: 0x04003B73 RID: 15219
			private MonthCalendar.CalendarBodyAccessibleObject _calendarBodyAccessibleObject;

			// Token: 0x04003B74 RID: 15220
			private MonthCalendar.CalendarHeaderAccessibleObject _calendarHeaderAccessibleObject;

			// Token: 0x04003B75 RID: 15221
			private SelectionRange _dateRange;
		}

		// Token: 0x020006E0 RID: 1760
		internal class CalendarBodyAccessibleObject : MonthCalendar.MonthCalendarChildAccessibleObject
		{
			// Token: 0x06006B20 RID: 27424 RVA: 0x0018C6C4 File Offset: 0x0018A8C4
			public CalendarBodyAccessibleObject(MonthCalendar.CalendarAccessibleObject calendarAccessibleObject, MonthCalendar.MonthCalendarAccessibleObjectLevel5 monthCalendarAccessibleObject, int calendarIndex)
				: base(monthCalendarAccessibleObject)
			{
				this._calendarAccessibleObject = calendarAccessibleObject;
				this._monthCalendarAccessibleObject = monthCalendarAccessibleObject;
				this._calendarIndex = calendarIndex;
				this._initName = this._monthCalendarAccessibleObject.GetCalendarPartText(5U, this._calendarIndex, 0, 0);
				this._initRuntimeId = new int[]
				{
					this._calendarAccessibleObject.RuntimeId[0],
					this._calendarAccessibleObject.RuntimeId[1],
					this._calendarAccessibleObject.RuntimeId[2],
					this.GetChildId()
				};
			}

			// Token: 0x1700174E RID: 5966
			// (get) Token: 0x06006B21 RID: 27425 RVA: 0x0018C74C File Offset: 0x0018A94C
			public override Rectangle Bounds
			{
				get
				{
					return this._monthCalendarAccessibleObject.GetCalendarPartRectangle(6U, this._calendarIndex, 0, 0);
				}
			}

			// Token: 0x06006B22 RID: 27426 RVA: 0x0018C768 File Offset: 0x0018A968
			internal void DisconnectChildren()
			{
				if (this._rowsAccessibleObjects == null)
				{
					return;
				}
				foreach (MonthCalendar.CalendarRowAccessibleObject calendarRowAccessibleObject in this._rowsAccessibleObjects)
				{
					calendarRowAccessibleObject.DisconnectChildren();
					int num = UnsafeNativeMethods.UiaDisconnectProvider(calendarRowAccessibleObject);
				}
			}

			// Token: 0x06006B23 RID: 27427 RVA: 0x0018C7CC File Offset: 0x0018A9CC
			internal void ClearChildCollection()
			{
				if (this.RowsAccessibleObjects != null)
				{
					foreach (MonthCalendar.CalendarRowAccessibleObject calendarRowAccessibleObject in this.RowsAccessibleObjects)
					{
						calendarRowAccessibleObject.ClearChildCollection();
					}
				}
				this._rowsAccessibleObjects = null;
			}

			// Token: 0x1700174F RID: 5967
			// (get) Token: 0x06006B24 RID: 27428 RVA: 0x0018C830 File Offset: 0x0018AA30
			internal override int ColumnCount
			{
				get
				{
					if (this._monthCalendarAccessibleObject.CalendarView != NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_MONTH)
					{
						return 4;
					}
					return 7;
				}
			}

			// Token: 0x06006B25 RID: 27429 RVA: 0x0018C844 File Offset: 0x0018AA44
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				switch (direction)
				{
				case UnsafeNativeMethods.NavigateDirection.NextSibling:
					return null;
				case UnsafeNativeMethods.NavigateDirection.PreviousSibling:
					return this._calendarAccessibleObject.CalendarHeaderAccessibleObject;
				case UnsafeNativeMethods.NavigateDirection.FirstChild:
				{
					LinkedList<MonthCalendar.CalendarRowAccessibleObject> rowsAccessibleObjects = this.RowsAccessibleObjects;
					if (rowsAccessibleObjects == null)
					{
						return null;
					}
					LinkedListNode<MonthCalendar.CalendarRowAccessibleObject> first = rowsAccessibleObjects.First;
					if (first == null)
					{
						return null;
					}
					return first.Value;
				}
				case UnsafeNativeMethods.NavigateDirection.LastChild:
				{
					LinkedList<MonthCalendar.CalendarRowAccessibleObject> rowsAccessibleObjects2 = this.RowsAccessibleObjects;
					if (rowsAccessibleObjects2 == null)
					{
						return null;
					}
					LinkedListNode<MonthCalendar.CalendarRowAccessibleObject> last = rowsAccessibleObjects2.Last;
					if (last == null)
					{
						return null;
					}
					return last.Value;
				}
				default:
					return base.FragmentNavigate(direction);
				}
			}

			// Token: 0x06006B26 RID: 27430 RVA: 0x00016041 File Offset: 0x00014241
			internal override int GetChildId()
			{
				return 2;
			}

			// Token: 0x06006B27 RID: 27431 RVA: 0x0018C8BC File Offset: 0x0018AABC
			internal override UnsafeNativeMethods.IRawElementProviderSimple[] GetColumnHeaders()
			{
				if (this._monthCalendarAccessibleObject.CalendarView != NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_MONTH)
				{
					return null;
				}
				LinkedList<MonthCalendar.CalendarRowAccessibleObject> rowsAccessibleObjects = this.RowsAccessibleObjects;
				LinkedList<MonthCalendar.CalendarCellAccessibleObject> linkedList;
				if (rowsAccessibleObjects == null)
				{
					linkedList = null;
				}
				else
				{
					LinkedListNode<MonthCalendar.CalendarRowAccessibleObject> first = rowsAccessibleObjects.First;
					linkedList = ((first != null) ? first.Value.CellsAccessibleObjects : null);
				}
				LinkedList<MonthCalendar.CalendarCellAccessibleObject> linkedList2 = linkedList;
				if (linkedList2 == null)
				{
					return null;
				}
				UnsafeNativeMethods.IRawElementProviderSimple[] array2;
				try
				{
					UnsafeNativeMethods.IRawElementProviderSimple[] array = new UnsafeNativeMethods.IRawElementProviderSimple[linkedList2.Count];
					int num = 0;
					foreach (MonthCalendar.CalendarCellAccessibleObject calendarCellAccessibleObject in linkedList2)
					{
						array[num++] = calendarCellAccessibleObject;
					}
					array2 = array;
				}
				catch (Exception ex)
				{
					throw ex;
				}
				return array2;
			}

			// Token: 0x06006B28 RID: 27432 RVA: 0x0018C96C File Offset: 0x0018AB6C
			internal override UnsafeNativeMethods.IRawElementProviderSimple GetItem(int rowIndex, int columnIndex)
			{
				if (!this._monthCalendarAccessibleObject.IsHandleCreated || this.RowsAccessibleObjects == null)
				{
					return null;
				}
				MonthCalendar.CalendarRowAccessibleObject calendarRowAccessibleObject = null;
				foreach (MonthCalendar.CalendarRowAccessibleObject calendarRowAccessibleObject2 in this.RowsAccessibleObjects)
				{
					if (calendarRowAccessibleObject2.Row == rowIndex)
					{
						calendarRowAccessibleObject = calendarRowAccessibleObject2;
						break;
					}
				}
				if (calendarRowAccessibleObject == null)
				{
					return null;
				}
				if (rowIndex >= 0 && columnIndex == -1)
				{
					return calendarRowAccessibleObject.WeekNumberCellAccessibleObject;
				}
				if (calendarRowAccessibleObject.CellsAccessibleObjects == null)
				{
					return null;
				}
				foreach (MonthCalendar.CalendarCellAccessibleObject calendarCellAccessibleObject in calendarRowAccessibleObject.CellsAccessibleObjects)
				{
					if (calendarCellAccessibleObject.Column == columnIndex)
					{
						return calendarCellAccessibleObject;
					}
				}
				return null;
			}

			// Token: 0x06006B29 RID: 27433 RVA: 0x0018CA4C File Offset: 0x0018AC4C
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID <= 30009)
				{
					if (propertyID == 30003)
					{
						return 50036;
					}
					if (propertyID == 30009)
					{
						return this.IsEnabled;
					}
				}
				else
				{
					if (propertyID == 30030)
					{
						return this.IsPatternSupported(10006);
					}
					if (propertyID == 30038)
					{
						return this.IsPatternSupported(10012);
					}
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x06006B2A RID: 27434 RVA: 0x0018CAC8 File Offset: 0x0018ACC8
			internal override UnsafeNativeMethods.IRawElementProviderSimple[] GetRowHeaders()
			{
				if (!this._monthCalendarAccessibleObject.IsHandleCreated || !this._monthCalendarAccessibleObject.ShowWeekNumbers || this._monthCalendarAccessibleObject.CalendarView != NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_MONTH || this.RowsAccessibleObjects == null)
				{
					return null;
				}
				List<UnsafeNativeMethods.IRawElementProviderSimple> list = new List<UnsafeNativeMethods.IRawElementProviderSimple>();
				foreach (MonthCalendar.CalendarRowAccessibleObject calendarRowAccessibleObject in this.RowsAccessibleObjects)
				{
					if (calendarRowAccessibleObject.Row != -1)
					{
						if (calendarRowAccessibleObject.WeekNumberCellAccessibleObject == null)
						{
							return null;
						}
						list.Add(calendarRowAccessibleObject.WeekNumberCellAccessibleObject);
					}
				}
				return list.ToArray();
			}

			// Token: 0x17001750 RID: 5968
			// (get) Token: 0x06006B2B RID: 27435 RVA: 0x0018CB74 File Offset: 0x0018AD74
			internal override bool HasKeyboardFocus
			{
				get
				{
					if (this._monthCalendarAccessibleObject.Focused)
					{
						MonthCalendar.CalendarCellAccessibleObject focusedCell = this._monthCalendarAccessibleObject.FocusedCell;
						int? num = ((focusedCell != null) ? new int?(focusedCell.CalendarIndex) : null);
						int calendarIndex = this._calendarIndex;
						return (num.GetValueOrDefault() == calendarIndex) & (num != null);
					}
					return false;
				}
			}

			// Token: 0x06006B2C RID: 27436 RVA: 0x0018CBCE File Offset: 0x0018ADCE
			internal override bool IsPatternSupported(int patternId)
			{
				return patternId == 10006 || patternId == 10012 || base.IsPatternSupported(patternId);
			}

			// Token: 0x17001751 RID: 5969
			// (get) Token: 0x06006B2D RID: 27437 RVA: 0x0018CBED File Offset: 0x0018ADED
			public override string Name
			{
				get
				{
					return this._initName;
				}
			}

			// Token: 0x17001752 RID: 5970
			// (get) Token: 0x06006B2E RID: 27438 RVA: 0x0018CBF5 File Offset: 0x0018ADF5
			public override AccessibleObject Parent
			{
				get
				{
					return this._calendarAccessibleObject;
				}
			}

			// Token: 0x17001753 RID: 5971
			// (get) Token: 0x06006B2F RID: 27439 RVA: 0x0018CBFD File Offset: 0x0018ADFD
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.Table;
				}
			}

			// Token: 0x17001754 RID: 5972
			// (get) Token: 0x06006B30 RID: 27440 RVA: 0x0018CC01 File Offset: 0x0018AE01
			internal override int RowCount
			{
				get
				{
					LinkedList<MonthCalendar.CalendarRowAccessibleObject> rowsAccessibleObjects = this.RowsAccessibleObjects;
					if (rowsAccessibleObjects == null)
					{
						return -1;
					}
					return rowsAccessibleObjects.Count;
				}
			}

			// Token: 0x17001755 RID: 5973
			// (get) Token: 0x06006B31 RID: 27441 RVA: 0x0001180C File Offset: 0x0000FA0C
			internal override UnsafeNativeMethods.RowOrColumnMajor RowOrColumnMajor
			{
				get
				{
					return UnsafeNativeMethods.RowOrColumnMajor.RowOrColumnMajor_RowMajor;
				}
			}

			// Token: 0x17001756 RID: 5974
			// (get) Token: 0x06006B32 RID: 27442 RVA: 0x0018CC14 File Offset: 0x0018AE14
			internal LinkedList<MonthCalendar.CalendarRowAccessibleObject> RowsAccessibleObjects
			{
				get
				{
					if (this._rowsAccessibleObjects == null && this._monthCalendarAccessibleObject.IsHandleCreated)
					{
						this._rowsAccessibleObjects = new LinkedList<MonthCalendar.CalendarRowAccessibleObject>();
						int num = ((this._monthCalendarAccessibleObject.CalendarView == NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_MONTH) ? (-1) : 0);
						int num2 = ((this._monthCalendarAccessibleObject.CalendarView == NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_MONTH) ? 6 : 3);
						for (int i = num; i < num2; i++)
						{
							MonthCalendar.CalendarRowAccessibleObject calendarRowAccessibleObject = new MonthCalendar.CalendarRowAccessibleObject(this, this._monthCalendarAccessibleObject, this._calendarIndex, i);
							LinkedList<MonthCalendar.CalendarCellAccessibleObject> cellsAccessibleObjects = calendarRowAccessibleObject.CellsAccessibleObjects;
							if (cellsAccessibleObjects != null && cellsAccessibleObjects.Count > 0)
							{
								this._rowsAccessibleObjects.AddLast(calendarRowAccessibleObject);
							}
						}
					}
					return this._rowsAccessibleObjects;
				}
			}

			// Token: 0x17001757 RID: 5975
			// (get) Token: 0x06006B33 RID: 27443 RVA: 0x0018CCB2 File Offset: 0x0018AEB2
			internal override int[] RuntimeId
			{
				get
				{
					return this._initRuntimeId;
				}
			}

			// Token: 0x06006B34 RID: 27444 RVA: 0x0018CCBC File Offset: 0x0018AEBC
			internal override void SetFocus()
			{
				MonthCalendar.CalendarCellAccessibleObject focusedCell = this._monthCalendarAccessibleObject.FocusedCell;
				int? num = ((focusedCell != null) ? new int?(focusedCell.CalendarIndex) : null);
				int calendarIndex = this._calendarIndex;
				if ((num.GetValueOrDefault() == calendarIndex) & (num != null))
				{
					focusedCell.RaiseAutomationEvent(20005);
				}
			}

			// Token: 0x17001758 RID: 5976
			// (get) Token: 0x06006B35 RID: 27445 RVA: 0x0018CD16 File Offset: 0x0018AF16
			public override AccessibleStates State
			{
				get
				{
					return AccessibleStates.Default;
				}
			}

			// Token: 0x04003B76 RID: 15222
			private const int ChildId = 2;

			// Token: 0x04003B77 RID: 15223
			private readonly MonthCalendar.CalendarAccessibleObject _calendarAccessibleObject;

			// Token: 0x04003B78 RID: 15224
			private readonly MonthCalendar.MonthCalendarAccessibleObjectLevel5 _monthCalendarAccessibleObject;

			// Token: 0x04003B79 RID: 15225
			private readonly int _calendarIndex;

			// Token: 0x04003B7A RID: 15226
			private readonly string _initName;

			// Token: 0x04003B7B RID: 15227
			private readonly int[] _initRuntimeId;

			// Token: 0x04003B7C RID: 15228
			private LinkedList<MonthCalendar.CalendarRowAccessibleObject> _rowsAccessibleObjects;
		}

		// Token: 0x020006E1 RID: 1761
		internal abstract class CalendarButtonAccessibleObject : MonthCalendar.MonthCalendarChildAccessibleObject
		{
			// Token: 0x06006B36 RID: 27446 RVA: 0x0018CD1D File Offset: 0x0018AF1D
			public CalendarButtonAccessibleObject(MonthCalendar.MonthCalendarAccessibleObjectLevel5 calendarAccessibleObject)
				: base(calendarAccessibleObject)
			{
				this._monthCalendarAccessibleObject = calendarAccessibleObject;
			}

			// Token: 0x17001759 RID: 5977
			// (get) Token: 0x06006B37 RID: 27447 RVA: 0x00185E58 File Offset: 0x00184058
			public override string DefaultAction
			{
				get
				{
					return SR.GetString("AccessibleActionClick");
				}
			}

			// Token: 0x06006B38 RID: 27448 RVA: 0x000161F4 File Offset: 0x000143F4
			public override void DoDefaultAction()
			{
				this.Invoke();
			}

			// Token: 0x06006B39 RID: 27449 RVA: 0x0018CD2D File Offset: 0x0018AF2D
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30003)
				{
					return 50000;
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x06006B3A RID: 27450 RVA: 0x0018CD49 File Offset: 0x0018AF49
			internal override void Invoke()
			{
				this.RaiseMouseClick();
			}

			// Token: 0x06006B3B RID: 27451 RVA: 0x0018CD51 File Offset: 0x0018AF51
			internal override bool IsPatternSupported(int patternId)
			{
				return patternId == 10000 || base.IsPatternSupported(patternId);
			}

			// Token: 0x1700175A RID: 5978
			// (get) Token: 0x06006B3C RID: 27452 RVA: 0x0018CD64 File Offset: 0x0018AF64
			public override AccessibleObject Parent
			{
				get
				{
					return this._monthCalendarAccessibleObject;
				}
			}

			// Token: 0x06006B3D RID: 27453 RVA: 0x0018CD6C File Offset: 0x0018AF6C
			private void RaiseMouseClick()
			{
				if (!this._monthCalendarAccessibleObject.IsHandleCreated || !this._monthCalendarAccessibleObject.IsEnabled || !this.IsEnabled)
				{
					return;
				}
				NativeMethods.RECT rect = this.Bounds;
				int num = rect.left + (rect.right - rect.left) / 2;
				int num2 = rect.top + (rect.bottom - rect.top) / 2;
				this.RaiseMouseClick(num, num2);
			}

			// Token: 0x06006B3E RID: 27454 RVA: 0x0018CDE0 File Offset: 0x0018AFE0
			private void RaiseMouseClick(int x, int y)
			{
				Point point = default(Point);
				UnsafeNativeMethods.BOOL physicalCursorPos = UnsafeNativeMethods.GetPhysicalCursorPos(ref point);
				bool flag = UnsafeNativeMethods.GetSystemMetrics(23) != 0;
				this.SendMouseInput(x, y, 32769U);
				this.SendMouseInput(0, 0, flag ? 8U : 2U);
				this.SendMouseInput(0, 0, flag ? 16U : 4U);
				Thread.Sleep(50);
				if (physicalCursorPos == UnsafeNativeMethods.BOOL.TRUE)
				{
					this.SendMouseInput(point.X, point.Y, 32769U);
				}
			}

			// Token: 0x1700175B RID: 5979
			// (get) Token: 0x06006B3F RID: 27455 RVA: 0x0015EE45 File Offset: 0x0015D045
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.PushButton;
				}
			}

			// Token: 0x06006B40 RID: 27456 RVA: 0x0018CE58 File Offset: 0x0018B058
			private void SendMouseInput(int x, int y, uint flags)
			{
				if ((flags & 32768U) != 0U)
				{
					int systemMetrics = UnsafeNativeMethods.GetSystemMetrics(78);
					int systemMetrics2 = UnsafeNativeMethods.GetSystemMetrics(79);
					int systemMetrics3 = UnsafeNativeMethods.GetSystemMetrics(76);
					int systemMetrics4 = UnsafeNativeMethods.GetSystemMetrics(77);
					x = (x - systemMetrics3) * 65536 / systemMetrics + 65536 / (systemMetrics * 2);
					y = (y - systemMetrics4) * 65536 / systemMetrics2 + 65536 / (systemMetrics2 * 2);
					flags |= 16384U;
				}
				NativeMethods.INPUT input = default(NativeMethods.INPUT);
				input.type = 0;
				input.inputUnion.mi.dx = x;
				input.inputUnion.mi.dy = y;
				input.inputUnion.mi.mouseData = 0;
				input.inputUnion.mi.dwFlags = (int)flags;
				input.inputUnion.mi.time = 0;
				input.inputUnion.mi.dwExtraInfo = IntPtr.Zero;
				UnsafeNativeMethods.SendInput(1U, new NativeMethods.INPUT[] { input }, Marshal.SizeOf(input));
			}

			// Token: 0x04003B7D RID: 15229
			private readonly MonthCalendar.MonthCalendarAccessibleObjectLevel5 _monthCalendarAccessibleObject;
		}

		// Token: 0x020006E2 RID: 1762
		internal class CalendarCellAccessibleObject : MonthCalendar.CalendarButtonAccessibleObject
		{
			// Token: 0x06006B41 RID: 27457 RVA: 0x0018CF68 File Offset: 0x0018B168
			public CalendarCellAccessibleObject(MonthCalendar.CalendarRowAccessibleObject calendarRowAccessibleObject, MonthCalendar.CalendarBodyAccessibleObject calendarBodyAccessibleObject, MonthCalendar.MonthCalendarAccessibleObjectLevel5 monthCalendarAccessibleObject, int calendarIndex, int rowIndex, int columnIndex)
				: base(monthCalendarAccessibleObject)
			{
				this._calendarRowAccessibleObject = calendarRowAccessibleObject;
				this._calendarBodyAccessibleObject = calendarBodyAccessibleObject;
				this._monthCalendarAccessibleObject = monthCalendarAccessibleObject;
				this._calendarIndex = calendarIndex;
				this._rowIndex = rowIndex;
				this._columnIndex = columnIndex;
				this._initRuntimeId = new int[]
				{
					this._calendarRowAccessibleObject.RuntimeId[0],
					this._calendarRowAccessibleObject.RuntimeId[1],
					this._calendarRowAccessibleObject.RuntimeId[2],
					this._calendarRowAccessibleObject.RuntimeId[3],
					this._calendarRowAccessibleObject.RuntimeId[4],
					this.GetChildId()
				};
			}

			// Token: 0x1700175C RID: 5980
			// (get) Token: 0x06006B42 RID: 27458 RVA: 0x0018D00E File Offset: 0x0018B20E
			public override Rectangle Bounds
			{
				get
				{
					return this._monthCalendarAccessibleObject.GetCalendarPartRectangle(8U, this._calendarIndex, this._rowIndex, this._columnIndex);
				}
			}

			// Token: 0x1700175D RID: 5981
			// (get) Token: 0x06006B43 RID: 27459 RVA: 0x0018D033 File Offset: 0x0018B233
			internal int CalendarIndex
			{
				get
				{
					return this._calendarIndex;
				}
			}

			// Token: 0x1700175E RID: 5982
			// (get) Token: 0x06006B44 RID: 27460 RVA: 0x0018D03B File Offset: 0x0018B23B
			internal override int Column
			{
				get
				{
					return this._columnIndex;
				}
			}

			// Token: 0x1700175F RID: 5983
			// (get) Token: 0x06006B45 RID: 27461 RVA: 0x0018D043 File Offset: 0x0018B243
			internal override UnsafeNativeMethods.IRawElementProviderSimple ContainingGrid
			{
				get
				{
					return this._calendarBodyAccessibleObject;
				}
			}

			// Token: 0x17001760 RID: 5984
			// (get) Token: 0x06006B46 RID: 27462 RVA: 0x0018D04B File Offset: 0x0018B24B
			internal virtual SelectionRange DateRange
			{
				get
				{
					if (this._dateRange == null)
					{
						this._dateRange = this._monthCalendarAccessibleObject.GetCalendarPartDateRange(8U, this._calendarIndex, this._rowIndex, this._columnIndex);
					}
					return this._dateRange;
				}
			}

			// Token: 0x17001761 RID: 5985
			// (get) Token: 0x06006B47 RID: 27463 RVA: 0x0018D080 File Offset: 0x0018B280
			public override string Description
			{
				get
				{
					if (!this._monthCalendarAccessibleObject.IsHandleCreated || this._monthCalendarAccessibleObject.CalendarView != NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_MONTH || this.DateRange == null)
					{
						return null;
					}
					DateTime start = this.DateRange.Start;
					CultureInfo currentCulture = CultureInfo.CurrentCulture;
					int weekOfYear = currentCulture.Calendar.GetWeekOfYear(start, currentCulture.DateTimeFormat.CalendarWeekRule, this._monthCalendarAccessibleObject.FirstDayOfWeek);
					return string.Format(SR.GetString("MonthCalendarWeekNumberDescription"), weekOfYear) + ", " + start.ToString("dddd", currentCulture);
				}
			}

			// Token: 0x06006B48 RID: 27464 RVA: 0x0018D114 File Offset: 0x0018B314
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (direction != UnsafeNativeMethods.NavigateDirection.NextSibling)
				{
					if (direction != UnsafeNativeMethods.NavigateDirection.PreviousSibling)
					{
						return base.FragmentNavigate(direction);
					}
					if (this._columnIndex == 0)
					{
						return this._calendarRowAccessibleObject.WeekNumberCellAccessibleObject;
					}
					LinkedList<MonthCalendar.CalendarCellAccessibleObject> cellsAccessibleObjects = this._calendarRowAccessibleObject.CellsAccessibleObjects;
					if (cellsAccessibleObjects == null)
					{
						return null;
					}
					LinkedListNode<MonthCalendar.CalendarCellAccessibleObject> linkedListNode = cellsAccessibleObjects.Find(this);
					if (linkedListNode == null)
					{
						return null;
					}
					LinkedListNode<MonthCalendar.CalendarCellAccessibleObject> previous = linkedListNode.Previous;
					if (previous == null)
					{
						return null;
					}
					return previous.Value;
				}
				else
				{
					LinkedList<MonthCalendar.CalendarCellAccessibleObject> cellsAccessibleObjects2 = this._calendarRowAccessibleObject.CellsAccessibleObjects;
					if (cellsAccessibleObjects2 == null)
					{
						return null;
					}
					LinkedListNode<MonthCalendar.CalendarCellAccessibleObject> linkedListNode2 = cellsAccessibleObjects2.Find(this);
					if (linkedListNode2 == null)
					{
						return null;
					}
					LinkedListNode<MonthCalendar.CalendarCellAccessibleObject> next = linkedListNode2.Next;
					if (next == null)
					{
						return null;
					}
					return next.Value;
				}
			}

			// Token: 0x06006B49 RID: 27465 RVA: 0x0018D1A2 File Offset: 0x0018B3A2
			internal override int GetChildId()
			{
				return 1 + this._columnIndex;
			}

			// Token: 0x06006B4A RID: 27466 RVA: 0x0018D1AC File Offset: 0x0018B3AC
			internal override UnsafeNativeMethods.IRawElementProviderSimple[] GetColumnHeaderItems()
			{
				if (!this._monthCalendarAccessibleObject.IsHandleCreated || this._monthCalendarAccessibleObject.CalendarView != NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_MONTH)
				{
					return null;
				}
				LinkedList<MonthCalendar.CalendarRowAccessibleObject> rowsAccessibleObjects = this._calendarBodyAccessibleObject.RowsAccessibleObjects;
				MonthCalendar.CalendarRowAccessibleObject calendarRowAccessibleObject;
				if (rowsAccessibleObjects == null)
				{
					calendarRowAccessibleObject = null;
				}
				else
				{
					LinkedListNode<MonthCalendar.CalendarRowAccessibleObject> first = rowsAccessibleObjects.First;
					calendarRowAccessibleObject = ((first != null) ? first.Value : null);
				}
				MonthCalendar.CalendarRowAccessibleObject calendarRowAccessibleObject2 = calendarRowAccessibleObject;
				if (calendarRowAccessibleObject2 == null || calendarRowAccessibleObject2.CellsAccessibleObjects == null)
				{
					return null;
				}
				foreach (MonthCalendar.CalendarCellAccessibleObject calendarCellAccessibleObject in calendarRowAccessibleObject2.CellsAccessibleObjects)
				{
					if (calendarCellAccessibleObject.Column == this._columnIndex)
					{
						return new UnsafeNativeMethods.IRawElementProviderSimple[] { calendarCellAccessibleObject };
					}
				}
				return null;
			}

			// Token: 0x06006B4B RID: 27467 RVA: 0x0018D264 File Offset: 0x0018B464
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID <= 30009)
				{
					if (propertyID == 30003)
					{
						return 50029;
					}
					if (propertyID == 30009)
					{
						return this.IsEnabled;
					}
				}
				else
				{
					if (propertyID == 30029)
					{
						return this.IsPatternSupported(10007);
					}
					if (propertyID == 30039)
					{
						return this.IsPatternSupported(10013);
					}
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x06006B4C RID: 27468 RVA: 0x0018D2E0 File Offset: 0x0018B4E0
			internal override UnsafeNativeMethods.IRawElementProviderSimple[] GetRowHeaderItems()
			{
				AccessibleObject weekNumberCellAccessibleObject = this._calendarRowAccessibleObject.WeekNumberCellAccessibleObject;
				if (weekNumberCellAccessibleObject == null)
				{
					return null;
				}
				return new UnsafeNativeMethods.IRawElementProviderSimple[] { weekNumberCellAccessibleObject };
			}

			// Token: 0x17001762 RID: 5986
			// (get) Token: 0x06006B4D RID: 27469 RVA: 0x0018D308 File Offset: 0x0018B508
			internal override bool HasKeyboardFocus
			{
				get
				{
					return this._monthCalendarAccessibleObject.Focused && this._monthCalendarAccessibleObject.FocusedCell == this;
				}
			}

			// Token: 0x06006B4E RID: 27470 RVA: 0x0018D327 File Offset: 0x0018B527
			internal override bool IsPatternSupported(int patternId)
			{
				return patternId == 10007 || patternId == 10013 || base.IsPatternSupported(patternId);
			}

			// Token: 0x17001763 RID: 5987
			// (get) Token: 0x06006B4F RID: 27471 RVA: 0x0018D348 File Offset: 0x0018B548
			public override string Name
			{
				get
				{
					if (this.DateRange == null)
					{
						return string.Empty;
					}
					switch (this._monthCalendarAccessibleObject.CalendarView)
					{
					case NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_MONTH:
						return string.Format("{0:D}", this.DateRange.Start);
					case NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_YEAR:
						return string.Format("{0:Y}", this.DateRange.Start);
					case NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_DECADE:
						return string.Format("{0:yyy}", this.DateRange.Start);
					case NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_CENTURY:
						return string.Format("{0:yyy} - {1:yyy}", this.DateRange.Start, this.DateRange.End);
					default:
						return string.Empty;
					}
				}
			}

			// Token: 0x17001764 RID: 5988
			// (get) Token: 0x06006B50 RID: 27472 RVA: 0x0018D408 File Offset: 0x0018B608
			public override AccessibleObject Parent
			{
				get
				{
					return this._calendarRowAccessibleObject;
				}
			}

			// Token: 0x17001765 RID: 5989
			// (get) Token: 0x06006B51 RID: 27473 RVA: 0x00178038 File Offset: 0x00176238
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.Cell;
				}
			}

			// Token: 0x17001766 RID: 5990
			// (get) Token: 0x06006B52 RID: 27474 RVA: 0x0018D410 File Offset: 0x0018B610
			internal override int Row
			{
				get
				{
					return this._rowIndex;
				}
			}

			// Token: 0x17001767 RID: 5991
			// (get) Token: 0x06006B53 RID: 27475 RVA: 0x0018D418 File Offset: 0x0018B618
			internal override int[] RuntimeId
			{
				get
				{
					return this._initRuntimeId;
				}
			}

			// Token: 0x06006B54 RID: 27476 RVA: 0x0018D420 File Offset: 0x0018B620
			public override void Select(AccessibleSelection flags)
			{
				if (this.DateRange != null)
				{
					this._monthCalendarAccessibleObject.SetSelectionRange(this.DateRange.Start, this.DateRange.End);
				}
			}

			// Token: 0x17001768 RID: 5992
			// (get) Token: 0x06006B55 RID: 27477 RVA: 0x0018D44C File Offset: 0x0018B64C
			public override AccessibleStates State
			{
				get
				{
					AccessibleStates accessibleStates = AccessibleStates.Focusable | AccessibleStates.Selectable;
					if (this._monthCalendarAccessibleObject.Focused && this._monthCalendarAccessibleObject.FocusedCell == this)
					{
						return accessibleStates | AccessibleStates.Focused | AccessibleStates.Selected;
					}
					if (this.DateRange != null && this._monthCalendarAccessibleObject.CalendarView == NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_MONTH && this.DateRange.Start >= this._monthCalendarAccessibleObject.SelectionRange.Start && this.DateRange.End <= this._monthCalendarAccessibleObject.SelectionRange.End)
					{
						accessibleStates |= AccessibleStates.Selected;
					}
					return accessibleStates;
				}
			}

			// Token: 0x04003B7E RID: 15230
			private const int ChildIdIncrement = 1;

			// Token: 0x04003B7F RID: 15231
			private readonly MonthCalendar.CalendarRowAccessibleObject _calendarRowAccessibleObject;

			// Token: 0x04003B80 RID: 15232
			private readonly MonthCalendar.CalendarBodyAccessibleObject _calendarBodyAccessibleObject;

			// Token: 0x04003B81 RID: 15233
			private readonly MonthCalendar.MonthCalendarAccessibleObjectLevel5 _monthCalendarAccessibleObject;

			// Token: 0x04003B82 RID: 15234
			private readonly int _calendarIndex;

			// Token: 0x04003B83 RID: 15235
			private readonly int _rowIndex;

			// Token: 0x04003B84 RID: 15236
			private readonly int _columnIndex;

			// Token: 0x04003B85 RID: 15237
			private readonly int[] _initRuntimeId;

			// Token: 0x04003B86 RID: 15238
			private SelectionRange _dateRange;
		}

		// Token: 0x020006E3 RID: 1763
		internal class CalendarDayOfWeekCellAccessibleObject : MonthCalendar.CalendarCellAccessibleObject
		{
			// Token: 0x06006B56 RID: 27478 RVA: 0x0018D4DE File Offset: 0x0018B6DE
			public CalendarDayOfWeekCellAccessibleObject(MonthCalendar.CalendarRowAccessibleObject calendarRowAccessibleObject, MonthCalendar.CalendarBodyAccessibleObject calendarBodyAccessibleObject, MonthCalendar.MonthCalendarAccessibleObjectLevel5 monthCalendarAccessibleObject, int calendarIndex, int rowIndex, int columnIndex, string initName)
				: base(calendarRowAccessibleObject, calendarBodyAccessibleObject, monthCalendarAccessibleObject, calendarIndex, rowIndex, columnIndex)
			{
				this._calendarRowAccessibleObject = calendarRowAccessibleObject;
				this._initName = initName;
			}

			// Token: 0x17001769 RID: 5993
			// (get) Token: 0x06006B57 RID: 27479 RVA: 0x00015C90 File Offset: 0x00013E90
			internal override SelectionRange DateRange
			{
				get
				{
					return null;
				}
			}

			// Token: 0x1700176A RID: 5994
			// (get) Token: 0x06006B58 RID: 27480 RVA: 0x0017E1A1 File Offset: 0x0017C3A1
			public override string DefaultAction
			{
				get
				{
					return string.Empty;
				}
			}

			// Token: 0x1700176B RID: 5995
			// (get) Token: 0x06006B59 RID: 27481 RVA: 0x00015C90 File Offset: 0x00013E90
			public override string Description
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06006B5A RID: 27482 RVA: 0x0018D500 File Offset: 0x0018B700
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (direction != UnsafeNativeMethods.NavigateDirection.NextSibling)
				{
					if (direction != UnsafeNativeMethods.NavigateDirection.PreviousSibling)
					{
						return base.FragmentNavigate(direction);
					}
					LinkedList<MonthCalendar.CalendarCellAccessibleObject> cellsAccessibleObjects = this._calendarRowAccessibleObject.CellsAccessibleObjects;
					if (cellsAccessibleObjects == null)
					{
						return null;
					}
					LinkedListNode<MonthCalendar.CalendarCellAccessibleObject> linkedListNode = cellsAccessibleObjects.Find(this);
					if (linkedListNode == null)
					{
						return null;
					}
					LinkedListNode<MonthCalendar.CalendarCellAccessibleObject> previous = linkedListNode.Previous;
					if (previous == null)
					{
						return null;
					}
					return previous.Value;
				}
				else
				{
					LinkedList<MonthCalendar.CalendarCellAccessibleObject> cellsAccessibleObjects2 = this._calendarRowAccessibleObject.CellsAccessibleObjects;
					if (cellsAccessibleObjects2 == null)
					{
						return null;
					}
					LinkedListNode<MonthCalendar.CalendarCellAccessibleObject> linkedListNode2 = cellsAccessibleObjects2.Find(this);
					if (linkedListNode2 == null)
					{
						return null;
					}
					LinkedListNode<MonthCalendar.CalendarCellAccessibleObject> next = linkedListNode2.Next;
					if (next == null)
					{
						return null;
					}
					return next.Value;
				}
			}

			// Token: 0x06006B5B RID: 27483 RVA: 0x0018D57A File Offset: 0x0018B77A
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30003)
				{
					return 50034;
				}
				if (propertyID != 30009)
				{
					return base.GetPropertyValue(propertyID);
				}
				return false;
			}

			// Token: 0x1700176C RID: 5996
			// (get) Token: 0x06006B5C RID: 27484 RVA: 0x0001180C File Offset: 0x0000FA0C
			internal override bool HasKeyboardFocus
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06006B5D RID: 27485 RVA: 0x000070A6 File Offset: 0x000052A6
			internal override void Invoke()
			{
			}

			// Token: 0x06006B5E RID: 27486 RVA: 0x0018D5A7 File Offset: 0x0018B7A7
			internal override bool IsPatternSupported(int patternId)
			{
				return patternId != 10000 && patternId != 10007 && patternId != 10013 && base.IsPatternSupported(patternId);
			}

			// Token: 0x1700176D RID: 5997
			// (get) Token: 0x06006B5F RID: 27487 RVA: 0x0018D5D0 File Offset: 0x0018B7D0
			public override string Name
			{
				get
				{
					return this._initName;
				}
			}

			// Token: 0x1700176E RID: 5998
			// (get) Token: 0x06006B60 RID: 27488 RVA: 0x00177384 File Offset: 0x00175584
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.ColumnHeader;
				}
			}

			// Token: 0x1700176F RID: 5999
			// (get) Token: 0x06006B61 RID: 27489 RVA: 0x0001180C File Offset: 0x0000FA0C
			public override AccessibleStates State
			{
				get
				{
					return AccessibleStates.None;
				}
			}

			// Token: 0x04003B87 RID: 15239
			private readonly MonthCalendar.CalendarRowAccessibleObject _calendarRowAccessibleObject;

			// Token: 0x04003B88 RID: 15240
			private readonly string _initName;
		}

		// Token: 0x020006E4 RID: 1764
		internal class CalendarHeaderAccessibleObject : MonthCalendar.CalendarButtonAccessibleObject
		{
			// Token: 0x06006B62 RID: 27490 RVA: 0x0018D5D8 File Offset: 0x0018B7D8
			public CalendarHeaderAccessibleObject(MonthCalendar.CalendarAccessibleObject calendarAccessibleObject, MonthCalendar.MonthCalendarAccessibleObjectLevel5 monthCalendarAccessibleObject, int calendarIndex)
				: base(monthCalendarAccessibleObject)
			{
				this._calendarAccessibleObject = calendarAccessibleObject;
				this._monthCalendarAccessibleObject = monthCalendarAccessibleObject;
				this._calendarIndex = calendarIndex;
				this._initName = this._monthCalendarAccessibleObject.GetCalendarPartText(5U, this._calendarIndex, 0, 0);
				this._initRuntimeId = new int[]
				{
					this._calendarAccessibleObject.RuntimeId[0],
					this._calendarAccessibleObject.RuntimeId[1],
					this._calendarAccessibleObject.RuntimeId[2],
					this.GetChildId()
				};
			}

			// Token: 0x17001770 RID: 6000
			// (get) Token: 0x06006B63 RID: 27491 RVA: 0x0018D660 File Offset: 0x0018B860
			public override Rectangle Bounds
			{
				get
				{
					return this._monthCalendarAccessibleObject.GetCalendarPartRectangle(5U, this._calendarIndex, 0, 0);
				}
			}

			// Token: 0x06006B64 RID: 27492 RVA: 0x0018D67B File Offset: 0x0018B87B
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (direction == UnsafeNativeMethods.NavigateDirection.NextSibling)
				{
					return this._calendarAccessibleObject.CalendarBodyAccessibleObject;
				}
				if (direction == UnsafeNativeMethods.NavigateDirection.PreviousSibling)
				{
					return null;
				}
				return base.FragmentNavigate(direction);
			}

			// Token: 0x06006B65 RID: 27493 RVA: 0x00012E4E File Offset: 0x0001104E
			internal override int GetChildId()
			{
				return 1;
			}

			// Token: 0x17001771 RID: 6001
			// (get) Token: 0x06006B66 RID: 27494 RVA: 0x0018D69A File Offset: 0x0018B89A
			public override string Name
			{
				get
				{
					return this._initName;
				}
			}

			// Token: 0x17001772 RID: 6002
			// (get) Token: 0x06006B67 RID: 27495 RVA: 0x0018D6A2 File Offset: 0x0018B8A2
			public override AccessibleObject Parent
			{
				get
				{
					return this._calendarAccessibleObject;
				}
			}

			// Token: 0x17001773 RID: 6003
			// (get) Token: 0x06006B68 RID: 27496 RVA: 0x0018D6AA File Offset: 0x0018B8AA
			internal override int[] RuntimeId
			{
				get
				{
					return this._initRuntimeId;
				}
			}

			// Token: 0x04003B89 RID: 15241
			private const int ChildId = 1;

			// Token: 0x04003B8A RID: 15242
			private readonly MonthCalendar.CalendarAccessibleObject _calendarAccessibleObject;

			// Token: 0x04003B8B RID: 15243
			private readonly MonthCalendar.MonthCalendarAccessibleObjectLevel5 _monthCalendarAccessibleObject;

			// Token: 0x04003B8C RID: 15244
			private readonly int _calendarIndex;

			// Token: 0x04003B8D RID: 15245
			private readonly string _initName;

			// Token: 0x04003B8E RID: 15246
			private readonly int[] _initRuntimeId;
		}

		// Token: 0x020006E5 RID: 1765
		internal class CalendarNextButtonAccessibleObject : MonthCalendar.CalendarButtonAccessibleObject
		{
			// Token: 0x06006B69 RID: 27497 RVA: 0x0018D6B2 File Offset: 0x0018B8B2
			public CalendarNextButtonAccessibleObject(MonthCalendar.MonthCalendarAccessibleObjectLevel5 calendarAccessibleObject)
				: base(calendarAccessibleObject)
			{
				this._monthCalendarAccessibleObject = calendarAccessibleObject;
			}

			// Token: 0x17001774 RID: 6004
			// (get) Token: 0x06006B6A RID: 27498 RVA: 0x0018D6C2 File Offset: 0x0018B8C2
			public override Rectangle Bounds
			{
				get
				{
					return this._monthCalendarAccessibleObject.GetCalendarPartRectangle(1U, 0, 0, 0);
				}
			}

			// Token: 0x17001775 RID: 6005
			// (get) Token: 0x06006B6B RID: 27499 RVA: 0x0018D6D8 File Offset: 0x0018B8D8
			public override string Description
			{
				get
				{
					return SR.GetString("CalendarNextButtonAccessibleObjectDescription");
				}
			}

			// Token: 0x06006B6C RID: 27500 RVA: 0x0018D6E4 File Offset: 0x0018B8E4
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (direction != UnsafeNativeMethods.NavigateDirection.NextSibling)
				{
					if (direction == UnsafeNativeMethods.NavigateDirection.PreviousSibling)
					{
						return this._monthCalendarAccessibleObject.PreviousButtonAccessibleObject;
					}
					return base.FragmentNavigate(direction);
				}
				else
				{
					LinkedList<MonthCalendar.CalendarAccessibleObject> calendarsAccessibleObjects = this._monthCalendarAccessibleObject.CalendarsAccessibleObjects;
					if (calendarsAccessibleObjects == null)
					{
						return null;
					}
					LinkedListNode<MonthCalendar.CalendarAccessibleObject> first = calendarsAccessibleObjects.First;
					if (first == null)
					{
						return null;
					}
					return first.Value;
				}
			}

			// Token: 0x06006B6D RID: 27501 RVA: 0x00016041 File Offset: 0x00014241
			internal override int GetChildId()
			{
				return 2;
			}

			// Token: 0x06006B6E RID: 27502 RVA: 0x0018D723 File Offset: 0x0018B923
			internal override void Invoke()
			{
				if (!this._monthCalendarAccessibleObject.IsHandleCreated || !this._monthCalendarAccessibleObject.IsEnabled || !this.IsEnabled)
				{
					return;
				}
				base.Invoke();
				this._monthCalendarAccessibleObject.UpdateDisplayRange();
			}

			// Token: 0x17001776 RID: 6006
			// (get) Token: 0x06006B6F RID: 27503 RVA: 0x0018D75C File Offset: 0x0018B95C
			internal override bool IsEnabled
			{
				get
				{
					if (!this._monthCalendarAccessibleObject.IsHandleCreated)
					{
						return false;
					}
					SelectionRange displayRange = this._monthCalendarAccessibleObject.GetDisplayRange(true);
					return displayRange != null && this._monthCalendarAccessibleObject.IsEnabled && this._monthCalendarAccessibleObject.MaxDate > displayRange.End;
				}
			}

			// Token: 0x17001777 RID: 6007
			// (get) Token: 0x06006B70 RID: 27504 RVA: 0x0018D7AD File Offset: 0x0018B9AD
			public override string Name
			{
				get
				{
					return SR.GetString("MonthCalendarNextButtonAccessibleName");
				}
			}

			// Token: 0x04003B8F RID: 15247
			private const int ChildId = 2;

			// Token: 0x04003B90 RID: 15248
			private readonly MonthCalendar.MonthCalendarAccessibleObjectLevel5 _monthCalendarAccessibleObject;
		}

		// Token: 0x020006E6 RID: 1766
		internal class CalendarPreviousButtonAccessibleObject : MonthCalendar.CalendarButtonAccessibleObject
		{
			// Token: 0x06006B71 RID: 27505 RVA: 0x0018D7B9 File Offset: 0x0018B9B9
			public CalendarPreviousButtonAccessibleObject(MonthCalendar.MonthCalendarAccessibleObjectLevel5 calendarAccessibleObject)
				: base(calendarAccessibleObject)
			{
				this._monthCalendarAccessibleObject = calendarAccessibleObject;
			}

			// Token: 0x17001778 RID: 6008
			// (get) Token: 0x06006B72 RID: 27506 RVA: 0x0018D7C9 File Offset: 0x0018B9C9
			public override Rectangle Bounds
			{
				get
				{
					return this._monthCalendarAccessibleObject.GetCalendarPartRectangle(2U, 0, 0, 0);
				}
			}

			// Token: 0x17001779 RID: 6009
			// (get) Token: 0x06006B73 RID: 27507 RVA: 0x0018D7DF File Offset: 0x0018B9DF
			public override string Description
			{
				get
				{
					return SR.GetString("CalendarPreviousButtonAccessibleObjectDescription");
				}
			}

			// Token: 0x06006B74 RID: 27508 RVA: 0x0018D7EB File Offset: 0x0018B9EB
			internal override void Invoke()
			{
				if (!this._monthCalendarAccessibleObject.IsHandleCreated || !this._monthCalendarAccessibleObject.IsEnabled || !this.IsEnabled)
				{
					return;
				}
				base.Invoke();
				this._monthCalendarAccessibleObject.UpdateDisplayRange();
			}

			// Token: 0x1700177A RID: 6010
			// (get) Token: 0x06006B75 RID: 27509 RVA: 0x0018D824 File Offset: 0x0018BA24
			internal override bool IsEnabled
			{
				get
				{
					if (!this._monthCalendarAccessibleObject.IsHandleCreated)
					{
						return false;
					}
					SelectionRange displayRange = this._monthCalendarAccessibleObject.GetDisplayRange(true);
					return displayRange != null && this._monthCalendarAccessibleObject.IsEnabled && this._monthCalendarAccessibleObject.MinDate < displayRange.Start;
				}
			}

			// Token: 0x06006B76 RID: 27510 RVA: 0x0018D875 File Offset: 0x0018BA75
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (direction == UnsafeNativeMethods.NavigateDirection.NextSibling)
				{
					return this._monthCalendarAccessibleObject.NextButtonAccessibleObject;
				}
				return base.FragmentNavigate(direction);
			}

			// Token: 0x06006B77 RID: 27511 RVA: 0x00012E4E File Offset: 0x0001104E
			internal override int GetChildId()
			{
				return 1;
			}

			// Token: 0x1700177B RID: 6011
			// (get) Token: 0x06006B78 RID: 27512 RVA: 0x0018D88E File Offset: 0x0018BA8E
			public override string Name
			{
				get
				{
					return SR.GetString("MonthCalendarPreviousButtonAccessibleName");
				}
			}

			// Token: 0x04003B91 RID: 15249
			private const int ChildId = 1;

			// Token: 0x04003B92 RID: 15250
			private readonly MonthCalendar.MonthCalendarAccessibleObjectLevel5 _monthCalendarAccessibleObject;
		}

		// Token: 0x020006E7 RID: 1767
		internal class CalendarRowAccessibleObject : MonthCalendar.MonthCalendarChildAccessibleObject
		{
			// Token: 0x06006B79 RID: 27513 RVA: 0x0018D89C File Offset: 0x0018BA9C
			public CalendarRowAccessibleObject(MonthCalendar.CalendarBodyAccessibleObject calendarBodyAccessibleObject, MonthCalendar.MonthCalendarAccessibleObjectLevel5 monthCalendarAccessibleObject, int calendarIndex, int rowIndex)
				: base(monthCalendarAccessibleObject)
			{
				this._calendarBodyAccessibleObject = calendarBodyAccessibleObject;
				this._monthCalendarAccessibleObject = monthCalendarAccessibleObject;
				this._calendarIndex = calendarIndex;
				this._rowIndex = rowIndex;
				this._initRuntimeId = new int[]
				{
					this._calendarBodyAccessibleObject.RuntimeId[0],
					this._calendarBodyAccessibleObject.RuntimeId[1],
					this._calendarBodyAccessibleObject.RuntimeId[2],
					this._calendarBodyAccessibleObject.RuntimeId[3],
					this.GetChildId()
				};
			}

			// Token: 0x1700177C RID: 6012
			// (get) Token: 0x06006B7A RID: 27514 RVA: 0x0018D922 File Offset: 0x0018BB22
			public override Rectangle Bounds
			{
				get
				{
					return this._monthCalendarAccessibleObject.GetCalendarPartRectangle(7U, this._calendarIndex, this._rowIndex, 0);
				}
			}

			// Token: 0x1700177D RID: 6013
			// (get) Token: 0x06006B7B RID: 27515 RVA: 0x0018D944 File Offset: 0x0018BB44
			internal LinkedList<MonthCalendar.CalendarCellAccessibleObject> CellsAccessibleObjects
			{
				get
				{
					if (this._cellsAccessibleObjects == null && this._monthCalendarAccessibleObject.IsHandleCreated)
					{
						this._cellsAccessibleObjects = new LinkedList<MonthCalendar.CalendarCellAccessibleObject>();
						int num = 0;
						int num2 = ((this._monthCalendarAccessibleObject.CalendarView == NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_MONTH) ? 7 : 4);
						for (int i = num; i < num2; i++)
						{
							string calendarPartText = this._monthCalendarAccessibleObject.GetCalendarPartText(8U, this._calendarIndex, this._rowIndex, i);
							if (!string.IsNullOrEmpty(calendarPartText))
							{
								MonthCalendar.CalendarCellAccessibleObject calendarCellAccessibleObject = ((this._rowIndex == -1) ? new MonthCalendar.CalendarDayOfWeekCellAccessibleObject(this, this._calendarBodyAccessibleObject, this._monthCalendarAccessibleObject, this._calendarIndex, this._rowIndex, i, calendarPartText) : new MonthCalendar.CalendarCellAccessibleObject(this, this._calendarBodyAccessibleObject, this._monthCalendarAccessibleObject, this._calendarIndex, this._rowIndex, i));
								this._cellsAccessibleObjects.AddLast(calendarCellAccessibleObject);
							}
						}
					}
					return this._cellsAccessibleObjects;
				}
			}

			// Token: 0x06006B7C RID: 27516 RVA: 0x0018DA1F File Offset: 0x0018BC1F
			internal void ClearChildCollection()
			{
				this._cellsAccessibleObjects = null;
			}

			// Token: 0x06006B7D RID: 27517 RVA: 0x0018DA28 File Offset: 0x0018BC28
			internal void DisconnectChildren()
			{
				int num = UnsafeNativeMethods.UiaDisconnectProvider(this._weekNumberCellAccessibleObject);
				if (this._cellsAccessibleObjects == null)
				{
					return;
				}
				foreach (MonthCalendar.CalendarCellAccessibleObject calendarCellAccessibleObject in this._cellsAccessibleObjects)
				{
					num = UnsafeNativeMethods.UiaDisconnectProvider(calendarCellAccessibleObject);
				}
			}

			// Token: 0x1700177E RID: 6014
			// (get) Token: 0x06006B7E RID: 27518 RVA: 0x0018DA90 File Offset: 0x0018BC90
			public override string Description
			{
				get
				{
					if (this._rowIndex == -1 || this._monthCalendarAccessibleObject.IsHandleCreated || this._monthCalendarAccessibleObject.CalendarView != NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_MONTH)
					{
						return null;
					}
					LinkedList<MonthCalendar.CalendarCellAccessibleObject> cellsAccessibleObjects = this.CellsAccessibleObjects;
					MonthCalendar.CalendarCellAccessibleObject calendarCellAccessibleObject;
					if (cellsAccessibleObjects == null)
					{
						calendarCellAccessibleObject = null;
					}
					else
					{
						LinkedListNode<MonthCalendar.CalendarCellAccessibleObject> first = cellsAccessibleObjects.First;
						calendarCellAccessibleObject = ((first != null) ? first.Value : null);
					}
					MonthCalendar.CalendarCellAccessibleObject calendarCellAccessibleObject2 = calendarCellAccessibleObject;
					if (calendarCellAccessibleObject2 == null || calendarCellAccessibleObject2.DateRange == null)
					{
						return null;
					}
					string weekNumber = this.GetWeekNumber(calendarCellAccessibleObject2.DateRange.Start);
					return string.Format(SR.GetString("MonthCalendarWeekNumberDescription"), weekNumber);
				}
			}

			// Token: 0x06006B7F RID: 27519 RVA: 0x0018DB10 File Offset: 0x0018BD10
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				switch (direction)
				{
				case UnsafeNativeMethods.NavigateDirection.NextSibling:
				{
					LinkedList<MonthCalendar.CalendarRowAccessibleObject> rowsAccessibleObjects = this._calendarBodyAccessibleObject.RowsAccessibleObjects;
					if (rowsAccessibleObjects == null)
					{
						return null;
					}
					LinkedListNode<MonthCalendar.CalendarRowAccessibleObject> linkedListNode = rowsAccessibleObjects.Find(this);
					if (linkedListNode == null)
					{
						return null;
					}
					LinkedListNode<MonthCalendar.CalendarRowAccessibleObject> next = linkedListNode.Next;
					if (next == null)
					{
						return null;
					}
					return next.Value;
				}
				case UnsafeNativeMethods.NavigateDirection.PreviousSibling:
				{
					LinkedList<MonthCalendar.CalendarRowAccessibleObject> rowsAccessibleObjects2 = this._calendarBodyAccessibleObject.RowsAccessibleObjects;
					if (rowsAccessibleObjects2 == null)
					{
						return null;
					}
					LinkedListNode<MonthCalendar.CalendarRowAccessibleObject> linkedListNode2 = rowsAccessibleObjects2.Find(this);
					if (linkedListNode2 == null)
					{
						return null;
					}
					LinkedListNode<MonthCalendar.CalendarRowAccessibleObject> previous = linkedListNode2.Previous;
					if (previous == null)
					{
						return null;
					}
					return previous.Value;
				}
				case UnsafeNativeMethods.NavigateDirection.FirstChild:
				{
					if (this._monthCalendarAccessibleObject.ShowWeekNumbers && this._rowIndex != -1)
					{
						return this.WeekNumberCellAccessibleObject;
					}
					LinkedList<MonthCalendar.CalendarCellAccessibleObject> cellsAccessibleObjects = this.CellsAccessibleObjects;
					if (cellsAccessibleObjects == null)
					{
						return null;
					}
					LinkedListNode<MonthCalendar.CalendarCellAccessibleObject> first = cellsAccessibleObjects.First;
					if (first == null)
					{
						return null;
					}
					return first.Value;
				}
				case UnsafeNativeMethods.NavigateDirection.LastChild:
				{
					LinkedList<MonthCalendar.CalendarCellAccessibleObject> cellsAccessibleObjects2 = this.CellsAccessibleObjects;
					if (cellsAccessibleObjects2 == null)
					{
						return null;
					}
					LinkedListNode<MonthCalendar.CalendarCellAccessibleObject> last = cellsAccessibleObjects2.Last;
					if (last == null)
					{
						return null;
					}
					return last.Value;
				}
				default:
					return base.FragmentNavigate(direction);
				}
			}

			// Token: 0x06006B80 RID: 27520 RVA: 0x0018DBF4 File Offset: 0x0018BDF4
			internal override int GetChildId()
			{
				return 1 + this._rowIndex;
			}

			// Token: 0x06006B81 RID: 27521 RVA: 0x0018C564 File Offset: 0x0018A764
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30003)
				{
					return 50033;
				}
				if (propertyID != 30009)
				{
					return base.GetPropertyValue(propertyID);
				}
				return this.IsEnabled;
			}

			// Token: 0x06006B82 RID: 27522 RVA: 0x0018DC00 File Offset: 0x0018BE00
			private string GetWeekNumber(DateTime date)
			{
				return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date, CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule, this._monthCalendarAccessibleObject.FirstDayOfWeek).ToString();
			}

			// Token: 0x1700177F RID: 6015
			// (get) Token: 0x06006B83 RID: 27523 RVA: 0x0018DC40 File Offset: 0x0018BE40
			internal override bool HasKeyboardFocus
			{
				get
				{
					MonthCalendar.CalendarCellAccessibleObject focusedCell = this._monthCalendarAccessibleObject.FocusedCell;
					return this._monthCalendarAccessibleObject.Focused && focusedCell != null && focusedCell.CalendarIndex == this._calendarIndex && focusedCell.Row == this._rowIndex;
				}
			}

			// Token: 0x17001780 RID: 6016
			// (get) Token: 0x06006B84 RID: 27524 RVA: 0x00015C90 File Offset: 0x00013E90
			public override string Name
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17001781 RID: 6017
			// (get) Token: 0x06006B85 RID: 27525 RVA: 0x0018DC87 File Offset: 0x0018BE87
			public override AccessibleObject Parent
			{
				get
				{
					return this._calendarBodyAccessibleObject;
				}
			}

			// Token: 0x17001782 RID: 6018
			// (get) Token: 0x06006B86 RID: 27526 RVA: 0x00177DCE File Offset: 0x00175FCE
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.Row;
				}
			}

			// Token: 0x17001783 RID: 6019
			// (get) Token: 0x06006B87 RID: 27527 RVA: 0x0018DC8F File Offset: 0x0018BE8F
			internal override int Row
			{
				get
				{
					return this._rowIndex;
				}
			}

			// Token: 0x17001784 RID: 6020
			// (get) Token: 0x06006B88 RID: 27528 RVA: 0x0018DC97 File Offset: 0x0018BE97
			internal override int[] RuntimeId
			{
				get
				{
					return this._initRuntimeId;
				}
			}

			// Token: 0x06006B89 RID: 27529 RVA: 0x0018DCA0 File Offset: 0x0018BEA0
			internal override void SetFocus()
			{
				MonthCalendar.CalendarCellAccessibleObject focusedCell = this._monthCalendarAccessibleObject.FocusedCell;
				if (focusedCell != null && focusedCell.CalendarIndex == this._calendarIndex && focusedCell.Row == this._rowIndex)
				{
					focusedCell.RaiseAutomationEvent(20005);
				}
			}

			// Token: 0x17001785 RID: 6021
			// (get) Token: 0x06006B8A RID: 27530 RVA: 0x0018DCE4 File Offset: 0x0018BEE4
			internal MonthCalendar.CalendarWeekNumberCellAccessibleObject WeekNumberCellAccessibleObject
			{
				get
				{
					if (this._monthCalendarAccessibleObject.ShowWeekNumbers && this._monthCalendarAccessibleObject.CalendarView == NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_MONTH)
					{
						LinkedList<MonthCalendar.CalendarCellAccessibleObject> cellsAccessibleObjects = this.CellsAccessibleObjects;
						if (((cellsAccessibleObjects != null) ? cellsAccessibleObjects.First : null) != null && this.CellsAccessibleObjects.First.Value.DateRange != null)
						{
							if (this._weekNumberCellAccessibleObject == null)
							{
								this._weekNumberCellAccessibleObject = new MonthCalendar.CalendarWeekNumberCellAccessibleObject(this, this._calendarBodyAccessibleObject, this._monthCalendarAccessibleObject, this._calendarIndex, this._rowIndex, -1, this.GetWeekNumber(this.CellsAccessibleObjects.First.Value.DateRange.Start));
							}
							return this._weekNumberCellAccessibleObject;
						}
					}
					return null;
				}
			}

			// Token: 0x04003B93 RID: 15251
			private const int ChildIdIncrement = 1;

			// Token: 0x04003B94 RID: 15252
			private readonly MonthCalendar.CalendarBodyAccessibleObject _calendarBodyAccessibleObject;

			// Token: 0x04003B95 RID: 15253
			private readonly MonthCalendar.MonthCalendarAccessibleObjectLevel5 _monthCalendarAccessibleObject;

			// Token: 0x04003B96 RID: 15254
			private readonly int _calendarIndex;

			// Token: 0x04003B97 RID: 15255
			private readonly int _rowIndex;

			// Token: 0x04003B98 RID: 15256
			private readonly int[] _initRuntimeId;

			// Token: 0x04003B99 RID: 15257
			private LinkedList<MonthCalendar.CalendarCellAccessibleObject> _cellsAccessibleObjects;

			// Token: 0x04003B9A RID: 15258
			private MonthCalendar.CalendarWeekNumberCellAccessibleObject _weekNumberCellAccessibleObject;
		}

		// Token: 0x020006E8 RID: 1768
		internal class CalendarTodayLinkAccessibleObject : MonthCalendar.CalendarButtonAccessibleObject
		{
			// Token: 0x06006B8B RID: 27531 RVA: 0x0018DD8B File Offset: 0x0018BF8B
			public CalendarTodayLinkAccessibleObject(MonthCalendar.MonthCalendarAccessibleObjectLevel5 calendarAccessibleObject)
				: base(calendarAccessibleObject)
			{
				this._monthCalendarAccessibleObject = calendarAccessibleObject;
			}

			// Token: 0x17001786 RID: 6022
			// (get) Token: 0x06006B8C RID: 27532 RVA: 0x0018DD9B File Offset: 0x0018BF9B
			public override Rectangle Bounds
			{
				get
				{
					return this._monthCalendarAccessibleObject.GetCalendarPartRectangle(3U, 0, 0, 0);
				}
			}

			// Token: 0x17001787 RID: 6023
			// (get) Token: 0x06006B8D RID: 27533 RVA: 0x0018DDB1 File Offset: 0x0018BFB1
			public override string Description
			{
				get
				{
					return SR.GetString("CalendarTodayLinkAccessibleObjectDescription");
				}
			}

			// Token: 0x06006B8E RID: 27534 RVA: 0x0018DDBD File Offset: 0x0018BFBD
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (direction != UnsafeNativeMethods.NavigateDirection.PreviousSibling)
				{
					return base.FragmentNavigate(direction);
				}
				LinkedList<MonthCalendar.CalendarAccessibleObject> calendarsAccessibleObjects = this._monthCalendarAccessibleObject.CalendarsAccessibleObjects;
				if (calendarsAccessibleObjects == null)
				{
					return null;
				}
				LinkedListNode<MonthCalendar.CalendarAccessibleObject> last = calendarsAccessibleObjects.Last;
				if (last == null)
				{
					return null;
				}
				return last.Value;
			}

			// Token: 0x06006B8F RID: 27535 RVA: 0x0018DDEC File Offset: 0x0018BFEC
			internal override int GetChildId()
			{
				LinkedList<MonthCalendar.CalendarAccessibleObject> calendarsAccessibleObjects = this._monthCalendarAccessibleObject.CalendarsAccessibleObjects;
				int? num = 3 + ((calendarsAccessibleObjects != null) ? new int?(calendarsAccessibleObjects.Count) : null);
				if (num == null)
				{
					return -1;
				}
				return num.GetValueOrDefault();
			}

			// Token: 0x17001788 RID: 6024
			// (get) Token: 0x06006B90 RID: 27536 RVA: 0x0018DE54 File Offset: 0x0018C054
			public override string Name
			{
				get
				{
					return string.Format(SR.GetString("MonthCalendarTodayButtonAccessibleName"), this._monthCalendarAccessibleObject.TodayDate.ToShortDateString());
				}
			}

			// Token: 0x04003B9B RID: 15259
			private const int ChildIdIncrement = 3;

			// Token: 0x04003B9C RID: 15260
			private readonly MonthCalendar.MonthCalendarAccessibleObjectLevel5 _monthCalendarAccessibleObject;
		}

		// Token: 0x020006E9 RID: 1769
		internal class CalendarWeekNumberCellAccessibleObject : MonthCalendar.CalendarCellAccessibleObject
		{
			// Token: 0x06006B91 RID: 27537 RVA: 0x0018DE83 File Offset: 0x0018C083
			public CalendarWeekNumberCellAccessibleObject(MonthCalendar.CalendarRowAccessibleObject calendarRowAccessibleObject, MonthCalendar.CalendarBodyAccessibleObject calendarBodyAccessibleObject, MonthCalendar.MonthCalendarAccessibleObjectLevel5 monthCalendarAccessibleObject, int calendarIndex, int rowIndex, int columnIndex, string weekNumber)
				: base(calendarRowAccessibleObject, calendarBodyAccessibleObject, monthCalendarAccessibleObject, calendarIndex, rowIndex, columnIndex)
			{
				this._calendarRowAccessibleObject = calendarRowAccessibleObject;
				this._weekNumber = weekNumber;
			}

			// Token: 0x17001789 RID: 6025
			// (get) Token: 0x06006B92 RID: 27538 RVA: 0x00015C90 File Offset: 0x00013E90
			internal override SelectionRange DateRange
			{
				get
				{
					return null;
				}
			}

			// Token: 0x1700178A RID: 6026
			// (get) Token: 0x06006B93 RID: 27539 RVA: 0x0017E1A1 File Offset: 0x0017C3A1
			public override string DefaultAction
			{
				get
				{
					return string.Empty;
				}
			}

			// Token: 0x1700178B RID: 6027
			// (get) Token: 0x06006B94 RID: 27540 RVA: 0x00015C90 File Offset: 0x00013E90
			public override string Description
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06006B95 RID: 27541 RVA: 0x0018DEA3 File Offset: 0x0018C0A3
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (direction != UnsafeNativeMethods.NavigateDirection.NextSibling)
				{
					if (direction != UnsafeNativeMethods.NavigateDirection.PreviousSibling)
					{
						return base.FragmentNavigate(direction);
					}
					return null;
				}
				else
				{
					LinkedList<MonthCalendar.CalendarCellAccessibleObject> cellsAccessibleObjects = this._calendarRowAccessibleObject.CellsAccessibleObjects;
					if (cellsAccessibleObjects == null)
					{
						return null;
					}
					LinkedListNode<MonthCalendar.CalendarCellAccessibleObject> first = cellsAccessibleObjects.First;
					if (first == null)
					{
						return null;
					}
					return first.Value;
				}
			}

			// Token: 0x06006B96 RID: 27542 RVA: 0x0001180C File Offset: 0x0000FA0C
			internal override int GetChildId()
			{
				return 0;
			}

			// Token: 0x06006B97 RID: 27543 RVA: 0x0018D57A File Offset: 0x0018B77A
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30003)
				{
					return 50034;
				}
				if (propertyID != 30009)
				{
					return base.GetPropertyValue(propertyID);
				}
				return false;
			}

			// Token: 0x1700178C RID: 6028
			// (get) Token: 0x06006B98 RID: 27544 RVA: 0x0001180C File Offset: 0x0000FA0C
			internal override bool HasKeyboardFocus
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06006B99 RID: 27545 RVA: 0x000070A6 File Offset: 0x000052A6
			internal override void Invoke()
			{
			}

			// Token: 0x06006B9A RID: 27546 RVA: 0x0018D5A7 File Offset: 0x0018B7A7
			internal override bool IsPatternSupported(int patternId)
			{
				return patternId != 10000 && patternId != 10007 && patternId != 10013 && base.IsPatternSupported(patternId);
			}

			// Token: 0x1700178D RID: 6029
			// (get) Token: 0x06006B9B RID: 27547 RVA: 0x0018DEDA File Offset: 0x0018C0DA
			public override string Name
			{
				get
				{
					return string.Format(SR.GetString("MonthCalendarWeekNumberDescription"), this._weekNumber);
				}
			}

			// Token: 0x1700178E RID: 6030
			// (get) Token: 0x06006B9C RID: 27548 RVA: 0x0017F528 File Offset: 0x0017D728
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.RowHeader;
				}
			}

			// Token: 0x1700178F RID: 6031
			// (get) Token: 0x06006B9D RID: 27549 RVA: 0x0001180C File Offset: 0x0000FA0C
			public override AccessibleStates State
			{
				get
				{
					return AccessibleStates.None;
				}
			}

			// Token: 0x04003B9D RID: 15261
			private const int ChildId = 0;

			// Token: 0x04003B9E RID: 15262
			private readonly MonthCalendar.CalendarRowAccessibleObject _calendarRowAccessibleObject;

			// Token: 0x04003B9F RID: 15263
			private readonly string _weekNumber;
		}

		// Token: 0x020006EA RID: 1770
		internal class MonthCalendarAccessibleObjectLevel5 : MonthCalendar.MonthCalendarAccessibleObject
		{
			// Token: 0x06006B9E RID: 27550 RVA: 0x0018DEF1 File Offset: 0x0018C0F1
			public MonthCalendarAccessibleObjectLevel5(MonthCalendar owner)
				: base(owner)
			{
				this.calendar.DisplayRangeChanged += this.OnMonthCalendarStateChanged;
				this.calendar.CalendarViewChanged += this.OnMonthCalendarStateChanged;
			}

			// Token: 0x17001790 RID: 6032
			// (get) Token: 0x06006B9F RID: 27551 RVA: 0x0018DF28 File Offset: 0x0018C128
			internal LinkedList<MonthCalendar.CalendarAccessibleObject> CalendarsAccessibleObjects
			{
				get
				{
					if (!this.calendar.IsHandleCreated)
					{
						return null;
					}
					if (this._calendarsAccessibleObjects == null)
					{
						this._calendarsAccessibleObjects = new LinkedList<MonthCalendar.CalendarAccessibleObject>();
						string text = string.Empty;
						for (int i = 0; i < 12; i++)
						{
							string calendarPartText = this.GetCalendarPartText(5U, i, 0, 0);
							if (calendarPartText == string.Empty || calendarPartText == text)
							{
								break;
							}
							MonthCalendar.CalendarAccessibleObject calendarAccessibleObject = new MonthCalendar.CalendarAccessibleObject(this, i, calendarPartText);
							this._calendarsAccessibleObjects.AddLast(calendarAccessibleObject);
							text = calendarPartText;
						}
					}
					return this._calendarsAccessibleObjects;
				}
			}

			// Token: 0x06006BA0 RID: 27552 RVA: 0x0018DFAC File Offset: 0x0018C1AC
			internal void DisconnectChildren()
			{
				int num = UnsafeNativeMethods.UiaDisconnectProvider(this._previousButtonAccessibleObject);
				num = UnsafeNativeMethods.UiaDisconnectProvider(this._nextButtonAccessibleObject);
				num = UnsafeNativeMethods.UiaDisconnectProvider(this._todayLinkAccessibleObject);
				num = UnsafeNativeMethods.UiaDisconnectProvider(this._focusedCellAccessibleObject);
				if (this._calendarsAccessibleObjects == null)
				{
					return;
				}
				foreach (MonthCalendar.CalendarAccessibleObject calendarAccessibleObject in this._calendarsAccessibleObjects)
				{
					calendarAccessibleObject.DisconnectChildren();
					num = UnsafeNativeMethods.UiaDisconnectProvider(calendarAccessibleObject);
				}
			}

			// Token: 0x06006BA1 RID: 27553 RVA: 0x0018E040 File Offset: 0x0018C240
			private DayOfWeek CastDayToDayOfWeek(Day day)
			{
				switch (day)
				{
				case Day.Monday:
					return DayOfWeek.Monday;
				case Day.Tuesday:
					return DayOfWeek.Tuesday;
				case Day.Wednesday:
					return DayOfWeek.Wednesday;
				case Day.Thursday:
					return DayOfWeek.Thursday;
				case Day.Friday:
					return DayOfWeek.Friday;
				case Day.Saturday:
					return DayOfWeek.Saturday;
				case Day.Sunday:
					return DayOfWeek.Sunday;
				case Day.Default:
					return DayOfWeek.Sunday;
				default:
					return DayOfWeek.Sunday;
				}
			}

			// Token: 0x17001791 RID: 6033
			// (get) Token: 0x06006BA2 RID: 27554 RVA: 0x0018E07B File Offset: 0x0018C27B
			internal NativeMethods.MONTCALENDAR_VIEW_MODE CalendarView
			{
				get
				{
					return this.calendar.mcCurView;
				}
			}

			// Token: 0x17001792 RID: 6034
			// (get) Token: 0x06006BA3 RID: 27555 RVA: 0x0018E088 File Offset: 0x0018C288
			internal override int ColumnCount
			{
				get
				{
					if (!this.calendar.IsHandleCreated || this.CalendarsAccessibleObjects == null)
					{
						return -1;
					}
					LinkedListNode<MonthCalendar.CalendarAccessibleObject> first = this.CalendarsAccessibleObjects.First;
					int num = ((first != null) ? first.Value.Bounds.Y : 0);
					int num2 = 0;
					foreach (MonthCalendar.CalendarAccessibleObject calendarAccessibleObject in this.CalendarsAccessibleObjects)
					{
						if (calendarAccessibleObject.Bounds.Y > num)
						{
							break;
						}
						num2++;
					}
					return num2;
				}
			}

			// Token: 0x06006BA4 RID: 27556 RVA: 0x0018E12C File Offset: 0x0018C32C
			internal override UnsafeNativeMethods.IRawElementProviderFragment ElementProviderFromPoint(double x, double y)
			{
				if (!this.calendar.IsHandleCreated)
				{
					return base.ElementProviderFromPoint(x, y);
				}
				int num = (int)x;
				int num2 = (int)y;
				NativeMethods.MCHITTESTINFOLEVEL5 hitTestInfo = this.GetHitTestInfo(num, num2);
				int uHit = hitTestInfo.uHit;
				if (uHit <= 1048576)
				{
					if (uHit <= 65538)
					{
						if (uHit == 0)
						{
							return this;
						}
						if (uHit - 65536 > 2)
						{
							goto IL_1B9;
						}
						MonthCalendar.CalendarAccessibleObject calendarFromPoint = this.GetCalendarFromPoint(num, num2);
						return ((calendarFromPoint != null) ? calendarFromPoint.CalendarHeaderAccessibleObject : null) ?? this;
					}
					else
					{
						switch (uHit)
						{
						case 131072:
							return this.GetCalendarFromPoint(num, num2) ?? this;
						case 131073:
						case 131074:
						case 131075:
							break;
						case 131076:
						{
							LinkedList<MonthCalendar.CalendarAccessibleObject> calendarsAccessibleObjects = this.CalendarsAccessibleObjects;
							MonthCalendar.CalendarAccessibleObject calendarAccessibleObject;
							if (calendarsAccessibleObjects == null)
							{
								calendarAccessibleObject = null;
							}
							else
							{
								LinkedListNode<MonthCalendar.CalendarAccessibleObject> first = calendarsAccessibleObjects.First;
								calendarAccessibleObject = ((first != null) ? first.Value : null);
							}
							MonthCalendar.CalendarAccessibleObject calendarAccessibleObject2 = calendarAccessibleObject;
							if (calendarAccessibleObject2 != null && calendarAccessibleObject2.Bounds.Contains(num, num2))
							{
								return calendarAccessibleObject2;
							}
							return this;
						}
						case 131077:
						{
							LinkedList<MonthCalendar.CalendarAccessibleObject> calendarsAccessibleObjects2 = this.CalendarsAccessibleObjects;
							MonthCalendar.CalendarAccessibleObject calendarAccessibleObject3;
							if (calendarsAccessibleObjects2 == null)
							{
								calendarAccessibleObject3 = null;
							}
							else
							{
								LinkedListNode<MonthCalendar.CalendarAccessibleObject> last = calendarsAccessibleObjects2.Last;
								calendarAccessibleObject3 = ((last != null) ? last.Value : null);
							}
							MonthCalendar.CalendarAccessibleObject calendarAccessibleObject4 = calendarAccessibleObject3;
							if (calendarAccessibleObject4 != null && calendarAccessibleObject4.Bounds.Contains(num, num2))
							{
								return calendarAccessibleObject4;
							}
							return this;
						}
						default:
							if (uHit == 196608)
							{
								return this.TodayLinkAccessibleObject;
							}
							if (uHit != 1048576)
							{
								goto IL_1B9;
							}
							return this;
						}
					}
				}
				else if (uHit <= 16908289)
				{
					if (uHit == 16777216 || uHit == 16842755)
					{
						return this.NextButtonAccessibleObject;
					}
					if (uHit != 16908289)
					{
						goto IL_1B9;
					}
				}
				else
				{
					if (uHit == 33554432 || uHit == 33619971)
					{
						return this.PreviousButtonAccessibleObject;
					}
					if (uHit != 33685505)
					{
						goto IL_1B9;
					}
				}
				MonthCalendar.CalendarAccessibleObject calendarFromPoint2 = this.GetCalendarFromPoint(num, num2);
				return ((calendarFromPoint2 != null) ? calendarFromPoint2.GetChildFromPoint(hitTestInfo) : null) ?? this;
				IL_1B9:
				return base.ElementProviderFromPoint(x, y);
			}

			// Token: 0x17001793 RID: 6035
			// (get) Token: 0x06006BA5 RID: 27557 RVA: 0x0018E2FA File Offset: 0x0018C4FA
			internal DayOfWeek FirstDayOfWeek
			{
				get
				{
					return this.CastDayToDayOfWeek(this.calendar.FirstDayOfWeek);
				}
			}

			// Token: 0x17001794 RID: 6036
			// (get) Token: 0x06006BA6 RID: 27558 RVA: 0x0018E30D File Offset: 0x0018C50D
			internal bool Focused
			{
				get
				{
					return this.calendar.Focused;
				}
			}

			// Token: 0x17001795 RID: 6037
			// (get) Token: 0x06006BA7 RID: 27559 RVA: 0x0018E31A File Offset: 0x0018C51A
			internal MonthCalendar.CalendarCellAccessibleObject FocusedCell
			{
				get
				{
					if (UnsafeNativeMethods.UiaClientsAreListening())
					{
						if (this._focusedCellAccessibleObject == null)
						{
							this._focusedCellAccessibleObject = this.GetCellByDate(this.calendar._focusedDate);
						}
						return this._focusedCellAccessibleObject;
					}
					return null;
				}
			}

			// Token: 0x06006BA8 RID: 27560 RVA: 0x0018E34C File Offset: 0x0018C54C
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (direction == UnsafeNativeMethods.NavigateDirection.FirstChild)
				{
					return this.PreviousButtonAccessibleObject;
				}
				if (direction != UnsafeNativeMethods.NavigateDirection.LastChild)
				{
					return base.FragmentNavigate(direction);
				}
				if (this.ShowToday)
				{
					return this.TodayLinkAccessibleObject;
				}
				LinkedList<MonthCalendar.CalendarAccessibleObject> calendarsAccessibleObjects = this.CalendarsAccessibleObjects;
				if (calendarsAccessibleObjects == null)
				{
					return null;
				}
				LinkedListNode<MonthCalendar.CalendarAccessibleObject> last = calendarsAccessibleObjects.Last;
				if (last == null)
				{
					return null;
				}
				return last.Value;
			}

			// Token: 0x06006BA9 RID: 27561 RVA: 0x0018E3A0 File Offset: 0x0018C5A0
			private MonthCalendar.CalendarAccessibleObject GetCalendarFromPoint(int x, int y)
			{
				if (!this.calendar.IsHandleCreated || this.CalendarsAccessibleObjects == null)
				{
					return null;
				}
				foreach (MonthCalendar.CalendarAccessibleObject calendarAccessibleObject in this.CalendarsAccessibleObjects)
				{
					if (calendarAccessibleObject.Bounds.Contains(x, y))
					{
						return calendarAccessibleObject;
					}
				}
				return null;
			}

			// Token: 0x06006BAA RID: 27562 RVA: 0x0018E41C File Offset: 0x0018C61C
			internal SelectionRange GetCalendarPartDateRange(uint dwPart, int calendarIndex = 0, int rowIndex = 0, int columnIndex = 0)
			{
				if (!this.calendar.IsHandleCreated)
				{
					return null;
				}
				NativeMethods.MCGRIDINFO mcgridinfo = new NativeMethods.MCGRIDINFO
				{
					cbSize = (uint)Marshal.SizeOf(typeof(NativeMethods.MCGRIDINFO)),
					dwFlags = 1,
					dwPart = dwPart,
					iCalendar = calendarIndex,
					iCol = columnIndex,
					iRow = rowIndex
				};
				if (!(UnsafeNativeMethods.SendMessage(new HandleRef(this.calendar, this.calendar.Handle), 4120, 0, ref mcgridinfo) != IntPtr.Zero))
				{
					return null;
				}
				return new SelectionRange(mcgridinfo.stStart, mcgridinfo.stEnd);
			}

			// Token: 0x06006BAB RID: 27563 RVA: 0x0018E4D0 File Offset: 0x0018C6D0
			internal NativeMethods.RECT GetCalendarPartRectangle(uint dwPart, int calendarIndex = 0, int rowIndex = 0, int columnIndex = 0)
			{
				if (!this.calendar.IsHandleCreated)
				{
					return default(NativeMethods.RECT);
				}
				NativeMethods.MCGRIDINFO mcgridinfo = new NativeMethods.MCGRIDINFO
				{
					cbSize = (uint)Marshal.SizeOf(typeof(NativeMethods.MCGRIDINFO)),
					dwFlags = 2,
					dwPart = dwPart,
					iCalendar = calendarIndex,
					iCol = columnIndex,
					iRow = rowIndex
				};
				bool flag = UnsafeNativeMethods.SendMessage(new HandleRef(this.calendar, this.calendar.Handle), 4120, 0, ref mcgridinfo) != IntPtr.Zero;
				if (flag)
				{
					return this.calendar.RectangleToScreen(mcgridinfo.rc);
				}
				return default(NativeMethods.RECT);
			}

			// Token: 0x06006BAC RID: 27564 RVA: 0x0018E594 File Offset: 0x0018C794
			internal unsafe string GetCalendarPartText(uint dwPart, int calendarIndex = 0, int rowIndex = 0, int columnIndex = 0)
			{
				if (!this.calendar.IsHandleCreated)
				{
					return string.Empty;
				}
				char[] array = new char[20];
				char[] array2;
				char* ptr;
				if ((array2 = array) == null || array2.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array2[0];
				}
				NativeMethods.MCGRIDINFO mcgridinfo = new NativeMethods.MCGRIDINFO
				{
					cbSize = (uint)Marshal.SizeOf(typeof(NativeMethods.MCGRIDINFO)),
					dwFlags = 4,
					dwPart = dwPart,
					iCalendar = calendarIndex,
					iCol = columnIndex,
					iRow = rowIndex,
					pszName = ptr,
					cchName = (UIntPtr)((ulong)((long)array.Length)) - 1
				};
				bool flag = UnsafeNativeMethods.SendMessage(new HandleRef(this.calendar, this.calendar.Handle), 4120, 0, ref mcgridinfo) != IntPtr.Zero;
				array2 = null;
				string text = string.Empty;
				foreach (char c in array)
				{
					if (c != '\0' && c != '\u200e')
					{
						text += c.ToString();
					}
				}
				return text;
			}

			// Token: 0x06006BAD RID: 27565 RVA: 0x0018E6B0 File Offset: 0x0018C8B0
			private MonthCalendar.CalendarCellAccessibleObject GetCellByDate(DateTime date)
			{
				if (!this.calendar.IsHandleCreated || this.CalendarsAccessibleObjects == null)
				{
					return null;
				}
				foreach (MonthCalendar.CalendarAccessibleObject calendarAccessibleObject in this.CalendarsAccessibleObjects)
				{
					if (calendarAccessibleObject.DateRange != null)
					{
						DateTime start = calendarAccessibleObject.DateRange.Start;
						DateTime end = calendarAccessibleObject.DateRange.End;
						if (!(date < start) && !(date > end))
						{
							LinkedList<MonthCalendar.CalendarRowAccessibleObject> rowsAccessibleObjects = calendarAccessibleObject.CalendarBodyAccessibleObject.RowsAccessibleObjects;
							if (rowsAccessibleObjects == null)
							{
								return null;
							}
							foreach (MonthCalendar.CalendarRowAccessibleObject calendarRowAccessibleObject in rowsAccessibleObjects)
							{
								if (calendarRowAccessibleObject.CellsAccessibleObjects == null)
								{
									return null;
								}
								foreach (MonthCalendar.CalendarCellAccessibleObject calendarCellAccessibleObject in calendarRowAccessibleObject.CellsAccessibleObjects)
								{
									SelectionRange dateRange = calendarCellAccessibleObject.DateRange;
									if (dateRange != null && date >= dateRange.Start && date <= dateRange.End)
									{
										return calendarCellAccessibleObject;
									}
								}
							}
						}
					}
				}
				return null;
			}

			// Token: 0x06006BAE RID: 27566 RVA: 0x00015C90 File Offset: 0x00013E90
			internal override UnsafeNativeMethods.IRawElementProviderSimple[] GetColumnHeaders()
			{
				return null;
			}

			// Token: 0x06006BAF RID: 27567 RVA: 0x0018E854 File Offset: 0x0018CA54
			internal SelectionRange GetDisplayRange(bool visible)
			{
				if (!this.calendar.IsHandleCreated)
				{
					return null;
				}
				return this.calendar.GetDisplayRange(visible);
			}

			// Token: 0x06006BB0 RID: 27568 RVA: 0x0018E871 File Offset: 0x0018CA71
			internal override UnsafeNativeMethods.IRawElementProviderFragment GetFocus()
			{
				return this._focusedCellAccessibleObject;
			}

			// Token: 0x06006BB1 RID: 27569 RVA: 0x0018E871 File Offset: 0x0018CA71
			public override AccessibleObject GetFocused()
			{
				return this._focusedCellAccessibleObject;
			}

			// Token: 0x06006BB2 RID: 27570 RVA: 0x0018E87C File Offset: 0x0018CA7C
			private NativeMethods.MCHITTESTINFOLEVEL5 GetHitTestInfo(int xScreen, int yScreen)
			{
				if (!this.calendar.IsHandleCreated)
				{
					return default(NativeMethods.MCHITTESTINFOLEVEL5);
				}
				Point point = this.calendar.PointToClient(new Point(xScreen, yScreen));
				NativeMethods.MCHITTESTINFOLEVEL5 mchittestinfolevel = new NativeMethods.MCHITTESTINFOLEVEL5
				{
					cbSize = (uint)Marshal.SizeOf(typeof(NativeMethods.MCHITTESTINFOLEVEL5)),
					pt = point
				};
				UnsafeNativeMethods.SendMessage(new HandleRef(this.calendar, this.calendar.Handle), 4110, 0, ref mchittestinfolevel);
				return mchittestinfolevel;
			}

			// Token: 0x06006BB3 RID: 27571 RVA: 0x0018E900 File Offset: 0x0018CB00
			internal override UnsafeNativeMethods.IRawElementProviderSimple GetItem(int row, int column)
			{
				if (!this.calendar.IsHandleCreated || this.CalendarsAccessibleObjects == null)
				{
					return null;
				}
				foreach (MonthCalendar.CalendarAccessibleObject calendarAccessibleObject in this.CalendarsAccessibleObjects)
				{
					if (calendarAccessibleObject.Row == row && calendarAccessibleObject.Column == column)
					{
						return calendarAccessibleObject;
					}
				}
				return null;
			}

			// Token: 0x06006BB4 RID: 27572 RVA: 0x0018E97C File Offset: 0x0018CB7C
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID <= 30030)
				{
					if (propertyID <= 30005)
					{
						if (propertyID != 30003)
						{
							if (propertyID == 30005)
							{
								return this.Name;
							}
						}
						else
						{
							if (this.calendar.AccessibleRole == AccessibleRole.Default)
							{
								return 50001;
							}
							return base.GetPropertyValue(propertyID);
						}
					}
					else
					{
						if (propertyID == 30009)
						{
							return this.IsEnabled;
						}
						if (propertyID == 30030)
						{
							return this.IsPatternSupported(10006);
						}
					}
				}
				else if (propertyID <= 30043)
				{
					if (propertyID == 30038)
					{
						return this.IsPatternSupported(10012);
					}
					if (propertyID == 30043)
					{
						return this.IsPatternSupported(10002);
					}
				}
				else
				{
					if (propertyID == 30090)
					{
						return this.IsPatternSupported(10018);
					}
					if (propertyID == 30096)
					{
						return this.State;
					}
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x06006BB5 RID: 27573 RVA: 0x00015C90 File Offset: 0x00013E90
			internal override UnsafeNativeMethods.IRawElementProviderSimple[] GetRowHeaders()
			{
				return null;
			}

			// Token: 0x17001796 RID: 6038
			// (get) Token: 0x06006BB6 RID: 27574 RVA: 0x0018EA86 File Offset: 0x0018CC86
			internal bool IsEnabled
			{
				get
				{
					return this.calendar.Enabled;
				}
			}

			// Token: 0x17001797 RID: 6039
			// (get) Token: 0x06006BB7 RID: 27575 RVA: 0x0018EA93 File Offset: 0x0018CC93
			internal bool IsHandleCreated
			{
				get
				{
					return this.calendar.IsHandleCreated;
				}
			}

			// Token: 0x06006BB8 RID: 27576 RVA: 0x0018EAA0 File Offset: 0x0018CCA0
			internal override bool IsPatternSupported(int patternId)
			{
				if (patternId <= 10006)
				{
					if (patternId == 10002)
					{
						return true;
					}
					if (patternId == 10006)
					{
						return true;
					}
				}
				else
				{
					if (patternId == 10012)
					{
						return true;
					}
					if (patternId == 10018)
					{
						return true;
					}
				}
				return base.IsPatternSupported(patternId);
			}

			// Token: 0x17001798 RID: 6040
			// (get) Token: 0x06006BB9 RID: 27577 RVA: 0x0018EADD File Offset: 0x0018CCDD
			internal DateTime MinDate
			{
				get
				{
					return this.calendar.MinDate;
				}
			}

			// Token: 0x17001799 RID: 6041
			// (get) Token: 0x06006BBA RID: 27578 RVA: 0x0018EAEA File Offset: 0x0018CCEA
			internal DateTime MaxDate
			{
				get
				{
					return this.calendar.MaxDate;
				}
			}

			// Token: 0x1700179A RID: 6042
			// (get) Token: 0x06006BBB RID: 27579 RVA: 0x0018EAF7 File Offset: 0x0018CCF7
			internal MonthCalendar.CalendarNextButtonAccessibleObject NextButtonAccessibleObject
			{
				get
				{
					if (this._nextButtonAccessibleObject == null)
					{
						this._nextButtonAccessibleObject = new MonthCalendar.CalendarNextButtonAccessibleObject(this);
					}
					return this._nextButtonAccessibleObject;
				}
			}

			// Token: 0x06006BBC RID: 27580 RVA: 0x0018EB13 File Offset: 0x0018CD13
			private void OnMonthCalendarStateChanged(object sender, EventArgs e)
			{
				this.RebuildAccessibilityTree();
				MonthCalendar.CalendarCellAccessibleObject focusedCell = this.FocusedCell;
				if (focusedCell == null)
				{
					return;
				}
				focusedCell.RaiseAutomationEvent(20005);
			}

			// Token: 0x1700179B RID: 6043
			// (get) Token: 0x06006BBD RID: 27581 RVA: 0x0018EB31 File Offset: 0x0018CD31
			internal MonthCalendar.CalendarPreviousButtonAccessibleObject PreviousButtonAccessibleObject
			{
				get
				{
					if (this._previousButtonAccessibleObject == null)
					{
						this._previousButtonAccessibleObject = new MonthCalendar.CalendarPreviousButtonAccessibleObject(this);
					}
					return this._previousButtonAccessibleObject;
				}
			}

			// Token: 0x06006BBE RID: 27582 RVA: 0x0018EB4D File Offset: 0x0018CD4D
			internal void RaiseAutomationEventForChild(int automationEventId)
			{
				if (!this.calendar.IsHandleCreated)
				{
					return;
				}
				if (this._calendarsAccessibleObjects == null)
				{
					return;
				}
				this._focusedCellAccessibleObject = null;
				MonthCalendar.CalendarCellAccessibleObject focusedCell = this.FocusedCell;
				if (focusedCell == null)
				{
					return;
				}
				focusedCell.RaiseAutomationEvent(automationEventId);
			}

			// Token: 0x06006BBF RID: 27583 RVA: 0x0018EB80 File Offset: 0x0018CD80
			private void RebuildAccessibilityTree()
			{
				if (!this.calendar.IsHandleCreated || this.CalendarsAccessibleObjects == null)
				{
					return;
				}
				foreach (MonthCalendar.CalendarAccessibleObject calendarAccessibleObject in this.CalendarsAccessibleObjects)
				{
					calendarAccessibleObject.CalendarBodyAccessibleObject.ClearChildCollection();
				}
				this._calendarsAccessibleObjects = null;
				this._focusedCellAccessibleObject = null;
				if (this.CalendarsAccessibleObjects.Count > 0)
				{
					MonthCalendar.CalendarCellAccessibleObject focusedCell = this.FocusedCell;
					if (focusedCell == null)
					{
						return;
					}
					focusedCell.RaiseAutomationEvent(20005);
				}
			}

			// Token: 0x1700179C RID: 6044
			// (get) Token: 0x06006BC0 RID: 27584 RVA: 0x0018EC20 File Offset: 0x0018CE20
			internal override int RowCount
			{
				get
				{
					if (this.ColumnCount <= 0 || this.CalendarsAccessibleObjects == null)
					{
						return 0;
					}
					return (int)Math.Ceiling((double)this.CalendarsAccessibleObjects.Count / (double)this.ColumnCount);
				}
			}

			// Token: 0x1700179D RID: 6045
			// (get) Token: 0x06006BC1 RID: 27585 RVA: 0x0001180C File Offset: 0x0000FA0C
			internal override UnsafeNativeMethods.RowOrColumnMajor RowOrColumnMajor
			{
				get
				{
					return UnsafeNativeMethods.RowOrColumnMajor.RowOrColumnMajor_RowMajor;
				}
			}

			// Token: 0x1700179E RID: 6046
			// (get) Token: 0x06006BC2 RID: 27586 RVA: 0x0018EC4F File Offset: 0x0018CE4F
			internal SelectionRange SelectionRange
			{
				get
				{
					return this.calendar.SelectionRange;
				}
			}

			// Token: 0x06006BC3 RID: 27587 RVA: 0x0018EC5C File Offset: 0x0018CE5C
			internal override void SetFocus()
			{
				MonthCalendar.CalendarCellAccessibleObject focusedCell = this.FocusedCell;
				if (focusedCell == null)
				{
					return;
				}
				focusedCell.RaiseAutomationEvent(20005);
			}

			// Token: 0x06006BC4 RID: 27588 RVA: 0x0018EC74 File Offset: 0x0018CE74
			internal void SetSelectionRange(DateTime d1, DateTime d2)
			{
				if (this.calendar.IsHandleCreated)
				{
					this.calendar.SetSelectionRange(d1, d2);
				}
			}

			// Token: 0x1700179F RID: 6047
			// (get) Token: 0x06006BC5 RID: 27589 RVA: 0x0018EC90 File Offset: 0x0018CE90
			internal bool ShowToday
			{
				get
				{
					return this.calendar.ShowToday;
				}
			}

			// Token: 0x170017A0 RID: 6048
			// (get) Token: 0x06006BC6 RID: 27590 RVA: 0x0018EC9D File Offset: 0x0018CE9D
			internal bool ShowWeekNumbers
			{
				get
				{
					return this.calendar.ShowWeekNumbers;
				}
			}

			// Token: 0x170017A1 RID: 6049
			// (get) Token: 0x06006BC7 RID: 27591 RVA: 0x0018ECAA File Offset: 0x0018CEAA
			internal DateTime TodayDate
			{
				get
				{
					return this.calendar.TodayDate;
				}
			}

			// Token: 0x170017A2 RID: 6050
			// (get) Token: 0x06006BC8 RID: 27592 RVA: 0x0018ECB7 File Offset: 0x0018CEB7
			internal MonthCalendar.CalendarTodayLinkAccessibleObject TodayLinkAccessibleObject
			{
				get
				{
					if (this._todayLinkAccessibleObject == null)
					{
						this._todayLinkAccessibleObject = new MonthCalendar.CalendarTodayLinkAccessibleObject(this);
					}
					return this._todayLinkAccessibleObject;
				}
			}

			// Token: 0x06006BC9 RID: 27593 RVA: 0x0018ECD3 File Offset: 0x0018CED3
			internal void UpdateDisplayRange()
			{
				this.calendar.UpdateDisplayRange();
			}

			// Token: 0x04003BA0 RID: 15264
			private const int MaxCalendarsCount = 12;

			// Token: 0x04003BA1 RID: 15265
			private MonthCalendar.CalendarCellAccessibleObject _focusedCellAccessibleObject;

			// Token: 0x04003BA2 RID: 15266
			private MonthCalendar.CalendarPreviousButtonAccessibleObject _previousButtonAccessibleObject;

			// Token: 0x04003BA3 RID: 15267
			private MonthCalendar.CalendarNextButtonAccessibleObject _nextButtonAccessibleObject;

			// Token: 0x04003BA4 RID: 15268
			private LinkedList<MonthCalendar.CalendarAccessibleObject> _calendarsAccessibleObjects;

			// Token: 0x04003BA5 RID: 15269
			private MonthCalendar.CalendarTodayLinkAccessibleObject _todayLinkAccessibleObject;
		}

		// Token: 0x020006EB RID: 1771
		internal abstract class MonthCalendarChildAccessibleObject : AccessibleObject
		{
			// Token: 0x06006BCA RID: 27594 RVA: 0x0018ECE0 File Offset: 0x0018CEE0
			public MonthCalendarChildAccessibleObject(MonthCalendar.MonthCalendarAccessibleObjectLevel5 calendarAccessibleObject)
			{
				if (calendarAccessibleObject == null)
				{
					throw new ArgumentNullException();
				}
				this._monthCalendarAccessibleObject = calendarAccessibleObject;
			}

			// Token: 0x06006BCB RID: 27595 RVA: 0x0018ECF8 File Offset: 0x0018CEF8
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID <= 30090)
				{
					switch (propertyID)
					{
					case 30005:
						return this.Name;
					case 30006:
					case 30007:
						break;
					case 30008:
						return this.HasKeyboardFocus;
					case 30009:
						return false;
					case 30010:
						return this.IsEnabled;
					default:
						if (propertyID == 30090)
						{
							return this.IsPatternSupported(10018);
						}
						break;
					}
				}
				else
				{
					if (propertyID == 30095)
					{
						return this.Role;
					}
					if (propertyID == 30096)
					{
						return this.State;
					}
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x170017A3 RID: 6051
			// (get) Token: 0x06006BCC RID: 27596 RVA: 0x0001180C File Offset: 0x0000FA0C
			internal virtual bool HasKeyboardFocus
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170017A4 RID: 6052
			// (get) Token: 0x06006BCD RID: 27597 RVA: 0x0018EDA3 File Offset: 0x0018CFA3
			internal virtual bool IsEnabled
			{
				get
				{
					return this._monthCalendarAccessibleObject.IsEnabled;
				}
			}

			// Token: 0x06006BCE RID: 27598 RVA: 0x000F156F File Offset: 0x000EF76F
			internal override bool IsPatternSupported(int patternId)
			{
				return patternId == 10018 || base.IsPatternSupported(patternId);
			}

			// Token: 0x170017A5 RID: 6053
			// (get) Token: 0x06006BCF RID: 27599 RVA: 0x0018EDB0 File Offset: 0x0018CFB0
			internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
			{
				get
				{
					return this._monthCalendarAccessibleObject;
				}
			}

			// Token: 0x06006BD0 RID: 27600 RVA: 0x0018EDB8 File Offset: 0x0018CFB8
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (direction == UnsafeNativeMethods.NavigateDirection.Parent)
				{
					return this.Parent;
				}
				return base.FragmentNavigate(direction);
			}

			// Token: 0x170017A6 RID: 6054
			// (get) Token: 0x06006BD1 RID: 27601 RVA: 0x0018EDCC File Offset: 0x0018CFCC
			internal override int[] RuntimeId
			{
				get
				{
					return new int[]
					{
						42,
						this._monthCalendarAccessibleObject.Owner.InternalHandle.ToInt32(),
						this.GetChildId()
					};
				}
			}

			// Token: 0x170017A7 RID: 6055
			// (get) Token: 0x06006BD2 RID: 27602 RVA: 0x0001180C File Offset: 0x0000FA0C
			public override AccessibleStates State
			{
				get
				{
					return AccessibleStates.None;
				}
			}

			// Token: 0x04003BA6 RID: 15270
			private readonly MonthCalendar.MonthCalendarAccessibleObjectLevel5 _monthCalendarAccessibleObject;
		}
	}
}
