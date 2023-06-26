using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.ItemDrag" /> event of the <see cref="T:System.Windows.Forms.ListView" /> and <see cref="T:System.Windows.Forms.TreeView" /> controls.</summary>
	// Token: 0x020002AC RID: 684
	[ComVisible(true)]
	public class ItemDragEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ItemDragEventArgs" /> class with a specified mouse button.</summary>
		/// <param name="button">A bitwise combination of <see cref="T:System.Windows.Forms.MouseButtons" /> values that indicates which mouse buttons were pressed.</param>
		// Token: 0x06002A4D RID: 10829 RVA: 0x000BF952 File Offset: 0x000BDB52
		public ItemDragEventArgs(MouseButtons button)
		{
			this.button = button;
			this.item = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ItemDragEventArgs" /> class with a specified mouse button and the item that is being dragged.</summary>
		/// <param name="button">A bitwise combination of <see cref="T:System.Windows.Forms.MouseButtons" /> values that indicates which mouse buttons were pressed.</param>
		/// <param name="item">The item being dragged.</param>
		// Token: 0x06002A4E RID: 10830 RVA: 0x000BF968 File Offset: 0x000BDB68
		public ItemDragEventArgs(MouseButtons button, object item)
		{
			this.button = button;
			this.item = item;
		}

		/// <summary>Gets a value that indicates which mouse buttons were pressed during the drag operation.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.MouseButtons" /> values.</returns>
		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06002A4F RID: 10831 RVA: 0x000BF97E File Offset: 0x000BDB7E
		public MouseButtons Button
		{
			get
			{
				return this.button;
			}
		}

		/// <summary>Gets the item that is being dragged.</summary>
		/// <returns>An object that represents the item being dragged.</returns>
		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x06002A50 RID: 10832 RVA: 0x000BF986 File Offset: 0x000BDB86
		public object Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x04001124 RID: 4388
		private readonly MouseButtons button;

		// Token: 0x04001125 RID: 4389
		private readonly object item;
	}
}
