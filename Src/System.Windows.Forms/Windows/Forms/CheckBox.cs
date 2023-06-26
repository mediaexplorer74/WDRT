using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.ButtonInternal;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows <see cref="T:System.Windows.Forms.CheckBox" />.</summary>
	// Token: 0x0200014A RID: 330
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultProperty("Checked")]
	[DefaultEvent("CheckedChanged")]
	[DefaultBindingProperty("CheckState")]
	[ToolboxItem("System.Windows.Forms.Design.AutoSizeToolboxItem,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionCheckBox")]
	public class CheckBox : ButtonBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.CheckBox" /> class.</summary>
		// Token: 0x06000CDA RID: 3290 RVA: 0x00024D1C File Offset: 0x00022F1C
		public CheckBox()
		{
			if (DpiHelper.EnableDpiChangedHighDpiImprovements)
			{
				this.flatSystemStylePaddingWidth = base.LogicalToDeviceUnits(25);
				this.flatSystemStyleMinimumHeight = base.LogicalToDeviceUnits(13);
			}
			base.SetStyle(ControlStyles.StandardClick | ControlStyles.StandardDoubleClick, false);
			base.SetAutoSizeMode(AutoSizeMode.GrowAndShrink);
			this.autoCheck = true;
			this.TextAlign = ContentAlignment.MiddleLeft;
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x00024D8C File Offset: 0x00022F8C
		// (set) Token: 0x06000CDC RID: 3292 RVA: 0x00024D94 File Offset: 0x00022F94
		private bool AccObjDoDefaultAction
		{
			get
			{
				return this.accObjDoDefaultAction;
			}
			set
			{
				this.accObjDoDefaultAction = value;
			}
		}

		/// <summary>Gets or sets the value that determines the appearance of a <see cref="T:System.Windows.Forms.CheckBox" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.Appearance" /> values. The default value is <see cref="F:System.Windows.Forms.Appearance.Normal" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.Appearance" /> values.</exception>
		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000CDD RID: 3293 RVA: 0x00024D9D File Offset: 0x00022F9D
		// (set) Token: 0x06000CDE RID: 3294 RVA: 0x00024DA8 File Offset: 0x00022FA8
		[DefaultValue(Appearance.Normal)]
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("CheckBoxAppearanceDescr")]
		public Appearance Appearance
		{
			get
			{
				return this.appearance;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 1))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(Appearance));
				}
				if (this.appearance != value)
				{
					using (LayoutTransaction.CreateTransactionIf(this.AutoSize, this.ParentInternal, this, PropertyNames.Appearance))
					{
						this.appearance = value;
						if (base.OwnerDraw)
						{
							this.Refresh();
						}
						else
						{
							base.UpdateStyles();
						}
						this.OnAppearanceChanged(EventArgs.Empty);
					}
				}
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.CheckBox.Appearance" /> property changes.</summary>
		// Token: 0x14000066 RID: 102
		// (add) Token: 0x06000CDF RID: 3295 RVA: 0x00024E40 File Offset: 0x00023040
		// (remove) Token: 0x06000CE0 RID: 3296 RVA: 0x00024E53 File Offset: 0x00023053
		[SRCategory("CatPropertyChanged")]
		[SRDescription("CheckBoxOnAppearanceChangedDescr")]
		public event EventHandler AppearanceChanged
		{
			add
			{
				base.Events.AddHandler(CheckBox.EVENT_APPEARANCECHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(CheckBox.EVENT_APPEARANCECHANGED, value);
			}
		}

		/// <summary>Gets or set a value indicating whether the <see cref="P:System.Windows.Forms.CheckBox.Checked" /> or <see cref="P:System.Windows.Forms.CheckBox.CheckState" /> values and the <see cref="T:System.Windows.Forms.CheckBox" />'s appearance are automatically changed when the <see cref="T:System.Windows.Forms.CheckBox" /> is clicked.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Windows.Forms.CheckBox.Checked" /> value or <see cref="P:System.Windows.Forms.CheckBox.CheckState" /> value and the appearance of the control are automatically changed on the <see cref="E:System.Windows.Forms.Control.Click" /> event; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x00024E66 File Offset: 0x00023066
		// (set) Token: 0x06000CE2 RID: 3298 RVA: 0x00024E6E File Offset: 0x0002306E
		[DefaultValue(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("CheckBoxAutoCheckDescr")]
		public bool AutoCheck
		{
			get
			{
				return this.autoCheck;
			}
			set
			{
				this.autoCheck = value;
			}
		}

		/// <summary>Gets or sets the horizontal and vertical alignment of the check mark on a <see cref="T:System.Windows.Forms.CheckBox" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values. The default value is <see langword="MiddleLeft" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Drawing.ContentAlignment" /> enumeration values.</exception>
		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x00024E77 File Offset: 0x00023077
		// (set) Token: 0x06000CE4 RID: 3300 RVA: 0x00024E80 File Offset: 0x00023080
		[Bindable(true)]
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[DefaultValue(ContentAlignment.MiddleLeft)]
		[SRDescription("CheckBoxCheckAlignDescr")]
		public ContentAlignment CheckAlign
		{
			get
			{
				return this.checkAlign;
			}
			set
			{
				if (!WindowsFormsUtils.EnumValidator.IsValidContentAlignment(value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ContentAlignment));
				}
				if (this.checkAlign != value)
				{
					this.checkAlign = value;
					LayoutTransaction.DoLayoutIf(this.AutoSize, this.ParentInternal, this, PropertyNames.CheckAlign);
					if (base.OwnerDraw)
					{
						base.Invalidate();
						return;
					}
					base.UpdateStyles();
				}
			}
		}

		/// <summary>Gets or set a value indicating whether the <see cref="T:System.Windows.Forms.CheckBox" /> is in the checked state.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.CheckBox" /> is in the checked state; otherwise, <see langword="false" />. The default value is <see langword="false" />.  
		///
		///  If the <see cref="P:System.Windows.Forms.CheckBox.ThreeState" /> property is set to <see langword="true" />, the <see cref="P:System.Windows.Forms.CheckBox.Checked" /> property will return <see langword="true" /> for either a <see langword="Checked" /> or <see langword="Indeterminate" /><see cref="P:System.Windows.Forms.CheckBox.CheckState" />.</returns>
		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x00024EE7 File Offset: 0x000230E7
		// (set) Token: 0x06000CE6 RID: 3302 RVA: 0x00024EF2 File Offset: 0x000230F2
		[Bindable(true)]
		[SettingsBindable(true)]
		[DefaultValue(false)]
		[SRCategory("CatAppearance")]
		[RefreshProperties(RefreshProperties.All)]
		[SRDescription("CheckBoxCheckedDescr")]
		public bool Checked
		{
			get
			{
				return this.checkState > CheckState.Unchecked;
			}
			set
			{
				if (value != this.Checked)
				{
					this.CheckState = (value ? CheckState.Checked : CheckState.Unchecked);
				}
			}
		}

		/// <summary>Gets or sets the state of the <see cref="T:System.Windows.Forms.CheckBox" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.CheckState" /> enumeration values. The default value is <see langword="Unchecked" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.CheckState" /> enumeration values.</exception>
		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x00024F0A File Offset: 0x0002310A
		// (set) Token: 0x06000CE8 RID: 3304 RVA: 0x00024F14 File Offset: 0x00023114
		[Bindable(true)]
		[SRCategory("CatAppearance")]
		[DefaultValue(CheckState.Unchecked)]
		[RefreshProperties(RefreshProperties.All)]
		[SRDescription("CheckBoxCheckStateDescr")]
		public CheckState CheckState
		{
			get
			{
				return this.checkState;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(CheckState));
				}
				if (this.checkState != value)
				{
					bool @checked = this.Checked;
					this.checkState = value;
					if (base.IsHandleCreated)
					{
						base.SendMessage(241, (int)this.checkState, 0);
					}
					if (@checked != this.Checked)
					{
						this.OnCheckedChanged(EventArgs.Empty);
					}
					this.OnCheckStateChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.CheckBox" /> control.</summary>
		// Token: 0x14000067 RID: 103
		// (add) Token: 0x06000CE9 RID: 3305 RVA: 0x00023757 File Offset: 0x00021957
		// (remove) Token: 0x06000CEA RID: 3306 RVA: 0x00023760 File Offset: 0x00021960
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler DoubleClick
		{
			add
			{
				base.DoubleClick += value;
			}
			remove
			{
				base.DoubleClick -= value;
			}
		}

		/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.CheckBox" /> control.</summary>
		// Token: 0x14000068 RID: 104
		// (add) Token: 0x06000CEB RID: 3307 RVA: 0x00023769 File Offset: 0x00021969
		// (remove) Token: 0x06000CEC RID: 3308 RVA: 0x00023772 File Offset: 0x00021972
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event MouseEventHandler MouseDoubleClick
		{
			add
			{
				base.MouseDoubleClick += value;
			}
			remove
			{
				base.MouseDoubleClick -= value;
			}
		}

		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x00024F98 File Offset: 0x00023198
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ClassName = "BUTTON";
				if (base.OwnerDraw)
				{
					createParams.Style |= 11;
				}
				else
				{
					createParams.Style |= 5;
					if (this.Appearance == Appearance.Button)
					{
						createParams.Style |= 4096;
					}
					ContentAlignment contentAlignment = base.RtlTranslateContent(this.CheckAlign);
					if ((contentAlignment & CheckBox.anyRight) != (ContentAlignment)0)
					{
						createParams.Style |= 32;
					}
				}
				return createParams;
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>The default size.</returns>
		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x0002501F File Offset: 0x0002321F
		protected override Size DefaultSize
		{
			get
			{
				return new Size(104, 24);
			}
		}

		/// <summary>Provides constants for rescaling the <see cref="T:System.Windows.Forms.CheckBox" /> control when a DPI change occurs.</summary>
		/// <param name="deviceDpiOld">The DPI value prior to the change.</param>
		/// <param name="deviceDpiNew">The DPI value after the change.</param>
		// Token: 0x06000CEF RID: 3311 RVA: 0x0002502A File Offset: 0x0002322A
		protected override void RescaleConstantsForDpi(int deviceDpiOld, int deviceDpiNew)
		{
			base.RescaleConstantsForDpi(deviceDpiOld, deviceDpiNew);
			if (DpiHelper.EnableDpiChangedHighDpiImprovements)
			{
				this.flatSystemStylePaddingWidth = base.LogicalToDeviceUnits(25);
				this.flatSystemStyleMinimumHeight = base.LogicalToDeviceUnits(13);
			}
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x00025058 File Offset: 0x00023258
		internal override Size GetPreferredSizeCore(Size proposedConstraints)
		{
			if (this.Appearance == Appearance.Button)
			{
				ButtonStandardAdapter buttonStandardAdapter = new ButtonStandardAdapter(this);
				return buttonStandardAdapter.GetPreferredSizeCore(proposedConstraints);
			}
			if (base.FlatStyle != FlatStyle.System)
			{
				return base.GetPreferredSizeCore(proposedConstraints);
			}
			Size size = TextRenderer.MeasureText(this.Text, this.Font);
			Size size2 = this.SizeFromClientSize(size);
			size2.Width += this.flatSystemStylePaddingWidth;
			size2.Height = (DpiHelper.EnableDpiChangedHighDpiImprovements ? Math.Max(size2.Height + 5, this.flatSystemStyleMinimumHeight) : (size2.Height + 5));
			return size2 + base.Padding.Size;
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000CF1 RID: 3313 RVA: 0x000250FD File Offset: 0x000232FD
		internal override Rectangle OverChangeRectangle
		{
			get
			{
				if (this.Appearance == Appearance.Button)
				{
					return base.OverChangeRectangle;
				}
				if (base.FlatStyle == FlatStyle.Standard)
				{
					return new Rectangle(-1, -1, 1, 1);
				}
				return base.Adapter.CommonLayout().Layout().checkBounds;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x00025137 File Offset: 0x00023337
		internal override Rectangle DownChangeRectangle
		{
			get
			{
				if (this.Appearance == Appearance.Button || base.FlatStyle == FlatStyle.System)
				{
					return base.DownChangeRectangle;
				}
				return base.Adapter.CommonLayout().Layout().checkBounds;
			}
		}

		/// <summary>Gets or sets the alignment of the text on the <see cref="T:System.Windows.Forms.CheckBox" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values. The default is <see cref="F:System.Drawing.ContentAlignment.MiddleLeft" />.</returns>
		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x00025167 File Offset: 0x00023367
		// (set) Token: 0x06000CF4 RID: 3316 RVA: 0x0002516F File Offset: 0x0002336F
		[Localizable(true)]
		[DefaultValue(ContentAlignment.MiddleLeft)]
		public override ContentAlignment TextAlign
		{
			get
			{
				return base.TextAlign;
			}
			set
			{
				base.TextAlign = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.CheckBox" /> will allow three check states rather than two.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.CheckBox" /> is able to display three check states; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x00025178 File Offset: 0x00023378
		// (set) Token: 0x06000CF6 RID: 3318 RVA: 0x00025180 File Offset: 0x00023380
		[DefaultValue(false)]
		[SRCategory("CatBehavior")]
		[SRDescription("CheckBoxThreeStateDescr")]
		public bool ThreeState
		{
			get
			{
				return this.threeState;
			}
			set
			{
				this.threeState = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.CheckBox.Checked" /> property changes.</summary>
		// Token: 0x14000069 RID: 105
		// (add) Token: 0x06000CF7 RID: 3319 RVA: 0x00025189 File Offset: 0x00023389
		// (remove) Token: 0x06000CF8 RID: 3320 RVA: 0x0002519C File Offset: 0x0002339C
		[SRDescription("CheckBoxOnCheckedChangedDescr")]
		public event EventHandler CheckedChanged
		{
			add
			{
				base.Events.AddHandler(CheckBox.EVENT_CHECKEDCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(CheckBox.EVENT_CHECKEDCHANGED, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.CheckBox.CheckState" /> property changes.</summary>
		// Token: 0x1400006A RID: 106
		// (add) Token: 0x06000CF9 RID: 3321 RVA: 0x000251AF File Offset: 0x000233AF
		// (remove) Token: 0x06000CFA RID: 3322 RVA: 0x000251C2 File Offset: 0x000233C2
		[SRDescription("CheckBoxOnCheckStateChangedDescr")]
		public event EventHandler CheckStateChanged
		{
			add
			{
				base.Events.AddHandler(CheckBox.EVENT_CHECKSTATECHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(CheckBox.EVENT_CHECKSTATECHANGED, value);
			}
		}

		/// <summary>Creates a new accessibility object for the <see cref="T:System.Windows.Forms.CheckBox" /> control.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.CheckBox.CheckBoxAccessibleObject" /> for the control.</returns>
		// Token: 0x06000CFB RID: 3323 RVA: 0x000251D5 File Offset: 0x000233D5
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new CheckBox.CheckBoxAccessibleObject(this);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.CheckBox.AppearanceChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000CFC RID: 3324 RVA: 0x000251E0 File Offset: 0x000233E0
		protected virtual void OnAppearanceChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[CheckBox.EVENT_APPEARANCECHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.CheckBox.CheckedChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000CFD RID: 3325 RVA: 0x00025210 File Offset: 0x00023410
		protected virtual void OnCheckedChanged(EventArgs e)
		{
			if (base.FlatStyle == FlatStyle.System)
			{
				base.AccessibilityNotifyClients(AccessibleEvents.SystemCaptureStart, -1);
			}
			base.AccessibilityNotifyClients(AccessibleEvents.StateChange, -1);
			base.AccessibilityNotifyClients(AccessibleEvents.NameChange, -1);
			if (base.FlatStyle == FlatStyle.System)
			{
				base.AccessibilityNotifyClients(AccessibleEvents.SystemCaptureEnd, -1);
			}
			EventHandler eventHandler = (EventHandler)base.Events[CheckBox.EVENT_CHECKEDCHANGED];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.CheckBox.CheckStateChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000CFE RID: 3326 RVA: 0x0002527C File Offset: 0x0002347C
		protected virtual void OnCheckStateChanged(EventArgs e)
		{
			if (base.OwnerDraw)
			{
				this.Refresh();
			}
			EventHandler eventHandler = (EventHandler)base.Events[CheckBox.EVENT_CHECKSTATECHANGED];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Click" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000CFF RID: 3327 RVA: 0x000252B8 File Offset: 0x000234B8
		protected override void OnClick(EventArgs e)
		{
			if (this.autoCheck)
			{
				CheckState checkState = this.CheckState;
				if (checkState != CheckState.Unchecked)
				{
					if (checkState != CheckState.Checked)
					{
						this.CheckState = CheckState.Unchecked;
					}
					else if (this.threeState)
					{
						this.CheckState = CheckState.Indeterminate;
						if (this.AccObjDoDefaultAction)
						{
							base.AccessibilityNotifyClients(AccessibleEvents.StateChange, -1);
						}
					}
					else
					{
						this.CheckState = CheckState.Unchecked;
					}
				}
				else
				{
					this.CheckState = CheckState.Checked;
				}
			}
			base.OnClick(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000D00 RID: 3328 RVA: 0x00025322 File Offset: 0x00023522
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			if (base.IsHandleCreated)
			{
				base.SendMessage(241, (int)this.checkState, 0);
			}
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.ButtonBase.OnKeyUp(System.Windows.Forms.KeyEventArgs)" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
		// Token: 0x06000D01 RID: 3329 RVA: 0x00025346 File Offset: 0x00023546
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
		}

		/// <summary>Raises the OnMouseUp event.</summary>
		/// <param name="mevent">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06000D02 RID: 3330 RVA: 0x00025350 File Offset: 0x00023550
		protected override void OnMouseUp(MouseEventArgs mevent)
		{
			if (mevent.Button == MouseButtons.Left && base.MouseIsPressed && base.MouseIsDown)
			{
				Point point = base.PointToScreen(new Point(mevent.X, mevent.Y));
				if (UnsafeNativeMethods.WindowFromPoint(point.X, point.Y) == base.Handle)
				{
					base.ResetFlagsandPaint();
					if (!base.ValidationCancelled)
					{
						if (base.Capture)
						{
							this.OnClick(mevent);
						}
						this.OnMouseClick(mevent);
					}
				}
			}
			base.OnMouseUp(mevent);
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x000253DD File Offset: 0x000235DD
		internal override ButtonBaseAdapter CreateFlatAdapter()
		{
			return new CheckBoxFlatAdapter(this);
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x000253E5 File Offset: 0x000235E5
		internal override ButtonBaseAdapter CreatePopupAdapter()
		{
			return new CheckBoxPopupAdapter(this);
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x000253ED File Offset: 0x000235ED
		internal override ButtonBaseAdapter CreateStandardAdapter()
		{
			return new CheckBoxStandardAdapter(this);
		}

		/// <summary>Processes a mnemonic character.</summary>
		/// <param name="charCode">The character to process.</param>
		/// <returns>
		///   <see langword="true" /> if the character was processed as a mnemonic by the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000D06 RID: 3334 RVA: 0x000253F8 File Offset: 0x000235F8
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected internal override bool ProcessMnemonic(char charCode)
		{
			if (base.UseMnemonic && Control.IsMnemonic(charCode, this.Text) && base.CanSelect)
			{
				if (this.FocusInternal())
				{
					base.ResetFlagsandPaint();
					if (!base.ValidationCancelled)
					{
						this.OnClick(EventArgs.Empty);
					}
				}
				return true;
			}
			return false;
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Windows.Forms.CheckBox" /> control.</summary>
		/// <returns>A string that states the control type and the state of the <see cref="P:System.Windows.Forms.CheckBox.CheckState" /> property.</returns>
		// Token: 0x06000D07 RID: 3335 RVA: 0x00025448 File Offset: 0x00023648
		public override string ToString()
		{
			string text = base.ToString();
			return text + ", CheckState: " + ((int)this.CheckState).ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x04000756 RID: 1878
		private static readonly object EVENT_CHECKEDCHANGED = new object();

		// Token: 0x04000757 RID: 1879
		private static readonly object EVENT_CHECKSTATECHANGED = new object();

		// Token: 0x04000758 RID: 1880
		private static readonly object EVENT_APPEARANCECHANGED = new object();

		// Token: 0x04000759 RID: 1881
		private static readonly ContentAlignment anyRight = (ContentAlignment)1092;

		// Token: 0x0400075A RID: 1882
		private bool autoCheck;

		// Token: 0x0400075B RID: 1883
		private bool threeState;

		// Token: 0x0400075C RID: 1884
		private bool accObjDoDefaultAction;

		// Token: 0x0400075D RID: 1885
		private ContentAlignment checkAlign = ContentAlignment.MiddleLeft;

		// Token: 0x0400075E RID: 1886
		private CheckState checkState;

		// Token: 0x0400075F RID: 1887
		private Appearance appearance;

		// Token: 0x04000760 RID: 1888
		private const int FlatSystemStylePaddingWidth = 25;

		// Token: 0x04000761 RID: 1889
		private const int FlatSystemStyleMinimumHeight = 13;

		// Token: 0x04000762 RID: 1890
		internal int flatSystemStylePaddingWidth = 25;

		// Token: 0x04000763 RID: 1891
		internal int flatSystemStyleMinimumHeight = 13;

		/// <summary>Provides information about the <see cref="T:System.Windows.Forms.CheckBox" /> control to accessibility client applications.</summary>
		// Token: 0x0200061B RID: 1563
		[ComVisible(true)]
		public class CheckBoxAccessibleObject : ButtonBase.ButtonBaseAccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.CheckBox.CheckBoxAccessibleObject" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.CheckBox" /> that owns the <see cref="T:System.Windows.Forms.CheckBox.CheckBoxAccessibleObject" />.</param>
			// Token: 0x060062F5 RID: 25333 RVA: 0x0016E0E9 File Offset: 0x0016C2E9
			public CheckBoxAccessibleObject(Control owner)
				: base(owner)
			{
			}

			/// <summary>Gets a string that describes the default action of the <see cref="T:System.Windows.Forms.CheckBox" /> control.</summary>
			/// <returns>The description of the default action of the <see cref="T:System.Windows.Forms.CheckBox" /> control.</returns>
			// Token: 0x17001517 RID: 5399
			// (get) Token: 0x060062F6 RID: 25334 RVA: 0x0016E0F4 File Offset: 0x0016C2F4
			public override string DefaultAction
			{
				get
				{
					string accessibleDefaultActionDescription = base.Owner.AccessibleDefaultActionDescription;
					if (accessibleDefaultActionDescription != null)
					{
						return accessibleDefaultActionDescription;
					}
					if (((CheckBox)base.Owner).Checked)
					{
						return SR.GetString("AccessibleActionUncheck");
					}
					return SR.GetString("AccessibleActionCheck");
				}
			}

			/// <summary>Gets the role of this accessible object.</summary>
			/// <returns>The <see cref="F:System.Windows.Forms.AccessibleRole.CheckButton" /> value.</returns>
			// Token: 0x17001518 RID: 5400
			// (get) Token: 0x060062F7 RID: 25335 RVA: 0x0016E13C File Offset: 0x0016C33C
			public override AccessibleRole Role
			{
				get
				{
					AccessibleRole accessibleRole = base.Owner.AccessibleRole;
					if (accessibleRole != AccessibleRole.Default)
					{
						return accessibleRole;
					}
					return AccessibleRole.CheckButton;
				}
			}

			/// <summary>Gets the state of the <see cref="T:System.Windows.Forms.CheckBox" /> control.</summary>
			/// <returns>One of the <see cref="T:System.Windows.Forms.AccessibleStates" /> values. If the <see cref="P:System.Windows.Forms.CheckBox.CheckState" /> property is set to <see cref="F:System.Windows.Forms.CheckState.Checked" />, this property returns <see cref="F:System.Windows.Forms.AccessibleStates.Checked" />. If <see cref="P:System.Windows.Forms.CheckBox.CheckState" /> is set to <see cref="F:System.Windows.Forms.CheckState.Indeterminate" />, this property returns <see cref="F:System.Windows.Forms.AccessibleStates.Indeterminate" />.</returns>
			// Token: 0x17001519 RID: 5401
			// (get) Token: 0x060062F8 RID: 25336 RVA: 0x0016E160 File Offset: 0x0016C360
			public override AccessibleStates State
			{
				get
				{
					CheckState checkState = ((CheckBox)base.Owner).CheckState;
					if (checkState == CheckState.Checked)
					{
						return AccessibleStates.Checked | base.State;
					}
					if (checkState != CheckState.Indeterminate)
					{
						return base.State;
					}
					return AccessibleStates.Mixed | base.State;
				}
			}

			/// <summary>Performs the default action associated with this accessible object.</summary>
			// Token: 0x060062F9 RID: 25337 RVA: 0x0016E1A4 File Offset: 0x0016C3A4
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void DoDefaultAction()
			{
				CheckBox checkBox = base.Owner as CheckBox;
				if (checkBox != null)
				{
					checkBox.AccObjDoDefaultAction = true;
				}
				try
				{
					base.DoDefaultAction();
				}
				finally
				{
					if (checkBox != null)
					{
						checkBox.AccObjDoDefaultAction = false;
					}
				}
			}
		}
	}
}
