using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	/// <summary>When applied to an array parameter in a Windows Runtime component, specifies that the contents of an array that is passed to that parameter are used only for output. The caller does not guarantee that the contents are initialized, and the called method should not read the contents.</summary>
	// Token: 0x020009C8 RID: 2504
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
	[__DynamicallyInvokable]
	public sealed class WriteOnlyArrayAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.WindowsRuntime.WriteOnlyArrayAttribute" /> class.</summary>
		// Token: 0x060063E0 RID: 25568 RVA: 0x0015609D File Offset: 0x0015429D
		[__DynamicallyInvokable]
		public WriteOnlyArrayAttribute()
		{
		}
	}
}
