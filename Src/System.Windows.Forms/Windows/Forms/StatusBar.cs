using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows status bar control. Although <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> replaces and adds functionality to the <see cref="T:System.Windows.Forms.StatusBar" /> control of previous versions, <see cref="T:System.Windows.Forms.StatusBar" /> is retained for both backward compatibility and future use if you choose.</summary>
	// Token: 0x02000373 RID: 883
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultEvent("PanelClick")]
	[DefaultProperty("Text")]
	[Designer("System.Windows.Forms.Design.StatusBarDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class StatusBar : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.StatusBar" /> class.</summary>
		// Token: 0x060039D3 RID: 14803 RVA: 0x000FFD74 File Offset: 0x000FDF74
		public StatusBar()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.Selectable, false);
			this.Dock = DockStyle.Bottom;
			this.TabStop = false;
		}

		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x060039D4 RID: 14804 RVA: 0x000FFDC0 File Offset: 0x000FDFC0
		private static VisualStyleRenderer VisualStyleRenderer
		{
			get
			{
				if (VisualStyleRenderer.IsSupported)
				{
					if (StatusBar.renderer == null)
					{
						StatusBar.renderer = new VisualStyleRenderer(VisualStyleElement.ToolBar.Button.Normal);
					}
				}
				else
				{
					StatusBar.renderer = null;
				}
				return StatusBar.renderer;
			}
		}

		// Token: 0x17000DC7 RID: 3527
		// (get) Token: 0x060039D5 RID: 14805 RVA: 0x000FFDEC File Offset: 0x000FDFEC
		private int SizeGripWidth
		{
			get
			{
				if (this.sizeGripWidth == 0)
				{
					if (Application.RenderWithVisualStyles && StatusBar.VisualStyleRenderer != null)
					{
						VisualStyleRenderer visualStyleRenderer = StatusBar.VisualStyleRenderer;
						VisualStyleElement visualStyleElement = VisualStyleElement.Status.GripperPane.Normal;
						visualStyleRenderer.SetParameters(visualStyleElement);
						this.sizeGripWidth = visualStyleRenderer.GetPartSize(Graphics.FromHwndInternal(base.Handle), ThemeSizeType.True).Width;
						visualStyleElement = VisualStyleElement.Status.Gripper.Normal;
						visualStyleRenderer.SetParameters(visualStyleElement);
						Size partSize = visualStyleRenderer.GetPartSize(Graphics.FromHwndInternal(base.Handle), ThemeSizeType.True);
						this.sizeGripWidth += partSize.Width;
						this.sizeGripWidth = Math.Max(this.sizeGripWidth, 16);
					}
					else
					{
						this.sizeGripWidth = 16;
					}
				}
				return this.sizeGripWidth;
			}
		}

		/// <summary>Gets or sets the background color for the <see cref="T:System.Windows.Forms.StatusBar" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Color" /> that is the background color of the control</returns>
		// Token: 0x17000DC8 RID: 3528
		// (get) Token: 0x060039D6 RID: 14806 RVA: 0x00030577 File Offset: 0x0002E777
		// (set) Token: 0x060039D7 RID: 14807 RVA: 0x000070A6 File Offset: 0x000052A6
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.StatusBar.BackColor" /> property changes.</summary>
		// Token: 0x140002C8 RID: 712
		// (add) Token: 0x060039D8 RID: 14808 RVA: 0x00058BF2 File Offset: 0x00056DF2
		// (remove) Token: 0x060039D9 RID: 14809 RVA: 0x00058BFB File Offset: 0x00056DFB
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

		/// <summary>Gets or sets the background image for the <see cref="T:System.Windows.Forms.StatusBar" />.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> that is the background image for the <see cref="T:System.Windows.Forms.StatusBar" />.</returns>
		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x060039DA RID: 14810 RVA: 0x0001187C File Offset: 0x0000FA7C
		// (set) Token: 0x060039DB RID: 14811 RVA: 0x00011884 File Offset: 0x0000FA84
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.StatusBar.BackgroundImage" /> property is changed.</summary>
		// Token: 0x140002C9 RID: 713
		// (add) Token: 0x060039DC RID: 14812 RVA: 0x0001188D File Offset: 0x0000FA8D
		// (remove) Token: 0x060039DD RID: 14813 RVA: 0x00011896 File Offset: 0x0000FA96
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

		/// <summary>Gets or sets the layout of the background image of the <see cref="T:System.Windows.Forms.StatusBar" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImageLayout" /> values.</returns>
		// Token: 0x17000DCA RID: 3530
		// (get) Token: 0x060039DE RID: 14814 RVA: 0x0001189F File Offset: 0x0000FA9F
		// (set) Token: 0x060039DF RID: 14815 RVA: 0x000118A7 File Offset: 0x0000FAA7
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.StatusBar.BackgroundImageLayout" /> property changes.</summary>
		// Token: 0x140002CA RID: 714
		// (add) Token: 0x060039E0 RID: 14816 RVA: 0x000118B0 File Offset: 0x0000FAB0
		// (remove) Token: 0x060039E1 RID: 14817 RVA: 0x000118B9 File Offset: 0x0000FAB9
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

		/// <summary>Gets the <see cref="T:System.Windows.Forms.CreateParams" /> used to create the handle for this control.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.CreateParams" /> used to create the handle for this control.</returns>
		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x060039E2 RID: 14818 RVA: 0x000FFEA0 File Offset: 0x000FE0A0
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ClassName = "msctls_statusbar32";
				if (this.sizeGrip)
				{
					createParams.Style |= 256;
				}
				else
				{
					createParams.Style &= -257;
				}
				createParams.Style |= 12;
				return createParams;
			}
		}

		/// <summary>Gets the default Input Method Editor (IME) mode supported by this control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x060039E3 RID: 14819 RVA: 0x00023BD7 File Offset: 0x00021DD7
		protected override ImeMode DefaultImeMode
		{
			get
			{
				return ImeMode.Disable;
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the size of the control.</returns>
		// Token: 0x17000DCD RID: 3533
		// (get) Token: 0x060039E4 RID: 14820 RVA: 0x00010FC8 File Offset: 0x0000F1C8
		protected override Size DefaultSize
		{
			get
			{
				return new Size(100, 22);
			}
		}

		/// <summary>Gets or sets a value indicating whether this control should redraw its surface using a secondary buffer to reduce or prevent flicker, however this property has no effect on the <see cref="T:System.Windows.Forms.StatusBar" /> control</summary>
		/// <returns>
		///   <see langword="true" /> if the control has a secondary buffer; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x060039E5 RID: 14821 RVA: 0x00012FC3 File Offset: 0x000111C3
		// (set) Token: 0x060039E6 RID: 14822 RVA: 0x00012FCB File Offset: 0x000111CB
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

		/// <summary>Gets or sets the docking behavior of the <see cref="T:System.Windows.Forms.StatusBar" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values. The default is <see langword="Bottom" />.</returns>
		// Token: 0x17000DCF RID: 3535
		// (get) Token: 0x060039E7 RID: 14823 RVA: 0x000FC41E File Offset: 0x000FA61E
		// (set) Token: 0x060039E8 RID: 14824 RVA: 0x000FFC4E File Offset: 0x000FDE4E
		[Localizable(true)]
		[DefaultValue(DockStyle.Bottom)]
		public override DockStyle Dock
		{
			get
			{
				return base.Dock;
			}
			set
			{
				base.Dock = value;
			}
		}

		/// <summary>Gets or sets the font the <see cref="T:System.Windows.Forms.StatusBar" /> control will use to display information.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> of the text. The default is the font of the container, unless you override it.</returns>
		// Token: 0x17000DD0 RID: 3536
		// (get) Token: 0x060039E9 RID: 14825 RVA: 0x0001A0D6 File Offset: 0x000182D6
		// (set) Token: 0x060039EA RID: 14826 RVA: 0x000FFEFD File Offset: 0x000FE0FD
		[Localizable(true)]
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				base.Font = value;
				this.SetPanelContentsWidths(false);
			}
		}

		/// <summary>Gets or sets the forecolor for the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the forecolor of the control. The default is <see langword="Empty" />.</returns>
		// Token: 0x17000DD1 RID: 3537
		// (get) Token: 0x060039EB RID: 14827 RVA: 0x0001A0E7 File Offset: 0x000182E7
		// (set) Token: 0x060039EC RID: 14828 RVA: 0x00013024 File Offset: 0x00011224
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.StatusBar.ForeColor" /> property changes.</summary>
		// Token: 0x140002CB RID: 715
		// (add) Token: 0x060039ED RID: 14829 RVA: 0x0005A8EE File Offset: 0x00058AEE
		// (remove) Token: 0x060039EE RID: 14830 RVA: 0x0005A8F7 File Offset: 0x00058AF7
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
		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x060039EF RID: 14831 RVA: 0x0001A051 File Offset: 0x00018251
		// (set) Token: 0x060039F0 RID: 14832 RVA: 0x0001A059 File Offset: 0x00018259
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.StatusBar.ImeMode" /> property changes.</summary>
		// Token: 0x140002CC RID: 716
		// (add) Token: 0x060039F1 RID: 14833 RVA: 0x00023F70 File Offset: 0x00022170
		// (remove) Token: 0x060039F2 RID: 14834 RVA: 0x00023F79 File Offset: 0x00022179
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

		/// <summary>Gets the collection of <see cref="T:System.Windows.Forms.StatusBar" /> panels contained within the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.StatusBar.StatusBarPanelCollection" /> containing the <see cref="T:System.Windows.Forms.StatusBarPanel" /> objects of the <see cref="T:System.Windows.Forms.StatusBar" /> control.</returns>
		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x060039F3 RID: 14835 RVA: 0x000FFF0D File Offset: 0x000FE10D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[SRDescription("StatusBarPanelsDescr")]
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[MergableProperty(false)]
		public StatusBar.StatusBarPanelCollection Panels
		{
			get
			{
				if (this.panelsCollection == null)
				{
					this.panelsCollection = new StatusBar.StatusBarPanelCollection(this);
				}
				return this.panelsCollection;
			}
		}

		/// <summary>Gets or sets the text associated with the <see cref="T:System.Windows.Forms.StatusBar" /> control.</summary>
		/// <returns>The text associated with the <see cref="T:System.Windows.Forms.StatusBar" /> control.</returns>
		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x060039F4 RID: 14836 RVA: 0x000FFF29 File Offset: 0x000FE129
		// (set) Token: 0x060039F5 RID: 14837 RVA: 0x000FFF3F File Offset: 0x000FE13F
		[Localizable(true)]
		public override string Text
		{
			get
			{
				if (this.simpleText == null)
				{
					return "";
				}
				return this.simpleText;
			}
			set
			{
				this.SetSimpleText(value);
				if (this.simpleText != value)
				{
					this.simpleText = value;
					this.OnTextChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether any panels that have been added to the control are displayed.</summary>
		/// <returns>
		///   <see langword="true" /> if panels are displayed; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x060039F6 RID: 14838 RVA: 0x000FFF68 File Offset: 0x000FE168
		// (set) Token: 0x060039F7 RID: 14839 RVA: 0x000FFF70 File Offset: 0x000FE170
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("StatusBarShowPanelsDescr")]
		public bool ShowPanels
		{
			get
			{
				return this.showPanels;
			}
			set
			{
				if (this.showPanels != value)
				{
					this.showPanels = value;
					this.layoutDirty = true;
					if (base.IsHandleCreated)
					{
						int num = ((!this.showPanels) ? 1 : 0);
						base.SendMessage(1033, num, 0);
						if (this.showPanels)
						{
							base.PerformLayout();
							this.RealizePanels();
						}
						else if (this.tooltips != null)
						{
							for (int i = 0; i < this.panels.Count; i++)
							{
								this.tooltips.SetTool(this.panels[i], null);
							}
						}
						this.SetSimpleText(this.simpleText);
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether a sizing grip is displayed in the lower-right corner of the control.</summary>
		/// <returns>
		///   <see langword="true" /> if a sizing grip is displayed; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x060039F8 RID: 14840 RVA: 0x00100012 File Offset: 0x000FE212
		// (set) Token: 0x060039F9 RID: 14841 RVA: 0x0010001A File Offset: 0x000FE21A
		[SRCategory("CatAppearance")]
		[DefaultValue(true)]
		[SRDescription("StatusBarSizingGripDescr")]
		public bool SizingGrip
		{
			get
			{
				return this.sizeGrip;
			}
			set
			{
				if (value != this.sizeGrip)
				{
					this.sizeGrip = value;
					base.RecreateHandle();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the user will be able to tab to the <see cref="T:System.Windows.Forms.StatusBar" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the tab key moves focus to the <see cref="T:System.Windows.Forms.StatusBar" />; otherwise <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x060039FA RID: 14842 RVA: 0x000B2395 File Offset: 0x000B0595
		// (set) Token: 0x060039FB RID: 14843 RVA: 0x000B239D File Offset: 0x000B059D
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

		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x060039FC RID: 14844 RVA: 0x00100032 File Offset: 0x000FE232
		internal bool ToolTipSet
		{
			get
			{
				return this.toolTipSet;
			}
		}

		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x060039FD RID: 14845 RVA: 0x0010003A File Offset: 0x000FE23A
		internal ToolTip MainToolTip
		{
			get
			{
				return this.mainToolTip;
			}
		}

		/// <summary>Occurs when a visual aspect of an owner-drawn status bar control changes.</summary>
		// Token: 0x140002CD RID: 717
		// (add) Token: 0x060039FE RID: 14846 RVA: 0x00100042 File Offset: 0x000FE242
		// (remove) Token: 0x060039FF RID: 14847 RVA: 0x00100055 File Offset: 0x000FE255
		[SRCategory("CatBehavior")]
		[SRDescription("StatusBarDrawItem")]
		public event StatusBarDrawItemEventHandler DrawItem
		{
			add
			{
				base.Events.AddHandler(StatusBar.EVENT_SBDRAWITEM, value);
			}
			remove
			{
				base.Events.RemoveHandler(StatusBar.EVENT_SBDRAWITEM, value);
			}
		}

		/// <summary>Occurs when a <see cref="T:System.Windows.Forms.StatusBarPanel" /> object on a <see cref="T:System.Windows.Forms.StatusBar" /> control is clicked.</summary>
		// Token: 0x140002CE RID: 718
		// (add) Token: 0x06003A00 RID: 14848 RVA: 0x00100068 File Offset: 0x000FE268
		// (remove) Token: 0x06003A01 RID: 14849 RVA: 0x0010007B File Offset: 0x000FE27B
		[SRCategory("CatMouse")]
		[SRDescription("StatusBarOnPanelClickDescr")]
		public event StatusBarPanelClickEventHandler PanelClick
		{
			add
			{
				base.Events.AddHandler(StatusBar.EVENT_PANELCLICK, value);
			}
			remove
			{
				base.Events.RemoveHandler(StatusBar.EVENT_PANELCLICK, value);
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.StatusBar" /> control is redrawn.</summary>
		// Token: 0x140002CF RID: 719
		// (add) Token: 0x06003A02 RID: 14850 RVA: 0x00013D73 File Offset: 0x00011F73
		// (remove) Token: 0x06003A03 RID: 14851 RVA: 0x00013D7C File Offset: 0x00011F7C
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

		// Token: 0x06003A04 RID: 14852 RVA: 0x0010008E File Offset: 0x000FE28E
		internal bool ArePanelsRealized()
		{
			return this.showPanels && base.IsHandleCreated;
		}

		// Token: 0x06003A05 RID: 14853 RVA: 0x001000A0 File Offset: 0x000FE2A0
		internal void DirtyLayout()
		{
			this.layoutDirty = true;
		}

		// Token: 0x06003A06 RID: 14854 RVA: 0x001000AC File Offset: 0x000FE2AC
		private void ApplyPanelWidths()
		{
			if (!base.IsHandleCreated)
			{
				return;
			}
			int count = this.panels.Count;
			if (count == 0)
			{
				int[] array = new int[] { base.Size.Width };
				if (this.sizeGrip)
				{
					array[0] -= this.SizeGripWidth;
				}
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1028, 1, array);
				base.SendMessage(1039, 0, IntPtr.Zero);
				return;
			}
			int[] array2 = new int[count];
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				StatusBarPanel statusBarPanel = (StatusBarPanel)this.panels[i];
				num += statusBarPanel.Width;
				array2[i] = num;
				statusBarPanel.Right = array2[i];
			}
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1028, count, array2);
			for (int j = 0; j < count; j++)
			{
				StatusBarPanel statusBarPanel = (StatusBarPanel)this.panels[j];
				this.UpdateTooltip(statusBarPanel);
			}
			this.layoutDirty = false;
		}

		/// <summary>Overrides <see cref="M:System.Windows.Forms.Control.CreateHandle" />.</summary>
		// Token: 0x06003A07 RID: 14855 RVA: 0x001001C4 File Offset: 0x000FE3C4
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

		/// <summary>Disposes of the resources (other than memory) used by the <see cref="T:System.Windows.Forms.StatusBar" />.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06003A08 RID: 14856 RVA: 0x00100214 File Offset: 0x000FE414
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.panelsCollection != null)
			{
				StatusBarPanel[] array = new StatusBarPanel[this.panelsCollection.Count];
				((ICollection)this.panelsCollection).CopyTo(array, 0);
				this.panelsCollection.Clear();
				foreach (StatusBarPanel statusBarPanel in array)
				{
					statusBarPanel.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06003A09 RID: 14857 RVA: 0x00100276 File Offset: 0x000FE476
		private void ForcePanelUpdate()
		{
			if (this.ArePanelsRealized())
			{
				this.layoutDirty = true;
				this.SetPanelContentsWidths(true);
				base.PerformLayout();
				this.RealizePanels();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003A0A RID: 14858 RVA: 0x0010029C File Offset: 0x000FE49C
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			if (!base.DesignMode)
			{
				this.tooltips = new StatusBar.ControlToolTip(this);
			}
			if (!this.showPanels)
			{
				base.SendMessage(1033, 1, 0);
				this.SetSimpleText(this.simpleText);
				return;
			}
			this.ForcePanelUpdate();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003A0B RID: 14859 RVA: 0x001002ED File Offset: 0x000FE4ED
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
			if (this.tooltips != null)
			{
				this.tooltips.Dispose();
				this.tooltips = null;
			}
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.StatusBar.OnMouseDown(System.Windows.Forms.MouseEventArgs)" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06003A0C RID: 14860 RVA: 0x00100310 File Offset: 0x000FE510
		protected override void OnMouseDown(MouseEventArgs e)
		{
			this.lastClick.X = e.X;
			this.lastClick.Y = e.Y;
			base.OnMouseDown(e);
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.StatusBar.OnPanelClick(System.Windows.Forms.StatusBarPanelClickEventArgs)" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.StatusBarPanelClickEventArgs" /> that contains the event data.</param>
		// Token: 0x06003A0D RID: 14861 RVA: 0x0010033C File Offset: 0x000FE53C
		protected virtual void OnPanelClick(StatusBarPanelClickEventArgs e)
		{
			StatusBarPanelClickEventHandler statusBarPanelClickEventHandler = (StatusBarPanelClickEventHandler)base.Events[StatusBar.EVENT_PANELCLICK];
			if (statusBarPanelClickEventHandler != null)
			{
				statusBarPanelClickEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see langword="Layout" /> event.</summary>
		/// <param name="levent">A <see langword="LayoutEventArgs" /> that contains the event data.</param>
		// Token: 0x06003A0E RID: 14862 RVA: 0x0010036A File Offset: 0x000FE56A
		protected override void OnLayout(LayoutEventArgs levent)
		{
			if (this.showPanels)
			{
				this.LayoutPanels();
				if (base.IsHandleCreated && this.panelsRealized != this.panels.Count)
				{
					this.RealizePanels();
				}
			}
			base.OnLayout(levent);
		}

		// Token: 0x06003A0F RID: 14863 RVA: 0x001003A4 File Offset: 0x000FE5A4
		internal void RealizePanels()
		{
			int count = this.panels.Count;
			int num = this.panelsRealized;
			this.panelsRealized = 0;
			if (count == 0)
			{
				base.SendMessage(NativeMethods.SB_SETTEXT, 0, "");
			}
			int i;
			for (i = 0; i < count; i++)
			{
				StatusBarPanel statusBarPanel = (StatusBarPanel)this.panels[i];
				try
				{
					statusBarPanel.Realize();
					this.panelsRealized++;
				}
				catch
				{
				}
			}
			while (i < num)
			{
				base.SendMessage(NativeMethods.SB_SETTEXT, 0, null);
				i++;
			}
		}

		// Token: 0x06003A10 RID: 14864 RVA: 0x00100444 File Offset: 0x000FE644
		internal void RemoveAllPanelsWithoutUpdate()
		{
			int count = this.panels.Count;
			for (int i = 0; i < count; i++)
			{
				StatusBarPanel statusBarPanel = (StatusBarPanel)this.panels[i];
				statusBarPanel.ParentInternal = null;
			}
			this.panels.Clear();
			if (this.showPanels)
			{
				this.ApplyPanelWidths();
				this.ForcePanelUpdate();
			}
		}

		// Token: 0x06003A11 RID: 14865 RVA: 0x001004A4 File Offset: 0x000FE6A4
		internal void SetPanelContentsWidths(bool newPanels)
		{
			int count = this.panels.Count;
			bool flag = false;
			for (int i = 0; i < count; i++)
			{
				StatusBarPanel statusBarPanel = (StatusBarPanel)this.panels[i];
				if (statusBarPanel.AutoSize == StatusBarPanelAutoSize.Contents)
				{
					int contentsWidth = statusBarPanel.GetContentsWidth(newPanels);
					if (statusBarPanel.Width != contentsWidth)
					{
						statusBarPanel.Width = contentsWidth;
						flag = true;
					}
				}
			}
			if (flag)
			{
				this.DirtyLayout();
				base.PerformLayout();
			}
		}

		// Token: 0x06003A12 RID: 14866 RVA: 0x00100514 File Offset: 0x000FE714
		private void SetSimpleText(string simpleText)
		{
			if (!this.showPanels && base.IsHandleCreated)
			{
				int num = 511;
				if (this.RightToLeft == RightToLeft.Yes)
				{
					num |= 1024;
				}
				base.SendMessage(NativeMethods.SB_SETTEXT, num, simpleText);
			}
		}

		// Token: 0x06003A13 RID: 14867 RVA: 0x00100558 File Offset: 0x000FE758
		private void LayoutPanels()
		{
			int num = 0;
			int num2 = 0;
			StatusBarPanel[] array = new StatusBarPanel[this.panels.Count];
			bool flag = false;
			for (int i = 0; i < array.Length; i++)
			{
				StatusBarPanel statusBarPanel = (StatusBarPanel)this.panels[i];
				if (statusBarPanel.AutoSize == StatusBarPanelAutoSize.Spring)
				{
					array[num2] = statusBarPanel;
					num2++;
				}
				else
				{
					num += statusBarPanel.Width;
				}
			}
			if (num2 > 0)
			{
				Rectangle bounds = base.Bounds;
				int j = num2;
				int num3 = bounds.Width - num;
				if (this.sizeGrip)
				{
					num3 -= this.SizeGripWidth;
				}
				int num4 = int.MinValue;
				while (j > 0)
				{
					int num5 = num3 / j;
					if (num3 == num4)
					{
						break;
					}
					num4 = num3;
					for (int k = 0; k < num2; k++)
					{
						StatusBarPanel statusBarPanel = array[k];
						if (statusBarPanel != null)
						{
							if (num5 < statusBarPanel.MinWidth)
							{
								if (statusBarPanel.Width != statusBarPanel.MinWidth)
								{
									flag = true;
								}
								statusBarPanel.Width = statusBarPanel.MinWidth;
								array[k] = null;
								j--;
								num3 -= statusBarPanel.MinWidth;
							}
							else
							{
								if (statusBarPanel.Width != num5)
								{
									flag = true;
								}
								statusBarPanel.Width = num5;
							}
						}
					}
				}
			}
			if (flag || this.layoutDirty)
			{
				this.ApplyPanelWidths();
			}
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.StatusBar.OnDrawItem(System.Windows.Forms.StatusBarDrawItemEventArgs)" /> event.</summary>
		/// <param name="sbdievent">A <see cref="T:System.Windows.Forms.StatusBarDrawItemEventArgs" /> that contains the event data.</param>
		// Token: 0x06003A14 RID: 14868 RVA: 0x00100690 File Offset: 0x000FE890
		protected virtual void OnDrawItem(StatusBarDrawItemEventArgs sbdievent)
		{
			StatusBarDrawItemEventHandler statusBarDrawItemEventHandler = (StatusBarDrawItemEventHandler)base.Events[StatusBar.EVENT_SBDRAWITEM];
			if (statusBarDrawItemEventHandler != null)
			{
				statusBarDrawItemEventHandler(this, sbdievent);
			}
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.StatusBar.OnResize(System.EventArgs)" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003A15 RID: 14869 RVA: 0x001006BE File Offset: 0x000FE8BE
		protected override void OnResize(EventArgs e)
		{
			base.Invalidate();
			base.OnResize(e);
		}

		/// <summary>Returns a string representation for this control.</summary>
		/// <returns>String</returns>
		// Token: 0x06003A16 RID: 14870 RVA: 0x001006D0 File Offset: 0x000FE8D0
		public override string ToString()
		{
			string text = base.ToString();
			if (this.Panels != null)
			{
				text = text + ", Panels.Count: " + this.Panels.Count.ToString(CultureInfo.CurrentCulture);
				if (this.Panels.Count > 0)
				{
					text = text + ", Panels[0]: " + this.Panels[0].ToString();
				}
			}
			return text;
		}

		// Token: 0x06003A17 RID: 14871 RVA: 0x0010073C File Offset: 0x000FE93C
		internal void SetToolTip(ToolTip t)
		{
			this.mainToolTip = t;
			this.toolTipSet = true;
		}

		// Token: 0x06003A18 RID: 14872 RVA: 0x0010074C File Offset: 0x000FE94C
		internal void UpdateTooltip(StatusBarPanel panel)
		{
			if (this.tooltips == null)
			{
				if (!base.IsHandleCreated || base.DesignMode)
				{
					return;
				}
				this.tooltips = new StatusBar.ControlToolTip(this);
			}
			if (panel.Parent == this && panel.ToolTipText.Length > 0)
			{
				int width = SystemInformation.Border3DSize.Width;
				StatusBar.ControlToolTip.Tool tool = this.tooltips.GetTool(panel);
				if (tool == null)
				{
					tool = new StatusBar.ControlToolTip.Tool();
				}
				tool.text = panel.ToolTipText;
				tool.rect = new Rectangle(panel.Right - panel.Width + width, 0, panel.Width - width, base.Height);
				this.tooltips.SetTool(panel, tool);
				return;
			}
			this.tooltips.SetTool(panel, null);
		}

		// Token: 0x06003A19 RID: 14873 RVA: 0x0010080C File Offset: 0x000FEA0C
		private void UpdatePanelIndex()
		{
			int count = this.panels.Count;
			for (int i = 0; i < count; i++)
			{
				((StatusBarPanel)this.panels[i]).Index = i;
			}
		}

		// Token: 0x06003A1A RID: 14874 RVA: 0x00100848 File Offset: 0x000FEA48
		private void WmDrawItem(ref Message m)
		{
			NativeMethods.DRAWITEMSTRUCT drawitemstruct = (NativeMethods.DRAWITEMSTRUCT)m.GetLParam(typeof(NativeMethods.DRAWITEMSTRUCT));
			int count = this.panels.Count;
			if (drawitemstruct.itemID >= 0)
			{
				int itemID = drawitemstruct.itemID;
			}
			StatusBarPanel statusBarPanel = (StatusBarPanel)this.panels[drawitemstruct.itemID];
			Graphics graphics = Graphics.FromHdcInternal(drawitemstruct.hDC);
			Rectangle rectangle = Rectangle.FromLTRB(drawitemstruct.rcItem.left, drawitemstruct.rcItem.top, drawitemstruct.rcItem.right, drawitemstruct.rcItem.bottom);
			this.OnDrawItem(new StatusBarDrawItemEventArgs(graphics, this.Font, rectangle, drawitemstruct.itemID, DrawItemState.None, statusBarPanel, this.ForeColor, this.BackColor));
			graphics.Dispose();
		}

		// Token: 0x06003A1B RID: 14875 RVA: 0x00100910 File Offset: 0x000FEB10
		private void WmNotifyNMClick(NativeMethods.NMHDR note)
		{
			if (!this.showPanels)
			{
				return;
			}
			int count = this.panels.Count;
			int num = 0;
			int num2 = -1;
			for (int i = 0; i < count; i++)
			{
				StatusBarPanel statusBarPanel = (StatusBarPanel)this.panels[i];
				num += statusBarPanel.Width;
				if (this.lastClick.X < num)
				{
					num2 = i;
					break;
				}
			}
			if (num2 != -1)
			{
				MouseButtons mouseButtons = MouseButtons.Left;
				int num3 = 0;
				switch (note.code)
				{
				case -6:
					mouseButtons = MouseButtons.Right;
					num3 = 2;
					break;
				case -5:
					mouseButtons = MouseButtons.Right;
					num3 = 1;
					break;
				case -3:
					mouseButtons = MouseButtons.Left;
					num3 = 2;
					break;
				case -2:
					mouseButtons = MouseButtons.Left;
					num3 = 1;
					break;
				}
				Point point = this.lastClick;
				StatusBarPanel statusBarPanel2 = (StatusBarPanel)this.panels[num2];
				StatusBarPanelClickEventArgs statusBarPanelClickEventArgs = new StatusBarPanelClickEventArgs(statusBarPanel2, mouseButtons, num3, point.X, point.Y);
				this.OnPanelClick(statusBarPanelClickEventArgs);
			}
		}

		// Token: 0x06003A1C RID: 14876 RVA: 0x00100A18 File Offset: 0x000FEC18
		private void WmNCHitTest(ref Message m)
		{
			int num = NativeMethods.Util.LOWORD(m.LParam);
			Rectangle bounds = base.Bounds;
			bool flag = true;
			if (num > bounds.X + bounds.Width - this.SizeGripWidth)
			{
				Control parentInternal = this.ParentInternal;
				if (parentInternal != null && parentInternal is Form)
				{
					FormBorderStyle formBorderStyle = ((Form)parentInternal).FormBorderStyle;
					if (formBorderStyle != FormBorderStyle.Sizable && formBorderStyle != FormBorderStyle.SizableToolWindow)
					{
						flag = false;
					}
					if (!((Form)parentInternal).TopLevel || this.Dock != DockStyle.Bottom)
					{
						flag = false;
					}
					if (flag)
					{
						Control.ControlCollection controls = parentInternal.Controls;
						int count = controls.Count;
						for (int i = 0; i < count; i++)
						{
							Control control = controls[i];
							if (control != this && control.Dock == DockStyle.Bottom && control.Top > base.Top)
							{
								flag = false;
								break;
							}
						}
					}
				}
				else
				{
					flag = false;
				}
			}
			if (flag)
			{
				base.WndProc(ref m);
				return;
			}
			m.Result = (IntPtr)1;
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x06003A1D RID: 14877 RVA: 0x00100B0C File Offset: 0x000FED0C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg <= 132)
			{
				if (msg != 78)
				{
					if (msg != 132)
					{
						goto IL_7B;
					}
					this.WmNCHitTest(ref m);
					return;
				}
			}
			else
			{
				if (msg == 8235)
				{
					this.WmDrawItem(ref m);
					return;
				}
				if (msg != 8270)
				{
					goto IL_7B;
				}
			}
			NativeMethods.NMHDR nmhdr = (NativeMethods.NMHDR)m.GetLParam(typeof(NativeMethods.NMHDR));
			int code = nmhdr.code;
			if (code - -6 <= 1 || code - -3 <= 1)
			{
				this.WmNotifyNMClick(nmhdr);
				return;
			}
			base.WndProc(ref m);
			return;
			IL_7B:
			base.WndProc(ref m);
		}

		// Token: 0x040022CD RID: 8909
		private int sizeGripWidth;

		// Token: 0x040022CE RID: 8910
		private const int SIMPLE_INDEX = 255;

		// Token: 0x040022CF RID: 8911
		private static readonly object EVENT_PANELCLICK = new object();

		// Token: 0x040022D0 RID: 8912
		private static readonly object EVENT_SBDRAWITEM = new object();

		// Token: 0x040022D1 RID: 8913
		private bool showPanels;

		// Token: 0x040022D2 RID: 8914
		private bool layoutDirty;

		// Token: 0x040022D3 RID: 8915
		private int panelsRealized;

		// Token: 0x040022D4 RID: 8916
		private bool sizeGrip = true;

		// Token: 0x040022D5 RID: 8917
		private string simpleText;

		// Token: 0x040022D6 RID: 8918
		private Point lastClick = new Point(0, 0);

		// Token: 0x040022D7 RID: 8919
		private IList panels = new ArrayList();

		// Token: 0x040022D8 RID: 8920
		private StatusBar.StatusBarPanelCollection panelsCollection;

		// Token: 0x040022D9 RID: 8921
		private StatusBar.ControlToolTip tooltips;

		// Token: 0x040022DA RID: 8922
		private ToolTip mainToolTip;

		// Token: 0x040022DB RID: 8923
		private bool toolTipSet;

		// Token: 0x040022DC RID: 8924
		private static VisualStyleRenderer renderer = null;

		/// <summary>Represents the collection of panels in a <see cref="T:System.Windows.Forms.StatusBar" /> control.</summary>
		// Token: 0x020007E8 RID: 2024
		[ListBindable(false)]
		public class StatusBarPanelCollection : IList, ICollection, IEnumerable
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.StatusBar.StatusBarPanelCollection" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.StatusBar" /> control that contains this collection.</param>
			// Token: 0x06006DD0 RID: 28112 RVA: 0x00192527 File Offset: 0x00190727
			public StatusBarPanelCollection(StatusBar owner)
			{
				this.owner = owner;
			}

			/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.StatusBarPanel" /> at the specified index.</summary>
			/// <param name="index">The index of the panel in the collection to get or set.</param>
			/// <returns>A <see cref="T:System.Windows.Forms.StatusBarPanel" /> representing the panel located at the specified index within the collection.</returns>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than or equal to the value of the <see cref="P:System.Windows.Forms.StatusBar.StatusBarPanelCollection.Count" /> property of the <see cref="T:System.Windows.Forms.StatusBar.StatusBarPanelCollection" /> class.</exception>
			/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Windows.Forms.StatusBarPanel" /> assigned to the collection was <see langword="null" />.</exception>
			// Token: 0x17001803 RID: 6147
			public virtual StatusBarPanel this[int index]
			{
				get
				{
					return (StatusBarPanel)this.owner.panels[index];
				}
				set
				{
					if (value == null)
					{
						throw new ArgumentNullException("StatusBarPanel");
					}
					this.owner.layoutDirty = true;
					if (value.Parent != null)
					{
						throw new ArgumentException(SR.GetString("ObjectHasParent"), "value");
					}
					int count = this.owner.panels.Count;
					if (index < 0 || index >= count)
					{
						throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
					StatusBarPanel statusBarPanel = (StatusBarPanel)this.owner.panels[index];
					statusBarPanel.ParentInternal = null;
					value.ParentInternal = this.owner;
					if (value.AutoSize == StatusBarPanelAutoSize.Contents)
					{
						value.Width = value.GetContentsWidth(true);
					}
					this.owner.panels[index] = value;
					value.Index = index;
					if (this.owner.ArePanelsRealized())
					{
						this.owner.PerformLayout();
						value.Realize();
					}
				}
			}

			/// <summary>Gets or sets the element at the specified index.</summary>
			/// <param name="index">The zero-based index of the element to get or set.</param>
			/// <returns>The element at the specified index.</returns>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.StatusBarPanel" />.</exception>
			// Token: 0x17001804 RID: 6148
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					if (value is StatusBarPanel)
					{
						this[index] = (StatusBarPanel)value;
						return;
					}
					throw new ArgumentException(SR.GetString("StatusBarBadStatusBarPanel"), "value");
				}
			}

			/// <summary>Gets an item with the specified key from the collection.</summary>
			/// <param name="key">The name of the item to retrieve from the collection.</param>
			/// <returns>The <see cref="T:System.Windows.Forms.StatusBarPanel" /> with the specified key.</returns>
			// Token: 0x17001805 RID: 6149
			public virtual StatusBarPanel this[string key]
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

			/// <summary>Gets the number of items in the collection.</summary>
			/// <returns>The number of <see cref="T:System.Windows.Forms.StatusBarPanel" /> objects in the collection.</returns>
			// Token: 0x17001806 RID: 6150
			// (get) Token: 0x06006DD6 RID: 28118 RVA: 0x001926C5 File Offset: 0x001908C5
			[Browsable(false)]
			[EditorBrowsable(EditorBrowsableState.Never)]
			public int Count
			{
				get
				{
					return this.owner.panels.Count;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
			/// <returns>The object used to synchronize access to the collection.</returns>
			// Token: 0x17001807 RID: 6151
			// (get) Token: 0x06006DD7 RID: 28119 RVA: 0x00006A49 File Offset: 0x00004C49
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
			/// <returns>
			///   <see langword="false" /> in all cases.</returns>
			// Token: 0x17001808 RID: 6152
			// (get) Token: 0x06006DD8 RID: 28120 RVA: 0x0001180C File Offset: 0x0000FA0C
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
			/// <returns>
			///   <see langword="false" /> in all cases.</returns>
			// Token: 0x17001809 RID: 6153
			// (get) Token: 0x06006DD9 RID: 28121 RVA: 0x0001180C File Offset: 0x0000FA0C
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets a value indicating whether this collection is read-only.</summary>
			/// <returns>
			///   <see langword="true" /> if this collection is read-only; otherwise, <see langword="false" />.</returns>
			// Token: 0x1700180A RID: 6154
			// (get) Token: 0x06006DDA RID: 28122 RVA: 0x0001180C File Offset: 0x0000FA0C
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			/// <summary>Adds a <see cref="T:System.Windows.Forms.StatusBarPanel" /> with the specified text to the collection.</summary>
			/// <param name="text">The text for the <see cref="T:System.Windows.Forms.StatusBarPanel" /> that is being added.</param>
			/// <returns>A <see cref="T:System.Windows.Forms.StatusBarPanel" /> that represents the panel that was added to the collection.</returns>
			// Token: 0x06006DDB RID: 28123 RVA: 0x001926D8 File Offset: 0x001908D8
			public virtual StatusBarPanel Add(string text)
			{
				StatusBarPanel statusBarPanel = new StatusBarPanel();
				statusBarPanel.Text = text;
				this.Add(statusBarPanel);
				return statusBarPanel;
			}

			/// <summary>Adds a <see cref="T:System.Windows.Forms.StatusBarPanel" /> to the collection.</summary>
			/// <param name="value">A <see cref="T:System.Windows.Forms.StatusBarPanel" /> that represents the panel to add to the collection.</param>
			/// <returns>The zero-based index of the item in the collection.</returns>
			/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Windows.Forms.StatusBarPanel" /> being added to the collection was <see langword="null" />.</exception>
			/// <exception cref="T:System.ArgumentException">The parent of the <see cref="T:System.Windows.Forms.StatusBarPanel" /> specified in the <paramref name="value" /> parameter is not <see langword="null" />.</exception>
			// Token: 0x06006DDC RID: 28124 RVA: 0x001926FC File Offset: 0x001908FC
			public virtual int Add(StatusBarPanel value)
			{
				int count = this.owner.panels.Count;
				this.Insert(count, value);
				return count;
			}

			/// <summary>Adds a <see cref="T:System.Windows.Forms.StatusBarPanel" /> to the collection.</summary>
			/// <param name="value">A <see cref="T:System.Windows.Forms.StatusBarPanel" /> that represents the panel to add to the collection.</param>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.StatusBarPanel" />.  
			/// -or-  
			/// The parent of value is not <see langword="null" />.</exception>
			// Token: 0x06006DDD RID: 28125 RVA: 0x00192723 File Offset: 0x00190923
			int IList.Add(object value)
			{
				if (value is StatusBarPanel)
				{
					return this.Add((StatusBarPanel)value);
				}
				throw new ArgumentException(SR.GetString("StatusBarBadStatusBarPanel"), "value");
			}

			/// <summary>Adds an array of <see cref="T:System.Windows.Forms.StatusBarPanel" /> objects to the collection.</summary>
			/// <param name="panels">An array of <see cref="T:System.Windows.Forms.StatusBarPanel" /> objects to add.</param>
			/// <exception cref="T:System.ArgumentNullException">The array of <see cref="T:System.Windows.Forms.StatusBarPanel" /> objects being added to the collection was <see langword="null" />.</exception>
			// Token: 0x06006DDE RID: 28126 RVA: 0x00192750 File Offset: 0x00190950
			public virtual void AddRange(StatusBarPanel[] panels)
			{
				if (panels == null)
				{
					throw new ArgumentNullException("panels");
				}
				foreach (StatusBarPanel statusBarPanel in panels)
				{
					this.Add(statusBarPanel);
				}
			}

			/// <summary>Determines whether the specified panel is located within the collection.</summary>
			/// <param name="panel">The <see cref="T:System.Windows.Forms.StatusBarPanel" /> to locate in the collection.</param>
			/// <returns>
			///   <see langword="true" /> if the panel is located within the collection; otherwise, <see langword="false" />.</returns>
			// Token: 0x06006DDF RID: 28127 RVA: 0x00192787 File Offset: 0x00190987
			public bool Contains(StatusBarPanel panel)
			{
				return this.IndexOf(panel) != -1;
			}

			/// <summary>Determines whether the specified panel is located within the collection.</summary>
			/// <param name="panel">The <see cref="T:System.Windows.Forms.StatusBarPanel" /> to locate in the collection.</param>
			/// <returns>
			///   <see langword="true" /> if panel is a <see cref="T:System.Windows.Forms.StatusBarPanel" /> located within the collection; otherwise, <see langword="false" />.</returns>
			// Token: 0x06006DE0 RID: 28128 RVA: 0x00192796 File Offset: 0x00190996
			bool IList.Contains(object panel)
			{
				return panel is StatusBarPanel && this.Contains((StatusBarPanel)panel);
			}

			/// <summary>Determines whether the collection contains a <see cref="T:System.Windows.Forms.StatusBarPanel" /> with the specified key.</summary>
			/// <param name="key">The name of the item to find in the collection.</param>
			/// <returns>
			///   <see langword="true" /> to indicate the collection contains a <see cref="T:System.Windows.Forms.StatusBarPanel" /> with the specified key; otherwise, <see langword="false" />.</returns>
			// Token: 0x06006DE1 RID: 28129 RVA: 0x001927AE File Offset: 0x001909AE
			public virtual bool ContainsKey(string key)
			{
				return this.IsValidIndex(this.IndexOfKey(key));
			}

			/// <summary>Returns the index within the collection of the specified panel.</summary>
			/// <param name="panel">The <see cref="T:System.Windows.Forms.StatusBarPanel" /> to locate in the collection.</param>
			/// <returns>The zero-based index where the panel is located within the collection; otherwise, negative one (-1).</returns>
			// Token: 0x06006DE2 RID: 28130 RVA: 0x001927C0 File Offset: 0x001909C0
			public int IndexOf(StatusBarPanel panel)
			{
				for (int i = 0; i < this.Count; i++)
				{
					if (this[i] == panel)
					{
						return i;
					}
				}
				return -1;
			}

			/// <summary>Returns the index of the specified panel within the collection.</summary>
			/// <param name="panel">The <see cref="T:System.Windows.Forms.StatusBarPanel" /> to locate in the collection.</param>
			/// <returns>The zero-based index of panel, if found, within the entire collection; otherwise, -1.</returns>
			// Token: 0x06006DE3 RID: 28131 RVA: 0x001927EB File Offset: 0x001909EB
			int IList.IndexOf(object panel)
			{
				if (panel is StatusBarPanel)
				{
					return this.IndexOf((StatusBarPanel)panel);
				}
				return -1;
			}

			/// <summary>Returns the index of the first occurrence of a <see cref="T:System.Windows.Forms.StatusBarPanel" /> with the specified key.</summary>
			/// <param name="key">The name of the <see cref="T:System.Windows.Forms.StatusBarPanel" /> to find in the collection.</param>
			/// <returns>The zero-based index of the first occurrence of the <see cref="T:System.Windows.Forms.StatusBarPanel" /> with the specified key, if found; otherwise, -1.</returns>
			// Token: 0x06006DE4 RID: 28132 RVA: 0x00192804 File Offset: 0x00190A04
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

			/// <summary>Inserts the specified <see cref="T:System.Windows.Forms.StatusBarPanel" /> into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the panel is inserted.</param>
			/// <param name="value">A <see cref="T:System.Windows.Forms.StatusBarPanel" /> representing the panel to insert.</param>
			/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
			/// <exception cref="T:System.ArgumentException">The <paramref name="value" /> parameter's parent is not <see langword="null" />.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than the value of the <see cref="P:System.Windows.Forms.StatusBar.StatusBarPanelCollection.Count" /> property of the <see cref="T:System.Windows.Forms.StatusBar.StatusBarPanelCollection" /> class.</exception>
			/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <see cref="P:System.Windows.Forms.StatusBarPanel.AutoSize" /> property of the <paramref name="value" /> parameter's panel is not a valid <see cref="T:System.Windows.Forms.StatusBarPanelAutoSize" /> value.</exception>
			// Token: 0x06006DE5 RID: 28133 RVA: 0x00192884 File Offset: 0x00190A84
			public virtual void Insert(int index, StatusBarPanel value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.owner.layoutDirty = true;
				if (value.Parent != this.owner && value.Parent != null)
				{
					throw new ArgumentException(SR.GetString("ObjectHasParent"), "value");
				}
				int count = this.owner.panels.Count;
				if (index < 0 || index > count)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				value.ParentInternal = this.owner;
				StatusBarPanelAutoSize autoSize = value.AutoSize;
				if (autoSize - StatusBarPanelAutoSize.None > 1 && autoSize == StatusBarPanelAutoSize.Contents)
				{
					value.Width = value.GetContentsWidth(true);
				}
				this.owner.panels.Insert(index, value);
				this.owner.UpdatePanelIndex();
				this.owner.ForcePanelUpdate();
			}

			/// <summary>Inserts the specified <see cref="T:System.Windows.Forms.StatusBarPanel" /> into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the panel is inserted.</param>
			/// <param name="value">A <see cref="T:System.Windows.Forms.StatusBarPanel" /> that represents the panel to insert.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The index parameter is less than zero or greater than the value of the <see langword="Count" /> property.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.StatusBarPanel" />.  
			/// -or-  
			/// The parent of value is not <see langword="null" />.</exception>
			// Token: 0x06006DE6 RID: 28134 RVA: 0x00192973 File Offset: 0x00190B73
			void IList.Insert(int index, object value)
			{
				if (value is StatusBarPanel)
				{
					this.Insert(index, (StatusBarPanel)value);
					return;
				}
				throw new ArgumentException(SR.GetString("StatusBarBadStatusBarPanel"), "value");
			}

			// Token: 0x06006DE7 RID: 28135 RVA: 0x0019299F File Offset: 0x00190B9F
			private bool IsValidIndex(int index)
			{
				return index >= 0 && index < this.Count;
			}

			/// <summary>Removes all items from the collection.</summary>
			// Token: 0x06006DE8 RID: 28136 RVA: 0x001929B0 File Offset: 0x00190BB0
			public virtual void Clear()
			{
				this.owner.RemoveAllPanelsWithoutUpdate();
				this.owner.PerformLayout();
			}

			/// <summary>Removes the specified <see cref="T:System.Windows.Forms.StatusBarPanel" /> from the collection.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.StatusBarPanel" /> representing the panel to remove from the collection.</param>
			/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Windows.Forms.StatusBarPanel" /> assigned to the <paramref name="value" /> parameter is <see langword="null" />.</exception>
			// Token: 0x06006DE9 RID: 28137 RVA: 0x001929C8 File Offset: 0x00190BC8
			public virtual void Remove(StatusBarPanel value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("StatusBarPanel");
				}
				if (value.Parent != this.owner)
				{
					return;
				}
				this.RemoveAt(value.Index);
			}

			/// <summary>Removes the specified <see cref="T:System.Windows.Forms.StatusBarPanel" /> from the collection.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.StatusBarPanel" /> that represents the panel to remove from the collection.</param>
			// Token: 0x06006DEA RID: 28138 RVA: 0x001929F3 File Offset: 0x00190BF3
			void IList.Remove(object value)
			{
				if (value is StatusBarPanel)
				{
					this.Remove((StatusBarPanel)value);
				}
			}

			/// <summary>Removes the <see cref="T:System.Windows.Forms.StatusBarPanel" /> located at the specified index within the collection.</summary>
			/// <param name="index">The zero-based index of the item to remove.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than or equal to the value of the <see cref="P:System.Windows.Forms.StatusBar.StatusBarPanelCollection.Count" /> property of the <see cref="T:System.Windows.Forms.StatusBar.StatusBarPanelCollection" /> class.</exception>
			// Token: 0x06006DEB RID: 28139 RVA: 0x00192A0C File Offset: 0x00190C0C
			public virtual void RemoveAt(int index)
			{
				int count = this.Count;
				if (index < 0 || index >= count)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				StatusBarPanel statusBarPanel = (StatusBarPanel)this.owner.panels[index];
				this.owner.panels.RemoveAt(index);
				statusBarPanel.ParentInternal = null;
				this.owner.UpdateTooltip(statusBarPanel);
				this.owner.UpdatePanelIndex();
				this.owner.ForcePanelUpdate();
			}

			/// <summary>Removes the <see cref="T:System.Windows.Forms.StatusBarPanel" /> with the specified key from the collection.</summary>
			/// <param name="key">The name of the <see cref="T:System.Windows.Forms.StatusBarPanel" /> to remove from the collection.</param>
			// Token: 0x06006DEC RID: 28140 RVA: 0x00192AAC File Offset: 0x00190CAC
			public virtual void RemoveByKey(string key)
			{
				int num = this.IndexOfKey(key);
				if (this.IsValidIndex(num))
				{
					this.RemoveAt(num);
				}
			}

			/// <summary>Copies the <see cref="T:System.Windows.Forms.StatusBar.StatusBarPanelCollection" /> to a compatible one-dimensional array, starting at the specified index of the target array.</summary>
			/// <param name="dest">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in the array at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is <see langword="null" />.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than zero.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="array" /> is multidimensional.  
			/// -or-  
			/// The number of elements in the <see cref="T:System.Windows.Forms.StatusBar.StatusBarPanelCollection" /> is greater than the available space from index to the end of <paramref name="array" />.</exception>
			/// <exception cref="T:System.InvalidCastException">The type in the collection cannot be cast automatically to the type of <paramref name="array" />.</exception>
			// Token: 0x06006DED RID: 28141 RVA: 0x00192AD1 File Offset: 0x00190CD1
			void ICollection.CopyTo(Array dest, int index)
			{
				this.owner.panels.CopyTo(dest, index);
			}

			/// <summary>Returns an enumerator to use to iterate through the item collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the item collection.</returns>
			// Token: 0x06006DEE RID: 28142 RVA: 0x00192AE5 File Offset: 0x00190CE5
			public IEnumerator GetEnumerator()
			{
				if (this.owner.panels != null)
				{
					return this.owner.panels.GetEnumerator();
				}
				return new StatusBarPanel[0].GetEnumerator();
			}

			// Token: 0x040042C5 RID: 17093
			private StatusBar owner;

			// Token: 0x040042C6 RID: 17094
			private int lastAccessedIndex = -1;
		}

		// Token: 0x020007E9 RID: 2025
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		private class ControlToolTip
		{
			// Token: 0x06006DEF RID: 28143 RVA: 0x00192B10 File Offset: 0x00190D10
			public ControlToolTip(Control parent)
			{
				this.window = new StatusBar.ControlToolTip.ToolTipNativeWindow(this);
				this.parent = parent;
			}

			// Token: 0x1700180B RID: 6155
			// (get) Token: 0x06006DF0 RID: 28144 RVA: 0x00192B38 File Offset: 0x00190D38
			protected CreateParams CreateParams
			{
				get
				{
					SafeNativeMethods.InitCommonControlsEx(new NativeMethods.INITCOMMONCONTROLSEX
					{
						dwICC = 8
					});
					CreateParams createParams = new CreateParams();
					createParams.Parent = IntPtr.Zero;
					createParams.ClassName = "tooltips_class32";
					createParams.Style |= 1;
					createParams.ExStyle = 0;
					createParams.Caption = null;
					return createParams;
				}
			}

			// Token: 0x1700180C RID: 6156
			// (get) Token: 0x06006DF1 RID: 28145 RVA: 0x00192B92 File Offset: 0x00190D92
			public IntPtr Handle
			{
				get
				{
					if (this.window.Handle == IntPtr.Zero)
					{
						this.CreateHandle();
					}
					return this.window.Handle;
				}
			}

			// Token: 0x1700180D RID: 6157
			// (get) Token: 0x06006DF2 RID: 28146 RVA: 0x00192BBC File Offset: 0x00190DBC
			private bool IsHandleCreated
			{
				get
				{
					return this.window.Handle != IntPtr.Zero;
				}
			}

			// Token: 0x06006DF3 RID: 28147 RVA: 0x00192BD3 File Offset: 0x00190DD3
			private void AssignId(StatusBar.ControlToolTip.Tool tool)
			{
				tool.id = (IntPtr)this.nextId;
				this.nextId++;
			}

			// Token: 0x06006DF4 RID: 28148 RVA: 0x00192BF4 File Offset: 0x00190DF4
			public void SetTool(object key, StatusBar.ControlToolTip.Tool tool)
			{
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				StatusBar.ControlToolTip.Tool tool2 = null;
				if (this.tools.ContainsKey(key))
				{
					tool2 = (StatusBar.ControlToolTip.Tool)this.tools[key];
				}
				if (tool2 != null)
				{
					flag = true;
				}
				if (tool != null)
				{
					flag2 = true;
				}
				if (tool != null && tool2 != null && tool.id == tool2.id)
				{
					flag3 = true;
				}
				if (flag3)
				{
					this.UpdateTool(tool);
				}
				else
				{
					if (flag)
					{
						this.RemoveTool(tool2);
					}
					if (flag2)
					{
						this.AddTool(tool);
					}
				}
				if (tool != null)
				{
					this.tools[key] = tool;
					return;
				}
				this.tools.Remove(key);
			}

			// Token: 0x06006DF5 RID: 28149 RVA: 0x00192C8B File Offset: 0x00190E8B
			public StatusBar.ControlToolTip.Tool GetTool(object key)
			{
				return (StatusBar.ControlToolTip.Tool)this.tools[key];
			}

			// Token: 0x06006DF6 RID: 28150 RVA: 0x00192CA0 File Offset: 0x00190EA0
			private void AddTool(StatusBar.ControlToolTip.Tool tool)
			{
				if (tool != null && tool.text != null && tool.text.Length > 0)
				{
					StatusBar statusBar = (StatusBar)this.parent;
					int num;
					if (statusBar.ToolTipSet)
					{
						num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(statusBar.MainToolTip, statusBar.MainToolTip.Handle), NativeMethods.TTM_ADDTOOL, 0, this.GetTOOLINFO(tool));
					}
					else
					{
						num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), NativeMethods.TTM_ADDTOOL, 0, this.GetTOOLINFO(tool));
					}
					if (num == 0)
					{
						throw new InvalidOperationException(SR.GetString("StatusBarAddFailed"));
					}
				}
			}

			// Token: 0x06006DF7 RID: 28151 RVA: 0x00192D48 File Offset: 0x00190F48
			private void RemoveTool(StatusBar.ControlToolTip.Tool tool)
			{
				if (tool != null && tool.text != null && tool.text.Length > 0 && (int)tool.id >= 0)
				{
					UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), NativeMethods.TTM_DELTOOL, 0, this.GetMinTOOLINFO(tool));
				}
			}

			// Token: 0x06006DF8 RID: 28152 RVA: 0x00192D9C File Offset: 0x00190F9C
			private void UpdateTool(StatusBar.ControlToolTip.Tool tool)
			{
				if (tool != null && tool.text != null && tool.text.Length > 0 && (int)tool.id >= 0)
				{
					UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), NativeMethods.TTM_SETTOOLINFO, 0, this.GetTOOLINFO(tool));
				}
			}

			// Token: 0x06006DF9 RID: 28153 RVA: 0x00192DF0 File Offset: 0x00190FF0
			protected void CreateHandle()
			{
				if (this.IsHandleCreated)
				{
					return;
				}
				this.window.CreateHandle(this.CreateParams);
				SafeNativeMethods.SetWindowPos(new HandleRef(this, this.Handle), NativeMethods.HWND_TOPMOST, 0, 0, 0, 0, 19);
				UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), 1048, 0, SystemInformation.MaxWindowTrackSize.Width);
			}

			// Token: 0x06006DFA RID: 28154 RVA: 0x00192E59 File Offset: 0x00191059
			protected void DestroyHandle()
			{
				if (this.IsHandleCreated)
				{
					this.window.DestroyHandle();
					this.tools.Clear();
				}
			}

			// Token: 0x06006DFB RID: 28155 RVA: 0x00192E79 File Offset: 0x00191079
			public void Dispose()
			{
				this.DestroyHandle();
			}

			// Token: 0x06006DFC RID: 28156 RVA: 0x00192E84 File Offset: 0x00191084
			private NativeMethods.TOOLINFO_T GetMinTOOLINFO(StatusBar.ControlToolTip.Tool tool)
			{
				NativeMethods.TOOLINFO_T toolinfo_T = new NativeMethods.TOOLINFO_T();
				toolinfo_T.cbSize = Marshal.SizeOf(typeof(NativeMethods.TOOLINFO_T));
				toolinfo_T.hwnd = this.parent.Handle;
				if ((int)tool.id < 0)
				{
					this.AssignId(tool);
				}
				StatusBar statusBar = (StatusBar)this.parent;
				if (statusBar != null && statusBar.ToolTipSet)
				{
					toolinfo_T.uId = this.parent.Handle;
				}
				else
				{
					toolinfo_T.uId = tool.id;
				}
				return toolinfo_T;
			}

			// Token: 0x06006DFD RID: 28157 RVA: 0x00192F0C File Offset: 0x0019110C
			private NativeMethods.TOOLINFO_T GetTOOLINFO(StatusBar.ControlToolTip.Tool tool)
			{
				NativeMethods.TOOLINFO_T minTOOLINFO = this.GetMinTOOLINFO(tool);
				minTOOLINFO.cbSize = Marshal.SizeOf(typeof(NativeMethods.TOOLINFO_T));
				minTOOLINFO.uFlags |= 272;
				Control control = this.parent;
				if (control != null && control.RightToLeft == RightToLeft.Yes)
				{
					minTOOLINFO.uFlags |= 4;
				}
				minTOOLINFO.lpszText = tool.text;
				minTOOLINFO.rect = NativeMethods.RECT.FromXYWH(tool.rect.X, tool.rect.Y, tool.rect.Width, tool.rect.Height);
				return minTOOLINFO;
			}

			// Token: 0x06006DFE RID: 28158 RVA: 0x00192FB0 File Offset: 0x001911B0
			~ControlToolTip()
			{
				this.DestroyHandle();
			}

			// Token: 0x06006DFF RID: 28159 RVA: 0x00192FDC File Offset: 0x001911DC
			protected void WndProc(ref Message msg)
			{
				int msg2 = msg.Msg;
				if (msg2 == 7)
				{
					return;
				}
				this.window.DefWndProc(ref msg);
			}

			// Token: 0x040042C7 RID: 17095
			private Hashtable tools = new Hashtable();

			// Token: 0x040042C8 RID: 17096
			private StatusBar.ControlToolTip.ToolTipNativeWindow window;

			// Token: 0x040042C9 RID: 17097
			private Control parent;

			// Token: 0x040042CA RID: 17098
			private int nextId;

			// Token: 0x020008C5 RID: 2245
			public class Tool
			{
				// Token: 0x0400454D RID: 17741
				public Rectangle rect = Rectangle.Empty;

				// Token: 0x0400454E RID: 17742
				public string text;

				// Token: 0x0400454F RID: 17743
				internal IntPtr id = new IntPtr(-1);
			}

			// Token: 0x020008C6 RID: 2246
			private class ToolTipNativeWindow : NativeWindow
			{
				// Token: 0x060072C5 RID: 29381 RVA: 0x001A30C4 File Offset: 0x001A12C4
				internal ToolTipNativeWindow(StatusBar.ControlToolTip control)
				{
					this.control = control;
				}

				// Token: 0x060072C6 RID: 29382 RVA: 0x001A30D3 File Offset: 0x001A12D3
				protected override void WndProc(ref Message m)
				{
					if (this.control != null)
					{
						this.control.WndProc(ref m);
					}
				}

				// Token: 0x04004550 RID: 17744
				private StatusBar.ControlToolTip control;
			}
		}
	}
}
