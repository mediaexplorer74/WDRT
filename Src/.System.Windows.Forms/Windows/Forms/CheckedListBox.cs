using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Displays a <see cref="T:System.Windows.Forms.ListBox" /> in which a check box is displayed to the left of each item.</summary>
	// Token: 0x0200014C RID: 332
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[LookupBindingProperties]
	[SRDescription("DescriptionCheckedListBox")]
	public class CheckedListBox : ListBox
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.CheckedListBox" /> class.</summary>
		// Token: 0x06000D1F RID: 3359 RVA: 0x0002590A File Offset: 0x00023B0A
		public CheckedListBox()
		{
			base.SetStyle(ControlStyles.ResizeRedraw, true);
		}

		/// <summary>Gets or sets a value indicating whether the check box should be toggled when an item is selected.</summary>
		/// <returns>
		///   <see langword="true" /> if the check mark is applied immediately; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000D20 RID: 3360 RVA: 0x00025931 File Offset: 0x00023B31
		// (set) Token: 0x06000D21 RID: 3361 RVA: 0x00025939 File Offset: 0x00023B39
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("CheckedListBoxCheckOnClickDescr")]
		public bool CheckOnClick
		{
			get
			{
				return this.checkOnClick;
			}
			set
			{
				this.checkOnClick = value;
			}
		}

		/// <summary>Collection of checked indexes in this <see cref="T:System.Windows.Forms.CheckedListBox" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.CheckedListBox.CheckedIndexCollection" /> collection for the <see cref="T:System.Windows.Forms.CheckedListBox" />.</returns>
		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000D22 RID: 3362 RVA: 0x00025942 File Offset: 0x00023B42
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public CheckedListBox.CheckedIndexCollection CheckedIndices
		{
			get
			{
				if (this.checkedIndexCollection == null)
				{
					this.checkedIndexCollection = new CheckedListBox.CheckedIndexCollection(this);
				}
				return this.checkedIndexCollection;
			}
		}

		/// <summary>Collection of checked items in this <see cref="T:System.Windows.Forms.CheckedListBox" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.CheckedListBox.CheckedItemCollection" /> collection for the <see cref="T:System.Windows.Forms.CheckedListBox" />.</returns>
		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000D23 RID: 3363 RVA: 0x0002595E File Offset: 0x00023B5E
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public CheckedListBox.CheckedItemCollection CheckedItems
		{
			get
			{
				if (this.checkedItemCollection == null)
				{
					this.checkedItemCollection = new CheckedListBox.CheckedItemCollection(this);
				}
				return this.checkedItemCollection;
			}
		}

		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required parameters.</returns>
		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000D24 RID: 3364 RVA: 0x0002597C File Offset: 0x00023B7C
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.Style |= 1040;
				return createParams;
			}
		}

		/// <summary>Gets or sets the data source for the control.</summary>
		/// <returns>An object representing the source of the data.</returns>
		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000D25 RID: 3365 RVA: 0x000259A3 File Offset: 0x00023BA3
		// (set) Token: 0x06000D26 RID: 3366 RVA: 0x000259AB File Offset: 0x00023BAB
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new object DataSource
		{
			get
			{
				return base.DataSource;
			}
			set
			{
				base.DataSource = value;
			}
		}

		/// <summary>Gets or sets a string that specifies a property of the objects contained in the list box whose contents you want to display.</summary>
		/// <returns>A string that specifies the name of a property of the objects contained in the list box. The default is an empty string ("").</returns>
		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000D27 RID: 3367 RVA: 0x000259B4 File Offset: 0x00023BB4
		// (set) Token: 0x06000D28 RID: 3368 RVA: 0x000259BC File Offset: 0x00023BBC
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new string DisplayMember
		{
			get
			{
				return base.DisplayMember;
			}
			set
			{
				base.DisplayMember = value;
			}
		}

		/// <summary>Gets a value indicating the mode for drawing elements of the <see cref="T:System.Windows.Forms.CheckedListBox" />. This property is not relevant to this class.</summary>
		/// <returns>Always a <see cref="T:System.Windows.Forms.DrawMode" /> of <see langword="Normal" />.</returns>
		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000D29 RID: 3369 RVA: 0x0001180C File Offset: 0x0000FA0C
		// (set) Token: 0x06000D2A RID: 3370 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override DrawMode DrawMode
		{
			get
			{
				return DrawMode.Normal;
			}
			set
			{
			}
		}

		/// <summary>Gets the height of the item area.</summary>
		/// <returns>The height, in pixels, of the item area.</returns>
		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000D2B RID: 3371 RVA: 0x000259C5 File Offset: 0x00023BC5
		// (set) Token: 0x06000D2C RID: 3372 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override int ItemHeight
		{
			get
			{
				return this.Font.Height + this.scaledListItemBordersHeight;
			}
			set
			{
			}
		}

		/// <summary>Gets the collection of items in this <see cref="T:System.Windows.Forms.CheckedListBox" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.CheckedListBox.ObjectCollection" /> collection representing the items in the <see cref="T:System.Windows.Forms.CheckedListBox" />.</returns>
		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000D2D RID: 3373 RVA: 0x000259D9 File Offset: 0x00023BD9
		[SRCategory("CatData")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Localizable(true)]
		[SRDescription("ListBoxItemsDescr")]
		[Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public new CheckedListBox.ObjectCollection Items
		{
			get
			{
				return (CheckedListBox.ObjectCollection)base.Items;
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x000259E6 File Offset: 0x00023BE6
		internal override int MaxItemWidth
		{
			get
			{
				return base.MaxItemWidth + this.idealCheckSize + this.scaledListItemPaddingBuffer;
			}
		}

		/// <summary>Gets or sets a value specifying the selection mode.</summary>
		/// <returns>Either the <see langword="One" /> or <see langword="None" /> value of <see cref="T:System.Windows.Forms.SelectionMode" />.</returns>
		/// <exception cref="T:System.ArgumentException">An attempt was made to assign a value that is not a <see cref="T:System.Windows.Forms.SelectionMode" /> value of <see langword="One" /> or <see langword="None" />.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">An attempt was made to assign the <see langword="MultiExtended" /> value of <see cref="T:System.Windows.Forms.SelectionMode" /> to the control.</exception>
		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000D2F RID: 3375 RVA: 0x000259FC File Offset: 0x00023BFC
		// (set) Token: 0x06000D30 RID: 3376 RVA: 0x00025A04 File Offset: 0x00023C04
		public override SelectionMode SelectionMode
		{
			get
			{
				return base.SelectionMode;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(SelectionMode));
				}
				if (value != SelectionMode.One && value != SelectionMode.None)
				{
					throw new ArgumentException(SR.GetString("CheckedListBoxInvalidSelectionMode"));
				}
				if (value != this.SelectionMode)
				{
					base.SelectionMode = value;
					base.RecreateHandle();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the check boxes have a <see cref="T:System.Windows.Forms.ButtonState" /> of <see langword="Flat" /> or <see langword="Normal" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the check box has a flat appearance; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000D31 RID: 3377 RVA: 0x00025A64 File Offset: 0x00023C64
		// (set) Token: 0x06000D32 RID: 3378 RVA: 0x00025A70 File Offset: 0x00023C70
		[SRCategory("CatAppearance")]
		[DefaultValue(false)]
		[SRDescription("CheckedListBoxThreeDCheckBoxesDescr")]
		public bool ThreeDCheckBoxes
		{
			get
			{
				return !this.flat;
			}
			set
			{
				if (this.flat == value)
				{
					this.flat = !value;
					CheckedListBox.ObjectCollection items = this.Items;
					if (items != null && items.Count > 0)
					{
						base.Invalidate();
					}
				}
			}
		}

		/// <summary>Gets or sets a value that determines whether to use the <see cref="T:System.Drawing.Graphics" /> class (GDI+) or the <see cref="T:System.Windows.Forms.TextRenderer" /> class (GDI) to render text.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Drawing.Graphics" /> class should be used to perform text rendering for compatibility with versions 1.0 and 1.1. of the .NET Framework; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000D33 RID: 3379 RVA: 0x00024807 File Offset: 0x00022A07
		// (set) Token: 0x06000D34 RID: 3380 RVA: 0x0002480F File Offset: 0x00022A0F
		[DefaultValue(false)]
		[SRCategory("CatBehavior")]
		[SRDescription("UseCompatibleTextRenderingDescr")]
		public bool UseCompatibleTextRendering
		{
			get
			{
				return base.UseCompatibleTextRenderingInt;
			}
			set
			{
				base.UseCompatibleTextRenderingInt = value;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000D35 RID: 3381 RVA: 0x00012E4E File Offset: 0x0001104E
		internal override bool SupportsUseCompatibleTextRendering
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets or sets a string that specifies the property of the data source from which to draw the value.</summary>
		/// <returns>A string that specifies the property of the data source from which to draw the value.</returns>
		/// <exception cref="T:System.ArgumentException">The specified property cannot be found on the object specified by the <see cref="P:System.Windows.Forms.CheckedListBox.DataSource" /> property.</exception>
		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x00025AA9 File Offset: 0x00023CA9
		// (set) Token: 0x06000D37 RID: 3383 RVA: 0x00025AB1 File Offset: 0x00023CB1
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new string ValueMember
		{
			get
			{
				return base.ValueMember;
			}
			set
			{
				base.ValueMember = value;
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.CheckedListBox.DataSource" /> property changes.</summary>
		// Token: 0x1400006B RID: 107
		// (add) Token: 0x06000D38 RID: 3384 RVA: 0x00025ABA File Offset: 0x00023CBA
		// (remove) Token: 0x06000D39 RID: 3385 RVA: 0x00025AC3 File Offset: 0x00023CC3
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler DataSourceChanged
		{
			add
			{
				base.DataSourceChanged += value;
			}
			remove
			{
				base.DataSourceChanged -= value;
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.CheckedListBox.DisplayMember" /> property changes.</summary>
		// Token: 0x1400006C RID: 108
		// (add) Token: 0x06000D3A RID: 3386 RVA: 0x00025ACC File Offset: 0x00023CCC
		// (remove) Token: 0x06000D3B RID: 3387 RVA: 0x00025AD5 File Offset: 0x00023CD5
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler DisplayMemberChanged
		{
			add
			{
				base.DisplayMemberChanged += value;
			}
			remove
			{
				base.DisplayMemberChanged -= value;
			}
		}

		/// <summary>Occurs when the checked state of an item changes.</summary>
		// Token: 0x1400006D RID: 109
		// (add) Token: 0x06000D3C RID: 3388 RVA: 0x00025ADE File Offset: 0x00023CDE
		// (remove) Token: 0x06000D3D RID: 3389 RVA: 0x00025AF7 File Offset: 0x00023CF7
		[SRCategory("CatBehavior")]
		[SRDescription("CheckedListBoxItemCheckDescr")]
		public event ItemCheckEventHandler ItemCheck
		{
			add
			{
				this.onItemCheck = (ItemCheckEventHandler)Delegate.Combine(this.onItemCheck, value);
			}
			remove
			{
				this.onItemCheck = (ItemCheckEventHandler)Delegate.Remove(this.onItemCheck, value);
			}
		}

		/// <summary>Occurs when the user clicks the <see cref="T:System.Windows.Forms.CheckedListBox" /> control.</summary>
		// Token: 0x1400006E RID: 110
		// (add) Token: 0x06000D3E RID: 3390 RVA: 0x00025B10 File Offset: 0x00023D10
		// (remove) Token: 0x06000D3F RID: 3391 RVA: 0x00025B19 File Offset: 0x00023D19
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

		/// <summary>Occurs when the user clicks the <see cref="T:System.Windows.Forms.CheckedListBox" /> control with the mouse.</summary>
		// Token: 0x1400006F RID: 111
		// (add) Token: 0x06000D40 RID: 3392 RVA: 0x00025B22 File Offset: 0x00023D22
		// (remove) Token: 0x06000D41 RID: 3393 RVA: 0x00025B2B File Offset: 0x00023D2B
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

		/// <summary>Occurs when a visual aspect of an owner-drawn <see cref="T:System.Windows.Forms.CheckedListBox" /> changes. This event is not relevant to this class.</summary>
		// Token: 0x14000070 RID: 112
		// (add) Token: 0x06000D42 RID: 3394 RVA: 0x00025B34 File Offset: 0x00023D34
		// (remove) Token: 0x06000D43 RID: 3395 RVA: 0x00025B3D File Offset: 0x00023D3D
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event DrawItemEventHandler DrawItem
		{
			add
			{
				base.DrawItem += value;
			}
			remove
			{
				base.DrawItem -= value;
			}
		}

		/// <summary>Occurs when an owner-drawn <see cref="T:System.Windows.Forms.ListBox" /> is created and the sizes of the list items are determined. This event is not relevant to this class.</summary>
		// Token: 0x14000071 RID: 113
		// (add) Token: 0x06000D44 RID: 3396 RVA: 0x00025B46 File Offset: 0x00023D46
		// (remove) Token: 0x06000D45 RID: 3397 RVA: 0x00025B4F File Offset: 0x00023D4F
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event MeasureItemEventHandler MeasureItem
		{
			add
			{
				base.MeasureItem += value;
			}
			remove
			{
				base.MeasureItem -= value;
			}
		}

		/// <summary>Gets or sets padding within the <see cref="T:System.Windows.Forms.CheckedListBox" />. This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> representing the control's internal spacing characteristics.</returns>
		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x00025B58 File Offset: 0x00023D58
		// (set) Token: 0x06000D47 RID: 3399 RVA: 0x00025B60 File Offset: 0x00023D60
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.CheckedListBox.ValueMember" /> property changes.</summary>
		// Token: 0x14000072 RID: 114
		// (add) Token: 0x06000D48 RID: 3400 RVA: 0x00025B69 File Offset: 0x00023D69
		// (remove) Token: 0x06000D49 RID: 3401 RVA: 0x00025B72 File Offset: 0x00023D72
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ValueMemberChanged
		{
			add
			{
				base.ValueMemberChanged += value;
			}
			remove
			{
				base.ValueMemberChanged -= value;
			}
		}

		/// <summary>Creates a new accessibility object for the <see cref="T:System.Windows.Forms.CheckedListBox" /> control.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the control.</returns>
		// Token: 0x06000D4A RID: 3402 RVA: 0x00025B7B File Offset: 0x00023D7B
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new CheckedListBox.CheckedListBoxAccessibleObject(this);
		}

		/// <summary>Creates a new instance of the item collection.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> that represents the new item collection.</returns>
		// Token: 0x06000D4B RID: 3403 RVA: 0x00025B83 File Offset: 0x00023D83
		protected override ListBox.ObjectCollection CreateItemCollection()
		{
			return new CheckedListBox.ObjectCollection(this);
		}

		/// <summary>Returns a value indicating the check state of the current item.</summary>
		/// <param name="index">The index of the item to get the checked value of.</param>
		/// <returns>One of the <see cref="T:System.Windows.Forms.CheckState" /> values.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is less than zero.  
		///  -or-  
		///  The <paramref name="index" /> specified is greater than or equal to the count of items in the list.</exception>
		// Token: 0x06000D4C RID: 3404 RVA: 0x00025B8C File Offset: 0x00023D8C
		public CheckState GetItemCheckState(int index)
		{
			if (index < 0 || index >= this.Items.Count)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			return this.CheckedItems.GetCheckedState(index);
		}

		/// <summary>Returns a value indicating whether the specified item is checked.</summary>
		/// <param name="index">The index of the item.</param>
		/// <returns>
		///   <see langword="true" /> if the item is checked; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> specified is less than zero.  
		///  -or-  
		///  The <paramref name="index" /> specified is greater than or equal to the count of items in the list.</exception>
		// Token: 0x06000D4D RID: 3405 RVA: 0x00025BE9 File Offset: 0x00023DE9
		public bool GetItemChecked(int index)
		{
			return this.GetItemCheckState(index) > CheckState.Unchecked;
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x00025BF8 File Offset: 0x00023DF8
		private void InvalidateItem(int index)
		{
			if (base.IsHandleCreated)
			{
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				base.SendMessage(408, index, ref rect);
				SafeNativeMethods.InvalidateRect(new HandleRef(this, base.Handle), ref rect, false);
			}
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x00025C3C File Offset: 0x00023E3C
		private void LbnSelChange()
		{
			int selectedIndex = this.SelectedIndex;
			if (selectedIndex < 0 || selectedIndex >= this.Items.Count)
			{
				return;
			}
			base.AccessibilityNotifyClients(AccessibleEvents.Focus, selectedIndex);
			base.AccessibilityNotifyClients(AccessibleEvents.Selection, selectedIndex);
			if (!this.killnextselect && (selectedIndex == this.lastSelected || this.checkOnClick))
			{
				CheckState checkedState = this.CheckedItems.GetCheckedState(selectedIndex);
				CheckState checkState = ((checkedState != CheckState.Unchecked) ? CheckState.Unchecked : CheckState.Checked);
				ItemCheckEventArgs itemCheckEventArgs = new ItemCheckEventArgs(selectedIndex, checkState, checkedState);
				this.OnItemCheck(itemCheckEventArgs);
				this.CheckedItems.SetCheckedState(selectedIndex, itemCheckEventArgs.NewValue);
				if (AccessibilityImprovements.Level1)
				{
					base.AccessibilityNotifyClients(AccessibleEvents.StateChange, selectedIndex);
					base.AccessibilityNotifyClients(AccessibleEvents.NameChange, selectedIndex);
				}
			}
			this.lastSelected = selectedIndex;
			this.InvalidateItem(selectedIndex);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.CheckedListBox.Click" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000D50 RID: 3408 RVA: 0x00025CF8 File Offset: 0x00023EF8
		protected override void OnClick(EventArgs e)
		{
			this.killnextselect = false;
			base.OnClick(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000D51 RID: 3409 RVA: 0x00025D08 File Offset: 0x00023F08
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			base.SendMessage(416, 0, this.ItemHeight);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.CheckedListBox.DrawItem" /> event.</summary>
		/// <param name="e">The <see cref="T:System.Windows.Forms.DrawItemEventArgs" /> object with the details</param>
		// Token: 0x06000D52 RID: 3410 RVA: 0x00025D24 File Offset: 0x00023F24
		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			if (this.Font.Height < 0)
			{
				this.Font = Control.DefaultFont;
			}
			if (e.Index >= 0)
			{
				object obj;
				if (e.Index < this.Items.Count)
				{
					obj = this.Items[e.Index];
				}
				else
				{
					obj = base.NativeGetItemText(e.Index);
				}
				Rectangle bounds = e.Bounds;
				int itemHeight = this.ItemHeight;
				ButtonState buttonState = ButtonState.Normal;
				if (this.flat)
				{
					buttonState |= ButtonState.Flat;
				}
				if (e.Index < this.Items.Count)
				{
					CheckState checkedState = this.CheckedItems.GetCheckedState(e.Index);
					if (checkedState != CheckState.Checked)
					{
						if (checkedState == CheckState.Indeterminate)
						{
							buttonState |= ButtonState.Checked | ButtonState.Inactive;
						}
					}
					else
					{
						buttonState |= ButtonState.Checked;
					}
				}
				if (Application.RenderWithVisualStyles)
				{
					CheckBoxState checkBoxState = CheckBoxRenderer.ConvertFromButtonState(buttonState, false, (e.State & DrawItemState.HotLight) == DrawItemState.HotLight);
					this.idealCheckSize = CheckBoxRenderer.GetGlyphSize(e.Graphics, checkBoxState, base.HandleInternal).Width;
				}
				int num = Math.Max((itemHeight - this.idealCheckSize) / 2, 0);
				if (num + this.idealCheckSize > bounds.Height)
				{
					num = bounds.Height - this.idealCheckSize;
				}
				Rectangle rectangle = new Rectangle(bounds.X + this.scaledListItemStartPosition, bounds.Y + num, this.idealCheckSize, this.idealCheckSize);
				if (this.RightToLeft == RightToLeft.Yes)
				{
					rectangle.X = bounds.X + bounds.Width - this.idealCheckSize - this.scaledListItemStartPosition;
				}
				if (Application.RenderWithVisualStyles)
				{
					CheckBoxState checkBoxState2 = CheckBoxRenderer.ConvertFromButtonState(buttonState, false, (e.State & DrawItemState.HotLight) == DrawItemState.HotLight);
					CheckBoxRenderer.DrawCheckBox(e.Graphics, new Point(rectangle.X, rectangle.Y), checkBoxState2, base.HandleInternal);
				}
				else
				{
					ControlPaint.DrawCheckBox(e.Graphics, rectangle, buttonState);
				}
				Rectangle rectangle2 = new Rectangle(bounds.X + this.idealCheckSize + this.scaledListItemStartPosition * 2, bounds.Y, bounds.Width - (this.idealCheckSize + this.scaledListItemStartPosition * 2), bounds.Height);
				if (this.RightToLeft == RightToLeft.Yes)
				{
					rectangle2.X = bounds.X;
				}
				string text = "";
				Color color = ((this.SelectionMode != SelectionMode.None) ? e.BackColor : this.BackColor);
				Color color2 = ((this.SelectionMode != SelectionMode.None) ? e.ForeColor : this.ForeColor);
				if (!base.Enabled)
				{
					color2 = SystemColors.GrayText;
				}
				Font font = this.Font;
				text = base.GetItemText(obj);
				if (this.SelectionMode != SelectionMode.None && (e.State & DrawItemState.Selected) == DrawItemState.Selected)
				{
					if (base.Enabled)
					{
						color = SystemColors.Highlight;
						color2 = SystemColors.HighlightText;
					}
					else
					{
						color = SystemColors.InactiveBorder;
						color2 = SystemColors.GrayText;
					}
				}
				using (Brush brush = new SolidBrush(color))
				{
					e.Graphics.FillRectangle(brush, rectangle2);
				}
				Rectangle rectangle3 = new Rectangle(rectangle2.X + 1, rectangle2.Y, rectangle2.Width - 1, rectangle2.Height - 2);
				if (this.UseCompatibleTextRendering)
				{
					using (StringFormat stringFormat = new StringFormat())
					{
						if (base.UseTabStops)
						{
							float num2 = 3.6f * (float)this.Font.Height;
							float[] array = new float[15];
							float num3 = (float)(-(float)(this.idealCheckSize + this.scaledListItemStartPosition * 2));
							for (int i = 1; i < array.Length; i++)
							{
								array[i] = num2;
							}
							if (Math.Abs(num3) < num2)
							{
								array[0] = num2 + num3;
							}
							else
							{
								array[0] = num2;
							}
							stringFormat.SetTabStops(0f, array);
						}
						else if (base.UseCustomTabOffsets)
						{
							int count = base.CustomTabOffsets.Count;
							float[] array2 = new float[count];
							base.CustomTabOffsets.CopyTo(array2, 0);
							stringFormat.SetTabStops(0f, array2);
						}
						if (this.RightToLeft == RightToLeft.Yes)
						{
							stringFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
						}
						stringFormat.FormatFlags |= StringFormatFlags.NoWrap;
						stringFormat.Trimming = StringTrimming.None;
						using (SolidBrush solidBrush = new SolidBrush(color2))
						{
							e.Graphics.DrawString(text, font, solidBrush, rectangle3, stringFormat);
							goto IL_4A0;
						}
					}
				}
				TextFormatFlags textFormatFlags = TextFormatFlags.Default;
				textFormatFlags |= TextFormatFlags.NoPrefix;
				if (base.UseTabStops || base.UseCustomTabOffsets)
				{
					textFormatFlags |= TextFormatFlags.ExpandTabs;
				}
				if (this.RightToLeft == RightToLeft.Yes)
				{
					textFormatFlags |= TextFormatFlags.RightToLeft;
					textFormatFlags |= TextFormatFlags.Right;
				}
				TextRenderer.DrawText(e.Graphics, text, font, rectangle3, color2, textFormatFlags);
				IL_4A0:
				if ((e.State & DrawItemState.Focus) == DrawItemState.Focus && (e.State & DrawItemState.NoFocusRect) != DrawItemState.NoFocusRect)
				{
					ControlPaint.DrawFocusRectangle(e.Graphics, rectangle2, color2, color);
				}
			}
			if (this.Items.Count == 0 && AccessibilityImprovements.Level3 && e.Bounds.Width > 2 && e.Bounds.Height > 2)
			{
				Color color3 = ((this.SelectionMode != SelectionMode.None) ? e.BackColor : this.BackColor);
				Rectangle bounds2 = e.Bounds;
				Rectangle rectangle4 = new Rectangle(bounds2.X + 1, bounds2.Y, bounds2.Width - 1, bounds2.Height - 2);
				if (this.Focused)
				{
					Color color4 = ((this.SelectionMode != SelectionMode.None) ? e.ForeColor : this.ForeColor);
					if (!base.Enabled)
					{
						color4 = SystemColors.GrayText;
					}
					ControlPaint.DrawFocusRectangle(e.Graphics, rectangle4, color4, color3);
					return;
				}
				if (!Application.RenderWithVisualStyles)
				{
					using (Brush brush2 = new SolidBrush(color3))
					{
						e.Graphics.FillRectangle(brush2, rectangle4);
					}
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000D53 RID: 3411 RVA: 0x00026364 File Offset: 0x00024564
		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);
			if (base.IsHandleCreated)
			{
				SafeNativeMethods.InvalidateRect(new HandleRef(this, base.Handle), null, true);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000D54 RID: 3412 RVA: 0x00026389 File Offset: 0x00024589
		protected override void OnFontChanged(EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				base.SendMessage(416, 0, this.ItemHeight);
			}
			base.OnFontChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event.</summary>
		/// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that was raised.</param>
		// Token: 0x06000D55 RID: 3413 RVA: 0x000263AD File Offset: 0x000245AD
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			if (e.KeyChar == ' ' && this.SelectionMode != SelectionMode.None)
			{
				this.LbnSelChange();
			}
			if (base.FormattingEnabled)
			{
				base.OnKeyPress(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.CheckedListBox.ItemCheck" /> event.</summary>
		/// <param name="ice">An <see cref="T:System.Windows.Forms.ItemCheckEventArgs" /> that contains the event data.</param>
		// Token: 0x06000D56 RID: 3414 RVA: 0x000263D6 File Offset: 0x000245D6
		protected virtual void OnItemCheck(ItemCheckEventArgs ice)
		{
			if (this.onItemCheck != null)
			{
				this.onItemCheck(this, ice);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.CheckedListBox.MeasureItem" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MeasureItemEventArgs" /> that contains the event data.</param>
		// Token: 0x06000D57 RID: 3415 RVA: 0x000263ED File Offset: 0x000245ED
		protected override void OnMeasureItem(MeasureItemEventArgs e)
		{
			base.OnMeasureItem(e);
			if (e.ItemHeight < this.idealCheckSize + 2)
			{
				e.ItemHeight = this.idealCheckSize + 2;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListBox.SelectedIndexChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000D58 RID: 3416 RVA: 0x00026414 File Offset: 0x00024614
		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			base.OnSelectedIndexChanged(e);
			this.lastSelected = this.SelectedIndex;
		}

		/// <summary>Parses all <see cref="T:System.Windows.Forms.CheckedListBox" /> items again and gets new text strings for the items.</summary>
		// Token: 0x06000D59 RID: 3417 RVA: 0x0002642C File Offset: 0x0002462C
		protected override void RefreshItems()
		{
			Hashtable hashtable = new Hashtable();
			for (int i = 0; i < this.Items.Count; i++)
			{
				hashtable[i] = this.CheckedItems.GetCheckedState(i);
			}
			base.RefreshItems();
			for (int j = 0; j < this.Items.Count; j++)
			{
				this.CheckedItems.SetCheckedState(j, (CheckState)hashtable[j]);
			}
		}

		/// <summary>Sets the check state of the item at the specified index.</summary>
		/// <param name="index">The index of the item to set the state for.</param>
		/// <param name="value">One of the <see cref="T:System.Windows.Forms.CheckState" /> values.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is less than zero.  
		///  -or-  
		///  The <paramref name="index" /> is greater than or equal to the count of items in the list.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="value" /> is not one of the <see cref="T:System.Windows.Forms.CheckState" /> values.</exception>
		// Token: 0x06000D5A RID: 3418 RVA: 0x000264AC File Offset: 0x000246AC
		public void SetItemCheckState(int index, CheckState value)
		{
			if (index < 0 || index >= this.Items.Count)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
			{
				throw new InvalidEnumArgumentException("value", (int)value, typeof(CheckState));
			}
			CheckState checkedState = this.CheckedItems.GetCheckedState(index);
			if (value != checkedState)
			{
				ItemCheckEventArgs itemCheckEventArgs = new ItemCheckEventArgs(index, value, checkedState);
				this.OnItemCheck(itemCheckEventArgs);
				if (itemCheckEventArgs.NewValue != checkedState)
				{
					this.CheckedItems.SetCheckedState(index, itemCheckEventArgs.NewValue);
					this.InvalidateItem(index);
				}
			}
		}

		/// <summary>Sets <see cref="T:System.Windows.Forms.CheckState" /> for the item at the specified index to <see langword="Checked" />.</summary>
		/// <param name="index">The index of the item to set the check state for.</param>
		/// <param name="value">
		///   <see langword="true" /> to set the item as checked; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentException">The index specified is less than zero.  
		///  -or-  
		///  The index is greater than the count of items in the list.</exception>
		// Token: 0x06000D5B RID: 3419 RVA: 0x00026566 File Offset: 0x00024766
		public void SetItemChecked(int index, bool value)
		{
			this.SetItemCheckState(index, value ? CheckState.Checked : CheckState.Unchecked);
		}

		/// <summary>Processes the command message the <see cref="T:System.Windows.Forms.CheckedListBox" /> control receives from the top-level window.</summary>
		/// <param name="m">The <see cref="T:System.Windows.Forms.Message" /> the top-level window sent to the <see cref="T:System.Windows.Forms.CheckedListBox" /> control.</param>
		// Token: 0x06000D5C RID: 3420 RVA: 0x00026578 File Offset: 0x00024778
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WmReflectCommand(ref Message m)
		{
			int num = NativeMethods.Util.HIWORD(m.WParam);
			if (num == 1)
			{
				this.LbnSelChange();
				base.WmReflectCommand(ref m);
				return;
			}
			if (num != 2)
			{
				base.WmReflectCommand(ref m);
				return;
			}
			this.LbnSelChange();
			base.WmReflectCommand(ref m);
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x000265C0 File Offset: 0x000247C0
		private void WmReflectVKeyToItem(ref Message m)
		{
			int num = NativeMethods.Util.LOWORD(m.WParam);
			Keys keys = (Keys)num;
			if (keys - Keys.Prior <= 7)
			{
				this.killnextselect = true;
			}
			else
			{
				this.killnextselect = false;
			}
			m.Result = NativeMethods.InvalidIntPtr;
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x06000D5E RID: 3422 RVA: 0x00026600 File Offset: 0x00024800
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg == 8238)
			{
				this.WmReflectVKeyToItem(ref m);
				return;
			}
			if (msg == 8239)
			{
				m.Result = NativeMethods.InvalidIntPtr;
				return;
			}
			if (m.Msg == CheckedListBox.LBC_GETCHECKSTATE)
			{
				int num = (int)(long)m.WParam;
				if (num < 0 || num >= this.Items.Count)
				{
					m.Result = (IntPtr)(-1);
					return;
				}
				m.Result = (IntPtr)(this.GetItemChecked(num) ? 1 : 0);
				return;
			}
			else
			{
				if (m.Msg != CheckedListBox.LBC_SETCHECKSTATE)
				{
					base.WndProc(ref m);
					return;
				}
				int num2 = (int)(long)m.WParam;
				int num3 = (int)(long)m.LParam;
				if (num2 < 0 || num2 >= this.Items.Count || (num3 != 1 && num3 != 0))
				{
					m.Result = IntPtr.Zero;
					return;
				}
				this.SetItemChecked(num2, num3 == 1);
				m.Result = (IntPtr)1;
				return;
			}
		}

		// Token: 0x04000767 RID: 1895
		private int idealCheckSize = 13;

		// Token: 0x04000768 RID: 1896
		private const int LB_CHECKED = 1;

		// Token: 0x04000769 RID: 1897
		private const int LB_UNCHECKED = 0;

		// Token: 0x0400076A RID: 1898
		private const int LB_ERROR = -1;

		// Token: 0x0400076B RID: 1899
		private const int BORDER_SIZE = 1;

		// Token: 0x0400076C RID: 1900
		private bool killnextselect;

		// Token: 0x0400076D RID: 1901
		private ItemCheckEventHandler onItemCheck;

		// Token: 0x0400076E RID: 1902
		private bool checkOnClick;

		// Token: 0x0400076F RID: 1903
		private bool flat = true;

		// Token: 0x04000770 RID: 1904
		private int lastSelected = -1;

		// Token: 0x04000771 RID: 1905
		private CheckedListBox.CheckedItemCollection checkedItemCollection;

		// Token: 0x04000772 RID: 1906
		private CheckedListBox.CheckedIndexCollection checkedIndexCollection;

		// Token: 0x04000773 RID: 1907
		private static int LBC_GETCHECKSTATE = SafeNativeMethods.RegisterWindowMessage("LBC_GETCHECKSTATE");

		// Token: 0x04000774 RID: 1908
		private static int LBC_SETCHECKSTATE = SafeNativeMethods.RegisterWindowMessage("LBC_SETCHECKSTATE");

		/// <summary>Represents the collection of items in a <see cref="T:System.Windows.Forms.CheckedListBox" />.</summary>
		// Token: 0x0200061C RID: 1564
		public new class ObjectCollection : ListBox.ObjectCollection
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.CheckedListBox.ObjectCollection" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.CheckedListBox" /> that owns the collection.</param>
			// Token: 0x060062FA RID: 25338 RVA: 0x0016E1EC File Offset: 0x0016C3EC
			public ObjectCollection(CheckedListBox owner)
				: base(owner)
			{
				this.owner = owner;
			}

			/// <summary>Adds an item to the list of items for a <see cref="T:System.Windows.Forms.CheckedListBox" />, specifying the object to add and whether it is checked.</summary>
			/// <param name="item">An object representing the item to add to the collection.</param>
			/// <param name="isChecked">
			///   <see langword="true" /> to check the item; otherwise, <see langword="false" />.</param>
			/// <returns>The index of the newly added item.</returns>
			// Token: 0x060062FB RID: 25339 RVA: 0x0016E1FC File Offset: 0x0016C3FC
			public int Add(object item, bool isChecked)
			{
				return this.Add(item, isChecked ? CheckState.Checked : CheckState.Unchecked);
			}

			/// <summary>Adds an item to the list of items for a <see cref="T:System.Windows.Forms.CheckedListBox" />, specifying the object to add and the initial checked value.</summary>
			/// <param name="item">An object representing the item to add to the collection.</param>
			/// <param name="check">The initial <see cref="T:System.Windows.Forms.CheckState" /> for the checked portion of the item.</param>
			/// <returns>The index of the newly added item.</returns>
			/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="check" /> parameter is not one of the valid <see cref="T:System.Windows.Forms.CheckState" /> values.</exception>
			// Token: 0x060062FC RID: 25340 RVA: 0x0016E20C File Offset: 0x0016C40C
			public int Add(object item, CheckState check)
			{
				if (!ClientUtils.IsEnumValid(check, (int)check, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)check, typeof(CheckState));
				}
				int num = base.Add(item);
				this.owner.SetItemCheckState(num, check);
				return num;
			}

			// Token: 0x04003915 RID: 14613
			private CheckedListBox owner;
		}

		/// <summary>Encapsulates the collection of indexes of checked items (including items in an indeterminate state) in a <see cref="T:System.Windows.Forms.CheckedListBox" />.</summary>
		// Token: 0x0200061D RID: 1565
		public class CheckedIndexCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x060062FD RID: 25341 RVA: 0x0016E255 File Offset: 0x0016C455
			internal CheckedIndexCollection(CheckedListBox owner)
			{
				this.owner = owner;
			}

			/// <summary>Gets the number of checked items.</summary>
			/// <returns>The number of indexes in the collection.</returns>
			// Token: 0x1700151A RID: 5402
			// (get) Token: 0x060062FE RID: 25342 RVA: 0x0016E264 File Offset: 0x0016C464
			public int Count
			{
				get
				{
					return this.owner.CheckedItems.Count;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection of controls. For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
			/// <returns>The <see cref="T:System.Object" /> used to synchronize to the collection.</returns>
			// Token: 0x1700151B RID: 5403
			// (get) Token: 0x060062FF RID: 25343 RVA: 0x00006A49 File Offset: 0x00004C49
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>Gets a value indicating whether access to the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedIndexCollection" /> is synchronized (thread safe).</summary>
			/// <returns>
			///   <see langword="false" /> in all cases.</returns>
			// Token: 0x1700151C RID: 5404
			// (get) Token: 0x06006300 RID: 25344 RVA: 0x0001180C File Offset: 0x0000FA0C
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
			/// <returns>
			///   <see langword="true" /> in all cases.</returns>
			// Token: 0x1700151D RID: 5405
			// (get) Token: 0x06006301 RID: 25345 RVA: 0x00012E4E File Offset: 0x0001104E
			bool IList.IsFixedSize
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>
			///   <see langword="true" /> in all cases.</returns>
			// Token: 0x1700151E RID: 5406
			// (get) Token: 0x06006302 RID: 25346 RVA: 0x00012E4E File Offset: 0x0001104E
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets the index of a checked item in the <see cref="T:System.Windows.Forms.CheckedListBox" /> control.</summary>
			/// <param name="index">An index into the checked indexes collection. This index specifies the index of the checked item you want to retrieve.</param>
			/// <returns>The index of the checked item. For more information, see the examples in the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedIndexCollection" /> class overview.</returns>
			/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> is less than zero.  
			///  -or-  
			///  The <paramref name="index" /> is not in the collection.</exception>
			// Token: 0x1700151F RID: 5407
			[Browsable(false)]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
			public int this[int index]
			{
				get
				{
					object entryObject = this.InnerArray.GetEntryObject(index, CheckedListBox.CheckedItemCollection.AnyMask);
					return this.InnerArray.IndexOfIdentifier(entryObject, 0);
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
			/// <param name="index">The zero-based index of the item to get.</param>
			/// <returns>The index value from the <see cref="T:System.Windows.Forms.CheckedListBox.ObjectCollection" /> that is stored at the specified location.</returns>
			// Token: 0x17001520 RID: 5408
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					throw new NotSupportedException(SR.GetString("CheckedListBoxCheckedIndexCollectionIsReadOnly"));
				}
			}

			/// <summary>Adds an item to the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedIndexCollection" />. For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
			/// <param name="value">The object to be added to the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedIndexCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06006306 RID: 25350 RVA: 0x0016E2B2 File Offset: 0x0016C4B2
			int IList.Add(object value)
			{
				throw new NotSupportedException(SR.GetString("CheckedListBoxCheckedIndexCollectionIsReadOnly"));
			}

			/// <summary>Removes all items from the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedIndexCollection" />. For a description of this member, see <see cref="M:System.Collections.IList.Clear" />.</summary>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06006307 RID: 25351 RVA: 0x0016E2B2 File Offset: 0x0016C4B2
			void IList.Clear()
			{
				throw new NotSupportedException(SR.GetString("CheckedListBoxCheckedIndexCollectionIsReadOnly"));
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
			/// <param name="index">The index at which value should be inserted.</param>
			/// <param name="value">The object to be added to the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedIndexCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06006308 RID: 25352 RVA: 0x0016E2B2 File Offset: 0x0016C4B2
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException(SR.GetString("CheckedListBoxCheckedIndexCollectionIsReadOnly"));
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
			/// <param name="value">The object to be removed from the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedIndexCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06006309 RID: 25353 RVA: 0x0016E2B2 File Offset: 0x0016C4B2
			void IList.Remove(object value)
			{
				throw new NotSupportedException(SR.GetString("CheckedListBoxCheckedIndexCollectionIsReadOnly"));
			}

			/// <summary>or a description of this member, see <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" />.</summary>
			/// <param name="index">The zero-based index of the item to remove.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x0600630A RID: 25354 RVA: 0x0016E2B2 File Offset: 0x0016C4B2
			void IList.RemoveAt(int index)
			{
				throw new NotSupportedException(SR.GetString("CheckedListBoxCheckedIndexCollectionIsReadOnly"));
			}

			/// <summary>Determines whether the specified index is located in the collection.</summary>
			/// <param name="index">The index to locate in the collection.</param>
			/// <returns>
			///   <see langword="true" /> if the specified index from the <see cref="T:System.Windows.Forms.CheckedListBox.ObjectCollection" /> is an item in this collection; otherwise, <see langword="false" />.</returns>
			// Token: 0x0600630B RID: 25355 RVA: 0x0016E2C3 File Offset: 0x0016C4C3
			public bool Contains(int index)
			{
				return this.IndexOf(index) != -1;
			}

			/// <summary>Determines whether the specified index is located within the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedIndexCollection" />. For a description of this member, see <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
			/// <param name="index">The index to locate in the collection.</param>
			/// <returns>
			///   <see langword="true" /> if the specified index from the <see cref="T:System.Windows.Forms.CheckedListBox.ObjectCollection" /> for the <see cref="T:System.Windows.Forms.CheckedListBox" /> is an item in this collection; otherwise, <see langword="false" />.</returns>
			// Token: 0x0600630C RID: 25356 RVA: 0x0016E2D2 File Offset: 0x0016C4D2
			bool IList.Contains(object index)
			{
				return index is int && this.Contains((int)index);
			}

			/// <summary>Copies the entire collection into an existing array at a specified location within the array.</summary>
			/// <param name="dest">The destination array.</param>
			/// <param name="index">The zero-based relative index in <paramref name="dest" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is <see langword="null" />.</exception>
			/// <exception cref="T:System.RankException">
			///   <paramref name="array" /> is multidimensional.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than zero.</exception>
			/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Array" /> is greater than the available space from index to the end of the destination <see cref="T:System.Array" />.</exception>
			/// <exception cref="T:System.ArrayTypeMismatchException">The type of the source <see cref="T:System.Array" /> cannot be cast automatically to the type of the destination <see cref="T:System.Array" />.</exception>
			// Token: 0x0600630D RID: 25357 RVA: 0x0016E2EC File Offset: 0x0016C4EC
			public void CopyTo(Array dest, int index)
			{
				int count = this.owner.CheckedItems.Count;
				for (int i = 0; i < count; i++)
				{
					dest.SetValue(this[i], i + index);
				}
			}

			// Token: 0x17001521 RID: 5409
			// (get) Token: 0x0600630E RID: 25358 RVA: 0x0016E32B File Offset: 0x0016C52B
			private ListBox.ItemArray InnerArray
			{
				get
				{
					return this.owner.Items.InnerArray;
				}
			}

			/// <summary>Returns an enumerator that can be used to iterate through the <see cref="P:System.Windows.Forms.CheckedListBox.CheckedIndices" /> collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for navigating through the list.</returns>
			// Token: 0x0600630F RID: 25359 RVA: 0x0016E340 File Offset: 0x0016C540
			public IEnumerator GetEnumerator()
			{
				int[] array = new int[this.Count];
				this.CopyTo(array, 0);
				return array.GetEnumerator();
			}

			/// <summary>Returns an index into the collection of checked indexes.</summary>
			/// <param name="index">The index of the checked item.</param>
			/// <returns>The index that specifies the index of the checked item or -1 if the <paramref name="index" /> parameter is not in the checked indexes collection. For more information, see the examples in the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedIndexCollection" /> class overview.</returns>
			// Token: 0x06006310 RID: 25360 RVA: 0x0016E368 File Offset: 0x0016C568
			public int IndexOf(int index)
			{
				if (index >= 0 && index < this.owner.Items.Count)
				{
					object entryObject = this.InnerArray.GetEntryObject(index, 0);
					return this.owner.CheckedItems.IndexOfIdentifier(entryObject);
				}
				return -1;
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
			/// <param name="index">The zero-based index from the <see cref="T:System.Windows.Forms.CheckedListBox.ObjectCollection" /> to locate in this collection.</param>
			/// <returns>This member is an explicit interface member implementation. It can be used only when the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedIndexCollection" /> instance is cast to an <see cref="T:System.Collections.IList" /> interface.</returns>
			// Token: 0x06006311 RID: 25361 RVA: 0x0016E3AD File Offset: 0x0016C5AD
			int IList.IndexOf(object index)
			{
				if (index is int)
				{
					return this.IndexOf((int)index);
				}
				return -1;
			}

			// Token: 0x04003916 RID: 14614
			private CheckedListBox owner;
		}

		/// <summary>Encapsulates the collection of checked items, including items in an indeterminate state, in a <see cref="T:System.Windows.Forms.CheckedListBox" /> control.</summary>
		// Token: 0x0200061E RID: 1566
		public class CheckedItemCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x06006312 RID: 25362 RVA: 0x0016E3C5 File Offset: 0x0016C5C5
			internal CheckedItemCollection(CheckedListBox owner)
			{
				this.owner = owner;
			}

			/// <summary>Gets the number of items in the collection.</summary>
			/// <returns>The number of items in the collection.</returns>
			// Token: 0x17001522 RID: 5410
			// (get) Token: 0x06006313 RID: 25363 RVA: 0x0016E3D4 File Offset: 0x0016C5D4
			public int Count
			{
				get
				{
					return this.InnerArray.GetCount(CheckedListBox.CheckedItemCollection.AnyMask);
				}
			}

			// Token: 0x17001523 RID: 5411
			// (get) Token: 0x06006314 RID: 25364 RVA: 0x0016E3E6 File Offset: 0x0016C5E6
			private ListBox.ItemArray InnerArray
			{
				get
				{
					return this.owner.Items.InnerArray;
				}
			}

			/// <summary>Gets an object in the checked items collection.</summary>
			/// <param name="index">An index into the collection of checked items. This collection index corresponds to the index of the checked item.</param>
			/// <returns>The object at the specified index. For more information, see the examples in the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedItemCollection" /> class overview.</returns>
			/// <exception cref="T:System.NotSupportedException">The object cannot be set.</exception>
			// Token: 0x17001524 RID: 5412
			[Browsable(false)]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
			public object this[int index]
			{
				get
				{
					return this.InnerArray.GetItem(index, CheckedListBox.CheckedItemCollection.AnyMask);
				}
				set
				{
					throw new NotSupportedException(SR.GetString("CheckedListBoxCheckedItemCollectionIsReadOnly"));
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
			/// <returns>The <see cref="T:System.Object" /> used to synchronize to the collection.</returns>
			// Token: 0x17001525 RID: 5413
			// (get) Token: 0x06006317 RID: 25367 RVA: 0x00006A49 File Offset: 0x00004C49
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
			/// <returns>
			///   <see langword="false" /> in all cases.</returns>
			// Token: 0x17001526 RID: 5414
			// (get) Token: 0x06006318 RID: 25368 RVA: 0x0001180C File Offset: 0x0000FA0C
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
			/// <returns>
			///   <see langword="false" /> in all cases.</returns>
			// Token: 0x17001527 RID: 5415
			// (get) Token: 0x06006319 RID: 25369 RVA: 0x00012E4E File Offset: 0x0001104E
			bool IList.IsFixedSize
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets a value indicating if the collection is read-only.</summary>
			/// <returns>Always <see langword="true" />.</returns>
			// Token: 0x17001528 RID: 5416
			// (get) Token: 0x0600631A RID: 25370 RVA: 0x00012E4E File Offset: 0x0001104E
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			/// <summary>Determines whether the specified item is located in the collection.</summary>
			/// <param name="item">An object from the items collection.</param>
			/// <returns>
			///   <see langword="true" /> if item is in the collection; otherwise, <see langword="false" />.</returns>
			// Token: 0x0600631B RID: 25371 RVA: 0x0016E41C File Offset: 0x0016C61C
			public bool Contains(object item)
			{
				return this.IndexOf(item) != -1;
			}

			/// <summary>Returns an index into the collection of checked items.</summary>
			/// <param name="item">The object whose index you want to retrieve. This object must belong to the checked items collection.</param>
			/// <returns>The index of the object in the checked item collection or -1 if the object is not in the collection. For more information, see the examples in the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedItemCollection" /> class overview.</returns>
			// Token: 0x0600631C RID: 25372 RVA: 0x0016E42B File Offset: 0x0016C62B
			public int IndexOf(object item)
			{
				return this.InnerArray.IndexOf(item, CheckedListBox.CheckedItemCollection.AnyMask);
			}

			// Token: 0x0600631D RID: 25373 RVA: 0x0016E43E File Offset: 0x0016C63E
			internal int IndexOfIdentifier(object item)
			{
				return this.InnerArray.IndexOfIdentifier(item, CheckedListBox.CheckedItemCollection.AnyMask);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
			/// <param name="value">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
			/// <returns>The zero-based index of the item to add.</returns>
			// Token: 0x0600631E RID: 25374 RVA: 0x0016E40B File Offset: 0x0016C60B
			int IList.Add(object value)
			{
				throw new NotSupportedException(SR.GetString("CheckedListBoxCheckedItemCollectionIsReadOnly"));
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Clear" />.</summary>
			// Token: 0x0600631F RID: 25375 RVA: 0x0016E40B File Offset: 0x0016C60B
			void IList.Clear()
			{
				throw new NotSupportedException(SR.GetString("CheckedListBoxCheckedItemCollectionIsReadOnly"));
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
			/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
			/// <param name="value">The item to insert into the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedItemCollection" />.</param>
			// Token: 0x06006320 RID: 25376 RVA: 0x0016E40B File Offset: 0x0016C60B
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException(SR.GetString("CheckedListBoxCheckedItemCollectionIsReadOnly"));
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
			/// <param name="value">The item to remove from the <see cref="T:System.Windows.Forms.CheckedListBox.CheckedItemCollection" />.</param>
			// Token: 0x06006321 RID: 25377 RVA: 0x0016E40B File Offset: 0x0016C60B
			void IList.Remove(object value)
			{
				throw new NotSupportedException(SR.GetString("CheckedListBoxCheckedItemCollectionIsReadOnly"));
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" />.</summary>
			/// <param name="index">The zero-based index of the item to remove.</param>
			// Token: 0x06006322 RID: 25378 RVA: 0x0016E40B File Offset: 0x0016C60B
			void IList.RemoveAt(int index)
			{
				throw new NotSupportedException(SR.GetString("CheckedListBoxCheckedItemCollectionIsReadOnly"));
			}

			/// <summary>Copies the entire collection into an existing array at a specified location within the array.</summary>
			/// <param name="dest">The destination array.</param>
			/// <param name="index">The zero-based relative index in <paramref name="dest" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is <see langword="null" />.</exception>
			/// <exception cref="T:System.RankException">
			///   <paramref name="array" /> is multidimensional.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than zero.</exception>
			/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Array" /> is greater than the available space from index to the end of the destination <see cref="T:System.Array" />.</exception>
			/// <exception cref="T:System.ArrayTypeMismatchException">The type of the source <see cref="T:System.Array" /> cannot be cast automatically to the type of the destination <see cref="T:System.Array" />.</exception>
			// Token: 0x06006323 RID: 25379 RVA: 0x0016E454 File Offset: 0x0016C654
			public void CopyTo(Array dest, int index)
			{
				int count = this.InnerArray.GetCount(CheckedListBox.CheckedItemCollection.AnyMask);
				for (int i = 0; i < count; i++)
				{
					dest.SetValue(this.InnerArray.GetItem(i, CheckedListBox.CheckedItemCollection.AnyMask), i + index);
				}
			}

			// Token: 0x06006324 RID: 25380 RVA: 0x0016E498 File Offset: 0x0016C698
			internal CheckState GetCheckedState(int index)
			{
				bool state = this.InnerArray.GetState(index, CheckedListBox.CheckedItemCollection.CheckedItemMask);
				bool state2 = this.InnerArray.GetState(index, CheckedListBox.CheckedItemCollection.IndeterminateItemMask);
				if (state2)
				{
					return CheckState.Indeterminate;
				}
				if (state)
				{
					return CheckState.Checked;
				}
				return CheckState.Unchecked;
			}

			/// <summary>Returns an enumerator that can be used to iterate through the <see cref="P:System.Windows.Forms.CheckedListBox.CheckedItems" /> collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for navigating through the list.</returns>
			// Token: 0x06006325 RID: 25381 RVA: 0x0016E4D4 File Offset: 0x0016C6D4
			public IEnumerator GetEnumerator()
			{
				return this.InnerArray.GetEnumerator(CheckedListBox.CheckedItemCollection.AnyMask, true);
			}

			// Token: 0x06006326 RID: 25382 RVA: 0x0016E4E8 File Offset: 0x0016C6E8
			internal void SetCheckedState(int index, CheckState value)
			{
				bool flag;
				bool flag2;
				if (value != CheckState.Checked)
				{
					if (value != CheckState.Indeterminate)
					{
						flag = false;
						flag2 = false;
					}
					else
					{
						flag = false;
						flag2 = true;
					}
				}
				else
				{
					flag = true;
					flag2 = false;
				}
				bool state = this.InnerArray.GetState(index, CheckedListBox.CheckedItemCollection.CheckedItemMask);
				bool state2 = this.InnerArray.GetState(index, CheckedListBox.CheckedItemCollection.IndeterminateItemMask);
				this.InnerArray.SetState(index, CheckedListBox.CheckedItemCollection.CheckedItemMask, flag);
				this.InnerArray.SetState(index, CheckedListBox.CheckedItemCollection.IndeterminateItemMask, flag2);
				if (state != flag || state2 != flag2)
				{
					this.owner.AccessibilityNotifyClients(AccessibleEvents.StateChange, index);
				}
			}

			// Token: 0x04003917 RID: 14615
			internal static int CheckedItemMask = ListBox.ItemArray.CreateMask();

			// Token: 0x04003918 RID: 14616
			internal static int IndeterminateItemMask = ListBox.ItemArray.CreateMask();

			// Token: 0x04003919 RID: 14617
			internal static int AnyMask = CheckedListBox.CheckedItemCollection.CheckedItemMask | CheckedListBox.CheckedItemCollection.IndeterminateItemMask;

			// Token: 0x0400391A RID: 14618
			private CheckedListBox owner;
		}

		// Token: 0x0200061F RID: 1567
		[ComVisible(true)]
		internal class CheckedListBoxAccessibleObject : Control.ControlAccessibleObject
		{
			// Token: 0x06006328 RID: 25384 RVA: 0x0009B733 File Offset: 0x00099933
			public CheckedListBoxAccessibleObject(CheckedListBox owner)
				: base(owner)
			{
			}

			// Token: 0x17001529 RID: 5417
			// (get) Token: 0x06006329 RID: 25385 RVA: 0x0016E596 File Offset: 0x0016C796
			private CheckedListBox CheckedListBox
			{
				get
				{
					return (CheckedListBox)base.Owner;
				}
			}

			// Token: 0x0600632A RID: 25386 RVA: 0x0016E5A3 File Offset: 0x0016C7A3
			public override AccessibleObject GetChild(int index)
			{
				if (index >= 0 && index < this.CheckedListBox.Items.Count)
				{
					return new CheckedListBox.CheckedListBoxItemAccessibleObject(this.CheckedListBox.GetItemText(this.CheckedListBox.Items[index]), index, this);
				}
				return null;
			}

			// Token: 0x0600632B RID: 25387 RVA: 0x0016E5E1 File Offset: 0x0016C7E1
			public override int GetChildCount()
			{
				return this.CheckedListBox.Items.Count;
			}

			// Token: 0x0600632C RID: 25388 RVA: 0x0016E5F4 File Offset: 0x0016C7F4
			public override AccessibleObject GetFocused()
			{
				int focusedIndex = this.CheckedListBox.FocusedIndex;
				if (focusedIndex >= 0)
				{
					return this.GetChild(focusedIndex);
				}
				return null;
			}

			// Token: 0x0600632D RID: 25389 RVA: 0x0016E61C File Offset: 0x0016C81C
			public override AccessibleObject GetSelected()
			{
				int selectedIndex = this.CheckedListBox.SelectedIndex;
				if (selectedIndex >= 0)
				{
					return this.GetChild(selectedIndex);
				}
				return null;
			}

			// Token: 0x0600632E RID: 25390 RVA: 0x0016E644 File Offset: 0x0016C844
			public override AccessibleObject HitTest(int x, int y)
			{
				int childCount = this.GetChildCount();
				for (int i = 0; i < childCount; i++)
				{
					AccessibleObject child = this.GetChild(i);
					if (child.Bounds.Contains(x, y))
					{
						return child;
					}
				}
				if (this.Bounds.Contains(x, y))
				{
					return this;
				}
				return null;
			}

			// Token: 0x0600632F RID: 25391 RVA: 0x0016E695 File Offset: 0x0016C895
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override AccessibleObject Navigate(AccessibleNavigation direction)
			{
				if (this.GetChildCount() > 0)
				{
					if (direction == AccessibleNavigation.FirstChild)
					{
						return this.GetChild(0);
					}
					if (direction == AccessibleNavigation.LastChild)
					{
						return this.GetChild(this.GetChildCount() - 1);
					}
				}
				return base.Navigate(direction);
			}
		}

		// Token: 0x02000620 RID: 1568
		[ComVisible(true)]
		internal class CheckedListBoxItemAccessibleObject : AccessibleObject
		{
			// Token: 0x06006330 RID: 25392 RVA: 0x0016E6C6 File Offset: 0x0016C8C6
			public CheckedListBoxItemAccessibleObject(string name, int index, CheckedListBox.CheckedListBoxAccessibleObject parent)
			{
				this.name = name;
				this.parent = parent;
				this.index = index;
			}

			// Token: 0x1700152A RID: 5418
			// (get) Token: 0x06006331 RID: 25393 RVA: 0x0016E6E4 File Offset: 0x0016C8E4
			public override Rectangle Bounds
			{
				get
				{
					Rectangle itemRectangle = this.ParentCheckedListBox.GetItemRectangle(this.index);
					NativeMethods.POINT point = new NativeMethods.POINT(itemRectangle.X, itemRectangle.Y);
					UnsafeNativeMethods.ClientToScreen(new HandleRef(this.ParentCheckedListBox, this.ParentCheckedListBox.Handle), point);
					return new Rectangle(point.x, point.y, itemRectangle.Width, itemRectangle.Height);
				}
			}

			// Token: 0x1700152B RID: 5419
			// (get) Token: 0x06006332 RID: 25394 RVA: 0x0016E753 File Offset: 0x0016C953
			public override string DefaultAction
			{
				get
				{
					if (this.ParentCheckedListBox.GetItemChecked(this.index))
					{
						return SR.GetString("AccessibleActionUncheck");
					}
					return SR.GetString("AccessibleActionCheck");
				}
			}

			// Token: 0x1700152C RID: 5420
			// (get) Token: 0x06006333 RID: 25395 RVA: 0x0016E77D File Offset: 0x0016C97D
			private CheckedListBox ParentCheckedListBox
			{
				get
				{
					return (CheckedListBox)this.parent.Owner;
				}
			}

			// Token: 0x1700152D RID: 5421
			// (get) Token: 0x06006334 RID: 25396 RVA: 0x0016E78F File Offset: 0x0016C98F
			// (set) Token: 0x06006335 RID: 25397 RVA: 0x0016E797 File Offset: 0x0016C997
			public override string Name
			{
				get
				{
					return this.name;
				}
				set
				{
					this.name = value;
				}
			}

			// Token: 0x1700152E RID: 5422
			// (get) Token: 0x06006336 RID: 25398 RVA: 0x0016E7A0 File Offset: 0x0016C9A0
			public override AccessibleObject Parent
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return this.parent;
				}
			}

			// Token: 0x1700152F RID: 5423
			// (get) Token: 0x06006337 RID: 25399 RVA: 0x0016E7A8 File Offset: 0x0016C9A8
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.CheckButton;
				}
			}

			// Token: 0x17001530 RID: 5424
			// (get) Token: 0x06006338 RID: 25400 RVA: 0x0016E7AC File Offset: 0x0016C9AC
			public override AccessibleStates State
			{
				get
				{
					AccessibleStates accessibleStates = AccessibleStates.Focusable | AccessibleStates.Selectable;
					switch (this.ParentCheckedListBox.GetItemCheckState(this.index))
					{
					case CheckState.Checked:
						accessibleStates |= AccessibleStates.Checked;
						break;
					case CheckState.Indeterminate:
						accessibleStates |= AccessibleStates.Mixed;
						break;
					}
					if (this.ParentCheckedListBox.SelectedIndex == this.index)
					{
						accessibleStates |= AccessibleStates.Selected | AccessibleStates.Focused;
					}
					if (AccessibilityImprovements.Level3 && this.ParentCheckedListBox.Focused && this.ParentCheckedListBox.SelectedIndex == -1)
					{
						accessibleStates |= AccessibleStates.Focused;
					}
					return accessibleStates;
				}
			}

			// Token: 0x17001531 RID: 5425
			// (get) Token: 0x06006339 RID: 25401 RVA: 0x0016E830 File Offset: 0x0016CA30
			public override string Value
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return this.ParentCheckedListBox.GetItemChecked(this.index).ToString();
				}
			}

			// Token: 0x0600633A RID: 25402 RVA: 0x0016E856 File Offset: 0x0016CA56
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void DoDefaultAction()
			{
				this.ParentCheckedListBox.SetItemChecked(this.index, !this.ParentCheckedListBox.GetItemChecked(this.index));
			}

			// Token: 0x0600633B RID: 25403 RVA: 0x0016E880 File Offset: 0x0016CA80
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override AccessibleObject Navigate(AccessibleNavigation direction)
			{
				if ((direction == AccessibleNavigation.Down || direction == AccessibleNavigation.Next) && this.index < this.parent.GetChildCount() - 1)
				{
					return this.parent.GetChild(this.index + 1);
				}
				if ((direction == AccessibleNavigation.Up || direction == AccessibleNavigation.Previous) && this.index > 0)
				{
					return this.parent.GetChild(this.index - 1);
				}
				return base.Navigate(direction);
			}

			// Token: 0x0600633C RID: 25404 RVA: 0x0016E8EC File Offset: 0x0016CAEC
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void Select(AccessibleSelection flags)
			{
				try
				{
					this.ParentCheckedListBox.AccessibilityObject.GetSystemIAccessibleInternal().accSelect((int)flags, this.index + 1);
				}
				catch (ArgumentException)
				{
				}
			}

			// Token: 0x0400391B RID: 14619
			private string name;

			// Token: 0x0400391C RID: 14620
			private int index;

			// Token: 0x0400391D RID: 14621
			private CheckedListBox.CheckedListBoxAccessibleObject parent;
		}
	}
}
