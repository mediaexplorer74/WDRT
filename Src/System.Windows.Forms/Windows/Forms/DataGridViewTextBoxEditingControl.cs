using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	/// <summary>Represents a text box control that can be hosted in a <see cref="T:System.Windows.Forms.DataGridViewTextBoxCell" />.</summary>
	// Token: 0x0200021D RID: 541
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	public class DataGridViewTextBoxEditingControl : TextBox, IDataGridViewEditingControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewTextBoxEditingControl" /> class.</summary>
		// Token: 0x06002348 RID: 9032 RVA: 0x000A830E File Offset: 0x000A650E
		public DataGridViewTextBoxEditingControl()
		{
			base.TabStop = false;
		}

		/// <summary>Creates a new <see cref="T:System.Windows.Forms.AccessibleObject" /> for this <see cref="T:System.Windows.Forms.DataGridViewTextBoxEditingControl" /> instance.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> instance that supports the ControlType UIA property.</returns>
		// Token: 0x06002349 RID: 9033 RVA: 0x000A831D File Offset: 0x000A651D
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level5)
			{
				return new DataGridViewTextBoxEditingControlAccessibleObjectLevel5(this);
			}
			if (AccessibilityImprovements.Level3)
			{
				return new DataGridViewTextBoxEditingControlAccessibleObject(this);
			}
			if (AccessibilityImprovements.Level2)
			{
				return new DataGridViewEditingControlAccessibleObject(this);
			}
			return base.CreateAccessibilityInstance();
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.DataGridView" /> that contains the text box control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridView" /> that contains the <see cref="T:System.Windows.Forms.DataGridViewTextBoxCell" /> that contains this control; otherwise, <see langword="null" /> if there is no associated <see cref="T:System.Windows.Forms.DataGridView" />.</returns>
		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x0600234A RID: 9034 RVA: 0x000A834F File Offset: 0x000A654F
		// (set) Token: 0x0600234B RID: 9035 RVA: 0x000A8357 File Offset: 0x000A6557
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

		/// <summary>Gets or sets the formatted representation of the current value of the text box control.</summary>
		/// <returns>An object representing the current value of this control.</returns>
		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x0600234C RID: 9036 RVA: 0x000A8360 File Offset: 0x000A6560
		// (set) Token: 0x0600234D RID: 9037 RVA: 0x000A8369 File Offset: 0x000A6569
		public virtual object EditingControlFormattedValue
		{
			get
			{
				return this.GetEditingControlFormattedValue(DataGridViewDataErrorContexts.Formatting);
			}
			set
			{
				this.Text = (string)value;
			}
		}

		/// <summary>Gets or sets the index of the owning cell's parent row.</summary>
		/// <returns>The index of the row that contains the owning cell; -1 if there is no owning row.</returns>
		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x0600234E RID: 9038 RVA: 0x000A8377 File Offset: 0x000A6577
		// (set) Token: 0x0600234F RID: 9039 RVA: 0x000A837F File Offset: 0x000A657F
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

		/// <summary>Gets or sets a value indicating whether the current value of the text box control has changed.</summary>
		/// <returns>
		///   <see langword="true" /> if the value of the control has changed; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06002350 RID: 9040 RVA: 0x000A8388 File Offset: 0x000A6588
		// (set) Token: 0x06002351 RID: 9041 RVA: 0x000A8390 File Offset: 0x000A6590
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

		/// <summary>Gets the cursor used when the mouse pointer is over the <see cref="P:System.Windows.Forms.DataGridView.EditingPanel" /> but not over the editing control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Cursor" /> that represents the mouse pointer used for the editing panel.</returns>
		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06002352 RID: 9042 RVA: 0x0003057E File Offset: 0x0002E77E
		public virtual Cursor EditingPanelCursor
		{
			get
			{
				return Cursors.Default;
			}
		}

		/// <summary>Gets a value indicating whether the cell contents need to be repositioned whenever the value changes.</summary>
		/// <returns>
		///   <see langword="true" /> if the cell's <see cref="P:System.Windows.Forms.DataGridViewCellStyle.WrapMode" /> is set to <see langword="true" /> and the alignment property is not set to one of the <see cref="T:System.Windows.Forms.DataGridViewContentAlignment" /> values that aligns the content to the top; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06002353 RID: 9043 RVA: 0x000A8399 File Offset: 0x000A6599
		public virtual bool RepositionEditingControlOnValueChange
		{
			get
			{
				return this.repositionOnValueChange;
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06002354 RID: 9044 RVA: 0x000A83A1 File Offset: 0x000A65A1
		internal override bool SupportsUiaProviders
		{
			get
			{
				return AccessibilityImprovements.Level3;
			}
		}

		/// <summary>Changes the control's user interface (UI) to be consistent with the specified cell style.</summary>
		/// <param name="dataGridViewCellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to use as the model for the UI.</param>
		// Token: 0x06002355 RID: 9045 RVA: 0x000A83A8 File Offset: 0x000A65A8
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
			if (dataGridViewCellStyle.WrapMode == DataGridViewTriState.True)
			{
				base.WordWrap = true;
			}
			base.TextAlign = DataGridViewTextBoxEditingControl.TranslateAlignment(dataGridViewCellStyle.Alignment);
			this.repositionOnValueChange = dataGridViewCellStyle.WrapMode == DataGridViewTriState.True && (dataGridViewCellStyle.Alignment & DataGridViewTextBoxEditingControl.anyTop) == DataGridViewContentAlignment.NotSet;
		}

		/// <summary>Determines whether the specified key is a regular input key that the editing control should process or a special key that the <see cref="T:System.Windows.Forms.DataGridView" /> should process.</summary>
		/// <param name="keyData">A <see cref="T:System.Windows.Forms.Keys" /> that represents the key that was pressed.</param>
		/// <param name="dataGridViewWantsInputKey">
		///   <see langword="true" /> when the <see cref="T:System.Windows.Forms.DataGridView" /> wants to process the <paramref name="keyData" />; otherwise, <see langword="false" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified key is a regular input key that should be handled by the editing control; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002356 RID: 9046 RVA: 0x000A845C File Offset: 0x000A665C
		public virtual bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
		{
			Keys keys = keyData & Keys.KeyCode;
			if (keys != Keys.Return)
			{
				switch (keys)
				{
				case Keys.Prior:
				case Keys.Next:
					if (this.valueChanged)
					{
						return true;
					}
					break;
				case Keys.End:
				case Keys.Home:
					if (this.SelectionLength != this.Text.Length)
					{
						return true;
					}
					break;
				case Keys.Left:
					if ((this.RightToLeft == RightToLeft.No && (this.SelectionLength != 0 || base.SelectionStart != 0)) || (this.RightToLeft == RightToLeft.Yes && (this.SelectionLength != 0 || base.SelectionStart != this.Text.Length)))
					{
						return true;
					}
					break;
				case Keys.Up:
					if (this.Text.IndexOf("\r\n") >= 0 && base.SelectionStart + this.SelectionLength >= this.Text.IndexOf("\r\n"))
					{
						return true;
					}
					break;
				case Keys.Right:
					if ((this.RightToLeft == RightToLeft.No && (this.SelectionLength != 0 || base.SelectionStart != this.Text.Length)) || (this.RightToLeft == RightToLeft.Yes && (this.SelectionLength != 0 || base.SelectionStart != 0)))
					{
						return true;
					}
					break;
				case Keys.Down:
				{
					int num = base.SelectionStart + this.SelectionLength;
					if (this.Text.IndexOf("\r\n", num) != -1)
					{
						return true;
					}
					break;
				}
				case Keys.Delete:
					if (this.SelectionLength > 0 || base.SelectionStart < this.Text.Length)
					{
						return true;
					}
					break;
				}
			}
			else if ((keyData & (Keys.Shift | Keys.Control | Keys.Alt)) == Keys.Shift && this.Multiline && base.AcceptsReturn)
			{
				return true;
			}
			return !dataGridViewWantsInputKey;
		}

		/// <summary>Retrieves the formatted value of the cell.</summary>
		/// <param name="context">One of the <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts" /> values that specifies the data error context.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the formatted version of the cell contents.</returns>
		// Token: 0x06002357 RID: 9047 RVA: 0x0009B5E9 File Offset: 0x000997E9
		public virtual object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
		{
			return this.Text;
		}

		/// <summary>Prepares the currently selected cell for editing.</summary>
		/// <param name="selectAll">
		///   <see langword="true" /> to select the cell contents; otherwise, <see langword="false" />.</param>
		// Token: 0x06002358 RID: 9048 RVA: 0x000A8603 File Offset: 0x000A6803
		public virtual void PrepareEditingControlForEdit(bool selectAll)
		{
			if (selectAll)
			{
				base.SelectAll();
				return;
			}
			base.SelectionStart = this.Text.Length;
		}

		// Token: 0x06002359 RID: 9049 RVA: 0x000A8620 File Offset: 0x000A6820
		private void NotifyDataGridViewOfValueChange()
		{
			this.valueChanged = true;
			this.dataGridView.NotifyCurrentCellDirty(true);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event.</summary>
		/// <param name="e">An object that contains the event data.</param>
		// Token: 0x0600235A RID: 9050 RVA: 0x000A8635 File Offset: 0x000A6835
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			if (AccessibilityImprovements.Level3)
			{
				base.AccessibilityObject.RaiseAutomationEvent(20005);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseWheel" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x0600235B RID: 9051 RVA: 0x000A8656 File Offset: 0x000A6856
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			this.dataGridView.OnMouseWheelInternal(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.TextChanged" /> event and notifies the <see cref="T:System.Windows.Forms.DataGridView" /> of the text change.</summary>
		/// <param name="e">The event data.</param>
		// Token: 0x0600235C RID: 9052 RVA: 0x000A8664 File Offset: 0x000A6864
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			this.NotifyDataGridViewOfValueChange();
		}

		/// <summary>Processes key events.</summary>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" /> indicating the key that was pressed.</param>
		/// <returns>
		///   <see langword="true" /> if the key event was handled by the editing control; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600235D RID: 9053 RVA: 0x000A8674 File Offset: 0x000A6874
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override bool ProcessKeyEventArgs(ref Message m)
		{
			Keys keys = (Keys)(int)m.WParam;
			if (keys != Keys.LineFeed)
			{
				if (keys != Keys.Return)
				{
					if (keys == Keys.A)
					{
						if (m.Msg == 256 && Control.ModifierKeys == Keys.Control)
						{
							base.SelectAll();
							return true;
						}
					}
				}
				else if (m.Msg == 258 && (Control.ModifierKeys != Keys.Shift || !this.Multiline || !base.AcceptsReturn))
				{
					return true;
				}
			}
			else if (m.Msg == 258 && Control.ModifierKeys == Keys.Control && this.Multiline && base.AcceptsReturn)
			{
				return true;
			}
			return base.ProcessKeyEventArgs(ref m);
		}

		// Token: 0x0600235E RID: 9054 RVA: 0x000A871C File Offset: 0x000A691C
		private static HorizontalAlignment TranslateAlignment(DataGridViewContentAlignment align)
		{
			if ((align & DataGridViewTextBoxEditingControl.anyRight) != DataGridViewContentAlignment.NotSet)
			{
				return HorizontalAlignment.Right;
			}
			if ((align & DataGridViewTextBoxEditingControl.anyCenter) != DataGridViewContentAlignment.NotSet)
			{
				return HorizontalAlignment.Center;
			}
			return HorizontalAlignment.Left;
		}

		// Token: 0x04000E7D RID: 3709
		private static readonly DataGridViewContentAlignment anyTop = (DataGridViewContentAlignment)7;

		// Token: 0x04000E7E RID: 3710
		private static readonly DataGridViewContentAlignment anyRight = (DataGridViewContentAlignment)1092;

		// Token: 0x04000E7F RID: 3711
		private static readonly DataGridViewContentAlignment anyCenter = (DataGridViewContentAlignment)546;

		// Token: 0x04000E80 RID: 3712
		private DataGridView dataGridView;

		// Token: 0x04000E81 RID: 3713
		private bool valueChanged;

		// Token: 0x04000E82 RID: 3714
		private bool repositionOnValueChange;

		// Token: 0x04000E83 RID: 3715
		private int rowIndex;
	}
}
