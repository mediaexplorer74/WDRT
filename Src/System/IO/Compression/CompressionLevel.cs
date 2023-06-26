using System;

namespace System.IO.Compression
{
	/// <summary>Specifies values that indicate whether a compression operation emphasizes speed or compression size.</summary>
	// Token: 0x0200041B RID: 1051
	[global::__DynamicallyInvokable]
	public enum CompressionLevel
	{
		/// <summary>The compression operation should be optimally compressed, even if the operation takes a longer time to complete.</summary>
		// Token: 0x04002153 RID: 8531
		[global::__DynamicallyInvokable]
		Optimal,
		/// <summary>The compression operation should complete as quickly as possible, even if the resulting file is not optimally compressed.</summary>
		// Token: 0x04002154 RID: 8532
		[global::__DynamicallyInvokable]
		Fastest,
		/// <summary>No compression should be performed on the file.</summary>
		// Token: 0x04002155 RID: 8533
		[global::__DynamicallyInvokable]
		NoCompression
	}
}
