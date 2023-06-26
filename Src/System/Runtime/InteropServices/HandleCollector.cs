using System;
using System.Threading;

namespace System.Runtime.InteropServices
{
	/// <summary>Tracks outstanding handles and forces a garbage collection when the specified threshold is reached.</summary>
	// Token: 0x020003DA RID: 986
	[global::__DynamicallyInvokable]
	public sealed class HandleCollector
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.HandleCollector" /> class using a name and a threshold at which to begin handle collection.</summary>
		/// <param name="name">A name for the collector. This parameter allows you to name collectors that track handle types separately.</param>
		/// <param name="initialThreshold">A value that specifies the point at which collections should begin.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="initialThreshold" /> parameter is less than 0.</exception>
		// Token: 0x060025E3 RID: 9699 RVA: 0x000AFFF9 File Offset: 0x000AE1F9
		[global::__DynamicallyInvokable]
		public HandleCollector(string name, int initialThreshold)
			: this(name, initialThreshold, int.MaxValue)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.HandleCollector" /> class using a name, a threshold at which to begin handle collection, and a threshold at which handle collection must occur.</summary>
		/// <param name="name">A name for the collector.  This parameter allows you to name collectors that track handle types separately.</param>
		/// <param name="initialThreshold">A value that specifies the point at which collections should begin.</param>
		/// <param name="maximumThreshold">A value that specifies the point at which collections must occur. This should be set to the maximum number of available handles.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="initialThreshold" /> parameter is less than 0.  
		///  -or-  
		///  The <paramref name="maximumThreshold" /> parameter is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="maximumThreshold" /> parameter is less than the <paramref name="initialThreshold" /> parameter.</exception>
		// Token: 0x060025E4 RID: 9700 RVA: 0x000B0008 File Offset: 0x000AE208
		[global::__DynamicallyInvokable]
		public HandleCollector(string name, int initialThreshold, int maximumThreshold)
		{
			if (initialThreshold < 0)
			{
				throw new ArgumentOutOfRangeException("initialThreshold", SR.GetString("ArgumentOutOfRange_NeedNonNegNumRequired"));
			}
			if (maximumThreshold < 0)
			{
				throw new ArgumentOutOfRangeException("maximumThreshold", SR.GetString("ArgumentOutOfRange_NeedNonNegNumRequired"));
			}
			if (initialThreshold > maximumThreshold)
			{
				throw new ArgumentException(SR.GetString("Argument_InvalidThreshold"));
			}
			if (name != null)
			{
				this.name = name;
			}
			else
			{
				this.name = string.Empty;
			}
			this.initialThreshold = initialThreshold;
			this.maximumThreshold = maximumThreshold;
			this.threshold = initialThreshold;
			this.handleCount = 0;
		}

		/// <summary>Gets the number of handles collected.</summary>
		/// <returns>The number of handles collected.</returns>
		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x060025E5 RID: 9701 RVA: 0x000B00A0 File Offset: 0x000AE2A0
		[global::__DynamicallyInvokable]
		public int Count
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.handleCount;
			}
		}

		/// <summary>Gets a value that specifies the point at which collections should begin.</summary>
		/// <returns>A value that specifies the point at which collections should begin.</returns>
		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x060025E6 RID: 9702 RVA: 0x000B00A8 File Offset: 0x000AE2A8
		[global::__DynamicallyInvokable]
		public int InitialThreshold
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.initialThreshold;
			}
		}

		/// <summary>Gets a value that specifies the point at which collections must occur.</summary>
		/// <returns>A value that specifies the point at which collections must occur.</returns>
		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x060025E7 RID: 9703 RVA: 0x000B00B0 File Offset: 0x000AE2B0
		[global::__DynamicallyInvokable]
		public int MaximumThreshold
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.maximumThreshold;
			}
		}

		/// <summary>Gets the name of a <see cref="T:System.Runtime.InteropServices.HandleCollector" /> object.</summary>
		/// <returns>This <see cref="P:System.Runtime.InteropServices.HandleCollector.Name" /> property allows you to name collectors that track handle types separately.</returns>
		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x060025E8 RID: 9704 RVA: 0x000B00B8 File Offset: 0x000AE2B8
		[global::__DynamicallyInvokable]
		public string Name
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.name;
			}
		}

		/// <summary>Increments the current handle count.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Runtime.InteropServices.HandleCollector.Count" /> property is less than 0.</exception>
		// Token: 0x060025E9 RID: 9705 RVA: 0x000B00C0 File Offset: 0x000AE2C0
		[global::__DynamicallyInvokable]
		public void Add()
		{
			int num = -1;
			Interlocked.Increment(ref this.handleCount);
			if (this.handleCount < 0)
			{
				throw new InvalidOperationException(SR.GetString("InvalidOperation_HCCountOverflow"));
			}
			if (this.handleCount > this.threshold)
			{
				lock (this)
				{
					this.threshold = this.handleCount + this.handleCount / 10;
					num = this.gc_gen;
					if (this.gc_gen < 2)
					{
						this.gc_gen++;
					}
				}
			}
			if (num >= 0 && (num == 0 || this.gc_counts[num] == GC.CollectionCount(num)))
			{
				GC.Collect(num);
				Thread.Sleep(10 * num);
			}
			for (int i = 1; i < 3; i++)
			{
				this.gc_counts[i] = GC.CollectionCount(i);
			}
		}

		/// <summary>Decrements the current handle count.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Runtime.InteropServices.HandleCollector.Count" /> property is less than 0.</exception>
		// Token: 0x060025EA RID: 9706 RVA: 0x000B01A0 File Offset: 0x000AE3A0
		[global::__DynamicallyInvokable]
		public void Remove()
		{
			Interlocked.Decrement(ref this.handleCount);
			if (this.handleCount < 0)
			{
				throw new InvalidOperationException(SR.GetString("InvalidOperation_HCCountOverflow"));
			}
			int num = this.handleCount + this.handleCount / 10;
			if (num < this.threshold - this.threshold / 10)
			{
				lock (this)
				{
					if (num > this.initialThreshold)
					{
						this.threshold = num;
					}
					else
					{
						this.threshold = this.initialThreshold;
					}
					this.gc_gen = 0;
				}
			}
			for (int i = 1; i < 3; i++)
			{
				this.gc_counts[i] = GC.CollectionCount(i);
			}
		}

		// Token: 0x04002064 RID: 8292
		private const int deltaPercent = 10;

		// Token: 0x04002065 RID: 8293
		private string name;

		// Token: 0x04002066 RID: 8294
		private int initialThreshold;

		// Token: 0x04002067 RID: 8295
		private int maximumThreshold;

		// Token: 0x04002068 RID: 8296
		private int threshold;

		// Token: 0x04002069 RID: 8297
		private int handleCount;

		// Token: 0x0400206A RID: 8298
		private int[] gc_counts = new int[3];

		// Token: 0x0400206B RID: 8299
		private int gc_gen;
	}
}
