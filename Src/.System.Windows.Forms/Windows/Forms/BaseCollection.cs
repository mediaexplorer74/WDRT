using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides the base functionality for creating data-related collections in the <see cref="N:System.Windows.Forms" /> namespace.</summary>
	// Token: 0x0200012D RID: 301
	public class BaseCollection : MarshalByRefObject, ICollection, IEnumerable
	{
		/// <summary>Gets the total number of elements in the collection.</summary>
		/// <returns>The total number of elements in the collection.</returns>
		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x0001E442 File Offset: 0x0001C642
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual int Count
		{
			get
			{
				return this.List.Count;
			}
		}

		/// <summary>Copies all the elements of the current one-dimensional <see cref="T:System.Array" /> to the specified one-dimensional <see cref="T:System.Array" /> starting at the specified destination <see cref="T:System.Array" /> index.</summary>
		/// <param name="ar">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the current <see langword="Array" />.</param>
		/// <param name="index">The zero-based relative index in <paramref name="ar" /> at which copying begins.</param>
		// Token: 0x06000AAF RID: 2735 RVA: 0x0001E44F File Offset: 0x0001C64F
		public void CopyTo(Array ar, int index)
		{
			this.List.CopyTo(ar, index);
		}

		/// <summary>Gets the object that enables iterating through the members of the collection.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerator" /> interface.</returns>
		// Token: 0x06000AB0 RID: 2736 RVA: 0x0001E45E File Offset: 0x0001C65E
		public IEnumerator GetEnumerator()
		{
			return this.List.GetEnumerator();
		}

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>This property is always <see langword="false" />.</returns>
		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x0001180C File Offset: 0x0000FA0C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized.</summary>
		/// <returns>This property always returns <see langword="false" />.</returns>
		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x0001180C File Offset: 0x0000FA0C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.BaseCollection" />.</summary>
		/// <returns>An object that can be used to synchronize the <see cref="T:System.Windows.Forms.BaseCollection" />.</returns>
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x00006A49 File Offset: 0x00004C49
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets the list of elements contained in the <see cref="T:System.Windows.Forms.BaseCollection" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> containing the elements of the collection. This property returns <see langword="null" /> unless overridden in a derived class.</returns>
		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x00015C90 File Offset: 0x00013E90
		protected virtual ArrayList List
		{
			get
			{
				return null;
			}
		}
	}
}
