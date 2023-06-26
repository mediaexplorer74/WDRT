using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	/// <summary>Prompts the user to choose a font from among those installed on the local computer.</summary>
	// Token: 0x0200025A RID: 602
	[DefaultEvent("Apply")]
	[DefaultProperty("Font")]
	[SRDescription("DescriptionFontDialog")]
	public class FontDialog : CommonDialog
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.FontDialog" /> class.</summary>
		// Token: 0x060025D6 RID: 9686 RVA: 0x000AFA7F File Offset: 0x000ADC7F
		public FontDialog()
		{
			this.Reset();
		}

		/// <summary>Gets or sets a value indicating whether the dialog box allows graphics device interface (GDI) font simulations.</summary>
		/// <returns>
		///   <see langword="true" /> if font simulations are allowed; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x060025D7 RID: 9687 RVA: 0x000AFE05 File Offset: 0x000AE005
		// (set) Token: 0x060025D8 RID: 9688 RVA: 0x000AFE15 File Offset: 0x000AE015
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("FnDallowSimulationsDescr")]
		public bool AllowSimulations
		{
			get
			{
				return !this.GetOption(4096);
			}
			set
			{
				this.SetOption(4096, !value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box allows vector font selections.</summary>
		/// <returns>
		///   <see langword="true" /> if vector fonts are allowed; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x060025D9 RID: 9689 RVA: 0x000AFE26 File Offset: 0x000AE026
		// (set) Token: 0x060025DA RID: 9690 RVA: 0x000AFE36 File Offset: 0x000AE036
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("FnDallowVectorFontsDescr")]
		public bool AllowVectorFonts
		{
			get
			{
				return !this.GetOption(2048);
			}
			set
			{
				this.SetOption(2048, !value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box displays both vertical and horizontal fonts or only horizontal fonts.</summary>
		/// <returns>
		///   <see langword="true" /> if both vertical and horizontal fonts are allowed; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x060025DB RID: 9691 RVA: 0x000AFE47 File Offset: 0x000AE047
		// (set) Token: 0x060025DC RID: 9692 RVA: 0x000AFE57 File Offset: 0x000AE057
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("FnDallowVerticalFontsDescr")]
		public bool AllowVerticalFonts
		{
			get
			{
				return !this.GetOption(16777216);
			}
			set
			{
				this.SetOption(16777216, !value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the user can change the character set specified in the Script combo box to display a character set other than the one currently displayed.</summary>
		/// <returns>
		///   <see langword="true" /> if the user can change the character set specified in the Script combo box; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x060025DD RID: 9693 RVA: 0x000AFE68 File Offset: 0x000AE068
		// (set) Token: 0x060025DE RID: 9694 RVA: 0x000AFE78 File Offset: 0x000AE078
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("FnDallowScriptChangeDescr")]
		public bool AllowScriptChange
		{
			get
			{
				return !this.GetOption(4194304);
			}
			set
			{
				this.SetOption(4194304, !value);
			}
		}

		/// <summary>Gets or sets the selected font color.</summary>
		/// <returns>The color of the selected font. The default value is <see cref="P:System.Drawing.Color.Black" />.</returns>
		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x060025DF RID: 9695 RVA: 0x000AFE89 File Offset: 0x000AE089
		// (set) Token: 0x060025E0 RID: 9696 RVA: 0x000AFEAA File Offset: 0x000AE0AA
		[SRCategory("CatData")]
		[SRDescription("FnDcolorDescr")]
		[DefaultValue(typeof(Color), "Black")]
		public Color Color
		{
			get
			{
				if (this.usingDefaultIndirectColor)
				{
					return ColorTranslator.FromWin32(ColorTranslator.ToWin32(this.color));
				}
				return this.color;
			}
			set
			{
				if (!value.IsEmpty)
				{
					this.color = value;
					this.usingDefaultIndirectColor = false;
					return;
				}
				this.color = SystemColors.ControlText;
				this.usingDefaultIndirectColor = true;
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box allows only the selection of fixed-pitch fonts.</summary>
		/// <returns>
		///   <see langword="true" /> if only fixed-pitch fonts can be selected; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x060025E1 RID: 9697 RVA: 0x000AFED6 File Offset: 0x000AE0D6
		// (set) Token: 0x060025E2 RID: 9698 RVA: 0x000AFEE3 File Offset: 0x000AE0E3
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("FnDfixedPitchOnlyDescr")]
		public bool FixedPitchOnly
		{
			get
			{
				return this.GetOption(16384);
			}
			set
			{
				this.SetOption(16384, value);
			}
		}

		/// <summary>Gets or sets the selected font.</summary>
		/// <returns>The selected font.</returns>
		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x060025E3 RID: 9699 RVA: 0x000AFEF4 File Offset: 0x000AE0F4
		// (set) Token: 0x060025E4 RID: 9700 RVA: 0x000AFF71 File Offset: 0x000AE171
		[SRCategory("CatData")]
		[SRDescription("FnDfontDescr")]
		public Font Font
		{
			get
			{
				Font font = this.font;
				if (font == null)
				{
					font = Control.DefaultFont;
				}
				float sizeInPoints = font.SizeInPoints;
				if (this.minSize != 0 && sizeInPoints < (float)this.MinSize)
				{
					font = new Font(font.FontFamily, (float)this.MinSize, font.Style, GraphicsUnit.Point);
				}
				if (this.maxSize != 0 && sizeInPoints > (float)this.MaxSize)
				{
					font = new Font(font.FontFamily, (float)this.MaxSize, font.Style, GraphicsUnit.Point);
				}
				return font;
			}
			set
			{
				this.font = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box specifies an error condition if the user attempts to select a font or style that does not exist.</summary>
		/// <returns>
		///   <see langword="true" /> if the dialog box specifies an error condition when the user tries to select a font or style that does not exist; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x060025E5 RID: 9701 RVA: 0x000AFF7A File Offset: 0x000AE17A
		// (set) Token: 0x060025E6 RID: 9702 RVA: 0x000AFF87 File Offset: 0x000AE187
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("FnDfontMustExistDescr")]
		public bool FontMustExist
		{
			get
			{
				return this.GetOption(65536);
			}
			set
			{
				this.SetOption(65536, value);
			}
		}

		/// <summary>Gets or sets the maximum point size a user can select.</summary>
		/// <returns>The maximum point size a user can select. The default is 0.</returns>
		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x060025E7 RID: 9703 RVA: 0x000AFF95 File Offset: 0x000AE195
		// (set) Token: 0x060025E8 RID: 9704 RVA: 0x000AFF9D File Offset: 0x000AE19D
		[SRCategory("CatData")]
		[DefaultValue(0)]
		[SRDescription("FnDmaxSizeDescr")]
		public int MaxSize
		{
			get
			{
				return this.maxSize;
			}
			set
			{
				if (value < 0)
				{
					value = 0;
				}
				this.maxSize = value;
				if (this.maxSize > 0 && this.maxSize < this.minSize)
				{
					this.minSize = this.maxSize;
				}
			}
		}

		/// <summary>Gets or sets the minimum point size a user can select.</summary>
		/// <returns>The minimum point size a user can select. The default is 0.</returns>
		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x060025E9 RID: 9705 RVA: 0x000AFFD0 File Offset: 0x000AE1D0
		// (set) Token: 0x060025EA RID: 9706 RVA: 0x000AFFD8 File Offset: 0x000AE1D8
		[SRCategory("CatData")]
		[DefaultValue(0)]
		[SRDescription("FnDminSizeDescr")]
		public int MinSize
		{
			get
			{
				return this.minSize;
			}
			set
			{
				if (value < 0)
				{
					value = 0;
				}
				this.minSize = value;
				if (this.maxSize > 0 && this.maxSize < this.minSize)
				{
					this.maxSize = this.minSize;
				}
			}
		}

		/// <summary>Gets values to initialize the <see cref="T:System.Windows.Forms.FontDialog" />.</summary>
		/// <returns>A bitwise combination of internal values that initializes the <see cref="T:System.Windows.Forms.FontDialog" />.</returns>
		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x060025EB RID: 9707 RVA: 0x000B000B File Offset: 0x000AE20B
		protected int Options
		{
			get
			{
				return this.options;
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box allows selection of fonts for all non-OEM and Symbol character sets, as well as the ANSI character set.</summary>
		/// <returns>
		///   <see langword="true" /> if selection of fonts for all non-OEM and Symbol character sets, as well as the ANSI character set, is allowed; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x060025EC RID: 9708 RVA: 0x000B0013 File Offset: 0x000AE213
		// (set) Token: 0x060025ED RID: 9709 RVA: 0x000B0020 File Offset: 0x000AE220
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("FnDscriptsOnlyDescr")]
		public bool ScriptsOnly
		{
			get
			{
				return this.GetOption(1024);
			}
			set
			{
				this.SetOption(1024, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box contains an Apply button.</summary>
		/// <returns>
		///   <see langword="true" /> if the dialog box contains an Apply button; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x060025EE RID: 9710 RVA: 0x000B002E File Offset: 0x000AE22E
		// (set) Token: 0x060025EF RID: 9711 RVA: 0x000B003B File Offset: 0x000AE23B
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("FnDshowApplyDescr")]
		public bool ShowApply
		{
			get
			{
				return this.GetOption(512);
			}
			set
			{
				this.SetOption(512, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box displays the color choice.</summary>
		/// <returns>
		///   <see langword="true" /> if the dialog box displays the color choice; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x060025F0 RID: 9712 RVA: 0x000B0049 File Offset: 0x000AE249
		// (set) Token: 0x060025F1 RID: 9713 RVA: 0x000B0051 File Offset: 0x000AE251
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("FnDshowColorDescr")]
		public bool ShowColor
		{
			get
			{
				return this.showColor;
			}
			set
			{
				this.showColor = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box contains controls that allow the user to specify strikethrough, underline, and text color options.</summary>
		/// <returns>
		///   <see langword="true" /> if the dialog box contains controls to set strikethrough, underline, and text color options; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x060025F2 RID: 9714 RVA: 0x000B005A File Offset: 0x000AE25A
		// (set) Token: 0x060025F3 RID: 9715 RVA: 0x000B0067 File Offset: 0x000AE267
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("FnDshowEffectsDescr")]
		public bool ShowEffects
		{
			get
			{
				return this.GetOption(256);
			}
			set
			{
				this.SetOption(256, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box displays a Help button.</summary>
		/// <returns>
		///   <see langword="true" /> if the dialog box displays a Help button; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x060025F4 RID: 9716 RVA: 0x000B0075 File Offset: 0x000AE275
		// (set) Token: 0x060025F5 RID: 9717 RVA: 0x000B007E File Offset: 0x000AE27E
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("FnDshowHelpDescr")]
		public bool ShowHelp
		{
			get
			{
				return this.GetOption(4);
			}
			set
			{
				this.SetOption(4, value);
			}
		}

		/// <summary>Occurs when the user clicks the Apply button in the font dialog box.</summary>
		// Token: 0x1400019C RID: 412
		// (add) Token: 0x060025F6 RID: 9718 RVA: 0x000B0088 File Offset: 0x000AE288
		// (remove) Token: 0x060025F7 RID: 9719 RVA: 0x000B009B File Offset: 0x000AE29B
		[SRDescription("FnDapplyDescr")]
		public event EventHandler Apply
		{
			add
			{
				base.Events.AddHandler(FontDialog.EventApply, value);
			}
			remove
			{
				base.Events.RemoveHandler(FontDialog.EventApply, value);
			}
		}

		// Token: 0x060025F8 RID: 9720 RVA: 0x000B00AE File Offset: 0x000AE2AE
		internal bool GetOption(int option)
		{
			return (this.options & option) != 0;
		}

		/// <summary>Specifies the common dialog box hook procedure that is overridden to add specific functionality to a common dialog box.</summary>
		/// <param name="hWnd">The handle to the dialog box window.</param>
		/// <param name="msg">The message being received.</param>
		/// <param name="wparam">Additional information about the message.</param>
		/// <param name="lparam">Additional information about the message.</param>
		/// <returns>A zero value if the default dialog box procedure processes the message; a nonzero value if the default dialog box procedure ignores the message.</returns>
		// Token: 0x060025F9 RID: 9721 RVA: 0x000B00BC File Offset: 0x000AE2BC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override IntPtr HookProc(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
		{
			if (msg != 272)
			{
				if (msg != 273 || (int)wparam != 1026)
				{
					goto IL_110;
				}
				NativeMethods.LOGFONT logfont = new NativeMethods.LOGFONT();
				UnsafeNativeMethods.SendMessage(new HandleRef(null, hWnd), 1025, 0, logfont);
				this.UpdateFont(logfont);
				int num = (int)UnsafeNativeMethods.SendDlgItemMessage(new HandleRef(null, hWnd), 1139, 327, IntPtr.Zero, IntPtr.Zero);
				if (num != -1)
				{
					this.UpdateColor((int)UnsafeNativeMethods.SendDlgItemMessage(new HandleRef(null, hWnd), 1139, 336, (IntPtr)num, IntPtr.Zero));
				}
				if (NativeWindow.WndProcShouldBeDebuggable)
				{
					this.OnApply(EventArgs.Empty);
					goto IL_110;
				}
				try
				{
					this.OnApply(EventArgs.Empty);
					goto IL_110;
				}
				catch (Exception ex)
				{
					Application.OnThreadException(ex);
					goto IL_110;
				}
			}
			if (!this.showColor)
			{
				IntPtr intPtr = UnsafeNativeMethods.GetDlgItem(new HandleRef(null, hWnd), 1139);
				SafeNativeMethods.ShowWindow(new HandleRef(null, intPtr), 0);
				intPtr = UnsafeNativeMethods.GetDlgItem(new HandleRef(null, hWnd), 1091);
				SafeNativeMethods.ShowWindow(new HandleRef(null, intPtr), 0);
			}
			IL_110:
			return base.HookProc(hWnd, msg, wparam, lparam);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.FontDialog.Apply" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the data.</param>
		// Token: 0x060025FA RID: 9722 RVA: 0x000B01F4 File Offset: 0x000AE3F4
		protected virtual void OnApply(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[FontDialog.EventApply];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Resets all dialog box options to their default values.</summary>
		// Token: 0x060025FB RID: 9723 RVA: 0x000B0224 File Offset: 0x000AE424
		public override void Reset()
		{
			this.options = 257;
			this.font = null;
			this.color = SystemColors.ControlText;
			this.usingDefaultIndirectColor = true;
			this.showColor = false;
			this.minSize = 0;
			this.maxSize = 0;
			this.SetOption(262144, true);
		}

		// Token: 0x060025FC RID: 9724 RVA: 0x000B0276 File Offset: 0x000AE476
		private void ResetFont()
		{
			this.font = null;
		}

		/// <summary>Specifies a file dialog box.</summary>
		/// <param name="hWndOwner">The window handle of the owner window for the common dialog box.</param>
		/// <returns>
		///   <see langword="true" /> if the dialog box was successfully run; otherwise, <see langword="false" />.</returns>
		// Token: 0x060025FD RID: 9725 RVA: 0x000B0280 File Offset: 0x000AE480
		protected override bool RunDialog(IntPtr hWndOwner)
		{
			NativeMethods.WndProc wndProc = new NativeMethods.WndProc(this.HookProc);
			NativeMethods.CHOOSEFONT choosefont = new NativeMethods.CHOOSEFONT();
			IntPtr dc = UnsafeNativeMethods.GetDC(NativeMethods.NullHandleRef);
			NativeMethods.LOGFONT logfont = new NativeMethods.LOGFONT();
			Graphics graphics = Graphics.FromHdcInternal(dc);
			IntSecurity.ObjectFromWin32Handle.Assert();
			try
			{
				this.Font.ToLogFont(logfont, graphics);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
				graphics.Dispose();
			}
			UnsafeNativeMethods.ReleaseDC(NativeMethods.NullHandleRef, new HandleRef(null, dc));
			IntPtr intPtr = IntPtr.Zero;
			bool flag;
			try
			{
				intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(NativeMethods.LOGFONT)));
				Marshal.StructureToPtr(logfont, intPtr, false);
				choosefont.lStructSize = Marshal.SizeOf(typeof(NativeMethods.CHOOSEFONT));
				choosefont.hwndOwner = hWndOwner;
				choosefont.hDC = IntPtr.Zero;
				choosefont.lpLogFont = intPtr;
				choosefont.Flags = this.Options | 64 | 8;
				if (this.minSize > 0 || this.maxSize > 0)
				{
					choosefont.Flags |= 8192;
				}
				if (this.ShowColor || this.ShowEffects)
				{
					choosefont.rgbColors = ColorTranslator.ToWin32(this.color);
				}
				else
				{
					choosefont.rgbColors = ColorTranslator.ToWin32(SystemColors.ControlText);
				}
				choosefont.lpfnHook = wndProc;
				choosefont.hInstance = UnsafeNativeMethods.GetModuleHandle(null);
				choosefont.nSizeMin = this.minSize;
				if (this.maxSize == 0)
				{
					choosefont.nSizeMax = int.MaxValue;
				}
				else
				{
					choosefont.nSizeMax = this.maxSize;
				}
				if (!SafeNativeMethods.ChooseFont(choosefont))
				{
					flag = false;
				}
				else
				{
					NativeMethods.LOGFONT logfont2 = (NativeMethods.LOGFONT)UnsafeNativeMethods.PtrToStructure(intPtr, typeof(NativeMethods.LOGFONT));
					if (logfont2.lfFaceName != null && logfont2.lfFaceName.Length > 0)
					{
						logfont = logfont2;
						this.UpdateFont(logfont);
						this.UpdateColor(choosefont.rgbColors);
					}
					flag = true;
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(intPtr);
				}
			}
			return flag;
		}

		// Token: 0x060025FE RID: 9726 RVA: 0x000B0498 File Offset: 0x000AE698
		internal void SetOption(int option, bool value)
		{
			if (value)
			{
				this.options |= option;
				return;
			}
			this.options &= ~option;
		}

		// Token: 0x060025FF RID: 9727 RVA: 0x000B04BB File Offset: 0x000AE6BB
		private bool ShouldSerializeFont()
		{
			return !this.Font.Equals(Control.DefaultFont);
		}

		/// <summary>Retrieves a string that includes the name of the current font selected in the dialog box.</summary>
		/// <returns>A string that includes the name of the currently selected font.</returns>
		// Token: 0x06002600 RID: 9728 RVA: 0x000B04D0 File Offset: 0x000AE6D0
		public override string ToString()
		{
			string text = base.ToString();
			return text + ",  Font: " + this.Font.ToString();
		}

		// Token: 0x06002601 RID: 9729 RVA: 0x000B04FA File Offset: 0x000AE6FA
		private void UpdateColor(int rgb)
		{
			if (ColorTranslator.ToWin32(this.color) != rgb)
			{
				this.color = ColorTranslator.FromOle(rgb);
				this.usingDefaultIndirectColor = false;
			}
		}

		// Token: 0x06002602 RID: 9730 RVA: 0x000B0520 File Offset: 0x000AE720
		private void UpdateFont(NativeMethods.LOGFONT lf)
		{
			IntPtr dc = UnsafeNativeMethods.GetDC(NativeMethods.NullHandleRef);
			try
			{
				Font font = null;
				try
				{
					IntSecurity.UnmanagedCode.Assert();
					try
					{
						font = Font.FromLogFont(lf, dc);
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
					this.font = ControlPaint.FontInPoints(font);
				}
				finally
				{
					if (font != null)
					{
						font.Dispose();
					}
				}
			}
			finally
			{
				UnsafeNativeMethods.ReleaseDC(NativeMethods.NullHandleRef, new HandleRef(null, dc));
			}
		}

		/// <summary>Owns the <see cref="E:System.Windows.Forms.FontDialog.Apply" /> event.</summary>
		// Token: 0x04000FB2 RID: 4018
		protected static readonly object EventApply = new object();

		// Token: 0x04000FB3 RID: 4019
		private const int defaultMinSize = 0;

		// Token: 0x04000FB4 RID: 4020
		private const int defaultMaxSize = 0;

		// Token: 0x04000FB5 RID: 4021
		private int options;

		// Token: 0x04000FB6 RID: 4022
		private Font font;

		// Token: 0x04000FB7 RID: 4023
		private Color color;

		// Token: 0x04000FB8 RID: 4024
		private int minSize;

		// Token: 0x04000FB9 RID: 4025
		private int maxSize;

		// Token: 0x04000FBA RID: 4026
		private bool showColor;

		// Token: 0x04000FBB RID: 4027
		private bool usingDefaultIndirectColor;
	}
}
