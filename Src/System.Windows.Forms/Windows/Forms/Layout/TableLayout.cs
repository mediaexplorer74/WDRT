using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;

namespace System.Windows.Forms.Layout
{
	// Token: 0x020004D0 RID: 1232
	internal class TableLayout : LayoutEngine
	{
		// Token: 0x060050D6 RID: 20694 RVA: 0x00150559 File Offset: 0x0014E759
		private static int GetMedian(int low, int hi)
		{
			return low + (hi - low >> 1);
		}

		// Token: 0x060050D7 RID: 20695 RVA: 0x00150564 File Offset: 0x0014E764
		private static void Sort(object[] array, IComparer comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Length > 1)
			{
				TableLayout.SorterObjectArray sorterObjectArray = new TableLayout.SorterObjectArray(array, comparer);
				sorterObjectArray.QuickSort(0, array.Length - 1);
			}
		}

		// Token: 0x060050D8 RID: 20696 RVA: 0x0015059B File Offset: 0x0014E79B
		internal static TableLayoutSettings CreateSettings(IArrangedElement owner)
		{
			return new TableLayoutSettings(owner);
		}

		// Token: 0x060050D9 RID: 20697 RVA: 0x001505A4 File Offset: 0x0014E7A4
		internal override void ProcessSuspendedLayoutEventArgs(IArrangedElement container, LayoutEventArgs args)
		{
			TableLayout.ContainerInfo containerInfo = TableLayout.GetContainerInfo(container);
			foreach (string text in TableLayout._propertiesWhichInvalidateCache)
			{
				if (args.AffectedProperty == text)
				{
					TableLayout.ClearCachedAssignments(containerInfo);
					return;
				}
			}
		}

		// Token: 0x060050DA RID: 20698 RVA: 0x001505E0 File Offset: 0x0014E7E0
		internal override bool LayoutCore(IArrangedElement container, LayoutEventArgs args)
		{
			this.ProcessSuspendedLayoutEventArgs(container, args);
			TableLayout.ContainerInfo containerInfo = TableLayout.GetContainerInfo(container);
			this.EnsureRowAndColumnAssignments(container, containerInfo, false);
			int cellBorderWidth = containerInfo.CellBorderWidth;
			Size size = container.DisplayRectangle.Size - new Size(cellBorderWidth, cellBorderWidth);
			size.Width = Math.Max(size.Width, 1);
			size.Height = Math.Max(size.Height, 1);
			Size size2 = this.ApplyStyles(containerInfo, size, false);
			this.ExpandLastElement(containerInfo, size2, size);
			RectangleF rectangleF = container.DisplayRectangle;
			rectangleF.Inflate(-((float)cellBorderWidth / 2f), (float)(-(float)cellBorderWidth) / 2f);
			this.SetElementBounds(containerInfo, rectangleF);
			CommonProperties.SetLayoutBounds(containerInfo.Container, new Size(this.SumStrips(containerInfo.Columns, 0, containerInfo.Columns.Length), this.SumStrips(containerInfo.Rows, 0, containerInfo.Rows.Length)));
			return CommonProperties.GetAutoSize(container);
		}

		// Token: 0x060050DB RID: 20699 RVA: 0x001506D4 File Offset: 0x0014E8D4
		internal override Size GetPreferredSize(IArrangedElement container, Size proposedConstraints)
		{
			TableLayout.ContainerInfo containerInfo = TableLayout.GetContainerInfo(container);
			bool flag = false;
			float num = -1f;
			Size size = containerInfo.GetCachedPreferredSize(proposedConstraints, out flag);
			if (flag)
			{
				return size;
			}
			TableLayout.ContainerInfo containerInfo2 = new TableLayout.ContainerInfo(containerInfo);
			int cellBorderWidth = containerInfo.CellBorderWidth;
			if (containerInfo.MaxColumns == 1 && containerInfo.ColumnStyles.Count > 0 && containerInfo.ColumnStyles[0].SizeType == SizeType.Absolute)
			{
				Size size2 = container.DisplayRectangle.Size - new Size(cellBorderWidth * 2, cellBorderWidth * 2);
				size2.Width = Math.Max(size2.Width, 1);
				size2.Height = Math.Max(size2.Height, 1);
				num = containerInfo.ColumnStyles[0].Size;
				containerInfo.ColumnStyles[0].SetSize(Math.Max(num, (float)Math.Min(proposedConstraints.Width, size2.Width)));
			}
			this.EnsureRowAndColumnAssignments(container, containerInfo2, true);
			Size size3 = new Size(cellBorderWidth, cellBorderWidth);
			proposedConstraints -= size3;
			proposedConstraints.Width = Math.Max(proposedConstraints.Width, 1);
			proposedConstraints.Height = Math.Max(proposedConstraints.Height, 1);
			if (containerInfo2.Columns != null && containerInfo.Columns != null && containerInfo2.Columns.Length != containerInfo.Columns.Length)
			{
				TableLayout.ClearCachedAssignments(containerInfo);
			}
			if (containerInfo2.Rows != null && containerInfo.Rows != null && containerInfo2.Rows.Length != containerInfo.Rows.Length)
			{
				TableLayout.ClearCachedAssignments(containerInfo);
			}
			size = this.ApplyStyles(containerInfo2, proposedConstraints, true);
			if (num >= 0f)
			{
				containerInfo.ColumnStyles[0].SetSize(num);
			}
			return size + size3;
		}

		// Token: 0x060050DC RID: 20700 RVA: 0x00150893 File Offset: 0x0014EA93
		private void EnsureRowAndColumnAssignments(IArrangedElement container, TableLayout.ContainerInfo containerInfo, bool doNotCache)
		{
			if (!TableLayout.HasCachedAssignments(containerInfo) || doNotCache)
			{
				this.AssignRowsAndColumns(containerInfo);
			}
		}

		// Token: 0x060050DD RID: 20701 RVA: 0x001508AC File Offset: 0x0014EAAC
		private void ExpandLastElement(TableLayout.ContainerInfo containerInfo, Size usedSpace, Size totalSpace)
		{
			TableLayout.Strip[] rows = containerInfo.Rows;
			TableLayout.Strip[] columns = containerInfo.Columns;
			if (columns.Length != 0 && totalSpace.Width > usedSpace.Width)
			{
				TableLayout.Strip[] array = columns;
				int num = columns.Length - 1;
				array[num].MinSize = array[num].MinSize + (totalSpace.Width - usedSpace.Width);
			}
			if (rows.Length != 0 && totalSpace.Height > usedSpace.Height)
			{
				TableLayout.Strip[] array2 = rows;
				int num2 = rows.Length - 1;
				array2[num2].MinSize = array2[num2].MinSize + (totalSpace.Height - usedSpace.Height);
			}
		}

		// Token: 0x060050DE RID: 20702 RVA: 0x0015093C File Offset: 0x0014EB3C
		private void AssignRowsAndColumns(TableLayout.ContainerInfo containerInfo)
		{
			int num = containerInfo.MaxColumns;
			int num2 = containerInfo.MaxRows;
			TableLayout.LayoutInfo[] childrenInfo = containerInfo.ChildrenInfo;
			int minRowsAndColumns = containerInfo.MinRowsAndColumns;
			int minColumns = containerInfo.MinColumns;
			int minRows = containerInfo.MinRows;
			TableLayoutPanelGrowStyle growStyle = containerInfo.GrowStyle;
			if (growStyle == TableLayoutPanelGrowStyle.FixedSize)
			{
				if (containerInfo.MinRowsAndColumns > num * num2)
				{
					throw new ArgumentException(SR.GetString("TableLayoutPanelFullDesc"));
				}
				if (minColumns > num || minRows > num2)
				{
					throw new ArgumentException(SR.GetString("TableLayoutPanelSpanDesc"));
				}
				num2 = Math.Max(1, num2);
				num = Math.Max(1, num);
			}
			else if (growStyle == TableLayoutPanelGrowStyle.AddRows)
			{
				num2 = 0;
			}
			else
			{
				num = 0;
			}
			if (num > 0)
			{
				this.xAssignRowsAndColumns(containerInfo, childrenInfo, num, (num2 == 0) ? int.MaxValue : num2, growStyle);
				return;
			}
			if (num2 > 0)
			{
				int num3 = Math.Max((int)Math.Ceiling((double)((float)minRowsAndColumns / (float)num2)), minColumns);
				num3 = Math.Max(num3, 1);
				while (!this.xAssignRowsAndColumns(containerInfo, childrenInfo, num3, num2, growStyle))
				{
					num3++;
				}
				return;
			}
			this.xAssignRowsAndColumns(containerInfo, childrenInfo, Math.Max(minColumns, 1), int.MaxValue, growStyle);
		}

		// Token: 0x060050DF RID: 20703 RVA: 0x00150A44 File Offset: 0x0014EC44
		private bool xAssignRowsAndColumns(TableLayout.ContainerInfo containerInfo, TableLayout.LayoutInfo[] childrenInfo, int maxColumns, int maxRows, TableLayoutPanelGrowStyle growStyle)
		{
			int num = 0;
			int num2 = 0;
			TableLayout.ReservationGrid reservationGrid = new TableLayout.ReservationGrid();
			int num3 = 0;
			int num4 = 0;
			int num5 = -1;
			int num6 = -1;
			TableLayout.LayoutInfo[] fixedChildrenInfo = containerInfo.FixedChildrenInfo;
			TableLayout.LayoutInfo layoutInfo = TableLayout.GetNextLayoutInfo(fixedChildrenInfo, ref num5, true);
			TableLayout.LayoutInfo layoutInfo2 = TableLayout.GetNextLayoutInfo(childrenInfo, ref num6, false);
			while (layoutInfo != null || layoutInfo2 != null)
			{
				int num7 = num4;
				if (layoutInfo2 != null)
				{
					layoutInfo2.RowStart = num3;
					layoutInfo2.ColumnStart = num4;
					this.AdvanceUntilFits(maxColumns, reservationGrid, layoutInfo2, out num7);
					if (layoutInfo2.RowStart >= maxRows)
					{
						return false;
					}
				}
				int num8;
				if (layoutInfo2 != null && (layoutInfo == null || (!this.IsCursorPastInsertionPoint(layoutInfo, layoutInfo2.RowStart, num7) && !this.IsOverlappingWithReservationGrid(layoutInfo, reservationGrid, num3))))
				{
					for (int i = 0; i < layoutInfo2.RowStart - num3; i++)
					{
						reservationGrid.AdvanceRow();
					}
					num3 = layoutInfo2.RowStart;
					num8 = Math.Min(num3 + layoutInfo2.RowSpan, maxRows);
					reservationGrid.ReserveAll(layoutInfo2, num8, num7);
					layoutInfo2 = TableLayout.GetNextLayoutInfo(childrenInfo, ref num6, false);
				}
				else
				{
					if (num4 >= maxColumns)
					{
						num4 = 0;
						num3++;
						reservationGrid.AdvanceRow();
					}
					layoutInfo.RowStart = Math.Min(layoutInfo.RowPosition, maxRows - 1);
					layoutInfo.ColumnStart = Math.Min(layoutInfo.ColumnPosition, maxColumns - 1);
					if (num3 > layoutInfo.RowStart)
					{
						layoutInfo.ColumnStart = num4;
					}
					else if (num3 == layoutInfo.RowStart)
					{
						layoutInfo.ColumnStart = Math.Max(layoutInfo.ColumnStart, num4);
					}
					layoutInfo.RowStart = Math.Max(layoutInfo.RowStart, num3);
					int j;
					for (j = 0; j < layoutInfo.RowStart - num3; j++)
					{
						reservationGrid.AdvanceRow();
					}
					this.AdvanceUntilFits(maxColumns, reservationGrid, layoutInfo, out num7);
					if (layoutInfo.RowStart >= maxRows)
					{
						return false;
					}
					while (j < layoutInfo.RowStart - num3)
					{
						reservationGrid.AdvanceRow();
						j++;
					}
					num3 = layoutInfo.RowStart;
					num7 = Math.Min(layoutInfo.ColumnStart + layoutInfo.ColumnSpan, maxColumns);
					num8 = Math.Min(layoutInfo.RowStart + layoutInfo.RowSpan, maxRows);
					reservationGrid.ReserveAll(layoutInfo, num8, num7);
					layoutInfo = TableLayout.GetNextLayoutInfo(fixedChildrenInfo, ref num5, true);
				}
				num4 = num7;
				num2 = ((num2 == int.MaxValue) ? num8 : Math.Max(num2, num8));
				num = ((num == int.MaxValue) ? num7 : Math.Max(num, num7));
			}
			if (growStyle == TableLayoutPanelGrowStyle.FixedSize)
			{
				num = maxColumns;
				num2 = maxRows;
			}
			else if (growStyle == TableLayoutPanelGrowStyle.AddRows)
			{
				num = maxColumns;
				num2 = Math.Max(containerInfo.MaxRows, num2);
			}
			else
			{
				num2 = ((maxRows == int.MaxValue) ? num2 : maxRows);
				num = Math.Max(containerInfo.MaxColumns, num);
			}
			if (containerInfo.Rows == null || containerInfo.Rows.Length != num2)
			{
				containerInfo.Rows = new TableLayout.Strip[num2];
			}
			if (containerInfo.Columns == null || containerInfo.Columns.Length != num)
			{
				containerInfo.Columns = new TableLayout.Strip[num];
			}
			containerInfo.Valid = true;
			return true;
		}

		// Token: 0x060050E0 RID: 20704 RVA: 0x00150D1C File Offset: 0x0014EF1C
		private static TableLayout.LayoutInfo GetNextLayoutInfo(TableLayout.LayoutInfo[] layoutInfo, ref int index, bool absolutelyPositioned)
		{
			int num = index + 1;
			index = num;
			for (int i = num; i < layoutInfo.Length; i++)
			{
				if (absolutelyPositioned == layoutInfo[i].IsAbsolutelyPositioned)
				{
					index = i;
					return layoutInfo[i];
				}
			}
			index = layoutInfo.Length;
			return null;
		}

		// Token: 0x060050E1 RID: 20705 RVA: 0x00150D57 File Offset: 0x0014EF57
		private bool IsCursorPastInsertionPoint(TableLayout.LayoutInfo fixedLayoutInfo, int insertionRow, int insertionCol)
		{
			return fixedLayoutInfo.RowPosition < insertionRow || (fixedLayoutInfo.RowPosition == insertionRow && fixedLayoutInfo.ColumnPosition < insertionCol);
		}

		// Token: 0x060050E2 RID: 20706 RVA: 0x00150D7C File Offset: 0x0014EF7C
		private bool IsOverlappingWithReservationGrid(TableLayout.LayoutInfo fixedLayoutInfo, TableLayout.ReservationGrid reservationGrid, int currentRow)
		{
			if (fixedLayoutInfo.RowPosition < currentRow)
			{
				return true;
			}
			for (int i = fixedLayoutInfo.RowPosition - currentRow; i < fixedLayoutInfo.RowPosition - currentRow + fixedLayoutInfo.RowSpan; i++)
			{
				for (int j = fixedLayoutInfo.ColumnPosition; j < fixedLayoutInfo.ColumnPosition + fixedLayoutInfo.ColumnSpan; j++)
				{
					if (reservationGrid.IsReserved(j, i))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060050E3 RID: 20707 RVA: 0x00150DE0 File Offset: 0x0014EFE0
		private void AdvanceUntilFits(int maxColumns, TableLayout.ReservationGrid reservationGrid, TableLayout.LayoutInfo layoutInfo, out int colStop)
		{
			int rowStart = layoutInfo.RowStart;
			do
			{
				this.GetColStartAndStop(maxColumns, reservationGrid, layoutInfo, out colStop);
			}
			while (this.ScanRowForOverlap(maxColumns, reservationGrid, layoutInfo, colStop, layoutInfo.RowStart - rowStart));
		}

		// Token: 0x060050E4 RID: 20708 RVA: 0x00150E18 File Offset: 0x0014F018
		private void GetColStartAndStop(int maxColumns, TableLayout.ReservationGrid reservationGrid, TableLayout.LayoutInfo layoutInfo, out int colStop)
		{
			colStop = layoutInfo.ColumnStart + layoutInfo.ColumnSpan;
			if (colStop > maxColumns)
			{
				if (layoutInfo.ColumnStart != 0)
				{
					layoutInfo.ColumnStart = 0;
					int rowStart = layoutInfo.RowStart;
					layoutInfo.RowStart = rowStart + 1;
				}
				colStop = Math.Min(layoutInfo.ColumnSpan, maxColumns);
			}
		}

		// Token: 0x060050E5 RID: 20709 RVA: 0x00150E6C File Offset: 0x0014F06C
		private bool ScanRowForOverlap(int maxColumns, TableLayout.ReservationGrid reservationGrid, TableLayout.LayoutInfo layoutInfo, int stopCol, int rowOffset)
		{
			for (int i = layoutInfo.ColumnStart; i < stopCol; i++)
			{
				if (reservationGrid.IsReserved(i, rowOffset))
				{
					layoutInfo.ColumnStart = i + 1;
					while (layoutInfo.ColumnStart < maxColumns && reservationGrid.IsReserved(layoutInfo.ColumnStart, rowOffset))
					{
						int columnStart = layoutInfo.ColumnStart;
						layoutInfo.ColumnStart = columnStart + 1;
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x060050E6 RID: 20710 RVA: 0x00150ED0 File Offset: 0x0014F0D0
		private Size ApplyStyles(TableLayout.ContainerInfo containerInfo, Size proposedConstraints, bool measureOnly)
		{
			Size empty = Size.Empty;
			this.InitializeStrips(containerInfo.Columns, containerInfo.ColumnStyles);
			this.InitializeStrips(containerInfo.Rows, containerInfo.RowStyles);
			containerInfo.ChildHasColumnSpan = false;
			containerInfo.ChildHasRowSpan = false;
			foreach (TableLayout.LayoutInfo layoutInfo in containerInfo.ChildrenInfo)
			{
				containerInfo.Columns[layoutInfo.ColumnStart].IsStart = true;
				containerInfo.Rows[layoutInfo.RowStart].IsStart = true;
				if (layoutInfo.ColumnSpan > 1)
				{
					containerInfo.ChildHasColumnSpan = true;
				}
				if (layoutInfo.RowSpan > 1)
				{
					containerInfo.ChildHasRowSpan = true;
				}
			}
			empty.Width = this.InflateColumns(containerInfo, proposedConstraints, measureOnly);
			int num = Math.Max(0, proposedConstraints.Width - empty.Width);
			empty.Height = this.InflateRows(containerInfo, proposedConstraints, num, measureOnly);
			return empty;
		}

		// Token: 0x060050E7 RID: 20711 RVA: 0x00150FBC File Offset: 0x0014F1BC
		private void InitializeStrips(TableLayout.Strip[] strips, IList styles)
		{
			for (int i = 0; i < strips.Length; i++)
			{
				TableLayoutStyle tableLayoutStyle = ((i < styles.Count) ? ((TableLayoutStyle)styles[i]) : null);
				TableLayout.Strip strip = strips[i];
				if (tableLayoutStyle != null && tableLayoutStyle.SizeType == SizeType.Absolute)
				{
					strip.MinSize = (int)Math.Round((double)((TableLayoutStyle)styles[i]).Size);
					strip.MaxSize = strip.MinSize;
				}
				else
				{
					strip.MinSize = 0;
					strip.MaxSize = 0;
				}
				strip.IsStart = false;
				strips[i] = strip;
			}
		}

		// Token: 0x060050E8 RID: 20712 RVA: 0x0015105C File Offset: 0x0014F25C
		private int InflateColumns(TableLayout.ContainerInfo containerInfo, Size proposedConstraints, bool measureOnly)
		{
			bool flag = measureOnly;
			TableLayout.LayoutInfo[] childrenInfo = containerInfo.ChildrenInfo;
			if (containerInfo.ChildHasColumnSpan)
			{
				object[] array = childrenInfo;
				TableLayout.Sort(array, TableLayout.ColumnSpanComparer.GetInstance);
			}
			if (flag && proposedConstraints.Width < 32767)
			{
				TableLayoutPanel tableLayoutPanel = containerInfo.Container as TableLayoutPanel;
				if (tableLayoutPanel != null && tableLayoutPanel.ParentInternal != null && tableLayoutPanel.ParentInternal.LayoutEngine == DefaultLayout.Instance)
				{
					if (tableLayoutPanel.Dock == DockStyle.Top || tableLayoutPanel.Dock == DockStyle.Bottom || tableLayoutPanel.Dock == DockStyle.Fill)
					{
						flag = false;
					}
					if ((tableLayoutPanel.Anchor & (AnchorStyles.Left | AnchorStyles.Right)) == (AnchorStyles.Left | AnchorStyles.Right))
					{
						flag = false;
					}
				}
			}
			foreach (TableLayout.LayoutInfo layoutInfo in childrenInfo)
			{
				IArrangedElement element = layoutInfo.Element;
				int columnSpan = layoutInfo.ColumnSpan;
				if (columnSpan > 1 || !this.IsAbsolutelySized(layoutInfo.ColumnStart, containerInfo.ColumnStyles))
				{
					int num2;
					int num3;
					if (columnSpan == 1 && layoutInfo.RowSpan == 1 && this.IsAbsolutelySized(layoutInfo.RowStart, containerInfo.RowStyles))
					{
						int num = (int)containerInfo.RowStyles[layoutInfo.RowStart].Size;
						num2 = this.GetElementSize(element, new Size(0, num)).Width;
						num3 = num2;
					}
					else
					{
						num2 = this.GetElementSize(element, new Size(1, 0)).Width;
						num3 = this.GetElementSize(element, Size.Empty).Width;
					}
					Padding margin = CommonProperties.GetMargin(element);
					num2 += margin.Horizontal;
					num3 += margin.Horizontal;
					int num4 = Math.Min(layoutInfo.ColumnStart + layoutInfo.ColumnSpan, containerInfo.Columns.Length);
					this.DistributeSize(containerInfo.ColumnStyles, containerInfo.Columns, layoutInfo.ColumnStart, num4, num2, num3, containerInfo.CellBorderWidth);
				}
			}
			int num5 = this.DistributeStyles(containerInfo.CellBorderWidth, containerInfo.ColumnStyles, containerInfo.Columns, proposedConstraints.Width, flag);
			if (flag && num5 > proposedConstraints.Width && proposedConstraints.Width > 1)
			{
				TableLayout.Strip[] columns = containerInfo.Columns;
				float num6 = 0f;
				int num7 = 0;
				TableLayoutStyleCollection columnStyles = containerInfo.ColumnStyles;
				for (int j = 0; j < columns.Length; j++)
				{
					TableLayout.Strip strip = columns[j];
					if (j < columnStyles.Count)
					{
						TableLayoutStyle tableLayoutStyle = columnStyles[j];
						if (tableLayoutStyle.SizeType == SizeType.Percent)
						{
							num6 += tableLayoutStyle.Size;
							num7 += strip.MinSize;
						}
					}
				}
				int num8 = num5 - proposedConstraints.Width;
				int num9 = Math.Min(num8, num7);
				for (int k = 0; k < columns.Length; k++)
				{
					if (k < columnStyles.Count)
					{
						TableLayoutStyle tableLayoutStyle2 = columnStyles[k];
						if (tableLayoutStyle2.SizeType == SizeType.Percent)
						{
							float num10 = tableLayoutStyle2.Size / num6;
							TableLayout.Strip[] array3 = columns;
							int num11 = k;
							array3[num11].MinSize = array3[num11].MinSize - (int)(num10 * (float)num9);
						}
					}
				}
				return num5 - num9;
			}
			return num5;
		}

		// Token: 0x060050E9 RID: 20713 RVA: 0x0015136C File Offset: 0x0014F56C
		private int InflateRows(TableLayout.ContainerInfo containerInfo, Size proposedConstraints, int expandLastElementWidth, bool measureOnly)
		{
			bool flag = measureOnly;
			TableLayout.LayoutInfo[] childrenInfo = containerInfo.ChildrenInfo;
			if (containerInfo.ChildHasRowSpan)
			{
				object[] array = childrenInfo;
				TableLayout.Sort(array, TableLayout.RowSpanComparer.GetInstance);
			}
			bool hasMultiplePercentColumns = containerInfo.HasMultiplePercentColumns;
			if (flag && proposedConstraints.Height < 32767)
			{
				TableLayoutPanel tableLayoutPanel = containerInfo.Container as TableLayoutPanel;
				if (tableLayoutPanel != null && tableLayoutPanel.ParentInternal != null && tableLayoutPanel.ParentInternal.LayoutEngine == DefaultLayout.Instance)
				{
					if (tableLayoutPanel.Dock == DockStyle.Left || tableLayoutPanel.Dock == DockStyle.Right || tableLayoutPanel.Dock == DockStyle.Fill)
					{
						flag = false;
					}
					if ((tableLayoutPanel.Anchor & (AnchorStyles.Top | AnchorStyles.Bottom)) == (AnchorStyles.Top | AnchorStyles.Bottom))
					{
						flag = false;
					}
				}
			}
			foreach (TableLayout.LayoutInfo layoutInfo in childrenInfo)
			{
				IArrangedElement element = layoutInfo.Element;
				int rowSpan = layoutInfo.RowSpan;
				if (rowSpan > 1 || !this.IsAbsolutelySized(layoutInfo.RowStart, containerInfo.RowStyles))
				{
					int num = this.SumStrips(containerInfo.Columns, layoutInfo.ColumnStart, layoutInfo.ColumnSpan);
					if (!flag && layoutInfo.ColumnStart + layoutInfo.ColumnSpan >= containerInfo.MaxColumns && !hasMultiplePercentColumns)
					{
						num += expandLastElementWidth;
					}
					Padding margin = CommonProperties.GetMargin(element);
					int num2 = this.GetElementSize(element, new Size(num - margin.Horizontal, 0)).Height + margin.Vertical;
					int num3 = num2;
					int num4 = Math.Min(layoutInfo.RowStart + layoutInfo.RowSpan, containerInfo.Rows.Length);
					this.DistributeSize(containerInfo.RowStyles, containerInfo.Rows, layoutInfo.RowStart, num4, num2, num3, containerInfo.CellBorderWidth);
				}
			}
			return this.DistributeStyles(containerInfo.CellBorderWidth, containerInfo.RowStyles, containerInfo.Rows, proposedConstraints.Height, flag);
		}

		// Token: 0x060050EA RID: 20714 RVA: 0x00151538 File Offset: 0x0014F738
		private Size GetElementSize(IArrangedElement element, Size proposedConstraints)
		{
			if (CommonProperties.GetAutoSize(element))
			{
				return element.GetPreferredSize(proposedConstraints);
			}
			return CommonProperties.GetSpecifiedBounds(element).Size;
		}

		// Token: 0x060050EB RID: 20715 RVA: 0x00151564 File Offset: 0x0014F764
		internal int SumStrips(TableLayout.Strip[] strips, int start, int span)
		{
			int num = 0;
			for (int i = start; i < Math.Min(start + span, strips.Length); i++)
			{
				TableLayout.Strip strip = strips[i];
				num += strip.MinSize;
			}
			return num;
		}

		// Token: 0x060050EC RID: 20716 RVA: 0x0015159C File Offset: 0x0014F79C
		private void DistributeSize(IList styles, TableLayout.Strip[] strips, int start, int stop, int min, int max, int cellBorderWidth)
		{
			this.xDistributeSize(styles, strips, start, stop, min, TableLayout.MinSizeProxy.GetInstance, cellBorderWidth);
			this.xDistributeSize(styles, strips, start, stop, max, TableLayout.MaxSizeProxy.GetInstance, cellBorderWidth);
		}

		// Token: 0x060050ED RID: 20717 RVA: 0x001515C8 File Offset: 0x0014F7C8
		private void xDistributeSize(IList styles, TableLayout.Strip[] strips, int start, int stop, int desiredLength, TableLayout.SizeProxy sizeProxy, int cellBorderWidth)
		{
			int num = 0;
			int num2 = 0;
			desiredLength -= cellBorderWidth * (stop - start - 1);
			desiredLength = Math.Max(0, desiredLength);
			for (int i = start; i < stop; i++)
			{
				sizeProxy.Strip = strips[i];
				if (!this.IsAbsolutelySized(i, styles) && sizeProxy.Size == 0)
				{
					num2++;
				}
				num += sizeProxy.Size;
			}
			int num3 = desiredLength - num;
			if (num3 <= 0)
			{
				return;
			}
			if (num2 == 0)
			{
				int num4 = stop - 1;
				while (num4 >= start && (num4 >= styles.Count || ((TableLayoutStyle)styles[num4]).SizeType != SizeType.Percent))
				{
					num4--;
				}
				if (num4 != start - 1)
				{
					stop = num4 + 1;
				}
				for (int j = stop - 1; j >= start; j--)
				{
					if (!this.IsAbsolutelySized(j, styles))
					{
						sizeProxy.Strip = strips[j];
						if (j != strips.Length - 1 && !strips[j + 1].IsStart && !this.IsAbsolutelySized(j + 1, styles))
						{
							sizeProxy.Strip = strips[j + 1];
							int num5 = Math.Min(sizeProxy.Size, num3);
							sizeProxy.Size -= num5;
							strips[j + 1] = sizeProxy.Strip;
							sizeProxy.Strip = strips[j];
						}
						sizeProxy.Size += num3;
						strips[j] = sizeProxy.Strip;
						return;
					}
				}
				return;
			}
			int num6 = num3 / num2;
			int num7 = 0;
			for (int k = start; k < stop; k++)
			{
				sizeProxy.Strip = strips[k];
				if (!this.IsAbsolutelySized(k, styles) && sizeProxy.Size == 0)
				{
					num7++;
					if (num7 == num2)
					{
						num6 = num3 - num6 * (num2 - 1);
					}
					sizeProxy.Size += num6;
					strips[k] = sizeProxy.Strip;
				}
			}
		}

		// Token: 0x060050EE RID: 20718 RVA: 0x001517B9 File Offset: 0x0014F9B9
		private bool IsAbsolutelySized(int index, IList styles)
		{
			return index < styles.Count && ((TableLayoutStyle)styles[index]).SizeType == SizeType.Absolute;
		}

		// Token: 0x060050EF RID: 20719 RVA: 0x001517DC File Offset: 0x0014F9DC
		private int DistributeStyles(int cellBorderWidth, IList styles, TableLayout.Strip[] strips, int maxSize, bool dontHonorConstraint)
		{
			int num = 0;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			bool flag = false;
			for (int i = 0; i < strips.Length; i++)
			{
				TableLayout.Strip strip = strips[i];
				if (i < styles.Count)
				{
					TableLayoutStyle tableLayoutStyle = (TableLayoutStyle)styles[i];
					SizeType sizeType = tableLayoutStyle.SizeType;
					if (sizeType != SizeType.Absolute)
					{
						if (sizeType != SizeType.Percent)
						{
							num5 += (float)strip.MinSize;
							flag = true;
						}
						else
						{
							num3 += tableLayoutStyle.Size;
							num4 += (float)strip.MinSize;
						}
					}
					else
					{
						num5 += (float)strip.MinSize;
					}
				}
				else
				{
					flag = true;
				}
				strip.MaxSize += cellBorderWidth;
				strip.MinSize += cellBorderWidth;
				strips[i] = strip;
				num += strip.MinSize;
			}
			int num6 = maxSize - num;
			if (num3 > 0f)
			{
				if (!dontHonorConstraint)
				{
					if (num4 > (float)maxSize - num5)
					{
						num4 = Math.Max(0f, (float)maxSize - num5);
					}
					if (num6 > 0)
					{
						num4 += (float)num6;
					}
					else if (num6 < 0)
					{
						num4 = (float)maxSize - num5 - (float)(strips.Length * cellBorderWidth);
					}
					for (int j = 0; j < strips.Length; j++)
					{
						TableLayout.Strip strip2 = strips[j];
						SizeType sizeType2 = ((j < styles.Count) ? ((TableLayoutStyle)styles[j]).SizeType : SizeType.AutoSize);
						if (sizeType2 == SizeType.Percent)
						{
							TableLayoutStyle tableLayoutStyle2 = (TableLayoutStyle)styles[j];
							int num7 = (int)(tableLayoutStyle2.Size * num4 / num3);
							num -= strip2.MinSize;
							num += num7 + cellBorderWidth;
							strip2.MinSize = num7 + cellBorderWidth;
							strips[j] = strip2;
						}
					}
				}
				else
				{
					int num8 = 0;
					for (int k = 0; k < strips.Length; k++)
					{
						TableLayout.Strip strip3 = strips[k];
						SizeType sizeType3 = ((k < styles.Count) ? ((TableLayoutStyle)styles[k]).SizeType : SizeType.AutoSize);
						if (sizeType3 == SizeType.Percent)
						{
							TableLayoutStyle tableLayoutStyle3 = (TableLayoutStyle)styles[k];
							int num9 = (int)Math.Round((double)((float)strip3.MinSize * num3 / tableLayoutStyle3.Size));
							num8 = Math.Max(num8, num9);
							num -= strip3.MinSize;
						}
					}
					num += num8;
				}
			}
			num6 = maxSize - num;
			if (flag && num6 > 0)
			{
				if ((float)num6 < num2)
				{
					float num10 = (float)num6 / num2;
				}
				num6 -= (int)Math.Ceiling((double)num2);
				for (int l = 0; l < strips.Length; l++)
				{
					TableLayout.Strip strip4 = strips[l];
					if (l >= styles.Count || ((TableLayoutStyle)styles[l]).SizeType == SizeType.AutoSize)
					{
						int num11 = Math.Min(strip4.MaxSize - strip4.MinSize, num6);
						if (num11 > 0)
						{
							num += num11;
							num6 -= num11;
							strip4.MinSize += num11;
							strips[l] = strip4;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x060050F0 RID: 20720 RVA: 0x00151AE8 File Offset: 0x0014FCE8
		private void SetElementBounds(TableLayout.ContainerInfo containerInfo, RectangleF displayRectF)
		{
			int cellBorderWidth = containerInfo.CellBorderWidth;
			float num = displayRectF.Y;
			int i = 0;
			int j = 0;
			bool flag = false;
			Rectangle rectangle = Rectangle.Truncate(displayRectF);
			if (containerInfo.Container is Control)
			{
				Control control = containerInfo.Container as Control;
				flag = control.RightToLeft == RightToLeft.Yes;
			}
			TableLayout.LayoutInfo[] childrenInfo = containerInfo.ChildrenInfo;
			float num2 = (flag ? displayRectF.Right : displayRectF.X);
			object[] array = childrenInfo;
			TableLayout.Sort(array, TableLayout.PostAssignedPositionComparer.GetInstance);
			foreach (TableLayout.LayoutInfo layoutInfo in childrenInfo)
			{
				IArrangedElement element = layoutInfo.Element;
				if (j != layoutInfo.RowStart)
				{
					while (j < layoutInfo.RowStart)
					{
						num += (float)containerInfo.Rows[j].MinSize;
						j++;
					}
					num2 = (flag ? displayRectF.Right : displayRectF.X);
					i = 0;
				}
				while (i < layoutInfo.ColumnStart)
				{
					if (flag)
					{
						num2 -= (float)containerInfo.Columns[i].MinSize;
					}
					else
					{
						num2 += (float)containerInfo.Columns[i].MinSize;
					}
					i++;
				}
				int num3 = i + layoutInfo.ColumnSpan;
				int num4 = 0;
				while (i < num3 && i < containerInfo.Columns.Length)
				{
					num4 += containerInfo.Columns[i].MinSize;
					i++;
				}
				if (flag)
				{
					num2 -= (float)num4;
				}
				int num5 = j + layoutInfo.RowSpan;
				int num6 = 0;
				int num7 = j;
				while (num7 < num5 && num7 < containerInfo.Rows.Length)
				{
					num6 += containerInfo.Rows[num7].MinSize;
					num7++;
				}
				Rectangle rectangle2 = new Rectangle((int)(num2 + (float)cellBorderWidth / 2f), (int)(num + (float)cellBorderWidth / 2f), num4 - cellBorderWidth, num6 - cellBorderWidth);
				Padding margin = CommonProperties.GetMargin(element);
				if (flag)
				{
					int right = margin.Right;
					margin.Right = margin.Left;
					margin.Left = right;
				}
				rectangle2 = LayoutUtils.DeflateRect(rectangle2, margin);
				rectangle2.Width = Math.Max(rectangle2.Width, 1);
				rectangle2.Height = Math.Max(rectangle2.Height, 1);
				AnchorStyles unifiedAnchor = LayoutUtils.GetUnifiedAnchor(element);
				Rectangle rectangle3 = LayoutUtils.AlignAndStretch(this.GetElementSize(element, rectangle2.Size), rectangle2, unifiedAnchor);
				rectangle3.Width = Math.Min(rectangle2.Width, rectangle3.Width);
				rectangle3.Height = Math.Min(rectangle2.Height, rectangle3.Height);
				if (flag)
				{
					rectangle3.X = rectangle2.X + (rectangle2.Right - rectangle3.Right);
				}
				element.SetBounds(rectangle3, BoundsSpecified.None);
				if (!flag)
				{
					num2 += (float)num4;
				}
			}
		}

		// Token: 0x060050F1 RID: 20721 RVA: 0x00151DB8 File Offset: 0x0014FFB8
		internal IArrangedElement GetControlFromPosition(IArrangedElement container, int column, int row)
		{
			if (row < 0)
			{
				throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
				{
					"RowPosition",
					row.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (column < 0)
			{
				throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
				{
					"ColumnPosition",
					column.ToString(CultureInfo.CurrentCulture)
				}));
			}
			ArrangedElementCollection children = container.Children;
			TableLayout.ContainerInfo containerInfo = TableLayout.GetContainerInfo(container);
			if (children == null || children.Count == 0)
			{
				return null;
			}
			if (!containerInfo.Valid)
			{
				this.EnsureRowAndColumnAssignments(container, containerInfo, true);
			}
			for (int i = 0; i < children.Count; i++)
			{
				TableLayout.LayoutInfo layoutInfo = TableLayout.GetLayoutInfo(children[i]);
				if (layoutInfo.ColumnStart <= column && layoutInfo.ColumnStart + layoutInfo.ColumnSpan - 1 >= column && layoutInfo.RowStart <= row && layoutInfo.RowStart + layoutInfo.RowSpan - 1 >= row)
				{
					return layoutInfo.Element;
				}
			}
			return null;
		}

		// Token: 0x060050F2 RID: 20722 RVA: 0x00151EB0 File Offset: 0x001500B0
		internal TableLayoutPanelCellPosition GetPositionFromControl(IArrangedElement container, IArrangedElement child)
		{
			if (container == null || child == null)
			{
				return new TableLayoutPanelCellPosition(-1, -1);
			}
			ArrangedElementCollection children = container.Children;
			TableLayout.ContainerInfo containerInfo = TableLayout.GetContainerInfo(container);
			if (children == null || children.Count == 0)
			{
				return new TableLayoutPanelCellPosition(-1, -1);
			}
			if (!containerInfo.Valid)
			{
				this.EnsureRowAndColumnAssignments(container, containerInfo, true);
			}
			TableLayout.LayoutInfo layoutInfo = TableLayout.GetLayoutInfo(child);
			return new TableLayoutPanelCellPosition(layoutInfo.ColumnStart, layoutInfo.RowStart);
		}

		// Token: 0x060050F3 RID: 20723 RVA: 0x00151F18 File Offset: 0x00150118
		internal static TableLayout.LayoutInfo GetLayoutInfo(IArrangedElement element)
		{
			TableLayout.LayoutInfo layoutInfo = (TableLayout.LayoutInfo)element.Properties.GetObject(TableLayout._layoutInfoProperty);
			if (layoutInfo == null)
			{
				layoutInfo = new TableLayout.LayoutInfo(element);
				TableLayout.SetLayoutInfo(element, layoutInfo);
			}
			return layoutInfo;
		}

		// Token: 0x060050F4 RID: 20724 RVA: 0x00151F4D File Offset: 0x0015014D
		internal static void SetLayoutInfo(IArrangedElement element, TableLayout.LayoutInfo value)
		{
			element.Properties.SetObject(TableLayout._layoutInfoProperty, value);
		}

		// Token: 0x060050F5 RID: 20725 RVA: 0x00151F60 File Offset: 0x00150160
		internal static bool HasCachedAssignments(TableLayout.ContainerInfo containerInfo)
		{
			return containerInfo.Valid;
		}

		// Token: 0x060050F6 RID: 20726 RVA: 0x00151F68 File Offset: 0x00150168
		internal static void ClearCachedAssignments(TableLayout.ContainerInfo containerInfo)
		{
			containerInfo.Valid = false;
		}

		// Token: 0x060050F7 RID: 20727 RVA: 0x00151F74 File Offset: 0x00150174
		internal static TableLayout.ContainerInfo GetContainerInfo(IArrangedElement container)
		{
			TableLayout.ContainerInfo containerInfo = (TableLayout.ContainerInfo)container.Properties.GetObject(TableLayout._containerInfoProperty);
			if (containerInfo == null)
			{
				containerInfo = new TableLayout.ContainerInfo(container);
				container.Properties.SetObject(TableLayout._containerInfoProperty, containerInfo);
			}
			return containerInfo;
		}

		// Token: 0x060050F8 RID: 20728 RVA: 0x000070A6 File Offset: 0x000052A6
		[Conditional("DEBUG_LAYOUT")]
		private void Debug_VerifyAssignmentsAreCurrent(IArrangedElement container, TableLayout.ContainerInfo containerInfo)
		{
		}

		// Token: 0x060050F9 RID: 20729 RVA: 0x00151FB4 File Offset: 0x001501B4
		[Conditional("DEBUG_LAYOUT")]
		private void Debug_VerifyNoOverlapping(IArrangedElement container)
		{
			ArrayList arrayList = new ArrayList(container.Children.Count);
			TableLayout.ContainerInfo containerInfo = TableLayout.GetContainerInfo(container);
			TableLayout.Strip[] rows = containerInfo.Rows;
			TableLayout.Strip[] columns = containerInfo.Columns;
			foreach (object obj in container.Children)
			{
				IArrangedElement arrangedElement = (IArrangedElement)obj;
				if (arrangedElement.ParticipatesInLayout)
				{
					arrayList.Add(TableLayout.GetLayoutInfo(arrangedElement));
				}
			}
			for (int i = 0; i < arrayList.Count; i++)
			{
				TableLayout.LayoutInfo layoutInfo = (TableLayout.LayoutInfo)arrayList[i];
				Rectangle bounds = layoutInfo.Element.Bounds;
				Rectangle rectangle = new Rectangle(layoutInfo.ColumnStart, layoutInfo.RowStart, layoutInfo.ColumnSpan, layoutInfo.RowSpan);
				for (int j = i + 1; j < arrayList.Count; j++)
				{
					TableLayout.LayoutInfo layoutInfo2 = (TableLayout.LayoutInfo)arrayList[j];
					Rectangle bounds2 = layoutInfo2.Element.Bounds;
					Rectangle rectangle2 = new Rectangle(layoutInfo2.ColumnStart, layoutInfo2.RowStart, layoutInfo2.ColumnSpan, layoutInfo2.RowSpan);
					if (LayoutUtils.IsIntersectHorizontally(bounds, bounds2))
					{
						for (int k = layoutInfo.ColumnStart; k < layoutInfo.ColumnStart + layoutInfo.ColumnSpan; k++)
						{
						}
						for (int k = layoutInfo2.ColumnStart; k < layoutInfo2.ColumnStart + layoutInfo2.ColumnSpan; k++)
						{
						}
					}
					if (LayoutUtils.IsIntersectVertically(bounds, bounds2))
					{
						for (int l = layoutInfo.RowStart; l < layoutInfo.RowStart + layoutInfo.RowSpan; l++)
						{
						}
						for (int l = layoutInfo2.RowStart; l < layoutInfo2.RowStart + layoutInfo2.RowSpan; l++)
						{
						}
					}
				}
			}
		}

		// Token: 0x040034E5 RID: 13541
		internal static readonly TableLayout Instance = new TableLayout();

		// Token: 0x040034E6 RID: 13542
		private static readonly int _containerInfoProperty = PropertyStore.CreateKey();

		// Token: 0x040034E7 RID: 13543
		private static readonly int _layoutInfoProperty = PropertyStore.CreateKey();

		// Token: 0x040034E8 RID: 13544
		private static string[] _propertiesWhichInvalidateCache = new string[]
		{
			null,
			PropertyNames.ChildIndex,
			PropertyNames.Parent,
			PropertyNames.Visible,
			PropertyNames.Items,
			PropertyNames.Rows,
			PropertyNames.Columns,
			PropertyNames.RowStyles,
			PropertyNames.ColumnStyles
		};

		// Token: 0x02000865 RID: 2149
		private struct SorterObjectArray
		{
			// Token: 0x060070D5 RID: 28885 RVA: 0x0019D566 File Offset: 0x0019B766
			internal SorterObjectArray(object[] keys, IComparer comparer)
			{
				if (comparer == null)
				{
					comparer = Comparer.Default;
				}
				this.keys = keys;
				this.comparer = comparer;
			}

			// Token: 0x060070D6 RID: 28886 RVA: 0x0019D580 File Offset: 0x0019B780
			internal void SwapIfGreaterWithItems(int a, int b)
			{
				if (a != b)
				{
					try
					{
						if (this.comparer.Compare(this.keys[a], this.keys[b]) > 0)
						{
							object obj = this.keys[a];
							this.keys[a] = this.keys[b];
							this.keys[b] = obj;
						}
					}
					catch (IndexOutOfRangeException)
					{
						throw new ArgumentException();
					}
					catch (Exception)
					{
						throw new InvalidOperationException();
					}
				}
			}

			// Token: 0x060070D7 RID: 28887 RVA: 0x0019D600 File Offset: 0x0019B800
			internal void QuickSort(int left, int right)
			{
				do
				{
					int num = left;
					int num2 = right;
					int median = TableLayout.GetMedian(num, num2);
					this.SwapIfGreaterWithItems(num, median);
					this.SwapIfGreaterWithItems(num, num2);
					this.SwapIfGreaterWithItems(median, num2);
					object obj = this.keys[median];
					do
					{
						try
						{
							while (this.comparer.Compare(this.keys[num], obj) < 0)
							{
								num++;
							}
							while (this.comparer.Compare(obj, this.keys[num2]) < 0)
							{
								num2--;
							}
						}
						catch (IndexOutOfRangeException)
						{
							throw new ArgumentException();
						}
						catch (Exception)
						{
							throw new InvalidOperationException();
						}
						if (num > num2)
						{
							break;
						}
						if (num < num2)
						{
							object obj2 = this.keys[num];
							this.keys[num] = this.keys[num2];
							this.keys[num2] = obj2;
						}
						num++;
						num2--;
					}
					while (num <= num2);
					if (num2 - left <= right - num)
					{
						if (left < num2)
						{
							this.QuickSort(left, num2);
						}
						left = num;
					}
					else
					{
						if (num < right)
						{
							this.QuickSort(num, right);
						}
						right = num2;
					}
				}
				while (left < right);
			}

			// Token: 0x040043F7 RID: 17399
			private object[] keys;

			// Token: 0x040043F8 RID: 17400
			private IComparer comparer;
		}

		// Token: 0x02000866 RID: 2150
		internal sealed class LayoutInfo
		{
			// Token: 0x060070D8 RID: 28888 RVA: 0x0019D70C File Offset: 0x0019B90C
			public LayoutInfo(IArrangedElement element)
			{
				this._element = element;
			}

			// Token: 0x170018AF RID: 6319
			// (get) Token: 0x060070D9 RID: 28889 RVA: 0x0019D745 File Offset: 0x0019B945
			internal bool IsAbsolutelyPositioned
			{
				get
				{
					return this._rowPos >= 0 && this._colPos >= 0;
				}
			}

			// Token: 0x170018B0 RID: 6320
			// (get) Token: 0x060070DA RID: 28890 RVA: 0x0019D75E File Offset: 0x0019B95E
			internal IArrangedElement Element
			{
				get
				{
					return this._element;
				}
			}

			// Token: 0x170018B1 RID: 6321
			// (get) Token: 0x060070DB RID: 28891 RVA: 0x0019D766 File Offset: 0x0019B966
			// (set) Token: 0x060070DC RID: 28892 RVA: 0x0019D76E File Offset: 0x0019B96E
			internal int RowPosition
			{
				get
				{
					return this._rowPos;
				}
				set
				{
					this._rowPos = value;
				}
			}

			// Token: 0x170018B2 RID: 6322
			// (get) Token: 0x060070DD RID: 28893 RVA: 0x0019D777 File Offset: 0x0019B977
			// (set) Token: 0x060070DE RID: 28894 RVA: 0x0019D77F File Offset: 0x0019B97F
			internal int ColumnPosition
			{
				get
				{
					return this._colPos;
				}
				set
				{
					this._colPos = value;
				}
			}

			// Token: 0x170018B3 RID: 6323
			// (get) Token: 0x060070DF RID: 28895 RVA: 0x0019D788 File Offset: 0x0019B988
			// (set) Token: 0x060070E0 RID: 28896 RVA: 0x0019D790 File Offset: 0x0019B990
			internal int RowStart
			{
				get
				{
					return this._rowStart;
				}
				set
				{
					this._rowStart = value;
				}
			}

			// Token: 0x170018B4 RID: 6324
			// (get) Token: 0x060070E1 RID: 28897 RVA: 0x0019D799 File Offset: 0x0019B999
			// (set) Token: 0x060070E2 RID: 28898 RVA: 0x0019D7A1 File Offset: 0x0019B9A1
			internal int ColumnStart
			{
				get
				{
					return this._columnStart;
				}
				set
				{
					this._columnStart = value;
				}
			}

			// Token: 0x170018B5 RID: 6325
			// (get) Token: 0x060070E3 RID: 28899 RVA: 0x0019D7AA File Offset: 0x0019B9AA
			// (set) Token: 0x060070E4 RID: 28900 RVA: 0x0019D7B2 File Offset: 0x0019B9B2
			internal int ColumnSpan
			{
				get
				{
					return this._columnSpan;
				}
				set
				{
					this._columnSpan = value;
				}
			}

			// Token: 0x170018B6 RID: 6326
			// (get) Token: 0x060070E5 RID: 28901 RVA: 0x0019D7BB File Offset: 0x0019B9BB
			// (set) Token: 0x060070E6 RID: 28902 RVA: 0x0019D7C3 File Offset: 0x0019B9C3
			internal int RowSpan
			{
				get
				{
					return this._rowSpan;
				}
				set
				{
					this._rowSpan = value;
				}
			}

			// Token: 0x040043F9 RID: 17401
			private int _rowStart = -1;

			// Token: 0x040043FA RID: 17402
			private int _columnStart = -1;

			// Token: 0x040043FB RID: 17403
			private int _columnSpan = 1;

			// Token: 0x040043FC RID: 17404
			private int _rowSpan = 1;

			// Token: 0x040043FD RID: 17405
			private int _rowPos = -1;

			// Token: 0x040043FE RID: 17406
			private int _colPos = -1;

			// Token: 0x040043FF RID: 17407
			private IArrangedElement _element;
		}

		// Token: 0x02000867 RID: 2151
		internal sealed class ContainerInfo
		{
			// Token: 0x060070E7 RID: 28903 RVA: 0x0019D7CC File Offset: 0x0019B9CC
			public ContainerInfo(IArrangedElement container)
			{
				this._container = container;
				this._growStyle = TableLayoutPanelGrowStyle.AddRows;
			}

			// Token: 0x060070E8 RID: 28904 RVA: 0x0019D7F8 File Offset: 0x0019B9F8
			public ContainerInfo(TableLayout.ContainerInfo containerInfo)
			{
				this._cellBorderWidth = containerInfo.CellBorderWidth;
				this._maxRows = containerInfo.MaxRows;
				this._maxColumns = containerInfo.MaxColumns;
				this._growStyle = containerInfo.GrowStyle;
				this._container = containerInfo.Container;
				this._rowStyles = containerInfo.RowStyles;
				this._colStyles = containerInfo.ColumnStyles;
			}

			// Token: 0x170018B7 RID: 6327
			// (get) Token: 0x060070E9 RID: 28905 RVA: 0x0019D875 File Offset: 0x0019BA75
			public IArrangedElement Container
			{
				get
				{
					return this._container;
				}
			}

			// Token: 0x170018B8 RID: 6328
			// (get) Token: 0x060070EA RID: 28906 RVA: 0x0019D87D File Offset: 0x0019BA7D
			// (set) Token: 0x060070EB RID: 28907 RVA: 0x0019D885 File Offset: 0x0019BA85
			public int CellBorderWidth
			{
				get
				{
					return this._cellBorderWidth;
				}
				set
				{
					this._cellBorderWidth = value;
				}
			}

			// Token: 0x170018B9 RID: 6329
			// (get) Token: 0x060070EC RID: 28908 RVA: 0x0019D88E File Offset: 0x0019BA8E
			// (set) Token: 0x060070ED RID: 28909 RVA: 0x0019D896 File Offset: 0x0019BA96
			public TableLayout.Strip[] Columns
			{
				get
				{
					return this._cols;
				}
				set
				{
					this._cols = value;
				}
			}

			// Token: 0x170018BA RID: 6330
			// (get) Token: 0x060070EE RID: 28910 RVA: 0x0019D89F File Offset: 0x0019BA9F
			// (set) Token: 0x060070EF RID: 28911 RVA: 0x0019D8A7 File Offset: 0x0019BAA7
			public TableLayout.Strip[] Rows
			{
				get
				{
					return this._rows;
				}
				set
				{
					this._rows = value;
				}
			}

			// Token: 0x170018BB RID: 6331
			// (get) Token: 0x060070F0 RID: 28912 RVA: 0x0019D8B0 File Offset: 0x0019BAB0
			// (set) Token: 0x060070F1 RID: 28913 RVA: 0x0019D8B8 File Offset: 0x0019BAB8
			public int MaxRows
			{
				get
				{
					return this._maxRows;
				}
				set
				{
					if (this._maxRows != value)
					{
						this._maxRows = value;
						this.Valid = false;
					}
				}
			}

			// Token: 0x170018BC RID: 6332
			// (get) Token: 0x060070F2 RID: 28914 RVA: 0x0019D8D1 File Offset: 0x0019BAD1
			// (set) Token: 0x060070F3 RID: 28915 RVA: 0x0019D8D9 File Offset: 0x0019BAD9
			public int MaxColumns
			{
				get
				{
					return this._maxColumns;
				}
				set
				{
					if (this._maxColumns != value)
					{
						this._maxColumns = value;
						this.Valid = false;
					}
				}
			}

			// Token: 0x170018BD RID: 6333
			// (get) Token: 0x060070F4 RID: 28916 RVA: 0x0019D8F2 File Offset: 0x0019BAF2
			public int MinRowsAndColumns
			{
				get
				{
					return this._minRowsAndColumns;
				}
			}

			// Token: 0x170018BE RID: 6334
			// (get) Token: 0x060070F5 RID: 28917 RVA: 0x0019D8FA File Offset: 0x0019BAFA
			public int MinColumns
			{
				get
				{
					return this._minColumns;
				}
			}

			// Token: 0x170018BF RID: 6335
			// (get) Token: 0x060070F6 RID: 28918 RVA: 0x0019D902 File Offset: 0x0019BB02
			public int MinRows
			{
				get
				{
					return this._minRows;
				}
			}

			// Token: 0x170018C0 RID: 6336
			// (get) Token: 0x060070F7 RID: 28919 RVA: 0x0019D90A File Offset: 0x0019BB0A
			// (set) Token: 0x060070F8 RID: 28920 RVA: 0x0019D912 File Offset: 0x0019BB12
			public TableLayoutPanelGrowStyle GrowStyle
			{
				get
				{
					return this._growStyle;
				}
				set
				{
					if (this._growStyle != value)
					{
						this._growStyle = value;
						this.Valid = false;
					}
				}
			}

			// Token: 0x170018C1 RID: 6337
			// (get) Token: 0x060070F9 RID: 28921 RVA: 0x0019D92B File Offset: 0x0019BB2B
			// (set) Token: 0x060070FA RID: 28922 RVA: 0x0019D94C File Offset: 0x0019BB4C
			public TableLayoutRowStyleCollection RowStyles
			{
				get
				{
					if (this._rowStyles == null)
					{
						this._rowStyles = new TableLayoutRowStyleCollection(this._container);
					}
					return this._rowStyles;
				}
				set
				{
					this._rowStyles = value;
					if (this._rowStyles != null)
					{
						this._rowStyles.EnsureOwnership(this._container);
					}
				}
			}

			// Token: 0x170018C2 RID: 6338
			// (get) Token: 0x060070FB RID: 28923 RVA: 0x0019D96E File Offset: 0x0019BB6E
			// (set) Token: 0x060070FC RID: 28924 RVA: 0x0019D98F File Offset: 0x0019BB8F
			public TableLayoutColumnStyleCollection ColumnStyles
			{
				get
				{
					if (this._colStyles == null)
					{
						this._colStyles = new TableLayoutColumnStyleCollection(this._container);
					}
					return this._colStyles;
				}
				set
				{
					this._colStyles = value;
					if (this._colStyles != null)
					{
						this._colStyles.EnsureOwnership(this._container);
					}
				}
			}

			// Token: 0x170018C3 RID: 6339
			// (get) Token: 0x060070FD RID: 28925 RVA: 0x0019D9B4 File Offset: 0x0019BBB4
			public TableLayout.LayoutInfo[] ChildrenInfo
			{
				get
				{
					if (!this._state[TableLayout.ContainerInfo.stateChildInfoValid])
					{
						this._countFixedChildren = 0;
						this._minRowsAndColumns = 0;
						this._minColumns = 0;
						this._minRows = 0;
						ArrangedElementCollection children = this.Container.Children;
						TableLayout.LayoutInfo[] array = new TableLayout.LayoutInfo[children.Count];
						int num = 0;
						int num2 = 0;
						for (int i = 0; i < children.Count; i++)
						{
							IArrangedElement arrangedElement = children[i];
							if (!arrangedElement.ParticipatesInLayout)
							{
								num++;
							}
							else
							{
								TableLayout.LayoutInfo layoutInfo = TableLayout.GetLayoutInfo(arrangedElement);
								if (layoutInfo.IsAbsolutelyPositioned)
								{
									this._countFixedChildren++;
								}
								array[num2++] = layoutInfo;
								this._minRowsAndColumns += layoutInfo.RowSpan * layoutInfo.ColumnSpan;
								if (layoutInfo.IsAbsolutelyPositioned)
								{
									this._minColumns = Math.Max(this._minColumns, layoutInfo.ColumnPosition + layoutInfo.ColumnSpan);
									this._minRows = Math.Max(this._minRows, layoutInfo.RowPosition + layoutInfo.RowSpan);
								}
							}
						}
						if (num > 0)
						{
							TableLayout.LayoutInfo[] array2 = new TableLayout.LayoutInfo[array.Length - num];
							Array.Copy(array, array2, array2.Length);
							this._childInfo = array2;
						}
						else
						{
							this._childInfo = array;
						}
						this._state[TableLayout.ContainerInfo.stateChildInfoValid] = true;
					}
					if (this._childInfo != null)
					{
						return this._childInfo;
					}
					return new TableLayout.LayoutInfo[0];
				}
			}

			// Token: 0x170018C4 RID: 6340
			// (get) Token: 0x060070FE RID: 28926 RVA: 0x0019DB26 File Offset: 0x0019BD26
			public bool ChildInfoValid
			{
				get
				{
					return this._state[TableLayout.ContainerInfo.stateChildInfoValid];
				}
			}

			// Token: 0x170018C5 RID: 6341
			// (get) Token: 0x060070FF RID: 28927 RVA: 0x0019DB38 File Offset: 0x0019BD38
			public TableLayout.LayoutInfo[] FixedChildrenInfo
			{
				get
				{
					TableLayout.LayoutInfo[] array = new TableLayout.LayoutInfo[this._countFixedChildren];
					if (this.HasChildWithAbsolutePositioning)
					{
						int num = 0;
						for (int i = 0; i < this._childInfo.Length; i++)
						{
							if (this._childInfo[i].IsAbsolutelyPositioned)
							{
								array[num++] = this._childInfo[i];
							}
						}
						object[] array2 = array;
						TableLayout.Sort(array2, TableLayout.PreAssignedPositionComparer.GetInstance);
					}
					return array;
				}
			}

			// Token: 0x170018C6 RID: 6342
			// (get) Token: 0x06007100 RID: 28928 RVA: 0x0019DB9A File Offset: 0x0019BD9A
			// (set) Token: 0x06007101 RID: 28929 RVA: 0x0019DBAC File Offset: 0x0019BDAC
			public bool Valid
			{
				get
				{
					return this._state[TableLayout.ContainerInfo.stateValid];
				}
				set
				{
					this._state[TableLayout.ContainerInfo.stateValid] = value;
					if (!this._state[TableLayout.ContainerInfo.stateValid])
					{
						this._state[TableLayout.ContainerInfo.stateChildInfoValid] = false;
					}
				}
			}

			// Token: 0x170018C7 RID: 6343
			// (get) Token: 0x06007102 RID: 28930 RVA: 0x0019DBE2 File Offset: 0x0019BDE2
			public bool HasChildWithAbsolutePositioning
			{
				get
				{
					return this._countFixedChildren > 0;
				}
			}

			// Token: 0x170018C8 RID: 6344
			// (get) Token: 0x06007103 RID: 28931 RVA: 0x0019DBF0 File Offset: 0x0019BDF0
			public bool HasMultiplePercentColumns
			{
				get
				{
					if (this._colStyles != null)
					{
						bool flag = false;
						foreach (object obj in ((IEnumerable)this._colStyles))
						{
							ColumnStyle columnStyle = (ColumnStyle)obj;
							if (columnStyle.SizeType == SizeType.Percent)
							{
								if (flag)
								{
									return true;
								}
								flag = true;
							}
						}
						return false;
					}
					return false;
				}
			}

			// Token: 0x170018C9 RID: 6345
			// (get) Token: 0x06007104 RID: 28932 RVA: 0x0019DC64 File Offset: 0x0019BE64
			// (set) Token: 0x06007105 RID: 28933 RVA: 0x0019DC76 File Offset: 0x0019BE76
			public bool ChildHasColumnSpan
			{
				get
				{
					return this._state[TableLayout.ContainerInfo.stateChildHasColumnSpan];
				}
				set
				{
					this._state[TableLayout.ContainerInfo.stateChildHasColumnSpan] = value;
				}
			}

			// Token: 0x170018CA RID: 6346
			// (get) Token: 0x06007106 RID: 28934 RVA: 0x0019DC89 File Offset: 0x0019BE89
			// (set) Token: 0x06007107 RID: 28935 RVA: 0x0019DC9B File Offset: 0x0019BE9B
			public bool ChildHasRowSpan
			{
				get
				{
					return this._state[TableLayout.ContainerInfo.stateChildHasRowSpan];
				}
				set
				{
					this._state[TableLayout.ContainerInfo.stateChildHasRowSpan] = value;
				}
			}

			// Token: 0x06007108 RID: 28936 RVA: 0x0019DCB0 File Offset: 0x0019BEB0
			public Size GetCachedPreferredSize(Size proposedContstraints, out bool isValid)
			{
				isValid = false;
				if (proposedContstraints.Height == 0 || proposedContstraints.Width == 0)
				{
					Size size = CommonProperties.xGetPreferredSizeCache(this.Container);
					if (!size.IsEmpty)
					{
						isValid = true;
						return size;
					}
				}
				return Size.Empty;
			}

			// Token: 0x04004400 RID: 17408
			private static TableLayout.Strip[] emptyStrip = new TableLayout.Strip[0];

			// Token: 0x04004401 RID: 17409
			private static readonly int stateValid = BitVector32.CreateMask();

			// Token: 0x04004402 RID: 17410
			private static readonly int stateChildInfoValid = BitVector32.CreateMask(TableLayout.ContainerInfo.stateValid);

			// Token: 0x04004403 RID: 17411
			private static readonly int stateChildHasColumnSpan = BitVector32.CreateMask(TableLayout.ContainerInfo.stateChildInfoValid);

			// Token: 0x04004404 RID: 17412
			private static readonly int stateChildHasRowSpan = BitVector32.CreateMask(TableLayout.ContainerInfo.stateChildHasColumnSpan);

			// Token: 0x04004405 RID: 17413
			private int _cellBorderWidth;

			// Token: 0x04004406 RID: 17414
			private TableLayout.Strip[] _cols = TableLayout.ContainerInfo.emptyStrip;

			// Token: 0x04004407 RID: 17415
			private TableLayout.Strip[] _rows = TableLayout.ContainerInfo.emptyStrip;

			// Token: 0x04004408 RID: 17416
			private int _maxRows;

			// Token: 0x04004409 RID: 17417
			private int _maxColumns;

			// Token: 0x0400440A RID: 17418
			private TableLayoutRowStyleCollection _rowStyles;

			// Token: 0x0400440B RID: 17419
			private TableLayoutColumnStyleCollection _colStyles;

			// Token: 0x0400440C RID: 17420
			private TableLayoutPanelGrowStyle _growStyle;

			// Token: 0x0400440D RID: 17421
			private IArrangedElement _container;

			// Token: 0x0400440E RID: 17422
			private TableLayout.LayoutInfo[] _childInfo;

			// Token: 0x0400440F RID: 17423
			private int _countFixedChildren;

			// Token: 0x04004410 RID: 17424
			private int _minRowsAndColumns;

			// Token: 0x04004411 RID: 17425
			private int _minColumns;

			// Token: 0x04004412 RID: 17426
			private int _minRows;

			// Token: 0x04004413 RID: 17427
			private BitVector32 _state;
		}

		// Token: 0x02000868 RID: 2152
		private abstract class SizeProxy
		{
			// Token: 0x170018CB RID: 6347
			// (get) Token: 0x0600710A RID: 28938 RVA: 0x0019DD43 File Offset: 0x0019BF43
			// (set) Token: 0x0600710B RID: 28939 RVA: 0x0019DD4B File Offset: 0x0019BF4B
			public TableLayout.Strip Strip
			{
				get
				{
					return this.strip;
				}
				set
				{
					this.strip = value;
				}
			}

			// Token: 0x170018CC RID: 6348
			// (get) Token: 0x0600710C RID: 28940
			// (set) Token: 0x0600710D RID: 28941
			public abstract int Size { get; set; }

			// Token: 0x04004414 RID: 17428
			protected TableLayout.Strip strip;
		}

		// Token: 0x02000869 RID: 2153
		private class MinSizeProxy : TableLayout.SizeProxy
		{
			// Token: 0x170018CD RID: 6349
			// (get) Token: 0x0600710F RID: 28943 RVA: 0x0019DD54 File Offset: 0x0019BF54
			// (set) Token: 0x06007110 RID: 28944 RVA: 0x0019DD61 File Offset: 0x0019BF61
			public override int Size
			{
				get
				{
					return this.strip.MinSize;
				}
				set
				{
					this.strip.MinSize = value;
				}
			}

			// Token: 0x170018CE RID: 6350
			// (get) Token: 0x06007111 RID: 28945 RVA: 0x0019DD6F File Offset: 0x0019BF6F
			public static TableLayout.MinSizeProxy GetInstance
			{
				get
				{
					return TableLayout.MinSizeProxy.instance;
				}
			}

			// Token: 0x04004415 RID: 17429
			private static readonly TableLayout.MinSizeProxy instance = new TableLayout.MinSizeProxy();
		}

		// Token: 0x0200086A RID: 2154
		private class MaxSizeProxy : TableLayout.SizeProxy
		{
			// Token: 0x170018CF RID: 6351
			// (get) Token: 0x06007114 RID: 28948 RVA: 0x0019DD8A File Offset: 0x0019BF8A
			// (set) Token: 0x06007115 RID: 28949 RVA: 0x0019DD97 File Offset: 0x0019BF97
			public override int Size
			{
				get
				{
					return this.strip.MaxSize;
				}
				set
				{
					this.strip.MaxSize = value;
				}
			}

			// Token: 0x170018D0 RID: 6352
			// (get) Token: 0x06007116 RID: 28950 RVA: 0x0019DDA5 File Offset: 0x0019BFA5
			public static TableLayout.MaxSizeProxy GetInstance
			{
				get
				{
					return TableLayout.MaxSizeProxy.instance;
				}
			}

			// Token: 0x04004416 RID: 17430
			private static readonly TableLayout.MaxSizeProxy instance = new TableLayout.MaxSizeProxy();
		}

		// Token: 0x0200086B RID: 2155
		private abstract class SpanComparer : IComparer
		{
			// Token: 0x06007119 RID: 28953
			public abstract int GetSpan(TableLayout.LayoutInfo layoutInfo);

			// Token: 0x0600711A RID: 28954 RVA: 0x0019DDB8 File Offset: 0x0019BFB8
			public int Compare(object x, object y)
			{
				TableLayout.LayoutInfo layoutInfo = (TableLayout.LayoutInfo)x;
				TableLayout.LayoutInfo layoutInfo2 = (TableLayout.LayoutInfo)y;
				return this.GetSpan(layoutInfo) - this.GetSpan(layoutInfo2);
			}
		}

		// Token: 0x0200086C RID: 2156
		private class RowSpanComparer : TableLayout.SpanComparer
		{
			// Token: 0x0600711C RID: 28956 RVA: 0x0019DDE2 File Offset: 0x0019BFE2
			public override int GetSpan(TableLayout.LayoutInfo layoutInfo)
			{
				return layoutInfo.RowSpan;
			}

			// Token: 0x170018D1 RID: 6353
			// (get) Token: 0x0600711D RID: 28957 RVA: 0x0019DDEA File Offset: 0x0019BFEA
			public static TableLayout.RowSpanComparer GetInstance
			{
				get
				{
					return TableLayout.RowSpanComparer.instance;
				}
			}

			// Token: 0x04004417 RID: 17431
			private static readonly TableLayout.RowSpanComparer instance = new TableLayout.RowSpanComparer();
		}

		// Token: 0x0200086D RID: 2157
		private class ColumnSpanComparer : TableLayout.SpanComparer
		{
			// Token: 0x06007120 RID: 28960 RVA: 0x0019DE05 File Offset: 0x0019C005
			public override int GetSpan(TableLayout.LayoutInfo layoutInfo)
			{
				return layoutInfo.ColumnSpan;
			}

			// Token: 0x170018D2 RID: 6354
			// (get) Token: 0x06007121 RID: 28961 RVA: 0x0019DE0D File Offset: 0x0019C00D
			public static TableLayout.ColumnSpanComparer GetInstance
			{
				get
				{
					return TableLayout.ColumnSpanComparer.instance;
				}
			}

			// Token: 0x04004418 RID: 17432
			private static readonly TableLayout.ColumnSpanComparer instance = new TableLayout.ColumnSpanComparer();
		}

		// Token: 0x0200086E RID: 2158
		private class PostAssignedPositionComparer : IComparer
		{
			// Token: 0x170018D3 RID: 6355
			// (get) Token: 0x06007124 RID: 28964 RVA: 0x0019DE20 File Offset: 0x0019C020
			public static TableLayout.PostAssignedPositionComparer GetInstance
			{
				get
				{
					return TableLayout.PostAssignedPositionComparer.instance;
				}
			}

			// Token: 0x06007125 RID: 28965 RVA: 0x0019DE28 File Offset: 0x0019C028
			public int Compare(object x, object y)
			{
				TableLayout.LayoutInfo layoutInfo = (TableLayout.LayoutInfo)x;
				TableLayout.LayoutInfo layoutInfo2 = (TableLayout.LayoutInfo)y;
				if (layoutInfo.RowStart < layoutInfo2.RowStart)
				{
					return -1;
				}
				if (layoutInfo.RowStart > layoutInfo2.RowStart)
				{
					return 1;
				}
				if (layoutInfo.ColumnStart < layoutInfo2.ColumnStart)
				{
					return -1;
				}
				if (layoutInfo.ColumnStart > layoutInfo2.ColumnStart)
				{
					return 1;
				}
				return 0;
			}

			// Token: 0x04004419 RID: 17433
			private static readonly TableLayout.PostAssignedPositionComparer instance = new TableLayout.PostAssignedPositionComparer();
		}

		// Token: 0x0200086F RID: 2159
		private class PreAssignedPositionComparer : IComparer
		{
			// Token: 0x170018D4 RID: 6356
			// (get) Token: 0x06007128 RID: 28968 RVA: 0x0019DE90 File Offset: 0x0019C090
			public static TableLayout.PreAssignedPositionComparer GetInstance
			{
				get
				{
					return TableLayout.PreAssignedPositionComparer.instance;
				}
			}

			// Token: 0x06007129 RID: 28969 RVA: 0x0019DE98 File Offset: 0x0019C098
			public int Compare(object x, object y)
			{
				TableLayout.LayoutInfo layoutInfo = (TableLayout.LayoutInfo)x;
				TableLayout.LayoutInfo layoutInfo2 = (TableLayout.LayoutInfo)y;
				if (layoutInfo.RowPosition < layoutInfo2.RowPosition)
				{
					return -1;
				}
				if (layoutInfo.RowPosition > layoutInfo2.RowPosition)
				{
					return 1;
				}
				if (layoutInfo.ColumnPosition < layoutInfo2.ColumnPosition)
				{
					return -1;
				}
				if (layoutInfo.ColumnPosition > layoutInfo2.ColumnPosition)
				{
					return 1;
				}
				return 0;
			}

			// Token: 0x0400441A RID: 17434
			private static readonly TableLayout.PreAssignedPositionComparer instance = new TableLayout.PreAssignedPositionComparer();
		}

		// Token: 0x02000870 RID: 2160
		private sealed class ReservationGrid
		{
			// Token: 0x0600712C RID: 28972 RVA: 0x0019DF00 File Offset: 0x0019C100
			public bool IsReserved(int column, int rowOffset)
			{
				return rowOffset < this._rows.Count && column < ((BitArray)this._rows[rowOffset]).Length && ((BitArray)this._rows[rowOffset])[column];
			}

			// Token: 0x0600712D RID: 28973 RVA: 0x0019DF50 File Offset: 0x0019C150
			public void Reserve(int column, int rowOffset)
			{
				while (rowOffset >= this._rows.Count)
				{
					this._rows.Add(new BitArray(this._numColumns));
				}
				if (column >= ((BitArray)this._rows[rowOffset]).Length)
				{
					((BitArray)this._rows[rowOffset]).Length = column + 1;
					if (column >= this._numColumns)
					{
						this._numColumns = column + 1;
					}
				}
				((BitArray)this._rows[rowOffset])[column] = true;
			}

			// Token: 0x0600712E RID: 28974 RVA: 0x0019DFE0 File Offset: 0x0019C1E0
			public void ReserveAll(TableLayout.LayoutInfo layoutInfo, int rowStop, int colStop)
			{
				for (int i = 1; i < rowStop - layoutInfo.RowStart; i++)
				{
					for (int j = layoutInfo.ColumnStart; j < colStop; j++)
					{
						this.Reserve(j, i);
					}
				}
			}

			// Token: 0x0600712F RID: 28975 RVA: 0x0019E019 File Offset: 0x0019C219
			public void AdvanceRow()
			{
				if (this._rows.Count > 0)
				{
					this._rows.RemoveAt(0);
				}
			}

			// Token: 0x0400441B RID: 17435
			private int _numColumns = 1;

			// Token: 0x0400441C RID: 17436
			private ArrayList _rows = new ArrayList();
		}

		// Token: 0x02000871 RID: 2161
		internal struct Strip
		{
			// Token: 0x170018D5 RID: 6357
			// (get) Token: 0x06007131 RID: 28977 RVA: 0x0019E04F File Offset: 0x0019C24F
			// (set) Token: 0x06007132 RID: 28978 RVA: 0x0019E057 File Offset: 0x0019C257
			public int MinSize
			{
				get
				{
					return this._minSize;
				}
				set
				{
					this._minSize = value;
				}
			}

			// Token: 0x170018D6 RID: 6358
			// (get) Token: 0x06007133 RID: 28979 RVA: 0x0019E060 File Offset: 0x0019C260
			// (set) Token: 0x06007134 RID: 28980 RVA: 0x0019E068 File Offset: 0x0019C268
			public int MaxSize
			{
				get
				{
					return this._maxSize;
				}
				set
				{
					this._maxSize = value;
				}
			}

			// Token: 0x170018D7 RID: 6359
			// (get) Token: 0x06007135 RID: 28981 RVA: 0x0019E071 File Offset: 0x0019C271
			// (set) Token: 0x06007136 RID: 28982 RVA: 0x0019E079 File Offset: 0x0019C279
			public bool IsStart
			{
				get
				{
					return this._isStart;
				}
				set
				{
					this._isStart = value;
				}
			}

			// Token: 0x0400441D RID: 17437
			private int _maxSize;

			// Token: 0x0400441E RID: 17438
			private int _minSize;

			// Token: 0x0400441F RID: 17439
			private bool _isStart;
		}
	}
}
