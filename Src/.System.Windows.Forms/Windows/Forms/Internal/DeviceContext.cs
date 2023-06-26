using System;
using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.Internal
{
	// Token: 0x020004D3 RID: 1235
	internal sealed class DeviceContext : MarshalByRefObject, IDeviceContext, IDisposable
	{
		// Token: 0x14000416 RID: 1046
		// (add) Token: 0x060050FC RID: 20732 RVA: 0x00152224 File Offset: 0x00150424
		// (remove) Token: 0x060050FD RID: 20733 RVA: 0x0015225C File Offset: 0x0015045C
		public event EventHandler Disposing;

		// Token: 0x17001390 RID: 5008
		// (get) Token: 0x060050FE RID: 20734 RVA: 0x00152291 File Offset: 0x00150491
		public DeviceContextType DeviceContextType
		{
			get
			{
				return this.dcType;
			}
		}

		// Token: 0x17001391 RID: 5009
		// (get) Token: 0x060050FF RID: 20735 RVA: 0x00152299 File Offset: 0x00150499
		public IntPtr Hdc
		{
			get
			{
				if (this.hDC == IntPtr.Zero && this.dcType == DeviceContextType.Display)
				{
					this.hDC = ((IDeviceContext)this).GetHdc();
					this.CacheInitialState();
				}
				return this.hDC;
			}
		}

		// Token: 0x06005100 RID: 20736 RVA: 0x001522D0 File Offset: 0x001504D0
		private void CacheInitialState()
		{
			this.hCurrentPen = (this.hInitialPen = IntUnsafeNativeMethods.GetCurrentObject(new HandleRef(this, this.hDC), 1));
			this.hCurrentBrush = (this.hInitialBrush = IntUnsafeNativeMethods.GetCurrentObject(new HandleRef(this, this.hDC), 2));
			this.hCurrentBmp = (this.hInitialBmp = IntUnsafeNativeMethods.GetCurrentObject(new HandleRef(this, this.hDC), 7));
			this.hCurrentFont = (this.hInitialFont = IntUnsafeNativeMethods.GetCurrentObject(new HandleRef(this, this.hDC), 6));
		}

		// Token: 0x06005101 RID: 20737 RVA: 0x00152364 File Offset: 0x00150564
		public void DeleteObject(IntPtr handle, GdiObjectType type)
		{
			IntPtr intPtr = IntPtr.Zero;
			if (type != GdiObjectType.Pen)
			{
				if (type != GdiObjectType.Brush)
				{
					if (type == GdiObjectType.Bitmap)
					{
						if (handle == this.hCurrentBmp)
						{
							IntPtr intPtr2 = IntUnsafeNativeMethods.SelectObject(new HandleRef(this, this.Hdc), new HandleRef(this, this.hInitialBmp));
							this.hCurrentBmp = IntPtr.Zero;
						}
						intPtr = handle;
					}
				}
				else
				{
					if (handle == this.hCurrentBrush)
					{
						IntPtr intPtr3 = IntUnsafeNativeMethods.SelectObject(new HandleRef(this, this.Hdc), new HandleRef(this, this.hInitialBrush));
						this.hCurrentBrush = IntPtr.Zero;
					}
					intPtr = handle;
				}
			}
			else
			{
				if (handle == this.hCurrentPen)
				{
					IntPtr intPtr4 = IntUnsafeNativeMethods.SelectObject(new HandleRef(this, this.Hdc), new HandleRef(this, this.hInitialPen));
					this.hCurrentPen = IntPtr.Zero;
				}
				intPtr = handle;
			}
			IntUnsafeNativeMethods.DeleteObject(new HandleRef(this, intPtr));
		}

		// Token: 0x06005102 RID: 20738 RVA: 0x00152444 File Offset: 0x00150644
		private DeviceContext(IntPtr hWnd)
		{
			this.hWnd = hWnd;
			this.dcType = DeviceContextType.Display;
			DeviceContexts.AddDeviceContext(this);
		}

		// Token: 0x06005103 RID: 20739 RVA: 0x0015246C File Offset: 0x0015066C
		private DeviceContext(IntPtr hDC, DeviceContextType dcType)
		{
			this.hDC = hDC;
			this.dcType = dcType;
			this.CacheInitialState();
			DeviceContexts.AddDeviceContext(this);
			if (dcType == DeviceContextType.Display)
			{
				this.hWnd = IntUnsafeNativeMethods.WindowFromDC(new HandleRef(this, this.hDC));
			}
		}

		// Token: 0x06005104 RID: 20740 RVA: 0x001524C0 File Offset: 0x001506C0
		public static DeviceContext CreateDC(string driverName, string deviceName, string fileName, HandleRef devMode)
		{
			IntPtr intPtr = IntUnsafeNativeMethods.CreateDC(driverName, deviceName, fileName, devMode);
			return new DeviceContext(intPtr, DeviceContextType.NamedDevice);
		}

		// Token: 0x06005105 RID: 20741 RVA: 0x001524E0 File Offset: 0x001506E0
		public static DeviceContext CreateIC(string driverName, string deviceName, string fileName, HandleRef devMode)
		{
			IntPtr intPtr = IntUnsafeNativeMethods.CreateIC(driverName, deviceName, fileName, devMode);
			return new DeviceContext(intPtr, DeviceContextType.Information);
		}

		// Token: 0x06005106 RID: 20742 RVA: 0x00152500 File Offset: 0x00150700
		public static DeviceContext FromCompatibleDC(IntPtr hdc)
		{
			IntPtr intPtr = IntUnsafeNativeMethods.CreateCompatibleDC(new HandleRef(null, hdc));
			return new DeviceContext(intPtr, DeviceContextType.Memory);
		}

		// Token: 0x06005107 RID: 20743 RVA: 0x00152521 File Offset: 0x00150721
		public static DeviceContext FromHdc(IntPtr hdc)
		{
			return new DeviceContext(hdc, DeviceContextType.Unknown);
		}

		// Token: 0x06005108 RID: 20744 RVA: 0x0015252A File Offset: 0x0015072A
		public static DeviceContext FromHwnd(IntPtr hwnd)
		{
			return new DeviceContext(hwnd);
		}

		// Token: 0x06005109 RID: 20745 RVA: 0x00152534 File Offset: 0x00150734
		~DeviceContext()
		{
			this.Dispose(false);
		}

		// Token: 0x0600510A RID: 20746 RVA: 0x00152564 File Offset: 0x00150764
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600510B RID: 20747 RVA: 0x00152574 File Offset: 0x00150774
		internal void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			if (this.Disposing != null)
			{
				this.Disposing(this, EventArgs.Empty);
			}
			this.disposed = true;
			this.DisposeFont(disposing);
			switch (this.dcType)
			{
			case DeviceContextType.Unknown:
			case DeviceContextType.NCWindow:
				return;
			case DeviceContextType.Display:
				((IDeviceContext)this).ReleaseHdc();
				return;
			case DeviceContextType.NamedDevice:
			case DeviceContextType.Information:
				IntUnsafeNativeMethods.DeleteHDC(new HandleRef(this, this.hDC));
				this.hDC = IntPtr.Zero;
				return;
			case DeviceContextType.Memory:
				IntUnsafeNativeMethods.DeleteDC(new HandleRef(this, this.hDC));
				this.hDC = IntPtr.Zero;
				return;
			default:
				return;
			}
		}

		// Token: 0x0600510C RID: 20748 RVA: 0x0015261A File Offset: 0x0015081A
		IntPtr IDeviceContext.GetHdc()
		{
			if (this.hDC == IntPtr.Zero)
			{
				this.hDC = IntUnsafeNativeMethods.GetDC(new HandleRef(this, this.hWnd));
			}
			return this.hDC;
		}

		// Token: 0x0600510D RID: 20749 RVA: 0x0015264C File Offset: 0x0015084C
		void IDeviceContext.ReleaseHdc()
		{
			if (this.hDC != IntPtr.Zero && this.dcType == DeviceContextType.Display)
			{
				IntUnsafeNativeMethods.ReleaseDC(new HandleRef(this, this.hWnd), new HandleRef(this, this.hDC));
				this.hDC = IntPtr.Zero;
			}
		}

		// Token: 0x17001392 RID: 5010
		// (get) Token: 0x0600510E RID: 20750 RVA: 0x0015269D File Offset: 0x0015089D
		public DeviceContextGraphicsMode GraphicsMode
		{
			get
			{
				return (DeviceContextGraphicsMode)IntUnsafeNativeMethods.GetGraphicsMode(new HandleRef(this, this.Hdc));
			}
		}

		// Token: 0x0600510F RID: 20751 RVA: 0x001526B0 File Offset: 0x001508B0
		public DeviceContextGraphicsMode SetGraphicsMode(DeviceContextGraphicsMode newMode)
		{
			return (DeviceContextGraphicsMode)IntUnsafeNativeMethods.SetGraphicsMode(new HandleRef(this, this.Hdc), (int)newMode);
		}

		// Token: 0x06005110 RID: 20752 RVA: 0x001526C4 File Offset: 0x001508C4
		public void RestoreHdc()
		{
			IntUnsafeNativeMethods.RestoreDC(new HandleRef(this, this.hDC), -1);
			if (this.contextStack != null)
			{
				DeviceContext.GraphicsState graphicsState = (DeviceContext.GraphicsState)this.contextStack.Pop();
				this.hCurrentBmp = graphicsState.hBitmap;
				this.hCurrentBrush = graphicsState.hBrush;
				this.hCurrentPen = graphicsState.hPen;
				this.hCurrentFont = graphicsState.hFont;
				if (graphicsState.font != null && graphicsState.font.IsAlive)
				{
					this.selectedFont = graphicsState.font.Target as WindowsFont;
				}
				else
				{
					WindowsFont windowsFont = this.selectedFont;
					this.selectedFont = null;
					if (windowsFont != null && MeasurementDCInfo.IsMeasurementDC(this))
					{
						windowsFont.Dispose();
					}
				}
			}
			MeasurementDCInfo.ResetIfIsMeasurementDC(this.hDC);
		}

		// Token: 0x06005111 RID: 20753 RVA: 0x00152788 File Offset: 0x00150988
		public int SaveHdc()
		{
			HandleRef handleRef = new HandleRef(this, this.Hdc);
			int num = IntUnsafeNativeMethods.SaveDC(handleRef);
			if (this.contextStack == null)
			{
				this.contextStack = new Stack();
			}
			DeviceContext.GraphicsState graphicsState = new DeviceContext.GraphicsState();
			graphicsState.hBitmap = this.hCurrentBmp;
			graphicsState.hBrush = this.hCurrentBrush;
			graphicsState.hPen = this.hCurrentPen;
			graphicsState.hFont = this.hCurrentFont;
			graphicsState.font = new WeakReference(this.selectedFont);
			this.contextStack.Push(graphicsState);
			return num;
		}

		// Token: 0x06005112 RID: 20754 RVA: 0x00152814 File Offset: 0x00150A14
		public void SetClip(WindowsRegion region)
		{
			HandleRef handleRef = new HandleRef(this, this.Hdc);
			HandleRef handleRef2 = new HandleRef(region, region.HRegion);
			IntUnsafeNativeMethods.SelectClipRgn(handleRef, handleRef2);
		}

		// Token: 0x06005113 RID: 20755 RVA: 0x00152848 File Offset: 0x00150A48
		public void IntersectClip(WindowsRegion wr)
		{
			if (wr.HRegion == IntPtr.Zero)
			{
				return;
			}
			WindowsRegion windowsRegion = new WindowsRegion(0, 0, 0, 0);
			try
			{
				int clipRgn = IntUnsafeNativeMethods.GetClipRgn(new HandleRef(this, this.Hdc), new HandleRef(windowsRegion, windowsRegion.HRegion));
				if (clipRgn == 1)
				{
					wr.CombineRegion(windowsRegion, wr, RegionCombineMode.AND);
				}
				this.SetClip(wr);
			}
			finally
			{
				windowsRegion.Dispose();
			}
		}

		// Token: 0x06005114 RID: 20756 RVA: 0x001528C0 File Offset: 0x00150AC0
		public void TranslateTransform(int dx, int dy)
		{
			IntNativeMethods.POINT point = new IntNativeMethods.POINT();
			IntUnsafeNativeMethods.OffsetViewportOrgEx(new HandleRef(this, this.Hdc), dx, dy, point);
		}

		// Token: 0x06005115 RID: 20757 RVA: 0x001528E8 File Offset: 0x00150AE8
		public override bool Equals(object obj)
		{
			DeviceContext deviceContext = obj as DeviceContext;
			return deviceContext == this || (deviceContext != null && deviceContext.Hdc == this.Hdc);
		}

		// Token: 0x06005116 RID: 20758 RVA: 0x00152918 File Offset: 0x00150B18
		public override int GetHashCode()
		{
			return this.Hdc.GetHashCode();
		}

		// Token: 0x17001393 RID: 5011
		// (get) Token: 0x06005117 RID: 20759 RVA: 0x00152933 File Offset: 0x00150B33
		public WindowsFont ActiveFont
		{
			get
			{
				return this.selectedFont;
			}
		}

		// Token: 0x17001394 RID: 5012
		// (get) Token: 0x06005118 RID: 20760 RVA: 0x0015293B File Offset: 0x00150B3B
		public Color BackgroundColor
		{
			get
			{
				return ColorTranslator.FromWin32(IntUnsafeNativeMethods.GetBkColor(new HandleRef(this, this.Hdc)));
			}
		}

		// Token: 0x06005119 RID: 20761 RVA: 0x00152953 File Offset: 0x00150B53
		public Color SetBackgroundColor(Color newColor)
		{
			return ColorTranslator.FromWin32(IntUnsafeNativeMethods.SetBkColor(new HandleRef(this, this.Hdc), ColorTranslator.ToWin32(newColor)));
		}

		// Token: 0x17001395 RID: 5013
		// (get) Token: 0x0600511A RID: 20762 RVA: 0x00152971 File Offset: 0x00150B71
		public DeviceContextBackgroundMode BackgroundMode
		{
			get
			{
				return (DeviceContextBackgroundMode)IntUnsafeNativeMethods.GetBkMode(new HandleRef(this, this.Hdc));
			}
		}

		// Token: 0x0600511B RID: 20763 RVA: 0x00152984 File Offset: 0x00150B84
		public DeviceContextBackgroundMode SetBackgroundMode(DeviceContextBackgroundMode newMode)
		{
			return (DeviceContextBackgroundMode)IntUnsafeNativeMethods.SetBkMode(new HandleRef(this, this.Hdc), (int)newMode);
		}

		// Token: 0x17001396 RID: 5014
		// (get) Token: 0x0600511C RID: 20764 RVA: 0x00152998 File Offset: 0x00150B98
		public DeviceContextBinaryRasterOperationFlags BinaryRasterOperation
		{
			get
			{
				return (DeviceContextBinaryRasterOperationFlags)IntUnsafeNativeMethods.GetROP2(new HandleRef(this, this.Hdc));
			}
		}

		// Token: 0x0600511D RID: 20765 RVA: 0x001529AB File Offset: 0x00150BAB
		public DeviceContextBinaryRasterOperationFlags SetRasterOperation(DeviceContextBinaryRasterOperationFlags rasterOperation)
		{
			return (DeviceContextBinaryRasterOperationFlags)IntUnsafeNativeMethods.SetROP2(new HandleRef(this, this.Hdc), (int)rasterOperation);
		}

		// Token: 0x17001397 RID: 5015
		// (get) Token: 0x0600511E RID: 20766 RVA: 0x001529BF File Offset: 0x00150BBF
		public Size Dpi
		{
			get
			{
				return new Size(this.GetDeviceCapabilities(DeviceCapabilities.LogicalPixelsX), this.GetDeviceCapabilities(DeviceCapabilities.LogicalPixelsY));
			}
		}

		// Token: 0x17001398 RID: 5016
		// (get) Token: 0x0600511F RID: 20767 RVA: 0x001529D6 File Offset: 0x00150BD6
		public int DpiX
		{
			get
			{
				return this.GetDeviceCapabilities(DeviceCapabilities.LogicalPixelsX);
			}
		}

		// Token: 0x17001399 RID: 5017
		// (get) Token: 0x06005120 RID: 20768 RVA: 0x001529E0 File Offset: 0x00150BE0
		public int DpiY
		{
			get
			{
				return this.GetDeviceCapabilities(DeviceCapabilities.LogicalPixelsY);
			}
		}

		// Token: 0x1700139A RID: 5018
		// (get) Token: 0x06005121 RID: 20769 RVA: 0x001529EC File Offset: 0x00150BEC
		public WindowsFont Font
		{
			get
			{
				if (MeasurementDCInfo.IsMeasurementDC(this))
				{
					WindowsFont lastUsedFont = MeasurementDCInfo.LastUsedFont;
					if (lastUsedFont != null && lastUsedFont.Hfont != IntPtr.Zero)
					{
						return lastUsedFont;
					}
				}
				return WindowsFont.FromHdc(this.Hdc);
			}
		}

		// Token: 0x1700139B RID: 5019
		// (get) Token: 0x06005122 RID: 20770 RVA: 0x00152A29 File Offset: 0x00150C29
		public static DeviceContext ScreenDC
		{
			get
			{
				return DeviceContext.FromHwnd(IntPtr.Zero);
			}
		}

		// Token: 0x06005123 RID: 20771 RVA: 0x00152A38 File Offset: 0x00150C38
		internal void DisposeFont(bool disposing)
		{
			if (disposing)
			{
				DeviceContexts.RemoveDeviceContext(this);
			}
			if (this.selectedFont != null && this.selectedFont.Hfont != IntPtr.Zero)
			{
				IntPtr currentObject = IntUnsafeNativeMethods.GetCurrentObject(new HandleRef(this, this.hDC), 6);
				if (currentObject == this.selectedFont.Hfont)
				{
					IntUnsafeNativeMethods.SelectObject(new HandleRef(this, this.Hdc), new HandleRef(null, this.hInitialFont));
					currentObject = this.hInitialFont;
				}
				this.selectedFont.Dispose(disposing);
				this.selectedFont = null;
			}
		}

		// Token: 0x06005124 RID: 20772 RVA: 0x00152ACC File Offset: 0x00150CCC
		public IntPtr SelectFont(WindowsFont font)
		{
			if (font.Equals(this.Font))
			{
				return IntPtr.Zero;
			}
			IntPtr intPtr = this.SelectObject(font.Hfont, GdiObjectType.Font);
			WindowsFont windowsFont = this.selectedFont;
			this.selectedFont = font;
			this.hCurrentFont = font.Hfont;
			if (windowsFont != null && MeasurementDCInfo.IsMeasurementDC(this))
			{
				windowsFont.Dispose();
			}
			if (MeasurementDCInfo.IsMeasurementDC(this))
			{
				if (intPtr != IntPtr.Zero)
				{
					MeasurementDCInfo.LastUsedFont = font;
				}
				else
				{
					MeasurementDCInfo.Reset();
				}
			}
			return intPtr;
		}

		// Token: 0x06005125 RID: 20773 RVA: 0x00152B49 File Offset: 0x00150D49
		public void ResetFont()
		{
			MeasurementDCInfo.ResetIfIsMeasurementDC(this.Hdc);
			IntUnsafeNativeMethods.SelectObject(new HandleRef(this, this.Hdc), new HandleRef(null, this.hInitialFont));
			this.selectedFont = null;
			this.hCurrentFont = this.hInitialFont;
		}

		// Token: 0x06005126 RID: 20774 RVA: 0x00152B87 File Offset: 0x00150D87
		public int GetDeviceCapabilities(DeviceCapabilities capabilityIndex)
		{
			return IntUnsafeNativeMethods.GetDeviceCaps(new HandleRef(this, this.Hdc), (int)capabilityIndex);
		}

		// Token: 0x1700139C RID: 5020
		// (get) Token: 0x06005127 RID: 20775 RVA: 0x00152B9B File Offset: 0x00150D9B
		public DeviceContextMapMode MapMode
		{
			get
			{
				return (DeviceContextMapMode)IntUnsafeNativeMethods.GetMapMode(new HandleRef(this, this.Hdc));
			}
		}

		// Token: 0x06005128 RID: 20776 RVA: 0x00152BB0 File Offset: 0x00150DB0
		public bool IsFontOnContextStack(WindowsFont wf)
		{
			if (this.contextStack == null)
			{
				return false;
			}
			foreach (object obj in this.contextStack)
			{
				DeviceContext.GraphicsState graphicsState = (DeviceContext.GraphicsState)obj;
				if (graphicsState.hFont == wf.Hfont)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005129 RID: 20777 RVA: 0x00152C28 File Offset: 0x00150E28
		public DeviceContextMapMode SetMapMode(DeviceContextMapMode newMode)
		{
			return (DeviceContextMapMode)IntUnsafeNativeMethods.SetMapMode(new HandleRef(this, this.Hdc), (int)newMode);
		}

		// Token: 0x0600512A RID: 20778 RVA: 0x00152C3C File Offset: 0x00150E3C
		public IntPtr SelectObject(IntPtr hObj, GdiObjectType type)
		{
			if (type != GdiObjectType.Pen)
			{
				if (type != GdiObjectType.Brush)
				{
					if (type == GdiObjectType.Bitmap)
					{
						this.hCurrentBmp = hObj;
					}
				}
				else
				{
					this.hCurrentBrush = hObj;
				}
			}
			else
			{
				this.hCurrentPen = hObj;
			}
			return IntUnsafeNativeMethods.SelectObject(new HandleRef(this, this.Hdc), new HandleRef(null, hObj));
		}

		// Token: 0x1700139D RID: 5021
		// (get) Token: 0x0600512B RID: 20779 RVA: 0x00152C88 File Offset: 0x00150E88
		public DeviceContextTextAlignment TextAlignment
		{
			get
			{
				return (DeviceContextTextAlignment)IntUnsafeNativeMethods.GetTextAlign(new HandleRef(this, this.Hdc));
			}
		}

		// Token: 0x0600512C RID: 20780 RVA: 0x00152C9B File Offset: 0x00150E9B
		public DeviceContextTextAlignment SetTextAlignment(DeviceContextTextAlignment newAligment)
		{
			return (DeviceContextTextAlignment)IntUnsafeNativeMethods.SetTextAlign(new HandleRef(this, this.Hdc), (int)newAligment);
		}

		// Token: 0x1700139E RID: 5022
		// (get) Token: 0x0600512D RID: 20781 RVA: 0x00152CAF File Offset: 0x00150EAF
		public Color TextColor
		{
			get
			{
				return ColorTranslator.FromWin32(IntUnsafeNativeMethods.GetTextColor(new HandleRef(this, this.Hdc)));
			}
		}

		// Token: 0x0600512E RID: 20782 RVA: 0x00152CC7 File Offset: 0x00150EC7
		public Color SetTextColor(Color newColor)
		{
			return ColorTranslator.FromWin32(IntUnsafeNativeMethods.SetTextColor(new HandleRef(this, this.Hdc), ColorTranslator.ToWin32(newColor)));
		}

		// Token: 0x1700139F RID: 5023
		// (get) Token: 0x0600512F RID: 20783 RVA: 0x00152CE8 File Offset: 0x00150EE8
		// (set) Token: 0x06005130 RID: 20784 RVA: 0x00152D14 File Offset: 0x00150F14
		public Size ViewportExtent
		{
			get
			{
				IntNativeMethods.SIZE size = new IntNativeMethods.SIZE();
				IntUnsafeNativeMethods.GetViewportExtEx(new HandleRef(this, this.Hdc), size);
				return size.ToSize();
			}
			set
			{
				this.SetViewportExtent(value);
			}
		}

		// Token: 0x06005131 RID: 20785 RVA: 0x00152D20 File Offset: 0x00150F20
		public Size SetViewportExtent(Size newExtent)
		{
			IntNativeMethods.SIZE size = new IntNativeMethods.SIZE();
			IntUnsafeNativeMethods.SetViewportExtEx(new HandleRef(this, this.Hdc), newExtent.Width, newExtent.Height, size);
			return size.ToSize();
		}

		// Token: 0x170013A0 RID: 5024
		// (get) Token: 0x06005132 RID: 20786 RVA: 0x00152D5C File Offset: 0x00150F5C
		// (set) Token: 0x06005133 RID: 20787 RVA: 0x00152D88 File Offset: 0x00150F88
		public Point ViewportOrigin
		{
			get
			{
				IntNativeMethods.POINT point = new IntNativeMethods.POINT();
				IntUnsafeNativeMethods.GetViewportOrgEx(new HandleRef(this, this.Hdc), point);
				return point.ToPoint();
			}
			set
			{
				this.SetViewportOrigin(value);
			}
		}

		// Token: 0x06005134 RID: 20788 RVA: 0x00152D94 File Offset: 0x00150F94
		public Point SetViewportOrigin(Point newOrigin)
		{
			IntNativeMethods.POINT point = new IntNativeMethods.POINT();
			IntUnsafeNativeMethods.SetViewportOrgEx(new HandleRef(this, this.Hdc), newOrigin.X, newOrigin.Y, point);
			return point.ToPoint();
		}

		// Token: 0x040034FE RID: 13566
		private IntPtr hDC;

		// Token: 0x040034FF RID: 13567
		private DeviceContextType dcType;

		// Token: 0x04003501 RID: 13569
		private bool disposed;

		// Token: 0x04003502 RID: 13570
		private IntPtr hWnd = (IntPtr)(-1);

		// Token: 0x04003503 RID: 13571
		private IntPtr hInitialPen;

		// Token: 0x04003504 RID: 13572
		private IntPtr hInitialBrush;

		// Token: 0x04003505 RID: 13573
		private IntPtr hInitialBmp;

		// Token: 0x04003506 RID: 13574
		private IntPtr hInitialFont;

		// Token: 0x04003507 RID: 13575
		private IntPtr hCurrentPen;

		// Token: 0x04003508 RID: 13576
		private IntPtr hCurrentBrush;

		// Token: 0x04003509 RID: 13577
		private IntPtr hCurrentBmp;

		// Token: 0x0400350A RID: 13578
		private IntPtr hCurrentFont;

		// Token: 0x0400350B RID: 13579
		private Stack contextStack;

		// Token: 0x0400350C RID: 13580
		private WindowsFont selectedFont;

		// Token: 0x02000872 RID: 2162
		internal class GraphicsState
		{
			// Token: 0x04004420 RID: 17440
			internal IntPtr hBrush;

			// Token: 0x04004421 RID: 17441
			internal IntPtr hFont;

			// Token: 0x04004422 RID: 17442
			internal IntPtr hPen;

			// Token: 0x04004423 RID: 17443
			internal IntPtr hBitmap;

			// Token: 0x04004424 RID: 17444
			internal WeakReference font;
		}
	}
}
