using System;

namespace System.Windows.Forms
{
	/// <summary>Contains information about an area of a <see cref="T:System.Windows.Forms.TreeView" /> control or a <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
	// Token: 0x0200041D RID: 1053
	public class TreeViewHitTestInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeViewHitTestInfo" /> class.</summary>
		/// <param name="hitNode">The tree node located at the position indicated by the hit test.</param>
		/// <param name="hitLocation">One of the <see cref="T:System.Windows.Forms.TreeViewHitTestLocations" /> values.</param>
		// Token: 0x060049C9 RID: 18889 RVA: 0x00136C90 File Offset: 0x00134E90
		public TreeViewHitTestInfo(TreeNode hitNode, TreeViewHitTestLocations hitLocation)
		{
			this.node = hitNode;
			this.loc = hitLocation;
		}

		/// <summary>Gets the location of a hit test on a <see cref="T:System.Windows.Forms.TreeView" /> control, in relation to the <see cref="T:System.Windows.Forms.TreeView" /> and the nodes it contains.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.TreeViewHitTestLocations" /> values.</returns>
		// Token: 0x17001214 RID: 4628
		// (get) Token: 0x060049CA RID: 18890 RVA: 0x00136CA6 File Offset: 0x00134EA6
		public TreeViewHitTestLocations Location
		{
			get
			{
				return this.loc;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.TreeNode" /> at the position indicated by a hit test of a <see cref="T:System.Windows.Forms.TreeView" /> control.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> at the position indicated by a hit test of a <see cref="T:System.Windows.Forms.TreeView" /> control.</returns>
		// Token: 0x17001215 RID: 4629
		// (get) Token: 0x060049CB RID: 18891 RVA: 0x00136CAE File Offset: 0x00134EAE
		public TreeNode Node
		{
			get
			{
				return this.node;
			}
		}

		// Token: 0x04002796 RID: 10134
		private TreeViewHitTestLocations loc;

		// Token: 0x04002797 RID: 10135
		private TreeNode node;
	}
}
