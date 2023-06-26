using System;

namespace System.Windows.Forms
{
	// Token: 0x02000108 RID: 264
	internal class DataGridViewTextBoxEditingControlAccessibleObjectLevel5 : TextBoxBase.TextBoxBaseAccessibleObject
	{
		// Token: 0x06000472 RID: 1138 RVA: 0x00010E0C File Offset: 0x0000F00C
		public DataGridViewTextBoxEditingControlAccessibleObjectLevel5(DataGridViewTextBoxEditingControl ownerControl)
			: base(ownerControl)
		{
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x00010E15 File Offset: 0x0000F015
		public override AccessibleObject Parent
		{
			get
			{
				return this._parentAccessibleObject;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x00010E20 File Offset: 0x0000F020
		// (set) Token: 0x06000475 RID: 1141 RVA: 0x00010E62 File Offset: 0x0000F062
		public override string Name
		{
			get
			{
				if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5 && base.Owner == null)
				{
					return SR.GetString("DataGridView_AccEditingControlAccName");
				}
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

		// Token: 0x06000476 RID: 1142 RVA: 0x00010E6C File Offset: 0x0000F06C
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

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x00010EA9 File Offset: 0x0000F0A9
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

		// Token: 0x06000478 RID: 1144 RVA: 0x00010ECC File Offset: 0x0000F0CC
		internal override bool IsPatternSupported(int patternId)
		{
			return patternId == 10002 || base.IsPatternSupported(patternId);
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00010EDF File Offset: 0x0000F0DF
		internal override void SetParent(AccessibleObject parent)
		{
			this._parentAccessibleObject = parent;
		}

		// Token: 0x0400049C RID: 1180
		private AccessibleObject _parentAccessibleObject;
	}
}
