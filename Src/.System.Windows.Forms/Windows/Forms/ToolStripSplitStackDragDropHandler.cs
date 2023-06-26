using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x020003B1 RID: 945
	internal sealed class ToolStripSplitStackDragDropHandler : IDropTarget, ISupportOleDropSource
	{
		// Token: 0x06003ED0 RID: 16080 RVA: 0x0011066A File Offset: 0x0010E86A
		public ToolStripSplitStackDragDropHandler(ToolStrip owner)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			this.owner = owner;
		}

		// Token: 0x06003ED1 RID: 16081 RVA: 0x00110688 File Offset: 0x0010E888
		public void OnDragEnter(DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(ToolStripItem)))
			{
				e.Effect = DragDropEffects.Move;
				this.ShowItemDropPoint(this.owner.PointToClient(new Point(e.X, e.Y)));
			}
		}

		// Token: 0x06003ED2 RID: 16082 RVA: 0x001106D6 File Offset: 0x0010E8D6
		public void OnDragLeave(EventArgs e)
		{
			this.owner.ClearInsertionMark();
		}

		// Token: 0x06003ED3 RID: 16083 RVA: 0x001106E4 File Offset: 0x0010E8E4
		public void OnDragDrop(DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(ToolStripItem)))
			{
				ToolStripItem toolStripItem = (ToolStripItem)e.Data.GetData(typeof(ToolStripItem));
				this.OnDropItem(toolStripItem, this.owner.PointToClient(new Point(e.X, e.Y)));
			}
		}

		// Token: 0x06003ED4 RID: 16084 RVA: 0x00110748 File Offset: 0x0010E948
		public void OnDragOver(DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(ToolStripItem)))
			{
				if (this.ShowItemDropPoint(this.owner.PointToClient(new Point(e.X, e.Y))))
				{
					e.Effect = DragDropEffects.Move;
					return;
				}
				if (this.owner != null)
				{
					this.owner.ClearInsertionMark();
				}
				e.Effect = DragDropEffects.None;
			}
		}

		// Token: 0x06003ED5 RID: 16085 RVA: 0x000070A6 File Offset: 0x000052A6
		public void OnGiveFeedback(GiveFeedbackEventArgs e)
		{
		}

		// Token: 0x06003ED6 RID: 16086 RVA: 0x000070A6 File Offset: 0x000052A6
		public void OnQueryContinueDrag(QueryContinueDragEventArgs e)
		{
		}

		// Token: 0x06003ED7 RID: 16087 RVA: 0x001107B4 File Offset: 0x0010E9B4
		private void OnDropItem(ToolStripItem droppedItem, Point ownerClientAreaRelativeDropPoint)
		{
			Point empty = Point.Empty;
			int itemInsertionIndex = this.GetItemInsertionIndex(ownerClientAreaRelativeDropPoint);
			if (itemInsertionIndex < 0)
			{
				if (itemInsertionIndex == -1 && this.owner.Items.Count == 0)
				{
					this.owner.Items.Add(droppedItem);
					this.owner.ClearInsertionMark();
				}
				return;
			}
			ToolStripItem toolStripItem = this.owner.Items[itemInsertionIndex];
			if (toolStripItem == droppedItem)
			{
				this.owner.ClearInsertionMark();
				return;
			}
			ToolStripSplitStackDragDropHandler.RelativeLocation relativeLocation = this.ComparePositions(toolStripItem.Bounds, ownerClientAreaRelativeDropPoint);
			droppedItem.Alignment = toolStripItem.Alignment;
			int num = Math.Max(0, itemInsertionIndex);
			if (relativeLocation == ToolStripSplitStackDragDropHandler.RelativeLocation.Above)
			{
				num = ((toolStripItem.Alignment == ToolStripItemAlignment.Left) ? num : (num + 1));
			}
			else if (relativeLocation == ToolStripSplitStackDragDropHandler.RelativeLocation.Below)
			{
				num = ((toolStripItem.Alignment == ToolStripItemAlignment.Left) ? num : (num - 1));
			}
			else if ((toolStripItem.Alignment == ToolStripItemAlignment.Left && relativeLocation == ToolStripSplitStackDragDropHandler.RelativeLocation.Left) || (toolStripItem.Alignment == ToolStripItemAlignment.Right && relativeLocation == ToolStripSplitStackDragDropHandler.RelativeLocation.Right))
			{
				num = Math.Max(0, (this.owner.RightToLeft == RightToLeft.Yes) ? (num + 1) : num);
			}
			else
			{
				num = Math.Max(0, (this.owner.RightToLeft == RightToLeft.No) ? (num + 1) : num);
			}
			if (this.owner.Items.IndexOf(droppedItem) < num)
			{
				num--;
			}
			this.owner.Items.MoveItem(Math.Max(0, num), droppedItem);
			this.owner.ClearInsertionMark();
		}

		// Token: 0x06003ED8 RID: 16088 RVA: 0x00110914 File Offset: 0x0010EB14
		private bool ShowItemDropPoint(Point ownerClientAreaRelativeDropPoint)
		{
			int itemInsertionIndex = this.GetItemInsertionIndex(ownerClientAreaRelativeDropPoint);
			if (itemInsertionIndex >= 0)
			{
				ToolStripItem toolStripItem = this.owner.Items[itemInsertionIndex];
				ToolStripSplitStackDragDropHandler.RelativeLocation relativeLocation = this.ComparePositions(toolStripItem.Bounds, ownerClientAreaRelativeDropPoint);
				Rectangle empty = Rectangle.Empty;
				switch (relativeLocation)
				{
				case ToolStripSplitStackDragDropHandler.RelativeLocation.Above:
					empty = new Rectangle(this.owner.Margin.Left, toolStripItem.Bounds.Top, this.owner.Width - this.owner.Margin.Horizontal - 1, ToolStrip.insertionBeamWidth);
					break;
				case ToolStripSplitStackDragDropHandler.RelativeLocation.Below:
					empty = new Rectangle(this.owner.Margin.Left, toolStripItem.Bounds.Bottom, this.owner.Width - this.owner.Margin.Horizontal - 1, ToolStrip.insertionBeamWidth);
					break;
				case ToolStripSplitStackDragDropHandler.RelativeLocation.Right:
					empty = new Rectangle(toolStripItem.Bounds.Right, this.owner.Margin.Top, ToolStrip.insertionBeamWidth, this.owner.Height - this.owner.Margin.Vertical - 1);
					break;
				case ToolStripSplitStackDragDropHandler.RelativeLocation.Left:
					empty = new Rectangle(toolStripItem.Bounds.Left, this.owner.Margin.Top, ToolStrip.insertionBeamWidth, this.owner.Height - this.owner.Margin.Vertical - 1);
					break;
				}
				this.owner.PaintInsertionMark(empty);
				return true;
			}
			if (this.owner.Items.Count == 0)
			{
				Rectangle displayRectangle = this.owner.DisplayRectangle;
				displayRectangle.Width = ToolStrip.insertionBeamWidth;
				this.owner.PaintInsertionMark(displayRectangle);
				return true;
			}
			return false;
		}

		// Token: 0x06003ED9 RID: 16089 RVA: 0x00110B0C File Offset: 0x0010ED0C
		private int GetItemInsertionIndex(Point ownerClientAreaRelativeDropPoint)
		{
			for (int i = 0; i < this.owner.DisplayedItems.Count; i++)
			{
				Rectangle bounds = this.owner.DisplayedItems[i].Bounds;
				bounds.Inflate(this.owner.DisplayedItems[i].Margin.Size);
				if (bounds.Contains(ownerClientAreaRelativeDropPoint))
				{
					return this.owner.Items.IndexOf(this.owner.DisplayedItems[i]);
				}
			}
			if (this.owner.DisplayedItems.Count > 0)
			{
				int j = 0;
				while (j < this.owner.DisplayedItems.Count)
				{
					if (this.owner.DisplayedItems[j].Alignment == ToolStripItemAlignment.Right)
					{
						if (j > 0)
						{
							return this.owner.Items.IndexOf(this.owner.DisplayedItems[j - 1]);
						}
						return this.owner.Items.IndexOf(this.owner.DisplayedItems[j]);
					}
					else
					{
						j++;
					}
				}
				return this.owner.Items.IndexOf(this.owner.DisplayedItems[this.owner.DisplayedItems.Count - 1]);
			}
			return -1;
		}

		// Token: 0x06003EDA RID: 16090 RVA: 0x00110C64 File Offset: 0x0010EE64
		private ToolStripSplitStackDragDropHandler.RelativeLocation ComparePositions(Rectangle orig, Point check)
		{
			if (this.owner.Orientation == Orientation.Horizontal)
			{
				int num = orig.Width / 2;
				if (orig.Left + num >= check.X)
				{
					return ToolStripSplitStackDragDropHandler.RelativeLocation.Left;
				}
				if (orig.Right - num <= check.X)
				{
					return ToolStripSplitStackDragDropHandler.RelativeLocation.Right;
				}
			}
			if (this.owner.Orientation == Orientation.Vertical)
			{
				int num2 = orig.Height / 2;
				return (check.Y <= orig.Top + num2) ? ToolStripSplitStackDragDropHandler.RelativeLocation.Above : ToolStripSplitStackDragDropHandler.RelativeLocation.Below;
			}
			return ToolStripSplitStackDragDropHandler.RelativeLocation.Left;
		}

		// Token: 0x04002489 RID: 9353
		private ToolStrip owner;

		// Token: 0x020007F9 RID: 2041
		private enum RelativeLocation
		{
			// Token: 0x040042E3 RID: 17123
			Above,
			// Token: 0x040042E4 RID: 17124
			Below,
			// Token: 0x040042E5 RID: 17125
			Right,
			// Token: 0x040042E6 RID: 17126
			Left
		}
	}
}
