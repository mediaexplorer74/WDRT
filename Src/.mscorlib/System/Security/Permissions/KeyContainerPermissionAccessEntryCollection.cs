using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Represents a collection of <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> objects. This class cannot be inherited.</summary>
	// Token: 0x02000314 RID: 788
	[ComVisible(true)]
	[Serializable]
	public sealed class KeyContainerPermissionAccessEntryCollection : ICollection, IEnumerable
	{
		// Token: 0x060027E4 RID: 10212 RVA: 0x000923C9 File Offset: 0x000905C9
		private KeyContainerPermissionAccessEntryCollection()
		{
		}

		// Token: 0x060027E5 RID: 10213 RVA: 0x000923D1 File Offset: 0x000905D1
		internal KeyContainerPermissionAccessEntryCollection(KeyContainerPermissionFlags globalFlags)
		{
			this.m_list = new ArrayList();
			this.m_globalFlags = globalFlags;
		}

		/// <summary>Gets the item at the specified index in the collection.</summary>
		/// <param name="index">The zero-based index of the element to access.</param>
		/// <returns>The <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object at the specified index in the collection.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is greater than or equal to the collection count.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="index" /> is negative.</exception>
		// Token: 0x17000516 RID: 1302
		public KeyContainerPermissionAccessEntry this[int index]
		{
			get
			{
				if (index < 0)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
				}
				if (index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				return (KeyContainerPermissionAccessEntry)this.m_list[index];
			}
		}

		/// <summary>Gets the number of items in the collection.</summary>
		/// <returns>The number of <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> objects in the collection.</returns>
		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x060027E7 RID: 10215 RVA: 0x0009243C File Offset: 0x0009063C
		public int Count
		{
			get
			{
				return this.m_list.Count;
			}
		}

		/// <summary>Adds a <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object to the collection.</summary>
		/// <param name="accessEntry">The <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object to add.</param>
		/// <returns>The index at which the new element was inserted.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="accessEntry" /> is <see langword="null" />.</exception>
		// Token: 0x060027E8 RID: 10216 RVA: 0x0009244C File Offset: 0x0009064C
		public int Add(KeyContainerPermissionAccessEntry accessEntry)
		{
			if (accessEntry == null)
			{
				throw new ArgumentNullException("accessEntry");
			}
			int num = this.m_list.IndexOf(accessEntry);
			if (num != -1)
			{
				((KeyContainerPermissionAccessEntry)this.m_list[num]).Flags &= accessEntry.Flags;
				return num;
			}
			if (accessEntry.Flags != this.m_globalFlags)
			{
				return this.m_list.Add(accessEntry);
			}
			return -1;
		}

		/// <summary>Removes all the <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> objects from the collection.</summary>
		// Token: 0x060027E9 RID: 10217 RVA: 0x000924B9 File Offset: 0x000906B9
		public void Clear()
		{
			this.m_list.Clear();
		}

		/// <summary>Gets the index in the collection of the specified <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object, if it exists in the collection.</summary>
		/// <param name="accessEntry">The <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object to locate.</param>
		/// <returns>The index of the specified <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object in the collection, or -1 if no match is found.</returns>
		// Token: 0x060027EA RID: 10218 RVA: 0x000924C6 File Offset: 0x000906C6
		public int IndexOf(KeyContainerPermissionAccessEntry accessEntry)
		{
			return this.m_list.IndexOf(accessEntry);
		}

		/// <summary>Removes the specified <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object from thecollection.</summary>
		/// <param name="accessEntry">The <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="accessEntry" /> is <see langword="null" />.</exception>
		// Token: 0x060027EB RID: 10219 RVA: 0x000924D4 File Offset: 0x000906D4
		public void Remove(KeyContainerPermissionAccessEntry accessEntry)
		{
			if (accessEntry == null)
			{
				throw new ArgumentNullException("accessEntry");
			}
			this.m_list.Remove(accessEntry);
		}

		/// <summary>Returns a <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator" /> object that can be used to iterate through the objects in the collection.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x060027EC RID: 10220 RVA: 0x000924F0 File Offset: 0x000906F0
		public KeyContainerPermissionAccessEntryEnumerator GetEnumerator()
		{
			return new KeyContainerPermissionAccessEntryEnumerator(this);
		}

		/// <summary>Returns a <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator" /> object that can be used to iterate through the objects in the collection.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x060027ED RID: 10221 RVA: 0x000924F8 File Offset: 0x000906F8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new KeyContainerPermissionAccessEntryEnumerator(this);
		}

		/// <summary>Copies the elements of the collection to a compatible one-dimensional array, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the current collection.</param>
		/// <param name="index">The index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="array" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the collection is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x060027EE RID: 10222 RVA: 0x00092500 File Offset: 0x00090700
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
			}
			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (index + this.Count > array.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index);
				index++;
			}
		}

		/// <summary>Copies the elements of the collection to a compatible one-dimensional array, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> array that is the destination of the elements copied from the current collection.</param>
		/// <param name="index">The index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="array" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the collection is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x060027EF RID: 10223 RVA: 0x0009259A File Offset: 0x0009079A
		public void CopyTo(KeyContainerPermissionAccessEntry[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		/// <summary>Gets a value indicating whether the collection is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x060027F0 RID: 10224 RVA: 0x000925A4 File Offset: 0x000907A4
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x060027F1 RID: 10225 RVA: 0x000925A7 File Offset: 0x000907A7
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x04000F77 RID: 3959
		private ArrayList m_list;

		// Token: 0x04000F78 RID: 3960
		private KeyContainerPermissionFlags m_globalFlags;
	}
}
