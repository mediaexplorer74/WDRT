using System;

namespace System.ComponentModel
{
	/// <summary>Specifies how the collection is changed.</summary>
	// Token: 0x02000524 RID: 1316
	public enum CollectionChangeAction
	{
		/// <summary>Specifies that an element was added to the collection.</summary>
		// Token: 0x0400293C RID: 10556
		Add = 1,
		/// <summary>Specifies that an element was removed from the collection.</summary>
		// Token: 0x0400293D RID: 10557
		Remove,
		/// <summary>Specifies that the entire collection has changed. This is caused by using methods that manipulate the entire collection, such as <see cref="M:System.Collections.CollectionBase.Clear" />.</summary>
		// Token: 0x0400293E RID: 10558
		Refresh
	}
}
