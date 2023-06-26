using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	/// <summary>Represents an item in a <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
	// Token: 0x020002DB RID: 731
	[TypeConverter(typeof(ListViewItemConverter))]
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	[DefaultProperty("Text")]
	[Serializable]
	public class ListViewItem : ICloneable, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with default values.</summary>
		// Token: 0x06002E35 RID: 11829 RVA: 0x000D1990 File Offset: 0x000CFB90
		public ListViewItem()
		{
			this.StateSelected = false;
			this.UseItemStyleForSubItems = true;
			this.SavedStateImageIndex = -1;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the specified serialization information and streaming context.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> containing information about the <see cref="T:System.Windows.Forms.ListViewItem" /> to be initialized.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that indicates the source destination and context information of a serialized stream.</param>
		// Token: 0x06002E36 RID: 11830 RVA: 0x000D19DE File Offset: 0x000CFBDE
		protected ListViewItem(SerializationInfo info, StreamingContext context)
			: this()
		{
			this.Deserialize(info, context);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the specified item text.</summary>
		/// <param name="text">The text to display for the item. This should not exceed 259 characters.</param>
		// Token: 0x06002E37 RID: 11831 RVA: 0x000D19EE File Offset: 0x000CFBEE
		public ListViewItem(string text)
			: this(text, -1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the specified item text and the image index position of the item's icon.</summary>
		/// <param name="text">The text to display for the item. This should not exceed 259 characters.</param>
		/// <param name="imageIndex">The zero-based index of the image within the <see cref="T:System.Windows.Forms.ImageList" /> associated with the <see cref="T:System.Windows.Forms.ListView" /> that contains the item.</param>
		// Token: 0x06002E38 RID: 11832 RVA: 0x000D19F8 File Offset: 0x000CFBF8
		public ListViewItem(string text, int imageIndex)
			: this()
		{
			this.ImageIndexer.Index = imageIndex;
			this.Text = text;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with an array of strings representing subitems.</summary>
		/// <param name="items">An array of strings that represent the subitems of the new item.</param>
		// Token: 0x06002E39 RID: 11833 RVA: 0x000D1A13 File Offset: 0x000CFC13
		public ListViewItem(string[] items)
			: this(items, -1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the image index position of the item's icon and an array of strings representing subitems.</summary>
		/// <param name="items">An array of strings that represent the subitems of the new item.</param>
		/// <param name="imageIndex">The zero-based index of the image within the <see cref="T:System.Windows.Forms.ImageList" /> associated with the <see cref="T:System.Windows.Forms.ListView" /> that contains the item.</param>
		// Token: 0x06002E3A RID: 11834 RVA: 0x000D1A20 File Offset: 0x000CFC20
		public ListViewItem(string[] items, int imageIndex)
			: this()
		{
			this.ImageIndexer.Index = imageIndex;
			if (items != null && items.Length != 0)
			{
				this.subItems = new ListViewItem.ListViewSubItem[items.Length];
				for (int i = 0; i < items.Length; i++)
				{
					this.subItems[i] = new ListViewItem.ListViewSubItem(this, items[i]);
				}
				this.SubItemCount = items.Length;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the image index position of the item's icon; the foreground color, background color, and font of the item; and an array of strings representing subitems.</summary>
		/// <param name="items">An array of strings that represent the subitems of the new item.</param>
		/// <param name="imageIndex">The zero-based index of the image within the <see cref="T:System.Windows.Forms.ImageList" /> associated with the <see cref="T:System.Windows.Forms.ListView" /> that contains the item.</param>
		/// <param name="foreColor">A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the item.</param>
		/// <param name="backColor">A <see cref="T:System.Drawing.Color" /> that represents the background color of the item.</param>
		/// <param name="font">A <see cref="T:System.Drawing.Font" /> that represents the font to display the item's text in.</param>
		// Token: 0x06002E3B RID: 11835 RVA: 0x000D1A7C File Offset: 0x000CFC7C
		public ListViewItem(string[] items, int imageIndex, Color foreColor, Color backColor, Font font)
			: this(items, imageIndex)
		{
			this.ForeColor = foreColor;
			this.BackColor = backColor;
			this.Font = font;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the image index position of the item's icon and an array of <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> objects.</summary>
		/// <param name="subItems">An array of type <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> that represents the subitems of the item.</param>
		/// <param name="imageIndex">The zero-based index of the image within the <see cref="T:System.Windows.Forms.ImageList" /> associated with the <see cref="T:System.Windows.Forms.ListView" /> that contains the item.</param>
		// Token: 0x06002E3C RID: 11836 RVA: 0x000D1AA0 File Offset: 0x000CFCA0
		public ListViewItem(ListViewItem.ListViewSubItem[] subItems, int imageIndex)
			: this()
		{
			this.ImageIndexer.Index = imageIndex;
			this.subItems = subItems;
			this.SubItemCount = this.subItems.Length;
			for (int i = 0; i < subItems.Length; i++)
			{
				subItems[i].owner = this;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class and assigns it to the specified group.</summary>
		/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to assign the item to.</param>
		// Token: 0x06002E3D RID: 11837 RVA: 0x000D1AEB File Offset: 0x000CFCEB
		public ListViewItem(ListViewGroup group)
			: this()
		{
			this.Group = group;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the specified item text and assigns it to the specified group.</summary>
		/// <param name="text">The text to display for the item. This should not exceed 259 characters.</param>
		/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to assign the item to.</param>
		// Token: 0x06002E3E RID: 11838 RVA: 0x000D1AFA File Offset: 0x000CFCFA
		public ListViewItem(string text, ListViewGroup group)
			: this(text)
		{
			this.Group = group;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the specified item text and the image index position of the item's icon, and assigns the item to the specified group.</summary>
		/// <param name="text">The text to display for the item. This should not exceed 259 characters.</param>
		/// <param name="imageIndex">The zero-based index of the image within the <see cref="T:System.Windows.Forms.ImageList" /> associated with the <see cref="T:System.Windows.Forms.ListView" /> that contains the item.</param>
		/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to assign the item to.</param>
		// Token: 0x06002E3F RID: 11839 RVA: 0x000D1B0A File Offset: 0x000CFD0A
		public ListViewItem(string text, int imageIndex, ListViewGroup group)
			: this(text, imageIndex)
		{
			this.Group = group;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with an array of strings representing subitems, and assigns the item to the specified group.</summary>
		/// <param name="items">An array of strings that represent the subitems of the new item.</param>
		/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to assign the item to.</param>
		// Token: 0x06002E40 RID: 11840 RVA: 0x000D1B1B File Offset: 0x000CFD1B
		public ListViewItem(string[] items, ListViewGroup group)
			: this(items)
		{
			this.Group = group;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the image index position of the item's icon and an array of strings representing subitems, and assigns the item to the specified group.</summary>
		/// <param name="items">An array of strings that represent the subitems of the new item.</param>
		/// <param name="imageIndex">The zero-based index of the image within the <see cref="T:System.Windows.Forms.ImageList" /> associated with the <see cref="T:System.Windows.Forms.ListView" /> that contains the item.</param>
		/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to assign the item to.</param>
		// Token: 0x06002E41 RID: 11841 RVA: 0x000D1B2B File Offset: 0x000CFD2B
		public ListViewItem(string[] items, int imageIndex, ListViewGroup group)
			: this(items, imageIndex)
		{
			this.Group = group;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the image index position of the item's icon; the foreground color, background color, and font of the item; and an array of strings representing subitems. Assigns the item to the specified group.</summary>
		/// <param name="items">An array of strings that represent the subitems of the new item.</param>
		/// <param name="imageIndex">The zero-based index of the image within the <see cref="T:System.Windows.Forms.ImageList" /> associated with the <see cref="T:System.Windows.Forms.ListView" /> that contains the item.</param>
		/// <param name="foreColor">A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the item.</param>
		/// <param name="backColor">A <see cref="T:System.Drawing.Color" /> that represents the background color of the item.</param>
		/// <param name="font">A <see cref="T:System.Drawing.Font" /> that represents the font to display the item's text in.</param>
		/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to assign the item to.</param>
		// Token: 0x06002E42 RID: 11842 RVA: 0x000D1B3C File Offset: 0x000CFD3C
		public ListViewItem(string[] items, int imageIndex, Color foreColor, Color backColor, Font font, ListViewGroup group)
			: this(items, imageIndex, foreColor, backColor, font)
		{
			this.Group = group;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the image index position of the item's icon and an array of <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> objects, and assigns the item to the specified group.</summary>
		/// <param name="subItems">An array of type <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> that represents the subitems of the item.</param>
		/// <param name="imageIndex">The zero-based index of the image within the <see cref="T:System.Windows.Forms.ImageList" /> associated with the <see cref="T:System.Windows.Forms.ListView" /> that contains the item.</param>
		/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to assign the item to.</param>
		// Token: 0x06002E43 RID: 11843 RVA: 0x000D1B53 File Offset: 0x000CFD53
		public ListViewItem(ListViewItem.ListViewSubItem[] subItems, int imageIndex, ListViewGroup group)
			: this(subItems, imageIndex)
		{
			this.Group = group;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the specified text and image.</summary>
		/// <param name="text">The text to display for the item. This should not exceed 259 characters.</param>
		/// <param name="imageKey">The name of the image within the <see cref="P:System.Windows.Forms.ListViewItem.ImageList" /> of the owning <see cref="T:System.Windows.Forms.ListView" /> to display in the <see cref="T:System.Windows.Forms.ListViewItem" />.</param>
		// Token: 0x06002E44 RID: 11844 RVA: 0x000D1B64 File Offset: 0x000CFD64
		public ListViewItem(string text, string imageKey)
			: this()
		{
			this.ImageIndexer.Key = imageKey;
			this.Text = text;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the specified item and subitem text and image.</summary>
		/// <param name="items">An array containing the text of the subitems of the <see cref="T:System.Windows.Forms.ListViewItem" />.</param>
		/// <param name="imageKey">The name of the image within the <see cref="P:System.Windows.Forms.ListViewItem.ImageList" /> of the owning <see cref="T:System.Windows.Forms.ListView" /> to display in the <see cref="T:System.Windows.Forms.ListViewItem" />.</param>
		// Token: 0x06002E45 RID: 11845 RVA: 0x000D1B80 File Offset: 0x000CFD80
		public ListViewItem(string[] items, string imageKey)
			: this()
		{
			this.ImageIndexer.Key = imageKey;
			if (items != null && items.Length != 0)
			{
				this.subItems = new ListViewItem.ListViewSubItem[items.Length];
				for (int i = 0; i < items.Length; i++)
				{
					this.subItems[i] = new ListViewItem.ListViewSubItem(this, items[i]);
				}
				this.SubItemCount = items.Length;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the subitems containing the specified text, image, colors, and font.</summary>
		/// <param name="items">An array of strings that represent the text of the subitems for the <see cref="T:System.Windows.Forms.ListViewItem" />.</param>
		/// <param name="imageKey">The name of the image within the <see cref="P:System.Windows.Forms.ListViewItem.ImageList" /> of the owning <see cref="T:System.Windows.Forms.ListView" /> to display in the item.</param>
		/// <param name="foreColor">A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the item.</param>
		/// <param name="backColor">A <see cref="T:System.Drawing.Color" /> that represents the background color of the item.</param>
		/// <param name="font">A <see cref="T:System.Drawing.Font" /> to apply to the item text.</param>
		// Token: 0x06002E46 RID: 11846 RVA: 0x000D1BDC File Offset: 0x000CFDDC
		public ListViewItem(string[] items, string imageKey, Color foreColor, Color backColor, Font font)
			: this(items, imageKey)
		{
			this.ForeColor = foreColor;
			this.BackColor = backColor;
			this.Font = font;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the specified subitems and image.</summary>
		/// <param name="subItems">An array of <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> objects.</param>
		/// <param name="imageKey">The name of the image within the <see cref="P:System.Windows.Forms.ListViewItem.ImageList" /> of the owning <see cref="T:System.Windows.Forms.ListView" /> to display in the <see cref="T:System.Windows.Forms.ListViewItem" />.</param>
		// Token: 0x06002E47 RID: 11847 RVA: 0x000D1C00 File Offset: 0x000CFE00
		public ListViewItem(ListViewItem.ListViewSubItem[] subItems, string imageKey)
			: this()
		{
			this.ImageIndexer.Key = imageKey;
			this.subItems = subItems;
			this.SubItemCount = this.subItems.Length;
			for (int i = 0; i < subItems.Length; i++)
			{
				subItems[i].owner = this;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the specified text, image, and group.</summary>
		/// <param name="text">The text to display for the item. This should not exceed 259 characters.</param>
		/// <param name="imageKey">The name of the image within the <see cref="P:System.Windows.Forms.ListViewItem.ImageList" /> of the owning <see cref="T:System.Windows.Forms.ListView" /> to display in the item.</param>
		/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to assign the item to.</param>
		// Token: 0x06002E48 RID: 11848 RVA: 0x000D1C4B File Offset: 0x000CFE4B
		public ListViewItem(string text, string imageKey, ListViewGroup group)
			: this(text, imageKey)
		{
			this.Group = group;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with subitems containing the specified text, image, and group.</summary>
		/// <param name="items">An array of strings that represents the text for subitems of the <see cref="T:System.Windows.Forms.ListViewItem" />.</param>
		/// <param name="imageKey">The name of the image within the <see cref="P:System.Windows.Forms.ListViewItem.ImageList" /> of the owning <see cref="T:System.Windows.Forms.ListView" /> to display in the item.</param>
		/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to assign the item to.</param>
		// Token: 0x06002E49 RID: 11849 RVA: 0x000D1C5C File Offset: 0x000CFE5C
		public ListViewItem(string[] items, string imageKey, ListViewGroup group)
			: this(items, imageKey)
		{
			this.Group = group;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the subitems containing the specified text, image, colors, font, and group.</summary>
		/// <param name="items">An array of strings that represents the text of the subitems for the <see cref="T:System.Windows.Forms.ListViewItem" />.</param>
		/// <param name="imageKey">The name of the image within the <see cref="P:System.Windows.Forms.ListViewItem.ImageList" /> of the owning <see cref="T:System.Windows.Forms.ListView" /> to display in the item.</param>
		/// <param name="foreColor">A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the item.</param>
		/// <param name="backColor">A <see cref="T:System.Drawing.Color" /> that represents the background color of the item.</param>
		/// <param name="font">A <see cref="T:System.Drawing.Font" /> to apply to the item text.</param>
		/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to assign the item to.</param>
		// Token: 0x06002E4A RID: 11850 RVA: 0x000D1C6D File Offset: 0x000CFE6D
		public ListViewItem(string[] items, string imageKey, Color foreColor, Color backColor, Font font, ListViewGroup group)
			: this(items, imageKey, foreColor, backColor, font)
		{
			this.Group = group;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the specified subitems, image, and group.</summary>
		/// <param name="subItems">An array of <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> objects that represent the subitems of the <see cref="T:System.Windows.Forms.ListViewItem" />.</param>
		/// <param name="imageKey">The name of the image within the <see cref="P:System.Windows.Forms.ListViewItem.ImageList" /> of the owning <see cref="T:System.Windows.Forms.ListView" /> to display in the item.</param>
		/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to assign the item to.</param>
		// Token: 0x06002E4B RID: 11851 RVA: 0x000D1C84 File Offset: 0x000CFE84
		public ListViewItem(ListViewItem.ListViewSubItem[] subItems, string imageKey, ListViewGroup group)
			: this(subItems, imageKey)
		{
			this.Group = group;
		}

		/// <summary>Gets or sets the background color of the item's text.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the item's text.</returns>
		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x06002E4C RID: 11852 RVA: 0x000D1C95 File Offset: 0x000CFE95
		// (set) Token: 0x06002E4D RID: 11853 RVA: 0x000D1CC6 File Offset: 0x000CFEC6
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRCategory("CatAppearance")]
		public Color BackColor
		{
			get
			{
				if (this.SubItemCount != 0)
				{
					return this.subItems[0].BackColor;
				}
				if (this.listView != null)
				{
					return this.listView.BackColor;
				}
				return SystemColors.Window;
			}
			set
			{
				this.SubItems[0].BackColor = value;
			}
		}

		/// <summary>Gets the bounding rectangle of the item, including subitems.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounding rectangle of the item.</returns>
		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06002E4E RID: 11854 RVA: 0x000D1CDC File Offset: 0x000CFEDC
		[Browsable(false)]
		public Rectangle Bounds
		{
			get
			{
				if (this.listView != null)
				{
					return this.listView.GetItemRect(this.Index);
				}
				return default(Rectangle);
			}
		}

		/// <summary>Gets or sets a value indicating whether the item is checked.</summary>
		/// <returns>
		///   <see langword="true" /> if the item is checked; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06002E4F RID: 11855 RVA: 0x000D1D0C File Offset: 0x000CFF0C
		// (set) Token: 0x06002E50 RID: 11856 RVA: 0x000D1D18 File Offset: 0x000CFF18
		[DefaultValue(false)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRCategory("CatAppearance")]
		public bool Checked
		{
			get
			{
				return this.StateImageIndex > 0;
			}
			set
			{
				if (this.Checked != value)
				{
					if (this.listView != null && this.listView.IsHandleCreated)
					{
						this.StateImageIndex = (value ? 1 : 0);
						if (this.listView != null && !this.listView.UseCompatibleStateImageBehavior && !this.listView.CheckBoxes)
						{
							this.listView.UpdateSavedCheckedItems(this, value);
							return;
						}
					}
					else
					{
						this.SavedStateImageIndex = (value ? 1 : 0);
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the item has focus within the <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
		/// <returns>
		///   <see langword="true" /> if the item has focus; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06002E51 RID: 11857 RVA: 0x000D1D8D File Offset: 0x000CFF8D
		// (set) Token: 0x06002E52 RID: 11858 RVA: 0x000D1DBB File Offset: 0x000CFFBB
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool Focused
		{
			get
			{
				return this.listView != null && this.listView.IsHandleCreated && this.listView.GetItemState(this.Index, 1) != 0;
			}
			set
			{
				if (this.listView != null && this.listView.IsHandleCreated)
				{
					this.listView.SetItemState(this.Index, value ? 1 : 0, 1);
				}
			}
		}

		/// <summary>Gets or sets the font of the text displayed by the item.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultFont" /> property if the <see cref="T:System.Windows.Forms.ListViewItem" /> is not associated with a <see cref="T:System.Windows.Forms.ListView" /> control; otherwise, the font specified in the <see cref="P:System.Windows.Forms.Control.Font" /> property for the <see cref="T:System.Windows.Forms.ListView" /> control is used.</returns>
		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x06002E53 RID: 11859 RVA: 0x000D1DEB File Offset: 0x000CFFEB
		// (set) Token: 0x06002E54 RID: 11860 RVA: 0x000D1E1C File Offset: 0x000D001C
		[Localizable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRCategory("CatAppearance")]
		public Font Font
		{
			get
			{
				if (this.SubItemCount != 0)
				{
					return this.subItems[0].Font;
				}
				if (this.listView != null)
				{
					return this.listView.Font;
				}
				return Control.DefaultFont;
			}
			set
			{
				this.SubItems[0].Font = value;
			}
		}

		/// <summary>Gets or sets the foreground color of the item's text.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the item's text.</returns>
		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x06002E55 RID: 11861 RVA: 0x000D1E30 File Offset: 0x000D0030
		// (set) Token: 0x06002E56 RID: 11862 RVA: 0x000D1E61 File Offset: 0x000D0061
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRCategory("CatAppearance")]
		public Color ForeColor
		{
			get
			{
				if (this.SubItemCount != 0)
				{
					return this.subItems[0].ForeColor;
				}
				if (this.listView != null)
				{
					return this.listView.ForeColor;
				}
				return SystemColors.WindowText;
			}
			set
			{
				this.SubItems[0].ForeColor = value;
			}
		}

		/// <summary>Gets or sets the group to which the item is assigned.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ListViewGroup" /> to which the item is assigned.</returns>
		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x06002E57 RID: 11863 RVA: 0x000D1E75 File Offset: 0x000D0075
		// (set) Token: 0x06002E58 RID: 11864 RVA: 0x000D1E7D File Offset: 0x000D007D
		[DefaultValue(null)]
		[Localizable(true)]
		[SRCategory("CatBehavior")]
		public ListViewGroup Group
		{
			get
			{
				return this.group;
			}
			set
			{
				if (this.group != value)
				{
					if (value != null)
					{
						value.Items.Add(this);
					}
					else
					{
						this.group.Items.Remove(this);
					}
				}
				this.groupName = null;
			}
		}

		/// <summary>Gets or sets the index of the image that is displayed for the item.</summary>
		/// <returns>The zero-based index of the image in the <see cref="T:System.Windows.Forms.ImageList" /> that is displayed for the item. The default is -1.</returns>
		/// <exception cref="T:System.ArgumentException">The value specified is less than -1.</exception>
		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x06002E59 RID: 11865 RVA: 0x000D1EB4 File Offset: 0x000D00B4
		// (set) Token: 0x06002E5A RID: 11866 RVA: 0x000D1F14 File Offset: 0x000D0114
		[DefaultValue(-1)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRCategory("CatBehavior")]
		[SRDescription("ListViewItemImageIndexDescr")]
		[TypeConverter(typeof(NoneExcludedImageIndexConverter))]
		public int ImageIndex
		{
			get
			{
				if (this.ImageIndexer.Index != -1 && this.ImageList != null && this.ImageIndexer.Index >= this.ImageList.Images.Count)
				{
					return this.ImageList.Images.Count - 1;
				}
				return this.ImageIndexer.Index;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("ImageIndex", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"ImageIndex",
						value.ToString(CultureInfo.CurrentCulture),
						(-1).ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.ImageIndexer.Index = value;
				if (this.listView != null && this.listView.IsHandleCreated)
				{
					this.listView.SetItemImage(this.Index, this.ImageIndexer.ActualIndex);
				}
			}
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x06002E5B RID: 11867 RVA: 0x000D1FA5 File Offset: 0x000D01A5
		internal ListViewItem.ListViewItemImageIndexer ImageIndexer
		{
			get
			{
				if (this.imageIndexer == null)
				{
					this.imageIndexer = new ListViewItem.ListViewItemImageIndexer(this);
				}
				return this.imageIndexer;
			}
		}

		/// <summary>Gets or sets the key for the image that is displayed for the item.</summary>
		/// <returns>The key for the image that is displayed for the <see cref="T:System.Windows.Forms.ListViewItem" />.</returns>
		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x06002E5C RID: 11868 RVA: 0x000D1FC1 File Offset: 0x000D01C1
		// (set) Token: 0x06002E5D RID: 11869 RVA: 0x000D1FCE File Offset: 0x000D01CE
		[DefaultValue("")]
		[TypeConverter(typeof(ImageKeyConverter))]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		public string ImageKey
		{
			get
			{
				return this.ImageIndexer.Key;
			}
			set
			{
				this.ImageIndexer.Key = value;
				if (this.listView != null && this.listView.IsHandleCreated)
				{
					this.listView.SetItemImage(this.Index, this.ImageIndexer.ActualIndex);
				}
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ImageList" /> that contains the image displayed with the item.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ImageList" /> used by the <see cref="T:System.Windows.Forms.ListView" /> control that contains the image displayed with the item.</returns>
		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x06002E5E RID: 11870 RVA: 0x000D2010 File Offset: 0x000D0210
		[Browsable(false)]
		public ImageList ImageList
		{
			get
			{
				if (this.listView != null)
				{
					switch (this.listView.View)
					{
					case View.LargeIcon:
					case View.Tile:
						return this.listView.LargeImageList;
					case View.Details:
					case View.SmallIcon:
					case View.List:
						return this.listView.SmallImageList;
					}
				}
				return null;
			}
		}

		/// <summary>Gets or sets the number of small image widths by which to indent the <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
		/// <returns>The number of small image widths by which to indent the <see cref="T:System.Windows.Forms.ListViewItem" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">When setting <see cref="P:System.Windows.Forms.ListViewItem.IndentCount" />, the number specified is less than 0.</exception>
		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x06002E5F RID: 11871 RVA: 0x000D2066 File Offset: 0x000D0266
		// (set) Token: 0x06002E60 RID: 11872 RVA: 0x000D2070 File Offset: 0x000D0270
		[DefaultValue(0)]
		[SRDescription("ListViewItemIndentCountDescr")]
		[SRCategory("CatDisplay")]
		public int IndentCount
		{
			get
			{
				return this.indentCount;
			}
			set
			{
				if (value == this.indentCount)
				{
					return;
				}
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("IndentCount", SR.GetString("ListViewIndentCountCantBeNegative"));
				}
				this.indentCount = value;
				if (this.listView != null && this.listView.IsHandleCreated)
				{
					this.listView.SetItemIndentCount(this.Index, this.indentCount);
				}
			}
		}

		/// <summary>Gets the zero-based index of the item within the <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
		/// <returns>The zero-based index of the item within the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> of the <see cref="T:System.Windows.Forms.ListView" /> control, or -1 if the item is not associated with a <see cref="T:System.Windows.Forms.ListView" /> control.</returns>
		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x06002E61 RID: 11873 RVA: 0x000D20D3 File Offset: 0x000D02D3
		[Browsable(false)]
		public int Index
		{
			get
			{
				if (this.listView != null)
				{
					if (!this.listView.VirtualMode)
					{
						this.lastIndex = this.listView.GetDisplayIndex(this, this.lastIndex);
					}
					return this.lastIndex;
				}
				return -1;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ListView" /> control that contains the item.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListView" /> that contains the <see cref="T:System.Windows.Forms.ListViewItem" />.</returns>
		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x06002E62 RID: 11874 RVA: 0x000D210A File Offset: 0x000D030A
		[Browsable(false)]
		public ListView ListView
		{
			get
			{
				return this.listView;
			}
		}

		/// <summary>Gets or sets the name associated with this <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
		/// <returns>The name of the <see cref="T:System.Windows.Forms.ListViewItem" />. The default is an empty string ("").</returns>
		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x06002E63 RID: 11875 RVA: 0x000D2112 File Offset: 0x000D0312
		// (set) Token: 0x06002E64 RID: 11876 RVA: 0x000D212F File Offset: 0x000D032F
		[Localizable(true)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Name
		{
			get
			{
				if (this.SubItemCount == 0)
				{
					return string.Empty;
				}
				return this.subItems[0].Name;
			}
			set
			{
				this.SubItems[0].Name = value;
			}
		}

		/// <summary>Gets or sets the position of the upper-left corner of the <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Point" /> at the upper-left corner of the <see cref="T:System.Windows.Forms.ListViewItem" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.ListViewItem.Position" /> is set when the containing <see cref="T:System.Windows.Forms.ListView" /> is in virtual mode.</exception>
		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x06002E65 RID: 11877 RVA: 0x000D2143 File Offset: 0x000D0343
		// (set) Token: 0x06002E66 RID: 11878 RVA: 0x000D2178 File Offset: 0x000D0378
		[SRCategory("CatDisplay")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public Point Position
		{
			get
			{
				if (this.listView != null && this.listView.IsHandleCreated)
				{
					this.position = this.listView.GetItemPosition(this.Index);
				}
				return this.position;
			}
			set
			{
				if (value.Equals(this.position))
				{
					return;
				}
				this.position = value;
				if (this.listView != null && this.listView.IsHandleCreated && !this.listView.VirtualMode)
				{
					this.listView.SetItemPosition(this.Index, this.position.X, this.position.Y);
				}
			}
		}

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06002E67 RID: 11879 RVA: 0x000D21F0 File Offset: 0x000D03F0
		internal int RawStateImageIndex
		{
			get
			{
				return this.SavedStateImageIndex + 1 << 12;
			}
		}

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x06002E68 RID: 11880 RVA: 0x000D21FD File Offset: 0x000D03FD
		// (set) Token: 0x06002E69 RID: 11881 RVA: 0x000D2211 File Offset: 0x000D0411
		private int SavedStateImageIndex
		{
			get
			{
				return this.state[ListViewItem.SavedStateImageIndexSection] - 1;
			}
			set
			{
				this.state[ListViewItem.StateImageMaskSet] = ((value == -1) ? 0 : 1);
				this.state[ListViewItem.SavedStateImageIndexSection] = value + 1;
			}
		}

		/// <summary>Gets or sets a value indicating whether the item is selected.</summary>
		/// <returns>
		///   <see langword="true" /> if the item is selected; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x06002E6A RID: 11882 RVA: 0x000D223E File Offset: 0x000D043E
		// (set) Token: 0x06002E6B RID: 11883 RVA: 0x000D2274 File Offset: 0x000D0474
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Selected
		{
			get
			{
				if (this.listView != null && this.listView.IsHandleCreated)
				{
					return this.listView.GetItemState(this.Index, 2) != 0;
				}
				return this.StateSelected;
			}
			set
			{
				if (this.listView != null && this.listView.IsHandleCreated)
				{
					this.listView.SetItemState(this.Index, value ? 2 : 0, 2);
					this.listView.SetSelectionMark(this.Index);
					return;
				}
				this.StateSelected = value;
				if (this.listView != null && this.listView.IsHandleCreated)
				{
					this.listView.CacheSelectedStateForItem(this, value);
				}
			}
		}

		/// <summary>Gets or sets the index of the state image (an image such as a selected or cleared check box that indicates the state of the item) that is displayed for the item.</summary>
		/// <returns>The zero-based index of the state image in the <see cref="T:System.Windows.Forms.ImageList" /> that is displayed for the item.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for this property is less than -1.  
		///  -or-  
		///  The value specified for this property is greater than 14.</exception>
		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x06002E6C RID: 11884 RVA: 0x000D22EC File Offset: 0x000D04EC
		// (set) Token: 0x06002E6D RID: 11885 RVA: 0x000D2334 File Offset: 0x000D0534
		[Localizable(true)]
		[TypeConverter(typeof(NoneExcludedImageIndexConverter))]
		[DefaultValue(-1)]
		[SRDescription("ListViewItemStateImageIndexDescr")]
		[SRCategory("CatBehavior")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[RelatedImageList("ListView.StateImageList")]
		public int StateImageIndex
		{
			get
			{
				if (this.listView != null && this.listView.IsHandleCreated)
				{
					int itemState = this.listView.GetItemState(this.Index, 61440);
					return (itemState >> 12) - 1;
				}
				return this.SavedStateImageIndex;
			}
			set
			{
				if (value < -1 || value > 14)
				{
					throw new ArgumentOutOfRangeException("StateImageIndex", SR.GetString("InvalidArgument", new object[]
					{
						"StateImageIndex",
						value.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this.listView != null && this.listView.IsHandleCreated)
				{
					this.state[ListViewItem.StateImageMaskSet] = ((value == -1) ? 0 : 1);
					int num = value + 1 << 12;
					this.listView.SetItemState(this.Index, num, 61440);
				}
				this.SavedStateImageIndex = value;
			}
		}

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x06002E6E RID: 11886 RVA: 0x000D23CE File Offset: 0x000D05CE
		internal bool StateImageSet
		{
			get
			{
				return this.state[ListViewItem.StateImageMaskSet] != 0;
			}
		}

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06002E6F RID: 11887 RVA: 0x000D23E3 File Offset: 0x000D05E3
		// (set) Token: 0x06002E70 RID: 11888 RVA: 0x000D23F8 File Offset: 0x000D05F8
		internal bool StateSelected
		{
			get
			{
				return this.state[ListViewItem.StateSelectedSection] == 1;
			}
			set
			{
				this.state[ListViewItem.StateSelectedSection] = (value ? 1 : 0);
			}
		}

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06002E71 RID: 11889 RVA: 0x000D2411 File Offset: 0x000D0611
		// (set) Token: 0x06002E72 RID: 11890 RVA: 0x000D2423 File Offset: 0x000D0623
		private int SubItemCount
		{
			get
			{
				return this.state[ListViewItem.SubItemCountSection];
			}
			set
			{
				this.state[ListViewItem.SubItemCountSection] = value;
			}
		}

		/// <summary>Gets a collection containing all subitems of the item.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItemCollection" /> that contains the subitems.</returns>
		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x06002E73 RID: 11891 RVA: 0x000D2438 File Offset: 0x000D0638
		[SRCategory("CatData")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ListViewItemSubItemsDescr")]
		[Editor("System.Windows.Forms.Design.ListViewSubItemCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public ListViewItem.ListViewSubItemCollection SubItems
		{
			get
			{
				if (this.SubItemCount == 0)
				{
					this.subItems = new ListViewItem.ListViewSubItem[1];
					this.subItems[0] = new ListViewItem.ListViewSubItem(this, string.Empty);
					this.SubItemCount = 1;
				}
				if (this.listViewSubItemCollection == null)
				{
					this.listViewSubItemCollection = new ListViewItem.ListViewSubItemCollection(this);
				}
				return this.listViewSubItemCollection;
			}
		}

		/// <summary>Gets or sets an object that contains data to associate with the item.</summary>
		/// <returns>An object that contains information that is associated with the item.</returns>
		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x06002E74 RID: 11892 RVA: 0x000D248D File Offset: 0x000D068D
		// (set) Token: 0x06002E75 RID: 11893 RVA: 0x000D2495 File Offset: 0x000D0695
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

		/// <summary>Gets or sets the text of the item.</summary>
		/// <returns>The text to display for the item. This should not exceed 259 characters.</returns>
		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x06002E76 RID: 11894 RVA: 0x000D249E File Offset: 0x000D069E
		// (set) Token: 0x06002E77 RID: 11895 RVA: 0x000D24BB File Offset: 0x000D06BB
		[Localizable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRCategory("CatAppearance")]
		public string Text
		{
			get
			{
				if (this.SubItemCount == 0)
				{
					return string.Empty;
				}
				return this.subItems[0].Text;
			}
			set
			{
				this.SubItems[0].Text = value;
			}
		}

		/// <summary>Gets or sets the text shown when the mouse pointer rests on the <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
		/// <returns>The text shown when the mouse pointer rests on the <see cref="T:System.Windows.Forms.ListViewItem" />.</returns>
		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06002E78 RID: 11896 RVA: 0x000D24CF File Offset: 0x000D06CF
		// (set) Token: 0x06002E79 RID: 11897 RVA: 0x000D24D8 File Offset: 0x000D06D8
		[SRCategory("CatAppearance")]
		[DefaultValue("")]
		public string ToolTipText
		{
			get
			{
				return this.toolTipText;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (WindowsFormsUtils.SafeCompareStrings(this.toolTipText, value, false))
				{
					return;
				}
				this.toolTipText = value;
				if (this.listView != null && this.listView.IsHandleCreated)
				{
					this.listView.ListViewItemToolTipChanged(this);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="P:System.Windows.Forms.ListViewItem.Font" />, <see cref="P:System.Windows.Forms.ListViewItem.ForeColor" />, and <see cref="P:System.Windows.Forms.ListViewItem.BackColor" /> properties for the item are used for all its subitems.</summary>
		/// <returns>
		///   <see langword="true" /> if all subitems use the font, foreground color, and background color settings of the item; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x06002E7A RID: 11898 RVA: 0x000D2527 File Offset: 0x000D0727
		// (set) Token: 0x06002E7B RID: 11899 RVA: 0x000D253C File Offset: 0x000D073C
		[DefaultValue(true)]
		[SRCategory("CatAppearance")]
		public bool UseItemStyleForSubItems
		{
			get
			{
				return this.state[ListViewItem.StateWholeRowOneStyleSection] == 1;
			}
			set
			{
				this.state[ListViewItem.StateWholeRowOneStyleSection] = (value ? 1 : 0);
			}
		}

		/// <summary>Places the item text into edit mode.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.ListView.LabelEdit" /> property of the associated <see cref="T:System.Windows.Forms.ListView" /> is not set to <see langword="true" />.</exception>
		// Token: 0x06002E7C RID: 11900 RVA: 0x000D2558 File Offset: 0x000D0758
		public void BeginEdit()
		{
			if (this.Index >= 0)
			{
				ListView listView = this.ListView;
				if (!listView.LabelEdit)
				{
					throw new InvalidOperationException(SR.GetString("ListViewBeginEditFailed"));
				}
				if (!listView.Focused)
				{
					listView.FocusInternal();
				}
				UnsafeNativeMethods.SendMessage(new HandleRef(listView, listView.Handle), NativeMethods.LVM_EDITLABEL, this.Index, 0);
			}
		}

		/// <summary>Creates an identical copy of the item.</summary>
		/// <returns>An object that represents an item that has the same text, image, and subitems associated with it as the cloned item.</returns>
		// Token: 0x06002E7D RID: 11901 RVA: 0x000D25BC File Offset: 0x000D07BC
		public virtual object Clone()
		{
			ListViewItem.ListViewSubItem[] array = new ListViewItem.ListViewSubItem[this.SubItems.Count];
			for (int i = 0; i < this.SubItems.Count; i++)
			{
				ListViewItem.ListViewSubItem listViewSubItem = this.SubItems[i];
				array[i] = new ListViewItem.ListViewSubItem(null, listViewSubItem.Text, listViewSubItem.ForeColor, listViewSubItem.BackColor, listViewSubItem.Font);
				array[i].Tag = listViewSubItem.Tag;
			}
			Type type = base.GetType();
			ListViewItem listViewItem;
			if (type == typeof(ListViewItem))
			{
				listViewItem = new ListViewItem(array, this.ImageIndexer.Index);
			}
			else
			{
				listViewItem = (ListViewItem)Activator.CreateInstance(type);
			}
			listViewItem.subItems = array;
			listViewItem.ImageIndexer.Index = this.ImageIndexer.Index;
			listViewItem.SubItemCount = this.SubItemCount;
			listViewItem.Checked = this.Checked;
			listViewItem.UseItemStyleForSubItems = this.UseItemStyleForSubItems;
			listViewItem.Tag = this.Tag;
			if (!string.IsNullOrEmpty(this.ImageIndexer.Key))
			{
				listViewItem.ImageIndexer.Key = this.ImageIndexer.Key;
			}
			listViewItem.indentCount = this.indentCount;
			listViewItem.StateImageIndex = this.StateImageIndex;
			listViewItem.toolTipText = this.toolTipText;
			listViewItem.BackColor = this.BackColor;
			listViewItem.ForeColor = this.ForeColor;
			listViewItem.Font = this.Font;
			listViewItem.Text = this.Text;
			listViewItem.Group = this.Group;
			return listViewItem;
		}

		/// <summary>Ensures that the item is visible within the control, scrolling the contents of the control, if necessary.</summary>
		// Token: 0x06002E7E RID: 11902 RVA: 0x000D2743 File Offset: 0x000D0943
		public virtual void EnsureVisible()
		{
			if (this.listView != null && this.listView.IsHandleCreated)
			{
				this.listView.EnsureVisible(this.Index);
			}
		}

		/// <summary>Finds the next item from the <see cref="T:System.Windows.Forms.ListViewItem" />, searching in the specified direction.</summary>
		/// <param name="searchDirection">One of the <see cref="T:System.Windows.Forms.SearchDirectionHint" /> values.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> that is closest to the given coordinates, searching in the specified direction.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.ListView.View" /> property of the containing <see cref="T:System.Windows.Forms.ListView" /> is set to a value other than <see cref="F:System.Windows.Forms.View.SmallIcon" /> or <see cref="F:System.Windows.Forms.View.LargeIcon" />.</exception>
		// Token: 0x06002E7F RID: 11903 RVA: 0x000D276C File Offset: 0x000D096C
		public ListViewItem FindNearestItem(SearchDirectionHint searchDirection)
		{
			Rectangle bounds = this.Bounds;
			switch (searchDirection)
			{
			case SearchDirectionHint.Left:
				return this.ListView.FindNearestItem(searchDirection, bounds.Left, bounds.Top);
			case SearchDirectionHint.Up:
				return this.ListView.FindNearestItem(searchDirection, bounds.Left, bounds.Top);
			case SearchDirectionHint.Right:
				return this.ListView.FindNearestItem(searchDirection, bounds.Right, bounds.Top);
			case SearchDirectionHint.Down:
				return this.ListView.FindNearestItem(searchDirection, bounds.Left, bounds.Bottom);
			default:
				return null;
			}
		}

		/// <summary>Retrieves the specified portion of the bounding rectangle for the item.</summary>
		/// <param name="portion">One of the <see cref="T:System.Windows.Forms.ItemBoundsPortion" /> values that represents a portion of the item for which to retrieve the bounding rectangle.</param>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounding rectangle for the specified portion of the item.</returns>
		// Token: 0x06002E80 RID: 11904 RVA: 0x000D2808 File Offset: 0x000D0A08
		public Rectangle GetBounds(ItemBoundsPortion portion)
		{
			if (this.listView != null && this.listView.IsHandleCreated)
			{
				return this.listView.GetItemRect(this.Index, portion);
			}
			return default(Rectangle);
		}

		/// <summary>Returns the subitem of the <see cref="T:System.Windows.Forms.ListViewItem" /> at the specified coordinates.</summary>
		/// <param name="x">The x-coordinate.</param>
		/// <param name="y">The y-coordinate.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> at the specified x- and y-coordinates.</returns>
		// Token: 0x06002E81 RID: 11905 RVA: 0x000D2848 File Offset: 0x000D0A48
		public ListViewItem.ListViewSubItem GetSubItemAt(int x, int y)
		{
			if (this.listView == null || !this.listView.IsHandleCreated || this.listView.View != View.Details)
			{
				return null;
			}
			int num = -1;
			int num2 = -1;
			this.listView.GetSubItemAt(x, y, out num, out num2);
			if (num == this.Index && num2 != -1 && num2 < this.SubItems.Count)
			{
				return this.SubItems[num2];
			}
			return null;
		}

		// Token: 0x06002E82 RID: 11906 RVA: 0x000D28B8 File Offset: 0x000D0AB8
		internal void Host(ListView parent, int ID, int index)
		{
			this.ID = ID;
			this.listView = parent;
			if (index != -1)
			{
				this.UpdateStateToListView(index);
			}
		}

		// Token: 0x06002E83 RID: 11907 RVA: 0x000D28D4 File Offset: 0x000D0AD4
		internal void UpdateGroupFromName()
		{
			if (string.IsNullOrEmpty(this.groupName))
			{
				return;
			}
			ListViewGroup listViewGroup = this.listView.Groups[this.groupName];
			this.Group = listViewGroup;
			this.groupName = null;
		}

		// Token: 0x06002E84 RID: 11908 RVA: 0x000D2914 File Offset: 0x000D0B14
		internal void UpdateStateToListView(int index)
		{
			NativeMethods.LVITEM lvitem = default(NativeMethods.LVITEM);
			this.UpdateStateToListView(index, ref lvitem, true);
		}

		// Token: 0x06002E85 RID: 11909 RVA: 0x000D2934 File Offset: 0x000D0B34
		internal void UpdateStateToListView(int index, ref NativeMethods.LVITEM lvItem, bool updateOwner)
		{
			if (index == -1)
			{
				index = this.Index;
			}
			else
			{
				this.lastIndex = index;
			}
			int num = 0;
			int num2 = 0;
			if (this.StateSelected)
			{
				num |= 2;
				num2 |= 2;
			}
			if (this.SavedStateImageIndex > -1)
			{
				num |= this.SavedStateImageIndex + 1 << 12;
				num2 |= 61440;
			}
			lvItem.mask |= 8;
			lvItem.iItem = index;
			lvItem.stateMask |= num2;
			lvItem.state |= num;
			if (this.listView.GroupsEnabled)
			{
				lvItem.mask |= 256;
				lvItem.iGroupId = this.listView.GetNativeGroupId(this);
			}
			if (updateOwner)
			{
				UnsafeNativeMethods.SendMessage(new HandleRef(this.listView, this.listView.Handle), NativeMethods.LVM_SETITEM, 0, ref lvItem);
			}
		}

		// Token: 0x06002E86 RID: 11910 RVA: 0x000D2A08 File Offset: 0x000D0C08
		internal void UpdateStateFromListView(int displayIndex, bool checkSelection)
		{
			if (this.listView != null && this.listView.IsHandleCreated && displayIndex != -1)
			{
				NativeMethods.LVITEM lvitem = default(NativeMethods.LVITEM);
				lvitem.mask = 268;
				if (checkSelection)
				{
					lvitem.stateMask = 2;
				}
				lvitem.stateMask |= 61440;
				if (lvitem.stateMask == 0)
				{
					return;
				}
				lvitem.iItem = displayIndex;
				UnsafeNativeMethods.SendMessage(new HandleRef(this.listView, this.listView.Handle), NativeMethods.LVM_GETITEM, 0, ref lvitem);
				if (checkSelection)
				{
					this.StateSelected = (lvitem.state & 2) != 0;
				}
				this.SavedStateImageIndex = ((lvitem.state & 61440) >> 12) - 1;
				this.group = null;
				foreach (object obj in this.ListView.Groups)
				{
					ListViewGroup listViewGroup = (ListViewGroup)obj;
					if (listViewGroup.ID == lvitem.iGroupId)
					{
						this.group = listViewGroup;
						break;
					}
				}
			}
		}

		// Token: 0x06002E87 RID: 11911 RVA: 0x000D2B30 File Offset: 0x000D0D30
		internal void UnHost(bool checkSelection)
		{
			this.UnHost(this.Index, checkSelection);
		}

		// Token: 0x06002E88 RID: 11912 RVA: 0x000D2B40 File Offset: 0x000D0D40
		internal void UnHost(int displayIndex, bool checkSelection)
		{
			this.UpdateStateFromListView(displayIndex, checkSelection);
			if (this.listView != null && (this.listView.Site == null || !this.listView.Site.DesignMode) && this.group != null)
			{
				this.group.Items.Remove(this);
			}
			this.ID = -1;
			this.listView = null;
		}

		/// <summary>Removes the item from its associated <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
		// Token: 0x06002E89 RID: 11913 RVA: 0x000D2BA3 File Offset: 0x000D0DA3
		public virtual void Remove()
		{
			if (this.listView != null)
			{
				this.listView.Items.Remove(this);
			}
		}

		/// <summary>Deserializes the item.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the data needed to deserialize the item.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that represents the source and destination of the stream being deserialized.</param>
		// Token: 0x06002E8A RID: 11914 RVA: 0x000D2BC0 File Offset: 0x000D0DC0
		protected virtual void Deserialize(SerializationInfo info, StreamingContext context)
		{
			bool flag = false;
			string text = null;
			int num = -1;
			foreach (SerializationEntry serializationEntry in info)
			{
				if (serializationEntry.Name == "Text")
				{
					this.Text = info.GetString(serializationEntry.Name);
				}
				else if (serializationEntry.Name == "ImageIndex")
				{
					num = info.GetInt32(serializationEntry.Name);
				}
				else if (serializationEntry.Name == "ImageKey")
				{
					text = info.GetString(serializationEntry.Name);
				}
				else if (serializationEntry.Name == "SubItemCount")
				{
					this.SubItemCount = info.GetInt32(serializationEntry.Name);
					if (this.SubItemCount > 0)
					{
						flag = true;
					}
				}
				else if (serializationEntry.Name == "BackColor")
				{
					this.BackColor = (Color)info.GetValue(serializationEntry.Name, typeof(Color));
				}
				else if (serializationEntry.Name == "Checked")
				{
					this.Checked = info.GetBoolean(serializationEntry.Name);
				}
				else if (serializationEntry.Name == "Font")
				{
					this.Font = (Font)info.GetValue(serializationEntry.Name, typeof(Font));
				}
				else if (serializationEntry.Name == "ForeColor")
				{
					this.ForeColor = (Color)info.GetValue(serializationEntry.Name, typeof(Color));
				}
				else if (serializationEntry.Name == "UseItemStyleForSubItems")
				{
					this.UseItemStyleForSubItems = info.GetBoolean(serializationEntry.Name);
				}
				else if (serializationEntry.Name == "Group")
				{
					ListViewGroup listViewGroup = (ListViewGroup)info.GetValue(serializationEntry.Name, typeof(ListViewGroup));
					this.groupName = listViewGroup.Name;
				}
			}
			if (text != null)
			{
				this.ImageKey = text;
			}
			else if (num != -1)
			{
				this.ImageIndex = num;
			}
			if (flag)
			{
				ListViewItem.ListViewSubItem[] array = new ListViewItem.ListViewSubItem[this.SubItemCount];
				for (int i = 1; i < this.SubItemCount; i++)
				{
					ListViewItem.ListViewSubItem listViewSubItem = (ListViewItem.ListViewSubItem)info.GetValue("SubItem" + i.ToString(CultureInfo.InvariantCulture), typeof(ListViewItem.ListViewSubItem));
					listViewSubItem.owner = this;
					array[i] = listViewSubItem;
				}
				array[0] = this.subItems[0];
				this.subItems = array;
			}
		}

		/// <summary>Serializes the item.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the data needed to serialize the item.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that represents the source and destination of the stream being serialized.</param>
		// Token: 0x06002E8B RID: 11915 RVA: 0x000D2E6C File Offset: 0x000D106C
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		protected virtual void Serialize(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Text", this.Text);
			info.AddValue("ImageIndex", this.ImageIndexer.Index);
			if (!string.IsNullOrEmpty(this.ImageIndexer.Key))
			{
				info.AddValue("ImageKey", this.ImageIndexer.Key);
			}
			if (this.SubItemCount > 1)
			{
				info.AddValue("SubItemCount", this.SubItemCount);
				for (int i = 1; i < this.SubItemCount; i++)
				{
					info.AddValue("SubItem" + i.ToString(CultureInfo.InvariantCulture), this.subItems[i], typeof(ListViewItem.ListViewSubItem));
				}
			}
			info.AddValue("BackColor", this.BackColor);
			info.AddValue("Checked", this.Checked);
			info.AddValue("Font", this.Font);
			info.AddValue("ForeColor", this.ForeColor);
			info.AddValue("UseItemStyleForSubItems", this.UseItemStyleForSubItems);
			if (this.Group != null)
			{
				info.AddValue("Group", this.Group);
			}
		}

		// Token: 0x06002E8C RID: 11916 RVA: 0x000D2F99 File Offset: 0x000D1199
		internal void SetItemIndex(ListView listView, int index)
		{
			this.listView = listView;
			this.lastIndex = index;
		}

		// Token: 0x06002E8D RID: 11917 RVA: 0x0001180C File Offset: 0x0000FA0C
		internal bool ShouldSerializeText()
		{
			return false;
		}

		// Token: 0x06002E8E RID: 11918 RVA: 0x000D2FA9 File Offset: 0x000D11A9
		private bool ShouldSerializePosition()
		{
			return !this.position.Equals(new Point(-1, -1));
		}

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		// Token: 0x06002E8F RID: 11919 RVA: 0x000D2FCB File Offset: 0x000D11CB
		public override string ToString()
		{
			return "ListViewItem: {" + this.Text + "}";
		}

		// Token: 0x06002E90 RID: 11920 RVA: 0x000D2FE2 File Offset: 0x000D11E2
		internal void InvalidateListView()
		{
			if (this.listView != null && this.listView.IsHandleCreated)
			{
				this.listView.Invalidate();
			}
		}

		// Token: 0x06002E91 RID: 11921 RVA: 0x000D3004 File Offset: 0x000D1204
		internal void UpdateSubItems(int index)
		{
			this.UpdateSubItems(index, this.SubItemCount);
		}

		// Token: 0x06002E92 RID: 11922 RVA: 0x000D3014 File Offset: 0x000D1214
		internal void UpdateSubItems(int index, int oldCount)
		{
			if (this.listView != null && this.listView.IsHandleCreated)
			{
				int subItemCount = this.SubItemCount;
				int index2 = this.Index;
				if (index != -1)
				{
					this.listView.SetItemText(index2, index, this.subItems[index].Text);
				}
				else
				{
					for (int i = 0; i < subItemCount; i++)
					{
						this.listView.SetItemText(index2, i, this.subItems[i].Text);
					}
				}
				for (int j = subItemCount; j < oldCount; j++)
				{
					this.listView.SetItemText(index2, j, string.Empty);
				}
			}
		}

		/// <summary>Serializes the item.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the data needed to serialize the item.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that represents the source and destination of the stream being serialized.</param>
		// Token: 0x06002E93 RID: 11923 RVA: 0x000D30A8 File Offset: 0x000D12A8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			this.Serialize(info, context);
		}

		// Token: 0x0400131E RID: 4894
		private const int MAX_SUBITEMS = 4096;

		// Token: 0x0400131F RID: 4895
		private static readonly BitVector32.Section StateSelectedSection = BitVector32.CreateSection(1);

		// Token: 0x04001320 RID: 4896
		private static readonly BitVector32.Section StateImageMaskSet = BitVector32.CreateSection(1, ListViewItem.StateSelectedSection);

		// Token: 0x04001321 RID: 4897
		private static readonly BitVector32.Section StateWholeRowOneStyleSection = BitVector32.CreateSection(1, ListViewItem.StateImageMaskSet);

		// Token: 0x04001322 RID: 4898
		private static readonly BitVector32.Section SavedStateImageIndexSection = BitVector32.CreateSection(15, ListViewItem.StateWholeRowOneStyleSection);

		// Token: 0x04001323 RID: 4899
		private static readonly BitVector32.Section SubItemCountSection = BitVector32.CreateSection(4096, ListViewItem.SavedStateImageIndexSection);

		// Token: 0x04001324 RID: 4900
		private int indentCount;

		// Token: 0x04001325 RID: 4901
		private Point position = new Point(-1, -1);

		// Token: 0x04001326 RID: 4902
		internal ListView listView;

		// Token: 0x04001327 RID: 4903
		internal ListViewGroup group;

		// Token: 0x04001328 RID: 4904
		private string groupName;

		// Token: 0x04001329 RID: 4905
		private ListViewItem.ListViewSubItemCollection listViewSubItemCollection;

		// Token: 0x0400132A RID: 4906
		private ListViewItem.ListViewSubItem[] subItems;

		// Token: 0x0400132B RID: 4907
		private int lastIndex = -1;

		// Token: 0x0400132C RID: 4908
		internal int ID = -1;

		// Token: 0x0400132D RID: 4909
		private BitVector32 state;

		// Token: 0x0400132E RID: 4910
		private ListViewItem.ListViewItemImageIndexer imageIndexer;

		// Token: 0x0400132F RID: 4911
		private string toolTipText = string.Empty;

		// Token: 0x04001330 RID: 4912
		private object userData;

		// Token: 0x020006CD RID: 1741
		internal class ListViewItemImageIndexer : ImageList.Indexer
		{
			// Token: 0x06006A6C RID: 27244 RVA: 0x0018A161 File Offset: 0x00188361
			public ListViewItemImageIndexer(ListViewItem item)
			{
				this.owner = item;
			}

			// Token: 0x17001715 RID: 5909
			// (get) Token: 0x06006A6D RID: 27245 RVA: 0x0018A170 File Offset: 0x00188370
			// (set) Token: 0x06006A6E RID: 27246 RVA: 0x000070A6 File Offset: 0x000052A6
			public override ImageList ImageList
			{
				get
				{
					if (this.owner != null)
					{
						return this.owner.ImageList;
					}
					return null;
				}
				set
				{
				}
			}

			// Token: 0x04003B3B RID: 15163
			private ListViewItem owner;
		}

		/// <summary>Represents a subitem of a <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
		// Token: 0x020006CE RID: 1742
		[TypeConverter(typeof(ListViewSubItemConverter))]
		[ToolboxItem(false)]
		[DesignTimeVisible(false)]
		[DefaultProperty("Text")]
		[Serializable]
		public class ListViewSubItem
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> class with default values.</summary>
			// Token: 0x06006A6F RID: 27247 RVA: 0x00002843 File Offset: 0x00000A43
			public ListViewSubItem()
			{
			}

			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> class with the specified owner and text.</summary>
			/// <param name="owner">A <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the item that owns the subitem.</param>
			/// <param name="text">The text to display for the subitem.</param>
			// Token: 0x06006A70 RID: 27248 RVA: 0x0018A187 File Offset: 0x00188387
			public ListViewSubItem(ListViewItem owner, string text)
			{
				this.owner = owner;
				this.text = text;
			}

			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> class with the specified owner, text, foreground color, background color, and font values.</summary>
			/// <param name="owner">A <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the item that owns the subitem.</param>
			/// <param name="text">The text to display for the subitem.</param>
			/// <param name="foreColor">A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the subitem.</param>
			/// <param name="backColor">A <see cref="T:System.Drawing.Color" /> that represents the background color of the subitem.</param>
			/// <param name="font">A <see cref="T:System.Drawing.Font" /> that represents the font to display the subitem's text in.</param>
			// Token: 0x06006A71 RID: 27249 RVA: 0x0018A1A0 File Offset: 0x001883A0
			public ListViewSubItem(ListViewItem owner, string text, Color foreColor, Color backColor, Font font)
			{
				this.owner = owner;
				this.text = text;
				this.style = new ListViewItem.ListViewSubItem.SubItemStyle();
				this.style.foreColor = foreColor;
				this.style.backColor = backColor;
				this.style.font = font;
			}

			/// <summary>Gets or sets the background color of the subitem's text.</summary>
			/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the subitem's text.</returns>
			// Token: 0x17001716 RID: 5910
			// (get) Token: 0x06006A72 RID: 27250 RVA: 0x0018A1F4 File Offset: 0x001883F4
			// (set) Token: 0x06006A73 RID: 27251 RVA: 0x0018A258 File Offset: 0x00188458
			public Color BackColor
			{
				get
				{
					if (this.style != null && this.style.backColor != Color.Empty)
					{
						return this.style.backColor;
					}
					if (this.owner != null && this.owner.listView != null)
					{
						return this.owner.listView.BackColor;
					}
					return SystemColors.Window;
				}
				set
				{
					if (this.style == null)
					{
						this.style = new ListViewItem.ListViewSubItem.SubItemStyle();
					}
					if (this.style.backColor != value)
					{
						this.style.backColor = value;
						if (this.owner != null)
						{
							this.owner.InvalidateListView();
						}
					}
				}
			}

			/// <summary>Gets the bounding rectangle of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />.</summary>
			/// <returns>The bounding <see cref="T:System.Drawing.Rectangle" /> of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />.</returns>
			// Token: 0x17001717 RID: 5911
			// (get) Token: 0x06006A74 RID: 27252 RVA: 0x0018A2AC File Offset: 0x001884AC
			[Browsable(false)]
			public Rectangle Bounds
			{
				get
				{
					if (this.owner != null && this.owner.listView != null && this.owner.listView.IsHandleCreated)
					{
						return this.owner.listView.GetSubItemRect(this.owner.Index, this.owner.SubItems.IndexOf(this));
					}
					return Rectangle.Empty;
				}
			}

			// Token: 0x17001718 RID: 5912
			// (get) Token: 0x06006A75 RID: 27253 RVA: 0x0018A312 File Offset: 0x00188512
			internal bool CustomBackColor
			{
				get
				{
					return this.style != null && !this.style.backColor.IsEmpty;
				}
			}

			// Token: 0x17001719 RID: 5913
			// (get) Token: 0x06006A76 RID: 27254 RVA: 0x0018A331 File Offset: 0x00188531
			internal bool CustomFont
			{
				get
				{
					return this.style != null && this.style.font != null;
				}
			}

			// Token: 0x1700171A RID: 5914
			// (get) Token: 0x06006A77 RID: 27255 RVA: 0x0018A34B File Offset: 0x0018854B
			internal bool CustomForeColor
			{
				get
				{
					return this.style != null && !this.style.foreColor.IsEmpty;
				}
			}

			// Token: 0x1700171B RID: 5915
			// (get) Token: 0x06006A78 RID: 27256 RVA: 0x0018A36A File Offset: 0x0018856A
			internal bool CustomStyle
			{
				get
				{
					return this.style != null;
				}
			}

			/// <summary>Gets or sets the font of the text displayed by the subitem.</summary>
			/// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the control.</returns>
			// Token: 0x1700171C RID: 5916
			// (get) Token: 0x06006A79 RID: 27257 RVA: 0x0018A378 File Offset: 0x00188578
			// (set) Token: 0x06006A7A RID: 27258 RVA: 0x0018A3D4 File Offset: 0x001885D4
			[Localizable(true)]
			public Font Font
			{
				get
				{
					if (this.style != null && this.style.font != null)
					{
						return this.style.font;
					}
					if (this.owner != null && this.owner.listView != null)
					{
						return this.owner.listView.Font;
					}
					return Control.DefaultFont;
				}
				set
				{
					if (this.style == null)
					{
						this.style = new ListViewItem.ListViewSubItem.SubItemStyle();
					}
					if (this.style.font != value)
					{
						this.style.font = value;
						if (this.owner != null)
						{
							this.owner.InvalidateListView();
						}
					}
				}
			}

			/// <summary>Gets or sets the foreground color of the subitem's text.</summary>
			/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the subitem's text.</returns>
			// Token: 0x1700171D RID: 5917
			// (get) Token: 0x06006A7B RID: 27259 RVA: 0x0018A424 File Offset: 0x00188624
			// (set) Token: 0x06006A7C RID: 27260 RVA: 0x0018A488 File Offset: 0x00188688
			public Color ForeColor
			{
				get
				{
					if (this.style != null && this.style.foreColor != Color.Empty)
					{
						return this.style.foreColor;
					}
					if (this.owner != null && this.owner.listView != null)
					{
						return this.owner.listView.ForeColor;
					}
					return SystemColors.WindowText;
				}
				set
				{
					if (this.style == null)
					{
						this.style = new ListViewItem.ListViewSubItem.SubItemStyle();
					}
					if (this.style.foreColor != value)
					{
						this.style.foreColor = value;
						if (this.owner != null)
						{
							this.owner.InvalidateListView();
						}
					}
				}
			}

			/// <summary>Gets or sets an object that contains data about the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />.</summary>
			/// <returns>An <see cref="T:System.Object" /> that contains data about the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />. The default is <see langword="null" />.</returns>
			// Token: 0x1700171E RID: 5918
			// (get) Token: 0x06006A7D RID: 27261 RVA: 0x0018A4DA File Offset: 0x001886DA
			// (set) Token: 0x06006A7E RID: 27262 RVA: 0x0018A4E2 File Offset: 0x001886E2
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

			/// <summary>Gets or sets the text of the subitem.</summary>
			/// <returns>The text to display for the subitem.</returns>
			// Token: 0x1700171F RID: 5919
			// (get) Token: 0x06006A7F RID: 27263 RVA: 0x0018A4EB File Offset: 0x001886EB
			// (set) Token: 0x06006A80 RID: 27264 RVA: 0x0018A501 File Offset: 0x00188701
			[Localizable(true)]
			public string Text
			{
				get
				{
					if (this.text != null)
					{
						return this.text;
					}
					return "";
				}
				set
				{
					this.text = value;
					if (this.owner != null)
					{
						this.owner.UpdateSubItems(-1);
					}
				}
			}

			/// <summary>Gets or sets the name of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />.</summary>
			/// <returns>The name of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />, or an empty string ("") if a name has not been set.</returns>
			// Token: 0x17001720 RID: 5920
			// (get) Token: 0x06006A81 RID: 27265 RVA: 0x0018A51E File Offset: 0x0018871E
			// (set) Token: 0x06006A82 RID: 27266 RVA: 0x0018A534 File Offset: 0x00188734
			[Localizable(true)]
			public string Name
			{
				get
				{
					if (this.name != null)
					{
						return this.name;
					}
					return "";
				}
				set
				{
					this.name = value;
					if (this.owner != null)
					{
						this.owner.UpdateSubItems(-1);
					}
				}
			}

			// Token: 0x06006A83 RID: 27267 RVA: 0x000070A6 File Offset: 0x000052A6
			[OnDeserializing]
			private void OnDeserializing(StreamingContext ctx)
			{
			}

			// Token: 0x06006A84 RID: 27268 RVA: 0x0018A551 File Offset: 0x00188751
			[OnDeserialized]
			private void OnDeserialized(StreamingContext ctx)
			{
				this.name = null;
				this.userData = null;
			}

			// Token: 0x06006A85 RID: 27269 RVA: 0x000070A6 File Offset: 0x000052A6
			[OnSerializing]
			private void OnSerializing(StreamingContext ctx)
			{
			}

			// Token: 0x06006A86 RID: 27270 RVA: 0x000070A6 File Offset: 0x000052A6
			[OnSerialized]
			private void OnSerialized(StreamingContext ctx)
			{
			}

			/// <summary>Resets the styles applied to the subitem to the default font and colors.</summary>
			// Token: 0x06006A87 RID: 27271 RVA: 0x0018A561 File Offset: 0x00188761
			public void ResetStyle()
			{
				if (this.style != null)
				{
					this.style = null;
					if (this.owner != null)
					{
						this.owner.InvalidateListView();
					}
				}
			}

			/// <summary>Returns a string that represents the current object.</summary>
			/// <returns>A string that represents the current object.</returns>
			// Token: 0x06006A88 RID: 27272 RVA: 0x0018A585 File Offset: 0x00188785
			public override string ToString()
			{
				return "ListViewSubItem: {" + this.Text + "}";
			}

			// Token: 0x04003B3C RID: 15164
			[NonSerialized]
			internal ListViewItem owner;

			// Token: 0x04003B3D RID: 15165
			private string text;

			// Token: 0x04003B3E RID: 15166
			[OptionalField(VersionAdded = 2)]
			private string name;

			// Token: 0x04003B3F RID: 15167
			private ListViewItem.ListViewSubItem.SubItemStyle style;

			// Token: 0x04003B40 RID: 15168
			[OptionalField(VersionAdded = 2)]
			private object userData;

			// Token: 0x020008C3 RID: 2243
			[Serializable]
			private class SubItemStyle
			{
				// Token: 0x0400453F RID: 17727
				public Color backColor = Color.Empty;

				// Token: 0x04004540 RID: 17728
				public Color foreColor = Color.Empty;

				// Token: 0x04004541 RID: 17729
				public Font font;
			}
		}

		/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> objects stored in a <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
		// Token: 0x020006CF RID: 1743
		public class ListViewSubItemCollection : IList, ICollection, IEnumerable
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItemCollection" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.ListViewItem" /> that owns the collection.</param>
			// Token: 0x06006A89 RID: 27273 RVA: 0x0018A59C File Offset: 0x0018879C
			public ListViewSubItemCollection(ListViewItem owner)
			{
				this.owner = owner;
			}

			/// <summary>Gets the number of subitems in the collection.</summary>
			/// <returns>The number of subitems in the collection.</returns>
			// Token: 0x17001721 RID: 5921
			// (get) Token: 0x06006A8A RID: 27274 RVA: 0x0018A5B2 File Offset: 0x001887B2
			[Browsable(false)]
			public int Count
			{
				get
				{
					return this.owner.SubItemCount;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
			/// <returns>The object used to synchronize the collection.</returns>
			// Token: 0x17001722 RID: 5922
			// (get) Token: 0x06006A8B RID: 27275 RVA: 0x00006A49 File Offset: 0x00004C49
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
			// Token: 0x17001723 RID: 5923
			// (get) Token: 0x06006A8C RID: 27276 RVA: 0x00012E4E File Offset: 0x0001104E
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
			// Token: 0x17001724 RID: 5924
			// (get) Token: 0x06006A8D RID: 27277 RVA: 0x0001180C File Offset: 0x0000FA0C
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
			// Token: 0x17001725 RID: 5925
			// (get) Token: 0x06006A8E RID: 27278 RVA: 0x0001180C File Offset: 0x0000FA0C
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets or sets the subitem at the specified index within the collection.</summary>
			/// <param name="index">The index of the item in the collection to retrieve.</param>
			/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> representing the subitem located at the specified index within the collection.</returns>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListViewItem.ListViewSubItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItemCollection" />.</exception>
			// Token: 0x17001726 RID: 5926
			public ListViewItem.ListViewSubItem this[int index]
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
					return this.owner.subItems[index];
				}
				set
				{
					if (index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
					this.owner.subItems[index] = value;
					this.owner.UpdateSubItems(index);
				}
			}

			/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> at the specified index within the collection.</summary>
			/// <param name="index">The zero-based index of the element to get or set.</param>
			/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> that represents the item located at the specified index within the collection.</returns>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The index parameter is less than 0 or greater than or equal to the value of the Count property of the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" />.</exception>
			/// <exception cref="T:System.ArgumentException">The object is not a <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />.</exception>
			// Token: 0x17001727 RID: 5927
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					if (value is ListViewItem.ListViewSubItem)
					{
						this[index] = (ListViewItem.ListViewSubItem)value;
						return;
					}
					throw new ArgumentException(SR.GetString("ListViewBadListViewSubItem"), "value");
				}
			}

			/// <summary>Gets an item with the specified key from the collection.</summary>
			/// <param name="key">The name of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to retrieve.</param>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> with the specified key.</returns>
			// Token: 0x17001728 RID: 5928
			public virtual ListViewItem.ListViewSubItem this[string key]
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

			/// <summary>Adds an existing <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to the collection.</summary>
			/// <param name="item">The <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to add to the collection.</param>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> that was added to the collection.</returns>
			// Token: 0x06006A94 RID: 27284 RVA: 0x0018A6EC File Offset: 0x001888EC
			public ListViewItem.ListViewSubItem Add(ListViewItem.ListViewSubItem item)
			{
				this.EnsureSubItemSpace(1, -1);
				item.owner = this.owner;
				this.owner.subItems[this.owner.SubItemCount] = item;
				ListViewItem listViewItem = this.owner;
				ListViewItem listViewItem2 = this.owner;
				int subItemCount = listViewItem2.SubItemCount;
				listViewItem2.SubItemCount = subItemCount + 1;
				listViewItem.UpdateSubItems(subItemCount);
				return item;
			}

			/// <summary>Adds a subitem to the collection with specified text.</summary>
			/// <param name="text">The text to display for the subitem.</param>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> that was added to the collection.</returns>
			// Token: 0x06006A95 RID: 27285 RVA: 0x0018A748 File Offset: 0x00188948
			public ListViewItem.ListViewSubItem Add(string text)
			{
				ListViewItem.ListViewSubItem listViewSubItem = new ListViewItem.ListViewSubItem(this.owner, text);
				this.Add(listViewSubItem);
				return listViewSubItem;
			}

			/// <summary>Adds a subitem to the collection with specified text, foreground color, background color, and font settings.</summary>
			/// <param name="text">The text to display for the subitem.</param>
			/// <param name="foreColor">A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the subitem.</param>
			/// <param name="backColor">A <see cref="T:System.Drawing.Color" /> that represents the background color of the subitem.</param>
			/// <param name="font">A <see cref="T:System.Drawing.Font" /> that represents the typeface to display the subitem's text in.</param>
			/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> that was added to the collection.</returns>
			// Token: 0x06006A96 RID: 27286 RVA: 0x0018A76C File Offset: 0x0018896C
			public ListViewItem.ListViewSubItem Add(string text, Color foreColor, Color backColor, Font font)
			{
				ListViewItem.ListViewSubItem listViewSubItem = new ListViewItem.ListViewSubItem(this.owner, text, foreColor, backColor, font);
				this.Add(listViewSubItem);
				return listViewSubItem;
			}

			/// <summary>Adds an array of <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> objects to the collection.</summary>
			/// <param name="items">An array of <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> objects to add to the collection.</param>
			// Token: 0x06006A97 RID: 27287 RVA: 0x0018A794 File Offset: 0x00188994
			public void AddRange(ListViewItem.ListViewSubItem[] items)
			{
				if (items == null)
				{
					throw new ArgumentNullException("items");
				}
				this.EnsureSubItemSpace(items.Length, -1);
				foreach (ListViewItem.ListViewSubItem listViewSubItem in items)
				{
					if (listViewSubItem != null)
					{
						ListViewItem.ListViewSubItem[] subItems = this.owner.subItems;
						ListViewItem listViewItem = this.owner;
						int subItemCount = listViewItem.SubItemCount;
						listViewItem.SubItemCount = subItemCount + 1;
						subItems[subItemCount] = listViewSubItem;
					}
				}
				this.owner.UpdateSubItems(-1);
			}

			/// <summary>Creates new subitems based on an array and adds them to the collection.</summary>
			/// <param name="items">An array of strings representing the text of each subitem to add to the collection.</param>
			// Token: 0x06006A98 RID: 27288 RVA: 0x0018A800 File Offset: 0x00188A00
			public void AddRange(string[] items)
			{
				if (items == null)
				{
					throw new ArgumentNullException("items");
				}
				this.EnsureSubItemSpace(items.Length, -1);
				foreach (string text in items)
				{
					if (text != null)
					{
						ListViewItem.ListViewSubItem[] subItems = this.owner.subItems;
						ListViewItem listViewItem = this.owner;
						int subItemCount = listViewItem.SubItemCount;
						listViewItem.SubItemCount = subItemCount + 1;
						subItems[subItemCount] = new ListViewItem.ListViewSubItem(this.owner, text);
					}
				}
				this.owner.UpdateSubItems(-1);
			}

			/// <summary>Creates new subitems based on an array and adds them to the collection with specified foreground color, background color, and font.</summary>
			/// <param name="items">An array of strings representing the text of each subitem to add to the collection.</param>
			/// <param name="foreColor">A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the subitem.</param>
			/// <param name="backColor">A <see cref="T:System.Drawing.Color" /> that represents the background color of the subitem.</param>
			/// <param name="font">A <see cref="T:System.Drawing.Font" /> that represents the typeface to display the subitem's text in.</param>
			// Token: 0x06006A99 RID: 27289 RVA: 0x0018A878 File Offset: 0x00188A78
			public void AddRange(string[] items, Color foreColor, Color backColor, Font font)
			{
				if (items == null)
				{
					throw new ArgumentNullException("items");
				}
				this.EnsureSubItemSpace(items.Length, -1);
				foreach (string text in items)
				{
					if (text != null)
					{
						ListViewItem.ListViewSubItem[] subItems = this.owner.subItems;
						ListViewItem listViewItem = this.owner;
						int subItemCount = listViewItem.SubItemCount;
						listViewItem.SubItemCount = subItemCount + 1;
						subItems[subItemCount] = new ListViewItem.ListViewSubItem(this.owner, text, foreColor, backColor, font);
					}
				}
				this.owner.UpdateSubItems(-1);
			}

			/// <summary>Adds an existing <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to the collection.</summary>
			/// <param name="item">The <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to add to the collection.</param>
			/// <returns>The zero-based index that indicates the location of the object that was added to the collection.</returns>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="item" /> is not a <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />.</exception>
			// Token: 0x06006A9A RID: 27290 RVA: 0x0018A8F2 File Offset: 0x00188AF2
			int IList.Add(object item)
			{
				if (item is ListViewItem.ListViewSubItem)
				{
					return this.IndexOf(this.Add((ListViewItem.ListViewSubItem)item));
				}
				throw new ArgumentException(SR.GetString("ListViewSubItemCollectionInvalidArgument"));
			}

			/// <summary>Removes all subitems and the parent <see cref="T:System.Windows.Forms.ListViewItem" /> from the collection.</summary>
			// Token: 0x06006A9B RID: 27291 RVA: 0x0018A920 File Offset: 0x00188B20
			public void Clear()
			{
				int subItemCount = this.owner.SubItemCount;
				if (subItemCount > 0)
				{
					this.owner.SubItemCount = 0;
					this.owner.UpdateSubItems(-1, subItemCount);
				}
			}

			/// <summary>Determines whether the specified subitem is located in the collection.</summary>
			/// <param name="subItem">A <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> representing the subitem to locate in the collection.</param>
			/// <returns>
			///   <see langword="true" /> if the subitem is contained in the collection; otherwise, <see langword="false" />.</returns>
			// Token: 0x06006A9C RID: 27292 RVA: 0x0018A956 File Offset: 0x00188B56
			public bool Contains(ListViewItem.ListViewSubItem subItem)
			{
				return this.IndexOf(subItem) != -1;
			}

			/// <summary>Determines whether the specified subitem is located in the collection.</summary>
			/// <param name="subItem">An object that represents the subitem to locate in the collection.</param>
			/// <returns>
			///   <see langword="true" /> if the subitem is contained in the collection; otherwise, <see langword="false" />.</returns>
			// Token: 0x06006A9D RID: 27293 RVA: 0x0018A965 File Offset: 0x00188B65
			bool IList.Contains(object subItem)
			{
				return subItem is ListViewItem.ListViewSubItem && this.Contains((ListViewItem.ListViewSubItem)subItem);
			}

			/// <summary>Determines if the collection contains an item with the specified key.</summary>
			/// <param name="key">The name of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to look for.</param>
			/// <returns>
			///   <see langword="true" /> to indicate the collection contains an item with the specified key; otherwise, <see langword="false" />.</returns>
			// Token: 0x06006A9E RID: 27294 RVA: 0x0018A97D File Offset: 0x00188B7D
			public virtual bool ContainsKey(string key)
			{
				return this.IsValidIndex(this.IndexOfKey(key));
			}

			// Token: 0x06006A9F RID: 27295 RVA: 0x0018A98C File Offset: 0x00188B8C
			private void EnsureSubItemSpace(int size, int index)
			{
				if (this.owner.SubItemCount == 4096)
				{
					throw new InvalidOperationException(SR.GetString("ErrorCollectionFull"));
				}
				if (this.owner.SubItemCount + size <= this.owner.subItems.Length)
				{
					if (index != -1)
					{
						for (int i = this.owner.SubItemCount - 1; i >= index; i--)
						{
							this.owner.subItems[i + size] = this.owner.subItems[i];
						}
					}
					return;
				}
				if (this.owner.subItems == null)
				{
					int num = ((size > 4) ? size : 4);
					this.owner.subItems = new ListViewItem.ListViewSubItem[num];
					return;
				}
				int num2 = this.owner.subItems.Length * 2;
				while (num2 - this.owner.SubItemCount < size)
				{
					num2 *= 2;
				}
				ListViewItem.ListViewSubItem[] array = new ListViewItem.ListViewSubItem[num2];
				if (index != -1)
				{
					Array.Copy(this.owner.subItems, 0, array, 0, index);
					Array.Copy(this.owner.subItems, index, array, index + size, this.owner.SubItemCount - index);
				}
				else
				{
					Array.Copy(this.owner.subItems, array, this.owner.SubItemCount);
				}
				this.owner.subItems = array;
			}

			/// <summary>Returns the index within the collection of the specified subitem.</summary>
			/// <param name="subItem">A <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> representing the subitem to locate in the collection.</param>
			/// <returns>The zero-based index of the subitem's location in the collection. If the subitem is not located in the collection, the return value is negative one (-1).</returns>
			// Token: 0x06006AA0 RID: 27296 RVA: 0x0018AACC File Offset: 0x00188CCC
			public int IndexOf(ListViewItem.ListViewSubItem subItem)
			{
				for (int i = 0; i < this.Count; i++)
				{
					if (this.owner.subItems[i] == subItem)
					{
						return i;
					}
				}
				return -1;
			}

			/// <summary>Returns the index within the collection of the specified subitem.</summary>
			/// <param name="subItem">An object that represents the subitem to locate in the collection.</param>
			/// <returns>The zero-based index of the subitem if it is in the collection; otherwise, -1.</returns>
			// Token: 0x06006AA1 RID: 27297 RVA: 0x0018AAFD File Offset: 0x00188CFD
			int IList.IndexOf(object subItem)
			{
				if (subItem is ListViewItem.ListViewSubItem)
				{
					return this.IndexOf((ListViewItem.ListViewSubItem)subItem);
				}
				return -1;
			}

			/// <summary>Returns the index of the first occurrence of an item with the specified key within the collection.</summary>
			/// <param name="key">The name of the item to retrieve the index for.</param>
			/// <returns>The zero-based index of the first occurrence of an item with the specified key.</returns>
			// Token: 0x06006AA2 RID: 27298 RVA: 0x0018AB18 File Offset: 0x00188D18
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

			// Token: 0x06006AA3 RID: 27299 RVA: 0x0018AB95 File Offset: 0x00188D95
			private bool IsValidIndex(int index)
			{
				return index >= 0 && index < this.Count;
			}

			/// <summary>Inserts a subitem into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the item is inserted.</param>
			/// <param name="item">A <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> representing the subitem to insert into the collection.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than the value of the <see cref="P:System.Windows.Forms.ListViewItem.ListViewSubItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItemCollection" />.</exception>
			// Token: 0x06006AA4 RID: 27300 RVA: 0x0018ABA8 File Offset: 0x00188DA8
			public void Insert(int index, ListViewItem.ListViewSubItem item)
			{
				if (index < 0 || index > this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				item.owner = this.owner;
				this.EnsureSubItemSpace(1, index);
				this.owner.subItems[index] = item;
				ListViewItem listViewItem = this.owner;
				int subItemCount = listViewItem.SubItemCount;
				listViewItem.SubItemCount = subItemCount + 1;
				this.owner.UpdateSubItems(-1);
			}

			/// <summary>Inserts a subitem into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the item is inserted.</param>
			/// <param name="item">An object that represents the subitem to insert into the collection.</param>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="item" /> is not a <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The index parameter is less than 0 or greater than or equal to the value of the Count property of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItemCollection" />.</exception>
			// Token: 0x06006AA5 RID: 27301 RVA: 0x0018AC10 File Offset: 0x00188E10
			void IList.Insert(int index, object item)
			{
				if (item is ListViewItem.ListViewSubItem)
				{
					this.Insert(index, (ListViewItem.ListViewSubItem)item);
					return;
				}
				throw new ArgumentException(SR.GetString("ListViewBadListViewSubItem"), "item");
			}

			/// <summary>Removes a specified item from the collection.</summary>
			/// <param name="item">The item to remove from the collection.</param>
			// Token: 0x06006AA6 RID: 27302 RVA: 0x0018AC3C File Offset: 0x00188E3C
			public void Remove(ListViewItem.ListViewSubItem item)
			{
				int num = this.IndexOf(item);
				if (num != -1)
				{
					this.RemoveAt(num);
				}
			}

			/// <summary>Removes a specified item from the collection.</summary>
			/// <param name="item">The item to remove from the collection.</param>
			// Token: 0x06006AA7 RID: 27303 RVA: 0x0018AC5C File Offset: 0x00188E5C
			void IList.Remove(object item)
			{
				if (item is ListViewItem.ListViewSubItem)
				{
					this.Remove((ListViewItem.ListViewSubItem)item);
				}
			}

			/// <summary>Removes the subitem at the specified index within the collection.</summary>
			/// <param name="index">The zero-based index of the subitem to remove.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListViewItem.ListViewSubItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItemCollection" />.</exception>
			// Token: 0x06006AA8 RID: 27304 RVA: 0x0018AC74 File Offset: 0x00188E74
			public void RemoveAt(int index)
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				for (int i = index + 1; i < this.owner.SubItemCount; i++)
				{
					this.owner.subItems[i - 1] = this.owner.subItems[i];
				}
				int subItemCount = this.owner.SubItemCount;
				ListViewItem listViewItem = this.owner;
				int subItemCount2 = listViewItem.SubItemCount;
				listViewItem.SubItemCount = subItemCount2 - 1;
				this.owner.subItems[this.owner.SubItemCount] = null;
				this.owner.UpdateSubItems(-1, subItemCount);
			}

			/// <summary>Removes an item with the specified key from the collection.</summary>
			/// <param name="key">The name of the item to remove from the collection.</param>
			// Token: 0x06006AA9 RID: 27305 RVA: 0x0018AD14 File Offset: 0x00188F14
			public virtual void RemoveByKey(string key)
			{
				int num = this.IndexOfKey(key);
				if (this.IsValidIndex(num))
				{
					this.RemoveAt(num);
				}
			}

			/// <summary>Copies the item and collection of subitems into an array.</summary>
			/// <param name="dest">An array of <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />.</param>
			/// <param name="index">The zero-based index in array at which copying begins.</param>
			/// <exception cref="T:System.ArrayTypeMismatchException">The array type is not compatible with <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />.</exception>
			// Token: 0x06006AAA RID: 27306 RVA: 0x0018AD39 File Offset: 0x00188F39
			void ICollection.CopyTo(Array dest, int index)
			{
				if (this.Count > 0)
				{
					Array.Copy(this.owner.subItems, 0, dest, index, this.Count);
				}
			}

			/// <summary>Returns an enumerator to use to iterate through the subitem collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the subitem collection.</returns>
			// Token: 0x06006AAB RID: 27307 RVA: 0x0018AD60 File Offset: 0x00188F60
			public IEnumerator GetEnumerator()
			{
				if (this.owner.subItems != null)
				{
					object[] subItems = this.owner.subItems;
					return new WindowsFormsUtils.ArraySubsetEnumerator(subItems, this.owner.SubItemCount);
				}
				return new ListViewItem.ListViewSubItem[0].GetEnumerator();
			}

			// Token: 0x04003B41 RID: 15169
			private ListViewItem owner;

			// Token: 0x04003B42 RID: 15170
			private int lastAccessedIndex = -1;
		}
	}
}
