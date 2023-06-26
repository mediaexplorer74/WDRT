using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x0200032E RID: 814
	[ComVisible(true)]
	internal class PropertyGridToolStripAccessibleObject : ToolStrip.ToolStripAccessibleObject
	{
		// Token: 0x06003521 RID: 13601 RVA: 0x000F159F File Offset: 0x000EF79F
		public PropertyGridToolStripAccessibleObject(PropertyGridToolStrip owningPropertyGridToolStrip, PropertyGrid parentPropertyGrid)
			: base(owningPropertyGridToolStrip)
		{
			this._parentPropertyGrid = parentPropertyGrid;
		}

		// Token: 0x06003522 RID: 13602 RVA: 0x000F15B0 File Offset: 0x000EF7B0
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

		// Token: 0x06003523 RID: 13603 RVA: 0x000F15E6 File Offset: 0x000EF7E6
		internal override object GetPropertyValue(int propertyID)
		{
			if (propertyID == 30003)
			{
				return 50021;
			}
			if (propertyID == 30005)
			{
				return this.Name;
			}
			return base.GetPropertyValue(propertyID);
		}

		// Token: 0x04001F37 RID: 7991
		private PropertyGrid _parentPropertyGrid;
	}
}
