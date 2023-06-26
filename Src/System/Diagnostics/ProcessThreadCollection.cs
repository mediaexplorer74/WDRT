using System;
using System.Collections;

namespace System.Diagnostics
{
	/// <summary>Provides a strongly typed collection of <see cref="T:System.Diagnostics.ProcessThread" /> objects.</summary>
	// Token: 0x02000500 RID: 1280
	public class ProcessThreadCollection : ReadOnlyCollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.ProcessThreadCollection" /> class, with no associated <see cref="T:System.Diagnostics.ProcessThread" /> instances.</summary>
		// Token: 0x0600309C RID: 12444 RVA: 0x000DB9E0 File Offset: 0x000D9BE0
		protected ProcessThreadCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.ProcessThreadCollection" /> class, using the specified array of <see cref="T:System.Diagnostics.ProcessThread" /> instances.</summary>
		/// <param name="processThreads">An array of <see cref="T:System.Diagnostics.ProcessThread" /> instances with which to initialize this <see cref="T:System.Diagnostics.ProcessThreadCollection" /> instance.</param>
		// Token: 0x0600309D RID: 12445 RVA: 0x000DB9E8 File Offset: 0x000D9BE8
		public ProcessThreadCollection(ProcessThread[] processThreads)
		{
			base.InnerList.AddRange(processThreads);
		}

		/// <summary>Gets an index for iterating over the set of process threads.</summary>
		/// <param name="index">The zero-based index value of the thread in the collection.</param>
		/// <returns>A <see cref="T:System.Diagnostics.ProcessThread" /> that indexes the threads in the collection.</returns>
		// Token: 0x17000BF0 RID: 3056
		public ProcessThread this[int index]
		{
			get
			{
				return (ProcessThread)base.InnerList[index];
			}
		}

		/// <summary>Appends a process thread to the collection.</summary>
		/// <param name="thread">The thread to add to the collection.</param>
		/// <returns>The zero-based index of the thread in the collection.</returns>
		// Token: 0x0600309F RID: 12447 RVA: 0x000DBA0F File Offset: 0x000D9C0F
		public int Add(ProcessThread thread)
		{
			return base.InnerList.Add(thread);
		}

		/// <summary>Inserts a process thread at the specified location in the collection.</summary>
		/// <param name="index">The zero-based index indicating the location at which to insert the thread.</param>
		/// <param name="thread">The thread to insert into the collection.</param>
		// Token: 0x060030A0 RID: 12448 RVA: 0x000DBA1D File Offset: 0x000D9C1D
		public void Insert(int index, ProcessThread thread)
		{
			base.InnerList.Insert(index, thread);
		}

		/// <summary>Provides the location of a specified thread within the collection.</summary>
		/// <param name="thread">The <see cref="T:System.Diagnostics.ProcessThread" /> whose index is retrieved.</param>
		/// <returns>The zero-based index that defines the location of the thread within the <see cref="T:System.Diagnostics.ProcessThreadCollection" />.</returns>
		// Token: 0x060030A1 RID: 12449 RVA: 0x000DBA2C File Offset: 0x000D9C2C
		public int IndexOf(ProcessThread thread)
		{
			return base.InnerList.IndexOf(thread);
		}

		/// <summary>Determines whether the specified process thread exists in the collection.</summary>
		/// <param name="thread">A <see cref="T:System.Diagnostics.ProcessThread" /> instance that indicates the thread to find in this collection.</param>
		/// <returns>
		///   <see langword="true" /> if the thread exists in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x060030A2 RID: 12450 RVA: 0x000DBA3A File Offset: 0x000D9C3A
		public bool Contains(ProcessThread thread)
		{
			return base.InnerList.Contains(thread);
		}

		/// <summary>Deletes a process thread from the collection.</summary>
		/// <param name="thread">The thread to remove from the collection.</param>
		// Token: 0x060030A3 RID: 12451 RVA: 0x000DBA48 File Offset: 0x000D9C48
		public void Remove(ProcessThread thread)
		{
			base.InnerList.Remove(thread);
		}

		/// <summary>Copies an array of <see cref="T:System.Diagnostics.ProcessThread" /> instances to the collection, at the specified index.</summary>
		/// <param name="array">An array of <see cref="T:System.Diagnostics.ProcessThread" /> instances to add to the collection.</param>
		/// <param name="index">The location at which to add the new instances.</param>
		// Token: 0x060030A4 RID: 12452 RVA: 0x000DBA56 File Offset: 0x000D9C56
		public void CopyTo(ProcessThread[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}
	}
}
