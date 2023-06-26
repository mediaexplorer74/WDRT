using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Indicates that a method will allow a variable number of arguments in its invocation. This class cannot be inherited.</summary>
	// Token: 0x0200011F RID: 287
	[AttributeUsage(AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class ParamArrayAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ParamArrayAttribute" /> class with default properties.</summary>
		// Token: 0x060010DA RID: 4314 RVA: 0x00032CCF File Offset: 0x00030ECF
		[__DynamicallyInvokable]
		public ParamArrayAttribute()
		{
		}
	}
}
