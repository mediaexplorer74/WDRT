using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.Design;

namespace System.Windows.Forms
{
	/// <summary>Represents a nonselectable <see cref="T:System.Windows.Forms.ToolStripItem" /> that renders text and images and can display hyperlinks.</summary>
	// Token: 0x020003DE RID: 990
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip)]
	public class ToolStripLabel : ToolStripItem
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripLabel" /> class.</summary>
		// Token: 0x06004364 RID: 17252 RVA: 0x0011D051 File Offset: 0x0011B251
		public ToolStripLabel()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripLabel" /> class, specifying the text to display.</summary>
		/// <param name="text">The text to display on the <see cref="T:System.Windows.Forms.ToolStripLabel" />.</param>
		// Token: 0x06004365 RID: 17253 RVA: 0x0011D07A File Offset: 0x0011B27A
		public ToolStripLabel(string text)
			: base(text, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripLabel" /> class, specifying the image to display.</summary>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the <see cref="T:System.Windows.Forms.ToolStripLabel" />.</param>
		// Token: 0x06004366 RID: 17254 RVA: 0x0011D0A6 File Offset: 0x0011B2A6
		public ToolStripLabel(Image image)
			: base(null, image, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripLabel" /> class, specifying the text and image to display.</summary>
		/// <param name="text">The text to display on the <see cref="T:System.Windows.Forms.ToolStripLabel" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the <see cref="T:System.Windows.Forms.ToolStripLabel" />.</param>
		// Token: 0x06004367 RID: 17255 RVA: 0x0011D0D2 File Offset: 0x0011B2D2
		public ToolStripLabel(string text, Image image)
			: base(text, image, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripLabel" /> class, specifying the text and image to display and whether the <see cref="T:System.Windows.Forms.ToolStripLabel" /> acts as a link.</summary>
		/// <param name="text">The text to display on the <see cref="T:System.Windows.Forms.ToolStripLabel" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the <see cref="T:System.Windows.Forms.ToolStripLabel" />.</param>
		/// <param name="isLink">
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.ToolStripLabel" /> acts as a link; otherwise, <see langword="false" />.</param>
		// Token: 0x06004368 RID: 17256 RVA: 0x0011D0FE File Offset: 0x0011B2FE
		public ToolStripLabel(string text, Image image, bool isLink)
			: this(text, image, isLink, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripLabel" /> class, specifying the text and image to display, whether the <see cref="T:System.Windows.Forms.ToolStripLabel" /> acts as a link, and providing a <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event handler.</summary>
		/// <param name="text">The text to display on the <see cref="T:System.Windows.Forms.ToolStripLabel" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the <see cref="T:System.Windows.Forms.ToolStripLabel" />.</param>
		/// <param name="isLink">
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.ToolStripLabel" /> acts as a link; otherwise, <see langword="false" />.</param>
		/// <param name="onClick">A <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event handler.</param>
		// Token: 0x06004369 RID: 17257 RVA: 0x0011D10A File Offset: 0x0011B30A
		public ToolStripLabel(string text, Image image, bool isLink, EventHandler onClick)
			: this(text, image, isLink, onClick, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripLabel" /> class, specifying the text and image to display, whether the <see cref="T:System.Windows.Forms.ToolStripLabel" /> acts as a link, and providing a <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event handler and name for the <see cref="T:System.Windows.Forms.ToolStripLabel" />.</summary>
		/// <param name="text">The text to display on the <see cref="T:System.Windows.Forms.ToolStripLabel" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the <see cref="T:System.Windows.Forms.ToolStripLabel" />.</param>
		/// <param name="isLink">
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.ToolStripLabel" /> acts as a link; otherwise, <see langword="false" />.</param>
		/// <param name="onClick">A <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event handler.</param>
		/// <param name="name">The name of the <see cref="T:System.Windows.Forms.ToolStripLabel" />.</param>
		// Token: 0x0600436A RID: 17258 RVA: 0x0011D118 File Offset: 0x0011B318
		public ToolStripLabel(string text, Image image, bool isLink, EventHandler onClick, string name)
			: base(text, image, onClick, name)
		{
			this.IsLink = isLink;
		}

		/// <summary>Gets a value indicating the selectable state of a <see cref="T:System.Windows.Forms.ToolStripLabel" />.</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x17001075 RID: 4213
		// (get) Token: 0x0600436B RID: 17259 RVA: 0x0011D14E File Offset: 0x0011B34E
		public override bool CanSelect
		{
			get
			{
				return this.IsLink || base.DesignMode;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripLabel" /> is a hyperlink.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.ToolStripLabel" /> is a hyperlink; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17001076 RID: 4214
		// (get) Token: 0x0600436C RID: 17260 RVA: 0x0011D160 File Offset: 0x0011B360
		// (set) Token: 0x0600436D RID: 17261 RVA: 0x0011D168 File Offset: 0x0011B368
		[DefaultValue(false)]
		[SRCategory("CatBehavior")]
		[SRDescription("ToolStripLabelIsLinkDescr")]
		public bool IsLink
		{
			get
			{
				return this.isLink;
			}
			set
			{
				if (this.isLink != value)
				{
					this.isLink = value;
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the color used to display an active link.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color to display an active link. The default color is specified by the system. Typically, this color is <see langword="Color.Red" />.</returns>
		// Token: 0x17001077 RID: 4215
		// (get) Token: 0x0600436E RID: 17262 RVA: 0x0011D180 File Offset: 0x0011B380
		// (set) Token: 0x0600436F RID: 17263 RVA: 0x0011D19C File Offset: 0x0011B39C
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripLabelActiveLinkColorDescr")]
		public Color ActiveLinkColor
		{
			get
			{
				if (this.activeLinkColor.IsEmpty)
				{
					return this.IEActiveLinkColor;
				}
				return this.activeLinkColor;
			}
			set
			{
				if (this.activeLinkColor != value)
				{
					this.activeLinkColor = value;
					base.Invalidate();
				}
			}
		}

		// Token: 0x17001078 RID: 4216
		// (get) Token: 0x06004370 RID: 17264 RVA: 0x000C2675 File Offset: 0x000C0875
		private Color IELinkColor
		{
			get
			{
				return LinkUtilities.IELinkColor;
			}
		}

		// Token: 0x17001079 RID: 4217
		// (get) Token: 0x06004371 RID: 17265 RVA: 0x000C267C File Offset: 0x000C087C
		private Color IEActiveLinkColor
		{
			get
			{
				return LinkUtilities.IEActiveLinkColor;
			}
		}

		// Token: 0x1700107A RID: 4218
		// (get) Token: 0x06004372 RID: 17266 RVA: 0x000C2683 File Offset: 0x000C0883
		private Color IEVisitedLinkColor
		{
			get
			{
				return LinkUtilities.IEVisitedLinkColor;
			}
		}

		/// <summary>Gets or sets a value that represents the behavior of a link.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.LinkBehavior" /> values. The default is <see langword="LinkBehavior.SystemDefault" />.</returns>
		// Token: 0x1700107B RID: 4219
		// (get) Token: 0x06004373 RID: 17267 RVA: 0x0011D1B9 File Offset: 0x0011B3B9
		// (set) Token: 0x06004374 RID: 17268 RVA: 0x0011D1C4 File Offset: 0x0011B3C4
		[DefaultValue(LinkBehavior.SystemDefault)]
		[SRCategory("CatBehavior")]
		[SRDescription("ToolStripLabelLinkBehaviorDescr")]
		public LinkBehavior LinkBehavior
		{
			get
			{
				return this.linkBehavior;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("LinkBehavior", (int)value, typeof(LinkBehavior));
				}
				if (this.linkBehavior != value)
				{
					this.linkBehavior = value;
					this.InvalidateLinkFonts();
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the color used when displaying a normal link.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color used to displaying a normal link. The default color is specified by the system. Typically, this color is <see langword="Color.Blue" />.</returns>
		// Token: 0x1700107C RID: 4220
		// (get) Token: 0x06004375 RID: 17269 RVA: 0x0011D213 File Offset: 0x0011B413
		// (set) Token: 0x06004376 RID: 17270 RVA: 0x0011D22F File Offset: 0x0011B42F
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripLabelLinkColorDescr")]
		public Color LinkColor
		{
			get
			{
				if (this.linkColor.IsEmpty)
				{
					return this.IELinkColor;
				}
				return this.linkColor;
			}
			set
			{
				if (this.linkColor != value)
				{
					this.linkColor = value;
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether a link should be displayed as though it were visited.</summary>
		/// <returns>
		///   <see langword="true" /> if links should display as though they were visited; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x1700107D RID: 4221
		// (get) Token: 0x06004377 RID: 17271 RVA: 0x0011D24C File Offset: 0x0011B44C
		// (set) Token: 0x06004378 RID: 17272 RVA: 0x0011D254 File Offset: 0x0011B454
		[DefaultValue(false)]
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripLabelLinkVisitedDescr")]
		public bool LinkVisited
		{
			get
			{
				return this.linkVisited;
			}
			set
			{
				if (this.linkVisited != value)
				{
					this.linkVisited = value;
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the color used when displaying a link that has been previously visited.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color used to display links that have been visited. The default color is specified by the system. Typically, this color is <see langword="Color.Purple" />.</returns>
		// Token: 0x1700107E RID: 4222
		// (get) Token: 0x06004379 RID: 17273 RVA: 0x0011D26C File Offset: 0x0011B46C
		// (set) Token: 0x0600437A RID: 17274 RVA: 0x0011D288 File Offset: 0x0011B488
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripLabelVisitedLinkColorDescr")]
		public Color VisitedLinkColor
		{
			get
			{
				if (this.visitedLinkColor.IsEmpty)
				{
					return this.IEVisitedLinkColor;
				}
				return this.visitedLinkColor;
			}
			set
			{
				if (this.visitedLinkColor != value)
				{
					this.visitedLinkColor = value;
					base.Invalidate();
				}
			}
		}

		// Token: 0x0600437B RID: 17275 RVA: 0x0011D2A8 File Offset: 0x0011B4A8
		private void InvalidateLinkFonts()
		{
			if (this.linkFont != null)
			{
				this.linkFont.Dispose();
			}
			if (this.hoverLinkFont != null && this.hoverLinkFont != this.linkFont)
			{
				this.hoverLinkFont.Dispose();
			}
			this.linkFont = null;
			this.hoverLinkFont = null;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600437C RID: 17276 RVA: 0x0011D2F7 File Offset: 0x0011B4F7
		protected override void OnFontChanged(EventArgs e)
		{
			this.InvalidateLinkFonts();
			base.OnFontChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseEnter" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600437D RID: 17277 RVA: 0x0011D308 File Offset: 0x0011B508
		protected override void OnMouseEnter(EventArgs e)
		{
			if (this.IsLink)
			{
				ToolStrip parent = base.Parent;
				if (parent != null)
				{
					this.lastCursor = parent.Cursor;
					parent.Cursor = Cursors.Hand;
				}
			}
			base.OnMouseEnter(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseLeave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600437E RID: 17278 RVA: 0x0011D348 File Offset: 0x0011B548
		protected override void OnMouseLeave(EventArgs e)
		{
			if (this.IsLink)
			{
				ToolStrip parent = base.Parent;
				if (parent != null)
				{
					parent.Cursor = this.lastCursor;
				}
			}
			base.OnMouseLeave(e);
		}

		// Token: 0x0600437F RID: 17279 RVA: 0x0011D37A File Offset: 0x0011B57A
		private void ResetActiveLinkColor()
		{
			this.ActiveLinkColor = this.IEActiveLinkColor;
		}

		// Token: 0x06004380 RID: 17280 RVA: 0x0011D388 File Offset: 0x0011B588
		private void ResetLinkColor()
		{
			this.LinkColor = this.IELinkColor;
		}

		// Token: 0x06004381 RID: 17281 RVA: 0x0011D396 File Offset: 0x0011B596
		private void ResetVisitedLinkColor()
		{
			this.VisitedLinkColor = this.IEVisitedLinkColor;
		}

		// Token: 0x06004382 RID: 17282 RVA: 0x0011D3A4 File Offset: 0x0011B5A4
		[EditorBrowsable(EditorBrowsableState.Never)]
		private bool ShouldSerializeActiveLinkColor()
		{
			return !this.activeLinkColor.IsEmpty;
		}

		// Token: 0x06004383 RID: 17283 RVA: 0x0011D3B4 File Offset: 0x0011B5B4
		[EditorBrowsable(EditorBrowsableState.Never)]
		private bool ShouldSerializeLinkColor()
		{
			return !this.linkColor.IsEmpty;
		}

		// Token: 0x06004384 RID: 17284 RVA: 0x0011D3C4 File Offset: 0x0011B5C4
		[EditorBrowsable(EditorBrowsableState.Never)]
		private bool ShouldSerializeVisitedLinkColor()
		{
			return !this.visitedLinkColor.IsEmpty;
		}

		// Token: 0x06004385 RID: 17285 RVA: 0x0011D3D4 File Offset: 0x0011B5D4
		internal override ToolStripItemInternalLayout CreateInternalLayout()
		{
			return new ToolStripLabel.ToolStripLabelLayout(this);
		}

		/// <summary>Creates a new accessibility object for the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		// Token: 0x06004386 RID: 17286 RVA: 0x0011D3DC File Offset: 0x0011B5DC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new ToolStripLabel.ToolStripLabelAccessibleObject(this);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.Paint" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		// Token: 0x06004387 RID: 17287 RVA: 0x0011D3E4 File Offset: 0x0011B5E4
		protected override void OnPaint(PaintEventArgs e)
		{
			if (base.Owner != null)
			{
				ToolStripRenderer renderer = base.Renderer;
				renderer.DrawLabelBackground(new ToolStripItemRenderEventArgs(e.Graphics, this));
				if ((this.DisplayStyle & ToolStripItemDisplayStyle.Image) == ToolStripItemDisplayStyle.Image)
				{
					renderer.DrawItemImage(new ToolStripItemImageRenderEventArgs(e.Graphics, this, base.InternalLayout.ImageRectangle));
				}
				this.PaintText(e.Graphics);
			}
		}

		// Token: 0x06004388 RID: 17288 RVA: 0x0011D448 File Offset: 0x0011B648
		internal void PaintText(Graphics g)
		{
			ToolStripRenderer renderer = base.Renderer;
			if ((this.DisplayStyle & ToolStripItemDisplayStyle.Text) == ToolStripItemDisplayStyle.Text)
			{
				Font font = this.Font;
				Color color = this.ForeColor;
				if (this.IsLink)
				{
					LinkUtilities.EnsureLinkFonts(font, this.LinkBehavior, ref this.linkFont, ref this.hoverLinkFont);
					if (this.Pressed)
					{
						font = this.hoverLinkFont;
						color = this.ActiveLinkColor;
					}
					else if (this.Selected)
					{
						font = this.hoverLinkFont;
						color = (this.LinkVisited ? this.VisitedLinkColor : this.LinkColor);
					}
					else
					{
						font = this.linkFont;
						color = (this.LinkVisited ? this.VisitedLinkColor : this.LinkColor);
					}
				}
				Rectangle textRectangle = base.InternalLayout.TextRectangle;
				renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(g, this, this.Text, textRectangle, color, font, base.InternalLayout.TextFormat));
			}
		}

		/// <summary>Processes a mnemonic character.</summary>
		/// <param name="charCode">The character to process.</param>
		/// <returns>
		///   <see langword="true" /> if the character was processed as a mnemonic by the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004389 RID: 17289 RVA: 0x0011D523 File Offset: 0x0011B723
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected internal override bool ProcessMnemonic(char charCode)
		{
			if (base.ParentInternal != null)
			{
				if (!this.CanSelect)
				{
					base.ParentInternal.SetFocusUnsafe();
					base.ParentInternal.SelectNextToolStripItem(this, true);
				}
				else
				{
					base.FireEvent(ToolStripItemEventType.Click);
				}
				return true;
			}
			return false;
		}

		// Token: 0x040025C9 RID: 9673
		private LinkBehavior linkBehavior;

		// Token: 0x040025CA RID: 9674
		private bool isLink;

		// Token: 0x040025CB RID: 9675
		private bool linkVisited;

		// Token: 0x040025CC RID: 9676
		private Color linkColor = Color.Empty;

		// Token: 0x040025CD RID: 9677
		private Color activeLinkColor = Color.Empty;

		// Token: 0x040025CE RID: 9678
		private Color visitedLinkColor = Color.Empty;

		// Token: 0x040025CF RID: 9679
		private Font hoverLinkFont;

		// Token: 0x040025D0 RID: 9680
		private Font linkFont;

		// Token: 0x040025D1 RID: 9681
		private Cursor lastCursor;

		// Token: 0x02000806 RID: 2054
		[ComVisible(true)]
		internal class ToolStripLabelAccessibleObject : ToolStripItem.ToolStripItemAccessibleObject
		{
			// Token: 0x06006EE3 RID: 28387 RVA: 0x00196244 File Offset: 0x00194444
			public ToolStripLabelAccessibleObject(ToolStripLabel ownerItem)
				: base(ownerItem)
			{
				this.ownerItem = ownerItem;
			}

			// Token: 0x17001845 RID: 6213
			// (get) Token: 0x06006EE4 RID: 28388 RVA: 0x00196254 File Offset: 0x00194454
			public override string DefaultAction
			{
				get
				{
					if (this.ownerItem.IsLink)
					{
						return SR.GetString("AccessibleActionClick");
					}
					return string.Empty;
				}
			}

			// Token: 0x06006EE5 RID: 28389 RVA: 0x00196273 File Offset: 0x00194473
			public override void DoDefaultAction()
			{
				if (this.ownerItem.IsLink)
				{
					base.DoDefaultAction();
				}
			}

			// Token: 0x06006EE6 RID: 28390 RVA: 0x00196288 File Offset: 0x00194488
			internal override object GetPropertyValue(int propertyID)
			{
				if (AccessibilityImprovements.Level3)
				{
					if (propertyID == 30003)
					{
						return 50020;
					}
					if (propertyID == 30096)
					{
						return this.State;
					}
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x17001846 RID: 6214
			// (get) Token: 0x06006EE7 RID: 28391 RVA: 0x001962C0 File Offset: 0x001944C0
			public override AccessibleRole Role
			{
				get
				{
					AccessibleRole accessibleRole = base.Owner.AccessibleRole;
					if (accessibleRole != AccessibleRole.Default)
					{
						return accessibleRole;
					}
					if (!this.ownerItem.IsLink)
					{
						return AccessibleRole.StaticText;
					}
					return AccessibleRole.Link;
				}
			}

			// Token: 0x17001847 RID: 6215
			// (get) Token: 0x06006EE8 RID: 28392 RVA: 0x001962F1 File Offset: 0x001944F1
			public override AccessibleStates State
			{
				get
				{
					return base.State | AccessibleStates.ReadOnly;
				}
			}

			// Token: 0x04004300 RID: 17152
			private ToolStripLabel ownerItem;
		}

		// Token: 0x02000807 RID: 2055
		private class ToolStripLabelLayout : ToolStripItemInternalLayout
		{
			// Token: 0x06006EE9 RID: 28393 RVA: 0x001962FC File Offset: 0x001944FC
			public ToolStripLabelLayout(ToolStripLabel owner)
				: base(owner)
			{
				this.owner = owner;
			}

			// Token: 0x06006EEA RID: 28394 RVA: 0x0019630C File Offset: 0x0019450C
			protected override ToolStripItemInternalLayout.ToolStripItemLayoutOptions CommonLayoutOptions()
			{
				ToolStripItemInternalLayout.ToolStripItemLayoutOptions toolStripItemLayoutOptions = base.CommonLayoutOptions();
				toolStripItemLayoutOptions.borderSize = 0;
				return toolStripItemLayoutOptions;
			}

			// Token: 0x04004301 RID: 17153
			private ToolStripLabel owner;
		}
	}
}
