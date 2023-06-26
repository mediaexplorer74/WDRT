using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Implements the interfaces of an ActiveX site for use as a base class by the <see cref="T:System.Windows.Forms.WebBrowser.WebBrowserSite" /> class.</summary>
	// Token: 0x0200043E RID: 1086
	public class WebBrowserSiteBase : UnsafeNativeMethods.IOleControlSite, UnsafeNativeMethods.IOleClientSite, UnsafeNativeMethods.IOleInPlaceSite, UnsafeNativeMethods.ISimpleFrameSite, UnsafeNativeMethods.IPropertyNotifySink, IDisposable
	{
		// Token: 0x06004B84 RID: 19332 RVA: 0x0013A52D File Offset: 0x0013872D
		internal WebBrowserSiteBase(WebBrowserBase h)
		{
			if (h == null)
			{
				throw new ArgumentNullException("h");
			}
			this.host = h;
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Windows.Forms.WebBrowserSiteBase" />.</summary>
		// Token: 0x06004B85 RID: 19333 RVA: 0x0013A54A File Offset: 0x0013874A
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.WebBrowserSiteBase" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06004B86 RID: 19334 RVA: 0x0013A553 File Offset: 0x00138753
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.StopEvents();
			}
		}

		// Token: 0x1700126C RID: 4716
		// (get) Token: 0x06004B87 RID: 19335 RVA: 0x0013A55E File Offset: 0x0013875E
		internal WebBrowserBase Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x06004B88 RID: 19336 RVA: 0x0001180C File Offset: 0x0000FA0C
		int UnsafeNativeMethods.IOleControlSite.OnControlInfoChanged()
		{
			return 0;
		}

		// Token: 0x06004B89 RID: 19337 RVA: 0x0003BC68 File Offset: 0x00039E68
		int UnsafeNativeMethods.IOleControlSite.LockInPlaceActive(int fLock)
		{
			return -2147467263;
		}

		// Token: 0x06004B8A RID: 19338 RVA: 0x0003BD47 File Offset: 0x00039F47
		int UnsafeNativeMethods.IOleControlSite.GetExtendedControl(out object ppDisp)
		{
			ppDisp = null;
			return -2147467263;
		}

		// Token: 0x06004B8B RID: 19339 RVA: 0x0013A568 File Offset: 0x00138768
		int UnsafeNativeMethods.IOleControlSite.TransformCoords(NativeMethods._POINTL pPtlHimetric, NativeMethods.tagPOINTF pPtfContainer, int dwFlags)
		{
			if ((dwFlags & 4) != 0)
			{
				if ((dwFlags & 2) != 0)
				{
					pPtfContainer.x = (float)WebBrowserHelper.HM2Pix(pPtlHimetric.x, WebBrowserHelper.LogPixelsX);
					pPtfContainer.y = (float)WebBrowserHelper.HM2Pix(pPtlHimetric.y, WebBrowserHelper.LogPixelsY);
				}
				else
				{
					if ((dwFlags & 1) == 0)
					{
						return -2147024809;
					}
					pPtfContainer.x = (float)WebBrowserHelper.HM2Pix(pPtlHimetric.x, WebBrowserHelper.LogPixelsX);
					pPtfContainer.y = (float)WebBrowserHelper.HM2Pix(pPtlHimetric.y, WebBrowserHelper.LogPixelsY);
				}
			}
			else
			{
				if ((dwFlags & 8) == 0)
				{
					return -2147024809;
				}
				if ((dwFlags & 2) != 0)
				{
					pPtlHimetric.x = WebBrowserHelper.Pix2HM((int)pPtfContainer.x, WebBrowserHelper.LogPixelsX);
					pPtlHimetric.y = WebBrowserHelper.Pix2HM((int)pPtfContainer.y, WebBrowserHelper.LogPixelsY);
				}
				else
				{
					if ((dwFlags & 1) == 0)
					{
						return -2147024809;
					}
					pPtlHimetric.x = WebBrowserHelper.Pix2HM((int)pPtfContainer.x, WebBrowserHelper.LogPixelsX);
					pPtlHimetric.y = WebBrowserHelper.Pix2HM((int)pPtfContainer.y, WebBrowserHelper.LogPixelsY);
				}
			}
			return 0;
		}

		// Token: 0x06004B8C RID: 19340 RVA: 0x0013A66C File Offset: 0x0013886C
		int UnsafeNativeMethods.IOleControlSite.TranslateAccelerator(ref NativeMethods.MSG pMsg, int grfModifiers)
		{
			this.Host.SetAXHostState(WebBrowserHelper.siteProcessedInputKey, true);
			Message message = default(Message);
			message.Msg = pMsg.message;
			message.WParam = pMsg.wParam;
			message.LParam = pMsg.lParam;
			message.HWnd = pMsg.hwnd;
			int num;
			try
			{
				num = ((this.Host.PreProcessControlMessage(ref message) == PreProcessControlState.MessageProcessed) ? 0 : 1);
			}
			finally
			{
				this.Host.SetAXHostState(WebBrowserHelper.siteProcessedInputKey, false);
			}
			return num;
		}

		// Token: 0x06004B8D RID: 19341 RVA: 0x0001180C File Offset: 0x0000FA0C
		int UnsafeNativeMethods.IOleControlSite.OnFocus(int fGotFocus)
		{
			return 0;
		}

		// Token: 0x06004B8E RID: 19342 RVA: 0x0003BC68 File Offset: 0x00039E68
		int UnsafeNativeMethods.IOleControlSite.ShowPropertyFrame()
		{
			return -2147467263;
		}

		// Token: 0x06004B8F RID: 19343 RVA: 0x0003BC68 File Offset: 0x00039E68
		int UnsafeNativeMethods.IOleClientSite.SaveObject()
		{
			return -2147467263;
		}

		// Token: 0x06004B90 RID: 19344 RVA: 0x0003BC6F File Offset: 0x00039E6F
		int UnsafeNativeMethods.IOleClientSite.GetMoniker(int dwAssign, int dwWhichMoniker, out object moniker)
		{
			moniker = null;
			return -2147467263;
		}

		// Token: 0x06004B91 RID: 19345 RVA: 0x0013A704 File Offset: 0x00138904
		int UnsafeNativeMethods.IOleClientSite.GetContainer(out UnsafeNativeMethods.IOleContainer container)
		{
			container = this.Host.GetParentContainer();
			return 0;
		}

		// Token: 0x06004B92 RID: 19346 RVA: 0x0013A714 File Offset: 0x00138914
		int UnsafeNativeMethods.IOleClientSite.ShowObject()
		{
			if (this.Host.ActiveXState >= WebBrowserHelper.AXState.InPlaceActive)
			{
				IntPtr intPtr;
				if (NativeMethods.Succeeded(this.Host.AXInPlaceObject.GetWindow(out intPtr)))
				{
					if (this.Host.GetHandleNoCreate() != intPtr && intPtr != IntPtr.Zero)
					{
						this.Host.AttachWindow(intPtr);
						this.OnActiveXRectChange(new NativeMethods.COMRECT(this.Host.Bounds));
					}
				}
				else if (this.Host.AXInPlaceObject is UnsafeNativeMethods.IOleInPlaceObjectWindowless)
				{
					throw new InvalidOperationException(SR.GetString("AXWindowlessControl"));
				}
			}
			return 0;
		}

		// Token: 0x06004B93 RID: 19347 RVA: 0x0001180C File Offset: 0x0000FA0C
		int UnsafeNativeMethods.IOleClientSite.OnShowWindow(int fShow)
		{
			return 0;
		}

		// Token: 0x06004B94 RID: 19348 RVA: 0x0003BC68 File Offset: 0x00039E68
		int UnsafeNativeMethods.IOleClientSite.RequestNewObjectLayout()
		{
			return -2147467263;
		}

		// Token: 0x06004B95 RID: 19349 RVA: 0x0013A7B4 File Offset: 0x001389B4
		IntPtr UnsafeNativeMethods.IOleInPlaceSite.GetWindow()
		{
			IntPtr parent;
			try
			{
				parent = UnsafeNativeMethods.GetParent(new HandleRef(this.Host, this.Host.Handle));
			}
			catch (Exception ex)
			{
				throw;
			}
			return parent;
		}

		// Token: 0x06004B96 RID: 19350 RVA: 0x0003BC68 File Offset: 0x00039E68
		int UnsafeNativeMethods.IOleInPlaceSite.ContextSensitiveHelp(int fEnterMode)
		{
			return -2147467263;
		}

		// Token: 0x06004B97 RID: 19351 RVA: 0x0001180C File Offset: 0x0000FA0C
		int UnsafeNativeMethods.IOleInPlaceSite.CanInPlaceActivate()
		{
			return 0;
		}

		// Token: 0x06004B98 RID: 19352 RVA: 0x0013A7F4 File Offset: 0x001389F4
		int UnsafeNativeMethods.IOleInPlaceSite.OnInPlaceActivate()
		{
			this.Host.ActiveXState = WebBrowserHelper.AXState.InPlaceActive;
			this.OnActiveXRectChange(new NativeMethods.COMRECT(this.Host.Bounds));
			return 0;
		}

		// Token: 0x06004B99 RID: 19353 RVA: 0x0013A81A File Offset: 0x00138A1A
		int UnsafeNativeMethods.IOleInPlaceSite.OnUIActivate()
		{
			this.Host.ActiveXState = WebBrowserHelper.AXState.UIActive;
			this.Host.GetParentContainer().OnUIActivate(this.Host);
			return 0;
		}

		// Token: 0x06004B9A RID: 19354 RVA: 0x0013A840 File Offset: 0x00138A40
		int UnsafeNativeMethods.IOleInPlaceSite.GetWindowContext(out UnsafeNativeMethods.IOleInPlaceFrame ppFrame, out UnsafeNativeMethods.IOleInPlaceUIWindow ppDoc, NativeMethods.COMRECT lprcPosRect, NativeMethods.COMRECT lprcClipRect, NativeMethods.tagOIFI lpFrameInfo)
		{
			ppDoc = null;
			ppFrame = this.Host.GetParentContainer();
			lprcPosRect.left = this.Host.Bounds.X;
			lprcPosRect.top = this.Host.Bounds.Y;
			lprcPosRect.right = this.Host.Bounds.Width + this.Host.Bounds.X;
			lprcPosRect.bottom = this.Host.Bounds.Height + this.Host.Bounds.Y;
			lprcClipRect = WebBrowserHelper.GetClipRect();
			if (lpFrameInfo != null)
			{
				lpFrameInfo.cb = Marshal.SizeOf(typeof(NativeMethods.tagOIFI));
				lpFrameInfo.fMDIApp = false;
				lpFrameInfo.hAccel = IntPtr.Zero;
				lpFrameInfo.cAccelEntries = 0;
				lpFrameInfo.hwndFrame = ((this.Host.ParentInternal == null) ? IntPtr.Zero : this.Host.ParentInternal.Handle);
			}
			return 0;
		}

		// Token: 0x06004B9B RID: 19355 RVA: 0x00012E4E File Offset: 0x0001104E
		int UnsafeNativeMethods.IOleInPlaceSite.Scroll(NativeMethods.tagSIZE scrollExtant)
		{
			return 1;
		}

		// Token: 0x06004B9C RID: 19356 RVA: 0x0013A952 File Offset: 0x00138B52
		int UnsafeNativeMethods.IOleInPlaceSite.OnUIDeactivate(int fUndoable)
		{
			this.Host.GetParentContainer().OnUIDeactivate(this.Host);
			if (this.Host.ActiveXState > WebBrowserHelper.AXState.InPlaceActive)
			{
				this.Host.ActiveXState = WebBrowserHelper.AXState.InPlaceActive;
			}
			return 0;
		}

		// Token: 0x06004B9D RID: 19357 RVA: 0x0013A985 File Offset: 0x00138B85
		int UnsafeNativeMethods.IOleInPlaceSite.OnInPlaceDeactivate()
		{
			if (this.Host.ActiveXState == WebBrowserHelper.AXState.UIActive)
			{
				((UnsafeNativeMethods.IOleInPlaceSite)this).OnUIDeactivate(0);
			}
			this.Host.GetParentContainer().OnInPlaceDeactivate(this.Host);
			this.Host.ActiveXState = WebBrowserHelper.AXState.Running;
			return 0;
		}

		// Token: 0x06004B9E RID: 19358 RVA: 0x0001180C File Offset: 0x0000FA0C
		int UnsafeNativeMethods.IOleInPlaceSite.DiscardUndoState()
		{
			return 0;
		}

		// Token: 0x06004B9F RID: 19359 RVA: 0x0013A9C0 File Offset: 0x00138BC0
		int UnsafeNativeMethods.IOleInPlaceSite.DeactivateAndUndo()
		{
			return this.Host.AXInPlaceObject.UIDeactivate();
		}

		// Token: 0x06004BA0 RID: 19360 RVA: 0x0013A9D2 File Offset: 0x00138BD2
		int UnsafeNativeMethods.IOleInPlaceSite.OnPosRectChange(NativeMethods.COMRECT lprcPosRect)
		{
			return this.OnActiveXRectChange(lprcPosRect);
		}

		// Token: 0x06004BA1 RID: 19361 RVA: 0x0001180C File Offset: 0x0000FA0C
		int UnsafeNativeMethods.ISimpleFrameSite.PreMessageFilter(IntPtr hwnd, int msg, IntPtr wp, IntPtr lp, ref IntPtr plResult, ref int pdwCookie)
		{
			return 0;
		}

		// Token: 0x06004BA2 RID: 19362 RVA: 0x00012E4E File Offset: 0x0001104E
		int UnsafeNativeMethods.ISimpleFrameSite.PostMessageFilter(IntPtr hwnd, int msg, IntPtr wp, IntPtr lp, ref IntPtr plResult, int dwCookie)
		{
			return 1;
		}

		// Token: 0x06004BA3 RID: 19363 RVA: 0x0013A9DC File Offset: 0x00138BDC
		void UnsafeNativeMethods.IPropertyNotifySink.OnChanged(int dispid)
		{
			if (this.Host.NoComponentChangeEvents != 0)
			{
				return;
			}
			WebBrowserBase webBrowserBase = this.Host;
			int num = webBrowserBase.NoComponentChangeEvents;
			webBrowserBase.NoComponentChangeEvents = num + 1;
			try
			{
				this.OnPropertyChanged(dispid);
			}
			catch (Exception ex)
			{
				throw;
			}
			finally
			{
				WebBrowserBase webBrowserBase2 = this.Host;
				num = webBrowserBase2.NoComponentChangeEvents;
				webBrowserBase2.NoComponentChangeEvents = num - 1;
			}
		}

		// Token: 0x06004BA4 RID: 19364 RVA: 0x0001180C File Offset: 0x0000FA0C
		int UnsafeNativeMethods.IPropertyNotifySink.OnRequestEdit(int dispid)
		{
			return 0;
		}

		// Token: 0x06004BA5 RID: 19365 RVA: 0x0013AA4C File Offset: 0x00138C4C
		internal virtual void OnPropertyChanged(int dispid)
		{
			try
			{
				ISite site = this.Host.Site;
				if (site != null)
				{
					IComponentChangeService componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
					if (componentChangeService != null)
					{
						try
						{
							componentChangeService.OnComponentChanging(this.Host, null);
						}
						catch (CheckoutException ex)
						{
							if (ex == CheckoutException.Canceled)
							{
								return;
							}
							throw ex;
						}
						componentChangeService.OnComponentChanged(this.Host, null, null, null);
					}
				}
			}
			catch (Exception ex2)
			{
				throw;
			}
		}

		// Token: 0x06004BA6 RID: 19366 RVA: 0x0013AAD0 File Offset: 0x00138CD0
		internal WebBrowserBase GetAXHost()
		{
			return this.Host;
		}

		// Token: 0x06004BA7 RID: 19367 RVA: 0x0013AAD8 File Offset: 0x00138CD8
		internal void StartEvents()
		{
			if (this.connectionPoint != null)
			{
				return;
			}
			object activeXInstance = this.Host.activeXInstance;
			if (activeXInstance != null)
			{
				try
				{
					this.connectionPoint = new AxHost.ConnectionPointCookie(activeXInstance, this, typeof(UnsafeNativeMethods.IPropertyNotifySink));
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

		// Token: 0x06004BA8 RID: 19368 RVA: 0x0013AB34 File Offset: 0x00138D34
		internal void StopEvents()
		{
			if (this.connectionPoint != null)
			{
				this.connectionPoint.Disconnect();
				this.connectionPoint = null;
			}
		}

		// Token: 0x06004BA9 RID: 19369 RVA: 0x0013AB50 File Offset: 0x00138D50
		private int OnActiveXRectChange(NativeMethods.COMRECT lprcPosRect)
		{
			this.Host.AXInPlaceObject.SetObjectRects(NativeMethods.COMRECT.FromXYWH(0, 0, lprcPosRect.right - lprcPosRect.left, lprcPosRect.bottom - lprcPosRect.top), WebBrowserHelper.GetClipRect());
			this.Host.MakeDirty();
			return 0;
		}

		// Token: 0x0400282E RID: 10286
		private WebBrowserBase host;

		// Token: 0x0400282F RID: 10287
		private AxHost.ConnectionPointCookie connectionPoint;
	}
}
