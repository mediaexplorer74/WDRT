using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System.Windows.Forms
{
	/// <summary>Represents an individual item that is displayed within a <see cref="T:System.Windows.Forms.MainMenu" /> or <see cref="T:System.Windows.Forms.ContextMenu" />. Although <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> replaces and adds functionality to the <see cref="T:System.Windows.Forms.MenuItem" /> control of previous versions, <see cref="T:System.Windows.Forms.MenuItem" /> is retained for both backward compatibility and future use if you choose.</summary>
	// Token: 0x020002F3 RID: 755
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	[DefaultEvent("Click")]
	[DefaultProperty("Text")]
	public class MenuItem : Menu
	{
		/// <summary>Initializes a <see cref="T:System.Windows.Forms.MenuItem" /> with a blank caption.</summary>
		// Token: 0x06002FC2 RID: 12226 RVA: 0x000D755C File Offset: 0x000D575C
		public MenuItem()
			: this(MenuMerge.Add, 0, Shortcut.None, null, null, null, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MenuItem" /> class with a specified caption for the menu item.</summary>
		/// <param name="text">The caption for the menu item.</param>
		// Token: 0x06002FC3 RID: 12227 RVA: 0x000D7578 File Offset: 0x000D5778
		public MenuItem(string text)
			: this(MenuMerge.Add, 0, Shortcut.None, text, null, null, null, null)
		{
		}

		/// <summary>Initializes a new instance of the class with a specified caption and event handler for the <see cref="E:System.Windows.Forms.MenuItem.Click" /> event of the menu item.</summary>
		/// <param name="text">The caption for the menu item.</param>
		/// <param name="onClick">The <see cref="T:System.EventHandler" /> that handles the <see cref="E:System.Windows.Forms.MenuItem.Click" /> event for this menu item.</param>
		// Token: 0x06002FC4 RID: 12228 RVA: 0x000D7594 File Offset: 0x000D5794
		public MenuItem(string text, EventHandler onClick)
			: this(MenuMerge.Add, 0, Shortcut.None, text, onClick, null, null, null)
		{
		}

		/// <summary>Initializes a new instance of the class with a specified caption, event handler, and associated shortcut key for the menu item.</summary>
		/// <param name="text">The caption for the menu item.</param>
		/// <param name="onClick">The <see cref="T:System.EventHandler" /> that handles the <see cref="E:System.Windows.Forms.MenuItem.Click" /> event for this menu item.</param>
		/// <param name="shortcut">One of the <see cref="T:System.Windows.Forms.Shortcut" /> values.</param>
		// Token: 0x06002FC5 RID: 12229 RVA: 0x000D75B0 File Offset: 0x000D57B0
		public MenuItem(string text, EventHandler onClick, Shortcut shortcut)
			: this(MenuMerge.Add, 0, shortcut, text, onClick, null, null, null)
		{
		}

		/// <summary>Initializes a new instance of the class with a specified caption and an array of submenu items defined for the menu item.</summary>
		/// <param name="text">The caption for the menu item.</param>
		/// <param name="items">An array of <see cref="T:System.Windows.Forms.MenuItem" /> objects that contains the submenu items for this menu item.</param>
		// Token: 0x06002FC6 RID: 12230 RVA: 0x000D75CC File Offset: 0x000D57CC
		public MenuItem(string text, MenuItem[] items)
			: this(MenuMerge.Add, 0, Shortcut.None, text, null, null, null, items)
		{
		}

		// Token: 0x06002FC7 RID: 12231 RVA: 0x000D75E7 File Offset: 0x000D57E7
		internal MenuItem(MenuItem.MenuItemData data)
		{
			this.msaaMenuInfoPtr = IntPtr.Zero;
			base..ctor(null);
			data.AddItem(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MenuItem" /> class with a specified caption; defined event-handlers for the <see cref="E:System.Windows.Forms.MenuItem.Click" />, <see cref="E:System.Windows.Forms.MenuItem.Select" /> and <see cref="E:System.Windows.Forms.MenuItem.Popup" /> events; a shortcut key; a merge type; and order specified for the menu item.</summary>
		/// <param name="mergeType">One of the <see cref="T:System.Windows.Forms.MenuMerge" /> values.</param>
		/// <param name="mergeOrder">The relative position that this menu item will take in a merged menu.</param>
		/// <param name="shortcut">One of the <see cref="T:System.Windows.Forms.Shortcut" /> values.</param>
		/// <param name="text">The caption for the menu item.</param>
		/// <param name="onClick">The <see cref="T:System.EventHandler" /> that handles the <see cref="E:System.Windows.Forms.MenuItem.Click" /> event for this menu item.</param>
		/// <param name="onPopup">The <see cref="T:System.EventHandler" /> that handles the <see cref="E:System.Windows.Forms.MenuItem.Popup" /> event for this menu item.</param>
		/// <param name="onSelect">The <see cref="T:System.EventHandler" /> that handles the <see cref="E:System.Windows.Forms.MenuItem.Select" /> event for this menu item.</param>
		/// <param name="items">An array of <see cref="T:System.Windows.Forms.MenuItem" /> objects that contains the submenu items for this menu item.</param>
		// Token: 0x06002FC8 RID: 12232 RVA: 0x000D7604 File Offset: 0x000D5804
		public MenuItem(MenuMerge mergeType, int mergeOrder, Shortcut shortcut, string text, EventHandler onClick, EventHandler onPopup, EventHandler onSelect, MenuItem[] items)
		{
			this.msaaMenuInfoPtr = IntPtr.Zero;
			base..ctor(items);
			new MenuItem.MenuItemData(this, mergeType, mergeOrder, shortcut, true, text, onClick, onPopup, onSelect, null, null);
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.MenuItem" /> is placed on a new line (for a menu item added to a <see cref="T:System.Windows.Forms.MainMenu" /> object) or in a new column (for a submenu item or menu item displayed in a <see cref="T:System.Windows.Forms.ContextMenu" />).</summary>
		/// <returns>
		///   <see langword="true" /> if the menu item is placed on a new line or in a new column; <see langword="false" /> if the menu item is left in its default placement. The default is <see langword="false" />.</returns>
		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x06002FC9 RID: 12233 RVA: 0x000D7639 File Offset: 0x000D5839
		// (set) Token: 0x06002FCA RID: 12234 RVA: 0x000D764C File Offset: 0x000D584C
		[Browsable(false)]
		[DefaultValue(false)]
		public bool BarBreak
		{
			get
			{
				return (this.data.State & 32) != 0;
			}
			set
			{
				this.data.SetState(32, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the item is placed on a new line (for a menu item added to a <see cref="T:System.Windows.Forms.MainMenu" /> object) or in a new column (for a menu item or submenu item displayed in a <see cref="T:System.Windows.Forms.ContextMenu" />).</summary>
		/// <returns>
		///   <see langword="true" /> if the menu item is placed on a new line or in a new column; <see langword="false" /> if the menu item is left in its default placement. The default is <see langword="false" />.</returns>
		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x06002FCB RID: 12235 RVA: 0x000D765C File Offset: 0x000D585C
		// (set) Token: 0x06002FCC RID: 12236 RVA: 0x000D766F File Offset: 0x000D586F
		[Browsable(false)]
		[DefaultValue(false)]
		public bool Break
		{
			get
			{
				return (this.data.State & 64) != 0;
			}
			set
			{
				this.data.SetState(64, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether a check mark appears next to the text of the menu item.</summary>
		/// <returns>
		///   <see langword="true" /> if there is a check mark next to the menu item; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Windows.Forms.MenuItem" /> is a top-level menu or has children.</exception>
		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x06002FCD RID: 12237 RVA: 0x000D767F File Offset: 0x000D587F
		// (set) Token: 0x06002FCE RID: 12238 RVA: 0x000D7691 File Offset: 0x000D5891
		[DefaultValue(false)]
		[SRDescription("MenuItemCheckedDescr")]
		public bool Checked
		{
			get
			{
				return (this.data.State & 8) != 0;
			}
			set
			{
				if (value && (base.ItemCount != 0 || (this.Parent != null && this.Parent is MainMenu)))
				{
					throw new ArgumentException(SR.GetString("MenuItemInvalidCheckProperty"));
				}
				this.data.SetState(8, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the menu item is the default menu item.</summary>
		/// <returns>
		///   <see langword="true" /> if the menu item is the default item in a menu; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x06002FCF RID: 12239 RVA: 0x000D76D0 File Offset: 0x000D58D0
		// (set) Token: 0x06002FD0 RID: 12240 RVA: 0x000D76E8 File Offset: 0x000D58E8
		[DefaultValue(false)]
		[SRDescription("MenuItemDefaultDescr")]
		public bool DefaultItem
		{
			get
			{
				return (this.data.State & 4096) != 0;
			}
			set
			{
				if (this.menu != null)
				{
					if (value)
					{
						UnsafeNativeMethods.SetMenuDefaultItem(new HandleRef(this.menu, this.menu.handle), this.MenuID, false);
					}
					else if (this.DefaultItem)
					{
						UnsafeNativeMethods.SetMenuDefaultItem(new HandleRef(this.menu, this.menu.handle), -1, false);
					}
				}
				this.data.SetState(4096, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the code that you provide draws the menu item or Windows draws the menu item.</summary>
		/// <returns>
		///   <see langword="true" /> if the menu item is to be drawn using code; <see langword="false" /> if the menu item is to be drawn by Windows. The default is <see langword="false" />.</returns>
		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x06002FD1 RID: 12241 RVA: 0x000D775C File Offset: 0x000D595C
		// (set) Token: 0x06002FD2 RID: 12242 RVA: 0x000D7772 File Offset: 0x000D5972
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("MenuItemOwnerDrawDescr")]
		public bool OwnerDraw
		{
			get
			{
				return (this.data.State & 256) != 0;
			}
			set
			{
				this.data.SetState(256, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the menu item is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if the menu item is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x06002FD3 RID: 12243 RVA: 0x000D7785 File Offset: 0x000D5985
		// (set) Token: 0x06002FD4 RID: 12244 RVA: 0x000D7797 File Offset: 0x000D5997
		[Localizable(true)]
		[DefaultValue(true)]
		[SRDescription("MenuItemEnabledDescr")]
		public bool Enabled
		{
			get
			{
				return (this.data.State & 3) == 0;
			}
			set
			{
				this.data.SetState(3, !value);
			}
		}

		/// <summary>Gets or sets a value indicating the position of the menu item in its parent menu.</summary>
		/// <returns>The zero-based index representing the position of the menu item in its parent menu.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is less than zero or greater than the item count.</exception>
		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x06002FD5 RID: 12245 RVA: 0x000D77AC File Offset: 0x000D59AC
		// (set) Token: 0x06002FD6 RID: 12246 RVA: 0x000D77EC File Offset: 0x000D59EC
		[Browsable(false)]
		public int Index
		{
			get
			{
				if (this.menu != null)
				{
					for (int i = 0; i < this.menu.ItemCount; i++)
					{
						if (this.menu.items[i] == this)
						{
							return i;
						}
					}
				}
				return -1;
			}
			set
			{
				int index = this.Index;
				if (index >= 0)
				{
					if (value < 0 || value >= this.menu.ItemCount)
					{
						throw new ArgumentOutOfRangeException("Index", SR.GetString("InvalidArgument", new object[]
						{
							"Index",
							value.ToString(CultureInfo.CurrentCulture)
						}));
					}
					if (value != index)
					{
						Menu menu = this.menu;
						menu.MenuItems.RemoveAt(index);
						menu.MenuItems.Add(value, this);
					}
				}
			}
		}

		/// <summary>Gets a value indicating whether the menu item contains child menu items.</summary>
		/// <returns>
		///   <see langword="true" /> if the menu item contains child menu items; <see langword="false" /> if the menu is a standalone menu item.</returns>
		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06002FD7 RID: 12247 RVA: 0x000D7870 File Offset: 0x000D5A70
		[Browsable(false)]
		public override bool IsParent
		{
			get
			{
				bool flag = false;
				if (this.data != null && this.MdiList)
				{
					for (int i = 0; i < base.ItemCount; i++)
					{
						if (!(this.items[i].data.UserData is MenuItem.MdiListUserData))
						{
							flag = true;
							break;
						}
					}
					if (!flag && this.FindMdiForms().Length != 0)
					{
						flag = true;
					}
					if (!flag && this.menu != null && !(this.menu is MenuItem))
					{
						flag = true;
					}
				}
				else
				{
					flag = base.IsParent;
				}
				return flag;
			}
		}

		/// <summary>Gets or sets a value indicating whether the menu item will be populated with a list of the Multiple Document Interface (MDI) child windows that are displayed within the associated form.</summary>
		/// <returns>
		///   <see langword="true" /> if a list of the MDI child windows is displayed in this menu item; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x06002FD8 RID: 12248 RVA: 0x000D78EF File Offset: 0x000D5AEF
		// (set) Token: 0x06002FD9 RID: 12249 RVA: 0x000D7905 File Offset: 0x000D5B05
		[DefaultValue(false)]
		[SRDescription("MenuItemMDIListDescr")]
		public bool MdiList
		{
			get
			{
				return (this.data.State & 131072) != 0;
			}
			set
			{
				this.data.MdiList = value;
				MenuItem.CleanListItems(this);
			}
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x06002FDA RID: 12250 RVA: 0x000D7919 File Offset: 0x000D5B19
		// (set) Token: 0x06002FDB RID: 12251 RVA: 0x000D7921 File Offset: 0x000D5B21
		internal Menu Menu
		{
			get
			{
				return this.menu;
			}
			set
			{
				this.menu = value;
			}
		}

		/// <summary>Gets a value indicating the Windows identifier for this menu item.</summary>
		/// <returns>The Windows identifier for this menu item.</returns>
		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x06002FDC RID: 12252 RVA: 0x000D792A File Offset: 0x000D5B2A
		protected int MenuID
		{
			get
			{
				return this.data.GetMenuID();
			}
		}

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x06002FDD RID: 12253 RVA: 0x000D7938 File Offset: 0x000D5B38
		internal bool Selected
		{
			get
			{
				if (this.menu == null)
				{
					return false;
				}
				NativeMethods.MENUITEMINFO_T menuiteminfo_T = new NativeMethods.MENUITEMINFO_T();
				menuiteminfo_T.cbSize = Marshal.SizeOf(typeof(NativeMethods.MENUITEMINFO_T));
				menuiteminfo_T.fMask = 1;
				UnsafeNativeMethods.GetMenuItemInfo(new HandleRef(this.menu, this.menu.handle), this.MenuID, false, menuiteminfo_T);
				return (menuiteminfo_T.fState & 128) != 0;
			}
		}

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x06002FDE RID: 12254 RVA: 0x000D79A4 File Offset: 0x000D5BA4
		internal int MenuIndex
		{
			get
			{
				if (this.menu == null)
				{
					return -1;
				}
				int menuItemCount = UnsafeNativeMethods.GetMenuItemCount(new HandleRef(this.menu, this.menu.Handle));
				int menuID = this.MenuID;
				NativeMethods.MENUITEMINFO_T menuiteminfo_T = new NativeMethods.MENUITEMINFO_T();
				menuiteminfo_T.cbSize = Marshal.SizeOf(typeof(NativeMethods.MENUITEMINFO_T));
				menuiteminfo_T.fMask = 6;
				for (int i = 0; i < menuItemCount; i++)
				{
					UnsafeNativeMethods.GetMenuItemInfo(new HandleRef(this.menu, this.menu.handle), i, true, menuiteminfo_T);
					if ((menuiteminfo_T.hSubMenu == IntPtr.Zero || menuiteminfo_T.hSubMenu == base.Handle) && menuiteminfo_T.wID == menuID)
					{
						return i;
					}
				}
				return -1;
			}
		}

		/// <summary>Gets or sets a value indicating the behavior of this menu item when its menu is merged with another.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.MenuMerge" /> value that represents the menu item's merge type.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.MenuMerge" /> values.</exception>
		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x06002FDF RID: 12255 RVA: 0x000D7A5C File Offset: 0x000D5C5C
		// (set) Token: 0x06002FE0 RID: 12256 RVA: 0x000D7A69 File Offset: 0x000D5C69
		[DefaultValue(MenuMerge.Add)]
		[SRDescription("MenuItemMergeTypeDescr")]
		public MenuMerge MergeType
		{
			get
			{
				return this.data.mergeType;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(MenuMerge));
				}
				this.data.MergeType = value;
			}
		}

		/// <summary>Gets or sets a value indicating the relative position of the menu item when it is merged with another.</summary>
		/// <returns>A zero-based index representing the merge order position for this menu item. The default is 0.</returns>
		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x06002FE1 RID: 12257 RVA: 0x000D7A9D File Offset: 0x000D5C9D
		// (set) Token: 0x06002FE2 RID: 12258 RVA: 0x000D7AAA File Offset: 0x000D5CAA
		[DefaultValue(0)]
		[SRDescription("MenuItemMergeOrderDescr")]
		public int MergeOrder
		{
			get
			{
				return this.data.mergeOrder;
			}
			set
			{
				this.data.MergeOrder = value;
			}
		}

		/// <summary>Gets a value indicating the mnemonic character that is associated with this menu item.</summary>
		/// <returns>A character that represents the mnemonic character associated with this menu item. Returns the NUL character (ASCII value 0) if no mnemonic character is specified in the text of the <see cref="T:System.Windows.Forms.MenuItem" />.</returns>
		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x06002FE3 RID: 12259 RVA: 0x000D7AB8 File Offset: 0x000D5CB8
		[Browsable(false)]
		public char Mnemonic
		{
			get
			{
				return this.data.Mnemonic;
			}
		}

		/// <summary>Gets a value indicating the menu that contains this menu item.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Menu" /> that represents the menu that contains this menu item.</returns>
		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x06002FE4 RID: 12260 RVA: 0x000D7919 File Offset: 0x000D5B19
		[Browsable(false)]
		public Menu Parent
		{
			get
			{
				return this.menu;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.MenuItem" />, if checked, displays a radio-button instead of a check mark.</summary>
		/// <returns>
		///   <see langword="true" /> if a radio-button is to be used instead of a check mark; <see langword="false" /> if the standard check mark is to be displayed when the menu item is checked. The default is <see langword="false" />.</returns>
		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x06002FE5 RID: 12261 RVA: 0x000D7AC5 File Offset: 0x000D5CC5
		// (set) Token: 0x06002FE6 RID: 12262 RVA: 0x000D7ADB File Offset: 0x000D5CDB
		[DefaultValue(false)]
		[SRDescription("MenuItemRadioCheckDescr")]
		public bool RadioCheck
		{
			get
			{
				return (this.data.State & 512) != 0;
			}
			set
			{
				this.data.SetState(512, value);
			}
		}

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x06002FE7 RID: 12263 RVA: 0x000D7AEE File Offset: 0x000D5CEE
		internal override bool RenderIsRightToLeft
		{
			get
			{
				return this.Parent != null && this.Parent.RenderIsRightToLeft;
			}
		}

		/// <summary>Gets or sets a value indicating the caption of the menu item.</summary>
		/// <returns>The text caption of the menu item.</returns>
		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x06002FE8 RID: 12264 RVA: 0x000D7B05 File Offset: 0x000D5D05
		// (set) Token: 0x06002FE9 RID: 12265 RVA: 0x000D7B12 File Offset: 0x000D5D12
		[Localizable(true)]
		[SRDescription("MenuItemTextDescr")]
		public string Text
		{
			get
			{
				return this.data.caption;
			}
			set
			{
				this.data.SetCaption(value);
			}
		}

		/// <summary>Gets or sets a value indicating the shortcut key associated with the menu item.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.Shortcut" /> values. The default is <see langword="Shortcut.None" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.Shortcut" /> values.</exception>
		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x06002FEA RID: 12266 RVA: 0x000D7B20 File Offset: 0x000D5D20
		// (set) Token: 0x06002FEB RID: 12267 RVA: 0x000D7B30 File Offset: 0x000D5D30
		[Localizable(true)]
		[DefaultValue(Shortcut.None)]
		[SRDescription("MenuItemShortCutDescr")]
		public Shortcut Shortcut
		{
			get
			{
				return this.data.shortcut;
			}
			set
			{
				if (!Enum.IsDefined(typeof(Shortcut), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(Shortcut));
				}
				this.data.shortcut = value;
				this.UpdateMenuItem(true);
			}
		}

		/// <summary>Gets or sets a value indicating whether the shortcut key that is associated with the menu item is displayed next to the menu item caption.</summary>
		/// <returns>
		///   <see langword="true" /> if the shortcut key combination is displayed next to the menu item caption; <see langword="false" /> if the shortcut key combination is not to be displayed. The default is <see langword="true" />.</returns>
		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x06002FEC RID: 12268 RVA: 0x000D7B7D File Offset: 0x000D5D7D
		// (set) Token: 0x06002FED RID: 12269 RVA: 0x000D7B8A File Offset: 0x000D5D8A
		[DefaultValue(true)]
		[Localizable(true)]
		[SRDescription("MenuItemShowShortCutDescr")]
		public bool ShowShortcut
		{
			get
			{
				return this.data.showShortcut;
			}
			set
			{
				if (value != this.data.showShortcut)
				{
					this.data.showShortcut = value;
					this.UpdateMenuItem(true);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the menu item is visible.</summary>
		/// <returns>
		///   <see langword="true" /> if the menu item will be made visible on the menu; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x06002FEE RID: 12270 RVA: 0x000D7BAD File Offset: 0x000D5DAD
		// (set) Token: 0x06002FEF RID: 12271 RVA: 0x000D7BC3 File Offset: 0x000D5DC3
		[Localizable(true)]
		[DefaultValue(true)]
		[SRDescription("MenuItemVisibleDescr")]
		public bool Visible
		{
			get
			{
				return (this.data.State & 65536) == 0;
			}
			set
			{
				this.data.Visible = value;
			}
		}

		/// <summary>Occurs when the menu item is clicked or selected using a shortcut key or access key defined for the menu item.</summary>
		// Token: 0x14000229 RID: 553
		// (add) Token: 0x06002FF0 RID: 12272 RVA: 0x000D7BD1 File Offset: 0x000D5DD1
		// (remove) Token: 0x06002FF1 RID: 12273 RVA: 0x000D7BEF File Offset: 0x000D5DEF
		[SRDescription("MenuItemOnClickDescr")]
		public event EventHandler Click
		{
			add
			{
				MenuItem.MenuItemData menuItemData = this.data;
				menuItemData.onClick = (EventHandler)Delegate.Combine(menuItemData.onClick, value);
			}
			remove
			{
				MenuItem.MenuItemData menuItemData = this.data;
				menuItemData.onClick = (EventHandler)Delegate.Remove(menuItemData.onClick, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.MenuItem.OwnerDraw" /> property of a menu item is set to <see langword="true" /> and a request is made to draw the menu item.</summary>
		// Token: 0x1400022A RID: 554
		// (add) Token: 0x06002FF2 RID: 12274 RVA: 0x000D7C0D File Offset: 0x000D5E0D
		// (remove) Token: 0x06002FF3 RID: 12275 RVA: 0x000D7C2B File Offset: 0x000D5E2B
		[SRCategory("CatBehavior")]
		[SRDescription("drawItemEventDescr")]
		public event DrawItemEventHandler DrawItem
		{
			add
			{
				MenuItem.MenuItemData menuItemData = this.data;
				menuItemData.onDrawItem = (DrawItemEventHandler)Delegate.Combine(menuItemData.onDrawItem, value);
			}
			remove
			{
				MenuItem.MenuItemData menuItemData = this.data;
				menuItemData.onDrawItem = (DrawItemEventHandler)Delegate.Remove(menuItemData.onDrawItem, value);
			}
		}

		/// <summary>Occurs when the menu needs to know the size of a menu item before drawing it.</summary>
		// Token: 0x1400022B RID: 555
		// (add) Token: 0x06002FF4 RID: 12276 RVA: 0x000D7C49 File Offset: 0x000D5E49
		// (remove) Token: 0x06002FF5 RID: 12277 RVA: 0x000D7C67 File Offset: 0x000D5E67
		[SRCategory("CatBehavior")]
		[SRDescription("measureItemEventDescr")]
		public event MeasureItemEventHandler MeasureItem
		{
			add
			{
				MenuItem.MenuItemData menuItemData = this.data;
				menuItemData.onMeasureItem = (MeasureItemEventHandler)Delegate.Combine(menuItemData.onMeasureItem, value);
			}
			remove
			{
				MenuItem.MenuItemData menuItemData = this.data;
				menuItemData.onMeasureItem = (MeasureItemEventHandler)Delegate.Remove(menuItemData.onMeasureItem, value);
			}
		}

		/// <summary>Occurs before a menu item's list of menu items is displayed.</summary>
		// Token: 0x1400022C RID: 556
		// (add) Token: 0x06002FF6 RID: 12278 RVA: 0x000D7C85 File Offset: 0x000D5E85
		// (remove) Token: 0x06002FF7 RID: 12279 RVA: 0x000D7CA3 File Offset: 0x000D5EA3
		[SRDescription("MenuItemOnInitDescr")]
		public event EventHandler Popup
		{
			add
			{
				MenuItem.MenuItemData menuItemData = this.data;
				menuItemData.onPopup = (EventHandler)Delegate.Combine(menuItemData.onPopup, value);
			}
			remove
			{
				MenuItem.MenuItemData menuItemData = this.data;
				menuItemData.onPopup = (EventHandler)Delegate.Remove(menuItemData.onPopup, value);
			}
		}

		/// <summary>Occurs when the user places the pointer over a menu item.</summary>
		// Token: 0x1400022D RID: 557
		// (add) Token: 0x06002FF8 RID: 12280 RVA: 0x000D7CC1 File Offset: 0x000D5EC1
		// (remove) Token: 0x06002FF9 RID: 12281 RVA: 0x000D7CDF File Offset: 0x000D5EDF
		[SRDescription("MenuItemOnSelectDescr")]
		public event EventHandler Select
		{
			add
			{
				MenuItem.MenuItemData menuItemData = this.data;
				menuItemData.onSelect = (EventHandler)Delegate.Combine(menuItemData.onSelect, value);
			}
			remove
			{
				MenuItem.MenuItemData menuItemData = this.data;
				menuItemData.onSelect = (EventHandler)Delegate.Remove(menuItemData.onSelect, value);
			}
		}

		// Token: 0x06002FFA RID: 12282 RVA: 0x000D7D00 File Offset: 0x000D5F00
		private static void CleanListItems(MenuItem senderMenu)
		{
			for (int i = senderMenu.MenuItems.Count - 1; i >= 0; i--)
			{
				MenuItem menuItem = senderMenu.MenuItems[i];
				if (menuItem.data.UserData is MenuItem.MdiListUserData)
				{
					menuItem.Dispose();
				}
			}
		}

		/// <summary>Creates a copy of the current <see cref="T:System.Windows.Forms.MenuItem" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.MenuItem" /> that represents the duplicated menu item.</returns>
		// Token: 0x06002FFB RID: 12283 RVA: 0x000D7D4C File Offset: 0x000D5F4C
		public virtual MenuItem CloneMenu()
		{
			MenuItem menuItem = new MenuItem();
			menuItem.CloneMenu(this);
			return menuItem;
		}

		/// <summary>Creates a copy of the specified <see cref="T:System.Windows.Forms.MenuItem" />.</summary>
		/// <param name="itemSrc">The <see cref="T:System.Windows.Forms.MenuItem" /> that represents the menu item to copy.</param>
		// Token: 0x06002FFC RID: 12284 RVA: 0x000D7D68 File Offset: 0x000D5F68
		protected void CloneMenu(MenuItem itemSrc)
		{
			base.CloneMenu(itemSrc);
			int state = itemSrc.data.State;
			new MenuItem.MenuItemData(this, itemSrc.MergeType, itemSrc.MergeOrder, itemSrc.Shortcut, itemSrc.ShowShortcut, itemSrc.Text, itemSrc.data.onClick, itemSrc.data.onPopup, itemSrc.data.onSelect, itemSrc.data.onDrawItem, itemSrc.data.onMeasureItem);
			this.data.SetState(state & 201579, true);
		}

		// Token: 0x06002FFD RID: 12285 RVA: 0x000D7DF8 File Offset: 0x000D5FF8
		internal virtual void CreateMenuItem()
		{
			if ((this.data.State & 65536) == 0)
			{
				NativeMethods.MENUITEMINFO_T menuiteminfo_T = this.CreateMenuItemInfo();
				UnsafeNativeMethods.InsertMenuItem(new HandleRef(this.menu, this.menu.handle), -1, true, menuiteminfo_T);
				this.hasHandle = menuiteminfo_T.hSubMenu != IntPtr.Zero;
				this.dataVersion = this.data.version;
				this.menuItemIsCreated = true;
				if (this.RenderIsRightToLeft)
				{
					this.Menu.UpdateRtl(true);
				}
			}
		}

		// Token: 0x06002FFE RID: 12286 RVA: 0x000D7E80 File Offset: 0x000D6080
		private NativeMethods.MENUITEMINFO_T CreateMenuItemInfo()
		{
			NativeMethods.MENUITEMINFO_T menuiteminfo_T = new NativeMethods.MENUITEMINFO_T();
			menuiteminfo_T.fMask = 55;
			menuiteminfo_T.fType = this.data.State & 864;
			bool flag = false;
			if (this.menu == base.GetMainMenu())
			{
				flag = true;
			}
			if (this.data.caption.Equals("-"))
			{
				if (flag)
				{
					this.data.caption = " ";
					menuiteminfo_T.fType |= 64;
				}
				else
				{
					menuiteminfo_T.fType |= 2048;
				}
			}
			menuiteminfo_T.fState = this.data.State & 4107;
			menuiteminfo_T.wID = this.MenuID;
			if (this.IsParent)
			{
				menuiteminfo_T.hSubMenu = base.Handle;
			}
			menuiteminfo_T.hbmpChecked = IntPtr.Zero;
			menuiteminfo_T.hbmpUnchecked = IntPtr.Zero;
			if (this.uniqueID == 0U)
			{
				Hashtable hashtable = MenuItem.allCreatedMenuItems;
				lock (hashtable)
				{
					this.uniqueID = (uint)Interlocked.Increment(ref MenuItem.nextUniqueID);
					MenuItem.allCreatedMenuItems.Add(this.uniqueID, new WeakReference(this));
				}
			}
			if (IntPtr.Size == 4)
			{
				if (this.data.OwnerDraw)
				{
					menuiteminfo_T.dwItemData = this.AllocMsaaMenuInfo();
				}
				else
				{
					menuiteminfo_T.dwItemData = (IntPtr)((int)this.uniqueID);
				}
			}
			else
			{
				menuiteminfo_T.dwItemData = this.AllocMsaaMenuInfo();
			}
			if (this.data.showShortcut && this.data.shortcut != Shortcut.None && !this.IsParent && !flag)
			{
				menuiteminfo_T.dwTypeData = this.data.caption + "\t" + TypeDescriptor.GetConverter(typeof(Keys)).ConvertToString((Keys)this.data.shortcut);
			}
			else
			{
				menuiteminfo_T.dwTypeData = ((this.data.caption.Length == 0) ? " " : this.data.caption);
			}
			menuiteminfo_T.cch = 0;
			return menuiteminfo_T;
		}

		/// <summary>Disposes of the resources (other than memory) used by the <see cref="T:System.Windows.Forms.MenuItem" />.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06002FFF RID: 12287 RVA: 0x000D8098 File Offset: 0x000D6298
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.menu != null)
				{
					this.menu.MenuItems.Remove(this);
				}
				if (this.data != null)
				{
					this.data.RemoveItem(this);
				}
				Hashtable hashtable = MenuItem.allCreatedMenuItems;
				lock (hashtable)
				{
					MenuItem.allCreatedMenuItems.Remove(this.uniqueID);
				}
				this.uniqueID = 0U;
			}
			this.FreeMsaaMenuInfo();
			base.Dispose(disposing);
		}

		// Token: 0x06003000 RID: 12288 RVA: 0x000D812C File Offset: 0x000D632C
		internal static MenuItem GetMenuItemFromUniqueID(uint uniqueID)
		{
			WeakReference weakReference = (WeakReference)MenuItem.allCreatedMenuItems[uniqueID];
			if (weakReference != null && weakReference.IsAlive)
			{
				return (MenuItem)weakReference.Target;
			}
			return null;
		}

		// Token: 0x06003001 RID: 12289 RVA: 0x000D8168 File Offset: 0x000D6368
		internal static MenuItem GetMenuItemFromItemData(IntPtr itemData)
		{
			if (itemData == IntPtr.Zero || itemData == (IntPtr)(-1))
			{
				return null;
			}
			uint num = (uint)(long)itemData;
			if (num == 0U)
			{
				return null;
			}
			if (IntPtr.Size == 4)
			{
				if (num < 3221225472U)
				{
					MenuItem.MsaaMenuInfoWithId msaaMenuInfoWithId = (MenuItem.MsaaMenuInfoWithId)Marshal.PtrToStructure(itemData, typeof(MenuItem.MsaaMenuInfoWithId));
					num = msaaMenuInfoWithId.uniqueID;
				}
			}
			else
			{
				MenuItem.MsaaMenuInfoWithId msaaMenuInfoWithId2 = (MenuItem.MsaaMenuInfoWithId)Marshal.PtrToStructure(itemData, typeof(MenuItem.MsaaMenuInfoWithId));
				num = msaaMenuInfoWithId2.uniqueID;
			}
			return MenuItem.GetMenuItemFromUniqueID(num);
		}

		// Token: 0x06003002 RID: 12290 RVA: 0x000D81F4 File Offset: 0x000D63F4
		private IntPtr AllocMsaaMenuInfo()
		{
			this.FreeMsaaMenuInfo();
			this.msaaMenuInfoPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(MenuItem.MsaaMenuInfoWithId)));
			int size = IntPtr.Size;
			MenuItem.MsaaMenuInfoWithId msaaMenuInfoWithId = new MenuItem.MsaaMenuInfoWithId(this.data.caption, this.uniqueID);
			Marshal.StructureToPtr(msaaMenuInfoWithId, this.msaaMenuInfoPtr, false);
			return this.msaaMenuInfoPtr;
		}

		// Token: 0x06003003 RID: 12291 RVA: 0x000D8259 File Offset: 0x000D6459
		private void FreeMsaaMenuInfo()
		{
			if (this.msaaMenuInfoPtr != IntPtr.Zero)
			{
				Marshal.DestroyStructure(this.msaaMenuInfoPtr, typeof(MenuItem.MsaaMenuInfoWithId));
				Marshal.FreeHGlobal(this.msaaMenuInfoPtr);
				this.msaaMenuInfoPtr = IntPtr.Zero;
			}
		}

		// Token: 0x06003004 RID: 12292 RVA: 0x000D8298 File Offset: 0x000D6498
		internal override void ItemsChanged(int change)
		{
			base.ItemsChanged(change);
			if (change == 0)
			{
				if (this.menu != null && this.menu.created)
				{
					this.UpdateMenuItem(true);
					base.CreateMenuItems();
					return;
				}
			}
			else
			{
				if (!this.hasHandle && this.IsParent)
				{
					this.UpdateMenuItem(true);
				}
				MainMenu mainMenu = base.GetMainMenu();
				if (mainMenu != null && (this.data.State & 512) == 0)
				{
					mainMenu.ItemsChanged(change, this);
				}
			}
		}

		// Token: 0x06003005 RID: 12293 RVA: 0x000D8310 File Offset: 0x000D6510
		internal void ItemsChanged(int change, MenuItem item)
		{
			if (change == 4 && this.data != null && this.data.baseItem != null && this.data.baseItem.MenuItems.Contains(item))
			{
				if (this.menu != null && this.menu.created)
				{
					this.UpdateMenuItem(true);
					base.CreateMenuItems();
					return;
				}
				if (this.data != null)
				{
					for (MenuItem firstItem = this.data.firstItem; firstItem != null; firstItem = firstItem.nextLinkedItem)
					{
						if (firstItem.created)
						{
							MenuItem menuItem = item.CloneMenu();
							item.data.AddItem(menuItem);
							firstItem.MenuItems.Add(menuItem);
							return;
						}
					}
				}
			}
		}

		// Token: 0x06003006 RID: 12294 RVA: 0x000D83C4 File Offset: 0x000D65C4
		internal Form[] FindMdiForms()
		{
			Form[] array = null;
			MainMenu mainMenu = base.GetMainMenu();
			Form form = null;
			if (mainMenu != null)
			{
				form = mainMenu.GetFormUnsafe();
			}
			if (form != null)
			{
				array = form.MdiChildren;
			}
			if (array == null)
			{
				array = new Form[0];
			}
			return array;
		}

		// Token: 0x06003007 RID: 12295 RVA: 0x000D83FC File Offset: 0x000D65FC
		private void PopulateMdiList()
		{
			this.data.SetState(512, true);
			try
			{
				MenuItem.CleanListItems(this);
				Form[] array = this.FindMdiForms();
				if (array != null && array.Length != 0)
				{
					Form activeMdiChild = base.GetMainMenu().GetFormUnsafe().ActiveMdiChild;
					if (this.MenuItems.Count > 0)
					{
						MenuItem menuItem = (MenuItem)Activator.CreateInstance(base.GetType());
						menuItem.data.UserData = new MenuItem.MdiListUserData();
						menuItem.Text = "-";
						this.MenuItems.Add(menuItem);
					}
					int num = 0;
					int num2 = 1;
					int num3 = 0;
					bool flag = false;
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i].Visible)
						{
							num++;
							if ((flag && num3 < 9) || (!flag && num3 < 8) || array[i].Equals(activeMdiChild))
							{
								MenuItem menuItem2 = (MenuItem)Activator.CreateInstance(base.GetType());
								menuItem2.data.UserData = new MenuItem.MdiListFormData(this, i);
								if (array[i].Equals(activeMdiChild))
								{
									menuItem2.Checked = true;
									flag = true;
								}
								menuItem2.Text = string.Format(CultureInfo.CurrentUICulture, "&{0} {1}", new object[]
								{
									num2,
									array[i].Text
								});
								num2++;
								num3++;
								this.MenuItems.Add(menuItem2);
							}
						}
					}
					if (num > 9)
					{
						MenuItem menuItem3 = (MenuItem)Activator.CreateInstance(base.GetType());
						menuItem3.data.UserData = new MenuItem.MdiListMoreWindowsData(this);
						menuItem3.Text = SR.GetString("MDIMenuMoreWindows");
						this.MenuItems.Add(menuItem3);
					}
				}
			}
			finally
			{
				this.data.SetState(512, false);
			}
		}

		/// <summary>Merges this <see cref="T:System.Windows.Forms.MenuItem" /> with another <see cref="T:System.Windows.Forms.MenuItem" /> and returns the resulting merged <see cref="T:System.Windows.Forms.MenuItem" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.MenuItem" /> that represents the merged menu item.</returns>
		// Token: 0x06003008 RID: 12296 RVA: 0x000D85EC File Offset: 0x000D67EC
		public virtual MenuItem MergeMenu()
		{
			MenuItem menuItem = (MenuItem)Activator.CreateInstance(base.GetType());
			this.data.AddItem(menuItem);
			menuItem.MergeMenu(this);
			return menuItem;
		}

		/// <summary>Merges another menu item with this menu item.</summary>
		/// <param name="itemSrc">A <see cref="T:System.Windows.Forms.MenuItem" /> that specifies the menu item to merge with this one.</param>
		// Token: 0x06003009 RID: 12297 RVA: 0x000D861E File Offset: 0x000D681E
		public void MergeMenu(MenuItem itemSrc)
		{
			base.MergeMenu(itemSrc);
			itemSrc.data.AddItem(this);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.MenuItem.Click" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600300A RID: 12298 RVA: 0x000D8634 File Offset: 0x000D6834
		protected virtual void OnClick(EventArgs e)
		{
			if (this.data.UserData is MenuItem.MdiListUserData)
			{
				((MenuItem.MdiListUserData)this.data.UserData).OnClick(e);
				return;
			}
			if (this.data.baseItem != this)
			{
				this.data.baseItem.OnClick(e);
				return;
			}
			if (this.data.onClick != null)
			{
				this.data.onClick(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.MenuItem.DrawItem" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DrawItemEventArgs" /> that contains the event data.</param>
		// Token: 0x0600300B RID: 12299 RVA: 0x000D86AC File Offset: 0x000D68AC
		protected virtual void OnDrawItem(DrawItemEventArgs e)
		{
			if (this.data.baseItem != this)
			{
				this.data.baseItem.OnDrawItem(e);
				return;
			}
			if (this.data.onDrawItem != null)
			{
				this.data.onDrawItem(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.MenuItem.MeasureItem" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MeasureItemEventArgs" /> that contains the event data.</param>
		// Token: 0x0600300C RID: 12300 RVA: 0x000D86F8 File Offset: 0x000D68F8
		protected virtual void OnMeasureItem(MeasureItemEventArgs e)
		{
			if (this.data.baseItem != this)
			{
				this.data.baseItem.OnMeasureItem(e);
				return;
			}
			if (this.data.onMeasureItem != null)
			{
				this.data.onMeasureItem(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.MenuItem.Popup" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600300D RID: 12301 RVA: 0x000D8744 File Offset: 0x000D6944
		protected virtual void OnPopup(EventArgs e)
		{
			bool flag = false;
			for (int i = 0; i < base.ItemCount; i++)
			{
				if (this.items[i].MdiList)
				{
					flag = true;
					this.items[i].UpdateMenuItem(true);
				}
			}
			if (flag || (this.hasHandle && !this.IsParent))
			{
				this.UpdateMenuItem(true);
			}
			if (this.data.baseItem != this)
			{
				this.data.baseItem.OnPopup(e);
			}
			else if (this.data.onPopup != null)
			{
				this.data.onPopup(this, e);
			}
			for (int j = 0; j < base.ItemCount; j++)
			{
				this.items[j].UpdateMenuItemIfDirty();
			}
			if (this.MdiList)
			{
				this.PopulateMdiList();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.MenuItem.Select" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600300E RID: 12302 RVA: 0x000D880C File Offset: 0x000D6A0C
		protected virtual void OnSelect(EventArgs e)
		{
			if (this.data.baseItem != this)
			{
				this.data.baseItem.OnSelect(e);
				return;
			}
			if (this.data.onSelect != null)
			{
				this.data.onSelect(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.MenuItem.Popup" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600300F RID: 12303 RVA: 0x000D8858 File Offset: 0x000D6A58
		protected virtual void OnInitMenuPopup(EventArgs e)
		{
			this.OnPopup(e);
		}

		// Token: 0x06003010 RID: 12304 RVA: 0x000D8861 File Offset: 0x000D6A61
		internal virtual void _OnInitMenuPopup(EventArgs e)
		{
			this.OnInitMenuPopup(e);
		}

		/// <summary>Generates a <see cref="E:System.Windows.Forms.Control.Click" /> event for the <see cref="T:System.Windows.Forms.MenuItem" />, simulating a click by a user.</summary>
		// Token: 0x06003011 RID: 12305 RVA: 0x000D886A File Offset: 0x000D6A6A
		public void PerformClick()
		{
			this.OnClick(EventArgs.Empty);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.MenuItem.Select" /> event for this menu item.</summary>
		// Token: 0x06003012 RID: 12306 RVA: 0x000D8877 File Offset: 0x000D6A77
		public virtual void PerformSelect()
		{
			this.OnSelect(EventArgs.Empty);
		}

		// Token: 0x06003013 RID: 12307 RVA: 0x000D8884 File Offset: 0x000D6A84
		internal virtual bool ShortcutClick()
		{
			if (this.menu is MenuItem)
			{
				MenuItem menuItem = (MenuItem)this.menu;
				if (!menuItem.ShortcutClick() || this.menu != menuItem)
				{
					return false;
				}
			}
			if ((this.data.State & 3) != 0)
			{
				return false;
			}
			if (base.ItemCount > 0)
			{
				this.OnPopup(EventArgs.Empty);
			}
			else
			{
				this.OnClick(EventArgs.Empty);
			}
			return true;
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.MenuItem" />.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.MenuItem" />. The string includes the type and the <see cref="P:System.Windows.Forms.MenuItem.Text" /> property of the control.</returns>
		// Token: 0x06003014 RID: 12308 RVA: 0x000D88F0 File Offset: 0x000D6AF0
		public override string ToString()
		{
			string text = base.ToString();
			string text2 = string.Empty;
			if (this.data != null && this.data.caption != null)
			{
				text2 = this.data.caption;
			}
			return text + ", Text: " + text2;
		}

		// Token: 0x06003015 RID: 12309 RVA: 0x000D8937 File Offset: 0x000D6B37
		internal void UpdateMenuItemIfDirty()
		{
			if (this.dataVersion != this.data.version)
			{
				this.UpdateMenuItem(true);
			}
		}

		// Token: 0x06003016 RID: 12310 RVA: 0x000D8954 File Offset: 0x000D6B54
		internal void UpdateItemRtl(bool setRightToLeftBit)
		{
			if (!this.menuItemIsCreated)
			{
				return;
			}
			NativeMethods.MENUITEMINFO_T menuiteminfo_T = new NativeMethods.MENUITEMINFO_T();
			menuiteminfo_T.fMask = 21;
			menuiteminfo_T.dwTypeData = new string('\0', this.Text.Length + 2);
			menuiteminfo_T.cbSize = Marshal.SizeOf(typeof(NativeMethods.MENUITEMINFO_T));
			menuiteminfo_T.cch = menuiteminfo_T.dwTypeData.Length - 1;
			UnsafeNativeMethods.GetMenuItemInfo(new HandleRef(this.menu, this.menu.handle), this.MenuID, false, menuiteminfo_T);
			if (setRightToLeftBit)
			{
				menuiteminfo_T.fType |= 24576;
			}
			else
			{
				menuiteminfo_T.fType &= -24577;
			}
			UnsafeNativeMethods.SetMenuItemInfo(new HandleRef(this.menu, this.menu.handle), this.MenuID, false, menuiteminfo_T);
		}

		// Token: 0x06003017 RID: 12311 RVA: 0x000D8A2C File Offset: 0x000D6C2C
		internal void UpdateMenuItem(bool force)
		{
			if (this.menu == null || !this.menu.created)
			{
				return;
			}
			if (force || this.menu is MainMenu || this.menu is ContextMenu)
			{
				NativeMethods.MENUITEMINFO_T menuiteminfo_T = this.CreateMenuItemInfo();
				UnsafeNativeMethods.SetMenuItemInfo(new HandleRef(this.menu, this.menu.handle), this.MenuID, false, menuiteminfo_T);
				if (this.hasHandle && menuiteminfo_T.hSubMenu == IntPtr.Zero)
				{
					base.ClearHandles();
				}
				this.hasHandle = menuiteminfo_T.hSubMenu != IntPtr.Zero;
				this.dataVersion = this.data.version;
				if (this.menu is MainMenu)
				{
					Form formUnsafe = ((MainMenu)this.menu).GetFormUnsafe();
					if (formUnsafe != null)
					{
						SafeNativeMethods.DrawMenuBar(new HandleRef(formUnsafe, formUnsafe.Handle));
					}
				}
			}
		}

		// Token: 0x06003018 RID: 12312 RVA: 0x000D8B14 File Offset: 0x000D6D14
		internal void WmDrawItem(ref Message m)
		{
			NativeMethods.DRAWITEMSTRUCT drawitemstruct = (NativeMethods.DRAWITEMSTRUCT)m.GetLParam(typeof(NativeMethods.DRAWITEMSTRUCT));
			IntPtr intPtr = Control.SetUpPalette(drawitemstruct.hDC, false, false);
			try
			{
				Graphics graphics = Graphics.FromHdcInternal(drawitemstruct.hDC);
				try
				{
					this.OnDrawItem(new DrawItemEventArgs(graphics, SystemInformation.MenuFont, Rectangle.FromLTRB(drawitemstruct.rcItem.left, drawitemstruct.rcItem.top, drawitemstruct.rcItem.right, drawitemstruct.rcItem.bottom), this.Index, (DrawItemState)drawitemstruct.itemState));
				}
				finally
				{
					graphics.Dispose();
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					SafeNativeMethods.SelectPalette(new HandleRef(null, drawitemstruct.hDC), new HandleRef(null, intPtr), 0);
				}
			}
			m.Result = (IntPtr)1;
		}

		// Token: 0x06003019 RID: 12313 RVA: 0x000D8BF8 File Offset: 0x000D6DF8
		internal void WmMeasureItem(ref Message m)
		{
			NativeMethods.MEASUREITEMSTRUCT measureitemstruct = (NativeMethods.MEASUREITEMSTRUCT)m.GetLParam(typeof(NativeMethods.MEASUREITEMSTRUCT));
			IntPtr dc = UnsafeNativeMethods.GetDC(NativeMethods.NullHandleRef);
			Graphics graphics = Graphics.FromHdcInternal(dc);
			MeasureItemEventArgs measureItemEventArgs = new MeasureItemEventArgs(graphics, this.Index);
			try
			{
				this.OnMeasureItem(measureItemEventArgs);
			}
			finally
			{
				graphics.Dispose();
			}
			UnsafeNativeMethods.ReleaseDC(NativeMethods.NullHandleRef, new HandleRef(null, dc));
			measureitemstruct.itemHeight = measureItemEventArgs.ItemHeight;
			measureitemstruct.itemWidth = measureItemEventArgs.ItemWidth;
			Marshal.StructureToPtr(measureitemstruct, m.LParam, false);
			m.Result = (IntPtr)1;
		}

		// Token: 0x040013B6 RID: 5046
		internal const int STATE_BARBREAK = 32;

		// Token: 0x040013B7 RID: 5047
		internal const int STATE_BREAK = 64;

		// Token: 0x040013B8 RID: 5048
		internal const int STATE_CHECKED = 8;

		// Token: 0x040013B9 RID: 5049
		internal const int STATE_DEFAULT = 4096;

		// Token: 0x040013BA RID: 5050
		internal const int STATE_DISABLED = 3;

		// Token: 0x040013BB RID: 5051
		internal const int STATE_RADIOCHECK = 512;

		// Token: 0x040013BC RID: 5052
		internal const int STATE_HIDDEN = 65536;

		// Token: 0x040013BD RID: 5053
		internal const int STATE_MDILIST = 131072;

		// Token: 0x040013BE RID: 5054
		internal const int STATE_CLONE_MASK = 201579;

		// Token: 0x040013BF RID: 5055
		internal const int STATE_OWNERDRAW = 256;

		// Token: 0x040013C0 RID: 5056
		internal const int STATE_INMDIPOPUP = 512;

		// Token: 0x040013C1 RID: 5057
		internal const int STATE_HILITE = 128;

		// Token: 0x040013C2 RID: 5058
		private Menu menu;

		// Token: 0x040013C3 RID: 5059
		private bool hasHandle;

		// Token: 0x040013C4 RID: 5060
		private MenuItem.MenuItemData data;

		// Token: 0x040013C5 RID: 5061
		private int dataVersion;

		// Token: 0x040013C6 RID: 5062
		private MenuItem nextLinkedItem;

		// Token: 0x040013C7 RID: 5063
		private static Hashtable allCreatedMenuItems = new Hashtable();

		// Token: 0x040013C8 RID: 5064
		private const uint firstUniqueID = 3221225472U;

		// Token: 0x040013C9 RID: 5065
		private static long nextUniqueID = (long)((ulong)(-1073741824));

		// Token: 0x040013CA RID: 5066
		private uint uniqueID;

		// Token: 0x040013CB RID: 5067
		private IntPtr msaaMenuInfoPtr;

		// Token: 0x040013CC RID: 5068
		private bool menuItemIsCreated;

		// Token: 0x020006D6 RID: 1750
		private struct MsaaMenuInfoWithId
		{
			// Token: 0x06006ADC RID: 27356 RVA: 0x0018B6C3 File Offset: 0x001898C3
			public MsaaMenuInfoWithId(string text, uint uniqueID)
			{
				this.msaaMenuInfo = new NativeMethods.MSAAMENUINFO(text);
				this.uniqueID = uniqueID;
			}

			// Token: 0x04003B47 RID: 15175
			public NativeMethods.MSAAMENUINFO msaaMenuInfo;

			// Token: 0x04003B48 RID: 15176
			public uint uniqueID;
		}

		// Token: 0x020006D7 RID: 1751
		internal class MenuItemData : ICommandExecutor
		{
			// Token: 0x06006ADD RID: 27357 RVA: 0x0018B6D8 File Offset: 0x001898D8
			internal MenuItemData(MenuItem baseItem, MenuMerge mergeType, int mergeOrder, Shortcut shortcut, bool showShortcut, string caption, EventHandler onClick, EventHandler onPopup, EventHandler onSelect, DrawItemEventHandler onDrawItem, MeasureItemEventHandler onMeasureItem)
			{
				this.AddItem(baseItem);
				this.mergeType = mergeType;
				this.mergeOrder = mergeOrder;
				this.shortcut = shortcut;
				this.showShortcut = showShortcut;
				this.caption = ((caption == null) ? "" : caption);
				this.onClick = onClick;
				this.onPopup = onPopup;
				this.onSelect = onSelect;
				this.onDrawItem = onDrawItem;
				this.onMeasureItem = onMeasureItem;
				this.version = 1;
				this.mnemonic = -1;
			}

			// Token: 0x17001732 RID: 5938
			// (get) Token: 0x06006ADE RID: 27358 RVA: 0x0018B759 File Offset: 0x00189959
			// (set) Token: 0x06006ADF RID: 27359 RVA: 0x0018B76A File Offset: 0x0018996A
			internal bool OwnerDraw
			{
				get
				{
					return (this.State & 256) != 0;
				}
				set
				{
					this.SetState(256, value);
				}
			}

			// Token: 0x17001733 RID: 5939
			// (get) Token: 0x06006AE0 RID: 27360 RVA: 0x0018B778 File Offset: 0x00189978
			// (set) Token: 0x06006AE1 RID: 27361 RVA: 0x0018B788 File Offset: 0x00189988
			internal bool MdiList
			{
				get
				{
					return this.HasState(131072);
				}
				set
				{
					if ((this.state & 131072) != 0 != value)
					{
						this.SetState(131072, value);
						for (MenuItem nextLinkedItem = this.firstItem; nextLinkedItem != null; nextLinkedItem = nextLinkedItem.nextLinkedItem)
						{
							nextLinkedItem.ItemsChanged(2);
						}
					}
				}
			}

			// Token: 0x17001734 RID: 5940
			// (get) Token: 0x06006AE2 RID: 27362 RVA: 0x0018B7CD File Offset: 0x001899CD
			// (set) Token: 0x06006AE3 RID: 27363 RVA: 0x0018B7D5 File Offset: 0x001899D5
			internal MenuMerge MergeType
			{
				get
				{
					return this.mergeType;
				}
				set
				{
					if (this.mergeType != value)
					{
						this.mergeType = value;
						this.ItemsChanged(3);
					}
				}
			}

			// Token: 0x17001735 RID: 5941
			// (get) Token: 0x06006AE4 RID: 27364 RVA: 0x0018B7EE File Offset: 0x001899EE
			// (set) Token: 0x06006AE5 RID: 27365 RVA: 0x0018B7F6 File Offset: 0x001899F6
			internal int MergeOrder
			{
				get
				{
					return this.mergeOrder;
				}
				set
				{
					if (this.mergeOrder != value)
					{
						this.mergeOrder = value;
						this.ItemsChanged(3);
					}
				}
			}

			// Token: 0x17001736 RID: 5942
			// (get) Token: 0x06006AE6 RID: 27366 RVA: 0x0018B80F File Offset: 0x00189A0F
			internal char Mnemonic
			{
				get
				{
					if (this.mnemonic == -1)
					{
						this.mnemonic = (short)WindowsFormsUtils.GetMnemonic(this.caption, true);
					}
					return (char)this.mnemonic;
				}
			}

			// Token: 0x17001737 RID: 5943
			// (get) Token: 0x06006AE7 RID: 27367 RVA: 0x0018B834 File Offset: 0x00189A34
			internal int State
			{
				get
				{
					return this.state;
				}
			}

			// Token: 0x17001738 RID: 5944
			// (get) Token: 0x06006AE8 RID: 27368 RVA: 0x0018B83C File Offset: 0x00189A3C
			// (set) Token: 0x06006AE9 RID: 27369 RVA: 0x0018B84D File Offset: 0x00189A4D
			internal bool Visible
			{
				get
				{
					return (this.state & 65536) == 0;
				}
				set
				{
					if ((this.state & 65536) == 0 != value)
					{
						this.state = (value ? (this.state & -65537) : (this.state | 65536));
						this.ItemsChanged(1);
					}
				}
			}

			// Token: 0x17001739 RID: 5945
			// (get) Token: 0x06006AEA RID: 27370 RVA: 0x0018B88B File Offset: 0x00189A8B
			// (set) Token: 0x06006AEB RID: 27371 RVA: 0x0018B893 File Offset: 0x00189A93
			internal object UserData
			{
				get
				{
					return this.userData;
				}
				set
				{
					this.userData = value;
				}
			}

			// Token: 0x06006AEC RID: 27372 RVA: 0x0018B89C File Offset: 0x00189A9C
			internal void AddItem(MenuItem item)
			{
				if (item.data != this)
				{
					if (item.data != null)
					{
						item.data.RemoveItem(item);
					}
					item.nextLinkedItem = this.firstItem;
					this.firstItem = item;
					if (this.baseItem == null)
					{
						this.baseItem = item;
					}
					item.data = this;
					item.dataVersion = 0;
					item.UpdateMenuItem(false);
				}
			}

			// Token: 0x06006AED RID: 27373 RVA: 0x0018B8FD File Offset: 0x00189AFD
			public void Execute()
			{
				if (this.baseItem != null)
				{
					this.baseItem.OnClick(EventArgs.Empty);
				}
			}

			// Token: 0x06006AEE RID: 27374 RVA: 0x0018B917 File Offset: 0x00189B17
			internal int GetMenuID()
			{
				if (this.cmd == null)
				{
					this.cmd = new Command(this);
				}
				return this.cmd.ID;
			}

			// Token: 0x06006AEF RID: 27375 RVA: 0x0018B938 File Offset: 0x00189B38
			internal void ItemsChanged(int change)
			{
				for (MenuItem nextLinkedItem = this.firstItem; nextLinkedItem != null; nextLinkedItem = nextLinkedItem.nextLinkedItem)
				{
					if (nextLinkedItem.menu != null)
					{
						nextLinkedItem.menu.ItemsChanged(change);
					}
				}
			}

			// Token: 0x06006AF0 RID: 27376 RVA: 0x0018B96C File Offset: 0x00189B6C
			internal void RemoveItem(MenuItem item)
			{
				if (item == this.firstItem)
				{
					this.firstItem = item.nextLinkedItem;
				}
				else
				{
					MenuItem nextLinkedItem = this.firstItem;
					while (item != nextLinkedItem.nextLinkedItem)
					{
						nextLinkedItem = nextLinkedItem.nextLinkedItem;
					}
					nextLinkedItem.nextLinkedItem = item.nextLinkedItem;
				}
				item.nextLinkedItem = null;
				item.data = null;
				item.dataVersion = 0;
				if (item == this.baseItem)
				{
					this.baseItem = this.firstItem;
				}
				if (this.firstItem == null)
				{
					this.onClick = null;
					this.onPopup = null;
					this.onSelect = null;
					this.onDrawItem = null;
					this.onMeasureItem = null;
					if (this.cmd != null)
					{
						this.cmd.Dispose();
						this.cmd = null;
					}
				}
			}

			// Token: 0x06006AF1 RID: 27377 RVA: 0x0018BA24 File Offset: 0x00189C24
			internal void SetCaption(string value)
			{
				if (value == null)
				{
					value = "";
				}
				if (!this.caption.Equals(value))
				{
					this.caption = value;
					this.UpdateMenuItems();
				}
			}

			// Token: 0x06006AF2 RID: 27378 RVA: 0x0018BA4B File Offset: 0x00189C4B
			internal bool HasState(int flag)
			{
				return (this.State & flag) == flag;
			}

			// Token: 0x06006AF3 RID: 27379 RVA: 0x0018BA58 File Offset: 0x00189C58
			internal void SetState(int flag, bool value)
			{
				if ((this.state & flag) != 0 != value)
				{
					this.state = (value ? (this.state | flag) : (this.state & ~flag));
					this.UpdateMenuItems();
				}
			}

			// Token: 0x06006AF4 RID: 27380 RVA: 0x0018BA8C File Offset: 0x00189C8C
			internal void UpdateMenuItems()
			{
				this.version++;
				for (MenuItem nextLinkedItem = this.firstItem; nextLinkedItem != null; nextLinkedItem = nextLinkedItem.nextLinkedItem)
				{
					nextLinkedItem.UpdateMenuItem(true);
				}
			}

			// Token: 0x04003B49 RID: 15177
			internal MenuItem baseItem;

			// Token: 0x04003B4A RID: 15178
			internal MenuItem firstItem;

			// Token: 0x04003B4B RID: 15179
			private int state;

			// Token: 0x04003B4C RID: 15180
			internal int version;

			// Token: 0x04003B4D RID: 15181
			internal MenuMerge mergeType;

			// Token: 0x04003B4E RID: 15182
			internal int mergeOrder;

			// Token: 0x04003B4F RID: 15183
			internal string caption;

			// Token: 0x04003B50 RID: 15184
			internal short mnemonic;

			// Token: 0x04003B51 RID: 15185
			internal Shortcut shortcut;

			// Token: 0x04003B52 RID: 15186
			internal bool showShortcut;

			// Token: 0x04003B53 RID: 15187
			internal EventHandler onClick;

			// Token: 0x04003B54 RID: 15188
			internal EventHandler onPopup;

			// Token: 0x04003B55 RID: 15189
			internal EventHandler onSelect;

			// Token: 0x04003B56 RID: 15190
			internal DrawItemEventHandler onDrawItem;

			// Token: 0x04003B57 RID: 15191
			internal MeasureItemEventHandler onMeasureItem;

			// Token: 0x04003B58 RID: 15192
			private object userData;

			// Token: 0x04003B59 RID: 15193
			internal Command cmd;
		}

		// Token: 0x020006D8 RID: 1752
		private class MdiListUserData
		{
			// Token: 0x06006AF5 RID: 27381 RVA: 0x000070A6 File Offset: 0x000052A6
			public virtual void OnClick(EventArgs e)
			{
			}
		}

		// Token: 0x020006D9 RID: 1753
		private class MdiListFormData : MenuItem.MdiListUserData
		{
			// Token: 0x06006AF7 RID: 27383 RVA: 0x0018BAC1 File Offset: 0x00189CC1
			public MdiListFormData(MenuItem parentItem, int boundFormIndex)
			{
				this.boundIndex = boundFormIndex;
				this.parent = parentItem;
			}

			// Token: 0x06006AF8 RID: 27384 RVA: 0x0018BAD8 File Offset: 0x00189CD8
			public override void OnClick(EventArgs e)
			{
				if (this.boundIndex != -1)
				{
					IntSecurity.ModifyFocus.Assert();
					try
					{
						Form[] array = this.parent.FindMdiForms();
						if (array != null && array.Length > this.boundIndex)
						{
							Form form = array[this.boundIndex];
							form.Activate();
							if (form.ActiveControl != null && !form.ActiveControl.Focused)
							{
								form.ActiveControl.Focus();
							}
						}
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
			}

			// Token: 0x04003B5A RID: 15194
			private MenuItem parent;

			// Token: 0x04003B5B RID: 15195
			private int boundIndex;
		}

		// Token: 0x020006DA RID: 1754
		private class MdiListMoreWindowsData : MenuItem.MdiListUserData
		{
			// Token: 0x06006AF9 RID: 27385 RVA: 0x0018BB5C File Offset: 0x00189D5C
			public MdiListMoreWindowsData(MenuItem parent)
			{
				this.parent = parent;
			}

			// Token: 0x06006AFA RID: 27386 RVA: 0x0018BB6C File Offset: 0x00189D6C
			public override void OnClick(EventArgs e)
			{
				Form[] array = this.parent.FindMdiForms();
				Form activeMdiChild = this.parent.GetMainMenu().GetFormUnsafe().ActiveMdiChild;
				if (array != null && array.Length != 0 && activeMdiChild != null)
				{
					IntSecurity.AllWindows.Assert();
					try
					{
						using (MdiWindowDialog mdiWindowDialog = new MdiWindowDialog())
						{
							mdiWindowDialog.SetItems(activeMdiChild, array);
							DialogResult dialogResult = mdiWindowDialog.ShowDialog();
							if (dialogResult == DialogResult.OK)
							{
								mdiWindowDialog.ActiveChildForm.Activate();
								if (mdiWindowDialog.ActiveChildForm.ActiveControl != null && !mdiWindowDialog.ActiveChildForm.ActiveControl.Focused)
								{
									mdiWindowDialog.ActiveChildForm.ActiveControl.Focus();
								}
							}
						}
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
			}

			// Token: 0x04003B5C RID: 15196
			private MenuItem parent;
		}
	}
}
