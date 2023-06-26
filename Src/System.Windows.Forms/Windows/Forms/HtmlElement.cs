using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	/// <summary>Represents an HTML element inside of a Web page.</summary>
	// Token: 0x02000279 RID: 633
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class HtmlElement
	{
		// Token: 0x06002881 RID: 10369 RVA: 0x000BB967 File Offset: 0x000B9B67
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		internal HtmlElement(HtmlShimManager shimManager, UnsafeNativeMethods.IHTMLElement element)
		{
			this.htmlElement = element;
			this.shimManager = shimManager;
		}

		/// <summary>Gets an <see cref="T:System.Windows.Forms.HtmlElementCollection" /> of all elements underneath the current element.</summary>
		/// <returns>A collection of all elements that are direct or indirect children of the current element. If the current element is a <c>TABLE</c>, for example, <see cref="P:System.Windows.Forms.HtmlElement.All" /> will return every <c>TH</c>, <c>TR</c>, and <c>TD</c> element within the table, as well as any other elements, such as <c>DIV</c> and <c>SPAN</c> elements, contained within the cells.</returns>
		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06002882 RID: 10370 RVA: 0x000BB980 File Offset: 0x000B9B80
		public HtmlElementCollection All
		{
			get
			{
				UnsafeNativeMethods.IHTMLElementCollection ihtmlelementCollection = this.NativeHtmlElement.GetAll() as UnsafeNativeMethods.IHTMLElementCollection;
				if (ihtmlelementCollection == null)
				{
					return new HtmlElementCollection(this.shimManager);
				}
				return new HtmlElementCollection(this.shimManager, ihtmlelementCollection);
			}
		}

		/// <summary>Gets an <see cref="T:System.Windows.Forms.HtmlElementCollection" /> of all children of the current element.</summary>
		/// <returns>A collection of all <see cref="T:System.Windows.Forms.HtmlElement" /> objects that have the current element as a parent.</returns>
		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06002883 RID: 10371 RVA: 0x000BB9BC File Offset: 0x000B9BBC
		public HtmlElementCollection Children
		{
			get
			{
				UnsafeNativeMethods.IHTMLElementCollection ihtmlelementCollection = this.NativeHtmlElement.GetChildren() as UnsafeNativeMethods.IHTMLElementCollection;
				if (ihtmlelementCollection == null)
				{
					return new HtmlElementCollection(this.shimManager);
				}
				return new HtmlElementCollection(this.shimManager, ihtmlelementCollection);
			}
		}

		/// <summary>Gets a value indicating whether this element can have child elements.</summary>
		/// <returns>
		///   <see langword="true" /> if element can have child elements; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06002884 RID: 10372 RVA: 0x000BB9F5 File Offset: 0x000B9BF5
		public bool CanHaveChildren
		{
			get
			{
				return ((UnsafeNativeMethods.IHTMLElement2)this.NativeHtmlElement).CanHaveChildren();
			}
		}

		/// <summary>Gets the bounds of the client area of the element in the HTML document.</summary>
		/// <returns>The client area occupied by the element, minus any area taken by borders and scroll bars. To obtain the position and dimensions of the element inclusive of its adornments, use <see cref="P:System.Windows.Forms.HtmlElement.OffsetRectangle" /> instead.</returns>
		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06002885 RID: 10373 RVA: 0x000BBA08 File Offset: 0x000B9C08
		public Rectangle ClientRectangle
		{
			get
			{
				UnsafeNativeMethods.IHTMLElement2 ihtmlelement = (UnsafeNativeMethods.IHTMLElement2)this.NativeHtmlElement;
				return new Rectangle(ihtmlelement.ClientLeft(), ihtmlelement.ClientTop(), ihtmlelement.ClientWidth(), ihtmlelement.ClientHeight());
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.HtmlDocument" /> to which this element belongs.</summary>
		/// <returns>The parent document of this element.</returns>
		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06002886 RID: 10374 RVA: 0x000BBA40 File Offset: 0x000B9C40
		public HtmlDocument Document
		{
			get
			{
				UnsafeNativeMethods.IHTMLDocument ihtmldocument = this.NativeHtmlElement.GetDocument() as UnsafeNativeMethods.IHTMLDocument;
				if (ihtmldocument == null)
				{
					return null;
				}
				return new HtmlDocument(this.shimManager, ihtmldocument);
			}
		}

		/// <summary>Gets or sets whether the user can input data into this element.</summary>
		/// <returns>
		///   <see langword="true" /> if the element allows user input; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06002887 RID: 10375 RVA: 0x000BBA6F File Offset: 0x000B9C6F
		// (set) Token: 0x06002888 RID: 10376 RVA: 0x000BBA84 File Offset: 0x000B9C84
		public bool Enabled
		{
			get
			{
				return !((UnsafeNativeMethods.IHTMLElement3)this.NativeHtmlElement).GetDisabled();
			}
			set
			{
				((UnsafeNativeMethods.IHTMLElement3)this.NativeHtmlElement).SetDisabled(!value);
			}
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06002889 RID: 10377 RVA: 0x000BBA9C File Offset: 0x000B9C9C
		private HtmlElement.HtmlElementShim ElementShim
		{
			get
			{
				if (this.ShimManager != null)
				{
					HtmlElement.HtmlElementShim htmlElementShim = this.ShimManager.GetElementShim(this);
					if (htmlElementShim == null)
					{
						this.shimManager.AddElementShim(this);
						htmlElementShim = this.ShimManager.GetElementShim(this);
					}
					return htmlElementShim;
				}
				return null;
			}
		}

		/// <summary>Gets the next element below this element in the document tree.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.HtmlElement" /> representing the first element contained underneath the current element, in source order.</returns>
		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x0600288A RID: 10378 RVA: 0x000BBAE0 File Offset: 0x000B9CE0
		public HtmlElement FirstChild
		{
			get
			{
				UnsafeNativeMethods.IHTMLElement ihtmlelement = null;
				UnsafeNativeMethods.IHTMLDOMNode ihtmldomnode = this.NativeHtmlElement as UnsafeNativeMethods.IHTMLDOMNode;
				if (ihtmldomnode != null)
				{
					ihtmlelement = ihtmldomnode.FirstChild() as UnsafeNativeMethods.IHTMLElement;
				}
				if (ihtmlelement == null)
				{
					return null;
				}
				return new HtmlElement(this.shimManager, ihtmlelement);
			}
		}

		/// <summary>Gets or sets a label by which to identify the element.</summary>
		/// <returns>The unique identifier for the element.</returns>
		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x0600288B RID: 10379 RVA: 0x000BBB1B File Offset: 0x000B9D1B
		// (set) Token: 0x0600288C RID: 10380 RVA: 0x000BBB28 File Offset: 0x000B9D28
		public string Id
		{
			get
			{
				return this.NativeHtmlElement.GetId();
			}
			set
			{
				this.NativeHtmlElement.SetId(value);
			}
		}

		/// <summary>Gets or sets the HTML markup underneath this element.</summary>
		/// <returns>The HTML markup that defines the child elements of the current element.</returns>
		/// <exception cref="T:System.NotSupportedException">Creating child elements on this element is not allowed.</exception>
		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x0600288D RID: 10381 RVA: 0x000BBB36 File Offset: 0x000B9D36
		// (set) Token: 0x0600288E RID: 10382 RVA: 0x000BBB44 File Offset: 0x000B9D44
		public string InnerHtml
		{
			get
			{
				return this.NativeHtmlElement.GetInnerHTML();
			}
			set
			{
				try
				{
					this.NativeHtmlElement.SetInnerHTML(value);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode == -2146827688)
					{
						throw new NotSupportedException(SR.GetString("HtmlElementPropertyNotSupported"));
					}
					throw;
				}
			}
		}

		/// <summary>Gets or sets the text assigned to the element.</summary>
		/// <returns>The element's text, absent any HTML markup. If the element contains child elements, only the text in those child elements will be preserved.</returns>
		/// <exception cref="T:System.NotSupportedException">The specified element cannot contain text (for example, an <c>IMG</c> element).</exception>
		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x0600288F RID: 10383 RVA: 0x000BBB90 File Offset: 0x000B9D90
		// (set) Token: 0x06002890 RID: 10384 RVA: 0x000BBBA0 File Offset: 0x000B9DA0
		public string InnerText
		{
			get
			{
				return this.NativeHtmlElement.GetInnerText();
			}
			set
			{
				try
				{
					this.NativeHtmlElement.SetInnerText(value);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode == -2146827688)
					{
						throw new NotSupportedException(SR.GetString("HtmlElementPropertyNotSupported"));
					}
					throw;
				}
			}
		}

		/// <summary>Gets or sets the name of the element.</summary>
		/// <returns>A <see cref="T:System.String" /> representing the element's name.</returns>
		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06002891 RID: 10385 RVA: 0x000BBBEC File Offset: 0x000B9DEC
		// (set) Token: 0x06002892 RID: 10386 RVA: 0x000BBBF9 File Offset: 0x000B9DF9
		public string Name
		{
			get
			{
				return this.GetAttribute("Name");
			}
			set
			{
				this.SetAttribute("Name", value);
			}
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06002893 RID: 10387 RVA: 0x000BBC07 File Offset: 0x000B9E07
		private UnsafeNativeMethods.IHTMLElement NativeHtmlElement
		{
			get
			{
				return this.htmlElement;
			}
		}

		/// <summary>Gets the next element at the same level as this element in the document tree.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.HtmlElement" /> representing the element to the right of the current element.</returns>
		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06002894 RID: 10388 RVA: 0x000BBC10 File Offset: 0x000B9E10
		public HtmlElement NextSibling
		{
			get
			{
				UnsafeNativeMethods.IHTMLElement ihtmlelement = null;
				UnsafeNativeMethods.IHTMLDOMNode ihtmldomnode = this.NativeHtmlElement as UnsafeNativeMethods.IHTMLDOMNode;
				if (ihtmldomnode != null)
				{
					ihtmlelement = ihtmldomnode.NextSibling() as UnsafeNativeMethods.IHTMLElement;
				}
				if (ihtmlelement == null)
				{
					return null;
				}
				return new HtmlElement(this.shimManager, ihtmlelement);
			}
		}

		/// <summary>Gets the location of an element relative to its parent.</summary>
		/// <returns>The x- and y-coordinate positions of the element, and its width and its height, in relation to its parent.  
		///  If an element's parent is relatively or absolutely positioned, <see cref="P:System.Windows.Forms.HtmlElement.OffsetRectangle" /> will return the offset of the parent element. If the element itself is relatively positioned with respect to its parent, <see cref="P:System.Windows.Forms.HtmlElement.OffsetRectangle" /> will return the offset from its parent.</returns>
		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06002895 RID: 10389 RVA: 0x000BBC4B File Offset: 0x000B9E4B
		public Rectangle OffsetRectangle
		{
			get
			{
				return new Rectangle(this.NativeHtmlElement.GetOffsetLeft(), this.NativeHtmlElement.GetOffsetTop(), this.NativeHtmlElement.GetOffsetWidth(), this.NativeHtmlElement.GetOffsetHeight());
			}
		}

		/// <summary>Gets the element from which <see cref="P:System.Windows.Forms.HtmlElement.OffsetRectangle" /> is calculated.</summary>
		/// <returns>The element from which the offsets are calculated.  
		///  If an element's parent or another element in the element's hierarchy uses relative or absolute positioning, <see langword="OffsetParent" /> will be the first relatively or absolutely positioned element in which the current element is nested. If none of the elements above the current element are absolutely or relatively positioned, <see langword="OffsetParent" /> will be the <c>BODY</c> tag of the document.</returns>
		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06002896 RID: 10390 RVA: 0x000BBC80 File Offset: 0x000B9E80
		public HtmlElement OffsetParent
		{
			get
			{
				UnsafeNativeMethods.IHTMLElement offsetParent = this.NativeHtmlElement.GetOffsetParent();
				if (offsetParent == null)
				{
					return null;
				}
				return new HtmlElement(this.shimManager, offsetParent);
			}
		}

		/// <summary>Gets or sets the current element's HTML code.</summary>
		/// <returns>The HTML code for the current element and its children.</returns>
		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06002897 RID: 10391 RVA: 0x000BBCAA File Offset: 0x000B9EAA
		// (set) Token: 0x06002898 RID: 10392 RVA: 0x000BBCB8 File Offset: 0x000B9EB8
		public string OuterHtml
		{
			get
			{
				return this.NativeHtmlElement.GetOuterHTML();
			}
			set
			{
				try
				{
					this.NativeHtmlElement.SetOuterHTML(value);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode == -2146827688)
					{
						throw new NotSupportedException(SR.GetString("HtmlElementPropertyNotSupported"));
					}
					throw;
				}
			}
		}

		/// <summary>Gets or sets the current element's text.</summary>
		/// <returns>The text inside the current element, and in the element's children.</returns>
		/// <exception cref="T:System.NotSupportedException">You cannot set text outside of this element.</exception>
		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06002899 RID: 10393 RVA: 0x000BBD04 File Offset: 0x000B9F04
		// (set) Token: 0x0600289A RID: 10394 RVA: 0x000BBD14 File Offset: 0x000B9F14
		public string OuterText
		{
			get
			{
				return this.NativeHtmlElement.GetOuterText();
			}
			set
			{
				try
				{
					this.NativeHtmlElement.SetOuterText(value);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode == -2146827688)
					{
						throw new NotSupportedException(SR.GetString("HtmlElementPropertyNotSupported"));
					}
					throw;
				}
			}
		}

		/// <summary>Gets the current element's parent element.</summary>
		/// <returns>The element above the current element in the HTML document's hierarchy.</returns>
		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x0600289B RID: 10395 RVA: 0x000BBD60 File Offset: 0x000B9F60
		public HtmlElement Parent
		{
			get
			{
				UnsafeNativeMethods.IHTMLElement parentElement = this.NativeHtmlElement.GetParentElement();
				if (parentElement == null)
				{
					return null;
				}
				return new HtmlElement(this.shimManager, parentElement);
			}
		}

		/// <summary>Gets the dimensions of an element's scrollable region.</summary>
		/// <returns>The size and coordinate location of the scrollable area of an element.</returns>
		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x0600289C RID: 10396 RVA: 0x000BBD8C File Offset: 0x000B9F8C
		public Rectangle ScrollRectangle
		{
			get
			{
				UnsafeNativeMethods.IHTMLElement2 ihtmlelement = (UnsafeNativeMethods.IHTMLElement2)this.NativeHtmlElement;
				return new Rectangle(ihtmlelement.GetScrollLeft(), ihtmlelement.GetScrollTop(), ihtmlelement.GetScrollWidth(), ihtmlelement.GetScrollHeight());
			}
		}

		/// <summary>Gets or sets the distance between the edge of the element and the left edge of its content.</summary>
		/// <returns>The distance, in pixels, between the left edge of the element and the left edge of its content.</returns>
		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x0600289D RID: 10397 RVA: 0x000BBDC2 File Offset: 0x000B9FC2
		// (set) Token: 0x0600289E RID: 10398 RVA: 0x000BBDD4 File Offset: 0x000B9FD4
		public int ScrollLeft
		{
			get
			{
				return ((UnsafeNativeMethods.IHTMLElement2)this.NativeHtmlElement).GetScrollLeft();
			}
			set
			{
				((UnsafeNativeMethods.IHTMLElement2)this.NativeHtmlElement).SetScrollLeft(value);
			}
		}

		/// <summary>Gets or sets the distance between the edge of the element and the top edge of its content.</summary>
		/// <returns>The distance, in pixels, between the top edge of the element and the top edge of its content.</returns>
		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x0600289F RID: 10399 RVA: 0x000BBDE7 File Offset: 0x000B9FE7
		// (set) Token: 0x060028A0 RID: 10400 RVA: 0x000BBDF9 File Offset: 0x000B9FF9
		public int ScrollTop
		{
			get
			{
				return ((UnsafeNativeMethods.IHTMLElement2)this.NativeHtmlElement).GetScrollTop();
			}
			set
			{
				((UnsafeNativeMethods.IHTMLElement2)this.NativeHtmlElement).SetScrollTop(value);
			}
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x060028A1 RID: 10401 RVA: 0x000BBE0C File Offset: 0x000BA00C
		private HtmlShimManager ShimManager
		{
			get
			{
				return this.shimManager;
			}
		}

		/// <summary>Gets or sets a semicolon-delimited list of styles for the current element.</summary>
		/// <returns>A string consisting of all of the element's styles</returns>
		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x060028A2 RID: 10402 RVA: 0x000BBE14 File Offset: 0x000BA014
		// (set) Token: 0x060028A3 RID: 10403 RVA: 0x000BBE26 File Offset: 0x000BA026
		public string Style
		{
			get
			{
				return this.NativeHtmlElement.GetStyle().GetCssText();
			}
			set
			{
				this.NativeHtmlElement.GetStyle().SetCssText(value);
			}
		}

		/// <summary>Gets the name of the HTML tag.</summary>
		/// <returns>The name used to create this element using HTML markup.</returns>
		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x060028A4 RID: 10404 RVA: 0x000BBE39 File Offset: 0x000BA039
		public string TagName
		{
			get
			{
				return this.NativeHtmlElement.GetTagName();
			}
		}

		/// <summary>Gets or sets the location of this element in the tab order.</summary>
		/// <returns>The numeric index of the element in the tab order.</returns>
		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x060028A5 RID: 10405 RVA: 0x000BBE46 File Offset: 0x000BA046
		// (set) Token: 0x060028A6 RID: 10406 RVA: 0x000BBE58 File Offset: 0x000BA058
		public short TabIndex
		{
			get
			{
				return ((UnsafeNativeMethods.IHTMLElement2)this.NativeHtmlElement).GetTabIndex();
			}
			set
			{
				((UnsafeNativeMethods.IHTMLElement2)this.NativeHtmlElement).SetTabIndex((int)value);
			}
		}

		/// <summary>Gets an unmanaged interface pointer for this element.</summary>
		/// <returns>The COM <c>IUnknown</c> pointer for the element, which you can cast to one of the HTML element interfaces, such as <c>IHTMLElement</c>.</returns>
		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x060028A7 RID: 10407 RVA: 0x000BBE6B File Offset: 0x000BA06B
		public object DomElement
		{
			get
			{
				return this.NativeHtmlElement;
			}
		}

		/// <summary>Adds an element to another element's subtree.</summary>
		/// <param name="newElement">The <see cref="T:System.Windows.Forms.HtmlElement" /> to append to this location in the tree.</param>
		/// <returns>The element after it has been added to the tree.</returns>
		// Token: 0x060028A8 RID: 10408 RVA: 0x000BBE73 File Offset: 0x000BA073
		public HtmlElement AppendChild(HtmlElement newElement)
		{
			return this.InsertAdjacentElement(HtmlElementInsertionOrientation.BeforeEnd, newElement);
		}

		/// <summary>Adds an event handler for a named event on the HTML Document Object Model (DOM).</summary>
		/// <param name="eventName">The name of the event you want to handle.</param>
		/// <param name="eventHandler">The managed code that handles the event.</param>
		// Token: 0x060028A9 RID: 10409 RVA: 0x000BBE7D File Offset: 0x000BA07D
		public void AttachEventHandler(string eventName, EventHandler eventHandler)
		{
			this.ElementShim.AttachEventHandler(eventName, eventHandler);
		}

		/// <summary>Removes an event handler from a named event on the HTML Document Object Model (DOM).</summary>
		/// <param name="eventName">The name of the event you want to handle.</param>
		/// <param name="eventHandler">The managed code that handles the event.</param>
		// Token: 0x060028AA RID: 10410 RVA: 0x000BBE8C File Offset: 0x000BA08C
		public void DetachEventHandler(string eventName, EventHandler eventHandler)
		{
			this.ElementShim.DetachEventHandler(eventName, eventHandler);
		}

		/// <summary>Puts user input focus on the current element.</summary>
		// Token: 0x060028AB RID: 10411 RVA: 0x000BBE9C File Offset: 0x000BA09C
		public void Focus()
		{
			try
			{
				((UnsafeNativeMethods.IHTMLElement2)this.NativeHtmlElement).Focus();
			}
			catch (COMException ex)
			{
				if (ex.ErrorCode == -2146826178)
				{
					throw new NotSupportedException(SR.GetString("HtmlElementMethodNotSupported"));
				}
				throw;
			}
		}

		/// <summary>Retrieves the value of the named attribute on the element.</summary>
		/// <param name="attributeName">The name of the attribute. This argument is case-insensitive.</param>
		/// <returns>The value of this attribute on the element, as a <see cref="T:System.String" /> value. If the specified attribute does not exist on this element, returns an empty string.</returns>
		// Token: 0x060028AC RID: 10412 RVA: 0x000BBEEC File Offset: 0x000BA0EC
		public string GetAttribute(string attributeName)
		{
			object attribute = this.NativeHtmlElement.GetAttribute(attributeName, 0);
			if (attribute != null)
			{
				return attribute.ToString();
			}
			return "";
		}

		/// <summary>Retrieves a collection of elements represented in HTML by the specified <c>HTML</c> tag.</summary>
		/// <param name="tagName">The name of the tag whose <see cref="T:System.Windows.Forms.HtmlElement" /> objects you wish to retrieve.</param>
		/// <returns>An <see cref="T:System.Windows.Forms.HtmlElementCollection" /> containing all elements whose <c>HTML</c> tag name is equal to <paramref name="tagName" />.</returns>
		// Token: 0x060028AD RID: 10413 RVA: 0x000BBF18 File Offset: 0x000BA118
		public HtmlElementCollection GetElementsByTagName(string tagName)
		{
			UnsafeNativeMethods.IHTMLElementCollection elementsByTagName = ((UnsafeNativeMethods.IHTMLElement2)this.NativeHtmlElement).GetElementsByTagName(tagName);
			if (elementsByTagName == null)
			{
				return new HtmlElementCollection(this.shimManager);
			}
			return new HtmlElementCollection(this.shimManager, elementsByTagName);
		}

		/// <summary>Insert a new element into the Document Object Model (DOM).</summary>
		/// <param name="orient">Where to insert this element in relation to the current element.</param>
		/// <param name="newElement">The new element to insert.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.HtmlElement" /> that was just inserted. If insertion failed, this will return <see langword="null" />.</returns>
		// Token: 0x060028AE RID: 10414 RVA: 0x000BBF54 File Offset: 0x000BA154
		public HtmlElement InsertAdjacentElement(HtmlElementInsertionOrientation orient, HtmlElement newElement)
		{
			UnsafeNativeMethods.IHTMLElement ihtmlelement = ((UnsafeNativeMethods.IHTMLElement2)this.NativeHtmlElement).InsertAdjacentElement(orient.ToString(), (UnsafeNativeMethods.IHTMLElement)newElement.DomElement);
			if (ihtmlelement == null)
			{
				return null;
			}
			return new HtmlElement(this.shimManager, ihtmlelement);
		}

		/// <summary>Executes an unexposed method on the underlying DOM element of this element.</summary>
		/// <param name="methodName">The name of the property or method to invoke.</param>
		/// <returns>The element returned by this method, represented as an <see cref="T:System.Object" />. If this <see cref="T:System.Object" /> is another HTML element, and you have a reference to the unmanaged MSHTML library added to your project, you can cast it to its appropriate unmanaged interface.</returns>
		// Token: 0x060028AF RID: 10415 RVA: 0x000BBF9B File Offset: 0x000BA19B
		public object InvokeMember(string methodName)
		{
			return this.InvokeMember(methodName, null);
		}

		/// <summary>Executes a function defined in the current HTML page by a scripting language.</summary>
		/// <param name="methodName">The name of the property or method to invoke.</param>
		/// <param name="parameter">A list of parameters to pass.</param>
		/// <returns>The element returned by the function, represented as an <see cref="T:System.Object" />. If this <see cref="T:System.Object" /> is another HTML element, and you have a reference to the unmanaged MSHTML library added to your project, you can cast it to its appropriate unmanaged interface.</returns>
		// Token: 0x060028B0 RID: 10416 RVA: 0x000BBFA8 File Offset: 0x000BA1A8
		public object InvokeMember(string methodName, params object[] parameter)
		{
			object obj = null;
			NativeMethods.tagDISPPARAMS tagDISPPARAMS = new NativeMethods.tagDISPPARAMS();
			tagDISPPARAMS.rgvarg = IntPtr.Zero;
			try
			{
				UnsafeNativeMethods.IDispatch dispatch = this.NativeHtmlElement as UnsafeNativeMethods.IDispatch;
				if (dispatch != null)
				{
					Guid empty = Guid.Empty;
					string[] array = new string[] { methodName };
					int[] array2 = new int[] { -1 };
					int idsOfNames = dispatch.GetIDsOfNames(ref empty, array, 1, SafeNativeMethods.GetThreadLCID(), array2);
					if (NativeMethods.Succeeded(idsOfNames) && array2[0] != -1)
					{
						if (parameter != null)
						{
							Array.Reverse(parameter);
						}
						tagDISPPARAMS.rgvarg = ((parameter == null) ? IntPtr.Zero : HtmlDocument.ArrayToVARIANTVector(parameter));
						tagDISPPARAMS.cArgs = ((parameter == null) ? 0 : parameter.Length);
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
					HtmlDocument.FreeVARIANTVector(tagDISPPARAMS.rgvarg, parameter.Length);
				}
			}
			return obj;
		}

		/// <summary>Removes focus from the current element, if that element has focus.</summary>
		// Token: 0x060028B1 RID: 10417 RVA: 0x000BC0DC File Offset: 0x000BA2DC
		public void RemoveFocus()
		{
			((UnsafeNativeMethods.IHTMLElement2)this.NativeHtmlElement).Blur();
		}

		/// <summary>Causes the named event to call all registered event handlers.</summary>
		/// <param name="eventName">The name of the event to raise.</param>
		// Token: 0x060028B2 RID: 10418 RVA: 0x000BC0EE File Offset: 0x000BA2EE
		public void RaiseEvent(string eventName)
		{
			((UnsafeNativeMethods.IHTMLElement3)this.NativeHtmlElement).FireEvent(eventName, IntPtr.Zero);
		}

		/// <summary>Scrolls through the document containing this element until the top or bottom edge of this element is aligned with the document's window.</summary>
		/// <param name="alignWithTop">If <see langword="true" />, the top of the object will be displayed at the top of the window. If <see langword="false" />, the bottom of the object will be displayed at the bottom of the window.</param>
		// Token: 0x060028B3 RID: 10419 RVA: 0x000BC107 File Offset: 0x000BA307
		public void ScrollIntoView(bool alignWithTop)
		{
			this.NativeHtmlElement.ScrollIntoView(alignWithTop);
		}

		/// <summary>Sets the value of the named attribute on the element.</summary>
		/// <param name="attributeName">The name of the attribute to set.</param>
		/// <param name="value">The new value of this attribute.</param>
		// Token: 0x060028B4 RID: 10420 RVA: 0x000BC11C File Offset: 0x000BA31C
		public void SetAttribute(string attributeName, string value)
		{
			try
			{
				this.NativeHtmlElement.SetAttribute(attributeName, value, 0);
			}
			catch (COMException ex)
			{
				if (ex.ErrorCode == -2147352567)
				{
					throw new NotSupportedException(SR.GetString("HtmlElementAttributeNotSupported"));
				}
				throw;
			}
		}

		/// <summary>Occurs when the user clicks on the element with the left mouse button.</summary>
		// Token: 0x140001CF RID: 463
		// (add) Token: 0x060028B5 RID: 10421 RVA: 0x000BC16C File Offset: 0x000BA36C
		// (remove) Token: 0x060028B6 RID: 10422 RVA: 0x000BC17F File Offset: 0x000BA37F
		public event HtmlElementEventHandler Click
		{
			add
			{
				this.ElementShim.AddHandler(HtmlElement.EventClick, value);
			}
			remove
			{
				this.ElementShim.RemoveHandler(HtmlElement.EventClick, value);
			}
		}

		/// <summary>Occurs when the user clicks the left mouse button over an element twice, in rapid succession.</summary>
		// Token: 0x140001D0 RID: 464
		// (add) Token: 0x060028B7 RID: 10423 RVA: 0x000BC192 File Offset: 0x000BA392
		// (remove) Token: 0x060028B8 RID: 10424 RVA: 0x000BC1A5 File Offset: 0x000BA3A5
		public event HtmlElementEventHandler DoubleClick
		{
			add
			{
				this.ElementShim.AddHandler(HtmlElement.EventDoubleClick, value);
			}
			remove
			{
				this.ElementShim.RemoveHandler(HtmlElement.EventDoubleClick, value);
			}
		}

		/// <summary>Occurs when the user drags text to various locations.</summary>
		// Token: 0x140001D1 RID: 465
		// (add) Token: 0x060028B9 RID: 10425 RVA: 0x000BC1B8 File Offset: 0x000BA3B8
		// (remove) Token: 0x060028BA RID: 10426 RVA: 0x000BC1CB File Offset: 0x000BA3CB
		public event HtmlElementEventHandler Drag
		{
			add
			{
				this.ElementShim.AddHandler(HtmlElement.EventDrag, value);
			}
			remove
			{
				this.ElementShim.RemoveHandler(HtmlElement.EventDrag, value);
			}
		}

		/// <summary>Occurs when a user finishes a drag operation.</summary>
		// Token: 0x140001D2 RID: 466
		// (add) Token: 0x060028BB RID: 10427 RVA: 0x000BC1DE File Offset: 0x000BA3DE
		// (remove) Token: 0x060028BC RID: 10428 RVA: 0x000BC1F1 File Offset: 0x000BA3F1
		public event HtmlElementEventHandler DragEnd
		{
			add
			{
				this.ElementShim.AddHandler(HtmlElement.EventDragEnd, value);
			}
			remove
			{
				this.ElementShim.RemoveHandler(HtmlElement.EventDragEnd, value);
			}
		}

		/// <summary>Occurs when the user is no longer dragging an item over this element.</summary>
		// Token: 0x140001D3 RID: 467
		// (add) Token: 0x060028BD RID: 10429 RVA: 0x000BC204 File Offset: 0x000BA404
		// (remove) Token: 0x060028BE RID: 10430 RVA: 0x000BC217 File Offset: 0x000BA417
		public event HtmlElementEventHandler DragLeave
		{
			add
			{
				this.ElementShim.AddHandler(HtmlElement.EventDragLeave, value);
			}
			remove
			{
				this.ElementShim.RemoveHandler(HtmlElement.EventDragLeave, value);
			}
		}

		/// <summary>Occurs when the user drags text over the element.</summary>
		// Token: 0x140001D4 RID: 468
		// (add) Token: 0x060028BF RID: 10431 RVA: 0x000BC22A File Offset: 0x000BA42A
		// (remove) Token: 0x060028C0 RID: 10432 RVA: 0x000BC23D File Offset: 0x000BA43D
		public event HtmlElementEventHandler DragOver
		{
			add
			{
				this.ElementShim.AddHandler(HtmlElement.EventDragOver, value);
			}
			remove
			{
				this.ElementShim.RemoveHandler(HtmlElement.EventDragOver, value);
			}
		}

		/// <summary>Occurs when the element first receives user input focus.</summary>
		// Token: 0x140001D5 RID: 469
		// (add) Token: 0x060028C1 RID: 10433 RVA: 0x000BC250 File Offset: 0x000BA450
		// (remove) Token: 0x060028C2 RID: 10434 RVA: 0x000BC263 File Offset: 0x000BA463
		public event HtmlElementEventHandler Focusing
		{
			add
			{
				this.ElementShim.AddHandler(HtmlElement.EventFocusing, value);
			}
			remove
			{
				this.ElementShim.RemoveHandler(HtmlElement.EventFocusing, value);
			}
		}

		/// <summary>Occurs when the element has received user input focus.</summary>
		// Token: 0x140001D6 RID: 470
		// (add) Token: 0x060028C3 RID: 10435 RVA: 0x000BC276 File Offset: 0x000BA476
		// (remove) Token: 0x060028C4 RID: 10436 RVA: 0x000BC289 File Offset: 0x000BA489
		public event HtmlElementEventHandler GotFocus
		{
			add
			{
				this.ElementShim.AddHandler(HtmlElement.EventGotFocus, value);
			}
			remove
			{
				this.ElementShim.RemoveHandler(HtmlElement.EventGotFocus, value);
			}
		}

		/// <summary>Occurs when the element is losing user input focus.</summary>
		// Token: 0x140001D7 RID: 471
		// (add) Token: 0x060028C5 RID: 10437 RVA: 0x000BC29C File Offset: 0x000BA49C
		// (remove) Token: 0x060028C6 RID: 10438 RVA: 0x000BC2AF File Offset: 0x000BA4AF
		public event HtmlElementEventHandler LosingFocus
		{
			add
			{
				this.ElementShim.AddHandler(HtmlElement.EventLosingFocus, value);
			}
			remove
			{
				this.ElementShim.RemoveHandler(HtmlElement.EventLosingFocus, value);
			}
		}

		/// <summary>Occurs when the element has lost user input focus.</summary>
		// Token: 0x140001D8 RID: 472
		// (add) Token: 0x060028C7 RID: 10439 RVA: 0x000BC2C2 File Offset: 0x000BA4C2
		// (remove) Token: 0x060028C8 RID: 10440 RVA: 0x000BC2D5 File Offset: 0x000BA4D5
		public event HtmlElementEventHandler LostFocus
		{
			add
			{
				this.ElementShim.AddHandler(HtmlElement.EventLostFocus, value);
			}
			remove
			{
				this.ElementShim.RemoveHandler(HtmlElement.EventLostFocus, value);
			}
		}

		/// <summary>Occurs when the user presses a key on the keyboard.</summary>
		// Token: 0x140001D9 RID: 473
		// (add) Token: 0x060028C9 RID: 10441 RVA: 0x000BC2E8 File Offset: 0x000BA4E8
		// (remove) Token: 0x060028CA RID: 10442 RVA: 0x000BC2FB File Offset: 0x000BA4FB
		public event HtmlElementEventHandler KeyDown
		{
			add
			{
				this.ElementShim.AddHandler(HtmlElement.EventKeyDown, value);
			}
			remove
			{
				this.ElementShim.RemoveHandler(HtmlElement.EventKeyDown, value);
			}
		}

		/// <summary>Occurs when the user presses and releases a key on the keyboard.</summary>
		// Token: 0x140001DA RID: 474
		// (add) Token: 0x060028CB RID: 10443 RVA: 0x000BC30E File Offset: 0x000BA50E
		// (remove) Token: 0x060028CC RID: 10444 RVA: 0x000BC321 File Offset: 0x000BA521
		public event HtmlElementEventHandler KeyPress
		{
			add
			{
				this.ElementShim.AddHandler(HtmlElement.EventKeyPress, value);
			}
			remove
			{
				this.ElementShim.RemoveHandler(HtmlElement.EventKeyPress, value);
			}
		}

		/// <summary>Occurs when the user releases a key on the keyboard.</summary>
		// Token: 0x140001DB RID: 475
		// (add) Token: 0x060028CD RID: 10445 RVA: 0x000BC334 File Offset: 0x000BA534
		// (remove) Token: 0x060028CE RID: 10446 RVA: 0x000BC347 File Offset: 0x000BA547
		public event HtmlElementEventHandler KeyUp
		{
			add
			{
				this.ElementShim.AddHandler(HtmlElement.EventKeyUp, value);
			}
			remove
			{
				this.ElementShim.RemoveHandler(HtmlElement.EventKeyUp, value);
			}
		}

		/// <summary>Occurs when the user moves the mouse cursor across the element.</summary>
		// Token: 0x140001DC RID: 476
		// (add) Token: 0x060028CF RID: 10447 RVA: 0x000BC35A File Offset: 0x000BA55A
		// (remove) Token: 0x060028D0 RID: 10448 RVA: 0x000BC36D File Offset: 0x000BA56D
		public event HtmlElementEventHandler MouseMove
		{
			add
			{
				this.ElementShim.AddHandler(HtmlElement.EventMouseMove, value);
			}
			remove
			{
				this.ElementShim.RemoveHandler(HtmlElement.EventMouseMove, value);
			}
		}

		/// <summary>Occurs when the user presses a mouse button.</summary>
		// Token: 0x140001DD RID: 477
		// (add) Token: 0x060028D1 RID: 10449 RVA: 0x000BC380 File Offset: 0x000BA580
		// (remove) Token: 0x060028D2 RID: 10450 RVA: 0x000BC393 File Offset: 0x000BA593
		public event HtmlElementEventHandler MouseDown
		{
			add
			{
				this.ElementShim.AddHandler(HtmlElement.EventMouseDown, value);
			}
			remove
			{
				this.ElementShim.RemoveHandler(HtmlElement.EventMouseDown, value);
			}
		}

		/// <summary>Occurs when the mouse cursor enters the bounds of the element.</summary>
		// Token: 0x140001DE RID: 478
		// (add) Token: 0x060028D3 RID: 10451 RVA: 0x000BC3A6 File Offset: 0x000BA5A6
		// (remove) Token: 0x060028D4 RID: 10452 RVA: 0x000BC3B9 File Offset: 0x000BA5B9
		public event HtmlElementEventHandler MouseOver
		{
			add
			{
				this.ElementShim.AddHandler(HtmlElement.EventMouseOver, value);
			}
			remove
			{
				this.ElementShim.RemoveHandler(HtmlElement.EventMouseOver, value);
			}
		}

		/// <summary>Occurs when the user releases a mouse button.</summary>
		// Token: 0x140001DF RID: 479
		// (add) Token: 0x060028D5 RID: 10453 RVA: 0x000BC3CC File Offset: 0x000BA5CC
		// (remove) Token: 0x060028D6 RID: 10454 RVA: 0x000BC3DF File Offset: 0x000BA5DF
		public event HtmlElementEventHandler MouseUp
		{
			add
			{
				this.ElementShim.AddHandler(HtmlElement.EventMouseUp, value);
			}
			remove
			{
				this.ElementShim.RemoveHandler(HtmlElement.EventMouseUp, value);
			}
		}

		/// <summary>Occurs when the user first moves the mouse cursor over the current element.</summary>
		// Token: 0x140001E0 RID: 480
		// (add) Token: 0x060028D7 RID: 10455 RVA: 0x000BC3F2 File Offset: 0x000BA5F2
		// (remove) Token: 0x060028D8 RID: 10456 RVA: 0x000BC405 File Offset: 0x000BA605
		public event HtmlElementEventHandler MouseEnter
		{
			add
			{
				this.ElementShim.AddHandler(HtmlElement.EventMouseEnter, value);
			}
			remove
			{
				this.ElementShim.RemoveHandler(HtmlElement.EventMouseEnter, value);
			}
		}

		/// <summary>Occurs when the user moves the mouse cursor off of the current element.</summary>
		// Token: 0x140001E1 RID: 481
		// (add) Token: 0x060028D9 RID: 10457 RVA: 0x000BC418 File Offset: 0x000BA618
		// (remove) Token: 0x060028DA RID: 10458 RVA: 0x000BC42B File Offset: 0x000BA62B
		public event HtmlElementEventHandler MouseLeave
		{
			add
			{
				this.ElementShim.AddHandler(HtmlElement.EventMouseLeave, value);
			}
			remove
			{
				this.ElementShim.RemoveHandler(HtmlElement.EventMouseLeave, value);
			}
		}

		/// <summary>Compares two elements for equality.</summary>
		/// <param name="left">The first <see cref="T:System.Windows.Forms.HtmlElement" />.</param>
		/// <param name="right">The second <see cref="T:System.Windows.Forms.HtmlElement" />.</param>
		/// <returns>
		///   <see langword="true" /> if both parameters are <see langword="null" />, or if both elements have the same underlying COM interface; otherwise, <see langword="false" />.</returns>
		// Token: 0x060028DB RID: 10459 RVA: 0x000BC440 File Offset: 0x000BA640
		public static bool operator ==(HtmlElement left, HtmlElement right)
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
				intPtr = Marshal.GetIUnknownForObject(left.NativeHtmlElement);
				intPtr2 = Marshal.GetIUnknownForObject(right.NativeHtmlElement);
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

		/// <summary>Compares two <see cref="T:System.Windows.Forms.HtmlElement" /> objects for inequality.</summary>
		/// <param name="left">The first <see cref="T:System.Windows.Forms.HtmlElement" />.</param>
		/// <param name="right">The second <see cref="T:System.Windows.Forms.HtmlElement" />.</param>
		/// <returns>
		///   <see langword="true" /> is only one element is <see langword="null" />, or the two objects are not equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x060028DC RID: 10460 RVA: 0x000BC4C8 File Offset: 0x000BA6C8
		public static bool operator !=(HtmlElement left, HtmlElement right)
		{
			return !(left == right);
		}

		/// <summary>Serves as a hash function for a particular type.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
		// Token: 0x060028DD RID: 10461 RVA: 0x000BC4D4 File Offset: 0x000BA6D4
		public override int GetHashCode()
		{
			if (this.htmlElement != null)
			{
				return this.htmlElement.GetHashCode();
			}
			return 0;
		}

		/// <summary>Tests if the supplied object is equal to the current element.</summary>
		/// <param name="obj">The object to test for equality.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is an <see cref="T:System.Windows.Forms.HtmlElement" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060028DE RID: 10462 RVA: 0x000BC4EB File Offset: 0x000BA6EB
		public override bool Equals(object obj)
		{
			return this == obj as HtmlElement;
		}

		// Token: 0x040010A6 RID: 4262
		internal static readonly object EventClick = new object();

		// Token: 0x040010A7 RID: 4263
		internal static readonly object EventDoubleClick = new object();

		// Token: 0x040010A8 RID: 4264
		internal static readonly object EventDrag = new object();

		// Token: 0x040010A9 RID: 4265
		internal static readonly object EventDragEnd = new object();

		// Token: 0x040010AA RID: 4266
		internal static readonly object EventDragLeave = new object();

		// Token: 0x040010AB RID: 4267
		internal static readonly object EventDragOver = new object();

		// Token: 0x040010AC RID: 4268
		internal static readonly object EventFocusing = new object();

		// Token: 0x040010AD RID: 4269
		internal static readonly object EventGotFocus = new object();

		// Token: 0x040010AE RID: 4270
		internal static readonly object EventLosingFocus = new object();

		// Token: 0x040010AF RID: 4271
		internal static readonly object EventLostFocus = new object();

		// Token: 0x040010B0 RID: 4272
		internal static readonly object EventKeyDown = new object();

		// Token: 0x040010B1 RID: 4273
		internal static readonly object EventKeyPress = new object();

		// Token: 0x040010B2 RID: 4274
		internal static readonly object EventKeyUp = new object();

		// Token: 0x040010B3 RID: 4275
		internal static readonly object EventMouseDown = new object();

		// Token: 0x040010B4 RID: 4276
		internal static readonly object EventMouseEnter = new object();

		// Token: 0x040010B5 RID: 4277
		internal static readonly object EventMouseLeave = new object();

		// Token: 0x040010B6 RID: 4278
		internal static readonly object EventMouseMove = new object();

		// Token: 0x040010B7 RID: 4279
		internal static readonly object EventMouseOver = new object();

		// Token: 0x040010B8 RID: 4280
		internal static readonly object EventMouseUp = new object();

		// Token: 0x040010B9 RID: 4281
		private UnsafeNativeMethods.IHTMLElement htmlElement;

		// Token: 0x040010BA RID: 4282
		private HtmlShimManager shimManager;

		// Token: 0x020006A6 RID: 1702
		[ClassInterface(ClassInterfaceType.None)]
		private class HTMLElementEvents2 : StandardOleMarshalObject, UnsafeNativeMethods.DHTMLElementEvents2, UnsafeNativeMethods.DHTMLAnchorEvents2, UnsafeNativeMethods.DHTMLAreaEvents2, UnsafeNativeMethods.DHTMLButtonElementEvents2, UnsafeNativeMethods.DHTMLControlElementEvents2, UnsafeNativeMethods.DHTMLFormElementEvents2, UnsafeNativeMethods.DHTMLFrameSiteEvents2, UnsafeNativeMethods.DHTMLImgEvents2, UnsafeNativeMethods.DHTMLInputFileElementEvents2, UnsafeNativeMethods.DHTMLInputImageEvents2, UnsafeNativeMethods.DHTMLInputTextElementEvents2, UnsafeNativeMethods.DHTMLLabelEvents2, UnsafeNativeMethods.DHTMLLinkElementEvents2, UnsafeNativeMethods.DHTMLMapEvents2, UnsafeNativeMethods.DHTMLMarqueeElementEvents2, UnsafeNativeMethods.DHTMLOptionButtonElementEvents2, UnsafeNativeMethods.DHTMLSelectElementEvents2, UnsafeNativeMethods.DHTMLStyleElementEvents2, UnsafeNativeMethods.DHTMLTableEvents2, UnsafeNativeMethods.DHTMLTextContainerEvents2, UnsafeNativeMethods.DHTMLScriptEvents2
		{
			// Token: 0x06006800 RID: 26624 RVA: 0x001835F1 File Offset: 0x001817F1
			public HTMLElementEvents2(HtmlElement htmlElement)
			{
				this.parent = htmlElement;
			}

			// Token: 0x06006801 RID: 26625 RVA: 0x00183600 File Offset: 0x00181800
			private void FireEvent(object key, EventArgs e)
			{
				if (this.parent != null)
				{
					this.parent.ElementShim.FireEvent(key, e);
				}
			}

			// Token: 0x06006802 RID: 26626 RVA: 0x00183624 File Offset: 0x00181824
			public bool onclick(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlElement.EventClick, htmlElementEventArgs);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006803 RID: 26627 RVA: 0x00183658 File Offset: 0x00181858
			public bool ondblclick(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlElement.EventDoubleClick, htmlElementEventArgs);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006804 RID: 26628 RVA: 0x0018368C File Offset: 0x0018188C
			public bool onkeypress(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlElement.EventKeyPress, htmlElementEventArgs);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006805 RID: 26629 RVA: 0x001836C0 File Offset: 0x001818C0
			public void onkeydown(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlElement.EventKeyDown, htmlElementEventArgs);
			}

			// Token: 0x06006806 RID: 26630 RVA: 0x001836EC File Offset: 0x001818EC
			public void onkeyup(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlElement.EventKeyUp, htmlElementEventArgs);
			}

			// Token: 0x06006807 RID: 26631 RVA: 0x00183718 File Offset: 0x00181918
			public void onmouseover(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlElement.EventMouseOver, htmlElementEventArgs);
			}

			// Token: 0x06006808 RID: 26632 RVA: 0x00183744 File Offset: 0x00181944
			public void onmousemove(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlElement.EventMouseMove, htmlElementEventArgs);
			}

			// Token: 0x06006809 RID: 26633 RVA: 0x00183770 File Offset: 0x00181970
			public void onmousedown(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlElement.EventMouseDown, htmlElementEventArgs);
			}

			// Token: 0x0600680A RID: 26634 RVA: 0x0018379C File Offset: 0x0018199C
			public void onmouseup(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlElement.EventMouseUp, htmlElementEventArgs);
			}

			// Token: 0x0600680B RID: 26635 RVA: 0x001837C8 File Offset: 0x001819C8
			public void onmouseenter(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlElement.EventMouseEnter, htmlElementEventArgs);
			}

			// Token: 0x0600680C RID: 26636 RVA: 0x001837F4 File Offset: 0x001819F4
			public void onmouseleave(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlElement.EventMouseLeave, htmlElementEventArgs);
			}

			// Token: 0x0600680D RID: 26637 RVA: 0x00183820 File Offset: 0x00181A20
			public bool onerrorupdate(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x0600680E RID: 26638 RVA: 0x00183848 File Offset: 0x00181A48
			public void onfocus(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlElement.EventGotFocus, htmlElementEventArgs);
			}

			// Token: 0x0600680F RID: 26639 RVA: 0x00183874 File Offset: 0x00181A74
			public bool ondrag(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlElement.EventDrag, htmlElementEventArgs);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006810 RID: 26640 RVA: 0x001838A8 File Offset: 0x00181AA8
			public void ondragend(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlElement.EventDragEnd, htmlElementEventArgs);
			}

			// Token: 0x06006811 RID: 26641 RVA: 0x001838D4 File Offset: 0x00181AD4
			public void ondragleave(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlElement.EventDragLeave, htmlElementEventArgs);
			}

			// Token: 0x06006812 RID: 26642 RVA: 0x00183900 File Offset: 0x00181B00
			public bool ondragover(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlElement.EventDragOver, htmlElementEventArgs);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006813 RID: 26643 RVA: 0x00183934 File Offset: 0x00181B34
			public void onfocusin(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlElement.EventFocusing, htmlElementEventArgs);
			}

			// Token: 0x06006814 RID: 26644 RVA: 0x00183960 File Offset: 0x00181B60
			public void onfocusout(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlElement.EventLosingFocus, htmlElementEventArgs);
			}

			// Token: 0x06006815 RID: 26645 RVA: 0x0018398C File Offset: 0x00181B8C
			public void onblur(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlElement.EventLostFocus, htmlElementEventArgs);
			}

			// Token: 0x06006816 RID: 26646 RVA: 0x000070A6 File Offset: 0x000052A6
			public void onresizeend(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006817 RID: 26647 RVA: 0x001839B8 File Offset: 0x00181BB8
			public bool onresizestart(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006818 RID: 26648 RVA: 0x001839E0 File Offset: 0x00181BE0
			public bool onhelp(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006819 RID: 26649 RVA: 0x000070A6 File Offset: 0x000052A6
			public void onmouseout(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x0600681A RID: 26650 RVA: 0x00183A08 File Offset: 0x00181C08
			public bool onselectstart(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x0600681B RID: 26651 RVA: 0x000070A6 File Offset: 0x000052A6
			public void onfilterchange(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x0600681C RID: 26652 RVA: 0x00183A30 File Offset: 0x00181C30
			public bool ondragstart(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x0600681D RID: 26653 RVA: 0x00183A58 File Offset: 0x00181C58
			public bool onbeforeupdate(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x0600681E RID: 26654 RVA: 0x000070A6 File Offset: 0x000052A6
			public void onafterupdate(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x0600681F RID: 26655 RVA: 0x00183A80 File Offset: 0x00181C80
			public bool onrowexit(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006820 RID: 26656 RVA: 0x000070A6 File Offset: 0x000052A6
			public void onrowenter(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006821 RID: 26657 RVA: 0x000070A6 File Offset: 0x000052A6
			public void ondatasetchanged(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006822 RID: 26658 RVA: 0x000070A6 File Offset: 0x000052A6
			public void ondataavailable(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006823 RID: 26659 RVA: 0x000070A6 File Offset: 0x000052A6
			public void ondatasetcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006824 RID: 26660 RVA: 0x000070A6 File Offset: 0x000052A6
			public void onlosecapture(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006825 RID: 26661 RVA: 0x000070A6 File Offset: 0x000052A6
			public void onpropertychange(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006826 RID: 26662 RVA: 0x000070A6 File Offset: 0x000052A6
			public void onscroll(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006827 RID: 26663 RVA: 0x000070A6 File Offset: 0x000052A6
			public void onresize(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006828 RID: 26664 RVA: 0x00183AA8 File Offset: 0x00181CA8
			public bool ondragenter(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006829 RID: 26665 RVA: 0x00183AD0 File Offset: 0x00181CD0
			public bool ondrop(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x0600682A RID: 26666 RVA: 0x00183AF8 File Offset: 0x00181CF8
			public bool onbeforecut(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x0600682B RID: 26667 RVA: 0x00183B20 File Offset: 0x00181D20
			public bool oncut(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x0600682C RID: 26668 RVA: 0x00183B48 File Offset: 0x00181D48
			public bool onbeforecopy(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x0600682D RID: 26669 RVA: 0x00183B70 File Offset: 0x00181D70
			public bool oncopy(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x0600682E RID: 26670 RVA: 0x00183B98 File Offset: 0x00181D98
			public bool onbeforepaste(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x0600682F RID: 26671 RVA: 0x00183BC0 File Offset: 0x00181DC0
			public bool onpaste(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006830 RID: 26672 RVA: 0x00183BE8 File Offset: 0x00181DE8
			public bool oncontextmenu(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006831 RID: 26673 RVA: 0x000070A6 File Offset: 0x000052A6
			public void onrowsdelete(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006832 RID: 26674 RVA: 0x000070A6 File Offset: 0x000052A6
			public void onrowsinserted(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006833 RID: 26675 RVA: 0x000070A6 File Offset: 0x000052A6
			public void oncellchange(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006834 RID: 26676 RVA: 0x000070A6 File Offset: 0x000052A6
			public void onreadystatechange(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006835 RID: 26677 RVA: 0x000070A6 File Offset: 0x000052A6
			public void onlayoutcomplete(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006836 RID: 26678 RVA: 0x000070A6 File Offset: 0x000052A6
			public void onpage(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006837 RID: 26679 RVA: 0x000070A6 File Offset: 0x000052A6
			public void onactivate(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006838 RID: 26680 RVA: 0x000070A6 File Offset: 0x000052A6
			public void ondeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006839 RID: 26681 RVA: 0x00183C10 File Offset: 0x00181E10
			public bool onbeforedeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x0600683A RID: 26682 RVA: 0x00183C38 File Offset: 0x00181E38
			public bool onbeforeactivate(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x0600683B RID: 26683 RVA: 0x000070A6 File Offset: 0x000052A6
			public void onmove(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x0600683C RID: 26684 RVA: 0x00183C60 File Offset: 0x00181E60
			public bool oncontrolselect(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x0600683D RID: 26685 RVA: 0x00183C88 File Offset: 0x00181E88
			public bool onmovestart(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x0600683E RID: 26686 RVA: 0x000070A6 File Offset: 0x000052A6
			public void onmoveend(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x0600683F RID: 26687 RVA: 0x00183CB0 File Offset: 0x00181EB0
			public bool onmousewheel(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006840 RID: 26688 RVA: 0x00183CD8 File Offset: 0x00181ED8
			public bool onchange(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006841 RID: 26689 RVA: 0x000070A6 File Offset: 0x000052A6
			public void onselect(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006842 RID: 26690 RVA: 0x000070A6 File Offset: 0x000052A6
			public void onload(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006843 RID: 26691 RVA: 0x000070A6 File Offset: 0x000052A6
			public void onerror(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006844 RID: 26692 RVA: 0x000070A6 File Offset: 0x000052A6
			public void onabort(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006845 RID: 26693 RVA: 0x00183D00 File Offset: 0x00181F00
			public bool onsubmit(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006846 RID: 26694 RVA: 0x00183D28 File Offset: 0x00181F28
			public bool onreset(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006847 RID: 26695 RVA: 0x000070A6 File Offset: 0x000052A6
			public void onchange_void(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006848 RID: 26696 RVA: 0x000070A6 File Offset: 0x000052A6
			public void onbounce(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006849 RID: 26697 RVA: 0x000070A6 File Offset: 0x000052A6
			public void onfinish(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x0600684A RID: 26698 RVA: 0x000070A6 File Offset: 0x000052A6
			public void onstart(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x04003ADF RID: 15071
			private HtmlElement parent;
		}

		// Token: 0x020006A7 RID: 1703
		internal class HtmlElementShim : HtmlShim
		{
			// Token: 0x0600684B RID: 26699 RVA: 0x00183D50 File Offset: 0x00181F50
			public HtmlElementShim(HtmlElement element)
			{
				this.htmlElement = element;
				if (this.htmlElement != null)
				{
					HtmlDocument document = this.htmlElement.Document;
					if (document != null)
					{
						HtmlWindow window = document.Window;
						if (window != null)
						{
							this.associatedWindow = window.NativeHtmlWindow;
						}
					}
				}
			}

			// Token: 0x1700168D RID: 5773
			// (get) Token: 0x0600684C RID: 26700 RVA: 0x00183DA9 File Offset: 0x00181FA9
			public UnsafeNativeMethods.IHTMLElement NativeHtmlElement
			{
				get
				{
					return this.htmlElement.NativeHtmlElement;
				}
			}

			// Token: 0x1700168E RID: 5774
			// (get) Token: 0x0600684D RID: 26701 RVA: 0x00183DB6 File Offset: 0x00181FB6
			internal HtmlElement Element
			{
				get
				{
					return this.htmlElement;
				}
			}

			// Token: 0x1700168F RID: 5775
			// (get) Token: 0x0600684E RID: 26702 RVA: 0x00183DBE File Offset: 0x00181FBE
			public override UnsafeNativeMethods.IHTMLWindow2 AssociatedWindow
			{
				get
				{
					return this.associatedWindow;
				}
			}

			// Token: 0x0600684F RID: 26703 RVA: 0x00183DC8 File Offset: 0x00181FC8
			public override void AttachEventHandler(string eventName, EventHandler eventHandler)
			{
				HtmlToClrEventProxy htmlToClrEventProxy = base.AddEventProxy(eventName, eventHandler);
				bool flag = ((UnsafeNativeMethods.IHTMLElement2)this.NativeHtmlElement).AttachEvent(eventName, htmlToClrEventProxy);
			}

			// Token: 0x06006850 RID: 26704 RVA: 0x00183DF4 File Offset: 0x00181FF4
			public override void ConnectToEvents()
			{
				if (this.cookie == null || !this.cookie.Connected)
				{
					int num = 0;
					while (num < HtmlElement.HtmlElementShim.dispInterfaceTypes.Length && this.cookie == null)
					{
						this.cookie = new AxHost.ConnectionPointCookie(this.NativeHtmlElement, new HtmlElement.HTMLElementEvents2(this.htmlElement), HtmlElement.HtmlElementShim.dispInterfaceTypes[num], false);
						if (!this.cookie.Connected)
						{
							this.cookie = null;
						}
						num++;
					}
				}
			}

			// Token: 0x06006851 RID: 26705 RVA: 0x00183E68 File Offset: 0x00182068
			public override void DetachEventHandler(string eventName, EventHandler eventHandler)
			{
				HtmlToClrEventProxy htmlToClrEventProxy = base.RemoveEventProxy(eventHandler);
				if (htmlToClrEventProxy != null)
				{
					((UnsafeNativeMethods.IHTMLElement2)this.NativeHtmlElement).DetachEvent(eventName, htmlToClrEventProxy);
				}
			}

			// Token: 0x06006852 RID: 26706 RVA: 0x00183E92 File Offset: 0x00182092
			public override void DisconnectFromEvents()
			{
				if (this.cookie != null)
				{
					this.cookie.Disconnect();
					this.cookie = null;
				}
			}

			// Token: 0x06006853 RID: 26707 RVA: 0x00183EAE File Offset: 0x001820AE
			protected override void Dispose(bool disposing)
			{
				base.Dispose(disposing);
				if (this.htmlElement != null)
				{
					Marshal.FinalReleaseComObject(this.htmlElement.NativeHtmlElement);
				}
				this.htmlElement = null;
			}

			// Token: 0x06006854 RID: 26708 RVA: 0x00183DB6 File Offset: 0x00181FB6
			protected override object GetEventSender()
			{
				return this.htmlElement;
			}

			// Token: 0x04003AE0 RID: 15072
			private static Type[] dispInterfaceTypes = new Type[]
			{
				typeof(UnsafeNativeMethods.DHTMLElementEvents2),
				typeof(UnsafeNativeMethods.DHTMLAnchorEvents2),
				typeof(UnsafeNativeMethods.DHTMLAreaEvents2),
				typeof(UnsafeNativeMethods.DHTMLButtonElementEvents2),
				typeof(UnsafeNativeMethods.DHTMLControlElementEvents2),
				typeof(UnsafeNativeMethods.DHTMLFormElementEvents2),
				typeof(UnsafeNativeMethods.DHTMLFrameSiteEvents2),
				typeof(UnsafeNativeMethods.DHTMLImgEvents2),
				typeof(UnsafeNativeMethods.DHTMLInputFileElementEvents2),
				typeof(UnsafeNativeMethods.DHTMLInputImageEvents2),
				typeof(UnsafeNativeMethods.DHTMLInputTextElementEvents2),
				typeof(UnsafeNativeMethods.DHTMLLabelEvents2),
				typeof(UnsafeNativeMethods.DHTMLLinkElementEvents2),
				typeof(UnsafeNativeMethods.DHTMLMapEvents2),
				typeof(UnsafeNativeMethods.DHTMLMarqueeElementEvents2),
				typeof(UnsafeNativeMethods.DHTMLOptionButtonElementEvents2),
				typeof(UnsafeNativeMethods.DHTMLSelectElementEvents2),
				typeof(UnsafeNativeMethods.DHTMLStyleElementEvents2),
				typeof(UnsafeNativeMethods.DHTMLTableEvents2),
				typeof(UnsafeNativeMethods.DHTMLTextContainerEvents2),
				typeof(UnsafeNativeMethods.DHTMLScriptEvents2)
			};

			// Token: 0x04003AE1 RID: 15073
			private AxHost.ConnectionPointCookie cookie;

			// Token: 0x04003AE2 RID: 15074
			private HtmlElement htmlElement;

			// Token: 0x04003AE3 RID: 15075
			private UnsafeNativeMethods.IHTMLWindow2 associatedWindow;
		}
	}
}
