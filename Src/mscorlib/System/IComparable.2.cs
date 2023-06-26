using System;

namespace System
{
	/// <summary>Defines a generalized comparison method that a value type or class implements to create a type-specific comparison method for ordering or sorting its instances.</summary>
	/// <typeparam name="T">The type of object to compare.</typeparam>
	// Token: 0x02000059 RID: 89
	[__DynamicallyInvokable]
	public interface IComparable<in T>
	{
		/// <summary>Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.</summary>
		/// <param name="other">An object to compare with this instance.</param>
		/// <returns>A value that indicates the relative order of the objects being compared. The return value has these meanings:  
		///   Value  
		///
		///   Meaning  
		///
		///   Less than zero  
		///
		///   This instance precedes <paramref name="other" /> in the sort order.  
		///
		///   Zero  
		///
		///   This instance occurs in the same position in the sort order as <paramref name="other" />.  
		///
		///   Greater than zero  
		///
		///   This instance follows <paramref name="other" /> in the sort order.</returns>
		// Token: 0x06000333 RID: 819
		[__DynamicallyInvokable]
		int CompareTo(T other);
	}
}
