using System;
using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Win32;

namespace System.Windows.Forms
{
	/// <summary>Represents a display device or multiple display devices on a single system.</summary>
	// Token: 0x02000352 RID: 850
	public class Screen
	{
		// Token: 0x06003791 RID: 14225 RVA: 0x000F7A07 File Offset: 0x000F5C07
		internal Screen(IntPtr monitor)
			: this(monitor, IntPtr.Zero)
		{
		}

		// Token: 0x06003792 RID: 14226 RVA: 0x000F7A18 File Offset: 0x000F5C18
		internal Screen(IntPtr monitor, IntPtr hdc)
		{
			IntPtr intPtr = hdc;
			if (!Screen.multiMonitorSupport || monitor == (IntPtr)(-1163005939))
			{
				this.bounds = SystemInformation.VirtualScreen;
				this.primary = true;
				this.deviceName = "DISPLAY";
			}
			else
			{
				NativeMethods.MONITORINFOEX monitorinfoex = new NativeMethods.MONITORINFOEX();
				SafeNativeMethods.GetMonitorInfo(new HandleRef(null, monitor), monitorinfoex);
				this.bounds = Rectangle.FromLTRB(monitorinfoex.rcMonitor.left, monitorinfoex.rcMonitor.top, monitorinfoex.rcMonitor.right, monitorinfoex.rcMonitor.bottom);
				this.primary = (monitorinfoex.dwFlags & 1) != 0;
				this.deviceName = new string(monitorinfoex.szDevice);
				this.deviceName = this.deviceName.TrimEnd(new char[1]);
				if (hdc == IntPtr.Zero)
				{
					intPtr = UnsafeNativeMethods.CreateDC(this.deviceName);
				}
			}
			this.hmonitor = monitor;
			this.bitDepth = UnsafeNativeMethods.GetDeviceCaps(new HandleRef(null, intPtr), 12);
			this.bitDepth *= UnsafeNativeMethods.GetDeviceCaps(new HandleRef(null, intPtr), 14);
			if (hdc != intPtr)
			{
				UnsafeNativeMethods.DeleteDC(new HandleRef(null, intPtr));
			}
		}

		/// <summary>Gets an array of all displays on the system.</summary>
		/// <returns>An array of type <see cref="T:System.Windows.Forms.Screen" />, containing all displays on the system.</returns>
		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x06003793 RID: 14227 RVA: 0x000F7B64 File Offset: 0x000F5D64
		public static Screen[] AllScreens
		{
			get
			{
				if (Screen.screens == null)
				{
					if (Screen.multiMonitorSupport)
					{
						Screen.MonitorEnumCallback monitorEnumCallback = new Screen.MonitorEnumCallback();
						NativeMethods.MonitorEnumProc monitorEnumProc = new NativeMethods.MonitorEnumProc(monitorEnumCallback.Callback);
						SafeNativeMethods.EnumDisplayMonitors(NativeMethods.NullHandleRef, null, monitorEnumProc, IntPtr.Zero);
						if (monitorEnumCallback.screens.Count > 0)
						{
							Screen[] array = new Screen[monitorEnumCallback.screens.Count];
							monitorEnumCallback.screens.CopyTo(array, 0);
							Screen.screens = array;
						}
						else
						{
							Screen.screens = new Screen[]
							{
								new Screen((IntPtr)(-1163005939))
							};
						}
					}
					else
					{
						Screen.screens = new Screen[] { Screen.PrimaryScreen };
					}
					SystemEvents.DisplaySettingsChanging += Screen.OnDisplaySettingsChanging;
				}
				return Screen.screens;
			}
		}

		/// <summary>Gets the number of bits of memory, associated with one pixel of data.</summary>
		/// <returns>The number of bits of memory, associated with one pixel of data</returns>
		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x06003794 RID: 14228 RVA: 0x000F7C24 File Offset: 0x000F5E24
		public int BitsPerPixel
		{
			get
			{
				return this.bitDepth;
			}
		}

		/// <summary>Gets the bounds of the display.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" />, representing the bounds of the display.</returns>
		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x06003795 RID: 14229 RVA: 0x000F7C2C File Offset: 0x000F5E2C
		public Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
		}

		/// <summary>Gets the device name associated with a display.</summary>
		/// <returns>The device name associated with a display.</returns>
		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x06003796 RID: 14230 RVA: 0x000F7C34 File Offset: 0x000F5E34
		public string DeviceName
		{
			get
			{
				return this.deviceName;
			}
		}

		/// <summary>Gets a value indicating whether a particular display is the primary device.</summary>
		/// <returns>
		///   <see langword="true" /> if this display is primary; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x06003797 RID: 14231 RVA: 0x000F7C3C File Offset: 0x000F5E3C
		public bool Primary
		{
			get
			{
				return this.primary;
			}
		}

		/// <summary>Gets the primary display.</summary>
		/// <returns>The primary display.</returns>
		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x06003798 RID: 14232 RVA: 0x000F7C44 File Offset: 0x000F5E44
		public static Screen PrimaryScreen
		{
			get
			{
				if (Screen.multiMonitorSupport)
				{
					Screen[] allScreens = Screen.AllScreens;
					for (int i = 0; i < allScreens.Length; i++)
					{
						if (allScreens[i].primary)
						{
							return allScreens[i];
						}
					}
					return null;
				}
				return new Screen((IntPtr)(-1163005939), IntPtr.Zero);
			}
		}

		/// <summary>Gets the working area of the display. The working area is the desktop area of the display, excluding taskbars, docked windows, and docked tool bars.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" />, representing the working area of the display.</returns>
		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x06003799 RID: 14233 RVA: 0x000F7C90 File Offset: 0x000F5E90
		public Rectangle WorkingArea
		{
			get
			{
				if (this.currentDesktopChangedCount != Screen.DesktopChangedCount)
				{
					Interlocked.Exchange(ref this.currentDesktopChangedCount, Screen.DesktopChangedCount);
					if (!Screen.multiMonitorSupport || this.hmonitor == (IntPtr)(-1163005939))
					{
						this.workingArea = SystemInformation.WorkingArea;
					}
					else
					{
						NativeMethods.MONITORINFOEX monitorinfoex = new NativeMethods.MONITORINFOEX();
						SafeNativeMethods.GetMonitorInfo(new HandleRef(null, this.hmonitor), monitorinfoex);
						this.workingArea = Rectangle.FromLTRB(monitorinfoex.rcWork.left, monitorinfoex.rcWork.top, monitorinfoex.rcWork.right, monitorinfoex.rcWork.bottom);
					}
				}
				return this.workingArea;
			}
		}

		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x0600379A RID: 14234 RVA: 0x000F7D40 File Offset: 0x000F5F40
		private static int DesktopChangedCount
		{
			get
			{
				if (Screen.desktopChangedCount == -1)
				{
					object obj = Screen.syncLock;
					lock (obj)
					{
						if (Screen.desktopChangedCount == -1)
						{
							SystemEvents.UserPreferenceChanged += Screen.OnUserPreferenceChanged;
							Screen.desktopChangedCount = 0;
						}
					}
				}
				return Screen.desktopChangedCount;
			}
		}

		/// <summary>Gets or sets a value indicating whether the specified object is equal to this <see langword="Screen" />.</summary>
		/// <param name="obj">The object to compare to this <see cref="T:System.Windows.Forms.Screen" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified object is equal to this <see cref="T:System.Windows.Forms.Screen" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600379B RID: 14235 RVA: 0x000F7DA8 File Offset: 0x000F5FA8
		public override bool Equals(object obj)
		{
			if (obj is Screen)
			{
				Screen screen = (Screen)obj;
				if (this.hmonitor == screen.hmonitor)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Retrieves a <see cref="T:System.Windows.Forms.Screen" /> for the display that contains the specified point.</summary>
		/// <param name="point">A <see cref="T:System.Drawing.Point" /> that specifies the location for which to retrieve a <see cref="T:System.Windows.Forms.Screen" />.</param>
		/// <returns>A <see cref="T:System.Windows.Forms.Screen" /> for the display that contains the point. In multiple display environments where no display contains the point, the display closest to the specified point is returned.</returns>
		// Token: 0x0600379C RID: 14236 RVA: 0x000F7DDC File Offset: 0x000F5FDC
		public static Screen FromPoint(Point point)
		{
			if (Screen.multiMonitorSupport)
			{
				NativeMethods.POINTSTRUCT pointstruct = new NativeMethods.POINTSTRUCT(point.X, point.Y);
				return new Screen(SafeNativeMethods.MonitorFromPoint(pointstruct, 2));
			}
			return new Screen((IntPtr)(-1163005939));
		}

		/// <summary>Retrieves a <see cref="T:System.Windows.Forms.Screen" /> for the display that contains the largest portion of the rectangle.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> that specifies the area for which to retrieve the display.</param>
		/// <returns>A <see cref="T:System.Windows.Forms.Screen" /> for the display that contains the largest region of the specified rectangle. In multiple display environments where no display contains the rectangle, the display closest to the rectangle is returned.</returns>
		// Token: 0x0600379D RID: 14237 RVA: 0x000F7E24 File Offset: 0x000F6024
		public static Screen FromRectangle(Rectangle rect)
		{
			if (Screen.multiMonitorSupport)
			{
				NativeMethods.RECT rect2 = NativeMethods.RECT.FromXYWH(rect.X, rect.Y, rect.Width, rect.Height);
				return new Screen(SafeNativeMethods.MonitorFromRect(ref rect2, 2));
			}
			return new Screen((IntPtr)(-1163005939), IntPtr.Zero);
		}

		/// <summary>Retrieves a <see cref="T:System.Windows.Forms.Screen" /> for the display that contains the largest portion of the specified control.</summary>
		/// <param name="control">A <see cref="T:System.Windows.Forms.Control" /> for which to retrieve a <see cref="T:System.Windows.Forms.Screen" />.</param>
		/// <returns>A <see cref="T:System.Windows.Forms.Screen" /> for the display that contains the largest region of the specified control. In multiple display environments where no display contains the control, the display closest to the specified control is returned.</returns>
		// Token: 0x0600379E RID: 14238 RVA: 0x000F7E7C File Offset: 0x000F607C
		public static Screen FromControl(Control control)
		{
			return Screen.FromHandleInternal(control.Handle);
		}

		/// <summary>Retrieves a <see cref="T:System.Windows.Forms.Screen" /> for the display that contains the largest portion of the object referred to by the specified handle.</summary>
		/// <param name="hwnd">The window handle for which to retrieve the <see cref="T:System.Windows.Forms.Screen" />.</param>
		/// <returns>A <see cref="T:System.Windows.Forms.Screen" /> for the display that contains the largest region of the object. In multiple display environments where no display contains any portion of the specified window, the display closest to the object is returned.</returns>
		// Token: 0x0600379F RID: 14239 RVA: 0x000F7E89 File Offset: 0x000F6089
		public static Screen FromHandle(IntPtr hwnd)
		{
			IntSecurity.ObjectFromWin32Handle.Demand();
			return Screen.FromHandleInternal(hwnd);
		}

		// Token: 0x060037A0 RID: 14240 RVA: 0x000F7E9B File Offset: 0x000F609B
		internal static Screen FromHandleInternal(IntPtr hwnd)
		{
			if (Screen.multiMonitorSupport)
			{
				return new Screen(SafeNativeMethods.MonitorFromWindow(new HandleRef(null, hwnd), 2));
			}
			return new Screen((IntPtr)(-1163005939), IntPtr.Zero);
		}

		/// <summary>Retrieves the working area closest to the specified point. The working area is the desktop area of the display, excluding taskbars, docked windows, and docked tool bars.</summary>
		/// <param name="pt">A <see cref="T:System.Drawing.Point" /> that specifies the coordinates for which to retrieve the working area.</param>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that specifies the working area. In multiple display environments where no display contains the specified point, the display closest to the point is returned.</returns>
		// Token: 0x060037A1 RID: 14241 RVA: 0x000F7ECB File Offset: 0x000F60CB
		public static Rectangle GetWorkingArea(Point pt)
		{
			return Screen.FromPoint(pt).WorkingArea;
		}

		/// <summary>Retrieves the working area for the display that contains the largest portion of the specified rectangle. The working area is the desktop area of the display, excluding taskbars, docked windows, and docked tool bars.</summary>
		/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> that specifies the area for which to retrieve the working area.</param>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that specifies the working area. In multiple display environments where no display contains the specified rectangle, the display closest to the rectangle is returned.</returns>
		// Token: 0x060037A2 RID: 14242 RVA: 0x000F7ED8 File Offset: 0x000F60D8
		public static Rectangle GetWorkingArea(Rectangle rect)
		{
			return Screen.FromRectangle(rect).WorkingArea;
		}

		/// <summary>Retrieves the working area for the display that contains the largest region of the specified control. The working area is the desktop area of the display, excluding taskbars, docked windows, and docked tool bars.</summary>
		/// <param name="ctl">The <see cref="T:System.Windows.Forms.Control" /> for which to retrieve the working area.</param>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that specifies the working area. In multiple display environments where no display contains the specified control, the display closest to the control is returned.</returns>
		// Token: 0x060037A3 RID: 14243 RVA: 0x000F7EE5 File Offset: 0x000F60E5
		public static Rectangle GetWorkingArea(Control ctl)
		{
			return Screen.FromControl(ctl).WorkingArea;
		}

		/// <summary>Retrieves the bounds of the display that contains the specified point.</summary>
		/// <param name="pt">A <see cref="T:System.Drawing.Point" /> that specifies the coordinates for which to retrieve the display bounds.</param>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the display that contains the specified point. In multiple display environments where no display contains the specified point, the display closest to the point is returned.</returns>
		// Token: 0x060037A4 RID: 14244 RVA: 0x000F7EF2 File Offset: 0x000F60F2
		public static Rectangle GetBounds(Point pt)
		{
			return Screen.FromPoint(pt).Bounds;
		}

		/// <summary>Retrieves the bounds of the display that contains the largest portion of the specified rectangle.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> that specifies the area for which to retrieve the display bounds.</param>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the display that contains the specified rectangle. In multiple display environments where no monitor contains the specified rectangle, the monitor closest to the rectangle is returned.</returns>
		// Token: 0x060037A5 RID: 14245 RVA: 0x000F7EFF File Offset: 0x000F60FF
		public static Rectangle GetBounds(Rectangle rect)
		{
			return Screen.FromRectangle(rect).Bounds;
		}

		/// <summary>Retrieves the bounds of the display that contains the largest portion of the specified control.</summary>
		/// <param name="ctl">The <see cref="T:System.Windows.Forms.Control" /> for which to retrieve the display bounds.</param>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the display that contains the specified control. In multiple display environments where no display contains the specified control, the display closest to the control is returned.</returns>
		// Token: 0x060037A6 RID: 14246 RVA: 0x000F7F0C File Offset: 0x000F610C
		public static Rectangle GetBounds(Control ctl)
		{
			return Screen.FromControl(ctl).Bounds;
		}

		/// <summary>Computes and retrieves a hash code for an object.</summary>
		/// <returns>A hash code for an object.</returns>
		// Token: 0x060037A7 RID: 14247 RVA: 0x000F7F19 File Offset: 0x000F6119
		public override int GetHashCode()
		{
			return (int)this.hmonitor;
		}

		// Token: 0x060037A8 RID: 14248 RVA: 0x000F7F26 File Offset: 0x000F6126
		private static void OnDisplaySettingsChanging(object sender, EventArgs e)
		{
			SystemEvents.DisplaySettingsChanging -= Screen.OnDisplaySettingsChanging;
			Screen.screens = null;
		}

		// Token: 0x060037A9 RID: 14249 RVA: 0x000F7F3F File Offset: 0x000F613F
		private static void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
		{
			if (e.Category == UserPreferenceCategory.Desktop)
			{
				Interlocked.Increment(ref Screen.desktopChangedCount);
			}
		}

		/// <summary>Retrieves a string representing this object.</summary>
		/// <returns>A string representation of the object.</returns>
		// Token: 0x060037AA RID: 14250 RVA: 0x000F7F58 File Offset: 0x000F6158
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				base.GetType().Name,
				"[Bounds=",
				this.bounds.ToString(),
				" WorkingArea=",
				this.WorkingArea.ToString(),
				" Primary=",
				this.primary.ToString(),
				" DeviceName=",
				this.deviceName
			});
		}

		// Token: 0x04002152 RID: 8530
		private readonly IntPtr hmonitor;

		// Token: 0x04002153 RID: 8531
		private readonly Rectangle bounds;

		// Token: 0x04002154 RID: 8532
		private Rectangle workingArea = Rectangle.Empty;

		// Token: 0x04002155 RID: 8533
		private readonly bool primary;

		// Token: 0x04002156 RID: 8534
		private readonly string deviceName;

		// Token: 0x04002157 RID: 8535
		private readonly int bitDepth;

		// Token: 0x04002158 RID: 8536
		private static object syncLock = new object();

		// Token: 0x04002159 RID: 8537
		private static int desktopChangedCount = -1;

		// Token: 0x0400215A RID: 8538
		private int currentDesktopChangedCount = -1;

		// Token: 0x0400215B RID: 8539
		private const int PRIMARY_MONITOR = -1163005939;

		// Token: 0x0400215C RID: 8540
		private const int MONITOR_DEFAULTTONULL = 0;

		// Token: 0x0400215D RID: 8541
		private const int MONITOR_DEFAULTTOPRIMARY = 1;

		// Token: 0x0400215E RID: 8542
		private const int MONITOR_DEFAULTTONEAREST = 2;

		// Token: 0x0400215F RID: 8543
		private const int MONITORINFOF_PRIMARY = 1;

		// Token: 0x04002160 RID: 8544
		private static bool multiMonitorSupport = UnsafeNativeMethods.GetSystemMetrics(80) != 0;

		// Token: 0x04002161 RID: 8545
		private static Screen[] screens;

		// Token: 0x020007DC RID: 2012
		private class MonitorEnumCallback
		{
			// Token: 0x06006DA6 RID: 28070 RVA: 0x00191D85 File Offset: 0x0018FF85
			public virtual bool Callback(IntPtr monitor, IntPtr hdc, IntPtr lprcMonitor, IntPtr lparam)
			{
				this.screens.Add(new Screen(monitor, hdc));
				return true;
			}

			// Token: 0x040042AE RID: 17070
			public ArrayList screens = new ArrayList();
		}
	}
}
