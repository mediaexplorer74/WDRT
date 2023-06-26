using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Provides information that accessibility applications use to adjust the user interface of a <see cref="T:System.Windows.Forms.ToolStripDropDown" /> for users with impairments.</summary>
	// Token: 0x020003C0 RID: 960
	[ComVisible(true)]
	public class ToolStripDropDownItemAccessibleObject : ToolStripItem.ToolStripItemAccessibleObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownItemAccessibleObject" /> class with the specified <see cref="T:System.Windows.Forms.ToolStripDropDownItem" />.</summary>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ToolStripDropDownItem" /> that owns this <see cref="T:System.Windows.Forms.ToolStripDropDownItemAccessibleObject" />.</param>
		// Token: 0x06004118 RID: 16664 RVA: 0x001158B4 File Offset: 0x00113AB4
		public ToolStripDropDownItemAccessibleObject(ToolStripDropDownItem item)
			: base(item)
		{
			this.owner = item;
		}

		/// <summary>Gets the role of this accessible object.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AccessibleRole" /> values.</returns>
		// Token: 0x17000FDD RID: 4061
		// (get) Token: 0x06004119 RID: 16665 RVA: 0x001158C4 File Offset: 0x00113AC4
		public override AccessibleRole Role
		{
			get
			{
				AccessibleRole accessibleRole = base.Owner.AccessibleRole;
				if (accessibleRole != AccessibleRole.Default)
				{
					return accessibleRole;
				}
				return AccessibleRole.MenuItem;
			}
		}

		/// <summary>Performs the default action associated with this accessible object.</summary>
		// Token: 0x0600411A RID: 16666 RVA: 0x001158E8 File Offset: 0x00113AE8
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public override void DoDefaultAction()
		{
			ToolStripDropDownItem toolStripDropDownItem = base.Owner as ToolStripDropDownItem;
			if (toolStripDropDownItem != null && toolStripDropDownItem.HasDropDownItems)
			{
				toolStripDropDownItem.ShowDropDown();
				return;
			}
			base.DoDefaultAction();
		}

		// Token: 0x0600411B RID: 16667 RVA: 0x0011591C File Offset: 0x00113B1C
		internal override bool IsIAccessibleExSupported()
		{
			return (!AccessibilityImprovements.Level3 || this.owner.Parent == null || (!this.owner.Parent.IsInDesignMode && !this.owner.Parent.IsTopInDesignMode)) && ((this.owner != null && AccessibilityImprovements.Level1) || base.IsIAccessibleExSupported());
		}

		// Token: 0x0600411C RID: 16668 RVA: 0x0011597A File Offset: 0x00113B7A
		internal override bool IsPatternSupported(int patternId)
		{
			return (patternId == 10005 && this.owner.HasDropDownItems) || base.IsPatternSupported(patternId);
		}

		// Token: 0x0600411D RID: 16669 RVA: 0x0011599C File Offset: 0x00113B9C
		internal override object GetPropertyValue(int propertyID)
		{
			if (AccessibilityImprovements.Level3 && propertyID == 30022 && this.owner != null && this.owner.Owner is ToolStripDropDown)
			{
				return !((ToolStripDropDown)this.owner.Owner).Visible;
			}
			return base.GetPropertyValue(propertyID);
		}

		// Token: 0x0600411E RID: 16670 RVA: 0x00016044 File Offset: 0x00014244
		internal override void Expand()
		{
			this.DoDefaultAction();
		}

		// Token: 0x0600411F RID: 16671 RVA: 0x001159F7 File Offset: 0x00113BF7
		internal override void Collapse()
		{
			if (this.owner != null && this.owner.DropDown != null && this.owner.DropDown.Visible)
			{
				this.owner.DropDown.Close();
			}
		}

		// Token: 0x17000FDE RID: 4062
		// (get) Token: 0x06004120 RID: 16672 RVA: 0x00115A30 File Offset: 0x00113C30
		internal override UnsafeNativeMethods.ExpandCollapseState ExpandCollapseState
		{
			get
			{
				if (!this.owner.DropDown.Visible)
				{
					return UnsafeNativeMethods.ExpandCollapseState.Collapsed;
				}
				return UnsafeNativeMethods.ExpandCollapseState.Expanded;
			}
		}

		/// <summary>Retrieves the accessible child control corresponding to the specified index.</summary>
		/// <param name="index">The zero-based index of the accessible child control.</param>
		/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the accessible child control corresponding to the specified index.</returns>
		// Token: 0x06004121 RID: 16673 RVA: 0x00115A47 File Offset: 0x00113C47
		public override AccessibleObject GetChild(int index)
		{
			if (this.owner == null || !this.owner.HasDropDownItems)
			{
				return null;
			}
			return this.owner.DropDown.AccessibilityObject.GetChild(index);
		}

		/// <summary>Retrieves the number of children belonging to an accessible object.</summary>
		/// <returns>The number of children belonging to an accessible object.</returns>
		// Token: 0x06004122 RID: 16674 RVA: 0x00115A78 File Offset: 0x00113C78
		public override int GetChildCount()
		{
			if (this.owner == null || !this.owner.HasDropDownItems)
			{
				return -1;
			}
			if (AccessibilityImprovements.Level3 && this.ExpandCollapseState == UnsafeNativeMethods.ExpandCollapseState.Collapsed)
			{
				return 0;
			}
			if (this.owner.DropDown.LayoutRequired)
			{
				LayoutTransaction.DoLayout(this.owner.DropDown, this.owner.DropDown, PropertyNames.Items);
			}
			return this.owner.DropDown.AccessibilityObject.GetChildCount();
		}

		// Token: 0x06004123 RID: 16675 RVA: 0x00115AF4 File Offset: 0x00113CF4
		internal int GetChildFragmentIndex(ToolStripItem.ToolStripItemAccessibleObject child)
		{
			if (this.owner == null || this.owner.DropDownItems == null)
			{
				return -1;
			}
			for (int i = 0; i < this.owner.DropDownItems.Count; i++)
			{
				if (this.owner.DropDownItems[i].Available && child.Owner == this.owner.DropDownItems[i])
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06004124 RID: 16676 RVA: 0x00115B68 File Offset: 0x00113D68
		internal int GetChildFragmentCount()
		{
			if (this.owner == null || this.owner.DropDownItems == null)
			{
				return -1;
			}
			int num = 0;
			for (int i = 0; i < this.owner.DropDownItems.Count; i++)
			{
				if (this.owner.DropDownItems[i].Available)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06004125 RID: 16677 RVA: 0x00115BC8 File Offset: 0x00113DC8
		internal AccessibleObject GetChildFragment(int index, UnsafeNativeMethods.NavigateDirection direction = UnsafeNativeMethods.NavigateDirection.NextSibling)
		{
			ToolStrip.ToolStripAccessibleObject toolStripAccessibleObject = this.owner.DropDown.AccessibilityObject as ToolStrip.ToolStripAccessibleObject;
			if (toolStripAccessibleObject != null)
			{
				return toolStripAccessibleObject.GetChildFragment(index, false, direction);
			}
			return null;
		}

		// Token: 0x06004126 RID: 16678 RVA: 0x00115BFC File Offset: 0x00113DFC
		internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
		{
			if (this.owner == null || this.owner.DropDown == null)
			{
				return null;
			}
			switch (direction)
			{
			case UnsafeNativeMethods.NavigateDirection.NextSibling:
			case UnsafeNativeMethods.NavigateDirection.PreviousSibling:
			{
				ToolStripDropDown toolStripDropDown = this.owner.Owner as ToolStripDropDown;
				if (toolStripDropDown != null)
				{
					int num = toolStripDropDown.Items.IndexOf(this.owner);
					if (num == -1)
					{
						return null;
					}
					num += ((direction == UnsafeNativeMethods.NavigateDirection.NextSibling) ? 1 : (-1));
					if (num < 0 || num >= toolStripDropDown.Items.Count)
					{
						return null;
					}
					ToolStripItem toolStripItem = toolStripDropDown.Items[num];
					ToolStripControlHost toolStripControlHost = toolStripItem as ToolStripControlHost;
					if (toolStripControlHost != null)
					{
						return toolStripControlHost.ControlAccessibilityObject;
					}
					return toolStripItem.AccessibilityObject;
				}
				break;
			}
			case UnsafeNativeMethods.NavigateDirection.FirstChild:
			{
				int num2 = this.GetChildCount();
				if (num2 > 0)
				{
					return this.GetChildFragment(0, direction);
				}
				return null;
			}
			case UnsafeNativeMethods.NavigateDirection.LastChild:
			{
				int num2 = this.GetChildCount();
				if (num2 > 0)
				{
					return this.GetChildFragment(num2 - 1, direction);
				}
				return null;
			}
			}
			return base.FragmentNavigate(direction);
		}

		// Token: 0x040024E6 RID: 9446
		private ToolStripDropDownItem owner;
	}
}
