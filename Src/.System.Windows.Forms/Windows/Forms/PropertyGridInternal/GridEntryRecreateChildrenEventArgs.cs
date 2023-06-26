using System;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x02000503 RID: 1283
	internal class GridEntryRecreateChildrenEventArgs : EventArgs
	{
		// Token: 0x06005479 RID: 21625 RVA: 0x00161850 File Offset: 0x0015FA50
		public GridEntryRecreateChildrenEventArgs(int oldCount, int newCount)
		{
			this.OldChildCount = oldCount;
			this.NewChildCount = newCount;
		}

		// Token: 0x040036FC RID: 14076
		public readonly int OldChildCount;

		// Token: 0x040036FD RID: 14077
		public readonly int NewChildCount;
	}
}
