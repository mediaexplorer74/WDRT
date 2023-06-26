using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	/// <summary>Represents a group of items displayed within a <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
	// Token: 0x020002D4 RID: 724
	[TypeConverter(typeof(ListViewGroupConverter))]
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	[DefaultProperty("Header")]
	[Serializable]
	public sealed class ListViewGroup : ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewGroup" /> class using the default header text of "ListViewGroup" and the default left header alignment.</summary>
		// Token: 0x06002DD4 RID: 11732 RVA: 0x000D093D File Offset: 0x000CEB3D
		public ListViewGroup()
			: this(SR.GetString("ListViewGroupDefaultHeader", new object[] { ListViewGroup.nextHeader++ }))
		{
		}

		// Token: 0x06002DD5 RID: 11733 RVA: 0x000D096A File Offset: 0x000CEB6A
		private ListViewGroup(SerializationInfo info, StreamingContext context)
			: this()
		{
			this.Deserialize(info, context);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewGroup" /> class using the specified values to initialize the <see cref="P:System.Windows.Forms.ListViewGroup.Name" /> and <see cref="P:System.Windows.Forms.ListViewGroup.Header" /> properties.</summary>
		/// <param name="key">The initial value of the <see cref="P:System.Windows.Forms.ListViewGroup.Name" /> property.</param>
		/// <param name="headerText">The initial value of the <see cref="P:System.Windows.Forms.ListViewGroup.Header" /> property.</param>
		// Token: 0x06002DD6 RID: 11734 RVA: 0x000D097A File Offset: 0x000CEB7A
		public ListViewGroup(string key, string headerText)
			: this()
		{
			this.name = key;
			this.header = headerText;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewGroup" /> class using the specified value to initialize the <see cref="P:System.Windows.Forms.ListViewGroup.Header" /> property and using the default left header alignment.</summary>
		/// <param name="header">The text to display for the group header.</param>
		// Token: 0x06002DD7 RID: 11735 RVA: 0x000D0990 File Offset: 0x000CEB90
		public ListViewGroup(string header)
		{
			this.header = header;
			this.id = ListViewGroup.nextID++;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewGroup" /> class using the specified header text and the specified header alignment.</summary>
		/// <param name="header">The text to display for the group header.</param>
		/// <param name="headerAlignment">One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values that specifies the alignment of the header text.</param>
		// Token: 0x06002DD8 RID: 11736 RVA: 0x000D09B2 File Offset: 0x000CEBB2
		public ListViewGroup(string header, HorizontalAlignment headerAlignment)
			: this(header)
		{
			this.headerAlignment = headerAlignment;
		}

		/// <summary>Gets or sets the header text for the group.</summary>
		/// <returns>The text to display for the group header. The default is "ListViewGroup".</returns>
		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x06002DD9 RID: 11737 RVA: 0x000D09C2 File Offset: 0x000CEBC2
		// (set) Token: 0x06002DDA RID: 11738 RVA: 0x000D09D8 File Offset: 0x000CEBD8
		[SRCategory("CatAppearance")]
		public string Header
		{
			get
			{
				if (this.header != null)
				{
					return this.header;
				}
				return "";
			}
			set
			{
				if (this.header != value)
				{
					this.header = value;
					if (this.listView != null)
					{
						this.listView.RecreateHandleInternal();
					}
				}
			}
		}

		/// <summary>Gets or sets the alignment of the group header text.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values that specifies the alignment of the header text. The default is <see cref="F:System.Windows.Forms.HorizontalAlignment.Left" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.HorizontalAlignment" /> value.</exception>
		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06002DDB RID: 11739 RVA: 0x000D0A02 File Offset: 0x000CEC02
		// (set) Token: 0x06002DDC RID: 11740 RVA: 0x000D0A0A File Offset: 0x000CEC0A
		[DefaultValue(HorizontalAlignment.Left)]
		[SRCategory("CatAppearance")]
		public HorizontalAlignment HeaderAlignment
		{
			get
			{
				return this.headerAlignment;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(HorizontalAlignment));
				}
				if (this.headerAlignment != value)
				{
					this.headerAlignment = value;
					this.UpdateListView();
				}
			}
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x06002DDD RID: 11741 RVA: 0x000D0A48 File Offset: 0x000CEC48
		internal int ID
		{
			get
			{
				return this.id;
			}
		}

		/// <summary>Gets a collection containing all items associated with this group.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> that contains all the items in the group. If there are no items in the group, an empty <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> object is returned.</returns>
		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x06002DDE RID: 11742 RVA: 0x000D0A50 File Offset: 0x000CEC50
		[Browsable(false)]
		public ListView.ListViewItemCollection Items
		{
			get
			{
				if (this.items == null)
				{
					this.items = new ListView.ListViewItemCollection(new ListViewGroupItemCollection(this));
				}
				return this.items;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ListView" /> control that contains this group.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ListView" /> control that contains this group.</returns>
		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x06002DDF RID: 11743 RVA: 0x000D0A71 File Offset: 0x000CEC71
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ListView ListView
		{
			get
			{
				return this.listView;
			}
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x06002DE0 RID: 11744 RVA: 0x000D0A71 File Offset: 0x000CEC71
		// (set) Token: 0x06002DE1 RID: 11745 RVA: 0x000D0A79 File Offset: 0x000CEC79
		internal ListView ListViewInternal
		{
			get
			{
				return this.listView;
			}
			set
			{
				if (this.listView != value)
				{
					this.listView = value;
				}
			}
		}

		/// <summary>Gets or sets the name of the group.</summary>
		/// <returns>The name of the group.</returns>
		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x06002DE2 RID: 11746 RVA: 0x000D0A8B File Offset: 0x000CEC8B
		// (set) Token: 0x06002DE3 RID: 11747 RVA: 0x000D0A93 File Offset: 0x000CEC93
		[SRCategory("CatBehavior")]
		[SRDescription("ListViewGroupNameDescr")]
		[Browsable(true)]
		[DefaultValue("")]
		public string Name
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

		/// <summary>Gets or sets the object that contains data about the group.</summary>
		/// <returns>An <see cref="T:System.Object" /> for storing the additional data.</returns>
		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x06002DE4 RID: 11748 RVA: 0x000D0A9C File Offset: 0x000CEC9C
		// (set) Token: 0x06002DE5 RID: 11749 RVA: 0x000D0AA4 File Offset: 0x000CECA4
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

		// Token: 0x06002DE6 RID: 11750 RVA: 0x000D0AB0 File Offset: 0x000CECB0
		private void Deserialize(SerializationInfo info, StreamingContext context)
		{
			int num = 0;
			foreach (SerializationEntry serializationEntry in info)
			{
				if (serializationEntry.Name == "Header")
				{
					this.Header = (string)serializationEntry.Value;
				}
				else if (serializationEntry.Name == "HeaderAlignment")
				{
					this.HeaderAlignment = (HorizontalAlignment)serializationEntry.Value;
				}
				else if (serializationEntry.Name == "Tag")
				{
					this.Tag = serializationEntry.Value;
				}
				else if (serializationEntry.Name == "ItemsCount")
				{
					num = (int)serializationEntry.Value;
				}
				else if (serializationEntry.Name == "Name")
				{
					this.Name = (string)serializationEntry.Value;
				}
			}
			if (num > 0)
			{
				ListViewItem[] array = new ListViewItem[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = (ListViewItem)info.GetValue("Item" + i.ToString(), typeof(ListViewItem));
				}
				this.Items.AddRange(array);
			}
		}

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		// Token: 0x06002DE7 RID: 11751 RVA: 0x000D0BE8 File Offset: 0x000CEDE8
		public override string ToString()
		{
			return this.Header;
		}

		// Token: 0x06002DE8 RID: 11752 RVA: 0x000D0BF0 File Offset: 0x000CEDF0
		private void UpdateListView()
		{
			if (this.listView != null && this.listView.IsHandleCreated)
			{
				this.listView.UpdateGroupNative(this);
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
		// Token: 0x06002DE9 RID: 11753 RVA: 0x000D0C14 File Offset: 0x000CEE14
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Header", this.Header);
			info.AddValue("HeaderAlignment", this.HeaderAlignment);
			info.AddValue("Tag", this.Tag);
			if (!string.IsNullOrEmpty(this.Name))
			{
				info.AddValue("Name", this.Name);
			}
			if (this.items != null && this.items.Count > 0)
			{
				info.AddValue("ItemsCount", this.Items.Count);
				for (int i = 0; i < this.Items.Count; i++)
				{
					info.AddValue("Item" + i.ToString(CultureInfo.InvariantCulture), this.Items[i], typeof(ListViewItem));
				}
			}
		}

		// Token: 0x04001301 RID: 4865
		private ListView listView;

		// Token: 0x04001302 RID: 4866
		private int id;

		// Token: 0x04001303 RID: 4867
		private string header;

		// Token: 0x04001304 RID: 4868
		private HorizontalAlignment headerAlignment;

		// Token: 0x04001305 RID: 4869
		private ListView.ListViewItemCollection items;

		// Token: 0x04001306 RID: 4870
		private static int nextID;

		// Token: 0x04001307 RID: 4871
		private static int nextHeader = 1;

		// Token: 0x04001308 RID: 4872
		private object userData;

		// Token: 0x04001309 RID: 4873
		private string name;
	}
}
