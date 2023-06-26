using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	/// <summary>Supplies a hash code for an object, using a custom hash function.</summary>
	// Token: 0x0200049F RID: 1183
	[Obsolete("Please use IEqualityComparer instead.")]
	[ComVisible(true)]
	public interface IHashCodeProvider
	{
		/// <summary>Returns a hash code for the specified object.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> for which a hash code is to be returned.</param>
		/// <returns>A hash code for the specified object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The type of <paramref name="obj" /> is a reference type and <paramref name="obj" /> is <see langword="null" />.</exception>
		// Token: 0x060038D8 RID: 14552
		int GetHashCode(object obj);
	}
}
