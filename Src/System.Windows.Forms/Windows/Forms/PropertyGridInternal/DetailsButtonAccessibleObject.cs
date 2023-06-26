using System;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x02000507 RID: 1287
	internal class DetailsButtonAccessibleObject : Control.ControlAccessibleObject
	{
		// Token: 0x0600548F RID: 21647 RVA: 0x001625FA File Offset: 0x001607FA
		public DetailsButtonAccessibleObject(DetailsButton owner)
			: base(owner)
		{
			this.ownerItem = owner;
		}

		// Token: 0x06005490 RID: 21648 RVA: 0x00012E4E File Offset: 0x0001104E
		internal override bool IsIAccessibleExSupported()
		{
			return true;
		}

		// Token: 0x06005491 RID: 21649 RVA: 0x0016260A File Offset: 0x0016080A
		internal override object GetPropertyValue(int propertyID)
		{
			if (propertyID == 30003)
			{
				return 50000;
			}
			return base.GetPropertyValue(propertyID);
		}

		// Token: 0x06005492 RID: 21650 RVA: 0x00162626 File Offset: 0x00160826
		internal override bool IsPatternSupported(int patternId)
		{
			return patternId == 10005 || base.IsPatternSupported(patternId);
		}

		// Token: 0x17001447 RID: 5191
		// (get) Token: 0x06005493 RID: 21651 RVA: 0x00162639 File Offset: 0x00160839
		internal override UnsafeNativeMethods.ExpandCollapseState ExpandCollapseState
		{
			get
			{
				if (!this.ownerItem.Expanded)
				{
					return UnsafeNativeMethods.ExpandCollapseState.Collapsed;
				}
				return UnsafeNativeMethods.ExpandCollapseState.Expanded;
			}
		}

		// Token: 0x06005494 RID: 21652 RVA: 0x0016264B File Offset: 0x0016084B
		internal override void Expand()
		{
			if (this.ownerItem != null && !this.ownerItem.Expanded)
			{
				this.DoDefaultAction();
			}
		}

		// Token: 0x06005495 RID: 21653 RVA: 0x00162668 File Offset: 0x00160868
		internal override void Collapse()
		{
			if (this.ownerItem != null && this.ownerItem.Expanded)
			{
				this.DoDefaultAction();
			}
		}

		// Token: 0x0400370D RID: 14093
		private DetailsButton ownerItem;
	}
}
