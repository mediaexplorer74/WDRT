using System;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x020004AA RID: 1194
	internal class GetRefreshStateEvent : GetBoolValueEvent
	{
		// Token: 0x06004F3A RID: 20282 RVA: 0x0014608E File Offset: 0x0014428E
		public GetRefreshStateEvent(Com2ShouldRefreshTypes item, bool defValue)
			: base(defValue)
		{
			this.item = item;
		}

		// Token: 0x04003440 RID: 13376
		private Com2ShouldRefreshTypes item;
	}
}
