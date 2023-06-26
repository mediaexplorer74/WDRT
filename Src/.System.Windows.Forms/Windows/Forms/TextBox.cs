using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows text box control.</summary>
	// Token: 0x0200039F RID: 927
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Designer("System.Windows.Forms.Design.TextBoxDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionTextBox")]
	public class TextBox : TextBoxBase
	{
		/// <summary>Gets or sets a value indicating whether pressing ENTER in a multiline <see cref="T:System.Windows.Forms.TextBox" /> control creates a new line of text in the control or activates the default button for the form.</summary>
		/// <returns>
		///   <see langword="true" /> if the ENTER key creates a new line of text in a multiline version of the control; <see langword="false" /> if the ENTER key activates the default button for the form. The default is <see langword="false" />.</returns>
		// Token: 0x17000EC9 RID: 3785
		// (get) Token: 0x06003CAC RID: 15532 RVA: 0x00107732 File Offset: 0x00105932
		// (set) Token: 0x06003CAD RID: 15533 RVA: 0x0010773A File Offset: 0x0010593A
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("TextBoxAcceptsReturnDescr")]
		public bool AcceptsReturn
		{
			get
			{
				return this.acceptsReturn;
			}
			set
			{
				this.acceptsReturn = value;
			}
		}

		/// <summary>Gets or sets an option that controls how automatic completion works for the <see cref="T:System.Windows.Forms.TextBox" />.</summary>
		/// <returns>One of the values of <see cref="T:System.Windows.Forms.AutoCompleteMode" />. The following are the values.  
		///  <see cref="F:System.Windows.Forms.AutoCompleteMode.Append" /> Appends the remainder of the most likely candidate string to the existing characters, highlighting the appended characters.  
		///  <see cref="F:System.Windows.Forms.AutoCompleteMode.Suggest" /> Displays the auxiliary drop-down list associated with the edit control. This drop-down is populated with one or more suggested completion strings.  
		///  <see cref="F:System.Windows.Forms.AutoCompleteMode.SuggestAppend" /> Appends both <see langword="Suggest" /> and <see langword="Append" /> options.  
		///  <see cref="F:System.Windows.Forms.AutoCompleteMode.None" /> Disables automatic completion. This is the default.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not one of the values of <see cref="T:System.Windows.Forms.AutoCompleteMode" />.</exception>
		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x06003CAE RID: 15534 RVA: 0x00107743 File Offset: 0x00105943
		// (set) Token: 0x06003CAF RID: 15535 RVA: 0x0010774C File Offset: 0x0010594C
		[DefaultValue(AutoCompleteMode.None)]
		[SRDescription("TextBoxAutoCompleteModeDescr")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public AutoCompleteMode AutoCompleteMode
		{
			get
			{
				return this.autoCompleteMode;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(AutoCompleteMode));
				}
				bool flag = false;
				if (this.autoCompleteMode != AutoCompleteMode.None && value == AutoCompleteMode.None)
				{
					flag = true;
				}
				this.autoCompleteMode = value;
				this.SetAutoComplete(flag);
			}
		}

		/// <summary>Gets or sets a value specifying the source of complete strings used for automatic completion.</summary>
		/// <returns>One of the values of <see cref="T:System.Windows.Forms.AutoCompleteSource" />. The options are <see langword="AllSystemSources" />, <see langword="AllUrl" />, <see langword="FileSystem" />, <see langword="HistoryList" />, <see langword="RecentlyUsedList" />, <see langword="CustomSource" />, and <see langword="None" />. The default is <see langword="None" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not one of the values of <see cref="T:System.Windows.Forms.AutoCompleteSource" />.</exception>
		// Token: 0x17000ECB RID: 3787
		// (get) Token: 0x06003CB0 RID: 15536 RVA: 0x0010779C File Offset: 0x0010599C
		// (set) Token: 0x06003CB1 RID: 15537 RVA: 0x001077A4 File Offset: 0x001059A4
		[DefaultValue(AutoCompleteSource.None)]
		[SRDescription("TextBoxAutoCompleteSourceDescr")]
		[TypeConverter(typeof(TextBoxAutoCompleteSourceConverter))]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public AutoCompleteSource AutoCompleteSource
		{
			get
			{
				return this.autoCompleteSource;
			}
			set
			{
				if (!ClientUtils.IsEnumValid_NotSequential(value, (int)value, new int[] { 128, 7, 6, 64, 1, 32, 2, 256, 4 }))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(AutoCompleteSource));
				}
				if (value == AutoCompleteSource.ListItems)
				{
					throw new NotSupportedException(SR.GetString("TextBoxAutoCompleteSourceNoItems"));
				}
				if (value != AutoCompleteSource.None && value != AutoCompleteSource.CustomSource)
				{
					new FileIOPermission(PermissionState.Unrestricted)
					{
						AllFiles = FileIOPermissionAccess.PathDiscovery
					}.Demand();
				}
				this.autoCompleteSource = value;
				this.SetAutoComplete(false);
			}
		}

		/// <summary>Gets or sets a custom <see cref="T:System.Collections.Specialized.StringCollection" /> to use when the <see cref="P:System.Windows.Forms.TextBox.AutoCompleteSource" /> property is set to <see langword="CustomSource" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> to use with <see cref="P:System.Windows.Forms.TextBox.AutoCompleteSource" />.</returns>
		// Token: 0x17000ECC RID: 3788
		// (get) Token: 0x06003CB2 RID: 15538 RVA: 0x0010782E File Offset: 0x00105A2E
		// (set) Token: 0x06003CB3 RID: 15539 RVA: 0x00107860 File Offset: 0x00105A60
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Localizable(true)]
		[SRDescription("TextBoxAutoCompleteCustomSourceDescr")]
		[Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public AutoCompleteStringCollection AutoCompleteCustomSource
		{
			get
			{
				if (this.autoCompleteCustomSource == null)
				{
					this.autoCompleteCustomSource = new AutoCompleteStringCollection();
					this.autoCompleteCustomSource.CollectionChanged += this.OnAutoCompleteCustomSourceChanged;
				}
				return this.autoCompleteCustomSource;
			}
			set
			{
				if (this.autoCompleteCustomSource != value)
				{
					if (this.autoCompleteCustomSource != null)
					{
						this.autoCompleteCustomSource.CollectionChanged -= this.OnAutoCompleteCustomSourceChanged;
					}
					this.autoCompleteCustomSource = value;
					if (value != null)
					{
						this.autoCompleteCustomSource.CollectionChanged += this.OnAutoCompleteCustomSourceChanged;
					}
					this.SetAutoComplete(false);
				}
			}
		}

		/// <summary>Gets or sets whether the <see cref="T:System.Windows.Forms.TextBox" /> control modifies the case of characters as they are typed.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.CharacterCasing" /> enumeration values that specifies whether the <see cref="T:System.Windows.Forms.TextBox" /> control modifies the case of characters. The default is <see langword="CharacterCasing.Normal" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">A value that is not within the range of valid values for the enumeration was assigned to the property.</exception>
		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x06003CB4 RID: 15540 RVA: 0x001078BD File Offset: 0x00105ABD
		// (set) Token: 0x06003CB5 RID: 15541 RVA: 0x001078C5 File Offset: 0x00105AC5
		[SRCategory("CatBehavior")]
		[DefaultValue(CharacterCasing.Normal)]
		[SRDescription("TextBoxCharacterCasingDescr")]
		public CharacterCasing CharacterCasing
		{
			get
			{
				return this.characterCasing;
			}
			set
			{
				if (this.characterCasing != value)
				{
					if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
					{
						throw new InvalidEnumArgumentException("value", (int)value, typeof(CharacterCasing));
					}
					this.characterCasing = value;
					base.RecreateHandle();
				}
			}
		}

		// Token: 0x06003CB6 RID: 15542 RVA: 0x00107903 File Offset: 0x00105B03
		private bool ContainsNavigationKeyCode(Keys keyCode)
		{
			return keyCode - Keys.Prior <= 7;
		}

		/// <summary>Gets or sets a value indicating whether this is a multiline <see cref="T:System.Windows.Forms.TextBox" /> control.</summary>
		/// <returns>
		///   <see langword="true" /> if the control is a multiline <see cref="T:System.Windows.Forms.TextBox" /> control; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x06003CB7 RID: 15543 RVA: 0x000F3E3F File Offset: 0x000F203F
		// (set) Token: 0x06003CB8 RID: 15544 RVA: 0x0010790F File Offset: 0x00105B0F
		public override bool Multiline
		{
			get
			{
				return base.Multiline;
			}
			set
			{
				if (this.Multiline != value)
				{
					base.Multiline = value;
					if (value && this.AutoCompleteMode != AutoCompleteMode.None)
					{
						base.RecreateHandle();
					}
				}
			}
		}

		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x06003CB9 RID: 15545 RVA: 0x00107932 File Offset: 0x00105B32
		internal override bool PasswordProtect
		{
			get
			{
				return this.PasswordChar > '\0';
			}
		}

		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x06003CBA RID: 15546 RVA: 0x00107940 File Offset: 0x00105B40
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				CharacterCasing characterCasing = this.characterCasing;
				if (characterCasing != CharacterCasing.Upper)
				{
					if (characterCasing == CharacterCasing.Lower)
					{
						createParams.Style |= 16;
					}
				}
				else
				{
					createParams.Style |= 8;
				}
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
				if (this.Multiline)
				{
					if ((this.scrollBars & ScrollBars.Horizontal) == ScrollBars.Horizontal && this.textAlign == HorizontalAlignment.Left && !base.WordWrap)
					{
						createParams.Style |= 1048576;
					}
					if ((this.scrollBars & ScrollBars.Vertical) == ScrollBars.Vertical)
					{
						createParams.Style |= 2097152;
					}
				}
				if (this.useSystemPasswordChar)
				{
					createParams.Style |= 32;
				}
				return createParams;
			}
		}

		/// <summary>Gets or sets the character used to mask characters of a password in a single-line <see cref="T:System.Windows.Forms.TextBox" /> control.</summary>
		/// <returns>The character used to mask characters entered in a single-line <see cref="T:System.Windows.Forms.TextBox" /> control. Set the value of this property to 0 (character value) if you do not want the control to mask characters as they are typed. Equals 0 (character value) by default.</returns>
		// Token: 0x17000ED1 RID: 3793
		// (get) Token: 0x06003CBB RID: 15547 RVA: 0x00107A4D File Offset: 0x00105C4D
		// (set) Token: 0x06003CBC RID: 15548 RVA: 0x00107A70 File Offset: 0x00105C70
		[SRCategory("CatBehavior")]
		[DefaultValue('\0')]
		[Localizable(true)]
		[SRDescription("TextBoxPasswordCharDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public char PasswordChar
		{
			get
			{
				if (!base.IsHandleCreated)
				{
					this.CreateHandle();
				}
				return (char)(int)base.SendMessage(210, 0, 0);
			}
			set
			{
				this.passwordChar = value;
				if (!this.useSystemPasswordChar && base.IsHandleCreated && this.PasswordChar != value)
				{
					base.SendMessage(204, (int)value, 0);
					base.VerifyImeRestrictedModeChanged();
					this.ResetAutoComplete(false);
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets which scroll bars should appear in a multiline <see cref="T:System.Windows.Forms.TextBox" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ScrollBars" /> enumeration values that indicates whether a multiline <see cref="T:System.Windows.Forms.TextBox" /> control appears with no scroll bars, a horizontal scroll bar, a vertical scroll bar, or both. The default is <see langword="ScrollBars.None" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">A value that is not within the range of valid values for the enumeration was assigned to the property.</exception>
		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x06003CBD RID: 15549 RVA: 0x00107ABE File Offset: 0x00105CBE
		// (set) Token: 0x06003CBE RID: 15550 RVA: 0x00107AC6 File Offset: 0x00105CC6
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[DefaultValue(ScrollBars.None)]
		[SRDescription("TextBoxScrollBarsDescr")]
		public ScrollBars ScrollBars
		{
			get
			{
				return this.scrollBars;
			}
			set
			{
				if (this.scrollBars != value)
				{
					if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
					{
						throw new InvalidEnumArgumentException("value", (int)value, typeof(ScrollBars));
					}
					this.scrollBars = value;
					base.RecreateHandle();
				}
			}
		}

		// Token: 0x06003CBF RID: 15551 RVA: 0x00107B04 File Offset: 0x00105D04
		internal override Size GetPreferredSizeCore(Size proposedConstraints)
		{
			Size empty = Size.Empty;
			if (this.Multiline && !base.WordWrap && (this.ScrollBars & ScrollBars.Horizontal) != ScrollBars.None)
			{
				empty.Height += SystemInformation.GetHorizontalScrollBarHeightForDpi(this.deviceDpi);
			}
			if (this.Multiline && (this.ScrollBars & ScrollBars.Vertical) != ScrollBars.None)
			{
				empty.Width += SystemInformation.GetVerticalScrollBarWidthForDpi(this.deviceDpi);
			}
			proposedConstraints -= empty;
			Size preferredSizeCore = base.GetPreferredSizeCore(proposedConstraints);
			return preferredSizeCore + empty;
		}

		/// <summary>Gets or sets the text associated with this control.</summary>
		/// <returns>The text associated with this control.</returns>
		// Token: 0x17000ED3 RID: 3795
		// (get) Token: 0x06003CC0 RID: 15552 RVA: 0x00107B8D File Offset: 0x00105D8D
		// (set) Token: 0x06003CC1 RID: 15553 RVA: 0x00107B95 File Offset: 0x00105D95
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
				this.selectionSet = false;
			}
		}

		/// <summary>Gets or sets how text is aligned in a <see cref="T:System.Windows.Forms.TextBox" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> enumeration values that specifies how text is aligned in the control. The default is <see langword="HorizontalAlignment.Left" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">A value that is not within the range of valid values for the enumeration was assigned to the property.</exception>
		// Token: 0x17000ED4 RID: 3796
		// (get) Token: 0x06003CC2 RID: 15554 RVA: 0x00107BA5 File Offset: 0x00105DA5
		// (set) Token: 0x06003CC3 RID: 15555 RVA: 0x00107BB0 File Offset: 0x00105DB0
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

		/// <summary>Gets or sets a value indicating whether the text in the <see cref="T:System.Windows.Forms.TextBox" /> control should appear as the default password character.</summary>
		/// <returns>
		///   <see langword="true" /> if the text in the <see cref="T:System.Windows.Forms.TextBox" /> control should appear as the default password character; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x06003CC4 RID: 15556 RVA: 0x00107C04 File Offset: 0x00105E04
		// (set) Token: 0x06003CC5 RID: 15557 RVA: 0x00107C0C File Offset: 0x00105E0C
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("TextBoxUseSystemPasswordCharDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public bool UseSystemPasswordChar
		{
			get
			{
				return this.useSystemPasswordChar;
			}
			set
			{
				if (value != this.useSystemPasswordChar)
				{
					this.useSystemPasswordChar = value;
					base.RecreateHandle();
					if (value)
					{
						this.ResetAutoComplete(false);
					}
				}
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TextBox.TextAlign" /> property has changed.</summary>
		// Token: 0x140002E7 RID: 743
		// (add) Token: 0x06003CC6 RID: 15558 RVA: 0x00107C2E File Offset: 0x00105E2E
		// (remove) Token: 0x06003CC7 RID: 15559 RVA: 0x00107C41 File Offset: 0x00105E41
		[SRCategory("CatPropertyChanged")]
		[SRDescription("RadioButtonOnTextAlignChangedDescr")]
		public event EventHandler TextAlignChanged
		{
			add
			{
				base.Events.AddHandler(TextBox.EVENT_TEXTALIGNCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(TextBox.EVENT_TEXTALIGNCHANGED, value);
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.TextBox" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06003CC8 RID: 15560 RVA: 0x00107C54 File Offset: 0x00105E54
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.ResetAutoComplete(true);
				if (this.autoCompleteCustomSource != null)
				{
					this.autoCompleteCustomSource.CollectionChanged -= this.OnAutoCompleteCustomSourceChanged;
				}
				if (this.stringSource != null)
				{
					this.stringSource.ReleaseAutoComplete();
					this.stringSource = null;
				}
			}
			base.Dispose(disposing);
		}

		/// <summary>Determines whether the specified key is an input key or a special key that requires preprocessing.</summary>
		/// <param name="keyData">One of the key's values.</param>
		/// <returns>
		///   <see langword="true" /> if the specified key is an input key; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003CC9 RID: 15561 RVA: 0x00107CAC File Offset: 0x00105EAC
		protected override bool IsInputKey(Keys keyData)
		{
			if (this.Multiline && (keyData & Keys.Alt) == Keys.None)
			{
				Keys keys = keyData & Keys.KeyCode;
				if (keys == Keys.Return)
				{
					return this.acceptsReturn;
				}
			}
			return base.IsInputKey(keyData);
		}

		// Token: 0x06003CCA RID: 15562 RVA: 0x00107CE5 File Offset: 0x00105EE5
		private void OnAutoCompleteCustomSourceChanged(object sender, CollectionChangeEventArgs e)
		{
			if (this.AutoCompleteSource == AutoCompleteSource.CustomSource)
			{
				this.SetAutoComplete(true);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003CCB RID: 15563 RVA: 0x000D47D0 File Offset: 0x000D29D0
		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);
			if (Application.RenderWithVisualStyles && base.IsHandleCreated && base.BorderStyle == BorderStyle.Fixed3D)
			{
				SafeNativeMethods.RedrawWindow(new HandleRef(this, base.Handle), null, NativeMethods.NullHandleRef, 1025);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003CCC RID: 15564 RVA: 0x00107CF8 File Offset: 0x00105EF8
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			if (this.AutoCompleteMode != AutoCompleteMode.None)
			{
				base.RecreateHandle();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003CCD RID: 15565 RVA: 0x00107D0F File Offset: 0x00105F0F
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			if (!this.selectionSet)
			{
				this.selectionSet = true;
				if (this.SelectionLength == 0 && Control.MouseButtons == MouseButtons.None)
				{
					base.SelectAll();
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">The event data.</param>
		// Token: 0x06003CCE RID: 15566 RVA: 0x00107D3C File Offset: 0x00105F3C
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			base.SetSelectionOnHandle();
			if (this.passwordChar != '\0' && !this.useSystemPasswordChar)
			{
				base.SendMessage(204, (int)this.passwordChar, 0);
			}
			base.VerifyImeRestrictedModeChanged();
			if (this.AutoCompleteMode != AutoCompleteMode.None)
			{
				try
				{
					this.fromHandleCreate = true;
					this.SetAutoComplete(false);
				}
				finally
				{
					this.fromHandleCreate = false;
				}
			}
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.OnHandleDestroyed(System.EventArgs)" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003CCF RID: 15567 RVA: 0x00107DB0 File Offset: 0x00105FB0
		protected override void OnHandleDestroyed(EventArgs e)
		{
			if (this.stringSource != null)
			{
				this.stringSource.ReleaseAutoComplete();
				this.stringSource = null;
			}
			base.OnHandleDestroyed(e);
		}

		// Token: 0x06003CD0 RID: 15568 RVA: 0x00107DD3 File Offset: 0x00105FD3
		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
			if (AccessibilityImprovements.Level5 && base.IsHandleCreated && base.IsAccessibilityObjectCreated && this.ContainsNavigationKeyCode(e.KeyCode))
			{
				base.AccessibilityObject.RaiseAutomationEvent(20014);
			}
		}

		// Token: 0x06003CD1 RID: 15569 RVA: 0x00107E12 File Offset: 0x00106012
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (e.Button == MouseButtons.Left && AccessibilityImprovements.Level5 && base.IsHandleCreated && base.IsAccessibilityObjectCreated)
			{
				base.AccessibilityObject.RaiseAutomationEvent(20014);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TextBox.TextAlignChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003CD2 RID: 15570 RVA: 0x00107E50 File Offset: 0x00106050
		protected virtual void OnTextAlignChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[TextBox.EVENT_TEXTALIGNCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Processes a command key.</summary>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference that represents the window message to process.</param>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the shortcut key to process.</param>
		/// <returns>
		///   <see langword="true" /> if the command key was processed by the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003CD3 RID: 15571 RVA: 0x00107E80 File Offset: 0x00106080
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override bool ProcessCmdKey(ref Message m, Keys keyData)
		{
			bool flag = base.ProcessCmdKey(ref m, keyData);
			if (!flag && this.Multiline && !LocalAppContextSwitches.DoNotSupportSelectAllShortcutInMultilineTextBox && this.ShortcutsEnabled && keyData == (Keys)131137)
			{
				base.SelectAll();
				return true;
			}
			return flag;
		}

		/// <summary>Sets the selected text to the specified text without clearing the undo buffer.</summary>
		/// <param name="text">The text to replace.</param>
		// Token: 0x06003CD4 RID: 15572 RVA: 0x00107EC1 File Offset: 0x001060C1
		public void Paste(string text)
		{
			base.SetSelectedTextInternal(text, false);
		}

		// Token: 0x06003CD5 RID: 15573 RVA: 0x00107ECB File Offset: 0x001060CB
		internal override void SelectInternal(int start, int length, int textLen)
		{
			this.selectionSet = true;
			base.SelectInternal(start, length, textLen);
		}

		// Token: 0x06003CD6 RID: 15574 RVA: 0x00107EE0 File Offset: 0x001060E0
		private string[] GetStringsForAutoComplete()
		{
			string[] array = new string[this.AutoCompleteCustomSource.Count];
			for (int i = 0; i < this.AutoCompleteCustomSource.Count; i++)
			{
				array[i] = this.AutoCompleteCustomSource[i];
			}
			return array;
		}

		// Token: 0x06003CD7 RID: 15575 RVA: 0x00107F24 File Offset: 0x00106124
		internal void SetAutoComplete(bool reset)
		{
			if (this.Multiline || this.passwordChar != '\0' || this.useSystemPasswordChar || this.AutoCompleteSource == AutoCompleteSource.None)
			{
				return;
			}
			if (this.AutoCompleteMode != AutoCompleteMode.None)
			{
				if (!this.fromHandleCreate)
				{
					AutoCompleteMode autoCompleteMode = this.AutoCompleteMode;
					this.autoCompleteMode = AutoCompleteMode.None;
					base.RecreateHandle();
					this.autoCompleteMode = autoCompleteMode;
				}
				if (this.AutoCompleteSource == AutoCompleteSource.CustomSource)
				{
					if (!base.IsHandleCreated || this.AutoCompleteCustomSource == null)
					{
						return;
					}
					if (this.AutoCompleteCustomSource.Count == 0)
					{
						this.ResetAutoComplete(true);
						return;
					}
					if (this.stringSource != null)
					{
						this.stringSource.RefreshList(this.GetStringsForAutoComplete());
						return;
					}
					this.stringSource = new StringSource(this.GetStringsForAutoComplete());
					if (!this.stringSource.Bind(new HandleRef(this, base.Handle), (int)this.AutoCompleteMode))
					{
						throw new ArgumentException(SR.GetString("AutoCompleteFailure"));
					}
					return;
				}
				else
				{
					try
					{
						if (base.IsHandleCreated)
						{
							int num = 0;
							if (this.AutoCompleteMode == AutoCompleteMode.Suggest)
							{
								num |= -1879048192;
							}
							if (this.AutoCompleteMode == AutoCompleteMode.Append)
							{
								num |= 1610612736;
							}
							if (this.AutoCompleteMode == AutoCompleteMode.SuggestAppend)
							{
								num |= 268435456;
								num |= 1073741824;
							}
							int num2 = SafeNativeMethods.SHAutoComplete(new HandleRef(this, base.Handle), (int)(this.AutoCompleteSource | (AutoCompleteSource)num));
						}
						return;
					}
					catch (SecurityException)
					{
						return;
					}
				}
			}
			if (reset)
			{
				this.ResetAutoComplete(true);
			}
		}

		// Token: 0x06003CD8 RID: 15576 RVA: 0x0010809C File Offset: 0x0010629C
		private void ResetAutoComplete(bool force)
		{
			if ((this.AutoCompleteMode > AutoCompleteMode.None || force) && base.IsHandleCreated)
			{
				int num = -1610612729;
				SafeNativeMethods.SHAutoComplete(new HandleRef(this, base.Handle), num);
			}
		}

		// Token: 0x06003CD9 RID: 15577 RVA: 0x001080D7 File Offset: 0x001062D7
		private void ResetAutoCompleteCustomSource()
		{
			this.AutoCompleteCustomSource = null;
		}

		// Token: 0x06003CDA RID: 15578 RVA: 0x001080E0 File Offset: 0x001062E0
		private void WmPrint(ref Message m)
		{
			base.WndProc(ref m);
			if ((2 & (int)m.LParam) != 0 && Application.RenderWithVisualStyles && base.BorderStyle == BorderStyle.Fixed3D)
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
		/// <param name="m">A Windows Message object.</param>
		// Token: 0x06003CDB RID: 15579 RVA: 0x001081CC File Offset: 0x001063CC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg != 513)
			{
				if (msg == 514)
				{
					base.WndProc(ref m);
					return;
				}
				if (msg == 791)
				{
					this.WmPrint(ref m);
					return;
				}
				base.WndProc(ref m);
			}
			else
			{
				MouseButtons mouseButtons = Control.MouseButtons;
				bool validationCancelled = base.ValidationCancelled;
				this.FocusInternal();
				if (mouseButtons == Control.MouseButtons && (!base.ValidationCancelled || validationCancelled))
				{
					base.WndProc(ref m);
					return;
				}
			}
		}

		// Token: 0x0400239F RID: 9119
		private static readonly object EVENT_TEXTALIGNCHANGED = new object();

		// Token: 0x040023A0 RID: 9120
		private bool acceptsReturn;

		// Token: 0x040023A1 RID: 9121
		private char passwordChar;

		// Token: 0x040023A2 RID: 9122
		private bool useSystemPasswordChar;

		// Token: 0x040023A3 RID: 9123
		private CharacterCasing characterCasing;

		// Token: 0x040023A4 RID: 9124
		private ScrollBars scrollBars;

		// Token: 0x040023A5 RID: 9125
		private HorizontalAlignment textAlign;

		// Token: 0x040023A6 RID: 9126
		private bool selectionSet;

		// Token: 0x040023A7 RID: 9127
		private AutoCompleteMode autoCompleteMode;

		// Token: 0x040023A8 RID: 9128
		private AutoCompleteSource autoCompleteSource = AutoCompleteSource.None;

		// Token: 0x040023A9 RID: 9129
		private AutoCompleteStringCollection autoCompleteCustomSource;

		// Token: 0x040023AA RID: 9130
		private bool fromHandleCreate;

		// Token: 0x040023AB RID: 9131
		private StringSource stringSource;
	}
}
