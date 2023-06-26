using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Contains a strongly typed collection of <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> objects.</summary>
	// Token: 0x020004D3 RID: 1235
	[Serializable]
	public class EventLogPermissionEntryCollection : CollectionBase
	{
		// Token: 0x06002E76 RID: 11894 RVA: 0x000D18A4 File Offset: 0x000CFAA4
		internal EventLogPermissionEntryCollection(EventLogPermission owner, ResourcePermissionBaseEntry[] entries)
		{
			this.owner = owner;
			for (int i = 0; i < entries.Length; i++)
			{
				base.InnerList.Add(new EventLogPermissionEntry(entries[i]));
			}
		}

		/// <summary>Gets or sets the object at a specified index.</summary>
		/// <param name="index">The zero-based index into the collection.</param>
		/// <returns>The <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> that exists at the specified index.</returns>
		// Token: 0x17000B3B RID: 2875
		public EventLogPermissionEntry this[int index]
		{
			get
			{
				return (EventLogPermissionEntry)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds a specified <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> to this collection.</summary>
		/// <param name="value">The <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> to add.</param>
		/// <returns>The zero-based index of the added <see cref="T:System.Diagnostics.EventLogPermissionEntry" />.</returns>
		// Token: 0x06002E79 RID: 11897 RVA: 0x000D1902 File Offset: 0x000CFB02
		public int Add(EventLogPermissionEntry value)
		{
			return base.List.Add(value);
		}

		/// <summary>Appends a set of specified permission entries to this collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> objects that contains the permission entries to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06002E7A RID: 11898 RVA: 0x000D1910 File Offset: 0x000CFB10
		public void AddRange(EventLogPermissionEntry[] value)
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

		/// <summary>Appends a set of specified permission entries to this collection.</summary>
		/// <param name="value">A <see cref="T:System.Diagnostics.EventLogPermissionEntryCollection" /> that contains the permission entries to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06002E7B RID: 11899 RVA: 0x000D1944 File Offset: 0x000CFB44
		public void AddRange(EventLogPermissionEntryCollection value)
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

		/// <summary>Determines whether this collection contains a specified <see cref="T:System.Diagnostics.EventLogPermissionEntry" />.</summary>
		/// <param name="value">The <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> to find.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> belongs to this collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002E7C RID: 11900 RVA: 0x000D1980 File Offset: 0x000CFB80
		public bool Contains(EventLogPermissionEntry value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Copies the permission entries from this collection to an array, starting at a particular index of the array.</summary>
		/// <param name="array">An array of type <see cref="T:System.Diagnostics.EventLogPermissionEntry" /> that receives this collection's permission entries.</param>
		/// <param name="index">The zero-based index at which to begin copying the permission entries.</param>
		// Token: 0x06002E7D RID: 11901 RVA: 0x000D198E File Offset: 0x000CFB8E
		public void CopyTo(EventLogPermissionEntry[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Determines the index of a specified permission entry in this collection.</summary>
		/// <param name="value">The permission entry to search for.</param>
		/// <returns>The zero-based index of the specified permission entry, or -1 if the permission entry was not found in the collection.</returns>
		// Token: 0x06002E7E RID: 11902 RVA: 0x000D199D File Offset: 0x000CFB9D
		public int IndexOf(EventLogPermissionEntry value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts a permission entry into this collection at a specified index.</summary>
		/// <param name="index">The zero-based index of the collection at which to insert the permission entry.</param>
		/// <param name="value">The permission entry to insert into this collection.</param>
		// Token: 0x06002E7F RID: 11903 RVA: 0x000D19AB File Offset: 0x000CFBAB
		public void Insert(int index, EventLogPermissionEntry value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Removes a specified permission entry from this collection.</summary>
		/// <param name="value">The permission entry to remove.</param>
		// Token: 0x06002E80 RID: 11904 RVA: 0x000D19BA File Offset: 0x000CFBBA
		public void Remove(EventLogPermissionEntry value)
		{
			base.List.Remove(value);
		}

		/// <summary>Performs additional custom processes after clearing the contents of the collection.</summary>
		// Token: 0x06002E81 RID: 11905 RVA: 0x000D19C8 File Offset: 0x000CFBC8
		protected override void OnClear()
		{
			this.owner.Clear();
		}

		/// <summary>Performs additional custom processes before a new permission entry is inserted into the collection.</summary>
		/// <param name="index">The zero-based index at which to insert <paramref name="value" />.</param>
		/// <param name="value">The new value of the permission entry at <paramref name="index" />.</param>
		// Token: 0x06002E82 RID: 11906 RVA: 0x000D19D5 File Offset: 0x000CFBD5
		protected override void OnInsert(int index, object value)
		{
			this.owner.AddPermissionAccess((EventLogPermissionEntry)value);
		}

		/// <summary>Performs additional custom processes when removing a new permission entry from the collection.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> can be found.</param>
		/// <param name="value">The permission entry to remove from <paramref name="index" />.</param>
		// Token: 0x06002E83 RID: 11907 RVA: 0x000D19E8 File Offset: 0x000CFBE8
		protected override void OnRemove(int index, object value)
		{
			this.owner.RemovePermissionAccess((EventLogPermissionEntry)value);
		}

		/// <summary>Performs additional custom processes before setting a value in the collection.</summary>
		/// <param name="index">The zero-based index at which <paramref name="oldValue" /> can be found.</param>
		/// <param name="oldValue">The value to replace with <paramref name="newValue" />.</param>
		/// <param name="newValue">The new value of the permission entry at <paramref name="index" />.</param>
		// Token: 0x06002E84 RID: 11908 RVA: 0x000D19FB File Offset: 0x000CFBFB
		protected override void OnSet(int index, object oldValue, object newValue)
		{
			this.owner.RemovePermissionAccess((EventLogPermissionEntry)oldValue);
			this.owner.AddPermissionAccess((EventLogPermissionEntry)newValue);
		}

		// Token: 0x04002768 RID: 10088
		private EventLogPermission owner;
	}
}
