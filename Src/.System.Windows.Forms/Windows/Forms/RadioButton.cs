using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.ButtonInternal;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Enables the user to select a single option from a group of choices when paired with other <see cref="T:System.Windows.Forms.RadioButton" /> controls.</summary>
	// Token: 0x0200033C RID: 828
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultProperty("Checked")]
	[DefaultEvent("CheckedChanged")]
	[DefaultBindingProperty("Checked")]
	[ToolboxItem("System.Windows.Forms.Design.AutoSizeToolboxItem,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Designer("System.Windows.Forms.Design.RadioButtonDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionRadioButton")]
	public class RadioButton : ButtonBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.RadioButton" /> class.</summary>
		// Token: 0x06003583 RID: 13699 RVA: 0x000F26AC File Offset: 0x000F08AC
		public RadioButton()
		{
			if (DpiHelper.EnableDpiChangedHighDpiImprovements)
			{
				this.flatSystemStylePaddingWidth = base.LogicalToDeviceUnits(24);
				this.flatSystemStyleMinimumHeight = base.LogicalToDeviceUnits(13);
			}
			base.SetStyle(ControlStyles.StandardClick, false);
			this.TextAlign = ContentAlignment.MiddleLeft;
			this.TabStop = false;
			base.SetAutoSizeMode(AutoSizeMode.GrowAndShrink);
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="P:System.Windows.Forms.RadioButton.Checked" /> value and the appearance of the control automatically change when the control is clicked.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Windows.Forms.RadioButton.Checked" /> value and the appearance of the control automatically change on the <see cref="E:System.Windows.Forms.Control.Click" /> event; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x06003584 RID: 13700 RVA: 0x000F272A File Offset: 0x000F092A
		// (set) Token: 0x06003585 RID: 13701 RVA: 0x000F2732 File Offset: 0x000F0932
		[DefaultValue(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("RadioButtonAutoCheckDescr")]
		public bool AutoCheck
		{
			get
			{
				return this.autoCheck;
			}
			set
			{
				if (this.autoCheck != value)
				{
					this.autoCheck = value;
					this.PerformAutoUpdates(false);
				}
			}
		}

		/// <summary>Gets or sets a value determining the appearance of the <see cref="T:System.Windows.Forms.RadioButton" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.Appearance" /> values. The default value is <see cref="F:System.Windows.Forms.Appearance.Normal" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.Appearance" /> values.</exception>
		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x06003586 RID: 13702 RVA: 0x000F274B File Offset: 0x000F094B
		// (set) Token: 0x06003587 RID: 13703 RVA: 0x000F2754 File Offset: 0x000F0954
		[DefaultValue(Appearance.Normal)]
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[SRDescription("RadioButtonAppearanceDescr")]
		public Appearance Appearance
		{
			get
			{
				return this.appearance;
			}
			set
			{
				if (this.appearance != value)
				{
					if (!ClientUtils.IsEnumValid(value, (int)value, 0, 1))
					{
						throw new InvalidEnumArgumentException("value", (int)value, typeof(Appearance));
					}
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.RadioButton.Appearance" /> property value changes.</summary>
		// Token: 0x14000285 RID: 645
		// (add) Token: 0x06003588 RID: 13704 RVA: 0x000F27EC File Offset: 0x000F09EC
		// (remove) Token: 0x06003589 RID: 13705 RVA: 0x000F27FF File Offset: 0x000F09FF
		[SRCategory("CatPropertyChanged")]
		[SRDescription("RadioButtonOnAppearanceChangedDescr")]
		public event EventHandler AppearanceChanged
		{
			add
			{
				base.Events.AddHandler(RadioButton.EVENT_APPEARANCECHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadioButton.EVENT_APPEARANCECHANGED, value);
			}
		}

		/// <summary>Gets or sets the location of the check box portion of the <see cref="T:System.Windows.Forms.RadioButton" />.</summary>
		/// <returns>One of the valid <see cref="T:System.Drawing.ContentAlignment" /> values. The default value is <see cref="F:System.Drawing.ContentAlignment.MiddleLeft" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Drawing.ContentAlignment" /> values.</exception>
		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x0600358A RID: 13706 RVA: 0x000F2812 File Offset: 0x000F0A12
		// (set) Token: 0x0600358B RID: 13707 RVA: 0x000F281A File Offset: 0x000F0A1A
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[DefaultValue(ContentAlignment.MiddleLeft)]
		[SRDescription("RadioButtonCheckAlignDescr")]
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
				this.checkAlign = value;
				if (base.OwnerDraw)
				{
					base.Invalidate();
					return;
				}
				base.UpdateStyles();
			}
		}

		/// <summary>Gets or sets a value indicating whether the control is checked.</summary>
		/// <returns>
		///   <see langword="true" /> if the check box is checked; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x0600358C RID: 13708 RVA: 0x000F2856 File Offset: 0x000F0A56
		// (set) Token: 0x0600358D RID: 13709 RVA: 0x000F2860 File Offset: 0x000F0A60
		[Bindable(true)]
		[SettingsBindable(true)]
		[DefaultValue(false)]
		[SRCategory("CatAppearance")]
		[SRDescription("RadioButtonCheckedDescr")]
		public bool Checked
		{
			get
			{
				return this.isChecked;
			}
			set
			{
				if (this.isChecked != value)
				{
					this.isChecked = value;
					if (base.IsHandleCreated)
					{
						base.SendMessage(241, value ? 1 : 0, 0);
					}
					base.Invalidate();
					base.Update();
					this.PerformAutoUpdates(false);
					this.OnCheckedChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.RadioButton" /> control.</summary>
		// Token: 0x14000286 RID: 646
		// (add) Token: 0x0600358E RID: 13710 RVA: 0x00023757 File Offset: 0x00021957
		// (remove) Token: 0x0600358F RID: 13711 RVA: 0x00023760 File Offset: 0x00021960
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

		/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.RadioButton" /> control with the mouse.</summary>
		// Token: 0x14000287 RID: 647
		// (add) Token: 0x06003590 RID: 13712 RVA: 0x00023769 File Offset: 0x00021969
		// (remove) Token: 0x06003591 RID: 13713 RVA: 0x00023772 File Offset: 0x00021972
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
		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x06003592 RID: 13714 RVA: 0x000F28B8 File Offset: 0x000F0AB8
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
					createParams.Style |= 4;
					if (this.Appearance == Appearance.Button)
					{
						createParams.Style |= 4096;
					}
					ContentAlignment contentAlignment = base.RtlTranslateContent(this.CheckAlign);
					if ((contentAlignment & RadioButton.anyRight) != (ContentAlignment)0)
					{
						createParams.Style |= 32;
					}
				}
				return createParams;
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>Returns a <see cref="T:System.Drawing.Size" /> with a <see cref="P:System.Drawing.Size.Width" /> of 104 and a <see cref="P:System.Drawing.Size.Height" /> of 24.</returns>
		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x06003593 RID: 13715 RVA: 0x0002501F File Offset: 0x0002321F
		protected override Size DefaultSize
		{
			get
			{
				return new Size(104, 24);
			}
		}

		/// <summary>Provides constants for rescaling the <see cref="T:System.Windows.Forms.RadioButton" /> control when a DPI change occurs.</summary>
		/// <param name="deviceDpiOld">The DPI value prior to the change.</param>
		/// <param name="deviceDpiNew">The DPI value after the change.</param>
		// Token: 0x06003594 RID: 13716 RVA: 0x000F293F File Offset: 0x000F0B3F
		protected override void RescaleConstantsForDpi(int deviceDpiOld, int deviceDpiNew)
		{
			base.RescaleConstantsForDpi(deviceDpiOld, deviceDpiNew);
			if (DpiHelper.EnableDpiChangedHighDpiImprovements)
			{
				this.flatSystemStylePaddingWidth = base.LogicalToDeviceUnits(24);
				this.flatSystemStyleMinimumHeight = base.LogicalToDeviceUnits(13);
			}
		}

		// Token: 0x06003595 RID: 13717 RVA: 0x000F296C File Offset: 0x000F0B6C
		internal override Size GetPreferredSizeCore(Size proposedConstraints)
		{
			if (base.FlatStyle != FlatStyle.System)
			{
				return base.GetPreferredSizeCore(proposedConstraints);
			}
			Size size = TextRenderer.MeasureText(this.Text, this.Font);
			Size size2 = this.SizeFromClientSize(size);
			size2.Width += this.flatSystemStylePaddingWidth;
			size2.Height = (DpiHelper.EnableDpiChangedHighDpiImprovements ? Math.Max(size2.Height + 5, this.flatSystemStyleMinimumHeight) : (size2.Height + 5));
			return size2;
		}

		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x06003596 RID: 13718 RVA: 0x000F29E6 File Offset: 0x000F0BE6
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

		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x06003597 RID: 13719 RVA: 0x000F2A20 File Offset: 0x000F0C20
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

		/// <summary>Gets or sets a value indicating whether the user can give the focus to this control using the TAB key.</summary>
		/// <returns>
		///   <see langword="true" /> if the user can give focus to this control using the TAB key; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x06003598 RID: 13720 RVA: 0x000B2395 File Offset: 0x000B0595
		// (set) Token: 0x06003599 RID: 13721 RVA: 0x000B239D File Offset: 0x000B059D
		[DefaultValue(false)]
		public new bool TabStop
		{
			get
			{
				return base.TabStop;
			}
			set
			{
				base.TabStop = value;
			}
		}

		/// <summary>Gets or sets the alignment of the text on the <see cref="T:System.Windows.Forms.RadioButton" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values. The default is <see cref="F:System.Drawing.ContentAlignment.MiddleLeft" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Drawing.ContentAlignment" /> values.</exception>
		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x0600359A RID: 13722 RVA: 0x00025167 File Offset: 0x00023367
		// (set) Token: 0x0600359B RID: 13723 RVA: 0x0002516F File Offset: 0x0002336F
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.RadioButton.Checked" /> property changes.</summary>
		// Token: 0x14000288 RID: 648
		// (add) Token: 0x0600359C RID: 13724 RVA: 0x000F2A50 File Offset: 0x000F0C50
		// (remove) Token: 0x0600359D RID: 13725 RVA: 0x000F2A63 File Offset: 0x000F0C63
		[SRDescription("RadioButtonOnCheckedChangedDescr")]
		public event EventHandler CheckedChanged
		{
			add
			{
				base.Events.AddHandler(RadioButton.EVENT_CHECKEDCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadioButton.EVENT_CHECKEDCHANGED, value);
			}
		}

		/// <summary>Creates a new accessibility object for the <see cref="T:System.Windows.Forms.RadioButton" /> control.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.RadioButton.RadioButtonAccessibleObject" /> for the control.</returns>
		// Token: 0x0600359E RID: 13726 RVA: 0x000F2A76 File Offset: 0x000F0C76
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new RadioButton.RadioButtonAccessibleObject(this);
		}

		/// <summary>Overrides the <see cref="M:System.Windows.Forms.Control.OnHandleCreated(System.EventArgs)" /> method.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600359F RID: 13727 RVA: 0x000F2A7E File Offset: 0x000F0C7E
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			if (base.IsHandleCreated)
			{
				base.SendMessage(241, this.isChecked ? 1 : 0, 0);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.CheckBox.CheckedChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060035A0 RID: 13728 RVA: 0x000F2AA8 File Offset: 0x000F0CA8
		protected virtual void OnCheckedChanged(EventArgs e)
		{
			base.AccessibilityNotifyClients(AccessibleEvents.StateChange, -1);
			base.AccessibilityNotifyClients(AccessibleEvents.NameChange, -1);
			EventHandler eventHandler = (EventHandler)base.Events[RadioButton.EVENT_CHECKEDCHANGED];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Click" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060035A1 RID: 13729 RVA: 0x000F2AEE File Offset: 0x000F0CEE
		protected override void OnClick(EventArgs e)
		{
			if (this.autoCheck)
			{
				this.Checked = true;
			}
			base.OnClick(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Enter" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060035A2 RID: 13730 RVA: 0x000F2B06 File Offset: 0x000F0D06
		protected override void OnEnter(EventArgs e)
		{
			if (Control.MouseButtons == MouseButtons.None)
			{
				if (UnsafeNativeMethods.GetKeyState(9) >= 0)
				{
					base.ResetFlagsandPaint();
					if (!base.ValidationCancelled)
					{
						this.OnClick(e);
					}
				}
				else
				{
					this.PerformAutoUpdates(true);
					this.TabStop = true;
				}
			}
			base.OnEnter(e);
		}

		// Token: 0x060035A3 RID: 13731 RVA: 0x000F2B48 File Offset: 0x000F0D48
		private void PerformAutoUpdates(bool tabbedInto)
		{
			if (this.autoCheck)
			{
				if (this.firstfocus)
				{
					this.WipeTabStops(tabbedInto);
				}
				this.TabStop = this.isChecked;
				if (this.isChecked)
				{
					Control parentInternal = this.ParentInternal;
					if (parentInternal != null)
					{
						Control.ControlCollection controls = parentInternal.Controls;
						for (int i = 0; i < controls.Count; i++)
						{
							Control control = controls[i];
							if (control != this && control is RadioButton)
							{
								RadioButton radioButton = (RadioButton)control;
								if (radioButton.autoCheck && radioButton.Checked)
								{
									PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this)["Checked"];
									propertyDescriptor.SetValue(radioButton, false);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060035A4 RID: 13732 RVA: 0x000F2BF4 File Offset: 0x000F0DF4
		private void WipeTabStops(bool tabbedInto)
		{
			Control parentInternal = this.ParentInternal;
			if (parentInternal != null)
			{
				Control.ControlCollection controls = parentInternal.Controls;
				for (int i = 0; i < controls.Count; i++)
				{
					Control control = controls[i];
					if (control is RadioButton)
					{
						RadioButton radioButton = (RadioButton)control;
						if (!tabbedInto)
						{
							radioButton.firstfocus = false;
						}
						if (radioButton.autoCheck)
						{
							radioButton.TabStop = false;
						}
					}
				}
			}
		}

		// Token: 0x060035A5 RID: 13733 RVA: 0x000F2C57 File Offset: 0x000F0E57
		internal override ButtonBaseAdapter CreateFlatAdapter()
		{
			return new RadioButtonFlatAdapter(this);
		}

		// Token: 0x060035A6 RID: 13734 RVA: 0x000F2C5F File Offset: 0x000F0E5F
		internal override ButtonBaseAdapter CreatePopupAdapter()
		{
			return new RadioButtonPopupAdapter(this);
		}

		// Token: 0x060035A7 RID: 13735 RVA: 0x000F2C67 File Offset: 0x000F0E67
		internal override ButtonBaseAdapter CreateStandardAdapter()
		{
			return new RadioButtonStandardAdapter(this);
		}

		// Token: 0x060035A8 RID: 13736 RVA: 0x000F2C70 File Offset: 0x000F0E70
		private void OnAppearanceChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[RadioButton.EVENT_APPEARANCECHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.</summary>
		/// <param name="mevent">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x060035A9 RID: 13737 RVA: 0x000F2CA0 File Offset: 0x000F0EA0
		protected override void OnMouseUp(MouseEventArgs mevent)
		{
			if (mevent.Button == MouseButtons.Left && base.GetStyle(ControlStyles.UserPaint) && base.MouseIsDown)
			{
				Point point = base.PointToScreen(new Point(mevent.X, mevent.Y));
				if (UnsafeNativeMethods.WindowFromPoint(point.X, point.Y) == base.Handle)
				{
					base.ResetFlagsandPaint();
					if (!base.ValidationCancelled)
					{
						this.OnClick(mevent);
						this.OnMouseClick(mevent);
					}
				}
			}
			base.OnMouseUp(mevent);
		}

		/// <summary>Generates a <see cref="E:System.Windows.Forms.Control.Click" /> event for the control, simulating a click by a user.</summary>
		// Token: 0x060035AA RID: 13738 RVA: 0x000F2D26 File Offset: 0x000F0F26
		public void PerformClick()
		{
			if (base.CanSelect)
			{
				base.ResetFlagsandPaint();
				if (!base.ValidationCancelled)
				{
					this.OnClick(EventArgs.Empty);
				}
			}
		}

		/// <summary>Overrides the <see cref="M:System.Windows.Forms.Control.ProcessMnemonic(System.Char)" /> method.</summary>
		/// <param name="charCode">The character to process.</param>
		/// <returns>
		///   <see langword="true" /> if the character was successfully processed; otherwise, <see langword="false" />.</returns>
		// Token: 0x060035AB RID: 13739 RVA: 0x000F2D49 File Offset: 0x000F0F49
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected internal override bool ProcessMnemonic(char charCode)
		{
			if (base.UseMnemonic && Control.IsMnemonic(charCode, this.Text) && base.CanSelect)
			{
				if (!this.Focused)
				{
					this.FocusInternal();
				}
				else
				{
					this.PerformClick();
				}
				return true;
			}
			return false;
		}

		/// <summary>Overrides the <see cref="M:System.ComponentModel.Component.ToString" /> method.</summary>
		/// <returns>A string representation of the <see cref="T:System.Windows.Forms.RadioButton" /> that indicates whether it is checked.</returns>
		// Token: 0x060035AC RID: 13740 RVA: 0x000F2D84 File Offset: 0x000F0F84
		public override string ToString()
		{
			string text = base.ToString();
			return text + ", Checked: " + this.Checked.ToString();
		}

		// Token: 0x04001F4F RID: 8015
		private static readonly object EVENT_CHECKEDCHANGED = new object();

		// Token: 0x04001F50 RID: 8016
		private static readonly ContentAlignment anyRight = (ContentAlignment)1092;

		// Token: 0x04001F51 RID: 8017
		private bool firstfocus = true;

		// Token: 0x04001F52 RID: 8018
		private bool isChecked;

		// Token: 0x04001F53 RID: 8019
		private bool autoCheck = true;

		// Token: 0x04001F54 RID: 8020
		private ContentAlignment checkAlign = ContentAlignment.MiddleLeft;

		// Token: 0x04001F55 RID: 8021
		private Appearance appearance;

		// Token: 0x04001F56 RID: 8022
		private const int FlatSystemStylePaddingWidth = 24;

		// Token: 0x04001F57 RID: 8023
		private const int FlatSystemStyleMinimumHeight = 13;

		// Token: 0x04001F58 RID: 8024
		internal int flatSystemStylePaddingWidth = 24;

		// Token: 0x04001F59 RID: 8025
		internal int flatSystemStyleMinimumHeight = 13;

		// Token: 0x04001F5A RID: 8026
		private static readonly object EVENT_APPEARANCECHANGED = new object();

		/// <summary>Provides information about the <see cref="T:System.Windows.Forms.RadioButton" /> control to accessibility client applications.</summary>
		// Token: 0x020007D8 RID: 2008
		[ComVisible(true)]
		public class RadioButtonAccessibleObject : ButtonBase.ButtonBaseAccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.RadioButton.RadioButtonAccessibleObject" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.RadioButton" /> that this object provides information for.</param>
			// Token: 0x06006D82 RID: 28034 RVA: 0x0016E0E9 File Offset: 0x0016C2E9
			public RadioButtonAccessibleObject(RadioButton owner)
				: base(owner)
			{
			}

			/// <summary>Gets a string that describes the default action of the <see cref="T:System.Windows.Forms.RadioButton" /> control.</summary>
			/// <returns>A description of the default action of the <see cref="T:System.Windows.Forms.RadioButton" /> control.</returns>
			// Token: 0x170017F3 RID: 6131
			// (get) Token: 0x06006D83 RID: 28035 RVA: 0x00191918 File Offset: 0x0018FB18
			public override string DefaultAction
			{
				get
				{
					string accessibleDefaultActionDescription = base.Owner.AccessibleDefaultActionDescription;
					if (accessibleDefaultActionDescription != null)
					{
						return accessibleDefaultActionDescription;
					}
					return SR.GetString("AccessibleActionCheck");
				}
			}

			/// <summary>Gets the role of this accessible object.</summary>
			/// <returns>The <see cref="F:System.Windows.Forms.AccessibleRole.RadioButton" /> value.</returns>
			// Token: 0x170017F4 RID: 6132
			// (get) Token: 0x06006D84 RID: 28036 RVA: 0x00191940 File Offset: 0x0018FB40
			public override AccessibleRole Role
			{
				get
				{
					AccessibleRole accessibleRole = base.Owner.AccessibleRole;
					if (accessibleRole != AccessibleRole.Default)
					{
						return accessibleRole;
					}
					return AccessibleRole.RadioButton;
				}
			}

			/// <summary>Gets the state of the <see cref="T:System.Windows.Forms.RadioButton" /> control.</summary>
			/// <returns>If the <see cref="P:System.Windows.Forms.RadioButton.Checked" /> property is set to true, returns <see cref="F:System.Windows.Forms.AccessibleStates.Checked" />.</returns>
			// Token: 0x170017F5 RID: 6133
			// (get) Token: 0x06006D85 RID: 28037 RVA: 0x00191961 File Offset: 0x0018FB61
			public override AccessibleStates State
			{
				get
				{
					if (((RadioButton)base.Owner).Checked)
					{
						return AccessibleStates.Checked | base.State;
					}
					return base.State;
				}
			}

			/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Click" /> event.</summary>
			// Token: 0x06006D86 RID: 28038 RVA: 0x00191985 File Offset: 0x0018FB85
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void DoDefaultAction()
			{
				((RadioButton)base.Owner).PerformClick();
			}
		}
	}
}
