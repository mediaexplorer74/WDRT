using System;
using Microsoft.Win32;

namespace System.Diagnostics
{
	/// <summary>Provides a set of methods and properties that you can use to accurately measure elapsed time.</summary>
	// Token: 0x02000506 RID: 1286
	[global::__DynamicallyInvokable]
	public class Stopwatch
	{
		// Token: 0x060030E6 RID: 12518 RVA: 0x000DE188 File Offset: 0x000DC388
		static Stopwatch()
		{
			if (!SafeNativeMethods.QueryPerformanceFrequency(out Stopwatch.Frequency))
			{
				Stopwatch.IsHighResolution = false;
				Stopwatch.Frequency = 10000000L;
				Stopwatch.tickFrequency = 1.0;
				return;
			}
			Stopwatch.IsHighResolution = true;
			Stopwatch.tickFrequency = 10000000.0;
			Stopwatch.tickFrequency /= (double)Stopwatch.Frequency;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Stopwatch" /> class.</summary>
		// Token: 0x060030E7 RID: 12519 RVA: 0x000DE1E8 File Offset: 0x000DC3E8
		[global::__DynamicallyInvokable]
		public Stopwatch()
		{
			this.Reset();
		}

		/// <summary>Starts, or resumes, measuring elapsed time for an interval.</summary>
		// Token: 0x060030E8 RID: 12520 RVA: 0x000DE1F6 File Offset: 0x000DC3F6
		[global::__DynamicallyInvokable]
		public void Start()
		{
			if (!this.isRunning)
			{
				this.startTimeStamp = Stopwatch.GetTimestamp();
				this.isRunning = true;
			}
		}

		/// <summary>Initializes a new <see cref="T:System.Diagnostics.Stopwatch" /> instance, sets the elapsed time property to zero, and starts measuring elapsed time.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.Stopwatch" /> that has just begun measuring elapsed time.</returns>
		// Token: 0x060030E9 RID: 12521 RVA: 0x000DE214 File Offset: 0x000DC414
		[global::__DynamicallyInvokable]
		public static Stopwatch StartNew()
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			return stopwatch;
		}

		/// <summary>Stops measuring elapsed time for an interval.</summary>
		// Token: 0x060030EA RID: 12522 RVA: 0x000DE230 File Offset: 0x000DC430
		[global::__DynamicallyInvokable]
		public void Stop()
		{
			if (this.isRunning)
			{
				long timestamp = Stopwatch.GetTimestamp();
				long num = timestamp - this.startTimeStamp;
				this.elapsed += num;
				this.isRunning = false;
				if (this.elapsed < 0L)
				{
					this.elapsed = 0L;
				}
			}
		}

		/// <summary>Stops time interval measurement and resets the elapsed time to zero.</summary>
		// Token: 0x060030EB RID: 12523 RVA: 0x000DE27B File Offset: 0x000DC47B
		[global::__DynamicallyInvokable]
		public void Reset()
		{
			this.elapsed = 0L;
			this.isRunning = false;
			this.startTimeStamp = 0L;
		}

		/// <summary>Stops time interval measurement, resets the elapsed time to zero, and starts measuring elapsed time.</summary>
		// Token: 0x060030EC RID: 12524 RVA: 0x000DE294 File Offset: 0x000DC494
		[global::__DynamicallyInvokable]
		public void Restart()
		{
			this.elapsed = 0L;
			this.startTimeStamp = Stopwatch.GetTimestamp();
			this.isRunning = true;
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Diagnostics.Stopwatch" /> timer is running.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Diagnostics.Stopwatch" /> instance is currently running and measuring elapsed time for an interval; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x060030ED RID: 12525 RVA: 0x000DE2B0 File Offset: 0x000DC4B0
		[global::__DynamicallyInvokable]
		public bool IsRunning
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.isRunning;
			}
		}

		/// <summary>Gets the total elapsed time measured by the current instance.</summary>
		/// <returns>A read-only <see cref="T:System.TimeSpan" /> representing the total elapsed time measured by the current instance.</returns>
		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x060030EE RID: 12526 RVA: 0x000DE2B8 File Offset: 0x000DC4B8
		[global::__DynamicallyInvokable]
		public TimeSpan Elapsed
		{
			[global::__DynamicallyInvokable]
			get
			{
				return new TimeSpan(this.GetElapsedDateTimeTicks());
			}
		}

		/// <summary>Gets the total elapsed time measured by the current instance, in milliseconds.</summary>
		/// <returns>A read-only long integer representing the total number of milliseconds measured by the current instance.</returns>
		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x060030EF RID: 12527 RVA: 0x000DE2C5 File Offset: 0x000DC4C5
		[global::__DynamicallyInvokable]
		public long ElapsedMilliseconds
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.GetElapsedDateTimeTicks() / 10000L;
			}
		}

		/// <summary>Gets the total elapsed time measured by the current instance, in timer ticks.</summary>
		/// <returns>A read-only long integer representing the total number of timer ticks measured by the current instance.</returns>
		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x060030F0 RID: 12528 RVA: 0x000DE2D4 File Offset: 0x000DC4D4
		[global::__DynamicallyInvokable]
		public long ElapsedTicks
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.GetRawElapsedTicks();
			}
		}

		/// <summary>Gets the current number of ticks in the timer mechanism.</summary>
		/// <returns>A long integer representing the tick counter value of the underlying timer mechanism.</returns>
		// Token: 0x060030F1 RID: 12529 RVA: 0x000DE2DC File Offset: 0x000DC4DC
		[global::__DynamicallyInvokable]
		public static long GetTimestamp()
		{
			if (Stopwatch.IsHighResolution)
			{
				long num = 0L;
				SafeNativeMethods.QueryPerformanceCounter(out num);
				return num;
			}
			return DateTime.UtcNow.Ticks;
		}

		// Token: 0x060030F2 RID: 12530 RVA: 0x000DE30C File Offset: 0x000DC50C
		private long GetRawElapsedTicks()
		{
			long num = this.elapsed;
			if (this.isRunning)
			{
				long timestamp = Stopwatch.GetTimestamp();
				long num2 = timestamp - this.startTimeStamp;
				num += num2;
			}
			return num;
		}

		// Token: 0x060030F3 RID: 12531 RVA: 0x000DE33C File Offset: 0x000DC53C
		private long GetElapsedDateTimeTicks()
		{
			long rawElapsedTicks = this.GetRawElapsedTicks();
			if (Stopwatch.IsHighResolution)
			{
				double num = (double)rawElapsedTicks;
				num *= Stopwatch.tickFrequency;
				return (long)num;
			}
			return rawElapsedTicks;
		}

		// Token: 0x040028C5 RID: 10437
		private const long TicksPerMillisecond = 10000L;

		// Token: 0x040028C6 RID: 10438
		private const long TicksPerSecond = 10000000L;

		// Token: 0x040028C7 RID: 10439
		private long elapsed;

		// Token: 0x040028C8 RID: 10440
		private long startTimeStamp;

		// Token: 0x040028C9 RID: 10441
		private bool isRunning;

		/// <summary>Gets the frequency of the timer as the number of ticks per second. This field is read-only.</summary>
		// Token: 0x040028CA RID: 10442
		[global::__DynamicallyInvokable]
		public static readonly long Frequency;

		/// <summary>Indicates whether the timer is based on a high-resolution performance counter. This field is read-only.</summary>
		// Token: 0x040028CB RID: 10443
		[global::__DynamicallyInvokable]
		public static readonly bool IsHighResolution;

		// Token: 0x040028CC RID: 10444
		private static readonly double tickFrequency;
	}
}
