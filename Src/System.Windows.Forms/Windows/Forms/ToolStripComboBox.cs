using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design;

namespace System.Windows.Forms
{
	/// <summary>Represents a <see cref="T:System.Windows.Forms.ToolStripComboBox" /> that is properly rendered in a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
	// Token: 0x020003B5 RID: 949
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.MenuStrip | ToolStripItemDesignerAvailability.ContextMenuStrip)]
	[DefaultProperty("Items")]
	public class ToolStripComboBox : ToolStripControlHost
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> class.</summary>
		// Token: 0x06003F08 RID: 16136 RVA: 0x00111114 File Offset: 0x0010F314
		public ToolStripComboBox()
			: base(ToolStripComboBox.CreateControlInstance())
		{
			ToolStripComboBox.ToolStripComboBoxControl toolStripComboBoxControl = base.Control as ToolStripComboBox.ToolStripComboBoxControl;
			toolStripComboBoxControl.Owner = this;
			if (DpiHelper.EnableToolStripHighDpiImprovements)
			{
				this.scaledPadding = DpiHelper.LogicalToDeviceUnits(ToolStripComboBox.padding, 0);
				this.scaledDropDownPadding = DpiHelper.LogicalToDeviceUnits(ToolStripComboBox.dropDownPadding, 0);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> class with the specified name.</summary>
		/// <param name="name">The name of the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</param>
		// Token: 0x06003F09 RID: 16137 RVA: 0x0011117E File Offset: 0x0010F37E
		public ToolStripComboBox(string name)
			: this()
		{
			base.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> class derived from a base control.</summary>
		/// <param name="c">The base control.</param>
		/// <exception cref="T:System.NotSupportedException">The operation is not supported.</exception>
		// Token: 0x06003F0A RID: 16138 RVA: 0x0011118D File Offset: 0x0010F38D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ToolStripComboBox(Control c)
			: base(c)
		{
			throw new NotSupportedException(SR.GetString("ToolStripMustSupplyItsOwnComboBox"));
		}

		/// <summary>Creates a new accessibility object for the control.</summary>
		/// <returns>A new accessibility object for the control.</returns>
		// Token: 0x06003F0B RID: 16139 RVA: 0x001111BB File Offset: 0x0010F3BB
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level3)
			{
				return new ToolStripComboBox.ToolStripComboBoxAccessibleObject(this);
			}
			return base.CreateAccessibilityInstance();
		}

		// Token: 0x06003F0C RID: 16140 RVA: 0x001111D4 File Offset: 0x0010F3D4
		private static Control CreateControlInstance()
		{
			return new ToolStripComboBox.ToolStripComboBoxControl
			{
				FlatStyle = FlatStyle.Popup,
				Font = ToolStripManager.DefaultFont
			};
		}

		/// <summary>Gets or sets the custom string collection to use when the <see cref="P:System.Windows.Forms.ToolStripComboBox.AutoCompleteSource" /> property is set to <see cref="F:System.Windows.Forms.AutoCompleteSource.CustomSource" />.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AutoCompleteStringCollection" /> that contains the strings.</returns>
		// Token: 0x17000F6C RID: 3948
		// (get) Token: 0x06003F0D RID: 16141 RVA: 0x001111FA File Offset: 0x0010F3FA
		// (set) Token: 0x06003F0E RID: 16142 RVA: 0x00111207 File Offset: 0x0010F407
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Localizable(true)]
		[SRDescription("ComboBoxAutoCompleteCustomSourceDescr")]
		[Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public AutoCompleteStringCollection AutoCompleteCustomSource
		{
			get
			{
				return this.ComboBox.AutoCompleteCustomSource;
			}
			set
			{
				this.ComboBox.AutoCompleteCustomSource = value;
			}
		}

		/// <summary>Gets or sets a value that indicates the text completion behavior of the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AutoCompleteMode" /> values. The default is <see cref="F:System.Windows.Forms.AutoCompleteMode.None" />.</returns>
		// Token: 0x17000F6D RID: 3949
		// (get) Token: 0x06003F0F RID: 16143 RVA: 0x00111215 File Offset: 0x0010F415
		// (set) Token: 0x06003F10 RID: 16144 RVA: 0x00111222 File Offset: 0x0010F422
		[DefaultValue(AutoCompleteMode.None)]
		[SRDescription("ComboBoxAutoCompleteModeDescr")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public AutoCompleteMode AutoCompleteMode
		{
			get
			{
				return this.ComboBox.AutoCompleteMode;
			}
			set
			{
				this.ComboBox.AutoCompleteMode = value;
			}
		}

		/// <summary>Gets or sets the source of complete strings used for automatic completion.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AutoCompleteSource" /> values. The default is <see cref="F:System.Windows.Forms.AutoCompleteSource.None" />.</returns>
		// Token: 0x17000F6E RID: 3950
		// (get) Token: 0x06003F11 RID: 16145 RVA: 0x00111230 File Offset: 0x0010F430
		// (set) Token: 0x06003F12 RID: 16146 RVA: 0x0011123D File Offset: 0x0010F43D
		[DefaultValue(AutoCompleteSource.None)]
		[SRDescription("ComboBoxAutoCompleteSourceDescr")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public AutoCompleteSource AutoCompleteSource
		{
			get
			{
				return this.ComboBox.AutoCompleteSource;
			}
			set
			{
				this.ComboBox.AutoCompleteSource = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>The background image displayed in the control.</returns>
		// Token: 0x17000F6F RID: 3951
		// (get) Token: 0x06003F13 RID: 16147 RVA: 0x00010F8F File Offset: 0x0000F18F
		// (set) Token: 0x06003F14 RID: 16148 RVA: 0x00010F97 File Offset: 0x0000F197
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
		/// <returns>One of the values of <see cref="T:System.Windows.Forms.ImageLayout" />.</returns>
		// Token: 0x17000F70 RID: 3952
		// (get) Token: 0x06003F15 RID: 16149 RVA: 0x00010FA0 File Offset: 0x0000F1A0
		// (set) Token: 0x06003F16 RID: 16150 RVA: 0x00010FA8 File Offset: 0x0000F1A8
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

		/// <summary>Gets a <see cref="T:System.Windows.Forms.ComboBox" /> in which the user can enter text, along with a list from which the user can select.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ComboBox" /> for a <see cref="T:System.Windows.Forms.ToolStrip" />.</returns>
		// Token: 0x17000F71 RID: 3953
		// (get) Token: 0x06003F17 RID: 16151 RVA: 0x0011124B File Offset: 0x0010F44B
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ComboBox ComboBox
		{
			get
			{
				return base.Control as ComboBox;
			}
		}

		/// <summary>Gets the default size of the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> in pixels. The default size is 100 x 20 pixels.</returns>
		// Token: 0x17000F72 RID: 3954
		// (get) Token: 0x06003F18 RID: 16152 RVA: 0x00010FC8 File Offset: 0x0000F1C8
		protected override Size DefaultSize
		{
			get
			{
				return new Size(100, 22);
			}
		}

		/// <summary>Gets the default spacing, in pixels, between the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> and an adjacent item.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value.</returns>
		// Token: 0x17000F73 RID: 3955
		// (get) Token: 0x06003F19 RID: 16153 RVA: 0x00111258 File Offset: 0x0010F458
		protected internal override Padding DefaultMargin
		{
			get
			{
				if (base.IsOnDropDown)
				{
					return this.scaledDropDownPadding;
				}
				return this.scaledPadding;
			}
		}

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x14000307 RID: 775
		// (add) Token: 0x06003F1A RID: 16154 RVA: 0x0011126F File Offset: 0x0010F46F
		// (remove) Token: 0x06003F1B RID: 16155 RVA: 0x00111278 File Offset: 0x0010F478
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

		/// <summary>Occurs when the drop-down portion of a <see cref="T:System.Windows.Forms.ToolStripComboBox" /> is shown.</summary>
		// Token: 0x14000308 RID: 776
		// (add) Token: 0x06003F1C RID: 16156 RVA: 0x00111281 File Offset: 0x0010F481
		// (remove) Token: 0x06003F1D RID: 16157 RVA: 0x00111294 File Offset: 0x0010F494
		[SRCategory("CatBehavior")]
		[SRDescription("ComboBoxOnDropDownDescr")]
		public event EventHandler DropDown
		{
			add
			{
				base.Events.AddHandler(ToolStripComboBox.EventDropDown, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripComboBox.EventDropDown, value);
			}
		}

		/// <summary>Occurs when the drop-down portion of the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> has closed.</summary>
		// Token: 0x14000309 RID: 777
		// (add) Token: 0x06003F1E RID: 16158 RVA: 0x001112A7 File Offset: 0x0010F4A7
		// (remove) Token: 0x06003F1F RID: 16159 RVA: 0x001112BA File Offset: 0x0010F4BA
		[SRCategory("CatBehavior")]
		[SRDescription("ComboBoxOnDropDownClosedDescr")]
		public event EventHandler DropDownClosed
		{
			add
			{
				base.Events.AddHandler(ToolStripComboBox.EventDropDownClosed, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripComboBox.EventDropDownClosed, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolStripComboBox.DropDownStyle" /> property has changed.</summary>
		// Token: 0x1400030A RID: 778
		// (add) Token: 0x06003F20 RID: 16160 RVA: 0x001112CD File Offset: 0x0010F4CD
		// (remove) Token: 0x06003F21 RID: 16161 RVA: 0x001112E0 File Offset: 0x0010F4E0
		[SRCategory("CatBehavior")]
		[SRDescription("ComboBoxDropDownStyleChangedDescr")]
		public event EventHandler DropDownStyleChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripComboBox.EventDropDownStyleChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripComboBox.EventDropDownStyleChanged, value);
			}
		}

		/// <summary>Gets or sets the height, in pixels, of the drop-down portion box of a <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
		/// <returns>The height, in pixels, of the drop-down box.</returns>
		// Token: 0x17000F74 RID: 3956
		// (get) Token: 0x06003F22 RID: 16162 RVA: 0x001112F3 File Offset: 0x0010F4F3
		// (set) Token: 0x06003F23 RID: 16163 RVA: 0x00111300 File Offset: 0x0010F500
		[SRCategory("CatBehavior")]
		[SRDescription("ComboBoxDropDownHeightDescr")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DefaultValue(106)]
		public int DropDownHeight
		{
			get
			{
				return this.ComboBox.DropDownHeight;
			}
			set
			{
				this.ComboBox.DropDownHeight = value;
			}
		}

		/// <summary>Gets or sets a value specifying the style of the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ComboBoxStyle" /> values. The default is <see cref="F:System.Windows.Forms.ComboBoxStyle.DropDown" />.</returns>
		// Token: 0x17000F75 RID: 3957
		// (get) Token: 0x06003F24 RID: 16164 RVA: 0x0011130E File Offset: 0x0010F50E
		// (set) Token: 0x06003F25 RID: 16165 RVA: 0x0011131B File Offset: 0x0010F51B
		[SRCategory("CatAppearance")]
		[DefaultValue(ComboBoxStyle.DropDown)]
		[SRDescription("ComboBoxStyleDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public ComboBoxStyle DropDownStyle
		{
			get
			{
				return this.ComboBox.DropDownStyle;
			}
			set
			{
				this.ComboBox.DropDownStyle = value;
			}
		}

		/// <summary>Gets or sets the width, in pixels, of the drop-down portion of a <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
		/// <returns>The width, in pixels, of the drop-down box.</returns>
		// Token: 0x17000F76 RID: 3958
		// (get) Token: 0x06003F26 RID: 16166 RVA: 0x00111329 File Offset: 0x0010F529
		// (set) Token: 0x06003F27 RID: 16167 RVA: 0x00111336 File Offset: 0x0010F536
		[SRCategory("CatBehavior")]
		[SRDescription("ComboBoxDropDownWidthDescr")]
		public int DropDownWidth
		{
			get
			{
				return this.ComboBox.DropDownWidth;
			}
			set
			{
				this.ComboBox.DropDownWidth = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> currently displays its drop-down portion.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> currently displays its drop-down portion; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000F77 RID: 3959
		// (get) Token: 0x06003F28 RID: 16168 RVA: 0x00111344 File Offset: 0x0010F544
		// (set) Token: 0x06003F29 RID: 16169 RVA: 0x00111351 File Offset: 0x0010F551
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ComboBoxDroppedDownDescr")]
		public bool DroppedDown
		{
			get
			{
				return this.ComboBox.DroppedDown;
			}
			set
			{
				this.ComboBox.DroppedDown = value;
			}
		}

		/// <summary>Gets or sets the appearance of the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
		/// <returns>One of the values of <see cref="T:System.Windows.Forms.FlatStyle" />. The options are <see cref="F:System.Windows.Forms.FlatStyle.Flat" />, <see cref="F:System.Windows.Forms.FlatStyle.Popup" />, <see cref="F:System.Windows.Forms.FlatStyle.Standard" />, and <see cref="F:System.Windows.Forms.FlatStyle.System" />. The default is <see cref="F:System.Windows.Forms.FlatStyle.Popup" />.</returns>
		// Token: 0x17000F78 RID: 3960
		// (get) Token: 0x06003F2A RID: 16170 RVA: 0x0011135F File Offset: 0x0010F55F
		// (set) Token: 0x06003F2B RID: 16171 RVA: 0x0011136C File Offset: 0x0010F56C
		[SRCategory("CatAppearance")]
		[DefaultValue(FlatStyle.Popup)]
		[Localizable(true)]
		[SRDescription("ComboBoxFlatStyleDescr")]
		public FlatStyle FlatStyle
		{
			get
			{
				return this.ComboBox.FlatStyle;
			}
			set
			{
				this.ComboBox.FlatStyle = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> should resize to avoid showing partial items.</summary>
		/// <returns>
		///   <see langword="true" /> if the list portion can contain only complete items; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000F79 RID: 3961
		// (get) Token: 0x06003F2C RID: 16172 RVA: 0x0011137A File Offset: 0x0010F57A
		// (set) Token: 0x06003F2D RID: 16173 RVA: 0x00111387 File Offset: 0x0010F587
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[Localizable(true)]
		[SRDescription("ComboBoxIntegralHeightDescr")]
		public bool IntegralHeight
		{
			get
			{
				return this.ComboBox.IntegralHeight;
			}
			set
			{
				this.ComboBox.IntegralHeight = value;
			}
		}

		/// <summary>Gets a collection of the items contained in this <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
		/// <returns>A collection of items.</returns>
		// Token: 0x17000F7A RID: 3962
		// (get) Token: 0x06003F2E RID: 16174 RVA: 0x00111395 File Offset: 0x0010F595
		[SRCategory("CatData")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Localizable(true)]
		[SRDescription("ComboBoxItemsDescr")]
		[Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public ComboBox.ObjectCollection Items
		{
			get
			{
				return this.ComboBox.Items;
			}
		}

		/// <summary>Gets or sets the maximum number of items to be shown in the drop-down portion of the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
		/// <returns>The maximum number of items in the drop-down portion. The minimum for this property is 1 and the maximum is 100.</returns>
		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x06003F2F RID: 16175 RVA: 0x001113A2 File Offset: 0x0010F5A2
		// (set) Token: 0x06003F30 RID: 16176 RVA: 0x001113AF File Offset: 0x0010F5AF
		[SRCategory("CatBehavior")]
		[DefaultValue(8)]
		[Localizable(true)]
		[SRDescription("ComboBoxMaxDropDownItemsDescr")]
		public int MaxDropDownItems
		{
			get
			{
				return this.ComboBox.MaxDropDownItems;
			}
			set
			{
				this.ComboBox.MaxDropDownItems = value;
			}
		}

		/// <summary>Gets or sets the maximum number of characters allowed in the editable portion of a combo box.</summary>
		/// <returns>The maximum number of characters the user can enter. Values of less than zero are reset to zero, which is the default value.</returns>
		// Token: 0x17000F7C RID: 3964
		// (get) Token: 0x06003F31 RID: 16177 RVA: 0x001113BD File Offset: 0x0010F5BD
		// (set) Token: 0x06003F32 RID: 16178 RVA: 0x001113CA File Offset: 0x0010F5CA
		[SRCategory("CatBehavior")]
		[DefaultValue(0)]
		[Localizable(true)]
		[SRDescription("ComboBoxMaxLengthDescr")]
		public int MaxLength
		{
			get
			{
				return this.ComboBox.MaxLength;
			}
			set
			{
				this.ComboBox.MaxLength = value;
			}
		}

		/// <summary>Gets or sets the index specifying the currently selected item.</summary>
		/// <returns>A zero-based index of the currently selected item. A value of negative one (-1) is returned if no item is selected.</returns>
		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x06003F33 RID: 16179 RVA: 0x001113D8 File Offset: 0x0010F5D8
		// (set) Token: 0x06003F34 RID: 16180 RVA: 0x001113E5 File Offset: 0x0010F5E5
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ComboBoxSelectedIndexDescr")]
		public int SelectedIndex
		{
			get
			{
				return this.ComboBox.SelectedIndex;
			}
			set
			{
				this.ComboBox.SelectedIndex = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripComboBox.SelectedIndex" /> property has changed.</summary>
		// Token: 0x1400030B RID: 779
		// (add) Token: 0x06003F35 RID: 16181 RVA: 0x001113F3 File Offset: 0x0010F5F3
		// (remove) Token: 0x06003F36 RID: 16182 RVA: 0x00111406 File Offset: 0x0010F606
		[SRCategory("CatBehavior")]
		[SRDescription("selectedIndexChangedEventDescr")]
		public event EventHandler SelectedIndexChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripComboBox.EventSelectedIndexChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripComboBox.EventSelectedIndexChanged, value);
			}
		}

		/// <summary>Gets or sets currently selected item in the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
		/// <returns>The object that is the currently selected item or <see langword="null" /> if there is no currently selected item.</returns>
		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x06003F37 RID: 16183 RVA: 0x00111419 File Offset: 0x0010F619
		// (set) Token: 0x06003F38 RID: 16184 RVA: 0x00111426 File Offset: 0x0010F626
		[Browsable(false)]
		[Bindable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ComboBoxSelectedItemDescr")]
		public object SelectedItem
		{
			get
			{
				return this.ComboBox.SelectedItem;
			}
			set
			{
				this.ComboBox.SelectedItem = value;
			}
		}

		/// <summary>Gets or sets the text that is selected in the editable portion of a <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
		/// <returns>A string that represents the currently selected text in the combo box. If <see cref="P:System.Windows.Forms.ToolStripComboBox.DropDownStyle" /> is set to <see langword="DropDownList" />, the return value is an empty string ("").</returns>
		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x06003F39 RID: 16185 RVA: 0x00111434 File Offset: 0x0010F634
		// (set) Token: 0x06003F3A RID: 16186 RVA: 0x00111441 File Offset: 0x0010F641
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ComboBoxSelectedTextDescr")]
		public string SelectedText
		{
			get
			{
				return this.ComboBox.SelectedText;
			}
			set
			{
				this.ComboBox.SelectedText = value;
			}
		}

		/// <summary>Gets or sets the number of characters selected in the editable portion of the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
		/// <returns>The number of characters selected in the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</returns>
		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x06003F3B RID: 16187 RVA: 0x0011144F File Offset: 0x0010F64F
		// (set) Token: 0x06003F3C RID: 16188 RVA: 0x0011145C File Offset: 0x0010F65C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ComboBoxSelectionLengthDescr")]
		public int SelectionLength
		{
			get
			{
				return this.ComboBox.SelectionLength;
			}
			set
			{
				this.ComboBox.SelectionLength = value;
			}
		}

		/// <summary>Gets or sets the starting index of text selected in the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
		/// <returns>The zero-based index of the first character in the string of the current text selection.</returns>
		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x06003F3D RID: 16189 RVA: 0x0011146A File Offset: 0x0010F66A
		// (set) Token: 0x06003F3E RID: 16190 RVA: 0x00111477 File Offset: 0x0010F677
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ComboBoxSelectionStartDescr")]
		public int SelectionStart
		{
			get
			{
				return this.ComboBox.SelectionStart;
			}
			set
			{
				this.ComboBox.SelectionStart = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the items in the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> are sorted.</summary>
		/// <returns>
		///   <see langword="true" /> if the combo box is sorted; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000F82 RID: 3970
		// (get) Token: 0x06003F3F RID: 16191 RVA: 0x00111485 File Offset: 0x0010F685
		// (set) Token: 0x06003F40 RID: 16192 RVA: 0x00111492 File Offset: 0x0010F692
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("ComboBoxSortedDescr")]
		public bool Sorted
		{
			get
			{
				return this.ComboBox.Sorted;
			}
			set
			{
				this.ComboBox.Sorted = value;
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> text has changed.</summary>
		// Token: 0x1400030C RID: 780
		// (add) Token: 0x06003F41 RID: 16193 RVA: 0x001114A0 File Offset: 0x0010F6A0
		// (remove) Token: 0x06003F42 RID: 16194 RVA: 0x001114B3 File Offset: 0x0010F6B3
		[SRCategory("CatBehavior")]
		[SRDescription("ComboBoxOnTextUpdateDescr")]
		public event EventHandler TextUpdate
		{
			add
			{
				base.Events.AddHandler(ToolStripComboBox.EventTextUpdate, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripComboBox.EventTextUpdate, value);
			}
		}

		/// <summary>Maintains performance when items are added to the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> one at a time.</summary>
		// Token: 0x06003F43 RID: 16195 RVA: 0x001114C6 File Offset: 0x0010F6C6
		public void BeginUpdate()
		{
			this.ComboBox.BeginUpdate();
		}

		/// <summary>Resumes painting the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> control after painting is suspended by the <see cref="M:System.Windows.Forms.ToolStripComboBox.BeginUpdate" /> method.</summary>
		// Token: 0x06003F44 RID: 16196 RVA: 0x001114D3 File Offset: 0x0010F6D3
		public void EndUpdate()
		{
			this.ComboBox.EndUpdate();
		}

		/// <summary>Finds the first item in the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> that starts with the specified string.</summary>
		/// <param name="s">The <see cref="T:System.String" /> to search for.</param>
		/// <returns>The zero-based index of the first item found; returns -1 if no match is found.</returns>
		// Token: 0x06003F45 RID: 16197 RVA: 0x001114E0 File Offset: 0x0010F6E0
		public int FindString(string s)
		{
			return this.ComboBox.FindString(s);
		}

		/// <summary>Finds the first item after the given index which starts with the given string.</summary>
		/// <param name="s">The <see cref="T:System.String" /> to search for.</param>
		/// <param name="startIndex">The zero-based index of the item before the first item to be searched. Set to -1 to search from the beginning of the control.</param>
		/// <returns>The zero-based index of the first item found; returns -1 if no match is found.</returns>
		// Token: 0x06003F46 RID: 16198 RVA: 0x001114EE File Offset: 0x0010F6EE
		public int FindString(string s, int startIndex)
		{
			return this.ComboBox.FindString(s, startIndex);
		}

		/// <summary>Finds the first item in the <see cref="T:System.Windows.Forms.ToolStripComboBox" /> that exactly matches the specified string.</summary>
		/// <param name="s">The <see cref="T:System.String" /> to search for.</param>
		/// <returns>The zero-based index of the first item found; -1 if no match is found.</returns>
		// Token: 0x06003F47 RID: 16199 RVA: 0x001114FD File Offset: 0x0010F6FD
		public int FindStringExact(string s)
		{
			return this.ComboBox.FindStringExact(s);
		}

		/// <summary>Finds the first item after the specified index that exactly matches the specified string.</summary>
		/// <param name="s">The <see cref="T:System.String" /> to search for.</param>
		/// <param name="startIndex">The zero-based index of the item before the first item to be searched. Set to -1 to search from the beginning of the control.</param>
		/// <returns>The zero-based index of the first item found; returns -1 if no match is found.</returns>
		// Token: 0x06003F48 RID: 16200 RVA: 0x0011150B File Offset: 0x0010F70B
		public int FindStringExact(string s, int startIndex)
		{
			return this.ComboBox.FindStringExact(s, startIndex);
		}

		/// <summary>Returns the height, in pixels, of an item in the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
		/// <param name="index">The index of the item to return the height of.</param>
		/// <returns>The height, in pixels, of the item at the specified index.</returns>
		// Token: 0x06003F49 RID: 16201 RVA: 0x0011151A File Offset: 0x0010F71A
		public int GetItemHeight(int index)
		{
			return this.ComboBox.GetItemHeight(index);
		}

		/// <summary>Selects a range of text in the editable portion of the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
		/// <param name="start">The position of the first character in the current text selection within the text box.</param>
		/// <param name="length">The number of characters to select.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="start" /> is less than zero.  
		///  -or-  
		///  <paramref name="start" /> minus <paramref name="length" /> is less than zero.</exception>
		// Token: 0x06003F4A RID: 16202 RVA: 0x00111528 File Offset: 0x0010F728
		public void Select(int start, int length)
		{
			this.ComboBox.Select(start, length);
		}

		/// <summary>Selects all the text in the editable portion of the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
		// Token: 0x06003F4B RID: 16203 RVA: 0x00111537 File Offset: 0x0010F737
		public void SelectAll()
		{
			this.ComboBox.SelectAll();
		}

		/// <summary>Retrieves the size of a rectangular area into which a control can be fitted.</summary>
		/// <param name="constrainingSize">The custom-sized area for a control.</param>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		// Token: 0x06003F4C RID: 16204 RVA: 0x00111544 File Offset: 0x0010F744
		public override Size GetPreferredSize(Size constrainingSize)
		{
			Size preferredSize = base.GetPreferredSize(constrainingSize);
			preferredSize.Width = Math.Max(preferredSize.Width, 75);
			return preferredSize;
		}

		// Token: 0x06003F4D RID: 16205 RVA: 0x0011156F File Offset: 0x0010F76F
		private void HandleDropDown(object sender, EventArgs e)
		{
			this.OnDropDown(e);
		}

		// Token: 0x06003F4E RID: 16206 RVA: 0x00111578 File Offset: 0x0010F778
		private void HandleDropDownClosed(object sender, EventArgs e)
		{
			this.OnDropDownClosed(e);
		}

		// Token: 0x06003F4F RID: 16207 RVA: 0x00111581 File Offset: 0x0010F781
		private void HandleDropDownStyleChanged(object sender, EventArgs e)
		{
			this.OnDropDownStyleChanged(e);
		}

		// Token: 0x06003F50 RID: 16208 RVA: 0x0011158A File Offset: 0x0010F78A
		private void HandleSelectedIndexChanged(object sender, EventArgs e)
		{
			this.OnSelectedIndexChanged(e);
		}

		// Token: 0x06003F51 RID: 16209 RVA: 0x00111593 File Offset: 0x0010F793
		private void HandleSelectionChangeCommitted(object sender, EventArgs e)
		{
			this.OnSelectionChangeCommitted(e);
		}

		// Token: 0x06003F52 RID: 16210 RVA: 0x0011159C File Offset: 0x0010F79C
		private void HandleTextUpdate(object sender, EventArgs e)
		{
			this.OnTextUpdate(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripComboBox.DropDown" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003F53 RID: 16211 RVA: 0x001115A5 File Offset: 0x0010F7A5
		protected virtual void OnDropDown(EventArgs e)
		{
			if (base.ParentInternal != null)
			{
				Application.ThreadContext.FromCurrent().RemoveMessageFilter(base.ParentInternal.RestoreFocusFilter);
				ToolStripManager.ModalMenuFilter.SuspendMenuMode();
			}
			base.RaiseEvent(ToolStripComboBox.EventDropDown, e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripComboBox.DropDownClosed" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003F54 RID: 16212 RVA: 0x001115D5 File Offset: 0x0010F7D5
		protected virtual void OnDropDownClosed(EventArgs e)
		{
			if (base.ParentInternal != null)
			{
				Application.ThreadContext.FromCurrent().RemoveMessageFilter(base.ParentInternal.RestoreFocusFilter);
				ToolStripManager.ModalMenuFilter.ResumeMenuMode();
			}
			base.RaiseEvent(ToolStripComboBox.EventDropDownClosed, e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripComboBox.DropDownStyleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003F55 RID: 16213 RVA: 0x00111605 File Offset: 0x0010F805
		protected virtual void OnDropDownStyleChanged(EventArgs e)
		{
			base.RaiseEvent(ToolStripComboBox.EventDropDownStyleChanged, e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripComboBox.SelectedIndexChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003F56 RID: 16214 RVA: 0x00111613 File Offset: 0x0010F813
		protected virtual void OnSelectedIndexChanged(EventArgs e)
		{
			base.RaiseEvent(ToolStripComboBox.EventSelectedIndexChanged, e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ComboBox.SelectionChangeCommitted" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003F57 RID: 16215 RVA: 0x00111621 File Offset: 0x0010F821
		protected virtual void OnSelectionChangeCommitted(EventArgs e)
		{
			base.RaiseEvent(ToolStripComboBox.EventSelectionChangeCommitted, e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripComboBox.TextUpdate" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003F58 RID: 16216 RVA: 0x0011162F File Offset: 0x0010F82F
		protected virtual void OnTextUpdate(EventArgs e)
		{
			base.RaiseEvent(ToolStripComboBox.EventTextUpdate, e);
		}

		/// <summary>Subscribes events from the specified control.</summary>
		/// <param name="control">The control from which to subscribe events.</param>
		// Token: 0x06003F59 RID: 16217 RVA: 0x00111640 File Offset: 0x0010F840
		protected override void OnSubscribeControlEvents(Control control)
		{
			ComboBox comboBox = control as ComboBox;
			if (comboBox != null)
			{
				comboBox.DropDown += this.HandleDropDown;
				comboBox.DropDownClosed += this.HandleDropDownClosed;
				comboBox.DropDownStyleChanged += this.HandleDropDownStyleChanged;
				comboBox.SelectedIndexChanged += this.HandleSelectedIndexChanged;
				comboBox.SelectionChangeCommitted += this.HandleSelectionChangeCommitted;
				comboBox.TextUpdate += this.HandleTextUpdate;
			}
			base.OnSubscribeControlEvents(control);
		}

		/// <summary>Unsubscribes events from the specified control.</summary>
		/// <param name="control">The control from which to unsubscribe events.</param>
		// Token: 0x06003F5A RID: 16218 RVA: 0x001116CC File Offset: 0x0010F8CC
		protected override void OnUnsubscribeControlEvents(Control control)
		{
			ComboBox comboBox = control as ComboBox;
			if (comboBox != null)
			{
				comboBox.DropDown -= this.HandleDropDown;
				comboBox.DropDownClosed -= this.HandleDropDownClosed;
				comboBox.DropDownStyleChanged -= this.HandleDropDownStyleChanged;
				comboBox.SelectedIndexChanged -= this.HandleSelectedIndexChanged;
				comboBox.SelectionChangeCommitted -= this.HandleSelectionChangeCommitted;
				comboBox.TextUpdate -= this.HandleTextUpdate;
			}
			base.OnUnsubscribeControlEvents(control);
		}

		// Token: 0x06003F5B RID: 16219 RVA: 0x00111756 File Offset: 0x0010F956
		private bool ShouldSerializeDropDownWidth()
		{
			return this.ComboBox.ShouldSerializeDropDownWidth();
		}

		// Token: 0x06003F5C RID: 16220 RVA: 0x00111763 File Offset: 0x0010F963
		internal override bool ShouldSerializeFont()
		{
			return !object.Equals(this.Font, ToolStripManager.DefaultFont);
		}

		/// <summary>Returns a string representation of the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</summary>
		/// <returns>A string that represents the <see cref="T:System.Windows.Forms.ToolStripComboBox" />.</returns>
		// Token: 0x06003F5D RID: 16221 RVA: 0x00111778 File Offset: 0x0010F978
		public override string ToString()
		{
			return base.ToString() + ", Items.Count: " + this.Items.Count.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x04002497 RID: 9367
		internal static readonly object EventDropDown = new object();

		// Token: 0x04002498 RID: 9368
		internal static readonly object EventDropDownClosed = new object();

		// Token: 0x04002499 RID: 9369
		internal static readonly object EventDropDownStyleChanged = new object();

		// Token: 0x0400249A RID: 9370
		internal static readonly object EventSelectedIndexChanged = new object();

		// Token: 0x0400249B RID: 9371
		internal static readonly object EventSelectionChangeCommitted = new object();

		// Token: 0x0400249C RID: 9372
		internal static readonly object EventTextUpdate = new object();

		// Token: 0x0400249D RID: 9373
		private static readonly Padding dropDownPadding = new Padding(2);

		// Token: 0x0400249E RID: 9374
		private static readonly Padding padding = new Padding(1, 0, 1, 0);

		// Token: 0x0400249F RID: 9375
		private Padding scaledDropDownPadding = ToolStripComboBox.dropDownPadding;

		// Token: 0x040024A0 RID: 9376
		private Padding scaledPadding = ToolStripComboBox.padding;

		// Token: 0x020007FB RID: 2043
		[ComVisible(true)]
		internal class ToolStripComboBoxAccessibleObject : ToolStripItem.ToolStripItemAccessibleObject
		{
			// Token: 0x06006E98 RID: 28312 RVA: 0x00195189 File Offset: 0x00193389
			public ToolStripComboBoxAccessibleObject(ToolStripComboBox ownerItem)
				: base(ownerItem)
			{
				this.ownerItem = ownerItem;
			}

			// Token: 0x1700182A RID: 6186
			// (get) Token: 0x06006E99 RID: 28313 RVA: 0x0017E1A1 File Offset: 0x0017C3A1
			public override string DefaultAction
			{
				get
				{
					return string.Empty;
				}
			}

			// Token: 0x06006E9A RID: 28314 RVA: 0x000070A6 File Offset: 0x000052A6
			public override void DoDefaultAction()
			{
			}

			// Token: 0x1700182B RID: 6187
			// (get) Token: 0x06006E9B RID: 28315 RVA: 0x0019519C File Offset: 0x0019339C
			public override AccessibleRole Role
			{
				get
				{
					AccessibleRole accessibleRole = base.Owner.AccessibleRole;
					if (accessibleRole != AccessibleRole.Default)
					{
						return accessibleRole;
					}
					return AccessibleRole.ComboBox;
				}
			}

			// Token: 0x06006E9C RID: 28316 RVA: 0x001951BD File Offset: 0x001933BD
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (direction == UnsafeNativeMethods.NavigateDirection.FirstChild || direction == UnsafeNativeMethods.NavigateDirection.LastChild)
				{
					return this.ownerItem.ComboBox.AccessibilityObject;
				}
				return base.FragmentNavigate(direction);
			}

			// Token: 0x1700182C RID: 6188
			// (get) Token: 0x06006E9D RID: 28317 RVA: 0x001951DF File Offset: 0x001933DF
			internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
			{
				get
				{
					return this.ownerItem.RootToolStrip.AccessibilityObject;
				}
			}

			// Token: 0x040042E8 RID: 17128
			private ToolStripComboBox ownerItem;
		}

		// Token: 0x020007FC RID: 2044
		internal class ToolStripComboBoxControl : ComboBox
		{
			// Token: 0x06006E9E RID: 28318 RVA: 0x001951F1 File Offset: 0x001933F1
			public ToolStripComboBoxControl()
			{
				base.FlatStyle = FlatStyle.Popup;
				base.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
			}

			// Token: 0x1700182D RID: 6189
			// (get) Token: 0x06006E9F RID: 28319 RVA: 0x0019520C File Offset: 0x0019340C
			// (set) Token: 0x06006EA0 RID: 28320 RVA: 0x00195214 File Offset: 0x00193414
			public ToolStripComboBox Owner
			{
				get
				{
					return this.owner;
				}
				set
				{
					this.owner = value;
				}
			}

			// Token: 0x1700182E RID: 6190
			// (get) Token: 0x06006EA1 RID: 28321 RVA: 0x00195220 File Offset: 0x00193420
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

			// Token: 0x06006EA2 RID: 28322 RVA: 0x00195255 File Offset: 0x00193455
			protected override AccessibleObject CreateAccessibilityInstance()
			{
				if (AccessibilityImprovements.Level3)
				{
					return new ToolStripComboBox.ToolStripComboBoxControl.ToolStripComboBoxControlAccessibleObject(this);
				}
				return base.CreateAccessibilityInstance();
			}

			// Token: 0x06006EA3 RID: 28323 RVA: 0x0019526B File Offset: 0x0019346B
			internal override ComboBox.FlatComboAdapter CreateFlatComboAdapterInstance()
			{
				return new ToolStripComboBox.ToolStripComboBoxControl.ToolStripComboBoxFlatComboAdapter(this);
			}

			// Token: 0x06006EA4 RID: 28324 RVA: 0x00195274 File Offset: 0x00193474
			protected override bool IsInputKey(Keys keyData)
			{
				if ((keyData & Keys.Alt) == Keys.Alt)
				{
					if (AccessibilityImprovements.Level5)
					{
						Keys keys = keyData & Keys.KeyCode;
						if (keys == Keys.Up || keys == Keys.Down)
						{
							return true;
						}
					}
					else if ((keyData & Keys.Down) == Keys.Down || (keyData & Keys.Up) == Keys.Up)
					{
						return true;
					}
				}
				return base.IsInputKey(keyData);
			}

			// Token: 0x06006EA5 RID: 28325 RVA: 0x001952C3 File Offset: 0x001934C3
			protected override void OnDropDownClosed(EventArgs e)
			{
				base.OnDropDownClosed(e);
				base.Invalidate();
				base.Update();
			}

			// Token: 0x1700182F RID: 6191
			// (get) Token: 0x06006EA6 RID: 28326 RVA: 0x000A83A1 File Offset: 0x000A65A1
			internal override bool SupportsUiaProviders
			{
				get
				{
					return AccessibilityImprovements.Level3;
				}
			}

			// Token: 0x040042E9 RID: 17129
			private ToolStripComboBox owner;

			// Token: 0x020008C7 RID: 2247
			internal class ToolStripComboBoxFlatComboAdapter : ComboBox.FlatComboAdapter
			{
				// Token: 0x060072C7 RID: 29383 RVA: 0x001A30E9 File Offset: 0x001A12E9
				public ToolStripComboBoxFlatComboAdapter(ComboBox comboBox)
					: base(comboBox, true)
				{
				}

				// Token: 0x060072C8 RID: 29384 RVA: 0x001A30F4 File Offset: 0x001A12F4
				private static bool UseBaseAdapter(ComboBox comboBox)
				{
					ToolStripComboBox.ToolStripComboBoxControl toolStripComboBoxControl = comboBox as ToolStripComboBox.ToolStripComboBoxControl;
					return toolStripComboBoxControl == null || !(toolStripComboBoxControl.Owner.Renderer is ToolStripProfessionalRenderer);
				}

				// Token: 0x060072C9 RID: 29385 RVA: 0x001A3120 File Offset: 0x001A1320
				private static ProfessionalColorTable GetColorTable(ToolStripComboBox.ToolStripComboBoxControl toolStripComboBoxControl)
				{
					if (toolStripComboBoxControl != null)
					{
						return toolStripComboBoxControl.ColorTable;
					}
					return ProfessionalColors.ColorTable;
				}

				// Token: 0x060072CA RID: 29386 RVA: 0x001A3131 File Offset: 0x001A1331
				protected override Color GetOuterBorderColor(ComboBox comboBox)
				{
					if (ToolStripComboBox.ToolStripComboBoxControl.ToolStripComboBoxFlatComboAdapter.UseBaseAdapter(comboBox))
					{
						return base.GetOuterBorderColor(comboBox);
					}
					if (!comboBox.Enabled)
					{
						return ToolStripComboBox.ToolStripComboBoxControl.ToolStripComboBoxFlatComboAdapter.GetColorTable(comboBox as ToolStripComboBox.ToolStripComboBoxControl).ComboBoxBorder;
					}
					return SystemColors.Window;
				}

				// Token: 0x060072CB RID: 29387 RVA: 0x001A3161 File Offset: 0x001A1361
				protected override Color GetPopupOuterBorderColor(ComboBox comboBox, bool focused)
				{
					if (ToolStripComboBox.ToolStripComboBoxControl.ToolStripComboBoxFlatComboAdapter.UseBaseAdapter(comboBox))
					{
						return base.GetPopupOuterBorderColor(comboBox, focused);
					}
					if (!comboBox.Enabled)
					{
						return SystemColors.ControlDark;
					}
					if (!focused)
					{
						return SystemColors.Window;
					}
					return ToolStripComboBox.ToolStripComboBoxControl.ToolStripComboBoxFlatComboAdapter.GetColorTable(comboBox as ToolStripComboBox.ToolStripComboBoxControl).ComboBoxBorder;
				}

				// Token: 0x060072CC RID: 29388 RVA: 0x001A319C File Offset: 0x001A139C
				protected override void DrawFlatComboDropDown(ComboBox comboBox, Graphics g, Rectangle dropDownRect)
				{
					if (ToolStripComboBox.ToolStripComboBoxControl.ToolStripComboBoxFlatComboAdapter.UseBaseAdapter(comboBox))
					{
						base.DrawFlatComboDropDown(comboBox, g, dropDownRect);
						return;
					}
					if (!comboBox.Enabled || !ToolStripManager.VisualStylesEnabled)
					{
						g.FillRectangle(SystemBrushes.Control, dropDownRect);
					}
					else
					{
						ToolStripComboBox.ToolStripComboBoxControl toolStripComboBoxControl = comboBox as ToolStripComboBox.ToolStripComboBoxControl;
						ProfessionalColorTable colorTable = ToolStripComboBox.ToolStripComboBoxControl.ToolStripComboBoxFlatComboAdapter.GetColorTable(toolStripComboBoxControl);
						if (!comboBox.DroppedDown)
						{
							bool flag = comboBox.ContainsFocus || comboBox.MouseIsOver;
							if (flag)
							{
								using (Brush brush = new LinearGradientBrush(dropDownRect, colorTable.ComboBoxButtonSelectedGradientBegin, colorTable.ComboBoxButtonSelectedGradientEnd, LinearGradientMode.Vertical))
								{
									g.FillRectangle(brush, dropDownRect);
									goto IL_11A;
								}
							}
							if (toolStripComboBoxControl.Owner.IsOnOverflow)
							{
								using (Brush brush2 = new SolidBrush(colorTable.ComboBoxButtonOnOverflow))
								{
									g.FillRectangle(brush2, dropDownRect);
									goto IL_11A;
								}
							}
							using (Brush brush3 = new LinearGradientBrush(dropDownRect, colorTable.ComboBoxButtonGradientBegin, colorTable.ComboBoxButtonGradientEnd, LinearGradientMode.Vertical))
							{
								g.FillRectangle(brush3, dropDownRect);
								goto IL_11A;
							}
						}
						using (Brush brush4 = new LinearGradientBrush(dropDownRect, colorTable.ComboBoxButtonPressedGradientBegin, colorTable.ComboBoxButtonPressedGradientEnd, LinearGradientMode.Vertical))
						{
							g.FillRectangle(brush4, dropDownRect);
						}
					}
					IL_11A:
					Brush brush5;
					if (comboBox.Enabled)
					{
						if (AccessibilityImprovements.Level2 && SystemInformation.HighContrast && (comboBox.ContainsFocus || comboBox.MouseIsOver) && ToolStripManager.VisualStylesEnabled)
						{
							brush5 = SystemBrushes.HighlightText;
						}
						else
						{
							brush5 = SystemBrushes.ControlText;
						}
					}
					else
					{
						brush5 = SystemBrushes.GrayText;
					}
					Point point = new Point(dropDownRect.Left + dropDownRect.Width / 2, dropDownRect.Top + dropDownRect.Height / 2);
					point.X += dropDownRect.Width % 2;
					g.FillPolygon(brush5, new Point[]
					{
						new Point(point.X - ComboBox.FlatComboAdapter.Offset2Pixels, point.Y - 1),
						new Point(point.X + ComboBox.FlatComboAdapter.Offset2Pixels + 1, point.Y - 1),
						new Point(point.X, point.Y + ComboBox.FlatComboAdapter.Offset2Pixels)
					});
				}
			}

			// Token: 0x020008C8 RID: 2248
			internal class ToolStripComboBoxControlAccessibleObject : ComboBox.ComboBoxUiaProvider
			{
				// Token: 0x060072CD RID: 29389 RVA: 0x001A33F0 File Offset: 0x001A15F0
				public ToolStripComboBoxControlAccessibleObject(ToolStripComboBox.ToolStripComboBoxControl toolStripComboBoxControl)
					: base(toolStripComboBoxControl)
				{
					this.childAccessibleObject = new ComboBox.ChildAccessibleObject(toolStripComboBoxControl, toolStripComboBoxControl.Handle);
				}

				// Token: 0x060072CE RID: 29390 RVA: 0x001A340C File Offset: 0x001A160C
				internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
				{
					if (direction <= UnsafeNativeMethods.NavigateDirection.PreviousSibling)
					{
						ToolStripComboBox.ToolStripComboBoxControl toolStripComboBoxControl = base.Owner as ToolStripComboBox.ToolStripComboBoxControl;
						if (toolStripComboBoxControl != null)
						{
							return toolStripComboBoxControl.Owner.AccessibilityObject.FragmentNavigate(direction);
						}
					}
					return base.FragmentNavigate(direction);
				}

				// Token: 0x1700193A RID: 6458
				// (get) Token: 0x060072CF RID: 29391 RVA: 0x001A3448 File Offset: 0x001A1648
				internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
				{
					get
					{
						ToolStripComboBox.ToolStripComboBoxControl toolStripComboBoxControl = base.Owner as ToolStripComboBox.ToolStripComboBoxControl;
						if (toolStripComboBoxControl != null)
						{
							return toolStripComboBoxControl.Owner.Owner.AccessibilityObject;
						}
						return base.FragmentRoot;
					}
				}

				// Token: 0x060072D0 RID: 29392 RVA: 0x001A347B File Offset: 0x001A167B
				internal override object GetPropertyValue(int propertyID)
				{
					if (propertyID == 30003)
					{
						return 50003;
					}
					if (propertyID != 30022)
					{
						return base.GetPropertyValue(propertyID);
					}
					return (this.State & AccessibleStates.Offscreen) == AccessibleStates.Offscreen;
				}

				// Token: 0x060072D1 RID: 29393 RVA: 0x001A34BA File Offset: 0x001A16BA
				internal override bool IsPatternSupported(int patternId)
				{
					return patternId == 10005 || patternId == 10002 || base.IsPatternSupported(patternId);
				}

				// Token: 0x04004551 RID: 17745
				private ComboBox.ChildAccessibleObject childAccessibleObject;
			}
		}
	}
}
