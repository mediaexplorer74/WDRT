using System;

namespace System.Windows.Forms
{
	/// <summary>Defines mouse events.</summary>
	// Token: 0x020002A2 RID: 674
	public interface IDropTarget
	{
		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DragEnter" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data.</param>
		// Token: 0x06002A2C RID: 10796
		void OnDragEnter(DragEventArgs e);

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DragLeave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002A2D RID: 10797
		void OnDragLeave(EventArgs e);

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DragDrop" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data.</param>
		// Token: 0x06002A2E RID: 10798
		void OnDragDrop(DragEventArgs e);

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DragOver" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data.</param>
		// Token: 0x06002A2F RID: 10799
		void OnDragOver(DragEventArgs e);
	}
}
