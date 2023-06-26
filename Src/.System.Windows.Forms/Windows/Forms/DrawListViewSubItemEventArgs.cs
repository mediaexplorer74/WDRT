using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.DrawSubItem" /> event.</summary>
	// Token: 0x0200023F RID: 575
	public class DrawListViewSubItemEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DrawListViewSubItemEventArgs" /> class.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> surface on which to draw.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> within which to draw.</param>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ListViewItem" /> parent of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to draw.</param>
		/// <param name="subItem">The <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to draw.</param>
		/// <param name="itemIndex">The index of the parent <see cref="T:System.Windows.Forms.ListViewItem" /> within the <see cref="P:System.Windows.Forms.ListView.Items" /> collection.</param>
		/// <param name="columnIndex">The index of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> column within the <see cref="P:System.Windows.Forms.ListView.Columns" /> collection.</param>
		/// <param name="header">The <see cref="T:System.Windows.Forms.ColumnHeader" /> for the column in which the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> is displayed.</param>
		/// <param name="itemState">A bitwise combination of <see cref="T:System.Windows.Forms.ListViewItemStates" /> values indicating the current state of the <see cref="T:System.Windows.Forms.ListViewItem" /> parent of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to draw.</param>
		// Token: 0x060024DB RID: 9435 RVA: 0x000ACF70 File Offset: 0x000AB170
		public DrawListViewSubItemEventArgs(Graphics graphics, Rectangle bounds, ListViewItem item, ListViewItem.ListViewSubItem subItem, int itemIndex, int columnIndex, ColumnHeader header, ListViewItemStates itemState)
		{
			this.graphics = graphics;
			this.bounds = bounds;
			this.item = item;
			this.subItem = subItem;
			this.itemIndex = itemIndex;
			this.columnIndex = columnIndex;
			this.header = header;
			this.itemState = itemState;
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> should be drawn by the operating system instead of owner-drawn.</summary>
		/// <returns>
		///   <see langword="true" /> if the subitem should be drawn by the operating system; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x060024DC RID: 9436 RVA: 0x000ACFC0 File Offset: 0x000AB1C0
		// (set) Token: 0x060024DD RID: 9437 RVA: 0x000ACFC8 File Offset: 0x000AB1C8
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

		/// <summary>Gets the <see cref="T:System.Drawing.Graphics" /> used to draw the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Graphics" /> used to draw the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />.</returns>
		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x060024DE RID: 9438 RVA: 0x000ACFD1 File Offset: 0x000AB1D1
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		/// <summary>Gets the size and location of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to draw.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to draw.</returns>
		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x060024DF RID: 9439 RVA: 0x000ACFD9 File Offset: 0x000AB1D9
		public Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
		}

		/// <summary>Gets the parent <see cref="T:System.Windows.Forms.ListViewItem" /> of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to draw.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the parent of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to draw.</returns>
		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x060024E0 RID: 9440 RVA: 0x000ACFE1 File Offset: 0x000AB1E1
		public ListViewItem Item
		{
			get
			{
				return this.item;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to draw.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to draw.</returns>
		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x060024E1 RID: 9441 RVA: 0x000ACFE9 File Offset: 0x000AB1E9
		public ListViewItem.ListViewSubItem SubItem
		{
			get
			{
				return this.subItem;
			}
		}

		/// <summary>Gets the index of the parent <see cref="T:System.Windows.Forms.ListViewItem" /> of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to draw.</summary>
		/// <returns>The index of the parent <see cref="T:System.Windows.Forms.ListViewItem" /> within the <see cref="P:System.Windows.Forms.ListView.Items" /> collection.</returns>
		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x060024E2 RID: 9442 RVA: 0x000ACFF1 File Offset: 0x000AB1F1
		public int ItemIndex
		{
			get
			{
				return this.itemIndex;
			}
		}

		/// <summary>Gets the index of the <see cref="T:System.Windows.Forms.ListView" /> column in which the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> is displayed.</summary>
		/// <returns>The index of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> column within the <see cref="P:System.Windows.Forms.ListView.Columns" /> collection.</returns>
		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x060024E3 RID: 9443 RVA: 0x000ACFF9 File Offset: 0x000AB1F9
		public int ColumnIndex
		{
			get
			{
				return this.columnIndex;
			}
		}

		/// <summary>Gets the header of the <see cref="T:System.Windows.Forms.ListView" /> column in which the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> is displayed.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ColumnHeader" /> for the column in which the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> is displayed.</returns>
		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x060024E4 RID: 9444 RVA: 0x000AD001 File Offset: 0x000AB201
		public ColumnHeader Header
		{
			get
			{
				return this.header;
			}
		}

		/// <summary>Gets the current state of the parent <see cref="T:System.Windows.Forms.ListViewItem" /> of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to draw.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.ListViewItemStates" /> values indicating the current state of the parent <see cref="T:System.Windows.Forms.ListViewItem" />.</returns>
		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x060024E5 RID: 9445 RVA: 0x000AD009 File Offset: 0x000AB209
		public ListViewItemStates ItemState
		{
			get
			{
				return this.itemState;
			}
		}

		/// <summary>Draws the background of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> using its current background color.</summary>
		// Token: 0x060024E6 RID: 9446 RVA: 0x000AD014 File Offset: 0x000AB214
		public void DrawBackground()
		{
			Color color = ((this.itemIndex == -1) ? this.item.BackColor : this.subItem.BackColor);
			using (Brush brush = new SolidBrush(color))
			{
				this.Graphics.FillRectangle(brush, this.bounds);
			}
		}

		/// <summary>Draws a focus rectangle for the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> if the parent <see cref="T:System.Windows.Forms.ListViewItem" /> has focus.</summary>
		/// <param name="bounds">The area within which to draw the focus rectangle.</param>
		// Token: 0x060024E7 RID: 9447 RVA: 0x000AD078 File Offset: 0x000AB278
		public void DrawFocusRectangle(Rectangle bounds)
		{
			if ((this.itemState & ListViewItemStates.Focused) == ListViewItemStates.Focused)
			{
				ControlPaint.DrawFocusRectangle(this.graphics, Rectangle.Inflate(bounds, -1, -1), this.item.ForeColor, this.item.BackColor);
			}
		}

		/// <summary>Draws the text of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> using its current foreground color.</summary>
		// Token: 0x060024E8 RID: 9448 RVA: 0x000AD0B0 File Offset: 0x000AB2B0
		public void DrawText()
		{
			HorizontalAlignment textAlign = this.header.TextAlign;
			TextFormatFlags textFormatFlags = ((textAlign == HorizontalAlignment.Left) ? TextFormatFlags.Default : ((textAlign == HorizontalAlignment.Center) ? TextFormatFlags.HorizontalCenter : TextFormatFlags.Right));
			textFormatFlags |= TextFormatFlags.WordEllipsis;
			this.DrawText(textFormatFlags);
		}

		/// <summary>Draws the text of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> using its current foreground color and formatting it with the specified <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</summary>
		/// <param name="flags">A bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
		// Token: 0x060024E9 RID: 9449 RVA: 0x000AD0E8 File Offset: 0x000AB2E8
		public void DrawText(TextFormatFlags flags)
		{
			string text = ((this.itemIndex == -1) ? this.item.Text : this.subItem.Text);
			Font font = ((this.itemIndex == -1) ? this.item.Font : this.subItem.Font);
			Color color = ((this.itemIndex == -1) ? this.item.ForeColor : this.subItem.ForeColor);
			int width = TextRenderer.MeasureText(" ", font).Width;
			Rectangle rectangle = Rectangle.Inflate(this.bounds, -width, 0);
			TextRenderer.DrawText(this.graphics, text, font, rectangle, color, flags);
		}

		// Token: 0x04000F3D RID: 3901
		private readonly Graphics graphics;

		// Token: 0x04000F3E RID: 3902
		private readonly Rectangle bounds;

		// Token: 0x04000F3F RID: 3903
		private readonly ListViewItem item;

		// Token: 0x04000F40 RID: 3904
		private readonly ListViewItem.ListViewSubItem subItem;

		// Token: 0x04000F41 RID: 3905
		private readonly int itemIndex;

		// Token: 0x04000F42 RID: 3906
		private readonly int columnIndex;

		// Token: 0x04000F43 RID: 3907
		private readonly ColumnHeader header;

		// Token: 0x04000F44 RID: 3908
		private readonly ListViewItemStates itemState;

		// Token: 0x04000F45 RID: 3909
		private bool drawDefault;
	}
}
