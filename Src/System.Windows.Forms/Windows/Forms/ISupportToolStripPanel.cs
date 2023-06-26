using System;

namespace System.Windows.Forms
{
	// Token: 0x020002A3 RID: 675
	internal interface ISupportToolStripPanel
	{
		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06002A30 RID: 10800
		// (set) Token: 0x06002A31 RID: 10801
		ToolStripPanelRow ToolStripPanelRow { get; set; }

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06002A32 RID: 10802
		ToolStripPanelCell ToolStripPanelCell { get; }

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06002A33 RID: 10803
		// (set) Token: 0x06002A34 RID: 10804
		bool Stretch { get; set; }

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06002A35 RID: 10805
		bool IsCurrentlyDragging { get; }

		// Token: 0x06002A36 RID: 10806
		void BeginDrag();

		// Token: 0x06002A37 RID: 10807
		void EndDrag();
	}
}
