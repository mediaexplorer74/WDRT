using System;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x020001CA RID: 458
	internal class DataGridViewComboBoxEditingControlAccessibleObject : ComboBox.ComboBoxUiaProvider
	{
		// Token: 0x0600204D RID: 8269 RVA: 0x0009B629 File Offset: 0x00099829
		public DataGridViewComboBoxEditingControlAccessibleObject(DataGridViewComboBoxEditingControl ownerControl)
			: base(ownerControl)
		{
			this.ownerControl = ownerControl;
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x0600204E RID: 8270 RVA: 0x0009B639 File Offset: 0x00099839
		public override AccessibleObject Parent
		{
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				return this._parentAccessibleObject;
			}
		}

		// Token: 0x0600204F RID: 8271 RVA: 0x0009B644 File Offset: 0x00099844
		internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
		{
			if (direction != UnsafeNativeMethods.NavigateDirection.Parent)
			{
				return base.FragmentNavigate(direction);
			}
			IDataGridViewEditingControl dataGridViewEditingControl = base.Owner as IDataGridViewEditingControl;
			if (dataGridViewEditingControl != null && dataGridViewEditingControl.EditingControlDataGridView.EditingControl == dataGridViewEditingControl)
			{
				return this._parentAccessibleObject;
			}
			return null;
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06002050 RID: 8272 RVA: 0x00010EA9 File Offset: 0x0000F0A9
		internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
		{
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
				return editingControlDataGridView.AccessibilityObject;
			}
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x0009B681 File Offset: 0x00099881
		internal override bool IsPatternSupported(int patternId)
		{
			if (patternId == 10005)
			{
				return this.ownerControl.DropDownStyle > ComboBoxStyle.Simple;
			}
			return base.IsPatternSupported(patternId);
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x0009B6A1 File Offset: 0x000998A1
		internal override object GetPropertyValue(int propertyID)
		{
			if (propertyID == 30028)
			{
				return this.IsPatternSupported(10005);
			}
			return base.GetPropertyValue(propertyID);
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06002053 RID: 8275 RVA: 0x0009B6C3 File Offset: 0x000998C3
		internal override UnsafeNativeMethods.ExpandCollapseState ExpandCollapseState
		{
			get
			{
				if (!this.ownerControl.DroppedDown)
				{
					return UnsafeNativeMethods.ExpandCollapseState.Collapsed;
				}
				return UnsafeNativeMethods.ExpandCollapseState.Expanded;
			}
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x0009B6D5 File Offset: 0x000998D5
		internal override void SetParent(AccessibleObject parent)
		{
			this._parentAccessibleObject = parent;
		}

		// Token: 0x04000D8A RID: 3466
		private DataGridViewComboBoxEditingControl ownerControl;

		// Token: 0x04000D8B RID: 3467
		private AccessibleObject _parentAccessibleObject;
	}
}
