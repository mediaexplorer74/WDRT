using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Represents a standard Windows track bar.</summary>
	// Token: 0x0200040C RID: 1036
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultProperty("Value")]
	[DefaultEvent("Scroll")]
	[DefaultBindingProperty("Value")]
	[Designer("System.Windows.Forms.Design.TrackBarDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionTrackBar")]
	public class TrackBar : Control, ISupportInitialize
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TrackBar" /> class.</summary>
		// Token: 0x060047DE RID: 18398 RVA: 0x0012F484 File Offset: 0x0012D684
		public TrackBar()
		{
			base.SetStyle(ControlStyles.UserPaint, false);
			base.SetStyle(ControlStyles.UseTextForAccessibility, false);
			this.requestedDim = this.PreferredDimension;
		}

		/// <summary>Gets or sets a value indicating whether the height or width of the track bar is being automatically sized.</summary>
		/// <returns>
		///   <see langword="true" /> if the track bar is being automatically sized; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x1700119B RID: 4507
		// (get) Token: 0x060047DF RID: 18399 RVA: 0x0012F4E2 File Offset: 0x0012D6E2
		// (set) Token: 0x060047E0 RID: 18400 RVA: 0x0012F4EC File Offset: 0x0012D6EC
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("TrackBarAutoSizeDescr")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override bool AutoSize
		{
			get
			{
				return this.autoSize;
			}
			set
			{
				if (this.autoSize != value)
				{
					this.autoSize = value;
					if (this.orientation == Orientation.Horizontal)
					{
						base.SetStyle(ControlStyles.FixedHeight, this.autoSize);
						base.SetStyle(ControlStyles.FixedWidth, false);
					}
					else
					{
						base.SetStyle(ControlStyles.FixedWidth, this.autoSize);
						base.SetStyle(ControlStyles.FixedHeight, false);
					}
					this.AdjustSize();
					this.OnAutoSizeChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TrackBar.AutoSize" /> property changes.</summary>
		// Token: 0x14000390 RID: 912
		// (add) Token: 0x060047E1 RID: 18401 RVA: 0x00011842 File Offset: 0x0000FA42
		// (remove) Token: 0x060047E2 RID: 18402 RVA: 0x0001184B File Offset: 0x0000FA4B
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnAutoSizeChangedDescr")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
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

		/// <summary>Gets or sets the background image for the <see cref="T:System.Windows.Forms.TrackBar" /> control.</summary>
		/// <returns>The <see cref="T:System.Drawing.Image" /> that is the background image for the <see cref="T:System.Windows.Forms.TrackBar" />.</returns>
		// Token: 0x1700119C RID: 4508
		// (get) Token: 0x060047E3 RID: 18403 RVA: 0x0001187C File Offset: 0x0000FA7C
		// (set) Token: 0x060047E4 RID: 18404 RVA: 0x00011884 File Offset: 0x0000FA84
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.TrackBar.BackgroundImage" /> property changes.</summary>
		// Token: 0x14000391 RID: 913
		// (add) Token: 0x060047E5 RID: 18405 RVA: 0x0001188D File Offset: 0x0000FA8D
		// (remove) Token: 0x060047E6 RID: 18406 RVA: 0x00011896 File Offset: 0x0000FA96
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

		/// <summary>Gets or sets an <see cref="T:System.Windows.Forms.ImageLayout" /> value; however, setting this property has no effect on the <see cref="T:System.Windows.Forms.TrackBar" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImageLayout" /> values.</returns>
		// Token: 0x1700119D RID: 4509
		// (get) Token: 0x060047E7 RID: 18407 RVA: 0x0001189F File Offset: 0x0000FA9F
		// (set) Token: 0x060047E8 RID: 18408 RVA: 0x000118A7 File Offset: 0x0000FAA7
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.TrackBar.BackgroundImageLayout" /> property changes.</summary>
		// Token: 0x14000392 RID: 914
		// (add) Token: 0x060047E9 RID: 18409 RVA: 0x000118B0 File Offset: 0x0000FAB0
		// (remove) Token: 0x060047EA RID: 18410 RVA: 0x000118B9 File Offset: 0x0000FAB9
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

		/// <summary>Overrides the <see cref="P:System.Windows.Forms.Control.CreateParams" /> property.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x1700119E RID: 4510
		// (get) Token: 0x060047EB RID: 18411 RVA: 0x0012F554 File Offset: 0x0012D754
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ClassName = "msctls_trackbar32";
				switch (this.tickStyle)
				{
				case TickStyle.None:
					createParams.Style |= 16;
					break;
				case TickStyle.TopLeft:
					createParams.Style |= 5;
					break;
				case TickStyle.BottomRight:
					createParams.Style |= 1;
					break;
				case TickStyle.Both:
					createParams.Style |= 9;
					break;
				}
				if (this.orientation == Orientation.Vertical)
				{
					createParams.Style |= 2;
				}
				if (this.RightToLeft == RightToLeft.Yes && this.RightToLeftLayout)
				{
					createParams.ExStyle |= 5242880;
					createParams.ExStyle &= -28673;
				}
				return createParams;
			}
		}

		/// <summary>Gets a value indicating the mode for the Input Method Editor (IME) for the <see cref="T:System.Windows.Forms.TrackBar" />.</summary>
		/// <returns>Always <see cref="F:System.Windows.Forms.ImeMode.Disable" />.</returns>
		// Token: 0x1700119F RID: 4511
		// (get) Token: 0x060047EC RID: 18412 RVA: 0x00023BD7 File Offset: 0x00021DD7
		protected override ImeMode DefaultImeMode
		{
			get
			{
				return ImeMode.Disable;
			}
		}

		/// <summary>Gets the default size of the <see cref="T:System.Windows.Forms.TrackBar" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> representing the default size of the control.</returns>
		// Token: 0x170011A0 RID: 4512
		// (get) Token: 0x060047ED RID: 18413 RVA: 0x0012F61F File Offset: 0x0012D81F
		protected override Size DefaultSize
		{
			get
			{
				return new Size(104, this.PreferredDimension);
			}
		}

		/// <summary>Gets or sets a value indicating whether this control should redraw its surface using a secondary buffer to reduce or prevent flicker; however, this property has no effect on the <see cref="T:System.Windows.Forms.TrackBar" /> control</summary>
		/// <returns>
		///   <see langword="true" /> if the control has a secondary buffer; otherwise, <see langword="false" />.</returns>
		// Token: 0x170011A1 RID: 4513
		// (get) Token: 0x060047EE RID: 18414 RVA: 0x00012FC3 File Offset: 0x000111C3
		// (set) Token: 0x060047EF RID: 18415 RVA: 0x00012FCB File Offset: 0x000111CB
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

		/// <summary>Overrides <see cref="P:System.Windows.Forms.Control.Font" /></summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultFont" /> property.</returns>
		// Token: 0x170011A2 RID: 4514
		// (get) Token: 0x060047F0 RID: 18416 RVA: 0x0001A0D6 File Offset: 0x000182D6
		// (set) Token: 0x060047F1 RID: 18417 RVA: 0x0001A0DE File Offset: 0x000182DE
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.TrackBar.Font" /> property changes.</summary>
		// Token: 0x14000393 RID: 915
		// (add) Token: 0x060047F2 RID: 18418 RVA: 0x0005A900 File Offset: 0x00058B00
		// (remove) Token: 0x060047F3 RID: 18419 RVA: 0x0005A909 File Offset: 0x00058B09
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

		/// <summary>Gets the foreground color of the track bar.</summary>
		/// <returns>Always <see cref="P:System.Drawing.SystemColors.WindowText" />.</returns>
		// Token: 0x170011A3 RID: 4515
		// (get) Token: 0x060047F4 RID: 18420 RVA: 0x0012F62E File Offset: 0x0012D82E
		// (set) Token: 0x060047F5 RID: 18421 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color ForeColor
		{
			get
			{
				return SystemColors.WindowText;
			}
			set
			{
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.TrackBar.ForeColor" /> property changes.</summary>
		// Token: 0x14000394 RID: 916
		// (add) Token: 0x060047F6 RID: 18422 RVA: 0x0005A8EE File Offset: 0x00058AEE
		// (remove) Token: 0x060047F7 RID: 18423 RVA: 0x0005A8F7 File Offset: 0x00058AF7
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

		/// <summary>Gets or sets the Input Method Editor (IME) mode supported by this control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
		// Token: 0x170011A4 RID: 4516
		// (get) Token: 0x060047F8 RID: 18424 RVA: 0x0001A051 File Offset: 0x00018251
		// (set) Token: 0x060047F9 RID: 18425 RVA: 0x0001A059 File Offset: 0x00018259
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.TrackBar.ImeMode" /> property changes.</summary>
		// Token: 0x14000395 RID: 917
		// (add) Token: 0x060047FA RID: 18426 RVA: 0x00023F70 File Offset: 0x00022170
		// (remove) Token: 0x060047FB RID: 18427 RVA: 0x00023F79 File Offset: 0x00022179
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

		/// <summary>Gets or sets a value to be added to or subtracted from the <see cref="P:System.Windows.Forms.TrackBar.Value" /> property when the scroll box is moved a large distance.</summary>
		/// <returns>A numeric value. The default is 5.</returns>
		/// <exception cref="T:System.ArgumentException">The assigned value is less than 0.</exception>
		// Token: 0x170011A5 RID: 4517
		// (get) Token: 0x060047FC RID: 18428 RVA: 0x0012F635 File Offset: 0x0012D835
		// (set) Token: 0x060047FD RID: 18429 RVA: 0x0012F640 File Offset: 0x0012D840
		[SRCategory("CatBehavior")]
		[DefaultValue(5)]
		[SRDescription("TrackBarLargeChangeDescr")]
		public int LargeChange
		{
			get
			{
				return this.largeChange;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("LargeChange", SR.GetString("TrackBarLargeChangeError", new object[] { value }));
				}
				if (this.largeChange != value)
				{
					this.largeChange = value;
					if (base.IsHandleCreated)
					{
						base.SendMessage(1045, 0, value);
					}
				}
			}
		}

		/// <summary>Gets or sets the upper limit of the range this <see cref="T:System.Windows.Forms.TrackBar" /> is working with.</summary>
		/// <returns>The maximum value for the <see cref="T:System.Windows.Forms.TrackBar" />. The default is 10.</returns>
		// Token: 0x170011A6 RID: 4518
		// (get) Token: 0x060047FE RID: 18430 RVA: 0x0012F69B File Offset: 0x0012D89B
		// (set) Token: 0x060047FF RID: 18431 RVA: 0x0012F6A3 File Offset: 0x0012D8A3
		[SRCategory("CatBehavior")]
		[DefaultValue(10)]
		[RefreshProperties(RefreshProperties.All)]
		[SRDescription("TrackBarMaximumDescr")]
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
					if (value < this.minimum)
					{
						this.minimum = value;
					}
					this.SetRange(this.minimum, value);
				}
			}
		}

		/// <summary>Gets or sets the lower limit of the range this <see cref="T:System.Windows.Forms.TrackBar" /> is working with.</summary>
		/// <returns>The minimum value for the <see cref="T:System.Windows.Forms.TrackBar" />. The default is 0.</returns>
		// Token: 0x170011A7 RID: 4519
		// (get) Token: 0x06004800 RID: 18432 RVA: 0x0012F6CB File Offset: 0x0012D8CB
		// (set) Token: 0x06004801 RID: 18433 RVA: 0x0012F6D3 File Offset: 0x0012D8D3
		[SRCategory("CatBehavior")]
		[DefaultValue(0)]
		[RefreshProperties(RefreshProperties.All)]
		[SRDescription("TrackBarMinimumDescr")]
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
					if (value > this.maximum)
					{
						this.maximum = value;
					}
					this.SetRange(value, this.maximum);
				}
			}
		}

		/// <summary>Gets or sets a value indicating the horizontal or vertical orientation of the track bar.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.Orientation" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.Orientation" /> values.</exception>
		// Token: 0x170011A8 RID: 4520
		// (get) Token: 0x06004802 RID: 18434 RVA: 0x0012F6FB File Offset: 0x0012D8FB
		// (set) Token: 0x06004803 RID: 18435 RVA: 0x0012F704 File Offset: 0x0012D904
		[SRCategory("CatAppearance")]
		[DefaultValue(Orientation.Horizontal)]
		[Localizable(true)]
		[SRDescription("TrackBarOrientationDescr")]
		public Orientation Orientation
		{
			get
			{
				return this.orientation;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 1))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(Orientation));
				}
				if (this.orientation != value)
				{
					this.orientation = value;
					if (this.orientation == Orientation.Horizontal)
					{
						base.SetStyle(ControlStyles.FixedHeight, this.autoSize);
						base.SetStyle(ControlStyles.FixedWidth, false);
						base.Width = this.requestedDim;
					}
					else
					{
						base.SetStyle(ControlStyles.FixedHeight, false);
						base.SetStyle(ControlStyles.FixedWidth, this.autoSize);
						base.Height = this.requestedDim;
					}
					if (base.IsHandleCreated)
					{
						Rectangle bounds = base.Bounds;
						base.RecreateHandle();
						base.SetBounds(bounds.X, bounds.Y, bounds.Height, bounds.Width, BoundsSpecified.All);
						this.AdjustSize();
					}
				}
			}
		}

		/// <summary>Gets or sets the space between the edges of a <see cref="T:System.Windows.Forms.TrackBar" /> control and its contents.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> object.</returns>
		// Token: 0x170011A9 RID: 4521
		// (get) Token: 0x06004804 RID: 18436 RVA: 0x00013442 File Offset: 0x00011642
		// (set) Token: 0x06004805 RID: 18437 RVA: 0x0001344A File Offset: 0x0001164A
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TrackBar.Padding" /> property changes.</summary>
		// Token: 0x14000396 RID: 918
		// (add) Token: 0x06004806 RID: 18438 RVA: 0x00013453 File Offset: 0x00011653
		// (remove) Token: 0x06004807 RID: 18439 RVA: 0x0001345C File Offset: 0x0001165C
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

		// Token: 0x170011AA RID: 4522
		// (get) Token: 0x06004808 RID: 18440 RVA: 0x0012F7DC File Offset: 0x0012D9DC
		private int PreferredDimension
		{
			get
			{
				int systemMetrics = UnsafeNativeMethods.GetSystemMetrics(3);
				return systemMetrics * 8 / 3;
			}
		}

		// Token: 0x06004809 RID: 18441 RVA: 0x0012F7F5 File Offset: 0x0012D9F5
		private void RedrawControl()
		{
			if (base.IsHandleCreated)
			{
				base.SendMessage(1032, 1, this.maximum);
				base.Invalidate();
			}
		}

		/// <summary>Gets or sets a value indicating whether the contents of the <see cref="T:System.Windows.Forms.TrackBar" /> will be laid out from right to left.</summary>
		/// <returns>
		///   <see langword="true" /> if the contents of the <see cref="T:System.Windows.Forms.TrackBar" /> are laid out from right to left; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170011AB RID: 4523
		// (get) Token: 0x0600480A RID: 18442 RVA: 0x0012F818 File Offset: 0x0012DA18
		// (set) Token: 0x0600480B RID: 18443 RVA: 0x0012F820 File Offset: 0x0012DA20
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

		/// <summary>Gets or sets the value added to or subtracted from the <see cref="P:System.Windows.Forms.TrackBar.Value" /> property when the scroll box is moved a small distance.</summary>
		/// <returns>A numeric value. The default value is 1.</returns>
		/// <exception cref="T:System.ArgumentException">The assigned value is less than 0.</exception>
		// Token: 0x170011AC RID: 4524
		// (get) Token: 0x0600480C RID: 18444 RVA: 0x0012F874 File Offset: 0x0012DA74
		// (set) Token: 0x0600480D RID: 18445 RVA: 0x0012F87C File Offset: 0x0012DA7C
		[SRCategory("CatBehavior")]
		[DefaultValue(1)]
		[SRDescription("TrackBarSmallChangeDescr")]
		public int SmallChange
		{
			get
			{
				return this.smallChange;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("SmallChange", SR.GetString("TrackBarSmallChangeError", new object[] { value }));
				}
				if (this.smallChange != value)
				{
					this.smallChange = value;
					if (base.IsHandleCreated)
					{
						base.SendMessage(1047, 0, value);
					}
				}
			}
		}

		/// <summary>Gets or sets the text of the <see cref="T:System.Windows.Forms.TrackBar" />.</summary>
		/// <returns>The text associated with this control.</returns>
		// Token: 0x170011AD RID: 4525
		// (get) Token: 0x0600480E RID: 18446 RVA: 0x00013814 File Offset: 0x00011A14
		// (set) Token: 0x0600480F RID: 18447 RVA: 0x00023FE9 File Offset: 0x000221E9
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Bindable(false)]
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.TrackBar.Text" /> property changes.</summary>
		// Token: 0x14000397 RID: 919
		// (add) Token: 0x06004810 RID: 18448 RVA: 0x00046591 File Offset: 0x00044791
		// (remove) Token: 0x06004811 RID: 18449 RVA: 0x0004659A File Offset: 0x0004479A
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

		/// <summary>Gets or sets a value indicating how to display the tick marks on the track bar.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.TickStyle" /> values. The default is <see cref="F:System.Windows.Forms.TickStyle.BottomRight" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not a valid <see cref="T:System.Windows.Forms.TickStyle" />.</exception>
		// Token: 0x170011AE RID: 4526
		// (get) Token: 0x06004812 RID: 18450 RVA: 0x0012F8D7 File Offset: 0x0012DAD7
		// (set) Token: 0x06004813 RID: 18451 RVA: 0x0012F8DF File Offset: 0x0012DADF
		[SRCategory("CatAppearance")]
		[DefaultValue(TickStyle.BottomRight)]
		[SRDescription("TrackBarTickStyleDescr")]
		public TickStyle TickStyle
		{
			get
			{
				return this.tickStyle;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(TickStyle));
				}
				if (this.tickStyle != value)
				{
					this.tickStyle = value;
					base.RecreateHandle();
				}
			}
		}

		/// <summary>Gets or sets a value that specifies the delta between ticks drawn on the control.</summary>
		/// <returns>The numeric value representing the delta between ticks. The default is 1.</returns>
		// Token: 0x170011AF RID: 4527
		// (get) Token: 0x06004814 RID: 18452 RVA: 0x0012F91D File Offset: 0x0012DB1D
		// (set) Token: 0x06004815 RID: 18453 RVA: 0x0012F925 File Offset: 0x0012DB25
		[SRCategory("CatAppearance")]
		[DefaultValue(1)]
		[SRDescription("TrackBarTickFrequencyDescr")]
		public int TickFrequency
		{
			get
			{
				return this.tickFrequency;
			}
			set
			{
				if (this.tickFrequency != value)
				{
					this.tickFrequency = value;
					if (base.IsHandleCreated)
					{
						base.SendMessage(1044, value, 0);
						base.Invalidate();
					}
				}
			}
		}

		/// <summary>Gets or sets a numeric value that represents the current position of the scroll box on the track bar.</summary>
		/// <returns>A numeric value that is within the <see cref="P:System.Windows.Forms.TrackBar.Minimum" /> and <see cref="P:System.Windows.Forms.TrackBar.Maximum" /> range. The default value is 0.</returns>
		/// <exception cref="T:System.ArgumentException">The assigned value is less than the value of <see cref="P:System.Windows.Forms.TrackBar.Minimum" />.  
		///  -or-  
		///  The assigned value is greater than the value of <see cref="P:System.Windows.Forms.TrackBar.Maximum" />.</exception>
		// Token: 0x170011B0 RID: 4528
		// (get) Token: 0x06004816 RID: 18454 RVA: 0x0012F953 File Offset: 0x0012DB53
		// (set) Token: 0x06004817 RID: 18455 RVA: 0x0012F964 File Offset: 0x0012DB64
		[SRCategory("CatBehavior")]
		[DefaultValue(0)]
		[Bindable(true)]
		[SRDescription("TrackBarValueDescr")]
		public int Value
		{
			get
			{
				this.GetTrackBarValue();
				return this.value;
			}
			set
			{
				if (this.value != value)
				{
					if (!this.initializing && (value < this.minimum || value > this.maximum))
					{
						throw new ArgumentOutOfRangeException("Value", SR.GetString("InvalidBoundArgument", new object[]
						{
							"Value",
							value.ToString(CultureInfo.CurrentCulture),
							"'Minimum'",
							"'Maximum'"
						}));
					}
					this.value = value;
					this.SetTrackBarPosition();
					this.OnValueChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the user clicks the <see cref="T:System.Windows.Forms.TrackBar" /> control.</summary>
		// Token: 0x14000398 RID: 920
		// (add) Token: 0x06004818 RID: 18456 RVA: 0x00012FD4 File Offset: 0x000111D4
		// (remove) Token: 0x06004819 RID: 18457 RVA: 0x00012FDD File Offset: 0x000111DD
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

		/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.TrackBar" /> control.</summary>
		// Token: 0x14000399 RID: 921
		// (add) Token: 0x0600481A RID: 18458 RVA: 0x00023757 File Offset: 0x00021957
		// (remove) Token: 0x0600481B RID: 18459 RVA: 0x00023760 File Offset: 0x00021960
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

		/// <summary>Occurs when the user clicks the <see cref="T:System.Windows.Forms.TrackBar" /> control.</summary>
		// Token: 0x1400039A RID: 922
		// (add) Token: 0x0600481C RID: 18460 RVA: 0x00012FE6 File Offset: 0x000111E6
		// (remove) Token: 0x0600481D RID: 18461 RVA: 0x00012FEF File Offset: 0x000111EF
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

		/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.TrackBar" /> control.</summary>
		// Token: 0x1400039B RID: 923
		// (add) Token: 0x0600481E RID: 18462 RVA: 0x00023769 File Offset: 0x00021969
		// (remove) Token: 0x0600481F RID: 18463 RVA: 0x00023772 File Offset: 0x00021972
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TrackBar.RightToLeftLayout" /> property changes.</summary>
		// Token: 0x1400039C RID: 924
		// (add) Token: 0x06004820 RID: 18464 RVA: 0x0012F9EE File Offset: 0x0012DBEE
		// (remove) Token: 0x06004821 RID: 18465 RVA: 0x0012FA01 File Offset: 0x0012DC01
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnRightToLeftLayoutChangedDescr")]
		public event EventHandler RightToLeftLayoutChanged
		{
			add
			{
				base.Events.AddHandler(TrackBar.EVENT_RIGHTTOLEFTLAYOUTCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(TrackBar.EVENT_RIGHTTOLEFTLAYOUTCHANGED, value);
			}
		}

		/// <summary>Occurs when either a mouse or keyboard action moves the scroll box.</summary>
		// Token: 0x1400039D RID: 925
		// (add) Token: 0x06004822 RID: 18466 RVA: 0x0012FA14 File Offset: 0x0012DC14
		// (remove) Token: 0x06004823 RID: 18467 RVA: 0x0012FA27 File Offset: 0x0012DC27
		[SRCategory("CatBehavior")]
		[SRDescription("TrackBarOnScrollDescr")]
		public event EventHandler Scroll
		{
			add
			{
				base.Events.AddHandler(TrackBar.EVENT_SCROLL, value);
			}
			remove
			{
				base.Events.RemoveHandler(TrackBar.EVENT_SCROLL, value);
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.TrackBar" /> control is drawn.</summary>
		// Token: 0x1400039E RID: 926
		// (add) Token: 0x06004824 RID: 18468 RVA: 0x00013D73 File Offset: 0x00011F73
		// (remove) Token: 0x06004825 RID: 18469 RVA: 0x00013D7C File Offset: 0x00011F7C
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.TrackBar.Value" /> property of a track bar changes, either by movement of the scroll box or by manipulation in code.</summary>
		// Token: 0x1400039F RID: 927
		// (add) Token: 0x06004826 RID: 18470 RVA: 0x0012FA3A File Offset: 0x0012DC3A
		// (remove) Token: 0x06004827 RID: 18471 RVA: 0x0012FA4D File Offset: 0x0012DC4D
		[SRCategory("CatAction")]
		[SRDescription("valueChangedEventDescr")]
		public event EventHandler ValueChanged
		{
			add
			{
				base.Events.AddHandler(TrackBar.EVENT_VALUECHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(TrackBar.EVENT_VALUECHANGED, value);
			}
		}

		// Token: 0x06004828 RID: 18472 RVA: 0x0012FA60 File Offset: 0x0012DC60
		private void AdjustSize()
		{
			if (base.IsHandleCreated)
			{
				int num = this.requestedDim;
				try
				{
					if (this.orientation == Orientation.Horizontal)
					{
						base.Height = (this.autoSize ? this.PreferredDimension : num);
					}
					else
					{
						base.Width = (this.autoSize ? this.PreferredDimension : num);
					}
				}
				finally
				{
					this.requestedDim = num;
				}
			}
		}

		/// <summary>Begins the initialization of a <see cref="T:System.Windows.Forms.TrackBar" /> that is used on a form or used by another component. The initialization occurs at run time.</summary>
		// Token: 0x06004829 RID: 18473 RVA: 0x0012FAD0 File Offset: 0x0012DCD0
		public void BeginInit()
		{
			this.initializing = true;
		}

		// Token: 0x0600482A RID: 18474 RVA: 0x0012FAD9 File Offset: 0x0012DCD9
		private void ConstrainValue()
		{
			if (this.initializing)
			{
				return;
			}
			if (this.Value < this.minimum)
			{
				this.Value = this.minimum;
			}
			if (this.Value > this.maximum)
			{
				this.Value = this.maximum;
			}
		}

		/// <summary>Overrides the <see cref="M:System.Windows.Forms.Control.CreateHandle" /> method.</summary>
		// Token: 0x0600482B RID: 18475 RVA: 0x0012FB18 File Offset: 0x0012DD18
		protected override void CreateHandle()
		{
			if (!base.RecreatingHandle)
			{
				IntPtr intPtr = UnsafeNativeMethods.ThemingScope.Activate();
				try
				{
					SafeNativeMethods.InitCommonControlsEx(new NativeMethods.INITCOMMONCONTROLSEX
					{
						dwICC = 4
					});
				}
				finally
				{
					UnsafeNativeMethods.ThemingScope.Deactivate(intPtr);
				}
			}
			base.CreateHandle();
		}

		/// <summary>Ends the initialization of a <see cref="T:System.Windows.Forms.TrackBar" /> that is used on a form or used by another component. The initialization occurs at run time.</summary>
		// Token: 0x0600482C RID: 18476 RVA: 0x0012FB68 File Offset: 0x0012DD68
		public void EndInit()
		{
			this.initializing = false;
			this.ConstrainValue();
		}

		// Token: 0x0600482D RID: 18477 RVA: 0x0012FB78 File Offset: 0x0012DD78
		private void GetTrackBarValue()
		{
			if (base.IsHandleCreated)
			{
				this.value = (int)(long)base.SendMessage(1024, 0, 0);
				if (this.orientation == Orientation.Vertical)
				{
					this.value = this.Minimum + this.Maximum - this.value;
				}
				if (this.orientation == Orientation.Horizontal && this.RightToLeft == RightToLeft.Yes && !base.IsMirrored)
				{
					this.value = this.Minimum + this.Maximum - this.value;
				}
			}
		}

		/// <summary>Determines whether the specified key is a regular input key or a special key that requires preprocessing.</summary>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values.</param>
		/// <returns>
		///   <see langword="true" /> if the specified key is a regular input key; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600482E RID: 18478 RVA: 0x0012FBFC File Offset: 0x0012DDFC
		protected override bool IsInputKey(Keys keyData)
		{
			if ((keyData & Keys.Alt) == Keys.Alt)
			{
				return false;
			}
			Keys keys = keyData & Keys.KeyCode;
			return keys - Keys.Prior <= 3 || base.IsInputKey(keyData);
		}

		/// <summary>Use the <see cref="M:System.Windows.Forms.Control.OnHandleCreated(System.EventArgs)" /> method.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600482F RID: 18479 RVA: 0x0012FC34 File Offset: 0x0012DE34
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			base.SendMessage(1031, 0, this.minimum);
			base.SendMessage(1032, 0, this.maximum);
			base.SendMessage(1044, this.tickFrequency, 0);
			base.SendMessage(1045, 0, this.largeChange);
			base.SendMessage(1047, 0, this.smallChange);
			this.SetTrackBarPosition();
			this.AdjustSize();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TrackBar.RightToLeftLayoutChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06004830 RID: 18480 RVA: 0x0012FCB4 File Offset: 0x0012DEB4
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
			EventHandler eventHandler = base.Events[TrackBar.EVENT_RIGHTTOLEFTLAYOUTCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TrackBar.Scroll" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06004831 RID: 18481 RVA: 0x0012FCFC File Offset: 0x0012DEFC
		protected virtual void OnScroll(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[TrackBar.EVENT_SCROLL];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseWheel" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06004832 RID: 18482 RVA: 0x0012FD2C File Offset: 0x0012DF2C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel(e);
			HandledMouseEventArgs handledMouseEventArgs = e as HandledMouseEventArgs;
			if (handledMouseEventArgs != null)
			{
				if (handledMouseEventArgs.Handled)
				{
					return;
				}
				handledMouseEventArgs.Handled = true;
			}
			if ((Control.ModifierKeys & (Keys.Shift | Keys.Alt)) != Keys.None || Control.MouseButtons != MouseButtons.None)
			{
				return;
			}
			int mouseWheelScrollLines = SystemInformation.MouseWheelScrollLines;
			if (mouseWheelScrollLines == 0)
			{
				return;
			}
			this.cumulativeWheelData += e.Delta;
			float num = (float)this.cumulativeWheelData / 120f;
			if (mouseWheelScrollLines == -1)
			{
				mouseWheelScrollLines = this.TickFrequency;
			}
			int num2 = (int)((float)mouseWheelScrollLines * num);
			if (num2 != 0)
			{
				if (num2 > 0)
				{
					int num3 = num2;
					this.Value = Math.Min(num3 + this.Value, this.Maximum);
					this.cumulativeWheelData -= (int)((float)num2 * (120f / (float)mouseWheelScrollLines));
				}
				else
				{
					int num3 = -num2;
					this.Value = Math.Max(this.Value - num3, this.Minimum);
					this.cumulativeWheelData -= (int)((float)num2 * (120f / (float)mouseWheelScrollLines));
				}
			}
			if (e.Delta != this.Value)
			{
				this.OnScroll(EventArgs.Empty);
				this.OnValueChanged(EventArgs.Empty);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TrackBar.ValueChanged" /> event.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06004833 RID: 18483 RVA: 0x0012FE48 File Offset: 0x0012E048
		protected virtual void OnValueChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[TrackBar.EVENT_VALUECHANGED];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06004834 RID: 18484 RVA: 0x0012FE76 File Offset: 0x0012E076
		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);
			this.RedrawControl();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.SystemColorsChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06004835 RID: 18485 RVA: 0x0012FE85 File Offset: 0x0012E085
		protected override void OnSystemColorsChanged(EventArgs e)
		{
			base.OnSystemColorsChanged(e);
			this.RedrawControl();
		}

		/// <summary>Performs the work of setting the specified bounds of this control.</summary>
		/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control.</param>
		/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control.</param>
		/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control.</param>
		/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control.</param>
		/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values.</param>
		// Token: 0x06004836 RID: 18486 RVA: 0x0012FE94 File Offset: 0x0012E094
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			this.requestedDim = ((this.orientation == Orientation.Horizontal) ? height : width);
			if (this.autoSize)
			{
				if (this.orientation == Orientation.Horizontal)
				{
					if ((specified & BoundsSpecified.Height) != BoundsSpecified.None)
					{
						height = this.PreferredDimension;
					}
				}
				else if ((specified & BoundsSpecified.Width) != BoundsSpecified.None)
				{
					width = this.PreferredDimension;
				}
			}
			base.SetBoundsCore(x, y, width, height, specified);
		}

		/// <summary>Sets the minimum and maximum values for a <see cref="T:System.Windows.Forms.TrackBar" />.</summary>
		/// <param name="minValue">The lower limit of the range of the track bar.</param>
		/// <param name="maxValue">The upper limit of the range of the track bar.</param>
		// Token: 0x06004837 RID: 18487 RVA: 0x0012FEF0 File Offset: 0x0012E0F0
		public void SetRange(int minValue, int maxValue)
		{
			if (this.minimum != minValue || this.maximum != maxValue)
			{
				if (minValue > maxValue)
				{
					maxValue = minValue;
				}
				this.minimum = minValue;
				this.maximum = maxValue;
				if (base.IsHandleCreated)
				{
					base.SendMessage(1031, 0, this.minimum);
					base.SendMessage(1032, 1, this.maximum);
					base.Invalidate();
				}
				if (this.value < this.minimum)
				{
					this.value = this.minimum;
				}
				if (this.value > this.maximum)
				{
					this.value = this.maximum;
				}
				this.SetTrackBarPosition();
			}
		}

		// Token: 0x06004838 RID: 18488 RVA: 0x0012FF98 File Offset: 0x0012E198
		private void SetTrackBarPosition()
		{
			if (base.IsHandleCreated)
			{
				int num = this.value;
				if (this.orientation == Orientation.Vertical)
				{
					num = this.Minimum + this.Maximum - this.value;
				}
				if (this.orientation == Orientation.Horizontal && this.RightToLeft == RightToLeft.Yes && !base.IsMirrored)
				{
					num = this.Minimum + this.Maximum - this.value;
				}
				base.SendMessage(1029, 1, num);
			}
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.TrackBar" /> control.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.TrackBar" />.</returns>
		// Token: 0x06004839 RID: 18489 RVA: 0x00130010 File Offset: 0x0012E210
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

		/// <summary>Overrides the <see cref="M:System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@)" /> method.</summary>
		/// <param name="m">A Windows Message object.</param>
		// Token: 0x0600483A RID: 18490 RVA: 0x00130090 File Offset: 0x0012E290
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg - 8468 <= 1)
			{
				int num = NativeMethods.Util.LOWORD(m.WParam);
				if ((num <= 3 || num - 5 <= 3) && this.value != this.Value)
				{
					this.OnScroll(EventArgs.Empty);
					this.OnValueChanged(EventArgs.Empty);
					return;
				}
			}
			else
			{
				base.WndProc(ref m);
			}
		}

		// Token: 0x0400270A RID: 9994
		private static readonly object EVENT_SCROLL = new object();

		// Token: 0x0400270B RID: 9995
		private static readonly object EVENT_VALUECHANGED = new object();

		// Token: 0x0400270C RID: 9996
		private static readonly object EVENT_RIGHTTOLEFTLAYOUTCHANGED = new object();

		// Token: 0x0400270D RID: 9997
		private bool autoSize = true;

		// Token: 0x0400270E RID: 9998
		private int largeChange = 5;

		// Token: 0x0400270F RID: 9999
		private int maximum = 10;

		// Token: 0x04002710 RID: 10000
		private int minimum;

		// Token: 0x04002711 RID: 10001
		private Orientation orientation;

		// Token: 0x04002712 RID: 10002
		private int value;

		// Token: 0x04002713 RID: 10003
		private int smallChange = 1;

		// Token: 0x04002714 RID: 10004
		private int tickFrequency = 1;

		// Token: 0x04002715 RID: 10005
		private TickStyle tickStyle = TickStyle.BottomRight;

		// Token: 0x04002716 RID: 10006
		private int requestedDim;

		// Token: 0x04002717 RID: 10007
		private int cumulativeWheelData;

		// Token: 0x04002718 RID: 10008
		private bool initializing;

		// Token: 0x04002719 RID: 10009
		private bool rightToLeftLayout;
	}
}
