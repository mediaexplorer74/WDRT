using System;

namespace System.Collections
{
	/// <summary>Supports the structural comparison of collection objects.</summary>
	// Token: 0x020004A4 RID: 1188
	[__DynamicallyInvokable]
	public interface IStructuralComparable
	{
		/// <summary>Determines whether the current collection object precedes, occurs in the same position as, or follows another object in the sort order.</summary>
		/// <param name="other">The object to compare with the current instance.</param>
		/// <param name="comparer">An object that compares members of the current collection object with the corresponding members of <paramref name="other" />.</param>
		/// <returns>A signed integer that indicates the relationship of the current collection object to <paramref name="other" /> in the sort order: - If less than 0, the current instance precedes <paramref name="other" />. - If 0, the current instance and <paramref name="other" /> are equal. - If greater than 0, the current instance follows <paramref name="other" />.  
		///   Return value  
		///
		///   Description  
		///
		///   -1  
		///
		///   The current instance precedes <paramref name="other" />.  
		///
		///   0  
		///
		///   The current instance and <paramref name="other" /> are equal.  
		///
		///   1  
		///
		///   The current instance follows <paramref name="other" />.</returns>
		/// <exception cref="T:System.ArgumentException">This instance and <paramref name="other" /> are not the same type.</exception>
		// Token: 0x06003913 RID: 14611
		[__DynamicallyInvokable]
		int CompareTo(object other, IComparer comparer);
	}
}
