using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see langword="DrawItem" /> event.</summary>
	// Token: 0x02000238 RID: 568
	public class DrawItemEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DrawItemEventArgs" /> class for the specified control with the specified font, state, surface to draw on, and the bounds to draw within.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> surface on which to draw.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to use, usually the parent control's <see cref="T:System.Drawing.Font" /> property.</param>
		/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> bounds to draw within.</param>
		/// <param name="index">The <see cref="T:System.Windows.Forms.Control.ControlCollection" /> index value of the item that is being drawn.</param>
		/// <param name="state">The control's <see cref="T:System.Windows.Forms.DrawItemState" /> information.</param>
		// Token: 0x060024A9 RID: 9385 RVA: 0x000AC950 File Offset: 0x000AAB50
		public DrawItemEventArgs(Graphics graphics, Font font, Rectangle rect, int index, DrawItemState state)
		{
			this.graphics = graphics;
			this.font = font;
			this.rect = rect;
			this.index = index;
			this.state = state;
			this.foreColor = SystemColors.WindowText;
			this.backColor = SystemColors.Window;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DrawItemEventArgs" /> class for the specified control with the specified font, state, foreground color, background color, surface to draw on, and the bounds to draw within.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> surface on which to draw.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to use, usually the parent control's <see cref="T:System.Drawing.Font" /> property.</param>
		/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> bounds to draw within.</param>
		/// <param name="index">The <see cref="T:System.Windows.Forms.Control.ControlCollection" /> index value of the item that is being drawn.</param>
		/// <param name="state">The control's <see cref="T:System.Windows.Forms.DrawItemState" /> information.</param>
		/// <param name="foreColor">The foreground <see cref="T:System.Drawing.Color" /> to draw the control with.</param>
		/// <param name="backColor">The background <see cref="T:System.Drawing.Color" /> to draw the control with.</param>
		// Token: 0x060024AA RID: 9386 RVA: 0x000AC99E File Offset: 0x000AAB9E
		public DrawItemEventArgs(Graphics graphics, Font font, Rectangle rect, int index, DrawItemState state, Color foreColor, Color backColor)
		{
			this.graphics = graphics;
			this.font = font;
			this.rect = rect;
			this.index = index;
			this.state = state;
			this.foreColor = foreColor;
			this.backColor = backColor;
		}

		/// <summary>Gets the background color of the item that is being drawn.</summary>
		/// <returns>The background <see cref="T:System.Drawing.Color" /> of the item that is being drawn.</returns>
		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x060024AB RID: 9387 RVA: 0x000AC9DB File Offset: 0x000AABDB
		public Color BackColor
		{
			get
			{
				if ((this.state & DrawItemState.Selected) == DrawItemState.Selected)
				{
					return SystemColors.Highlight;
				}
				return this.backColor;
			}
		}

		/// <summary>Gets the rectangle that represents the bounds of the item that is being drawn.</summary>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the item that is being drawn.</returns>
		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x060024AC RID: 9388 RVA: 0x000AC9F4 File Offset: 0x000AABF4
		public Rectangle Bounds
		{
			get
			{
				return this.rect;
			}
		}

		/// <summary>Gets the font that is assigned to the item being drawn.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> that is assigned to the item being drawn.</returns>
		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x060024AD RID: 9389 RVA: 0x000AC9FC File Offset: 0x000AABFC
		public Font Font
		{
			get
			{
				return this.font;
			}
		}

		/// <summary>Gets the foreground color of the of the item being drawn.</summary>
		/// <returns>The foreground <see cref="T:System.Drawing.Color" /> of the item being drawn.</returns>
		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x060024AE RID: 9390 RVA: 0x000ACA04 File Offset: 0x000AAC04
		public Color ForeColor
		{
			get
			{
				if ((this.state & DrawItemState.Selected) == DrawItemState.Selected)
				{
					return SystemColors.HighlightText;
				}
				return this.foreColor;
			}
		}

		/// <summary>Gets the graphics surface to draw the item on.</summary>
		/// <returns>The <see cref="T:System.Drawing.Graphics" /> surface to draw the item on.</returns>
		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x060024AF RID: 9391 RVA: 0x000ACA1D File Offset: 0x000AAC1D
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		/// <summary>Gets the index value of the item that is being drawn.</summary>
		/// <returns>The numeric value that represents the <see cref="P:System.Windows.Forms.Control.ControlCollection.Item(System.Int32)" /> value of the item being drawn.</returns>
		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x060024B0 RID: 9392 RVA: 0x000ACA25 File Offset: 0x000AAC25
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		/// <summary>Gets the state of the item being drawn.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DrawItemState" /> that represents the state of the item being drawn.</returns>
		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x060024B1 RID: 9393 RVA: 0x000ACA2D File Offset: 0x000AAC2D
		public DrawItemState State
		{
			get
			{
				return this.state;
			}
		}

		/// <summary>Draws the background within the bounds specified in the <see cref="Overload:System.Windows.Forms.DrawItemEventArgs.#ctor" /> constructor and with the appropriate color.</summary>
		// Token: 0x060024B2 RID: 9394 RVA: 0x000ACA38 File Offset: 0x000AAC38
		public virtual void DrawBackground()
		{
			Brush brush = new SolidBrush(this.BackColor);
			this.Graphics.FillRectangle(brush, this.rect);
			brush.Dispose();
		}

		/// <summary>Draws a focus rectangle within the bounds specified in the <see cref="Overload:System.Windows.Forms.DrawItemEventArgs.#ctor" /> constructor.</summary>
		// Token: 0x060024B3 RID: 9395 RVA: 0x000ACA69 File Offset: 0x000AAC69
		public virtual void DrawFocusRectangle()
		{
			if ((this.state & DrawItemState.Focus) == DrawItemState.Focus && (this.state & DrawItemState.NoFocusRect) != DrawItemState.NoFocusRect)
			{
				ControlPaint.DrawFocusRectangle(this.Graphics, this.rect, this.ForeColor, this.BackColor);
			}
		}

		// Token: 0x04000F1A RID: 3866
		private Color backColor;

		// Token: 0x04000F1B RID: 3867
		private Color foreColor;

		// Token: 0x04000F1C RID: 3868
		private Font font;

		// Token: 0x04000F1D RID: 3869
		private readonly Graphics graphics;

		// Token: 0x04000F1E RID: 3870
		private readonly int index;

		// Token: 0x04000F1F RID: 3871
		private readonly Rectangle rect;

		// Token: 0x04000F20 RID: 3872
		private readonly DrawItemState state;
	}
}
