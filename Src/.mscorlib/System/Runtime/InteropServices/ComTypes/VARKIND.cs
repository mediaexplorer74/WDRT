using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Defines the kind of variable.</summary>
	// Token: 0x02000A43 RID: 2627
	[__DynamicallyInvokable]
	[Serializable]
	public enum VARKIND
	{
		/// <summary>The variable is a field or member of the type. It exists at a fixed offset within each instance of the type.</summary>
		// Token: 0x04002DC0 RID: 11712
		[__DynamicallyInvokable]
		VAR_PERINSTANCE,
		/// <summary>There is only one instance of the variable.</summary>
		// Token: 0x04002DC1 RID: 11713
		[__DynamicallyInvokable]
		VAR_STATIC,
		/// <summary>The <see langword="VARDESC" /> structure describes a symbolic constant. There is no memory associated with it.</summary>
		// Token: 0x04002DC2 RID: 11714
		[__DynamicallyInvokable]
		VAR_CONST,
		/// <summary>The variable can be accessed only through <see langword="IDispatch::Invoke" />.</summary>
		// Token: 0x04002DC3 RID: 11715
		[__DynamicallyInvokable]
		VAR_DISPATCH
	}
}
