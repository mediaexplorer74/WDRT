using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Represents a single tab page in a <see cref="T:System.Windows.Forms.TabControl" />.</summary>
	// Token: 0x0200039C RID: 924
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Designer("System.Windows.Forms.Design.TabPageDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	[DefaultEvent("Click")]
	[DefaultProperty("Text")]
	public class TabPage : Panel
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TabPage" /> class.</summary>
		// Token: 0x06003C5A RID: 15450 RVA: 0x00106F3D File Offset: 0x0010513D
		public TabPage()
		{
			base.SetStyle(ControlStyles.CacheText, true);
			this.Text = null;
		}

		/// <summary>This property is not meaningful for this control.</summary>
		/// <returns>The control grows as much as necessary to fit its contents but does not shrink smaller than the value of its size property</returns>
		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x06003C5B RID: 15451 RVA: 0x00012E4E File Offset: 0x0001104E
		// (set) Token: 0x06003C5C RID: 15452 RVA: 0x000070A6 File Offset: 0x000052A6
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[Localizable(false)]
		public override AutoSizeMode AutoSizeMode
		{
			get
			{
				return AutoSizeMode.GrowOnly;
			}
			set
			{
			}
		}

		/// <summary>This property is not meaningful for this control.</summary>
		/// <returns>The default value is <see langword="false" />.</returns>
		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x06003C5D RID: 15453 RVA: 0x000FFC09 File Offset: 0x000FDE09
		// (set) Token: 0x06003C5E RID: 15454 RVA: 0x000FFC11 File Offset: 0x000FDE11
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TabPage.AutoSize" /> property changes.</summary>
		// Token: 0x140002DF RID: 735
		// (add) Token: 0x06003C5F RID: 15455 RVA: 0x000FFC1A File Offset: 0x000FDE1A
		// (remove) Token: 0x06003C60 RID: 15456 RVA: 0x000FFC23 File Offset: 0x000FDE23
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnAutoSizeChangedDescr")]
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

		/// <summary>Gets or sets the background color for the <see cref="T:System.Windows.Forms.TabPage" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the <see cref="T:System.Windows.Forms.TabPage" />.</returns>
		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x06003C61 RID: 15457 RVA: 0x00106F64 File Offset: 0x00105164
		// (set) Token: 0x06003C62 RID: 15458 RVA: 0x00106FB4 File Offset: 0x001051B4
		[SRCategory("CatAppearance")]
		[SRDescription("ControlBackColorDescr")]
		public override Color BackColor
		{
			get
			{
				Color backColor = base.BackColor;
				if (backColor != Control.DefaultBackColor)
				{
					return backColor;
				}
				TabControl tabControl = this.ParentInternal as TabControl;
				if (Application.RenderWithVisualStyles && this.UseVisualStyleBackColor && tabControl != null && tabControl.Appearance == TabAppearance.Normal)
				{
					return Color.Transparent;
				}
				return backColor;
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

		/// <summary>Creates a new instance of the control collection for the control.</summary>
		/// <returns>A new instance of <see cref="T:System.Windows.Forms.Control.ControlCollection" /> assigned to the control.</returns>
		// Token: 0x06003C63 RID: 15459 RVA: 0x00107007 File Offset: 0x00105207
		protected override Control.ControlCollection CreateControlsInstance()
		{
			return new TabPage.TabPageControlCollection(this);
		}

		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x06003C64 RID: 15460 RVA: 0x0010700F File Offset: 0x0010520F
		internal ImageList.Indexer ImageIndexer
		{
			get
			{
				if (this.imageIndexer == null)
				{
					this.imageIndexer = new ImageList.Indexer();
				}
				return this.imageIndexer;
			}
		}

		/// <summary>Gets or sets the index to the image displayed on this tab.</summary>
		/// <returns>The zero-based index to the image in the <see cref="P:System.Windows.Forms.TabControl.ImageList" /> that appears on the tab. The default is -1, which signifies no image.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Windows.Forms.TabPage.ImageIndex" /> value is less than -1.</exception>
		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x06003C65 RID: 15461 RVA: 0x0010702A File Offset: 0x0010522A
		// (set) Token: 0x06003C66 RID: 15462 RVA: 0x00107038 File Offset: 0x00105238
		[TypeConverter(typeof(ImageIndexConverter))]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[DefaultValue(-1)]
		[SRDescription("TabItemImageIndexDescr")]
		public int ImageIndex
		{
			get
			{
				return this.ImageIndexer.Index;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("ImageIndex", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"imageIndex",
						value.ToString(CultureInfo.CurrentCulture),
						(-1).ToString(CultureInfo.CurrentCulture)
					}));
				}
				TabControl tabControl = this.ParentInternal as TabControl;
				if (tabControl != null)
				{
					this.ImageIndexer.ImageList = tabControl.ImageList;
				}
				this.ImageIndexer.Index = value;
				this.UpdateParent();
			}
		}

		/// <summary>Gets or sets the key accessor for the image in the <see cref="P:System.Windows.Forms.TabControl.ImageList" /> of the associated <see cref="T:System.Windows.Forms.TabControl" />.</summary>
		/// <returns>A string representing the key of the image.</returns>
		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x06003C67 RID: 15463 RVA: 0x001070BE File Offset: 0x001052BE
		// (set) Token: 0x06003C68 RID: 15464 RVA: 0x001070CC File Offset: 0x001052CC
		[TypeConverter(typeof(ImageKeyConverter))]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Localizable(true)]
		[DefaultValue("")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRDescription("TabItemImageIndexDescr")]
		public string ImageKey
		{
			get
			{
				return this.ImageIndexer.Key;
			}
			set
			{
				this.ImageIndexer.Key = value;
				TabControl tabControl = this.ParentInternal as TabControl;
				if (tabControl != null)
				{
					this.ImageIndexer.ImageList = tabControl.ImageList;
				}
				this.UpdateParent();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TabPage" /> class and specifies the text for the tab.</summary>
		/// <param name="text">The text for the tab.</param>
		// Token: 0x06003C69 RID: 15465 RVA: 0x0010710B File Offset: 0x0010530B
		public TabPage(string text)
			: this()
		{
			this.Text = text;
		}

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AnchorStyles" /> value.</returns>
		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x06003C6A RID: 15466 RVA: 0x000FFC2C File Offset: 0x000FDE2C
		// (set) Token: 0x06003C6B RID: 15467 RVA: 0x000FFC34 File Offset: 0x000FDE34
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override AnchorStyles Anchor
		{
			get
			{
				return base.Anchor;
			}
			set
			{
				base.Anchor = value;
			}
		}

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DockStyle" /> value.</returns>
		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x06003C6C RID: 15468 RVA: 0x000FC41E File Offset: 0x000FA61E
		// (set) Token: 0x06003C6D RID: 15469 RVA: 0x000FFC4E File Offset: 0x000FDE4E
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TabPage.Dock" /> property changes.</summary>
		// Token: 0x140002E0 RID: 736
		// (add) Token: 0x06003C6E RID: 15470 RVA: 0x000FFD50 File Offset: 0x000FDF50
		// (remove) Token: 0x06003C6F RID: 15471 RVA: 0x000FFD59 File Offset: 0x000FDF59
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler DockChanged
		{
			add
			{
				base.DockChanged += value;
			}
			remove
			{
				base.DockChanged -= value;
			}
		}

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>The default is <see langword="true" />.</returns>
		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x06003C70 RID: 15472 RVA: 0x0001A0C5 File Offset: 0x000182C5
		// (set) Token: 0x06003C71 RID: 15473 RVA: 0x0001A0CD File Offset: 0x000182CD
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TabPage.Enabled" /> property changes.</summary>
		// Token: 0x140002E1 RID: 737
		// (add) Token: 0x06003C72 RID: 15474 RVA: 0x0010711A File Offset: 0x0010531A
		// (remove) Token: 0x06003C73 RID: 15475 RVA: 0x00107123 File Offset: 0x00105323
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler EnabledChanged
		{
			add
			{
				base.EnabledChanged += value;
			}
			remove
			{
				base.EnabledChanged -= value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.TabPage" /> background renders using the current visual style when visual styles are enabled.</summary>
		/// <returns>
		///   <see langword="true" /> to render the background using the current visual style; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x06003C74 RID: 15476 RVA: 0x0010712C File Offset: 0x0010532C
		// (set) Token: 0x06003C75 RID: 15477 RVA: 0x00107134 File Offset: 0x00105334
		[DefaultValue(false)]
		[SRCategory("CatAppearance")]
		[SRDescription("TabItemUseVisualStyleBackColorDescr")]
		public bool UseVisualStyleBackColor
		{
			get
			{
				return this.useVisualStyleBackColor;
			}
			set
			{
				this.useVisualStyleBackColor = value;
				base.Invalidate(true);
			}
		}

		/// <summary>This property is not meaningful for this control.</summary>
		/// <returns>The x and y coordinates which specifies the location of the object.</returns>
		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x06003C76 RID: 15478 RVA: 0x000B15D1 File Offset: 0x000AF7D1
		// (set) Token: 0x06003C77 RID: 15479 RVA: 0x000B15D9 File Offset: 0x000AF7D9
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Point Location
		{
			get
			{
				return base.Location;
			}
			set
			{
				base.Location = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TabPage.Location" /> property changes.</summary>
		// Token: 0x140002E2 RID: 738
		// (add) Token: 0x06003C78 RID: 15480 RVA: 0x000FFD62 File Offset: 0x000FDF62
		// (remove) Token: 0x06003C79 RID: 15481 RVA: 0x000FFD6B File Offset: 0x000FDF6B
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler LocationChanged
		{
			add
			{
				base.LocationChanged += value;
			}
			remove
			{
				base.LocationChanged -= value;
			}
		}

		/// <summary>This property is not meaningful for this control.</summary>
		/// <returns>The upper limit of the size of the objet.</returns>
		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x06003C7A RID: 15482 RVA: 0x00011A0E File Offset: 0x0000FC0E
		// (set) Token: 0x06003C7B RID: 15483 RVA: 0x000FFC9F File Offset: 0x000FDE9F
		[DefaultValue(typeof(Size), "0, 0")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Size MaximumSize
		{
			get
			{
				return base.MaximumSize;
			}
			set
			{
				base.MaximumSize = value;
			}
		}

		/// <summary>This property is not meaningful for this control.</summary>
		/// <returns>The lower limit of the size of the objet.</returns>
		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x06003C7C RID: 15484 RVA: 0x00011A2B File Offset: 0x0000FC2B
		// (set) Token: 0x06003C7D RID: 15485 RVA: 0x000FFC96 File Offset: 0x000FDE96
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Size MinimumSize
		{
			get
			{
				return base.MinimumSize;
			}
			set
			{
				base.MinimumSize = value;
			}
		}

		/// <summary>This property is not meaningful for this control.</summary>
		/// <returns>The size of a rectangular area into which the control can fit.</returns>
		// Token: 0x17000EC1 RID: 3777
		// (get) Token: 0x06003C7E RID: 15486 RVA: 0x00107144 File Offset: 0x00105344
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Size PreferredSize
		{
			get
			{
				return base.PreferredSize;
			}
		}

		/// <summary>This property is not meaningful for this control.</summary>
		/// <returns>The tab order of the control.</returns>
		// Token: 0x17000EC2 RID: 3778
		// (get) Token: 0x06003C7F RID: 15487 RVA: 0x000B2372 File Offset: 0x000B0572
		// (set) Token: 0x06003C80 RID: 15488 RVA: 0x000B237A File Offset: 0x000B057A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new int TabIndex
		{
			get
			{
				return base.TabIndex;
			}
			set
			{
				base.TabIndex = value;
			}
		}

		// Token: 0x17000EC3 RID: 3779
		// (get) Token: 0x06003C81 RID: 15489 RVA: 0x00012E4E File Offset: 0x0001104E
		internal override bool RenderTransparencyWithVisualStyles
		{
			get
			{
				return true;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TabPage.TabIndex" /> property changes.</summary>
		// Token: 0x140002E3 RID: 739
		// (add) Token: 0x06003C82 RID: 15490 RVA: 0x000B2383 File Offset: 0x000B0583
		// (remove) Token: 0x06003C83 RID: 15491 RVA: 0x000B238C File Offset: 0x000B058C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TabIndexChanged
		{
			add
			{
				base.TabIndexChanged += value;
			}
			remove
			{
				base.TabIndexChanged -= value;
			}
		}

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>The default is <see langword="true" />.</returns>
		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x06003C84 RID: 15492 RVA: 0x000FFCE8 File Offset: 0x000FDEE8
		// (set) Token: 0x06003C85 RID: 15493 RVA: 0x000FFCF0 File Offset: 0x000FDEF0
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TabPage.TabStop" /> property changes.</summary>
		// Token: 0x140002E4 RID: 740
		// (add) Token: 0x06003C86 RID: 15494 RVA: 0x000B23A6 File Offset: 0x000B05A6
		// (remove) Token: 0x06003C87 RID: 15495 RVA: 0x000B23AF File Offset: 0x000B05AF
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

		/// <summary>Gets or sets the text to display on the tab.</summary>
		/// <returns>The text to display on the tab.</returns>
		// Token: 0x17000EC5 RID: 3781
		// (get) Token: 0x06003C88 RID: 15496 RVA: 0x0010714C File Offset: 0x0010534C
		// (set) Token: 0x06003C89 RID: 15497 RVA: 0x00107154 File Offset: 0x00105354
		[Localizable(true)]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
				this.UpdateParent();
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TabControl.Text" /> property changes.</summary>
		// Token: 0x140002E5 RID: 741
		// (add) Token: 0x06003C8A RID: 15498 RVA: 0x00107163 File Offset: 0x00105363
		// (remove) Token: 0x06003C8B RID: 15499 RVA: 0x0010716C File Offset: 0x0010536C
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
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

		/// <summary>Gets or sets the ToolTip text for this tab.</summary>
		/// <returns>The ToolTip text for this tab.</returns>
		// Token: 0x17000EC6 RID: 3782
		// (get) Token: 0x06003C8C RID: 15500 RVA: 0x00107175 File Offset: 0x00105375
		// (set) Token: 0x06003C8D RID: 15501 RVA: 0x0010717D File Offset: 0x0010537D
		[DefaultValue("")]
		[Localizable(true)]
		[SRDescription("TabItemToolTipTextDescr")]
		public string ToolTipText
		{
			get
			{
				return this.toolTipText;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (value == this.toolTipText)
				{
					return;
				}
				this.toolTipText = value;
				this.UpdateParent();
			}
		}

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>The default is <see langword="true" />.</returns>
		// Token: 0x17000EC7 RID: 3783
		// (get) Token: 0x06003C8E RID: 15502 RVA: 0x000FFCF9 File Offset: 0x000FDEF9
		// (set) Token: 0x06003C8F RID: 15503 RVA: 0x000FFD01 File Offset: 0x000FDF01
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				base.Visible = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TabPage.Visible" /> property changes.</summary>
		// Token: 0x140002E6 RID: 742
		// (add) Token: 0x06003C90 RID: 15504 RVA: 0x000FFD3E File Offset: 0x000FDF3E
		// (remove) Token: 0x06003C91 RID: 15505 RVA: 0x000FFD47 File Offset: 0x000FDF47
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler VisibleChanged
		{
			add
			{
				base.VisibleChanged += value;
			}
			remove
			{
				base.VisibleChanged -= value;
			}
		}

		// Token: 0x06003C92 RID: 15506 RVA: 0x001071A5 File Offset: 0x001053A5
		internal override void AssignParent(Control value)
		{
			if (value != null && !(value is TabControl))
			{
				throw new ArgumentException(SR.GetString("TABCONTROLTabPageNotOnTabControl", new object[] { value.GetType().FullName }));
			}
			base.AssignParent(value);
		}

		/// <summary>Retrieves the tab page that contains the specified object.</summary>
		/// <param name="comp">The object to look for.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.TabPage" /> that contains the specified object, or <see langword="null" /> if the object cannot be found.</returns>
		// Token: 0x06003C93 RID: 15507 RVA: 0x001071E0 File Offset: 0x001053E0
		public static TabPage GetTabPageOfComponent(object comp)
		{
			if (!(comp is Control))
			{
				return null;
			}
			Control control = (Control)comp;
			while (control != null && !(control is TabPage))
			{
				control = control.ParentInternal;
			}
			return (TabPage)control;
		}

		// Token: 0x06003C94 RID: 15508 RVA: 0x00107218 File Offset: 0x00105418
		internal NativeMethods.TCITEM_T GetTCITEM()
		{
			NativeMethods.TCITEM_T tcitem_T = new NativeMethods.TCITEM_T();
			tcitem_T.mask = 0;
			tcitem_T.pszText = null;
			tcitem_T.cchTextMax = 0;
			tcitem_T.lParam = IntPtr.Zero;
			string text = this.Text;
			this.PrefixAmpersands(ref text);
			if (text != null)
			{
				tcitem_T.mask |= 1;
				tcitem_T.pszText = text;
				tcitem_T.cchTextMax = text.Length;
			}
			int imageIndex = this.ImageIndex;
			tcitem_T.mask |= 2;
			tcitem_T.iImage = this.ImageIndexer.ActualIndex;
			return tcitem_T;
		}

		// Token: 0x06003C95 RID: 15509 RVA: 0x001072A8 File Offset: 0x001054A8
		private void PrefixAmpersands(ref string value)
		{
			if (value == null || value.Length == 0)
			{
				return;
			}
			if (value.IndexOf('&') < 0)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < value.Length; i++)
			{
				if (value[i] == '&')
				{
					if (i < value.Length - 1 && value[i + 1] == '&')
					{
						i++;
					}
					stringBuilder.Append("&&");
				}
				else
				{
					stringBuilder.Append(value[i]);
				}
			}
			value = stringBuilder.ToString();
		}

		// Token: 0x06003C96 RID: 15510 RVA: 0x00107337 File Offset: 0x00105537
		internal void FireLeave(EventArgs e)
		{
			this.leaveFired = true;
			this.OnLeave(e);
		}

		// Token: 0x06003C97 RID: 15511 RVA: 0x00107347 File Offset: 0x00105547
		internal void FireEnter(EventArgs e)
		{
			this.enterFired = true;
			this.OnEnter(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Enter" /> event of the <see cref="T:System.Windows.Forms.TabPage" />.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003C98 RID: 15512 RVA: 0x00107358 File Offset: 0x00105558
		protected override void OnEnter(EventArgs e)
		{
			TabControl tabControl = this.ParentInternal as TabControl;
			if (tabControl != null)
			{
				if (this.enterFired)
				{
					base.OnEnter(e);
				}
				this.enterFired = false;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Leave" /> event of the <see cref="T:System.Windows.Forms.TabPage" />.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003C99 RID: 15513 RVA: 0x0010738C File Offset: 0x0010558C
		protected override void OnLeave(EventArgs e)
		{
			TabControl tabControl = this.ParentInternal as TabControl;
			if (tabControl != null)
			{
				if (this.leaveFired)
				{
					base.OnLeave(e);
				}
				this.leaveFired = false;
			}
		}

		/// <summary>Paints the background of the <see cref="T:System.Windows.Forms.TabPage" />.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains data useful for painting the background.</param>
		// Token: 0x06003C9A RID: 15514 RVA: 0x001073C0 File Offset: 0x001055C0
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			TabControl tabControl = this.ParentInternal as TabControl;
			if (Application.RenderWithVisualStyles && this.UseVisualStyleBackColor && tabControl != null && tabControl.Appearance == TabAppearance.Normal)
			{
				Color color = (this.UseVisualStyleBackColor ? Color.Transparent : this.BackColor);
				Rectangle rectangle = LayoutUtils.InflateRect(this.DisplayRectangle, base.Padding);
				Rectangle rectangle2 = new Rectangle(rectangle.X - 4, rectangle.Y - 2, rectangle.Width + 8, rectangle.Height + 6);
				TabRenderer.DrawTabPage(e.Graphics, rectangle2);
				if (this.BackgroundImage != null)
				{
					ControlPaint.DrawBackgroundImage(e.Graphics, this.BackgroundImage, color, this.BackgroundImageLayout, rectangle, rectangle, this.DisplayRectangle.Location);
					return;
				}
			}
			else
			{
				base.OnPaintBackground(e);
			}
		}

		/// <summary>This member overrides <see cref="M:System.Windows.Forms.Control.SetBoundsCore(System.Int32,System.Int32,System.Int32,System.Int32,System.Windows.Forms.BoundsSpecified)" />.</summary>
		/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control.</param>
		/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control.</param>
		/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control.</param>
		/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control.</param>
		/// <param name="specified">A bitwise combination of <see cref="T:System.Windows.Forms.BoundsSpecified" /> values.</param>
		// Token: 0x06003C9B RID: 15515 RVA: 0x00107498 File Offset: 0x00105698
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			Control parentInternal = this.ParentInternal;
			if (parentInternal is TabControl && parentInternal.IsHandleCreated)
			{
				Rectangle displayRectangle = parentInternal.DisplayRectangle;
				base.SetBoundsCore(displayRectangle.X, displayRectangle.Y, displayRectangle.Width, displayRectangle.Height, (specified == BoundsSpecified.None) ? BoundsSpecified.None : BoundsSpecified.All);
				return;
			}
			base.SetBoundsCore(x, y, width, height, specified);
		}

		// Token: 0x06003C9C RID: 15516 RVA: 0x000B658D File Offset: 0x000B478D
		[EditorBrowsable(EditorBrowsableState.Never)]
		private bool ShouldSerializeLocation()
		{
			return base.Left != 0 || base.Top != 0;
		}

		/// <summary>Returns a string containing the value of the <see cref="P:System.Windows.Forms.TabPage.Text" /> property.</summary>
		/// <returns>A string containing the value of the <see cref="P:System.Windows.Forms.TabPage.Text" /> property.</returns>
		// Token: 0x06003C9D RID: 15517 RVA: 0x001074FC File Offset: 0x001056FC
		public override string ToString()
		{
			return "TabPage: {" + this.Text + "}";
		}

		// Token: 0x06003C9E RID: 15518 RVA: 0x00107514 File Offset: 0x00105714
		internal void UpdateParent()
		{
			TabControl tabControl = this.ParentInternal as TabControl;
			if (tabControl != null)
			{
				tabControl.UpdateTab(this);
			}
		}

		// Token: 0x04002395 RID: 9109
		private ImageList.Indexer imageIndexer;

		// Token: 0x04002396 RID: 9110
		private string toolTipText = "";

		// Token: 0x04002397 RID: 9111
		private bool enterFired;

		// Token: 0x04002398 RID: 9112
		private bool leaveFired;

		// Token: 0x04002399 RID: 9113
		private bool useVisualStyleBackColor;

		/// <summary>Contains the collection of controls that the <see cref="T:System.Windows.Forms.TabPage" /> uses.</summary>
		// Token: 0x020007F1 RID: 2033
		[ComVisible(false)]
		public class TabPageControlCollection : Control.ControlCollection
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TabPage.TabPageControlCollection" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.TabPage" /> that contains this collection of controls.</param>
			// Token: 0x06006E47 RID: 28231 RVA: 0x00193D37 File Offset: 0x00191F37
			public TabPageControlCollection(TabPage owner)
				: base(owner)
			{
			}

			/// <summary>Adds a control to the collection.</summary>
			/// <param name="value">The control to add.</param>
			/// <exception cref="T:System.ArgumentException">The specified control is a <see cref="T:System.Windows.Forms.TabPage" />.</exception>
			// Token: 0x06006E48 RID: 28232 RVA: 0x00193D40 File Offset: 0x00191F40
			public override void Add(Control value)
			{
				if (value is TabPage)
				{
					throw new ArgumentException(SR.GetString("TABCONTROLTabPageOnTabPage"));
				}
				base.Add(value);
			}
		}
	}
}
