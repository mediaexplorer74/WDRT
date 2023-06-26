using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Media;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Uses a mask to distinguish between proper and improper user input.</summary>
	// Token: 0x020002E5 RID: 741
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultEvent("MaskInputRejected")]
	[DefaultBindingProperty("Text")]
	[DefaultProperty("Mask")]
	[Designer("System.Windows.Forms.Design.MaskedTextBoxDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionMaskedTextBox")]
	public class MaskedTextBox : TextBoxBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MaskedTextBox" /> class using defaults.</summary>
		// Token: 0x06002ECB RID: 11979 RVA: 0x000D3490 File Offset: 0x000D1690
		public MaskedTextBox()
		{
			MaskedTextProvider maskedTextProvider = new MaskedTextProvider("<>", CultureInfo.CurrentCulture);
			this.flagState[MaskedTextBox.IS_NULL_MASK] = true;
			this.Initialize(maskedTextProvider);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MaskedTextBox" /> class using the specified input mask.</summary>
		/// <param name="mask">A <see cref="T:System.String" /> representing the input mask. The initial value of the <see cref="P:System.Windows.Forms.MaskedTextBox.Mask" /> property.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="mask" /> is <see langword="null" />.</exception>
		// Token: 0x06002ECC RID: 11980 RVA: 0x000D34CC File Offset: 0x000D16CC
		public MaskedTextBox(string mask)
		{
			if (mask == null)
			{
				throw new ArgumentNullException();
			}
			MaskedTextProvider maskedTextProvider = new MaskedTextProvider(mask, CultureInfo.CurrentCulture);
			this.flagState[MaskedTextBox.IS_NULL_MASK] = false;
			this.Initialize(maskedTextProvider);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MaskedTextBox" /> class using the specified custom mask language provider.</summary>
		/// <param name="maskedTextProvider">A custom mask language provider, derived from the <see cref="T:System.ComponentModel.MaskedTextProvider" /> class.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="maskedTextProvider" /> is <see langword="null" />.</exception>
		// Token: 0x06002ECD RID: 11981 RVA: 0x000D350C File Offset: 0x000D170C
		public MaskedTextBox(MaskedTextProvider maskedTextProvider)
		{
			if (maskedTextProvider == null)
			{
				throw new ArgumentNullException();
			}
			this.flagState[MaskedTextBox.IS_NULL_MASK] = false;
			this.Initialize(maskedTextProvider);
		}

		// Token: 0x06002ECE RID: 11982 RVA: 0x000D3538 File Offset: 0x000D1738
		private void Initialize(MaskedTextProvider maskedTextProvider)
		{
			this.maskedTextProvider = maskedTextProvider;
			if (!this.flagState[MaskedTextBox.IS_NULL_MASK])
			{
				this.SetWindowText();
			}
			this.passwordChar = this.maskedTextProvider.PasswordChar;
			this.insertMode = InsertKeyMode.Default;
			this.flagState[MaskedTextBox.HIDE_PROMPT_ON_LEAVE] = false;
			this.flagState[MaskedTextBox.BEEP_ON_ERROR] = false;
			this.flagState[MaskedTextBox.USE_SYSTEM_PASSWORD_CHAR] = false;
			this.flagState[MaskedTextBox.REJECT_INPUT_ON_FIRST_FAILURE] = false;
			this.flagState[MaskedTextBox.CUTCOPYINCLUDEPROMPT] = this.maskedTextProvider.IncludePrompt;
			this.flagState[MaskedTextBox.CUTCOPYINCLUDELITERALS] = this.maskedTextProvider.IncludeLiterals;
			this.flagState[MaskedTextBox.HANDLE_KEY_PRESS] = true;
			this.caretTestPos = 0;
		}

		/// <summary>Gets or sets a value determining how TAB keys are handled for multiline configurations. This property is not supported by <see cref="T:System.Windows.Forms.MaskedTextBox" />.</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x06002ECF RID: 11983 RVA: 0x0001180C File Offset: 0x0000FA0C
		// (set) Token: 0x06002ED0 RID: 11984 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool AcceptsTab
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets a value indicating whether <see cref="P:System.Windows.Forms.MaskedTextBox.PromptChar" /> can be entered as valid data by the user.</summary>
		/// <returns>
		///   <see langword="true" /> if the user can enter the prompt character into the control; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x06002ED1 RID: 11985 RVA: 0x000D360E File Offset: 0x000D180E
		// (set) Token: 0x06002ED2 RID: 11986 RVA: 0x000D361C File Offset: 0x000D181C
		[SRCategory("CatBehavior")]
		[SRDescription("MaskedTextBoxAllowPromptAsInputDescr")]
		[DefaultValue(true)]
		public bool AllowPromptAsInput
		{
			get
			{
				return this.maskedTextProvider.AllowPromptAsInput;
			}
			set
			{
				if (value != this.maskedTextProvider.AllowPromptAsInput)
				{
					MaskedTextProvider maskedTextProvider = new MaskedTextProvider(this.maskedTextProvider.Mask, this.maskedTextProvider.Culture, value, this.maskedTextProvider.PromptChar, this.maskedTextProvider.PasswordChar, this.maskedTextProvider.AsciiOnly);
					this.SetMaskedTextProvider(maskedTextProvider);
				}
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.MaskedTextBox.AcceptsTab" /> property has changed. This event is not raised by <see cref="T:System.Windows.Forms.MaskedTextBox" />.</summary>
		// Token: 0x14000222 RID: 546
		// (add) Token: 0x06002ED3 RID: 11987 RVA: 0x000070A6 File Offset: 0x000052A6
		// (remove) Token: 0x06002ED4 RID: 11988 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new event EventHandler AcceptsTabChanged
		{
			add
			{
			}
			remove
			{
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.MaskedTextBox" /> control accepts characters outside of the ASCII character set.</summary>
		/// <returns>
		///   <see langword="true" /> if only ASCII is accepted; <see langword="false" /> if the <see cref="T:System.Windows.Forms.MaskedTextBox" /> control can accept any arbitrary Unicode character. The default is <see langword="false" />.</returns>
		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x06002ED5 RID: 11989 RVA: 0x000D367C File Offset: 0x000D187C
		// (set) Token: 0x06002ED6 RID: 11990 RVA: 0x000D368C File Offset: 0x000D188C
		[SRCategory("CatBehavior")]
		[SRDescription("MaskedTextBoxAsciiOnlyDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[DefaultValue(false)]
		public bool AsciiOnly
		{
			get
			{
				return this.maskedTextProvider.AsciiOnly;
			}
			set
			{
				if (value != this.maskedTextProvider.AsciiOnly)
				{
					MaskedTextProvider maskedTextProvider = new MaskedTextProvider(this.maskedTextProvider.Mask, this.maskedTextProvider.Culture, this.maskedTextProvider.AllowPromptAsInput, this.maskedTextProvider.PromptChar, this.maskedTextProvider.PasswordChar, value);
					this.SetMaskedTextProvider(maskedTextProvider);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the masked text box control raises the system beep for each user key stroke that it rejects.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.MaskedTextBox" /> control should beep on invalid input; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x06002ED7 RID: 11991 RVA: 0x000D36EC File Offset: 0x000D18EC
		// (set) Token: 0x06002ED8 RID: 11992 RVA: 0x000D36FE File Offset: 0x000D18FE
		[SRCategory("CatBehavior")]
		[SRDescription("MaskedTextBoxBeepOnErrorDescr")]
		[DefaultValue(false)]
		public bool BeepOnError
		{
			get
			{
				return this.flagState[MaskedTextBox.BEEP_ON_ERROR];
			}
			set
			{
				this.flagState[MaskedTextBox.BEEP_ON_ERROR] = value;
			}
		}

		/// <summary>Gets a value indicating whether the user can undo the previous operation. This property is not supported by <see cref="T:System.Windows.Forms.MaskedTextBox" />.</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x06002ED9 RID: 11993 RVA: 0x0001180C File Offset: 0x0000FA0C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool CanUndo
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> representing the information needed when creating a control.</returns>
		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x06002EDA RID: 11994 RVA: 0x000D3714 File Offset: 0x000D1914
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				HorizontalAlignment horizontalAlignment = base.RtlTranslateHorizontal(this.textAlign);
				createParams.ExStyle &= -4097;
				switch (horizontalAlignment)
				{
				case HorizontalAlignment.Left:
					createParams.Style |= 0;
					break;
				case HorizontalAlignment.Right:
					createParams.Style |= 2;
					break;
				case HorizontalAlignment.Center:
					createParams.Style |= 1;
					break;
				}
				return createParams;
			}
		}

		/// <summary>Gets or sets the culture information associated with the masked text box.</summary>
		/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> representing the culture supported by the <see cref="T:System.Windows.Forms.MaskedTextBox" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Windows.Forms.MaskedTextBox.Culture" /> was set to <see langword="null" />.</exception>
		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x06002EDB RID: 11995 RVA: 0x000D378A File Offset: 0x000D198A
		// (set) Token: 0x06002EDC RID: 11996 RVA: 0x000D3798 File Offset: 0x000D1998
		[SRCategory("CatBehavior")]
		[SRDescription("MaskedTextBoxCultureDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public CultureInfo Culture
		{
			get
			{
				return this.maskedTextProvider.Culture;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				if (!this.maskedTextProvider.Culture.Equals(value))
				{
					MaskedTextProvider maskedTextProvider = new MaskedTextProvider(this.maskedTextProvider.Mask, value, this.maskedTextProvider.AllowPromptAsInput, this.maskedTextProvider.PromptChar, this.maskedTextProvider.PasswordChar, this.maskedTextProvider.AsciiOnly);
					this.SetMaskedTextProvider(maskedTextProvider);
				}
			}
		}

		/// <summary>Gets or sets a value that determines whether literals and prompt characters are copied to the clipboard.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.MaskFormat" /> values. The default is <see cref="F:System.Windows.Forms.MaskFormat.IncludeLiterals" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">Property set with a <see cref="T:System.Windows.Forms.MaskFormat" /> value that is not valid.</exception>
		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x06002EDD RID: 11997 RVA: 0x000D3806 File Offset: 0x000D1A06
		// (set) Token: 0x06002EDE RID: 11998 RVA: 0x000D3848 File Offset: 0x000D1A48
		[SRCategory("CatBehavior")]
		[SRDescription("MaskedTextBoxCutCopyMaskFormat")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[DefaultValue(MaskFormat.IncludeLiterals)]
		public MaskFormat CutCopyMaskFormat
		{
			get
			{
				if (this.flagState[MaskedTextBox.CUTCOPYINCLUDEPROMPT])
				{
					if (this.flagState[MaskedTextBox.CUTCOPYINCLUDELITERALS])
					{
						return MaskFormat.IncludePromptAndLiterals;
					}
					return MaskFormat.IncludePrompt;
				}
				else
				{
					if (this.flagState[MaskedTextBox.CUTCOPYINCLUDELITERALS])
					{
						return MaskFormat.IncludeLiterals;
					}
					return MaskFormat.ExcludePromptAndLiterals;
				}
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(MaskFormat));
				}
				if (value == MaskFormat.IncludePrompt)
				{
					this.flagState[MaskedTextBox.CUTCOPYINCLUDEPROMPT] = true;
					this.flagState[MaskedTextBox.CUTCOPYINCLUDELITERALS] = false;
					return;
				}
				if (value == MaskFormat.IncludeLiterals)
				{
					this.flagState[MaskedTextBox.CUTCOPYINCLUDEPROMPT] = false;
					this.flagState[MaskedTextBox.CUTCOPYINCLUDELITERALS] = true;
					return;
				}
				bool flag = value == MaskFormat.IncludePromptAndLiterals;
				this.flagState[MaskedTextBox.CUTCOPYINCLUDEPROMPT] = flag;
				this.flagState[MaskedTextBox.CUTCOPYINCLUDELITERALS] = flag;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.IFormatProvider" /> to use when performing type validation.</summary>
		/// <returns>An object that implements the <see cref="T:System.IFormatProvider" /> interface.</returns>
		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x06002EDF RID: 11999 RVA: 0x000D38F0 File Offset: 0x000D1AF0
		// (set) Token: 0x06002EE0 RID: 12000 RVA: 0x000D38F8 File Offset: 0x000D1AF8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IFormatProvider FormatProvider
		{
			get
			{
				return this.formatProvider;
			}
			set
			{
				this.formatProvider = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the prompt characters in the input mask are hidden when the masked text box loses focus.</summary>
		/// <returns>
		///   <see langword="true" /> if <see cref="P:System.Windows.Forms.MaskedTextBox.PromptChar" /> is hidden when <see cref="T:System.Windows.Forms.MaskedTextBox" /> does not have focus; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x06002EE1 RID: 12001 RVA: 0x000D3901 File Offset: 0x000D1B01
		// (set) Token: 0x06002EE2 RID: 12002 RVA: 0x000D3914 File Offset: 0x000D1B14
		[SRCategory("CatBehavior")]
		[SRDescription("MaskedTextBoxHidePromptOnLeaveDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[DefaultValue(false)]
		public bool HidePromptOnLeave
		{
			get
			{
				return this.flagState[MaskedTextBox.HIDE_PROMPT_ON_LEAVE];
			}
			set
			{
				if (this.flagState[MaskedTextBox.HIDE_PROMPT_ON_LEAVE] != value)
				{
					this.flagState[MaskedTextBox.HIDE_PROMPT_ON_LEAVE] = value;
					if (!this.flagState[MaskedTextBox.IS_NULL_MASK] && !this.Focused && !this.MaskFull && !base.DesignMode)
					{
						this.SetWindowText();
					}
				}
			}
		}

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x06002EE3 RID: 12003 RVA: 0x000D3975 File Offset: 0x000D1B75
		// (set) Token: 0x06002EE4 RID: 12004 RVA: 0x000D3982 File Offset: 0x000D1B82
		private bool IncludeLiterals
		{
			get
			{
				return this.maskedTextProvider.IncludeLiterals;
			}
			set
			{
				this.maskedTextProvider.IncludeLiterals = value;
			}
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x06002EE5 RID: 12005 RVA: 0x000D3990 File Offset: 0x000D1B90
		// (set) Token: 0x06002EE6 RID: 12006 RVA: 0x000D399D File Offset: 0x000D1B9D
		private bool IncludePrompt
		{
			get
			{
				return this.maskedTextProvider.IncludePrompt;
			}
			set
			{
				this.maskedTextProvider.IncludePrompt = value;
			}
		}

		/// <summary>Gets or sets the text insertion mode of the masked text box control.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.InsertKeyMode" /> value that indicates the current insertion mode. The default is <see cref="F:System.Windows.Forms.InsertKeyMode.Default" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">An invalid <see cref="T:System.Windows.Forms.InsertKeyMode" /> value was supplied when setting this property.</exception>
		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x06002EE7 RID: 12007 RVA: 0x000D39AB File Offset: 0x000D1BAB
		// (set) Token: 0x06002EE8 RID: 12008 RVA: 0x000D39B4 File Offset: 0x000D1BB4
		[SRCategory("CatBehavior")]
		[SRDescription("MaskedTextBoxInsertKeyModeDescr")]
		[DefaultValue(InsertKeyMode.Default)]
		public InsertKeyMode InsertKeyMode
		{
			get
			{
				return this.insertMode;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(InsertKeyMode));
				}
				if (this.insertMode != value)
				{
					bool isOverwriteMode = this.IsOverwriteMode;
					this.insertMode = value;
					if (isOverwriteMode != this.IsOverwriteMode)
					{
						this.OnIsOverwriteModeChanged(EventArgs.Empty);
					}
				}
			}
		}

		/// <summary>Determines whether the specified key is an input key or a special key that requires preprocessing.</summary>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values.</param>
		// Token: 0x06002EE9 RID: 12009 RVA: 0x000D3A12 File Offset: 0x000D1C12
		protected override bool IsInputKey(Keys keyData)
		{
			return (keyData & Keys.KeyCode) != Keys.Return && base.IsInputKey(keyData);
		}

		/// <summary>Gets a value that specifies whether new user input overwrites existing input.</summary>
		/// <returns>
		///   <see langword="true" /> if <see cref="T:System.Windows.Forms.MaskedTextBox" /> will overwrite existing characters as the user enters new ones; <see langword="false" /> if <see cref="T:System.Windows.Forms.MaskedTextBox" /> will shift existing characters forward. The default is <see langword="false" />.</returns>
		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x06002EEA RID: 12010 RVA: 0x000D3A28 File Offset: 0x000D1C28
		[Browsable(false)]
		public bool IsOverwriteMode
		{
			get
			{
				if (this.flagState[MaskedTextBox.IS_NULL_MASK])
				{
					return false;
				}
				switch (this.insertMode)
				{
				case InsertKeyMode.Default:
					return this.flagState[MaskedTextBox.INSERT_TOGGLED];
				case InsertKeyMode.Insert:
					return false;
				case InsertKeyMode.Overwrite:
					return true;
				default:
					return false;
				}
			}
		}

		/// <summary>Occurs after the insert mode has changed.</summary>
		// Token: 0x14000223 RID: 547
		// (add) Token: 0x06002EEB RID: 12011 RVA: 0x000D3A7A File Offset: 0x000D1C7A
		// (remove) Token: 0x06002EEC RID: 12012 RVA: 0x000D3A8D File Offset: 0x000D1C8D
		[SRCategory("CatPropertyChanged")]
		[SRDescription("MaskedTextBoxIsOverwriteModeChangedDescr")]
		public event EventHandler IsOverwriteModeChanged
		{
			add
			{
				base.Events.AddHandler(MaskedTextBox.EVENT_ISOVERWRITEMODECHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(MaskedTextBox.EVENT_ISOVERWRITEMODECHANGED, value);
			}
		}

		/// <summary>Gets or sets the lines of text in multiline configurations. This property is not supported by <see cref="T:System.Windows.Forms.MaskedTextBox" />.</summary>
		/// <returns>An array of type <see cref="T:System.String" /> that contains a single line.</returns>
		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x06002EED RID: 12013 RVA: 0x000D3AA0 File Offset: 0x000D1CA0
		// (set) Token: 0x06002EEE RID: 12014 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new string[] Lines
		{
			get
			{
				this.flagState[MaskedTextBox.QUERY_BASE_TEXT] = true;
				string[] lines;
				try
				{
					lines = base.Lines;
				}
				finally
				{
					this.flagState[MaskedTextBox.QUERY_BASE_TEXT] = false;
				}
				return lines;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the input mask to use at run time.</summary>
		/// <returns>A <see cref="T:System.String" /> representing the current mask. The default value is the empty string which allows any input.</returns>
		/// <exception cref="T:System.ArgumentException">The string supplied to the <see cref="P:System.Windows.Forms.MaskedTextBox.Mask" /> property is not a valid mask. Invalid masks include masks containing non-printable characters.</exception>
		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x06002EEF RID: 12015 RVA: 0x000D3AEC File Offset: 0x000D1CEC
		// (set) Token: 0x06002EF0 RID: 12016 RVA: 0x000D3B14 File Offset: 0x000D1D14
		[SRCategory("CatBehavior")]
		[SRDescription("MaskedTextBoxMaskDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[DefaultValue("")]
		[MergableProperty(false)]
		[Localizable(true)]
		[Editor("System.Windows.Forms.Design.MaskPropertyEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string Mask
		{
			get
			{
				if (!this.flagState[MaskedTextBox.IS_NULL_MASK])
				{
					return this.maskedTextProvider.Mask;
				}
				return string.Empty;
			}
			set
			{
				if (this.flagState[MaskedTextBox.IS_NULL_MASK] == string.IsNullOrEmpty(value) && (this.flagState[MaskedTextBox.IS_NULL_MASK] || value == this.maskedTextProvider.Mask))
				{
					return;
				}
				string text = null;
				string text2 = value;
				if (string.IsNullOrEmpty(value))
				{
					string textOutput = this.TextOutput;
					string text3 = this.maskedTextProvider.ToString(false, false);
					this.flagState[MaskedTextBox.IS_NULL_MASK] = true;
					if (this.maskedTextProvider.IsPassword)
					{
						this.SetEditControlPasswordChar(this.maskedTextProvider.PasswordChar);
					}
					this.SetWindowText(text3, false, false);
					EventArgs empty = EventArgs.Empty;
					this.OnMaskChanged(empty);
					if (text3 != textOutput)
					{
						this.OnTextChanged(empty);
					}
					text2 = "<>";
				}
				else
				{
					for (int i = 0; i < value.Length; i++)
					{
						char c = value[i];
						if (!MaskedTextProvider.IsValidMaskChar(c))
						{
							throw new ArgumentException(SR.GetString("MaskedTextBoxMaskInvalidChar"));
						}
					}
					if (this.flagState[MaskedTextBox.IS_NULL_MASK])
					{
						text = this.Text;
					}
				}
				MaskedTextProvider maskedTextProvider = new MaskedTextProvider(text2, this.maskedTextProvider.Culture, this.maskedTextProvider.AllowPromptAsInput, this.maskedTextProvider.PromptChar, this.maskedTextProvider.PasswordChar, this.maskedTextProvider.AsciiOnly);
				this.SetMaskedTextProvider(maskedTextProvider, text);
			}
		}

		/// <summary>Occurs after the input mask is changed.</summary>
		// Token: 0x14000224 RID: 548
		// (add) Token: 0x06002EF1 RID: 12017 RVA: 0x000D3C7F File Offset: 0x000D1E7F
		// (remove) Token: 0x06002EF2 RID: 12018 RVA: 0x000D3C92 File Offset: 0x000D1E92
		[SRCategory("CatPropertyChanged")]
		[SRDescription("MaskedTextBoxMaskChangedDescr")]
		public event EventHandler MaskChanged
		{
			add
			{
				base.Events.AddHandler(MaskedTextBox.EVENT_MASKCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(MaskedTextBox.EVENT_MASKCHANGED, value);
			}
		}

		/// <summary>Gets a value indicating whether all required inputs have been entered into the input mask.</summary>
		/// <returns>
		///   <see langword="true" /> if all required input has been entered into the mask; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x06002EF3 RID: 12019 RVA: 0x000D3CA5 File Offset: 0x000D1EA5
		[Browsable(false)]
		public bool MaskCompleted
		{
			get
			{
				return this.maskedTextProvider.MaskCompleted;
			}
		}

		/// <summary>Gets a value indicating whether all required and optional inputs have been entered into the input mask.</summary>
		/// <returns>
		///   <see langword="true" /> if all required and optional inputs have been entered; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x06002EF4 RID: 12020 RVA: 0x000D3CB2 File Offset: 0x000D1EB2
		[Browsable(false)]
		public bool MaskFull
		{
			get
			{
				return this.maskedTextProvider.MaskFull;
			}
		}

		/// <summary>Gets a clone of the mask provider associated with this instance of the masked text box control.</summary>
		/// <returns>A masking language provider of type <see cref="P:System.Windows.Forms.MaskedTextBox.MaskedTextProvider" />.</returns>
		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x06002EF5 RID: 12021 RVA: 0x000D3CBF File Offset: 0x000D1EBF
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public MaskedTextProvider MaskedTextProvider
		{
			get
			{
				if (!this.flagState[MaskedTextBox.IS_NULL_MASK])
				{
					return (MaskedTextProvider)this.maskedTextProvider.Clone();
				}
				return null;
			}
		}

		/// <summary>Occurs when the user's input or assigned character does not match the corresponding format element of the input mask.</summary>
		// Token: 0x14000225 RID: 549
		// (add) Token: 0x06002EF6 RID: 12022 RVA: 0x000D3CE5 File Offset: 0x000D1EE5
		// (remove) Token: 0x06002EF7 RID: 12023 RVA: 0x000D3CF8 File Offset: 0x000D1EF8
		[SRCategory("CatBehavior")]
		[SRDescription("MaskedTextBoxMaskInputRejectedDescr")]
		public event MaskInputRejectedEventHandler MaskInputRejected
		{
			add
			{
				base.Events.AddHandler(MaskedTextBox.EVENT_MASKINPUTREJECTED, value);
			}
			remove
			{
				base.Events.RemoveHandler(MaskedTextBox.EVENT_MASKINPUTREJECTED, value);
			}
		}

		/// <summary>Gets or sets the maximum number of characters the user can type or paste into the text box control. This property is not supported by <see cref="T:System.Windows.Forms.MaskedTextBox" />.</summary>
		/// <returns>This property always returns 0.</returns>
		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x06002EF8 RID: 12024 RVA: 0x000D3D0B File Offset: 0x000D1F0B
		// (set) Token: 0x06002EF9 RID: 12025 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override int MaxLength
		{
			get
			{
				return base.MaxLength;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets a value indicating whether this is a multiline text box control. This property is not fully supported by <see cref="T:System.Windows.Forms.MaskedTextBox" />.</summary>
		/// <returns>This property always returns <see langword="false" />.</returns>
		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x06002EFA RID: 12026 RVA: 0x0001180C File Offset: 0x0000FA0C
		// (set) Token: 0x06002EFB RID: 12027 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool Multiline
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		/// <summary>Typically occurs when the value of the <see cref="P:System.Windows.Forms.MaskedTextBox.Multiline" /> property has changed; however, this event is not raised by <see cref="T:System.Windows.Forms.MaskedTextBox" />.</summary>
		// Token: 0x14000226 RID: 550
		// (add) Token: 0x06002EFC RID: 12028 RVA: 0x000070A6 File Offset: 0x000052A6
		// (remove) Token: 0x06002EFD RID: 12029 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new event EventHandler MultilineChanged
		{
			add
			{
			}
			remove
			{
			}
		}

		/// <summary>Gets or sets the character to be displayed in substitute for user input.</summary>
		/// <returns>The <see cref="T:System.Char" /> value used as the password character.</returns>
		/// <exception cref="T:System.ArgumentException">The character specified when setting this property is not a valid password character, as determined by the <see cref="M:System.ComponentModel.MaskedTextProvider.IsValidPasswordChar(System.Char)" /> method of the <see cref="T:System.ComponentModel.MaskedTextProvider" /> class.</exception>
		/// <exception cref="T:System.InvalidOperationException">The password character specified is the same as the current prompt character, <see cref="P:System.Windows.Forms.MaskedTextBox.PromptChar" />. The two are required to be different.</exception>
		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x06002EFE RID: 12030 RVA: 0x000D3D13 File Offset: 0x000D1F13
		// (set) Token: 0x06002EFF RID: 12031 RVA: 0x000D3D20 File Offset: 0x000D1F20
		[SRCategory("CatBehavior")]
		[SRDescription("MaskedTextBoxPasswordCharDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[DefaultValue('\0')]
		public char PasswordChar
		{
			get
			{
				return this.maskedTextProvider.PasswordChar;
			}
			set
			{
				if (!MaskedTextProvider.IsValidPasswordChar(value))
				{
					throw new ArgumentException(SR.GetString("MaskedTextBoxInvalidCharError"));
				}
				if (this.passwordChar != value)
				{
					if (value == this.maskedTextProvider.PromptChar)
					{
						throw new InvalidOperationException(SR.GetString("MaskedTextBoxPasswordAndPromptCharError"));
					}
					this.passwordChar = value;
					if (!this.UseSystemPasswordChar)
					{
						this.maskedTextProvider.PasswordChar = value;
						if (this.flagState[MaskedTextBox.IS_NULL_MASK])
						{
							this.SetEditControlPasswordChar(value);
						}
						else
						{
							this.SetWindowText();
						}
						base.VerifyImeRestrictedModeChanged();
					}
				}
			}
		}

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x06002F00 RID: 12032 RVA: 0x000D3DAE File Offset: 0x000D1FAE
		internal override bool PasswordProtect
		{
			get
			{
				if (this.maskedTextProvider != null)
				{
					return this.maskedTextProvider.IsPassword;
				}
				return base.PasswordProtect;
			}
		}

		/// <summary>Gets or sets the character used to represent the absence of user input in <see cref="T:System.Windows.Forms.MaskedTextBox" />.</summary>
		/// <returns>The character used to prompt the user for input. The default is an underscore (_).</returns>
		/// <exception cref="T:System.ArgumentException">The character specified when setting this property is not a valid prompt character, as determined by the <see cref="M:System.ComponentModel.MaskedTextProvider.IsValidPasswordChar(System.Char)" /> method of the <see cref="T:System.ComponentModel.MaskedTextProvider" /> class.</exception>
		/// <exception cref="T:System.InvalidOperationException">The prompt character specified is the same as the current password character, <see cref="P:System.Windows.Forms.MaskedTextBox.PasswordChar" />. The two are required to be different.</exception>
		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x06002F01 RID: 12033 RVA: 0x000D3DCA File Offset: 0x000D1FCA
		// (set) Token: 0x06002F02 RID: 12034 RVA: 0x000D3DD8 File Offset: 0x000D1FD8
		[SRCategory("CatAppearance")]
		[SRDescription("MaskedTextBoxPromptCharDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[Localizable(true)]
		[DefaultValue('_')]
		public char PromptChar
		{
			get
			{
				return this.maskedTextProvider.PromptChar;
			}
			set
			{
				if (!MaskedTextProvider.IsValidInputChar(value))
				{
					throw new ArgumentException(SR.GetString("MaskedTextBoxInvalidCharError"));
				}
				if (this.maskedTextProvider.PromptChar != value)
				{
					if (value == this.passwordChar || value == this.maskedTextProvider.PasswordChar)
					{
						throw new InvalidOperationException(SR.GetString("MaskedTextBoxPasswordAndPromptCharError"));
					}
					MaskedTextProvider maskedTextProvider = new MaskedTextProvider(this.maskedTextProvider.Mask, this.maskedTextProvider.Culture, this.maskedTextProvider.AllowPromptAsInput, value, this.maskedTextProvider.PasswordChar, this.maskedTextProvider.AsciiOnly);
					this.SetMaskedTextProvider(maskedTextProvider);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether text in the text box is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> to indicate the text is read only; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x06002F03 RID: 12035 RVA: 0x000D3E77 File Offset: 0x000D2077
		// (set) Token: 0x06002F04 RID: 12036 RVA: 0x000D3E7F File Offset: 0x000D207F
		public new bool ReadOnly
		{
			get
			{
				return base.ReadOnly;
			}
			set
			{
				if (this.ReadOnly != value)
				{
					base.ReadOnly = value;
					if (!this.flagState[MaskedTextBox.IS_NULL_MASK])
					{
						this.SetWindowText();
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the parsing of user input should stop after the first invalid character is reached.</summary>
		/// <returns>
		///   <see langword="true" /> if processing of the input string should be terminated at the first parsing error; otherwise, <see langword="false" /> if processing should ignore all errors. The default is <see langword="false" />.</returns>
		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x06002F05 RID: 12037 RVA: 0x000D3EA9 File Offset: 0x000D20A9
		// (set) Token: 0x06002F06 RID: 12038 RVA: 0x000D3EBB File Offset: 0x000D20BB
		[SRCategory("CatBehavior")]
		[SRDescription("MaskedTextBoxRejectInputOnFirstFailureDescr")]
		[DefaultValue(false)]
		public bool RejectInputOnFirstFailure
		{
			get
			{
				return this.flagState[MaskedTextBox.REJECT_INPUT_ON_FIRST_FAILURE];
			}
			set
			{
				this.flagState[MaskedTextBox.REJECT_INPUT_ON_FIRST_FAILURE] = value;
			}
		}

		/// <summary>Gets or sets a value that determines how an input character that matches the prompt character should be handled.</summary>
		/// <returns>
		///   <see langword="true" /> if the prompt character entered as input causes the current editable position in the mask to be reset; otherwise, <see langword="false" /> to indicate that the prompt character is to be processed as a normal input character. The default is <see langword="true" />.</returns>
		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x06002F07 RID: 12039 RVA: 0x000D3ECE File Offset: 0x000D20CE
		// (set) Token: 0x06002F08 RID: 12040 RVA: 0x000D3EDB File Offset: 0x000D20DB
		[SRCategory("CatBehavior")]
		[SRDescription("MaskedTextBoxResetOnPrompt")]
		[DefaultValue(true)]
		public bool ResetOnPrompt
		{
			get
			{
				return this.maskedTextProvider.ResetOnPrompt;
			}
			set
			{
				this.maskedTextProvider.ResetOnPrompt = value;
			}
		}

		/// <summary>Gets or sets a value that determines how a space input character should be handled.</summary>
		/// <returns>
		///   <see langword="true" /> if the space input character causes the current editable position in the mask to be reset; otherwise, <see langword="false" /> to indicate that it is to be processed as a normal input character. The default is <see langword="true" />.</returns>
		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x06002F09 RID: 12041 RVA: 0x000D3EE9 File Offset: 0x000D20E9
		// (set) Token: 0x06002F0A RID: 12042 RVA: 0x000D3EF6 File Offset: 0x000D20F6
		[SRCategory("CatBehavior")]
		[SRDescription("MaskedTextBoxResetOnSpace")]
		[DefaultValue(true)]
		public bool ResetOnSpace
		{
			get
			{
				return this.maskedTextProvider.ResetOnSpace;
			}
			set
			{
				this.maskedTextProvider.ResetOnSpace = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the user is allowed to reenter literal values.</summary>
		/// <returns>
		///   <see langword="true" /> to allow literals to be reentered; otherwise, <see langword="false" /> to prevent the user from overwriting literal characters. The default is <see langword="true" />.</returns>
		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x06002F0B RID: 12043 RVA: 0x000D3F04 File Offset: 0x000D2104
		// (set) Token: 0x06002F0C RID: 12044 RVA: 0x000D3F11 File Offset: 0x000D2111
		[SRCategory("CatBehavior")]
		[SRDescription("MaskedTextBoxSkipLiterals")]
		[DefaultValue(true)]
		public bool SkipLiterals
		{
			get
			{
				return this.maskedTextProvider.SkipLiterals;
			}
			set
			{
				this.maskedTextProvider.SkipLiterals = value;
			}
		}

		/// <summary>Gets or sets the current selection in the <see cref="T:System.Windows.Forms.MaskedTextBox" /> control.</summary>
		/// <returns>The currently selected text as a <see cref="T:System.String" />. If no text is currently selected, this property resolves to an empty string.</returns>
		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x06002F0D RID: 12045 RVA: 0x000D3F1F File Offset: 0x000D211F
		// (set) Token: 0x06002F0E RID: 12046 RVA: 0x000136B0 File Offset: 0x000118B0
		public override string SelectedText
		{
			get
			{
				if (this.flagState[MaskedTextBox.IS_NULL_MASK])
				{
					return base.SelectedText;
				}
				return this.GetSelectedText();
			}
			set
			{
				this.SetSelectedTextInternal(value, true);
			}
		}

		// Token: 0x06002F0F RID: 12047 RVA: 0x000D3F40 File Offset: 0x000D2140
		internal override void SetSelectedTextInternal(string value, bool clearUndo)
		{
			if (this.flagState[MaskedTextBox.IS_NULL_MASK])
			{
				base.SetSelectedTextInternal(value, true);
				return;
			}
			this.PasteInt(value);
		}

		// Token: 0x06002F10 RID: 12048 RVA: 0x000D3F64 File Offset: 0x000D2164
		private void ImeComplete()
		{
			this.flagState[MaskedTextBox.IME_COMPLETING] = true;
			this.ImeNotify(1);
		}

		// Token: 0x06002F11 RID: 12049 RVA: 0x000D3F80 File Offset: 0x000D2180
		private void ImeNotify(int action)
		{
			HandleRef handleRef = new HandleRef(this, base.Handle);
			IntPtr intPtr = UnsafeNativeMethods.ImmGetContext(handleRef);
			if (intPtr != IntPtr.Zero)
			{
				try
				{
					UnsafeNativeMethods.ImmNotifyIME(new HandleRef(null, intPtr), 21, action, 0);
				}
				finally
				{
					UnsafeNativeMethods.ImmReleaseContext(handleRef, new HandleRef(null, intPtr));
				}
			}
		}

		// Token: 0x06002F12 RID: 12050 RVA: 0x000D3FE4 File Offset: 0x000D21E4
		private void SetEditControlPasswordChar(char pwdChar)
		{
			if (base.IsHandleCreated)
			{
				base.SendMessage(204, (int)pwdChar, 0);
				base.Invalidate();
			}
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x06002F13 RID: 12051 RVA: 0x000D4004 File Offset: 0x000D2204
		private char SystemPasswordChar
		{
			get
			{
				if (MaskedTextBox.systemPwdChar == '\0')
				{
					TextBox textBox = new TextBox();
					textBox.UseSystemPasswordChar = true;
					MaskedTextBox.systemPwdChar = textBox.PasswordChar;
					textBox.Dispose();
				}
				return MaskedTextBox.systemPwdChar;
			}
		}

		/// <summary>Gets or sets the text as it is currently displayed to the user.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the text currently displayed by the control. The default is an empty string.</returns>
		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x06002F14 RID: 12052 RVA: 0x000D403B File Offset: 0x000D223B
		// (set) Token: 0x06002F15 RID: 12053 RVA: 0x000D4070 File Offset: 0x000D2270
		[Editor("System.Windows.Forms.Design.MaskedTextBoxTextEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[SRCategory("CatAppearance")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[Bindable(true)]
		[DefaultValue("")]
		[Localizable(true)]
		public override string Text
		{
			get
			{
				if (this.flagState[MaskedTextBox.IS_NULL_MASK] || this.flagState[MaskedTextBox.QUERY_BASE_TEXT])
				{
					return base.Text;
				}
				return this.TextOutput;
			}
			set
			{
				if (this.flagState[MaskedTextBox.IS_NULL_MASK])
				{
					base.Text = value;
					return;
				}
				if (string.IsNullOrEmpty(value))
				{
					this.Delete(Keys.Delete, 0, this.maskedTextProvider.Length);
					return;
				}
				if (!this.RejectInputOnFirstFailure)
				{
					this.Replace(value, 0, this.maskedTextProvider.Length);
					return;
				}
				string textOutput = this.TextOutput;
				MaskedTextResultHint maskedTextResultHint;
				if (this.maskedTextProvider.Set(value, out this.caretTestPos, out maskedTextResultHint))
				{
					if (this.TextOutput != textOutput)
					{
						this.SetText();
					}
					int num = this.caretTestPos + 1;
					this.caretTestPos = num;
					base.SelectionStart = num;
					return;
				}
				this.OnMaskInputRejected(new MaskInputRejectedEventArgs(this.caretTestPos, maskedTextResultHint));
			}
		}

		/// <summary>Gets the length of the displayed text.</summary>
		/// <returns>An Int32 representing the number of characters in the <see cref="P:System.Windows.Forms.MaskedTextBox.Text" /> property. <see cref="P:System.Windows.Forms.MaskedTextBox.TextLength" /> respects properties such as <see cref="P:System.Windows.Forms.MaskedTextBox.HidePromptOnLeave" />, which means that the return results may be different depending on whether the control has focus.</returns>
		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x06002F16 RID: 12054 RVA: 0x000D412B File Offset: 0x000D232B
		[Browsable(false)]
		public override int TextLength
		{
			get
			{
				if (this.flagState[MaskedTextBox.IS_NULL_MASK])
				{
					return base.TextLength;
				}
				return this.GetFormattedDisplayString().Length;
			}
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x06002F17 RID: 12055 RVA: 0x000D4151 File Offset: 0x000D2351
		private string TextOutput
		{
			get
			{
				return this.maskedTextProvider.ToString();
			}
		}

		/// <summary>Gets or sets how text is aligned in a masked text box control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> enumeration values that specifies how text is aligned relative to the control. The default is <see cref="F:System.Windows.Forms.HorizontalAlignment.Left" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned to this property is not of type <see cref="T:System.Windows.Forms.HorizontalAlignment" />.</exception>
		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x06002F18 RID: 12056 RVA: 0x000D415E File Offset: 0x000D235E
		// (set) Token: 0x06002F19 RID: 12057 RVA: 0x000D4168 File Offset: 0x000D2368
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[DefaultValue(HorizontalAlignment.Left)]
		[SRDescription("TextBoxTextAlignDescr")]
		public HorizontalAlignment TextAlign
		{
			get
			{
				return this.textAlign;
			}
			set
			{
				if (this.textAlign != value)
				{
					if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
					{
						throw new InvalidEnumArgumentException("value", (int)value, typeof(HorizontalAlignment));
					}
					this.textAlign = value;
					base.RecreateHandle();
					this.OnTextAlignChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the text alignment is changed.</summary>
		// Token: 0x14000227 RID: 551
		// (add) Token: 0x06002F1A RID: 12058 RVA: 0x000D41BC File Offset: 0x000D23BC
		// (remove) Token: 0x06002F1B RID: 12059 RVA: 0x000D41CF File Offset: 0x000D23CF
		[SRCategory("CatPropertyChanged")]
		[SRDescription("RadioButtonOnTextAlignChangedDescr")]
		public event EventHandler TextAlignChanged
		{
			add
			{
				base.Events.AddHandler(MaskedTextBox.EVENT_TEXTALIGNCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(MaskedTextBox.EVENT_TEXTALIGNCHANGED, value);
			}
		}

		/// <summary>Gets or sets a value that determines whether literals and prompt characters are included in the formatted string.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.MaskFormat" /> values. The default is <see cref="F:System.Windows.Forms.MaskFormat.IncludeLiterals" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">Property set with a <see cref="T:System.Windows.Forms.MaskFormat" /> value that is not valid.</exception>
		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x06002F1C RID: 12060 RVA: 0x000D41E2 File Offset: 0x000D23E2
		// (set) Token: 0x06002F1D RID: 12061 RVA: 0x000D4204 File Offset: 0x000D2404
		[SRCategory("CatBehavior")]
		[SRDescription("MaskedTextBoxTextMaskFormat")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[DefaultValue(MaskFormat.IncludeLiterals)]
		public MaskFormat TextMaskFormat
		{
			get
			{
				if (this.IncludePrompt)
				{
					if (this.IncludeLiterals)
					{
						return MaskFormat.IncludePromptAndLiterals;
					}
					return MaskFormat.IncludePrompt;
				}
				else
				{
					if (this.IncludeLiterals)
					{
						return MaskFormat.IncludeLiterals;
					}
					return MaskFormat.ExcludePromptAndLiterals;
				}
			}
			set
			{
				if (this.TextMaskFormat == value)
				{
					return;
				}
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(MaskFormat));
				}
				string text = (this.flagState[MaskedTextBox.IS_NULL_MASK] ? null : this.TextOutput);
				if (value == MaskFormat.IncludePrompt)
				{
					this.IncludePrompt = true;
					this.IncludeLiterals = false;
				}
				else if (value == MaskFormat.IncludeLiterals)
				{
					this.IncludePrompt = false;
					this.IncludeLiterals = true;
				}
				else
				{
					bool flag = value == MaskFormat.IncludePromptAndLiterals;
					this.IncludePrompt = flag;
					this.IncludeLiterals = flag;
				}
				if (text != null && text != this.TextOutput)
				{
					this.OnTextChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Returns a string that represents the current masked text box. This method overrides <see cref="M:System.Windows.Forms.TextBoxBase.ToString" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains information about the current <see cref="T:System.Windows.Forms.MaskedTextBox" />. The string includes the type, a simplified view of the input string, and the formatted input string.</returns>
		// Token: 0x06002F1E RID: 12062 RVA: 0x000D42B4 File Offset: 0x000D24B4
		public override string ToString()
		{
			if (this.flagState[MaskedTextBox.IS_NULL_MASK])
			{
				return base.ToString();
			}
			bool includePrompt = this.IncludePrompt;
			bool includeLiterals = this.IncludeLiterals;
			string text;
			try
			{
				this.IncludePrompt = (this.IncludeLiterals = true);
				text = base.ToString();
			}
			finally
			{
				this.IncludePrompt = includePrompt;
				this.IncludeLiterals = includeLiterals;
			}
			return text;
		}

		/// <summary>Occurs when <see cref="T:System.Windows.Forms.MaskedTextBox" /> has finished parsing the current value using the <see cref="P:System.Windows.Forms.MaskedTextBox.ValidatingType" /> property.</summary>
		// Token: 0x14000228 RID: 552
		// (add) Token: 0x06002F1F RID: 12063 RVA: 0x000D4324 File Offset: 0x000D2524
		// (remove) Token: 0x06002F20 RID: 12064 RVA: 0x000D4337 File Offset: 0x000D2537
		[SRCategory("CatFocus")]
		[SRDescription("MaskedTextBoxTypeValidationCompletedDescr")]
		public event TypeValidationEventHandler TypeValidationCompleted
		{
			add
			{
				base.Events.AddHandler(MaskedTextBox.EVENT_VALIDATIONCOMPLETED, value);
			}
			remove
			{
				base.Events.RemoveHandler(MaskedTextBox.EVENT_VALIDATIONCOMPLETED, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the operating system-supplied password character should be used.</summary>
		/// <returns>
		///   <see langword="true" /> if the system password should be used as the prompt character; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The password character specified is the same as the current prompt character, <see cref="P:System.Windows.Forms.MaskedTextBox.PromptChar" />. The two are required to be different.</exception>
		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x06002F21 RID: 12065 RVA: 0x000D434A File Offset: 0x000D254A
		// (set) Token: 0x06002F22 RID: 12066 RVA: 0x000D435C File Offset: 0x000D255C
		[SRCategory("CatBehavior")]
		[SRDescription("MaskedTextBoxUseSystemPasswordCharDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[DefaultValue(false)]
		public bool UseSystemPasswordChar
		{
			get
			{
				return this.flagState[MaskedTextBox.USE_SYSTEM_PASSWORD_CHAR];
			}
			set
			{
				if (value != this.flagState[MaskedTextBox.USE_SYSTEM_PASSWORD_CHAR])
				{
					if (value)
					{
						if (this.SystemPasswordChar == this.PromptChar)
						{
							throw new InvalidOperationException(SR.GetString("MaskedTextBoxPasswordAndPromptCharError"));
						}
						this.maskedTextProvider.PasswordChar = this.SystemPasswordChar;
					}
					else
					{
						this.maskedTextProvider.PasswordChar = this.passwordChar;
					}
					this.flagState[MaskedTextBox.USE_SYSTEM_PASSWORD_CHAR] = value;
					if (this.flagState[MaskedTextBox.IS_NULL_MASK])
					{
						this.SetEditControlPasswordChar(this.maskedTextProvider.PasswordChar);
					}
					else
					{
						this.SetWindowText();
					}
					base.VerifyImeRestrictedModeChanged();
				}
			}
		}

		/// <summary>Gets or sets the data type used to verify the data input by the user.</summary>
		/// <returns>A <see cref="T:System.Type" /> representing the data type used in validation. The default is <see langword="null" />.</returns>
		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x06002F23 RID: 12067 RVA: 0x000D4406 File Offset: 0x000D2606
		// (set) Token: 0x06002F24 RID: 12068 RVA: 0x000D440E File Offset: 0x000D260E
		[Browsable(false)]
		[DefaultValue(null)]
		public Type ValidatingType
		{
			get
			{
				return this.validatingType;
			}
			set
			{
				if (this.validatingType != value)
				{
					this.validatingType = value;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether a multiline text box control automatically wraps words to the beginning of the next line when necessary. This property is not supported by <see cref="T:System.Windows.Forms.MaskedTextBox" />.</summary>
		/// <returns>The <see cref="P:System.Windows.Forms.MaskedTextBox.WordWrap" /> property always returns <see langword="false" />.</returns>
		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x06002F25 RID: 12069 RVA: 0x0001180C File Offset: 0x0000FA0C
		// (set) Token: 0x06002F26 RID: 12070 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool WordWrap
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		/// <summary>Clears information about the most recent operation from the undo buffer of the text box. This method is not supported by <see cref="T:System.Windows.Forms.MaskedTextBox" />.</summary>
		// Token: 0x06002F27 RID: 12071 RVA: 0x000070A6 File Offset: 0x000052A6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new void ClearUndo()
		{
		}

		/// <summary>Creates a handle for the control.</summary>
		// Token: 0x06002F28 RID: 12072 RVA: 0x000D4425 File Offset: 0x000D2625
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[UIPermission(SecurityAction.InheritanceDemand, Window = UIPermissionWindow.AllWindows)]
		protected override void CreateHandle()
		{
			if (!this.flagState[MaskedTextBox.IS_NULL_MASK] && base.RecreatingHandle)
			{
				this.SetWindowText(this.GetFormattedDisplayString(), false, false);
			}
			base.CreateHandle();
		}

		// Token: 0x06002F29 RID: 12073 RVA: 0x000D4458 File Offset: 0x000D2658
		private void Delete(Keys keyCode, int startPosition, int selectionLen)
		{
			this.caretTestPos = startPosition;
			if (selectionLen == 0)
			{
				if (keyCode == Keys.Back)
				{
					if (startPosition == 0)
					{
						return;
					}
					startPosition--;
				}
				else if (startPosition + selectionLen == this.maskedTextProvider.Length)
				{
					return;
				}
			}
			int num = ((selectionLen > 0) ? (startPosition + selectionLen - 1) : startPosition);
			string textOutput = this.TextOutput;
			int num2;
			MaskedTextResultHint maskedTextResultHint;
			if (this.maskedTextProvider.RemoveAt(startPosition, num, out num2, out maskedTextResultHint))
			{
				if (this.TextOutput != textOutput)
				{
					this.SetText();
					this.caretTestPos = startPosition;
				}
				else if (selectionLen > 0)
				{
					this.caretTestPos = startPosition;
				}
				else if (maskedTextResultHint == MaskedTextResultHint.NoEffect)
				{
					if (keyCode == Keys.Delete)
					{
						this.caretTestPos = this.maskedTextProvider.FindEditPositionFrom(startPosition, true);
					}
					else
					{
						if (this.maskedTextProvider.FindAssignedEditPositionFrom(startPosition, true) == MaskedTextProvider.InvalidIndex)
						{
							this.caretTestPos = this.maskedTextProvider.FindAssignedEditPositionFrom(startPosition, false);
						}
						else
						{
							this.caretTestPos = this.maskedTextProvider.FindEditPositionFrom(startPosition, false);
						}
						if (this.caretTestPos != MaskedTextProvider.InvalidIndex)
						{
							this.caretTestPos++;
						}
					}
					if (this.caretTestPos == MaskedTextProvider.InvalidIndex)
					{
						this.caretTestPos = startPosition;
					}
				}
				else if (keyCode == Keys.Back)
				{
					this.caretTestPos = startPosition;
				}
			}
			else
			{
				this.OnMaskInputRejected(new MaskInputRejectedEventArgs(num2, maskedTextResultHint));
			}
			base.SelectInternal(this.caretTestPos, 0, this.maskedTextProvider.Length);
		}

		/// <summary>Retrieves the character that is closest to the specified location within the control.</summary>
		/// <param name="pt">The location from which to seek the nearest character.</param>
		/// <returns>The character at the specified location.</returns>
		// Token: 0x06002F2A RID: 12074 RVA: 0x000D45AC File Offset: 0x000D27AC
		public override char GetCharFromPosition(Point pt)
		{
			this.flagState[MaskedTextBox.QUERY_BASE_TEXT] = true;
			char charFromPosition;
			try
			{
				charFromPosition = base.GetCharFromPosition(pt);
			}
			finally
			{
				this.flagState[MaskedTextBox.QUERY_BASE_TEXT] = false;
			}
			return charFromPosition;
		}

		/// <summary>Retrieves the index of the character nearest to the specified location.</summary>
		/// <param name="pt">The location to search.</param>
		/// <returns>The zero-based character index at the specified location.</returns>
		// Token: 0x06002F2B RID: 12075 RVA: 0x000D45F8 File Offset: 0x000D27F8
		public override int GetCharIndexFromPosition(Point pt)
		{
			this.flagState[MaskedTextBox.QUERY_BASE_TEXT] = true;
			int charIndexFromPosition;
			try
			{
				charIndexFromPosition = base.GetCharIndexFromPosition(pt);
			}
			finally
			{
				this.flagState[MaskedTextBox.QUERY_BASE_TEXT] = false;
			}
			return charIndexFromPosition;
		}

		// Token: 0x06002F2C RID: 12076 RVA: 0x000D4644 File Offset: 0x000D2844
		internal override int GetEndPosition()
		{
			if (this.flagState[MaskedTextBox.IS_NULL_MASK])
			{
				return base.GetEndPosition();
			}
			int num = this.maskedTextProvider.FindEditPositionFrom(this.maskedTextProvider.LastAssignedPosition + 1, true);
			if (num == MaskedTextProvider.InvalidIndex)
			{
				num = this.maskedTextProvider.LastAssignedPosition + 1;
			}
			return num;
		}

		/// <summary>Retrieves the index of the first character of the current line. This method is not supported by <see cref="T:System.Windows.Forms.MaskedTextBox" />.</summary>
		/// <returns>This method will always return 0.</returns>
		// Token: 0x06002F2D RID: 12077 RVA: 0x0001180C File Offset: 0x0000FA0C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new int GetFirstCharIndexOfCurrentLine()
		{
			return 0;
		}

		/// <summary>Retrieves the index of the first character of a given line. This method is not supported by <see cref="T:System.Windows.Forms.MaskedTextBox" />.</summary>
		/// <param name="lineNumber">This parameter is not used.</param>
		/// <returns>This method will always return 0.</returns>
		// Token: 0x06002F2E RID: 12078 RVA: 0x0001180C File Offset: 0x0000FA0C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new int GetFirstCharIndexFromLine(int lineNumber)
		{
			return 0;
		}

		// Token: 0x06002F2F RID: 12079 RVA: 0x000D469C File Offset: 0x000D289C
		private string GetFormattedDisplayString()
		{
			bool flag = !this.ReadOnly && (base.DesignMode || !this.HidePromptOnLeave || this.Focused);
			return this.maskedTextProvider.ToString(false, flag, true, 0, this.maskedTextProvider.Length);
		}

		/// <summary>Retrieves the line number from the specified character position within the text of the control. This method is not supported by <see cref="T:System.Windows.Forms.MaskedTextBox" />.</summary>
		/// <param name="index">This parameter is not used.</param>
		/// <returns>This method will always return 0.</returns>
		// Token: 0x06002F30 RID: 12080 RVA: 0x0001180C File Offset: 0x0000FA0C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetLineFromCharIndex(int index)
		{
			return 0;
		}

		/// <summary>Retrieves the location within the control at the specified character index.</summary>
		/// <param name="index">The index of the character for which to retrieve the location.</param>
		/// <returns>The location of the specified character within the client rectangle of the control.</returns>
		// Token: 0x06002F31 RID: 12081 RVA: 0x000D46F0 File Offset: 0x000D28F0
		public override Point GetPositionFromCharIndex(int index)
		{
			this.flagState[MaskedTextBox.QUERY_BASE_TEXT] = true;
			Point positionFromCharIndex;
			try
			{
				positionFromCharIndex = base.GetPositionFromCharIndex(index);
			}
			finally
			{
				this.flagState[MaskedTextBox.QUERY_BASE_TEXT] = false;
			}
			return positionFromCharIndex;
		}

		// Token: 0x06002F32 RID: 12082 RVA: 0x000D473C File Offset: 0x000D293C
		internal override Size GetPreferredSizeCore(Size proposedConstraints)
		{
			this.flagState[MaskedTextBox.QUERY_BASE_TEXT] = true;
			Size preferredSizeCore;
			try
			{
				preferredSizeCore = base.GetPreferredSizeCore(proposedConstraints);
			}
			finally
			{
				this.flagState[MaskedTextBox.QUERY_BASE_TEXT] = false;
			}
			return preferredSizeCore;
		}

		// Token: 0x06002F33 RID: 12083 RVA: 0x000D4788 File Offset: 0x000D2988
		private string GetSelectedText()
		{
			int num;
			int num2;
			base.GetSelectionStartAndLength(out num, out num2);
			if (num2 == 0)
			{
				return string.Empty;
			}
			bool flag = (this.CutCopyMaskFormat & MaskFormat.IncludePrompt) > MaskFormat.ExcludePromptAndLiterals;
			bool flag2 = (this.CutCopyMaskFormat & MaskFormat.IncludeLiterals) > MaskFormat.ExcludePromptAndLiterals;
			return this.maskedTextProvider.ToString(true, flag, flag2, num, num2);
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Control.BackColor" /> property changes.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002F34 RID: 12084 RVA: 0x000D47D0 File Offset: 0x000D29D0
		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);
			if (Application.RenderWithVisualStyles && base.IsHandleCreated && base.BorderStyle == BorderStyle.Fixed3D)
			{
				SafeNativeMethods.RedrawWindow(new HandleRef(this, base.Handle), null, NativeMethods.NullHandleRef, 1025);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002F35 RID: 12085 RVA: 0x000D480E File Offset: 0x000D2A0E
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			base.SetSelectionOnHandle();
			if (this.flagState[MaskedTextBox.IS_NULL_MASK] && this.maskedTextProvider.IsPassword)
			{
				this.SetEditControlPasswordChar(this.maskedTextProvider.PasswordChar);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.MaskedTextBox.IsOverwriteModeChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data.</param>
		// Token: 0x06002F36 RID: 12086 RVA: 0x000D4850 File Offset: 0x000D2A50
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnIsOverwriteModeChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[MaskedTextBox.EVENT_ISOVERWRITEMODECHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
		// Token: 0x06002F37 RID: 12087 RVA: 0x000D4880 File Offset: 0x000D2A80
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (this.flagState[MaskedTextBox.IS_NULL_MASK])
			{
				return;
			}
			Keys keys = e.KeyCode;
			if (keys == Keys.Return || keys == Keys.Escape)
			{
				this.flagState[MaskedTextBox.HANDLE_KEY_PRESS] = false;
			}
			if (keys == Keys.Insert && e.Modifiers == Keys.None && this.insertMode == InsertKeyMode.Default)
			{
				this.flagState[MaskedTextBox.INSERT_TOGGLED] = !this.flagState[MaskedTextBox.INSERT_TOGGLED];
				this.OnIsOverwriteModeChanged(EventArgs.Empty);
				return;
			}
			if (e.Control && char.IsLetter((char)keys))
			{
				if (keys != Keys.H)
				{
					this.flagState[MaskedTextBox.HANDLE_KEY_PRESS] = false;
					return;
				}
				keys = Keys.Back;
			}
			if ((keys == Keys.Delete || keys == Keys.Back) && !this.ReadOnly)
			{
				int num;
				int num2;
				base.GetSelectionStartAndLength(out num, out num2);
				Keys modifiers = e.Modifiers;
				if (modifiers != Keys.Shift)
				{
					if (modifiers == Keys.Control)
					{
						if (num2 == 0)
						{
							if (keys == Keys.Delete)
							{
								num2 = this.maskedTextProvider.Length - num;
							}
							else
							{
								num2 = ((num == this.maskedTextProvider.Length) ? num : (num + 1));
								num = 0;
							}
						}
					}
				}
				else if (keys == Keys.Delete)
				{
					keys = Keys.Back;
				}
				if (!this.flagState[MaskedTextBox.HANDLE_KEY_PRESS])
				{
					this.flagState[MaskedTextBox.HANDLE_KEY_PRESS] = true;
				}
				this.Delete(keys, num, num2);
				e.SuppressKeyPress = true;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains the event data.</param>
		// Token: 0x06002F38 RID: 12088 RVA: 0x000D49E0 File Offset: 0x000D2BE0
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);
			if (this.flagState[MaskedTextBox.IS_NULL_MASK])
			{
				return;
			}
			if (!this.flagState[MaskedTextBox.HANDLE_KEY_PRESS])
			{
				this.flagState[MaskedTextBox.HANDLE_KEY_PRESS] = true;
				if (!char.IsLetter(e.KeyChar))
				{
					return;
				}
			}
			if (!this.ReadOnly)
			{
				int num;
				int num2;
				base.GetSelectionStartAndLength(out num, out num2);
				string textOutput = this.TextOutput;
				MaskedTextResultHint maskedTextResultHint;
				if (this.PlaceChar(e.KeyChar, num, num2, this.IsOverwriteMode, out maskedTextResultHint))
				{
					if (this.TextOutput != textOutput)
					{
						this.SetText();
					}
					int num3 = this.caretTestPos + 1;
					this.caretTestPos = num3;
					base.SelectionStart = num3;
					if (ImeModeConversion.InputLanguageTable == ImeModeConversion.KoreanTable)
					{
						int num4 = this.maskedTextProvider.FindUnassignedEditPositionFrom(this.caretTestPos, true);
						if (num4 == MaskedTextProvider.InvalidIndex)
						{
							this.ImeComplete();
						}
					}
				}
				else
				{
					this.OnMaskInputRejected(new MaskInputRejectedEventArgs(this.caretTestPos, maskedTextResultHint));
				}
				if (num2 > 0)
				{
					this.SelectionLength = 0;
				}
				e.Handled = true;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyUp" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
		// Token: 0x06002F39 RID: 12089 RVA: 0x000D4AF0 File Offset: 0x000D2CF0
		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
			if (this.flagState[MaskedTextBox.IME_COMPLETING])
			{
				this.flagState[MaskedTextBox.IME_COMPLETING] = false;
			}
			if (this.flagState[MaskedTextBox.IME_ENDING_COMPOSITION])
			{
				this.flagState[MaskedTextBox.IME_ENDING_COMPOSITION] = false;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.MaskedTextBox.MaskChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data.</param>
		// Token: 0x06002F3A RID: 12090 RVA: 0x000D4B4C File Offset: 0x000D2D4C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMaskChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[MaskedTextBox.EVENT_MASKCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002F3B RID: 12091 RVA: 0x000D4B7C File Offset: 0x000D2D7C
		private void OnMaskInputRejected(MaskInputRejectedEventArgs e)
		{
			if (this.BeepOnError)
			{
				SoundPlayer soundPlayer = new SoundPlayer();
				soundPlayer.Play();
			}
			MaskInputRejectedEventHandler maskInputRejectedEventHandler = base.Events[MaskedTextBox.EVENT_MASKINPUTREJECTED] as MaskInputRejectedEventHandler;
			if (maskInputRejectedEventHandler != null)
			{
				maskInputRejectedEventHandler(this, e);
			}
		}

		/// <summary>Typically raises the <see cref="E:System.Windows.Forms.MaskedTextBox.MultilineChanged" /> event, but disabled for <see cref="T:System.Windows.Forms.MaskedTextBox" />.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data.</param>
		// Token: 0x06002F3C RID: 12092 RVA: 0x000070A6 File Offset: 0x000052A6
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void OnMultilineChanged(EventArgs e)
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.MaskedTextBox.TextAlignChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data.</param>
		// Token: 0x06002F3D RID: 12093 RVA: 0x000D4BC0 File Offset: 0x000D2DC0
		protected virtual void OnTextAlignChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[MaskedTextBox.EVENT_TEXTALIGNCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002F3E RID: 12094 RVA: 0x000D4BF0 File Offset: 0x000D2DF0
		private void OnTypeValidationCompleted(TypeValidationEventArgs e)
		{
			TypeValidationEventHandler typeValidationEventHandler = base.Events[MaskedTextBox.EVENT_VALIDATIONCOMPLETED] as TypeValidationEventHandler;
			if (typeValidationEventHandler != null)
			{
				typeValidationEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Validating" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains event data.</param>
		/// <exception cref="T:System.Exception">A critical exception occurred during the parsing of the input string.</exception>
		// Token: 0x06002F3F RID: 12095 RVA: 0x000D4C1E File Offset: 0x000D2E1E
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnValidating(CancelEventArgs e)
		{
			this.PerformTypeValidation(e);
			base.OnValidating(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.TextChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data.</param>
		// Token: 0x06002F40 RID: 12096 RVA: 0x000D4C30 File Offset: 0x000D2E30
		protected override void OnTextChanged(EventArgs e)
		{
			bool flag = this.flagState[MaskedTextBox.QUERY_BASE_TEXT];
			this.flagState[MaskedTextBox.QUERY_BASE_TEXT] = false;
			try
			{
				base.OnTextChanged(e);
			}
			finally
			{
				this.flagState[MaskedTextBox.QUERY_BASE_TEXT] = flag;
			}
		}

		// Token: 0x06002F41 RID: 12097 RVA: 0x000D4C8C File Offset: 0x000D2E8C
		private void Replace(string text, int startPosition, int selectionLen)
		{
			MaskedTextProvider maskedTextProvider = (MaskedTextProvider)this.maskedTextProvider.Clone();
			int num = this.caretTestPos;
			MaskedTextResultHint maskedTextResultHint = MaskedTextResultHint.NoEffect;
			int num2 = startPosition + selectionLen - 1;
			if (this.RejectInputOnFirstFailure)
			{
				if (!((startPosition > num2) ? maskedTextProvider.InsertAt(text, startPosition, out this.caretTestPos, out maskedTextResultHint) : maskedTextProvider.Replace(text, startPosition, num2, out this.caretTestPos, out maskedTextResultHint)))
				{
					this.OnMaskInputRejected(new MaskInputRejectedEventArgs(this.caretTestPos, maskedTextResultHint));
				}
			}
			else
			{
				MaskedTextResultHint maskedTextResultHint2 = maskedTextResultHint;
				int i = 0;
				while (i < text.Length)
				{
					char c = text[i];
					if (this.maskedTextProvider.VerifyEscapeChar(c, startPosition))
					{
						goto IL_BF;
					}
					int num3 = maskedTextProvider.FindEditPositionFrom(startPosition, true);
					if (num3 != MaskedTextProvider.InvalidIndex)
					{
						startPosition = num3;
						goto IL_BF;
					}
					this.OnMaskInputRejected(new MaskInputRejectedEventArgs(startPosition, MaskedTextResultHint.UnavailableEditPosition));
					IL_109:
					i++;
					continue;
					IL_BF:
					int num4 = ((num2 >= startPosition) ? 1 : 0);
					bool flag = num4 > 0;
					if (!this.PlaceChar(maskedTextProvider, c, startPosition, num4, flag, out maskedTextResultHint2))
					{
						this.OnMaskInputRejected(new MaskInputRejectedEventArgs(startPosition, maskedTextResultHint2));
						goto IL_109;
					}
					startPosition = this.caretTestPos + 1;
					if (maskedTextResultHint2 == MaskedTextResultHint.Success && maskedTextResultHint != maskedTextResultHint2)
					{
						maskedTextResultHint = maskedTextResultHint2;
						goto IL_109;
					}
					goto IL_109;
				}
				if (selectionLen > 0 && startPosition <= num2)
				{
					if (!maskedTextProvider.RemoveAt(startPosition, num2, out this.caretTestPos, out maskedTextResultHint2))
					{
						this.OnMaskInputRejected(new MaskInputRejectedEventArgs(this.caretTestPos, maskedTextResultHint2));
					}
					if (maskedTextResultHint == MaskedTextResultHint.NoEffect && maskedTextResultHint != maskedTextResultHint2)
					{
						maskedTextResultHint = maskedTextResultHint2;
					}
				}
			}
			bool flag2 = this.TextOutput != maskedTextProvider.ToString();
			this.maskedTextProvider = maskedTextProvider;
			if (flag2)
			{
				this.SetText();
				this.caretTestPos = startPosition;
				base.SelectInternal(this.caretTestPos, 0, this.maskedTextProvider.Length);
				return;
			}
			this.caretTestPos = num;
		}

		// Token: 0x06002F42 RID: 12098 RVA: 0x000D4E3C File Offset: 0x000D303C
		private void PasteInt(string text)
		{
			int num;
			int num2;
			base.GetSelectionStartAndLength(out num, out num2);
			if (string.IsNullOrEmpty(text))
			{
				this.Delete(Keys.Delete, num, num2);
				return;
			}
			this.Replace(text, num, num2);
		}

		// Token: 0x06002F43 RID: 12099 RVA: 0x000D4E70 File Offset: 0x000D3070
		private object PerformTypeValidation(CancelEventArgs e)
		{
			object obj = null;
			if (this.validatingType != null)
			{
				string text = null;
				if (!this.flagState[MaskedTextBox.IS_NULL_MASK] && !this.maskedTextProvider.MaskCompleted)
				{
					text = SR.GetString("MaskedTextBoxIncompleteMsg");
				}
				else
				{
					string text2;
					if (!this.flagState[MaskedTextBox.IS_NULL_MASK])
					{
						text2 = this.maskedTextProvider.ToString(false, this.IncludeLiterals);
					}
					else
					{
						text2 = base.Text;
					}
					try
					{
						obj = Formatter.ParseObject(text2, this.validatingType, typeof(string), null, null, this.formatProvider, null, Formatter.GetDefaultDataSourceNullValue(this.validatingType));
					}
					catch (Exception innerException)
					{
						if (ClientUtils.IsSecurityOrCriticalException(innerException))
						{
							throw;
						}
						if (innerException.InnerException != null)
						{
							innerException = innerException.InnerException;
						}
						text = innerException.GetType().ToString() + ": " + innerException.Message;
					}
				}
				bool flag = false;
				if (text == null)
				{
					flag = true;
					text = SR.GetString("MaskedTextBoxTypeValidationSucceeded");
				}
				TypeValidationEventArgs typeValidationEventArgs = new TypeValidationEventArgs(this.validatingType, flag, obj, text);
				this.OnTypeValidationCompleted(typeValidationEventArgs);
				if (e != null)
				{
					e.Cancel = typeValidationEventArgs.Cancel;
				}
			}
			return obj;
		}

		// Token: 0x06002F44 RID: 12100 RVA: 0x000D4FA8 File Offset: 0x000D31A8
		private bool PlaceChar(char ch, int startPosition, int length, bool overwrite, out MaskedTextResultHint hint)
		{
			return this.PlaceChar(this.maskedTextProvider, ch, startPosition, length, overwrite, out hint);
		}

		// Token: 0x06002F45 RID: 12101 RVA: 0x000D4FC0 File Offset: 0x000D31C0
		private bool PlaceChar(MaskedTextProvider provider, char ch, int startPosition, int length, bool overwrite, out MaskedTextResultHint hint)
		{
			this.caretTestPos = startPosition;
			if (startPosition >= this.maskedTextProvider.Length)
			{
				hint = MaskedTextResultHint.UnavailableEditPosition;
				return false;
			}
			if (length > 0)
			{
				int num = startPosition + length - 1;
				return provider.Replace(ch, startPosition, num, out this.caretTestPos, out hint);
			}
			if (overwrite)
			{
				return provider.Replace(ch, startPosition, out this.caretTestPos, out hint);
			}
			return provider.InsertAt(ch, startPosition, out this.caretTestPos, out hint);
		}

		/// <summary>Processes a command key.</summary>
		/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference that represents the window message to process.</param>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the shortcut key to process.</param>
		/// <returns>
		///   <see langword="true" /> if the command key was processed by the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002F46 RID: 12102 RVA: 0x000D502C File Offset: 0x000D322C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			bool flag = base.ProcessCmdKey(ref msg, keyData);
			if (!flag && keyData == (Keys)131137)
			{
				base.SelectAll();
				flag = true;
			}
			return flag;
		}

		/// <summary>Overrides the base implementation of this method to handle input language changes.</summary>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference that represents the window message to process.</param>
		/// <returns>
		///   <see langword="true" /> if the message was processed by the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002F47 RID: 12103 RVA: 0x000D5058 File Offset: 0x000D3258
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected internal override bool ProcessKeyMessage(ref Message m)
		{
			bool flag = base.ProcessKeyMessage(ref m);
			if (this.flagState[MaskedTextBox.IS_NULL_MASK])
			{
				return flag;
			}
			return (m.Msg == 258 && base.ImeWmCharsToIgnore > 0) || flag;
		}

		// Token: 0x06002F48 RID: 12104 RVA: 0x000D509A File Offset: 0x000D329A
		private void ResetCulture()
		{
			this.Culture = CultureInfo.CurrentCulture;
		}

		/// <summary>Scrolls the contents of the control to the current caret position. This method is not supported by <see cref="T:System.Windows.Forms.MaskedTextBox" />.</summary>
		// Token: 0x06002F49 RID: 12105 RVA: 0x000070A6 File Offset: 0x000052A6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new void ScrollToCaret()
		{
		}

		// Token: 0x06002F4A RID: 12106 RVA: 0x000D50A7 File Offset: 0x000D32A7
		private void SetMaskedTextProvider(MaskedTextProvider newProvider)
		{
			this.SetMaskedTextProvider(newProvider, null);
		}

		// Token: 0x06002F4B RID: 12107 RVA: 0x000D50B4 File Offset: 0x000D32B4
		private void SetMaskedTextProvider(MaskedTextProvider newProvider, string textOnInitializingMask)
		{
			newProvider.IncludePrompt = this.maskedTextProvider.IncludePrompt;
			newProvider.IncludeLiterals = this.maskedTextProvider.IncludeLiterals;
			newProvider.SkipLiterals = this.maskedTextProvider.SkipLiterals;
			newProvider.ResetOnPrompt = this.maskedTextProvider.ResetOnPrompt;
			newProvider.ResetOnSpace = this.maskedTextProvider.ResetOnSpace;
			if (this.flagState[MaskedTextBox.IS_NULL_MASK] && textOnInitializingMask == null)
			{
				this.maskedTextProvider = newProvider;
				return;
			}
			int num = 0;
			MaskedTextResultHint maskedTextResultHint = MaskedTextResultHint.NoEffect;
			MaskedTextProvider maskedTextProvider = this.maskedTextProvider;
			bool flag = maskedTextProvider.Mask == newProvider.Mask;
			string text;
			bool flag2;
			if (textOnInitializingMask != null)
			{
				text = textOnInitializingMask;
				flag2 = !newProvider.Set(textOnInitializingMask, out num, out maskedTextResultHint);
			}
			else
			{
				text = this.TextOutput;
				int i = maskedTextProvider.AssignedEditPositionCount;
				int num2 = 0;
				int num3 = 0;
				while (i > 0)
				{
					num2 = maskedTextProvider.FindAssignedEditPositionFrom(num2, true);
					if (flag)
					{
						num3 = num2;
					}
					else
					{
						num3 = newProvider.FindEditPositionFrom(num3, true);
						if (num3 == MaskedTextProvider.InvalidIndex)
						{
							newProvider.Clear();
							num = newProvider.Length;
							maskedTextResultHint = MaskedTextResultHint.UnavailableEditPosition;
							break;
						}
					}
					if (!newProvider.Replace(maskedTextProvider[num2], num3, out num, out maskedTextResultHint))
					{
						flag = false;
						newProvider.Clear();
						break;
					}
					num2++;
					num3++;
					i--;
				}
				flag2 = !MaskedTextProvider.GetOperationResultFromHint(maskedTextResultHint);
			}
			this.maskedTextProvider = newProvider;
			if (this.flagState[MaskedTextBox.IS_NULL_MASK])
			{
				this.flagState[MaskedTextBox.IS_NULL_MASK] = false;
			}
			if (flag2)
			{
				this.OnMaskInputRejected(new MaskInputRejectedEventArgs(num, maskedTextResultHint));
			}
			if (newProvider.IsPassword)
			{
				this.SetEditControlPasswordChar('\0');
			}
			EventArgs empty = EventArgs.Empty;
			if (textOnInitializingMask != null || maskedTextProvider.Mask != newProvider.Mask)
			{
				this.OnMaskChanged(empty);
			}
			this.SetWindowText(this.GetFormattedDisplayString(), text != this.TextOutput, flag);
		}

		// Token: 0x06002F4C RID: 12108 RVA: 0x000D528B File Offset: 0x000D348B
		private void SetText()
		{
			this.SetWindowText(this.GetFormattedDisplayString(), true, false);
		}

		// Token: 0x06002F4D RID: 12109 RVA: 0x000D529B File Offset: 0x000D349B
		private void SetWindowText()
		{
			this.SetWindowText(this.GetFormattedDisplayString(), false, true);
		}

		// Token: 0x06002F4E RID: 12110 RVA: 0x000D52AC File Offset: 0x000D34AC
		private void SetWindowText(string text, bool raiseTextChangedEvent, bool preserveCaret)
		{
			this.flagState[MaskedTextBox.QUERY_BASE_TEXT] = true;
			try
			{
				if (preserveCaret)
				{
					this.caretTestPos = base.SelectionStart;
				}
				this.WindowText = text;
				if (raiseTextChangedEvent)
				{
					this.OnTextChanged(EventArgs.Empty);
				}
				if (preserveCaret)
				{
					base.SelectionStart = this.caretTestPos;
				}
			}
			finally
			{
				this.flagState[MaskedTextBox.QUERY_BASE_TEXT] = false;
			}
		}

		// Token: 0x06002F4F RID: 12111 RVA: 0x000D5324 File Offset: 0x000D3524
		private bool ShouldSerializeCulture()
		{
			return !CultureInfo.CurrentCulture.Equals(this.Culture);
		}

		/// <summary>Undoes the last edit operation in the text box. This method is not supported by <see cref="T:System.Windows.Forms.MaskedTextBox" />.</summary>
		// Token: 0x06002F50 RID: 12112 RVA: 0x000070A6 File Offset: 0x000052A6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new void Undo()
		{
		}

		/// <summary>Converts the user input string to an instance of the validating type.</summary>
		/// <returns>If successful, an <see cref="T:System.Object" /> of the type specified by the <see cref="P:System.Windows.Forms.MaskedTextBox.ValidatingType" /> property; otherwise, <see langword="null" /> to indicate conversion failure.</returns>
		/// <exception cref="T:System.Exception">A critical exception occurred during the parsing of the input string.</exception>
		// Token: 0x06002F51 RID: 12113 RVA: 0x000D5339 File Offset: 0x000D3539
		public object ValidateText()
		{
			return this.PerformTypeValidation(null);
		}

		// Token: 0x06002F52 RID: 12114 RVA: 0x000D5344 File Offset: 0x000D3544
		private bool WmClear()
		{
			if (!this.ReadOnly)
			{
				int num;
				int num2;
				base.GetSelectionStartAndLength(out num, out num2);
				this.Delete(Keys.Delete, num, num2);
				return true;
			}
			return false;
		}

		// Token: 0x06002F53 RID: 12115 RVA: 0x000D5370 File Offset: 0x000D3570
		private bool WmCopy()
		{
			if (this.maskedTextProvider.IsPassword)
			{
				return false;
			}
			string selectedText = this.GetSelectedText();
			try
			{
				IntSecurity.ClipboardWrite.Assert();
				if (selectedText.Length == 0)
				{
					Clipboard.Clear();
				}
				else
				{
					Clipboard.SetText(selectedText);
				}
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsSecurityOrCriticalException(ex))
				{
					throw;
				}
			}
			return true;
		}

		// Token: 0x06002F54 RID: 12116 RVA: 0x000D53D4 File Offset: 0x000D35D4
		private bool WmImeComposition(ref Message m)
		{
			if (ImeModeConversion.InputLanguageTable == ImeModeConversion.KoreanTable)
			{
				byte b = 0;
				if ((m.LParam.ToInt32() & 8) != 0)
				{
					b = 1;
				}
				else if ((m.LParam.ToInt32() & 2048) != 0)
				{
					b = 2;
				}
				if (b != 0 && this.flagState[MaskedTextBox.IME_ENDING_COMPOSITION])
				{
					return this.flagState[MaskedTextBox.IME_COMPLETING];
				}
			}
			return false;
		}

		// Token: 0x06002F55 RID: 12117 RVA: 0x000D5444 File Offset: 0x000D3644
		private bool WmImeStartComposition()
		{
			int num;
			int num2;
			base.GetSelectionStartAndLength(out num, out num2);
			int num3 = this.maskedTextProvider.FindEditPositionFrom(num, true);
			if (num3 != MaskedTextProvider.InvalidIndex)
			{
				if (num2 > 0 && ImeModeConversion.InputLanguageTable == ImeModeConversion.KoreanTable)
				{
					int num4 = this.maskedTextProvider.FindEditPositionFrom(num + num2 - 1, false);
					if (num4 < num3)
					{
						this.ImeComplete();
						this.OnMaskInputRejected(new MaskInputRejectedEventArgs(num, MaskedTextResultHint.UnavailableEditPosition));
						return true;
					}
					num2 = num4 - num3 + 1;
					this.Delete(Keys.Delete, num3, num2);
				}
				if (num != num3)
				{
					this.caretTestPos = num3;
					base.SelectionStart = this.caretTestPos;
				}
				this.SelectionLength = 0;
				return false;
			}
			this.ImeComplete();
			this.OnMaskInputRejected(new MaskInputRejectedEventArgs(num, MaskedTextResultHint.UnavailableEditPosition));
			return true;
		}

		// Token: 0x06002F56 RID: 12118 RVA: 0x000D54F8 File Offset: 0x000D36F8
		private void WmPaste()
		{
			if (this.ReadOnly)
			{
				return;
			}
			string text;
			try
			{
				IntSecurity.ClipboardRead.Assert();
				text = Clipboard.GetText();
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsSecurityOrCriticalException(ex))
				{
					throw;
				}
				return;
			}
			this.PasteInt(text);
		}

		// Token: 0x06002F57 RID: 12119 RVA: 0x000D5548 File Offset: 0x000D3748
		private void WmPrint(ref Message m)
		{
			base.WndProc(ref m);
			if ((2 & (int)(long)m.LParam) != 0 && Application.RenderWithVisualStyles && base.BorderStyle == BorderStyle.Fixed3D)
			{
				IntSecurity.UnmanagedCode.Assert();
				try
				{
					using (Graphics graphics = Graphics.FromHdc(m.WParam))
					{
						Rectangle rectangle = new Rectangle(0, 0, base.Size.Width - 1, base.Size.Height - 1);
						using (Pen pen = new Pen(VisualStyleInformation.TextControlBorder))
						{
							graphics.DrawRectangle(pen, rectangle);
						}
						rectangle.Inflate(-1, -1);
						graphics.DrawRectangle(SystemPens.Window, rectangle);
					}
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x06002F58 RID: 12120 RVA: 0x000D5634 File Offset: 0x000D3834
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg <= 183)
			{
				if (msg != 123)
				{
					if (msg != 183)
					{
						goto IL_5D;
					}
					return;
				}
			}
			else
			{
				switch (msg)
				{
				case 197:
				case 199:
					return;
				case 198:
					break;
				default:
					if (msg == 772)
					{
						return;
					}
					if (msg == 791)
					{
						this.WmPrint(ref m);
						return;
					}
					goto IL_5D;
				}
			}
			base.ClearUndo();
			base.WndProc(ref m);
			return;
			IL_5D:
			if (this.flagState[MaskedTextBox.IS_NULL_MASK])
			{
				base.WndProc(ref m);
				return;
			}
			int msg2 = m.Msg;
			if (msg2 <= 8)
			{
				if (msg2 == 7)
				{
					this.WmSetFocus();
					base.WndProc(ref m);
					return;
				}
				if (msg2 == 8)
				{
					base.WndProc(ref m);
					this.WmKillFocus();
					return;
				}
			}
			else
			{
				switch (msg2)
				{
				case 269:
					if (this.WmImeStartComposition())
					{
						return;
					}
					break;
				case 270:
					this.flagState[MaskedTextBox.IME_ENDING_COMPOSITION] = true;
					break;
				case 271:
					if (this.WmImeComposition(ref m))
					{
						return;
					}
					break;
				default:
					switch (msg2)
					{
					case 768:
						if (!this.ReadOnly && this.WmCopy())
						{
							this.WmClear();
							return;
						}
						return;
					case 769:
						this.WmCopy();
						return;
					case 770:
						this.WmPaste();
						return;
					case 771:
						this.WmClear();
						return;
					}
					break;
				}
			}
			base.WndProc(ref m);
		}

		// Token: 0x06002F59 RID: 12121 RVA: 0x000D5784 File Offset: 0x000D3984
		private void WmKillFocus()
		{
			base.GetSelectionStartAndLength(out this.caretTestPos, out this.lastSelLength);
			if (this.HidePromptOnLeave && !this.MaskFull)
			{
				this.SetWindowText();
				base.SelectInternal(this.caretTestPos, this.lastSelLength, this.maskedTextProvider.Length);
			}
		}

		// Token: 0x06002F5A RID: 12122 RVA: 0x000D57D6 File Offset: 0x000D39D6
		private void WmSetFocus()
		{
			if (this.HidePromptOnLeave && !this.MaskFull)
			{
				this.SetWindowText();
			}
			base.SelectInternal(this.caretTestPos, this.lastSelLength, this.maskedTextProvider.Length);
		}

		// Token: 0x04001362 RID: 4962
		private const bool forward = true;

		// Token: 0x04001363 RID: 4963
		private const bool backward = false;

		// Token: 0x04001364 RID: 4964
		private const string nullMask = "<>";

		// Token: 0x04001365 RID: 4965
		private static readonly object EVENT_MASKINPUTREJECTED = new object();

		// Token: 0x04001366 RID: 4966
		private static readonly object EVENT_VALIDATIONCOMPLETED = new object();

		// Token: 0x04001367 RID: 4967
		private static readonly object EVENT_TEXTALIGNCHANGED = new object();

		// Token: 0x04001368 RID: 4968
		private static readonly object EVENT_ISOVERWRITEMODECHANGED = new object();

		// Token: 0x04001369 RID: 4969
		private static readonly object EVENT_MASKCHANGED = new object();

		// Token: 0x0400136A RID: 4970
		private static char systemPwdChar;

		// Token: 0x0400136B RID: 4971
		private const byte imeConvertionNone = 0;

		// Token: 0x0400136C RID: 4972
		private const byte imeConvertionUpdate = 1;

		// Token: 0x0400136D RID: 4973
		private const byte imeConvertionCompleted = 2;

		// Token: 0x0400136E RID: 4974
		private int lastSelLength;

		// Token: 0x0400136F RID: 4975
		private int caretTestPos;

		// Token: 0x04001370 RID: 4976
		private static int IME_ENDING_COMPOSITION = BitVector32.CreateMask();

		// Token: 0x04001371 RID: 4977
		private static int IME_COMPLETING = BitVector32.CreateMask(MaskedTextBox.IME_ENDING_COMPOSITION);

		// Token: 0x04001372 RID: 4978
		private static int HANDLE_KEY_PRESS = BitVector32.CreateMask(MaskedTextBox.IME_COMPLETING);

		// Token: 0x04001373 RID: 4979
		private static int IS_NULL_MASK = BitVector32.CreateMask(MaskedTextBox.HANDLE_KEY_PRESS);

		// Token: 0x04001374 RID: 4980
		private static int QUERY_BASE_TEXT = BitVector32.CreateMask(MaskedTextBox.IS_NULL_MASK);

		// Token: 0x04001375 RID: 4981
		private static int REJECT_INPUT_ON_FIRST_FAILURE = BitVector32.CreateMask(MaskedTextBox.QUERY_BASE_TEXT);

		// Token: 0x04001376 RID: 4982
		private static int HIDE_PROMPT_ON_LEAVE = BitVector32.CreateMask(MaskedTextBox.REJECT_INPUT_ON_FIRST_FAILURE);

		// Token: 0x04001377 RID: 4983
		private static int BEEP_ON_ERROR = BitVector32.CreateMask(MaskedTextBox.HIDE_PROMPT_ON_LEAVE);

		// Token: 0x04001378 RID: 4984
		private static int USE_SYSTEM_PASSWORD_CHAR = BitVector32.CreateMask(MaskedTextBox.BEEP_ON_ERROR);

		// Token: 0x04001379 RID: 4985
		private static int INSERT_TOGGLED = BitVector32.CreateMask(MaskedTextBox.USE_SYSTEM_PASSWORD_CHAR);

		// Token: 0x0400137A RID: 4986
		private static int CUTCOPYINCLUDEPROMPT = BitVector32.CreateMask(MaskedTextBox.INSERT_TOGGLED);

		// Token: 0x0400137B RID: 4987
		private static int CUTCOPYINCLUDELITERALS = BitVector32.CreateMask(MaskedTextBox.CUTCOPYINCLUDEPROMPT);

		// Token: 0x0400137C RID: 4988
		private char passwordChar;

		// Token: 0x0400137D RID: 4989
		private Type validatingType;

		// Token: 0x0400137E RID: 4990
		private IFormatProvider formatProvider;

		// Token: 0x0400137F RID: 4991
		private MaskedTextProvider maskedTextProvider;

		// Token: 0x04001380 RID: 4992
		private InsertKeyMode insertMode;

		// Token: 0x04001381 RID: 4993
		private HorizontalAlignment textAlign;

		// Token: 0x04001382 RID: 4994
		private BitVector32 flagState;
	}
}
