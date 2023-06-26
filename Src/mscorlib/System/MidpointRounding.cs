using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Specifies how mathematical rounding methods should process a number that is midway between two numbers.</summary>
	// Token: 0x0200010E RID: 270
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public enum MidpointRounding
	{
		/// <summary>When a number is halfway between two others, it is rounded toward the nearest even number.</summary>
		// Token: 0x040005C1 RID: 1473
		[__DynamicallyInvokable]
		ToEven,
		/// <summary>When a number is halfway between two others, it is rounded toward the nearest number that is away from zero.</summary>
		// Token: 0x040005C2 RID: 1474
		[__DynamicallyInvokable]
		AwayFromZero
	}
}
