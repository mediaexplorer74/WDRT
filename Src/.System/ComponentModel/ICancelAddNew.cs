using System;

namespace System.ComponentModel
{
	/// <summary>Adds transactional capability when adding a new item to a collection.</summary>
	// Token: 0x02000559 RID: 1369
	public interface ICancelAddNew
	{
		/// <summary>Discards a pending new item from the collection.</summary>
		/// <param name="itemIndex">The index of the item that was previously added to the collection.</param>
		// Token: 0x06003368 RID: 13160
		void CancelNew(int itemIndex);

		/// <summary>Commits a pending new item to the collection.</summary>
		/// <param name="itemIndex">The index of the item that was previously added to the collection.</param>
		// Token: 0x06003369 RID: 13161
		void EndNew(int itemIndex);
	}
}
