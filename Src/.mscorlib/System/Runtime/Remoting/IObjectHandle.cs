using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
	/// <summary>Defines the interface for unwrapping marshal-by-value objects from indirection.</summary>
	// Token: 0x020007A9 RID: 1961
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("C460E2B4-E199-412a-8456-84DC3E4838C3")]
	[ComVisible(true)]
	public interface IObjectHandle
	{
		/// <summary>Unwraps the object.</summary>
		/// <returns>The unwrapped object.</returns>
		// Token: 0x06005524 RID: 21796
		object Unwrap();
	}
}
