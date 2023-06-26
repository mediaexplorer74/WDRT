using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Defines a general-purpose Tuple implementation that allows acccess to Tuple instance members without knowing the underlying Tuple type.</summary>
	// Token: 0x020008EA RID: 2282
	public interface ITuple
	{
		/// <summary>Gets the number of elements in this <see langword="Tuple" /> instance.</summary>
		/// <returns>The number of elements in this <see langword="Tuple" /> instance.</returns>
		// Token: 0x17001026 RID: 4134
		// (get) Token: 0x06005E1F RID: 24095
		int Length { get; }

		/// <summary>Returns the value of the specified <see langword="Tuple" /> element.</summary>
		/// <param name="index">The index of the specified <see langword="Tuple" /> element. <paramref name="index" /> can range from 0 for <see langword="Item1" /> of the <see langword="Tuple" /> to one less than the number of elements in the <see langword="Tuple" />.</param>
		/// <returns>The value of the specified <see langword="Tuple" /> element.</returns>
		// Token: 0x17001027 RID: 4135
		object this[int index] { get; }
	}
}
