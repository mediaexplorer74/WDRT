using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolBar.ButtonClick" /> event.</summary>
	// Token: 0x020003AA RID: 938
	public class ToolBarButtonClickEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolBarButtonClickEventArgs" /> class.</summary>
		/// <param name="button">The <see cref="T:System.Windows.Forms.ToolBarButton" /> that was clicked.</param>
		// Token: 0x06003D90 RID: 15760 RVA: 0x0010B593 File Offset: 0x00109793
		public ToolBarButtonClickEventArgs(ToolBarButton button)
		{
			this.button = button;
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ToolBarButton" /> that was clicked.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolBarButton" /> that was clicked.</returns>
		// Token: 0x17000F06 RID: 3846
		// (get) Token: 0x06003D91 RID: 15761 RVA: 0x0010B5A2 File Offset: 0x001097A2
		// (set) Token: 0x06003D92 RID: 15762 RVA: 0x0010B5AA File Offset: 0x001097AA
		public ToolBarButton Button
		{
			get
			{
				return this.button;
			}
			set
			{
				this.button = value;
			}
		}

		// Token: 0x0400241F RID: 9247
		private ToolBarButton button;
	}
}
