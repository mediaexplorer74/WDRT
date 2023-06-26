using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Manages a related set of tab pages.</summary>
	// Token: 0x02000385 RID: 901
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultProperty("TabPages")]
	[DefaultEvent("SelectedIndexChanged")]
	[Designer("System.Windows.Forms.Design.TabControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionTabControl")]
	public class TabControl : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TabControl" /> class.</summary>
		// Token: 0x06003B0E RID: 15118 RVA: 0x001033B0 File Offset: 0x001015B0
		public TabControl()
		{
			this.tabControlState = new BitVector32(0);
			this.tabCollection = new TabControl.TabPageCollection(this);
			base.SetStyle(ControlStyles.UserPaint, false);
		}

		/// <summary>Gets or sets the area of the control (for example, along the top) where the tabs are aligned.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.TabAlignment" /> values. The default is <see langword="Top" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The property value is not a valid <see cref="T:System.Windows.Forms.TabAlignment" /> value.</exception>
		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x06003B0F RID: 15119 RVA: 0x0010343B File Offset: 0x0010163B
		// (set) Token: 0x06003B10 RID: 15120 RVA: 0x00103444 File Offset: 0x00101644
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[DefaultValue(TabAlignment.Top)]
		[RefreshProperties(RefreshProperties.All)]
		[SRDescription("TabBaseAlignmentDescr")]
		public TabAlignment Alignment
		{
			get
			{
				return this.alignment;
			}
			set
			{
				if (this.alignment != value)
				{
					if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
					{
						throw new InvalidEnumArgumentException("value", (int)value, typeof(TabAlignment));
					}
					this.alignment = value;
					if (this.alignment == TabAlignment.Left || this.alignment == TabAlignment.Right)
					{
						this.Multiline = true;
					}
					base.RecreateHandle();
				}
			}
		}

		/// <summary>Gets or sets the visual appearance of the control's tabs.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.TabAppearance" /> values. The default is <see langword="Normal" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The property value is not a valid <see cref="T:System.Windows.Forms.TabAppearance" /> value.</exception>
		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x06003B11 RID: 15121 RVA: 0x001034A6 File Offset: 0x001016A6
		// (set) Token: 0x06003B12 RID: 15122 RVA: 0x001034C4 File Offset: 0x001016C4
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[DefaultValue(TabAppearance.Normal)]
		[SRDescription("TabBaseAppearanceDescr")]
		public TabAppearance Appearance
		{
			get
			{
				if (this.appearance == TabAppearance.FlatButtons && this.alignment != TabAlignment.Top)
				{
					return TabAppearance.Buttons;
				}
				return this.appearance;
			}
			set
			{
				if (this.appearance != value)
				{
					if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
					{
						throw new InvalidEnumArgumentException("value", (int)value, typeof(TabAppearance));
					}
					this.appearance = value;
					base.RecreateHandle();
					this.OnStyleChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>The background color for the control.</returns>
		// Token: 0x17000E68 RID: 3688
		// (get) Token: 0x06003B13 RID: 15123 RVA: 0x00030577 File Offset: 0x0002E777
		// (set) Token: 0x06003B14 RID: 15124 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color BackColor
		{
			get
			{
				return SystemColors.Control;
			}
			set
			{
			}
		}

		/// <summary>This event is not meaningful for this control.</summary>
		// Token: 0x140002D1 RID: 721
		// (add) Token: 0x06003B15 RID: 15125 RVA: 0x00058BF2 File Offset: 0x00056DF2
		// (remove) Token: 0x06003B16 RID: 15126 RVA: 0x00058BFB File Offset: 0x00056DFB
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

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>The background image displayed in the control.</returns>
		// Token: 0x17000E69 RID: 3689
		// (get) Token: 0x06003B17 RID: 15127 RVA: 0x0001187C File Offset: 0x0000FA7C
		// (set) Token: 0x06003B18 RID: 15128 RVA: 0x00011884 File Offset: 0x0000FA84
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TabControl.BackgroundImage" /> property changes.</summary>
		// Token: 0x140002D2 RID: 722
		// (add) Token: 0x06003B19 RID: 15129 RVA: 0x0001188D File Offset: 0x0000FA8D
		// (remove) Token: 0x06003B1A RID: 15130 RVA: 0x00011896 File Offset: 0x0000FA96
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

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>One of the values of <see cref="T:System.Windows.Forms.ImageLayout" />. The default value is Tile.</returns>
		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x06003B1B RID: 15131 RVA: 0x0001189F File Offset: 0x0000FA9F
		// (set) Token: 0x06003B1C RID: 15132 RVA: 0x000118A7 File Offset: 0x0000FAA7
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TabControl.BackgroundImageLayout" /> property changes.</summary>
		// Token: 0x140002D3 RID: 723
		// (add) Token: 0x06003B1D RID: 15133 RVA: 0x000118B0 File Offset: 0x0000FAB0
		// (remove) Token: 0x06003B1E RID: 15134 RVA: 0x000118B9 File Offset: 0x0000FAB9
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

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x17000E6B RID: 3691
		// (get) Token: 0x06003B1F RID: 15135 RVA: 0x000B8F3C File Offset: 0x000B713C
		protected override Size DefaultSize
		{
			get
			{
				return new Size(200, 100);
			}
		}

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value.</returns>
		// Token: 0x17000E6C RID: 3692
		// (get) Token: 0x06003B20 RID: 15136 RVA: 0x00012FC3 File Offset: 0x000111C3
		// (set) Token: 0x06003B21 RID: 15137 RVA: 0x00012FCB File Offset: 0x000111CB
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

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>The foreground color of the control.</returns>
		// Token: 0x17000E6D RID: 3693
		// (get) Token: 0x06003B22 RID: 15138 RVA: 0x0001A0E7 File Offset: 0x000182E7
		// (set) Token: 0x06003B23 RID: 15139 RVA: 0x00013024 File Offset: 0x00011224
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TabControl.ForeColor" /> property changes.</summary>
		// Token: 0x140002D4 RID: 724
		// (add) Token: 0x06003B24 RID: 15140 RVA: 0x0005A8EE File Offset: 0x00058AEE
		// (remove) Token: 0x06003B25 RID: 15141 RVA: 0x0005A8F7 File Offset: 0x00058AF7
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

		/// <summary>This member overrides <see cref="P:System.Windows.Forms.Control.CreateParams" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x06003B26 RID: 15142 RVA: 0x00103518 File Offset: 0x00101718
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ClassName = "SysTabControl32";
				if (this.Multiline)
				{
					createParams.Style |= 512;
				}
				if (this.drawMode == TabDrawMode.OwnerDrawFixed)
				{
					createParams.Style |= 8192;
				}
				if (this.ShowToolTips && !base.DesignMode)
				{
					createParams.Style |= 16384;
				}
				if (this.alignment == TabAlignment.Bottom || this.alignment == TabAlignment.Right)
				{
					createParams.Style |= 2;
				}
				if (this.alignment == TabAlignment.Left || this.alignment == TabAlignment.Right)
				{
					createParams.Style |= 640;
				}
				if (this.tabControlState[1])
				{
					createParams.Style |= 64;
				}
				if (this.appearance == TabAppearance.Normal)
				{
					createParams.Style |= 0;
				}
				else
				{
					createParams.Style |= 256;
					if (this.appearance == TabAppearance.FlatButtons && this.alignment == TabAlignment.Top)
					{
						createParams.Style |= 8;
					}
				}
				switch (this.sizeMode)
				{
				case TabSizeMode.Normal:
					createParams.Style |= 2048;
					break;
				case TabSizeMode.FillToRight:
					createParams.Style |= 0;
					break;
				case TabSizeMode.Fixed:
					createParams.Style |= 1024;
					break;
				}
				if (this.RightToLeft == RightToLeft.Yes && this.RightToLeftLayout)
				{
					createParams.ExStyle |= 5242880;
					createParams.ExStyle &= -28673;
				}
				return createParams;
			}
		}

		/// <summary>Gets the display area of the control's tab pages.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the display area of the tab pages.</returns>
		// Token: 0x17000E6F RID: 3695
		// (get) Token: 0x06003B27 RID: 15143 RVA: 0x001036C0 File Offset: 0x001018C0
		public override Rectangle DisplayRectangle
		{
			get
			{
				if (!this.cachedDisplayRect.IsEmpty)
				{
					return this.cachedDisplayRect;
				}
				Rectangle bounds = base.Bounds;
				NativeMethods.RECT rect = NativeMethods.RECT.FromXYWH(bounds.X, bounds.Y, bounds.Width, bounds.Height);
				if (!base.IsDisposed)
				{
					if (!base.IsActiveX && !base.IsHandleCreated)
					{
						this.CreateHandle();
					}
					if (base.IsHandleCreated)
					{
						base.SendMessage(4904, 0, ref rect);
					}
				}
				Rectangle rectangle = Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
				Point location = base.Location;
				rectangle.X -= location.X;
				rectangle.Y -= location.Y;
				this.cachedDisplayRect = rectangle;
				return rectangle;
			}
		}

		/// <summary>Gets or sets the way that the control's tabs are drawn.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.TabDrawMode" /> values. The default is <see langword="Normal" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The property value is not a valid <see cref="T:System.Windows.Forms.TabDrawMode" /> value.</exception>
		// Token: 0x17000E70 RID: 3696
		// (get) Token: 0x06003B28 RID: 15144 RVA: 0x00103796 File Offset: 0x00101996
		// (set) Token: 0x06003B29 RID: 15145 RVA: 0x0010379E File Offset: 0x0010199E
		[SRCategory("CatBehavior")]
		[DefaultValue(TabDrawMode.Normal)]
		[SRDescription("TabBaseDrawModeDescr")]
		public TabDrawMode DrawMode
		{
			get
			{
				return this.drawMode;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 1))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(TabDrawMode));
				}
				if (this.drawMode != value)
				{
					this.drawMode = value;
					base.RecreateHandle();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the control's tabs change in appearance when the mouse passes over them.</summary>
		/// <returns>
		///   <see langword="true" /> if the tabs change in appearance when the mouse passes over them; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000E71 RID: 3697
		// (get) Token: 0x06003B2A RID: 15146 RVA: 0x001037DC File Offset: 0x001019DC
		// (set) Token: 0x06003B2B RID: 15147 RVA: 0x001037EA File Offset: 0x001019EA
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("TabBaseHotTrackDescr")]
		public bool HotTrack
		{
			get
			{
				return this.tabControlState[1];
			}
			set
			{
				if (this.HotTrack != value)
				{
					this.tabControlState[1] = value;
					if (base.IsHandleCreated)
					{
						base.RecreateHandle();
					}
				}
			}
		}

		/// <summary>Gets or sets the images to display on the control's tabs.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageList" /> that specifies the images to display on the tabs.</returns>
		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x06003B2C RID: 15148 RVA: 0x00103810 File Offset: 0x00101A10
		// (set) Token: 0x06003B2D RID: 15149 RVA: 0x00103818 File Offset: 0x00101A18
		[SRCategory("CatAppearance")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[DefaultValue(null)]
		[SRDescription("TabBaseImageListDescr")]
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
					this.imageList = value;
					IntPtr intPtr = ((value != null) ? value.Handle : IntPtr.Zero);
					if (base.IsHandleCreated)
					{
						base.SendMessage(4867, IntPtr.Zero, intPtr);
					}
					foreach (object obj in this.TabPages)
					{
						TabPage tabPage = (TabPage)obj;
						tabPage.ImageIndexer.ImageList = value;
					}
					if (value != null)
					{
						value.RecreateHandle += eventHandler;
						value.Disposed += eventHandler2;
					}
				}
			}
		}

		/// <summary>Gets or sets the size of the control's tabs.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the size of the tabs. The default automatically sizes the tabs to fit the icons and labels on the tabs.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The width or height of the <see cref="T:System.Drawing.Size" /> is less than 0.</exception>
		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x06003B2E RID: 15150 RVA: 0x00103904 File Offset: 0x00101B04
		// (set) Token: 0x06003B2F RID: 15151 RVA: 0x00103950 File Offset: 0x00101B50
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[SRDescription("TabBaseItemSizeDescr")]
		public Size ItemSize
		{
			get
			{
				if (!this.itemSize.IsEmpty)
				{
					return this.itemSize;
				}
				if (base.IsHandleCreated)
				{
					this.tabControlState[8] = true;
					return this.GetTabRect(0).Size;
				}
				return TabControl.DEFAULT_ITEMSIZE;
			}
			set
			{
				if (value.Width < 0 || value.Height < 0)
				{
					throw new ArgumentOutOfRangeException("ItemSize", SR.GetString("InvalidArgument", new object[]
					{
						"ItemSize",
						value.ToString()
					}));
				}
				this.itemSize = value;
				this.ApplyItemSize();
				this.UpdateSize();
				base.Invalidate();
			}
		}

		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x06003B30 RID: 15152 RVA: 0x001039BD File Offset: 0x00101BBD
		// (set) Token: 0x06003B31 RID: 15153 RVA: 0x001039CF File Offset: 0x00101BCF
		private bool InsertingItem
		{
			get
			{
				return this.tabControlState[128];
			}
			set
			{
				this.tabControlState[128] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether more than one row of tabs can be displayed.</summary>
		/// <returns>
		///   <see langword="true" /> if more than one row of tabs can be displayed; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x06003B32 RID: 15154 RVA: 0x001039E2 File Offset: 0x00101BE2
		// (set) Token: 0x06003B33 RID: 15155 RVA: 0x001039F0 File Offset: 0x00101BF0
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("TabBaseMultilineDescr")]
		public bool Multiline
		{
			get
			{
				return this.tabControlState[2];
			}
			set
			{
				if (this.Multiline != value)
				{
					this.tabControlState[2] = value;
					if (!this.Multiline && (this.alignment == TabAlignment.Left || this.alignment == TabAlignment.Right))
					{
						this.alignment = TabAlignment.Top;
					}
					base.RecreateHandle();
				}
			}
		}

		/// <summary>Gets or sets the amount of space around each item on the control's tab pages.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> that specifies the amount of space around each item. The default is (6,3).</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The width or height of the <see cref="T:System.Drawing.Point" /> is less than 0.</exception>
		// Token: 0x17000E76 RID: 3702
		// (get) Token: 0x06003B34 RID: 15156 RVA: 0x00103A2F File Offset: 0x00101C2F
		// (set) Token: 0x06003B35 RID: 15157 RVA: 0x00103A38 File Offset: 0x00101C38
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[SRDescription("TabBasePaddingDescr")]
		public new Point Padding
		{
			get
			{
				return this.padding;
			}
			set
			{
				if (value.X < 0 || value.Y < 0)
				{
					throw new ArgumentOutOfRangeException("Padding", SR.GetString("InvalidArgument", new object[]
					{
						"Padding",
						value.ToString()
					}));
				}
				if (this.padding != value)
				{
					this.padding = value;
					if (base.IsHandleCreated)
					{
						base.RecreateHandle();
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether right-to-left mirror placement is turned on.</summary>
		/// <returns>
		///   <see langword="true" /> if right-to-left mirror placement is turned on; <see langword="false" /> for standard child control placement. The default is <see langword="false" />.</returns>
		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x06003B36 RID: 15158 RVA: 0x00103AAF File Offset: 0x00101CAF
		// (set) Token: 0x06003B37 RID: 15159 RVA: 0x00103AB8 File Offset: 0x00101CB8
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

		/// <summary>Gets the number of rows that are currently being displayed in the control's tab strip.</summary>
		/// <returns>The number of rows that are currently being displayed in the tab strip.</returns>
		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x06003B38 RID: 15160 RVA: 0x00103B0C File Offset: 0x00101D0C
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TabBaseRowCountDescr")]
		public int RowCount
		{
			get
			{
				return (int)(long)base.SendMessage(4908, 0, 0);
			}
		}

		/// <summary>Gets or sets the index of the currently selected tab page.</summary>
		/// <returns>The zero-based index of the currently selected tab page. The default is -1, which is also the value if no tab page is selected.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than -1.</exception>
		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x06003B39 RID: 15161 RVA: 0x00103B30 File Offset: 0x00101D30
		// (set) Token: 0x06003B3A RID: 15162 RVA: 0x00103B64 File Offset: 0x00101D64
		[Browsable(false)]
		[SRCategory("CatBehavior")]
		[DefaultValue(-1)]
		[SRDescription("selectedIndexDescr")]
		public int SelectedIndex
		{
			get
			{
				if (base.IsHandleCreated)
				{
					return (int)(long)base.SendMessage(4875, 0, 0);
				}
				return this.selectedIndex;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("SelectedIndex", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"SelectedIndex",
						value.ToString(CultureInfo.CurrentCulture),
						(-1).ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this.SelectedIndex != value)
				{
					if (base.IsHandleCreated)
					{
						if (!this.tabControlState[16] && !this.tabControlState[64])
						{
							this.tabControlState[32] = true;
							if (this.WmSelChanging())
							{
								this.tabControlState[32] = false;
								return;
							}
							if (base.ValidationCancelled)
							{
								this.tabControlState[32] = false;
								return;
							}
						}
						base.SendMessage(4876, value, 0);
						if (!this.tabControlState[16] && !this.tabControlState[64])
						{
							this.tabControlState[64] = true;
							if (this.WmSelChange())
							{
								this.tabControlState[32] = false;
								this.tabControlState[64] = false;
								return;
							}
							this.tabControlState[64] = false;
							return;
						}
					}
					else
					{
						this.selectedIndex = value;
					}
				}
			}
		}

		/// <summary>Gets or sets the currently selected tab page.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TabPage" /> that represents the selected tab page. If no tab page is selected, the value is <see langword="null" />.</returns>
		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x06003B3B RID: 15163 RVA: 0x00103C9E File Offset: 0x00101E9E
		// (set) Token: 0x06003B3C RID: 15164 RVA: 0x00103CA6 File Offset: 0x00101EA6
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TabControlSelectedTabDescr")]
		public TabPage SelectedTab
		{
			get
			{
				return this.SelectedTabInternal;
			}
			set
			{
				this.SelectedTabInternal = value;
			}
		}

		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x06003B3D RID: 15165 RVA: 0x00103CB0 File Offset: 0x00101EB0
		// (set) Token: 0x06003B3E RID: 15166 RVA: 0x00103CD4 File Offset: 0x00101ED4
		internal TabPage SelectedTabInternal
		{
			get
			{
				int num = this.SelectedIndex;
				if (num == -1)
				{
					return null;
				}
				return this.tabPages[num];
			}
			set
			{
				int num = this.FindTabPage(value);
				this.SelectedIndex = num;
			}
		}

		/// <summary>Gets or sets the way that the control's tabs are sized.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.TabSizeMode" /> values. The default is <see langword="Normal" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The property value is not a valid <see cref="T:System.Windows.Forms.TabSizeMode" /> value.</exception>
		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x06003B3F RID: 15167 RVA: 0x00103CF0 File Offset: 0x00101EF0
		// (set) Token: 0x06003B40 RID: 15168 RVA: 0x00103CF8 File Offset: 0x00101EF8
		[SRCategory("CatBehavior")]
		[DefaultValue(TabSizeMode.Normal)]
		[SRDescription("TabBaseSizeModeDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public TabSizeMode SizeMode
		{
			get
			{
				return this.sizeMode;
			}
			set
			{
				if (this.sizeMode == value)
				{
					return;
				}
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(TabSizeMode));
				}
				this.sizeMode = value;
				base.RecreateHandle();
			}
		}

		/// <summary>Gets or sets a value indicating whether a tab's ToolTip is shown when the mouse passes over the tab.</summary>
		/// <returns>
		///   <see langword="true" /> if ToolTips are shown for the tabs that have them; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x06003B41 RID: 15169 RVA: 0x00103D37 File Offset: 0x00101F37
		// (set) Token: 0x06003B42 RID: 15170 RVA: 0x00103D45 File Offset: 0x00101F45
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[Localizable(true)]
		[SRDescription("TabBaseShowToolTipsDescr")]
		public bool ShowToolTips
		{
			get
			{
				return this.tabControlState[4];
			}
			set
			{
				if (this.ShowToolTips != value)
				{
					this.tabControlState[4] = value;
					base.RecreateHandle();
				}
			}
		}

		/// <summary>Gets the number of tabs in the tab strip.</summary>
		/// <returns>The number of tabs in the tab strip.</returns>
		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x06003B43 RID: 15171 RVA: 0x00103D63 File Offset: 0x00101F63
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TabBaseTabCountDescr")]
		public int TabCount
		{
			get
			{
				return this.tabPageCount;
			}
		}

		/// <summary>Gets the collection of tab pages in this tab control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TabControl.TabPageCollection" /> that contains the <see cref="T:System.Windows.Forms.TabPage" /> objects in this <see cref="T:System.Windows.Forms.TabControl" />.</returns>
		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x06003B44 RID: 15172 RVA: 0x00103D6B File Offset: 0x00101F6B
		[SRCategory("CatBehavior")]
		[SRDescription("TabControlTabsDescr")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Editor("System.Windows.Forms.Design.TabPageCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[MergableProperty(false)]
		public TabControl.TabPageCollection TabPages
		{
			get
			{
				return this.tabCollection;
			}
		}

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>The text associated with this control.</returns>
		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x06003B45 RID: 15173 RVA: 0x00013814 File Offset: 0x00011A14
		// (set) Token: 0x06003B46 RID: 15174 RVA: 0x00023FE9 File Offset: 0x000221E9
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TabControl.Text" /> property changes.</summary>
		// Token: 0x140002D5 RID: 725
		// (add) Token: 0x06003B47 RID: 15175 RVA: 0x00046591 File Offset: 0x00044791
		// (remove) Token: 0x06003B48 RID: 15176 RVA: 0x0004659A File Offset: 0x0004479A
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

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.TabControl" /> needs to paint each of its tabs if the <see cref="P:System.Windows.Forms.TabControl.DrawMode" /> property is set to <see cref="F:System.Windows.Forms.TabDrawMode.OwnerDrawFixed" />.</summary>
		// Token: 0x140002D6 RID: 726
		// (add) Token: 0x06003B49 RID: 15177 RVA: 0x00103D73 File Offset: 0x00101F73
		// (remove) Token: 0x06003B4A RID: 15178 RVA: 0x00103D8C File Offset: 0x00101F8C
		[SRCategory("CatBehavior")]
		[SRDescription("drawItemEventDescr")]
		public event DrawItemEventHandler DrawItem
		{
			add
			{
				this.onDrawItem = (DrawItemEventHandler)Delegate.Combine(this.onDrawItem, value);
			}
			remove
			{
				this.onDrawItem = (DrawItemEventHandler)Delegate.Remove(this.onDrawItem, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TabControl.RightToLeftLayout" /> property changes.</summary>
		// Token: 0x140002D7 RID: 727
		// (add) Token: 0x06003B4B RID: 15179 RVA: 0x00103DA5 File Offset: 0x00101FA5
		// (remove) Token: 0x06003B4C RID: 15180 RVA: 0x00103DB8 File Offset: 0x00101FB8
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnRightToLeftLayoutChangedDescr")]
		public event EventHandler RightToLeftLayoutChanged
		{
			add
			{
				base.Events.AddHandler(TabControl.EVENT_RIGHTTOLEFTLAYOUTCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(TabControl.EVENT_RIGHTTOLEFTLAYOUTCHANGED, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.TabControl.SelectedIndex" /> property has changed.</summary>
		// Token: 0x140002D8 RID: 728
		// (add) Token: 0x06003B4D RID: 15181 RVA: 0x00103DCB File Offset: 0x00101FCB
		// (remove) Token: 0x06003B4E RID: 15182 RVA: 0x00103DE4 File Offset: 0x00101FE4
		[SRCategory("CatBehavior")]
		[SRDescription("selectedIndexChangedEventDescr")]
		public event EventHandler SelectedIndexChanged
		{
			add
			{
				this.onSelectedIndexChanged = (EventHandler)Delegate.Combine(this.onSelectedIndexChanged, value);
			}
			remove
			{
				this.onSelectedIndexChanged = (EventHandler)Delegate.Remove(this.onSelectedIndexChanged, value);
			}
		}

		/// <summary>Occurs before a tab is selected, enabling a handler to cancel the tab change.</summary>
		// Token: 0x140002D9 RID: 729
		// (add) Token: 0x06003B4F RID: 15183 RVA: 0x00103DFD File Offset: 0x00101FFD
		// (remove) Token: 0x06003B50 RID: 15184 RVA: 0x00103E10 File Offset: 0x00102010
		[SRCategory("CatAction")]
		[SRDescription("TabControlSelectingEventDescr")]
		public event TabControlCancelEventHandler Selecting
		{
			add
			{
				base.Events.AddHandler(TabControl.EVENT_SELECTING, value);
			}
			remove
			{
				base.Events.RemoveHandler(TabControl.EVENT_SELECTING, value);
			}
		}

		/// <summary>Occurs when a tab is selected.</summary>
		// Token: 0x140002DA RID: 730
		// (add) Token: 0x06003B51 RID: 15185 RVA: 0x00103E23 File Offset: 0x00102023
		// (remove) Token: 0x06003B52 RID: 15186 RVA: 0x00103E36 File Offset: 0x00102036
		[SRCategory("CatAction")]
		[SRDescription("TabControlSelectedEventDescr")]
		public event TabControlEventHandler Selected
		{
			add
			{
				base.Events.AddHandler(TabControl.EVENT_SELECTED, value);
			}
			remove
			{
				base.Events.RemoveHandler(TabControl.EVENT_SELECTED, value);
			}
		}

		/// <summary>Occurs before a tab is deselected, enabling a handler to cancel the tab change.</summary>
		// Token: 0x140002DB RID: 731
		// (add) Token: 0x06003B53 RID: 15187 RVA: 0x00103E49 File Offset: 0x00102049
		// (remove) Token: 0x06003B54 RID: 15188 RVA: 0x00103E5C File Offset: 0x0010205C
		[SRCategory("CatAction")]
		[SRDescription("TabControlDeselectingEventDescr")]
		public event TabControlCancelEventHandler Deselecting
		{
			add
			{
				base.Events.AddHandler(TabControl.EVENT_DESELECTING, value);
			}
			remove
			{
				base.Events.RemoveHandler(TabControl.EVENT_DESELECTING, value);
			}
		}

		/// <summary>Occurs when a tab is deselected.</summary>
		// Token: 0x140002DC RID: 732
		// (add) Token: 0x06003B55 RID: 15189 RVA: 0x00103E6F File Offset: 0x0010206F
		// (remove) Token: 0x06003B56 RID: 15190 RVA: 0x00103E82 File Offset: 0x00102082
		[SRCategory("CatAction")]
		[SRDescription("TabControlDeselectedEventDescr")]
		public event TabControlEventHandler Deselected
		{
			add
			{
				base.Events.AddHandler(TabControl.EVENT_DESELECTED, value);
			}
			remove
			{
				base.Events.RemoveHandler(TabControl.EVENT_DESELECTED, value);
			}
		}

		/// <summary>This event is not meaningful for this control.</summary>
		// Token: 0x140002DD RID: 733
		// (add) Token: 0x06003B57 RID: 15191 RVA: 0x00013D73 File Offset: 0x00011F73
		// (remove) Token: 0x06003B58 RID: 15192 RVA: 0x00013D7C File Offset: 0x00011F7C
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

		// Token: 0x06003B59 RID: 15193 RVA: 0x00103E98 File Offset: 0x00102098
		internal int AddTabPage(TabPage tabPage, NativeMethods.TCITEM_T tcitem)
		{
			int num = this.AddNativeTabPage(tcitem);
			if (num >= 0)
			{
				this.Insert(num, tabPage);
			}
			return num;
		}

		// Token: 0x06003B5A RID: 15194 RVA: 0x00103EBC File Offset: 0x001020BC
		internal int AddNativeTabPage(NativeMethods.TCITEM_T tcitem)
		{
			int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.TCM_INSERTITEM, this.tabPageCount + 1, tcitem);
			UnsafeNativeMethods.PostMessage(new HandleRef(this, base.Handle), this.tabBaseReLayoutMessage, IntPtr.Zero, IntPtr.Zero);
			return num;
		}

		// Token: 0x06003B5B RID: 15195 RVA: 0x00103F14 File Offset: 0x00102114
		internal void ApplyItemSize()
		{
			if (base.IsHandleCreated && this.ShouldSerializeItemSize())
			{
				base.SendMessage(4905, 0, (int)NativeMethods.Util.MAKELPARAM(this.itemSize.Width, this.itemSize.Height));
			}
			this.cachedDisplayRect = Rectangle.Empty;
		}

		// Token: 0x06003B5C RID: 15196 RVA: 0x00103F69 File Offset: 0x00102169
		internal void BeginUpdate()
		{
			base.BeginUpdateInternal();
		}

		/// <summary>This member overrides <see cref="M:System.Windows.Forms.Control.CreateControlsInstance" />.</summary>
		/// <returns>A new instance of <see cref="T:System.Windows.Forms.Control.ControlCollection" /> assigned to the control.</returns>
		// Token: 0x06003B5D RID: 15197 RVA: 0x00103F71 File Offset: 0x00102171
		protected override Control.ControlCollection CreateControlsInstance()
		{
			return new TabControl.ControlCollection(this);
		}

		/// <summary>This member overrides <see cref="M:System.Windows.Forms.Control.CreateHandle" />.</summary>
		// Token: 0x06003B5E RID: 15198 RVA: 0x00103F7C File Offset: 0x0010217C
		protected override void CreateHandle()
		{
			if (!base.RecreatingHandle)
			{
				IntPtr intPtr = UnsafeNativeMethods.ThemingScope.Activate();
				try
				{
					SafeNativeMethods.InitCommonControlsEx(new NativeMethods.INITCOMMONCONTROLSEX
					{
						dwICC = 8
					});
				}
				finally
				{
					UnsafeNativeMethods.ThemingScope.Deactivate(intPtr);
				}
			}
			base.CreateHandle();
		}

		// Token: 0x06003B5F RID: 15199 RVA: 0x00103FCC File Offset: 0x001021CC
		private void DetachImageList(object sender, EventArgs e)
		{
			this.ImageList = null;
		}

		/// <summary>Makes the tab following the tab with the specified index the current tab.</summary>
		/// <param name="index">The index in the <see cref="P:System.Windows.Forms.TabControl.TabPages" /> collection of the tab to deselect.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0 or greater than the number of <see cref="T:System.Windows.Forms.TabPage" /> controls in the <see cref="P:System.Windows.Forms.TabControl.TabPages" /> collection minus 1.</exception>
		// Token: 0x06003B60 RID: 15200 RVA: 0x00103FD8 File Offset: 0x001021D8
		public void DeselectTab(int index)
		{
			TabPage tabPage = this.GetTabPage(index);
			if (this.SelectedTab == tabPage)
			{
				if (0 <= index && index < this.TabPages.Count - 1)
				{
					this.SelectedTab = this.GetTabPage(++index);
					return;
				}
				this.SelectedTab = this.GetTabPage(0);
			}
		}

		/// <summary>Makes the tab following the specified <see cref="T:System.Windows.Forms.TabPage" /> the current tab.</summary>
		/// <param name="tabPage">The <see cref="T:System.Windows.Forms.TabPage" /> to deselect.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0 or greater than the number of <see cref="T:System.Windows.Forms.TabPage" /> controls in the <see cref="P:System.Windows.Forms.TabControl.TabPages" /> collection minus 1.  
		/// -or-  
		/// <paramref name="tabPage" /> is not in the <see cref="P:System.Windows.Forms.TabControl.TabPages" /> collection.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="tabPage" /> is <see langword="null" />.</exception>
		// Token: 0x06003B61 RID: 15201 RVA: 0x0010402C File Offset: 0x0010222C
		public void DeselectTab(TabPage tabPage)
		{
			if (tabPage == null)
			{
				throw new ArgumentNullException("tabPage");
			}
			int num = this.FindTabPage(tabPage);
			this.DeselectTab(num);
		}

		/// <summary>Makes the tab following the tab with the specified name the current tab.</summary>
		/// <param name="tabPageName">The <see cref="P:System.Windows.Forms.Control.Name" /> of the tab to deselect.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="tabPageName" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="tabPageName" /> does not match the <see cref="P:System.Windows.Forms.Control.Name" /> property of any <see cref="T:System.Windows.Forms.TabPage" /> in the <see cref="P:System.Windows.Forms.TabControl.TabPages" /> collection.</exception>
		// Token: 0x06003B62 RID: 15202 RVA: 0x00104058 File Offset: 0x00102258
		public void DeselectTab(string tabPageName)
		{
			if (tabPageName == null)
			{
				throw new ArgumentNullException("tabPageName");
			}
			TabPage tabPage = this.TabPages[tabPageName];
			this.DeselectTab(tabPage);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Control" /> and its child controls and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06003B63 RID: 15203 RVA: 0x00104087 File Offset: 0x00102287
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.imageList != null)
			{
				this.imageList.Disposed -= this.DetachImageList;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06003B64 RID: 15204 RVA: 0x001040B2 File Offset: 0x001022B2
		internal void EndUpdate()
		{
			this.EndUpdate(true);
		}

		// Token: 0x06003B65 RID: 15205 RVA: 0x001040BB File Offset: 0x001022BB
		internal void EndUpdate(bool invalidate)
		{
			base.EndUpdateInternal(invalidate);
		}

		// Token: 0x06003B66 RID: 15206 RVA: 0x001040C8 File Offset: 0x001022C8
		internal int FindTabPage(TabPage tabPage)
		{
			if (this.tabPages != null)
			{
				for (int i = 0; i < this.tabPageCount; i++)
				{
					if (this.tabPages[i].Equals(tabPage))
					{
						return i;
					}
				}
			}
			return -1;
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.TabPage" /> control at the specified location.</summary>
		/// <param name="index">The index of the <see cref="T:System.Windows.Forms.TabPage" /> to get.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.TabPage" /> at the specified location.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0 or greater than the <see cref="P:System.Windows.Forms.TabControl.TabCount" />.</exception>
		// Token: 0x06003B67 RID: 15207 RVA: 0x00104101 File Offset: 0x00102301
		public Control GetControl(int index)
		{
			return this.GetTabPage(index);
		}

		// Token: 0x06003B68 RID: 15208 RVA: 0x0010410C File Offset: 0x0010230C
		internal TabPage GetTabPage(int index)
		{
			if (index < 0 || index >= this.tabPageCount)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			return this.tabPages[index];
		}

		/// <summary>Gets an array of <see cref="T:System.Windows.Forms.TabPage" /> controls that belong to the <see cref="T:System.Windows.Forms.TabControl" /> control.</summary>
		/// <returns>An array of <see cref="T:System.Windows.Forms.TabPage" /> controls that belong to the <see cref="T:System.Windows.Forms.TabControl" />.</returns>
		// Token: 0x06003B69 RID: 15209 RVA: 0x00104160 File Offset: 0x00102360
		protected virtual object[] GetItems()
		{
			TabPage[] array = new TabPage[this.tabPageCount];
			if (this.tabPageCount > 0)
			{
				Array.Copy(this.tabPages, 0, array, 0, this.tabPageCount);
			}
			return array;
		}

		/// <summary>Copies the <see cref="T:System.Windows.Forms.TabPage" /> controls in the <see cref="T:System.Windows.Forms.TabControl" /> to an array of the specified type.</summary>
		/// <param name="baseType">The <see cref="T:System.Type" /> of the array to create.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.TabPage" /> controls that belong to the <see cref="T:System.Windows.Forms.TabControl" /> as an array of the specified type.</returns>
		/// <exception cref="T:System.ArrayTypeMismatchException">The type <see cref="T:System.Windows.Forms.TabPage" /> cannot be converted to <paramref name="baseType" />.</exception>
		// Token: 0x06003B6A RID: 15210 RVA: 0x0010419C File Offset: 0x0010239C
		protected virtual object[] GetItems(Type baseType)
		{
			object[] array = (object[])Array.CreateInstance(baseType, this.tabPageCount);
			if (this.tabPageCount > 0)
			{
				Array.Copy(this.tabPages, 0, array, 0, this.tabPageCount);
			}
			return array;
		}

		// Token: 0x06003B6B RID: 15211 RVA: 0x001041D9 File Offset: 0x001023D9
		internal TabPage[] GetTabPages()
		{
			return (TabPage[])this.GetItems();
		}

		/// <summary>Returns the bounding rectangle for a specified tab in this tab control.</summary>
		/// <param name="index">The zero-based index of the tab you want.</param>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the specified tab.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index is less than zero.  
		///  -or-  
		///  The index is greater than or equal to <see cref="P:System.Windows.Forms.TabControl.TabPageCollection.Count" />.</exception>
		// Token: 0x06003B6C RID: 15212 RVA: 0x001041E8 File Offset: 0x001023E8
		public Rectangle GetTabRect(int index)
		{
			if (index < 0 || (index >= this.tabPageCount && !this.tabControlState[8]))
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			this.tabControlState[8] = false;
			NativeMethods.RECT rect = default(NativeMethods.RECT);
			if (!base.IsHandleCreated)
			{
				this.CreateHandle();
			}
			base.SendMessage(4874, index, ref rect);
			return Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
		}

		/// <summary>Gets the ToolTip for the specified <see cref="T:System.Windows.Forms.TabPage" />.</summary>
		/// <param name="item">The <see cref="T:System.Windows.Forms.TabPage" /> that owns the desired ToolTip.</param>
		/// <returns>The ToolTip text.</returns>
		// Token: 0x06003B6D RID: 15213 RVA: 0x00104291 File Offset: 0x00102491
		protected string GetToolTipText(object item)
		{
			return ((TabPage)item).ToolTipText;
		}

		// Token: 0x06003B6E RID: 15214 RVA: 0x0010429E File Offset: 0x0010249E
		private void ImageListRecreateHandle(object sender, EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				base.SendMessage(4867, 0, this.ImageList.Handle);
			}
		}

		// Token: 0x06003B6F RID: 15215 RVA: 0x001042C0 File Offset: 0x001024C0
		internal void Insert(int index, TabPage tabPage)
		{
			if (this.tabPages == null)
			{
				this.tabPages = new TabPage[4];
			}
			else if (this.tabPages.Length == this.tabPageCount)
			{
				TabPage[] array = new TabPage[this.tabPageCount * 2];
				Array.Copy(this.tabPages, 0, array, 0, this.tabPageCount);
				this.tabPages = array;
			}
			if (index < this.tabPageCount)
			{
				Array.Copy(this.tabPages, index, this.tabPages, index + 1, this.tabPageCount - index);
			}
			this.tabPages[index] = tabPage;
			this.tabPageCount++;
			this.cachedDisplayRect = Rectangle.Empty;
			this.ApplyItemSize();
			if (this.Appearance == TabAppearance.FlatButtons)
			{
				base.Invalidate();
			}
		}

		// Token: 0x06003B70 RID: 15216 RVA: 0x0010437C File Offset: 0x0010257C
		private void InsertItem(int index, TabPage tabPage)
		{
			if (index < 0 || (this.tabPages != null && index > this.tabPageCount))
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (tabPage == null)
			{
				throw new ArgumentNullException("tabPage");
			}
			if (base.IsHandleCreated)
			{
				NativeMethods.TCITEM_T tcitem = tabPage.GetTCITEM();
				int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.TCM_INSERTITEM, index, tcitem);
				if (num >= 0)
				{
					this.Insert(num, tabPage);
				}
			}
		}

		/// <summary>Determines whether the specified key is a regular input key or a special key that requires preprocessing.</summary>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values.</param>
		/// <returns>
		///   <see langword="true" /> if the specified key is a regular input key; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003B71 RID: 15217 RVA: 0x00104418 File Offset: 0x00102618
		protected override bool IsInputKey(Keys keyData)
		{
			if ((keyData & Keys.Alt) == Keys.Alt)
			{
				return false;
			}
			Keys keys = keyData & Keys.KeyCode;
			return keys - Keys.Prior <= 3 || base.IsInputKey(keyData);
		}

		/// <summary>This member overrides <see cref="M:System.Windows.Forms.Control.OnHandleCreated(System.EventArgs)" />.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003B72 RID: 15218 RVA: 0x00104450 File Offset: 0x00102650
		protected override void OnHandleCreated(EventArgs e)
		{
			NativeWindow.AddWindowToIDTable(this, base.Handle);
			this.handleInTable = true;
			if (!this.padding.IsEmpty)
			{
				base.SendMessage(4907, 0, NativeMethods.Util.MAKELPARAM(this.padding.X, this.padding.Y));
			}
			base.OnHandleCreated(e);
			this.cachedDisplayRect = Rectangle.Empty;
			this.ApplyItemSize();
			if (this.imageList != null)
			{
				base.SendMessage(4867, 0, this.imageList.Handle);
			}
			if (this.ShowToolTips)
			{
				IntPtr intPtr = base.SendMessage(4909, 0, 0);
				if (intPtr != IntPtr.Zero)
				{
					SafeNativeMethods.SetWindowPos(new HandleRef(this, intPtr), NativeMethods.HWND_TOPMOST, 0, 0, 0, 0, 19);
				}
			}
			foreach (object obj in this.TabPages)
			{
				TabPage tabPage = (TabPage)obj;
				this.AddNativeTabPage(tabPage.GetTCITEM());
			}
			this.ResizePages();
			if (this.selectedIndex != -1)
			{
				try
				{
					this.tabControlState[16] = true;
					this.SelectedIndex = this.selectedIndex;
				}
				finally
				{
					this.tabControlState[16] = false;
				}
				this.selectedIndex = -1;
			}
			this.UpdateTabSelection(false);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003B73 RID: 15219 RVA: 0x001045C0 File Offset: 0x001027C0
		protected override void OnHandleDestroyed(EventArgs e)
		{
			if (!base.Disposing)
			{
				this.selectedIndex = this.SelectedIndex;
			}
			if (this.handleInTable)
			{
				this.handleInTable = false;
				NativeWindow.RemoveWindowFromIDTable(base.Handle);
			}
			base.OnHandleDestroyed(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TabControl.DrawItem" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DrawItemEventArgs" /> that contains the event data.</param>
		// Token: 0x06003B74 RID: 15220 RVA: 0x001045F7 File Offset: 0x001027F7
		protected virtual void OnDrawItem(DrawItemEventArgs e)
		{
			if (this.onDrawItem != null)
			{
				this.onDrawItem(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Enter" /> event of the <see cref="T:System.Windows.Forms.TabControl" />.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003B75 RID: 15221 RVA: 0x0010460E File Offset: 0x0010280E
		protected override void OnEnter(EventArgs e)
		{
			base.OnEnter(e);
			if (this.SelectedTab != null)
			{
				this.SelectedTab.FireEnter(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Leave" /> event of the <see cref="T:System.Windows.Forms.TabControl" />.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003B76 RID: 15222 RVA: 0x0010462B File Offset: 0x0010282B
		protected override void OnLeave(EventArgs e)
		{
			if (this.SelectedTab != null)
			{
				this.SelectedTab.FireLeave(e);
			}
			base.OnLeave(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event.</summary>
		/// <param name="ke">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
		// Token: 0x06003B77 RID: 15223 RVA: 0x00104648 File Offset: 0x00102848
		protected override void OnKeyDown(KeyEventArgs ke)
		{
			if (ke.KeyCode == Keys.Tab && (ke.KeyData & Keys.Control) != Keys.None)
			{
				bool flag = (ke.KeyData & Keys.Shift) == Keys.None;
				this.SelectNextTab(ke, flag);
			}
			if (ke.KeyCode == Keys.Next && (ke.KeyData & Keys.Control) != Keys.None)
			{
				this.SelectNextTab(ke, true);
			}
			if (ke.KeyCode == Keys.Prior && (ke.KeyData & Keys.Control) != Keys.None)
			{
				this.SelectNextTab(ke, false);
			}
			base.OnKeyDown(ke);
		}

		// Token: 0x06003B78 RID: 15224 RVA: 0x001046CC File Offset: 0x001028CC
		internal override void OnParentHandleRecreated()
		{
			this.skipUpdateSize = true;
			try
			{
				base.OnParentHandleRecreated();
			}
			finally
			{
				this.skipUpdateSize = false;
			}
		}

		/// <summary>This member overrides <see cref="M:System.Windows.Forms.Control.OnResize(System.EventArgs)" />.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003B79 RID: 15225 RVA: 0x00104700 File Offset: 0x00102900
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			this.cachedDisplayRect = Rectangle.Empty;
			this.UpdateTabSelection(false);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TabControl.RightToLeftLayoutChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003B7A RID: 15226 RVA: 0x0010471C File Offset: 0x0010291C
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
			EventHandler eventHandler = base.Events[TabControl.EVENT_RIGHTTOLEFTLAYOUTCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TabControl.SelectedIndexChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003B7B RID: 15227 RVA: 0x00104764 File Offset: 0x00102964
		protected virtual void OnSelectedIndexChanged(EventArgs e)
		{
			int num = this.SelectedIndex;
			this.cachedDisplayRect = Rectangle.Empty;
			this.UpdateTabSelection(this.tabControlState[32]);
			this.tabControlState[32] = false;
			if (this.onSelectedIndexChanged != null)
			{
				this.onSelectedIndexChanged(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TabControl.Selecting" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TabControlCancelEventArgs" /> that contains the event data.</param>
		// Token: 0x06003B7C RID: 15228 RVA: 0x001047BC File Offset: 0x001029BC
		protected virtual void OnSelecting(TabControlCancelEventArgs e)
		{
			TabControlCancelEventHandler tabControlCancelEventHandler = (TabControlCancelEventHandler)base.Events[TabControl.EVENT_SELECTING];
			if (tabControlCancelEventHandler != null)
			{
				tabControlCancelEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TabControl.Selected" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TabControlEventArgs" /> that contains the event data.</param>
		// Token: 0x06003B7D RID: 15229 RVA: 0x001047EC File Offset: 0x001029EC
		protected virtual void OnSelected(TabControlEventArgs e)
		{
			TabControlEventHandler tabControlEventHandler = (TabControlEventHandler)base.Events[TabControl.EVENT_SELECTED];
			if (tabControlEventHandler != null)
			{
				tabControlEventHandler(this, e);
			}
			if (this.SelectedTab != null)
			{
				this.SelectedTab.FireEnter(EventArgs.Empty);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TabControl.Deselecting" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TabControlCancelEventArgs" /> that contains the event data.</param>
		// Token: 0x06003B7E RID: 15230 RVA: 0x00104834 File Offset: 0x00102A34
		protected virtual void OnDeselecting(TabControlCancelEventArgs e)
		{
			TabControlCancelEventHandler tabControlCancelEventHandler = (TabControlCancelEventHandler)base.Events[TabControl.EVENT_DESELECTING];
			if (tabControlCancelEventHandler != null)
			{
				tabControlCancelEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TabControl.Deselected" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TabControlEventArgs" /> that contains the event data.</param>
		// Token: 0x06003B7F RID: 15231 RVA: 0x00104864 File Offset: 0x00102A64
		protected virtual void OnDeselected(TabControlEventArgs e)
		{
			TabControlEventHandler tabControlEventHandler = (TabControlEventHandler)base.Events[TabControl.EVENT_DESELECTED];
			if (tabControlEventHandler != null)
			{
				tabControlEventHandler(this, e);
			}
			if (this.SelectedTab != null)
			{
				this.SelectedTab.FireLeave(EventArgs.Empty);
			}
		}

		/// <summary>Previews a keyboard message.</summary>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process.</param>
		/// <returns>
		///   <see langword="true" /> if the message was processed by the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003B80 RID: 15232 RVA: 0x001048AA File Offset: 0x00102AAA
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override bool ProcessKeyPreview(ref Message m)
		{
			return this.ProcessKeyEventArgs(ref m) || base.ProcessKeyPreview(ref m);
		}

		// Token: 0x06003B81 RID: 15233 RVA: 0x001048C0 File Offset: 0x00102AC0
		internal void UpdateSize()
		{
			if (this.skipUpdateSize)
			{
				return;
			}
			this.BeginUpdate();
			Size size = base.Size;
			base.Size = new Size(size.Width + 1, size.Height);
			base.Size = size;
			this.EndUpdate();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003B82 RID: 15234 RVA: 0x0010490B File Offset: 0x00102B0B
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.cachedDisplayRect = Rectangle.Empty;
			this.UpdateSize();
		}

		// Token: 0x06003B83 RID: 15235 RVA: 0x00104928 File Offset: 0x00102B28
		internal override void RecreateHandleCore()
		{
			TabPage[] array = this.GetTabPages();
			int num = ((array.Length != 0 && this.SelectedIndex == -1) ? 0 : this.SelectedIndex);
			if (base.IsHandleCreated)
			{
				base.SendMessage(4873, 0, 0);
			}
			this.tabPages = null;
			this.tabPageCount = 0;
			base.RecreateHandleCore();
			for (int i = 0; i < array.Length; i++)
			{
				this.TabPages.Add(array[i]);
			}
			try
			{
				this.tabControlState[16] = true;
				this.SelectedIndex = num;
			}
			finally
			{
				this.tabControlState[16] = false;
			}
			this.UpdateSize();
		}

		/// <summary>Removes all the tab pages and additional controls from this tab control.</summary>
		// Token: 0x06003B84 RID: 15236 RVA: 0x001049D8 File Offset: 0x00102BD8
		protected void RemoveAll()
		{
			base.Controls.Clear();
			base.SendMessage(4873, 0, 0);
			this.tabPages = null;
			this.tabPageCount = 0;
		}

		// Token: 0x06003B85 RID: 15237 RVA: 0x00104A04 File Offset: 0x00102C04
		internal void RemoveTabPage(int index)
		{
			if (index < 0 || index >= this.tabPageCount)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			this.tabPageCount--;
			if (index < this.tabPageCount)
			{
				Array.Copy(this.tabPages, index + 1, this.tabPages, index, this.tabPageCount - index);
			}
			this.tabPages[this.tabPageCount] = null;
			if (base.IsHandleCreated)
			{
				base.SendMessage(4872, index, 0);
			}
			this.cachedDisplayRect = Rectangle.Empty;
		}

		// Token: 0x06003B86 RID: 15238 RVA: 0x00104AB3 File Offset: 0x00102CB3
		private void ResetItemSize()
		{
			this.ItemSize = TabControl.DEFAULT_ITEMSIZE;
		}

		// Token: 0x06003B87 RID: 15239 RVA: 0x00104AC0 File Offset: 0x00102CC0
		private void ResetPadding()
		{
			this.Padding = TabControl.DEFAULT_PADDING;
		}

		// Token: 0x06003B88 RID: 15240 RVA: 0x00104AD0 File Offset: 0x00102CD0
		private void ResizePages()
		{
			Rectangle displayRectangle = this.DisplayRectangle;
			TabPage[] array = this.GetTabPages();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Bounds = displayRectangle;
			}
		}

		// Token: 0x06003B89 RID: 15241 RVA: 0x00104B02 File Offset: 0x00102D02
		internal void SetToolTip(ToolTip toolTip, string controlToolTipText)
		{
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4910, new HandleRef(toolTip, toolTip.Handle), 0);
			this.controlTipText = controlToolTipText;
		}

		// Token: 0x06003B8A RID: 15242 RVA: 0x00104B30 File Offset: 0x00102D30
		internal void SetTabPage(int index, TabPage tabPage, NativeMethods.TCITEM_T tcitem)
		{
			if (index < 0 || index >= this.tabPageCount)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (base.IsHandleCreated)
			{
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.TCM_SETITEM, index, tcitem);
			}
			if (base.DesignMode && base.IsHandleCreated)
			{
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4876, (IntPtr)index, IntPtr.Zero);
			}
			this.tabPages[index] = tabPage;
		}

		/// <summary>Makes the tab with the specified index the current tab.</summary>
		/// <param name="index">The index in the <see cref="P:System.Windows.Forms.TabControl.TabPages" /> collection of the tab to select.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0 or greater than the number of <see cref="T:System.Windows.Forms.TabPage" /> controls in the <see cref="P:System.Windows.Forms.TabControl.TabPages" /> collection minus 1.</exception>
		// Token: 0x06003B8B RID: 15243 RVA: 0x00104BD8 File Offset: 0x00102DD8
		public void SelectTab(int index)
		{
			TabPage tabPage = this.GetTabPage(index);
			if (tabPage != null)
			{
				this.SelectedTab = tabPage;
			}
		}

		/// <summary>Makes the specified <see cref="T:System.Windows.Forms.TabPage" /> the current tab.</summary>
		/// <param name="tabPage">The <see cref="T:System.Windows.Forms.TabPage" /> to select.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0 or greater than the number of <see cref="T:System.Windows.Forms.TabPage" /> controls in the <see cref="P:System.Windows.Forms.TabControl.TabPages" /> collection minus 1.  
		/// -or-  
		/// <paramref name="tabPage" /> is not in the <see cref="P:System.Windows.Forms.TabControl.TabPages" /> collection.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="tabPage" /> is <see langword="null" />.</exception>
		// Token: 0x06003B8C RID: 15244 RVA: 0x00104BF8 File Offset: 0x00102DF8
		public void SelectTab(TabPage tabPage)
		{
			if (tabPage == null)
			{
				throw new ArgumentNullException("tabPage");
			}
			int num = this.FindTabPage(tabPage);
			this.SelectTab(num);
		}

		/// <summary>Makes the tab with the specified name the current tab.</summary>
		/// <param name="tabPageName">The <see cref="P:System.Windows.Forms.Control.Name" /> of the tab to select.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="tabPageName" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="tabPageName" /> does not match the <see cref="P:System.Windows.Forms.Control.Name" /> property of any <see cref="T:System.Windows.Forms.TabPage" /> in the <see cref="P:System.Windows.Forms.TabControl.TabPages" /> collection.</exception>
		// Token: 0x06003B8D RID: 15245 RVA: 0x00104C24 File Offset: 0x00102E24
		public void SelectTab(string tabPageName)
		{
			if (tabPageName == null)
			{
				throw new ArgumentNullException("tabPageName");
			}
			TabPage tabPage = this.TabPages[tabPageName];
			this.SelectTab(tabPage);
		}

		// Token: 0x06003B8E RID: 15246 RVA: 0x00104C54 File Offset: 0x00102E54
		private void SelectNextTab(KeyEventArgs ke, bool forward)
		{
			bool focused = this.Focused;
			if (this.WmSelChanging())
			{
				this.tabControlState[32] = false;
				return;
			}
			if (base.ValidationCancelled)
			{
				this.tabControlState[32] = false;
				return;
			}
			int num = this.SelectedIndex;
			if (num != -1)
			{
				int tabCount = this.TabCount;
				if (forward)
				{
					num = (num + 1) % tabCount;
				}
				else
				{
					num = (num + tabCount - 1) % tabCount;
				}
				try
				{
					this.tabControlState[32] = true;
					this.tabControlState[64] = true;
					this.SelectedIndex = num;
					this.tabControlState[64] = !focused;
					this.WmSelChange();
				}
				finally
				{
					this.tabControlState[64] = false;
					ke.Handled = true;
				}
			}
		}

		// Token: 0x06003B8F RID: 15247 RVA: 0x00012E4E File Offset: 0x0001104E
		internal override bool ShouldPerformContainerValidation()
		{
			return true;
		}

		// Token: 0x06003B90 RID: 15248 RVA: 0x00104D20 File Offset: 0x00102F20
		private bool ShouldSerializeItemSize()
		{
			return !this.itemSize.Equals(TabControl.DEFAULT_ITEMSIZE);
		}

		// Token: 0x06003B91 RID: 15249 RVA: 0x00104D40 File Offset: 0x00102F40
		private new bool ShouldSerializePadding()
		{
			return !this.padding.Equals(TabControl.DEFAULT_PADDING);
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.TabControl" /> control.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.TabControl" />.</returns>
		// Token: 0x06003B92 RID: 15250 RVA: 0x00104D60 File Offset: 0x00102F60
		public override string ToString()
		{
			string text = base.ToString();
			if (this.TabPages != null)
			{
				text = text + ", TabPages.Count: " + this.TabPages.Count.ToString(CultureInfo.CurrentCulture);
				if (this.TabPages.Count > 0)
				{
					text = text + ", TabPages[0]: " + this.TabPages[0].ToString();
				}
			}
			return text;
		}

		/// <summary>This member overrides <see cref="M:System.Windows.Forms.Control.ScaleCore(System.Single,System.Single)" />.</summary>
		/// <param name="dx">The horizontal scaling factor.</param>
		/// <param name="dy">The vertical scaling factor.</param>
		// Token: 0x06003B93 RID: 15251 RVA: 0x00104DCC File Offset: 0x00102FCC
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void ScaleCore(float dx, float dy)
		{
			this.currentlyScaling = true;
			base.ScaleCore(dx, dy);
			this.currentlyScaling = false;
		}

		/// <summary>Sets the <see cref="P:System.Windows.Forms.TabPage.Visible" /> property to <see langword="true" /> for the appropriate <see cref="T:System.Windows.Forms.TabPage" /> control in the <see cref="P:System.Windows.Forms.TabControl.TabPages" /> collection.</summary>
		/// <param name="updateFocus">
		///   <see langword="true" /> to change focus to the next <see cref="T:System.Windows.Forms.TabPage" />; otherwise, <see langword="false" />.</param>
		// Token: 0x06003B94 RID: 15252 RVA: 0x00104DE4 File Offset: 0x00102FE4
		protected void UpdateTabSelection(bool updateFocus)
		{
			if (base.IsHandleCreated)
			{
				int num = this.SelectedIndex;
				TabPage[] array = this.GetTabPages();
				if (num != -1)
				{
					if (this.currentlyScaling)
					{
						array[num].SuspendLayout();
					}
					array[num].Bounds = this.DisplayRectangle;
					array[num].Invalidate();
					if (this.currentlyScaling)
					{
						array[num].ResumeLayout(false);
					}
					array[num].Visible = true;
					if (updateFocus && (!this.Focused || this.tabControlState[64]))
					{
						this.tabControlState[32] = false;
						bool flag = false;
						IntSecurity.ModifyFocus.Assert();
						try
						{
							flag = array[num].SelectNextControl(null, true, true, false, false);
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
						if (flag)
						{
							if (!base.ContainsFocus)
							{
								IContainerControl containerControl = base.GetContainerControlInternal();
								if (containerControl != null)
								{
									while (containerControl.ActiveControl is ContainerControl)
									{
										containerControl = (IContainerControl)containerControl.ActiveControl;
									}
									if (containerControl.ActiveControl != null)
									{
										containerControl.ActiveControl.FocusInternal();
									}
								}
							}
						}
						else
						{
							IContainerControl containerControlInternal = base.GetContainerControlInternal();
							if (containerControlInternal != null && !base.DesignMode)
							{
								if (containerControlInternal is ContainerControl)
								{
									((ContainerControl)containerControlInternal).SetActiveControlInternal(this);
								}
								else
								{
									IntSecurity.ModifyFocus.Assert();
									try
									{
										containerControlInternal.ActiveControl = this;
									}
									finally
									{
										CodeAccessPermission.RevertAssert();
									}
								}
							}
						}
					}
				}
				for (int i = 0; i < array.Length; i++)
				{
					if (i != this.SelectedIndex)
					{
						array[i].Visible = false;
					}
				}
			}
		}

		/// <summary>This member overrides <see cref="M:System.Windows.Forms.Control.OnStyleChanged(System.EventArgs)" />.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003B95 RID: 15253 RVA: 0x00104F74 File Offset: 0x00103174
		protected override void OnStyleChanged(EventArgs e)
		{
			base.OnStyleChanged(e);
			this.cachedDisplayRect = Rectangle.Empty;
			this.UpdateTabSelection(false);
		}

		// Token: 0x06003B96 RID: 15254 RVA: 0x00104F90 File Offset: 0x00103190
		internal void UpdateTab(TabPage tabPage)
		{
			int num = this.FindTabPage(tabPage);
			this.SetTabPage(num, tabPage, tabPage.GetTCITEM());
			this.cachedDisplayRect = Rectangle.Empty;
			this.UpdateTabSelection(false);
		}

		// Token: 0x06003B97 RID: 15255 RVA: 0x00104FC8 File Offset: 0x001031C8
		private void WmNeedText(ref Message m)
		{
			NativeMethods.TOOLTIPTEXT tooltiptext = (NativeMethods.TOOLTIPTEXT)m.GetLParam(typeof(NativeMethods.TOOLTIPTEXT));
			int num = (int)tooltiptext.hdr.idFrom;
			string toolTipText = this.GetToolTipText(this.GetTabPage(num));
			if (!string.IsNullOrEmpty(toolTipText))
			{
				tooltiptext.lpszText = toolTipText;
			}
			else
			{
				tooltiptext.lpszText = this.controlTipText;
			}
			tooltiptext.hinst = IntPtr.Zero;
			if (this.RightToLeft == RightToLeft.Yes)
			{
				tooltiptext.uFlags |= 4;
			}
			Marshal.StructureToPtr(tooltiptext, m.LParam, false);
		}

		// Token: 0x06003B98 RID: 15256 RVA: 0x00105058 File Offset: 0x00103258
		private void WmReflectDrawItem(ref Message m)
		{
			NativeMethods.DRAWITEMSTRUCT drawitemstruct = (NativeMethods.DRAWITEMSTRUCT)m.GetLParam(typeof(NativeMethods.DRAWITEMSTRUCT));
			IntPtr intPtr = Control.SetUpPalette(drawitemstruct.hDC, false, false);
			using (Graphics graphics = Graphics.FromHdcInternal(drawitemstruct.hDC))
			{
				this.OnDrawItem(new DrawItemEventArgs(graphics, this.Font, Rectangle.FromLTRB(drawitemstruct.rcItem.left, drawitemstruct.rcItem.top, drawitemstruct.rcItem.right, drawitemstruct.rcItem.bottom), drawitemstruct.itemID, (DrawItemState)drawitemstruct.itemState));
			}
			if (intPtr != IntPtr.Zero)
			{
				SafeNativeMethods.SelectPalette(new HandleRef(null, drawitemstruct.hDC), new HandleRef(null, intPtr), 0);
			}
			m.Result = (IntPtr)1;
		}

		// Token: 0x06003B99 RID: 15257 RVA: 0x00105134 File Offset: 0x00103334
		private bool WmSelChange()
		{
			TabControlCancelEventArgs tabControlCancelEventArgs = new TabControlCancelEventArgs(this.SelectedTab, this.SelectedIndex, false, TabControlAction.Selecting);
			this.OnSelecting(tabControlCancelEventArgs);
			if (!tabControlCancelEventArgs.Cancel)
			{
				this.OnSelected(new TabControlEventArgs(this.SelectedTab, this.SelectedIndex, TabControlAction.Selected));
				this.OnSelectedIndexChanged(EventArgs.Empty);
			}
			else
			{
				base.SendMessage(4876, this.lastSelection, 0);
				this.UpdateTabSelection(true);
			}
			return tabControlCancelEventArgs.Cancel;
		}

		// Token: 0x06003B9A RID: 15258 RVA: 0x001051AC File Offset: 0x001033AC
		private bool WmSelChanging()
		{
			IContainerControl containerControlInternal = base.GetContainerControlInternal();
			if (containerControlInternal != null && !base.DesignMode)
			{
				if (containerControlInternal is ContainerControl)
				{
					((ContainerControl)containerControlInternal).SetActiveControlInternal(this);
				}
				else
				{
					IntSecurity.ModifyFocus.Assert();
					try
					{
						containerControlInternal.ActiveControl = this;
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
			}
			this.lastSelection = this.SelectedIndex;
			TabControlCancelEventArgs tabControlCancelEventArgs = new TabControlCancelEventArgs(this.SelectedTab, this.SelectedIndex, false, TabControlAction.Deselecting);
			this.OnDeselecting(tabControlCancelEventArgs);
			if (!tabControlCancelEventArgs.Cancel)
			{
				this.OnDeselected(new TabControlEventArgs(this.SelectedTab, this.SelectedIndex, TabControlAction.Deselected));
			}
			return tabControlCancelEventArgs.Cancel;
		}

		// Token: 0x06003B9B RID: 15259 RVA: 0x00105258 File Offset: 0x00103458
		private void WmTabBaseReLayout(ref Message m)
		{
			this.BeginUpdate();
			this.cachedDisplayRect = Rectangle.Empty;
			this.UpdateTabSelection(false);
			this.EndUpdate();
			base.Invalidate(true);
			NativeMethods.MSG msg = default(NativeMethods.MSG);
			IntPtr handle = base.Handle;
			while (UnsafeNativeMethods.PeekMessage(ref msg, new HandleRef(this, handle), this.tabBaseReLayoutMessage, this.tabBaseReLayoutMessage, 1))
			{
			}
		}

		/// <summary>This member overrides <see cref="M:System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@)" />.</summary>
		/// <param name="m">A Windows Message Object.</param>
		// Token: 0x06003B9C RID: 15260 RVA: 0x001052B8 File Offset: 0x001034B8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg <= 8235)
			{
				if (msg != 78)
				{
					if (msg != 8235)
					{
						goto IL_161;
					}
					this.WmReflectDrawItem(ref m);
					goto IL_161;
				}
			}
			else
			{
				if (msg == 8236)
				{
					goto IL_161;
				}
				if (msg != 8270)
				{
					goto IL_161;
				}
			}
			NativeMethods.NMHDR nmhdr = (NativeMethods.NMHDR)m.GetLParam(typeof(NativeMethods.NMHDR));
			int code = nmhdr.code;
			if (code <= -551)
			{
				if (code != -552)
				{
					if (code == -551)
					{
						if (this.WmSelChange())
						{
							m.Result = (IntPtr)1;
							this.tabControlState[32] = false;
							return;
						}
						this.tabControlState[32] = true;
					}
				}
				else
				{
					if (this.WmSelChanging())
					{
						m.Result = (IntPtr)1;
						this.tabControlState[32] = false;
						return;
					}
					if (base.ValidationCancelled)
					{
						m.Result = (IntPtr)1;
						this.tabControlState[32] = false;
						return;
					}
					this.tabControlState[32] = true;
				}
			}
			else if (code == -530 || code == -520)
			{
				UnsafeNativeMethods.SendMessage(new HandleRef(nmhdr, nmhdr.hwndFrom), 1048, 0, SystemInformation.MaxWindowTrackSize.Width);
				this.WmNeedText(ref m);
				m.Result = (IntPtr)1;
				return;
			}
			IL_161:
			if (m.Msg == this.tabBaseReLayoutMessage)
			{
				this.WmTabBaseReLayout(ref m);
				return;
			}
			base.WndProc(ref m);
		}

		// Token: 0x04002340 RID: 9024
		private static readonly Size DEFAULT_ITEMSIZE = Size.Empty;

		// Token: 0x04002341 RID: 9025
		private static readonly Point DEFAULT_PADDING = new Point(6, 3);

		// Token: 0x04002342 RID: 9026
		private TabControl.TabPageCollection tabCollection;

		// Token: 0x04002343 RID: 9027
		private TabAlignment alignment;

		// Token: 0x04002344 RID: 9028
		private TabDrawMode drawMode;

		// Token: 0x04002345 RID: 9029
		private ImageList imageList;

		// Token: 0x04002346 RID: 9030
		private Size itemSize = TabControl.DEFAULT_ITEMSIZE;

		// Token: 0x04002347 RID: 9031
		private Point padding = TabControl.DEFAULT_PADDING;

		// Token: 0x04002348 RID: 9032
		private TabSizeMode sizeMode;

		// Token: 0x04002349 RID: 9033
		private TabAppearance appearance;

		// Token: 0x0400234A RID: 9034
		private Rectangle cachedDisplayRect = Rectangle.Empty;

		// Token: 0x0400234B RID: 9035
		private bool currentlyScaling;

		// Token: 0x0400234C RID: 9036
		private int selectedIndex = -1;

		// Token: 0x0400234D RID: 9037
		private Size cachedSize = Size.Empty;

		// Token: 0x0400234E RID: 9038
		private string controlTipText = string.Empty;

		// Token: 0x0400234F RID: 9039
		private bool handleInTable;

		// Token: 0x04002350 RID: 9040
		private EventHandler onSelectedIndexChanged;

		// Token: 0x04002351 RID: 9041
		private DrawItemEventHandler onDrawItem;

		// Token: 0x04002352 RID: 9042
		private static readonly object EVENT_DESELECTING = new object();

		// Token: 0x04002353 RID: 9043
		private static readonly object EVENT_DESELECTED = new object();

		// Token: 0x04002354 RID: 9044
		private static readonly object EVENT_SELECTING = new object();

		// Token: 0x04002355 RID: 9045
		private static readonly object EVENT_SELECTED = new object();

		// Token: 0x04002356 RID: 9046
		private static readonly object EVENT_RIGHTTOLEFTLAYOUTCHANGED = new object();

		// Token: 0x04002357 RID: 9047
		private const int TABCONTROLSTATE_hotTrack = 1;

		// Token: 0x04002358 RID: 9048
		private const int TABCONTROLSTATE_multiline = 2;

		// Token: 0x04002359 RID: 9049
		private const int TABCONTROLSTATE_showToolTips = 4;

		// Token: 0x0400235A RID: 9050
		private const int TABCONTROLSTATE_getTabRectfromItemSize = 8;

		// Token: 0x0400235B RID: 9051
		private const int TABCONTROLSTATE_fromCreateHandles = 16;

		// Token: 0x0400235C RID: 9052
		private const int TABCONTROLSTATE_UISelection = 32;

		// Token: 0x0400235D RID: 9053
		private const int TABCONTROLSTATE_selectFirstControl = 64;

		// Token: 0x0400235E RID: 9054
		private const int TABCONTROLSTATE_insertingItem = 128;

		// Token: 0x0400235F RID: 9055
		private const int TABCONTROLSTATE_autoSize = 256;

		// Token: 0x04002360 RID: 9056
		private BitVector32 tabControlState;

		// Token: 0x04002361 RID: 9057
		private readonly int tabBaseReLayoutMessage = SafeNativeMethods.RegisterWindowMessage(Application.WindowMessagesVersion + "_TabBaseReLayout");

		// Token: 0x04002362 RID: 9058
		private TabPage[] tabPages;

		// Token: 0x04002363 RID: 9059
		private int tabPageCount;

		// Token: 0x04002364 RID: 9060
		private int lastSelection;

		// Token: 0x04002365 RID: 9061
		private bool rightToLeftLayout;

		// Token: 0x04002366 RID: 9062
		private bool skipUpdateSize;

		/// <summary>Contains a collection of <see cref="T:System.Windows.Forms.TabPage" /> objects.</summary>
		// Token: 0x020007EC RID: 2028
		public class TabPageCollection : IList, ICollection, IEnumerable
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TabControl.TabPageCollection" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.TabControl" /> that this collection belongs to.</param>
			/// <exception cref="T:System.ArgumentNullException">The specified <see cref="T:System.Windows.Forms.TabControl" /> is <see langword="null" />.</exception>
			// Token: 0x06006E09 RID: 28169 RVA: 0x001931B6 File Offset: 0x001913B6
			public TabPageCollection(TabControl owner)
			{
				if (owner == null)
				{
					throw new ArgumentNullException("owner");
				}
				this.owner = owner;
			}

			/// <summary>Gets or sets a <see cref="T:System.Windows.Forms.TabPage" /> in the collection.</summary>
			/// <param name="index">The zero-based index of the tab page to get or set.</param>
			/// <returns>The <see cref="T:System.Windows.Forms.TabPage" /> at the specified index.</returns>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than zero or greater than the highest available index.</exception>
			// Token: 0x17001810 RID: 6160
			public virtual TabPage this[int index]
			{
				get
				{
					return this.owner.GetTabPage(index);
				}
				set
				{
					this.owner.SetTabPage(index, value, value.GetTCITEM());
				}
			}

			/// <summary>Gets or sets a <see cref="T:System.Windows.Forms.TabPage" /> in the collection.</summary>
			/// <param name="index">The zero-based index of the element to get.</param>
			/// <returns>The <see cref="T:System.Windows.Forms.TabPage" /> at the specified index.</returns>
			/// <exception cref="T:System.ArgumentException">The value is not a <see cref="T:System.Windows.Forms.TabPage" />.</exception>
			// Token: 0x17001811 RID: 6161
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					if (value is TabPage)
					{
						this[index] = (TabPage)value;
						return;
					}
					throw new ArgumentException("value");
				}
			}

			/// <summary>Gets a tab page with the specified key from the collection.</summary>
			/// <param name="key">The name of the tab page to retrieve.</param>
			/// <returns>The <see cref="T:System.Windows.Forms.TabPage" /> with the specified key.</returns>
			// Token: 0x17001812 RID: 6162
			public virtual TabPage this[string key]
			{
				get
				{
					if (string.IsNullOrEmpty(key))
					{
						return null;
					}
					int num = this.IndexOfKey(key);
					if (this.IsValidIndex(num))
					{
						return this[num];
					}
					return null;
				}
			}

			/// <summary>Gets the number of tab pages in the collection.</summary>
			/// <returns>The number of tab pages in the collection.</returns>
			// Token: 0x17001813 RID: 6163
			// (get) Token: 0x06006E0F RID: 28175 RVA: 0x00193259 File Offset: 0x00191459
			[Browsable(false)]
			public int Count
			{
				get
				{
					return this.owner.tabPageCount;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.TabControl.TabPageCollection" />.</summary>
			/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.TabControl.TabPageCollection" />.</returns>
			// Token: 0x17001814 RID: 6164
			// (get) Token: 0x06006E10 RID: 28176 RVA: 0x00006A49 File Offset: 0x00004C49
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>Gets a value indicating whether access to the <see cref="T:System.Windows.Forms.TabControl.TabPageCollection" /> is synchronized (thread safe).</summary>
			/// <returns>
			///   <see langword="false" /> in all cases.</returns>
			// Token: 0x17001815 RID: 6165
			// (get) Token: 0x06006E11 RID: 28177 RVA: 0x0001180C File Offset: 0x0000FA0C
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.TabControl.TabPageCollection" /> has a fixed size.</summary>
			/// <returns>
			///   <see langword="false" /> in all cases.</returns>
			// Token: 0x17001816 RID: 6166
			// (get) Token: 0x06006E12 RID: 28178 RVA: 0x0001180C File Offset: 0x0000FA0C
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>This property always returns <see langword="false" />.</returns>
			// Token: 0x17001817 RID: 6167
			// (get) Token: 0x06006E13 RID: 28179 RVA: 0x0001180C File Offset: 0x0000FA0C
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			/// <summary>Adds a <see cref="T:System.Windows.Forms.TabPage" /> to the collection.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.TabPage" /> to add.</param>
			/// <exception cref="T:System.ArgumentNullException">The specified <paramref name="value" /> is <see langword="null" />.</exception>
			// Token: 0x06006E14 RID: 28180 RVA: 0x00193266 File Offset: 0x00191466
			public void Add(TabPage value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.owner.Controls.Add(value);
			}

			/// <summary>Adds a <see cref="T:System.Windows.Forms.TabPage" /> control to the collection.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.TabPage" /> to add to the collection.</param>
			/// <returns>The position into which the <see cref="T:System.Windows.Forms.TabPage" /> was inserted.</returns>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.TabPage" />.</exception>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="value" /> is <see langword="null" />.</exception>
			// Token: 0x06006E15 RID: 28181 RVA: 0x00193287 File Offset: 0x00191487
			int IList.Add(object value)
			{
				if (value is TabPage)
				{
					this.Add((TabPage)value);
					return this.IndexOf((TabPage)value);
				}
				throw new ArgumentException("value");
			}

			/// <summary>Creates a tab page with the specified text, and adds it to the collection.</summary>
			/// <param name="text">The text to display on the tab page.</param>
			// Token: 0x06006E16 RID: 28182 RVA: 0x001932B4 File Offset: 0x001914B4
			public void Add(string text)
			{
				this.Add(new TabPage
				{
					Text = text
				});
			}

			/// <summary>Creates a tab page with the specified text and key, and adds it to the collection.</summary>
			/// <param name="key">The name of the tab page.</param>
			/// <param name="text">The text to display on the tab page.</param>
			// Token: 0x06006E17 RID: 28183 RVA: 0x001932D8 File Offset: 0x001914D8
			public void Add(string key, string text)
			{
				this.Add(new TabPage
				{
					Name = key,
					Text = text
				});
			}

			/// <summary>Creates a tab page with the specified key, text, and image, and adds it to the collection.</summary>
			/// <param name="key">The name of the tab page.</param>
			/// <param name="text">The text to display on the tab page.</param>
			/// <param name="imageIndex">The index of the image to display on the tab page.</param>
			// Token: 0x06006E18 RID: 28184 RVA: 0x00193300 File Offset: 0x00191500
			public void Add(string key, string text, int imageIndex)
			{
				this.Add(new TabPage
				{
					Name = key,
					Text = text,
					ImageIndex = imageIndex
				});
			}

			/// <summary>Creates a tab page with the specified key, text, and image, and adds it to the collection.</summary>
			/// <param name="key">The name of the tab page.</param>
			/// <param name="text">The text to display on the tab page.</param>
			/// <param name="imageKey">The key of the image to display on the tab page.</param>
			// Token: 0x06006E19 RID: 28185 RVA: 0x00193330 File Offset: 0x00191530
			public void Add(string key, string text, string imageKey)
			{
				this.Add(new TabPage
				{
					Name = key,
					Text = text,
					ImageKey = imageKey
				});
			}

			/// <summary>Adds a set of tab pages to the collection.</summary>
			/// <param name="pages">An array of type <see cref="T:System.Windows.Forms.TabPage" /> that contains the tab pages to add.</param>
			/// <exception cref="T:System.ArgumentNullException">The value of pages equals <see langword="null" />.</exception>
			// Token: 0x06006E1A RID: 28186 RVA: 0x00193360 File Offset: 0x00191560
			public void AddRange(TabPage[] pages)
			{
				if (pages == null)
				{
					throw new ArgumentNullException("pages");
				}
				foreach (TabPage tabPage in pages)
				{
					this.Add(tabPage);
				}
			}

			/// <summary>Determines whether a specified tab page is in the collection.</summary>
			/// <param name="page">The <see cref="T:System.Windows.Forms.TabPage" /> to locate in the collection.</param>
			/// <returns>
			///   <see langword="true" /> if the specified <see cref="T:System.Windows.Forms.TabPage" /> is in the collection; otherwise, <see langword="false" />.</returns>
			/// <exception cref="T:System.ArgumentNullException">The value of <paramref name="page" /> is <see langword="null" />.</exception>
			// Token: 0x06006E1B RID: 28187 RVA: 0x00193396 File Offset: 0x00191596
			public bool Contains(TabPage page)
			{
				if (page == null)
				{
					throw new ArgumentNullException("value");
				}
				return this.IndexOf(page) != -1;
			}

			/// <summary>Determines whether the specified <see cref="T:System.Windows.Forms.TabPage" /> control is in the <see cref="T:System.Windows.Forms.TabControl.TabPageCollection" />.</summary>
			/// <param name="page">The object to locate in the collection.</param>
			/// <returns>
			///   <see langword="true" /> if the specified object is a <see cref="T:System.Windows.Forms.TabPage" /> in the collection; otherwise, <see langword="false" />.</returns>
			// Token: 0x06006E1C RID: 28188 RVA: 0x001933B3 File Offset: 0x001915B3
			bool IList.Contains(object page)
			{
				return page is TabPage && this.Contains((TabPage)page);
			}

			/// <summary>Determines whether the collection contains a tab page with the specified key.</summary>
			/// <param name="key">The name of the tab page to search for.</param>
			/// <returns>
			///   <see langword="true" /> to indicate a tab page with the specified key was found in the collection; otherwise, <see langword="false" />.</returns>
			// Token: 0x06006E1D RID: 28189 RVA: 0x001933CB File Offset: 0x001915CB
			public virtual bool ContainsKey(string key)
			{
				return this.IsValidIndex(this.IndexOfKey(key));
			}

			/// <summary>Returns the index of the specified tab page in the collection.</summary>
			/// <param name="page">The <see cref="T:System.Windows.Forms.TabPage" /> to locate in the collection.</param>
			/// <returns>The zero-based index of the tab page; -1 if it cannot be found.</returns>
			/// <exception cref="T:System.ArgumentNullException">The value of <paramref name="page" /> is <see langword="null" />.</exception>
			// Token: 0x06006E1E RID: 28190 RVA: 0x001933DC File Offset: 0x001915DC
			public int IndexOf(TabPage page)
			{
				if (page == null)
				{
					throw new ArgumentNullException("value");
				}
				for (int i = 0; i < this.Count; i++)
				{
					if (this[i] == page)
					{
						return i;
					}
				}
				return -1;
			}

			/// <summary>Returns the index of the specified <see cref="T:System.Windows.Forms.TabPage" /> control in the collection.</summary>
			/// <param name="page">The <see cref="T:System.Windows.Forms.TabPage" /> to locate in the collection.</param>
			/// <returns>The zero-based index if page is a <see cref="T:System.Windows.Forms.TabPage" /> in the collection; otherwise -1.</returns>
			// Token: 0x06006E1F RID: 28191 RVA: 0x00193415 File Offset: 0x00191615
			int IList.IndexOf(object page)
			{
				if (page is TabPage)
				{
					return this.IndexOf((TabPage)page);
				}
				return -1;
			}

			/// <summary>Returns the index of the first occurrence of the <see cref="T:System.Windows.Forms.TabPage" /> with the specified key.</summary>
			/// <param name="key">The name of the tab page to find in the collection.</param>
			/// <returns>The zero-based index of the first occurrence of a tab page with the specified key, if found; otherwise, -1.</returns>
			// Token: 0x06006E20 RID: 28192 RVA: 0x00193430 File Offset: 0x00191630
			public virtual int IndexOfKey(string key)
			{
				if (string.IsNullOrEmpty(key))
				{
					return -1;
				}
				if (this.IsValidIndex(this.lastAccessedIndex) && WindowsFormsUtils.SafeCompareStrings(this[this.lastAccessedIndex].Name, key, true))
				{
					return this.lastAccessedIndex;
				}
				for (int i = 0; i < this.Count; i++)
				{
					if (WindowsFormsUtils.SafeCompareStrings(this[i].Name, key, true))
					{
						this.lastAccessedIndex = i;
						return i;
					}
				}
				this.lastAccessedIndex = -1;
				return -1;
			}

			/// <summary>Inserts an existing tab page into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the tab page is inserted.</param>
			/// <param name="tabPage">The <see cref="T:System.Windows.Forms.TabPage" /> to insert in the collection.</param>
			// Token: 0x06006E21 RID: 28193 RVA: 0x001934B0 File Offset: 0x001916B0
			public void Insert(int index, TabPage tabPage)
			{
				this.owner.InsertItem(index, tabPage);
				try
				{
					this.owner.InsertingItem = true;
					this.owner.Controls.Add(tabPage);
				}
				finally
				{
					this.owner.InsertingItem = false;
				}
				this.owner.Controls.SetChildIndex(tabPage, index);
			}

			/// <summary>Inserts a <see cref="T:System.Windows.Forms.TabPage" /> control into the collection.</summary>
			/// <param name="index">The zero-based index at which the <see cref="T:System.Windows.Forms.TabPage" /> should be inserted.</param>
			/// <param name="tabPage">The <see cref="T:System.Windows.Forms.TabPage" /> to insert into the <see cref="T:System.Windows.Forms.TabControl.TabPageCollection" />.</param>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="tabPage" /> is not a <see cref="T:System.Windows.Forms.TabPage" />.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0, or index is greater than or equal to <see cref="P:System.Windows.Forms.TabControl.TabPageCollection.Count" />.</exception>
			// Token: 0x06006E22 RID: 28194 RVA: 0x00193518 File Offset: 0x00191718
			void IList.Insert(int index, object tabPage)
			{
				if (tabPage is TabPage)
				{
					this.Insert(index, (TabPage)tabPage);
					return;
				}
				throw new ArgumentException("tabPage");
			}

			/// <summary>Creates a new tab page with the specified text and inserts it into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the tab page is inserted.</param>
			/// <param name="text">The text to display in the tab page.</param>
			// Token: 0x06006E23 RID: 28195 RVA: 0x0019353C File Offset: 0x0019173C
			public void Insert(int index, string text)
			{
				this.Insert(index, new TabPage
				{
					Text = text
				});
			}

			/// <summary>Creates a new tab page with the specified key and text, and inserts it into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the tab page is inserted.</param>
			/// <param name="key">The name of the tab page.</param>
			/// <param name="text">The text to display on the tab page.</param>
			// Token: 0x06006E24 RID: 28196 RVA: 0x00193560 File Offset: 0x00191760
			public void Insert(int index, string key, string text)
			{
				this.Insert(index, new TabPage
				{
					Name = key,
					Text = text
				});
			}

			/// <summary>Creates a new tab page with the specified key, text, and image, and inserts it into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the tab page is inserted</param>
			/// <param name="key">The name of the tab page.</param>
			/// <param name="text">The text to display on the tab page</param>
			/// <param name="imageIndex">The zero-based index of the image to display on the tab page.</param>
			// Token: 0x06006E25 RID: 28197 RVA: 0x0019358C File Offset: 0x0019178C
			public void Insert(int index, string key, string text, int imageIndex)
			{
				TabPage tabPage = new TabPage();
				tabPage.Name = key;
				tabPage.Text = text;
				this.Insert(index, tabPage);
				tabPage.ImageIndex = imageIndex;
			}

			/// <summary>Creates a tab page with the specified key, text, and image, and inserts it into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the tab page is inserted.</param>
			/// <param name="key">The name of the tab page.</param>
			/// <param name="text">The text to display on the tab page.</param>
			/// <param name="imageKey">The key of the image to display on the tab page.</param>
			// Token: 0x06006E26 RID: 28198 RVA: 0x001935C0 File Offset: 0x001917C0
			public void Insert(int index, string key, string text, string imageKey)
			{
				TabPage tabPage = new TabPage();
				tabPage.Name = key;
				tabPage.Text = text;
				this.Insert(index, tabPage);
				tabPage.ImageKey = imageKey;
			}

			// Token: 0x06006E27 RID: 28199 RVA: 0x001935F1 File Offset: 0x001917F1
			private bool IsValidIndex(int index)
			{
				return index >= 0 && index < this.Count;
			}

			/// <summary>Removes all the tab pages from the collection.</summary>
			// Token: 0x06006E28 RID: 28200 RVA: 0x00193602 File Offset: 0x00191802
			public virtual void Clear()
			{
				this.owner.RemoveAll();
			}

			/// <summary>Copies the elements of the collection to the specified array, starting at the specified index.</summary>
			/// <param name="dest">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in the array at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="dest" /> is <see langword="null" />.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than zero.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="dest" /> is multidimensional.  
			/// -or-  
			/// The number of elements in the <see cref="T:System.Windows.Forms.TabControl.TabPageCollection" /> is greater than the available space from index to the end of <paramref name="dest" />.</exception>
			/// <exception cref="T:System.InvalidCastException">The items in the <see cref="T:System.Windows.Forms.TabControl.TabPageCollection" /> cannot be cast automatically to the type of <paramref name="dest" />.</exception>
			// Token: 0x06006E29 RID: 28201 RVA: 0x0019360F File Offset: 0x0019180F
			void ICollection.CopyTo(Array dest, int index)
			{
				if (this.Count > 0)
				{
					Array.Copy(this.owner.GetTabPages(), 0, dest, index, this.Count);
				}
			}

			/// <summary>Returns an enumeration of all the tab pages in the collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Windows.Forms.TabControl.TabPageCollection" />.</returns>
			// Token: 0x06006E2A RID: 28202 RVA: 0x00193634 File Offset: 0x00191834
			public IEnumerator GetEnumerator()
			{
				TabPage[] tabPages = this.owner.GetTabPages();
				if (tabPages != null)
				{
					return tabPages.GetEnumerator();
				}
				return new TabPage[0].GetEnumerator();
			}

			/// <summary>Removes a <see cref="T:System.Windows.Forms.TabPage" /> from the collection.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.TabPage" /> to remove.</param>
			/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
			// Token: 0x06006E2B RID: 28203 RVA: 0x00193662 File Offset: 0x00191862
			public void Remove(TabPage value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.owner.Controls.Remove(value);
			}

			/// <summary>Removes a <see cref="T:System.Windows.Forms.TabPage" /> from the collection.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.TabPage" /> to remove.</param>
			// Token: 0x06006E2C RID: 28204 RVA: 0x00193683 File Offset: 0x00191883
			void IList.Remove(object value)
			{
				if (value is TabPage)
				{
					this.Remove((TabPage)value);
				}
			}

			/// <summary>Removes the tab page at the specified index from the collection.</summary>
			/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.TabPage" /> to remove.</param>
			// Token: 0x06006E2D RID: 28205 RVA: 0x00193699 File Offset: 0x00191899
			public void RemoveAt(int index)
			{
				this.owner.Controls.RemoveAt(index);
			}

			/// <summary>Removes the tab page with the specified key from the collection.</summary>
			/// <param name="key">The name of the tab page to remove.</param>
			// Token: 0x06006E2E RID: 28206 RVA: 0x001936AC File Offset: 0x001918AC
			public virtual void RemoveByKey(string key)
			{
				int num = this.IndexOfKey(key);
				if (this.IsValidIndex(num))
				{
					this.RemoveAt(num);
				}
			}

			// Token: 0x040042CB RID: 17099
			private TabControl owner;

			// Token: 0x040042CC RID: 17100
			private int lastAccessedIndex = -1;
		}

		/// <summary>Contains a collection of <see cref="T:System.Windows.Forms.Control" /> objects.</summary>
		// Token: 0x020007ED RID: 2029
		[ComVisible(false)]
		public new class ControlCollection : Control.ControlCollection
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TabControl.ControlCollection" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.TabControl" /> that this collection belongs to.</param>
			// Token: 0x06006E2F RID: 28207 RVA: 0x001936D1 File Offset: 0x001918D1
			public ControlCollection(TabControl owner)
				: base(owner)
			{
				this.owner = owner;
			}

			/// <summary>Adds a <see cref="T:System.Windows.Forms.Control" /> to the collection.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.Control" /> to add.</param>
			/// <exception cref="T:System.Exception">The specified <see cref="T:System.Windows.Forms.Control" /> is a <see cref="T:System.Windows.Forms.TabPage" />.</exception>
			// Token: 0x06006E30 RID: 28208 RVA: 0x001936E4 File Offset: 0x001918E4
			public override void Add(Control value)
			{
				if (!(value is TabPage))
				{
					throw new ArgumentException(SR.GetString("TabControlInvalidTabPageType", new object[] { value.GetType().Name }));
				}
				TabPage tabPage = (TabPage)value;
				if (!this.owner.InsertingItem)
				{
					if (this.owner.IsHandleCreated)
					{
						this.owner.AddTabPage(tabPage, tabPage.GetTCITEM());
					}
					else
					{
						this.owner.Insert(this.owner.TabCount, tabPage);
					}
				}
				base.Add(tabPage);
				tabPage.Visible = false;
				if (this.owner.IsHandleCreated)
				{
					tabPage.Bounds = this.owner.DisplayRectangle;
				}
				ISite site = this.owner.Site;
				if (site != null && tabPage.Site == null)
				{
					IContainer container = site.Container;
					if (container != null)
					{
						container.Add(tabPage);
					}
				}
				this.owner.ApplyItemSize();
				this.owner.UpdateTabSelection(false);
			}

			/// <summary>Removes a <see cref="T:System.Windows.Forms.Control" /> from the collection.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.Control" /> to remove.</param>
			// Token: 0x06006E31 RID: 28209 RVA: 0x001937D8 File Offset: 0x001919D8
			public override void Remove(Control value)
			{
				base.Remove(value);
				if (!(value is TabPage))
				{
					return;
				}
				int num = this.owner.FindTabPage((TabPage)value);
				int selectedIndex = this.owner.SelectedIndex;
				if (num != -1)
				{
					this.owner.RemoveTabPage(num);
					if (num == selectedIndex)
					{
						this.owner.SelectedIndex = 0;
					}
				}
				this.owner.UpdateTabSelection(false);
			}

			// Token: 0x040042CD RID: 17101
			private TabControl owner;
		}
	}
}
