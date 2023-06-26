using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Provides return values for the <see cref="M:System.Runtime.InteropServices.ICustomQueryInterface.GetInterface(System.Guid@,System.IntPtr@)" /> method.</summary>
	// Token: 0x02000964 RID: 2404
	[ComVisible(false)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum CustomQueryInterfaceResult
	{
		/// <summary>The interface pointer that is returned from the <see cref="M:System.Runtime.InteropServices.ICustomQueryInterface.GetInterface(System.Guid@,System.IntPtr@)" /> method can be used as the result of IUnknown::QueryInterface.</summary>
		// Token: 0x04002B9E RID: 11166
		[__DynamicallyInvokable]
		Handled,
		/// <summary>The custom <see langword="QueryInterface" /> was not used. Instead, the default implementation of IUnknown::QueryInterface should be used.</summary>
		// Token: 0x04002B9F RID: 11167
		[__DynamicallyInvokable]
		NotHandled,
		/// <summary>The interface for a specific interface ID is not available. In this case, the returned interface is <see langword="null" />. E_NOINTERFACE is returned to the caller of IUnknown::QueryInterface.</summary>
		// Token: 0x04002BA0 RID: 11168
		[__DynamicallyInvokable]
		Failed
	}
}
