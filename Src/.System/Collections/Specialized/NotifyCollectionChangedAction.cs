using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Specialized
{
	/// <summary>Describes the action that caused a <see cref="E:System.Collections.Specialized.INotifyCollectionChanged.CollectionChanged" /> event.</summary>
	// Token: 0x020003B1 RID: 945
	[TypeForwardedFrom("WindowsBase, Version=3.0.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	[global::__DynamicallyInvokable]
	public enum NotifyCollectionChangedAction
	{
		/// <summary>An item was added to the collection.</summary>
		// Token: 0x04001FC5 RID: 8133
		[global::__DynamicallyInvokable]
		Add,
		/// <summary>An item was removed from the collection.</summary>
		// Token: 0x04001FC6 RID: 8134
		[global::__DynamicallyInvokable]
		Remove,
		/// <summary>An item was replaced in the collection.</summary>
		// Token: 0x04001FC7 RID: 8135
		[global::__DynamicallyInvokable]
		Replace,
		/// <summary>An item was moved within the collection.</summary>
		// Token: 0x04001FC8 RID: 8136
		[global::__DynamicallyInvokable]
		Move,
		/// <summary>The content of the collection was cleared.</summary>
		// Token: 0x04001FC9 RID: 8137
		[global::__DynamicallyInvokable]
		Reset
	}
}
