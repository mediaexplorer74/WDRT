using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections.Concurrent
{
	/// <summary>Provides common partitioning strategies for arrays, lists, and enumerables.</summary>
	// Token: 0x020004B2 RID: 1202
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public static class Partitioner
	{
		/// <summary>Creates an orderable partitioner from an <see cref="T:System.Collections.Generic.IList`1" /> instance.</summary>
		/// <param name="list">The list to be partitioned.</param>
		/// <param name="loadBalance">A Boolean value that indicates whether the created partitioner should dynamically load balance between partitions rather than statically partition.</param>
		/// <typeparam name="TSource">Type of the elements in source list.</typeparam>
		/// <returns>An orderable partitioner based on the input list.</returns>
		// Token: 0x060039B5 RID: 14773 RVA: 0x000DE096 File Offset: 0x000DC296
		[__DynamicallyInvokable]
		public static OrderablePartitioner<TSource> Create<TSource>(IList<TSource> list, bool loadBalance)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (loadBalance)
			{
				return new Partitioner.DynamicPartitionerForIList<TSource>(list);
			}
			return new Partitioner.StaticIndexRangePartitionerForIList<TSource>(list);
		}

		/// <summary>Creates an orderable partitioner from a <see cref="T:System.Array" /> instance.</summary>
		/// <param name="array">The array to be partitioned.</param>
		/// <param name="loadBalance">A Boolean value that indicates whether the created partitioner should dynamically load balance between partitions rather than statically partition.</param>
		/// <typeparam name="TSource">Type of the elements in source array.</typeparam>
		/// <returns>An orderable partitioner based on the input array.</returns>
		// Token: 0x060039B6 RID: 14774 RVA: 0x000DE0B6 File Offset: 0x000DC2B6
		[__DynamicallyInvokable]
		public static OrderablePartitioner<TSource> Create<TSource>(TSource[] array, bool loadBalance)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (loadBalance)
			{
				return new Partitioner.DynamicPartitionerForArray<TSource>(array);
			}
			return new Partitioner.StaticIndexRangePartitionerForArray<TSource>(array);
		}

		/// <summary>Creates an orderable partitioner from a <see cref="T:System.Collections.Generic.IEnumerable`1" /> instance.</summary>
		/// <param name="source">The enumerable to be partitioned.</param>
		/// <typeparam name="TSource">Type of the elements in source enumerable.</typeparam>
		/// <returns>An orderable partitioner based on the input array.</returns>
		// Token: 0x060039B7 RID: 14775 RVA: 0x000DE0D6 File Offset: 0x000DC2D6
		[__DynamicallyInvokable]
		public static OrderablePartitioner<TSource> Create<TSource>(IEnumerable<TSource> source)
		{
			return Partitioner.Create<TSource>(source, EnumerablePartitionerOptions.None);
		}

		/// <summary>Creates an orderable partitioner from a <see cref="T:System.Collections.Generic.IEnumerable`1" /> instance.</summary>
		/// <param name="source">The enumerable to be partitioned.</param>
		/// <param name="partitionerOptions">Options to control the buffering behavior of the partitioner.</param>
		/// <typeparam name="TSource">Type of the elements in source enumerable.</typeparam>
		/// <returns>An orderable partitioner based on the input array.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="partitionerOptions" /> argument specifies an invalid value for <see cref="T:System.Collections.Concurrent.EnumerablePartitionerOptions" />.</exception>
		// Token: 0x060039B8 RID: 14776 RVA: 0x000DE0DF File Offset: 0x000DC2DF
		[__DynamicallyInvokable]
		public static OrderablePartitioner<TSource> Create<TSource>(IEnumerable<TSource> source, EnumerablePartitionerOptions partitionerOptions)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if ((partitionerOptions & ~EnumerablePartitionerOptions.NoBuffering) != EnumerablePartitionerOptions.None)
			{
				throw new ArgumentOutOfRangeException("partitionerOptions");
			}
			return new Partitioner.DynamicPartitionerForIEnumerable<TSource>(source, partitionerOptions);
		}

		/// <summary>Creates a partitioner that chunks the user-specified range.</summary>
		/// <param name="fromInclusive">The lower, inclusive bound of the range.</param>
		/// <param name="toExclusive">The upper, exclusive bound of the range.</param>
		/// <returns>A partitioner.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="toExclusive" /> argument is less than or equal to the <paramref name="fromInclusive" /> argument.</exception>
		// Token: 0x060039B9 RID: 14777 RVA: 0x000DE108 File Offset: 0x000DC308
		[__DynamicallyInvokable]
		public static OrderablePartitioner<Tuple<long, long>> Create(long fromInclusive, long toExclusive)
		{
			int num = 3;
			if (toExclusive <= fromInclusive)
			{
				throw new ArgumentOutOfRangeException("toExclusive");
			}
			long num2 = (toExclusive - fromInclusive) / (long)(PlatformHelper.ProcessorCount * num);
			if (num2 == 0L)
			{
				num2 = 1L;
			}
			return Partitioner.Create<Tuple<long, long>>(Partitioner.CreateRanges(fromInclusive, toExclusive, num2), EnumerablePartitionerOptions.NoBuffering);
		}

		/// <summary>Creates a partitioner that chunks the user-specified range.</summary>
		/// <param name="fromInclusive">The lower, inclusive bound of the range.</param>
		/// <param name="toExclusive">The upper, exclusive bound of the range.</param>
		/// <param name="rangeSize">The size of each subrange.</param>
		/// <returns>A partitioner.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="toExclusive" /> argument is less than or equal to the <paramref name="fromInclusive" /> argument.  
		///  -or-  
		///  The <paramref name="rangeSize" /> argument is less than or equal to 0.</exception>
		// Token: 0x060039BA RID: 14778 RVA: 0x000DE147 File Offset: 0x000DC347
		[__DynamicallyInvokable]
		public static OrderablePartitioner<Tuple<long, long>> Create(long fromInclusive, long toExclusive, long rangeSize)
		{
			if (toExclusive <= fromInclusive)
			{
				throw new ArgumentOutOfRangeException("toExclusive");
			}
			if (rangeSize <= 0L)
			{
				throw new ArgumentOutOfRangeException("rangeSize");
			}
			return Partitioner.Create<Tuple<long, long>>(Partitioner.CreateRanges(fromInclusive, toExclusive, rangeSize), EnumerablePartitionerOptions.NoBuffering);
		}

		// Token: 0x060039BB RID: 14779 RVA: 0x000DE176 File Offset: 0x000DC376
		private static IEnumerable<Tuple<long, long>> CreateRanges(long fromInclusive, long toExclusive, long rangeSize)
		{
			bool shouldQuit = false;
			long i = fromInclusive;
			while (i < toExclusive && !shouldQuit)
			{
				long num = i;
				long num2;
				try
				{
					num2 = checked(i + rangeSize);
				}
				catch (OverflowException)
				{
					num2 = toExclusive;
					shouldQuit = true;
				}
				if (num2 > toExclusive)
				{
					num2 = toExclusive;
				}
				yield return new Tuple<long, long>(num, num2);
				i += rangeSize;
			}
			yield break;
		}

		/// <summary>Creates a partitioner that chunks the user-specified range.</summary>
		/// <param name="fromInclusive">The lower, inclusive bound of the range.</param>
		/// <param name="toExclusive">The upper, exclusive bound of the range.</param>
		/// <returns>A partitioner.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="toExclusive" /> argument is less than or equal to the <paramref name="fromInclusive" /> argument.</exception>
		// Token: 0x060039BC RID: 14780 RVA: 0x000DE194 File Offset: 0x000DC394
		[__DynamicallyInvokable]
		public static OrderablePartitioner<Tuple<int, int>> Create(int fromInclusive, int toExclusive)
		{
			int num = 3;
			if (toExclusive <= fromInclusive)
			{
				throw new ArgumentOutOfRangeException("toExclusive");
			}
			int num2 = (toExclusive - fromInclusive) / (PlatformHelper.ProcessorCount * num);
			if (num2 == 0)
			{
				num2 = 1;
			}
			return Partitioner.Create<Tuple<int, int>>(Partitioner.CreateRanges(fromInclusive, toExclusive, num2), EnumerablePartitionerOptions.NoBuffering);
		}

		/// <summary>Creates a partitioner that chunks the user-specified range.</summary>
		/// <param name="fromInclusive">The lower, inclusive bound of the range.</param>
		/// <param name="toExclusive">The upper, exclusive bound of the range.</param>
		/// <param name="rangeSize">The size of each subrange.</param>
		/// <returns>A partitioner.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="toExclusive" /> argument is less than or equal to the <paramref name="fromInclusive" /> argument.  
		///  -or-  
		///  The <paramref name="rangeSize" /> argument is less than or equal to 0.</exception>
		// Token: 0x060039BD RID: 14781 RVA: 0x000DE1D1 File Offset: 0x000DC3D1
		[__DynamicallyInvokable]
		public static OrderablePartitioner<Tuple<int, int>> Create(int fromInclusive, int toExclusive, int rangeSize)
		{
			if (toExclusive <= fromInclusive)
			{
				throw new ArgumentOutOfRangeException("toExclusive");
			}
			if (rangeSize <= 0)
			{
				throw new ArgumentOutOfRangeException("rangeSize");
			}
			return Partitioner.Create<Tuple<int, int>>(Partitioner.CreateRanges(fromInclusive, toExclusive, rangeSize), EnumerablePartitionerOptions.NoBuffering);
		}

		// Token: 0x060039BE RID: 14782 RVA: 0x000DE1FF File Offset: 0x000DC3FF
		private static IEnumerable<Tuple<int, int>> CreateRanges(int fromInclusive, int toExclusive, int rangeSize)
		{
			bool shouldQuit = false;
			int i = fromInclusive;
			while (i < toExclusive && !shouldQuit)
			{
				int num = i;
				int num2;
				try
				{
					num2 = checked(i + rangeSize);
				}
				catch (OverflowException)
				{
					num2 = toExclusive;
					shouldQuit = true;
				}
				if (num2 > toExclusive)
				{
					num2 = toExclusive;
				}
				yield return new Tuple<int, int>(num, num2);
				i += rangeSize;
			}
			yield break;
		}

		// Token: 0x060039BF RID: 14783 RVA: 0x000DE220 File Offset: 0x000DC420
		private static int GetDefaultChunkSize<TSource>()
		{
			int num;
			if (typeof(TSource).IsValueType)
			{
				if (typeof(TSource).StructLayoutAttribute.Value == LayoutKind.Explicit)
				{
					num = Math.Max(1, 512 / Marshal.SizeOf(typeof(TSource)));
				}
				else
				{
					num = 128;
				}
			}
			else
			{
				num = 512 / IntPtr.Size;
			}
			return num;
		}

		// Token: 0x0400193A RID: 6458
		private const int DEFAULT_BYTES_PER_CHUNK = 512;

		// Token: 0x02000BC6 RID: 3014
		private abstract class DynamicPartitionEnumerator_Abstract<TSource, TSourceReader> : IEnumerator<KeyValuePair<long, TSource>>, IDisposable, IEnumerator
		{
			// Token: 0x06006EB3 RID: 28339 RVA: 0x0017F28F File Offset: 0x0017D48F
			protected DynamicPartitionEnumerator_Abstract(TSourceReader sharedReader, Partitioner.SharedLong sharedIndex)
				: this(sharedReader, sharedIndex, false)
			{
			}

			// Token: 0x06006EB4 RID: 28340 RVA: 0x0017F29A File Offset: 0x0017D49A
			protected DynamicPartitionEnumerator_Abstract(TSourceReader sharedReader, Partitioner.SharedLong sharedIndex, bool useSingleChunking)
			{
				this.m_sharedReader = sharedReader;
				this.m_sharedIndex = sharedIndex;
				this.m_maxChunkSize = (useSingleChunking ? 1 : Partitioner.DynamicPartitionEnumerator_Abstract<TSource, TSourceReader>.s_defaultMaxChunkSize);
			}

			// Token: 0x06006EB5 RID: 28341
			protected abstract bool GrabNextChunk(int requestedChunkSize);

			// Token: 0x170012EB RID: 4843
			// (get) Token: 0x06006EB6 RID: 28342
			// (set) Token: 0x06006EB7 RID: 28343
			protected abstract bool HasNoElementsLeft { get; set; }

			// Token: 0x170012EC RID: 4844
			// (get) Token: 0x06006EB8 RID: 28344
			public abstract KeyValuePair<long, TSource> Current { get; }

			// Token: 0x06006EB9 RID: 28345
			public abstract void Dispose();

			// Token: 0x06006EBA RID: 28346 RVA: 0x0017F2C1 File Offset: 0x0017D4C1
			public void Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170012ED RID: 4845
			// (get) Token: 0x06006EBB RID: 28347 RVA: 0x0017F2C8 File Offset: 0x0017D4C8
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06006EBC RID: 28348 RVA: 0x0017F2D8 File Offset: 0x0017D4D8
			public bool MoveNext()
			{
				if (this.m_localOffset == null)
				{
					this.m_localOffset = new Partitioner.SharedInt(-1);
					this.m_currentChunkSize = new Partitioner.SharedInt(0);
					this.m_doublingCountdown = 3;
				}
				if (this.m_localOffset.Value < this.m_currentChunkSize.Value - 1)
				{
					this.m_localOffset.Value++;
					return true;
				}
				int num;
				if (this.m_currentChunkSize.Value == 0)
				{
					num = 1;
				}
				else if (this.m_doublingCountdown > 0)
				{
					num = this.m_currentChunkSize.Value;
				}
				else
				{
					num = Math.Min(this.m_currentChunkSize.Value * 2, this.m_maxChunkSize);
					this.m_doublingCountdown = 3;
				}
				this.m_doublingCountdown--;
				if (this.GrabNextChunk(num))
				{
					this.m_localOffset.Value = 0;
					return true;
				}
				return false;
			}

			// Token: 0x040035B6 RID: 13750
			protected readonly TSourceReader m_sharedReader;

			// Token: 0x040035B7 RID: 13751
			protected static int s_defaultMaxChunkSize = Partitioner.GetDefaultChunkSize<TSource>();

			// Token: 0x040035B8 RID: 13752
			protected Partitioner.SharedInt m_currentChunkSize;

			// Token: 0x040035B9 RID: 13753
			protected Partitioner.SharedInt m_localOffset;

			// Token: 0x040035BA RID: 13754
			private const int CHUNK_DOUBLING_RATE = 3;

			// Token: 0x040035BB RID: 13755
			private int m_doublingCountdown;

			// Token: 0x040035BC RID: 13756
			protected readonly int m_maxChunkSize;

			// Token: 0x040035BD RID: 13757
			protected readonly Partitioner.SharedLong m_sharedIndex;
		}

		// Token: 0x02000BC7 RID: 3015
		private class DynamicPartitionerForIEnumerable<TSource> : OrderablePartitioner<TSource>
		{
			// Token: 0x06006EBE RID: 28350 RVA: 0x0017F3C5 File Offset: 0x0017D5C5
			internal DynamicPartitionerForIEnumerable(IEnumerable<TSource> source, EnumerablePartitionerOptions partitionerOptions)
				: base(true, false, true)
			{
				this.m_source = source;
				this.m_useSingleChunking = (partitionerOptions & EnumerablePartitionerOptions.NoBuffering) > EnumerablePartitionerOptions.None;
			}

			// Token: 0x06006EBF RID: 28351 RVA: 0x0017F3E4 File Offset: 0x0017D5E4
			public override IList<IEnumerator<KeyValuePair<long, TSource>>> GetOrderablePartitions(int partitionCount)
			{
				if (partitionCount <= 0)
				{
					throw new ArgumentOutOfRangeException("partitionCount");
				}
				IEnumerator<KeyValuePair<long, TSource>>[] array = new IEnumerator<KeyValuePair<long, TSource>>[partitionCount];
				IEnumerable<KeyValuePair<long, TSource>> enumerable = new Partitioner.DynamicPartitionerForIEnumerable<TSource>.InternalPartitionEnumerable(this.m_source.GetEnumerator(), this.m_useSingleChunking, true);
				for (int i = 0; i < partitionCount; i++)
				{
					array[i] = enumerable.GetEnumerator();
				}
				return array;
			}

			// Token: 0x06006EC0 RID: 28352 RVA: 0x0017F435 File Offset: 0x0017D635
			public override IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions()
			{
				return new Partitioner.DynamicPartitionerForIEnumerable<TSource>.InternalPartitionEnumerable(this.m_source.GetEnumerator(), this.m_useSingleChunking, false);
			}

			// Token: 0x170012EE RID: 4846
			// (get) Token: 0x06006EC1 RID: 28353 RVA: 0x0017F44E File Offset: 0x0017D64E
			public override bool SupportsDynamicPartitions
			{
				get
				{
					return true;
				}
			}

			// Token: 0x040035BE RID: 13758
			private IEnumerable<TSource> m_source;

			// Token: 0x040035BF RID: 13759
			private readonly bool m_useSingleChunking;

			// Token: 0x02000D00 RID: 3328
			private class InternalPartitionEnumerable : IEnumerable<KeyValuePair<long, TSource>>, IEnumerable, IDisposable
			{
				// Token: 0x0600720E RID: 29198 RVA: 0x0018A39C File Offset: 0x0018859C
				internal InternalPartitionEnumerable(IEnumerator<TSource> sharedReader, bool useSingleChunking, bool isStaticPartitioning)
				{
					this.m_sharedReader = sharedReader;
					this.m_sharedIndex = new Partitioner.SharedLong(-1L);
					this.m_hasNoElementsLeft = new Partitioner.SharedBool(false);
					this.m_sourceDepleted = new Partitioner.SharedBool(false);
					this.m_sharedLock = new object();
					this.m_useSingleChunking = useSingleChunking;
					if (!this.m_useSingleChunking)
					{
						int num = ((PlatformHelper.ProcessorCount > 4) ? 4 : 1);
						this.m_FillBuffer = new KeyValuePair<long, TSource>[num * Partitioner.GetDefaultChunkSize<TSource>()];
					}
					if (isStaticPartitioning)
					{
						this.m_activePartitionCount = new Partitioner.SharedInt(0);
						return;
					}
					this.m_activePartitionCount = null;
				}

				// Token: 0x0600720F RID: 29199 RVA: 0x0018A430 File Offset: 0x00188630
				public IEnumerator<KeyValuePair<long, TSource>> GetEnumerator()
				{
					if (this.m_disposed)
					{
						throw new ObjectDisposedException(Environment.GetResourceString("PartitionerStatic_CanNotCallGetEnumeratorAfterSourceHasBeenDisposed"));
					}
					return new Partitioner.DynamicPartitionerForIEnumerable<TSource>.InternalPartitionEnumerator(this.m_sharedReader, this.m_sharedIndex, this.m_hasNoElementsLeft, this.m_sharedLock, this.m_activePartitionCount, this, this.m_useSingleChunking);
				}

				// Token: 0x06007210 RID: 29200 RVA: 0x0018A47F File Offset: 0x0018867F
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x06007211 RID: 29201 RVA: 0x0018A488 File Offset: 0x00188688
				private void TryCopyFromFillBuffer(KeyValuePair<long, TSource>[] destArray, int requestedChunkSize, ref int actualNumElementsGrabbed)
				{
					actualNumElementsGrabbed = 0;
					KeyValuePair<long, TSource>[] fillBuffer = this.m_FillBuffer;
					if (fillBuffer == null)
					{
						return;
					}
					if (this.m_FillBufferCurrentPosition >= this.m_FillBufferSize)
					{
						return;
					}
					Interlocked.Increment(ref this.m_activeCopiers);
					int num = Interlocked.Add(ref this.m_FillBufferCurrentPosition, requestedChunkSize);
					int num2 = num - requestedChunkSize;
					if (num2 < this.m_FillBufferSize)
					{
						actualNumElementsGrabbed = ((num < this.m_FillBufferSize) ? num : (this.m_FillBufferSize - num2));
						Array.Copy(fillBuffer, num2, destArray, 0, actualNumElementsGrabbed);
					}
					Interlocked.Decrement(ref this.m_activeCopiers);
				}

				// Token: 0x06007212 RID: 29202 RVA: 0x0018A511 File Offset: 0x00188711
				internal bool GrabChunk(KeyValuePair<long, TSource>[] destArray, int requestedChunkSize, ref int actualNumElementsGrabbed)
				{
					actualNumElementsGrabbed = 0;
					if (this.m_hasNoElementsLeft.Value)
					{
						return false;
					}
					if (this.m_useSingleChunking)
					{
						return this.GrabChunk_Single(destArray, requestedChunkSize, ref actualNumElementsGrabbed);
					}
					return this.GrabChunk_Buffered(destArray, requestedChunkSize, ref actualNumElementsGrabbed);
				}

				// Token: 0x06007213 RID: 29203 RVA: 0x0018A544 File Offset: 0x00188744
				internal bool GrabChunk_Single(KeyValuePair<long, TSource>[] destArray, int requestedChunkSize, ref int actualNumElementsGrabbed)
				{
					object sharedLock = this.m_sharedLock;
					bool flag2;
					lock (sharedLock)
					{
						if (this.m_hasNoElementsLeft.Value)
						{
							flag2 = false;
						}
						else
						{
							try
							{
								if (this.m_sharedReader.MoveNext())
								{
									this.m_sharedIndex.Value = checked(this.m_sharedIndex.Value + 1L);
									destArray[0] = new KeyValuePair<long, TSource>(this.m_sharedIndex.Value, this.m_sharedReader.Current);
									actualNumElementsGrabbed = 1;
									flag2 = true;
								}
								else
								{
									this.m_sourceDepleted.Value = true;
									this.m_hasNoElementsLeft.Value = true;
									flag2 = false;
								}
							}
							catch
							{
								this.m_sourceDepleted.Value = true;
								this.m_hasNoElementsLeft.Value = true;
								throw;
							}
						}
					}
					return flag2;
				}

				// Token: 0x06007214 RID: 29204 RVA: 0x0018A630 File Offset: 0x00188830
				internal bool GrabChunk_Buffered(KeyValuePair<long, TSource>[] destArray, int requestedChunkSize, ref int actualNumElementsGrabbed)
				{
					this.TryCopyFromFillBuffer(destArray, requestedChunkSize, ref actualNumElementsGrabbed);
					if (actualNumElementsGrabbed == requestedChunkSize)
					{
						return true;
					}
					if (this.m_sourceDepleted.Value)
					{
						this.m_hasNoElementsLeft.Value = true;
						this.m_FillBuffer = null;
						return actualNumElementsGrabbed > 0;
					}
					object sharedLock = this.m_sharedLock;
					lock (sharedLock)
					{
						if (this.m_sourceDepleted.Value)
						{
							return actualNumElementsGrabbed > 0;
						}
						try
						{
							if (this.m_activeCopiers > 0)
							{
								SpinWait spinWait = default(SpinWait);
								while (this.m_activeCopiers > 0)
								{
									spinWait.SpinOnce();
								}
							}
							while (actualNumElementsGrabbed < requestedChunkSize)
							{
								if (!this.m_sharedReader.MoveNext())
								{
									this.m_sourceDepleted.Value = true;
									break;
								}
								this.m_sharedIndex.Value = checked(this.m_sharedIndex.Value + 1L);
								destArray[actualNumElementsGrabbed] = new KeyValuePair<long, TSource>(this.m_sharedIndex.Value, this.m_sharedReader.Current);
								actualNumElementsGrabbed++;
							}
							KeyValuePair<long, TSource>[] fillBuffer = this.m_FillBuffer;
							if (!this.m_sourceDepleted.Value && fillBuffer != null && this.m_FillBufferCurrentPosition >= fillBuffer.Length)
							{
								for (int i = 0; i < fillBuffer.Length; i++)
								{
									if (!this.m_sharedReader.MoveNext())
									{
										this.m_sourceDepleted.Value = true;
										this.m_FillBufferSize = i;
										break;
									}
									this.m_sharedIndex.Value = checked(this.m_sharedIndex.Value + 1L);
									fillBuffer[i] = new KeyValuePair<long, TSource>(this.m_sharedIndex.Value, this.m_sharedReader.Current);
								}
								this.m_FillBufferCurrentPosition = 0;
							}
						}
						catch
						{
							this.m_sourceDepleted.Value = true;
							this.m_hasNoElementsLeft.Value = true;
							throw;
						}
					}
					return actualNumElementsGrabbed > 0;
				}

				// Token: 0x06007215 RID: 29205 RVA: 0x0018A84C File Offset: 0x00188A4C
				public void Dispose()
				{
					if (!this.m_disposed)
					{
						this.m_disposed = true;
						this.m_sharedReader.Dispose();
					}
				}

				// Token: 0x0400393D RID: 14653
				private readonly IEnumerator<TSource> m_sharedReader;

				// Token: 0x0400393E RID: 14654
				private Partitioner.SharedLong m_sharedIndex;

				// Token: 0x0400393F RID: 14655
				private volatile KeyValuePair<long, TSource>[] m_FillBuffer;

				// Token: 0x04003940 RID: 14656
				private volatile int m_FillBufferSize;

				// Token: 0x04003941 RID: 14657
				private volatile int m_FillBufferCurrentPosition;

				// Token: 0x04003942 RID: 14658
				private volatile int m_activeCopiers;

				// Token: 0x04003943 RID: 14659
				private Partitioner.SharedBool m_hasNoElementsLeft;

				// Token: 0x04003944 RID: 14660
				private Partitioner.SharedBool m_sourceDepleted;

				// Token: 0x04003945 RID: 14661
				private object m_sharedLock;

				// Token: 0x04003946 RID: 14662
				private bool m_disposed;

				// Token: 0x04003947 RID: 14663
				private Partitioner.SharedInt m_activePartitionCount;

				// Token: 0x04003948 RID: 14664
				private readonly bool m_useSingleChunking;
			}

			// Token: 0x02000D01 RID: 3329
			private class InternalPartitionEnumerator : Partitioner.DynamicPartitionEnumerator_Abstract<TSource, IEnumerator<TSource>>
			{
				// Token: 0x06007216 RID: 29206 RVA: 0x0018A868 File Offset: 0x00188A68
				internal InternalPartitionEnumerator(IEnumerator<TSource> sharedReader, Partitioner.SharedLong sharedIndex, Partitioner.SharedBool hasNoElementsLeft, object sharedLock, Partitioner.SharedInt activePartitionCount, Partitioner.DynamicPartitionerForIEnumerable<TSource>.InternalPartitionEnumerable enumerable, bool useSingleChunking)
					: base(sharedReader, sharedIndex, useSingleChunking)
				{
					this.m_hasNoElementsLeft = hasNoElementsLeft;
					this.m_sharedLock = sharedLock;
					this.m_enumerable = enumerable;
					this.m_activePartitionCount = activePartitionCount;
					if (this.m_activePartitionCount != null)
					{
						Interlocked.Increment(ref this.m_activePartitionCount.Value);
					}
				}

				// Token: 0x06007217 RID: 29207 RVA: 0x0018A8B8 File Offset: 0x00188AB8
				protected override bool GrabNextChunk(int requestedChunkSize)
				{
					if (this.HasNoElementsLeft)
					{
						return false;
					}
					if (this.m_localList == null)
					{
						this.m_localList = new KeyValuePair<long, TSource>[this.m_maxChunkSize];
					}
					return this.m_enumerable.GrabChunk(this.m_localList, requestedChunkSize, ref this.m_currentChunkSize.Value);
				}

				// Token: 0x17001386 RID: 4998
				// (get) Token: 0x06007218 RID: 29208 RVA: 0x0018A905 File Offset: 0x00188B05
				// (set) Token: 0x06007219 RID: 29209 RVA: 0x0018A914 File Offset: 0x00188B14
				protected override bool HasNoElementsLeft
				{
					get
					{
						return this.m_hasNoElementsLeft.Value;
					}
					set
					{
						this.m_hasNoElementsLeft.Value = true;
					}
				}

				// Token: 0x17001387 RID: 4999
				// (get) Token: 0x0600721A RID: 29210 RVA: 0x0018A924 File Offset: 0x00188B24
				public override KeyValuePair<long, TSource> Current
				{
					get
					{
						if (this.m_currentChunkSize == null)
						{
							throw new InvalidOperationException(Environment.GetResourceString("PartitionerStatic_CurrentCalledBeforeMoveNext"));
						}
						return this.m_localList[this.m_localOffset.Value];
					}
				}

				// Token: 0x0600721B RID: 29211 RVA: 0x0018A956 File Offset: 0x00188B56
				public override void Dispose()
				{
					if (this.m_activePartitionCount != null && Interlocked.Decrement(ref this.m_activePartitionCount.Value) == 0)
					{
						this.m_enumerable.Dispose();
					}
				}

				// Token: 0x04003949 RID: 14665
				private KeyValuePair<long, TSource>[] m_localList;

				// Token: 0x0400394A RID: 14666
				private readonly Partitioner.SharedBool m_hasNoElementsLeft;

				// Token: 0x0400394B RID: 14667
				private readonly object m_sharedLock;

				// Token: 0x0400394C RID: 14668
				private readonly Partitioner.SharedInt m_activePartitionCount;

				// Token: 0x0400394D RID: 14669
				private Partitioner.DynamicPartitionerForIEnumerable<TSource>.InternalPartitionEnumerable m_enumerable;
			}
		}

		// Token: 0x02000BC8 RID: 3016
		private abstract class DynamicPartitionerForIndexRange_Abstract<TSource, TCollection> : OrderablePartitioner<TSource>
		{
			// Token: 0x06006EC2 RID: 28354 RVA: 0x0017F451 File Offset: 0x0017D651
			protected DynamicPartitionerForIndexRange_Abstract(TCollection data)
				: base(true, false, true)
			{
				this.m_data = data;
			}

			// Token: 0x06006EC3 RID: 28355
			protected abstract IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions_Factory(TCollection data);

			// Token: 0x06006EC4 RID: 28356 RVA: 0x0017F464 File Offset: 0x0017D664
			public override IList<IEnumerator<KeyValuePair<long, TSource>>> GetOrderablePartitions(int partitionCount)
			{
				if (partitionCount <= 0)
				{
					throw new ArgumentOutOfRangeException("partitionCount");
				}
				IEnumerator<KeyValuePair<long, TSource>>[] array = new IEnumerator<KeyValuePair<long, TSource>>[partitionCount];
				IEnumerable<KeyValuePair<long, TSource>> orderableDynamicPartitions_Factory = this.GetOrderableDynamicPartitions_Factory(this.m_data);
				for (int i = 0; i < partitionCount; i++)
				{
					array[i] = orderableDynamicPartitions_Factory.GetEnumerator();
				}
				return array;
			}

			// Token: 0x06006EC5 RID: 28357 RVA: 0x0017F4AA File Offset: 0x0017D6AA
			public override IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions()
			{
				return this.GetOrderableDynamicPartitions_Factory(this.m_data);
			}

			// Token: 0x170012EF RID: 4847
			// (get) Token: 0x06006EC6 RID: 28358 RVA: 0x0017F4B8 File Offset: 0x0017D6B8
			public override bool SupportsDynamicPartitions
			{
				get
				{
					return true;
				}
			}

			// Token: 0x040035C0 RID: 13760
			private TCollection m_data;
		}

		// Token: 0x02000BC9 RID: 3017
		private abstract class DynamicPartitionEnumeratorForIndexRange_Abstract<TSource, TSourceReader> : Partitioner.DynamicPartitionEnumerator_Abstract<TSource, TSourceReader>
		{
			// Token: 0x06006EC7 RID: 28359 RVA: 0x0017F4BB File Offset: 0x0017D6BB
			protected DynamicPartitionEnumeratorForIndexRange_Abstract(TSourceReader sharedReader, Partitioner.SharedLong sharedIndex)
				: base(sharedReader, sharedIndex)
			{
			}

			// Token: 0x170012F0 RID: 4848
			// (get) Token: 0x06006EC8 RID: 28360
			protected abstract int SourceCount { get; }

			// Token: 0x06006EC9 RID: 28361 RVA: 0x0017F4C8 File Offset: 0x0017D6C8
			protected override bool GrabNextChunk(int requestedChunkSize)
			{
				while (!this.HasNoElementsLeft)
				{
					long num = Volatile.Read(ref this.m_sharedIndex.Value);
					if (this.HasNoElementsLeft)
					{
						return false;
					}
					long num2 = Math.Min((long)(this.SourceCount - 1), num + (long)requestedChunkSize);
					if (Interlocked.CompareExchange(ref this.m_sharedIndex.Value, num2, num) == num)
					{
						this.m_currentChunkSize.Value = (int)(num2 - num);
						this.m_localOffset.Value = -1;
						this.m_startIndex = (int)(num + 1L);
						return true;
					}
				}
				return false;
			}

			// Token: 0x170012F1 RID: 4849
			// (get) Token: 0x06006ECA RID: 28362 RVA: 0x0017F54F File Offset: 0x0017D74F
			// (set) Token: 0x06006ECB RID: 28363 RVA: 0x0017F56F File Offset: 0x0017D76F
			protected override bool HasNoElementsLeft
			{
				get
				{
					return Volatile.Read(ref this.m_sharedIndex.Value) >= (long)(this.SourceCount - 1);
				}
				set
				{
				}
			}

			// Token: 0x06006ECC RID: 28364 RVA: 0x0017F571 File Offset: 0x0017D771
			public override void Dispose()
			{
			}

			// Token: 0x040035C1 RID: 13761
			protected int m_startIndex;
		}

		// Token: 0x02000BCA RID: 3018
		private class DynamicPartitionerForIList<TSource> : Partitioner.DynamicPartitionerForIndexRange_Abstract<TSource, IList<TSource>>
		{
			// Token: 0x06006ECD RID: 28365 RVA: 0x0017F573 File Offset: 0x0017D773
			internal DynamicPartitionerForIList(IList<TSource> source)
				: base(source)
			{
			}

			// Token: 0x06006ECE RID: 28366 RVA: 0x0017F57C File Offset: 0x0017D77C
			protected override IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions_Factory(IList<TSource> m_data)
			{
				return new Partitioner.DynamicPartitionerForIList<TSource>.InternalPartitionEnumerable(m_data);
			}

			// Token: 0x02000D02 RID: 3330
			private class InternalPartitionEnumerable : IEnumerable<KeyValuePair<long, TSource>>, IEnumerable
			{
				// Token: 0x0600721C RID: 29212 RVA: 0x0018A97D File Offset: 0x00188B7D
				internal InternalPartitionEnumerable(IList<TSource> sharedReader)
				{
					this.m_sharedReader = sharedReader;
					this.m_sharedIndex = new Partitioner.SharedLong(-1L);
				}

				// Token: 0x0600721D RID: 29213 RVA: 0x0018A999 File Offset: 0x00188B99
				public IEnumerator<KeyValuePair<long, TSource>> GetEnumerator()
				{
					return new Partitioner.DynamicPartitionerForIList<TSource>.InternalPartitionEnumerator(this.m_sharedReader, this.m_sharedIndex);
				}

				// Token: 0x0600721E RID: 29214 RVA: 0x0018A9AC File Offset: 0x00188BAC
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x0400394E RID: 14670
				private readonly IList<TSource> m_sharedReader;

				// Token: 0x0400394F RID: 14671
				private Partitioner.SharedLong m_sharedIndex;
			}

			// Token: 0x02000D03 RID: 3331
			private class InternalPartitionEnumerator : Partitioner.DynamicPartitionEnumeratorForIndexRange_Abstract<TSource, IList<TSource>>
			{
				// Token: 0x0600721F RID: 29215 RVA: 0x0018A9B4 File Offset: 0x00188BB4
				internal InternalPartitionEnumerator(IList<TSource> sharedReader, Partitioner.SharedLong sharedIndex)
					: base(sharedReader, sharedIndex)
				{
				}

				// Token: 0x17001388 RID: 5000
				// (get) Token: 0x06007220 RID: 29216 RVA: 0x0018A9BE File Offset: 0x00188BBE
				protected override int SourceCount
				{
					get
					{
						return this.m_sharedReader.Count;
					}
				}

				// Token: 0x17001389 RID: 5001
				// (get) Token: 0x06007221 RID: 29217 RVA: 0x0018A9CC File Offset: 0x00188BCC
				public override KeyValuePair<long, TSource> Current
				{
					get
					{
						if (this.m_currentChunkSize == null)
						{
							throw new InvalidOperationException(Environment.GetResourceString("PartitionerStatic_CurrentCalledBeforeMoveNext"));
						}
						return new KeyValuePair<long, TSource>((long)(this.m_startIndex + this.m_localOffset.Value), this.m_sharedReader[this.m_startIndex + this.m_localOffset.Value]);
					}
				}
			}
		}

		// Token: 0x02000BCB RID: 3019
		private class DynamicPartitionerForArray<TSource> : Partitioner.DynamicPartitionerForIndexRange_Abstract<TSource, TSource[]>
		{
			// Token: 0x06006ECF RID: 28367 RVA: 0x0017F584 File Offset: 0x0017D784
			internal DynamicPartitionerForArray(TSource[] source)
				: base(source)
			{
			}

			// Token: 0x06006ED0 RID: 28368 RVA: 0x0017F58D File Offset: 0x0017D78D
			protected override IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions_Factory(TSource[] m_data)
			{
				return new Partitioner.DynamicPartitionerForArray<TSource>.InternalPartitionEnumerable(m_data);
			}

			// Token: 0x02000D04 RID: 3332
			private class InternalPartitionEnumerable : IEnumerable<KeyValuePair<long, TSource>>, IEnumerable
			{
				// Token: 0x06007222 RID: 29218 RVA: 0x0018AA2A File Offset: 0x00188C2A
				internal InternalPartitionEnumerable(TSource[] sharedReader)
				{
					this.m_sharedReader = sharedReader;
					this.m_sharedIndex = new Partitioner.SharedLong(-1L);
				}

				// Token: 0x06007223 RID: 29219 RVA: 0x0018AA46 File Offset: 0x00188C46
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x06007224 RID: 29220 RVA: 0x0018AA4E File Offset: 0x00188C4E
				public IEnumerator<KeyValuePair<long, TSource>> GetEnumerator()
				{
					return new Partitioner.DynamicPartitionerForArray<TSource>.InternalPartitionEnumerator(this.m_sharedReader, this.m_sharedIndex);
				}

				// Token: 0x04003950 RID: 14672
				private readonly TSource[] m_sharedReader;

				// Token: 0x04003951 RID: 14673
				private Partitioner.SharedLong m_sharedIndex;
			}

			// Token: 0x02000D05 RID: 3333
			private class InternalPartitionEnumerator : Partitioner.DynamicPartitionEnumeratorForIndexRange_Abstract<TSource, TSource[]>
			{
				// Token: 0x06007225 RID: 29221 RVA: 0x0018AA61 File Offset: 0x00188C61
				internal InternalPartitionEnumerator(TSource[] sharedReader, Partitioner.SharedLong sharedIndex)
					: base(sharedReader, sharedIndex)
				{
				}

				// Token: 0x1700138A RID: 5002
				// (get) Token: 0x06007226 RID: 29222 RVA: 0x0018AA6B File Offset: 0x00188C6B
				protected override int SourceCount
				{
					get
					{
						return this.m_sharedReader.Length;
					}
				}

				// Token: 0x1700138B RID: 5003
				// (get) Token: 0x06007227 RID: 29223 RVA: 0x0018AA78 File Offset: 0x00188C78
				public override KeyValuePair<long, TSource> Current
				{
					get
					{
						if (this.m_currentChunkSize == null)
						{
							throw new InvalidOperationException(Environment.GetResourceString("PartitionerStatic_CurrentCalledBeforeMoveNext"));
						}
						return new KeyValuePair<long, TSource>((long)(this.m_startIndex + this.m_localOffset.Value), this.m_sharedReader[this.m_startIndex + this.m_localOffset.Value]);
					}
				}
			}
		}

		// Token: 0x02000BCC RID: 3020
		private abstract class StaticIndexRangePartitioner<TSource, TCollection> : OrderablePartitioner<TSource>
		{
			// Token: 0x06006ED1 RID: 28369 RVA: 0x0017F595 File Offset: 0x0017D795
			protected StaticIndexRangePartitioner()
				: base(true, true, true)
			{
			}

			// Token: 0x170012F2 RID: 4850
			// (get) Token: 0x06006ED2 RID: 28370
			protected abstract int SourceCount { get; }

			// Token: 0x06006ED3 RID: 28371
			protected abstract IEnumerator<KeyValuePair<long, TSource>> CreatePartition(int startIndex, int endIndex);

			// Token: 0x06006ED4 RID: 28372 RVA: 0x0017F5A0 File Offset: 0x0017D7A0
			public override IList<IEnumerator<KeyValuePair<long, TSource>>> GetOrderablePartitions(int partitionCount)
			{
				if (partitionCount <= 0)
				{
					throw new ArgumentOutOfRangeException("partitionCount");
				}
				int num2;
				int num = Math.DivRem(this.SourceCount, partitionCount, out num2);
				IEnumerator<KeyValuePair<long, TSource>>[] array = new IEnumerator<KeyValuePair<long, TSource>>[partitionCount];
				int num3 = -1;
				for (int i = 0; i < partitionCount; i++)
				{
					int num4 = num3 + 1;
					if (i < num2)
					{
						num3 = num4 + num;
					}
					else
					{
						num3 = num4 + num - 1;
					}
					array[i] = this.CreatePartition(num4, num3);
				}
				return array;
			}
		}

		// Token: 0x02000BCD RID: 3021
		private abstract class StaticIndexRangePartition<TSource> : IEnumerator<KeyValuePair<long, TSource>>, IDisposable, IEnumerator
		{
			// Token: 0x06006ED5 RID: 28373 RVA: 0x0017F60A File Offset: 0x0017D80A
			protected StaticIndexRangePartition(int startIndex, int endIndex)
			{
				this.m_startIndex = startIndex;
				this.m_endIndex = endIndex;
				this.m_offset = startIndex - 1;
			}

			// Token: 0x170012F3 RID: 4851
			// (get) Token: 0x06006ED6 RID: 28374
			public abstract KeyValuePair<long, TSource> Current { get; }

			// Token: 0x06006ED7 RID: 28375 RVA: 0x0017F62B File Offset: 0x0017D82B
			public void Dispose()
			{
			}

			// Token: 0x06006ED8 RID: 28376 RVA: 0x0017F62D File Offset: 0x0017D82D
			public void Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06006ED9 RID: 28377 RVA: 0x0017F634 File Offset: 0x0017D834
			public bool MoveNext()
			{
				if (this.m_offset < this.m_endIndex)
				{
					this.m_offset++;
					return true;
				}
				this.m_offset = this.m_endIndex + 1;
				return false;
			}

			// Token: 0x170012F4 RID: 4852
			// (get) Token: 0x06006EDA RID: 28378 RVA: 0x0017F66B File Offset: 0x0017D86B
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x040035C2 RID: 13762
			protected readonly int m_startIndex;

			// Token: 0x040035C3 RID: 13763
			protected readonly int m_endIndex;

			// Token: 0x040035C4 RID: 13764
			protected volatile int m_offset;
		}

		// Token: 0x02000BCE RID: 3022
		private class StaticIndexRangePartitionerForIList<TSource> : Partitioner.StaticIndexRangePartitioner<TSource, IList<TSource>>
		{
			// Token: 0x06006EDB RID: 28379 RVA: 0x0017F678 File Offset: 0x0017D878
			internal StaticIndexRangePartitionerForIList(IList<TSource> list)
			{
				this.m_list = list;
			}

			// Token: 0x170012F5 RID: 4853
			// (get) Token: 0x06006EDC RID: 28380 RVA: 0x0017F687 File Offset: 0x0017D887
			protected override int SourceCount
			{
				get
				{
					return this.m_list.Count;
				}
			}

			// Token: 0x06006EDD RID: 28381 RVA: 0x0017F694 File Offset: 0x0017D894
			protected override IEnumerator<KeyValuePair<long, TSource>> CreatePartition(int startIndex, int endIndex)
			{
				return new Partitioner.StaticIndexRangePartitionForIList<TSource>(this.m_list, startIndex, endIndex);
			}

			// Token: 0x040035C5 RID: 13765
			private IList<TSource> m_list;
		}

		// Token: 0x02000BCF RID: 3023
		private class StaticIndexRangePartitionForIList<TSource> : Partitioner.StaticIndexRangePartition<TSource>
		{
			// Token: 0x06006EDE RID: 28382 RVA: 0x0017F6A3 File Offset: 0x0017D8A3
			internal StaticIndexRangePartitionForIList(IList<TSource> list, int startIndex, int endIndex)
				: base(startIndex, endIndex)
			{
				this.m_list = list;
			}

			// Token: 0x170012F6 RID: 4854
			// (get) Token: 0x06006EDF RID: 28383 RVA: 0x0017F6B8 File Offset: 0x0017D8B8
			public override KeyValuePair<long, TSource> Current
			{
				get
				{
					if (this.m_offset < this.m_startIndex)
					{
						throw new InvalidOperationException(Environment.GetResourceString("PartitionerStatic_CurrentCalledBeforeMoveNext"));
					}
					return new KeyValuePair<long, TSource>((long)this.m_offset, this.m_list[this.m_offset]);
				}
			}

			// Token: 0x040035C6 RID: 13766
			private volatile IList<TSource> m_list;
		}

		// Token: 0x02000BD0 RID: 3024
		private class StaticIndexRangePartitionerForArray<TSource> : Partitioner.StaticIndexRangePartitioner<TSource, TSource[]>
		{
			// Token: 0x06006EE0 RID: 28384 RVA: 0x0017F708 File Offset: 0x0017D908
			internal StaticIndexRangePartitionerForArray(TSource[] array)
			{
				this.m_array = array;
			}

			// Token: 0x170012F7 RID: 4855
			// (get) Token: 0x06006EE1 RID: 28385 RVA: 0x0017F717 File Offset: 0x0017D917
			protected override int SourceCount
			{
				get
				{
					return this.m_array.Length;
				}
			}

			// Token: 0x06006EE2 RID: 28386 RVA: 0x0017F721 File Offset: 0x0017D921
			protected override IEnumerator<KeyValuePair<long, TSource>> CreatePartition(int startIndex, int endIndex)
			{
				return new Partitioner.StaticIndexRangePartitionForArray<TSource>(this.m_array, startIndex, endIndex);
			}

			// Token: 0x040035C7 RID: 13767
			private TSource[] m_array;
		}

		// Token: 0x02000BD1 RID: 3025
		private class StaticIndexRangePartitionForArray<TSource> : Partitioner.StaticIndexRangePartition<TSource>
		{
			// Token: 0x06006EE3 RID: 28387 RVA: 0x0017F730 File Offset: 0x0017D930
			internal StaticIndexRangePartitionForArray(TSource[] array, int startIndex, int endIndex)
				: base(startIndex, endIndex)
			{
				this.m_array = array;
			}

			// Token: 0x170012F8 RID: 4856
			// (get) Token: 0x06006EE4 RID: 28388 RVA: 0x0017F744 File Offset: 0x0017D944
			public override KeyValuePair<long, TSource> Current
			{
				get
				{
					if (this.m_offset < this.m_startIndex)
					{
						throw new InvalidOperationException(Environment.GetResourceString("PartitionerStatic_CurrentCalledBeforeMoveNext"));
					}
					return new KeyValuePair<long, TSource>((long)this.m_offset, this.m_array[this.m_offset]);
				}
			}

			// Token: 0x040035C8 RID: 13768
			private volatile TSource[] m_array;
		}

		// Token: 0x02000BD2 RID: 3026
		private class SharedInt
		{
			// Token: 0x06006EE5 RID: 28389 RVA: 0x0017F794 File Offset: 0x0017D994
			internal SharedInt(int value)
			{
				this.Value = value;
			}

			// Token: 0x040035C9 RID: 13769
			internal volatile int Value;
		}

		// Token: 0x02000BD3 RID: 3027
		private class SharedBool
		{
			// Token: 0x06006EE6 RID: 28390 RVA: 0x0017F7A5 File Offset: 0x0017D9A5
			internal SharedBool(bool value)
			{
				this.Value = value;
			}

			// Token: 0x040035CA RID: 13770
			internal volatile bool Value;
		}

		// Token: 0x02000BD4 RID: 3028
		private class SharedLong
		{
			// Token: 0x06006EE7 RID: 28391 RVA: 0x0017F7B6 File Offset: 0x0017D9B6
			internal SharedLong(long value)
			{
				this.Value = value;
			}

			// Token: 0x040035CB RID: 13771
			internal long Value;
		}
	}
}
