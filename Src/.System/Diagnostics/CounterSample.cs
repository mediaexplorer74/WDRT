using System;

namespace System.Diagnostics
{
	/// <summary>Defines a structure that holds the raw data for a performance counter.</summary>
	// Token: 0x020004C1 RID: 1217
	public struct CounterSample
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.CounterSample" /> structure and sets the <see cref="P:System.Diagnostics.CounterSample.CounterTimeStamp" /> property to 0 (zero).</summary>
		/// <param name="rawValue">The numeric value associated with the performance counter sample.</param>
		/// <param name="baseValue">An optional, base raw value for the counter, to use only if the sample is based on multiple counters.</param>
		/// <param name="counterFrequency">The frequency with which the counter is read.</param>
		/// <param name="systemFrequency">The frequency with which the system reads from the counter.</param>
		/// <param name="timeStamp">The raw time stamp.</param>
		/// <param name="timeStamp100nSec">The raw, high-fidelity time stamp.</param>
		/// <param name="counterType">A <see cref="T:System.Diagnostics.PerformanceCounterType" /> object that indicates the type of the counter for which this sample is a snapshot.</param>
		// Token: 0x06002D70 RID: 11632 RVA: 0x000CC5FD File Offset: 0x000CA7FD
		public CounterSample(long rawValue, long baseValue, long counterFrequency, long systemFrequency, long timeStamp, long timeStamp100nSec, PerformanceCounterType counterType)
		{
			this.rawValue = rawValue;
			this.baseValue = baseValue;
			this.timeStamp = timeStamp;
			this.counterFrequency = counterFrequency;
			this.counterType = counterType;
			this.timeStamp100nSec = timeStamp100nSec;
			this.systemFrequency = systemFrequency;
			this.counterTimeStamp = 0L;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.CounterSample" /> structure and sets the <see cref="P:System.Diagnostics.CounterSample.CounterTimeStamp" /> property to the value that is passed in.</summary>
		/// <param name="rawValue">The numeric value associated with the performance counter sample.</param>
		/// <param name="baseValue">An optional, base raw value for the counter, to use only if the sample is based on multiple counters.</param>
		/// <param name="counterFrequency">The frequency with which the counter is read.</param>
		/// <param name="systemFrequency">The frequency with which the system reads from the counter.</param>
		/// <param name="timeStamp">The raw time stamp.</param>
		/// <param name="timeStamp100nSec">The raw, high-fidelity time stamp.</param>
		/// <param name="counterType">A <see cref="T:System.Diagnostics.PerformanceCounterType" /> object that indicates the type of the counter for which this sample is a snapshot.</param>
		/// <param name="counterTimeStamp">The time at which the sample was taken.</param>
		// Token: 0x06002D71 RID: 11633 RVA: 0x000CC63C File Offset: 0x000CA83C
		public CounterSample(long rawValue, long baseValue, long counterFrequency, long systemFrequency, long timeStamp, long timeStamp100nSec, PerformanceCounterType counterType, long counterTimeStamp)
		{
			this.rawValue = rawValue;
			this.baseValue = baseValue;
			this.timeStamp = timeStamp;
			this.counterFrequency = counterFrequency;
			this.counterType = counterType;
			this.timeStamp100nSec = timeStamp100nSec;
			this.systemFrequency = systemFrequency;
			this.counterTimeStamp = counterTimeStamp;
		}

		/// <summary>Gets the raw value of the counter.</summary>
		/// <returns>The numeric value that is associated with the performance counter sample.</returns>
		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x06002D72 RID: 11634 RVA: 0x000CC67B File Offset: 0x000CA87B
		public long RawValue
		{
			get
			{
				return this.rawValue;
			}
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x06002D73 RID: 11635 RVA: 0x000CC683 File Offset: 0x000CA883
		internal ulong UnsignedRawValue
		{
			get
			{
				return (ulong)this.rawValue;
			}
		}

		/// <summary>Gets an optional, base raw value for the counter.</summary>
		/// <returns>The base raw value, which is used only if the sample is based on multiple counters.</returns>
		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x06002D74 RID: 11636 RVA: 0x000CC68B File Offset: 0x000CA88B
		public long BaseValue
		{
			get
			{
				return this.baseValue;
			}
		}

		/// <summary>Gets the raw system frequency.</summary>
		/// <returns>The frequency with which the system reads from the counter.</returns>
		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x06002D75 RID: 11637 RVA: 0x000CC693 File Offset: 0x000CA893
		public long SystemFrequency
		{
			get
			{
				return this.systemFrequency;
			}
		}

		/// <summary>Gets the raw counter frequency.</summary>
		/// <returns>The frequency with which the counter is read.</returns>
		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x06002D76 RID: 11638 RVA: 0x000CC69B File Offset: 0x000CA89B
		public long CounterFrequency
		{
			get
			{
				return this.counterFrequency;
			}
		}

		/// <summary>Gets the counter's time stamp.</summary>
		/// <returns>The time at which the sample was taken.</returns>
		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x06002D77 RID: 11639 RVA: 0x000CC6A3 File Offset: 0x000CA8A3
		public long CounterTimeStamp
		{
			get
			{
				return this.counterTimeStamp;
			}
		}

		/// <summary>Gets the raw time stamp.</summary>
		/// <returns>The system time stamp.</returns>
		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x06002D78 RID: 11640 RVA: 0x000CC6AB File Offset: 0x000CA8AB
		public long TimeStamp
		{
			get
			{
				return this.timeStamp;
			}
		}

		/// <summary>Gets the raw, high-fidelity time stamp.</summary>
		/// <returns>The system time stamp, represented within 0.1 millisecond.</returns>
		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x06002D79 RID: 11641 RVA: 0x000CC6B3 File Offset: 0x000CA8B3
		public long TimeStamp100nSec
		{
			get
			{
				return this.timeStamp100nSec;
			}
		}

		/// <summary>Gets the performance counter type.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.PerformanceCounterType" /> object that indicates the type of the counter for which this sample is a snapshot.</returns>
		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x06002D7A RID: 11642 RVA: 0x000CC6BB File Offset: 0x000CA8BB
		public PerformanceCounterType CounterType
		{
			get
			{
				return this.counterType;
			}
		}

		/// <summary>Calculates the performance data of the counter, using a single sample point. This method is generally used for uncalculated performance counter types.</summary>
		/// <param name="counterSample">The <see cref="T:System.Diagnostics.CounterSample" /> structure to use as a base point for calculating performance data.</param>
		/// <returns>The calculated performance value.</returns>
		// Token: 0x06002D7B RID: 11643 RVA: 0x000CC6C3 File Offset: 0x000CA8C3
		public static float Calculate(CounterSample counterSample)
		{
			return CounterSampleCalculator.ComputeCounterValue(counterSample);
		}

		/// <summary>Calculates the performance data of the counter, using two sample points. This method is generally used for calculated performance counter types, such as averages.</summary>
		/// <param name="counterSample">The <see cref="T:System.Diagnostics.CounterSample" /> structure to use as a base point for calculating performance data.</param>
		/// <param name="nextCounterSample">The <see cref="T:System.Diagnostics.CounterSample" /> structure to use as an ending point for calculating performance data.</param>
		/// <returns>The calculated performance value.</returns>
		// Token: 0x06002D7C RID: 11644 RVA: 0x000CC6CB File Offset: 0x000CA8CB
		public static float Calculate(CounterSample counterSample, CounterSample nextCounterSample)
		{
			return CounterSampleCalculator.ComputeCounterValue(counterSample, nextCounterSample);
		}

		/// <summary>Indicates whether the specified structure is a <see cref="T:System.Diagnostics.CounterSample" /> structure and is identical to the current <see cref="T:System.Diagnostics.CounterSample" /> structure.</summary>
		/// <param name="o">The <see cref="T:System.Diagnostics.CounterSample" /> structure to be compared with the current structure.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="o" /> is a <see cref="T:System.Diagnostics.CounterSample" /> structure and is identical to the current instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002D7D RID: 11645 RVA: 0x000CC6D4 File Offset: 0x000CA8D4
		public override bool Equals(object o)
		{
			return o is CounterSample && this.Equals((CounterSample)o);
		}

		/// <summary>Indicates whether the specified <see cref="T:System.Diagnostics.CounterSample" /> structure is equal to the current <see cref="T:System.Diagnostics.CounterSample" /> structure.</summary>
		/// <param name="sample">The <see cref="T:System.Diagnostics.CounterSample" /> structure to be compared with this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="sample" /> is equal to the current instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002D7E RID: 11646 RVA: 0x000CC6EC File Offset: 0x000CA8EC
		public bool Equals(CounterSample sample)
		{
			return this.rawValue == sample.rawValue && this.baseValue == sample.baseValue && this.timeStamp == sample.timeStamp && this.counterFrequency == sample.counterFrequency && this.counterType == sample.counterType && this.timeStamp100nSec == sample.timeStamp100nSec && this.systemFrequency == sample.systemFrequency && this.counterTimeStamp == sample.counterTimeStamp;
		}

		/// <summary>Gets a hash code for the current counter sample.</summary>
		/// <returns>A hash code for the current counter sample.</returns>
		// Token: 0x06002D7F RID: 11647 RVA: 0x000CC76B File Offset: 0x000CA96B
		public override int GetHashCode()
		{
			return this.rawValue.GetHashCode();
		}

		/// <summary>Returns a value that indicates whether two <see cref="T:System.Diagnostics.CounterSample" /> structures are equal.</summary>
		/// <param name="a">A <see cref="T:System.Diagnostics.CounterSample" /> structure.</param>
		/// <param name="b">Another <see cref="T:System.Diagnostics.CounterSample" /> structure to be compared to the structure specified by the <paramref name="a" /> parameter.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="a" /> and <paramref name="b" /> are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002D80 RID: 11648 RVA: 0x000CC778 File Offset: 0x000CA978
		public static bool operator ==(CounterSample a, CounterSample b)
		{
			return a.Equals(b);
		}

		/// <summary>Returns a value that indicates whether two <see cref="T:System.Diagnostics.CounterSample" /> structures are not equal.</summary>
		/// <param name="a">A <see cref="T:System.Diagnostics.CounterSample" /> structure.</param>
		/// <param name="b">Another <see cref="T:System.Diagnostics.CounterSample" /> structure to be compared to the structure specified by the <paramref name="a" /> parameter.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="a" /> and <paramref name="b" /> are not equal; otherwise, <see langword="false" /></returns>
		// Token: 0x06002D81 RID: 11649 RVA: 0x000CC782 File Offset: 0x000CA982
		public static bool operator !=(CounterSample a, CounterSample b)
		{
			return !a.Equals(b);
		}

		// Token: 0x04002711 RID: 10001
		private long rawValue;

		// Token: 0x04002712 RID: 10002
		private long baseValue;

		// Token: 0x04002713 RID: 10003
		private long timeStamp;

		// Token: 0x04002714 RID: 10004
		private long counterFrequency;

		// Token: 0x04002715 RID: 10005
		private PerformanceCounterType counterType;

		// Token: 0x04002716 RID: 10006
		private long timeStamp100nSec;

		// Token: 0x04002717 RID: 10007
		private long systemFrequency;

		// Token: 0x04002718 RID: 10008
		private long counterTimeStamp;

		/// <summary>Defines an empty, uninitialized performance counter sample of type <see langword="NumberOfItems32" />.</summary>
		// Token: 0x04002719 RID: 10009
		public static CounterSample Empty = new CounterSample(0L, 0L, 0L, 0L, 0L, 0L, PerformanceCounterType.NumberOfItems32);
	}
}
