using System;

namespace System.ComponentModel
{
	/// <summary>Indicates whether a class converts property change events to <see cref="E:System.ComponentModel.IBindingList.ListChanged" /> events.</summary>
	// Token: 0x02000573 RID: 1395
	public interface IRaiseItemChangedEvents
	{
		/// <summary>Gets a value indicating whether the <see cref="T:System.ComponentModel.IRaiseItemChangedEvents" /> object raises <see cref="E:System.ComponentModel.IBindingList.ListChanged" /> events.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.ComponentModel.IRaiseItemChangedEvents" /> object raises <see cref="E:System.ComponentModel.IBindingList.ListChanged" /> events when one of its property values changes; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x060033CD RID: 13261
		bool RaisesItemChangedEvents { get; }
	}
}
