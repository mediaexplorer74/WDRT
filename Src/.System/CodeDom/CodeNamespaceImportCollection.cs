using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeNamespaceImport" /> objects.</summary>
	// Token: 0x02000646 RID: 1606
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeNamespaceImportCollection : IList, ICollection, IEnumerable
	{
		/// <summary>Gets or sets the <see cref="T:System.CodeDom.CodeNamespaceImport" /> object at the specified index in the collection.</summary>
		/// <param name="index">The index of the collection to access.</param>
		/// <returns>A <see cref="T:System.CodeDom.CodeNamespaceImport" /> object at each valid index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is outside the valid range of indexes for the collection.</exception>
		// Token: 0x17000E03 RID: 3587
		public CodeNamespaceImport this[int index]
		{
			get
			{
				return (CodeNamespaceImport)this.data[index];
			}
			set
			{
				this.data[index] = value;
				this.SyncKeys();
			}
		}

		/// <summary>Gets the number of namespaces in the collection.</summary>
		/// <returns>The number of namespaces in the collection.</returns>
		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x06003A55 RID: 14933 RVA: 0x000F3713 File Offset: 0x000F1913
		public int Count
		{
			get
			{
				return this.data.Count;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IList" /> is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.IList" /> is read-only; otherwise, <see langword="false" />.  This property always returns <see langword="false" />.</returns>
		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x06003A56 RID: 14934 RVA: 0x000F3720 File Offset: 0x000F1920
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IList" /> has a fixed size.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.IList" /> has a fixed size; otherwise, <see langword="false" />.  This property always returns <see langword="false" />.</returns>
		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x06003A57 RID: 14935 RVA: 0x000F3723 File Offset: 0x000F1923
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Adds a <see cref="T:System.CodeDom.CodeNamespaceImport" /> object to the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeNamespaceImport" /> object to add to the collection.</param>
		// Token: 0x06003A58 RID: 14936 RVA: 0x000F3726 File Offset: 0x000F1926
		public void Add(CodeNamespaceImport value)
		{
			if (!this.keys.ContainsKey(value.Namespace))
			{
				this.keys[value.Namespace] = value;
				this.data.Add(value);
			}
		}

		/// <summary>Adds a set of <see cref="T:System.CodeDom.CodeNamespaceImport" /> objects to the collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.CodeDom.CodeNamespaceImport" /> that contains the objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06003A59 RID: 14937 RVA: 0x000F375C File Offset: 0x000F195C
		public void AddRange(CodeNamespaceImport[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			foreach (CodeNamespaceImport codeNamespaceImport in value)
			{
				this.Add(codeNamespaceImport);
			}
		}

		/// <summary>Clears the collection of members.</summary>
		// Token: 0x06003A5A RID: 14938 RVA: 0x000F3792 File Offset: 0x000F1992
		public void Clear()
		{
			this.data.Clear();
			this.keys.Clear();
		}

		// Token: 0x06003A5B RID: 14939 RVA: 0x000F37AC File Offset: 0x000F19AC
		private void SyncKeys()
		{
			this.keys = new Hashtable(StringComparer.OrdinalIgnoreCase);
			foreach (object obj in this)
			{
				CodeNamespaceImport codeNamespaceImport = (CodeNamespaceImport)obj;
				this.keys[codeNamespaceImport.Namespace] = codeNamespaceImport;
			}
		}

		/// <summary>Gets an enumerator that enumerates the collection members.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that indicates the collection members.</returns>
		// Token: 0x06003A5C RID: 14940 RVA: 0x000F381C File Offset: 0x000F1A1C
		public IEnumerator GetEnumerator()
		{
			return this.data.GetEnumerator();
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <returns>The element at the specified index.</returns>
		// Token: 0x17000E07 RID: 3591
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (CodeNamespaceImport)value;
				this.SyncKeys();
			}
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x06003A5F RID: 14943 RVA: 0x000F3847 File Offset: 0x000F1A47
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, <see langword="false" />. This property always returns <see langword="false" />.</returns>
		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x06003A60 RID: 14944 RVA: 0x000F384F File Offset: 0x000F1A4F
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  This property always returns <see langword="null" />.</returns>
		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x06003A61 RID: 14945 RVA: 0x000F3852 File Offset: 0x000F1A52
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Collections.ICollection" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x06003A62 RID: 14946 RVA: 0x000F3855 File Offset: 0x000F1A55
		void ICollection.CopyTo(Array array, int index)
		{
			this.data.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that can iterate through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06003A63 RID: 14947 RVA: 0x000F3864 File Offset: 0x000F1A64
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Adds an object to the <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
		/// <returns>The position at which the new element was inserted.</returns>
		// Token: 0x06003A64 RID: 14948 RVA: 0x000F386C File Offset: 0x000F1A6C
		int IList.Add(object value)
		{
			return this.data.Add((CodeNamespaceImport)value);
		}

		/// <summary>Removes all items from the <see cref="T:System.Collections.IList" />.</summary>
		// Token: 0x06003A65 RID: 14949 RVA: 0x000F387F File Offset: 0x000F1A7F
		void IList.Clear()
		{
			this.Clear();
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IList" /> contains a specific value.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		/// <returns>
		///   <see langword="true" /> if the value is in the list; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003A66 RID: 14950 RVA: 0x000F3887 File Offset: 0x000F1A87
		bool IList.Contains(object value)
		{
			return this.data.Contains(value);
		}

		/// <summary>Determines the index of a specific item in the <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		/// <returns>The index of <paramref name="value" /> if it is found in the list; otherwise, -1.</returns>
		// Token: 0x06003A67 RID: 14951 RVA: 0x000F3895 File Offset: 0x000F1A95
		int IList.IndexOf(object value)
		{
			return this.data.IndexOf((CodeNamespaceImport)value);
		}

		/// <summary>Inserts an item in the <see cref="T:System.Collections.IList" /> at the specified position.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to insert into the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06003A68 RID: 14952 RVA: 0x000F38A8 File Offset: 0x000F1AA8
		void IList.Insert(int index, object value)
		{
			this.data.Insert(index, (CodeNamespaceImport)value);
			this.SyncKeys();
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06003A69 RID: 14953 RVA: 0x000F38C2 File Offset: 0x000F1AC2
		void IList.Remove(object value)
		{
			this.data.Remove((CodeNamespaceImport)value);
			this.SyncKeys();
		}

		/// <summary>Removes the element at the specified index of the <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		// Token: 0x06003A6A RID: 14954 RVA: 0x000F38DB File Offset: 0x000F1ADB
		void IList.RemoveAt(int index)
		{
			this.data.RemoveAt(index);
			this.SyncKeys();
		}

		// Token: 0x04002BEC RID: 11244
		private ArrayList data = new ArrayList();

		// Token: 0x04002BED RID: 11245
		private Hashtable keys = new Hashtable(StringComparer.OrdinalIgnoreCase);
	}
}
