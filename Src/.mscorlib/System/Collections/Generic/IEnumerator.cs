using System;

namespace System.Collections.Generic
{
	/// <summary>Supports a simple iteration over a generic collection.</summary>
	/// <typeparam name="T">The type of objects to enumerate.</typeparam>
	// Token: 0x020004D2 RID: 1234
	[__DynamicallyInvokable]
	public interface IEnumerator<out T> : IDisposable, IEnumerator
	{
		/// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
		/// <returns>The element in the collection at the current position of the enumerator.</returns>
		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06003AEC RID: 15084
		[__DynamicallyInvokable]
		T Current
		{
			[__DynamicallyInvokable]
			get;
		}
	}
}
