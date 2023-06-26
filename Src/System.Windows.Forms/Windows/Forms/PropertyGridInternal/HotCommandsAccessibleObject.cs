using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x0200050A RID: 1290
	[ComVisible(true)]
	internal class HotCommandsAccessibleObject : Control.ControlAccessibleObject
	{
		// Token: 0x060054B4 RID: 21684 RVA: 0x00163062 File Offset: 0x00161262
		public HotCommandsAccessibleObject(HotCommands owningHotCommands, PropertyGrid parentPropertyGrid)
			: base(owningHotCommands)
		{
			this._parentPropertyGrid = parentPropertyGrid;
		}

		// Token: 0x060054B5 RID: 21685 RVA: 0x00163074 File Offset: 0x00161274
		internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
		{
			PropertyGridAccessibleObject propertyGridAccessibleObject = this._parentPropertyGrid.AccessibilityObject as PropertyGridAccessibleObject;
			if (propertyGridAccessibleObject != null)
			{
				UnsafeNativeMethods.IRawElementProviderFragment rawElementProviderFragment = propertyGridAccessibleObject.ChildFragmentNavigate(this, direction);
				if (rawElementProviderFragment != null)
				{
					return rawElementProviderFragment;
				}
			}
			return base.FragmentNavigate(direction);
		}

		// Token: 0x060054B6 RID: 21686 RVA: 0x0015E91A File Offset: 0x0015CB1A
		internal override object GetPropertyValue(int propertyID)
		{
			if (propertyID == 30003)
			{
				return 50033;
			}
			if (propertyID == 30005)
			{
				return this.Name;
			}
			return base.GetPropertyValue(propertyID);
		}

		// Token: 0x0400371A RID: 14106
		private PropertyGrid _parentPropertyGrid;
	}
}
