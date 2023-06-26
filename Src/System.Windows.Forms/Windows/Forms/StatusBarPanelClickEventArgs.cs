using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.StatusBar.PanelClick" /> event.</summary>
	// Token: 0x02000379 RID: 889
	public class StatusBarPanelClickEventArgs : MouseEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.StatusBarPanelClickEventArgs" /> class.</summary>
		/// <param name="statusBarPanel">The <see cref="T:System.Windows.Forms.StatusBarPanel" /> that represents the panel that was clicked.</param>
		/// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values that represents the mouse buttons that were clicked while over the <see cref="T:System.Windows.Forms.StatusBarPanel" />.</param>
		/// <param name="clicks">The number of times that the mouse button was clicked.</param>
		/// <param name="x">The x-coordinate of the mouse click.</param>
		/// <param name="y">The y-coordinate of the mouse click.</param>
		// Token: 0x06003A4D RID: 14925 RVA: 0x0010141B File Offset: 0x000FF61B
		public StatusBarPanelClickEventArgs(StatusBarPanel statusBarPanel, MouseButtons button, int clicks, int x, int y)
			: base(button, clicks, x, y, 0)
		{
			this.statusBarPanel = statusBarPanel;
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.StatusBarPanel" /> to draw.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.StatusBarPanel" /> to draw.</returns>
		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x06003A4E RID: 14926 RVA: 0x00101431 File Offset: 0x000FF631
		public StatusBarPanel StatusBarPanel
		{
			get
			{
				return this.statusBarPanel;
			}
		}

		// Token: 0x040022F9 RID: 8953
		private readonly StatusBarPanel statusBarPanel;
	}
}
