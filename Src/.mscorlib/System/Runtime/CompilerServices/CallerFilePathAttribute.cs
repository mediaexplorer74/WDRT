using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Allows you to obtain the full path of the source file that contains the caller. This is the file path at the time of compile.</summary>
	// Token: 0x020008E5 RID: 2277
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	[__DynamicallyInvokable]
	public sealed class CallerFilePathAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.CallerFilePathAttribute" /> class.</summary>
		// Token: 0x06005E18 RID: 24088 RVA: 0x0014BD0D File Offset: 0x00149F0D
		[__DynamicallyInvokable]
		public CallerFilePathAttribute()
		{
		}
	}
}
