using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	/// <summary>Supports a simple iteration over a non-generic collection.</summary>
	// Token: 0x0200049D RID: 1181
	[Guid("496B0ABF-CDEE-11d3-88E8-00902754C43A")]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public interface IEnumerator
	{
		/// <summary>Advances the enumerator to the next element of the collection.</summary>
		/// <returns>
		///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x060038D3 RID: 14547
		[__DynamicallyInvokable]
		bool MoveNext();

		/// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
		/// <returns>The element in the collection at the current position of the enumerator.</returns>
		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x060038D4 RID: 14548
		[__DynamicallyInvokable]
		object Current
		{
			[__DynamicallyInvokable]
			get;
		}

		/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x060038D5 RID: 14549
		[__DynamicallyInvokable]
		void Reset();
	}
}
