using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms.Internal;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows label control that can display hyperlinks.</summary>
	// Token: 0x020002C4 RID: 708
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultEvent("LinkClicked")]
	[ToolboxItem("System.Windows.Forms.Design.AutoSizeToolboxItem,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionLinkLabel")]
	public class LinkLabel : Label, IButtonControl
	{
		/// <summary>Initializes a new default instance of the <see cref="T:System.Windows.Forms.LinkLabel" /> class.</summary>
		// Token: 0x06002B41 RID: 11073 RVA: 0x000C2548 File Offset: 0x000C0748
		public LinkLabel()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.Opaque | ControlStyles.ResizeRedraw | ControlStyles.StandardClick | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.ResetLinkArea();
		}

		/// <summary>Gets or sets the color used to display an active link.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color to display an active link. The default color is specified by the system, typically this color is <see langword="Color.Red" />.</returns>
		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06002B42 RID: 11074 RVA: 0x000C25A5 File Offset: 0x000C07A5
		// (set) Token: 0x06002B43 RID: 11075 RVA: 0x000C25C1 File Offset: 0x000C07C1
		[SRCategory("CatAppearance")]
		[SRDescription("LinkLabelActiveLinkColorDescr")]
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
					this.InvalidateLink(null);
				}
			}
		}

		/// <summary>Gets or sets the color used when displaying a disabled link.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color when displaying a disabled link. The default is <see langword="Empty" />.</returns>
		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06002B44 RID: 11076 RVA: 0x000C25DF File Offset: 0x000C07DF
		// (set) Token: 0x06002B45 RID: 11077 RVA: 0x000C25FB File Offset: 0x000C07FB
		[SRCategory("CatAppearance")]
		[SRDescription("LinkLabelDisabledLinkColorDescr")]
		public Color DisabledLinkColor
		{
			get
			{
				if (this.disabledLinkColor.IsEmpty)
				{
					return this.IEDisabledLinkColor;
				}
				return this.disabledLinkColor;
			}
			set
			{
				if (this.disabledLinkColor != value)
				{
					this.disabledLinkColor = value;
					this.InvalidateLink(null);
				}
			}
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06002B46 RID: 11078 RVA: 0x000C2619 File Offset: 0x000C0819
		// (set) Token: 0x06002B47 RID: 11079 RVA: 0x000C2624 File Offset: 0x000C0824
		private LinkLabel.Link FocusLink
		{
			get
			{
				return this.focusLink;
			}
			set
			{
				if (this.focusLink != value)
				{
					if (this.focusLink != null)
					{
						this.InvalidateLink(this.focusLink);
					}
					this.focusLink = value;
					if (this.focusLink != null)
					{
						this.InvalidateLink(this.focusLink);
						this.UpdateAccessibilityLink(this.focusLink);
					}
				}
			}
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x06002B48 RID: 11080 RVA: 0x000C2675 File Offset: 0x000C0875
		private Color IELinkColor
		{
			get
			{
				return LinkUtilities.IELinkColor;
			}
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06002B49 RID: 11081 RVA: 0x000C267C File Offset: 0x000C087C
		private Color IEActiveLinkColor
		{
			get
			{
				return LinkUtilities.IEActiveLinkColor;
			}
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06002B4A RID: 11082 RVA: 0x000C2683 File Offset: 0x000C0883
		private Color IEVisitedLinkColor
		{
			get
			{
				return LinkUtilities.IEVisitedLinkColor;
			}
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x06002B4B RID: 11083 RVA: 0x000C268A File Offset: 0x000C088A
		private Color IEDisabledLinkColor
		{
			get
			{
				if (LinkLabel.iedisabledLinkColor.IsEmpty)
				{
					LinkLabel.iedisabledLinkColor = ControlPaint.Dark(base.DisabledColor);
				}
				return LinkLabel.iedisabledLinkColor;
			}
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x06002B4C RID: 11084 RVA: 0x000C26AD File Offset: 0x000C08AD
		private Rectangle ClientRectWithPadding
		{
			get
			{
				return LayoutUtils.DeflateRect(base.ClientRectangle, this.Padding);
			}
		}

		/// <summary>Gets or sets the flat style appearance of the <see cref="T:System.Windows.Forms.LinkLabel" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.FlatStyle" /> values.</returns>
		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06002B4D RID: 11085 RVA: 0x000C26C0 File Offset: 0x000C08C0
		// (set) Token: 0x06002B4E RID: 11086 RVA: 0x000C26C8 File Offset: 0x000C08C8
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new FlatStyle FlatStyle
		{
			get
			{
				return base.FlatStyle;
			}
			set
			{
				base.FlatStyle = value;
			}
		}

		/// <summary>Gets or sets the range in the text to treat as a link.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.LinkArea" /> that represents the area treated as a link.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Windows.Forms.LinkArea.Start" /> property of the <see cref="T:System.Windows.Forms.LinkArea" /> object is less than zero.  
		///  -or-  
		///  The <see cref="P:System.Windows.Forms.LinkArea.Length" /> property of the <see cref="T:System.Windows.Forms.LinkArea" /> object is less than -1.</exception>
		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06002B4F RID: 11087 RVA: 0x000C26D4 File Offset: 0x000C08D4
		// (set) Token: 0x06002B50 RID: 11088 RVA: 0x000C2728 File Offset: 0x000C0928
		[Editor("System.Windows.Forms.Design.LinkAreaEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[RefreshProperties(RefreshProperties.Repaint)]
		[Localizable(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("LinkLabelLinkAreaDescr")]
		public LinkArea LinkArea
		{
			get
			{
				if (this.links.Count == 0)
				{
					return new LinkArea(0, 0);
				}
				return new LinkArea(((LinkLabel.Link)this.links[0]).Start, ((LinkLabel.Link)this.links[0]).Length);
			}
			set
			{
				LinkArea linkArea = this.LinkArea;
				this.links.Clear();
				if (!value.IsEmpty)
				{
					if (value.Start < 0)
					{
						throw new ArgumentOutOfRangeException("LinkArea", value, SR.GetString("LinkLabelAreaStart"));
					}
					if (value.Length < -1)
					{
						throw new ArgumentOutOfRangeException("LinkArea", value, SR.GetString("LinkLabelAreaLength"));
					}
					if (value.Start != 0 || value.Length != 0)
					{
						this.Links.Add(new LinkLabel.Link(this));
						((LinkLabel.Link)this.links[0]).Start = value.Start;
						((LinkLabel.Link)this.links[0]).Length = value.Length;
					}
				}
				this.UpdateSelectability();
				if (!linkArea.Equals(this.LinkArea))
				{
					this.InvalidateTextLayout();
					LayoutTransaction.DoLayout(this.ParentInternal, this, PropertyNames.LinkArea);
					base.AdjustSize();
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets a value that represents the behavior of a link.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.LinkBehavior" /> values. The default is <see langword="LinkBehavior.SystemDefault" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">A value is assigned that is not one of the <see cref="T:System.Windows.Forms.LinkBehavior" /> values.</exception>
		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06002B51 RID: 11089 RVA: 0x000C283E File Offset: 0x000C0A3E
		// (set) Token: 0x06002B52 RID: 11090 RVA: 0x000C2848 File Offset: 0x000C0A48
		[DefaultValue(LinkBehavior.SystemDefault)]
		[SRCategory("CatBehavior")]
		[SRDescription("LinkLabelLinkBehaviorDescr")]
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
				if (value != this.linkBehavior)
				{
					this.linkBehavior = value;
					this.InvalidateLinkFonts();
					this.InvalidateLink(null);
				}
			}
		}

		/// <summary>Gets or sets the color used when displaying a normal link.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color used to displaying a normal link. The default color is specified by the system, typically this color is <see langword="Color.Blue" />.</returns>
		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06002B53 RID: 11091 RVA: 0x000C2898 File Offset: 0x000C0A98
		// (set) Token: 0x06002B54 RID: 11092 RVA: 0x000C28C1 File Offset: 0x000C0AC1
		[SRCategory("CatAppearance")]
		[SRDescription("LinkLabelLinkColorDescr")]
		public Color LinkColor
		{
			get
			{
				if (!this.linkColor.IsEmpty)
				{
					return this.linkColor;
				}
				if (SystemInformation.HighContrast)
				{
					return SystemColors.HotTrack;
				}
				return this.IELinkColor;
			}
			set
			{
				if (this.linkColor != value)
				{
					this.linkColor = value;
					this.InvalidateLink(null);
				}
			}
		}

		/// <summary>Gets the collection of links contained within the <see cref="T:System.Windows.Forms.LinkLabel" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.LinkLabel.LinkCollection" /> that represents the links contained within the <see cref="T:System.Windows.Forms.LinkLabel" /> control.</returns>
		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x06002B55 RID: 11093 RVA: 0x000C28DF File Offset: 0x000C0ADF
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public LinkLabel.LinkCollection Links
		{
			get
			{
				if (this.linkCollection == null)
				{
					this.linkCollection = new LinkLabel.LinkCollection(this);
				}
				return this.linkCollection;
			}
		}

		/// <summary>Gets or sets a value indicating whether a link should be displayed as though it were visited.</summary>
		/// <returns>
		///   <see langword="true" /> if links should display as though they were visited; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x06002B56 RID: 11094 RVA: 0x000C28FB File Offset: 0x000C0AFB
		// (set) Token: 0x06002B57 RID: 11095 RVA: 0x000C2924 File Offset: 0x000C0B24
		[DefaultValue(false)]
		[SRCategory("CatAppearance")]
		[SRDescription("LinkLabelLinkVisitedDescr")]
		public bool LinkVisited
		{
			get
			{
				return this.links.Count != 0 && ((LinkLabel.Link)this.links[0]).Visited;
			}
			set
			{
				if (value != this.LinkVisited)
				{
					if (this.links.Count == 0)
					{
						this.Links.Add(new LinkLabel.Link(this));
					}
					((LinkLabel.Link)this.links[0]).Visited = value;
				}
			}
		}

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x06002B58 RID: 11096 RVA: 0x00012E4E File Offset: 0x0001104E
		internal override bool OwnerDraw
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets or sets the mouse pointer to use when the mouse pointer is within the bounds of the <see cref="T:System.Windows.Forms.LinkLabel" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> to use when the mouse pointer is within the <see cref="T:System.Windows.Forms.LinkLabel" /> bounds.</returns>
		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x06002B59 RID: 11097 RVA: 0x000C2970 File Offset: 0x000C0B70
		// (set) Token: 0x06002B5A RID: 11098 RVA: 0x000C2978 File Offset: 0x000C0B78
		protected Cursor OverrideCursor
		{
			get
			{
				return this.overrideCursor;
			}
			set
			{
				if (this.overrideCursor != value)
				{
					this.overrideCursor = value;
					if (base.IsHandleCreated)
					{
						NativeMethods.POINT point = new NativeMethods.POINT();
						NativeMethods.RECT rect = default(NativeMethods.RECT);
						UnsafeNativeMethods.GetCursorPos(point);
						UnsafeNativeMethods.GetWindowRect(new HandleRef(this, base.Handle), ref rect);
						if ((rect.left <= point.x && point.x < rect.right && rect.top <= point.y && point.y < rect.bottom) || UnsafeNativeMethods.GetCapture() == base.Handle)
						{
							base.SendMessage(32, base.Handle, 1);
						}
					}
				}
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Label.TabStop" /> property changes.</summary>
		// Token: 0x140001F4 RID: 500
		// (add) Token: 0x06002B5B RID: 11099 RVA: 0x000C2A2B File Offset: 0x000C0C2B
		// (remove) Token: 0x06002B5C RID: 11100 RVA: 0x000C2A34 File Offset: 0x000C0C34
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
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

		/// <summary>Gets or sets a value that indicates whether the user can tab to the <see cref="T:System.Windows.Forms.LinkLabel" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the user can tab to the <see cref="T:System.Windows.Forms.LinkLabel" />;otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x06002B5D RID: 11101 RVA: 0x000C2A3D File Offset: 0x000C0C3D
		// (set) Token: 0x06002B5E RID: 11102 RVA: 0x000C2A45 File Offset: 0x000C0C45
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
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

		/// <summary>Gets or sets the text displayed by the <see cref="T:System.Windows.Forms.LinkLabel" />.</summary>
		/// <returns>The text displayed by the <see cref="T:System.Windows.Forms.LinkLabel" />.</returns>
		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06002B5F RID: 11103 RVA: 0x000C2A4E File Offset: 0x000C0C4E
		// (set) Token: 0x06002B60 RID: 11104 RVA: 0x000C2A56 File Offset: 0x000C0C56
		[RefreshProperties(RefreshProperties.Repaint)]
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

		/// <summary>Gets or sets the interior spacing, in pixels, between the edges of a <see cref="T:System.Windows.Forms.LinkLabel" /> and its contents.</summary>
		/// <returns>
		///   <see cref="T:System.Windows.Forms.Padding" /> values representing the interior spacing, in pixels.</returns>
		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x06002B61 RID: 11105 RVA: 0x00013442 File Offset: 0x00011642
		// (set) Token: 0x06002B62 RID: 11106 RVA: 0x0001344A File Offset: 0x0001164A
		[RefreshProperties(RefreshProperties.Repaint)]
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

		/// <summary>Gets or sets the color used when displaying a link that has been previously visited.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color used to display links that have been visited. The default color is specified by the system, typically this color is <see langword="Color.Purple" />.</returns>
		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06002B63 RID: 11107 RVA: 0x000C2A5F File Offset: 0x000C0C5F
		// (set) Token: 0x06002B64 RID: 11108 RVA: 0x000C2A88 File Offset: 0x000C0C88
		[SRCategory("CatAppearance")]
		[SRDescription("LinkLabelVisitedLinkColorDescr")]
		public Color VisitedLinkColor
		{
			get
			{
				if (!this.visitedLinkColor.IsEmpty)
				{
					return this.visitedLinkColor;
				}
				if (SystemInformation.HighContrast)
				{
					return LinkUtilities.GetVisitedLinkColor();
				}
				return this.IEVisitedLinkColor;
			}
			set
			{
				if (this.visitedLinkColor != value)
				{
					this.visitedLinkColor = value;
					this.InvalidateLink(null);
				}
			}
		}

		/// <summary>Occurs when a link is clicked within the control.</summary>
		// Token: 0x140001F5 RID: 501
		// (add) Token: 0x06002B65 RID: 11109 RVA: 0x000C2AA6 File Offset: 0x000C0CA6
		// (remove) Token: 0x06002B66 RID: 11110 RVA: 0x000C2AB9 File Offset: 0x000C0CB9
		[WinCategory("Action")]
		[SRDescription("LinkLabelLinkClickedDescr")]
		public event LinkLabelLinkClickedEventHandler LinkClicked
		{
			add
			{
				base.Events.AddHandler(LinkLabel.EventLinkClicked, value);
			}
			remove
			{
				base.Events.RemoveHandler(LinkLabel.EventLinkClicked, value);
			}
		}

		// Token: 0x06002B67 RID: 11111 RVA: 0x000C2ACC File Offset: 0x000C0CCC
		internal static Rectangle CalcTextRenderBounds(Rectangle textRect, Rectangle clientRect, ContentAlignment align)
		{
			int num;
			if ((align & WindowsFormsUtils.AnyRightAlign) != (ContentAlignment)0)
			{
				num = clientRect.Right - textRect.Width;
			}
			else if ((align & WindowsFormsUtils.AnyCenterAlign) != (ContentAlignment)0)
			{
				num = (clientRect.Width - textRect.Width) / 2;
			}
			else
			{
				num = clientRect.X;
			}
			int num2;
			if ((align & WindowsFormsUtils.AnyBottomAlign) != (ContentAlignment)0)
			{
				num2 = clientRect.Bottom - textRect.Height;
			}
			else if ((align & WindowsFormsUtils.AnyMiddleAlign) != (ContentAlignment)0)
			{
				num2 = (clientRect.Height - textRect.Height) / 2;
			}
			else
			{
				num2 = clientRect.Y;
			}
			int num3;
			if (textRect.Width > clientRect.Width)
			{
				num = clientRect.X;
				num3 = clientRect.Width;
			}
			else
			{
				num3 = textRect.Width;
			}
			int num4;
			if (textRect.Height > clientRect.Height)
			{
				num2 = clientRect.Y;
				num4 = clientRect.Height;
			}
			else
			{
				num4 = textRect.Height;
			}
			return new Rectangle(num, num2, num3, num4);
		}

		/// <summary>Creates a new accessibility object for the <see cref="T:System.Windows.Forms.LinkLabel" /> control.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the control.</returns>
		// Token: 0x06002B68 RID: 11112 RVA: 0x000C2BB6 File Offset: 0x000C0DB6
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new LinkLabel.LinkLabelAccessibleObject(this);
		}

		/// <summary>Creates a handle for this control. This method is called by the .NET Framework, this should not be called. Inheriting classes should always call <see langword="base.createHandle" /> when overriding this method.</summary>
		// Token: 0x06002B69 RID: 11113 RVA: 0x000C2BBE File Offset: 0x000C0DBE
		protected override void CreateHandle()
		{
			base.CreateHandle();
			this.InvalidateTextLayout();
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x06002B6A RID: 11114 RVA: 0x000C2BCC File Offset: 0x000C0DCC
		internal override bool CanUseTextRenderer
		{
			get
			{
				StringInfo stringInfo = new StringInfo(this.Text);
				return this.LinkArea.Start == 0 && (this.LinkArea.Length == 0 || this.LinkArea.Length == stringInfo.LengthInTextElements);
			}
		}

		// Token: 0x06002B6B RID: 11115 RVA: 0x000C2C1F File Offset: 0x000C0E1F
		internal override bool UseGDIMeasuring()
		{
			return !this.UseCompatibleTextRendering;
		}

		// Token: 0x06002B6C RID: 11116 RVA: 0x000C2C2C File Offset: 0x000C0E2C
		private static int ConvertToCharIndex(int index, string text)
		{
			if (index <= 0)
			{
				return 0;
			}
			if (string.IsNullOrEmpty(text))
			{
				return index;
			}
			StringInfo stringInfo = new StringInfo(text);
			int lengthInTextElements = stringInfo.LengthInTextElements;
			if (index > lengthInTextElements)
			{
				return index - lengthInTextElements + text.Length;
			}
			string text2 = stringInfo.SubstringByTextElements(0, index);
			return text2.Length;
		}

		// Token: 0x06002B6D RID: 11117 RVA: 0x000C2C78 File Offset: 0x000C0E78
		private void EnsureRun(Graphics g)
		{
			if (this.textLayoutValid)
			{
				return;
			}
			if (this.textRegion != null)
			{
				this.textRegion.Dispose();
				this.textRegion = null;
			}
			if (this.Text.Length == 0)
			{
				this.Links.Clear();
				this.Links.Add(new LinkLabel.Link(0, -1));
				this.textLayoutValid = true;
				return;
			}
			StringFormat stringFormat = this.CreateStringFormat();
			string text = this.Text;
			try
			{
				Font font = new Font(this.Font, this.Font.Style | FontStyle.Underline);
				Graphics graphics = null;
				try
				{
					if (g == null)
					{
						graphics = (g = base.CreateGraphicsInternal());
					}
					if (this.UseCompatibleTextRendering)
					{
						Region[] array = g.MeasureCharacterRanges(text, font, this.ClientRectWithPadding, stringFormat);
						int num = 0;
						for (int i = 0; i < this.Links.Count; i++)
						{
							LinkLabel.Link link = this.Links[i];
							int num2 = LinkLabel.ConvertToCharIndex(link.Start, text);
							int num3 = LinkLabel.ConvertToCharIndex(link.Start + link.Length, text);
							if (this.LinkInText(num2, num3 - num2))
							{
								this.Links[i].VisualRegion = array[num];
								num++;
							}
						}
						this.textRegion = array[array.Length - 1];
					}
					else
					{
						Rectangle clientRectWithPadding = this.ClientRectWithPadding;
						Size size = new Size(clientRectWithPadding.Width, clientRectWithPadding.Height);
						TextFormatFlags textFormatFlags = this.CreateTextFormatFlags(size);
						Size size2 = TextRenderer.MeasureText(text, font, size, textFormatFlags);
						int iLeftMargin;
						int iRightMargin;
						using (WindowsGraphics windowsGraphics = WindowsGraphics.FromGraphics(g))
						{
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
								iRightMargin = textMargins.iRightMargin;
							}
						}
						Rectangle rectangle = new Rectangle(clientRectWithPadding.X + iLeftMargin, clientRectWithPadding.Y, size2.Width - iRightMargin - iLeftMargin, size2.Height);
						rectangle = LinkLabel.CalcTextRenderBounds(rectangle, clientRectWithPadding, base.RtlTranslateContent(this.TextAlign));
						Region region = new Region(rectangle);
						if (this.links != null && this.links.Count == 1)
						{
							this.Links[0].VisualRegion = region;
						}
						this.textRegion = region;
					}
				}
				finally
				{
					font.Dispose();
					font = null;
					if (graphics != null)
					{
						graphics.Dispose();
						graphics = null;
					}
				}
				this.textLayoutValid = true;
			}
			finally
			{
				stringFormat.Dispose();
			}
		}

		// Token: 0x06002B6E RID: 11118 RVA: 0x000C2F7C File Offset: 0x000C117C
		internal override StringFormat CreateStringFormat()
		{
			StringFormat stringFormat = base.CreateStringFormat();
			if (string.IsNullOrEmpty(this.Text))
			{
				return stringFormat;
			}
			CharacterRange[] array = this.AdjustCharacterRangesForSurrogateChars();
			stringFormat.SetMeasurableCharacterRanges(array);
			return stringFormat;
		}

		// Token: 0x06002B6F RID: 11119 RVA: 0x000C2FB0 File Offset: 0x000C11B0
		private CharacterRange[] AdjustCharacterRangesForSurrogateChars()
		{
			string text = this.Text;
			if (string.IsNullOrEmpty(text))
			{
				return new CharacterRange[0];
			}
			StringInfo stringInfo = new StringInfo(text);
			int lengthInTextElements = stringInfo.LengthInTextElements;
			ArrayList arrayList = new ArrayList(this.Links.Count);
			foreach (object obj in this.Links)
			{
				LinkLabel.Link link = (LinkLabel.Link)obj;
				int num = LinkLabel.ConvertToCharIndex(link.Start, text);
				int num2 = LinkLabel.ConvertToCharIndex(link.Start + link.Length, text);
				if (this.LinkInText(num, num2 - num))
				{
					int num3 = Math.Min(link.Length, lengthInTextElements - link.Start);
					arrayList.Add(new CharacterRange(num, LinkLabel.ConvertToCharIndex(link.Start + num3, text) - num));
				}
			}
			CharacterRange[] array = new CharacterRange[arrayList.Count + 1];
			arrayList.CopyTo(array, 0);
			array[array.Length - 1] = new CharacterRange(0, text.Length);
			return array;
		}

		// Token: 0x06002B70 RID: 11120 RVA: 0x000C30EC File Offset: 0x000C12EC
		private bool IsOneLink()
		{
			if (this.links == null || this.links.Count != 1 || this.Text == null)
			{
				return false;
			}
			StringInfo stringInfo = new StringInfo(this.Text);
			return this.LinkArea.Start == 0 && this.LinkArea.Length == stringInfo.LengthInTextElements;
		}

		/// <summary>Gets the link located at the specified client coordinates.</summary>
		/// <param name="x">The horizontal coordinate of the point to search for a link.</param>
		/// <param name="y">The vertical coordinate of the point to search for a link.</param>
		/// <returns>A <see cref="T:System.Windows.Forms.LinkLabel.Link" /> representing the link located at the specified coordinates. If the point does not contain a link, <see langword="null" /> is returned.</returns>
		// Token: 0x06002B71 RID: 11121 RVA: 0x000C3150 File Offset: 0x000C1350
		protected LinkLabel.Link PointInLink(int x, int y)
		{
			Graphics graphics = base.CreateGraphicsInternal();
			LinkLabel.Link link = null;
			try
			{
				this.EnsureRun(graphics);
				foreach (object obj in this.links)
				{
					LinkLabel.Link link2 = (LinkLabel.Link)obj;
					if (link2.VisualRegion != null && link2.VisualRegion.IsVisible(x, y, graphics))
					{
						link = link2;
						break;
					}
				}
			}
			finally
			{
				graphics.Dispose();
				graphics = null;
			}
			return link;
		}

		// Token: 0x06002B72 RID: 11122 RVA: 0x000C31E8 File Offset: 0x000C13E8
		private void InvalidateLink(LinkLabel.Link link)
		{
			if (base.IsHandleCreated)
			{
				if (link == null || link.VisualRegion == null || this.IsOneLink())
				{
					base.Invalidate();
					return;
				}
				base.Invalidate(link.VisualRegion);
			}
		}

		// Token: 0x06002B73 RID: 11123 RVA: 0x000C3218 File Offset: 0x000C1418
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

		// Token: 0x06002B74 RID: 11124 RVA: 0x000C3267 File Offset: 0x000C1467
		private void InvalidateTextLayout()
		{
			this.textLayoutValid = false;
		}

		// Token: 0x06002B75 RID: 11125 RVA: 0x000C3270 File Offset: 0x000C1470
		private bool LinkInText(int start, int length)
		{
			return 0 <= start && start < this.Text.Length && 0 < length;
		}

		/// <summary>For a description of this member, see <see cref="P:System.Windows.Forms.IButtonControl.DialogResult" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06002B76 RID: 11126 RVA: 0x000C328A File Offset: 0x000C148A
		// (set) Token: 0x06002B77 RID: 11127 RVA: 0x000C3292 File Offset: 0x000C1492
		DialogResult IButtonControl.DialogResult
		{
			get
			{
				return this.dialogResult;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 7))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DialogResult));
				}
				this.dialogResult = value;
			}
		}

		/// <summary>Notifies the <see cref="T:System.Windows.Forms.LinkLabel" /> control that it is the default button.</summary>
		/// <param name="value">
		///   <see langword="true" /> if the control should behave as a default button; otherwise, <see langword="false" />.</param>
		// Token: 0x06002B78 RID: 11128 RVA: 0x000070A6 File Offset: 0x000052A6
		void IButtonControl.NotifyDefault(bool value)
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002B79 RID: 11129 RVA: 0x000C32C4 File Offset: 0x000C14C4
		protected override void OnGotFocus(EventArgs e)
		{
			if (!this.processingOnGotFocus)
			{
				base.OnGotFocus(e);
				this.processingOnGotFocus = true;
			}
			try
			{
				LinkLabel.Link link = this.FocusLink;
				if (link == null)
				{
					IntSecurity.ModifyFocus.Assert();
					this.Select(true, true);
				}
				else
				{
					this.InvalidateLink(link);
					this.UpdateAccessibilityLink(link);
				}
			}
			finally
			{
				if (this.processingOnGotFocus)
				{
					this.processingOnGotFocus = false;
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.LostFocus" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002B7A RID: 11130 RVA: 0x000C3338 File Offset: 0x000C1538
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			if (this.FocusLink != null)
			{
				this.InvalidateLink(this.FocusLink);
			}
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.OnKeyDown(System.Windows.Forms.KeyEventArgs)" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
		// Token: 0x06002B7B RID: 11131 RVA: 0x000C3355 File Offset: 0x000C1555
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.KeyCode == Keys.Return && this.FocusLink != null && this.FocusLink.Enabled)
			{
				this.OnLinkClicked(new LinkLabelLinkClickedEventArgs(this.FocusLink));
			}
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.OnMouseLeave(System.EventArgs)" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002B7C RID: 11132 RVA: 0x000C3390 File Offset: 0x000C1590
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			if (!base.Enabled)
			{
				return;
			}
			foreach (object obj in this.links)
			{
				LinkLabel.Link link = (LinkLabel.Link)obj;
				if ((link.State & LinkState.Hover) == LinkState.Hover || (link.State & LinkState.Active) == LinkState.Active)
				{
					bool flag = (link.State & LinkState.Active) == LinkState.Active;
					link.State &= (LinkState)(-4);
					if (flag || this.hoverLinkFont != this.linkFont)
					{
						this.InvalidateLink(link);
					}
					this.OverrideCursor = null;
				}
			}
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.OnMouseDown(System.Windows.Forms.MouseEventArgs)" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06002B7D RID: 11133 RVA: 0x000C3444 File Offset: 0x000C1644
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (!base.Enabled || e.Clicks > 1)
			{
				this.receivedDoubleClick = true;
				return;
			}
			for (int i = 0; i < this.links.Count; i++)
			{
				if ((((LinkLabel.Link)this.links[i]).State & LinkState.Hover) == LinkState.Hover)
				{
					((LinkLabel.Link)this.links[i]).State |= LinkState.Active;
					this.FocusInternal();
					if (((LinkLabel.Link)this.links[i]).Enabled)
					{
						this.FocusLink = (LinkLabel.Link)this.links[i];
						this.InvalidateLink(this.FocusLink);
					}
					base.CaptureInternal = true;
					return;
				}
			}
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.OnMouseUp(System.Windows.Forms.MouseEventArgs)" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06002B7E RID: 11134 RVA: 0x000C3510 File Offset: 0x000C1710
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (base.Disposing || base.IsDisposed)
			{
				return;
			}
			if (!base.Enabled || e.Clicks > 1 || this.receivedDoubleClick)
			{
				this.receivedDoubleClick = false;
				return;
			}
			for (int i = 0; i < this.links.Count; i++)
			{
				if ((((LinkLabel.Link)this.links[i]).State & LinkState.Active) == LinkState.Active)
				{
					((LinkLabel.Link)this.links[i]).State &= (LinkState)(-3);
					this.InvalidateLink((LinkLabel.Link)this.links[i]);
					base.CaptureInternal = false;
					LinkLabel.Link link = this.PointInLink(e.X, e.Y);
					if (link != null && link == this.FocusLink && link.Enabled)
					{
						this.OnLinkClicked(new LinkLabelLinkClickedEventArgs(link, e.Button));
					}
				}
			}
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.OnMouseMove(System.Windows.Forms.MouseEventArgs)" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06002B7F RID: 11135 RVA: 0x000C3604 File Offset: 0x000C1804
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (!base.Enabled)
			{
				return;
			}
			LinkLabel.Link link = null;
			foreach (object obj in this.links)
			{
				LinkLabel.Link link2 = (LinkLabel.Link)obj;
				if ((link2.State & LinkState.Hover) == LinkState.Hover)
				{
					link = link2;
					break;
				}
			}
			LinkLabel.Link link3 = this.PointInLink(e.X, e.Y);
			if (link3 != link)
			{
				if (link != null)
				{
					link.State &= (LinkState)(-2);
				}
				if (link3 != null)
				{
					link3.State |= LinkState.Hover;
					if (link3.Enabled)
					{
						this.OverrideCursor = Cursors.Hand;
					}
				}
				else
				{
					this.OverrideCursor = null;
				}
				if (this.hoverLinkFont != this.linkFont)
				{
					if (link != null)
					{
						this.InvalidateLink(link);
					}
					if (link3 != null)
					{
						this.InvalidateLink(link3);
					}
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.LinkLabel.LinkClicked" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs" /> that contains the event data.</param>
		// Token: 0x06002B80 RID: 11136 RVA: 0x000C36F4 File Offset: 0x000C18F4
		protected virtual void OnLinkClicked(LinkLabelLinkClickedEventArgs e)
		{
			LinkLabelLinkClickedEventHandler linkLabelLinkClickedEventHandler = (LinkLabelLinkClickedEventHandler)base.Events[LinkLabel.EventLinkClicked];
			if (linkLabelLinkClickedEventHandler != null)
			{
				linkLabelLinkClickedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.PaddingChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002B81 RID: 11137 RVA: 0x000C3722 File Offset: 0x000C1922
		protected override void OnPaddingChanged(EventArgs e)
		{
			base.OnPaddingChanged(e);
			this.InvalidateTextLayout();
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.OnPaint(System.Windows.Forms.PaintEventArgs)" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		// Token: 0x06002B82 RID: 11138 RVA: 0x000C3734 File Offset: 0x000C1934
		protected override void OnPaint(PaintEventArgs e)
		{
			RectangleF rectangleF = RectangleF.Empty;
			base.Animate();
			ImageAnimator.UpdateFrames(base.Image);
			this.EnsureRun(e.Graphics);
			if (this.Text.Length == 0)
			{
				this.PaintLinkBackground(e.Graphics);
			}
			else
			{
				if (base.AutoEllipsis)
				{
					Rectangle clientRectWithPadding = this.ClientRectWithPadding;
					Size preferredSize = this.GetPreferredSize(new Size(clientRectWithPadding.Width, clientRectWithPadding.Height));
					this.showToolTip = clientRectWithPadding.Width < preferredSize.Width || clientRectWithPadding.Height < preferredSize.Height;
				}
				else
				{
					this.showToolTip = false;
				}
				if (base.Enabled)
				{
					bool flag = !base.GetStyle(ControlStyles.OptimizedDoubleBuffer);
					SolidBrush solidBrush = new SolidBrush(this.ForeColor);
					SolidBrush solidBrush2 = new SolidBrush(this.LinkColor);
					try
					{
						if (!flag)
						{
							this.PaintLinkBackground(e.Graphics);
						}
						LinkUtilities.EnsureLinkFonts(this.Font, this.LinkBehavior, ref this.linkFont, ref this.hoverLinkFont);
						Region clip = e.Graphics.Clip;
						try
						{
							if (this.IsOneLink())
							{
								e.Graphics.Clip = clip;
								RectangleF[] regionScans = ((LinkLabel.Link)this.links[0]).VisualRegion.GetRegionScans(e.Graphics.Transform);
								if (regionScans == null || regionScans.Length == 0)
								{
									goto IL_2B6;
								}
								if (this.UseCompatibleTextRendering)
								{
									rectangleF = new RectangleF(regionScans[0].Location, SizeF.Empty);
									foreach (RectangleF rectangleF2 in regionScans)
									{
										rectangleF = RectangleF.Union(rectangleF, rectangleF2);
									}
								}
								else
								{
									rectangleF = this.ClientRectWithPadding;
									Size size = rectangleF.Size.ToSize();
									Size textSize = base.MeasureTextCache.GetTextSize(this.Text, this.Font, size, this.CreateTextFormatFlags(size));
									rectangleF.Width = (float)textSize.Width;
									if ((float)textSize.Height < rectangleF.Height)
									{
										rectangleF.Height = (float)textSize.Height;
									}
									rectangleF = LinkLabel.CalcTextRenderBounds(Rectangle.Round(rectangleF), this.ClientRectWithPadding, base.RtlTranslateContent(this.TextAlign));
								}
								using (Region region = new Region(rectangleF))
								{
									e.Graphics.ExcludeClip(region);
									goto IL_2B6;
								}
							}
							foreach (object obj in this.links)
							{
								LinkLabel.Link link = (LinkLabel.Link)obj;
								if (link.VisualRegion != null)
								{
									e.Graphics.ExcludeClip(link.VisualRegion);
								}
							}
							IL_2B6:
							if (!this.IsOneLink())
							{
								this.PaintLink(e.Graphics, null, solidBrush, solidBrush2, flag, rectangleF);
							}
							foreach (object obj2 in this.links)
							{
								LinkLabel.Link link2 = (LinkLabel.Link)obj2;
								this.PaintLink(e.Graphics, link2, solidBrush, solidBrush2, flag, rectangleF);
							}
							if (flag)
							{
								e.Graphics.Clip = clip;
								e.Graphics.ExcludeClip(this.textRegion);
								this.PaintLinkBackground(e.Graphics);
							}
							goto IL_456;
						}
						finally
						{
							e.Graphics.Clip = clip;
						}
					}
					finally
					{
						solidBrush.Dispose();
						solidBrush2.Dispose();
					}
				}
				Region clip2 = e.Graphics.Clip;
				try
				{
					this.PaintLinkBackground(e.Graphics);
					e.Graphics.IntersectClip(this.textRegion);
					if (this.UseCompatibleTextRendering)
					{
						StringFormat stringFormat = this.CreateStringFormat();
						ControlPaint.DrawStringDisabled(e.Graphics, this.Text, this.Font, base.DisabledColor, this.ClientRectWithPadding, stringFormat);
					}
					else
					{
						IntPtr hdc = e.Graphics.GetHdc();
						Color nearestColor;
						try
						{
							using (WindowsGraphics windowsGraphics = WindowsGraphics.FromHdc(hdc))
							{
								nearestColor = windowsGraphics.GetNearestColor(base.DisabledColor);
							}
						}
						finally
						{
							e.Graphics.ReleaseHdc();
						}
						Rectangle clientRectWithPadding2 = this.ClientRectWithPadding;
						ControlPaint.DrawStringDisabled(e.Graphics, this.Text, this.Font, nearestColor, clientRectWithPadding2, this.CreateTextFormatFlags(clientRectWithPadding2.Size));
					}
				}
				finally
				{
					e.Graphics.Clip = clip2;
				}
			}
			IL_456:
			base.RaisePaintEvent(this, e);
		}

		/// <summary>Paints the background of the control.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		// Token: 0x06002B83 RID: 11139 RVA: 0x000C3C64 File Offset: 0x000C1E64
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			Image image = base.Image;
			if (image != null)
			{
				Region clip = e.Graphics.Clip;
				Rectangle rectangle = base.CalcImageRenderBounds(image, base.ClientRectangle, base.RtlTranslateAlignment(base.ImageAlign));
				e.Graphics.ExcludeClip(rectangle);
				try
				{
					base.OnPaintBackground(e);
				}
				finally
				{
					e.Graphics.Clip = clip;
				}
				e.Graphics.IntersectClip(rectangle);
				try
				{
					base.OnPaintBackground(e);
					base.DrawImage(e.Graphics, image, base.ClientRectangle, base.RtlTranslateAlignment(base.ImageAlign));
					return;
				}
				finally
				{
					e.Graphics.Clip = clip;
				}
			}
			base.OnPaintBackground(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002B84 RID: 11140 RVA: 0x000C3D2C File Offset: 0x000C1F2C
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.InvalidateTextLayout();
			this.InvalidateLinkFonts();
			base.Invalidate();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Label.AutoSizeChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002B85 RID: 11141 RVA: 0x000C3D47 File Offset: 0x000C1F47
		protected override void OnAutoSizeChanged(EventArgs e)
		{
			base.OnAutoSizeChanged(e);
			this.InvalidateTextLayout();
		}

		// Token: 0x06002B86 RID: 11142 RVA: 0x000C3D56 File Offset: 0x000C1F56
		internal override void OnAutoEllipsisChanged()
		{
			base.OnAutoEllipsisChanged();
			this.InvalidateTextLayout();
		}

		/// <summary>Provides handling for the <see cref="E:System.Windows.Forms.Control.EnabledChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002B87 RID: 11143 RVA: 0x000C3D64 File Offset: 0x000C1F64
		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
			if (!base.Enabled)
			{
				for (int i = 0; i < this.links.Count; i++)
				{
					((LinkLabel.Link)this.links[i]).State &= (LinkState)(-4);
				}
				this.OverrideCursor = null;
			}
			this.InvalidateTextLayout();
			base.Invalidate();
		}

		/// <summary>Provides handling for the <see cref="E:System.Windows.Forms.Control.TextChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002B88 RID: 11144 RVA: 0x000C3DC8 File Offset: 0x000C1FC8
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			this.InvalidateTextLayout();
			this.UpdateSelectability();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Label.TextAlignChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002B89 RID: 11145 RVA: 0x000C3DDD File Offset: 0x000C1FDD
		protected override void OnTextAlignChanged(EventArgs e)
		{
			base.OnTextAlignChanged(e);
			this.InvalidateTextLayout();
			this.UpdateSelectability();
		}

		// Token: 0x06002B8A RID: 11146 RVA: 0x000C3DF4 File Offset: 0x000C1FF4
		private void PaintLink(Graphics g, LinkLabel.Link link, SolidBrush foreBrush, SolidBrush linkBrush, bool optimizeBackgroundRendering, RectangleF finalrect)
		{
			Font font = this.Font;
			if (link != null)
			{
				if (link.VisualRegion != null)
				{
					Color color = Color.Empty;
					LinkState state = link.State;
					if ((state & LinkState.Hover) == LinkState.Hover)
					{
						font = this.hoverLinkFont;
					}
					else
					{
						font = this.linkFont;
					}
					if (link.Enabled)
					{
						if ((state & LinkState.Active) == LinkState.Active)
						{
							color = this.ActiveLinkColor;
						}
						else if ((state & LinkState.Visited) == LinkState.Visited)
						{
							color = this.VisitedLinkColor;
						}
					}
					else
					{
						color = this.DisabledLinkColor;
					}
					if (this.IsOneLink())
					{
						g.Clip = new Region(finalrect);
					}
					else
					{
						g.Clip = link.VisualRegion;
					}
					if (optimizeBackgroundRendering)
					{
						this.PaintLinkBackground(g);
					}
					if (this.UseCompatibleTextRendering)
					{
						SolidBrush solidBrush = ((color == Color.Empty) ? linkBrush : new SolidBrush(color));
						StringFormat stringFormat = this.CreateStringFormat();
						g.DrawString(this.Text, font, solidBrush, this.ClientRectWithPadding, stringFormat);
						if (solidBrush != linkBrush)
						{
							solidBrush.Dispose();
						}
					}
					else
					{
						if (color == Color.Empty)
						{
							color = linkBrush.Color;
						}
						IntPtr hdc = g.GetHdc();
						try
						{
							using (WindowsGraphics windowsGraphics = WindowsGraphics.FromHdc(hdc))
							{
								color = windowsGraphics.GetNearestColor(color);
							}
						}
						finally
						{
							g.ReleaseHdc();
						}
						Rectangle clientRectWithPadding = this.ClientRectWithPadding;
						TextRenderer.DrawText(g, this.Text, font, clientRectWithPadding, color, this.CreateTextFormatFlags(clientRectWithPadding.Size));
					}
					if (this.Focused && this.ShowFocusCues && this.FocusLink == link)
					{
						RectangleF[] regionScans = link.VisualRegion.GetRegionScans(g.Transform);
						if (regionScans != null && regionScans.Length != 0)
						{
							if (this.IsOneLink())
							{
								Rectangle rectangle = Rectangle.Ceiling(finalrect);
								ControlPaint.DrawFocusRectangle(g, rectangle, this.ForeColor, this.BackColor);
								return;
							}
							foreach (RectangleF rectangleF in regionScans)
							{
								ControlPaint.DrawFocusRectangle(g, Rectangle.Ceiling(rectangleF), this.ForeColor, this.BackColor);
							}
							return;
						}
					}
				}
			}
			else
			{
				g.IntersectClip(this.textRegion);
				if (optimizeBackgroundRendering)
				{
					this.PaintLinkBackground(g);
				}
				if (this.UseCompatibleTextRendering)
				{
					StringFormat stringFormat2 = this.CreateStringFormat();
					g.DrawString(this.Text, font, foreBrush, this.ClientRectWithPadding, stringFormat2);
					return;
				}
				IntPtr hdc2 = g.GetHdc();
				Color nearestColor;
				try
				{
					using (WindowsGraphics windowsGraphics2 = WindowsGraphics.FromHdc(hdc2))
					{
						nearestColor = windowsGraphics2.GetNearestColor(foreBrush.Color);
					}
				}
				finally
				{
					g.ReleaseHdc();
				}
				Rectangle clientRectWithPadding2 = this.ClientRectWithPadding;
				TextRenderer.DrawText(g, this.Text, font, clientRectWithPadding2, nearestColor, this.CreateTextFormatFlags(clientRectWithPadding2.Size));
			}
		}

		// Token: 0x06002B8B RID: 11147 RVA: 0x000C40C8 File Offset: 0x000C22C8
		private void PaintLinkBackground(Graphics g)
		{
			using (PaintEventArgs paintEventArgs = new PaintEventArgs(g, base.ClientRectangle))
			{
				base.InvokePaintBackground(this, paintEventArgs);
			}
		}

		/// <summary>Generates a <see cref="E:System.Windows.Forms.Control.Click" /> event for the <see cref="T:System.Windows.Forms.LinkLabel" /> control.</summary>
		// Token: 0x06002B8C RID: 11148 RVA: 0x000C4108 File Offset: 0x000C2308
		void IButtonControl.PerformClick()
		{
			if (this.FocusLink == null && this.Links.Count > 0)
			{
				string text = this.Text;
				foreach (object obj in this.Links)
				{
					LinkLabel.Link link = (LinkLabel.Link)obj;
					int num = LinkLabel.ConvertToCharIndex(link.Start, text);
					int num2 = LinkLabel.ConvertToCharIndex(link.Start + link.Length, text);
					if (link.Enabled && this.LinkInText(num, num2 - num))
					{
						this.FocusLink = link;
						break;
					}
				}
			}
			if (this.FocusLink != null)
			{
				this.OnLinkClicked(new LinkLabelLinkClickedEventArgs(this.FocusLink));
			}
		}

		/// <summary>Processes a dialog key.</summary>
		/// <param name="keyData">Key code and modifier flags.</param>
		/// <returns>
		///   <see langword="true" /> to consume the key; <see langword="false" /> to allow further processing.</returns>
		// Token: 0x06002B8D RID: 11149 RVA: 0x000C41D8 File Offset: 0x000C23D8
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if ((keyData & (Keys.Control | Keys.Alt)) != Keys.Alt)
			{
				Keys keys = keyData & Keys.KeyCode;
				if (keys != Keys.Tab)
				{
					if (keys - Keys.Left > 1)
					{
						if (keys - Keys.Right <= 1)
						{
							if (this.FocusNextLink(true))
							{
								return true;
							}
						}
					}
					else if (this.FocusNextLink(false))
					{
						return true;
					}
				}
				else if (this.TabStop)
				{
					bool flag = (keyData & Keys.Shift) != Keys.Shift;
					if (this.FocusNextLink(flag))
					{
						return true;
					}
				}
			}
			return base.ProcessDialogKey(keyData);
		}

		// Token: 0x06002B8E RID: 11150 RVA: 0x000C4254 File Offset: 0x000C2454
		private bool FocusNextLink(bool forward)
		{
			int num = -1;
			if (this.focusLink != null)
			{
				for (int i = 0; i < this.links.Count; i++)
				{
					if (this.links[i] == this.focusLink)
					{
						num = i;
						break;
					}
				}
			}
			num = this.GetNextLinkIndex(num, forward);
			if (num != -1)
			{
				this.FocusLink = this.Links[num];
				return true;
			}
			this.FocusLink = null;
			return false;
		}

		// Token: 0x06002B8F RID: 11151 RVA: 0x000C42C4 File Offset: 0x000C24C4
		private int GetNextLinkIndex(int focusIndex, bool forward)
		{
			string text = this.Text;
			int num = 0;
			int num2 = 0;
			if (forward)
			{
				do
				{
					focusIndex++;
					LinkLabel.Link link;
					if (focusIndex < this.Links.Count)
					{
						link = this.Links[focusIndex];
						num = LinkLabel.ConvertToCharIndex(link.Start, text);
						num2 = LinkLabel.ConvertToCharIndex(link.Start + link.Length, text);
					}
					else
					{
						link = null;
					}
					if (link == null || link.Enabled)
					{
						break;
					}
				}
				while (this.LinkInText(num, num2 - num));
			}
			else
			{
				LinkLabel.Link link;
				do
				{
					focusIndex--;
					if (focusIndex >= 0)
					{
						link = this.Links[focusIndex];
						num = LinkLabel.ConvertToCharIndex(link.Start, text);
						num2 = LinkLabel.ConvertToCharIndex(link.Start + link.Length, text);
					}
					else
					{
						link = null;
					}
				}
				while (link != null && !link.Enabled && this.LinkInText(num, num2 - num));
			}
			if (focusIndex < 0 || focusIndex >= this.links.Count)
			{
				return -1;
			}
			return focusIndex;
		}

		// Token: 0x06002B90 RID: 11152 RVA: 0x000C43A4 File Offset: 0x000C25A4
		private void ResetLinkArea()
		{
			this.LinkArea = new LinkArea(0, -1);
		}

		// Token: 0x06002B91 RID: 11153 RVA: 0x000C43B3 File Offset: 0x000C25B3
		internal void ResetActiveLinkColor()
		{
			this.activeLinkColor = Color.Empty;
		}

		// Token: 0x06002B92 RID: 11154 RVA: 0x000C43C0 File Offset: 0x000C25C0
		internal void ResetDisabledLinkColor()
		{
			this.disabledLinkColor = Color.Empty;
		}

		// Token: 0x06002B93 RID: 11155 RVA: 0x000C43CD File Offset: 0x000C25CD
		internal void ResetLinkColor()
		{
			this.linkColor = Color.Empty;
			this.InvalidateLink(null);
		}

		// Token: 0x06002B94 RID: 11156 RVA: 0x000C43E1 File Offset: 0x000C25E1
		private void ResetVisitedLinkColor()
		{
			this.visitedLinkColor = Color.Empty;
		}

		/// <summary>Performs the work of setting the bounds of this control.</summary>
		/// <param name="x">New left of the control.</param>
		/// <param name="y">New right of the control.</param>
		/// <param name="width">New width of the control.</param>
		/// <param name="height">New height of the control.</param>
		/// <param name="specified">Which values were specified. This parameter reflects user intent, not which values have changed.</param>
		// Token: 0x06002B95 RID: 11157 RVA: 0x000C43EE File Offset: 0x000C25EE
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			this.InvalidateTextLayout();
			base.Invalidate();
			base.SetBoundsCore(x, y, width, height, specified);
		}

		/// <summary>Activates a child control. Optionally specifies the direction in the tab order to select the control from.</summary>
		/// <param name="directed">
		///   <see langword="true" /> to specify the direction of the control to select; otherwise, <see langword="false" />.</param>
		/// <param name="forward">
		///   <see langword="true" /> to move forward in the tab order; <see langword="false" /> to move backward in the tab order.</param>
		// Token: 0x06002B96 RID: 11158 RVA: 0x000C440C File Offset: 0x000C260C
		protected override void Select(bool directed, bool forward)
		{
			if (directed && this.links.Count > 0)
			{
				int num = -1;
				if (this.FocusLink != null)
				{
					num = this.links.IndexOf(this.FocusLink);
				}
				this.FocusLink = null;
				int num2 = this.GetNextLinkIndex(num, forward);
				if (num2 == -1)
				{
					if (forward)
					{
						num2 = this.GetNextLinkIndex(-1, forward);
					}
					else
					{
						num2 = this.GetNextLinkIndex(this.links.Count, forward);
					}
				}
				if (num2 != -1)
				{
					this.FocusLink = (LinkLabel.Link)this.links[num2];
				}
			}
			base.Select(directed, forward);
		}

		// Token: 0x06002B97 RID: 11159 RVA: 0x000C449E File Offset: 0x000C269E
		internal bool ShouldSerializeActiveLinkColor()
		{
			return !this.activeLinkColor.IsEmpty;
		}

		// Token: 0x06002B98 RID: 11160 RVA: 0x000C44AE File Offset: 0x000C26AE
		internal bool ShouldSerializeDisabledLinkColor()
		{
			return !this.disabledLinkColor.IsEmpty;
		}

		// Token: 0x06002B99 RID: 11161 RVA: 0x000C44BE File Offset: 0x000C26BE
		private bool ShouldSerializeLinkArea()
		{
			return this.links.Count != 1 || this.Links[0].Start != 0 || this.Links[0].length != -1;
		}

		// Token: 0x06002B9A RID: 11162 RVA: 0x000C44FC File Offset: 0x000C26FC
		internal bool ShouldSerializeLinkColor()
		{
			return !this.linkColor.IsEmpty;
		}

		// Token: 0x06002B9B RID: 11163 RVA: 0x000C450C File Offset: 0x000C270C
		private bool ShouldSerializeUseCompatibleTextRendering()
		{
			return !this.CanUseTextRenderer || this.UseCompatibleTextRendering != Control.UseCompatibleTextRenderingDefault;
		}

		// Token: 0x06002B9C RID: 11164 RVA: 0x000C4528 File Offset: 0x000C2728
		private bool ShouldSerializeVisitedLinkColor()
		{
			return !this.visitedLinkColor.IsEmpty;
		}

		// Token: 0x06002B9D RID: 11165 RVA: 0x000C4538 File Offset: 0x000C2738
		private void UpdateAccessibilityLink(LinkLabel.Link focusLink)
		{
			if (!base.IsHandleCreated)
			{
				return;
			}
			int num = -1;
			for (int i = 0; i < this.links.Count; i++)
			{
				if (this.links[i] == focusLink)
				{
					num = i;
				}
			}
			base.AccessibilityNotifyClients(AccessibleEvents.Focus, num);
		}

		// Token: 0x06002B9E RID: 11166 RVA: 0x000C4584 File Offset: 0x000C2784
		private void ValidateNoOverlappingLinks()
		{
			for (int i = 0; i < this.links.Count; i++)
			{
				LinkLabel.Link link = (LinkLabel.Link)this.links[i];
				if (link.Length < 0)
				{
					throw new InvalidOperationException(SR.GetString("LinkLabelOverlap"));
				}
				for (int j = i; j < this.links.Count; j++)
				{
					if (i != j)
					{
						LinkLabel.Link link2 = (LinkLabel.Link)this.links[j];
						int num = Math.Max(link.Start, link2.Start);
						int num2 = Math.Min(link.Start + link.Length, link2.Start + link2.Length);
						if (num < num2)
						{
							throw new InvalidOperationException(SR.GetString("LinkLabelOverlap"));
						}
					}
				}
			}
		}

		// Token: 0x06002B9F RID: 11167 RVA: 0x000C4650 File Offset: 0x000C2850
		private void UpdateSelectability()
		{
			LinkArea linkArea = this.LinkArea;
			bool flag = false;
			string text = this.Text;
			int num = LinkLabel.ConvertToCharIndex(linkArea.Start, text);
			int num2 = LinkLabel.ConvertToCharIndex(linkArea.Start + linkArea.Length, text);
			if (this.LinkInText(num, num2 - num))
			{
				flag = true;
			}
			else if (this.FocusLink != null)
			{
				this.FocusLink = null;
			}
			this.OverrideCursor = null;
			this.TabStop = flag;
			base.SetStyle(ControlStyles.Selectable, flag);
		}

		/// <summary>Gets or sets a value that determines whether to use the <see cref="T:System.Drawing.Graphics" /> class (GDI+) or the <see cref="T:System.Windows.Forms.TextRenderer" /> class (GDI) to render text.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Drawing.Graphics" /> class should be used to perform text rendering for compatibility with versions 1.0 and 1.1. of the .NET Framework; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06002BA0 RID: 11168 RVA: 0x000C46CC File Offset: 0x000C28CC
		// (set) Token: 0x06002BA1 RID: 11169 RVA: 0x000C46D4 File Offset: 0x000C28D4
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRCategory("CatBehavior")]
		[SRDescription("UseCompatibleTextRenderingDescr")]
		public new bool UseCompatibleTextRendering
		{
			get
			{
				return base.UseCompatibleTextRendering;
			}
			set
			{
				if (base.UseCompatibleTextRendering != value)
				{
					base.UseCompatibleTextRendering = value;
					this.InvalidateTextLayout();
				}
			}
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06002BA2 RID: 11170 RVA: 0x0001180C File Offset: 0x0000FA0C
		internal override bool SupportsUiaProviders
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002BA3 RID: 11171 RVA: 0x000C46EC File Offset: 0x000C28EC
		private void WmSetCursor(ref Message m)
		{
			if (!(m.WParam == base.InternalHandle) || NativeMethods.Util.LOWORD(m.LParam) != 1)
			{
				this.DefWndProc(ref m);
				return;
			}
			if (this.OverrideCursor != null)
			{
				Cursor.CurrentInternal = this.OverrideCursor;
				return;
			}
			Cursor.CurrentInternal = this.Cursor;
		}

		/// <summary>Processes the specified Windows message.</summary>
		/// <param name="msg">The message to process.</param>
		// Token: 0x06002BA4 RID: 11172 RVA: 0x000C4748 File Offset: 0x000C2948
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message msg)
		{
			int msg2 = msg.Msg;
			if (msg2 == 32)
			{
				this.WmSetCursor(ref msg);
				return;
			}
			base.WndProc(ref msg);
		}

		// Token: 0x04001229 RID: 4649
		private static readonly object EventLinkClicked = new object();

		// Token: 0x0400122A RID: 4650
		private static Color iedisabledLinkColor = Color.Empty;

		// Token: 0x0400122B RID: 4651
		private static LinkLabel.LinkComparer linkComparer = new LinkLabel.LinkComparer();

		// Token: 0x0400122C RID: 4652
		private DialogResult dialogResult;

		// Token: 0x0400122D RID: 4653
		private Color linkColor = Color.Empty;

		// Token: 0x0400122E RID: 4654
		private Color activeLinkColor = Color.Empty;

		// Token: 0x0400122F RID: 4655
		private Color visitedLinkColor = Color.Empty;

		// Token: 0x04001230 RID: 4656
		private Color disabledLinkColor = Color.Empty;

		// Token: 0x04001231 RID: 4657
		private Font linkFont;

		// Token: 0x04001232 RID: 4658
		private Font hoverLinkFont;

		// Token: 0x04001233 RID: 4659
		private bool textLayoutValid;

		// Token: 0x04001234 RID: 4660
		private bool receivedDoubleClick;

		// Token: 0x04001235 RID: 4661
		private ArrayList links = new ArrayList(2);

		// Token: 0x04001236 RID: 4662
		private LinkLabel.Link focusLink;

		// Token: 0x04001237 RID: 4663
		private LinkLabel.LinkCollection linkCollection;

		// Token: 0x04001238 RID: 4664
		private Region textRegion;

		// Token: 0x04001239 RID: 4665
		private Cursor overrideCursor;

		// Token: 0x0400123A RID: 4666
		private bool processingOnGotFocus;

		// Token: 0x0400123B RID: 4667
		private LinkBehavior linkBehavior;

		/// <summary>Represents the collection of links within a <see cref="T:System.Windows.Forms.LinkLabel" /> control.</summary>
		// Token: 0x020006B9 RID: 1721
		public class LinkCollection : IList, ICollection, IEnumerable
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LinkLabel.LinkCollection" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.LinkLabel" /> control that owns the collection.</param>
			// Token: 0x060068C3 RID: 26819 RVA: 0x001853AE File Offset: 0x001835AE
			public LinkCollection(LinkLabel owner)
			{
				if (owner == null)
				{
					throw new ArgumentNullException("owner");
				}
				this.owner = owner;
			}

			/// <summary>Gets or sets the link at the specified index within the collection.</summary>
			/// <param name="index">The index of the link in the collection to get.</param>
			/// <returns>An object representing the link located at the specified index within the collection.</returns>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="index" /> is a negative value or greater than the number of items in the collection.</exception>
			// Token: 0x170016A3 RID: 5795
			public virtual LinkLabel.Link this[int index]
			{
				get
				{
					return (LinkLabel.Link)this.owner.links[index];
				}
				set
				{
					this.owner.links[index] = value;
					this.owner.links.Sort(LinkLabel.linkComparer);
					this.owner.InvalidateTextLayout();
					this.owner.Invalidate();
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
			/// <param name="index">The zero-based index of the element to get or set.</param>
			/// <returns>The element at the specified index.</returns>
			// Token: 0x170016A4 RID: 5796
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					if (value is LinkLabel.Link)
					{
						this[index] = (LinkLabel.Link)value;
						return;
					}
					throw new ArgumentException(SR.GetString("LinkLabelBadLink"), "value");
				}
			}

			/// <summary>Gets a link with the specified key from the collection.</summary>
			/// <param name="key">The name of the link to retrieve from the collection.</param>
			/// <returns>The <see cref="T:System.Windows.Forms.LinkLabel.Link" /> with the specified key within the collection.</returns>
			// Token: 0x170016A5 RID: 5797
			public virtual LinkLabel.Link this[string key]
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

			/// <summary>Gets the number of links in the collection.</summary>
			/// <returns>The number of links in the collection.</returns>
			// Token: 0x170016A6 RID: 5798
			// (get) Token: 0x060068C9 RID: 26825 RVA: 0x00185491 File Offset: 0x00183691
			[Browsable(false)]
			public int Count
			{
				get
				{
					return this.owner.links.Count;
				}
			}

			/// <summary>Gets a value indicating whether links have been added to the <see cref="T:System.Windows.Forms.LinkLabel.LinkCollection" />.</summary>
			/// <returns>
			///   <see langword="true" /> if links have been added to the <see cref="T:System.Windows.Forms.LinkLabel.LinkCollection" />; otherwise, <see langword="false" />.</returns>
			// Token: 0x170016A7 RID: 5799
			// (get) Token: 0x060068CA RID: 26826 RVA: 0x001854A3 File Offset: 0x001836A3
			public bool LinksAdded
			{
				get
				{
					return this.linksAdded;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
			/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
			// Token: 0x170016A8 RID: 5800
			// (get) Token: 0x060068CB RID: 26827 RVA: 0x00006A49 File Offset: 0x00004C49
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
			/// <returns>
			///   <see langword="true" /> if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, <see langword="false" />.</returns>
			// Token: 0x170016A9 RID: 5801
			// (get) Token: 0x060068CC RID: 26828 RVA: 0x0001180C File Offset: 0x0000FA0C
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
			/// <returns>
			///   <see langword="true" /> if the <see cref="T:System.Collections.IList" /> has a fixed size; otherwise, <see langword="false" />.</returns>
			// Token: 0x170016AA RID: 5802
			// (get) Token: 0x060068CD RID: 26829 RVA: 0x0001180C File Offset: 0x0000FA0C
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets a value indicating whether this collection is read-only.</summary>
			/// <returns>
			///   <see langword="true" /> if the collection is read-only; otherwise, <see langword="false" />.</returns>
			// Token: 0x170016AB RID: 5803
			// (get) Token: 0x060068CE RID: 26830 RVA: 0x0001180C File Offset: 0x0000FA0C
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			/// <summary>Adds a link to the collection.</summary>
			/// <param name="start">The starting character within the text of the label where the link is created.</param>
			/// <param name="length">The number of characters after the starting character to include in the link text.</param>
			/// <returns>A <see cref="T:System.Windows.Forms.LinkLabel.Link" /> representing the link that was created and added to the collection.</returns>
			// Token: 0x060068CF RID: 26831 RVA: 0x001854AB File Offset: 0x001836AB
			public LinkLabel.Link Add(int start, int length)
			{
				if (length != 0)
				{
					this.linksAdded = true;
				}
				return this.Add(start, length, null);
			}

			/// <summary>Adds a link to the collection with information to associate with the link.</summary>
			/// <param name="start">The starting character within the text of the label where the link is created.</param>
			/// <param name="length">The number of characters after the starting character to include in the link text.</param>
			/// <param name="linkData">The object containing the information to associate with the link.</param>
			/// <returns>A <see cref="T:System.Windows.Forms.LinkLabel.Link" /> representing the link that was created and added to the collection.</returns>
			// Token: 0x060068D0 RID: 26832 RVA: 0x001854C0 File Offset: 0x001836C0
			public LinkLabel.Link Add(int start, int length, object linkData)
			{
				if (length != 0)
				{
					this.linksAdded = true;
				}
				if (this.owner.links.Count == 1 && this[0].Start == 0 && this[0].length == -1)
				{
					this.owner.links.Clear();
					this.owner.FocusLink = null;
				}
				LinkLabel.Link link = new LinkLabel.Link(this.owner);
				link.Start = start;
				link.Length = length;
				link.LinkData = linkData;
				this.Add(link);
				return link;
			}

			/// <summary>Adds a link with the specified value to the collection.</summary>
			/// <param name="value">A <see cref="T:System.Windows.Forms.LinkLabel.Link" /> representing the link to add.</param>
			/// <returns>The zero-based index where the link specified by the <paramref name="value" /> parameter is located in the collection.</returns>
			// Token: 0x060068D1 RID: 26833 RVA: 0x00185550 File Offset: 0x00183750
			public int Add(LinkLabel.Link value)
			{
				if (value != null && value.Length != 0)
				{
					this.linksAdded = true;
				}
				if (this.owner.links.Count == 1 && this[0].Start == 0 && this[0].length == -1)
				{
					this.owner.links.Clear();
					this.owner.FocusLink = null;
				}
				value.Owner = this.owner;
				this.owner.links.Add(value);
				if (this.owner.AutoSize)
				{
					LayoutTransaction.DoLayout(this.owner.ParentInternal, this.owner, PropertyNames.Links);
					this.owner.AdjustSize();
					this.owner.Invalidate();
				}
				if (this.owner.Links.Count > 1)
				{
					this.owner.links.Sort(LinkLabel.linkComparer);
				}
				this.owner.ValidateNoOverlappingLinks();
				this.owner.UpdateSelectability();
				this.owner.InvalidateTextLayout();
				this.owner.Invalidate();
				if (this.owner.Links.Count > 1)
				{
					return this.IndexOf(value);
				}
				return 0;
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
			/// <param name="value">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
			/// <returns>The position into which the new element was inserted.</returns>
			// Token: 0x060068D2 RID: 26834 RVA: 0x00185687 File Offset: 0x00183887
			int IList.Add(object value)
			{
				if (value is LinkLabel.Link)
				{
					return this.Add((LinkLabel.Link)value);
				}
				throw new ArgumentException(SR.GetString("LinkLabelBadLink"), "value");
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
			/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
			/// <param name="value">The <see cref="T:System.Object" /> to insert into the <see cref="T:System.Collections.IList" />.</param>
			// Token: 0x060068D3 RID: 26835 RVA: 0x001856B2 File Offset: 0x001838B2
			void IList.Insert(int index, object value)
			{
				if (value is LinkLabel.Link)
				{
					this.Add((LinkLabel.Link)value);
					return;
				}
				throw new ArgumentException(SR.GetString("LinkLabelBadLink"), "value");
			}

			/// <summary>Determines whether the specified link is within the collection.</summary>
			/// <param name="link">A <see cref="T:System.Windows.Forms.LinkLabel.Link" /> representing the link to search for in the collection.</param>
			/// <returns>
			///   <see langword="true" /> if the specified link is within the collection; otherwise, <see langword="false" />.</returns>
			// Token: 0x060068D4 RID: 26836 RVA: 0x001856DE File Offset: 0x001838DE
			public bool Contains(LinkLabel.Link link)
			{
				return this.owner.links.Contains(link);
			}

			/// <summary>Returns a value indicating whether the collection contains a link with the specified key.</summary>
			/// <param name="key">The link to search for in the collection.</param>
			/// <returns>
			///   <see langword="true" /> if the collection contains an item with the specified key; otherwise, <see langword="false" />.</returns>
			// Token: 0x060068D5 RID: 26837 RVA: 0x001856F1 File Offset: 0x001838F1
			public virtual bool ContainsKey(string key)
			{
				return this.IsValidIndex(this.IndexOfKey(key));
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
			/// <param name="link">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
			/// <returns>
			///   <see langword="true" /> if the <see cref="T:System.Object" /> is found in the <see cref="T:System.Collections.IList" />; otherwise, <see langword="false" />.</returns>
			// Token: 0x060068D6 RID: 26838 RVA: 0x00185700 File Offset: 0x00183900
			bool IList.Contains(object link)
			{
				return link is LinkLabel.Link && this.Contains((LinkLabel.Link)link);
			}

			/// <summary>Returns the index of the specified link within the collection.</summary>
			/// <param name="link">A <see cref="T:System.Windows.Forms.LinkLabel.Link" /> representing the link to search for in the collection.</param>
			/// <returns>The zero-based index where the link is located within the collection; otherwise, negative one (-1).</returns>
			// Token: 0x060068D7 RID: 26839 RVA: 0x00185718 File Offset: 0x00183918
			public int IndexOf(LinkLabel.Link link)
			{
				return this.owner.links.IndexOf(link);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
			/// <param name="link">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
			/// <returns>The index of the <paramref name="link" /> parameter, if found in the list; otherwise, -1.</returns>
			// Token: 0x060068D8 RID: 26840 RVA: 0x0018572B File Offset: 0x0018392B
			int IList.IndexOf(object link)
			{
				if (link is LinkLabel.Link)
				{
					return this.IndexOf((LinkLabel.Link)link);
				}
				return -1;
			}

			/// <summary>Retrieves the zero-based index of the first occurrence of the specified key within the entire collection.</summary>
			/// <param name="key">The key to search the collection for.</param>
			/// <returns>The zero-based index of the first occurrence of value within the entire collection, if found; otherwise, -1.</returns>
			// Token: 0x060068D9 RID: 26841 RVA: 0x00185744 File Offset: 0x00183944
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

			// Token: 0x060068DA RID: 26842 RVA: 0x001857C1 File Offset: 0x001839C1
			private bool IsValidIndex(int index)
			{
				return index >= 0 && index < this.Count;
			}

			/// <summary>Clears all links from the collection.</summary>
			// Token: 0x060068DB RID: 26843 RVA: 0x001857D4 File Offset: 0x001839D4
			public virtual void Clear()
			{
				bool flag = this.owner.links.Count > 0 && this.owner.AutoSize;
				this.owner.links.Clear();
				if (flag)
				{
					LayoutTransaction.DoLayout(this.owner.ParentInternal, this.owner, PropertyNames.Links);
					this.owner.AdjustSize();
					this.owner.Invalidate();
				}
				this.owner.UpdateSelectability();
				this.owner.InvalidateTextLayout();
				this.owner.Invalidate();
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
			/// <param name="dest">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="dest" /> at which copying begins.</param>
			// Token: 0x060068DC RID: 26844 RVA: 0x00185868 File Offset: 0x00183A68
			void ICollection.CopyTo(Array dest, int index)
			{
				this.owner.links.CopyTo(dest, index);
			}

			/// <summary>Returns an enumerator to use to iterate through the link collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the link collection.</returns>
			// Token: 0x060068DD RID: 26845 RVA: 0x0018587C File Offset: 0x00183A7C
			public IEnumerator GetEnumerator()
			{
				if (this.owner.links != null)
				{
					return this.owner.links.GetEnumerator();
				}
				return new LinkLabel.Link[0].GetEnumerator();
			}

			/// <summary>Removes the specified link from the collection.</summary>
			/// <param name="value">A <see cref="T:System.Windows.Forms.LinkLabel.Link" /> that represents the link to remove from the collection.</param>
			// Token: 0x060068DE RID: 26846 RVA: 0x001858A8 File Offset: 0x00183AA8
			public void Remove(LinkLabel.Link value)
			{
				if (value.Owner != this.owner)
				{
					return;
				}
				this.owner.links.Remove(value);
				if (this.owner.AutoSize)
				{
					LayoutTransaction.DoLayout(this.owner.ParentInternal, this.owner, PropertyNames.Links);
					this.owner.AdjustSize();
					this.owner.Invalidate();
				}
				this.owner.links.Sort(LinkLabel.linkComparer);
				this.owner.ValidateNoOverlappingLinks();
				this.owner.UpdateSelectability();
				this.owner.InvalidateTextLayout();
				this.owner.Invalidate();
				if (this.owner.FocusLink == null && this.owner.links.Count > 0)
				{
					this.owner.FocusLink = (LinkLabel.Link)this.owner.links[0];
				}
			}

			/// <summary>Removes a link at a specified location within the collection.</summary>
			/// <param name="index">The zero-based index of the item to remove from the collection.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="index" /> is a negative value or greater than the number of items in the collection.</exception>
			// Token: 0x060068DF RID: 26847 RVA: 0x00185995 File Offset: 0x00183B95
			public void RemoveAt(int index)
			{
				this.Remove(this[index]);
			}

			/// <summary>Removes the link with the specified key.</summary>
			/// <param name="key">The key of the link to remove.</param>
			// Token: 0x060068E0 RID: 26848 RVA: 0x001859A4 File Offset: 0x00183BA4
			public virtual void RemoveByKey(string key)
			{
				int num = this.IndexOfKey(key);
				if (this.IsValidIndex(num))
				{
					this.RemoveAt(num);
				}
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
			/// <param name="value">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.IList" />.</param>
			// Token: 0x060068E1 RID: 26849 RVA: 0x001859C9 File Offset: 0x00183BC9
			void IList.Remove(object value)
			{
				if (value is LinkLabel.Link)
				{
					this.Remove((LinkLabel.Link)value);
				}
			}

			// Token: 0x04003B0F RID: 15119
			private LinkLabel owner;

			// Token: 0x04003B10 RID: 15120
			private bool linksAdded;

			// Token: 0x04003B11 RID: 15121
			private int lastAccessedIndex = -1;
		}

		/// <summary>Represents a link within a <see cref="T:System.Windows.Forms.LinkLabel" /> control.</summary>
		// Token: 0x020006BA RID: 1722
		[TypeConverter(typeof(LinkConverter))]
		public class Link
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LinkLabel.Link" /> class.</summary>
			// Token: 0x060068E2 RID: 26850 RVA: 0x001859DF File Offset: 0x00183BDF
			public Link()
			{
			}

			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LinkLabel.Link" /> class with the specified starting location and number of characters after the starting location within the <see cref="T:System.Windows.Forms.LinkLabel" />.</summary>
			/// <param name="start">The zero-based starting location of the link area within the text of the <see cref="T:System.Windows.Forms.LinkLabel" />.</param>
			/// <param name="length">The number of characters, after the starting character, to include in the link area.</param>
			// Token: 0x060068E3 RID: 26851 RVA: 0x001859EE File Offset: 0x00183BEE
			public Link(int start, int length)
			{
				this.start = start;
				this.length = length;
			}

			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LinkLabel.Link" /> class with the specified starting location, number of characters after the starting location within the <see cref="T:System.Windows.Forms.LinkLabel" />, and the data associated with the link.</summary>
			/// <param name="start">The zero-based starting location of the link area within the text of the <see cref="T:System.Windows.Forms.LinkLabel" />.</param>
			/// <param name="length">The number of characters, after the starting character, to include in the link area.</param>
			/// <param name="linkData">The data associated with the link.</param>
			// Token: 0x060068E4 RID: 26852 RVA: 0x00185A0B File Offset: 0x00183C0B
			public Link(int start, int length, object linkData)
			{
				this.start = start;
				this.length = length;
				this.linkData = linkData;
			}

			// Token: 0x060068E5 RID: 26853 RVA: 0x00185A2F File Offset: 0x00183C2F
			internal Link(LinkLabel owner)
			{
				this.owner = owner;
			}

			/// <summary>Gets or sets a text description of the link.</summary>
			/// <returns>A <see cref="T:System.String" /> representing a text description of the link.</returns>
			// Token: 0x170016AC RID: 5804
			// (get) Token: 0x060068E6 RID: 26854 RVA: 0x00185A45 File Offset: 0x00183C45
			// (set) Token: 0x060068E7 RID: 26855 RVA: 0x00185A4D File Offset: 0x00183C4D
			public string Description
			{
				get
				{
					return this.description;
				}
				set
				{
					this.description = value;
				}
			}

			/// <summary>Gets or sets a value indicating whether the link is enabled.</summary>
			/// <returns>
			///   <see langword="true" /> if the link is enabled; otherwise, <see langword="false" />.</returns>
			// Token: 0x170016AD RID: 5805
			// (get) Token: 0x060068E8 RID: 26856 RVA: 0x00185A56 File Offset: 0x00183C56
			// (set) Token: 0x060068E9 RID: 26857 RVA: 0x00185A60 File Offset: 0x00183C60
			[DefaultValue(true)]
			public bool Enabled
			{
				get
				{
					return this.enabled;
				}
				set
				{
					if (this.enabled != value)
					{
						this.enabled = value;
						if ((this.state & (LinkState)3) != LinkState.Normal)
						{
							this.state &= (LinkState)(-4);
							if (this.owner != null)
							{
								this.owner.OverrideCursor = null;
							}
						}
						if (this.owner != null)
						{
							this.owner.InvalidateLink(this);
						}
					}
				}
			}

			/// <summary>Gets or sets the number of characters in the link text.</summary>
			/// <returns>The number of characters, including spaces, in the link text.</returns>
			// Token: 0x170016AE RID: 5806
			// (get) Token: 0x060068EA RID: 26858 RVA: 0x00185AC0 File Offset: 0x00183CC0
			// (set) Token: 0x060068EB RID: 26859 RVA: 0x00185B17 File Offset: 0x00183D17
			public int Length
			{
				get
				{
					if (this.length != -1)
					{
						return this.length;
					}
					if (this.owner != null && !string.IsNullOrEmpty(this.owner.Text))
					{
						StringInfo stringInfo = new StringInfo(this.owner.Text);
						return stringInfo.LengthInTextElements - this.Start;
					}
					return 0;
				}
				set
				{
					if (this.length != value)
					{
						this.length = value;
						if (this.owner != null)
						{
							this.owner.InvalidateTextLayout();
							this.owner.Invalidate();
						}
					}
				}
			}

			/// <summary>Gets or sets the data associated with the link.</summary>
			/// <returns>An <see cref="T:System.Object" /> representing the data associated with the link.</returns>
			// Token: 0x170016AF RID: 5807
			// (get) Token: 0x060068EC RID: 26860 RVA: 0x00185B47 File Offset: 0x00183D47
			// (set) Token: 0x060068ED RID: 26861 RVA: 0x00185B4F File Offset: 0x00183D4F
			[DefaultValue(null)]
			public object LinkData
			{
				get
				{
					return this.linkData;
				}
				set
				{
					this.linkData = value;
				}
			}

			// Token: 0x170016B0 RID: 5808
			// (get) Token: 0x060068EE RID: 26862 RVA: 0x00185B58 File Offset: 0x00183D58
			// (set) Token: 0x060068EF RID: 26863 RVA: 0x00185B60 File Offset: 0x00183D60
			internal LinkLabel Owner
			{
				get
				{
					return this.owner;
				}
				set
				{
					this.owner = value;
				}
			}

			// Token: 0x170016B1 RID: 5809
			// (get) Token: 0x060068F0 RID: 26864 RVA: 0x00185B69 File Offset: 0x00183D69
			// (set) Token: 0x060068F1 RID: 26865 RVA: 0x00185B71 File Offset: 0x00183D71
			internal LinkState State
			{
				get
				{
					return this.state;
				}
				set
				{
					this.state = value;
				}
			}

			/// <summary>Gets or sets the name of the <see cref="T:System.Windows.Forms.LinkLabel.Link" />.</summary>
			/// <returns>A <see cref="T:System.String" /> representing the name of the <see cref="T:System.Windows.Forms.LinkLabel.Link" />. The default value is the empty string ("").</returns>
			// Token: 0x170016B2 RID: 5810
			// (get) Token: 0x060068F2 RID: 26866 RVA: 0x00185B7A File Offset: 0x00183D7A
			// (set) Token: 0x060068F3 RID: 26867 RVA: 0x00185B90 File Offset: 0x00183D90
			[DefaultValue("")]
			[SRCategory("CatAppearance")]
			[SRDescription("TreeNodeNodeNameDescr")]
			public string Name
			{
				get
				{
					if (this.name != null)
					{
						return this.name;
					}
					return "";
				}
				set
				{
					this.name = value;
				}
			}

			/// <summary>Gets or sets the starting location of the link within the text of the <see cref="T:System.Windows.Forms.LinkLabel" />.</summary>
			/// <returns>The location within the text of the <see cref="T:System.Windows.Forms.LinkLabel" /> control where the link starts.</returns>
			// Token: 0x170016B3 RID: 5811
			// (get) Token: 0x060068F4 RID: 26868 RVA: 0x00185B99 File Offset: 0x00183D99
			// (set) Token: 0x060068F5 RID: 26869 RVA: 0x00185BA4 File Offset: 0x00183DA4
			public int Start
			{
				get
				{
					return this.start;
				}
				set
				{
					if (this.start != value)
					{
						this.start = value;
						if (this.owner != null)
						{
							this.owner.links.Sort(LinkLabel.linkComparer);
							this.owner.InvalidateTextLayout();
							this.owner.Invalidate();
						}
					}
				}
			}

			/// <summary>Gets or sets the object that contains data about the <see cref="T:System.Windows.Forms.LinkLabel.Link" />.</summary>
			/// <returns>An <see cref="T:System.Object" /> that contains data about the control. The default is <see langword="null" />.</returns>
			// Token: 0x170016B4 RID: 5812
			// (get) Token: 0x060068F6 RID: 26870 RVA: 0x00185BF4 File Offset: 0x00183DF4
			// (set) Token: 0x060068F7 RID: 26871 RVA: 0x00185BFC File Offset: 0x00183DFC
			[SRCategory("CatData")]
			[Localizable(false)]
			[Bindable(true)]
			[SRDescription("ControlTagDescr")]
			[DefaultValue(null)]
			[TypeConverter(typeof(StringConverter))]
			public object Tag
			{
				get
				{
					return this.userData;
				}
				set
				{
					this.userData = value;
				}
			}

			/// <summary>Gets or sets a value indicating whether the user has visited the link.</summary>
			/// <returns>
			///   <see langword="true" /> if the link has been visited; otherwise, <see langword="false" />.</returns>
			// Token: 0x170016B5 RID: 5813
			// (get) Token: 0x060068F8 RID: 26872 RVA: 0x00185C05 File Offset: 0x00183E05
			// (set) Token: 0x060068F9 RID: 26873 RVA: 0x00185C14 File Offset: 0x00183E14
			[DefaultValue(false)]
			public bool Visited
			{
				get
				{
					return (this.State & LinkState.Visited) == LinkState.Visited;
				}
				set
				{
					bool visited = this.Visited;
					if (value)
					{
						this.State |= LinkState.Visited;
					}
					else
					{
						this.State &= (LinkState)(-5);
					}
					if (visited != this.Visited && this.owner != null)
					{
						this.owner.InvalidateLink(this);
					}
				}
			}

			// Token: 0x170016B6 RID: 5814
			// (get) Token: 0x060068FA RID: 26874 RVA: 0x00185C67 File Offset: 0x00183E67
			// (set) Token: 0x060068FB RID: 26875 RVA: 0x00185C6F File Offset: 0x00183E6F
			internal Region VisualRegion
			{
				get
				{
					return this.visualRegion;
				}
				set
				{
					this.visualRegion = value;
				}
			}

			// Token: 0x04003B12 RID: 15122
			private int start;

			// Token: 0x04003B13 RID: 15123
			private object linkData;

			// Token: 0x04003B14 RID: 15124
			private LinkState state;

			// Token: 0x04003B15 RID: 15125
			private bool enabled = true;

			// Token: 0x04003B16 RID: 15126
			private Region visualRegion;

			// Token: 0x04003B17 RID: 15127
			internal int length;

			// Token: 0x04003B18 RID: 15128
			private LinkLabel owner;

			// Token: 0x04003B19 RID: 15129
			private string name;

			// Token: 0x04003B1A RID: 15130
			private string description;

			// Token: 0x04003B1B RID: 15131
			private object userData;
		}

		// Token: 0x020006BB RID: 1723
		private class LinkComparer : IComparer
		{
			// Token: 0x060068FC RID: 26876 RVA: 0x00185C78 File Offset: 0x00183E78
			int IComparer.Compare(object link1, object link2)
			{
				int start = ((LinkLabel.Link)link1).Start;
				int start2 = ((LinkLabel.Link)link2).Start;
				return start - start2;
			}
		}

		// Token: 0x020006BC RID: 1724
		[ComVisible(true)]
		internal class LinkLabelAccessibleObject : Label.LabelAccessibleObject
		{
			// Token: 0x060068FE RID: 26878 RVA: 0x00185CA0 File Offset: 0x00183EA0
			public LinkLabelAccessibleObject(LinkLabel owner)
				: base(owner)
			{
			}

			// Token: 0x060068FF RID: 26879 RVA: 0x00185CA9 File Offset: 0x00183EA9
			internal override bool IsIAccessibleExSupported()
			{
				return AccessibilityImprovements.Level3 || base.IsIAccessibleExSupported();
			}

			// Token: 0x06006900 RID: 26880 RVA: 0x00185CBA File Offset: 0x00183EBA
			public override AccessibleObject GetChild(int index)
			{
				if (index >= 0 && index < ((LinkLabel)base.Owner).Links.Count)
				{
					return new LinkLabel.LinkAccessibleObject(((LinkLabel)base.Owner).Links[index]);
				}
				return null;
			}

			// Token: 0x06006901 RID: 26881 RVA: 0x00185CF5 File Offset: 0x00183EF5
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30010 && !base.Owner.Enabled)
				{
					return false;
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x06006902 RID: 26882 RVA: 0x00185D1C File Offset: 0x00183F1C
			public override AccessibleObject HitTest(int x, int y)
			{
				Point point = base.Owner.PointToClient(new Point(x, y));
				LinkLabel.Link link = ((LinkLabel)base.Owner).PointInLink(point.X, point.Y);
				if (link != null)
				{
					return new LinkLabel.LinkAccessibleObject(link);
				}
				if (this.Bounds.Contains(x, y))
				{
					return this;
				}
				return null;
			}

			// Token: 0x06006903 RID: 26883 RVA: 0x00185D7A File Offset: 0x00183F7A
			public override int GetChildCount()
			{
				return ((LinkLabel)base.Owner).Links.Count;
			}
		}

		// Token: 0x020006BD RID: 1725
		[ComVisible(true)]
		internal class LinkAccessibleObject : AccessibleObject
		{
			// Token: 0x06006904 RID: 26884 RVA: 0x00185D91 File Offset: 0x00183F91
			public LinkAccessibleObject(LinkLabel.Link link)
			{
				this.link = link;
			}

			// Token: 0x170016B7 RID: 5815
			// (get) Token: 0x06006905 RID: 26885 RVA: 0x00185DA0 File Offset: 0x00183FA0
			public override Rectangle Bounds
			{
				get
				{
					Region region = this.link.VisualRegion;
					Graphics graphics = null;
					IntSecurity.ObjectFromWin32Handle.Assert();
					try
					{
						graphics = Graphics.FromHwnd(this.link.Owner.Handle);
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
					if (region == null)
					{
						this.link.Owner.EnsureRun(graphics);
						region = this.link.VisualRegion;
						if (region == null)
						{
							graphics.Dispose();
							return Rectangle.Empty;
						}
					}
					Rectangle rectangle;
					try
					{
						rectangle = Rectangle.Ceiling(region.GetBounds(graphics));
					}
					finally
					{
						graphics.Dispose();
					}
					return this.link.Owner.RectangleToScreen(rectangle);
				}
			}

			// Token: 0x170016B8 RID: 5816
			// (get) Token: 0x06006906 RID: 26886 RVA: 0x00185E58 File Offset: 0x00184058
			public override string DefaultAction
			{
				get
				{
					return SR.GetString("AccessibleActionClick");
				}
			}

			// Token: 0x170016B9 RID: 5817
			// (get) Token: 0x06006907 RID: 26887 RVA: 0x00185E64 File Offset: 0x00184064
			public override string Description
			{
				get
				{
					return this.link.Description;
				}
			}

			// Token: 0x170016BA RID: 5818
			// (get) Token: 0x06006908 RID: 26888 RVA: 0x00185E74 File Offset: 0x00184074
			// (set) Token: 0x06006909 RID: 26889 RVA: 0x0016FA70 File Offset: 0x0016DC70
			public override string Name
			{
				get
				{
					string text = this.link.Owner.Text;
					string text2;
					if (AccessibilityImprovements.Level3)
					{
						text2 = text;
						if (this.link.Owner.UseMnemonic)
						{
							text2 = WindowsFormsUtils.TextWithoutMnemonics(text2);
						}
					}
					else
					{
						int num = LinkLabel.ConvertToCharIndex(this.link.Start, text);
						int num2 = LinkLabel.ConvertToCharIndex(this.link.Start + this.link.Length, text);
						text2 = text.Substring(num, num2 - num);
						if (AccessibilityImprovements.Level1 && this.link.Owner.UseMnemonic)
						{
							text2 = WindowsFormsUtils.TextWithoutMnemonics(text2);
						}
					}
					return text2;
				}
				set
				{
					base.Name = value;
				}
			}

			// Token: 0x170016BB RID: 5819
			// (get) Token: 0x0600690A RID: 26890 RVA: 0x00185F12 File Offset: 0x00184112
			public override AccessibleObject Parent
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return this.link.Owner.AccessibilityObject;
				}
			}

			// Token: 0x170016BC RID: 5820
			// (get) Token: 0x0600690B RID: 26891 RVA: 0x00177983 File Offset: 0x00175B83
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.Link;
				}
			}

			// Token: 0x170016BD RID: 5821
			// (get) Token: 0x0600690C RID: 26892 RVA: 0x00185F24 File Offset: 0x00184124
			public override AccessibleStates State
			{
				get
				{
					AccessibleStates accessibleStates = AccessibleStates.Focusable;
					if (this.link.Owner.FocusLink == this.link)
					{
						accessibleStates |= AccessibleStates.Focused;
					}
					return accessibleStates;
				}
			}

			// Token: 0x170016BE RID: 5822
			// (get) Token: 0x0600690D RID: 26893 RVA: 0x00185F54 File Offset: 0x00184154
			public override string Value
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					if (AccessibilityImprovements.Level1)
					{
						return string.Empty;
					}
					return this.Name;
				}
			}

			// Token: 0x0600690E RID: 26894 RVA: 0x00185F69 File Offset: 0x00184169
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void DoDefaultAction()
			{
				this.link.Owner.OnLinkClicked(new LinkLabelLinkClickedEventArgs(this.link));
			}

			// Token: 0x0600690F RID: 26895 RVA: 0x00185F86 File Offset: 0x00184186
			internal override bool IsIAccessibleExSupported()
			{
				return AccessibilityImprovements.Level3 || base.IsIAccessibleExSupported();
			}

			// Token: 0x06006910 RID: 26896 RVA: 0x00185F97 File Offset: 0x00184197
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30010 && !this.link.Owner.Enabled)
				{
					return false;
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x04003B1C RID: 15132
			private LinkLabel.Link link;
		}
	}
}
