using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Describes the type of a COM member.</summary>
	// Token: 0x02000960 RID: 2400
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum ComMemberType
	{
		/// <summary>The member is a normal method.</summary>
		// Token: 0x04002B9A RID: 11162
		[__DynamicallyInvokable]
		Method,
		/// <summary>The member gets properties.</summary>
		// Token: 0x04002B9B RID: 11163
		[__DynamicallyInvokable]
		PropGet,
		/// <summary>The member sets properties.</summary>
		// Token: 0x04002B9C RID: 11164
		[__DynamicallyInvokable]
		PropSet
	}
}
