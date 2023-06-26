using System;
using System.Collections;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a collection of designers.</summary>
	// Token: 0x020005DC RID: 1500
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class DesignerCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerCollection" /> class that contains the specified designers.</summary>
		/// <param name="designers">An array of <see cref="T:System.ComponentModel.Design.IDesignerHost" /> objects to store.</param>
		// Token: 0x060037AF RID: 14255 RVA: 0x000F05F6 File Offset: 0x000EE7F6
		public DesignerCollection(IDesignerHost[] designers)
		{
			if (designers != null)
			{
				this.designers = new ArrayList(designers);
				return;
			}
			this.designers = new ArrayList();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerCollection" /> class that contains the specified set of designers.</summary>
		/// <param name="designers">A list that contains the collection of designers to add.</param>
		// Token: 0x060037B0 RID: 14256 RVA: 0x000F0619 File Offset: 0x000EE819
		public DesignerCollection(IList designers)
		{
			this.designers = designers;
		}

		/// <summary>Gets the number of designers in the collection.</summary>
		/// <returns>The number of designers in the collection.</returns>
		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x060037B1 RID: 14257 RVA: 0x000F0628 File Offset: 0x000EE828
		public int Count
		{
			get
			{
				return this.designers.Count;
			}
		}

		/// <summary>Gets the designer at the specified index.</summary>
		/// <param name="index">The index of the designer to return.</param>
		/// <returns>The designer at the specified index.</returns>
		// Token: 0x17000D60 RID: 3424
		public virtual IDesignerHost this[int index]
		{
			get
			{
				return (IDesignerHost)this.designers[index];
			}
		}

		/// <summary>Gets a new enumerator for this collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that enumerates the collection.</returns>
		// Token: 0x060037B3 RID: 14259 RVA: 0x000F0648 File Offset: 0x000EE848
		public IEnumerator GetEnumerator()
		{
			return this.designers.GetEnumerator();
		}

		/// <summary>Gets the number of elements contained in the collection.</summary>
		/// <returns>The number of elements contained in the collection.</returns>
		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x060037B4 RID: 14260 RVA: 0x000F0655 File Offset: 0x000EE855
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x060037B5 RID: 14261 RVA: 0x000F065D File Offset: 0x000EE85D
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x060037B6 RID: 14262 RVA: 0x000F0660 File Offset: 0x000EE860
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		/// <summary>Copies the elements of the collection to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from collection. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x060037B7 RID: 14263 RVA: 0x000F0663 File Offset: 0x000EE863
		void ICollection.CopyTo(Array array, int index)
		{
			this.designers.CopyTo(array, index);
		}

		/// <summary>Gets a new enumerator for this collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that enumerates the collection.</returns>
		// Token: 0x060037B8 RID: 14264 RVA: 0x000F0672 File Offset: 0x000EE872
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04002AE9 RID: 10985
		private IList designers;
	}
}
