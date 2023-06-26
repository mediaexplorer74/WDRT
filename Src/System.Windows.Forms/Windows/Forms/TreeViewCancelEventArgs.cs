using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.TreeView.BeforeCheck" />, <see cref="E:System.Windows.Forms.TreeView.BeforeCollapse" />, <see cref="E:System.Windows.Forms.TreeView.BeforeExpand" />, and <see cref="E:System.Windows.Forms.TreeView.BeforeSelect" /> events of a <see cref="T:System.Windows.Forms.TreeView" /> control.</summary>
	// Token: 0x02000418 RID: 1048
	public class TreeViewCancelEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeViewCancelEventArgs" /> class with the specified tree node, a value specifying whether the event is to be canceled, and the type of tree view action that raised the event.</summary>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> that the event is responding to.</param>
		/// <param name="cancel">
		///   <see langword="true" /> to cancel the event; otherwise, <see langword="false" />.</param>
		/// <param name="action">One of the <see cref="T:System.Windows.Forms.TreeViewAction" /> values indicating the type of action that raised the event.</param>
		// Token: 0x060049BA RID: 18874 RVA: 0x00136C34 File Offset: 0x00134E34
		public TreeViewCancelEventArgs(TreeNode node, bool cancel, TreeViewAction action)
			: base(cancel)
		{
			this.node = node;
			this.action = action;
		}

		/// <summary>Gets the tree node to be checked, expanded, collapsed, or selected.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> to be checked, expanded, collapsed, or selected.</returns>
		// Token: 0x17001210 RID: 4624
		// (get) Token: 0x060049BB RID: 18875 RVA: 0x00136C4B File Offset: 0x00134E4B
		public TreeNode Node
		{
			get
			{
				return this.node;
			}
		}

		/// <summary>Gets the type of <see cref="T:System.Windows.Forms.TreeView" /> action that raised the event.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.TreeViewAction" /> values.</returns>
		// Token: 0x17001211 RID: 4625
		// (get) Token: 0x060049BC RID: 18876 RVA: 0x00136C53 File Offset: 0x00134E53
		public TreeViewAction Action
		{
			get
			{
				return this.action;
			}
		}

		// Token: 0x0400278E RID: 10126
		private TreeNode node;

		// Token: 0x0400278F RID: 10127
		private TreeViewAction action;
	}
}
