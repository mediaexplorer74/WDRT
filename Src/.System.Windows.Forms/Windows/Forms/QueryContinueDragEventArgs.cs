using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.QueryContinueDrag" /> event.</summary>
	// Token: 0x02000338 RID: 824
	[ComVisible(true)]
	public class QueryContinueDragEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.QueryContinueDragEventArgs" /> class.</summary>
		/// <param name="keyState">The current state of the SHIFT, CTRL, and ALT keys.</param>
		/// <param name="escapePressed">
		///   <see langword="true" /> if the ESC key was pressed; otherwise, <see langword="false" />.</param>
		/// <param name="action">A <see cref="T:System.Windows.Forms.DragAction" /> value.</param>
		// Token: 0x06003572 RID: 13682 RVA: 0x000F263F File Offset: 0x000F083F
		public QueryContinueDragEventArgs(int keyState, bool escapePressed, DragAction action)
		{
			this.keyState = keyState;
			this.escapePressed = escapePressed;
			this.action = action;
		}

		/// <summary>Gets the current state of the SHIFT, CTRL, and ALT keys.</summary>
		/// <returns>The current state of the SHIFT, CTRL, and ALT keys.</returns>
		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x06003573 RID: 13683 RVA: 0x000F265C File Offset: 0x000F085C
		public int KeyState
		{
			get
			{
				return this.keyState;
			}
		}

		/// <summary>Gets whether the user pressed the ESC key.</summary>
		/// <returns>
		///   <see langword="true" /> if the ESC key was pressed; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x06003574 RID: 13684 RVA: 0x000F2664 File Offset: 0x000F0864
		public bool EscapePressed
		{
			get
			{
				return this.escapePressed;
			}
		}

		/// <summary>Gets or sets the status of a drag-and-drop operation.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DragAction" /> value.</returns>
		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x06003575 RID: 13685 RVA: 0x000F266C File Offset: 0x000F086C
		// (set) Token: 0x06003576 RID: 13686 RVA: 0x000F2674 File Offset: 0x000F0874
		public DragAction Action
		{
			get
			{
				return this.action;
			}
			set
			{
				this.action = value;
			}
		}

		// Token: 0x04001F4B RID: 8011
		private readonly int keyState;

		// Token: 0x04001F4C RID: 8012
		private readonly bool escapePressed;

		// Token: 0x04001F4D RID: 8013
		private DragAction action;
	}
}
