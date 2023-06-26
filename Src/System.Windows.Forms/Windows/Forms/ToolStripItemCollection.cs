using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.ToolStripItem" /> objects.</summary>
	// Token: 0x020003CF RID: 975
	[Editor("System.Windows.Forms.Design.ToolStripCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[ListBindable(false)]
	[UIPermission(SecurityAction.InheritanceDemand, Window = UIPermissionWindow.AllWindows)]
	public class ToolStripItemCollection : ArrangedElementCollection, IList, ICollection, IEnumerable
	{
		// Token: 0x0600430D RID: 17165 RVA: 0x0011C510 File Offset: 0x0011A710
		internal ToolStripItemCollection(ToolStrip owner, bool itemsCollection)
			: this(owner, itemsCollection, false)
		{
		}

		// Token: 0x0600430E RID: 17166 RVA: 0x0011C51B File Offset: 0x0011A71B
		internal ToolStripItemCollection(ToolStrip owner, bool itemsCollection, bool isReadOnly)
		{
			this.lastAccessedIndex = -1;
			base..ctor();
			this.owner = owner;
			this.itemsCollection = itemsCollection;
			this.isReadOnly = isReadOnly;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> class with the specified container <see cref="T:System.Windows.Forms.ToolStrip" /> and the specified array of <see cref="T:System.Windows.Forms.ToolStripItem" /> controls.</summary>
		/// <param name="owner">The <see cref="T:System.Windows.Forms.ToolStrip" /> to which this <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> belongs.</param>
		/// <param name="value">An array of type <see cref="T:System.Windows.Forms.ToolStripItem" /> containing the initial controls for this <see cref="T:System.Windows.Forms.ToolStripItemCollection" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="owner" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600430F RID: 17167 RVA: 0x0011C53F File Offset: 0x0011A73F
		public ToolStripItemCollection(ToolStrip owner, ToolStripItem[] value)
		{
			this.lastAccessedIndex = -1;
			base..ctor();
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			this.owner = owner;
			this.AddRange(value);
		}

		/// <summary>Gets the item at the specified index.</summary>
		/// <param name="index">The zero-based index of the item to get.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStripItem" /> located at the specified position in the <see cref="T:System.Windows.Forms.ToolStripItemCollection" />.</returns>
		// Token: 0x17001061 RID: 4193
		public virtual ToolStripItem this[int index]
		{
			get
			{
				return (ToolStripItem)base.InnerList[index];
			}
		}

		/// <summary>Gets the item with the specified name.</summary>
		/// <param name="key">The name of the item to get.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStripItem" /> with the specified name.</returns>
		// Token: 0x17001062 RID: 4194
		public virtual ToolStripItem this[string key]
		{
			get
			{
				if (key == null || key.Length == 0)
				{
					return null;
				}
				int num = this.IndexOfKey(key);
				if (this.IsValidIndex(num))
				{
					return (ToolStripItem)base.InnerList[num];
				}
				return null;
			}
		}

		/// <summary>Adds a <see cref="T:System.Windows.Forms.ToolStripItem" /> that displays the specified text to the collection.</summary>
		/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
		/// <returns>The new <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		// Token: 0x06004312 RID: 17170 RVA: 0x0011C5BE File Offset: 0x0011A7BE
		public ToolStripItem Add(string text)
		{
			return this.Add(text, null, null);
		}

		/// <summary>Adds a <see cref="T:System.Windows.Forms.ToolStripItem" /> that displays the specified image to the collection.</summary>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
		/// <returns>The new <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		// Token: 0x06004313 RID: 17171 RVA: 0x0011C5C9 File Offset: 0x0011A7C9
		public ToolStripItem Add(Image image)
		{
			return this.Add(null, image, null);
		}

		/// <summary>Adds a <see cref="T:System.Windows.Forms.ToolStripItem" /> that displays the specified image and text to the collection.</summary>
		/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
		/// <returns>The new <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		// Token: 0x06004314 RID: 17172 RVA: 0x0011C5D4 File Offset: 0x0011A7D4
		public ToolStripItem Add(string text, Image image)
		{
			return this.Add(text, image, null);
		}

		/// <summary>Adds a <see cref="T:System.Windows.Forms.ToolStripItem" /> that displays the specified image and text to the collection and that raises the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event.</summary>
		/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
		/// <param name="onClick">Raises the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event.</param>
		/// <returns>The new <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		// Token: 0x06004315 RID: 17173 RVA: 0x0011C5E0 File Offset: 0x0011A7E0
		public ToolStripItem Add(string text, Image image, EventHandler onClick)
		{
			ToolStripItem toolStripItem = this.owner.CreateDefaultItem(text, image, onClick);
			this.Add(toolStripItem);
			return toolStripItem;
		}

		/// <summary>Adds the specified item to the end of the collection.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripItem" /> to add to the end of the collection.</param>
		/// <returns>An <see cref="T:System.Int32" /> representing the zero-based index of the new item in the collection.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06004316 RID: 17174 RVA: 0x0011C608 File Offset: 0x0011A808
		public int Add(ToolStripItem value)
		{
			this.CheckCanAddOrInsertItem(value);
			this.SetOwner(value);
			int num = base.InnerList.Add(value);
			if (this.itemsCollection && this.owner != null)
			{
				this.owner.OnItemAddedInternal(value);
				this.owner.OnItemAdded(new ToolStripItemEventArgs(value));
			}
			return num;
		}

		/// <summary>Adds an array of <see cref="T:System.Windows.Forms.ToolStripItem" /> controls to the collection.</summary>
		/// <param name="toolStripItems">An array of <see cref="T:System.Windows.Forms.ToolStripItem" /> controls.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="toolStripItems" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> is read-only.</exception>
		// Token: 0x06004317 RID: 17175 RVA: 0x0011C660 File Offset: 0x0011A860
		public void AddRange(ToolStripItem[] toolStripItems)
		{
			if (toolStripItems == null)
			{
				throw new ArgumentNullException("toolStripItems");
			}
			if (this.IsReadOnly)
			{
				throw new NotSupportedException(SR.GetString("ToolStripItemCollectionIsReadOnly"));
			}
			using (new LayoutTransaction(this.owner, this.owner, PropertyNames.Items))
			{
				for (int i = 0; i < toolStripItems.Length; i++)
				{
					this.Add(toolStripItems[i]);
				}
			}
		}

		/// <summary>Adds a <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> to the current collection.</summary>
		/// <param name="toolStripItems">The <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> to be added to the current collection.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="toolStripItems" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> is read-only.</exception>
		// Token: 0x06004318 RID: 17176 RVA: 0x0011C6E0 File Offset: 0x0011A8E0
		public void AddRange(ToolStripItemCollection toolStripItems)
		{
			if (toolStripItems == null)
			{
				throw new ArgumentNullException("toolStripItems");
			}
			if (this.IsReadOnly)
			{
				throw new NotSupportedException(SR.GetString("ToolStripItemCollectionIsReadOnly"));
			}
			using (new LayoutTransaction(this.owner, this.owner, PropertyNames.Items))
			{
				int count = toolStripItems.Count;
				for (int i = 0; i < count; i++)
				{
					this.Add(toolStripItems[i]);
				}
			}
		}

		/// <summary>Determines whether the specified item is a member of the collection.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripItem" /> to search for in the <see cref="T:System.Windows.Forms.ToolStripItemCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.ToolStripItem" /> is a member of the current <see cref="T:System.Windows.Forms.ToolStripItemCollection" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004319 RID: 17177 RVA: 0x0011C768 File Offset: 0x0011A968
		public bool Contains(ToolStripItem value)
		{
			return base.InnerList.Contains(value);
		}

		/// <summary>Removes all items from the collection.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> is read-only.</exception>
		// Token: 0x0600431A RID: 17178 RVA: 0x0011C778 File Offset: 0x0011A978
		public virtual void Clear()
		{
			if (this.IsReadOnly)
			{
				throw new NotSupportedException(SR.GetString("ToolStripItemCollectionIsReadOnly"));
			}
			if (this.Count == 0)
			{
				return;
			}
			ToolStripOverflow toolStripOverflow = null;
			if (this.owner != null && !this.owner.IsDisposingItems)
			{
				this.owner.SuspendLayout();
				toolStripOverflow = this.owner.GetOverflow();
				if (toolStripOverflow != null)
				{
					toolStripOverflow.SuspendLayout();
				}
			}
			try
			{
				while (this.Count != 0)
				{
					this.RemoveAt(this.Count - 1);
				}
			}
			finally
			{
				if (toolStripOverflow != null)
				{
					toolStripOverflow.ResumeLayout(false);
				}
				if (this.owner != null && !this.owner.IsDisposingItems)
				{
					this.owner.ResumeLayout();
				}
			}
		}

		/// <summary>Determines whether the collection contains an item with the specified key.</summary>
		/// <param name="key">The key to locate in the <see cref="T:System.Windows.Forms.ToolStripItemCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> contains a <see cref="T:System.Windows.Forms.ToolStripItem" /> with the specified key; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600431B RID: 17179 RVA: 0x0011C834 File Offset: 0x0011AA34
		public virtual bool ContainsKey(string key)
		{
			return this.IsValidIndex(this.IndexOfKey(key));
		}

		// Token: 0x0600431C RID: 17180 RVA: 0x0011C844 File Offset: 0x0011AA44
		private void CheckCanAddOrInsertItem(ToolStripItem value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.IsReadOnly)
			{
				throw new NotSupportedException(SR.GetString("ToolStripItemCollectionIsReadOnly"));
			}
			ToolStripDropDown toolStripDropDown = this.owner as ToolStripDropDown;
			if (toolStripDropDown != null)
			{
				if (toolStripDropDown.OwnerItem == value)
				{
					throw new NotSupportedException(SR.GetString("ToolStripItemCircularReference"));
				}
				if (value is ToolStripControlHost && !(value is ToolStripScrollButton) && toolStripDropDown.IsRestrictedWindow)
				{
					IntSecurity.AllWindows.Demand();
				}
			}
		}

		/// <summary>Searches for items by their name and returns an array of all matching controls.</summary>
		/// <param name="key">The item name to search the <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> for.</param>
		/// <param name="searchAllChildren">
		///   <see langword="true" /> to search child items of the <see cref="T:System.Windows.Forms.ToolStripItem" /> specified by the <paramref name="key" /> parameter; otherwise, <see langword="false" />.</param>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItem" /> array of the search results.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> parameter is <see langword="null" /> or empty.</exception>
		// Token: 0x0600431D RID: 17181 RVA: 0x0011C8C4 File Offset: 0x0011AAC4
		public ToolStripItem[] Find(string key, bool searchAllChildren)
		{
			if (key == null || key.Length == 0)
			{
				throw new ArgumentNullException("key", SR.GetString("FindKeyMayNotBeEmptyOrNull"));
			}
			ArrayList arrayList = this.FindInternal(key, searchAllChildren, this, new ArrayList());
			ToolStripItem[] array = new ToolStripItem[arrayList.Count];
			arrayList.CopyTo(array, 0);
			return array;
		}

		// Token: 0x0600431E RID: 17182 RVA: 0x0011C918 File Offset: 0x0011AB18
		private ArrayList FindInternal(string key, bool searchAllChildren, ToolStripItemCollection itemsToLookIn, ArrayList foundItems)
		{
			if (itemsToLookIn == null || foundItems == null)
			{
				return null;
			}
			try
			{
				for (int i = 0; i < itemsToLookIn.Count; i++)
				{
					if (itemsToLookIn[i] != null && WindowsFormsUtils.SafeCompareStrings(itemsToLookIn[i].Name, key, true))
					{
						foundItems.Add(itemsToLookIn[i]);
					}
				}
				if (searchAllChildren)
				{
					for (int j = 0; j < itemsToLookIn.Count; j++)
					{
						ToolStripDropDownItem toolStripDropDownItem = itemsToLookIn[j] as ToolStripDropDownItem;
						if (toolStripDropDownItem != null && toolStripDropDownItem.HasDropDownItems)
						{
							foundItems = this.FindInternal(key, searchAllChildren, toolStripDropDownItem.DropDownItems, foundItems);
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsCriticalException(ex))
				{
					throw;
				}
			}
			return foundItems;
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> is read-only; otherwise, <see langword="false" />.</returns>
		// Token: 0x17001063 RID: 4195
		// (get) Token: 0x0600431F RID: 17183 RVA: 0x0011C9CC File Offset: 0x0011ABCC
		public override bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		/// <summary>Removes all items from the collection.</summary>
		// Token: 0x06004320 RID: 17184 RVA: 0x0011C9D4 File Offset: 0x0011ABD4
		void IList.Clear()
		{
			this.Clear();
		}

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>
		///   <see langword="true" /> if the collection has a fixed size; otherwise, <see langword="false" />.</returns>
		// Token: 0x17001064 RID: 4196
		// (get) Token: 0x06004321 RID: 17185 RVA: 0x0011C9DC File Offset: 0x0011ABDC
		bool IList.IsFixedSize
		{
			get
			{
				return base.InnerList.IsFixedSize;
			}
		}

		/// <summary>Determines if the collection contains a specified item.</summary>
		/// <param name="value">The item to locate in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is contained in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004322 RID: 17186 RVA: 0x0011C768 File Offset: 0x0011A968
		bool IList.Contains(object value)
		{
			return base.InnerList.Contains(value);
		}

		/// <summary>Removes an item from the collection at a specified index.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		// Token: 0x06004323 RID: 17187 RVA: 0x0011C9E9 File Offset: 0x0011ABE9
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		/// <summary>Removes the first occurrence of a specified item from the collection.</summary>
		/// <param name="value">The item to remove from the collection.</param>
		// Token: 0x06004324 RID: 17188 RVA: 0x0011C9F2 File Offset: 0x0011ABF2
		void IList.Remove(object value)
		{
			this.Remove(value as ToolStripItem);
		}

		/// <summary>Adds an item to the collection.</summary>
		/// <param name="value">The item to add to the collection.</param>
		/// <returns>The location at which <paramref name="value" /> was inserted.</returns>
		// Token: 0x06004325 RID: 17189 RVA: 0x0011CA00 File Offset: 0x0011AC00
		int IList.Add(object value)
		{
			return this.Add(value as ToolStripItem);
		}

		/// <summary>Determines the location of a specified item in the collection.</summary>
		/// <param name="value">The item to locate in the collection.</param>
		/// <returns>The index of the item in the collection, if found; otherwise, -1.</returns>
		// Token: 0x06004326 RID: 17190 RVA: 0x0011CA0E File Offset: 0x0011AC0E
		int IList.IndexOf(object value)
		{
			return this.IndexOf(value as ToolStripItem);
		}

		/// <summary>Inserts an item into the collection at a specified index.</summary>
		/// <param name="index">The zero-based index at which to insert <paramref name="value" />.</param>
		/// <param name="value">The item to insert into the collection.</param>
		// Token: 0x06004327 RID: 17191 RVA: 0x0011CA1C File Offset: 0x0011AC1C
		void IList.Insert(int index, object value)
		{
			this.Insert(index, value as ToolStripItem);
		}

		/// <summary>Retrieves the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the item to get.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStripItem" /> at the specified index.</returns>
		// Token: 0x17001065 RID: 4197
		object IList.this[int index]
		{
			get
			{
				return base.InnerList[index];
			}
			set
			{
				throw new NotSupportedException(SR.GetString("ToolStripCollectionMustInsertAndRemove"));
			}
		}

		/// <summary>Inserts the specified item into the collection at the specified index.</summary>
		/// <param name="index">The location in the <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> at which to insert the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripItem" /> to insert.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600432A RID: 17194 RVA: 0x0011CA4C File Offset: 0x0011AC4C
		public void Insert(int index, ToolStripItem value)
		{
			this.CheckCanAddOrInsertItem(value);
			this.SetOwner(value);
			base.InnerList.Insert(index, value);
			if (this.itemsCollection && this.owner != null)
			{
				if (this.owner.IsHandleCreated)
				{
					LayoutTransaction.DoLayout(this.owner, value, PropertyNames.Parent);
				}
				else
				{
					CommonProperties.xClearPreferredSizeCache(this.owner);
				}
				this.owner.OnItemAddedInternal(value);
				this.owner.OnItemAdded(new ToolStripItemEventArgs(value));
			}
		}

		/// <summary>Retrieves the index of the specified item in the collection.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripItem" /> to locate in the <see cref="T:System.Windows.Forms.ToolStripItemCollection" />.</param>
		/// <returns>A zero-based index value that represents the position of the specified <see cref="T:System.Windows.Forms.ToolStripItem" /> in the <see cref="T:System.Windows.Forms.ToolStripItemCollection" />, if found; otherwise, -1.</returns>
		// Token: 0x0600432B RID: 17195 RVA: 0x0011CACC File Offset: 0x0011ACCC
		public int IndexOf(ToolStripItem value)
		{
			return base.InnerList.IndexOf(value);
		}

		/// <summary>Retrieves the index of the first occurrence of the specified item within the collection.</summary>
		/// <param name="key">The name of the <see cref="T:System.Windows.Forms.ToolStripItem" /> to search for.</param>
		/// <returns>A zero-based index value that represents the position of the first occurrence of the <see cref="T:System.Windows.Forms.ToolStripItem" /> specified by the <paramref name="key" /> parameter, if found; otherwise, -1.</returns>
		// Token: 0x0600432C RID: 17196 RVA: 0x0011CADC File Offset: 0x0011ACDC
		public virtual int IndexOfKey(string key)
		{
			if (key == null || key.Length == 0)
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

		// Token: 0x0600432D RID: 17197 RVA: 0x0011CB5C File Offset: 0x0011AD5C
		private bool IsValidIndex(int index)
		{
			return index >= 0 && index < this.Count;
		}

		// Token: 0x0600432E RID: 17198 RVA: 0x0011CB70 File Offset: 0x0011AD70
		private void OnAfterRemove(ToolStripItem item)
		{
			if (this.itemsCollection)
			{
				ToolStrip toolStrip = null;
				if (item != null)
				{
					toolStrip = item.ParentInternal;
					item.SetOwner(null);
				}
				if (this.owner != null)
				{
					this.owner.OnItemRemovedInternal(item);
					if (!this.owner.IsDisposingItems)
					{
						ToolStripItemEventArgs toolStripItemEventArgs = new ToolStripItemEventArgs(item);
						this.owner.OnItemRemoved(toolStripItemEventArgs);
						if (toolStrip != null && toolStrip != this.owner)
						{
							toolStrip.OnItemVisibleChanged(toolStripItemEventArgs, false);
						}
					}
				}
			}
		}

		/// <summary>Removes the specified item from the collection.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripItem" /> to remove from the <see cref="T:System.Windows.Forms.ToolStripItemCollection" />.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> is read-only.</exception>
		// Token: 0x0600432F RID: 17199 RVA: 0x0011CBE0 File Offset: 0x0011ADE0
		public void Remove(ToolStripItem value)
		{
			if (this.IsReadOnly)
			{
				throw new NotSupportedException(SR.GetString("ToolStripItemCollectionIsReadOnly"));
			}
			base.InnerList.Remove(value);
			this.OnAfterRemove(value);
		}

		/// <summary>Removes an item from the specified index in the collection.</summary>
		/// <param name="index">The index value of the <see cref="T:System.Windows.Forms.ToolStripItem" /> to remove.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> is read-only.</exception>
		// Token: 0x06004330 RID: 17200 RVA: 0x0011CC10 File Offset: 0x0011AE10
		public void RemoveAt(int index)
		{
			if (this.IsReadOnly)
			{
				throw new NotSupportedException(SR.GetString("ToolStripItemCollectionIsReadOnly"));
			}
			ToolStripItem toolStripItem = null;
			if (index < this.Count && index >= 0)
			{
				toolStripItem = (ToolStripItem)base.InnerList[index];
			}
			base.InnerList.RemoveAt(index);
			this.OnAfterRemove(toolStripItem);
		}

		/// <summary>Removes the item that has the specified key.</summary>
		/// <param name="key">The key of the <see cref="T:System.Windows.Forms.ToolStripItem" /> to remove.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> is read-only.</exception>
		// Token: 0x06004331 RID: 17201 RVA: 0x0011CC6C File Offset: 0x0011AE6C
		public virtual void RemoveByKey(string key)
		{
			if (this.IsReadOnly)
			{
				throw new NotSupportedException(SR.GetString("ToolStripItemCollectionIsReadOnly"));
			}
			int num = this.IndexOfKey(key);
			if (this.IsValidIndex(num))
			{
				this.RemoveAt(num);
			}
		}

		/// <summary>Copies the collection into the specified position of the specified <see cref="T:System.Windows.Forms.ToolStripItem" /> array.</summary>
		/// <param name="array">The array of type <see cref="T:System.Windows.Forms.ToolStripItem" /> to which to copy the collection.</param>
		/// <param name="index">The position in the <see cref="T:System.Windows.Forms.ToolStripItem" /> array at which to paste the collection.</param>
		// Token: 0x06004332 RID: 17202 RVA: 0x0011CCA9 File Offset: 0x0011AEA9
		public void CopyTo(ToolStripItem[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		// Token: 0x06004333 RID: 17203 RVA: 0x0011CCB8 File Offset: 0x0011AEB8
		internal void MoveItem(ToolStripItem value)
		{
			if (value.ParentInternal != null)
			{
				int num = value.ParentInternal.Items.IndexOf(value);
				if (num >= 0)
				{
					value.ParentInternal.Items.RemoveAt(num);
				}
			}
			this.Add(value);
		}

		// Token: 0x06004334 RID: 17204 RVA: 0x0011CCFC File Offset: 0x0011AEFC
		internal void MoveItem(int index, ToolStripItem value)
		{
			if (index == this.Count)
			{
				this.MoveItem(value);
				return;
			}
			if (value.ParentInternal != null)
			{
				int num = value.ParentInternal.Items.IndexOf(value);
				if (num >= 0)
				{
					value.ParentInternal.Items.RemoveAt(num);
					if (value.ParentInternal == this.owner && index > num)
					{
						index--;
					}
				}
			}
			this.Insert(index, value);
		}

		// Token: 0x06004335 RID: 17205 RVA: 0x0011CD68 File Offset: 0x0011AF68
		private void SetOwner(ToolStripItem item)
		{
			if (this.itemsCollection && item != null)
			{
				if (item.Owner != null)
				{
					item.Owner.Items.Remove(item);
				}
				item.SetOwner(this.owner);
				if (item.Renderer != null)
				{
					item.Renderer.InitializeItem(item);
				}
			}
		}

		// Token: 0x04002593 RID: 9619
		private ToolStrip owner;

		// Token: 0x04002594 RID: 9620
		private bool itemsCollection;

		// Token: 0x04002595 RID: 9621
		private bool isReadOnly;

		// Token: 0x04002596 RID: 9622
		private int lastAccessedIndex;
	}
}
