using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.TreeView.NodeMouseHover" /> event.</summary>
	// Token: 0x02000413 RID: 1043
	[ComVisible(true)]
	public class TreeNodeMouseHoverEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeNodeMouseHoverEventArgs" /> class.</summary>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> the mouse pointer is currently resting on.</param>
		// Token: 0x060048EC RID: 18668 RVA: 0x00133092 File Offset: 0x00131292
		public TreeNodeMouseHoverEventArgs(TreeNode node)
		{
			this.node = node;
		}

		/// <summary>Gets the node the mouse pointer is currently resting on.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> the mouse pointer is currently resting on.</returns>
		// Token: 0x170011E6 RID: 4582
		// (get) Token: 0x060048ED RID: 18669 RVA: 0x001330A1 File Offset: 0x001312A1
		public TreeNode Node
		{
			get
			{
				return this.node;
			}
		}

		// Token: 0x0400273C RID: 10044
		private readonly TreeNode node;
	}
}
