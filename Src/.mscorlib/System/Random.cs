using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Represents a pseudo-random number generator, which is a device that produces a sequence of numbers that meet certain statistical requirements for randomness.</summary>
	// Token: 0x02000126 RID: 294
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class Random
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Random" /> class, using a time-dependent default seed value.</summary>
		// Token: 0x060010F7 RID: 4343 RVA: 0x00032FCB File Offset: 0x000311CB
		[__DynamicallyInvokable]
		public Random()
			: this(Environment.TickCount)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Random" /> class, using the specified seed value.</summary>
		/// <param name="Seed">A number used to calculate a starting value for the pseudo-random number sequence. If a negative number is specified, the absolute value of the number is used.</param>
		// Token: 0x060010F8 RID: 4344 RVA: 0x00032FD8 File Offset: 0x000311D8
		[__DynamicallyInvokable]
		public Random(int Seed)
		{
			int num = ((Seed == int.MinValue) ? int.MaxValue : Math.Abs(Seed));
			int num2 = 161803398 - num;
			this.SeedArray[55] = num2;
			int num3 = 1;
			for (int i = 1; i < 55; i++)
			{
				int num4 = 21 * i % 55;
				this.SeedArray[num4] = num3;
				num3 = num2 - num3;
				if (num3 < 0)
				{
					num3 += int.MaxValue;
				}
				num2 = this.SeedArray[num4];
			}
			for (int j = 1; j < 5; j++)
			{
				for (int k = 1; k < 56; k++)
				{
					this.SeedArray[k] -= this.SeedArray[1 + (k + 30) % 55];
					if (this.SeedArray[k] < 0)
					{
						this.SeedArray[k] += int.MaxValue;
					}
				}
			}
			this.inext = 0;
			this.inextp = 21;
			Seed = 1;
		}

		/// <summary>Returns a random floating-point number between 0.0 and 1.0.</summary>
		/// <returns>A double-precision floating point number that is greater than or equal to 0.0, and less than 1.0.</returns>
		// Token: 0x060010F9 RID: 4345 RVA: 0x000330D5 File Offset: 0x000312D5
		[__DynamicallyInvokable]
		protected virtual double Sample()
		{
			return (double)this.InternalSample() * 4.6566128752457969E-10;
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x000330E8 File Offset: 0x000312E8
		private int InternalSample()
		{
			int num = this.inext;
			int num2 = this.inextp;
			if (++num >= 56)
			{
				num = 1;
			}
			if (++num2 >= 56)
			{
				num2 = 1;
			}
			int num3 = this.SeedArray[num] - this.SeedArray[num2];
			if (num3 == 2147483647)
			{
				num3--;
			}
			if (num3 < 0)
			{
				num3 += int.MaxValue;
			}
			this.SeedArray[num] = num3;
			this.inext = num;
			this.inextp = num2;
			return num3;
		}

		/// <summary>Returns a non-negative random integer.</summary>
		/// <returns>A 32-bit signed integer that is greater than or equal to 0 and less than <see cref="F:System.Int32.MaxValue" />.</returns>
		// Token: 0x060010FB RID: 4347 RVA: 0x0003315B File Offset: 0x0003135B
		[__DynamicallyInvokable]
		public virtual int Next()
		{
			return this.InternalSample();
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x00033164 File Offset: 0x00031364
		private double GetSampleForLargeRange()
		{
			int num = this.InternalSample();
			bool flag = this.InternalSample() % 2 == 0;
			if (flag)
			{
				num = -num;
			}
			double num2 = (double)num;
			num2 += 2147483646.0;
			return num2 / 4294967293.0;
		}

		/// <summary>Returns a random integer that is within a specified range.</summary>
		/// <param name="minValue">The inclusive lower bound of the random number returned.</param>
		/// <param name="maxValue">The exclusive upper bound of the random number returned. <paramref name="maxValue" /> must be greater than or equal to <paramref name="minValue" />.</param>
		/// <returns>A 32-bit signed integer greater than or equal to <paramref name="minValue" /> and less than <paramref name="maxValue" />; that is, the range of return values includes <paramref name="minValue" /> but not <paramref name="maxValue" />. If <paramref name="minValue" /> equals <paramref name="maxValue" />, <paramref name="minValue" /> is returned.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="minValue" /> is greater than <paramref name="maxValue" />.</exception>
		// Token: 0x060010FD RID: 4349 RVA: 0x000331AC File Offset: 0x000313AC
		[__DynamicallyInvokable]
		public virtual int Next(int minValue, int maxValue)
		{
			if (minValue > maxValue)
			{
				throw new ArgumentOutOfRangeException("minValue", Environment.GetResourceString("Argument_MinMaxValue", new object[] { "minValue", "maxValue" }));
			}
			long num = (long)maxValue - (long)minValue;
			if (num <= 2147483647L)
			{
				return (int)(this.Sample() * (double)num) + minValue;
			}
			return (int)((long)(this.GetSampleForLargeRange() * (double)num) + (long)minValue);
		}

		/// <summary>Returns a non-negative random integer that is less than the specified maximum.</summary>
		/// <param name="maxValue">The exclusive upper bound of the random number to be generated. <paramref name="maxValue" /> must be greater than or equal to 0.</param>
		/// <returns>A 32-bit signed integer that is greater than or equal to 0, and less than <paramref name="maxValue" />; that is, the range of return values ordinarily includes 0 but not <paramref name="maxValue" />. However, if <paramref name="maxValue" /> equals 0, <paramref name="maxValue" /> is returned.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maxValue" /> is less than 0.</exception>
		// Token: 0x060010FE RID: 4350 RVA: 0x00033212 File Offset: 0x00031412
		[__DynamicallyInvokable]
		public virtual int Next(int maxValue)
		{
			if (maxValue < 0)
			{
				throw new ArgumentOutOfRangeException("maxValue", Environment.GetResourceString("ArgumentOutOfRange_MustBePositive", new object[] { "maxValue" }));
			}
			return (int)(this.Sample() * (double)maxValue);
		}

		/// <summary>Returns a random floating-point number that is greater than or equal to 0.0, and less than 1.0.</summary>
		/// <returns>A double-precision floating point number that is greater than or equal to 0.0, and less than 1.0.</returns>
		// Token: 0x060010FF RID: 4351 RVA: 0x00033245 File Offset: 0x00031445
		[__DynamicallyInvokable]
		public virtual double NextDouble()
		{
			return this.Sample();
		}

		/// <summary>Fills the elements of a specified array of bytes with random numbers.</summary>
		/// <param name="buffer">An array of bytes to contain random numbers.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		// Token: 0x06001100 RID: 4352 RVA: 0x00033250 File Offset: 0x00031450
		[__DynamicallyInvokable]
		public virtual void NextBytes(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			for (int i = 0; i < buffer.Length; i++)
			{
				buffer[i] = (byte)(this.InternalSample() % 256);
			}
		}

		// Token: 0x040005F0 RID: 1520
		private const int MBIG = 2147483647;

		// Token: 0x040005F1 RID: 1521
		private const int MSEED = 161803398;

		// Token: 0x040005F2 RID: 1522
		private const int MZ = 0;

		// Token: 0x040005F3 RID: 1523
		private int inext;

		// Token: 0x040005F4 RID: 1524
		private int inextp;

		// Token: 0x040005F5 RID: 1525
		private int[] SeedArray = new int[56];
	}
}
