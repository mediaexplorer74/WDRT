using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x020004FF RID: 1279
	[ComVisible(true)]
	internal class DropDownButtonAccessibleObject : Control.ControlAccessibleObject
	{
		// Token: 0x060053C2 RID: 21442 RVA: 0x0015ECD9 File Offset: 0x0015CED9
		public DropDownButtonAccessibleObject(DropDownButton owningDropDownButton)
			: base(owningDropDownButton)
		{
			this._owningDropDownButton = owningDropDownButton;
			this._owningPropertyGrid = owningDropDownButton.Parent as PropertyGridView;
			base.UseStdAccessibleObjects(owningDropDownButton.Handle);
		}

		// Token: 0x060053C3 RID: 21443 RVA: 0x0015ED06 File Offset: 0x0015CF06
		public override void DoDefaultAction()
		{
			this._owningDropDownButton.PerformButtonClick();
		}

		// Token: 0x060053C4 RID: 21444 RVA: 0x0015ED14 File Offset: 0x0015CF14
		internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
		{
			if (AccessibilityImprovements.Level5)
			{
				if (!this._owningDropDownButton.Visible)
				{
					return null;
				}
				GridEntry selectedGridEntry = this._owningPropertyGrid.SelectedGridEntry;
				PropertyDescriptorGridEntry.PropertyDescriptorGridEntryAccessibleObject propertyDescriptorGridEntryAccessibleObject = ((selectedGridEntry != null) ? selectedGridEntry.AccessibilityObject : null) as PropertyDescriptorGridEntry.PropertyDescriptorGridEntryAccessibleObject;
				if (propertyDescriptorGridEntryAccessibleObject == null)
				{
					return null;
				}
				switch (direction)
				{
				case UnsafeNativeMethods.NavigateDirection.Parent:
					return propertyDescriptorGridEntryAccessibleObject;
				case UnsafeNativeMethods.NavigateDirection.NextSibling:
					return propertyDescriptorGridEntryAccessibleObject.GetNextChildFragment(this);
				case UnsafeNativeMethods.NavigateDirection.PreviousSibling:
					return propertyDescriptorGridEntryAccessibleObject.GetPreviousChildFragment(this);
				default:
					return base.FragmentNavigate(direction);
				}
			}
			else if (direction == UnsafeNativeMethods.NavigateDirection.Parent && this._owningPropertyGrid.SelectedGridEntry != null && this._owningDropDownButton.Visible)
			{
				GridEntry selectedGridEntry2 = this._owningPropertyGrid.SelectedGridEntry;
				if (selectedGridEntry2 == null)
				{
					return null;
				}
				return selectedGridEntry2.AccessibilityObject;
			}
			else
			{
				if (direction == UnsafeNativeMethods.NavigateDirection.PreviousSibling)
				{
					return this._owningPropertyGrid.EditAccessibleObject;
				}
				return base.FragmentNavigate(direction);
			}
		}

		// Token: 0x17001403 RID: 5123
		// (get) Token: 0x060053C5 RID: 21445 RVA: 0x0015EDD2 File Offset: 0x0015CFD2
		internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
		{
			get
			{
				return this._owningPropertyGrid.AccessibilityObject;
			}
		}

		// Token: 0x060053C6 RID: 21446 RVA: 0x0015EDE0 File Offset: 0x0015CFE0
		internal override object GetPropertyValue(int propertyID)
		{
			if (propertyID <= 30005)
			{
				if (propertyID == 30003)
				{
					return 50000;
				}
				if (propertyID == 30005)
				{
					return this.Name;
				}
			}
			else
			{
				if (propertyID == 30090)
				{
					return true;
				}
				if (propertyID == 30095)
				{
					return this.Role;
				}
			}
			return base.GetPropertyValue(propertyID);
		}

		// Token: 0x060053C7 RID: 21447 RVA: 0x000F156F File Offset: 0x000EF76F
		internal override bool IsPatternSupported(int patternId)
		{
			return patternId == 10018 || base.IsPatternSupported(patternId);
		}

		// Token: 0x17001404 RID: 5124
		// (get) Token: 0x060053C8 RID: 21448 RVA: 0x0015EE45 File Offset: 0x0015D045
		public override AccessibleRole Role
		{
			get
			{
				return AccessibleRole.PushButton;
			}
		}

		// Token: 0x060053C9 RID: 21449 RVA: 0x0015EE49 File Offset: 0x0015D049
		internal override void SetFocus()
		{
			base.RaiseAutomationEvent(20005);
			base.SetFocus();
		}

		// Token: 0x040036C3 RID: 14019
		private DropDownButton _owningDropDownButton;

		// Token: 0x040036C4 RID: 14020
		private PropertyGridView _owningPropertyGrid;
	}
}
