using System;

namespace System.IO.Compression
{
	/// <summary>Specifies whether to compress or decompress the underlying stream.</summary>
	// Token: 0x02000418 RID: 1048
	[global::__DynamicallyInvokable]
	public enum CompressionMode
	{
		/// <summary>Decompresses the underlying stream.</summary>
		// Token: 0x0400214B RID: 8523
		[global::__DynamicallyInvokable]
		Decompress,
		/// <summary>Compresses the underlying stream.</summary>
		// Token: 0x0400214C RID: 8524
		[global::__DynamicallyInvokable]
		Compress
	}
}
