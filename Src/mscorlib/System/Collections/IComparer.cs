using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	/// <summary>Exposes a method that compares two objects.</summary>
	// Token: 0x02000499 RID: 1177
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public interface IComparer
	{
		/// <summary>Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.</summary>
		/// <param name="x">The first object to compare.</param>
		/// <param name="y">The second object to compare.</param>
		/// <returns>A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />:   - If less than 0, <paramref name="x" /> is less than <paramref name="y" />.   - If 0, <paramref name="x" /> equals <paramref name="y" />.   - If greater than 0, <paramref name="x" /> is greater than <paramref name="y" />.</returns>
		/// <exception cref="T:System.ArgumentException">Neither <paramref name="x" /> nor <paramref name="y" /> implements the <see cref="T:System.IComparable" /> interface.  
		///  -or-  
		///  <paramref name="x" /> and <paramref name="y" /> are of different types and neither one can handle comparisons with the other.</exception>
		// Token: 0x060038C3 RID: 14531
		[__DynamicallyInvokable]
		int Compare(object x, object y);
	}
}
