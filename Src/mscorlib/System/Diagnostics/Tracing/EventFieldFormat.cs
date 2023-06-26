using System;

namespace System.Diagnostics.Tracing
{
	/// <summary>Specifies how to format the value of a user-defined type and can be used to override the default formatting for a field.</summary>
	// Token: 0x02000442 RID: 1090
	[__DynamicallyInvokable]
	public enum EventFieldFormat
	{
		/// <summary>Default.</summary>
		// Token: 0x04001830 RID: 6192
		[__DynamicallyInvokable]
		Default,
		/// <summary>String.</summary>
		// Token: 0x04001831 RID: 6193
		[__DynamicallyInvokable]
		String = 2,
		/// <summary>Boolean</summary>
		// Token: 0x04001832 RID: 6194
		[__DynamicallyInvokable]
		Boolean,
		/// <summary>Hexadecimal.</summary>
		// Token: 0x04001833 RID: 6195
		[__DynamicallyInvokable]
		Hexadecimal,
		/// <summary>XML.</summary>
		// Token: 0x04001834 RID: 6196
		[__DynamicallyInvokable]
		Xml = 11,
		/// <summary>JSON.</summary>
		// Token: 0x04001835 RID: 6197
		[__DynamicallyInvokable]
		Json,
		/// <summary>HResult.</summary>
		// Token: 0x04001836 RID: 6198
		[__DynamicallyInvokable]
		HResult = 15
	}
}
