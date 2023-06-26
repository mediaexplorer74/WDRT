using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates which <see langword="IDispatch" /> implementation to use for a particular class.</summary>
	// Token: 0x0200091D RID: 2333
	[Obsolete("The IDispatchImplAttribute is deprecated.", false)]
	[ComVisible(true)]
	[Serializable]
	public enum IDispatchImplType
	{
		/// <summary>Specifies that the common language runtime decides which <see langword="IDispatch" /> implementation to use.</summary>
		// Token: 0x04002A7F RID: 10879
		SystemDefinedImpl,
		/// <summary>Specifies that the <see langword="IDispatch" /> implementation is supplied by the runtime.</summary>
		// Token: 0x04002A80 RID: 10880
		InternalImpl,
		/// <summary>Specifies that the <see langword="IDispatch" /> implementation is supplied by passing the type information for the object to the COM <see langword="CreateStdDispatch" /> API method.</summary>
		// Token: 0x04002A81 RID: 10881
		CompatibleImpl
	}
}
