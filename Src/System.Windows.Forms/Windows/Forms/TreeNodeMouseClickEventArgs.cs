using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.TreeView.NodeMouseClick" /> and <see cref="E:System.Windows.Forms.TreeView.NodeMouseDoubleClick" /> events.</summary>
	// Token: 0x0200040F RID: 1039
	public class TreeNodeMouseClickEventArgs : MouseEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeNodeMouseClickEventArgs" /> class.</summary>
		/// <param name="node">The node that was clicked.</param>
		/// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> members.</param>
		/// <param name="clicks">The number of clicks that occurred.</param>
		/// <param name="x">The x-coordinate where the click occurred.</param>
		/// <param name="y">The y-coordinate where the click occurred.</param>
		// Token: 0x060048B4 RID: 18612 RVA: 0x00132589 File Offset: 0x00130789
		public TreeNodeMouseClickEventArgs(TreeNode node, MouseButtons button, int clicks, int x, int y)
			: base(button, clicks, x, y, 0)
		{
			this.node = node;
		}

		/// <summary>Gets the node that was clicked.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> that was clicked.</returns>
		// Token: 0x170011DC RID: 4572
		// (get) Token: 0x060048B5 RID: 18613 RVA: 0x0013259F File Offset: 0x0013079F
		public TreeNode Node
		{
			get
			{
				return this.node;
			}
		}

		// Token: 0x04002738 RID: 10040
		private TreeNode node;
	}
}
