using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design;
using System.Windows.Forms.Layout;
using Microsoft.Win32;

namespace System.Windows.Forms
{
	/// <summary>Represents a text box in a <see cref="T:System.Windows.Forms.ToolStrip" /> that allows the user to enter text.</summary>
	// Token: 0x02000109 RID: 265
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.MenuStrip | ToolStripItemDesignerAvailability.ContextMenuStrip)]
	public class ToolStripTextBox : ToolStripControlHost
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> class.</summary>
		// Token: 0x0600047A RID: 1146 RVA: 0x00010EE8 File Offset: 0x0000F0E8
		public ToolStripTextBox()
			: base(ToolStripTextBox.CreateControlInstance())
		{
			ToolStripTextBox.ToolStripTextBoxControl toolStripTextBoxControl = base.Control as ToolStripTextBox.ToolStripTextBoxControl;
			toolStripTextBoxControl.Owner = this;
			if (DpiHelper.EnableToolStripHighDpiImprovements)
			{
				this.scaledDefaultMargin = DpiHelper.LogicalToDeviceUnits(ToolStripTextBox.defaultMargin, 0);
				this.scaledDefaultDropDownMargin = DpiHelper.LogicalToDeviceUnits(ToolStripTextBox.defaultDropDownMargin, 0);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> class with the specified name.</summary>
		/// <param name="name">The name of the <see cref="T:System.Windows.Forms.ToolStripTextBox" />.</param>
		// Token: 0x0600047B RID: 1147 RVA: 0x00010F52 File Offset: 0x0000F152
		public ToolStripTextBox(string name)
			: this()
		{
			base.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> class derived from a base control.</summary>
		/// <param name="c">The control from which to derive the <see cref="T:System.Windows.Forms.ToolStripTextBox" />.</param>
		// Token: 0x0600047C RID: 1148 RVA: 0x00010F61 File Offset: 0x0000F161
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ToolStripTextBox(Control c)
			: base(c)
		{
			throw new NotSupportedException(SR.GetString("ToolStripMustSupplyItsOwnTextBox"));
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>The background image displayed in the control.</returns>
		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x00010F8F File Offset: 0x0000F18F
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x00010F97 File Offset: 0x0000F197
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageLayout" /> value.</returns>
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x00010FA0 File Offset: 0x0000F1A0
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x00010FA8 File Offset: 0x0000F1A8
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		/// <summary>Gets the spacing, in pixels, between the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> and adjacent items.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value representing the spacing.</returns>
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x00010FB1 File Offset: 0x0000F1B1
		protected internal override Padding DefaultMargin
		{
			get
			{
				if (base.IsOnDropDown)
				{
					return this.scaledDefaultDropDownMargin;
				}
				return this.scaledDefaultMargin;
			}
		}

		/// <summary>Gets the default size of the <see cref="T:System.Windows.Forms.ToolStripTextBox" />.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> in pixels. The default size is 100 pixels by 25 pixels.</returns>
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x00010FC8 File Offset: 0x0000F1C8
		protected override Size DefaultSize
		{
			get
			{
				return new Size(100, 22);
			}
		}

		/// <summary>Gets the hosted <see cref="T:System.Windows.Forms.TextBox" /> control.</summary>
		/// <returns>The hosted <see cref="T:System.Windows.Forms.TextBox" />.</returns>
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x00010FD3 File Offset: 0x0000F1D3
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TextBox TextBox
		{
			get
			{
				return base.Control as TextBox;
			}
		}

		/// <summary>Creates a new accessibility object for the control.</summary>
		/// <returns>A new accessibility object for the control.</returns>
		// Token: 0x06000484 RID: 1156 RVA: 0x00010FE0 File Offset: 0x0000F1E0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level3)
			{
				return new ToolStripTextBox.ToolStripTextBoxAccessibleObject(this);
			}
			return base.CreateAccessibilityInstance();
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00010FF8 File Offset: 0x0000F1F8
		private static Control CreateControlInstance()
		{
			return new ToolStripTextBox.ToolStripTextBoxControl
			{
				BorderStyle = BorderStyle.Fixed3D,
				AutoSize = true
			};
		}

		/// <summary>Retrieves the size of a rectangular area into which a control can be fitted.</summary>
		/// <param name="constrainingSize">The custom-sized area for a control.</param>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		// Token: 0x06000486 RID: 1158 RVA: 0x0001101C File Offset: 0x0000F21C
		public override Size GetPreferredSize(Size constrainingSize)
		{
			return new Size(CommonProperties.GetSpecifiedBounds(this.TextBox).Width, this.TextBox.PreferredHeight);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0001104C File Offset: 0x0000F24C
		private void HandleAcceptsTabChanged(object sender, EventArgs e)
		{
			this.OnAcceptsTabChanged(e);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00011055 File Offset: 0x0000F255
		private void HandleBorderStyleChanged(object sender, EventArgs e)
		{
			this.OnBorderStyleChanged(e);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0001105E File Offset: 0x0000F25E
		private void HandleHideSelectionChanged(object sender, EventArgs e)
		{
			this.OnHideSelectionChanged(e);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00011067 File Offset: 0x0000F267
		private void HandleModifiedChanged(object sender, EventArgs e)
		{
			this.OnModifiedChanged(e);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00011070 File Offset: 0x0000F270
		private void HandleMultilineChanged(object sender, EventArgs e)
		{
			this.OnMultilineChanged(e);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00011079 File Offset: 0x0000F279
		private void HandleReadOnlyChanged(object sender, EventArgs e)
		{
			this.OnReadOnlyChanged(e);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00011082 File Offset: 0x0000F282
		private void HandleTextBoxTextAlignChanged(object sender, EventArgs e)
		{
			base.RaiseEvent(ToolStripTextBox.EventTextBoxTextAlignChanged, e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripTextBox.AcceptsTabChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600048E RID: 1166 RVA: 0x00011090 File Offset: 0x0000F290
		protected virtual void OnAcceptsTabChanged(EventArgs e)
		{
			base.RaiseEvent(ToolStripTextBox.EventAcceptsTabChanged, e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripTextBox.BorderStyleChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600048F RID: 1167 RVA: 0x0001109E File Offset: 0x0000F29E
		protected virtual void OnBorderStyleChanged(EventArgs e)
		{
			base.RaiseEvent(ToolStripTextBox.EventBorderStyleChanged, e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripTextBox.HideSelectionChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000490 RID: 1168 RVA: 0x000110AC File Offset: 0x0000F2AC
		protected virtual void OnHideSelectionChanged(EventArgs e)
		{
			base.RaiseEvent(ToolStripTextBox.EventHideSelectionChanged, e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripTextBox.ModifiedChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000491 RID: 1169 RVA: 0x000110BA File Offset: 0x0000F2BA
		protected virtual void OnModifiedChanged(EventArgs e)
		{
			base.RaiseEvent(ToolStripTextBox.EventModifiedChanged, e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripTextBox.MultilineChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000492 RID: 1170 RVA: 0x000110C8 File Offset: 0x0000F2C8
		protected virtual void OnMultilineChanged(EventArgs e)
		{
			base.RaiseEvent(ToolStripTextBox.EventMultilineChanged, e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripTextBox.ReadOnlyChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000493 RID: 1171 RVA: 0x000110D6 File Offset: 0x0000F2D6
		protected virtual void OnReadOnlyChanged(EventArgs e)
		{
			base.RaiseEvent(ToolStripTextBox.EventReadOnlyChanged, e);
		}

		/// <summary>Subscribes events from the hosted control.</summary>
		/// <param name="control">The control from which to subscribe events.</param>
		// Token: 0x06000494 RID: 1172 RVA: 0x000110E4 File Offset: 0x0000F2E4
		protected override void OnSubscribeControlEvents(Control control)
		{
			TextBox textBox = control as TextBox;
			if (textBox != null)
			{
				textBox.AcceptsTabChanged += this.HandleAcceptsTabChanged;
				textBox.BorderStyleChanged += this.HandleBorderStyleChanged;
				textBox.HideSelectionChanged += this.HandleHideSelectionChanged;
				textBox.ModifiedChanged += this.HandleModifiedChanged;
				textBox.MultilineChanged += this.HandleMultilineChanged;
				textBox.ReadOnlyChanged += this.HandleReadOnlyChanged;
				textBox.TextAlignChanged += this.HandleTextBoxTextAlignChanged;
			}
			base.OnSubscribeControlEvents(control);
		}

		/// <summary>Unsubscribes events from the hosted control.</summary>
		/// <param name="control">The control from which to unsubscribe events.</param>
		// Token: 0x06000495 RID: 1173 RVA: 0x00011180 File Offset: 0x0000F380
		protected override void OnUnsubscribeControlEvents(Control control)
		{
			TextBox textBox = control as TextBox;
			if (textBox != null)
			{
				textBox.AcceptsTabChanged -= this.HandleAcceptsTabChanged;
				textBox.BorderStyleChanged -= this.HandleBorderStyleChanged;
				textBox.HideSelectionChanged -= this.HandleHideSelectionChanged;
				textBox.ModifiedChanged -= this.HandleModifiedChanged;
				textBox.MultilineChanged -= this.HandleMultilineChanged;
				textBox.ReadOnlyChanged -= this.HandleReadOnlyChanged;
				textBox.TextAlignChanged -= this.HandleTextBoxTextAlignChanged;
			}
			base.OnUnsubscribeControlEvents(control);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0001121C File Offset: 0x0000F41C
		internal override bool ShouldSerializeFont()
		{
			return this.Font != ToolStripManager.DefaultFont;
		}

		/// <summary>Gets or sets a value indicating whether pressing the TAB key in a multiline text box control types a TAB character in the control instead of moving the focus to the next control in the tab order.</summary>
		/// <returns>
		///   <see langword="true" /> if users can enter tabs in a multiline text box using the TAB key; <see langword="false" /> if pressing the TAB key moves the focus. The default is <see langword="false" />.</returns>
		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x0001122E File Offset: 0x0000F42E
		// (set) Token: 0x06000498 RID: 1176 RVA: 0x0001123B File Offset: 0x0000F43B
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("TextBoxAcceptsTabDescr")]
		public bool AcceptsTab
		{
			get
			{
				return this.TextBox.AcceptsTab;
			}
			set
			{
				this.TextBox.AcceptsTab = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether pressing ENTER in a multiline <see cref="T:System.Windows.Forms.TextBox" /> control creates a new line of text in the control or activates the default button for the form.</summary>
		/// <returns>
		///   <see langword="true" /> if the ENTER key creates a new line of text in a multiline version of the control; <see langword="false" /> if the ENTER key activates the default button for the form. The default is <see langword="false" />.</returns>
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x00011249 File Offset: 0x0000F449
		// (set) Token: 0x0600049A RID: 1178 RVA: 0x00011256 File Offset: 0x0000F456
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("TextBoxAcceptsReturnDescr")]
		public bool AcceptsReturn
		{
			get
			{
				return this.TextBox.AcceptsReturn;
			}
			set
			{
				this.TextBox.AcceptsReturn = value;
			}
		}

		/// <summary>Gets or sets a custom string collection to use when the <see cref="P:System.Windows.Forms.ToolStripTextBox.AutoCompleteSource" /> property is set to <see langword="CustomSource" />.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AutoCompleteStringCollection" /> to use with <see cref="P:System.Windows.Forms.TextBox.AutoCompleteSource" />.</returns>
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x00011264 File Offset: 0x0000F464
		// (set) Token: 0x0600049C RID: 1180 RVA: 0x00011271 File Offset: 0x0000F471
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
				return this.TextBox.AutoCompleteCustomSource;
			}
			set
			{
				this.TextBox.AutoCompleteCustomSource = value;
			}
		}

		/// <summary>Gets or sets an option that controls how automatic completion works for the <see cref="T:System.Windows.Forms.ToolStripTextBox" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AutoCompleteMode" /> values. The default is <see cref="F:System.Windows.Forms.AutoCompleteMode.None" />.</returns>
		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x0001127F File Offset: 0x0000F47F
		// (set) Token: 0x0600049E RID: 1182 RVA: 0x0001128C File Offset: 0x0000F48C
		[DefaultValue(AutoCompleteMode.None)]
		[SRDescription("TextBoxAutoCompleteModeDescr")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public AutoCompleteMode AutoCompleteMode
		{
			get
			{
				return this.TextBox.AutoCompleteMode;
			}
			set
			{
				this.TextBox.AutoCompleteMode = value;
			}
		}

		/// <summary>Gets or sets a value specifying the source of complete strings used for automatic completion.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AutoCompleteSource" /> values. The default is <see cref="F:System.Windows.Forms.AutoCompleteSource.None" />.</returns>
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x0001129A File Offset: 0x0000F49A
		// (set) Token: 0x060004A0 RID: 1184 RVA: 0x000112A7 File Offset: 0x0000F4A7
		[DefaultValue(AutoCompleteSource.None)]
		[SRDescription("TextBoxAutoCompleteSourceDescr")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public AutoCompleteSource AutoCompleteSource
		{
			get
			{
				return this.TextBox.AutoCompleteSource;
			}
			set
			{
				this.TextBox.AutoCompleteSource = value;
			}
		}

		/// <summary>Gets or sets the border type of the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. The default is <see cref="F:System.Windows.Forms.BorderStyle.Fixed3D" />.</returns>
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x000112B5 File Offset: 0x0000F4B5
		// (set) Token: 0x060004A2 RID: 1186 RVA: 0x000112C2 File Offset: 0x0000F4C2
		[SRCategory("CatAppearance")]
		[DefaultValue(BorderStyle.Fixed3D)]
		[DispId(-504)]
		[SRDescription("TextBoxBorderDescr")]
		public BorderStyle BorderStyle
		{
			get
			{
				return this.TextBox.BorderStyle;
			}
			set
			{
				this.TextBox.BorderStyle = value;
			}
		}

		/// <summary>Gets a value indicating whether the user can undo the previous operation in a <see cref="T:System.Windows.Forms.ToolStripTextBox" /> control.</summary>
		/// <returns>
		///   <see langword="true" /> if the user can undo the previous operation performed in a text box control; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x000112D0 File Offset: 0x0000F4D0
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TextBoxCanUndoDescr")]
		public bool CanUndo
		{
			get
			{
				return this.TextBox.CanUndo;
			}
		}

		/// <summary>Gets or sets whether the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> control modifies the case of characters as they are typed.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.CharacterCasing" /> values. The default is <see cref="F:System.Windows.Forms.CharacterCasing.Normal" />.</returns>
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x000112DD File Offset: 0x0000F4DD
		// (set) Token: 0x060004A5 RID: 1189 RVA: 0x000112EA File Offset: 0x0000F4EA
		[SRCategory("CatBehavior")]
		[DefaultValue(CharacterCasing.Normal)]
		[SRDescription("TextBoxCharacterCasingDescr")]
		public CharacterCasing CharacterCasing
		{
			get
			{
				return this.TextBox.CharacterCasing;
			}
			set
			{
				this.TextBox.CharacterCasing = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the selected text in the text box control remains highlighted when the control loses focus.</summary>
		/// <returns>
		///   <see langword="true" /> if the selected text does not appear highlighted when the text box control loses focus; <see langword="false" />, if the selected text remains highlighted when the text box control loses focus. The default is <see langword="true" />.</returns>
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x000112F8 File Offset: 0x0000F4F8
		// (set) Token: 0x060004A7 RID: 1191 RVA: 0x00011305 File Offset: 0x0000F505
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("TextBoxHideSelectionDescr")]
		public bool HideSelection
		{
			get
			{
				return this.TextBox.HideSelection;
			}
			set
			{
				this.TextBox.HideSelection = value;
			}
		}

		/// <summary>Gets or sets the lines of text in a <see cref="T:System.Windows.Forms.ToolStripTextBox" /> control.</summary>
		/// <returns>An array of strings that contains the text in a text box control.</returns>
		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x00011313 File Offset: 0x0000F513
		// (set) Token: 0x060004A9 RID: 1193 RVA: 0x00011320 File Offset: 0x0000F520
		[SRCategory("CatAppearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Localizable(true)]
		[SRDescription("TextBoxLinesDescr")]
		[Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string[] Lines
		{
			get
			{
				return this.TextBox.Lines;
			}
			set
			{
				this.TextBox.Lines = value;
			}
		}

		/// <summary>Gets or sets the maximum number of characters the user can type or paste into the text box control.</summary>
		/// <returns>The number of characters that can be entered into the control. The default is 32767 characters.</returns>
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x0001132E File Offset: 0x0000F52E
		// (set) Token: 0x060004AB RID: 1195 RVA: 0x0001133B File Offset: 0x0000F53B
		[SRCategory("CatBehavior")]
		[DefaultValue(32767)]
		[Localizable(true)]
		[SRDescription("TextBoxMaxLengthDescr")]
		public int MaxLength
		{
			get
			{
				return this.TextBox.MaxLength;
			}
			set
			{
				this.TextBox.MaxLength = value;
			}
		}

		/// <summary>Gets or sets a value that indicates that the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> control has been modified by the user since the control was created or its contents were last set.</summary>
		/// <returns>
		///   <see langword="true" /> if the control's contents have been modified; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x00011349 File Offset: 0x0000F549
		// (set) Token: 0x060004AD RID: 1197 RVA: 0x00011356 File Offset: 0x0000F556
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TextBoxModifiedDescr")]
		public bool Modified
		{
			get
			{
				return this.TextBox.Modified;
			}
			set
			{
				this.TextBox.Modified = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>
		///   <see langword="true" /> if enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x00011364 File Offset: 0x0000F564
		// (set) Token: 0x060004AF RID: 1199 RVA: 0x00011371 File Offset: 0x0000F571
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[Localizable(true)]
		[SRDescription("TextBoxMultilineDescr")]
		[RefreshProperties(RefreshProperties.All)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Multiline
		{
			get
			{
				return this.TextBox.Multiline;
			}
			set
			{
				this.TextBox.Multiline = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether text in the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> is read-only; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x0001137F File Offset: 0x0000F57F
		// (set) Token: 0x060004B1 RID: 1201 RVA: 0x0001138C File Offset: 0x0000F58C
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("TextBoxReadOnlyDescr")]
		public bool ReadOnly
		{
			get
			{
				return this.TextBox.ReadOnly;
			}
			set
			{
				this.TextBox.ReadOnly = value;
			}
		}

		/// <summary>Gets or sets a value indicating the currently selected text in the control.</summary>
		/// <returns>A string that represents the currently selected text in the text box.</returns>
		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x0001139A File Offset: 0x0000F59A
		// (set) Token: 0x060004B3 RID: 1203 RVA: 0x000113A7 File Offset: 0x0000F5A7
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TextBoxSelectedTextDescr")]
		public string SelectedText
		{
			get
			{
				return this.TextBox.SelectedText;
			}
			set
			{
				this.TextBox.SelectedText = value;
			}
		}

		/// <summary>Gets or sets the number of characters selected in the <see cref="T:System.Windows.Forms.ToolStripTextBox" />.</summary>
		/// <returns>The number of characters selected in the <see cref="T:System.Windows.Forms.ToolStripTextBox" />.</returns>
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x000113B5 File Offset: 0x0000F5B5
		// (set) Token: 0x060004B5 RID: 1205 RVA: 0x000113C2 File Offset: 0x0000F5C2
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TextBoxSelectionLengthDescr")]
		public int SelectionLength
		{
			get
			{
				return this.TextBox.SelectionLength;
			}
			set
			{
				this.TextBox.SelectionLength = value;
			}
		}

		/// <summary>Gets or sets the starting point of text selected in the <see cref="T:System.Windows.Forms.ToolStripTextBox" />.</summary>
		/// <returns>The starting position of text selected in the <see cref="T:System.Windows.Forms.ToolStripTextBox" />.</returns>
		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x000113D0 File Offset: 0x0000F5D0
		// (set) Token: 0x060004B7 RID: 1207 RVA: 0x000113DD File Offset: 0x0000F5DD
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TextBoxSelectionStartDescr")]
		public int SelectionStart
		{
			get
			{
				return this.TextBox.SelectionStart;
			}
			set
			{
				this.TextBox.SelectionStart = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the defined shortcuts are enabled.</summary>
		/// <returns>
		///   <see langword="true" /> to enable the shortcuts; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x000113EB File Offset: 0x0000F5EB
		// (set) Token: 0x060004B9 RID: 1209 RVA: 0x000113F8 File Offset: 0x0000F5F8
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("TextBoxShortcutsEnabledDescr")]
		public bool ShortcutsEnabled
		{
			get
			{
				return this.TextBox.ShortcutsEnabled;
			}
			set
			{
				this.TextBox.ShortcutsEnabled = value;
			}
		}

		/// <summary>Gets the length of text in the control.</summary>
		/// <returns>The number of characters contained in the text of the <see cref="T:System.Windows.Forms.ToolStripTextBox" />.</returns>
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x00011406 File Offset: 0x0000F606
		[Browsable(false)]
		public int TextLength
		{
			get
			{
				return this.TextBox.TextLength;
			}
		}

		/// <summary>Gets or sets how text is aligned in a <see cref="T:System.Windows.Forms.TextBox" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> enumeration values that specifies how text is aligned in the control. The default is <see cref="F:System.Windows.Forms.HorizontalAlignment.Left" />.</returns>
		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x00011413 File Offset: 0x0000F613
		// (set) Token: 0x060004BC RID: 1212 RVA: 0x00011420 File Offset: 0x0000F620
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[DefaultValue(HorizontalAlignment.Left)]
		[SRDescription("TextBoxTextAlignDescr")]
		public HorizontalAlignment TextBoxTextAlign
		{
			get
			{
				return this.TextBox.TextAlign;
			}
			set
			{
				this.TextBox.TextAlign = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>
		///   <see langword="true" /> if enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x0001142E File Offset: 0x0000F62E
		// (set) Token: 0x060004BE RID: 1214 RVA: 0x0001143B File Offset: 0x0000F63B
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[DefaultValue(true)]
		[SRDescription("TextBoxWordWrapDescr")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool WordWrap
		{
			get
			{
				return this.TextBox.WordWrap;
			}
			set
			{
				this.TextBox.WordWrap = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripTextBox.AcceptsTab" /> property changes.</summary>
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060004BF RID: 1215 RVA: 0x00011449 File Offset: 0x0000F649
		// (remove) Token: 0x060004C0 RID: 1216 RVA: 0x0001145C File Offset: 0x0000F65C
		[SRCategory("CatPropertyChanged")]
		[SRDescription("TextBoxBaseOnAcceptsTabChangedDescr")]
		public event EventHandler AcceptsTabChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripTextBox.EventAcceptsTabChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripTextBox.EventAcceptsTabChanged, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripTextBox.BorderStyle" /> property changes.</summary>
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060004C1 RID: 1217 RVA: 0x0001146F File Offset: 0x0000F66F
		// (remove) Token: 0x060004C2 RID: 1218 RVA: 0x00011482 File Offset: 0x0000F682
		[SRCategory("CatPropertyChanged")]
		[SRDescription("TextBoxBaseOnBorderStyleChangedDescr")]
		public event EventHandler BorderStyleChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripTextBox.EventBorderStyleChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripTextBox.EventBorderStyleChanged, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripTextBox.HideSelection" /> property changes.</summary>
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060004C3 RID: 1219 RVA: 0x00011495 File Offset: 0x0000F695
		// (remove) Token: 0x060004C4 RID: 1220 RVA: 0x000114A8 File Offset: 0x0000F6A8
		[SRCategory("CatPropertyChanged")]
		[SRDescription("TextBoxBaseOnHideSelectionChangedDescr")]
		public event EventHandler HideSelectionChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripTextBox.EventHideSelectionChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripTextBox.EventHideSelectionChanged, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripTextBox.Modified" /> property changes.</summary>
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060004C5 RID: 1221 RVA: 0x000114BB File Offset: 0x0000F6BB
		// (remove) Token: 0x060004C6 RID: 1222 RVA: 0x000114CE File Offset: 0x0000F6CE
		[SRCategory("CatPropertyChanged")]
		[SRDescription("TextBoxBaseOnModifiedChangedDescr")]
		public event EventHandler ModifiedChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripTextBox.EventModifiedChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripTextBox.EventModifiedChanged, value);
			}
		}

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060004C7 RID: 1223 RVA: 0x000114E1 File Offset: 0x0000F6E1
		// (remove) Token: 0x060004C8 RID: 1224 RVA: 0x000114F4 File Offset: 0x0000F6F4
		[SRCategory("CatPropertyChanged")]
		[SRDescription("TextBoxBaseOnMultilineChangedDescr")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event EventHandler MultilineChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripTextBox.EventMultilineChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripTextBox.EventMultilineChanged, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripTextBox.ReadOnly" /> property changes.</summary>
		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060004C9 RID: 1225 RVA: 0x00011507 File Offset: 0x0000F707
		// (remove) Token: 0x060004CA RID: 1226 RVA: 0x0001151A File Offset: 0x0000F71A
		[SRCategory("CatPropertyChanged")]
		[SRDescription("TextBoxBaseOnReadOnlyChangedDescr")]
		public event EventHandler ReadOnlyChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripTextBox.EventReadOnlyChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripTextBox.EventReadOnlyChanged, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripTextBox.TextBoxTextAlign" /> property changes.</summary>
		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060004CB RID: 1227 RVA: 0x0001152D File Offset: 0x0000F72D
		// (remove) Token: 0x060004CC RID: 1228 RVA: 0x00011540 File Offset: 0x0000F740
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ToolStripTextBoxTextBoxTextAlignChangedDescr")]
		public event EventHandler TextBoxTextAlignChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripTextBox.EventTextBoxTextAlignChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripTextBox.EventTextBoxTextAlignChanged, value);
			}
		}

		/// <summary>Appends text to the current text of the <see cref="T:System.Windows.Forms.ToolStripTextBox" />.</summary>
		/// <param name="text">The text to append to the current contents of the <see cref="T:System.Windows.Forms.ToolStripTextBox" />.</param>
		// Token: 0x060004CD RID: 1229 RVA: 0x00011553 File Offset: 0x0000F753
		public void AppendText(string text)
		{
			this.TextBox.AppendText(text);
		}

		/// <summary>Clears all text from the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> control.</summary>
		// Token: 0x060004CE RID: 1230 RVA: 0x00011561 File Offset: 0x0000F761
		public void Clear()
		{
			this.TextBox.Clear();
		}

		/// <summary>Clears information about the most recent operation from the undo buffer of the <see cref="T:System.Windows.Forms.ToolStripTextBox" />.</summary>
		// Token: 0x060004CF RID: 1231 RVA: 0x0001156E File Offset: 0x0000F76E
		public void ClearUndo()
		{
			this.TextBox.ClearUndo();
		}

		/// <summary>Copies the current selection in the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> to the Clipboard.</summary>
		// Token: 0x060004D0 RID: 1232 RVA: 0x0001157B File Offset: 0x0000F77B
		public void Copy()
		{
			this.TextBox.Copy();
		}

		/// <summary>Moves the current selection in the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> to the Clipboard.</summary>
		// Token: 0x060004D1 RID: 1233 RVA: 0x0001157B File Offset: 0x0000F77B
		public void Cut()
		{
			this.TextBox.Copy();
		}

		/// <summary>Specifies that the value of the <see cref="P:System.Windows.Forms.ToolStripTextBox.SelectionLength" /> property is zero so that no characters are selected in the control.</summary>
		// Token: 0x060004D2 RID: 1234 RVA: 0x00011588 File Offset: 0x0000F788
		public void DeselectAll()
		{
			this.TextBox.DeselectAll();
		}

		/// <summary>Retrieves the character that is closest to the specified location within the control.</summary>
		/// <param name="pt">The location from which to seek the nearest character.</param>
		/// <returns>The character at the specified location.</returns>
		// Token: 0x060004D3 RID: 1235 RVA: 0x00011595 File Offset: 0x0000F795
		public char GetCharFromPosition(Point pt)
		{
			return this.TextBox.GetCharFromPosition(pt);
		}

		/// <summary>Retrieves the index of the character nearest to the specified location.</summary>
		/// <param name="pt">The location to search.</param>
		/// <returns>The zero-based character index at the specified location.</returns>
		// Token: 0x060004D4 RID: 1236 RVA: 0x000115A3 File Offset: 0x0000F7A3
		public int GetCharIndexFromPosition(Point pt)
		{
			return this.TextBox.GetCharIndexFromPosition(pt);
		}

		/// <summary>Retrieves the index of the first character of a given line.</summary>
		/// <param name="lineNumber">The line for which to get the index of its first character.</param>
		/// <returns>The zero-based character index in the specified line.</returns>
		// Token: 0x060004D5 RID: 1237 RVA: 0x000115B1 File Offset: 0x0000F7B1
		public int GetFirstCharIndexFromLine(int lineNumber)
		{
			return this.TextBox.GetFirstCharIndexFromLine(lineNumber);
		}

		/// <summary>Retrieves the index of the first character of the current line.</summary>
		/// <returns>The zero-based character index in the current line.</returns>
		// Token: 0x060004D6 RID: 1238 RVA: 0x000115BF File Offset: 0x0000F7BF
		public int GetFirstCharIndexOfCurrentLine()
		{
			return this.TextBox.GetFirstCharIndexOfCurrentLine();
		}

		/// <summary>Retrieves the line number from the specified character position within the text of the control.</summary>
		/// <param name="index">The character index position to search.</param>
		/// <returns>The zero-based line number in which the character index is located.</returns>
		// Token: 0x060004D7 RID: 1239 RVA: 0x000115CC File Offset: 0x0000F7CC
		public int GetLineFromCharIndex(int index)
		{
			return this.TextBox.GetLineFromCharIndex(index);
		}

		/// <summary>Retrieves the location within the control at the specified character index.</summary>
		/// <param name="index">The index of the character for which to retrieve the location.</param>
		/// <returns>The location of the specified character.</returns>
		// Token: 0x060004D8 RID: 1240 RVA: 0x000115DA File Offset: 0x0000F7DA
		public Point GetPositionFromCharIndex(int index)
		{
			return this.TextBox.GetPositionFromCharIndex(index);
		}

		/// <summary>Replaces the current selection in the text box with the contents of the Clipboard.</summary>
		// Token: 0x060004D9 RID: 1241 RVA: 0x000115E8 File Offset: 0x0000F7E8
		public void Paste()
		{
			this.TextBox.Paste();
		}

		/// <summary>Scrolls the contents of the control to the current caret position.</summary>
		// Token: 0x060004DA RID: 1242 RVA: 0x000115F5 File Offset: 0x0000F7F5
		public void ScrollToCaret()
		{
			this.TextBox.ScrollToCaret();
		}

		/// <summary>Selects a range of text in the text box.</summary>
		/// <param name="start">The position of the first character in the current text selection within the text box.</param>
		/// <param name="length">The number of characters to select.</param>
		// Token: 0x060004DB RID: 1243 RVA: 0x00011602 File Offset: 0x0000F802
		public void Select(int start, int length)
		{
			this.TextBox.Select(start, length);
		}

		/// <summary>Selects all text in the text box.</summary>
		// Token: 0x060004DC RID: 1244 RVA: 0x00011611 File Offset: 0x0000F811
		public void SelectAll()
		{
			this.TextBox.SelectAll();
		}

		/// <summary>Undoes the last edit operation in the text box.</summary>
		// Token: 0x060004DD RID: 1245 RVA: 0x0001161E File Offset: 0x0000F81E
		public void Undo()
		{
			this.TextBox.Undo();
		}

		// Token: 0x0400049D RID: 1181
		internal static readonly object EventTextBoxTextAlignChanged = new object();

		// Token: 0x0400049E RID: 1182
		internal static readonly object EventAcceptsTabChanged = new object();

		// Token: 0x0400049F RID: 1183
		internal static readonly object EventBorderStyleChanged = new object();

		// Token: 0x040004A0 RID: 1184
		internal static readonly object EventHideSelectionChanged = new object();

		// Token: 0x040004A1 RID: 1185
		internal static readonly object EventReadOnlyChanged = new object();

		// Token: 0x040004A2 RID: 1186
		internal static readonly object EventMultilineChanged = new object();

		// Token: 0x040004A3 RID: 1187
		internal static readonly object EventModifiedChanged = new object();

		// Token: 0x040004A4 RID: 1188
		private static readonly Padding defaultMargin = new Padding(1, 0, 1, 0);

		// Token: 0x040004A5 RID: 1189
		private static readonly Padding defaultDropDownMargin = new Padding(1);

		// Token: 0x040004A6 RID: 1190
		private Padding scaledDefaultMargin = ToolStripTextBox.defaultMargin;

		// Token: 0x040004A7 RID: 1191
		private Padding scaledDefaultDropDownMargin = ToolStripTextBox.defaultDropDownMargin;

		// Token: 0x02000552 RID: 1362
		private class ToolStripTextBoxControlAccessibleObjectLevel5 : TextBoxBase.TextBoxBaseAccessibleObject
		{
			// Token: 0x06005582 RID: 21890 RVA: 0x00010E0C File Offset: 0x0000F00C
			public ToolStripTextBoxControlAccessibleObjectLevel5(ToolStripTextBox.ToolStripTextBoxControl toolStripTextBoxControl)
				: base(toolStripTextBoxControl)
			{
			}

			// Token: 0x06005583 RID: 21891 RVA: 0x00166727 File Offset: 0x00164927
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30008)
				{
					return (this.State & AccessibleStates.Focused) == AccessibleStates.Focused;
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x06005584 RID: 21892 RVA: 0x00010ECC File Offset: 0x0000F0CC
			internal override bool IsPatternSupported(int patternId)
			{
				return patternId == 10002 || base.IsPatternSupported(patternId);
			}

			// Token: 0x17001485 RID: 5253
			// (get) Token: 0x06005585 RID: 21893 RVA: 0x0016674C File Offset: 0x0016494C
			internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
			{
				get
				{
					ToolStripTextBox.ToolStripTextBoxControl toolStripTextBoxControl = base.Owner as ToolStripTextBox.ToolStripTextBoxControl;
					if (toolStripTextBoxControl != null)
					{
						return toolStripTextBoxControl.Owner.Owner.AccessibilityObject;
					}
					return base.FragmentRoot;
				}
			}

			// Token: 0x06005586 RID: 21894 RVA: 0x00166780 File Offset: 0x00164980
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (direction <= UnsafeNativeMethods.NavigateDirection.PreviousSibling)
				{
					ToolStripTextBox.ToolStripTextBoxControl toolStripTextBoxControl = base.Owner as ToolStripTextBox.ToolStripTextBoxControl;
					if (toolStripTextBoxControl != null)
					{
						return toolStripTextBoxControl.Owner.AccessibilityObject.FragmentNavigate(direction);
					}
				}
				return base.FragmentNavigate(direction);
			}
		}

		// Token: 0x02000553 RID: 1363
		[ComVisible(true)]
		internal class ToolStripTextBoxAccessibleObject : ToolStripItem.ToolStripItemAccessibleObject
		{
			// Token: 0x06005587 RID: 21895 RVA: 0x001667B9 File Offset: 0x001649B9
			public ToolStripTextBoxAccessibleObject(ToolStripTextBox ownerItem)
				: base(ownerItem)
			{
				this.ownerItem = ownerItem;
			}

			// Token: 0x17001486 RID: 5254
			// (get) Token: 0x06005588 RID: 21896 RVA: 0x001667CC File Offset: 0x001649CC
			public override AccessibleRole Role
			{
				get
				{
					AccessibleRole accessibleRole = base.Owner.AccessibleRole;
					if (accessibleRole != AccessibleRole.Default)
					{
						return accessibleRole;
					}
					return AccessibleRole.Text;
				}
			}

			// Token: 0x06005589 RID: 21897 RVA: 0x001667ED File Offset: 0x001649ED
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (direction == UnsafeNativeMethods.NavigateDirection.FirstChild || direction == UnsafeNativeMethods.NavigateDirection.LastChild)
				{
					return this.ownerItem.TextBox.AccessibilityObject;
				}
				return base.FragmentNavigate(direction);
			}

			// Token: 0x0400381F RID: 14367
			private ToolStripTextBox ownerItem;
		}

		// Token: 0x02000554 RID: 1364
		private class ToolStripTextBoxControl : TextBox
		{
			// Token: 0x0600558A RID: 21898 RVA: 0x0016680F File Offset: 0x00164A0F
			public ToolStripTextBoxControl()
			{
				this.Font = ToolStripManager.DefaultFont;
				this.isFontSet = false;
			}

			// Token: 0x17001487 RID: 5255
			// (get) Token: 0x0600558B RID: 21899 RVA: 0x00166830 File Offset: 0x00164A30
			private NativeMethods.RECT AbsoluteClientRECT
			{
				get
				{
					NativeMethods.RECT rect = default(NativeMethods.RECT);
					CreateParams createParams = this.CreateParams;
					base.AdjustWindowRectEx(ref rect, createParams.Style, this.HasMenu, createParams.ExStyle);
					int num = -rect.left;
					int num2 = -rect.top;
					UnsafeNativeMethods.GetClientRect(new HandleRef(this, base.Handle), ref rect);
					rect.left += num;
					rect.right += num;
					rect.top += num2;
					rect.bottom += num2;
					return rect;
				}
			}

			// Token: 0x17001488 RID: 5256
			// (get) Token: 0x0600558C RID: 21900 RVA: 0x001668BC File Offset: 0x00164ABC
			private Rectangle AbsoluteClientRectangle
			{
				get
				{
					NativeMethods.RECT absoluteClientRECT = this.AbsoluteClientRECT;
					return Rectangle.FromLTRB(absoluteClientRECT.top, absoluteClientRECT.top, absoluteClientRECT.right, absoluteClientRECT.bottom);
				}
			}

			// Token: 0x17001489 RID: 5257
			// (get) Token: 0x0600558D RID: 21901 RVA: 0x001668F0 File Offset: 0x00164AF0
			private ProfessionalColorTable ColorTable
			{
				get
				{
					if (this.Owner != null)
					{
						ToolStripProfessionalRenderer toolStripProfessionalRenderer = this.Owner.Renderer as ToolStripProfessionalRenderer;
						if (toolStripProfessionalRenderer != null)
						{
							return toolStripProfessionalRenderer.ColorTable;
						}
					}
					return ProfessionalColors.ColorTable;
				}
			}

			// Token: 0x1700148A RID: 5258
			// (get) Token: 0x0600558E RID: 21902 RVA: 0x00166925 File Offset: 0x00164B25
			private bool IsPopupTextBox
			{
				get
				{
					return base.BorderStyle == BorderStyle.Fixed3D && this.Owner != null && this.Owner.Renderer is ToolStripProfessionalRenderer;
				}
			}

			// Token: 0x1700148B RID: 5259
			// (get) Token: 0x0600558F RID: 21903 RVA: 0x0016694F File Offset: 0x00164B4F
			// (set) Token: 0x06005590 RID: 21904 RVA: 0x00166957 File Offset: 0x00164B57
			internal bool MouseIsOver
			{
				get
				{
					return this.mouseIsOver;
				}
				set
				{
					if (this.mouseIsOver != value)
					{
						this.mouseIsOver = value;
						if (!this.Focused)
						{
							this.InvalidateNonClient();
						}
					}
				}
			}

			// Token: 0x1700148C RID: 5260
			// (get) Token: 0x06005591 RID: 21905 RVA: 0x0001A0D6 File Offset: 0x000182D6
			// (set) Token: 0x06005592 RID: 21906 RVA: 0x00166977 File Offset: 0x00164B77
			public override Font Font
			{
				get
				{
					return base.Font;
				}
				set
				{
					base.Font = value;
					this.isFontSet = this.ShouldSerializeFont();
				}
			}

			// Token: 0x1700148D RID: 5261
			// (get) Token: 0x06005593 RID: 21907 RVA: 0x0016698C File Offset: 0x00164B8C
			// (set) Token: 0x06005594 RID: 21908 RVA: 0x00166994 File Offset: 0x00164B94
			public ToolStripTextBox Owner
			{
				get
				{
					return this.ownerItem;
				}
				set
				{
					this.ownerItem = value;
				}
			}

			// Token: 0x1700148E RID: 5262
			// (get) Token: 0x06005595 RID: 21909 RVA: 0x000A83A1 File Offset: 0x000A65A1
			internal override bool SupportsUiaProviders
			{
				get
				{
					return AccessibilityImprovements.Level3;
				}
			}

			// Token: 0x06005596 RID: 21910 RVA: 0x001669A0 File Offset: 0x00164BA0
			private void InvalidateNonClient()
			{
				if (!this.IsPopupTextBox)
				{
					return;
				}
				NativeMethods.RECT absoluteClientRECT = this.AbsoluteClientRECT;
				HandleRef handleRef = NativeMethods.NullHandleRef;
				HandleRef handleRef2 = NativeMethods.NullHandleRef;
				HandleRef handleRef3 = NativeMethods.NullHandleRef;
				try
				{
					handleRef3 = new HandleRef(this, SafeNativeMethods.CreateRectRgn(0, 0, base.Width, base.Height));
					handleRef2 = new HandleRef(this, SafeNativeMethods.CreateRectRgn(absoluteClientRECT.left, absoluteClientRECT.top, absoluteClientRECT.right, absoluteClientRECT.bottom));
					handleRef = new HandleRef(this, SafeNativeMethods.CreateRectRgn(0, 0, 0, 0));
					SafeNativeMethods.CombineRgn(handleRef, handleRef3, handleRef2, 3);
					NativeMethods.RECT rect = default(NativeMethods.RECT);
					SafeNativeMethods.RedrawWindow(new HandleRef(this, base.Handle), ref rect, handleRef, 1797);
				}
				finally
				{
					try
					{
						if (handleRef.Handle != IntPtr.Zero)
						{
							SafeNativeMethods.DeleteObject(handleRef);
						}
					}
					finally
					{
						try
						{
							if (handleRef2.Handle != IntPtr.Zero)
							{
								SafeNativeMethods.DeleteObject(handleRef2);
							}
						}
						finally
						{
							if (handleRef3.Handle != IntPtr.Zero)
							{
								SafeNativeMethods.DeleteObject(handleRef3);
							}
						}
					}
				}
			}

			// Token: 0x06005597 RID: 21911 RVA: 0x00166ACC File Offset: 0x00164CCC
			protected override void OnGotFocus(EventArgs e)
			{
				base.OnGotFocus(e);
				this.InvalidateNonClient();
			}

			// Token: 0x06005598 RID: 21912 RVA: 0x00166ADB File Offset: 0x00164CDB
			protected override void OnLostFocus(EventArgs e)
			{
				base.OnLostFocus(e);
				this.InvalidateNonClient();
			}

			// Token: 0x06005599 RID: 21913 RVA: 0x00166AEA File Offset: 0x00164CEA
			protected override void OnMouseEnter(EventArgs e)
			{
				base.OnMouseEnter(e);
				this.MouseIsOver = true;
			}

			// Token: 0x0600559A RID: 21914 RVA: 0x00166AFA File Offset: 0x00164CFA
			protected override void OnMouseLeave(EventArgs e)
			{
				base.OnMouseLeave(e);
				this.MouseIsOver = false;
			}

			// Token: 0x0600559B RID: 21915 RVA: 0x00166B0C File Offset: 0x00164D0C
			private void HookStaticEvents(bool hook)
			{
				if (hook)
				{
					if (this.alreadyHooked)
					{
						return;
					}
					try
					{
						SystemEvents.UserPreferenceChanged += this.OnUserPreferenceChanged;
						return;
					}
					finally
					{
						this.alreadyHooked = true;
					}
				}
				if (this.alreadyHooked)
				{
					try
					{
						SystemEvents.UserPreferenceChanged -= this.OnUserPreferenceChanged;
					}
					finally
					{
						this.alreadyHooked = false;
					}
				}
			}

			// Token: 0x0600559C RID: 21916 RVA: 0x00166B80 File Offset: 0x00164D80
			private void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
			{
				if (e.Category == UserPreferenceCategory.Window && !this.isFontSet)
				{
					this.Font = ToolStripManager.DefaultFont;
				}
			}

			// Token: 0x0600559D RID: 21917 RVA: 0x00166B9F File Offset: 0x00164D9F
			protected override void OnVisibleChanged(EventArgs e)
			{
				base.OnVisibleChanged(e);
				if (!base.Disposing && !base.IsDisposed)
				{
					this.HookStaticEvents(base.Visible);
				}
			}

			// Token: 0x0600559E RID: 21918 RVA: 0x00166BC4 File Offset: 0x00164DC4
			protected override AccessibleObject CreateAccessibilityInstance()
			{
				if (AccessibilityImprovements.Level5)
				{
					return new ToolStripTextBox.ToolStripTextBoxControlAccessibleObjectLevel5(this);
				}
				if (AccessibilityImprovements.Level3)
				{
					return new ToolStripTextBox.ToolStripTextBoxControlAccessibleObject(this);
				}
				return base.CreateAccessibilityInstance();
			}

			// Token: 0x0600559F RID: 21919 RVA: 0x00166BE8 File Offset: 0x00164DE8
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					this.HookStaticEvents(false);
				}
				base.Dispose(disposing);
			}

			// Token: 0x060055A0 RID: 21920 RVA: 0x00166BFC File Offset: 0x00164DFC
			private void WmNCPaint(ref Message m)
			{
				if (!this.IsPopupTextBox)
				{
					base.WndProc(ref m);
					return;
				}
				HandleRef handleRef = new HandleRef(this, UnsafeNativeMethods.GetWindowDC(new HandleRef(this, m.HWnd)));
				if (handleRef.Handle == IntPtr.Zero)
				{
					throw new Win32Exception();
				}
				try
				{
					Color color = ((this.MouseIsOver || this.Focused) ? this.ColorTable.TextBoxBorder : this.BackColor);
					Color color2 = this.BackColor;
					if (!base.Enabled)
					{
						color = SystemColors.ControlDark;
						color2 = SystemColors.Control;
					}
					using (Graphics graphics = Graphics.FromHdcInternal(handleRef.Handle))
					{
						Rectangle absoluteClientRectangle = this.AbsoluteClientRectangle;
						using (Brush brush = new SolidBrush(color2))
						{
							graphics.FillRectangle(brush, 0, 0, base.Width, absoluteClientRectangle.Top);
							graphics.FillRectangle(brush, 0, 0, absoluteClientRectangle.Left, base.Height);
							graphics.FillRectangle(brush, 0, absoluteClientRectangle.Bottom, base.Width, base.Height - absoluteClientRectangle.Height);
							graphics.FillRectangle(brush, absoluteClientRectangle.Right, 0, base.Width - absoluteClientRectangle.Right, base.Height);
						}
						using (Pen pen = new Pen(color))
						{
							graphics.DrawRectangle(pen, 0, 0, base.Width - 1, base.Height - 1);
						}
					}
				}
				finally
				{
					UnsafeNativeMethods.ReleaseDC(new HandleRef(this, base.Handle), handleRef);
				}
				m.Result = IntPtr.Zero;
			}

			// Token: 0x060055A1 RID: 21921 RVA: 0x00166DEC File Offset: 0x00164FEC
			protected override void WndProc(ref Message m)
			{
				if (m.Msg == 133)
				{
					this.WmNCPaint(ref m);
					return;
				}
				base.WndProc(ref m);
			}

			// Token: 0x04003820 RID: 14368
			private bool mouseIsOver;

			// Token: 0x04003821 RID: 14369
			private ToolStripTextBox ownerItem;

			// Token: 0x04003822 RID: 14370
			private bool isFontSet = true;

			// Token: 0x04003823 RID: 14371
			private bool alreadyHooked;
		}

		// Token: 0x02000555 RID: 1365
		private class ToolStripTextBoxControlAccessibleObject : Control.ControlAccessibleObject
		{
			// Token: 0x060055A2 RID: 21922 RVA: 0x0009B733 File Offset: 0x00099933
			public ToolStripTextBoxControlAccessibleObject(ToolStripTextBox.ToolStripTextBoxControl toolStripTextBoxControl)
				: base(toolStripTextBoxControl)
			{
			}

			// Token: 0x1700148F RID: 5263
			// (get) Token: 0x060055A3 RID: 21923 RVA: 0x00166E0C File Offset: 0x0016500C
			internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
			{
				get
				{
					ToolStripTextBox.ToolStripTextBoxControl toolStripTextBoxControl = base.Owner as ToolStripTextBox.ToolStripTextBoxControl;
					if (toolStripTextBoxControl != null)
					{
						return toolStripTextBoxControl.Owner.Owner.AccessibilityObject;
					}
					return base.FragmentRoot;
				}
			}

			// Token: 0x060055A4 RID: 21924 RVA: 0x00166E40 File Offset: 0x00165040
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (direction <= UnsafeNativeMethods.NavigateDirection.PreviousSibling)
				{
					ToolStripTextBox.ToolStripTextBoxControl toolStripTextBoxControl = base.Owner as ToolStripTextBox.ToolStripTextBoxControl;
					if (toolStripTextBoxControl != null)
					{
						return toolStripTextBoxControl.Owner.AccessibilityObject.FragmentNavigate(direction);
					}
				}
				return base.FragmentNavigate(direction);
			}

			// Token: 0x060055A5 RID: 21925 RVA: 0x00166E7C File Offset: 0x0016507C
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30003)
				{
					return 50004;
				}
				if (propertyID == 30005)
				{
					return this.Name;
				}
				if (propertyID != 30008)
				{
					return base.GetPropertyValue(propertyID);
				}
				return (this.State & AccessibleStates.Focused) == AccessibleStates.Focused;
			}

			// Token: 0x060055A6 RID: 21926 RVA: 0x00166ECD File Offset: 0x001650CD
			internal override bool IsPatternSupported(int patternId)
			{
				return patternId == 10002 || patternId == 10018 || base.IsPatternSupported(patternId);
			}
		}
	}
}
