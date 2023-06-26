using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms.Layout;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows list view control, which displays a collection of items that can be displayed using one of four different views.</summary>
	// Token: 0x020002D2 RID: 722
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Docking(DockingBehavior.Ask)]
	[Designer("System.Windows.Forms.Design.ListViewDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultProperty("Items")]
	[DefaultEvent("SelectedIndexChanged")]
	[SRDescription("DescriptionListView")]
	public class ListView : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListView" /> class.</summary>
		// Token: 0x06002CB9 RID: 11449 RVA: 0x000C8F3C File Offset: 0x000C713C
		public ListView()
		{
			int num = 8392196;
			if (!AccessibilityImprovements.Level3)
			{
				num |= 64;
			}
			this.listViewState = new BitVector32(num);
			this.listViewState1 = new BitVector32(8);
			base.SetStyle(ControlStyles.UserPaint, false);
			base.SetStyle(ControlStyles.StandardClick, false);
			base.SetStyle(ControlStyles.UseTextForAccessibility, false);
			this.odCacheFont = this.Font;
			this.odCacheFontHandle = base.FontHandle;
			base.SetBounds(0, 0, 121, 97);
			this.listItemCollection = new ListView.ListViewItemCollection(new ListView.ListViewNativeItemCollection(this));
			this.columnHeaderCollection = new ListView.ColumnHeaderCollection(this);
		}

		/// <summary>Gets or sets the type of action the user must take to activate an item.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ItemActivation" /> values. The default is <see cref="F:System.Windows.Forms.ItemActivation.Standard" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is not one of the <see cref="T:System.Windows.Forms.ItemActivation" /> members.</exception>
		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06002CBA RID: 11450 RVA: 0x000C9055 File Offset: 0x000C7255
		// (set) Token: 0x06002CBB RID: 11451 RVA: 0x000C9060 File Offset: 0x000C7260
		[SRCategory("CatBehavior")]
		[DefaultValue(ItemActivation.Standard)]
		[SRDescription("ListViewActivationDescr")]
		public ItemActivation Activation
		{
			get
			{
				return this.activation;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ItemActivation));
				}
				if (this.HotTracking && value != ItemActivation.OneClick)
				{
					throw new ArgumentException(SR.GetString("ListViewActivationMustBeOnWhenHotTrackingIsOn"), "value");
				}
				if (this.activation != value)
				{
					this.activation = value;
					this.UpdateExtendedStyles();
				}
			}
		}

		/// <summary>Gets or sets the alignment of items in the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ListViewAlignment" /> values. The default is <see cref="F:System.Windows.Forms.ListViewAlignment.Top" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is not one of the <see cref="T:System.Windows.Forms.ListViewAlignment" /> values.</exception>
		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06002CBC RID: 11452 RVA: 0x000C90CA File Offset: 0x000C72CA
		// (set) Token: 0x06002CBD RID: 11453 RVA: 0x000C90D4 File Offset: 0x000C72D4
		[SRCategory("CatBehavior")]
		[DefaultValue(ListViewAlignment.Top)]
		[Localizable(true)]
		[SRDescription("ListViewAlignmentDescr")]
		public ListViewAlignment Alignment
		{
			get
			{
				return this.alignStyle;
			}
			set
			{
				if (!ClientUtils.IsEnumValid_NotSequential(value, (int)value, new int[] { 0, 2, 1, 5 }))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ListViewAlignment));
				}
				if (this.alignStyle != value)
				{
					this.alignStyle = value;
					this.RecreateHandleInternal();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the user can drag column headers to reorder columns in the control.</summary>
		/// <returns>
		///   <see langword="true" /> if drag-and-drop column reordering is allowed; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06002CBE RID: 11454 RVA: 0x000C912C File Offset: 0x000C732C
		// (set) Token: 0x06002CBF RID: 11455 RVA: 0x000C913A File Offset: 0x000C733A
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("ListViewAllowColumnReorderDescr")]
		public bool AllowColumnReorder
		{
			get
			{
				return this.listViewState[2];
			}
			set
			{
				if (this.AllowColumnReorder != value)
				{
					this.listViewState[2] = value;
					this.UpdateExtendedStyles();
				}
			}
		}

		/// <summary>Gets or sets whether icons are automatically kept arranged.</summary>
		/// <returns>
		///   <see langword="true" /> if icons are automatically kept arranged and snapped to the grid; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06002CC0 RID: 11456 RVA: 0x000C9158 File Offset: 0x000C7358
		// (set) Token: 0x06002CC1 RID: 11457 RVA: 0x000C9166 File Offset: 0x000C7366
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("ListViewAutoArrangeDescr")]
		public bool AutoArrange
		{
			get
			{
				return this.listViewState[4];
			}
			set
			{
				if (this.AutoArrange != value)
				{
					this.listViewState[4] = value;
					base.UpdateStyles();
				}
			}
		}

		/// <summary>Gets or sets the background color.</summary>
		/// <returns>The <see cref="T:System.Drawing.Color" /> of the background.</returns>
		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06002CC2 RID: 11458 RVA: 0x00027DA7 File Offset: 0x00025FA7
		// (set) Token: 0x06002CC3 RID: 11459 RVA: 0x000C9184 File Offset: 0x000C7384
		public override Color BackColor
		{
			get
			{
				if (this.ShouldSerializeBackColor())
				{
					return base.BackColor;
				}
				return SystemColors.Window;
			}
			set
			{
				base.BackColor = value;
				if (base.IsHandleCreated)
				{
					base.SendMessage(4097, 0, ColorTranslator.ToWin32(this.BackColor));
				}
			}
		}

		/// <summary>Gets or sets an <see cref="T:System.Windows.Forms.ImageLayout" /> value.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImageLayout" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is not one of the <see cref="T:System.Windows.Forms.ImageLayout" /> values.</exception>
		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06002CC4 RID: 11460 RVA: 0x0001189F File Offset: 0x0000FA9F
		// (set) Token: 0x06002CC5 RID: 11461 RVA: 0x000118A7 File Offset: 0x0000FAA7
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ListView.BackgroundImageLayout" /> property changes.</summary>
		// Token: 0x14000208 RID: 520
		// (add) Token: 0x06002CC6 RID: 11462 RVA: 0x000118B0 File Offset: 0x0000FAB0
		// (remove) Token: 0x06002CC7 RID: 11463 RVA: 0x000118B9 File Offset: 0x0000FAB9
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

		/// <summary>Gets or sets a value indicating whether the background image of the <see cref="T:System.Windows.Forms.ListView" /> should be tiled.</summary>
		/// <returns>
		///   <see langword="true" /> if the background image of the <see cref="T:System.Windows.Forms.ListView" /> should be tiled; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06002CC8 RID: 11464 RVA: 0x000C91AD File Offset: 0x000C73AD
		// (set) Token: 0x06002CC9 RID: 11465 RVA: 0x000C91C0 File Offset: 0x000C73C0
		[SRCategory("CatAppearance")]
		[DefaultValue(false)]
		[SRDescription("ListViewBackgroundImageTiledDescr")]
		public bool BackgroundImageTiled
		{
			get
			{
				return this.listViewState[65536];
			}
			set
			{
				if (this.BackgroundImageTiled != value)
				{
					this.listViewState[65536] = value;
					if (base.IsHandleCreated && this.BackgroundImage != null)
					{
						NativeMethods.LVBKIMAGE lvbkimage = new NativeMethods.LVBKIMAGE();
						lvbkimage.xOffset = 0;
						lvbkimage.yOffset = 0;
						if (this.BackgroundImageTiled)
						{
							lvbkimage.ulFlags = 16;
						}
						else
						{
							lvbkimage.ulFlags = 0;
						}
						lvbkimage.ulFlags |= 2;
						lvbkimage.pszImage = this.backgroundImageFileName;
						lvbkimage.cchImageMax = this.backgroundImageFileName.Length + 1;
						UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.LVM_SETBKIMAGE, 0, lvbkimage);
					}
				}
			}
		}

		/// <summary>Gets or sets the border style of the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. The default is <see cref="F:System.Windows.Forms.BorderStyle.Fixed3D" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is not one of the <see cref="T:System.Windows.Forms.BorderStyle" /> values.</exception>
		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06002CCA RID: 11466 RVA: 0x000C926D File Offset: 0x000C746D
		// (set) Token: 0x06002CCB RID: 11467 RVA: 0x000C9275 File Offset: 0x000C7475
		[SRCategory("CatAppearance")]
		[DefaultValue(BorderStyle.Fixed3D)]
		[DispId(-504)]
		[SRDescription("borderStyleDescr")]
		public BorderStyle BorderStyle
		{
			get
			{
				return this.borderStyle;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(BorderStyle));
				}
				if (this.borderStyle != value)
				{
					this.borderStyle = value;
					base.UpdateStyles();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether a check box appears next to each item in the control.</summary>
		/// <returns>
		///   <see langword="true" /> if a check box appears next to each item in the <see cref="T:System.Windows.Forms.ListView" /> control; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06002CCC RID: 11468 RVA: 0x000C92B3 File Offset: 0x000C74B3
		// (set) Token: 0x06002CCD RID: 11469 RVA: 0x000C92C4 File Offset: 0x000C74C4
		[SRCategory("CatAppearance")]
		[DefaultValue(false)]
		[SRDescription("ListViewCheckBoxesDescr")]
		public bool CheckBoxes
		{
			get
			{
				return this.listViewState[8];
			}
			set
			{
				if (this.UseCompatibleStateImageBehavior)
				{
					if (this.CheckBoxes != value)
					{
						if (value && this.View == View.Tile)
						{
							throw new NotSupportedException(SR.GetString("ListViewCheckBoxesNotSupportedInTileView"));
						}
						if (this.CheckBoxes)
						{
							this.savedCheckedItems = new List<ListViewItem>(this.CheckedItems.Count);
							ListViewItem[] array = new ListViewItem[this.CheckedItems.Count];
							this.CheckedItems.CopyTo(array, 0);
							for (int i = 0; i < array.Length; i++)
							{
								this.savedCheckedItems.Add(array[i]);
							}
						}
						this.listViewState[8] = value;
						this.UpdateExtendedStyles();
						if (this.CheckBoxes && this.savedCheckedItems != null)
						{
							if (this.savedCheckedItems.Count > 0)
							{
								foreach (ListViewItem listViewItem in this.savedCheckedItems)
								{
									listViewItem.Checked = true;
								}
							}
							this.savedCheckedItems = null;
						}
						if (this.AutoArrange)
						{
							this.ArrangeIcons(this.Alignment);
							return;
						}
					}
				}
				else if (this.CheckBoxes != value)
				{
					if (value && this.View == View.Tile)
					{
						throw new NotSupportedException(SR.GetString("ListViewCheckBoxesNotSupportedInTileView"));
					}
					if (this.CheckBoxes)
					{
						this.savedCheckedItems = new List<ListViewItem>(this.CheckedItems.Count);
						ListViewItem[] array2 = new ListViewItem[this.CheckedItems.Count];
						this.CheckedItems.CopyTo(array2, 0);
						for (int j = 0; j < array2.Length; j++)
						{
							this.savedCheckedItems.Add(array2[j]);
						}
					}
					this.listViewState[8] = value;
					if ((!value && this.StateImageList != null && base.IsHandleCreated) || (!value && this.Alignment == ListViewAlignment.Left && base.IsHandleCreated) || (value && this.View == View.List && base.IsHandleCreated) || (value && (this.View == View.SmallIcon || this.View == View.LargeIcon) && base.IsHandleCreated))
					{
						this.RecreateHandleInternal();
					}
					else
					{
						this.UpdateExtendedStyles();
					}
					if (this.CheckBoxes && this.savedCheckedItems != null)
					{
						if (this.savedCheckedItems.Count > 0)
						{
							foreach (ListViewItem listViewItem2 in this.savedCheckedItems)
							{
								listViewItem2.Checked = true;
							}
						}
						this.savedCheckedItems = null;
					}
					if (base.IsHandleCreated && this.imageListState != null)
					{
						if (this.CheckBoxes)
						{
							base.SendMessage(4099, 2, this.imageListState.Handle);
						}
						else
						{
							base.SendMessage(4099, 2, IntPtr.Zero);
						}
					}
					if (this.AutoArrange)
					{
						this.ArrangeIcons(this.Alignment);
					}
				}
			}
		}

		/// <summary>Gets the indexes of the currently checked items in the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" /> that contains the indexes of the currently checked items. If no items are currently checked, an empty <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" /> is returned.</returns>
		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06002CCE RID: 11470 RVA: 0x000C95B0 File Offset: 0x000C77B0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ListView.CheckedIndexCollection CheckedIndices
		{
			get
			{
				if (this.checkedIndexCollection == null)
				{
					this.checkedIndexCollection = new ListView.CheckedIndexCollection(this);
				}
				return this.checkedIndexCollection;
			}
		}

		/// <summary>Gets the currently checked items in the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListView.CheckedListViewItemCollection" /> that contains the currently checked items. If no items are currently checked, an empty <see cref="T:System.Windows.Forms.ListView.CheckedListViewItemCollection" /> is returned.</returns>
		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x06002CCF RID: 11471 RVA: 0x000C95CC File Offset: 0x000C77CC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ListView.CheckedListViewItemCollection CheckedItems
		{
			get
			{
				if (this.checkedListViewItemCollection == null)
				{
					this.checkedListViewItemCollection = new ListView.CheckedListViewItemCollection(this);
				}
				return this.checkedListViewItemCollection;
			}
		}

		/// <summary>Gets the collection of all column headers that appear in the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" /> that represents the column headers that appear when the <see cref="P:System.Windows.Forms.ListView.View" /> property is set to <see cref="F:System.Windows.Forms.View.Details" />.</returns>
		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x06002CD0 RID: 11472 RVA: 0x000C95E8 File Offset: 0x000C77E8
		[SRCategory("CatBehavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor("System.Windows.Forms.Design.ColumnHeaderCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[SRDescription("ListViewColumnsDescr")]
		[Localizable(true)]
		[MergableProperty(false)]
		public ListView.ColumnHeaderCollection Columns
		{
			get
			{
				return this.columnHeaderCollection;
			}
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06002CD1 RID: 11473 RVA: 0x000C95F0 File Offset: 0x000C77F0
		private bool ComctlSupportsVisualStyles
		{
			get
			{
				if (!this.listViewState[4194304])
				{
					this.listViewState[4194304] = true;
					this.listViewState[2097152] = Application.ComCtlSupportsVisualStyles;
				}
				return this.listViewState[2097152];
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>
		///   <see langword="null" /> in all cases.</returns>
		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x06002CD2 RID: 11474 RVA: 0x000C9648 File Offset: 0x000C7848
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ClassName = "SysListView32";
				if (base.IsHandleCreated)
				{
					int num = (int)(long)UnsafeNativeMethods.GetWindowLong(new HandleRef(this, base.Handle), -16);
					createParams.Style |= num & 3145728;
				}
				createParams.Style |= 64;
				ListViewAlignment listViewAlignment = this.alignStyle;
				if (listViewAlignment != ListViewAlignment.Left)
				{
					if (listViewAlignment == ListViewAlignment.Top)
					{
						createParams.Style |= 0;
					}
				}
				else
				{
					createParams.Style |= 2048;
				}
				if (this.AutoArrange)
				{
					createParams.Style |= 256;
				}
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
				ColumnHeaderStyle columnHeaderStyle = this.headerStyle;
				if (columnHeaderStyle != ColumnHeaderStyle.None)
				{
					if (columnHeaderStyle == ColumnHeaderStyle.Nonclickable)
					{
						createParams.Style |= 32768;
					}
				}
				else
				{
					createParams.Style |= 16384;
				}
				if (this.LabelEdit)
				{
					createParams.Style |= 512;
				}
				if (!this.LabelWrap)
				{
					createParams.Style |= 128;
				}
				if (!this.HideSelection)
				{
					createParams.Style |= 8;
				}
				if (!this.MultiSelect)
				{
					createParams.Style |= 4;
				}
				if (this.listItemSorter == null)
				{
					SortOrder sortOrder = this.sorting;
					if (sortOrder != SortOrder.Ascending)
					{
						if (sortOrder == SortOrder.Descending)
						{
							createParams.Style |= 32;
						}
					}
					else
					{
						createParams.Style |= 16;
					}
				}
				if (this.VirtualMode)
				{
					createParams.Style |= 4096;
				}
				if (this.viewStyle != View.Tile)
				{
					createParams.Style |= (int)this.viewStyle;
				}
				if (this.RightToLeft == RightToLeft.Yes && this.RightToLeftLayout)
				{
					createParams.ExStyle |= 4194304;
					createParams.ExStyle &= -28673;
				}
				return createParams;
			}
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06002CD3 RID: 11475 RVA: 0x000C986D File Offset: 0x000C7A6D
		internal ListViewGroup DefaultGroup
		{
			get
			{
				if (this.defaultGroup == null)
				{
					this.defaultGroup = new ListViewGroup(SR.GetString("ListViewGroupDefaultGroup", new object[] { "1" }));
				}
				return this.defaultGroup;
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06002CD4 RID: 11476 RVA: 0x000C98A0 File Offset: 0x000C7AA0
		protected override Size DefaultSize
		{
			get
			{
				return new Size(121, 97);
			}
		}

		/// <summary>Gets or sets a value indicating whether this control should redraw its surface using a secondary buffer to reduce or prevent flicker.</summary>
		/// <returns>
		///   <see langword="true" /> if the surface of the control should be drawn using double buffering; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06002CD5 RID: 11477 RVA: 0x00012FC3 File Offset: 0x000111C3
		// (set) Token: 0x06002CD6 RID: 11478 RVA: 0x000C98AB File Offset: 0x000C7AAB
		protected override bool DoubleBuffered
		{
			get
			{
				return base.DoubleBuffered;
			}
			set
			{
				if (this.DoubleBuffered != value)
				{
					base.DoubleBuffered = value;
					this.UpdateExtendedStyles();
				}
			}
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x06002CD7 RID: 11479 RVA: 0x000C98C3 File Offset: 0x000C7AC3
		internal bool ExpectingMouseUp
		{
			get
			{
				return this.listViewState[1048576];
			}
		}

		/// <summary>Gets or sets the item in the control that currently has focus.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the item that has focus, or <see langword="null" /> if no item has the focus in the <see cref="T:System.Windows.Forms.ListView" />.</returns>
		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x06002CD8 RID: 11480 RVA: 0x000C98D8 File Offset: 0x000C7AD8
		// (set) Token: 0x06002CD9 RID: 11481 RVA: 0x000C9913 File Offset: 0x000C7B13
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ListViewFocusedItemDescr")]
		public ListViewItem FocusedItem
		{
			get
			{
				if (base.IsHandleCreated)
				{
					int num = (int)(long)base.SendMessage(4108, -1, 1);
					if (num > -1)
					{
						return this.Items[num];
					}
				}
				return null;
			}
			set
			{
				if (base.IsHandleCreated && value != null)
				{
					value.Focused = true;
				}
			}
		}

		/// <summary>Gets or sets the foreground color.</summary>
		/// <returns>The <see cref="T:System.Drawing.Color" /> that is the foreground color.</returns>
		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06002CDA RID: 11482 RVA: 0x0001300E File Offset: 0x0001120E
		// (set) Token: 0x06002CDB RID: 11483 RVA: 0x000C9927 File Offset: 0x000C7B27
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
				if (base.IsHandleCreated)
				{
					base.SendMessage(4132, 0, ColorTranslator.ToWin32(this.ForeColor));
				}
			}
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06002CDC RID: 11484 RVA: 0x000C9950 File Offset: 0x000C7B50
		// (set) Token: 0x06002CDD RID: 11485 RVA: 0x000C9962 File Offset: 0x000C7B62
		private bool FlipViewToLargeIconAndSmallIcon
		{
			get
			{
				return this.listViewState[268435456];
			}
			set
			{
				this.listViewState[268435456] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether clicking an item selects all its subitems.</summary>
		/// <returns>
		///   <see langword="true" /> if clicking an item selects the item and all its subitems; <see langword="false" /> if clicking an item selects only the item itself. The default is <see langword="false" />.</returns>
		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x06002CDE RID: 11486 RVA: 0x000C9975 File Offset: 0x000C7B75
		// (set) Token: 0x06002CDF RID: 11487 RVA: 0x000C9984 File Offset: 0x000C7B84
		[SRCategory("CatAppearance")]
		[DefaultValue(false)]
		[SRDescription("ListViewFullRowSelectDescr")]
		public bool FullRowSelect
		{
			get
			{
				return this.listViewState[16];
			}
			set
			{
				if (this.FullRowSelect != value)
				{
					this.listViewState[16] = value;
					this.UpdateExtendedStyles();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether grid lines appear between the rows and columns containing the items and subitems in the control.</summary>
		/// <returns>
		///   <see langword="true" /> if grid lines are drawn around items and subitems; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06002CE0 RID: 11488 RVA: 0x000C99A3 File Offset: 0x000C7BA3
		// (set) Token: 0x06002CE1 RID: 11489 RVA: 0x000C99B2 File Offset: 0x000C7BB2
		[SRCategory("CatAppearance")]
		[DefaultValue(false)]
		[SRDescription("ListViewGridLinesDescr")]
		public bool GridLines
		{
			get
			{
				return this.listViewState[32];
			}
			set
			{
				if (this.GridLines != value)
				{
					this.listViewState[32] = value;
					this.UpdateExtendedStyles();
				}
			}
		}

		/// <summary>Gets the collection of <see cref="T:System.Windows.Forms.ListViewGroup" /> objects assigned to the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListViewGroupCollection" /> that contains all the groups in the <see cref="T:System.Windows.Forms.ListView" /> control.</returns>
		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06002CE2 RID: 11490 RVA: 0x000C99D1 File Offset: 0x000C7BD1
		[SRCategory("CatBehavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Localizable(true)]
		[Editor("System.Windows.Forms.Design.ListViewGroupCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[SRDescription("ListViewGroupsDescr")]
		[MergableProperty(false)]
		public ListViewGroupCollection Groups
		{
			get
			{
				if (this.groups == null)
				{
					this.groups = new ListViewGroupCollection(this);
				}
				return this.groups;
			}
		}

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x06002CE3 RID: 11491 RVA: 0x000C99ED File Offset: 0x000C7BED
		internal bool GroupsEnabled
		{
			get
			{
				return this.ShowGroups && this.groups != null && this.groups.Count > 0 && this.ComctlSupportsVisualStyles && !this.VirtualMode;
			}
		}

		/// <summary>Gets or sets the column header style.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ColumnHeaderStyle" /> values. The default is <see cref="F:System.Windows.Forms.ColumnHeaderStyle.Clickable" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is not one of the <see cref="T:System.Windows.Forms.ColumnHeaderStyle" /> values.</exception>
		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x06002CE4 RID: 11492 RVA: 0x000C9A20 File Offset: 0x000C7C20
		// (set) Token: 0x06002CE5 RID: 11493 RVA: 0x000C9A28 File Offset: 0x000C7C28
		[SRCategory("CatBehavior")]
		[DefaultValue(ColumnHeaderStyle.Clickable)]
		[SRDescription("ListViewHeaderStyleDescr")]
		public ColumnHeaderStyle HeaderStyle
		{
			get
			{
				return this.headerStyle;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ColumnHeaderStyle));
				}
				if (this.headerStyle != value)
				{
					this.headerStyle = value;
					if ((this.listViewState[8192] && value == ColumnHeaderStyle.Clickable) || (!this.listViewState[8192] && value == ColumnHeaderStyle.Nonclickable))
					{
						this.listViewState[8192] = !this.listViewState[8192];
						this.RecreateHandleInternal();
						return;
					}
					base.UpdateStyles();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the selected item in the control remains highlighted when the control loses focus.</summary>
		/// <returns>
		///   <see langword="true" /> if the selected item does not appear highlighted when the control loses focus; <see langword="false" /> if the selected item still appears highlighted when the control loses focus. The default is <see langword="true" />.</returns>
		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x06002CE6 RID: 11494 RVA: 0x000C9AC7 File Offset: 0x000C7CC7
		// (set) Token: 0x06002CE7 RID: 11495 RVA: 0x000C9AD6 File Offset: 0x000C7CD6
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("ListViewHideSelectionDescr")]
		public bool HideSelection
		{
			get
			{
				return this.listViewState[64];
			}
			set
			{
				if (this.HideSelection != value)
				{
					this.listViewState[64] = value;
					base.UpdateStyles();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the text of an item or subitem has the appearance of a hyperlink when the mouse pointer passes over it.</summary>
		/// <returns>
		///   <see langword="true" /> if the item text has the appearance of a hyperlink when the mouse passes over it; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06002CE8 RID: 11496 RVA: 0x000C9AF5 File Offset: 0x000C7CF5
		// (set) Token: 0x06002CE9 RID: 11497 RVA: 0x000C9B07 File Offset: 0x000C7D07
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("ListViewHotTrackingDescr")]
		public bool HotTracking
		{
			get
			{
				return this.listViewState[128];
			}
			set
			{
				if (this.HotTracking != value)
				{
					this.listViewState[128] = value;
					if (value)
					{
						this.HoverSelection = true;
						this.Activation = ItemActivation.OneClick;
					}
					this.UpdateExtendedStyles();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether an item is automatically selected when the mouse pointer remains over the item for a few seconds.</summary>
		/// <returns>
		///   <see langword="true" /> if an item is automatically selected when the mouse pointer hovers over it; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06002CEA RID: 11498 RVA: 0x000C9B3A File Offset: 0x000C7D3A
		// (set) Token: 0x06002CEB RID: 11499 RVA: 0x000C9B4C File Offset: 0x000C7D4C
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("ListViewHoverSelectDescr")]
		public bool HoverSelection
		{
			get
			{
				return this.listViewState[4096];
			}
			set
			{
				if (this.HoverSelection != value)
				{
					if (this.HotTracking && !value)
					{
						throw new ArgumentException(SR.GetString("ListViewHoverMustBeOnWhenHotTrackingIsOn"), "value");
					}
					this.listViewState[4096] = value;
					this.UpdateExtendedStyles();
				}
			}
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x06002CEC RID: 11500 RVA: 0x000C9B99 File Offset: 0x000C7D99
		internal bool InsertingItemsNatively
		{
			get
			{
				return this.listViewState1[1];
			}
		}

		/// <summary>Gets an object used to indicate the expected drop location when an item is dragged within a <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListViewInsertionMark" /> object representing the insertion mark.</returns>
		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x06002CED RID: 11501 RVA: 0x000C9BA7 File Offset: 0x000C7DA7
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ListViewInsertionMarkDescr")]
		public ListViewInsertionMark InsertionMark
		{
			get
			{
				if (this.insertionMark == null)
				{
					this.insertionMark = new ListViewInsertionMark(this);
				}
				return this.insertionMark;
			}
		}

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x06002CEE RID: 11502 RVA: 0x000C9BC3 File Offset: 0x000C7DC3
		// (set) Token: 0x06002CEF RID: 11503 RVA: 0x000C9BD5 File Offset: 0x000C7DD5
		private bool ItemCollectionChangedInMouseDown
		{
			get
			{
				return this.listViewState[134217728];
			}
			set
			{
				this.listViewState[134217728] = value;
			}
		}

		/// <summary>Gets a collection containing all items in the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> that contains all the items in the <see cref="T:System.Windows.Forms.ListView" /> control.</returns>
		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06002CF0 RID: 11504 RVA: 0x000C9BE8 File Offset: 0x000C7DE8
		[SRCategory("CatBehavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Localizable(true)]
		[Editor("System.Windows.Forms.Design.ListViewItemCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[SRDescription("ListViewItemsDescr")]
		[MergableProperty(false)]
		public ListView.ListViewItemCollection Items
		{
			get
			{
				return this.listItemCollection;
			}
		}

		/// <summary>Gets or sets a value indicating whether the user can edit the labels of items in the control.</summary>
		/// <returns>
		///   <see langword="true" /> if the user can edit the labels of items at run time; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x06002CF1 RID: 11505 RVA: 0x000C9BF0 File Offset: 0x000C7DF0
		// (set) Token: 0x06002CF2 RID: 11506 RVA: 0x000C9C02 File Offset: 0x000C7E02
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("ListViewLabelEditDescr")]
		public bool LabelEdit
		{
			get
			{
				return this.listViewState[256];
			}
			set
			{
				if (this.LabelEdit != value)
				{
					this.listViewState[256] = value;
					base.UpdateStyles();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether item labels wrap when items are displayed in the control as icons.</summary>
		/// <returns>
		///   <see langword="true" /> if item labels wrap when items are displayed as icons; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06002CF3 RID: 11507 RVA: 0x000C9C24 File Offset: 0x000C7E24
		// (set) Token: 0x06002CF4 RID: 11508 RVA: 0x000C9C36 File Offset: 0x000C7E36
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[Localizable(true)]
		[SRDescription("ListViewLabelWrapDescr")]
		public bool LabelWrap
		{
			get
			{
				return this.listViewState[512];
			}
			set
			{
				if (this.LabelWrap != value)
				{
					this.listViewState[512] = value;
					base.UpdateStyles();
				}
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ImageList" /> to use when displaying items as large icons in the control.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageList" /> that contains the icons to use when the <see cref="P:System.Windows.Forms.ListView.View" /> property is set to <see cref="F:System.Windows.Forms.View.LargeIcon" />. The default is <see langword="null" />.</returns>
		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06002CF5 RID: 11509 RVA: 0x000C9C58 File Offset: 0x000C7E58
		// (set) Token: 0x06002CF6 RID: 11510 RVA: 0x000C9C60 File Offset: 0x000C7E60
		[SRCategory("CatBehavior")]
		[DefaultValue(null)]
		[SRDescription("ListViewLargeImageListDescr")]
		public ImageList LargeImageList
		{
			get
			{
				return this.imageListLarge;
			}
			set
			{
				if (value != this.imageListLarge)
				{
					EventHandler eventHandler = new EventHandler(this.LargeImageListRecreateHandle);
					EventHandler eventHandler2 = new EventHandler(this.DetachImageList);
					EventHandler eventHandler3 = new EventHandler(this.LargeImageListChangedHandle);
					if (this.imageListLarge != null)
					{
						this.imageListLarge.RecreateHandle -= eventHandler;
						this.imageListLarge.Disposed -= eventHandler2;
						this.imageListLarge.ChangeHandle -= eventHandler3;
					}
					this.imageListLarge = value;
					if (value != null)
					{
						value.RecreateHandle += eventHandler;
						value.Disposed += eventHandler2;
						value.ChangeHandle += eventHandler3;
					}
					if (base.IsHandleCreated)
					{
						base.SendMessage(4099, (IntPtr)0, (value == null) ? IntPtr.Zero : value.Handle);
						if (this.AutoArrange && !this.listViewState1[4])
						{
							this.UpdateListViewItemsLocations();
						}
					}
				}
			}
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x06002CF7 RID: 11511 RVA: 0x000C9D31 File Offset: 0x000C7F31
		// (set) Token: 0x06002CF8 RID: 11512 RVA: 0x000C9D43 File Offset: 0x000C7F43
		internal bool ListViewHandleDestroyed
		{
			get
			{
				return this.listViewState[16777216];
			}
			set
			{
				this.listViewState[16777216] = value;
			}
		}

		/// <summary>Gets or sets the sorting comparer for the control.</summary>
		/// <returns>An <see cref="T:System.Collections.IComparer" /> that represents the sorting comparer for the control.</returns>
		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x06002CF9 RID: 11513 RVA: 0x000C9D56 File Offset: 0x000C7F56
		// (set) Token: 0x06002CFA RID: 11514 RVA: 0x000C9D5E File Offset: 0x000C7F5E
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ListViewItemSorterDescr")]
		public IComparer ListViewItemSorter
		{
			get
			{
				return this.listItemSorter;
			}
			set
			{
				if (this.listItemSorter != value)
				{
					this.listItemSorter = value;
					if (!this.VirtualMode)
					{
						this.Sort();
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether multiple items can be selected.</summary>
		/// <returns>
		///   <see langword="true" /> if multiple items in the control can be selected at one time; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x06002CFB RID: 11515 RVA: 0x000C9D7E File Offset: 0x000C7F7E
		// (set) Token: 0x06002CFC RID: 11516 RVA: 0x000C9D90 File Offset: 0x000C7F90
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("ListViewMultiSelectDescr")]
		public bool MultiSelect
		{
			get
			{
				return this.listViewState[1024];
			}
			set
			{
				if (this.MultiSelect != value)
				{
					this.listViewState[1024] = value;
					base.UpdateStyles();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ListView" /> control is drawn by the operating system or by code that you provide.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.ListView" /> control is drawn by code that you provide; <see langword="false" /> if the <see cref="T:System.Windows.Forms.ListView" /> control is drawn by the operating system. The default is <see langword="false" />.</returns>
		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x06002CFD RID: 11517 RVA: 0x000C9DB2 File Offset: 0x000C7FB2
		// (set) Token: 0x06002CFE RID: 11518 RVA: 0x000C9DC0 File Offset: 0x000C7FC0
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("ListViewOwnerDrawDescr")]
		public bool OwnerDraw
		{
			get
			{
				return this.listViewState[1];
			}
			set
			{
				if (this.OwnerDraw != value)
				{
					this.listViewState[1] = value;
					base.Invalidate(true);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the control is laid out from right to left.</summary>
		/// <returns>
		///   <see langword="true" /> to indicate the <see cref="T:System.Windows.Forms.ListView" /> control is laid out from right to left; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x06002CFF RID: 11519 RVA: 0x000C9DDF File Offset: 0x000C7FDF
		// (set) Token: 0x06002D00 RID: 11520 RVA: 0x000C9DE8 File Offset: 0x000C7FE8
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[DefaultValue(false)]
		[SRDescription("ControlRightToLeftLayoutDescr")]
		public virtual bool RightToLeftLayout
		{
			get
			{
				return this.rightToLeftLayout;
			}
			set
			{
				if (value != this.rightToLeftLayout)
				{
					this.rightToLeftLayout = value;
					using (new LayoutTransaction(this, this, PropertyNames.RightToLeftLayout))
					{
						this.OnRightToLeftLayoutChanged(EventArgs.Empty);
					}
				}
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ListView.RightToLeftLayout" /> property changes.</summary>
		// Token: 0x14000209 RID: 521
		// (add) Token: 0x06002D01 RID: 11521 RVA: 0x000C9E3C File Offset: 0x000C803C
		// (remove) Token: 0x06002D02 RID: 11522 RVA: 0x000C9E4F File Offset: 0x000C804F
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnRightToLeftLayoutChangedDescr")]
		public event EventHandler RightToLeftLayoutChanged
		{
			add
			{
				base.Events.AddHandler(ListView.EVENT_RIGHTTOLEFTLAYOUTCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EVENT_RIGHTTOLEFTLAYOUTCHANGED, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether a scroll bar is added to the control when there is not enough room to display all items.</summary>
		/// <returns>
		///   <see langword="true" /> if scroll bars are added to the control when necessary to allow the user to see all the items; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x06002D03 RID: 11523 RVA: 0x000C9E62 File Offset: 0x000C8062
		// (set) Token: 0x06002D04 RID: 11524 RVA: 0x000C9E74 File Offset: 0x000C8074
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("ListViewScrollableDescr")]
		public bool Scrollable
		{
			get
			{
				return this.listViewState[2048];
			}
			set
			{
				if (this.Scrollable != value)
				{
					this.listViewState[2048] = value;
					this.RecreateHandleInternal();
				}
			}
		}

		/// <summary>Gets the indexes of the selected items in the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" /> that contains the indexes of the selected items. If no items are currently selected, an empty <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" /> is returned.</returns>
		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x06002D05 RID: 11525 RVA: 0x000C9E96 File Offset: 0x000C8096
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ListView.SelectedIndexCollection SelectedIndices
		{
			get
			{
				if (this.selectedIndexCollection == null)
				{
					this.selectedIndexCollection = new ListView.SelectedIndexCollection(this);
				}
				return this.selectedIndexCollection;
			}
		}

		/// <summary>Gets the items that are selected in the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListView.SelectedListViewItemCollection" /> that contains the items that are selected in the control. If no items are currently selected, an empty <see cref="T:System.Windows.Forms.ListView.SelectedListViewItemCollection" /> is returned.</returns>
		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x06002D06 RID: 11526 RVA: 0x000C9EB2 File Offset: 0x000C80B2
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ListViewSelectedItemsDescr")]
		public ListView.SelectedListViewItemCollection SelectedItems
		{
			get
			{
				if (this.selectedListViewItemCollection == null)
				{
					this.selectedListViewItemCollection = new ListView.SelectedListViewItemCollection(this);
				}
				return this.selectedListViewItemCollection;
			}
		}

		/// <summary>Gets or sets a value indicating whether items are displayed in groups.</summary>
		/// <returns>
		///   <see langword="true" /> to display items in groups; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x06002D07 RID: 11527 RVA: 0x000C9ECE File Offset: 0x000C80CE
		// (set) Token: 0x06002D08 RID: 11528 RVA: 0x000C9EE0 File Offset: 0x000C80E0
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("ListViewShowGroupsDescr")]
		public bool ShowGroups
		{
			get
			{
				return this.listViewState[8388608];
			}
			set
			{
				if (value != this.ShowGroups)
				{
					this.listViewState[8388608] = value;
					if (base.IsHandleCreated)
					{
						this.UpdateGroupView();
					}
				}
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ImageList" /> to use when displaying items as small icons in the control.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageList" /> that contains the icons to use when the <see cref="P:System.Windows.Forms.ListView.View" /> property is set to <see cref="F:System.Windows.Forms.View.SmallIcon" />. The default is <see langword="null" />.</returns>
		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x06002D09 RID: 11529 RVA: 0x000C9F0A File Offset: 0x000C810A
		// (set) Token: 0x06002D0A RID: 11530 RVA: 0x000C9F14 File Offset: 0x000C8114
		[SRCategory("CatBehavior")]
		[DefaultValue(null)]
		[SRDescription("ListViewSmallImageListDescr")]
		public ImageList SmallImageList
		{
			get
			{
				return this.imageListSmall;
			}
			set
			{
				if (this.imageListSmall != value)
				{
					EventHandler eventHandler = new EventHandler(this.SmallImageListRecreateHandle);
					EventHandler eventHandler2 = new EventHandler(this.DetachImageList);
					if (this.imageListSmall != null)
					{
						this.imageListSmall.RecreateHandle -= eventHandler;
						this.imageListSmall.Disposed -= eventHandler2;
					}
					this.imageListSmall = value;
					if (value != null)
					{
						value.RecreateHandle += eventHandler;
						value.Disposed += eventHandler2;
					}
					if (base.IsHandleCreated)
					{
						base.SendMessage(4099, (IntPtr)1, (value == null) ? IntPtr.Zero : value.Handle);
						if (this.View == View.SmallIcon)
						{
							this.View = View.LargeIcon;
							this.View = View.SmallIcon;
						}
						else if (!this.listViewState1[4])
						{
							this.UpdateListViewItemsLocations();
						}
						if (this.View == View.Details)
						{
							base.Invalidate(true);
						}
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether ToolTips are shown for the <see cref="T:System.Windows.Forms.ListViewItem" /> objects contained in the <see cref="T:System.Windows.Forms.ListView" />.</summary>
		/// <returns>
		///   <see langword="true" /> if <see cref="T:System.Windows.Forms.ListViewItem" /> ToolTips should be shown; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x06002D0B RID: 11531 RVA: 0x000C9FE6 File Offset: 0x000C81E6
		// (set) Token: 0x06002D0C RID: 11532 RVA: 0x000C9FF8 File Offset: 0x000C81F8
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("ListViewShowItemToolTipsDescr")]
		public bool ShowItemToolTips
		{
			get
			{
				return this.listViewState[32768];
			}
			set
			{
				if (this.ShowItemToolTips != value)
				{
					this.listViewState[32768] = value;
					this.RecreateHandleInternal();
				}
			}
		}

		/// <summary>Gets or sets the sort order for items in the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.SortOrder" /> values. The default is <see cref="F:System.Windows.Forms.SortOrder.None" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is not one of the <see cref="T:System.Windows.Forms.SortOrder" /> values.</exception>
		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x06002D0D RID: 11533 RVA: 0x000CA01A File Offset: 0x000C821A
		// (set) Token: 0x06002D0E RID: 11534 RVA: 0x000CA024 File Offset: 0x000C8224
		[SRCategory("CatBehavior")]
		[DefaultValue(SortOrder.None)]
		[SRDescription("ListViewSortingDescr")]
		public SortOrder Sorting
		{
			get
			{
				return this.sorting;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(SortOrder));
				}
				if (this.sorting != value)
				{
					this.sorting = value;
					if (this.View == View.LargeIcon || this.View == View.SmallIcon)
					{
						if (this.listItemSorter == null)
						{
							this.listItemSorter = new ListView.IconComparer(this.sorting);
						}
						else if (this.listItemSorter is ListView.IconComparer)
						{
							((ListView.IconComparer)this.listItemSorter).SortOrder = this.sorting;
						}
					}
					else if (value == SortOrder.None)
					{
						this.listItemSorter = null;
					}
					if (value == SortOrder.None)
					{
						base.UpdateStyles();
						return;
					}
					this.RecreateHandleInternal();
				}
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ImageList" /> associated with application-defined states in the control.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageList" /> that contains a set of state images that can be used to indicate an application-defined state of an item. The default is <see langword="null" />.</returns>
		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x06002D0F RID: 11535 RVA: 0x000CA0D2 File Offset: 0x000C82D2
		// (set) Token: 0x06002D10 RID: 11536 RVA: 0x000CA0DC File Offset: 0x000C82DC
		[SRCategory("CatBehavior")]
		[DefaultValue(null)]
		[SRDescription("ListViewStateImageListDescr")]
		public ImageList StateImageList
		{
			get
			{
				return this.imageListState;
			}
			set
			{
				if (this.UseCompatibleStateImageBehavior)
				{
					if (this.imageListState != value)
					{
						EventHandler eventHandler = new EventHandler(this.StateImageListRecreateHandle);
						EventHandler eventHandler2 = new EventHandler(this.DetachImageList);
						if (this.imageListState != null)
						{
							this.imageListState.RecreateHandle -= eventHandler;
							this.imageListState.Disposed -= eventHandler2;
						}
						this.imageListState = value;
						if (value != null)
						{
							value.RecreateHandle += eventHandler;
							value.Disposed += eventHandler2;
						}
						if (base.IsHandleCreated)
						{
							base.SendMessage(4099, 2, (value == null) ? IntPtr.Zero : value.Handle);
							return;
						}
					}
				}
				else if (this.imageListState != value)
				{
					EventHandler eventHandler3 = new EventHandler(this.StateImageListRecreateHandle);
					EventHandler eventHandler4 = new EventHandler(this.DetachImageList);
					if (this.imageListState != null)
					{
						this.imageListState.RecreateHandle -= eventHandler3;
						this.imageListState.Disposed -= eventHandler4;
					}
					if (base.IsHandleCreated && this.imageListState != null && this.CheckBoxes)
					{
						base.SendMessage(4099, 2, IntPtr.Zero);
					}
					this.imageListState = value;
					if (value != null)
					{
						value.RecreateHandle += eventHandler3;
						value.Disposed += eventHandler4;
					}
					if (base.IsHandleCreated)
					{
						if (this.CheckBoxes)
						{
							this.RecreateHandleInternal();
						}
						else
						{
							base.SendMessage(4099, 2, (this.imageListState == null || this.imageListState.Images.Count == 0) ? IntPtr.Zero : this.imageListState.Handle);
						}
						if (!this.listViewState1[4])
						{
							this.UpdateListViewItemsLocations();
						}
					}
				}
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>The text to display in the <see cref="T:System.Windows.Forms.ListView" /> control.</returns>
		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x06002D11 RID: 11537 RVA: 0x00013814 File Offset: 0x00011A14
		// (set) Token: 0x06002D12 RID: 11538 RVA: 0x00023FE9 File Offset: 0x000221E9
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Bindable(false)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ListView.Text" /> property changes.</summary>
		// Token: 0x1400020A RID: 522
		// (add) Token: 0x06002D13 RID: 11539 RVA: 0x00046591 File Offset: 0x00044791
		// (remove) Token: 0x06002D14 RID: 11540 RVA: 0x0004659A File Offset: 0x0004479A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TextChanged
		{
			add
			{
				base.TextChanged += value;
			}
			remove
			{
				base.TextChanged -= value;
			}
		}

		/// <summary>Gets or sets the size of the tiles shown in tile view.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that contains the new tile size.</returns>
		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x06002D15 RID: 11541 RVA: 0x000CA268 File Offset: 0x000C8468
		// (set) Token: 0x06002D16 RID: 11542 RVA: 0x000CA2D8 File Offset: 0x000C84D8
		[SRCategory("CatAppearance")]
		[Browsable(true)]
		[SRDescription("ListViewTileSizeDescr")]
		public Size TileSize
		{
			get
			{
				if (!this.tileSize.IsEmpty)
				{
					return this.tileSize;
				}
				if (base.IsHandleCreated)
				{
					NativeMethods.LVTILEVIEWINFO lvtileviewinfo = new NativeMethods.LVTILEVIEWINFO();
					lvtileviewinfo.dwMask = 1;
					UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4259, 0, lvtileviewinfo);
					return new Size(lvtileviewinfo.sizeTile.cx, lvtileviewinfo.sizeTile.cy);
				}
				return Size.Empty;
			}
			set
			{
				if (this.tileSize != value)
				{
					if (value.IsEmpty || value.Height <= 0 || value.Width <= 0)
					{
						throw new ArgumentOutOfRangeException("TileSize", SR.GetString("ListViewTileSizeMustBePositive"));
					}
					this.tileSize = value;
					if (base.IsHandleCreated)
					{
						NativeMethods.LVTILEVIEWINFO lvtileviewinfo = new NativeMethods.LVTILEVIEWINFO();
						lvtileviewinfo.dwMask = 1;
						lvtileviewinfo.dwFlags = 3;
						lvtileviewinfo.sizeTile = new NativeMethods.SIZE(this.tileSize.Width, this.tileSize.Height);
						bool flag = UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4258, 0, lvtileviewinfo);
						if (this.AutoArrange)
						{
							this.UpdateListViewItemsLocations();
						}
					}
				}
			}
		}

		// Token: 0x06002D17 RID: 11543 RVA: 0x000CA393 File Offset: 0x000C8593
		private bool ShouldSerializeTileSize()
		{
			return !this.tileSize.Equals(Size.Empty);
		}

		/// <summary>Gets or sets the first visible item in the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the first visible item in the control.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.ListView.View" /> property is set to <see cref="F:System.Windows.Forms.View.LargeIcon" />,  <see cref="F:System.Windows.Forms.View.SmallIcon" />, or <see cref="F:System.Windows.Forms.View.Tile" />.</exception>
		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x06002D18 RID: 11544 RVA: 0x000CA3B4 File Offset: 0x000C85B4
		// (set) Token: 0x06002D19 RID: 11545 RVA: 0x000CA458 File Offset: 0x000C8658
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ListViewTopItemDescr")]
		public ListViewItem TopItem
		{
			get
			{
				if (this.viewStyle == View.LargeIcon || this.viewStyle == View.SmallIcon || this.viewStyle == View.Tile)
				{
					throw new InvalidOperationException(SR.GetString("ListViewGetTopItem"));
				}
				if (!base.IsHandleCreated)
				{
					if (this.Items.Count > 0)
					{
						return this.Items[0];
					}
					return null;
				}
				else
				{
					this.topIndex = (int)(long)base.SendMessage(4135, 0, 0);
					if (this.topIndex >= 0 && this.topIndex < this.Items.Count)
					{
						return this.Items[this.topIndex];
					}
					return null;
				}
			}
			set
			{
				if (this.viewStyle == View.LargeIcon || this.viewStyle == View.SmallIcon || this.viewStyle == View.Tile)
				{
					throw new InvalidOperationException(SR.GetString("ListViewSetTopItem"));
				}
				if (value == null)
				{
					return;
				}
				if (value.ListView != this)
				{
					return;
				}
				if (!base.IsHandleCreated)
				{
					this.CreateHandle();
				}
				if (value == this.TopItem)
				{
					return;
				}
				this.EnsureVisible(value.Index);
				ListViewItem topItem = this.TopItem;
				if (topItem == null && this.topIndex == this.Items.Count)
				{
					if (this.Scrollable)
					{
						this.EnsureVisible(0);
						this.Scroll(0, value.Index);
					}
					return;
				}
				if (value.Index == topItem.Index)
				{
					return;
				}
				if (this.Scrollable)
				{
					this.Scroll(topItem.Index, value.Index);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ListView" /> uses state image behavior that is compatible with the .NET Framework 1.1 or the .NET Framework 2.0.</summary>
		/// <returns>
		///   <see langword="true" /> if the state image behavior is compatible with the .NET Framework 1.1; <see langword="false" /> if the behavior is compatible with the .NET Framework 2.0. The default is <see langword="true" />.</returns>
		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x06002D1A RID: 11546 RVA: 0x000CA526 File Offset: 0x000C8726
		// (set) Token: 0x06002D1B RID: 11547 RVA: 0x000CA534 File Offset: 0x000C8734
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DefaultValue(true)]
		public bool UseCompatibleStateImageBehavior
		{
			get
			{
				return this.listViewState1[8];
			}
			set
			{
				this.listViewState1[8] = value;
			}
		}

		/// <summary>Gets or sets how items are displayed in the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.View" /> values. The default is <see cref="F:System.Windows.Forms.View.LargeIcon" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is not one of the <see cref="T:System.Windows.Forms.View" /> values.</exception>
		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x06002D1C RID: 11548 RVA: 0x000CA543 File Offset: 0x000C8743
		// (set) Token: 0x06002D1D RID: 11549 RVA: 0x000CA54C File Offset: 0x000C874C
		[SRCategory("CatAppearance")]
		[DefaultValue(View.LargeIcon)]
		[SRDescription("ListViewViewDescr")]
		public View View
		{
			get
			{
				return this.viewStyle;
			}
			set
			{
				if (value == View.Tile && this.CheckBoxes)
				{
					throw new NotSupportedException(SR.GetString("ListViewTileViewDoesNotSupportCheckBoxes"));
				}
				this.FlipViewToLargeIconAndSmallIcon = false;
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 4))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(View));
				}
				if (value == View.Tile && this.VirtualMode)
				{
					throw new NotSupportedException(SR.GetString("ListViewCantSetViewToTileViewInVirtualMode"));
				}
				if (this.viewStyle != value)
				{
					this.viewStyle = value;
					if (base.IsHandleCreated && this.ComctlSupportsVisualStyles)
					{
						base.SendMessage(4238, (int)this.viewStyle, 0);
						this.UpdateGroupView();
						if (this.viewStyle == View.Tile)
						{
							this.UpdateTileView();
						}
					}
					else
					{
						base.UpdateStyles();
					}
					this.UpdateListViewItemsLocations();
				}
			}
		}

		/// <summary>Gets or sets the number of <see cref="T:System.Windows.Forms.ListViewItem" /> objects contained in the list when in virtual mode.</summary>
		/// <returns>The number of <see cref="T:System.Windows.Forms.ListViewItem" /> objects contained in the <see cref="T:System.Windows.Forms.ListView" /> when in virtual mode.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Windows.Forms.ListView.VirtualListSize" /> is set to a value less than 0.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.ListView.VirtualMode" /> is set to <see langword="true" />, <see cref="P:System.Windows.Forms.ListView.VirtualListSize" /> is greater than 0, and <see cref="E:System.Windows.Forms.ListView.RetrieveVirtualItem" /> is not handled.</exception>
		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x06002D1E RID: 11550 RVA: 0x000CA614 File Offset: 0x000C8814
		// (set) Token: 0x06002D1F RID: 11551 RVA: 0x000CA61C File Offset: 0x000C881C
		[SRCategory("CatBehavior")]
		[DefaultValue(0)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRDescription("ListViewVirtualListSizeDescr")]
		public int VirtualListSize
		{
			get
			{
				return this.virtualListSize;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException(SR.GetString("ListViewVirtualListSizeInvalidArgument", new object[]
					{
						"value",
						value.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (value == this.virtualListSize)
				{
					return;
				}
				bool flag = base.IsHandleCreated && this.VirtualMode && this.View == View.Details && !base.DesignMode;
				int num = -1;
				if (flag)
				{
					num = (int)(long)base.SendMessage(4135, 0, 0);
				}
				this.virtualListSize = value;
				if (base.IsHandleCreated && this.VirtualMode && !base.DesignMode)
				{
					base.SendMessage(4143, this.virtualListSize, 0);
				}
				if (flag)
				{
					num = Math.Min(num, this.VirtualListSize - 1);
					if (num > 0)
					{
						ListViewItem listViewItem = this.Items[num];
						this.TopItem = listViewItem;
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether you have provided your own data-management operations for the <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
		/// <returns>
		///   <see langword="true" /> if <see cref="T:System.Windows.Forms.ListView" /> uses data-management operations that you provide; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.ListView.VirtualMode" /> is set to <see langword="true" /> and one of the following conditions exist:  
		///
		/// <see cref="P:System.Windows.Forms.ListView.VirtualListSize" /> is greater than 0 and <see cref="E:System.Windows.Forms.ListView.RetrieveVirtualItem" /> is not handled.  
		///  -or-  
		///
		/// <see cref="P:System.Windows.Forms.ListView.Items" />, <see cref="P:System.Windows.Forms.ListView.CheckedItems" />, or <see cref="P:System.Windows.Forms.ListView.SelectedItems" /> contains items.  
		///  -or-  
		///
		/// Edits are made to <see cref="P:System.Windows.Forms.ListView.Items" />.</exception>
		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06002D20 RID: 11552 RVA: 0x000CA6FF File Offset: 0x000C88FF
		// (set) Token: 0x06002D21 RID: 11553 RVA: 0x000CA714 File Offset: 0x000C8914
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRDescription("ListViewVirtualModeDescr")]
		public bool VirtualMode
		{
			get
			{
				return this.listViewState[33554432];
			}
			set
			{
				if (value == this.VirtualMode)
				{
					return;
				}
				if (value && this.Items.Count > 0)
				{
					throw new InvalidOperationException(SR.GetString("ListViewVirtualListViewRequiresNoItems"));
				}
				if (value && this.CheckedItems.Count > 0)
				{
					throw new InvalidOperationException(SR.GetString("ListViewVirtualListViewRequiresNoCheckedItems"));
				}
				if (value && this.SelectedItems.Count > 0)
				{
					throw new InvalidOperationException(SR.GetString("ListViewVirtualListViewRequiresNoSelectedItems"));
				}
				if (value && this.View == View.Tile)
				{
					throw new NotSupportedException(SR.GetString("ListViewCantSetVirtualModeWhenInTileView"));
				}
				this.listViewState[33554432] = value;
				this.RecreateHandleInternal();
			}
		}

		/// <summary>Occurs when the label for an item is edited by the user.</summary>
		// Token: 0x1400020B RID: 523
		// (add) Token: 0x06002D22 RID: 11554 RVA: 0x000CA7C1 File Offset: 0x000C89C1
		// (remove) Token: 0x06002D23 RID: 11555 RVA: 0x000CA7DA File Offset: 0x000C89DA
		[SRCategory("CatBehavior")]
		[SRDescription("ListViewAfterLabelEditDescr")]
		public event LabelEditEventHandler AfterLabelEdit
		{
			add
			{
				this.onAfterLabelEdit = (LabelEditEventHandler)Delegate.Combine(this.onAfterLabelEdit, value);
			}
			remove
			{
				this.onAfterLabelEdit = (LabelEditEventHandler)Delegate.Remove(this.onAfterLabelEdit, value);
			}
		}

		/// <summary>Occurs when the user starts editing the label of an item.</summary>
		// Token: 0x1400020C RID: 524
		// (add) Token: 0x06002D24 RID: 11556 RVA: 0x000CA7F3 File Offset: 0x000C89F3
		// (remove) Token: 0x06002D25 RID: 11557 RVA: 0x000CA80C File Offset: 0x000C8A0C
		[SRCategory("CatBehavior")]
		[SRDescription("ListViewBeforeLabelEditDescr")]
		public event LabelEditEventHandler BeforeLabelEdit
		{
			add
			{
				this.onBeforeLabelEdit = (LabelEditEventHandler)Delegate.Combine(this.onBeforeLabelEdit, value);
			}
			remove
			{
				this.onBeforeLabelEdit = (LabelEditEventHandler)Delegate.Remove(this.onBeforeLabelEdit, value);
			}
		}

		/// <summary>Occurs when the contents of the display area for a <see cref="T:System.Windows.Forms.ListView" /> in virtual mode has changed, and the <see cref="T:System.Windows.Forms.ListView" /> determines that a new range of items is needed.</summary>
		// Token: 0x1400020D RID: 525
		// (add) Token: 0x06002D26 RID: 11558 RVA: 0x000CA825 File Offset: 0x000C8A25
		// (remove) Token: 0x06002D27 RID: 11559 RVA: 0x000CA838 File Offset: 0x000C8A38
		[SRCategory("CatAction")]
		[SRDescription("ListViewCacheVirtualItemsEventDescr")]
		public event CacheVirtualItemsEventHandler CacheVirtualItems
		{
			add
			{
				base.Events.AddHandler(ListView.EVENT_CACHEVIRTUALITEMS, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EVENT_CACHEVIRTUALITEMS, value);
			}
		}

		/// <summary>Occurs when the user clicks a column header within the list view control.</summary>
		// Token: 0x1400020E RID: 526
		// (add) Token: 0x06002D28 RID: 11560 RVA: 0x000CA84B File Offset: 0x000C8A4B
		// (remove) Token: 0x06002D29 RID: 11561 RVA: 0x000CA864 File Offset: 0x000C8A64
		[SRCategory("CatAction")]
		[SRDescription("ListViewColumnClickDescr")]
		public event ColumnClickEventHandler ColumnClick
		{
			add
			{
				this.onColumnClick = (ColumnClickEventHandler)Delegate.Combine(this.onColumnClick, value);
			}
			remove
			{
				this.onColumnClick = (ColumnClickEventHandler)Delegate.Remove(this.onColumnClick, value);
			}
		}

		/// <summary>Occurs when the column header order is changed.</summary>
		// Token: 0x1400020F RID: 527
		// (add) Token: 0x06002D2A RID: 11562 RVA: 0x000CA87D File Offset: 0x000C8A7D
		// (remove) Token: 0x06002D2B RID: 11563 RVA: 0x000CA890 File Offset: 0x000C8A90
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ListViewColumnReorderedDscr")]
		public event ColumnReorderedEventHandler ColumnReordered
		{
			add
			{
				base.Events.AddHandler(ListView.EVENT_COLUMNREORDERED, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EVENT_COLUMNREORDERED, value);
			}
		}

		/// <summary>Occurs after the width of a column is successfully changed.</summary>
		// Token: 0x14000210 RID: 528
		// (add) Token: 0x06002D2C RID: 11564 RVA: 0x000CA8A3 File Offset: 0x000C8AA3
		// (remove) Token: 0x06002D2D RID: 11565 RVA: 0x000CA8B6 File Offset: 0x000C8AB6
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ListViewColumnWidthChangedDscr")]
		public event ColumnWidthChangedEventHandler ColumnWidthChanged
		{
			add
			{
				base.Events.AddHandler(ListView.EVENT_COLUMNWIDTHCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EVENT_COLUMNWIDTHCHANGED, value);
			}
		}

		/// <summary>Occurs when the width of a column is changing.</summary>
		// Token: 0x14000211 RID: 529
		// (add) Token: 0x06002D2E RID: 11566 RVA: 0x000CA8C9 File Offset: 0x000C8AC9
		// (remove) Token: 0x06002D2F RID: 11567 RVA: 0x000CA8DC File Offset: 0x000C8ADC
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ListViewColumnWidthChangingDscr")]
		public event ColumnWidthChangingEventHandler ColumnWidthChanging
		{
			add
			{
				base.Events.AddHandler(ListView.EVENT_COLUMNWIDTHCHANGING, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EVENT_COLUMNWIDTHCHANGING, value);
			}
		}

		/// <summary>Occurs when the details view of a <see cref="T:System.Windows.Forms.ListView" /> is drawn and the <see cref="P:System.Windows.Forms.ListView.OwnerDraw" /> property is set to <see langword="true" />.</summary>
		// Token: 0x14000212 RID: 530
		// (add) Token: 0x06002D30 RID: 11568 RVA: 0x000CA8EF File Offset: 0x000C8AEF
		// (remove) Token: 0x06002D31 RID: 11569 RVA: 0x000CA902 File Offset: 0x000C8B02
		[SRCategory("CatBehavior")]
		[SRDescription("ListViewDrawColumnHeaderEventDescr")]
		public event DrawListViewColumnHeaderEventHandler DrawColumnHeader
		{
			add
			{
				base.Events.AddHandler(ListView.EVENT_DRAWCOLUMNHEADER, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EVENT_DRAWCOLUMNHEADER, value);
			}
		}

		/// <summary>Occurs when a <see cref="T:System.Windows.Forms.ListView" /> is drawn and the <see cref="P:System.Windows.Forms.ListView.OwnerDraw" /> property is set to <see langword="true" />.</summary>
		// Token: 0x14000213 RID: 531
		// (add) Token: 0x06002D32 RID: 11570 RVA: 0x000CA915 File Offset: 0x000C8B15
		// (remove) Token: 0x06002D33 RID: 11571 RVA: 0x000CA928 File Offset: 0x000C8B28
		[SRCategory("CatBehavior")]
		[SRDescription("ListViewDrawItemEventDescr")]
		public event DrawListViewItemEventHandler DrawItem
		{
			add
			{
				base.Events.AddHandler(ListView.EVENT_DRAWITEM, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EVENT_DRAWITEM, value);
			}
		}

		/// <summary>Occurs when the details view of a <see cref="T:System.Windows.Forms.ListView" /> is drawn and the <see cref="P:System.Windows.Forms.ListView.OwnerDraw" /> property is set to <see langword="true" />.</summary>
		// Token: 0x14000214 RID: 532
		// (add) Token: 0x06002D34 RID: 11572 RVA: 0x000CA93B File Offset: 0x000C8B3B
		// (remove) Token: 0x06002D35 RID: 11573 RVA: 0x000CA94E File Offset: 0x000C8B4E
		[SRCategory("CatBehavior")]
		[SRDescription("ListViewDrawSubItemEventDescr")]
		public event DrawListViewSubItemEventHandler DrawSubItem
		{
			add
			{
				base.Events.AddHandler(ListView.EVENT_DRAWSUBITEM, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EVENT_DRAWSUBITEM, value);
			}
		}

		/// <summary>Occurs when an item is activated.</summary>
		// Token: 0x14000215 RID: 533
		// (add) Token: 0x06002D36 RID: 11574 RVA: 0x000CA961 File Offset: 0x000C8B61
		// (remove) Token: 0x06002D37 RID: 11575 RVA: 0x000CA97A File Offset: 0x000C8B7A
		[SRCategory("CatAction")]
		[SRDescription("ListViewItemClickDescr")]
		public event EventHandler ItemActivate
		{
			add
			{
				this.onItemActivate = (EventHandler)Delegate.Combine(this.onItemActivate, value);
			}
			remove
			{
				this.onItemActivate = (EventHandler)Delegate.Remove(this.onItemActivate, value);
			}
		}

		/// <summary>Occurs when the check state of an item changes.</summary>
		// Token: 0x14000216 RID: 534
		// (add) Token: 0x06002D38 RID: 11576 RVA: 0x000CA993 File Offset: 0x000C8B93
		// (remove) Token: 0x06002D39 RID: 11577 RVA: 0x000CA9AC File Offset: 0x000C8BAC
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

		/// <summary>Occurs when the checked state of an item changes.</summary>
		// Token: 0x14000217 RID: 535
		// (add) Token: 0x06002D3A RID: 11578 RVA: 0x000CA9C5 File Offset: 0x000C8BC5
		// (remove) Token: 0x06002D3B RID: 11579 RVA: 0x000CA9DE File Offset: 0x000C8BDE
		[SRCategory("CatBehavior")]
		[SRDescription("ListViewItemCheckedDescr")]
		public event ItemCheckedEventHandler ItemChecked
		{
			add
			{
				this.onItemChecked = (ItemCheckedEventHandler)Delegate.Combine(this.onItemChecked, value);
			}
			remove
			{
				this.onItemChecked = (ItemCheckedEventHandler)Delegate.Remove(this.onItemChecked, value);
			}
		}

		/// <summary>Occurs when the user begins dragging an item.</summary>
		// Token: 0x14000218 RID: 536
		// (add) Token: 0x06002D3C RID: 11580 RVA: 0x000CA9F7 File Offset: 0x000C8BF7
		// (remove) Token: 0x06002D3D RID: 11581 RVA: 0x000CAA10 File Offset: 0x000C8C10
		[SRCategory("CatAction")]
		[SRDescription("ListViewItemDragDescr")]
		public event ItemDragEventHandler ItemDrag
		{
			add
			{
				this.onItemDrag = (ItemDragEventHandler)Delegate.Combine(this.onItemDrag, value);
			}
			remove
			{
				this.onItemDrag = (ItemDragEventHandler)Delegate.Remove(this.onItemDrag, value);
			}
		}

		/// <summary>Occurs when the mouse hovers over an item.</summary>
		// Token: 0x14000219 RID: 537
		// (add) Token: 0x06002D3E RID: 11582 RVA: 0x000CAA29 File Offset: 0x000C8C29
		// (remove) Token: 0x06002D3F RID: 11583 RVA: 0x000CAA42 File Offset: 0x000C8C42
		[SRCategory("CatAction")]
		[SRDescription("ListViewItemMouseHoverDescr")]
		public event ListViewItemMouseHoverEventHandler ItemMouseHover
		{
			add
			{
				this.onItemMouseHover = (ListViewItemMouseHoverEventHandler)Delegate.Combine(this.onItemMouseHover, value);
			}
			remove
			{
				this.onItemMouseHover = (ListViewItemMouseHoverEventHandler)Delegate.Remove(this.onItemMouseHover, value);
			}
		}

		/// <summary>Occurs when the selection state of an item changes.</summary>
		// Token: 0x1400021A RID: 538
		// (add) Token: 0x06002D40 RID: 11584 RVA: 0x000CAA5B File Offset: 0x000C8C5B
		// (remove) Token: 0x06002D41 RID: 11585 RVA: 0x000CAA6E File Offset: 0x000C8C6E
		[SRCategory("CatBehavior")]
		[SRDescription("ListViewItemSelectionChangedDescr")]
		public event ListViewItemSelectionChangedEventHandler ItemSelectionChanged
		{
			add
			{
				base.Events.AddHandler(ListView.EVENT_ITEMSELECTIONCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EVENT_ITEMSELECTIONCHANGED, value);
			}
		}

		/// <summary>Gets or sets the space between the <see cref="T:System.Windows.Forms.ListView" /> control and its contents.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Padding" /> that specifies the space between the <see cref="T:System.Windows.Forms.ListView" /> control and its contents.</returns>
		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06002D42 RID: 11586 RVA: 0x00013442 File Offset: 0x00011642
		// (set) Token: 0x06002D43 RID: 11587 RVA: 0x0001344A File Offset: 0x0001164A
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ListView.Padding" /> property changes.</summary>
		// Token: 0x1400021B RID: 539
		// (add) Token: 0x06002D44 RID: 11588 RVA: 0x00013453 File Offset: 0x00011653
		// (remove) Token: 0x06002D45 RID: 11589 RVA: 0x0001345C File Offset: 0x0001165C
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

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ListView" /> control is painted.</summary>
		// Token: 0x1400021C RID: 540
		// (add) Token: 0x06002D46 RID: 11590 RVA: 0x00013D73 File Offset: 0x00011F73
		// (remove) Token: 0x06002D47 RID: 11591 RVA: 0x00013D7C File Offset: 0x00011F7C
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

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ListView" /> is in virtual mode and requires a <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.RetrieveVirtualItemEventArgs.Item" /> property is not set to an item when the <see cref="E:System.Windows.Forms.ListView.RetrieveVirtualItem" /> event is handled.</exception>
		// Token: 0x1400021D RID: 541
		// (add) Token: 0x06002D48 RID: 11592 RVA: 0x000CAA81 File Offset: 0x000C8C81
		// (remove) Token: 0x06002D49 RID: 11593 RVA: 0x000CAA94 File Offset: 0x000C8C94
		[SRCategory("CatAction")]
		[SRDescription("ListViewRetrieveVirtualItemEventDescr")]
		public event RetrieveVirtualItemEventHandler RetrieveVirtualItem
		{
			add
			{
				base.Events.AddHandler(ListView.EVENT_RETRIEVEVIRTUALITEM, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EVENT_RETRIEVEVIRTUALITEM, value);
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ListView" /> is in virtual mode and a search is taking place.</summary>
		// Token: 0x1400021E RID: 542
		// (add) Token: 0x06002D4A RID: 11594 RVA: 0x000CAAA7 File Offset: 0x000C8CA7
		// (remove) Token: 0x06002D4B RID: 11595 RVA: 0x000CAABA File Offset: 0x000C8CBA
		[SRCategory("CatAction")]
		[SRDescription("ListViewSearchForVirtualItemDescr")]
		public event SearchForVirtualItemEventHandler SearchForVirtualItem
		{
			add
			{
				base.Events.AddHandler(ListView.EVENT_SEARCHFORVIRTUALITEM, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EVENT_SEARCHFORVIRTUALITEM, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ListView.SelectedIndices" /> collection changes.</summary>
		// Token: 0x1400021F RID: 543
		// (add) Token: 0x06002D4C RID: 11596 RVA: 0x000CAACD File Offset: 0x000C8CCD
		// (remove) Token: 0x06002D4D RID: 11597 RVA: 0x000CAAE0 File Offset: 0x000C8CE0
		[SRCategory("CatBehavior")]
		[SRDescription("ListViewSelectedIndexChangedDescr")]
		public event EventHandler SelectedIndexChanged
		{
			add
			{
				base.Events.AddHandler(ListView.EVENT_SELECTEDINDEXCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EVENT_SELECTEDINDEXCHANGED, value);
			}
		}

		/// <summary>Occurs when a <see cref="T:System.Windows.Forms.ListView" /> is in virtual mode and the selection state of a range of items has changed.</summary>
		// Token: 0x14000220 RID: 544
		// (add) Token: 0x06002D4E RID: 11598 RVA: 0x000CAAF3 File Offset: 0x000C8CF3
		// (remove) Token: 0x06002D4F RID: 11599 RVA: 0x000CAB06 File Offset: 0x000C8D06
		[SRCategory("CatBehavior")]
		[SRDescription("ListViewVirtualItemsSelectionRangeChangedDescr")]
		public event ListViewVirtualItemsSelectionRangeChangedEventHandler VirtualItemsSelectionRangeChanged
		{
			add
			{
				base.Events.AddHandler(ListView.EVENT_VIRTUALITEMSSELECTIONRANGECHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EVENT_VIRTUALITEMSSELECTIONRANGECHANGED, value);
			}
		}

		// Token: 0x06002D50 RID: 11600 RVA: 0x000CAB1C File Offset: 0x000C8D1C
		private void ApplyUpdateCachedItems()
		{
			ArrayList arrayList = (ArrayList)base.Properties.GetObject(ListView.PropDelayedUpdateItems);
			if (arrayList != null)
			{
				base.Properties.SetObject(ListView.PropDelayedUpdateItems, null);
				ListViewItem[] array = (ListViewItem[])arrayList.ToArray(typeof(ListViewItem));
				if (array.Length != 0)
				{
					this.InsertItems(this.itemCount, array, false);
				}
			}
		}

		/// <summary>Arranges items in the control when they are displayed as icons with a specified alignment setting.</summary>
		/// <param name="value">One of the <see cref="T:System.Windows.Forms.ListViewAlignment" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The value specified in the <paramref name="value" /> parameter is not a member of the <see cref="T:System.Windows.Forms.ListViewAlignment" /> enumeration.</exception>
		// Token: 0x06002D51 RID: 11601 RVA: 0x000CAB7C File Offset: 0x000C8D7C
		public void ArrangeIcons(ListViewAlignment value)
		{
			if (this.viewStyle != View.SmallIcon)
			{
				return;
			}
			int num = (int)value;
			if (num <= 2 || num == 5)
			{
				if (base.IsHandleCreated)
				{
					UnsafeNativeMethods.PostMessage(new HandleRef(this, base.Handle), 4118, (int)value, 0);
				}
				if (!this.VirtualMode && this.sorting != SortOrder.None)
				{
					this.Sort();
				}
				return;
			}
			throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
			{
				"value",
				value.ToString()
			}));
		}

		/// <summary>Arranges items in the control when they are displayed as icons based on the value of the <see cref="P:System.Windows.Forms.ListView.Alignment" /> property.</summary>
		// Token: 0x06002D52 RID: 11602 RVA: 0x000CAC04 File Offset: 0x000C8E04
		public void ArrangeIcons()
		{
			this.ArrangeIcons(ListViewAlignment.Default);
		}

		/// <summary>Resizes the width of the columns as indicated by the resize style.</summary>
		/// <param name="headerAutoResize">One of the <see cref="T:System.Windows.Forms.ColumnHeaderAutoResizeStyle" /> values.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Windows.Forms.ListView.AutoResizeColumn(System.Int32,System.Windows.Forms.ColumnHeaderAutoResizeStyle)" /> is called with a value other than <see cref="F:System.Windows.Forms.ColumnHeaderAutoResizeStyle.None" /> when <see cref="P:System.Windows.Forms.ListView.View" /> is not set to <see cref="F:System.Windows.Forms.View.Details" />.</exception>
		// Token: 0x06002D53 RID: 11603 RVA: 0x000CAC0D File Offset: 0x000C8E0D
		public void AutoResizeColumns(ColumnHeaderAutoResizeStyle headerAutoResize)
		{
			if (!base.IsHandleCreated)
			{
				this.CreateHandle();
			}
			this.UpdateColumnWidths(headerAutoResize);
		}

		/// <summary>Resizes the width of the given column as indicated by the resize style.</summary>
		/// <param name="columnIndex">The zero-based index of the column to resize.</param>
		/// <param name="headerAutoResize">One of the <see cref="T:System.Windows.Forms.ColumnHeaderAutoResizeStyle" /> values.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="columnIndex" /> is greater than 0 when <see cref="P:System.Windows.Forms.ListView.Columns" /> is <see langword="null" />  
		/// -or-  
		/// <paramref name="columnIndex" /> is less than 0 or greater than the number of columns set.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="headerAutoResize" /> is not a member of the <see cref="T:System.Windows.Forms.ColumnHeaderAutoResizeStyle" /> enumeration.</exception>
		// Token: 0x06002D54 RID: 11604 RVA: 0x000CAC24 File Offset: 0x000C8E24
		public void AutoResizeColumn(int columnIndex, ColumnHeaderAutoResizeStyle headerAutoResize)
		{
			if (!base.IsHandleCreated)
			{
				this.CreateHandle();
			}
			this.SetColumnWidth(columnIndex, headerAutoResize);
		}

		/// <summary>Prevents the control from drawing until the <see cref="M:System.Windows.Forms.ListView.EndUpdate" /> method is called.</summary>
		// Token: 0x06002D55 RID: 11605 RVA: 0x000CAC3C File Offset: 0x000C8E3C
		public void BeginUpdate()
		{
			base.BeginUpdateInternal();
			int num = this.updateCounter;
			this.updateCounter = num + 1;
			if (num == 0 && base.Properties.GetObject(ListView.PropDelayedUpdateItems) == null)
			{
				base.Properties.SetObject(ListView.PropDelayedUpdateItems, new ArrayList());
			}
		}

		// Token: 0x06002D56 RID: 11606 RVA: 0x000CAC8C File Offset: 0x000C8E8C
		internal void CacheSelectedStateForItem(ListViewItem lvi, bool selected)
		{
			if (selected)
			{
				if (this.savedSelectedItems == null)
				{
					this.savedSelectedItems = new List<ListViewItem>();
				}
				if (!this.savedSelectedItems.Contains(lvi))
				{
					this.savedSelectedItems.Add(lvi);
					return;
				}
			}
			else if (this.savedSelectedItems != null && this.savedSelectedItems.Contains(lvi))
			{
				this.savedSelectedItems.Remove(lvi);
			}
		}

		// Token: 0x06002D57 RID: 11607 RVA: 0x000CACF0 File Offset: 0x000C8EF0
		private void CleanPreviousBackgroundImageFiles()
		{
			if (this.bkImgFileNames == null)
			{
				return;
			}
			FileIOPermission fileIOPermission = new FileIOPermission(PermissionState.Unrestricted);
			fileIOPermission.Assert();
			try
			{
				for (int i = 0; i <= this.bkImgFileNamesCount; i++)
				{
					FileInfo fileInfo = new FileInfo(this.bkImgFileNames[i]);
					if (fileInfo.Exists)
					{
						try
						{
							fileInfo.Delete();
						}
						catch (IOException)
						{
						}
					}
				}
			}
			finally
			{
				PermissionSet.RevertAssert();
			}
			this.bkImgFileNames = null;
			this.bkImgFileNamesCount = -1;
		}

		/// <summary>Removes all items and columns from the control.</summary>
		// Token: 0x06002D58 RID: 11608 RVA: 0x000CAD78 File Offset: 0x000C8F78
		public void Clear()
		{
			this.Items.Clear();
			this.Columns.Clear();
		}

		// Token: 0x06002D59 RID: 11609 RVA: 0x000CAD90 File Offset: 0x000C8F90
		private int CompareFunc(IntPtr lparam1, IntPtr lparam2, IntPtr lparamSort)
		{
			if (this.listItemSorter != null)
			{
				return this.listItemSorter.Compare(this.listItemsTable[(int)lparam1], this.listItemsTable[(int)lparam2]);
			}
			return 0;
		}

		// Token: 0x06002D5A RID: 11610 RVA: 0x000CADE0 File Offset: 0x000C8FE0
		private int CompensateColumnHeaderResize(Message m, bool columnResizeCancelled)
		{
			if (this.ComctlSupportsVisualStyles && this.View == View.Details && !columnResizeCancelled && this.Items.Count > 0)
			{
				NativeMethods.NMHEADER nmheader = (NativeMethods.NMHEADER)m.GetLParam(typeof(NativeMethods.NMHEADER));
				return this.CompensateColumnHeaderResize(nmheader.iItem, columnResizeCancelled);
			}
			return 0;
		}

		// Token: 0x06002D5B RID: 11611 RVA: 0x000CAE38 File Offset: 0x000C9038
		private int CompensateColumnHeaderResize(int columnIndex, bool columnResizeCancelled)
		{
			if (this.ComctlSupportsVisualStyles && this.View == View.Details && !columnResizeCancelled && this.Items.Count > 0 && columnIndex == 0)
			{
				ColumnHeader columnHeader = ((this.columnHeaders != null && this.columnHeaders.Length != 0) ? this.columnHeaders[0] : null);
				if (columnHeader != null)
				{
					if (this.SmallImageList == null)
					{
						return 2;
					}
					bool flag = true;
					for (int i = 0; i < this.Items.Count; i++)
					{
						if (this.Items[i].ImageIndexer.ActualIndex > -1)
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						return 18;
					}
				}
			}
			return 0;
		}

		/// <summary>Creates a handle for the control.</summary>
		// Token: 0x06002D5C RID: 11612 RVA: 0x000CAED4 File Offset: 0x000C90D4
		protected override void CreateHandle()
		{
			if (!base.RecreatingHandle)
			{
				IntPtr intPtr = UnsafeNativeMethods.ThemingScope.Activate();
				try
				{
					SafeNativeMethods.InitCommonControlsEx(new NativeMethods.INITCOMMONCONTROLSEX
					{
						dwICC = 1
					});
				}
				finally
				{
					UnsafeNativeMethods.ThemingScope.Deactivate(intPtr);
				}
			}
			base.CreateHandle();
			if (this.BackgroundImage != null)
			{
				this.SetBackgroundImage();
			}
		}

		// Token: 0x06002D5D RID: 11613 RVA: 0x000CAF34 File Offset: 0x000C9134
		private unsafe void CustomDraw(ref Message m)
		{
			bool flag = false;
			bool flag2 = false;
			try
			{
				NativeMethods.NMLVCUSTOMDRAW* ptr = (NativeMethods.NMLVCUSTOMDRAW*)(void*)m.LParam;
				int dwDrawStage = ptr->nmcd.dwDrawStage;
				if (dwDrawStage != 1)
				{
					int num;
					Rectangle rectangle;
					if (dwDrawStage != 65537)
					{
						if (dwDrawStage != 196609)
						{
							m.Result = (IntPtr)0;
							return;
						}
					}
					else
					{
						num = (int)ptr->nmcd.dwItemSpec;
						rectangle = this.GetItemRectOrEmpty(num);
						if (!base.ClientRectangle.IntersectsWith(rectangle))
						{
							return;
						}
						if (this.OwnerDraw)
						{
							Graphics graphics = Graphics.FromHdcInternal(ptr->nmcd.hdc);
							DrawListViewItemEventArgs drawListViewItemEventArgs = null;
							try
							{
								drawListViewItemEventArgs = new DrawListViewItemEventArgs(graphics, this.Items[(int)ptr->nmcd.dwItemSpec], rectangle, (int)ptr->nmcd.dwItemSpec, (ListViewItemStates)ptr->nmcd.uItemState);
								this.OnDrawItem(drawListViewItemEventArgs);
							}
							finally
							{
								graphics.Dispose();
							}
							flag2 = drawListViewItemEventArgs.DrawDefault;
							if (this.viewStyle == View.Details)
							{
								m.Result = (IntPtr)32;
							}
							else if (!drawListViewItemEventArgs.DrawDefault)
							{
								m.Result = (IntPtr)4;
							}
							if (!drawListViewItemEventArgs.DrawDefault)
							{
								return;
							}
						}
						if (this.viewStyle == View.Details || this.viewStyle == View.Tile)
						{
							m.Result = (IntPtr)34;
							flag = true;
						}
					}
					num = (int)ptr->nmcd.dwItemSpec;
					rectangle = this.GetItemRectOrEmpty(num);
					if (base.ClientRectangle.IntersectsWith(rectangle))
					{
						if (this.OwnerDraw && !flag2)
						{
							Graphics graphics2 = Graphics.FromHdcInternal(ptr->nmcd.hdc);
							bool flag3 = true;
							try
							{
								if (ptr->iSubItem < this.Items[num].SubItems.Count)
								{
									Rectangle subItemRect = this.GetSubItemRect(num, ptr->iSubItem);
									if (ptr->iSubItem == 0 && this.Items[num].SubItems.Count > 1)
									{
										subItemRect.Width = this.columnHeaders[0].Width;
									}
									if (base.ClientRectangle.IntersectsWith(subItemRect))
									{
										DrawListViewSubItemEventArgs drawListViewSubItemEventArgs = new DrawListViewSubItemEventArgs(graphics2, subItemRect, this.Items[num], this.Items[num].SubItems[ptr->iSubItem], num, ptr->iSubItem, this.columnHeaders[ptr->iSubItem], (ListViewItemStates)ptr->nmcd.uItemState);
										this.OnDrawSubItem(drawListViewSubItemEventArgs);
										flag3 = !drawListViewSubItemEventArgs.DrawDefault;
									}
								}
							}
							finally
							{
								graphics2.Dispose();
							}
							if (flag3)
							{
								m.Result = (IntPtr)4;
								return;
							}
						}
						ListViewItem listViewItem = this.Items[(int)ptr->nmcd.dwItemSpec];
						if (flag && listViewItem.UseItemStyleForSubItems)
						{
							m.Result = (IntPtr)2;
						}
						int num2 = ptr->nmcd.uItemState;
						if (!this.HideSelection)
						{
							int itemState = this.GetItemState((int)ptr->nmcd.dwItemSpec);
							if ((itemState & 2) == 0)
							{
								num2 &= -2;
							}
						}
						int num3 = (((ptr->nmcd.dwDrawStage & 131072) != 0) ? ptr->iSubItem : 0);
						Font font = null;
						Color color = Color.Empty;
						Color color2 = Color.Empty;
						bool flag4 = false;
						bool flag5 = false;
						if (listViewItem != null && num3 < listViewItem.SubItems.Count)
						{
							flag4 = true;
							if (num3 == 0 && (num2 & 64) != 0 && this.HotTracking)
							{
								flag5 = true;
								font = new Font(listViewItem.SubItems[0].Font, FontStyle.Underline);
							}
							else
							{
								font = listViewItem.SubItems[num3].Font;
							}
							if (num3 > 0 || (num2 & 71) == 0)
							{
								color = listViewItem.SubItems[num3].ForeColor;
								color2 = listViewItem.SubItems[num3].BackColor;
							}
						}
						Color color3 = Color.Empty;
						Color color4 = Color.Empty;
						if (flag4)
						{
							color3 = color;
							color4 = color2;
						}
						bool flag6 = true;
						if (!base.Enabled)
						{
							flag6 = false;
						}
						else if ((this.activation == ItemActivation.OneClick || this.activation == ItemActivation.TwoClick) && (num2 & 71) != 0)
						{
							flag6 = false;
						}
						if (flag6)
						{
							if (!flag4 || color3.IsEmpty)
							{
								ptr->clrText = ColorTranslator.ToWin32(this.odCacheForeColor);
							}
							else
							{
								ptr->clrText = ColorTranslator.ToWin32(color3);
							}
							if (ptr->clrText == ColorTranslator.ToWin32(SystemColors.HotTrack))
							{
								int num4 = 0;
								bool flag7 = false;
								int num5 = 16711680;
								do
								{
									int num6 = ptr->clrText & num5;
									if (num6 != 0 || num5 == 255)
									{
										int num7 = 16 - num4;
										if (num6 == num5)
										{
											num6 = (num6 >> num7) - 1 << num7;
										}
										else
										{
											num6 = (num6 >> num7) + 1 << num7;
										}
										ptr->clrText = (ptr->clrText & ~num5) | num6;
										flag7 = true;
									}
									else
									{
										num5 >>= 8;
										num4 += 8;
									}
								}
								while (!flag7);
							}
							if (!flag4 || color4.IsEmpty)
							{
								ptr->clrTextBk = ColorTranslator.ToWin32(this.odCacheBackColor);
							}
							else
							{
								ptr->clrTextBk = ColorTranslator.ToWin32(color4);
							}
						}
						if (!flag4 || font == null)
						{
							if (this.odCacheFont != null)
							{
								SafeNativeMethods.SelectObject(new HandleRef(ptr->nmcd, ptr->nmcd.hdc), new HandleRef(null, this.odCacheFontHandle));
							}
						}
						else
						{
							if (this.odCacheFontHandleWrapper != null)
							{
								this.odCacheFontHandleWrapper.Dispose();
							}
							this.odCacheFontHandleWrapper = new Control.FontHandleWrapper(font);
							SafeNativeMethods.SelectObject(new HandleRef(ptr->nmcd, ptr->nmcd.hdc), new HandleRef(this.odCacheFontHandleWrapper, this.odCacheFontHandleWrapper.Handle));
						}
						if (!flag)
						{
							m.Result = (IntPtr)2;
						}
						if (flag5)
						{
							font.Dispose();
						}
					}
				}
				else if (this.OwnerDraw)
				{
					m.Result = (IntPtr)32;
				}
				else
				{
					m.Result = (IntPtr)34;
					this.odCacheBackColor = this.BackColor;
					this.odCacheForeColor = this.ForeColor;
					this.odCacheFont = this.Font;
					this.odCacheFontHandle = base.FontHandle;
					if (ptr->dwItemType == 1)
					{
						if (this.odCacheFontHandleWrapper != null)
						{
							this.odCacheFontHandleWrapper.Dispose();
						}
						this.odCacheFont = new Font(this.odCacheFont, FontStyle.Bold);
						this.odCacheFontHandleWrapper = new Control.FontHandleWrapper(this.odCacheFont);
						this.odCacheFontHandle = this.odCacheFontHandleWrapper.Handle;
						SafeNativeMethods.SelectObject(new HandleRef(ptr->nmcd, ptr->nmcd.hdc), new HandleRef(this.odCacheFontHandleWrapper, this.odCacheFontHandleWrapper.Handle));
						m.Result = (IntPtr)2;
					}
				}
			}
			catch (Exception ex)
			{
				m.Result = (IntPtr)0;
			}
		}

		// Token: 0x06002D5E RID: 11614 RVA: 0x000CB66C File Offset: 0x000C986C
		private void DeleteFileName(string fileName)
		{
			if (!string.IsNullOrEmpty(fileName))
			{
				FileIOPermission fileIOPermission = new FileIOPermission(PermissionState.Unrestricted);
				fileIOPermission.Assert();
				try
				{
					FileInfo fileInfo = new FileInfo(fileName);
					if (fileInfo.Exists)
					{
						try
						{
							fileInfo.Delete();
						}
						catch (IOException)
						{
						}
					}
				}
				finally
				{
					PermissionSet.RevertAssert();
				}
			}
		}

		// Token: 0x06002D5F RID: 11615 RVA: 0x000CB6CC File Offset: 0x000C98CC
		private void DestroyLVGROUP(NativeMethods.LVGROUP lvgroup)
		{
			if (lvgroup.pszHeader != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(lvgroup.pszHeader);
			}
		}

		// Token: 0x06002D60 RID: 11616 RVA: 0x000CB6EC File Offset: 0x000C98EC
		private void DetachImageList(object sender, EventArgs e)
		{
			this.listViewState1[4] = true;
			try
			{
				if (sender == this.imageListSmall)
				{
					this.SmallImageList = null;
				}
				if (sender == this.imageListLarge)
				{
					this.LargeImageList = null;
				}
				if (sender == this.imageListState)
				{
					this.StateImageList = null;
				}
			}
			finally
			{
				this.listViewState1[4] = false;
			}
			this.UpdateListViewItemsLocations();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ListView" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06002D61 RID: 11617 RVA: 0x000CB75C File Offset: 0x000C995C
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.imageListSmall != null)
				{
					this.imageListSmall.Disposed -= this.DetachImageList;
					this.imageListSmall = null;
				}
				if (this.imageListLarge != null)
				{
					this.imageListLarge.Disposed -= this.DetachImageList;
					this.imageListLarge = null;
				}
				if (this.imageListState != null)
				{
					this.imageListState.Disposed -= this.DetachImageList;
					this.imageListState = null;
				}
				if (this.columnHeaders != null)
				{
					for (int i = this.columnHeaders.Length - 1; i >= 0; i--)
					{
						this.columnHeaders[i].OwnerListview = null;
						this.columnHeaders[i].Dispose();
					}
					this.columnHeaders = null;
				}
				this.Items.Clear();
				if (this.odCacheFontHandleWrapper != null)
				{
					this.odCacheFontHandleWrapper.Dispose();
					this.odCacheFontHandleWrapper = null;
				}
				if (!string.IsNullOrEmpty(this.backgroundImageFileName) || this.bkImgFileNames != null)
				{
					FileIOPermission fileIOPermission = new FileIOPermission(PermissionState.Unrestricted);
					fileIOPermission.Assert();
					try
					{
						if (!string.IsNullOrEmpty(this.backgroundImageFileName))
						{
							FileInfo fileInfo = new FileInfo(this.backgroundImageFileName);
							try
							{
								fileInfo.Delete();
							}
							catch (IOException)
							{
							}
							this.backgroundImageFileName = string.Empty;
						}
						for (int j = 0; j <= this.bkImgFileNamesCount; j++)
						{
							FileInfo fileInfo = new FileInfo(this.bkImgFileNames[j]);
							try
							{
								fileInfo.Delete();
							}
							catch (IOException)
							{
							}
						}
						this.bkImgFileNames = null;
						this.bkImgFileNamesCount = -1;
					}
					finally
					{
						PermissionSet.RevertAssert();
					}
				}
			}
			base.Dispose(disposing);
		}

		/// <summary>Resumes drawing of the list view control after drawing is suspended by the <see cref="M:System.Windows.Forms.ListView.BeginUpdate" /> method.</summary>
		// Token: 0x06002D62 RID: 11618 RVA: 0x000CB908 File Offset: 0x000C9B08
		public void EndUpdate()
		{
			int num = this.updateCounter - 1;
			this.updateCounter = num;
			if (num == 0 && base.Properties.GetObject(ListView.PropDelayedUpdateItems) != null)
			{
				this.ApplyUpdateCachedItems();
			}
			base.EndUpdateInternal();
		}

		// Token: 0x06002D63 RID: 11619 RVA: 0x000CB948 File Offset: 0x000C9B48
		private void EnsureDefaultGroup()
		{
			if (base.IsHandleCreated && this.ComctlSupportsVisualStyles && this.GroupsEnabled && base.SendMessage(4257, this.DefaultGroup.ID, 0) == IntPtr.Zero)
			{
				this.UpdateGroupView();
				this.InsertGroupNative(0, this.DefaultGroup);
			}
		}

		/// <summary>Ensures that the specified item is visible within the control, scrolling the contents of the control if necessary.</summary>
		/// <param name="index">The zero-based index of the item to scroll into view.</param>
		// Token: 0x06002D64 RID: 11620 RVA: 0x000CB9A4 File Offset: 0x000C9BA4
		public void EnsureVisible(int index)
		{
			if (index < 0 || index >= this.Items.Count)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (base.IsHandleCreated)
			{
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4115, index, 0);
			}
		}

		/// <summary>Finds the first <see cref="T:System.Windows.Forms.ListViewItem" /> that begins with the specified text value.</summary>
		/// <param name="text">The text to search for.</param>
		/// <returns>The first <see cref="T:System.Windows.Forms.ListViewItem" /> that begins with the specified text value.</returns>
		// Token: 0x06002D65 RID: 11621 RVA: 0x000CBA16 File Offset: 0x000C9C16
		public ListViewItem FindItemWithText(string text)
		{
			if (this.Items.Count == 0)
			{
				return null;
			}
			return this.FindItemWithText(text, true, 0, true);
		}

		/// <summary>Finds the first <see cref="T:System.Windows.Forms.ListViewItem" /> or <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />, if indicated, that begins with the specified text value. The search starts at the specified index.</summary>
		/// <param name="text">The text to search for.</param>
		/// <param name="includeSubItemsInSearch">
		///   <see langword="true" /> to include subitems in the search; otherwise, <see langword="false" />.</param>
		/// <param name="startIndex">The index of the item at which to start the search.</param>
		/// <returns>The first <see cref="T:System.Windows.Forms.ListViewItem" /> that begins with the specified text value.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is less 0 or more than the number items in the <see cref="T:System.Windows.Forms.ListView" />.</exception>
		// Token: 0x06002D66 RID: 11622 RVA: 0x000CBA31 File Offset: 0x000C9C31
		public ListViewItem FindItemWithText(string text, bool includeSubItemsInSearch, int startIndex)
		{
			return this.FindItemWithText(text, includeSubItemsInSearch, startIndex, true);
		}

		/// <summary>Finds the first <see cref="T:System.Windows.Forms.ListViewItem" /> or <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />, if indicated, that begins with the specified text value. The search starts at the specified index.</summary>
		/// <param name="text">The text to search for.</param>
		/// <param name="includeSubItemsInSearch">
		///   <see langword="true" /> to include subitems in the search; otherwise, <see langword="false" />.</param>
		/// <param name="startIndex">The index of the item at which to start the search.</param>
		/// <param name="isPrefixSearch">
		///   <see langword="true" /> to allow partial matches; otherwise, <see langword="false" />.</param>
		/// <returns>The first <see cref="T:System.Windows.Forms.ListViewItem" /> that begins with the specified text value.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is less than 0 or more than the number of items in the <see cref="T:System.Windows.Forms.ListView" />.</exception>
		// Token: 0x06002D67 RID: 11623 RVA: 0x000CBA40 File Offset: 0x000C9C40
		public ListViewItem FindItemWithText(string text, bool includeSubItemsInSearch, int startIndex, bool isPrefixSearch)
		{
			if (startIndex < 0 || startIndex >= this.Items.Count)
			{
				throw new ArgumentOutOfRangeException("startIndex", SR.GetString("InvalidArgument", new object[]
				{
					"startIndex",
					startIndex.ToString(CultureInfo.CurrentCulture)
				}));
			}
			return this.FindItem(true, text, isPrefixSearch, new Point(0, 0), SearchDirectionHint.Down, startIndex, includeSubItemsInSearch);
		}

		/// <summary>Finds the next item from the given point, searching in the specified direction</summary>
		/// <param name="dir">One of the <see cref="T:System.Windows.Forms.SearchDirectionHint" /> values.</param>
		/// <param name="point">The point at which to begin searching.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> that is closest to the given point, searching in the specified direction.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.ListView.View" /> is set to a value other than <see cref="F:System.Windows.Forms.View.SmallIcon" /> or <see cref="F:System.Windows.Forms.View.LargeIcon" />.</exception>
		// Token: 0x06002D68 RID: 11624 RVA: 0x000CBAA6 File Offset: 0x000C9CA6
		public ListViewItem FindNearestItem(SearchDirectionHint dir, Point point)
		{
			return this.FindNearestItem(dir, point.X, point.Y);
		}

		/// <summary>Finds the next item from the given x- and y-coordinates, searching in the specified direction.</summary>
		/// <param name="searchDirection">One of the <see cref="T:System.Windows.Forms.SearchDirectionHint" /> values.</param>
		/// <param name="x">The x-coordinate for the point at which to begin searching.</param>
		/// <param name="y">The y-coordinate for the point at which to begin searching.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> that is closest to the given coordinates, searching in the specified direction.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.ListView.View" /> is set to a value other than <see cref="F:System.Windows.Forms.View.SmallIcon" /> or <see cref="F:System.Windows.Forms.View.LargeIcon" />.</exception>
		// Token: 0x06002D69 RID: 11625 RVA: 0x000CBAC0 File Offset: 0x000C9CC0
		public ListViewItem FindNearestItem(SearchDirectionHint searchDirection, int x, int y)
		{
			if (this.View != View.SmallIcon && this.View != View.LargeIcon)
			{
				throw new InvalidOperationException(SR.GetString("ListViewFindNearestItemWorksOnlyInIconView"));
			}
			if (searchDirection < SearchDirectionHint.Left || searchDirection > SearchDirectionHint.Down)
			{
				throw new ArgumentOutOfRangeException("searchDirection", SR.GetString("InvalidArgument", new object[]
				{
					"searchDirection",
					searchDirection.ToString()
				}));
			}
			ListViewItem itemAt = this.GetItemAt(x, y);
			if (itemAt != null)
			{
				Rectangle bounds = itemAt.Bounds;
				Rectangle itemRect = this.GetItemRect(itemAt.Index, ItemBoundsPortion.Icon);
				switch (searchDirection)
				{
				case SearchDirectionHint.Left:
					x = Math.Max(bounds.Left, itemRect.Left) - 1;
					break;
				case SearchDirectionHint.Up:
					y = Math.Max(bounds.Top, itemRect.Top) - 1;
					break;
				case SearchDirectionHint.Right:
					x = Math.Max(bounds.Left, itemRect.Left) + 1;
					break;
				case SearchDirectionHint.Down:
					y = Math.Max(bounds.Top, itemRect.Top) + 1;
					break;
				}
			}
			return this.FindItem(false, string.Empty, false, new Point(x, y), searchDirection, -1, false);
		}

		// Token: 0x06002D6A RID: 11626 RVA: 0x000CBBE4 File Offset: 0x000C9DE4
		private ListViewItem FindItem(bool isTextSearch, string text, bool isPrefixSearch, Point pt, SearchDirectionHint dir, int startIndex, bool includeSubItemsInSearch)
		{
			if (this.Items.Count == 0)
			{
				return null;
			}
			if (!base.IsHandleCreated)
			{
				this.CreateHandle();
			}
			if (this.VirtualMode)
			{
				SearchForVirtualItemEventArgs searchForVirtualItemEventArgs = new SearchForVirtualItemEventArgs(isTextSearch, isPrefixSearch, includeSubItemsInSearch, text, pt, dir, startIndex);
				this.OnSearchForVirtualItem(searchForVirtualItemEventArgs);
				if (searchForVirtualItemEventArgs.Index != -1)
				{
					return this.Items[searchForVirtualItemEventArgs.Index];
				}
				return null;
			}
			else
			{
				NativeMethods.LVFINDINFO lvfindinfo = default(NativeMethods.LVFINDINFO);
				if (isTextSearch)
				{
					lvfindinfo.flags = 2;
					lvfindinfo.flags |= (isPrefixSearch ? 8 : 0);
					lvfindinfo.psz = text;
				}
				else
				{
					lvfindinfo.flags = 64;
					lvfindinfo.ptX = pt.X;
					lvfindinfo.ptY = pt.Y;
					lvfindinfo.vkDirection = (int)dir;
				}
				lvfindinfo.lParam = IntPtr.Zero;
				int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.LVM_FINDITEM, startIndex - 1, ref lvfindinfo);
				if (num >= 0)
				{
					return this.Items[num];
				}
				if (isTextSearch && includeSubItemsInSearch)
				{
					for (int i = startIndex; i < this.Items.Count; i++)
					{
						ListViewItem listViewItem = this.Items[i];
						for (int j = 0; j < listViewItem.SubItems.Count; j++)
						{
							ListViewItem.ListViewSubItem listViewSubItem = listViewItem.SubItems[j];
							if (string.Equals(text, listViewSubItem.Text, StringComparison.OrdinalIgnoreCase))
							{
								return listViewItem;
							}
							if (isPrefixSearch && CultureInfo.CurrentCulture.CompareInfo.IsPrefix(listViewSubItem.Text, text, CompareOptions.IgnoreCase))
							{
								return listViewItem;
							}
						}
					}
					return null;
				}
				return null;
			}
		}

		// Token: 0x06002D6B RID: 11627 RVA: 0x000CBD78 File Offset: 0x000C9F78
		private void ForceCheckBoxUpdate()
		{
			if (this.CheckBoxes && base.IsHandleCreated)
			{
				base.SendMessage(4150, 4, 0);
				base.SendMessage(4150, 4, 4);
				if (this.AutoArrange)
				{
					this.ArrangeIcons(this.Alignment);
				}
			}
		}

		// Token: 0x06002D6C RID: 11628 RVA: 0x000CBDC8 File Offset: 0x000C9FC8
		private string GenerateRandomName()
		{
			Bitmap bitmap = new Bitmap(this.BackgroundImage);
			int num = 0;
			try
			{
				num = (int)(long)bitmap.GetHicon();
			}
			catch
			{
				bitmap.Dispose();
			}
			Random random;
			if (num == 0)
			{
				random = new Random((int)DateTime.Now.Ticks);
			}
			else
			{
				random = new Random(num);
			}
			return random.Next().ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06002D6D RID: 11629 RVA: 0x000CBE40 File Offset: 0x000CA040
		private int GenerateUniqueID()
		{
			int num = this.nextID;
			this.nextID = num + 1;
			int num2 = num;
			if (num2 == -1)
			{
				num2 = 0;
				this.nextID = 1;
			}
			return num2;
		}

		// Token: 0x06002D6E RID: 11630 RVA: 0x000CBE70 File Offset: 0x000CA070
		internal int GetDisplayIndex(ListViewItem item, int lastIndex)
		{
			this.ApplyUpdateCachedItems();
			if (base.IsHandleCreated && !this.ListViewHandleDestroyed)
			{
				NativeMethods.LVFINDINFO lvfindinfo = default(NativeMethods.LVFINDINFO);
				lvfindinfo.lParam = (IntPtr)item.ID;
				lvfindinfo.flags = 1;
				int num = -1;
				if (lastIndex != -1)
				{
					num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.LVM_FINDITEM, lastIndex - 1, ref lvfindinfo);
				}
				if (num == -1)
				{
					num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.LVM_FINDITEM, -1, ref lvfindinfo);
				}
				return num;
			}
			int num2 = 0;
			foreach (object obj in this.listItemsArray)
			{
				if (obj == item)
				{
					return num2;
				}
				num2++;
			}
			return -1;
		}

		// Token: 0x06002D6F RID: 11631 RVA: 0x000CBF5C File Offset: 0x000CA15C
		internal int GetColumnIndex(ColumnHeader ch)
		{
			if (this.columnHeaders == null)
			{
				return -1;
			}
			for (int i = 0; i < this.columnHeaders.Length; i++)
			{
				if (this.columnHeaders[i] == ch)
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Retrieves the item at the specified location.</summary>
		/// <param name="x">The x-coordinate of the location to search for an item (expressed in client coordinates).</param>
		/// <param name="y">The y-coordinate of the location to search for an item (expressed in client coordinates).</param>
		/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the item at the specified position. If there is no item at the specified location, the method returns <see langword="null" />.</returns>
		// Token: 0x06002D70 RID: 11632 RVA: 0x000CBF94 File Offset: 0x000CA194
		public ListViewItem GetItemAt(int x, int y)
		{
			NativeMethods.LVHITTESTINFO lvhittestinfo = new NativeMethods.LVHITTESTINFO();
			lvhittestinfo.pt_x = x;
			lvhittestinfo.pt_y = y;
			int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4114, 0, lvhittestinfo);
			ListViewItem listViewItem = null;
			if (num >= 0 && (lvhittestinfo.flags & 14) != 0)
			{
				listViewItem = this.Items[num];
			}
			return listViewItem;
		}

		// Token: 0x06002D71 RID: 11633 RVA: 0x000CBFF2 File Offset: 0x000CA1F2
		internal int GetNativeGroupId(ListViewItem item)
		{
			item.UpdateGroupFromName();
			if (item.Group != null && this.Groups.Contains(item.Group))
			{
				return item.Group.ID;
			}
			this.EnsureDefaultGroup();
			return this.DefaultGroup.ID;
		}

		// Token: 0x06002D72 RID: 11634 RVA: 0x000CC034 File Offset: 0x000CA234
		internal void GetSubItemAt(int x, int y, out int iItem, out int iSubItem)
		{
			NativeMethods.LVHITTESTINFO lvhittestinfo = new NativeMethods.LVHITTESTINFO();
			lvhittestinfo.pt_x = x;
			lvhittestinfo.pt_y = y;
			int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4153, 0, lvhittestinfo);
			if (num > -1)
			{
				iItem = lvhittestinfo.iItem;
				iSubItem = lvhittestinfo.iSubItem;
				return;
			}
			iItem = -1;
			iSubItem = -1;
		}

		// Token: 0x06002D73 RID: 11635 RVA: 0x000CC090 File Offset: 0x000CA290
		internal Point GetItemPosition(int index)
		{
			NativeMethods.POINT point = new NativeMethods.POINT();
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4112, index, point);
			return new Point(point.x, point.y);
		}

		// Token: 0x06002D74 RID: 11636 RVA: 0x000CC0CD File Offset: 0x000CA2CD
		internal int GetItemState(int index)
		{
			return this.GetItemState(index, 65295);
		}

		// Token: 0x06002D75 RID: 11637 RVA: 0x000CC0DC File Offset: 0x000CA2DC
		internal int GetItemState(int index, int mask)
		{
			if (index < 0 || (this.VirtualMode && index >= this.VirtualListSize) || (!this.VirtualMode && index >= this.itemCount))
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			return (int)(long)base.SendMessage(4140, index, mask);
		}

		/// <summary>Retrieves the bounding rectangle for a specific item within the list view control.</summary>
		/// <param name="index">The zero-based index of the item within the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> whose bounding rectangle you want to return.</param>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounding rectangle of the specified <see cref="T:System.Windows.Forms.ListViewItem" />.</returns>
		// Token: 0x06002D76 RID: 11638 RVA: 0x000CC154 File Offset: 0x000CA354
		public Rectangle GetItemRect(int index)
		{
			return this.GetItemRect(index, ItemBoundsPortion.Entire);
		}

		/// <summary>Retrieves the specified portion of the bounding rectangle for a specific item within the list view control.</summary>
		/// <param name="index">The zero-based index of the item within the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> whose bounding rectangle you want to return.</param>
		/// <param name="portion">One of the <see cref="T:System.Windows.Forms.ItemBoundsPortion" /> values that represents a portion of the <see cref="T:System.Windows.Forms.ListViewItem" /> for which to retrieve the bounding rectangle.</param>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounding rectangle for the specified portion of the specified <see cref="T:System.Windows.Forms.ListViewItem" />.</returns>
		// Token: 0x06002D77 RID: 11639 RVA: 0x000CC160 File Offset: 0x000CA360
		public Rectangle GetItemRect(int index, ItemBoundsPortion portion)
		{
			if (index < 0 || index >= this.Items.Count)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (!ClientUtils.IsEnumValid(portion, (int)portion, 0, 3))
			{
				throw new InvalidEnumArgumentException("portion", (int)portion, typeof(ItemBoundsPortion));
			}
			if (this.View == View.Details && this.Columns.Count == 0)
			{
				return Rectangle.Empty;
			}
			NativeMethods.RECT rect = default(NativeMethods.RECT);
			rect.left = (int)portion;
			if ((int)(long)base.SendMessage(4110, index, ref rect) == 0)
			{
				throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			return Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
		}

		// Token: 0x06002D78 RID: 11640 RVA: 0x000CC264 File Offset: 0x000CA464
		private Rectangle GetItemRectOrEmpty(int index)
		{
			if (index < 0 || index >= this.Items.Count)
			{
				return Rectangle.Empty;
			}
			if (this.View == View.Details && this.Columns.Count == 0)
			{
				return Rectangle.Empty;
			}
			NativeMethods.RECT rect = default(NativeMethods.RECT);
			rect.left = 0;
			if ((int)(long)base.SendMessage(4110, index, ref rect) == 0)
			{
				return Rectangle.Empty;
			}
			return Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
		}

		// Token: 0x06002D79 RID: 11641 RVA: 0x000CC2F0 File Offset: 0x000CA4F0
		private NativeMethods.LVGROUP GetLVGROUP(ListViewGroup group)
		{
			NativeMethods.LVGROUP lvgroup = new NativeMethods.LVGROUP();
			lvgroup.mask = 25U;
			string header = group.Header;
			lvgroup.pszHeader = Marshal.StringToHGlobalAuto(header);
			lvgroup.cchHeader = header.Length;
			lvgroup.iGroupId = group.ID;
			switch (group.HeaderAlignment)
			{
			case HorizontalAlignment.Left:
				lvgroup.uAlign = 1U;
				break;
			case HorizontalAlignment.Right:
				lvgroup.uAlign = 4U;
				break;
			case HorizontalAlignment.Center:
				lvgroup.uAlign = 2U;
				break;
			}
			return lvgroup;
		}

		// Token: 0x06002D7A RID: 11642 RVA: 0x000CC36B File Offset: 0x000CA56B
		internal Rectangle GetSubItemRect(int itemIndex, int subItemIndex)
		{
			return this.GetSubItemRect(itemIndex, subItemIndex, ItemBoundsPortion.Entire);
		}

		// Token: 0x06002D7B RID: 11643 RVA: 0x000CC378 File Offset: 0x000CA578
		internal Rectangle GetSubItemRect(int itemIndex, int subItemIndex, ItemBoundsPortion portion)
		{
			if (this.View != View.Details)
			{
				return Rectangle.Empty;
			}
			if (itemIndex < 0 || itemIndex >= this.Items.Count)
			{
				throw new ArgumentOutOfRangeException("itemIndex", SR.GetString("InvalidArgument", new object[]
				{
					"itemIndex",
					itemIndex.ToString(CultureInfo.CurrentCulture)
				}));
			}
			int count = this.Items[itemIndex].SubItems.Count;
			if (subItemIndex < 0 || subItemIndex >= count)
			{
				throw new ArgumentOutOfRangeException("subItemIndex", SR.GetString("InvalidArgument", new object[]
				{
					"subItemIndex",
					subItemIndex.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (!ClientUtils.IsEnumValid(portion, (int)portion, 0, 3))
			{
				throw new InvalidEnumArgumentException("portion", (int)portion, typeof(ItemBoundsPortion));
			}
			if (this.Columns.Count == 0)
			{
				return Rectangle.Empty;
			}
			NativeMethods.RECT rect = default(NativeMethods.RECT);
			rect.left = (int)portion;
			rect.top = subItemIndex;
			if ((int)(long)base.SendMessage(4152, itemIndex, ref rect) == 0)
			{
				throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
				{
					"itemIndex",
					itemIndex.ToString(CultureInfo.CurrentCulture)
				}));
			}
			return Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
		}

		/// <summary>Provides item information, given a point.</summary>
		/// <param name="point">The <see cref="T:System.Drawing.Point" /> at which to retrieve the item information. The coordinates are relative to the upper-left corner of the control.</param>
		/// <returns>The item information, given a point.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The point contains coordinates that are less than 0.</exception>
		// Token: 0x06002D7C RID: 11644 RVA: 0x000CC4DC File Offset: 0x000CA6DC
		public ListViewHitTestInfo HitTest(Point point)
		{
			return this.HitTest(point.X, point.Y);
		}

		/// <summary>Provides item information, given x- and y-coordinates.</summary>
		/// <param name="x">The x-coordinate at which to retrieve the item information. The coordinate is relative to the upper-left corner of the control.</param>
		/// <param name="y">The y-coordinate at which to retrieve the item information. The coordinate is relative to the upper-left corner of the control.</param>
		/// <returns>The item information, given x- and y- coordinates.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The x- or y-coordinate is less than 0.</exception>
		// Token: 0x06002D7D RID: 11645 RVA: 0x000CC4F4 File Offset: 0x000CA6F4
		public ListViewHitTestInfo HitTest(int x, int y)
		{
			if (!base.ClientRectangle.Contains(x, y))
			{
				return new ListViewHitTestInfo(null, null, ListViewHitTestLocations.None);
			}
			NativeMethods.LVHITTESTINFO lvhittestinfo = new NativeMethods.LVHITTESTINFO();
			lvhittestinfo.pt_x = x;
			lvhittestinfo.pt_y = y;
			int num;
			if (this.View == View.Details)
			{
				num = (int)(long)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4153, 0, lvhittestinfo);
			}
			else
			{
				num = (int)(long)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4114, 0, lvhittestinfo);
			}
			ListViewItem listViewItem = ((num == -1) ? null : this.Items[num]);
			ListViewHitTestLocations listViewHitTestLocations;
			if (listViewItem == null && (8 & lvhittestinfo.flags) == 8)
			{
				listViewHitTestLocations = (ListViewHitTestLocations)((247 & lvhittestinfo.flags) | 256);
			}
			else if (listViewItem != null && (8 & lvhittestinfo.flags) == 8)
			{
				listViewHitTestLocations = (ListViewHitTestLocations)((247 & lvhittestinfo.flags) | 512);
			}
			else
			{
				listViewHitTestLocations = (ListViewHitTestLocations)lvhittestinfo.flags;
			}
			if (this.View != View.Details || listViewItem == null)
			{
				return new ListViewHitTestInfo(listViewItem, null, listViewHitTestLocations);
			}
			if (lvhittestinfo.iSubItem < listViewItem.SubItems.Count)
			{
				return new ListViewHitTestInfo(listViewItem, listViewItem.SubItems[lvhittestinfo.iSubItem], listViewHitTestLocations);
			}
			return new ListViewHitTestInfo(listViewItem, null, listViewHitTestLocations);
		}

		// Token: 0x06002D7E RID: 11646 RVA: 0x000CC628 File Offset: 0x000CA828
		private void InvalidateColumnHeaders()
		{
			if (this.viewStyle == View.Details && base.IsHandleCreated)
			{
				IntPtr intPtr = UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4127, 0, 0);
				if (intPtr != IntPtr.Zero)
				{
					SafeNativeMethods.InvalidateRect(new HandleRef(this, intPtr), null, true);
				}
			}
		}

		// Token: 0x06002D7F RID: 11647 RVA: 0x000CC67B File Offset: 0x000CA87B
		internal ColumnHeader InsertColumn(int index, ColumnHeader ch)
		{
			return this.InsertColumn(index, ch, true);
		}

		// Token: 0x06002D80 RID: 11648 RVA: 0x000CC688 File Offset: 0x000CA888
		internal ColumnHeader InsertColumn(int index, ColumnHeader ch, bool refreshSubItems)
		{
			if (ch == null)
			{
				throw new ArgumentNullException("ch");
			}
			if (ch.OwnerListview != null)
			{
				throw new ArgumentException(SR.GetString("OnlyOneControl", new object[] { ch.Text }), "ch");
			}
			int num;
			if (base.IsHandleCreated && this.View != View.Tile)
			{
				num = this.InsertColumnNative(index, ch);
			}
			else
			{
				num = index;
			}
			if (-1 == num)
			{
				throw new InvalidOperationException(SR.GetString("ListViewAddColumnFailed"));
			}
			int num2 = ((this.columnHeaders == null) ? 0 : this.columnHeaders.Length);
			if (num2 > 0)
			{
				ColumnHeader[] array = new ColumnHeader[num2 + 1];
				if (num2 > 0)
				{
					Array.Copy(this.columnHeaders, 0, array, 0, num2);
				}
				this.columnHeaders = array;
			}
			else
			{
				this.columnHeaders = new ColumnHeader[1];
			}
			if (num < num2)
			{
				Array.Copy(this.columnHeaders, num, this.columnHeaders, num + 1, num2 - num);
			}
			this.columnHeaders[num] = ch;
			ch.OwnerListview = this;
			if (ch.ActualImageIndex_Internal != -1 && base.IsHandleCreated && this.View != View.Tile)
			{
				this.SetColumnInfo(16, ch);
			}
			int[] array2 = new int[this.Columns.Count];
			for (int i = 0; i < this.Columns.Count; i++)
			{
				ColumnHeader columnHeader = this.Columns[i];
				if (columnHeader == ch)
				{
					columnHeader.DisplayIndexInternal = index;
				}
				else if (columnHeader.DisplayIndex >= index)
				{
					ColumnHeader columnHeader2 = columnHeader;
					int displayIndexInternal = columnHeader2.DisplayIndexInternal;
					columnHeader2.DisplayIndexInternal = displayIndexInternal + 1;
				}
				array2[i] = columnHeader.DisplayIndexInternal;
			}
			this.SetDisplayIndices(array2);
			if (base.IsHandleCreated && this.View == View.Tile)
			{
				this.RecreateHandleInternal();
			}
			else if (base.IsHandleCreated && refreshSubItems)
			{
				this.RealizeAllSubItems();
			}
			return ch;
		}

		// Token: 0x06002D81 RID: 11649 RVA: 0x000CC83C File Offset: 0x000CAA3C
		private int InsertColumnNative(int index, ColumnHeader ch)
		{
			NativeMethods.LVCOLUMN_T lvcolumn_T = new NativeMethods.LVCOLUMN_T();
			lvcolumn_T.mask = 7;
			if (ch.OwnerListview != null && ch.ActualImageIndex_Internal != -1)
			{
				lvcolumn_T.mask |= 16;
				lvcolumn_T.iImage = ch.ActualImageIndex_Internal;
			}
			lvcolumn_T.fmt = (int)ch.TextAlign;
			lvcolumn_T.cx = ch.Width;
			lvcolumn_T.pszText = ch.Text;
			return (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.LVM_INSERTCOLUMN, index, lvcolumn_T);
		}

		// Token: 0x06002D82 RID: 11650 RVA: 0x000CC8C4 File Offset: 0x000CAAC4
		internal void InsertGroupInListView(int index, ListViewGroup group)
		{
			bool flag = this.groups.Count == 1 && this.GroupsEnabled;
			this.UpdateGroupView();
			this.EnsureDefaultGroup();
			this.InsertGroupNative(index, group);
			if (flag)
			{
				for (int i = 0; i < this.Items.Count; i++)
				{
					ListViewItem listViewItem = this.Items[i];
					if (listViewItem.Group == null)
					{
						listViewItem.UpdateStateToListView(listViewItem.Index);
					}
				}
			}
		}

		// Token: 0x06002D83 RID: 11651 RVA: 0x000CC938 File Offset: 0x000CAB38
		private void InsertGroupNative(int index, ListViewGroup group)
		{
			NativeMethods.LVGROUP lvgroup = new NativeMethods.LVGROUP();
			try
			{
				lvgroup = this.GetLVGROUP(group);
				int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4241, index, lvgroup);
			}
			finally
			{
				this.DestroyLVGROUP(lvgroup);
			}
		}

		// Token: 0x06002D84 RID: 11652 RVA: 0x000CC98C File Offset: 0x000CAB8C
		private void InsertItems(int displayIndex, ListViewItem[] items, bool checkHosting)
		{
			if (items == null || items.Length == 0)
			{
				return;
			}
			if (base.IsHandleCreated && this.Items.Count == 0 && this.View == View.SmallIcon && this.ComctlSupportsVisualStyles)
			{
				this.FlipViewToLargeIconAndSmallIcon = true;
			}
			if (this.updateCounter > 0 && base.Properties.GetObject(ListView.PropDelayedUpdateItems) != null)
			{
				if (checkHosting)
				{
					for (int i = 0; i < items.Length; i++)
					{
						if (items[i].listView != null)
						{
							throw new ArgumentException(SR.GetString("OnlyOneControl", new object[] { items[i].Text }), "item");
						}
					}
				}
				ArrayList arrayList = (ArrayList)base.Properties.GetObject(ListView.PropDelayedUpdateItems);
				if (arrayList != null)
				{
					arrayList.AddRange(items);
				}
				for (int j = 0; j < items.Length; j++)
				{
					items[j].Host(this, this.GenerateUniqueID(), -1);
				}
				this.FlipViewToLargeIconAndSmallIcon = false;
				return;
			}
			for (int k = 0; k < items.Length; k++)
			{
				ListViewItem listViewItem = items[k];
				if (checkHosting && listViewItem.listView != null)
				{
					throw new ArgumentException(SR.GetString("OnlyOneControl", new object[] { listViewItem.Text }), "item");
				}
				int num = this.GenerateUniqueID();
				this.listItemsTable.Add(num, listViewItem);
				this.itemCount++;
				listViewItem.Host(this, num, -1);
				if (!base.IsHandleCreated)
				{
					this.listItemsArray.Insert(displayIndex + k, listViewItem);
				}
			}
			if (base.IsHandleCreated)
			{
				this.InsertItemsNative(displayIndex, items);
			}
			base.Invalidate();
			this.ArrangeIcons(this.alignStyle);
			if (!this.VirtualMode)
			{
				this.Sort();
			}
		}

		// Token: 0x06002D85 RID: 11653 RVA: 0x000CCB40 File Offset: 0x000CAD40
		private int InsertItemsNative(int index, ListViewItem[] items)
		{
			if (items == null || items.Length == 0)
			{
				return 0;
			}
			if (index == this.itemCount - 1)
			{
				index++;
			}
			NativeMethods.LVITEM lvitem = default(NativeMethods.LVITEM);
			int num = -1;
			IntPtr intPtr = IntPtr.Zero;
			int num2 = 0;
			this.listViewState1[1] = true;
			try
			{
				base.SendMessage(4143, this.itemCount, 0);
				for (int i = 0; i < items.Length; i++)
				{
					ListViewItem listViewItem = items[i];
					lvitem.Reset();
					lvitem.mask = 23;
					lvitem.iItem = index + i;
					lvitem.pszText = listViewItem.Text;
					lvitem.iImage = listViewItem.ImageIndexer.ActualIndex;
					lvitem.iIndent = listViewItem.IndentCount;
					lvitem.lParam = (IntPtr)listViewItem.ID;
					if (this.GroupsEnabled)
					{
						lvitem.mask |= 256;
						lvitem.iGroupId = this.GetNativeGroupId(listViewItem);
					}
					lvitem.mask |= 512;
					lvitem.cColumns = ((this.columnHeaders != null) ? Math.Min(20, this.columnHeaders.Length) : 0);
					if (lvitem.cColumns > num2 || intPtr == IntPtr.Zero)
					{
						if (intPtr != IntPtr.Zero)
						{
							Marshal.FreeHGlobal(intPtr);
						}
						intPtr = Marshal.AllocHGlobal(lvitem.cColumns * Marshal.SizeOf(typeof(int)));
						num2 = lvitem.cColumns;
					}
					lvitem.puColumns = intPtr;
					int[] array = new int[lvitem.cColumns];
					for (int j = 0; j < lvitem.cColumns; j++)
					{
						array[j] = j + 1;
					}
					Marshal.Copy(array, 0, lvitem.puColumns, lvitem.cColumns);
					ItemCheckEventHandler itemCheckEventHandler = this.onItemCheck;
					this.onItemCheck = null;
					int num3;
					try
					{
						listViewItem.UpdateStateToListView(lvitem.iItem, ref lvitem, false);
						num3 = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.LVM_INSERTITEM, 0, ref lvitem);
						if (num == -1)
						{
							num = num3;
							index = num;
						}
					}
					finally
					{
						this.onItemCheck = itemCheckEventHandler;
					}
					if (-1 == num3)
					{
						throw new InvalidOperationException(SR.GetString("ListViewAddItemFailed"));
					}
					for (int k = 1; k < listViewItem.SubItems.Count; k++)
					{
						this.SetItemText(num3, k, listViewItem.SubItems[k].Text, ref lvitem);
					}
					if (listViewItem.StateImageSet || listViewItem.StateSelected)
					{
						this.SetItemState(num3, lvitem.state, lvitem.stateMask);
					}
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
				this.listViewState1[1] = false;
			}
			if (this.listViewState1[16])
			{
				this.listViewState1[16] = false;
				this.OnSelectedIndexChanged(EventArgs.Empty);
			}
			if (this.FlipViewToLargeIconAndSmallIcon)
			{
				this.FlipViewToLargeIconAndSmallIcon = false;
				this.View = View.LargeIcon;
				this.View = View.SmallIcon;
			}
			return num;
		}

		/// <summary>Determines whether the specified key is a regular input key or a special key that requires preprocessing.</summary>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values.</param>
		/// <returns>
		///   <see langword="true" /> if the specified key is a regular input key; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002D86 RID: 11654 RVA: 0x000CCE64 File Offset: 0x000CB064
		protected override bool IsInputKey(Keys keyData)
		{
			if ((keyData & Keys.Alt) == Keys.Alt)
			{
				return false;
			}
			Keys keys = keyData & Keys.KeyCode;
			if (keys - Keys.Prior <= 3)
			{
				return true;
			}
			bool flag = base.IsInputKey(keyData);
			if (flag)
			{
				return true;
			}
			if (this.listViewState[16384])
			{
				Keys keys2 = keyData & Keys.KeyCode;
				if (keys2 == Keys.Return || keys2 == Keys.Escape)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002D87 RID: 11655 RVA: 0x000CCEC8 File Offset: 0x000CB0C8
		private void LargeImageListRecreateHandle(object sender, EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				IntPtr intPtr = ((this.LargeImageList == null) ? IntPtr.Zero : this.LargeImageList.Handle);
				base.SendMessage(4099, (IntPtr)0, intPtr);
				this.ForceCheckBoxUpdate();
			}
		}

		// Token: 0x06002D88 RID: 11656 RVA: 0x000CCF14 File Offset: 0x000CB114
		private void LargeImageListChangedHandle(object sender, EventArgs e)
		{
			if (!this.VirtualMode && sender != null && sender == this.imageListLarge && base.IsHandleCreated)
			{
				foreach (object obj in this.Items)
				{
					ListViewItem listViewItem = (ListViewItem)obj;
					if (listViewItem.ImageIndexer.ActualIndex != -1 && listViewItem.ImageIndexer.ActualIndex >= this.imageListLarge.Images.Count)
					{
						this.SetItemImage(listViewItem.Index, this.imageListLarge.Images.Count - 1);
					}
					else
					{
						this.SetItemImage(listViewItem.Index, listViewItem.ImageIndexer.ActualIndex);
					}
				}
			}
		}

		// Token: 0x06002D89 RID: 11657 RVA: 0x000CCFF0 File Offset: 0x000CB1F0
		internal void ListViewItemToolTipChanged(ListViewItem item)
		{
			if (base.IsHandleCreated)
			{
				this.SetItemText(item.Index, 0, item.Text);
			}
		}

		// Token: 0x06002D8A RID: 11658 RVA: 0x000CD010 File Offset: 0x000CB210
		private void LvnBeginDrag(MouseButtons buttons, NativeMethods.NMLISTVIEW nmlv)
		{
			ListViewItem listViewItem = this.Items[nmlv.iItem];
			this.OnItemDrag(new ItemDragEventArgs(buttons, listViewItem));
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.AfterLabelEdit" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.LabelEditEventArgs" /> that contains the event data.</param>
		// Token: 0x06002D8B RID: 11659 RVA: 0x000CD03C File Offset: 0x000CB23C
		protected virtual void OnAfterLabelEdit(LabelEditEventArgs e)
		{
			if (this.onAfterLabelEdit != null)
			{
				this.onAfterLabelEdit(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackgroundImageChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002D8C RID: 11660 RVA: 0x000CD053 File Offset: 0x000CB253
		protected override void OnBackgroundImageChanged(EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				this.SetBackgroundImage();
			}
			base.OnBackgroundImageChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002D8D RID: 11661 RVA: 0x000CD06A File Offset: 0x000CB26A
		protected override void OnMouseLeave(EventArgs e)
		{
			this.hoveredAlready = false;
			base.OnMouseLeave(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseHover" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002D8E RID: 11662 RVA: 0x000CD07C File Offset: 0x000CB27C
		protected override void OnMouseHover(EventArgs e)
		{
			ListViewItem listViewItem = null;
			if (this.Items.Count > 0)
			{
				Point point = Cursor.Position;
				point = base.PointToClientInternal(point);
				listViewItem = this.GetItemAt(point.X, point.Y);
			}
			if (listViewItem != this.prevHoveredItem && listViewItem != null)
			{
				this.OnItemMouseHover(new ListViewItemMouseHoverEventArgs(listViewItem));
				this.prevHoveredItem = listViewItem;
			}
			if (!this.hoveredAlready)
			{
				base.OnMouseHover(e);
				this.hoveredAlready = true;
			}
			base.ResetMouseEventArgs();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.BeforeLabelEdit" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.LabelEditEventArgs" /> that contains the event data.</param>
		// Token: 0x06002D8F RID: 11663 RVA: 0x000CD0F7 File Offset: 0x000CB2F7
		protected virtual void OnBeforeLabelEdit(LabelEditEventArgs e)
		{
			if (this.onBeforeLabelEdit != null)
			{
				this.onBeforeLabelEdit(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.CacheVirtualItems" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.CacheVirtualItemsEventArgs" /> that contains the event data.</param>
		// Token: 0x06002D90 RID: 11664 RVA: 0x000CD110 File Offset: 0x000CB310
		protected virtual void OnCacheVirtualItems(CacheVirtualItemsEventArgs e)
		{
			CacheVirtualItemsEventHandler cacheVirtualItemsEventHandler = (CacheVirtualItemsEventHandler)base.Events[ListView.EVENT_CACHEVIRTUALITEMS];
			if (cacheVirtualItemsEventHandler != null)
			{
				cacheVirtualItemsEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.ColumnClick" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ColumnClickEventArgs" /> that contains the event data.</param>
		// Token: 0x06002D91 RID: 11665 RVA: 0x000CD13E File Offset: 0x000CB33E
		protected virtual void OnColumnClick(ColumnClickEventArgs e)
		{
			if (this.onColumnClick != null)
			{
				this.onColumnClick(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.ColumnReordered" /> event.</summary>
		/// <param name="e">The <see cref="T:System.Windows.Forms.ColumnReorderedEventArgs" /> that contains the event data.</param>
		// Token: 0x06002D92 RID: 11666 RVA: 0x000CD158 File Offset: 0x000CB358
		protected virtual void OnColumnReordered(ColumnReorderedEventArgs e)
		{
			ColumnReorderedEventHandler columnReorderedEventHandler = (ColumnReorderedEventHandler)base.Events[ListView.EVENT_COLUMNREORDERED];
			if (columnReorderedEventHandler != null)
			{
				columnReorderedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.ColumnWidthChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ColumnWidthChangedEventArgs" /> that contains the event data.</param>
		// Token: 0x06002D93 RID: 11667 RVA: 0x000CD188 File Offset: 0x000CB388
		protected virtual void OnColumnWidthChanged(ColumnWidthChangedEventArgs e)
		{
			ColumnWidthChangedEventHandler columnWidthChangedEventHandler = (ColumnWidthChangedEventHandler)base.Events[ListView.EVENT_COLUMNWIDTHCHANGED];
			if (columnWidthChangedEventHandler != null)
			{
				columnWidthChangedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.ColumnWidthChanging" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ColumnWidthChangingEventArgs" /> that contains the event data.</param>
		// Token: 0x06002D94 RID: 11668 RVA: 0x000CD1B8 File Offset: 0x000CB3B8
		protected virtual void OnColumnWidthChanging(ColumnWidthChangingEventArgs e)
		{
			ColumnWidthChangingEventHandler columnWidthChangingEventHandler = (ColumnWidthChangingEventHandler)base.Events[ListView.EVENT_COLUMNWIDTHCHANGING];
			if (columnWidthChangingEventHandler != null)
			{
				columnWidthChangingEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.DrawColumnHeader" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DrawListViewColumnHeaderEventArgs" /> that contains the event data.</param>
		// Token: 0x06002D95 RID: 11669 RVA: 0x000CD1E8 File Offset: 0x000CB3E8
		protected virtual void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
		{
			DrawListViewColumnHeaderEventHandler drawListViewColumnHeaderEventHandler = (DrawListViewColumnHeaderEventHandler)base.Events[ListView.EVENT_DRAWCOLUMNHEADER];
			if (drawListViewColumnHeaderEventHandler != null)
			{
				drawListViewColumnHeaderEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.DrawItem" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DrawListViewItemEventArgs" /> that contains the event data.</param>
		// Token: 0x06002D96 RID: 11670 RVA: 0x000CD218 File Offset: 0x000CB418
		protected virtual void OnDrawItem(DrawListViewItemEventArgs e)
		{
			DrawListViewItemEventHandler drawListViewItemEventHandler = (DrawListViewItemEventHandler)base.Events[ListView.EVENT_DRAWITEM];
			if (drawListViewItemEventHandler != null)
			{
				drawListViewItemEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.DrawSubItem" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DrawListViewSubItemEventArgs" /> that contains the event data.</param>
		// Token: 0x06002D97 RID: 11671 RVA: 0x000CD248 File Offset: 0x000CB448
		protected virtual void OnDrawSubItem(DrawListViewSubItemEventArgs e)
		{
			DrawListViewSubItemEventHandler drawListViewSubItemEventHandler = (DrawListViewSubItemEventHandler)base.Events[ListView.EVENT_DRAWSUBITEM];
			if (drawListViewSubItemEventHandler != null)
			{
				drawListViewSubItemEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see langword="FontChanged" /> event.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002D98 RID: 11672 RVA: 0x000CD278 File Offset: 0x000CB478
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			if (!this.VirtualMode && base.IsHandleCreated && this.AutoArrange)
			{
				this.BeginUpdate();
				try
				{
					base.SendMessage(4138, -1, 0);
				}
				finally
				{
					this.EndUpdate();
				}
			}
			this.InvalidateColumnHeaders();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002D99 RID: 11673 RVA: 0x000CD2D8 File Offset: 0x000CB4D8
		protected override void OnHandleCreated(EventArgs e)
		{
			this.listViewState[4194304] = false;
			this.FlipViewToLargeIconAndSmallIcon = false;
			base.OnHandleCreated(e);
			int num = (int)(long)base.SendMessage(8200, 0, 0);
			if (num < 5)
			{
				base.SendMessage(8199, 5, 0);
			}
			this.UpdateExtendedStyles();
			this.RealizeProperties();
			int num2 = ColorTranslator.ToWin32(this.BackColor);
			base.SendMessage(4097, 0, num2);
			base.SendMessage(4132, 0, ColorTranslator.ToWin32(base.ForeColor));
			base.SendMessage(4134, 0, -1);
			if (!this.Scrollable)
			{
				int num3 = (int)(long)UnsafeNativeMethods.GetWindowLong(new HandleRef(this, base.Handle), -16);
				num3 |= 8192;
				UnsafeNativeMethods.SetWindowLong(new HandleRef(this, base.Handle), -16, new HandleRef(null, (IntPtr)num3));
			}
			if (this.VirtualMode)
			{
				int num4 = (int)(long)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4106, 0, 0);
				num4 |= 61440;
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4107, num4, 0);
			}
			if (this.ComctlSupportsVisualStyles)
			{
				base.SendMessage(4238, (int)this.viewStyle, 0);
				this.UpdateGroupView();
				if (this.groups != null)
				{
					for (int i = 0; i < this.groups.Count; i++)
					{
						this.InsertGroupNative(i, this.groups[i]);
					}
				}
				if (this.viewStyle == View.Tile)
				{
					this.UpdateTileView();
				}
			}
			this.ListViewHandleDestroyed = false;
			ListViewItem[] array = null;
			if (this.listItemsArray != null)
			{
				array = (ListViewItem[])this.listItemsArray.ToArray(typeof(ListViewItem));
				this.listItemsArray = null;
			}
			int num5 = ((this.columnHeaders == null) ? 0 : this.columnHeaders.Length);
			if (num5 > 0)
			{
				int[] array2 = new int[this.columnHeaders.Length];
				int num6 = 0;
				foreach (ColumnHeader columnHeader in this.columnHeaders)
				{
					array2[num6] = columnHeader.DisplayIndex;
					this.InsertColumnNative(num6++, columnHeader);
				}
				this.SetDisplayIndices(array2);
			}
			if (this.itemCount > 0 && array != null)
			{
				this.InsertItemsNative(0, array);
			}
			if (this.VirtualMode && this.VirtualListSize > -1 && !base.DesignMode)
			{
				base.SendMessage(4143, this.VirtualListSize, 0);
			}
			if (num5 > 0)
			{
				this.UpdateColumnWidths(ColumnHeaderAutoResizeStyle.None);
			}
			this.ArrangeIcons(this.alignStyle);
			this.UpdateListViewItemsLocations();
			if (!this.VirtualMode)
			{
				this.Sort();
			}
			if (this.ComctlSupportsVisualStyles && this.InsertionMark.Index > 0)
			{
				this.InsertionMark.UpdateListView();
			}
			this.savedCheckedItems = null;
			if (!this.CheckBoxes && !this.VirtualMode)
			{
				for (int k = 0; k < this.Items.Count; k++)
				{
					if (this.Items[k].Checked)
					{
						this.UpdateSavedCheckedItems(this.Items[k], true);
					}
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002D9A RID: 11674 RVA: 0x000CD600 File Offset: 0x000CB800
		protected override void OnHandleDestroyed(EventArgs e)
		{
			if (!base.Disposing && !this.VirtualMode)
			{
				int count = this.Items.Count;
				for (int i = 0; i < count; i++)
				{
					this.Items[i].UpdateStateFromListView(i, true);
				}
				if (this.SelectedItems != null && !this.VirtualMode)
				{
					ListViewItem[] array = new ListViewItem[this.SelectedItems.Count];
					this.SelectedItems.CopyTo(array, 0);
					this.savedSelectedItems = new List<ListViewItem>(array.Length);
					for (int j = 0; j < array.Length; j++)
					{
						this.savedSelectedItems.Add(array[j]);
					}
				}
				ListViewItem[] array2 = null;
				ListView.ListViewItemCollection items = this.Items;
				if (items != null)
				{
					array2 = new ListViewItem[items.Count];
					items.CopyTo(array2, 0);
				}
				if (array2 != null)
				{
					this.listItemsArray = new ArrayList(array2.Length);
					this.listItemsArray.AddRange(array2);
				}
				this.ListViewHandleDestroyed = true;
			}
			base.OnHandleDestroyed(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.ItemActivate" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002D9B RID: 11675 RVA: 0x000CD6FB File Offset: 0x000CB8FB
		protected virtual void OnItemActivate(EventArgs e)
		{
			if (this.onItemActivate != null)
			{
				this.onItemActivate(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.ItemCheck" /> event.</summary>
		/// <param name="ice">An <see cref="T:System.Windows.Forms.ItemCheckEventArgs" /> that contains the event data.</param>
		// Token: 0x06002D9C RID: 11676 RVA: 0x000CD712 File Offset: 0x000CB912
		protected virtual void OnItemCheck(ItemCheckEventArgs ice)
		{
			if (this.onItemCheck != null)
			{
				this.onItemCheck(this, ice);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.ItemChecked" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Windows.Forms.ItemCheckedEventArgs" /> that contains the event data.</param>
		// Token: 0x06002D9D RID: 11677 RVA: 0x000CD729 File Offset: 0x000CB929
		protected virtual void OnItemChecked(ItemCheckedEventArgs e)
		{
			if (this.onItemChecked != null)
			{
				this.onItemChecked(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.ItemDrag" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Windows.Forms.ItemDragEventArgs" /> that contains the event data.</param>
		// Token: 0x06002D9E RID: 11678 RVA: 0x000CD740 File Offset: 0x000CB940
		protected virtual void OnItemDrag(ItemDragEventArgs e)
		{
			if (this.onItemDrag != null)
			{
				this.onItemDrag(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.ItemMouseHover" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ListViewItemMouseHoverEventArgs" /> that contains the event data.</param>
		// Token: 0x06002D9F RID: 11679 RVA: 0x000CD757 File Offset: 0x000CB957
		protected virtual void OnItemMouseHover(ListViewItemMouseHoverEventArgs e)
		{
			if (this.onItemMouseHover != null)
			{
				this.onItemMouseHover(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.ItemSelectionChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ListViewItemSelectionChangedEventArgs" /> that contains the event data.</param>
		// Token: 0x06002DA0 RID: 11680 RVA: 0x000CD770 File Offset: 0x000CB970
		protected virtual void OnItemSelectionChanged(ListViewItemSelectionChangedEventArgs e)
		{
			ListViewItemSelectionChangedEventHandler listViewItemSelectionChangedEventHandler = (ListViewItemSelectionChangedEventHandler)base.Events[ListView.EVENT_ITEMSELECTIONCHANGED];
			if (listViewItemSelectionChangedEventHandler != null)
			{
				listViewItemSelectionChangedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ParentChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002DA1 RID: 11681 RVA: 0x000CD79E File Offset: 0x000CB99E
		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
			if (base.IsHandleCreated)
			{
				this.RecreateHandleInternal();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002DA2 RID: 11682 RVA: 0x000CD7B5 File Offset: 0x000CB9B5
		protected override void OnResize(EventArgs e)
		{
			if (this.View == View.Details && !this.Scrollable && base.IsHandleCreated)
			{
				this.PositionHeader();
			}
			base.OnResize(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.RetrieveVirtualItem" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.RetrieveVirtualItemEventArgs" /> that contains the event data.</param>
		// Token: 0x06002DA3 RID: 11683 RVA: 0x000CD7E0 File Offset: 0x000CB9E0
		protected virtual void OnRetrieveVirtualItem(RetrieveVirtualItemEventArgs e)
		{
			RetrieveVirtualItemEventHandler retrieveVirtualItemEventHandler = (RetrieveVirtualItemEventHandler)base.Events[ListView.EVENT_RETRIEVEVIRTUALITEM];
			if (retrieveVirtualItemEventHandler != null)
			{
				retrieveVirtualItemEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.RightToLeftLayoutChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002DA4 RID: 11684 RVA: 0x000CD810 File Offset: 0x000CBA10
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnRightToLeftLayoutChanged(EventArgs e)
		{
			if (base.GetAnyDisposingInHierarchy())
			{
				return;
			}
			if (this.RightToLeft == RightToLeft.Yes)
			{
				this.RecreateHandleInternal();
			}
			EventHandler eventHandler = base.Events[ListView.EVENT_RIGHTTOLEFTLAYOUTCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.SearchForVirtualItem" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.SearchForVirtualItemEventArgs" /> that contains the event data.</param>
		// Token: 0x06002DA5 RID: 11685 RVA: 0x000CD858 File Offset: 0x000CBA58
		protected virtual void OnSearchForVirtualItem(SearchForVirtualItemEventArgs e)
		{
			SearchForVirtualItemEventHandler searchForVirtualItemEventHandler = (SearchForVirtualItemEventHandler)base.Events[ListView.EVENT_SEARCHFORVIRTUALITEM];
			if (searchForVirtualItemEventHandler != null)
			{
				searchForVirtualItemEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.SelectedIndexChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002DA6 RID: 11686 RVA: 0x000CD888 File Offset: 0x000CBA88
		protected virtual void OnSelectedIndexChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ListView.EVENT_SELECTEDINDEXCHANGED];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.SystemColorsChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002DA7 RID: 11687 RVA: 0x000CD8B8 File Offset: 0x000CBAB8
		protected override void OnSystemColorsChanged(EventArgs e)
		{
			base.OnSystemColorsChanged(e);
			if (base.IsHandleCreated)
			{
				int num = ColorTranslator.ToWin32(this.BackColor);
				base.SendMessage(4097, 0, num);
				base.SendMessage(4134, 0, -1);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListView.VirtualItemsSelectionRangeChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ListViewVirtualItemsSelectionRangeChangedEventArgs" /> that contains the event data.</param>
		// Token: 0x06002DA8 RID: 11688 RVA: 0x000CD8FC File Offset: 0x000CBAFC
		protected virtual void OnVirtualItemsSelectionRangeChanged(ListViewVirtualItemsSelectionRangeChangedEventArgs e)
		{
			ListViewVirtualItemsSelectionRangeChangedEventHandler listViewVirtualItemsSelectionRangeChangedEventHandler = (ListViewVirtualItemsSelectionRangeChangedEventHandler)base.Events[ListView.EVENT_VIRTUALITEMSSELECTIONRANGECHANGED];
			if (listViewVirtualItemsSelectionRangeChangedEventHandler != null)
			{
				listViewVirtualItemsSelectionRangeChangedEventHandler(this, e);
			}
		}

		// Token: 0x06002DA9 RID: 11689 RVA: 0x000CD92C File Offset: 0x000CBB2C
		private void PositionHeader()
		{
			IntPtr window = UnsafeNativeMethods.GetWindow(new HandleRef(this, base.Handle), 5);
			if (window != IntPtr.Zero)
			{
				IntPtr intPtr = IntPtr.Zero;
				IntPtr intPtr2 = IntPtr.Zero;
				intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NativeMethods.RECT)));
				if (intPtr == IntPtr.Zero)
				{
					return;
				}
				try
				{
					intPtr2 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NativeMethods.WINDOWPOS)));
					if (!(intPtr == IntPtr.Zero))
					{
						UnsafeNativeMethods.GetClientRect(new HandleRef(this, base.Handle), intPtr);
						NativeMethods.HDLAYOUT hdlayout = default(NativeMethods.HDLAYOUT);
						hdlayout.prc = intPtr;
						hdlayout.pwpos = intPtr2;
						UnsafeNativeMethods.SendMessage(new HandleRef(this, window), 4613, 0, ref hdlayout);
						NativeMethods.WINDOWPOS windowpos = (NativeMethods.WINDOWPOS)Marshal.PtrToStructure(intPtr2, typeof(NativeMethods.WINDOWPOS));
						SafeNativeMethods.SetWindowPos(new HandleRef(this, window), new HandleRef(this, windowpos.hwndInsertAfter), windowpos.x, windowpos.y, windowpos.cx, windowpos.cy, windowpos.flags | 64);
					}
				}
				finally
				{
					if (intPtr != IntPtr.Zero)
					{
						Marshal.FreeHGlobal(intPtr);
					}
					if (intPtr2 != IntPtr.Zero)
					{
						Marshal.FreeHGlobal(intPtr2);
					}
				}
			}
		}

		// Token: 0x06002DAA RID: 11690 RVA: 0x000CDA84 File Offset: 0x000CBC84
		private void RealizeAllSubItems()
		{
			NativeMethods.LVITEM lvitem = default(NativeMethods.LVITEM);
			for (int i = 0; i < this.itemCount; i++)
			{
				int count = this.Items[i].SubItems.Count;
				for (int j = 0; j < count; j++)
				{
					this.SetItemText(i, j, this.Items[i].SubItems[j].Text, ref lvitem);
				}
			}
		}

		/// <summary>Initializes the properties of the <see cref="T:System.Windows.Forms.ListView" /> control that manage the appearance of the control.</summary>
		// Token: 0x06002DAB RID: 11691 RVA: 0x000CDAF4 File Offset: 0x000CBCF4
		protected void RealizeProperties()
		{
			Color color = this.BackColor;
			if (color != SystemColors.Window)
			{
				base.SendMessage(4097, 0, ColorTranslator.ToWin32(color));
			}
			color = this.ForeColor;
			if (color != SystemColors.WindowText)
			{
				base.SendMessage(4132, 0, ColorTranslator.ToWin32(color));
			}
			if (this.imageListLarge != null)
			{
				base.SendMessage(4099, 0, this.imageListLarge.Handle);
			}
			if (this.imageListSmall != null)
			{
				base.SendMessage(4099, 1, this.imageListSmall.Handle);
			}
			if (this.imageListState != null)
			{
				base.SendMessage(4099, 2, this.imageListState.Handle);
			}
		}

		/// <summary>Forces a range of <see cref="T:System.Windows.Forms.ListViewItem" /> objects to be redrawn.</summary>
		/// <param name="startIndex">The index for the first item in the range to be redrawn.</param>
		/// <param name="endIndex">The index for the last item of the range to be redrawn.</param>
		/// <param name="invalidateOnly">
		///   <see langword="true" /> to invalidate the range of items; <see langword="false" /> to invalidate and repaint the items.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> or <paramref name="endIndex" /> is less than 0, greater than or equal to the number of items in the <see cref="T:System.Windows.Forms.ListView" /> or, if in virtual mode, greater than the value of <see cref="P:System.Windows.Forms.ListView.VirtualListSize" />.  
		/// -or-  
		/// The given <paramref name="startIndex" /> is greater than the <paramref name="endIndex." /></exception>
		// Token: 0x06002DAC RID: 11692 RVA: 0x000CDBB0 File Offset: 0x000CBDB0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void RedrawItems(int startIndex, int endIndex, bool invalidateOnly)
		{
			if (this.VirtualMode)
			{
				if (startIndex < 0 || startIndex >= this.VirtualListSize)
				{
					throw new ArgumentOutOfRangeException("startIndex", SR.GetString("InvalidArgument", new object[]
					{
						"startIndex",
						startIndex.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (endIndex < 0 || endIndex >= this.VirtualListSize)
				{
					throw new ArgumentOutOfRangeException("endIndex", SR.GetString("InvalidArgument", new object[]
					{
						"endIndex",
						endIndex.ToString(CultureInfo.CurrentCulture)
					}));
				}
			}
			else
			{
				if (startIndex < 0 || startIndex >= this.Items.Count)
				{
					throw new ArgumentOutOfRangeException("startIndex", SR.GetString("InvalidArgument", new object[]
					{
						"startIndex",
						startIndex.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (endIndex < 0 || endIndex >= this.Items.Count)
				{
					throw new ArgumentOutOfRangeException("endIndex", SR.GetString("InvalidArgument", new object[]
					{
						"endIndex",
						endIndex.ToString(CultureInfo.CurrentCulture)
					}));
				}
			}
			if (startIndex > endIndex)
			{
				throw new ArgumentException(SR.GetString("ListViewStartIndexCannotBeLargerThanEndIndex"));
			}
			if (base.IsHandleCreated)
			{
				int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4117, startIndex, endIndex);
				if (this.View == View.LargeIcon || this.View == View.SmallIcon)
				{
					Rectangle rectangle = this.Items[startIndex].Bounds;
					for (int i = startIndex + 1; i <= endIndex; i++)
					{
						rectangle = Rectangle.Union(rectangle, this.Items[i].Bounds);
					}
					if (startIndex > 0)
					{
						rectangle = Rectangle.Union(rectangle, this.Items[startIndex - 1].Bounds);
					}
					else
					{
						rectangle.Width += rectangle.X;
						rectangle.Height += rectangle.Y;
						rectangle.X = (rectangle.Y = 0);
					}
					if (endIndex < this.Items.Count - 1)
					{
						rectangle = Rectangle.Union(rectangle, this.Items[endIndex + 1].Bounds);
					}
					else
					{
						rectangle.Height += base.ClientRectangle.Bottom - rectangle.Bottom;
						rectangle.Width += base.ClientRectangle.Right - rectangle.Right;
					}
					if (this.View == View.LargeIcon)
					{
						rectangle.Inflate(1, this.Font.Height + 1);
					}
					base.Invalidate(rectangle);
				}
				if (!invalidateOnly)
				{
					base.Update();
				}
			}
		}

		// Token: 0x06002DAD RID: 11693 RVA: 0x000CDE5C File Offset: 0x000CC05C
		internal void RemoveGroupFromListView(ListViewGroup group)
		{
			this.EnsureDefaultGroup();
			foreach (object obj in group.Items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				if (listViewItem.ListView == this)
				{
					listViewItem.UpdateStateToListView(listViewItem.Index);
				}
			}
			this.RemoveGroupNative(group);
			this.UpdateGroupView();
		}

		// Token: 0x06002DAE RID: 11694 RVA: 0x000CDED8 File Offset: 0x000CC0D8
		private void RemoveGroupNative(ListViewGroup group)
		{
			int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4246, group.ID, IntPtr.Zero);
		}

		// Token: 0x06002DAF RID: 11695 RVA: 0x000CDF0C File Offset: 0x000CC10C
		private void Scroll(int fromLVItem, int toLVItem)
		{
			int num = this.GetItemPosition(toLVItem).Y - this.GetItemPosition(fromLVItem).Y;
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4116, 0, num);
		}

		// Token: 0x06002DB0 RID: 11696 RVA: 0x000CDF54 File Offset: 0x000CC154
		private void SetBackgroundImage()
		{
			Application.OleRequired();
			NativeMethods.LVBKIMAGE lvbkimage = new NativeMethods.LVBKIMAGE();
			lvbkimage.xOffset = 0;
			lvbkimage.yOffset = 0;
			string text = this.backgroundImageFileName;
			if (this.BackgroundImage != null)
			{
				EnvironmentPermission environmentPermission = new EnvironmentPermission(EnvironmentPermissionAccess.Read, "TEMP");
				FileIOPermission fileIOPermission = new FileIOPermission(PermissionState.Unrestricted);
				PermissionSet permissionSet = new PermissionSet(PermissionState.Unrestricted);
				permissionSet.AddPermission(environmentPermission);
				permissionSet.AddPermission(fileIOPermission);
				permissionSet.Assert();
				try
				{
					string tempPath = Path.GetTempPath();
					StringBuilder stringBuilder = new StringBuilder(1024);
					UnsafeNativeMethods.GetTempFileName(tempPath, this.GenerateRandomName(), 0, stringBuilder);
					this.backgroundImageFileName = stringBuilder.ToString();
					this.BackgroundImage.Save(this.backgroundImageFileName, ImageFormat.Bmp);
				}
				finally
				{
					PermissionSet.RevertAssert();
				}
				lvbkimage.pszImage = this.backgroundImageFileName;
				lvbkimage.cchImageMax = this.backgroundImageFileName.Length + 1;
				lvbkimage.ulFlags = 2;
				if (this.BackgroundImageTiled)
				{
					lvbkimage.ulFlags |= 16;
				}
				else
				{
					lvbkimage.ulFlags |= 0;
				}
			}
			else
			{
				lvbkimage.ulFlags = 0;
				this.backgroundImageFileName = string.Empty;
			}
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.LVM_SETBKIMAGE, 0, lvbkimage);
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			if (this.bkImgFileNames == null)
			{
				this.bkImgFileNames = new string[8];
				this.bkImgFileNamesCount = -1;
			}
			if (this.bkImgFileNamesCount == 7)
			{
				this.DeleteFileName(this.bkImgFileNames[0]);
				this.bkImgFileNames[0] = this.bkImgFileNames[1];
				this.bkImgFileNames[1] = this.bkImgFileNames[2];
				this.bkImgFileNames[2] = this.bkImgFileNames[3];
				this.bkImgFileNames[3] = this.bkImgFileNames[4];
				this.bkImgFileNames[4] = this.bkImgFileNames[5];
				this.bkImgFileNames[5] = this.bkImgFileNames[6];
				this.bkImgFileNames[6] = this.bkImgFileNames[7];
				this.bkImgFileNames[7] = null;
				this.bkImgFileNamesCount--;
			}
			this.bkImgFileNamesCount++;
			this.bkImgFileNames[this.bkImgFileNamesCount] = text;
			this.Refresh();
		}

		// Token: 0x06002DB1 RID: 11697 RVA: 0x000CE180 File Offset: 0x000CC380
		internal void SetColumnInfo(int mask, ColumnHeader ch)
		{
			if (base.IsHandleCreated)
			{
				NativeMethods.LVCOLUMN lvcolumn = new NativeMethods.LVCOLUMN();
				lvcolumn.mask = mask;
				if ((mask & 16) != 0 || (mask & 1) != 0)
				{
					lvcolumn.mask |= 1;
					if (ch.ActualImageIndex_Internal > -1)
					{
						lvcolumn.iImage = ch.ActualImageIndex_Internal;
						lvcolumn.fmt |= 2048;
					}
					lvcolumn.fmt |= (int)ch.TextAlign;
				}
				if ((mask & 4) != 0)
				{
					lvcolumn.pszText = Marshal.StringToHGlobalAuto(ch.Text);
				}
				int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.LVM_SETCOLUMN, ch.Index, lvcolumn);
				if ((mask & 4) != 0)
				{
					Marshal.FreeHGlobal(lvcolumn.pszText);
				}
				if (num == 0)
				{
					throw new InvalidOperationException(SR.GetString("ListViewColumnInfoSet"));
				}
				this.InvalidateColumnHeaders();
			}
		}

		// Token: 0x06002DB2 RID: 11698 RVA: 0x000CE25C File Offset: 0x000CC45C
		internal void SetColumnWidth(int columnIndex, ColumnHeaderAutoResizeStyle headerAutoResize)
		{
			if (columnIndex < 0 || (columnIndex >= 0 && this.columnHeaders == null) || columnIndex >= this.columnHeaders.Length)
			{
				throw new ArgumentOutOfRangeException("columnIndex", SR.GetString("InvalidArgument", new object[]
				{
					"columnIndex",
					columnIndex.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (!ClientUtils.IsEnumValid(headerAutoResize, (int)headerAutoResize, 0, 2))
			{
				throw new InvalidEnumArgumentException("headerAutoResize", (int)headerAutoResize, typeof(ColumnHeaderAutoResizeStyle));
			}
			int num = 0;
			int num2 = 0;
			if (headerAutoResize == ColumnHeaderAutoResizeStyle.None)
			{
				num = this.columnHeaders[columnIndex].WidthInternal;
				if (num == -2)
				{
					headerAutoResize = ColumnHeaderAutoResizeStyle.HeaderSize;
				}
				else if (num == -1)
				{
					headerAutoResize = ColumnHeaderAutoResizeStyle.ColumnContent;
				}
			}
			if (headerAutoResize == ColumnHeaderAutoResizeStyle.HeaderSize)
			{
				num2 = this.CompensateColumnHeaderResize(columnIndex, false);
				num = -2;
			}
			else if (headerAutoResize == ColumnHeaderAutoResizeStyle.ColumnContent)
			{
				num2 = this.CompensateColumnHeaderResize(columnIndex, false);
				num = -1;
			}
			if (base.IsHandleCreated)
			{
				base.SendMessage(4126, columnIndex, NativeMethods.Util.MAKELPARAM(num, 0));
			}
			if (base.IsHandleCreated && (headerAutoResize == ColumnHeaderAutoResizeStyle.ColumnContent || headerAutoResize == ColumnHeaderAutoResizeStyle.HeaderSize) && num2 != 0)
			{
				int num3 = this.columnHeaders[columnIndex].Width + num2;
				base.SendMessage(4126, columnIndex, NativeMethods.Util.MAKELPARAM(num3, 0));
			}
		}

		// Token: 0x06002DB3 RID: 11699 RVA: 0x000CE376 File Offset: 0x000CC576
		private void SetColumnWidth(int index, int width)
		{
			if (base.IsHandleCreated)
			{
				base.SendMessage(4126, index, NativeMethods.Util.MAKELPARAM(width, 0));
			}
		}

		// Token: 0x06002DB4 RID: 11700 RVA: 0x000CE394 File Offset: 0x000CC594
		private void SetDisplayIndices(int[] indices)
		{
			int[] array = new int[indices.Length];
			for (int i = 0; i < indices.Length; i++)
			{
				this.Columns[i].DisplayIndexInternal = indices[i];
				array[indices[i]] = i;
			}
			if (base.IsHandleCreated && !base.Disposing)
			{
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4154, array.Length, array);
			}
		}

		// Token: 0x06002DB5 RID: 11701 RVA: 0x000CE3FD File Offset: 0x000CC5FD
		internal void UpdateSavedCheckedItems(ListViewItem item, bool addItem)
		{
			if (addItem && this.savedCheckedItems == null)
			{
				this.savedCheckedItems = new List<ListViewItem>();
			}
			if (addItem)
			{
				this.savedCheckedItems.Add(item);
				return;
			}
			if (this.savedCheckedItems != null)
			{
				this.savedCheckedItems.Remove(item);
			}
		}

		// Token: 0x06002DB6 RID: 11702 RVA: 0x000CE43C File Offset: 0x000CC63C
		internal void SetToolTip(ToolTip toolTip, string toolTipCaption)
		{
			this.toolTipCaption = toolTipCaption;
			IntPtr intPtr = UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4170, new HandleRef(toolTip, toolTip.Handle), 0);
			UnsafeNativeMethods.DestroyWindow(new HandleRef(null, intPtr));
		}

		// Token: 0x06002DB7 RID: 11703 RVA: 0x000CE484 File Offset: 0x000CC684
		internal void SetItemImage(int index, int image)
		{
			if (index < 0 || (this.VirtualMode && index >= this.VirtualListSize) || (!this.VirtualMode && index >= this.itemCount))
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (base.IsHandleCreated)
			{
				NativeMethods.LVITEM lvitem = default(NativeMethods.LVITEM);
				lvitem.mask = 2;
				lvitem.iItem = index;
				lvitem.iImage = image;
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.LVM_SETITEM, 0, ref lvitem);
			}
		}

		// Token: 0x06002DB8 RID: 11704 RVA: 0x000CE52C File Offset: 0x000CC72C
		internal void SetItemIndentCount(int index, int indentCount)
		{
			if (index < 0 || (this.VirtualMode && index >= this.VirtualListSize) || (!this.VirtualMode && index >= this.itemCount))
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (base.IsHandleCreated)
			{
				NativeMethods.LVITEM lvitem = default(NativeMethods.LVITEM);
				lvitem.mask = 16;
				lvitem.iItem = index;
				lvitem.iIndent = indentCount;
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.LVM_SETITEM, 0, ref lvitem);
			}
		}

		// Token: 0x06002DB9 RID: 11705 RVA: 0x000CE5D4 File Offset: 0x000CC7D4
		internal void SetItemPosition(int index, int x, int y)
		{
			if (this.VirtualMode)
			{
				return;
			}
			if (index < 0 || index >= this.itemCount)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			NativeMethods.POINT point = new NativeMethods.POINT();
			point.x = x;
			point.y = y;
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4145, index, point);
		}

		// Token: 0x06002DBA RID: 11706 RVA: 0x000CE658 File Offset: 0x000CC858
		internal void SetItemState(int index, int state, int mask)
		{
			if (index < -1 || (this.VirtualMode && index >= this.VirtualListSize) || (!this.VirtualMode && index >= this.itemCount))
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (base.IsHandleCreated)
			{
				NativeMethods.LVITEM lvitem = default(NativeMethods.LVITEM);
				lvitem.mask = 8;
				lvitem.state = state;
				lvitem.stateMask = mask;
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4139, index, ref lvitem);
			}
		}

		// Token: 0x06002DBB RID: 11707 RVA: 0x000CE700 File Offset: 0x000CC900
		internal void SetItemText(int itemIndex, int subItemIndex, string text)
		{
			NativeMethods.LVITEM lvitem = default(NativeMethods.LVITEM);
			this.SetItemText(itemIndex, subItemIndex, text, ref lvitem);
		}

		// Token: 0x06002DBC RID: 11708 RVA: 0x000CE720 File Offset: 0x000CC920
		private void SetItemText(int itemIndex, int subItemIndex, string text, ref NativeMethods.LVITEM lvItem)
		{
			if (this.View == View.List && subItemIndex == 0)
			{
				int num = (int)(long)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4125, 0, 0);
				Graphics graphics = base.CreateGraphicsInternal();
				int num2 = 0;
				try
				{
					num2 = Size.Ceiling(graphics.MeasureString(text, this.Font)).Width;
				}
				finally
				{
					graphics.Dispose();
				}
				if (num2 > num)
				{
					this.SetColumnWidth(0, num2);
				}
			}
			lvItem.mask = 1;
			lvItem.iItem = itemIndex;
			lvItem.iSubItem = subItemIndex;
			lvItem.pszText = text;
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.LVM_SETITEMTEXT, itemIndex, ref lvItem);
		}

		// Token: 0x06002DBD RID: 11709 RVA: 0x000CE7DC File Offset: 0x000CC9DC
		internal void SetSelectionMark(int itemIndex)
		{
			if (itemIndex < 0 || itemIndex >= this.Items.Count)
			{
				return;
			}
			base.SendMessage(4163, 0, itemIndex);
		}

		// Token: 0x06002DBE RID: 11710 RVA: 0x000CE800 File Offset: 0x000CCA00
		private void SmallImageListRecreateHandle(object sender, EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				IntPtr intPtr = ((this.SmallImageList == null) ? IntPtr.Zero : this.SmallImageList.Handle);
				base.SendMessage(4099, (IntPtr)1, intPtr);
				this.ForceCheckBoxUpdate();
			}
		}

		/// <summary>Sorts the items of the list view.</summary>
		// Token: 0x06002DBF RID: 11711 RVA: 0x000CE84C File Offset: 0x000CCA4C
		public void Sort()
		{
			if (this.VirtualMode)
			{
				throw new InvalidOperationException(SR.GetString("ListViewSortNotAllowedInVirtualListView"));
			}
			this.ApplyUpdateCachedItems();
			if (base.IsHandleCreated && this.listItemSorter != null)
			{
				NativeMethods.ListViewCompareCallback listViewCompareCallback = new NativeMethods.ListViewCompareCallback(this.CompareFunc);
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4144, IntPtr.Zero, listViewCompareCallback);
			}
		}

		// Token: 0x06002DC0 RID: 11712 RVA: 0x000CE8B4 File Offset: 0x000CCAB4
		private void StateImageListRecreateHandle(object sender, EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				IntPtr intPtr = IntPtr.Zero;
				if (this.StateImageList != null)
				{
					intPtr = this.imageListState.Handle;
				}
				base.SendMessage(4099, (IntPtr)2, intPtr);
			}
		}

		/// <summary>Returns a string representation of the <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
		/// <returns>A string that states the control type, the count of items in the <see cref="T:System.Windows.Forms.ListView" /> control, and the type of the first item in the <see cref="T:System.Windows.Forms.ListView" />, if the count is not 0.</returns>
		// Token: 0x06002DC1 RID: 11713 RVA: 0x000CE8F8 File Offset: 0x000CCAF8
		public override string ToString()
		{
			string text = base.ToString();
			if (this.listItemsArray != null)
			{
				text = text + ", Items.Count: " + this.listItemsArray.Count.ToString(CultureInfo.CurrentCulture);
				if (this.listItemsArray.Count > 0)
				{
					string text2 = this.listItemsArray[0].ToString();
					string text3 = ((text2.Length > 40) ? text2.Substring(0, 40) : text2);
					text = text + ", Items[0]: " + text3;
				}
			}
			else if (this.Items != null)
			{
				text = text + ", Items.Count: " + this.Items.Count.ToString(CultureInfo.CurrentCulture);
				if (this.Items.Count > 0 && !this.VirtualMode)
				{
					string text4 = ((this.Items[0] == null) ? "null" : this.Items[0].ToString());
					string text5 = ((text4.Length > 40) ? text4.Substring(0, 40) : text4);
					text = text + ", Items[0]: " + text5;
				}
			}
			return text;
		}

		// Token: 0x06002DC2 RID: 11714 RVA: 0x000CEA1C File Offset: 0x000CCC1C
		internal void UpdateListViewItemsLocations()
		{
			if (!this.VirtualMode && base.IsHandleCreated && this.AutoArrange && (this.View == View.LargeIcon || this.View == View.SmallIcon))
			{
				try
				{
					this.BeginUpdate();
					base.SendMessage(4138, -1, 0);
				}
				finally
				{
					this.EndUpdate();
				}
			}
		}

		// Token: 0x06002DC3 RID: 11715 RVA: 0x000CEA80 File Offset: 0x000CCC80
		private void UpdateColumnWidths(ColumnHeaderAutoResizeStyle headerAutoResize)
		{
			if (this.columnHeaders != null)
			{
				for (int i = 0; i < this.columnHeaders.Length; i++)
				{
					this.SetColumnWidth(i, headerAutoResize);
				}
			}
		}

		/// <summary>Updates the extended styles applied to the list view control.</summary>
		// Token: 0x06002DC4 RID: 11716 RVA: 0x000CEAB0 File Offset: 0x000CCCB0
		protected void UpdateExtendedStyles()
		{
			if (base.IsHandleCreated)
			{
				int num = 0;
				int num2 = 68861;
				ItemActivation itemActivation = this.activation;
				if (itemActivation != ItemActivation.OneClick)
				{
					if (itemActivation == ItemActivation.TwoClick)
					{
						num |= 128;
					}
				}
				else
				{
					num |= 64;
				}
				if (this.AllowColumnReorder)
				{
					num |= 16;
				}
				if (this.CheckBoxes)
				{
					num |= 4;
				}
				if (this.DoubleBuffered)
				{
					num |= 65536;
				}
				if (this.FullRowSelect)
				{
					num |= 32;
				}
				if (this.GridLines)
				{
					num |= 1;
				}
				if (this.HoverSelection)
				{
					num |= 8;
				}
				if (this.HotTracking)
				{
					num |= 2048;
				}
				if (this.ShowItemToolTips)
				{
					num |= 1024;
				}
				base.SendMessage(4150, num2, num);
				base.Invalidate();
			}
		}

		// Token: 0x06002DC5 RID: 11717 RVA: 0x000CEB74 File Offset: 0x000CCD74
		internal void UpdateGroupNative(ListViewGroup group)
		{
			NativeMethods.LVGROUP lvgroup = new NativeMethods.LVGROUP();
			try
			{
				lvgroup = this.GetLVGROUP(group);
				int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4243, group.ID, lvgroup);
			}
			finally
			{
				this.DestroyLVGROUP(lvgroup);
			}
			base.Invalidate();
		}

		// Token: 0x06002DC6 RID: 11718 RVA: 0x000CEBD4 File Offset: 0x000CCDD4
		internal void UpdateGroupView()
		{
			if (base.IsHandleCreated && this.ComctlSupportsVisualStyles && !this.VirtualMode)
			{
				int num = (int)(long)base.SendMessage(4253, this.GroupsEnabled ? 1 : 0, 0);
			}
		}

		// Token: 0x06002DC7 RID: 11719 RVA: 0x000CEC18 File Offset: 0x000CCE18
		private void UpdateTileView()
		{
			NativeMethods.LVTILEVIEWINFO lvtileviewinfo = new NativeMethods.LVTILEVIEWINFO();
			lvtileviewinfo.dwMask = 2;
			lvtileviewinfo.cLines = ((this.columnHeaders != null) ? this.columnHeaders.Length : 0);
			lvtileviewinfo.dwMask |= 1;
			lvtileviewinfo.dwFlags = 3;
			lvtileviewinfo.sizeTile = new NativeMethods.SIZE(this.TileSize.Width, this.TileSize.Height);
			bool flag = UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4258, 0, lvtileviewinfo);
		}

		// Token: 0x06002DC8 RID: 11720 RVA: 0x000CECA0 File Offset: 0x000CCEA0
		private void WmNmClick(ref Message m)
		{
			if (this.CheckBoxes)
			{
				Point point = Cursor.Position;
				point = base.PointToClientInternal(point);
				NativeMethods.LVHITTESTINFO lvhittestinfo = new NativeMethods.LVHITTESTINFO();
				lvhittestinfo.pt_x = point.X;
				lvhittestinfo.pt_y = point.Y;
				int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4114, 0, lvhittestinfo);
				if (num != -1 && (lvhittestinfo.flags & 8) != 0)
				{
					ListViewItem listViewItem = this.Items[num];
					if (listViewItem.Selected)
					{
						bool flag = !listViewItem.Checked;
						if (!this.VirtualMode)
						{
							foreach (object obj in this.SelectedItems)
							{
								ListViewItem listViewItem2 = (ListViewItem)obj;
								if (listViewItem2 != listViewItem)
								{
									listViewItem2.Checked = flag;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06002DC9 RID: 11721 RVA: 0x000CED98 File Offset: 0x000CCF98
		private void WmNmDblClick(ref Message m)
		{
			if (this.CheckBoxes)
			{
				Point point = Cursor.Position;
				point = base.PointToClientInternal(point);
				NativeMethods.LVHITTESTINFO lvhittestinfo = new NativeMethods.LVHITTESTINFO();
				lvhittestinfo.pt_x = point.X;
				lvhittestinfo.pt_y = point.Y;
				int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4114, 0, lvhittestinfo);
				if (num != -1 && (lvhittestinfo.flags & 14) != 0)
				{
					ListViewItem listViewItem = this.Items[num];
					listViewItem.Checked = !listViewItem.Checked;
				}
			}
		}

		// Token: 0x06002DCA RID: 11722 RVA: 0x000CEE24 File Offset: 0x000CD024
		private void WmMouseDown(ref Message m, MouseButtons button, int clicks)
		{
			this.listViewState[524288] = false;
			this.listViewState[1048576] = true;
			this.FocusInternal();
			int num = NativeMethods.Util.SignedLOWORD(m.LParam);
			int num2 = NativeMethods.Util.SignedHIWORD(m.LParam);
			this.OnMouseDown(new MouseEventArgs(button, clicks, num, num2, 0));
			if (!base.ValidationCancelled)
			{
				if (this.CheckBoxes)
				{
					ListViewHitTestInfo listViewHitTestInfo = this.HitTest(num, num2);
					if (this.imageListState == null || this.imageListState.Images.Count >= 2)
					{
						if (AccessibilityImprovements.Level2 && listViewHitTestInfo.Item != null && listViewHitTestInfo.Location == ListViewHitTestLocations.StateImage)
						{
							listViewHitTestInfo.Item.Focused = true;
						}
						this.DefWndProc(ref m);
						return;
					}
					if (listViewHitTestInfo.Location != ListViewHitTestLocations.StateImage)
					{
						this.DefWndProc(ref m);
						return;
					}
				}
				else
				{
					this.DefWndProc(ref m);
				}
			}
		}

		// Token: 0x06002DCB RID: 11723 RVA: 0x000CEF04 File Offset: 0x000CD104
		private unsafe bool WmNotify(ref Message m)
		{
			NativeMethods.NMHDR* ptr = (NativeMethods.NMHDR*)(void*)m.LParam;
			if (ptr->code == -12 && this.OwnerDraw)
			{
				try
				{
					NativeMethods.NMCUSTOMDRAW* ptr2 = (NativeMethods.NMCUSTOMDRAW*)(void*)m.LParam;
					int dwDrawStage = ptr2->dwDrawStage;
					if (dwDrawStage == 1)
					{
						m.Result = (IntPtr)32;
						return true;
					}
					if (dwDrawStage != 65537)
					{
						return false;
					}
					Graphics graphics = Graphics.FromHdcInternal(ptr2->hdc);
					Rectangle rectangle = Rectangle.FromLTRB(ptr2->rc.left, ptr2->rc.top, ptr2->rc.right, ptr2->rc.bottom);
					DrawListViewColumnHeaderEventArgs drawListViewColumnHeaderEventArgs = null;
					try
					{
						Color color = ColorTranslator.FromWin32(SafeNativeMethods.GetTextColor(new HandleRef(this, ptr2->hdc)));
						Color color2 = ColorTranslator.FromWin32(SafeNativeMethods.GetBkColor(new HandleRef(this, ptr2->hdc)));
						Font listHeaderFont = this.GetListHeaderFont();
						drawListViewColumnHeaderEventArgs = new DrawListViewColumnHeaderEventArgs(graphics, rectangle, (int)ptr2->dwItemSpec, this.columnHeaders[(int)ptr2->dwItemSpec], (ListViewItemStates)ptr2->uItemState, color, color2, listHeaderFont);
						this.OnDrawColumnHeader(drawListViewColumnHeaderEventArgs);
					}
					finally
					{
						graphics.Dispose();
					}
					if (drawListViewColumnHeaderEventArgs.DrawDefault)
					{
						m.Result = (IntPtr)0;
						return false;
					}
					m.Result = (IntPtr)4;
					return true;
				}
				catch (Exception ex)
				{
					m.Result = (IntPtr)0;
				}
			}
			if (ptr->code == -16 && this.listViewState[131072])
			{
				this.listViewState[131072] = false;
				this.OnColumnClick(new ColumnClickEventArgs(this.columnIndex));
			}
			if (ptr->code == -306 || ptr->code == -326)
			{
				this.listViewState[67108864] = true;
				this.listViewState1[2] = false;
				this.newWidthForColumnWidthChangingCancelled = -1;
				this.listViewState1[2] = false;
				NativeMethods.NMHEADER nmheader = (NativeMethods.NMHEADER)m.GetLParam(typeof(NativeMethods.NMHEADER));
				if (this.columnHeaders != null && this.columnHeaders.Length > nmheader.iItem)
				{
					this.columnHeaderClicked = this.columnHeaders[nmheader.iItem];
					this.columnHeaderClickedWidth = this.columnHeaderClicked.Width;
				}
				else
				{
					this.columnHeaderClickedWidth = -1;
					this.columnHeaderClicked = null;
				}
			}
			if (ptr->code == -300 || ptr->code == -320)
			{
				NativeMethods.NMHEADER nmheader2 = (NativeMethods.NMHEADER)m.GetLParam(typeof(NativeMethods.NMHEADER));
				if (this.columnHeaders != null && nmheader2.iItem < this.columnHeaders.Length && (this.listViewState[67108864] || this.listViewState[536870912]))
				{
					NativeMethods.HDITEM2 hditem = (NativeMethods.HDITEM2)UnsafeNativeMethods.PtrToStructure(nmheader2.pItem, typeof(NativeMethods.HDITEM2));
					int num = (((hditem.mask & 1) != 0) ? hditem.cxy : (-1));
					ColumnWidthChangingEventArgs columnWidthChangingEventArgs = new ColumnWidthChangingEventArgs(nmheader2.iItem, num);
					this.OnColumnWidthChanging(columnWidthChangingEventArgs);
					m.Result = (IntPtr)(columnWidthChangingEventArgs.Cancel ? 1 : 0);
					if (columnWidthChangingEventArgs.Cancel)
					{
						hditem.cxy = columnWidthChangingEventArgs.NewWidth;
						if (this.listViewState[536870912])
						{
							this.listViewState[1073741824] = true;
						}
						this.listViewState1[2] = true;
						this.newWidthForColumnWidthChangingCancelled = columnWidthChangingEventArgs.NewWidth;
						return true;
					}
					return false;
				}
			}
			if ((ptr->code == -301 || ptr->code == -321) && !this.listViewState[67108864])
			{
				NativeMethods.NMHEADER nmheader3 = (NativeMethods.NMHEADER)m.GetLParam(typeof(NativeMethods.NMHEADER));
				if (this.columnHeaders != null && nmheader3.iItem < this.columnHeaders.Length)
				{
					int width = this.columnHeaders[nmheader3.iItem].Width;
					if (this.columnHeaderClicked == null || (this.columnHeaderClicked == this.columnHeaders[nmheader3.iItem] && this.columnHeaderClickedWidth != -1 && this.columnHeaderClickedWidth != width))
					{
						if (this.listViewState[536870912])
						{
							if (this.CompensateColumnHeaderResize(m, this.listViewState[1073741824]) == 0)
							{
								this.OnColumnWidthChanged(new ColumnWidthChangedEventArgs(nmheader3.iItem));
							}
						}
						else
						{
							this.OnColumnWidthChanged(new ColumnWidthChangedEventArgs(nmheader3.iItem));
						}
					}
				}
				this.columnHeaderClicked = null;
				this.columnHeaderClickedWidth = -1;
				ISite site = this.Site;
				if (site != null)
				{
					IComponentChangeService componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
					if (componentChangeService != null)
					{
						try
						{
							componentChangeService.OnComponentChanging(this, null);
						}
						catch (CheckoutException ex2)
						{
							if (ex2 == CheckoutException.Canceled)
							{
								return false;
							}
							throw ex2;
						}
					}
				}
			}
			if (ptr->code == -307 || ptr->code == -327)
			{
				this.listViewState[67108864] = false;
				if (this.listViewState1[2])
				{
					m.Result = (IntPtr)1;
					if (this.newWidthForColumnWidthChangingCancelled != -1)
					{
						NativeMethods.NMHEADER nmheader4 = (NativeMethods.NMHEADER)m.GetLParam(typeof(NativeMethods.NMHEADER));
						if (this.columnHeaders != null && this.columnHeaders.Length > nmheader4.iItem)
						{
							this.columnHeaders[nmheader4.iItem].Width = this.newWidthForColumnWidthChangingCancelled;
						}
					}
					this.listViewState1[2] = false;
					this.newWidthForColumnWidthChangingCancelled = -1;
					return true;
				}
				return false;
			}
			else
			{
				if (ptr->code == -311)
				{
					NativeMethods.NMHEADER nmheader5 = (NativeMethods.NMHEADER)m.GetLParam(typeof(NativeMethods.NMHEADER));
					if (nmheader5.pItem != IntPtr.Zero)
					{
						NativeMethods.HDITEM2 hditem2 = (NativeMethods.HDITEM2)UnsafeNativeMethods.PtrToStructure(nmheader5.pItem, typeof(NativeMethods.HDITEM2));
						if ((hditem2.mask & 128) == 128)
						{
							int displayIndex = this.Columns[nmheader5.iItem].DisplayIndex;
							int iOrder = hditem2.iOrder;
							if (displayIndex == iOrder)
							{
								return false;
							}
							if (iOrder < 0)
							{
								return false;
							}
							ColumnReorderedEventArgs columnReorderedEventArgs = new ColumnReorderedEventArgs(displayIndex, iOrder, this.Columns[nmheader5.iItem]);
							this.OnColumnReordered(columnReorderedEventArgs);
							if (columnReorderedEventArgs.Cancel)
							{
								m.Result = new IntPtr(1);
								return true;
							}
							int num2 = Math.Min(displayIndex, iOrder);
							int num3 = Math.Max(displayIndex, iOrder);
							bool flag = iOrder > displayIndex;
							ColumnHeader columnHeader = null;
							int[] array = new int[this.Columns.Count];
							for (int i = 0; i < this.Columns.Count; i++)
							{
								ColumnHeader columnHeader2 = this.Columns[i];
								if (columnHeader2.DisplayIndex == displayIndex)
								{
									columnHeader = columnHeader2;
								}
								else if (columnHeader2.DisplayIndex >= num2 && columnHeader2.DisplayIndex <= num3)
								{
									columnHeader2.DisplayIndexInternal -= (flag ? 1 : (-1));
								}
								array[i] = columnHeader2.DisplayIndexInternal;
							}
							columnHeader.DisplayIndexInternal = iOrder;
							array[columnHeader.Index] = columnHeader.DisplayIndexInternal;
							this.SetDisplayIndices(array);
						}
					}
				}
				if (ptr->code == -305 || ptr->code == -325)
				{
					this.listViewState[536870912] = true;
					this.listViewState[1073741824] = false;
					bool flag2 = false;
					try
					{
						this.DefWndProc(ref m);
					}
					finally
					{
						this.listViewState[536870912] = false;
						flag2 = this.listViewState[1073741824];
						this.listViewState[1073741824] = false;
					}
					this.columnHeaderClicked = null;
					this.columnHeaderClickedWidth = -1;
					if (flag2)
					{
						if (this.newWidthForColumnWidthChangingCancelled != -1)
						{
							NativeMethods.NMHEADER nmheader6 = (NativeMethods.NMHEADER)m.GetLParam(typeof(NativeMethods.NMHEADER));
							if (this.columnHeaders != null && this.columnHeaders.Length > nmheader6.iItem)
							{
								this.columnHeaders[nmheader6.iItem].Width = this.newWidthForColumnWidthChangingCancelled;
							}
						}
						m.Result = (IntPtr)1;
					}
					else
					{
						int num4 = this.CompensateColumnHeaderResize(m, flag2);
						if (num4 != 0)
						{
							ColumnHeader columnHeader3 = this.columnHeaders[0];
							columnHeader3.Width += num4;
						}
					}
					return true;
				}
				return false;
			}
			bool flag3;
			return flag3;
		}

		// Token: 0x06002DCC RID: 11724 RVA: 0x000CF7D8 File Offset: 0x000CD9D8
		private Font GetListHeaderFont()
		{
			IntPtr intPtr = UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4127, 0, 0);
			IntPtr intPtr2 = UnsafeNativeMethods.SendMessage(new HandleRef(this, intPtr), 49, 0, 0);
			IntSecurity.ObjectFromWin32Handle.Assert();
			return Font.FromHfont(intPtr2);
		}

		// Token: 0x06002DCD RID: 11725 RVA: 0x000CF820 File Offset: 0x000CDA20
		private int GetIndexOfClickedItem(NativeMethods.LVHITTESTINFO lvhi)
		{
			Point point = Cursor.Position;
			point = base.PointToClientInternal(point);
			lvhi.pt_x = point.X;
			lvhi.pt_y = point.Y;
			return (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4114, 0, lvhi);
		}

		// Token: 0x06002DCE RID: 11726 RVA: 0x000CF872 File Offset: 0x000CDA72
		internal void RecreateHandleInternal()
		{
			if (base.IsHandleCreated && this.StateImageList != null)
			{
				base.SendMessage(4099, 2, IntPtr.Zero);
			}
			base.RecreateHandle();
		}

		// Token: 0x06002DCF RID: 11727 RVA: 0x000CF89C File Offset: 0x000CDA9C
		private unsafe void WmReflectNotify(ref Message m)
		{
			NativeMethods.NMHDR* ptr = (NativeMethods.NMHDR*)(void*)m.LParam;
			int code = ptr->code;
			if (code <= -155)
			{
				if (code == -176)
				{
					goto IL_158;
				}
				if (code != -175)
				{
					if (code != -155)
					{
						goto IL_655;
					}
					if (!this.CheckBoxes)
					{
						return;
					}
					NativeMethods.NMLVKEYDOWN nmlvkeydown = (NativeMethods.NMLVKEYDOWN)m.GetLParam(typeof(NativeMethods.NMLVKEYDOWN));
					if (nmlvkeydown.wVKey != 32)
					{
						return;
					}
					ListViewItem focusedItem = this.FocusedItem;
					if (focusedItem == null)
					{
						return;
					}
					bool flag = !focusedItem.Checked;
					if (!this.VirtualMode)
					{
						using (IEnumerator enumerator = this.SelectedItems.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								object obj = enumerator.Current;
								ListViewItem listViewItem = (ListViewItem)obj;
								if (listViewItem != focusedItem)
								{
									listViewItem.Checked = flag;
								}
							}
							return;
						}
						goto IL_624;
					}
					return;
				}
			}
			else
			{
				switch (code)
				{
				case -114:
					this.OnItemActivate(EventArgs.Empty);
					return;
				case -113:
					goto IL_624;
				case -112:
				case -110:
				case -107:
				case -104:
				case -103:
				case -102:
					goto IL_655;
				case -111:
					if (!this.ItemCollectionChangedInMouseDown)
					{
						NativeMethods.NMLISTVIEW nmlistview = (NativeMethods.NMLISTVIEW)m.GetLParam(typeof(NativeMethods.NMLISTVIEW));
						this.LvnBeginDrag(MouseButtons.Right, nmlistview);
						return;
					}
					return;
				case -109:
					if (!this.ItemCollectionChangedInMouseDown)
					{
						NativeMethods.NMLISTVIEW nmlistview2 = (NativeMethods.NMLISTVIEW)m.GetLParam(typeof(NativeMethods.NMLISTVIEW));
						this.LvnBeginDrag(MouseButtons.Left, nmlistview2);
						return;
					}
					return;
				case -108:
				{
					NativeMethods.NMLISTVIEW nmlistview3 = (NativeMethods.NMLISTVIEW)m.GetLParam(typeof(NativeMethods.NMLISTVIEW));
					this.listViewState[131072] = true;
					this.columnIndex = nmlistview3.iSubItem;
					return;
				}
				case -106:
					goto IL_158;
				case -105:
					break;
				case -101:
				{
					NativeMethods.NMLISTVIEW* ptr2 = (NativeMethods.NMLISTVIEW*)(void*)m.LParam;
					if ((ptr2->uChanged & 8) == 0)
					{
						return;
					}
					CheckState checkState = (((ptr2->uOldState & 61440) >> 12 == 1) ? CheckState.Unchecked : CheckState.Checked);
					CheckState checkState2 = (((ptr2->uNewState & 61440) >> 12 == 1) ? CheckState.Unchecked : CheckState.Checked);
					if (checkState2 != checkState)
					{
						ItemCheckedEventArgs itemCheckedEventArgs = new ItemCheckedEventArgs(this.Items[ptr2->iItem]);
						this.OnItemChecked(itemCheckedEventArgs);
						if (AccessibilityImprovements.Level1)
						{
							base.AccessibilityNotifyClients(AccessibleEvents.StateChange, ptr2->iItem);
							base.AccessibilityNotifyClients(AccessibleEvents.NameChange, ptr2->iItem);
						}
					}
					int num = ptr2->uOldState & 2;
					int num2 = ptr2->uNewState & 2;
					if (num2 == num)
					{
						return;
					}
					if (this.VirtualMode && ptr2->iItem == -1)
					{
						if (this.VirtualListSize > 0)
						{
							ListViewVirtualItemsSelectionRangeChangedEventArgs listViewVirtualItemsSelectionRangeChangedEventArgs = new ListViewVirtualItemsSelectionRangeChangedEventArgs(0, this.VirtualListSize - 1, num2 != 0);
							this.OnVirtualItemsSelectionRangeChanged(listViewVirtualItemsSelectionRangeChangedEventArgs);
						}
					}
					else if (this.Items.Count > 0)
					{
						ListViewItemSelectionChangedEventArgs listViewItemSelectionChangedEventArgs = new ListViewItemSelectionChangedEventArgs(this.Items[ptr2->iItem], ptr2->iItem, num2 != 0);
						this.OnItemSelectionChanged(listViewItemSelectionChangedEventArgs);
					}
					if (this.Items.Count == 0 || this.Items[this.Items.Count - 1] != null)
					{
						this.listViewState1[16] = false;
						this.OnSelectedIndexChanged(EventArgs.Empty);
						return;
					}
					this.listViewState1[16] = true;
					return;
				}
				case -100:
				{
					NativeMethods.NMLISTVIEW* ptr3 = (NativeMethods.NMLISTVIEW*)(void*)m.LParam;
					if ((ptr3->uChanged & 8) == 0)
					{
						return;
					}
					CheckState checkState3 = (((ptr3->uOldState & 61440) >> 12 == 1) ? CheckState.Unchecked : CheckState.Checked);
					CheckState checkState4 = (((ptr3->uNewState & 61440) >> 12 == 1) ? CheckState.Unchecked : CheckState.Checked);
					if (checkState3 != checkState4)
					{
						ItemCheckEventArgs itemCheckEventArgs = new ItemCheckEventArgs(ptr3->iItem, checkState4, checkState3);
						this.OnItemCheck(itemCheckEventArgs);
						m.Result = (IntPtr)((((itemCheckEventArgs.NewValue == CheckState.Unchecked) ? CheckState.Unchecked : CheckState.Checked) == checkState3) ? 1 : 0);
						return;
					}
					return;
				}
				default:
					if (code != -12)
					{
						switch (code)
						{
						case -6:
							goto IL_53F;
						case -5:
							break;
						case -4:
							goto IL_655;
						case -3:
							this.WmNmDblClick(ref m);
							goto IL_53F;
						case -2:
							this.WmNmClick(ref m);
							break;
						default:
							goto IL_655;
						}
						NativeMethods.LVHITTESTINFO lvhittestinfo = new NativeMethods.LVHITTESTINFO();
						int indexOfClickedItem = this.GetIndexOfClickedItem(lvhittestinfo);
						MouseButtons mouseButtons = ((ptr->code == -2) ? MouseButtons.Left : MouseButtons.Right);
						Point point = Cursor.Position;
						point = base.PointToClientInternal(point);
						if (!base.ValidationCancelled && indexOfClickedItem != -1)
						{
							this.OnClick(EventArgs.Empty);
							this.OnMouseClick(new MouseEventArgs(mouseButtons, 1, point.X, point.Y, 0));
						}
						if (!this.listViewState[524288])
						{
							this.OnMouseUp(new MouseEventArgs(mouseButtons, 1, point.X, point.Y, 0));
							this.listViewState[524288] = true;
							return;
						}
						return;
						IL_53F:
						NativeMethods.LVHITTESTINFO lvhittestinfo2 = new NativeMethods.LVHITTESTINFO();
						int indexOfClickedItem2 = this.GetIndexOfClickedItem(lvhittestinfo2);
						if (indexOfClickedItem2 != -1)
						{
							this.listViewState[262144] = true;
						}
						this.listViewState[524288] = false;
						base.CaptureInternal = true;
						return;
					}
					this.CustomDraw(ref m);
					return;
				}
			}
			NativeMethods.NMLVDISPINFO_NOTEXT nmlvdispinfo_NOTEXT = (NativeMethods.NMLVDISPINFO_NOTEXT)m.GetLParam(typeof(NativeMethods.NMLVDISPINFO_NOTEXT));
			LabelEditEventArgs labelEditEventArgs = new LabelEditEventArgs(nmlvdispinfo_NOTEXT.item.iItem);
			this.OnBeforeLabelEdit(labelEditEventArgs);
			m.Result = (IntPtr)(labelEditEventArgs.CancelEdit ? 1 : 0);
			this.listViewState[16384] = !labelEditEventArgs.CancelEdit;
			return;
			IL_158:
			this.listViewState[16384] = false;
			NativeMethods.NMLVDISPINFO nmlvdispinfo = (NativeMethods.NMLVDISPINFO)m.GetLParam(typeof(NativeMethods.NMLVDISPINFO));
			LabelEditEventArgs labelEditEventArgs2 = new LabelEditEventArgs(nmlvdispinfo.item.iItem, nmlvdispinfo.item.pszText);
			this.OnAfterLabelEdit(labelEditEventArgs2);
			m.Result = (IntPtr)(labelEditEventArgs2.CancelEdit ? 0 : 1);
			if (!labelEditEventArgs2.CancelEdit && nmlvdispinfo.item.pszText != null)
			{
				this.Items[nmlvdispinfo.item.iItem].Text = nmlvdispinfo.item.pszText;
				return;
			}
			return;
			IL_624:
			NativeMethods.NMLVCACHEHINT nmlvcachehint = (NativeMethods.NMLVCACHEHINT)m.GetLParam(typeof(NativeMethods.NMLVCACHEHINT));
			this.OnCacheVirtualItems(new CacheVirtualItemsEventArgs(nmlvcachehint.iFrom, nmlvcachehint.iTo));
			return;
			IL_655:
			if (ptr->code == NativeMethods.LVN_GETDISPINFO)
			{
				if (this.VirtualMode && m.LParam != IntPtr.Zero)
				{
					NativeMethods.NMLVDISPINFO_NOTEXT nmlvdispinfo_NOTEXT2 = (NativeMethods.NMLVDISPINFO_NOTEXT)m.GetLParam(typeof(NativeMethods.NMLVDISPINFO_NOTEXT));
					RetrieveVirtualItemEventArgs retrieveVirtualItemEventArgs = new RetrieveVirtualItemEventArgs(nmlvdispinfo_NOTEXT2.item.iItem);
					this.OnRetrieveVirtualItem(retrieveVirtualItemEventArgs);
					ListViewItem item = retrieveVirtualItemEventArgs.Item;
					if (item == null)
					{
						throw new InvalidOperationException(SR.GetString("ListViewVirtualItemRequired"));
					}
					item.SetItemIndex(this, nmlvdispinfo_NOTEXT2.item.iItem);
					if ((nmlvdispinfo_NOTEXT2.item.mask & 1) != 0)
					{
						string text;
						if (nmlvdispinfo_NOTEXT2.item.iSubItem == 0)
						{
							text = item.Text;
						}
						else
						{
							if (item.SubItems.Count <= nmlvdispinfo_NOTEXT2.item.iSubItem)
							{
								throw new InvalidOperationException(SR.GetString("ListViewVirtualModeCantAccessSubItem"));
							}
							text = item.SubItems[nmlvdispinfo_NOTEXT2.item.iSubItem].Text;
						}
						if (nmlvdispinfo_NOTEXT2.item.cchTextMax <= text.Length)
						{
							text = text.Substring(0, nmlvdispinfo_NOTEXT2.item.cchTextMax - 1);
						}
						if (Marshal.SystemDefaultCharSize == 1)
						{
							byte[] bytes = Encoding.Default.GetBytes(text + "\0");
							Marshal.Copy(bytes, 0, nmlvdispinfo_NOTEXT2.item.pszText, text.Length + 1);
						}
						else
						{
							char[] array = (text + "\0").ToCharArray();
							Marshal.Copy(array, 0, nmlvdispinfo_NOTEXT2.item.pszText, text.Length + 1);
						}
					}
					if ((nmlvdispinfo_NOTEXT2.item.mask & 2) != 0 && item.ImageIndex != -1)
					{
						nmlvdispinfo_NOTEXT2.item.iImage = item.ImageIndex;
					}
					if ((nmlvdispinfo_NOTEXT2.item.mask & 16) != 0)
					{
						nmlvdispinfo_NOTEXT2.item.iIndent = item.IndentCount;
					}
					if ((nmlvdispinfo_NOTEXT2.item.stateMask & 61440) != 0)
					{
						NativeMethods.NMLVDISPINFO_NOTEXT nmlvdispinfo_NOTEXT3 = nmlvdispinfo_NOTEXT2;
						nmlvdispinfo_NOTEXT3.item.state = nmlvdispinfo_NOTEXT3.item.state | item.RawStateImageIndex;
					}
					Marshal.StructureToPtr(nmlvdispinfo_NOTEXT2, m.LParam, false);
					return;
				}
			}
			else if (ptr->code == -115)
			{
				if (this.VirtualMode && m.LParam != IntPtr.Zero)
				{
					NativeMethods.NMLVODSTATECHANGE nmlvodstatechange = (NativeMethods.NMLVODSTATECHANGE)m.GetLParam(typeof(NativeMethods.NMLVODSTATECHANGE));
					bool flag2 = (nmlvodstatechange.uNewState & 2) != (nmlvodstatechange.uOldState & 2);
					if (flag2)
					{
						int num3 = nmlvodstatechange.iTo;
						if (!UnsafeNativeMethods.IsVista)
						{
							num3--;
						}
						ListViewVirtualItemsSelectionRangeChangedEventArgs listViewVirtualItemsSelectionRangeChangedEventArgs2 = new ListViewVirtualItemsSelectionRangeChangedEventArgs(nmlvodstatechange.iFrom, num3, (nmlvodstatechange.uNewState & 2) != 0);
						this.OnVirtualItemsSelectionRangeChanged(listViewVirtualItemsSelectionRangeChangedEventArgs2);
						return;
					}
				}
			}
			else if (ptr->code == NativeMethods.LVN_GETINFOTIP)
			{
				if (this.ShowItemToolTips && m.LParam != IntPtr.Zero)
				{
					NativeMethods.NMLVGETINFOTIP nmlvgetinfotip = (NativeMethods.NMLVGETINFOTIP)m.GetLParam(typeof(NativeMethods.NMLVGETINFOTIP));
					ListViewItem listViewItem2 = this.Items[nmlvgetinfotip.item];
					if (listViewItem2 != null && !string.IsNullOrEmpty(listViewItem2.ToolTipText))
					{
						UnsafeNativeMethods.SendMessage(new HandleRef(this, ptr->hwndFrom), 1048, 0, SystemInformation.MaxWindowTrackSize.Width);
						if (Marshal.SystemDefaultCharSize == 1)
						{
							byte[] bytes2 = Encoding.Default.GetBytes(listViewItem2.ToolTipText + "\0");
							Marshal.Copy(bytes2, 0, nmlvgetinfotip.lpszText, Math.Min(bytes2.Length, nmlvgetinfotip.cchTextMax));
						}
						else
						{
							char[] array2 = (listViewItem2.ToolTipText + "\0").ToCharArray();
							Marshal.Copy(array2, 0, nmlvgetinfotip.lpszText, Math.Min(array2.Length, nmlvgetinfotip.cchTextMax));
						}
						Marshal.StructureToPtr(nmlvgetinfotip, m.LParam, false);
						return;
					}
				}
			}
			else if (ptr->code == NativeMethods.LVN_ODFINDITEM && this.VirtualMode)
			{
				NativeMethods.NMLVFINDITEM nmlvfinditem = (NativeMethods.NMLVFINDITEM)m.GetLParam(typeof(NativeMethods.NMLVFINDITEM));
				if ((nmlvfinditem.lvfi.flags & 1) != 0)
				{
					m.Result = (IntPtr)(-1);
					return;
				}
				bool flag3 = (nmlvfinditem.lvfi.flags & 2) != 0 || (nmlvfinditem.lvfi.flags & 8) != 0;
				bool flag4 = (nmlvfinditem.lvfi.flags & 8) != 0;
				string text2 = string.Empty;
				if (flag3)
				{
					text2 = nmlvfinditem.lvfi.psz;
				}
				Point empty = Point.Empty;
				if ((nmlvfinditem.lvfi.flags & 64) != 0)
				{
					empty = new Point(nmlvfinditem.lvfi.ptX, nmlvfinditem.lvfi.ptY);
				}
				SearchDirectionHint searchDirectionHint = SearchDirectionHint.Down;
				if ((nmlvfinditem.lvfi.flags & 64) != 0)
				{
					searchDirectionHint = (SearchDirectionHint)nmlvfinditem.lvfi.vkDirection;
				}
				int iStart = nmlvfinditem.iStart;
				if (iStart >= this.VirtualListSize)
				{
				}
				SearchForVirtualItemEventArgs searchForVirtualItemEventArgs = new SearchForVirtualItemEventArgs(flag3, flag4, false, text2, empty, searchDirectionHint, nmlvfinditem.iStart);
				this.OnSearchForVirtualItem(searchForVirtualItemEventArgs);
				if (searchForVirtualItemEventArgs.Index != -1)
				{
					m.Result = (IntPtr)searchForVirtualItemEventArgs.Index;
					return;
				}
				m.Result = (IntPtr)(-1);
			}
		}

		// Token: 0x06002DD0 RID: 11728 RVA: 0x000D045C File Offset: 0x000CE65C
		private void WmPrint(ref Message m)
		{
			base.WndProc(ref m);
			if ((2 & (int)m.LParam) != 0 && Application.RenderWithVisualStyles && this.BorderStyle == BorderStyle.Fixed3D)
			{
				IntSecurity.UnmanagedCode.Assert();
				try
				{
					using (Graphics graphics = Graphics.FromHdc(m.WParam))
					{
						Rectangle rectangle = new Rectangle(0, 0, base.Size.Width - 1, base.Size.Height - 1);
						graphics.DrawRectangle(new Pen(VisualStyleInformation.TextControlBorder), rectangle);
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

		/// <summary>Overrides <see cref="M:System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@)" />.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x06002DD1 RID: 11729 RVA: 0x000D0528 File Offset: 0x000CE728
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg <= 275)
			{
				if (msg <= 15)
				{
					if (msg != 7)
					{
						if (msg == 15)
						{
							base.WndProc(ref m);
							base.BeginInvoke(new MethodInvoker(this.CleanPreviousBackgroundImageFiles));
							return;
						}
					}
					else
					{
						base.WndProc(ref m);
						if (!base.RecreatingHandle && !this.ListViewHandleDestroyed && this.FocusedItem == null && this.Items.Count > 0)
						{
							this.Items[0].Focused = true;
							return;
						}
						return;
					}
				}
				else if (msg != 78)
				{
					if (msg == 275)
					{
						if ((int)(long)m.WParam != 48 || !this.ComctlSupportsVisualStyles)
						{
							base.WndProc(ref m);
							return;
						}
						return;
					}
				}
				else if (this.WmNotify(ref m))
				{
					return;
				}
			}
			else if (msg <= 673)
			{
				switch (msg)
				{
				case 512:
					if (this.listViewState[1048576] && !this.listViewState[524288] && Control.MouseButtons == MouseButtons.None)
					{
						this.OnMouseUp(new MouseEventArgs(this.downButton, 1, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
						this.listViewState[524288] = true;
					}
					base.CaptureInternal = false;
					base.WndProc(ref m);
					return;
				case 513:
					this.ItemCollectionChangedInMouseDown = false;
					this.WmMouseDown(ref m, MouseButtons.Left, 1);
					this.downButton = MouseButtons.Left;
					return;
				case 514:
				case 517:
				case 520:
				{
					NativeMethods.LVHITTESTINFO lvhittestinfo = new NativeMethods.LVHITTESTINFO();
					int indexOfClickedItem = this.GetIndexOfClickedItem(lvhittestinfo);
					if (!base.ValidationCancelled && this.listViewState[262144] && indexOfClickedItem != -1)
					{
						this.listViewState[262144] = false;
						this.OnDoubleClick(EventArgs.Empty);
						this.OnMouseDoubleClick(new MouseEventArgs(this.downButton, 2, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
					}
					if (!this.listViewState[524288])
					{
						this.OnMouseUp(new MouseEventArgs(this.downButton, 1, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
						this.listViewState[1048576] = false;
					}
					this.ItemCollectionChangedInMouseDown = false;
					this.listViewState[524288] = true;
					base.CaptureInternal = false;
					return;
				}
				case 515:
					this.ItemCollectionChangedInMouseDown = false;
					base.CaptureInternal = true;
					this.WmMouseDown(ref m, MouseButtons.Left, 2);
					return;
				case 516:
					this.WmMouseDown(ref m, MouseButtons.Right, 1);
					this.downButton = MouseButtons.Right;
					return;
				case 518:
					this.WmMouseDown(ref m, MouseButtons.Right, 2);
					return;
				case 519:
					this.WmMouseDown(ref m, MouseButtons.Middle, 1);
					this.downButton = MouseButtons.Middle;
					return;
				case 521:
					this.WmMouseDown(ref m, MouseButtons.Middle, 2);
					return;
				default:
					if (msg == 673)
					{
						if (this.HoverSelection)
						{
							base.WndProc(ref m);
							return;
						}
						this.OnMouseHover(EventArgs.Empty);
						return;
					}
					break;
				}
			}
			else
			{
				if (msg == 675)
				{
					this.prevHoveredItem = null;
					base.WndProc(ref m);
					return;
				}
				if (msg == 791)
				{
					this.WmPrint(ref m);
					return;
				}
				if (msg == 8270)
				{
					this.WmReflectNotify(ref m);
					return;
				}
			}
			base.WndProc(ref m);
		}

		/// <summary>Creates a new instance of the accessibility object for the <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Control.ControlAccessibleObject" /> for this <see cref="T:System.Windows.Forms.ListView" /> control.</returns>
		// Token: 0x06002DD2 RID: 11730 RVA: 0x000D088D File Offset: 0x000CEA8D
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level3)
			{
				return new ListView.ListViewAccessibleObject(this);
			}
			return base.CreateAccessibilityInstance();
		}

		// Token: 0x0400128D RID: 4749
		private const int MASK_HITTESTFLAG = 247;

		// Token: 0x0400128E RID: 4750
		private static readonly object EVENT_CACHEVIRTUALITEMS = new object();

		// Token: 0x0400128F RID: 4751
		private static readonly object EVENT_COLUMNREORDERED = new object();

		// Token: 0x04001290 RID: 4752
		private static readonly object EVENT_COLUMNWIDTHCHANGED = new object();

		// Token: 0x04001291 RID: 4753
		private static readonly object EVENT_COLUMNWIDTHCHANGING = new object();

		// Token: 0x04001292 RID: 4754
		private static readonly object EVENT_DRAWCOLUMNHEADER = new object();

		// Token: 0x04001293 RID: 4755
		private static readonly object EVENT_DRAWITEM = new object();

		// Token: 0x04001294 RID: 4756
		private static readonly object EVENT_DRAWSUBITEM = new object();

		// Token: 0x04001295 RID: 4757
		private static readonly object EVENT_ITEMSELECTIONCHANGED = new object();

		// Token: 0x04001296 RID: 4758
		private static readonly object EVENT_RETRIEVEVIRTUALITEM = new object();

		// Token: 0x04001297 RID: 4759
		private static readonly object EVENT_SEARCHFORVIRTUALITEM = new object();

		// Token: 0x04001298 RID: 4760
		private static readonly object EVENT_SELECTEDINDEXCHANGED = new object();

		// Token: 0x04001299 RID: 4761
		private static readonly object EVENT_VIRTUALITEMSSELECTIONRANGECHANGED = new object();

		// Token: 0x0400129A RID: 4762
		private static readonly object EVENT_RIGHTTOLEFTLAYOUTCHANGED = new object();

		// Token: 0x0400129B RID: 4763
		private ItemActivation activation;

		// Token: 0x0400129C RID: 4764
		private ListViewAlignment alignStyle = ListViewAlignment.Top;

		// Token: 0x0400129D RID: 4765
		private BorderStyle borderStyle = BorderStyle.Fixed3D;

		// Token: 0x0400129E RID: 4766
		private ColumnHeaderStyle headerStyle = ColumnHeaderStyle.Clickable;

		// Token: 0x0400129F RID: 4767
		private SortOrder sorting;

		// Token: 0x040012A0 RID: 4768
		private View viewStyle;

		// Token: 0x040012A1 RID: 4769
		private string toolTipCaption = string.Empty;

		// Token: 0x040012A2 RID: 4770
		private const int LISTVIEWSTATE_ownerDraw = 1;

		// Token: 0x040012A3 RID: 4771
		private const int LISTVIEWSTATE_allowColumnReorder = 2;

		// Token: 0x040012A4 RID: 4772
		private const int LISTVIEWSTATE_autoArrange = 4;

		// Token: 0x040012A5 RID: 4773
		private const int LISTVIEWSTATE_checkBoxes = 8;

		// Token: 0x040012A6 RID: 4774
		private const int LISTVIEWSTATE_fullRowSelect = 16;

		// Token: 0x040012A7 RID: 4775
		private const int LISTVIEWSTATE_gridLines = 32;

		// Token: 0x040012A8 RID: 4776
		private const int LISTVIEWSTATE_hideSelection = 64;

		// Token: 0x040012A9 RID: 4777
		private const int LISTVIEWSTATE_hotTracking = 128;

		// Token: 0x040012AA RID: 4778
		private const int LISTVIEWSTATE_labelEdit = 256;

		// Token: 0x040012AB RID: 4779
		private const int LISTVIEWSTATE_labelWrap = 512;

		// Token: 0x040012AC RID: 4780
		private const int LISTVIEWSTATE_multiSelect = 1024;

		// Token: 0x040012AD RID: 4781
		private const int LISTVIEWSTATE_scrollable = 2048;

		// Token: 0x040012AE RID: 4782
		private const int LISTVIEWSTATE_hoverSelection = 4096;

		// Token: 0x040012AF RID: 4783
		private const int LISTVIEWSTATE_nonclickHdr = 8192;

		// Token: 0x040012B0 RID: 4784
		private const int LISTVIEWSTATE_inLabelEdit = 16384;

		// Token: 0x040012B1 RID: 4785
		private const int LISTVIEWSTATE_showItemToolTips = 32768;

		// Token: 0x040012B2 RID: 4786
		private const int LISTVIEWSTATE_backgroundImageTiled = 65536;

		// Token: 0x040012B3 RID: 4787
		private const int LISTVIEWSTATE_columnClicked = 131072;

		// Token: 0x040012B4 RID: 4788
		private const int LISTVIEWSTATE_doubleclickFired = 262144;

		// Token: 0x040012B5 RID: 4789
		private const int LISTVIEWSTATE_mouseUpFired = 524288;

		// Token: 0x040012B6 RID: 4790
		private const int LISTVIEWSTATE_expectingMouseUp = 1048576;

		// Token: 0x040012B7 RID: 4791
		private const int LISTVIEWSTATE_comctlSupportsVisualStyles = 2097152;

		// Token: 0x040012B8 RID: 4792
		private const int LISTVIEWSTATE_comctlSupportsVisualStylesTested = 4194304;

		// Token: 0x040012B9 RID: 4793
		private const int LISTVIEWSTATE_showGroups = 8388608;

		// Token: 0x040012BA RID: 4794
		private const int LISTVIEWSTATE_handleDestroyed = 16777216;

		// Token: 0x040012BB RID: 4795
		private const int LISTVIEWSTATE_virtualMode = 33554432;

		// Token: 0x040012BC RID: 4796
		private const int LISTVIEWSTATE_headerControlTracking = 67108864;

		// Token: 0x040012BD RID: 4797
		private const int LISTVIEWSTATE_itemCollectionChangedInMouseDown = 134217728;

		// Token: 0x040012BE RID: 4798
		private const int LISTVIEWSTATE_flipViewToLargeIconAndSmallIcon = 268435456;

		// Token: 0x040012BF RID: 4799
		private const int LISTVIEWSTATE_headerDividerDblClick = 536870912;

		// Token: 0x040012C0 RID: 4800
		private const int LISTVIEWSTATE_columnResizeCancelled = 1073741824;

		// Token: 0x040012C1 RID: 4801
		private const int LISTVIEWSTATE1_insertingItemsNatively = 1;

		// Token: 0x040012C2 RID: 4802
		private const int LISTVIEWSTATE1_cancelledColumnWidthChanging = 2;

		// Token: 0x040012C3 RID: 4803
		private const int LISTVIEWSTATE1_disposingImageLists = 4;

		// Token: 0x040012C4 RID: 4804
		private const int LISTVIEWSTATE1_useCompatibleStateImageBehavior = 8;

		// Token: 0x040012C5 RID: 4805
		private const int LISTVIEWSTATE1_selectedIndexChangedSkipped = 16;

		// Token: 0x040012C6 RID: 4806
		private const int LVTOOLTIPTRACKING = 48;

		// Token: 0x040012C7 RID: 4807
		private const int MAXTILECOLUMNS = 20;

		// Token: 0x040012C8 RID: 4808
		private BitVector32 listViewState;

		// Token: 0x040012C9 RID: 4809
		private BitVector32 listViewState1;

		// Token: 0x040012CA RID: 4810
		private Color odCacheForeColor = SystemColors.WindowText;

		// Token: 0x040012CB RID: 4811
		private Color odCacheBackColor = SystemColors.Window;

		// Token: 0x040012CC RID: 4812
		private Font odCacheFont;

		// Token: 0x040012CD RID: 4813
		private IntPtr odCacheFontHandle = IntPtr.Zero;

		// Token: 0x040012CE RID: 4814
		private Control.FontHandleWrapper odCacheFontHandleWrapper;

		// Token: 0x040012CF RID: 4815
		private ImageList imageListLarge;

		// Token: 0x040012D0 RID: 4816
		private ImageList imageListSmall;

		// Token: 0x040012D1 RID: 4817
		private ImageList imageListState;

		// Token: 0x040012D2 RID: 4818
		private MouseButtons downButton;

		// Token: 0x040012D3 RID: 4819
		private int itemCount;

		// Token: 0x040012D4 RID: 4820
		private int columnIndex;

		// Token: 0x040012D5 RID: 4821
		private int topIndex;

		// Token: 0x040012D6 RID: 4822
		private bool hoveredAlready;

		// Token: 0x040012D7 RID: 4823
		private bool rightToLeftLayout;

		// Token: 0x040012D8 RID: 4824
		private int virtualListSize;

		// Token: 0x040012D9 RID: 4825
		private ListViewGroup defaultGroup;

		// Token: 0x040012DA RID: 4826
		private Hashtable listItemsTable = new Hashtable();

		// Token: 0x040012DB RID: 4827
		private ArrayList listItemsArray = new ArrayList();

		// Token: 0x040012DC RID: 4828
		private Size tileSize = Size.Empty;

		// Token: 0x040012DD RID: 4829
		private static readonly int PropDelayedUpdateItems = PropertyStore.CreateKey();

		// Token: 0x040012DE RID: 4830
		private int updateCounter;

		// Token: 0x040012DF RID: 4831
		private ColumnHeader[] columnHeaders;

		// Token: 0x040012E0 RID: 4832
		private ListView.ListViewItemCollection listItemCollection;

		// Token: 0x040012E1 RID: 4833
		private ListView.ColumnHeaderCollection columnHeaderCollection;

		// Token: 0x040012E2 RID: 4834
		private ListView.CheckedIndexCollection checkedIndexCollection;

		// Token: 0x040012E3 RID: 4835
		private ListView.CheckedListViewItemCollection checkedListViewItemCollection;

		// Token: 0x040012E4 RID: 4836
		private ListView.SelectedListViewItemCollection selectedListViewItemCollection;

		// Token: 0x040012E5 RID: 4837
		private ListView.SelectedIndexCollection selectedIndexCollection;

		// Token: 0x040012E6 RID: 4838
		private ListViewGroupCollection groups;

		// Token: 0x040012E7 RID: 4839
		private ListViewInsertionMark insertionMark;

		// Token: 0x040012E8 RID: 4840
		private LabelEditEventHandler onAfterLabelEdit;

		// Token: 0x040012E9 RID: 4841
		private LabelEditEventHandler onBeforeLabelEdit;

		// Token: 0x040012EA RID: 4842
		private ColumnClickEventHandler onColumnClick;

		// Token: 0x040012EB RID: 4843
		private EventHandler onItemActivate;

		// Token: 0x040012EC RID: 4844
		private ItemCheckedEventHandler onItemChecked;

		// Token: 0x040012ED RID: 4845
		private ItemDragEventHandler onItemDrag;

		// Token: 0x040012EE RID: 4846
		private ItemCheckEventHandler onItemCheck;

		// Token: 0x040012EF RID: 4847
		private ListViewItemMouseHoverEventHandler onItemMouseHover;

		// Token: 0x040012F0 RID: 4848
		private int nextID;

		// Token: 0x040012F1 RID: 4849
		private List<ListViewItem> savedSelectedItems;

		// Token: 0x040012F2 RID: 4850
		private List<ListViewItem> savedCheckedItems;

		// Token: 0x040012F3 RID: 4851
		private IComparer listItemSorter;

		// Token: 0x040012F4 RID: 4852
		private ListViewItem prevHoveredItem;

		// Token: 0x040012F5 RID: 4853
		private string backgroundImageFileName = string.Empty;

		// Token: 0x040012F6 RID: 4854
		private int bkImgFileNamesCount = -1;

		// Token: 0x040012F7 RID: 4855
		private string[] bkImgFileNames;

		// Token: 0x040012F8 RID: 4856
		private const int BKIMGARRAYSIZE = 8;

		// Token: 0x040012F9 RID: 4857
		private ColumnHeader columnHeaderClicked;

		// Token: 0x040012FA RID: 4858
		private int columnHeaderClickedWidth;

		// Token: 0x040012FB RID: 4859
		private int newWidthForColumnWidthChangingCancelled = -1;

		// Token: 0x020006C4 RID: 1732
		internal class IconComparer : IComparer
		{
			// Token: 0x0600699E RID: 27038 RVA: 0x00187582 File Offset: 0x00185782
			public IconComparer(SortOrder currentSortOrder)
			{
				this.sortOrder = currentSortOrder;
			}

			// Token: 0x170016DD RID: 5853
			// (set) Token: 0x0600699F RID: 27039 RVA: 0x00187591 File Offset: 0x00185791
			public SortOrder SortOrder
			{
				set
				{
					this.sortOrder = value;
				}
			}

			// Token: 0x060069A0 RID: 27040 RVA: 0x0018759C File Offset: 0x0018579C
			public int Compare(object obj1, object obj2)
			{
				ListViewItem listViewItem = (ListViewItem)obj1;
				ListViewItem listViewItem2 = (ListViewItem)obj2;
				if (this.sortOrder == SortOrder.Ascending)
				{
					return string.Compare(listViewItem.Text, listViewItem2.Text, false, CultureInfo.CurrentCulture);
				}
				return string.Compare(listViewItem2.Text, listViewItem.Text, false, CultureInfo.CurrentCulture);
			}

			// Token: 0x04003B2E RID: 15150
			private SortOrder sortOrder;
		}

		/// <summary>Represents the collection containing the indexes to the checked items in a list view control.</summary>
		// Token: 0x020006C5 RID: 1733
		[ListBindable(false)]
		public class CheckedIndexCollection : IList, ICollection, IEnumerable
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" /> class.</summary>
			/// <param name="owner">A <see cref="T:System.Windows.Forms.ListView" /> control that owns the collection.</param>
			// Token: 0x060069A1 RID: 27041 RVA: 0x001875EF File Offset: 0x001857EF
			public CheckedIndexCollection(ListView owner)
			{
				this.owner = owner;
			}

			/// <summary>Gets the number of items in the collection.</summary>
			/// <returns>The number of items in the collection.</returns>
			// Token: 0x170016DE RID: 5854
			// (get) Token: 0x060069A2 RID: 27042 RVA: 0x00187600 File Offset: 0x00185800
			[Browsable(false)]
			public int Count
			{
				get
				{
					if (!this.owner.CheckBoxes)
					{
						return 0;
					}
					int num = 0;
					foreach (object obj in this.owner.Items)
					{
						ListViewItem listViewItem = (ListViewItem)obj;
						if (listViewItem != null && listViewItem.Checked)
						{
							num++;
						}
					}
					return num;
				}
			}

			// Token: 0x170016DF RID: 5855
			// (get) Token: 0x060069A3 RID: 27043 RVA: 0x00187678 File Offset: 0x00185878
			private int[] IndicesArray
			{
				get
				{
					int[] array = new int[this.Count];
					int num = 0;
					int num2 = 0;
					while (num2 < this.owner.Items.Count && num < array.Length)
					{
						if (this.owner.Items[num2].Checked)
						{
							array[num++] = num2;
						}
						num2++;
					}
					return array;
				}
			}

			/// <summary>Gets the index value at the specified index within the collection.</summary>
			/// <param name="index">The index of the item in the collection to retrieve.</param>
			/// <returns>The index value from the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> that is stored at the specified location.</returns>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than 0 or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListView.CheckedIndexCollection.Count" /> property of <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" />.</exception>
			// Token: 0x170016E0 RID: 5856
			public int this[int index]
			{
				get
				{
					if (index < 0)
					{
						throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
					int count = this.owner.Items.Count;
					int num = 0;
					for (int i = 0; i < count; i++)
					{
						ListViewItem listViewItem = this.owner.Items[i];
						if (listViewItem.Checked)
						{
							if (num == index)
							{
								return i;
							}
							num++;
						}
					}
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
			}

			/// <summary>Gets or sets an object in the <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" />.</summary>
			/// <param name="index">The zero-based index of the element to get or set.</param>
			/// <returns>The object from the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> that is stored at the specified location.</returns>
			// Token: 0x170016E1 RID: 5857
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection of controls.</summary>
			/// <returns>The object used to synchronize the collection.</returns>
			// Token: 0x170016E2 RID: 5858
			// (get) Token: 0x060069A7 RID: 27047 RVA: 0x00006A49 File Offset: 0x00004C49
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
			/// <returns>
			///   <see langword="false" /> in all cases.</returns>
			// Token: 0x170016E3 RID: 5859
			// (get) Token: 0x060069A8 RID: 27048 RVA: 0x0001180C File Offset: 0x0000FA0C
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" /> has a fixed size.</summary>
			/// <returns>
			///   <see langword="true" /> in all cases.</returns>
			// Token: 0x170016E4 RID: 5860
			// (get) Token: 0x060069A9 RID: 27049 RVA: 0x00012E4E File Offset: 0x0001104E
			bool IList.IsFixedSize
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>
			///   <see langword="true" /> if the collection is read-only; otherwise, <see langword="false" />.</returns>
			// Token: 0x170016E5 RID: 5861
			// (get) Token: 0x060069AA RID: 27050 RVA: 0x00012E4E File Offset: 0x0001104E
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			/// <summary>Determines whether the specified index is located in the collection.</summary>
			/// <param name="checkedIndex">The index to locate in the collection.</param>
			/// <returns>
			///   <see langword="true" /> if the specified index from the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> for the <see cref="T:System.Windows.Forms.ListView" /> is an item in the collection; otherwise, <see langword="false" />.</returns>
			// Token: 0x060069AB RID: 27051 RVA: 0x0018779D File Offset: 0x0018599D
			public bool Contains(int checkedIndex)
			{
				return this.owner.Items[checkedIndex].Checked;
			}

			/// <summary>Checks whether the index corresponding with the <see cref="T:System.Windows.Forms.ListViewItem" /> is checked.</summary>
			/// <param name="checkedIndex">An index to locate in the <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" />.</param>
			/// <returns>
			///   <see langword="true" /> if the index is found in the <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" />; otherwise, <see langword="false" />.</returns>
			// Token: 0x060069AC RID: 27052 RVA: 0x001877BA File Offset: 0x001859BA
			bool IList.Contains(object checkedIndex)
			{
				return checkedIndex is int && this.Contains((int)checkedIndex);
			}

			/// <summary>Returns the index within the <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" /> of the specified index from the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> of the list view control.</summary>
			/// <param name="checkedIndex">The zero-based index from the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> to locate in the collection.</param>
			/// <returns>The zero-based index in the collection where the specified index of the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> is located within the <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" />; otherwise, -1 if the index is not located in the collection.</returns>
			// Token: 0x060069AD RID: 27053 RVA: 0x001877D4 File Offset: 0x001859D4
			public int IndexOf(int checkedIndex)
			{
				int[] indicesArray = this.IndicesArray;
				for (int i = 0; i < indicesArray.Length; i++)
				{
					if (indicesArray[i] == checkedIndex)
					{
						return i;
					}
				}
				return -1;
			}

			/// <summary>Returns the index of the specified object in the <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" />.</summary>
			/// <param name="checkedIndex">The zero-based index from the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> to locate in the collection.</param>
			/// <returns>The zero-based index in the collection where the specified index of the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> is located if it is in the <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" />; otherwise, -1.</returns>
			// Token: 0x060069AE RID: 27054 RVA: 0x001877FF File Offset: 0x001859FF
			int IList.IndexOf(object checkedIndex)
			{
				if (checkedIndex is int)
				{
					return this.IndexOf((int)checkedIndex);
				}
				return -1;
			}

			/// <summary>Adds an item to the collection.</summary>
			/// <param name="value">The object to add to the <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" />.</param>
			/// <returns>The zero-based index where <paramref name="value" /> is located in the collection.</returns>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x060069AF RID: 27055 RVA: 0x0000A337 File Offset: 0x00008537
			int IList.Add(object value)
			{
				throw new NotSupportedException();
			}

			/// <summary>Removes all items from the collection.</summary>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x060069B0 RID: 27056 RVA: 0x0000A337 File Offset: 0x00008537
			void IList.Clear()
			{
				throw new NotSupportedException();
			}

			/// <summary>Inserts an item into the collection at a specified index.</summary>
			/// <param name="index">The index at which <paramref name="value" /> should be inserted.</param>
			/// <param name="value">The object to be added to the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x060069B1 RID: 27057 RVA: 0x0000A337 File Offset: 0x00008537
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException();
			}

			/// <summary>Removes the first occurrence of an item from the collection.</summary>
			/// <param name="value">The object to be removed from the <see cref="T:System.Windows.Forms.ListView.CheckedIndexCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x060069B2 RID: 27058 RVA: 0x0000A337 File Offset: 0x00008537
			void IList.Remove(object value)
			{
				throw new NotSupportedException();
			}

			/// <summary>Removes an item from the collection at a specified index.</summary>
			/// <param name="index">The zero-based index of the item to remove.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x060069B3 RID: 27059 RVA: 0x0000A337 File Offset: 0x00008537
			void IList.RemoveAt(int index)
			{
				throw new NotSupportedException();
			}

			/// <summary>Copies the collection of checked-item indexes into an array.</summary>
			/// <param name="dest">An array of type <see cref="T:System.Int32" />.</param>
			/// <param name="index">The zero-based index in the array at which copying begins.</param>
			/// <exception cref="T:System.ArrayTypeMismatchException">The array type cannot be cast to an <see cref="T:System.Int32" />.</exception>
			// Token: 0x060069B4 RID: 27060 RVA: 0x00187817 File Offset: 0x00185A17
			void ICollection.CopyTo(Array dest, int index)
			{
				if (this.Count > 0)
				{
					Array.Copy(this.IndicesArray, 0, dest, index, this.Count);
				}
			}

			/// <summary>Returns an enumerator that can be used to iterate through the checked index collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the checked index collection.</returns>
			// Token: 0x060069B5 RID: 27061 RVA: 0x00187838 File Offset: 0x00185A38
			public IEnumerator GetEnumerator()
			{
				int[] indicesArray = this.IndicesArray;
				if (indicesArray != null)
				{
					return indicesArray.GetEnumerator();
				}
				return new int[0].GetEnumerator();
			}

			// Token: 0x04003B2F RID: 15151
			private ListView owner;
		}

		/// <summary>Represents the collection of checked items in a list view control.</summary>
		// Token: 0x020006C6 RID: 1734
		[ListBindable(false)]
		public class CheckedListViewItemCollection : IList, ICollection, IEnumerable
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListView.CheckedListViewItemCollection" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.ListView" /> control that owns the collection.</param>
			// Token: 0x060069B6 RID: 27062 RVA: 0x00187861 File Offset: 0x00185A61
			public CheckedListViewItemCollection(ListView owner)
			{
				this.owner = owner;
			}

			/// <summary>Gets the number of items in the collection.</summary>
			/// <returns>The number of items in the collection.</returns>
			// Token: 0x170016E6 RID: 5862
			// (get) Token: 0x060069B7 RID: 27063 RVA: 0x00187877 File Offset: 0x00185A77
			[Browsable(false)]
			public int Count
			{
				get
				{
					if (this.owner.VirtualMode)
					{
						throw new InvalidOperationException(SR.GetString("ListViewCantAccessCheckedItemsCollectionWhenInVirtualMode"));
					}
					return this.owner.CheckedIndices.Count;
				}
			}

			// Token: 0x170016E7 RID: 5863
			// (get) Token: 0x060069B8 RID: 27064 RVA: 0x001878A8 File Offset: 0x00185AA8
			private ListViewItem[] ItemArray
			{
				get
				{
					ListViewItem[] array = new ListViewItem[this.Count];
					int num = 0;
					int num2 = 0;
					while (num2 < this.owner.Items.Count && num < array.Length)
					{
						if (this.owner.Items[num2].Checked)
						{
							array[num++] = this.owner.Items[num2];
						}
						num2++;
					}
					return array;
				}
			}

			/// <summary>Gets the item at the specified index within the collection.</summary>
			/// <param name="index">The index of the item in the collection to retrieve.</param>
			/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem" /> representing the item located at the specified index within the collection.</returns>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than 0 or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListView.CheckedListViewItemCollection.Count" /> property of <see cref="T:System.Windows.Forms.ListView.CheckedListViewItemCollection" />.</exception>
			/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Windows.Forms.ListView" /> is in virtual mode.</exception>
			// Token: 0x170016E8 RID: 5864
			public ListViewItem this[int index]
			{
				get
				{
					if (this.owner.VirtualMode)
					{
						throw new InvalidOperationException(SR.GetString("ListViewCantAccessCheckedItemsCollectionWhenInVirtualMode"));
					}
					int num = this.owner.CheckedIndices[index];
					return this.owner.Items[num];
				}
			}

			/// <summary>Gets or sets an object from the collection.</summary>
			/// <param name="index">The zero-based index of the element to get or set.</param>
			/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the item located at the specified index within the collection.</returns>
			/// <exception cref="T:System.NotSupportedException">This property cannot be set.</exception>
			// Token: 0x170016E9 RID: 5865
			object IList.this[int index]
			{
				get
				{
					if (this.owner.VirtualMode)
					{
						throw new InvalidOperationException(SR.GetString("ListViewCantAccessCheckedItemsCollectionWhenInVirtualMode"));
					}
					return this[index];
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			/// <summary>Gets an item with the specified key within the collection.</summary>
			/// <param name="key">The key of the item in the collection to retrieve.</param>
			/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem" /> representing the item with the specified index within the collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The owner <see cref="T:System.Windows.Forms.ListView" /> is in virtual mode.</exception>
			// Token: 0x170016EA RID: 5866
			public virtual ListViewItem this[string key]
			{
				get
				{
					if (this.owner.VirtualMode)
					{
						throw new InvalidOperationException(SR.GetString("ListViewCantAccessCheckedItemsCollectionWhenInVirtualMode"));
					}
					if (string.IsNullOrEmpty(key))
					{
						return null;
					}
					int num = this.IndexOfKey(key);
					if (this.IsValidIndex(num))
					{
						return this[num];
					}
					return null;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection of controls.</summary>
			/// <returns>The object used to synchronize the collection.</returns>
			// Token: 0x170016EB RID: 5867
			// (get) Token: 0x060069BD RID: 27069 RVA: 0x00006A49 File Offset: 0x00004C49
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>Gets a value indicating whether access to the <see cref="T:System.Windows.Forms.ListView.CheckedListViewItemCollection" /> is synchronized (thread safe).</summary>
			/// <returns>
			///   <see langword="false" /> in all cases.</returns>
			// Token: 0x170016EC RID: 5868
			// (get) Token: 0x060069BE RID: 27070 RVA: 0x0001180C File Offset: 0x0000FA0C
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
			/// <returns>
			///   <see langword="true" /> in all cases.</returns>
			// Token: 0x170016ED RID: 5869
			// (get) Token: 0x060069BF RID: 27071 RVA: 0x00012E4E File Offset: 0x0001104E
			bool IList.IsFixedSize
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>
			///   <see langword="true" /> if the collection is read-only; otherwise, <see langword="false" />.</returns>
			// Token: 0x170016EE RID: 5870
			// (get) Token: 0x060069C0 RID: 27072 RVA: 0x00012E4E File Offset: 0x0001104E
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			/// <summary>Determines whether the specified item is located in the collection.</summary>
			/// <param name="item">A <see cref="T:System.Windows.Forms.ListViewItem" /> representing the item to locate in the collection.</param>
			/// <returns>
			///   <see langword="true" /> if the specified item is located in the collection; otherwise, <see langword="false" />.</returns>
			// Token: 0x060069C1 RID: 27073 RVA: 0x001879DA File Offset: 0x00185BDA
			public bool Contains(ListViewItem item)
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantAccessCheckedItemsCollectionWhenInVirtualMode"));
				}
				return item != null && item.ListView == this.owner && item.Checked;
			}

			/// <summary>Verifies whether the item is checked.</summary>
			/// <param name="item">A <see cref="T:System.Windows.Forms.ListViewItem" /> to locate in the <see cref="T:System.Windows.Forms.ListView.CheckedListViewItemCollection" />.</param>
			/// <returns>
			///   <see langword="true" /> if item is found in the <see cref="T:System.Windows.Forms.ListView.CheckedListViewItemCollection" />; otherwise, <see langword="false" />.</returns>
			// Token: 0x060069C2 RID: 27074 RVA: 0x00187A15 File Offset: 0x00185C15
			bool IList.Contains(object item)
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantAccessCheckedItemsCollectionWhenInVirtualMode"));
				}
				return item is ListViewItem && this.Contains((ListViewItem)item);
			}

			/// <summary>Determines if a column with the specified key is contained in the collection.</summary>
			/// <param name="key">The name of the item to search for.</param>
			/// <returns>
			///   <see langword="true" /> if an item with the specified key is contained in the collection; otherwise, <see langword="false." /></returns>
			/// <exception cref="T:System.InvalidOperationException">The owner <see cref="T:System.Windows.Forms.ListView" /> is in virtual mode.</exception>
			// Token: 0x060069C3 RID: 27075 RVA: 0x00187A4A File Offset: 0x00185C4A
			public virtual bool ContainsKey(string key)
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantAccessCheckedItemsCollectionWhenInVirtualMode"));
				}
				return this.IsValidIndex(this.IndexOfKey(key));
			}

			/// <summary>Returns the index within the collection of the specified item.</summary>
			/// <param name="item">A <see cref="T:System.Windows.Forms.ListViewItem" /> representing the item to locate in the collection.</param>
			/// <returns>The zero-based index of the item in the collection; otherwise, -1.</returns>
			// Token: 0x060069C4 RID: 27076 RVA: 0x00187A78 File Offset: 0x00185C78
			public int IndexOf(ListViewItem item)
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantAccessCheckedItemsCollectionWhenInVirtualMode"));
				}
				ListViewItem[] itemArray = this.ItemArray;
				for (int i = 0; i < itemArray.Length; i++)
				{
					if (itemArray[i] == item)
					{
						return i;
					}
				}
				return -1;
			}

			/// <summary>Determines the index for an item with the specified key.</summary>
			/// <param name="key">The name of the item to retrieve the index for.</param>
			/// <returns>The zero-based index for the <see cref="T:System.Windows.Forms.ListViewItem" /> with the specified name, if found; otherwise, -1.</returns>
			/// <exception cref="T:System.InvalidOperationException">The owner <see cref="T:System.Windows.Forms.ListView" /> is in virtual mode.</exception>
			// Token: 0x060069C5 RID: 27077 RVA: 0x00187AC0 File Offset: 0x00185CC0
			public virtual int IndexOfKey(string key)
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantAccessCheckedItemsCollectionWhenInVirtualMode"));
				}
				if (string.IsNullOrEmpty(key))
				{
					return -1;
				}
				if (this.IsValidIndex(this.lastAccessedIndex) && WindowsFormsUtils.SafeCompareStrings(this[this.lastAccessedIndex].Name, key, true))
				{
					return this.lastAccessedIndex;
				}
				for (int i = 0; i < this.Count; i++)
				{
					if (WindowsFormsUtils.SafeCompareStrings(this[i].Name, key, true))
					{
						this.lastAccessedIndex = i;
						return i;
					}
				}
				this.lastAccessedIndex = -1;
				return -1;
			}

			// Token: 0x060069C6 RID: 27078 RVA: 0x00187B5A File Offset: 0x00185D5A
			private bool IsValidIndex(int index)
			{
				return index >= 0 && index < this.Count;
			}

			/// <summary>Returns the index within the collection of the specified item.</summary>
			/// <param name="item">A <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the item to locate in the collection.</param>
			/// <returns>The zero-based index of the item if it is in the collection; otherwise, -1.</returns>
			// Token: 0x060069C7 RID: 27079 RVA: 0x00187B6B File Offset: 0x00185D6B
			int IList.IndexOf(object item)
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantAccessCheckedItemsCollectionWhenInVirtualMode"));
				}
				if (item is ListViewItem)
				{
					return this.IndexOf((ListViewItem)item);
				}
				return -1;
			}

			/// <summary>Adds an item to the collection.</summary>
			/// <param name="value">The item to add to the collection.</param>
			/// <returns>The zero-based index where value is located in the collection.</returns>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x060069C8 RID: 27080 RVA: 0x0000A337 File Offset: 0x00008537
			int IList.Add(object value)
			{
				throw new NotSupportedException();
			}

			/// <summary>Removes all items from the collection.</summary>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x060069C9 RID: 27081 RVA: 0x0000A337 File Offset: 0x00008537
			void IList.Clear()
			{
				throw new NotSupportedException();
			}

			/// <summary>Inserts an item into the collection at a specified index.</summary>
			/// <param name="index">The index at which value should be inserted.</param>
			/// <param name="value">The object to be added to the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x060069CA RID: 27082 RVA: 0x0000A337 File Offset: 0x00008537
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException();
			}

			/// <summary>Removes the first occurrence of an item from the collection.</summary>
			/// <param name="value">The object to be removed from the <see cref="T:System.Windows.Forms.ListView.CheckedListViewItemCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x060069CB RID: 27083 RVA: 0x0000A337 File Offset: 0x00008537
			void IList.Remove(object value)
			{
				throw new NotSupportedException();
			}

			/// <summary>Removes an item from the collection at the specified index.</summary>
			/// <param name="index">The zero-based index of the item to remove.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x060069CC RID: 27084 RVA: 0x0000A337 File Offset: 0x00008537
			void IList.RemoveAt(int index)
			{
				throw new NotSupportedException();
			}

			/// <summary>Copies the entire collection into an existing array at a specified location within the array.</summary>
			/// <param name="dest">An <see cref="T:System.Array" /> representing the array to copy the contents of the collection to.</param>
			/// <param name="index">The location within the destination array to copy the items from the collection to.</param>
			// Token: 0x060069CD RID: 27085 RVA: 0x00187BA0 File Offset: 0x00185DA0
			public void CopyTo(Array dest, int index)
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantAccessCheckedItemsCollectionWhenInVirtualMode"));
				}
				if (this.Count > 0)
				{
					Array.Copy(this.ItemArray, 0, dest, index, this.Count);
				}
			}

			/// <summary>Returns an enumerator that can be used to iterate through the checked item collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the checked item collection.</returns>
			// Token: 0x060069CE RID: 27086 RVA: 0x00187BDC File Offset: 0x00185DDC
			public IEnumerator GetEnumerator()
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantAccessCheckedItemsCollectionWhenInVirtualMode"));
				}
				ListViewItem[] itemArray = this.ItemArray;
				if (itemArray != null)
				{
					return itemArray.GetEnumerator();
				}
				return new ListViewItem[0].GetEnumerator();
			}

			// Token: 0x04003B30 RID: 15152
			private ListView owner;

			// Token: 0x04003B31 RID: 15153
			private int lastAccessedIndex = -1;
		}

		/// <summary>Represents the collection that contains the indexes to the selected items in a <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
		// Token: 0x020006C7 RID: 1735
		[ListBindable(false)]
		public class SelectedIndexCollection : IList, ICollection, IEnumerable
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" /> class.</summary>
			/// <param name="owner">A <see cref="T:System.Windows.Forms.ListView" /> control that owns the collection.</param>
			// Token: 0x060069CF RID: 27087 RVA: 0x00187C22 File Offset: 0x00185E22
			public SelectedIndexCollection(ListView owner)
			{
				this.owner = owner;
			}

			/// <summary>Gets the number of items in the collection.</summary>
			/// <returns>The number of items in the collection.</returns>
			// Token: 0x170016EF RID: 5871
			// (get) Token: 0x060069D0 RID: 27088 RVA: 0x00187C34 File Offset: 0x00185E34
			[Browsable(false)]
			public int Count
			{
				get
				{
					if (this.owner.IsHandleCreated)
					{
						return (int)(long)this.owner.SendMessage(4146, 0, 0);
					}
					if (this.owner.savedSelectedItems != null)
					{
						return this.owner.savedSelectedItems.Count;
					}
					return 0;
				}
			}

			// Token: 0x170016F0 RID: 5872
			// (get) Token: 0x060069D1 RID: 27089 RVA: 0x00187C88 File Offset: 0x00185E88
			private int[] IndicesArray
			{
				get
				{
					int count = this.Count;
					int[] array = new int[count];
					if (this.owner.IsHandleCreated)
					{
						int num = -1;
						for (int i = 0; i < count; i++)
						{
							int num2 = (int)(long)this.owner.SendMessage(4108, num, 2);
							if (num2 <= -1)
							{
								throw new InvalidOperationException(SR.GetString("SelectedNotEqualActual"));
							}
							array[i] = num2;
							num = num2;
						}
					}
					else
					{
						for (int j = 0; j < count; j++)
						{
							array[j] = this.owner.savedSelectedItems[j].Index;
						}
					}
					return array;
				}
			}

			/// <summary>Gets the index value at the specified index within the collection.</summary>
			/// <param name="index">The index of the item in the collection to retrieve.</param>
			/// <returns>The index value from the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> that is stored at the specified location.</returns>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0 or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListView.SelectedIndexCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" />.</exception>
			// Token: 0x170016F1 RID: 5873
			public int this[int index]
			{
				get
				{
					if (index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
					if (this.owner.IsHandleCreated)
					{
						int num = -1;
						for (int i = 0; i <= index; i++)
						{
							num = (int)(long)this.owner.SendMessage(4108, num, 2);
						}
						return num;
					}
					return this.owner.savedSelectedItems[index].Index;
				}
			}

			/// <summary>Gets or sets an object in the collection.</summary>
			/// <param name="index">The zero-based index of the element to get or set.</param>
			/// <returns>The index value from the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> that is stored at the specified location.</returns>
			// Token: 0x170016F2 RID: 5874
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection of controls.</summary>
			/// <returns>The object used to synchronize the collection.</returns>
			// Token: 0x170016F3 RID: 5875
			// (get) Token: 0x060069D5 RID: 27093 RVA: 0x00006A49 File Offset: 0x00004C49
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
			/// <returns>
			///   <see langword="false" /> in all cases.</returns>
			// Token: 0x170016F4 RID: 5876
			// (get) Token: 0x060069D6 RID: 27094 RVA: 0x0001180C File Offset: 0x0000FA0C
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" /> has a fixed size.</summary>
			/// <returns>
			///   <see langword="true" /> in all cases.</returns>
			// Token: 0x170016F5 RID: 5877
			// (get) Token: 0x060069D7 RID: 27095 RVA: 0x0001180C File Offset: 0x0000FA0C
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>
			///   <see langword="true" /> if the collection is read-only; otherwise, <see langword="false" />.</returns>
			// Token: 0x170016F6 RID: 5878
			// (get) Token: 0x060069D8 RID: 27096 RVA: 0x0001180C File Offset: 0x0000FA0C
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			/// <summary>Determines whether the specified index is located in the collection.</summary>
			/// <param name="selectedIndex">The index to locate in the collection.</param>
			/// <returns>
			///   <see langword="true" /> if the specified index from the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> for the <see cref="T:System.Windows.Forms.ListView" /> is an item in the collection; otherwise, <see langword="false" />.</returns>
			// Token: 0x060069D9 RID: 27097 RVA: 0x00187DCE File Offset: 0x00185FCE
			public bool Contains(int selectedIndex)
			{
				return this.owner.Items[selectedIndex].Selected;
			}

			/// <summary>Determines whether the specified item is located in the collection.</summary>
			/// <param name="selectedIndex">The index to locate in the collection.</param>
			/// <returns>
			///   <see langword="true" /> if the specified index from the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> for the <see cref="T:System.Windows.Forms.ListView" /> is an item in the collection; otherwise, <see langword="false" />.</returns>
			// Token: 0x060069DA RID: 27098 RVA: 0x00187DE6 File Offset: 0x00185FE6
			bool IList.Contains(object selectedIndex)
			{
				return selectedIndex is int && this.Contains((int)selectedIndex);
			}

			/// <summary>Returns the index within the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" /> of the specified index from the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> of the <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
			/// <param name="selectedIndex">The zero-based index from the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> to locate in the collection.</param>
			/// <returns>The zero-based index in the collection where the specified index of the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> is located within the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" />, or -1 if the index is not located in the collection.</returns>
			// Token: 0x060069DB RID: 27099 RVA: 0x00187E00 File Offset: 0x00186000
			public int IndexOf(int selectedIndex)
			{
				int[] indicesArray = this.IndicesArray;
				for (int i = 0; i < indicesArray.Length; i++)
				{
					if (indicesArray[i] == selectedIndex)
					{
						return i;
					}
				}
				return -1;
			}

			/// <summary>Returns the index in the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" />. The <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" /> contains the indexes of selected items in the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> of the <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
			/// <param name="selectedIndex">The zero-based index from the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> to locate in the collection.</param>
			// Token: 0x060069DC RID: 27100 RVA: 0x00187E2B File Offset: 0x0018602B
			int IList.IndexOf(object selectedIndex)
			{
				if (selectedIndex is int)
				{
					return this.IndexOf((int)selectedIndex);
				}
				return -1;
			}

			/// <summary>Adds an item to the collection.</summary>
			/// <param name="value">An object to be added to the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" />.</param>
			/// <returns>The location of the added item.</returns>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x060069DD RID: 27101 RVA: 0x00187E43 File Offset: 0x00186043
			int IList.Add(object value)
			{
				if (value is int)
				{
					return this.Add((int)value);
				}
				throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
				{
					"value",
					value.ToString()
				}));
			}

			/// <summary>Removes all items from the collection.</summary>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x060069DE RID: 27102 RVA: 0x00187E80 File Offset: 0x00186080
			void IList.Clear()
			{
				this.Clear();
			}

			/// <summary>Inserts an item into the collection at a specified index.</summary>
			/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
			/// <param name="value">The object to be inserted into the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x060069DF RID: 27103 RVA: 0x0000A337 File Offset: 0x00008537
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException();
			}

			/// <summary>Removes the first occurrence of a specified item from the collection.</summary>
			/// <param name="value">The object to remove from the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x060069E0 RID: 27104 RVA: 0x00187E88 File Offset: 0x00186088
			void IList.Remove(object value)
			{
				if (value is int)
				{
					this.Remove((int)value);
					return;
				}
				throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
				{
					"value",
					value.ToString()
				}));
			}

			/// <summary>Removes an item from the collection at a specified index.</summary>
			/// <param name="index">The zero-based index of the item to remove.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x060069E1 RID: 27105 RVA: 0x0000A337 File Offset: 0x00008537
			void IList.RemoveAt(int index)
			{
				throw new NotSupportedException();
			}

			/// <summary>Adds the item at the specified index in the <see cref="P:System.Windows.Forms.ListView.Items" /> array to the collection.</summary>
			/// <param name="itemIndex">The index of the item in the <see cref="P:System.Windows.Forms.ListView.Items" /> collection to be added to the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" />.</param>
			/// <returns>The number of items in the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" />.</returns>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The specified index is less than 0 or greater than or equal to the number of items in the owner <see cref="T:System.Windows.Forms.ListView" />.  
			///  -or-  
			///  The owner <see cref="T:System.Windows.Forms.ListView" /> is in virtual mode, and the specified index is less than 0 or greater than or equal to the value of <see cref="P:System.Windows.Forms.ListView.VirtualListSize" />.</exception>
			// Token: 0x060069E2 RID: 27106 RVA: 0x00187EC8 File Offset: 0x001860C8
			public int Add(int itemIndex)
			{
				if (this.owner.VirtualMode)
				{
					if (itemIndex < 0 || itemIndex >= this.owner.VirtualListSize)
					{
						throw new ArgumentOutOfRangeException("itemIndex", SR.GetString("InvalidArgument", new object[]
						{
							"itemIndex",
							itemIndex.ToString(CultureInfo.CurrentCulture)
						}));
					}
					if (this.owner.IsHandleCreated)
					{
						this.owner.SetItemState(itemIndex, 2, 2);
						return this.Count;
					}
					return -1;
				}
				else
				{
					if (itemIndex < 0 || itemIndex >= this.owner.Items.Count)
					{
						throw new ArgumentOutOfRangeException("itemIndex", SR.GetString("InvalidArgument", new object[]
						{
							"itemIndex",
							itemIndex.ToString(CultureInfo.CurrentCulture)
						}));
					}
					this.owner.Items[itemIndex].Selected = true;
					return this.Count;
				}
			}

			/// <summary>Clears the items in the collection.</summary>
			// Token: 0x060069E3 RID: 27107 RVA: 0x00187FB0 File Offset: 0x001861B0
			public void Clear()
			{
				if (!this.owner.VirtualMode)
				{
					this.owner.savedSelectedItems = null;
				}
				if (this.owner.IsHandleCreated)
				{
					this.owner.SetItemState(-1, 0, 2);
				}
			}

			/// <summary>Copies the entire collection into an existing array at a specified location within the array.</summary>
			/// <param name="dest">An <see cref="T:System.Array" /> representing the array to copy the contents of the collection to.</param>
			/// <param name="index">The location within the destination array to copy the items from the collection to.</param>
			// Token: 0x060069E4 RID: 27108 RVA: 0x00187FE6 File Offset: 0x001861E6
			public void CopyTo(Array dest, int index)
			{
				if (this.Count > 0)
				{
					Array.Copy(this.IndicesArray, 0, dest, index, this.Count);
				}
			}

			/// <summary>Returns an enumerator that can be used to iterate through the selected index collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the selected index collection.</returns>
			// Token: 0x060069E5 RID: 27109 RVA: 0x00188008 File Offset: 0x00186208
			public IEnumerator GetEnumerator()
			{
				int[] indicesArray = this.IndicesArray;
				if (indicesArray != null)
				{
					return indicesArray.GetEnumerator();
				}
				return new int[0].GetEnumerator();
			}

			/// <summary>Removes the item at the specified index in the <see cref="P:System.Windows.Forms.ListView.Items" /> collection from the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" />.</summary>
			/// <param name="itemIndex">The index of the item in the <see cref="P:System.Windows.Forms.ListView.Items" /> collection to remove from the <see cref="T:System.Windows.Forms.ListView.SelectedIndexCollection" />.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The specified index is less than 0 or greater than or equal to the number of items in the owner <see cref="T:System.Windows.Forms.ListView" />.  
			///  -or-  
			///  The owner <see cref="T:System.Windows.Forms.ListView" /> is in virtual mode, and the specified index is less than 0 or greater than or equal to the value of <see cref="P:System.Windows.Forms.ListView.VirtualListSize" />.</exception>
			// Token: 0x060069E6 RID: 27110 RVA: 0x00188034 File Offset: 0x00186234
			public void Remove(int itemIndex)
			{
				if (this.owner.VirtualMode)
				{
					if (itemIndex < 0 || itemIndex >= this.owner.VirtualListSize)
					{
						throw new ArgumentOutOfRangeException("itemIndex", SR.GetString("InvalidArgument", new object[]
						{
							"itemIndex",
							itemIndex.ToString(CultureInfo.CurrentCulture)
						}));
					}
					if (this.owner.IsHandleCreated)
					{
						this.owner.SetItemState(itemIndex, 0, 2);
						return;
					}
				}
				else
				{
					if (itemIndex < 0 || itemIndex >= this.owner.Items.Count)
					{
						throw new ArgumentOutOfRangeException("itemIndex", SR.GetString("InvalidArgument", new object[]
						{
							"itemIndex",
							itemIndex.ToString(CultureInfo.CurrentCulture)
						}));
					}
					this.owner.Items[itemIndex].Selected = false;
				}
			}

			// Token: 0x04003B32 RID: 15154
			private ListView owner;
		}

		/// <summary>Represents the collection of selected items in a list view control.</summary>
		// Token: 0x020006C8 RID: 1736
		[ListBindable(false)]
		public class SelectedListViewItemCollection : IList, ICollection, IEnumerable
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListView.SelectedListViewItemCollection" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.ListView" /> control that owns the collection.</param>
			// Token: 0x060069E7 RID: 27111 RVA: 0x0018810E File Offset: 0x0018630E
			public SelectedListViewItemCollection(ListView owner)
			{
				this.owner = owner;
			}

			// Token: 0x170016F7 RID: 5879
			// (get) Token: 0x060069E8 RID: 27112 RVA: 0x00188124 File Offset: 0x00186324
			private ListViewItem[] SelectedItemArray
			{
				get
				{
					if (this.owner.IsHandleCreated)
					{
						int num = (int)(long)this.owner.SendMessage(4146, 0, 0);
						ListViewItem[] array = new ListViewItem[num];
						int num2 = -1;
						for (int i = 0; i < num; i++)
						{
							int num3 = (int)(long)this.owner.SendMessage(4108, num2, 2);
							if (num3 <= -1)
							{
								throw new InvalidOperationException(SR.GetString("SelectedNotEqualActual"));
							}
							array[i] = this.owner.Items[num3];
							num2 = num3;
						}
						return array;
					}
					if (this.owner.savedSelectedItems != null)
					{
						ListViewItem[] array2 = new ListViewItem[this.owner.savedSelectedItems.Count];
						for (int j = 0; j < this.owner.savedSelectedItems.Count; j++)
						{
							array2[j] = this.owner.savedSelectedItems[j];
						}
						return array2;
					}
					return new ListViewItem[0];
				}
			}

			/// <summary>Gets the number of items in the collection.</summary>
			/// <returns>The number of items in the collection.</returns>
			// Token: 0x170016F8 RID: 5880
			// (get) Token: 0x060069E9 RID: 27113 RVA: 0x0018821C File Offset: 0x0018641C
			[Browsable(false)]
			public int Count
			{
				get
				{
					if (this.owner.VirtualMode)
					{
						throw new InvalidOperationException(SR.GetString("ListViewCantAccessSelectedItemsCollectionWhenInVirtualMode"));
					}
					if (this.owner.IsHandleCreated)
					{
						return (int)(long)this.owner.SendMessage(4146, 0, 0);
					}
					if (this.owner.savedSelectedItems != null)
					{
						return this.owner.savedSelectedItems.Count;
					}
					return 0;
				}
			}

			/// <summary>Gets the item at the specified index within the collection.</summary>
			/// <param name="index">The index of the item in the collection to retrieve.</param>
			/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem" /> representing the item located at the specified index within the collection.</returns>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListView.ListViewItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.SelectedListViewItemCollection" />.</exception>
			// Token: 0x170016F9 RID: 5881
			public ListViewItem this[int index]
			{
				get
				{
					if (this.owner.VirtualMode)
					{
						throw new InvalidOperationException(SR.GetString("ListViewCantAccessSelectedItemsCollectionWhenInVirtualMode"));
					}
					if (index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
					if (this.owner.IsHandleCreated)
					{
						int num = -1;
						for (int i = 0; i <= index; i++)
						{
							num = (int)(long)this.owner.SendMessage(4108, num, 2);
						}
						return this.owner.Items[num];
					}
					return this.owner.savedSelectedItems[index];
				}
			}

			/// <summary>Gets an item with the specified key from the collection.</summary>
			/// <param name="key">The name of the item to retrieve from the collection.</param>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> with the specified key.</returns>
			// Token: 0x170016FA RID: 5882
			public virtual ListViewItem this[string key]
			{
				get
				{
					if (this.owner.VirtualMode)
					{
						throw new InvalidOperationException(SR.GetString("ListViewCantAccessSelectedItemsCollectionWhenInVirtualMode"));
					}
					if (string.IsNullOrEmpty(key))
					{
						return null;
					}
					int num = this.IndexOfKey(key);
					if (this.IsValidIndex(num))
					{
						return this[num];
					}
					return null;
				}
			}

			/// <summary>Gets or sets an object from the collection.</summary>
			/// <param name="index">The zero-based index of the element to get.</param>
			/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the item located at the specified index within the collection.</returns>
			// Token: 0x170016FB RID: 5883
			object IList.this[int index]
			{
				get
				{
					if (this.owner.VirtualMode)
					{
						throw new InvalidOperationException(SR.GetString("ListViewCantAccessSelectedItemsCollectionWhenInVirtualMode"));
					}
					return this[index];
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
			/// <returns>
			///   <see langword="true" /> in all cases.</returns>
			// Token: 0x170016FC RID: 5884
			// (get) Token: 0x060069EE RID: 27118 RVA: 0x00012E4E File Offset: 0x0001104E
			bool IList.IsFixedSize
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>
			///   <see langword="true" /> if the collection is read-only; otherwise, <see langword="false" />.</returns>
			// Token: 0x170016FD RID: 5885
			// (get) Token: 0x060069EF RID: 27119 RVA: 0x00012E4E File Offset: 0x0001104E
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection of controls.</summary>
			/// <returns>The object used to synchronize the collection.</returns>
			// Token: 0x170016FE RID: 5886
			// (get) Token: 0x060069F0 RID: 27120 RVA: 0x00006A49 File Offset: 0x00004C49
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
			/// <returns>
			///   <see langword="false" /> in all cases.</returns>
			// Token: 0x170016FF RID: 5887
			// (get) Token: 0x060069F1 RID: 27121 RVA: 0x0001180C File Offset: 0x0000FA0C
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Adds an item to the collection.</summary>
			/// <param name="value">An object to be added to the <see cref="T:System.Windows.Forms.ListView.SelectedListViewItemCollection" />.</param>
			/// <returns>The location of the added item.</returns>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x060069F2 RID: 27122 RVA: 0x0000A337 File Offset: 0x00008537
			int IList.Add(object value)
			{
				throw new NotSupportedException();
			}

			/// <summary>Inserts an item into the collection at a specified index.</summary>
			/// <param name="index">The zero-based index of the item to be inserted.</param>
			/// <param name="value">An object to be added to the <see cref="T:System.Windows.Forms.ListView.SelectedListViewItemCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x060069F3 RID: 27123 RVA: 0x0000A337 File Offset: 0x00008537
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060069F4 RID: 27124 RVA: 0x001883C0 File Offset: 0x001865C0
			private bool IsValidIndex(int index)
			{
				return index >= 0 && index < this.Count;
			}

			/// <summary>Removes the first occurrence of a specified item from the collection.</summary>
			/// <param name="value">The object to remove from the <see cref="T:System.Windows.Forms.ListView.SelectedListViewItemCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x060069F5 RID: 27125 RVA: 0x0000A337 File Offset: 0x00008537
			void IList.Remove(object value)
			{
				throw new NotSupportedException();
			}

			/// <summary>Removes an item from the collection at a specified index.</summary>
			/// <param name="index">The zero-based index of the item to remove.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x060069F6 RID: 27126 RVA: 0x0000A337 File Offset: 0x00008537
			void IList.RemoveAt(int index)
			{
				throw new NotSupportedException();
			}

			/// <summary>Removes all items from the collection.</summary>
			// Token: 0x060069F7 RID: 27127 RVA: 0x001883D4 File Offset: 0x001865D4
			public void Clear()
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantAccessSelectedItemsCollectionWhenInVirtualMode"));
				}
				ListViewItem[] selectedItemArray = this.SelectedItemArray;
				for (int i = 0; i < selectedItemArray.Length; i++)
				{
					selectedItemArray[i].Selected = false;
				}
			}

			/// <summary>Determines whether an item with the specified key is contained in the collection.</summary>
			/// <param name="key">The name of the item to find in the collection.</param>
			/// <returns>
			///   <see langword="true" /> to indicate the specified item is contained in the collection; otherwise, <see langword="false" />.</returns>
			// Token: 0x060069F8 RID: 27128 RVA: 0x0018841C File Offset: 0x0018661C
			public virtual bool ContainsKey(string key)
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantAccessSelectedItemsCollectionWhenInVirtualMode"));
				}
				return this.IsValidIndex(this.IndexOfKey(key));
			}

			/// <summary>Determines whether the specified item is located in the collection.</summary>
			/// <param name="item">A <see cref="T:System.Windows.Forms.ListViewItem" /> representing the item to locate in the collection.</param>
			/// <returns>
			///   <see langword="true" /> if the specified item is located in the collection; otherwise, <see langword="false" />.</returns>
			// Token: 0x060069F9 RID: 27129 RVA: 0x00188448 File Offset: 0x00186648
			public bool Contains(ListViewItem item)
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantAccessSelectedItemsCollectionWhenInVirtualMode"));
				}
				return this.IndexOf(item) != -1;
			}

			/// <summary>Determines whether the specified item is located in the collection.</summary>
			/// <param name="item">An object that represents the item to locate in the collection.</param>
			/// <returns>
			///   <see langword="true" /> if the specified item is located in the collection; otherwise, <see langword="false" />.</returns>
			// Token: 0x060069FA RID: 27130 RVA: 0x00188474 File Offset: 0x00186674
			bool IList.Contains(object item)
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantAccessSelectedItemsCollectionWhenInVirtualMode"));
				}
				return item is ListViewItem && this.Contains((ListViewItem)item);
			}

			/// <summary>Copies the entire collection into an existing array at a specified location within the array.</summary>
			/// <param name="dest">An <see cref="T:System.Array" /> representing the array to copy the contents of the collection to.</param>
			/// <param name="index">The location within the destination array to copy the items from the collection to.</param>
			// Token: 0x060069FB RID: 27131 RVA: 0x001884A9 File Offset: 0x001866A9
			public void CopyTo(Array dest, int index)
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantAccessSelectedItemsCollectionWhenInVirtualMode"));
				}
				if (this.Count > 0)
				{
					Array.Copy(this.SelectedItemArray, 0, dest, index, this.Count);
				}
			}

			/// <summary>Returns an enumerator that can be used to iterate through the selected item collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the collection of selected items.</returns>
			// Token: 0x060069FC RID: 27132 RVA: 0x001884E8 File Offset: 0x001866E8
			public IEnumerator GetEnumerator()
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantAccessSelectedItemsCollectionWhenInVirtualMode"));
				}
				ListViewItem[] selectedItemArray = this.SelectedItemArray;
				if (selectedItemArray != null)
				{
					return selectedItemArray.GetEnumerator();
				}
				return new ListViewItem[0].GetEnumerator();
			}

			/// <summary>Returns the index within the collection of the specified item.</summary>
			/// <param name="item">A <see cref="T:System.Windows.Forms.ListViewItem" /> representing the item to locate in the collection.</param>
			/// <returns>The zero-based index of the item in the collection. If the item is not located in the collection, the return value is negative one (-1).</returns>
			// Token: 0x060069FD RID: 27133 RVA: 0x00188530 File Offset: 0x00186730
			public int IndexOf(ListViewItem item)
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantAccessSelectedItemsCollectionWhenInVirtualMode"));
				}
				ListViewItem[] selectedItemArray = this.SelectedItemArray;
				for (int i = 0; i < selectedItemArray.Length; i++)
				{
					if (selectedItemArray[i] == item)
					{
						return i;
					}
				}
				return -1;
			}

			/// <summary>Returns the index, within the collection, of the specified item.</summary>
			/// <param name="item">An object that represents the item to locate in the collection.</param>
			/// <returns>The zero-based index of the item if it is in the collection; otherwise, -1</returns>
			// Token: 0x060069FE RID: 27134 RVA: 0x00188578 File Offset: 0x00186778
			int IList.IndexOf(object item)
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantAccessSelectedItemsCollectionWhenInVirtualMode"));
				}
				if (item is ListViewItem)
				{
					return this.IndexOf((ListViewItem)item);
				}
				return -1;
			}

			/// <summary>Returns the index of the first occurrence of the item with the specified key.</summary>
			/// <param name="key">The name of the item to find in the collection.</param>
			/// <returns>The zero-based index of the first item with the specified key.</returns>
			// Token: 0x060069FF RID: 27135 RVA: 0x001885B0 File Offset: 0x001867B0
			public virtual int IndexOfKey(string key)
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantAccessSelectedItemsCollectionWhenInVirtualMode"));
				}
				if (string.IsNullOrEmpty(key))
				{
					return -1;
				}
				if (this.IsValidIndex(this.lastAccessedIndex) && WindowsFormsUtils.SafeCompareStrings(this[this.lastAccessedIndex].Name, key, true))
				{
					return this.lastAccessedIndex;
				}
				for (int i = 0; i < this.Count; i++)
				{
					if (WindowsFormsUtils.SafeCompareStrings(this[i].Name, key, true))
					{
						this.lastAccessedIndex = i;
						return i;
					}
				}
				this.lastAccessedIndex = -1;
				return -1;
			}

			// Token: 0x04003B33 RID: 15155
			private ListView owner;

			// Token: 0x04003B34 RID: 15156
			private int lastAccessedIndex = -1;
		}

		/// <summary>Represents the collection of column headers in a <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
		// Token: 0x020006C9 RID: 1737
		[ListBindable(false)]
		public class ColumnHeaderCollection : IList, ICollection, IEnumerable
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.ListView" /> that owns this collection.</param>
			// Token: 0x06006A00 RID: 27136 RVA: 0x0018864A File Offset: 0x0018684A
			public ColumnHeaderCollection(ListView owner)
			{
				this.owner = owner;
			}

			/// <summary>Gets the column header at the specified index within the collection.</summary>
			/// <param name="index">The index of the column header to retrieve from the collection.</param>
			/// <returns>A <see cref="T:System.Windows.Forms.ColumnHeader" /> representing the column header located at the specified index within the collection.</returns>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0 or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListView.ColumnHeaderCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" />.</exception>
			// Token: 0x17001700 RID: 5888
			public virtual ColumnHeader this[int index]
			{
				get
				{
					if (this.owner.columnHeaders == null || index < 0 || index >= this.owner.columnHeaders.Length)
					{
						throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
					return this.owner.columnHeaders[index];
				}
			}

			/// <summary>Gets or sets the column header at the specified index within the collection.</summary>
			/// <param name="index">The zero-based index of the element to get or set.</param>
			/// <returns>A <see cref="T:System.Windows.Forms.ColumnHeader" /> that represents the column header located at the specified index within the collection.</returns>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0 or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListView.ColumnHeaderCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" />.</exception>
			// Token: 0x17001701 RID: 5889
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			/// <summary>Gets the column header with the specified key from the collection.</summary>
			/// <param name="key">The name of the column header to retrieve from the collection.</param>
			/// <returns>The <see cref="T:System.Windows.Forms.ColumnHeader" /> with the specified key.</returns>
			// Token: 0x17001702 RID: 5890
			public virtual ColumnHeader this[string key]
			{
				get
				{
					if (string.IsNullOrEmpty(key))
					{
						return null;
					}
					int num = this.IndexOfKey(key);
					if (this.IsValidIndex(num))
					{
						return this[num];
					}
					return null;
				}
			}

			/// <summary>Gets the number of items in the collection.</summary>
			/// <returns>The number of items in the collection.</returns>
			// Token: 0x17001703 RID: 5891
			// (get) Token: 0x06006A05 RID: 27141 RVA: 0x00188709 File Offset: 0x00186909
			[Browsable(false)]
			public int Count
			{
				get
				{
					if (this.owner.columnHeaders != null)
					{
						return this.owner.columnHeaders.Length;
					}
					return 0;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection of controls.</summary>
			/// <returns>The object used to synchronize the collection.</returns>
			// Token: 0x17001704 RID: 5892
			// (get) Token: 0x06006A06 RID: 27142 RVA: 0x00006A49 File Offset: 0x00004C49
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>Gets a value indicating whether access to the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" /> is synchronized (thread safe).</summary>
			/// <returns>
			///   <see langword="true" /> in all cases.</returns>
			// Token: 0x17001705 RID: 5893
			// (get) Token: 0x06006A07 RID: 27143 RVA: 0x00012E4E File Offset: 0x0001104E
			bool ICollection.IsSynchronized
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" /> has a fixed size.</summary>
			/// <returns>
			///   <see langword="false" /> in all cases.</returns>
			// Token: 0x17001706 RID: 5894
			// (get) Token: 0x06006A08 RID: 27144 RVA: 0x0001180C File Offset: 0x0000FA0C
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>
			///   <see langword="true" /> if the collection is read-only; otherwise, <see langword="false" />.</returns>
			// Token: 0x17001707 RID: 5895
			// (get) Token: 0x06006A09 RID: 27145 RVA: 0x0001180C File Offset: 0x0000FA0C
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			/// <summary>Removes the column with the specified key from the collection.</summary>
			/// <param name="key">The name of the column to remove from the collection.</param>
			// Token: 0x06006A0A RID: 27146 RVA: 0x00188728 File Offset: 0x00186928
			public virtual void RemoveByKey(string key)
			{
				int num = this.IndexOfKey(key);
				if (this.IsValidIndex(num))
				{
					this.RemoveAt(num);
				}
			}

			/// <summary>Determines the index for a column with the specified key.</summary>
			/// <param name="key">The name of the column to retrieve the index for.</param>
			/// <returns>The zero-based index for the first occurrence of the column with the specified name, if found; otherwise, -1.</returns>
			// Token: 0x06006A0B RID: 27147 RVA: 0x00188750 File Offset: 0x00186950
			public virtual int IndexOfKey(string key)
			{
				if (string.IsNullOrEmpty(key))
				{
					return -1;
				}
				if (this.IsValidIndex(this.lastAccessedIndex) && WindowsFormsUtils.SafeCompareStrings(this[this.lastAccessedIndex].Name, key, true))
				{
					return this.lastAccessedIndex;
				}
				for (int i = 0; i < this.Count; i++)
				{
					if (WindowsFormsUtils.SafeCompareStrings(this[i].Name, key, true))
					{
						this.lastAccessedIndex = i;
						return i;
					}
				}
				this.lastAccessedIndex = -1;
				return -1;
			}

			// Token: 0x06006A0C RID: 27148 RVA: 0x001887CD File Offset: 0x001869CD
			private bool IsValidIndex(int index)
			{
				return index >= 0 && index < this.Count;
			}

			/// <summary>Adds a column header to the collection with specified text, width, and alignment settings.</summary>
			/// <param name="text">The text to display in the column header.</param>
			/// <param name="width">The initial width of the column header.</param>
			/// <param name="textAlign">One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values.</param>
			/// <returns>The <see cref="T:System.Windows.Forms.ColumnHeader" /> that was created and added to the collection.</returns>
			// Token: 0x06006A0D RID: 27149 RVA: 0x001887E0 File Offset: 0x001869E0
			public virtual ColumnHeader Add(string text, int width, HorizontalAlignment textAlign)
			{
				ColumnHeader columnHeader = new ColumnHeader();
				columnHeader.Text = text;
				columnHeader.Width = width;
				columnHeader.TextAlign = textAlign;
				return this.owner.InsertColumn(this.Count, columnHeader);
			}

			/// <summary>Adds an existing <see cref="T:System.Windows.Forms.ColumnHeader" /> to the collection.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.ColumnHeader" /> to add to the collection.</param>
			/// <returns>The zero-based index into the collection where the item was added.</returns>
			// Token: 0x06006A0E RID: 27150 RVA: 0x0018881C File Offset: 0x00186A1C
			public virtual int Add(ColumnHeader value)
			{
				int count = this.Count;
				this.owner.InsertColumn(count, value);
				return count;
			}

			/// <summary>Creates and adds a column with the specified text to the collection.</summary>
			/// <param name="text">The text to display in the column header.</param>
			/// <returns>The <see cref="T:System.Windows.Forms.ColumnHeader" /> with the specified text that was added to the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" />.</returns>
			// Token: 0x06006A0F RID: 27151 RVA: 0x00188840 File Offset: 0x00186A40
			public virtual ColumnHeader Add(string text)
			{
				ColumnHeader columnHeader = new ColumnHeader();
				columnHeader.Text = text;
				return this.owner.InsertColumn(this.Count, columnHeader);
			}

			/// <summary>Creates and adds a column with the specified text and width to the collection.</summary>
			/// <param name="text">The text of the <see cref="T:System.Windows.Forms.ColumnHeader" /> to add to the collection.</param>
			/// <param name="width">The width of the <see cref="T:System.Windows.Forms.ColumnHeader" /> to add to the collection.</param>
			/// <returns>The <see cref="T:System.Windows.Forms.ColumnHeader" /> with the specified text and width that was added to the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" />.</returns>
			// Token: 0x06006A10 RID: 27152 RVA: 0x0018886C File Offset: 0x00186A6C
			public virtual ColumnHeader Add(string text, int width)
			{
				ColumnHeader columnHeader = new ColumnHeader();
				columnHeader.Text = text;
				columnHeader.Width = width;
				return this.owner.InsertColumn(this.Count, columnHeader);
			}

			/// <summary>Creates and adds a column with the specified text and key to the collection.</summary>
			/// <param name="key">The key of the <see cref="T:System.Windows.Forms.ColumnHeader" /> to add to the collection.</param>
			/// <param name="text">The text of the <see cref="T:System.Windows.Forms.ColumnHeader" /> to add to the collection.</param>
			/// <returns>The <see cref="T:System.Windows.Forms.ColumnHeader" /> with the specified key and text that was added to the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" />.</returns>
			// Token: 0x06006A11 RID: 27153 RVA: 0x001888A0 File Offset: 0x00186AA0
			public virtual ColumnHeader Add(string key, string text)
			{
				ColumnHeader columnHeader = new ColumnHeader();
				columnHeader.Name = key;
				columnHeader.Text = text;
				return this.owner.InsertColumn(this.Count, columnHeader);
			}

			/// <summary>Creates and adds a column with the specified text, key, and width to the collection.</summary>
			/// <param name="key">The key of the column header.</param>
			/// <param name="text">The text to display in the column header.</param>
			/// <param name="width">The initial width of the <see cref="T:System.Windows.Forms.ColumnHeader" />.</param>
			/// <returns>The <see cref="T:System.Windows.Forms.ColumnHeader" /> with the given text, key, and width that was added to the collection.</returns>
			// Token: 0x06006A12 RID: 27154 RVA: 0x001888D4 File Offset: 0x00186AD4
			public virtual ColumnHeader Add(string key, string text, int width)
			{
				ColumnHeader columnHeader = new ColumnHeader();
				columnHeader.Name = key;
				columnHeader.Text = text;
				columnHeader.Width = width;
				return this.owner.InsertColumn(this.Count, columnHeader);
			}

			/// <summary>Creates and adds a column with the specified key, aligned text, width, and image key to the collection.</summary>
			/// <param name="key">The key of the column header.</param>
			/// <param name="text">The text to display in the column header.</param>
			/// <param name="width">The initial width of the column header.</param>
			/// <param name="textAlign">One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values.</param>
			/// <param name="imageKey">The key value of the image to display in the column header.</param>
			/// <returns>The <see cref="T:System.Windows.Forms.ColumnHeader" /> with the specified key, aligned text, width, and image key that has been added to the collection.</returns>
			// Token: 0x06006A13 RID: 27155 RVA: 0x00188910 File Offset: 0x00186B10
			public virtual ColumnHeader Add(string key, string text, int width, HorizontalAlignment textAlign, string imageKey)
			{
				ColumnHeader columnHeader = new ColumnHeader(imageKey);
				columnHeader.Name = key;
				columnHeader.Text = text;
				columnHeader.Width = width;
				columnHeader.TextAlign = textAlign;
				return this.owner.InsertColumn(this.Count, columnHeader);
			}

			/// <summary>Creates and adds a column with the specified key, aligned text, width, and image index to the collection.</summary>
			/// <param name="key">The key of the column header.</param>
			/// <param name="text">The text to display in the column header.</param>
			/// <param name="width">The initial width of the column header.</param>
			/// <param name="textAlign">One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values.</param>
			/// <param name="imageIndex">The index value of the image to display in the column.</param>
			/// <returns>The <see cref="T:System.Windows.Forms.ColumnHeader" /> with the specified key, aligned text, width, and image index that has been added to the collection.</returns>
			// Token: 0x06006A14 RID: 27156 RVA: 0x00188954 File Offset: 0x00186B54
			public virtual ColumnHeader Add(string key, string text, int width, HorizontalAlignment textAlign, int imageIndex)
			{
				ColumnHeader columnHeader = new ColumnHeader(imageIndex);
				columnHeader.Name = key;
				columnHeader.Text = text;
				columnHeader.Width = width;
				columnHeader.TextAlign = textAlign;
				return this.owner.InsertColumn(this.Count, columnHeader);
			}

			/// <summary>Adds an array of column headers to the collection.</summary>
			/// <param name="values">An array of <see cref="T:System.Windows.Forms.ColumnHeader" /> objects to add to the collection.</param>
			// Token: 0x06006A15 RID: 27157 RVA: 0x00188998 File Offset: 0x00186B98
			public virtual void AddRange(ColumnHeader[] values)
			{
				if (values == null)
				{
					throw new ArgumentNullException("values");
				}
				Hashtable hashtable = new Hashtable();
				int[] array = new int[values.Length];
				for (int i = 0; i < values.Length; i++)
				{
					if (values[i].DisplayIndex == -1)
					{
						values[i].DisplayIndexInternal = i;
					}
					if (!hashtable.ContainsKey(values[i].DisplayIndex) && values[i].DisplayIndex >= 0 && values[i].DisplayIndex < values.Length)
					{
						hashtable.Add(values[i].DisplayIndex, i);
					}
					array[i] = values[i].DisplayIndex;
					this.Add(values[i]);
				}
				if (hashtable.Count == values.Length)
				{
					this.owner.SetDisplayIndices(array);
				}
			}

			/// <summary>Adds a <see cref="T:System.Windows.Forms.ColumnHeader" /> to the <see cref="T:System.Windows.Forms.ListView" />.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.ColumnHeader" /> to be added to the <see cref="T:System.Windows.Forms.ListView" />.</param>
			/// <returns>The zero-based index indicating the location of the object that was added to the collection</returns>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.ColumnHeader" />.</exception>
			// Token: 0x06006A16 RID: 27158 RVA: 0x00188A56 File Offset: 0x00186C56
			int IList.Add(object value)
			{
				if (value is ColumnHeader)
				{
					return this.Add((ColumnHeader)value);
				}
				throw new ArgumentException(SR.GetString("ColumnHeaderCollectionInvalidArgument"));
			}

			/// <summary>Removes all column headers from the collection.</summary>
			// Token: 0x06006A17 RID: 27159 RVA: 0x00188A7C File Offset: 0x00186C7C
			public virtual void Clear()
			{
				if (this.owner.columnHeaders != null)
				{
					if (this.owner.View == View.Tile)
					{
						for (int i = this.owner.columnHeaders.Length - 1; i >= 0; i--)
						{
							int width = this.owner.columnHeaders[i].Width;
							this.owner.columnHeaders[i].OwnerListview = null;
						}
						this.owner.columnHeaders = null;
						if (this.owner.IsHandleCreated)
						{
							this.owner.RecreateHandleInternal();
							return;
						}
					}
					else
					{
						for (int j = this.owner.columnHeaders.Length - 1; j >= 0; j--)
						{
							int width2 = this.owner.columnHeaders[j].Width;
							if (this.owner.IsHandleCreated)
							{
								this.owner.SendMessage(4124, j, 0);
							}
							this.owner.columnHeaders[j].OwnerListview = null;
						}
						this.owner.columnHeaders = null;
					}
				}
			}

			/// <summary>Determines whether the specified column header is located in the collection.</summary>
			/// <param name="value">A <see cref="T:System.Windows.Forms.ColumnHeader" /> representing the column header to locate in the collection.</param>
			/// <returns>
			///   <see langword="true" /> if the column header is contained in the collection; otherwise, <see langword="false" />.</returns>
			// Token: 0x06006A18 RID: 27160 RVA: 0x00188B78 File Offset: 0x00186D78
			public bool Contains(ColumnHeader value)
			{
				return this.IndexOf(value) != -1;
			}

			/// <summary>Determines whether the specified column header is located in the collection.</summary>
			/// <param name="value">An object that represents the column header to locate in the collection.</param>
			/// <returns>
			///   <see langword="true" /> if the object is a column header that is contained in the collection; otherwise, <see langword="false" />.</returns>
			// Token: 0x06006A19 RID: 27161 RVA: 0x00188B87 File Offset: 0x00186D87
			bool IList.Contains(object value)
			{
				return value is ColumnHeader && this.Contains((ColumnHeader)value);
			}

			/// <summary>Determines if a column with the specified key is contained in the collection.</summary>
			/// <param name="key">The name of the column to search for.</param>
			/// <returns>
			///   <see langword="true" /> if a column with the specified name is contained in the collection; otherwise, <see langword="false" />.</returns>
			// Token: 0x06006A1A RID: 27162 RVA: 0x00188B9F File Offset: 0x00186D9F
			public virtual bool ContainsKey(string key)
			{
				return this.IsValidIndex(this.IndexOfKey(key));
			}

			/// <summary>Copies the <see cref="T:System.Windows.Forms.ColumnHeader" /> objects in the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" /> to an array, starting at a particular array index.</summary>
			/// <param name="dest">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in the array at which copying begins.</param>
			// Token: 0x06006A1B RID: 27163 RVA: 0x00188BAE File Offset: 0x00186DAE
			void ICollection.CopyTo(Array dest, int index)
			{
				if (this.Count > 0)
				{
					Array.Copy(this.owner.columnHeaders, 0, dest, index, this.Count);
				}
			}

			/// <summary>Returns the index, within the collection, of the specified column header.</summary>
			/// <param name="value">A <see cref="T:System.Windows.Forms.ColumnHeader" /> representing the column header to locate in the collection.</param>
			/// <returns>The zero-based index of the column header's location in the collection. If the column header is not located in the collection, the return value is -1.</returns>
			// Token: 0x06006A1C RID: 27164 RVA: 0x00188BD4 File Offset: 0x00186DD4
			public int IndexOf(ColumnHeader value)
			{
				for (int i = 0; i < this.Count; i++)
				{
					if (this[i] == value)
					{
						return i;
					}
				}
				return -1;
			}

			/// <summary>Returns the index, within the collection, of the specified column header.</summary>
			/// <param name="value">An object that represents the column header to locate in the collection.</param>
			// Token: 0x06006A1D RID: 27165 RVA: 0x00188BFF File Offset: 0x00186DFF
			int IList.IndexOf(object value)
			{
				if (value is ColumnHeader)
				{
					return this.IndexOf((ColumnHeader)value);
				}
				return -1;
			}

			/// <summary>Inserts an existing column header into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the column header is inserted.</param>
			/// <param name="value">The <see cref="T:System.Windows.Forms.ColumnHeader" /> to insert into the collection.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0 or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListView.ColumnHeaderCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" />.</exception>
			// Token: 0x06006A1E RID: 27166 RVA: 0x00188C18 File Offset: 0x00186E18
			public void Insert(int index, ColumnHeader value)
			{
				if (index < 0 || index > this.Count)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.owner.InsertColumn(index, value);
			}

			/// <summary>Inserts an existing column header into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the column header is inserted.</param>
			/// <param name="value">The <see cref="T:System.Windows.Forms.ColumnHeader" /> to insert into the collection.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0 or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListView.ColumnHeaderCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" />.</exception>
			// Token: 0x06006A1F RID: 27167 RVA: 0x00188C72 File Offset: 0x00186E72
			void IList.Insert(int index, object value)
			{
				if (value is ColumnHeader)
				{
					this.Insert(index, (ColumnHeader)value);
				}
			}

			/// <summary>Creates a new column header and inserts it into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the column header is inserted.</param>
			/// <param name="text">The text to display in the column header.</param>
			/// <param name="width">The initial width of the column header. Set to -1 to autosize the column header to the size of the largest subitem text in the column or -2 to autosize the column header to the size of the text of the column header.</param>
			/// <param name="textAlign">One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0 or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListView.ColumnHeaderCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" />.</exception>
			// Token: 0x06006A20 RID: 27168 RVA: 0x00188C8C File Offset: 0x00186E8C
			public void Insert(int index, string text, int width, HorizontalAlignment textAlign)
			{
				this.Insert(index, new ColumnHeader
				{
					Text = text,
					Width = width,
					TextAlign = textAlign
				});
			}

			/// <summary>Creates a new column header with the specified text, and inserts the header into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the column header is inserted.</param>
			/// <param name="text">The text to display in the column header.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0 or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListView.ColumnHeaderCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" />.</exception>
			// Token: 0x06006A21 RID: 27169 RVA: 0x00188CC0 File Offset: 0x00186EC0
			public void Insert(int index, string text)
			{
				this.Insert(index, new ColumnHeader
				{
					Text = text
				});
			}

			/// <summary>Creates a new column header with the specified text and initial width, and inserts the header into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the column header is inserted.</param>
			/// <param name="text">The text to display in the column header.</param>
			/// <param name="width">The initial width, in pixels, of the column header.</param>
			// Token: 0x06006A22 RID: 27170 RVA: 0x00188CE4 File Offset: 0x00186EE4
			public void Insert(int index, string text, int width)
			{
				this.Insert(index, new ColumnHeader
				{
					Text = text,
					Width = width
				});
			}

			/// <summary>Creates a new column header with the specified text and key, and inserts the header into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the column header is inserted.</param>
			/// <param name="key">The name of the column header.</param>
			/// <param name="text">The text to display in the column header.</param>
			// Token: 0x06006A23 RID: 27171 RVA: 0x00188D10 File Offset: 0x00186F10
			public void Insert(int index, string key, string text)
			{
				this.Insert(index, new ColumnHeader
				{
					Name = key,
					Text = text
				});
			}

			/// <summary>Creates a new column header with the specified text, key, and width, and inserts the header into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the column header is inserted.</param>
			/// <param name="key">The name of the column header.</param>
			/// <param name="text">The text to display in the column header.</param>
			/// <param name="width">The initial width, in pixels, of the column header.</param>
			// Token: 0x06006A24 RID: 27172 RVA: 0x00188D3C File Offset: 0x00186F3C
			public void Insert(int index, string key, string text, int width)
			{
				this.Insert(index, new ColumnHeader
				{
					Name = key,
					Text = text,
					Width = width
				});
			}

			/// <summary>Creates a new column header with the specified aligned text, key, width, and image key, and inserts the header into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the column header is inserted.</param>
			/// <param name="key">The name of the column header.</param>
			/// <param name="text">The text to display in the column header.</param>
			/// <param name="width">The initial width, in pixels, of the column header.</param>
			/// <param name="textAlign">One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values.</param>
			/// <param name="imageKey">The key of the image to display in the column header.</param>
			// Token: 0x06006A25 RID: 27173 RVA: 0x00188D70 File Offset: 0x00186F70
			public void Insert(int index, string key, string text, int width, HorizontalAlignment textAlign, string imageKey)
			{
				this.Insert(index, new ColumnHeader(imageKey)
				{
					Name = key,
					Text = text,
					Width = width,
					TextAlign = textAlign
				});
			}

			/// <summary>Creates a new column header with the specified aligned text, key, width, and image index, and inserts the header into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the column header is inserted.</param>
			/// <param name="key">The name of the column header.</param>
			/// <param name="text">The text to display in the column header.</param>
			/// <param name="width">The initial width, in pixels, of the column header.</param>
			/// <param name="textAlign">One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values.</param>
			/// <param name="imageIndex">The index of the image to display in the column header.</param>
			// Token: 0x06006A26 RID: 27174 RVA: 0x00188DAC File Offset: 0x00186FAC
			public void Insert(int index, string key, string text, int width, HorizontalAlignment textAlign, int imageIndex)
			{
				this.Insert(index, new ColumnHeader(imageIndex)
				{
					Name = key,
					Text = text,
					Width = width,
					TextAlign = textAlign
				});
			}

			/// <summary>Removes the column header at the specified index within the collection.</summary>
			/// <param name="index">The zero-based index of the column header to remove.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0 or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListView.ColumnHeaderCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" />.</exception>
			// Token: 0x06006A27 RID: 27175 RVA: 0x00188DE8 File Offset: 0x00186FE8
			public virtual void RemoveAt(int index)
			{
				if (index < 0 || index >= this.owner.columnHeaders.Length)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				int width = this.owner.columnHeaders[index].Width;
				if (this.owner.IsHandleCreated && this.owner.View != View.Tile && (int)(long)this.owner.SendMessage(4124, index, 0) == 0)
				{
					throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				int[] array = new int[this.Count - 1];
				ColumnHeader columnHeader = this[index];
				for (int i = 0; i < this.Count; i++)
				{
					ColumnHeader columnHeader2 = this[i];
					if (i != index)
					{
						if (columnHeader2.DisplayIndex >= columnHeader.DisplayIndex)
						{
							ColumnHeader columnHeader3 = columnHeader2;
							int displayIndexInternal = columnHeader3.DisplayIndexInternal;
							columnHeader3.DisplayIndexInternal = displayIndexInternal - 1;
						}
						array[(i > index) ? (i - 1) : i] = columnHeader2.DisplayIndexInternal;
					}
				}
				columnHeader.DisplayIndexInternal = -1;
				this.owner.columnHeaders[index].OwnerListview = null;
				int num = this.owner.columnHeaders.Length;
				if (num == 1)
				{
					this.owner.columnHeaders = null;
				}
				else
				{
					ColumnHeader[] array2 = new ColumnHeader[--num];
					if (index > 0)
					{
						Array.Copy(this.owner.columnHeaders, 0, array2, 0, index);
					}
					if (index < num)
					{
						Array.Copy(this.owner.columnHeaders, index + 1, array2, index, num - index);
					}
					this.owner.columnHeaders = array2;
				}
				if (this.owner.IsHandleCreated && this.owner.View == View.Tile)
				{
					this.owner.RecreateHandleInternal();
				}
				this.owner.SetDisplayIndices(array);
			}

			/// <summary>Removes the specified column header from the collection.</summary>
			/// <param name="column">A <see cref="T:System.Windows.Forms.ColumnHeader" /> representing the column header to remove from the collection.</param>
			// Token: 0x06006A28 RID: 27176 RVA: 0x00188FE4 File Offset: 0x001871E4
			public virtual void Remove(ColumnHeader column)
			{
				int num = this.IndexOf(column);
				if (num != -1)
				{
					this.RemoveAt(num);
				}
			}

			/// <summary>Removes the specified column header from the collection.</summary>
			/// <param name="value">A <see cref="T:System.Windows.Forms.ColumnHeader" /> that represents the column header to remove from the collection.</param>
			// Token: 0x06006A29 RID: 27177 RVA: 0x00189004 File Offset: 0x00187204
			void IList.Remove(object value)
			{
				if (value is ColumnHeader)
				{
					this.Remove((ColumnHeader)value);
				}
			}

			/// <summary>Returns an enumerator to use to iterate through the column header collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the column header collection.</returns>
			// Token: 0x06006A2A RID: 27178 RVA: 0x0018901A File Offset: 0x0018721A
			public IEnumerator GetEnumerator()
			{
				if (this.owner.columnHeaders != null)
				{
					return this.owner.columnHeaders.GetEnumerator();
				}
				return new ColumnHeader[0].GetEnumerator();
			}

			// Token: 0x04003B35 RID: 15157
			private ListView owner;

			// Token: 0x04003B36 RID: 15158
			private int lastAccessedIndex = -1;
		}

		/// <summary>Represents the collection of items in a <see cref="T:System.Windows.Forms.ListView" /> control or assigned to a <see cref="T:System.Windows.Forms.ListViewGroup" />.</summary>
		// Token: 0x020006CA RID: 1738
		[ListBindable(false)]
		public class ListViewItemCollection : IList, ICollection, IEnumerable
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.ListView" /> that owns the collection.</param>
			// Token: 0x06006A2B RID: 27179 RVA: 0x00189045 File Offset: 0x00187245
			public ListViewItemCollection(ListView owner)
			{
				this.innerList = new ListView.ListViewNativeItemCollection(owner);
			}

			// Token: 0x06006A2C RID: 27180 RVA: 0x00189060 File Offset: 0x00187260
			internal ListViewItemCollection(ListView.ListViewItemCollection.IInnerList innerList)
			{
				this.innerList = innerList;
			}

			// Token: 0x17001708 RID: 5896
			// (get) Token: 0x06006A2D RID: 27181 RVA: 0x00189076 File Offset: 0x00187276
			private ListView.ListViewItemCollection.IInnerList InnerList
			{
				get
				{
					return this.innerList;
				}
			}

			/// <summary>Gets the number of items in the collection.</summary>
			/// <returns>The number of items in the collection.</returns>
			// Token: 0x17001709 RID: 5897
			// (get) Token: 0x06006A2E RID: 27182 RVA: 0x0018907E File Offset: 0x0018727E
			[Browsable(false)]
			public int Count
			{
				get
				{
					return this.InnerList.Count;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection of controls.</summary>
			/// <returns>The object used to synchronize the collection.</returns>
			// Token: 0x1700170A RID: 5898
			// (get) Token: 0x06006A2F RID: 27183 RVA: 0x00006A49 File Offset: 0x00004C49
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
			/// <returns>
			///   <see langword="true" /> in all cases.</returns>
			// Token: 0x1700170B RID: 5899
			// (get) Token: 0x06006A30 RID: 27184 RVA: 0x00012E4E File Offset: 0x0001104E
			bool ICollection.IsSynchronized
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
			/// <returns>
			///   <see langword="false" /> in all cases.</returns>
			// Token: 0x1700170C RID: 5900
			// (get) Token: 0x06006A31 RID: 27185 RVA: 0x0001180C File Offset: 0x0000FA0C
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>
			///   <see langword="true" /> if the collection is read-only; otherwise, <see langword="false" />.</returns>
			// Token: 0x1700170D RID: 5901
			// (get) Token: 0x06006A32 RID: 27186 RVA: 0x0001180C File Offset: 0x0000FA0C
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets or sets the item at the specified index within the collection.</summary>
			/// <param name="index">The index of the item in the collection to get or set.</param>
			/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem" /> representing the item located at the specified index within the collection.</returns>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than 0 or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListView.ListViewItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" />.</exception>
			// Token: 0x1700170E RID: 5902
			public virtual ListViewItem this[int index]
			{
				get
				{
					if (index < 0 || index >= this.InnerList.Count)
					{
						throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
					return this.InnerList[index];
				}
				set
				{
					if (index < 0 || index >= this.InnerList.Count)
					{
						throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
					this.InnerList[index] = value;
				}
			}

			/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ListViewItem" /> at the specified index within the collection.</summary>
			/// <param name="index">The zero-based index of the element to get.</param>
			/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the item located at the specified index within the collection.</returns>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The index parameter is less than 0 or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListView.ListViewItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" />.</exception>
			// Token: 0x1700170F RID: 5903
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					if (value is ListViewItem)
					{
						this[index] = (ListViewItem)value;
						return;
					}
					if (value != null)
					{
						this[index] = new ListViewItem(value.ToString(), -1);
					}
				}
			}

			/// <summary>Retrieves the item with the specified key.</summary>
			/// <param name="key">The name of the item to retrieve.</param>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> whose <see cref="P:System.Windows.Forms.ListViewItem.Name" /> property matches the specified key.</returns>
			// Token: 0x17001710 RID: 5904
			public virtual ListViewItem this[string key]
			{
				get
				{
					if (string.IsNullOrEmpty(key))
					{
						return null;
					}
					int num = this.IndexOfKey(key);
					if (this.IsValidIndex(num))
					{
						return this[num];
					}
					return null;
				}
			}

			/// <summary>Creates an item with the specified text and adds it to the collection.</summary>
			/// <param name="text">The text to display for the item.</param>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> that was added to the collection.</returns>
			// Token: 0x06006A38 RID: 27192 RVA: 0x001891B5 File Offset: 0x001873B5
			public virtual ListViewItem Add(string text)
			{
				return this.Add(text, -1);
			}

			/// <summary>Adds an existing object to the collection.</summary>
			/// <param name="item">The object to add to the collection.</param>
			/// <returns>The zero-based index indicating the location of the object if it was added to the collection; otherwise, -1.</returns>
			// Token: 0x06006A39 RID: 27193 RVA: 0x001891BF File Offset: 0x001873BF
			int IList.Add(object item)
			{
				if (item is ListViewItem)
				{
					return this.IndexOf(this.Add((ListViewItem)item));
				}
				if (item != null)
				{
					return this.IndexOf(this.Add(item.ToString()));
				}
				return -1;
			}

			/// <summary>Creates an item with the specified text and image and adds it to the collection.</summary>
			/// <param name="text">The text of the item.</param>
			/// <param name="imageIndex">The index of the image to display for the item.</param>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> that was added to the collection.</returns>
			// Token: 0x06006A3A RID: 27194 RVA: 0x001891F4 File Offset: 0x001873F4
			public virtual ListViewItem Add(string text, int imageIndex)
			{
				ListViewItem listViewItem = new ListViewItem(text, imageIndex);
				this.Add(listViewItem);
				return listViewItem;
			}

			/// <summary>Adds an existing <see cref="T:System.Windows.Forms.ListViewItem" /> to the collection.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.ListViewItem" /> to add to the collection.</param>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> that was added to the collection.</returns>
			// Token: 0x06006A3B RID: 27195 RVA: 0x00189212 File Offset: 0x00187412
			public virtual ListViewItem Add(ListViewItem value)
			{
				this.InnerList.Add(value);
				return value;
			}

			/// <summary>Creates an item with the specified text and image and adds it to the collection.</summary>
			/// <param name="text">The text of the item.</param>
			/// <param name="imageKey">The key of the image to display for the item.</param>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> added to the collection.</returns>
			// Token: 0x06006A3C RID: 27196 RVA: 0x00189224 File Offset: 0x00187424
			public virtual ListViewItem Add(string text, string imageKey)
			{
				ListViewItem listViewItem = new ListViewItem(text, imageKey);
				this.Add(listViewItem);
				return listViewItem;
			}

			/// <summary>Creates an item with the specified key, text, and image, and adds it to the collection.</summary>
			/// <param name="key">The name of the item.</param>
			/// <param name="text">The text of the item.</param>
			/// <param name="imageKey">The key of the image to display for the item.</param>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> added to the collection.</returns>
			// Token: 0x06006A3D RID: 27197 RVA: 0x00189244 File Offset: 0x00187444
			public virtual ListViewItem Add(string key, string text, string imageKey)
			{
				ListViewItem listViewItem = new ListViewItem(text, imageKey);
				listViewItem.Name = key;
				this.Add(listViewItem);
				return listViewItem;
			}

			/// <summary>Creates an item with the specified key, text, and image and adds an item to the collection.</summary>
			/// <param name="key">The name of the item.</param>
			/// <param name="text">The text of the item.</param>
			/// <param name="imageIndex">The index of the image to display for the item.</param>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> added to the collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The containing <see cref="T:System.Windows.Forms.ListView" /> is in virtual mode.</exception>
			// Token: 0x06006A3E RID: 27198 RVA: 0x0018926C File Offset: 0x0018746C
			public virtual ListViewItem Add(string key, string text, int imageIndex)
			{
				ListViewItem listViewItem = new ListViewItem(text, imageIndex);
				listViewItem.Name = key;
				this.Add(listViewItem);
				return listViewItem;
			}

			/// <summary>Adds an array of <see cref="T:System.Windows.Forms.ListViewItem" /> objects to the collection.</summary>
			/// <param name="items">An array of <see cref="T:System.Windows.Forms.ListViewItem" /> objects to add to the collection.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="items" /> is <see langword="null" />.</exception>
			// Token: 0x06006A3F RID: 27199 RVA: 0x00189291 File Offset: 0x00187491
			public void AddRange(ListViewItem[] items)
			{
				if (items == null)
				{
					throw new ArgumentNullException("items");
				}
				this.InnerList.AddRange(items);
			}

			/// <summary>Adds a collection of items to the collection.</summary>
			/// <param name="items">The <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> to add to the collection.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="items" /> is <see langword="null" />.</exception>
			/// <exception cref="T:System.InvalidOperationException">The containing <see cref="T:System.Windows.Forms.ListView" /> is in virtual mode.</exception>
			// Token: 0x06006A40 RID: 27200 RVA: 0x001892B0 File Offset: 0x001874B0
			public void AddRange(ListView.ListViewItemCollection items)
			{
				if (items == null)
				{
					throw new ArgumentNullException("items");
				}
				ListViewItem[] array = new ListViewItem[items.Count];
				items.CopyTo(array, 0);
				this.InnerList.AddRange(array);
			}

			/// <summary>Removes all items from the collection.</summary>
			// Token: 0x06006A41 RID: 27201 RVA: 0x001892EB File Offset: 0x001874EB
			public virtual void Clear()
			{
				this.InnerList.Clear();
			}

			/// <summary>Determines whether the specified item is located in the collection.</summary>
			/// <param name="item">A <see cref="T:System.Windows.Forms.ListViewItem" /> representing the item to locate in the collection.</param>
			/// <returns>
			///   <see langword="true" /> if the item is contained in the collection; otherwise, <see langword="false" />.</returns>
			// Token: 0x06006A42 RID: 27202 RVA: 0x001892F8 File Offset: 0x001874F8
			public bool Contains(ListViewItem item)
			{
				return this.InnerList.Contains(item);
			}

			/// <summary>Determines whether the specified item is in the collection.</summary>
			/// <param name="item">An object that represents the item to locate in the collection.</param>
			/// <returns>
			///   <see langword="true" /> if the specified item is located in the collection; otherwise, <see langword="false" />.</returns>
			// Token: 0x06006A43 RID: 27203 RVA: 0x00189306 File Offset: 0x00187506
			bool IList.Contains(object item)
			{
				return item is ListViewItem && this.Contains((ListViewItem)item);
			}

			/// <summary>Determines whether the collection contains an item with the specified key.</summary>
			/// <param name="key">The name of the item to search for.</param>
			/// <returns>
			///   <see langword="true" /> to indicate the collection contains an item with the specified key; otherwise, <see langword="false" />.</returns>
			// Token: 0x06006A44 RID: 27204 RVA: 0x0018931E File Offset: 0x0018751E
			public virtual bool ContainsKey(string key)
			{
				return this.IsValidIndex(this.IndexOfKey(key));
			}

			/// <summary>Copies the entire collection into an existing array at a specified location within the array.</summary>
			/// <param name="dest">An <see cref="T:System.Array" /> representing the array to copy the contents of the collection to.</param>
			/// <param name="index">The location within the destination array to copy the items from the collection to.</param>
			// Token: 0x06006A45 RID: 27205 RVA: 0x0018932D File Offset: 0x0018752D
			public void CopyTo(Array dest, int index)
			{
				this.InnerList.CopyTo(dest, index);
			}

			/// <summary>Searches for items whose name matches the specified key, optionally searching subitems.</summary>
			/// <param name="key">The item name to search for.</param>
			/// <param name="searchAllSubItems">
			///   <see langword="true" /> to search subitems; otherwise, <see langword="false" />.</param>
			/// <returns>An array of  <see cref="T:System.Windows.Forms.ListViewItem" /> objects containing the matching items, or an empty array if no items matched.</returns>
			// Token: 0x06006A46 RID: 27206 RVA: 0x0018933C File Offset: 0x0018753C
			public ListViewItem[] Find(string key, bool searchAllSubItems)
			{
				ArrayList arrayList = this.FindInternal(key, searchAllSubItems, this, new ArrayList());
				ListViewItem[] array = new ListViewItem[arrayList.Count];
				arrayList.CopyTo(array, 0);
				return array;
			}

			// Token: 0x06006A47 RID: 27207 RVA: 0x00189370 File Offset: 0x00187570
			private ArrayList FindInternal(string key, bool searchAllSubItems, ListView.ListViewItemCollection listViewItems, ArrayList foundItems)
			{
				if (listViewItems == null || foundItems == null)
				{
					return null;
				}
				for (int i = 0; i < listViewItems.Count; i++)
				{
					if (WindowsFormsUtils.SafeCompareStrings(listViewItems[i].Name, key, true))
					{
						foundItems.Add(listViewItems[i]);
					}
					else if (searchAllSubItems)
					{
						for (int j = 1; j < listViewItems[i].SubItems.Count; j++)
						{
							if (WindowsFormsUtils.SafeCompareStrings(listViewItems[i].SubItems[j].Name, key, true))
							{
								foundItems.Add(listViewItems[i]);
								break;
							}
						}
					}
				}
				return foundItems;
			}

			/// <summary>Returns an enumerator to use to iterate through the item collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the item collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The owner <see cref="T:System.Windows.Forms.ListView" /> is in virtual mode.</exception>
			// Token: 0x06006A48 RID: 27208 RVA: 0x00189412 File Offset: 0x00187612
			public IEnumerator GetEnumerator()
			{
				if (this.InnerList.OwnerIsVirtualListView && !this.InnerList.OwnerIsDesignMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantGetEnumeratorInVirtualMode"));
				}
				return this.InnerList.GetEnumerator();
			}

			/// <summary>Returns the index within the collection of the specified item.</summary>
			/// <param name="item">A <see cref="T:System.Windows.Forms.ListViewItem" /> representing the item to locate in the collection.</param>
			/// <returns>The zero-based index of the item's location in the collection; otherwise, -1 if the item is not located in the collection.</returns>
			// Token: 0x06006A49 RID: 27209 RVA: 0x0018944C File Offset: 0x0018764C
			public int IndexOf(ListViewItem item)
			{
				for (int i = 0; i < this.Count; i++)
				{
					if (this[i] == item)
					{
						return i;
					}
				}
				return -1;
			}

			/// <summary>Returns the index within the collection of the specified item.</summary>
			/// <param name="item">A <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the item to locate in the collection.</param>
			/// <returns>The zero-based index of the item if it is in the collection; otherwise, -1.</returns>
			// Token: 0x06006A4A RID: 27210 RVA: 0x00189477 File Offset: 0x00187677
			int IList.IndexOf(object item)
			{
				if (item is ListViewItem)
				{
					return this.IndexOf((ListViewItem)item);
				}
				return -1;
			}

			/// <summary>Retrieves the index of the item with the specified key.</summary>
			/// <param name="key">The name of the item to find in the collection.</param>
			/// <returns>The zero-based index of the first occurrence of the item with the specified key, if found; otherwise, -1.</returns>
			// Token: 0x06006A4B RID: 27211 RVA: 0x00189490 File Offset: 0x00187690
			public virtual int IndexOfKey(string key)
			{
				if (string.IsNullOrEmpty(key))
				{
					return -1;
				}
				if (this.IsValidIndex(this.lastAccessedIndex) && WindowsFormsUtils.SafeCompareStrings(this[this.lastAccessedIndex].Name, key, true))
				{
					return this.lastAccessedIndex;
				}
				for (int i = 0; i < this.Count; i++)
				{
					if (WindowsFormsUtils.SafeCompareStrings(this[i].Name, key, true))
					{
						this.lastAccessedIndex = i;
						return i;
					}
				}
				this.lastAccessedIndex = -1;
				return -1;
			}

			// Token: 0x06006A4C RID: 27212 RVA: 0x0018950D File Offset: 0x0018770D
			private bool IsValidIndex(int index)
			{
				return index >= 0 && index < this.Count;
			}

			/// <summary>Inserts an existing <see cref="T:System.Windows.Forms.ListViewItem" /> into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the item is inserted.</param>
			/// <param name="item">The <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the item to insert.</param>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> that was inserted into the collection.</returns>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than 0 or greater than the value of the <see cref="P:System.Windows.Forms.ListView.ListViewItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" />.</exception>
			// Token: 0x06006A4D RID: 27213 RVA: 0x00189520 File Offset: 0x00187720
			public ListViewItem Insert(int index, ListViewItem item)
			{
				if (index < 0 || index > this.Count)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.InnerList.Insert(index, item);
				return item;
			}

			/// <summary>Creates a new item and inserts it into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the item is inserted.</param>
			/// <param name="text">The text to display for the item.</param>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> that was inserted into the collection.</returns>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than 0 or greater than the value of the <see cref="P:System.Windows.Forms.ListView.ListViewItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" />.</exception>
			// Token: 0x06006A4E RID: 27214 RVA: 0x0018957B File Offset: 0x0018777B
			public ListViewItem Insert(int index, string text)
			{
				return this.Insert(index, new ListViewItem(text));
			}

			/// <summary>Creates a new item with the specified image index and inserts it into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the item is inserted.</param>
			/// <param name="text">The text to display for the item.</param>
			/// <param name="imageIndex">The index of the image to display for the item.</param>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> that was inserted into the collection.</returns>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than 0 or greater than the value of the <see cref="P:System.Windows.Forms.ListView.ListViewItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" />.</exception>
			// Token: 0x06006A4F RID: 27215 RVA: 0x0018958A File Offset: 0x0018778A
			public ListViewItem Insert(int index, string text, int imageIndex)
			{
				return this.Insert(index, new ListViewItem(text, imageIndex));
			}

			/// <summary>Inserts an object into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the item is inserted.</param>
			/// <param name="item">The object that represents the item to insert.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The index parameter is less than 0 or greater than the value of the <see cref="P:System.Windows.Forms.ListView.ListViewItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" />.</exception>
			// Token: 0x06006A50 RID: 27216 RVA: 0x0018959A File Offset: 0x0018779A
			void IList.Insert(int index, object item)
			{
				if (item is ListViewItem)
				{
					this.Insert(index, (ListViewItem)item);
					return;
				}
				if (item != null)
				{
					this.Insert(index, item.ToString());
				}
			}

			/// <summary>Creates a new item with the specified text and image and inserts it in the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the item is inserted.</param>
			/// <param name="text">The text of the <see cref="T:System.Windows.Forms.ListViewItem" />.</param>
			/// <param name="imageKey">The key of the image to display for the item.</param>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> added to the collection.</returns>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than 0 or greater than the value of the <see cref="P:System.Windows.Forms.ListView.ListViewItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" />.</exception>
			// Token: 0x06006A51 RID: 27217 RVA: 0x001895C4 File Offset: 0x001877C4
			public ListViewItem Insert(int index, string text, string imageKey)
			{
				return this.Insert(index, new ListViewItem(text, imageKey));
			}

			/// <summary>Creates a new item with the specified key, text, and image, and adds it to the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the item is inserted.</param>
			/// <param name="key">The <see cref="P:System.Windows.Forms.ListViewItem.Name" /> of the item.</param>
			/// <param name="text">The text of the item.</param>
			/// <param name="imageKey">The key of the image to display for the item.</param>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> added to the collection.</returns>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than 0 or greater than the value of the <see cref="P:System.Windows.Forms.ListView.ListViewItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" />.</exception>
			// Token: 0x06006A52 RID: 27218 RVA: 0x001895D4 File Offset: 0x001877D4
			public virtual ListViewItem Insert(int index, string key, string text, string imageKey)
			{
				return this.Insert(index, new ListViewItem(text, imageKey)
				{
					Name = key
				});
			}

			/// <summary>Creates a new item with the specified key, text, and image, and inserts it in the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the item is inserted</param>
			/// <param name="key">The <see cref="P:System.Windows.Forms.ListViewItem.Name" /> of the item.</param>
			/// <param name="text">The text of the item.</param>
			/// <param name="imageIndex">The index of the image to display for the item.</param>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> added to the collection.</returns>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than 0 or greater than the value of the <see cref="P:System.Windows.Forms.ListView.ListViewItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" />.</exception>
			// Token: 0x06006A53 RID: 27219 RVA: 0x001895FC File Offset: 0x001877FC
			public virtual ListViewItem Insert(int index, string key, string text, int imageIndex)
			{
				return this.Insert(index, new ListViewItem(text, imageIndex)
				{
					Name = key
				});
			}

			/// <summary>Removes the specified item from the collection.</summary>
			/// <param name="item">A <see cref="T:System.Windows.Forms.ListViewItem" /> representing the item to remove from the collection.</param>
			/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Windows.Forms.ListViewItem" /> assigned to the <paramref name="item" /> parameter is <see langword="null" />.</exception>
			// Token: 0x06006A54 RID: 27220 RVA: 0x00189621 File Offset: 0x00187821
			public virtual void Remove(ListViewItem item)
			{
				this.InnerList.Remove(item);
			}

			/// <summary>Removes the item at the specified index within the collection.</summary>
			/// <param name="index">The zero-based index of the item to remove.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than 0 or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListView.ListViewItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" />.</exception>
			// Token: 0x06006A55 RID: 27221 RVA: 0x00189630 File Offset: 0x00187830
			public virtual void RemoveAt(int index)
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.InnerList.RemoveAt(index);
			}

			/// <summary>Removes the item with the specified key from the collection.</summary>
			/// <param name="key">The name of the item to remove from the collection.</param>
			// Token: 0x06006A56 RID: 27222 RVA: 0x00189688 File Offset: 0x00187888
			public virtual void RemoveByKey(string key)
			{
				int num = this.IndexOfKey(key);
				if (this.IsValidIndex(num))
				{
					this.RemoveAt(num);
				}
			}

			/// <summary>Removes the specified item from the collection.</summary>
			/// <param name="item">A <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the item to remove from the collection.</param>
			// Token: 0x06006A57 RID: 27223 RVA: 0x001896AD File Offset: 0x001878AD
			void IList.Remove(object item)
			{
				if (item == null || !(item is ListViewItem))
				{
					return;
				}
				this.Remove((ListViewItem)item);
			}

			// Token: 0x04003B37 RID: 15159
			private int lastAccessedIndex = -1;

			// Token: 0x04003B38 RID: 15160
			private ListView.ListViewItemCollection.IInnerList innerList;

			// Token: 0x020008C2 RID: 2242
			internal interface IInnerList
			{
				// Token: 0x17001936 RID: 6454
				// (get) Token: 0x060072B4 RID: 29364
				int Count { get; }

				// Token: 0x17001937 RID: 6455
				// (get) Token: 0x060072B5 RID: 29365
				bool OwnerIsVirtualListView { get; }

				// Token: 0x17001938 RID: 6456
				// (get) Token: 0x060072B6 RID: 29366
				bool OwnerIsDesignMode { get; }

				// Token: 0x17001939 RID: 6457
				ListViewItem this[int index] { get; set; }

				// Token: 0x060072B9 RID: 29369
				ListViewItem Add(ListViewItem item);

				// Token: 0x060072BA RID: 29370
				void AddRange(ListViewItem[] items);

				// Token: 0x060072BB RID: 29371
				void Clear();

				// Token: 0x060072BC RID: 29372
				bool Contains(ListViewItem item);

				// Token: 0x060072BD RID: 29373
				void CopyTo(Array dest, int index);

				// Token: 0x060072BE RID: 29374
				IEnumerator GetEnumerator();

				// Token: 0x060072BF RID: 29375
				int IndexOf(ListViewItem item);

				// Token: 0x060072C0 RID: 29376
				ListViewItem Insert(int index, ListViewItem item);

				// Token: 0x060072C1 RID: 29377
				void Remove(ListViewItem item);

				// Token: 0x060072C2 RID: 29378
				void RemoveAt(int index);
			}
		}

		// Token: 0x020006CB RID: 1739
		internal class ListViewNativeItemCollection : ListView.ListViewItemCollection.IInnerList
		{
			// Token: 0x06006A58 RID: 27224 RVA: 0x001896C7 File Offset: 0x001878C7
			public ListViewNativeItemCollection(ListView owner)
			{
				this.owner = owner;
			}

			// Token: 0x17001711 RID: 5905
			// (get) Token: 0x06006A59 RID: 27225 RVA: 0x001896D6 File Offset: 0x001878D6
			public int Count
			{
				get
				{
					this.owner.ApplyUpdateCachedItems();
					if (this.owner.VirtualMode)
					{
						return this.owner.VirtualListSize;
					}
					return this.owner.itemCount;
				}
			}

			// Token: 0x17001712 RID: 5906
			// (get) Token: 0x06006A5A RID: 27226 RVA: 0x00189707 File Offset: 0x00187907
			public bool OwnerIsVirtualListView
			{
				get
				{
					return this.owner.VirtualMode;
				}
			}

			// Token: 0x17001713 RID: 5907
			// (get) Token: 0x06006A5B RID: 27227 RVA: 0x00189714 File Offset: 0x00187914
			public bool OwnerIsDesignMode
			{
				get
				{
					return this.owner.DesignMode;
				}
			}

			// Token: 0x17001714 RID: 5908
			public ListViewItem this[int displayIndex]
			{
				get
				{
					this.owner.ApplyUpdateCachedItems();
					if (this.owner.VirtualMode)
					{
						RetrieveVirtualItemEventArgs retrieveVirtualItemEventArgs = new RetrieveVirtualItemEventArgs(displayIndex);
						this.owner.OnRetrieveVirtualItem(retrieveVirtualItemEventArgs);
						retrieveVirtualItemEventArgs.Item.SetItemIndex(this.owner, displayIndex);
						return retrieveVirtualItemEventArgs.Item;
					}
					if (displayIndex < 0 || displayIndex >= this.owner.itemCount)
					{
						throw new ArgumentOutOfRangeException("displayIndex", SR.GetString("InvalidArgument", new object[]
						{
							"displayIndex",
							displayIndex.ToString(CultureInfo.CurrentCulture)
						}));
					}
					if (this.owner.IsHandleCreated && !this.owner.ListViewHandleDestroyed)
					{
						return (ListViewItem)this.owner.listItemsTable[this.DisplayIndexToID(displayIndex)];
					}
					return (ListViewItem)this.owner.listItemsArray[displayIndex];
				}
				set
				{
					this.owner.ApplyUpdateCachedItems();
					if (this.owner.VirtualMode)
					{
						throw new InvalidOperationException(SR.GetString("ListViewCantModifyTheItemCollInAVirtualListView"));
					}
					if (displayIndex < 0 || displayIndex >= this.owner.itemCount)
					{
						throw new ArgumentOutOfRangeException("displayIndex", SR.GetString("InvalidArgument", new object[]
						{
							"displayIndex",
							displayIndex.ToString(CultureInfo.CurrentCulture)
						}));
					}
					if (this.owner.ExpectingMouseUp)
					{
						this.owner.ItemCollectionChangedInMouseDown = true;
					}
					this.RemoveAt(displayIndex);
					this.Insert(displayIndex, value);
				}
			}

			// Token: 0x06006A5E RID: 27230 RVA: 0x001898B0 File Offset: 0x00187AB0
			public ListViewItem Add(ListViewItem value)
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantAddItemsToAVirtualListView"));
				}
				bool @checked = value.Checked;
				this.owner.InsertItems(this.owner.itemCount, new ListViewItem[] { value }, true);
				if (this.owner.IsHandleCreated && !this.owner.CheckBoxes && @checked)
				{
					this.owner.UpdateSavedCheckedItems(value, true);
				}
				if (this.owner.ExpectingMouseUp)
				{
					this.owner.ItemCollectionChangedInMouseDown = true;
				}
				return value;
			}

			// Token: 0x06006A5F RID: 27231 RVA: 0x0018994C File Offset: 0x00187B4C
			public void AddRange(ListViewItem[] values)
			{
				if (values == null)
				{
					throw new ArgumentNullException("values");
				}
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantAddItemsToAVirtualListView"));
				}
				IComparer listItemSorter = this.owner.listItemSorter;
				this.owner.listItemSorter = null;
				bool[] array = null;
				if (this.owner.IsHandleCreated && !this.owner.CheckBoxes)
				{
					array = new bool[values.Length];
					for (int i = 0; i < values.Length; i++)
					{
						array[i] = values[i].Checked;
					}
				}
				try
				{
					this.owner.BeginUpdate();
					this.owner.InsertItems(this.owner.itemCount, values, true);
					if (this.owner.IsHandleCreated && !this.owner.CheckBoxes)
					{
						for (int j = 0; j < values.Length; j++)
						{
							if (array[j])
							{
								this.owner.UpdateSavedCheckedItems(values[j], true);
							}
						}
					}
				}
				finally
				{
					this.owner.listItemSorter = listItemSorter;
					this.owner.EndUpdate();
				}
				if (this.owner.ExpectingMouseUp)
				{
					this.owner.ItemCollectionChangedInMouseDown = true;
				}
				if (listItemSorter != null || (this.owner.Sorting != SortOrder.None && !this.owner.VirtualMode))
				{
					this.owner.Sort();
				}
			}

			// Token: 0x06006A60 RID: 27232 RVA: 0x00189AA8 File Offset: 0x00187CA8
			private int DisplayIndexToID(int displayIndex)
			{
				if (this.owner.IsHandleCreated && !this.owner.ListViewHandleDestroyed)
				{
					NativeMethods.LVITEM lvitem = default(NativeMethods.LVITEM);
					lvitem.mask = 4;
					lvitem.iItem = displayIndex;
					UnsafeNativeMethods.SendMessage(new HandleRef(this.owner, this.owner.Handle), NativeMethods.LVM_GETITEM, 0, ref lvitem);
					return (int)lvitem.lParam;
				}
				return this[displayIndex].ID;
			}

			// Token: 0x06006A61 RID: 27233 RVA: 0x00189B24 File Offset: 0x00187D24
			public void Clear()
			{
				if (this.owner.itemCount > 0)
				{
					this.owner.ApplyUpdateCachedItems();
					if (this.owner.IsHandleCreated && !this.owner.ListViewHandleDestroyed)
					{
						int count = this.owner.Items.Count;
						int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this.owner, this.owner.Handle), 4108, -1, 2);
						for (int i = 0; i < count; i++)
						{
							ListViewItem listViewItem = this.owner.Items[i];
							if (listViewItem != null)
							{
								if (i == num)
								{
									listViewItem.StateSelected = true;
									num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this.owner, this.owner.Handle), 4108, num, 2);
								}
								else
								{
									listViewItem.StateSelected = false;
								}
								listViewItem.UnHost(i, false);
							}
						}
						UnsafeNativeMethods.SendMessage(new HandleRef(this.owner, this.owner.Handle), 4105, 0, 0);
						if (this.owner.View == View.SmallIcon)
						{
							if (this.owner.ComctlSupportsVisualStyles)
							{
								this.owner.FlipViewToLargeIconAndSmallIcon = true;
							}
							else
							{
								this.owner.View = View.LargeIcon;
								this.owner.View = View.SmallIcon;
							}
						}
					}
					else
					{
						int count2 = this.owner.Items.Count;
						for (int j = 0; j < count2; j++)
						{
							ListViewItem listViewItem2 = this.owner.Items[j];
							if (listViewItem2 != null)
							{
								listViewItem2.UnHost(j, true);
							}
						}
						this.owner.listItemsArray.Clear();
					}
					this.owner.listItemsTable.Clear();
					if (this.owner.IsHandleCreated && !this.owner.CheckBoxes)
					{
						this.owner.savedCheckedItems = null;
					}
					this.owner.itemCount = 0;
					if (this.owner.ExpectingMouseUp)
					{
						this.owner.ItemCollectionChangedInMouseDown = true;
					}
				}
			}

			// Token: 0x06006A62 RID: 27234 RVA: 0x00189D24 File Offset: 0x00187F24
			public bool Contains(ListViewItem item)
			{
				this.owner.ApplyUpdateCachedItems();
				if (this.owner.IsHandleCreated && !this.owner.ListViewHandleDestroyed)
				{
					return this.owner.listItemsTable[item.ID] == item;
				}
				return this.owner.listItemsArray.Contains(item);
			}

			// Token: 0x06006A63 RID: 27235 RVA: 0x00189D88 File Offset: 0x00187F88
			public ListViewItem Insert(int index, ListViewItem item)
			{
				int num;
				if (this.owner.VirtualMode)
				{
					num = this.Count;
				}
				else
				{
					num = this.owner.itemCount;
				}
				if (index < 0 || index > num)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantAddItemsToAVirtualListView"));
				}
				if (index < num)
				{
					this.owner.ApplyUpdateCachedItems();
				}
				this.owner.InsertItems(index, new ListViewItem[] { item }, true);
				if (this.owner.IsHandleCreated && !this.owner.CheckBoxes && item.Checked)
				{
					this.owner.UpdateSavedCheckedItems(item, true);
				}
				if (this.owner.ExpectingMouseUp)
				{
					this.owner.ItemCollectionChangedInMouseDown = true;
				}
				return item;
			}

			// Token: 0x06006A64 RID: 27236 RVA: 0x00189E80 File Offset: 0x00188080
			public int IndexOf(ListViewItem item)
			{
				for (int i = 0; i < this.Count; i++)
				{
					if (item == this[i])
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x06006A65 RID: 27237 RVA: 0x00189EAC File Offset: 0x001880AC
			public void Remove(ListViewItem item)
			{
				int num = (this.owner.VirtualMode ? (this.Count - 1) : this.IndexOf(item));
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantRemoveItemsFromAVirtualListView"));
				}
				if (num != -1)
				{
					this.RemoveAt(num);
				}
			}

			// Token: 0x06006A66 RID: 27238 RVA: 0x00189F00 File Offset: 0x00188100
			public void RemoveAt(int index)
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantRemoveItemsFromAVirtualListView"));
				}
				if (index < 0 || index >= this.owner.itemCount)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this.owner.IsHandleCreated && !this.owner.CheckBoxes && this[index].Checked)
				{
					this.owner.UpdateSavedCheckedItems(this[index], false);
				}
				this.owner.ApplyUpdateCachedItems();
				int num = this.DisplayIndexToID(index);
				this[index].UnHost(true);
				if (this.owner.IsHandleCreated)
				{
					if ((int)(long)this.owner.SendMessage(4104, index, 0) == 0)
					{
						throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
				}
				else
				{
					this.owner.listItemsArray.RemoveAt(index);
				}
				this.owner.itemCount--;
				this.owner.listItemsTable.Remove(num);
				if (this.owner.ExpectingMouseUp)
				{
					this.owner.ItemCollectionChangedInMouseDown = true;
				}
			}

			// Token: 0x06006A67 RID: 27239 RVA: 0x0018A074 File Offset: 0x00188274
			public void CopyTo(Array dest, int index)
			{
				if (this.owner.itemCount > 0)
				{
					for (int i = 0; i < this.Count; i++)
					{
						dest.SetValue(this[i], index++);
					}
				}
			}

			// Token: 0x06006A68 RID: 27240 RVA: 0x0018A0B4 File Offset: 0x001882B4
			public IEnumerator GetEnumerator()
			{
				ListViewItem[] array = new ListViewItem[this.owner.itemCount];
				this.CopyTo(array, 0);
				return array.GetEnumerator();
			}

			// Token: 0x04003B39 RID: 15161
			private ListView owner;
		}

		// Token: 0x020006CC RID: 1740
		internal class ListViewAccessibleObject : Control.ControlAccessibleObject
		{
			// Token: 0x06006A69 RID: 27241 RVA: 0x0018A0E0 File Offset: 0x001882E0
			internal ListViewAccessibleObject(ListView owner)
				: base(owner)
			{
				this.owner = owner;
			}

			// Token: 0x06006A6A RID: 27242 RVA: 0x0018A0F0 File Offset: 0x001882F0
			internal override bool IsIAccessibleExSupported()
			{
				return this.owner != null || base.IsIAccessibleExSupported();
			}

			// Token: 0x06006A6B RID: 27243 RVA: 0x0018A104 File Offset: 0x00188304
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30026)
				{
					switch (this.owner.Sorting)
					{
					case SortOrder.None:
						return SR.GetString("NotSortedAccessibleStatus");
					case SortOrder.Ascending:
						return SR.GetString("SortedAscendingAccessibleStatus");
					case SortOrder.Descending:
						return SR.GetString("SortedDescendingAccessibleStatus");
					}
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x04003B3A RID: 15162
			private ListView owner;
		}
	}
}
