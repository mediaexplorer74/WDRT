using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Indicates that the COM threading model for an application is single-threaded apartment (STA).</summary>
	// Token: 0x02000140 RID: 320
	[AttributeUsage(AttributeTargets.Method)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class STAThreadAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.STAThreadAttribute" /> class.</summary>
		// Token: 0x0600133F RID: 4927 RVA: 0x000387CA File Offset: 0x000369CA
		[__DynamicallyInvokable]
		public STAThreadAttribute()
		{
		}
	}
}
