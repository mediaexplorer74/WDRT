using System;

namespace System.Collections.Specialized
{
	/// <summary>Represents a collection of strings.</summary>
	// Token: 0x020003B5 RID: 949
	[Serializable]
	public class StringCollection : IList, ICollection, IEnumerable
	{
		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the entry to get or set.</param>
		/// <returns>The element at the specified index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.Specialized.StringCollection.Count" />.</exception>
		// Token: 0x17000904 RID: 2308
		public string this[int index]
		{
			get
			{
				return (string)this.data[index];
			}
			set
			{
				this.data[index] = value;
			}
		}

		/// <summary>Gets the number of strings contained in the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <returns>The number of strings contained in the <see cref="T:System.Collections.Specialized.StringCollection" />.</returns>
		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x060023A3 RID: 9123 RVA: 0x000A85BF File Offset: 0x000A67BF
		public int Count
		{
			get
			{
				return this.data.Count;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.StringCollection" /> object is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Specialized.StringCollection" /> object is read-only; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x060023A4 RID: 9124 RVA: 0x000A85CC File Offset: 0x000A67CC
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.StringCollection" /> object has a fixed size.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Specialized.StringCollection" /> object has a fixed size; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x060023A5 RID: 9125 RVA: 0x000A85CF File Offset: 0x000A67CF
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Adds a string to the end of the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <param name="value">The string to add to the end of the <see cref="T:System.Collections.Specialized.StringCollection" />. The value can be <see langword="null" />.</param>
		/// <returns>The zero-based index at which the new element is inserted.</returns>
		// Token: 0x060023A6 RID: 9126 RVA: 0x000A85D2 File Offset: 0x000A67D2
		public int Add(string value)
		{
			return this.data.Add(value);
		}

		/// <summary>Copies the elements of a string array to the end of the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <param name="value">An array of strings to add to the end of the <see cref="T:System.Collections.Specialized.StringCollection" />. The array itself can not be <see langword="null" /> but it can contain elements that are <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x060023A7 RID: 9127 RVA: 0x000A85E0 File Offset: 0x000A67E0
		public void AddRange(string[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.data.AddRange(value);
		}

		/// <summary>Removes all the strings from the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		// Token: 0x060023A8 RID: 9128 RVA: 0x000A85FC File Offset: 0x000A67FC
		public void Clear()
		{
			this.data.Clear();
		}

		/// <summary>Determines whether the specified string is in the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <param name="value">The string to locate in the <see cref="T:System.Collections.Specialized.StringCollection" />. The value can be <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is found in the <see cref="T:System.Collections.Specialized.StringCollection" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060023A9 RID: 9129 RVA: 0x000A8609 File Offset: 0x000A6809
		public bool Contains(string value)
		{
			return this.data.Contains(value);
		}

		/// <summary>Copies the entire <see cref="T:System.Collections.Specialized.StringCollection" /> values to a one-dimensional array of strings, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional array of strings that is the destination of the elements copied from <see cref="T:System.Collections.Specialized.StringCollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the source <see cref="T:System.Collections.Specialized.StringCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.Specialized.StringCollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x060023AA RID: 9130 RVA: 0x000A8617 File Offset: 0x000A6817
		public void CopyTo(string[] array, int index)
		{
			this.data.CopyTo(array, index);
		}

		/// <summary>Returns a <see cref="T:System.Collections.Specialized.StringEnumerator" /> that iterates through the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringEnumerator" /> for the <see cref="T:System.Collections.Specialized.StringCollection" />.</returns>
		// Token: 0x060023AB RID: 9131 RVA: 0x000A8626 File Offset: 0x000A6826
		public StringEnumerator GetEnumerator()
		{
			return new StringEnumerator(this);
		}

		/// <summary>Searches for the specified string and returns the zero-based index of the first occurrence within the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <param name="value">The string to locate. The value can be <see langword="null" />.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> in the <see cref="T:System.Collections.Specialized.StringCollection" />, if found; otherwise, -1.</returns>
		// Token: 0x060023AC RID: 9132 RVA: 0x000A862E File Offset: 0x000A682E
		public int IndexOf(string value)
		{
			return this.data.IndexOf(value);
		}

		/// <summary>Inserts a string into the <see cref="T:System.Collections.Specialized.StringCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> is inserted.</param>
		/// <param name="value">The string to insert. The value can be <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> greater than <see cref="P:System.Collections.Specialized.StringCollection.Count" />.</exception>
		// Token: 0x060023AD RID: 9133 RVA: 0x000A863C File Offset: 0x000A683C
		public void Insert(int index, string value)
		{
			this.data.Insert(index, value);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.StringCollection" /> is read-only.</summary>
		/// <returns>This property always returns <see langword="false" />.</returns>
		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x060023AE RID: 9134 RVA: 0x000A864B File Offset: 0x000A684B
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.Specialized.StringCollection" /> is synchronized (thread safe).</summary>
		/// <returns>This property always returns <see langword="false" />.</returns>
		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x060023AF RID: 9135 RVA: 0x000A864E File Offset: 0x000A684E
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Removes the first occurrence of a specific string from the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <param name="value">The string to remove from the <see cref="T:System.Collections.Specialized.StringCollection" />. The value can be <see langword="null" />.</param>
		// Token: 0x060023B0 RID: 9136 RVA: 0x000A8651 File Offset: 0x000A6851
		public void Remove(string value)
		{
			this.data.Remove(value);
		}

		/// <summary>Removes the string at the specified index of the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <param name="index">The zero-based index of the string to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.Specialized.StringCollection.Count" />.</exception>
		// Token: 0x060023B1 RID: 9137 RVA: 0x000A865F File Offset: 0x000A685F
		public void RemoveAt(int index)
		{
			this.data.RemoveAt(index);
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.StringCollection" />.</returns>
		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x060023B2 RID: 9138 RVA: 0x000A866D File Offset: 0x000A686D
		public object SyncRoot
		{
			get
			{
				return this.data.SyncRoot;
			}
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <returns>The element at the specified index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.Specialized.StringCollection.Count" />.</exception>
		// Token: 0x1700090B RID: 2315
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (string)value;
			}
		}

		/// <summary>Adds an object to the end of the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to be added to the end of the <see cref="T:System.Collections.Specialized.StringCollection" />. The value can be <see langword="null" />.</param>
		/// <returns>The <see cref="T:System.Collections.Specialized.StringCollection" /> index at which the <paramref name="value" /> has been added.</returns>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.StringCollection" /> is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.Specialized.StringCollection" /> has a fixed size.</exception>
		// Token: 0x060023B5 RID: 9141 RVA: 0x000A8692 File Offset: 0x000A6892
		int IList.Add(object value)
		{
			return this.Add((string)value);
		}

		/// <summary>Determines whether an element is in the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.Specialized.StringCollection" />. The value can be <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is found in the <see cref="T:System.Collections.Specialized.StringCollection" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060023B6 RID: 9142 RVA: 0x000A86A0 File Offset: 0x000A68A0
		bool IList.Contains(object value)
		{
			return this.Contains((string)value);
		}

		/// <summary>Searches for the specified <see cref="T:System.Object" /> and returns the zero-based index of the first occurrence within the entire <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.Specialized.StringCollection" />. The value can be <see langword="null" />.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the entire <see cref="T:System.Collections.Specialized.StringCollection" />, if found; otherwise, -1.</returns>
		// Token: 0x060023B7 RID: 9143 RVA: 0x000A86AE File Offset: 0x000A68AE
		int IList.IndexOf(object value)
		{
			return this.IndexOf((string)value);
		}

		/// <summary>Inserts an element into the <see cref="T:System.Collections.Specialized.StringCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to insert. The value can be <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is greater than <see cref="P:System.Collections.Specialized.StringCollection.Count" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.StringCollection" /> is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.Specialized.StringCollection" /> has a fixed size.</exception>
		// Token: 0x060023B8 RID: 9144 RVA: 0x000A86BC File Offset: 0x000A68BC
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (string)value);
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.Specialized.StringCollection" />. The value can be <see langword="null" />.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.StringCollection" /> is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.Specialized.StringCollection" /> has a fixed size.</exception>
		// Token: 0x060023B9 RID: 9145 RVA: 0x000A86CB File Offset: 0x000A68CB
		void IList.Remove(object value)
		{
			this.Remove((string)value);
		}

		/// <summary>Copies the entire <see cref="T:System.Collections.Specialized.StringCollection" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Specialized.StringCollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the source <see cref="T:System.Collections.Specialized.StringCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.Specialized.StringCollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x060023BA RID: 9146 RVA: 0x000A86D9 File Offset: 0x000A68D9
		void ICollection.CopyTo(Array array, int index)
		{
			this.data.CopyTo(array, index);
		}

		/// <summary>Returns a <see cref="T:System.Collections.IEnumerator" /> that iterates through the <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
		/// <returns>A <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.Specialized.StringCollection" />.</returns>
		// Token: 0x060023BB RID: 9147 RVA: 0x000A86E8 File Offset: 0x000A68E8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.data.GetEnumerator();
		}

		// Token: 0x04001FDA RID: 8154
		private ArrayList data = new ArrayList();
	}
}
