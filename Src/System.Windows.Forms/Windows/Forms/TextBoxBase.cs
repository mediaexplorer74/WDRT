using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms.Automation;
using System.Windows.Forms.Internal;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Implements the basic functionality required by text controls.</summary>
	// Token: 0x0200010C RID: 268
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultEvent("TextChanged")]
	[DefaultBindingProperty("Text")]
	[Designer("System.Windows.Forms.Design.TextBoxBaseDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public abstract class TextBoxBase : Control
	{
		// Token: 0x060006A9 RID: 1705 RVA: 0x00012B48 File Offset: 0x00010D48
		internal TextBoxBase()
		{
			base.SetState2(2048, true);
			this.textBoxFlags[TextBoxBase.autoSize | TextBoxBase.hideSelection | TextBoxBase.wordWrap | TextBoxBase.shortcutsEnabled] = true;
			base.SetStyle(ControlStyles.FixedHeight, this.textBoxFlags[TextBoxBase.autoSize]);
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.StandardClick | ControlStyles.StandardDoubleClick | ControlStyles.UseTextForAccessibility, false);
			this.requestedHeight = base.Height;
		}

		/// <summary>Gets or sets a value indicating whether pressing the TAB key in a multiline text box control types a TAB character in the control instead of moving the focus to the next control in the tab order.</summary>
		/// <returns>
		///   <see langword="true" /> if users can enter tabs in a multiline text box using the TAB key; <see langword="false" /> if pressing the TAB key moves the focus. The default is <see langword="false" />.</returns>
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x00012BCC File Offset: 0x00010DCC
		// (set) Token: 0x060006AB RID: 1707 RVA: 0x00012BDE File Offset: 0x00010DDE
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("TextBoxAcceptsTabDescr")]
		public bool AcceptsTab
		{
			get
			{
				return this.textBoxFlags[TextBoxBase.acceptsTab];
			}
			set
			{
				if (this.textBoxFlags[TextBoxBase.acceptsTab] != value)
				{
					this.textBoxFlags[TextBoxBase.acceptsTab] = value;
					this.OnAcceptsTabChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TextBoxBase.AcceptsTab" /> property has changed.</summary>
		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060006AC RID: 1708 RVA: 0x00012C0F File Offset: 0x00010E0F
		// (remove) Token: 0x060006AD RID: 1709 RVA: 0x00012C22 File Offset: 0x00010E22
		[SRCategory("CatPropertyChanged")]
		[SRDescription("TextBoxBaseOnAcceptsTabChangedDescr")]
		public event EventHandler AcceptsTabChanged
		{
			add
			{
				base.Events.AddHandler(TextBoxBase.EVENT_ACCEPTSTABCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(TextBoxBase.EVENT_ACCEPTSTABCHANGED, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the defined shortcuts are enabled.</summary>
		/// <returns>
		///   <see langword="true" /> to enable the shortcuts; otherwise, <see langword="false" />.</returns>
		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060006AE RID: 1710 RVA: 0x00012C35 File Offset: 0x00010E35
		// (set) Token: 0x060006AF RID: 1711 RVA: 0x00012C47 File Offset: 0x00010E47
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("TextBoxShortcutsEnabledDescr")]
		public virtual bool ShortcutsEnabled
		{
			get
			{
				return this.textBoxFlags[TextBoxBase.shortcutsEnabled];
			}
			set
			{
				if (TextBoxBase.shortcutsToDisable == null)
				{
					TextBoxBase.shortcutsToDisable = new int[]
					{
						131162, 131139, 131160, 131158, 131137, 131148, 131154, 131141, 131161, 131080,
						131118, 65582, 65581, 131146
					};
				}
				this.textBoxFlags[TextBoxBase.shortcutsEnabled] = value;
			}
		}

		/// <summary>Processes a command key.</summary>
		/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference that represents the window message to process.</param>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the shortcut key to process.</param>
		/// <returns>
		///   <see langword="true" /> if the command key was processed by the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x060006B0 RID: 1712 RVA: 0x00012C78 File Offset: 0x00010E78
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			bool flag = base.ProcessCmdKey(ref msg, keyData);
			if (!this.ShortcutsEnabled)
			{
				foreach (int num in TextBoxBase.shortcutsToDisable)
				{
					if (keyData == (Keys)num || keyData == (Keys)(num | 65536))
					{
						return true;
					}
				}
			}
			return (this.textBoxFlags[TextBoxBase.readOnly] && (keyData == (Keys)131148 || keyData == (Keys)131154 || keyData == (Keys)131141 || keyData == (Keys)131146)) || flag;
		}

		/// <summary>Gets or sets a value indicating whether the height of the control automatically adjusts when the font assigned to the control is changed.</summary>
		/// <returns>
		///   <see langword="true" /> if the height of the control automatically adjusts when the font is changed; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x00012CFA File Offset: 0x00010EFA
		// (set) Token: 0x060006B2 RID: 1714 RVA: 0x00012D0C File Offset: 0x00010F0C
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[Localizable(true)]
		[SRDescription("TextBoxAutoSizeDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool AutoSize
		{
			get
			{
				return this.textBoxFlags[TextBoxBase.autoSize];
			}
			set
			{
				if (this.textBoxFlags[TextBoxBase.autoSize] != value)
				{
					this.textBoxFlags[TextBoxBase.autoSize] = value;
					if (!this.Multiline)
					{
						base.SetStyle(ControlStyles.FixedHeight, value);
						this.AdjustHeight(false);
					}
					this.OnAutoSizeChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the background color of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background of the control.</returns>
		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x00012D60 File Offset: 0x00010F60
		// (set) Token: 0x060006B4 RID: 1716 RVA: 0x00012D84 File Offset: 0x00010F84
		[SRCategory("CatAppearance")]
		[DispId(-501)]
		[SRDescription("ControlBackColorDescr")]
		public override Color BackColor
		{
			get
			{
				if (this.ShouldSerializeBackColor())
				{
					return base.BackColor;
				}
				if (this.ReadOnly)
				{
					return SystemColors.Control;
				}
				return SystemColors.Window;
			}
			set
			{
				base.BackColor = value;
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>The background image for the object.</returns>
		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x0001187C File Offset: 0x0000FA7C
		// (set) Token: 0x060006B6 RID: 1718 RVA: 0x00011884 File Offset: 0x0000FA84
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

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060006B7 RID: 1719 RVA: 0x00011842 File Offset: 0x0000FA42
		// (remove) Token: 0x060006B8 RID: 1720 RVA: 0x0001184B File Offset: 0x0000FA4B
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler AutoSizeChanged
		{
			add
			{
				base.AutoSizeChanged += value;
			}
			remove
			{
				base.AutoSizeChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TextBoxBase.BackgroundImage" /> property changes. This event is not relevant for this class.</summary>
		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060006B9 RID: 1721 RVA: 0x0001188D File Offset: 0x0000FA8D
		// (remove) Token: 0x060006BA RID: 1722 RVA: 0x00011896 File Offset: 0x0000FA96
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

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImageLayout" /> values.</returns>
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x0001189F File Offset: 0x0000FA9F
		// (set) Token: 0x060006BC RID: 1724 RVA: 0x000118A7 File Offset: 0x0000FAA7
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TextBoxBase.BackgroundImageLayout" /> property changes. This event is not relevant for this class.</summary>
		// Token: 0x14000014 RID: 20
		// (add) Token: 0x060006BD RID: 1725 RVA: 0x000118B0 File Offset: 0x0000FAB0
		// (remove) Token: 0x060006BE RID: 1726 RVA: 0x000118B9 File Offset: 0x0000FAB9
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

		/// <summary>Gets or sets the border type of the text box control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.BorderStyle" /> that represents the border type of the text box control. The default is <see langword="Fixed3D" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">A value that is not within the range of valid values for the enumeration was assigned to the property.</exception>
		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x00012D8D File Offset: 0x00010F8D
		// (set) Token: 0x060006C0 RID: 1728 RVA: 0x00012D98 File Offset: 0x00010F98
		[SRCategory("CatAppearance")]
		[DefaultValue(BorderStyle.Fixed3D)]
		[DispId(-504)]
		[SRDescription("TextBoxBorderDescr")]
		public BorderStyle BorderStyle
		{
			get
			{
				return this.borderStyle;
			}
			set
			{
				if (this.borderStyle != value)
				{
					if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
					{
						throw new InvalidEnumArgumentException("value", (int)value, typeof(BorderStyle));
					}
					this.borderStyle = value;
					base.UpdateStyles();
					base.RecreateHandle();
					using (LayoutTransaction.CreateTransactionIf(this.AutoSize, this.ParentInternal, this, PropertyNames.BorderStyle))
					{
						this.OnBorderStyleChanged(EventArgs.Empty);
					}
				}
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TextBoxBase.BorderStyle" /> property has changed.</summary>
		// Token: 0x14000015 RID: 21
		// (add) Token: 0x060006C1 RID: 1729 RVA: 0x00012E28 File Offset: 0x00011028
		// (remove) Token: 0x060006C2 RID: 1730 RVA: 0x00012E3B File Offset: 0x0001103B
		[SRCategory("CatPropertyChanged")]
		[SRDescription("TextBoxBaseOnBorderStyleChangedDescr")]
		public event EventHandler BorderStyleChanged
		{
			add
			{
				base.Events.AddHandler(TextBoxBase.EVENT_BORDERSTYLECHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(TextBoxBase.EVENT_BORDERSTYLECHANGED, value);
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x00012E4E File Offset: 0x0001104E
		internal virtual bool CanRaiseTextChangedEvent
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="P:System.Windows.Forms.Control.ImeMode" /> property can be set to an active value, to enable IME support.</summary>
		/// <returns>
		///   <see langword="false" /> if the <see cref="P:System.Windows.Forms.TextBoxBase.ReadOnly" /> property is <see langword="true" /> or if this <see cref="T:System.Windows.Forms.TextBoxBase" /> class is set to use a password mask character; otherwise, <see langword="true" />.</returns>
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x00012E54 File Offset: 0x00011054
		protected override bool CanEnableIme
		{
			get
			{
				return !this.ReadOnly && !this.PasswordProtect && base.CanEnableIme;
			}
		}

		/// <summary>Gets a value indicating whether the user can undo the previous operation in a text box control.</summary>
		/// <returns>
		///   <see langword="true" /> if the user can undo the previous operation performed in a text box control; otherwise, <see langword="false" />.</returns>
		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x00012E7C File Offset: 0x0001107C
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TextBoxCanUndoDescr")]
		public bool CanUndo
		{
			get
			{
				return base.IsHandleCreated && (int)(long)base.SendMessage(198, 0, 0) != 0;
			}
		}

		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x00012EAC File Offset: 0x000110AC
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ClassName = "EDIT";
				createParams.Style |= 192;
				if (!this.textBoxFlags[TextBoxBase.hideSelection])
				{
					createParams.Style |= 256;
				}
				if (this.textBoxFlags[TextBoxBase.readOnly])
				{
					createParams.Style |= 2048;
				}
				createParams.ExStyle &= -513;
				createParams.Style &= -8388609;
				BorderStyle borderStyle = this.borderStyle;
				if (borderStyle != BorderStyle.FixedSingle)
				{
					if (borderStyle == BorderStyle.Fixed3D)
					{
						createParams.ExStyle |= 512;
					}
				}
				else
				{
					createParams.Style |= 8388608;
				}
				if (this.textBoxFlags[TextBoxBase.multiline])
				{
					createParams.Style |= 4;
					if (this.textBoxFlags[TextBoxBase.wordWrap])
					{
						createParams.Style &= -129;
					}
				}
				return createParams;
			}
		}

		/// <summary>Gets or sets a value indicating whether control drawing is done in a buffer before the control is displayed. This property is not relevant for this class.</summary>
		/// <returns>
		///   <see langword="true" /> to implement double buffering on the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x00012FC3 File Offset: 0x000111C3
		// (set) Token: 0x060006C8 RID: 1736 RVA: 0x00012FCB File Offset: 0x000111CB
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override bool DoubleBuffered
		{
			get
			{
				return base.DoubleBuffered;
			}
			set
			{
				base.DoubleBuffered = value;
			}
		}

		/// <summary>Occurs when the text box is clicked.</summary>
		// Token: 0x14000016 RID: 22
		// (add) Token: 0x060006C9 RID: 1737 RVA: 0x00012FD4 File Offset: 0x000111D4
		// (remove) Token: 0x060006CA RID: 1738 RVA: 0x00012FDD File Offset: 0x000111DD
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public new event EventHandler Click
		{
			add
			{
				base.Click += value;
			}
			remove
			{
				base.Click -= value;
			}
		}

		/// <summary>Occurs when the control is clicked by the mouse.</summary>
		// Token: 0x14000017 RID: 23
		// (add) Token: 0x060006CB RID: 1739 RVA: 0x00012FE6 File Offset: 0x000111E6
		// (remove) Token: 0x060006CC RID: 1740 RVA: 0x00012FEF File Offset: 0x000111EF
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public new event MouseEventHandler MouseClick
		{
			add
			{
				base.MouseClick += value;
			}
			remove
			{
				base.MouseClick -= value;
			}
		}

		/// <summary>Gets or sets the default cursor for the control.</summary>
		/// <returns>An object of type <see cref="T:System.Windows.Forms.Cursor" /> representing the current default cursor.</returns>
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x00012FF8 File Offset: 0x000111F8
		protected override Cursor DefaultCursor
		{
			get
			{
				return Cursors.IBeam;
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x00012FFF File Offset: 0x000111FF
		protected override Size DefaultSize
		{
			get
			{
				return new Size(100, this.PreferredHeight);
			}
		}

		/// <summary>Gets or sets the foreground color of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the control's foreground color.</returns>
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x0001300E File Offset: 0x0001120E
		// (set) Token: 0x060006D0 RID: 1744 RVA: 0x00013024 File Offset: 0x00011224
		[SRCategory("CatAppearance")]
		[DispId(-513)]
		[SRDescription("ControlForeColorDescr")]
		public override Color ForeColor
		{
			get
			{
				if (this.ShouldSerializeForeColor())
				{
					return base.ForeColor;
				}
				return SystemColors.WindowText;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the selected text in the text box control remains highlighted when the control loses focus.</summary>
		/// <returns>
		///   <see langword="true" /> if the selected text does not appear highlighted when the text box control loses focus; <see langword="false" />, if the selected text remains highlighted when the text box control loses focus. The default is <see langword="true" />.</returns>
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x0001302D File Offset: 0x0001122D
		// (set) Token: 0x060006D2 RID: 1746 RVA: 0x0001303F File Offset: 0x0001123F
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("TextBoxHideSelectionDescr")]
		public bool HideSelection
		{
			get
			{
				return this.textBoxFlags[TextBoxBase.hideSelection];
			}
			set
			{
				if (this.textBoxFlags[TextBoxBase.hideSelection] != value)
				{
					this.textBoxFlags[TextBoxBase.hideSelection] = value;
					base.RecreateHandle();
					this.OnHideSelectionChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TextBoxBase.HideSelection" /> property has changed.</summary>
		// Token: 0x14000018 RID: 24
		// (add) Token: 0x060006D3 RID: 1747 RVA: 0x00013076 File Offset: 0x00011276
		// (remove) Token: 0x060006D4 RID: 1748 RVA: 0x00013089 File Offset: 0x00011289
		[SRCategory("CatPropertyChanged")]
		[SRDescription("TextBoxBaseOnHideSelectionChangedDescr")]
		public event EventHandler HideSelectionChanged
		{
			add
			{
				base.Events.AddHandler(TextBoxBase.EVENT_HIDESELECTIONCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(TextBoxBase.EVENT_HIDESELECTIONCHANGED, value);
			}
		}

		/// <summary>Gets or sets the Input Method Editor (IME) mode of a control.</summary>
		/// <returns>The IME mode of the control.</returns>
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x0001309C File Offset: 0x0001129C
		// (set) Token: 0x060006D6 RID: 1750 RVA: 0x000130CB File Offset: 0x000112CB
		protected override ImeMode ImeModeBase
		{
			get
			{
				if (base.DesignMode)
				{
					return base.ImeModeBase;
				}
				return this.CanEnableIme ? base.ImeModeBase : ImeMode.Disable;
			}
			set
			{
				base.ImeModeBase = value;
			}
		}

		/// <summary>Gets or sets the lines of text in a text box control.</summary>
		/// <returns>An array of strings that contains the text in a text box control.</returns>
		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x000130D4 File Offset: 0x000112D4
		// (set) Token: 0x060006D8 RID: 1752 RVA: 0x000131B8 File Offset: 0x000113B8
		[SRCategory("CatAppearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MergableProperty(false)]
		[Localizable(true)]
		[SRDescription("TextBoxLinesDescr")]
		[Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string[] Lines
		{
			get
			{
				string text = this.Text;
				ArrayList arrayList = new ArrayList();
				int j;
				for (int i = 0; i < text.Length; i = j)
				{
					for (j = i; j < text.Length; j++)
					{
						char c = text[j];
						if (c == '\r' || c == '\n')
						{
							break;
						}
					}
					string text2 = text.Substring(i, j - i);
					arrayList.Add(text2);
					if (j < text.Length && text[j] == '\r')
					{
						j++;
					}
					if (j < text.Length && text[j] == '\n')
					{
						j++;
					}
				}
				if (text.Length > 0 && (text[text.Length - 1] == '\r' || text[text.Length - 1] == '\n'))
				{
					arrayList.Add("");
				}
				return (string[])arrayList.ToArray(typeof(string));
			}
			set
			{
				if (value != null && value.Length != 0)
				{
					StringBuilder stringBuilder = new StringBuilder(value[0]);
					for (int i = 1; i < value.Length; i++)
					{
						stringBuilder.Append("\r\n");
						stringBuilder.Append(value[i]);
					}
					this.Text = stringBuilder.ToString();
					return;
				}
				this.Text = "";
			}
		}

		/// <summary>Gets or sets the maximum number of characters the user can type or paste into the text box control.</summary>
		/// <returns>The number of characters that can be entered into the control. The default is 32767.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value assigned to the property is less than 0.</exception>
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x00013211 File Offset: 0x00011411
		// (set) Token: 0x060006DA RID: 1754 RVA: 0x0001321C File Offset: 0x0001141C
		[SRCategory("CatBehavior")]
		[DefaultValue(32767)]
		[Localizable(true)]
		[SRDescription("TextBoxMaxLengthDescr")]
		public virtual int MaxLength
		{
			get
			{
				return this.maxLength;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("MaxLength", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"MaxLength",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this.maxLength != value)
				{
					this.maxLength = value;
					this.UpdateMaxLength();
				}
			}
		}

		/// <summary>Gets or sets a value that indicates that the text box control has been modified by the user since the control was created or its contents were last set.</summary>
		/// <returns>
		///   <see langword="true" /> if the control's contents have been modified; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x00013288 File Offset: 0x00011488
		// (set) Token: 0x060006DC RID: 1756 RVA: 0x000132F8 File Offset: 0x000114F8
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TextBoxModifiedDescr")]
		public bool Modified
		{
			get
			{
				if (base.IsHandleCreated)
				{
					bool flag = (int)(long)base.SendMessage(184, 0, 0) != 0;
					if (this.textBoxFlags[TextBoxBase.modified] != flag)
					{
						this.textBoxFlags[TextBoxBase.modified] = flag;
						this.OnModifiedChanged(EventArgs.Empty);
					}
					return flag;
				}
				return this.textBoxFlags[TextBoxBase.modified];
			}
			set
			{
				if (this.Modified != value)
				{
					if (base.IsHandleCreated)
					{
						base.SendMessage(185, value ? 1 : 0, 0);
					}
					this.textBoxFlags[TextBoxBase.modified] = value;
					this.OnModifiedChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TextBoxBase.Modified" /> property has changed.</summary>
		// Token: 0x14000019 RID: 25
		// (add) Token: 0x060006DD RID: 1757 RVA: 0x00013346 File Offset: 0x00011546
		// (remove) Token: 0x060006DE RID: 1758 RVA: 0x00013359 File Offset: 0x00011559
		[SRCategory("CatPropertyChanged")]
		[SRDescription("TextBoxBaseOnModifiedChangedDescr")]
		public event EventHandler ModifiedChanged
		{
			add
			{
				base.Events.AddHandler(TextBoxBase.EVENT_MODIFIEDCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(TextBoxBase.EVENT_MODIFIEDCHANGED, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether this is a multiline text box control.</summary>
		/// <returns>
		///   <see langword="true" /> if the control is a multiline text box control; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x0001336C File Offset: 0x0001156C
		// (set) Token: 0x060006E0 RID: 1760 RVA: 0x00013380 File Offset: 0x00011580
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[Localizable(true)]
		[SRDescription("TextBoxMultilineDescr")]
		[RefreshProperties(RefreshProperties.All)]
		public virtual bool Multiline
		{
			get
			{
				return this.textBoxFlags[TextBoxBase.multiline];
			}
			set
			{
				if (this.textBoxFlags[TextBoxBase.multiline] != value)
				{
					using (LayoutTransaction.CreateTransactionIf(this.AutoSize, this.ParentInternal, this, PropertyNames.Multiline))
					{
						this.textBoxFlags[TextBoxBase.multiline] = value;
						if (value)
						{
							base.SetStyle(ControlStyles.FixedHeight, false);
						}
						else
						{
							base.SetStyle(ControlStyles.FixedHeight, this.AutoSize);
						}
						base.RecreateHandle();
						this.AdjustHeight(false);
						this.OnMultilineChanged(EventArgs.Empty);
					}
				}
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TextBoxBase.Multiline" /> property has changed.</summary>
		// Token: 0x1400001A RID: 26
		// (add) Token: 0x060006E1 RID: 1761 RVA: 0x0001341C File Offset: 0x0001161C
		// (remove) Token: 0x060006E2 RID: 1762 RVA: 0x0001342F File Offset: 0x0001162F
		[SRCategory("CatPropertyChanged")]
		[SRDescription("TextBoxBaseOnMultilineChangedDescr")]
		public event EventHandler MultilineChanged
		{
			add
			{
				base.Events.AddHandler(TextBoxBase.EVENT_MULTILINECHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(TextBoxBase.EVENT_MULTILINECHANGED, value);
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value.</returns>
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x00013442 File Offset: 0x00011642
		// (set) Token: 0x060006E4 RID: 1764 RVA: 0x0001344A File Offset: 0x0001164A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Padding Padding
		{
			get
			{
				return base.Padding;
			}
			set
			{
				base.Padding = value;
			}
		}

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x1400001B RID: 27
		// (add) Token: 0x060006E5 RID: 1765 RVA: 0x00013453 File Offset: 0x00011653
		// (remove) Token: 0x060006E6 RID: 1766 RVA: 0x0001345C File Offset: 0x0001165C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRCategory("CatLayout")]
		[SRDescription("ControlOnPaddingChangedDescr")]
		public new event EventHandler PaddingChanged
		{
			add
			{
				base.PaddingChanged += value;
			}
			remove
			{
				base.PaddingChanged -= value;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x0001180C File Offset: 0x0000FA0C
		internal virtual bool PasswordProtect
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the preferred height for a text box.</summary>
		/// <returns>The preferred height of a text box.</returns>
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x00013468 File Offset: 0x00011668
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TextBoxPreferredHeightDescr")]
		public int PreferredHeight
		{
			get
			{
				int num = base.FontHeight;
				if (this.borderStyle != BorderStyle.None)
				{
					num += SystemInformation.GetBorderSizeForDpi(this.deviceDpi).Height * 4 + 3;
				}
				return num;
			}
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x000134A0 File Offset: 0x000116A0
		internal override Size GetPreferredSizeCore(Size proposedConstraints)
		{
			Size size = this.SizeFromClientSize(Size.Empty) + this.Padding.Size;
			if (this.BorderStyle != BorderStyle.None)
			{
				size += new Size(0, 3);
			}
			if (this.BorderStyle == BorderStyle.FixedSingle)
			{
				size.Width += 2;
				size.Height += 2;
			}
			proposedConstraints -= size;
			TextFormatFlags textFormatFlags = TextFormatFlags.NoPrefix;
			if (!this.Multiline)
			{
				textFormatFlags |= TextFormatFlags.SingleLine;
			}
			else if (this.WordWrap)
			{
				textFormatFlags |= TextFormatFlags.WordBreak;
			}
			Size size2 = TextRenderer.MeasureText(this.Text, this.Font, proposedConstraints, textFormatFlags);
			size2.Height = Math.Max(size2.Height, base.FontHeight);
			return size2 + size;
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0001356C File Offset: 0x0001176C
		internal void GetSelectionStartAndLength(out int start, out int length)
		{
			int num = 0;
			if (!base.IsHandleCreated)
			{
				this.AdjustSelectionStartAndEnd(this.selectionStart, this.selectionLength, out start, out num, -1);
				length = num - start;
				return;
			}
			start = 0;
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 176, ref start, ref num);
			start = Math.Max(0, start);
			num = Math.Max(0, num);
			if (this.SelectionUsesDbcsOffsetsInWin9x && Marshal.SystemDefaultCharSize == 1)
			{
				TextBoxBase.ToUnicodeOffsets(this.WindowText, ref start, ref num);
			}
			length = num - start;
		}

		/// <summary>Gets or sets a value indicating whether text in the text box is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the text box is read-only; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x000135F3 File Offset: 0x000117F3
		// (set) Token: 0x060006EC RID: 1772 RVA: 0x00013608 File Offset: 0x00011808
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRDescription("TextBoxReadOnlyDescr")]
		public bool ReadOnly
		{
			get
			{
				return this.textBoxFlags[TextBoxBase.readOnly];
			}
			set
			{
				if (this.textBoxFlags[TextBoxBase.readOnly] != value)
				{
					this.textBoxFlags[TextBoxBase.readOnly] = value;
					if (base.IsHandleCreated)
					{
						base.SendMessage(207, value ? (-1) : 0, 0);
					}
					this.OnReadOnlyChanged(EventArgs.Empty);
					base.VerifyImeRestrictedModeChanged();
				}
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TextBoxBase.ReadOnly" /> property has changed.</summary>
		// Token: 0x1400001C RID: 28
		// (add) Token: 0x060006ED RID: 1773 RVA: 0x00013666 File Offset: 0x00011866
		// (remove) Token: 0x060006EE RID: 1774 RVA: 0x00013679 File Offset: 0x00011879
		[SRCategory("CatPropertyChanged")]
		[SRDescription("TextBoxBaseOnReadOnlyChangedDescr")]
		public event EventHandler ReadOnlyChanged
		{
			add
			{
				base.Events.AddHandler(TextBoxBase.EVENT_READONLYCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(TextBoxBase.EVENT_READONLYCHANGED, value);
			}
		}

		/// <summary>Gets or sets a value indicating the currently selected text in the control.</summary>
		/// <returns>A string that represents the currently selected text in the text box.</returns>
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x0001368C File Offset: 0x0001188C
		// (set) Token: 0x060006F0 RID: 1776 RVA: 0x000136B0 File Offset: 0x000118B0
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TextBoxSelectedTextDescr")]
		public virtual string SelectedText
		{
			get
			{
				int num;
				int num2;
				this.GetSelectionStartAndLength(out num, out num2);
				return this.Text.Substring(num, num2);
			}
			set
			{
				this.SetSelectedTextInternal(value, true);
			}
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x000136BC File Offset: 0x000118BC
		internal virtual void SetSelectedTextInternal(string text, bool clearUndo)
		{
			if (!base.IsHandleCreated)
			{
				this.CreateHandle();
			}
			if (text == null)
			{
				text = "";
			}
			base.SendMessage(197, 0, 0);
			if (clearUndo)
			{
				base.SendMessage(194, 0, text);
				base.SendMessage(185, 0, 0);
				this.ClearUndo();
			}
			else
			{
				base.SendMessage(194, -1, text);
			}
			base.SendMessage(197, this.maxLength, 0);
		}

		/// <summary>Gets or sets the number of characters selected in the text box.</summary>
		/// <returns>The number of characters selected in the text box.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is less than zero.</exception>
		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x00013738 File Offset: 0x00011938
		// (set) Token: 0x060006F3 RID: 1779 RVA: 0x00013750 File Offset: 0x00011950
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TextBoxSelectionLengthDescr")]
		public virtual int SelectionLength
		{
			get
			{
				int num;
				int num2;
				this.GetSelectionStartAndLength(out num, out num2);
				return num2;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("SelectionLength", SR.GetString("InvalidArgument", new object[]
					{
						"SelectionLength",
						value.ToString(CultureInfo.CurrentCulture)
					}));
				}
				int num;
				int num2;
				this.GetSelectionStartAndLength(out num, out num2);
				if (value != num2)
				{
					this.Select(num, value);
				}
			}
		}

		/// <summary>Gets or sets the starting point of text selected in the text box.</summary>
		/// <returns>The starting position of text selected in the text box.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is less than zero.</exception>
		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x000137AC File Offset: 0x000119AC
		// (set) Token: 0x060006F5 RID: 1781 RVA: 0x000137C4 File Offset: 0x000119C4
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TextBoxSelectionStartDescr")]
		public int SelectionStart
		{
			get
			{
				int num;
				int num2;
				this.GetSelectionStartAndLength(out num, out num2);
				return num;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("SelectionStart", SR.GetString("InvalidArgument", new object[]
					{
						"SelectionStart",
						value.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.Select(value, this.SelectionLength);
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x00012E4E File Offset: 0x0001104E
		internal virtual bool SetSelectionInCreateHandle
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets or sets the current text in the text box.</summary>
		/// <returns>The text displayed in the control.</returns>
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x00013814 File Offset: 0x00011A14
		// (set) Token: 0x060006F8 RID: 1784 RVA: 0x0001381C File Offset: 0x00011A1C
		[Localizable(true)]
		[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				if (value != base.Text)
				{
					base.Text = value;
					if (base.IsHandleCreated)
					{
						base.SendMessage(185, 0, 0);
					}
				}
			}
		}

		/// <summary>Gets the length of text in the control.</summary>
		/// <returns>The number of characters contained in the text of the control.</returns>
		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x00013849 File Offset: 0x00011A49
		[Browsable(false)]
		public virtual int TextLength
		{
			get
			{
				if (base.IsHandleCreated && Marshal.SystemDefaultCharSize == 2)
				{
					return SafeNativeMethods.GetWindowTextLength(new HandleRef(this, base.Handle));
				}
				return this.Text.Length;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x00012E4E File Offset: 0x0001104E
		internal virtual bool SelectionUsesDbcsOffsetsInWin9x
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x00013878 File Offset: 0x00011A78
		// (set) Token: 0x060006FC RID: 1788 RVA: 0x00013880 File Offset: 0x00011A80
		internal override string WindowText
		{
			get
			{
				return base.WindowText;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (!this.WindowText.Equals(value))
				{
					this.textBoxFlags[TextBoxBase.codeUpdateText] = true;
					try
					{
						base.WindowText = value;
					}
					finally
					{
						this.textBoxFlags[TextBoxBase.codeUpdateText] = false;
					}
				}
			}
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x000138E4 File Offset: 0x00011AE4
		internal void ForceWindowText(string value)
		{
			if (value == null)
			{
				value = "";
			}
			this.textBoxFlags[TextBoxBase.codeUpdateText] = true;
			try
			{
				if (base.IsHandleCreated)
				{
					UnsafeNativeMethods.SetWindowText(new HandleRef(this, base.Handle), value);
				}
				else if (value.Length == 0)
				{
					this.Text = null;
				}
				else
				{
					this.Text = value;
				}
			}
			finally
			{
				this.textBoxFlags[TextBoxBase.codeUpdateText] = false;
			}
		}

		/// <summary>Indicates whether a multiline text box control automatically wraps words to the beginning of the next line when necessary.</summary>
		/// <returns>
		///   <see langword="true" /> if the multiline text box control wraps words; <see langword="false" /> if the text box control automatically scrolls horizontally when the user types past the right edge of the control. The default is <see langword="true" />.</returns>
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x00013968 File Offset: 0x00011B68
		// (set) Token: 0x060006FF RID: 1791 RVA: 0x0001397C File Offset: 0x00011B7C
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[DefaultValue(true)]
		[SRDescription("TextBoxWordWrapDescr")]
		public bool WordWrap
		{
			get
			{
				return this.textBoxFlags[TextBoxBase.wordWrap];
			}
			set
			{
				using (LayoutTransaction.CreateTransactionIf(this.AutoSize, this.ParentInternal, this, PropertyNames.WordWrap))
				{
					if (this.textBoxFlags[TextBoxBase.wordWrap] != value)
					{
						this.textBoxFlags[TextBoxBase.wordWrap] = value;
						base.RecreateHandle();
					}
				}
			}
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x000139E8 File Offset: 0x00011BE8
		private void AdjustHeight(bool returnIfAnchored)
		{
			if (returnIfAnchored && (this.Anchor & (AnchorStyles.Top | AnchorStyles.Bottom)) == (AnchorStyles.Top | AnchorStyles.Bottom))
			{
				return;
			}
			int num = this.requestedHeight;
			try
			{
				if (this.textBoxFlags[TextBoxBase.autoSize] && !this.textBoxFlags[TextBoxBase.multiline])
				{
					base.Height = this.PreferredHeight;
				}
				else
				{
					int height = base.Height;
					if (this.textBoxFlags[TextBoxBase.multiline])
					{
						base.Height = Math.Max(num, this.PreferredHeight + 2);
					}
					this.integralHeightAdjust = true;
					try
					{
						base.Height = num;
					}
					finally
					{
						this.integralHeightAdjust = false;
					}
				}
			}
			finally
			{
				this.requestedHeight = num;
			}
		}

		/// <summary>Appends text to the current text of a text box.</summary>
		/// <param name="text">The text to append to the current contents of the text box.</param>
		// Token: 0x06000701 RID: 1793 RVA: 0x00013AA8 File Offset: 0x00011CA8
		public void AppendText(string text)
		{
			if (text.Length > 0)
			{
				int num;
				int num2;
				this.GetSelectionStartAndLength(out num, out num2);
				try
				{
					int endPosition = this.GetEndPosition();
					this.SelectInternal(endPosition, endPosition, endPosition);
					this.SelectedText = text;
				}
				finally
				{
					if (base.Width == 0 || base.Height == 0)
					{
						this.Select(num, num2);
					}
				}
			}
		}

		/// <summary>Clears all text from the text box control.</summary>
		// Token: 0x06000702 RID: 1794 RVA: 0x00013B0C File Offset: 0x00011D0C
		public void Clear()
		{
			this.Text = null;
		}

		/// <summary>Clears information about the most recent operation from the undo buffer of the text box.</summary>
		// Token: 0x06000703 RID: 1795 RVA: 0x00013B15 File Offset: 0x00011D15
		public void ClearUndo()
		{
			if (base.IsHandleCreated)
			{
				base.SendMessage(205, 0, 0);
			}
		}

		/// <summary>Copies the current selection in the text box to the Clipboard.</summary>
		// Token: 0x06000704 RID: 1796 RVA: 0x00013B2D File Offset: 0x00011D2D
		[UIPermission(SecurityAction.Demand, Clipboard = UIPermissionClipboard.OwnClipboard)]
		public void Copy()
		{
			base.SendMessage(769, 0, 0);
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00013B3D File Offset: 0x00011D3D
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (!AccessibilityImprovements.Level5)
			{
				return base.CreateAccessibilityInstance();
			}
			return new TextBoxBase.TextBoxBaseAccessibleObject(this);
		}

		/// <summary>Creates a handle for the control.</summary>
		// Token: 0x06000706 RID: 1798 RVA: 0x00013B54 File Offset: 0x00011D54
		protected override void CreateHandle()
		{
			this.textBoxFlags[TextBoxBase.creatingHandle] = true;
			try
			{
				base.CreateHandle();
				if (this.SetSelectionInCreateHandle)
				{
					this.SetSelectionOnHandle();
				}
			}
			finally
			{
				this.textBoxFlags[TextBoxBase.creatingHandle] = false;
			}
		}

		/// <summary>Moves the current selection in the text box to the Clipboard.</summary>
		// Token: 0x06000707 RID: 1799 RVA: 0x00013BAC File Offset: 0x00011DAC
		public void Cut()
		{
			base.SendMessage(768, 0, 0);
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00013BBC File Offset: 0x00011DBC
		internal virtual int GetEndPosition()
		{
			if (!base.IsHandleCreated)
			{
				return this.TextLength;
			}
			return this.TextLength + 1;
		}

		/// <summary>Determines whether the specified key is an input key or a special key that requires preprocessing.</summary>
		/// <param name="keyData">One of the Keys value.</param>
		/// <returns>
		///   <see langword="true" /> if the specified key is an input key; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000709 RID: 1801 RVA: 0x00013BD8 File Offset: 0x00011DD8
		protected override bool IsInputKey(Keys keyData)
		{
			if ((keyData & Keys.Alt) != Keys.Alt)
			{
				Keys keys = keyData & Keys.KeyCode;
				if (keys <= Keys.Tab)
				{
					if (keys != Keys.Back)
					{
						if (keys == Keys.Tab)
						{
							return this.Multiline && this.textBoxFlags[TextBoxBase.acceptsTab] && (keyData & Keys.Control) == Keys.None;
						}
					}
					else if (!this.ReadOnly)
					{
						return true;
					}
				}
				else if (keys != Keys.Escape)
				{
					if (keys - Keys.Prior <= 3)
					{
						return true;
					}
				}
				else if (this.Multiline)
				{
					return false;
				}
			}
			return base.IsInputKey(keyData);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600070A RID: 1802 RVA: 0x00013C60 File Offset: 0x00011E60
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			CommonProperties.xClearPreferredSizeCache(this);
			this.AdjustHeight(true);
			this.UpdateMaxLength();
			if (this.textBoxFlags[TextBoxBase.modified])
			{
				base.SendMessage(185, 1, 0);
			}
			if (this.textBoxFlags[TextBoxBase.scrollToCaretOnHandleCreated])
			{
				this.ScrollToCaret();
				this.textBoxFlags[TextBoxBase.scrollToCaretOnHandleCreated] = false;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600070B RID: 1803 RVA: 0x00013CD0 File Offset: 0x00011ED0
		protected override void OnHandleDestroyed(EventArgs e)
		{
			this.textBoxFlags[TextBoxBase.modified] = this.Modified;
			this.textBoxFlags[TextBoxBase.setSelectionOnHandleCreated] = true;
			this.GetSelectionStartAndLength(out this.selectionStart, out this.selectionLength);
			base.OnHandleDestroyed(e);
		}

		/// <summary>Replaces the current selection in the text box with the contents of the Clipboard.</summary>
		// Token: 0x0600070C RID: 1804 RVA: 0x00013D1D File Offset: 0x00011F1D
		[UIPermission(SecurityAction.Demand, Clipboard = UIPermissionClipboard.OwnClipboard)]
		public void Paste()
		{
			IntSecurity.ClipboardRead.Demand();
			base.SendMessage(770, 0, 0);
		}

		/// <summary>Processes a dialog key.</summary>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
		/// <returns>
		///   <see langword="true" /> if the key was processed by the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600070D RID: 1805 RVA: 0x00013D38 File Offset: 0x00011F38
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected override bool ProcessDialogKey(Keys keyData)
		{
			Keys keys = keyData & Keys.KeyCode;
			if (keys == Keys.Tab && this.AcceptsTab && (keyData & Keys.Control) != Keys.None)
			{
				keyData &= ~Keys.Control;
			}
			return base.ProcessDialogKey(keyData);
		}

		/// <summary>Occurs when the control is redrawn. This event is not relevant for this class.</summary>
		// Token: 0x1400001D RID: 29
		// (add) Token: 0x0600070E RID: 1806 RVA: 0x00013D73 File Offset: 0x00011F73
		// (remove) Token: 0x0600070F RID: 1807 RVA: 0x00013D7C File Offset: 0x00011F7C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event PaintEventHandler Paint
		{
			add
			{
				base.Paint += value;
			}
			remove
			{
				base.Paint -= value;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TextBoxBase.AcceptsTabChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000710 RID: 1808 RVA: 0x00013D88 File Offset: 0x00011F88
		protected virtual void OnAcceptsTabChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[TextBoxBase.EVENT_ACCEPTSTABCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TextBoxBase.BorderStyleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000711 RID: 1809 RVA: 0x00013DB8 File Offset: 0x00011FB8
		protected virtual void OnBorderStyleChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[TextBoxBase.EVENT_BORDERSTYLECHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000712 RID: 1810 RVA: 0x00013DE6 File Offset: 0x00011FE6
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.AdjustHeight(false);
		}

		/// <summary>Raise the <see cref="E:System.Windows.Forms.TextBoxBase.HideSelectionChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000713 RID: 1811 RVA: 0x00013DF8 File Offset: 0x00011FF8
		protected virtual void OnHideSelectionChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[TextBoxBase.EVENT_HIDESELECTIONCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TextBoxBase.ModifiedChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000714 RID: 1812 RVA: 0x00013E28 File Offset: 0x00012028
		protected virtual void OnModifiedChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[TextBoxBase.EVENT_MODIFIEDCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.</summary>
		/// <param name="mevent">The event data.</param>
		// Token: 0x06000715 RID: 1813 RVA: 0x00013E58 File Offset: 0x00012058
		protected override void OnMouseUp(MouseEventArgs mevent)
		{
			Point point = base.PointToScreen(mevent.Location);
			if (mevent.Button == MouseButtons.Left)
			{
				if (!base.ValidationCancelled && UnsafeNativeMethods.WindowFromPoint(point.X, point.Y) == base.Handle)
				{
					if (!this.doubleClickFired)
					{
						this.OnClick(mevent);
						this.OnMouseClick(mevent);
					}
					else
					{
						this.doubleClickFired = false;
						this.OnDoubleClick(mevent);
						this.OnMouseDoubleClick(mevent);
					}
				}
				this.doubleClickFired = false;
			}
			base.OnMouseUp(mevent);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TextBoxBase.MultilineChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000716 RID: 1814 RVA: 0x00013EE4 File Offset: 0x000120E4
		protected virtual void OnMultilineChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[TextBoxBase.EVENT_MULTILINECHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>This method is not relevant for this class.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000717 RID: 1815 RVA: 0x00013F12 File Offset: 0x00012112
		protected override void OnPaddingChanged(EventArgs e)
		{
			base.OnPaddingChanged(e);
			this.AdjustHeight(false);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TextBoxBase.ReadOnlyChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000718 RID: 1816 RVA: 0x00013F24 File Offset: 0x00012124
		protected virtual void OnReadOnlyChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[TextBoxBase.EVENT_READONLYCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.TextChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000719 RID: 1817 RVA: 0x00013F52 File Offset: 0x00012152
		protected override void OnTextChanged(EventArgs e)
		{
			CommonProperties.xClearPreferredSizeCache(this);
			base.OnTextChanged(e);
			this.RaiseAccessibilityTextChangedEvent();
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00013F67 File Offset: 0x00012167
		internal virtual void RaiseAccessibilityTextChangedEvent()
		{
			if (AccessibilityImprovements.Level5 && base.IsAccessibilityObjectCreated)
			{
				base.AccessibilityObject.RaiseAutomationEvent(20015);
			}
		}

		/// <summary>Retrieves the character that is closest to the specified location within the control.</summary>
		/// <param name="pt">The location from which to seek the nearest character.</param>
		/// <returns>The character at the specified location.</returns>
		// Token: 0x0600071B RID: 1819 RVA: 0x00013F8C File Offset: 0x0001218C
		public virtual char GetCharFromPosition(Point pt)
		{
			string text = this.Text;
			int charIndexFromPosition = this.GetCharIndexFromPosition(pt);
			if (charIndexFromPosition >= 0 && charIndexFromPosition < text.Length)
			{
				return text[charIndexFromPosition];
			}
			return '\0';
		}

		/// <summary>Retrieves the index of the character nearest to the specified location.</summary>
		/// <param name="pt">The location to search.</param>
		/// <returns>The zero-based character index at the specified location.</returns>
		// Token: 0x0600071C RID: 1820 RVA: 0x00013FC0 File Offset: 0x000121C0
		public virtual int GetCharIndexFromPosition(Point pt)
		{
			int num = NativeMethods.Util.MAKELONG(pt.X, pt.Y);
			int num2 = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 215, 0, num);
			num2 = NativeMethods.Util.LOWORD(num2);
			if (num2 < 0)
			{
				num2 = 0;
			}
			else
			{
				string text = this.Text;
				if (num2 >= text.Length)
				{
					num2 = Math.Max(text.Length - 1, 0);
				}
			}
			return num2;
		}

		/// <summary>Retrieves the line number from the specified character position within the text of the control.</summary>
		/// <param name="index">The character index position to search.</param>
		/// <returns>The zero-based line number in which the character index is located.</returns>
		// Token: 0x0600071D RID: 1821 RVA: 0x0001402E File Offset: 0x0001222E
		public virtual int GetLineFromCharIndex(int index)
		{
			return (int)(long)base.SendMessage(201, index, 0);
		}

		/// <summary>Retrieves the location within the control at the specified character index.</summary>
		/// <param name="index">The index of the character for which to retrieve the location.</param>
		/// <returns>The location of the specified character within the client rectangle of the control.</returns>
		// Token: 0x0600071E RID: 1822 RVA: 0x00014044 File Offset: 0x00012244
		public virtual Point GetPositionFromCharIndex(int index)
		{
			if (index < 0 || index >= this.Text.Length)
			{
				return Point.Empty;
			}
			int num = (int)(long)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 214, index, 0);
			return new Point(NativeMethods.Util.SignedLOWORD(num), NativeMethods.Util.SignedHIWORD(num));
		}

		/// <summary>Retrieves the index of the first character of a given line.</summary>
		/// <param name="lineNumber">The line for which to get the index of its first character.</param>
		/// <returns>The zero-based index of the first character in the specified line.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <paramref name="lineNumber" /> parameter is less than zero.</exception>
		// Token: 0x0600071F RID: 1823 RVA: 0x0001409C File Offset: 0x0001229C
		public int GetFirstCharIndexFromLine(int lineNumber)
		{
			if (lineNumber < 0)
			{
				throw new ArgumentOutOfRangeException("lineNumber", SR.GetString("InvalidArgument", new object[]
				{
					"lineNumber",
					lineNumber.ToString(CultureInfo.CurrentCulture)
				}));
			}
			return (int)(long)base.SendMessage(187, lineNumber, 0);
		}

		/// <summary>Retrieves the index of the first character of the current line.</summary>
		/// <returns>The zero-based character index in the current line.</returns>
		// Token: 0x06000720 RID: 1824 RVA: 0x000140F2 File Offset: 0x000122F2
		public int GetFirstCharIndexOfCurrentLine()
		{
			return (int)(long)base.SendMessage(187, -1, 0);
		}

		/// <summary>Scrolls the contents of the control to the current caret position.</summary>
		// Token: 0x06000721 RID: 1825 RVA: 0x00014108 File Offset: 0x00012308
		public void ScrollToCaret()
		{
			if (base.IsHandleCreated)
			{
				if (string.IsNullOrEmpty(this.WindowText))
				{
					return;
				}
				bool flag = false;
				object obj = null;
				IntPtr intPtr = IntPtr.Zero;
				try
				{
					if (UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1084, 0, out obj) != 0)
					{
						intPtr = Marshal.GetIUnknownForObject(obj);
						if (intPtr != IntPtr.Zero)
						{
							IntPtr zero = IntPtr.Zero;
							Guid guid = typeof(UnsafeNativeMethods.ITextDocument).GUID;
							try
							{
								Marshal.QueryInterface(intPtr, ref guid, out zero);
								UnsafeNativeMethods.ITextDocument textDocument = Marshal.GetObjectForIUnknown(zero) as UnsafeNativeMethods.ITextDocument;
								if (textDocument != null)
								{
									int num;
									int num2;
									this.GetSelectionStartAndLength(out num, out num2);
									int lineFromCharIndex = this.GetLineFromCharIndex(num);
									UnsafeNativeMethods.ITextRange textRange = textDocument.Range(this.WindowText.Length - 1, this.WindowText.Length - 1);
									textRange.ScrollIntoView(0);
									int num3 = (int)(long)base.SendMessage(206, 0, 0);
									if (num3 > lineFromCharIndex)
									{
										textRange = textDocument.Range(num, num + num2);
										textRange.ScrollIntoView(32);
									}
									flag = true;
								}
							}
							finally
							{
								if (zero != IntPtr.Zero)
								{
									Marshal.Release(zero);
								}
							}
						}
					}
				}
				finally
				{
					if (intPtr != IntPtr.Zero)
					{
						Marshal.Release(intPtr);
					}
				}
				if (!flag)
				{
					base.SendMessage(183, 0, 0);
					return;
				}
			}
			else
			{
				this.textBoxFlags[TextBoxBase.scrollToCaretOnHandleCreated] = true;
			}
		}

		/// <summary>Specifies that the value of the <see cref="P:System.Windows.Forms.TextBoxBase.SelectionLength" /> property is zero so that no characters are selected in the control.</summary>
		// Token: 0x06000722 RID: 1826 RVA: 0x00014284 File Offset: 0x00012484
		public void DeselectAll()
		{
			this.SelectionLength = 0;
		}

		/// <summary>Selects a range of text in the text box.</summary>
		/// <param name="start">The position of the first character in the current text selection within the text box.</param>
		/// <param name="length">The number of characters to select.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <paramref name="start" /> parameter is less than zero.</exception>
		// Token: 0x06000723 RID: 1827 RVA: 0x00014290 File Offset: 0x00012490
		public void Select(int start, int length)
		{
			if (start < 0)
			{
				throw new ArgumentOutOfRangeException("start", SR.GetString("InvalidArgument", new object[]
				{
					"start",
					start.ToString(CultureInfo.CurrentCulture)
				}));
			}
			int textLength = this.TextLength;
			if (start > textLength)
			{
				long num = Math.Min(0L, (long)length + (long)start - (long)textLength);
				if (num < -2147483648L)
				{
					length = int.MinValue;
				}
				else
				{
					length = (int)num;
				}
				start = textLength;
			}
			this.SelectInternal(start, length, textLength);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00014310 File Offset: 0x00012510
		internal virtual void SelectInternal(int start, int length, int textLen)
		{
			if (base.IsHandleCreated)
			{
				int num;
				int num2;
				this.AdjustSelectionStartAndEnd(start, length, out num, out num2, textLen);
				base.SendMessage(177, num, num2);
				return;
			}
			this.selectionStart = start;
			this.selectionLength = length;
			this.textBoxFlags[TextBoxBase.setSelectionOnHandleCreated] = true;
		}

		/// <summary>Selects all text in the text box.</summary>
		// Token: 0x06000725 RID: 1829 RVA: 0x00014360 File Offset: 0x00012560
		public void SelectAll()
		{
			int textLength = this.TextLength;
			this.SelectInternal(0, textLength, textLength);
		}

		/// <summary>Sets the specified bounds of the <see cref="T:System.Windows.Forms.TextBoxBase" /> control.</summary>
		/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control.</param>
		/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control.</param>
		/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control.</param>
		/// <param name="height">Not used.</param>
		/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values.</param>
		// Token: 0x06000726 RID: 1830 RVA: 0x00014380 File Offset: 0x00012580
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if (!this.integralHeightAdjust && height != base.Height)
			{
				this.requestedHeight = height;
			}
			if (this.textBoxFlags[TextBoxBase.autoSize] && !this.textBoxFlags[TextBoxBase.multiline])
			{
				height = this.PreferredHeight;
			}
			base.SetBoundsCore(x, y, width, height, specified);
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x000143E0 File Offset: 0x000125E0
		private static void Swap(ref int n1, ref int n2)
		{
			int num = n2;
			n2 = n1;
			n1 = num;
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x000143F8 File Offset: 0x000125F8
		internal void AdjustSelectionStartAndEnd(int selStart, int selLength, out int start, out int end, int textLen)
		{
			start = selStart;
			end = 0;
			if (start <= -1)
			{
				start = -1;
				return;
			}
			int num;
			if (textLen >= 0)
			{
				num = textLen;
			}
			else
			{
				num = this.TextLength;
			}
			if (start > num)
			{
				start = num;
			}
			try
			{
				end = checked(start + selLength);
			}
			catch (OverflowException)
			{
				end = ((start > 0) ? int.MaxValue : int.MinValue);
			}
			if (end < 0)
			{
				end = 0;
			}
			else if (end > num)
			{
				end = num;
			}
			if (this.SelectionUsesDbcsOffsetsInWin9x && Marshal.SystemDefaultCharSize == 1)
			{
				TextBoxBase.ToDbcsOffsets(this.WindowText, ref start, ref end);
			}
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00014494 File Offset: 0x00012694
		internal void SetSelectionOnHandle()
		{
			if (this.textBoxFlags[TextBoxBase.setSelectionOnHandleCreated])
			{
				this.textBoxFlags[TextBoxBase.setSelectionOnHandleCreated] = false;
				int num;
				int num2;
				this.AdjustSelectionStartAndEnd(this.selectionStart, this.selectionLength, out num, out num2, -1);
				base.SendMessage(177, num, num2);
			}
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x000144EC File Offset: 0x000126EC
		private static void ToUnicodeOffsets(string str, ref int start, ref int end)
		{
			Encoding @default = Encoding.Default;
			byte[] bytes = @default.GetBytes(str);
			bool flag = start > end;
			if (flag)
			{
				TextBoxBase.Swap(ref start, ref end);
			}
			if (start < 0)
			{
				start = 0;
			}
			if (start > bytes.Length)
			{
				start = bytes.Length;
			}
			if (end > bytes.Length)
			{
				end = bytes.Length;
			}
			int num = ((start == 0) ? 0 : @default.GetCharCount(bytes, 0, start));
			end = num + @default.GetCharCount(bytes, start, end - start);
			start = num;
			if (flag)
			{
				TextBoxBase.Swap(ref start, ref end);
			}
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0001456C File Offset: 0x0001276C
		internal static void ToDbcsOffsets(string str, ref int start, ref int end)
		{
			Encoding @default = Encoding.Default;
			bool flag = start > end;
			if (flag)
			{
				TextBoxBase.Swap(ref start, ref end);
			}
			if (start < 0)
			{
				start = 0;
			}
			if (start > str.Length)
			{
				start = str.Length;
			}
			if (end < start)
			{
				end = start;
			}
			if (end > str.Length)
			{
				end = str.Length;
			}
			int num = ((start == 0) ? 0 : @default.GetByteCount(str.Substring(0, start)));
			end = num + @default.GetByteCount(str.Substring(start, end - start));
			start = num;
			if (flag)
			{
				TextBoxBase.Swap(ref start, ref end);
			}
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.TextBoxBase" /> control.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.TextBoxBase" />. The string includes the type and the <see cref="T:System.Windows.Forms.TextBoxBase" /> property of the control.</returns>
		// Token: 0x0600072C RID: 1836 RVA: 0x00014604 File Offset: 0x00012804
		public override string ToString()
		{
			string text = base.ToString();
			string text2 = this.Text;
			if (text2.Length > 40)
			{
				text2 = text2.Substring(0, 40) + "...";
			}
			return text + ", Text: " + text2.ToString();
		}

		/// <summary>Undoes the last edit operation in the text box.</summary>
		// Token: 0x0600072D RID: 1837 RVA: 0x0001464E File Offset: 0x0001284E
		public void Undo()
		{
			base.SendMessage(199, 0, 0);
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0001465E File Offset: 0x0001285E
		internal virtual void UpdateMaxLength()
		{
			if (base.IsHandleCreated)
			{
				base.SendMessage(197, this.maxLength, 0);
			}
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0001467B File Offset: 0x0001287B
		internal override IntPtr InitializeDCForWmCtlColor(IntPtr dc, int msg)
		{
			if (msg == 312 && !this.ShouldSerializeBackColor())
			{
				return IntPtr.Zero;
			}
			return base.InitializeDCForWmCtlColor(dc, msg);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0001469C File Offset: 0x0001289C
		private void WmReflectCommand(ref Message m)
		{
			if (!this.textBoxFlags[TextBoxBase.codeUpdateText] && !this.textBoxFlags[TextBoxBase.creatingHandle])
			{
				if (NativeMethods.Util.HIWORD(m.WParam) == 768 && this.CanRaiseTextChangedEvent)
				{
					this.OnTextChanged(EventArgs.Empty);
					return;
				}
				if (NativeMethods.Util.HIWORD(m.WParam) == 1024)
				{
					bool flag = this.Modified;
				}
			}
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0001470C File Offset: 0x0001290C
		private void WmSetFont(ref Message m)
		{
			base.WndProc(ref m);
			if (!this.textBoxFlags[TextBoxBase.multiline])
			{
				base.SendMessage(211, 3, 0);
			}
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x00014738 File Offset: 0x00012938
		private void WmGetDlgCode(ref Message m)
		{
			base.WndProc(ref m);
			if (this.AcceptsTab)
			{
				m.Result = (IntPtr)((int)(long)m.Result | 2);
				return;
			}
			m.Result = (IntPtr)((int)(long)m.Result & -7);
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00014788 File Offset: 0x00012988
		private void WmTextBoxContextMenu(ref Message m)
		{
			if (this.ContextMenu != null || this.ContextMenuStrip != null)
			{
				int num = NativeMethods.Util.SignedLOWORD(m.LParam);
				int num2 = NativeMethods.Util.SignedHIWORD(m.LParam);
				bool flag = false;
				Point point;
				if ((int)(long)m.LParam == -1)
				{
					flag = true;
					point = new Point(base.Width / 2, base.Height / 2);
				}
				else
				{
					point = base.PointToClientInternal(new Point(num, num2));
				}
				if (base.ClientRectangle.Contains(point))
				{
					if (this.ContextMenu != null)
					{
						this.ContextMenu.Show(this, point);
						return;
					}
					if (this.ContextMenuStrip != null)
					{
						this.ContextMenuStrip.ShowInternal(this, point, flag);
						return;
					}
					this.DefWndProc(ref m);
				}
			}
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x06000734 RID: 1844 RVA: 0x00014840 File Offset: 0x00012A40
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg <= 123)
			{
				if (msg != 2)
				{
					if (msg == 48)
					{
						this.WmSetFont(ref m);
						return;
					}
					if (msg == 123)
					{
						if (this.ShortcutsEnabled)
						{
							base.WndProc(ref m);
							return;
						}
						this.WmTextBoxContextMenu(ref m);
						return;
					}
				}
				else
				{
					base.WndProc(ref m);
					if (!AccessibilityImprovements.Level5 || !base.IsAccessibilityObjectCreated || base.RecreatingHandle)
					{
						return;
					}
					if (ApiHelper.IsApiAvailable("UIAutomationCore.dll", "UiaDisconnectProvider"))
					{
						int num = UnsafeNativeMethods.UiaDisconnectProvider(base.AccessibilityObject);
					}
					TextBoxBase.TextBoxBaseAccessibleObject textBoxBaseAccessibleObject = base.AccessibilityObject as TextBoxBase.TextBoxBaseAccessibleObject;
					if (textBoxBaseAccessibleObject != null)
					{
						textBoxBaseAccessibleObject.ClearObjects();
						return;
					}
					return;
				}
			}
			else
			{
				if (msg == 135)
				{
					this.WmGetDlgCode(ref m);
					return;
				}
				if (msg == 515)
				{
					this.doubleClickFired = true;
					base.WndProc(ref m);
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

		// Token: 0x040004C0 RID: 1216
		private static readonly int autoSize = BitVector32.CreateMask();

		// Token: 0x040004C1 RID: 1217
		private static readonly int hideSelection = BitVector32.CreateMask(TextBoxBase.autoSize);

		// Token: 0x040004C2 RID: 1218
		private static readonly int multiline = BitVector32.CreateMask(TextBoxBase.hideSelection);

		// Token: 0x040004C3 RID: 1219
		private static readonly int modified = BitVector32.CreateMask(TextBoxBase.multiline);

		// Token: 0x040004C4 RID: 1220
		private static readonly int readOnly = BitVector32.CreateMask(TextBoxBase.modified);

		// Token: 0x040004C5 RID: 1221
		private static readonly int acceptsTab = BitVector32.CreateMask(TextBoxBase.readOnly);

		// Token: 0x040004C6 RID: 1222
		private static readonly int wordWrap = BitVector32.CreateMask(TextBoxBase.acceptsTab);

		// Token: 0x040004C7 RID: 1223
		private static readonly int creatingHandle = BitVector32.CreateMask(TextBoxBase.wordWrap);

		// Token: 0x040004C8 RID: 1224
		private static readonly int codeUpdateText = BitVector32.CreateMask(TextBoxBase.creatingHandle);

		// Token: 0x040004C9 RID: 1225
		private static readonly int shortcutsEnabled = BitVector32.CreateMask(TextBoxBase.codeUpdateText);

		// Token: 0x040004CA RID: 1226
		private static readonly int scrollToCaretOnHandleCreated = BitVector32.CreateMask(TextBoxBase.shortcutsEnabled);

		// Token: 0x040004CB RID: 1227
		private static readonly int setSelectionOnHandleCreated = BitVector32.CreateMask(TextBoxBase.scrollToCaretOnHandleCreated);

		// Token: 0x040004CC RID: 1228
		private static readonly object EVENT_ACCEPTSTABCHANGED = new object();

		// Token: 0x040004CD RID: 1229
		private static readonly object EVENT_BORDERSTYLECHANGED = new object();

		// Token: 0x040004CE RID: 1230
		private static readonly object EVENT_HIDESELECTIONCHANGED = new object();

		// Token: 0x040004CF RID: 1231
		private static readonly object EVENT_MODIFIEDCHANGED = new object();

		// Token: 0x040004D0 RID: 1232
		private static readonly object EVENT_MULTILINECHANGED = new object();

		// Token: 0x040004D1 RID: 1233
		private static readonly object EVENT_READONLYCHANGED = new object();

		// Token: 0x040004D2 RID: 1234
		private BorderStyle borderStyle = BorderStyle.Fixed3D;

		// Token: 0x040004D3 RID: 1235
		private int maxLength = 32767;

		// Token: 0x040004D4 RID: 1236
		private int requestedHeight;

		// Token: 0x040004D5 RID: 1237
		private bool integralHeightAdjust;

		// Token: 0x040004D6 RID: 1238
		private int selectionStart;

		// Token: 0x040004D7 RID: 1239
		private int selectionLength;

		// Token: 0x040004D8 RID: 1240
		private bool doubleClickFired;

		// Token: 0x040004D9 RID: 1241
		private static int[] shortcutsToDisable;

		// Token: 0x040004DA RID: 1242
		private BitVector32 textBoxFlags;

		// Token: 0x020005F8 RID: 1528
		internal class TextBoxBaseAccessibleObject : Control.ControlAccessibleObject
		{
			// Token: 0x06006162 RID: 24930 RVA: 0x001680F8 File Offset: 0x001662F8
			public TextBoxBaseAccessibleObject(TextBoxBase owner)
				: base(owner)
			{
				this._owningTextBoxBase = owner;
				this._textProvider = new TextBoxBase.TextBoxBaseUiaTextProvider(owner);
			}

			// Token: 0x06006163 RID: 24931 RVA: 0x00168114 File Offset: 0x00166314
			internal void ClearObjects()
			{
				this._textProvider = null;
				this._owningTextBoxBase = null;
				base.ClearOwnerControl();
			}

			// Token: 0x06006164 RID: 24932 RVA: 0x00012E4E File Offset: 0x0001104E
			internal override bool IsIAccessibleExSupported()
			{
				return true;
			}

			// Token: 0x06006165 RID: 24933 RVA: 0x0016812A File Offset: 0x0016632A
			internal override bool IsPatternSupported(int patternId)
			{
				return patternId == 10014 || patternId == 10018 || patternId == 10024 || base.IsPatternSupported(patternId);
			}

			// Token: 0x06006166 RID: 24934 RVA: 0x00168150 File Offset: 0x00166350
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID <= 30040)
				{
					switch (propertyID)
					{
					case 30001:
						return this.Bounds;
					case 30002:
					case 30004:
						break;
					case 30003:
						return 50004;
					case 30005:
						return this.Name;
					default:
						if (propertyID == 30040)
						{
							return this.IsPatternSupported(10014);
						}
						break;
					}
				}
				else
				{
					if (propertyID == 30043)
					{
						return this.IsPatternSupported(10002);
					}
					if (propertyID == 30090)
					{
						return this.IsPatternSupported(10018);
					}
					if (propertyID == 30119)
					{
						return this.IsPatternSupported(10024);
					}
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x170014E4 RID: 5348
			// (get) Token: 0x06006167 RID: 24935 RVA: 0x00168212 File Offset: 0x00166412
			internal override bool IsReadOnly
			{
				get
				{
					TextBoxBase owningTextBoxBase = this._owningTextBoxBase;
					return owningTextBoxBase != null && owningTextBoxBase.ReadOnly;
				}
			}

			// Token: 0x170014E5 RID: 5349
			// (get) Token: 0x06006168 RID: 24936 RVA: 0x00168225 File Offset: 0x00166425
			internal override UnsafeNativeMethods.UiaCore.ITextRangeProvider DocumentRangeInternal
			{
				get
				{
					TextBoxBase.TextBoxBaseUiaTextProvider textProvider = this._textProvider;
					if (textProvider == null)
					{
						return null;
					}
					return textProvider.DocumentRange;
				}
			}

			// Token: 0x06006169 RID: 24937 RVA: 0x00168238 File Offset: 0x00166438
			internal override UnsafeNativeMethods.UiaCore.ITextRangeProvider[] GetTextSelection()
			{
				TextBoxBase.TextBoxBaseUiaTextProvider textProvider = this._textProvider;
				if (textProvider == null)
				{
					return null;
				}
				return textProvider.GetSelection();
			}

			// Token: 0x0600616A RID: 24938 RVA: 0x0016824B File Offset: 0x0016644B
			internal override UnsafeNativeMethods.UiaCore.ITextRangeProvider[] GetTextVisibleRanges()
			{
				TextBoxBase.TextBoxBaseUiaTextProvider textProvider = this._textProvider;
				if (textProvider == null)
				{
					return null;
				}
				return textProvider.GetVisibleRanges();
			}

			// Token: 0x0600616B RID: 24939 RVA: 0x0016825E File Offset: 0x0016645E
			internal override UnsafeNativeMethods.UiaCore.ITextRangeProvider GetTextRangeFromChild(UnsafeNativeMethods.IRawElementProviderSimple childElement)
			{
				TextBoxBase.TextBoxBaseUiaTextProvider textProvider = this._textProvider;
				if (textProvider == null)
				{
					return null;
				}
				return textProvider.RangeFromChild(childElement);
			}

			// Token: 0x0600616C RID: 24940 RVA: 0x00168272 File Offset: 0x00166472
			internal override UnsafeNativeMethods.UiaCore.ITextRangeProvider GetTextRangeFromPoint(Point screenLocation)
			{
				TextBoxBase.TextBoxBaseUiaTextProvider textProvider = this._textProvider;
				if (textProvider == null)
				{
					return null;
				}
				return textProvider.RangeFromPoint(screenLocation);
			}

			// Token: 0x170014E6 RID: 5350
			// (get) Token: 0x0600616D RID: 24941 RVA: 0x00168286 File Offset: 0x00166486
			internal override UnsafeNativeMethods.UiaCore.SupportedTextSelection SupportedTextSelectionInternal
			{
				get
				{
					TextBoxBase.TextBoxBaseUiaTextProvider textProvider = this._textProvider;
					if (textProvider == null)
					{
						return UnsafeNativeMethods.UiaCore.SupportedTextSelection.None;
					}
					return textProvider.SupportedTextSelection;
				}
			}

			// Token: 0x0600616E RID: 24942 RVA: 0x00168299 File Offset: 0x00166499
			internal override UnsafeNativeMethods.UiaCore.ITextRangeProvider GetTextCaretRange(out UnsafeNativeMethods.BOOL isActive)
			{
				isActive = UnsafeNativeMethods.BOOL.FALSE;
				TextBoxBase.TextBoxBaseUiaTextProvider textProvider = this._textProvider;
				if (textProvider == null)
				{
					return null;
				}
				return textProvider.GetCaretRange(out isActive);
			}

			// Token: 0x0600616F RID: 24943 RVA: 0x001682B0 File Offset: 0x001664B0
			internal override UnsafeNativeMethods.UiaCore.ITextRangeProvider GetRangeFromAnnotation(UnsafeNativeMethods.IRawElementProviderSimple annotationElement)
			{
				TextBoxBase.TextBoxBaseUiaTextProvider textProvider = this._textProvider;
				if (textProvider == null)
				{
					return null;
				}
				return textProvider.RangeFromAnnotation(annotationElement);
			}

			// Token: 0x0400388D RID: 14477
			private TextBoxBase _owningTextBoxBase;

			// Token: 0x0400388E RID: 14478
			private TextBoxBase.TextBoxBaseUiaTextProvider _textProvider;
		}

		// Token: 0x020005F9 RID: 1529
		internal class TextBoxBaseUiaTextProvider : UiaTextProvider2
		{
			// Token: 0x06006170 RID: 24944 RVA: 0x001682C4 File Offset: 0x001664C4
			public TextBoxBaseUiaTextProvider(TextBoxBase owner)
			{
				if (owner == null)
				{
					throw new ArgumentNullException("owner");
				}
				this._owningTextBoxBase = owner;
			}

			// Token: 0x06006171 RID: 24945 RVA: 0x001682E4 File Offset: 0x001664E4
			public override UnsafeNativeMethods.UiaCore.ITextRangeProvider[] GetSelection()
			{
				if (!this._owningTextBoxBase.IsHandleCreated)
				{
					return null;
				}
				int num = 0;
				int num2 = 0;
				this._owningTextBoxBase.SendMessage(176, ref num, ref num2);
				InternalAccessibleObject internalAccessibleObject = new InternalAccessibleObject(this._owningTextBoxBase.AccessibilityObject);
				return new UnsafeNativeMethods.UiaCore.ITextRangeProvider[]
				{
					new UiaTextRange(internalAccessibleObject, this, num, num2)
				};
			}

			// Token: 0x06006172 RID: 24946 RVA: 0x0016833C File Offset: 0x0016653C
			public override UnsafeNativeMethods.UiaCore.ITextRangeProvider[] GetVisibleRanges()
			{
				if (!this._owningTextBoxBase.IsHandleCreated)
				{
					return null;
				}
				int num;
				int num2;
				this.GetVisibleRangePoints(out num, out num2);
				InternalAccessibleObject internalAccessibleObject = new InternalAccessibleObject(this._owningTextBoxBase.AccessibilityObject);
				return new UnsafeNativeMethods.UiaCore.ITextRangeProvider[]
				{
					new UiaTextRange(internalAccessibleObject, this, num, num2)
				};
			}

			// Token: 0x06006173 RID: 24947 RVA: 0x00015C90 File Offset: 0x00013E90
			public override UnsafeNativeMethods.UiaCore.ITextRangeProvider RangeFromChild(UnsafeNativeMethods.IRawElementProviderSimple childElement)
			{
				return null;
			}

			// Token: 0x06006174 RID: 24948 RVA: 0x00168388 File Offset: 0x00166588
			public override UnsafeNativeMethods.UiaCore.ITextRangeProvider RangeFromPoint(Point screenLocation)
			{
				if (!this._owningTextBoxBase.IsHandleCreated)
				{
					return null;
				}
				Point point = screenLocation;
				if (UnsafeNativeMethods.MapWindowPoint(IntPtr.Zero, this._owningTextBoxBase.InternalHandle, ref point) == 0)
				{
					return new UiaTextRange(new InternalAccessibleObject(this._owningTextBoxBase.AccessibilityObject), this, 0, 0);
				}
				NativeMethods.RECT rect = this._owningTextBoxBase.ClientRectangle;
				point.X = Math.Max(point.X, rect.left);
				point.X = Math.Min(point.X, rect.right);
				point.Y = Math.Max(point.Y, rect.top);
				point.Y = Math.Min(point.Y, rect.bottom);
				int charIndexFromPosition = this._owningTextBoxBase.GetCharIndexFromPosition(point);
				return new UiaTextRange(new InternalAccessibleObject(this._owningTextBoxBase.AccessibilityObject), this, charIndexFromPosition, charIndexFromPosition);
			}

			// Token: 0x170014E7 RID: 5351
			// (get) Token: 0x06006175 RID: 24949 RVA: 0x00168472 File Offset: 0x00166672
			public override UnsafeNativeMethods.UiaCore.ITextRangeProvider DocumentRange
			{
				get
				{
					return new UiaTextRange(new InternalAccessibleObject(this._owningTextBoxBase.AccessibilityObject), this, 0, this.TextLength);
				}
			}

			// Token: 0x170014E8 RID: 5352
			// (get) Token: 0x06006176 RID: 24950 RVA: 0x00012E4E File Offset: 0x0001104E
			public override UnsafeNativeMethods.UiaCore.SupportedTextSelection SupportedTextSelection
			{
				get
				{
					return UnsafeNativeMethods.UiaCore.SupportedTextSelection.Single;
				}
			}

			// Token: 0x06006177 RID: 24951 RVA: 0x00168494 File Offset: 0x00166694
			public override UnsafeNativeMethods.UiaCore.ITextRangeProvider GetCaretRange(out UnsafeNativeMethods.BOOL isActive)
			{
				isActive = UnsafeNativeMethods.BOOL.FALSE;
				if (!this._owningTextBoxBase.IsHandleCreated)
				{
					return null;
				}
				object propertyValue = this._owningTextBoxBase.AccessibilityObject.GetPropertyValue(30008);
				if (propertyValue is bool && (bool)propertyValue)
				{
					isActive = UnsafeNativeMethods.BOOL.TRUE;
				}
				InternalAccessibleObject internalAccessibleObject = new InternalAccessibleObject(this._owningTextBoxBase.AccessibilityObject);
				return new UiaTextRange(internalAccessibleObject, this, this._owningTextBoxBase.SelectionStart, this._owningTextBoxBase.SelectionStart);
			}

			// Token: 0x06006178 RID: 24952 RVA: 0x0016850A File Offset: 0x0016670A
			public override Point PointToScreen(Point pt)
			{
				return this._owningTextBoxBase.PointToScreen(pt);
			}

			// Token: 0x06006179 RID: 24953 RVA: 0x00168518 File Offset: 0x00166718
			public override UnsafeNativeMethods.UiaCore.ITextRangeProvider RangeFromAnnotation(UnsafeNativeMethods.IRawElementProviderSimple annotationElement)
			{
				InternalAccessibleObject internalAccessibleObject = new InternalAccessibleObject(this._owningTextBoxBase.AccessibilityObject);
				return new UiaTextRange(internalAccessibleObject, this, 0, 0);
			}

			// Token: 0x170014E9 RID: 5353
			// (get) Token: 0x0600617A RID: 24954 RVA: 0x0016853F File Offset: 0x0016673F
			public override Rectangle BoundingRectangle
			{
				get
				{
					if (this._owningTextBoxBase.IsHandleCreated)
					{
						return this.GetFormattingRectangle();
					}
					return Rectangle.Empty;
				}
			}

			// Token: 0x170014EA RID: 5354
			// (get) Token: 0x0600617B RID: 24955 RVA: 0x0016855F File Offset: 0x0016675F
			public override int FirstVisibleLine
			{
				get
				{
					if (!this._owningTextBoxBase.IsHandleCreated)
					{
						return -1;
					}
					return (int)(long)this._owningTextBoxBase.SendMessage(206, 0, 0);
				}
			}

			// Token: 0x170014EB RID: 5355
			// (get) Token: 0x0600617C RID: 24956 RVA: 0x00168588 File Offset: 0x00166788
			public override bool IsMultiline
			{
				get
				{
					return this._owningTextBoxBase.Multiline;
				}
			}

			// Token: 0x170014EC RID: 5356
			// (get) Token: 0x0600617D RID: 24957 RVA: 0x00168595 File Offset: 0x00166795
			public override bool IsReadingRTL
			{
				get
				{
					return this._owningTextBoxBase.IsHandleCreated && NativeMethods.HasFlag(this.WindowExStyle, 8192);
				}
			}

			// Token: 0x170014ED RID: 5357
			// (get) Token: 0x0600617E RID: 24958 RVA: 0x001685B6 File Offset: 0x001667B6
			public override bool IsReadOnly
			{
				get
				{
					return this._owningTextBoxBase.ReadOnly;
				}
			}

			// Token: 0x170014EE RID: 5358
			// (get) Token: 0x0600617F RID: 24959 RVA: 0x001685C4 File Offset: 0x001667C4
			public override bool IsScrollable
			{
				get
				{
					if (!this._owningTextBoxBase.IsHandleCreated)
					{
						return false;
					}
					int num = (int)(long)UnsafeNativeMethods.GetWindowLong(new HandleRef(this._owningTextBoxBase, this._owningTextBoxBase.Handle), -16);
					return NativeMethods.HasFlag(num, 128) || NativeMethods.HasFlag(num, 64);
				}
			}

			// Token: 0x170014EF RID: 5359
			// (get) Token: 0x06006180 RID: 24960 RVA: 0x0016861B File Offset: 0x0016681B
			public override int LinesCount
			{
				get
				{
					if (!this._owningTextBoxBase.IsHandleCreated)
					{
						return -1;
					}
					return (int)(long)this._owningTextBoxBase.SendMessage(186, 0, 0);
				}
			}

			// Token: 0x170014F0 RID: 5360
			// (get) Token: 0x06006181 RID: 24961 RVA: 0x00168644 File Offset: 0x00166844
			public override int LinesPerPage
			{
				get
				{
					if (!this._owningTextBoxBase.IsHandleCreated)
					{
						return -1;
					}
					Rectangle clientRectangle = this._owningTextBoxBase.ClientRectangle;
					if (clientRectangle.IsEmpty)
					{
						return 0;
					}
					if (!this._owningTextBoxBase.Multiline)
					{
						return 1;
					}
					int height = this._owningTextBoxBase.Font.Height;
					if (height == 0)
					{
						return 0;
					}
					return (int)Math.Ceiling((double)clientRectangle.Height / (double)height);
				}
			}

			// Token: 0x170014F1 RID: 5361
			// (get) Token: 0x06006182 RID: 24962 RVA: 0x001686AD File Offset: 0x001668AD
			public override NativeMethods.LOGFONT Logfont
			{
				get
				{
					if (!this._owningTextBoxBase.IsHandleCreated)
					{
						return new NativeMethods.LOGFONT();
					}
					return NativeMethods.LOGFONT.FromFont(this._owningTextBoxBase.Font);
				}
			}

			// Token: 0x170014F2 RID: 5362
			// (get) Token: 0x06006183 RID: 24963 RVA: 0x001686D2 File Offset: 0x001668D2
			public override string Text
			{
				get
				{
					if (!this._owningTextBoxBase.IsHandleCreated)
					{
						return string.Empty;
					}
					return this._owningTextBoxBase.Text;
				}
			}

			// Token: 0x170014F3 RID: 5363
			// (get) Token: 0x06006184 RID: 24964 RVA: 0x001686F2 File Offset: 0x001668F2
			public override int TextLength
			{
				get
				{
					if (!this._owningTextBoxBase.IsHandleCreated)
					{
						return -1;
					}
					return (int)(long)this._owningTextBoxBase.SendMessage(14, 0, 0);
				}
			}

			// Token: 0x170014F4 RID: 5364
			// (get) Token: 0x06006185 RID: 24965 RVA: 0x00168718 File Offset: 0x00166918
			public override int WindowExStyle
			{
				get
				{
					if (!this._owningTextBoxBase.IsHandleCreated)
					{
						return 0;
					}
					return base.GetWindowExStyle(new HandleRef(this._owningTextBoxBase, this._owningTextBoxBase.Handle));
				}
			}

			// Token: 0x170014F5 RID: 5365
			// (get) Token: 0x06006186 RID: 24966 RVA: 0x00168745 File Offset: 0x00166945
			public override int WindowStyle
			{
				get
				{
					if (!this._owningTextBoxBase.IsHandleCreated)
					{
						return 0;
					}
					return base.GetWindowStyle(new HandleRef(this._owningTextBoxBase, this._owningTextBoxBase.Handle));
				}
			}

			// Token: 0x170014F6 RID: 5366
			// (get) Token: 0x06006187 RID: 24967 RVA: 0x00168772 File Offset: 0x00166972
			public override int EditStyle
			{
				get
				{
					if (!this._owningTextBoxBase.IsHandleCreated)
					{
						return 0;
					}
					return base.GetEditStyle(new HandleRef(this._owningTextBoxBase, this._owningTextBoxBase.Handle));
				}
			}

			// Token: 0x06006188 RID: 24968 RVA: 0x0016879F File Offset: 0x0016699F
			public override int GetLineFromCharIndex(int charIndex)
			{
				if (!this._owningTextBoxBase.IsHandleCreated)
				{
					return -1;
				}
				return this._owningTextBoxBase.GetLineFromCharIndex(charIndex);
			}

			// Token: 0x06006189 RID: 24969 RVA: 0x001687BC File Offset: 0x001669BC
			public override int GetLineIndex(int line)
			{
				if (!this._owningTextBoxBase.IsHandleCreated)
				{
					return -1;
				}
				return (int)(long)this._owningTextBoxBase.SendMessage(187, line, 0);
			}

			// Token: 0x0600618A RID: 24970 RVA: 0x001687E5 File Offset: 0x001669E5
			public override Point GetPositionFromChar(int charIndex)
			{
				if (!this._owningTextBoxBase.IsHandleCreated)
				{
					return Point.Empty;
				}
				return this._owningTextBoxBase.GetPositionFromCharIndex(charIndex);
			}

			// Token: 0x0600618B RID: 24971 RVA: 0x00168808 File Offset: 0x00166A08
			public override Point GetPositionFromCharForUpperRightCorner(int startCharIndex, string text)
			{
				if (!this._owningTextBoxBase.IsHandleCreated || startCharIndex < 0 || startCharIndex >= text.Length)
				{
					return Point.Empty;
				}
				char c = text[startCharIndex];
				Point point;
				if (!char.IsControl(c))
				{
					point = this._owningTextBoxBase.GetPositionFromCharIndex(startCharIndex);
					Size size;
					if (this.GetTextExtentPoint32(c, out size))
					{
						point.X += size.Width;
					}
					return point;
				}
				if (c == '\t')
				{
					bool flag = startCharIndex < this.TextLength - 1 && this.GetLineFromCharIndex(startCharIndex + 1) == this.GetLineFromCharIndex(startCharIndex);
					return this._owningTextBoxBase.GetPositionFromCharIndex(flag ? (startCharIndex + 1) : startCharIndex);
				}
				point = this._owningTextBoxBase.GetPositionFromCharIndex(startCharIndex);
				if (c == '\r' || c == '\n')
				{
					point.X += 2;
				}
				return point;
			}

			// Token: 0x0600618C RID: 24972 RVA: 0x001688D8 File Offset: 0x00166AD8
			public override void GetVisibleRangePoints(out int visibleStart, out int visibleEnd)
			{
				visibleStart = 0;
				visibleEnd = 0;
				if (!this._owningTextBoxBase.IsHandleCreated || TextBoxBase.TextBoxBaseUiaTextProvider.<GetVisibleRangePoints>g__IsDegenerate|45_0(this._owningTextBoxBase.ClientRectangle))
				{
					return;
				}
				Rectangle rectangle = this.GetFormattingRectangle();
				if (TextBoxBase.TextBoxBaseUiaTextProvider.<GetVisibleRangePoints>g__IsDegenerate|45_0(rectangle))
				{
					return;
				}
				Point point = new Point(rectangle.X + 1, rectangle.Y + 1);
				Point point2 = new Point(rectangle.Right - 1, rectangle.Bottom - 1);
				visibleStart = this._owningTextBoxBase.GetCharIndexFromPosition(point);
				visibleEnd = this._owningTextBoxBase.GetCharIndexFromPosition(point2) + 1;
			}

			// Token: 0x0600618D RID: 24973 RVA: 0x00168970 File Offset: 0x00166B70
			public override bool LineScroll(int charactersHorizontal, int linesVertical)
			{
				return this._owningTextBoxBase.IsHandleCreated && this._owningTextBoxBase.SendMessage(182, charactersHorizontal, linesVertical) != IntPtr.Zero;
			}

			// Token: 0x0600618E RID: 24974 RVA: 0x0016899D File Offset: 0x00166B9D
			public override void SetSelection(int start, int end)
			{
				if (!this._owningTextBoxBase.IsHandleCreated)
				{
					return;
				}
				if (start < 0 || start > this.TextLength)
				{
					return;
				}
				if (end < 0 || end > this.TextLength)
				{
					return;
				}
				this._owningTextBoxBase.SendMessage(177, start, end);
			}

			// Token: 0x0600618F RID: 24975 RVA: 0x001689DC File Offset: 0x00166BDC
			private NativeMethods.RECT GetFormattingRectangle()
			{
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				this._owningTextBoxBase.SendMessage(178, 0, ref rect);
				return rect;
			}

			// Token: 0x06006190 RID: 24976 RVA: 0x00168A08 File Offset: 0x00166C08
			private bool GetTextExtentPoint32(char item, out Size size)
			{
				IntNativeMethods.SIZE size2 = new IntNativeMethods.SIZE();
				IntPtr dc = UnsafeNativeMethods.GetDC(new HandleRef(this._owningTextBoxBase, this._owningTextBoxBase.Handle));
				bool flag = IntUnsafeNativeMethods.GetTextExtentPoint32(new HandleRef(this._owningTextBoxBase, dc), item.ToString(), size2) != 0;
				size = size2.ToSize();
				return flag;
			}

			// Token: 0x06006191 RID: 24977 RVA: 0x00168A61 File Offset: 0x00166C61
			[CompilerGenerated]
			internal static bool <GetVisibleRangePoints>g__IsDegenerate|45_0(Rectangle rect)
			{
				return rect.IsEmpty || rect.Width <= 0 || rect.Height <= 0;
			}

			// Token: 0x0400388F RID: 14479
			private readonly TextBoxBase _owningTextBoxBase;
		}
	}
}
