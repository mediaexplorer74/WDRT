using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeCommentStatement" /> objects.</summary>
	// Token: 0x02000627 RID: 1575
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeCommentStatementCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCommentStatementCollection" /> class.</summary>
		// Token: 0x06003974 RID: 14708 RVA: 0x000F2458 File Offset: 0x000F0658
		public CodeCommentStatementCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCommentStatementCollection" /> class containing the elements of the specified source collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeCommentStatementCollection" /> with which to initialize the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06003975 RID: 14709 RVA: 0x000F2460 File Offset: 0x000F0660
		public CodeCommentStatementCollection(CodeCommentStatementCollection value)
		{
			this.AddRange(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCommentStatementCollection" /> class containing the specified array of <see cref="T:System.CodeDom.CodeCommentStatement" /> objects.</summary>
		/// <param name="value">An array of <see cref="T:System.CodeDom.CodeCommentStatement" /> objects with which to initialize the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">One or more objects in the array are <see langword="null" />.</exception>
		// Token: 0x06003976 RID: 14710 RVA: 0x000F246F File Offset: 0x000F066F
		public CodeCommentStatementCollection(CodeCommentStatement[] value)
		{
			this.AddRange(value);
		}

		/// <summary>Gets or sets the <see cref="T:System.CodeDom.CodeCommentStatement" /> object at the specified index in the collection.</summary>
		/// <param name="index">The index of the collection to access.</param>
		/// <returns>A <see cref="T:System.CodeDom.CodeCommentStatement" /> object at each valid index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is outside the valid range of indexes for the collection.</exception>
		// Token: 0x17000DBD RID: 3517
		public CodeCommentStatement this[int index]
		{
			get
			{
				return (CodeCommentStatement)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.CodeDom.CodeCommentStatement" /> object to the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeCommentStatement" /> object to add.</param>
		/// <returns>The index at which the new element was inserted.</returns>
		// Token: 0x06003979 RID: 14713 RVA: 0x000F24A0 File Offset: 0x000F06A0
		public int Add(CodeCommentStatement value)
		{
			return base.List.Add(value);
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.CodeDom.CodeCommentStatement" /> array to the end of the collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.CodeDom.CodeCommentStatement" /> that contains the objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x0600397A RID: 14714 RVA: 0x000F24B0 File Offset: 0x000F06B0
		public void AddRange(CodeCommentStatement[] value)
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

		/// <summary>Copies the contents of another <see cref="T:System.CodeDom.CodeCommentStatementCollection" /> object to the end of the collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeCommentStatementCollection" /> that contains the objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x0600397B RID: 14715 RVA: 0x000F24E4 File Offset: 0x000F06E4
		public void AddRange(CodeCommentStatementCollection value)
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

		/// <summary>Gets a value that indicates whether the collection contains the specified <see cref="T:System.CodeDom.CodeCommentStatement" /> object.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeCommentStatement" /> to search for in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the collection contains the specified object; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600397C RID: 14716 RVA: 0x000F2520 File Offset: 0x000F0720
		public bool Contains(CodeCommentStatement value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Copies the collection objects to the specified one-dimensional <see cref="T:System.Array" /> beginning at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection.</param>
		/// <param name="index">The index of the array at which to begin inserting.</param>
		/// <exception cref="T:System.ArgumentException">The destination array is multidimensional.  
		///  -or-  
		///  The number of elements in the <see cref="T:System.CodeDom.CodeCommentStatementCollection" /> is greater than the available space between the index of the target array specified by the <paramref name="index" /> parameter and the end of the target array.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="array" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than the target array's minimum index.</exception>
		// Token: 0x0600397D RID: 14717 RVA: 0x000F252E File Offset: 0x000F072E
		public void CopyTo(CodeCommentStatement[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Gets the index of the specified <see cref="T:System.CodeDom.CodeCommentStatement" /> object in the collection, if it exists in the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeCommentStatement" /> object to locate.</param>
		/// <returns>The index of the specified object, if found, in the collection; otherwise, -1.</returns>
		// Token: 0x0600397E RID: 14718 RVA: 0x000F253D File Offset: 0x000F073D
		public int IndexOf(CodeCommentStatement value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts a <see cref="T:System.CodeDom.CodeCommentStatement" /> object into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index where the item should be inserted.</param>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeCommentStatement" /> object to insert.</param>
		// Token: 0x0600397F RID: 14719 RVA: 0x000F254B File Offset: 0x000F074B
		public void Insert(int index, CodeCommentStatement value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Removes the specified <see cref="T:System.CodeDom.CodeCommentStatement" /> object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeCommentStatement" /> object to remove from the collection.</param>
		/// <exception cref="T:System.ArgumentException">The specified object is not found in the collection.</exception>
		// Token: 0x06003980 RID: 14720 RVA: 0x000F255A File Offset: 0x000F075A
		public void Remove(CodeCommentStatement value)
		{
			base.List.Remove(value);
		}
	}
}
