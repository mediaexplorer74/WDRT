using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Represents the method that handles calls from a <see cref="T:System.Threading.Timer" />.</summary>
	/// <param name="state">An object containing application-specific information relevant to the method invoked by this delegate, or <see langword="null" />.</param>
	// Token: 0x0200052B RID: 1323
	// (Invoke) Token: 0x06003E49 RID: 15945
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public delegate void TimerCallback(object state);
}
