using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Security.Permissions;
using System.Windows.Forms.Design;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Represents a combination of a standard button on the left and a drop-down button on the right, or the other way around if the value of <see cref="T:System.Windows.Forms.RightToLeft" /> is <see langword="Yes" />.</summary>
	// Token: 0x02000402 RID: 1026
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.StatusStrip)]
	[DefaultEvent("ButtonClick")]
	public class ToolStripSplitButton : ToolStripDropDownItem
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> class.</summary>
		// Token: 0x060046DF RID: 18143 RVA: 0x00129530 File Offset: 0x00127730
		public ToolStripSplitButton()
		{
			this.Initialize();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> class with the specified text.</summary>
		/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</param>
		// Token: 0x060046E0 RID: 18144 RVA: 0x0012955B File Offset: 0x0012775B
		public ToolStripSplitButton(string text)
			: base(text, null, null)
		{
			this.Initialize();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> class with the specified image.</summary>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</param>
		// Token: 0x060046E1 RID: 18145 RVA: 0x00129589 File Offset: 0x00127789
		public ToolStripSplitButton(Image image)
			: base(null, image, null)
		{
			this.Initialize();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> class with the specified text and image.</summary>
		/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</param>
		// Token: 0x060046E2 RID: 18146 RVA: 0x001295B7 File Offset: 0x001277B7
		public ToolStripSplitButton(string text, Image image)
			: base(text, image, null)
		{
			this.Initialize();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> class with the specified display text, image, and <see cref="E:System.Windows.Forms.Control.Click" /> event handler.</summary>
		/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</param>
		/// <param name="onClick">Raises the <see cref="E:System.Windows.Forms.Control.Click" /> event when the user clicks the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</param>
		// Token: 0x060046E3 RID: 18147 RVA: 0x001295E5 File Offset: 0x001277E5
		public ToolStripSplitButton(string text, Image image, EventHandler onClick)
			: base(text, image, onClick)
		{
			this.Initialize();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> class with the specified display text, image, <see cref="E:System.Windows.Forms.Control.Click" /> event handler, and name.</summary>
		/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</param>
		/// <param name="onClick">Raises the <see cref="E:System.Windows.Forms.Control.Click" /> event when the user clicks the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</param>
		/// <param name="name">The name of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</param>
		// Token: 0x060046E4 RID: 18148 RVA: 0x00129613 File Offset: 0x00127813
		public ToolStripSplitButton(string text, Image image, EventHandler onClick, string name)
			: base(text, image, onClick, name)
		{
			this.Initialize();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> class with the specified text, image, and <see cref="T:System.Windows.Forms.ToolStripItem" /> array.</summary>
		/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</param>
		/// <param name="dropDownItems">A <see cref="T:System.Windows.Forms.ToolStripItem" /> array of controls.</param>
		// Token: 0x060046E5 RID: 18149 RVA: 0x00129643 File Offset: 0x00127843
		public ToolStripSplitButton(string text, Image image, params ToolStripItem[] dropDownItems)
			: base(text, image, dropDownItems)
		{
			this.Initialize();
		}

		/// <summary>Gets or sets a value indicating whether default or custom <see cref="T:System.Windows.Forms.ToolTip" /> text is displayed on the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</summary>
		/// <returns>
		///   <see langword="true" /> if default <see cref="T:System.Windows.Forms.ToolTip" /> text is displayed; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17001165 RID: 4453
		// (get) Token: 0x060046E6 RID: 18150 RVA: 0x00110E48 File Offset: 0x0010F048
		// (set) Token: 0x060046E7 RID: 18151 RVA: 0x00110E50 File Offset: 0x0010F050
		[DefaultValue(true)]
		public new bool AutoToolTip
		{
			get
			{
				return base.AutoToolTip;
			}
			set
			{
				base.AutoToolTip = value;
			}
		}

		/// <summary>Gets the size and location of the standard button portion of a <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the size and location of the standard button portion of a <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</returns>
		// Token: 0x17001166 RID: 4454
		// (get) Token: 0x060046E8 RID: 18152 RVA: 0x00129671 File Offset: 0x00127871
		[Browsable(false)]
		public Rectangle ButtonBounds
		{
			get
			{
				return this.SplitButtonButton.Bounds;
			}
		}

		/// <summary>Gets a value indicating whether the button portion of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> is in the pressed state.</summary>
		/// <returns>
		///   <see langword="true" /> if the button portion of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> is in the pressed state; otherwise, <see langword="false" />.</returns>
		// Token: 0x17001167 RID: 4455
		// (get) Token: 0x060046E9 RID: 18153 RVA: 0x0012967E File Offset: 0x0012787E
		[Browsable(false)]
		public bool ButtonPressed
		{
			get
			{
				return this.SplitButtonButton.Pressed;
			}
		}

		/// <summary>Gets a value indicating whether the standard button portion of a <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> is selected or the <see cref="P:System.Windows.Forms.ToolStripSplitButton.DropDownButtonPressed" /> property is <see langword="true" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the button portion of a <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> is selected or whether <see cref="P:System.Windows.Forms.ToolStripSplitButton.DropDownButtonPressed" /> is <see langword="true" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17001168 RID: 4456
		// (get) Token: 0x060046EA RID: 18154 RVA: 0x0012968B File Offset: 0x0012788B
		[Browsable(false)]
		public bool ButtonSelected
		{
			get
			{
				return this.SplitButtonButton.Selected || this.DropDownButtonPressed;
			}
		}

		/// <summary>Occurs when the standard button portion of a <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> is clicked.</summary>
		// Token: 0x1400038B RID: 907
		// (add) Token: 0x060046EB RID: 18155 RVA: 0x001296A2 File Offset: 0x001278A2
		// (remove) Token: 0x060046EC RID: 18156 RVA: 0x001296B5 File Offset: 0x001278B5
		[SRCategory("CatAction")]
		[SRDescription("ToolStripSplitButtonOnButtonClickDescr")]
		public event EventHandler ButtonClick
		{
			add
			{
				base.Events.AddHandler(ToolStripSplitButton.EventButtonClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripSplitButton.EventButtonClick, value);
			}
		}

		/// <summary>Occurs when the standard button portion of a <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> is double-clicked.</summary>
		// Token: 0x1400038C RID: 908
		// (add) Token: 0x060046ED RID: 18157 RVA: 0x001296C8 File Offset: 0x001278C8
		// (remove) Token: 0x060046EE RID: 18158 RVA: 0x001296DB File Offset: 0x001278DB
		[SRCategory("CatAction")]
		[SRDescription("ToolStripSplitButtonOnButtonDoubleClickDescr")]
		public event EventHandler ButtonDoubleClick
		{
			add
			{
				base.Events.AddHandler(ToolStripSplitButton.EventButtonDoubleClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripSplitButton.EventButtonDoubleClick, value);
			}
		}

		/// <summary>Gets a value indicating whether to display the <see cref="T:System.Windows.Forms.ToolTip" /> that is defined as the default.</summary>
		/// <returns>
		///   <see langword="true" /> in all cases.</returns>
		// Token: 0x17001169 RID: 4457
		// (get) Token: 0x060046EF RID: 18159 RVA: 0x00012E4E File Offset: 0x0001104E
		protected override bool DefaultAutoToolTip
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets or sets the portion of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> that is activated when the control is first selected.</summary>
		/// <returns>A <see langword="Forms.ToolStripItem" /> representing the portion of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> that is activated when first selected. The default value is <see langword="null" />.</returns>
		// Token: 0x1700116A RID: 4458
		// (get) Token: 0x060046F0 RID: 18160 RVA: 0x001296EE File Offset: 0x001278EE
		// (set) Token: 0x060046F1 RID: 18161 RVA: 0x001296F6 File Offset: 0x001278F6
		[DefaultValue(null)]
		[Browsable(false)]
		public ToolStripItem DefaultItem
		{
			get
			{
				return this.defaultItem;
			}
			set
			{
				if (this.defaultItem != value)
				{
					this.OnDefaultItemChanged(new EventArgs());
					this.defaultItem = value;
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolStripSplitButton.DefaultItem" /> has changed.</summary>
		// Token: 0x1400038D RID: 909
		// (add) Token: 0x060046F2 RID: 18162 RVA: 0x00129713 File Offset: 0x00127913
		// (remove) Token: 0x060046F3 RID: 18163 RVA: 0x00129726 File Offset: 0x00127926
		[SRCategory("CatAction")]
		[SRDescription("ToolStripSplitButtonOnDefaultItemChangedDescr")]
		public event EventHandler DefaultItemChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripSplitButton.EventDefaultItemChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripSplitButton.EventDefaultItemChanged, value);
			}
		}

		/// <summary>Gets a value indicating whether items on a <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> are hidden after they are clicked.</summary>
		/// <returns>
		///   <see langword="true" /> if the items are hidden after they are clicked; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700116B RID: 4459
		// (get) Token: 0x060046F4 RID: 18164 RVA: 0x00129739 File Offset: 0x00127939
		protected internal override bool DismissWhenClicked
		{
			get
			{
				return !base.DropDown.Visible;
			}
		}

		// Token: 0x1700116C RID: 4460
		// (get) Token: 0x060046F5 RID: 18165 RVA: 0x00129749 File Offset: 0x00127949
		internal override Rectangle DropDownButtonArea
		{
			get
			{
				return this.DropDownButtonBounds;
			}
		}

		/// <summary>Gets the size and location, in screen coordinates, of the drop-down button portion of a <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the size and location of the drop-down button portion of a <see cref="T:System.Windows.Forms.ToolStripSplitButton" />, in screen coordinates.</returns>
		// Token: 0x1700116D RID: 4461
		// (get) Token: 0x060046F6 RID: 18166 RVA: 0x00129751 File Offset: 0x00127951
		[Browsable(false)]
		public Rectangle DropDownButtonBounds
		{
			get
			{
				return this.dropDownButtonBounds;
			}
		}

		/// <summary>Gets a value indicating whether the drop-down portion of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> is in the pressed state.</summary>
		/// <returns>
		///   <see langword="true" /> if the drop-down portion of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> is in the pressed state; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700116E RID: 4462
		// (get) Token: 0x060046F7 RID: 18167 RVA: 0x00129759 File Offset: 0x00127959
		[Browsable(false)]
		public bool DropDownButtonPressed
		{
			get
			{
				return base.DropDown.Visible;
			}
		}

		/// <summary>Gets a value indicating whether the drop-down button portion of a <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> is selected.</summary>
		/// <returns>
		///   <see langword="true" /> if the drop-down button portion of a <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> is selected; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700116F RID: 4463
		// (get) Token: 0x060046F8 RID: 18168 RVA: 0x00129766 File Offset: 0x00127966
		[Browsable(false)]
		public bool DropDownButtonSelected
		{
			get
			{
				return this.Selected;
			}
		}

		/// <summary>The width, in pixels, of the drop-down button portion of a <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the width in pixels. The default is 11. Starting with the .NET Framework 4.6, the default value is based on the DPI setting of the device running the app.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is less than zero (0).</exception>
		// Token: 0x17001170 RID: 4464
		// (get) Token: 0x060046F9 RID: 18169 RVA: 0x0012976E File Offset: 0x0012796E
		// (set) Token: 0x060046FA RID: 18170 RVA: 0x00129778 File Offset: 0x00127978
		[SRCategory("CatLayout")]
		[SRDescription("ToolStripSplitButtonDropDownButtonWidthDescr")]
		public int DropDownButtonWidth
		{
			get
			{
				return this.dropDownButtonWidth;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("DropDownButtonWidth", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"DropDownButtonWidth",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this.dropDownButtonWidth != value)
				{
					this.dropDownButtonWidth = value;
					this.InvalidateSplitButtonLayout();
					base.InvalidateItemLayout(PropertyNames.DropDownButtonWidth, true);
				}
			}
		}

		// Token: 0x17001171 RID: 4465
		// (get) Token: 0x060046FB RID: 18171 RVA: 0x001297EE File Offset: 0x001279EE
		private int DefaultDropDownButtonWidth
		{
			get
			{
				if (!ToolStripSplitButton.isScalingInitialized)
				{
					if (DpiHelper.IsScalingRequired)
					{
						ToolStripSplitButton.scaledDropDownButtonWidth = DpiHelper.LogicalToDeviceUnitsX(11);
					}
					ToolStripSplitButton.isScalingInitialized = true;
				}
				return ToolStripSplitButton.scaledDropDownButtonWidth;
			}
		}

		// Token: 0x17001172 RID: 4466
		// (get) Token: 0x060046FC RID: 18172 RVA: 0x00129818 File Offset: 0x00127A18
		private ToolStripSplitButton.ToolStripSplitButtonButton SplitButtonButton
		{
			get
			{
				if (this.splitButtonButton == null)
				{
					this.splitButtonButton = new ToolStripSplitButton.ToolStripSplitButtonButton(this);
				}
				this.splitButtonButton.Image = this.Image;
				this.splitButtonButton.Text = this.Text;
				this.splitButtonButton.BackColor = this.BackColor;
				this.splitButtonButton.ForeColor = this.ForeColor;
				this.splitButtonButton.Font = this.Font;
				this.splitButtonButton.ImageAlign = base.ImageAlign;
				this.splitButtonButton.TextAlign = this.TextAlign;
				this.splitButtonButton.TextImageRelation = base.TextImageRelation;
				return this.splitButtonButton;
			}
		}

		// Token: 0x17001173 RID: 4467
		// (get) Token: 0x060046FD RID: 18173 RVA: 0x001298C7 File Offset: 0x00127AC7
		internal ToolStripItemInternalLayout SplitButtonButtonLayout
		{
			get
			{
				if (base.InternalLayout != null && this.splitButtonButtonLayout == null)
				{
					this.splitButtonButtonLayout = new ToolStripSplitButton.ToolStripSplitButtonButtonLayout(this);
				}
				return this.splitButtonButtonLayout;
			}
		}

		// Token: 0x17001174 RID: 4468
		// (get) Token: 0x060046FE RID: 18174 RVA: 0x001298EB File Offset: 0x00127AEB
		// (set) Token: 0x060046FF RID: 18175 RVA: 0x001298F3 File Offset: 0x00127AF3
		[SRDescription("ToolStripSplitButtonSplitterWidthDescr")]
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal int SplitterWidth
		{
			get
			{
				return this.splitterWidth;
			}
			set
			{
				if (value < 0)
				{
					this.splitterWidth = 0;
				}
				else
				{
					this.splitterWidth = value;
				}
				this.InvalidateSplitButtonLayout();
			}
		}

		/// <summary>Gets the boundaries of the separator between the standard and drop-down button portions of a <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the size and location of the separator.</returns>
		// Token: 0x17001175 RID: 4469
		// (get) Token: 0x06004700 RID: 18176 RVA: 0x0012990F File Offset: 0x00127B0F
		[Browsable(false)]
		public Rectangle SplitterBounds
		{
			get
			{
				return this.splitterBounds;
			}
		}

		// Token: 0x06004701 RID: 18177 RVA: 0x00129918 File Offset: 0x00127B18
		private void CalculateLayout()
		{
			Rectangle rectangle = new Rectangle(Point.Empty, this.Size);
			Rectangle empty = Rectangle.Empty;
			rectangle = new Rectangle(Point.Empty, new Size(Math.Min(base.Width, this.DropDownButtonWidth), base.Height));
			int num = Math.Max(0, base.Width - rectangle.Width);
			int num2 = Math.Max(0, base.Height);
			empty = new Rectangle(Point.Empty, new Size(num, num2));
			empty.Width -= this.splitterWidth;
			if (this.RightToLeft == RightToLeft.No)
			{
				rectangle.Offset(empty.Right + this.splitterWidth, 0);
				this.splitterBounds = new Rectangle(empty.Right, empty.Top, this.splitterWidth, empty.Height);
			}
			else
			{
				empty.Offset(this.DropDownButtonWidth + this.splitterWidth, 0);
				this.splitterBounds = new Rectangle(rectangle.Right, rectangle.Top, this.splitterWidth, rectangle.Height);
			}
			this.SplitButtonButton.SetBounds(empty);
			this.SetDropDownButtonBounds(rectangle);
		}

		/// <summary>Creates a new accessibility object for the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</summary>
		/// <returns>A new accessibility object for the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</returns>
		// Token: 0x06004702 RID: 18178 RVA: 0x00129A42 File Offset: 0x00127C42
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level3)
			{
				return new ToolStripSplitButton.ToolStripSplitButtonUiaProvider(this);
			}
			if (AccessibilityImprovements.Level1)
			{
				return new ToolStripSplitButton.ToolStripSplitButtonExAccessibleObject(this);
			}
			return new ToolStripSplitButton.ToolStripSplitButtonAccessibleObject(this);
		}

		/// <summary>Creates a generic <see cref="T:System.Windows.Forms.ToolStripDropDown" /> for which events can be defined.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripDropDown" />.</returns>
		// Token: 0x06004703 RID: 18179 RVA: 0x00114874 File Offset: 0x00112A74
		protected override ToolStripDropDown CreateDefaultDropDown()
		{
			return new ToolStripDropDownMenu(this, true);
		}

		// Token: 0x06004704 RID: 18180 RVA: 0x00129A66 File Offset: 0x00127C66
		internal override ToolStripItemInternalLayout CreateInternalLayout()
		{
			this.splitButtonButtonLayout = null;
			return new ToolStripItemInternalLayout(this);
		}

		/// <summary>Retrieves the size of a rectangular area into which a <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> can be fitted.</summary>
		/// <param name="constrainingSize">The custom-sized area for a control.</param>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" />, representing the width and height of a rectangle.</returns>
		// Token: 0x06004705 RID: 18181 RVA: 0x00129A78 File Offset: 0x00127C78
		public override Size GetPreferredSize(Size constrainingSize)
		{
			Size preferredSize = this.SplitButtonButtonLayout.GetPreferredSize(constrainingSize);
			preferredSize.Width += this.DropDownButtonWidth + this.SplitterWidth + this.Padding.Horizontal;
			return preferredSize;
		}

		// Token: 0x06004706 RID: 18182 RVA: 0x00129ABD File Offset: 0x00127CBD
		private void InvalidateSplitButtonLayout()
		{
			this.splitButtonButtonLayout = null;
			this.CalculateLayout();
		}

		// Token: 0x06004707 RID: 18183 RVA: 0x00129ACC File Offset: 0x00127CCC
		private void Initialize()
		{
			this.dropDownButtonWidth = this.DefaultDropDownButtonWidth;
			base.SupportsSpaceKey = true;
		}

		/// <summary>Processes a dialog key.</summary>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
		/// <returns>
		///   <see langword="true" /> if the key was processed by the item; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004708 RID: 18184 RVA: 0x00129AE1 File Offset: 0x00127CE1
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected internal override bool ProcessDialogKey(Keys keyData)
		{
			if (this.Enabled && (keyData == Keys.Return || (base.SupportsSpaceKey && keyData == Keys.Space)))
			{
				this.PerformButtonClick();
				return true;
			}
			return base.ProcessDialogKey(keyData);
		}

		/// <summary>Processes a mnemonic character.</summary>
		/// <param name="charCode">The character to process.</param>
		/// <returns>
		///   <see langword="true" /> in all cases.</returns>
		// Token: 0x06004709 RID: 18185 RVA: 0x00129B0C File Offset: 0x00127D0C
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected internal override bool ProcessMnemonic(char charCode)
		{
			this.PerformButtonClick();
			return true;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripSplitButton.ButtonClick" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600470A RID: 18186 RVA: 0x00129B18 File Offset: 0x00127D18
		protected virtual void OnButtonClick(EventArgs e)
		{
			if (this.DefaultItem != null)
			{
				this.DefaultItem.FireEvent(ToolStripItemEventType.Click);
			}
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripSplitButton.EventButtonClick];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripSplitButton.ButtonDoubleClick" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600470B RID: 18187 RVA: 0x00129B5C File Offset: 0x00127D5C
		public virtual void OnButtonDoubleClick(EventArgs e)
		{
			if (this.DefaultItem != null)
			{
				this.DefaultItem.FireEvent(ToolStripItemEventType.DoubleClick);
			}
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripSplitButton.EventButtonDoubleClick];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripSplitButton.DefaultItemChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600470C RID: 18188 RVA: 0x00129BA0 File Offset: 0x00127DA0
		protected virtual void OnDefaultItemChanged(EventArgs e)
		{
			this.InvalidateSplitButtonLayout();
			if (this.CanRaiseEvents)
			{
				EventHandler eventHandler = base.Events[ToolStripSplitButton.EventDefaultItemChanged] as EventHandler;
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x0600470D RID: 18189 RVA: 0x00129BDC File Offset: 0x00127DDC
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (this.DropDownButtonBounds.Contains(e.Location))
			{
				if (e.Button == MouseButtons.Left && !base.DropDown.Visible)
				{
					this.openMouseId = ((base.ParentInternal == null) ? 0 : base.ParentInternal.GetMouseId());
					base.ShowDropDown(true);
					return;
				}
			}
			else
			{
				this.SplitButtonButton.Push(true);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x0600470E RID: 18190 RVA: 0x00129C4C File Offset: 0x00127E4C
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (!this.Enabled)
			{
				return;
			}
			this.SplitButtonButton.Push(false);
			if (this.DropDownButtonBounds.Contains(e.Location) && e.Button == MouseButtons.Left && base.DropDown.Visible)
			{
				byte b = ((base.ParentInternal == null) ? 0 : base.ParentInternal.GetMouseId());
				if (b != this.openMouseId)
				{
					this.openMouseId = 0;
					ToolStripManager.ModalMenuFilter.CloseActiveDropDown(base.DropDown, ToolStripDropDownCloseReason.AppClicked);
					base.Select();
				}
			}
			Point point = new Point(e.X, e.Y);
			if (e.Button == MouseButtons.Left && this.SplitButtonButton.Bounds.Contains(point))
			{
				bool flag = false;
				if (base.DoubleClickEnabled)
				{
					long ticks = DateTime.Now.Ticks;
					long num = ticks - this.lastClickTime;
					this.lastClickTime = ticks;
					if (num >= 0L && num < ToolStripItem.DoubleClickTicks)
					{
						flag = true;
					}
				}
				if (flag)
				{
					this.OnButtonDoubleClick(new EventArgs());
					this.lastClickTime = 0L;
					return;
				}
				this.OnButtonClick(new EventArgs());
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600470F RID: 18191 RVA: 0x00129D6D File Offset: 0x00127F6D
		protected override void OnMouseLeave(EventArgs e)
		{
			this.openMouseId = 0;
			this.SplitButtonButton.Push(false);
			base.OnMouseLeave(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.RightToLeftChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06004710 RID: 18192 RVA: 0x00129D89 File Offset: 0x00127F89
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
			this.InvalidateSplitButtonLayout();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.Paint" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		// Token: 0x06004711 RID: 18193 RVA: 0x00129D98 File Offset: 0x00127F98
		protected override void OnPaint(PaintEventArgs e)
		{
			ToolStripRenderer renderer = base.Renderer;
			if (renderer != null)
			{
				this.InvalidateSplitButtonLayout();
				Graphics graphics = e.Graphics;
				renderer.DrawSplitButton(new ToolStripItemRenderEventArgs(graphics, this));
				if ((this.DisplayStyle & ToolStripItemDisplayStyle.Image) != ToolStripItemDisplayStyle.None)
				{
					renderer.DrawItemImage(new ToolStripItemImageRenderEventArgs(graphics, this, this.SplitButtonButtonLayout.ImageRectangle));
				}
				if ((this.DisplayStyle & ToolStripItemDisplayStyle.Text) != ToolStripItemDisplayStyle.None)
				{
					renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(graphics, this, this.SplitButtonButton.Text, this.SplitButtonButtonLayout.TextRectangle, this.ForeColor, this.Font, this.SplitButtonButtonLayout.TextFormat));
				}
			}
		}

		/// <summary>If the <see cref="P:System.Windows.Forms.ToolStripItem.Enabled" /> property is <see langword="true" />, calls the <see cref="M:System.Windows.Forms.ToolStripSplitButton.OnButtonClick(System.EventArgs)" /> method.</summary>
		// Token: 0x06004712 RID: 18194 RVA: 0x00129E32 File Offset: 0x00128032
		public void PerformButtonClick()
		{
			if (this.Enabled && base.Available)
			{
				base.PerformClick();
				this.OnButtonClick(EventArgs.Empty);
			}
		}

		/// <summary>This method is not relevant to this class.</summary>
		// Token: 0x06004713 RID: 18195 RVA: 0x00129E55 File Offset: 0x00128055
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetDropDownButtonWidth()
		{
			this.DropDownButtonWidth = this.DefaultDropDownButtonWidth;
		}

		// Token: 0x06004714 RID: 18196 RVA: 0x00129E63 File Offset: 0x00128063
		private void SetDropDownButtonBounds(Rectangle rect)
		{
			this.dropDownButtonBounds = rect;
		}

		// Token: 0x06004715 RID: 18197 RVA: 0x00129E6C File Offset: 0x0012806C
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeDropDownButtonWidth()
		{
			return this.DropDownButtonWidth != this.DefaultDropDownButtonWidth;
		}

		// Token: 0x040026B1 RID: 9905
		private ToolStripItem defaultItem;

		// Token: 0x040026B2 RID: 9906
		private ToolStripSplitButton.ToolStripSplitButtonButton splitButtonButton;

		// Token: 0x040026B3 RID: 9907
		private Rectangle dropDownButtonBounds = Rectangle.Empty;

		// Token: 0x040026B4 RID: 9908
		private ToolStripSplitButton.ToolStripSplitButtonButtonLayout splitButtonButtonLayout;

		// Token: 0x040026B5 RID: 9909
		private int dropDownButtonWidth;

		// Token: 0x040026B6 RID: 9910
		private int splitterWidth = 1;

		// Token: 0x040026B7 RID: 9911
		private Rectangle splitterBounds = Rectangle.Empty;

		// Token: 0x040026B8 RID: 9912
		private byte openMouseId;

		// Token: 0x040026B9 RID: 9913
		private long lastClickTime;

		// Token: 0x040026BA RID: 9914
		private const int DEFAULT_DROPDOWN_WIDTH = 11;

		// Token: 0x040026BB RID: 9915
		private static readonly object EventDefaultItemChanged = new object();

		// Token: 0x040026BC RID: 9916
		private static readonly object EventButtonClick = new object();

		// Token: 0x040026BD RID: 9917
		private static readonly object EventButtonDoubleClick = new object();

		// Token: 0x040026BE RID: 9918
		private static readonly object EventDropDownOpened = new object();

		// Token: 0x040026BF RID: 9919
		private static readonly object EventDropDownClosed = new object();

		// Token: 0x040026C0 RID: 9920
		private static bool isScalingInitialized = false;

		// Token: 0x040026C1 RID: 9921
		private static int scaledDropDownButtonWidth = 11;

		// Token: 0x0200081A RID: 2074
		private class ToolStripSplitButtonButton : ToolStripButton
		{
			// Token: 0x06006F9D RID: 28573 RVA: 0x0019985D File Offset: 0x00197A5D
			public ToolStripSplitButtonButton(ToolStripSplitButton owner)
			{
				this.owner = owner;
			}

			// Token: 0x1700186A RID: 6250
			// (get) Token: 0x06006F9E RID: 28574 RVA: 0x0019986C File Offset: 0x00197A6C
			// (set) Token: 0x06006F9F RID: 28575 RVA: 0x000070A6 File Offset: 0x000052A6
			public override bool Enabled
			{
				get
				{
					return this.owner.Enabled;
				}
				set
				{
				}
			}

			// Token: 0x1700186B RID: 6251
			// (get) Token: 0x06006FA0 RID: 28576 RVA: 0x00199879 File Offset: 0x00197A79
			// (set) Token: 0x06006FA1 RID: 28577 RVA: 0x000070A6 File Offset: 0x000052A6
			public override ToolStripItemDisplayStyle DisplayStyle
			{
				get
				{
					return this.owner.DisplayStyle;
				}
				set
				{
				}
			}

			// Token: 0x1700186C RID: 6252
			// (get) Token: 0x06006FA2 RID: 28578 RVA: 0x00199886 File Offset: 0x00197A86
			// (set) Token: 0x06006FA3 RID: 28579 RVA: 0x000070A6 File Offset: 0x000052A6
			public override Padding Padding
			{
				get
				{
					return this.owner.Padding;
				}
				set
				{
				}
			}

			// Token: 0x1700186D RID: 6253
			// (get) Token: 0x06006FA4 RID: 28580 RVA: 0x00199893 File Offset: 0x00197A93
			public override ToolStripTextDirection TextDirection
			{
				get
				{
					return this.owner.TextDirection;
				}
			}

			// Token: 0x1700186E RID: 6254
			// (get) Token: 0x06006FA5 RID: 28581 RVA: 0x001998A0 File Offset: 0x00197AA0
			// (set) Token: 0x06006FA6 RID: 28582 RVA: 0x000070A6 File Offset: 0x000052A6
			public override Image Image
			{
				get
				{
					if ((this.owner.DisplayStyle & ToolStripItemDisplayStyle.Image) == ToolStripItemDisplayStyle.Image)
					{
						return this.owner.Image;
					}
					return null;
				}
				set
				{
				}
			}

			// Token: 0x1700186F RID: 6255
			// (get) Token: 0x06006FA7 RID: 28583 RVA: 0x001998BF File Offset: 0x00197ABF
			public override bool Selected
			{
				get
				{
					if (this.owner != null)
					{
						return this.owner.Selected;
					}
					return base.Selected;
				}
			}

			// Token: 0x17001870 RID: 6256
			// (get) Token: 0x06006FA8 RID: 28584 RVA: 0x001998DB File Offset: 0x00197ADB
			// (set) Token: 0x06006FA9 RID: 28585 RVA: 0x000070A6 File Offset: 0x000052A6
			public override string Text
			{
				get
				{
					if ((this.owner.DisplayStyle & ToolStripItemDisplayStyle.Text) == ToolStripItemDisplayStyle.Text)
					{
						return this.owner.Text;
					}
					return null;
				}
				set
				{
				}
			}

			// Token: 0x04004329 RID: 17193
			private ToolStripSplitButton owner;
		}

		// Token: 0x0200081B RID: 2075
		private class ToolStripSplitButtonButtonLayout : ToolStripItemInternalLayout
		{
			// Token: 0x06006FAA RID: 28586 RVA: 0x001998FA File Offset: 0x00197AFA
			public ToolStripSplitButtonButtonLayout(ToolStripSplitButton owner)
				: base(owner.SplitButtonButton)
			{
				this.owner = owner;
			}

			// Token: 0x17001871 RID: 6257
			// (get) Token: 0x06006FAB RID: 28587 RVA: 0x0019990F File Offset: 0x00197B0F
			protected override ToolStripItem Owner
			{
				get
				{
					return this.owner;
				}
			}

			// Token: 0x17001872 RID: 6258
			// (get) Token: 0x06006FAC RID: 28588 RVA: 0x00199917 File Offset: 0x00197B17
			protected override ToolStrip ParentInternal
			{
				get
				{
					return this.owner.ParentInternal;
				}
			}

			// Token: 0x17001873 RID: 6259
			// (get) Token: 0x06006FAD RID: 28589 RVA: 0x00199924 File Offset: 0x00197B24
			public override Rectangle ImageRectangle
			{
				get
				{
					Rectangle imageRectangle = base.ImageRectangle;
					imageRectangle.Offset(this.owner.SplitButtonButton.Bounds.Location);
					return imageRectangle;
				}
			}

			// Token: 0x17001874 RID: 6260
			// (get) Token: 0x06006FAE RID: 28590 RVA: 0x00199958 File Offset: 0x00197B58
			public override Rectangle TextRectangle
			{
				get
				{
					Rectangle textRectangle = base.TextRectangle;
					textRectangle.Offset(this.owner.SplitButtonButton.Bounds.Location);
					return textRectangle;
				}
			}

			// Token: 0x0400432A RID: 17194
			private ToolStripSplitButton owner;
		}

		/// <summary>Provides information that accessibility applications use to adjust the user interface of a <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> for users with impairments.</summary>
		// Token: 0x0200081C RID: 2076
		public class ToolStripSplitButtonAccessibleObject : ToolStripItem.ToolStripItemAccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripSplitButton.ToolStripSplitButtonAccessibleObject" /> class.</summary>
			/// <param name="item">The <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> that owns this <see cref="T:System.Windows.Forms.ToolStripSplitButton.ToolStripSplitButtonAccessibleObject" />.</param>
			// Token: 0x06006FAF RID: 28591 RVA: 0x0019998C File Offset: 0x00197B8C
			public ToolStripSplitButtonAccessibleObject(ToolStripSplitButton item)
				: base(item)
			{
				this.owner = item;
			}

			/// <summary>Performs the default action associated with this <see cref="T:System.Windows.Forms.ToolStripSplitButton.ToolStripSplitButtonAccessibleObject" />.</summary>
			// Token: 0x06006FB0 RID: 28592 RVA: 0x0019999C File Offset: 0x00197B9C
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void DoDefaultAction()
			{
				this.owner.PerformButtonClick();
			}

			// Token: 0x0400432B RID: 17195
			private ToolStripSplitButton owner;
		}

		// Token: 0x0200081D RID: 2077
		internal class ToolStripSplitButtonExAccessibleObject : ToolStripSplitButton.ToolStripSplitButtonAccessibleObject
		{
			// Token: 0x06006FB1 RID: 28593 RVA: 0x001999A9 File Offset: 0x00197BA9
			public ToolStripSplitButtonExAccessibleObject(ToolStripSplitButton item)
				: base(item)
			{
				this.ownerItem = item;
			}

			// Token: 0x06006FB2 RID: 28594 RVA: 0x001999B9 File Offset: 0x00197BB9
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30003)
				{
					return 50000;
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x06006FB3 RID: 28595 RVA: 0x001999D5 File Offset: 0x00197BD5
			internal override bool IsIAccessibleExSupported()
			{
				return this.ownerItem != null || base.IsIAccessibleExSupported();
			}

			// Token: 0x06006FB4 RID: 28596 RVA: 0x001999E7 File Offset: 0x00197BE7
			internal override bool IsPatternSupported(int patternId)
			{
				return (patternId == 10005 && this.ownerItem.HasDropDownItems) || base.IsPatternSupported(patternId);
			}

			// Token: 0x06006FB5 RID: 28597 RVA: 0x00016044 File Offset: 0x00014244
			internal override void Expand()
			{
				this.DoDefaultAction();
			}

			// Token: 0x06006FB6 RID: 28598 RVA: 0x00199A07 File Offset: 0x00197C07
			internal override void Collapse()
			{
				if (this.ownerItem != null && this.ownerItem.DropDown != null && this.ownerItem.DropDown.Visible)
				{
					this.ownerItem.DropDown.Close();
				}
			}

			// Token: 0x17001875 RID: 6261
			// (get) Token: 0x06006FB7 RID: 28599 RVA: 0x00199A40 File Offset: 0x00197C40
			internal override UnsafeNativeMethods.ExpandCollapseState ExpandCollapseState
			{
				get
				{
					if (!this.ownerItem.DropDown.Visible)
					{
						return UnsafeNativeMethods.ExpandCollapseState.Collapsed;
					}
					return UnsafeNativeMethods.ExpandCollapseState.Expanded;
				}
			}

			// Token: 0x06006FB8 RID: 28600 RVA: 0x00199A58 File Offset: 0x00197C58
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (direction != UnsafeNativeMethods.NavigateDirection.FirstChild)
				{
					if (direction != UnsafeNativeMethods.NavigateDirection.LastChild)
					{
						return base.FragmentNavigate(direction);
					}
					if (this.DropDownItemsCount <= 0)
					{
						return null;
					}
					return this.ownerItem.DropDown.Items[this.ownerItem.DropDown.Items.Count - 1].AccessibilityObject;
				}
				else
				{
					if (this.DropDownItemsCount <= 0)
					{
						return null;
					}
					return this.ownerItem.DropDown.Items[0].AccessibilityObject;
				}
			}

			// Token: 0x17001876 RID: 6262
			// (get) Token: 0x06006FB9 RID: 28601 RVA: 0x00199ADA File Offset: 0x00197CDA
			private int DropDownItemsCount
			{
				get
				{
					if (AccessibilityImprovements.Level3 && this.ExpandCollapseState == UnsafeNativeMethods.ExpandCollapseState.Collapsed)
					{
						return 0;
					}
					return this.ownerItem.DropDownItems.Count;
				}
			}

			// Token: 0x0400432C RID: 17196
			private ToolStripSplitButton ownerItem;
		}

		// Token: 0x0200081E RID: 2078
		internal class ToolStripSplitButtonUiaProvider : ToolStripDropDownItemAccessibleObject
		{
			// Token: 0x06006FBA RID: 28602 RVA: 0x00199AFD File Offset: 0x00197CFD
			public ToolStripSplitButtonUiaProvider(ToolStripSplitButton owner)
				: base(owner)
			{
				this._owner = owner;
				this._accessibleObject = new ToolStripSplitButton.ToolStripSplitButtonExAccessibleObject(owner);
			}

			// Token: 0x06006FBB RID: 28603 RVA: 0x00199B19 File Offset: 0x00197D19
			public override void DoDefaultAction()
			{
				this._accessibleObject.DoDefaultAction();
			}

			// Token: 0x06006FBC RID: 28604 RVA: 0x00199B26 File Offset: 0x00197D26
			internal override object GetPropertyValue(int propertyID)
			{
				return this._accessibleObject.GetPropertyValue(propertyID);
			}

			// Token: 0x06006FBD RID: 28605 RVA: 0x00012E4E File Offset: 0x0001104E
			internal override bool IsIAccessibleExSupported()
			{
				return true;
			}

			// Token: 0x06006FBE RID: 28606 RVA: 0x00199B34 File Offset: 0x00197D34
			internal override bool IsPatternSupported(int patternId)
			{
				return this._accessibleObject.IsPatternSupported(patternId);
			}

			// Token: 0x06006FBF RID: 28607 RVA: 0x00016044 File Offset: 0x00014244
			internal override void Expand()
			{
				this.DoDefaultAction();
			}

			// Token: 0x06006FC0 RID: 28608 RVA: 0x00199B42 File Offset: 0x00197D42
			internal override void Collapse()
			{
				this._accessibleObject.Collapse();
			}

			// Token: 0x17001877 RID: 6263
			// (get) Token: 0x06006FC1 RID: 28609 RVA: 0x00199B4F File Offset: 0x00197D4F
			internal override UnsafeNativeMethods.ExpandCollapseState ExpandCollapseState
			{
				get
				{
					return this._accessibleObject.ExpandCollapseState;
				}
			}

			// Token: 0x06006FC2 RID: 28610 RVA: 0x00199B5C File Offset: 0x00197D5C
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				return this._accessibleObject.FragmentNavigate(direction);
			}

			// Token: 0x0400432D RID: 17197
			private ToolStripSplitButton _owner;

			// Token: 0x0400432E RID: 17198
			private ToolStripSplitButton.ToolStripSplitButtonExAccessibleObject _accessibleObject;
		}
	}
}
