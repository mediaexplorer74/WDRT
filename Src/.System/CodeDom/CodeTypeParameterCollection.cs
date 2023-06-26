using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeTypeParameter" /> objects.</summary>
	// Token: 0x02000662 RID: 1634
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeTypeParameterCollection : CollectionBase
	{
		/// <summary>Initializes a new, empty instance of the <see cref="T:System.CodeDom.CodeTypeParameterCollection" /> class.</summary>
		// Token: 0x06003B25 RID: 15141 RVA: 0x000F47F5 File Offset: 0x000F29F5
		public CodeTypeParameterCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeParameterCollection" /> class containing the elements of the specified source collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeTypeParameterCollection" /> with which to initialize the collection.</param>
		// Token: 0x06003B26 RID: 15142 RVA: 0x000F47FD File Offset: 0x000F29FD
		public CodeTypeParameterCollection(CodeTypeParameterCollection value)
		{
			this.AddRange(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeParameterCollection" /> class containing the specified array of <see cref="T:System.CodeDom.CodeTypeParameter" /> objects.</summary>
		/// <param name="value">An array of <see cref="T:System.CodeDom.CodeTypeParameter" /> objects with which to initialize the collection.</param>
		// Token: 0x06003B27 RID: 15143 RVA: 0x000F480C File Offset: 0x000F2A0C
		public CodeTypeParameterCollection(CodeTypeParameter[] value)
		{
			this.AddRange(value);
		}

		/// <summary>Gets or sets the <see cref="T:System.CodeDom.CodeTypeParameter" /> object at the specified index in the collection.</summary>
		/// <param name="index">The zero-based index of the collection object to access.</param>
		/// <returns>The <see cref="T:System.CodeDom.CodeTypeParameter" /> object at the specified index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the valid range of indexes for the collection.</exception>
		// Token: 0x17000E40 RID: 3648
		public CodeTypeParameter this[int index]
		{
			get
			{
				return (CodeTypeParameter)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.CodeDom.CodeTypeParameter" /> object to the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeParameter" /> to add.</param>
		/// <returns>The zero-based index at which the new element was inserted.</returns>
		// Token: 0x06003B2A RID: 15146 RVA: 0x000F483D File Offset: 0x000F2A3D
		public int Add(CodeTypeParameter value)
		{
			return base.List.Add(value);
		}

		/// <summary>Adds the specified <see cref="T:System.CodeDom.CodeTypeParameter" /> object to the collection using the specified data type name.</summary>
		/// <param name="value">The name of a data type for which to add the <see cref="T:System.CodeDom.CodeTypeParameter" /> object to the collection.</param>
		// Token: 0x06003B2B RID: 15147 RVA: 0x000F484B File Offset: 0x000F2A4B
		public void Add(string value)
		{
			this.Add(new CodeTypeParameter(value));
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.CodeDom.CodeTypeParameter" /> array to the end of the collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.CodeDom.CodeTypeParameter" /> containing the objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06003B2C RID: 15148 RVA: 0x000F485C File Offset: 0x000F2A5C
		public void AddRange(CodeTypeParameter[] value)
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

		/// <summary>Copies the elements of the specified <see cref="T:System.CodeDom.CodeTypeParameterCollection" /> to the end of the collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeTypeParameterCollection" /> containing the <see cref="T:System.CodeDom.CodeTypeParameter" /> objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06003B2D RID: 15149 RVA: 0x000F4890 File Offset: 0x000F2A90
		public void AddRange(CodeTypeParameterCollection value)
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

		/// <summary>Determines whether the collection contains the specified <see cref="T:System.CodeDom.CodeTypeParameter" /> object.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeParameter" /> object to search for in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.CodeDom.CodeTypeParameter" /> object is contained in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003B2E RID: 15150 RVA: 0x000F48CC File Offset: 0x000F2ACC
		public bool Contains(CodeTypeParameter value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Copies the items in the collection to the specified one-dimensional <see cref="T:System.Array" /> at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection.</param>
		/// <param name="index">The index of the array at which to begin inserting.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the <see cref="T:System.CodeDom.CodeTypeParameterCollection" /> is greater than the available space between the index of the target array specified by <paramref name="index" /> and the end of the target array.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the target array's lowest index.</exception>
		// Token: 0x06003B2F RID: 15151 RVA: 0x000F48DA File Offset: 0x000F2ADA
		public void CopyTo(CodeTypeParameter[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Gets the index in the collection of the specified <see cref="T:System.CodeDom.CodeTypeParameter" /> object, if it exists in the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeParameter" /> object to locate in the collection.</param>
		/// <returns>The zero-based index of the specified <see cref="T:System.CodeDom.CodeTypeParameter" /> object in the collection if found; otherwise, -1.</returns>
		// Token: 0x06003B30 RID: 15152 RVA: 0x000F48E9 File Offset: 0x000F2AE9
		public int IndexOf(CodeTypeParameter value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts the specified <see cref="T:System.CodeDom.CodeTypeParameter" /> object into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index at which to insert the item.</param>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeParameter" /> object to insert.</param>
		// Token: 0x06003B31 RID: 15153 RVA: 0x000F48F7 File Offset: 0x000F2AF7
		public void Insert(int index, CodeTypeParameter value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Removes the specified <see cref="T:System.CodeDom.CodeTypeParameter" /> object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeParameter" /> object to remove from the collection.</param>
		/// <exception cref="T:System.ArgumentException">The specified object is not found in the collection.</exception>
		// Token: 0x06003B32 RID: 15154 RVA: 0x000F4906 File Offset: 0x000F2B06
		public void Remove(CodeTypeParameter value)
		{
			base.List.Remove(value);
		}
	}
}
