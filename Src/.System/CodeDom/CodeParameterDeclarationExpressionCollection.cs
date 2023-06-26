using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> objects.</summary>
	// Token: 0x0200064A RID: 1610
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeParameterDeclarationExpressionCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeParameterDeclarationExpressionCollection" /> class.</summary>
		// Token: 0x06003A81 RID: 14977 RVA: 0x000F3AC6 File Offset: 0x000F1CC6
		public CodeParameterDeclarationExpressionCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeParameterDeclarationExpressionCollection" /> class containing the elements of the specified source collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeParameterDeclarationExpressionCollection" /> with which to initialize the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06003A82 RID: 14978 RVA: 0x000F3ACE File Offset: 0x000F1CCE
		public CodeParameterDeclarationExpressionCollection(CodeParameterDeclarationExpressionCollection value)
		{
			this.AddRange(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeParameterDeclarationExpressionCollection" /> class containing the specified array of <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> objects.</summary>
		/// <param name="value">An array of <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> objects with which to initialize the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">one or more objects in the array are <see langword="null" />.</exception>
		// Token: 0x06003A83 RID: 14979 RVA: 0x000F3ADD File Offset: 0x000F1CDD
		public CodeParameterDeclarationExpressionCollection(CodeParameterDeclarationExpression[] value)
		{
			this.AddRange(value);
		}

		/// <summary>Gets or sets the <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> at the specified index in the collection.</summary>
		/// <param name="index">The index of the collection to access.</param>
		/// <returns>A <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> at each valid index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is outside the valid range of indexes for the collection.</exception>
		// Token: 0x17000E12 RID: 3602
		public CodeParameterDeclarationExpression this[int index]
		{
			get
			{
				return (CodeParameterDeclarationExpression)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> to the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> to add.</param>
		/// <returns>The index at which the new element was inserted.</returns>
		// Token: 0x06003A86 RID: 14982 RVA: 0x000F3B0E File Offset: 0x000F1D0E
		public int Add(CodeParameterDeclarationExpression value)
		{
			return base.List.Add(value);
		}

		/// <summary>Copies the elements of the specified array to the end of the collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> containing the objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06003A87 RID: 14983 RVA: 0x000F3B1C File Offset: 0x000F1D1C
		public void AddRange(CodeParameterDeclarationExpression[] value)
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

		/// <summary>Adds the contents of another <see cref="T:System.CodeDom.CodeParameterDeclarationExpressionCollection" /> to the end of the collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeParameterDeclarationExpressionCollection" /> containing the objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06003A88 RID: 14984 RVA: 0x000F3B50 File Offset: 0x000F1D50
		public void AddRange(CodeParameterDeclarationExpressionCollection value)
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

		/// <summary>Gets a value indicating whether the collection contains the specified <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" />.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> to search for in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the collection contains the specified object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003A89 RID: 14985 RVA: 0x000F3B8C File Offset: 0x000F1D8C
		public bool Contains(CodeParameterDeclarationExpression value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Copies the collection objects to a one-dimensional <see cref="T:System.Array" /> instance beginning at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection.</param>
		/// <param name="index">The index of the array at which to begin inserting.</param>
		/// <exception cref="T:System.ArgumentException">The destination array is multidimensional.  
		///  -or-  
		///  The number of elements in the <see cref="T:System.CodeDom.CodeParameterDeclarationExpressionCollection" /> is greater than the available space between the index of the target array specified by the <paramref name="index" /> parameter and the end of the target array.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="array" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than the target array's minimum index.</exception>
		// Token: 0x06003A8A RID: 14986 RVA: 0x000F3B9A File Offset: 0x000F1D9A
		public void CopyTo(CodeParameterDeclarationExpression[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Gets the index in the collection of the specified <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" />, if it exists in the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> to locate in the collection.</param>
		/// <returns>The index in the collection of the specified object, if found; otherwise, -1.</returns>
		// Token: 0x06003A8B RID: 14987 RVA: 0x000F3BA9 File Offset: 0x000F1DA9
		public int IndexOf(CodeParameterDeclarationExpression value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts the specified <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index where the specified object should be inserted.</param>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> to insert.</param>
		// Token: 0x06003A8C RID: 14988 RVA: 0x000F3BB7 File Offset: 0x000F1DB7
		public void Insert(int index, CodeParameterDeclarationExpression value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Removes the specified <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> from the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> to remove from the collection.</param>
		/// <exception cref="T:System.ArgumentException">The specified object is not found in the collection.</exception>
		// Token: 0x06003A8D RID: 14989 RVA: 0x000F3BC6 File Offset: 0x000F1DC6
		public void Remove(CodeParameterDeclarationExpression value)
		{
			base.List.Remove(value);
		}
	}
}
