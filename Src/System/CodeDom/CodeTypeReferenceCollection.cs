using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeTypeReference" /> objects.</summary>
	// Token: 0x02000665 RID: 1637
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeTypeReferenceCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> class.</summary>
		// Token: 0x06003B4B RID: 15179 RVA: 0x000F4FBD File Offset: 0x000F31BD
		public CodeTypeReferenceCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> class containing the elements of the specified source collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> with which to initialize the collection.</param>
		// Token: 0x06003B4C RID: 15180 RVA: 0x000F4FC5 File Offset: 0x000F31C5
		public CodeTypeReferenceCollection(CodeTypeReferenceCollection value)
		{
			this.AddRange(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> class containing the specified array of <see cref="T:System.CodeDom.CodeTypeReference" /> objects.</summary>
		/// <param name="value">An array of <see cref="T:System.CodeDom.CodeTypeReference" /> objects with which to initialize the collection.</param>
		// Token: 0x06003B4D RID: 15181 RVA: 0x000F4FD4 File Offset: 0x000F31D4
		public CodeTypeReferenceCollection(CodeTypeReference[] value)
		{
			this.AddRange(value);
		}

		/// <summary>Gets or sets the <see cref="T:System.CodeDom.CodeTypeReference" /> at the specified index in the collection.</summary>
		/// <param name="index">The index of the collection to access.</param>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> at each valid index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is outside the valid range of indexes for the collection.</exception>
		// Token: 0x17000E48 RID: 3656
		public CodeTypeReference this[int index]
		{
			get
			{
				return (CodeTypeReference)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.CodeDom.CodeTypeReference" /> to the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeReference" /> to add.</param>
		/// <returns>The index at which the new element was inserted.</returns>
		// Token: 0x06003B50 RID: 15184 RVA: 0x000F5005 File Offset: 0x000F3205
		public int Add(CodeTypeReference value)
		{
			return base.List.Add(value);
		}

		/// <summary>Adds a <see cref="T:System.CodeDom.CodeTypeReference" /> to the collection using the specified data type name.</summary>
		/// <param name="value">The name of a data type for which to add a <see cref="T:System.CodeDom.CodeTypeReference" /> to the collection.</param>
		// Token: 0x06003B51 RID: 15185 RVA: 0x000F5013 File Offset: 0x000F3213
		public void Add(string value)
		{
			this.Add(new CodeTypeReference(value));
		}

		/// <summary>Adds a <see cref="T:System.CodeDom.CodeTypeReference" /> to the collection using the specified data type.</summary>
		/// <param name="value">The data type for which to add a <see cref="T:System.CodeDom.CodeTypeReference" /> to the collection.</param>
		// Token: 0x06003B52 RID: 15186 RVA: 0x000F5022 File Offset: 0x000F3222
		public void Add(Type value)
		{
			this.Add(new CodeTypeReference(value));
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.CodeDom.CodeTypeReference" /> array to the end of the collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.CodeDom.CodeTypeReference" /> containing the objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06003B53 RID: 15187 RVA: 0x000F5034 File Offset: 0x000F3234
		public void AddRange(CodeTypeReference[] value)
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

		/// <summary>Adds the contents of the specified <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> to the end of the collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> containing the objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06003B54 RID: 15188 RVA: 0x000F5068 File Offset: 0x000F3268
		public void AddRange(CodeTypeReferenceCollection value)
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

		/// <summary>Gets a value indicating whether the collection contains the specified <see cref="T:System.CodeDom.CodeTypeReference" />.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeReference" /> to search for in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.CodeDom.CodeTypeReference" /> is contained in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003B55 RID: 15189 RVA: 0x000F50A4 File Offset: 0x000F32A4
		public bool Contains(CodeTypeReference value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Copies the items in the collection to the specified one-dimensional <see cref="T:System.Array" /> at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection.</param>
		/// <param name="index">The index of the array at which to begin inserting.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="array" /> parameter is multidimensional.  
		///  -or-  
		///  The number of elements in the <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> is greater than the available space between the index of the target array specified by the <paramref name="index" /> parameter and the end of the target array.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="array" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than the target array's minimum index.</exception>
		// Token: 0x06003B56 RID: 15190 RVA: 0x000F50B2 File Offset: 0x000F32B2
		public void CopyTo(CodeTypeReference[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Gets the index in the collection of the specified <see cref="T:System.CodeDom.CodeTypeReference" />, if it exists in the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeReference" /> to locate in the collection.</param>
		/// <returns>The index of the specified <see cref="T:System.CodeDom.CodeTypeReference" /> in the collection if found; otherwise, -1.</returns>
		// Token: 0x06003B57 RID: 15191 RVA: 0x000F50C1 File Offset: 0x000F32C1
		public int IndexOf(CodeTypeReference value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts a <see cref="T:System.CodeDom.CodeTypeReference" /> into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index where the item should be inserted.</param>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeReference" /> to insert.</param>
		// Token: 0x06003B58 RID: 15192 RVA: 0x000F50CF File Offset: 0x000F32CF
		public void Insert(int index, CodeTypeReference value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Removes the specified <see cref="T:System.CodeDom.CodeTypeReference" /> from the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeReference" /> to remove from the collection.</param>
		/// <exception cref="T:System.ArgumentException">The specified object is not found in the collection.</exception>
		// Token: 0x06003B59 RID: 15193 RVA: 0x000F50DE File Offset: 0x000F32DE
		public void Remove(CodeTypeReference value)
		{
			base.List.Remove(value);
		}
	}
}
