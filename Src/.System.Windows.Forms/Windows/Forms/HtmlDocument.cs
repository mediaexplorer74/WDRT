using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	/// <summary>Provides top-level programmatic access to an HTML document hosted by the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
	// Token: 0x02000278 RID: 632
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class HtmlDocument
	{
		// Token: 0x06002836 RID: 10294 RVA: 0x000BACE4 File Offset: 0x000B8EE4
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		internal HtmlDocument(HtmlShimManager shimManager, UnsafeNativeMethods.IHTMLDocument doc)
		{
			this.htmlDocument2 = (UnsafeNativeMethods.IHTMLDocument2)doc;
			this.shimManager = shimManager;
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06002837 RID: 10295 RVA: 0x000BACFF File Offset: 0x000B8EFF
		internal UnsafeNativeMethods.IHTMLDocument2 NativeHtmlDocument2
		{
			get
			{
				return this.htmlDocument2;
			}
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06002838 RID: 10296 RVA: 0x000BAD08 File Offset: 0x000B8F08
		private HtmlDocument.HtmlDocumentShim DocumentShim
		{
			get
			{
				if (this.ShimManager != null)
				{
					HtmlDocument.HtmlDocumentShim htmlDocumentShim = this.ShimManager.GetDocumentShim(this);
					if (htmlDocumentShim == null)
					{
						this.shimManager.AddDocumentShim(this);
						htmlDocumentShim = this.ShimManager.GetDocumentShim(this);
					}
					return htmlDocumentShim;
				}
				return null;
			}
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06002839 RID: 10297 RVA: 0x000BAD49 File Offset: 0x000B8F49
		private HtmlShimManager ShimManager
		{
			get
			{
				return this.shimManager;
			}
		}

		/// <summary>Provides the <see cref="T:System.Windows.Forms.HtmlElement" /> which currently has user input focus.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.HtmlElement" /> which currently has user input focus.</returns>
		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x0600283A RID: 10298 RVA: 0x000BAD54 File Offset: 0x000B8F54
		public HtmlElement ActiveElement
		{
			get
			{
				UnsafeNativeMethods.IHTMLElement activeElement = this.NativeHtmlDocument2.GetActiveElement();
				if (activeElement == null)
				{
					return null;
				}
				return new HtmlElement(this.ShimManager, activeElement);
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.HtmlElement" /> for the <c>BODY</c> tag.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.HtmlElement" /> object for the <c>BODY</c> tag.</returns>
		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x0600283B RID: 10299 RVA: 0x000BAD80 File Offset: 0x000B8F80
		public HtmlElement Body
		{
			get
			{
				UnsafeNativeMethods.IHTMLElement body = this.NativeHtmlDocument2.GetBody();
				if (body == null)
				{
					return null;
				}
				return new HtmlElement(this.ShimManager, body);
			}
		}

		/// <summary>Gets or sets the string describing the domain of this document for security purposes.</summary>
		/// <returns>A valid domain.</returns>
		/// <exception cref="T:System.ArgumentException">The argument for the <c>Domain</c> property must be a valid domain name using Domain Name System (DNS) conventions.</exception>
		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x0600283C RID: 10300 RVA: 0x000BADAA File Offset: 0x000B8FAA
		// (set) Token: 0x0600283D RID: 10301 RVA: 0x000BADB8 File Offset: 0x000B8FB8
		public string Domain
		{
			get
			{
				return this.NativeHtmlDocument2.GetDomain();
			}
			set
			{
				try
				{
					this.NativeHtmlDocument2.SetDomain(value);
				}
				catch (ArgumentException)
				{
					throw new ArgumentException(SR.GetString("HtmlDocumentInvalidDomain"));
				}
			}
		}

		/// <summary>Gets or sets the text value of the <c>&lt;TITLE&gt;</c> tag in the current HTML document.</summary>
		/// <returns>The title of the current document.</returns>
		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x0600283E RID: 10302 RVA: 0x000BADF4 File Offset: 0x000B8FF4
		// (set) Token: 0x0600283F RID: 10303 RVA: 0x000BAE01 File Offset: 0x000B9001
		public string Title
		{
			get
			{
				return this.NativeHtmlDocument2.GetTitle();
			}
			set
			{
				this.NativeHtmlDocument2.SetTitle(value);
			}
		}

		/// <summary>Gets the URL describing the location of this document.</summary>
		/// <returns>A <see cref="T:System.Uri" /> representing this document's URL.</returns>
		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06002840 RID: 10304 RVA: 0x000BAE10 File Offset: 0x000B9010
		public Uri Url
		{
			get
			{
				UnsafeNativeMethods.IHTMLLocation location = this.NativeHtmlDocument2.GetLocation();
				string text = ((location == null) ? "" : location.GetHref());
				if (!string.IsNullOrEmpty(text))
				{
					return new Uri(text);
				}
				return null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.HtmlWindow" /> associated with this document.</summary>
		/// <returns>The window for this document.</returns>
		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06002841 RID: 10305 RVA: 0x000BAE4C File Offset: 0x000B904C
		public HtmlWindow Window
		{
			get
			{
				UnsafeNativeMethods.IHTMLWindow2 parentWindow = this.NativeHtmlDocument2.GetParentWindow();
				if (parentWindow == null)
				{
					return null;
				}
				return new HtmlWindow(this.ShimManager, parentWindow);
			}
		}

		/// <summary>Gets or sets the background color of the HTML document.</summary>
		/// <returns>The <see cref="T:System.Drawing.Color" /> of the document's background.</returns>
		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06002842 RID: 10306 RVA: 0x000BAE78 File Offset: 0x000B9078
		// (set) Token: 0x06002843 RID: 10307 RVA: 0x000BAEC0 File Offset: 0x000B90C0
		public Color BackColor
		{
			get
			{
				Color color = Color.Empty;
				try
				{
					color = this.ColorFromObject(this.NativeHtmlDocument2.GetBgColor());
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
				}
				return color;
			}
			set
			{
				int num = ((int)value.R << 16) | ((int)value.G << 8) | (int)value.B;
				this.NativeHtmlDocument2.SetBgColor(num);
			}
		}

		/// <summary>Gets or sets the text color for the document.</summary>
		/// <returns>The color of the text in the document.</returns>
		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06002844 RID: 10308 RVA: 0x000BAEFC File Offset: 0x000B90FC
		// (set) Token: 0x06002845 RID: 10309 RVA: 0x000BAF44 File Offset: 0x000B9144
		public Color ForeColor
		{
			get
			{
				Color color = Color.Empty;
				try
				{
					color = this.ColorFromObject(this.NativeHtmlDocument2.GetFgColor());
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
				}
				return color;
			}
			set
			{
				int num = ((int)value.R << 16) | ((int)value.G << 8) | (int)value.B;
				this.NativeHtmlDocument2.SetFgColor(num);
			}
		}

		/// <summary>Gets or sets the color of hyperlinks.</summary>
		/// <returns>The color for hyperlinks in the current document.</returns>
		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06002846 RID: 10310 RVA: 0x000BAF80 File Offset: 0x000B9180
		// (set) Token: 0x06002847 RID: 10311 RVA: 0x000BAFC8 File Offset: 0x000B91C8
		public Color LinkColor
		{
			get
			{
				Color color = Color.Empty;
				try
				{
					color = this.ColorFromObject(this.NativeHtmlDocument2.GetLinkColor());
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
				}
				return color;
			}
			set
			{
				int num = ((int)value.R << 16) | ((int)value.G << 8) | (int)value.B;
				this.NativeHtmlDocument2.SetLinkColor(num);
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Drawing.Color" /> of a hyperlink when clicked by a user.</summary>
		/// <returns>The <see cref="T:System.Drawing.Color" /> for active links.</returns>
		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06002848 RID: 10312 RVA: 0x000BB004 File Offset: 0x000B9204
		// (set) Token: 0x06002849 RID: 10313 RVA: 0x000BB04C File Offset: 0x000B924C
		public Color ActiveLinkColor
		{
			get
			{
				Color color = Color.Empty;
				try
				{
					color = this.ColorFromObject(this.NativeHtmlDocument2.GetAlinkColor());
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
				}
				return color;
			}
			set
			{
				int num = ((int)value.R << 16) | ((int)value.G << 8) | (int)value.B;
				this.NativeHtmlDocument2.SetAlinkColor(num);
			}
		}

		/// <summary>Gets or sets the Color of links to HTML pages that the user has already visited.</summary>
		/// <returns>The color of visited links.</returns>
		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x0600284A RID: 10314 RVA: 0x000BB088 File Offset: 0x000B9288
		// (set) Token: 0x0600284B RID: 10315 RVA: 0x000BB0D0 File Offset: 0x000B92D0
		public Color VisitedLinkColor
		{
			get
			{
				Color color = Color.Empty;
				try
				{
					color = this.ColorFromObject(this.NativeHtmlDocument2.GetVlinkColor());
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
				}
				return color;
			}
			set
			{
				int num = ((int)value.R << 16) | ((int)value.G << 8) | (int)value.B;
				this.NativeHtmlDocument2.SetVlinkColor(num);
			}
		}

		/// <summary>Gets a value indicating whether the document has user input focus.</summary>
		/// <returns>
		///   <see langword="true" /> if the document has focus; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x0600284C RID: 10316 RVA: 0x000BB10B File Offset: 0x000B930B
		public bool Focused
		{
			get
			{
				return ((UnsafeNativeMethods.IHTMLDocument4)this.NativeHtmlDocument2).HasFocus();
			}
		}

		/// <summary>Gets the unmanaged interface pointer for this <see cref="T:System.Windows.Forms.HtmlDocument" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing an <c>IDispatch</c> pointer to the unmanaged document.</returns>
		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x0600284D RID: 10317 RVA: 0x000BB11D File Offset: 0x000B931D
		public object DomDocument
		{
			get
			{
				return this.NativeHtmlDocument2;
			}
		}

		/// <summary>Gets or sets the HTTP cookies associated with this document.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a list of cookies, with each cookie separated by a semicolon.</returns>
		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x0600284E RID: 10318 RVA: 0x000BB125 File Offset: 0x000B9325
		// (set) Token: 0x0600284F RID: 10319 RVA: 0x000BB132 File Offset: 0x000B9332
		public string Cookie
		{
			get
			{
				return this.NativeHtmlDocument2.GetCookie();
			}
			set
			{
				this.NativeHtmlDocument2.SetCookie(value);
			}
		}

		/// <summary>Gets or sets the direction of text in the current document.</summary>
		/// <returns>
		///   <see langword="true" /> if text renders from right to left; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06002850 RID: 10320 RVA: 0x000BB140 File Offset: 0x000B9340
		// (set) Token: 0x06002851 RID: 10321 RVA: 0x000BB15C File Offset: 0x000B935C
		public bool RightToLeft
		{
			get
			{
				return ((UnsafeNativeMethods.IHTMLDocument3)this.NativeHtmlDocument2).GetDir() == "rtl";
			}
			set
			{
				((UnsafeNativeMethods.IHTMLDocument3)this.NativeHtmlDocument2).SetDir(value ? "rtl" : "ltr");
			}
		}

		/// <summary>Gets or sets the character encoding for this document.</summary>
		/// <returns>The <see cref="T:System.String" /> representing the current character encoding.</returns>
		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06002852 RID: 10322 RVA: 0x000BB17D File Offset: 0x000B937D
		// (set) Token: 0x06002853 RID: 10323 RVA: 0x000BB18A File Offset: 0x000B938A
		public string Encoding
		{
			get
			{
				return this.NativeHtmlDocument2.GetCharset();
			}
			set
			{
				this.NativeHtmlDocument2.SetCharset(value);
			}
		}

		/// <summary>Gets the encoding used by default for the current document.</summary>
		/// <returns>The <see cref="T:System.String" /> representing the encoding that the browser uses when the page is first displayed.</returns>
		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06002854 RID: 10324 RVA: 0x000BB198 File Offset: 0x000B9398
		public string DefaultEncoding
		{
			get
			{
				return this.NativeHtmlDocument2.GetDefaultCharset();
			}
		}

		/// <summary>Gets an instance of <see cref="T:System.Windows.Forms.HtmlElementCollection" />, which stores all <see cref="T:System.Windows.Forms.HtmlElement" /> objects for the document.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.HtmlElementCollection" /> of all elements in the document.</returns>
		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06002855 RID: 10325 RVA: 0x000BB1A8 File Offset: 0x000B93A8
		public HtmlElementCollection All
		{
			get
			{
				UnsafeNativeMethods.IHTMLElementCollection all = this.NativeHtmlDocument2.GetAll();
				if (all == null)
				{
					return new HtmlElementCollection(this.ShimManager);
				}
				return new HtmlElementCollection(this.ShimManager, all);
			}
		}

		/// <summary>Gets a list of all the hyperlinks within this HTML document.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.HtmlElementCollection" /> of <see cref="T:System.Windows.Forms.HtmlElement" /> objects.</returns>
		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06002856 RID: 10326 RVA: 0x000BB1DC File Offset: 0x000B93DC
		public HtmlElementCollection Links
		{
			get
			{
				UnsafeNativeMethods.IHTMLElementCollection links = this.NativeHtmlDocument2.GetLinks();
				if (links == null)
				{
					return new HtmlElementCollection(this.ShimManager);
				}
				return new HtmlElementCollection(this.ShimManager, links);
			}
		}

		/// <summary>Gets a collection of all image tags in the document.</summary>
		/// <returns>A collection of <see cref="T:System.Windows.Forms.HtmlElement" /> objects, one for each IMG tag in the document. Elements are returned from the collection in source order.</returns>
		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06002857 RID: 10327 RVA: 0x000BB210 File Offset: 0x000B9410
		public HtmlElementCollection Images
		{
			get
			{
				UnsafeNativeMethods.IHTMLElementCollection images = this.NativeHtmlDocument2.GetImages();
				if (images == null)
				{
					return new HtmlElementCollection(this.ShimManager);
				}
				return new HtmlElementCollection(this.ShimManager, images);
			}
		}

		/// <summary>Gets a collection of all of the <c>&lt;FORM&gt;</c> elements in the document.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.HtmlElementCollection" /> of the <c>&lt;FORM&gt;</c> elements within the document.</returns>
		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06002858 RID: 10328 RVA: 0x000BB244 File Offset: 0x000B9444
		public HtmlElementCollection Forms
		{
			get
			{
				UnsafeNativeMethods.IHTMLElementCollection forms = this.NativeHtmlDocument2.GetForms();
				if (forms == null)
				{
					return new HtmlElementCollection(this.ShimManager);
				}
				return new HtmlElementCollection(this.ShimManager, forms);
			}
		}

		/// <summary>Writes a new HTML page.</summary>
		/// <param name="text">The HTML text to write into the document.</param>
		// Token: 0x06002859 RID: 10329 RVA: 0x000BB278 File Offset: 0x000B9478
		public void Write(string text)
		{
			object[] array = new object[] { text };
			this.NativeHtmlDocument2.Write(array);
		}

		/// <summary>Executes the specified command against the document.</summary>
		/// <param name="command">The name of the command to execute.</param>
		/// <param name="showUI">Whether or not to show command-specific dialog boxes or message boxes to the user.</param>
		/// <param name="value">The value to assign using the command. Not applicable for all commands.</param>
		// Token: 0x0600285A RID: 10330 RVA: 0x000BB29D File Offset: 0x000B949D
		public void ExecCommand(string command, bool showUI, object value)
		{
			this.NativeHtmlDocument2.ExecCommand(command, showUI, value);
		}

		/// <summary>Sets user input focus on the current document.</summary>
		// Token: 0x0600285B RID: 10331 RVA: 0x000BB2AE File Offset: 0x000B94AE
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void Focus()
		{
			((UnsafeNativeMethods.IHTMLDocument4)this.NativeHtmlDocument2).Focus();
			((UnsafeNativeMethods.IHTMLDocument4)this.NativeHtmlDocument2).Focus();
		}

		/// <summary>Retrieves a single <see cref="T:System.Windows.Forms.HtmlElement" /> using the element's <c>ID</c> attribute as a search key.</summary>
		/// <param name="id">The ID attribute of the element to retrieve.</param>
		/// <returns>Returns the first object with the same <c>ID</c> attribute as the specified value, or <see langword="null" /> if the <paramref name="id" /> cannot be found.</returns>
		// Token: 0x0600285C RID: 10332 RVA: 0x000BB2D0 File Offset: 0x000B94D0
		public HtmlElement GetElementById(string id)
		{
			UnsafeNativeMethods.IHTMLElement elementById = ((UnsafeNativeMethods.IHTMLDocument3)this.NativeHtmlDocument2).GetElementById(id);
			if (elementById == null)
			{
				return null;
			}
			return new HtmlElement(this.ShimManager, elementById);
		}

		/// <summary>Retrieves the HTML element located at the specified client coordinates.</summary>
		/// <param name="point">The x,y position of the element on the screen, relative to the top-left corner of the document.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.HtmlElement" /> at the specified screen location in the document.</returns>
		// Token: 0x0600285D RID: 10333 RVA: 0x000BB300 File Offset: 0x000B9500
		public HtmlElement GetElementFromPoint(Point point)
		{
			UnsafeNativeMethods.IHTMLElement ihtmlelement = this.NativeHtmlDocument2.ElementFromPoint(point.X, point.Y);
			if (ihtmlelement == null)
			{
				return null;
			}
			return new HtmlElement(this.ShimManager, ihtmlelement);
		}

		/// <summary>Retrieve a collection of elements with the specified HTML tag.</summary>
		/// <param name="tagName">The name of the HTML tag for the <see cref="T:System.Windows.Forms.HtmlElement" /> objects you want to retrieve.</param>
		/// <returns>The collection of elements who tag name is equal to the <paramref name="tagName" /> argument.</returns>
		// Token: 0x0600285E RID: 10334 RVA: 0x000BB338 File Offset: 0x000B9538
		public HtmlElementCollection GetElementsByTagName(string tagName)
		{
			UnsafeNativeMethods.IHTMLElementCollection elementsByTagName = ((UnsafeNativeMethods.IHTMLDocument3)this.NativeHtmlDocument2).GetElementsByTagName(tagName);
			if (elementsByTagName == null)
			{
				return new HtmlElementCollection(this.ShimManager);
			}
			return new HtmlElementCollection(this.ShimManager, elementsByTagName);
		}

		/// <summary>Gets a new <see cref="T:System.Windows.Forms.HtmlDocument" /> to use with the <see cref="M:System.Windows.Forms.HtmlDocument.Write(System.String)" /> method.</summary>
		/// <param name="replaceInHistory">Whether the new window's navigation should replace the previous element in the navigation history of the DOM.</param>
		/// <returns>A new document for writing.</returns>
		// Token: 0x0600285F RID: 10335 RVA: 0x000BB374 File Offset: 0x000B9574
		public HtmlDocument OpenNew(bool replaceInHistory)
		{
			object obj = (replaceInHistory ? "replace" : "");
			object obj2 = null;
			object obj3 = this.NativeHtmlDocument2.Open("text/html", obj, obj2, obj2);
			UnsafeNativeMethods.IHTMLDocument ihtmldocument = obj3 as UnsafeNativeMethods.IHTMLDocument;
			if (ihtmldocument == null)
			{
				return null;
			}
			return new HtmlDocument(this.ShimManager, ihtmldocument);
		}

		/// <summary>Creates a new <see langword="HtmlElement" /> of the specified HTML tag type.</summary>
		/// <param name="elementTag">The name of the HTML element to create.</param>
		/// <returns>A new element of the specified tag type.</returns>
		// Token: 0x06002860 RID: 10336 RVA: 0x000BB3C0 File Offset: 0x000B95C0
		public HtmlElement CreateElement(string elementTag)
		{
			UnsafeNativeMethods.IHTMLElement ihtmlelement = this.NativeHtmlDocument2.CreateElement(elementTag);
			if (ihtmlelement == null)
			{
				return null;
			}
			return new HtmlElement(this.ShimManager, ihtmlelement);
		}

		/// <summary>Executes an Active Scripting function defined in an HTML page.</summary>
		/// <param name="scriptName">The name of the script method to invoke.</param>
		/// <param name="args">The arguments to pass to the script method.</param>
		/// <returns>The object returned by the Active Scripting call.</returns>
		// Token: 0x06002861 RID: 10337 RVA: 0x000BB3EC File Offset: 0x000B95EC
		public object InvokeScript(string scriptName, object[] args)
		{
			object obj = null;
			NativeMethods.tagDISPPARAMS tagDISPPARAMS = new NativeMethods.tagDISPPARAMS();
			tagDISPPARAMS.rgvarg = IntPtr.Zero;
			try
			{
				UnsafeNativeMethods.IDispatch dispatch = this.NativeHtmlDocument2.GetScript() as UnsafeNativeMethods.IDispatch;
				if (dispatch != null)
				{
					Guid empty = Guid.Empty;
					string[] array = new string[] { scriptName };
					int[] array2 = new int[] { -1 };
					int idsOfNames = dispatch.GetIDsOfNames(ref empty, array, 1, SafeNativeMethods.GetThreadLCID(), array2);
					if (NativeMethods.Succeeded(idsOfNames) && array2[0] != -1)
					{
						if (args != null)
						{
							Array.Reverse(args);
						}
						tagDISPPARAMS.rgvarg = ((args == null) ? IntPtr.Zero : HtmlDocument.ArrayToVARIANTVector(args));
						tagDISPPARAMS.cArgs = ((args == null) ? 0 : args.Length);
						tagDISPPARAMS.rgdispidNamedArgs = IntPtr.Zero;
						tagDISPPARAMS.cNamedArgs = 0;
						object[] array3 = new object[1];
						if (dispatch.Invoke(array2[0], ref empty, SafeNativeMethods.GetThreadLCID(), 1, tagDISPPARAMS, array3, new NativeMethods.tagEXCEPINFO(), null) == 0)
						{
							obj = array3[0];
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsSecurityOrCriticalException(ex))
				{
					throw;
				}
			}
			finally
			{
				if (tagDISPPARAMS.rgvarg != IntPtr.Zero)
				{
					HtmlDocument.FreeVARIANTVector(tagDISPPARAMS.rgvarg, args.Length);
				}
			}
			return obj;
		}

		/// <summary>Executes an Active Scripting function defined in an HTML page.</summary>
		/// <param name="scriptName">The name of the script method to invoke.</param>
		/// <returns>The object returned by the Active Scripting call.</returns>
		// Token: 0x06002862 RID: 10338 RVA: 0x000BB524 File Offset: 0x000B9724
		public object InvokeScript(string scriptName)
		{
			return this.InvokeScript(scriptName, null);
		}

		/// <summary>Adds an event handler for the named HTML DOM event.</summary>
		/// <param name="eventName">The name of the event you want to handle.</param>
		/// <param name="eventHandler">The managed code that handles the event.</param>
		// Token: 0x06002863 RID: 10339 RVA: 0x000BB530 File Offset: 0x000B9730
		public void AttachEventHandler(string eventName, EventHandler eventHandler)
		{
			HtmlDocument.HtmlDocumentShim documentShim = this.DocumentShim;
			if (documentShim != null)
			{
				documentShim.AttachEventHandler(eventName, eventHandler);
			}
		}

		/// <summary>Removes an event handler from a named event on the HTML DOM.</summary>
		/// <param name="eventName">The name of the event you want to cease handling.</param>
		/// <param name="eventHandler">The managed code that handles the event.</param>
		// Token: 0x06002864 RID: 10340 RVA: 0x000BB550 File Offset: 0x000B9750
		public void DetachEventHandler(string eventName, EventHandler eventHandler)
		{
			HtmlDocument.HtmlDocumentShim documentShim = this.DocumentShim;
			if (documentShim != null)
			{
				documentShim.DetachEventHandler(eventName, eventHandler);
			}
		}

		/// <summary>Occurs when the user clicks anywhere on the document.</summary>
		// Token: 0x140001C5 RID: 453
		// (add) Token: 0x06002865 RID: 10341 RVA: 0x000BB56F File Offset: 0x000B976F
		// (remove) Token: 0x06002866 RID: 10342 RVA: 0x000BB582 File Offset: 0x000B9782
		public event HtmlElementEventHandler Click
		{
			add
			{
				this.DocumentShim.AddHandler(HtmlDocument.EventClick, value);
			}
			remove
			{
				this.DocumentShim.RemoveHandler(HtmlDocument.EventClick, value);
			}
		}

		/// <summary>Occurs when the user requests to display the document's context menu.</summary>
		// Token: 0x140001C6 RID: 454
		// (add) Token: 0x06002867 RID: 10343 RVA: 0x000BB595 File Offset: 0x000B9795
		// (remove) Token: 0x06002868 RID: 10344 RVA: 0x000BB5A8 File Offset: 0x000B97A8
		public event HtmlElementEventHandler ContextMenuShowing
		{
			add
			{
				this.DocumentShim.AddHandler(HtmlDocument.EventContextMenuShowing, value);
			}
			remove
			{
				this.DocumentShim.RemoveHandler(HtmlDocument.EventContextMenuShowing, value);
			}
		}

		/// <summary>Occurs before focus is given to the document.</summary>
		// Token: 0x140001C7 RID: 455
		// (add) Token: 0x06002869 RID: 10345 RVA: 0x000BB5BB File Offset: 0x000B97BB
		// (remove) Token: 0x0600286A RID: 10346 RVA: 0x000BB5CE File Offset: 0x000B97CE
		public event HtmlElementEventHandler Focusing
		{
			add
			{
				this.DocumentShim.AddHandler(HtmlDocument.EventFocusing, value);
			}
			remove
			{
				this.DocumentShim.RemoveHandler(HtmlDocument.EventFocusing, value);
			}
		}

		/// <summary>Occurs while focus is leaving a control.</summary>
		// Token: 0x140001C8 RID: 456
		// (add) Token: 0x0600286B RID: 10347 RVA: 0x000BB5E1 File Offset: 0x000B97E1
		// (remove) Token: 0x0600286C RID: 10348 RVA: 0x000BB5F4 File Offset: 0x000B97F4
		public event HtmlElementEventHandler LosingFocus
		{
			add
			{
				this.DocumentShim.AddHandler(HtmlDocument.EventLosingFocus, value);
			}
			remove
			{
				this.DocumentShim.RemoveHandler(HtmlDocument.EventLosingFocus, value);
			}
		}

		/// <summary>Occurs when the user clicks the left mouse button.</summary>
		// Token: 0x140001C9 RID: 457
		// (add) Token: 0x0600286D RID: 10349 RVA: 0x000BB607 File Offset: 0x000B9807
		// (remove) Token: 0x0600286E RID: 10350 RVA: 0x000BB61A File Offset: 0x000B981A
		public event HtmlElementEventHandler MouseDown
		{
			add
			{
				this.DocumentShim.AddHandler(HtmlDocument.EventMouseDown, value);
			}
			remove
			{
				this.DocumentShim.RemoveHandler(HtmlDocument.EventMouseDown, value);
			}
		}

		/// <summary>Occurs when the mouse is no longer hovering over the document.</summary>
		// Token: 0x140001CA RID: 458
		// (add) Token: 0x0600286F RID: 10351 RVA: 0x000BB62D File Offset: 0x000B982D
		// (remove) Token: 0x06002870 RID: 10352 RVA: 0x000BB640 File Offset: 0x000B9840
		public event HtmlElementEventHandler MouseLeave
		{
			add
			{
				this.DocumentShim.AddHandler(HtmlDocument.EventMouseLeave, value);
			}
			remove
			{
				this.DocumentShim.RemoveHandler(HtmlDocument.EventMouseLeave, value);
			}
		}

		/// <summary>Occurs when the mouse is moved over the document.</summary>
		// Token: 0x140001CB RID: 459
		// (add) Token: 0x06002871 RID: 10353 RVA: 0x000BB653 File Offset: 0x000B9853
		// (remove) Token: 0x06002872 RID: 10354 RVA: 0x000BB666 File Offset: 0x000B9866
		public event HtmlElementEventHandler MouseMove
		{
			add
			{
				this.DocumentShim.AddHandler(HtmlDocument.EventMouseMove, value);
			}
			remove
			{
				this.DocumentShim.RemoveHandler(HtmlDocument.EventMouseMove, value);
			}
		}

		/// <summary>Occurs when the mouse is moved over the document.</summary>
		// Token: 0x140001CC RID: 460
		// (add) Token: 0x06002873 RID: 10355 RVA: 0x000BB679 File Offset: 0x000B9879
		// (remove) Token: 0x06002874 RID: 10356 RVA: 0x000BB68C File Offset: 0x000B988C
		public event HtmlElementEventHandler MouseOver
		{
			add
			{
				this.DocumentShim.AddHandler(HtmlDocument.EventMouseOver, value);
			}
			remove
			{
				this.DocumentShim.RemoveHandler(HtmlDocument.EventMouseOver, value);
			}
		}

		/// <summary>Occurs when the user releases the left mouse button.</summary>
		// Token: 0x140001CD RID: 461
		// (add) Token: 0x06002875 RID: 10357 RVA: 0x000BB69F File Offset: 0x000B989F
		// (remove) Token: 0x06002876 RID: 10358 RVA: 0x000BB6B2 File Offset: 0x000B98B2
		public event HtmlElementEventHandler MouseUp
		{
			add
			{
				this.DocumentShim.AddHandler(HtmlDocument.EventMouseUp, value);
			}
			remove
			{
				this.DocumentShim.RemoveHandler(HtmlDocument.EventMouseUp, value);
			}
		}

		/// <summary>Occurs when navigation to another Web page is halted.</summary>
		// Token: 0x140001CE RID: 462
		// (add) Token: 0x06002877 RID: 10359 RVA: 0x000BB6C5 File Offset: 0x000B98C5
		// (remove) Token: 0x06002878 RID: 10360 RVA: 0x000BB6D8 File Offset: 0x000B98D8
		public event HtmlElementEventHandler Stop
		{
			add
			{
				this.DocumentShim.AddHandler(HtmlDocument.EventStop, value);
			}
			remove
			{
				this.DocumentShim.RemoveHandler(HtmlDocument.EventStop, value);
			}
		}

		// Token: 0x06002879 RID: 10361 RVA: 0x000BB6EC File Offset: 0x000B98EC
		internal unsafe static IntPtr ArrayToVARIANTVector(object[] args)
		{
			int num = args.Length;
			IntPtr intPtr = Marshal.AllocCoTaskMem(num * HtmlDocument.VariantSize);
			byte* ptr = (byte*)(void*)intPtr;
			for (int i = 0; i < num; i++)
			{
				Marshal.GetNativeVariantForObject(args[i], (IntPtr)((void*)(ptr + HtmlDocument.VariantSize * i)));
			}
			return intPtr;
		}

		// Token: 0x0600287A RID: 10362 RVA: 0x000BB734 File Offset: 0x000B9934
		internal unsafe static void FreeVARIANTVector(IntPtr mem, int len)
		{
			byte* ptr = (byte*)(void*)mem;
			for (int i = 0; i < len; i++)
			{
				SafeNativeMethods.VariantClear(new HandleRef(null, (IntPtr)((void*)(ptr + HtmlDocument.VariantSize * i))));
			}
			Marshal.FreeCoTaskMem(mem);
		}

		// Token: 0x0600287B RID: 10363 RVA: 0x000BB774 File Offset: 0x000B9974
		private Color ColorFromObject(object oColor)
		{
			try
			{
				if (oColor is string)
				{
					string text = oColor as string;
					int num = text.IndexOf('#');
					if (num >= 0)
					{
						string text2 = text.Substring(num + 1);
						return Color.FromArgb(255, Color.FromArgb(int.Parse(text2, NumberStyles.HexNumber, CultureInfo.InvariantCulture)));
					}
					return Color.FromName(text);
				}
				else if (oColor is int)
				{
					return Color.FromArgb(255, Color.FromArgb((int)oColor));
				}
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsSecurityOrCriticalException(ex))
				{
					throw;
				}
			}
			return Color.Empty;
		}

		/// <summary>Returns a value that indicates whether the specified <see cref="T:System.Windows.Forms.HtmlDocument" /> instances represent the same value.</summary>
		/// <param name="left">The first instance to compare.</param>
		/// <param name="right">The second instance to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the specified instances are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600287C RID: 10364 RVA: 0x000BB81C File Offset: 0x000B9A1C
		public static bool operator ==(HtmlDocument left, HtmlDocument right)
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
				intPtr = Marshal.GetIUnknownForObject(left.NativeHtmlDocument2);
				intPtr2 = Marshal.GetIUnknownForObject(right.NativeHtmlDocument2);
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

		/// <summary>Returns a value that indicates whether the specified <see cref="T:System.Windows.Forms.HtmlDocument" /> instances do not represent the same value.</summary>
		/// <param name="left">The first instance to compare.</param>
		/// <param name="right">The second instance to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the specified instances are not equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600287D RID: 10365 RVA: 0x000BB8A4 File Offset: 0x000B9AA4
		public static bool operator !=(HtmlDocument left, HtmlDocument right)
		{
			return !(left == right);
		}

		/// <summary>Retrieves the hash code for this object.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing an in-memory hash of this object.</returns>
		// Token: 0x0600287E RID: 10366 RVA: 0x000BB8B0 File Offset: 0x000B9AB0
		public override int GetHashCode()
		{
			if (this.htmlDocument2 != null)
			{
				return this.htmlDocument2.GetHashCode();
			}
			return 0;
		}

		/// <summary>Tests the object for equality against the current object.</summary>
		/// <param name="obj">The object to test.</param>
		/// <returns>
		///   <see langword="true" /> if the objects are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600287F RID: 10367 RVA: 0x000BB8C7 File Offset: 0x000B9AC7
		public override bool Equals(object obj)
		{
			return this == (HtmlDocument)obj;
		}

		// Token: 0x04001099 RID: 4249
		internal static object EventClick = new object();

		// Token: 0x0400109A RID: 4250
		internal static object EventContextMenuShowing = new object();

		// Token: 0x0400109B RID: 4251
		internal static object EventFocusing = new object();

		// Token: 0x0400109C RID: 4252
		internal static object EventLosingFocus = new object();

		// Token: 0x0400109D RID: 4253
		internal static object EventMouseDown = new object();

		// Token: 0x0400109E RID: 4254
		internal static object EventMouseLeave = new object();

		// Token: 0x0400109F RID: 4255
		internal static object EventMouseMove = new object();

		// Token: 0x040010A0 RID: 4256
		internal static object EventMouseOver = new object();

		// Token: 0x040010A1 RID: 4257
		internal static object EventMouseUp = new object();

		// Token: 0x040010A2 RID: 4258
		internal static object EventStop = new object();

		// Token: 0x040010A3 RID: 4259
		private UnsafeNativeMethods.IHTMLDocument2 htmlDocument2;

		// Token: 0x040010A4 RID: 4260
		private HtmlShimManager shimManager;

		// Token: 0x040010A5 RID: 4261
		private static readonly int VariantSize = (int)Marshal.OffsetOf(typeof(HtmlDocument.FindSizeOfVariant), "b");

		// Token: 0x020006A3 RID: 1699
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct FindSizeOfVariant
		{
			// Token: 0x04003AD9 RID: 15065
			[MarshalAs(UnmanagedType.Struct)]
			public object var;

			// Token: 0x04003ADA RID: 15066
			public byte b;
		}

		// Token: 0x020006A4 RID: 1700
		internal class HtmlDocumentShim : HtmlShim
		{
			// Token: 0x060067CE RID: 26574 RVA: 0x001830AC File Offset: 0x001812AC
			internal HtmlDocumentShim(HtmlDocument htmlDocument)
			{
				this.htmlDocument = htmlDocument;
				if (this.htmlDocument != null)
				{
					HtmlWindow window = htmlDocument.Window;
					if (window != null)
					{
						this.associatedWindow = window.NativeHtmlWindow;
					}
				}
			}

			// Token: 0x1700168A RID: 5770
			// (get) Token: 0x060067CF RID: 26575 RVA: 0x001830F0 File Offset: 0x001812F0
			public override UnsafeNativeMethods.IHTMLWindow2 AssociatedWindow
			{
				get
				{
					return this.associatedWindow;
				}
			}

			// Token: 0x1700168B RID: 5771
			// (get) Token: 0x060067D0 RID: 26576 RVA: 0x001830F8 File Offset: 0x001812F8
			public UnsafeNativeMethods.IHTMLDocument2 NativeHtmlDocument2
			{
				get
				{
					return this.htmlDocument.NativeHtmlDocument2;
				}
			}

			// Token: 0x1700168C RID: 5772
			// (get) Token: 0x060067D1 RID: 26577 RVA: 0x00183105 File Offset: 0x00181305
			internal HtmlDocument Document
			{
				get
				{
					return this.htmlDocument;
				}
			}

			// Token: 0x060067D2 RID: 26578 RVA: 0x00183110 File Offset: 0x00181310
			public override void AttachEventHandler(string eventName, EventHandler eventHandler)
			{
				HtmlToClrEventProxy htmlToClrEventProxy = base.AddEventProxy(eventName, eventHandler);
				bool flag = ((UnsafeNativeMethods.IHTMLDocument3)this.NativeHtmlDocument2).AttachEvent(eventName, htmlToClrEventProxy);
			}

			// Token: 0x060067D3 RID: 26579 RVA: 0x0018313C File Offset: 0x0018133C
			public override void DetachEventHandler(string eventName, EventHandler eventHandler)
			{
				HtmlToClrEventProxy htmlToClrEventProxy = base.RemoveEventProxy(eventHandler);
				if (htmlToClrEventProxy != null)
				{
					((UnsafeNativeMethods.IHTMLDocument3)this.NativeHtmlDocument2).DetachEvent(eventName, htmlToClrEventProxy);
				}
			}

			// Token: 0x060067D4 RID: 26580 RVA: 0x00183168 File Offset: 0x00181368
			public override void ConnectToEvents()
			{
				if (this.cookie == null || !this.cookie.Connected)
				{
					this.cookie = new AxHost.ConnectionPointCookie(this.NativeHtmlDocument2, new HtmlDocument.HTMLDocumentEvents2(this.htmlDocument), typeof(UnsafeNativeMethods.DHTMLDocumentEvents2), false);
					if (!this.cookie.Connected)
					{
						this.cookie = null;
					}
				}
			}

			// Token: 0x060067D5 RID: 26581 RVA: 0x001831C5 File Offset: 0x001813C5
			public override void DisconnectFromEvents()
			{
				if (this.cookie != null)
				{
					this.cookie.Disconnect();
					this.cookie = null;
				}
			}

			// Token: 0x060067D6 RID: 26582 RVA: 0x001831E1 File Offset: 0x001813E1
			protected override void Dispose(bool disposing)
			{
				base.Dispose(disposing);
				if (disposing)
				{
					if (this.htmlDocument != null)
					{
						Marshal.FinalReleaseComObject(this.htmlDocument.NativeHtmlDocument2);
					}
					this.htmlDocument = null;
				}
			}

			// Token: 0x060067D7 RID: 26583 RVA: 0x00183105 File Offset: 0x00181305
			protected override object GetEventSender()
			{
				return this.htmlDocument;
			}

			// Token: 0x04003ADB RID: 15067
			private AxHost.ConnectionPointCookie cookie;

			// Token: 0x04003ADC RID: 15068
			private HtmlDocument htmlDocument;

			// Token: 0x04003ADD RID: 15069
			private UnsafeNativeMethods.IHTMLWindow2 associatedWindow;
		}

		// Token: 0x020006A5 RID: 1701
		[ClassInterface(ClassInterfaceType.None)]
		private class HTMLDocumentEvents2 : StandardOleMarshalObject, UnsafeNativeMethods.DHTMLDocumentEvents2
		{
			// Token: 0x060067D8 RID: 26584 RVA: 0x00183213 File Offset: 0x00181413
			public HTMLDocumentEvents2(HtmlDocument htmlDocument)
			{
				this.parent = htmlDocument;
			}

			// Token: 0x060067D9 RID: 26585 RVA: 0x00183222 File Offset: 0x00181422
			private void FireEvent(object key, EventArgs e)
			{
				if (this.parent != null)
				{
					this.parent.DocumentShim.FireEvent(key, e);
				}
			}

			// Token: 0x060067DA RID: 26586 RVA: 0x00183244 File Offset: 0x00181444
			public bool onclick(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlDocument.EventClick, htmlElementEventArgs);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x060067DB RID: 26587 RVA: 0x00183278 File Offset: 0x00181478
			public bool oncontextmenu(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlDocument.EventContextMenuShowing, htmlElementEventArgs);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x060067DC RID: 26588 RVA: 0x001832AC File Offset: 0x001814AC
			public void onfocusin(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlDocument.EventFocusing, htmlElementEventArgs);
			}

			// Token: 0x060067DD RID: 26589 RVA: 0x001832D8 File Offset: 0x001814D8
			public void onfocusout(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlDocument.EventLosingFocus, htmlElementEventArgs);
			}

			// Token: 0x060067DE RID: 26590 RVA: 0x00183304 File Offset: 0x00181504
			public void onmousemove(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlDocument.EventMouseMove, htmlElementEventArgs);
			}

			// Token: 0x060067DF RID: 26591 RVA: 0x00183330 File Offset: 0x00181530
			public void onmousedown(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlDocument.EventMouseDown, htmlElementEventArgs);
			}

			// Token: 0x060067E0 RID: 26592 RVA: 0x0018335C File Offset: 0x0018155C
			public void onmouseout(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlDocument.EventMouseLeave, htmlElementEventArgs);
			}

			// Token: 0x060067E1 RID: 26593 RVA: 0x00183388 File Offset: 0x00181588
			public void onmouseover(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlDocument.EventMouseOver, htmlElementEventArgs);
			}

			// Token: 0x060067E2 RID: 26594 RVA: 0x001833B4 File Offset: 0x001815B4
			public void onmouseup(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlDocument.EventMouseUp, htmlElementEventArgs);
			}

			// Token: 0x060067E3 RID: 26595 RVA: 0x001833E0 File Offset: 0x001815E0
			public bool onstop(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlDocument.EventStop, htmlElementEventArgs);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x060067E4 RID: 26596 RVA: 0x00183414 File Offset: 0x00181614
			public bool onhelp(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x060067E5 RID: 26597 RVA: 0x0018343C File Offset: 0x0018163C
			public bool ondblclick(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x060067E6 RID: 26598 RVA: 0x000070A6 File Offset: 0x000052A6
			public void onkeydown(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x060067E7 RID: 26599 RVA: 0x000070A6 File Offset: 0x000052A6
			public void onkeyup(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x060067E8 RID: 26600 RVA: 0x00183464 File Offset: 0x00181664
			public bool onkeypress(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x060067E9 RID: 26601 RVA: 0x000070A6 File Offset: 0x000052A6
			public void onreadystatechange(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x060067EA RID: 26602 RVA: 0x0018348C File Offset: 0x0018168C
			public bool onbeforeupdate(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x060067EB RID: 26603 RVA: 0x000070A6 File Offset: 0x000052A6
			public void onafterupdate(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x060067EC RID: 26604 RVA: 0x001834B4 File Offset: 0x001816B4
			public bool onrowexit(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x060067ED RID: 26605 RVA: 0x000070A6 File Offset: 0x000052A6
			public void onrowenter(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x060067EE RID: 26606 RVA: 0x001834DC File Offset: 0x001816DC
			public bool ondragstart(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x060067EF RID: 26607 RVA: 0x00183504 File Offset: 0x00181704
			public bool onselectstart(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x060067F0 RID: 26608 RVA: 0x0018352C File Offset: 0x0018172C
			public bool onerrorupdate(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x060067F1 RID: 26609 RVA: 0x000070A6 File Offset: 0x000052A6
			public void onrowsdelete(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x060067F2 RID: 26610 RVA: 0x000070A6 File Offset: 0x000052A6
			public void onrowsinserted(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x060067F3 RID: 26611 RVA: 0x000070A6 File Offset: 0x000052A6
			public void oncellchange(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x060067F4 RID: 26612 RVA: 0x000070A6 File Offset: 0x000052A6
			public void onpropertychange(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x060067F5 RID: 26613 RVA: 0x000070A6 File Offset: 0x000052A6
			public void ondatasetchanged(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x060067F6 RID: 26614 RVA: 0x000070A6 File Offset: 0x000052A6
			public void ondataavailable(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x060067F7 RID: 26615 RVA: 0x000070A6 File Offset: 0x000052A6
			public void ondatasetcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x060067F8 RID: 26616 RVA: 0x000070A6 File Offset: 0x000052A6
			public void onbeforeeditfocus(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x060067F9 RID: 26617 RVA: 0x000070A6 File Offset: 0x000052A6
			public void onselectionchange(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x060067FA RID: 26618 RVA: 0x00183554 File Offset: 0x00181754
			public bool oncontrolselect(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x060067FB RID: 26619 RVA: 0x0018357C File Offset: 0x0018177C
			public bool onmousewheel(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x060067FC RID: 26620 RVA: 0x000070A6 File Offset: 0x000052A6
			public void onactivate(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x060067FD RID: 26621 RVA: 0x000070A6 File Offset: 0x000052A6
			public void ondeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x060067FE RID: 26622 RVA: 0x001835A4 File Offset: 0x001817A4
			public bool onbeforeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x060067FF RID: 26623 RVA: 0x001835CC File Offset: 0x001817CC
			public bool onbeforedeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x04003ADE RID: 15070
			private HtmlDocument parent;
		}
	}
}
