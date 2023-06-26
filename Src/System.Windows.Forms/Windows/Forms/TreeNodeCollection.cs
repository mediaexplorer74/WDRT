using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.TreeNode" /> objects.</summary>
	// Token: 0x02000411 RID: 1041
	[Editor("System.Windows.Forms.Design.TreeNodeCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public class TreeNodeCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x060048BA RID: 18618 RVA: 0x001325A7 File Offset: 0x001307A7
		internal TreeNodeCollection(TreeNode owner)
		{
			this.owner = owner;
		}

		// Token: 0x170011DD RID: 4573
		// (get) Token: 0x060048BB RID: 18619 RVA: 0x001325C4 File Offset: 0x001307C4
		// (set) Token: 0x060048BC RID: 18620 RVA: 0x001325CC File Offset: 0x001307CC
		internal int FixedIndex
		{
			get
			{
				return this.fixedIndex;
			}
			set
			{
				this.fixedIndex = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.TreeNode" /> at the specified indexed location in the collection.</summary>
		/// <param name="index">The indexed location of the <see cref="T:System.Windows.Forms.TreeNode" /> in the collection.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> at the specified indexed location in the collection.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> value is less than 0 or is greater than the number of tree nodes in the collection.</exception>
		// Token: 0x170011DE RID: 4574
		public virtual TreeNode this[int index]
		{
			get
			{
				if (index < 0 || index >= this.owner.childCount)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.owner.children[index];
			}
			set
			{
				if (index < 0 || index >= this.owner.childCount)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				value.parent = this.owner;
				value.index = index;
				this.owner.children[index] = value;
				value.Realize(false);
			}
		}

		/// <summary>Gets or sets the tree node at the specified index in the collection.</summary>
		/// <param name="index">The zero-based index at which to get or set the item.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> at the specified index in the <see cref="T:System.Windows.Forms.TreeNodeCollection" />.</returns>
		/// <exception cref="T:System.ArgumentException">The value set is not a <see cref="T:System.Windows.Forms.TreeNode" />.</exception>
		// Token: 0x170011DF RID: 4575
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				if (value is TreeNode)
				{
					this[index] = (TreeNode)value;
					return;
				}
				throw new ArgumentException(SR.GetString("TreeNodeCollectionBadTreeNode"), "value");
			}
		}

		/// <summary>Gets the tree node with the specified key from the collection.</summary>
		/// <param name="key">The name of the <see cref="T:System.Windows.Forms.TreeNode" /> to retrieve from the collection.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> with the specified key.</returns>
		// Token: 0x170011E0 RID: 4576
		public virtual TreeNode this[string key]
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

		/// <summary>Gets the total number of <see cref="T:System.Windows.Forms.TreeNode" /> objects in the collection.</summary>
		/// <returns>The total number of <see cref="T:System.Windows.Forms.TreeNode" /> objects in the collection.</returns>
		// Token: 0x170011E1 RID: 4577
		// (get) Token: 0x060048C2 RID: 18626 RVA: 0x001326E5 File Offset: 0x001308E5
		[Browsable(false)]
		public int Count
		{
			get
			{
				return this.owner.childCount;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.TreeNodeCollection" />.</returns>
		// Token: 0x170011E2 RID: 4578
		// (get) Token: 0x060048C3 RID: 18627 RVA: 0x00006A49 File Offset: 0x00004C49
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
		// Token: 0x170011E3 RID: 4579
		// (get) Token: 0x060048C4 RID: 18628 RVA: 0x0001180C File Offset: 0x0000FA0C
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the tree node collection has a fixed size.</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x170011E4 RID: 4580
		// (get) Token: 0x060048C5 RID: 18629 RVA: 0x0001180C File Offset: 0x0000FA0C
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
		// Token: 0x170011E5 RID: 4581
		// (get) Token: 0x060048C6 RID: 18630 RVA: 0x0001180C File Offset: 0x0000FA0C
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Adds a new tree node with the specified label text to the end of the current tree node collection.</summary>
		/// <param name="text">The label text displayed by the <see cref="T:System.Windows.Forms.TreeNode" />.</param>
		/// <returns>A <see cref="T:System.Windows.Forms.TreeNode" /> that represents the tree node being added to the collection.</returns>
		// Token: 0x060048C7 RID: 18631 RVA: 0x001326F4 File Offset: 0x001308F4
		public virtual TreeNode Add(string text)
		{
			TreeNode treeNode = new TreeNode(text);
			this.Add(treeNode);
			return treeNode;
		}

		/// <summary>Creates a new tree node with the specified key and text, and adds it to the collection.</summary>
		/// <param name="key">The name of the tree node.</param>
		/// <param name="text">The text to display in the tree node.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> that was added to the collection.</returns>
		// Token: 0x060048C8 RID: 18632 RVA: 0x00132714 File Offset: 0x00130914
		public virtual TreeNode Add(string key, string text)
		{
			TreeNode treeNode = new TreeNode(text);
			treeNode.Name = key;
			this.Add(treeNode);
			return treeNode;
		}

		/// <summary>Creates a tree node with the specified key, text, and image, and adds it to the collection.</summary>
		/// <param name="key">The name of the tree node.</param>
		/// <param name="text">The text to display in the tree node.</param>
		/// <param name="imageIndex">The index of the image to display in the tree node.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> that was added to the collection.</returns>
		// Token: 0x060048C9 RID: 18633 RVA: 0x00132738 File Offset: 0x00130938
		public virtual TreeNode Add(string key, string text, int imageIndex)
		{
			TreeNode treeNode = new TreeNode(text);
			treeNode.Name = key;
			treeNode.ImageIndex = imageIndex;
			this.Add(treeNode);
			return treeNode;
		}

		/// <summary>Creates a tree node with the specified key, text, and image, and adds it to the collection.</summary>
		/// <param name="key">The name of the tree node.</param>
		/// <param name="text">The text to display in the tree node.</param>
		/// <param name="imageKey">The image to display in the tree node.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> that was added to the collection.</returns>
		// Token: 0x060048CA RID: 18634 RVA: 0x00132764 File Offset: 0x00130964
		public virtual TreeNode Add(string key, string text, string imageKey)
		{
			TreeNode treeNode = new TreeNode(text);
			treeNode.Name = key;
			treeNode.ImageKey = imageKey;
			this.Add(treeNode);
			return treeNode;
		}

		/// <summary>Creates a tree node with the specified key, text, and images, and adds it to the collection.</summary>
		/// <param name="key">The name of the tree node.</param>
		/// <param name="text">The text to display in the tree node.</param>
		/// <param name="imageIndex">The index of the image to display in the tree node.</param>
		/// <param name="selectedImageIndex">The index of the image to be displayed in the tree node when it is in a selected state.</param>
		/// <returns>The tree node that was added to the collection.</returns>
		// Token: 0x060048CB RID: 18635 RVA: 0x00132790 File Offset: 0x00130990
		public virtual TreeNode Add(string key, string text, int imageIndex, int selectedImageIndex)
		{
			TreeNode treeNode = new TreeNode(text, imageIndex, selectedImageIndex);
			treeNode.Name = key;
			this.Add(treeNode);
			return treeNode;
		}

		/// <summary>Creates a tree node with the specified key, text, and images, and adds it to the collection.</summary>
		/// <param name="key">The name of the tree node.</param>
		/// <param name="text">The text to display in the tree node.</param>
		/// <param name="imageKey">The key of the image to display in the tree node.</param>
		/// <param name="selectedImageKey">The key of the image to display when the node is in a selected state.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> that was added to the collection.</returns>
		// Token: 0x060048CC RID: 18636 RVA: 0x001327B8 File Offset: 0x001309B8
		public virtual TreeNode Add(string key, string text, string imageKey, string selectedImageKey)
		{
			TreeNode treeNode = new TreeNode(text);
			treeNode.Name = key;
			treeNode.ImageKey = imageKey;
			treeNode.SelectedImageKey = selectedImageKey;
			this.Add(treeNode);
			return treeNode;
		}

		/// <summary>Adds an array of previously created tree nodes to the collection.</summary>
		/// <param name="nodes">An array of <see cref="T:System.Windows.Forms.TreeNode" /> objects representing the tree nodes to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="nodes" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="nodes" /> is the child of another <see cref="T:System.Windows.Forms.TreeView" />.</exception>
		// Token: 0x060048CD RID: 18637 RVA: 0x001327EC File Offset: 0x001309EC
		public virtual void AddRange(TreeNode[] nodes)
		{
			if (nodes == null)
			{
				throw new ArgumentNullException("nodes");
			}
			if (nodes.Length == 0)
			{
				return;
			}
			TreeView treeView = this.owner.TreeView;
			if (treeView != null && nodes.Length > 200)
			{
				treeView.BeginUpdate();
			}
			this.owner.Nodes.FixedIndex = this.owner.childCount;
			this.owner.EnsureCapacity(nodes.Length);
			for (int i = nodes.Length - 1; i >= 0; i--)
			{
				this.AddInternal(nodes[i], i);
			}
			this.owner.Nodes.FixedIndex = -1;
			if (treeView != null && nodes.Length > 200)
			{
				treeView.EndUpdate();
			}
		}

		/// <summary>Finds the tree nodes with specified key, optionally searching subnodes.</summary>
		/// <param name="key">The name of the tree node to search for.</param>
		/// <param name="searchAllChildren">
		///   <see langword="true" /> to search child nodes of tree nodes; otherwise, <see langword="false" />.</param>
		/// <returns>An array of <see cref="T:System.Windows.Forms.TreeNode" /> objects whose <see cref="P:System.Windows.Forms.TreeNode.Name" /> property matches the specified key.</returns>
		// Token: 0x060048CE RID: 18638 RVA: 0x00132894 File Offset: 0x00130A94
		public TreeNode[] Find(string key, bool searchAllChildren)
		{
			ArrayList arrayList = this.FindInternal(key, searchAllChildren, this, new ArrayList());
			TreeNode[] array = new TreeNode[arrayList.Count];
			arrayList.CopyTo(array, 0);
			return array;
		}

		// Token: 0x060048CF RID: 18639 RVA: 0x001328C8 File Offset: 0x00130AC8
		private ArrayList FindInternal(string key, bool searchAllChildren, TreeNodeCollection treeNodeCollectionToLookIn, ArrayList foundTreeNodes)
		{
			if (treeNodeCollectionToLookIn == null || foundTreeNodes == null)
			{
				return null;
			}
			for (int i = 0; i < treeNodeCollectionToLookIn.Count; i++)
			{
				if (treeNodeCollectionToLookIn[i] != null && WindowsFormsUtils.SafeCompareStrings(treeNodeCollectionToLookIn[i].Name, key, true))
				{
					foundTreeNodes.Add(treeNodeCollectionToLookIn[i]);
				}
			}
			if (searchAllChildren)
			{
				for (int j = 0; j < treeNodeCollectionToLookIn.Count; j++)
				{
					if (treeNodeCollectionToLookIn[j] != null && treeNodeCollectionToLookIn[j].Nodes != null && treeNodeCollectionToLookIn[j].Nodes.Count > 0)
					{
						foundTreeNodes = this.FindInternal(key, searchAllChildren, treeNodeCollectionToLookIn[j].Nodes, foundTreeNodes);
					}
				}
			}
			return foundTreeNodes;
		}

		/// <summary>Adds a previously created tree node to the end of the tree node collection.</summary>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> to add to the collection.</param>
		/// <returns>The zero-based index value of the <see cref="T:System.Windows.Forms.TreeNode" /> added to the tree node collection.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="node" /> is currently assigned to another <see cref="T:System.Windows.Forms.TreeView" />.</exception>
		// Token: 0x060048D0 RID: 18640 RVA: 0x00132975 File Offset: 0x00130B75
		public virtual int Add(TreeNode node)
		{
			return this.AddInternal(node, 0);
		}

		// Token: 0x060048D1 RID: 18641 RVA: 0x00132980 File Offset: 0x00130B80
		private int AddInternal(TreeNode node, int delta)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (node.handle != IntPtr.Zero)
			{
				throw new ArgumentException(SR.GetString("OnlyOneControl", new object[] { node.Text }), "node");
			}
			TreeView treeView = this.owner.TreeView;
			if (treeView != null && treeView.Sorted)
			{
				return this.owner.AddSorted(node);
			}
			node.parent = this.owner;
			int num = this.owner.Nodes.FixedIndex;
			if (num != -1)
			{
				node.index = num + delta;
			}
			else
			{
				this.owner.EnsureCapacity(1);
				node.index = this.owner.childCount;
			}
			this.owner.children[node.index] = node;
			this.owner.childCount++;
			node.Realize(false);
			if (treeView != null && node == treeView.selectedNode)
			{
				treeView.SelectedNode = node;
			}
			if (treeView != null && treeView.TreeViewNodeSorter != null)
			{
				treeView.Sort();
			}
			return node.index;
		}

		/// <summary>Adds an object to the end of the tree node collection.</summary>
		/// <param name="node">The object to add to the tree node collection.</param>
		/// <returns>The zero-based index value of the <see cref="T:System.Windows.Forms.TreeNode" /> that was added to the tree node collection.</returns>
		/// <exception cref="T:System.Exception">
		///   <paramref name="node" /> is currently assigned to another <see cref="T:System.Windows.Forms.TreeView" /> control.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="node" /> is <see langword="null" />.</exception>
		// Token: 0x060048D2 RID: 18642 RVA: 0x00132A95 File Offset: 0x00130C95
		int IList.Add(object node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (node is TreeNode)
			{
				return this.Add((TreeNode)node);
			}
			return this.Add(node.ToString()).index;
		}

		/// <summary>Determines whether the specified tree node is a member of the collection.</summary>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> to locate in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.TreeNode" /> is a member of the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x060048D3 RID: 18643 RVA: 0x00132ACB File Offset: 0x00130CCB
		public bool Contains(TreeNode node)
		{
			return this.IndexOf(node) != -1;
		}

		/// <summary>Determines whether the collection contains a tree node with the specified key.</summary>
		/// <param name="key">The name of the <see cref="T:System.Windows.Forms.TreeNode" /> to search for.</param>
		/// <returns>
		///   <see langword="true" /> to indicate the collection contains a <see cref="T:System.Windows.Forms.TreeNode" /> with the specified key; otherwise, <see langword="false" />.</returns>
		// Token: 0x060048D4 RID: 18644 RVA: 0x00132ADA File Offset: 0x00130CDA
		public virtual bool ContainsKey(string key)
		{
			return this.IsValidIndex(this.IndexOfKey(key));
		}

		/// <summary>Determines whether the specified tree node is a member of the collection.</summary>
		/// <param name="node">The object to find in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="node" /> is a member of the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x060048D5 RID: 18645 RVA: 0x00132AE9 File Offset: 0x00130CE9
		bool IList.Contains(object node)
		{
			return node is TreeNode && this.Contains((TreeNode)node);
		}

		/// <summary>Returns the index of the specified tree node in the collection.</summary>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> to locate in the collection.</param>
		/// <returns>The zero-based index of the item found in the tree node collection; otherwise, -1.</returns>
		// Token: 0x060048D6 RID: 18646 RVA: 0x00132B04 File Offset: 0x00130D04
		public int IndexOf(TreeNode node)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (this[i] == node)
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Returns the index of the specified tree node in the collection.</summary>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> to locate in the collection.</param>
		/// <returns>The zero-based index of the item found in the tree node collection; otherwise, -1.</returns>
		// Token: 0x060048D7 RID: 18647 RVA: 0x00132B2F File Offset: 0x00130D2F
		int IList.IndexOf(object node)
		{
			if (node is TreeNode)
			{
				return this.IndexOf((TreeNode)node);
			}
			return -1;
		}

		/// <summary>Returns the index of the first occurrence of a tree node with the specified key.</summary>
		/// <param name="key">The name of the tree node to search for.</param>
		/// <returns>The zero-based index of the first occurrence of a tree node with the specified key, if found; otherwise, -1.</returns>
		// Token: 0x060048D8 RID: 18648 RVA: 0x00132B48 File Offset: 0x00130D48
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

		/// <summary>Inserts an existing tree node into the tree node collection at the specified location.</summary>
		/// <param name="index">The indexed location within the collection to insert the tree node.</param>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> to insert into the collection.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="node" /> is currently assigned to another <see cref="T:System.Windows.Forms.TreeView" />.</exception>
		// Token: 0x060048D9 RID: 18649 RVA: 0x00132BC8 File Offset: 0x00130DC8
		public virtual void Insert(int index, TreeNode node)
		{
			if (node.handle != IntPtr.Zero)
			{
				throw new ArgumentException(SR.GetString("OnlyOneControl", new object[] { node.Text }), "node");
			}
			TreeView treeView = this.owner.TreeView;
			if (treeView != null && treeView.Sorted)
			{
				this.owner.AddSorted(node);
				return;
			}
			if (index < 0)
			{
				index = 0;
			}
			if (index > this.owner.childCount)
			{
				index = this.owner.childCount;
			}
			this.owner.InsertNodeAt(index, node);
		}

		/// <summary>Inserts an existing tree node in the tree node collection at the specified location.</summary>
		/// <param name="index">The indexed location within the collection to insert the tree node.</param>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> to insert into the collection.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="node" /> is currently assigned to another <see cref="T:System.Windows.Forms.TreeView" />.  
		/// -or-  
		/// <paramref name="node" /> is not a <see cref="T:System.Windows.Forms.TreeNode" />.</exception>
		// Token: 0x060048DA RID: 18650 RVA: 0x00132C5F File Offset: 0x00130E5F
		void IList.Insert(int index, object node)
		{
			if (node is TreeNode)
			{
				this.Insert(index, (TreeNode)node);
				return;
			}
			throw new ArgumentException(SR.GetString("TreeNodeCollectionBadTreeNode"), "node");
		}

		/// <summary>Creates a tree node with the specified text and inserts it at the specified index.</summary>
		/// <param name="index">The location within the collection to insert the node.</param>
		/// <param name="text">The text to display in the tree node.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> that was inserted in the collection.</returns>
		// Token: 0x060048DB RID: 18651 RVA: 0x00132C8C File Offset: 0x00130E8C
		public virtual TreeNode Insert(int index, string text)
		{
			TreeNode treeNode = new TreeNode(text);
			this.Insert(index, treeNode);
			return treeNode;
		}

		/// <summary>Creates a tree node with the specified text and key, and inserts it into the collection.</summary>
		/// <param name="index">The location within the collection to insert the node.</param>
		/// <param name="key">The name of the tree node.</param>
		/// <param name="text">The text to display in the tree node.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> that was inserted in the collection.</returns>
		// Token: 0x060048DC RID: 18652 RVA: 0x00132CAC File Offset: 0x00130EAC
		public virtual TreeNode Insert(int index, string key, string text)
		{
			TreeNode treeNode = new TreeNode(text);
			treeNode.Name = key;
			this.Insert(index, treeNode);
			return treeNode;
		}

		/// <summary>Creates a tree node with the specified key, text, and image, and inserts it into the collection at the specified index.</summary>
		/// <param name="index">The location within the collection to insert the node.</param>
		/// <param name="key">The name of the tree node.</param>
		/// <param name="text">The text to display in the tree node.</param>
		/// <param name="imageIndex">The index of the image to display in the tree node.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> that was inserted in the collection.</returns>
		// Token: 0x060048DD RID: 18653 RVA: 0x00132CD0 File Offset: 0x00130ED0
		public virtual TreeNode Insert(int index, string key, string text, int imageIndex)
		{
			TreeNode treeNode = new TreeNode(text);
			treeNode.Name = key;
			treeNode.ImageIndex = imageIndex;
			this.Insert(index, treeNode);
			return treeNode;
		}

		/// <summary>Creates a tree node with the specified key, text, and image, and inserts it into the collection at the specified index.</summary>
		/// <param name="index">The location within the collection to insert the node.</param>
		/// <param name="key">The name of the tree node.</param>
		/// <param name="text">The text to display in the tree node.</param>
		/// <param name="imageKey">The key of the image to display in the tree node.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> that was inserted in the collection.</returns>
		// Token: 0x060048DE RID: 18654 RVA: 0x00132CFC File Offset: 0x00130EFC
		public virtual TreeNode Insert(int index, string key, string text, string imageKey)
		{
			TreeNode treeNode = new TreeNode(text);
			treeNode.Name = key;
			treeNode.ImageKey = imageKey;
			this.Insert(index, treeNode);
			return treeNode;
		}

		/// <summary>Creates a tree node with the specified key, text, and images, and inserts it into the collection at the specified index.</summary>
		/// <param name="index">The location within the collection to insert the node.</param>
		/// <param name="key">The name of the tree node.</param>
		/// <param name="text">The text to display in the tree node.</param>
		/// <param name="imageIndex">The index of the image to display in the tree node.</param>
		/// <param name="selectedImageIndex">The index of the image to display in the tree node when it is in a selected state.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> that was inserted in the collection.</returns>
		// Token: 0x060048DF RID: 18655 RVA: 0x00132D28 File Offset: 0x00130F28
		public virtual TreeNode Insert(int index, string key, string text, int imageIndex, int selectedImageIndex)
		{
			TreeNode treeNode = new TreeNode(text, imageIndex, selectedImageIndex);
			treeNode.Name = key;
			this.Insert(index, treeNode);
			return treeNode;
		}

		/// <summary>Creates a tree node with the specified key, text, and images, and inserts it into the collection at the specified index.</summary>
		/// <param name="index">The location within the collection to insert the node.</param>
		/// <param name="key">The name of the tree node.</param>
		/// <param name="text">The text to display in the tree node.</param>
		/// <param name="imageKey">The key of the image to display in the tree node.</param>
		/// <param name="selectedImageKey">The key of the image to display in the tree node when it is in a selected state.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> that was inserted in the collection.</returns>
		// Token: 0x060048E0 RID: 18656 RVA: 0x00132D50 File Offset: 0x00130F50
		public virtual TreeNode Insert(int index, string key, string text, string imageKey, string selectedImageKey)
		{
			TreeNode treeNode = new TreeNode(text);
			treeNode.Name = key;
			treeNode.ImageKey = imageKey;
			treeNode.SelectedImageKey = selectedImageKey;
			this.Insert(index, treeNode);
			return treeNode;
		}

		// Token: 0x060048E1 RID: 18657 RVA: 0x00132D84 File Offset: 0x00130F84
		private bool IsValidIndex(int index)
		{
			return index >= 0 && index < this.Count;
		}

		/// <summary>Removes all tree nodes from the collection.</summary>
		// Token: 0x060048E2 RID: 18658 RVA: 0x00132D95 File Offset: 0x00130F95
		public virtual void Clear()
		{
			this.owner.Clear();
		}

		/// <summary>Copies the entire collection into an existing array at a specified location within the array.</summary>
		/// <param name="dest">The destination array.</param>
		/// <param name="index">The index in the destination array at which storing begins.</param>
		// Token: 0x060048E3 RID: 18659 RVA: 0x00132DA2 File Offset: 0x00130FA2
		public void CopyTo(Array dest, int index)
		{
			if (this.owner.childCount > 0)
			{
				Array.Copy(this.owner.children, 0, dest, index, this.owner.childCount);
			}
		}

		/// <summary>Removes the specified tree node from the tree node collection.</summary>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> to remove.</param>
		// Token: 0x060048E4 RID: 18660 RVA: 0x00132DD0 File Offset: 0x00130FD0
		public void Remove(TreeNode node)
		{
			node.Remove();
		}

		/// <summary>Removes the specified tree node from the tree node collection.</summary>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> to remove from the collection.</param>
		// Token: 0x060048E5 RID: 18661 RVA: 0x00132DD8 File Offset: 0x00130FD8
		void IList.Remove(object node)
		{
			if (node is TreeNode)
			{
				this.Remove((TreeNode)node);
			}
		}

		/// <summary>Removes a tree node from the tree node collection at a specified index.</summary>
		/// <param name="index">The index of the <see cref="T:System.Windows.Forms.TreeNode" /> to remove.</param>
		// Token: 0x060048E6 RID: 18662 RVA: 0x00132DEE File Offset: 0x00130FEE
		public virtual void RemoveAt(int index)
		{
			this[index].Remove();
		}

		/// <summary>Removes the tree node with the specified key from the collection.</summary>
		/// <param name="key">The name of the tree node to remove from the collection.</param>
		// Token: 0x060048E7 RID: 18663 RVA: 0x00132DFC File Offset: 0x00130FFC
		public virtual void RemoveByKey(string key)
		{
			int num = this.IndexOfKey(key);
			if (this.IsValidIndex(num))
			{
				this.RemoveAt(num);
			}
		}

		/// <summary>Returns an enumerator that can be used to iterate through the tree node collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the tree node collection.</returns>
		// Token: 0x060048E8 RID: 18664 RVA: 0x00132E24 File Offset: 0x00131024
		public IEnumerator GetEnumerator()
		{
			object[] children = this.owner.children;
			return new WindowsFormsUtils.ArraySubsetEnumerator(children, this.owner.childCount);
		}

		// Token: 0x04002739 RID: 10041
		private TreeNode owner;

		// Token: 0x0400273A RID: 10042
		private int lastAccessedIndex = -1;

		// Token: 0x0400273B RID: 10043
		private int fixedIndex = -1;
	}
}
