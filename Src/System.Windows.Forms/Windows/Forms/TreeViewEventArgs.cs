using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.TreeView.AfterCheck" />, <see cref="E:System.Windows.Forms.TreeView.AfterCollapse" />, <see cref="E:System.Windows.Forms.TreeView.AfterExpand" />, or <see cref="E:System.Windows.Forms.TreeView.AfterSelect" /> events of a <see cref="T:System.Windows.Forms.TreeView" /> control.</summary>
	// Token: 0x0200041B RID: 1051
	public class TreeViewEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeViewEventArgs" /> class for the specified tree node.</summary>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> that the event is responding to.</param>
		// Token: 0x060049C1 RID: 18881 RVA: 0x00136C5B File Offset: 0x00134E5B
		public TreeViewEventArgs(TreeNode node)
		{
			this.node = node;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeViewEventArgs" /> class for the specified tree node and with the specified type of action that raised the event.</summary>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> that the event is responding to.</param>
		/// <param name="action">The type of <see cref="T:System.Windows.Forms.TreeViewAction" /> that raised the event.</param>
		// Token: 0x060049C2 RID: 18882 RVA: 0x00136C6A File Offset: 0x00134E6A
		public TreeViewEventArgs(TreeNode node, TreeViewAction action)
		{
			this.node = node;
			this.action = action;
		}

		/// <summary>Gets the tree node that has been checked, expanded, collapsed, or selected.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> that has been checked, expanded, collapsed, or selected.</returns>
		// Token: 0x17001212 RID: 4626
		// (get) Token: 0x060049C3 RID: 18883 RVA: 0x00136C80 File Offset: 0x00134E80
		public TreeNode Node
		{
			get
			{
				return this.node;
			}
		}

		/// <summary>Gets the type of action that raised the event.</summary>
		/// <returns>The type of <see cref="T:System.Windows.Forms.TreeViewAction" /> that raised the event.</returns>
		// Token: 0x17001213 RID: 4627
		// (get) Token: 0x060049C4 RID: 18884 RVA: 0x00136C88 File Offset: 0x00134E88
		public TreeViewAction Action
		{
			get
			{
				return this.action;
			}
		}

		// Token: 0x04002794 RID: 10132
		private TreeNode node;

		// Token: 0x04002795 RID: 10133
		private TreeViewAction action;
	}
}
