using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.MouseUp" />, <see cref="E:System.Windows.Forms.Control.MouseDown" />, and <see cref="E:System.Windows.Forms.Control.MouseMove" /> events.</summary>
	// Token: 0x02000301 RID: 769
	[ComVisible(true)]
	public class MouseEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MouseEventArgs" /> class.</summary>
		/// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values that indicate which mouse button was pressed.</param>
		/// <param name="clicks">The number of times a mouse button was pressed.</param>
		/// <param name="x">The x-coordinate of a mouse click, in pixels.</param>
		/// <param name="y">The y-coordinate of a mouse click, in pixels.</param>
		/// <param name="delta">A signed count of the number of detents the wheel has rotated.</param>
		// Token: 0x06003125 RID: 12581 RVA: 0x000DD610 File Offset: 0x000DB810
		public MouseEventArgs(MouseButtons button, int clicks, int x, int y, int delta)
		{
			this.button = button;
			this.clicks = clicks;
			this.x = x;
			this.y = y;
			this.delta = delta;
		}

		/// <summary>Gets which mouse button was pressed.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values.</returns>
		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x06003126 RID: 12582 RVA: 0x000DD63D File Offset: 0x000DB83D
		public MouseButtons Button
		{
			get
			{
				return this.button;
			}
		}

		/// <summary>Gets the number of times the mouse button was pressed and released.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the number of times the mouse button was pressed and released.</returns>
		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x06003127 RID: 12583 RVA: 0x000DD645 File Offset: 0x000DB845
		public int Clicks
		{
			get
			{
				return this.clicks;
			}
		}

		/// <summary>Gets the x-coordinate of the mouse during the generating mouse event.</summary>
		/// <returns>The x-coordinate of the mouse, in pixels.</returns>
		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x06003128 RID: 12584 RVA: 0x000DD64D File Offset: 0x000DB84D
		public int X
		{
			get
			{
				return this.x;
			}
		}

		/// <summary>Gets the y-coordinate of the mouse during the generating mouse event.</summary>
		/// <returns>The y-coordinate of the mouse, in pixels.</returns>
		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x06003129 RID: 12585 RVA: 0x000DD655 File Offset: 0x000DB855
		public int Y
		{
			get
			{
				return this.y;
			}
		}

		/// <summary>Gets a signed count of the number of detents the mouse wheel has rotated, multiplied by the WHEEL_DELTA constant. A detent is one notch of the mouse wheel.</summary>
		/// <returns>A signed count of the number of detents the mouse wheel has rotated, multiplied by the WHEEL_DELTA constant.</returns>
		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x0600312A RID: 12586 RVA: 0x000DD65D File Offset: 0x000DB85D
		public int Delta
		{
			get
			{
				return this.delta;
			}
		}

		/// <summary>Gets the location of the mouse during the generating mouse event.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> that contains the x- and y- mouse coordinates, in pixels, relative to the upper-left corner of the form.</returns>
		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x0600312B RID: 12587 RVA: 0x000DD665 File Offset: 0x000DB865
		public Point Location
		{
			get
			{
				return new Point(this.x, this.y);
			}
		}

		// Token: 0x04001440 RID: 5184
		private readonly MouseButtons button;

		// Token: 0x04001441 RID: 5185
		private readonly int clicks;

		// Token: 0x04001442 RID: 5186
		private readonly int x;

		// Token: 0x04001443 RID: 5187
		private readonly int y;

		// Token: 0x04001444 RID: 5188
		private readonly int delta;
	}
}
