using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Represents the enumerator for <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> objects in a <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntryCollection" />.</summary>
	// Token: 0x02000315 RID: 789
	[ComVisible(true)]
	[Serializable]
	public sealed class KeyContainerPermissionAccessEntryEnumerator : IEnumerator
	{
		// Token: 0x060027F2 RID: 10226 RVA: 0x000925AA File Offset: 0x000907AA
		private KeyContainerPermissionAccessEntryEnumerator()
		{
		}

		// Token: 0x060027F3 RID: 10227 RVA: 0x000925B2 File Offset: 0x000907B2
		internal KeyContainerPermissionAccessEntryEnumerator(KeyContainerPermissionAccessEntryCollection entries)
		{
			this.m_entries = entries;
			this.m_current = -1;
		}

		/// <summary>Gets the current entry in the collection.</summary>
		/// <returns>The current <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object in the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator.Current" /> property is accessed before first calling the <see cref="M:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator.MoveNext" /> method. The cursor is located before the first object in the collection.  
		///  -or-  
		///  The <see cref="P:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator.Current" /> property is accessed after a call to the <see cref="M:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator.MoveNext" /> method returns <see langword="false" />, which indicates that the cursor is located after the last object in the collection.</exception>
		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x060027F4 RID: 10228 RVA: 0x000925C8 File Offset: 0x000907C8
		public KeyContainerPermissionAccessEntry Current
		{
			get
			{
				return this.m_entries[this.m_current];
			}
		}

		/// <summary>Gets the current object in the collection.</summary>
		/// <returns>The current object in the collection.</returns>
		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x060027F5 RID: 10229 RVA: 0x000925DB File Offset: 0x000907DB
		object IEnumerator.Current
		{
			get
			{
				return this.m_entries[this.m_current];
			}
		}

		/// <summary>Moves to the next element in the collection.</summary>
		/// <returns>
		///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
		// Token: 0x060027F6 RID: 10230 RVA: 0x000925EE File Offset: 0x000907EE
		public bool MoveNext()
		{
			if (this.m_current == this.m_entries.Count - 1)
			{
				return false;
			}
			this.m_current++;
			return true;
		}

		/// <summary>Resets the enumerator to the beginning of the collection.</summary>
		// Token: 0x060027F7 RID: 10231 RVA: 0x00092616 File Offset: 0x00090816
		public void Reset()
		{
			this.m_current = -1;
		}

		// Token: 0x04000F79 RID: 3961
		private KeyContainerPermissionAccessEntryCollection m_entries;

		// Token: 0x04000F7A RID: 3962
		private int m_current;
	}
}
