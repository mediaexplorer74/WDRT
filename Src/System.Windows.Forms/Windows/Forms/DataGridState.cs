using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

namespace System.Windows.Forms
{
	// Token: 0x02000187 RID: 391
	internal sealed class DataGridState : ICloneable
	{
		// Token: 0x06001764 RID: 5988 RVA: 0x00054A61 File Offset: 0x00052C61
		public DataGridState()
		{
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x00054A75 File Offset: 0x00052C75
		public DataGridState(DataGrid dataGrid)
		{
			this.PushState(dataGrid);
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001766 RID: 5990 RVA: 0x00054A90 File Offset: 0x00052C90
		internal AccessibleObject ParentRowAccessibleObject
		{
			get
			{
				if (this.parentRowAccessibleObject == null)
				{
					this.parentRowAccessibleObject = new DataGridState.DataGridStateParentRowAccessibleObject(this);
				}
				return this.parentRowAccessibleObject;
			}
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x00054AAC File Offset: 0x00052CAC
		public object Clone()
		{
			return new DataGridState
			{
				DataGridRows = this.DataGridRows,
				DataSource = this.DataSource,
				DataMember = this.DataMember,
				FirstVisibleRow = this.FirstVisibleRow,
				FirstVisibleCol = this.FirstVisibleCol,
				CurrentRow = this.CurrentRow,
				CurrentCol = this.CurrentCol,
				GridColumnStyles = this.GridColumnStyles,
				ListManager = this.ListManager,
				DataGrid = this.DataGrid
			};
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x00054B38 File Offset: 0x00052D38
		public void PushState(DataGrid dataGrid)
		{
			this.DataSource = dataGrid.DataSource;
			this.DataMember = dataGrid.DataMember;
			this.DataGrid = dataGrid;
			this.DataGridRows = dataGrid.DataGridRows;
			this.DataGridRowsLength = dataGrid.DataGridRowsLength;
			this.FirstVisibleRow = dataGrid.firstVisibleRow;
			this.FirstVisibleCol = dataGrid.firstVisibleCol;
			this.CurrentRow = dataGrid.currentRow;
			this.GridColumnStyles = new GridColumnStylesCollection(dataGrid.myGridTable);
			this.GridColumnStyles.Clear();
			foreach (object obj in dataGrid.myGridTable.GridColumnStyles)
			{
				DataGridColumnStyle dataGridColumnStyle = (DataGridColumnStyle)obj;
				this.GridColumnStyles.Add(dataGridColumnStyle);
			}
			this.ListManager = dataGrid.ListManager;
			this.ListManager.ItemChanged += this.DataSource_Changed;
			this.ListManager.MetaDataChanged += this.DataSource_MetaDataChanged;
			this.CurrentCol = dataGrid.currentCol;
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x00054C5C File Offset: 0x00052E5C
		public void RemoveChangeNotification()
		{
			this.ListManager.ItemChanged -= this.DataSource_Changed;
			this.ListManager.MetaDataChanged -= this.DataSource_MetaDataChanged;
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x00054C8C File Offset: 0x00052E8C
		public void PullState(DataGrid dataGrid, bool createColumn)
		{
			dataGrid.Set_ListManager(this.DataSource, this.DataMember, true, createColumn);
			dataGrid.firstVisibleRow = this.FirstVisibleRow;
			dataGrid.firstVisibleCol = this.FirstVisibleCol;
			dataGrid.currentRow = this.CurrentRow;
			dataGrid.currentCol = this.CurrentCol;
			dataGrid.SetDataGridRows(this.DataGridRows, this.DataGridRowsLength);
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x00054CEF File Offset: 0x00052EEF
		private void DataSource_Changed(object sender, ItemChangedEventArgs e)
		{
			if (this.DataGrid != null && this.ListManager.Position == e.Index)
			{
				this.DataGrid.InvalidateParentRows();
				return;
			}
			if (this.DataGrid != null)
			{
				this.DataGrid.ParentRowsDataChanged();
			}
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x00054D2B File Offset: 0x00052F2B
		private void DataSource_MetaDataChanged(object sender, EventArgs e)
		{
			if (this.DataGrid != null)
			{
				this.DataGrid.ParentRowsDataChanged();
			}
		}

		// Token: 0x04000A7B RID: 2683
		public object DataSource;

		// Token: 0x04000A7C RID: 2684
		public string DataMember;

		// Token: 0x04000A7D RID: 2685
		public CurrencyManager ListManager;

		// Token: 0x04000A7E RID: 2686
		public DataGridRow[] DataGridRows = new DataGridRow[0];

		// Token: 0x04000A7F RID: 2687
		public DataGrid DataGrid;

		// Token: 0x04000A80 RID: 2688
		public int DataGridRowsLength;

		// Token: 0x04000A81 RID: 2689
		public GridColumnStylesCollection GridColumnStyles;

		// Token: 0x04000A82 RID: 2690
		public int FirstVisibleRow;

		// Token: 0x04000A83 RID: 2691
		public int FirstVisibleCol;

		// Token: 0x04000A84 RID: 2692
		public int CurrentRow;

		// Token: 0x04000A85 RID: 2693
		public int CurrentCol;

		// Token: 0x04000A86 RID: 2694
		public DataGridRow LinkingRow;

		// Token: 0x04000A87 RID: 2695
		private AccessibleObject parentRowAccessibleObject;

		// Token: 0x02000653 RID: 1619
		[ComVisible(true)]
		internal class DataGridStateParentRowAccessibleObject : AccessibleObject
		{
			// Token: 0x0600651D RID: 25885 RVA: 0x00178308 File Offset: 0x00176508
			public DataGridStateParentRowAccessibleObject(DataGridState owner)
			{
				this.owner = owner;
			}

			// Token: 0x170015CB RID: 5579
			// (get) Token: 0x0600651E RID: 25886 RVA: 0x00178318 File Offset: 0x00176518
			public override Rectangle Bounds
			{
				get
				{
					DataGridParentRows dataGridParentRows = ((DataGridParentRows.DataGridParentRowsAccessibleObject)this.Parent).Owner;
					DataGrid dataGrid = this.owner.LinkingRow.DataGrid;
					Rectangle boundsForDataGridStateAccesibility = dataGridParentRows.GetBoundsForDataGridStateAccesibility(this.owner);
					boundsForDataGridStateAccesibility.Y += dataGrid.ParentRowsBounds.Y;
					return dataGrid.RectangleToScreen(boundsForDataGridStateAccesibility);
				}
			}

			// Token: 0x170015CC RID: 5580
			// (get) Token: 0x0600651F RID: 25887 RVA: 0x00178377 File Offset: 0x00176577
			public override string Name
			{
				get
				{
					return SR.GetString("AccDGParentRow");
				}
			}

			// Token: 0x170015CD RID: 5581
			// (get) Token: 0x06006520 RID: 25888 RVA: 0x00178383 File Offset: 0x00176583
			public override AccessibleObject Parent
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return this.owner.LinkingRow.DataGrid.ParentRowsAccessibleObject;
				}
			}

			// Token: 0x170015CE RID: 5582
			// (get) Token: 0x06006521 RID: 25889 RVA: 0x00015EF1 File Offset: 0x000140F1
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.ListItem;
				}
			}

			// Token: 0x170015CF RID: 5583
			// (get) Token: 0x06006522 RID: 25890 RVA: 0x0017839C File Offset: 0x0017659C
			public override string Value
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					StringBuilder stringBuilder = new StringBuilder();
					CurrencyManager currencyManager = (CurrencyManager)this.owner.LinkingRow.DataGrid.BindingContext[this.owner.DataSource, this.owner.DataMember];
					stringBuilder.Append(this.owner.ListManager.GetListName());
					stringBuilder.Append(": ");
					bool flag = false;
					foreach (object obj in this.owner.GridColumnStyles)
					{
						DataGridColumnStyle dataGridColumnStyle = (DataGridColumnStyle)obj;
						if (flag)
						{
							stringBuilder.Append(", ");
						}
						string headerText = dataGridColumnStyle.HeaderText;
						string text = dataGridColumnStyle.PropertyDescriptor.Converter.ConvertToString(dataGridColumnStyle.PropertyDescriptor.GetValue(currencyManager.Current));
						stringBuilder.Append(headerText);
						stringBuilder.Append(": ");
						stringBuilder.Append(text);
						flag = true;
					}
					return stringBuilder.ToString();
				}
			}

			// Token: 0x06006523 RID: 25891 RVA: 0x001784BC File Offset: 0x001766BC
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override AccessibleObject Navigate(AccessibleNavigation navdir)
			{
				DataGridParentRows.DataGridParentRowsAccessibleObject dataGridParentRowsAccessibleObject = (DataGridParentRows.DataGridParentRowsAccessibleObject)this.Parent;
				switch (navdir)
				{
				case AccessibleNavigation.Up:
				case AccessibleNavigation.Left:
				case AccessibleNavigation.Previous:
					return dataGridParentRowsAccessibleObject.GetPrev(this);
				case AccessibleNavigation.Down:
				case AccessibleNavigation.Right:
				case AccessibleNavigation.Next:
					return dataGridParentRowsAccessibleObject.GetNext(this);
				default:
					return null;
				}
			}

			// Token: 0x040039D9 RID: 14809
			private DataGridState owner;
		}
	}
}
