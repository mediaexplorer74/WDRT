using System;

namespace System.ComponentModel.Design
{
	/// <summary>Specifies methods for the designer host to report on the state of transactions.</summary>
	// Token: 0x020005E9 RID: 1513
	public interface IDesignerHostTransactionState
	{
		/// <summary>Gets a value indicating whether the designer host is closing a transaction.</summary>
		/// <returns>
		///   <see langword="true" /> if the designer is closing a transaction; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x0600380A RID: 14346
		bool IsClosingTransaction { get; }
	}
}
