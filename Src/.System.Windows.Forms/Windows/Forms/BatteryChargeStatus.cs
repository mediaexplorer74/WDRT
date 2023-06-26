using System;

namespace System.Windows.Forms
{
	/// <summary>Defines identifiers that indicate the current battery charge level or charging state information.</summary>
	// Token: 0x0200031F RID: 799
	[Flags]
	public enum BatteryChargeStatus
	{
		/// <summary>Indicates a high level of battery charge.</summary>
		// Token: 0x04001EA4 RID: 7844
		High = 1,
		/// <summary>Indicates a low level of battery charge.</summary>
		// Token: 0x04001EA5 RID: 7845
		Low = 2,
		/// <summary>Indicates a critically low level of battery charge.</summary>
		// Token: 0x04001EA6 RID: 7846
		Critical = 4,
		/// <summary>Indicates a battery is charging.</summary>
		// Token: 0x04001EA7 RID: 7847
		Charging = 8,
		/// <summary>Indicates that no battery is present.</summary>
		// Token: 0x04001EA8 RID: 7848
		NoSystemBattery = 128,
		/// <summary>Indicates an unknown battery condition.</summary>
		// Token: 0x04001EA9 RID: 7849
		Unknown = 255
	}
}
