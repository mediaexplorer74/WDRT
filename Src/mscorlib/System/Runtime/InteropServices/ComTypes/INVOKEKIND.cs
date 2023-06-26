using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Specifies how to invoke a function by <see langword="IDispatch::Invoke" />.</summary>
	// Token: 0x02000A48 RID: 2632
	[Flags]
	[__DynamicallyInvokable]
	[Serializable]
	public enum INVOKEKIND
	{
		/// <summary>The member is called using a normal function invocation syntax.</summary>
		// Token: 0x04002DDE RID: 11742
		[__DynamicallyInvokable]
		INVOKE_FUNC = 1,
		/// <summary>The function is invoked using a normal property access syntax.</summary>
		// Token: 0x04002DDF RID: 11743
		[__DynamicallyInvokable]
		INVOKE_PROPERTYGET = 2,
		/// <summary>The function is invoked using a property value assignment syntax.</summary>
		// Token: 0x04002DE0 RID: 11744
		[__DynamicallyInvokable]
		INVOKE_PROPERTYPUT = 4,
		/// <summary>The function is invoked using a property reference assignment syntax.</summary>
		// Token: 0x04002DE1 RID: 11745
		[__DynamicallyInvokable]
		INVOKE_PROPERTYPUTREF = 8
	}
}
