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
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>Represents a node of a <see cref="T:System.Windows.Forms.TreeView" />.</summary>
	// Token: 0x0200040E RID: 1038
	[TypeConverter(typeof(TreeNodeConverter))]
	[DefaultProperty("Text")]
	[Serializable]
	public class TreeNode : MarshalByRefObject, ICloneable, ISerializable
	{
		// Token: 0x170011B2 RID: 4530
		// (get) Token: 0x0600484D RID: 18509 RVA: 0x001303F9 File Offset: 0x0012E5F9
		internal TreeNode.TreeNodeImageIndexer ImageIndexer
		{
			get
			{
				if (this.imageIndexer == null)
				{
					this.imageIndexer = new TreeNode.TreeNodeImageIndexer(this, TreeNode.TreeNodeImageIndexer.ImageListType.Default);
				}
				return this.imageIndexer;
			}
		}

		// Token: 0x170011B3 RID: 4531
		// (get) Token: 0x0600484E RID: 18510 RVA: 0x00130416 File Offset: 0x0012E616
		internal TreeNode.TreeNodeImageIndexer SelectedImageIndexer
		{
			get
			{
				if (this.selectedImageIndexer == null)
				{
					this.selectedImageIndexer = new TreeNode.TreeNodeImageIndexer(this, TreeNode.TreeNodeImageIndexer.ImageListType.Default);
				}
				return this.selectedImageIndexer;
			}
		}

		// Token: 0x170011B4 RID: 4532
		// (get) Token: 0x0600484F RID: 18511 RVA: 0x00130433 File Offset: 0x0012E633
		internal TreeNode.TreeNodeImageIndexer StateImageIndexer
		{
			get
			{
				if (this.stateImageIndexer == null)
				{
					this.stateImageIndexer = new TreeNode.TreeNodeImageIndexer(this, TreeNode.TreeNodeImageIndexer.ImageListType.State);
				}
				return this.stateImageIndexer;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeNode" /> class.</summary>
		// Token: 0x06004850 RID: 18512 RVA: 0x00130450 File Offset: 0x0012E650
		public TreeNode()
		{
			this.treeNodeState = default(BitVector32);
		}

		// Token: 0x06004851 RID: 18513 RVA: 0x0013046F File Offset: 0x0012E66F
		internal TreeNode(TreeView treeView)
			: this()
		{
			this.treeView = treeView;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeNode" /> class with the specified label text.</summary>
		/// <param name="text">The label <see cref="P:System.Windows.Forms.TreeNode.Text" /> of the new tree node.</param>
		// Token: 0x06004852 RID: 18514 RVA: 0x0013047E File Offset: 0x0012E67E
		public TreeNode(string text)
			: this()
		{
			this.text = text;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeNode" /> class with the specified label text and child tree nodes.</summary>
		/// <param name="text">The label <see cref="P:System.Windows.Forms.TreeNode.Text" /> of the new tree node.</param>
		/// <param name="children">An array of child <see cref="T:System.Windows.Forms.TreeNode" /> objects.</param>
		// Token: 0x06004853 RID: 18515 RVA: 0x0013048D File Offset: 0x0012E68D
		public TreeNode(string text, TreeNode[] children)
			: this()
		{
			this.text = text;
			this.Nodes.AddRange(children);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeNode" /> class with the specified label text and images to display when the tree node is in a selected and unselected state.</summary>
		/// <param name="text">The label <see cref="P:System.Windows.Forms.TreeNode.Text" /> of the new tree node.</param>
		/// <param name="imageIndex">The index value of <see cref="T:System.Drawing.Image" /> to display when the tree node is unselected.</param>
		/// <param name="selectedImageIndex">The index value of <see cref="T:System.Drawing.Image" /> to display when the tree node is selected.</param>
		// Token: 0x06004854 RID: 18516 RVA: 0x001304A8 File Offset: 0x0012E6A8
		public TreeNode(string text, int imageIndex, int selectedImageIndex)
			: this()
		{
			this.text = text;
			this.ImageIndexer.Index = imageIndex;
			this.SelectedImageIndexer.Index = selectedImageIndex;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeNode" /> class with the specified label text, child tree nodes, and images to display when the tree node is in a selected and unselected state.</summary>
		/// <param name="text">The label <see cref="P:System.Windows.Forms.TreeNode.Text" /> of the new tree node.</param>
		/// <param name="imageIndex">The index value of <see cref="T:System.Drawing.Image" /> to display when the tree node is unselected.</param>
		/// <param name="selectedImageIndex">The index value of <see cref="T:System.Drawing.Image" /> to display when the tree node is selected.</param>
		/// <param name="children">An array of child <see cref="T:System.Windows.Forms.TreeNode" /> objects.</param>
		// Token: 0x06004855 RID: 18517 RVA: 0x001304CF File Offset: 0x0012E6CF
		public TreeNode(string text, int imageIndex, int selectedImageIndex, TreeNode[] children)
			: this()
		{
			this.text = text;
			this.ImageIndexer.Index = imageIndex;
			this.SelectedImageIndexer.Index = selectedImageIndex;
			this.Nodes.AddRange(children);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeNode" /> class using the specified serialization information and context.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains the data to deserialize the class.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source and destination of the serialized stream.</param>
		// Token: 0x06004856 RID: 18518 RVA: 0x00130503 File Offset: 0x0012E703
		protected TreeNode(SerializationInfo serializationInfo, StreamingContext context)
			: this()
		{
			this.Deserialize(serializationInfo, context);
		}

		/// <summary>Gets or sets the background color of the tree node.</summary>
		/// <returns>The background <see cref="T:System.Drawing.Color" /> of the tree node. The default is <see cref="F:System.Drawing.Color.Empty" />.</returns>
		// Token: 0x170011B5 RID: 4533
		// (get) Token: 0x06004857 RID: 18519 RVA: 0x00130513 File Offset: 0x0012E713
		// (set) Token: 0x06004858 RID: 18520 RVA: 0x00130530 File Offset: 0x0012E730
		[SRCategory("CatAppearance")]
		[SRDescription("TreeNodeBackColorDescr")]
		public Color BackColor
		{
			get
			{
				if (this.propBag == null)
				{
					return Color.Empty;
				}
				return this.propBag.BackColor;
			}
			set
			{
				Color backColor = this.BackColor;
				if (value.IsEmpty)
				{
					if (this.propBag != null)
					{
						this.propBag.BackColor = Color.Empty;
						this.RemovePropBagIfEmpty();
					}
					if (!backColor.IsEmpty)
					{
						this.InvalidateHostTree();
					}
					return;
				}
				if (this.propBag == null)
				{
					this.propBag = new OwnerDrawPropertyBag();
				}
				this.propBag.BackColor = value;
				if (!value.Equals(backColor))
				{
					this.InvalidateHostTree();
				}
			}
		}

		/// <summary>Gets the bounds of the tree node.</summary>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the tree node.</returns>
		// Token: 0x170011B6 RID: 4534
		// (get) Token: 0x06004859 RID: 18521 RVA: 0x001305B8 File Offset: 0x0012E7B8
		[Browsable(false)]
		public unsafe Rectangle Bounds
		{
			get
			{
				TreeView treeView = this.TreeView;
				if (treeView == null || treeView.IsDisposed)
				{
					return Rectangle.Empty;
				}
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				*(IntPtr*)(&rect.left) = this.Handle;
				if ((int)UnsafeNativeMethods.SendMessage(new HandleRef(treeView, treeView.Handle), 4356, 1, ref rect) == 0)
				{
					return Rectangle.Empty;
				}
				return Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
			}
		}

		// Token: 0x170011B7 RID: 4535
		// (get) Token: 0x0600485A RID: 18522 RVA: 0x00130638 File Offset: 0x0012E838
		internal unsafe Rectangle RowBounds
		{
			get
			{
				TreeView treeView = this.TreeView;
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				*(IntPtr*)(&rect.left) = this.Handle;
				if (treeView == null || treeView.IsDisposed)
				{
					return Rectangle.Empty;
				}
				if ((int)UnsafeNativeMethods.SendMessage(new HandleRef(treeView, treeView.Handle), 4356, 0, ref rect) == 0)
				{
					return Rectangle.Empty;
				}
				return Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
			}
		}

		// Token: 0x170011B8 RID: 4536
		// (get) Token: 0x0600485B RID: 18523 RVA: 0x001306B7 File Offset: 0x0012E8B7
		// (set) Token: 0x0600485C RID: 18524 RVA: 0x001306C5 File Offset: 0x0012E8C5
		internal bool CheckedStateInternal
		{
			get
			{
				return this.treeNodeState[1];
			}
			set
			{
				this.treeNodeState[1] = value;
			}
		}

		// Token: 0x170011B9 RID: 4537
		// (get) Token: 0x0600485D RID: 18525 RVA: 0x001306D4 File Offset: 0x0012E8D4
		// (set) Token: 0x0600485E RID: 18526 RVA: 0x001306DC File Offset: 0x0012E8DC
		internal bool CheckedInternal
		{
			get
			{
				return this.CheckedStateInternal;
			}
			set
			{
				this.CheckedStateInternal = value;
				if (this.handle == IntPtr.Zero)
				{
					return;
				}
				TreeView treeView = this.TreeView;
				if (treeView == null || !treeView.IsHandleCreated || treeView.IsDisposed)
				{
					return;
				}
				NativeMethods.TV_ITEM tv_ITEM = default(NativeMethods.TV_ITEM);
				tv_ITEM.mask = 24;
				tv_ITEM.hItem = this.handle;
				tv_ITEM.stateMask = 61440;
				tv_ITEM.state |= (value ? 8192 : 4096);
				UnsafeNativeMethods.SendMessage(new HandleRef(treeView, treeView.Handle), NativeMethods.TVM_SETITEM, 0, ref tv_ITEM);
			}
		}

		/// <summary>Gets or sets a value indicating whether the tree node is in a checked state.</summary>
		/// <returns>
		///   <see langword="true" /> if the tree node is in a checked state; otherwise, <see langword="false" />.</returns>
		// Token: 0x170011BA RID: 4538
		// (get) Token: 0x0600485F RID: 18527 RVA: 0x0013077C File Offset: 0x0012E97C
		// (set) Token: 0x06004860 RID: 18528 RVA: 0x00130784 File Offset: 0x0012E984
		[SRCategory("CatBehavior")]
		[SRDescription("TreeNodeCheckedDescr")]
		[DefaultValue(false)]
		public bool Checked
		{
			get
			{
				return this.CheckedInternal;
			}
			set
			{
				TreeView treeView = this.TreeView;
				if (treeView != null)
				{
					if (!treeView.TreeViewBeforeCheck(this, TreeViewAction.Unknown))
					{
						this.CheckedInternal = value;
						treeView.TreeViewAfterCheck(this, TreeViewAction.Unknown);
						return;
					}
				}
				else
				{
					this.CheckedInternal = value;
				}
			}
		}

		/// <summary>Gets the shortcut menu that is associated with this tree node.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ContextMenu" /> that is associated with the tree node.</returns>
		// Token: 0x170011BB RID: 4539
		// (get) Token: 0x06004861 RID: 18529 RVA: 0x001307BE File Offset: 0x0012E9BE
		// (set) Token: 0x06004862 RID: 18530 RVA: 0x001307C6 File Offset: 0x0012E9C6
		[SRCategory("CatBehavior")]
		[DefaultValue(null)]
		[SRDescription("ControlContextMenuDescr")]
		public virtual ContextMenu ContextMenu
		{
			get
			{
				return this.contextMenu;
			}
			set
			{
				this.contextMenu = value;
			}
		}

		/// <summary>Gets or sets the shortcut menu associated with this tree node.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> associated with the tree node.</returns>
		// Token: 0x170011BC RID: 4540
		// (get) Token: 0x06004863 RID: 18531 RVA: 0x001307CF File Offset: 0x0012E9CF
		// (set) Token: 0x06004864 RID: 18532 RVA: 0x001307D7 File Offset: 0x0012E9D7
		[SRCategory("CatBehavior")]
		[DefaultValue(null)]
		[SRDescription("ControlContextMenuDescr")]
		public virtual ContextMenuStrip ContextMenuStrip
		{
			get
			{
				return this.contextMenuStrip;
			}
			set
			{
				this.contextMenuStrip = value;
			}
		}

		/// <summary>Gets the first child tree node in the tree node collection.</summary>
		/// <returns>The first child <see cref="T:System.Windows.Forms.TreeNode" /> in the <see cref="P:System.Windows.Forms.TreeNode.Nodes" /> collection.</returns>
		// Token: 0x170011BD RID: 4541
		// (get) Token: 0x06004865 RID: 18533 RVA: 0x001307E0 File Offset: 0x0012E9E0
		[Browsable(false)]
		public TreeNode FirstNode
		{
			get
			{
				if (this.childCount == 0)
				{
					return null;
				}
				return this.children[0];
			}
		}

		// Token: 0x170011BE RID: 4542
		// (get) Token: 0x06004866 RID: 18534 RVA: 0x001307F4 File Offset: 0x0012E9F4
		private TreeNode FirstVisibleParent
		{
			get
			{
				TreeNode treeNode = this;
				while (treeNode != null && treeNode.Bounds.IsEmpty)
				{
					treeNode = treeNode.Parent;
				}
				return treeNode;
			}
		}

		/// <summary>Gets or sets the foreground color of the tree node.</summary>
		/// <returns>The foreground <see cref="T:System.Drawing.Color" /> of the tree node.</returns>
		// Token: 0x170011BF RID: 4543
		// (get) Token: 0x06004867 RID: 18535 RVA: 0x00130820 File Offset: 0x0012EA20
		// (set) Token: 0x06004868 RID: 18536 RVA: 0x0013083C File Offset: 0x0012EA3C
		[SRCategory("CatAppearance")]
		[SRDescription("TreeNodeForeColorDescr")]
		public Color ForeColor
		{
			get
			{
				if (this.propBag == null)
				{
					return Color.Empty;
				}
				return this.propBag.ForeColor;
			}
			set
			{
				Color foreColor = this.ForeColor;
				if (value.IsEmpty)
				{
					if (this.propBag != null)
					{
						this.propBag.ForeColor = Color.Empty;
						this.RemovePropBagIfEmpty();
					}
					if (!foreColor.IsEmpty)
					{
						this.InvalidateHostTree();
					}
					return;
				}
				if (this.propBag == null)
				{
					this.propBag = new OwnerDrawPropertyBag();
				}
				this.propBag.ForeColor = value;
				if (!value.Equals(foreColor))
				{
					this.InvalidateHostTree();
				}
			}
		}

		/// <summary>Gets the path from the root tree node to the current tree node.</summary>
		/// <returns>The path from the root tree node to the current tree node.</returns>
		/// <exception cref="T:System.InvalidOperationException">The node is not contained in a <see cref="T:System.Windows.Forms.TreeView" />.</exception>
		// Token: 0x170011C0 RID: 4544
		// (get) Token: 0x06004869 RID: 18537 RVA: 0x001308C4 File Offset: 0x0012EAC4
		[Browsable(false)]
		public string FullPath
		{
			get
			{
				TreeView treeView = this.TreeView;
				if (treeView != null)
				{
					StringBuilder stringBuilder = new StringBuilder();
					this.GetFullPath(stringBuilder, treeView.PathSeparator);
					return stringBuilder.ToString();
				}
				throw new InvalidOperationException(SR.GetString("TreeNodeNoParent"));
			}
		}

		/// <summary>Gets the handle of the tree node.</summary>
		/// <returns>The tree node handle.</returns>
		// Token: 0x170011C1 RID: 4545
		// (get) Token: 0x0600486A RID: 18538 RVA: 0x00130904 File Offset: 0x0012EB04
		[Browsable(false)]
		public IntPtr Handle
		{
			get
			{
				if (this.handle == IntPtr.Zero)
				{
					this.TreeView.CreateControl();
				}
				return this.handle;
			}
		}

		/// <summary>Gets or sets the image list index value of the image displayed when the tree node is in the unselected state.</summary>
		/// <returns>A zero-based index value that represents the image position in the assigned <see cref="T:System.Windows.Forms.ImageList" />.</returns>
		// Token: 0x170011C2 RID: 4546
		// (get) Token: 0x0600486B RID: 18539 RVA: 0x00130929 File Offset: 0x0012EB29
		// (set) Token: 0x0600486C RID: 18540 RVA: 0x00130936 File Offset: 0x0012EB36
		[Localizable(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("TreeNodeImageIndexDescr")]
		[TypeConverter(typeof(TreeViewImageIndexConverter))]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[RefreshProperties(RefreshProperties.Repaint)]
		[DefaultValue(-1)]
		[RelatedImageList("TreeView.ImageList")]
		public int ImageIndex
		{
			get
			{
				return this.ImageIndexer.Index;
			}
			set
			{
				this.ImageIndexer.Index = value;
				this.UpdateNode(2);
			}
		}

		/// <summary>Gets or sets the key for the image associated with this tree node when the node is in an unselected state.</summary>
		/// <returns>The key for the image associated with this tree node when the node is in an unselected state.</returns>
		// Token: 0x170011C3 RID: 4547
		// (get) Token: 0x0600486D RID: 18541 RVA: 0x0013094B File Offset: 0x0012EB4B
		// (set) Token: 0x0600486E RID: 18542 RVA: 0x00130958 File Offset: 0x0012EB58
		[Localizable(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("TreeNodeImageKeyDescr")]
		[TypeConverter(typeof(TreeViewImageKeyConverter))]
		[DefaultValue("")]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[RefreshProperties(RefreshProperties.Repaint)]
		[RelatedImageList("TreeView.ImageList")]
		public string ImageKey
		{
			get
			{
				return this.ImageIndexer.Key;
			}
			set
			{
				this.ImageIndexer.Key = value;
				this.UpdateNode(2);
			}
		}

		/// <summary>Gets the position of the tree node in the tree node collection.</summary>
		/// <returns>A zero-based index value that represents the position of the tree node in the <see cref="P:System.Windows.Forms.TreeNode.Nodes" /> collection.</returns>
		// Token: 0x170011C4 RID: 4548
		// (get) Token: 0x0600486F RID: 18543 RVA: 0x0013096D File Offset: 0x0012EB6D
		[SRCategory("CatBehavior")]
		[SRDescription("TreeNodeIndexDescr")]
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		/// <summary>Gets a value indicating whether the tree node is in an editable state.</summary>
		/// <returns>
		///   <see langword="true" /> if the tree node is in editable state; otherwise, <see langword="false" />.</returns>
		// Token: 0x170011C5 RID: 4549
		// (get) Token: 0x06004870 RID: 18544 RVA: 0x00130978 File Offset: 0x0012EB78
		[Browsable(false)]
		public bool IsEditing
		{
			get
			{
				TreeView treeView = this.TreeView;
				return treeView != null && treeView.editNode == this;
			}
		}

		/// <summary>Gets a value indicating whether the tree node is in the expanded state.</summary>
		/// <returns>
		///   <see langword="true" /> if the tree node is in the expanded state; otherwise, <see langword="false" />.</returns>
		// Token: 0x170011C6 RID: 4550
		// (get) Token: 0x06004871 RID: 18545 RVA: 0x0013099A File Offset: 0x0012EB9A
		[Browsable(false)]
		public bool IsExpanded
		{
			get
			{
				if (this.handle == IntPtr.Zero)
				{
					return this.expandOnRealization;
				}
				return (this.State & 32) != 0;
			}
		}

		/// <summary>Gets a value indicating whether the tree node is in the selected state.</summary>
		/// <returns>
		///   <see langword="true" /> if the tree node is in the selected state; otherwise, <see langword="false" />.</returns>
		// Token: 0x170011C7 RID: 4551
		// (get) Token: 0x06004872 RID: 18546 RVA: 0x001309C1 File Offset: 0x0012EBC1
		[Browsable(false)]
		public bool IsSelected
		{
			get
			{
				return !(this.handle == IntPtr.Zero) && (this.State & 2) != 0;
			}
		}

		/// <summary>Gets a value indicating whether the tree node is visible or partially visible.</summary>
		/// <returns>
		///   <see langword="true" /> if the tree node is visible or partially visible; otherwise, <see langword="false" />.</returns>
		// Token: 0x170011C8 RID: 4552
		// (get) Token: 0x06004873 RID: 18547 RVA: 0x001309E4 File Offset: 0x0012EBE4
		[Browsable(false)]
		public unsafe bool IsVisible
		{
			get
			{
				if (this.handle == IntPtr.Zero)
				{
					return false;
				}
				TreeView treeView = this.TreeView;
				if (treeView.IsDisposed)
				{
					return false;
				}
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				*(IntPtr*)(&rect.left) = this.Handle;
				bool flag = (int)UnsafeNativeMethods.SendMessage(new HandleRef(treeView, treeView.Handle), 4356, 1, ref rect) != 0;
				if (flag)
				{
					Size clientSize = treeView.ClientSize;
					flag = rect.bottom > 0 && rect.right > 0 && rect.top < clientSize.Height && rect.left < clientSize.Width;
				}
				return flag;
			}
		}

		/// <summary>Gets the last child tree node.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TreeNode" /> that represents the last child tree node.</returns>
		// Token: 0x170011C9 RID: 4553
		// (get) Token: 0x06004874 RID: 18548 RVA: 0x00130A8E File Offset: 0x0012EC8E
		[Browsable(false)]
		public TreeNode LastNode
		{
			get
			{
				if (this.childCount == 0)
				{
					return null;
				}
				return this.children[this.childCount - 1];
			}
		}

		/// <summary>Gets the zero-based depth of the tree node in the <see cref="T:System.Windows.Forms.TreeView" /> control.</summary>
		/// <returns>The zero-based depth of the tree node in the <see cref="T:System.Windows.Forms.TreeView" /> control.</returns>
		// Token: 0x170011CA RID: 4554
		// (get) Token: 0x06004875 RID: 18549 RVA: 0x00130AA9 File Offset: 0x0012ECA9
		[Browsable(false)]
		public int Level
		{
			get
			{
				if (this.Parent == null)
				{
					return 0;
				}
				return this.Parent.Level + 1;
			}
		}

		/// <summary>Gets the next sibling tree node.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TreeNode" /> that represents the next sibling tree node.</returns>
		// Token: 0x170011CB RID: 4555
		// (get) Token: 0x06004876 RID: 18550 RVA: 0x00130AC2 File Offset: 0x0012ECC2
		[Browsable(false)]
		public TreeNode NextNode
		{
			get
			{
				if (this.index + 1 < this.parent.Nodes.Count)
				{
					return this.parent.Nodes[this.index + 1];
				}
				return null;
			}
		}

		/// <summary>Gets the next visible tree node.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TreeNode" /> that represents the next visible tree node.</returns>
		// Token: 0x170011CC RID: 4556
		// (get) Token: 0x06004877 RID: 18551 RVA: 0x00130AF8 File Offset: 0x0012ECF8
		[Browsable(false)]
		public TreeNode NextVisibleNode
		{
			get
			{
				TreeView treeView = this.TreeView;
				if (treeView == null || treeView.IsDisposed)
				{
					return null;
				}
				TreeNode firstVisibleParent = this.FirstVisibleParent;
				if (firstVisibleParent != null)
				{
					IntPtr intPtr = UnsafeNativeMethods.SendMessage(new HandleRef(treeView, treeView.Handle), 4362, 6, firstVisibleParent.Handle);
					if (intPtr != IntPtr.Zero)
					{
						return treeView.NodeFromHandle(intPtr);
					}
				}
				return null;
			}
		}

		/// <summary>Gets or sets the font that is used to display the text on the tree node label.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> that is used to display the text on the tree node label.</returns>
		// Token: 0x170011CD RID: 4557
		// (get) Token: 0x06004878 RID: 18552 RVA: 0x00130B57 File Offset: 0x0012ED57
		// (set) Token: 0x06004879 RID: 18553 RVA: 0x00130B70 File Offset: 0x0012ED70
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("TreeNodeNodeFontDescr")]
		[DefaultValue(null)]
		public Font NodeFont
		{
			get
			{
				if (this.propBag == null)
				{
					return null;
				}
				return this.propBag.Font;
			}
			set
			{
				Font nodeFont = this.NodeFont;
				if (value == null)
				{
					if (this.propBag != null)
					{
						this.propBag.Font = null;
						this.RemovePropBagIfEmpty();
					}
					if (nodeFont != null)
					{
						this.InvalidateHostTree();
					}
					return;
				}
				if (this.propBag == null)
				{
					this.propBag = new OwnerDrawPropertyBag();
				}
				this.propBag.Font = value;
				if (!value.Equals(nodeFont))
				{
					this.InvalidateHostTree();
				}
			}
		}

		/// <summary>Gets the collection of <see cref="T:System.Windows.Forms.TreeNode" /> objects assigned to the current tree node.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TreeNodeCollection" /> that represents the tree nodes assigned to the current tree node.</returns>
		// Token: 0x170011CE RID: 4558
		// (get) Token: 0x0600487A RID: 18554 RVA: 0x00130BD9 File Offset: 0x0012EDD9
		[ListBindable(false)]
		[Browsable(false)]
		public TreeNodeCollection Nodes
		{
			get
			{
				if (this.nodes == null)
				{
					this.nodes = new TreeNodeCollection(this);
				}
				return this.nodes;
			}
		}

		/// <summary>Gets the parent tree node of the current tree node.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TreeNode" /> that represents the parent of the current tree node.</returns>
		// Token: 0x170011CF RID: 4559
		// (get) Token: 0x0600487B RID: 18555 RVA: 0x00130BF8 File Offset: 0x0012EDF8
		[Browsable(false)]
		public TreeNode Parent
		{
			get
			{
				TreeView treeView = this.TreeView;
				if (treeView != null && this.parent == treeView.root)
				{
					return null;
				}
				return this.parent;
			}
		}

		/// <summary>Gets the previous sibling tree node.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TreeNode" /> that represents the previous sibling tree node.</returns>
		// Token: 0x170011D0 RID: 4560
		// (get) Token: 0x0600487C RID: 18556 RVA: 0x00130C28 File Offset: 0x0012EE28
		[Browsable(false)]
		public TreeNode PrevNode
		{
			get
			{
				int num = this.index;
				int fixedIndex = this.parent.Nodes.FixedIndex;
				if (fixedIndex > 0)
				{
					num = fixedIndex;
				}
				if (num > 0 && num <= this.parent.Nodes.Count)
				{
					return this.parent.Nodes[num - 1];
				}
				return null;
			}
		}

		/// <summary>Gets the previous visible tree node.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TreeNode" /> that represents the previous visible tree node.</returns>
		// Token: 0x170011D1 RID: 4561
		// (get) Token: 0x0600487D RID: 18557 RVA: 0x00130C80 File Offset: 0x0012EE80
		[Browsable(false)]
		public TreeNode PrevVisibleNode
		{
			get
			{
				TreeNode firstVisibleParent = this.FirstVisibleParent;
				TreeView treeView = this.TreeView;
				if (firstVisibleParent != null)
				{
					if (treeView == null || treeView.IsDisposed)
					{
						return null;
					}
					IntPtr intPtr = UnsafeNativeMethods.SendMessage(new HandleRef(treeView, treeView.Handle), 4362, 7, firstVisibleParent.Handle);
					if (intPtr != IntPtr.Zero)
					{
						return treeView.NodeFromHandle(intPtr);
					}
				}
				return null;
			}
		}

		/// <summary>Gets or sets the image list index value of the image that is displayed when the tree node is in the selected state.</summary>
		/// <returns>A zero-based index value that represents the image position in an <see cref="T:System.Windows.Forms.ImageList" />.</returns>
		// Token: 0x170011D2 RID: 4562
		// (get) Token: 0x0600487E RID: 18558 RVA: 0x00130CDF File Offset: 0x0012EEDF
		// (set) Token: 0x0600487F RID: 18559 RVA: 0x00130CEC File Offset: 0x0012EEEC
		[Localizable(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("TreeNodeSelectedImageIndexDescr")]
		[TypeConverter(typeof(TreeViewImageIndexConverter))]
		[DefaultValue(-1)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[RelatedImageList("TreeView.ImageList")]
		public int SelectedImageIndex
		{
			get
			{
				return this.SelectedImageIndexer.Index;
			}
			set
			{
				this.SelectedImageIndexer.Index = value;
				this.UpdateNode(32);
			}
		}

		/// <summary>Gets or sets the key of the image displayed in the tree node when it is in a selected state.</summary>
		/// <returns>The key of the image displayed when the tree node is in a selected state.</returns>
		// Token: 0x170011D3 RID: 4563
		// (get) Token: 0x06004880 RID: 18560 RVA: 0x00130D02 File Offset: 0x0012EF02
		// (set) Token: 0x06004881 RID: 18561 RVA: 0x00130D0F File Offset: 0x0012EF0F
		[Localizable(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("TreeNodeSelectedImageKeyDescr")]
		[TypeConverter(typeof(TreeViewImageKeyConverter))]
		[DefaultValue("")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[RelatedImageList("TreeView.ImageList")]
		public string SelectedImageKey
		{
			get
			{
				return this.SelectedImageIndexer.Key;
			}
			set
			{
				this.SelectedImageIndexer.Key = value;
				this.UpdateNode(32);
			}
		}

		// Token: 0x170011D4 RID: 4564
		// (get) Token: 0x06004882 RID: 18562 RVA: 0x00130D28 File Offset: 0x0012EF28
		internal int State
		{
			get
			{
				if (this.handle == IntPtr.Zero)
				{
					return 0;
				}
				TreeView treeView = this.TreeView;
				if (treeView == null || treeView.IsDisposed)
				{
					return 0;
				}
				NativeMethods.TV_ITEM tv_ITEM = default(NativeMethods.TV_ITEM);
				tv_ITEM.hItem = this.Handle;
				tv_ITEM.mask = 24;
				tv_ITEM.stateMask = 34;
				UnsafeNativeMethods.SendMessage(new HandleRef(treeView, treeView.Handle), NativeMethods.TVM_GETITEM, 0, ref tv_ITEM);
				return tv_ITEM.state;
			}
		}

		/// <summary>Gets or sets the key of the image that is used to indicate the state of the <see cref="T:System.Windows.Forms.TreeNode" /> when the parent <see cref="T:System.Windows.Forms.TreeView" /> has its <see cref="P:System.Windows.Forms.TreeView.CheckBoxes" /> property set to <see langword="false" />.</summary>
		/// <returns>The key of the image that is used to indicate the state of the <see cref="T:System.Windows.Forms.TreeNode" />.</returns>
		// Token: 0x170011D5 RID: 4565
		// (get) Token: 0x06004883 RID: 18563 RVA: 0x00130DA4 File Offset: 0x0012EFA4
		// (set) Token: 0x06004884 RID: 18564 RVA: 0x00130DB1 File Offset: 0x0012EFB1
		[Localizable(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("TreeNodeStateImageKeyDescr")]
		[TypeConverter(typeof(ImageKeyConverter))]
		[DefaultValue("")]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[RefreshProperties(RefreshProperties.Repaint)]
		[RelatedImageList("TreeView.StateImageList")]
		public string StateImageKey
		{
			get
			{
				return this.StateImageIndexer.Key;
			}
			set
			{
				if (this.StateImageIndexer.Key != value)
				{
					this.StateImageIndexer.Key = value;
					if (this.treeView != null && !this.treeView.CheckBoxes)
					{
						this.UpdateNode(8);
					}
				}
			}
		}

		/// <summary>Gets or sets the index of the image that is used to indicate the state of the <see cref="T:System.Windows.Forms.TreeNode" /> when the parent <see cref="T:System.Windows.Forms.TreeView" /> has its <see cref="P:System.Windows.Forms.TreeView.CheckBoxes" /> property set to <see langword="false" />.</summary>
		/// <returns>The index of the image that is used to indicate the state of the <see cref="T:System.Windows.Forms.TreeNode" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified index is less than -1 or greater than 14.</exception>
		// Token: 0x170011D6 RID: 4566
		// (get) Token: 0x06004885 RID: 18565 RVA: 0x00130DEE File Offset: 0x0012EFEE
		// (set) Token: 0x06004886 RID: 18566 RVA: 0x00130E14 File Offset: 0x0012F014
		[Localizable(true)]
		[TypeConverter(typeof(NoneExcludedImageIndexConverter))]
		[DefaultValue(-1)]
		[SRCategory("CatBehavior")]
		[SRDescription("TreeNodeStateImageIndexDescr")]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[RefreshProperties(RefreshProperties.Repaint)]
		[RelatedImageList("TreeView.StateImageList")]
		public int StateImageIndex
		{
			get
			{
				if (this.treeView != null && this.treeView.StateImageList != null)
				{
					return this.StateImageIndexer.Index;
				}
				return -1;
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
				this.StateImageIndexer.Index = value;
				if (this.treeView != null && !this.treeView.CheckBoxes)
				{
					this.UpdateNode(8);
				}
			}
		}

		/// <summary>Gets or sets the object that contains data about the tree node.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains data about the tree node. The default is <see langword="null" />.</returns>
		// Token: 0x170011D7 RID: 4567
		// (get) Token: 0x06004887 RID: 18567 RVA: 0x00130E84 File Offset: 0x0012F084
		// (set) Token: 0x06004888 RID: 18568 RVA: 0x00130E8C File Offset: 0x0012F08C
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

		/// <summary>Gets or sets the text displayed in the label of the tree node.</summary>
		/// <returns>The text displayed in the label of the tree node.</returns>
		// Token: 0x170011D8 RID: 4568
		// (get) Token: 0x06004889 RID: 18569 RVA: 0x00130E95 File Offset: 0x0012F095
		// (set) Token: 0x0600488A RID: 18570 RVA: 0x00130EAB File Offset: 0x0012F0AB
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("TreeNodeTextDescr")]
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
				this.UpdateNode(1);
			}
		}

		/// <summary>Gets or sets the text that appears when the mouse pointer hovers over a <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
		/// <returns>Gets the text that appears when the mouse pointer hovers over a <see cref="T:System.Windows.Forms.TreeNode" />.</returns>
		// Token: 0x170011D9 RID: 4569
		// (get) Token: 0x0600488B RID: 18571 RVA: 0x00130EBB File Offset: 0x0012F0BB
		// (set) Token: 0x0600488C RID: 18572 RVA: 0x00130EC3 File Offset: 0x0012F0C3
		[Localizable(false)]
		[SRCategory("CatAppearance")]
		[SRDescription("TreeNodeToolTipTextDescr")]
		[DefaultValue("")]
		public string ToolTipText
		{
			get
			{
				return this.toolTipText;
			}
			set
			{
				this.toolTipText = value;
			}
		}

		/// <summary>Gets or sets the name of the tree node.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the name of the tree node.</returns>
		// Token: 0x170011DA RID: 4570
		// (get) Token: 0x0600488D RID: 18573 RVA: 0x00130ECC File Offset: 0x0012F0CC
		// (set) Token: 0x0600488E RID: 18574 RVA: 0x00130EE2 File Offset: 0x0012F0E2
		[SRCategory("CatAppearance")]
		[SRDescription("TreeNodeNodeNameDescr")]
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
			}
		}

		/// <summary>Gets the parent tree view that the tree node is assigned to.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TreeView" /> that represents the parent tree view that the tree node is assigned to, or <see langword="null" /> if the node has not been assigned to a tree view.</returns>
		// Token: 0x170011DB RID: 4571
		// (get) Token: 0x0600488F RID: 18575 RVA: 0x00130EEB File Offset: 0x0012F0EB
		[Browsable(false)]
		public TreeView TreeView
		{
			get
			{
				if (this.treeView == null)
				{
					this.treeView = this.FindTreeView();
				}
				return this.treeView;
			}
		}

		// Token: 0x06004890 RID: 18576 RVA: 0x00130F08 File Offset: 0x0012F108
		internal int AddSorted(TreeNode node)
		{
			int num = 0;
			string text = node.Text;
			TreeView treeView = this.TreeView;
			if (this.childCount > 0)
			{
				if (treeView.TreeViewNodeSorter == null)
				{
					CompareInfo compareInfo = Application.CurrentCulture.CompareInfo;
					if (compareInfo.Compare(this.children[this.childCount - 1].Text, text) <= 0)
					{
						num = this.childCount;
					}
					else
					{
						int i = 0;
						int num2 = this.childCount;
						while (i < num2)
						{
							int num3 = (i + num2) / 2;
							if (compareInfo.Compare(this.children[num3].Text, text) <= 0)
							{
								i = num3 + 1;
							}
							else
							{
								num2 = num3;
							}
						}
						num = i;
					}
				}
				else
				{
					IComparer treeViewNodeSorter = treeView.TreeViewNodeSorter;
					int i = 0;
					int num2 = this.childCount;
					while (i < num2)
					{
						int num3 = (i + num2) / 2;
						if (treeViewNodeSorter.Compare(this.children[num3], node) <= 0)
						{
							i = num3 + 1;
						}
						else
						{
							num2 = num3;
						}
					}
					num = i;
				}
			}
			node.SortChildren(treeView);
			this.InsertNodeAt(num, node);
			return num;
		}

		/// <summary>Returns the tree node with the specified handle and assigned to the specified tree view control.</summary>
		/// <param name="tree">The <see cref="T:System.Windows.Forms.TreeView" /> that contains the tree node.</param>
		/// <param name="handle">The handle of the tree node.</param>
		/// <returns>A <see cref="T:System.Windows.Forms.TreeNode" /> that represents the tree node assigned to the specified <see cref="T:System.Windows.Forms.TreeView" /> control with the specified handle.</returns>
		// Token: 0x06004891 RID: 18577 RVA: 0x00130FF7 File Offset: 0x0012F1F7
		public static TreeNode FromHandle(TreeView tree, IntPtr handle)
		{
			IntSecurity.ControlFromHandleOrLocation.Demand();
			return tree.NodeFromHandle(handle);
		}

		// Token: 0x06004892 RID: 18578 RVA: 0x0013100C File Offset: 0x0012F20C
		private void SortChildren(TreeView parentTreeView)
		{
			if (this.childCount > 0)
			{
				TreeNode[] array = new TreeNode[this.childCount];
				if (parentTreeView == null || parentTreeView.TreeViewNodeSorter == null)
				{
					CompareInfo compareInfo = Application.CurrentCulture.CompareInfo;
					for (int i = 0; i < this.childCount; i++)
					{
						int num = -1;
						for (int j = 0; j < this.childCount; j++)
						{
							if (this.children[j] != null)
							{
								if (num == -1)
								{
									num = j;
								}
								else if (compareInfo.Compare(this.children[j].Text, this.children[num].Text) <= 0)
								{
									num = j;
								}
							}
						}
						array[i] = this.children[num];
						this.children[num] = null;
						array[i].index = i;
						array[i].SortChildren(parentTreeView);
					}
					this.children = array;
					return;
				}
				IComparer treeViewNodeSorter = parentTreeView.TreeViewNodeSorter;
				for (int k = 0; k < this.childCount; k++)
				{
					int num2 = -1;
					for (int l = 0; l < this.childCount; l++)
					{
						if (this.children[l] != null)
						{
							if (num2 == -1)
							{
								num2 = l;
							}
							else if (treeViewNodeSorter.Compare(this.children[l], this.children[num2]) <= 0)
							{
								num2 = l;
							}
						}
					}
					array[k] = this.children[num2];
					this.children[num2] = null;
					array[k].index = k;
					array[k].SortChildren(parentTreeView);
				}
				this.children = array;
			}
		}

		/// <summary>Initiates the editing of the tree node label.</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.TreeView.LabelEdit" /> is set to <see langword="false" />.</exception>
		// Token: 0x06004893 RID: 18579 RVA: 0x00131184 File Offset: 0x0012F384
		public void BeginEdit()
		{
			if (this.handle != IntPtr.Zero)
			{
				TreeView treeView = this.TreeView;
				if (!treeView.LabelEdit)
				{
					throw new InvalidOperationException(SR.GetString("TreeNodeBeginEditFailed"));
				}
				if (!treeView.Focused)
				{
					treeView.FocusInternal();
				}
				UnsafeNativeMethods.SendMessage(new HandleRef(treeView, treeView.Handle), NativeMethods.TVM_EDITLABEL, 0, this.handle);
			}
		}

		// Token: 0x06004894 RID: 18580 RVA: 0x001311F0 File Offset: 0x0012F3F0
		internal void Clear()
		{
			bool flag = false;
			TreeView treeView = this.TreeView;
			try
			{
				if (treeView != null)
				{
					treeView.nodesCollectionClear = true;
					if (treeView != null && this.childCount > 200)
					{
						flag = true;
						treeView.BeginUpdate();
					}
				}
				while (this.childCount > 0)
				{
					this.children[this.childCount - 1].Remove(true);
				}
				this.children = null;
				if (treeView != null && flag)
				{
					treeView.EndUpdate();
				}
			}
			finally
			{
				if (treeView != null)
				{
					treeView.nodesCollectionClear = false;
				}
				this.nodesCleared = true;
			}
		}

		/// <summary>Copies the tree node and the entire subtree rooted at this tree node.</summary>
		/// <returns>The <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.TreeNode" />.</returns>
		// Token: 0x06004895 RID: 18581 RVA: 0x00131284 File Offset: 0x0012F484
		public virtual object Clone()
		{
			Type type = base.GetType();
			TreeNode treeNode;
			if (type == typeof(TreeNode))
			{
				treeNode = new TreeNode(this.text, this.ImageIndexer.Index, this.SelectedImageIndexer.Index);
			}
			else
			{
				treeNode = (TreeNode)Activator.CreateInstance(type);
			}
			treeNode.Text = this.text;
			treeNode.Name = this.name;
			treeNode.ImageIndexer.Index = this.ImageIndexer.Index;
			treeNode.SelectedImageIndexer.Index = this.SelectedImageIndexer.Index;
			treeNode.StateImageIndexer.Index = this.StateImageIndexer.Index;
			treeNode.ToolTipText = this.toolTipText;
			treeNode.ContextMenu = this.contextMenu;
			treeNode.ContextMenuStrip = this.contextMenuStrip;
			if (!string.IsNullOrEmpty(this.ImageIndexer.Key))
			{
				treeNode.ImageIndexer.Key = this.ImageIndexer.Key;
			}
			if (!string.IsNullOrEmpty(this.SelectedImageIndexer.Key))
			{
				treeNode.SelectedImageIndexer.Key = this.SelectedImageIndexer.Key;
			}
			if (!string.IsNullOrEmpty(this.StateImageIndexer.Key))
			{
				treeNode.StateImageIndexer.Key = this.StateImageIndexer.Key;
			}
			if (this.childCount > 0)
			{
				treeNode.children = new TreeNode[this.childCount];
				for (int i = 0; i < this.childCount; i++)
				{
					treeNode.Nodes.Add((TreeNode)this.children[i].Clone());
				}
			}
			if (this.propBag != null)
			{
				treeNode.propBag = OwnerDrawPropertyBag.Copy(this.propBag);
			}
			treeNode.Checked = this.Checked;
			treeNode.Tag = this.Tag;
			return treeNode;
		}

		// Token: 0x06004896 RID: 18582 RVA: 0x00131450 File Offset: 0x0012F650
		private void CollapseInternal(bool ignoreChildren)
		{
			TreeView treeView = this.TreeView;
			bool flag = false;
			this.collapseOnRealization = false;
			this.expandOnRealization = false;
			if (treeView == null || !treeView.IsHandleCreated)
			{
				this.collapseOnRealization = true;
				return;
			}
			if (ignoreChildren)
			{
				this.DoCollapse(treeView);
			}
			else
			{
				if (!ignoreChildren && this.childCount > 0)
				{
					for (int i = 0; i < this.childCount; i++)
					{
						if (treeView.SelectedNode == this.children[i])
						{
							flag = true;
						}
						this.children[i].DoCollapse(treeView);
						this.children[i].Collapse();
					}
				}
				this.DoCollapse(treeView);
			}
			if (flag)
			{
				treeView.SelectedNode = this;
			}
			treeView.Invalidate();
			this.collapseOnRealization = false;
		}

		/// <summary>Collapses the <see cref="T:System.Windows.Forms.TreeNode" /> and optionally collapses its children.</summary>
		/// <param name="ignoreChildren">
		///   <see langword="true" /> to leave the child nodes in their current state; <see langword="false" /> to collapse the child nodes.</param>
		// Token: 0x06004897 RID: 18583 RVA: 0x001314FB File Offset: 0x0012F6FB
		public void Collapse(bool ignoreChildren)
		{
			this.CollapseInternal(ignoreChildren);
		}

		/// <summary>Collapses the tree node.</summary>
		// Token: 0x06004898 RID: 18584 RVA: 0x00131504 File Offset: 0x0012F704
		public void Collapse()
		{
			this.CollapseInternal(false);
		}

		// Token: 0x06004899 RID: 18585 RVA: 0x00131510 File Offset: 0x0012F710
		private void DoCollapse(TreeView tv)
		{
			if ((this.State & 32) != 0)
			{
				TreeViewCancelEventArgs treeViewCancelEventArgs = new TreeViewCancelEventArgs(this, false, TreeViewAction.Collapse);
				tv.OnBeforeCollapse(treeViewCancelEventArgs);
				if (!treeViewCancelEventArgs.Cancel)
				{
					UnsafeNativeMethods.SendMessage(new HandleRef(tv, tv.Handle), 4354, 1, this.Handle);
					tv.OnAfterCollapse(new TreeViewEventArgs(this));
				}
			}
		}

		/// <summary>Loads the state of the <see cref="T:System.Windows.Forms.TreeNode" /> from the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" />.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that describes the <see cref="T:System.Windows.Forms.TreeNode" />.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that indicates the state of the stream during deserialization.</param>
		// Token: 0x0600489A RID: 18586 RVA: 0x0013156C File Offset: 0x0012F76C
		protected virtual void Deserialize(SerializationInfo serializationInfo, StreamingContext context)
		{
			int num = 0;
			int num2 = -1;
			string text = null;
			int num3 = -1;
			string text2 = null;
			int num4 = -1;
			string text3 = null;
			foreach (SerializationEntry serializationEntry in serializationInfo)
			{
				string text4 = serializationEntry.Name;
				uint num5 = <PrivateImplementationDetails>.ComputeStringHash(text4);
				if (num5 <= 1606954993U)
				{
					if (num5 <= 759659912U)
					{
						if (num5 != 266367750U)
						{
							if (num5 != 717129186U)
							{
								if (num5 == 759659912U)
								{
									if (text4 == "SelectedImageKey")
									{
										text2 = serializationInfo.GetString(serializationEntry.Name);
									}
								}
							}
							else if (text4 == "UserData")
							{
								this.userData = serializationEntry.Value;
							}
						}
						else if (text4 == "Name")
						{
							this.Name = serializationInfo.GetString(serializationEntry.Name);
						}
					}
					else if (num5 != 1011358670U)
					{
						if (num5 != 1041509726U)
						{
							if (num5 == 1606954993U)
							{
								if (text4 == "ImageKey")
								{
									text = serializationInfo.GetString(serializationEntry.Name);
								}
							}
						}
						else if (text4 == "Text")
						{
							this.Text = serializationInfo.GetString(serializationEntry.Name);
						}
					}
					else if (text4 == "PropBag")
					{
						this.propBag = (OwnerDrawPropertyBag)serializationInfo.GetValue(serializationEntry.Name, typeof(OwnerDrawPropertyBag));
					}
				}
				else if (num5 <= 2569126364U)
				{
					if (num5 != 2041341998U)
					{
						if (num5 != 2143661137U)
						{
							if (num5 == 2569126364U)
							{
								if (text4 == "ChildCount")
								{
									num = serializationInfo.GetInt32(serializationEntry.Name);
								}
							}
						}
						else if (text4 == "StateImageIndex")
						{
							num4 = serializationInfo.GetInt32(serializationEntry.Name);
						}
					}
					else if (text4 == "ImageIndex")
					{
						num2 = serializationInfo.GetInt32(serializationEntry.Name);
					}
				}
				else if (num5 <= 3441588130U)
				{
					if (num5 != 2606303591U)
					{
						if (num5 == 3441588130U)
						{
							if (text4 == "StateImageKey")
							{
								text3 = serializationInfo.GetString(serializationEntry.Name);
							}
						}
					}
					else if (text4 == "ToolTipText")
					{
						this.ToolTipText = serializationInfo.GetString(serializationEntry.Name);
					}
				}
				else if (num5 != 3693047415U)
				{
					if (num5 == 3931153718U)
					{
						if (text4 == "IsChecked")
						{
							this.CheckedStateInternal = serializationInfo.GetBoolean(serializationEntry.Name);
						}
					}
				}
				else if (text4 == "SelectedImageIndex")
				{
					num3 = serializationInfo.GetInt32(serializationEntry.Name);
				}
			}
			if (text != null)
			{
				this.ImageKey = text;
			}
			else if (num2 != -1)
			{
				this.ImageIndex = num2;
			}
			if (text2 != null)
			{
				this.SelectedImageKey = text2;
			}
			else if (num3 != -1)
			{
				this.SelectedImageIndex = num3;
			}
			if (text3 != null)
			{
				this.StateImageKey = text3;
			}
			else if (num4 != -1)
			{
				this.StateImageIndex = num4;
			}
			if (num > 0)
			{
				TreeNode[] array = new TreeNode[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = (TreeNode)serializationInfo.GetValue("children" + i.ToString(), typeof(TreeNode));
				}
				this.Nodes.AddRange(array);
			}
		}

		/// <summary>Ends the editing of the tree node label.</summary>
		/// <param name="cancel">
		///   <see langword="true" /> if the editing of the tree node label text was canceled without being saved; otherwise, <see langword="false" />.</param>
		// Token: 0x0600489B RID: 18587 RVA: 0x00131958 File Offset: 0x0012FB58
		public void EndEdit(bool cancel)
		{
			TreeView treeView = this.TreeView;
			if (treeView == null || treeView.IsDisposed)
			{
				return;
			}
			UnsafeNativeMethods.SendMessage(new HandleRef(treeView, treeView.Handle), 4374, cancel ? 1 : 0, 0);
		}

		// Token: 0x0600489C RID: 18588 RVA: 0x00131998 File Offset: 0x0012FB98
		internal void EnsureCapacity(int num)
		{
			int num2 = num;
			if (num2 < 4)
			{
				num2 = 4;
			}
			if (this.children == null)
			{
				this.children = new TreeNode[num2];
				return;
			}
			if (this.childCount + num > this.children.Length)
			{
				int num3 = this.childCount + num;
				if (num == 1)
				{
					num3 = this.childCount * 2;
				}
				TreeNode[] array = new TreeNode[num3];
				Array.Copy(this.children, 0, array, 0, this.childCount);
				this.children = array;
			}
		}

		// Token: 0x0600489D RID: 18589 RVA: 0x00131A0C File Offset: 0x0012FC0C
		private void EnsureStateImageValue()
		{
			if (this.treeView == null)
			{
				return;
			}
			if (this.treeView.CheckBoxes && this.treeView.StateImageList != null)
			{
				if (!string.IsNullOrEmpty(this.StateImageKey))
				{
					this.StateImageIndex = (this.Checked ? 1 : 0);
					this.StateImageKey = this.treeView.StateImageList.Images.Keys[this.StateImageIndex];
					return;
				}
				this.StateImageIndex = (this.Checked ? 1 : 0);
			}
		}

		/// <summary>Ensures that the tree node is visible, expanding tree nodes and scrolling the tree view control as necessary.</summary>
		// Token: 0x0600489E RID: 18590 RVA: 0x00131A94 File Offset: 0x0012FC94
		public void EnsureVisible()
		{
			TreeView treeView = this.TreeView;
			if (treeView == null || treeView.IsDisposed)
			{
				return;
			}
			UnsafeNativeMethods.SendMessage(new HandleRef(treeView, treeView.Handle), 4372, 0, this.Handle);
		}

		/// <summary>Expands the tree node.</summary>
		// Token: 0x0600489F RID: 18591 RVA: 0x00131AD4 File Offset: 0x0012FCD4
		public void Expand()
		{
			TreeView treeView = this.TreeView;
			if (treeView == null || !treeView.IsHandleCreated)
			{
				this.expandOnRealization = true;
				return;
			}
			this.ResetExpandedState(treeView);
			if (!this.IsExpanded)
			{
				UnsafeNativeMethods.SendMessage(new HandleRef(treeView, treeView.Handle), 4354, 2, this.Handle);
			}
			this.expandOnRealization = false;
		}

		/// <summary>Expands all the child tree nodes.</summary>
		// Token: 0x060048A0 RID: 18592 RVA: 0x00131B30 File Offset: 0x0012FD30
		public void ExpandAll()
		{
			this.Expand();
			for (int i = 0; i < this.childCount; i++)
			{
				this.children[i].ExpandAll();
			}
		}

		// Token: 0x060048A1 RID: 18593 RVA: 0x00131B64 File Offset: 0x0012FD64
		internal TreeView FindTreeView()
		{
			TreeNode treeNode = this;
			while (treeNode.parent != null)
			{
				treeNode = treeNode.parent;
			}
			return treeNode.treeView;
		}

		// Token: 0x060048A2 RID: 18594 RVA: 0x00131B8A File Offset: 0x0012FD8A
		private void GetFullPath(StringBuilder path, string pathSeparator)
		{
			if (this.parent != null)
			{
				this.parent.GetFullPath(path, pathSeparator);
				if (this.parent.parent != null)
				{
					path.Append(pathSeparator);
				}
				path.Append(this.text);
			}
		}

		/// <summary>Returns the number of child tree nodes.</summary>
		/// <param name="includeSubTrees">
		///   <see langword="true" /> if the resulting count includes all tree nodes indirectly rooted at this tree node; otherwise, <see langword="false" />.</param>
		/// <returns>The number of child tree nodes assigned to the <see cref="P:System.Windows.Forms.TreeNode.Nodes" /> collection.</returns>
		// Token: 0x060048A3 RID: 18595 RVA: 0x00131BC4 File Offset: 0x0012FDC4
		public int GetNodeCount(bool includeSubTrees)
		{
			int num = this.childCount;
			if (includeSubTrees)
			{
				for (int i = 0; i < this.childCount; i++)
				{
					num += this.children[i].GetNodeCount(true);
				}
			}
			return num;
		}

		// Token: 0x060048A4 RID: 18596 RVA: 0x00131C00 File Offset: 0x0012FE00
		internal void InsertNodeAt(int index, TreeNode node)
		{
			this.EnsureCapacity(1);
			node.parent = this;
			node.index = index;
			for (int i = this.childCount; i > index; i--)
			{
				(this.children[i] = this.children[i - 1]).index = i;
			}
			this.children[index] = node;
			this.childCount++;
			node.Realize(false);
			if (this.TreeView != null && node == this.TreeView.selectedNode)
			{
				this.TreeView.SelectedNode = node;
			}
		}

		// Token: 0x060048A5 RID: 18597 RVA: 0x00131C8E File Offset: 0x0012FE8E
		private void InvalidateHostTree()
		{
			if (this.treeView != null && this.treeView.IsHandleCreated)
			{
				this.treeView.Invalidate();
			}
		}

		// Token: 0x060048A6 RID: 18598 RVA: 0x00131CB0 File Offset: 0x0012FEB0
		internal void Realize(bool insertFirst)
		{
			TreeView treeView = this.TreeView;
			if (treeView == null || !treeView.IsHandleCreated || treeView.IsDisposed)
			{
				return;
			}
			if (this.parent != null)
			{
				if (treeView.InvokeRequired)
				{
					throw new InvalidOperationException(SR.GetString("InvalidCrossThreadControlCall"));
				}
				NativeMethods.TV_INSERTSTRUCT tv_INSERTSTRUCT = default(NativeMethods.TV_INSERTSTRUCT);
				tv_INSERTSTRUCT.item_mask = TreeNode.insertMask;
				tv_INSERTSTRUCT.hParent = this.parent.handle;
				TreeNode prevNode = this.PrevNode;
				if (insertFirst || prevNode == null)
				{
					tv_INSERTSTRUCT.hInsertAfter = (IntPtr)(-65535);
				}
				else
				{
					tv_INSERTSTRUCT.hInsertAfter = prevNode.handle;
				}
				tv_INSERTSTRUCT.item_pszText = Marshal.StringToHGlobalAuto(this.text);
				tv_INSERTSTRUCT.item_iImage = ((this.ImageIndexer.ActualIndex == -1) ? treeView.ImageIndexer.ActualIndex : this.ImageIndexer.ActualIndex);
				tv_INSERTSTRUCT.item_iSelectedImage = ((this.SelectedImageIndexer.ActualIndex == -1) ? treeView.SelectedImageIndexer.ActualIndex : this.SelectedImageIndexer.ActualIndex);
				tv_INSERTSTRUCT.item_mask = 1;
				tv_INSERTSTRUCT.item_stateMask = 0;
				tv_INSERTSTRUCT.item_state = 0;
				if (treeView.CheckBoxes)
				{
					tv_INSERTSTRUCT.item_mask |= 8;
					tv_INSERTSTRUCT.item_stateMask |= 61440;
					tv_INSERTSTRUCT.item_state |= (this.CheckedInternal ? 8192 : 4096);
				}
				else if (treeView.StateImageList != null && this.StateImageIndexer.ActualIndex >= 0)
				{
					tv_INSERTSTRUCT.item_mask |= 8;
					tv_INSERTSTRUCT.item_stateMask = 61440;
					tv_INSERTSTRUCT.item_state = this.StateImageIndexer.ActualIndex + 1 << 12;
				}
				if (tv_INSERTSTRUCT.item_iImage >= 0)
				{
					tv_INSERTSTRUCT.item_mask |= 2;
				}
				if (tv_INSERTSTRUCT.item_iSelectedImage >= 0)
				{
					tv_INSERTSTRUCT.item_mask |= 32;
				}
				bool flag = false;
				IntPtr intPtr = UnsafeNativeMethods.SendMessage(new HandleRef(treeView, treeView.Handle), 4367, 0, 0);
				if (intPtr != IntPtr.Zero)
				{
					flag = true;
					UnsafeNativeMethods.SendMessage(new HandleRef(treeView, treeView.Handle), 4374, 0, 0);
				}
				this.handle = UnsafeNativeMethods.SendMessage(new HandleRef(treeView, treeView.Handle), NativeMethods.TVM_INSERTITEM, 0, ref tv_INSERTSTRUCT);
				treeView.nodeTable[this.handle] = this;
				this.UpdateNode(4);
				Marshal.FreeHGlobal(tv_INSERTSTRUCT.item_pszText);
				if (flag)
				{
					UnsafeNativeMethods.PostMessage(new HandleRef(treeView, treeView.Handle), NativeMethods.TVM_EDITLABEL, IntPtr.Zero, this.handle);
				}
				SafeNativeMethods.InvalidateRect(new HandleRef(treeView, treeView.Handle), null, false);
				if (this.parent.nodesCleared && (insertFirst || prevNode == null) && !treeView.Scrollable)
				{
					UnsafeNativeMethods.SendMessage(new HandleRef(treeView, treeView.Handle), 11, 1, 0);
					this.nodesCleared = false;
				}
			}
			for (int i = this.childCount - 1; i >= 0; i--)
			{
				this.children[i].Realize(true);
			}
			if (this.expandOnRealization)
			{
				this.Expand();
			}
			if (this.collapseOnRealization)
			{
				this.Collapse();
			}
		}

		/// <summary>Removes the current tree node from the tree view control.</summary>
		// Token: 0x060048A7 RID: 18599 RVA: 0x00131FC7 File Offset: 0x001301C7
		public void Remove()
		{
			this.Remove(true);
		}

		// Token: 0x060048A8 RID: 18600 RVA: 0x00131FD0 File Offset: 0x001301D0
		internal void Remove(bool notify)
		{
			bool isExpanded = this.IsExpanded;
			for (int i = 0; i < this.childCount; i++)
			{
				this.children[i].Remove(false);
			}
			if (notify && this.parent != null)
			{
				for (int j = this.index; j < this.parent.childCount - 1; j++)
				{
					(this.parent.children[j] = this.parent.children[j + 1]).index = j;
				}
				this.parent.children[this.parent.childCount - 1] = null;
				this.parent.childCount--;
				this.parent = null;
			}
			this.expandOnRealization = isExpanded;
			TreeView treeView = this.TreeView;
			if (treeView == null || treeView.IsDisposed)
			{
				return;
			}
			if (this.handle != IntPtr.Zero)
			{
				if (notify && treeView.IsHandleCreated)
				{
					UnsafeNativeMethods.SendMessage(new HandleRef(treeView, treeView.Handle), 4353, 0, this.handle);
				}
				this.treeView.nodeTable.Remove(this.handle);
				this.handle = IntPtr.Zero;
			}
			this.treeView = null;
		}

		// Token: 0x060048A9 RID: 18601 RVA: 0x0013210B File Offset: 0x0013030B
		private void RemovePropBagIfEmpty()
		{
			if (this.propBag == null)
			{
				return;
			}
			if (this.propBag.IsEmpty())
			{
				this.propBag = null;
			}
		}

		// Token: 0x060048AA RID: 18602 RVA: 0x0013212C File Offset: 0x0013032C
		private void ResetExpandedState(TreeView tv)
		{
			NativeMethods.TV_ITEM tv_ITEM = default(NativeMethods.TV_ITEM);
			tv_ITEM.mask = 24;
			tv_ITEM.hItem = this.handle;
			tv_ITEM.stateMask = 64;
			tv_ITEM.state = 0;
			UnsafeNativeMethods.SendMessage(new HandleRef(tv, tv.Handle), NativeMethods.TVM_SETITEM, 0, ref tv_ITEM);
		}

		// Token: 0x060048AB RID: 18603 RVA: 0x00132182 File Offset: 0x00130382
		private bool ShouldSerializeBackColor()
		{
			return this.BackColor != Color.Empty;
		}

		// Token: 0x060048AC RID: 18604 RVA: 0x00132194 File Offset: 0x00130394
		private bool ShouldSerializeForeColor()
		{
			return this.ForeColor != Color.Empty;
		}

		/// <summary>Saves the state of the <see cref="T:System.Windows.Forms.TreeNode" /> to the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" />.</summary>
		/// <param name="si">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that describes the <see cref="T:System.Windows.Forms.TreeNode" />.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that indicates the state of the stream during serialization</param>
		// Token: 0x060048AD RID: 18605 RVA: 0x001321A8 File Offset: 0x001303A8
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		protected virtual void Serialize(SerializationInfo si, StreamingContext context)
		{
			if (this.propBag != null)
			{
				si.AddValue("PropBag", this.propBag, typeof(OwnerDrawPropertyBag));
			}
			si.AddValue("Text", this.text);
			si.AddValue("ToolTipText", this.toolTipText);
			si.AddValue("Name", this.Name);
			si.AddValue("IsChecked", this.treeNodeState[1]);
			si.AddValue("ImageIndex", this.ImageIndexer.Index);
			si.AddValue("ImageKey", this.ImageIndexer.Key);
			si.AddValue("SelectedImageIndex", this.SelectedImageIndexer.Index);
			si.AddValue("SelectedImageKey", this.SelectedImageIndexer.Key);
			if (this.treeView != null && this.treeView.StateImageList != null)
			{
				si.AddValue("StateImageIndex", this.StateImageIndexer.Index);
			}
			if (this.treeView != null && this.treeView.StateImageList != null)
			{
				si.AddValue("StateImageKey", this.StateImageIndexer.Key);
			}
			si.AddValue("ChildCount", this.childCount);
			if (this.childCount > 0)
			{
				for (int i = 0; i < this.childCount; i++)
				{
					si.AddValue("children" + i.ToString(), this.children[i], typeof(TreeNode));
				}
			}
			if (this.userData != null && this.userData.GetType().IsSerializable)
			{
				si.AddValue("UserData", this.userData, this.userData.GetType());
			}
		}

		/// <summary>Toggles the tree node to either the expanded or collapsed state.</summary>
		// Token: 0x060048AE RID: 18606 RVA: 0x0013235A File Offset: 0x0013055A
		public void Toggle()
		{
			if (this.IsExpanded)
			{
				this.Collapse();
				return;
			}
			this.Expand();
		}

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		// Token: 0x060048AF RID: 18607 RVA: 0x00132371 File Offset: 0x00130571
		public override string ToString()
		{
			return "TreeNode: " + ((this.text == null) ? "" : this.text);
		}

		// Token: 0x060048B0 RID: 18608 RVA: 0x00132394 File Offset: 0x00130594
		private void UpdateNode(int mask)
		{
			if (this.handle == IntPtr.Zero)
			{
				return;
			}
			TreeView treeView = this.TreeView;
			NativeMethods.TV_ITEM tv_ITEM = default(NativeMethods.TV_ITEM);
			tv_ITEM.mask = 16 | mask;
			tv_ITEM.hItem = this.handle;
			if ((mask & 1) != 0)
			{
				tv_ITEM.pszText = Marshal.StringToHGlobalAuto(this.text);
			}
			if ((mask & 2) != 0)
			{
				tv_ITEM.iImage = ((this.ImageIndexer.ActualIndex == -1) ? treeView.ImageIndexer.ActualIndex : this.ImageIndexer.ActualIndex);
			}
			if ((mask & 32) != 0)
			{
				tv_ITEM.iSelectedImage = ((this.SelectedImageIndexer.ActualIndex == -1) ? treeView.SelectedImageIndexer.ActualIndex : this.SelectedImageIndexer.ActualIndex);
			}
			if ((mask & 8) != 0)
			{
				tv_ITEM.stateMask = 61440;
				if (this.StateImageIndexer.ActualIndex != -1)
				{
					tv_ITEM.state = this.StateImageIndexer.ActualIndex + 1 << 12;
				}
			}
			if ((mask & 4) != 0)
			{
				tv_ITEM.lParam = this.handle;
			}
			UnsafeNativeMethods.SendMessage(new HandleRef(treeView, treeView.Handle), NativeMethods.TVM_SETITEM, 0, ref tv_ITEM);
			if ((mask & 1) != 0)
			{
				Marshal.FreeHGlobal(tv_ITEM.pszText);
				if (treeView.Scrollable)
				{
					treeView.ForceScrollbarUpdate(false);
				}
			}
		}

		// Token: 0x060048B1 RID: 18609 RVA: 0x001324D8 File Offset: 0x001306D8
		internal void UpdateImage()
		{
			TreeView treeView = this.TreeView;
			if (treeView.IsDisposed)
			{
				return;
			}
			NativeMethods.TV_ITEM tv_ITEM = default(NativeMethods.TV_ITEM);
			tv_ITEM.mask = 18;
			tv_ITEM.hItem = this.Handle;
			tv_ITEM.iImage = Math.Max(0, (this.ImageIndexer.ActualIndex >= treeView.ImageList.Images.Count) ? (treeView.ImageList.Images.Count - 1) : this.ImageIndexer.ActualIndex);
			UnsafeNativeMethods.SendMessage(new HandleRef(treeView, treeView.Handle), NativeMethods.TVM_SETITEM, 0, ref tv_ITEM);
		}

		/// <summary>Populates a serialization information object with the data needed to serialize the <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
		/// <param name="si">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains the data to serialize the <see cref="T:System.Windows.Forms.TreeNode" />.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the destination information for this serialization.</param>
		// Token: 0x060048B2 RID: 18610 RVA: 0x00132576 File Offset: 0x00130776
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			this.Serialize(si, context);
		}

		// Token: 0x0400271C RID: 10012
		private const int SHIFTVAL = 12;

		// Token: 0x0400271D RID: 10013
		private const int CHECKED = 8192;

		// Token: 0x0400271E RID: 10014
		private const int UNCHECKED = 4096;

		// Token: 0x0400271F RID: 10015
		private const int ALLOWEDIMAGES = 14;

		// Token: 0x04002720 RID: 10016
		internal const int MAX_TREENODES_OPS = 200;

		// Token: 0x04002721 RID: 10017
		internal OwnerDrawPropertyBag propBag;

		// Token: 0x04002722 RID: 10018
		internal IntPtr handle;

		// Token: 0x04002723 RID: 10019
		internal string text;

		// Token: 0x04002724 RID: 10020
		internal string name;

		// Token: 0x04002725 RID: 10021
		private const int TREENODESTATE_isChecked = 1;

		// Token: 0x04002726 RID: 10022
		private BitVector32 treeNodeState;

		// Token: 0x04002727 RID: 10023
		private TreeNode.TreeNodeImageIndexer imageIndexer;

		// Token: 0x04002728 RID: 10024
		private TreeNode.TreeNodeImageIndexer selectedImageIndexer;

		// Token: 0x04002729 RID: 10025
		private TreeNode.TreeNodeImageIndexer stateImageIndexer;

		// Token: 0x0400272A RID: 10026
		private string toolTipText = "";

		// Token: 0x0400272B RID: 10027
		private ContextMenu contextMenu;

		// Token: 0x0400272C RID: 10028
		private ContextMenuStrip contextMenuStrip;

		// Token: 0x0400272D RID: 10029
		internal bool nodesCleared;

		// Token: 0x0400272E RID: 10030
		internal int index;

		// Token: 0x0400272F RID: 10031
		internal int childCount;

		// Token: 0x04002730 RID: 10032
		internal TreeNode[] children;

		// Token: 0x04002731 RID: 10033
		internal TreeNode parent;

		// Token: 0x04002732 RID: 10034
		internal TreeView treeView;

		// Token: 0x04002733 RID: 10035
		private bool expandOnRealization;

		// Token: 0x04002734 RID: 10036
		private bool collapseOnRealization;

		// Token: 0x04002735 RID: 10037
		private TreeNodeCollection nodes;

		// Token: 0x04002736 RID: 10038
		private object userData;

		// Token: 0x04002737 RID: 10039
		private static readonly int insertMask = 35;

		// Token: 0x02000824 RID: 2084
		internal class TreeNodeImageIndexer : ImageList.Indexer
		{
			// Token: 0x06006FCF RID: 28623 RVA: 0x00199C79 File Offset: 0x00197E79
			public TreeNodeImageIndexer(TreeNode node, TreeNode.TreeNodeImageIndexer.ImageListType imageListType)
			{
				this.owner = node;
				this.imageListType = imageListType;
			}

			// Token: 0x1700187A RID: 6266
			// (get) Token: 0x06006FD0 RID: 28624 RVA: 0x00199C8F File Offset: 0x00197E8F
			// (set) Token: 0x06006FD1 RID: 28625 RVA: 0x000070A6 File Offset: 0x000052A6
			public override ImageList ImageList
			{
				get
				{
					if (this.owner.TreeView == null)
					{
						return null;
					}
					if (this.imageListType == TreeNode.TreeNodeImageIndexer.ImageListType.State)
					{
						return this.owner.TreeView.StateImageList;
					}
					return this.owner.TreeView.ImageList;
				}
				set
				{
				}
			}

			// Token: 0x04004337 RID: 17207
			private TreeNode owner;

			// Token: 0x04004338 RID: 17208
			private TreeNode.TreeNodeImageIndexer.ImageListType imageListType;

			// Token: 0x020008CF RID: 2255
			public enum ImageListType
			{
				// Token: 0x0400455E RID: 17758
				Default,
				// Token: 0x0400455F RID: 17759
				State
			}
		}
	}
}
