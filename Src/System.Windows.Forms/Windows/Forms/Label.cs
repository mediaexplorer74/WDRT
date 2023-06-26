using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms.Automation;
using System.Windows.Forms.Internal;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Represents a standard Windows label.</summary>
	// Token: 0x020002B8 RID: 696
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultProperty("Text")]
	[DefaultBindingProperty("Text")]
	[Designer("System.Windows.Forms.Design.LabelDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxItem("System.Windows.Forms.Design.AutoSizeToolboxItem,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionLabel")]
	public class Label : Control, IAutomationLiveRegion
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Label" /> class.</summary>
		// Token: 0x06002A9D RID: 10909 RVA: 0x000C0844 File Offset: 0x000BEA44
		public Label()
		{
			base.SetState2(2048, true);
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, this.IsOwnerDraw());
			base.SetStyle(ControlStyles.FixedHeight | ControlStyles.Selectable, false);
			base.SetStyle(ControlStyles.ResizeRedraw, true);
			CommonProperties.SetSelfAutoSizeInDefaultLayout(this, true);
			this.labelState[Label.StateFlatStyle] = 2;
			this.labelState[Label.StateUseMnemonic] = 1;
			this.labelState[Label.StateBorderStyle] = 0;
			this.TabStop = false;
			this.requestedHeight = base.Height;
			this.requestedWidth = base.Width;
		}

		/// <summary>Gets or sets a value indicating whether the control is automatically resized to display its entire contents.</summary>
		/// <returns>
		///   <see langword="true" /> if the control adjusts its width to closely fit its contents; otherwise, <see langword="false" />.  
		///
		///  When added to a form using the designer, the default value is <see langword="true" />. When instantiated from code, the default value is <see langword="false" />.</returns>
		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06002A9E RID: 10910 RVA: 0x00011831 File Offset: 0x0000FA31
		// (set) Token: 0x06002A9F RID: 10911 RVA: 0x000C08E2 File Offset: 0x000BEAE2
		[SRCategory("CatLayout")]
		[DefaultValue(false)]
		[RefreshProperties(RefreshProperties.All)]
		[Localizable(true)]
		[SRDescription("LabelAutoSizeDescr")]
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
				if (this.AutoSize != value)
				{
					base.AutoSize = value;
					this.AdjustSize();
				}
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Label.AutoSize" /> property changes.</summary>
		// Token: 0x140001EB RID: 491
		// (add) Token: 0x06002AA0 RID: 10912 RVA: 0x00011842 File Offset: 0x0000FA42
		// (remove) Token: 0x06002AA1 RID: 10913 RVA: 0x0001184B File Offset: 0x0000FA4B
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

		/// <summary>Gets or sets a value indicating whether the ellipsis character (...) appears at the right edge of the <see cref="T:System.Windows.Forms.Label" />, denoting that the <see cref="T:System.Windows.Forms.Label" /> text extends beyond the specified length of the <see cref="T:System.Windows.Forms.Label" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the additional label text is to be indicated by an ellipsis; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06002AA2 RID: 10914 RVA: 0x000C08FA File Offset: 0x000BEAFA
		// (set) Token: 0x06002AA3 RID: 10915 RVA: 0x000C0910 File Offset: 0x000BEB10
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[SRDescription("LabelAutoEllipsisDescr")]
		public bool AutoEllipsis
		{
			get
			{
				return this.labelState[Label.StateAutoEllipsis] != 0;
			}
			set
			{
				if (this.AutoEllipsis != value)
				{
					this.labelState[Label.StateAutoEllipsis] = (value ? 1 : 0);
					this.MeasureTextCache.InvalidateCache();
					this.OnAutoEllipsisChanged();
					if (value && this.textToolTip == null)
					{
						this.textToolTip = new ToolTip();
					}
					if (this.ParentInternal != null)
					{
						LayoutTransaction.DoLayoutIf(this.AutoSize, this.ParentInternal, this, PropertyNames.AutoEllipsis);
					}
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the image rendered on the background of the control.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> that represents the background image of the control. The default is <see langword="null" />.</returns>
		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06002AA4 RID: 10916 RVA: 0x0001187C File Offset: 0x0000FA7C
		// (set) Token: 0x06002AA5 RID: 10917 RVA: 0x00011884 File Offset: 0x0000FA84
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("LabelBackgroundImageDescr")]
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Label.BackgroundImage" /> property changes.</summary>
		// Token: 0x140001EC RID: 492
		// (add) Token: 0x06002AA6 RID: 10918 RVA: 0x0001188D File Offset: 0x0000FA8D
		// (remove) Token: 0x06002AA7 RID: 10919 RVA: 0x00011896 File Offset: 0x0000FA96
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

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageLayout" /> object.</returns>
		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06002AA8 RID: 10920 RVA: 0x0001189F File Offset: 0x0000FA9F
		// (set) Token: 0x06002AA9 RID: 10921 RVA: 0x000118A7 File Offset: 0x0000FAA7
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Label.BackgroundImageLayout" /> property changes.</summary>
		// Token: 0x140001ED RID: 493
		// (add) Token: 0x06002AAA RID: 10922 RVA: 0x000118B0 File Offset: 0x0000FAB0
		// (remove) Token: 0x06002AAB RID: 10923 RVA: 0x000118B9 File Offset: 0x0000FAB9
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

		/// <summary>Gets or sets the border style for the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. The default is <see langword="BorderStyle.None" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.BorderStyle" /> values.</exception>
		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06002AAC RID: 10924 RVA: 0x000C0989 File Offset: 0x000BEB89
		// (set) Token: 0x06002AAD RID: 10925 RVA: 0x000C099C File Offset: 0x000BEB9C
		[SRCategory("CatAppearance")]
		[DefaultValue(BorderStyle.None)]
		[DispId(-504)]
		[SRDescription("LabelBorderDescr")]
		public virtual BorderStyle BorderStyle
		{
			get
			{
				return (BorderStyle)this.labelState[Label.StateBorderStyle];
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(BorderStyle));
				}
				if (this.BorderStyle != value)
				{
					this.labelState[Label.StateBorderStyle] = (int)value;
					if (this.ParentInternal != null)
					{
						LayoutTransaction.DoLayoutIf(this.AutoSize, this.ParentInternal, this, PropertyNames.BorderStyle);
					}
					if (this.AutoSize)
					{
						this.AdjustSize();
					}
					base.RecreateHandle();
				}
			}
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06002AAE RID: 10926 RVA: 0x00012E4E File Offset: 0x0001104E
		internal virtual bool CanUseTextRenderer
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06002AAF RID: 10927 RVA: 0x000C0A1C File Offset: 0x000BEC1C
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ClassName = "STATIC";
				if (this.OwnerDraw)
				{
					createParams.Style |= 13;
					createParams.ExStyle &= -4097;
				}
				if (!this.OwnerDraw)
				{
					ContentAlignment textAlign = this.TextAlign;
					if (textAlign <= ContentAlignment.MiddleCenter)
					{
						switch (textAlign)
						{
						case ContentAlignment.TopLeft:
							break;
						case ContentAlignment.TopCenter:
							goto IL_BF;
						case (ContentAlignment)3:
							goto IL_DD;
						case ContentAlignment.TopRight:
							goto IL_AF;
						default:
							if (textAlign != ContentAlignment.MiddleLeft)
							{
								if (textAlign != ContentAlignment.MiddleCenter)
								{
									goto IL_DD;
								}
								goto IL_BF;
							}
							break;
						}
					}
					else if (textAlign <= ContentAlignment.BottomLeft)
					{
						if (textAlign == ContentAlignment.MiddleRight)
						{
							goto IL_AF;
						}
						if (textAlign != ContentAlignment.BottomLeft)
						{
							goto IL_DD;
						}
					}
					else
					{
						if (textAlign == ContentAlignment.BottomCenter)
						{
							goto IL_BF;
						}
						if (textAlign != ContentAlignment.BottomRight)
						{
							goto IL_DD;
						}
						goto IL_AF;
					}
					createParams.Style |= 0;
					goto IL_DD;
					IL_AF:
					createParams.Style |= 2;
					goto IL_DD;
					IL_BF:
					createParams.Style |= 1;
				}
				else
				{
					createParams.Style |= 0;
				}
				IL_DD:
				BorderStyle borderStyle = this.BorderStyle;
				if (borderStyle != BorderStyle.FixedSingle)
				{
					if (borderStyle == BorderStyle.Fixed3D)
					{
						createParams.Style |= 4096;
					}
				}
				else
				{
					createParams.Style |= 8388608;
				}
				if (!this.UseMnemonic)
				{
					createParams.Style |= 128;
				}
				return createParams;
			}
		}

		/// <summary>Gets the default Input Method Editor (IME) mode supported by this control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> supported by this control. The default is <see cref="F:System.Windows.Forms.ImeMode.Disable" />.</returns>
		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x06002AB0 RID: 10928 RVA: 0x00023BD7 File Offset: 0x00021DD7
		protected override ImeMode DefaultImeMode
		{
			get
			{
				return ImeMode.Disable;
			}
		}

		/// <summary>Gets the space, in pixels, that is specified by default between controls.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value that represents the default space between controls.</returns>
		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x06002AB1 RID: 10929 RVA: 0x000C0B58 File Offset: 0x000BED58
		protected override Padding DefaultMargin
		{
			get
			{
				return new Padding(3, 0, 3, 0);
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x06002AB2 RID: 10930 RVA: 0x000C0B63 File Offset: 0x000BED63
		protected override Size DefaultSize
		{
			get
			{
				return new Size(100, this.AutoSize ? this.PreferredHeight : 23);
			}
		}

		/// <summary>Gets or sets the flat style appearance of the label control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.FlatStyle" /> values. The default value is <see langword="Standard" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.FlatStyle" /> values.</exception>
		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x06002AB3 RID: 10931 RVA: 0x000C0B7E File Offset: 0x000BED7E
		// (set) Token: 0x06002AB4 RID: 10932 RVA: 0x000C0B90 File Offset: 0x000BED90
		[SRCategory("CatAppearance")]
		[DefaultValue(FlatStyle.Standard)]
		[SRDescription("ButtonFlatStyleDescr")]
		public FlatStyle FlatStyle
		{
			get
			{
				return (FlatStyle)this.labelState[Label.StateFlatStyle];
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(FlatStyle));
				}
				if (this.labelState[Label.StateFlatStyle] != (int)value)
				{
					bool flag = this.labelState[Label.StateFlatStyle] == 3 || value == FlatStyle.System;
					this.labelState[Label.StateFlatStyle] = (int)value;
					base.SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, this.OwnerDraw);
					if (flag)
					{
						LayoutTransaction.DoLayoutIf(this.AutoSize, this.ParentInternal, this, PropertyNames.BorderStyle);
						if (this.AutoSize)
						{
							this.AdjustSize();
						}
						base.RecreateHandle();
						return;
					}
					this.Refresh();
				}
			}
		}

		/// <summary>Gets or sets the image that is displayed on a <see cref="T:System.Windows.Forms.Label" />.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> displayed on the <see cref="T:System.Windows.Forms.Label" />. The default is <see langword="null" />.</returns>
		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06002AB5 RID: 10933 RVA: 0x000C0C48 File Offset: 0x000BEE48
		// (set) Token: 0x06002AB6 RID: 10934 RVA: 0x000C0CA1 File Offset: 0x000BEEA1
		[Localizable(true)]
		[SRDescription("ButtonImageDescr")]
		[SRCategory("CatAppearance")]
		public Image Image
		{
			get
			{
				Image image = (Image)base.Properties.GetObject(Label.PropImage);
				if (image == null && this.ImageList != null && this.ImageIndexer.ActualIndex >= 0)
				{
					return this.ImageList.Images[this.ImageIndexer.ActualIndex];
				}
				return image;
			}
			set
			{
				if (this.Image != value)
				{
					this.StopAnimate();
					base.Properties.SetObject(Label.PropImage, value);
					if (value != null)
					{
						this.ImageIndex = -1;
						this.ImageList = null;
					}
					this.Animate();
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the index value of the image displayed on the <see cref="T:System.Windows.Forms.Label" />.</summary>
		/// <returns>A zero-based index that represents the position in the <see cref="T:System.Windows.Forms.ImageList" /> control (assigned to the <see cref="P:System.Windows.Forms.Label.ImageList" /> property) where the image is located. The default is -1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value assigned is less than the lower bounds of the <see cref="P:System.Windows.Forms.Label.ImageIndex" /> property.</exception>
		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06002AB7 RID: 10935 RVA: 0x000C0CE0 File Offset: 0x000BEEE0
		// (set) Token: 0x06002AB8 RID: 10936 RVA: 0x000C0D34 File Offset: 0x000BEF34
		[TypeConverter(typeof(ImageIndexConverter))]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue(-1)]
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRDescription("ButtonImageIndexDescr")]
		[SRCategory("CatAppearance")]
		public int ImageIndex
		{
			get
			{
				if (this.ImageIndexer == null)
				{
					return -1;
				}
				int index = this.ImageIndexer.Index;
				if (this.ImageList != null && index >= this.ImageList.Images.Count)
				{
					return this.ImageList.Images.Count - 1;
				}
				return index;
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
				if (this.ImageIndex != value)
				{
					if (value != -1)
					{
						base.Properties.SetObject(Label.PropImage, null);
					}
					this.ImageIndexer.Index = value;
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the key accessor for the image in the <see cref="P:System.Windows.Forms.Label.ImageList" />.</summary>
		/// <returns>A string representing the key of the image.</returns>
		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06002AB9 RID: 10937 RVA: 0x000C0DB8 File Offset: 0x000BEFB8
		// (set) Token: 0x06002ABA RID: 10938 RVA: 0x000C0DCF File Offset: 0x000BEFCF
		[TypeConverter(typeof(ImageKeyConverter))]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRDescription("ButtonImageIndexDescr")]
		[SRCategory("CatAppearance")]
		public string ImageKey
		{
			get
			{
				if (this.ImageIndexer != null)
				{
					return this.ImageIndexer.Key;
				}
				return null;
			}
			set
			{
				if (this.ImageKey != value)
				{
					base.Properties.SetObject(Label.PropImage, null);
					this.ImageIndexer.Key = value;
					base.Invalidate();
				}
			}
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06002ABB RID: 10939 RVA: 0x000C0E04 File Offset: 0x000BF004
		// (set) Token: 0x06002ABC RID: 10940 RVA: 0x000C0E3E File Offset: 0x000BF03E
		internal LabelImageIndexer ImageIndexer
		{
			get
			{
				bool flag;
				LabelImageIndexer labelImageIndexer = base.Properties.GetObject(Label.PropImageIndex, out flag) as LabelImageIndexer;
				if (labelImageIndexer == null || !flag)
				{
					labelImageIndexer = new LabelImageIndexer(this);
					this.ImageIndexer = labelImageIndexer;
				}
				return labelImageIndexer;
			}
			set
			{
				base.Properties.SetObject(Label.PropImageIndex, value);
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ImageList" /> that contains the images to display in the <see cref="T:System.Windows.Forms.Label" /> control.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageList" /> that stores the collection of <see cref="T:System.Drawing.Image" /> objects. The default value is <see langword="null" />.</returns>
		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06002ABD RID: 10941 RVA: 0x000C0E51 File Offset: 0x000BF051
		// (set) Token: 0x06002ABE RID: 10942 RVA: 0x000C0E68 File Offset: 0x000BF068
		[DefaultValue(null)]
		[SRDescription("ButtonImageListDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRCategory("CatAppearance")]
		public ImageList ImageList
		{
			get
			{
				return (ImageList)base.Properties.GetObject(Label.PropImageList);
			}
			set
			{
				if (this.ImageList != value)
				{
					EventHandler eventHandler = new EventHandler(this.ImageListRecreateHandle);
					EventHandler eventHandler2 = new EventHandler(this.DetachImageList);
					ImageList imageList = this.ImageList;
					if (imageList != null)
					{
						imageList.RecreateHandle -= eventHandler;
						imageList.Disposed -= eventHandler2;
					}
					if (value != null)
					{
						base.Properties.SetObject(Label.PropImage, null);
					}
					base.Properties.SetObject(Label.PropImageList, value);
					if (value != null)
					{
						value.RecreateHandle += eventHandler;
						value.Disposed += eventHandler2;
					}
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the alignment of an image that is displayed in the control.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values. The default is <see langword="ContentAlignment.MiddleCenter" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Drawing.ContentAlignment" /> values.</exception>
		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06002ABF RID: 10943 RVA: 0x000C0EEC File Offset: 0x000BF0EC
		// (set) Token: 0x06002AC0 RID: 10944 RVA: 0x000C0F14 File Offset: 0x000BF114
		[DefaultValue(ContentAlignment.MiddleCenter)]
		[Localizable(true)]
		[SRDescription("ButtonImageAlignDescr")]
		[SRCategory("CatAppearance")]
		public ContentAlignment ImageAlign
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(Label.PropImageAlign, out flag);
				if (flag)
				{
					return (ContentAlignment)integer;
				}
				return ContentAlignment.MiddleCenter;
			}
			set
			{
				if (!WindowsFormsUtils.EnumValidator.IsValidContentAlignment(value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ContentAlignment));
				}
				if (value != this.ImageAlign)
				{
					base.Properties.SetInteger(Label.PropImageAlign, (int)value);
					LayoutTransaction.DoLayoutIf(this.AutoSize, this.ParentInternal, this, PropertyNames.ImageAlign);
					base.Invalidate();
				}
			}
		}

		/// <summary>Indicates the politeness level that a client should use to notify the user of changes to the live region.</summary>
		/// <returns>The politeness level for notifications. Its default value is <see cref="F:System.Windows.Forms.Automation.AutomationLiveSetting.Off" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.Automation.AutomationLiveSetting" /> values.</exception>
		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06002AC1 RID: 10945 RVA: 0x000C0F76 File Offset: 0x000BF176
		// (set) Token: 0x06002AC2 RID: 10946 RVA: 0x000C0F7E File Offset: 0x000BF17E
		[SRCategory("CatAccessibility")]
		[DefaultValue(AutomationLiveSetting.Off)]
		[SRDescription("LiveRegionAutomationLiveSettingDescr")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public AutomationLiveSetting LiveSetting
		{
			get
			{
				return this.liveSetting;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(AutomationLiveSetting));
				}
				this.liveSetting = value;
			}
		}

		/// <summary>Gets or sets the Input Method Editor (IME) mode supported by this control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned to this property is not within the range of valid values specified in the enumeration.</exception>
		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06002AC3 RID: 10947 RVA: 0x0001A051 File Offset: 0x00018251
		// (set) Token: 0x06002AC4 RID: 10948 RVA: 0x0001A059 File Offset: 0x00018259
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Label.ImeMode" /> property changes.</summary>
		// Token: 0x140001EE RID: 494
		// (add) Token: 0x06002AC5 RID: 10949 RVA: 0x00023F70 File Offset: 0x00022170
		// (remove) Token: 0x06002AC6 RID: 10950 RVA: 0x00023F79 File Offset: 0x00022179
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

		/// <summary>Occurs when the user releases a key while the label has focus.</summary>
		// Token: 0x140001EF RID: 495
		// (add) Token: 0x06002AC7 RID: 10951 RVA: 0x000B9104 File Offset: 0x000B7304
		// (remove) Token: 0x06002AC8 RID: 10952 RVA: 0x000B910D File Offset: 0x000B730D
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

		/// <summary>Occurs when the user presses a key while the label has focus.</summary>
		// Token: 0x140001F0 RID: 496
		// (add) Token: 0x06002AC9 RID: 10953 RVA: 0x000B9116 File Offset: 0x000B7316
		// (remove) Token: 0x06002ACA RID: 10954 RVA: 0x000B911F File Offset: 0x000B731F
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

		/// <summary>Occurs when the user presses a key while the label has focus.</summary>
		// Token: 0x140001F1 RID: 497
		// (add) Token: 0x06002ACB RID: 10955 RVA: 0x000B9128 File Offset: 0x000B7328
		// (remove) Token: 0x06002ACC RID: 10956 RVA: 0x000B9131 File Offset: 0x000B7331
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

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06002ACD RID: 10957 RVA: 0x000C0FAD File Offset: 0x000BF1AD
		internal LayoutUtils.MeasureTextCache MeasureTextCache
		{
			get
			{
				if (this.textMeasurementCache == null)
				{
					this.textMeasurementCache = new LayoutUtils.MeasureTextCache();
				}
				return this.textMeasurementCache;
			}
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06002ACE RID: 10958 RVA: 0x000C0FC8 File Offset: 0x000BF1C8
		internal virtual bool OwnerDraw
		{
			get
			{
				return this.IsOwnerDraw();
			}
		}

		/// <summary>Gets the preferred height of the control.</summary>
		/// <returns>The height of the control (in pixels), assuming a single line of text is displayed.</returns>
		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06002ACF RID: 10959 RVA: 0x000C0FD0 File Offset: 0x000BF1D0
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("LabelPreferredHeightDescr")]
		public virtual int PreferredHeight
		{
			get
			{
				return base.PreferredSize.Height;
			}
		}

		/// <summary>Gets the preferred width of the control.</summary>
		/// <returns>The width of the control (in pixels), assuming a single line of text is displayed.</returns>
		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06002AD0 RID: 10960 RVA: 0x000C0FEC File Offset: 0x000BF1EC
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("LabelPreferredWidthDescr")]
		public virtual int PreferredWidth
		{
			get
			{
				return base.PreferredSize.Width;
			}
		}

		/// <summary>Indicates whether the container control background is rendered on the <see cref="T:System.Windows.Forms.Label" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the background of the <see cref="T:System.Windows.Forms.Label" /> control's container is rendered on the <see cref="T:System.Windows.Forms.Label" />; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06002AD1 RID: 10961 RVA: 0x000C1007 File Offset: 0x000BF207
		// (set) Token: 0x06002AD2 RID: 10962 RVA: 0x000070A6 File Offset: 0x000052A6
		[Obsolete("This property has been deprecated. Use BackColor instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		protected new virtual bool RenderTransparent
		{
			get
			{
				return base.RenderTransparent;
			}
			set
			{
			}
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06002AD3 RID: 10963 RVA: 0x000C100F File Offset: 0x000BF20F
		private bool SelfSizing
		{
			get
			{
				return CommonProperties.ShouldSelfSize(this);
			}
		}

		/// <summary>Gets or sets a value indicating whether the user can tab to the <see cref="T:System.Windows.Forms.Label" />. This property is not used by this class.</summary>
		/// <returns>This property is not used by this class. The default is <see langword="false" />.</returns>
		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06002AD4 RID: 10964 RVA: 0x000B2395 File Offset: 0x000B0595
		// (set) Token: 0x06002AD5 RID: 10965 RVA: 0x000B239D File Offset: 0x000B059D
		[DefaultValue(false)]
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Label.TabStop" /> property changes.</summary>
		// Token: 0x140001F2 RID: 498
		// (add) Token: 0x06002AD6 RID: 10966 RVA: 0x000B23A6 File Offset: 0x000B05A6
		// (remove) Token: 0x06002AD7 RID: 10967 RVA: 0x000B23AF File Offset: 0x000B05AF
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

		/// <summary>Gets or sets the alignment of text in the label.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values. The default is <see cref="F:System.Drawing.ContentAlignment.TopLeft" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Drawing.ContentAlignment" /> values.</exception>
		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06002AD8 RID: 10968 RVA: 0x000C1018 File Offset: 0x000BF218
		// (set) Token: 0x06002AD9 RID: 10969 RVA: 0x000C1040 File Offset: 0x000BF240
		[SRDescription("LabelTextAlignDescr")]
		[Localizable(true)]
		[DefaultValue(ContentAlignment.TopLeft)]
		[SRCategory("CatAppearance")]
		public virtual ContentAlignment TextAlign
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(Label.PropTextAlign, out flag);
				if (flag)
				{
					return (ContentAlignment)integer;
				}
				return ContentAlignment.TopLeft;
			}
			set
			{
				if (!WindowsFormsUtils.EnumValidator.IsValidContentAlignment(value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ContentAlignment));
				}
				if (this.TextAlign != value)
				{
					base.Properties.SetInteger(Label.PropTextAlign, (int)value);
					base.Invalidate();
					if (!this.OwnerDraw)
					{
						base.RecreateHandle();
					}
					this.OnTextAlignChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the text associated with this control.</summary>
		/// <returns>The text associated with this control.</returns>
		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x06002ADA RID: 10970 RVA: 0x00013814 File Offset: 0x00011A14
		// (set) Token: 0x06002ADB RID: 10971 RVA: 0x00023FE9 File Offset: 0x000221E9
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Label.TextAlign" /> property has changed.</summary>
		// Token: 0x140001F3 RID: 499
		// (add) Token: 0x06002ADC RID: 10972 RVA: 0x000C10A4 File Offset: 0x000BF2A4
		// (remove) Token: 0x06002ADD RID: 10973 RVA: 0x000C10B7 File Offset: 0x000BF2B7
		[SRCategory("CatPropertyChanged")]
		[SRDescription("LabelOnTextAlignChangedDescr")]
		public event EventHandler TextAlignChanged
		{
			add
			{
				base.Events.AddHandler(Label.EVENT_TEXTALIGNCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(Label.EVENT_TEXTALIGNCHANGED, value);
			}
		}

		/// <summary>Gets or sets a value that determines whether to use the <see cref="T:System.Drawing.Graphics" /> class (GDI+) or the <see cref="T:System.Windows.Forms.TextRenderer" /> class (GDI) to render text.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Drawing.Graphics" /> class should be used to perform text rendering for compatibility with versions 1.0 and 1.1. of the .NET Framework; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06002ADE RID: 10974 RVA: 0x000C10CA File Offset: 0x000BF2CA
		// (set) Token: 0x06002ADF RID: 10975 RVA: 0x000C10DC File Offset: 0x000BF2DC
		[DefaultValue(false)]
		[SRCategory("CatBehavior")]
		[SRDescription("UseCompatibleTextRenderingDescr")]
		public bool UseCompatibleTextRendering
		{
			get
			{
				return !this.CanUseTextRenderer || base.UseCompatibleTextRenderingInt;
			}
			set
			{
				if (base.UseCompatibleTextRenderingInt != value)
				{
					base.UseCompatibleTextRenderingInt = value;
					this.AdjustSize();
				}
			}
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06002AE0 RID: 10976 RVA: 0x00012E4E File Offset: 0x0001104E
		internal override bool SupportsUseCompatibleTextRendering
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets or sets a value indicating whether the control interprets an ampersand character (&amp;) in the control's <see cref="P:System.Windows.Forms.Control.Text" /> property to be an access key prefix character.</summary>
		/// <returns>
		///   <see langword="true" /> if the label doesn't display the ampersand character and underlines the character after the ampersand in its displayed text and treats the underlined character as an access key; otherwise, <see langword="false" /> if the ampersand character is displayed in the text of the control. The default is <see langword="true" />.</returns>
		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06002AE1 RID: 10977 RVA: 0x000C10F4 File Offset: 0x000BF2F4
		// (set) Token: 0x06002AE2 RID: 10978 RVA: 0x000C110C File Offset: 0x000BF30C
		[SRDescription("LabelUseMnemonicDescr")]
		[DefaultValue(true)]
		[SRCategory("CatAppearance")]
		public bool UseMnemonic
		{
			get
			{
				return this.labelState[Label.StateUseMnemonic] != 0;
			}
			set
			{
				if (this.UseMnemonic != value)
				{
					this.labelState[Label.StateUseMnemonic] = (value ? 1 : 0);
					this.MeasureTextCache.InvalidateCache();
					using (LayoutTransaction.CreateTransactionIf(this.AutoSize, this.ParentInternal, this, PropertyNames.Text))
					{
						this.AdjustSize();
						base.Invalidate();
					}
					if (base.IsHandleCreated)
					{
						int num = base.WindowStyle;
						if (!this.UseMnemonic)
						{
							num |= 128;
						}
						else
						{
							num &= -129;
						}
						base.WindowStyle = num;
					}
				}
			}
		}

		// Token: 0x06002AE3 RID: 10979 RVA: 0x000C11B8 File Offset: 0x000BF3B8
		internal void AdjustSize()
		{
			if (!this.SelfSizing)
			{
				return;
			}
			if (!this.AutoSize && ((this.Anchor & (AnchorStyles.Left | AnchorStyles.Right)) == (AnchorStyles.Left | AnchorStyles.Right) || (this.Anchor & (AnchorStyles.Top | AnchorStyles.Bottom)) == (AnchorStyles.Top | AnchorStyles.Bottom)))
			{
				return;
			}
			int num = this.requestedHeight;
			int num2 = this.requestedWidth;
			try
			{
				Size size = (this.AutoSize ? base.PreferredSize : new Size(num2, num));
				base.Size = size;
			}
			finally
			{
				this.requestedHeight = num;
				this.requestedWidth = num2;
			}
		}

		// Token: 0x06002AE4 RID: 10980 RVA: 0x000C1240 File Offset: 0x000BF440
		internal void Animate()
		{
			this.Animate(!base.DesignMode && base.Visible && base.Enabled && this.ParentInternal != null);
		}

		// Token: 0x06002AE5 RID: 10981 RVA: 0x000C126C File Offset: 0x000BF46C
		internal void StopAnimate()
		{
			this.Animate(false);
		}

		// Token: 0x06002AE6 RID: 10982 RVA: 0x000C1278 File Offset: 0x000BF478
		private void Animate(bool animate)
		{
			bool flag = this.labelState[Label.StateAnimating] != 0;
			if (animate != flag)
			{
				Image image = (Image)base.Properties.GetObject(Label.PropImage);
				if (animate)
				{
					if (image != null)
					{
						ImageAnimator.Animate(image, new EventHandler(this.OnFrameChanged));
						this.labelState[Label.StateAnimating] = (animate ? 1 : 0);
						return;
					}
				}
				else if (image != null)
				{
					ImageAnimator.StopAnimate(image, new EventHandler(this.OnFrameChanged));
					this.labelState[Label.StateAnimating] = (animate ? 1 : 0);
				}
			}
		}

		/// <summary>Determines the size and location of an image drawn within the <see cref="T:System.Windows.Forms.Label" /> control based on the alignment of the control.</summary>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> used to determine size and location when drawn within the control.</param>
		/// <param name="r">A <see cref="T:System.Drawing.Rectangle" /> that represents the area to draw the image in.</param>
		/// <param name="align">The alignment of content within the control.</param>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the size and location of the specified image within the control.</returns>
		// Token: 0x06002AE7 RID: 10983 RVA: 0x000C1310 File Offset: 0x000BF510
		protected Rectangle CalcImageRenderBounds(Image image, Rectangle r, ContentAlignment align)
		{
			Size size = image.Size;
			int num = r.X + 2;
			int num2 = r.Y + 2;
			if ((align & WindowsFormsUtils.AnyRightAlign) != (ContentAlignment)0)
			{
				num = r.X + r.Width - 4 - size.Width;
			}
			else if ((align & WindowsFormsUtils.AnyCenterAlign) != (ContentAlignment)0)
			{
				num = r.X + (r.Width - size.Width) / 2;
			}
			if ((align & WindowsFormsUtils.AnyBottomAlign) != (ContentAlignment)0)
			{
				num2 = r.Y + r.Height - 4 - size.Height;
			}
			else if ((align & WindowsFormsUtils.AnyTopAlign) != (ContentAlignment)0)
			{
				num2 = r.Y + 2;
			}
			else
			{
				num2 = r.Y + (r.Height - size.Height) / 2;
			}
			return new Rectangle(num, num2, size.Width, size.Height);
		}

		/// <summary>Creates a new accessibility object for the control.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the control.</returns>
		// Token: 0x06002AE8 RID: 10984 RVA: 0x000C13E9 File Offset: 0x000BF5E9
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new Label.LabelAccessibleObject(this);
		}

		// Token: 0x06002AE9 RID: 10985 RVA: 0x000C13F1 File Offset: 0x000BF5F1
		internal virtual StringFormat CreateStringFormat()
		{
			return ControlPaint.CreateStringFormat(this, this.TextAlign, this.AutoEllipsis, this.UseMnemonic);
		}

		// Token: 0x06002AEA RID: 10986 RVA: 0x000C140B File Offset: 0x000BF60B
		private TextFormatFlags CreateTextFormatFlags()
		{
			return this.CreateTextFormatFlags(base.Size - this.GetBordersAndPadding());
		}

		// Token: 0x06002AEB RID: 10987 RVA: 0x000C1424 File Offset: 0x000BF624
		internal virtual TextFormatFlags CreateTextFormatFlags(Size constrainingSize)
		{
			TextFormatFlags textFormatFlags = ControlPaint.CreateTextFormatFlags(this, this.TextAlign, this.AutoEllipsis, this.UseMnemonic);
			if (!this.MeasureTextCache.TextRequiresWordBreak(this.Text, this.Font, constrainingSize, textFormatFlags))
			{
				textFormatFlags &= ~(TextFormatFlags.TextBoxControl | TextFormatFlags.WordBreak);
			}
			return textFormatFlags;
		}

		// Token: 0x06002AEC RID: 10988 RVA: 0x000C146E File Offset: 0x000BF66E
		private void DetachImageList(object sender, EventArgs e)
		{
			this.ImageList = null;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Label" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06002AED RID: 10989 RVA: 0x000C1478 File Offset: 0x000BF678
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.StopAnimate();
				if (this.ImageList != null)
				{
					this.ImageList.Disposed -= this.DetachImageList;
					this.ImageList.RecreateHandle -= this.ImageListRecreateHandle;
					base.Properties.SetObject(Label.PropImageList, null);
				}
				if (this.Image != null)
				{
					base.Properties.SetObject(Label.PropImage, null);
				}
				if (this.textToolTip != null)
				{
					this.textToolTip.Dispose();
					this.textToolTip = null;
				}
				this.controlToolTip = false;
			}
			base.Dispose(disposing);
		}

		/// <summary>Draws an <see cref="T:System.Drawing.Image" /> within the specified bounds.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> surface on which to draw.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <param name="r">The <see cref="T:System.Drawing.Rectangle" /> bounds to draw within.</param>
		/// <param name="align">The alignment of the image to draw within the <see cref="T:System.Windows.Forms.Label" />.</param>
		// Token: 0x06002AEE RID: 10990 RVA: 0x000C151C File Offset: 0x000BF71C
		protected void DrawImage(Graphics g, Image image, Rectangle r, ContentAlignment align)
		{
			Rectangle rectangle = this.CalcImageRenderBounds(image, r, align);
			if (!base.Enabled)
			{
				ControlPaint.DrawImageDisabled(g, image, rectangle.X, rectangle.Y, this.BackColor);
				return;
			}
			g.DrawImage(image, rectangle.X, rectangle.Y, image.Width, image.Height);
		}

		// Token: 0x06002AEF RID: 10991 RVA: 0x000C157C File Offset: 0x000BF77C
		private Size GetBordersAndPadding()
		{
			Size size = base.Padding.Size;
			if (this.UseCompatibleTextRendering)
			{
				if (this.BorderStyle != BorderStyle.None)
				{
					size.Height += 6;
					size.Width += 2;
				}
				else
				{
					size.Height += 3;
				}
			}
			else
			{
				size += this.SizeFromClientSize(Size.Empty);
				if (this.BorderStyle == BorderStyle.Fixed3D)
				{
					size += new Size(2, 2);
				}
			}
			return size;
		}

		/// <summary>Retrieves the size of a rectangular area into which a control can be fitted.</summary>
		/// <param name="proposedSize">The custom-sized area for a control.</param>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		// Token: 0x06002AF0 RID: 10992 RVA: 0x00024412 File Offset: 0x00022612
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

		// Token: 0x06002AF1 RID: 10993 RVA: 0x000C1603 File Offset: 0x000BF803
		internal virtual bool UseGDIMeasuring()
		{
			return this.FlatStyle == FlatStyle.System || !this.UseCompatibleTextRendering;
		}

		// Token: 0x06002AF2 RID: 10994 RVA: 0x000C161C File Offset: 0x000BF81C
		internal override Size GetPreferredSizeCore(Size proposedConstraints)
		{
			Size bordersAndPadding = this.GetBordersAndPadding();
			proposedConstraints -= bordersAndPadding;
			proposedConstraints = LayoutUtils.UnionSizes(proposedConstraints, Size.Empty);
			Size size;
			if (string.IsNullOrEmpty(this.Text))
			{
				using (WindowsFont windowsFont = WindowsFont.FromFont(this.Font))
				{
					size = WindowsGraphicsCacheManager.MeasurementGraphics.GetTextExtent("0", windowsFont);
					size.Width = 0;
					goto IL_111;
				}
			}
			if (this.UseGDIMeasuring())
			{
				TextFormatFlags textFormatFlags = ((this.FlatStyle == FlatStyle.System) ? TextFormatFlags.Default : this.CreateTextFormatFlags(proposedConstraints));
				size = this.MeasureTextCache.GetTextSize(this.Text, this.Font, proposedConstraints, textFormatFlags);
			}
			else
			{
				using (Graphics graphics = WindowsFormsUtils.CreateMeasurementGraphics())
				{
					using (StringFormat stringFormat = this.CreateStringFormat())
					{
						SizeF sizeF = ((proposedConstraints.Width == 1) ? new SizeF(0f, (float)proposedConstraints.Height) : new SizeF((float)proposedConstraints.Width, (float)proposedConstraints.Height));
						size = Size.Ceiling(graphics.MeasureString(this.Text, this.Font, sizeF, stringFormat));
					}
				}
			}
			IL_111:
			size += bordersAndPadding;
			return size;
		}

		// Token: 0x06002AF3 RID: 10995 RVA: 0x000C176C File Offset: 0x000BF96C
		private int GetLeadingTextPaddingFromTextFormatFlags()
		{
			if (!base.IsHandleCreated)
			{
				return 0;
			}
			if (this.UseCompatibleTextRendering && this.FlatStyle != FlatStyle.System)
			{
				return 0;
			}
			int iLeftMargin;
			using (WindowsGraphics windowsGraphics = WindowsGraphics.FromHwnd(base.Handle))
			{
				TextFormatFlags textFormatFlags = this.CreateTextFormatFlags();
				if ((textFormatFlags & TextFormatFlags.NoPadding) == TextFormatFlags.NoPadding)
				{
					windowsGraphics.TextPadding = TextPaddingOptions.NoPadding;
				}
				else if ((textFormatFlags & TextFormatFlags.LeftAndRightPadding) == TextFormatFlags.LeftAndRightPadding)
				{
					windowsGraphics.TextPadding = TextPaddingOptions.LeftAndRightPadding;
				}
				using (WindowsFont windowsFont = WindowsGraphicsCacheManager.GetWindowsFont(this.Font))
				{
					IntNativeMethods.DRAWTEXTPARAMS textMargins = windowsGraphics.GetTextMargins(windowsFont);
					iLeftMargin = textMargins.iLeftMargin;
				}
			}
			return iLeftMargin;
		}

		// Token: 0x06002AF4 RID: 10996 RVA: 0x00024217 File Offset: 0x00022417
		private void ImageListRecreateHandle(object sender, EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				base.Invalidate();
			}
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06002AF5 RID: 10997 RVA: 0x00012E4E File Offset: 0x0001104E
		internal override bool IsMnemonicsListenerAxSourced
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002AF6 RID: 10998 RVA: 0x000C1828 File Offset: 0x000BFA28
		internal bool IsOwnerDraw()
		{
			return this.FlatStyle != FlatStyle.System;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseEnter" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002AF7 RID: 10999 RVA: 0x000C1838 File Offset: 0x000BFA38
		protected override void OnMouseEnter(EventArgs e)
		{
			if (!this.controlToolTip && !base.DesignMode && this.AutoEllipsis && this.showToolTip && this.textToolTip != null)
			{
				IntSecurity.AllWindows.Assert();
				try
				{
					this.controlToolTip = true;
					this.textToolTip.Show(WindowsFormsUtils.TextWithoutMnemonics(this.Text), this);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
					this.controlToolTip = false;
				}
			}
			base.OnMouseEnter(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002AF8 RID: 11000 RVA: 0x000C18BC File Offset: 0x000BFABC
		protected override void OnMouseLeave(EventArgs e)
		{
			if (!this.controlToolTip && this.textToolTip != null && this.textToolTip.GetHandleCreated())
			{
				this.textToolTip.RemoveAll();
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
			base.OnMouseLeave(e);
		}

		// Token: 0x06002AF9 RID: 11001 RVA: 0x000C1928 File Offset: 0x000BFB28
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

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002AFA RID: 11002 RVA: 0x000C197E File Offset: 0x000BFB7E
		protected override void OnFontChanged(EventArgs e)
		{
			this.MeasureTextCache.InvalidateCache();
			base.OnFontChanged(e);
			this.AdjustSize();
			base.Invalidate();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002AFB RID: 11003 RVA: 0x000C199E File Offset: 0x000BFB9E
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
			if (this.textToolTip != null && this.textToolTip.GetHandleCreated())
			{
				this.textToolTip.DestroyHandle();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.TextChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002AFC RID: 11004 RVA: 0x000C19C8 File Offset: 0x000BFBC8
		protected override void OnTextChanged(EventArgs e)
		{
			using (LayoutTransaction.CreateTransactionIf(this.AutoSize, this.ParentInternal, this, PropertyNames.Text))
			{
				this.MeasureTextCache.InvalidateCache();
				base.OnTextChanged(e);
				this.AdjustSize();
				base.Invalidate();
			}
			if (AccessibilityImprovements.Level3 && this.LiveSetting != AutomationLiveSetting.Off)
			{
				base.AccessibilityObject.RaiseLiveRegionChanged();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Label.TextAlignChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002AFD RID: 11005 RVA: 0x000C1A44 File Offset: 0x000BFC44
		protected virtual void OnTextAlignChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[Label.EVENT_TEXTALIGNCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.PaddingChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002AFE RID: 11006 RVA: 0x000C1A72 File Offset: 0x000BFC72
		protected override void OnPaddingChanged(EventArgs e)
		{
			base.OnPaddingChanged(e);
			this.AdjustSize();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		// Token: 0x06002AFF RID: 11007 RVA: 0x000C1A84 File Offset: 0x000BFC84
		protected override void OnPaint(PaintEventArgs e)
		{
			this.Animate();
			ImageAnimator.UpdateFrames(this.Image);
			Rectangle rectangle = LayoutUtils.DeflateRect(base.ClientRectangle, base.Padding);
			Image image = this.Image;
			if (image != null)
			{
				this.DrawImage(e.Graphics, image, rectangle, base.RtlTranslateAlignment(this.ImageAlign));
			}
			IntPtr hdc = e.Graphics.GetHdc();
			Color nearestColor;
			try
			{
				using (WindowsGraphics windowsGraphics = WindowsGraphics.FromHdc(hdc))
				{
					nearestColor = windowsGraphics.GetNearestColor(base.Enabled ? this.ForeColor : base.DisabledColor);
				}
			}
			finally
			{
				e.Graphics.ReleaseHdc();
			}
			if (this.AutoEllipsis)
			{
				Rectangle clientRectangle = base.ClientRectangle;
				Size preferredSize = this.GetPreferredSize(new Size(clientRectangle.Width, clientRectangle.Height));
				this.showToolTip = clientRectangle.Width < preferredSize.Width || clientRectangle.Height < preferredSize.Height;
			}
			else
			{
				this.showToolTip = false;
			}
			if (this.UseCompatibleTextRendering)
			{
				using (StringFormat stringFormat = this.CreateStringFormat())
				{
					if (base.Enabled)
					{
						using (Brush brush = new SolidBrush(nearestColor))
						{
							e.Graphics.DrawString(this.Text, this.Font, brush, rectangle, stringFormat);
							goto IL_1C6;
						}
					}
					ControlPaint.DrawStringDisabled(e.Graphics, this.Text, this.Font, nearestColor, rectangle, stringFormat);
					goto IL_1C6;
				}
			}
			TextFormatFlags textFormatFlags = this.CreateTextFormatFlags();
			if (base.Enabled)
			{
				TextRenderer.DrawText(e.Graphics, this.Text, this.Font, rectangle, nearestColor, textFormatFlags);
			}
			else
			{
				Color color = TextRenderer.DisabledTextColor(this.BackColor);
				TextRenderer.DrawText(e.Graphics, this.Text, this.Font, rectangle, color, textFormatFlags);
			}
			IL_1C6:
			base.OnPaint(e);
		}

		// Token: 0x06002B00 RID: 11008 RVA: 0x000070A6 File Offset: 0x000052A6
		internal virtual void OnAutoEllipsisChanged()
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.EnabledChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002B01 RID: 11009 RVA: 0x000C1C94 File Offset: 0x000BFE94
		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
			this.Animate();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ParentChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002B02 RID: 11010 RVA: 0x000C1CA3 File Offset: 0x000BFEA3
		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
			if (this.SelfSizing)
			{
				this.AdjustSize();
			}
			this.Animate();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.RightToLeftChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002B03 RID: 11011 RVA: 0x000C1CC0 File Offset: 0x000BFEC0
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			this.MeasureTextCache.InvalidateCache();
			base.OnRightToLeftChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.VisibleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002B04 RID: 11012 RVA: 0x000C1CD4 File Offset: 0x000BFED4
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			this.Animate();
		}

		// Token: 0x06002B05 RID: 11013 RVA: 0x000C1CE4 File Offset: 0x000BFEE4
		internal override void PrintToMetaFileRecursive(HandleRef hDC, IntPtr lParam, Rectangle bounds)
		{
			base.PrintToMetaFileRecursive(hDC, lParam, bounds);
			using (new WindowsFormsUtils.DCMapping(hDC, bounds))
			{
				using (Graphics graphics = Graphics.FromHdcInternal(hDC.Handle))
				{
					ControlPaint.PrintBorder(graphics, new Rectangle(Point.Empty, base.Size), this.BorderStyle, Border3DStyle.SunkenOuter);
				}
			}
		}

		/// <summary>Processes a mnemonic character.</summary>
		/// <param name="charCode">The character to process.</param>
		/// <returns>
		///   <see langword="true" /> if the character was processed as a mnemonic by the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002B06 RID: 11014 RVA: 0x000C1D64 File Offset: 0x000BFF64
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected internal override bool ProcessMnemonic(char charCode)
		{
			if (this.UseMnemonic && Control.IsMnemonic(charCode, this.Text) && this.CanProcessMnemonic())
			{
				Control parentInternal = this.ParentInternal;
				if (parentInternal != null)
				{
					IntSecurity.ModifyFocus.Assert();
					try
					{
						if (parentInternal.SelectNextControl(this, true, false, true, false) && !parentInternal.ContainsFocus)
						{
							parentInternal.Focus();
						}
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
				return true;
			}
			return false;
		}

		/// <summary>Sets the specified bounds of the label.</summary>
		/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control.</param>
		/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control.</param>
		/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control.</param>
		/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control.</param>
		/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values. For any parameter not specified, the current value will be used.</param>
		// Token: 0x06002B07 RID: 11015 RVA: 0x000C1DDC File Offset: 0x000BFFDC
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if ((specified & BoundsSpecified.Height) != BoundsSpecified.None)
			{
				this.requestedHeight = height;
			}
			if ((specified & BoundsSpecified.Width) != BoundsSpecified.None)
			{
				this.requestedWidth = width;
			}
			if (this.AutoSize && this.SelfSizing)
			{
				Size preferredSize = base.PreferredSize;
				width = preferredSize.Width;
				height = preferredSize.Height;
			}
			base.SetBoundsCore(x, y, width, height, specified);
		}

		// Token: 0x06002B08 RID: 11016 RVA: 0x000C1E3A File Offset: 0x000C003A
		private void ResetImage()
		{
			this.Image = null;
		}

		// Token: 0x06002B09 RID: 11017 RVA: 0x000C1E43 File Offset: 0x000C0043
		private bool ShouldSerializeImage()
		{
			return base.Properties.GetObject(Label.PropImage) != null;
		}

		// Token: 0x06002B0A RID: 11018 RVA: 0x000C1E58 File Offset: 0x000C0058
		internal void SetToolTip(ToolTip toolTip)
		{
			if (toolTip != null && !this.controlToolTip)
			{
				this.controlToolTip = true;
			}
		}

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06002B0B RID: 11019 RVA: 0x00028BBB File Offset: 0x00026DBB
		internal override bool SupportsUiaProviders
		{
			get
			{
				return AccessibilityImprovements.Level3 && !base.DesignMode;
			}
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Windows.Forms.Label" />.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.Label" />.</returns>
		// Token: 0x06002B0C RID: 11020 RVA: 0x000C1E6C File Offset: 0x000C006C
		public override string ToString()
		{
			string text = base.ToString();
			return text + ", Text: " + this.Text;
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x06002B0D RID: 11021 RVA: 0x000C1E94 File Offset: 0x000C0094
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg == 132)
			{
				Rectangle rectangle = base.RectangleToScreen(new Rectangle(0, 0, base.Width, base.Height));
				Point point = new Point((int)(long)m.LParam);
				m.Result = (IntPtr)(rectangle.Contains(point) ? 1 : 0);
				return;
			}
			base.WndProc(ref m);
		}

		/// <summary>Provides constants for rescaling the control when a DPI change occurs.</summary>
		/// <param name="deviceDpiOld">The DPI value prior to the change.</param>
		/// <param name="deviceDpiNew">The DPI value after the change.</param>
		// Token: 0x06002B0E RID: 11022 RVA: 0x000C1EFE File Offset: 0x000C00FE
		protected override void RescaleConstantsForDpi(int deviceDpiOld, int deviceDpiNew)
		{
			base.RescaleConstantsForDpi(deviceDpiOld, deviceDpiNew);
			if (!DpiHelper.EnableDpiChangedHighDpiImprovements)
			{
				return;
			}
			this.MeasureTextCache.InvalidateCache();
		}

		// Token: 0x04001203 RID: 4611
		private static readonly object EVENT_TEXTALIGNCHANGED = new object();

		// Token: 0x04001204 RID: 4612
		private static readonly BitVector32.Section StateUseMnemonic = BitVector32.CreateSection(1);

		// Token: 0x04001205 RID: 4613
		private static readonly BitVector32.Section StateAutoSize = BitVector32.CreateSection(1, Label.StateUseMnemonic);

		// Token: 0x04001206 RID: 4614
		private static readonly BitVector32.Section StateAnimating = BitVector32.CreateSection(1, Label.StateAutoSize);

		// Token: 0x04001207 RID: 4615
		private static readonly BitVector32.Section StateFlatStyle = BitVector32.CreateSection(3, Label.StateAnimating);

		// Token: 0x04001208 RID: 4616
		private static readonly BitVector32.Section StateBorderStyle = BitVector32.CreateSection(2, Label.StateFlatStyle);

		// Token: 0x04001209 RID: 4617
		private static readonly BitVector32.Section StateAutoEllipsis = BitVector32.CreateSection(1, Label.StateBorderStyle);

		// Token: 0x0400120A RID: 4618
		private static readonly int PropImageList = PropertyStore.CreateKey();

		// Token: 0x0400120B RID: 4619
		private static readonly int PropImage = PropertyStore.CreateKey();

		// Token: 0x0400120C RID: 4620
		private static readonly int PropTextAlign = PropertyStore.CreateKey();

		// Token: 0x0400120D RID: 4621
		private static readonly int PropImageAlign = PropertyStore.CreateKey();

		// Token: 0x0400120E RID: 4622
		private static readonly int PropImageIndex = PropertyStore.CreateKey();

		// Token: 0x0400120F RID: 4623
		private BitVector32 labelState;

		// Token: 0x04001210 RID: 4624
		private int requestedHeight;

		// Token: 0x04001211 RID: 4625
		private int requestedWidth;

		// Token: 0x04001212 RID: 4626
		private LayoutUtils.MeasureTextCache textMeasurementCache;

		// Token: 0x04001213 RID: 4627
		internal bool showToolTip;

		// Token: 0x04001214 RID: 4628
		private ToolTip textToolTip;

		// Token: 0x04001215 RID: 4629
		private bool controlToolTip;

		// Token: 0x04001216 RID: 4630
		private AutomationLiveSetting liveSetting;

		// Token: 0x020006B7 RID: 1719
		[ComVisible(true)]
		internal class LabelAccessibleObject : Control.ControlAccessibleObject
		{
			// Token: 0x060068B5 RID: 26805 RVA: 0x0009B733 File Offset: 0x00099933
			public LabelAccessibleObject(Label owner)
				: base(owner)
			{
			}

			// Token: 0x170016A2 RID: 5794
			// (get) Token: 0x060068B6 RID: 26806 RVA: 0x001850B8 File Offset: 0x001832B8
			public override AccessibleRole Role
			{
				get
				{
					AccessibleRole accessibleRole = base.Owner.AccessibleRole;
					if (accessibleRole != AccessibleRole.Default)
					{
						return accessibleRole;
					}
					return AccessibleRole.StaticText;
				}
			}

			// Token: 0x060068B7 RID: 26807 RVA: 0x0009B73C File Offset: 0x0009993C
			internal override bool IsIAccessibleExSupported()
			{
				return AccessibilityImprovements.Level3 || base.IsIAccessibleExSupported();
			}

			// Token: 0x060068B8 RID: 26808 RVA: 0x00183041 File Offset: 0x00181241
			internal override bool IsPatternSupported(int patternId)
			{
				return (AccessibilityImprovements.Level3 && patternId == 10018) || base.IsPatternSupported(patternId);
			}

			// Token: 0x060068B9 RID: 26809 RVA: 0x001850D9 File Offset: 0x001832D9
			internal override object GetPropertyValue(int propertyID)
			{
				if (AccessibilityImprovements.Level3 && propertyID == 30005)
				{
					return this.Name;
				}
				if (propertyID == 30003)
				{
					return 50020;
				}
				return base.GetPropertyValue(propertyID);
			}
		}
	}
}
