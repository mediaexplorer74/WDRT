using System;

namespace System.Globalization
{
	/// <summary>Defines the formatting options that customize string parsing for the <see cref="Overload:System.TimeSpan.ParseExact" /> and <see cref="Overload:System.TimeSpan.TryParseExact" /> methods.</summary>
	// Token: 0x020003D5 RID: 981
	[Flags]
	[__DynamicallyInvokable]
	public enum TimeSpanStyles
	{
		/// <summary>Indicates that input is interpreted as a negative time interval only if a negative sign is present.</summary>
		// Token: 0x0400154B RID: 5451
		[__DynamicallyInvokable]
		None = 0,
		/// <summary>Indicates that input is always interpreted as a negative time interval.</summary>
		// Token: 0x0400154C RID: 5452
		[__DynamicallyInvokable]
		AssumeNegative = 1
	}
}
