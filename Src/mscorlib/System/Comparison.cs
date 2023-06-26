using System;

namespace System
{
	/// <summary>Represents the method that compares two objects of the same type.</summary>
	/// <param name="x">The first object to compare.</param>
	/// <param name="y">The second object to compare.</param>
	/// <typeparam name="T">The type of the objects to compare.</typeparam>
	/// <returns>A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.  
	///   Value  
	///
	///   Meaning  
	///
	///   Less than 0  
	///
	///  <paramref name="x" /> is less than <paramref name="y" />.  
	///
	///   0  
	///
	///  <paramref name="x" /> equals <paramref name="y" />.  
	///
	///   Greater than 0  
	///
	///  <paramref name="x" /> is greater than <paramref name="y" />.</returns>
	// Token: 0x02000052 RID: 82
	// (Invoke) Token: 0x06000280 RID: 640
	[__DynamicallyInvokable]
	public delegate int Comparison<in T>(T x, T y);
}
