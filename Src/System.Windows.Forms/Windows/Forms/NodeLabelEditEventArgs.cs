using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.TreeView.BeforeLabelEdit" /> and <see cref="E:System.Windows.Forms.TreeView.AfterLabelEdit" /> events.</summary>
	// Token: 0x02000308 RID: 776
	public class NodeLabelEditEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.NodeLabelEditEventArgs" /> class for the specified <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
		/// <param name="node">The tree node containing the text to edit.</param>
		// Token: 0x06003169 RID: 12649 RVA: 0x000DF612 File Offset: 0x000DD812
		public NodeLabelEditEventArgs(TreeNode node)
		{
			this.node = node;
			this.label = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.NodeLabelEditEventArgs" /> class for the specified <see cref="T:System.Windows.Forms.TreeNode" /> and the specified text with which to update the tree node label.</summary>
		/// <param name="node">The tree node containing the text to edit.</param>
		/// <param name="label">The new text to associate with the tree node.</param>
		// Token: 0x0600316A RID: 12650 RVA: 0x000DF628 File Offset: 0x000DD828
		public NodeLabelEditEventArgs(TreeNode node, string label)
		{
			this.node = node;
			this.label = label;
		}

		/// <summary>Gets or sets a value indicating whether the edit has been canceled.</summary>
		/// <returns>
		///   <see langword="true" /> if the edit has been canceled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x0600316B RID: 12651 RVA: 0x000DF63E File Offset: 0x000DD83E
		// (set) Token: 0x0600316C RID: 12652 RVA: 0x000DF646 File Offset: 0x000DD846
		public bool CancelEdit
		{
			get
			{
				return this.cancelEdit;
			}
			set
			{
				this.cancelEdit = value;
			}
		}

		/// <summary>Gets the new text to associate with the tree node.</summary>
		/// <returns>The string value that represents the new <see cref="T:System.Windows.Forms.TreeNode" /> label or <see langword="null" /> if the user cancels the edit.</returns>
		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x0600316D RID: 12653 RVA: 0x000DF64F File Offset: 0x000DD84F
		public string Label
		{
			get
			{
				return this.label;
			}
		}

		/// <summary>Gets the tree node containing the text to edit.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TreeNode" /> that represents the tree node containing the text to edit.</returns>
		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x0600316E RID: 12654 RVA: 0x000DF657 File Offset: 0x000DD857
		public TreeNode Node
		{
			get
			{
				return this.node;
			}
		}

		// Token: 0x04001E1F RID: 7711
		private readonly string label;

		// Token: 0x04001E20 RID: 7712
		private readonly TreeNode node;

		// Token: 0x04001E21 RID: 7713
		private bool cancelEdit;
	}
}
