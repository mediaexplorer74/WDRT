using System;

namespace System.ComponentModel
{
	/// <summary>Provides support for rolling back the changes</summary>
	// Token: 0x02000574 RID: 1396
	[global::__DynamicallyInvokable]
	public interface IRevertibleChangeTracking : IChangeTracking
	{
		/// <summary>Resets the object's state to unchanged by rejecting the modifications.</summary>
		// Token: 0x060033CE RID: 13262
		[global::__DynamicallyInvokable]
		void RejectChanges();
	}
}
