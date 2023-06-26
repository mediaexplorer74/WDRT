using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies a component that creates an icon in the notification area. This class cannot be inherited.</summary>
	// Token: 0x0200030B RID: 779
	[DefaultProperty("Text")]
	[DefaultEvent("MouseDoubleClick")]
	[Designer("System.Windows.Forms.Design.NotifyIconDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxItemFilter("System.Windows.Forms")]
	[SRDescription("DescriptionNotifyIcon")]
	public sealed class NotifyIcon : Component
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.NotifyIcon" /> class.</summary>
		// Token: 0x06003175 RID: 12661 RVA: 0x000DF668 File Offset: 0x000DD868
		public NotifyIcon()
		{
			this.id = ++NotifyIcon.nextId;
			this.window = new NotifyIcon.NotifyIconNativeWindow(this);
			this.UpdateIcon(this.visible);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.NotifyIcon" /> class with the specified container.</summary>
		/// <param name="container">An <see cref="T:System.ComponentModel.IContainer" /> that represents the container for the <see cref="T:System.Windows.Forms.NotifyIcon" /> control.</param>
		// Token: 0x06003176 RID: 12662 RVA: 0x000DF6D2 File Offset: 0x000DD8D2
		public NotifyIcon(IContainer container)
			: this()
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			container.Add(this);
		}

		/// <summary>Gets or sets the text to display on the balloon tip associated with the <see cref="T:System.Windows.Forms.NotifyIcon" />.</summary>
		/// <returns>The text to display on the balloon tip associated with the <see cref="T:System.Windows.Forms.NotifyIcon" />.</returns>
		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x06003177 RID: 12663 RVA: 0x000DF6EF File Offset: 0x000DD8EF
		// (set) Token: 0x06003178 RID: 12664 RVA: 0x000DF6F7 File Offset: 0x000DD8F7
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[DefaultValue("")]
		[SRDescription("NotifyIconBalloonTipTextDescr")]
		[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string BalloonTipText
		{
			get
			{
				return this.balloonTipText;
			}
			set
			{
				if (value != this.balloonTipText)
				{
					this.balloonTipText = value;
				}
			}
		}

		/// <summary>Gets or sets the icon to display on the balloon tip associated with the <see cref="T:System.Windows.Forms.NotifyIcon" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolTipIcon" /> to display on the balloon tip associated with the <see cref="T:System.Windows.Forms.NotifyIcon" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not a <see cref="T:System.Windows.Forms.ToolTipIcon" />.</exception>
		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x06003179 RID: 12665 RVA: 0x000DF70E File Offset: 0x000DD90E
		// (set) Token: 0x0600317A RID: 12666 RVA: 0x000DF716 File Offset: 0x000DD916
		[SRCategory("CatAppearance")]
		[DefaultValue(ToolTipIcon.None)]
		[SRDescription("NotifyIconBalloonTipIconDescr")]
		public ToolTipIcon BalloonTipIcon
		{
			get
			{
				return this.balloonTipIcon;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ToolTipIcon));
				}
				if (value != this.balloonTipIcon)
				{
					this.balloonTipIcon = value;
				}
			}
		}

		/// <summary>Gets or sets the title of the balloon tip displayed on the <see cref="T:System.Windows.Forms.NotifyIcon" />.</summary>
		/// <returns>The text to display as the title of the balloon tip.</returns>
		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x0600317B RID: 12667 RVA: 0x000DF74E File Offset: 0x000DD94E
		// (set) Token: 0x0600317C RID: 12668 RVA: 0x000DF756 File Offset: 0x000DD956
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[DefaultValue("")]
		[SRDescription("NotifyIconBalloonTipTitleDescr")]
		public string BalloonTipTitle
		{
			get
			{
				return this.balloonTipTitle;
			}
			set
			{
				if (value != this.balloonTipTitle)
				{
					this.balloonTipTitle = value;
				}
			}
		}

		/// <summary>Occurs when the balloon tip is clicked.</summary>
		// Token: 0x1400023F RID: 575
		// (add) Token: 0x0600317D RID: 12669 RVA: 0x000DF76D File Offset: 0x000DD96D
		// (remove) Token: 0x0600317E RID: 12670 RVA: 0x000DF780 File Offset: 0x000DD980
		[SRCategory("CatAction")]
		[SRDescription("NotifyIconOnBalloonTipClickedDescr")]
		public event EventHandler BalloonTipClicked
		{
			add
			{
				base.Events.AddHandler(NotifyIcon.EVENT_BALLOONTIPCLICKED, value);
			}
			remove
			{
				base.Events.RemoveHandler(NotifyIcon.EVENT_BALLOONTIPCLICKED, value);
			}
		}

		/// <summary>Occurs when the balloon tip is closed by the user.</summary>
		// Token: 0x14000240 RID: 576
		// (add) Token: 0x0600317F RID: 12671 RVA: 0x000DF793 File Offset: 0x000DD993
		// (remove) Token: 0x06003180 RID: 12672 RVA: 0x000DF7A6 File Offset: 0x000DD9A6
		[SRCategory("CatAction")]
		[SRDescription("NotifyIconOnBalloonTipClosedDescr")]
		public event EventHandler BalloonTipClosed
		{
			add
			{
				base.Events.AddHandler(NotifyIcon.EVENT_BALLOONTIPCLOSED, value);
			}
			remove
			{
				base.Events.RemoveHandler(NotifyIcon.EVENT_BALLOONTIPCLOSED, value);
			}
		}

		/// <summary>Occurs when the balloon tip is displayed on the screen.</summary>
		// Token: 0x14000241 RID: 577
		// (add) Token: 0x06003181 RID: 12673 RVA: 0x000DF7B9 File Offset: 0x000DD9B9
		// (remove) Token: 0x06003182 RID: 12674 RVA: 0x000DF7CC File Offset: 0x000DD9CC
		[SRCategory("CatAction")]
		[SRDescription("NotifyIconOnBalloonTipShownDescr")]
		public event EventHandler BalloonTipShown
		{
			add
			{
				base.Events.AddHandler(NotifyIcon.EVENT_BALLOONTIPSHOWN, value);
			}
			remove
			{
				base.Events.RemoveHandler(NotifyIcon.EVENT_BALLOONTIPSHOWN, value);
			}
		}

		/// <summary>Gets or sets the shortcut menu for the icon.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ContextMenu" /> for the icon. The default value is <see langword="null" />.</returns>
		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x06003183 RID: 12675 RVA: 0x000DF7DF File Offset: 0x000DD9DF
		// (set) Token: 0x06003184 RID: 12676 RVA: 0x000DF7E7 File Offset: 0x000DD9E7
		[Browsable(false)]
		[DefaultValue(null)]
		[SRCategory("CatBehavior")]
		[SRDescription("NotifyIconMenuDescr")]
		public ContextMenu ContextMenu
		{
			get
			{
				return this.contextMenu;
			}
			set
			{
				this.contextMenu = value;
			}
		}

		/// <summary>Gets or sets the shortcut menu associated with the <see cref="T:System.Windows.Forms.NotifyIcon" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> associated with the <see cref="T:System.Windows.Forms.NotifyIcon" /></returns>
		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x06003185 RID: 12677 RVA: 0x000DF7F0 File Offset: 0x000DD9F0
		// (set) Token: 0x06003186 RID: 12678 RVA: 0x000DF7F8 File Offset: 0x000DD9F8
		[DefaultValue(null)]
		[SRCategory("CatBehavior")]
		[SRDescription("NotifyIconMenuDescr")]
		public ContextMenuStrip ContextMenuStrip
		{
			get
			{
				return this.contextMenuStrip;
			}
			set
			{
				this.contextMenuStrip = value;
			}
		}

		/// <summary>Gets or sets the current icon.</summary>
		/// <returns>The <see cref="T:System.Drawing.Icon" /> displayed by the <see cref="T:System.Windows.Forms.NotifyIcon" /> component. The default value is <see langword="null" />.</returns>
		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x06003187 RID: 12679 RVA: 0x000DF801 File Offset: 0x000DDA01
		// (set) Token: 0x06003188 RID: 12680 RVA: 0x000DF809 File Offset: 0x000DDA09
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[DefaultValue(null)]
		[SRDescription("NotifyIconIconDescr")]
		public Icon Icon
		{
			get
			{
				return this.icon;
			}
			set
			{
				if (this.icon != value)
				{
					this.icon = value;
					this.UpdateIcon(this.visible);
				}
			}
		}

		/// <summary>Gets or sets the ToolTip text displayed when the mouse pointer rests on a notification area icon.</summary>
		/// <returns>The ToolTip text displayed when the mouse pointer rests on a notification area icon.</returns>
		/// <exception cref="T:System.ArgumentException">ToolTip text is more than 63 characters long.</exception>
		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x06003189 RID: 12681 RVA: 0x000DF827 File Offset: 0x000DDA27
		// (set) Token: 0x0600318A RID: 12682 RVA: 0x000DF830 File Offset: 0x000DDA30
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[DefaultValue("")]
		[SRDescription("NotifyIconTextDescr")]
		[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (value != null && !value.Equals(this.text))
				{
					if (value != null && value.Length > 63)
					{
						throw new ArgumentOutOfRangeException("Text", value, SR.GetString("TrayIcon_TextTooLong"));
					}
					this.text = value;
					if (this.added)
					{
						this.UpdateIcon(true);
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the icon is visible in the notification area of the taskbar.</summary>
		/// <returns>
		///   <see langword="true" /> if the icon is visible in the notification area; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x0600318B RID: 12683 RVA: 0x000DF891 File Offset: 0x000DDA91
		// (set) Token: 0x0600318C RID: 12684 RVA: 0x000DF899 File Offset: 0x000DDA99
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[DefaultValue(false)]
		[SRDescription("NotifyIconVisDescr")]
		public bool Visible
		{
			get
			{
				return this.visible;
			}
			set
			{
				if (this.visible != value)
				{
					this.UpdateIcon(value);
					this.visible = value;
				}
			}
		}

		/// <summary>Gets or sets an object that contains data about the <see cref="T:System.Windows.Forms.NotifyIcon" />.</summary>
		/// <returns>The <see cref="T:System.Object" /> that contains data about the <see cref="T:System.Windows.Forms.NotifyIcon" />.</returns>
		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x0600318D RID: 12685 RVA: 0x000DF8B2 File Offset: 0x000DDAB2
		// (set) Token: 0x0600318E RID: 12686 RVA: 0x000DF8BA File Offset: 0x000DDABA
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

		/// <summary>Occurs when the user clicks the icon in the notification area.</summary>
		// Token: 0x14000242 RID: 578
		// (add) Token: 0x0600318F RID: 12687 RVA: 0x000DF8C3 File Offset: 0x000DDAC3
		// (remove) Token: 0x06003190 RID: 12688 RVA: 0x000DF8D6 File Offset: 0x000DDAD6
		[SRCategory("CatAction")]
		[SRDescription("ControlOnClickDescr")]
		public event EventHandler Click
		{
			add
			{
				base.Events.AddHandler(NotifyIcon.EVENT_CLICK, value);
			}
			remove
			{
				base.Events.RemoveHandler(NotifyIcon.EVENT_CLICK, value);
			}
		}

		/// <summary>Occurs when the user double-clicks the icon in the notification area of the taskbar.</summary>
		// Token: 0x14000243 RID: 579
		// (add) Token: 0x06003191 RID: 12689 RVA: 0x000DF8E9 File Offset: 0x000DDAE9
		// (remove) Token: 0x06003192 RID: 12690 RVA: 0x000DF8FC File Offset: 0x000DDAFC
		[SRCategory("CatAction")]
		[SRDescription("ControlOnDoubleClickDescr")]
		public event EventHandler DoubleClick
		{
			add
			{
				base.Events.AddHandler(NotifyIcon.EVENT_DOUBLECLICK, value);
			}
			remove
			{
				base.Events.RemoveHandler(NotifyIcon.EVENT_DOUBLECLICK, value);
			}
		}

		/// <summary>Occurs when the user clicks a <see cref="T:System.Windows.Forms.NotifyIcon" /> with the mouse.</summary>
		// Token: 0x14000244 RID: 580
		// (add) Token: 0x06003193 RID: 12691 RVA: 0x000DF90F File Offset: 0x000DDB0F
		// (remove) Token: 0x06003194 RID: 12692 RVA: 0x000DF922 File Offset: 0x000DDB22
		[SRCategory("CatAction")]
		[SRDescription("NotifyIconMouseClickDescr")]
		public event MouseEventHandler MouseClick
		{
			add
			{
				base.Events.AddHandler(NotifyIcon.EVENT_MOUSECLICK, value);
			}
			remove
			{
				base.Events.RemoveHandler(NotifyIcon.EVENT_MOUSECLICK, value);
			}
		}

		/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.NotifyIcon" /> with the mouse.</summary>
		// Token: 0x14000245 RID: 581
		// (add) Token: 0x06003195 RID: 12693 RVA: 0x000DF935 File Offset: 0x000DDB35
		// (remove) Token: 0x06003196 RID: 12694 RVA: 0x000DF948 File Offset: 0x000DDB48
		[SRCategory("CatAction")]
		[SRDescription("NotifyIconMouseDoubleClickDescr")]
		public event MouseEventHandler MouseDoubleClick
		{
			add
			{
				base.Events.AddHandler(NotifyIcon.EVENT_MOUSEDOUBLECLICK, value);
			}
			remove
			{
				base.Events.RemoveHandler(NotifyIcon.EVENT_MOUSEDOUBLECLICK, value);
			}
		}

		/// <summary>Occurs when the user presses the mouse button while the pointer is over the icon in the notification area of the taskbar.</summary>
		// Token: 0x14000246 RID: 582
		// (add) Token: 0x06003197 RID: 12695 RVA: 0x000DF95B File Offset: 0x000DDB5B
		// (remove) Token: 0x06003198 RID: 12696 RVA: 0x000DF96E File Offset: 0x000DDB6E
		[SRCategory("CatMouse")]
		[SRDescription("ControlOnMouseDownDescr")]
		public event MouseEventHandler MouseDown
		{
			add
			{
				base.Events.AddHandler(NotifyIcon.EVENT_MOUSEDOWN, value);
			}
			remove
			{
				base.Events.RemoveHandler(NotifyIcon.EVENT_MOUSEDOWN, value);
			}
		}

		/// <summary>Occurs when the user moves the mouse while the pointer is over the icon in the notification area of the taskbar.</summary>
		// Token: 0x14000247 RID: 583
		// (add) Token: 0x06003199 RID: 12697 RVA: 0x000DF981 File Offset: 0x000DDB81
		// (remove) Token: 0x0600319A RID: 12698 RVA: 0x000DF994 File Offset: 0x000DDB94
		[SRCategory("CatMouse")]
		[SRDescription("ControlOnMouseMoveDescr")]
		public event MouseEventHandler MouseMove
		{
			add
			{
				base.Events.AddHandler(NotifyIcon.EVENT_MOUSEMOVE, value);
			}
			remove
			{
				base.Events.RemoveHandler(NotifyIcon.EVENT_MOUSEMOVE, value);
			}
		}

		/// <summary>Occurs when the user releases the mouse button while the pointer is over the icon in the notification area of the taskbar.</summary>
		// Token: 0x14000248 RID: 584
		// (add) Token: 0x0600319B RID: 12699 RVA: 0x000DF9A7 File Offset: 0x000DDBA7
		// (remove) Token: 0x0600319C RID: 12700 RVA: 0x000DF9BA File Offset: 0x000DDBBA
		[SRCategory("CatMouse")]
		[SRDescription("ControlOnMouseUpDescr")]
		public event MouseEventHandler MouseUp
		{
			add
			{
				base.Events.AddHandler(NotifyIcon.EVENT_MOUSEUP, value);
			}
			remove
			{
				base.Events.RemoveHandler(NotifyIcon.EVENT_MOUSEUP, value);
			}
		}

		// Token: 0x0600319D RID: 12701 RVA: 0x000DF9D0 File Offset: 0x000DDBD0
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.window != null)
				{
					this.icon = null;
					this.Text = string.Empty;
					this.UpdateIcon(false);
					this.window.DestroyHandle();
					this.window = null;
					this.contextMenu = null;
					this.contextMenuStrip = null;
				}
			}
			else if (this.window != null && this.window.Handle != IntPtr.Zero)
			{
				UnsafeNativeMethods.PostMessage(new HandleRef(this.window, this.window.Handle), 16, 0, 0);
				this.window.ReleaseHandle();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600319E RID: 12702 RVA: 0x000DFA78 File Offset: 0x000DDC78
		private void OnBalloonTipClicked()
		{
			EventHandler eventHandler = (EventHandler)base.Events[NotifyIcon.EVENT_BALLOONTIPCLICKED];
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600319F RID: 12703 RVA: 0x000DFAAC File Offset: 0x000DDCAC
		private void OnBalloonTipClosed()
		{
			EventHandler eventHandler = (EventHandler)base.Events[NotifyIcon.EVENT_BALLOONTIPCLOSED];
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x060031A0 RID: 12704 RVA: 0x000DFAE0 File Offset: 0x000DDCE0
		private void OnBalloonTipShown()
		{
			EventHandler eventHandler = (EventHandler)base.Events[NotifyIcon.EVENT_BALLOONTIPSHOWN];
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x060031A1 RID: 12705 RVA: 0x000DFB14 File Offset: 0x000DDD14
		private void OnClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[NotifyIcon.EVENT_CLICK];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060031A2 RID: 12706 RVA: 0x000DFB44 File Offset: 0x000DDD44
		private void OnDoubleClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[NotifyIcon.EVENT_DOUBLECLICK];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060031A3 RID: 12707 RVA: 0x000DFB74 File Offset: 0x000DDD74
		private void OnMouseClick(MouseEventArgs mea)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[NotifyIcon.EVENT_MOUSECLICK];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, mea);
			}
		}

		// Token: 0x060031A4 RID: 12708 RVA: 0x000DFBA4 File Offset: 0x000DDDA4
		private void OnMouseDoubleClick(MouseEventArgs mea)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[NotifyIcon.EVENT_MOUSEDOUBLECLICK];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, mea);
			}
		}

		// Token: 0x060031A5 RID: 12709 RVA: 0x000DFBD4 File Offset: 0x000DDDD4
		private void OnMouseDown(MouseEventArgs e)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[NotifyIcon.EVENT_MOUSEDOWN];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, e);
			}
		}

		// Token: 0x060031A6 RID: 12710 RVA: 0x000DFC04 File Offset: 0x000DDE04
		private void OnMouseMove(MouseEventArgs e)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[NotifyIcon.EVENT_MOUSEMOVE];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, e);
			}
		}

		// Token: 0x060031A7 RID: 12711 RVA: 0x000DFC34 File Offset: 0x000DDE34
		private void OnMouseUp(MouseEventArgs e)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[NotifyIcon.EVENT_MOUSEUP];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, e);
			}
		}

		/// <summary>Displays a balloon tip in the taskbar for the specified time period.</summary>
		/// <param name="timeout">The time period, in milliseconds, the balloon tip should display.  
		///
		///  This parameter is deprecated as of Windows Vista. Notification display times are now based on system accessibility settings.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than 0.</exception>
		// Token: 0x060031A8 RID: 12712 RVA: 0x000DFC62 File Offset: 0x000DDE62
		public void ShowBalloonTip(int timeout)
		{
			this.ShowBalloonTip(timeout, this.balloonTipTitle, this.balloonTipText, this.balloonTipIcon);
		}

		/// <summary>Displays a balloon tip with the specified title, text, and icon in the taskbar for the specified time period.</summary>
		/// <param name="timeout">The time period, in milliseconds, the balloon tip should display.  
		///
		///  This parameter is deprecated as of Windows Vista. Notification display times are now based on system accessibility settings.</param>
		/// <param name="tipTitle">The title to display on the balloon tip.</param>
		/// <param name="tipText">The text to display on the balloon tip.</param>
		/// <param name="tipIcon">One of the <see cref="T:System.Windows.Forms.ToolTipIcon" /> values.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="tipText" /> is <see langword="null" /> or an empty string.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="tipIcon" /> is not a member of <see cref="T:System.Windows.Forms.ToolTipIcon" />.</exception>
		// Token: 0x060031A9 RID: 12713 RVA: 0x000DFC80 File Offset: 0x000DDE80
		public void ShowBalloonTip(int timeout, string tipTitle, string tipText, ToolTipIcon tipIcon)
		{
			if (timeout < 0)
			{
				throw new ArgumentOutOfRangeException("timeout", SR.GetString("InvalidArgument", new object[]
				{
					"timeout",
					timeout.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (string.IsNullOrEmpty(tipText))
			{
				throw new ArgumentException(SR.GetString("NotifyIconEmptyOrNullTipText"));
			}
			if (!ClientUtils.IsEnumValid(tipIcon, (int)tipIcon, 0, 3))
			{
				throw new InvalidEnumArgumentException("tipIcon", (int)tipIcon, typeof(ToolTipIcon));
			}
			if (this.added)
			{
				if (base.DesignMode)
				{
					return;
				}
				IntSecurity.UnrestrictedWindows.Demand();
				NativeMethods.NOTIFYICONDATA notifyicondata = new NativeMethods.NOTIFYICONDATA();
				if (this.window.Handle == IntPtr.Zero)
				{
					this.window.CreateHandle(new CreateParams());
				}
				notifyicondata.hWnd = this.window.Handle;
				notifyicondata.uID = this.id;
				notifyicondata.uFlags = 16;
				notifyicondata.uTimeoutOrVersion = timeout;
				notifyicondata.szInfoTitle = tipTitle;
				notifyicondata.szInfo = tipText;
				switch (tipIcon)
				{
				case ToolTipIcon.None:
					notifyicondata.dwInfoFlags = 0;
					break;
				case ToolTipIcon.Info:
					notifyicondata.dwInfoFlags = 1;
					break;
				case ToolTipIcon.Warning:
					notifyicondata.dwInfoFlags = 2;
					break;
				case ToolTipIcon.Error:
					notifyicondata.dwInfoFlags = 3;
					break;
				}
				UnsafeNativeMethods.Shell_NotifyIcon(1, notifyicondata);
			}
		}

		// Token: 0x060031AA RID: 12714 RVA: 0x000DFDCC File Offset: 0x000DDFCC
		private void ShowContextMenu()
		{
			if (this.contextMenu != null || this.contextMenuStrip != null)
			{
				NativeMethods.POINT point = new NativeMethods.POINT();
				UnsafeNativeMethods.GetCursorPos(point);
				UnsafeNativeMethods.SetForegroundWindow(new HandleRef(this.window, this.window.Handle));
				if (this.contextMenu != null)
				{
					this.contextMenu.OnPopup(EventArgs.Empty);
					SafeNativeMethods.TrackPopupMenuEx(new HandleRef(this.contextMenu, this.contextMenu.Handle), 72, point.x, point.y, new HandleRef(this.window, this.window.Handle), null);
					UnsafeNativeMethods.PostMessage(new HandleRef(this.window, this.window.Handle), 0, IntPtr.Zero, IntPtr.Zero);
					return;
				}
				if (this.contextMenuStrip != null)
				{
					this.contextMenuStrip.ShowInTaskbar(point.x, point.y);
				}
			}
		}

		// Token: 0x060031AB RID: 12715 RVA: 0x000DFEB8 File Offset: 0x000DE0B8
		private void UpdateIcon(bool showIconInTray)
		{
			object obj = this.syncObj;
			lock (obj)
			{
				if (!base.DesignMode)
				{
					IntSecurity.UnrestrictedWindows.Demand();
					this.window.LockReference(showIconInTray);
					NativeMethods.NOTIFYICONDATA notifyicondata = new NativeMethods.NOTIFYICONDATA();
					notifyicondata.uCallbackMessage = 2048;
					notifyicondata.uFlags = 1;
					if (showIconInTray && this.window.Handle == IntPtr.Zero)
					{
						this.window.CreateHandle(new CreateParams());
					}
					notifyicondata.hWnd = this.window.Handle;
					notifyicondata.uID = this.id;
					notifyicondata.hIcon = IntPtr.Zero;
					notifyicondata.szTip = null;
					if (this.icon != null)
					{
						notifyicondata.uFlags |= 2;
						notifyicondata.hIcon = this.icon.Handle;
					}
					notifyicondata.uFlags |= 4;
					notifyicondata.szTip = this.text;
					if (showIconInTray && this.icon != null)
					{
						if (!this.added)
						{
							UnsafeNativeMethods.Shell_NotifyIcon(0, notifyicondata);
							this.added = true;
						}
						else
						{
							UnsafeNativeMethods.Shell_NotifyIcon(1, notifyicondata);
						}
					}
					else if (this.added)
					{
						UnsafeNativeMethods.Shell_NotifyIcon(2, notifyicondata);
						this.added = false;
					}
				}
			}
		}

		// Token: 0x060031AC RID: 12716 RVA: 0x000E0018 File Offset: 0x000DE218
		private void WmMouseDown(ref Message m, MouseButtons button, int clicks)
		{
			if (clicks == 2)
			{
				this.OnDoubleClick(new MouseEventArgs(button, 2, 0, 0, 0));
				this.OnMouseDoubleClick(new MouseEventArgs(button, 2, 0, 0, 0));
				this.doubleClick = true;
			}
			this.OnMouseDown(new MouseEventArgs(button, clicks, 0, 0, 0));
		}

		// Token: 0x060031AD RID: 12717 RVA: 0x000E0055 File Offset: 0x000DE255
		private void WmMouseMove(ref Message m)
		{
			this.OnMouseMove(new MouseEventArgs(Control.MouseButtons, 0, 0, 0, 0));
		}

		// Token: 0x060031AE RID: 12718 RVA: 0x000E006C File Offset: 0x000DE26C
		private void WmMouseUp(ref Message m, MouseButtons button)
		{
			this.OnMouseUp(new MouseEventArgs(button, 0, 0, 0, 0));
			if (!this.doubleClick)
			{
				this.OnClick(new MouseEventArgs(button, 0, 0, 0, 0));
				this.OnMouseClick(new MouseEventArgs(button, 0, 0, 0, 0));
			}
			this.doubleClick = false;
		}

		// Token: 0x060031AF RID: 12719 RVA: 0x000E00B8 File Offset: 0x000DE2B8
		private void WmTaskbarCreated(ref Message m)
		{
			this.added = false;
			this.UpdateIcon(this.visible);
		}

		// Token: 0x060031B0 RID: 12720 RVA: 0x000E00D0 File Offset: 0x000DE2D0
		private void WndProc(ref Message msg)
		{
			int msg2 = msg.Msg;
			if (msg2 <= 44)
			{
				if (msg2 == 2)
				{
					this.UpdateIcon(false);
					return;
				}
				if (msg2 != 43)
				{
					if (msg2 == 44)
					{
						if (msg.WParam == IntPtr.Zero)
						{
							this.WmMeasureMenuItem(ref msg);
							return;
						}
						return;
					}
				}
				else
				{
					if (msg.WParam == IntPtr.Zero)
					{
						this.WmDrawItemMenuItem(ref msg);
						return;
					}
					return;
				}
			}
			else if (msg2 != 273)
			{
				if (msg2 == 279)
				{
					this.WmInitMenuPopup(ref msg);
					return;
				}
				if (msg2 == 2048)
				{
					int num = (int)msg.LParam;
					switch (num)
					{
					case 512:
						this.WmMouseMove(ref msg);
						return;
					case 513:
						this.WmMouseDown(ref msg, MouseButtons.Left, 1);
						return;
					case 514:
						this.WmMouseUp(ref msg, MouseButtons.Left);
						return;
					case 515:
						this.WmMouseDown(ref msg, MouseButtons.Left, 2);
						return;
					case 516:
						this.WmMouseDown(ref msg, MouseButtons.Right, 1);
						return;
					case 517:
						if (this.contextMenu != null || this.contextMenuStrip != null)
						{
							this.ShowContextMenu();
						}
						this.WmMouseUp(ref msg, MouseButtons.Right);
						return;
					case 518:
						this.WmMouseDown(ref msg, MouseButtons.Right, 2);
						return;
					case 519:
						this.WmMouseDown(ref msg, MouseButtons.Middle, 1);
						return;
					case 520:
						this.WmMouseUp(ref msg, MouseButtons.Middle);
						return;
					case 521:
						this.WmMouseDown(ref msg, MouseButtons.Middle, 2);
						return;
					default:
						switch (num)
						{
						case 1026:
							this.OnBalloonTipShown();
							return;
						case 1027:
							this.OnBalloonTipClosed();
							return;
						case 1028:
							this.OnBalloonTipClosed();
							return;
						case 1029:
							this.OnBalloonTipClicked();
							return;
						default:
							return;
						}
						break;
					}
				}
			}
			else
			{
				if (IntPtr.Zero == msg.LParam)
				{
					Command.DispatchID((int)msg.WParam & 65535);
					return;
				}
				this.window.DefWndProc(ref msg);
				return;
			}
			if (msg.Msg == NotifyIcon.WM_TASKBARCREATED)
			{
				this.WmTaskbarCreated(ref msg);
			}
			this.window.DefWndProc(ref msg);
		}

		// Token: 0x060031B1 RID: 12721 RVA: 0x000E02D3 File Offset: 0x000DE4D3
		private void WmInitMenuPopup(ref Message m)
		{
			if (this.contextMenu != null && this.contextMenu.ProcessInitMenuPopup(m.WParam))
			{
				return;
			}
			this.window.DefWndProc(ref m);
		}

		// Token: 0x060031B2 RID: 12722 RVA: 0x000E0300 File Offset: 0x000DE500
		private void WmMeasureMenuItem(ref Message m)
		{
			NativeMethods.MEASUREITEMSTRUCT measureitemstruct = (NativeMethods.MEASUREITEMSTRUCT)m.GetLParam(typeof(NativeMethods.MEASUREITEMSTRUCT));
			MenuItem menuItemFromItemData = MenuItem.GetMenuItemFromItemData(measureitemstruct.itemData);
			if (menuItemFromItemData != null)
			{
				menuItemFromItemData.WmMeasureItem(ref m);
			}
		}

		// Token: 0x060031B3 RID: 12723 RVA: 0x000E033C File Offset: 0x000DE53C
		private void WmDrawItemMenuItem(ref Message m)
		{
			NativeMethods.DRAWITEMSTRUCT drawitemstruct = (NativeMethods.DRAWITEMSTRUCT)m.GetLParam(typeof(NativeMethods.DRAWITEMSTRUCT));
			MenuItem menuItemFromItemData = MenuItem.GetMenuItemFromItemData(drawitemstruct.itemData);
			if (menuItemFromItemData != null)
			{
				menuItemFromItemData.WmDrawItem(ref m);
			}
		}

		// Token: 0x04001E22 RID: 7714
		private static readonly object EVENT_MOUSEDOWN = new object();

		// Token: 0x04001E23 RID: 7715
		private static readonly object EVENT_MOUSEMOVE = new object();

		// Token: 0x04001E24 RID: 7716
		private static readonly object EVENT_MOUSEUP = new object();

		// Token: 0x04001E25 RID: 7717
		private static readonly object EVENT_CLICK = new object();

		// Token: 0x04001E26 RID: 7718
		private static readonly object EVENT_DOUBLECLICK = new object();

		// Token: 0x04001E27 RID: 7719
		private static readonly object EVENT_MOUSECLICK = new object();

		// Token: 0x04001E28 RID: 7720
		private static readonly object EVENT_MOUSEDOUBLECLICK = new object();

		// Token: 0x04001E29 RID: 7721
		private static readonly object EVENT_BALLOONTIPSHOWN = new object();

		// Token: 0x04001E2A RID: 7722
		private static readonly object EVENT_BALLOONTIPCLICKED = new object();

		// Token: 0x04001E2B RID: 7723
		private static readonly object EVENT_BALLOONTIPCLOSED = new object();

		// Token: 0x04001E2C RID: 7724
		private const int WM_TRAYMOUSEMESSAGE = 2048;

		// Token: 0x04001E2D RID: 7725
		private static int WM_TASKBARCREATED = SafeNativeMethods.RegisterWindowMessage("TaskbarCreated");

		// Token: 0x04001E2E RID: 7726
		private object syncObj = new object();

		// Token: 0x04001E2F RID: 7727
		private Icon icon;

		// Token: 0x04001E30 RID: 7728
		private string text = "";

		// Token: 0x04001E31 RID: 7729
		private int id;

		// Token: 0x04001E32 RID: 7730
		private bool added;

		// Token: 0x04001E33 RID: 7731
		private NotifyIcon.NotifyIconNativeWindow window;

		// Token: 0x04001E34 RID: 7732
		private ContextMenu contextMenu;

		// Token: 0x04001E35 RID: 7733
		private ContextMenuStrip contextMenuStrip;

		// Token: 0x04001E36 RID: 7734
		private ToolTipIcon balloonTipIcon;

		// Token: 0x04001E37 RID: 7735
		private string balloonTipText = "";

		// Token: 0x04001E38 RID: 7736
		private string balloonTipTitle = "";

		// Token: 0x04001E39 RID: 7737
		private static int nextId = 0;

		// Token: 0x04001E3A RID: 7738
		private object userData;

		// Token: 0x04001E3B RID: 7739
		private bool doubleClick;

		// Token: 0x04001E3C RID: 7740
		private bool visible;

		// Token: 0x020007C7 RID: 1991
		private class NotifyIconNativeWindow : NativeWindow
		{
			// Token: 0x06006D4A RID: 27978 RVA: 0x00191282 File Offset: 0x0018F482
			internal NotifyIconNativeWindow(NotifyIcon component)
			{
				this.reference = component;
			}

			// Token: 0x06006D4B RID: 27979 RVA: 0x00191294 File Offset: 0x0018F494
			~NotifyIconNativeWindow()
			{
				if (base.Handle != IntPtr.Zero)
				{
					UnsafeNativeMethods.PostMessage(new HandleRef(this, base.Handle), 16, 0, 0);
				}
			}

			// Token: 0x06006D4C RID: 27980 RVA: 0x001912E4 File Offset: 0x0018F4E4
			public void LockReference(bool locked)
			{
				if (locked)
				{
					if (!this.rootRef.IsAllocated)
					{
						this.rootRef = GCHandle.Alloc(this.reference, GCHandleType.Normal);
						return;
					}
				}
				else if (this.rootRef.IsAllocated)
				{
					this.rootRef.Free();
				}
			}

			// Token: 0x06006D4D RID: 27981 RVA: 0x0003B8FD File Offset: 0x00039AFD
			protected override void OnThreadException(Exception e)
			{
				Application.OnThreadException(e);
			}

			// Token: 0x06006D4E RID: 27982 RVA: 0x00191321 File Offset: 0x0018F521
			protected override void WndProc(ref Message m)
			{
				this.reference.WndProc(ref m);
			}

			// Token: 0x040041B8 RID: 16824
			internal NotifyIcon reference;

			// Token: 0x040041B9 RID: 16825
			private GCHandle rootRef;
		}
	}
}
