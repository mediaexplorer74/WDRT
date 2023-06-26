using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Creates a COM object.</summary>
	/// <param name="aggregator">A pointer to the managed object's <see langword="IUnknown" /> interface.</param>
	/// <returns>An <see cref="T:System.IntPtr" /> object that represents the <see langword="IUnknown" /> interface of the COM object.</returns>
	// Token: 0x02000971 RID: 2417
	// (Invoke) Token: 0x06006256 RID: 25174
	[ComVisible(true)]
	public delegate IntPtr ObjectCreationDelegate(IntPtr aggregator);
}
