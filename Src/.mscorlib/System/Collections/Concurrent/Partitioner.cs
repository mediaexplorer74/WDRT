using System;
using System.Collections.Generic;
using System.Security.Permissions;

namespace System.Collections.Concurrent
{
	/// <summary>Represents a particular manner of splitting a data source into multiple partitions.</summary>
	/// <typeparam name="TSource">Type of the elements in the collection.</typeparam>
	// Token: 0x020004AF RID: 1199
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public abstract class Partitioner<TSource>
	{
		/// <summary>Partitions the underlying collection into the given number of partitions.</summary>
		/// <param name="partitionCount">The number of partitions to create.</param>
		/// <returns>A list containing <paramref name="partitionCount" /> enumerators.</returns>
		// Token: 0x060039A6 RID: 14758
		[__DynamicallyInvokable]
		public abstract IList<IEnumerator<TSource>> GetPartitions(int partitionCount);

		/// <summary>Gets whether additional partitions can be created dynamically.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Concurrent.Partitioner`1" /> can create partitions dynamically as they are requested; <see langword="false" /> if the <see cref="T:System.Collections.Concurrent.Partitioner`1" /> can only allocate partitions statically.</returns>
		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x060039A7 RID: 14759 RVA: 0x000DDFB2 File Offset: 0x000DC1B2
		[__DynamicallyInvokable]
		public virtual bool SupportsDynamicPartitions
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		/// <summary>Creates an object that can partition the underlying collection into a variable number of partitions.</summary>
		/// <returns>An object that can create partitions over the underlying data source.</returns>
		/// <exception cref="T:System.NotSupportedException">Dynamic partitioning is not supported by the base class. You must implement it in a derived class.</exception>
		// Token: 0x060039A8 RID: 14760 RVA: 0x000DDFB5 File Offset: 0x000DC1B5
		[__DynamicallyInvokable]
		public virtual IEnumerable<TSource> GetDynamicPartitions()
		{
			throw new NotSupportedException(Environment.GetResourceString("Partitioner_DynamicPartitionsNotSupported"));
		}

		/// <summary>Creates a new partitioner instance.</summary>
		// Token: 0x060039A9 RID: 14761 RVA: 0x000DDFC6 File Offset: 0x000DC1C6
		[__DynamicallyInvokable]
		protected Partitioner()
		{
		}
	}
}
