using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeTypeDeclaration" /> objects.</summary>
	// Token: 0x0200065C RID: 1628
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeTypeDeclarationCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeDeclarationCollection" /> class.</summary>
		// Token: 0x06003AEC RID: 15084 RVA: 0x000F4395 File Offset: 0x000F2595
		public CodeTypeDeclarationCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeDeclarationCollection" /> class that contains the elements of the specified source collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeTypeDeclarationCollection" /> object with which to initialize the collection.</param>
		// Token: 0x06003AED RID: 15085 RVA: 0x000F439D File Offset: 0x000F259D
		public CodeTypeDeclarationCollection(CodeTypeDeclarationCollection value)
		{
			this.AddRange(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeDeclarationCollection" /> class that contains the specified array of <see cref="T:System.CodeDom.CodeTypeDeclaration" /> objects.</summary>
		/// <param name="value">An array of <see cref="T:System.CodeDom.CodeTypeDeclaration" /> objects with which to initialize the collection.</param>
		// Token: 0x06003AEE RID: 15086 RVA: 0x000F43AC File Offset: 0x000F25AC
		public CodeTypeDeclarationCollection(CodeTypeDeclaration[] value)
		{
			this.AddRange(value);
		}

		/// <summary>Gets or sets the <see cref="T:System.CodeDom.CodeTypeDeclaration" /> object at the specified index in the collection.</summary>
		/// <param name="index">The index of the collection to access.</param>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeDeclaration" /> at each valid index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is outside the valid range of indexes for the collection.</exception>
		// Token: 0x17000E30 RID: 3632
		public CodeTypeDeclaration this[int index]
		{
			get
			{
				return (CodeTypeDeclaration)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.CodeDom.CodeTypeDeclaration" /> object to the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeDeclaration" /> object to add.</param>
		/// <returns>The index at which the new element was inserted.</returns>
		// Token: 0x06003AF1 RID: 15089 RVA: 0x000F43DD File Offset: 0x000F25DD
		public int Add(CodeTypeDeclaration value)
		{
			return base.List.Add(value);
		}

		/// <summary>Copies the elements of the specified array to the end of the collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.CodeDom.CodeTypeDeclaration" /> that contains the objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06003AF2 RID: 15090 RVA: 0x000F43EC File Offset: 0x000F25EC
		public void AddRange(CodeTypeDeclaration[] value)
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

		/// <summary>Adds the contents of another <see cref="T:System.CodeDom.CodeTypeDeclarationCollection" /> object to the end of the collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeTypeDeclarationCollection" /> object that contains the objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06003AF3 RID: 15091 RVA: 0x000F4420 File Offset: 0x000F2620
		public void AddRange(CodeTypeDeclarationCollection value)
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

		/// <summary>Gets a value that indicates whether the collection contains the specified <see cref="T:System.CodeDom.CodeTypeDeclaration" /> object.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeDeclaration" /> object to search for in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the collection contains the specified object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003AF4 RID: 15092 RVA: 0x000F445C File Offset: 0x000F265C
		public bool Contains(CodeTypeDeclaration value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Copies the elements in the <see cref="T:System.CodeDom.CodeTypeDeclarationCollection" /> object to a one-dimensional <see cref="T:System.Array" /> instance, starting at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection.</param>
		/// <param name="index">The index of the array at which to begin inserting.</param>
		/// <exception cref="T:System.ArgumentException">The destination array is multidimensional.  
		///  -or-  
		///  The number of elements in the <see cref="T:System.CodeDom.CodeTypeDeclarationCollection" /> is greater than the available space between the index of the target array specified by the <paramref name="index" /> parameter and the end of the target array.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="array" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than the target array's minimum index.</exception>
		// Token: 0x06003AF5 RID: 15093 RVA: 0x000F446A File Offset: 0x000F266A
		public void CopyTo(CodeTypeDeclaration[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Gets the index of the specified <see cref="T:System.CodeDom.CodeTypeDeclaration" /> object in the <see cref="T:System.CodeDom.CodeTypeDeclarationCollection" />, if it exists in the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeDeclaration" /> to locate in the collection.</param>
		/// <returns>The index of the specified object, if it is found, in the collection; otherwise, -1.</returns>
		// Token: 0x06003AF6 RID: 15094 RVA: 0x000F4479 File Offset: 0x000F2679
		public int IndexOf(CodeTypeDeclaration value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts the specified <see cref="T:System.CodeDom.CodeTypeDeclaration" /> object into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index where the specified object should be inserted.</param>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeDeclaration" /> object to insert.</param>
		// Token: 0x06003AF7 RID: 15095 RVA: 0x000F4487 File Offset: 0x000F2687
		public void Insert(int index, CodeTypeDeclaration value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Removes the specified <see cref="T:System.CodeDom.CodeTypeDeclaration" /> object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeDeclaration" /> to remove from the collection.</param>
		/// <exception cref="T:System.ArgumentException">The specified object is not found in the collection.</exception>
		// Token: 0x06003AF8 RID: 15096 RVA: 0x000F4496 File Offset: 0x000F2696
		public void Remove(CodeTypeDeclaration value)
		{
			base.List.Remove(value);
		}
	}
}
