using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Represents a callback method to be executed by a thread pool thread.</summary>
	/// <param name="state">An object containing information to be used by the callback method.</param>
	// Token: 0x0200051D RID: 1309
	// (Invoke) Token: 0x06003DF3 RID: 15859
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public delegate void WaitCallback(object state);
}
