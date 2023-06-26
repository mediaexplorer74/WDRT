using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.RichTextBox.ContentsResized" /> event.</summary>
	// Token: 0x02000341 RID: 833
	public class ContentsResizedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ContentsResizedEventArgs" /> class.</summary>
		/// <param name="newRectangle">A <see cref="T:System.Drawing.Rectangle" /> that specifies the requested dimensions of the <see cref="T:System.Windows.Forms.RichTextBox" /> control.</param>
		// Token: 0x060035D9 RID: 13785 RVA: 0x000F3633 File Offset: 0x000F1833
		public ContentsResizedEventArgs(Rectangle newRectangle)
		{
			this.newRectangle = newRectangle;
		}

		/// <summary>Represents the requested size of the <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the requested size of the <see cref="T:System.Windows.Forms.RichTextBox" /> control.</returns>
		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x060035DA RID: 13786 RVA: 0x000F3642 File Offset: 0x000F1842
		public Rectangle NewRectangle
		{
			get
			{
				return this.newRectangle;
			}
		}

		// Token: 0x04001F66 RID: 8038
		private readonly Rectangle newRectangle;
	}
}
