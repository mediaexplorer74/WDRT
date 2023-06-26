using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.TreeView.DrawNode" /> event.</summary>
	// Token: 0x02000244 RID: 580
	public class DrawTreeNodeEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DrawTreeNodeEventArgs" /> class.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> surface on which to draw.</param>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> to draw.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> within which to draw.</param>
		/// <param name="state">A bitwise combination of the <see cref="T:System.Windows.Forms.TreeNodeStates" /> values indicating the current state of the <see cref="T:System.Windows.Forms.TreeNode" /> to draw.</param>
		// Token: 0x060024FD RID: 9469 RVA: 0x000AD291 File Offset: 0x000AB491
		public DrawTreeNodeEventArgs(Graphics graphics, TreeNode node, Rectangle bounds, TreeNodeStates state)
		{
			this.graphics = graphics;
			this.node = node;
			this.bounds = bounds;
			this.state = state;
			this.drawDefault = false;
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.TreeNode" /> should be drawn by the operating system rather than being owner drawn.</summary>
		/// <returns>
		///   <see langword="true" /> if the node should be drawn by the operating system; <see langword="false" /> if the node will be drawn in the event handler. The default value is <see langword="false" />.</returns>
		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x060024FE RID: 9470 RVA: 0x000AD2BD File Offset: 0x000AB4BD
		// (set) Token: 0x060024FF RID: 9471 RVA: 0x000AD2C5 File Offset: 0x000AB4C5
		public bool DrawDefault
		{
			get
			{
				return this.drawDefault;
			}
			set
			{
				this.drawDefault = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Drawing.Graphics" /> object used to draw the <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Graphics" /> used to draw the <see cref="T:System.Windows.Forms.TreeNode" />.</returns>
		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06002500 RID: 9472 RVA: 0x000AD2CE File Offset: 0x000AB4CE
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.TreeNode" /> to draw.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> to draw.</returns>
		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06002501 RID: 9473 RVA: 0x000AD2D6 File Offset: 0x000AB4D6
		public TreeNode Node
		{
			get
			{
				return this.node;
			}
		}

		/// <summary>Gets the size and location of the <see cref="T:System.Windows.Forms.TreeNode" /> to draw.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the <see cref="T:System.Windows.Forms.TreeNode" /> to draw.</returns>
		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06002502 RID: 9474 RVA: 0x000AD2DE File Offset: 0x000AB4DE
		public Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
		}

		/// <summary>Gets the current state of the <see cref="T:System.Windows.Forms.TreeNode" /> to draw.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.TreeNodeStates" /> values indicating the current state of the <see cref="T:System.Windows.Forms.TreeNode" />.</returns>
		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06002503 RID: 9475 RVA: 0x000AD2E6 File Offset: 0x000AB4E6
		public TreeNodeStates State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x04000F52 RID: 3922
		private readonly Graphics graphics;

		// Token: 0x04000F53 RID: 3923
		private readonly TreeNode node;

		// Token: 0x04000F54 RID: 3924
		private readonly Rectangle bounds;

		// Token: 0x04000F55 RID: 3925
		private readonly TreeNodeStates state;

		// Token: 0x04000F56 RID: 3926
		private bool drawDefault;
	}
}
