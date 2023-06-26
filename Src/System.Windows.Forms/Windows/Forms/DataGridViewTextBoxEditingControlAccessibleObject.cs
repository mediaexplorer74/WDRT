using System;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x0200021E RID: 542
	internal class DataGridViewTextBoxEditingControlAccessibleObject : Control.ControlAccessibleObject
	{
		// Token: 0x06002360 RID: 9056 RVA: 0x000A8751 File Offset: 0x000A6951
		public DataGridViewTextBoxEditingControlAccessibleObject(DataGridViewTextBoxEditingControl ownerControl)
			: base(ownerControl)
		{
			this.ownerControl = ownerControl;
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06002361 RID: 9057 RVA: 0x000A8761 File Offset: 0x000A6961
		public override AccessibleObject Parent
		{
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				return this._parentAccessibleObject;
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06002362 RID: 9058 RVA: 0x000A876C File Offset: 0x000A696C
		// (set) Token: 0x06002363 RID: 9059 RVA: 0x00010E62 File Offset: 0x0000F062
		public override string Name
		{
			get
			{
				string accessibleName = base.Owner.AccessibleName;
				if (accessibleName != null)
				{
					return accessibleName;
				}
				return SR.GetString("DataGridView_AccEditingControlAccName");
			}
			set
			{
				base.Name = value;
			}
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x000A8794 File Offset: 0x000A6994
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

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06002365 RID: 9061 RVA: 0x00010EA9 File Offset: 0x0000F0A9
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

		// Token: 0x06002366 RID: 9062 RVA: 0x000A87D1 File Offset: 0x000A69D1
		internal override object GetPropertyValue(int propertyID)
		{
			if (propertyID == 30003)
			{
				return 50004;
			}
			if (propertyID == 30005)
			{
				return this.Name;
			}
			if (propertyID != 30043)
			{
				return base.GetPropertyValue(propertyID);
			}
			return true;
		}

		// Token: 0x06002367 RID: 9063 RVA: 0x000A880D File Offset: 0x000A6A0D
		internal override bool IsPatternSupported(int patternId)
		{
			return patternId == 10002 || base.IsPatternSupported(patternId);
		}

		// Token: 0x06002368 RID: 9064 RVA: 0x000A8820 File Offset: 0x000A6A20
		internal override void SetParent(AccessibleObject parent)
		{
			this._parentAccessibleObject = parent;
		}

		// Token: 0x04000E84 RID: 3716
		private DataGridViewTextBoxEditingControl ownerControl;

		// Token: 0x04000E85 RID: 3717
		private AccessibleObject _parentAccessibleObject;
	}
}
