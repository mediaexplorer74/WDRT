using System;

namespace System.Threading
{
	/// <summary>Represents a method to be called when a message is to be dispatched to a synchronization context.</summary>
	/// <param name="state">The object passed to the delegate.</param>
	// Token: 0x020004E9 RID: 1257
	// (Invoke) Token: 0x06003BA4 RID: 15268
	[__DynamicallyInvokable]
	public delegate void SendOrPostCallback(object state);
}
