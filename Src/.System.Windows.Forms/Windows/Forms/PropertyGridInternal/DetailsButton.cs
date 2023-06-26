using System;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x02000506 RID: 1286
	internal class DetailsButton : Button
	{
		// Token: 0x0600548C RID: 21644 RVA: 0x001625C8 File Offset: 0x001607C8
		public DetailsButton(GridErrorDlg form)
		{
			this.parent = form;
		}

		// Token: 0x17001446 RID: 5190
		// (get) Token: 0x0600548D RID: 21645 RVA: 0x001625D7 File Offset: 0x001607D7
		public bool Expanded
		{
			get
			{
				return this.parent.DetailsButtonExpanded;
			}
		}

		// Token: 0x0600548E RID: 21646 RVA: 0x001625E4 File Offset: 0x001607E4
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level1)
			{
				return new DetailsButtonAccessibleObject(this);
			}
			return base.CreateAccessibilityInstance();
		}

		// Token: 0x0400370C RID: 14092
		private GridErrorDlg parent;
	}
}
