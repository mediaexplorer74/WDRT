using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms.ButtonInternal;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Implements the basic functionality common to button controls.</summary>
	// Token: 0x02000142 RID: 322
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Designer("System.Windows.Forms.Design.ButtonBaseDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public abstract class ButtonBase : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ButtonBase" /> class.</summary>
		// Token: 0x06000C65 RID: 3173 RVA: 0x000239B8 File Offset: 0x00021BB8
		protected ButtonBase()
		{
			base.SetStyle(ControlStyles.Opaque | ControlStyles.ResizeRedraw | ControlStyles.StandardClick | ControlStyles.SupportsTransparentBackColor | ControlStyles.CacheText | ControlStyles.OptimizedDoubleBuffer, true);
			base.SetState2(2048, true);
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.UserMouse, this.OwnerDraw);
			this.SetFlag(128, true);
			this.SetFlag(256, false);
		}

		/// <summary>Gets or sets a value indicating whether the ellipsis character (...) appears at the right edge of the control, denoting that the control text extends beyond the specified length of the control.</summary>
		/// <returns>
		///   <see langword="true" /> if the additional label text is to be indicated by an ellipsis; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x00023A35 File Offset: 0x00021C35
		// (set) Token: 0x06000C67 RID: 3175 RVA: 0x00023A3F File Offset: 0x00021C3F
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[SRDescription("ButtonAutoEllipsisDescr")]
		public bool AutoEllipsis
		{
			get
			{
				return this.GetFlag(32);
			}
			set
			{
				if (this.AutoEllipsis != value)
				{
					this.SetFlag(32, value);
					if (value && this.textToolTip == null)
					{
						this.textToolTip = new ToolTip();
					}
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets a value that indicates whether the control resizes based on its contents.</summary>
		/// <returns>
		///   <see langword="true" /> if the control automatically resizes based on its contents; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x00011831 File Offset: 0x0000FA31
		// (set) Token: 0x06000C69 RID: 3177 RVA: 0x00023A6F File Offset: 0x00021C6F
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
				if (value)
				{
					this.AutoEllipsis = false;
				}
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ButtonBase.AutoSize" /> property changes.</summary>
		// Token: 0x14000064 RID: 100
		// (add) Token: 0x06000C6A RID: 3178 RVA: 0x00011842 File Offset: 0x0000FA42
		// (remove) Token: 0x06000C6B RID: 3179 RVA: 0x0001184B File Offset: 0x0000FA4B
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

		/// <summary>Gets or sets the background color of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> value representing the background color.</returns>
		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000C6C RID: 3180 RVA: 0x0001A049 File Offset: 0x00018249
		// (set) Token: 0x06000C6D RID: 3181 RVA: 0x00023A84 File Offset: 0x00021C84
		[SRCategory("CatAppearance")]
		[SRDescription("ControlBackColorDescr")]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				if (base.DesignMode)
				{
					if (value != Color.Empty)
					{
						PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this)["UseVisualStyleBackColor"];
						if (propertyDescriptor != null)
						{
							propertyDescriptor.SetValue(this, false);
						}
					}
				}
				else
				{
					this.UseVisualStyleBackColor = false;
				}
				base.BackColor = value;
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x0001A0BA File Offset: 0x000182BA
		protected override Size DefaultSize
		{
			get
			{
				return new Size(75, 23);
			}
		}

		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x00023AD8 File Offset: 0x00021CD8
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				if (!this.OwnerDraw)
				{
					createParams.ExStyle &= -4097;
					createParams.Style |= 8192;
					if (this.IsDefault)
					{
						createParams.Style |= 1;
					}
					ContentAlignment contentAlignment = base.RtlTranslateContent(this.TextAlign);
					if ((contentAlignment & WindowsFormsUtils.AnyLeftAlign) != (ContentAlignment)0)
					{
						createParams.Style |= 256;
					}
					else if ((contentAlignment & WindowsFormsUtils.AnyRightAlign) != (ContentAlignment)0)
					{
						createParams.Style |= 512;
					}
					else
					{
						createParams.Style |= 768;
					}
					if ((contentAlignment & WindowsFormsUtils.AnyTopAlign) != (ContentAlignment)0)
					{
						createParams.Style |= 1024;
					}
					else if ((contentAlignment & WindowsFormsUtils.AnyBottomAlign) != (ContentAlignment)0)
					{
						createParams.Style |= 2048;
					}
					else
					{
						createParams.Style |= 3072;
					}
				}
				return createParams;
			}
		}

		/// <summary>Gets the default Input Method Editor (IME) mode supported by this control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x00023BD7 File Offset: 0x00021DD7
		protected override ImeMode DefaultImeMode
		{
			get
			{
				return ImeMode.Disable;
			}
		}

		/// <summary>Gets or sets a value indicating whether the button control is the default button.</summary>
		/// <returns>
		///   <see langword="true" /> if the button control is the default button; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000C71 RID: 3185 RVA: 0x00023BDA File Offset: 0x00021DDA
		// (set) Token: 0x06000C72 RID: 3186 RVA: 0x00023BE4 File Offset: 0x00021DE4
		protected internal bool IsDefault
		{
			get
			{
				return this.GetFlag(64);
			}
			set
			{
				if (this.GetFlag(64) != value)
				{
					this.SetFlag(64, value);
					if (base.IsHandleCreated)
					{
						if (this.OwnerDraw)
						{
							base.Invalidate();
							return;
						}
						base.UpdateStyles();
					}
				}
			}
		}

		/// <summary>Gets or sets the flat style appearance of the button control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.FlatStyle" /> values. The default value is <see langword="Standard" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.FlatStyle" /> values.</exception>
		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x00023C17 File Offset: 0x00021E17
		// (set) Token: 0x06000C74 RID: 3188 RVA: 0x00023C20 File Offset: 0x00021E20
		[SRCategory("CatAppearance")]
		[DefaultValue(FlatStyle.Standard)]
		[Localizable(true)]
		[SRDescription("ButtonFlatStyleDescr")]
		public FlatStyle FlatStyle
		{
			get
			{
				return this.flatStyle;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(FlatStyle));
				}
				this.flatStyle = value;
				LayoutTransaction.DoLayoutIf(this.AutoSize, this.ParentInternal, this, PropertyNames.FlatStyle);
				base.Invalidate();
				this.UpdateOwnerDraw();
			}
		}

		/// <summary>Gets the appearance of the border and the colors used to indicate check state and mouse state.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.FlatButtonAppearance" /> values.</returns>
		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x00023C7D File Offset: 0x00021E7D
		[Browsable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ButtonFlatAppearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public FlatButtonAppearance FlatAppearance
		{
			get
			{
				if (this.flatAppearance == null)
				{
					this.flatAppearance = new FlatButtonAppearance(this);
				}
				return this.flatAppearance;
			}
		}

		/// <summary>Gets or sets the image that is displayed on a button control.</summary>
		/// <returns>The <see cref="T:System.Drawing.Image" /> displayed on the button control. The default value is <see langword="null" />.</returns>
		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x00023C9C File Offset: 0x00021E9C
		// (set) Token: 0x06000C77 RID: 3191 RVA: 0x00023D08 File Offset: 0x00021F08
		[SRDescription("ButtonImageDescr")]
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		public Image Image
		{
			get
			{
				if (this.image == null && this.imageList != null)
				{
					int num = this.imageIndex.ActualIndex;
					if (num >= this.imageList.Images.Count)
					{
						num = this.imageList.Images.Count - 1;
					}
					if (num >= 0)
					{
						return this.imageList.Images[num];
					}
				}
				return this.image;
			}
			set
			{
				if (this.Image != value)
				{
					this.StopAnimate();
					this.image = value;
					if (this.image != null)
					{
						this.ImageIndex = -1;
						this.ImageList = null;
					}
					LayoutTransaction.DoLayoutIf(this.AutoSize, this.ParentInternal, this, PropertyNames.Image);
					this.Animate();
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the alignment of the image on the button control.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values. The default value is <see langword="MiddleCenter" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Drawing.ContentAlignment" /> values.</exception>
		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x00023D64 File Offset: 0x00021F64
		// (set) Token: 0x06000C79 RID: 3193 RVA: 0x00023D6C File Offset: 0x00021F6C
		[DefaultValue(ContentAlignment.MiddleCenter)]
		[Localizable(true)]
		[SRDescription("ButtonImageAlignDescr")]
		[SRCategory("CatAppearance")]
		public ContentAlignment ImageAlign
		{
			get
			{
				return this.imageAlign;
			}
			set
			{
				if (!WindowsFormsUtils.EnumValidator.IsValidContentAlignment(value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ContentAlignment));
				}
				if (value != this.imageAlign)
				{
					this.imageAlign = value;
					LayoutTransaction.DoLayoutIf(this.AutoSize, this.ParentInternal, this, PropertyNames.ImageAlign);
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the image list index value of the image displayed on the button control.</summary>
		/// <returns>A zero-based index, which represents the image position in an <see cref="T:System.Windows.Forms.ImageList" />. The default is -1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is less than the lower bounds of the <see cref="P:System.Windows.Forms.ButtonBase.ImageIndex" />.</exception>
		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x00023DC4 File Offset: 0x00021FC4
		// (set) Token: 0x06000C7B RID: 3195 RVA: 0x00023E24 File Offset: 0x00022024
		[TypeConverter(typeof(ImageIndexConverter))]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Localizable(true)]
		[DefaultValue(-1)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRDescription("ButtonImageIndexDescr")]
		[SRCategory("CatAppearance")]
		public int ImageIndex
		{
			get
			{
				if (this.imageIndex.Index != -1 && this.imageList != null && this.imageIndex.Index >= this.imageList.Images.Count)
				{
					return this.imageList.Images.Count - 1;
				}
				return this.imageIndex.Index;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("ImageIndex", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"ImageIndex",
						value.ToString(CultureInfo.CurrentCulture),
						(-1).ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this.imageIndex.Index != value)
				{
					if (value != -1)
					{
						this.image = null;
					}
					this.imageIndex.Index = value;
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the key accessor for the image in the <see cref="P:System.Windows.Forms.ButtonBase.ImageList" />.</summary>
		/// <returns>A string representing the key of the image.</returns>
		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x00023EA3 File Offset: 0x000220A3
		// (set) Token: 0x06000C7D RID: 3197 RVA: 0x00023EB0 File Offset: 0x000220B0
		[TypeConverter(typeof(ImageKeyConverter))]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Localizable(true)]
		[DefaultValue("")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRDescription("ButtonImageIndexDescr")]
		[SRCategory("CatAppearance")]
		public string ImageKey
		{
			get
			{
				return this.imageIndex.Key;
			}
			set
			{
				if (this.imageIndex.Key != value)
				{
					if (value != null)
					{
						this.image = null;
					}
					this.imageIndex.Key = value;
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ImageList" /> that contains the <see cref="T:System.Drawing.Image" /> displayed on a button control.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageList" />. The default value is <see langword="null" />.</returns>
		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x00023EE1 File Offset: 0x000220E1
		// (set) Token: 0x06000C7F RID: 3199 RVA: 0x00023EEC File Offset: 0x000220EC
		[DefaultValue(null)]
		[SRDescription("ButtonImageListDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRCategory("CatAppearance")]
		public ImageList ImageList
		{
			get
			{
				return this.imageList;
			}
			set
			{
				if (this.imageList != value)
				{
					EventHandler eventHandler = new EventHandler(this.ImageListRecreateHandle);
					EventHandler eventHandler2 = new EventHandler(this.DetachImageList);
					if (this.imageList != null)
					{
						this.imageList.RecreateHandle -= eventHandler;
						this.imageList.Disposed -= eventHandler2;
					}
					if (value != null)
					{
						this.image = null;
					}
					this.imageList = value;
					this.imageIndex.ImageList = value;
					if (value != null)
					{
						value.RecreateHandle += eventHandler;
						value.Disposed += eventHandler2;
					}
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the Input Method Editor (IME) mode supported by this control. This property is not relevant for this class.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000C80 RID: 3200 RVA: 0x0001A051 File Offset: 0x00018251
		// (set) Token: 0x06000C81 RID: 3201 RVA: 0x0001A059 File Offset: 0x00018259
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ButtonBase.ImeMode" /> property is changed. This event is not relevant for this class.</summary>
		// Token: 0x14000065 RID: 101
		// (add) Token: 0x06000C82 RID: 3202 RVA: 0x00023F70 File Offset: 0x00022170
		// (remove) Token: 0x06000C83 RID: 3203 RVA: 0x00023F79 File Offset: 0x00022179
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

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x00012E4E File Offset: 0x0001104E
		internal override bool IsMnemonicsListenerAxSourced
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000C85 RID: 3205 RVA: 0x00023F82 File Offset: 0x00022182
		internal virtual Rectangle OverChangeRectangle
		{
			get
			{
				if (this.FlatStyle == FlatStyle.Standard)
				{
					return new Rectangle(-1, -1, 1, 1);
				}
				return base.ClientRectangle;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x00023F9D File Offset: 0x0002219D
		internal bool OwnerDraw
		{
			get
			{
				return this.FlatStyle != FlatStyle.System;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000C87 RID: 3207 RVA: 0x00023FAB File Offset: 0x000221AB
		internal virtual Rectangle DownChangeRectangle
		{
			get
			{
				return base.ClientRectangle;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x00023FB3 File Offset: 0x000221B3
		internal bool MouseIsPressed
		{
			get
			{
				return this.GetFlag(4);
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000C89 RID: 3209 RVA: 0x00023FBC File Offset: 0x000221BC
		internal bool MouseIsDown
		{
			get
			{
				return this.GetFlag(2);
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x00023FC5 File Offset: 0x000221C5
		internal bool MouseIsOver
		{
			get
			{
				return this.GetFlag(1);
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000C8B RID: 3211 RVA: 0x00023FCE File Offset: 0x000221CE
		// (set) Token: 0x06000C8C RID: 3212 RVA: 0x00023FDB File Offset: 0x000221DB
		internal bool ShowToolTip
		{
			get
			{
				return this.GetFlag(256);
			}
			set
			{
				this.SetFlag(256, value);
			}
		}

		/// <summary>Gets or sets the text associated with this control.</summary>
		/// <returns>The text associated with this control.</returns>
		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x00013814 File Offset: 0x00011A14
		// (set) Token: 0x06000C8E RID: 3214 RVA: 0x00023FE9 File Offset: 0x000221E9
		[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[SettingsBindable(true)]
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

		/// <summary>Gets or sets the alignment of the text on the button control.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values. The default is <see langword="MiddleCenter" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Drawing.ContentAlignment" /> values.</exception>
		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000C8F RID: 3215 RVA: 0x00023FF2 File Offset: 0x000221F2
		// (set) Token: 0x06000C90 RID: 3216 RVA: 0x00023FFC File Offset: 0x000221FC
		[DefaultValue(ContentAlignment.MiddleCenter)]
		[Localizable(true)]
		[SRDescription("ButtonTextAlignDescr")]
		[SRCategory("CatAppearance")]
		public virtual ContentAlignment TextAlign
		{
			get
			{
				return this.textAlign;
			}
			set
			{
				if (!WindowsFormsUtils.EnumValidator.IsValidContentAlignment(value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ContentAlignment));
				}
				if (value != this.textAlign)
				{
					this.textAlign = value;
					LayoutTransaction.DoLayoutIf(this.AutoSize, this.ParentInternal, this, PropertyNames.TextAlign);
					if (this.OwnerDraw)
					{
						base.Invalidate();
						return;
					}
					base.UpdateStyles();
				}
			}
		}

		/// <summary>Gets or sets the position of text and image relative to each other.</summary>
		/// <returns>One of the values of <see cref="T:System.Windows.Forms.TextImageRelation" />. The default is <see cref="F:System.Windows.Forms.TextImageRelation.Overlay" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value is not one of the <see cref="T:System.Windows.Forms.TextImageRelation" /> values.</exception>
		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000C91 RID: 3217 RVA: 0x00024063 File Offset: 0x00022263
		// (set) Token: 0x06000C92 RID: 3218 RVA: 0x0002406C File Offset: 0x0002226C
		[DefaultValue(TextImageRelation.Overlay)]
		[Localizable(true)]
		[SRDescription("ButtonTextImageRelationDescr")]
		[SRCategory("CatAppearance")]
		public TextImageRelation TextImageRelation
		{
			get
			{
				return this.textImageRelation;
			}
			set
			{
				if (!WindowsFormsUtils.EnumValidator.IsValidTextImageRelation(value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(TextImageRelation));
				}
				if (value != this.TextImageRelation)
				{
					this.textImageRelation = value;
					LayoutTransaction.DoLayoutIf(this.AutoSize, this.ParentInternal, this, PropertyNames.TextImageRelation);
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the first character that is preceded by an ampersand (&amp;) is used as the mnemonic key of the control.</summary>
		/// <returns>
		///   <see langword="true" /> if the first character that is preceded by an ampersand (&amp;) is used as the mnemonic key of the control; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000C93 RID: 3219 RVA: 0x000240C4 File Offset: 0x000222C4
		// (set) Token: 0x06000C94 RID: 3220 RVA: 0x000240D1 File Offset: 0x000222D1
		[SRDescription("ButtonUseMnemonicDescr")]
		[DefaultValue(true)]
		[SRCategory("CatAppearance")]
		public bool UseMnemonic
		{
			get
			{
				return this.GetFlag(128);
			}
			set
			{
				this.SetFlag(128, value);
				LayoutTransaction.DoLayoutIf(this.AutoSize, this.ParentInternal, this, PropertyNames.Text);
				base.Invalidate();
			}
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x000240FC File Offset: 0x000222FC
		private void Animate()
		{
			this.Animate(!base.DesignMode && base.Visible && base.Enabled && this.ParentInternal != null);
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x00024128 File Offset: 0x00022328
		private void StopAnimate()
		{
			this.Animate(false);
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x00024134 File Offset: 0x00022334
		private void Animate(bool animate)
		{
			if (animate != this.GetFlag(16))
			{
				if (animate)
				{
					if (this.image != null)
					{
						ImageAnimator.Animate(this.image, new EventHandler(this.OnFrameChanged));
						this.SetFlag(16, animate);
						return;
					}
				}
				else if (this.image != null)
				{
					ImageAnimator.StopAnimate(this.image, new EventHandler(this.OnFrameChanged));
					this.SetFlag(16, animate);
				}
			}
		}

		/// <summary>Creates a new accessibility object for the control.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the control.</returns>
		// Token: 0x06000C98 RID: 3224 RVA: 0x000241A0 File Offset: 0x000223A0
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new ButtonBase.ButtonBaseAccessibleObject(this);
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x000241A8 File Offset: 0x000223A8
		private void DetachImageList(object sender, EventArgs e)
		{
			this.ImageList = null;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ButtonBase" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06000C9A RID: 3226 RVA: 0x000241B4 File Offset: 0x000223B4
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.StopAnimate();
				if (this.imageList != null)
				{
					this.imageList.Disposed -= this.DetachImageList;
				}
				if (this.textToolTip != null)
				{
					this.textToolTip.Dispose();
					this.textToolTip = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x0002420A File Offset: 0x0002240A
		private bool GetFlag(int flag)
		{
			return (this.state & flag) == flag;
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x00024217 File Offset: 0x00022417
		private void ImageListRecreateHandle(object sender, EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				base.Invalidate();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000C9D RID: 3229 RVA: 0x00024227 File Offset: 0x00022427
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			base.Invalidate();
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.ButtonBase.OnLostFocus(System.EventArgs)" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000C9E RID: 3230 RVA: 0x00024236 File Offset: 0x00022436
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			this.SetFlag(2, false);
			base.CaptureInternal = false;
			base.Invalidate();
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.OnMouseEnter(System.EventArgs)" /> event.</summary>
		/// <param name="eventargs">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000C9F RID: 3231 RVA: 0x00024254 File Offset: 0x00022454
		protected override void OnMouseEnter(EventArgs eventargs)
		{
			this.SetFlag(1, true);
			base.Invalidate();
			if (!base.DesignMode && this.AutoEllipsis && this.ShowToolTip && this.textToolTip != null)
			{
				IntSecurity.AllWindows.Assert();
				try
				{
					this.textToolTip.Show(WindowsFormsUtils.TextWithoutMnemonics(this.Text), this);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}
			base.OnMouseEnter(eventargs);
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.OnMouseLeave(System.EventArgs)" /> event.</summary>
		/// <param name="eventargs">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000CA0 RID: 3232 RVA: 0x000242D0 File Offset: 0x000224D0
		protected override void OnMouseLeave(EventArgs eventargs)
		{
			this.SetFlag(1, false);
			if (this.textToolTip != null)
			{
				IntSecurity.AllWindows.Assert();
				try
				{
					this.textToolTip.Hide(this);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}
			base.Invalidate();
			base.OnMouseLeave(eventargs);
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.OnMouseMove(System.Windows.Forms.MouseEventArgs)" /> event.</summary>
		/// <param name="mevent">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06000CA1 RID: 3233 RVA: 0x00024328 File Offset: 0x00022528
		protected override void OnMouseMove(MouseEventArgs mevent)
		{
			if (mevent.Button != MouseButtons.None && this.GetFlag(4))
			{
				if (!base.ClientRectangle.Contains(mevent.X, mevent.Y))
				{
					if (this.GetFlag(2))
					{
						this.SetFlag(2, false);
						base.Invalidate(this.DownChangeRectangle);
					}
				}
				else if (!this.GetFlag(2))
				{
					this.SetFlag(2, true);
					base.Invalidate(this.DownChangeRectangle);
				}
			}
			base.OnMouseMove(mevent);
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.OnMouseDown(System.Windows.Forms.MouseEventArgs)" /> event.</summary>
		/// <param name="mevent">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06000CA2 RID: 3234 RVA: 0x000243A5 File Offset: 0x000225A5
		protected override void OnMouseDown(MouseEventArgs mevent)
		{
			if (mevent.Button == MouseButtons.Left)
			{
				this.SetFlag(2, true);
				this.SetFlag(4, true);
				base.Invalidate(this.DownChangeRectangle);
			}
			base.OnMouseDown(mevent);
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.ButtonBase.OnMouseUp(System.Windows.Forms.MouseEventArgs)" /> event.</summary>
		/// <param name="mevent">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06000CA3 RID: 3235 RVA: 0x000243D7 File Offset: 0x000225D7
		protected override void OnMouseUp(MouseEventArgs mevent)
		{
			base.OnMouseUp(mevent);
		}

		/// <summary>Resets the <see cref="T:System.Windows.Forms.Button" /> control to the state before it is pressed and redraws it.</summary>
		// Token: 0x06000CA4 RID: 3236 RVA: 0x000243E0 File Offset: 0x000225E0
		protected void ResetFlagsandPaint()
		{
			this.SetFlag(4, false);
			this.SetFlag(2, false);
			base.Invalidate(this.DownChangeRectangle);
			base.Update();
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x00024404 File Offset: 0x00022604
		private void PaintControl(PaintEventArgs pevent)
		{
			this.Adapter.Paint(pevent);
		}

		/// <summary>Retrieves the size of a rectangular area into which a control can be fitted.</summary>
		/// <param name="proposedSize">The custom-sized area for a control.</param>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		// Token: 0x06000CA6 RID: 3238 RVA: 0x00024412 File Offset: 0x00022612
		public override Size GetPreferredSize(Size proposedSize)
		{
			if (proposedSize.Width == 1)
			{
				proposedSize.Width = 0;
			}
			if (proposedSize.Height == 1)
			{
				proposedSize.Height = 0;
			}
			return base.GetPreferredSize(proposedSize);
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x00024440 File Offset: 0x00022640
		internal override Size GetPreferredSizeCore(Size proposedConstraints)
		{
			Size preferredSizeCore = this.Adapter.GetPreferredSizeCore(proposedConstraints);
			return LayoutUtils.UnionSizes(preferredSizeCore + base.Padding.Size, this.MinimumSize);
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x0002447C File Offset: 0x0002267C
		internal ButtonBaseAdapter Adapter
		{
			get
			{
				if (this._adapter == null || this.FlatStyle != this._cachedAdapterType)
				{
					switch (this.FlatStyle)
					{
					case FlatStyle.Flat:
						this._adapter = this.CreateFlatAdapter();
						break;
					case FlatStyle.Popup:
						this._adapter = this.CreatePopupAdapter();
						break;
					case FlatStyle.Standard:
						this._adapter = this.CreateStandardAdapter();
						break;
					}
					this._cachedAdapterType = this.FlatStyle;
				}
				return this._adapter;
			}
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x00015C90 File Offset: 0x00013E90
		internal virtual ButtonBaseAdapter CreateFlatAdapter()
		{
			return null;
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x00015C90 File Offset: 0x00013E90
		internal virtual ButtonBaseAdapter CreatePopupAdapter()
		{
			return null;
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x00015C90 File Offset: 0x00013E90
		internal virtual ButtonBaseAdapter CreateStandardAdapter()
		{
			return null;
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x000244F4 File Offset: 0x000226F4
		internal virtual StringFormat CreateStringFormat()
		{
			if (this.Adapter == null)
			{
				return new StringFormat();
			}
			return this.Adapter.CreateStringFormat();
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0002450F File Offset: 0x0002270F
		internal virtual TextFormatFlags CreateTextFormatFlags()
		{
			if (this.Adapter == null)
			{
				return TextFormatFlags.Default;
			}
			return this.Adapter.CreateTextFormatFlags();
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x00024528 File Offset: 0x00022728
		private void OnFrameChanged(object o, EventArgs e)
		{
			if (base.Disposing || base.IsDisposed)
			{
				return;
			}
			if (base.IsHandleCreated && base.InvokeRequired)
			{
				base.BeginInvoke(new EventHandler(this.OnFrameChanged), new object[] { o, e });
				return;
			}
			base.Invalidate();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.EnabledChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000CAF RID: 3247 RVA: 0x0002457E File Offset: 0x0002277E
		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
			this.Animate();
			if (!base.Enabled)
			{
				this.SetFlag(2, false);
				this.SetFlag(1, false);
				base.Invalidate();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.TextChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000CB0 RID: 3248 RVA: 0x000245AC File Offset: 0x000227AC
		protected override void OnTextChanged(EventArgs e)
		{
			using (LayoutTransaction.CreateTransactionIf(this.AutoSize, this.ParentInternal, this, PropertyNames.Text))
			{
				base.OnTextChanged(e);
				base.Invalidate();
			}
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.ButtonBase.OnKeyUp(System.Windows.Forms.KeyEventArgs)" /> event.</summary>
		/// <param name="kevent">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
		// Token: 0x06000CB1 RID: 3249 RVA: 0x000245FC File Offset: 0x000227FC
		protected override void OnKeyDown(KeyEventArgs kevent)
		{
			if (kevent.KeyData == Keys.Space)
			{
				if (!this.GetFlag(2))
				{
					this.SetFlag(2, true);
					if (!this.OwnerDraw)
					{
						base.SendMessage(243, 1, 0);
					}
					base.Invalidate(this.DownChangeRectangle);
				}
				kevent.Handled = true;
			}
			base.OnKeyDown(kevent);
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.ButtonBase.OnKeyUp(System.Windows.Forms.KeyEventArgs)" /> event.</summary>
		/// <param name="kevent">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
		// Token: 0x06000CB2 RID: 3250 RVA: 0x00024654 File Offset: 0x00022854
		protected override void OnKeyUp(KeyEventArgs kevent)
		{
			if (this.GetFlag(2) && !base.ValidationCancelled)
			{
				if (this.OwnerDraw)
				{
					this.ResetFlagsandPaint();
				}
				else
				{
					this.SetFlag(4, false);
					this.SetFlag(2, false);
					base.SendMessage(243, 0, 0);
				}
				if (kevent.KeyCode == Keys.Return || kevent.KeyCode == Keys.Space)
				{
					this.OnClick(EventArgs.Empty);
				}
				kevent.Handled = true;
			}
			base.OnKeyUp(kevent);
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.ButtonBase.OnPaint(System.Windows.Forms.PaintEventArgs)" /> event.</summary>
		/// <param name="pevent">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		// Token: 0x06000CB3 RID: 3251 RVA: 0x000246D0 File Offset: 0x000228D0
		protected override void OnPaint(PaintEventArgs pevent)
		{
			if (this.AutoEllipsis)
			{
				Size preferredSize = base.PreferredSize;
				this.ShowToolTip = base.ClientRectangle.Width < preferredSize.Width || base.ClientRectangle.Height < preferredSize.Height;
			}
			else
			{
				this.ShowToolTip = false;
			}
			if (base.GetStyle(ControlStyles.UserPaint))
			{
				this.Animate();
				ImageAnimator.UpdateFrames(this.Image);
				this.PaintControl(pevent);
			}
			base.OnPaint(pevent);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ParentChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000CB4 RID: 3252 RVA: 0x00024754 File Offset: 0x00022954
		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
			this.Animate();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.VisibleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
		// Token: 0x06000CB5 RID: 3253 RVA: 0x00024763 File Offset: 0x00022963
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			this.Animate();
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x00024772 File Offset: 0x00022972
		private void ResetImage()
		{
			this.Image = null;
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x0002477C File Offset: 0x0002297C
		private void SetFlag(int flag, bool value)
		{
			bool flag2 = (this.state & flag) != 0;
			if (value)
			{
				this.state |= flag;
			}
			else
			{
				this.state &= ~flag;
			}
			if (this.OwnerDraw && (flag & 2) != 0 && value != flag2)
			{
				base.AccessibilityNotifyClients(AccessibleEvents.StateChange, -1);
			}
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x000247D4 File Offset: 0x000229D4
		private bool ShouldSerializeImage()
		{
			return this.image != null;
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x000247DF File Offset: 0x000229DF
		private void UpdateOwnerDraw()
		{
			if (this.OwnerDraw != base.GetStyle(ControlStyles.UserPaint))
			{
				base.SetStyle(ControlStyles.UserPaint | ControlStyles.UserMouse, this.OwnerDraw);
				base.RecreateHandle();
			}
		}

		/// <summary>Gets or sets a value that determines whether to use the <see cref="T:System.Drawing.Graphics" /> class (GDI+) or the <see cref="T:System.Windows.Forms.TextRenderer" /> class (GDI) to render text.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Drawing.Graphics" /> class should be used to perform text rendering for compatibility with versions 1.0 and 1.1. of the .NET Framework; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x00024807 File Offset: 0x00022A07
		// (set) Token: 0x06000CBB RID: 3259 RVA: 0x0002480F File Offset: 0x00022A0F
		[DefaultValue(false)]
		[SRCategory("CatBehavior")]
		[SRDescription("UseCompatibleTextRenderingDescr")]
		public bool UseCompatibleTextRendering
		{
			get
			{
				return base.UseCompatibleTextRenderingInt;
			}
			set
			{
				base.UseCompatibleTextRenderingInt = value;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000CBC RID: 3260 RVA: 0x00012E4E File Offset: 0x0001104E
		internal override bool SupportsUseCompatibleTextRendering
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets or sets a value that determines if the background is drawn using visual styles, if supported.</summary>
		/// <returns>
		///   <see langword="true" /> if the background is drawn using visual styles; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x00024818 File Offset: 0x00022A18
		// (set) Token: 0x06000CBE RID: 3262 RVA: 0x00024857 File Offset: 0x00022A57
		[SRCategory("CatAppearance")]
		[SRDescription("ButtonUseVisualStyleBackColorDescr")]
		public bool UseVisualStyleBackColor
		{
			get
			{
				return (this.isEnableVisualStyleBackgroundSet || (base.RawBackColor.IsEmpty && this.BackColor == SystemColors.Control)) && this.enableVisualStyleBackground;
			}
			set
			{
				this.isEnableVisualStyleBackgroundSet = true;
				this.enableVisualStyleBackground = value;
				base.Invalidate();
			}
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x0002486D File Offset: 0x00022A6D
		private void ResetUseVisualStyleBackColor()
		{
			this.isEnableVisualStyleBackgroundSet = false;
			this.enableVisualStyleBackground = true;
			base.Invalidate();
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x00024883 File Offset: 0x00022A83
		private bool ShouldSerializeUseVisualStyleBackColor()
		{
			return this.isEnableVisualStyleBackgroundSet;
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x06000CC1 RID: 3265 RVA: 0x0002488C File Offset: 0x00022A8C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg != 245)
			{
				if (this.OwnerDraw)
				{
					int msg2 = m.Msg;
					if (msg2 > 243)
					{
						if (msg2 <= 517)
						{
							if (msg2 != 514 && msg2 != 517)
							{
								goto IL_E6;
							}
						}
						else if (msg2 != 520)
						{
							if (msg2 == 533)
							{
								goto IL_8C;
							}
							goto IL_E6;
						}
						try
						{
							this.SetFlag(8, true);
							base.WndProc(ref m);
							return;
						}
						finally
						{
							this.SetFlag(8, false);
						}
						goto IL_E6;
					}
					if (msg2 != 8 && msg2 != 31)
					{
						if (msg2 != 243)
						{
							goto IL_E6;
						}
						return;
					}
					IL_8C:
					if (!this.GetFlag(8) && this.GetFlag(4))
					{
						this.SetFlag(4, false);
						if (this.GetFlag(2))
						{
							this.SetFlag(2, false);
							base.Invalidate(this.DownChangeRectangle);
						}
					}
					base.WndProc(ref m);
					return;
					IL_E6:
					base.WndProc(ref m);
					return;
				}
				int msg3 = m.Msg;
				if (msg3 == 8465)
				{
					if (NativeMethods.Util.HIWORD(m.WParam) == 0 && !base.ValidationCancelled)
					{
						this.OnClick(EventArgs.Empty);
						return;
					}
				}
				else
				{
					base.WndProc(ref m);
				}
				return;
			}
			if (this is IButtonControl)
			{
				((IButtonControl)this).PerformClick();
				return;
			}
			this.OnClick(EventArgs.Empty);
		}

		// Token: 0x04000722 RID: 1826
		private FlatStyle flatStyle = FlatStyle.Standard;

		// Token: 0x04000723 RID: 1827
		private ContentAlignment imageAlign = ContentAlignment.MiddleCenter;

		// Token: 0x04000724 RID: 1828
		private ContentAlignment textAlign = ContentAlignment.MiddleCenter;

		// Token: 0x04000725 RID: 1829
		private TextImageRelation textImageRelation;

		// Token: 0x04000726 RID: 1830
		private ImageList.Indexer imageIndex = new ImageList.Indexer();

		// Token: 0x04000727 RID: 1831
		private FlatButtonAppearance flatAppearance;

		// Token: 0x04000728 RID: 1832
		private ImageList imageList;

		// Token: 0x04000729 RID: 1833
		private Image image;

		// Token: 0x0400072A RID: 1834
		private const int FlagMouseOver = 1;

		// Token: 0x0400072B RID: 1835
		private const int FlagMouseDown = 2;

		// Token: 0x0400072C RID: 1836
		private const int FlagMousePressed = 4;

		// Token: 0x0400072D RID: 1837
		private const int FlagInButtonUp = 8;

		// Token: 0x0400072E RID: 1838
		private const int FlagCurrentlyAnimating = 16;

		// Token: 0x0400072F RID: 1839
		private const int FlagAutoEllipsis = 32;

		// Token: 0x04000730 RID: 1840
		private const int FlagIsDefault = 64;

		// Token: 0x04000731 RID: 1841
		private const int FlagUseMnemonic = 128;

		// Token: 0x04000732 RID: 1842
		private const int FlagShowToolTip = 256;

		// Token: 0x04000733 RID: 1843
		private int state;

		// Token: 0x04000734 RID: 1844
		private ToolTip textToolTip;

		// Token: 0x04000735 RID: 1845
		private bool enableVisualStyleBackground = true;

		// Token: 0x04000736 RID: 1846
		private bool isEnableVisualStyleBackgroundSet;

		// Token: 0x04000737 RID: 1847
		private ButtonBaseAdapter _adapter;

		// Token: 0x04000738 RID: 1848
		private FlatStyle _cachedAdapterType;

		/// <summary>Provides information that accessibility applications use to adjust an application's user interface for users with disabilities.</summary>
		// Token: 0x0200061A RID: 1562
		[ComVisible(true)]
		public class ButtonBaseAccessibleObject : Control.ControlAccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ButtonBase.ButtonBaseAccessibleObject" /> class.</summary>
			/// <param name="owner">The owner of this <see cref="T:System.Windows.Forms.ButtonBase.ButtonBaseAccessibleObject" />.</param>
			// Token: 0x060062F2 RID: 25330 RVA: 0x0009B733 File Offset: 0x00099933
			public ButtonBaseAccessibleObject(Control owner)
				: base(owner)
			{
			}

			/// <summary>Performs the default action associated with this accessible object.</summary>
			// Token: 0x060062F3 RID: 25331 RVA: 0x0016E09D File Offset: 0x0016C29D
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void DoDefaultAction()
			{
				((ButtonBase)base.Owner).OnClick(EventArgs.Empty);
			}

			/// <summary>Gets the state of this accessible object.</summary>
			/// <returns>One of the <see cref="T:System.Windows.Forms.AccessibleStates" /> values.</returns>
			// Token: 0x17001516 RID: 5398
			// (get) Token: 0x060062F4 RID: 25332 RVA: 0x0016E0B4 File Offset: 0x0016C2B4
			public override AccessibleStates State
			{
				get
				{
					AccessibleStates accessibleStates = base.State;
					ButtonBase buttonBase = (ButtonBase)base.Owner;
					if (buttonBase.OwnerDraw && buttonBase.MouseIsDown)
					{
						accessibleStates |= AccessibleStates.Pressed;
					}
					return accessibleStates;
				}
			}
		}
	}
}
