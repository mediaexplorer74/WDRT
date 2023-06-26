using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.TabControl.Selecting" /> and <see cref="E:System.Windows.Forms.TabControl.Deselecting" /> events of a <see cref="T:System.Windows.Forms.TabControl" /> control.</summary>
	// Token: 0x02000387 RID: 903
	public class TabControlCancelEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TabControlCancelEventArgs" /> class.</summary>
		/// <param name="tabPage">The <see cref="T:System.Windows.Forms.TabPage" /> the event is occurring for.</param>
		/// <param name="tabPageIndex">The zero-based index of <paramref name="tabPage" /> in the <see cref="P:System.Windows.Forms.TabControl.TabPages" /> collection.</param>
		/// <param name="cancel">
		///   <see langword="true" /> to cancel the tab change by default; otherwise, <see langword="false" />.</param>
		/// <param name="action">One of the <see cref="T:System.Windows.Forms.TabControlAction" /> values.</param>
		// Token: 0x06003B9E RID: 15262 RVA: 0x00105499 File Offset: 0x00103699
		public TabControlCancelEventArgs(TabPage tabPage, int tabPageIndex, bool cancel, TabControlAction action)
			: base(cancel)
		{
			this.tabPage = tabPage;
			this.tabPageIndex = tabPageIndex;
			this.action = action;
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.TabPage" /> the event is occurring for.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TabPage" /> the event is occurring for.</returns>
		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x06003B9F RID: 15263 RVA: 0x001054B8 File Offset: 0x001036B8
		public TabPage TabPage
		{
			get
			{
				return this.tabPage;
			}
		}

		/// <summary>Gets the zero-based index of the <see cref="P:System.Windows.Forms.TabControlCancelEventArgs.TabPage" /> in the <see cref="P:System.Windows.Forms.TabControl.TabPages" /> collection.</summary>
		/// <returns>The zero-based index of the <see cref="P:System.Windows.Forms.TabControlCancelEventArgs.TabPage" /> in the <see cref="P:System.Windows.Forms.TabControl.TabPages" /> collection.</returns>
		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x06003BA0 RID: 15264 RVA: 0x001054C0 File Offset: 0x001036C0
		public int TabPageIndex
		{
			get
			{
				return this.tabPageIndex;
			}
		}

		/// <summary>Gets a value indicating which event is occurring.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.TabControlAction" /> values.</returns>
		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x06003BA1 RID: 15265 RVA: 0x001054C8 File Offset: 0x001036C8
		public TabControlAction Action
		{
			get
			{
				return this.action;
			}
		}

		// Token: 0x0400236C RID: 9068
		private TabPage tabPage;

		// Token: 0x0400236D RID: 9069
		private int tabPageIndex;

		// Token: 0x0400236E RID: 9070
		private TabControlAction action;
	}
}
