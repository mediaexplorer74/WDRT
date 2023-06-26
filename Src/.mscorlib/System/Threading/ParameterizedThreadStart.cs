using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Represents the method that executes on a <see cref="T:System.Threading.Thread" />.</summary>
	/// <param name="obj">An object that contains data for the thread procedure.</param>
	// Token: 0x0200050A RID: 1290
	// (Invoke) Token: 0x06003CDF RID: 15583
	[ComVisible(false)]
	public delegate void ParameterizedThreadStart(object obj);
}
