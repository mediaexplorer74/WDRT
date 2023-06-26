using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.Design;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Represents a control that when clicked displays an associated <see cref="T:System.Windows.Forms.ToolStripDropDown" /> from which the user can select a single item.</summary>
	// Token: 0x020003BE RID: 958
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.StatusStrip)]
	public class ToolStripDropDownButton : ToolStripDropDownItem
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" /> class.</summary>
		// Token: 0x060040CC RID: 16588 RVA: 0x0011478A File Offset: 0x0011298A
		public ToolStripDropDownButton()
		{
			this.Initialize();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" /> class that displays the specified text.</summary>
		/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</param>
		// Token: 0x060040CD RID: 16589 RVA: 0x0011479F File Offset: 0x0011299F
		public ToolStripDropDownButton(string text)
			: base(text, null, null)
		{
			this.Initialize();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" /> class that displays the specified image.</summary>
		/// <param name="image">An <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</param>
		// Token: 0x060040CE RID: 16590 RVA: 0x001147B7 File Offset: 0x001129B7
		public ToolStripDropDownButton(Image image)
			: base(null, image, null)
		{
			this.Initialize();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" /> class that displays the specified text and image.</summary>
		/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</param>
		/// <param name="image">An <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</param>
		// Token: 0x060040CF RID: 16591 RVA: 0x001147CF File Offset: 0x001129CF
		public ToolStripDropDownButton(string text, Image image)
			: base(text, image, null)
		{
			this.Initialize();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" /> class that displays the specified text and image and raises the <see langword="Click" /> event.</summary>
		/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</param>
		/// <param name="image">An <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</param>
		/// <param name="onClick">The event handler for the <see cref="E:System.Windows.Forms.Control.Click" /> event.</param>
		// Token: 0x060040D0 RID: 16592 RVA: 0x001147E7 File Offset: 0x001129E7
		public ToolStripDropDownButton(string text, Image image, EventHandler onClick)
			: base(text, image, onClick)
		{
			this.Initialize();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" /> class that has the specified name, displays the specified text and image, and raises the <see langword="Click" /> event.</summary>
		/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</param>
		/// <param name="image">An <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</param>
		/// <param name="onClick">The event handler for the <see cref="E:System.Windows.Forms.Control.Click" /> event.</param>
		/// <param name="name">The name of the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</param>
		// Token: 0x060040D1 RID: 16593 RVA: 0x001147FF File Offset: 0x001129FF
		public ToolStripDropDownButton(string text, Image image, EventHandler onClick, string name)
			: base(text, image, onClick, name)
		{
			this.Initialize();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" /> class.</summary>
		/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</param>
		/// <param name="image">An <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</param>
		/// <param name="dropDownItems">An array of type <see cref="T:System.Windows.Forms.ToolStripItem" /> containing the items of the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</param>
		// Token: 0x060040D2 RID: 16594 RVA: 0x00114819 File Offset: 0x00112A19
		public ToolStripDropDownButton(string text, Image image, params ToolStripItem[] dropDownItems)
			: base(text, image, dropDownItems)
		{
			this.Initialize();
		}

		/// <summary>Creates a new accessibility object for this <see cref="T:System.Windows.Forms.ToolStripDropDownButton" /> instance.</summary>
		/// <returns>A new accessibility object for this <see cref="T:System.Windows.Forms.ToolStripDropDownButton" /> instance.</returns>
		// Token: 0x060040D3 RID: 16595 RVA: 0x00114831 File Offset: 0x00112A31
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level1)
			{
				return new ToolStripDropDownButton.ToolStripDropDownButtonAccessibleObject(this);
			}
			return base.CreateAccessibilityInstance();
		}

		/// <summary>Gets or sets a value indicating whether to use the <see langword="Text" /> property or the <see cref="P:System.Windows.Forms.ToolStripItem.ToolTipText" /> property for the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" /> ToolTip.</summary>
		/// <returns>
		///   <see langword="true" /> to use the <see cref="P:System.Windows.Forms.Control.Text" /> property for the ToolTip; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000FD1 RID: 4049
		// (get) Token: 0x060040D4 RID: 16596 RVA: 0x00110E48 File Offset: 0x0010F048
		// (set) Token: 0x060040D5 RID: 16597 RVA: 0x00110E50 File Offset: 0x0010F050
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

		/// <summary>Gets a value indicating whether to display the <see cref="T:System.Windows.Forms.ToolTip" /> that is defined as the default.</summary>
		/// <returns>
		///   <see langword="true" /> in all cases.</returns>
		// Token: 0x17000FD2 RID: 4050
		// (get) Token: 0x060040D6 RID: 16598 RVA: 0x00012E4E File Offset: 0x0001104E
		protected override bool DefaultAutoToolTip
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets or sets a value indicating whether an arrow is displayed on the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />, which indicates that further options are available in a drop-down list.</summary>
		/// <returns>
		///   <see langword="true" /> to show an arrow on the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000FD3 RID: 4051
		// (get) Token: 0x060040D7 RID: 16599 RVA: 0x00114847 File Offset: 0x00112A47
		// (set) Token: 0x060040D8 RID: 16600 RVA: 0x0011484F File Offset: 0x00112A4F
		[DefaultValue(true)]
		[SRDescription("ToolStripDropDownButtonShowDropDownArrowDescr")]
		[SRCategory("CatAppearance")]
		public bool ShowDropDownArrow
		{
			get
			{
				return this.showDropDownArrow;
			}
			set
			{
				if (this.showDropDownArrow != value)
				{
					this.showDropDownArrow = value;
					base.InvalidateItemLayout(PropertyNames.ShowDropDownArrow);
				}
			}
		}

		// Token: 0x060040D9 RID: 16601 RVA: 0x0011486C File Offset: 0x00112A6C
		internal override ToolStripItemInternalLayout CreateInternalLayout()
		{
			return new ToolStripDropDownButton.ToolStripDropDownButtonInternalLayout(this);
		}

		/// <summary>Creates a generic <see cref="T:System.Windows.Forms.ToolStripDropDown" /> for which events can be defined.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripDropDown" /> for which events can be defined.</returns>
		// Token: 0x060040DA RID: 16602 RVA: 0x00114874 File Offset: 0x00112A74
		protected override ToolStripDropDown CreateDefaultDropDown()
		{
			return new ToolStripDropDownMenu(this, true);
		}

		// Token: 0x060040DB RID: 16603 RVA: 0x0011487D File Offset: 0x00112A7D
		private void Initialize()
		{
			base.SupportsSpaceKey = true;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x060040DC RID: 16604 RVA: 0x00114888 File Offset: 0x00112A88
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (Control.ModifierKeys != Keys.Alt && e.Button == MouseButtons.Left)
			{
				if (base.DropDown.Visible)
				{
					ToolStripManager.ModalMenuFilter.CloseActiveDropDown(base.DropDown, ToolStripDropDownCloseReason.AppClicked);
				}
				else
				{
					this.openMouseId = ((base.ParentInternal == null) ? 0 : base.ParentInternal.GetMouseId());
					base.ShowDropDown(true);
				}
			}
			base.OnMouseDown(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseUp" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x060040DD RID: 16605 RVA: 0x001148F4 File Offset: 0x00112AF4
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (Control.ModifierKeys != Keys.Alt && e.Button == MouseButtons.Left)
			{
				byte b = ((base.ParentInternal == null) ? 0 : base.ParentInternal.GetMouseId());
				if (b != this.openMouseId)
				{
					this.openMouseId = 0;
					ToolStripManager.ModalMenuFilter.CloseActiveDropDown(base.DropDown, ToolStripDropDownCloseReason.AppClicked);
					base.Select();
				}
			}
			base.OnMouseUp(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseLeave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060040DE RID: 16606 RVA: 0x0011495A File Offset: 0x00112B5A
		protected override void OnMouseLeave(EventArgs e)
		{
			this.openMouseId = 0;
			base.OnMouseLeave(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.Paint" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		// Token: 0x060040DF RID: 16607 RVA: 0x0011496C File Offset: 0x00112B6C
		protected override void OnPaint(PaintEventArgs e)
		{
			if (base.Owner != null)
			{
				ToolStripRenderer renderer = base.Renderer;
				Graphics graphics = e.Graphics;
				renderer.DrawDropDownButtonBackground(new ToolStripItemRenderEventArgs(e.Graphics, this));
				if ((this.DisplayStyle & ToolStripItemDisplayStyle.Image) == ToolStripItemDisplayStyle.Image)
				{
					renderer.DrawItemImage(new ToolStripItemImageRenderEventArgs(graphics, this, base.InternalLayout.ImageRectangle));
				}
				if ((this.DisplayStyle & ToolStripItemDisplayStyle.Text) == ToolStripItemDisplayStyle.Text)
				{
					renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(graphics, this, this.Text, base.InternalLayout.TextRectangle, this.ForeColor, this.Font, base.InternalLayout.TextFormat));
				}
				if (this.ShowDropDownArrow)
				{
					ToolStripDropDownButton.ToolStripDropDownButtonInternalLayout toolStripDropDownButtonInternalLayout = base.InternalLayout as ToolStripDropDownButton.ToolStripDropDownButtonInternalLayout;
					Rectangle rectangle = ((toolStripDropDownButtonInternalLayout != null) ? toolStripDropDownButtonInternalLayout.DropDownArrowRect : Rectangle.Empty);
					Color color;
					if (this.Selected && !this.Pressed && AccessibilityImprovements.Level2 && SystemInformation.HighContrast)
					{
						color = (this.Enabled ? SystemColors.HighlightText : SystemColors.ControlDark);
					}
					else
					{
						color = (this.Enabled ? SystemColors.ControlText : SystemColors.ControlDark);
					}
					renderer.DrawArrow(new ToolStripArrowRenderEventArgs(graphics, this, rectangle, color, ArrowDirection.Down));
				}
			}
		}

		/// <summary>Retrieves a value indicating whether the drop-down list of the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" /> has items.</summary>
		/// <param name="charCode">The character to process.</param>
		/// <returns>
		///   <see langword="true" /> if the drop-down list has items; otherwise, <see langword="false" />.</returns>
		// Token: 0x060040E0 RID: 16608 RVA: 0x00114A8A File Offset: 0x00112C8A
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected internal override bool ProcessMnemonic(char charCode)
		{
			if (this.HasDropDownItems)
			{
				base.Select();
				base.ShowDropDown();
				return true;
			}
			return false;
		}

		// Token: 0x040024DD RID: 9437
		private bool showDropDownArrow = true;

		// Token: 0x040024DE RID: 9438
		private byte openMouseId;

		// Token: 0x020007FF RID: 2047
		[ComVisible(true)]
		internal class ToolStripDropDownButtonAccessibleObject : ToolStripDropDownItemAccessibleObject
		{
			// Token: 0x06006EB5 RID: 28341 RVA: 0x00195509 File Offset: 0x00193709
			public ToolStripDropDownButtonAccessibleObject(ToolStripDropDownButton ownerItem)
				: base(ownerItem)
			{
				this.ownerItem = ownerItem;
			}

			// Token: 0x06006EB6 RID: 28342 RVA: 0x00195519 File Offset: 0x00193719
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30003)
				{
					return 50000;
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x040042EE RID: 17134
			private ToolStripDropDownButton ownerItem;
		}

		// Token: 0x02000800 RID: 2048
		internal class ToolStripDropDownButtonInternalLayout : ToolStripItemInternalLayout
		{
			// Token: 0x06006EB7 RID: 28343 RVA: 0x00195538 File Offset: 0x00193738
			public ToolStripDropDownButtonInternalLayout(ToolStripDropDownButton ownerItem)
				: base(ownerItem)
			{
				if (DpiHelper.EnableToolStripPerMonitorV2HighDpiImprovements)
				{
					ToolStripDropDownButton.ToolStripDropDownButtonInternalLayout.dropDownArrowSize = DpiHelper.LogicalToDeviceUnits(ToolStripDropDownButton.ToolStripDropDownButtonInternalLayout.dropDownArrowSizeUnscaled, ownerItem.DeviceDpi);
					this.scaledDropDownArrowPadding = DpiHelper.LogicalToDeviceUnits(ToolStripDropDownButton.ToolStripDropDownButtonInternalLayout.dropDownArrowPadding, ownerItem.DeviceDpi);
				}
				else if (DpiHelper.IsScalingRequired)
				{
					ToolStripDropDownButton.ToolStripDropDownButtonInternalLayout.dropDownArrowSize = DpiHelper.LogicalToDeviceUnits(ToolStripDropDownButton.ToolStripDropDownButtonInternalLayout.dropDownArrowSizeUnscaled, 0);
					this.scaledDropDownArrowPadding = DpiHelper.LogicalToDeviceUnits(ToolStripDropDownButton.ToolStripDropDownButtonInternalLayout.dropDownArrowPadding, 0);
				}
				this.ownerItem = ownerItem;
			}

			// Token: 0x06006EB8 RID: 28344 RVA: 0x001955C8 File Offset: 0x001937C8
			public override Size GetPreferredSize(Size constrainingSize)
			{
				Size preferredSize = base.GetPreferredSize(constrainingSize);
				if (this.ownerItem.ShowDropDownArrow)
				{
					if (this.ownerItem.TextDirection == ToolStripTextDirection.Horizontal)
					{
						preferredSize.Width += this.DropDownArrowRect.Width + this.scaledDropDownArrowPadding.Horizontal;
					}
					else
					{
						preferredSize.Height += this.DropDownArrowRect.Height + this.scaledDropDownArrowPadding.Vertical;
					}
				}
				return preferredSize;
			}

			// Token: 0x06006EB9 RID: 28345 RVA: 0x0019564C File Offset: 0x0019384C
			protected override ToolStripItemInternalLayout.ToolStripItemLayoutOptions CommonLayoutOptions()
			{
				ToolStripItemInternalLayout.ToolStripItemLayoutOptions toolStripItemLayoutOptions = base.CommonLayoutOptions();
				if (this.ownerItem.ShowDropDownArrow)
				{
					if (this.ownerItem.TextDirection == ToolStripTextDirection.Horizontal)
					{
						int num = ToolStripDropDownButton.ToolStripDropDownButtonInternalLayout.dropDownArrowSize.Width + this.scaledDropDownArrowPadding.Horizontal;
						ToolStripItemInternalLayout.ToolStripItemLayoutOptions toolStripItemLayoutOptions2 = toolStripItemLayoutOptions;
						toolStripItemLayoutOptions2.client.Width = toolStripItemLayoutOptions2.client.Width - num;
						if (this.ownerItem.RightToLeft == RightToLeft.Yes)
						{
							toolStripItemLayoutOptions.client.Offset(num, 0);
							this.dropDownArrowRect = new Rectangle(this.scaledDropDownArrowPadding.Left, 0, ToolStripDropDownButton.ToolStripDropDownButtonInternalLayout.dropDownArrowSize.Width, this.ownerItem.Bounds.Height);
						}
						else
						{
							this.dropDownArrowRect = new Rectangle(toolStripItemLayoutOptions.client.Right, 0, ToolStripDropDownButton.ToolStripDropDownButtonInternalLayout.dropDownArrowSize.Width, this.ownerItem.Bounds.Height);
						}
					}
					else
					{
						int num2 = ToolStripDropDownButton.ToolStripDropDownButtonInternalLayout.dropDownArrowSize.Height + this.scaledDropDownArrowPadding.Vertical;
						ToolStripItemInternalLayout.ToolStripItemLayoutOptions toolStripItemLayoutOptions3 = toolStripItemLayoutOptions;
						toolStripItemLayoutOptions3.client.Height = toolStripItemLayoutOptions3.client.Height - num2;
						this.dropDownArrowRect = new Rectangle(0, toolStripItemLayoutOptions.client.Bottom + this.scaledDropDownArrowPadding.Top, this.ownerItem.Bounds.Width - 1, ToolStripDropDownButton.ToolStripDropDownButtonInternalLayout.dropDownArrowSize.Height);
					}
				}
				return toolStripItemLayoutOptions;
			}

			// Token: 0x17001836 RID: 6198
			// (get) Token: 0x06006EBA RID: 28346 RVA: 0x001957A2 File Offset: 0x001939A2
			public Rectangle DropDownArrowRect
			{
				get
				{
					return this.dropDownArrowRect;
				}
			}

			// Token: 0x040042EF RID: 17135
			private ToolStripDropDownButton ownerItem;

			// Token: 0x040042F0 RID: 17136
			private static readonly Size dropDownArrowSizeUnscaled = new Size(5, 3);

			// Token: 0x040042F1 RID: 17137
			private static Size dropDownArrowSize = ToolStripDropDownButton.ToolStripDropDownButtonInternalLayout.dropDownArrowSizeUnscaled;

			// Token: 0x040042F2 RID: 17138
			private const int DROP_DOWN_ARROW_PADDING = 2;

			// Token: 0x040042F3 RID: 17139
			private static Padding dropDownArrowPadding = new Padding(2);

			// Token: 0x040042F4 RID: 17140
			private Padding scaledDropDownArrowPadding = ToolStripDropDownButton.ToolStripDropDownButtonInternalLayout.dropDownArrowPadding;

			// Token: 0x040042F5 RID: 17141
			private Rectangle dropDownArrowRect = Rectangle.Empty;
		}
	}
}
