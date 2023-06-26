using System;

namespace System.Windows.Forms
{
	/// <summary>Provides information specifying how acceleration should be performed on a spin box (also known as an up-down control) when the up or down button is pressed for specified time period.</summary>
	// Token: 0x0200030D RID: 781
	public class NumericUpDownAcceleration
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> class.</summary>
		/// <param name="seconds">The number of seconds the up or down button is pressed before the acceleration starts.</param>
		/// <param name="increment">The quantity the value displayed in the control should be incremented or decremented during acceleration.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="seconds" /> or <paramref name="increment" /> is less than 0.</exception>
		// Token: 0x060031F0 RID: 12784 RVA: 0x000E0FA0 File Offset: 0x000DF1A0
		public NumericUpDownAcceleration(int seconds, decimal increment)
		{
			if (seconds < 0)
			{
				throw new ArgumentOutOfRangeException("seconds", seconds, SR.GetString("NumericUpDownLessThanZeroError"));
			}
			if (increment < 0m)
			{
				throw new ArgumentOutOfRangeException("increment", increment, SR.GetString("NumericUpDownLessThanZeroError"));
			}
			this.seconds = seconds;
			this.increment = increment;
		}

		/// <summary>Gets or sets the number of seconds the up or down button must be pressed before the acceleration starts.</summary>
		/// <returns>Gets or sets the number of seconds the up or down button must be pressed before the acceleration starts.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The set value is less than 0.</exception>
		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x060031F1 RID: 12785 RVA: 0x000E1008 File Offset: 0x000DF208
		// (set) Token: 0x060031F2 RID: 12786 RVA: 0x000E1010 File Offset: 0x000DF210
		public int Seconds
		{
			get
			{
				return this.seconds;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("seconds", value, SR.GetString("NumericUpDownLessThanZeroError"));
				}
				this.seconds = value;
			}
		}

		/// <summary>Gets or sets the quantity to increment or decrement the displayed value during acceleration.</summary>
		/// <returns>The quantity to increment or decrement the displayed value during acceleration.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The set value is less than 0.</exception>
		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x060031F3 RID: 12787 RVA: 0x000E1038 File Offset: 0x000DF238
		// (set) Token: 0x060031F4 RID: 12788 RVA: 0x000E1040 File Offset: 0x000DF240
		public decimal Increment
		{
			get
			{
				return this.increment;
			}
			set
			{
				if (value < 0m)
				{
					throw new ArgumentOutOfRangeException("increment", value, SR.GetString("NumericUpDownLessThanZeroError"));
				}
				this.increment = value;
			}
		}

		// Token: 0x04001E52 RID: 7762
		private int seconds;

		// Token: 0x04001E53 RID: 7763
		private decimal increment;
	}
}
