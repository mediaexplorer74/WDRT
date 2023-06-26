using System;

namespace System.Collections.Concurrent
{
	/// <summary>Specifies options to control the buffering behavior of a partitioner</summary>
	// Token: 0x020004B1 RID: 1201
	[Flags]
	[__DynamicallyInvokable]
	[Serializable]
	public enum EnumerablePartitionerOptions
	{
		/// <summary>Use the default behavior, which is to use buffering to achieve optimal performance.</summary>
		// Token: 0x04001938 RID: 6456
		[__DynamicallyInvokable]
		None = 0,
		/// <summary>Create a partitioner that takes items from the source enumerable one at a time and does not use intermediate storage that can be accessed more efficiently by multiple threads. This option provides support for low latency (items will be processed as soon as they are available from the source) and provides partial support for dependencies between items (a thread cannot deadlock waiting for an item that the thread itself is responsible for processing).</summary>
		// Token: 0x04001939 RID: 6457
		[__DynamicallyInvokable]
		NoBuffering = 1
	}
}
