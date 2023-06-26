using System;

namespace System.Net
{
	/// <summary>Represents the file compression and decompression encoding format to be used to compress the data received in response to an <see cref="T:System.Net.HttpWebRequest" />.</summary>
	// Token: 0x02000108 RID: 264
	[Flags]
	[global::__DynamicallyInvokable]
	public enum DecompressionMethods
	{
		/// <summary>Do not use compression.</summary>
		// Token: 0x04000ECB RID: 3787
		[global::__DynamicallyInvokable]
		None = 0,
		/// <summary>Use the gZip compression-decompression algorithm.</summary>
		// Token: 0x04000ECC RID: 3788
		[global::__DynamicallyInvokable]
		GZip = 1,
		/// <summary>Use the deflate compression-decompression algorithm.</summary>
		// Token: 0x04000ECD RID: 3789
		[global::__DynamicallyInvokable]
		Deflate = 2
	}
}
