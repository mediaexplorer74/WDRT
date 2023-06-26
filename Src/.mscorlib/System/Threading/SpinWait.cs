using System;
using System.Security.Permissions;

namespace System.Threading
{
	/// <summary>Provides support for spin-based waiting.</summary>
	// Token: 0x02000536 RID: 1334
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public struct SpinWait
	{
		/// <summary>Gets the number of times <see cref="M:System.Threading.SpinWait.SpinOnce" /> has been called on this instance.</summary>
		/// <returns>Returns an integer that represents the number of times <see cref="M:System.Threading.SpinWait.SpinOnce" /> has been called on this instance.</returns>
		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06003ED2 RID: 16082 RVA: 0x000EAFE3 File Offset: 0x000E91E3
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_count;
			}
		}

		/// <summary>Gets whether the next call to <see cref="M:System.Threading.SpinWait.SpinOnce" /> will yield the processor, triggering a forced context switch.</summary>
		/// <returns>Whether the next call to <see cref="M:System.Threading.SpinWait.SpinOnce" /> will yield the processor, triggering a forced context switch.</returns>
		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06003ED3 RID: 16083 RVA: 0x000EAFEB File Offset: 0x000E91EB
		[__DynamicallyInvokable]
		public bool NextSpinWillYield
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_count > 10 || PlatformHelper.IsSingleProcessor;
			}
		}

		/// <summary>Performs a single spin.</summary>
		// Token: 0x06003ED4 RID: 16084 RVA: 0x000EB000 File Offset: 0x000E9200
		[__DynamicallyInvokable]
		public void SpinOnce()
		{
			if (this.NextSpinWillYield)
			{
				CdsSyncEtwBCLProvider.Log.SpinWait_NextSpinWillYield();
				int num = ((this.m_count >= 10) ? (this.m_count - 10) : this.m_count);
				if (num % 20 == 19)
				{
					Thread.Sleep(1);
				}
				else if (num % 5 == 4)
				{
					Thread.Sleep(0);
				}
				else
				{
					Thread.Yield();
				}
			}
			else
			{
				Thread.SpinWait(4 << this.m_count);
			}
			this.m_count = ((this.m_count == int.MaxValue) ? 10 : (this.m_count + 1));
		}

		/// <summary>Resets the spin counter.</summary>
		// Token: 0x06003ED5 RID: 16085 RVA: 0x000EB090 File Offset: 0x000E9290
		[__DynamicallyInvokable]
		public void Reset()
		{
			this.m_count = 0;
		}

		/// <summary>Spins until the specified condition is satisfied.</summary>
		/// <param name="condition">A delegate to be executed over and over until it returns true.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="condition" /> argument is null.</exception>
		// Token: 0x06003ED6 RID: 16086 RVA: 0x000EB099 File Offset: 0x000E9299
		[__DynamicallyInvokable]
		public static void SpinUntil(Func<bool> condition)
		{
			SpinWait.SpinUntil(condition, -1);
		}

		/// <summary>Spins until the specified condition is satisfied or until the specified timeout is expired.</summary>
		/// <param name="condition">A delegate to be executed over and over until it returns true.</param>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a TimeSpan that represents -1 milliseconds to wait indefinitely.</param>
		/// <returns>True if the condition is satisfied within the timeout; otherwise, false</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="condition" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out -or- timeout is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06003ED7 RID: 16087 RVA: 0x000EB0A4 File Offset: 0x000E92A4
		[__DynamicallyInvokable]
		public static bool SpinUntil(Func<bool> condition, TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", timeout, Environment.GetResourceString("SpinWait_SpinUntil_TimeoutWrong"));
			}
			return SpinWait.SpinUntil(condition, (int)timeout.TotalMilliseconds);
		}

		/// <summary>Spins until the specified condition is satisfied or until the specified timeout is expired.</summary>
		/// <param name="condition">A delegate to be executed over and over until it returns true.</param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		/// <returns>True if the condition is satisfied within the timeout; otherwise, false</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="condition" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		// Token: 0x06003ED8 RID: 16088 RVA: 0x000EB0F4 File Offset: 0x000E92F4
		[__DynamicallyInvokable]
		public static bool SpinUntil(Func<bool> condition, int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", millisecondsTimeout, Environment.GetResourceString("SpinWait_SpinUntil_TimeoutWrong"));
			}
			if (condition == null)
			{
				throw new ArgumentNullException("condition", Environment.GetResourceString("SpinWait_SpinUntil_ArgumentNull"));
			}
			uint num = 0U;
			if (millisecondsTimeout != 0 && millisecondsTimeout != -1)
			{
				num = TimeoutHelper.GetTime();
			}
			SpinWait spinWait = default(SpinWait);
			while (!condition())
			{
				if (millisecondsTimeout == 0)
				{
					return false;
				}
				spinWait.SpinOnce();
				if (millisecondsTimeout != -1 && spinWait.NextSpinWillYield && (long)millisecondsTimeout <= (long)((ulong)(TimeoutHelper.GetTime() - num)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04001A6A RID: 6762
		internal const int YIELD_THRESHOLD = 10;

		// Token: 0x04001A6B RID: 6763
		internal const int SLEEP_0_EVERY_HOW_MANY_TIMES = 5;

		// Token: 0x04001A6C RID: 6764
		internal const int SLEEP_1_EVERY_HOW_MANY_TIMES = 20;

		// Token: 0x04001A6D RID: 6765
		private int m_count;
	}
}
