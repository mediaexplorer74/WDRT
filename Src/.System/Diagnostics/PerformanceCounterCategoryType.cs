using System;

namespace System.Diagnostics
{
	/// <summary>Indicates whether the performance counter category can have multiple instances.</summary>
	// Token: 0x020004E0 RID: 1248
	public enum PerformanceCounterCategoryType
	{
		/// <summary>The instance functionality for the performance counter category is unknown.</summary>
		// Token: 0x040027A9 RID: 10153
		Unknown = -1,
		/// <summary>The performance counter category can have only a single instance.</summary>
		// Token: 0x040027AA RID: 10154
		SingleInstance,
		/// <summary>The performance counter category can have multiple instances.</summary>
		// Token: 0x040027AB RID: 10155
		MultiInstance
	}
}
