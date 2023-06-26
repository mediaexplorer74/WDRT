using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Used to group collections of controls.</summary>
	// Token: 0x02000319 RID: 793
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultProperty("BorderStyle")]
	[DefaultEvent("Paint")]
	[Docking(DockingBehavior.Ask)]
	[Designer("System.Windows.Forms.Design.PanelDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionPanel")]
	public class Panel : ScrollableControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Panel" /> class.</summary>
		// Token: 0x06003266 RID: 12902 RVA: 0x000E24F5 File Offset: 0x000E06F5
		public Panel()
		{
			base.SetState2(2048, true);
			this.TabStop = false;
			base.SetStyle(ControlStyles.Selectable | ControlStyles.AllPaintingInWmPaint, false);
			base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
		}

		/// <summary>Gets or sets a value that indicates whether the control resizes based on its contents.</summary>
		/// <returns>
		///   <see langword="true" /> if the control automatically resizes based on its contents; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x06003267 RID: 12903 RVA: 0x00011831 File Offset: 0x0000FA31
		// (set) Token: 0x06003268 RID: 12904 RVA: 0x00011839 File Offset: 0x0000FA39
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Panel.AutoSize" /> property has changed.</summary>
		// Token: 0x1400024C RID: 588
		// (add) Token: 0x06003269 RID: 12905 RVA: 0x00011842 File Offset: 0x0000FA42
		// (remove) Token: 0x0600326A RID: 12906 RVA: 0x0001184B File Offset: 0x0000FA4B
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

		/// <summary>Indicates the automatic sizing behavior of the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AutoSizeMode" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.AutoSizeMode" /> values.</exception>
		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x0600326B RID: 12907 RVA: 0x00023551 File Offset: 0x00021751
		// (set) Token: 0x0600326C RID: 12908 RVA: 0x000E2528 File Offset: 0x000E0728
		[SRDescription("ControlAutoSizeModeDescr")]
		[SRCategory("CatLayout")]
		[Browsable(true)]
		[DefaultValue(AutoSizeMode.GrowOnly)]
		[Localizable(true)]
		public virtual AutoSizeMode AutoSizeMode
		{
			get
			{
				return base.GetAutoSizeMode();
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 1))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(AutoSizeMode));
				}
				if (base.GetAutoSizeMode() != value)
				{
					base.SetAutoSizeMode(value);
					if (this.ParentInternal != null)
					{
						if (this.ParentInternal.LayoutEngine == DefaultLayout.Instance)
						{
							this.ParentInternal.LayoutEngine.InitLayout(this, BoundsSpecified.Size);
						}
						LayoutTransaction.DoLayout(this.ParentInternal, this, PropertyNames.AutoSize);
					}
				}
			}
		}

		/// <summary>Indicates the border style for the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. The default is <see langword="BorderStyle.None" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.BorderStyle" /> value.</exception>
		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x0600326D RID: 12909 RVA: 0x000E25A9 File Offset: 0x000E07A9
		// (set) Token: 0x0600326E RID: 12910 RVA: 0x000E25B1 File Offset: 0x000E07B1
		[SRCategory("CatAppearance")]
		[DefaultValue(BorderStyle.None)]
		[DispId(-504)]
		[SRDescription("PanelBorderStyleDescr")]
		public BorderStyle BorderStyle
		{
			get
			{
				return this.borderStyle;
			}
			set
			{
				if (this.borderStyle != value)
				{
					if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
					{
						throw new InvalidEnumArgumentException("value", (int)value, typeof(BorderStyle));
					}
					this.borderStyle = value;
					base.UpdateStyles();
				}
			}
		}

		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x0600326F RID: 12911 RVA: 0x000E25F0 File Offset: 0x000E07F0
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ExStyle |= 65536;
				createParams.ExStyle &= -513;
				createParams.Style &= -8388609;
				BorderStyle borderStyle = this.borderStyle;
				if (borderStyle != BorderStyle.FixedSingle)
				{
					if (borderStyle == BorderStyle.Fixed3D)
					{
						createParams.ExStyle |= 512;
					}
				}
				else
				{
					createParams.Style |= 8388608;
				}
				return createParams;
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x06003270 RID: 12912 RVA: 0x000B8F3C File Offset: 0x000B713C
		protected override Size DefaultSize
		{
			get
			{
				return new Size(200, 100);
			}
		}

		// Token: 0x06003271 RID: 12913 RVA: 0x000E2670 File Offset: 0x000E0870
		internal override Size GetPreferredSizeCore(Size proposedSize)
		{
			Size size = this.SizeFromClientSize(Size.Empty);
			Size size2 = size + base.Padding.Size;
			return this.LayoutEngine.GetPreferredSize(this, proposedSize - size2) + size2;
		}

		/// <summary>This member is not meaningful for this control.</summary>
		// Token: 0x1400024D RID: 589
		// (add) Token: 0x06003272 RID: 12914 RVA: 0x000B9104 File Offset: 0x000B7304
		// (remove) Token: 0x06003273 RID: 12915 RVA: 0x000B910D File Offset: 0x000B730D
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

		/// <summary>This member is not meaningful for this control.</summary>
		// Token: 0x1400024E RID: 590
		// (add) Token: 0x06003274 RID: 12916 RVA: 0x000B9116 File Offset: 0x000B7316
		// (remove) Token: 0x06003275 RID: 12917 RVA: 0x000B911F File Offset: 0x000B731F
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

		/// <summary>This member is not meaningful for this control.</summary>
		// Token: 0x1400024F RID: 591
		// (add) Token: 0x06003276 RID: 12918 RVA: 0x000B9128 File Offset: 0x000B7328
		// (remove) Token: 0x06003277 RID: 12919 RVA: 0x000B9131 File Offset: 0x000B7331
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

		/// <summary>Gets or sets a value indicating whether the user can give the focus to this control using the TAB key.</summary>
		/// <returns>
		///   <see langword="true" /> if the user can give the focus to the control using the TAB key; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x06003278 RID: 12920 RVA: 0x000B2395 File Offset: 0x000B0595
		// (set) Token: 0x06003279 RID: 12921 RVA: 0x000B239D File Offset: 0x000B059D
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

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>The text associated with this control.</returns>
		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x0600327A RID: 12922 RVA: 0x00013814 File Offset: 0x00011A14
		// (set) Token: 0x0600327B RID: 12923 RVA: 0x00023FE9 File Offset: 0x000221E9
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

		/// <summary>This member is not meaningful for this control.</summary>
		// Token: 0x14000250 RID: 592
		// (add) Token: 0x0600327C RID: 12924 RVA: 0x00046591 File Offset: 0x00044791
		// (remove) Token: 0x0600327D RID: 12925 RVA: 0x0004659A File Offset: 0x0004479A
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

		/// <summary>Fires the event indicating that the panel has been resized. Inheriting controls should use this in favor of actually listening to the event, but should still call <see langword="base.onResize" /> to ensure that the event is fired for external listeners.</summary>
		/// <param name="eventargs">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600327E RID: 12926 RVA: 0x000E26B7 File Offset: 0x000E08B7
		protected override void OnResize(EventArgs eventargs)
		{
			if (base.DesignMode && this.borderStyle == BorderStyle.None)
			{
				base.Invalidate();
			}
			base.OnResize(eventargs);
		}

		// Token: 0x0600327F RID: 12927 RVA: 0x000E26D8 File Offset: 0x000E08D8
		internal override void PrintToMetaFileRecursive(HandleRef hDC, IntPtr lParam, Rectangle bounds)
		{
			base.PrintToMetaFileRecursive(hDC, lParam, bounds);
			using (new WindowsFormsUtils.DCMapping(hDC, bounds))
			{
				using (Graphics graphics = Graphics.FromHdcInternal(hDC.Handle))
				{
					ControlPaint.PrintBorder(graphics, new Rectangle(Point.Empty, base.Size), this.BorderStyle, Border3DStyle.Sunken);
				}
			}
		}

		// Token: 0x06003280 RID: 12928 RVA: 0x000E2758 File Offset: 0x000E0958
		private static string StringFromBorderStyle(BorderStyle value)
		{
			Type typeFromHandle = typeof(BorderStyle);
			if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
			{
				return "[Invalid BorderStyle]";
			}
			return typeFromHandle.ToString() + "." + value.ToString();
		}

		/// <summary>Returns a string representation for this control.</summary>
		/// <returns>A <see cref="T:System.String" /> representation of the control.</returns>
		// Token: 0x06003281 RID: 12929 RVA: 0x000E27A4 File Offset: 0x000E09A4
		public override string ToString()
		{
			string text = base.ToString();
			return text + ", BorderStyle: " + Panel.StringFromBorderStyle(this.borderStyle);
		}

		// Token: 0x04001E6E RID: 7790
		private BorderStyle borderStyle;
	}
}
