using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates whether the <see cref="M:System.Runtime.InteropServices.Marshal.GetComInterfaceForObject(System.Object,System.Type,System.Runtime.InteropServices.CustomQueryInterfaceMode)" /> method's IUnknown::QueryInterface calls can use the <see cref="T:System.Runtime.InteropServices.ICustomQueryInterface" /> interface.</summary>
	// Token: 0x0200094D RID: 2381
	[__DynamicallyInvokable]
	[Serializable]
	public enum CustomQueryInterfaceMode
	{
		/// <summary>IUnknown::QueryInterface method calls should ignore the <see cref="T:System.Runtime.InteropServices.ICustomQueryInterface" /> interface.</summary>
		// Token: 0x04002B61 RID: 11105
		[__DynamicallyInvokable]
		Ignore,
		/// <summary>IUnknown::QueryInterface method calls can use the <see cref="T:System.Runtime.InteropServices.ICustomQueryInterface" /> interface. When you use this value, the <see cref="M:System.Runtime.InteropServices.Marshal.GetComInterfaceForObject(System.Object,System.Type,System.Runtime.InteropServices.CustomQueryInterfaceMode)" /> method overload functions like the <see cref="M:System.Runtime.InteropServices.Marshal.GetComInterfaceForObject(System.Object,System.Type)" /> overload.</summary>
		// Token: 0x04002B62 RID: 11106
		[__DynamicallyInvokable]
		Allow
	}
}
