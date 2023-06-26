using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms.Layout;
using Microsoft.Win32;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows rich text box control.</summary>
	// Token: 0x02000345 RID: 837
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Docking(DockingBehavior.Ask)]
	[Designer("System.Windows.Forms.Design.RichTextBoxDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionRichTextBox")]
	public class RichTextBox : TextBoxBase
	{
		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x060035E7 RID: 13799 RVA: 0x000F3672 File Offset: 0x000F1872
		private static TraceSwitch RichTextDbg
		{
			get
			{
				if (RichTextBox.richTextDbg == null)
				{
					RichTextBox.richTextDbg = new TraceSwitch("RichTextDbg", "Debug info about RichTextBox");
				}
				return RichTextBox.richTextDbg;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.RichTextBox" /> class.</summary>
		// Token: 0x060035E8 RID: 13800 RVA: 0x000F3694 File Offset: 0x000F1894
		public RichTextBox()
		{
			this.InConstructor = true;
			this.richTextBoxFlags[RichTextBox.autoWordSelectionSection] = 0;
			this.DetectUrls = true;
			this.ScrollBars = RichTextBoxScrollBars.Both;
			this.RichTextShortcutsEnabled = true;
			this.MaxLength = int.MaxValue;
			this.Multiline = true;
			this.AutoSize = false;
			this.curSelStart = (this.curSelEnd = (int)(this.curSelType = -1));
			this.InConstructor = false;
		}

		/// <summary>Gets or sets a value indicating whether the control will enable drag-and-drop operations.</summary>
		/// <returns>
		///   <see langword="true" /> if drag-and-drop is enabled in the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x060035E9 RID: 13801 RVA: 0x000F3723 File Offset: 0x000F1923
		// (set) Token: 0x060035EA RID: 13802 RVA: 0x000F3738 File Offset: 0x000F1938
		[Browsable(false)]
		public override bool AllowDrop
		{
			get
			{
				return this.richTextBoxFlags[RichTextBox.allowOleDropSection] != 0;
			}
			set
			{
				if (value)
				{
					try
					{
						IntSecurity.ClipboardRead.Demand();
					}
					catch (Exception ex)
					{
						throw new InvalidOperationException(SR.GetString("DragDropRegFailed"), ex);
					}
				}
				this.richTextBoxFlags[RichTextBox.allowOleDropSection] = (value ? 1 : 0);
				this.UpdateOleCallback();
			}
		}

		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x060035EB RID: 13803 RVA: 0x000F3794 File Offset: 0x000F1994
		// (set) Token: 0x060035EC RID: 13804 RVA: 0x000F37A9 File Offset: 0x000F19A9
		internal bool AllowOleObjects
		{
			get
			{
				return this.richTextBoxFlags[RichTextBox.allowOleObjectsSection] != 0;
			}
			set
			{
				this.richTextBoxFlags[RichTextBox.allowOleObjectsSection] = (value ? 1 : 0);
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>
		///   <see langword="true" /> if enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x060035ED RID: 13805 RVA: 0x000F37C2 File Offset: 0x000F19C2
		// (set) Token: 0x060035EE RID: 13806 RVA: 0x000F37CA File Offset: 0x000F19CA
		[DefaultValue(false)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override bool AutoSize
		{
			get
			{
				return base.AutoSize;
			}
			set
			{
				base.AutoSize = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether automatic word selection is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if automatic word selection is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x060035EF RID: 13807 RVA: 0x000F37D3 File Offset: 0x000F19D3
		// (set) Token: 0x060035F0 RID: 13808 RVA: 0x000F37E8 File Offset: 0x000F19E8
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("RichTextBoxAutoWordSelection")]
		public bool AutoWordSelection
		{
			get
			{
				return this.richTextBoxFlags[RichTextBox.autoWordSelectionSection] != 0;
			}
			set
			{
				this.richTextBoxFlags[RichTextBox.autoWordSelectionSection] = (value ? 1 : 0);
				if (base.IsHandleCreated)
				{
					base.SendMessage(1101, value ? 2 : 4, 1);
				}
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>The background image displayed in the control.</returns>
		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x060035F1 RID: 13809 RVA: 0x000F381D File Offset: 0x000F1A1D
		// (set) Token: 0x060035F2 RID: 13810 RVA: 0x000F3825 File Offset: 0x000F1A25
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
			set
			{
				base.BackgroundImage = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.RichTextBox.BackgroundImage" /> property changes.</summary>
		// Token: 0x14000289 RID: 649
		// (add) Token: 0x060035F3 RID: 13811 RVA: 0x000F382E File Offset: 0x000F1A2E
		// (remove) Token: 0x060035F4 RID: 13812 RVA: 0x000F3837 File Offset: 0x000F1A37
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageChanged
		{
			add
			{
				base.BackgroundImageChanged += value;
			}
			remove
			{
				base.BackgroundImageChanged -= value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>The layout of the background image displayed in the control.</returns>
		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x060035F5 RID: 13813 RVA: 0x000F3840 File Offset: 0x000F1A40
		// (set) Token: 0x060035F6 RID: 13814 RVA: 0x000F3848 File Offset: 0x000F1A48
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ImageLayout BackgroundImageLayout
		{
			get
			{
				return base.BackgroundImageLayout;
			}
			set
			{
				base.BackgroundImageLayout = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.RichTextBox.BackgroundImageLayout" /> property changes.</summary>
		// Token: 0x1400028A RID: 650
		// (add) Token: 0x060035F7 RID: 13815 RVA: 0x000F3851 File Offset: 0x000F1A51
		// (remove) Token: 0x060035F8 RID: 13816 RVA: 0x000F385A File Offset: 0x000F1A5A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageLayoutChanged
		{
			add
			{
				base.BackgroundImageLayoutChanged += value;
			}
			remove
			{
				base.BackgroundImageLayoutChanged -= value;
			}
		}

		/// <summary>Gets or sets the indentation used in the <see cref="T:System.Windows.Forms.RichTextBox" /> control when the bullet style is applied to the text.</summary>
		/// <returns>The number of pixels inserted as the indentation after a bullet. The default is zero.</returns>
		/// <exception cref="T:System.ArgumentException">The specified indentation was less than zero.</exception>
		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x060035F9 RID: 13817 RVA: 0x000F3863 File Offset: 0x000F1A63
		// (set) Token: 0x060035FA RID: 13818 RVA: 0x000F386C File Offset: 0x000F1A6C
		[SRCategory("CatBehavior")]
		[DefaultValue(0)]
		[Localizable(true)]
		[SRDescription("RichTextBoxBulletIndent")]
		public int BulletIndent
		{
			get
			{
				return this.bulletIndent;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("BulletIndent", SR.GetString("InvalidArgument", new object[]
					{
						"BulletIndent",
						value.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.bulletIndent = value;
				if (base.IsHandleCreated && this.SelectionBullet)
				{
					this.SelectionBullet = true;
				}
			}
		}

		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x060035FB RID: 13819 RVA: 0x000F38CD File Offset: 0x000F1ACD
		// (set) Token: 0x060035FC RID: 13820 RVA: 0x000F38E2 File Offset: 0x000F1AE2
		private bool CallOnContentsResized
		{
			get
			{
				return this.richTextBoxFlags[RichTextBox.callOnContentsResizedSection] != 0;
			}
			set
			{
				this.richTextBoxFlags[RichTextBox.callOnContentsResizedSection] = (value ? 1 : 0);
			}
		}

		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x060035FD RID: 13821 RVA: 0x000F38FB File Offset: 0x000F1AFB
		internal override bool CanRaiseTextChangedEvent
		{
			get
			{
				return !this.SuppressTextChangedEvent;
			}
		}

		/// <summary>Gets a value indicating whether there are actions that have occurred within the <see cref="T:System.Windows.Forms.RichTextBox" /> that can be reapplied.</summary>
		/// <returns>
		///   <see langword="true" /> if there are operations that have been undone that can be reapplied to the content of the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x060035FE RID: 13822 RVA: 0x000F3908 File Offset: 0x000F1B08
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("RichTextBoxCanRedoDescr")]
		public bool CanRedo
		{
			get
			{
				return base.IsHandleCreated && (int)(long)base.SendMessage(1109, 0, 0) != 0;
			}
		}

		// Token: 0x060035FF RID: 13823 RVA: 0x000F3937 File Offset: 0x000F1B37
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (!AccessibilityImprovements.Level5)
			{
				return base.CreateAccessibilityInstance();
			}
			return new Control.ControlAccessibleObject(this);
		}

		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x06003600 RID: 13824 RVA: 0x000F3950 File Offset: 0x000F1B50
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				if (RichTextBox.moduleHandle == IntPtr.Zero)
				{
					string text = (LocalAppContextSwitches.DoNotLoadLatestRichEditControl ? "RichEd20.DLL" : "MsftEdit.DLL");
					RichTextBox.moduleHandle = UnsafeNativeMethods.LoadLibraryFromSystemPathIfAvailable(text);
					int lastWin32Error = Marshal.GetLastWin32Error();
					if ((long)RichTextBox.moduleHandle < 32L)
					{
						throw new Win32Exception(lastWin32Error, SR.GetString("LoadDLLError", new object[] { text }));
					}
					StringBuilder moduleFileNameLongPath = UnsafeNativeMethods.GetModuleFileNameLongPath(new HandleRef(null, RichTextBox.moduleHandle));
					string text2 = moduleFileNameLongPath.ToString();
					new FileIOPermission(FileIOPermissionAccess.Read, text2).Assert();
					FileVersionInfo versionInfo;
					try
					{
						versionInfo = FileVersionInfo.GetVersionInfo(text2);
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
					int num;
					if (versionInfo != null && !string.IsNullOrEmpty(versionInfo.ProductVersion) && int.TryParse(versionInfo.ProductVersion[0].ToString(), out num))
					{
						RichTextBox.richEditMajorVersion = num;
					}
				}
				CreateParams createParams = base.CreateParams;
				if (Marshal.SystemDefaultCharSize == 1)
				{
					createParams.ClassName = (LocalAppContextSwitches.DoNotLoadLatestRichEditControl ? "RichEdit20A" : "RICHEDIT50A");
				}
				else
				{
					createParams.ClassName = (LocalAppContextSwitches.DoNotLoadLatestRichEditControl ? "RichEdit20W" : "RICHEDIT50W");
				}
				if (this.Multiline)
				{
					if ((this.ScrollBars & RichTextBoxScrollBars.Horizontal) != RichTextBoxScrollBars.None && !base.WordWrap)
					{
						createParams.Style |= 1048576;
						if ((this.ScrollBars & (RichTextBoxScrollBars)16) != RichTextBoxScrollBars.None)
						{
							createParams.Style |= 8192;
						}
					}
					if ((this.ScrollBars & RichTextBoxScrollBars.Vertical) != RichTextBoxScrollBars.None)
					{
						createParams.Style |= 2097152;
						if ((this.ScrollBars & (RichTextBoxScrollBars)16) != RichTextBoxScrollBars.None)
						{
							createParams.Style |= 8192;
						}
					}
				}
				if (BorderStyle.FixedSingle == base.BorderStyle && (createParams.Style & 8388608) != 0)
				{
					createParams.Style &= -8388609;
					createParams.ExStyle |= 512;
				}
				return createParams;
			}
		}

		/// <summary>Gets or sets a value indicating whether or not the <see cref="T:System.Windows.Forms.RichTextBox" /> will automatically format a Uniform Resource Locator (URL) when it is typed into the control.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.RichTextBox" /> will automatically format URLs that are typed into the control as a link; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x06003601 RID: 13825 RVA: 0x000F3B40 File Offset: 0x000F1D40
		// (set) Token: 0x06003602 RID: 13826 RVA: 0x000F3B58 File Offset: 0x000F1D58
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("RichTextBoxDetectURLs")]
		public bool DetectUrls
		{
			get
			{
				return this.richTextBoxFlags[RichTextBox.autoUrlDetectSection] != 0;
			}
			set
			{
				if (value != this.DetectUrls)
				{
					this.richTextBoxFlags[RichTextBox.autoUrlDetectSection] = (value ? 1 : 0);
					if (base.IsHandleCreated)
					{
						base.SendMessage(1115, value ? 1 : 0, 0);
						base.RecreateHandle();
					}
				}
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> value.</returns>
		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x06003603 RID: 13827 RVA: 0x000F3BA7 File Offset: 0x000F1DA7
		protected override Size DefaultSize
		{
			get
			{
				return new Size(100, 96);
			}
		}

		/// <summary>Gets or sets a value that enables drag-and-drop operations on text, pictures, and other data.</summary>
		/// <returns>
		///   <see langword="true" /> to enable drag-and-drop operations; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x06003604 RID: 13828 RVA: 0x000F3BB2 File Offset: 0x000F1DB2
		// (set) Token: 0x06003605 RID: 13829 RVA: 0x000F3BC8 File Offset: 0x000F1DC8
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("RichTextBoxEnableAutoDragDrop")]
		public bool EnableAutoDragDrop
		{
			get
			{
				return this.richTextBoxFlags[RichTextBox.enableAutoDragDropSection] != 0;
			}
			set
			{
				if (value)
				{
					try
					{
						IntSecurity.ClipboardRead.Demand();
					}
					catch (Exception ex)
					{
						throw new InvalidOperationException(SR.GetString("DragDropRegFailed"), ex);
					}
				}
				this.richTextBoxFlags[RichTextBox.enableAutoDragDropSection] = (value ? 1 : 0);
				this.UpdateOleCallback();
			}
		}

		/// <summary>Gets or sets the foreground color of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the control's foreground color.</returns>
		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x06003606 RID: 13830 RVA: 0x000F3C24 File Offset: 0x000F1E24
		// (set) Token: 0x06003607 RID: 13831 RVA: 0x000F3C2C File Offset: 0x000F1E2C
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				if (base.IsHandleCreated)
				{
					if (this.InternalSetForeColor(value))
					{
						base.ForeColor = value;
						return;
					}
				}
				else
				{
					base.ForeColor = value;
				}
			}
		}

		/// <summary>Gets or sets the font of the text displayed by the control.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultFont" /> property.</returns>
		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x06003608 RID: 13832 RVA: 0x0001A0D6 File Offset: 0x000182D6
		// (set) Token: 0x06003609 RID: 13833 RVA: 0x000F3C50 File Offset: 0x000F1E50
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				if (base.IsHandleCreated)
				{
					if (SafeNativeMethods.GetWindowTextLength(new HandleRef(this, base.Handle)) > 0)
					{
						if (value == null)
						{
							base.Font = null;
							this.SetCharFormatFont(false, this.Font);
							return;
						}
						try
						{
							Font charFormatFont = this.GetCharFormatFont(false);
							if (charFormatFont == null || !charFormatFont.Equals(value))
							{
								this.SetCharFormatFont(false, value);
								this.CallOnContentsResized = true;
								base.Font = this.GetCharFormatFont(false);
							}
							return;
						}
						finally
						{
							this.CallOnContentsResized = false;
						}
					}
					base.Font = value;
					return;
				}
				base.Font = value;
			}
		}

		// Token: 0x0600360A RID: 13834 RVA: 0x000F3CEC File Offset: 0x000F1EEC
		internal override Size GetPreferredSizeCore(Size proposedConstraints)
		{
			Size empty = Size.Empty;
			if (!base.WordWrap && this.Multiline && (this.ScrollBars & RichTextBoxScrollBars.Horizontal) != RichTextBoxScrollBars.None)
			{
				empty.Height += SystemInformation.HorizontalScrollBarHeight;
			}
			if (this.Multiline && (this.ScrollBars & RichTextBoxScrollBars.Vertical) != RichTextBoxScrollBars.None)
			{
				empty.Width += SystemInformation.VerticalScrollBarWidth;
			}
			proposedConstraints -= empty;
			Size preferredSizeCore = base.GetPreferredSizeCore(proposedConstraints);
			return preferredSizeCore + empty;
		}

		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x0600360B RID: 13835 RVA: 0x000F3D69 File Offset: 0x000F1F69
		// (set) Token: 0x0600360C RID: 13836 RVA: 0x000F3D7E File Offset: 0x000F1F7E
		private bool InConstructor
		{
			get
			{
				return this.richTextBoxFlags[RichTextBox.fInCtorSection] != 0;
			}
			set
			{
				this.richTextBoxFlags[RichTextBox.fInCtorSection] = (value ? 1 : 0);
			}
		}

		/// <summary>Gets or sets a value that indicates <see cref="T:System.Windows.Forms.RichTextBox" /> settings for Input Method Editor (IME) and Asian language support.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.RichTextBoxLanguageOptions" /> values. The default is <see cref="F:System.Windows.Forms.RichTextBoxLanguageOptions.AutoFontSizeAdjust" />.</returns>
		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x0600360D RID: 13837 RVA: 0x000F3D98 File Offset: 0x000F1F98
		// (set) Token: 0x0600360E RID: 13838 RVA: 0x000F3DD5 File Offset: 0x000F1FD5
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public RichTextBoxLanguageOptions LanguageOption
		{
			get
			{
				RichTextBoxLanguageOptions richTextBoxLanguageOptions;
				if (base.IsHandleCreated)
				{
					richTextBoxLanguageOptions = (RichTextBoxLanguageOptions)(int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1145, 0, 0);
				}
				else
				{
					richTextBoxLanguageOptions = this.languageOption;
				}
				return richTextBoxLanguageOptions;
			}
			set
			{
				if (this.LanguageOption != value)
				{
					this.languageOption = value;
					if (base.IsHandleCreated)
					{
						UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1144, 0, (int)value);
					}
				}
			}
		}

		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x0600360F RID: 13839 RVA: 0x000F3E08 File Offset: 0x000F2008
		// (set) Token: 0x06003610 RID: 13840 RVA: 0x000F3E1D File Offset: 0x000F201D
		private bool LinkCursor
		{
			get
			{
				return this.richTextBoxFlags[RichTextBox.linkcursorSection] != 0;
			}
			set
			{
				this.richTextBoxFlags[RichTextBox.linkcursorSection] = (value ? 1 : 0);
			}
		}

		/// <summary>Gets or sets the maximum number of characters the user can type or paste into the rich text box control.</summary>
		/// <returns>The number of characters that can be entered into the control. The default is <see cref="F:System.Int32.MaxValue" />.</returns>
		/// <exception cref="T:System.ArgumentException">The value assigned to the property is less than 0.</exception>
		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x06003611 RID: 13841 RVA: 0x000D3D0B File Offset: 0x000D1F0B
		// (set) Token: 0x06003612 RID: 13842 RVA: 0x000F3E36 File Offset: 0x000F2036
		[DefaultValue(2147483647)]
		public override int MaxLength
		{
			get
			{
				return base.MaxLength;
			}
			set
			{
				base.MaxLength = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether this is a multiline <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
		/// <returns>
		///   <see langword="true" /> if the control is a multiline <see cref="T:System.Windows.Forms.RichTextBox" /> control; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x06003613 RID: 13843 RVA: 0x000F3E3F File Offset: 0x000F203F
		// (set) Token: 0x06003614 RID: 13844 RVA: 0x000F3E47 File Offset: 0x000F2047
		[DefaultValue(true)]
		public override bool Multiline
		{
			get
			{
				return base.Multiline;
			}
			set
			{
				base.Multiline = value;
			}
		}

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x06003615 RID: 13845 RVA: 0x000F3E50 File Offset: 0x000F2050
		// (set) Token: 0x06003616 RID: 13846 RVA: 0x000F3E65 File Offset: 0x000F2065
		private bool ProtectedError
		{
			get
			{
				return this.richTextBoxFlags[RichTextBox.protectedErrorSection] != 0;
			}
			set
			{
				this.richTextBoxFlags[RichTextBox.protectedErrorSection] = (value ? 1 : 0);
			}
		}

		/// <summary>Gets the name of the action that can be reapplied to the control when the <see cref="M:System.Windows.Forms.RichTextBox.Redo" /> method is called.</summary>
		/// <returns>A string that represents the name of the action that will be performed when a call to the <see cref="M:System.Windows.Forms.RichTextBox.Redo" /> method is made.</returns>
		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x06003617 RID: 13847 RVA: 0x000F3E80 File Offset: 0x000F2080
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("RichTextBoxRedoActionNameDescr")]
		public string RedoActionName
		{
			get
			{
				if (!this.CanRedo)
				{
					return "";
				}
				int num = (int)(long)base.SendMessage(1111, 0, 0);
				return this.GetEditorActionName(num);
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>
		///   <see langword="true" /> if shortcut keys are enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x06003618 RID: 13848 RVA: 0x000F3EB6 File Offset: 0x000F20B6
		// (set) Token: 0x06003619 RID: 13849 RVA: 0x000F3ECB File Offset: 0x000F20CB
		[DefaultValue(true)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool RichTextShortcutsEnabled
		{
			get
			{
				return this.richTextBoxFlags[RichTextBox.richTextShortcutsEnabledSection] != 0;
			}
			set
			{
				if (RichTextBox.shortcutsToDisable == null)
				{
					RichTextBox.shortcutsToDisable = new int[] { 131148, 131154, 131141, 131146 };
				}
				this.richTextBoxFlags[RichTextBox.richTextShortcutsEnabledSection] = (value ? 1 : 0);
			}
		}

		/// <summary>Gets or sets the size of a single line of text within the <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
		/// <returns>The size, in pixels, of a single line of text in the control. The default is zero.</returns>
		/// <exception cref="T:System.ArgumentException">The specified value was less than zero.</exception>
		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x0600361A RID: 13850 RVA: 0x000F3F01 File Offset: 0x000F2101
		// (set) Token: 0x0600361B RID: 13851 RVA: 0x000F3F0C File Offset: 0x000F210C
		[SRCategory("CatBehavior")]
		[DefaultValue(0)]
		[Localizable(true)]
		[SRDescription("RichTextBoxRightMargin")]
		public int RightMargin
		{
			get
			{
				return this.rightMargin;
			}
			set
			{
				if (this.rightMargin != value)
				{
					if (value < 0)
					{
						throw new ArgumentOutOfRangeException("RightMargin", SR.GetString("InvalidLowBoundArgumentEx", new object[]
						{
							"RightMargin",
							value.ToString(CultureInfo.CurrentCulture),
							0.ToString(CultureInfo.CurrentCulture)
						}));
					}
					this.rightMargin = value;
					if (value == 0)
					{
						base.RecreateHandle();
						return;
					}
					if (base.IsHandleCreated)
					{
						IntPtr intPtr = UnsafeNativeMethods.CreateIC("DISPLAY", null, null, new HandleRef(null, IntPtr.Zero));
						try
						{
							base.SendMessage(1096, intPtr, (IntPtr)RichTextBox.Pixel2Twip(intPtr, value, true));
						}
						finally
						{
							if (intPtr != IntPtr.Zero)
							{
								UnsafeNativeMethods.DeleteDC(new HandleRef(null, intPtr));
							}
						}
					}
				}
			}
		}

		/// <summary>Gets or sets the text of the <see cref="T:System.Windows.Forms.RichTextBox" /> control, including all rich text format (RTF) codes.</summary>
		/// <returns>The text of the control in RTF format.</returns>
		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x0600361C RID: 13852 RVA: 0x000F3FE4 File Offset: 0x000F21E4
		// (set) Token: 0x0600361D RID: 13853 RVA: 0x000F4014 File Offset: 0x000F2214
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("RichTextBoxRTF")]
		[RefreshProperties(RefreshProperties.All)]
		public string Rtf
		{
			get
			{
				if (base.IsHandleCreated)
				{
					return this.StreamOut(2);
				}
				if (this.textPlain != null)
				{
					this.ForceHandleCreate();
					return this.StreamOut(2);
				}
				return this.textRtf;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (value.Equals(this.Rtf))
				{
					return;
				}
				this.ForceHandleCreate();
				this.textRtf = value;
				this.StreamIn(value, 2);
				if (this.CanRaiseTextChangedEvent)
				{
					this.OnTextChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the type of scroll bars to display in the <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.RichTextBoxScrollBars" /> values. The default is <see langword="RichTextBoxScrollBars.Both" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not defined in the <see cref="T:System.Windows.Forms.RichTextBoxScrollBars" /> enumeration.</exception>
		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x0600361E RID: 13854 RVA: 0x000F4062 File Offset: 0x000F2262
		// (set) Token: 0x0600361F RID: 13855 RVA: 0x000F4074 File Offset: 0x000F2274
		[SRCategory("CatAppearance")]
		[DefaultValue(RichTextBoxScrollBars.Both)]
		[Localizable(true)]
		[SRDescription("RichTextBoxScrollBars")]
		public RichTextBoxScrollBars ScrollBars
		{
			get
			{
				return (RichTextBoxScrollBars)this.richTextBoxFlags[RichTextBox.scrollBarsSection];
			}
			set
			{
				if (!ClientUtils.IsEnumValid_NotSequential(value, (int)value, new int[] { 3, 0, 1, 2, 17, 18, 19 }))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(RichTextBoxScrollBars));
				}
				if (value != this.ScrollBars)
				{
					using (LayoutTransaction.CreateTransactionIf(this.AutoSize, this.ParentInternal, this, PropertyNames.ScrollBars))
					{
						this.richTextBoxFlags[RichTextBox.scrollBarsSection] = (int)value;
						base.RecreateHandle();
					}
				}
			}
		}

		/// <summary>Gets or sets the alignment to apply to the current selection or insertion point.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not one of the values defined in the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> class.</exception>
		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x06003620 RID: 13856 RVA: 0x000F410C File Offset: 0x000F230C
		// (set) Token: 0x06003621 RID: 13857 RVA: 0x000F4180 File Offset: 0x000F2380
		[DefaultValue(HorizontalAlignment.Left)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("RichTextBoxSelAlignment")]
		public HorizontalAlignment SelectionAlignment
		{
			get
			{
				HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left;
				this.ForceHandleCreate();
				NativeMethods.PARAFORMAT paraformat = new NativeMethods.PARAFORMAT();
				paraformat.rgxTabs = new int[32];
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1085, 0, paraformat);
				if ((8 & paraformat.dwMask) != 0)
				{
					switch (paraformat.wAlignment)
					{
					case 1:
						horizontalAlignment = HorizontalAlignment.Left;
						break;
					case 2:
						horizontalAlignment = HorizontalAlignment.Right;
						break;
					case 3:
						horizontalAlignment = HorizontalAlignment.Center;
						break;
					}
				}
				return horizontalAlignment;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(HorizontalAlignment));
				}
				this.ForceHandleCreate();
				NativeMethods.PARAFORMAT paraformat = new NativeMethods.PARAFORMAT();
				paraformat.dwMask = 8;
				switch (value)
				{
				case HorizontalAlignment.Left:
					paraformat.wAlignment = 1;
					break;
				case HorizontalAlignment.Right:
					paraformat.wAlignment = 2;
					break;
				case HorizontalAlignment.Center:
					paraformat.wAlignment = 3;
					break;
				}
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1095, 0, paraformat);
			}
		}

		/// <summary>Gets or sets a value indicating whether the bullet style is applied to the current selection or insertion point.</summary>
		/// <returns>
		///   <see langword="true" /> if the current selection or insertion point has the bullet style applied; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x06003622 RID: 13858 RVA: 0x000F420C File Offset: 0x000F240C
		// (set) Token: 0x06003623 RID: 13859 RVA: 0x000F426C File Offset: 0x000F246C
		[DefaultValue(false)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("RichTextBoxSelBullet")]
		public bool SelectionBullet
		{
			get
			{
				RichTextBoxSelectionAttribute richTextBoxSelectionAttribute = RichTextBoxSelectionAttribute.None;
				this.ForceHandleCreate();
				NativeMethods.PARAFORMAT paraformat = new NativeMethods.PARAFORMAT();
				paraformat.rgxTabs = new int[32];
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1085, 0, paraformat);
				if ((32 & paraformat.dwMask) != 0)
				{
					if (1 == paraformat.wNumbering)
					{
						richTextBoxSelectionAttribute = RichTextBoxSelectionAttribute.All;
					}
					return richTextBoxSelectionAttribute == RichTextBoxSelectionAttribute.All;
				}
				return false;
			}
			set
			{
				this.ForceHandleCreate();
				NativeMethods.PARAFORMAT paraformat = new NativeMethods.PARAFORMAT();
				paraformat.dwMask = 36;
				if (!value)
				{
					paraformat.wNumbering = 0;
					paraformat.dxOffset = 0;
				}
				else
				{
					paraformat.wNumbering = 1;
					paraformat.dxOffset = RichTextBox.Pixel2Twip(IntPtr.Zero, this.bulletIndent, true);
				}
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1095, 0, paraformat);
			}
		}

		/// <summary>Gets or sets whether text in the control appears on the baseline, as a superscript, or as a subscript below the baseline.</summary>
		/// <returns>A number that specifies the character offset.</returns>
		/// <exception cref="T:System.ArgumentException">The specified value was less than -2000 or greater than 2000.</exception>
		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x06003624 RID: 13860 RVA: 0x000F42D8 File Offset: 0x000F24D8
		// (set) Token: 0x06003625 RID: 13861 RVA: 0x000F4320 File Offset: 0x000F2520
		[DefaultValue(0)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("RichTextBoxSelCharOffset")]
		public int SelectionCharOffset
		{
			get
			{
				this.ForceHandleCreate();
				NativeMethods.CHARFORMATA charFormat = this.GetCharFormat(true);
				int num;
				if ((charFormat.dwMask & 268435456) != 0)
				{
					num = charFormat.yOffset;
				}
				else
				{
					num = charFormat.yOffset;
				}
				return RichTextBox.Twip2Pixel(IntPtr.Zero, num, false);
			}
			set
			{
				if (value > 2000 || value < -2000)
				{
					throw new ArgumentOutOfRangeException("SelectionCharOffset", SR.GetString("InvalidBoundArgument", new object[] { "SelectionCharOffset", value, -2000, 2000 }));
				}
				this.ForceHandleCreate();
				NativeMethods.CHARFORMATA charformata = new NativeMethods.CHARFORMATA();
				charformata.dwMask = 268435456;
				charformata.yOffset = RichTextBox.Pixel2Twip(IntPtr.Zero, value, false);
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1092, 1, charformata);
			}
		}

		/// <summary>Gets or sets the text color of the current text selection or insertion point.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color to apply to the current text selection or to text entered after the insertion point.</returns>
		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x06003626 RID: 13862 RVA: 0x000F43C8 File Offset: 0x000F25C8
		// (set) Token: 0x06003627 RID: 13863 RVA: 0x000F4404 File Offset: 0x000F2604
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("RichTextBoxSelColor")]
		public Color SelectionColor
		{
			get
			{
				Color color = Color.Empty;
				this.ForceHandleCreate();
				NativeMethods.CHARFORMATA charFormat = this.GetCharFormat(true);
				if ((charFormat.dwMask & 1073741824) != 0)
				{
					color = ColorTranslator.FromOle(charFormat.crTextColor);
				}
				return color;
			}
			set
			{
				this.ForceHandleCreate();
				NativeMethods.CHARFORMATA charFormat = this.GetCharFormat(true);
				charFormat.dwMask = 1073741824;
				charFormat.dwEffects = 0;
				charFormat.crTextColor = ColorTranslator.ToWin32(value);
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1092, 1, charFormat);
			}
		}

		/// <summary>Gets or sets the color of text when the text is selected in a <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the text color when the text is selected. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.</returns>
		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x06003628 RID: 13864 RVA: 0x000F4458 File Offset: 0x000F2658
		// (set) Token: 0x06003629 RID: 13865 RVA: 0x000F44B8 File Offset: 0x000F26B8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("RichTextBoxSelBackColor")]
		public Color SelectionBackColor
		{
			get
			{
				Color color = Color.Empty;
				if (base.IsHandleCreated)
				{
					NativeMethods.CHARFORMAT2A charFormat = this.GetCharFormat2(true);
					if ((charFormat.dwEffects & 67108864) != 0)
					{
						color = this.BackColor;
					}
					else if ((charFormat.dwMask & 67108864) != 0)
					{
						color = ColorTranslator.FromOle(charFormat.crBackColor);
					}
				}
				else
				{
					color = this.selectionBackColorToSetOnHandleCreated;
				}
				return color;
			}
			set
			{
				this.selectionBackColorToSetOnHandleCreated = value;
				if (base.IsHandleCreated)
				{
					NativeMethods.CHARFORMAT2A charformat2A = new NativeMethods.CHARFORMAT2A();
					if (value == Color.Empty)
					{
						charformat2A.dwEffects = 67108864;
					}
					else
					{
						charformat2A.dwMask = 67108864;
						charformat2A.crBackColor = ColorTranslator.ToWin32(value);
					}
					UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1092, 1, charformat2A);
				}
			}
		}

		/// <summary>Gets or sets the font of the current text selection or insertion point.</summary>
		/// <returns>A <see cref="T:System.Drawing.Font" /> that represents the font to apply to the current text selection or to text entered after the insertion point.</returns>
		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x0600362A RID: 13866 RVA: 0x000F4524 File Offset: 0x000F2724
		// (set) Token: 0x0600362B RID: 13867 RVA: 0x000F452D File Offset: 0x000F272D
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("RichTextBoxSelFont")]
		public Font SelectionFont
		{
			get
			{
				return this.GetCharFormatFont(true);
			}
			set
			{
				this.SetCharFormatFont(true, value);
			}
		}

		/// <summary>Gets or sets the distance between the left edge of the first line of text in the selected paragraph and the left edge of subsequent lines in the same paragraph.</summary>
		/// <returns>The distance, in pixels, for the hanging indent applied to the current text selection or the insertion point.</returns>
		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x0600362C RID: 13868 RVA: 0x000F4538 File Offset: 0x000F2738
		// (set) Token: 0x0600362D RID: 13869 RVA: 0x000F4598 File Offset: 0x000F2798
		[DefaultValue(0)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("RichTextBoxSelHangingIndent")]
		public int SelectionHangingIndent
		{
			get
			{
				int num = 0;
				this.ForceHandleCreate();
				NativeMethods.PARAFORMAT paraformat = new NativeMethods.PARAFORMAT();
				paraformat.rgxTabs = new int[32];
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1085, 0, paraformat);
				if ((4 & paraformat.dwMask) != 0)
				{
					num = paraformat.dxOffset;
				}
				return RichTextBox.Twip2Pixel(IntPtr.Zero, num, true);
			}
			set
			{
				this.ForceHandleCreate();
				NativeMethods.PARAFORMAT paraformat = new NativeMethods.PARAFORMAT();
				paraformat.dwMask = 4;
				paraformat.dxOffset = RichTextBox.Pixel2Twip(IntPtr.Zero, value, true);
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1095, 0, paraformat);
			}
		}

		/// <summary>Gets or sets the length, in pixels, of the indentation of the line where the selection starts.</summary>
		/// <returns>The current distance, in pixels, of the indentation applied to the left of the current text selection or the insertion point.</returns>
		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x0600362E RID: 13870 RVA: 0x000F45E4 File Offset: 0x000F27E4
		// (set) Token: 0x0600362F RID: 13871 RVA: 0x000F4644 File Offset: 0x000F2844
		[DefaultValue(0)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("RichTextBoxSelIndent")]
		public int SelectionIndent
		{
			get
			{
				int num = 0;
				this.ForceHandleCreate();
				NativeMethods.PARAFORMAT paraformat = new NativeMethods.PARAFORMAT();
				paraformat.rgxTabs = new int[32];
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1085, 0, paraformat);
				if ((1 & paraformat.dwMask) != 0)
				{
					num = paraformat.dxStartIndent;
				}
				return RichTextBox.Twip2Pixel(IntPtr.Zero, num, true);
			}
			set
			{
				this.ForceHandleCreate();
				NativeMethods.PARAFORMAT paraformat = new NativeMethods.PARAFORMAT();
				paraformat.dwMask = 1;
				paraformat.dxStartIndent = RichTextBox.Pixel2Twip(IntPtr.Zero, value, true);
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1095, 0, paraformat);
			}
		}

		/// <summary>Gets or sets the number of characters selected in control.</summary>
		/// <returns>The number of characters selected in the text box.</returns>
		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x06003630 RID: 13872 RVA: 0x000F468F File Offset: 0x000F288F
		// (set) Token: 0x06003631 RID: 13873 RVA: 0x000F46AB File Offset: 0x000F28AB
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TextBoxSelectionLengthDescr")]
		public override int SelectionLength
		{
			get
			{
				if (!base.IsHandleCreated)
				{
					return base.SelectionLength;
				}
				return this.SelectedText.Length;
			}
			set
			{
				base.SelectionLength = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the current text selection is protected.</summary>
		/// <returns>
		///   <see langword="true" /> if the current selection prevents any changes to its content; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x06003632 RID: 13874 RVA: 0x000F46B4 File Offset: 0x000F28B4
		// (set) Token: 0x06003633 RID: 13875 RVA: 0x000F46C9 File Offset: 0x000F28C9
		[DefaultValue(false)]
		[SRDescription("RichTextBoxSelProtected")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool SelectionProtected
		{
			get
			{
				this.ForceHandleCreate();
				return this.GetCharFormat(16, 16) == RichTextBoxSelectionAttribute.All;
			}
			set
			{
				this.ForceHandleCreate();
				this.SetCharFormat(16, value ? 16 : 0, RichTextBoxSelectionAttribute.All);
			}
		}

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x06003634 RID: 13876 RVA: 0x0001180C File Offset: 0x0000FA0C
		internal override bool SelectionUsesDbcsOffsetsInWin9x
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the currently selected rich text format (RTF) formatted text in the control.</summary>
		/// <returns>The selected RTF text in the control.</returns>
		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x06003635 RID: 13877 RVA: 0x000F46E3 File Offset: 0x000F28E3
		// (set) Token: 0x06003636 RID: 13878 RVA: 0x000F46F6 File Offset: 0x000F28F6
		[DefaultValue("")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("RichTextBoxSelRTF")]
		public string SelectedRtf
		{
			get
			{
				this.ForceHandleCreate();
				return this.StreamOut(32770);
			}
			set
			{
				this.ForceHandleCreate();
				if (value == null)
				{
					value = "";
				}
				this.StreamIn(value, 32770);
			}
		}

		/// <summary>The distance (in pixels) between the right edge of the <see cref="T:System.Windows.Forms.RichTextBox" /> control and the right edge of the text that is selected or added at the current insertion point.</summary>
		/// <returns>The indentation space, in pixels, at the right of the current selection or insertion point.</returns>
		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x06003637 RID: 13879 RVA: 0x000F4714 File Offset: 0x000F2914
		// (set) Token: 0x06003638 RID: 13880 RVA: 0x000F4774 File Offset: 0x000F2974
		[DefaultValue(0)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("RichTextBoxSelRightIndent")]
		public int SelectionRightIndent
		{
			get
			{
				int num = 0;
				this.ForceHandleCreate();
				NativeMethods.PARAFORMAT paraformat = new NativeMethods.PARAFORMAT();
				paraformat.rgxTabs = new int[32];
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1085, 0, paraformat);
				if ((2 & paraformat.dwMask) != 0)
				{
					num = paraformat.dxRightIndent;
				}
				return RichTextBox.Twip2Pixel(IntPtr.Zero, num, true);
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("SelectionRightIndent", SR.GetString("InvalidLowBoundArgumentEx", new object[] { "SelectionRightIndent", value, 0 }));
				}
				this.ForceHandleCreate();
				NativeMethods.PARAFORMAT paraformat = new NativeMethods.PARAFORMAT();
				paraformat.dwMask = 2;
				paraformat.dxRightIndent = RichTextBox.Pixel2Twip(IntPtr.Zero, value, true);
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1095, 0, paraformat);
			}
		}

		/// <summary>Gets or sets the absolute tab stop positions in a <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
		/// <returns>An array in which each member specifies a tab offset, in pixels.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The array has more than the maximum 32 elements.</exception>
		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x06003639 RID: 13881 RVA: 0x000F47F8 File Offset: 0x000F29F8
		// (set) Token: 0x0600363A RID: 13882 RVA: 0x000F4880 File Offset: 0x000F2A80
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("RichTextBoxSelTabs")]
		public int[] SelectionTabs
		{
			get
			{
				int[] array = new int[0];
				this.ForceHandleCreate();
				NativeMethods.PARAFORMAT paraformat = new NativeMethods.PARAFORMAT();
				paraformat.rgxTabs = new int[32];
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1085, 0, paraformat);
				if ((16 & paraformat.dwMask) != 0)
				{
					array = new int[(int)paraformat.cTabCount];
					for (int i = 0; i < (int)paraformat.cTabCount; i++)
					{
						array[i] = RichTextBox.Twip2Pixel(IntPtr.Zero, paraformat.rgxTabs[i], true);
					}
				}
				return array;
			}
			set
			{
				if (value != null && value.Length > 32)
				{
					throw new ArgumentOutOfRangeException("SelectionTabs", SR.GetString("SelTabCountRange"));
				}
				this.ForceHandleCreate();
				NativeMethods.PARAFORMAT paraformat = new NativeMethods.PARAFORMAT();
				paraformat.rgxTabs = new int[32];
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1085, 0, paraformat);
				paraformat.cTabCount = (short)((value == null) ? 0 : value.Length);
				paraformat.dwMask = 16;
				for (int i = 0; i < (int)paraformat.cTabCount; i++)
				{
					paraformat.rgxTabs[i] = RichTextBox.Pixel2Twip(IntPtr.Zero, value[i], true);
				}
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1095, 0, paraformat);
			}
		}

		/// <summary>Gets or sets the selected text within the <see cref="T:System.Windows.Forms.RichTextBox" />.</summary>
		/// <returns>A string that represents the selected text in the control.</returns>
		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x0600363B RID: 13883 RVA: 0x000F4938 File Offset: 0x000F2B38
		// (set) Token: 0x0600363C RID: 13884 RVA: 0x000F4958 File Offset: 0x000F2B58
		[DefaultValue("")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("RichTextBoxSelText")]
		public override string SelectedText
		{
			get
			{
				this.ForceHandleCreate();
				return this.StreamOut(32785);
			}
			set
			{
				this.ForceHandleCreate();
				this.StreamIn(value, 32785);
			}
		}

		/// <summary>Gets the selection type within the control.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.RichTextBoxSelectionTypes" /> values.</returns>
		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x0600363D RID: 13885 RVA: 0x000F496C File Offset: 0x000F2B6C
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("RichTextBoxSelTypeDescr")]
		public RichTextBoxSelectionTypes SelectionType
		{
			get
			{
				this.ForceHandleCreate();
				if (this.SelectionLength > 0)
				{
					return (RichTextBoxSelectionTypes)(long)base.SendMessage(1090, 0, 0);
				}
				return RichTextBoxSelectionTypes.Empty;
			}
		}

		/// <summary>Gets or sets a value indicating whether a selection margin is displayed in the <see cref="T:System.Windows.Forms.RichTextBox" />.</summary>
		/// <returns>
		///   <see langword="true" /> if a selection margin is enabled in the control; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x0600363E RID: 13886 RVA: 0x000F499F File Offset: 0x000F2B9F
		// (set) Token: 0x0600363F RID: 13887 RVA: 0x000F49B4 File Offset: 0x000F2BB4
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("RichTextBoxSelMargin")]
		public bool ShowSelectionMargin
		{
			get
			{
				return this.richTextBoxFlags[RichTextBox.showSelBarSection] != 0;
			}
			set
			{
				if (value != this.ShowSelectionMargin)
				{
					this.richTextBoxFlags[RichTextBox.showSelBarSection] = (value ? 1 : 0);
					if (base.IsHandleCreated)
					{
						base.SendMessage(1101, value ? 2 : 4, 16777216);
					}
				}
			}
		}

		/// <summary>Gets or sets the current text in the rich text box.</summary>
		/// <returns>The text displayed in the control.</returns>
		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x06003640 RID: 13888 RVA: 0x000F4A04 File Offset: 0x000F2C04
		// (set) Token: 0x06003641 RID: 13889 RVA: 0x000F4A6C File Offset: 0x000F2C6C
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.All)]
		public override string Text
		{
			get
			{
				if (base.IsDisposed)
				{
					return base.Text;
				}
				if (base.RecreatingHandle || base.GetAnyDisposingInHierarchy())
				{
					return "";
				}
				if (base.IsHandleCreated || this.textRtf != null)
				{
					this.ForceHandleCreate();
					return this.StreamOut(17);
				}
				if (this.textPlain != null)
				{
					return this.textPlain;
				}
				return base.Text;
			}
			set
			{
				using (LayoutTransaction.CreateTransactionIf(this.AutoSize, this.ParentInternal, this, PropertyNames.Text))
				{
					this.textRtf = null;
					if (!base.IsHandleCreated)
					{
						this.textPlain = value;
					}
					else
					{
						this.textPlain = null;
						if (value == null)
						{
							value = "";
						}
						this.StreamIn(value, 17);
						base.SendMessage(185, 0, 0);
					}
				}
			}
		}

		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x06003642 RID: 13890 RVA: 0x000F4AF0 File Offset: 0x000F2CF0
		// (set) Token: 0x06003643 RID: 13891 RVA: 0x000F4B08 File Offset: 0x000F2D08
		private bool SuppressTextChangedEvent
		{
			get
			{
				return this.richTextBoxFlags[RichTextBox.suppressTextChangedEventSection] != 0;
			}
			set
			{
				bool suppressTextChangedEvent = this.SuppressTextChangedEvent;
				if (value != suppressTextChangedEvent)
				{
					this.richTextBoxFlags[RichTextBox.suppressTextChangedEventSection] = (value ? 1 : 0);
					CommonProperties.xClearPreferredSizeCache(this);
				}
			}
		}

		/// <summary>Gets the length of text in the control.</summary>
		/// <returns>The number of characters contained in the text of the control.</returns>
		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x06003644 RID: 13892 RVA: 0x000F4B40 File Offset: 0x000F2D40
		[Browsable(false)]
		public override int TextLength
		{
			get
			{
				NativeMethods.GETTEXTLENGTHEX gettextlengthex = new NativeMethods.GETTEXTLENGTHEX();
				gettextlengthex.flags = 8U;
				if (Marshal.SystemDefaultCharSize == 1)
				{
					gettextlengthex.codepage = 0U;
				}
				else
				{
					gettextlengthex.codepage = 1200U;
				}
				return (int)(long)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1119, gettextlengthex, 0);
			}
		}

		/// <summary>Gets the name of the action that can be undone in the control when the <see cref="M:System.Windows.Forms.TextBoxBase.Undo" /> method is called.</summary>
		/// <returns>The text name of the action that can be undone.</returns>
		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x06003645 RID: 13893 RVA: 0x000F4B94 File Offset: 0x000F2D94
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("RichTextBoxUndoActionNameDescr")]
		public string UndoActionName
		{
			get
			{
				if (!base.CanUndo)
				{
					return "";
				}
				int num = (int)(long)base.SendMessage(1110, 0, 0);
				return this.GetEditorActionName(num);
			}
		}

		// Token: 0x06003646 RID: 13894 RVA: 0x000F4BCC File Offset: 0x000F2DCC
		private string GetEditorActionName(int actionID)
		{
			switch (actionID)
			{
			default:
				return SR.GetString("RichTextBox_IDUnknown");
			case 1:
				return SR.GetString("RichTextBox_IDTyping");
			case 2:
				return SR.GetString("RichTextBox_IDDelete");
			case 3:
				return SR.GetString("RichTextBox_IDDragDrop");
			case 4:
				return SR.GetString("RichTextBox_IDCut");
			case 5:
				return SR.GetString("RichTextBox_IDPaste");
			}
		}

		/// <summary>Gets or sets the current zoom level of the <see cref="T:System.Windows.Forms.RichTextBox" />.</summary>
		/// <returns>The factor by which the contents of the control is zoomed.</returns>
		/// <exception cref="T:System.ArgumentException">The specified zoom factor did not fall within the permissible range.</exception>
		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x06003647 RID: 13895 RVA: 0x000F4C38 File Offset: 0x000F2E38
		// (set) Token: 0x06003648 RID: 13896 RVA: 0x000F4C8C File Offset: 0x000F2E8C
		[SRCategory("CatBehavior")]
		[DefaultValue(1f)]
		[Localizable(true)]
		[SRDescription("RichTextBoxZoomFactor")]
		public float ZoomFactor
		{
			get
			{
				if (base.IsHandleCreated)
				{
					int num = 0;
					int num2 = 0;
					base.SendMessage(1248, ref num, ref num2);
					if (num != 0 && num2 != 0)
					{
						this.zoomMultiplier = (float)num / (float)num2;
					}
					else
					{
						this.zoomMultiplier = 1f;
					}
					return this.zoomMultiplier;
				}
				return this.zoomMultiplier;
			}
			set
			{
				if (this.zoomMultiplier == value)
				{
					return;
				}
				if (value <= 0.015625f || value >= 64f)
				{
					throw new ArgumentOutOfRangeException("ZoomFactor", SR.GetString("InvalidExBoundArgument", new object[]
					{
						"ZoomFactor",
						value.ToString(CultureInfo.CurrentCulture),
						0.015625f.ToString(CultureInfo.CurrentCulture),
						64f.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.SendZoomFactor(value);
			}
		}

		/// <summary>Occurs when contents within the control are resized.</summary>
		// Token: 0x1400028B RID: 651
		// (add) Token: 0x06003649 RID: 13897 RVA: 0x000F4D16 File Offset: 0x000F2F16
		// (remove) Token: 0x0600364A RID: 13898 RVA: 0x000F4D29 File Offset: 0x000F2F29
		[SRCategory("CatBehavior")]
		[SRDescription("RichTextBoxContentsResized")]
		public event ContentsResizedEventHandler ContentsResized
		{
			add
			{
				base.Events.AddHandler(RichTextBox.EVENT_REQUESTRESIZE, value);
			}
			remove
			{
				base.Events.RemoveHandler(RichTextBox.EVENT_REQUESTRESIZE, value);
			}
		}

		/// <summary>Occurs when the user completes a drag-and-drop</summary>
		// Token: 0x1400028C RID: 652
		// (add) Token: 0x0600364B RID: 13899 RVA: 0x000F4D3C File Offset: 0x000F2F3C
		// (remove) Token: 0x0600364C RID: 13900 RVA: 0x000F4D45 File Offset: 0x000F2F45
		[Browsable(false)]
		public new event DragEventHandler DragDrop
		{
			add
			{
				base.DragDrop += value;
			}
			remove
			{
				base.DragDrop -= value;
			}
		}

		/// <summary>Occurs when an object is dragged into the control's bounds.</summary>
		// Token: 0x1400028D RID: 653
		// (add) Token: 0x0600364D RID: 13901 RVA: 0x000F4D4E File Offset: 0x000F2F4E
		// (remove) Token: 0x0600364E RID: 13902 RVA: 0x000F4D57 File Offset: 0x000F2F57
		[Browsable(false)]
		public new event DragEventHandler DragEnter
		{
			add
			{
				base.DragEnter += value;
			}
			remove
			{
				base.DragEnter -= value;
			}
		}

		/// <summary>Occurs when an object is dragged out of the control's bounds.</summary>
		// Token: 0x1400028E RID: 654
		// (add) Token: 0x0600364F RID: 13903 RVA: 0x000F4D60 File Offset: 0x000F2F60
		// (remove) Token: 0x06003650 RID: 13904 RVA: 0x000F4D69 File Offset: 0x000F2F69
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler DragLeave
		{
			add
			{
				base.DragLeave += value;
			}
			remove
			{
				base.DragLeave -= value;
			}
		}

		/// <summary>Occurs when an object is dragged over the control's bounds.</summary>
		// Token: 0x1400028F RID: 655
		// (add) Token: 0x06003651 RID: 13905 RVA: 0x000F4D72 File Offset: 0x000F2F72
		// (remove) Token: 0x06003652 RID: 13906 RVA: 0x000F4D7B File Offset: 0x000F2F7B
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event DragEventHandler DragOver
		{
			add
			{
				base.DragOver += value;
			}
			remove
			{
				base.DragOver -= value;
			}
		}

		/// <summary>Occurs during a drag operation.</summary>
		// Token: 0x14000290 RID: 656
		// (add) Token: 0x06003653 RID: 13907 RVA: 0x000F4D84 File Offset: 0x000F2F84
		// (remove) Token: 0x06003654 RID: 13908 RVA: 0x000F4D8D File Offset: 0x000F2F8D
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event GiveFeedbackEventHandler GiveFeedback
		{
			add
			{
				base.GiveFeedback += value;
			}
			remove
			{
				base.GiveFeedback -= value;
			}
		}

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x14000291 RID: 657
		// (add) Token: 0x06003655 RID: 13909 RVA: 0x000F4D96 File Offset: 0x000F2F96
		// (remove) Token: 0x06003656 RID: 13910 RVA: 0x000F4D9F File Offset: 0x000F2F9F
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event QueryContinueDragEventHandler QueryContinueDrag
		{
			add
			{
				base.QueryContinueDrag += value;
			}
			remove
			{
				base.QueryContinueDrag -= value;
			}
		}

		/// <summary>Occurs when the user clicks the horizontal scroll bar of the control.</summary>
		// Token: 0x14000292 RID: 658
		// (add) Token: 0x06003657 RID: 13911 RVA: 0x000F4DA8 File Offset: 0x000F2FA8
		// (remove) Token: 0x06003658 RID: 13912 RVA: 0x000F4DBB File Offset: 0x000F2FBB
		[SRCategory("CatBehavior")]
		[SRDescription("RichTextBoxHScroll")]
		public event EventHandler HScroll
		{
			add
			{
				base.Events.AddHandler(RichTextBox.EVENT_HSCROLL, value);
			}
			remove
			{
				base.Events.RemoveHandler(RichTextBox.EVENT_HSCROLL, value);
			}
		}

		/// <summary>Occurs when the user clicks on a link within the text of the control.</summary>
		// Token: 0x14000293 RID: 659
		// (add) Token: 0x06003659 RID: 13913 RVA: 0x000F4DCE File Offset: 0x000F2FCE
		// (remove) Token: 0x0600365A RID: 13914 RVA: 0x000F4DE1 File Offset: 0x000F2FE1
		[SRCategory("CatBehavior")]
		[SRDescription("RichTextBoxLinkClick")]
		public event LinkClickedEventHandler LinkClicked
		{
			add
			{
				base.Events.AddHandler(RichTextBox.EVENT_LINKACTIVATE, value);
			}
			remove
			{
				base.Events.RemoveHandler(RichTextBox.EVENT_LINKACTIVATE, value);
			}
		}

		/// <summary>Occurs when the user switches input methods on an Asian version of the Windows operating system.</summary>
		// Token: 0x14000294 RID: 660
		// (add) Token: 0x0600365B RID: 13915 RVA: 0x000F4DF4 File Offset: 0x000F2FF4
		// (remove) Token: 0x0600365C RID: 13916 RVA: 0x000F4E07 File Offset: 0x000F3007
		[SRCategory("CatBehavior")]
		[SRDescription("RichTextBoxIMEChange")]
		public event EventHandler ImeChange
		{
			add
			{
				base.Events.AddHandler(RichTextBox.EVENT_IMECHANGE, value);
			}
			remove
			{
				base.Events.RemoveHandler(RichTextBox.EVENT_IMECHANGE, value);
			}
		}

		/// <summary>Occurs when the user attempts to modify protected text in the control.</summary>
		// Token: 0x14000295 RID: 661
		// (add) Token: 0x0600365D RID: 13917 RVA: 0x000F4E1A File Offset: 0x000F301A
		// (remove) Token: 0x0600365E RID: 13918 RVA: 0x000F4E2D File Offset: 0x000F302D
		[SRCategory("CatBehavior")]
		[SRDescription("RichTextBoxProtected")]
		public event EventHandler Protected
		{
			add
			{
				base.Events.AddHandler(RichTextBox.EVENT_PROTECTED, value);
			}
			remove
			{
				base.Events.RemoveHandler(RichTextBox.EVENT_PROTECTED, value);
			}
		}

		/// <summary>Occurs when the selection of text within the control has changed.</summary>
		// Token: 0x14000296 RID: 662
		// (add) Token: 0x0600365F RID: 13919 RVA: 0x000F4E40 File Offset: 0x000F3040
		// (remove) Token: 0x06003660 RID: 13920 RVA: 0x000F4E53 File Offset: 0x000F3053
		[SRCategory("CatBehavior")]
		[SRDescription("RichTextBoxSelChange")]
		public event EventHandler SelectionChanged
		{
			add
			{
				base.Events.AddHandler(RichTextBox.EVENT_SELCHANGE, value);
			}
			remove
			{
				base.Events.RemoveHandler(RichTextBox.EVENT_SELCHANGE, value);
			}
		}

		/// <summary>Occurs when the user clicks the vertical scroll bars of the control.</summary>
		// Token: 0x14000297 RID: 663
		// (add) Token: 0x06003661 RID: 13921 RVA: 0x000F4E66 File Offset: 0x000F3066
		// (remove) Token: 0x06003662 RID: 13922 RVA: 0x000F4E79 File Offset: 0x000F3079
		[SRCategory("CatBehavior")]
		[SRDescription("RichTextBoxVScroll")]
		public event EventHandler VScroll
		{
			add
			{
				base.Events.AddHandler(RichTextBox.EVENT_VSCROLL, value);
			}
			remove
			{
				base.Events.RemoveHandler(RichTextBox.EVENT_VSCROLL, value);
			}
		}

		/// <summary>Determines whether you can paste information from the Clipboard in the specified data format.</summary>
		/// <param name="clipFormat">One of the <see cref="T:System.Windows.Forms.DataFormats.Format" /> values.</param>
		/// <returns>
		///   <see langword="true" /> if you can paste data from the Clipboard in the specified data format; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003663 RID: 13923 RVA: 0x000F4E8C File Offset: 0x000F308C
		public bool CanPaste(DataFormats.Format clipFormat)
		{
			return (int)(long)base.SendMessage(1074, clipFormat.Id, 0) != 0;
		}

		/// <summary>This method is not relevant for this class.</summary>
		/// <param name="bitmap">A <see cref="T:System.Drawing.Bitmap" />.</param>
		/// <param name="targetBounds">A <see cref="T:System.Drawing.Rectangle" />.</param>
		// Token: 0x06003664 RID: 13924 RVA: 0x0001AAF9 File Offset: 0x00018CF9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new void DrawToBitmap(Bitmap bitmap, Rectangle targetBounds)
		{
			base.DrawToBitmap(bitmap, targetBounds);
		}

		// Token: 0x06003665 RID: 13925 RVA: 0x000F4EB8 File Offset: 0x000F30B8
		private unsafe int EditStreamProc(IntPtr dwCookie, IntPtr buf, int cb, out int transferred)
		{
			int num = 0;
			byte[] array = new byte[cb];
			int num2 = (int)dwCookie;
			transferred = 0;
			try
			{
				int num3 = num2 & 3;
				if (num3 != 1)
				{
					if (num3 == 2)
					{
						if (this.editStream == null)
						{
							this.editStream = new MemoryStream();
						}
						int num4 = num2 & 112;
						if (num4 != 16)
						{
							if (num4 == 32 || num4 == 64)
							{
								Marshal.Copy(buf, array, 0, cb);
								this.editStream.Write(array, 0, cb);
							}
						}
						else if ((num2 & 8) != 0)
						{
							int num5 = cb / 2;
							int num6 = 0;
							try
							{
								byte[] array2;
								byte* ptr;
								if ((array2 = array) == null || array2.Length == 0)
								{
									ptr = null;
								}
								else
								{
									ptr = &array2[0];
								}
								char* ptr2 = (char*)ptr;
								char* ptr3 = (long)buf;
								for (int i = 0; i < num5; i++)
								{
									if (*ptr3 == '\r')
									{
										ptr3++;
									}
									else
									{
										*ptr2 = *ptr3;
										ptr2++;
										ptr3++;
										num6++;
									}
								}
							}
							finally
							{
								byte[] array2 = null;
							}
							this.editStream.Write(array, 0, num6 * 2);
						}
						else
						{
							int num7 = 0;
							try
							{
								byte[] array2;
								byte* ptr4;
								if ((array2 = array) == null || array2.Length == 0)
								{
									ptr4 = null;
								}
								else
								{
									ptr4 = &array2[0];
								}
								byte* ptr5 = ptr4;
								byte* ptr6 = (long)buf;
								for (int j = 0; j < cb; j++)
								{
									if (*ptr6 == 13)
									{
										ptr6++;
									}
									else
									{
										*ptr5 = *ptr6;
										ptr5++;
										ptr6++;
										num7++;
									}
								}
							}
							finally
							{
								byte[] array2 = null;
							}
							this.editStream.Write(array, 0, num7);
						}
						transferred = cb;
					}
				}
				else if (this.editStream != null)
				{
					transferred = this.editStream.Read(array, 0, cb);
					Marshal.Copy(array, 0, buf, transferred);
					if (transferred < 0)
					{
						transferred = 0;
					}
				}
				else
				{
					transferred = 0;
				}
			}
			catch (IOException)
			{
				transferred = 0;
				num = 1;
			}
			return num;
		}

		/// <summary>Searches the text in a <see cref="T:System.Windows.Forms.RichTextBox" /> control for a string.</summary>
		/// <param name="str">The text to locate in the control.</param>
		/// <returns>The location within the control where the search text was found or -1 if the search string is not found or an empty search string is specified in the <paramref name="str" /> parameter.</returns>
		// Token: 0x06003666 RID: 13926 RVA: 0x000F50D0 File Offset: 0x000F32D0
		public int Find(string str)
		{
			return this.Find(str, 0, 0, RichTextBoxFinds.None);
		}

		/// <summary>Searches the text in a <see cref="T:System.Windows.Forms.RichTextBox" /> control for a string with specific options applied to the search.</summary>
		/// <param name="str">The text to locate in the control.</param>
		/// <param name="options">A bitwise combination of the <see cref="T:System.Windows.Forms.RichTextBoxFinds" /> values.</param>
		/// <returns>The location within the control where the search text was found.</returns>
		// Token: 0x06003667 RID: 13927 RVA: 0x000F50DC File Offset: 0x000F32DC
		public int Find(string str, RichTextBoxFinds options)
		{
			return this.Find(str, 0, 0, options);
		}

		/// <summary>Searches the text in a <see cref="T:System.Windows.Forms.RichTextBox" /> control for a string at a specific location within the control and with specific options applied to the search.</summary>
		/// <param name="str">The text to locate in the control.</param>
		/// <param name="start">The location within the control's text at which to begin searching.</param>
		/// <param name="options">A bitwise combination of the <see cref="T:System.Windows.Forms.RichTextBoxFinds" /> values.</param>
		/// <returns>The location within the control where the search text was found.</returns>
		// Token: 0x06003668 RID: 13928 RVA: 0x000F50E8 File Offset: 0x000F32E8
		public int Find(string str, int start, RichTextBoxFinds options)
		{
			return this.Find(str, start, -1, options);
		}

		/// <summary>Searches the text in a <see cref="T:System.Windows.Forms.RichTextBox" /> control for a string within a range of text within the control and with specific options applied to the search.</summary>
		/// <param name="str">The text to locate in the control.</param>
		/// <param name="start">The location within the control's text at which to begin searching.</param>
		/// <param name="end">The location within the control's text at which to end searching. This value must be equal to negative one (-1) or greater than or equal to the <paramref name="start" /> parameter.</param>
		/// <param name="options">A bitwise combination of the <see cref="T:System.Windows.Forms.RichTextBoxFinds" /> values.</param>
		/// <returns>The location within the control where the search text was found.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="str" /> parameter was <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="start" /> parameter was less than zero.
		/// -or-
		/// The <paramref name="end" /> parameter was less the <paramref name="start" /> parameter.</exception>
		// Token: 0x06003669 RID: 13929 RVA: 0x000F50F4 File Offset: 0x000F32F4
		public int Find(string str, int start, int end, RichTextBoxFinds options)
		{
			int textLength = this.TextLength;
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (start < 0 || start > textLength)
			{
				throw new ArgumentOutOfRangeException("start", SR.GetString("InvalidBoundArgument", new object[] { "start", start, 0, textLength }));
			}
			if (end < -1)
			{
				throw new ArgumentOutOfRangeException("end", SR.GetString("RichTextFindEndInvalid", new object[] { end }));
			}
			bool flag = true;
			NativeMethods.FINDTEXT findtext = new NativeMethods.FINDTEXT();
			findtext.chrg = new NativeMethods.CHARRANGE();
			findtext.lpstrText = str;
			if (end == -1)
			{
				end = textLength;
			}
			if (start > end)
			{
				throw new ArgumentException(SR.GetString("RichTextFindEndInvalid", new object[] { end }));
			}
			if ((options & RichTextBoxFinds.Reverse) != RichTextBoxFinds.Reverse)
			{
				findtext.chrg.cpMin = start;
				findtext.chrg.cpMax = end;
			}
			else
			{
				findtext.chrg.cpMin = end;
				findtext.chrg.cpMax = start;
			}
			if (findtext.chrg.cpMin == findtext.chrg.cpMax)
			{
				if ((options & RichTextBoxFinds.Reverse) != RichTextBoxFinds.Reverse)
				{
					findtext.chrg.cpMin = 0;
					findtext.chrg.cpMax = -1;
				}
				else
				{
					findtext.chrg.cpMin = textLength;
					findtext.chrg.cpMax = 0;
				}
			}
			int num = 0;
			if ((options & RichTextBoxFinds.WholeWord) == RichTextBoxFinds.WholeWord)
			{
				num |= 2;
			}
			if ((options & RichTextBoxFinds.MatchCase) == RichTextBoxFinds.MatchCase)
			{
				num |= 4;
			}
			if ((options & RichTextBoxFinds.NoHighlight) == RichTextBoxFinds.NoHighlight)
			{
				flag = false;
			}
			if ((options & RichTextBoxFinds.Reverse) != RichTextBoxFinds.Reverse)
			{
				num |= 1;
			}
			int num2 = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1080, num, findtext);
			if (num2 != -1 && flag)
			{
				NativeMethods.CHARRANGE charrange = new NativeMethods.CHARRANGE();
				charrange.cpMin = num2;
				char c = 'ـ';
				string text = this.Text;
				string text2 = text.Substring(num2, str.Length);
				int num3 = text2.IndexOf(c);
				if (num3 == -1)
				{
					charrange.cpMax = num2 + str.Length;
				}
				else
				{
					int i = num3;
					int num4 = num2 + num3;
					while (i < str.Length)
					{
						while (text[num4] == c && str[i] != c)
						{
							num4++;
						}
						i++;
						num4++;
					}
					charrange.cpMax = num4;
				}
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1079, 0, charrange);
				base.SendMessage(183, 0, 0);
			}
			return num2;
		}

		/// <summary>Searches the text of a <see cref="T:System.Windows.Forms.RichTextBox" /> control for the first instance of a character from a list of characters.</summary>
		/// <param name="characterSet">The array of characters to search for.</param>
		/// <returns>The location within the control where the search characters were found or -1 if the search characters are not found or an empty search character set is specified in the <paramref name="char" /> parameter.</returns>
		// Token: 0x0600366A RID: 13930 RVA: 0x000F5379 File Offset: 0x000F3579
		public int Find(char[] characterSet)
		{
			return this.Find(characterSet, 0, -1);
		}

		/// <summary>Searches the text of a <see cref="T:System.Windows.Forms.RichTextBox" /> control, at a specific starting point, for the first instance of a character from a list of characters.</summary>
		/// <param name="characterSet">The array of characters to search for.</param>
		/// <param name="start">The location within the control's text at which to begin searching.</param>
		/// <returns>The location within the control where the search characters are found.</returns>
		// Token: 0x0600366B RID: 13931 RVA: 0x000F5384 File Offset: 0x000F3584
		public int Find(char[] characterSet, int start)
		{
			return this.Find(characterSet, start, -1);
		}

		/// <summary>Searches a range of text in a <see cref="T:System.Windows.Forms.RichTextBox" /> control for the first instance of a character from a list of characters.</summary>
		/// <param name="characterSet">The array of characters to search for.</param>
		/// <param name="start">The location within the control's text at which to begin searching.</param>
		/// <param name="end">The location within the control's text at which to end searching.</param>
		/// <returns>The location within the control where the search characters are found.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="characterSet" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="start" /> is less than 0 or greater than the length of the text in the control.</exception>
		// Token: 0x0600366C RID: 13932 RVA: 0x000F5390 File Offset: 0x000F3590
		public int Find(char[] characterSet, int start, int end)
		{
			bool flag = true;
			bool flag2 = false;
			int textLength = this.TextLength;
			if (characterSet == null)
			{
				throw new ArgumentNullException("characterSet");
			}
			if (start < 0 || start > textLength)
			{
				throw new ArgumentOutOfRangeException("start", SR.GetString("InvalidBoundArgument", new object[] { "start", start, 0, textLength }));
			}
			if (end < start && end != -1)
			{
				throw new ArgumentOutOfRangeException("end", SR.GetString("InvalidLowBoundArgumentEx", new object[] { "end", end, "start" }));
			}
			if (characterSet.Length == 0)
			{
				return -1;
			}
			int windowTextLength = SafeNativeMethods.GetWindowTextLength(new HandleRef(this, base.Handle));
			if (start == end)
			{
				start = 0;
				end = windowTextLength;
			}
			if (end == -1)
			{
				end = windowTextLength;
			}
			NativeMethods.CHARRANGE charrange = new NativeMethods.CHARRANGE();
			charrange.cpMax = (charrange.cpMin = start);
			NativeMethods.TEXTRANGE textrange = new NativeMethods.TEXTRANGE();
			textrange.chrg = new NativeMethods.CHARRANGE();
			textrange.chrg.cpMin = charrange.cpMin;
			textrange.chrg.cpMax = charrange.cpMax;
			UnsafeNativeMethods.CharBuffer charBuffer = UnsafeNativeMethods.CharBuffer.CreateBuffer(513);
			textrange.lpstrText = charBuffer.AllocCoTaskMem();
			if (textrange.lpstrText == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			try
			{
				bool flag3 = false;
				while (!flag3)
				{
					if (flag)
					{
						textrange.chrg.cpMin = charrange.cpMax;
						textrange.chrg.cpMax += 512;
					}
					else
					{
						textrange.chrg.cpMax = charrange.cpMin;
						textrange.chrg.cpMin -= 512;
						if (textrange.chrg.cpMin < 0)
						{
							textrange.chrg.cpMin = 0;
						}
					}
					if (end != -1)
					{
						textrange.chrg.cpMax = Math.Min(textrange.chrg.cpMax, end);
					}
					int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1099, 0, textrange);
					if (num == 0)
					{
						charrange.cpMax = (charrange.cpMin = -1);
						break;
					}
					charBuffer.PutCoTaskMem(textrange.lpstrText);
					string @string = charBuffer.GetString();
					if (flag)
					{
						for (int i = 0; i < num; i++)
						{
							bool charInCharSet = this.GetCharInCharSet(@string[i], characterSet, flag2);
							if (charInCharSet)
							{
								flag3 = true;
								break;
							}
							charrange.cpMax++;
						}
					}
					else
					{
						int num2 = num;
						while (num2-- != 0)
						{
							bool charInCharSet2 = this.GetCharInCharSet(@string[num2], characterSet, flag2);
							if (charInCharSet2)
							{
								flag3 = true;
								break;
							}
							charrange.cpMin--;
						}
					}
				}
			}
			finally
			{
				if (textrange.lpstrText != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(textrange.lpstrText);
				}
			}
			return flag ? charrange.cpMax : charrange.cpMin;
		}

		// Token: 0x0600366D RID: 13933 RVA: 0x000F56B8 File Offset: 0x000F38B8
		private void ForceHandleCreate()
		{
			if (!base.IsHandleCreated)
			{
				this.CreateHandle();
			}
		}

		// Token: 0x0600366E RID: 13934 RVA: 0x000F56C8 File Offset: 0x000F38C8
		private bool InternalSetForeColor(Color value)
		{
			NativeMethods.CHARFORMATA charFormat = this.GetCharFormat(false);
			if ((charFormat.dwMask & 1073741824) != 0 && ColorTranslator.ToWin32(value) == charFormat.crTextColor)
			{
				return true;
			}
			charFormat.dwMask = 1073741824;
			charFormat.dwEffects = 0;
			charFormat.crTextColor = ColorTranslator.ToWin32(value);
			return this.SetCharFormat(4, charFormat);
		}

		// Token: 0x0600366F RID: 13935 RVA: 0x000F5724 File Offset: 0x000F3924
		private NativeMethods.CHARFORMATA GetCharFormat(bool fSelection)
		{
			NativeMethods.CHARFORMATA charformata = new NativeMethods.CHARFORMATA();
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1082, fSelection ? 1 : 0, charformata);
			return charformata;
		}

		// Token: 0x06003670 RID: 13936 RVA: 0x000F5758 File Offset: 0x000F3958
		private NativeMethods.CHARFORMAT2A GetCharFormat2(bool fSelection)
		{
			NativeMethods.CHARFORMAT2A charformat2A = new NativeMethods.CHARFORMAT2A();
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1082, fSelection ? 1 : 0, charformat2A);
			return charformat2A;
		}

		// Token: 0x06003671 RID: 13937 RVA: 0x000F578C File Offset: 0x000F398C
		private RichTextBoxSelectionAttribute GetCharFormat(int mask, int effect)
		{
			RichTextBoxSelectionAttribute richTextBoxSelectionAttribute = RichTextBoxSelectionAttribute.None;
			if (base.IsHandleCreated)
			{
				NativeMethods.CHARFORMATA charFormat = this.GetCharFormat(true);
				if ((charFormat.dwMask & mask) != 0 && (charFormat.dwEffects & effect) != 0)
				{
					richTextBoxSelectionAttribute = RichTextBoxSelectionAttribute.All;
				}
			}
			return richTextBoxSelectionAttribute;
		}

		// Token: 0x06003672 RID: 13938 RVA: 0x000F57C4 File Offset: 0x000F39C4
		private Font GetCharFormatFont(bool selectionOnly)
		{
			this.ForceHandleCreate();
			NativeMethods.CHARFORMATA charFormat = this.GetCharFormat(selectionOnly);
			if ((charFormat.dwMask & 536870912) == 0)
			{
				return null;
			}
			string text = Encoding.Default.GetString(charFormat.szFaceName);
			int num = text.IndexOf('\0');
			if (num != -1)
			{
				text = text.Substring(0, num);
			}
			float num2 = 13f;
			if ((charFormat.dwMask & -2147483648) != 0)
			{
				num2 = (float)charFormat.yHeight / 20f;
				if (num2 == 0f && charFormat.yHeight > 0)
				{
					num2 = 1f;
				}
			}
			FontStyle fontStyle = FontStyle.Regular;
			if ((charFormat.dwMask & 1) != 0 && (charFormat.dwEffects & 1) != 0)
			{
				fontStyle |= FontStyle.Bold;
			}
			if ((charFormat.dwMask & 2) != 0 && (charFormat.dwEffects & 2) != 0)
			{
				fontStyle |= FontStyle.Italic;
			}
			if ((charFormat.dwMask & 8) != 0 && (charFormat.dwEffects & 8) != 0)
			{
				fontStyle |= FontStyle.Strikeout;
			}
			if ((charFormat.dwMask & 4) != 0 && (charFormat.dwEffects & 4) != 0)
			{
				fontStyle |= FontStyle.Underline;
			}
			try
			{
				return new Font(text, num2, fontStyle, GraphicsUnit.Point, charFormat.bCharSet);
			}
			catch
			{
			}
			return null;
		}

		/// <summary>Retrieves the index of the character nearest to the specified location.</summary>
		/// <param name="pt">The location to search.</param>
		/// <returns>The zero-based character index at the specified location.</returns>
		// Token: 0x06003673 RID: 13939 RVA: 0x000F58E8 File Offset: 0x000F3AE8
		public override int GetCharIndexFromPosition(Point pt)
		{
			NativeMethods.POINT point = new NativeMethods.POINT(pt.X, pt.Y);
			int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 215, 0, point);
			string text = this.Text;
			if (num >= text.Length)
			{
				num = Math.Max(text.Length - 1, 0);
			}
			return num;
		}

		// Token: 0x06003674 RID: 13940 RVA: 0x000F5948 File Offset: 0x000F3B48
		private bool GetCharInCharSet(char c, char[] charSet, bool negate)
		{
			bool flag = false;
			int num = charSet.Length;
			int num2 = 0;
			while (!flag && num2 < num)
			{
				flag = c == charSet[num2];
				num2++;
			}
			if (!negate)
			{
				return flag;
			}
			return !flag;
		}

		/// <summary>Retrieves the line number from the specified character position within the text of the <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
		/// <param name="index">The character index position to search.</param>
		/// <returns>The zero-based line number in which the character index is located.</returns>
		// Token: 0x06003675 RID: 13941 RVA: 0x000F597A File Offset: 0x000F3B7A
		public override int GetLineFromCharIndex(int index)
		{
			return (int)(long)base.SendMessage(1078, 0, index);
		}

		/// <summary>Retrieves the location within the control at the specified character index.</summary>
		/// <param name="index">The index of the character for which to retrieve the location.</param>
		/// <returns>The location of the specified character.</returns>
		// Token: 0x06003676 RID: 13942 RVA: 0x000F5990 File Offset: 0x000F3B90
		public override Point GetPositionFromCharIndex(int index)
		{
			if (RichTextBox.richEditMajorVersion == 2)
			{
				return base.GetPositionFromCharIndex(index);
			}
			if (index < 0 || index > this.Text.Length)
			{
				return Point.Empty;
			}
			NativeMethods.POINT point = new NativeMethods.POINT();
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 214, point, index);
			return new Point(point.x, point.y);
		}

		// Token: 0x06003677 RID: 13943 RVA: 0x000F59F5 File Offset: 0x000F3BF5
		private bool GetProtectedError()
		{
			if (this.ProtectedError)
			{
				this.ProtectedError = false;
				return true;
			}
			return false;
		}

		/// <summary>Loads a rich text format (RTF) or standard ASCII text file into the <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
		/// <param name="path">The name and location of the file to load into the control.</param>
		/// <exception cref="T:System.IO.IOException">An error occurred while loading the file into the control.</exception>
		/// <exception cref="T:System.ArgumentException">The file being loaded is not an RTF document.</exception>
		// Token: 0x06003678 RID: 13944 RVA: 0x000F5A09 File Offset: 0x000F3C09
		public void LoadFile(string path)
		{
			this.LoadFile(path, RichTextBoxStreamType.RichText);
		}

		/// <summary>Loads a specific type of file into the <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
		/// <param name="path">The name and location of the file to load into the control.</param>
		/// <param name="fileType">One of the <see cref="T:System.Windows.Forms.RichTextBoxStreamType" /> values.</param>
		/// <exception cref="T:System.IO.IOException">An error occurred while loading the file into the control.</exception>
		/// <exception cref="T:System.ArgumentException">The file being loaded is not an RTF document.</exception>
		// Token: 0x06003679 RID: 13945 RVA: 0x000F5A14 File Offset: 0x000F3C14
		public void LoadFile(string path, RichTextBoxStreamType fileType)
		{
			if (!ClientUtils.IsEnumValid(fileType, (int)fileType, 0, 4))
			{
				throw new InvalidEnumArgumentException("fileType", (int)fileType, typeof(RichTextBoxStreamType));
			}
			Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
			try
			{
				this.LoadFile(stream, fileType);
			}
			finally
			{
				stream.Close();
			}
		}

		/// <summary>Loads the contents of an existing data stream into the <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
		/// <param name="data">A stream of data to load into the <see cref="T:System.Windows.Forms.RichTextBox" /> control.</param>
		/// <param name="fileType">One of the <see cref="T:System.Windows.Forms.RichTextBoxStreamType" /> values.</param>
		/// <exception cref="T:System.IO.IOException">An error occurred while loading the file into the control.</exception>
		/// <exception cref="T:System.ArgumentException">The file being loaded is not an RTF document.</exception>
		// Token: 0x0600367A RID: 13946 RVA: 0x000F5A74 File Offset: 0x000F3C74
		public void LoadFile(Stream data, RichTextBoxStreamType fileType)
		{
			if (!ClientUtils.IsEnumValid(fileType, (int)fileType, 0, 4))
			{
				throw new InvalidEnumArgumentException("fileType", (int)fileType, typeof(RichTextBoxStreamType));
			}
			int num;
			switch (fileType)
			{
			case RichTextBoxStreamType.RichText:
				num = 2;
				goto IL_6A;
			case RichTextBoxStreamType.PlainText:
				this.Rtf = "";
				num = 1;
				goto IL_6A;
			case RichTextBoxStreamType.UnicodePlainText:
				num = 17;
				goto IL_6A;
			}
			throw new ArgumentException(SR.GetString("InvalidFileType"));
			IL_6A:
			this.StreamIn(data, num);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600367B RID: 13947 RVA: 0x000F5AF3 File Offset: 0x000F3CF3
		protected override void OnBackColorChanged(EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				base.SendMessage(1091, 0, ColorTranslator.ToWin32(this.BackColor));
			}
			base.OnBackColorChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ContextMenuChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600367C RID: 13948 RVA: 0x000F5B1C File Offset: 0x000F3D1C
		protected override void OnContextMenuChanged(EventArgs e)
		{
			base.OnContextMenuChanged(e);
			this.UpdateOleCallback();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.RightToLeftChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600367D RID: 13949 RVA: 0x000F5B2C File Offset: 0x000F3D2C
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
			string windowText = this.WindowText;
			base.ForceWindowText(null);
			base.ForceWindowText(windowText);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.RichTextBox.ContentsResized" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ContentsResizedEventArgs" /> that contains the event data.</param>
		// Token: 0x0600367E RID: 13950 RVA: 0x000F5B58 File Offset: 0x000F3D58
		protected virtual void OnContentsResized(ContentsResizedEventArgs e)
		{
			ContentsResizedEventHandler contentsResizedEventHandler = (ContentsResizedEventHandler)base.Events[RichTextBox.EVENT_REQUESTRESIZE];
			if (contentsResizedEventHandler != null)
			{
				contentsResizedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600367F RID: 13951 RVA: 0x000F5B88 File Offset: 0x000F3D88
		protected override void OnHandleCreated(EventArgs e)
		{
			this.curSelStart = (this.curSelEnd = (int)(this.curSelType = -1));
			this.UpdateMaxLength();
			base.SendMessage(1093, 0, 79626255);
			int num = this.rightMargin;
			this.rightMargin = 0;
			this.RightMargin = num;
			base.SendMessage(1115, this.DetectUrls ? 1 : 0, 0);
			if (this.selectionBackColorToSetOnHandleCreated != Color.Empty)
			{
				this.SelectionBackColor = this.selectionBackColorToSetOnHandleCreated;
			}
			this.AutoWordSelection = this.AutoWordSelection;
			base.SendMessage(1091, 0, ColorTranslator.ToWin32(this.BackColor));
			this.InternalSetForeColor(this.ForeColor);
			base.OnHandleCreated(e);
			this.UpdateOleCallback();
			try
			{
				this.SuppressTextChangedEvent = true;
				if (this.textRtf != null)
				{
					string text = this.textRtf;
					this.textRtf = null;
					this.Rtf = text;
				}
				else if (this.textPlain != null)
				{
					string text2 = this.textPlain;
					this.textPlain = null;
					this.Text = text2;
				}
			}
			finally
			{
				this.SuppressTextChangedEvent = false;
			}
			base.SetSelectionOnHandle();
			if (this.ShowSelectionMargin)
			{
				UnsafeNativeMethods.PostMessage(new HandleRef(this, base.Handle), 1101, (IntPtr)2, (IntPtr)16777216);
			}
			if (this.languageOption != this.LanguageOption)
			{
				this.LanguageOption = this.languageOption;
			}
			base.ClearUndo();
			this.SendZoomFactor(this.zoomMultiplier);
			SystemEvents.UserPreferenceChanged += this.UserPreferenceChangedHandler;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003680 RID: 13952 RVA: 0x000F5D24 File Offset: 0x000F3F24
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
			if (!this.InConstructor)
			{
				this.textRtf = this.Rtf;
				if (this.textRtf.Length == 0)
				{
					this.textRtf = null;
				}
			}
			this.oleCallback = null;
			SystemEvents.UserPreferenceChanged -= this.UserPreferenceChangedHandler;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.RichTextBox.HScroll" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003681 RID: 13953 RVA: 0x000F5D78 File Offset: 0x000F3F78
		protected virtual void OnHScroll(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[RichTextBox.EVENT_HSCROLL];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.RichTextBox.LinkClicked" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.LinkClickedEventArgs" /> that contains the event data.</param>
		// Token: 0x06003682 RID: 13954 RVA: 0x000F5DA8 File Offset: 0x000F3FA8
		protected virtual void OnLinkClicked(LinkClickedEventArgs e)
		{
			LinkClickedEventHandler linkClickedEventHandler = (LinkClickedEventHandler)base.Events[RichTextBox.EVENT_LINKACTIVATE];
			if (linkClickedEventHandler != null)
			{
				linkClickedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.RichTextBox.ImeChange" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003683 RID: 13955 RVA: 0x000F5DD8 File Offset: 0x000F3FD8
		protected virtual void OnImeChange(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[RichTextBox.EVENT_IMECHANGE];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.RichTextBox.Protected" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003684 RID: 13956 RVA: 0x000F5E08 File Offset: 0x000F4008
		protected virtual void OnProtected(EventArgs e)
		{
			this.ProtectedError = true;
			EventHandler eventHandler = (EventHandler)base.Events[RichTextBox.EVENT_PROTECTED];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.RichTextBox.SelectionChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003685 RID: 13957 RVA: 0x000F5E40 File Offset: 0x000F4040
		protected virtual void OnSelectionChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[RichTextBox.EVENT_SELCHANGE];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.RichTextBox.VScroll" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003686 RID: 13958 RVA: 0x000F5E70 File Offset: 0x000F4070
		protected virtual void OnVScroll(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[RichTextBox.EVENT_VSCROLL];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Pastes the contents of the Clipboard in the specified Clipboard format.</summary>
		/// <param name="clipFormat">The Clipboard format in which the data should be obtained from the Clipboard.</param>
		// Token: 0x06003687 RID: 13959 RVA: 0x000F5E9E File Offset: 0x000F409E
		public void Paste(DataFormats.Format clipFormat)
		{
			IntSecurity.ClipboardRead.Demand();
			this.PasteUnsafe(clipFormat, 0);
		}

		// Token: 0x06003688 RID: 13960 RVA: 0x000F5EB4 File Offset: 0x000F40B4
		private void PasteUnsafe(DataFormats.Format clipFormat, int hIcon)
		{
			NativeMethods.REPASTESPECIAL repastespecial = null;
			if (hIcon != 0)
			{
				repastespecial = new NativeMethods.REPASTESPECIAL();
				repastespecial.dwAspect = 4;
				repastespecial.dwParam = hIcon;
			}
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1088, clipFormat.Id, repastespecial);
		}

		/// <summary>Processes a command key.</summary>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process.</param>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the  key to process.</param>
		/// <returns>
		///   <see langword="true" /> if the character was processed by the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003689 RID: 13961 RVA: 0x000F5EF8 File Offset: 0x000F40F8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override bool ProcessCmdKey(ref Message m, Keys keyData)
		{
			if (!this.RichTextShortcutsEnabled)
			{
				foreach (int num in RichTextBox.shortcutsToDisable)
				{
					if (keyData == (Keys)num)
					{
						return true;
					}
				}
			}
			return base.ProcessCmdKey(ref m, keyData);
		}

		// Token: 0x0600368A RID: 13962 RVA: 0x000070A6 File Offset: 0x000052A6
		internal override void RaiseAccessibilityTextChangedEvent()
		{
		}

		/// <summary>Reapplies the last operation that was undone in the control.</summary>
		// Token: 0x0600368B RID: 13963 RVA: 0x000F5F33 File Offset: 0x000F4133
		public void Redo()
		{
			base.SendMessage(1108, 0, 0);
		}

		/// <summary>Saves the contents of the <see cref="T:System.Windows.Forms.RichTextBox" /> to a rich text format (RTF) file.</summary>
		/// <param name="path">The name and location of the file to save.</param>
		/// <exception cref="T:System.IO.IOException">An error occurs in saving the contents of the control to a file.</exception>
		// Token: 0x0600368C RID: 13964 RVA: 0x000F5F43 File Offset: 0x000F4143
		public void SaveFile(string path)
		{
			this.SaveFile(path, RichTextBoxStreamType.RichText);
		}

		/// <summary>Saves the contents of the <see cref="T:System.Windows.Forms.RichTextBox" /> to a specific type of file.</summary>
		/// <param name="path">The name and location of the file to save.</param>
		/// <param name="fileType">One of the <see cref="T:System.Windows.Forms.RichTextBoxStreamType" /> values.</param>
		/// <exception cref="T:System.ArgumentException">An invalid file type is specified in the <paramref name="fileType" /> parameter.</exception>
		/// <exception cref="T:System.IO.IOException">An error occurs in saving the contents of the control to a file.</exception>
		// Token: 0x0600368D RID: 13965 RVA: 0x000F5F50 File Offset: 0x000F4150
		public void SaveFile(string path, RichTextBoxStreamType fileType)
		{
			if (!ClientUtils.IsEnumValid(fileType, (int)fileType, 0, 4))
			{
				throw new InvalidEnumArgumentException("fileType", (int)fileType, typeof(RichTextBoxStreamType));
			}
			Stream stream = File.Create(path);
			try
			{
				this.SaveFile(stream, fileType);
			}
			finally
			{
				stream.Close();
			}
		}

		/// <summary>Saves the contents of a <see cref="T:System.Windows.Forms.RichTextBox" /> control to an open data stream.</summary>
		/// <param name="data">The data stream that contains the file to save to.</param>
		/// <param name="fileType">One of the <see cref="T:System.Windows.Forms.RichTextBoxStreamType" /> values.</param>
		/// <exception cref="T:System.ArgumentException">An invalid file type is specified in the <paramref name="fileType" /> parameter.</exception>
		/// <exception cref="T:System.IO.IOException">An error occurs in saving the contents of the control to a file.</exception>
		// Token: 0x0600368E RID: 13966 RVA: 0x000F5FAC File Offset: 0x000F41AC
		public void SaveFile(Stream data, RichTextBoxStreamType fileType)
		{
			int num;
			switch (fileType)
			{
			case RichTextBoxStreamType.RichText:
				num = 2;
				break;
			case RichTextBoxStreamType.PlainText:
				num = 1;
				break;
			case RichTextBoxStreamType.RichNoOleObjs:
				num = 3;
				break;
			case RichTextBoxStreamType.TextTextOleObjs:
				num = 4;
				break;
			case RichTextBoxStreamType.UnicodePlainText:
				num = 17;
				break;
			default:
				throw new InvalidEnumArgumentException("fileType", (int)fileType, typeof(RichTextBoxStreamType));
			}
			this.StreamOut(data, num, true);
		}

		// Token: 0x0600368F RID: 13967 RVA: 0x000F600C File Offset: 0x000F420C
		private void SendZoomFactor(float zoom)
		{
			int num;
			int num2;
			if (zoom == 1f)
			{
				num = 0;
				num2 = 0;
			}
			else
			{
				num = 1000;
				float num3 = 1000f * zoom;
				num2 = (int)Math.Ceiling((double)num3);
				if (num2 >= 64000)
				{
					num2 = (int)Math.Floor((double)num3);
				}
			}
			if (base.IsHandleCreated)
			{
				base.SendMessage(1249, num2, num);
			}
			if (num2 != 0)
			{
				this.zoomMultiplier = (float)num2 / (float)num;
				return;
			}
			this.zoomMultiplier = 1f;
		}

		// Token: 0x06003690 RID: 13968 RVA: 0x000F6080 File Offset: 0x000F4280
		private bool SetCharFormat(int mask, int effect, RichTextBoxSelectionAttribute charFormat)
		{
			if (base.IsHandleCreated)
			{
				NativeMethods.CHARFORMATA charformata = new NativeMethods.CHARFORMATA();
				charformata.dwMask = mask;
				if (charFormat != RichTextBoxSelectionAttribute.None)
				{
					if (charFormat != RichTextBoxSelectionAttribute.All)
					{
						throw new ArgumentException(SR.GetString("UnknownAttr"));
					}
					charformata.dwEffects = effect;
				}
				else
				{
					charformata.dwEffects = 0;
				}
				return IntPtr.Zero != UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1092, 1, charformata);
			}
			return false;
		}

		// Token: 0x06003691 RID: 13969 RVA: 0x000F60EF File Offset: 0x000F42EF
		private bool SetCharFormat(int charRange, NativeMethods.CHARFORMATA cf)
		{
			return IntPtr.Zero != UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1092, charRange, cf);
		}

		// Token: 0x06003692 RID: 13970 RVA: 0x000F6114 File Offset: 0x000F4314
		private void SetCharFormatFont(bool selectionOnly, Font value)
		{
			this.ForceHandleCreate();
			NativeMethods.LOGFONT logfont = new NativeMethods.LOGFONT();
			RichTextBox.FontToLogFont(value, logfont);
			int num = -1476394993;
			int num2 = 0;
			if (value.Bold)
			{
				num2 |= 1;
			}
			if (value.Italic)
			{
				num2 |= 2;
			}
			if (value.Strikeout)
			{
				num2 |= 8;
			}
			if (value.Underline)
			{
				num2 |= 4;
			}
			byte[] array;
			if (Marshal.SystemDefaultCharSize == 1)
			{
				array = Encoding.Default.GetBytes(logfont.lfFaceName);
				NativeMethods.CHARFORMATA charformata = new NativeMethods.CHARFORMATA();
				for (int i = 0; i < array.Length; i++)
				{
					charformata.szFaceName[i] = array[i];
				}
				charformata.dwMask = num;
				charformata.dwEffects = num2;
				charformata.yHeight = (int)(value.SizeInPoints * 20f);
				charformata.bCharSet = logfont.lfCharSet;
				charformata.bPitchAndFamily = logfont.lfPitchAndFamily;
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1092, selectionOnly ? 1 : 4, charformata);
				return;
			}
			array = Encoding.Unicode.GetBytes(logfont.lfFaceName);
			NativeMethods.CHARFORMATW charformatw = new NativeMethods.CHARFORMATW();
			for (int j = 0; j < array.Length; j++)
			{
				charformatw.szFaceName[j] = array[j];
			}
			charformatw.dwMask = num;
			charformatw.dwEffects = num2;
			charformatw.yHeight = (int)(value.SizeInPoints * 20f);
			charformatw.bCharSet = logfont.lfCharSet;
			charformatw.bPitchAndFamily = logfont.lfPitchAndFamily;
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1092, selectionOnly ? 1 : 4, charformatw);
		}

		// Token: 0x06003693 RID: 13971 RVA: 0x000F62A4 File Offset: 0x000F44A4
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
		private static void FontToLogFont(Font value, NativeMethods.LOGFONT logfont)
		{
			value.ToLogFont(logfont);
		}

		// Token: 0x06003694 RID: 13972 RVA: 0x000F62B0 File Offset: 0x000F44B0
		private static void SetupLogPixels(IntPtr hDC)
		{
			bool flag = false;
			if (hDC == IntPtr.Zero)
			{
				hDC = UnsafeNativeMethods.GetDC(NativeMethods.NullHandleRef);
				flag = true;
			}
			if (hDC == IntPtr.Zero)
			{
				return;
			}
			RichTextBox.logPixelsX = UnsafeNativeMethods.GetDeviceCaps(new HandleRef(null, hDC), 88);
			RichTextBox.logPixelsY = UnsafeNativeMethods.GetDeviceCaps(new HandleRef(null, hDC), 90);
			if (flag)
			{
				UnsafeNativeMethods.ReleaseDC(NativeMethods.NullHandleRef, new HandleRef(null, hDC));
			}
		}

		// Token: 0x06003695 RID: 13973 RVA: 0x000F6324 File Offset: 0x000F4524
		private static int Pixel2Twip(IntPtr hDC, int v, bool xDirection)
		{
			RichTextBox.SetupLogPixels(hDC);
			int num = (xDirection ? RichTextBox.logPixelsX : RichTextBox.logPixelsY);
			return (int)((double)v / (double)num * 72.0 * 20.0);
		}

		// Token: 0x06003696 RID: 13974 RVA: 0x000F6364 File Offset: 0x000F4564
		private static int Twip2Pixel(IntPtr hDC, int v, bool xDirection)
		{
			RichTextBox.SetupLogPixels(hDC);
			int num = (xDirection ? RichTextBox.logPixelsX : RichTextBox.logPixelsY);
			return (int)((double)v / 20.0 / 72.0 * (double)num);
		}

		// Token: 0x06003697 RID: 13975 RVA: 0x000F63A4 File Offset: 0x000F45A4
		private void StreamIn(string str, int flags)
		{
			if (str.Length != 0)
			{
				int num = str.IndexOf('\0');
				if (num != -1)
				{
					str = str.Substring(0, num);
				}
				byte[] array;
				if ((flags & 16) != 0)
				{
					array = Encoding.Unicode.GetBytes(str);
				}
				else
				{
					array = Encoding.Default.GetBytes(str);
				}
				this.editStream = new MemoryStream(array.Length);
				this.editStream.Write(array, 0, array.Length);
				this.editStream.Position = 0L;
				this.StreamIn(this.editStream, flags);
				return;
			}
			if ((32768 & flags) != 0)
			{
				base.SendMessage(771, 0, 0);
				this.ProtectedError = false;
				return;
			}
			base.SendMessage(12, 0, "");
		}

		// Token: 0x06003698 RID: 13976 RVA: 0x000F6458 File Offset: 0x000F4658
		private void StreamIn(Stream data, int flags)
		{
			if ((flags & 32768) == 0)
			{
				NativeMethods.CHARRANGE charrange = new NativeMethods.CHARRANGE();
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1079, 0, charrange);
			}
			try
			{
				this.editStream = data;
				if ((flags & 2) != 0)
				{
					long position = this.editStream.Position;
					byte[] array = new byte[RichTextBox.SZ_RTF_TAG.Length];
					this.editStream.Read(array, (int)position, RichTextBox.SZ_RTF_TAG.Length);
					string @string = Encoding.Default.GetString(array);
					if (!RichTextBox.SZ_RTF_TAG.Equals(@string))
					{
						throw new ArgumentException(SR.GetString("InvalidFileFormat"));
					}
					this.editStream.Position = position;
				}
				NativeMethods.EDITSTREAM editstream = new NativeMethods.EDITSTREAM();
				int num;
				if ((flags & 16) != 0)
				{
					num = 9;
				}
				else
				{
					num = 5;
				}
				if ((flags & 2) != 0)
				{
					num |= 64;
				}
				else
				{
					num |= 16;
				}
				editstream.dwCookie = (IntPtr)num;
				editstream.pfnCallback = new NativeMethods.EditStreamCallback(this.EditStreamProc);
				base.SendMessage(1077, 0, int.MaxValue);
				if (IntPtr.Size == 8)
				{
					NativeMethods.EDITSTREAM64 editstream2 = this.ConvertToEDITSTREAM64(editstream);
					UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1097, flags, editstream2);
					editstream.dwError = this.GetErrorValue64(editstream2);
				}
				else
				{
					UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1097, flags, editstream);
				}
				this.UpdateMaxLength();
				if (!this.GetProtectedError())
				{
					if (editstream.dwError != 0)
					{
						throw new InvalidOperationException(SR.GetString("LoadTextError"));
					}
					base.SendMessage(185, -1, 0);
					base.SendMessage(186, 0, 0);
				}
			}
			finally
			{
				this.editStream = null;
			}
		}

		// Token: 0x06003699 RID: 13977 RVA: 0x000F6620 File Offset: 0x000F4820
		private string StreamOut(int flags)
		{
			Stream stream = new MemoryStream();
			this.StreamOut(stream, flags, false);
			stream.Position = 0L;
			int num = (int)stream.Length;
			string text = string.Empty;
			if (num > 0)
			{
				byte[] array = new byte[num];
				stream.Read(array, 0, num);
				if ((flags & 16) != 0)
				{
					text = Encoding.Unicode.GetString(array, 0, array.Length);
				}
				else
				{
					text = Encoding.Default.GetString(array, 0, array.Length);
				}
				if (!string.IsNullOrEmpty(text) && text[text.Length - 1] == '\0')
				{
					text = text.Substring(0, text.Length - 1);
				}
			}
			return text;
		}

		// Token: 0x0600369A RID: 13978 RVA: 0x000F66B8 File Offset: 0x000F48B8
		private void StreamOut(Stream data, int flags, bool includeCrLfs)
		{
			this.editStream = data;
			try
			{
				NativeMethods.EDITSTREAM editstream = new NativeMethods.EDITSTREAM();
				int num;
				if ((flags & 16) != 0)
				{
					num = 10;
				}
				else
				{
					num = 6;
				}
				if ((flags & 2) != 0)
				{
					num |= 64;
				}
				else if (includeCrLfs)
				{
					num |= 32;
				}
				else
				{
					num |= 16;
				}
				editstream.dwCookie = (IntPtr)num;
				editstream.pfnCallback = new NativeMethods.EditStreamCallback(this.EditStreamProc);
				if (IntPtr.Size == 8)
				{
					NativeMethods.EDITSTREAM64 editstream2 = this.ConvertToEDITSTREAM64(editstream);
					UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1098, flags, editstream2);
					editstream.dwError = this.GetErrorValue64(editstream2);
				}
				else
				{
					UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1098, flags, editstream);
				}
				if (editstream.dwError != 0)
				{
					throw new InvalidOperationException(SR.GetString("SaveTextError"));
				}
			}
			finally
			{
				this.editStream = null;
			}
		}

		// Token: 0x0600369B RID: 13979 RVA: 0x000F67A0 File Offset: 0x000F49A0
		private unsafe NativeMethods.EDITSTREAM64 ConvertToEDITSTREAM64(NativeMethods.EDITSTREAM es)
		{
			NativeMethods.EDITSTREAM64 editstream = new NativeMethods.EDITSTREAM64();
			fixed (byte* ptr = &editstream.contents[0])
			{
				byte* ptr2 = ptr;
				*(long*)ptr2 = (long)es.dwCookie;
				*(int*)(ptr2 + 8) = es.dwError;
				long num = (long)Marshal.GetFunctionPointerForDelegate(es.pfnCallback);
				byte* ptr3 = (byte*)(&num);
				for (int i = 0; i < 8; i++)
				{
					editstream.contents[i + 12] = ptr3[i];
				}
			}
			return editstream;
		}

		// Token: 0x0600369C RID: 13980 RVA: 0x000F6818 File Offset: 0x000F4A18
		private unsafe int GetErrorValue64(NativeMethods.EDITSTREAM64 es64)
		{
			int num;
			fixed (byte* ptr = &es64.contents[0])
			{
				byte* ptr2 = ptr;
				num = *(int*)(ptr2 + 8);
			}
			return num;
		}

		// Token: 0x0600369D RID: 13981 RVA: 0x000F6840 File Offset: 0x000F4A40
		private void UpdateOleCallback()
		{
			if (base.IsHandleCreated)
			{
				if (this.oleCallback == null)
				{
					bool flag = false;
					try
					{
						IntSecurity.UnmanagedCode.Demand();
						flag = true;
					}
					catch (SecurityException)
					{
						flag = false;
					}
					if (flag)
					{
						this.AllowOleObjects = true;
					}
					else
					{
						this.AllowOleObjects = (int)(long)base.SendMessage(1294, 0, 1) != 0;
					}
					this.oleCallback = this.CreateRichEditOleCallback();
					IntPtr iunknownForObject = Marshal.GetIUnknownForObject(this.oleCallback);
					try
					{
						Guid guid = typeof(UnsafeNativeMethods.IRichEditOleCallback).GUID;
						IntPtr intPtr;
						Marshal.QueryInterface(iunknownForObject, ref guid, out intPtr);
						try
						{
							UnsafeNativeMethods.SendCallbackMessage(new HandleRef(this, base.Handle), 1094, IntPtr.Zero, intPtr);
						}
						finally
						{
							Marshal.Release(intPtr);
						}
					}
					finally
					{
						Marshal.Release(iunknownForObject);
					}
				}
				UnsafeNativeMethods.DragAcceptFiles(new HandleRef(this, base.Handle), false);
			}
		}

		// Token: 0x0600369E RID: 13982 RVA: 0x000F693C File Offset: 0x000F4B3C
		private void UserPreferenceChangedHandler(object o, UserPreferenceChangedEventArgs e)
		{
			if (base.IsHandleCreated)
			{
				if (this.BackColor.IsSystemColor)
				{
					base.SendMessage(1091, 0, ColorTranslator.ToWin32(this.BackColor));
				}
				if (this.ForeColor.IsSystemColor)
				{
					this.InternalSetForeColor(this.ForeColor);
				}
			}
		}

		/// <summary>Creates an <see langword="IRichEditOleCallback" />-compatible object for handling rich-edit callback operations.</summary>
		/// <returns>An object that implements the <see langword="IRichEditOleCallback" /> interface.</returns>
		// Token: 0x0600369F RID: 13983 RVA: 0x000F6996 File Offset: 0x000F4B96
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected virtual object CreateRichEditOleCallback()
		{
			return new RichTextBox.OleCallback(this);
		}

		// Token: 0x060036A0 RID: 13984 RVA: 0x000F69A0 File Offset: 0x000F4BA0
		private void EnLinkMsgHandler(ref Message m)
		{
			NativeMethods.ENLINK enlink;
			if (IntPtr.Size == 8)
			{
				enlink = RichTextBox.ConvertFromENLINK64((NativeMethods.ENLINK64)m.GetLParam(typeof(NativeMethods.ENLINK64)));
			}
			else
			{
				enlink = (NativeMethods.ENLINK)m.GetLParam(typeof(NativeMethods.ENLINK));
			}
			int msg = enlink.msg;
			if (msg == 32)
			{
				this.LinkCursor = true;
				m.Result = (IntPtr)1;
				return;
			}
			if (msg != 513)
			{
				m.Result = IntPtr.Zero;
				return;
			}
			string text = this.CharRangeToString(enlink.charrange);
			if (!string.IsNullOrEmpty(text))
			{
				this.OnLinkClicked(new LinkClickedEventArgs(text));
			}
			m.Result = (IntPtr)1;
		}

		// Token: 0x060036A1 RID: 13985 RVA: 0x000F6A4C File Offset: 0x000F4C4C
		private string CharRangeToString(NativeMethods.CHARRANGE c)
		{
			NativeMethods.TEXTRANGE textrange = new NativeMethods.TEXTRANGE();
			textrange.chrg = c;
			if (c.cpMax > this.Text.Length || c.cpMax - c.cpMin <= 0)
			{
				return string.Empty;
			}
			int num = c.cpMax - c.cpMin + 1;
			UnsafeNativeMethods.CharBuffer charBuffer = UnsafeNativeMethods.CharBuffer.CreateBuffer(num);
			IntPtr intPtr = charBuffer.AllocCoTaskMem();
			if (intPtr == IntPtr.Zero)
			{
				throw new OutOfMemoryException(SR.GetString("OutOfMemory"));
			}
			textrange.lpstrText = intPtr;
			int num2 = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1099, 0, textrange);
			charBuffer.PutCoTaskMem(intPtr);
			if (textrange.lpstrText != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(intPtr);
			}
			return charBuffer.GetString();
		}

		// Token: 0x060036A2 RID: 13986 RVA: 0x000F6B19 File Offset: 0x000F4D19
		internal override void UpdateMaxLength()
		{
			if (base.IsHandleCreated)
			{
				base.SendMessage(1077, 0, this.MaxLength);
			}
		}

		// Token: 0x060036A3 RID: 13987 RVA: 0x000F6B38 File Offset: 0x000F4D38
		private void WmReflectCommand(ref Message m)
		{
			if (!(m.LParam == base.Handle) || base.GetState(262144))
			{
				base.WndProc(ref m);
				return;
			}
			int num = NativeMethods.Util.HIWORD(m.WParam);
			if (num == 1537)
			{
				this.OnHScroll(EventArgs.Empty);
				return;
			}
			if (num != 1538)
			{
				base.WndProc(ref m);
				return;
			}
			this.OnVScroll(EventArgs.Empty);
		}

		// Token: 0x060036A4 RID: 13988 RVA: 0x000F6BAC File Offset: 0x000F4DAC
		internal void WmReflectNotify(ref Message m)
		{
			if (m.HWnd == base.Handle)
			{
				NativeMethods.NMHDR nmhdr = (NativeMethods.NMHDR)m.GetLParam(typeof(NativeMethods.NMHDR));
				int code = nmhdr.code;
				switch (code)
				{
				case 1793:
					if (!this.CallOnContentsResized)
					{
						NativeMethods.REQRESIZE reqresize = (NativeMethods.REQRESIZE)m.GetLParam(typeof(NativeMethods.REQRESIZE));
						if (base.BorderStyle == BorderStyle.Fixed3D)
						{
							NativeMethods.REQRESIZE reqresize2 = reqresize;
							reqresize2.rc.bottom = reqresize2.rc.bottom + 1;
						}
						this.OnContentsResized(new ContentsResizedEventArgs(Rectangle.FromLTRB(reqresize.rc.left, reqresize.rc.top, reqresize.rc.right, reqresize.rc.bottom)));
						return;
					}
					break;
				case 1794:
				{
					NativeMethods.SELCHANGE selchange = (NativeMethods.SELCHANGE)m.GetLParam(typeof(NativeMethods.SELCHANGE));
					this.WmSelectionChange(selchange);
					return;
				}
				case 1795:
				{
					NativeMethods.ENDROPFILES endropfiles = (NativeMethods.ENDROPFILES)m.GetLParam(typeof(NativeMethods.ENDROPFILES));
					StringBuilder stringBuilder = new StringBuilder(260);
					if (UnsafeNativeMethods.DragQueryFileLongPath(new HandleRef(endropfiles, endropfiles.hDrop), 0, stringBuilder) != 0)
					{
						try
						{
							this.LoadFile(stringBuilder.ToString(), RichTextBoxStreamType.RichText);
						}
						catch
						{
							try
							{
								this.LoadFile(stringBuilder.ToString(), RichTextBoxStreamType.PlainText);
							}
							catch
							{
							}
						}
					}
					m.Result = (IntPtr)1;
					return;
				}
				case 1796:
				{
					NativeMethods.ENPROTECTED enprotected;
					if (IntPtr.Size == 8)
					{
						enprotected = this.ConvertFromENPROTECTED64((NativeMethods.ENPROTECTED64)m.GetLParam(typeof(NativeMethods.ENPROTECTED64)));
					}
					else
					{
						enprotected = (NativeMethods.ENPROTECTED)m.GetLParam(typeof(NativeMethods.ENPROTECTED));
					}
					int msg = enprotected.msg;
					if (msg <= 769)
					{
						if (msg != 12)
						{
							if (msg == 194)
							{
								goto IL_277;
							}
							if (msg != 769)
							{
								goto IL_270;
							}
						}
					}
					else if (msg <= 1092)
					{
						if (msg != 1077)
						{
							if (msg != 1092)
							{
								goto IL_270;
							}
							NativeMethods.CHARFORMATA charformata = (NativeMethods.CHARFORMATA)UnsafeNativeMethods.PtrToStructure(enprotected.lParam, typeof(NativeMethods.CHARFORMATA));
							if ((charformata.dwMask & 16) != 0)
							{
								m.Result = IntPtr.Zero;
								return;
							}
							goto IL_277;
						}
					}
					else
					{
						if (msg == 1095)
						{
							goto IL_277;
						}
						if (msg != 1097)
						{
							goto IL_270;
						}
						if (((int)(long)enprotected.wParam & 32768) == 0)
						{
							m.Result = IntPtr.Zero;
							return;
						}
						goto IL_277;
					}
					m.Result = IntPtr.Zero;
					return;
					IL_270:
					SafeNativeMethods.MessageBeep(0);
					IL_277:
					this.OnProtected(EventArgs.Empty);
					m.Result = (IntPtr)1;
					return;
				}
				default:
					if (code == 1803)
					{
						this.EnLinkMsgHandler(ref m);
						return;
					}
					base.WndProc(ref m);
					return;
				}
			}
			else
			{
				base.WndProc(ref m);
			}
		}

		// Token: 0x060036A5 RID: 13989 RVA: 0x000F6E74 File Offset: 0x000F5074
		private unsafe NativeMethods.ENPROTECTED ConvertFromENPROTECTED64(NativeMethods.ENPROTECTED64 es64)
		{
			NativeMethods.ENPROTECTED enprotected = new NativeMethods.ENPROTECTED();
			fixed (byte* ptr = &es64.contents[0])
			{
				byte* ptr2 = ptr;
				enprotected.nmhdr = default(NativeMethods.NMHDR);
				enprotected.chrg = new NativeMethods.CHARRANGE();
				enprotected.nmhdr.hwndFrom = Marshal.ReadIntPtr((IntPtr)((void*)ptr2));
				enprotected.nmhdr.idFrom = Marshal.ReadIntPtr((IntPtr)((void*)(ptr2 + 8)));
				enprotected.nmhdr.code = Marshal.ReadInt32((IntPtr)((void*)(ptr2 + 16)));
				enprotected.msg = Marshal.ReadInt32((IntPtr)((void*)(ptr2 + 24)));
				enprotected.wParam = Marshal.ReadIntPtr((IntPtr)((void*)(ptr2 + 28)));
				enprotected.lParam = Marshal.ReadIntPtr((IntPtr)((void*)(ptr2 + 36)));
				enprotected.chrg.cpMin = Marshal.ReadInt32((IntPtr)((void*)(ptr2 + 44)));
				enprotected.chrg.cpMax = Marshal.ReadInt32((IntPtr)((void*)(ptr2 + 48)));
			}
			return enprotected;
		}

		// Token: 0x060036A6 RID: 13990 RVA: 0x000F6F68 File Offset: 0x000F5168
		private unsafe static NativeMethods.ENLINK ConvertFromENLINK64(NativeMethods.ENLINK64 es64)
		{
			NativeMethods.ENLINK enlink = new NativeMethods.ENLINK();
			fixed (byte* ptr = &es64.contents[0])
			{
				byte* ptr2 = ptr;
				enlink.nmhdr = default(NativeMethods.NMHDR);
				enlink.charrange = new NativeMethods.CHARRANGE();
				enlink.nmhdr.hwndFrom = Marshal.ReadIntPtr((IntPtr)((void*)ptr2));
				enlink.nmhdr.idFrom = Marshal.ReadIntPtr((IntPtr)((void*)(ptr2 + 8)));
				enlink.nmhdr.code = Marshal.ReadInt32((IntPtr)((void*)(ptr2 + 16)));
				enlink.msg = Marshal.ReadInt32((IntPtr)((void*)(ptr2 + 24)));
				enlink.wParam = Marshal.ReadIntPtr((IntPtr)((void*)(ptr2 + 28)));
				enlink.lParam = Marshal.ReadIntPtr((IntPtr)((void*)(ptr2 + 36)));
				enlink.charrange.cpMin = Marshal.ReadInt32((IntPtr)((void*)(ptr2 + 44)));
				enlink.charrange.cpMax = Marshal.ReadInt32((IntPtr)((void*)(ptr2 + 48)));
			}
			return enlink;
		}

		// Token: 0x060036A7 RID: 13991 RVA: 0x000F705C File Offset: 0x000F525C
		private void WmSelectionChange(NativeMethods.SELCHANGE selChange)
		{
			int cpMin = selChange.chrg.cpMin;
			int cpMax = selChange.chrg.cpMax;
			short num = (short)selChange.seltyp;
			if (base.ImeMode == ImeMode.Hangul || base.ImeMode == ImeMode.HangulFull)
			{
				int num2 = (int)(long)base.SendMessage(1146, 0, 0);
				if (num2 != 0)
				{
					int windowTextLength = SafeNativeMethods.GetWindowTextLength(new HandleRef(this, base.Handle));
					if (cpMin == cpMax && windowTextLength == this.MaxLength)
					{
						base.SendMessage(8, 0, 0);
						base.SendMessage(7, 0, 0);
						UnsafeNativeMethods.PostMessage(new HandleRef(this, base.Handle), 177, cpMax - 1, cpMax);
					}
				}
			}
			if (cpMin != this.curSelStart || cpMax != this.curSelEnd || num != this.curSelType)
			{
				this.curSelStart = cpMin;
				this.curSelEnd = cpMax;
				this.curSelType = num;
				this.OnSelectionChanged(EventArgs.Empty);
			}
		}

		// Token: 0x060036A8 RID: 13992 RVA: 0x000F7140 File Offset: 0x000F5340
		private void WmSetFont(ref Message m)
		{
			try
			{
				this.SuppressTextChangedEvent = true;
				base.WndProc(ref m);
			}
			finally
			{
				this.SuppressTextChangedEvent = false;
			}
			this.InternalSetForeColor(this.ForeColor);
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">A Windows Message object.</param>
		// Token: 0x060036A9 RID: 13993 RVA: 0x000F7184 File Offset: 0x000F5384
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg <= 276)
			{
				if (msg <= 48)
				{
					if (msg != 32)
					{
						if (msg == 48)
						{
							this.WmSetFont(ref m);
							return;
						}
					}
					else
					{
						this.LinkCursor = false;
						this.DefWndProc(ref m);
						if (this.LinkCursor && !this.Cursor.Equals(Cursors.WaitCursor))
						{
							UnsafeNativeMethods.SetCursor(new HandleRef(Cursors.Hand, Cursors.Hand.Handle));
							m.Result = (IntPtr)1;
							return;
						}
						base.WndProc(ref m);
						return;
					}
				}
				else if (msg != 61)
				{
					if (msg == 135)
					{
						base.WndProc(ref m);
						m.Result = (IntPtr)(base.AcceptsTab ? ((int)(long)m.Result | 2) : ((int)(long)m.Result & -3));
						return;
					}
					if (msg == 276)
					{
						base.WndProc(ref m);
						int num = NativeMethods.Util.LOWORD(m.WParam);
						if (num == 5)
						{
							this.OnHScroll(EventArgs.Empty);
						}
						if (num == 4)
						{
							this.OnHScroll(EventArgs.Empty);
							return;
						}
						return;
					}
				}
				else
				{
					base.WndProc(ref m);
					if ((int)(long)m.LParam == -12)
					{
						m.Result = (IntPtr)((Marshal.SystemDefaultCharSize == 1) ? 65565 : 65566);
						return;
					}
					return;
				}
			}
			else if (msg <= 517)
			{
				if (msg != 277)
				{
					if (msg == 517)
					{
						bool style = base.GetStyle(ControlStyles.UserMouse);
						base.SetStyle(ControlStyles.UserMouse, true);
						base.WndProc(ref m);
						base.SetStyle(ControlStyles.UserMouse, style);
						return;
					}
				}
				else
				{
					base.WndProc(ref m);
					int num = NativeMethods.Util.LOWORD(m.WParam);
					if (num == 5)
					{
						this.OnVScroll(EventArgs.Empty);
						return;
					}
					if (num == 4)
					{
						this.OnVScroll(EventArgs.Empty);
						return;
					}
					return;
				}
			}
			else
			{
				if (msg == 642)
				{
					this.OnImeChange(EventArgs.Empty);
					base.WndProc(ref m);
					return;
				}
				if (msg == 8270)
				{
					this.WmReflectNotify(ref m);
					return;
				}
				if (msg == 8465)
				{
					this.WmReflectCommand(ref m);
					return;
				}
			}
			base.WndProc(ref m);
		}

		// Token: 0x04001F69 RID: 8041
		private static TraceSwitch richTextDbg;

		// Token: 0x04001F6A RID: 8042
		private const int DV_E_DVASPECT = -2147221397;

		// Token: 0x04001F6B RID: 8043
		private const int DVASPECT_CONTENT = 1;

		// Token: 0x04001F6C RID: 8044
		private const int DVASPECT_THUMBNAIL = 2;

		// Token: 0x04001F6D RID: 8045
		private const int DVASPECT_ICON = 4;

		// Token: 0x04001F6E RID: 8046
		private const int DVASPECT_DOCPRINT = 8;

		// Token: 0x04001F6F RID: 8047
		internal const int INPUT = 1;

		// Token: 0x04001F70 RID: 8048
		internal const int OUTPUT = 2;

		// Token: 0x04001F71 RID: 8049
		internal const int DIRECTIONMASK = 3;

		// Token: 0x04001F72 RID: 8050
		internal const int ANSI = 4;

		// Token: 0x04001F73 RID: 8051
		internal const int UNICODE = 8;

		// Token: 0x04001F74 RID: 8052
		internal const int FORMATMASK = 12;

		// Token: 0x04001F75 RID: 8053
		internal const int TEXTLF = 16;

		// Token: 0x04001F76 RID: 8054
		internal const int TEXTCRLF = 32;

		// Token: 0x04001F77 RID: 8055
		internal const int RTF = 64;

		// Token: 0x04001F78 RID: 8056
		internal const int KINDMASK = 112;

		// Token: 0x04001F79 RID: 8057
		private static IntPtr moduleHandle;

		// Token: 0x04001F7A RID: 8058
		private static readonly string SZ_RTF_TAG = "{\\rtf";

		// Token: 0x04001F7B RID: 8059
		private const int CHAR_BUFFER_LEN = 512;

		// Token: 0x04001F7C RID: 8060
		private static readonly object EVENT_HSCROLL = new object();

		// Token: 0x04001F7D RID: 8061
		private static readonly object EVENT_LINKACTIVATE = new object();

		// Token: 0x04001F7E RID: 8062
		private static readonly object EVENT_IMECHANGE = new object();

		// Token: 0x04001F7F RID: 8063
		private static readonly object EVENT_PROTECTED = new object();

		// Token: 0x04001F80 RID: 8064
		private static readonly object EVENT_REQUESTRESIZE = new object();

		// Token: 0x04001F81 RID: 8065
		private static readonly object EVENT_SELCHANGE = new object();

		// Token: 0x04001F82 RID: 8066
		private static readonly object EVENT_VSCROLL = new object();

		// Token: 0x04001F83 RID: 8067
		private int bulletIndent;

		// Token: 0x04001F84 RID: 8068
		private int rightMargin;

		// Token: 0x04001F85 RID: 8069
		private string textRtf;

		// Token: 0x04001F86 RID: 8070
		private string textPlain;

		// Token: 0x04001F87 RID: 8071
		private Color selectionBackColorToSetOnHandleCreated;

		// Token: 0x04001F88 RID: 8072
		private RichTextBoxLanguageOptions languageOption = RichTextBoxLanguageOptions.AutoFont | RichTextBoxLanguageOptions.DualFont;

		// Token: 0x04001F89 RID: 8073
		private static int logPixelsX;

		// Token: 0x04001F8A RID: 8074
		private static int logPixelsY;

		// Token: 0x04001F8B RID: 8075
		private Stream editStream;

		// Token: 0x04001F8C RID: 8076
		private float zoomMultiplier = 1f;

		// Token: 0x04001F8D RID: 8077
		private int curSelStart;

		// Token: 0x04001F8E RID: 8078
		private int curSelEnd;

		// Token: 0x04001F8F RID: 8079
		private short curSelType;

		// Token: 0x04001F90 RID: 8080
		private object oleCallback;

		// Token: 0x04001F91 RID: 8081
		private static int[] shortcutsToDisable;

		// Token: 0x04001F92 RID: 8082
		private static int richEditMajorVersion = 3;

		// Token: 0x04001F93 RID: 8083
		private BitVector32 richTextBoxFlags;

		// Token: 0x04001F94 RID: 8084
		private static readonly BitVector32.Section autoWordSelectionSection = BitVector32.CreateSection(1);

		// Token: 0x04001F95 RID: 8085
		private static readonly BitVector32.Section showSelBarSection = BitVector32.CreateSection(1, RichTextBox.autoWordSelectionSection);

		// Token: 0x04001F96 RID: 8086
		private static readonly BitVector32.Section autoUrlDetectSection = BitVector32.CreateSection(1, RichTextBox.showSelBarSection);

		// Token: 0x04001F97 RID: 8087
		private static readonly BitVector32.Section fInCtorSection = BitVector32.CreateSection(1, RichTextBox.autoUrlDetectSection);

		// Token: 0x04001F98 RID: 8088
		private static readonly BitVector32.Section protectedErrorSection = BitVector32.CreateSection(1, RichTextBox.fInCtorSection);

		// Token: 0x04001F99 RID: 8089
		private static readonly BitVector32.Section linkcursorSection = BitVector32.CreateSection(1, RichTextBox.protectedErrorSection);

		// Token: 0x04001F9A RID: 8090
		private static readonly BitVector32.Section allowOleDropSection = BitVector32.CreateSection(1, RichTextBox.linkcursorSection);

		// Token: 0x04001F9B RID: 8091
		private static readonly BitVector32.Section suppressTextChangedEventSection = BitVector32.CreateSection(1, RichTextBox.allowOleDropSection);

		// Token: 0x04001F9C RID: 8092
		private static readonly BitVector32.Section callOnContentsResizedSection = BitVector32.CreateSection(1, RichTextBox.suppressTextChangedEventSection);

		// Token: 0x04001F9D RID: 8093
		private static readonly BitVector32.Section richTextShortcutsEnabledSection = BitVector32.CreateSection(1, RichTextBox.callOnContentsResizedSection);

		// Token: 0x04001F9E RID: 8094
		private static readonly BitVector32.Section allowOleObjectsSection = BitVector32.CreateSection(1, RichTextBox.richTextShortcutsEnabledSection);

		// Token: 0x04001F9F RID: 8095
		private static readonly BitVector32.Section scrollBarsSection = BitVector32.CreateSection(19, RichTextBox.allowOleObjectsSection);

		// Token: 0x04001FA0 RID: 8096
		private static readonly BitVector32.Section enableAutoDragDropSection = BitVector32.CreateSection(1, RichTextBox.scrollBarsSection);

		// Token: 0x020007D9 RID: 2009
		private class OleCallback : UnsafeNativeMethods.IRichEditOleCallback
		{
			// Token: 0x06006D87 RID: 28039 RVA: 0x00191997 File Offset: 0x0018FB97
			internal OleCallback(RichTextBox owner)
			{
				this.owner = owner;
			}

			// Token: 0x06006D88 RID: 28040 RVA: 0x001919A8 File Offset: 0x0018FBA8
			public int GetNewStorage(out UnsafeNativeMethods.IStorage storage)
			{
				if (!this.owner.AllowOleObjects)
				{
					storage = null;
					return -2147467259;
				}
				UnsafeNativeMethods.ILockBytes lockBytes = UnsafeNativeMethods.CreateILockBytesOnHGlobal(NativeMethods.NullHandleRef, true);
				storage = UnsafeNativeMethods.StgCreateDocfileOnILockBytes(lockBytes, 4114, 0);
				return 0;
			}

			// Token: 0x06006D89 RID: 28041 RVA: 0x0003BC68 File Offset: 0x00039E68
			public int GetInPlaceContext(IntPtr lplpFrame, IntPtr lplpDoc, IntPtr lpFrameInfo)
			{
				return -2147467263;
			}

			// Token: 0x06006D8A RID: 28042 RVA: 0x0001180C File Offset: 0x0000FA0C
			public int ShowContainerUI(int fShow)
			{
				return 0;
			}

			// Token: 0x06006D8B RID: 28043 RVA: 0x001919E8 File Offset: 0x0018FBE8
			public int QueryInsertObject(ref Guid lpclsid, IntPtr lpstg, int cp)
			{
				try
				{
					IntSecurity.UnmanagedCode.Demand();
					return 0;
				}
				catch (SecurityException)
				{
				}
				Guid guid = default(Guid);
				int num = UnsafeNativeMethods.ReadClassStg(new HandleRef(null, lpstg), ref guid);
				if (!NativeMethods.Succeeded(num))
				{
					return 1;
				}
				if (guid == Guid.Empty)
				{
					guid = lpclsid;
				}
				string text = guid.ToString().ToUpper(CultureInfo.InvariantCulture);
				if (text == "00000315-0000-0000-C000-000000000046" || text == "00000316-0000-0000-C000-000000000046" || text == "00000319-0000-0000-C000-000000000046" || text == "0003000A-0000-0000-C000-000000000046")
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06006D8C RID: 28044 RVA: 0x0001180C File Offset: 0x0000FA0C
			public int DeleteObject(IntPtr lpoleobj)
			{
				return 0;
			}

			// Token: 0x06006D8D RID: 28045 RVA: 0x00191AA0 File Offset: 0x0018FCA0
			public int QueryAcceptData(IDataObject lpdataobj, IntPtr lpcfFormat, int reco, int fReally, IntPtr hMetaPict)
			{
				if (reco != 1)
				{
					return -2147467263;
				}
				if (!this.owner.AllowDrop && !this.owner.EnableAutoDragDrop)
				{
					this.lastDataObject = null;
					return -2147467259;
				}
				MouseButtons mouseButtons = Control.MouseButtons;
				Keys modifierKeys = Control.ModifierKeys;
				int num = 0;
				if ((mouseButtons & MouseButtons.Left) == MouseButtons.Left)
				{
					num |= 1;
				}
				if ((mouseButtons & MouseButtons.Right) == MouseButtons.Right)
				{
					num |= 2;
				}
				if ((mouseButtons & MouseButtons.Middle) == MouseButtons.Middle)
				{
					num |= 16;
				}
				if ((modifierKeys & Keys.Control) == Keys.Control)
				{
					num |= 8;
				}
				if ((modifierKeys & Keys.Shift) == Keys.Shift)
				{
					num |= 4;
				}
				this.lastDataObject = new DataObject(lpdataobj);
				if (!this.owner.EnableAutoDragDrop)
				{
					this.lastEffect = DragDropEffects.None;
				}
				DragEventArgs dragEventArgs = new DragEventArgs(this.lastDataObject, num, Control.MousePosition.X, Control.MousePosition.Y, DragDropEffects.All, this.lastEffect);
				if (fReally == 0)
				{
					dragEventArgs.Effect = (((num & 8) == 8) ? DragDropEffects.Copy : DragDropEffects.Move);
					this.owner.OnDragEnter(dragEventArgs);
				}
				else
				{
					this.owner.OnDragDrop(dragEventArgs);
					this.lastDataObject = null;
				}
				this.lastEffect = dragEventArgs.Effect;
				if (dragEventArgs.Effect == DragDropEffects.None)
				{
					return -2147467259;
				}
				return 0;
			}

			// Token: 0x06006D8E RID: 28046 RVA: 0x0003BC68 File Offset: 0x00039E68
			public int ContextSensitiveHelp(int fEnterMode)
			{
				return -2147467263;
			}

			// Token: 0x06006D8F RID: 28047 RVA: 0x0003BC68 File Offset: 0x00039E68
			public int GetClipboardData(NativeMethods.CHARRANGE lpchrg, int reco, IntPtr lplpdataobj)
			{
				return -2147467263;
			}

			// Token: 0x06006D90 RID: 28048 RVA: 0x00191BF4 File Offset: 0x0018FDF4
			public int GetDragDropEffect(bool fDrag, int grfKeyState, ref int pdwEffect)
			{
				if (this.owner.AllowDrop || this.owner.EnableAutoDragDrop)
				{
					if (fDrag && grfKeyState == 0)
					{
						if (this.owner.EnableAutoDragDrop)
						{
							this.lastEffect = DragDropEffects.All;
						}
						else
						{
							this.lastEffect = DragDropEffects.None;
						}
					}
					else if (!fDrag && this.lastDataObject != null && grfKeyState != 0)
					{
						DragEventArgs dragEventArgs = new DragEventArgs(this.lastDataObject, grfKeyState, Control.MousePosition.X, Control.MousePosition.Y, DragDropEffects.All, this.lastEffect);
						if (this.lastEffect != DragDropEffects.None)
						{
							dragEventArgs.Effect = (((grfKeyState & 8) == 8) ? DragDropEffects.Copy : DragDropEffects.Move);
						}
						this.owner.OnDragOver(dragEventArgs);
						this.lastEffect = dragEventArgs.Effect;
					}
					pdwEffect = (int)this.lastEffect;
				}
				else
				{
					pdwEffect = 0;
				}
				return 0;
			}

			// Token: 0x06006D91 RID: 28049 RVA: 0x00191CC8 File Offset: 0x0018FEC8
			public int GetContextMenu(short seltype, IntPtr lpoleobj, NativeMethods.CHARRANGE lpchrg, out IntPtr hmenu)
			{
				ContextMenu contextMenu = this.owner.ContextMenu;
				if (contextMenu == null || !this.owner.ShortcutsEnabled)
				{
					hmenu = IntPtr.Zero;
				}
				else
				{
					contextMenu.sourceControl = this.owner;
					contextMenu.OnPopup(EventArgs.Empty);
					IntPtr handle = contextMenu.Handle;
					Menu menu = contextMenu;
					for (;;)
					{
						int i = 0;
						int itemCount = menu.ItemCount;
						while (i < itemCount)
						{
							if (menu.items[i].handle != IntPtr.Zero)
							{
								menu = menu.items[i];
								break;
							}
							i++;
						}
						if (i == itemCount)
						{
							menu.handle = IntPtr.Zero;
							menu.created = false;
							if (menu == contextMenu)
							{
								break;
							}
							menu = ((MenuItem)menu).Menu;
						}
					}
					hmenu = handle;
				}
				return 0;
			}

			// Token: 0x040042AB RID: 17067
			private RichTextBox owner;

			// Token: 0x040042AC RID: 17068
			private IDataObject lastDataObject;

			// Token: 0x040042AD RID: 17069
			private DragDropEffects lastEffect;
		}
	}
}
