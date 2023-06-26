using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides an interface to expose Win32 HWND handles.</summary>
	// Token: 0x020002AE RID: 686
	[Guid("458AB8A2-A1EA-4d7b-8EBE-DEE5D3D9442C")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true)]
	public interface IWin32Window
	{
		/// <summary>Gets the handle to the window represented by the implementer.</summary>
		/// <returns>A handle to the window represented by the implementer.</returns>
		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06002A55 RID: 10837
		IntPtr Handle { get; }
	}
}
