using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Contains a strongly typed collection of <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> objects.</summary>
	// Token: 0x020004EC RID: 1260
	[Serializable]
	public class PerformanceCounterPermissionEntryCollection : CollectionBase
	{
		// Token: 0x06002F84 RID: 12164 RVA: 0x000D6A78 File Offset: 0x000D4C78
		internal PerformanceCounterPermissionEntryCollection(PerformanceCounterPermission owner, ResourcePermissionBaseEntry[] entries)
		{
			this.owner = owner;
			for (int i = 0; i < entries.Length; i++)
			{
				base.InnerList.Add(new PerformanceCounterPermissionEntry(entries[i]));
			}
		}

		/// <summary>Gets or sets the object at a specified index.</summary>
		/// <param name="index">The zero-based index into the collection.</param>
		/// <returns>The <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> object that exists at the specified index.</returns>
		// Token: 0x17000B87 RID: 2951
		public PerformanceCounterPermissionEntry this[int index]
		{
			get
			{
				return (PerformanceCounterPermissionEntry)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds a specified <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> to this collection.</summary>
		/// <param name="value">The <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> object to add.</param>
		/// <returns>The zero-based index of the added <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> object.</returns>
		// Token: 0x06002F87 RID: 12167 RVA: 0x000D6AD6 File Offset: 0x000D4CD6
		public int Add(PerformanceCounterPermissionEntry value)
		{
			return base.List.Add(value);
		}

		/// <summary>Appends a set of specified permission entries to this collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> objects that contains the permission entries to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06002F88 RID: 12168 RVA: 0x000D6AE4 File Offset: 0x000D4CE4
		public void AddRange(PerformanceCounterPermissionEntry[] value)
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
		/// <param name="value">A <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntryCollection" /> that contains the permission entries to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06002F89 RID: 12169 RVA: 0x000D6B18 File Offset: 0x000D4D18
		public void AddRange(PerformanceCounterPermissionEntryCollection value)
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

		/// <summary>Determines whether this collection contains a specified <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> object.</summary>
		/// <param name="value">The <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> object to find.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> object belongs to this collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002F8A RID: 12170 RVA: 0x000D6B54 File Offset: 0x000D4D54
		public bool Contains(PerformanceCounterPermissionEntry value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Copies the permission entries from this collection to an array, starting at a particular index of the array.</summary>
		/// <param name="array">An array of type <see cref="T:System.Diagnostics.PerformanceCounterPermissionEntry" /> that receives this collection's permission entries.</param>
		/// <param name="index">The zero-based index at which to begin copying the permission entries.</param>
		// Token: 0x06002F8B RID: 12171 RVA: 0x000D6B62 File Offset: 0x000D4D62
		public void CopyTo(PerformanceCounterPermissionEntry[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Determines the index of a specified permission entry in this collection.</summary>
		/// <param name="value">The permission entry for which to search.</param>
		/// <returns>The zero-based index of the specified permission entry, or -1 if the permission entry was not found in the collection.</returns>
		// Token: 0x06002F8C RID: 12172 RVA: 0x000D6B71 File Offset: 0x000D4D71
		public int IndexOf(PerformanceCounterPermissionEntry value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts a permission entry into this collection at a specified index.</summary>
		/// <param name="index">The zero-based index of the collection at which to insert the permission entry.</param>
		/// <param name="value">The permission entry to insert into this collection.</param>
		// Token: 0x06002F8D RID: 12173 RVA: 0x000D6B7F File Offset: 0x000D4D7F
		public void Insert(int index, PerformanceCounterPermissionEntry value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Removes a specified permission entry from this collection.</summary>
		/// <param name="value">The permission entry to remove.</param>
		// Token: 0x06002F8E RID: 12174 RVA: 0x000D6B8E File Offset: 0x000D4D8E
		public void Remove(PerformanceCounterPermissionEntry value)
		{
			base.List.Remove(value);
		}

		/// <summary>Performs additional custom processes after clearing the contents of the collection.</summary>
		// Token: 0x06002F8F RID: 12175 RVA: 0x000D6B9C File Offset: 0x000D4D9C
		protected override void OnClear()
		{
			this.owner.Clear();
		}

		/// <summary>Performs additional custom processes before a new permission entry is inserted into the collection.</summary>
		/// <param name="index">The zero-based index at which to insert <paramref name="value" />.</param>
		/// <param name="value">The new value of the permission entry at <paramref name="index" />.</param>
		// Token: 0x06002F90 RID: 12176 RVA: 0x000D6BA9 File Offset: 0x000D4DA9
		protected override void OnInsert(int index, object value)
		{
			this.owner.AddPermissionAccess((PerformanceCounterPermissionEntry)value);
		}

		/// <summary>Performs additional custom processes when removing a new permission entry from the collection.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> can be found.</param>
		/// <param name="value">The permission entry to remove from <paramref name="index" />.</param>
		// Token: 0x06002F91 RID: 12177 RVA: 0x000D6BBC File Offset: 0x000D4DBC
		protected override void OnRemove(int index, object value)
		{
			this.owner.RemovePermissionAccess((PerformanceCounterPermissionEntry)value);
		}

		/// <summary>Performs additional custom processes before setting a value in the collection.</summary>
		/// <param name="index">The zero-based index at which <paramref name="oldValue" /> can be found.</param>
		/// <param name="oldValue">The value to replace with <paramref name="newValue" />.</param>
		/// <param name="newValue">The new value of the permission entry at <paramref name="index" />.</param>
		// Token: 0x06002F92 RID: 12178 RVA: 0x000D6BCF File Offset: 0x000D4DCF
		protected override void OnSet(int index, object oldValue, object newValue)
		{
			this.owner.RemovePermissionAccess((PerformanceCounterPermissionEntry)oldValue);
			this.owner.AddPermissionAccess((PerformanceCounterPermissionEntry)newValue);
		}

		// Token: 0x040027FA RID: 10234
		private PerformanceCounterPermission owner;
	}
}
