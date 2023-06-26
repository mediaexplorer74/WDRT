using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides the base class for elements of a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
	// Token: 0x020001D3 RID: 467
	public class DataGridViewElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewElement" /> class.</summary>
		// Token: 0x06002078 RID: 8312 RVA: 0x0009B84F File Offset: 0x00099A4F
		public DataGridViewElement()
		{
			this.state = DataGridViewElementStates.Visible;
		}

		// Token: 0x06002079 RID: 8313 RVA: 0x0009B85F File Offset: 0x00099A5F
		internal DataGridViewElement(DataGridViewElement dgveTemplate)
		{
			this.state = dgveTemplate.State & (DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.ResizableSet | DataGridViewElementStates.Visible);
		}

		/// <summary>Gets the user interface (UI) state of the element.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values representing the state.</returns>
		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x0600207A RID: 8314 RVA: 0x0009B876 File Offset: 0x00099A76
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual DataGridViewElementStates State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x17000760 RID: 1888
		// (set) Token: 0x0600207B RID: 8315 RVA: 0x0009B87E File Offset: 0x00099A7E
		internal DataGridViewElementStates StateInternal
		{
			set
			{
				this.state = value;
			}
		}

		// Token: 0x0600207C RID: 8316 RVA: 0x0009B887 File Offset: 0x00099A87
		internal bool StateIncludes(DataGridViewElementStates elementState)
		{
			return (this.State & elementState) == elementState;
		}

		// Token: 0x0600207D RID: 8317 RVA: 0x0009B894 File Offset: 0x00099A94
		internal bool StateExcludes(DataGridViewElementStates elementState)
		{
			return (this.State & elementState) == DataGridViewElementStates.None;
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridView" /> control associated with this element.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridView" /> control that contains this element. The default is <see langword="null" />.</returns>
		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x0600207E RID: 8318 RVA: 0x0009B8A1 File Offset: 0x00099AA1
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataGridView DataGridView
		{
			get
			{
				return this.dataGridView;
			}
		}

		// Token: 0x17000762 RID: 1890
		// (set) Token: 0x0600207F RID: 8319 RVA: 0x0009B8A9 File Offset: 0x00099AA9
		internal DataGridView DataGridViewInternal
		{
			set
			{
				if (this.DataGridView != value)
				{
					this.dataGridView = value;
					this.OnDataGridViewChanged();
				}
			}
		}

		/// <summary>Called when the element is associated with a different <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		// Token: 0x06002080 RID: 8320 RVA: 0x000070A6 File Offset: 0x000052A6
		protected virtual void OnDataGridViewChanged()
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellClick" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data.</param>
		// Token: 0x06002081 RID: 8321 RVA: 0x0009B8C1 File Offset: 0x00099AC1
		protected void RaiseCellClick(DataGridViewCellEventArgs e)
		{
			if (this.dataGridView != null)
			{
				this.dataGridView.OnCellClickInternal(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellContentClick" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data.</param>
		// Token: 0x06002082 RID: 8322 RVA: 0x0009B8D7 File Offset: 0x00099AD7
		protected void RaiseCellContentClick(DataGridViewCellEventArgs e)
		{
			if (this.dataGridView != null)
			{
				this.dataGridView.OnCellContentClickInternal(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellContentDoubleClick" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data.</param>
		// Token: 0x06002083 RID: 8323 RVA: 0x0009B8ED File Offset: 0x00099AED
		protected void RaiseCellContentDoubleClick(DataGridViewCellEventArgs e)
		{
			if (this.dataGridView != null)
			{
				this.dataGridView.OnCellContentDoubleClickInternal(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellValueChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data.</param>
		// Token: 0x06002084 RID: 8324 RVA: 0x0009B903 File Offset: 0x00099B03
		protected void RaiseCellValueChanged(DataGridViewCellEventArgs e)
		{
			if (this.dataGridView != null)
			{
				this.dataGridView.OnCellValueChangedInternal(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewDataErrorEventArgs" /> that contains the event data.</param>
		// Token: 0x06002085 RID: 8325 RVA: 0x0009B919 File Offset: 0x00099B19
		protected void RaiseDataError(DataGridViewDataErrorEventArgs e)
		{
			if (this.dataGridView != null)
			{
				this.dataGridView.OnDataErrorInternal(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseWheel" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06002086 RID: 8326 RVA: 0x0009B92F File Offset: 0x00099B2F
		protected void RaiseMouseWheel(MouseEventArgs e)
		{
			if (this.dataGridView != null)
			{
				this.dataGridView.OnMouseWheelInternal(e);
			}
		}

		// Token: 0x04000DAE RID: 3502
		private DataGridViewElementStates state;

		// Token: 0x04000DAF RID: 3503
		private DataGridView dataGridView;
	}
}
