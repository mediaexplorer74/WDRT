using System;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>Enables users to change page-related print settings, including margins and paper orientation. This class cannot be inherited.</summary>
	// Token: 0x02000447 RID: 1095
	[DefaultProperty("Document")]
	[SRDescription("DescriptionPageSetupDialog")]
	public sealed class PageSetupDialog : CommonDialog
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PageSetupDialog" /> class.</summary>
		// Token: 0x06004C02 RID: 19458 RVA: 0x000AFA7F File Offset: 0x000ADC7F
		public PageSetupDialog()
		{
			this.Reset();
		}

		/// <summary>Gets or sets a value indicating whether the margins section of the dialog box is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if the margins section of the dialog box is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17001297 RID: 4759
		// (get) Token: 0x06004C03 RID: 19459 RVA: 0x0013B8B0 File Offset: 0x00139AB0
		// (set) Token: 0x06004C04 RID: 19460 RVA: 0x0013B8B8 File Offset: 0x00139AB8
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("PSDallowMarginsDescr")]
		public bool AllowMargins
		{
			get
			{
				return this.allowMargins;
			}
			set
			{
				this.allowMargins = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the orientation section of the dialog box (landscape versus portrait) is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if the orientation section of the dialog box is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17001298 RID: 4760
		// (get) Token: 0x06004C05 RID: 19461 RVA: 0x0013B8C1 File Offset: 0x00139AC1
		// (set) Token: 0x06004C06 RID: 19462 RVA: 0x0013B8C9 File Offset: 0x00139AC9
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("PSDallowOrientationDescr")]
		public bool AllowOrientation
		{
			get
			{
				return this.allowOrientation;
			}
			set
			{
				this.allowOrientation = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the paper section of the dialog box (paper size and paper source) is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if the paper section of the dialog box is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17001299 RID: 4761
		// (get) Token: 0x06004C07 RID: 19463 RVA: 0x0013B8D2 File Offset: 0x00139AD2
		// (set) Token: 0x06004C08 RID: 19464 RVA: 0x0013B8DA File Offset: 0x00139ADA
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("PSDallowPaperDescr")]
		public bool AllowPaper
		{
			get
			{
				return this.allowPaper;
			}
			set
			{
				this.allowPaper = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the Printer button is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if the Printer button is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x1700129A RID: 4762
		// (get) Token: 0x06004C09 RID: 19465 RVA: 0x0013B8E3 File Offset: 0x00139AE3
		// (set) Token: 0x06004C0A RID: 19466 RVA: 0x0013B8EB File Offset: 0x00139AEB
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("PSDallowPrinterDescr")]
		public bool AllowPrinter
		{
			get
			{
				return this.allowPrinter;
			}
			set
			{
				this.allowPrinter = value;
			}
		}

		/// <summary>Gets or sets a value indicating the <see cref="T:System.Drawing.Printing.PrintDocument" /> to get page settings from.</summary>
		/// <returns>The <see cref="T:System.Drawing.Printing.PrintDocument" /> to get page settings from. The default is <see langword="null" />.</returns>
		// Token: 0x1700129B RID: 4763
		// (get) Token: 0x06004C0B RID: 19467 RVA: 0x0013B8F4 File Offset: 0x00139AF4
		// (set) Token: 0x06004C0C RID: 19468 RVA: 0x0013B8FC File Offset: 0x00139AFC
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
				if (this.printDocument != null)
				{
					this.pageSettings = this.printDocument.DefaultPageSettings;
					this.printerSettings = this.printDocument.PrinterSettings;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the margin settings, when displayed in millimeters, should be automatically converted to and from hundredths of an inch.</summary>
		/// <returns>
		///   <see langword="true" /> if the margins should be automatically converted; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x1700129C RID: 4764
		// (get) Token: 0x06004C0D RID: 19469 RVA: 0x0013B92F File Offset: 0x00139B2F
		// (set) Token: 0x06004C0E RID: 19470 RVA: 0x0013B937 File Offset: 0x00139B37
		[DefaultValue(false)]
		[SRDescription("PSDenableMetricDescr")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public bool EnableMetric
		{
			get
			{
				return this.enableMetric;
			}
			set
			{
				this.enableMetric = value;
			}
		}

		/// <summary>Gets or sets a value indicating the minimum margins, in hundredths of an inch, the user is allowed to select.</summary>
		/// <returns>The minimum margins, in hundredths of an inch, the user is allowed to select. The default is <see langword="null" />.</returns>
		// Token: 0x1700129D RID: 4765
		// (get) Token: 0x06004C0F RID: 19471 RVA: 0x0013B940 File Offset: 0x00139B40
		// (set) Token: 0x06004C10 RID: 19472 RVA: 0x0013B948 File Offset: 0x00139B48
		[SRCategory("CatData")]
		[SRDescription("PSDminMarginsDescr")]
		public Margins MinMargins
		{
			get
			{
				return this.minMargins;
			}
			set
			{
				if (value == null)
				{
					value = new Margins(0, 0, 0, 0);
				}
				this.minMargins = value;
			}
		}

		/// <summary>Gets or sets a value indicating the page settings to modify.</summary>
		/// <returns>The <see cref="T:System.Drawing.Printing.PageSettings" /> to modify. The default is <see langword="null" />.</returns>
		// Token: 0x1700129E RID: 4766
		// (get) Token: 0x06004C11 RID: 19473 RVA: 0x0013B965 File Offset: 0x00139B65
		// (set) Token: 0x06004C12 RID: 19474 RVA: 0x0013B96D File Offset: 0x00139B6D
		[SRCategory("CatData")]
		[DefaultValue(null)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("PSDpageSettingsDescr")]
		public PageSettings PageSettings
		{
			get
			{
				return this.pageSettings;
			}
			set
			{
				this.pageSettings = value;
				this.printDocument = null;
			}
		}

		/// <summary>Gets or sets the printer settings that are modified when the user clicks the Printer button in the dialog.</summary>
		/// <returns>The <see cref="T:System.Drawing.Printing.PrinterSettings" /> to modify when the user clicks the Printer button. The default is <see langword="null" />.</returns>
		// Token: 0x1700129F RID: 4767
		// (get) Token: 0x06004C13 RID: 19475 RVA: 0x0013B97D File Offset: 0x00139B7D
		// (set) Token: 0x06004C14 RID: 19476 RVA: 0x0013B985 File Offset: 0x00139B85
		[SRCategory("CatData")]
		[DefaultValue(null)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("PSDprinterSettingsDescr")]
		public PrinterSettings PrinterSettings
		{
			get
			{
				return this.printerSettings;
			}
			set
			{
				this.printerSettings = value;
				this.printDocument = null;
			}
		}

		/// <summary>Gets or sets a value indicating whether the Help button is visible.</summary>
		/// <returns>
		///   <see langword="true" /> if the Help button is visible; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170012A0 RID: 4768
		// (get) Token: 0x06004C15 RID: 19477 RVA: 0x0013B995 File Offset: 0x00139B95
		// (set) Token: 0x06004C16 RID: 19478 RVA: 0x0013B99D File Offset: 0x00139B9D
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("PSDshowHelpDescr")]
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

		/// <summary>Gets or sets a value indicating whether the Network button is visible.</summary>
		/// <returns>
		///   <see langword="true" /> if the Network button is visible; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170012A1 RID: 4769
		// (get) Token: 0x06004C17 RID: 19479 RVA: 0x0013B9A6 File Offset: 0x00139BA6
		// (set) Token: 0x06004C18 RID: 19480 RVA: 0x0013B9AE File Offset: 0x00139BAE
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("PSDshowNetworkDescr")]
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

		// Token: 0x06004C19 RID: 19481 RVA: 0x0013B9B8 File Offset: 0x00139BB8
		private int GetFlags()
		{
			int num = 0;
			num |= 8192;
			if (!this.allowMargins)
			{
				num |= 16;
			}
			if (!this.allowOrientation)
			{
				num |= 256;
			}
			if (!this.allowPaper)
			{
				num |= 512;
			}
			if (!this.allowPrinter || this.printerSettings == null)
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
			if (this.minMargins != null)
			{
				num |= 1;
			}
			if (this.pageSettings.Margins != null)
			{
				num |= 2;
			}
			return num;
		}

		/// <summary>Resets all options to their default values.</summary>
		// Token: 0x06004C1A RID: 19482 RVA: 0x0013BA5C File Offset: 0x00139C5C
		public override void Reset()
		{
			this.allowMargins = true;
			this.allowOrientation = true;
			this.allowPaper = true;
			this.allowPrinter = true;
			this.MinMargins = null;
			this.pageSettings = null;
			this.printDocument = null;
			this.printerSettings = null;
			this.showHelp = false;
			this.showNetwork = true;
		}

		// Token: 0x06004C1B RID: 19483 RVA: 0x0013BAAF File Offset: 0x00139CAF
		private void ResetMinMargins()
		{
			this.MinMargins = null;
		}

		// Token: 0x06004C1C RID: 19484 RVA: 0x0013BAB8 File Offset: 0x00139CB8
		private bool ShouldSerializeMinMargins()
		{
			return this.minMargins.Left != 0 || this.minMargins.Right != 0 || this.minMargins.Top != 0 || this.minMargins.Bottom != 0;
		}

		// Token: 0x06004C1D RID: 19485 RVA: 0x0013BAF4 File Offset: 0x00139CF4
		private static void UpdateSettings(NativeMethods.PAGESETUPDLG data, PageSettings pageSettings, PrinterSettings printerSettings)
		{
			IntSecurity.AllPrintingAndUnmanagedCode.Assert();
			try
			{
				pageSettings.SetHdevmode(data.hDevMode);
				if (printerSettings != null)
				{
					printerSettings.SetHdevmode(data.hDevMode);
					printerSettings.SetHdevnames(data.hDevNames);
				}
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			Margins margins = new Margins();
			margins.Left = data.marginLeft;
			margins.Top = data.marginTop;
			margins.Right = data.marginRight;
			margins.Bottom = data.marginBottom;
			PrinterUnit printerUnit = (((data.Flags & 8) != 0) ? PrinterUnit.HundredthsOfAMillimeter : PrinterUnit.ThousandthsOfAnInch);
			pageSettings.Margins = PrinterUnitConvert.Convert(margins, printerUnit, PrinterUnit.Display);
		}

		// Token: 0x06004C1E RID: 19486 RVA: 0x0013BBA0 File Offset: 0x00139DA0
		protected override bool RunDialog(IntPtr hwndOwner)
		{
			IntSecurity.SafePrinting.Demand();
			NativeMethods.WndProc wndProc = new NativeMethods.WndProc(this.HookProc);
			if (this.pageSettings == null)
			{
				throw new ArgumentException(SR.GetString("PSDcantShowWithoutPage"));
			}
			NativeMethods.PAGESETUPDLG pagesetupdlg = new NativeMethods.PAGESETUPDLG();
			pagesetupdlg.lStructSize = Marshal.SizeOf(pagesetupdlg);
			pagesetupdlg.Flags = this.GetFlags();
			pagesetupdlg.hwndOwner = hwndOwner;
			pagesetupdlg.lpfnPageSetupHook = wndProc;
			PrinterUnit printerUnit = PrinterUnit.ThousandthsOfAnInch;
			if (this.EnableMetric)
			{
				StringBuilder stringBuilder = new StringBuilder(2);
				int localeInfo = UnsafeNativeMethods.GetLocaleInfo(NativeMethods.LOCALE_USER_DEFAULT, 13, stringBuilder, stringBuilder.Capacity);
				if (localeInfo > 0 && int.Parse(stringBuilder.ToString(), CultureInfo.InvariantCulture) == 0)
				{
					printerUnit = PrinterUnit.HundredthsOfAMillimeter;
				}
			}
			if (this.MinMargins != null)
			{
				Margins margins = PrinterUnitConvert.Convert(this.MinMargins, PrinterUnit.Display, printerUnit);
				pagesetupdlg.minMarginLeft = margins.Left;
				pagesetupdlg.minMarginTop = margins.Top;
				pagesetupdlg.minMarginRight = margins.Right;
				pagesetupdlg.minMarginBottom = margins.Bottom;
			}
			if (this.pageSettings.Margins != null)
			{
				Margins margins2 = PrinterUnitConvert.Convert(this.pageSettings.Margins, PrinterUnit.Display, printerUnit);
				pagesetupdlg.marginLeft = margins2.Left;
				pagesetupdlg.marginTop = margins2.Top;
				pagesetupdlg.marginRight = margins2.Right;
				pagesetupdlg.marginBottom = margins2.Bottom;
			}
			pagesetupdlg.marginLeft = Math.Max(pagesetupdlg.marginLeft, pagesetupdlg.minMarginLeft);
			pagesetupdlg.marginTop = Math.Max(pagesetupdlg.marginTop, pagesetupdlg.minMarginTop);
			pagesetupdlg.marginRight = Math.Max(pagesetupdlg.marginRight, pagesetupdlg.minMarginRight);
			pagesetupdlg.marginBottom = Math.Max(pagesetupdlg.marginBottom, pagesetupdlg.minMarginBottom);
			PrinterSettings printerSettings = ((this.printerSettings == null) ? this.pageSettings.PrinterSettings : this.printerSettings);
			IntSecurity.AllPrintingAndUnmanagedCode.Assert();
			try
			{
				pagesetupdlg.hDevMode = printerSettings.GetHdevmode(this.pageSettings);
				pagesetupdlg.hDevNames = printerSettings.GetHdevnames();
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			bool flag;
			try
			{
				if (!UnsafeNativeMethods.PageSetupDlg(pagesetupdlg))
				{
					flag = false;
				}
				else
				{
					PageSetupDialog.UpdateSettings(pagesetupdlg, this.pageSettings, this.printerSettings);
					flag = true;
				}
			}
			finally
			{
				UnsafeNativeMethods.GlobalFree(new HandleRef(pagesetupdlg, pagesetupdlg.hDevMode));
				UnsafeNativeMethods.GlobalFree(new HandleRef(pagesetupdlg, pagesetupdlg.hDevNames));
			}
			return flag;
		}

		// Token: 0x04002863 RID: 10339
		private PrintDocument printDocument;

		// Token: 0x04002864 RID: 10340
		private PageSettings pageSettings;

		// Token: 0x04002865 RID: 10341
		private PrinterSettings printerSettings;

		// Token: 0x04002866 RID: 10342
		private bool allowMargins;

		// Token: 0x04002867 RID: 10343
		private bool allowOrientation;

		// Token: 0x04002868 RID: 10344
		private bool allowPaper;

		// Token: 0x04002869 RID: 10345
		private bool allowPrinter;

		// Token: 0x0400286A RID: 10346
		private Margins minMargins;

		// Token: 0x0400286B RID: 10347
		private bool showHelp;

		// Token: 0x0400286C RID: 10348
		private bool showNetwork;

		// Token: 0x0400286D RID: 10349
		private bool enableMetric;
	}
}
