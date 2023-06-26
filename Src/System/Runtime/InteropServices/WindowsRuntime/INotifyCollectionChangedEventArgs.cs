using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020003E8 RID: 1000
	[Guid("4cf68d33-e3f2-4964-b85e-945b4f7e2f21")]
	[ComImport]
	internal interface INotifyCollectionChangedEventArgs
	{
		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x0600260C RID: 9740
		NotifyCollectionChangedAction Action { get; }

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x0600260D RID: 9741
		IList NewItems { get; }

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x0600260E RID: 9742
		IList OldItems { get; }

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x0600260F RID: 9743
		int NewStartingIndex { get; }

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06002610 RID: 9744
		int OldStartingIndex { get; }
	}
}
