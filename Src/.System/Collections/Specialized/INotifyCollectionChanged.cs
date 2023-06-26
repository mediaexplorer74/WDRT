using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Specialized
{
	/// <summary>Notifies listeners of dynamic changes, such as when an item is added and removed or the whole list is cleared.</summary>
	// Token: 0x020003AB RID: 939
	[TypeForwardedFrom("WindowsBase, Version=3.0.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	[global::__DynamicallyInvokable]
	public interface INotifyCollectionChanged
	{
		/// <summary>Occurs when the collection changes.</summary>
		// Token: 0x1400002A RID: 42
		// (add) Token: 0x06002304 RID: 8964
		// (remove) Token: 0x06002305 RID: 8965
		[global::__DynamicallyInvokable]
		event NotifyCollectionChangedEventHandler CollectionChanged;
	}
}
