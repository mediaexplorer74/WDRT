using System;

namespace System
{
	/// <summary>Defines the kinds of <see cref="T:System.Uri" />s for the <see cref="M:System.Uri.IsWellFormedUriString(System.String,System.UriKind)" /> and several <see cref="Overload:System.Uri.#ctor" /> methods.</summary>
	// Token: 0x02000048 RID: 72
	[global::__DynamicallyInvokable]
	public enum UriKind
	{
		/// <summary>The kind of the Uri is indeterminate.</summary>
		// Token: 0x04000478 RID: 1144
		[global::__DynamicallyInvokable]
		RelativeOrAbsolute,
		/// <summary>The Uri is an absolute Uri.</summary>
		// Token: 0x04000479 RID: 1145
		[global::__DynamicallyInvokable]
		Absolute,
		/// <summary>The Uri is a relative Uri.</summary>
		// Token: 0x0400047A RID: 1146
		[global::__DynamicallyInvokable]
		Relative
	}
}
