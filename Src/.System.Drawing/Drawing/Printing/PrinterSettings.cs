﻿using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Drawing.Internal;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System.Drawing.Printing
{
	/// <summary>Specifies information about how a document is printed, including the printer that prints it, when printing from a Windows Forms application.</summary>
	// Token: 0x02000064 RID: 100
	[Serializable]
	public class PrinterSettings : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrinterSettings" /> class.</summary>
		// Token: 0x0600079D RID: 1949 RVA: 0x0001EB1C File Offset: 0x0001CD1C
		public PrinterSettings()
		{
			this.defaultPageSettings = new PageSettings(this);
		}

		/// <summary>Gets a value indicating whether the printer supports double-sided printing.</summary>
		/// <returns>
		///   <see langword="true" /> if the printer supports double-sided printing; otherwise, <see langword="false" />.</returns>
		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x0001EB75 File Offset: 0x0001CD75
		public bool CanDuplex
		{
			get
			{
				return this.DeviceCapabilities(7, IntPtr.Zero, 0) == 1;
			}
		}

		/// <summary>Gets or sets the number of copies of the document to print.</summary>
		/// <returns>The number of copies to print. The default is 1.</returns>
		/// <exception cref="T:System.ArgumentException">The value of the <see cref="P:System.Drawing.Printing.PrinterSettings.Copies" /> property is less than zero.</exception>
		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x0600079F RID: 1951 RVA: 0x0001EB87 File Offset: 0x0001CD87
		// (set) Token: 0x060007A0 RID: 1952 RVA: 0x0001EBA4 File Offset: 0x0001CDA4
		public short Copies
		{
			get
			{
				if (this.copies != -1)
				{
					return this.copies;
				}
				return this.GetModeField(ModeField.Copies, 1);
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException(SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"value",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture)
					}));
				}
				IntSecurity.SafePrinting.Demand();
				this.copies = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the printed document is collated.</summary>
		/// <returns>
		///   <see langword="true" /> if the printed document is collated; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x0001EC04 File Offset: 0x0001CE04
		// (set) Token: 0x060007A2 RID: 1954 RVA: 0x0001EC2B File Offset: 0x0001CE2B
		public bool Collate
		{
			get
			{
				if (!this.collate.IsDefault)
				{
					return (bool)this.collate;
				}
				return this.GetModeField(ModeField.Collate, 0) == 1;
			}
			set
			{
				this.collate = value;
			}
		}

		/// <summary>Gets the default page settings for this printer.</summary>
		/// <returns>A <see cref="T:System.Drawing.Printing.PageSettings" /> that represents the default page settings for this printer.</returns>
		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x0001EC39 File Offset: 0x0001CE39
		public PageSettings DefaultPageSettings
		{
			get
			{
				return this.defaultPageSettings;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x0001EC41 File Offset: 0x0001CE41
		internal string DriverName
		{
			get
			{
				return this.driverName;
			}
		}

		/// <summary>Gets or sets the printer setting for double-sided printing.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Printing.Duplex" /> values. The default is determined by the printer.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value of the <see cref="P:System.Drawing.Printing.PrinterSettings.Duplex" /> property is not one of the <see cref="T:System.Drawing.Printing.Duplex" /> values.</exception>
		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x0001EC49 File Offset: 0x0001CE49
		// (set) Token: 0x060007A6 RID: 1958 RVA: 0x0001EC63 File Offset: 0x0001CE63
		public Duplex Duplex
		{
			get
			{
				if (this.duplex != Duplex.Default)
				{
					return this.duplex;
				}
				return (Duplex)this.GetModeField(ModeField.Duplex, 1);
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, -1, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(Duplex));
				}
				this.duplex = value;
			}
		}

		/// <summary>Gets or sets the page number of the first page to print.</summary>
		/// <returns>The page number of the first page to print.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Printing.PrinterSettings.FromPage" /> property's value is less than zero.</exception>
		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x0001EC92 File Offset: 0x0001CE92
		// (set) Token: 0x060007A8 RID: 1960 RVA: 0x0001EC9C File Offset: 0x0001CE9C
		public int FromPage
		{
			get
			{
				return this.fromPage;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException(SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"value",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.fromPage = value;
			}
		}

		/// <summary>Gets the names of all printers installed on the computer.</summary>
		/// <returns>A <see cref="T:System.Drawing.Printing.PrinterSettings.StringCollection" /> that represents the names of all printers installed on the computer.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The available printers could not be enumerated.</exception>
		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x0001ECF4 File Offset: 0x0001CEF4
		public static PrinterSettings.StringCollection InstalledPrinters
		{
			get
			{
				IntSecurity.AllPrinting.Demand();
				int num;
				int num2;
				if (Environment.OSVersion.Platform == PlatformID.Win32NT)
				{
					num = 4;
					if (IntPtr.Size == 8)
					{
						num2 = IntPtr.Size * 2 + Marshal.SizeOf(typeof(int)) + 4;
					}
					else
					{
						num2 = IntPtr.Size * 2 + Marshal.SizeOf(typeof(int));
					}
				}
				else
				{
					num = 5;
					num2 = IntPtr.Size * 2 + Marshal.SizeOf(typeof(int)) * 3;
				}
				IntSecurity.UnmanagedCode.Assert();
				string[] array;
				try
				{
					int num3;
					int num4;
					SafeNativeMethods.EnumPrinters(6, null, num, IntPtr.Zero, 0, out num3, out num4);
					IntPtr intPtr = Marshal.AllocCoTaskMem(num3);
					int num5 = SafeNativeMethods.EnumPrinters(6, null, num, intPtr, num3, out num3, out num4);
					array = new string[num4];
					if (num5 == 0)
					{
						Marshal.FreeCoTaskMem(intPtr);
						throw new Win32Exception();
					}
					for (int i = 0; i < num4; i++)
					{
						checked
						{
							IntPtr intPtr2 = Marshal.ReadIntPtr((IntPtr)((long)intPtr + unchecked((long)(checked(i * num2)))));
							array[i] = Marshal.PtrToStringAuto(intPtr2);
						}
					}
					Marshal.FreeCoTaskMem(intPtr);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				return new PrinterSettings.StringCollection(array);
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property designates the default printer, except when the user explicitly sets <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" />.</summary>
		/// <returns>
		///   <see langword="true" /> if <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> designates the default printer; otherwise, <see langword="false" />.</returns>
		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x0001EE20 File Offset: 0x0001D020
		public bool IsDefaultPrinter
		{
			get
			{
				return this.printerName == null || this.printerName == PrinterSettings.GetDefaultPrinterName();
			}
		}

		/// <summary>Gets a value indicating whether the printer is a plotter.</summary>
		/// <returns>
		///   <see langword="true" /> if the printer is a plotter; <see langword="false" /> if the printer is a raster.</returns>
		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x0001EE3C File Offset: 0x0001D03C
		public bool IsPlotter
		{
			get
			{
				return this.GetDeviceCaps(2, 2) == 0;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property designates a valid printer.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property designates a valid printer; otherwise, <see langword="false" />.</returns>
		// Token: 0x170002FA RID: 762
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x0001EE49 File Offset: 0x0001D049
		public bool IsValid
		{
			get
			{
				return this.DeviceCapabilities(18, IntPtr.Zero, -1) != -1;
			}
		}

		/// <summary>Gets the angle, in degrees, that the portrait orientation is rotated to produce the landscape orientation.</summary>
		/// <returns>The angle, in degrees, that the portrait orientation is rotated to produce the landscape orientation.</returns>
		// Token: 0x170002FB RID: 763
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x0001EE5F File Offset: 0x0001D05F
		public int LandscapeAngle
		{
			get
			{
				return this.DeviceCapabilities(17, IntPtr.Zero, 0);
			}
		}

		/// <summary>Gets the maximum number of copies that the printer enables the user to print at a time.</summary>
		/// <returns>The maximum number of copies that the printer enables the user to print at a time.</returns>
		// Token: 0x170002FC RID: 764
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x0001EE6F File Offset: 0x0001D06F
		public int MaximumCopies
		{
			get
			{
				return this.DeviceCapabilities(18, IntPtr.Zero, 1);
			}
		}

		/// <summary>Gets or sets the maximum <see cref="P:System.Drawing.Printing.PrinterSettings.FromPage" /> or <see cref="P:System.Drawing.Printing.PrinterSettings.ToPage" /> that can be selected in a <see cref="T:System.Windows.Forms.PrintDialog" />.</summary>
		/// <returns>The maximum <see cref="P:System.Drawing.Printing.PrinterSettings.FromPage" /> or <see cref="P:System.Drawing.Printing.PrinterSettings.ToPage" /> that can be selected in a <see cref="T:System.Windows.Forms.PrintDialog" />.</returns>
		/// <exception cref="T:System.ArgumentException">The value of the <see cref="P:System.Drawing.Printing.PrinterSettings.MaximumPage" /> property is less than zero.</exception>
		// Token: 0x170002FD RID: 765
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x0001EE7F File Offset: 0x0001D07F
		// (set) Token: 0x060007B0 RID: 1968 RVA: 0x0001EE88 File Offset: 0x0001D088
		public int MaximumPage
		{
			get
			{
				return this.maxPage;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException(SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"value",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.maxPage = value;
			}
		}

		/// <summary>Gets or sets the minimum <see cref="P:System.Drawing.Printing.PrinterSettings.FromPage" /> or <see cref="P:System.Drawing.Printing.PrinterSettings.ToPage" /> that can be selected in a <see cref="T:System.Windows.Forms.PrintDialog" />.</summary>
		/// <returns>The minimum <see cref="P:System.Drawing.Printing.PrinterSettings.FromPage" /> or <see cref="P:System.Drawing.Printing.PrinterSettings.ToPage" /> that can be selected in a <see cref="T:System.Windows.Forms.PrintDialog" />.</returns>
		/// <exception cref="T:System.ArgumentException">The value of the <see cref="P:System.Drawing.Printing.PrinterSettings.MinimumPage" /> property is less than zero.</exception>
		// Token: 0x170002FE RID: 766
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x0001EEDE File Offset: 0x0001D0DE
		// (set) Token: 0x060007B2 RID: 1970 RVA: 0x0001EEE8 File Offset: 0x0001D0E8
		public int MinimumPage
		{
			get
			{
				return this.minPage;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException(SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"value",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.minPage = value;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x0001EF3E File Offset: 0x0001D13E
		// (set) Token: 0x060007B4 RID: 1972 RVA: 0x0001EF46 File Offset: 0x0001D146
		internal string OutputPort
		{
			get
			{
				return this.outputPort;
			}
			set
			{
				this.outputPort = value;
			}
		}

		/// <summary>Gets or sets the file name, when printing to a file.</summary>
		/// <returns>The file name, when printing to a file.</returns>
		// Token: 0x17000300 RID: 768
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x0001EF50 File Offset: 0x0001D150
		// (set) Token: 0x060007B6 RID: 1974 RVA: 0x0001EF73 File Offset: 0x0001D173
		public string PrintFileName
		{
			get
			{
				string text = this.OutputPort;
				if (!string.IsNullOrEmpty(text))
				{
					IntSecurity.DemandReadFileIO(text);
				}
				return text;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentNullException(value);
				}
				IntSecurity.DemandWriteFileIO(value);
				this.OutputPort = value;
			}
		}

		/// <summary>Gets the paper sizes that are supported by this printer.</summary>
		/// <returns>A <see cref="T:System.Drawing.Printing.PrinterSettings.PaperSizeCollection" /> that represents the paper sizes that are supported by this printer.</returns>
		// Token: 0x17000301 RID: 769
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x0001EF91 File Offset: 0x0001D191
		public PrinterSettings.PaperSizeCollection PaperSizes
		{
			get
			{
				return new PrinterSettings.PaperSizeCollection(this.Get_PaperSizes());
			}
		}

		/// <summary>Gets the paper source trays that are available on the printer.</summary>
		/// <returns>A <see cref="T:System.Drawing.Printing.PrinterSettings.PaperSourceCollection" /> that represents the paper source trays that are available on this printer.</returns>
		// Token: 0x17000302 RID: 770
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x0001EF9E File Offset: 0x0001D19E
		public PrinterSettings.PaperSourceCollection PaperSources
		{
			get
			{
				return new PrinterSettings.PaperSourceCollection(this.Get_PaperSources());
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x0001EFAB File Offset: 0x0001D1AB
		// (set) Token: 0x060007BA RID: 1978 RVA: 0x0001EFB3 File Offset: 0x0001D1B3
		internal bool PrintDialogDisplayed
		{
			get
			{
				return this.printDialogDisplayed;
			}
			set
			{
				IntSecurity.AllPrinting.Demand();
				this.printDialogDisplayed = value;
			}
		}

		/// <summary>Gets or sets the page numbers that the user has specified to be printed.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Printing.PrintRange" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value of the <see cref="P:System.Drawing.Printing.PrinterSettings.PrintRange" /> property is not one of the <see cref="T:System.Drawing.Printing.PrintRange" /> values.</exception>
		// Token: 0x17000304 RID: 772
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x0001EFC6 File Offset: 0x0001D1C6
		// (set) Token: 0x060007BC RID: 1980 RVA: 0x0001EFCE File Offset: 0x0001D1CE
		public PrintRange PrintRange
		{
			get
			{
				return this.printRange;
			}
			set
			{
				if (!Enum.IsDefined(typeof(PrintRange), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(PrintRange));
				}
				this.printRange = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the printing output is sent to a file instead of a port.</summary>
		/// <returns>
		///   <see langword="true" /> if the printing output is sent to a file; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000305 RID: 773
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x0001F004 File Offset: 0x0001D204
		// (set) Token: 0x060007BE RID: 1982 RVA: 0x0001F00C File Offset: 0x0001D20C
		public bool PrintToFile
		{
			get
			{
				return this.printToFile;
			}
			set
			{
				this.printToFile = value;
			}
		}

		/// <summary>Gets or sets the name of the printer to use.</summary>
		/// <returns>The name of the printer to use.</returns>
		// Token: 0x17000306 RID: 774
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x0001F015 File Offset: 0x0001D215
		// (set) Token: 0x060007C0 RID: 1984 RVA: 0x0001F027 File Offset: 0x0001D227
		public string PrinterName
		{
			get
			{
				IntSecurity.AllPrinting.Demand();
				return this.PrinterNameInternal;
			}
			set
			{
				IntSecurity.AllPrinting.Demand();
				this.PrinterNameInternal = value;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x0001F03A File Offset: 0x0001D23A
		// (set) Token: 0x060007C2 RID: 1986 RVA: 0x0001F050 File Offset: 0x0001D250
		private string PrinterNameInternal
		{
			get
			{
				if (this.printerName == null)
				{
					return PrinterSettings.GetDefaultPrinterName();
				}
				return this.printerName;
			}
			set
			{
				this.cachedDevmode = null;
				this.extrainfo = null;
				this.printerName = value;
			}
		}

		/// <summary>Gets all the resolutions that are supported by this printer.</summary>
		/// <returns>A <see cref="T:System.Drawing.Printing.PrinterSettings.PrinterResolutionCollection" /> that represents the resolutions that are supported by this printer.</returns>
		// Token: 0x17000308 RID: 776
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x0001F067 File Offset: 0x0001D267
		public PrinterSettings.PrinterResolutionCollection PrinterResolutions
		{
			get
			{
				return new PrinterSettings.PrinterResolutionCollection(this.Get_PrinterResolutions());
			}
		}

		/// <summary>Returns a value indicating whether the printer supports printing the specified image format.</summary>
		/// <param name="imageFormat">An <see cref="T:System.Drawing.Imaging.ImageFormat" /> to print.</param>
		/// <returns>
		///   <see langword="true" /> if the printer supports printing the specified image format; otherwise, <see langword="false" />.</returns>
		// Token: 0x060007C4 RID: 1988 RVA: 0x0001F074 File Offset: 0x0001D274
		public bool IsDirectPrintingSupported(ImageFormat imageFormat)
		{
			bool flag = false;
			if (imageFormat.Equals(ImageFormat.Jpeg) || imageFormat.Equals(ImageFormat.Png))
			{
				int num = (imageFormat.Equals(ImageFormat.Jpeg) ? 4119 : 4120);
				int num2 = 0;
				DeviceContext deviceContext = this.CreateInformationContext(this.DefaultPageSettings);
				HandleRef handleRef = new HandleRef(deviceContext, deviceContext.Hdc);
				try
				{
					flag = SafeNativeMethods.ExtEscape(handleRef, 8, Marshal.SizeOf(typeof(int)), ref num, 0, out num2) > 0;
				}
				finally
				{
					deviceContext.Dispose();
				}
			}
			return flag;
		}

		/// <summary>Gets a value indicating whether the printer supports printing the specified image file.</summary>
		/// <param name="image">The image to print.</param>
		/// <returns>
		///   <see langword="true" /> if the printer supports printing the specified image; otherwise, <see langword="false" />.</returns>
		// Token: 0x060007C5 RID: 1989 RVA: 0x0001F110 File Offset: 0x0001D310
		public bool IsDirectPrintingSupported(Image image)
		{
			bool flag = false;
			if (image.RawFormat.Equals(ImageFormat.Jpeg) || image.RawFormat.Equals(ImageFormat.Png))
			{
				MemoryStream memoryStream = new MemoryStream();
				try
				{
					image.Save(memoryStream, image.RawFormat);
					memoryStream.Position = 0L;
					using (BufferedStream bufferedStream = new BufferedStream(memoryStream))
					{
						int num = (int)bufferedStream.Length;
						byte[] array = new byte[num];
						int num2 = bufferedStream.Read(array, 0, num);
						int num3 = (image.RawFormat.Equals(ImageFormat.Jpeg) ? 4119 : 4120);
						int num4 = 0;
						DeviceContext deviceContext = this.CreateInformationContext(this.DefaultPageSettings);
						HandleRef handleRef = new HandleRef(deviceContext, deviceContext.Hdc);
						try
						{
							bool flag2 = SafeNativeMethods.ExtEscape(handleRef, 8, Marshal.SizeOf(typeof(int)), ref num3, 0, out num4) > 0;
							if (flag2)
							{
								flag = SafeNativeMethods.ExtEscape(handleRef, num3, num, array, Marshal.SizeOf(typeof(int)), out num4) > 0 && num4 == 1;
							}
						}
						finally
						{
							deviceContext.Dispose();
						}
					}
				}
				finally
				{
					memoryStream.Close();
				}
			}
			return flag;
		}

		/// <summary>Gets a value indicating whether this printer supports color printing.</summary>
		/// <returns>
		///   <see langword="true" /> if this printer supports color; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000309 RID: 777
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x0001F258 File Offset: 0x0001D458
		public bool SupportsColor
		{
			get
			{
				return this.GetDeviceCaps(12, 1) > 1;
			}
		}

		/// <summary>Gets or sets the number of the last page to print.</summary>
		/// <returns>The number of the last page to print.</returns>
		/// <exception cref="T:System.ArgumentException">The value of the <see cref="P:System.Drawing.Printing.PrinterSettings.ToPage" /> property is less than zero.</exception>
		// Token: 0x1700030A RID: 778
		// (get) Token: 0x060007C7 RID: 1991 RVA: 0x0001F266 File Offset: 0x0001D466
		// (set) Token: 0x060007C8 RID: 1992 RVA: 0x0001F270 File Offset: 0x0001D470
		public int ToPage
		{
			get
			{
				return this.toPage;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException(SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"value",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.toPage = value;
			}
		}

		/// <summary>Creates a copy of this <see cref="T:System.Drawing.Printing.PrinterSettings" />.</summary>
		/// <returns>A copy of this object.</returns>
		// Token: 0x060007C9 RID: 1993 RVA: 0x0001F2C8 File Offset: 0x0001D4C8
		public object Clone()
		{
			PrinterSettings printerSettings = (PrinterSettings)base.MemberwiseClone();
			printerSettings.printDialogDisplayed = false;
			return printerSettings;
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0001F2EC File Offset: 0x0001D4EC
		internal DeviceContext CreateDeviceContext(PageSettings pageSettings)
		{
			IntPtr hdevmodeInternal = this.GetHdevmodeInternal();
			DeviceContext deviceContext = null;
			try
			{
				IntSecurity.AllPrintingAndUnmanagedCode.Assert();
				try
				{
					pageSettings.CopyToHdevmode(hdevmodeInternal);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				deviceContext = this.CreateDeviceContext(hdevmodeInternal);
			}
			finally
			{
				SafeNativeMethods.GlobalFree(new HandleRef(null, hdevmodeInternal));
			}
			return deviceContext;
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0001F350 File Offset: 0x0001D550
		internal DeviceContext CreateDeviceContext(IntPtr hdevmode)
		{
			IntPtr intPtr = SafeNativeMethods.GlobalLock(new HandleRef(null, hdevmode));
			DeviceContext deviceContext = DeviceContext.CreateDC(this.DriverName, this.PrinterNameInternal, null, new HandleRef(null, intPtr));
			SafeNativeMethods.GlobalUnlock(new HandleRef(null, hdevmode));
			return deviceContext;
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0001F394 File Offset: 0x0001D594
		internal DeviceContext CreateInformationContext(PageSettings pageSettings)
		{
			IntPtr hdevmodeInternal = this.GetHdevmodeInternal();
			DeviceContext deviceContext;
			try
			{
				IntSecurity.AllPrintingAndUnmanagedCode.Assert();
				try
				{
					pageSettings.CopyToHdevmode(hdevmodeInternal);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				deviceContext = this.CreateInformationContext(hdevmodeInternal);
			}
			finally
			{
				SafeNativeMethods.GlobalFree(new HandleRef(null, hdevmodeInternal));
			}
			return deviceContext;
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0001F3F8 File Offset: 0x0001D5F8
		internal DeviceContext CreateInformationContext(IntPtr hdevmode)
		{
			IntPtr intPtr = SafeNativeMethods.GlobalLock(new HandleRef(null, hdevmode));
			DeviceContext deviceContext = DeviceContext.CreateIC(this.DriverName, this.PrinterNameInternal, null, new HandleRef(null, intPtr));
			SafeNativeMethods.GlobalUnlock(new HandleRef(null, hdevmode));
			return deviceContext;
		}

		/// <summary>Returns a <see cref="T:System.Drawing.Graphics" /> that contains printer information that is useful when creating a <see cref="T:System.Drawing.Printing.PrintDocument" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Graphics" /> that contains information from a printer.</returns>
		/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist.</exception>
		// Token: 0x060007CE RID: 1998 RVA: 0x0001F43A File Offset: 0x0001D63A
		public Graphics CreateMeasurementGraphics()
		{
			return this.CreateMeasurementGraphics(this.DefaultPageSettings);
		}

		/// <summary>Returns a <see cref="T:System.Drawing.Graphics" /> that contains printer information, optionally specifying the origin at the margins.</summary>
		/// <param name="honorOriginAtMargins">
		///   <see langword="true" /> to indicate the origin at the margins; otherwise, <see langword="false" />.</param>
		/// <returns>A <see cref="T:System.Drawing.Graphics" /> that contains printer information from the <see cref="T:System.Drawing.Printing.PageSettings" />.</returns>
		// Token: 0x060007CF RID: 1999 RVA: 0x0001F448 File Offset: 0x0001D648
		public Graphics CreateMeasurementGraphics(bool honorOriginAtMargins)
		{
			Graphics graphics = this.CreateMeasurementGraphics();
			if (graphics != null && honorOriginAtMargins)
			{
				IntSecurity.AllPrintingAndUnmanagedCode.Assert();
				try
				{
					graphics.TranslateTransform(-this.defaultPageSettings.HardMarginX, -this.defaultPageSettings.HardMarginY);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				graphics.TranslateTransform((float)this.defaultPageSettings.Margins.Left, (float)this.defaultPageSettings.Margins.Top);
			}
			return graphics;
		}

		/// <summary>Returns a <see cref="T:System.Drawing.Graphics" /> that contains printer information associated with the specified <see cref="T:System.Drawing.Printing.PageSettings" />.</summary>
		/// <param name="pageSettings">The <see cref="T:System.Drawing.Printing.PageSettings" /> to retrieve a graphics object for.</param>
		/// <returns>A <see cref="T:System.Drawing.Graphics" /> that contains printer information from the <see cref="T:System.Drawing.Printing.PageSettings" />.</returns>
		// Token: 0x060007D0 RID: 2000 RVA: 0x0001F4D0 File Offset: 0x0001D6D0
		public Graphics CreateMeasurementGraphics(PageSettings pageSettings)
		{
			DeviceContext deviceContext = this.CreateDeviceContext(pageSettings);
			Graphics graphics = Graphics.FromHdcInternal(deviceContext.Hdc);
			graphics.PrintingHelper = deviceContext;
			return graphics;
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Graphics" /> associated with the specified page settings and optionally specifying the origin at the margins.</summary>
		/// <param name="pageSettings">The <see cref="T:System.Drawing.Printing.PageSettings" /> to retrieve a <see cref="T:System.Drawing.Graphics" /> object for.</param>
		/// <param name="honorOriginAtMargins">
		///   <see langword="true" /> to specify the origin at the margins; otherwise, <see langword="false" />.</param>
		/// <returns>A <see cref="T:System.Drawing.Graphics" /> that contains printer information from the <see cref="T:System.Drawing.Printing.PageSettings" />.</returns>
		// Token: 0x060007D1 RID: 2001 RVA: 0x0001F4FC File Offset: 0x0001D6FC
		public Graphics CreateMeasurementGraphics(PageSettings pageSettings, bool honorOriginAtMargins)
		{
			Graphics graphics = this.CreateMeasurementGraphics();
			if (graphics != null && honorOriginAtMargins)
			{
				IntSecurity.AllPrintingAndUnmanagedCode.Assert();
				try
				{
					graphics.TranslateTransform(-pageSettings.HardMarginX, -pageSettings.HardMarginY);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				graphics.TranslateTransform((float)pageSettings.Margins.Left, (float)pageSettings.Margins.Top);
			}
			return graphics;
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0001F570 File Offset: 0x0001D770
		private static SafeNativeMethods.PRINTDLGX86 CreatePRINTDLGX86()
		{
			return new SafeNativeMethods.PRINTDLGX86
			{
				lStructSize = Marshal.SizeOf(typeof(SafeNativeMethods.PRINTDLGX86)),
				hwndOwner = IntPtr.Zero,
				hDevMode = IntPtr.Zero,
				hDevNames = IntPtr.Zero,
				Flags = 0,
				hwndOwner = IntPtr.Zero,
				hDC = IntPtr.Zero,
				nFromPage = 1,
				nToPage = 1,
				nMinPage = 0,
				nMaxPage = 9999,
				nCopies = 1,
				hInstance = IntPtr.Zero,
				lCustData = IntPtr.Zero,
				lpfnPrintHook = IntPtr.Zero,
				lpfnSetupHook = IntPtr.Zero,
				lpPrintTemplateName = null,
				lpSetupTemplateName = null,
				hPrintTemplate = IntPtr.Zero,
				hSetupTemplate = IntPtr.Zero
			};
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0001F650 File Offset: 0x0001D850
		private static SafeNativeMethods.PRINTDLG CreatePRINTDLG()
		{
			return new SafeNativeMethods.PRINTDLG
			{
				lStructSize = Marshal.SizeOf(typeof(SafeNativeMethods.PRINTDLG)),
				hwndOwner = IntPtr.Zero,
				hDevMode = IntPtr.Zero,
				hDevNames = IntPtr.Zero,
				Flags = 0,
				hwndOwner = IntPtr.Zero,
				hDC = IntPtr.Zero,
				nFromPage = 1,
				nToPage = 1,
				nMinPage = 0,
				nMaxPage = 9999,
				nCopies = 1,
				hInstance = IntPtr.Zero,
				lCustData = IntPtr.Zero,
				lpfnPrintHook = IntPtr.Zero,
				lpfnSetupHook = IntPtr.Zero,
				lpPrintTemplateName = null,
				lpSetupTemplateName = null,
				hPrintTemplate = IntPtr.Zero,
				hSetupTemplate = IntPtr.Zero
			};
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0001F730 File Offset: 0x0001D930
		private int DeviceCapabilities(short capability, IntPtr pointerToBuffer, int defaultValue)
		{
			IntSecurity.AllPrinting.Assert();
			string text = this.PrinterName;
			CodeAccessPermission.RevertAssert();
			IntSecurity.UnmanagedCode.Assert();
			return PrinterSettings.FastDeviceCapabilities(capability, pointerToBuffer, defaultValue, text);
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0001F768 File Offset: 0x0001D968
		private static int FastDeviceCapabilities(short capability, IntPtr pointerToBuffer, int defaultValue, string printerName)
		{
			int num = SafeNativeMethods.DeviceCapabilities(printerName, PrinterSettings.GetOutputPort(), capability, pointerToBuffer, IntPtr.Zero);
			if (num == -1)
			{
				return defaultValue;
			}
			return num;
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0001F790 File Offset: 0x0001D990
		private static string GetDefaultPrinterName()
		{
			IntSecurity.UnmanagedCode.Assert();
			if (IntPtr.Size == 8)
			{
				SafeNativeMethods.PRINTDLG printdlg = PrinterSettings.CreatePRINTDLG();
				printdlg.Flags = 1024;
				if (!SafeNativeMethods.PrintDlg(printdlg))
				{
					return SR.GetString("NoDefaultPrinter");
				}
				IntPtr hDevNames = printdlg.hDevNames;
				IntPtr intPtr = SafeNativeMethods.GlobalLock(new HandleRef(printdlg, hDevNames));
				if (intPtr == IntPtr.Zero)
				{
					throw new Win32Exception();
				}
				string text = PrinterSettings.ReadOneDEVNAME(intPtr, 1);
				SafeNativeMethods.GlobalUnlock(new HandleRef(printdlg, hDevNames));
				intPtr = IntPtr.Zero;
				SafeNativeMethods.GlobalFree(new HandleRef(printdlg, printdlg.hDevNames));
				SafeNativeMethods.GlobalFree(new HandleRef(printdlg, printdlg.hDevMode));
				return text;
			}
			else
			{
				SafeNativeMethods.PRINTDLGX86 printdlgx = PrinterSettings.CreatePRINTDLGX86();
				printdlgx.Flags = 1024;
				if (!SafeNativeMethods.PrintDlg(printdlgx))
				{
					return SR.GetString("NoDefaultPrinter");
				}
				IntPtr hDevNames2 = printdlgx.hDevNames;
				IntPtr intPtr2 = SafeNativeMethods.GlobalLock(new HandleRef(printdlgx, hDevNames2));
				if (intPtr2 == IntPtr.Zero)
				{
					throw new Win32Exception();
				}
				string text2 = PrinterSettings.ReadOneDEVNAME(intPtr2, 1);
				SafeNativeMethods.GlobalUnlock(new HandleRef(printdlgx, hDevNames2));
				intPtr2 = IntPtr.Zero;
				SafeNativeMethods.GlobalFree(new HandleRef(printdlgx, printdlgx.hDevNames));
				SafeNativeMethods.GlobalFree(new HandleRef(printdlgx, printdlgx.hDevMode));
				return text2;
			}
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0001F8E4 File Offset: 0x0001DAE4
		private static string GetOutputPort()
		{
			IntSecurity.UnmanagedCode.Assert();
			if (IntPtr.Size == 8)
			{
				SafeNativeMethods.PRINTDLG printdlg = PrinterSettings.CreatePRINTDLG();
				printdlg.Flags = 1024;
				if (!SafeNativeMethods.PrintDlg(printdlg))
				{
					return SR.GetString("NoDefaultPrinter");
				}
				IntPtr hDevNames = printdlg.hDevNames;
				IntPtr intPtr = SafeNativeMethods.GlobalLock(new HandleRef(printdlg, hDevNames));
				if (intPtr == IntPtr.Zero)
				{
					throw new Win32Exception();
				}
				string text = PrinterSettings.ReadOneDEVNAME(intPtr, 2);
				SafeNativeMethods.GlobalUnlock(new HandleRef(printdlg, hDevNames));
				intPtr = IntPtr.Zero;
				SafeNativeMethods.GlobalFree(new HandleRef(printdlg, printdlg.hDevNames));
				SafeNativeMethods.GlobalFree(new HandleRef(printdlg, printdlg.hDevMode));
				return text;
			}
			else
			{
				SafeNativeMethods.PRINTDLGX86 printdlgx = PrinterSettings.CreatePRINTDLGX86();
				printdlgx.Flags = 1024;
				if (!SafeNativeMethods.PrintDlg(printdlgx))
				{
					return SR.GetString("NoDefaultPrinter");
				}
				IntPtr hDevNames2 = printdlgx.hDevNames;
				IntPtr intPtr2 = SafeNativeMethods.GlobalLock(new HandleRef(printdlgx, hDevNames2));
				if (intPtr2 == IntPtr.Zero)
				{
					throw new Win32Exception();
				}
				string text2 = PrinterSettings.ReadOneDEVNAME(intPtr2, 2);
				SafeNativeMethods.GlobalUnlock(new HandleRef(printdlgx, hDevNames2));
				intPtr2 = IntPtr.Zero;
				SafeNativeMethods.GlobalFree(new HandleRef(printdlgx, printdlgx.hDevNames));
				SafeNativeMethods.GlobalFree(new HandleRef(printdlgx, printdlgx.hDevMode));
				return text2;
			}
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0001FA38 File Offset: 0x0001DC38
		private int GetDeviceCaps(int capability, int defaultValue)
		{
			DeviceContext deviceContext = this.CreateInformationContext(this.DefaultPageSettings);
			int num = defaultValue;
			try
			{
				num = UnsafeNativeMethods.GetDeviceCaps(new HandleRef(deviceContext, deviceContext.Hdc), capability);
			}
			catch (InvalidPrinterException)
			{
			}
			finally
			{
				deviceContext.Dispose();
			}
			return num;
		}

		/// <summary>Creates a handle to a <see langword="DEVMODE" /> structure that corresponds to the printer settings.</summary>
		/// <returns>A handle to a <see langword="DEVMODE" /> structure.</returns>
		/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The printer's initialization information could not be retrieved.</exception>
		// Token: 0x060007D9 RID: 2009 RVA: 0x0001FA90 File Offset: 0x0001DC90
		public IntPtr GetHdevmode()
		{
			IntSecurity.AllPrintingAndUnmanagedCode.Demand();
			IntPtr hdevmodeInternal = this.GetHdevmodeInternal();
			this.defaultPageSettings.CopyToHdevmode(hdevmodeInternal);
			return hdevmodeInternal;
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0001FABB File Offset: 0x0001DCBB
		internal IntPtr GetHdevmodeInternal()
		{
			return this.GetHdevmodeInternal(this.PrinterNameInternal);
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0001FACC File Offset: 0x0001DCCC
		private IntPtr GetHdevmodeInternal(string printer)
		{
			int num = SafeNativeMethods.DocumentProperties(NativeMethods.NullHandleRef, NativeMethods.NullHandleRef, printer, IntPtr.Zero, NativeMethods.NullHandleRef, 0);
			if (num < 1)
			{
				throw new InvalidPrinterException(this);
			}
			IntPtr intPtr = SafeNativeMethods.GlobalAlloc(2, (uint)num);
			IntPtr intPtr2 = SafeNativeMethods.GlobalLock(new HandleRef(null, intPtr));
			if (this.cachedDevmode != null)
			{
				Marshal.Copy(this.cachedDevmode, 0, intPtr2, (int)this.devmodebytes);
			}
			else
			{
				int num2 = SafeNativeMethods.DocumentProperties(NativeMethods.NullHandleRef, NativeMethods.NullHandleRef, printer, intPtr2, NativeMethods.NullHandleRef, 2);
				if (num2 < 0)
				{
					throw new Win32Exception();
				}
			}
			SafeNativeMethods.DEVMODE devmode = (SafeNativeMethods.DEVMODE)UnsafeNativeMethods.PtrToStructure(intPtr2, typeof(SafeNativeMethods.DEVMODE));
			checked
			{
				if (this.extrainfo != null && this.extrabytes <= devmode.dmDriverExtra)
				{
					IntPtr intPtr3 = (IntPtr)((long)intPtr2 + unchecked((long)devmode.dmSize));
					Marshal.Copy(this.extrainfo, 0, intPtr3, (int)this.extrabytes);
				}
				if ((devmode.dmFields & 256) == 256 && this.copies != -1)
				{
					devmode.dmCopies = this.copies;
				}
			}
			if ((devmode.dmFields & 4096) == 4096 && this.duplex != Duplex.Default)
			{
				devmode.dmDuplex = (short)this.duplex;
			}
			if ((devmode.dmFields & 32768) == 32768 && this.collate.IsNotDefault)
			{
				devmode.dmCollate = (((bool)this.collate) ? 1 : 0);
			}
			Marshal.StructureToPtr(devmode, intPtr2, false);
			int num3 = SafeNativeMethods.DocumentProperties(NativeMethods.NullHandleRef, NativeMethods.NullHandleRef, printer, intPtr2, intPtr2, 10);
			if (num3 < 0)
			{
				SafeNativeMethods.GlobalFree(new HandleRef(null, intPtr));
				SafeNativeMethods.GlobalUnlock(new HandleRef(null, intPtr));
				return IntPtr.Zero;
			}
			SafeNativeMethods.GlobalUnlock(new HandleRef(null, intPtr));
			return intPtr;
		}

		/// <summary>Creates a handle to a <see langword="DEVMODE" /> structure that corresponds to the printer and the page settings specified through the <paramref name="pageSettings" /> parameter.</summary>
		/// <param name="pageSettings">The <see cref="T:System.Drawing.Printing.PageSettings" /> object that the <see langword="DEVMODE" /> structure's handle corresponds to.</param>
		/// <returns>A handle to a <see langword="DEVMODE" /> structure.</returns>
		/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The printer's initialization information could not be retrieved.</exception>
		// Token: 0x060007DC RID: 2012 RVA: 0x0001FC88 File Offset: 0x0001DE88
		public IntPtr GetHdevmode(PageSettings pageSettings)
		{
			IntSecurity.AllPrintingAndUnmanagedCode.Demand();
			IntPtr hdevmodeInternal = this.GetHdevmodeInternal();
			pageSettings.CopyToHdevmode(hdevmodeInternal);
			return hdevmodeInternal;
		}

		/// <summary>Creates a handle to a <see langword="DEVNAMES" /> structure that corresponds to the printer settings.</summary>
		/// <returns>A handle to a <see langword="DEVNAMES" /> structure.</returns>
		// Token: 0x060007DD RID: 2013 RVA: 0x0001FCB0 File Offset: 0x0001DEB0
		public IntPtr GetHdevnames()
		{
			IntSecurity.AllPrintingAndUnmanagedCode.Demand();
			string text = this.PrinterName;
			string text2 = this.DriverName;
			string text3 = this.OutputPort;
			int num = checked(4 + text.Length + text2.Length + text3.Length);
			short num2 = (short)(8 / Marshal.SystemDefaultCharSize);
			uint num3 = (uint)(checked(Marshal.SystemDefaultCharSize * ((int)num2 + num)));
			IntPtr intPtr = SafeNativeMethods.GlobalAlloc(66, num3);
			IntPtr intPtr2 = SafeNativeMethods.GlobalLock(new HandleRef(null, intPtr));
			Marshal.WriteInt16(intPtr2, num2);
			num2 += this.WriteOneDEVNAME(text2, intPtr2, (int)num2);
			Marshal.WriteInt16((IntPtr)(checked((long)intPtr2 + 2L)), num2);
			num2 += this.WriteOneDEVNAME(text, intPtr2, (int)num2);
			Marshal.WriteInt16((IntPtr)(checked((long)intPtr2 + 4L)), num2);
			num2 += this.WriteOneDEVNAME(text3, intPtr2, (int)num2);
			Marshal.WriteInt16((IntPtr)(checked((long)intPtr2 + 6L)), num2);
			SafeNativeMethods.GlobalUnlock(new HandleRef(null, intPtr));
			return intPtr;
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0001FDB1 File Offset: 0x0001DFB1
		internal short GetModeField(ModeField field, short defaultValue)
		{
			return this.GetModeField(field, defaultValue, IntPtr.Zero);
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0001FDC0 File Offset: 0x0001DFC0
		internal short GetModeField(ModeField field, short defaultValue, IntPtr modeHandle)
		{
			bool flag = false;
			short num;
			try
			{
				if (modeHandle == IntPtr.Zero)
				{
					try
					{
						modeHandle = this.GetHdevmodeInternal();
						flag = true;
					}
					catch (InvalidPrinterException)
					{
						return defaultValue;
					}
				}
				IntPtr intPtr = SafeNativeMethods.GlobalLock(new HandleRef(this, modeHandle));
				SafeNativeMethods.DEVMODE devmode = (SafeNativeMethods.DEVMODE)UnsafeNativeMethods.PtrToStructure(intPtr, typeof(SafeNativeMethods.DEVMODE));
				switch (field)
				{
				case ModeField.Orientation:
					num = devmode.dmOrientation;
					break;
				case ModeField.PaperSize:
					num = devmode.dmPaperSize;
					break;
				case ModeField.PaperLength:
					num = devmode.dmPaperLength;
					break;
				case ModeField.PaperWidth:
					num = devmode.dmPaperWidth;
					break;
				case ModeField.Copies:
					num = devmode.dmCopies;
					break;
				case ModeField.DefaultSource:
					num = devmode.dmDefaultSource;
					break;
				case ModeField.PrintQuality:
					num = devmode.dmPrintQuality;
					break;
				case ModeField.Color:
					num = devmode.dmColor;
					break;
				case ModeField.Duplex:
					num = devmode.dmDuplex;
					break;
				case ModeField.YResolution:
					num = devmode.dmYResolution;
					break;
				case ModeField.TTOption:
					num = devmode.dmTTOption;
					break;
				case ModeField.Collate:
					num = devmode.dmCollate;
					break;
				default:
					num = defaultValue;
					break;
				}
				SafeNativeMethods.GlobalUnlock(new HandleRef(this, modeHandle));
			}
			finally
			{
				if (flag)
				{
					SafeNativeMethods.GlobalFree(new HandleRef(this, modeHandle));
				}
			}
			return num;
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0001FEFC File Offset: 0x0001E0FC
		internal PaperSize[] Get_PaperSizes()
		{
			IntSecurity.AllPrintingAndUnmanagedCode.Assert();
			string text = this.PrinterName;
			int num = PrinterSettings.FastDeviceCapabilities(16, IntPtr.Zero, -1, text);
			if (num == -1)
			{
				return new PaperSize[0];
			}
			int num2 = Marshal.SystemDefaultCharSize * 64;
			IntPtr intPtr = Marshal.AllocCoTaskMem(checked(num2 * num));
			PrinterSettings.FastDeviceCapabilities(16, intPtr, -1, text);
			IntPtr intPtr2 = Marshal.AllocCoTaskMem(2 * num);
			PrinterSettings.FastDeviceCapabilities(2, intPtr2, -1, text);
			IntPtr intPtr3 = Marshal.AllocCoTaskMem(8 * num);
			PrinterSettings.FastDeviceCapabilities(3, intPtr3, -1, text);
			PaperSize[] array = new PaperSize[num];
			for (int i = 0; i < num; i++)
			{
				checked
				{
					string text2 = Marshal.PtrToStringAuto((IntPtr)((long)intPtr + unchecked((long)(checked(num2 * i)))), 64);
					int num3 = text2.IndexOf('\0');
					if (num3 > -1)
					{
						text2 = text2.Substring(0, num3);
					}
					short num4 = Marshal.ReadInt16((IntPtr)((long)intPtr2 + unchecked((long)(checked(i * 2)))));
					int num5 = Marshal.ReadInt32((IntPtr)((long)intPtr3 + unchecked((long)(checked(i * 8)))));
					int num6 = Marshal.ReadInt32((IntPtr)((long)intPtr3 + unchecked((long)(checked(i * 8))) + 4L));
					array[i] = new PaperSize((PaperKind)num4, text2, PrinterUnitConvert.Convert(num5, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display), PrinterUnitConvert.Convert(num6, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display));
				}
			}
			Marshal.FreeCoTaskMem(intPtr);
			Marshal.FreeCoTaskMem(intPtr2);
			Marshal.FreeCoTaskMem(intPtr3);
			return array;
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00020054 File Offset: 0x0001E254
		internal PaperSource[] Get_PaperSources()
		{
			IntSecurity.AllPrintingAndUnmanagedCode.Assert();
			string text = this.PrinterName;
			int num = PrinterSettings.FastDeviceCapabilities(12, IntPtr.Zero, -1, text);
			if (num == -1)
			{
				return new PaperSource[0];
			}
			int num2 = Marshal.SystemDefaultCharSize * 24;
			IntPtr intPtr = Marshal.AllocCoTaskMem(checked(num2 * num));
			PrinterSettings.FastDeviceCapabilities(12, intPtr, -1, text);
			IntPtr intPtr2 = Marshal.AllocCoTaskMem(2 * num);
			PrinterSettings.FastDeviceCapabilities(6, intPtr2, -1, text);
			PaperSource[] array = new PaperSource[num];
			for (int i = 0; i < num; i++)
			{
				checked
				{
					string text2 = Marshal.PtrToStringAuto((IntPtr)((long)intPtr + unchecked((long)(checked(num2 * i)))), 24);
					int num3 = text2.IndexOf('\0');
					if (num3 > -1)
					{
						text2 = text2.Substring(0, num3);
					}
					short num4 = Marshal.ReadInt16((IntPtr)((long)intPtr2 + unchecked((long)(checked(2 * i)))));
					array[i] = new PaperSource((PaperSourceKind)num4, text2);
				}
			}
			Marshal.FreeCoTaskMem(intPtr);
			Marshal.FreeCoTaskMem(intPtr2);
			return array;
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00020144 File Offset: 0x0001E344
		internal PrinterResolution[] Get_PrinterResolutions()
		{
			IntSecurity.AllPrintingAndUnmanagedCode.Assert();
			string text = this.PrinterName;
			int num = PrinterSettings.FastDeviceCapabilities(13, IntPtr.Zero, -1, text);
			if (num == -1)
			{
				return new PrinterResolution[]
				{
					new PrinterResolution(PrinterResolutionKind.High, -4, -1),
					new PrinterResolution(PrinterResolutionKind.Medium, -3, -1),
					new PrinterResolution(PrinterResolutionKind.Low, -2, -1),
					new PrinterResolution(PrinterResolutionKind.Draft, -1, -1)
				};
			}
			PrinterResolution[] array = new PrinterResolution[num + 4];
			array[0] = new PrinterResolution(PrinterResolutionKind.High, -4, -1);
			array[1] = new PrinterResolution(PrinterResolutionKind.Medium, -3, -1);
			array[2] = new PrinterResolution(PrinterResolutionKind.Low, -2, -1);
			array[3] = new PrinterResolution(PrinterResolutionKind.Draft, -1, -1);
			IntPtr intPtr = Marshal.AllocCoTaskMem(checked(8 * num));
			PrinterSettings.FastDeviceCapabilities(13, intPtr, -1, text);
			for (int i = 0; i < num; i++)
			{
				int num2;
				int num3;
				checked
				{
					num2 = Marshal.ReadInt32((IntPtr)((long)intPtr + unchecked((long)(checked(i * 8)))));
					num3 = Marshal.ReadInt32((IntPtr)((long)intPtr + unchecked((long)(checked(i * 8))) + 4L));
				}
				array[i + 4] = new PrinterResolution(PrinterResolutionKind.Custom, num2, num3);
			}
			Marshal.FreeCoTaskMem(intPtr);
			return array;
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0002025C File Offset: 0x0001E45C
		private static string ReadOneDEVNAME(IntPtr pDevnames, int slot)
		{
			checked
			{
				int num = Marshal.SystemDefaultCharSize * (int)Marshal.ReadInt16((IntPtr)((long)pDevnames + unchecked((long)(checked(slot * 2)))));
				return Marshal.PtrToStringAuto((IntPtr)((long)pDevnames + unchecked((long)num)));
			}
		}

		/// <summary>Copies the relevant information out of the given handle and into the <see cref="T:System.Drawing.Printing.PrinterSettings" />.</summary>
		/// <param name="hdevmode">The handle to a Win32 <see langword="DEVMODE" /> structure.</param>
		/// <exception cref="T:System.ArgumentException">The printer handle is not valid.</exception>
		// Token: 0x060007E4 RID: 2020 RVA: 0x0002029C File Offset: 0x0001E49C
		public void SetHdevmode(IntPtr hdevmode)
		{
			IntSecurity.AllPrintingAndUnmanagedCode.Demand();
			if (hdevmode == IntPtr.Zero)
			{
				throw new ArgumentException(SR.GetString("InvalidPrinterHandle", new object[] { hdevmode }));
			}
			IntPtr intPtr = SafeNativeMethods.GlobalLock(new HandleRef(null, hdevmode));
			SafeNativeMethods.DEVMODE devmode = (SafeNativeMethods.DEVMODE)UnsafeNativeMethods.PtrToStructure(intPtr, typeof(SafeNativeMethods.DEVMODE));
			this.devmodebytes = devmode.dmSize;
			if (this.devmodebytes > 0)
			{
				this.cachedDevmode = new byte[(int)this.devmodebytes];
				Marshal.Copy(intPtr, this.cachedDevmode, 0, (int)this.devmodebytes);
			}
			this.extrabytes = devmode.dmDriverExtra;
			checked
			{
				if (this.extrabytes > 0)
				{
					this.extrainfo = new byte[(int)this.extrabytes];
					Marshal.Copy((IntPtr)((long)intPtr + unchecked((long)devmode.dmSize)), this.extrainfo, 0, (int)this.extrabytes);
				}
				if ((devmode.dmFields & 256) == 256)
				{
					this.copies = devmode.dmCopies;
				}
				if ((devmode.dmFields & 4096) == 4096)
				{
					this.duplex = (Duplex)devmode.dmDuplex;
				}
				if ((devmode.dmFields & 32768) == 32768)
				{
					this.collate = devmode.dmCollate == 1;
				}
				SafeNativeMethods.GlobalUnlock(new HandleRef(null, hdevmode));
			}
		}

		/// <summary>Copies the relevant information out of the given handle and into the <see cref="T:System.Drawing.Printing.PrinterSettings" />.</summary>
		/// <param name="hdevnames">The handle to a Win32 <see langword="DEVNAMES" /> structure.</param>
		/// <exception cref="T:System.ArgumentException">The printer handle is invalid.</exception>
		// Token: 0x060007E5 RID: 2021 RVA: 0x000203F8 File Offset: 0x0001E5F8
		public void SetHdevnames(IntPtr hdevnames)
		{
			IntSecurity.AllPrintingAndUnmanagedCode.Demand();
			if (hdevnames == IntPtr.Zero)
			{
				throw new ArgumentException(SR.GetString("InvalidPrinterHandle", new object[] { hdevnames }));
			}
			IntPtr intPtr = SafeNativeMethods.GlobalLock(new HandleRef(null, hdevnames));
			this.driverName = PrinterSettings.ReadOneDEVNAME(intPtr, 0);
			this.printerName = PrinterSettings.ReadOneDEVNAME(intPtr, 1);
			this.outputPort = PrinterSettings.ReadOneDEVNAME(intPtr, 2);
			this.PrintDialogDisplayed = true;
			SafeNativeMethods.GlobalUnlock(new HandleRef(null, hdevnames));
		}

		/// <summary>Provides information about the <see cref="T:System.Drawing.Printing.PrinterSettings" /> in string form.</summary>
		/// <returns>A string.</returns>
		// Token: 0x060007E6 RID: 2022 RVA: 0x00020484 File Offset: 0x0001E684
		public override string ToString()
		{
			string text = (IntSecurity.HasPermission(IntSecurity.AllPrinting) ? this.PrinterName : "<printer name unavailable>");
			return string.Concat(new string[]
			{
				"[PrinterSettings ",
				text,
				" Copies=",
				this.Copies.ToString(CultureInfo.InvariantCulture),
				" Collate=",
				this.Collate.ToString(CultureInfo.InvariantCulture),
				" Duplex=",
				TypeDescriptor.GetConverter(typeof(Duplex)).ConvertToString((int)this.Duplex),
				" FromPage=",
				this.FromPage.ToString(CultureInfo.InvariantCulture),
				" LandscapeAngle=",
				this.LandscapeAngle.ToString(CultureInfo.InvariantCulture),
				" MaximumCopies=",
				this.MaximumCopies.ToString(CultureInfo.InvariantCulture),
				" OutputPort=",
				this.OutputPort.ToString(CultureInfo.InvariantCulture),
				" ToPage=",
				this.ToPage.ToString(CultureInfo.InvariantCulture),
				"]"
			});
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x000205D0 File Offset: 0x0001E7D0
		private short WriteOneDEVNAME(string str, IntPtr bufferStart, int index)
		{
			if (str == null)
			{
				str = "";
			}
			checked
			{
				IntPtr intPtr = (IntPtr)((long)bufferStart + unchecked((long)(checked(index * Marshal.SystemDefaultCharSize))));
				if (Marshal.SystemDefaultCharSize == 1)
				{
					byte[] bytes = Encoding.Default.GetBytes(str);
					Marshal.Copy(bytes, 0, intPtr, bytes.Length);
					Marshal.WriteByte((IntPtr)((long)intPtr + unchecked((long)bytes.Length)), 0);
				}
				else
				{
					char[] array = str.ToCharArray();
					Marshal.Copy(array, 0, intPtr, array.Length);
					Marshal.WriteInt16((IntPtr)((long)intPtr + unchecked((long)(checked(array.Length * 2)))), 0);
				}
				return (short)(str.Length + 1);
			}
		}

		// Token: 0x040006D2 RID: 1746
		private const int PADDING_IA64 = 4;

		// Token: 0x040006D3 RID: 1747
		private string printerName;

		// Token: 0x040006D4 RID: 1748
		private string driverName = "";

		// Token: 0x040006D5 RID: 1749
		private string outputPort = "";

		// Token: 0x040006D6 RID: 1750
		private bool printToFile;

		// Token: 0x040006D7 RID: 1751
		private bool printDialogDisplayed;

		// Token: 0x040006D8 RID: 1752
		private short extrabytes;

		// Token: 0x040006D9 RID: 1753
		private byte[] extrainfo;

		// Token: 0x040006DA RID: 1754
		private short copies = -1;

		// Token: 0x040006DB RID: 1755
		private Duplex duplex = Duplex.Default;

		// Token: 0x040006DC RID: 1756
		private TriState collate = TriState.Default;

		// Token: 0x040006DD RID: 1757
		private PageSettings defaultPageSettings;

		// Token: 0x040006DE RID: 1758
		private int fromPage;

		// Token: 0x040006DF RID: 1759
		private int toPage;

		// Token: 0x040006E0 RID: 1760
		private int maxPage = 9999;

		// Token: 0x040006E1 RID: 1761
		private int minPage;

		// Token: 0x040006E2 RID: 1762
		private PrintRange printRange;

		// Token: 0x040006E3 RID: 1763
		private short devmodebytes;

		// Token: 0x040006E4 RID: 1764
		private byte[] cachedDevmode;

		/// <summary>Contains a collection of <see cref="T:System.Drawing.Printing.PaperSize" /> objects.</summary>
		// Token: 0x0200011F RID: 287
		public class PaperSizeCollection : ICollection, IEnumerable
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrinterSettings.PaperSizeCollection" /> class.</summary>
			/// <param name="array">An array of type <see cref="T:System.Drawing.Printing.PaperSize" />.</param>
			// Token: 0x06000F55 RID: 3925 RVA: 0x0002DA89 File Offset: 0x0002BC89
			public PaperSizeCollection(PaperSize[] array)
			{
				this.array = array;
			}

			/// <summary>Gets the number of different paper sizes in the collection.</summary>
			/// <returns>The number of different paper sizes in the collection.</returns>
			// Token: 0x170003E8 RID: 1000
			// (get) Token: 0x06000F56 RID: 3926 RVA: 0x0002DA98 File Offset: 0x0002BC98
			public int Count
			{
				get
				{
					return this.array.Length;
				}
			}

			/// <summary>Gets the <see cref="T:System.Drawing.Printing.PaperSize" /> at a specified index.</summary>
			/// <param name="index">The index of the <see cref="T:System.Drawing.Printing.PaperSize" /> to get.</param>
			/// <returns>The <see cref="T:System.Drawing.Printing.PaperSize" /> at the specified index.</returns>
			// Token: 0x170003E9 RID: 1001
			public virtual PaperSize this[int index]
			{
				get
				{
					return this.array[index];
				}
			}

			/// <summary>Returns an enumerator that can iterate through the collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Drawing.Printing.PrinterSettings.PaperSizeCollection" />.</returns>
			// Token: 0x06000F58 RID: 3928 RVA: 0x0002DAAC File Offset: 0x0002BCAC
			public IEnumerator GetEnumerator()
			{
				object[] array = this.array;
				return new PrinterSettings.ArrayEnumerator(array, 0, this.Count);
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.Count" />.</summary>
			// Token: 0x170003EA RID: 1002
			// (get) Token: 0x06000F59 RID: 3929 RVA: 0x0002DACD File Offset: 0x0002BCCD
			int ICollection.Count
			{
				get
				{
					return this.Count;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
			// Token: 0x170003EB RID: 1003
			// (get) Token: 0x06000F5A RID: 3930 RVA: 0x0001E380 File Offset: 0x0001C580
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
			// Token: 0x170003EC RID: 1004
			// (get) Token: 0x06000F5B RID: 3931 RVA: 0x0002DAD5 File Offset: 0x0002BCD5
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
			/// <param name="array">A zero-based array that receives the items copied from the collection.</param>
			/// <param name="index">The index at which to start copying items.</param>
			// Token: 0x06000F5C RID: 3932 RVA: 0x0002DAD8 File Offset: 0x0002BCD8
			void ICollection.CopyTo(Array array, int index)
			{
				Array.Copy(this.array, index, array, 0, this.array.Length);
			}

			/// <summary>Copies the contents of the current <see cref="T:System.Drawing.Printing.PrinterSettings.PaperSizeCollection" /> to the specified array, starting at the specified index.</summary>
			/// <param name="paperSizes">A zero-based array that receives the items copied from the <see cref="T:System.Drawing.Printing.PrinterSettings.PaperSizeCollection" />.</param>
			/// <param name="index">The index at which to start copying items.</param>
			// Token: 0x06000F5D RID: 3933 RVA: 0x0002DAD8 File Offset: 0x0002BCD8
			public void CopyTo(PaperSize[] paperSizes, int index)
			{
				Array.Copy(this.array, index, paperSizes, 0, this.array.Length);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
			/// <returns>An enumerator associated with the collection.</returns>
			// Token: 0x06000F5E RID: 3934 RVA: 0x0002DAF0 File Offset: 0x0002BCF0
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			/// <summary>Adds a <see cref="T:System.Drawing.Printing.PrinterResolution" /> to the end of the collection.</summary>
			/// <param name="paperSize">The <see cref="T:System.Drawing.Printing.PaperSize" /> to add to the collection.</param>
			/// <returns>The zero-based index of the newly added item.</returns>
			// Token: 0x06000F5F RID: 3935 RVA: 0x0002DAF8 File Offset: 0x0002BCF8
			[EditorBrowsable(EditorBrowsableState.Never)]
			public int Add(PaperSize paperSize)
			{
				PaperSize[] array = new PaperSize[this.Count + 1];
				((ICollection)this).CopyTo(array, 0);
				array[this.Count] = paperSize;
				this.array = array;
				return this.Count;
			}

			// Token: 0x04000C68 RID: 3176
			private PaperSize[] array;
		}

		/// <summary>Contains a collection of <see cref="T:System.Drawing.Printing.PaperSource" /> objects.</summary>
		// Token: 0x02000120 RID: 288
		public class PaperSourceCollection : ICollection, IEnumerable
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrinterSettings.PaperSourceCollection" /> class.</summary>
			/// <param name="array">An array of type <see cref="T:System.Drawing.Printing.PaperSource" />.</param>
			// Token: 0x06000F60 RID: 3936 RVA: 0x0002DB31 File Offset: 0x0002BD31
			public PaperSourceCollection(PaperSource[] array)
			{
				this.array = array;
			}

			/// <summary>Gets the number of different paper sources in the collection.</summary>
			/// <returns>The number of different paper sources in the collection.</returns>
			// Token: 0x170003ED RID: 1005
			// (get) Token: 0x06000F61 RID: 3937 RVA: 0x0002DB40 File Offset: 0x0002BD40
			public int Count
			{
				get
				{
					return this.array.Length;
				}
			}

			/// <summary>Gets the <see cref="T:System.Drawing.Printing.PaperSource" /> at a specified index.</summary>
			/// <param name="index">The index of the <see cref="T:System.Drawing.Printing.PaperSource" /> to get.</param>
			/// <returns>The <see cref="T:System.Drawing.Printing.PaperSource" /> at the specified index.</returns>
			// Token: 0x170003EE RID: 1006
			public virtual PaperSource this[int index]
			{
				get
				{
					return this.array[index];
				}
			}

			/// <summary>Returns an enumerator that can iterate through the collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Drawing.Printing.PrinterSettings.PaperSourceCollection" />.</returns>
			// Token: 0x06000F63 RID: 3939 RVA: 0x0002DB54 File Offset: 0x0002BD54
			public IEnumerator GetEnumerator()
			{
				object[] array = this.array;
				return new PrinterSettings.ArrayEnumerator(array, 0, this.Count);
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.Count" />.</summary>
			// Token: 0x170003EF RID: 1007
			// (get) Token: 0x06000F64 RID: 3940 RVA: 0x0002DB75 File Offset: 0x0002BD75
			int ICollection.Count
			{
				get
				{
					return this.Count;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
			// Token: 0x170003F0 RID: 1008
			// (get) Token: 0x06000F65 RID: 3941 RVA: 0x0001E380 File Offset: 0x0001C580
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
			// Token: 0x170003F1 RID: 1009
			// (get) Token: 0x06000F66 RID: 3942 RVA: 0x0002DAD5 File Offset: 0x0002BCD5
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
			/// <param name="array">The destination array for the contents of the collection.</param>
			/// <param name="index">The index at which to start the copy operation.</param>
			// Token: 0x06000F67 RID: 3943 RVA: 0x0002DB7D File Offset: 0x0002BD7D
			void ICollection.CopyTo(Array array, int index)
			{
				Array.Copy(this.array, index, array, 0, this.array.Length);
			}

			/// <summary>Copies the contents of the current <see cref="T:System.Drawing.Printing.PrinterSettings.PaperSourceCollection" /> to the specified array, starting at the specified index.</summary>
			/// <param name="paperSources">A zero-based array that receives the items copied from the <see cref="T:System.Drawing.Printing.PrinterSettings.PaperSourceCollection" />.</param>
			/// <param name="index">The index at which to start copying items.</param>
			// Token: 0x06000F68 RID: 3944 RVA: 0x0002DB7D File Offset: 0x0002BD7D
			public void CopyTo(PaperSource[] paperSources, int index)
			{
				Array.Copy(this.array, index, paperSources, 0, this.array.Length);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
			// Token: 0x06000F69 RID: 3945 RVA: 0x0002DB95 File Offset: 0x0002BD95
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			/// <summary>Adds the specified <see cref="T:System.Drawing.Printing.PaperSource" /> to end of the <see cref="T:System.Drawing.Printing.PrinterSettings.PaperSourceCollection" />.</summary>
			/// <param name="paperSource">The <see cref="T:System.Drawing.Printing.PaperSource" /> to add to the collection.</param>
			/// <returns>The zero-based index where the <see cref="T:System.Drawing.Printing.PaperSource" /> was added.</returns>
			// Token: 0x06000F6A RID: 3946 RVA: 0x0002DBA0 File Offset: 0x0002BDA0
			[EditorBrowsable(EditorBrowsableState.Never)]
			public int Add(PaperSource paperSource)
			{
				PaperSource[] array = new PaperSource[this.Count + 1];
				((ICollection)this).CopyTo(array, 0);
				array[this.Count] = paperSource;
				this.array = array;
				return this.Count;
			}

			// Token: 0x04000C69 RID: 3177
			private PaperSource[] array;
		}

		/// <summary>Contains a collection of <see cref="T:System.Drawing.Printing.PrinterResolution" /> objects.</summary>
		// Token: 0x02000121 RID: 289
		public class PrinterResolutionCollection : ICollection, IEnumerable
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrinterSettings.PrinterResolutionCollection" /> class.</summary>
			/// <param name="array">An array of type <see cref="T:System.Drawing.Printing.PrinterResolution" />.</param>
			// Token: 0x06000F6B RID: 3947 RVA: 0x0002DBD9 File Offset: 0x0002BDD9
			public PrinterResolutionCollection(PrinterResolution[] array)
			{
				this.array = array;
			}

			/// <summary>Gets the number of available printer resolutions in the collection.</summary>
			/// <returns>The number of available printer resolutions in the collection.</returns>
			// Token: 0x170003F2 RID: 1010
			// (get) Token: 0x06000F6C RID: 3948 RVA: 0x0002DBE8 File Offset: 0x0002BDE8
			public int Count
			{
				get
				{
					return this.array.Length;
				}
			}

			/// <summary>Gets the <see cref="T:System.Drawing.Printing.PrinterResolution" /> at a specified index.</summary>
			/// <param name="index">The index of the <see cref="T:System.Drawing.Printing.PrinterResolution" /> to get.</param>
			/// <returns>The <see cref="T:System.Drawing.Printing.PrinterResolution" /> at the specified index.</returns>
			// Token: 0x170003F3 RID: 1011
			public virtual PrinterResolution this[int index]
			{
				get
				{
					return this.array[index];
				}
			}

			/// <summary>Returns an enumerator that can iterate through the collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Drawing.Printing.PrinterSettings.PrinterResolutionCollection" />.</returns>
			// Token: 0x06000F6E RID: 3950 RVA: 0x0002DBFC File Offset: 0x0002BDFC
			public IEnumerator GetEnumerator()
			{
				object[] array = this.array;
				return new PrinterSettings.ArrayEnumerator(array, 0, this.Count);
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.Count" />.</summary>
			// Token: 0x170003F4 RID: 1012
			// (get) Token: 0x06000F6F RID: 3951 RVA: 0x0002DC1D File Offset: 0x0002BE1D
			int ICollection.Count
			{
				get
				{
					return this.Count;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
			// Token: 0x170003F5 RID: 1013
			// (get) Token: 0x06000F70 RID: 3952 RVA: 0x0001E380 File Offset: 0x0001C580
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
			// Token: 0x170003F6 RID: 1014
			// (get) Token: 0x06000F71 RID: 3953 RVA: 0x0002DAD5 File Offset: 0x0002BCD5
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
			/// <param name="array">The destination array.</param>
			/// <param name="index">The index at which to start the copy operation.</param>
			// Token: 0x06000F72 RID: 3954 RVA: 0x0002DC25 File Offset: 0x0002BE25
			void ICollection.CopyTo(Array array, int index)
			{
				Array.Copy(this.array, index, array, 0, this.array.Length);
			}

			/// <summary>Copies the contents of the current <see cref="T:System.Drawing.Printing.PrinterSettings.PrinterResolutionCollection" /> to the specified array, starting at the specified index.</summary>
			/// <param name="printerResolutions">A zero-based array that receives the items copied from the <see cref="T:System.Drawing.Printing.PrinterSettings.PrinterResolutionCollection" />.</param>
			/// <param name="index">The index at which to start copying items.</param>
			// Token: 0x06000F73 RID: 3955 RVA: 0x0002DC25 File Offset: 0x0002BE25
			public void CopyTo(PrinterResolution[] printerResolutions, int index)
			{
				Array.Copy(this.array, index, printerResolutions, 0, this.array.Length);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
			// Token: 0x06000F74 RID: 3956 RVA: 0x0002DC3D File Offset: 0x0002BE3D
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			/// <summary>Adds a <see cref="T:System.Drawing.Printing.PrinterResolution" /> to the end of the collection.</summary>
			/// <param name="printerResolution">The <see cref="T:System.Drawing.Printing.PrinterResolution" /> to add to the collection.</param>
			/// <returns>The zero-based index of the newly added item.</returns>
			// Token: 0x06000F75 RID: 3957 RVA: 0x0002DC48 File Offset: 0x0002BE48
			[EditorBrowsable(EditorBrowsableState.Never)]
			public int Add(PrinterResolution printerResolution)
			{
				PrinterResolution[] array = new PrinterResolution[this.Count + 1];
				((ICollection)this).CopyTo(array, 0);
				array[this.Count] = printerResolution;
				this.array = array;
				return this.Count;
			}

			// Token: 0x04000C6A RID: 3178
			private PrinterResolution[] array;
		}

		/// <summary>Contains a collection of <see cref="T:System.String" /> objects.</summary>
		// Token: 0x02000122 RID: 290
		public class StringCollection : ICollection, IEnumerable
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrinterSettings.StringCollection" /> class.</summary>
			/// <param name="array">An array of type <see cref="T:System.String" />.</param>
			// Token: 0x06000F76 RID: 3958 RVA: 0x0002DC81 File Offset: 0x0002BE81
			public StringCollection(string[] array)
			{
				this.array = array;
			}

			/// <summary>Gets the number of strings in the collection.</summary>
			/// <returns>The number of strings in the collection.</returns>
			// Token: 0x170003F7 RID: 1015
			// (get) Token: 0x06000F77 RID: 3959 RVA: 0x0002DC90 File Offset: 0x0002BE90
			public int Count
			{
				get
				{
					return this.array.Length;
				}
			}

			/// <summary>Gets the <see cref="T:System.String" /> at a specified index.</summary>
			/// <param name="index">The index of the <see cref="T:System.String" /> to get.</param>
			/// <returns>The <see cref="T:System.String" /> at the specified index.</returns>
			// Token: 0x170003F8 RID: 1016
			public virtual string this[int index]
			{
				get
				{
					return this.array[index];
				}
			}

			/// <summary>Returns an enumerator that can iterate through the collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Drawing.Printing.PrinterSettings.StringCollection" />.</returns>
			// Token: 0x06000F79 RID: 3961 RVA: 0x0002DCA4 File Offset: 0x0002BEA4
			public IEnumerator GetEnumerator()
			{
				object[] array = this.array;
				return new PrinterSettings.ArrayEnumerator(array, 0, this.Count);
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.Count" />.</summary>
			// Token: 0x170003F9 RID: 1017
			// (get) Token: 0x06000F7A RID: 3962 RVA: 0x0002DCC5 File Offset: 0x0002BEC5
			int ICollection.Count
			{
				get
				{
					return this.Count;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
			// Token: 0x170003FA RID: 1018
			// (get) Token: 0x06000F7B RID: 3963 RVA: 0x0001E380 File Offset: 0x0001C580
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
			// Token: 0x170003FB RID: 1019
			// (get) Token: 0x06000F7C RID: 3964 RVA: 0x0002DAD5 File Offset: 0x0002BCD5
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
			/// <param name="array">The array for items to be copied to.</param>
			/// <param name="index">The starting index.</param>
			// Token: 0x06000F7D RID: 3965 RVA: 0x0002DCCD File Offset: 0x0002BECD
			void ICollection.CopyTo(Array array, int index)
			{
				Array.Copy(this.array, index, array, 0, this.array.Length);
			}

			/// <summary>Copies the contents of the current <see cref="T:System.Drawing.Printing.PrinterSettings.PrinterResolutionCollection" /> to the specified array, starting at the specified index</summary>
			/// <param name="strings">A zero-based array that receives the items copied from the <see cref="T:System.Drawing.Printing.PrinterSettings.StringCollection" />.</param>
			/// <param name="index">The index at which to start copying items.</param>
			// Token: 0x06000F7E RID: 3966 RVA: 0x0002DCCD File Offset: 0x0002BECD
			public void CopyTo(string[] strings, int index)
			{
				Array.Copy(this.array, index, strings, 0, this.array.Length);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
			// Token: 0x06000F7F RID: 3967 RVA: 0x0002DCE5 File Offset: 0x0002BEE5
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			/// <summary>Adds a string to the end of the collection.</summary>
			/// <param name="value">The string to add to the collection.</param>
			/// <returns>The zero-based index of the newly added item.</returns>
			// Token: 0x06000F80 RID: 3968 RVA: 0x0002DCF0 File Offset: 0x0002BEF0
			[EditorBrowsable(EditorBrowsableState.Never)]
			public int Add(string value)
			{
				string[] array = new string[this.Count + 1];
				((ICollection)this).CopyTo(array, 0);
				array[this.Count] = value;
				this.array = array;
				return this.Count;
			}

			// Token: 0x04000C6B RID: 3179
			private string[] array;
		}

		// Token: 0x02000123 RID: 291
		private class ArrayEnumerator : IEnumerator
		{
			// Token: 0x06000F81 RID: 3969 RVA: 0x0002DD29 File Offset: 0x0002BF29
			public ArrayEnumerator(object[] array, int startIndex, int count)
			{
				this.array = array;
				this.startIndex = startIndex;
				this.endIndex = this.index + count;
				this.index = this.startIndex;
			}

			// Token: 0x170003FC RID: 1020
			// (get) Token: 0x06000F82 RID: 3970 RVA: 0x0002DD59 File Offset: 0x0002BF59
			public object Current
			{
				get
				{
					return this.item;
				}
			}

			// Token: 0x06000F83 RID: 3971 RVA: 0x0002DD64 File Offset: 0x0002BF64
			public bool MoveNext()
			{
				if (this.index >= this.endIndex)
				{
					return false;
				}
				object[] array = this.array;
				int num = this.index;
				this.index = num + 1;
				this.item = array[num];
				return true;
			}

			// Token: 0x06000F84 RID: 3972 RVA: 0x0002DDA0 File Offset: 0x0002BFA0
			public void Reset()
			{
				this.index = this.startIndex;
				this.item = null;
			}

			// Token: 0x04000C6C RID: 3180
			private object[] array;

			// Token: 0x04000C6D RID: 3181
			private object item;

			// Token: 0x04000C6E RID: 3182
			private int index;

			// Token: 0x04000C6F RID: 3183
			private int startIndex;

			// Token: 0x04000C70 RID: 3184
			private int endIndex;
		}
	}
}
