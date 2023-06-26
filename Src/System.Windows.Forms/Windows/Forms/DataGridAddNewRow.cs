using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x0200017A RID: 378
	internal class DataGridAddNewRow : DataGridRow
	{
		// Token: 0x060015D3 RID: 5587 RVA: 0x0004EE0C File Offset: 0x0004D00C
		public DataGridAddNewRow(DataGrid dGrid, DataGridTableStyle gridTable, int rowNum)
			: base(dGrid, gridTable, rowNum)
		{
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x060015D4 RID: 5588 RVA: 0x0004EE17 File Offset: 0x0004D017
		// (set) Token: 0x060015D5 RID: 5589 RVA: 0x0004EE1F File Offset: 0x0004D01F
		public bool DataBound
		{
			get
			{
				return this.dataBound;
			}
			set
			{
				this.dataBound = value;
			}
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x0004EE28 File Offset: 0x0004D028
		public override void OnEdit()
		{
			if (!this.DataBound)
			{
				base.DataGrid.AddNewRow();
			}
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x0004EE3D File Offset: 0x0004D03D
		public override void OnRowLeave()
		{
			if (this.DataBound)
			{
				this.DataBound = false;
			}
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x000070A6 File Offset: 0x000052A6
		internal override void LoseChildFocus(Rectangle rowHeader, bool alignToRight)
		{
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x0001180C File Offset: 0x0000FA0C
		internal override bool ProcessTabKey(Keys keyData, Rectangle rowHeaders, bool alignToRight)
		{
			return false;
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x0004EE4E File Offset: 0x0004D04E
		public override int Paint(Graphics g, Rectangle bounds, Rectangle trueRowBounds, int firstVisibleColumn, int columnCount)
		{
			return this.Paint(g, bounds, trueRowBounds, firstVisibleColumn, columnCount, false);
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x0004EE60 File Offset: 0x0004D060
		public override int Paint(Graphics g, Rectangle bounds, Rectangle trueRowBounds, int firstVisibleColumn, int columnCount, bool alignToRight)
		{
			Rectangle rectangle = bounds;
			DataGridLineStyle dataGridLineStyle;
			if (this.dgTable.IsDefault)
			{
				dataGridLineStyle = base.DataGrid.GridLineStyle;
			}
			else
			{
				dataGridLineStyle = this.dgTable.GridLineStyle;
			}
			int num = ((base.DataGrid == null) ? 0 : ((dataGridLineStyle == DataGridLineStyle.Solid) ? 1 : 0));
			rectangle.Height -= num;
			int num2 = base.PaintData(g, rectangle, firstVisibleColumn, columnCount, alignToRight);
			if (num > 0)
			{
				this.PaintBottomBorder(g, bounds, num2, num, alignToRight);
			}
			return num2;
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x0004EEDC File Offset: 0x0004D0DC
		protected override void PaintCellContents(Graphics g, Rectangle cellBounds, DataGridColumnStyle column, Brush backBr, Brush foreBrush, bool alignToRight)
		{
			if (this.DataBound)
			{
				CurrencyManager listManager = base.DataGrid.ListManager;
				column.Paint(g, cellBounds, listManager, base.RowNumber, alignToRight);
				return;
			}
			base.PaintCellContents(g, cellBounds, column, backBr, foreBrush, alignToRight);
		}

		// Token: 0x04000A03 RID: 2563
		private bool dataBound;
	}
}
