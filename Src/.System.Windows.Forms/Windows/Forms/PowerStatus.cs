using System;

namespace System.Windows.Forms
{
	/// <summary>Indicates current system power status information.</summary>
	// Token: 0x02000321 RID: 801
	public class PowerStatus
	{
		// Token: 0x060032FB RID: 13051 RVA: 0x00002843 File Offset: 0x00000A43
		internal PowerStatus()
		{
		}

		/// <summary>Gets the current system power status.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.PowerLineStatus" /> values indicating the current system power status.</returns>
		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x060032FC RID: 13052 RVA: 0x000E38A4 File Offset: 0x000E1AA4
		public PowerLineStatus PowerLineStatus
		{
			get
			{
				this.UpdateSystemPowerStatus();
				return (PowerLineStatus)this.systemPowerStatus.ACLineStatus;
			}
		}

		/// <summary>Gets the current battery charge status.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BatteryChargeStatus" /> values indicating the current battery charge level or charging status.</returns>
		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x060032FD RID: 13053 RVA: 0x000E38B7 File Offset: 0x000E1AB7
		public BatteryChargeStatus BatteryChargeStatus
		{
			get
			{
				this.UpdateSystemPowerStatus();
				return (BatteryChargeStatus)this.systemPowerStatus.BatteryFlag;
			}
		}

		/// <summary>Gets the reported full charge lifetime of the primary battery power source in seconds.</summary>
		/// <returns>The reported number of seconds of battery life available when the battery is fully charged, or -1 if the battery life is unknown.</returns>
		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x060032FE RID: 13054 RVA: 0x000E38CA File Offset: 0x000E1ACA
		public int BatteryFullLifetime
		{
			get
			{
				this.UpdateSystemPowerStatus();
				return this.systemPowerStatus.BatteryFullLifeTime;
			}
		}

		/// <summary>Gets the approximate amount of full battery charge remaining.</summary>
		/// <returns>The approximate amount, from 0.0 to 1.0, of full battery charge remaining.</returns>
		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x060032FF RID: 13055 RVA: 0x000E38E0 File Offset: 0x000E1AE0
		public float BatteryLifePercent
		{
			get
			{
				this.UpdateSystemPowerStatus();
				float num = (float)this.systemPowerStatus.BatteryLifePercent / 100f;
				if (num <= 1f)
				{
					return num;
				}
				return 1f;
			}
		}

		/// <summary>Gets the approximate number of seconds of battery time remaining.</summary>
		/// <returns>The approximate number of seconds of battery life remaining, or -1 if the approximate remaining battery life is unknown.</returns>
		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x06003300 RID: 13056 RVA: 0x000E3915 File Offset: 0x000E1B15
		public int BatteryLifeRemaining
		{
			get
			{
				this.UpdateSystemPowerStatus();
				return this.systemPowerStatus.BatteryLifeTime;
			}
		}

		// Token: 0x06003301 RID: 13057 RVA: 0x000E3928 File Offset: 0x000E1B28
		private void UpdateSystemPowerStatus()
		{
			UnsafeNativeMethods.GetSystemPowerStatus(ref this.systemPowerStatus);
		}

		// Token: 0x04001EAD RID: 7853
		private NativeMethods.SYSTEM_POWER_STATUS systemPowerStatus;
	}
}
