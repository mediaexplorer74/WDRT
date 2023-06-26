using System;

namespace System
{
	/// <summary>Controls how URI information is escaped.</summary>
	// Token: 0x0200004A RID: 74
	[global::__DynamicallyInvokable]
	public enum UriFormat
	{
		/// <summary>Escaping is performed according to the rules in RFC 2396.</summary>
		// Token: 0x0400048E RID: 1166
		[global::__DynamicallyInvokable]
		UriEscaped = 1,
		/// <summary>No escaping is performed.</summary>
		// Token: 0x0400048F RID: 1167
		[global::__DynamicallyInvokable]
		Unescaped,
		/// <summary>Characters that have a reserved meaning in the requested URI components remain escaped. All others are not escaped.</summary>
		// Token: 0x04000490 RID: 1168
		[global::__DynamicallyInvokable]
		SafeUnescaped
	}
}
