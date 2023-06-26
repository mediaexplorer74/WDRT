using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.DragDrop" />, <see cref="E:System.Windows.Forms.Control.DragEnter" />, or <see cref="E:System.Windows.Forms.Control.DragOver" /> event.</summary>
	// Token: 0x02000236 RID: 566
	[ComVisible(true)]
	public class DragEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DragEventArgs" /> class.</summary>
		/// <param name="data">The data associated with this event.</param>
		/// <param name="keyState">The current state of the SHIFT, CTRL, and ALT keys.</param>
		/// <param name="x">The x-coordinate of the mouse cursor in pixels.</param>
		/// <param name="y">The y-coordinate of the mouse cursor in pixels.</param>
		/// <param name="allowedEffect">One of the <see cref="T:System.Windows.Forms.DragDropEffects" /> values.</param>
		/// <param name="effect">One of the <see cref="T:System.Windows.Forms.DragDropEffects" /> values.</param>
		// Token: 0x0600249D RID: 9373 RVA: 0x000AC8E0 File Offset: 0x000AAAE0
		public DragEventArgs(IDataObject data, int keyState, int x, int y, DragDropEffects allowedEffect, DragDropEffects effect)
		{
			this.data = data;
			this.keyState = keyState;
			this.x = x;
			this.y = y;
			this.allowedEffect = allowedEffect;
			this.effect = effect;
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.IDataObject" /> that contains the data associated with this event.</summary>
		/// <returns>The data associated with this event.</returns>
		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x0600249E RID: 9374 RVA: 0x000AC915 File Offset: 0x000AAB15
		public IDataObject Data
		{
			get
			{
				return this.data;
			}
		}

		/// <summary>Gets the current state of the SHIFT, CTRL, and ALT keys, as well as the state of the mouse buttons.</summary>
		/// <returns>The current state of the SHIFT, CTRL, and ALT keys and of the mouse buttons.</returns>
		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x0600249F RID: 9375 RVA: 0x000AC91D File Offset: 0x000AAB1D
		public int KeyState
		{
			get
			{
				return this.keyState;
			}
		}

		/// <summary>Gets the x-coordinate of the mouse pointer, in screen coordinates.</summary>
		/// <returns>The x-coordinate of the mouse pointer in pixels.</returns>
		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x060024A0 RID: 9376 RVA: 0x000AC925 File Offset: 0x000AAB25
		public int X
		{
			get
			{
				return this.x;
			}
		}

		/// <summary>Gets the y-coordinate of the mouse pointer, in screen coordinates.</summary>
		/// <returns>The y-coordinate of the mouse pointer in pixels.</returns>
		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x060024A1 RID: 9377 RVA: 0x000AC92D File Offset: 0x000AAB2D
		public int Y
		{
			get
			{
				return this.y;
			}
		}

		/// <summary>Gets which drag-and-drop operations are allowed by the originator (or source) of the drag event.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DragDropEffects" /> values.</returns>
		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x060024A2 RID: 9378 RVA: 0x000AC935 File Offset: 0x000AAB35
		public DragDropEffects AllowedEffect
		{
			get
			{
				return this.allowedEffect;
			}
		}

		/// <summary>Gets or sets the target drop effect in a drag-and-drop operation.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DragDropEffects" /> values.</returns>
		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x060024A3 RID: 9379 RVA: 0x000AC93D File Offset: 0x000AAB3D
		// (set) Token: 0x060024A4 RID: 9380 RVA: 0x000AC945 File Offset: 0x000AAB45
		public DragDropEffects Effect
		{
			get
			{
				return this.effect;
			}
			set
			{
				this.effect = value;
			}
		}

		// Token: 0x04000F14 RID: 3860
		private readonly IDataObject data;

		// Token: 0x04000F15 RID: 3861
		private readonly int keyState;

		// Token: 0x04000F16 RID: 3862
		private readonly int x;

		// Token: 0x04000F17 RID: 3863
		private readonly int y;

		// Token: 0x04000F18 RID: 3864
		private readonly DragDropEffects allowedEffect;

		// Token: 0x04000F19 RID: 3865
		private DragDropEffects effect;
	}
}
