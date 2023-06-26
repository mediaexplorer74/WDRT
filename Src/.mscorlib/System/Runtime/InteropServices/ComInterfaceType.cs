using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Identifies how to expose an interface to COM.</summary>
	// Token: 0x02000911 RID: 2321
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum ComInterfaceType
	{
		/// <summary>Indicates that the interface is exposed to COM as a dual interface, which enables both early and late binding. <see cref="F:System.Runtime.InteropServices.ComInterfaceType.InterfaceIsDual" /> is the default value.</summary>
		// Token: 0x04002A6E RID: 10862
		[__DynamicallyInvokable]
		InterfaceIsDual,
		/// <summary>Indicates that an interface is exposed to COM as an interface that is derived from IUnknown, which enables only early binding.</summary>
		// Token: 0x04002A6F RID: 10863
		[__DynamicallyInvokable]
		InterfaceIsIUnknown,
		/// <summary>Indicates that an interface is exposed to COM as a dispinterface, which enables late binding only.</summary>
		// Token: 0x04002A70 RID: 10864
		[__DynamicallyInvokable]
		InterfaceIsIDispatch,
		/// <summary>Indicates that an interface is exposed to COM as a Windows Runtime interface.</summary>
		// Token: 0x04002A71 RID: 10865
		[ComVisible(false)]
		[__DynamicallyInvokable]
		InterfaceIsIInspectable
	}
}
