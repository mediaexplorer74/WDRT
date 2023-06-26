using System;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Windows.Forms
{
	/// <summary>Lets users select a printer and choose which sections of the document to print from a Windows Forms application.</summary>
	// Token: 0x02000449 RID: 1097
	[DefaultProperty("Document")]
	[SRDescription("DescriptionPrintDialog")]
	[Designer("System.Windows.Forms.Design.PrintDialogDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public sealed class PrintDialog : CommonDialog
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PrintDialog" /> class.</summary>
		// Token: 0x06004C26 RID: 19494 RVA: 0x000AFA7F File Offset: 0x000ADC7F
		public PrintDialog()
		{
			this.Reset();
		}

		/// <summary>Gets or sets a value indicating whether the Current Page option button is displayed.</summary>
		/// <returns>
		///   <see langword="true" /> if the Current Page option button is displayed; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170012A3 RID: 4771
		// (get) Token: 0x06004C27 RID: 19495 RVA: 0x0013BFD5 File Offset: 0x0013A1D5
		// (set) Token: 0x06004C28 RID: 19496 RVA: 0x0013BFDD File Offset: 0x0013A1DD
		[DefaultValue(false)]
		[SRDescription("PDallowCurrentPageDescr")]
		public bool AllowCurrentPage
		{
			get
			{
				return this.allowCurrentPage;
			}
			set
			{
				this.allowCurrentPage = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the Pages option button is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if the Pages option button is enabled; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170012A4 RID: 4772
		// (get) Token: 0x06004C29 RID: 19497 RVA: 0x0013BFE6 File Offset: 0x0013A1E6
		// (set) Token: 0x06004C2A RID: 19498 RVA: 0x0013BFEE File Offset: 0x0013A1EE
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("PDallowPagesDescr")]
		public bool AllowSomePages
		{
			get
			{
				return this.allowPages;
			}
			set
			{
				this.allowPages = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the Print to file check box is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if the Print to file check box is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170012A5 RID: 4773
		// (get) Token: 0x06004C2B RID: 19499 RVA: 0x0013BFF7 File Offset: 0x0013A1F7
		// (set) Token: 0x06004C2C RID: 19500 RVA: 0x0013BFFF File Offset: 0x0013A1FF
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("PDallowPrintToFileDescr")]
		public bool AllowPrintToFile
		{
			get
			{
				return this.allowPrintToFile;
			}
			set
			{
				this.allowPrintToFile = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the Selection option button is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if the Selection option button is enabled; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170012A6 RID: 4774
		// (get) Token: 0x06004C2D RID: 19501 RVA: 0x0013C008 File Offset: 0x0013A208
		// (set) Token: 0x06004C2E RID: 19502 RVA: 0x0013C010 File Offset: 0x0013A210
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("PDallowSelectionDescr")]
		public bool AllowSelection
		{
			get
			{
				return this.allowSelection;
			}
			set
			{
				this.allowSelection = value;
			}
		}

		/// <summary>Gets or sets a value indicating the <see cref="T:System.Drawing.Printing.PrintDocument" /> used to obtain <see cref="T:System.Drawing.Printing.PrinterSettings" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Printing.PrintDocument" /> used to obtain <see cref="T:System.Drawing.Printing.PrinterSettings" />. The default is <see langword="null" />.</returns>
		// Token: 0x170012A7 RID: 4775
		// (get) Token: 0x06004C2F RID: 19503 RVA: 0x0013C019 File Offset: 0x0013A219
		// (set) Token: 0x06004C30 RID: 19504 RVA: 0x0013C021 File Offset: 0x0013A221
		[SRCategory("CatData")]
		[DefaultValue(null)]
		[SRDescription("PDdocumentDescr")]
		public PrintDocument Document
		{
			get
			{
				return this.printDocument;
			}
			set
			{
				this.printDocument = value;
				if (this.printDocument == null)
				{
					this.settings = new PrinterSettings();
					return;
				}
				this.settings = this.printDocument.PrinterSettings;
			}
		}

		// Token: 0x170012A8 RID: 4776
		// (get) Token: 0x06004C31 RID: 19505 RVA: 0x0013C04F File Offset: 0x0013A24F
		private PageSettings PageSettings
		{
			get
			{
				if (this.Document == null)
				{
					return this.PrinterSettings.DefaultPageSettings;
				}
				return this.Document.DefaultPageSettings;
			}
		}

		/// <summary>Gets or sets the printer settings the dialog box modifies.</summary>
		/// <returns>The <see cref="T:System.Drawing.Printing.PrinterSettings" /> the dialog box modifies.</returns>
		// Token: 0x170012A9 RID: 4777
		// (get) Token: 0x06004C32 RID: 19506 RVA: 0x0013C070 File Offset: 0x0013A270
		// (set) Token: 0x06004C33 RID: 19507 RVA: 0x0013C08B File Offset: 0x0013A28B
		[SRCategory("CatData")]
		[DefaultValue(null)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("PDprinterSettingsDescr")]
		public PrinterSettings PrinterSettings
		{
			get
			{
				if (this.settings == null)
				{
					this.settings = new PrinterSettings();
				}
				return this.settings;
			}
			set
			{
				if (value != this.PrinterSettings)
				{
					this.settings = value;
					this.printDocument = null;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the Print to file check box is selected.</summary>
		/// <returns>
		///   <see langword="true" /> if the Print to file check box is selected; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170012AA RID: 4778
		// (get) Token: 0x06004C34 RID: 19508 RVA: 0x0013C0A4 File Offset: 0x0013A2A4
		// (set) Token: 0x06004C35 RID: 19509 RVA: 0x0013C0AC File Offset: 0x0013A2AC
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("PDprintToFileDescr")]
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

		/// <summary>Gets or sets a value indicating whether the Help button is displayed.</summary>
		/// <returns>
		///   <see langword="true" /> if the Help button is displayed; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170012AB RID: 4779
		// (get) Token: 0x06004C36 RID: 19510 RVA: 0x0013C0B5 File Offset: 0x0013A2B5
		// (set) Token: 0x06004C37 RID: 19511 RVA: 0x0013C0BD File Offset: 0x0013A2BD
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("PDshowHelpDescr")]
		public bool ShowHelp
		{
			get
			{
				return this.showHelp;
			}
			set
			{
				this.showHelp = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the Network button is displayed.</summary>
		/// <returns>
		///   <see langword="true" /> if the Network button is displayed; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170012AC RID: 4780
		// (get) Token: 0x06004C38 RID: 19512 RVA: 0x0013C0C6 File Offset: 0x0013A2C6
		// (set) Token: 0x06004C39 RID: 19513 RVA: 0x0013C0CE File Offset: 0x0013A2CE
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("PDshowNetworkDescr")]
		public bool ShowNetwork
		{
			get
			{
				return this.showNetwork;
			}
			set
			{
				this.showNetwork = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog should be shown in the Windows XP style for systems running Windows XP Home Edition, Windows XP Professional, Windows Server 2003 or later.</summary>
		/// <returns>
		///   <see langword="true" /> to indicate the dialog should be shown with the Windows XP style, otherwise <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170012AD RID: 4781
		// (get) Token: 0x06004C3A RID: 19514 RVA: 0x0013C0D7 File Offset: 0x0013A2D7
		// (set) Token: 0x06004C3B RID: 19515 RVA: 0x0013C0DF File Offset: 0x0013A2DF
		[DefaultValue(false)]
		[SRDescription("PDuseEXDialog")]
		public bool UseEXDialog
		{
			get
			{
				return this.useEXDialog;
			}
			set
			{
				this.useEXDialog = value;
			}
		}

		// Token: 0x06004C3C RID: 19516 RVA: 0x0013C0E8 File Offset: 0x0013A2E8
		private int GetFlags()
		{
			int num = 0;
			if (!this.UseEXDialog || Environment.OSVersion.Platform != PlatformID.Win32NT || Environment.OSVersion.Version.Major < 5)
			{
				num |= 4096;
			}
			if (!this.allowCurrentPage)
			{
				num |= 8388608;
			}
			if (!this.allowPages)
			{
				num |= 8;
			}
			if (!this.allowPrintToFile)
			{
				num |= 524288;
			}
			if (!this.allowSelection)
			{
				num |= 4;
			}
			num |= (int)this.PrinterSettings.PrintRange;
			if (this.printToFile)
			{
				num |= 32;
			}
			if (this.showHelp)
			{
				num |= 2048;
			}
			if (!this.showNetwork)
			{
				num |= 2097152;
			}
			if (this.PrinterSettings.Collate)
			{
				num |= 16;
			}
			return num;
		}

		/// <summary>Resets all options, the last selected printer, and the page settings to their default values.</summary>
		// Token: 0x06004C3D RID: 19517 RVA: 0x0013C1AC File Offset: 0x0013A3AC
		public override void Reset()
		{
			this.allowCurrentPage = false;
			this.allowPages = false;
			this.allowPrintToFile = true;
			this.allowSelection = false;
			this.printDocument = null;
			this.printToFile = false;
			this.settings = null;
			this.showHelp = false;
			this.showNetwork = true;
		}

		// Token: 0x06004C3E RID: 19518 RVA: 0x0013C1F8 File Offset: 0x0013A3F8
		internal static NativeMethods.PRINTDLG CreatePRINTDLG()
		{
			NativeMethods.PRINTDLG printdlg;
			if (IntPtr.Size == 4)
			{
				printdlg = new NativeMethods.PRINTDLG_32();
			}
			else
			{
				printdlg = new NativeMethods.PRINTDLG_64();
			}
			printdlg.lStructSize = Marshal.SizeOf(printdlg);
			printdlg.hwndOwner = IntPtr.Zero;
			printdlg.hDevMode = IntPtr.Zero;
			printdlg.hDevNames = IntPtr.Zero;
			printdlg.Flags = 0;
			printdlg.hDC = IntPtr.Zero;
			printdlg.nFromPage = 1;
			printdlg.nToPage = 1;
			printdlg.nMinPage = 0;
			printdlg.nMaxPage = 9999;
			printdlg.nCopies = 1;
			printdlg.hInstance = IntPtr.Zero;
			printdlg.lCustData = IntPtr.Zero;
			printdlg.lpfnPrintHook = null;
			printdlg.lpfnSetupHook = null;
			printdlg.lpPrintTemplateName = null;
			printdlg.lpSetupTemplateName = null;
			printdlg.hPrintTemplate = IntPtr.Zero;
			printdlg.hSetupTemplate = IntPtr.Zero;
			return printdlg;
		}

		// Token: 0x06004C3F RID: 19519 RVA: 0x0013C2CC File Offset: 0x0013A4CC
		internal static NativeMethods.PRINTDLGEX CreatePRINTDLGEX()
		{
			NativeMethods.PRINTDLGEX printdlgex = new NativeMethods.PRINTDLGEX();
			printdlgex.lStructSize = Marshal.SizeOf(printdlgex);
			printdlgex.hwndOwner = IntPtr.Zero;
			printdlgex.hDevMode = IntPtr.Zero;
			printdlgex.hDevNames = IntPtr.Zero;
			printdlgex.hDC = IntPtr.Zero;
			printdlgex.Flags = 0;
			printdlgex.Flags2 = 0;
			printdlgex.ExclusionFlags = 0;
			printdlgex.nPageRanges = 0;
			printdlgex.nMaxPageRanges = 1;
			printdlgex.pageRanges = UnsafeNativeMethods.GlobalAlloc(64, printdlgex.nMaxPageRanges * Marshal.SizeOf(typeof(NativeMethods.PRINTPAGERANGE)));
			printdlgex.nMinPage = 0;
			printdlgex.nMaxPage = 9999;
			printdlgex.nCopies = 1;
			printdlgex.hInstance = IntPtr.Zero;
			printdlgex.lpPrintTemplateName = null;
			printdlgex.nPropertyPages = 0;
			printdlgex.lphPropertyPages = IntPtr.Zero;
			printdlgex.nStartPage = NativeMethods.START_PAGE_GENERAL;
			printdlgex.dwResultAction = 0;
			return printdlgex;
		}

		// Token: 0x06004C40 RID: 19520 RVA: 0x0013C3B0 File Offset: 0x0013A5B0
		protected override bool RunDialog(IntPtr hwndOwner)
		{
			IntSecurity.SafePrinting.Demand();
			NativeMethods.WndProc wndProc = new NativeMethods.WndProc(this.HookProc);
			bool flag;
			if (!this.UseEXDialog || Environment.OSVersion.Platform != PlatformID.Win32NT || Environment.OSVersion.Version.Major < 5)
			{
				NativeMethods.PRINTDLG printdlg = PrintDialog.CreatePRINTDLG();
				flag = this.ShowPrintDialog(hwndOwner, wndProc, printdlg);
			}
			else
			{
				NativeMethods.PRINTDLGEX printdlgex = PrintDialog.CreatePRINTDLGEX();
				flag = this.ShowPrintDialog(hwndOwner, printdlgex);
			}
			return flag;
		}

		// Token: 0x06004C41 RID: 19521 RVA: 0x0013C420 File Offset: 0x0013A620
		private bool ShowPrintDialog(IntPtr hwndOwner, NativeMethods.WndProc hookProcPtr, NativeMethods.PRINTDLG data)
		{
			data.Flags = this.GetFlags();
			data.nCopies = this.PrinterSettings.Copies;
			data.hwndOwner = hwndOwner;
			data.lpfnPrintHook = hookProcPtr;
			IntSecurity.AllPrintingAndUnmanagedCode.Assert();
			try
			{
				if (this.PageSettings == null)
				{
					data.hDevMode = this.PrinterSettings.GetHdevmode();
				}
				else
				{
					data.hDevMode = this.PrinterSettings.GetHdevmode(this.PageSettings);
				}
				data.hDevNames = this.PrinterSettings.GetHdevnames();
			}
			catch (InvalidPrinterException)
			{
				data.hDevMode = IntPtr.Zero;
				data.hDevNames = IntPtr.Zero;
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			bool flag;
			try
			{
				if (this.AllowSomePages)
				{
					if (this.PrinterSettings.FromPage < this.PrinterSettings.MinimumPage || this.PrinterSettings.FromPage > this.PrinterSettings.MaximumPage)
					{
						throw new ArgumentException(SR.GetString("PDpageOutOfRange", new object[] { "FromPage" }));
					}
					if (this.PrinterSettings.ToPage < this.PrinterSettings.MinimumPage || this.PrinterSettings.ToPage > this.PrinterSettings.MaximumPage)
					{
						throw new ArgumentException(SR.GetString("PDpageOutOfRange", new object[] { "ToPage" }));
					}
					if (this.PrinterSettings.ToPage < this.PrinterSettings.FromPage)
					{
						throw new ArgumentException(SR.GetString("PDpageOutOfRange", new object[] { "FromPage" }));
					}
					data.nFromPage = (short)this.PrinterSettings.FromPage;
					data.nToPage = (short)this.PrinterSettings.ToPage;
					data.nMinPage = (short)this.PrinterSettings.MinimumPage;
					data.nMaxPage = (short)this.PrinterSettings.MaximumPage;
				}
				if (!UnsafeNativeMethods.PrintDlg(data))
				{
					flag = false;
				}
				else
				{
					IntSecurity.AllPrintingAndUnmanagedCode.Assert();
					try
					{
						PrintDialog.UpdatePrinterSettings(data.hDevMode, data.hDevNames, data.nCopies, data.Flags, this.settings, this.PageSettings);
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
					this.PrintToFile = (data.Flags & 32) != 0;
					this.PrinterSettings.PrintToFile = this.PrintToFile;
					if (this.AllowSomePages)
					{
						this.PrinterSettings.FromPage = (int)data.nFromPage;
						this.PrinterSettings.ToPage = (int)data.nToPage;
					}
					if ((data.Flags & 262144) == 0 && Environment.OSVersion.Version.Major >= 6)
					{
						this.PrinterSettings.Copies = data.nCopies;
						this.PrinterSettings.Collate = (data.Flags & 16) == 16;
					}
					flag = true;
				}
			}
			finally
			{
				UnsafeNativeMethods.GlobalFree(new HandleRef(data, data.hDevMode));
				UnsafeNativeMethods.GlobalFree(new HandleRef(data, data.hDevNames));
			}
			return flag;
		}

		// Token: 0x06004C42 RID: 19522 RVA: 0x0013C75C File Offset: 0x0013A95C
		private unsafe bool ShowPrintDialog(IntPtr hwndOwner, NativeMethods.PRINTDLGEX data)
		{
			data.Flags = this.GetFlags();
			data.nCopies = (int)this.PrinterSettings.Copies;
			data.hwndOwner = hwndOwner;
			IntSecurity.AllPrintingAndUnmanagedCode.Assert();
			try
			{
				if (this.PageSettings == null)
				{
					data.hDevMode = this.PrinterSettings.GetHdevmode();
				}
				else
				{
					data.hDevMode = this.PrinterSettings.GetHdevmode(this.PageSettings);
				}
				data.hDevNames = this.PrinterSettings.GetHdevnames();
			}
			catch (InvalidPrinterException)
			{
				data.hDevMode = IntPtr.Zero;
				data.hDevNames = IntPtr.Zero;
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			bool flag;
			try
			{
				if (this.AllowSomePages)
				{
					if (this.PrinterSettings.FromPage < this.PrinterSettings.MinimumPage || this.PrinterSettings.FromPage > this.PrinterSettings.MaximumPage)
					{
						throw new ArgumentException(SR.GetString("PDpageOutOfRange", new object[] { "FromPage" }));
					}
					if (this.PrinterSettings.ToPage < this.PrinterSettings.MinimumPage || this.PrinterSettings.ToPage > this.PrinterSettings.MaximumPage)
					{
						throw new ArgumentException(SR.GetString("PDpageOutOfRange", new object[] { "ToPage" }));
					}
					if (this.PrinterSettings.ToPage < this.PrinterSettings.FromPage)
					{
						throw new ArgumentException(SR.GetString("PDpageOutOfRange", new object[] { "FromPage" }));
					}
					int* ptr = (int*)(void*)data.pageRanges;
					*ptr = this.PrinterSettings.FromPage;
					ptr++;
					*ptr = this.PrinterSettings.ToPage;
					data.nPageRanges = 1;
					data.nMinPage = this.PrinterSettings.MinimumPage;
					data.nMaxPage = this.PrinterSettings.MaximumPage;
				}
				data.Flags &= -2099201;
				int num = UnsafeNativeMethods.PrintDlgEx(data);
				if (NativeMethods.Failed(num) || data.dwResultAction == 0)
				{
					flag = false;
				}
				else
				{
					IntSecurity.AllPrintingAndUnmanagedCode.Assert();
					try
					{
						PrintDialog.UpdatePrinterSettings(data.hDevMode, data.hDevNames, (short)data.nCopies, data.Flags, this.PrinterSettings, this.PageSettings);
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
					this.PrintToFile = (data.Flags & 32) != 0;
					this.PrinterSettings.PrintToFile = this.PrintToFile;
					if (this.AllowSomePages)
					{
						int* ptr2 = (int*)(void*)data.pageRanges;
						this.PrinterSettings.FromPage = *ptr2;
						ptr2++;
						this.PrinterSettings.ToPage = *ptr2;
					}
					if ((data.Flags & 262144) == 0 && Environment.OSVersion.Version.Major >= 6)
					{
						this.PrinterSettings.Copies = (short)data.nCopies;
						this.PrinterSettings.Collate = (data.Flags & 16) == 16;
					}
					flag = data.dwResultAction == 1;
				}
			}
			finally
			{
				if (data.hDevMode != IntPtr.Zero)
				{
					UnsafeNativeMethods.GlobalFree(new HandleRef(data, data.hDevMode));
				}
				if (data.hDevNames != IntPtr.Zero)
				{
					UnsafeNativeMethods.GlobalFree(new HandleRef(data, data.hDevNames));
				}
				if (data.pageRanges != IntPtr.Zero)
				{
					UnsafeNativeMethods.GlobalFree(new HandleRef(data, data.pageRanges));
				}
			}
			return flag;
		}

		// Token: 0x06004C43 RID: 19523 RVA: 0x0013CB18 File Offset: 0x0013AD18
		private static void UpdatePrinterSettings(IntPtr hDevMode, IntPtr hDevNames, short copies, int flags, PrinterSettings settings, PageSettings pageSettings)
		{
			settings.SetHdevmode(hDevMode);
			settings.SetHdevnames(hDevNames);
			if (pageSettings != null)
			{
				pageSettings.SetHdevmode(hDevMode);
			}
			if (settings.Copies == 1)
			{
				settings.Copies = copies;
			}
			settings.PrintRange = (PrintRange)(flags & 4194307);
		}

		// Token: 0x04002873 RID: 10355
		private const int printRangeMask = 4194307;

		// Token: 0x04002874 RID: 10356
		private PrinterSettings settings;

		// Token: 0x04002875 RID: 10357
		private PrintDocument printDocument;

		// Token: 0x04002876 RID: 10358
		private bool allowCurrentPage;

		// Token: 0x04002877 RID: 10359
		private bool allowPages;

		// Token: 0x04002878 RID: 10360
		private bool allowPrintToFile;

		// Token: 0x04002879 RID: 10361
		private bool allowSelection;

		// Token: 0x0400287A RID: 10362
		private bool printToFile;

		// Token: 0x0400287B RID: 10363
		private bool showHelp;

		// Token: 0x0400287C RID: 10364
		private bool showNetwork;

		// Token: 0x0400287D RID: 10365
		private bool useEXDialog;
	}
}
