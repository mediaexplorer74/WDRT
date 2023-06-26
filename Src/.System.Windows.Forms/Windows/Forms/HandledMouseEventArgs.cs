using System;

namespace System.Windows.Forms
{
	/// <summary>Allows a custom control to prevent the <see cref="E:System.Windows.Forms.Control.MouseWheel" /> event from being sent to its parent container.</summary>
	// Token: 0x0200026E RID: 622
	public class HandledMouseEventArgs : MouseEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.HandledMouseEventArgs" /> class with the specified mouse button, number of mouse button clicks, horizontal and vertical screen coordinates, and the change of mouse pointer position.</summary>
		/// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values indicating which mouse button was pressed.</param>
		/// <param name="clicks">The number of times a mouse button was pressed.</param>
		/// <param name="x">The x-coordinate of a mouse click, in pixels.</param>
		/// <param name="y">The y-coordinate of a mouse click, in pixels.</param>
		/// <param name="delta">A signed count of the number of detents the wheel has rotated.</param>
		// Token: 0x060027F8 RID: 10232 RVA: 0x000BA0C2 File Offset: 0x000B82C2
		public HandledMouseEventArgs(MouseButtons button, int clicks, int x, int y, int delta)
			: this(button, clicks, x, y, delta, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.HandledMouseEventArgs" /> class with the specified mouse button, number of mouse button clicks, horizontal and vertical screen coordinates, the change of mouse pointer position, and the value indicating whether the event is handled.</summary>
		/// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values indicating which mouse button was pressed.</param>
		/// <param name="clicks">The number of times a mouse button was pressed.</param>
		/// <param name="x">The x-coordinate of a mouse click, in pixels.</param>
		/// <param name="y">The y-coordinate of a mouse click, in pixels.</param>
		/// <param name="delta">A signed count of the number of detents the wheel has rotated.</param>
		/// <param name="defaultHandledValue">
		///   <see langword="true" /> if the event is handled; otherwise, <see langword="false" />.</param>
		// Token: 0x060027F9 RID: 10233 RVA: 0x000BA0D2 File Offset: 0x000B82D2
		public HandledMouseEventArgs(MouseButtons button, int clicks, int x, int y, int delta, bool defaultHandledValue)
			: base(button, clicks, x, y, delta)
		{
			this.handled = defaultHandledValue;
		}

		/// <summary>Gets or sets whether this event should be forwarded to the control's parent container.</summary>
		/// <returns>
		///   <see langword="true" /> if the mouse event should go to the parent control; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x060027FA RID: 10234 RVA: 0x000BA0E9 File Offset: 0x000B82E9
		// (set) Token: 0x060027FB RID: 10235 RVA: 0x000BA0F1 File Offset: 0x000B82F1
		public bool Handled
		{
			get
			{
				return this.handled;
			}
			set
			{
				this.handled = value;
			}
		}

		// Token: 0x0400105F RID: 4191
		private bool handled;
	}
}
