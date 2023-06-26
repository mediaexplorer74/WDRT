using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Provides a mechanism for releasing unmanaged resources.</summary>
	// Token: 0x020000EE RID: 238
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public interface IDisposable
	{
		/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
		// Token: 0x06000EFD RID: 3837
		[__DynamicallyInvokable]
		void Dispose();
	}
}
