using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.DrawItem" /> event.</summary>
	// Token: 0x0200023D RID: 573
	public class DrawListViewItemEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DrawListViewItemEventArgs" /> class.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> surface on which to draw.</param>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ListViewItem" /> to draw.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> within which to draw.</param>
		/// <param name="itemIndex">The index of the <see cref="T:System.Windows.Forms.ListViewItem" /> within the <see cref="P:System.Windows.Forms.ListView.Items" /> collection.</param>
		/// <param name="state">A bitwise combination of <see cref="T:System.Windows.Forms.ListViewItemStates" /> values indicating the current state of the <see cref="T:System.Windows.Forms.ListViewItem" /> to draw.</param>
		// Token: 0x060024CA RID: 9418 RVA: 0x000ACD53 File Offset: 0x000AAF53
		public DrawListViewItemEventArgs(Graphics graphics, ListViewItem item, Rectangle bounds, int itemIndex, ListViewItemStates state)
		{
			this.graphics = graphics;
			this.item = item;
			this.bounds = bounds;
			this.itemIndex = itemIndex;
			this.state = state;
			this.drawDefault = false;
		}

		/// <summary>Gets or sets a property indicating whether the <see cref="T:System.Windows.Forms.ListView" /> control will use the default drawing for the <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the system draws the item; <see langword="false" /> if the event handler draws the item. The default value is <see langword="false" />.</returns>
		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x060024CB RID: 9419 RVA: 0x000ACD87 File Offset: 0x000AAF87
		// (set) Token: 0x060024CC RID: 9420 RVA: 0x000ACD8F File Offset: 0x000AAF8F
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

		/// <summary>Gets the <see cref="T:System.Drawing.Graphics" /> used to draw the <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Graphics" /> used to draw the <see cref="T:System.Windows.Forms.ListViewItem" />.</returns>
		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x060024CD RID: 9421 RVA: 0x000ACD98 File Offset: 0x000AAF98
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ListViewItem" /> to draw.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> to draw.</returns>
		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x060024CE RID: 9422 RVA: 0x000ACDA0 File Offset: 0x000AAFA0
		public ListViewItem Item
		{
			get
			{
				return this.item;
			}
		}

		/// <summary>Gets the size and location of the <see cref="T:System.Windows.Forms.ListViewItem" /> to draw.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the <see cref="T:System.Windows.Forms.ListViewItem" /> to draw.</returns>
		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x060024CF RID: 9423 RVA: 0x000ACDA8 File Offset: 0x000AAFA8
		public Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
		}

		/// <summary>Gets the index of the <see cref="T:System.Windows.Forms.ListViewItem" /> within the <see cref="P:System.Windows.Forms.ListView.Items" /> collection of the containing <see cref="T:System.Windows.Forms.ListView" />.</summary>
		/// <returns>The index of the <see cref="T:System.Windows.Forms.ListViewItem" /> within the <see cref="P:System.Windows.Forms.ListView.Items" /> collection.</returns>
		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x060024D0 RID: 9424 RVA: 0x000ACDB0 File Offset: 0x000AAFB0
		public int ItemIndex
		{
			get
			{
				return this.itemIndex;
			}
		}

		/// <summary>Gets the current state of the <see cref="T:System.Windows.Forms.ListViewItem" /> to draw.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.ListViewItemStates" /> values indicating the current state of the <see cref="T:System.Windows.Forms.ListViewItem" />.</returns>
		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x060024D1 RID: 9425 RVA: 0x000ACDB8 File Offset: 0x000AAFB8
		public ListViewItemStates State
		{
			get
			{
				return this.state;
			}
		}

		/// <summary>Draws the background of the <see cref="T:System.Windows.Forms.ListViewItem" /> using its current background color.</summary>
		// Token: 0x060024D2 RID: 9426 RVA: 0x000ACDC0 File Offset: 0x000AAFC0
		public void DrawBackground()
		{
			Brush brush = new SolidBrush(this.item.BackColor);
			this.Graphics.FillRectangle(brush, this.bounds);
			brush.Dispose();
		}

		/// <summary>Draws a focus rectangle for the <see cref="T:System.Windows.Forms.ListViewItem" /> if it has focus.</summary>
		// Token: 0x060024D3 RID: 9427 RVA: 0x000ACDF8 File Offset: 0x000AAFF8
		public void DrawFocusRectangle()
		{
			if ((this.state & ListViewItemStates.Focused) == ListViewItemStates.Focused)
			{
				Rectangle rectangle = this.bounds;
				ControlPaint.DrawFocusRectangle(this.graphics, this.UpdateBounds(rectangle, false), this.item.ForeColor, this.item.BackColor);
			}
		}

		/// <summary>Draws the text of the <see cref="T:System.Windows.Forms.ListViewItem" /> using its current foreground color.</summary>
		// Token: 0x060024D4 RID: 9428 RVA: 0x000ACE42 File Offset: 0x000AB042
		public void DrawText()
		{
			this.DrawText(TextFormatFlags.Default);
		}

		/// <summary>Draws the text of the <see cref="T:System.Windows.Forms.ListViewItem" /> using its current foreground color and formatting it with the specified <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</summary>
		/// <param name="flags">A bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
		// Token: 0x060024D5 RID: 9429 RVA: 0x000ACE4B File Offset: 0x000AB04B
		public void DrawText(TextFormatFlags flags)
		{
			TextRenderer.DrawText(this.graphics, this.item.Text, this.item.Font, this.UpdateBounds(this.bounds, true), this.item.ForeColor, flags);
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x000ACE88 File Offset: 0x000AB088
		private Rectangle UpdateBounds(Rectangle originalBounds, bool drawText)
		{
			Rectangle rectangle = originalBounds;
			if (this.item.ListView.View == View.Details)
			{
				if (!this.item.ListView.FullRowSelect && this.item.SubItems.Count > 0)
				{
					ListViewItem.ListViewSubItem listViewSubItem = this.item.SubItems[0];
					Size size = TextRenderer.MeasureText(listViewSubItem.Text, listViewSubItem.Font);
					rectangle = new Rectangle(originalBounds.X, originalBounds.Y, size.Width, size.Height);
					rectangle.X += 4;
					int num = rectangle.Width;
					rectangle.Width = num + 1;
				}
				else
				{
					rectangle.X += 4;
					rectangle.Width -= 4;
				}
				if (drawText)
				{
					int num = rectangle.X;
					rectangle.X = num - 1;
				}
			}
			return rectangle;
		}

		// Token: 0x04000F37 RID: 3895
		private readonly Graphics graphics;

		// Token: 0x04000F38 RID: 3896
		private readonly ListViewItem item;

		// Token: 0x04000F39 RID: 3897
		private readonly Rectangle bounds;

		// Token: 0x04000F3A RID: 3898
		private readonly int itemIndex;

		// Token: 0x04000F3B RID: 3899
		private readonly ListViewItemStates state;

		// Token: 0x04000F3C RID: 3900
		private bool drawDefault;
	}
}
