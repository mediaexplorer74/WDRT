using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x020001D0 RID: 464
	[ComVisible(true)]
	internal class DataGridViewEditingControlAccessibleObject : Control.ControlAccessibleObject
	{
		// Token: 0x0600206E RID: 8302 RVA: 0x0009B733 File Offset: 0x00099933
		public DataGridViewEditingControlAccessibleObject(Control ownerControl)
			: base(ownerControl)
		{
		}

		// Token: 0x0600206F RID: 8303 RVA: 0x0009B73C File Offset: 0x0009993C
		internal override bool IsIAccessibleExSupported()
		{
			return AccessibilityImprovements.Level3 || base.IsIAccessibleExSupported();
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06002070 RID: 8304 RVA: 0x0009B74D File Offset: 0x0009994D
		public override AccessibleObject Parent
		{
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				IDataGridViewEditingControl dataGridViewEditingControl = base.Owner as IDataGridViewEditingControl;
				if (dataGridViewEditingControl == null)
				{
					return null;
				}
				DataGridView editingControlDataGridView = dataGridViewEditingControl.EditingControlDataGridView;
				if (editingControlDataGridView == null)
				{
					return null;
				}
				DataGridViewCell currentCell = editingControlDataGridView.CurrentCell;
				if (currentCell == null)
				{
					return null;
				}
				return currentCell.AccessibilityObject;
			}
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x0009B77C File Offset: 0x0009997C
		internal override bool IsPatternSupported(int patternId)
		{
			if (AccessibilityImprovements.Level3 && patternId == 10005)
			{
				ComboBox comboBox = base.Owner as ComboBox;
				if (comboBox != null)
				{
					return comboBox.DropDownStyle > ComboBoxStyle.Simple;
				}
			}
			return base.IsPatternSupported(patternId);
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x0009B7B8 File Offset: 0x000999B8
		internal override object GetPropertyValue(int propertyID)
		{
			if (AccessibilityImprovements.Level3 && propertyID == 30028)
			{
				return this.IsPatternSupported(10005);
			}
			return base.GetPropertyValue(propertyID);
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06002073 RID: 8307 RVA: 0x0009B7E4 File Offset: 0x000999E4
		internal override UnsafeNativeMethods.ExpandCollapseState ExpandCollapseState
		{
			get
			{
				ComboBox comboBox = base.Owner as ComboBox;
				if (comboBox == null)
				{
					return base.ExpandCollapseState;
				}
				if (!comboBox.DroppedDown)
				{
					return UnsafeNativeMethods.ExpandCollapseState.Collapsed;
				}
				return UnsafeNativeMethods.ExpandCollapseState.Expanded;
			}
		}
	}
}
