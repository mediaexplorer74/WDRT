using System;

namespace System.ComponentModel
{
	/// <summary>Defines the mechanism for querying the object for changes and resetting of the changed status.</summary>
	// Token: 0x0200055A RID: 1370
	[global::__DynamicallyInvokable]
	public interface IChangeTracking
	{
		/// <summary>Gets the object's changed status.</summary>
		/// <returns>
		///   <see langword="true" /> if the object's content has changed since the last call to <see cref="M:System.ComponentModel.IChangeTracking.AcceptChanges" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x0600336A RID: 13162
		[global::__DynamicallyInvokable]
		bool IsChanged
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Resets the object's state to unchanged by accepting the modifications.</summary>
		// Token: 0x0600336B RID: 13163
		[global::__DynamicallyInvokable]
		void AcceptChanges();
	}
}
