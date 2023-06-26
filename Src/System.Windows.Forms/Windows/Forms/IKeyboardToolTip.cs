using System;
using System.Collections.Generic;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x0200028F RID: 655
	internal interface IKeyboardToolTip
	{
		// Token: 0x060029A6 RID: 10662
		bool CanShowToolTipsNow();

		// Token: 0x060029A7 RID: 10663
		Rectangle GetNativeScreenRectangle();

		// Token: 0x060029A8 RID: 10664
		IList<Rectangle> GetNeighboringToolsRectangles();

		// Token: 0x060029A9 RID: 10665
		bool IsHoveredWithMouse();

		// Token: 0x060029AA RID: 10666
		bool HasRtlModeEnabled();

		// Token: 0x060029AB RID: 10667
		bool AllowsToolTip();

		// Token: 0x060029AC RID: 10668
		IWin32Window GetOwnerWindow();

		// Token: 0x060029AD RID: 10669
		void OnHooked(ToolTip toolTip);

		// Token: 0x060029AE RID: 10670
		void OnUnhooked(ToolTip toolTip);

		// Token: 0x060029AF RID: 10671
		string GetCaptionForTool(ToolTip toolTip);

		// Token: 0x060029B0 RID: 10672
		bool ShowsOwnToolTip();

		// Token: 0x060029B1 RID: 10673
		bool IsBeingTabbedTo();

		// Token: 0x060029B2 RID: 10674
		bool AllowsChildrenToShowToolTips();
	}
}
