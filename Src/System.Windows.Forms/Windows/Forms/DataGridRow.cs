using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x02000186 RID: 390
	internal abstract class DataGridRow : MarshalByRefObject
	{
		// Token: 0x06001730 RID: 5936 RVA: 0x00053EC0 File Offset: 0x000520C0
		public DataGridRow(DataGrid dataGrid, DataGridTableStyle dgTable, int rowNumber)
		{
			if (dataGrid == null || dgTable.DataGrid == null)
			{
				throw new ArgumentNullException("dataGrid");
			}
			if (rowNumber < 0)
			{
				throw new ArgumentException(SR.GetString("DataGridRowRowNumber"), "rowNumber");
			}
			this.number = rowNumber;
			DataGridRow.colorMap[0].OldColor = Color.Black;
			DataGridRow.colorMap[0].NewColor = dgTable.HeaderForeColor;
			this.dgTable = dgTable;
			this.height = this.MinimumRowHeight(dgTable);
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06001731 RID: 5937 RVA: 0x00053F57 File Offset: 0x00052157
		public AccessibleObject AccessibleObject
		{
			get
			{
				if (this.accessibleObject == null)
				{
					this.accessibleObject = this.CreateAccessibleObject();
				}
				return this.accessibleObject;
			}
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x00053F73 File Offset: 0x00052173
		protected virtual AccessibleObject CreateAccessibleObject()
		{
			return new DataGridRow.DataGridRowAccessibleObject(this);
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x00053F7B File Offset: 0x0005217B
		protected internal virtual int MinimumRowHeight(DataGridTableStyle dgTable)
		{
			return this.MinimumRowHeight(dgTable.GridColumnStyles);
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x00053F8C File Offset: 0x0005218C
		protected internal virtual int MinimumRowHeight(GridColumnStylesCollection columns)
		{
			int num = (this.dgTable.IsDefault ? this.DataGrid.PreferredRowHeight : this.dgTable.PreferredRowHeight);
			try
			{
				if (this.dgTable.DataGrid.DataSource != null)
				{
					int count = columns.Count;
					for (int i = 0; i < count; i++)
					{
						if (columns[i].PropertyDescriptor != null)
						{
							num = Math.Max(num, columns[i].GetMinimumHeight());
						}
					}
				}
			}
			catch
			{
			}
			return num;
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06001735 RID: 5941 RVA: 0x0005401C File Offset: 0x0005221C
		public DataGrid DataGrid
		{
			get
			{
				return this.dgTable.DataGrid;
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06001736 RID: 5942 RVA: 0x00054029 File Offset: 0x00052229
		// (set) Token: 0x06001737 RID: 5943 RVA: 0x00054031 File Offset: 0x00052231
		internal DataGridTableStyle DataGridTableStyle
		{
			get
			{
				return this.dgTable;
			}
			set
			{
				this.dgTable = value;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06001738 RID: 5944 RVA: 0x0005403A File Offset: 0x0005223A
		// (set) Token: 0x06001739 RID: 5945 RVA: 0x00054042 File Offset: 0x00052242
		public virtual int Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = Math.Max(0, value);
				this.dgTable.DataGrid.OnRowHeightChanged(this);
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x0600173A RID: 5946 RVA: 0x00054062 File Offset: 0x00052262
		public int RowNumber
		{
			get
			{
				return this.number;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x0600173B RID: 5947 RVA: 0x0005406A File Offset: 0x0005226A
		// (set) Token: 0x0600173C RID: 5948 RVA: 0x00054072 File Offset: 0x00052272
		public virtual bool Selected
		{
			get
			{
				return this.selected;
			}
			set
			{
				this.selected = value;
				this.InvalidateRow();
			}
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x00054084 File Offset: 0x00052284
		protected Bitmap GetBitmap(string bitmapName)
		{
			Bitmap bitmap = null;
			try
			{
				bitmap = new Bitmap(typeof(DataGridCaption), bitmapName);
				bitmap.MakeTransparent();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return bitmap;
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x000540C0 File Offset: 0x000522C0
		public virtual Rectangle GetCellBounds(int col)
		{
			int firstVisibleColumn = this.dgTable.DataGrid.FirstVisibleColumn;
			int num = 0;
			Rectangle rectangle = default(Rectangle);
			GridColumnStylesCollection gridColumnStyles = this.dgTable.GridColumnStyles;
			if (gridColumnStyles != null)
			{
				for (int i = firstVisibleColumn; i < col; i++)
				{
					if (gridColumnStyles[i].PropertyDescriptor != null)
					{
						num += gridColumnStyles[i].Width;
					}
				}
				int gridLineWidth = this.dgTable.GridLineWidth;
				rectangle = new Rectangle(num, 0, gridColumnStyles[col].Width - gridLineWidth, this.Height - gridLineWidth);
			}
			return rectangle;
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x00054155 File Offset: 0x00052355
		public virtual Rectangle GetNonScrollableArea()
		{
			return Rectangle.Empty;
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x0005415C File Offset: 0x0005235C
		protected Bitmap GetStarBitmap()
		{
			if (DataGridRow.starBmp == null)
			{
				DataGridRow.starBmp = this.GetBitmap("DataGridRow.star.bmp");
			}
			return DataGridRow.starBmp;
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x0005417A File Offset: 0x0005237A
		protected Bitmap GetPencilBitmap()
		{
			if (DataGridRow.pencilBmp == null)
			{
				DataGridRow.pencilBmp = this.GetBitmap("DataGridRow.pencil.bmp");
			}
			return DataGridRow.pencilBmp;
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x00054198 File Offset: 0x00052398
		protected Bitmap GetErrorBitmap()
		{
			if (DataGridRow.errorBmp == null)
			{
				DataGridRow.errorBmp = this.GetBitmap("DataGridRow.error.bmp");
			}
			DataGridRow.errorBmp.MakeTransparent();
			return DataGridRow.errorBmp;
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x000541C0 File Offset: 0x000523C0
		protected Bitmap GetLeftArrowBitmap()
		{
			if (DataGridRow.leftArrow == null)
			{
				DataGridRow.leftArrow = this.GetBitmap("DataGridRow.left.bmp");
			}
			return DataGridRow.leftArrow;
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x000541DE File Offset: 0x000523DE
		protected Bitmap GetRightArrowBitmap()
		{
			if (DataGridRow.rightArrow == null)
			{
				DataGridRow.rightArrow = this.GetBitmap("DataGridRow.right.bmp");
			}
			return DataGridRow.rightArrow;
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x000541FC File Offset: 0x000523FC
		public virtual void InvalidateRow()
		{
			this.dgTable.DataGrid.InvalidateRow(this.number);
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x00054214 File Offset: 0x00052414
		public virtual void InvalidateRowRect(Rectangle r)
		{
			this.dgTable.DataGrid.InvalidateRowRect(this.number, r);
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x000070A6 File Offset: 0x000052A6
		public virtual void OnEdit()
		{
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x00054230 File Offset: 0x00052430
		public virtual bool OnKeyPress(Keys keyData)
		{
			int columnNumber = this.dgTable.DataGrid.CurrentCell.ColumnNumber;
			GridColumnStylesCollection gridColumnStyles = this.dgTable.GridColumnStyles;
			if (gridColumnStyles != null && columnNumber >= 0 && columnNumber < gridColumnStyles.Count)
			{
				DataGridColumnStyle dataGridColumnStyle = gridColumnStyles[columnNumber];
				if (dataGridColumnStyle.KeyPress(this.RowNumber, keyData))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x0005428C File Offset: 0x0005248C
		public virtual bool OnMouseDown(int x, int y, Rectangle rowHeaders)
		{
			return this.OnMouseDown(x, y, rowHeaders, false);
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x00054298 File Offset: 0x00052498
		public virtual bool OnMouseDown(int x, int y, Rectangle rowHeaders, bool alignToRight)
		{
			this.LoseChildFocus(rowHeaders, alignToRight);
			return false;
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x0001180C File Offset: 0x0000FA0C
		public virtual bool OnMouseMove(int x, int y, Rectangle rowHeaders)
		{
			return false;
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x0001180C File Offset: 0x0000FA0C
		public virtual bool OnMouseMove(int x, int y, Rectangle rowHeaders, bool alignToRight)
		{
			return false;
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x000070A6 File Offset: 0x000052A6
		public virtual void OnMouseLeft(Rectangle rowHeaders, bool alignToRight)
		{
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x000070A6 File Offset: 0x000052A6
		public virtual void OnMouseLeft()
		{
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x000070A6 File Offset: 0x000052A6
		public virtual void OnRowEnter()
		{
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x000070A6 File Offset: 0x000052A6
		public virtual void OnRowLeave()
		{
		}

		// Token: 0x06001751 RID: 5969
		internal abstract bool ProcessTabKey(Keys keyData, Rectangle rowHeaders, bool alignToRight);

		// Token: 0x06001752 RID: 5970
		internal abstract void LoseChildFocus(Rectangle rowHeaders, bool alignToRight);

		// Token: 0x06001753 RID: 5971
		public abstract int Paint(Graphics g, Rectangle dataBounds, Rectangle rowBounds, int firstVisibleColumn, int numVisibleColumns);

		// Token: 0x06001754 RID: 5972
		public abstract int Paint(Graphics g, Rectangle dataBounds, Rectangle rowBounds, int firstVisibleColumn, int numVisibleColumns, bool alignToRight);

		// Token: 0x06001755 RID: 5973 RVA: 0x000542A4 File Offset: 0x000524A4
		protected virtual void PaintBottomBorder(Graphics g, Rectangle bounds, int dataWidth)
		{
			this.PaintBottomBorder(g, bounds, dataWidth, this.dgTable.GridLineWidth, false);
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x000542BC File Offset: 0x000524BC
		protected virtual void PaintBottomBorder(Graphics g, Rectangle bounds, int dataWidth, int borderWidth, bool alignToRight)
		{
			Rectangle rectangle = new Rectangle(alignToRight ? (bounds.Right - dataWidth) : bounds.X, bounds.Bottom - borderWidth, dataWidth, borderWidth);
			g.FillRectangle(this.dgTable.IsDefault ? this.DataGrid.GridLineBrush : this.dgTable.GridLineBrush, rectangle);
			if (dataWidth < bounds.Width)
			{
				g.FillRectangle(this.dgTable.DataGrid.BackgroundBrush, alignToRight ? bounds.X : rectangle.Right, rectangle.Y, bounds.Width - rectangle.Width, borderWidth);
			}
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x0005436B File Offset: 0x0005256B
		public virtual int PaintData(Graphics g, Rectangle bounds, int firstVisibleColumn, int columnCount)
		{
			return this.PaintData(g, bounds, firstVisibleColumn, columnCount, false);
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x0005437C File Offset: 0x0005257C
		public virtual int PaintData(Graphics g, Rectangle bounds, int firstVisibleColumn, int columnCount, bool alignToRight)
		{
			Rectangle rectangle = bounds;
			int num = (this.dgTable.IsDefault ? this.DataGrid.GridLineWidth : this.dgTable.GridLineWidth);
			int num2 = 0;
			DataGridCell currentCell = this.dgTable.DataGrid.CurrentCell;
			GridColumnStylesCollection gridColumnStyles = this.dgTable.GridColumnStyles;
			int count = gridColumnStyles.Count;
			int num3 = firstVisibleColumn;
			while (num3 < count && num2 <= bounds.Width)
			{
				if (gridColumnStyles[num3].PropertyDescriptor != null && gridColumnStyles[num3].Width > 0)
				{
					rectangle.Width = gridColumnStyles[num3].Width - num;
					if (alignToRight)
					{
						rectangle.X = bounds.Right - num2 - rectangle.Width;
					}
					else
					{
						rectangle.X = bounds.X + num2;
					}
					Brush brush = this.BackBrushForDataPaint(ref currentCell, gridColumnStyles[num3], num3);
					Brush brush2 = this.ForeBrushForDataPaint(ref currentCell, gridColumnStyles[num3], num3);
					this.PaintCellContents(g, rectangle, gridColumnStyles[num3], brush, brush2, alignToRight);
					if (num > 0)
					{
						g.FillRectangle(this.dgTable.IsDefault ? this.DataGrid.GridLineBrush : this.dgTable.GridLineBrush, alignToRight ? (rectangle.X - num) : rectangle.Right, rectangle.Y, num, rectangle.Height);
					}
					num2 += rectangle.Width + num;
				}
				num3++;
			}
			if (num2 < bounds.Width)
			{
				g.FillRectangle(this.dgTable.DataGrid.BackgroundBrush, alignToRight ? bounds.X : (bounds.X + num2), bounds.Y, bounds.Width - num2, bounds.Height);
			}
			return num2;
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x00054559 File Offset: 0x00052759
		protected virtual void PaintCellContents(Graphics g, Rectangle cellBounds, DataGridColumnStyle column, Brush backBr, Brush foreBrush)
		{
			this.PaintCellContents(g, cellBounds, column, backBr, foreBrush, false);
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x00054569 File Offset: 0x00052769
		protected virtual void PaintCellContents(Graphics g, Rectangle cellBounds, DataGridColumnStyle column, Brush backBr, Brush foreBrush, bool alignToRight)
		{
			g.FillRectangle(backBr, cellBounds);
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x00054574 File Offset: 0x00052774
		protected Rectangle PaintIcon(Graphics g, Rectangle visualBounds, bool paintIcon, bool alignToRight, Bitmap bmp)
		{
			return this.PaintIcon(g, visualBounds, paintIcon, alignToRight, bmp, this.dgTable.IsDefault ? this.DataGrid.HeaderBackBrush : this.dgTable.HeaderBackBrush);
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x000545A8 File Offset: 0x000527A8
		protected Rectangle PaintIcon(Graphics g, Rectangle visualBounds, bool paintIcon, bool alignToRight, Bitmap bmp, Brush backBrush)
		{
			Size size = bmp.Size;
			Rectangle rectangle = new Rectangle(alignToRight ? (visualBounds.Right - 3 - size.Width) : (visualBounds.X + 3), visualBounds.Y + 2, size.Width, size.Height);
			g.FillRectangle(backBrush, visualBounds);
			if (paintIcon)
			{
				DataGridRow.colorMap[0].NewColor = (this.dgTable.IsDefault ? this.DataGrid.HeaderForeColor : this.dgTable.HeaderForeColor);
				DataGridRow.colorMap[0].OldColor = Color.Black;
				ImageAttributes imageAttributes = new ImageAttributes();
				imageAttributes.SetRemapTable(DataGridRow.colorMap, ColorAdjustType.Bitmap);
				g.DrawImage(bmp, rectangle, 0, 0, rectangle.Width, rectangle.Height, GraphicsUnit.Pixel, imageAttributes);
				imageAttributes.Dispose();
			}
			return rectangle;
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x0005467D File Offset: 0x0005287D
		public virtual void PaintHeader(Graphics g, Rectangle visualBounds)
		{
			this.PaintHeader(g, visualBounds, false);
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x00054688 File Offset: 0x00052888
		public virtual void PaintHeader(Graphics g, Rectangle visualBounds, bool alignToRight)
		{
			this.PaintHeader(g, visualBounds, alignToRight, false);
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x00054694 File Offset: 0x00052894
		public virtual void PaintHeader(Graphics g, Rectangle visualBounds, bool alignToRight, bool rowIsDirty)
		{
			Rectangle rectangle = visualBounds;
			Bitmap bitmap;
			if (this is DataGridAddNewRow)
			{
				bitmap = this.GetStarBitmap();
				Bitmap bitmap2 = bitmap;
				lock (bitmap2)
				{
					rectangle.X += this.PaintIcon(g, rectangle, true, alignToRight, bitmap).Width + 3;
				}
				return;
			}
			if (rowIsDirty)
			{
				bitmap = this.GetPencilBitmap();
				Bitmap bitmap3 = bitmap;
				lock (bitmap3)
				{
					rectangle.X += this.PaintIcon(g, rectangle, this.RowNumber == this.DataGrid.CurrentCell.RowNumber, alignToRight, bitmap).Width + 3;
					goto IL_128;
				}
			}
			bitmap = (alignToRight ? this.GetLeftArrowBitmap() : this.GetRightArrowBitmap());
			Bitmap bitmap4 = bitmap;
			lock (bitmap4)
			{
				rectangle.X += this.PaintIcon(g, rectangle, this.RowNumber == this.DataGrid.CurrentCell.RowNumber, alignToRight, bitmap).Width + 3;
			}
			IL_128:
			object obj = this.DataGrid.ListManager[this.number];
			if (!(obj is IDataErrorInfo))
			{
				return;
			}
			string text = ((IDataErrorInfo)obj).Error;
			if (text == null)
			{
				text = string.Empty;
			}
			if (this.tooltip != text && !string.IsNullOrEmpty(this.tooltip))
			{
				this.DataGrid.ToolTipProvider.RemoveToolTip(this.tooltipID);
				this.tooltip = string.Empty;
				this.tooltipID = new IntPtr(-1);
			}
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			bitmap = this.GetErrorBitmap();
			Bitmap bitmap5 = bitmap;
			Rectangle rectangle2;
			lock (bitmap5)
			{
				rectangle2 = this.PaintIcon(g, rectangle, true, alignToRight, bitmap);
			}
			rectangle.X += rectangle2.Width + 3;
			this.tooltip = text;
			DataGrid dataGrid = this.DataGrid;
			int toolTipId = dataGrid.ToolTipId;
			dataGrid.ToolTipId = toolTipId + 1;
			this.tooltipID = (IntPtr)toolTipId;
			this.DataGrid.ToolTipProvider.AddToolTip(this.tooltip, this.tooltipID, rectangle2);
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x00054914 File Offset: 0x00052B14
		protected Brush GetBackBrush()
		{
			Brush brush = (this.dgTable.IsDefault ? this.DataGrid.BackBrush : this.dgTable.BackBrush);
			if (this.DataGrid.LedgerStyle && this.RowNumber % 2 == 1)
			{
				brush = (this.dgTable.IsDefault ? this.DataGrid.AlternatingBackBrush : this.dgTable.AlternatingBackBrush);
			}
			return brush;
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x00054988 File Offset: 0x00052B88
		protected Brush BackBrushForDataPaint(ref DataGridCell current, DataGridColumnStyle gridColumn, int column)
		{
			Brush brush = this.GetBackBrush();
			if (this.Selected)
			{
				brush = (this.dgTable.IsDefault ? this.DataGrid.SelectionBackBrush : this.dgTable.SelectionBackBrush);
			}
			return brush;
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x000549CC File Offset: 0x00052BCC
		protected Brush ForeBrushForDataPaint(ref DataGridCell current, DataGridColumnStyle gridColumn, int column)
		{
			Brush brush = (this.dgTable.IsDefault ? this.DataGrid.ForeBrush : this.dgTable.ForeBrush);
			if (this.Selected)
			{
				brush = (this.dgTable.IsDefault ? this.DataGrid.SelectionForeBrush : this.dgTable.SelectionForeBrush);
			}
			return brush;
		}

		// Token: 0x04000A6C RID: 2668
		protected internal int number;

		// Token: 0x04000A6D RID: 2669
		private bool selected;

		// Token: 0x04000A6E RID: 2670
		private int height;

		// Token: 0x04000A6F RID: 2671
		private IntPtr tooltipID = new IntPtr(-1);

		// Token: 0x04000A70 RID: 2672
		private string tooltip = string.Empty;

		// Token: 0x04000A71 RID: 2673
		private AccessibleObject accessibleObject;

		// Token: 0x04000A72 RID: 2674
		protected DataGridTableStyle dgTable;

		// Token: 0x04000A73 RID: 2675
		private static ColorMap[] colorMap = new ColorMap[]
		{
			new ColorMap()
		};

		// Token: 0x04000A74 RID: 2676
		private static Bitmap rightArrow = null;

		// Token: 0x04000A75 RID: 2677
		private static Bitmap leftArrow = null;

		// Token: 0x04000A76 RID: 2678
		private static Bitmap errorBmp = null;

		// Token: 0x04000A77 RID: 2679
		private static Bitmap pencilBmp = null;

		// Token: 0x04000A78 RID: 2680
		private static Bitmap starBmp = null;

		// Token: 0x04000A79 RID: 2681
		protected const int xOffset = 3;

		// Token: 0x04000A7A RID: 2682
		protected const int yOffset = 2;

		// Token: 0x02000651 RID: 1617
		[ComVisible(true)]
		protected class DataGridRowAccessibleObject : AccessibleObject
		{
			// Token: 0x060064FC RID: 25852 RVA: 0x00177C28 File Offset: 0x00175E28
			internal static string CellToDisplayString(DataGrid grid, int row, int column)
			{
				if (column < grid.myGridTable.GridColumnStyles.Count)
				{
					return grid.myGridTable.GridColumnStyles[column].PropertyDescriptor.Converter.ConvertToString(grid[row, column]);
				}
				return "";
			}

			// Token: 0x060064FD RID: 25853 RVA: 0x00177C76 File Offset: 0x00175E76
			internal static object DisplayStringToCell(DataGrid grid, int row, int column, string value)
			{
				if (column < grid.myGridTable.GridColumnStyles.Count)
				{
					return grid.myGridTable.GridColumnStyles[column].PropertyDescriptor.Converter.ConvertFromString(value);
				}
				return null;
			}

			// Token: 0x060064FE RID: 25854 RVA: 0x00177CB0 File Offset: 0x00175EB0
			public DataGridRowAccessibleObject(DataGridRow owner)
			{
				this.owner = owner;
				DataGrid dataGrid = this.DataGrid;
				this.EnsureChildren();
			}

			// Token: 0x060064FF RID: 25855 RVA: 0x00177CD7 File Offset: 0x00175ED7
			private void EnsureChildren()
			{
				if (this.cells == null)
				{
					this.cells = new ArrayList(this.DataGrid.myGridTable.GridColumnStyles.Count + 2);
					this.AddChildAccessibleObjects(this.cells);
				}
			}

			// Token: 0x06006500 RID: 25856 RVA: 0x00177D10 File Offset: 0x00175F10
			protected virtual void AddChildAccessibleObjects(IList children)
			{
				GridColumnStylesCollection gridColumnStyles = this.DataGrid.myGridTable.GridColumnStyles;
				int count = gridColumnStyles.Count;
				for (int i = 0; i < count; i++)
				{
					children.Add(this.CreateCellAccessibleObject(i));
				}
			}

			// Token: 0x06006501 RID: 25857 RVA: 0x00177D4F File Offset: 0x00175F4F
			protected virtual AccessibleObject CreateCellAccessibleObject(int column)
			{
				return new DataGridRow.DataGridCellAccessibleObject(this.owner, column);
			}

			// Token: 0x170015BB RID: 5563
			// (get) Token: 0x06006502 RID: 25858 RVA: 0x00177D5D File Offset: 0x00175F5D
			public override Rectangle Bounds
			{
				get
				{
					return this.DataGrid.RectangleToScreen(this.DataGrid.GetRowBounds(this.owner));
				}
			}

			// Token: 0x170015BC RID: 5564
			// (get) Token: 0x06006503 RID: 25859 RVA: 0x00177D7B File Offset: 0x00175F7B
			public override string Name
			{
				get
				{
					if (this.owner is DataGridAddNewRow)
					{
						return SR.GetString("AccDGNewRow");
					}
					return DataGridRow.DataGridRowAccessibleObject.CellToDisplayString(this.DataGrid, this.owner.RowNumber, 0);
				}
			}

			// Token: 0x170015BD RID: 5565
			// (get) Token: 0x06006504 RID: 25860 RVA: 0x00177DAC File Offset: 0x00175FAC
			protected DataGridRow Owner
			{
				get
				{
					return this.owner;
				}
			}

			// Token: 0x170015BE RID: 5566
			// (get) Token: 0x06006505 RID: 25861 RVA: 0x00177DB4 File Offset: 0x00175FB4
			public override AccessibleObject Parent
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return this.DataGrid.AccessibilityObject;
				}
			}

			// Token: 0x170015BF RID: 5567
			// (get) Token: 0x06006506 RID: 25862 RVA: 0x00177DC1 File Offset: 0x00175FC1
			private DataGrid DataGrid
			{
				get
				{
					return this.owner.DataGrid;
				}
			}

			// Token: 0x170015C0 RID: 5568
			// (get) Token: 0x06006507 RID: 25863 RVA: 0x00177DCE File Offset: 0x00175FCE
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.Row;
				}
			}

			// Token: 0x170015C1 RID: 5569
			// (get) Token: 0x06006508 RID: 25864 RVA: 0x00177DD4 File Offset: 0x00175FD4
			public override AccessibleStates State
			{
				get
				{
					AccessibleStates accessibleStates = AccessibleStates.Focusable | AccessibleStates.Selectable;
					if (this.DataGrid.CurrentCell.RowNumber == this.owner.RowNumber)
					{
						accessibleStates |= AccessibleStates.Focused;
					}
					if (this.DataGrid.CurrentRowIndex == this.owner.RowNumber)
					{
						accessibleStates |= AccessibleStates.Selected;
					}
					return accessibleStates;
				}
			}

			// Token: 0x170015C2 RID: 5570
			// (get) Token: 0x06006509 RID: 25865 RVA: 0x00016178 File Offset: 0x00014378
			public override string Value
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return this.Name;
				}
			}

			// Token: 0x0600650A RID: 25866 RVA: 0x00177E28 File Offset: 0x00176028
			public override AccessibleObject GetChild(int index)
			{
				if (index < this.cells.Count)
				{
					return (AccessibleObject)this.cells[index];
				}
				return null;
			}

			// Token: 0x0600650B RID: 25867 RVA: 0x00177E4B File Offset: 0x0017604B
			public override int GetChildCount()
			{
				return this.cells.Count;
			}

			// Token: 0x0600650C RID: 25868 RVA: 0x00177E58 File Offset: 0x00176058
			public override AccessibleObject GetFocused()
			{
				if (this.DataGrid.Focused)
				{
					DataGridCell currentCell = this.DataGrid.CurrentCell;
					if (currentCell.RowNumber == this.owner.RowNumber)
					{
						return (AccessibleObject)this.cells[currentCell.ColumnNumber];
					}
				}
				return null;
			}

			// Token: 0x0600650D RID: 25869 RVA: 0x00177EAC File Offset: 0x001760AC
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override AccessibleObject Navigate(AccessibleNavigation navdir)
			{
				switch (navdir)
				{
				case AccessibleNavigation.Up:
				case AccessibleNavigation.Left:
				case AccessibleNavigation.Previous:
					return this.DataGrid.AccessibilityObject.GetChild(1 + this.owner.dgTable.GridColumnStyles.Count + this.owner.RowNumber - 1);
				case AccessibleNavigation.Down:
				case AccessibleNavigation.Right:
				case AccessibleNavigation.Next:
					return this.DataGrid.AccessibilityObject.GetChild(1 + this.owner.dgTable.GridColumnStyles.Count + this.owner.RowNumber + 1);
				case AccessibleNavigation.FirstChild:
					if (this.GetChildCount() > 0)
					{
						return this.GetChild(0);
					}
					break;
				case AccessibleNavigation.LastChild:
					if (this.GetChildCount() > 0)
					{
						return this.GetChild(this.GetChildCount() - 1);
					}
					break;
				}
				return null;
			}

			// Token: 0x0600650E RID: 25870 RVA: 0x00177F7C File Offset: 0x0017617C
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void Select(AccessibleSelection flags)
			{
				if ((flags & AccessibleSelection.TakeFocus) == AccessibleSelection.TakeFocus)
				{
					this.DataGrid.Focus();
				}
				if ((flags & AccessibleSelection.TakeSelection) == AccessibleSelection.TakeSelection)
				{
					this.DataGrid.CurrentRowIndex = this.owner.RowNumber;
				}
			}

			// Token: 0x040039D5 RID: 14805
			private ArrayList cells;

			// Token: 0x040039D6 RID: 14806
			private DataGridRow owner;
		}

		// Token: 0x02000652 RID: 1618
		[ComVisible(true)]
		protected class DataGridCellAccessibleObject : AccessibleObject
		{
			// Token: 0x0600650F RID: 25871 RVA: 0x00177FAC File Offset: 0x001761AC
			public DataGridCellAccessibleObject(DataGridRow owner, int column)
			{
				this.owner = owner;
				this.column = column;
			}

			// Token: 0x170015C3 RID: 5571
			// (get) Token: 0x06006510 RID: 25872 RVA: 0x00177FC2 File Offset: 0x001761C2
			public override Rectangle Bounds
			{
				get
				{
					return this.DataGrid.RectangleToScreen(this.DataGrid.GetCellBounds(new DataGridCell(this.owner.RowNumber, this.column)));
				}
			}

			// Token: 0x170015C4 RID: 5572
			// (get) Token: 0x06006511 RID: 25873 RVA: 0x00177FF0 File Offset: 0x001761F0
			public override string Name
			{
				get
				{
					return this.DataGrid.myGridTable.GridColumnStyles[this.column].HeaderText;
				}
			}

			// Token: 0x170015C5 RID: 5573
			// (get) Token: 0x06006512 RID: 25874 RVA: 0x00178012 File Offset: 0x00176212
			public override AccessibleObject Parent
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return this.owner.AccessibleObject;
				}
			}

			// Token: 0x170015C6 RID: 5574
			// (get) Token: 0x06006513 RID: 25875 RVA: 0x0017801F File Offset: 0x0017621F
			protected DataGrid DataGrid
			{
				get
				{
					return this.owner.DataGrid;
				}
			}

			// Token: 0x170015C7 RID: 5575
			// (get) Token: 0x06006514 RID: 25876 RVA: 0x0017802C File Offset: 0x0017622C
			public override string DefaultAction
			{
				get
				{
					return SR.GetString("AccDGEdit");
				}
			}

			// Token: 0x170015C8 RID: 5576
			// (get) Token: 0x06006515 RID: 25877 RVA: 0x00178038 File Offset: 0x00176238
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.Cell;
				}
			}

			// Token: 0x170015C9 RID: 5577
			// (get) Token: 0x06006516 RID: 25878 RVA: 0x0017803C File Offset: 0x0017623C
			public override AccessibleStates State
			{
				get
				{
					AccessibleStates accessibleStates = AccessibleStates.Focusable | AccessibleStates.Selectable;
					if (this.DataGrid.CurrentCell.RowNumber == this.owner.RowNumber && this.DataGrid.CurrentCell.ColumnNumber == this.column)
					{
						if (this.DataGrid.Focused)
						{
							accessibleStates |= AccessibleStates.Focused;
						}
						accessibleStates |= AccessibleStates.Selected;
					}
					return accessibleStates;
				}
			}

			// Token: 0x170015CA RID: 5578
			// (get) Token: 0x06006517 RID: 25879 RVA: 0x001780A0 File Offset: 0x001762A0
			// (set) Token: 0x06006518 RID: 25880 RVA: 0x001780D0 File Offset: 0x001762D0
			public override string Value
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					if (this.owner is DataGridAddNewRow)
					{
						return null;
					}
					return DataGridRow.DataGridRowAccessibleObject.CellToDisplayString(this.DataGrid, this.owner.RowNumber, this.column);
				}
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				set
				{
					if (!(this.owner is DataGridAddNewRow))
					{
						object obj = DataGridRow.DataGridRowAccessibleObject.DisplayStringToCell(this.DataGrid, this.owner.RowNumber, this.column, value);
						this.DataGrid[this.owner.RowNumber, this.column] = obj;
					}
				}
			}

			// Token: 0x06006519 RID: 25881 RVA: 0x00178125 File Offset: 0x00176325
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void DoDefaultAction()
			{
				this.Select(AccessibleSelection.TakeFocus | AccessibleSelection.TakeSelection);
			}

			// Token: 0x0600651A RID: 25882 RVA: 0x0017812E File Offset: 0x0017632E
			public override AccessibleObject GetFocused()
			{
				return this.DataGrid.AccessibilityObject.GetFocused();
			}

			// Token: 0x0600651B RID: 25883 RVA: 0x00178140 File Offset: 0x00176340
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override AccessibleObject Navigate(AccessibleNavigation navdir)
			{
				switch (navdir)
				{
				case AccessibleNavigation.Up:
					return this.DataGrid.AccessibilityObject.GetChild(1 + this.owner.dgTable.GridColumnStyles.Count + this.owner.RowNumber - 1).Navigate(AccessibleNavigation.FirstChild);
				case AccessibleNavigation.Down:
					return this.DataGrid.AccessibilityObject.GetChild(1 + this.owner.dgTable.GridColumnStyles.Count + this.owner.RowNumber + 1).Navigate(AccessibleNavigation.FirstChild);
				case AccessibleNavigation.Left:
				case AccessibleNavigation.Previous:
				{
					if (this.column > 0)
					{
						return this.owner.AccessibleObject.GetChild(this.column - 1);
					}
					AccessibleObject child = this.DataGrid.AccessibilityObject.GetChild(1 + this.owner.dgTable.GridColumnStyles.Count + this.owner.RowNumber - 1);
					if (child != null)
					{
						return child.Navigate(AccessibleNavigation.LastChild);
					}
					break;
				}
				case AccessibleNavigation.Right:
				case AccessibleNavigation.Next:
				{
					if (this.column < this.owner.AccessibleObject.GetChildCount() - 1)
					{
						return this.owner.AccessibleObject.GetChild(this.column + 1);
					}
					AccessibleObject child2 = this.DataGrid.AccessibilityObject.GetChild(1 + this.owner.dgTable.GridColumnStyles.Count + this.owner.RowNumber + 1);
					if (child2 != null)
					{
						return child2.Navigate(AccessibleNavigation.FirstChild);
					}
					break;
				}
				}
				return null;
			}

			// Token: 0x0600651C RID: 25884 RVA: 0x001782CD File Offset: 0x001764CD
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void Select(AccessibleSelection flags)
			{
				if ((flags & AccessibleSelection.TakeFocus) == AccessibleSelection.TakeFocus)
				{
					this.DataGrid.Focus();
				}
				if ((flags & AccessibleSelection.TakeSelection) == AccessibleSelection.TakeSelection)
				{
					this.DataGrid.CurrentCell = new DataGridCell(this.owner.RowNumber, this.column);
				}
			}

			// Token: 0x040039D7 RID: 14807
			private DataGridRow owner;

			// Token: 0x040039D8 RID: 14808
			private int column;
		}
	}
}
