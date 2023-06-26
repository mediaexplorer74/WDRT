using System;

namespace System.Windows.Forms
{
	// Token: 0x020002A1 RID: 673
	internal interface ISupportOleDropSource
	{
		// Token: 0x06002A2A RID: 10794
		void OnQueryContinueDrag(QueryContinueDragEventArgs qcdevent);

		// Token: 0x06002A2B RID: 10795
		void OnGiveFeedback(GiveFeedbackEventArgs gfbevent);
	}
}
