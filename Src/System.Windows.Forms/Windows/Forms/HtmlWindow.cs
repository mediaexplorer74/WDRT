using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	/// <summary>Represents the logical window that contains one or more instances of <see cref="T:System.Windows.Forms.HtmlDocument" />.</summary>
	// Token: 0x02000284 RID: 644
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class HtmlWindow
	{
		// Token: 0x06002943 RID: 10563 RVA: 0x000BD3B4 File Offset: 0x000BB5B4
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		internal HtmlWindow(HtmlShimManager shimManager, UnsafeNativeMethods.IHTMLWindow2 win)
		{
			this.htmlWindow2 = win;
			this.shimManager = shimManager;
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06002944 RID: 10564 RVA: 0x000BD3CA File Offset: 0x000BB5CA
		internal UnsafeNativeMethods.IHTMLWindow2 NativeHtmlWindow
		{
			get
			{
				return this.htmlWindow2;
			}
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06002945 RID: 10565 RVA: 0x000BD3D2 File Offset: 0x000BB5D2
		private HtmlShimManager ShimManager
		{
			get
			{
				return this.shimManager;
			}
		}

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06002946 RID: 10566 RVA: 0x000BD3DC File Offset: 0x000BB5DC
		private HtmlWindow.HtmlWindowShim WindowShim
		{
			get
			{
				if (this.ShimManager != null)
				{
					HtmlWindow.HtmlWindowShim htmlWindowShim = this.ShimManager.GetWindowShim(this);
					if (htmlWindowShim == null)
					{
						this.shimManager.AddWindowShim(this);
						htmlWindowShim = this.ShimManager.GetWindowShim(this);
					}
					return htmlWindowShim;
				}
				return null;
			}
		}

		/// <summary>Gets the HTML document contained within the window.</summary>
		/// <returns>A valid instance of <see cref="T:System.Windows.Forms.HtmlDocument" />, if a document is loaded. If this window contains a <c>FRAMESET</c>, or no document is currently loaded, it will return <see langword="null" />.</returns>
		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06002947 RID: 10567 RVA: 0x000BD420 File Offset: 0x000BB620
		public HtmlDocument Document
		{
			get
			{
				UnsafeNativeMethods.IHTMLDocument ihtmldocument = this.NativeHtmlWindow.GetDocument() as UnsafeNativeMethods.IHTMLDocument;
				if (ihtmldocument == null)
				{
					return null;
				}
				return new HtmlDocument(this.ShimManager, ihtmldocument);
			}
		}

		/// <summary>Gets the unmanaged interface wrapped by this class.</summary>
		/// <returns>An object that can be cast to an <see langword="IHTMLWindow2" />, <see langword="IHTMLWindow3" />, or <see langword="IHTMLWindow4" /> pointer.</returns>
		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06002948 RID: 10568 RVA: 0x000BD44F File Offset: 0x000BB64F
		public object DomWindow
		{
			get
			{
				return this.NativeHtmlWindow;
			}
		}

		/// <summary>Gets a reference to each of the <c>FRAME</c> elements defined within the Web page.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.HtmlWindowCollection" /> of a document's <c>FRAME</c> and <c>IFRAME</c> objects.</returns>
		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06002949 RID: 10569 RVA: 0x000BD458 File Offset: 0x000BB658
		public HtmlWindowCollection Frames
		{
			get
			{
				UnsafeNativeMethods.IHTMLFramesCollection2 frames = this.NativeHtmlWindow.GetFrames();
				if (frames == null)
				{
					return null;
				}
				return new HtmlWindowCollection(this.ShimManager, frames);
			}
		}

		/// <summary>Gets an object containing the user's most recently visited URLs.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.HtmlHistory" /> for the current window.</returns>
		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x0600294A RID: 10570 RVA: 0x000BD484 File Offset: 0x000BB684
		public HtmlHistory History
		{
			get
			{
				UnsafeNativeMethods.IOmHistory history = this.NativeHtmlWindow.GetHistory();
				if (history == null)
				{
					return null;
				}
				return new HtmlHistory(history);
			}
		}

		/// <summary>Gets a value indicating whether this window is open or closed.</summary>
		/// <returns>
		///   <see langword="true" /> if the window is still open on the screen; otherwise, <see langword="false" />.</returns>
		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x0600294B RID: 10571 RVA: 0x000BD4A8 File Offset: 0x000BB6A8
		public bool IsClosed
		{
			get
			{
				return this.NativeHtmlWindow.GetClosed();
			}
		}

		/// <summary>Gets or sets the name of the window.</summary>
		/// <returns>A <see cref="T:System.String" /> representing the name.</returns>
		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x0600294C RID: 10572 RVA: 0x000BD4B5 File Offset: 0x000BB6B5
		// (set) Token: 0x0600294D RID: 10573 RVA: 0x000BD4C2 File Offset: 0x000BB6C2
		public string Name
		{
			get
			{
				return this.NativeHtmlWindow.GetName();
			}
			set
			{
				this.NativeHtmlWindow.SetName(value);
			}
		}

		/// <summary>Gets a reference to the window that opened the current window.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.HtmlWindow" /> that was created by a call to the <see cref="M:System.Windows.Forms.HtmlWindow.Open(System.String,System.String,System.String,System.Boolean)" /> or <see cref="M:System.Windows.Forms.HtmlWindow.OpenNew(System.String,System.String)" /> methods. If the window was not created using one of these methods, this property returns <see langword="null" />.</returns>
		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x0600294E RID: 10574 RVA: 0x000BD4D0 File Offset: 0x000BB6D0
		public HtmlWindow Opener
		{
			get
			{
				UnsafeNativeMethods.IHTMLWindow2 ihtmlwindow = this.NativeHtmlWindow.GetOpener() as UnsafeNativeMethods.IHTMLWindow2;
				if (ihtmlwindow == null)
				{
					return null;
				}
				return new HtmlWindow(this.ShimManager, ihtmlwindow);
			}
		}

		/// <summary>Gets the window which resides above the current one in a page containing frames.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.HtmlWindow" /> that owns the current window. If the current window is not a <c>FRAME</c>, or is not embedded inside of a <c>FRAME</c>, it returns <see langword="null" />.</returns>
		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x0600294F RID: 10575 RVA: 0x000BD500 File Offset: 0x000BB700
		public HtmlWindow Parent
		{
			get
			{
				UnsafeNativeMethods.IHTMLWindow2 parent = this.NativeHtmlWindow.GetParent();
				if (parent == null)
				{
					return null;
				}
				return new HtmlWindow(this.ShimManager, parent);
			}
		}

		/// <summary>Gets the position of the window's client area on the screen.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> describing the x -and y-coordinates of the top-left corner of the screen, in pixels.</returns>
		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06002950 RID: 10576 RVA: 0x000BD52A File Offset: 0x000BB72A
		public Point Position
		{
			get
			{
				return new Point(((UnsafeNativeMethods.IHTMLWindow3)this.NativeHtmlWindow).GetScreenLeft(), ((UnsafeNativeMethods.IHTMLWindow3)this.NativeHtmlWindow).GetScreenTop());
			}
		}

		/// <summary>Gets or sets the size of the current window.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> describing the size of the window in pixels.</returns>
		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06002951 RID: 10577 RVA: 0x000BD554 File Offset: 0x000BB754
		// (set) Token: 0x06002952 RID: 10578 RVA: 0x000BD583 File Offset: 0x000BB783
		public Size Size
		{
			get
			{
				UnsafeNativeMethods.IHTMLElement body = this.NativeHtmlWindow.GetDocument().GetBody();
				return new Size(body.GetOffsetWidth(), body.GetOffsetHeight());
			}
			set
			{
				this.ResizeTo(value.Width, value.Height);
			}
		}

		/// <summary>Gets or sets the text displayed in the status bar of a window.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the current status text.</returns>
		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06002953 RID: 10579 RVA: 0x000BD599 File Offset: 0x000BB799
		// (set) Token: 0x06002954 RID: 10580 RVA: 0x000BD5A6 File Offset: 0x000BB7A6
		public string StatusBarText
		{
			get
			{
				return this.NativeHtmlWindow.GetStatus();
			}
			set
			{
				this.NativeHtmlWindow.SetStatus(value);
			}
		}

		/// <summary>Gets the URL corresponding to the current item displayed in the window.</summary>
		/// <returns>A <see cref="T:System.Uri" /> describing the URL.</returns>
		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06002955 RID: 10581 RVA: 0x000BD5B4 File Offset: 0x000BB7B4
		public Uri Url
		{
			get
			{
				UnsafeNativeMethods.IHTMLLocation location = this.NativeHtmlWindow.GetLocation();
				string text = ((location == null) ? "" : location.GetHref());
				if (!string.IsNullOrEmpty(text))
				{
					return new Uri(text);
				}
				return null;
			}
		}

		/// <summary>Gets the frame element corresponding to this window.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.HtmlElement" /> corresponding to this window's <c>FRAME</c> element. If this window is not a frame, it returns <see langword="null" />.</returns>
		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06002956 RID: 10582 RVA: 0x000BD5F0 File Offset: 0x000BB7F0
		public HtmlElement WindowFrameElement
		{
			get
			{
				UnsafeNativeMethods.IHTMLElement ihtmlelement = ((UnsafeNativeMethods.IHTMLWindow4)this.NativeHtmlWindow).frameElement() as UnsafeNativeMethods.IHTMLElement;
				if (ihtmlelement == null)
				{
					return null;
				}
				return new HtmlElement(this.ShimManager, ihtmlelement);
			}
		}

		/// <summary>Displays a message box.</summary>
		/// <param name="message">The <see cref="T:System.String" /> to display in the message box.</param>
		// Token: 0x06002957 RID: 10583 RVA: 0x000BD624 File Offset: 0x000BB824
		public void Alert(string message)
		{
			this.NativeHtmlWindow.Alert(message);
		}

		/// <summary>Adds an event handler for the named HTML DOM event.</summary>
		/// <param name="eventName">The name of the event you want to handle.</param>
		/// <param name="eventHandler">A reference to the managed code that handles the event.</param>
		// Token: 0x06002958 RID: 10584 RVA: 0x000BD632 File Offset: 0x000BB832
		public void AttachEventHandler(string eventName, EventHandler eventHandler)
		{
			this.WindowShim.AttachEventHandler(eventName, eventHandler);
		}

		/// <summary>Closes the window.</summary>
		// Token: 0x06002959 RID: 10585 RVA: 0x000BD641 File Offset: 0x000BB841
		public void Close()
		{
			this.NativeHtmlWindow.Close();
		}

		/// <summary>Displays a dialog box with a message and buttons to solicit a yes/no response.</summary>
		/// <param name="message">The text to display to the user.</param>
		/// <returns>
		///   <see langword="true" /> if the user clicked Yes; <see langword="false" /> if the user clicked No or closed the dialog box.</returns>
		// Token: 0x0600295A RID: 10586 RVA: 0x000BD64E File Offset: 0x000BB84E
		public bool Confirm(string message)
		{
			return this.NativeHtmlWindow.Confirm(message);
		}

		/// <summary>Removes the named event handler.</summary>
		/// <param name="eventName">The name of the event you want to handle.</param>
		/// <param name="eventHandler">A reference to the managed code that handles the event.</param>
		// Token: 0x0600295B RID: 10587 RVA: 0x000BD65C File Offset: 0x000BB85C
		public void DetachEventHandler(string eventName, EventHandler eventHandler)
		{
			this.WindowShim.DetachEventHandler(eventName, eventHandler);
		}

		/// <summary>Puts the focus on the current window.</summary>
		// Token: 0x0600295C RID: 10588 RVA: 0x000BD66B File Offset: 0x000BB86B
		public void Focus()
		{
			this.NativeHtmlWindow.Focus();
		}

		/// <summary>Moves the window to the specified coordinates on the screen.</summary>
		/// <param name="x">The x-coordinate of the window's upper-left corner.</param>
		/// <param name="y">The y-coordinate of the window's upper-left corner.</param>
		/// <exception cref="T:System.UnauthorizedAccessException">The code trying to execute this operation does not have permission to manipulate this window.</exception>
		// Token: 0x0600295D RID: 10589 RVA: 0x000BD678 File Offset: 0x000BB878
		public void MoveTo(int x, int y)
		{
			this.NativeHtmlWindow.MoveTo(x, y);
		}

		/// <summary>Moves the window to the specified coordinates on the screen.</summary>
		/// <param name="point">The x- and y-coordinates of the window's upper-left corner.</param>
		/// <exception cref="T:System.UnauthorizedAccessException">The code trying to execute this operation does not have permission to manipulate this window.</exception>
		// Token: 0x0600295E RID: 10590 RVA: 0x000BD687 File Offset: 0x000BB887
		public void MoveTo(Point point)
		{
			this.NativeHtmlWindow.MoveTo(point.X, point.Y);
		}

		/// <summary>Displays a new document in the current window.</summary>
		/// <param name="url">The location, specified as a <see cref="T:System.Uri" />, of the document or object to display in the current window.</param>
		// Token: 0x0600295F RID: 10591 RVA: 0x000BD6A2 File Offset: 0x000BB8A2
		public void Navigate(Uri url)
		{
			this.NativeHtmlWindow.Navigate(url.ToString());
		}

		/// <summary>Displays or downloads the new content located at the specified URL.</summary>
		/// <param name="urlString">The resource to display, described by a Uniform Resource Locator.</param>
		// Token: 0x06002960 RID: 10592 RVA: 0x000BD6B5 File Offset: 0x000BB8B5
		public void Navigate(string urlString)
		{
			this.NativeHtmlWindow.Navigate(urlString);
		}

		/// <summary>Displays a file in the named window.</summary>
		/// <param name="urlString">The Uniform Resource Locator that describes the location of the file to load.</param>
		/// <param name="target">The name of the window in which to open the resource. This may be a developer-supplied name, or one of the following special values:  
		///  <c>_blank</c>: Opens <paramref name="urlString" /> in a new window. Works the same as a call to <see cref="M:System.Windows.Forms.HtmlWindow.OpenNew(System.String,System.String)" />.  
		///  <c>_media</c>: Opens <paramref name="urlString" /> in the Media bar.  
		///  <c>_parent</c>: Opens <paramref name="urlString" /> in the window that created the current window.  
		///  <c>_search</c>: Opens <paramref name="urlString" /> in the Search bar.  
		///  <c>_self</c>: Opens <paramref name="urlString" /> in the current window.  
		///  <c>_top</c>: If called against a window belonging to a <c>FRAME</c> element, opens <paramref name="urlString" /> in the window hosting its <c>FRAMESET</c>. Otherwise, acts the same as <c>_self</c>.</param>
		/// <param name="windowOptions">A comma-delimited string consisting of zero or more of the following options in the form <c>name=value</c>. Except for the <c>left</c>, <c>top</c>, <c>height</c>, and <c>width</c> options, which take arbitrary integers, each option accepts <c>yes</c> or <see langword="1" />, and <c>no</c> or <see langword="0" />, as valid values.  
		///  <c>channelmode</c>: Used with the deprecated channels technology of Internet Explorer 4.0. Default is <c>no</c>.  
		///  <c>directories</c>: Whether the window should display directory navigation buttons. Default is <c>yes</c>.  
		///  <c>height</c>: The height of the window's client area, in pixels. The minimum is 100; attempts to open a window smaller than this will cause the window to open according to the Internet Explorer defaults.  
		///  <c>left</c>: The left (x-coordinate) position of the window, relative to the upper-left corner of the user's screen, in pixels. Must be a positive integer.  
		///  <c>location</c>: Whether to display the Address bar, which enables users to navigate the window to a new URL. Default is <c>yes</c>.  
		///  <c>menubar</c>: Whether to display menus on the new window. Default is <c>yes</c>.  
		///  <c>resizable</c>: Whether the window can be resized by the user. Default is <c>yes</c>.  
		///  <c>scrollbars</c>: Whether the window has horizontal and vertical scroll bars. Default is <c>yes</c>.  
		///  <c>status</c>: Whether the window has a status bar at the bottom. Default is <c>yes</c>.  
		///  <c>titlebar</c>: Whether the title of the current page is displayed. Setting this option to <c>no</c> has no effect within a managed application; the title bar will always appear.  
		///  <c>toolbar</c>: Whether toolbar buttons such as Back, Forward, and Stop are visible. Default is <c>yes</c>.  
		///  <c>top</c>: The top (y-coordinate) position of the window, relative to the upper-left corner of the user's screen, in pixels. Must be a positive integer.  
		///  <c>width</c>: The width of the window's client area, in pixels. The minimum is 100; attempts to open a window smaller than this will cause the window to open according to the Internet Explorer defaults.</param>
		/// <param name="replaceEntry">Whether <paramref name="urlString" /> replaces the current window's URL in the navigation history. This will effect the operation of methods on the <see cref="T:System.Windows.Forms.HtmlHistory" /> class.</param>
		/// <returns>An <see cref="T:System.Windows.Forms.HtmlWindow" /> representing the new window, or the previously created window named by the <paramref name="target" /> parameter.</returns>
		// Token: 0x06002961 RID: 10593 RVA: 0x000BD6C4 File Offset: 0x000BB8C4
		public HtmlWindow Open(string urlString, string target, string windowOptions, bool replaceEntry)
		{
			UnsafeNativeMethods.IHTMLWindow2 ihtmlwindow = this.NativeHtmlWindow.Open(urlString, target, windowOptions, replaceEntry);
			if (ihtmlwindow == null)
			{
				return null;
			}
			return new HtmlWindow(this.ShimManager, ihtmlwindow);
		}

		/// <summary>Displays a file in the named window.</summary>
		/// <param name="url">The Uniform Resource Locator that describes the location of the file to load.</param>
		/// <param name="target">The name of the window in which to open the resource. This can be a developer-supplied name, or one of the following special values:  
		///  <c>_blank</c>: Opens <paramref name="url" /> in a new window. Works the same as a call to <see cref="M:System.Windows.Forms.HtmlWindow.OpenNew(System.String,System.String)" />.  
		///  <c>_media</c>: Opens <paramref name="url" /> in the Media bar.  
		///  <c>_parent</c>: Opens <paramref name="url" /> in the window that created the current window.  
		///  <c>_search</c>: Opens <paramref name="url" /> in the Search bar.  
		///  <c>_self</c>: Opens <paramref name="url" /> in the current window.  
		///  <c>_top</c>: If called against a window belonging to a <c>FRAME</c> element, opens <paramref name="url" /> in the window hosting its <c>FRAMESET</c>. Otherwise, acts the same as <c>_self</c>.</param>
		/// <param name="windowOptions">A comma-delimited string consisting of zero or more of the following options in the form <c>name=value</c>. Except for the <c>left</c>, <c>top</c>, <c>height</c>, and <c>width</c> options, which take arbitrary integers, each option accepts <c>yes</c> or <see langword="1" />, and <c>no</c> or <see langword="0" />, as valid values.  
		///  <c>channelmode</c>: Used with the deprecated channels technology of Internet Explorer 4.0. Default is <c>no</c>.  
		///  <c>directories</c>: Whether the window should display directory navigation buttons. Default is <c>yes</c>.  
		///  <c>height</c>: The height of the window's client area, in pixels. The minimum is 100; attempts to open a window smaller than this will cause the window to open according to The Internet Explorer defaults.  
		///  <c>left</c>: The left (x-coordinate) position of the window, relative to the upper-left corner of the user's screen, in pixels. Must be a positive integer.  
		///  <c>location</c>: Whether to display the Address bar, which enables users to navigate the window to a new URL. Default is <c>yes</c>.  
		///  <c>menubar</c>: Whether to display menus on the new window. Default is <c>yes</c>.  
		///  <c>resizable</c>: Whether the window can be resized by the user. Default is <c>yes</c>.  
		///  <c>scrollbars</c>: Whether the window has horizontal and vertical scroll bars. Default is <c>yes</c>.  
		///  <c>status</c>: Whether the window has a status bar at the bottom. Default is <c>yes</c>.  
		///  <c>titlebar</c>: Whether the title of the current page is displayed. Setting this option to <c>no</c> has no effect within a managed application; the title bar will always appear.  
		///  <c>toolbar</c>: Whether toolbar buttons such as Back, Forward, and Stop are visible. Default is <c>yes</c>.  
		///  <c>top</c>: The top (y-coordinate) position of the window, relative to the upper-left corner of the user's screen, in pixels. Must be a positive integer.  
		///  <c>width</c>: The width of the window's client area, in pixels. The minimum is 100; attempts to open a window smaller than this will cause the window to open according to The Internet Explorer defaults.</param>
		/// <param name="replaceEntry">Whether <paramref name="url" /> replaces the current window's URL in the navigation history. This will effect the operation of methods on the <see cref="T:System.Windows.Forms.HtmlHistory" /> class.</param>
		/// <returns>An <see cref="T:System.Windows.Forms.HtmlWindow" /> representing the new window, or the previously created window named by the <paramref name="target" /> parameter.</returns>
		// Token: 0x06002962 RID: 10594 RVA: 0x000BD6F3 File Offset: 0x000BB8F3
		public HtmlWindow Open(Uri url, string target, string windowOptions, bool replaceEntry)
		{
			return this.Open(url.ToString(), target, windowOptions, replaceEntry);
		}

		/// <summary>Displays a file in a new window.</summary>
		/// <param name="urlString">The Uniform Resource Locator that describes the location of the file to load.</param>
		/// <param name="windowOptions">A comma-delimited string consisting of zero or more of the following options in the form <c>name=value</c>. See <see cref="M:System.Windows.Forms.HtmlWindow.Open(System.String,System.String,System.String,System.Boolean)" /> for a full description of the valid options.</param>
		/// <returns>An <see cref="T:System.Windows.Forms.HtmlWindow" /> representing the new window.</returns>
		// Token: 0x06002963 RID: 10595 RVA: 0x000BD708 File Offset: 0x000BB908
		public HtmlWindow OpenNew(string urlString, string windowOptions)
		{
			UnsafeNativeMethods.IHTMLWindow2 ihtmlwindow = this.NativeHtmlWindow.Open(urlString, "_blank", windowOptions, true);
			if (ihtmlwindow == null)
			{
				return null;
			}
			return new HtmlWindow(this.ShimManager, ihtmlwindow);
		}

		/// <summary>Displays a file in a new window.</summary>
		/// <param name="url">The Uniform Resource Locator that describes the location of the file to load.</param>
		/// <param name="windowOptions">A comma-delimited string consisting of zero or more of the following options in the form <c>name=value</c>. See <see cref="M:System.Windows.Forms.HtmlWindow.Open(System.String,System.String,System.String,System.Boolean)" /> for a full description of the valid options.</param>
		/// <returns>An <see cref="T:System.Windows.Forms.HtmlWindow" /> representing the new window.</returns>
		// Token: 0x06002964 RID: 10596 RVA: 0x000BD73A File Offset: 0x000BB93A
		public HtmlWindow OpenNew(Uri url, string windowOptions)
		{
			return this.OpenNew(url.ToString(), windowOptions);
		}

		/// <summary>Shows a dialog box that displays a message and a text box to the user.</summary>
		/// <param name="message">The message to display to the user.</param>
		/// <param name="defaultInputValue">The default value displayed in the text box.</param>
		/// <returns>A <see cref="T:System.String" /> representing the text entered by the user.</returns>
		// Token: 0x06002965 RID: 10597 RVA: 0x000BD749 File Offset: 0x000BB949
		public string Prompt(string message, string defaultInputValue)
		{
			return this.NativeHtmlWindow.Prompt(message, defaultInputValue).ToString();
		}

		/// <summary>Takes focus off of the current window.</summary>
		// Token: 0x06002966 RID: 10598 RVA: 0x000BD75D File Offset: 0x000BB95D
		public void RemoveFocus()
		{
			this.NativeHtmlWindow.Blur();
		}

		/// <summary>Changes the size of the window to the specified dimensions.</summary>
		/// <param name="width">Describes the desired width of the window, in pixels. Must be 100 pixels or more.</param>
		/// <param name="height">Describes the desired height of the window, in pixels. Must be 100 pixels or more.</param>
		/// <exception cref="T:System.UnauthorizedAccessException">The window you are trying to resize is in a different domain than its parent window. This restriction is part of cross-frame scripting security; for more information, see About Cross-Frame Scripting and Security.</exception>
		// Token: 0x06002967 RID: 10599 RVA: 0x000BD76A File Offset: 0x000BB96A
		public void ResizeTo(int width, int height)
		{
			this.NativeHtmlWindow.ResizeTo(width, height);
		}

		/// <summary>Changes the size of the window to the specified dimensions.</summary>
		/// <param name="size">A <see cref="T:System.Drawing.Size" /> describing the desired width and height of the window, in pixels. Must be 100 pixels or more in both dimensions.</param>
		/// <exception cref="T:System.UnauthorizedAccessException">The window you are trying to resize is in a different domain than its parent window. This restriction is part of cross-frame scripting security; for more information, see About Cross-Frame Scripting and Security.</exception>
		// Token: 0x06002968 RID: 10600 RVA: 0x000BD779 File Offset: 0x000BB979
		public void ResizeTo(Size size)
		{
			this.NativeHtmlWindow.ResizeTo(size.Width, size.Height);
		}

		/// <summary>Scrolls the window to the designated position.</summary>
		/// <param name="x">The x-coordinate, relative to the top-left corner of the current window, toward which the page should scroll.</param>
		/// <param name="y">The y-coordinate, relative to the top-left corner of the current window, toward which the page should scroll.</param>
		// Token: 0x06002969 RID: 10601 RVA: 0x000BD794 File Offset: 0x000BB994
		public void ScrollTo(int x, int y)
		{
			this.NativeHtmlWindow.ScrollTo(x, y);
		}

		/// <summary>Moves the window to the specified coordinates.</summary>
		/// <param name="point">The x- and y-coordinates, relative to the top-left corner of the current window, toward which the page should scroll.</param>
		// Token: 0x0600296A RID: 10602 RVA: 0x000BD7A3 File Offset: 0x000BB9A3
		public void ScrollTo(Point point)
		{
			this.NativeHtmlWindow.ScrollTo(point.X, point.Y);
		}

		/// <summary>Occurs when script running inside of the window encounters a run-time error.</summary>
		// Token: 0x140001E2 RID: 482
		// (add) Token: 0x0600296B RID: 10603 RVA: 0x000BD7BE File Offset: 0x000BB9BE
		// (remove) Token: 0x0600296C RID: 10604 RVA: 0x000BD7D1 File Offset: 0x000BB9D1
		public event HtmlElementErrorEventHandler Error
		{
			add
			{
				this.WindowShim.AddHandler(HtmlWindow.EventError, value);
			}
			remove
			{
				this.WindowShim.RemoveHandler(HtmlWindow.EventError, value);
			}
		}

		/// <summary>Occurs when the current window obtains user input focus.</summary>
		// Token: 0x140001E3 RID: 483
		// (add) Token: 0x0600296D RID: 10605 RVA: 0x000BD7E4 File Offset: 0x000BB9E4
		// (remove) Token: 0x0600296E RID: 10606 RVA: 0x000BD7F7 File Offset: 0x000BB9F7
		public event HtmlElementEventHandler GotFocus
		{
			add
			{
				this.WindowShim.AddHandler(HtmlWindow.EventGotFocus, value);
			}
			remove
			{
				this.WindowShim.RemoveHandler(HtmlWindow.EventGotFocus, value);
			}
		}

		/// <summary>Occurs when the window's document and all of its elements have finished initializing.</summary>
		// Token: 0x140001E4 RID: 484
		// (add) Token: 0x0600296F RID: 10607 RVA: 0x000BD80A File Offset: 0x000BBA0A
		// (remove) Token: 0x06002970 RID: 10608 RVA: 0x000BD81D File Offset: 0x000BBA1D
		public event HtmlElementEventHandler Load
		{
			add
			{
				this.WindowShim.AddHandler(HtmlWindow.EventLoad, value);
			}
			remove
			{
				this.WindowShim.RemoveHandler(HtmlWindow.EventLoad, value);
			}
		}

		/// <summary>Occurs when user input focus has left the window.</summary>
		// Token: 0x140001E5 RID: 485
		// (add) Token: 0x06002971 RID: 10609 RVA: 0x000BD830 File Offset: 0x000BBA30
		// (remove) Token: 0x06002972 RID: 10610 RVA: 0x000BD843 File Offset: 0x000BBA43
		public event HtmlElementEventHandler LostFocus
		{
			add
			{
				this.WindowShim.AddHandler(HtmlWindow.EventLostFocus, value);
			}
			remove
			{
				this.WindowShim.RemoveHandler(HtmlWindow.EventLostFocus, value);
			}
		}

		/// <summary>Occurs when the user uses the mouse to change the dimensions of the window.</summary>
		// Token: 0x140001E6 RID: 486
		// (add) Token: 0x06002973 RID: 10611 RVA: 0x000BD856 File Offset: 0x000BBA56
		// (remove) Token: 0x06002974 RID: 10612 RVA: 0x000BD869 File Offset: 0x000BBA69
		public event HtmlElementEventHandler Resize
		{
			add
			{
				this.WindowShim.AddHandler(HtmlWindow.EventResize, value);
			}
			remove
			{
				this.WindowShim.RemoveHandler(HtmlWindow.EventResize, value);
			}
		}

		/// <summary>Occurs when the user scrolls through the window to view off-screen text.</summary>
		// Token: 0x140001E7 RID: 487
		// (add) Token: 0x06002975 RID: 10613 RVA: 0x000BD87C File Offset: 0x000BBA7C
		// (remove) Token: 0x06002976 RID: 10614 RVA: 0x000BD88F File Offset: 0x000BBA8F
		public event HtmlElementEventHandler Scroll
		{
			add
			{
				this.WindowShim.AddHandler(HtmlWindow.EventScroll, value);
			}
			remove
			{
				this.WindowShim.RemoveHandler(HtmlWindow.EventScroll, value);
			}
		}

		/// <summary>Occurs when the current page is unloading, and a new page is about to be displayed.</summary>
		// Token: 0x140001E8 RID: 488
		// (add) Token: 0x06002977 RID: 10615 RVA: 0x000BD8A2 File Offset: 0x000BBAA2
		// (remove) Token: 0x06002978 RID: 10616 RVA: 0x000BD8B5 File Offset: 0x000BBAB5
		public event HtmlElementEventHandler Unload
		{
			add
			{
				this.WindowShim.AddHandler(HtmlWindow.EventUnload, value);
			}
			remove
			{
				this.WindowShim.RemoveHandler(HtmlWindow.EventUnload, value);
			}
		}

		/// <summary>Tests the two <see cref="T:System.Windows.Forms.HtmlWindow" /> objects for equality.</summary>
		/// <param name="left">The first <see cref="T:System.Windows.Forms.HtmlWindow" /> object.</param>
		/// <param name="right">The second <see cref="T:System.Windows.Forms.HtmlWindow" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if both parameters are <see langword="null" />, or if both elements have the same underlying COM interface; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002979 RID: 10617 RVA: 0x000BD8C8 File Offset: 0x000BBAC8
		public static bool operator ==(HtmlWindow left, HtmlWindow right)
		{
			if (left == null != (right == null))
			{
				return false;
			}
			if (left == null)
			{
				return true;
			}
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			bool flag;
			try
			{
				intPtr = Marshal.GetIUnknownForObject(left.NativeHtmlWindow);
				intPtr2 = Marshal.GetIUnknownForObject(right.NativeHtmlWindow);
				flag = intPtr == intPtr2;
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.Release(intPtr);
				}
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.Release(intPtr2);
				}
			}
			return flag;
		}

		/// <summary>Tests two <see langword="HtmlWindow" /> objects for inequality.</summary>
		/// <param name="left">The first <see cref="T:System.Windows.Forms.HtmlWindow" /> object.</param>
		/// <param name="right">The second <see cref="T:System.Windows.Forms.HtmlWindow" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if one but not both of the objects is <see langword="null" />, or the underlying COM pointers do not match; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600297A RID: 10618 RVA: 0x000BD950 File Offset: 0x000BBB50
		public static bool operator !=(HtmlWindow left, HtmlWindow right)
		{
			return !(left == right);
		}

		/// <summary>Serves as a hash function for a particular type.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Windows.Forms.HtmlWindow" />.</returns>
		// Token: 0x0600297B RID: 10619 RVA: 0x000BD95C File Offset: 0x000BBB5C
		public override int GetHashCode()
		{
			if (this.htmlWindow2 != null)
			{
				return this.htmlWindow2.GetHashCode();
			}
			return 0;
		}

		/// <summary>Tests the object for equality against the current object.</summary>
		/// <param name="obj">The object to test.</param>
		/// <returns>
		///   <see langword="true" /> if the objects are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600297C RID: 10620 RVA: 0x000BD973 File Offset: 0x000BBB73
		public override bool Equals(object obj)
		{
			return this == (HtmlWindow)obj;
		}

		// Token: 0x040010D6 RID: 4310
		internal static readonly object EventError = new object();

		// Token: 0x040010D7 RID: 4311
		internal static readonly object EventGotFocus = new object();

		// Token: 0x040010D8 RID: 4312
		internal static readonly object EventLoad = new object();

		// Token: 0x040010D9 RID: 4313
		internal static readonly object EventLostFocus = new object();

		// Token: 0x040010DA RID: 4314
		internal static readonly object EventResize = new object();

		// Token: 0x040010DB RID: 4315
		internal static readonly object EventScroll = new object();

		// Token: 0x040010DC RID: 4316
		internal static readonly object EventUnload = new object();

		// Token: 0x040010DD RID: 4317
		private HtmlShimManager shimManager;

		// Token: 0x040010DE RID: 4318
		private UnsafeNativeMethods.IHTMLWindow2 htmlWindow2;

		// Token: 0x020006A8 RID: 1704
		[ClassInterface(ClassInterfaceType.None)]
		private class HTMLWindowEvents2 : StandardOleMarshalObject, UnsafeNativeMethods.DHTMLWindowEvents2
		{
			// Token: 0x06006856 RID: 26710 RVA: 0x00184016 File Offset: 0x00182216
			public HTMLWindowEvents2(HtmlWindow htmlWindow)
			{
				this.parent = htmlWindow;
			}

			// Token: 0x06006857 RID: 26711 RVA: 0x00184025 File Offset: 0x00182225
			private void FireEvent(object key, EventArgs e)
			{
				if (this.parent != null)
				{
					this.parent.WindowShim.FireEvent(key, e);
				}
			}

			// Token: 0x06006858 RID: 26712 RVA: 0x00184048 File Offset: 0x00182248
			public void onfocus(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlWindow.EventGotFocus, htmlElementEventArgs);
			}

			// Token: 0x06006859 RID: 26713 RVA: 0x00184074 File Offset: 0x00182274
			public void onblur(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlWindow.EventLostFocus, htmlElementEventArgs);
			}

			// Token: 0x0600685A RID: 26714 RVA: 0x001840A0 File Offset: 0x001822A0
			public bool onerror(string description, string urlString, int line)
			{
				HtmlElementErrorEventArgs htmlElementErrorEventArgs = new HtmlElementErrorEventArgs(description, urlString, line);
				this.FireEvent(HtmlWindow.EventError, htmlElementErrorEventArgs);
				return htmlElementErrorEventArgs.Handled;
			}

			// Token: 0x0600685B RID: 26715 RVA: 0x001840C8 File Offset: 0x001822C8
			public void onload(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlWindow.EventLoad, htmlElementEventArgs);
			}

			// Token: 0x0600685C RID: 26716 RVA: 0x001840F4 File Offset: 0x001822F4
			public void onunload(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlWindow.EventUnload, htmlElementEventArgs);
				if (this.parent != null)
				{
					this.parent.WindowShim.OnWindowUnload();
				}
			}

			// Token: 0x0600685D RID: 26717 RVA: 0x00184140 File Offset: 0x00182340
			public void onscroll(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlWindow.EventScroll, htmlElementEventArgs);
			}

			// Token: 0x0600685E RID: 26718 RVA: 0x0018416C File Offset: 0x0018236C
			public void onresize(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlWindow.EventResize, htmlElementEventArgs);
			}

			// Token: 0x0600685F RID: 26719 RVA: 0x00184198 File Offset: 0x00182398
			public bool onhelp(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006860 RID: 26720 RVA: 0x000070A6 File Offset: 0x000052A6
			public void onbeforeunload(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006861 RID: 26721 RVA: 0x000070A6 File Offset: 0x000052A6
			public void onbeforeprint(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006862 RID: 26722 RVA: 0x000070A6 File Offset: 0x000052A6
			public void onafterprint(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x04003AE4 RID: 15076
			private HtmlWindow parent;
		}

		// Token: 0x020006A9 RID: 1705
		internal class HtmlWindowShim : HtmlShim
		{
			// Token: 0x06006863 RID: 26723 RVA: 0x001841BD File Offset: 0x001823BD
			public HtmlWindowShim(HtmlWindow window)
			{
				this.htmlWindow = window;
			}

			// Token: 0x17001690 RID: 5776
			// (get) Token: 0x06006864 RID: 26724 RVA: 0x001841CC File Offset: 0x001823CC
			public UnsafeNativeMethods.IHTMLWindow2 NativeHtmlWindow
			{
				get
				{
					return this.htmlWindow.NativeHtmlWindow;
				}
			}

			// Token: 0x17001691 RID: 5777
			// (get) Token: 0x06006865 RID: 26725 RVA: 0x001841CC File Offset: 0x001823CC
			public override UnsafeNativeMethods.IHTMLWindow2 AssociatedWindow
			{
				get
				{
					return this.htmlWindow.NativeHtmlWindow;
				}
			}

			// Token: 0x06006866 RID: 26726 RVA: 0x001841DC File Offset: 0x001823DC
			public override void AttachEventHandler(string eventName, EventHandler eventHandler)
			{
				HtmlToClrEventProxy htmlToClrEventProxy = base.AddEventProxy(eventName, eventHandler);
				bool flag = ((UnsafeNativeMethods.IHTMLWindow3)this.NativeHtmlWindow).AttachEvent(eventName, htmlToClrEventProxy);
			}

			// Token: 0x06006867 RID: 26727 RVA: 0x00184208 File Offset: 0x00182408
			public override void ConnectToEvents()
			{
				if (this.cookie == null || !this.cookie.Connected)
				{
					this.cookie = new AxHost.ConnectionPointCookie(this.NativeHtmlWindow, new HtmlWindow.HTMLWindowEvents2(this.htmlWindow), typeof(UnsafeNativeMethods.DHTMLWindowEvents2), false);
					if (!this.cookie.Connected)
					{
						this.cookie = null;
					}
				}
			}

			// Token: 0x06006868 RID: 26728 RVA: 0x00184268 File Offset: 0x00182468
			public override void DetachEventHandler(string eventName, EventHandler eventHandler)
			{
				HtmlToClrEventProxy htmlToClrEventProxy = base.RemoveEventProxy(eventHandler);
				if (htmlToClrEventProxy != null)
				{
					((UnsafeNativeMethods.IHTMLWindow3)this.NativeHtmlWindow).DetachEvent(eventName, htmlToClrEventProxy);
				}
			}

			// Token: 0x06006869 RID: 26729 RVA: 0x00184292 File Offset: 0x00182492
			public override void DisconnectFromEvents()
			{
				if (this.cookie != null)
				{
					this.cookie.Disconnect();
					this.cookie = null;
				}
			}

			// Token: 0x0600686A RID: 26730 RVA: 0x001842AE File Offset: 0x001824AE
			protected override void Dispose(bool disposing)
			{
				base.Dispose(disposing);
				if (disposing)
				{
					if (this.htmlWindow != null && this.htmlWindow.NativeHtmlWindow != null)
					{
						Marshal.FinalReleaseComObject(this.htmlWindow.NativeHtmlWindow);
					}
					this.htmlWindow = null;
				}
			}

			// Token: 0x0600686B RID: 26731 RVA: 0x001842ED File Offset: 0x001824ED
			protected override object GetEventSender()
			{
				return this.htmlWindow;
			}

			// Token: 0x0600686C RID: 26732 RVA: 0x001842F5 File Offset: 0x001824F5
			public void OnWindowUnload()
			{
				if (this.htmlWindow != null)
				{
					this.htmlWindow.ShimManager.OnWindowUnloaded(this.htmlWindow);
				}
			}

			// Token: 0x04003AE5 RID: 15077
			private AxHost.ConnectionPointCookie cookie;

			// Token: 0x04003AE6 RID: 15078
			private HtmlWindow htmlWindow;
		}
	}
}
