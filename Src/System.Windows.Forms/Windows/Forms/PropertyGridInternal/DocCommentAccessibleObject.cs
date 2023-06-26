using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x020004FC RID: 1276
	[ComVisible(true)]
	internal class DocCommentAccessibleObject : Control.ControlAccessibleObject
	{
		// Token: 0x060053AE RID: 21422 RVA: 0x0015E8D4 File Offset: 0x0015CAD4
		public DocCommentAccessibleObject(DocComment owningDocComment, PropertyGrid parentPropertyGrid)
			: base(owningDocComment)
		{
			this._parentPropertyGrid = parentPropertyGrid;
		}

		// Token: 0x060053AF RID: 21423 RVA: 0x0015E8E4 File Offset: 0x0015CAE4
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

		// Token: 0x060053B0 RID: 21424 RVA: 0x0015E91A File Offset: 0x0015CB1A
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

		// Token: 0x040036C0 RID: 14016
		private PropertyGrid _parentPropertyGrid;
	}
}
