using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>References a method to be called when a corresponding asynchronous operation completes.</summary>
	/// <param name="ar">The result of the asynchronous operation.</param>
	// Token: 0x020000AC RID: 172
	// (Invoke) Token: 0x060009C8 RID: 2504
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public delegate void AsyncCallback(IAsyncResult ar);
}
