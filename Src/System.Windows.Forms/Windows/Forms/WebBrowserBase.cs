using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.Windows.Forms
{
	/// <summary>Provides a wrapper for a generic ActiveX control for use as a base class by the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
	// Token: 0x02000433 RID: 1075
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultProperty("Name")]
	[DefaultEvent("Enter")]
	[Designer("System.Windows.Forms.Design.AxDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class WebBrowserBase : Control
	{
		// Token: 0x06004A90 RID: 19088 RVA: 0x00138960 File Offset: 0x00136B60
		internal WebBrowserBase(string clsidString)
		{
			if (Application.OleRequired() != ApartmentState.STA)
			{
				throw new ThreadStateException(SR.GetString("AXMTAThread", new object[] { clsidString }));
			}
			base.SetStyle(ControlStyles.UserPaint, false);
			this.clsid = new Guid(clsidString);
			this.webBrowserBaseChangingSize.Width = -1;
			this.SetAXHostState(WebBrowserHelper.isMaskEdit, this.clsid.Equals(WebBrowserHelper.maskEdit_Clsid));
		}

		/// <summary>Gets the underlying ActiveX <see langword="WebBrowser" /> control.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the underlying ActiveX <see langword="WebBrowser" /> control.</returns>
		// Token: 0x1700124D RID: 4685
		// (get) Token: 0x06004A91 RID: 19089 RVA: 0x001389E5 File Offset: 0x00136BE5
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object ActiveXInstance
		{
			get
			{
				return this.activeXInstance;
			}
		}

		/// <summary>Returns a reference to the unmanaged ActiveX control site.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.WebBrowserSiteBase" /> that represents the site of the underlying ActiveX control.</returns>
		// Token: 0x06004A92 RID: 19090 RVA: 0x001389ED File Offset: 0x00136BED
		protected virtual WebBrowserSiteBase CreateWebBrowserSiteBase()
		{
			return new WebBrowserSiteBase(this);
		}

		/// <summary>Called by the control when the underlying ActiveX control is created.</summary>
		/// <param name="nativeActiveXObject">An object that represents the underlying ActiveX control.</param>
		// Token: 0x06004A93 RID: 19091 RVA: 0x000070A6 File Offset: 0x000052A6
		protected virtual void AttachInterfaces(object nativeActiveXObject)
		{
		}

		/// <summary>Called by the control when the underlying ActiveX control is discarded.</summary>
		// Token: 0x06004A94 RID: 19092 RVA: 0x000070A6 File Offset: 0x000052A6
		protected virtual void DetachInterfaces()
		{
		}

		/// <summary>Called by the control to prepare it for listening to events.</summary>
		// Token: 0x06004A95 RID: 19093 RVA: 0x000070A6 File Offset: 0x000052A6
		protected virtual void CreateSink()
		{
		}

		/// <summary>Called by the control when it stops listening to events.</summary>
		// Token: 0x06004A96 RID: 19094 RVA: 0x000070A6 File Offset: 0x000052A6
		protected virtual void DetachSink()
		{
		}

		/// <summary>This method is not supported by this control.</summary>
		/// <param name="bitmap">A <see cref="T:System.Drawing.Bitmap" />.</param>
		/// <param name="targetBounds">A <see cref="T:System.Drawing.Rectangle" />.</param>
		// Token: 0x06004A97 RID: 19095 RVA: 0x0001AAF9 File Offset: 0x00018CF9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new void DrawToBitmap(Bitmap bitmap, Rectangle targetBounds)
		{
			base.DrawToBitmap(bitmap, targetBounds);
		}

		/// <summary>Gets or sets the site of the control.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISite" /> associated with the <see cref="T:System.Windows.Forms.Control" />, if any.</returns>
		// Token: 0x1700124E RID: 4686
		// (set) Token: 0x06004A98 RID: 19096 RVA: 0x001389F8 File Offset: 0x00136BF8
		public override ISite Site
		{
			set
			{
				bool flag = this.RemoveSelectionHandler();
				base.Site = value;
				if (flag)
				{
					this.AddSelectionHandler();
				}
			}
		}

		// Token: 0x06004A99 RID: 19097 RVA: 0x00138A1C File Offset: 0x00136C1C
		internal override void OnBoundsUpdate(int x, int y, int width, int height)
		{
			if (this.ActiveXState >= WebBrowserHelper.AXState.InPlaceActive)
			{
				try
				{
					this.webBrowserBaseChangingSize.Width = width;
					this.webBrowserBaseChangingSize.Height = height;
					this.AXInPlaceObject.SetObjectRects(new NativeMethods.COMRECT(new Rectangle(0, 0, width, height)), WebBrowserHelper.GetClipRect());
				}
				finally
				{
					this.webBrowserBaseChangingSize.Width = -1;
				}
			}
			base.OnBoundsUpdate(x, y, width, height);
		}

		/// <summary>Processes a dialog key if the WebBrowser ActiveX control does not process it.</summary>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
		/// <returns>
		///   <see langword="true" /> if the key was processed by the <see cref="T:System.Windows.Forms.WebBrowserBase" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004A9A RID: 19098 RVA: 0x00138A94 File Offset: 0x00136C94
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected override bool ProcessDialogKey(Keys keyData)
		{
			return !this.ignoreDialogKeys && base.ProcessDialogKey(keyData);
		}

		/// <summary>Preprocesses keyboard or input messages within the message loop before they are dispatched.</summary>
		/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the message to process. The possible values are WM_KEYDOWN, WM_SYSKEYDOWN, WM_CHAR, and WM_SYSCHAR.</param>
		/// <returns>
		///   <see langword="true" /> if the message was processed by the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004A9B RID: 19099 RVA: 0x00138AA8 File Offset: 0x00136CA8
		public override bool PreProcessMessage(ref Message msg)
		{
			if (this.IsUserMode)
			{
				if (this.GetAXHostState(WebBrowserHelper.siteProcessedInputKey))
				{
					return base.PreProcessMessage(ref msg);
				}
				NativeMethods.MSG msg2 = default(NativeMethods.MSG);
				msg2.message = msg.Msg;
				msg2.wParam = msg.WParam;
				msg2.lParam = msg.LParam;
				msg2.hwnd = msg.HWnd;
				this.SetAXHostState(WebBrowserHelper.siteProcessedInputKey, false);
				try
				{
					if (this.axOleInPlaceObject != null)
					{
						int num = this.axOleInPlaceActiveObject.TranslateAccelerator(ref msg2);
						if (num == 0)
						{
							return true;
						}
						msg.Msg = msg2.message;
						msg.WParam = msg2.wParam;
						msg.LParam = msg2.lParam;
						msg.HWnd = msg2.hwnd;
						if (num == 1)
						{
							bool flag = false;
							this.ignoreDialogKeys = true;
							try
							{
								flag = base.PreProcessMessage(ref msg);
							}
							finally
							{
								this.ignoreDialogKeys = false;
							}
							return flag;
						}
						if (this.GetAXHostState(WebBrowserHelper.siteProcessedInputKey))
						{
							return base.PreProcessMessage(ref msg);
						}
						return false;
					}
				}
				finally
				{
					this.SetAXHostState(WebBrowserHelper.siteProcessedInputKey, false);
				}
				return false;
			}
			return false;
		}

		/// <summary>Processes a mnemonic character.</summary>
		/// <param name="charCode">The character to process.</param>
		/// <returns>
		///   <see langword="true" /> if the character was processed as a mnemonic by the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004A9C RID: 19100 RVA: 0x00138BDC File Offset: 0x00136DDC
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected internal override bool ProcessMnemonic(char charCode)
		{
			bool flag = false;
			if (base.CanSelect)
			{
				try
				{
					NativeMethods.tagCONTROLINFO tagCONTROLINFO = new NativeMethods.tagCONTROLINFO();
					int controlInfo = this.axOleControl.GetControlInfo(tagCONTROLINFO);
					if (NativeMethods.Succeeded(controlInfo))
					{
						NativeMethods.MSG msg = default(NativeMethods.MSG);
						msg.hwnd = IntPtr.Zero;
						msg.message = 260;
						msg.wParam = (IntPtr)((int)char.ToUpper(charCode, CultureInfo.CurrentCulture));
						msg.lParam = (IntPtr)538443777;
						msg.time = SafeNativeMethods.GetTickCount();
						NativeMethods.POINT point = new NativeMethods.POINT();
						UnsafeNativeMethods.GetCursorPos(point);
						msg.pt_x = point.x;
						msg.pt_y = point.y;
						if (SafeNativeMethods.IsAccelerator(new HandleRef(tagCONTROLINFO, tagCONTROLINFO.hAccel), (int)tagCONTROLINFO.cAccel, ref msg, null))
						{
							this.axOleControl.OnMnemonic(ref msg);
							this.FocusInternal();
							flag = true;
						}
					}
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsCriticalException(ex))
					{
						throw;
					}
				}
			}
			return flag;
		}

		/// <summary>This member overrides <see cref="M:System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@)" />.</summary>
		/// <param name="m">The windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x06004A9D RID: 19101 RVA: 0x00138CE8 File Offset: 0x00136EE8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg <= 33)
			{
				if (msg <= 8)
				{
					if (msg != 2)
					{
						if (msg != 8)
						{
							goto IL_182;
						}
						this.hwndFocus = m.WParam;
						try
						{
							base.WndProc(ref m);
							return;
						}
						finally
						{
							this.hwndFocus = IntPtr.Zero;
						}
					}
					IntPtr intPtr;
					if (this.ActiveXState >= WebBrowserHelper.AXState.InPlaceActive && NativeMethods.Succeeded(this.AXInPlaceObject.GetWindow(out intPtr)))
					{
						Application.ParkHandle(new HandleRef(this.AXInPlaceObject, intPtr), DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNSPECIFIED);
					}
					if (base.RecreatingHandle)
					{
						this.axReloadingState = this.axState;
					}
					this.TransitionDownTo(WebBrowserHelper.AXState.Running);
					if (this.axWindow != null)
					{
						this.axWindow.ReleaseHandle();
					}
					this.OnHandleDestroyed(EventArgs.Empty);
					return;
				}
				if (msg - 20 > 1 && msg != 32)
				{
					if (msg != 33)
					{
						goto IL_182;
					}
					goto IL_C8;
				}
			}
			else if (msg <= 123)
			{
				if (msg != 43)
				{
					if (msg == 83)
					{
						base.WndProc(ref m);
						this.DefWndProc(ref m);
						return;
					}
					if (msg != 123)
					{
						goto IL_182;
					}
				}
			}
			else if (msg != 273)
			{
				switch (msg)
				{
				case 513:
				case 516:
				case 519:
					goto IL_C8;
				case 514:
				case 515:
				case 517:
				case 518:
				case 520:
				case 521:
					break;
				default:
					if (msg != 8277)
					{
						goto IL_182;
					}
					break;
				}
			}
			else
			{
				if (!Control.ReflectMessageInternal(m.LParam, ref m))
				{
					this.DefWndProc(ref m);
					return;
				}
				return;
			}
			this.DefWndProc(ref m);
			return;
			IL_C8:
			if (!base.DesignMode && this.containingControl != null && this.containingControl.ActiveControl != this)
			{
				this.FocusInternal();
			}
			this.DefWndProc(ref m);
			return;
			IL_182:
			if (m.Msg == WebBrowserHelper.REGMSG_MSG)
			{
				m.Result = (IntPtr)123;
				return;
			}
			base.WndProc(ref m);
		}

		/// <summary>This member overrides <see cref="M:System.Windows.Forms.Control.OnParentChanged(System.EventArgs)" />.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		/// <exception cref="T:System.Reflection.TargetInvocationException">Unable to get the window handle for the ActiveX control. Windowless ActiveX controls are not supported.</exception>
		// Token: 0x06004A9E RID: 19102 RVA: 0x00138EAC File Offset: 0x001370AC
		protected override void OnParentChanged(EventArgs e)
		{
			Control parentInternal = this.ParentInternal;
			if ((base.Visible && parentInternal != null && parentInternal.Visible) || base.IsHandleCreated)
			{
				this.TransitionUpTo(WebBrowserHelper.AXState.InPlaceActive);
			}
			base.OnParentChanged(e);
		}

		/// <summary>This member overrides <see cref="M:System.Windows.Forms.Control.OnVisibleChanged(System.EventArgs)" />.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		/// <exception cref="T:System.Reflection.TargetInvocationException">Unable to get the window handle for the ActiveX control. Windowless ActiveX controls are not supported.</exception>
		// Token: 0x06004A9F RID: 19103 RVA: 0x00138EE9 File Offset: 0x001370E9
		protected override void OnVisibleChanged(EventArgs e)
		{
			if (base.Visible && !base.Disposing && !base.IsDisposed)
			{
				this.TransitionUpTo(WebBrowserHelper.AXState.InPlaceActive);
			}
			base.OnVisibleChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06004AA0 RID: 19104 RVA: 0x00138F11 File Offset: 0x00137111
		protected override void OnGotFocus(EventArgs e)
		{
			if (this.ActiveXState < WebBrowserHelper.AXState.UIActive)
			{
				this.TransitionUpTo(WebBrowserHelper.AXState.UIActive);
			}
			base.OnGotFocus(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.LostFocus" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06004AA1 RID: 19105 RVA: 0x00138F2A File Offset: 0x0013712A
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			if (!base.ContainsFocus)
			{
				this.TransitionDownTo(WebBrowserHelper.AXState.InPlaceActive);
			}
		}

		/// <summary>This method is not meaningful for this control.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object.</param>
		// Token: 0x06004AA2 RID: 19106 RVA: 0x000070A6 File Offset: 0x000052A6
		protected override void OnRightToLeftChanged(EventArgs e)
		{
		}

		// Token: 0x06004AA3 RID: 19107 RVA: 0x00138F42 File Offset: 0x00137142
		internal override bool CanSelectCore()
		{
			return this.ActiveXState >= WebBrowserHelper.AXState.InPlaceActive && base.CanSelectCore();
		}

		// Token: 0x06004AA4 RID: 19108 RVA: 0x0001180C File Offset: 0x0000FA0C
		internal override bool AllowsKeyboardToolTip()
		{
			return false;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06004AA5 RID: 19109 RVA: 0x00138F55 File Offset: 0x00137155
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.AmbientChanged(-703);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ForeColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06004AA6 RID: 19110 RVA: 0x00138F69 File Offset: 0x00137169
		protected override void OnForeColorChanged(EventArgs e)
		{
			base.OnForeColorChanged(e);
			this.AmbientChanged(-704);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06004AA7 RID: 19111 RVA: 0x00138F7D File Offset: 0x0013717D
		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);
			this.AmbientChanged(-701);
		}

		// Token: 0x06004AA8 RID: 19112 RVA: 0x00138F91 File Offset: 0x00137191
		internal override void RecreateHandleCore()
		{
			if (!this.inRtlRecreate)
			{
				base.RecreateHandleCore();
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Control" /> and its child controls and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06004AA9 RID: 19113 RVA: 0x00138FA1 File Offset: 0x001371A1
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.TransitionDownTo(WebBrowserHelper.AXState.Passive);
			}
			base.Dispose(disposing);
		}

		// Token: 0x1700124F RID: 4687
		// (get) Token: 0x06004AAA RID: 19114 RVA: 0x00138FB4 File Offset: 0x001371B4
		// (set) Token: 0x06004AAB RID: 19115 RVA: 0x00138FBC File Offset: 0x001371BC
		internal WebBrowserHelper.AXState ActiveXState
		{
			get
			{
				return this.axState;
			}
			set
			{
				this.axState = value;
			}
		}

		// Token: 0x06004AAC RID: 19116 RVA: 0x00138FC5 File Offset: 0x001371C5
		internal bool GetAXHostState(int mask)
		{
			return this.axHostState[mask];
		}

		// Token: 0x06004AAD RID: 19117 RVA: 0x00138FD3 File Offset: 0x001371D3
		internal void SetAXHostState(int mask, bool value)
		{
			this.axHostState[mask] = value;
		}

		// Token: 0x06004AAE RID: 19118 RVA: 0x00031144 File Offset: 0x0002F344
		internal IntPtr GetHandleNoCreate()
		{
			if (!base.IsHandleCreated)
			{
				return IntPtr.Zero;
			}
			return base.Handle;
		}

		// Token: 0x06004AAF RID: 19119 RVA: 0x00138FE4 File Offset: 0x001371E4
		internal void TransitionUpTo(WebBrowserHelper.AXState state)
		{
			if (!this.GetAXHostState(WebBrowserHelper.inTransition))
			{
				this.SetAXHostState(WebBrowserHelper.inTransition, true);
				try
				{
					while (state > this.ActiveXState)
					{
						switch (this.ActiveXState)
						{
						case WebBrowserHelper.AXState.Passive:
							this.TransitionFromPassiveToLoaded();
							continue;
						case WebBrowserHelper.AXState.Loaded:
							this.TransitionFromLoadedToRunning();
							continue;
						case WebBrowserHelper.AXState.Running:
							this.TransitionFromRunningToInPlaceActive();
							continue;
						case WebBrowserHelper.AXState.InPlaceActive:
							this.TransitionFromInPlaceActiveToUIActive();
							continue;
						}
						WebBrowserHelper.AXState activeXState = this.ActiveXState;
						this.ActiveXState = activeXState + 1;
					}
				}
				finally
				{
					this.SetAXHostState(WebBrowserHelper.inTransition, false);
				}
			}
		}

		// Token: 0x06004AB0 RID: 19120 RVA: 0x00139088 File Offset: 0x00137288
		internal void TransitionDownTo(WebBrowserHelper.AXState state)
		{
			if (!this.GetAXHostState(WebBrowserHelper.inTransition))
			{
				this.SetAXHostState(WebBrowserHelper.inTransition, true);
				try
				{
					while (state < this.ActiveXState)
					{
						WebBrowserHelper.AXState activeXState = this.ActiveXState;
						switch (activeXState)
						{
						case WebBrowserHelper.AXState.Loaded:
							this.TransitionFromLoadedToPassive();
							continue;
						case WebBrowserHelper.AXState.Running:
							this.TransitionFromRunningToLoaded();
							continue;
						case (WebBrowserHelper.AXState)3:
							break;
						case WebBrowserHelper.AXState.InPlaceActive:
							this.TransitionFromInPlaceActiveToRunning();
							continue;
						default:
							if (activeXState == WebBrowserHelper.AXState.UIActive)
							{
								this.TransitionFromUIActiveToInPlaceActive();
								continue;
							}
							break;
						}
						WebBrowserHelper.AXState activeXState2 = this.ActiveXState;
						this.ActiveXState = activeXState2 - 1;
					}
				}
				finally
				{
					this.SetAXHostState(WebBrowserHelper.inTransition, false);
				}
			}
		}

		// Token: 0x06004AB1 RID: 19121 RVA: 0x0013912C File Offset: 0x0013732C
		internal bool DoVerb(int verb)
		{
			int num = this.axOleObject.DoVerb(verb, IntPtr.Zero, this.ActiveXSite, 0, base.Handle, new NativeMethods.COMRECT(base.Bounds));
			return num == 0;
		}

		// Token: 0x17001250 RID: 4688
		// (get) Token: 0x06004AB2 RID: 19122 RVA: 0x00139167 File Offset: 0x00137367
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal ContainerControl ContainingControl
		{
			get
			{
				if (this.containingControl == null || this.GetAXHostState(WebBrowserHelper.recomputeContainingControl))
				{
					this.containingControl = this.FindContainerControlInternal();
				}
				return this.containingControl;
			}
		}

		// Token: 0x06004AB3 RID: 19123 RVA: 0x00139190 File Offset: 0x00137390
		internal WebBrowserContainer CreateWebBrowserContainer()
		{
			if (this.wbContainer == null)
			{
				this.wbContainer = new WebBrowserContainer(this);
			}
			return this.wbContainer;
		}

		// Token: 0x06004AB4 RID: 19124 RVA: 0x001391AC File Offset: 0x001373AC
		internal WebBrowserContainer GetParentContainer()
		{
			if (this.container == null)
			{
				this.container = WebBrowserContainer.FindContainerForControl(this);
			}
			if (this.container == null)
			{
				this.container = this.CreateWebBrowserContainer();
				this.container.AddControl(this);
			}
			return this.container;
		}

		// Token: 0x06004AB5 RID: 19125 RVA: 0x001391E8 File Offset: 0x001373E8
		internal void SetEditMode(WebBrowserHelper.AXEditMode em)
		{
			this.axEditMode = em;
		}

		// Token: 0x06004AB6 RID: 19126 RVA: 0x001391F4 File Offset: 0x001373F4
		internal void SetSelectionStyle(WebBrowserHelper.SelectionStyle selectionStyle)
		{
			if (base.DesignMode)
			{
				ISelectionService selectionService = WebBrowserHelper.GetSelectionService(this);
				this.selectionStyle = selectionStyle;
				if (selectionService != null && selectionService.GetComponentSelected(this))
				{
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this)["SelectionStyle"];
					if (propertyDescriptor != null && propertyDescriptor.PropertyType == typeof(int))
					{
						propertyDescriptor.SetValue(this, (int)selectionStyle);
					}
				}
			}
		}

		// Token: 0x06004AB7 RID: 19127 RVA: 0x0013925C File Offset: 0x0013745C
		internal void AddSelectionHandler()
		{
			if (!this.GetAXHostState(WebBrowserHelper.addedSelectionHandler))
			{
				this.SetAXHostState(WebBrowserHelper.addedSelectionHandler, true);
				ISelectionService selectionService = WebBrowserHelper.GetSelectionService(this);
				if (selectionService != null)
				{
					selectionService.SelectionChanging += this.SelectionChangeHandler;
				}
			}
		}

		// Token: 0x06004AB8 RID: 19128 RVA: 0x00139298 File Offset: 0x00137498
		internal bool RemoveSelectionHandler()
		{
			bool axhostState = this.GetAXHostState(WebBrowserHelper.addedSelectionHandler);
			if (axhostState)
			{
				this.SetAXHostState(WebBrowserHelper.addedSelectionHandler, false);
				ISelectionService selectionService = WebBrowserHelper.GetSelectionService(this);
				if (selectionService != null)
				{
					selectionService.SelectionChanging -= this.SelectionChangeHandler;
				}
			}
			return axhostState;
		}

		// Token: 0x06004AB9 RID: 19129 RVA: 0x001392D8 File Offset: 0x001374D8
		internal void AttachWindow(IntPtr hwnd)
		{
			UnsafeNativeMethods.SetParent(new HandleRef(null, hwnd), new HandleRef(this, base.Handle));
			if (this.axWindow != null)
			{
				this.axWindow.ReleaseHandle();
			}
			this.axWindow = new WebBrowserBase.WebBrowserBaseNativeWindow(this);
			this.axWindow.AssignHandle(hwnd, false);
			base.UpdateZOrder();
			base.UpdateBounds();
			Size size = base.Size;
			size = this.SetExtent(size.Width, size.Height);
			Point location = base.Location;
			base.Bounds = new Rectangle(location.X, location.Y, size.Width, size.Height);
		}

		// Token: 0x17001251 RID: 4689
		// (get) Token: 0x06004ABA RID: 19130 RVA: 0x00139380 File Offset: 0x00137580
		internal bool IsUserMode
		{
			get
			{
				return this.Site == null || !base.DesignMode;
			}
		}

		// Token: 0x06004ABB RID: 19131 RVA: 0x00139398 File Offset: 0x00137598
		internal void MakeDirty()
		{
			ISite site = this.Site;
			if (site != null)
			{
				IComponentChangeService componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
				if (componentChangeService != null)
				{
					componentChangeService.OnComponentChanging(this, null);
					componentChangeService.OnComponentChanged(this, null, null, null);
				}
			}
		}

		// Token: 0x17001252 RID: 4690
		// (get) Token: 0x06004ABC RID: 19132 RVA: 0x001393DA File Offset: 0x001375DA
		// (set) Token: 0x06004ABD RID: 19133 RVA: 0x001393E2 File Offset: 0x001375E2
		internal int NoComponentChangeEvents
		{
			get
			{
				return this.noComponentChange;
			}
			set
			{
				this.noComponentChange = value;
			}
		}

		// Token: 0x06004ABE RID: 19134 RVA: 0x001393EB File Offset: 0x001375EB
		private void StartEvents()
		{
			if (!this.GetAXHostState(WebBrowserHelper.sinkAttached))
			{
				this.SetAXHostState(WebBrowserHelper.sinkAttached, true);
				this.CreateSink();
			}
			this.ActiveXSite.StartEvents();
		}

		// Token: 0x06004ABF RID: 19135 RVA: 0x00139417 File Offset: 0x00137617
		private void StopEvents()
		{
			if (this.GetAXHostState(WebBrowserHelper.sinkAttached))
			{
				this.SetAXHostState(WebBrowserHelper.sinkAttached, false);
				this.DetachSink();
			}
			this.ActiveXSite.StopEvents();
		}

		// Token: 0x06004AC0 RID: 19136 RVA: 0x00139443 File Offset: 0x00137643
		private void TransitionFromPassiveToLoaded()
		{
			if (this.ActiveXState == WebBrowserHelper.AXState.Passive)
			{
				this.activeXInstance = UnsafeNativeMethods.CoCreateInstance(ref this.clsid, null, 1, ref NativeMethods.ActiveX.IID_IUnknown);
				this.ActiveXState = WebBrowserHelper.AXState.Loaded;
				this.AttachInterfacesInternal();
			}
		}

		// Token: 0x06004AC1 RID: 19137 RVA: 0x00139474 File Offset: 0x00137674
		private void TransitionFromLoadedToPassive()
		{
			if (this.ActiveXState == WebBrowserHelper.AXState.Loaded)
			{
				int num = this.NoComponentChangeEvents;
				this.NoComponentChangeEvents = num + 1;
				try
				{
					if (this.activeXInstance != null)
					{
						this.DetachInterfacesInternal();
						Marshal.FinalReleaseComObject(this.activeXInstance);
						this.activeXInstance = null;
					}
				}
				finally
				{
					num = this.NoComponentChangeEvents;
					this.NoComponentChangeEvents = num - 1;
				}
				this.ActiveXState = WebBrowserHelper.AXState.Passive;
			}
		}

		// Token: 0x06004AC2 RID: 19138 RVA: 0x001394E8 File Offset: 0x001376E8
		private void TransitionFromLoadedToRunning()
		{
			if (this.ActiveXState == WebBrowserHelper.AXState.Loaded)
			{
				int num = 0;
				int miscStatus = this.axOleObject.GetMiscStatus(1, out num);
				if (NativeMethods.Succeeded(miscStatus) && (num & 131072) != 0)
				{
					this.axOleObject.SetClientSite(this.ActiveXSite);
				}
				if (!base.DesignMode)
				{
					this.StartEvents();
				}
				this.ActiveXState = WebBrowserHelper.AXState.Running;
			}
		}

		// Token: 0x06004AC3 RID: 19139 RVA: 0x00139548 File Offset: 0x00137748
		private void TransitionFromRunningToLoaded()
		{
			if (this.ActiveXState == WebBrowserHelper.AXState.Running)
			{
				this.StopEvents();
				WebBrowserContainer parentContainer = this.GetParentContainer();
				if (parentContainer != null)
				{
					parentContainer.RemoveControl(this);
				}
				this.axOleObject.SetClientSite(null);
				this.ActiveXState = WebBrowserHelper.AXState.Loaded;
			}
		}

		// Token: 0x06004AC4 RID: 19140 RVA: 0x0013958C File Offset: 0x0013778C
		private void TransitionFromRunningToInPlaceActive()
		{
			if (this.ActiveXState == WebBrowserHelper.AXState.Running)
			{
				try
				{
					this.DoVerb(-5);
				}
				catch (Exception ex)
				{
					throw new TargetInvocationException(SR.GetString("AXNohWnd", new object[] { base.GetType().Name }), ex);
				}
				base.CreateControl(true);
				this.ActiveXState = WebBrowserHelper.AXState.InPlaceActive;
			}
		}

		// Token: 0x06004AC5 RID: 19141 RVA: 0x001395F4 File Offset: 0x001377F4
		private void TransitionFromInPlaceActiveToRunning()
		{
			if (this.ActiveXState == WebBrowserHelper.AXState.InPlaceActive)
			{
				ContainerControl containerControl = this.ContainingControl;
				if (containerControl != null && containerControl.ActiveControl == this)
				{
					containerControl.SetActiveControlInternal(null);
				}
				this.AXInPlaceObject.InPlaceDeactivate();
				this.ActiveXState = WebBrowserHelper.AXState.Running;
			}
		}

		// Token: 0x06004AC6 RID: 19142 RVA: 0x00139638 File Offset: 0x00137838
		private void TransitionFromInPlaceActiveToUIActive()
		{
			if (this.ActiveXState == WebBrowserHelper.AXState.InPlaceActive)
			{
				try
				{
					this.DoVerb(-4);
				}
				catch (Exception ex)
				{
					throw new TargetInvocationException(SR.GetString("AXNohWnd", new object[] { base.GetType().Name }), ex);
				}
				this.ActiveXState = WebBrowserHelper.AXState.UIActive;
			}
		}

		// Token: 0x06004AC7 RID: 19143 RVA: 0x00139698 File Offset: 0x00137898
		private void TransitionFromUIActiveToInPlaceActive()
		{
			if (this.ActiveXState == WebBrowserHelper.AXState.UIActive)
			{
				int num = this.AXInPlaceObject.UIDeactivate();
				this.ActiveXState = WebBrowserHelper.AXState.InPlaceActive;
			}
		}

		// Token: 0x17001253 RID: 4691
		// (get) Token: 0x06004AC8 RID: 19144 RVA: 0x001396C1 File Offset: 0x001378C1
		internal WebBrowserSiteBase ActiveXSite
		{
			get
			{
				if (this.axSite == null)
				{
					this.axSite = this.CreateWebBrowserSiteBase();
				}
				return this.axSite;
			}
		}

		// Token: 0x06004AC9 RID: 19145 RVA: 0x001396E0 File Offset: 0x001378E0
		private void AttachInterfacesInternal()
		{
			this.axOleObject = (UnsafeNativeMethods.IOleObject)this.activeXInstance;
			this.axOleInPlaceObject = (UnsafeNativeMethods.IOleInPlaceObject)this.activeXInstance;
			this.axOleInPlaceActiveObject = (UnsafeNativeMethods.IOleInPlaceActiveObject)this.activeXInstance;
			this.axOleControl = (UnsafeNativeMethods.IOleControl)this.activeXInstance;
			this.AttachInterfaces(this.activeXInstance);
		}

		// Token: 0x06004ACA RID: 19146 RVA: 0x0013973D File Offset: 0x0013793D
		private void DetachInterfacesInternal()
		{
			this.axOleObject = null;
			this.axOleInPlaceObject = null;
			this.axOleInPlaceActiveObject = null;
			this.axOleControl = null;
			this.DetachInterfaces();
		}

		// Token: 0x17001254 RID: 4692
		// (get) Token: 0x06004ACB RID: 19147 RVA: 0x00139761 File Offset: 0x00137961
		private EventHandler SelectionChangeHandler
		{
			get
			{
				if (this.selectionChangeHandler == null)
				{
					this.selectionChangeHandler = new EventHandler(this.OnNewSelection);
				}
				return this.selectionChangeHandler;
			}
		}

		// Token: 0x06004ACC RID: 19148 RVA: 0x00139784 File Offset: 0x00137984
		private void OnNewSelection(object sender, EventArgs e)
		{
			if (base.DesignMode)
			{
				ISelectionService selectionService = WebBrowserHelper.GetSelectionService(this);
				if (selectionService != null)
				{
					if (!selectionService.GetComponentSelected(this))
					{
						if (this.EditMode)
						{
							this.GetParentContainer().OnExitEditMode(this);
							this.SetEditMode(WebBrowserHelper.AXEditMode.None);
						}
						this.SetSelectionStyle(WebBrowserHelper.SelectionStyle.Selected);
						this.RemoveSelectionHandler();
						return;
					}
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this)["SelectionStyle"];
					if (propertyDescriptor != null && propertyDescriptor.PropertyType == typeof(int))
					{
						int num = (int)propertyDescriptor.GetValue(this);
						if (num != (int)this.selectionStyle)
						{
							propertyDescriptor.SetValue(this, this.selectionStyle);
						}
					}
				}
			}
		}

		// Token: 0x06004ACD RID: 19149 RVA: 0x00139830 File Offset: 0x00137A30
		private Size SetExtent(int width, int height)
		{
			NativeMethods.tagSIZEL tagSIZEL = new NativeMethods.tagSIZEL();
			tagSIZEL.cx = width;
			tagSIZEL.cy = height;
			bool flag = base.DesignMode;
			try
			{
				this.Pixel2hiMetric(tagSIZEL, tagSIZEL);
				this.axOleObject.SetExtent(1, tagSIZEL);
			}
			catch (COMException)
			{
				flag = true;
			}
			if (flag)
			{
				this.axOleObject.GetExtent(1, tagSIZEL);
				try
				{
					this.axOleObject.SetExtent(1, tagSIZEL);
				}
				catch (COMException ex)
				{
				}
			}
			return this.GetExtent();
		}

		// Token: 0x06004ACE RID: 19150 RVA: 0x001398BC File Offset: 0x00137ABC
		private Size GetExtent()
		{
			NativeMethods.tagSIZEL tagSIZEL = new NativeMethods.tagSIZEL();
			this.axOleObject.GetExtent(1, tagSIZEL);
			this.HiMetric2Pixel(tagSIZEL, tagSIZEL);
			return new Size(tagSIZEL.cx, tagSIZEL.cy);
		}

		// Token: 0x06004ACF RID: 19151 RVA: 0x001398F8 File Offset: 0x00137AF8
		private void HiMetric2Pixel(NativeMethods.tagSIZEL sz, NativeMethods.tagSIZEL szout)
		{
			NativeMethods._POINTL pointl = new NativeMethods._POINTL();
			pointl.x = sz.cx;
			pointl.y = sz.cy;
			NativeMethods.tagPOINTF tagPOINTF = new NativeMethods.tagPOINTF();
			((UnsafeNativeMethods.IOleControlSite)this.ActiveXSite).TransformCoords(pointl, tagPOINTF, 6);
			szout.cx = (int)tagPOINTF.x;
			szout.cy = (int)tagPOINTF.y;
		}

		// Token: 0x06004AD0 RID: 19152 RVA: 0x00139954 File Offset: 0x00137B54
		private void Pixel2hiMetric(NativeMethods.tagSIZEL sz, NativeMethods.tagSIZEL szout)
		{
			NativeMethods.tagPOINTF tagPOINTF = new NativeMethods.tagPOINTF();
			tagPOINTF.x = (float)sz.cx;
			tagPOINTF.y = (float)sz.cy;
			NativeMethods._POINTL pointl = new NativeMethods._POINTL();
			((UnsafeNativeMethods.IOleControlSite)this.ActiveXSite).TransformCoords(pointl, tagPOINTF, 10);
			szout.cx = pointl.x;
			szout.cy = pointl.y;
		}

		// Token: 0x17001255 RID: 4693
		// (get) Token: 0x06004AD1 RID: 19153 RVA: 0x001399AF File Offset: 0x00137BAF
		private bool EditMode
		{
			get
			{
				return this.axEditMode > WebBrowserHelper.AXEditMode.None;
			}
		}

		// Token: 0x06004AD2 RID: 19154 RVA: 0x001399BC File Offset: 0x00137BBC
		internal ContainerControl FindContainerControlInternal()
		{
			if (this.Site != null)
			{
				IDesignerHost designerHost = (IDesignerHost)this.Site.GetService(typeof(IDesignerHost));
				if (designerHost != null)
				{
					IComponent rootComponent = designerHost.RootComponent;
					if (rootComponent != null && rootComponent is ContainerControl)
					{
						return (ContainerControl)rootComponent;
					}
				}
			}
			ContainerControl containerControl = null;
			for (Control control = this; control != null; control = control.ParentInternal)
			{
				ContainerControl containerControl2 = control as ContainerControl;
				if (containerControl2 != null)
				{
					containerControl = containerControl2;
				}
			}
			if (containerControl == null)
			{
				containerControl = Control.FromHandle(UnsafeNativeMethods.GetParent(new HandleRef(this, base.Handle))) as ContainerControl;
			}
			if (containerControl is Application.ParkingWindow)
			{
				containerControl = null;
			}
			this.SetAXHostState(WebBrowserHelper.recomputeContainingControl, containerControl == null);
			return containerControl;
		}

		// Token: 0x06004AD3 RID: 19155 RVA: 0x00139A60 File Offset: 0x00137C60
		private void AmbientChanged(int dispid)
		{
			if (this.activeXInstance != null)
			{
				try
				{
					base.Invalidate();
					this.axOleControl.OnAmbientPropertyChange(dispid);
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsCriticalException(ex))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x17001256 RID: 4694
		// (get) Token: 0x06004AD4 RID: 19156 RVA: 0x00139AA8 File Offset: 0x00137CA8
		internal UnsafeNativeMethods.IOleInPlaceObject AXInPlaceObject
		{
			get
			{
				return this.axOleInPlaceObject;
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x17001257 RID: 4695
		// (get) Token: 0x06004AD5 RID: 19157 RVA: 0x0001A0BA File Offset: 0x000182BA
		protected override Size DefaultSize
		{
			get
			{
				return new Size(75, 23);
			}
		}

		/// <summary>Determines if a character is an input character that the control recognizes.</summary>
		/// <param name="charCode">The character to test.</param>
		/// <returns>
		///   <see langword="true" /> if the character should be sent directly to the control and not preprocessed; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004AD6 RID: 19158 RVA: 0x00012E4E File Offset: 0x0001104E
		protected override bool IsInputChar(char charCode)
		{
			return true;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		/// <exception cref="T:System.Threading.ThreadStateException">The <see cref="P:System.Threading.Thread.ApartmentState" /> property of the application is not set to <see cref="F:System.Threading.ApartmentState.STA" />.</exception>
		// Token: 0x06004AD7 RID: 19159 RVA: 0x00139AB0 File Offset: 0x00137CB0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnHandleCreated(EventArgs e)
		{
			if (Application.OleRequired() != ApartmentState.STA)
			{
				throw new ThreadStateException(SR.GetString("ThreadMustBeSTA"));
			}
			base.OnHandleCreated(e);
			if (this.axReloadingState != WebBrowserHelper.AXState.Passive && this.axReloadingState != this.axState)
			{
				if (this.axState < this.axReloadingState)
				{
					this.TransitionUpTo(this.axReloadingState);
				}
				else
				{
					this.TransitionDownTo(this.axReloadingState);
				}
				this.axReloadingState = WebBrowserHelper.AXState.Passive;
			}
		}

		/// <summary>This property is not meaningful for this control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background of the control.</returns>
		// Token: 0x17001258 RID: 4696
		// (get) Token: 0x06004AD8 RID: 19160 RVA: 0x0001A049 File Offset: 0x00018249
		// (set) Token: 0x06004AD9 RID: 19161 RVA: 0x00012D84 File Offset: 0x00010F84
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
			}
		}

		/// <summary>This property is not meaningful for this control.</summary>
		/// <returns>The value of this property is not meaningful for this control.</returns>
		// Token: 0x17001259 RID: 4697
		// (get) Token: 0x06004ADA RID: 19162 RVA: 0x0001A0D6 File Offset: 0x000182D6
		// (set) Token: 0x06004ADB RID: 19163 RVA: 0x0001A0DE File Offset: 0x000182DE
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				base.Font = value;
			}
		}

		/// <summary>This property is not meaningful for this control.</summary>
		/// <returns>The value of this property is not meaningful for this control.</returns>
		// Token: 0x1700125A RID: 4698
		// (get) Token: 0x06004ADC RID: 19164 RVA: 0x0001A0E7 File Offset: 0x000182E7
		// (set) Token: 0x06004ADD RID: 19165 RVA: 0x00013024 File Offset: 0x00011224
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		/// <summary>This property is not meaningful for this control.</summary>
		/// <returns>The value of this property is not meaningful for this control.</returns>
		// Token: 0x1700125B RID: 4699
		// (get) Token: 0x06004ADE RID: 19166 RVA: 0x0001A051 File Offset: 0x00018251
		// (set) Token: 0x06004ADF RID: 19167 RVA: 0x0001A059 File Offset: 0x00018259
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ImeMode ImeMode
		{
			get
			{
				return base.ImeMode;
			}
			set
			{
				base.ImeMode = value;
			}
		}

		/// <summary>This property is not supported by this control.</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is being set.</exception>
		// Token: 0x1700125C RID: 4700
		// (get) Token: 0x06004AE0 RID: 19168 RVA: 0x000B8E3D File Offset: 0x000B703D
		// (set) Token: 0x06004AE1 RID: 19169 RVA: 0x00139B20 File Offset: 0x00137D20
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool AllowDrop
		{
			get
			{
				return base.AllowDrop;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("WebBrowserAllowDropNotSupported"));
			}
		}

		/// <summary>This property is not supported by this control.</summary>
		/// <returns>
		///   <see langword="null" />.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is being set.</exception>
		// Token: 0x1700125D RID: 4701
		// (get) Token: 0x06004AE2 RID: 19170 RVA: 0x0001187C File Offset: 0x0000FA7C
		// (set) Token: 0x06004AE3 RID: 19171 RVA: 0x00139B31 File Offset: 0x00137D31
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("WebBrowserBackgroundImageNotSupported"));
			}
		}

		/// <summary>This property is not supported by this control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImageLayout" /> values.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is being set.</exception>
		// Token: 0x1700125E RID: 4702
		// (get) Token: 0x06004AE4 RID: 19172 RVA: 0x0001189F File Offset: 0x0000FA9F
		// (set) Token: 0x06004AE5 RID: 19173 RVA: 0x00139B42 File Offset: 0x00137D42
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override ImageLayout BackgroundImageLayout
		{
			get
			{
				return base.BackgroundImageLayout;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("WebBrowserBackgroundImageLayoutNotSupported"));
			}
		}

		/// <summary>This property is not supported by this control.</summary>
		/// <returns>The value of this property is not meaningful for this control.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is being set.</exception>
		// Token: 0x1700125F RID: 4703
		// (get) Token: 0x06004AE6 RID: 19174 RVA: 0x0001A0A0 File Offset: 0x000182A0
		// (set) Token: 0x06004AE7 RID: 19175 RVA: 0x00139B53 File Offset: 0x00137D53
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Cursor Cursor
		{
			get
			{
				return base.Cursor;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("WebBrowserCursorNotSupported"));
			}
		}

		/// <summary>This property is not supported by this control.</summary>
		/// <returns>
		///   <see langword="true" /> in all cases.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is being set.</exception>
		// Token: 0x17001260 RID: 4704
		// (get) Token: 0x06004AE8 RID: 19176 RVA: 0x0001A0C5 File Offset: 0x000182C5
		// (set) Token: 0x06004AE9 RID: 19177 RVA: 0x00139B64 File Offset: 0x00137D64
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("WebBrowserEnabledNotSupported"));
			}
		}

		/// <summary>This property is not supported by this control.</summary>
		/// <returns>The value of this property is not meaningful for this control.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is being set.</exception>
		// Token: 0x17001261 RID: 4705
		// (get) Token: 0x06004AEA RID: 19178 RVA: 0x0001180C File Offset: 0x0000FA0C
		// (set) Token: 0x06004AEB RID: 19179 RVA: 0x00139B75 File Offset: 0x00137D75
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Localizable(false)]
		public override RightToLeft RightToLeft
		{
			get
			{
				return RightToLeft.No;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("WebBrowserRightToLeftNotSupported"));
			}
		}

		/// <summary>This property is not supported by this control.</summary>
		/// <returns>The text displayed in the control.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is being set.</exception>
		// Token: 0x17001262 RID: 4706
		// (get) Token: 0x06004AEC RID: 19180 RVA: 0x000F17EC File Offset: 0x000EF9EC
		// (set) Token: 0x06004AED RID: 19181 RVA: 0x00139B86 File Offset: 0x00137D86
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Bindable(false)]
		public override string Text
		{
			get
			{
				return "";
			}
			set
			{
				throw new NotSupportedException(SR.GetString("WebBrowserTextNotSupported"));
			}
		}

		/// <summary>This property is not supported by this control.</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is being set.</exception>
		// Token: 0x17001263 RID: 4707
		// (get) Token: 0x06004AEE RID: 19182 RVA: 0x00139B97 File Offset: 0x00137D97
		// (set) Token: 0x06004AEF RID: 19183 RVA: 0x00139B9F File Offset: 0x00137D9F
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool UseWaitCursor
		{
			get
			{
				return base.UseWaitCursor;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("WebBrowserUseWaitCursorNotSupported"));
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x140003C6 RID: 966
		// (add) Token: 0x06004AF0 RID: 19184 RVA: 0x0001A2B2 File Offset: 0x000184B2
		// (remove) Token: 0x06004AF1 RID: 19185 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageLayoutChanged
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "BackgroundImageLayoutChanged" }));
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x140003C7 RID: 967
		// (add) Token: 0x06004AF2 RID: 19186 RVA: 0x00139BB0 File Offset: 0x00137DB0
		// (remove) Token: 0x06004AF3 RID: 19187 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler Enter
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "Enter" }));
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x140003C8 RID: 968
		// (add) Token: 0x06004AF4 RID: 19188 RVA: 0x00139BCF File Offset: 0x00137DCF
		// (remove) Token: 0x06004AF5 RID: 19189 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler Leave
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "Leave" }));
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x140003C9 RID: 969
		// (add) Token: 0x06004AF6 RID: 19190 RVA: 0x00139BEE File Offset: 0x00137DEE
		// (remove) Token: 0x06004AF7 RID: 19191 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler MouseCaptureChanged
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "MouseCaptureChanged" }));
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x140003CA RID: 970
		// (add) Token: 0x06004AF8 RID: 19192 RVA: 0x0001A062 File Offset: 0x00018262
		// (remove) Token: 0x06004AF9 RID: 19193 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event MouseEventHandler MouseClick
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "MouseClick" }));
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x140003CB RID: 971
		// (add) Token: 0x06004AFA RID: 19194 RVA: 0x0001A081 File Offset: 0x00018281
		// (remove) Token: 0x06004AFB RID: 19195 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event MouseEventHandler MouseDoubleClick
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "MouseDoubleClick" }));
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x140003CC RID: 972
		// (add) Token: 0x06004AFC RID: 19196 RVA: 0x0001A274 File Offset: 0x00018474
		// (remove) Token: 0x06004AFD RID: 19197 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackColorChanged
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "BackColorChanged" }));
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x140003CD RID: 973
		// (add) Token: 0x06004AFE RID: 19198 RVA: 0x0001A293 File Offset: 0x00018493
		// (remove) Token: 0x06004AFF RID: 19199 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageChanged
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "BackgroundImageChanged" }));
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x140003CE RID: 974
		// (add) Token: 0x06004B00 RID: 19200 RVA: 0x0001A2D1 File Offset: 0x000184D1
		// (remove) Token: 0x06004B01 RID: 19201 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BindingContextChanged
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "BindingContextChanged" }));
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x140003CF RID: 975
		// (add) Token: 0x06004B02 RID: 19202 RVA: 0x0001A30F File Offset: 0x0001850F
		// (remove) Token: 0x06004B03 RID: 19203 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler CursorChanged
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "CursorChanged" }));
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x140003D0 RID: 976
		// (add) Token: 0x06004B04 RID: 19204 RVA: 0x0001A32E File Offset: 0x0001852E
		// (remove) Token: 0x06004B05 RID: 19205 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler EnabledChanged
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "EnabledChanged" }));
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x140003D1 RID: 977
		// (add) Token: 0x06004B06 RID: 19206 RVA: 0x0001A34D File Offset: 0x0001854D
		// (remove) Token: 0x06004B07 RID: 19207 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler FontChanged
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "FontChanged" }));
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x140003D2 RID: 978
		// (add) Token: 0x06004B08 RID: 19208 RVA: 0x0001A36C File Offset: 0x0001856C
		// (remove) Token: 0x06004B09 RID: 19209 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ForeColorChanged
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "ForeColorChanged" }));
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x140003D3 RID: 979
		// (add) Token: 0x06004B0A RID: 19210 RVA: 0x0001A38B File Offset: 0x0001858B
		// (remove) Token: 0x06004B0B RID: 19211 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler RightToLeftChanged
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "RightToLeftChanged" }));
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x140003D4 RID: 980
		// (add) Token: 0x06004B0C RID: 19212 RVA: 0x0001A3AA File Offset: 0x000185AA
		// (remove) Token: 0x06004B0D RID: 19213 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TextChanged
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "TextChanged" }));
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x140003D5 RID: 981
		// (add) Token: 0x06004B0E RID: 19214 RVA: 0x0001A3C9 File Offset: 0x000185C9
		// (remove) Token: 0x06004B0F RID: 19215 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler Click
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "Click" }));
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x140003D6 RID: 982
		// (add) Token: 0x06004B10 RID: 19216 RVA: 0x0001A3E8 File Offset: 0x000185E8
		// (remove) Token: 0x06004B11 RID: 19217 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event DragEventHandler DragDrop
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "DragDrop" }));
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x140003D7 RID: 983
		// (add) Token: 0x06004B12 RID: 19218 RVA: 0x0001A407 File Offset: 0x00018607
		// (remove) Token: 0x06004B13 RID: 19219 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event DragEventHandler DragEnter
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "DragEnter" }));
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x140003D8 RID: 984
		// (add) Token: 0x06004B14 RID: 19220 RVA: 0x0001A426 File Offset: 0x00018626
		// (remove) Token: 0x06004B15 RID: 19221 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event DragEventHandler DragOver
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "DragOver" }));
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x140003D9 RID: 985
		// (add) Token: 0x06004B16 RID: 19222 RVA: 0x0001A445 File Offset: 0x00018645
		// (remove) Token: 0x06004B17 RID: 19223 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler DragLeave
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "DragLeave" }));
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x140003DA RID: 986
		// (add) Token: 0x06004B18 RID: 19224 RVA: 0x0001A464 File Offset: 0x00018664
		// (remove) Token: 0x06004B19 RID: 19225 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event GiveFeedbackEventHandler GiveFeedback
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "GiveFeedback" }));
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x140003DB RID: 987
		// (add) Token: 0x06004B1A RID: 19226 RVA: 0x0001A483 File Offset: 0x00018683
		// (remove) Token: 0x06004B1B RID: 19227 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event HelpEventHandler HelpRequested
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "HelpRequested" }));
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x140003DC RID: 988
		// (add) Token: 0x06004B1C RID: 19228 RVA: 0x0001A4A2 File Offset: 0x000186A2
		// (remove) Token: 0x06004B1D RID: 19229 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event PaintEventHandler Paint
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "Paint" }));
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x140003DD RID: 989
		// (add) Token: 0x06004B1E RID: 19230 RVA: 0x0001A4C1 File Offset: 0x000186C1
		// (remove) Token: 0x06004B1F RID: 19231 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event QueryContinueDragEventHandler QueryContinueDrag
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "QueryContinueDrag" }));
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x140003DE RID: 990
		// (add) Token: 0x06004B20 RID: 19232 RVA: 0x0001A4E0 File Offset: 0x000186E0
		// (remove) Token: 0x06004B21 RID: 19233 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event QueryAccessibilityHelpEventHandler QueryAccessibilityHelp
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "QueryAccessibilityHelp" }));
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x140003DF RID: 991
		// (add) Token: 0x06004B22 RID: 19234 RVA: 0x0001A4FF File Offset: 0x000186FF
		// (remove) Token: 0x06004B23 RID: 19235 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler DoubleClick
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "DoubleClick" }));
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x140003E0 RID: 992
		// (add) Token: 0x06004B24 RID: 19236 RVA: 0x0001A51E File Offset: 0x0001871E
		// (remove) Token: 0x06004B25 RID: 19237 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ImeModeChanged
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "ImeModeChanged" }));
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x140003E1 RID: 993
		// (add) Token: 0x06004B26 RID: 19238 RVA: 0x0001A53D File Offset: 0x0001873D
		// (remove) Token: 0x06004B27 RID: 19239 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event KeyEventHandler KeyDown
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "KeyDown" }));
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x140003E2 RID: 994
		// (add) Token: 0x06004B28 RID: 19240 RVA: 0x0001A55C File Offset: 0x0001875C
		// (remove) Token: 0x06004B29 RID: 19241 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event KeyPressEventHandler KeyPress
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "KeyPress" }));
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x140003E3 RID: 995
		// (add) Token: 0x06004B2A RID: 19242 RVA: 0x0001A57B File Offset: 0x0001877B
		// (remove) Token: 0x06004B2B RID: 19243 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event KeyEventHandler KeyUp
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "KeyUp" }));
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x140003E4 RID: 996
		// (add) Token: 0x06004B2C RID: 19244 RVA: 0x0001A59A File Offset: 0x0001879A
		// (remove) Token: 0x06004B2D RID: 19245 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event LayoutEventHandler Layout
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "Layout" }));
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x140003E5 RID: 997
		// (add) Token: 0x06004B2E RID: 19246 RVA: 0x0001A5B9 File Offset: 0x000187B9
		// (remove) Token: 0x06004B2F RID: 19247 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event MouseEventHandler MouseDown
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "MouseDown" }));
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x140003E6 RID: 998
		// (add) Token: 0x06004B30 RID: 19248 RVA: 0x0001A5D8 File Offset: 0x000187D8
		// (remove) Token: 0x06004B31 RID: 19249 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler MouseEnter
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "MouseEnter" }));
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x140003E7 RID: 999
		// (add) Token: 0x06004B32 RID: 19250 RVA: 0x0001A5F7 File Offset: 0x000187F7
		// (remove) Token: 0x06004B33 RID: 19251 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler MouseLeave
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "MouseLeave" }));
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x140003E8 RID: 1000
		// (add) Token: 0x06004B34 RID: 19252 RVA: 0x0001A616 File Offset: 0x00018816
		// (remove) Token: 0x06004B35 RID: 19253 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler MouseHover
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "MouseHover" }));
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x140003E9 RID: 1001
		// (add) Token: 0x06004B36 RID: 19254 RVA: 0x0001A635 File Offset: 0x00018835
		// (remove) Token: 0x06004B37 RID: 19255 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event MouseEventHandler MouseMove
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "MouseMove" }));
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x140003EA RID: 1002
		// (add) Token: 0x06004B38 RID: 19256 RVA: 0x0001A654 File Offset: 0x00018854
		// (remove) Token: 0x06004B39 RID: 19257 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event MouseEventHandler MouseUp
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "MouseUp" }));
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x140003EB RID: 1003
		// (add) Token: 0x06004B3A RID: 19258 RVA: 0x0001A673 File Offset: 0x00018873
		// (remove) Token: 0x06004B3B RID: 19259 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event MouseEventHandler MouseWheel
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "MouseWheel" }));
			}
			remove
			{
			}
		}

		/// <summary>Occurs when the focus or keyboard user interface (UI) cues change.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x140003EC RID: 1004
		// (add) Token: 0x06004B3C RID: 19260 RVA: 0x0001A692 File Offset: 0x00018892
		// (remove) Token: 0x06004B3D RID: 19261 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event UICuesEventHandler ChangeUICues
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "ChangeUICues" }));
			}
			remove
			{
			}
		}

		/// <summary>This event is not supported by this control.</summary>
		/// <exception cref="T:System.NotSupportedException">A handler is being added to this event.</exception>
		// Token: 0x140003ED RID: 1005
		// (add) Token: 0x06004B3E RID: 19262 RVA: 0x0001A6B1 File Offset: 0x000188B1
		// (remove) Token: 0x06004B3F RID: 19263 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler StyleChanged
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "StyleChanged" }));
			}
			remove
			{
			}
		}

		// Token: 0x040027F9 RID: 10233
		private WebBrowserHelper.AXState axState;

		// Token: 0x040027FA RID: 10234
		private WebBrowserHelper.AXState axReloadingState;

		// Token: 0x040027FB RID: 10235
		private WebBrowserHelper.AXEditMode axEditMode;

		// Token: 0x040027FC RID: 10236
		private bool inRtlRecreate;

		// Token: 0x040027FD RID: 10237
		private BitVector32 axHostState;

		// Token: 0x040027FE RID: 10238
		private WebBrowserHelper.SelectionStyle selectionStyle;

		// Token: 0x040027FF RID: 10239
		private int noComponentChange;

		// Token: 0x04002800 RID: 10240
		private WebBrowserSiteBase axSite;

		// Token: 0x04002801 RID: 10241
		private ContainerControl containingControl;

		// Token: 0x04002802 RID: 10242
		private IntPtr hwndFocus = IntPtr.Zero;

		// Token: 0x04002803 RID: 10243
		private EventHandler selectionChangeHandler;

		// Token: 0x04002804 RID: 10244
		private Guid clsid;

		// Token: 0x04002805 RID: 10245
		private UnsafeNativeMethods.IOleObject axOleObject;

		// Token: 0x04002806 RID: 10246
		private UnsafeNativeMethods.IOleInPlaceObject axOleInPlaceObject;

		// Token: 0x04002807 RID: 10247
		private UnsafeNativeMethods.IOleInPlaceActiveObject axOleInPlaceActiveObject;

		// Token: 0x04002808 RID: 10248
		private UnsafeNativeMethods.IOleControl axOleControl;

		// Token: 0x04002809 RID: 10249
		private WebBrowserBase.WebBrowserBaseNativeWindow axWindow;

		// Token: 0x0400280A RID: 10250
		private Size webBrowserBaseChangingSize = Size.Empty;

		// Token: 0x0400280B RID: 10251
		private WebBrowserContainer wbContainer;

		// Token: 0x0400280C RID: 10252
		private bool ignoreDialogKeys;

		// Token: 0x0400280D RID: 10253
		internal WebBrowserContainer container;

		// Token: 0x0400280E RID: 10254
		internal object activeXInstance;

		// Token: 0x02000827 RID: 2087
		private class WebBrowserBaseNativeWindow : NativeWindow
		{
			// Token: 0x06007006 RID: 28678 RVA: 0x0019A0B9 File Offset: 0x001982B9
			public WebBrowserBaseNativeWindow(WebBrowserBase ax)
			{
				this.WebBrowserBase = ax;
			}

			// Token: 0x06007007 RID: 28679 RVA: 0x0019A0C8 File Offset: 0x001982C8
			protected override void WndProc(ref Message m)
			{
				int msg = m.Msg;
				if (msg == 70)
				{
					this.WmWindowPosChanging(ref m);
					return;
				}
				base.WndProc(ref m);
			}

			// Token: 0x06007008 RID: 28680 RVA: 0x0019A0F0 File Offset: 0x001982F0
			private unsafe void WmWindowPosChanging(ref Message m)
			{
				NativeMethods.WINDOWPOS* ptr = (NativeMethods.WINDOWPOS*)(void*)m.LParam;
				ptr->x = 0;
				ptr->y = 0;
				Size webBrowserBaseChangingSize = this.WebBrowserBase.webBrowserBaseChangingSize;
				if (webBrowserBaseChangingSize.Width == -1)
				{
					ptr->cx = this.WebBrowserBase.Width;
					ptr->cy = this.WebBrowserBase.Height;
				}
				else
				{
					ptr->cx = webBrowserBaseChangingSize.Width;
					ptr->cy = webBrowserBaseChangingSize.Height;
				}
				m.Result = (IntPtr)0;
			}

			// Token: 0x0400433C RID: 17212
			private WebBrowserBase WebBrowserBase;
		}
	}
}
