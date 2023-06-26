using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	/// <summary>Specifies the position in a stream to use for seeking.</summary>
	// Token: 0x020001A0 RID: 416
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum SeekOrigin
	{
		/// <summary>Specifies the beginning of a stream.</summary>
		// Token: 0x040008F0 RID: 2288
		[__DynamicallyInvokable]
		Begin,
		/// <summary>Specifies the current position within a stream.</summary>
		// Token: 0x040008F1 RID: 2289
		[__DynamicallyInvokable]
		Current,
		/// <summary>Specifies the end of a stream.</summary>
		// Token: 0x040008F2 RID: 2290
		[__DynamicallyInvokable]
		End
	}
}
