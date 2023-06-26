using System;

namespace System.Diagnostics.Tracing
{
	/// <summary>Specifies how the ETW manifest for the event source is generated.</summary>
	// Token: 0x0200042E RID: 1070
	[Flags]
	[__DynamicallyInvokable]
	public enum EventManifestOptions
	{
		/// <summary>No options are specified.</summary>
		// Token: 0x040017CF RID: 6095
		[__DynamicallyInvokable]
		None = 0,
		/// <summary>Causes an exception to be raised if any inconsistencies occur when writing the manifest file.</summary>
		// Token: 0x040017D0 RID: 6096
		[__DynamicallyInvokable]
		Strict = 1,
		/// <summary>Generates a resources node under the localization folder for every satellite assembly provided.</summary>
		// Token: 0x040017D1 RID: 6097
		[__DynamicallyInvokable]
		AllCultures = 2,
		/// <summary>A manifest is generated only the event source must be registered on the host computer.</summary>
		// Token: 0x040017D2 RID: 6098
		[__DynamicallyInvokable]
		OnlyIfNeededForRegistration = 4,
		/// <summary>Overrides the default behavior that the current <see cref="T:System.Diagnostics.Tracing.EventSource" /> must be the base class of the user-defined type passed to the write method. This enables the validation of .NET event sources.</summary>
		// Token: 0x040017D3 RID: 6099
		[__DynamicallyInvokable]
		AllowEventSourceOverride = 8
	}
}
