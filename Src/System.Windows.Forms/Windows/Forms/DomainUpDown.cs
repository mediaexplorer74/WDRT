using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows spin box (also known as an up-down control) that displays string values.</summary>
	// Token: 0x02000231 RID: 561
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultProperty("Items")]
	[DefaultEvent("SelectedItemChanged")]
	[DefaultBindingProperty("SelectedItem")]
	[SRDescription("DescriptionDomainUpDown")]
	public class DomainUpDown : UpDownBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DomainUpDown" /> class.</summary>
		// Token: 0x06002473 RID: 9331 RVA: 0x000AC1DD File Offset: 0x000AA3DD
		public DomainUpDown()
		{
			base.SetState2(2048, true);
			this.Text = string.Empty;
		}

		/// <summary>A collection of objects assigned to the spin box (also known as an up-down control).</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DomainUpDown.DomainUpDownItemCollection" /> that contains an <see cref="T:System.Object" /> collection.</returns>
		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06002474 RID: 9332 RVA: 0x000AC219 File Offset: 0x000AA419
		[SRCategory("CatData")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[SRDescription("DomainUpDownItemsDescr")]
		[Localizable(true)]
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public DomainUpDown.DomainUpDownItemCollection Items
		{
			get
			{
				if (this.domainItems == null)
				{
					this.domainItems = new DomainUpDown.DomainUpDownItemCollection(this);
				}
				return this.domainItems;
			}
		}

		/// <summary>Gets or sets the spacing between the <see cref="T:System.Windows.Forms.DomainUpDown" /> control's contents and its edges.</summary>
		/// <returns>
		///   <see cref="F:System.Windows.Forms.Padding.Empty" /> in all cases.</returns>
		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06002475 RID: 9333 RVA: 0x00013442 File Offset: 0x00011642
		// (set) Token: 0x06002476 RID: 9334 RVA: 0x0001344A File Offset: 0x0001164A
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DomainUpDown.Padding" /> property changes.</summary>
		// Token: 0x14000197 RID: 407
		// (add) Token: 0x06002477 RID: 9335 RVA: 0x00013453 File Offset: 0x00011653
		// (remove) Token: 0x06002478 RID: 9336 RVA: 0x0001345C File Offset: 0x0001165C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		/// <summary>Gets or sets the index value of the selected item.</summary>
		/// <returns>The zero-based index value of the selected item. The default value is -1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is less than the default, -1.  
		///  -or-  
		///  The assigned value is greater than the <see cref="P:System.Windows.Forms.DomainUpDown.Items" /> count.</exception>
		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06002479 RID: 9337 RVA: 0x000AC235 File Offset: 0x000AA435
		// (set) Token: 0x0600247A RID: 9338 RVA: 0x000AC248 File Offset: 0x000AA448
		[Browsable(false)]
		[DefaultValue(-1)]
		[SRCategory("CatAppearance")]
		[SRDescription("DomainUpDownSelectedIndexDescr")]
		public int SelectedIndex
		{
			get
			{
				if (base.UserEdit)
				{
					return -1;
				}
				return this.domainIndex;
			}
			set
			{
				if (value < -1 || value >= this.Items.Count)
				{
					throw new ArgumentOutOfRangeException("SelectedIndex", SR.GetString("InvalidArgument", new object[]
					{
						"SelectedIndex",
						value.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (value != this.SelectedIndex)
				{
					this.SelectIndex(value);
				}
			}
		}

		/// <summary>Gets or sets the selected item based on the index value of the selected item in the collection.</summary>
		/// <returns>The selected item based on the <see cref="P:System.Windows.Forms.DomainUpDown.SelectedIndex" /> value. The default value is <see langword="null" />.</returns>
		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x0600247B RID: 9339 RVA: 0x000AC2AC File Offset: 0x000AA4AC
		// (set) Token: 0x0600247C RID: 9340 RVA: 0x000AC2D4 File Offset: 0x000AA4D4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("DomainUpDownSelectedItemDescr")]
		public object SelectedItem
		{
			get
			{
				int selectedIndex = this.SelectedIndex;
				if (selectedIndex != -1)
				{
					return this.Items[selectedIndex];
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					this.SelectedIndex = -1;
					return;
				}
				for (int i = 0; i < this.Items.Count; i++)
				{
					if (value != null && value.Equals(this.Items[i]))
					{
						this.SelectedIndex = i;
						return;
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the item collection is sorted.</summary>
		/// <returns>
		///   <see langword="true" /> if the item collection is sorted; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x0600247D RID: 9341 RVA: 0x000AC321 File Offset: 0x000AA521
		// (set) Token: 0x0600247E RID: 9342 RVA: 0x000AC329 File Offset: 0x000AA529
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("DomainUpDownSortedDescr")]
		public bool Sorted
		{
			get
			{
				return this.sorted;
			}
			set
			{
				this.sorted = value;
				if (this.sorted)
				{
					this.SortDomainItems();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the collection of items continues to the first or last item if the user continues past the end of the list.</summary>
		/// <returns>
		///   <see langword="true" /> if the list starts again when the user reaches the beginning or end of the collection; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x0600247F RID: 9343 RVA: 0x000AC340 File Offset: 0x000AA540
		// (set) Token: 0x06002480 RID: 9344 RVA: 0x000AC348 File Offset: 0x000AA548
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[DefaultValue(false)]
		[SRDescription("DomainUpDownWrapDescr")]
		public bool Wrap
		{
			get
			{
				return this.wrap;
			}
			set
			{
				this.wrap = value;
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DomainUpDown.SelectedItem" /> property has been changed.</summary>
		// Token: 0x14000198 RID: 408
		// (add) Token: 0x06002481 RID: 9345 RVA: 0x000AC351 File Offset: 0x000AA551
		// (remove) Token: 0x06002482 RID: 9346 RVA: 0x000AC36A File Offset: 0x000AA56A
		[SRCategory("CatBehavior")]
		[SRDescription("DomainUpDownOnSelectedItemChangedDescr")]
		public event EventHandler SelectedItemChanged
		{
			add
			{
				this.onSelectedItemChanged = (EventHandler)Delegate.Combine(this.onSelectedItemChanged, value);
			}
			remove
			{
				this.onSelectedItemChanged = (EventHandler)Delegate.Remove(this.onSelectedItemChanged, value);
			}
		}

		/// <summary>Creates a new accessibility object for the <see cref="T:System.Windows.Forms.DomainUpDown" /> control.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.DomainUpDown.DomainUpDownAccessibleObject" /> for the control.</returns>
		// Token: 0x06002483 RID: 9347 RVA: 0x000AC383 File Offset: 0x000AA583
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new DomainUpDown.DomainUpDownAccessibleObject(this);
		}

		/// <summary>Displays the next item in the object collection.</summary>
		// Token: 0x06002484 RID: 9348 RVA: 0x000AC38C File Offset: 0x000AA58C
		public override void DownButton()
		{
			if (this.domainItems == null)
			{
				return;
			}
			if (this.domainItems.Count <= 0)
			{
				return;
			}
			int num = -1;
			if (base.UserEdit)
			{
				num = this.MatchIndex(this.Text, false, this.domainIndex);
			}
			if (num != -1)
			{
				if (LocalAppContextSwitches.UseLegacyDomainUpDownControlScrolling)
				{
					this.SelectIndex(num);
					return;
				}
				this.domainIndex = num;
			}
			if (this.domainIndex < this.domainItems.Count - 1)
			{
				this.SelectIndex(this.domainIndex + 1);
				return;
			}
			if (this.Wrap)
			{
				this.SelectIndex(0);
			}
		}

		// Token: 0x06002485 RID: 9349 RVA: 0x000AC41E File Offset: 0x000AA61E
		internal int MatchIndex(string text, bool complete)
		{
			return this.MatchIndex(text, complete, this.domainIndex);
		}

		// Token: 0x06002486 RID: 9350 RVA: 0x000AC430 File Offset: 0x000AA630
		internal int MatchIndex(string text, bool complete, int startPosition)
		{
			if (this.domainItems == null)
			{
				return -1;
			}
			if (text.Length < 1)
			{
				return -1;
			}
			if (this.domainItems.Count <= 0)
			{
				return -1;
			}
			if (startPosition < 0)
			{
				startPosition = this.domainItems.Count - 1;
			}
			if (startPosition >= this.domainItems.Count)
			{
				startPosition = 0;
			}
			int num = startPosition;
			int num2 = -1;
			if (!complete)
			{
				text = text.ToUpper(CultureInfo.InvariantCulture);
			}
			bool flag;
			do
			{
				if (complete)
				{
					flag = this.Items[num].ToString().Equals(text);
				}
				else
				{
					flag = this.Items[num].ToString().ToUpper(CultureInfo.InvariantCulture).StartsWith(text);
				}
				if (flag)
				{
					num2 = num;
				}
				num++;
				if (num >= this.domainItems.Count)
				{
					num = 0;
				}
			}
			while (!flag && num != startPosition);
			return num2;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DomainUpDown.SelectedItemChanged" /> event.</summary>
		/// <param name="source">The source of the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002487 RID: 9351 RVA: 0x000AC4FC File Offset: 0x000AA6FC
		protected override void OnChanged(object source, EventArgs e)
		{
			this.OnSelectedItemChanged(source, e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event.</summary>
		/// <param name="source">The source of the event.</param>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains the event data.</param>
		// Token: 0x06002488 RID: 9352 RVA: 0x000AC508 File Offset: 0x000AA708
		protected override void OnTextBoxKeyPress(object source, KeyPressEventArgs e)
		{
			if (base.ReadOnly)
			{
				char[] array = new char[] { e.KeyChar };
				UnicodeCategory unicodeCategory = char.GetUnicodeCategory(array[0]);
				if (unicodeCategory == UnicodeCategory.LetterNumber || unicodeCategory == UnicodeCategory.LowercaseLetter || unicodeCategory == UnicodeCategory.DecimalDigitNumber || unicodeCategory == UnicodeCategory.MathSymbol || unicodeCategory == UnicodeCategory.OtherLetter || unicodeCategory == UnicodeCategory.OtherNumber || unicodeCategory == UnicodeCategory.UppercaseLetter)
				{
					int num = this.MatchIndex(new string(array), false, this.domainIndex + 1);
					if (num != -1)
					{
						this.SelectIndex(num);
					}
					e.Handled = true;
				}
			}
			base.OnTextBoxKeyPress(source, e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DomainUpDown.SelectedItemChanged" /> event.</summary>
		/// <param name="source">The source of the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002489 RID: 9353 RVA: 0x000AC584 File Offset: 0x000AA784
		protected void OnSelectedItemChanged(object source, EventArgs e)
		{
			if (this.onSelectedItemChanged != null)
			{
				this.onSelectedItemChanged(this, e);
			}
		}

		// Token: 0x0600248A RID: 9354 RVA: 0x000AC59C File Offset: 0x000AA79C
		private void SelectIndex(int index)
		{
			if (this.domainItems == null || index < -1 || index >= this.domainItems.Count)
			{
				index = -1;
				return;
			}
			this.domainIndex = index;
			if (this.domainIndex >= 0)
			{
				this.stringValue = this.domainItems[this.domainIndex].ToString();
				base.UserEdit = false;
				this.UpdateEditText();
				return;
			}
			base.UserEdit = true;
		}

		// Token: 0x0600248B RID: 9355 RVA: 0x000AC608 File Offset: 0x000AA808
		private void SortDomainItems()
		{
			if (this.inSort)
			{
				return;
			}
			this.inSort = true;
			try
			{
				if (this.sorted)
				{
					if (this.domainItems != null)
					{
						ArrayList.Adapter(this.domainItems).Sort(new DomainUpDown.DomainUpDownItemCompare());
						if (!base.UserEdit)
						{
							int num = this.MatchIndex(this.stringValue, true);
							if (num != -1)
							{
								this.SelectIndex(num);
							}
						}
					}
				}
			}
			finally
			{
				this.inSort = false;
			}
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.DomainUpDown" /> control.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.DomainUpDown" />.</returns>
		// Token: 0x0600248C RID: 9356 RVA: 0x000AC688 File Offset: 0x000AA888
		public override string ToString()
		{
			string text = base.ToString();
			if (this.Items != null)
			{
				text = text + ", Items.Count: " + this.Items.Count.ToString(CultureInfo.CurrentCulture);
				text = text + ", SelectedIndex: " + this.SelectedIndex.ToString(CultureInfo.CurrentCulture);
			}
			return text;
		}

		/// <summary>Displays the previous item in the collection.</summary>
		// Token: 0x0600248D RID: 9357 RVA: 0x000AC6E8 File Offset: 0x000AA8E8
		public override void UpButton()
		{
			if (this.domainItems == null)
			{
				return;
			}
			if (this.domainItems.Count <= 0)
			{
				return;
			}
			if (this.domainIndex == -1 && LocalAppContextSwitches.UseLegacyDomainUpDownControlScrolling)
			{
				return;
			}
			int num = -1;
			if (base.UserEdit)
			{
				num = this.MatchIndex(this.Text, false, this.domainIndex);
			}
			if (num != -1)
			{
				if (LocalAppContextSwitches.UseLegacyDomainUpDownControlScrolling)
				{
					this.SelectIndex(num);
					return;
				}
				this.domainIndex = num;
			}
			if (this.domainIndex > 0)
			{
				this.SelectIndex(this.domainIndex - 1);
				return;
			}
			if (this.Wrap)
			{
				this.SelectIndex(this.domainItems.Count - 1);
			}
		}

		/// <summary>Updates the text in the spin box (also known as an up-down control) to display the selected item.</summary>
		// Token: 0x0600248E RID: 9358 RVA: 0x000AC78B File Offset: 0x000AA98B
		protected override void UpdateEditText()
		{
			base.UserEdit = false;
			base.ChangingText = true;
			this.Text = this.stringValue;
		}

		// Token: 0x0600248F RID: 9359 RVA: 0x000AC7A8 File Offset: 0x000AA9A8
		internal override Size GetPreferredSizeCore(Size proposedConstraints)
		{
			int preferredHeight = base.PreferredHeight;
			int num = LayoutUtils.OldGetLargestStringSizeInCollection(this.Font, this.Items).Width;
			num = base.SizeFromClientSize(num, preferredHeight).Width + this.upDownButtons.Width;
			return new Size(num, preferredHeight) + this.Padding.Size;
		}

		// Token: 0x04000EFD RID: 3837
		private static readonly string DefaultValue = "";

		// Token: 0x04000EFE RID: 3838
		private static readonly bool DefaultWrap = false;

		// Token: 0x04000EFF RID: 3839
		private DomainUpDown.DomainUpDownItemCollection domainItems;

		// Token: 0x04000F00 RID: 3840
		private string stringValue = DomainUpDown.DefaultValue;

		// Token: 0x04000F01 RID: 3841
		private int domainIndex = -1;

		// Token: 0x04000F02 RID: 3842
		private bool sorted;

		// Token: 0x04000F03 RID: 3843
		private bool wrap = DomainUpDown.DefaultWrap;

		// Token: 0x04000F04 RID: 3844
		private EventHandler onSelectedItemChanged;

		// Token: 0x04000F05 RID: 3845
		private bool inSort;

		/// <summary>Encapsulates a collection of objects for use by the <see cref="T:System.Windows.Forms.DomainUpDown" /> class.</summary>
		// Token: 0x02000682 RID: 1666
		public class DomainUpDownItemCollection : ArrayList
		{
			// Token: 0x060066FB RID: 26363 RVA: 0x0018126D File Offset: 0x0017F46D
			internal DomainUpDownItemCollection(DomainUpDown owner)
			{
				this.owner = owner;
			}

			/// <summary>Gets or sets the item at the specified indexed location in the collection.</summary>
			/// <param name="index">The indexed location of the item in the collection.</param>
			/// <returns>An <see cref="T:System.Object" /> that represents the item at the specified indexed location.</returns>
			// Token: 0x17001671 RID: 5745
			[Browsable(false)]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
			public override object this[int index]
			{
				get
				{
					return base[index];
				}
				set
				{
					base[index] = value;
					if (this.owner.SelectedIndex == index)
					{
						this.owner.SelectIndex(index);
					}
					if (this.owner.Sorted)
					{
						this.owner.SortDomainItems();
					}
				}
			}

			/// <summary>Adds the specified object to the end of the collection.</summary>
			/// <param name="item">The <see cref="T:System.Object" /> to be added to the end of the collection.</param>
			/// <returns>The zero-based index value of the <see cref="T:System.Object" /> added to the collection.</returns>
			// Token: 0x060066FE RID: 26366 RVA: 0x001812C4 File Offset: 0x0017F4C4
			public override int Add(object item)
			{
				int num = base.Add(item);
				if (this.owner.Sorted)
				{
					this.owner.SortDomainItems();
				}
				return num;
			}

			/// <summary>Removes the specified item from the collection.</summary>
			/// <param name="item">The <see cref="T:System.Object" /> to remove from the collection.</param>
			// Token: 0x060066FF RID: 26367 RVA: 0x001812F4 File Offset: 0x0017F4F4
			public override void Remove(object item)
			{
				int num = this.IndexOf(item);
				if (num == -1)
				{
					throw new ArgumentOutOfRangeException("item", SR.GetString("InvalidArgument", new object[]
					{
						"item",
						item.ToString()
					}));
				}
				this.RemoveAt(num);
			}

			/// <summary>Removes the item from the specified location in the collection.</summary>
			/// <param name="item">The indexed location of the <see cref="T:System.Object" /> in the collection.</param>
			// Token: 0x06006700 RID: 26368 RVA: 0x00181340 File Offset: 0x0017F540
			public override void RemoveAt(int item)
			{
				base.RemoveAt(item);
				if (item < this.owner.domainIndex)
				{
					this.owner.SelectIndex(this.owner.domainIndex - 1);
					return;
				}
				if (item == this.owner.domainIndex)
				{
					this.owner.SelectIndex(-1);
				}
			}

			/// <summary>Inserts the specified object into the collection at the specified location.</summary>
			/// <param name="index">The indexed location within the collection to insert the <see cref="T:System.Object" />.</param>
			/// <param name="item">The <see cref="T:System.Object" /> to insert.</param>
			// Token: 0x06006701 RID: 26369 RVA: 0x00181395 File Offset: 0x0017F595
			public override void Insert(int index, object item)
			{
				base.Insert(index, item);
				if (this.owner.Sorted)
				{
					this.owner.SortDomainItems();
				}
			}

			// Token: 0x04003A7F RID: 14975
			private DomainUpDown owner;
		}

		// Token: 0x02000683 RID: 1667
		private sealed class DomainUpDownItemCompare : IComparer
		{
			// Token: 0x06006702 RID: 26370 RVA: 0x001813B7 File Offset: 0x0017F5B7
			public int Compare(object p, object q)
			{
				if (p == q)
				{
					return 0;
				}
				if (p == null || q == null)
				{
					return 0;
				}
				return string.Compare(p.ToString(), q.ToString(), false, CultureInfo.CurrentCulture);
			}
		}

		/// <summary>Provides information about the <see cref="T:System.Windows.Forms.DomainUpDown" /> control to accessibility client applications.</summary>
		// Token: 0x02000684 RID: 1668
		[ComVisible(true)]
		public class DomainUpDownAccessibleObject : Control.ControlAccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DomainUpDown.DomainUpDownAccessibleObject" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.Control" /> that owns the <see cref="T:System.Windows.Forms.Control.ControlAccessibleObject" />.</param>
			// Token: 0x06006704 RID: 26372 RVA: 0x0009B733 File Offset: 0x00099933
			public DomainUpDownAccessibleObject(Control owner)
				: base(owner)
			{
			}

			/// <summary>Gets or sets the name of the control that the accessible object describes.</summary>
			/// <returns>The name of the control that the accessible object describes.</returns>
			// Token: 0x17001672 RID: 5746
			// (get) Token: 0x06006705 RID: 26373 RVA: 0x001813E0 File Offset: 0x0017F5E0
			// (set) Token: 0x06006706 RID: 26374 RVA: 0x00010E62 File Offset: 0x0000F062
			public override string Name
			{
				get
				{
					string name = base.Name;
					return ((DomainUpDown)base.Owner).GetAccessibleName(name);
				}
				set
				{
					base.Name = value;
				}
			}

			// Token: 0x17001673 RID: 5747
			// (get) Token: 0x06006707 RID: 26375 RVA: 0x00181405 File Offset: 0x0017F605
			private DomainUpDown.DomainItemListAccessibleObject ItemList
			{
				get
				{
					if (this.itemList == null)
					{
						this.itemList = new DomainUpDown.DomainItemListAccessibleObject(this);
					}
					return this.itemList;
				}
			}

			/// <summary>Gets the role of this accessible object.</summary>
			/// <returns>The <see cref="F:System.Windows.Forms.AccessibleRole.ComboBox" /> value.</returns>
			// Token: 0x17001674 RID: 5748
			// (get) Token: 0x06006708 RID: 26376 RVA: 0x00181424 File Offset: 0x0017F624
			public override AccessibleRole Role
			{
				get
				{
					AccessibleRole accessibleRole = base.Owner.AccessibleRole;
					if (accessibleRole != AccessibleRole.Default)
					{
						return accessibleRole;
					}
					if (AccessibilityImprovements.Level1)
					{
						return AccessibleRole.SpinButton;
					}
					return AccessibleRole.ComboBox;
				}
			}

			/// <summary>Gets the accessible child corresponding to the specified index.</summary>
			/// <param name="index">The zero-based index of the accessible child.</param>
			/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the accessible child corresponding to the specified index.</returns>
			// Token: 0x06006709 RID: 26377 RVA: 0x00181450 File Offset: 0x0017F650
			public override AccessibleObject GetChild(int index)
			{
				switch (index)
				{
				case 0:
					return ((UpDownBase)base.Owner).TextBox.AccessibilityObject.Parent;
				case 1:
					return ((UpDownBase)base.Owner).UpDownButtonsInternal.AccessibilityObject.Parent;
				case 2:
					return this.ItemList;
				default:
					return null;
				}
			}

			/// <summary>Retrieves the number of children belonging to an accessible object.</summary>
			/// <returns>Returns 3 in all cases.</returns>
			// Token: 0x0600670A RID: 26378 RVA: 0x00023BD7 File Offset: 0x00021DD7
			public override int GetChildCount()
			{
				return 3;
			}

			// Token: 0x04003A80 RID: 14976
			private DomainUpDown.DomainItemListAccessibleObject itemList;
		}

		// Token: 0x02000685 RID: 1669
		internal class DomainItemListAccessibleObject : AccessibleObject
		{
			// Token: 0x0600670B RID: 26379 RVA: 0x001814AF File Offset: 0x0017F6AF
			public DomainItemListAccessibleObject(DomainUpDown.DomainUpDownAccessibleObject parent)
			{
				this.parent = parent;
			}

			// Token: 0x17001675 RID: 5749
			// (get) Token: 0x0600670C RID: 26380 RVA: 0x001814C0 File Offset: 0x0017F6C0
			// (set) Token: 0x0600670D RID: 26381 RVA: 0x0016FA70 File Offset: 0x0016DC70
			public override string Name
			{
				get
				{
					string name = base.Name;
					if (name == null || name.Length == 0)
					{
						return "Items";
					}
					return name;
				}
				set
				{
					base.Name = value;
				}
			}

			// Token: 0x17001676 RID: 5750
			// (get) Token: 0x0600670E RID: 26382 RVA: 0x001814E6 File Offset: 0x0017F6E6
			public override AccessibleObject Parent
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return this.parent;
				}
			}

			// Token: 0x17001677 RID: 5751
			// (get) Token: 0x0600670F RID: 26383 RVA: 0x0017753D File Offset: 0x0017573D
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.List;
				}
			}

			// Token: 0x17001678 RID: 5752
			// (get) Token: 0x06006710 RID: 26384 RVA: 0x001814EE File Offset: 0x0017F6EE
			public override AccessibleStates State
			{
				get
				{
					return AccessibleStates.Invisible | AccessibleStates.Offscreen;
				}
			}

			// Token: 0x06006711 RID: 26385 RVA: 0x001814F5 File Offset: 0x0017F6F5
			public override AccessibleObject GetChild(int index)
			{
				if (index >= 0 && index < this.GetChildCount())
				{
					return new DomainUpDown.DomainItemAccessibleObject(((DomainUpDown)this.parent.Owner).Items[index].ToString(), this);
				}
				return null;
			}

			// Token: 0x06006712 RID: 26386 RVA: 0x0018152C File Offset: 0x0017F72C
			public override int GetChildCount()
			{
				return ((DomainUpDown)this.parent.Owner).Items.Count;
			}

			// Token: 0x04003A81 RID: 14977
			private DomainUpDown.DomainUpDownAccessibleObject parent;
		}

		/// <summary>Provides information about the items in the <see cref="T:System.Windows.Forms.DomainUpDown" /> control to accessibility client applications.</summary>
		// Token: 0x02000686 RID: 1670
		[ComVisible(true)]
		public class DomainItemAccessibleObject : AccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DomainUpDown.DomainItemAccessibleObject" /> class.</summary>
			/// <param name="name">The name of the <see cref="T:System.Windows.Forms.DomainUpDown.DomainItemAccessibleObject" />.</param>
			/// <param name="parent">The <see cref="T:System.Windows.Forms.AccessibleObject" /> that contains the items in the <see cref="T:System.Windows.Forms.DomainUpDown" /> control.</param>
			// Token: 0x06006713 RID: 26387 RVA: 0x00181548 File Offset: 0x0017F748
			public DomainItemAccessibleObject(string name, AccessibleObject parent)
			{
				this.name = name;
				this.parent = (DomainUpDown.DomainItemListAccessibleObject)parent;
			}

			/// <summary>Gets or sets the object name.</summary>
			/// <returns>The object name, or <see langword="null" /> if the property has not been set.</returns>
			// Token: 0x17001679 RID: 5753
			// (get) Token: 0x06006714 RID: 26388 RVA: 0x00181563 File Offset: 0x0017F763
			// (set) Token: 0x06006715 RID: 26389 RVA: 0x0018156B File Offset: 0x0017F76B
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

			/// <summary>Gets the parent of an accessible object.</summary>
			/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the parent of an accessible object, or <see langword="null" /> if there is no parent object.</returns>
			// Token: 0x1700167A RID: 5754
			// (get) Token: 0x06006716 RID: 26390 RVA: 0x00181574 File Offset: 0x0017F774
			public override AccessibleObject Parent
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return this.parent;
				}
			}

			/// <summary>Gets the role of this accessible object.</summary>
			/// <returns>The <see cref="F:System.Windows.Forms.AccessibleRole.ListItem" /> value.</returns>
			// Token: 0x1700167B RID: 5755
			// (get) Token: 0x06006717 RID: 26391 RVA: 0x00015EF1 File Offset: 0x000140F1
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.ListItem;
				}
			}

			/// <summary>Gets the state of the <see cref="T:System.Windows.Forms.RadioButton" /> control.</summary>
			/// <returns>If the <see cref="P:System.Windows.Forms.RadioButton.Checked" /> property is set to true, returns <see cref="F:System.Windows.Forms.AccessibleStates.Checked" />.</returns>
			// Token: 0x1700167C RID: 5756
			// (get) Token: 0x06006718 RID: 26392 RVA: 0x0018157C File Offset: 0x0017F77C
			public override AccessibleStates State
			{
				get
				{
					return AccessibleStates.Selectable;
				}
			}

			/// <summary>Gets the value of an accessible object.</summary>
			/// <returns>The Name property of the <see cref="T:System.Windows.Forms.DomainUpDown.DomainItemAccessibleObject" />.</returns>
			// Token: 0x1700167D RID: 5757
			// (get) Token: 0x06006719 RID: 26393 RVA: 0x00181563 File Offset: 0x0017F763
			public override string Value
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return this.name;
				}
			}

			// Token: 0x04003A82 RID: 14978
			private string name;

			// Token: 0x04003A83 RID: 14979
			private DomainUpDown.DomainItemListAccessibleObject parent;
		}
	}
}
