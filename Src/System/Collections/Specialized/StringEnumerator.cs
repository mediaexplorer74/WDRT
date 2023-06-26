using System;

namespace System.Collections.Specialized
{
	/// <summary>Supports a simple iteration over a <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
	// Token: 0x020003B6 RID: 950
	public class StringEnumerator
	{
		// Token: 0x060023BD RID: 9149 RVA: 0x000A8708 File Offset: 0x000A6908
		internal StringEnumerator(StringCollection mappings)
		{
			this.temp = mappings;
			this.baseEnumerator = this.temp.GetEnumerator();
		}

		/// <summary>Gets the current element in the collection.</summary>
		/// <returns>The current element in the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x060023BE RID: 9150 RVA: 0x000A8728 File Offset: 0x000A6928
		public string Current
		{
			get
			{
				return (string)this.baseEnumerator.Current;
			}
		}

		/// <summary>Advances the enumerator to the next element of the collection.</summary>
		/// <returns>
		///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x060023BF RID: 9151 RVA: 0x000A873A File Offset: 0x000A693A
		public bool MoveNext()
		{
			return this.baseEnumerator.MoveNext();
		}

		/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x060023C0 RID: 9152 RVA: 0x000A8747 File Offset: 0x000A6947
		public void Reset()
		{
			this.baseEnumerator.Reset();
		}

		// Token: 0x04001FDB RID: 8155
		private IEnumerator baseEnumerator;

		// Token: 0x04001FDC RID: 8156
		private IEnumerable temp;
	}
}
