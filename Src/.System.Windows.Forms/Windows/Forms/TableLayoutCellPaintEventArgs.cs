using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.TableLayoutPanel.CellPaint" /> event.</summary>
	// Token: 0x02000398 RID: 920
	public class TableLayoutCellPaintEventArgs : PaintEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TableLayoutCellPaintEventArgs" /> class.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to paint the item.</param>
		/// <param name="clipRectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle in which to paint.</param>
		/// <param name="cellBounds">The bounds of the cell.</param>
		/// <param name="column">The column of the cell.</param>
		/// <param name="row">The row of the cell.</param>
		// Token: 0x06003C3E RID: 15422 RVA: 0x00106EA3 File Offset: 0x001050A3
		public TableLayoutCellPaintEventArgs(Graphics g, Rectangle clipRectangle, Rectangle cellBounds, int column, int row)
			: base(g, clipRectangle)
		{
			this.bounds = cellBounds;
			this.row = row;
			this.column = column;
		}

		/// <summary>Gets the size and location of the cell.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> representing the size and location of the cell.</returns>
		// Token: 0x17000EAD RID: 3757
		// (get) Token: 0x06003C3F RID: 15423 RVA: 0x00106EC4 File Offset: 0x001050C4
		public Rectangle CellBounds
		{
			get
			{
				return this.bounds;
			}
		}

		/// <summary>Gets the row of the cell.</summary>
		/// <returns>The row position of the cell.</returns>
		// Token: 0x17000EAE RID: 3758
		// (get) Token: 0x06003C40 RID: 15424 RVA: 0x00106ECC File Offset: 0x001050CC
		public int Row
		{
			get
			{
				return this.row;
			}
		}

		/// <summary>Gets the column of the cell.</summary>
		/// <returns>The column position of the cell.</returns>
		// Token: 0x17000EAF RID: 3759
		// (get) Token: 0x06003C41 RID: 15425 RVA: 0x00106ED4 File Offset: 0x001050D4
		public int Column
		{
			get
			{
				return this.column;
			}
		}

		// Token: 0x04002392 RID: 9106
		private Rectangle bounds;

		// Token: 0x04002393 RID: 9107
		private int row;

		// Token: 0x04002394 RID: 9108
		private int column;
	}
}
