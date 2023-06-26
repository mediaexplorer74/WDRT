using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeStatement" /> objects.</summary>
	// Token: 0x02000656 RID: 1622
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeStatementCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeStatementCollection" /> class.</summary>
		// Token: 0x06003ABD RID: 15037 RVA: 0x000F3E5D File Offset: 0x000F205D
		public CodeStatementCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeStatementCollection" /> class that contains the elements of the specified source collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeStatementCollection" /> object with which to initialize the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06003ABE RID: 15038 RVA: 0x000F3E65 File Offset: 0x000F2065
		public CodeStatementCollection(CodeStatementCollection value)
		{
			this.AddRange(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeStatementCollection" /> class that contains the specified array of <see cref="T:System.CodeDom.CodeStatement" /> objects.</summary>
		/// <param name="value">An array of <see cref="T:System.CodeDom.CodeStatement" /> objects with which to initialize the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06003ABF RID: 15039 RVA: 0x000F3E74 File Offset: 0x000F2074
		public CodeStatementCollection(CodeStatement[] value)
		{
			this.AddRange(value);
		}

		/// <summary>Gets or sets the <see cref="T:System.CodeDom.CodeStatement" /> object at the specified index in the collection.</summary>
		/// <param name="index">The index of the collection to access.</param>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatement" /> at each valid index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is outside the valid range of indexes for the collection.</exception>
		// Token: 0x17000E22 RID: 3618
		public CodeStatement this[int index]
		{
			get
			{
				return (CodeStatement)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.CodeDom.CodeStatement" /> object to the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeStatement" /> object to add.</param>
		/// <returns>The index at which the new element was inserted.</returns>
		// Token: 0x06003AC2 RID: 15042 RVA: 0x000F3EA5 File Offset: 0x000F20A5
		public int Add(CodeStatement value)
		{
			return base.List.Add(value);
		}

		/// <summary>Adds the specified <see cref="T:System.CodeDom.CodeExpression" /> object to the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeExpression" /> object to add.</param>
		/// <returns>The index at which the new element was inserted.</returns>
		// Token: 0x06003AC3 RID: 15043 RVA: 0x000F3EB3 File Offset: 0x000F20B3
		public int Add(CodeExpression value)
		{
			return this.Add(new CodeExpressionStatement(value));
		}

		/// <summary>Adds a set of <see cref="T:System.CodeDom.CodeStatement" /> objects to the collection.</summary>
		/// <param name="value">An array of <see cref="T:System.CodeDom.CodeStatement" /> objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06003AC4 RID: 15044 RVA: 0x000F3EC4 File Offset: 0x000F20C4
		public void AddRange(CodeStatement[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			for (int i = 0; i < value.Length; i++)
			{
				this.Add(value[i]);
			}
		}

		/// <summary>Adds the contents of another <see cref="T:System.CodeDom.CodeStatementCollection" /> object to the end of the collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeStatementCollection" /> object that contains the objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06003AC5 RID: 15045 RVA: 0x000F3EF8 File Offset: 0x000F20F8
		public void AddRange(CodeStatementCollection value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			int count = value.Count;
			for (int i = 0; i < count; i++)
			{
				this.Add(value[i]);
			}
		}

		/// <summary>Gets a value that indicates whether the collection contains the specified <see cref="T:System.CodeDom.CodeStatement" /> object.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeStatement" /> object to search for in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the collection contains the specified object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003AC6 RID: 15046 RVA: 0x000F3F34 File Offset: 0x000F2134
		public bool Contains(CodeStatement value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Copies the elements of the <see cref="T:System.CodeDom.CodeStatementCollection" /> object to a one-dimensional <see cref="T:System.Array" /> instance, starting at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection.</param>
		/// <param name="index">The index of the array at which to begin inserting.</param>
		/// <exception cref="T:System.ArgumentException">The destination array is multidimensional.  
		///  -or-  
		///  The number of elements in the <see cref="T:System.CodeDom.CodeStatementCollection" /> is greater than the available space between the index of the target array specified by the <paramref name="index" /> parameter and the end of the target array.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="array" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than the target array's minimum index.</exception>
		// Token: 0x06003AC7 RID: 15047 RVA: 0x000F3F42 File Offset: 0x000F2142
		public void CopyTo(CodeStatement[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Gets the index of the specified <see cref="T:System.CodeDom.CodeStatement" /> object in the <see cref="T:System.CodeDom.CodeStatementCollection" />, if it exists in the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeStatement" /> to locate in the collection.</param>
		/// <returns>The index of the specified object, if it is found, in the collection; otherwise, -1.</returns>
		// Token: 0x06003AC8 RID: 15048 RVA: 0x000F3F51 File Offset: 0x000F2151
		public int IndexOf(CodeStatement value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts the specified <see cref="T:System.CodeDom.CodeStatement" /> object into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index where the specified object should be inserted.</param>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeStatement" /> object to insert.</param>
		// Token: 0x06003AC9 RID: 15049 RVA: 0x000F3F5F File Offset: 0x000F215F
		public void Insert(int index, CodeStatement value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Removes the specified <see cref="T:System.CodeDom.CodeStatement" /> object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeStatement" /> to remove from the collection.</param>
		/// <exception cref="T:System.ArgumentException">The specified object is not found in the collection.</exception>
		// Token: 0x06003ACA RID: 15050 RVA: 0x000F3F6E File Offset: 0x000F216E
		public void Remove(CodeStatement value)
		{
			base.List.Remove(value);
		}
	}
}
