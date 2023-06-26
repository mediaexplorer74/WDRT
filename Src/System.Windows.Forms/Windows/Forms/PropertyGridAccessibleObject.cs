using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x0200032C RID: 812
	[ComVisible(true)]
	internal class PropertyGridAccessibleObject : Control.ControlAccessibleObject
	{
		// Token: 0x06003513 RID: 13587 RVA: 0x000F130B File Offset: 0x000EF50B
		public PropertyGridAccessibleObject(PropertyGrid owningPropertyGrid)
			: base(owningPropertyGrid)
		{
			this._owningPropertyGrid = owningPropertyGrid;
		}

		// Token: 0x06003514 RID: 13588 RVA: 0x000F131C File Offset: 0x000EF51C
		internal override UnsafeNativeMethods.IRawElementProviderFragment ElementProviderFromPoint(double x, double y)
		{
			Point point = this._owningPropertyGrid.PointToClient(new Point((int)x, (int)y));
			Control elementFromPoint = this._owningPropertyGrid.GetElementFromPoint(point);
			if (elementFromPoint != null)
			{
				return elementFromPoint.AccessibilityObject;
			}
			return base.ElementProviderFromPoint(x, y);
		}

		// Token: 0x06003515 RID: 13589 RVA: 0x000F1360 File Offset: 0x000EF560
		internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
		{
			if (direction != UnsafeNativeMethods.NavigateDirection.FirstChild)
			{
				if (direction == UnsafeNativeMethods.NavigateDirection.LastChild)
				{
					int childFragmentCount = this.GetChildFragmentCount();
					if (childFragmentCount > 0)
					{
						return this.GetChildFragment(childFragmentCount - 1);
					}
				}
				return base.FragmentNavigate(direction);
			}
			return this.GetChildFragment(0);
		}

		// Token: 0x06003516 RID: 13590 RVA: 0x000F139C File Offset: 0x000EF59C
		internal UnsafeNativeMethods.IRawElementProviderFragment ChildFragmentNavigate(AccessibleObject childFragment, UnsafeNativeMethods.NavigateDirection direction)
		{
			switch (direction)
			{
			case UnsafeNativeMethods.NavigateDirection.Parent:
				return this;
			case UnsafeNativeMethods.NavigateDirection.NextSibling:
			{
				int num = this.GetChildFragmentCount();
				int num2 = this.GetChildFragmentIndex(childFragment);
				int num3 = num2 + 1;
				if (num > num3)
				{
					return this.GetChildFragment(num3);
				}
				return null;
			}
			case UnsafeNativeMethods.NavigateDirection.PreviousSibling:
			{
				int num = this.GetChildFragmentCount();
				int num2 = this.GetChildFragmentIndex(childFragment);
				if (num2 > 0)
				{
					return this.GetChildFragment(num2 - 1);
				}
				return null;
			}
			default:
				return null;
			}
		}

		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x06003517 RID: 13591 RVA: 0x000F1400 File Offset: 0x000EF600
		internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
		{
			get
			{
				ToolStripControlHost toolStripControlHost = base.Owner.ToolStripControlHost;
				ToolStrip toolStrip = ((toolStripControlHost != null) ? toolStripControlHost.Owner : null);
				if (toolStrip != null && toolStrip.IsHandleCreated)
				{
					return toolStrip.AccessibilityObject;
				}
				return this;
			}
		}

		// Token: 0x06003518 RID: 13592 RVA: 0x000F1438 File Offset: 0x000EF638
		internal AccessibleObject GetChildFragment(int index)
		{
			if (index < 0)
			{
				return null;
			}
			if (this._owningPropertyGrid.ToolbarVisible)
			{
				if (index == 0)
				{
					return this._owningPropertyGrid.ToolbarAccessibleObject;
				}
				index--;
			}
			if (this._owningPropertyGrid.GridViewVisible)
			{
				if (index == 0)
				{
					return this._owningPropertyGrid.GridViewAccessibleObject;
				}
				index--;
			}
			if (this._owningPropertyGrid.CommandsVisible)
			{
				if (index == 0)
				{
					return this._owningPropertyGrid.HotCommandsAccessibleObject;
				}
				index--;
			}
			if (this._owningPropertyGrid.HelpVisible && index == 0)
			{
				return this._owningPropertyGrid.HelpAccessibleObject;
			}
			return null;
		}

		// Token: 0x06003519 RID: 13593 RVA: 0x000F14CC File Offset: 0x000EF6CC
		internal int GetChildFragmentCount()
		{
			int num = 0;
			if (this._owningPropertyGrid.ToolbarVisible)
			{
				num++;
			}
			if (this._owningPropertyGrid.GridViewVisible)
			{
				num++;
			}
			if (this._owningPropertyGrid.CommandsVisible)
			{
				num++;
			}
			if (this._owningPropertyGrid.HelpVisible)
			{
				num++;
			}
			return num;
		}

		// Token: 0x0600351A RID: 13594 RVA: 0x000F1520 File Offset: 0x000EF720
		internal override UnsafeNativeMethods.IRawElementProviderFragment GetFocus()
		{
			return this.GetFocused();
		}

		// Token: 0x0600351B RID: 13595 RVA: 0x000F1528 File Offset: 0x000EF728
		internal int GetChildFragmentIndex(AccessibleObject controlAccessibleObject)
		{
			int childFragmentCount = this.GetChildFragmentCount();
			for (int i = 0; i < childFragmentCount; i++)
			{
				AccessibleObject childFragment = this.GetChildFragment(i);
				if (childFragment == controlAccessibleObject)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600351C RID: 13596 RVA: 0x000F1557 File Offset: 0x000EF757
		internal override object GetPropertyValue(int propertyID)
		{
			if (propertyID == 30005)
			{
				return this.Name;
			}
			return base.GetPropertyValue(propertyID);
		}

		// Token: 0x0600351D RID: 13597 RVA: 0x000F156F File Offset: 0x000EF76F
		internal override bool IsPatternSupported(int patternId)
		{
			return patternId == 10018 || base.IsPatternSupported(patternId);
		}

		// Token: 0x04001F35 RID: 7989
		private PropertyGrid _owningPropertyGrid;
	}
}
