using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	/// <summary>Implements the basic functionality of a scroll bar control.</summary>
	// Token: 0x02000355 RID: 853
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultProperty("Value")]
	[DefaultEvent("Scroll")]
	public abstract class ScrollBar : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ScrollBar" /> class.</summary>
		// Token: 0x060037E7 RID: 14311 RVA: 0x000F98E0 File Offset: 0x000F7AE0
		public ScrollBar()
		{
			base.SetStyle(ControlStyles.UserPaint, false);
			base.SetStyle(ControlStyles.StandardClick, false);
			base.SetStyle(ControlStyles.UseTextForAccessibility, false);
			this.TabStop = false;
			if ((this.CreateParams.Style & 1) != 0)
			{
				this.scrollOrientation = ScrollOrientation.VerticalScroll;
				return;
			}
			this.scrollOrientation = ScrollOrientation.HorizontalScroll;
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ScrollBar" /> is automatically resized to fit its contents.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.ScrollBar" /> should be automatically resized to fit its contents; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x060037E8 RID: 14312 RVA: 0x00011831 File Offset: 0x0000FA31
		// (set) Token: 0x060037E9 RID: 14313 RVA: 0x00011839 File Offset: 0x0000FA39
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool AutoSize
		{
			get
			{
				return base.AutoSize;
			}
			set
			{
				base.AutoSize = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ScrollBar.AutoSize" /> property changes.</summary>
		// Token: 0x14000299 RID: 665
		// (add) Token: 0x060037EA RID: 14314 RVA: 0x00011842 File Offset: 0x0000FA42
		// (remove) Token: 0x060037EB RID: 14315 RVA: 0x0001184B File Offset: 0x0000FA4B
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler AutoSizeChanged
		{
			add
			{
				base.AutoSizeChanged += value;
			}
			remove
			{
				base.AutoSizeChanged -= value;
			}
		}

		/// <summary>Gets or sets the background color for the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.</returns>
		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x060037EC RID: 14316 RVA: 0x0001A049 File Offset: 0x00018249
		// (set) Token: 0x060037ED RID: 14317 RVA: 0x00012D84 File Offset: 0x00010F84
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ScrollBar.BackColor" /> property changes.</summary>
		// Token: 0x1400029A RID: 666
		// (add) Token: 0x060037EE RID: 14318 RVA: 0x00058BF2 File Offset: 0x00056DF2
		// (remove) Token: 0x060037EF RID: 14319 RVA: 0x00058BFB File Offset: 0x00056DFB
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

		/// <summary>Gets or sets the background image displayed in the control.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> that represents the image to display in the background of the control.</returns>
		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x060037F0 RID: 14320 RVA: 0x0001187C File Offset: 0x0000FA7C
		// (set) Token: 0x060037F1 RID: 14321 RVA: 0x00011884 File Offset: 0x0000FA84
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ScrollBar.BackgroundImage" /> property changes.</summary>
		// Token: 0x1400029B RID: 667
		// (add) Token: 0x060037F2 RID: 14322 RVA: 0x0001188D File Offset: 0x0000FA8D
		// (remove) Token: 0x060037F3 RID: 14323 RVA: 0x00011896 File Offset: 0x0000FA96
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

		/// <summary>Gets or sets the background image layout as defined in the <see cref="T:System.Windows.Forms.ImageLayout" /> enumeration.</summary>
		/// <returns>One of the values of <see cref="T:System.Windows.Forms.ImageLayout" /> (Center , None, Stretch, Tile, or Zoom). Tile is the default value.</returns>
		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x060037F4 RID: 14324 RVA: 0x0001189F File Offset: 0x0000FA9F
		// (set) Token: 0x060037F5 RID: 14325 RVA: 0x000118A7 File Offset: 0x0000FAA7
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ScrollBar.BackgroundImageLayout" /> property changes.</summary>
		// Token: 0x1400029C RID: 668
		// (add) Token: 0x060037F6 RID: 14326 RVA: 0x000118B0 File Offset: 0x0000FAB0
		// (remove) Token: 0x060037F7 RID: 14327 RVA: 0x000118B9 File Offset: 0x0000FAB9
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

		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x060037F8 RID: 14328 RVA: 0x000F9958 File Offset: 0x000F7B58
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ClassName = "SCROLLBAR";
				createParams.Style &= -8388609;
				return createParams;
			}
		}

		/// <summary>Gets the default Input Method Editor (IME) mode supported by this control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x060037F9 RID: 14329 RVA: 0x00023BD7 File Offset: 0x00021DD7
		protected override ImeMode DefaultImeMode
		{
			get
			{
				return ImeMode.Disable;
			}
		}

		/// <summary>Gets the default distance between the <see cref="T:System.Windows.Forms.ScrollBar" /> control edges and its contents.</summary>
		/// <returns>
		///   <see cref="F:System.Windows.Forms.Padding.Empty" /> in all cases.</returns>
		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x060037FA RID: 14330 RVA: 0x00019A61 File Offset: 0x00017C61
		protected override Padding DefaultMargin
		{
			get
			{
				return Padding.Empty;
			}
		}

		/// <summary>Provides constants for rescaling the <see cref="T:System.Windows.Forms.ScrollBar" /> control when a DPI change occurs.</summary>
		/// <param name="deviceDpiOld">The DPI value prior to the change.</param>
		/// <param name="deviceDpiNew">The DPI value after the change.</param>
		// Token: 0x060037FB RID: 14331 RVA: 0x000F998A File Offset: 0x000F7B8A
		protected override void RescaleConstantsForDpi(int deviceDpiOld, int deviceDpiNew)
		{
			base.RescaleConstantsForDpi(deviceDpiOld, deviceDpiNew);
			if (DpiHelper.EnableDpiChangedHighDpiImprovements && this.ScaleScrollBarForDpiChange)
			{
				base.Scale((float)deviceDpiNew / (float)deviceDpiOld);
			}
		}

		/// <summary>Gets or sets the foreground color of the scroll bar control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color for this scroll bar control. The default is the foreground color of the parent control.</returns>
		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x060037FC RID: 14332 RVA: 0x0001A0E7 File Offset: 0x000182E7
		// (set) Token: 0x060037FD RID: 14333 RVA: 0x00013024 File Offset: 0x00011224
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ScrollBar.ForeColor" /> property changes.</summary>
		// Token: 0x1400029D RID: 669
		// (add) Token: 0x060037FE RID: 14334 RVA: 0x0005A8EE File Offset: 0x00058AEE
		// (remove) Token: 0x060037FF RID: 14335 RVA: 0x0005A8F7 File Offset: 0x00058AF7
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

		/// <summary>Gets or sets the font of the text displayed by the control.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultFont" /> property.</returns>
		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x06003800 RID: 14336 RVA: 0x0001A0D6 File Offset: 0x000182D6
		// (set) Token: 0x06003801 RID: 14337 RVA: 0x0001A0DE File Offset: 0x000182DE
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				base.Font = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ScrollBar.Font" /> property changes.</summary>
		// Token: 0x1400029E RID: 670
		// (add) Token: 0x06003802 RID: 14338 RVA: 0x0005A900 File Offset: 0x00058B00
		// (remove) Token: 0x06003803 RID: 14339 RVA: 0x0005A909 File Offset: 0x00058B09
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler FontChanged
		{
			add
			{
				base.FontChanged += value;
			}
			remove
			{
				base.FontChanged -= value;
			}
		}

		/// <summary>Gets or sets the Input Method Editor (IME) mode supported by this control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x06003804 RID: 14340 RVA: 0x0001A051 File Offset: 0x00018251
		// (set) Token: 0x06003805 RID: 14341 RVA: 0x0001A059 File Offset: 0x00018259
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ScrollBar.ImeMode" /> property changes.</summary>
		// Token: 0x1400029F RID: 671
		// (add) Token: 0x06003806 RID: 14342 RVA: 0x00023F70 File Offset: 0x00022170
		// (remove) Token: 0x06003807 RID: 14343 RVA: 0x00023F79 File Offset: 0x00022179
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

		/// <summary>Gets or sets a value to be added to or subtracted from the <see cref="P:System.Windows.Forms.ScrollBar.Value" /> property when the scroll box is moved a large distance.</summary>
		/// <returns>A numeric value. The default value is 10.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is less than 0.</exception>
		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x06003808 RID: 14344 RVA: 0x000F99AE File Offset: 0x000F7BAE
		// (set) Token: 0x06003809 RID: 14345 RVA: 0x000F99CC File Offset: 0x000F7BCC
		[SRCategory("CatBehavior")]
		[DefaultValue(10)]
		[SRDescription("ScrollBarLargeChangeDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public int LargeChange
		{
			get
			{
				return Math.Min(this.largeChange, this.maximum - this.minimum + 1);
			}
			set
			{
				if (this.largeChange != value)
				{
					if (value < 0)
					{
						throw new ArgumentOutOfRangeException("LargeChange", SR.GetString("InvalidLowBoundArgumentEx", new object[]
						{
							"LargeChange",
							value.ToString(CultureInfo.CurrentCulture),
							0.ToString(CultureInfo.CurrentCulture)
						}));
					}
					this.largeChange = value;
					this.UpdateScrollInfo();
				}
			}
		}

		/// <summary>Gets or sets the upper limit of values of the scrollable range.</summary>
		/// <returns>A numeric value. The default value is 100.</returns>
		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x0600380A RID: 14346 RVA: 0x000F9A36 File Offset: 0x000F7C36
		// (set) Token: 0x0600380B RID: 14347 RVA: 0x000F9A3E File Offset: 0x000F7C3E
		[SRCategory("CatBehavior")]
		[DefaultValue(100)]
		[SRDescription("ScrollBarMaximumDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public int Maximum
		{
			get
			{
				return this.maximum;
			}
			set
			{
				if (this.maximum != value)
				{
					if (this.minimum > value)
					{
						this.minimum = value;
					}
					if (value < this.value)
					{
						this.Value = value;
					}
					this.maximum = value;
					this.UpdateScrollInfo();
				}
			}
		}

		/// <summary>Gets or sets the lower limit of values of the scrollable range.</summary>
		/// <returns>A numeric value. The default value is 0.</returns>
		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x0600380C RID: 14348 RVA: 0x000F9A76 File Offset: 0x000F7C76
		// (set) Token: 0x0600380D RID: 14349 RVA: 0x000F9A7E File Offset: 0x000F7C7E
		[SRCategory("CatBehavior")]
		[DefaultValue(0)]
		[SRDescription("ScrollBarMinimumDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public int Minimum
		{
			get
			{
				return this.minimum;
			}
			set
			{
				if (this.minimum != value)
				{
					if (this.maximum < value)
					{
						this.maximum = value;
					}
					if (value > this.value)
					{
						this.value = value;
					}
					this.minimum = value;
					this.UpdateScrollInfo();
				}
			}
		}

		/// <summary>Gets or sets the value to be added to or subtracted from the <see cref="P:System.Windows.Forms.ScrollBar.Value" /> property when the scroll box is moved a small distance.</summary>
		/// <returns>A numeric value. The default value is 1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is less than 0.</exception>
		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x0600380E RID: 14350 RVA: 0x000F9AB6 File Offset: 0x000F7CB6
		// (set) Token: 0x0600380F RID: 14351 RVA: 0x000F9ACC File Offset: 0x000F7CCC
		[SRCategory("CatBehavior")]
		[DefaultValue(1)]
		[SRDescription("ScrollBarSmallChangeDescr")]
		public int SmallChange
		{
			get
			{
				return Math.Min(this.smallChange, this.LargeChange);
			}
			set
			{
				if (this.smallChange != value)
				{
					if (value < 0)
					{
						throw new ArgumentOutOfRangeException("SmallChange", SR.GetString("InvalidLowBoundArgumentEx", new object[]
						{
							"SmallChange",
							value.ToString(CultureInfo.CurrentCulture),
							0.ToString(CultureInfo.CurrentCulture)
						}));
					}
					this.smallChange = value;
					this.UpdateScrollInfo();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the user can give the focus to the <see cref="T:System.Windows.Forms.ScrollBar" /> control by using the TAB key.</summary>
		/// <returns>
		///   <see langword="true" /> if the user can give the focus to the control by using the TAB key; otherwise, false. The default is <see langword="false" />.</returns>
		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x06003810 RID: 14352 RVA: 0x000B2395 File Offset: 0x000B0595
		// (set) Token: 0x06003811 RID: 14353 RVA: 0x000B239D File Offset: 0x000B059D
		[DefaultValue(false)]
		public new bool TabStop
		{
			get
			{
				return base.TabStop;
			}
			set
			{
				base.TabStop = value;
			}
		}

		/// <summary>Gets or sets the text associated with this control.</summary>
		/// <returns>The text associated with this control.</returns>
		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x06003812 RID: 14354 RVA: 0x00013814 File Offset: 0x00011A14
		// (set) Token: 0x06003813 RID: 14355 RVA: 0x00023FE9 File Offset: 0x000221E9
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ScrollBar.Text" /> property changes.</summary>
		// Token: 0x140002A0 RID: 672
		// (add) Token: 0x06003814 RID: 14356 RVA: 0x00046591 File Offset: 0x00044791
		// (remove) Token: 0x06003815 RID: 14357 RVA: 0x0004659A File Offset: 0x0004479A
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

		/// <summary>Gets or sets a numeric value that represents the current position of the scroll box on the scroll bar control.</summary>
		/// <returns>A numeric value that is within the <see cref="P:System.Windows.Forms.ScrollBar.Minimum" /> and <see cref="P:System.Windows.Forms.ScrollBar.Maximum" /> range. The default value is 0.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is less than the <see cref="P:System.Windows.Forms.ScrollBar.Minimum" /> property value.  
		///  -or-  
		///  The assigned value is greater than the <see cref="P:System.Windows.Forms.ScrollBar.Maximum" /> property value.</exception>
		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x06003816 RID: 14358 RVA: 0x000F9B36 File Offset: 0x000F7D36
		// (set) Token: 0x06003817 RID: 14359 RVA: 0x000F9B40 File Offset: 0x000F7D40
		[SRCategory("CatBehavior")]
		[DefaultValue(0)]
		[Bindable(true)]
		[SRDescription("ScrollBarValueDescr")]
		public int Value
		{
			get
			{
				return this.value;
			}
			set
			{
				if (this.value != value)
				{
					if (value < this.minimum || value > this.maximum)
					{
						throw new ArgumentOutOfRangeException("Value", SR.GetString("InvalidBoundArgument", new object[]
						{
							"Value",
							value.ToString(CultureInfo.CurrentCulture),
							"'minimum'",
							"'maximum'"
						}));
					}
					this.value = value;
					this.UpdateScrollInfo();
					this.OnValueChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets a flag to let the scrollbar scale according to the DPI of the window.</summary>
		/// <returns>
		///   <see langword="true" /> to let the scrollbar scale according to the DPI of the window; <see langword="false" /> otherwise. The default value is <see langword="true" />.</returns>
		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x06003818 RID: 14360 RVA: 0x000F9BC2 File Offset: 0x000F7DC2
		// (set) Token: 0x06003819 RID: 14361 RVA: 0x000F9BCA File Offset: 0x000F7DCA
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[SRDescription("ControlDpiChangeScale")]
		public bool ScaleScrollBarForDpiChange
		{
			get
			{
				return this.scaleScrollBarForDpiChange;
			}
			set
			{
				this.scaleScrollBarForDpiChange = value;
			}
		}

		/// <summary>Occurs when the control is clicked if the <see cref="F:System.Windows.Forms.ControlStyles.StandardClick" /> bit flag is set to <see langword="true" /> in a derived class.</summary>
		// Token: 0x140002A1 RID: 673
		// (add) Token: 0x0600381A RID: 14362 RVA: 0x00012FD4 File Offset: 0x000111D4
		// (remove) Token: 0x0600381B RID: 14363 RVA: 0x00012FDD File Offset: 0x000111DD
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

		/// <summary>Occurs when the control is redrawn.</summary>
		// Token: 0x140002A2 RID: 674
		// (add) Token: 0x0600381C RID: 14364 RVA: 0x00013D73 File Offset: 0x00011F73
		// (remove) Token: 0x0600381D RID: 14365 RVA: 0x00013D7C File Offset: 0x00011F7C
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

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ScrollBar" /> control is double-clicked.</summary>
		// Token: 0x140002A3 RID: 675
		// (add) Token: 0x0600381E RID: 14366 RVA: 0x00023757 File Offset: 0x00021957
		// (remove) Token: 0x0600381F RID: 14367 RVA: 0x00023760 File Offset: 0x00021960
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

		/// <summary>Occurs when the user clicks the <see cref="T:System.Windows.Forms.ScrollBar" /> control with the mouse.</summary>
		// Token: 0x140002A4 RID: 676
		// (add) Token: 0x06003820 RID: 14368 RVA: 0x00012FE6 File Offset: 0x000111E6
		// (remove) Token: 0x06003821 RID: 14369 RVA: 0x00012FEF File Offset: 0x000111EF
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

		/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.ScrollBar" /> control with the mouse.</summary>
		// Token: 0x140002A5 RID: 677
		// (add) Token: 0x06003822 RID: 14370 RVA: 0x00023769 File Offset: 0x00021969
		// (remove) Token: 0x06003823 RID: 14371 RVA: 0x00023772 File Offset: 0x00021972
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

		/// <summary>Occurs when the mouse pointer is over the control and the user presses a mouse button.</summary>
		// Token: 0x140002A6 RID: 678
		// (add) Token: 0x06003824 RID: 14372 RVA: 0x000B913A File Offset: 0x000B733A
		// (remove) Token: 0x06003825 RID: 14373 RVA: 0x000B9143 File Offset: 0x000B7343
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event MouseEventHandler MouseDown
		{
			add
			{
				base.MouseDown += value;
			}
			remove
			{
				base.MouseDown -= value;
			}
		}

		/// <summary>Occurs when the user moves the mouse pointer over the control and releases a mouse button.</summary>
		// Token: 0x140002A7 RID: 679
		// (add) Token: 0x06003826 RID: 14374 RVA: 0x000B914C File Offset: 0x000B734C
		// (remove) Token: 0x06003827 RID: 14375 RVA: 0x000B9155 File Offset: 0x000B7355
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event MouseEventHandler MouseUp
		{
			add
			{
				base.MouseUp += value;
			}
			remove
			{
				base.MouseUp -= value;
			}
		}

		/// <summary>Occurs when the user moves the mouse pointer over the control.</summary>
		// Token: 0x140002A8 RID: 680
		// (add) Token: 0x06003828 RID: 14376 RVA: 0x00011A7E File Offset: 0x0000FC7E
		// (remove) Token: 0x06003829 RID: 14377 RVA: 0x00011A87 File Offset: 0x0000FC87
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event MouseEventHandler MouseMove
		{
			add
			{
				base.MouseMove += value;
			}
			remove
			{
				base.MouseMove -= value;
			}
		}

		/// <summary>Occurs when the scroll box has been moved by either a mouse or keyboard action.</summary>
		// Token: 0x140002A9 RID: 681
		// (add) Token: 0x0600382A RID: 14378 RVA: 0x000F9BD3 File Offset: 0x000F7DD3
		// (remove) Token: 0x0600382B RID: 14379 RVA: 0x000F9BE6 File Offset: 0x000F7DE6
		[SRCategory("CatAction")]
		[SRDescription("ScrollBarOnScrollDescr")]
		public event ScrollEventHandler Scroll
		{
			add
			{
				base.Events.AddHandler(ScrollBar.EVENT_SCROLL, value);
			}
			remove
			{
				base.Events.RemoveHandler(ScrollBar.EVENT_SCROLL, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ScrollBar.Value" /> property is changed, either by a <see cref="E:System.Windows.Forms.ScrollBar.Scroll" /> event or programmatically.</summary>
		// Token: 0x140002AA RID: 682
		// (add) Token: 0x0600382C RID: 14380 RVA: 0x000F9BF9 File Offset: 0x000F7DF9
		// (remove) Token: 0x0600382D RID: 14381 RVA: 0x000F9C0C File Offset: 0x000F7E0C
		[SRCategory("CatAction")]
		[SRDescription("valueChangedEventDescr")]
		public event EventHandler ValueChanged
		{
			add
			{
				base.Events.AddHandler(ScrollBar.EVENT_VALUECHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(ScrollBar.EVENT_VALUECHANGED, value);
			}
		}

		/// <summary>Returns the bounds to use when the <see cref="T:System.Windows.Forms.ScrollBar" /> is scaled by a specified amount.</summary>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that specifies the initial bounds.</param>
		/// <param name="factor">A <see cref="T:System.Drawing.SizeF" /> that indicates the amount the current bounds should be increased by.</param>
		/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values that indicate the how to define the control's size and position returned by <see cref="M:System.Windows.Forms.ScrollBar.GetScaledBounds(System.Drawing.Rectangle,System.Drawing.SizeF,System.Windows.Forms.BoundsSpecified)" />.</param>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> specifying the scaled bounds.</returns>
		// Token: 0x0600382E RID: 14382 RVA: 0x000F9C1F File Offset: 0x000F7E1F
		protected override Rectangle GetScaledBounds(Rectangle bounds, SizeF factor, BoundsSpecified specified)
		{
			if (this.scrollOrientation == ScrollOrientation.VerticalScroll)
			{
				specified &= ~BoundsSpecified.Width;
			}
			else
			{
				specified &= ~BoundsSpecified.Height;
			}
			return base.GetScaledBounds(bounds, factor, specified);
		}

		// Token: 0x0600382F RID: 14383 RVA: 0x000F9C41 File Offset: 0x000F7E41
		internal override IntPtr InitializeDCForWmCtlColor(IntPtr dc, int msg)
		{
			return IntPtr.Zero;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.EnabledChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003830 RID: 14384 RVA: 0x000F9C48 File Offset: 0x000F7E48
		protected override void OnEnabledChanged(EventArgs e)
		{
			if (base.Enabled)
			{
				this.UpdateScrollInfo();
			}
			base.OnEnabledChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003831 RID: 14385 RVA: 0x000F9C5F File Offset: 0x000F7E5F
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			this.UpdateScrollInfo();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ScrollBar.Scroll" /> event.</summary>
		/// <param name="se">A <see cref="T:System.Windows.Forms.ScrollEventArgs" /> that contains the event data.</param>
		// Token: 0x06003832 RID: 14386 RVA: 0x000F9C70 File Offset: 0x000F7E70
		protected virtual void OnScroll(ScrollEventArgs se)
		{
			ScrollEventHandler scrollEventHandler = (ScrollEventHandler)base.Events[ScrollBar.EVENT_SCROLL];
			if (scrollEventHandler != null)
			{
				scrollEventHandler(this, se);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseWheel" /> event</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /></param>
		// Token: 0x06003833 RID: 14387 RVA: 0x000F9CA0 File Offset: 0x000F7EA0
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			this.wheelDelta += e.Delta;
			bool flag = false;
			while (Math.Abs(this.wheelDelta) >= 120)
			{
				if (this.wheelDelta > 0)
				{
					this.wheelDelta -= 120;
					this.DoScroll(ScrollEventType.SmallDecrement);
					flag = true;
				}
				else
				{
					this.wheelDelta += 120;
					this.DoScroll(ScrollEventType.SmallIncrement);
					flag = true;
				}
			}
			if (flag)
			{
				this.DoScroll(ScrollEventType.EndScroll);
			}
			if (e is HandledMouseEventArgs)
			{
				((HandledMouseEventArgs)e).Handled = true;
			}
			base.OnMouseWheel(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ScrollBar.ValueChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003834 RID: 14388 RVA: 0x000F9D34 File Offset: 0x000F7F34
		protected virtual void OnValueChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ScrollBar.EVENT_VALUECHANGED];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06003835 RID: 14389 RVA: 0x000F9D62 File Offset: 0x000F7F62
		private int ReflectPosition(int position)
		{
			if (this is HScrollBar)
			{
				return this.minimum + (this.maximum - this.LargeChange + 1) - position;
			}
			return position;
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.ScrollBar" /> control.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.ScrollBar" />.</returns>
		// Token: 0x06003836 RID: 14390 RVA: 0x000F9D88 File Offset: 0x000F7F88
		public override string ToString()
		{
			string text = base.ToString();
			return string.Concat(new string[]
			{
				text,
				", Minimum: ",
				this.Minimum.ToString(CultureInfo.CurrentCulture),
				", Maximum: ",
				this.Maximum.ToString(CultureInfo.CurrentCulture),
				", Value: ",
				this.Value.ToString(CultureInfo.CurrentCulture)
			});
		}

		/// <summary>Updates the <see cref="T:System.Windows.Forms.ScrollBar" /> control.</summary>
		// Token: 0x06003837 RID: 14391 RVA: 0x000F9E08 File Offset: 0x000F8008
		protected void UpdateScrollInfo()
		{
			if (base.IsHandleCreated && base.Enabled)
			{
				NativeMethods.SCROLLINFO scrollinfo = new NativeMethods.SCROLLINFO();
				scrollinfo.cbSize = Marshal.SizeOf(typeof(NativeMethods.SCROLLINFO));
				scrollinfo.fMask = 23;
				scrollinfo.nMin = this.minimum;
				scrollinfo.nMax = this.maximum;
				scrollinfo.nPage = this.LargeChange;
				if (this.RightToLeft == RightToLeft.Yes)
				{
					scrollinfo.nPos = this.ReflectPosition(this.value);
				}
				else
				{
					scrollinfo.nPos = this.value;
				}
				scrollinfo.nTrackPos = 0;
				UnsafeNativeMethods.SetScrollInfo(new HandleRef(this, base.Handle), 2, scrollinfo, true);
			}
		}

		// Token: 0x06003838 RID: 14392 RVA: 0x000F9EB8 File Offset: 0x000F80B8
		private void WmReflectScroll(ref Message m)
		{
			ScrollEventType scrollEventType = (ScrollEventType)NativeMethods.Util.LOWORD(m.WParam);
			this.DoScroll(scrollEventType);
		}

		// Token: 0x06003839 RID: 14393 RVA: 0x000F9ED8 File Offset: 0x000F80D8
		private void DoScroll(ScrollEventType type)
		{
			if (this.RightToLeft == RightToLeft.Yes)
			{
				switch (type)
				{
				case ScrollEventType.SmallDecrement:
					type = ScrollEventType.SmallIncrement;
					break;
				case ScrollEventType.SmallIncrement:
					type = ScrollEventType.SmallDecrement;
					break;
				case ScrollEventType.LargeDecrement:
					type = ScrollEventType.LargeIncrement;
					break;
				case ScrollEventType.LargeIncrement:
					type = ScrollEventType.LargeDecrement;
					break;
				case ScrollEventType.First:
					type = ScrollEventType.Last;
					break;
				case ScrollEventType.Last:
					type = ScrollEventType.First;
					break;
				}
			}
			int num = this.value;
			int num2 = this.value;
			switch (type)
			{
			case ScrollEventType.SmallDecrement:
				num = Math.Max(this.value - this.SmallChange, this.minimum);
				break;
			case ScrollEventType.SmallIncrement:
				num = Math.Min(this.value + this.SmallChange, this.maximum - this.LargeChange + 1);
				break;
			case ScrollEventType.LargeDecrement:
				num = Math.Max(this.value - this.LargeChange, this.minimum);
				break;
			case ScrollEventType.LargeIncrement:
				num = Math.Min(this.value + this.LargeChange, this.maximum - this.LargeChange + 1);
				break;
			case ScrollEventType.ThumbPosition:
			case ScrollEventType.ThumbTrack:
			{
				NativeMethods.SCROLLINFO scrollinfo = new NativeMethods.SCROLLINFO();
				scrollinfo.fMask = 16;
				SafeNativeMethods.GetScrollInfo(new HandleRef(this, base.Handle), 2, scrollinfo);
				if (this.RightToLeft == RightToLeft.Yes)
				{
					num = this.ReflectPosition(scrollinfo.nTrackPos);
				}
				else
				{
					num = scrollinfo.nTrackPos;
				}
				break;
			}
			case ScrollEventType.First:
				num = this.minimum;
				break;
			case ScrollEventType.Last:
				num = this.maximum - this.LargeChange + 1;
				break;
			}
			ScrollEventArgs scrollEventArgs = new ScrollEventArgs(type, num2, num, this.scrollOrientation);
			this.OnScroll(scrollEventArgs);
			this.Value = scrollEventArgs.NewValue;
		}

		/// <summary>Overrides the <see cref="M:System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@)" /> method.</summary>
		/// <param name="m">A Windows Message object.</param>
		// Token: 0x0600383A RID: 14394 RVA: 0x000FA074 File Offset: 0x000F8274
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg != 5)
			{
				if (msg != 20)
				{
					if (msg - 8468 <= 1)
					{
						this.WmReflectScroll(ref m);
						return;
					}
					base.WndProc(ref m);
				}
			}
			else if (UnsafeNativeMethods.GetFocus() == base.Handle)
			{
				this.DefWndProc(ref m);
				base.SendMessage(8, 0, 0);
				base.SendMessage(7, 0, 0);
				return;
			}
		}

		// Token: 0x04002178 RID: 8568
		private static readonly object EVENT_SCROLL = new object();

		// Token: 0x04002179 RID: 8569
		private static readonly object EVENT_VALUECHANGED = new object();

		// Token: 0x0400217A RID: 8570
		private int minimum;

		// Token: 0x0400217B RID: 8571
		private int maximum = 100;

		// Token: 0x0400217C RID: 8572
		private int smallChange = 1;

		// Token: 0x0400217D RID: 8573
		private int largeChange = 10;

		// Token: 0x0400217E RID: 8574
		private int value;

		// Token: 0x0400217F RID: 8575
		private ScrollOrientation scrollOrientation;

		// Token: 0x04002180 RID: 8576
		private int wheelDelta;

		// Token: 0x04002181 RID: 8577
		private bool scaleScrollBarForDpiChange = true;
	}
}
