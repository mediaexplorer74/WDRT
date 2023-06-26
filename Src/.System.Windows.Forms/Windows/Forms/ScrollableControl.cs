using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Defines a base class for controls that support auto-scrolling behavior.</summary>
	// Token: 0x02000354 RID: 852
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Designer("System.Windows.Forms.Design.ScrollableControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class ScrollableControl : Control, IArrangedElement, IComponent, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ScrollableControl" /> class.</summary>
		// Token: 0x060037AC RID: 14252 RVA: 0x000F8008 File Offset: 0x000F6208
		public ScrollableControl()
		{
			base.SetStyle(ControlStyles.ContainerControl, true);
			base.SetStyle(ControlStyles.AllPaintingInWmPaint, false);
			this.SetScrollState(1, false);
		}

		/// <summary>Gets or sets a value indicating whether the container enables the user to scroll to any controls placed outside of its visible boundaries.</summary>
		/// <returns>
		///   <see langword="true" /> if the container enables auto-scrolling; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x060037AD RID: 14253 RVA: 0x000F806E File Offset: 0x000F626E
		// (set) Token: 0x060037AE RID: 14254 RVA: 0x000F8077 File Offset: 0x000F6277
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[DefaultValue(false)]
		[SRDescription("FormAutoScrollDescr")]
		public virtual bool AutoScroll
		{
			get
			{
				return this.GetScrollState(1);
			}
			set
			{
				if (value)
				{
					this.UpdateFullDrag();
				}
				this.SetScrollState(1, value);
				LayoutTransaction.DoLayout(this, this, PropertyNames.AutoScroll);
			}
		}

		/// <summary>Gets or sets the size of the auto-scroll margin.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the height and width of the auto-scroll margin in pixels.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Drawing.Size.Height" /> or <see cref="P:System.Drawing.Size.Width" /> value assigned is less than 0.</exception>
		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x060037AF RID: 14255 RVA: 0x000F8096 File Offset: 0x000F6296
		// (set) Token: 0x060037B0 RID: 14256 RVA: 0x000F80A0 File Offset: 0x000F62A0
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[SRDescription("FormAutoScrollMarginDescr")]
		public Size AutoScrollMargin
		{
			get
			{
				return this.requestedScrollMargin;
			}
			set
			{
				if (value.Width < 0 || value.Height < 0)
				{
					throw new ArgumentOutOfRangeException("AutoScrollMargin", SR.GetString("InvalidArgument", new object[]
					{
						"AutoScrollMargin",
						value.ToString()
					}));
				}
				this.SetAutoScrollMargin(value.Width, value.Height);
			}
		}

		/// <summary>Gets or sets the location of the auto-scroll position.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> that represents the auto-scroll position in pixels.</returns>
		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x060037B1 RID: 14257 RVA: 0x000F8108 File Offset: 0x000F6308
		// (set) Token: 0x060037B2 RID: 14258 RVA: 0x000F812F File Offset: 0x000F632F
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("FormAutoScrollPositionDescr")]
		public Point AutoScrollPosition
		{
			get
			{
				Rectangle displayRectInternal = this.GetDisplayRectInternal();
				return new Point(displayRectInternal.X, displayRectInternal.Y);
			}
			set
			{
				if (base.Created)
				{
					this.SetDisplayRectLocation(-value.X, -value.Y);
					this.SyncScrollbars(true);
				}
				this.scrollPosition = value;
			}
		}

		/// <summary>Gets or sets the minimum size of the auto-scroll.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that determines the minimum size of the virtual area through which the user can scroll.</returns>
		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x060037B3 RID: 14259 RVA: 0x000F815D File Offset: 0x000F635D
		// (set) Token: 0x060037B4 RID: 14260 RVA: 0x000F8165 File Offset: 0x000F6365
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[SRDescription("FormAutoScrollMinSizeDescr")]
		public Size AutoScrollMinSize
		{
			get
			{
				return this.userAutoScrollMinSize;
			}
			set
			{
				if (value != this.userAutoScrollMinSize)
				{
					this.userAutoScrollMinSize = value;
					this.AutoScroll = true;
					base.PerformLayout();
				}
			}
		}

		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x060037B5 RID: 14261 RVA: 0x000F818C File Offset: 0x000F638C
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				if (this.HScroll || this.HorizontalScroll.Visible)
				{
					createParams.Style |= 1048576;
				}
				else
				{
					createParams.Style &= -1048577;
				}
				if (this.VScroll || this.VerticalScroll.Visible)
				{
					createParams.Style |= 2097152;
				}
				else
				{
					createParams.Style &= -2097153;
				}
				return createParams;
			}
		}

		/// <summary>Gets the rectangle that represents the virtual display area of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the display area of the control.</returns>
		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x060037B6 RID: 14262 RVA: 0x000F8218 File Offset: 0x000F6418
		public override Rectangle DisplayRectangle
		{
			get
			{
				Rectangle clientRectangle = base.ClientRectangle;
				if (!this.displayRect.IsEmpty)
				{
					clientRectangle.X = this.displayRect.X;
					clientRectangle.Y = this.displayRect.Y;
					if (this.HScroll)
					{
						clientRectangle.Width = this.displayRect.Width;
					}
					if (this.VScroll)
					{
						clientRectangle.Height = this.displayRect.Height;
					}
				}
				return LayoutUtils.DeflateRect(clientRectangle, base.Padding);
			}
		}

		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x060037B7 RID: 14263 RVA: 0x000F82A0 File Offset: 0x000F64A0
		Rectangle IArrangedElement.DisplayRectangle
		{
			get
			{
				Rectangle displayRectangle = this.DisplayRectangle;
				if (this.AutoScrollMinSize.Width != 0 && this.AutoScrollMinSize.Height != 0)
				{
					displayRectangle.Width = Math.Max(displayRectangle.Width, this.AutoScrollMinSize.Width);
					displayRectangle.Height = Math.Max(displayRectangle.Height, this.AutoScrollMinSize.Height);
				}
				return displayRectangle;
			}
		}

		/// <summary>Gets or sets a value indicating whether the horizontal scroll bar is visible.</summary>
		/// <returns>
		///   <see langword="true" /> if the horizontal scroll bar is visible; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x060037B8 RID: 14264 RVA: 0x000F8317 File Offset: 0x000F6517
		// (set) Token: 0x060037B9 RID: 14265 RVA: 0x000F8320 File Offset: 0x000F6520
		protected bool HScroll
		{
			get
			{
				return this.GetScrollState(2);
			}
			set
			{
				this.SetScrollState(2, value);
			}
		}

		/// <summary>Gets the characteristics associated with the horizontal scroll bar.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.HScrollProperties" /> that contains information about the horizontal scroll bar.</returns>
		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x060037BA RID: 14266 RVA: 0x000F832A File Offset: 0x000F652A
		[SRCategory("CatLayout")]
		[SRDescription("ScrollableControlHorizontalScrollDescr")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public HScrollProperties HorizontalScroll
		{
			get
			{
				if (this.horizontalScroll == null)
				{
					this.horizontalScroll = new HScrollProperties(this);
				}
				return this.horizontalScroll;
			}
		}

		/// <summary>Gets or sets a value indicating whether the vertical scroll bar is visible.</summary>
		/// <returns>
		///   <see langword="true" /> if the vertical scroll bar is visible; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x060037BB RID: 14267 RVA: 0x000F8346 File Offset: 0x000F6546
		// (set) Token: 0x060037BC RID: 14268 RVA: 0x000F834F File Offset: 0x000F654F
		protected bool VScroll
		{
			get
			{
				return this.GetScrollState(4);
			}
			set
			{
				this.SetScrollState(4, value);
			}
		}

		/// <summary>Gets the characteristics associated with the vertical scroll bar.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.VScrollProperties" /> that contains information about the vertical scroll bar.</returns>
		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x060037BD RID: 14269 RVA: 0x000F8359 File Offset: 0x000F6559
		[SRCategory("CatLayout")]
		[SRDescription("ScrollableControlVerticalScrollDescr")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public VScrollProperties VerticalScroll
		{
			get
			{
				if (this.verticalScroll == null)
				{
					this.verticalScroll = new VScrollProperties(this);
				}
				return this.verticalScroll;
			}
		}

		/// <summary>Gets the dock padding settings for all edges of the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ScrollableControl.DockPaddingEdges" /> that represents the padding for all the edges of a docked control.</returns>
		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x060037BE RID: 14270 RVA: 0x000F8375 File Offset: 0x000F6575
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ScrollableControl.DockPaddingEdges DockPadding
		{
			get
			{
				if (this.dockPadding == null)
				{
					this.dockPadding = new ScrollableControl.DockPaddingEdges(this);
				}
				return this.dockPadding;
			}
		}

		/// <summary>Adjusts the scroll bars on the container based on the current control positions and the control currently selected.</summary>
		/// <param name="displayScrollbars">
		///   <see langword="true" /> to show the scroll bars; otherwise, <see langword="false" />.</param>
		// Token: 0x060037BF RID: 14271 RVA: 0x000F8394 File Offset: 0x000F6594
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void AdjustFormScrollbars(bool displayScrollbars)
		{
			bool flag = false;
			Rectangle displayRectInternal = this.GetDisplayRectInternal();
			if (!displayScrollbars && (this.HScroll || this.VScroll))
			{
				flag = this.SetVisibleScrollbars(false, false);
			}
			if (!displayScrollbars)
			{
				Rectangle clientRectangle = base.ClientRectangle;
				displayRectInternal.Width = clientRectangle.Width;
				displayRectInternal.Height = clientRectangle.Height;
			}
			else
			{
				flag |= this.ApplyScrollbarChanges(displayRectInternal);
			}
			if (flag)
			{
				LayoutTransaction.DoLayout(this, this, PropertyNames.DisplayRectangle);
			}
		}

		// Token: 0x060037C0 RID: 14272 RVA: 0x000F8408 File Offset: 0x000F6608
		private bool ApplyScrollbarChanges(Rectangle display)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			Rectangle clientRectangle = base.ClientRectangle;
			Rectangle rectangle = clientRectangle;
			Rectangle rectangle2 = rectangle;
			if (this.HScroll)
			{
				rectangle.Height += SystemInformation.HorizontalScrollBarHeight;
			}
			else
			{
				rectangle2.Height -= SystemInformation.HorizontalScrollBarHeight;
			}
			if (this.VScroll)
			{
				rectangle.Width += SystemInformation.VerticalScrollBarWidth;
			}
			else
			{
				rectangle2.Width -= SystemInformation.VerticalScrollBarWidth;
			}
			int num = rectangle2.Width;
			int num2 = rectangle2.Height;
			if (base.Controls.Count != 0)
			{
				this.scrollMargin = this.requestedScrollMargin;
				if (this.dockPadding != null)
				{
					this.scrollMargin.Height = this.scrollMargin.Height + base.Padding.Bottom;
					this.scrollMargin.Width = this.scrollMargin.Width + base.Padding.Right;
				}
				for (int i = 0; i < base.Controls.Count; i++)
				{
					Control control = base.Controls[i];
					if (control != null && control.GetState(2))
					{
						DockStyle dock = control.Dock;
						if (dock != DockStyle.Bottom)
						{
							if (dock == DockStyle.Right)
							{
								this.scrollMargin.Width = this.scrollMargin.Width + control.Size.Width;
							}
						}
						else
						{
							this.scrollMargin.Height = this.scrollMargin.Height + control.Size.Height;
						}
					}
				}
			}
			if (!this.userAutoScrollMinSize.IsEmpty)
			{
				num = this.userAutoScrollMinSize.Width + this.scrollMargin.Width;
				num2 = this.userAutoScrollMinSize.Height + this.scrollMargin.Height;
				flag2 = true;
				flag3 = true;
			}
			bool flag4 = this.LayoutEngine == DefaultLayout.Instance;
			if (!flag4 && CommonProperties.HasLayoutBounds(this))
			{
				Size layoutBounds = CommonProperties.GetLayoutBounds(this);
				if (layoutBounds.Width > num)
				{
					flag2 = true;
					num = layoutBounds.Width;
				}
				if (layoutBounds.Height > num2)
				{
					flag3 = true;
					num2 = layoutBounds.Height;
				}
			}
			else if (base.Controls.Count != 0)
			{
				for (int j = 0; j < base.Controls.Count; j++)
				{
					bool flag5 = true;
					bool flag6 = true;
					Control control2 = base.Controls[j];
					if (control2 != null && control2.GetState(2))
					{
						if (flag4)
						{
							Control control3 = control2;
							switch (control3.Dock)
							{
							case DockStyle.Top:
								flag5 = false;
								break;
							case DockStyle.Bottom:
							case DockStyle.Right:
							case DockStyle.Fill:
								flag5 = false;
								flag6 = false;
								break;
							case DockStyle.Left:
								flag6 = false;
								break;
							default:
							{
								AnchorStyles anchor = control3.Anchor;
								if ((anchor & AnchorStyles.Right) == AnchorStyles.Right)
								{
									flag5 = false;
								}
								if ((anchor & AnchorStyles.Left) != AnchorStyles.Left)
								{
									flag5 = false;
								}
								if ((anchor & AnchorStyles.Bottom) == AnchorStyles.Bottom)
								{
									flag6 = false;
								}
								if ((anchor & AnchorStyles.Top) != AnchorStyles.Top)
								{
									flag6 = false;
								}
								break;
							}
							}
						}
						if (flag5 || flag6)
						{
							Rectangle bounds = control2.Bounds;
							int num3 = -display.X + bounds.X + bounds.Width + this.scrollMargin.Width;
							int num4 = -display.Y + bounds.Y + bounds.Height + this.scrollMargin.Height;
							if (!flag4)
							{
								num3 += control2.Margin.Right;
								num4 += control2.Margin.Bottom;
							}
							if (num3 > num && flag5)
							{
								flag2 = true;
								num = num3;
							}
							if (num4 > num2 && flag6)
							{
								flag3 = true;
								num2 = num4;
							}
						}
					}
				}
			}
			if (num <= rectangle.Width)
			{
				flag2 = false;
			}
			if (num2 <= rectangle.Height)
			{
				flag3 = false;
			}
			Rectangle rectangle3 = rectangle;
			if (flag2)
			{
				rectangle3.Height -= SystemInformation.HorizontalScrollBarHeight;
			}
			if (flag3)
			{
				rectangle3.Width -= SystemInformation.VerticalScrollBarWidth;
			}
			if (flag2 && num2 > rectangle3.Height)
			{
				flag3 = true;
			}
			if (flag3 && num > rectangle3.Width)
			{
				flag2 = true;
			}
			if (!flag2)
			{
				num = rectangle3.Width;
			}
			if (!flag3)
			{
				num2 = rectangle3.Height;
			}
			flag = this.SetVisibleScrollbars(flag2, flag3) || flag;
			if (this.HScroll || this.VScroll)
			{
				flag = this.SetDisplayRectangleSize(num, num2) || flag;
			}
			else
			{
				this.SetDisplayRectangleSize(num, num2);
			}
			this.SyncScrollbars(true);
			return flag;
		}

		// Token: 0x060037C1 RID: 14273 RVA: 0x000F886C File Offset: 0x000F6A6C
		private Rectangle GetDisplayRectInternal()
		{
			if (this.displayRect.IsEmpty)
			{
				this.displayRect = base.ClientRectangle;
			}
			if (!this.AutoScroll && this.HorizontalScroll.visible)
			{
				this.displayRect = new Rectangle(this.displayRect.X, this.displayRect.Y, this.HorizontalScroll.Maximum, this.displayRect.Height);
			}
			if (!this.AutoScroll && this.VerticalScroll.visible)
			{
				this.displayRect = new Rectangle(this.displayRect.X, this.displayRect.Y, this.displayRect.Width, this.VerticalScroll.Maximum);
			}
			return this.displayRect;
		}

		/// <summary>Determines whether the specified flag has been set.</summary>
		/// <param name="bit">The flag to check.</param>
		/// <returns>
		///   <see langword="true" /> if the specified flag has been set; otherwise, <see langword="false" />.</returns>
		// Token: 0x060037C2 RID: 14274 RVA: 0x000F8930 File Offset: 0x000F6B30
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected bool GetScrollState(int bit)
		{
			return (bit & this.scrollState) == bit;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
		/// <param name="levent">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
		// Token: 0x060037C3 RID: 14275 RVA: 0x000F893D File Offset: 0x000F6B3D
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnLayout(LayoutEventArgs levent)
		{
			if (levent.AffectedControl != null && this.AutoScroll)
			{
				base.OnLayout(levent);
			}
			this.AdjustFormScrollbars(this.AutoScroll);
			base.OnLayout(levent);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseWheel" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x060037C4 RID: 14276 RVA: 0x000F896C File Offset: 0x000F6B6C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			if (this.VScroll)
			{
				Rectangle clientRectangle = base.ClientRectangle;
				int num = -this.displayRect.Y;
				int num2 = -(clientRectangle.Height - this.displayRect.Height);
				num = Math.Max(num - e.Delta, 0);
				num = Math.Min(num, num2);
				this.SetDisplayRectLocation(this.displayRect.X, -num);
				this.SyncScrollbars(this.AutoScroll);
				if (e is HandledMouseEventArgs)
				{
					((HandledMouseEventArgs)e).Handled = true;
				}
			}
			else if (this.HScroll)
			{
				Rectangle clientRectangle2 = base.ClientRectangle;
				int num3 = -this.displayRect.X;
				int num4 = -(clientRectangle2.Width - this.displayRect.Width);
				num3 = Math.Max(num3 - e.Delta, 0);
				num3 = Math.Min(num3, num4);
				this.SetDisplayRectLocation(-num3, this.displayRect.Y);
				this.SyncScrollbars(this.AutoScroll);
				if (e is HandledMouseEventArgs)
				{
					((HandledMouseEventArgs)e).Handled = true;
				}
			}
			base.OnMouseWheel(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.RightToLeftChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060037C5 RID: 14277 RVA: 0x000F8A86 File Offset: 0x000F6C86
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
			this.resetRTLHScrollValue = true;
			LayoutTransaction.DoLayout(this, this, PropertyNames.RightToLeft);
		}

		/// <summary>Paints the background of the control.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		// Token: 0x060037C6 RID: 14278 RVA: 0x000F8AA4 File Offset: 0x000F6CA4
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			if ((this.HScroll || this.VScroll) && this.BackgroundImage != null && (this.BackgroundImageLayout == ImageLayout.Zoom || this.BackgroundImageLayout == ImageLayout.Stretch || this.BackgroundImageLayout == ImageLayout.Center))
			{
				if (ControlPaint.IsImageTransparent(this.BackgroundImage))
				{
					base.PaintTransparentBackground(e, this.displayRect);
				}
				ControlPaint.DrawBackgroundImage(e.Graphics, this.BackgroundImage, this.BackColor, this.BackgroundImageLayout, this.displayRect, this.displayRect, this.displayRect.Location);
				return;
			}
			base.OnPaintBackground(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.PaddingChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060037C7 RID: 14279 RVA: 0x000F8B3C File Offset: 0x000F6D3C
		protected override void OnPaddingChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventPaddingChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.VisibleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
		// Token: 0x060037C8 RID: 14280 RVA: 0x000F8B6A File Offset: 0x000F6D6A
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnVisibleChanged(EventArgs e)
		{
			if (base.Visible)
			{
				LayoutTransaction.DoLayout(this, this, PropertyNames.Visible);
			}
			base.OnVisibleChanged(e);
		}

		// Token: 0x060037C9 RID: 14281 RVA: 0x000F8B87 File Offset: 0x000F6D87
		internal void ScaleDockPadding(float dx, float dy)
		{
			if (this.dockPadding != null)
			{
				this.dockPadding.Scale(dx, dy);
			}
		}

		/// <summary>This method is not relevant for this class.</summary>
		/// <param name="dx">The horizontal scaling factor.</param>
		/// <param name="dy">The vertical scaling factor.</param>
		// Token: 0x060037CA RID: 14282 RVA: 0x000F8B9E File Offset: 0x000F6D9E
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void ScaleCore(float dx, float dy)
		{
			this.ScaleDockPadding(dx, dy);
			base.ScaleCore(dx, dy);
		}

		/// <summary>Scales a control's location, size, padding and margin.</summary>
		/// <param name="factor">The factor by which the height and width of the control will be scaled.</param>
		/// <param name="specified">A <see cref="T:System.Windows.Forms.BoundsSpecified" /> value that specifies the bounds of the control to use when defining its size and position.</param>
		// Token: 0x060037CB RID: 14283 RVA: 0x000F8BB0 File Offset: 0x000F6DB0
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			this.ScaleDockPadding(factor.Width, factor.Height);
			base.ScaleControl(factor, specified);
		}

		// Token: 0x060037CC RID: 14284 RVA: 0x000F8BD0 File Offset: 0x000F6DD0
		internal void SetDisplayFromScrollProps(int x, int y)
		{
			Rectangle displayRectInternal = this.GetDisplayRectInternal();
			this.ApplyScrollbarChanges(displayRectInternal);
			this.SetDisplayRectLocation(x, y);
		}

		/// <summary>Positions the display window to the specified value.</summary>
		/// <param name="x">The horizontal offset at which to position the <see cref="T:System.Windows.Forms.ScrollableControl" />.</param>
		/// <param name="y">The vertical offset at which to position the <see cref="T:System.Windows.Forms.ScrollableControl" />.</param>
		// Token: 0x060037CD RID: 14285 RVA: 0x000F8BF4 File Offset: 0x000F6DF4
		protected void SetDisplayRectLocation(int x, int y)
		{
			int num = 0;
			int num2 = 0;
			Rectangle clientRectangle = base.ClientRectangle;
			Rectangle rectangle = this.displayRect;
			int num3 = Math.Min(clientRectangle.Width - rectangle.Width, 0);
			int num4 = Math.Min(clientRectangle.Height - rectangle.Height, 0);
			if (x > 0)
			{
				x = 0;
			}
			if (y > 0)
			{
				y = 0;
			}
			if (x < num3)
			{
				x = num3;
			}
			if (y < num4)
			{
				y = num4;
			}
			if (rectangle.X != x)
			{
				num = x - rectangle.X;
			}
			if (rectangle.Y != y)
			{
				num2 = y - rectangle.Y;
			}
			this.displayRect.X = x;
			this.displayRect.Y = y;
			if (num != 0 || (num2 != 0 && base.IsHandleCreated))
			{
				Rectangle clientRectangle2 = base.ClientRectangle;
				NativeMethods.RECT rect = NativeMethods.RECT.FromXYWH(clientRectangle2.X, clientRectangle2.Y, clientRectangle2.Width, clientRectangle2.Height);
				NativeMethods.RECT rect2 = NativeMethods.RECT.FromXYWH(clientRectangle2.X, clientRectangle2.Y, clientRectangle2.Width, clientRectangle2.Height);
				SafeNativeMethods.ScrollWindowEx(new HandleRef(this, base.Handle), num, num2, null, ref rect, NativeMethods.NullHandleRef, ref rect2, 7);
			}
			for (int i = 0; i < base.Controls.Count; i++)
			{
				Control control = base.Controls[i];
				if (control != null && control.IsHandleCreated)
				{
					control.UpdateBounds();
				}
			}
		}

		/// <summary>Scrolls the specified child control into view on an auto-scroll enabled control.</summary>
		/// <param name="activeControl">The child control to scroll into view.</param>
		// Token: 0x060037CE RID: 14286 RVA: 0x000F8D5C File Offset: 0x000F6F5C
		public void ScrollControlIntoView(Control activeControl)
		{
			Rectangle clientRectangle = base.ClientRectangle;
			if (base.IsDescendant(activeControl) && this.AutoScroll && (this.HScroll || this.VScroll) && activeControl != null && clientRectangle.Width > 0 && clientRectangle.Height > 0)
			{
				Point point = this.ScrollToControl(activeControl);
				this.SetScrollState(8, false);
				this.SetDisplayRectLocation(point.X, point.Y);
				this.SyncScrollbars(true);
			}
		}

		/// <summary>Calculates the scroll offset to the specified child control.</summary>
		/// <param name="activeControl">The child control to scroll into view.</param>
		/// <returns>The upper-left hand <see cref="T:System.Drawing.Point" /> of the display area relative to the client area required to scroll the control into view.</returns>
		// Token: 0x060037CF RID: 14287 RVA: 0x000F8DD4 File Offset: 0x000F6FD4
		protected virtual Point ScrollToControl(Control activeControl)
		{
			Rectangle clientRectangle = base.ClientRectangle;
			int num = this.displayRect.X;
			int num2 = this.displayRect.Y;
			int width = this.scrollMargin.Width;
			int height = this.scrollMargin.Height;
			Rectangle rectangle = activeControl.Bounds;
			if (activeControl.ParentInternal != this)
			{
				rectangle = base.RectangleToClient(activeControl.ParentInternal.RectangleToScreen(rectangle));
			}
			if (rectangle.X < width)
			{
				num = this.displayRect.X + width - rectangle.X;
			}
			else if (rectangle.X + rectangle.Width + width > clientRectangle.Width)
			{
				num = clientRectangle.Width - (rectangle.X + rectangle.Width + width - this.displayRect.X);
				if (rectangle.X + num - this.displayRect.X < width)
				{
					num = this.displayRect.X + width - rectangle.X;
				}
			}
			if (rectangle.Y < height)
			{
				num2 = this.displayRect.Y + height - rectangle.Y;
			}
			else if (rectangle.Y + rectangle.Height + height > clientRectangle.Height)
			{
				num2 = clientRectangle.Height - (rectangle.Y + rectangle.Height + height - this.displayRect.Y);
				if (rectangle.Y + num2 - this.displayRect.Y < height)
				{
					num2 = this.displayRect.Y + height - rectangle.Y;
				}
			}
			num += activeControl.AutoScrollOffset.X;
			num2 += activeControl.AutoScrollOffset.Y;
			return new Point(num, num2);
		}

		// Token: 0x060037D0 RID: 14288 RVA: 0x000F8F90 File Offset: 0x000F7190
		private int ScrollThumbPosition(int fnBar)
		{
			NativeMethods.SCROLLINFO scrollinfo = new NativeMethods.SCROLLINFO();
			scrollinfo.fMask = 16;
			SafeNativeMethods.GetScrollInfo(new HandleRef(this, base.Handle), fnBar, scrollinfo);
			return scrollinfo.nTrackPos;
		}

		/// <summary>Occurs when the user or code scrolls through the client area.</summary>
		// Token: 0x14000298 RID: 664
		// (add) Token: 0x060037D1 RID: 14289 RVA: 0x000F8FC5 File Offset: 0x000F71C5
		// (remove) Token: 0x060037D2 RID: 14290 RVA: 0x000F8FD8 File Offset: 0x000F71D8
		[SRCategory("CatAction")]
		[SRDescription("ScrollBarOnScrollDescr")]
		public event ScrollEventHandler Scroll
		{
			add
			{
				base.Events.AddHandler(ScrollableControl.EVENT_SCROLL, value);
			}
			remove
			{
				base.Events.RemoveHandler(ScrollableControl.EVENT_SCROLL, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ScrollableControl.Scroll" /> event.</summary>
		/// <param name="se">A <see cref="T:System.Windows.Forms.ScrollEventArgs" /> that contains the event data.</param>
		// Token: 0x060037D3 RID: 14291 RVA: 0x000F8FEC File Offset: 0x000F71EC
		protected virtual void OnScroll(ScrollEventArgs se)
		{
			ScrollEventHandler scrollEventHandler = (ScrollEventHandler)base.Events[ScrollableControl.EVENT_SCROLL];
			if (scrollEventHandler != null)
			{
				scrollEventHandler(this, se);
			}
		}

		// Token: 0x060037D4 RID: 14292 RVA: 0x000F901A File Offset: 0x000F721A
		private void ResetAutoScrollMargin()
		{
			this.AutoScrollMargin = Size.Empty;
		}

		// Token: 0x060037D5 RID: 14293 RVA: 0x000F9027 File Offset: 0x000F7227
		private void ResetAutoScrollMinSize()
		{
			this.AutoScrollMinSize = Size.Empty;
		}

		// Token: 0x060037D6 RID: 14294 RVA: 0x000F9034 File Offset: 0x000F7234
		private void ResetScrollProperties(ScrollProperties scrollProperties)
		{
			scrollProperties.visible = false;
			scrollProperties.value = 0;
		}

		/// <summary>Sets the size of the auto-scroll margins.</summary>
		/// <param name="x">The <see cref="P:System.Drawing.Size.Width" /> value.</param>
		/// <param name="y">The <see cref="P:System.Drawing.Size.Height" /> value.</param>
		// Token: 0x060037D7 RID: 14295 RVA: 0x000F9044 File Offset: 0x000F7244
		public void SetAutoScrollMargin(int x, int y)
		{
			if (x < 0)
			{
				x = 0;
			}
			if (y < 0)
			{
				y = 0;
			}
			if (x != this.requestedScrollMargin.Width || y != this.requestedScrollMargin.Height)
			{
				this.requestedScrollMargin = new Size(x, y);
				if (this.AutoScroll)
				{
					base.PerformLayout();
				}
			}
		}

		// Token: 0x060037D8 RID: 14296 RVA: 0x000F9098 File Offset: 0x000F7298
		private bool SetVisibleScrollbars(bool horiz, bool vert)
		{
			bool flag = false;
			if ((!horiz && this.HScroll) || (horiz && !this.HScroll) || (!vert && this.VScroll) || (vert && !this.VScroll))
			{
				flag = true;
			}
			if (horiz && !this.HScroll && this.RightToLeft == RightToLeft.Yes)
			{
				this.resetRTLHScrollValue = true;
			}
			if (flag)
			{
				int num = this.displayRect.X;
				int num2 = this.displayRect.Y;
				if (!horiz)
				{
					num = 0;
				}
				if (!vert)
				{
					num2 = 0;
				}
				this.SetDisplayRectLocation(num, num2);
				this.SetScrollState(8, false);
				this.HScroll = horiz;
				this.VScroll = vert;
				if (horiz)
				{
					this.HorizontalScroll.visible = true;
				}
				else
				{
					this.ResetScrollProperties(this.HorizontalScroll);
				}
				if (vert)
				{
					this.VerticalScroll.visible = true;
				}
				else
				{
					this.ResetScrollProperties(this.VerticalScroll);
				}
				base.UpdateStyles();
			}
			return flag;
		}

		// Token: 0x060037D9 RID: 14297 RVA: 0x000F9178 File Offset: 0x000F7378
		private bool SetDisplayRectangleSize(int width, int height)
		{
			bool flag = false;
			if (this.displayRect.Width != width || this.displayRect.Height != height)
			{
				this.displayRect.Width = width;
				this.displayRect.Height = height;
				flag = true;
			}
			int num = base.ClientRectangle.Width - width;
			int num2 = base.ClientRectangle.Height - height;
			if (num > 0)
			{
				num = 0;
			}
			if (num2 > 0)
			{
				num2 = 0;
			}
			int num3 = this.displayRect.X;
			int num4 = this.displayRect.Y;
			if (!this.HScroll)
			{
				num3 = 0;
			}
			if (!this.VScroll)
			{
				num4 = 0;
			}
			if (num3 < num)
			{
				num3 = num;
			}
			if (num4 < num2)
			{
				num4 = num2;
			}
			this.SetDisplayRectLocation(num3, num4);
			return flag;
		}

		/// <summary>Sets the specified scroll state flag.</summary>
		/// <param name="bit">The scroll state flag to set.</param>
		/// <param name="value">The value to set the flag.</param>
		// Token: 0x060037DA RID: 14298 RVA: 0x000F9233 File Offset: 0x000F7433
		protected void SetScrollState(int bit, bool value)
		{
			if (value)
			{
				this.scrollState |= bit;
				return;
			}
			this.scrollState &= ~bit;
		}

		// Token: 0x060037DB RID: 14299 RVA: 0x000F9258 File Offset: 0x000F7458
		private bool ShouldSerializeAutoScrollPosition()
		{
			if (this.AutoScroll)
			{
				Point autoScrollPosition = this.AutoScrollPosition;
				if (autoScrollPosition.X != 0 || autoScrollPosition.Y != 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060037DC RID: 14300 RVA: 0x000F928C File Offset: 0x000F748C
		private bool ShouldSerializeAutoScrollMargin()
		{
			return !this.AutoScrollMargin.Equals(new Size(0, 0));
		}

		// Token: 0x060037DD RID: 14301 RVA: 0x000F92BC File Offset: 0x000F74BC
		private bool ShouldSerializeAutoScrollMinSize()
		{
			return !this.AutoScrollMinSize.Equals(new Size(0, 0));
		}

		// Token: 0x060037DE RID: 14302 RVA: 0x000F92EC File Offset: 0x000F74EC
		private void SyncScrollbars(bool autoScroll)
		{
			Rectangle rectangle = this.displayRect;
			if (autoScroll)
			{
				if (!base.IsHandleCreated)
				{
					return;
				}
				if (this.HScroll)
				{
					if (!this.HorizontalScroll.maximumSetExternally)
					{
						this.HorizontalScroll.maximum = rectangle.Width - 1;
					}
					if (!this.HorizontalScroll.largeChangeSetExternally)
					{
						this.HorizontalScroll.largeChange = base.ClientRectangle.Width;
					}
					if (!this.HorizontalScroll.smallChangeSetExternally)
					{
						this.HorizontalScroll.smallChange = 5;
					}
					if (this.resetRTLHScrollValue && !base.IsMirrored)
					{
						this.resetRTLHScrollValue = false;
						base.BeginInvoke(new EventHandler(this.OnSetScrollPosition));
					}
					else if (-rectangle.X >= this.HorizontalScroll.minimum && -rectangle.X < this.HorizontalScroll.maximum)
					{
						this.HorizontalScroll.value = -rectangle.X;
					}
					this.HorizontalScroll.UpdateScrollInfo();
				}
				if (this.VScroll)
				{
					if (!this.VerticalScroll.maximumSetExternally)
					{
						this.VerticalScroll.maximum = rectangle.Height - 1;
					}
					if (!this.VerticalScroll.largeChangeSetExternally)
					{
						this.VerticalScroll.largeChange = base.ClientRectangle.Height;
					}
					if (!this.VerticalScroll.smallChangeSetExternally)
					{
						this.VerticalScroll.smallChange = 5;
					}
					if (-rectangle.Y >= this.VerticalScroll.minimum && -rectangle.Y < this.VerticalScroll.maximum)
					{
						this.VerticalScroll.value = -rectangle.Y;
					}
					this.VerticalScroll.UpdateScrollInfo();
					return;
				}
			}
			else
			{
				if (this.HorizontalScroll.Visible)
				{
					this.HorizontalScroll.Value = -rectangle.X;
				}
				else
				{
					this.ResetScrollProperties(this.HorizontalScroll);
				}
				if (this.VerticalScroll.Visible)
				{
					this.VerticalScroll.Value = -rectangle.Y;
					return;
				}
				this.ResetScrollProperties(this.VerticalScroll);
			}
		}

		// Token: 0x060037DF RID: 14303 RVA: 0x000F94FD File Offset: 0x000F76FD
		private void OnSetScrollPosition(object sender, EventArgs e)
		{
			if (!base.IsMirrored)
			{
				base.SendMessage(276, NativeMethods.Util.MAKELPARAM((this.RightToLeft == RightToLeft.Yes) ? 7 : 6, 0), 0);
			}
		}

		// Token: 0x060037E0 RID: 14304 RVA: 0x000F9527 File Offset: 0x000F7727
		private void UpdateFullDrag()
		{
			this.SetScrollState(16, SystemInformation.DragFullWindows);
		}

		// Token: 0x060037E1 RID: 14305 RVA: 0x000F9538 File Offset: 0x000F7738
		private void WmVScroll(ref Message m)
		{
			if (m.LParam != IntPtr.Zero)
			{
				base.WndProc(ref m);
				return;
			}
			Rectangle clientRectangle = base.ClientRectangle;
			bool flag = NativeMethods.Util.LOWORD(m.WParam) != 5;
			int num = -this.displayRect.Y;
			int num2 = num;
			int num3 = -(clientRectangle.Height - this.displayRect.Height);
			if (!this.AutoScroll)
			{
				num3 = this.VerticalScroll.Maximum;
			}
			switch (NativeMethods.Util.LOWORD(m.WParam))
			{
			case 0:
				if (num > 0)
				{
					num -= this.VerticalScroll.SmallChange;
				}
				else
				{
					num = 0;
				}
				break;
			case 1:
				if (num < num3 - this.VerticalScroll.SmallChange)
				{
					num += this.VerticalScroll.SmallChange;
				}
				else
				{
					num = num3;
				}
				break;
			case 2:
				if (num > this.VerticalScroll.LargeChange)
				{
					num -= this.VerticalScroll.LargeChange;
				}
				else
				{
					num = 0;
				}
				break;
			case 3:
				if (num < num3 - this.VerticalScroll.LargeChange)
				{
					num += this.VerticalScroll.LargeChange;
				}
				else
				{
					num = num3;
				}
				break;
			case 4:
			case 5:
				num = this.ScrollThumbPosition(1);
				break;
			case 6:
				num = 0;
				break;
			case 7:
				num = num3;
				break;
			}
			if (this.GetScrollState(16) || flag)
			{
				this.SetScrollState(8, true);
				this.SetDisplayRectLocation(this.displayRect.X, -num);
				this.SyncScrollbars(this.AutoScroll);
			}
			this.WmOnScroll(ref m, num2, num, ScrollOrientation.VerticalScroll);
		}

		// Token: 0x060037E2 RID: 14306 RVA: 0x000F96C4 File Offset: 0x000F78C4
		private void WmHScroll(ref Message m)
		{
			if (m.LParam != IntPtr.Zero)
			{
				base.WndProc(ref m);
				return;
			}
			Rectangle clientRectangle = base.ClientRectangle;
			int num = -this.displayRect.X;
			int num2 = num;
			int num3 = -(clientRectangle.Width - this.displayRect.Width);
			if (!this.AutoScroll)
			{
				num3 = this.HorizontalScroll.Maximum;
			}
			switch (NativeMethods.Util.LOWORD(m.WParam))
			{
			case 0:
				if (num > this.HorizontalScroll.SmallChange)
				{
					num -= this.HorizontalScroll.SmallChange;
				}
				else
				{
					num = 0;
				}
				break;
			case 1:
				if (num < num3 - this.HorizontalScroll.SmallChange)
				{
					num += this.HorizontalScroll.SmallChange;
				}
				else
				{
					num = num3;
				}
				break;
			case 2:
				if (num > this.HorizontalScroll.LargeChange)
				{
					num -= this.HorizontalScroll.LargeChange;
				}
				else
				{
					num = 0;
				}
				break;
			case 3:
				if (num < num3 - this.HorizontalScroll.LargeChange)
				{
					num += this.HorizontalScroll.LargeChange;
				}
				else
				{
					num = num3;
				}
				break;
			case 4:
			case 5:
				num = this.ScrollThumbPosition(0);
				break;
			case 6:
				num = 0;
				break;
			case 7:
				num = num3;
				break;
			}
			if (this.GetScrollState(16) || NativeMethods.Util.LOWORD(m.WParam) != 5)
			{
				this.SetScrollState(8, true);
				this.SetDisplayRectLocation(-num, this.displayRect.Y);
				this.SyncScrollbars(this.AutoScroll);
			}
			this.WmOnScroll(ref m, num2, num, ScrollOrientation.HorizontalScroll);
		}

		// Token: 0x060037E3 RID: 14307 RVA: 0x000F984C File Offset: 0x000F7A4C
		private void WmOnScroll(ref Message m, int oldValue, int value, ScrollOrientation scrollOrientation)
		{
			ScrollEventType scrollEventType = (ScrollEventType)NativeMethods.Util.LOWORD(m.WParam);
			if (scrollEventType != ScrollEventType.EndScroll)
			{
				ScrollEventArgs scrollEventArgs = new ScrollEventArgs(scrollEventType, oldValue, value, scrollOrientation);
				this.OnScroll(scrollEventArgs);
			}
		}

		// Token: 0x060037E4 RID: 14308 RVA: 0x000F987B File Offset: 0x000F7A7B
		private void WmSettingChange(ref Message m)
		{
			base.WndProc(ref m);
			this.UpdateFullDrag();
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x060037E5 RID: 14309 RVA: 0x000F988C File Offset: 0x000F7A8C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg == 26)
			{
				this.WmSettingChange(ref m);
				return;
			}
			if (msg == 276)
			{
				this.WmHScroll(ref m);
				return;
			}
			if (msg == 277)
			{
				this.WmVScroll(ref m);
				return;
			}
			base.WndProc(ref m);
		}

		// Token: 0x04002167 RID: 8551
		internal static readonly TraceSwitch AutoScrolling;

		/// <summary>Determines the value of the <see cref="P:System.Windows.Forms.ScrollableControl.AutoScroll" /> property.</summary>
		// Token: 0x04002168 RID: 8552
		protected const int ScrollStateAutoScrolling = 1;

		/// <summary>Determines whether the value of the <see cref="P:System.Windows.Forms.ScrollableControl.HScroll" /> property is set to <see langword="true" />.</summary>
		// Token: 0x04002169 RID: 8553
		protected const int ScrollStateHScrollVisible = 2;

		/// <summary>Determines whether the value of the <see cref="P:System.Windows.Forms.ScrollableControl.VScroll" /> property is set to <see langword="true" />.</summary>
		// Token: 0x0400216A RID: 8554
		protected const int ScrollStateVScrollVisible = 4;

		/// <summary>Determines whether the user had scrolled through the <see cref="T:System.Windows.Forms.ScrollableControl" /> control.</summary>
		// Token: 0x0400216B RID: 8555
		protected const int ScrollStateUserHasScrolled = 8;

		/// <summary>Determines whether the user has enabled full window drag.</summary>
		// Token: 0x0400216C RID: 8556
		protected const int ScrollStateFullDrag = 16;

		// Token: 0x0400216D RID: 8557
		private Size userAutoScrollMinSize = Size.Empty;

		// Token: 0x0400216E RID: 8558
		private Rectangle displayRect = Rectangle.Empty;

		// Token: 0x0400216F RID: 8559
		private Size scrollMargin = Size.Empty;

		// Token: 0x04002170 RID: 8560
		private Size requestedScrollMargin = Size.Empty;

		// Token: 0x04002171 RID: 8561
		internal Point scrollPosition = Point.Empty;

		// Token: 0x04002172 RID: 8562
		private ScrollableControl.DockPaddingEdges dockPadding;

		// Token: 0x04002173 RID: 8563
		private int scrollState;

		// Token: 0x04002174 RID: 8564
		private VScrollProperties verticalScroll;

		// Token: 0x04002175 RID: 8565
		private HScrollProperties horizontalScroll;

		// Token: 0x04002176 RID: 8566
		private static readonly object EVENT_SCROLL = new object();

		// Token: 0x04002177 RID: 8567
		private bool resetRTLHScrollValue;

		/// <summary>Determines the border padding for docked controls.</summary>
		// Token: 0x020007DD RID: 2013
		[TypeConverter(typeof(ScrollableControl.DockPaddingEdgesConverter))]
		public class DockPaddingEdges : ICloneable
		{
			// Token: 0x06006DA8 RID: 28072 RVA: 0x00191DAE File Offset: 0x0018FFAE
			internal DockPaddingEdges(ScrollableControl owner)
			{
				this.owner = owner;
			}

			// Token: 0x06006DA9 RID: 28073 RVA: 0x00191DBD File Offset: 0x0018FFBD
			internal DockPaddingEdges(int left, int right, int top, int bottom)
			{
				this.left = left;
				this.right = right;
				this.top = top;
				this.bottom = bottom;
			}

			/// <summary>Gets or sets the padding width for all edges of a docked control.</summary>
			/// <returns>The padding width, in pixels.</returns>
			// Token: 0x170017FE RID: 6142
			// (get) Token: 0x06006DAA RID: 28074 RVA: 0x00191DE4 File Offset: 0x0018FFE4
			// (set) Token: 0x06006DAB RID: 28075 RVA: 0x00191EAF File Offset: 0x001900AF
			[RefreshProperties(RefreshProperties.All)]
			[SRDescription("PaddingAllDescr")]
			public int All
			{
				get
				{
					if (this.owner == null)
					{
						if (this.left == this.right && this.top == this.bottom && this.left == this.top)
						{
							return this.left;
						}
						return 0;
					}
					else
					{
						if (this.owner.Padding.All == -1 && (this.owner.Padding.Left != -1 || this.owner.Padding.Top != -1 || this.owner.Padding.Right != -1 || this.owner.Padding.Bottom != -1))
						{
							return 0;
						}
						return this.owner.Padding.All;
					}
				}
				set
				{
					if (this.owner == null)
					{
						this.left = value;
						this.top = value;
						this.right = value;
						this.bottom = value;
						return;
					}
					this.owner.Padding = new Padding(value);
				}
			}

			/// <summary>Gets or sets the padding width for the bottom edge of a docked control.</summary>
			/// <returns>The padding width, in pixels.</returns>
			// Token: 0x170017FF RID: 6143
			// (get) Token: 0x06006DAC RID: 28076 RVA: 0x00191EE8 File Offset: 0x001900E8
			// (set) Token: 0x06006DAD RID: 28077 RVA: 0x00191F18 File Offset: 0x00190118
			[RefreshProperties(RefreshProperties.All)]
			[SRDescription("PaddingBottomDescr")]
			public int Bottom
			{
				get
				{
					if (this.owner == null)
					{
						return this.bottom;
					}
					return this.owner.Padding.Bottom;
				}
				set
				{
					if (this.owner == null)
					{
						this.bottom = value;
						return;
					}
					Padding padding = this.owner.Padding;
					padding.Bottom = value;
					this.owner.Padding = padding;
				}
			}

			/// <summary>Gets or sets the padding width for the left edge of a docked control.</summary>
			/// <returns>The padding width, in pixels.</returns>
			// Token: 0x17001800 RID: 6144
			// (get) Token: 0x06006DAE RID: 28078 RVA: 0x00191F58 File Offset: 0x00190158
			// (set) Token: 0x06006DAF RID: 28079 RVA: 0x00191F88 File Offset: 0x00190188
			[RefreshProperties(RefreshProperties.All)]
			[SRDescription("PaddingLeftDescr")]
			public int Left
			{
				get
				{
					if (this.owner == null)
					{
						return this.left;
					}
					return this.owner.Padding.Left;
				}
				set
				{
					if (this.owner == null)
					{
						this.left = value;
						return;
					}
					Padding padding = this.owner.Padding;
					padding.Left = value;
					this.owner.Padding = padding;
				}
			}

			/// <summary>Gets or sets the padding width for the right edge of a docked control.</summary>
			/// <returns>The padding width, in pixels.</returns>
			// Token: 0x17001801 RID: 6145
			// (get) Token: 0x06006DB0 RID: 28080 RVA: 0x00191FC8 File Offset: 0x001901C8
			// (set) Token: 0x06006DB1 RID: 28081 RVA: 0x00191FF8 File Offset: 0x001901F8
			[RefreshProperties(RefreshProperties.All)]
			[SRDescription("PaddingRightDescr")]
			public int Right
			{
				get
				{
					if (this.owner == null)
					{
						return this.right;
					}
					return this.owner.Padding.Right;
				}
				set
				{
					if (this.owner == null)
					{
						this.right = value;
						return;
					}
					Padding padding = this.owner.Padding;
					padding.Right = value;
					this.owner.Padding = padding;
				}
			}

			/// <summary>Gets or sets the padding width for the top edge of a docked control.</summary>
			/// <returns>The padding width, in pixels.</returns>
			// Token: 0x17001802 RID: 6146
			// (get) Token: 0x06006DB2 RID: 28082 RVA: 0x00192038 File Offset: 0x00190238
			// (set) Token: 0x06006DB3 RID: 28083 RVA: 0x00192068 File Offset: 0x00190268
			[RefreshProperties(RefreshProperties.All)]
			[SRDescription("PaddingTopDescr")]
			public int Top
			{
				get
				{
					if (this.owner == null)
					{
						return this.bottom;
					}
					return this.owner.Padding.Top;
				}
				set
				{
					if (this.owner == null)
					{
						this.top = value;
						return;
					}
					Padding padding = this.owner.Padding;
					padding.Top = value;
					this.owner.Padding = padding;
				}
			}

			/// <summary>Determines whether the specified object is equal to the current <see cref="T:System.Windows.Forms.ScrollableControl.DockPaddingEdges" /> object.</summary>
			/// <param name="other">The object to compare with the current <see cref="T:System.Windows.Forms.ScrollableControl.DockPaddingEdges" /> object.</param>
			/// <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.</returns>
			// Token: 0x06006DB4 RID: 28084 RVA: 0x001920A8 File Offset: 0x001902A8
			public override bool Equals(object other)
			{
				ScrollableControl.DockPaddingEdges dockPaddingEdges = other as ScrollableControl.DockPaddingEdges;
				return dockPaddingEdges != null && this.owner.Padding.Equals(dockPaddingEdges.owner.Padding);
			}

			/// <summary>Serves as a hash function for a particular type.</summary>
			/// <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
			// Token: 0x06006DB5 RID: 28085 RVA: 0x0014D2ED File Offset: 0x0014B4ED
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			// Token: 0x06006DB6 RID: 28086 RVA: 0x001920EA File Offset: 0x001902EA
			private void ResetAll()
			{
				this.All = 0;
			}

			// Token: 0x06006DB7 RID: 28087 RVA: 0x001920F3 File Offset: 0x001902F3
			private void ResetBottom()
			{
				this.Bottom = 0;
			}

			// Token: 0x06006DB8 RID: 28088 RVA: 0x001920FC File Offset: 0x001902FC
			private void ResetLeft()
			{
				this.Left = 0;
			}

			// Token: 0x06006DB9 RID: 28089 RVA: 0x00192105 File Offset: 0x00190305
			private void ResetRight()
			{
				this.Right = 0;
			}

			// Token: 0x06006DBA RID: 28090 RVA: 0x0019210E File Offset: 0x0019030E
			private void ResetTop()
			{
				this.Top = 0;
			}

			// Token: 0x06006DBB RID: 28091 RVA: 0x00192118 File Offset: 0x00190318
			internal void Scale(float dx, float dy)
			{
				this.owner.Padding.Scale(dx, dy);
			}

			/// <summary>Returns an empty string.</summary>
			/// <returns>An empty string.</returns>
			// Token: 0x06006DBC RID: 28092 RVA: 0x000F17EC File Offset: 0x000EF9EC
			public override string ToString()
			{
				return "";
			}

			/// <summary>Creates a new object that is a copy of the current instance.</summary>
			/// <returns>A new object that is a copy of the current instance.</returns>
			// Token: 0x06006DBD RID: 28093 RVA: 0x0019213C File Offset: 0x0019033C
			object ICloneable.Clone()
			{
				return new ScrollableControl.DockPaddingEdges(this.Left, this.Right, this.Top, this.Bottom);
			}

			// Token: 0x040042AF RID: 17071
			private ScrollableControl owner;

			// Token: 0x040042B0 RID: 17072
			private int left;

			// Token: 0x040042B1 RID: 17073
			private int right;

			// Token: 0x040042B2 RID: 17074
			private int top;

			// Token: 0x040042B3 RID: 17075
			private int bottom;
		}

		/// <summary>A <see cref="T:System.ComponentModel.TypeConverter" /> for the <see cref="T:System.Windows.Forms.ScrollableControl.DockPaddingEdges" /> class.</summary>
		// Token: 0x020007DE RID: 2014
		public class DockPaddingEdgesConverter : TypeConverter
		{
			/// <summary>Returns a collection of properties for the type of array specified by the value parameter, using the specified context and attributes.</summary>
			/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
			/// <param name="value">An object that specifies the type of array for which to get properties.</param>
			/// <param name="attributes">An array of type attribute that is used as a filter.</param>
			/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that are exposed for the <see cref="T:System.Windows.Forms.ScrollableControl" />.</returns>
			// Token: 0x06006DBE RID: 28094 RVA: 0x00192168 File Offset: 0x00190368
			public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
			{
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(ScrollableControl.DockPaddingEdges), attributes);
				return properties.Sort(new string[] { "All", "Left", "Top", "Right", "Bottom" });
			}

			/// <summary>Returns whether the current object supports properties, using the specified context.</summary>
			/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
			/// <returns>
			///   <see langword="true" /> in all cases.</returns>
			// Token: 0x06006DBF RID: 28095 RVA: 0x00012E4E File Offset: 0x0001104E
			public override bool GetPropertiesSupported(ITypeDescriptorContext context)
			{
				return true;
			}
		}
	}
}
