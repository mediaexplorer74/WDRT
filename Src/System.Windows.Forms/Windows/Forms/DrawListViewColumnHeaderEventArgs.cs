using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.DrawColumnHeader" /> event.</summary>
	// Token: 0x0200023B RID: 571
	public class DrawListViewColumnHeaderEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DrawListViewColumnHeaderEventArgs" /> class.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> surface on which to draw.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> within which to draw.</param>
		/// <param name="columnIndex">The index of the header's column within the <see cref="P:System.Windows.Forms.ListView.Columns" /> collection.</param>
		/// <param name="header">The <see cref="T:System.Windows.Forms.ColumnHeader" /> representing the header to draw.</param>
		/// <param name="state">A bitwise combination of <see cref="T:System.Windows.Forms.ListViewItemStates" /> values indicating the current state of the column header.</param>
		/// <param name="foreColor">The foreground <see cref="T:System.Drawing.Color" /> of the header.</param>
		/// <param name="backColor">The background <see cref="T:System.Drawing.Color" /> of the header.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> used for the header text.</param>
		// Token: 0x060024B8 RID: 9400 RVA: 0x000ACAA8 File Offset: 0x000AACA8
		public DrawListViewColumnHeaderEventArgs(Graphics graphics, Rectangle bounds, int columnIndex, ColumnHeader header, ListViewItemStates state, Color foreColor, Color backColor, Font font)
		{
			this.graphics = graphics;
			this.bounds = bounds;
			this.columnIndex = columnIndex;
			this.header = header;
			this.state = state;
			this.foreColor = foreColor;
			this.backColor = backColor;
			this.font = font;
		}

		/// <summary>Gets or sets a value indicating whether the column header should be drawn by the operating system instead of owner-drawn.</summary>
		/// <returns>
		///   <see langword="true" /> if the header should be drawn by the operating system; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x060024B9 RID: 9401 RVA: 0x000ACAF8 File Offset: 0x000AACF8
		// (set) Token: 0x060024BA RID: 9402 RVA: 0x000ACB00 File Offset: 0x000AAD00
		public bool DrawDefault
		{
			get
			{
				return this.drawDefault;
			}
			set
			{
				this.drawDefault = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Drawing.Graphics" /> used to draw the column header.</summary>
		/// <returns>A <see cref="T:System.Drawing.Graphics" /> used to draw the column header.</returns>
		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x060024BB RID: 9403 RVA: 0x000ACB09 File Offset: 0x000AAD09
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		/// <summary>Gets the size and location of the column header to draw.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the column header.</returns>
		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x060024BC RID: 9404 RVA: 0x000ACB11 File Offset: 0x000AAD11
		public Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
		}

		/// <summary>Gets the index of the <see cref="T:System.Windows.Forms.ColumnHeader" /> representing the header to draw.</summary>
		/// <returns>The index of the column header within the <see cref="P:System.Windows.Forms.ListView.Columns" /> collection.</returns>
		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x060024BD RID: 9405 RVA: 0x000ACB19 File Offset: 0x000AAD19
		public int ColumnIndex
		{
			get
			{
				return this.columnIndex;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ColumnHeader" /> representing the column header to draw.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ColumnHeader" /> representing the column header.</returns>
		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x060024BE RID: 9406 RVA: 0x000ACB21 File Offset: 0x000AAD21
		public ColumnHeader Header
		{
			get
			{
				return this.header;
			}
		}

		/// <summary>Gets the current state of the column header.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.ListViewItemStates" /> values indicating the current state of the column header.</returns>
		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x060024BF RID: 9407 RVA: 0x000ACB29 File Offset: 0x000AAD29
		public ListViewItemStates State
		{
			get
			{
				return this.state;
			}
		}

		/// <summary>Gets the foreground color of the header.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing the foreground color of the header.</returns>
		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x060024C0 RID: 9408 RVA: 0x000ACB31 File Offset: 0x000AAD31
		public Color ForeColor
		{
			get
			{
				return this.foreColor;
			}
		}

		/// <summary>Gets the background color of the header.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing the background color of the header.</returns>
		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x060024C1 RID: 9409 RVA: 0x000ACB39 File Offset: 0x000AAD39
		public Color BackColor
		{
			get
			{
				return this.backColor;
			}
		}

		/// <summary>Gets the font used to draw the column header text.</summary>
		/// <returns>A <see cref="T:System.Drawing.Font" /> representing the font of the header text.</returns>
		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x060024C2 RID: 9410 RVA: 0x000ACB41 File Offset: 0x000AAD41
		public Font Font
		{
			get
			{
				return this.font;
			}
		}

		/// <summary>Draws the background of the column header.</summary>
		// Token: 0x060024C3 RID: 9411 RVA: 0x000ACB4C File Offset: 0x000AAD4C
		public void DrawBackground()
		{
			if (Application.RenderWithVisualStyles)
			{
				VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.Header.Item.Normal);
				visualStyleRenderer.DrawBackground(this.graphics, this.bounds);
				return;
			}
			using (Brush brush = new SolidBrush(this.backColor))
			{
				this.graphics.FillRectangle(brush, this.bounds);
			}
			Rectangle rectangle = this.bounds;
			rectangle.Width--;
			rectangle.Height--;
			this.graphics.DrawRectangle(SystemPens.ControlDarkDark, rectangle);
			rectangle.Width--;
			rectangle.Height--;
			this.graphics.DrawLine(SystemPens.ControlLightLight, rectangle.X, rectangle.Y, rectangle.Right, rectangle.Y);
			this.graphics.DrawLine(SystemPens.ControlLightLight, rectangle.X, rectangle.Y, rectangle.X, rectangle.Bottom);
			this.graphics.DrawLine(SystemPens.ControlDark, rectangle.X + 1, rectangle.Bottom, rectangle.Right, rectangle.Bottom);
			this.graphics.DrawLine(SystemPens.ControlDark, rectangle.Right, rectangle.Y + 1, rectangle.Right, rectangle.Bottom);
		}

		/// <summary>Draws the column header text using the default formatting.</summary>
		// Token: 0x060024C4 RID: 9412 RVA: 0x000ACCC0 File Offset: 0x000AAEC0
		public void DrawText()
		{
			HorizontalAlignment textAlign = this.header.TextAlign;
			TextFormatFlags textFormatFlags = ((textAlign == HorizontalAlignment.Left) ? TextFormatFlags.Default : ((textAlign == HorizontalAlignment.Center) ? TextFormatFlags.HorizontalCenter : TextFormatFlags.Right));
			textFormatFlags |= TextFormatFlags.WordEllipsis;
			this.DrawText(textFormatFlags);
		}

		/// <summary>Draws the column header text, formatting it with the specified <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</summary>
		/// <param name="flags">A bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
		// Token: 0x060024C5 RID: 9413 RVA: 0x000ACCF8 File Offset: 0x000AAEF8
		public void DrawText(TextFormatFlags flags)
		{
			string text = this.header.Text;
			int width = TextRenderer.MeasureText(" ", this.font).Width;
			Rectangle rectangle = Rectangle.Inflate(this.bounds, -width, 0);
			TextRenderer.DrawText(this.graphics, text, this.font, rectangle, this.foreColor, flags);
		}

		// Token: 0x04000F2E RID: 3886
		private readonly Graphics graphics;

		// Token: 0x04000F2F RID: 3887
		private readonly Rectangle bounds;

		// Token: 0x04000F30 RID: 3888
		private readonly int columnIndex;

		// Token: 0x04000F31 RID: 3889
		private readonly ColumnHeader header;

		// Token: 0x04000F32 RID: 3890
		private readonly ListViewItemStates state;

		// Token: 0x04000F33 RID: 3891
		private readonly Color foreColor;

		// Token: 0x04000F34 RID: 3892
		private readonly Color backColor;

		// Token: 0x04000F35 RID: 3893
		private readonly Font font;

		// Token: 0x04000F36 RID: 3894
		private bool drawDefault;
	}
}
