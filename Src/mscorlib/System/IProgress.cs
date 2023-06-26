using System;

namespace System
{
	/// <summary>Defines a provider for progress updates.</summary>
	/// <typeparam name="T">The type of progress update value.</typeparam>
	// Token: 0x020000F4 RID: 244
	[__DynamicallyInvokable]
	public interface IProgress<in T>
	{
		/// <summary>Reports a progress update.</summary>
		/// <param name="value">The value of the updated progress.</param>
		// Token: 0x06000F08 RID: 3848
		[__DynamicallyInvokable]
		void Report(T value);
	}
}
