using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeDirective" /> objects.</summary>
	// Token: 0x02000630 RID: 1584
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeDirectiveCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDirectiveCollection" /> class.</summary>
		// Token: 0x060039AA RID: 14762 RVA: 0x000F2847 File Offset: 0x000F0A47
		public CodeDirectiveCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDirectiveCollection" /> class with the elements in the specified code directive collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object with which to initialize the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x060039AB RID: 14763 RVA: 0x000F284F File Offset: 0x000F0A4F
		public CodeDirectiveCollection(CodeDirectiveCollection value)
		{
			this.AddRange(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDirectiveCollection" /> class with the code directive objects in the specified array.</summary>
		/// <param name="value">An array of <see cref="T:System.CodeDom.CodeDirective" /> objects with which to initialize the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x060039AC RID: 14764 RVA: 0x000F285E File Offset: 0x000F0A5E
		public CodeDirectiveCollection(CodeDirective[] value)
		{
			this.AddRange(value);
		}

		/// <summary>Gets or sets the <see cref="T:System.CodeDom.CodeDirective" /> object at the specified index in the collection.</summary>
		/// <param name="index">The index position to access.</param>
		/// <returns>The <see cref="T:System.CodeDom.CodeDirective" /> at the index position.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the valid range of index positions for the collection.</exception>
		// Token: 0x17000DD0 RID: 3536
		public CodeDirective this[int index]
		{
			get
			{
				return (CodeDirective)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.CodeDom.CodeDirective" /> object to the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeDirective" /> object to add.</param>
		/// <returns>The index position at which the new element was inserted.</returns>
		// Token: 0x060039AF RID: 14767 RVA: 0x000F288F File Offset: 0x000F0A8F
		public int Add(CodeDirective value)
		{
			return base.List.Add(value);
		}

		/// <summary>Adds an array of <see cref="T:System.CodeDom.CodeDirective" /> objects to the end of the collection.</summary>
		/// <param name="value">An array of <see cref="T:System.CodeDom.CodeDirective" /> objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x060039B0 RID: 14768 RVA: 0x000F28A0 File Offset: 0x000F0AA0
		public void AddRange(CodeDirective[] value)
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

		/// <summary>Adds the contents of the specified <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object to the end of the collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing the <see cref="T:System.CodeDom.CodeDirective" /> objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x060039B1 RID: 14769 RVA: 0x000F28D4 File Offset: 0x000F0AD4
		public void AddRange(CodeDirectiveCollection value)
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

		/// <summary>Gets a value indicating whether the collection contains the specified <see cref="T:System.CodeDom.CodeDirective" /> object.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeDirective" /> object to search for in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the collection contains the specified object; otherwise, <see langword="false" />.</returns>
		// Token: 0x060039B2 RID: 14770 RVA: 0x000F2910 File Offset: 0x000F0B10
		public bool Contains(CodeDirective value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Copies the contents of the collection to a one-dimensional array beginning at the specified index.</summary>
		/// <param name="array">An array of type <see cref="T:System.CodeDom.CodeDirective" /> that is the destination of the values copied from the collection.</param>
		/// <param name="index">The index in the array at which to begin inserting collection objects.</param>
		/// <exception cref="T:System.ArgumentException">The destination array is multidimensional.  
		///  -or-  
		///  The number of elements in the <see cref="T:System.CodeDom.CodeDirectiveCollection" /> is greater than the available space between the index of the target array specified by <paramref name="index" /> and the end of the target array.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the target array's minimum index.</exception>
		// Token: 0x060039B3 RID: 14771 RVA: 0x000F291E File Offset: 0x000F0B1E
		public void CopyTo(CodeDirective[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Gets the index in the collection of the specified <see cref="T:System.CodeDom.CodeDirective" /> object, if it exists in the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeDirective" /> object to locate in the collection.</param>
		/// <returns>The index position in the collection of the specified object, if found; otherwise, -1.</returns>
		// Token: 0x060039B4 RID: 14772 RVA: 0x000F292D File Offset: 0x000F0B2D
		public int IndexOf(CodeDirective value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts the specified <see cref="T:System.CodeDom.CodeDirective" /> object into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index position where the specified object should be inserted.</param>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeDirective" /> object to insert.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.  
		/// -or-  
		/// <paramref name="index" /> is greater than <see cref="P:System.Collections.CollectionBase.Count" />.</exception>
		// Token: 0x060039B5 RID: 14773 RVA: 0x000F293B File Offset: 0x000F0B3B
		public void Insert(int index, CodeDirective value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Removes the specified <see cref="T:System.CodeDom.CodeDirective" /> object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeDirective" /> object to remove from the collection.</param>
		/// <exception cref="T:System.ArgumentException">The specified object is not found in the collection.</exception>
		// Token: 0x060039B6 RID: 14774 RVA: 0x000F294A File Offset: 0x000F0B4A
		public void Remove(CodeDirective value)
		{
			base.List.Remove(value);
		}
	}
}
