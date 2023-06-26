using System;
using System.Collections;
using System.Security.Permissions;

namespace System.CodeDom.Compiler
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.Compiler.CompilerError" /> objects.</summary>
	// Token: 0x02000675 RID: 1653
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[Serializable]
	public class CompilerErrorCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerErrorCollection" /> class.</summary>
		// Token: 0x06003CC1 RID: 15553 RVA: 0x000FA579 File Offset: 0x000F8779
		public CompilerErrorCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerErrorCollection" /> class that contains the contents of the specified <see cref="T:System.CodeDom.Compiler.CompilerErrorCollection" />.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.Compiler.CompilerErrorCollection" /> object with which to initialize the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06003CC2 RID: 15554 RVA: 0x000FA581 File Offset: 0x000F8781
		public CompilerErrorCollection(CompilerErrorCollection value)
		{
			this.AddRange(value);
		}

		/// <summary>Initializes a new instance of <see cref="T:System.CodeDom.Compiler.CompilerErrorCollection" /> that contains the specified array of <see cref="T:System.CodeDom.Compiler.CompilerError" /> objects.</summary>
		/// <param name="value">An array of <see cref="T:System.CodeDom.Compiler.CompilerError" /> objects to initialize the collection with.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06003CC3 RID: 15555 RVA: 0x000FA590 File Offset: 0x000F8790
		public CompilerErrorCollection(CompilerError[] value)
		{
			this.AddRange(value);
		}

		/// <summary>Gets or sets the <see cref="T:System.CodeDom.Compiler.CompilerError" /> at the specified index.</summary>
		/// <param name="index">The zero-based index of the entry to locate in the collection.</param>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.CompilerError" /> at each valid index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index value indicated by the <paramref name="index" /> parameter is outside the valid range of indexes for the collection.</exception>
		// Token: 0x17000E70 RID: 3696
		public CompilerError this[int index]
		{
			get
			{
				return (CompilerError)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.CodeDom.Compiler.CompilerError" /> object to the error collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.Compiler.CompilerError" /> object to add.</param>
		/// <returns>The index at which the new element was inserted.</returns>
		// Token: 0x06003CC6 RID: 15558 RVA: 0x000FA5C1 File Offset: 0x000F87C1
		public int Add(CompilerError value)
		{
			return base.List.Add(value);
		}

		/// <summary>Copies the elements of an array to the end of the error collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.CodeDom.Compiler.CompilerError" /> that contains the objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06003CC7 RID: 15559 RVA: 0x000FA5D0 File Offset: 0x000F87D0
		public void AddRange(CompilerError[] value)
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

		/// <summary>Adds the contents of the specified compiler error collection to the end of the error collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.Compiler.CompilerErrorCollection" /> object that contains the objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06003CC8 RID: 15560 RVA: 0x000FA604 File Offset: 0x000F8804
		public void AddRange(CompilerErrorCollection value)
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

		/// <summary>Gets a value that indicates whether the collection contains the specified <see cref="T:System.CodeDom.Compiler.CompilerError" /> object.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.Compiler.CompilerError" /> to locate.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.CodeDom.Compiler.CompilerError" /> is contained in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003CC9 RID: 15561 RVA: 0x000FA640 File Offset: 0x000F8840
		public bool Contains(CompilerError value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Copies the collection values to a one-dimensional <see cref="T:System.Array" /> instance at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from <see cref="T:System.CodeDom.Compiler.CompilerErrorCollection" />.</param>
		/// <param name="index">The index in the array at which to start copying.</param>
		/// <exception cref="T:System.ArgumentException">The array indicated by the <paramref name="array" /> parameter is multidimensional.  
		///  -or-  
		///  The number of elements in the <see cref="T:System.CodeDom.Compiler.CompilerErrorCollection" /> is greater than the available space between the index value of the <paramref name="arrayIndex" /> parameter in the array indicated by the <paramref name="array" /> parameter and the end of the array indicated by the <paramref name="array" /> parameter.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="array" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than the lowbound of the array indicated by the <paramref name="array" /> parameter.</exception>
		// Token: 0x06003CCA RID: 15562 RVA: 0x000FA64E File Offset: 0x000F884E
		public void CopyTo(CompilerError[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Gets a value that indicates whether the collection contains errors.</summary>
		/// <returns>
		///   <see langword="true" /> if the collection contains errors; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E71 RID: 3697
		// (get) Token: 0x06003CCB RID: 15563 RVA: 0x000FA660 File Offset: 0x000F8860
		public bool HasErrors
		{
			get
			{
				if (base.Count > 0)
				{
					foreach (object obj in this)
					{
						CompilerError compilerError = (CompilerError)obj;
						if (!compilerError.IsWarning)
						{
							return true;
						}
					}
					return false;
				}
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the collection contains warnings.</summary>
		/// <returns>
		///   <see langword="true" /> if the collection contains warnings; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x06003CCC RID: 15564 RVA: 0x000FA6C8 File Offset: 0x000F88C8
		public bool HasWarnings
		{
			get
			{
				if (base.Count > 0)
				{
					foreach (object obj in this)
					{
						CompilerError compilerError = (CompilerError)obj;
						if (compilerError.IsWarning)
						{
							return true;
						}
					}
					return false;
				}
				return false;
			}
		}

		/// <summary>Gets the index of the specified <see cref="T:System.CodeDom.Compiler.CompilerError" /> object in the collection, if it exists in the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.Compiler.CompilerError" /> to locate.</param>
		/// <returns>The index of the specified <see cref="T:System.CodeDom.Compiler.CompilerError" /> in the <see cref="T:System.CodeDom.Compiler.CompilerErrorCollection" />, if found; otherwise, -1.</returns>
		// Token: 0x06003CCD RID: 15565 RVA: 0x000FA730 File Offset: 0x000F8930
		public int IndexOf(CompilerError value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts the specified <see cref="T:System.CodeDom.Compiler.CompilerError" /> into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index where the compiler error should be inserted.</param>
		/// <param name="value">The <see cref="T:System.CodeDom.Compiler.CompilerError" /> to insert.</param>
		// Token: 0x06003CCE RID: 15566 RVA: 0x000FA73E File Offset: 0x000F893E
		public void Insert(int index, CompilerError value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Removes a specific <see cref="T:System.CodeDom.Compiler.CompilerError" /> from the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.Compiler.CompilerError" /> to remove from the <see cref="T:System.CodeDom.Compiler.CompilerErrorCollection" />.</param>
		/// <exception cref="T:System.ArgumentException">The specified object is not found in the collection.</exception>
		// Token: 0x06003CCF RID: 15567 RVA: 0x000FA74D File Offset: 0x000F894D
		public void Remove(CompilerError value)
		{
			base.List.Remove(value);
		}
	}
}
