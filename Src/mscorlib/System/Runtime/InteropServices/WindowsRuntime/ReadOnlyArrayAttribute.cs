using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	/// <summary>When applied to an array parameter in a Windows Runtime component, specifies that the contents of the array that is passed to that parameter are used only for input. The caller expects the array to be unchanged by the call.</summary>
	// Token: 0x020009C7 RID: 2503
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
	[__DynamicallyInvokable]
	public sealed class ReadOnlyArrayAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.WindowsRuntime.ReadOnlyArrayAttribute" /> class.</summary>
		// Token: 0x060063DF RID: 25567 RVA: 0x00156095 File Offset: 0x00154295
		[__DynamicallyInvokable]
		public ReadOnlyArrayAttribute()
		{
		}
	}
}
