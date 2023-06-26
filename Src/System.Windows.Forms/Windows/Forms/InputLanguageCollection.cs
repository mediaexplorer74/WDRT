using System;
using System.Collections;

namespace System.Windows.Forms
{
	/// <summary>Stores <see cref="T:System.Windows.Forms.InputLanguage" /> objects.</summary>
	// Token: 0x0200029E RID: 670
	public class InputLanguageCollection : ReadOnlyCollectionBase
	{
		// Token: 0x06002A1F RID: 10783 RVA: 0x000BF87D File Offset: 0x000BDA7D
		internal InputLanguageCollection(InputLanguage[] value)
		{
			base.InnerList.AddRange(value);
		}

		/// <summary>Gets the entry at the specified index of the <see cref="T:System.Windows.Forms.InputLanguageCollection" />.</summary>
		/// <param name="index">The zero-based index of the entry to locate in the collection.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.InputLanguage" /> at the specified index of the collection.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the valid range of indexes for the collection.</exception>
		// Token: 0x170009DA RID: 2522
		public InputLanguage this[int index]
		{
			get
			{
				return (InputLanguage)base.InnerList[index];
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.InputLanguageCollection" /> contains the specified <see cref="T:System.Windows.Forms.InputLanguage" />.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.InputLanguage" /> to locate.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.InputLanguage" /> is contained in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002A21 RID: 10785 RVA: 0x000BF8A4 File Offset: 0x000BDAA4
		public bool Contains(InputLanguage value)
		{
			return base.InnerList.Contains(value);
		}

		/// <summary>Copies the <see cref="T:System.Windows.Forms.InputLanguageCollection" /> values to a one-dimensional <see cref="T:System.Array" /> at the specified index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the values copied from <see cref="T:System.Windows.Forms.InputLanguageCollection" />.</param>
		/// <param name="index">The index in <paramref name="array" /> where copying begins.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> specifies a multidimensional array.  
		/// -or-  
		/// The number of elements in the <see cref="T:System.Windows.Forms.InputLanguageCollection" /> is greater than the available space between the <paramref name="index" /> and the end of <paramref name="array" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="array" />.</exception>
		// Token: 0x06002A22 RID: 10786 RVA: 0x000BF8B2 File Offset: 0x000BDAB2
		public void CopyTo(InputLanguage[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		/// <summary>Returns the index of an <see cref="T:System.Windows.Forms.InputLanguage" /> in the <see cref="T:System.Windows.Forms.InputLanguageCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.InputLanguage" /> to locate.</param>
		/// <returns>The index of the <see cref="T:System.Windows.Forms.InputLanguage" /> in the <see cref="T:System.Windows.Forms.InputLanguageCollection" />, if found; otherwise, -1.</returns>
		// Token: 0x06002A23 RID: 10787 RVA: 0x000BF8C1 File Offset: 0x000BDAC1
		public int IndexOf(InputLanguage value)
		{
			return base.InnerList.IndexOf(value);
		}
	}
}
