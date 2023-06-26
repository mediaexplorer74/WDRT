using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Permissions;
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>Enables the user to navigate Web pages inside your form.</summary>
	// Token: 0x0200042F RID: 1071
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultProperty("Url")]
	[DefaultEvent("DocumentCompleted")]
	[Docking(DockingBehavior.AutoDock)]
	[SRDescription("DescriptionWebBrowser")]
	[Designer("System.Windows.Forms.Design.WebBrowserDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class WebBrowser : WebBrowserBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.WebBrowser" /> class.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Windows.Forms.WebBrowser" /> control is hosted inside Internet Explorer.</exception>
		// Token: 0x06004A1C RID: 18972 RVA: 0x0013760C File Offset: 0x0013580C
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public WebBrowser()
			: base("8856f961-340a-11d0-a96b-00c04fd705a2")
		{
			this.CheckIfCreatedInIE();
			this.webBrowserState = new BitVector32(37);
			this.AllowNavigation = true;
		}

		/// <summary>Gets or sets a value indicating whether the control can navigate to another page after its initial page has been loaded.</summary>
		/// <returns>
		///   <see langword="true" /> if the control can navigate to another page; otherwise, <see langword="false" />.</returns>
		// Token: 0x17001230 RID: 4656
		// (get) Token: 0x06004A1D RID: 18973 RVA: 0x0013763E File Offset: 0x0013583E
		// (set) Token: 0x06004A1E RID: 18974 RVA: 0x0013764D File Offset: 0x0013584D
		[SRDescription("WebBrowserAllowNavigationDescr")]
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		public bool AllowNavigation
		{
			get
			{
				return this.webBrowserState[64];
			}
			set
			{
				this.webBrowserState[64] = value;
				if (this.webBrowserEvent != null)
				{
					this.webBrowserEvent.AllowNavigation = value;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.WebBrowser" /> control navigates to documents that are dropped onto it.</summary>
		/// <returns>
		///   <see langword="true" /> if the control accepts documents that are dropped onto it; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the <see langword="IWebBrowser2" /> interface could not be retrieved from the underlying ActiveX <see langword="WebBrowser" /> control.</exception>
		// Token: 0x17001231 RID: 4657
		// (get) Token: 0x06004A1F RID: 18975 RVA: 0x00137671 File Offset: 0x00135871
		// (set) Token: 0x06004A20 RID: 18976 RVA: 0x0013767E File Offset: 0x0013587E
		[SRDescription("WebBrowserAllowWebBrowserDropDescr")]
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		public bool AllowWebBrowserDrop
		{
			get
			{
				return this.AxIWebBrowser2.RegisterAsDropTarget;
			}
			set
			{
				if (value != this.AllowWebBrowserDrop)
				{
					this.AxIWebBrowser2.RegisterAsDropTarget = value;
					this.Refresh();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.WebBrowser" /> displays dialog boxes such as script error messages.</summary>
		/// <returns>
		///   <see langword="true" /> if the control does not display its dialog boxes; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the IWebBrowser2 interface could not be retrieved from the underlying ActiveX WebBrowser control.</exception>
		// Token: 0x17001232 RID: 4658
		// (get) Token: 0x06004A21 RID: 18977 RVA: 0x0013769B File Offset: 0x0013589B
		// (set) Token: 0x06004A22 RID: 18978 RVA: 0x001376A8 File Offset: 0x001358A8
		[SRDescription("WebBrowserScriptErrorsSuppressedDescr")]
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		public bool ScriptErrorsSuppressed
		{
			get
			{
				return this.AxIWebBrowser2.Silent;
			}
			set
			{
				if (value != this.ScriptErrorsSuppressed)
				{
					this.AxIWebBrowser2.Silent = value;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether keyboard shortcuts are enabled within the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
		/// <returns>
		///   <see langword="true" /> if keyboard shortcuts are enabled within the control; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17001233 RID: 4659
		// (get) Token: 0x06004A23 RID: 18979 RVA: 0x001376BF File Offset: 0x001358BF
		// (set) Token: 0x06004A24 RID: 18980 RVA: 0x001376CD File Offset: 0x001358CD
		[SRDescription("WebBrowserWebBrowserShortcutsEnabledDescr")]
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		public bool WebBrowserShortcutsEnabled
		{
			get
			{
				return this.webBrowserState[1];
			}
			set
			{
				this.webBrowserState[1] = value;
			}
		}

		/// <summary>Gets a value indicating whether a previous page in navigation history is available, which allows the <see cref="M:System.Windows.Forms.WebBrowser.GoBack" /> method to succeed.</summary>
		/// <returns>
		///   <see langword="true" /> if the control can navigate backward; otherwise, <see langword="false" />.</returns>
		// Token: 0x17001234 RID: 4660
		// (get) Token: 0x06004A25 RID: 18981 RVA: 0x001376DC File Offset: 0x001358DC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanGoBack
		{
			get
			{
				return this.CanGoBackInternal;
			}
		}

		// Token: 0x17001235 RID: 4661
		// (get) Token: 0x06004A26 RID: 18982 RVA: 0x001376E4 File Offset: 0x001358E4
		// (set) Token: 0x06004A27 RID: 18983 RVA: 0x001376F2 File Offset: 0x001358F2
		internal bool CanGoBackInternal
		{
			get
			{
				return this.webBrowserState[8];
			}
			set
			{
				if (value != this.CanGoBackInternal)
				{
					this.webBrowserState[8] = value;
					this.OnCanGoBackChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets a value indicating whether a subsequent page in navigation history is available, which allows the <see cref="M:System.Windows.Forms.WebBrowser.GoForward" /> method to succeed.</summary>
		/// <returns>
		///   <see langword="true" /> if the control can navigate forward; otherwise, <see langword="false" />.</returns>
		// Token: 0x17001236 RID: 4662
		// (get) Token: 0x06004A28 RID: 18984 RVA: 0x00137715 File Offset: 0x00135915
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanGoForward
		{
			get
			{
				return this.CanGoForwardInternal;
			}
		}

		// Token: 0x17001237 RID: 4663
		// (get) Token: 0x06004A29 RID: 18985 RVA: 0x0013771D File Offset: 0x0013591D
		// (set) Token: 0x06004A2A RID: 18986 RVA: 0x0013772C File Offset: 0x0013592C
		internal bool CanGoForwardInternal
		{
			get
			{
				return this.webBrowserState[16];
			}
			set
			{
				if (value != this.CanGoForwardInternal)
				{
					this.webBrowserState[16] = value;
					this.OnCanGoForwardChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets an <see cref="T:System.Windows.Forms.HtmlDocument" /> representing the Web page currently displayed in the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.HtmlDocument" /> representing the current page, or <see langword="null" /> if no page is loaded.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the <see langword="IWebBrowser2" /> interface could not be retrieved from the underlying ActiveX <see langword="WebBrowser" /> control.</exception>
		// Token: 0x17001238 RID: 4664
		// (get) Token: 0x06004A2B RID: 18987 RVA: 0x00137750 File Offset: 0x00135950
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public HtmlDocument Document
		{
			get
			{
				object document = this.AxIWebBrowser2.Document;
				if (document != null)
				{
					UnsafeNativeMethods.IHTMLDocument2 ihtmldocument = null;
					try
					{
						ihtmldocument = document as UnsafeNativeMethods.IHTMLDocument2;
					}
					catch (InvalidCastException)
					{
					}
					if (ihtmldocument != null)
					{
						UnsafeNativeMethods.IHTMLLocation location = ihtmldocument.GetLocation();
						if (location != null)
						{
							string href = location.GetHref();
							if (!string.IsNullOrEmpty(href))
							{
								Uri uri = new Uri(href);
								WebBrowser.EnsureUrlConnectPermission(uri);
								return new HtmlDocument(this.ShimManager, ihtmldocument as UnsafeNativeMethods.IHTMLDocument);
							}
						}
					}
				}
				return null;
			}
		}

		/// <summary>Gets or sets a stream containing the contents of the Web page displayed in the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> containing the contents of the current Web page, or <see langword="null" /> if no page is loaded. The default is <see langword="null" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the <see langword="IWebBrowser2" /> interface could not be retrieved from the underlying ActiveX <see langword="WebBrowser" /> control.</exception>
		// Token: 0x17001239 RID: 4665
		// (get) Token: 0x06004A2C RID: 18988 RVA: 0x001377C8 File Offset: 0x001359C8
		// (set) Token: 0x06004A2D RID: 18989 RVA: 0x00137824 File Offset: 0x00135A24
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Stream DocumentStream
		{
			get
			{
				HtmlDocument document = this.Document;
				if (document == null)
				{
					return null;
				}
				UnsafeNativeMethods.IPersistStreamInit persistStreamInit = document.DomDocument as UnsafeNativeMethods.IPersistStreamInit;
				if (persistStreamInit == null)
				{
					return null;
				}
				MemoryStream memoryStream = new MemoryStream();
				UnsafeNativeMethods.IStream stream = new UnsafeNativeMethods.ComStreamFromDataStream(memoryStream);
				persistStreamInit.Save(stream, false);
				return new MemoryStream(memoryStream.GetBuffer(), 0, (int)memoryStream.Length, false);
			}
			set
			{
				this.documentStreamToSetOnLoad = value;
				try
				{
					this.webBrowserState[2] = true;
					this.Url = new Uri("about:blank");
				}
				finally
				{
					this.webBrowserState[2] = false;
				}
			}
		}

		/// <summary>Gets or sets the HTML contents of the page displayed in the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
		/// <returns>The HTML text of the displayed page, or the empty string ("") if no document is loaded.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the <see langword="IWebBrowser2" /> interface could not be retrieved from the underlying ActiveX <see langword="WebBrowser" /> control.</exception>
		// Token: 0x1700123A RID: 4666
		// (get) Token: 0x06004A2E RID: 18990 RVA: 0x00137878 File Offset: 0x00135A78
		// (set) Token: 0x06004A2F RID: 18991 RVA: 0x001378AC File Offset: 0x00135AAC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string DocumentText
		{
			get
			{
				Stream documentStream = this.DocumentStream;
				if (documentStream == null)
				{
					return "";
				}
				StreamReader streamReader = new StreamReader(documentStream);
				documentStream.Position = 0L;
				return streamReader.ReadToEnd();
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				MemoryStream memoryStream = new MemoryStream(value.Length);
				StreamWriter streamWriter = new StreamWriter(memoryStream, Encoding.UTF8);
				streamWriter.Write(value);
				streamWriter.Flush();
				memoryStream.Position = 0L;
				this.DocumentStream = memoryStream;
			}
		}

		/// <summary>Gets the title of the document currently displayed in the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
		/// <returns>The title of the current document, or the empty string ("") if no document is loaded.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the <see langword="IWebBrowser2" /> interface could not be retrieved from the underlying ActiveX <see langword="WebBrowser" /> control.</exception>
		// Token: 0x1700123B RID: 4667
		// (get) Token: 0x06004A30 RID: 18992 RVA: 0x001378F8 File Offset: 0x00135AF8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string DocumentTitle
		{
			get
			{
				HtmlDocument document = this.Document;
				string text;
				if (document == null)
				{
					text = this.AxIWebBrowser2.LocationName;
				}
				else
				{
					UnsafeNativeMethods.IHTMLDocument2 ihtmldocument = document.DomDocument as UnsafeNativeMethods.IHTMLDocument2;
					try
					{
						text = ihtmldocument.GetTitle();
					}
					catch (COMException)
					{
						text = "";
					}
				}
				return text;
			}
		}

		/// <summary>Gets the type of the document currently displayed in the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
		/// <returns>The type of the current document.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the <see langword="IWebBrowser2" /> interface could not be retrieved from the underlying ActiveX <see langword="WebBrowser" /> control.</exception>
		// Token: 0x1700123C RID: 4668
		// (get) Token: 0x06004A31 RID: 18993 RVA: 0x00137954 File Offset: 0x00135B54
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string DocumentType
		{
			get
			{
				string text = "";
				HtmlDocument document = this.Document;
				if (document != null)
				{
					UnsafeNativeMethods.IHTMLDocument2 ihtmldocument = document.DomDocument as UnsafeNativeMethods.IHTMLDocument2;
					try
					{
						text = ihtmldocument.GetMimeType();
					}
					catch (COMException)
					{
						text = "";
					}
				}
				return text;
			}
		}

		/// <summary>Gets a value indicating the encryption method used by the document currently displayed in the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.WebBrowserEncryptionLevel" /> values.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the <see langword="IWebBrowser2" /> interface could not be retrieved from the underlying ActiveX <see langword="WebBrowser" /> control.</exception>
		// Token: 0x1700123D RID: 4669
		// (get) Token: 0x06004A32 RID: 18994 RVA: 0x001379A8 File Offset: 0x00135BA8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public WebBrowserEncryptionLevel EncryptionLevel
		{
			get
			{
				if (this.Document == null)
				{
					this.encryptionLevel = WebBrowserEncryptionLevel.Unknown;
				}
				return this.encryptionLevel;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.WebBrowser" /> control is currently loading a new document.</summary>
		/// <returns>
		///   <see langword="true" /> if the control is busy loading a document; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the <see langword="IWebBrowser2" /> interface could not be retrieved from the underlying ActiveX <see langword="WebBrowser" /> control.</exception>
		// Token: 0x1700123E RID: 4670
		// (get) Token: 0x06004A33 RID: 18995 RVA: 0x001379C5 File Offset: 0x00135BC5
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsBusy
		{
			get
			{
				return !(this.Document == null) && this.AxIWebBrowser2.Busy;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.WebBrowser" /> control is in offline mode.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.WebBrowser" /> control is in offline mode; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the <see langword="IWebBrowser2" /> interface could not be retrieved from the underlying ActiveX <see langword="WebBrowser" /> control.</exception>
		// Token: 0x1700123F RID: 4671
		// (get) Token: 0x06004A34 RID: 18996 RVA: 0x001379E2 File Offset: 0x00135BE2
		[SRDescription("WebBrowserIsOfflineDescr")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsOffline
		{
			get
			{
				return this.AxIWebBrowser2.Offline;
			}
		}

		/// <summary>Gets or a sets a value indicating whether the shortcut menu of the <see cref="T:System.Windows.Forms.WebBrowser" /> control is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.WebBrowser" /> control shortcut menu is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17001240 RID: 4672
		// (get) Token: 0x06004A35 RID: 18997 RVA: 0x001379EF File Offset: 0x00135BEF
		// (set) Token: 0x06004A36 RID: 18998 RVA: 0x001379FD File Offset: 0x00135BFD
		[SRDescription("WebBrowserIsWebBrowserContextMenuEnabledDescr")]
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		public bool IsWebBrowserContextMenuEnabled
		{
			get
			{
				return this.webBrowserState[4];
			}
			set
			{
				this.webBrowserState[4] = value;
			}
		}

		/// <summary>Gets or sets an object that can be accessed by scripting code that is contained within a Web page displayed in the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
		/// <returns>The object being made available to the scripting code.</returns>
		/// <exception cref="T:System.ArgumentException">The specified value when setting this property is an instance of a non-public type.  
		///  -or-  
		///  The specified value when setting this property is an instance of a type that is not COM-visible. For more information, see <see cref="M:System.Runtime.InteropServices.Marshal.IsTypeVisibleFromCom(System.Type)" />.</exception>
		// Token: 0x17001241 RID: 4673
		// (get) Token: 0x06004A37 RID: 18999 RVA: 0x00137A0C File Offset: 0x00135C0C
		// (set) Token: 0x06004A38 RID: 19000 RVA: 0x00137A14 File Offset: 0x00135C14
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object ObjectForScripting
		{
			get
			{
				return this.objectForScripting;
			}
			set
			{
				if (value != null)
				{
					Type type = value.GetType();
					if (!Marshal.IsTypeVisibleFromCom(type))
					{
						throw new ArgumentException(SR.GetString("WebBrowserObjectForScriptingComVisibleOnly"));
					}
				}
				this.objectForScripting = value;
			}
		}

		/// <summary>This property is not meaningful for this control.</summary>
		/// <returns>
		///   <see cref="F:System.Windows.Forms.Padding.Empty" />
		/// </returns>
		// Token: 0x17001242 RID: 4674
		// (get) Token: 0x06004A39 RID: 19001 RVA: 0x00013442 File Offset: 0x00011642
		// (set) Token: 0x06004A3A RID: 19002 RVA: 0x0001344A File Offset: 0x0001164A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.WebBrowser.Padding" /> property changes.</summary>
		// Token: 0x140003BA RID: 954
		// (add) Token: 0x06004A3B RID: 19003 RVA: 0x00013453 File Offset: 0x00011653
		// (remove) Token: 0x06004A3C RID: 19004 RVA: 0x0001345C File Offset: 0x0001165C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRCategory("CatLayout")]
		[SRDescription("ControlOnPaddingChangedDescr")]
		public new event EventHandler PaddingChanged
		{
			add
			{
				base.PaddingChanged += value;
			}
			remove
			{
				base.PaddingChanged -= value;
			}
		}

		/// <summary>Gets a value indicating the current state of the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.WebBrowserReadyState" /> values.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the <see langword="IWebBrowser2" /> interface could not be retrieved from the underlying ActiveX <see langword="WebBrowser" /> control.</exception>
		// Token: 0x17001243 RID: 4675
		// (get) Token: 0x06004A3D RID: 19005 RVA: 0x00137A4A File Offset: 0x00135C4A
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public WebBrowserReadyState ReadyState
		{
			get
			{
				if (this.Document == null)
				{
					return WebBrowserReadyState.Uninitialized;
				}
				return this.AxIWebBrowser2.ReadyState;
			}
		}

		/// <summary>Gets the status text of the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
		/// <returns>The status text.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the <see langword="IWebBrowser2" /> interface could not be retrieved from the underlying ActiveX <see langword="WebBrowser" /> control.</exception>
		// Token: 0x17001244 RID: 4676
		// (get) Token: 0x06004A3E RID: 19006 RVA: 0x00137A67 File Offset: 0x00135C67
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string StatusText
		{
			get
			{
				if (this.Document == null)
				{
					this.statusText = "";
				}
				return this.statusText;
			}
		}

		/// <summary>Gets or sets the URL of the current document.</summary>
		/// <returns>A <see cref="T:System.Uri" /> representing the URL of the current document.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the <see langword="IWebBrowser2" /> interface could not be retrieved from the underlying ActiveX <see langword="WebBrowser" /> control.</exception>
		/// <exception cref="T:System.ArgumentException">The specified value when setting this property is not an absolute URI. For more information, see <see cref="P:System.Uri.IsAbsoluteUri" />.</exception>
		// Token: 0x17001245 RID: 4677
		// (get) Token: 0x06004A3F RID: 19007 RVA: 0x00137A88 File Offset: 0x00135C88
		// (set) Token: 0x06004A40 RID: 19008 RVA: 0x00137ACC File Offset: 0x00135CCC
		[SRDescription("WebBrowserUrlDescr")]
		[Bindable(true)]
		[SRCategory("CatBehavior")]
		[TypeConverter(typeof(WebBrowserUriTypeConverter))]
		[DefaultValue(null)]
		public Uri Url
		{
			get
			{
				string locationURL = this.AxIWebBrowser2.LocationURL;
				if (string.IsNullOrEmpty(locationURL))
				{
					return null;
				}
				Uri uri;
				try
				{
					uri = new Uri(locationURL);
				}
				catch (UriFormatException)
				{
					uri = null;
				}
				return uri;
			}
			set
			{
				if (value != null && value.ToString() == "")
				{
					value = null;
				}
				this.PerformNavigateHelper(this.ReadyNavigateToUrl(value), false, null, null, null);
			}
		}

		/// <summary>Gets the version of Internet Explorer installed.</summary>
		/// <returns>A <see cref="T:System.Version" /> object representing the version of Internet Explorer installed.</returns>
		// Token: 0x17001246 RID: 4678
		// (get) Token: 0x06004A41 RID: 19009 RVA: 0x00137B00 File Offset: 0x00135D00
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Version Version
		{
			get
			{
				string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "mshtml.dll");
				FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(text);
				return new Version(versionInfo.FileMajorPart, versionInfo.FileMinorPart, versionInfo.FileBuildPart, versionInfo.FilePrivatePart);
			}
		}

		/// <summary>Navigates the <see cref="T:System.Windows.Forms.WebBrowser" /> control to the previous page in the navigation history, if one is available.</summary>
		/// <returns>
		///   <see langword="true" /> if the navigation succeeds; <see langword="false" /> if a previous page in the navigation history is not available.</returns>
		// Token: 0x06004A42 RID: 19010 RVA: 0x00137B44 File Offset: 0x00135D44
		public bool GoBack()
		{
			bool flag = true;
			try
			{
				this.AxIWebBrowser2.GoBack();
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsSecurityOrCriticalException(ex))
				{
					throw;
				}
				flag = false;
			}
			return flag;
		}

		/// <summary>Navigates the <see cref="T:System.Windows.Forms.WebBrowser" /> control to the next page in the navigation history, if one is available.</summary>
		/// <returns>
		///   <see langword="true" /> if the navigation succeeds; <see langword="false" /> if a subsequent page in the navigation history is not available.</returns>
		// Token: 0x06004A43 RID: 19011 RVA: 0x00137B80 File Offset: 0x00135D80
		public bool GoForward()
		{
			bool flag = true;
			try
			{
				this.AxIWebBrowser2.GoForward();
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsSecurityOrCriticalException(ex))
				{
					throw;
				}
				flag = false;
			}
			return flag;
		}

		/// <summary>Navigates the <see cref="T:System.Windows.Forms.WebBrowser" /> control to the home page of the current user.</summary>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the <see langword="IWebBrowser2" /> interface could not be retrieved from the underlying ActiveX <see langword="WebBrowser" /> control.</exception>
		// Token: 0x06004A44 RID: 19012 RVA: 0x00137BBC File Offset: 0x00135DBC
		public void GoHome()
		{
			this.AxIWebBrowser2.GoHome();
		}

		/// <summary>Navigates the <see cref="T:System.Windows.Forms.WebBrowser" /> control to the default search page of the current user.</summary>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the <see langword="IWebBrowser2" /> interface could not be retrieved from the underlying ActiveX <see langword="WebBrowser" /> control.</exception>
		// Token: 0x06004A45 RID: 19013 RVA: 0x00137BC9 File Offset: 0x00135DC9
		public void GoSearch()
		{
			this.AxIWebBrowser2.GoSearch();
		}

		/// <summary>Loads the document at the location indicated by the specified <see cref="T:System.Uri" /> into the <see cref="T:System.Windows.Forms.WebBrowser" /> control, replacing the previous document.</summary>
		/// <param name="url">A <see cref="T:System.Uri" /> representing the URL of the document to load.</param>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the <see langword="IWebBrowser2" /> interface could not be retrieved from the underlying ActiveX <see langword="WebBrowser" /> control.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="url" /> parameter value does not represent an absolute URI. For more information, see <see cref="P:System.Uri.IsAbsoluteUri" />.</exception>
		// Token: 0x06004A46 RID: 19014 RVA: 0x00137BD6 File Offset: 0x00135DD6
		public void Navigate(Uri url)
		{
			this.Url = url;
		}

		/// <summary>Loads the document at the specified Uniform Resource Locator (URL) into the <see cref="T:System.Windows.Forms.WebBrowser" /> control, replacing the previous document.</summary>
		/// <param name="urlString">The URL of the document to load.</param>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the <see langword="IWebBrowser2" /> interface could not be retrieved from the underlying ActiveX <see langword="WebBrowser" /> control.</exception>
		// Token: 0x06004A47 RID: 19015 RVA: 0x00137BDF File Offset: 0x00135DDF
		public void Navigate(string urlString)
		{
			this.PerformNavigateHelper(this.ReadyNavigateToUrl(urlString), false, null, null, null);
		}

		/// <summary>Loads the document at the location indicated by the specified <see cref="T:System.Uri" /> into the <see cref="T:System.Windows.Forms.WebBrowser" /> control, replacing the contents of the Web page frame with the specified name.</summary>
		/// <param name="url">A <see cref="T:System.Uri" /> representing the URL of the document to load.</param>
		/// <param name="targetFrameName">The name of the frame in which to load the document.</param>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the <see langword="IWebBrowser2" /> interface could not be retrieved from the underlying ActiveX <see langword="WebBrowser" /> control.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="url" /> parameter value does not represent an absolute URI. For more information, see <see cref="P:System.Uri.IsAbsoluteUri" />.</exception>
		// Token: 0x06004A48 RID: 19016 RVA: 0x00137BF2 File Offset: 0x00135DF2
		public void Navigate(Uri url, string targetFrameName)
		{
			this.PerformNavigateHelper(this.ReadyNavigateToUrl(url), false, targetFrameName, null, null);
		}

		/// <summary>Loads the document at the specified Uniform Resource Locator (URL) into the <see cref="T:System.Windows.Forms.WebBrowser" /> control, replacing the contents of the Web page frame with the specified name.</summary>
		/// <param name="urlString">The URL of the document to load.</param>
		/// <param name="targetFrameName">The name of the frame in which to load the document.</param>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the <see langword="IWebBrowser2" /> interface could not be retrieved from the underlying ActiveX <see langword="WebBrowser" /> control.</exception>
		// Token: 0x06004A49 RID: 19017 RVA: 0x00137C05 File Offset: 0x00135E05
		public void Navigate(string urlString, string targetFrameName)
		{
			this.PerformNavigateHelper(this.ReadyNavigateToUrl(urlString), false, targetFrameName, null, null);
		}

		/// <summary>Loads the document at the location indicated by the specified <see cref="T:System.Uri" /> into a new browser window or into the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
		/// <param name="url">A <see cref="T:System.Uri" /> representing the URL of the document to load.</param>
		/// <param name="newWindow">
		///   <see langword="true" /> to load the document into a new browser window; <see langword="false" /> to load the document into the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</param>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the <see langword="IWebBrowser2" /> interface could not be retrieved from the underlying ActiveX <see langword="WebBrowser" /> control.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="url" /> parameter value does not represent an absolute URI. For more information, see <see cref="P:System.Uri.IsAbsoluteUri" />.</exception>
		// Token: 0x06004A4A RID: 19018 RVA: 0x00137C18 File Offset: 0x00135E18
		public void Navigate(Uri url, bool newWindow)
		{
			this.PerformNavigateHelper(this.ReadyNavigateToUrl(url), newWindow, null, null, null);
		}

		/// <summary>Loads the document at the specified Uniform Resource Locator (URL) into a new browser window or into the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
		/// <param name="urlString">The URL of the document to load.</param>
		/// <param name="newWindow">
		///   <see langword="true" /> to load the document into a new browser window; <see langword="false" /> to load the document into the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</param>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the <see langword="IWebBrowser2" /> interface could not be retrieved from the underlying ActiveX <see langword="WebBrowser" /> control.</exception>
		// Token: 0x06004A4B RID: 19019 RVA: 0x00137C2B File Offset: 0x00135E2B
		public void Navigate(string urlString, bool newWindow)
		{
			this.PerformNavigateHelper(this.ReadyNavigateToUrl(urlString), newWindow, null, null, null);
		}

		/// <summary>Loads the document at the location indicated by the specified <see cref="T:System.Uri" /> into the <see cref="T:System.Windows.Forms.WebBrowser" /> control, requesting it using the specified HTTP data and replacing the contents of the Web page frame with the specified name.</summary>
		/// <param name="url">A <see cref="T:System.Uri" /> representing the URL of the document to load.</param>
		/// <param name="targetFrameName">The name of the frame in which to load the document.</param>
		/// <param name="postData">HTTP POST data such as form data.</param>
		/// <param name="additionalHeaders">HTTP headers to add to the default headers.</param>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the <see langword="IWebBrowser2" /> interface could not be retrieved from the underlying ActiveX <see langword="WebBrowser" /> control.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="url" /> parameter value does not represent an absolute URI. For more information, see <see cref="P:System.Uri.IsAbsoluteUri" />.</exception>
		// Token: 0x06004A4C RID: 19020 RVA: 0x00137C3E File Offset: 0x00135E3E
		public void Navigate(Uri url, string targetFrameName, byte[] postData, string additionalHeaders)
		{
			this.PerformNavigateHelper(this.ReadyNavigateToUrl(url), false, targetFrameName, postData, additionalHeaders);
		}

		/// <summary>Loads the document at the specified Uniform Resource Locator (URL) into the <see cref="T:System.Windows.Forms.WebBrowser" /> control, requesting it using the specified HTTP data and replacing the contents of the Web page frame with the specified name.</summary>
		/// <param name="urlString">The URL of the document to load.</param>
		/// <param name="targetFrameName">The name of the frame in which to load the document.</param>
		/// <param name="postData">HTTP POST data such as form data.</param>
		/// <param name="additionalHeaders">HTTP headers to add to the default headers.</param>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the <see langword="IWebBrowser2" /> interface could not be retrieved from the underlying ActiveX <see langword="WebBrowser" /> control.</exception>
		// Token: 0x06004A4D RID: 19021 RVA: 0x00137C52 File Offset: 0x00135E52
		public void Navigate(string urlString, string targetFrameName, byte[] postData, string additionalHeaders)
		{
			this.PerformNavigateHelper(this.ReadyNavigateToUrl(urlString), false, targetFrameName, postData, additionalHeaders);
		}

		/// <summary>Prints the document currently displayed in the <see cref="T:System.Windows.Forms.WebBrowser" /> control using the current print and page settings.</summary>
		// Token: 0x06004A4E RID: 19022 RVA: 0x00137C68 File Offset: 0x00135E68
		public void Print()
		{
			IntSecurity.DefaultPrinting.Demand();
			object obj = null;
			try
			{
				this.AxIWebBrowser2.ExecWB(NativeMethods.OLECMDID.OLECMDID_PRINT, NativeMethods.OLECMDEXECOPT.OLECMDEXECOPT_DONTPROMPTUSER, ref obj, IntPtr.Zero);
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsSecurityOrCriticalException(ex))
				{
					throw;
				}
			}
		}

		/// <summary>Reloads the document currently displayed in the <see cref="T:System.Windows.Forms.WebBrowser" /> control by checking the server for an updated version.</summary>
		// Token: 0x06004A4F RID: 19023 RVA: 0x00137CB4 File Offset: 0x00135EB4
		public override void Refresh()
		{
			try
			{
				if (this.ShouldSerializeDocumentText())
				{
					string documentText = this.DocumentText;
					this.AxIWebBrowser2.Refresh();
					this.DocumentText = documentText;
				}
				else
				{
					this.AxIWebBrowser2.Refresh();
				}
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsSecurityOrCriticalException(ex))
				{
					throw;
				}
			}
		}

		/// <summary>Reloads the document currently displayed in the <see cref="T:System.Windows.Forms.WebBrowser" /> control using the specified refresh options.</summary>
		/// <param name="opt">One of the <see cref="T:System.Windows.Forms.WebBrowserRefreshOption" /> values.</param>
		// Token: 0x06004A50 RID: 19024 RVA: 0x00137D10 File Offset: 0x00135F10
		public void Refresh(WebBrowserRefreshOption opt)
		{
			object obj = opt;
			try
			{
				if (this.ShouldSerializeDocumentText())
				{
					string documentText = this.DocumentText;
					this.AxIWebBrowser2.Refresh2(ref obj);
					this.DocumentText = documentText;
				}
				else
				{
					this.AxIWebBrowser2.Refresh2(ref obj);
				}
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsSecurityOrCriticalException(ex))
				{
					throw;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether scroll bars are displayed in the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
		/// <returns>
		///   <see langword="true" /> if scroll bars are displayed in the control; otherwise, <see langword="false" />. The default is true.</returns>
		// Token: 0x17001247 RID: 4679
		// (get) Token: 0x06004A51 RID: 19025 RVA: 0x00137D78 File Offset: 0x00135F78
		// (set) Token: 0x06004A52 RID: 19026 RVA: 0x00137D87 File Offset: 0x00135F87
		[SRDescription("WebBrowserScrollBarsEnabledDescr")]
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		public bool ScrollBarsEnabled
		{
			get
			{
				return this.webBrowserState[32];
			}
			set
			{
				if (value != this.webBrowserState[32])
				{
					this.webBrowserState[32] = value;
					this.Refresh();
				}
			}
		}

		/// <summary>Opens the Internet Explorer Page Setup dialog box.</summary>
		// Token: 0x06004A53 RID: 19027 RVA: 0x00137DB0 File Offset: 0x00135FB0
		public void ShowPageSetupDialog()
		{
			IntSecurity.SafePrinting.Demand();
			object obj = null;
			try
			{
				this.AxIWebBrowser2.ExecWB(NativeMethods.OLECMDID.OLECMDID_PAGESETUP, NativeMethods.OLECMDEXECOPT.OLECMDEXECOPT_PROMPTUSER, ref obj, IntPtr.Zero);
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsSecurityOrCriticalException(ex))
				{
					throw;
				}
			}
		}

		/// <summary>Opens the Internet Explorer Print dialog box without setting header and footer values.</summary>
		// Token: 0x06004A54 RID: 19028 RVA: 0x00137DFC File Offset: 0x00135FFC
		public void ShowPrintDialog()
		{
			IntSecurity.SafePrinting.Demand();
			object obj = null;
			try
			{
				this.AxIWebBrowser2.ExecWB(NativeMethods.OLECMDID.OLECMDID_PRINT, NativeMethods.OLECMDEXECOPT.OLECMDEXECOPT_PROMPTUSER, ref obj, IntPtr.Zero);
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsSecurityOrCriticalException(ex))
				{
					throw;
				}
			}
		}

		/// <summary>Opens the Internet Explorer Print Preview dialog box.</summary>
		// Token: 0x06004A55 RID: 19029 RVA: 0x00137E48 File Offset: 0x00136048
		public void ShowPrintPreviewDialog()
		{
			IntSecurity.SafePrinting.Demand();
			object obj = null;
			try
			{
				this.AxIWebBrowser2.ExecWB(NativeMethods.OLECMDID.OLECMDID_PRINTPREVIEW, NativeMethods.OLECMDEXECOPT.OLECMDEXECOPT_PROMPTUSER, ref obj, IntPtr.Zero);
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsSecurityOrCriticalException(ex))
				{
					throw;
				}
			}
		}

		/// <summary>Opens the Internet Explorer Properties dialog box for the current document.</summary>
		// Token: 0x06004A56 RID: 19030 RVA: 0x00137E94 File Offset: 0x00136094
		public void ShowPropertiesDialog()
		{
			object obj = null;
			try
			{
				this.AxIWebBrowser2.ExecWB(NativeMethods.OLECMDID.OLECMDID_PROPERTIES, NativeMethods.OLECMDEXECOPT.OLECMDEXECOPT_PROMPTUSER, ref obj, IntPtr.Zero);
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsSecurityOrCriticalException(ex))
				{
					throw;
				}
			}
		}

		/// <summary>Opens the Internet Explorer Save Web Page dialog box or the Save dialog box of the hosted document if it is not an HTML page.</summary>
		// Token: 0x06004A57 RID: 19031 RVA: 0x00137ED8 File Offset: 0x001360D8
		public void ShowSaveAsDialog()
		{
			IntSecurity.FileDialogSaveFile.Demand();
			object obj = null;
			try
			{
				this.AxIWebBrowser2.ExecWB(NativeMethods.OLECMDID.OLECMDID_SAVEAS, NativeMethods.OLECMDEXECOPT.OLECMDEXECOPT_DODEFAULT, ref obj, IntPtr.Zero);
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsSecurityOrCriticalException(ex))
				{
					throw;
				}
			}
		}

		/// <summary>Cancels any pending navigation and stops any dynamic page elements, such as background sounds and animations.</summary>
		// Token: 0x06004A58 RID: 19032 RVA: 0x00137F24 File Offset: 0x00136124
		public void Stop()
		{
			try
			{
				this.AxIWebBrowser2.Stop();
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsSecurityOrCriticalException(ex))
				{
					throw;
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.WebBrowser.CanGoBack" /> property value changes.</summary>
		// Token: 0x140003BB RID: 955
		// (add) Token: 0x06004A59 RID: 19033 RVA: 0x00137F5C File Offset: 0x0013615C
		// (remove) Token: 0x06004A5A RID: 19034 RVA: 0x00137F94 File Offset: 0x00136194
		[Browsable(false)]
		[SRCategory("CatPropertyChanged")]
		[SRDescription("WebBrowserCanGoBackChangedDescr")]
		public event EventHandler CanGoBackChanged;

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.WebBrowser.CanGoForward" /> property value changes.</summary>
		// Token: 0x140003BC RID: 956
		// (add) Token: 0x06004A5B RID: 19035 RVA: 0x00137FCC File Offset: 0x001361CC
		// (remove) Token: 0x06004A5C RID: 19036 RVA: 0x00138004 File Offset: 0x00136204
		[Browsable(false)]
		[SRCategory("CatPropertyChanged")]
		[SRDescription("WebBrowserCanGoForwardChangedDescr")]
		public event EventHandler CanGoForwardChanged;

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.WebBrowser" /> control finishes loading a document.</summary>
		// Token: 0x140003BD RID: 957
		// (add) Token: 0x06004A5D RID: 19037 RVA: 0x0013803C File Offset: 0x0013623C
		// (remove) Token: 0x06004A5E RID: 19038 RVA: 0x00138074 File Offset: 0x00136274
		[SRCategory("CatBehavior")]
		[SRDescription("WebBrowserDocumentCompletedDescr")]
		public event WebBrowserDocumentCompletedEventHandler DocumentCompleted;

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.WebBrowser.DocumentTitle" /> property value changes.</summary>
		// Token: 0x140003BE RID: 958
		// (add) Token: 0x06004A5F RID: 19039 RVA: 0x001380AC File Offset: 0x001362AC
		// (remove) Token: 0x06004A60 RID: 19040 RVA: 0x001380E4 File Offset: 0x001362E4
		[Browsable(false)]
		[SRCategory("CatPropertyChanged")]
		[SRDescription("WebBrowserDocumentTitleChangedDescr")]
		public event EventHandler DocumentTitleChanged;

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.WebBrowser" /> control navigates to or away from a Web site that uses encryption.</summary>
		// Token: 0x140003BF RID: 959
		// (add) Token: 0x06004A61 RID: 19041 RVA: 0x0013811C File Offset: 0x0013631C
		// (remove) Token: 0x06004A62 RID: 19042 RVA: 0x00138154 File Offset: 0x00136354
		[Browsable(false)]
		[SRCategory("CatPropertyChanged")]
		[SRDescription("WebBrowserEncryptionLevelChangedDescr")]
		public event EventHandler EncryptionLevelChanged;

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.WebBrowser" /> control downloads a file.</summary>
		// Token: 0x140003C0 RID: 960
		// (add) Token: 0x06004A63 RID: 19043 RVA: 0x0013818C File Offset: 0x0013638C
		// (remove) Token: 0x06004A64 RID: 19044 RVA: 0x001381C4 File Offset: 0x001363C4
		[SRCategory("CatBehavior")]
		[SRDescription("WebBrowserFileDownloadDescr")]
		public event EventHandler FileDownload;

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.WebBrowser" /> control has navigated to a new document and has begun loading it.</summary>
		// Token: 0x140003C1 RID: 961
		// (add) Token: 0x06004A65 RID: 19045 RVA: 0x001381FC File Offset: 0x001363FC
		// (remove) Token: 0x06004A66 RID: 19046 RVA: 0x00138234 File Offset: 0x00136434
		[SRCategory("CatAction")]
		[SRDescription("WebBrowserNavigatedDescr")]
		public event WebBrowserNavigatedEventHandler Navigated;

		/// <summary>Occurs before the <see cref="T:System.Windows.Forms.WebBrowser" /> control navigates to a new document.</summary>
		// Token: 0x140003C2 RID: 962
		// (add) Token: 0x06004A67 RID: 19047 RVA: 0x0013826C File Offset: 0x0013646C
		// (remove) Token: 0x06004A68 RID: 19048 RVA: 0x001382A4 File Offset: 0x001364A4
		[SRCategory("CatAction")]
		[SRDescription("WebBrowserNavigatingDescr")]
		public event WebBrowserNavigatingEventHandler Navigating;

		/// <summary>Occurs before a new browser window is opened.</summary>
		// Token: 0x140003C3 RID: 963
		// (add) Token: 0x06004A69 RID: 19049 RVA: 0x001382DC File Offset: 0x001364DC
		// (remove) Token: 0x06004A6A RID: 19050 RVA: 0x00138314 File Offset: 0x00136514
		[SRCategory("CatAction")]
		[SRDescription("WebBrowserNewWindowDescr")]
		public event CancelEventHandler NewWindow;

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.WebBrowser" /> control has updated information on the download progress of a document it is navigating to.</summary>
		// Token: 0x140003C4 RID: 964
		// (add) Token: 0x06004A6B RID: 19051 RVA: 0x0013834C File Offset: 0x0013654C
		// (remove) Token: 0x06004A6C RID: 19052 RVA: 0x00138384 File Offset: 0x00136584
		[SRCategory("CatAction")]
		[SRDescription("WebBrowserProgressChangedDescr")]
		public event WebBrowserProgressChangedEventHandler ProgressChanged;

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.WebBrowser.StatusText" /> property value changes.</summary>
		// Token: 0x140003C5 RID: 965
		// (add) Token: 0x06004A6D RID: 19053 RVA: 0x001383BC File Offset: 0x001365BC
		// (remove) Token: 0x06004A6E RID: 19054 RVA: 0x001383F4 File Offset: 0x001365F4
		[Browsable(false)]
		[SRCategory("CatPropertyChanged")]
		[SRDescription("WebBrowserStatusTextChangedDescr")]
		public event EventHandler StatusTextChanged;

		/// <summary>Gets a value indicating whether the control or any of its child windows has input focus.</summary>
		/// <returns>
		///   <see langword="true" /> if the control or any of its child windows has input focus; otherwise, <see langword="false" />.</returns>
		// Token: 0x17001248 RID: 4680
		// (get) Token: 0x06004A6F RID: 19055 RVA: 0x0013842C File Offset: 0x0013662C
		public override bool Focused
		{
			get
			{
				if (base.Focused)
				{
					return true;
				}
				IntPtr focus = UnsafeNativeMethods.GetFocus();
				return focus != IntPtr.Zero && SafeNativeMethods.IsChild(new HandleRef(this, base.Handle), new HandleRef(null, focus));
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.WebBrowser" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06004A70 RID: 19056 RVA: 0x00138470 File Offset: 0x00136670
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.htmlShimManager != null)
				{
					this.htmlShimManager.Dispose();
				}
				this.DetachSink();
				base.ActiveXSite.Dispose();
			}
			base.Dispose(disposing);
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>Gets the default size of the control.</returns>
		// Token: 0x17001249 RID: 4681
		// (get) Token: 0x06004A71 RID: 19057 RVA: 0x001384A0 File Offset: 0x001366A0
		protected override Size DefaultSize
		{
			get
			{
				return new Size(250, 250);
			}
		}

		/// <summary>Called by the control when the underlying ActiveX control is created.</summary>
		/// <param name="nativeActiveXObject">An object that represents the underlying ActiveX control.</param>
		// Token: 0x06004A72 RID: 19058 RVA: 0x001384B1 File Offset: 0x001366B1
		protected override void AttachInterfaces(object nativeActiveXObject)
		{
			this.axIWebBrowser2 = (UnsafeNativeMethods.IWebBrowser2)nativeActiveXObject;
		}

		/// <summary>Called by the control when the underlying ActiveX control is discarded.</summary>
		// Token: 0x06004A73 RID: 19059 RVA: 0x001384BF File Offset: 0x001366BF
		protected override void DetachInterfaces()
		{
			this.axIWebBrowser2 = null;
		}

		/// <summary>Returns a reference to the unmanaged <see langword="WebBrowser" /> ActiveX control site, which you can extend to customize the managed <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.WebBrowser.WebBrowserSite" /> that represents the <see langword="WebBrowser" /> ActiveX control site.</returns>
		// Token: 0x06004A74 RID: 19060 RVA: 0x001384C8 File Offset: 0x001366C8
		protected override WebBrowserSiteBase CreateWebBrowserSiteBase()
		{
			return new WebBrowser.WebBrowserSite(this);
		}

		/// <summary>Associates the underlying ActiveX control with a client that can handle control events.</summary>
		// Token: 0x06004A75 RID: 19061 RVA: 0x001384D0 File Offset: 0x001366D0
		protected override void CreateSink()
		{
			object activeXInstance = this.activeXInstance;
			if (activeXInstance != null)
			{
				this.webBrowserEvent = new WebBrowser.WebBrowserEvent(this);
				this.webBrowserEvent.AllowNavigation = this.AllowNavigation;
				this.cookie = new AxHost.ConnectionPointCookie(activeXInstance, this.webBrowserEvent, typeof(UnsafeNativeMethods.DWebBrowserEvents2));
			}
		}

		/// <summary>Releases the event-handling client attached in the <see cref="M:System.Windows.Forms.WebBrowser.CreateSink" /> method from the underlying ActiveX control.</summary>
		// Token: 0x06004A76 RID: 19062 RVA: 0x00138520 File Offset: 0x00136720
		protected override void DetachSink()
		{
			if (this.cookie != null)
			{
				this.cookie.Disconnect();
				this.cookie = null;
			}
		}

		// Token: 0x06004A77 RID: 19063 RVA: 0x0013853C File Offset: 0x0013673C
		internal override void OnTopMostActiveXParentChanged(EventArgs e)
		{
			if (base.TopMostParent.IsIEParent)
			{
				WebBrowser.createdInIE = true;
				this.CheckIfCreatedInIE();
				return;
			}
			WebBrowser.createdInIE = false;
			base.OnTopMostActiveXParentChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.WebBrowser.CanGoBackChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06004A78 RID: 19064 RVA: 0x00138565 File Offset: 0x00136765
		protected virtual void OnCanGoBackChanged(EventArgs e)
		{
			if (this.CanGoBackChanged != null)
			{
				this.CanGoBackChanged(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.WebBrowser.CanGoForwardChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06004A79 RID: 19065 RVA: 0x0013857C File Offset: 0x0013677C
		protected virtual void OnCanGoForwardChanged(EventArgs e)
		{
			if (this.CanGoForwardChanged != null)
			{
				this.CanGoForwardChanged(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.WebBrowser.DocumentCompleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.WebBrowserDocumentCompletedEventArgs" /> that contains the event data.</param>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Windows.Forms.WebBrowser" /> instance is no longer valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">A reference to an implementation of the <see langword="IWebBrowser2" /> interface could not be retrieved from the underlying ActiveX <see langword="WebBrowser" /> control.</exception>
		// Token: 0x06004A7A RID: 19066 RVA: 0x00138593 File Offset: 0x00136793
		protected virtual void OnDocumentCompleted(WebBrowserDocumentCompletedEventArgs e)
		{
			this.AxIWebBrowser2.RegisterAsDropTarget = this.AllowWebBrowserDrop;
			if (this.DocumentCompleted != null)
			{
				this.DocumentCompleted(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.WebBrowser.DocumentTitleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06004A7B RID: 19067 RVA: 0x001385BB File Offset: 0x001367BB
		protected virtual void OnDocumentTitleChanged(EventArgs e)
		{
			if (this.DocumentTitleChanged != null)
			{
				this.DocumentTitleChanged(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.WebBrowser.EncryptionLevelChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06004A7C RID: 19068 RVA: 0x001385D2 File Offset: 0x001367D2
		protected virtual void OnEncryptionLevelChanged(EventArgs e)
		{
			if (this.EncryptionLevelChanged != null)
			{
				this.EncryptionLevelChanged(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.WebBrowser.FileDownload" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data.</param>
		// Token: 0x06004A7D RID: 19069 RVA: 0x001385E9 File Offset: 0x001367E9
		protected virtual void OnFileDownload(EventArgs e)
		{
			if (this.FileDownload != null)
			{
				this.FileDownload(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.WebBrowser.Navigated" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.WebBrowserNavigatedEventArgs" /> that contains the event data.</param>
		// Token: 0x06004A7E RID: 19070 RVA: 0x00138600 File Offset: 0x00136800
		protected virtual void OnNavigated(WebBrowserNavigatedEventArgs e)
		{
			if (this.Navigated != null)
			{
				this.Navigated(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.WebBrowser.Navigating" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.WebBrowserNavigatingEventArgs" /> that contains the event data.</param>
		// Token: 0x06004A7F RID: 19071 RVA: 0x00138617 File Offset: 0x00136817
		protected virtual void OnNavigating(WebBrowserNavigatingEventArgs e)
		{
			if (this.Navigating != null)
			{
				this.Navigating(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.WebBrowser.NewWindow" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data.</param>
		// Token: 0x06004A80 RID: 19072 RVA: 0x0013862E File Offset: 0x0013682E
		protected virtual void OnNewWindow(CancelEventArgs e)
		{
			if (this.NewWindow != null)
			{
				this.NewWindow(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.WebBrowser.ProgressChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.WebBrowserProgressChangedEventArgs" /> that contains the event data.</param>
		// Token: 0x06004A81 RID: 19073 RVA: 0x00138645 File Offset: 0x00136845
		protected virtual void OnProgressChanged(WebBrowserProgressChangedEventArgs e)
		{
			if (this.ProgressChanged != null)
			{
				this.ProgressChanged(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.WebBrowser.StatusTextChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06004A82 RID: 19074 RVA: 0x0013865C File Offset: 0x0013685C
		protected virtual void OnStatusTextChanged(EventArgs e)
		{
			if (this.StatusTextChanged != null)
			{
				this.StatusTextChanged(this, e);
			}
		}

		// Token: 0x1700124A RID: 4682
		// (get) Token: 0x06004A83 RID: 19075 RVA: 0x00138673 File Offset: 0x00136873
		internal HtmlShimManager ShimManager
		{
			get
			{
				if (this.htmlShimManager == null)
				{
					this.htmlShimManager = new HtmlShimManager();
				}
				return this.htmlShimManager;
			}
		}

		// Token: 0x06004A84 RID: 19076 RVA: 0x0013868E File Offset: 0x0013688E
		private void CheckIfCreatedInIE()
		{
			if (!WebBrowser.createdInIE)
			{
				return;
			}
			if (this.ParentInternal != null)
			{
				this.ParentInternal.Controls.Remove(this);
				base.Dispose();
				return;
			}
			base.Dispose();
			throw new NotSupportedException(SR.GetString("WebBrowserInIENotSupported"));
		}

		// Token: 0x06004A85 RID: 19077 RVA: 0x001386D0 File Offset: 0x001368D0
		internal static void EnsureUrlConnectPermission(Uri url)
		{
			WebPermission webPermission = new WebPermission(NetworkAccess.Connect, url.AbsoluteUri);
			webPermission.Demand();
		}

		// Token: 0x06004A86 RID: 19078 RVA: 0x001386F1 File Offset: 0x001368F1
		private string ReadyNavigateToUrl(string urlString)
		{
			if (string.IsNullOrEmpty(urlString))
			{
				urlString = "about:blank";
			}
			if (!this.webBrowserState[2])
			{
				this.documentStreamToSetOnLoad = null;
			}
			return urlString;
		}

		// Token: 0x06004A87 RID: 19079 RVA: 0x00138718 File Offset: 0x00136918
		private string ReadyNavigateToUrl(Uri url)
		{
			string text;
			if (url == null)
			{
				text = "about:blank";
			}
			else
			{
				if (!url.IsAbsoluteUri)
				{
					throw new ArgumentException(SR.GetString("WebBrowserNavigateAbsoluteUri", new object[] { "uri" }));
				}
				text = (url.IsFile ? url.OriginalString : url.AbsoluteUri);
			}
			return this.ReadyNavigateToUrl(text);
		}

		// Token: 0x06004A88 RID: 19080 RVA: 0x0013877C File Offset: 0x0013697C
		private void PerformNavigateHelper(string urlString, bool newWindow, string targetFrameName, byte[] postData, string headers)
		{
			object obj = urlString;
			object obj2 = (newWindow ? 1 : 0);
			object obj3 = targetFrameName;
			object obj4 = postData;
			object obj5 = headers;
			this.PerformNavigate2(ref obj, ref obj2, ref obj3, ref obj4, ref obj5);
		}

		// Token: 0x06004A89 RID: 19081 RVA: 0x001387B4 File Offset: 0x001369B4
		private void PerformNavigate2(ref object URL, ref object flags, ref object targetFrameName, ref object postData, ref object headers)
		{
			try
			{
				this.AxIWebBrowser2.Navigate2(ref URL, ref flags, ref targetFrameName, ref postData, ref headers);
			}
			catch (COMException ex)
			{
				if (ex.ErrorCode != -2147023673)
				{
					throw;
				}
			}
		}

		// Token: 0x06004A8A RID: 19082 RVA: 0x001387F8 File Offset: 0x001369F8
		private bool ShouldSerializeDocumentText()
		{
			return this.IsValidUrl;
		}

		// Token: 0x1700124B RID: 4683
		// (get) Token: 0x06004A8B RID: 19083 RVA: 0x00138800 File Offset: 0x00136A00
		private bool IsValidUrl
		{
			get
			{
				return this.Url == null || this.Url.AbsoluteUri == "about:blank";
			}
		}

		// Token: 0x06004A8C RID: 19084 RVA: 0x00138827 File Offset: 0x00136A27
		private bool ShouldSerializeUrl()
		{
			return !this.ShouldSerializeDocumentText();
		}

		// Token: 0x06004A8D RID: 19085 RVA: 0x00138834 File Offset: 0x00136A34
		private bool ShowContextMenu(int x, int y)
		{
			ContextMenuStrip contextMenuStrip = this.ContextMenuStrip;
			ContextMenu contextMenu = ((contextMenuStrip != null) ? null : this.ContextMenu);
			if (contextMenuStrip == null && contextMenu == null)
			{
				return false;
			}
			bool flag = false;
			Point point;
			if (x == -1)
			{
				flag = true;
				point = new Point(base.Width / 2, base.Height / 2);
			}
			else
			{
				point = base.PointToClientInternal(new Point(x, y));
			}
			if (base.ClientRectangle.Contains(point))
			{
				if (contextMenuStrip != null)
				{
					contextMenuStrip.ShowInternal(this, point, flag);
				}
				else if (contextMenu != null)
				{
					contextMenu.Show(this, point);
				}
				return true;
			}
			return false;
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x06004A8E RID: 19086 RVA: 0x001388BC File Offset: 0x00136ABC
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg == 123)
			{
				int num = NativeMethods.Util.SignedLOWORD(m.LParam);
				int num2 = NativeMethods.Util.SignedHIWORD(m.LParam);
				if (!this.ShowContextMenu(num, num2))
				{
					this.DefWndProc(ref m);
					return;
				}
			}
			else
			{
				base.WndProc(ref m);
			}
		}

		// Token: 0x1700124C RID: 4684
		// (get) Token: 0x06004A8F RID: 19087 RVA: 0x00138908 File Offset: 0x00136B08
		private UnsafeNativeMethods.IWebBrowser2 AxIWebBrowser2
		{
			get
			{
				if (this.axIWebBrowser2 == null)
				{
					if (base.IsDisposed)
					{
						throw new ObjectDisposedException(base.GetType().Name);
					}
					base.TransitionUpTo(WebBrowserHelper.AXState.InPlaceActive);
				}
				if (this.axIWebBrowser2 == null)
				{
					throw new InvalidOperationException(SR.GetString("WebBrowserNoCastToIWebBrowser2"));
				}
				return this.axIWebBrowser2;
			}
		}

		// Token: 0x040027CA RID: 10186
		private static bool createdInIE;

		// Token: 0x040027CB RID: 10187
		private UnsafeNativeMethods.IWebBrowser2 axIWebBrowser2;

		// Token: 0x040027CC RID: 10188
		private AxHost.ConnectionPointCookie cookie;

		// Token: 0x040027CD RID: 10189
		private Stream documentStreamToSetOnLoad;

		// Token: 0x040027CE RID: 10190
		private WebBrowserEncryptionLevel encryptionLevel;

		// Token: 0x040027CF RID: 10191
		private object objectForScripting;

		// Token: 0x040027D0 RID: 10192
		private WebBrowser.WebBrowserEvent webBrowserEvent;

		// Token: 0x040027D1 RID: 10193
		internal string statusText = "";

		// Token: 0x040027D2 RID: 10194
		private const int WEBBROWSERSTATE_webBrowserShortcutsEnabled = 1;

		// Token: 0x040027D3 RID: 10195
		private const int WEBBROWSERSTATE_documentStreamJustSet = 2;

		// Token: 0x040027D4 RID: 10196
		private const int WEBBROWSERSTATE_isWebBrowserContextMenuEnabled = 4;

		// Token: 0x040027D5 RID: 10197
		private const int WEBBROWSERSTATE_canGoBack = 8;

		// Token: 0x040027D6 RID: 10198
		private const int WEBBROWSERSTATE_canGoForward = 16;

		// Token: 0x040027D7 RID: 10199
		private const int WEBBROWSERSTATE_scrollbarsEnabled = 32;

		// Token: 0x040027D8 RID: 10200
		private const int WEBBROWSERSTATE_allowNavigation = 64;

		// Token: 0x040027D9 RID: 10201
		private BitVector32 webBrowserState;

		// Token: 0x040027E5 RID: 10213
		private HtmlShimManager htmlShimManager;

		/// <summary>Represents the host window of a <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
		// Token: 0x02000825 RID: 2085
		[ComVisible(false)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected class WebBrowserSite : WebBrowserSiteBase, UnsafeNativeMethods.IDocHostUIHandler
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.WebBrowser.WebBrowserSite" /> class.</summary>
			/// <param name="host">The <see cref="T:System.Windows.Forms.WebBrowser" /></param>
			// Token: 0x06006FD2 RID: 28626 RVA: 0x00199CCA File Offset: 0x00197ECA
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public WebBrowserSite(WebBrowser host)
				: base(host)
			{
			}

			// Token: 0x06006FD3 RID: 28627 RVA: 0x00199CD4 File Offset: 0x00197ED4
			int UnsafeNativeMethods.IDocHostUIHandler.ShowContextMenu(int dwID, NativeMethods.POINT pt, object pcmdtReserved, object pdispReserved)
			{
				WebBrowser webBrowser = (WebBrowser)base.Host;
				if (webBrowser.IsWebBrowserContextMenuEnabled)
				{
					return 1;
				}
				if (pt.x == 0 && pt.y == 0)
				{
					pt.x = -1;
					pt.y = -1;
				}
				webBrowser.ShowContextMenu(pt.x, pt.y);
				return 0;
			}

			// Token: 0x06006FD4 RID: 28628 RVA: 0x00199D2C File Offset: 0x00197F2C
			int UnsafeNativeMethods.IDocHostUIHandler.GetHostInfo(NativeMethods.DOCHOSTUIINFO info)
			{
				WebBrowser webBrowser = (WebBrowser)base.Host;
				info.dwDoubleClick = 0;
				info.dwFlags = 2097168;
				if (webBrowser.ScrollBarsEnabled)
				{
					info.dwFlags |= 128;
				}
				else
				{
					info.dwFlags |= 8;
				}
				if (Application.RenderWithVisualStyles)
				{
					info.dwFlags |= 262144;
				}
				else
				{
					info.dwFlags |= 524288;
				}
				return 0;
			}

			// Token: 0x06006FD5 RID: 28629 RVA: 0x0003BC68 File Offset: 0x00039E68
			int UnsafeNativeMethods.IDocHostUIHandler.EnableModeless(bool fEnable)
			{
				return -2147467263;
			}

			// Token: 0x06006FD6 RID: 28630 RVA: 0x00012E4E File Offset: 0x0001104E
			int UnsafeNativeMethods.IDocHostUIHandler.ShowUI(int dwID, UnsafeNativeMethods.IOleInPlaceActiveObject activeObject, NativeMethods.IOleCommandTarget commandTarget, UnsafeNativeMethods.IOleInPlaceFrame frame, UnsafeNativeMethods.IOleInPlaceUIWindow doc)
			{
				return 1;
			}

			// Token: 0x06006FD7 RID: 28631 RVA: 0x0003BC68 File Offset: 0x00039E68
			int UnsafeNativeMethods.IDocHostUIHandler.HideUI()
			{
				return -2147467263;
			}

			// Token: 0x06006FD8 RID: 28632 RVA: 0x0003BC68 File Offset: 0x00039E68
			int UnsafeNativeMethods.IDocHostUIHandler.UpdateUI()
			{
				return -2147467263;
			}

			// Token: 0x06006FD9 RID: 28633 RVA: 0x0003BC68 File Offset: 0x00039E68
			int UnsafeNativeMethods.IDocHostUIHandler.OnDocWindowActivate(bool fActivate)
			{
				return -2147467263;
			}

			// Token: 0x06006FDA RID: 28634 RVA: 0x0003BC68 File Offset: 0x00039E68
			int UnsafeNativeMethods.IDocHostUIHandler.OnFrameWindowActivate(bool fActivate)
			{
				return -2147467263;
			}

			// Token: 0x06006FDB RID: 28635 RVA: 0x0003BC68 File Offset: 0x00039E68
			int UnsafeNativeMethods.IDocHostUIHandler.ResizeBorder(NativeMethods.COMRECT rect, UnsafeNativeMethods.IOleInPlaceUIWindow doc, bool fFrameWindow)
			{
				return -2147467263;
			}

			// Token: 0x06006FDC RID: 28636 RVA: 0x0003BC68 File Offset: 0x00039E68
			int UnsafeNativeMethods.IDocHostUIHandler.GetOptionKeyPath(string[] pbstrKey, int dw)
			{
				return -2147467263;
			}

			// Token: 0x06006FDD RID: 28637 RVA: 0x000160D7 File Offset: 0x000142D7
			int UnsafeNativeMethods.IDocHostUIHandler.GetDropTarget(UnsafeNativeMethods.IOleDropTarget pDropTarget, out UnsafeNativeMethods.IOleDropTarget ppDropTarget)
			{
				ppDropTarget = null;
				return -2147467263;
			}

			// Token: 0x06006FDE RID: 28638 RVA: 0x00199DB0 File Offset: 0x00197FB0
			int UnsafeNativeMethods.IDocHostUIHandler.GetExternal(out object ppDispatch)
			{
				WebBrowser webBrowser = (WebBrowser)base.Host;
				ppDispatch = webBrowser.ObjectForScripting;
				return 0;
			}

			// Token: 0x06006FDF RID: 28639 RVA: 0x00199DD4 File Offset: 0x00197FD4
			int UnsafeNativeMethods.IDocHostUIHandler.TranslateAccelerator(ref NativeMethods.MSG msg, ref Guid group, int nCmdID)
			{
				WebBrowser webBrowser = (WebBrowser)base.Host;
				if (webBrowser.WebBrowserShortcutsEnabled)
				{
					return 1;
				}
				int num = (int)msg.wParam | (int)Control.ModifierKeys;
				if (msg.message != 258 && Enum.IsDefined(typeof(Shortcut), (Shortcut)num))
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06006FE0 RID: 28640 RVA: 0x00199E30 File Offset: 0x00198030
			int UnsafeNativeMethods.IDocHostUIHandler.TranslateUrl(int dwTranslate, string strUrlIn, out string pstrUrlOut)
			{
				pstrUrlOut = null;
				return 1;
			}

			// Token: 0x06006FE1 RID: 28641 RVA: 0x00199E36 File Offset: 0x00198036
			int UnsafeNativeMethods.IDocHostUIHandler.FilterDataObject(IDataObject pDO, out IDataObject ppDORet)
			{
				ppDORet = null;
				return 1;
			}

			// Token: 0x06006FE2 RID: 28642 RVA: 0x00199E3C File Offset: 0x0019803C
			internal override void OnPropertyChanged(int dispid)
			{
				if (dispid != -525)
				{
					base.OnPropertyChanged(dispid);
				}
			}
		}

		// Token: 0x02000826 RID: 2086
		[ClassInterface(ClassInterfaceType.None)]
		private class WebBrowserEvent : StandardOleMarshalObject, UnsafeNativeMethods.DWebBrowserEvents2
		{
			// Token: 0x06006FE3 RID: 28643 RVA: 0x00199E4D File Offset: 0x0019804D
			public WebBrowserEvent(WebBrowser parent)
			{
				this.parent = parent;
			}

			// Token: 0x1700187B RID: 6267
			// (get) Token: 0x06006FE4 RID: 28644 RVA: 0x00199E5C File Offset: 0x0019805C
			// (set) Token: 0x06006FE5 RID: 28645 RVA: 0x00199E64 File Offset: 0x00198064
			public bool AllowNavigation
			{
				get
				{
					return this.allowNavigation;
				}
				set
				{
					this.allowNavigation = value;
				}
			}

			// Token: 0x06006FE6 RID: 28646 RVA: 0x00199E6D File Offset: 0x0019806D
			public void CommandStateChange(long command, bool enable)
			{
				if (command == 2L)
				{
					this.parent.CanGoBackInternal = enable;
					return;
				}
				if (command == 1L)
				{
					this.parent.CanGoForwardInternal = enable;
				}
			}

			// Token: 0x06006FE7 RID: 28647 RVA: 0x00199E94 File Offset: 0x00198094
			public void BeforeNavigate2(object pDisp, ref object urlObject, ref object flags, ref object targetFrameName, ref object postData, ref object headers, ref bool cancel)
			{
				if (this.AllowNavigation || !this.haveNavigated)
				{
					if (targetFrameName == null)
					{
						targetFrameName = "";
					}
					if (headers == null)
					{
						headers = "";
					}
					string text = ((urlObject == null) ? "" : ((string)urlObject));
					WebBrowserNavigatingEventArgs webBrowserNavigatingEventArgs = new WebBrowserNavigatingEventArgs(new Uri(text), (targetFrameName == null) ? "" : ((string)targetFrameName));
					this.parent.OnNavigating(webBrowserNavigatingEventArgs);
					cancel = webBrowserNavigatingEventArgs.Cancel;
					return;
				}
				cancel = true;
			}

			// Token: 0x06006FE8 RID: 28648 RVA: 0x00199F18 File Offset: 0x00198118
			public void DocumentComplete(object pDisp, ref object urlObject)
			{
				this.haveNavigated = true;
				if (this.parent.documentStreamToSetOnLoad != null && (string)urlObject == "about:blank")
				{
					HtmlDocument document = this.parent.Document;
					if (document != null)
					{
						UnsafeNativeMethods.IPersistStreamInit persistStreamInit = document.DomDocument as UnsafeNativeMethods.IPersistStreamInit;
						UnsafeNativeMethods.IStream stream = new UnsafeNativeMethods.ComStreamFromDataStream(this.parent.documentStreamToSetOnLoad);
						persistStreamInit.Load(stream);
						document.Encoding = "unicode";
					}
					this.parent.documentStreamToSetOnLoad = null;
					return;
				}
				string text = ((urlObject == null) ? "" : urlObject.ToString());
				WebBrowserDocumentCompletedEventArgs webBrowserDocumentCompletedEventArgs = new WebBrowserDocumentCompletedEventArgs(new Uri(text));
				this.parent.OnDocumentCompleted(webBrowserDocumentCompletedEventArgs);
			}

			// Token: 0x06006FE9 RID: 28649 RVA: 0x00199FCA File Offset: 0x001981CA
			public void TitleChange(string text)
			{
				this.parent.OnDocumentTitleChanged(EventArgs.Empty);
			}

			// Token: 0x06006FEA RID: 28650 RVA: 0x00199FDC File Offset: 0x001981DC
			public void SetSecureLockIcon(int secureLockIcon)
			{
				this.parent.encryptionLevel = (WebBrowserEncryptionLevel)secureLockIcon;
				this.parent.OnEncryptionLevelChanged(EventArgs.Empty);
			}

			// Token: 0x06006FEB RID: 28651 RVA: 0x00199FFC File Offset: 0x001981FC
			public void NavigateComplete2(object pDisp, ref object urlObject)
			{
				string text = ((urlObject == null) ? "" : ((string)urlObject));
				WebBrowserNavigatedEventArgs webBrowserNavigatedEventArgs = new WebBrowserNavigatedEventArgs(new Uri(text));
				this.parent.OnNavigated(webBrowserNavigatedEventArgs);
			}

			// Token: 0x06006FEC RID: 28652 RVA: 0x0019A034 File Offset: 0x00198234
			public void NewWindow2(ref object ppDisp, ref bool cancel)
			{
				CancelEventArgs cancelEventArgs = new CancelEventArgs();
				this.parent.OnNewWindow(cancelEventArgs);
				cancel = cancelEventArgs.Cancel;
			}

			// Token: 0x06006FED RID: 28653 RVA: 0x0019A05C File Offset: 0x0019825C
			public void ProgressChange(int progress, int progressMax)
			{
				WebBrowserProgressChangedEventArgs webBrowserProgressChangedEventArgs = new WebBrowserProgressChangedEventArgs((long)progress, (long)progressMax);
				this.parent.OnProgressChanged(webBrowserProgressChangedEventArgs);
			}

			// Token: 0x06006FEE RID: 28654 RVA: 0x0019A07F File Offset: 0x0019827F
			public void StatusTextChange(string text)
			{
				this.parent.statusText = ((text == null) ? "" : text);
				this.parent.OnStatusTextChanged(EventArgs.Empty);
			}

			// Token: 0x06006FEF RID: 28655 RVA: 0x0019A0A7 File Offset: 0x001982A7
			public void DownloadBegin()
			{
				this.parent.OnFileDownload(EventArgs.Empty);
			}

			// Token: 0x06006FF0 RID: 28656 RVA: 0x000070A6 File Offset: 0x000052A6
			public void FileDownload(ref bool cancel)
			{
			}

			// Token: 0x06006FF1 RID: 28657 RVA: 0x000070A6 File Offset: 0x000052A6
			public void PrivacyImpactedStateChange(bool bImpacted)
			{
			}

			// Token: 0x06006FF2 RID: 28658 RVA: 0x000070A6 File Offset: 0x000052A6
			public void UpdatePageStatus(object pDisp, ref object nPage, ref object fDone)
			{
			}

			// Token: 0x06006FF3 RID: 28659 RVA: 0x000070A6 File Offset: 0x000052A6
			public void PrintTemplateTeardown(object pDisp)
			{
			}

			// Token: 0x06006FF4 RID: 28660 RVA: 0x000070A6 File Offset: 0x000052A6
			public void PrintTemplateInstantiation(object pDisp)
			{
			}

			// Token: 0x06006FF5 RID: 28661 RVA: 0x000070A6 File Offset: 0x000052A6
			public void NavigateError(object pDisp, ref object url, ref object frame, ref object statusCode, ref bool cancel)
			{
			}

			// Token: 0x06006FF6 RID: 28662 RVA: 0x000070A6 File Offset: 0x000052A6
			public void ClientToHostWindow(ref long cX, ref long cY)
			{
			}

			// Token: 0x06006FF7 RID: 28663 RVA: 0x000070A6 File Offset: 0x000052A6
			public void WindowClosing(bool isChildWindow, ref bool cancel)
			{
			}

			// Token: 0x06006FF8 RID: 28664 RVA: 0x000070A6 File Offset: 0x000052A6
			public void WindowSetHeight(int height)
			{
			}

			// Token: 0x06006FF9 RID: 28665 RVA: 0x000070A6 File Offset: 0x000052A6
			public void WindowSetWidth(int width)
			{
			}

			// Token: 0x06006FFA RID: 28666 RVA: 0x000070A6 File Offset: 0x000052A6
			public void WindowSetTop(int top)
			{
			}

			// Token: 0x06006FFB RID: 28667 RVA: 0x000070A6 File Offset: 0x000052A6
			public void WindowSetLeft(int left)
			{
			}

			// Token: 0x06006FFC RID: 28668 RVA: 0x000070A6 File Offset: 0x000052A6
			public void WindowSetResizable(bool resizable)
			{
			}

			// Token: 0x06006FFD RID: 28669 RVA: 0x000070A6 File Offset: 0x000052A6
			public void OnTheaterMode(bool theaterMode)
			{
			}

			// Token: 0x06006FFE RID: 28670 RVA: 0x000070A6 File Offset: 0x000052A6
			public void OnFullScreen(bool fullScreen)
			{
			}

			// Token: 0x06006FFF RID: 28671 RVA: 0x000070A6 File Offset: 0x000052A6
			public void OnStatusBar(bool statusBar)
			{
			}

			// Token: 0x06007000 RID: 28672 RVA: 0x000070A6 File Offset: 0x000052A6
			public void OnMenuBar(bool menuBar)
			{
			}

			// Token: 0x06007001 RID: 28673 RVA: 0x000070A6 File Offset: 0x000052A6
			public void OnToolBar(bool toolBar)
			{
			}

			// Token: 0x06007002 RID: 28674 RVA: 0x000070A6 File Offset: 0x000052A6
			public void OnVisible(bool visible)
			{
			}

			// Token: 0x06007003 RID: 28675 RVA: 0x000070A6 File Offset: 0x000052A6
			public void OnQuit()
			{
			}

			// Token: 0x06007004 RID: 28676 RVA: 0x000070A6 File Offset: 0x000052A6
			public void PropertyChange(string szProperty)
			{
			}

			// Token: 0x06007005 RID: 28677 RVA: 0x000070A6 File Offset: 0x000052A6
			public void DownloadComplete()
			{
			}

			// Token: 0x04004339 RID: 17209
			private WebBrowser parent;

			// Token: 0x0400433A RID: 17210
			private bool allowNavigation;

			// Token: 0x0400433B RID: 17211
			private bool haveNavigated;
		}
	}
}
