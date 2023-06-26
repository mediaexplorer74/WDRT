using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	/// <summary>Represents the base functionality for all menus. Although <see cref="T:System.Windows.Forms.ToolStripDropDown" /> and <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> replace and add functionality to the <see cref="T:System.Windows.Forms.Menu" /> control of previous versions, <see cref="T:System.Windows.Forms.Menu" /> is retained for both backward compatibility and future use if you choose.</summary>
	// Token: 0x020002F1 RID: 753
	[ToolboxItemFilter("System.Windows.Forms")]
	[ListBindable(false)]
	public abstract class Menu : Component
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Menu" /> class.</summary>
		/// <param name="items">An array of type <see cref="T:System.Windows.Forms.MenuItem" /> containing the objects to add to the menu.</param>
		// Token: 0x06002F9F RID: 12191 RVA: 0x000D6CAB File Offset: 0x000D4EAB
		protected Menu(MenuItem[] items)
		{
			if (items != null)
			{
				this.MenuItems.AddRange(items);
			}
		}

		/// <summary>Gets a value representing the window handle for the menu.</summary>
		/// <returns>The HMENU value of the menu.</returns>
		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x06002FA0 RID: 12192 RVA: 0x000D6CC2 File Offset: 0x000D4EC2
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlHandleDescr")]
		public IntPtr Handle
		{
			get
			{
				if (this.handle == IntPtr.Zero)
				{
					this.handle = this.CreateMenuHandle();
				}
				this.CreateMenuItems();
				return this.handle;
			}
		}

		/// <summary>Gets a value indicating whether this menu contains any menu items. This property is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if this menu contains <see cref="T:System.Windows.Forms.MenuItem" /> objects; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x06002FA1 RID: 12193 RVA: 0x000D6CEE File Offset: 0x000D4EEE
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("MenuIsParentDescr")]
		public virtual bool IsParent
		{
			[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				return this.items != null && this.ItemCount > 0;
			}
		}

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x06002FA2 RID: 12194 RVA: 0x000D6D03 File Offset: 0x000D4F03
		internal int ItemCount
		{
			get
			{
				return this._itemCount;
			}
		}

		/// <summary>Gets a value indicating the <see cref="T:System.Windows.Forms.MenuItem" /> that is used to display a list of multiple document interface (MDI) child forms.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.MenuItem" /> that represents the menu item displaying a list of MDI child forms that are open in the application.</returns>
		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x06002FA3 RID: 12195 RVA: 0x000D6D0C File Offset: 0x000D4F0C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("MenuMDIListItemDescr")]
		public MenuItem MdiListItem
		{
			get
			{
				for (int i = 0; i < this.ItemCount; i++)
				{
					MenuItem menuItem = this.items[i];
					if (menuItem.MdiList)
					{
						return menuItem;
					}
					if (menuItem.IsParent)
					{
						menuItem = menuItem.MdiListItem;
						if (menuItem != null)
						{
							return menuItem;
						}
					}
				}
				return null;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Windows.Forms.Menu" />.</summary>
		/// <returns>A string representing the name.</returns>
		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x06002FA4 RID: 12196 RVA: 0x000D6D52 File Offset: 0x000D4F52
		// (set) Token: 0x06002FA5 RID: 12197 RVA: 0x000D6D60 File Offset: 0x000D4F60
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public string Name
		{
			get
			{
				return WindowsFormsUtils.GetComponentName(this, this.name);
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					this.name = null;
				}
				else
				{
					this.name = value;
				}
				if (this.Site != null)
				{
					this.Site.Name = this.name;
				}
			}
		}

		/// <summary>Gets a value indicating the collection of <see cref="T:System.Windows.Forms.MenuItem" /> objects associated with the menu.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Menu.MenuItemCollection" /> that represents the list of <see cref="T:System.Windows.Forms.MenuItem" /> objects stored in the menu.</returns>
		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x06002FA6 RID: 12198 RVA: 0x000D6D96 File Offset: 0x000D4F96
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[SRDescription("MenuMenuItemsDescr")]
		[MergableProperty(false)]
		public Menu.MenuItemCollection MenuItems
		{
			get
			{
				if (this.itemsCollection == null)
				{
					this.itemsCollection = new Menu.MenuItemCollection(this);
				}
				return this.itemsCollection;
			}
		}

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x06002FA7 RID: 12199 RVA: 0x0001180C File Offset: 0x0000FA0C
		internal virtual bool RenderIsRightToLeft
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets user-defined data associated with the control.</summary>
		/// <returns>An object representing the data.</returns>
		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x06002FA8 RID: 12200 RVA: 0x000D6DB2 File Offset: 0x000D4FB2
		// (set) Token: 0x06002FA9 RID: 12201 RVA: 0x000D6DBA File Offset: 0x000D4FBA
		[SRCategory("CatData")]
		[Localizable(false)]
		[Bindable(true)]
		[SRDescription("ControlTagDescr")]
		[DefaultValue(null)]
		[TypeConverter(typeof(StringConverter))]
		public object Tag
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

		// Token: 0x06002FAA RID: 12202 RVA: 0x000D6DC4 File Offset: 0x000D4FC4
		internal void ClearHandles()
		{
			if (this.handle != IntPtr.Zero)
			{
				UnsafeNativeMethods.DestroyMenu(new HandleRef(this, this.handle));
			}
			this.handle = IntPtr.Zero;
			if (this.created)
			{
				for (int i = 0; i < this.ItemCount; i++)
				{
					this.items[i].ClearHandles();
				}
				this.created = false;
			}
		}

		/// <summary>Copies the <see cref="T:System.Windows.Forms.Menu" /> that is passed as a parameter to the current <see cref="T:System.Windows.Forms.Menu" />.</summary>
		/// <param name="menuSrc">The <see cref="T:System.Windows.Forms.Menu" /> to copy.</param>
		// Token: 0x06002FAB RID: 12203 RVA: 0x000D6E30 File Offset: 0x000D5030
		protected internal void CloneMenu(Menu menuSrc)
		{
			MenuItem[] array = null;
			if (menuSrc.items != null)
			{
				int count = menuSrc.MenuItems.Count;
				array = new MenuItem[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = menuSrc.MenuItems[i].CloneMenu();
				}
			}
			this.MenuItems.Clear();
			if (array != null)
			{
				this.MenuItems.AddRange(array);
			}
		}

		/// <summary>Creates a new handle to the <see cref="T:System.Windows.Forms.Menu" />.</summary>
		/// <returns>A handle to the menu if the method succeeds; otherwise, <see langword="null" />.</returns>
		// Token: 0x06002FAC RID: 12204 RVA: 0x000D6E94 File Offset: 0x000D5094
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected virtual IntPtr CreateMenuHandle()
		{
			return UnsafeNativeMethods.CreatePopupMenu();
		}

		// Token: 0x06002FAD RID: 12205 RVA: 0x000D6E9C File Offset: 0x000D509C
		internal void CreateMenuItems()
		{
			if (!this.created)
			{
				for (int i = 0; i < this.ItemCount; i++)
				{
					this.items[i].CreateMenuItem();
				}
				this.created = true;
			}
		}

		// Token: 0x06002FAE RID: 12206 RVA: 0x000D6ED8 File Offset: 0x000D50D8
		internal void DestroyMenuItems()
		{
			if (this.created)
			{
				for (int i = 0; i < this.ItemCount; i++)
				{
					this.items[i].ClearHandles();
				}
				while (UnsafeNativeMethods.GetMenuItemCount(new HandleRef(this, this.handle)) > 0)
				{
					UnsafeNativeMethods.RemoveMenu(new HandleRef(this, this.handle), 0, 1024);
				}
				this.created = false;
			}
		}

		/// <summary>Disposes of the resources, other than memory, used by the <see cref="T:System.Windows.Forms.Menu" />.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06002FAF RID: 12207 RVA: 0x000D6F40 File Offset: 0x000D5140
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				while (this.ItemCount > 0)
				{
					MenuItem[] array = this.items;
					int num = this._itemCount - 1;
					this._itemCount = num;
					MenuItem menuItem = array[num];
					if (menuItem.Site != null && menuItem.Site.Container != null)
					{
						menuItem.Site.Container.Remove(menuItem);
					}
					menuItem.Menu = null;
					menuItem.Dispose();
				}
				this.items = null;
			}
			if (this.handle != IntPtr.Zero)
			{
				UnsafeNativeMethods.DestroyMenu(new HandleRef(this, this.handle));
				this.handle = IntPtr.Zero;
				if (disposing)
				{
					this.ClearHandles();
				}
			}
			base.Dispose(disposing);
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.MenuItem" /> that contains the value specified.</summary>
		/// <param name="type">The type of item to use to find the <see cref="T:System.Windows.Forms.MenuItem" />.</param>
		/// <param name="value">The item to use to find the <see cref="T:System.Windows.Forms.MenuItem" />.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.MenuItem" /> that matches value; otherwise, <see langword="null" />.</returns>
		// Token: 0x06002FB0 RID: 12208 RVA: 0x000D6FED File Offset: 0x000D51ED
		public MenuItem FindMenuItem(int type, IntPtr value)
		{
			IntSecurity.ControlFromHandleOrLocation.Demand();
			return this.FindMenuItemInternal(type, value);
		}

		// Token: 0x06002FB1 RID: 12209 RVA: 0x000D7004 File Offset: 0x000D5204
		private MenuItem FindMenuItemInternal(int type, IntPtr value)
		{
			for (int i = 0; i < this.ItemCount; i++)
			{
				MenuItem menuItem = this.items[i];
				if (type != 0)
				{
					if (type == 1)
					{
						if (menuItem.Shortcut == (Shortcut)(int)value)
						{
							return menuItem;
						}
					}
				}
				else if (menuItem.handle == value)
				{
					return menuItem;
				}
				menuItem = menuItem.FindMenuItemInternal(type, value);
				if (menuItem != null)
				{
					return menuItem;
				}
			}
			return null;
		}

		/// <summary>Returns the position at which a menu item should be inserted into the menu.</summary>
		/// <param name="mergeOrder">The merge order position for the menu item to be merged.</param>
		/// <returns>The position at which a menu item should be inserted into the menu.</returns>
		// Token: 0x06002FB2 RID: 12210 RVA: 0x000D7064 File Offset: 0x000D5264
		protected int FindMergePosition(int mergeOrder)
		{
			int i = 0;
			int num = this.ItemCount;
			while (i < num)
			{
				int num2 = (i + num) / 2;
				if (this.items[num2].MergeOrder <= mergeOrder)
				{
					i = num2 + 1;
				}
				else
				{
					num = num2;
				}
			}
			return i;
		}

		// Token: 0x06002FB3 RID: 12211 RVA: 0x000D70A0 File Offset: 0x000D52A0
		internal int xFindMergePosition(int mergeOrder)
		{
			int num = 0;
			int num2 = 0;
			while (num2 < this.ItemCount && this.items[num2].MergeOrder <= mergeOrder)
			{
				if (this.items[num2].MergeOrder < mergeOrder)
				{
					num = num2 + 1;
				}
				else if (mergeOrder == this.items[num2].MergeOrder)
				{
					num = num2;
					break;
				}
				num2++;
			}
			return num;
		}

		// Token: 0x06002FB4 RID: 12212 RVA: 0x000D70FC File Offset: 0x000D52FC
		internal void UpdateRtl(bool setRightToLeftBit)
		{
			foreach (object obj in this.MenuItems)
			{
				MenuItem menuItem = (MenuItem)obj;
				menuItem.UpdateItemRtl(setRightToLeftBit);
				menuItem.UpdateRtl(setRightToLeftBit);
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ContextMenu" /> that contains this menu.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ContextMenu" /> that contains this menu. The default is <see langword="null" />.</returns>
		// Token: 0x06002FB5 RID: 12213 RVA: 0x000D715C File Offset: 0x000D535C
		public ContextMenu GetContextMenu()
		{
			Menu menu = this;
			while (!(menu is ContextMenu))
			{
				if (!(menu is MenuItem))
				{
					return null;
				}
				menu = ((MenuItem)menu).Menu;
			}
			return (ContextMenu)menu;
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.MainMenu" /> that contains this menu.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.MainMenu" /> that contains this menu.</returns>
		// Token: 0x06002FB6 RID: 12214 RVA: 0x000D7194 File Offset: 0x000D5394
		public MainMenu GetMainMenu()
		{
			Menu menu = this;
			while (!(menu is MainMenu))
			{
				if (!(menu is MenuItem))
				{
					return null;
				}
				menu = ((MenuItem)menu).Menu;
			}
			return (MainMenu)menu;
		}

		// Token: 0x06002FB7 RID: 12215 RVA: 0x000D71C9 File Offset: 0x000D53C9
		internal virtual void ItemsChanged(int change)
		{
			if (change <= 1)
			{
				this.DestroyMenuItems();
			}
		}

		// Token: 0x06002FB8 RID: 12216 RVA: 0x000D71D8 File Offset: 0x000D53D8
		private IntPtr MatchKeyToMenuItem(int startItem, char key, Menu.MenuItemKeyComparer comparer)
		{
			int num = -1;
			bool flag = false;
			int num2 = 0;
			while (num2 < this.items.Length && !flag)
			{
				int num3 = (startItem + num2) % this.items.Length;
				MenuItem menuItem = this.items[num3];
				if (menuItem != null && comparer(menuItem, key))
				{
					if (num < 0)
					{
						num = menuItem.MenuIndex;
					}
					else
					{
						flag = true;
					}
				}
				num2++;
			}
			if (num < 0)
			{
				return IntPtr.Zero;
			}
			int num4 = (flag ? 3 : 2);
			return (IntPtr)NativeMethods.Util.MAKELONG(num, num4);
		}

		/// <summary>Merges the <see cref="T:System.Windows.Forms.MenuItem" /> objects of one menu with the current menu.</summary>
		/// <param name="menuSrc">The <see cref="T:System.Windows.Forms.Menu" /> whose menu items are merged with the menu items of the current menu.</param>
		/// <exception cref="T:System.ArgumentException">It was attempted to merge the menu with itself.</exception>
		// Token: 0x06002FB9 RID: 12217 RVA: 0x000D7258 File Offset: 0x000D5458
		public virtual void MergeMenu(Menu menuSrc)
		{
			if (menuSrc == this)
			{
				throw new ArgumentException(SR.GetString("MenuMergeWithSelf"), "menuSrc");
			}
			if (menuSrc.items != null && this.items == null)
			{
				this.MenuItems.Clear();
			}
			for (int i = 0; i < menuSrc.ItemCount; i++)
			{
				MenuItem menuItem = menuSrc.items[i];
				MenuMerge mergeType = menuItem.MergeType;
				if (mergeType != MenuMerge.Add)
				{
					if (mergeType - MenuMerge.Replace <= 1)
					{
						int mergeOrder = menuItem.MergeOrder;
						int j = this.xFindMergePosition(mergeOrder);
						while (j < this.ItemCount)
						{
							MenuItem menuItem2 = this.items[j];
							if (menuItem2.MergeOrder != mergeOrder)
							{
								this.MenuItems.Add(j, menuItem.MergeMenu());
								goto IL_11D;
							}
							if (menuItem2.MergeType != MenuMerge.Add)
							{
								if (menuItem.MergeType != MenuMerge.MergeItems || menuItem2.MergeType != MenuMerge.MergeItems)
								{
									menuItem2.Dispose();
									this.MenuItems.Add(j, menuItem.MergeMenu());
									goto IL_11D;
								}
								menuItem2.MergeMenu(menuItem);
								goto IL_11D;
							}
							else
							{
								j++;
							}
						}
						this.MenuItems.Add(j, menuItem.MergeMenu());
					}
				}
				else
				{
					this.MenuItems.Add(this.FindMergePosition(menuItem.MergeOrder), menuItem.MergeMenu());
				}
				IL_11D:;
			}
		}

		// Token: 0x06002FBA RID: 12218 RVA: 0x000D7394 File Offset: 0x000D5594
		internal virtual bool ProcessInitMenuPopup(IntPtr handle)
		{
			MenuItem menuItem = this.FindMenuItemInternal(0, handle);
			if (menuItem != null)
			{
				menuItem._OnInitMenuPopup(EventArgs.Empty);
				menuItem.CreateMenuItems();
				return true;
			}
			return false;
		}

		/// <summary>Processes a command key.</summary>
		/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference that represents the window message to process.</param>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
		/// <returns>
		///   <see langword="true" /> if the character was processed by the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002FBB RID: 12219 RVA: 0x000D73C4 File Offset: 0x000D55C4
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected internal virtual bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			MenuItem menuItem = this.FindMenuItemInternal(1, (IntPtr)((int)keyData));
			return menuItem != null && menuItem.ShortcutClick();
		}

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x06002FBC RID: 12220 RVA: 0x000D73EC File Offset: 0x000D55EC
		internal int SelectedMenuItemIndex
		{
			get
			{
				for (int i = 0; i < this.items.Length; i++)
				{
					MenuItem menuItem = this.items[i];
					if (menuItem != null && menuItem.Selected)
					{
						return i;
					}
				}
				return -1;
			}
		}

		/// <summary>Returns a <see cref="T:System.String" /> that represents the <see cref="T:System.Windows.Forms.Menu" /> control.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Windows.Forms.Menu" />.</returns>
		// Token: 0x06002FBD RID: 12221 RVA: 0x000D7424 File Offset: 0x000D5624
		public override string ToString()
		{
			string text = base.ToString();
			return text + ", Items.Count: " + this.ItemCount.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x06002FBE RID: 12222 RVA: 0x000D7458 File Offset: 0x000D5658
		internal void WmMenuChar(ref Message m)
		{
			Menu menu = ((m.LParam == this.handle) ? this : this.FindMenuItemInternal(0, m.LParam));
			if (menu == null)
			{
				return;
			}
			char c = char.ToUpper((char)NativeMethods.Util.LOWORD(m.WParam), CultureInfo.CurrentCulture);
			m.Result = menu.WmMenuCharInternal(c);
		}

		// Token: 0x06002FBF RID: 12223 RVA: 0x000D74B4 File Offset: 0x000D56B4
		internal IntPtr WmMenuCharInternal(char key)
		{
			int num = (this.SelectedMenuItemIndex + 1) % this.items.Length;
			IntPtr intPtr = this.MatchKeyToMenuItem(num, key, new Menu.MenuItemKeyComparer(this.CheckOwnerDrawItemWithMnemonic));
			if (intPtr == IntPtr.Zero)
			{
				intPtr = this.MatchKeyToMenuItem(num, key, new Menu.MenuItemKeyComparer(this.CheckOwnerDrawItemNoMnemonic));
			}
			return intPtr;
		}

		// Token: 0x06002FC0 RID: 12224 RVA: 0x000D750B File Offset: 0x000D570B
		private bool CheckOwnerDrawItemWithMnemonic(MenuItem mi, char key)
		{
			return mi.OwnerDraw && mi.Mnemonic == key;
		}

		// Token: 0x06002FC1 RID: 12225 RVA: 0x000D7520 File Offset: 0x000D5720
		private bool CheckOwnerDrawItemNoMnemonic(MenuItem mi, char key)
		{
			return mi.OwnerDraw && mi.Mnemonic == '\0' && mi.Text.Length > 0 && char.ToUpper(mi.Text[0], CultureInfo.CurrentCulture) == key;
		}

		// Token: 0x040013A2 RID: 5026
		internal const int CHANGE_ITEMS = 0;

		// Token: 0x040013A3 RID: 5027
		internal const int CHANGE_VISIBLE = 1;

		// Token: 0x040013A4 RID: 5028
		internal const int CHANGE_MDI = 2;

		// Token: 0x040013A5 RID: 5029
		internal const int CHANGE_MERGE = 3;

		// Token: 0x040013A6 RID: 5030
		internal const int CHANGE_ITEMADDED = 4;

		/// <summary>Specifies that the <see cref="M:System.Windows.Forms.Menu.FindMenuItem(System.Int32,System.IntPtr)" /> method should search for a handle.</summary>
		// Token: 0x040013A7 RID: 5031
		public const int FindHandle = 0;

		/// <summary>Specifies that the <see cref="M:System.Windows.Forms.Menu.FindMenuItem(System.Int32,System.IntPtr)" /> method should search for a shortcut.</summary>
		// Token: 0x040013A8 RID: 5032
		public const int FindShortcut = 1;

		// Token: 0x040013A9 RID: 5033
		private Menu.MenuItemCollection itemsCollection;

		// Token: 0x040013AA RID: 5034
		internal MenuItem[] items;

		// Token: 0x040013AB RID: 5035
		private int _itemCount;

		// Token: 0x040013AC RID: 5036
		internal IntPtr handle;

		// Token: 0x040013AD RID: 5037
		internal bool created;

		// Token: 0x040013AE RID: 5038
		private object userData;

		// Token: 0x040013AF RID: 5039
		private string name;

		// Token: 0x020006D4 RID: 1748
		// (Invoke) Token: 0x06006AB7 RID: 27319
		private delegate bool MenuItemKeyComparer(MenuItem mi, char key);

		/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.MenuItem" /> objects.</summary>
		// Token: 0x020006D5 RID: 1749
		[ListBindable(false)]
		public class MenuItemCollection : IList, ICollection, IEnumerable
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Menu.MenuItemCollection" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.Menu" /> that owns this collection.</param>
			// Token: 0x06006ABA RID: 27322 RVA: 0x0018AEEA File Offset: 0x001890EA
			public MenuItemCollection(Menu owner)
			{
				this.owner = owner;
			}

			/// <summary>Retrieves the <see cref="T:System.Windows.Forms.MenuItem" /> at the specified indexed location in the collection.</summary>
			/// <param name="index">The indexed location of the <see cref="T:System.Windows.Forms.MenuItem" /> in the collection.</param>
			/// <returns>The <see cref="T:System.Windows.Forms.MenuItem" /> at the specified location.</returns>
			/// <exception cref="T:System.ArgumentException">The <paramref name="value" /> parameter is <see langword="null" />.  
			///  or  
			///  The <paramref name="index" /> parameter is less than zero.  
			///  or  
			///  The <paramref name="index" /> parameter is greater than the number of menu items in the collection, and the collection of menu items is not <see langword="null" />.</exception>
			// Token: 0x1700172A RID: 5930
			public virtual MenuItem this[int index]
			{
				get
				{
					if (index < 0 || index >= this.owner.ItemCount)
					{
						throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
					return this.owner.items[index];
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
			/// <param name="index">The zero-based index of the element to get.</param>
			/// <returns>The <see cref="T:System.Windows.Forms.MenuItem" /> at the specified index.</returns>
			// Token: 0x1700172B RID: 5931
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

			/// <summary>Gets an item with the specified key from the collection.</summary>
			/// <param name="key">The name of the item to retrieve from the collection.</param>
			/// <returns>The <see cref="T:System.Windows.Forms.MenuItem" /> with the specified key.</returns>
			// Token: 0x1700172C RID: 5932
			public virtual MenuItem this[string key]
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

			/// <summary>Gets a value indicating the total number of <see cref="T:System.Windows.Forms.MenuItem" /> objects in the collection.</summary>
			/// <returns>The number of <see cref="T:System.Windows.Forms.MenuItem" /> objects in the collection.</returns>
			// Token: 0x1700172D RID: 5933
			// (get) Token: 0x06006ABF RID: 27327 RVA: 0x0018AF99 File Offset: 0x00189199
			public int Count
			{
				get
				{
					return this.owner.ItemCount;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
			/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.Menu.MenuItemCollection" />.</returns>
			// Token: 0x1700172E RID: 5934
			// (get) Token: 0x06006AC0 RID: 27328 RVA: 0x00006A49 File Offset: 0x00004C49
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
			// Token: 0x1700172F RID: 5935
			// (get) Token: 0x06006AC1 RID: 27329 RVA: 0x0001180C File Offset: 0x0000FA0C
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
			// Token: 0x17001730 RID: 5936
			// (get) Token: 0x06006AC2 RID: 27330 RVA: 0x0001180C File Offset: 0x0000FA0C
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>
			///   <see langword="true" /> if the collection is read-only; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
			// Token: 0x17001731 RID: 5937
			// (get) Token: 0x06006AC3 RID: 27331 RVA: 0x0001180C File Offset: 0x0000FA0C
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			/// <summary>Adds a new <see cref="T:System.Windows.Forms.MenuItem" />, to the end of the current menu, with a specified caption.</summary>
			/// <param name="caption">The caption of the menu item.</param>
			/// <returns>A <see cref="T:System.Windows.Forms.MenuItem" /> that represents the menu item being added to the collection.</returns>
			// Token: 0x06006AC4 RID: 27332 RVA: 0x0018AFA8 File Offset: 0x001891A8
			public virtual MenuItem Add(string caption)
			{
				MenuItem menuItem = new MenuItem(caption);
				this.Add(menuItem);
				return menuItem;
			}

			/// <summary>Adds a new <see cref="T:System.Windows.Forms.MenuItem" /> to the end of the current menu with a specified caption and a specified event handler for the <see cref="E:System.Windows.Forms.MenuItem.Click" /> event.</summary>
			/// <param name="caption">The caption of the menu item.</param>
			/// <param name="onClick">An <see cref="T:System.EventHandler" /> that represents the event handler that is called when the item is clicked by the user, or when a user presses an accelerator or shortcut key for the menu item.</param>
			/// <returns>A <see cref="T:System.Windows.Forms.MenuItem" /> that represents the menu item being added to the collection.</returns>
			// Token: 0x06006AC5 RID: 27333 RVA: 0x0018AFC8 File Offset: 0x001891C8
			public virtual MenuItem Add(string caption, EventHandler onClick)
			{
				MenuItem menuItem = new MenuItem(caption, onClick);
				this.Add(menuItem);
				return menuItem;
			}

			/// <summary>Adds a new <see cref="T:System.Windows.Forms.MenuItem" /> to the end of this menu with the specified caption, <see cref="E:System.Windows.Forms.MenuItem.Click" /> event handler, and items.</summary>
			/// <param name="caption">The caption of the menu item.</param>
			/// <param name="items">An array of <see cref="T:System.Windows.Forms.MenuItem" /> objects that this <see cref="T:System.Windows.Forms.MenuItem" /> will contain.</param>
			/// <returns>A <see cref="T:System.Windows.Forms.MenuItem" /> that represents the menu item being added to the collection.</returns>
			// Token: 0x06006AC6 RID: 27334 RVA: 0x0018AFE8 File Offset: 0x001891E8
			public virtual MenuItem Add(string caption, MenuItem[] items)
			{
				MenuItem menuItem = new MenuItem(caption, items);
				this.Add(menuItem);
				return menuItem;
			}

			/// <summary>Adds a previously created <see cref="T:System.Windows.Forms.MenuItem" /> to the end of the current menu.</summary>
			/// <param name="item">The <see cref="T:System.Windows.Forms.MenuItem" /> to add.</param>
			/// <returns>The zero-based index where the item is stored in the collection.</returns>
			// Token: 0x06006AC7 RID: 27335 RVA: 0x0018B006 File Offset: 0x00189206
			public virtual int Add(MenuItem item)
			{
				return this.Add(this.owner.ItemCount, item);
			}

			/// <summary>Adds a previously created <see cref="T:System.Windows.Forms.MenuItem" /> at the specified index within the menu item collection.</summary>
			/// <param name="index">The position to add the new item.</param>
			/// <param name="item">The <see cref="T:System.Windows.Forms.MenuItem" /> to add.</param>
			/// <returns>The zero-based index where the item is stored in the collection.</returns>
			/// <exception cref="T:System.Exception">The <see cref="T:System.Windows.Forms.MenuItem" /> being added is already in use.</exception>
			/// <exception cref="T:System.ArgumentException">The index supplied in the <paramref name="index" /> parameter is larger than the size of the collection.</exception>
			// Token: 0x06006AC8 RID: 27336 RVA: 0x0018B01C File Offset: 0x0018921C
			public virtual int Add(int index, MenuItem item)
			{
				if (item.Menu != null)
				{
					if (this.owner is MenuItem)
					{
						for (MenuItem menuItem = (MenuItem)this.owner; menuItem != null; menuItem = (MenuItem)menuItem.Parent)
						{
							if (menuItem.Equals(item))
							{
								throw new ArgumentException(SR.GetString("MenuItemAlreadyExists", new object[] { item.Text }), "item");
							}
							if (!(menuItem.Parent is MenuItem))
							{
								break;
							}
						}
					}
					if (item.Menu.Equals(this.owner) && index > 0)
					{
						index--;
					}
					item.Menu.MenuItems.Remove(item);
				}
				if (index < 0 || index > this.owner.ItemCount)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this.owner.items == null || this.owner.items.Length == this.owner.ItemCount)
				{
					MenuItem[] array = new MenuItem[(this.owner.ItemCount < 2) ? 4 : (this.owner.ItemCount * 2)];
					if (this.owner.ItemCount > 0)
					{
						Array.Copy(this.owner.items, 0, array, 0, this.owner.ItemCount);
					}
					this.owner.items = array;
				}
				Array.Copy(this.owner.items, index, this.owner.items, index + 1, this.owner.ItemCount - index);
				this.owner.items[index] = item;
				this.owner._itemCount++;
				item.Menu = this.owner;
				this.owner.ItemsChanged(0);
				if (this.owner is MenuItem)
				{
					((MenuItem)this.owner).ItemsChanged(4, item);
				}
				return index;
			}

			/// <summary>Adds an array of previously created <see cref="T:System.Windows.Forms.MenuItem" /> objects to the collection.</summary>
			/// <param name="items">An array of <see cref="T:System.Windows.Forms.MenuItem" /> objects representing the menu items to add to the collection.</param>
			// Token: 0x06006AC9 RID: 27337 RVA: 0x0018B214 File Offset: 0x00189414
			public virtual void AddRange(MenuItem[] items)
			{
				if (items == null)
				{
					throw new ArgumentNullException("items");
				}
				foreach (MenuItem menuItem in items)
				{
					this.Add(menuItem);
				}
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.MenuItem" /> to add to the collection.</param>
			/// <returns>The position into which the <see cref="T:System.Windows.Forms.MenuItem" /> was inserted.</returns>
			// Token: 0x06006ACA RID: 27338 RVA: 0x0018B24B File Offset: 0x0018944B
			int IList.Add(object value)
			{
				if (value is MenuItem)
				{
					return this.Add((MenuItem)value);
				}
				throw new ArgumentException(SR.GetString("MenuBadMenuItem"), "value");
			}

			/// <summary>Determines if the specified <see cref="T:System.Windows.Forms.MenuItem" /> is a member of the collection.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.MenuItem" /> to locate in the collection.</param>
			/// <returns>
			///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.MenuItem" /> is a member of the collection; otherwise, <see langword="false" />.</returns>
			// Token: 0x06006ACB RID: 27339 RVA: 0x0018B276 File Offset: 0x00189476
			public bool Contains(MenuItem value)
			{
				return this.IndexOf(value) != -1;
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
			/// <param name="value">The object to locate in the collection.</param>
			/// <returns>
			///   <see langword="true" /> if the specified object is a <see cref="T:System.Windows.Forms.MenuItem" /> in the collection; otherwise, <see langword="false" />.</returns>
			// Token: 0x06006ACC RID: 27340 RVA: 0x0018B285 File Offset: 0x00189485
			bool IList.Contains(object value)
			{
				return value is MenuItem && this.Contains((MenuItem)value);
			}

			/// <summary>Determines whether the collection contains an item with the specified key.</summary>
			/// <param name="key">The name of the item to look for.</param>
			/// <returns>
			///   <see langword="true" /> if the collection contains an item with the specified key, otherwise, <see langword="false" />.</returns>
			// Token: 0x06006ACD RID: 27341 RVA: 0x0018B29D File Offset: 0x0018949D
			public virtual bool ContainsKey(string key)
			{
				return this.IsValidIndex(this.IndexOfKey(key));
			}

			/// <summary>Finds the items with the specified key, optionally searching the submenu items</summary>
			/// <param name="key">The name of the menu item to search for.</param>
			/// <param name="searchAllChildren">
			///   <see langword="true" /> to search child menu items; otherwise, <see langword="false" />.</param>
			/// <returns>An array of <see cref="T:System.Windows.Forms.MenuItem" /> objects whose <see cref="P:System.Windows.Forms.Menu.Name" /> property matches the specified <paramref name="key" />.</returns>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="key" /> is <see langword="null" /> or an empty string.</exception>
			// Token: 0x06006ACE RID: 27342 RVA: 0x0018B2AC File Offset: 0x001894AC
			public MenuItem[] Find(string key, bool searchAllChildren)
			{
				if (key == null || key.Length == 0)
				{
					throw new ArgumentNullException("key", SR.GetString("FindKeyMayNotBeEmptyOrNull"));
				}
				ArrayList arrayList = this.FindInternal(key, searchAllChildren, this, new ArrayList());
				MenuItem[] array = new MenuItem[arrayList.Count];
				arrayList.CopyTo(array, 0);
				return array;
			}

			// Token: 0x06006ACF RID: 27343 RVA: 0x0018B300 File Offset: 0x00189500
			private ArrayList FindInternal(string key, bool searchAllChildren, Menu.MenuItemCollection menuItemsToLookIn, ArrayList foundMenuItems)
			{
				if (menuItemsToLookIn == null || foundMenuItems == null)
				{
					return null;
				}
				for (int i = 0; i < menuItemsToLookIn.Count; i++)
				{
					if (menuItemsToLookIn[i] != null && WindowsFormsUtils.SafeCompareStrings(menuItemsToLookIn[i].Name, key, true))
					{
						foundMenuItems.Add(menuItemsToLookIn[i]);
					}
				}
				if (searchAllChildren)
				{
					for (int j = 0; j < menuItemsToLookIn.Count; j++)
					{
						if (menuItemsToLookIn[j] != null && menuItemsToLookIn[j].MenuItems != null && menuItemsToLookIn[j].MenuItems.Count > 0)
						{
							foundMenuItems = this.FindInternal(key, searchAllChildren, menuItemsToLookIn[j].MenuItems, foundMenuItems);
						}
					}
				}
				return foundMenuItems;
			}

			/// <summary>Retrieves the index of a specific item in the collection.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.MenuItem" /> to locate in the collection.</param>
			/// <returns>The zero-based index of the item found in the collection; otherwise, -1.</returns>
			// Token: 0x06006AD0 RID: 27344 RVA: 0x0018B3B0 File Offset: 0x001895B0
			public int IndexOf(MenuItem value)
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

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.MenuItem" /> to locate in the collection.</param>
			/// <returns>The zero-based index if <paramref name="value" /> is a <see cref="T:System.Windows.Forms.MenuItem" /> in the collection; otherwise -1.</returns>
			// Token: 0x06006AD1 RID: 27345 RVA: 0x0018B3DB File Offset: 0x001895DB
			int IList.IndexOf(object value)
			{
				if (value is MenuItem)
				{
					return this.IndexOf((MenuItem)value);
				}
				return -1;
			}

			/// <summary>Finds the index of the first occurrence of a menu item with the specified key.</summary>
			/// <param name="key">The name of the menu item to search for.</param>
			/// <returns>The zero-based index of the first menu item with the specified key.</returns>
			// Token: 0x06006AD2 RID: 27346 RVA: 0x0018B3F4 File Offset: 0x001895F4
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

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
			/// <param name="index">The zero-based index at which the <see cref="T:System.Windows.Forms.MenuItem" /> should be inserted.</param>
			/// <param name="value">The <see cref="T:System.Windows.Forms.MenuItem" /> to insert into the <see cref="T:System.Windows.Forms.Menu.MenuItemCollection" />.</param>
			// Token: 0x06006AD3 RID: 27347 RVA: 0x0018B471 File Offset: 0x00189671
			void IList.Insert(int index, object value)
			{
				if (value is MenuItem)
				{
					this.Add(index, (MenuItem)value);
					return;
				}
				throw new ArgumentException(SR.GetString("MenuBadMenuItem"), "value");
			}

			// Token: 0x06006AD4 RID: 27348 RVA: 0x0018B49E File Offset: 0x0018969E
			private bool IsValidIndex(int index)
			{
				return index >= 0 && index < this.Count;
			}

			/// <summary>Removes all <see cref="T:System.Windows.Forms.MenuItem" /> objects from the menu item collection.</summary>
			// Token: 0x06006AD5 RID: 27349 RVA: 0x0018B4B0 File Offset: 0x001896B0
			public virtual void Clear()
			{
				if (this.owner.ItemCount > 0)
				{
					for (int i = 0; i < this.owner.ItemCount; i++)
					{
						this.owner.items[i].Menu = null;
					}
					this.owner._itemCount = 0;
					this.owner.items = null;
					this.owner.ItemsChanged(0);
					if (this.owner is MenuItem)
					{
						((MenuItem)this.owner).UpdateMenuItem(true);
					}
				}
			}

			/// <summary>Copies the entire collection into an existing array at a specified location within the array.</summary>
			/// <param name="dest">The destination array.</param>
			/// <param name="index">The index in the destination array at which storing begins.</param>
			// Token: 0x06006AD6 RID: 27350 RVA: 0x0018B536 File Offset: 0x00189736
			public void CopyTo(Array dest, int index)
			{
				if (this.owner.ItemCount > 0)
				{
					Array.Copy(this.owner.items, 0, dest, index, this.owner.ItemCount);
				}
			}

			/// <summary>Returns an enumerator that can be used to iterate through the menu item collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the menu item collection.</returns>
			// Token: 0x06006AD7 RID: 27351 RVA: 0x0018B564 File Offset: 0x00189764
			public IEnumerator GetEnumerator()
			{
				object[] items = this.owner.items;
				return new WindowsFormsUtils.ArraySubsetEnumerator(items, this.owner.ItemCount);
			}

			/// <summary>Removes a <see cref="T:System.Windows.Forms.MenuItem" /> from the menu item collection at a specified index.</summary>
			/// <param name="index">The index of the <see cref="T:System.Windows.Forms.MenuItem" /> to remove.</param>
			// Token: 0x06006AD8 RID: 27352 RVA: 0x0018B590 File Offset: 0x00189790
			public virtual void RemoveAt(int index)
			{
				if (index < 0 || index >= this.owner.ItemCount)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				MenuItem menuItem = this.owner.items[index];
				menuItem.Menu = null;
				this.owner._itemCount--;
				Array.Copy(this.owner.items, index + 1, this.owner.items, index, this.owner.ItemCount - index);
				this.owner.items[this.owner.ItemCount] = null;
				this.owner.ItemsChanged(0);
				if (this.owner.ItemCount == 0)
				{
					this.Clear();
				}
			}

			/// <summary>Removes the menu item with the specified key from the collection.</summary>
			/// <param name="key">The name of the menu item to remove.</param>
			// Token: 0x06006AD9 RID: 27353 RVA: 0x0018B66C File Offset: 0x0018986C
			public virtual void RemoveByKey(string key)
			{
				int num = this.IndexOfKey(key);
				if (this.IsValidIndex(num))
				{
					this.RemoveAt(num);
				}
			}

			/// <summary>Removes the specified <see cref="T:System.Windows.Forms.MenuItem" /> from the menu item collection.</summary>
			/// <param name="item">The <see cref="T:System.Windows.Forms.MenuItem" /> to remove.</param>
			// Token: 0x06006ADA RID: 27354 RVA: 0x0018B691 File Offset: 0x00189891
			public virtual void Remove(MenuItem item)
			{
				if (item.Menu == this.owner)
				{
					this.RemoveAt(item.Index);
				}
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.MenuItem" /> to remove.</param>
			// Token: 0x06006ADB RID: 27355 RVA: 0x0018B6AD File Offset: 0x001898AD
			void IList.Remove(object value)
			{
				if (value is MenuItem)
				{
					this.Remove((MenuItem)value);
				}
			}

			// Token: 0x04003B45 RID: 15173
			private Menu owner;

			// Token: 0x04003B46 RID: 15174
			private int lastAccessedIndex = -1;
		}
	}
}
