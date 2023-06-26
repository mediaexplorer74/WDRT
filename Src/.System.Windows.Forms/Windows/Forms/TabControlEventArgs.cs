using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.TabControl.Selected" /> and <see cref="E:System.Windows.Forms.TabControl.Deselected" /> events of a <see cref="T:System.Windows.Forms.TabControl" /> control.</summary>
	// Token: 0x02000389 RID: 905
	public class TabControlEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TabControlEventArgs" /> class.</summary>
		/// <param name="tabPage">The <see cref="T:System.Windows.Forms.TabPage" /> the event is occurring for.</param>
		/// <param name="tabPageIndex">The zero-based index of <paramref name="tabPage" /> in the <see cref="P:System.Windows.Forms.TabControl.TabPages" /> collection.</param>
		/// <param name="action">One of the <see cref="T:System.Windows.Forms.TabControlAction" /> values.</param>
		// Token: 0x06003BA6 RID: 15270 RVA: 0x001054D0 File Offset: 0x001036D0
		public TabControlEventArgs(TabPage tabPage, int tabPageIndex, TabControlAction action)
		{
			this.tabPage = tabPage;
			this.tabPageIndex = tabPageIndex;
			this.action = action;
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.TabPage" /> the event is occurring for.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TabPage" /> the event is occurring for.</returns>
		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x06003BA7 RID: 15271 RVA: 0x001054ED File Offset: 0x001036ED
		public TabPage TabPage
		{
			get
			{
				return this.tabPage;
			}
		}

		/// <summary>Gets the zero-based index of the <see cref="P:System.Windows.Forms.TabControlEventArgs.TabPage" /> in the <see cref="P:System.Windows.Forms.TabControl.TabPages" /> collection.</summary>
		/// <returns>The zero-based index of the <see cref="P:System.Windows.Forms.TabControlEventArgs.TabPage" /> in the <see cref="P:System.Windows.Forms.TabControl.TabPages" /> collection.</returns>
		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x06003BA8 RID: 15272 RVA: 0x001054F5 File Offset: 0x001036F5
		public int TabPageIndex
		{
			get
			{
				return this.tabPageIndex;
			}
		}

		/// <summary>Gets a value indicating which event is occurring.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.TabControlAction" /> values.</returns>
		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x06003BA9 RID: 15273 RVA: 0x001054FD File Offset: 0x001036FD
		public TabControlAction Action
		{
			get
			{
				return this.action;
			}
		}

		// Token: 0x0400236F RID: 9071
		private TabPage tabPage;

		// Token: 0x04002370 RID: 9072
		private int tabPageIndex;

		// Token: 0x04002371 RID: 9073
		private TabControlAction action;
	}
}
