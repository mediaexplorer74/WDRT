using System;
using System.Collections.Generic;
using System.Security.Permissions;

namespace System.Collections.Concurrent
{
	/// <summary>Represents a particular manner of splitting an orderable data source into multiple partitions.</summary>
	/// <typeparam name="TSource">Type of the elements in the collection.</typeparam>
	// Token: 0x020004B0 RID: 1200
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public abstract class OrderablePartitioner<TSource> : Partitioner<TSource>
	{
		/// <summary>Called from constructors in derived classes to initialize the <see cref="T:System.Collections.Concurrent.OrderablePartitioner`1" /> class with the specified constraints on the index keys.</summary>
		/// <param name="keysOrderedInEachPartition">Indicates whether the elements in each partition are yielded in the order of increasing keys.</param>
		/// <param name="keysOrderedAcrossPartitions">Indicates whether elements in an earlier partition always come before elements in a later partition. If true, each element in partition 0 has a smaller order key than any element in partition 1, each element in partition 1 has a smaller order key than any element in partition 2, and so on.</param>
		/// <param name="keysNormalized">Indicates whether keys are normalized. If true, all order keys are distinct integers in the range [0 .. numberOfElements-1]. If false, order keys must still be distinct, but only their relative order is considered, not their absolute values.</param>
		// Token: 0x060039AA RID: 14762 RVA: 0x000DDFCE File Offset: 0x000DC1CE
		[__DynamicallyInvokable]
		protected OrderablePartitioner(bool keysOrderedInEachPartition, bool keysOrderedAcrossPartitions, bool keysNormalized)
		{
			this.KeysOrderedInEachPartition = keysOrderedInEachPartition;
			this.KeysOrderedAcrossPartitions = keysOrderedAcrossPartitions;
			this.KeysNormalized = keysNormalized;
		}

		/// <summary>Partitions the underlying collection into the specified number of orderable partitions.</summary>
		/// <param name="partitionCount">The number of partitions to create.</param>
		/// <returns>A list containing <paramref name="partitionCount" /> enumerators.</returns>
		// Token: 0x060039AB RID: 14763
		[__DynamicallyInvokable]
		public abstract IList<IEnumerator<KeyValuePair<long, TSource>>> GetOrderablePartitions(int partitionCount);

		/// <summary>Creates an object that can partition the underlying collection into a variable number of partitions.</summary>
		/// <returns>An object that can create partitions over the underlying data source.</returns>
		/// <exception cref="T:System.NotSupportedException">Dynamic partitioning is not supported by this partitioner.</exception>
		// Token: 0x060039AC RID: 14764 RVA: 0x000DDFEB File Offset: 0x000DC1EB
		[__DynamicallyInvokable]
		public virtual IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions()
		{
			throw new NotSupportedException(Environment.GetResourceString("Partitioner_DynamicPartitionsNotSupported"));
		}

		/// <summary>Gets whether elements in each partition are yielded in the order of increasing keys.</summary>
		/// <returns>
		///   <see langword="true" /> if the elements in each partition are yielded in the order of increasing keys; otherwise, <see langword="false" />.</returns>
		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x060039AD RID: 14765 RVA: 0x000DDFFC File Offset: 0x000DC1FC
		// (set) Token: 0x060039AE RID: 14766 RVA: 0x000DE004 File Offset: 0x000DC204
		[__DynamicallyInvokable]
		public bool KeysOrderedInEachPartition
		{
			[__DynamicallyInvokable]
			get;
			private set; }

		/// <summary>Gets whether elements in an earlier partition always come before elements in a later partition.</summary>
		/// <returns>
		///   <see langword="true" /> if the elements in an earlier partition always come before elements in a later partition; otherwise, <see langword="false" />.</returns>
		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x060039AF RID: 14767 RVA: 0x000DE00D File Offset: 0x000DC20D
		// (set) Token: 0x060039B0 RID: 14768 RVA: 0x000DE015 File Offset: 0x000DC215
		[__DynamicallyInvokable]
		public bool KeysOrderedAcrossPartitions
		{
			[__DynamicallyInvokable]
			get;
			private set; }

		/// <summary>Gets whether order keys are normalized.</summary>
		/// <returns>
		///   <see langword="true" /> if the keys are normalized; otherwise, <see langword="false" />.</returns>
		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x060039B1 RID: 14769 RVA: 0x000DE01E File Offset: 0x000DC21E
		// (set) Token: 0x060039B2 RID: 14770 RVA: 0x000DE026 File Offset: 0x000DC226
		[__DynamicallyInvokable]
		public bool KeysNormalized
		{
			[__DynamicallyInvokable]
			get;
			private set; }

		/// <summary>Partitions the underlying collection into the given number of ordered partitions.</summary>
		/// <param name="partitionCount">The number of partitions to create.</param>
		/// <returns>A list containing <paramref name="partitionCount" /> enumerators.</returns>
		// Token: 0x060039B3 RID: 14771 RVA: 0x000DE030 File Offset: 0x000DC230
		[__DynamicallyInvokable]
		public override IList<IEnumerator<TSource>> GetPartitions(int partitionCount)
		{
			IList<IEnumerator<KeyValuePair<long, TSource>>> orderablePartitions = this.GetOrderablePartitions(partitionCount);
			if (orderablePartitions.Count != partitionCount)
			{
				throw new InvalidOperationException("OrderablePartitioner_GetPartitions_WrongNumberOfPartitions");
			}
			IEnumerator<TSource>[] array = new IEnumerator<TSource>[partitionCount];
			for (int i = 0; i < partitionCount; i++)
			{
				array[i] = new OrderablePartitioner<TSource>.EnumeratorDropIndices(orderablePartitions[i]);
			}
			return array;
		}

		/// <summary>Creates an object that can partition the underlying collection into a variable number of partitions.</summary>
		/// <returns>An object that can create partitions over the underlying data source.</returns>
		/// <exception cref="T:System.NotSupportedException">Dynamic partitioning is not supported by the base class. It must be implemented in derived classes.</exception>
		// Token: 0x060039B4 RID: 14772 RVA: 0x000DE07C File Offset: 0x000DC27C
		[__DynamicallyInvokable]
		public override IEnumerable<TSource> GetDynamicPartitions()
		{
			IEnumerable<KeyValuePair<long, TSource>> orderableDynamicPartitions = this.GetOrderableDynamicPartitions();
			return new OrderablePartitioner<TSource>.EnumerableDropIndices(orderableDynamicPartitions);
		}

		// Token: 0x02000BC4 RID: 3012
		private class EnumerableDropIndices : IEnumerable<TSource>, IEnumerable, IDisposable
		{
			// Token: 0x06006EA9 RID: 28329 RVA: 0x0017F1DF File Offset: 0x0017D3DF
			public EnumerableDropIndices(IEnumerable<KeyValuePair<long, TSource>> source)
			{
				this.m_source = source;
			}

			// Token: 0x06006EAA RID: 28330 RVA: 0x0017F1EE File Offset: 0x0017D3EE
			public IEnumerator<TSource> GetEnumerator()
			{
				return new OrderablePartitioner<TSource>.EnumeratorDropIndices(this.m_source.GetEnumerator());
			}

			// Token: 0x06006EAB RID: 28331 RVA: 0x0017F200 File Offset: 0x0017D400
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06006EAC RID: 28332 RVA: 0x0017F208 File Offset: 0x0017D408
			public void Dispose()
			{
				IDisposable disposable = this.m_source as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}

			// Token: 0x040035B4 RID: 13748
			private readonly IEnumerable<KeyValuePair<long, TSource>> m_source;
		}

		// Token: 0x02000BC5 RID: 3013
		private class EnumeratorDropIndices : IEnumerator<TSource>, IDisposable, IEnumerator
		{
			// Token: 0x06006EAD RID: 28333 RVA: 0x0017F22A File Offset: 0x0017D42A
			public EnumeratorDropIndices(IEnumerator<KeyValuePair<long, TSource>> source)
			{
				this.m_source = source;
			}

			// Token: 0x06006EAE RID: 28334 RVA: 0x0017F239 File Offset: 0x0017D439
			public bool MoveNext()
			{
				return this.m_source.MoveNext();
			}

			// Token: 0x170012E9 RID: 4841
			// (get) Token: 0x06006EAF RID: 28335 RVA: 0x0017F248 File Offset: 0x0017D448
			public TSource Current
			{
				get
				{
					KeyValuePair<long, TSource> keyValuePair = this.m_source.Current;
					return keyValuePair.Value;
				}
			}

			// Token: 0x170012EA RID: 4842
			// (get) Token: 0x06006EB0 RID: 28336 RVA: 0x0017F268 File Offset: 0x0017D468
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06006EB1 RID: 28337 RVA: 0x0017F275 File Offset: 0x0017D475
			public void Dispose()
			{
				this.m_source.Dispose();
			}

			// Token: 0x06006EB2 RID: 28338 RVA: 0x0017F282 File Offset: 0x0017D482
			public void Reset()
			{
				this.m_source.Reset();
			}

			// Token: 0x040035B5 RID: 13749
			private readonly IEnumerator<KeyValuePair<long, TSource>> m_source;
		}
	}
}
