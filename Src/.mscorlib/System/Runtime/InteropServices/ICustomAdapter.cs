using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Provides a way for clients to access the actual object, rather than the adapter object handed out by a custom marshaler.</summary>
	// Token: 0x02000962 RID: 2402
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public interface ICustomAdapter
	{
		/// <summary>Provides access to the underlying object wrapped by a custom marshaler.</summary>
		/// <returns>The object contained by the adapter object.</returns>
		// Token: 0x0600623D RID: 25149
		[__DynamicallyInvokable]
		[return: MarshalAs(UnmanagedType.IUnknown)]
		object GetUnderlyingObject();
	}
}
