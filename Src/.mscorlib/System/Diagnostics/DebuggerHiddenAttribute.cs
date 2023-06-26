using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Specifies the <see cref="T:System.Diagnostics.DebuggerHiddenAttribute" />. This class cannot be inherited.</summary>
	// Token: 0x020003E7 RID: 999
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class DebuggerHiddenAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerHiddenAttribute" /> class.</summary>
		// Token: 0x06003321 RID: 13089 RVA: 0x000C63A8 File Offset: 0x000C45A8
		[__DynamicallyInvokable]
		public DebuggerHiddenAttribute()
		{
		}
	}
}
