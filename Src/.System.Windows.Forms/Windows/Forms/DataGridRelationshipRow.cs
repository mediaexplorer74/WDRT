using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x02000185 RID: 389
	internal class DataGridRelationshipRow : DataGridRow
	{
		// Token: 0x0600170C RID: 5900 RVA: 0x0004EE0C File Offset: 0x0004D00C
		public DataGridRelationshipRow(DataGrid dataGrid, DataGridTableStyle dgTable, int rowNumber)
			: base(dataGrid, dgTable, rowNumber)
		{
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x00052D64 File Offset: 0x00050F64
		protected internal override int MinimumRowHeight(GridColumnStylesCollection cols)
		{
			return base.MinimumRowHeight(cols) + (this.expanded ? this.GetRelationshipRect().Height : 0);
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x00052D94 File Offset: 0x00050F94
		protected internal override int MinimumRowHeight(DataGridTableStyle dgTable)
		{
			return base.MinimumRowHeight(dgTable) + (this.expanded ? this.GetRelationshipRect().Height : 0);
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x0600170F RID: 5903 RVA: 0x00052DC2 File Offset: 0x00050FC2
		// (set) Token: 0x06001710 RID: 5904 RVA: 0x00052DCA File Offset: 0x00050FCA
		public virtual bool Expanded
		{
			get
			{
				return this.expanded;
			}
			set
			{
				if (this.expanded == value)
				{
					return;
				}
				if (this.expanded)
				{
					this.Collapse();
					return;
				}
				this.Expand();
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06001711 RID: 5905 RVA: 0x00052DEB File Offset: 0x00050FEB
		// (set) Token: 0x06001712 RID: 5906 RVA: 0x00052DF8 File Offset: 0x00050FF8
		private int FocusedRelation
		{
			get
			{
				return this.dgTable.FocusedRelation;
			}
			set
			{
				this.dgTable.FocusedRelation = value;
			}
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x00052E06 File Offset: 0x00051006
		private void Collapse()
		{
			if (this.expanded)
			{
				this.expanded = false;
				this.FocusedRelation = -1;
				base.DataGrid.OnRowHeightChanged(this);
			}
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x00052E2A File Offset: 0x0005102A
		protected override AccessibleObject CreateAccessibleObject()
		{
			return new DataGridRelationshipRow.DataGridRelationshipRowAccessibleObject(this);
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x00052E34 File Offset: 0x00051034
		private void Expand()
		{
			if (!this.expanded && base.DataGrid != null && this.dgTable != null && this.dgTable.RelationsList.Count > 0)
			{
				this.expanded = true;
				this.FocusedRelation = -1;
				base.DataGrid.OnRowHeightChanged(this);
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001716 RID: 5910 RVA: 0x00052E88 File Offset: 0x00051088
		// (set) Token: 0x06001717 RID: 5911 RVA: 0x00052EB8 File Offset: 0x000510B8
		public override int Height
		{
			get
			{
				int height = base.Height;
				if (this.expanded)
				{
					return height + this.GetRelationshipRect().Height;
				}
				return height;
			}
			set
			{
				if (this.expanded)
				{
					base.Height = value - this.GetRelationshipRect().Height;
					return;
				}
				base.Height = value;
			}
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x00052EEC File Offset: 0x000510EC
		public override Rectangle GetCellBounds(int col)
		{
			Rectangle cellBounds = base.GetCellBounds(col);
			cellBounds.Height = base.Height - 1;
			return cellBounds;
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x00052F14 File Offset: 0x00051114
		private Rectangle GetOutlineRect(int xOrigin, int yOrigin)
		{
			Rectangle rectangle = new Rectangle(xOrigin + 2, yOrigin + 2, 9, 9);
			return rectangle;
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x00052F33 File Offset: 0x00051133
		public override Rectangle GetNonScrollableArea()
		{
			if (this.expanded)
			{
				return this.GetRelationshipRect();
			}
			return Rectangle.Empty;
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x00052F4C File Offset: 0x0005114C
		private Rectangle GetRelationshipRect()
		{
			Rectangle relationshipRect = this.dgTable.RelationshipRect;
			relationshipRect.Y = base.Height - this.dgTable.BorderWidth;
			return relationshipRect;
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x00052F80 File Offset: 0x00051180
		private Rectangle GetRelationshipRectWithMirroring()
		{
			Rectangle relationshipRect = this.GetRelationshipRect();
			bool flag = (this.dgTable.IsDefault ? base.DataGrid.RowHeadersVisible : this.dgTable.RowHeadersVisible);
			if (flag)
			{
				int num = (this.dgTable.IsDefault ? base.DataGrid.RowHeaderWidth : this.dgTable.RowHeaderWidth);
				relationshipRect.X += base.DataGrid.GetRowHeaderRect().X + num;
			}
			relationshipRect.X = this.MirrorRelationshipRectangle(relationshipRect, base.DataGrid.GetRowHeaderRect(), base.DataGrid.RightToLeft == RightToLeft.Yes);
			return relationshipRect;
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x00053030 File Offset: 0x00051230
		private bool PointOverPlusMinusGlyph(int x, int y, Rectangle rowHeaders, bool alignToRight)
		{
			if (this.dgTable == null || this.dgTable.DataGrid == null || !this.dgTable.DataGrid.AllowNavigation)
			{
				return false;
			}
			Rectangle rectangle = rowHeaders;
			if (!base.DataGrid.FlatMode)
			{
				rectangle.Inflate(-1, -1);
			}
			Rectangle outlineRect = this.GetOutlineRect(rectangle.Right - 14, 0);
			outlineRect.X = this.MirrorRectangle(outlineRect.X, outlineRect.Width, rectangle, alignToRight);
			return outlineRect.Contains(x, y);
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x000530B8 File Offset: 0x000512B8
		public override bool OnMouseDown(int x, int y, Rectangle rowHeaders, bool alignToRight)
		{
			bool flag = (this.dgTable.IsDefault ? base.DataGrid.RowHeadersVisible : this.dgTable.RowHeadersVisible);
			if (flag && this.PointOverPlusMinusGlyph(x, y, rowHeaders, alignToRight))
			{
				if (this.dgTable.RelationsList.Count == 0)
				{
					return false;
				}
				if (this.expanded)
				{
					this.Collapse();
				}
				else
				{
					this.Expand();
				}
				base.DataGrid.OnNodeClick(EventArgs.Empty);
				return true;
			}
			else
			{
				if (!this.expanded)
				{
					return base.OnMouseDown(x, y, rowHeaders, alignToRight);
				}
				if (this.GetRelationshipRectWithMirroring().Contains(x, y))
				{
					int num = this.RelationFromY(y);
					if (num != -1)
					{
						this.FocusedRelation = -1;
						base.DataGrid.NavigateTo((string)this.dgTable.RelationsList[num], this, true);
					}
					return true;
				}
				return base.OnMouseDown(x, y, rowHeaders, alignToRight);
			}
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x000531A0 File Offset: 0x000513A0
		public override bool OnMouseMove(int x, int y, Rectangle rowHeaders, bool alignToRight)
		{
			if (!this.expanded)
			{
				return false;
			}
			if (this.GetRelationshipRectWithMirroring().Contains(x, y))
			{
				base.DataGrid.Cursor = Cursors.Hand;
				return true;
			}
			base.DataGrid.Cursor = Cursors.Default;
			return base.OnMouseMove(x, y, rowHeaders, alignToRight);
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x000531F8 File Offset: 0x000513F8
		public override void OnMouseLeft(Rectangle rowHeaders, bool alignToRight)
		{
			if (!this.expanded)
			{
				return;
			}
			Rectangle relationshipRect = this.GetRelationshipRect();
			relationshipRect.X += rowHeaders.X + this.dgTable.RowHeaderWidth;
			relationshipRect.X = this.MirrorRelationshipRectangle(relationshipRect, rowHeaders, alignToRight);
			if (this.FocusedRelation != -1)
			{
				this.InvalidateRowRect(relationshipRect);
				this.FocusedRelation = -1;
			}
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x0005325D File Offset: 0x0005145D
		public override void OnMouseLeft()
		{
			if (!this.expanded)
			{
				return;
			}
			if (this.FocusedRelation != -1)
			{
				this.InvalidateRow();
				this.FocusedRelation = -1;
			}
			base.OnMouseLeft();
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x00053284 File Offset: 0x00051484
		public override bool OnKeyPress(Keys keyData)
		{
			if ((keyData & Keys.Modifiers) == Keys.Shift && (keyData & Keys.KeyCode) != Keys.Tab)
			{
				return false;
			}
			Keys keys = keyData & Keys.KeyCode;
			if (keys <= Keys.Return)
			{
				if (keys == Keys.Tab)
				{
					return false;
				}
				if (keys == Keys.Return)
				{
					if (this.FocusedRelation != -1)
					{
						base.DataGrid.NavigateTo((string)this.dgTable.RelationsList[this.FocusedRelation], this, true);
						this.FocusedRelation = -1;
						return true;
					}
					return false;
				}
			}
			else if (keys != Keys.F5)
			{
				if (keys == Keys.NumLock)
				{
					return this.FocusedRelation == -1 && base.OnKeyPress(keyData);
				}
			}
			else
			{
				if (this.dgTable == null || this.dgTable.DataGrid == null || !this.dgTable.DataGrid.AllowNavigation)
				{
					return false;
				}
				if (this.expanded)
				{
					this.Collapse();
				}
				else
				{
					this.Expand();
				}
				this.FocusedRelation = -1;
				return true;
			}
			this.FocusedRelation = -1;
			return base.OnKeyPress(keyData);
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x00053384 File Offset: 0x00051584
		internal override void LoseChildFocus(Rectangle rowHeaders, bool alignToRight)
		{
			if (this.FocusedRelation == -1 || !this.expanded)
			{
				return;
			}
			this.FocusedRelation = -1;
			Rectangle relationshipRect = this.GetRelationshipRect();
			relationshipRect.X += rowHeaders.X + this.dgTable.RowHeaderWidth;
			relationshipRect.X = this.MirrorRelationshipRectangle(relationshipRect, rowHeaders, alignToRight);
			this.InvalidateRowRect(relationshipRect);
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x000533EC File Offset: 0x000515EC
		internal override bool ProcessTabKey(Keys keyData, Rectangle rowHeaders, bool alignToRight)
		{
			if (this.dgTable.RelationsList.Count == 0 || this.dgTable.DataGrid == null || !this.dgTable.DataGrid.AllowNavigation)
			{
				return false;
			}
			if (!this.expanded)
			{
				this.Expand();
			}
			if ((keyData & Keys.Shift) == Keys.Shift)
			{
				if (this.FocusedRelation == 0)
				{
					this.FocusedRelation = -1;
					return false;
				}
				Rectangle relationshipRect = this.GetRelationshipRect();
				relationshipRect.X += rowHeaders.X + this.dgTable.RowHeaderWidth;
				relationshipRect.X = this.MirrorRelationshipRectangle(relationshipRect, rowHeaders, alignToRight);
				this.InvalidateRowRect(relationshipRect);
				if (this.FocusedRelation == -1)
				{
					this.FocusedRelation = this.dgTable.RelationsList.Count - 1;
				}
				else
				{
					int num = this.FocusedRelation;
					this.FocusedRelation = num - 1;
				}
				return true;
			}
			else
			{
				if (this.FocusedRelation == this.dgTable.RelationsList.Count - 1)
				{
					this.FocusedRelation = -1;
					return false;
				}
				Rectangle relationshipRect2 = this.GetRelationshipRect();
				relationshipRect2.X += rowHeaders.X + this.dgTable.RowHeaderWidth;
				relationshipRect2.X = this.MirrorRelationshipRectangle(relationshipRect2, rowHeaders, alignToRight);
				this.InvalidateRowRect(relationshipRect2);
				int num = this.FocusedRelation;
				this.FocusedRelation = num + 1;
				return true;
			}
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x0004EE4E File Offset: 0x0004D04E
		public override int Paint(Graphics g, Rectangle bounds, Rectangle trueRowBounds, int firstVisibleColumn, int numVisibleColumns)
		{
			return this.Paint(g, bounds, trueRowBounds, firstVisibleColumn, numVisibleColumns, false);
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x00053544 File Offset: 0x00051744
		public override int Paint(Graphics g, Rectangle bounds, Rectangle trueRowBounds, int firstVisibleColumn, int numVisibleColumns, bool alignToRight)
		{
			bool traceVerbose = CompModSwitches.DGRelationShpRowPaint.TraceVerbose;
			int borderWidth = this.dgTable.BorderWidth;
			Rectangle rectangle = bounds;
			rectangle.Height = base.Height - borderWidth;
			int num = this.PaintData(g, rectangle, firstVisibleColumn, numVisibleColumns, alignToRight);
			int num2 = num + bounds.X - trueRowBounds.X;
			rectangle.Offset(0, borderWidth);
			if (borderWidth > 0)
			{
				this.PaintBottomBorder(g, rectangle, num, borderWidth, alignToRight);
			}
			if (this.expanded && this.dgTable.RelationsList.Count > 0)
			{
				Rectangle rectangle2 = new Rectangle(trueRowBounds.X, rectangle.Bottom, trueRowBounds.Width, trueRowBounds.Height - rectangle.Height - 2 * borderWidth);
				this.PaintRelations(g, rectangle2, trueRowBounds, num2, firstVisibleColumn, numVisibleColumns, alignToRight);
				rectangle2.Height++;
				if (borderWidth > 0)
				{
					this.PaintBottomBorder(g, rectangle2, num2, borderWidth, alignToRight);
				}
			}
			return num;
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x00053630 File Offset: 0x00051830
		protected override void PaintCellContents(Graphics g, Rectangle cellBounds, DataGridColumnStyle column, Brush backBr, Brush foreBrush, bool alignToRight)
		{
			CurrencyManager listManager = base.DataGrid.ListManager;
			string text = string.Empty;
			Rectangle rectangle = cellBounds;
			object obj = base.DataGrid.ListManager[this.number];
			if (obj is IDataErrorInfo)
			{
				text = ((IDataErrorInfo)obj)[column.PropertyDescriptor.Name];
			}
			if (!string.IsNullOrEmpty(text))
			{
				Bitmap errorBitmap = base.GetErrorBitmap();
				Bitmap bitmap = errorBitmap;
				Rectangle rectangle2;
				lock (bitmap)
				{
					rectangle2 = base.PaintIcon(g, rectangle, true, alignToRight, errorBitmap, backBr);
				}
				if (alignToRight)
				{
					rectangle.Width -= rectangle2.Width + 3;
				}
				else
				{
					rectangle.X += rectangle2.Width + 3;
				}
				DataGridToolTip toolTipProvider = base.DataGrid.ToolTipProvider;
				string text2 = text;
				DataGrid dataGrid = base.DataGrid;
				int toolTipId = dataGrid.ToolTipId;
				dataGrid.ToolTipId = toolTipId + 1;
				toolTipProvider.AddToolTip(text2, (IntPtr)toolTipId, rectangle2);
			}
			column.Paint(g, rectangle, listManager, base.RowNumber, backBr, foreBrush, alignToRight);
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x00053754 File Offset: 0x00051954
		public override void PaintHeader(Graphics g, Rectangle bounds, bool alignToRight, bool isDirty)
		{
			DataGrid dataGrid = base.DataGrid;
			Rectangle rectangle = bounds;
			if (!dataGrid.FlatMode)
			{
				ControlPaint.DrawBorder3D(g, rectangle, Border3DStyle.RaisedInner);
				rectangle.Inflate(-1, -1);
			}
			if (this.dgTable.IsDefault)
			{
				this.PaintHeaderInside(g, rectangle, base.DataGrid.HeaderBackBrush, alignToRight, isDirty);
				return;
			}
			this.PaintHeaderInside(g, rectangle, this.dgTable.HeaderBackBrush, alignToRight, isDirty);
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x000537C0 File Offset: 0x000519C0
		public void PaintHeaderInside(Graphics g, Rectangle bounds, Brush backBr, bool alignToRight, bool isDirty)
		{
			bool flag = this.dgTable.RelationsList.Count > 0 && this.dgTable.DataGrid.AllowNavigation;
			int num = this.MirrorRectangle(bounds.X, bounds.Width - (flag ? 14 : 0), bounds, alignToRight);
			Rectangle rectangle = new Rectangle(num, bounds.Y, bounds.Width - (flag ? 14 : 0), bounds.Height);
			base.PaintHeader(g, rectangle, alignToRight, isDirty);
			int num2 = this.MirrorRectangle(bounds.X + rectangle.Width, 14, bounds, alignToRight);
			Rectangle rectangle2 = new Rectangle(num2, bounds.Y, 14, bounds.Height);
			if (flag)
			{
				this.PaintPlusMinusGlyph(g, rectangle2, backBr, alignToRight);
			}
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x00053890 File Offset: 0x00051A90
		private void PaintRelations(Graphics g, Rectangle bounds, Rectangle trueRowBounds, int dataWidth, int firstCol, int nCols, bool alignToRight)
		{
			Rectangle relationshipRect = this.GetRelationshipRect();
			relationshipRect.X = (alignToRight ? (bounds.Right - relationshipRect.Width) : bounds.X);
			relationshipRect.Y = bounds.Y;
			int num = Math.Max(dataWidth, relationshipRect.Width);
			Region clip = g.Clip;
			g.ExcludeClip(relationshipRect);
			g.FillRectangle(base.GetBackBrush(), alignToRight ? (bounds.Right - dataWidth) : bounds.X, bounds.Y, dataWidth, bounds.Height);
			g.SetClip(bounds);
			relationshipRect.Height -= this.dgTable.BorderWidth;
			g.DrawRectangle(SystemPens.ControlText, relationshipRect.X, relationshipRect.Y, relationshipRect.Width - 1, relationshipRect.Height - 1);
			relationshipRect.Inflate(-1, -1);
			int num2 = this.PaintRelationText(g, relationshipRect, alignToRight);
			if (num2 < relationshipRect.Height)
			{
				g.FillRectangle(base.GetBackBrush(), relationshipRect.X, relationshipRect.Y + num2, relationshipRect.Width, relationshipRect.Height - num2);
			}
			g.Clip = clip;
			if (num < bounds.Width)
			{
				int num3;
				if (this.dgTable.IsDefault)
				{
					num3 = base.DataGrid.GridLineWidth;
				}
				else
				{
					num3 = this.dgTable.GridLineWidth;
				}
				g.FillRectangle(base.DataGrid.BackgroundBrush, alignToRight ? bounds.X : (bounds.X + num), bounds.Y, bounds.Width - num - num3 + 1, bounds.Height);
				if (num3 > 0)
				{
					Brush brush;
					if (this.dgTable.IsDefault)
					{
						brush = base.DataGrid.GridLineBrush;
					}
					else
					{
						brush = this.dgTable.GridLineBrush;
					}
					g.FillRectangle(brush, alignToRight ? (bounds.Right - num3 - num) : (bounds.X + num - num3), bounds.Y, num3, bounds.Height);
				}
			}
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x00053AA0 File Offset: 0x00051CA0
		private int PaintRelationText(Graphics g, Rectangle bounds, bool alignToRight)
		{
			g.FillRectangle(base.GetBackBrush(), bounds.X, bounds.Y, bounds.Width, 1);
			int relationshipHeight = this.dgTable.RelationshipHeight;
			Rectangle rectangle = new Rectangle(bounds.X, bounds.Y + 1, bounds.Width, relationshipHeight);
			int num = 1;
			int num2 = 0;
			while (num2 < this.dgTable.RelationsList.Count && num <= bounds.Height)
			{
				Brush brush = (this.dgTable.IsDefault ? base.DataGrid.LinkBrush : this.dgTable.LinkBrush);
				Font font = base.DataGrid.Font;
				Brush brush2 = (this.dgTable.IsDefault ? base.DataGrid.LinkBrush : this.dgTable.LinkBrush);
				font = base.DataGrid.LinkFont;
				g.FillRectangle(base.GetBackBrush(), rectangle);
				StringFormat stringFormat = new StringFormat();
				if (alignToRight)
				{
					stringFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
					stringFormat.Alignment = StringAlignment.Far;
				}
				g.DrawString((string)this.dgTable.RelationsList[num2], font, brush2, rectangle, stringFormat);
				if (num2 == this.FocusedRelation && this.number == base.DataGrid.CurrentCell.RowNumber)
				{
					rectangle.Width = this.dgTable.FocusedTextWidth;
					ControlPaint.DrawFocusRectangle(g, rectangle, ((SolidBrush)brush2).Color, ((SolidBrush)base.GetBackBrush()).Color);
					rectangle.Width = bounds.Width;
				}
				stringFormat.Dispose();
				rectangle.Y += relationshipHeight;
				num += rectangle.Height;
				num2++;
			}
			return num;
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x00053C74 File Offset: 0x00051E74
		private void PaintPlusMinusGlyph(Graphics g, Rectangle bounds, Brush backBr, bool alignToRight)
		{
			bool traceVerbose = CompModSwitches.DGRelationShpRowPaint.TraceVerbose;
			Rectangle rectangle = this.GetOutlineRect(bounds.X, bounds.Y);
			rectangle = Rectangle.Intersect(bounds, rectangle);
			if (rectangle.IsEmpty)
			{
				return;
			}
			g.FillRectangle(backBr, bounds);
			bool traceVerbose2 = CompModSwitches.DGRelationShpRowPaint.TraceVerbose;
			Pen pen = (this.dgTable.IsDefault ? base.DataGrid.HeaderForePen : this.dgTable.HeaderForePen);
			g.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
			int num = 2;
			g.DrawLine(pen, rectangle.X + num, rectangle.Y + rectangle.Width / 2, rectangle.Right - num - 1, rectangle.Y + rectangle.Width / 2);
			if (!this.expanded)
			{
				g.DrawLine(pen, rectangle.X + rectangle.Height / 2, rectangle.Y + num, rectangle.X + rectangle.Height / 2, rectangle.Bottom - num - 1);
				return;
			}
			Point[] array = new Point[3];
			array[0] = new Point(rectangle.X + rectangle.Height / 2, rectangle.Bottom);
			array[1] = new Point(array[0].X, bounds.Y + 2 * num + base.Height);
			array[2] = new Point(alignToRight ? bounds.X : bounds.Right, array[1].Y);
			g.DrawLines(pen, array);
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x00053E24 File Offset: 0x00052024
		private int RelationFromY(int y)
		{
			int num = -1;
			int relationshipHeight = this.dgTable.RelationshipHeight;
			Rectangle relationshipRect = this.GetRelationshipRect();
			int num2 = base.Height - this.dgTable.BorderWidth + 1;
			while (num2 < relationshipRect.Bottom && num2 <= y)
			{
				num2 += relationshipHeight;
				num++;
			}
			if (num >= this.dgTable.RelationsList.Count)
			{
				return -1;
			}
			return num;
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x00053E89 File Offset: 0x00052089
		private int MirrorRelationshipRectangle(Rectangle relRect, Rectangle rowHeader, bool alignToRight)
		{
			if (alignToRight)
			{
				return rowHeader.X - relRect.Width;
			}
			return relRect.X;
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x00053EA5 File Offset: 0x000520A5
		private int MirrorRectangle(int x, int width, Rectangle rect, bool alignToRight)
		{
			if (alignToRight)
			{
				return rect.Right + rect.X - width - x;
			}
			return x;
		}

		// Token: 0x04000A67 RID: 2663
		private const bool defaultOpen = false;

		// Token: 0x04000A68 RID: 2664
		private const int expandoBoxWidth = 14;

		// Token: 0x04000A69 RID: 2665
		private const int indentWidth = 20;

		// Token: 0x04000A6A RID: 2666
		private const int triangleSize = 5;

		// Token: 0x04000A6B RID: 2667
		private bool expanded;

		// Token: 0x0200064F RID: 1615
		[ComVisible(true)]
		protected class DataGridRelationshipRowAccessibleObject : DataGridRow.DataGridRowAccessibleObject
		{
			// Token: 0x060064E6 RID: 25830 RVA: 0x001776C6 File Offset: 0x001758C6
			public DataGridRelationshipRowAccessibleObject(DataGridRow owner)
				: base(owner)
			{
			}

			// Token: 0x060064E7 RID: 25831 RVA: 0x001776D0 File Offset: 0x001758D0
			protected override void AddChildAccessibleObjects(IList children)
			{
				base.AddChildAccessibleObjects(children);
				DataGridRelationshipRow dataGridRelationshipRow = (DataGridRelationshipRow)base.Owner;
				if (dataGridRelationshipRow.dgTable.RelationsList != null)
				{
					for (int i = 0; i < dataGridRelationshipRow.dgTable.RelationsList.Count; i++)
					{
						children.Add(new DataGridRelationshipRow.DataGridRelationshipAccessibleObject(dataGridRelationshipRow, i));
					}
				}
			}

			// Token: 0x170015AF RID: 5551
			// (get) Token: 0x060064E8 RID: 25832 RVA: 0x00177726 File Offset: 0x00175926
			private DataGridRelationshipRow RelationshipRow
			{
				get
				{
					return (DataGridRelationshipRow)base.Owner;
				}
			}

			// Token: 0x170015B0 RID: 5552
			// (get) Token: 0x060064E9 RID: 25833 RVA: 0x00177733 File Offset: 0x00175933
			public override string DefaultAction
			{
				get
				{
					if (this.RelationshipRow.dgTable.RelationsList.Count <= 0)
					{
						return null;
					}
					if (this.RelationshipRow.Expanded)
					{
						return SR.GetString("AccDGCollapse");
					}
					return SR.GetString("AccDGExpand");
				}
			}

			// Token: 0x170015B1 RID: 5553
			// (get) Token: 0x060064EA RID: 25834 RVA: 0x00177774 File Offset: 0x00175974
			public override AccessibleStates State
			{
				get
				{
					AccessibleStates accessibleStates = base.State;
					if (this.RelationshipRow.dgTable.RelationsList.Count > 0)
					{
						if (((DataGridRelationshipRow)base.Owner).Expanded)
						{
							accessibleStates |= AccessibleStates.Expanded;
						}
						else
						{
							accessibleStates |= AccessibleStates.Collapsed;
						}
					}
					return accessibleStates;
				}
			}

			// Token: 0x060064EB RID: 25835 RVA: 0x001777C5 File Offset: 0x001759C5
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void DoDefaultAction()
			{
				if (this.RelationshipRow.dgTable.RelationsList.Count > 0)
				{
					((DataGridRelationshipRow)base.Owner).Expanded = !((DataGridRelationshipRow)base.Owner).Expanded;
				}
			}

			// Token: 0x060064EC RID: 25836 RVA: 0x00177804 File Offset: 0x00175A04
			public override AccessibleObject GetFocused()
			{
				DataGridRelationshipRow dataGridRelationshipRow = (DataGridRelationshipRow)base.Owner;
				int focusedRelation = dataGridRelationshipRow.dgTable.FocusedRelation;
				if (focusedRelation == -1)
				{
					return base.GetFocused();
				}
				return this.GetChild(this.GetChildCount() - dataGridRelationshipRow.dgTable.RelationsList.Count + focusedRelation);
			}
		}

		// Token: 0x02000650 RID: 1616
		[ComVisible(true)]
		protected class DataGridRelationshipAccessibleObject : AccessibleObject
		{
			// Token: 0x060064ED RID: 25837 RVA: 0x00177853 File Offset: 0x00175A53
			public DataGridRelationshipAccessibleObject(DataGridRelationshipRow owner, int relationship)
			{
				this.owner = owner;
				this.relationship = relationship;
			}

			// Token: 0x170015B2 RID: 5554
			// (get) Token: 0x060064EE RID: 25838 RVA: 0x0017786C File Offset: 0x00175A6C
			public override Rectangle Bounds
			{
				get
				{
					Rectangle rowBounds = this.DataGrid.GetRowBounds(this.owner);
					Rectangle rectangle = (this.owner.Expanded ? this.owner.GetRelationshipRectWithMirroring() : Rectangle.Empty);
					rectangle.Y += this.owner.dgTable.RelationshipHeight * this.relationship;
					rectangle.Height = (this.owner.Expanded ? this.owner.dgTable.RelationshipHeight : 0);
					if (!this.owner.Expanded)
					{
						rectangle.X += rowBounds.X;
					}
					rectangle.Y += rowBounds.Y;
					return this.owner.DataGrid.RectangleToScreen(rectangle);
				}
			}

			// Token: 0x170015B3 RID: 5555
			// (get) Token: 0x060064EF RID: 25839 RVA: 0x0017793F File Offset: 0x00175B3F
			public override string Name
			{
				get
				{
					return (string)this.owner.dgTable.RelationsList[this.relationship];
				}
			}

			// Token: 0x170015B4 RID: 5556
			// (get) Token: 0x060064F0 RID: 25840 RVA: 0x00177961 File Offset: 0x00175B61
			protected DataGridRelationshipRow Owner
			{
				get
				{
					return this.owner;
				}
			}

			// Token: 0x170015B5 RID: 5557
			// (get) Token: 0x060064F1 RID: 25841 RVA: 0x00177969 File Offset: 0x00175B69
			public override AccessibleObject Parent
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return this.owner.AccessibleObject;
				}
			}

			// Token: 0x170015B6 RID: 5558
			// (get) Token: 0x060064F2 RID: 25842 RVA: 0x00177976 File Offset: 0x00175B76
			protected DataGrid DataGrid
			{
				get
				{
					return this.owner.DataGrid;
				}
			}

			// Token: 0x170015B7 RID: 5559
			// (get) Token: 0x060064F3 RID: 25843 RVA: 0x00177983 File Offset: 0x00175B83
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.Link;
				}
			}

			// Token: 0x170015B8 RID: 5560
			// (get) Token: 0x060064F4 RID: 25844 RVA: 0x00177988 File Offset: 0x00175B88
			public override AccessibleStates State
			{
				get
				{
					DataGridRow[] dataGridRows = this.DataGrid.DataGridRows;
					if (Array.IndexOf<DataGridRow>(dataGridRows, this.owner) == -1)
					{
						return AccessibleStates.Unavailable;
					}
					AccessibleStates accessibleStates = AccessibleStates.Focusable | AccessibleStates.Selectable | AccessibleStates.Linked;
					if (!this.owner.Expanded)
					{
						accessibleStates |= AccessibleStates.Invisible;
					}
					if (this.DataGrid.Focused && this.Owner.dgTable.FocusedRelation == this.relationship)
					{
						accessibleStates |= AccessibleStates.Focused;
					}
					return accessibleStates;
				}
			}

			// Token: 0x170015B9 RID: 5561
			// (get) Token: 0x060064F5 RID: 25845 RVA: 0x001779F8 File Offset: 0x00175BF8
			// (set) Token: 0x060064F6 RID: 25846 RVA: 0x000070A6 File Offset: 0x000052A6
			public override string Value
			{
				get
				{
					DataGridRow[] dataGridRows = this.DataGrid.DataGridRows;
					if (Array.IndexOf<DataGridRow>(dataGridRows, this.owner) == -1)
					{
						return null;
					}
					return (string)this.owner.dgTable.RelationsList[this.relationship];
				}
				set
				{
				}
			}

			// Token: 0x170015BA RID: 5562
			// (get) Token: 0x060064F7 RID: 25847 RVA: 0x00177A42 File Offset: 0x00175C42
			public override string DefaultAction
			{
				get
				{
					return SR.GetString("AccDGNavigate");
				}
			}

			// Token: 0x060064F8 RID: 25848 RVA: 0x00177A50 File Offset: 0x00175C50
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void DoDefaultAction()
			{
				this.Owner.Expanded = true;
				this.owner.FocusedRelation = -1;
				this.DataGrid.NavigateTo((string)this.owner.dgTable.RelationsList[this.relationship], this.owner, true);
				this.DataGrid.BeginInvoke(new MethodInvoker(this.ResetAccessibilityLayer));
			}

			// Token: 0x060064F9 RID: 25849 RVA: 0x00177AC0 File Offset: 0x00175CC0
			private void ResetAccessibilityLayer()
			{
				((DataGrid.DataGridAccessibleObject)this.DataGrid.AccessibilityObject).NotifyClients(AccessibleEvents.Reorder, 0);
				((DataGrid.DataGridAccessibleObject)this.DataGrid.AccessibilityObject).NotifyClients(AccessibleEvents.Focus, this.DataGrid.CurrentCellAccIndex);
				((DataGrid.DataGridAccessibleObject)this.DataGrid.AccessibilityObject).NotifyClients(AccessibleEvents.Selection, this.DataGrid.CurrentCellAccIndex);
			}

			// Token: 0x060064FA RID: 25850 RVA: 0x00177B34 File Offset: 0x00175D34
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override AccessibleObject Navigate(AccessibleNavigation navdir)
			{
				switch (navdir)
				{
				case AccessibleNavigation.Up:
				case AccessibleNavigation.Left:
				case AccessibleNavigation.Previous:
					if (this.relationship > 0)
					{
						return this.Parent.GetChild(this.Parent.GetChildCount() - this.owner.dgTable.RelationsList.Count + this.relationship - 1);
					}
					break;
				case AccessibleNavigation.Down:
				case AccessibleNavigation.Right:
				case AccessibleNavigation.Next:
					if (this.relationship + 1 < this.owner.dgTable.RelationsList.Count)
					{
						return this.Parent.GetChild(this.Parent.GetChildCount() - this.owner.dgTable.RelationsList.Count + this.relationship + 1);
					}
					break;
				}
				return null;
			}

			// Token: 0x060064FB RID: 25851 RVA: 0x00177BFB File Offset: 0x00175DFB
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void Select(AccessibleSelection flags)
			{
				if ((flags & AccessibleSelection.TakeFocus) == AccessibleSelection.TakeFocus)
				{
					this.DataGrid.Focus();
				}
				if ((flags & AccessibleSelection.TakeSelection) == AccessibleSelection.TakeSelection)
				{
					this.Owner.FocusedRelation = this.relationship;
				}
			}

			// Token: 0x040039D3 RID: 14803
			private DataGridRelationshipRow owner;

			// Token: 0x040039D4 RID: 14804
			private int relationship;
		}
	}
}
