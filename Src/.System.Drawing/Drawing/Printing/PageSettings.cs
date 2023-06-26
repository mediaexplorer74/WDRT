using System;
using System.Drawing.Internal;
using System.Runtime.InteropServices;

namespace System.Drawing.Printing
{
	/// <summary>Specifies settings that apply to a single, printed page.</summary>
	// Token: 0x02000058 RID: 88
	[Serializable]
	public class PageSettings : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PageSettings" /> class using the default printer.</summary>
		// Token: 0x0600072C RID: 1836 RVA: 0x0001D208 File Offset: 0x0001B408
		public PageSettings()
			: this(new PrinterSettings())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PageSettings" /> class using a specified printer.</summary>
		/// <param name="printerSettings">The <see cref="T:System.Drawing.Printing.PrinterSettings" /> that describes the printer to use.</param>
		// Token: 0x0600072D RID: 1837 RVA: 0x0001D215 File Offset: 0x0001B415
		public PageSettings(PrinterSettings printerSettings)
		{
			this.printerSettings = printerSettings;
		}

		/// <summary>Gets the size of the page, taking into account the page orientation specified by the <see cref="P:System.Drawing.Printing.PageSettings.Landscape" /> property.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the length and width, in hundredths of an inch, of the page.</returns>
		/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist.</exception>
		// Token: 0x170002CF RID: 719
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x0001D248 File Offset: 0x0001B448
		public Rectangle Bounds
		{
			get
			{
				IntSecurity.AllPrintingAndUnmanagedCode.Assert();
				IntPtr hdevmode = this.printerSettings.GetHdevmode();
				Rectangle bounds = this.GetBounds(hdevmode);
				SafeNativeMethods.GlobalFree(new HandleRef(this, hdevmode));
				return bounds;
			}
		}

		/// <summary>Gets or sets a value indicating whether the page should be printed in color.</summary>
		/// <returns>
		///   <see langword="true" /> if the page should be printed in color; otherwise, <see langword="false" />. The default is determined by the printer.</returns>
		/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist.</exception>
		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x0001D281 File Offset: 0x0001B481
		// (set) Token: 0x06000730 RID: 1840 RVA: 0x0001D2AC File Offset: 0x0001B4AC
		public bool Color
		{
			get
			{
				if (this.color.IsDefault)
				{
					return this.printerSettings.GetModeField(ModeField.Color, 1) == 2;
				}
				return (bool)this.color;
			}
			set
			{
				this.color = value;
			}
		}

		/// <summary>Gets the x-coordinate, in hundredths of an inch, of the hard margin at the left of the page.</summary>
		/// <returns>The x-coordinate, in hundredths of an inch, of the left-hand hard margin.</returns>
		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x0001D2BC File Offset: 0x0001B4BC
		public float HardMarginX
		{
			get
			{
				IntSecurity.AllPrintingAndUnmanagedCode.Assert();
				float num = 0f;
				DeviceContext deviceContext = this.printerSettings.CreateDeviceContext(this);
				try
				{
					int deviceCaps = UnsafeNativeMethods.GetDeviceCaps(new HandleRef(deviceContext, deviceContext.Hdc), 88);
					int deviceCaps2 = UnsafeNativeMethods.GetDeviceCaps(new HandleRef(deviceContext, deviceContext.Hdc), 112);
					num = (float)(deviceCaps2 * 100 / deviceCaps);
				}
				finally
				{
					deviceContext.Dispose();
				}
				return num;
			}
		}

		/// <summary>Gets the y-coordinate, in hundredths of an inch, of the hard margin at the top of the page.</summary>
		/// <returns>The y-coordinate, in hundredths of an inch, of the hard margin at the top of the page.</returns>
		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x0001D330 File Offset: 0x0001B530
		public float HardMarginY
		{
			get
			{
				float num = 0f;
				DeviceContext deviceContext = this.printerSettings.CreateDeviceContext(this);
				try
				{
					int deviceCaps = UnsafeNativeMethods.GetDeviceCaps(new HandleRef(deviceContext, deviceContext.Hdc), 90);
					int deviceCaps2 = UnsafeNativeMethods.GetDeviceCaps(new HandleRef(deviceContext, deviceContext.Hdc), 113);
					num = (float)(deviceCaps2 * 100 / deviceCaps);
				}
				finally
				{
					deviceContext.Dispose();
				}
				return num;
			}
		}

		/// <summary>Gets or sets a value indicating whether the page is printed in landscape or portrait orientation.</summary>
		/// <returns>
		///   <see langword="true" /> if the page should be printed in landscape orientation; otherwise, <see langword="false" />. The default is determined by the printer.</returns>
		/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist.</exception>
		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x0001D39C File Offset: 0x0001B59C
		// (set) Token: 0x06000734 RID: 1844 RVA: 0x0001D3C7 File Offset: 0x0001B5C7
		public bool Landscape
		{
			get
			{
				if (this.landscape.IsDefault)
				{
					return this.printerSettings.GetModeField(ModeField.Orientation, 1) == 2;
				}
				return (bool)this.landscape;
			}
			set
			{
				this.landscape = value;
			}
		}

		/// <summary>Gets or sets the margins for this page.</summary>
		/// <returns>A <see cref="T:System.Drawing.Printing.Margins" /> that represents the margins, in hundredths of an inch, for the page. The default is 1-inch margins on all sides.</returns>
		/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist.</exception>
		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x0001D3D5 File Offset: 0x0001B5D5
		// (set) Token: 0x06000736 RID: 1846 RVA: 0x0001D3DD File Offset: 0x0001B5DD
		public Margins Margins
		{
			get
			{
				return this.margins;
			}
			set
			{
				this.margins = value;
			}
		}

		/// <summary>Gets or sets the paper size for the page.</summary>
		/// <returns>A <see cref="T:System.Drawing.Printing.PaperSize" /> that represents the size of the paper. The default is the printer's default paper size.</returns>
		/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist or there is no default printer installed.</exception>
		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x0001D3E6 File Offset: 0x0001B5E6
		// (set) Token: 0x06000738 RID: 1848 RVA: 0x0001D3FD File Offset: 0x0001B5FD
		public PaperSize PaperSize
		{
			get
			{
				IntSecurity.AllPrintingAndUnmanagedCode.Assert();
				return this.GetPaperSize(IntPtr.Zero);
			}
			set
			{
				this.paperSize = value;
			}
		}

		/// <summary>Gets or sets the page's paper source; for example, the printer's upper tray.</summary>
		/// <returns>A <see cref="T:System.Drawing.Printing.PaperSource" /> that specifies the source of the paper. The default is the printer's default paper source.</returns>
		/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist or there is no default printer installed.</exception>
		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x0001D408 File Offset: 0x0001B608
		// (set) Token: 0x0600073A RID: 1850 RVA: 0x0001D480 File Offset: 0x0001B680
		public PaperSource PaperSource
		{
			get
			{
				if (this.paperSource == null)
				{
					IntSecurity.AllPrintingAndUnmanagedCode.Assert();
					IntPtr hdevmode = this.printerSettings.GetHdevmode();
					IntPtr intPtr = SafeNativeMethods.GlobalLock(new HandleRef(this, hdevmode));
					SafeNativeMethods.DEVMODE devmode = (SafeNativeMethods.DEVMODE)UnsafeNativeMethods.PtrToStructure(intPtr, typeof(SafeNativeMethods.DEVMODE));
					PaperSource paperSource = this.PaperSourceFromMode(devmode);
					SafeNativeMethods.GlobalUnlock(new HandleRef(this, hdevmode));
					SafeNativeMethods.GlobalFree(new HandleRef(this, hdevmode));
					return paperSource;
				}
				return this.paperSource;
			}
			set
			{
				this.paperSource = value;
			}
		}

		/// <summary>Gets the bounds of the printable area of the page for the printer.</summary>
		/// <returns>A <see cref="T:System.Drawing.RectangleF" /> representing the length and width, in hundredths of an inch, of the area the printer is capable of printing in.</returns>
		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x0001D48C File Offset: 0x0001B68C
		public RectangleF PrintableArea
		{
			get
			{
				RectangleF rectangleF = default(RectangleF);
				DeviceContext deviceContext = this.printerSettings.CreateInformationContext(this);
				HandleRef handleRef = new HandleRef(deviceContext, deviceContext.Hdc);
				try
				{
					int deviceCaps = UnsafeNativeMethods.GetDeviceCaps(handleRef, 88);
					int deviceCaps2 = UnsafeNativeMethods.GetDeviceCaps(handleRef, 90);
					if (!this.Landscape)
					{
						rectangleF.X = (float)UnsafeNativeMethods.GetDeviceCaps(handleRef, 112) * 100f / (float)deviceCaps;
						rectangleF.Y = (float)UnsafeNativeMethods.GetDeviceCaps(handleRef, 113) * 100f / (float)deviceCaps2;
						rectangleF.Width = (float)UnsafeNativeMethods.GetDeviceCaps(handleRef, 8) * 100f / (float)deviceCaps;
						rectangleF.Height = (float)UnsafeNativeMethods.GetDeviceCaps(handleRef, 10) * 100f / (float)deviceCaps2;
					}
					else
					{
						rectangleF.Y = (float)UnsafeNativeMethods.GetDeviceCaps(handleRef, 112) * 100f / (float)deviceCaps;
						rectangleF.X = (float)UnsafeNativeMethods.GetDeviceCaps(handleRef, 113) * 100f / (float)deviceCaps2;
						rectangleF.Height = (float)UnsafeNativeMethods.GetDeviceCaps(handleRef, 8) * 100f / (float)deviceCaps;
						rectangleF.Width = (float)UnsafeNativeMethods.GetDeviceCaps(handleRef, 10) * 100f / (float)deviceCaps2;
					}
				}
				finally
				{
					deviceContext.Dispose();
				}
				return rectangleF;
			}
		}

		/// <summary>Gets or sets the printer resolution for the page.</summary>
		/// <returns>A <see cref="T:System.Drawing.Printing.PrinterResolution" /> that specifies the printer resolution for the page. The default is the printer's default resolution.</returns>
		/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist or there is no default printer installed.</exception>
		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x0001D5C0 File Offset: 0x0001B7C0
		// (set) Token: 0x0600073D RID: 1853 RVA: 0x0001D638 File Offset: 0x0001B838
		public PrinterResolution PrinterResolution
		{
			get
			{
				if (this.printerResolution == null)
				{
					IntSecurity.AllPrintingAndUnmanagedCode.Assert();
					IntPtr hdevmode = this.printerSettings.GetHdevmode();
					IntPtr intPtr = SafeNativeMethods.GlobalLock(new HandleRef(this, hdevmode));
					SafeNativeMethods.DEVMODE devmode = (SafeNativeMethods.DEVMODE)UnsafeNativeMethods.PtrToStructure(intPtr, typeof(SafeNativeMethods.DEVMODE));
					PrinterResolution printerResolution = this.PrinterResolutionFromMode(devmode);
					SafeNativeMethods.GlobalUnlock(new HandleRef(this, hdevmode));
					SafeNativeMethods.GlobalFree(new HandleRef(this, hdevmode));
					return printerResolution;
				}
				return this.printerResolution;
			}
			set
			{
				this.printerResolution = value;
			}
		}

		/// <summary>Gets or sets the printer settings associated with the page.</summary>
		/// <returns>A <see cref="T:System.Drawing.Printing.PrinterSettings" /> that represents the printer settings associated with the page.</returns>
		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x0001D641 File Offset: 0x0001B841
		// (set) Token: 0x0600073F RID: 1855 RVA: 0x0001D649 File Offset: 0x0001B849
		public PrinterSettings PrinterSettings
		{
			get
			{
				return this.printerSettings;
			}
			set
			{
				if (value == null)
				{
					value = new PrinterSettings();
				}
				this.printerSettings = value;
			}
		}

		/// <summary>Creates a copy of this <see cref="T:System.Drawing.Printing.PageSettings" />.</summary>
		/// <returns>A copy of this object.</returns>
		// Token: 0x06000740 RID: 1856 RVA: 0x0001D65C File Offset: 0x0001B85C
		public object Clone()
		{
			PageSettings pageSettings = (PageSettings)base.MemberwiseClone();
			pageSettings.margins = (Margins)this.margins.Clone();
			return pageSettings;
		}

		/// <summary>Copies the relevant information from the <see cref="T:System.Drawing.Printing.PageSettings" /> to the specified <see langword="DEVMODE" /> structure.</summary>
		/// <param name="hdevmode">The handle to a Win32 <see langword="DEVMODE" /> structure.</param>
		/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist or there is no default printer installed.</exception>
		// Token: 0x06000741 RID: 1857 RVA: 0x0001D68C File Offset: 0x0001B88C
		public void CopyToHdevmode(IntPtr hdevmode)
		{
			IntSecurity.AllPrintingAndUnmanagedCode.Demand();
			IntPtr intPtr = SafeNativeMethods.GlobalLock(new HandleRef(null, hdevmode));
			SafeNativeMethods.DEVMODE devmode = (SafeNativeMethods.DEVMODE)UnsafeNativeMethods.PtrToStructure(intPtr, typeof(SafeNativeMethods.DEVMODE));
			if (this.color.IsNotDefault && (devmode.dmFields & 2048) == 2048)
			{
				devmode.dmColor = (((bool)this.color) ? 2 : 1);
			}
			if (this.landscape.IsNotDefault && (devmode.dmFields & 1) == 1)
			{
				devmode.dmOrientation = (((bool)this.landscape) ? 2 : 1);
			}
			if (this.paperSize != null)
			{
				if ((devmode.dmFields & 2) == 2)
				{
					devmode.dmPaperSize = (short)this.paperSize.RawKind;
				}
				bool flag = false;
				bool flag2 = false;
				if ((devmode.dmFields & 4) == 4)
				{
					int num = PrinterUnitConvert.Convert(this.paperSize.Height, PrinterUnit.Display, PrinterUnit.TenthsOfAMillimeter);
					devmode.dmPaperLength = (short)num;
					flag2 = true;
				}
				if ((devmode.dmFields & 8) == 8)
				{
					int num2 = PrinterUnitConvert.Convert(this.paperSize.Width, PrinterUnit.Display, PrinterUnit.TenthsOfAMillimeter);
					devmode.dmPaperWidth = (short)num2;
					flag = true;
				}
				if (this.paperSize.Kind == PaperKind.Custom)
				{
					if (!flag2)
					{
						devmode.dmFields |= 4;
						int num3 = PrinterUnitConvert.Convert(this.paperSize.Height, PrinterUnit.Display, PrinterUnit.TenthsOfAMillimeter);
						devmode.dmPaperLength = (short)num3;
					}
					if (!flag)
					{
						devmode.dmFields |= 8;
						int num4 = PrinterUnitConvert.Convert(this.paperSize.Width, PrinterUnit.Display, PrinterUnit.TenthsOfAMillimeter);
						devmode.dmPaperWidth = (short)num4;
					}
				}
			}
			if (this.paperSource != null && (devmode.dmFields & 512) == 512)
			{
				devmode.dmDefaultSource = (short)this.paperSource.RawKind;
			}
			if (this.printerResolution != null)
			{
				if (this.printerResolution.Kind == PrinterResolutionKind.Custom)
				{
					if ((devmode.dmFields & 1024) == 1024)
					{
						devmode.dmPrintQuality = (short)this.printerResolution.X;
					}
					if ((devmode.dmFields & 8192) == 8192)
					{
						devmode.dmYResolution = (short)this.printerResolution.Y;
					}
				}
				else if ((devmode.dmFields & 1024) == 1024)
				{
					devmode.dmPrintQuality = (short)this.printerResolution.Kind;
				}
			}
			Marshal.StructureToPtr(devmode, intPtr, false);
			if (devmode.dmDriverExtra >= this.ExtraBytes)
			{
				int num5 = SafeNativeMethods.DocumentProperties(NativeMethods.NullHandleRef, NativeMethods.NullHandleRef, this.printerSettings.PrinterName, intPtr, intPtr, 10);
				if (num5 < 0 && LocalAppContextSwitches.FreeCopyToDevModeOnFailure)
				{
					SafeNativeMethods.GlobalFree(new HandleRef(null, intPtr));
				}
			}
			SafeNativeMethods.GlobalUnlock(new HandleRef(null, hdevmode));
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x0001D928 File Offset: 0x0001BB28
		private short ExtraBytes
		{
			get
			{
				IntPtr hdevmodeInternal = this.printerSettings.GetHdevmodeInternal();
				IntPtr intPtr = SafeNativeMethods.GlobalLock(new HandleRef(this, hdevmodeInternal));
				SafeNativeMethods.DEVMODE devmode = (SafeNativeMethods.DEVMODE)UnsafeNativeMethods.PtrToStructure(intPtr, typeof(SafeNativeMethods.DEVMODE));
				short dmDriverExtra = devmode.dmDriverExtra;
				SafeNativeMethods.GlobalUnlock(new HandleRef(this, hdevmodeInternal));
				SafeNativeMethods.GlobalFree(new HandleRef(this, hdevmodeInternal));
				return dmDriverExtra;
			}
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0001D988 File Offset: 0x0001BB88
		internal Rectangle GetBounds(IntPtr modeHandle)
		{
			PaperSize paperSize = this.GetPaperSize(modeHandle);
			Rectangle rectangle;
			if (this.GetLandscape(modeHandle))
			{
				rectangle = new Rectangle(0, 0, paperSize.Height, paperSize.Width);
			}
			else
			{
				rectangle = new Rectangle(0, 0, paperSize.Width, paperSize.Height);
			}
			return rectangle;
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0001D9D3 File Offset: 0x0001BBD3
		private bool GetLandscape(IntPtr modeHandle)
		{
			if (this.landscape.IsDefault)
			{
				return this.printerSettings.GetModeField(ModeField.Orientation, 1, modeHandle) == 2;
			}
			return (bool)this.landscape;
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0001DA00 File Offset: 0x0001BC00
		private PaperSize GetPaperSize(IntPtr modeHandle)
		{
			if (this.paperSize == null)
			{
				bool flag = false;
				if (modeHandle == IntPtr.Zero)
				{
					modeHandle = this.printerSettings.GetHdevmode();
					flag = true;
				}
				IntPtr intPtr = SafeNativeMethods.GlobalLock(new HandleRef(null, modeHandle));
				SafeNativeMethods.DEVMODE devmode = (SafeNativeMethods.DEVMODE)UnsafeNativeMethods.PtrToStructure(intPtr, typeof(SafeNativeMethods.DEVMODE));
				PaperSize paperSize = this.PaperSizeFromMode(devmode);
				SafeNativeMethods.GlobalUnlock(new HandleRef(null, modeHandle));
				if (flag)
				{
					SafeNativeMethods.GlobalFree(new HandleRef(null, modeHandle));
				}
				return paperSize;
			}
			return this.paperSize;
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0001DA84 File Offset: 0x0001BC84
		private PaperSize PaperSizeFromMode(SafeNativeMethods.DEVMODE mode)
		{
			PaperSize[] paperSizes = this.printerSettings.Get_PaperSizes();
			if ((mode.dmFields & 2) == 2)
			{
				for (int i = 0; i < paperSizes.Length; i++)
				{
					if (paperSizes[i].RawKind == (int)mode.dmPaperSize)
					{
						return paperSizes[i];
					}
				}
			}
			return new PaperSize(PaperKind.Custom, "custom", PrinterUnitConvert.Convert((int)mode.dmPaperWidth, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert((int)mode.dmPaperLength, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0001DAF0 File Offset: 0x0001BCF0
		private PaperSource PaperSourceFromMode(SafeNativeMethods.DEVMODE mode)
		{
			PaperSource[] paperSources = this.printerSettings.Get_PaperSources();
			if ((mode.dmFields & 512) == 512)
			{
				for (int i = 0; i < paperSources.Length; i++)
				{
					if ((short)paperSources[i].RawKind == mode.dmDefaultSource)
					{
						return paperSources[i];
					}
				}
			}
			return new PaperSource((PaperSourceKind)mode.dmDefaultSource, "unknown");
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0001DB50 File Offset: 0x0001BD50
		private PrinterResolution PrinterResolutionFromMode(SafeNativeMethods.DEVMODE mode)
		{
			PrinterResolution[] printerResolutions = this.printerSettings.Get_PrinterResolutions();
			for (int i = 0; i < printerResolutions.Length; i++)
			{
				if (mode.dmPrintQuality >= 0 && (mode.dmFields & 1024) == 1024 && (mode.dmFields & 8192) == 8192)
				{
					if (printerResolutions[i].X == (int)mode.dmPrintQuality && printerResolutions[i].Y == (int)mode.dmYResolution)
					{
						return printerResolutions[i];
					}
				}
				else if ((mode.dmFields & 1024) == 1024 && printerResolutions[i].Kind == (PrinterResolutionKind)mode.dmPrintQuality)
				{
					return printerResolutions[i];
				}
			}
			return new PrinterResolution(PrinterResolutionKind.Custom, (int)mode.dmPrintQuality, (int)mode.dmYResolution);
		}

		/// <summary>Copies relevant information to the <see cref="T:System.Drawing.Printing.PageSettings" /> from the specified <see langword="DEVMODE" /> structure.</summary>
		/// <param name="hdevmode">The handle to a Win32 <see langword="DEVMODE" /> structure.</param>
		/// <exception cref="T:System.ArgumentException">The printer handle is not valid.</exception>
		/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist or there is no default printer installed.</exception>
		// Token: 0x06000749 RID: 1865 RVA: 0x0001DC08 File Offset: 0x0001BE08
		public void SetHdevmode(IntPtr hdevmode)
		{
			IntSecurity.AllPrintingAndUnmanagedCode.Demand();
			if (hdevmode == IntPtr.Zero)
			{
				throw new ArgumentException(SR.GetString("InvalidPrinterHandle", new object[] { hdevmode }));
			}
			IntPtr intPtr = SafeNativeMethods.GlobalLock(new HandleRef(null, hdevmode));
			SafeNativeMethods.DEVMODE devmode = (SafeNativeMethods.DEVMODE)UnsafeNativeMethods.PtrToStructure(intPtr, typeof(SafeNativeMethods.DEVMODE));
			if ((devmode.dmFields & 2048) == 2048)
			{
				this.color = devmode.dmColor == 2;
			}
			if ((devmode.dmFields & 1) == 1)
			{
				this.landscape = devmode.dmOrientation == 2;
			}
			this.paperSize = this.PaperSizeFromMode(devmode);
			this.paperSource = this.PaperSourceFromMode(devmode);
			this.printerResolution = this.PrinterResolutionFromMode(devmode);
			SafeNativeMethods.GlobalUnlock(new HandleRef(null, hdevmode));
		}

		/// <summary>Converts the <see cref="T:System.Drawing.Printing.PageSettings" /> to string form.</summary>
		/// <returns>A string showing the various property settings for the <see cref="T:System.Drawing.Printing.PageSettings" />.</returns>
		// Token: 0x0600074A RID: 1866 RVA: 0x0001DCE8 File Offset: 0x0001BEE8
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"[PageSettings: Color=",
				this.Color.ToString(),
				", Landscape=",
				this.Landscape.ToString(),
				", Margins=",
				this.Margins.ToString(),
				", PaperSize=",
				this.PaperSize.ToString(),
				", PaperSource=",
				this.PaperSource.ToString(),
				", PrinterResolution=",
				this.PrinterResolution.ToString(),
				"]"
			});
		}

		// Token: 0x04000621 RID: 1569
		internal PrinterSettings printerSettings;

		// Token: 0x04000622 RID: 1570
		private TriState color = TriState.Default;

		// Token: 0x04000623 RID: 1571
		private PaperSize paperSize;

		// Token: 0x04000624 RID: 1572
		private PaperSource paperSource;

		// Token: 0x04000625 RID: 1573
		private PrinterResolution printerResolution;

		// Token: 0x04000626 RID: 1574
		private TriState landscape = TriState.Default;

		// Token: 0x04000627 RID: 1575
		private Margins margins = new Margins();
	}
}
