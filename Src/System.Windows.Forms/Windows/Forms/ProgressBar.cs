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
	/// <summary>Represents a Windows progress bar control.</summary>
	// Token: 0x02000327 RID: 807
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultProperty("Value")]
	[DefaultBindingProperty("Value")]
	[SRDescription("DescriptionProgressBar")]
	public class ProgressBar : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ProgressBar" /> class.</summary>
		// Token: 0x0600339F RID: 13215 RVA: 0x000EB17C File Offset: 0x000E937C
		public ProgressBar()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.Selectable | ControlStyles.UseTextForAccessibility, false);
			this.ForeColor = this.defaultForeColor;
		}

		/// <summary>Overrides <see cref="P:System.Windows.Forms.Control.CreateParams" />.</summary>
		/// <returns>Information needed when you create a control.</returns>
		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x060033A0 RID: 13216 RVA: 0x000EB1CC File Offset: 0x000E93CC
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ClassName = "msctls_progress32";
				if (this.Style == ProgressBarStyle.Continuous)
				{
					createParams.Style |= 1;
				}
				else if (this.Style == ProgressBarStyle.Marquee && !base.DesignMode)
				{
					createParams.Style |= 8;
				}
				if (this.RightToLeft == RightToLeft.Yes && this.RightToLeftLayout)
				{
					createParams.ExStyle |= 4194304;
					createParams.ExStyle &= -28673;
				}
				return createParams;
			}
		}

		/// <summary>Gets or sets a value indicating whether the control can accept data that the user drags onto it.</summary>
		/// <returns>
		///   <see langword="true" /> if drag-and-drop operations are allowed in the control; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x060033A1 RID: 13217 RVA: 0x000B8E3D File Offset: 0x000B703D
		// (set) Token: 0x060033A2 RID: 13218 RVA: 0x000B8E45 File Offset: 0x000B7045
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool AllowDrop
		{
			get
			{
				return base.AllowDrop;
			}
			set
			{
				base.AllowDrop = value;
			}
		}

		/// <summary>Gets or sets the background image for the <see cref="T:System.Windows.Forms.ProgressBar" /> control.</summary>
		/// <returns>The current background image.</returns>
		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x060033A3 RID: 13219 RVA: 0x0001187C File Offset: 0x0000FA7C
		// (set) Token: 0x060033A4 RID: 13220 RVA: 0x00011884 File Offset: 0x0000FA84
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

		/// <summary>Gets or sets the manner in which progress should be indicated on the progress bar.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ProgressBarStyle" /> values. The default is <see cref="F:System.Windows.Forms.ProgressBarStyle.Blocks" /></returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value is not a member of the <see cref="T:System.Windows.Forms.ProgressBarStyle" /> enumeration.</exception>
		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x060033A5 RID: 13221 RVA: 0x000EB259 File Offset: 0x000E9459
		// (set) Token: 0x060033A6 RID: 13222 RVA: 0x000EB264 File Offset: 0x000E9464
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DefaultValue(ProgressBarStyle.Blocks)]
		[SRCategory("CatBehavior")]
		[SRDescription("ProgressBarStyleDescr")]
		public ProgressBarStyle Style
		{
			get
			{
				return this.style;
			}
			set
			{
				if (this.style != value)
				{
					if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
					{
						throw new InvalidEnumArgumentException("value", (int)value, typeof(ProgressBarStyle));
					}
					this.style = value;
					if (base.IsHandleCreated)
					{
						base.RecreateHandle();
					}
					if (this.style == ProgressBarStyle.Marquee)
					{
						this.StartMarquee();
					}
				}
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ProgressBar.BackgroundImage" /> property changes.</summary>
		// Token: 0x14000261 RID: 609
		// (add) Token: 0x060033A7 RID: 13223 RVA: 0x0001188D File Offset: 0x0000FA8D
		// (remove) Token: 0x060033A8 RID: 13224 RVA: 0x00011896 File Offset: 0x0000FA96
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

		/// <summary>Gets or sets the layout of the background image of the progress bar.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImageLayout" /> values.</returns>
		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x060033A9 RID: 13225 RVA: 0x0001189F File Offset: 0x0000FA9F
		// (set) Token: 0x060033AA RID: 13226 RVA: 0x000118A7 File Offset: 0x0000FAA7
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ProgressBar.BackgroundImageLayout" /> property changes.</summary>
		// Token: 0x14000262 RID: 610
		// (add) Token: 0x060033AB RID: 13227 RVA: 0x000118B0 File Offset: 0x0000FAB0
		// (remove) Token: 0x060033AC RID: 13228 RVA: 0x000118B9 File Offset: 0x0000FAB9
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

		/// <summary>Gets or sets a value indicating whether the control, when it receives focus, causes validation to be performed on any controls that require validation.</summary>
		/// <returns>
		///   <see langword="true" /> if the control, when it receives focus, causes validation to be performed on any controls that require validation; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x060033AD RID: 13229 RVA: 0x000E28D7 File Offset: 0x000E0AD7
		// (set) Token: 0x060033AE RID: 13230 RVA: 0x000E28DF File Offset: 0x000E0ADF
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool CausesValidation
		{
			get
			{
				return base.CausesValidation;
			}
			set
			{
				base.CausesValidation = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ProgressBar.CausesValidation" /> property changes.</summary>
		// Token: 0x14000263 RID: 611
		// (add) Token: 0x060033AF RID: 13231 RVA: 0x000E28E8 File Offset: 0x000E0AE8
		// (remove) Token: 0x060033B0 RID: 13232 RVA: 0x000E28F1 File Offset: 0x000E0AF1
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler CausesValidationChanged
		{
			add
			{
				base.CausesValidationChanged += value;
			}
			remove
			{
				base.CausesValidationChanged -= value;
			}
		}

		/// <summary>Gets the default Input Method Editor (IME) mode supported by the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x060033B1 RID: 13233 RVA: 0x00023BD7 File Offset: 0x00021DD7
		protected override ImeMode DefaultImeMode
		{
			get
			{
				return ImeMode.Disable;
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the default size of the control.</returns>
		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x060033B2 RID: 13234 RVA: 0x000EB2C4 File Offset: 0x000E94C4
		protected override Size DefaultSize
		{
			get
			{
				return new Size(100, 23);
			}
		}

		/// <summary>Gets or sets a value indicating whether the control should redraw its surface using a secondary buffer.</summary>
		/// <returns>
		///   <see langword="true" /> if a secondary buffer should be used, <see langword="false" /> otherwise.</returns>
		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x060033B3 RID: 13235 RVA: 0x00012FC3 File Offset: 0x000111C3
		// (set) Token: 0x060033B4 RID: 13236 RVA: 0x00012FCB File Offset: 0x000111CB
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

		/// <summary>Gets or sets the font of text in the <see cref="T:System.Windows.Forms.ProgressBar" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> of the text. The default is the font set by the container.</returns>
		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x060033B5 RID: 13237 RVA: 0x0001A0D6 File Offset: 0x000182D6
		// (set) Token: 0x060033B6 RID: 13238 RVA: 0x0001A0DE File Offset: 0x000182DE
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ProgressBar.Font" /> property changes.</summary>
		// Token: 0x14000264 RID: 612
		// (add) Token: 0x060033B7 RID: 13239 RVA: 0x0005A900 File Offset: 0x00058B00
		// (remove) Token: 0x060033B8 RID: 13240 RVA: 0x0005A909 File Offset: 0x00058B09
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

		/// <summary>Gets or sets the input method editor (IME) for the <see cref="T:System.Windows.Forms.ProgressBar" /></summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x060033B9 RID: 13241 RVA: 0x0001A051 File Offset: 0x00018251
		// (set) Token: 0x060033BA RID: 13242 RVA: 0x0001A059 File Offset: 0x00018259
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ProgressBar.ImeMode" /> property changes.</summary>
		// Token: 0x14000265 RID: 613
		// (add) Token: 0x060033BB RID: 13243 RVA: 0x00023F70 File Offset: 0x00022170
		// (remove) Token: 0x060033BC RID: 13244 RVA: 0x00023F79 File Offset: 0x00022179
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

		/// <summary>Gets or sets the time period, in milliseconds, that it takes the progress block to scroll across the progress bar.</summary>
		/// <returns>The time period, in milliseconds, that it takes the progress block to scroll across the progress bar.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The indicated time period is less than 0.</exception>
		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x060033BD RID: 13245 RVA: 0x000EB2CF File Offset: 0x000E94CF
		// (set) Token: 0x060033BE RID: 13246 RVA: 0x000EB2D7 File Offset: 0x000E94D7
		[DefaultValue(100)]
		[SRCategory("CatBehavior")]
		[SRDescription("ProgressBarMarqueeAnimationSpeed")]
		public int MarqueeAnimationSpeed
		{
			get
			{
				return this.marqueeSpeed;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("MarqueeAnimationSpeed must be non-negative");
				}
				this.marqueeSpeed = value;
				if (!base.DesignMode)
				{
					this.StartMarquee();
				}
			}
		}

		// Token: 0x060033BF RID: 13247 RVA: 0x000EB300 File Offset: 0x000E9500
		private void StartMarquee()
		{
			if (base.IsHandleCreated && this.style == ProgressBarStyle.Marquee)
			{
				if (this.marqueeSpeed == 0)
				{
					base.SendMessage(1034, 0, this.marqueeSpeed);
					return;
				}
				base.SendMessage(1034, 1, this.marqueeSpeed);
			}
		}

		/// <summary>Gets or sets the maximum value of the range of the control.</summary>
		/// <returns>The maximum value of the range. The default is 100.</returns>
		/// <exception cref="T:System.ArgumentException">The value specified is less than 0.</exception>
		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x060033C0 RID: 13248 RVA: 0x000EB34D File Offset: 0x000E954D
		// (set) Token: 0x060033C1 RID: 13249 RVA: 0x000EB358 File Offset: 0x000E9558
		[DefaultValue(100)]
		[SRCategory("CatBehavior")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRDescription("ProgressBarMaximumDescr")]
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
					if (value < 0)
					{
						throw new ArgumentOutOfRangeException("Maximum", SR.GetString("InvalidLowBoundArgumentEx", new object[]
						{
							"Maximum",
							value.ToString(CultureInfo.CurrentCulture),
							0.ToString(CultureInfo.CurrentCulture)
						}));
					}
					if (this.minimum > value)
					{
						this.minimum = value;
					}
					this.maximum = value;
					if (this.value > this.maximum)
					{
						this.value = this.maximum;
					}
					if (base.IsHandleCreated)
					{
						base.SendMessage(1030, this.minimum, this.maximum);
						this.UpdatePos();
					}
				}
			}
		}

		/// <summary>Gets or sets the minimum value of the range of the control.</summary>
		/// <returns>The minimum value of the range. The default is 0.</returns>
		/// <exception cref="T:System.ArgumentException">The value specified for the property is less than 0.</exception>
		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x060033C2 RID: 13250 RVA: 0x000EB40F File Offset: 0x000E960F
		// (set) Token: 0x060033C3 RID: 13251 RVA: 0x000EB418 File Offset: 0x000E9618
		[DefaultValue(0)]
		[SRCategory("CatBehavior")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRDescription("ProgressBarMinimumDescr")]
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
					if (value < 0)
					{
						throw new ArgumentOutOfRangeException("Minimum", SR.GetString("InvalidLowBoundArgumentEx", new object[]
						{
							"Minimum",
							value.ToString(CultureInfo.CurrentCulture),
							0.ToString(CultureInfo.CurrentCulture)
						}));
					}
					if (this.maximum < value)
					{
						this.maximum = value;
					}
					this.minimum = value;
					if (this.value < this.minimum)
					{
						this.value = this.minimum;
					}
					if (base.IsHandleCreated)
					{
						base.SendMessage(1030, this.minimum, this.maximum);
						this.UpdatePos();
					}
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackColorChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060033C4 RID: 13252 RVA: 0x000EB4CF File Offset: 0x000E96CF
		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);
			if (base.IsHandleCreated)
			{
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 8193, 0, ColorTranslator.ToWin32(this.BackColor));
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ForeColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060033C5 RID: 13253 RVA: 0x000EB503 File Offset: 0x000E9703
		protected override void OnForeColorChanged(EventArgs e)
		{
			base.OnForeColorChanged(e);
			if (base.IsHandleCreated)
			{
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1033, 0, ColorTranslator.ToWin32(this.ForeColor));
			}
		}

		/// <summary>Gets or sets the space between the edges of a <see cref="T:System.Windows.Forms.ProgressBar" /> control and its contents.</summary>
		/// <returns>
		///   <see cref="F:System.Windows.Forms.Padding.Empty" /> in all cases.</returns>
		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x060033C6 RID: 13254 RVA: 0x00013442 File Offset: 0x00011642
		// (set) Token: 0x060033C7 RID: 13255 RVA: 0x0001344A File Offset: 0x0001164A
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ProgressBar.Padding" /> property changes.</summary>
		// Token: 0x14000266 RID: 614
		// (add) Token: 0x060033C8 RID: 13256 RVA: 0x00013453 File Offset: 0x00011653
		// (remove) Token: 0x060033C9 RID: 13257 RVA: 0x0001345C File Offset: 0x0001165C
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

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ProgressBar" /> and any text it contains is displayed from right to left.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.ProgressBar" /> is displayed from right to left; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x060033CA RID: 13258 RVA: 0x000EB537 File Offset: 0x000E9737
		// (set) Token: 0x060033CB RID: 13259 RVA: 0x000EB540 File Offset: 0x000E9740
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ProgressBar.RightToLeftLayout" /> property changes.</summary>
		// Token: 0x14000267 RID: 615
		// (add) Token: 0x060033CC RID: 13260 RVA: 0x000EB594 File Offset: 0x000E9794
		// (remove) Token: 0x060033CD RID: 13261 RVA: 0x000EB5AD File Offset: 0x000E97AD
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

		/// <summary>Gets or sets the amount by which a call to the <see cref="M:System.Windows.Forms.ProgressBar.PerformStep" /> method increases the current position of the progress bar.</summary>
		/// <returns>The amount by which to increment the progress bar with each call to the <see cref="M:System.Windows.Forms.ProgressBar.PerformStep" /> method. The default is 10.</returns>
		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x060033CE RID: 13262 RVA: 0x000EB5C6 File Offset: 0x000E97C6
		// (set) Token: 0x060033CF RID: 13263 RVA: 0x000EB5CE File Offset: 0x000E97CE
		[DefaultValue(10)]
		[SRCategory("CatBehavior")]
		[SRDescription("ProgressBarStepDescr")]
		public int Step
		{
			get
			{
				return this.step;
			}
			set
			{
				this.step = value;
				if (base.IsHandleCreated)
				{
					base.SendMessage(1028, this.step, 0);
				}
			}
		}

		/// <summary>Overrides <see cref="P:System.Windows.Forms.Control.TabStop" />.</summary>
		/// <returns>true if the user can set the focus to the control by using the TAB key; otherwise, false. The default is true.</returns>
		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x060033D0 RID: 13264 RVA: 0x000B2395 File Offset: 0x000B0595
		// (set) Token: 0x060033D1 RID: 13265 RVA: 0x000B239D File Offset: 0x000B059D
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ProgressBar.TabStop" /> property changes.</summary>
		// Token: 0x14000268 RID: 616
		// (add) Token: 0x060033D2 RID: 13266 RVA: 0x000B23A6 File Offset: 0x000B05A6
		// (remove) Token: 0x060033D3 RID: 13267 RVA: 0x000B23AF File Offset: 0x000B05AF
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TabStopChanged
		{
			add
			{
				base.TabStopChanged += value;
			}
			remove
			{
				base.TabStopChanged -= value;
			}
		}

		/// <summary>Gets or sets the text associated with this control.</summary>
		/// <returns>The text associated with this control.</returns>
		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x060033D4 RID: 13268 RVA: 0x00013814 File Offset: 0x00011A14
		// (set) Token: 0x060033D5 RID: 13269 RVA: 0x00023FE9 File Offset: 0x000221E9
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ProgressBar.Text" /> property changes.</summary>
		// Token: 0x14000269 RID: 617
		// (add) Token: 0x060033D6 RID: 13270 RVA: 0x00046591 File Offset: 0x00044791
		// (remove) Token: 0x060033D7 RID: 13271 RVA: 0x0004659A File Offset: 0x0004479A
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

		/// <summary>Gets or sets the current position of the progress bar.</summary>
		/// <returns>The position within the range of the progress bar. The default is 0.</returns>
		/// <exception cref="T:System.ArgumentException">The value specified is greater than the value of the <see cref="P:System.Windows.Forms.ProgressBar.Maximum" /> property.  
		///  -or-  
		///  The value specified is less than the value of the <see cref="P:System.Windows.Forms.ProgressBar.Minimum" /> property.</exception>
		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x060033D8 RID: 13272 RVA: 0x000EB5F2 File Offset: 0x000E97F2
		// (set) Token: 0x060033D9 RID: 13273 RVA: 0x000EB5FC File Offset: 0x000E97FC
		[DefaultValue(0)]
		[SRCategory("CatBehavior")]
		[Bindable(true)]
		[SRDescription("ProgressBarValueDescr")]
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
					this.UpdatePos();
				}
			}
		}

		/// <summary>Occurs when the user double-clicks the control.</summary>
		// Token: 0x1400026A RID: 618
		// (add) Token: 0x060033DA RID: 13274 RVA: 0x00023757 File Offset: 0x00021957
		// (remove) Token: 0x060033DB RID: 13275 RVA: 0x00023760 File Offset: 0x00021960
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

		/// <summary>Occurs when the user double-clicks the control.</summary>
		// Token: 0x1400026B RID: 619
		// (add) Token: 0x060033DC RID: 13276 RVA: 0x00023769 File Offset: 0x00021969
		// (remove) Token: 0x060033DD RID: 13277 RVA: 0x00023772 File Offset: 0x00021972
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

		/// <summary>Occurs when the user releases a key while the control has focus.</summary>
		// Token: 0x1400026C RID: 620
		// (add) Token: 0x060033DE RID: 13278 RVA: 0x000B9104 File Offset: 0x000B7304
		// (remove) Token: 0x060033DF RID: 13279 RVA: 0x000B910D File Offset: 0x000B730D
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event KeyEventHandler KeyUp
		{
			add
			{
				base.KeyUp += value;
			}
			remove
			{
				base.KeyUp -= value;
			}
		}

		/// <summary>Occurs when the user presses a key while the control has focus.</summary>
		// Token: 0x1400026D RID: 621
		// (add) Token: 0x060033E0 RID: 13280 RVA: 0x000B9116 File Offset: 0x000B7316
		// (remove) Token: 0x060033E1 RID: 13281 RVA: 0x000B911F File Offset: 0x000B731F
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event KeyEventHandler KeyDown
		{
			add
			{
				base.KeyDown += value;
			}
			remove
			{
				base.KeyDown -= value;
			}
		}

		/// <summary>Occurs when the user presses a key while the control has focus.</summary>
		// Token: 0x1400026E RID: 622
		// (add) Token: 0x060033E2 RID: 13282 RVA: 0x000B9128 File Offset: 0x000B7328
		// (remove) Token: 0x060033E3 RID: 13283 RVA: 0x000B9131 File Offset: 0x000B7331
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event KeyPressEventHandler KeyPress
		{
			add
			{
				base.KeyPress += value;
			}
			remove
			{
				base.KeyPress -= value;
			}
		}

		/// <summary>Occurs when focus enters the <see cref="T:System.Windows.Forms.ProgressBar" /> control.</summary>
		// Token: 0x1400026F RID: 623
		// (add) Token: 0x060033E4 RID: 13284 RVA: 0x000E3338 File Offset: 0x000E1538
		// (remove) Token: 0x060033E5 RID: 13285 RVA: 0x000E3341 File Offset: 0x000E1541
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler Enter
		{
			add
			{
				base.Enter += value;
			}
			remove
			{
				base.Enter -= value;
			}
		}

		/// <summary>Occurs when focus leaves the <see cref="T:System.Windows.Forms.ProgressBar" /> control.</summary>
		// Token: 0x14000270 RID: 624
		// (add) Token: 0x060033E6 RID: 13286 RVA: 0x000E334A File Offset: 0x000E154A
		// (remove) Token: 0x060033E7 RID: 13287 RVA: 0x000E3353 File Offset: 0x000E1553
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler Leave
		{
			add
			{
				base.Leave += value;
			}
			remove
			{
				base.Leave -= value;
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ProgressBar" /> is drawn.</summary>
		// Token: 0x14000271 RID: 625
		// (add) Token: 0x060033E8 RID: 13288 RVA: 0x00013D73 File Offset: 0x00011F73
		// (remove) Token: 0x060033E9 RID: 13289 RVA: 0x00013D7C File Offset: 0x00011F7C
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

		/// <summary>Creates a handle for the control.</summary>
		// Token: 0x060033EA RID: 13290 RVA: 0x000EB674 File Offset: 0x000E9874
		protected override void CreateHandle()
		{
			if (!base.RecreatingHandle)
			{
				IntPtr intPtr = UnsafeNativeMethods.ThemingScope.Activate();
				try
				{
					SafeNativeMethods.InitCommonControlsEx(new NativeMethods.INITCOMMONCONTROLSEX
					{
						dwICC = 32
					});
				}
				finally
				{
					UnsafeNativeMethods.ThemingScope.Deactivate(intPtr);
				}
			}
			base.CreateHandle();
		}

		/// <summary>Advances the current position of the progress bar by the specified amount.</summary>
		/// <param name="value">The amount by which to increment the progress bar's current position.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.ProgressBar.Style" /> property is set to <see cref="F:System.Windows.Forms.ProgressBarStyle.Marquee" /></exception>
		// Token: 0x060033EB RID: 13291 RVA: 0x000EB6C4 File Offset: 0x000E98C4
		public void Increment(int value)
		{
			if (this.Style == ProgressBarStyle.Marquee)
			{
				throw new InvalidOperationException(SR.GetString("ProgressBarIncrementMarqueeException"));
			}
			this.value += value;
			if (this.value < this.minimum)
			{
				this.value = this.minimum;
			}
			if (this.value > this.maximum)
			{
				this.value = this.maximum;
			}
			this.UpdatePos();
		}

		/// <summary>Overrides <see cref="M:System.Windows.Forms.Control.OnHandleCreated(System.EventArgs)" /></summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060033EC RID: 13292 RVA: 0x000EB734 File Offset: 0x000E9934
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			base.SendMessage(1030, this.minimum, this.maximum);
			base.SendMessage(1028, this.step, 0);
			base.SendMessage(1026, this.value, 0);
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 8193, 0, ColorTranslator.ToWin32(this.BackColor));
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1033, 0, ColorTranslator.ToWin32(this.ForeColor));
			this.StartMarquee();
			SystemEvents.UserPreferenceChanged += this.UserPreferenceChangedHandler;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060033ED RID: 13293 RVA: 0x000EB7E3 File Offset: 0x000E99E3
		protected override void OnHandleDestroyed(EventArgs e)
		{
			SystemEvents.UserPreferenceChanged -= this.UserPreferenceChangedHandler;
			base.OnHandleDestroyed(e);
		}

		/// <summary>Raises the <see cref="P:System.Windows.Forms.ProgressBar.RightToLeftLayout" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060033EE RID: 13294 RVA: 0x000EB7FD File Offset: 0x000E99FD
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

		/// <summary>Advances the current position of the progress bar by the amount of the <see cref="P:System.Windows.Forms.ProgressBar.Step" /> property.</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.ProgressBar.Style" /> is set to <see cref="F:System.Windows.Forms.ProgressBarStyle.Marquee" />.</exception>
		// Token: 0x060033EF RID: 13295 RVA: 0x000EB82C File Offset: 0x000E9A2C
		public void PerformStep()
		{
			if (this.Style == ProgressBarStyle.Marquee)
			{
				throw new InvalidOperationException(SR.GetString("ProgressBarPerformStepMarqueeException"));
			}
			this.Increment(this.step);
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.Control.ForeColor" /> to its default value.</summary>
		// Token: 0x060033F0 RID: 13296 RVA: 0x000EB853 File Offset: 0x000E9A53
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void ResetForeColor()
		{
			this.ForeColor = this.defaultForeColor;
		}

		// Token: 0x060033F1 RID: 13297 RVA: 0x000EB861 File Offset: 0x000E9A61
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal override bool ShouldSerializeForeColor()
		{
			return this.ForeColor != this.defaultForeColor;
		}

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x060033F2 RID: 13298 RVA: 0x00028BBB File Offset: 0x00026DBB
		internal override bool SupportsUiaProviders
		{
			get
			{
				return AccessibilityImprovements.Level3 && !base.DesignMode;
			}
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.ProgressBar" /> control.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.ProgressBar" />.</returns>
		// Token: 0x060033F3 RID: 13299 RVA: 0x000EB874 File Offset: 0x000E9A74
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

		// Token: 0x060033F4 RID: 13300 RVA: 0x000EB8F1 File Offset: 0x000E9AF1
		private void UpdatePos()
		{
			if (base.IsHandleCreated)
			{
				base.SendMessage(1026, this.value, 0);
			}
		}

		// Token: 0x060033F5 RID: 13301 RVA: 0x000EB910 File Offset: 0x000E9B10
		private void UserPreferenceChangedHandler(object o, UserPreferenceChangedEventArgs e)
		{
			if (base.IsHandleCreated)
			{
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1033, 0, ColorTranslator.ToWin32(this.ForeColor));
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 8193, 0, ColorTranslator.ToWin32(this.BackColor));
			}
		}

		/// <summary>Creates a new instance of the accessibility object for the <see cref="T:System.Windows.Forms.ProgressBar" /> control.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Control.ControlAccessibleObject" /> for this <see cref="T:System.Windows.Forms.ProgressBar" /> control.</returns>
		// Token: 0x060033F6 RID: 13302 RVA: 0x000EB96B File Offset: 0x000E9B6B
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level3)
			{
				return new ProgressBar.ProgressBarAccessibleObject(this);
			}
			return base.CreateAccessibilityInstance();
		}

		// Token: 0x04001EC3 RID: 7875
		private int minimum;

		// Token: 0x04001EC4 RID: 7876
		private int maximum = 100;

		// Token: 0x04001EC5 RID: 7877
		private int step = 10;

		// Token: 0x04001EC6 RID: 7878
		private int value;

		// Token: 0x04001EC7 RID: 7879
		private int marqueeSpeed = 100;

		// Token: 0x04001EC8 RID: 7880
		private Color defaultForeColor = SystemColors.Highlight;

		// Token: 0x04001EC9 RID: 7881
		private ProgressBarStyle style;

		// Token: 0x04001ECA RID: 7882
		private EventHandler onRightToLeftLayoutChanged;

		// Token: 0x04001ECB RID: 7883
		private bool rightToLeftLayout;

		// Token: 0x020007CB RID: 1995
		[ComVisible(true)]
		internal class ProgressBarAccessibleObject : Control.ControlAccessibleObject
		{
			// Token: 0x06006D55 RID: 27989 RVA: 0x0009B733 File Offset: 0x00099933
			internal ProgressBarAccessibleObject(ProgressBar owner)
				: base(owner)
			{
			}

			// Token: 0x170017E6 RID: 6118
			// (get) Token: 0x06006D56 RID: 27990 RVA: 0x001913DC File Offset: 0x0018F5DC
			private ProgressBar OwningProgressBar
			{
				get
				{
					return base.Owner as ProgressBar;
				}
			}

			// Token: 0x06006D57 RID: 27991 RVA: 0x0009B73C File Offset: 0x0009993C
			internal override bool IsIAccessibleExSupported()
			{
				return AccessibilityImprovements.Level3 || base.IsIAccessibleExSupported();
			}

			// Token: 0x06006D58 RID: 27992 RVA: 0x001913E9 File Offset: 0x0018F5E9
			internal override bool IsPatternSupported(int patternId)
			{
				return patternId == 10002 || patternId == 10003 || patternId == 10018 || base.IsPatternSupported(patternId);
			}

			// Token: 0x06006D59 RID: 27993 RVA: 0x0019140C File Offset: 0x0018F60C
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID > 30009)
				{
					if (propertyID <= 30043)
					{
						if (propertyID != 30033 && propertyID != 30043)
						{
							goto IL_7F;
						}
					}
					else if (propertyID != 30048)
					{
						if (propertyID - 30051 > 1)
						{
							goto IL_7F;
						}
						return double.NaN;
					}
					return true;
				}
				if (propertyID == 30003)
				{
					return 50012;
				}
				if (propertyID == 30005)
				{
					return this.Name;
				}
				if (propertyID == 30009)
				{
					return true;
				}
				IL_7F:
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x06006D5A RID: 27994 RVA: 0x0019149F File Offset: 0x0018F69F
			internal override void SetValue(double newValue)
			{
				throw new InvalidOperationException("Progress Bar is read-only.");
			}

			// Token: 0x170017E7 RID: 6119
			// (get) Token: 0x06006D5B RID: 27995 RVA: 0x0001605B File Offset: 0x0001425B
			internal override double LargeChange
			{
				get
				{
					return double.NaN;
				}
			}

			// Token: 0x170017E8 RID: 6120
			// (get) Token: 0x06006D5C RID: 27996 RVA: 0x001914AC File Offset: 0x0018F6AC
			internal override double Maximum
			{
				get
				{
					ProgressBar owningProgressBar = this.OwningProgressBar;
					int? num = ((owningProgressBar != null) ? new int?(owningProgressBar.Maximum) : null);
					if (num == null)
					{
						return double.NaN;
					}
					return (double)num.GetValueOrDefault();
				}
			}

			// Token: 0x170017E9 RID: 6121
			// (get) Token: 0x06006D5D RID: 27997 RVA: 0x001914F4 File Offset: 0x0018F6F4
			internal override double Minimum
			{
				get
				{
					ProgressBar owningProgressBar = this.OwningProgressBar;
					int? num = ((owningProgressBar != null) ? new int?(owningProgressBar.Minimum) : null);
					if (num == null)
					{
						return double.NaN;
					}
					return (double)num.GetValueOrDefault();
				}
			}

			// Token: 0x170017EA RID: 6122
			// (get) Token: 0x06006D5E RID: 27998 RVA: 0x0001605B File Offset: 0x0001425B
			internal override double SmallChange
			{
				get
				{
					return double.NaN;
				}
			}

			// Token: 0x170017EB RID: 6123
			// (get) Token: 0x06006D5F RID: 27999 RVA: 0x0019153C File Offset: 0x0018F73C
			internal override double RangeValue
			{
				get
				{
					ProgressBar owningProgressBar = this.OwningProgressBar;
					int? num = ((owningProgressBar != null) ? new int?(owningProgressBar.Value) : null);
					if (num == null)
					{
						return double.NaN;
					}
					return (double)num.GetValueOrDefault();
				}
			}

			// Token: 0x170017EC RID: 6124
			// (get) Token: 0x06006D60 RID: 28000 RVA: 0x00012E4E File Offset: 0x0001104E
			internal override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}
		}
	}
}
