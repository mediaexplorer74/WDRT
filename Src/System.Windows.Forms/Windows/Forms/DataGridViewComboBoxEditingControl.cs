using System;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents the hosted combo box control in a <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" />.</summary>
	// Token: 0x020001C9 RID: 457
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	public class DataGridViewComboBoxEditingControl : ComboBox, IDataGridViewEditingControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewComboBoxEditingControl" /> class.</summary>
		// Token: 0x0600203B RID: 8251 RVA: 0x0009B493 File Offset: 0x00099693
		public DataGridViewComboBoxEditingControl()
		{
			base.TabStop = false;
		}

		/// <summary>Creates a new <see cref="T:System.Windows.Forms.AccessibleObject" /> for this <see cref="T:System.Windows.Forms.DataGridViewComboBoxEditingControl" /> instance.</summary>
		/// <returns>A new accessibility object.</returns>
		// Token: 0x0600203C RID: 8252 RVA: 0x0009B4A2 File Offset: 0x000996A2
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level3)
			{
				return new DataGridViewComboBoxEditingControlAccessibleObject(this);
			}
			if (AccessibilityImprovements.Level2)
			{
				return new DataGridViewEditingControlAccessibleObject(this);
			}
			return base.CreateAccessibilityInstance();
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.DataGridView" /> that contains the combo box control.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridView" /> that contains the <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" /> that contains this control; otherwise, <see langword="null" /> if there is no associated <see cref="T:System.Windows.Forms.DataGridView" />.</returns>
		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x0600203D RID: 8253 RVA: 0x0009B4C6 File Offset: 0x000996C6
		// (set) Token: 0x0600203E RID: 8254 RVA: 0x0009B4CE File Offset: 0x000996CE
		public virtual DataGridView EditingControlDataGridView
		{
			get
			{
				return this.dataGridView;
			}
			set
			{
				this.dataGridView = value;
			}
		}

		/// <summary>Gets or sets the formatted representation of the current value of the control.</summary>
		/// <returns>An object representing the current value of this control.</returns>
		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x0600203F RID: 8255 RVA: 0x0009B4D7 File Offset: 0x000996D7
		// (set) Token: 0x06002040 RID: 8256 RVA: 0x0009B4E0 File Offset: 0x000996E0
		public virtual object EditingControlFormattedValue
		{
			get
			{
				return this.GetEditingControlFormattedValue(DataGridViewDataErrorContexts.Formatting);
			}
			set
			{
				string text = value as string;
				if (text != null)
				{
					this.Text = text;
					if (string.Compare(text, this.Text, true, CultureInfo.CurrentCulture) != 0)
					{
						this.SelectedIndex = -1;
					}
				}
			}
		}

		/// <summary>Gets or sets the index of the owning cell's parent row.</summary>
		/// <returns>The index of the row that contains the owning cell; -1 if there is no owning row.</returns>
		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06002041 RID: 8257 RVA: 0x0009B519 File Offset: 0x00099719
		// (set) Token: 0x06002042 RID: 8258 RVA: 0x0009B521 File Offset: 0x00099721
		public virtual int EditingControlRowIndex
		{
			get
			{
				return this.rowIndex;
			}
			set
			{
				this.rowIndex = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the current value of the control has changed.</summary>
		/// <returns>
		///   <see langword="true" /> if the value of the control has changed; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06002043 RID: 8259 RVA: 0x0009B52A File Offset: 0x0009972A
		// (set) Token: 0x06002044 RID: 8260 RVA: 0x0009B532 File Offset: 0x00099732
		public virtual bool EditingControlValueChanged
		{
			get
			{
				return this.valueChanged;
			}
			set
			{
				this.valueChanged = value;
			}
		}

		/// <summary>Gets the cursor used during editing.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor image used by the mouse pointer during editing.</returns>
		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06002045 RID: 8261 RVA: 0x0003057E File Offset: 0x0002E77E
		public virtual Cursor EditingPanelCursor
		{
			get
			{
				return Cursors.Default;
			}
		}

		/// <summary>Gets a value indicating whether the cell contents need to be repositioned whenever the value changes.</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06002046 RID: 8262 RVA: 0x0001180C File Offset: 0x0000FA0C
		public virtual bool RepositionEditingControlOnValueChange
		{
			get
			{
				return false;
			}
		}

		/// <summary>Changes the control's user interface (UI) to be consistent with the specified cell style.</summary>
		/// <param name="dataGridViewCellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to use as a pattern for the UI.</param>
		// Token: 0x06002047 RID: 8263 RVA: 0x0009B53C File Offset: 0x0009973C
		public virtual void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
		{
			this.Font = dataGridViewCellStyle.Font;
			if (dataGridViewCellStyle.BackColor.A < 255)
			{
				Color color = Color.FromArgb(255, dataGridViewCellStyle.BackColor);
				this.BackColor = color;
				this.dataGridView.EditingPanel.BackColor = color;
			}
			else
			{
				this.BackColor = dataGridViewCellStyle.BackColor;
			}
			this.ForeColor = dataGridViewCellStyle.ForeColor;
		}

		/// <summary>Determines whether the specified key is a regular input key that the editing control should process or a special key that the <see cref="T:System.Windows.Forms.DataGridView" /> should process.</summary>
		/// <param name="keyData">A bitwise combination of <see cref="T:System.Windows.Forms.Keys" /> values that represents the key that was pressed.</param>
		/// <param name="dataGridViewWantsInputKey">
		///   <see langword="true" /> to indicate that the <see cref="T:System.Windows.Forms.DataGridView" /> control can process the key; otherwise, <see langword="false" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified key is a regular input key that should be handled by the editing control; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002048 RID: 8264 RVA: 0x0009B5AD File Offset: 0x000997AD
		public virtual bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
		{
			return (keyData & Keys.KeyCode) == Keys.Down || (keyData & Keys.KeyCode) == Keys.Up || (base.DroppedDown && (keyData & Keys.KeyCode) == Keys.Escape) || (keyData & Keys.KeyCode) == Keys.Return || !dataGridViewWantsInputKey;
		}

		/// <summary>Retrieves the formatted value of the cell.</summary>
		/// <param name="context">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts" /> values that specifies the data error context.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the formatted version of the cell contents.</returns>
		// Token: 0x06002049 RID: 8265 RVA: 0x0009B5E9 File Offset: 0x000997E9
		public virtual object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
		{
			return this.Text;
		}

		/// <summary>Prepares the currently selected cell for editing.</summary>
		/// <param name="selectAll">
		///   <see langword="true" /> to select all of the cell's content; otherwise, <see langword="false" />.</param>
		// Token: 0x0600204A RID: 8266 RVA: 0x0009B5F1 File Offset: 0x000997F1
		public virtual void PrepareEditingControlForEdit(bool selectAll)
		{
			if (selectAll)
			{
				base.SelectAll();
			}
		}

		// Token: 0x0600204B RID: 8267 RVA: 0x0009B5FC File Offset: 0x000997FC
		private void NotifyDataGridViewOfValueChange()
		{
			this.valueChanged = true;
			this.dataGridView.NotifyCurrentCellDirty(true);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ComboBox.SelectedIndexChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600204C RID: 8268 RVA: 0x0009B611 File Offset: 0x00099811
		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			base.OnSelectedIndexChanged(e);
			if (this.SelectedIndex != -1)
			{
				this.NotifyDataGridViewOfValueChange();
			}
		}

		// Token: 0x04000D87 RID: 3463
		private DataGridView dataGridView;

		// Token: 0x04000D88 RID: 3464
		private bool valueChanged;

		// Token: 0x04000D89 RID: 3465
		private int rowIndex;
	}
}
