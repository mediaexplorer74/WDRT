using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>Specifies whether a calendar is solar-based, lunar-based, or lunisolar-based.</summary>
	// Token: 0x020003A2 RID: 930
	[ComVisible(true)]
	public enum CalendarAlgorithmType
	{
		/// <summary>An unknown calendar basis.</summary>
		// Token: 0x040012FF RID: 4863
		Unknown,
		/// <summary>A solar-based calendar.</summary>
		// Token: 0x04001300 RID: 4864
		SolarCalendar,
		/// <summary>A lunar-based calendar.</summary>
		// Token: 0x04001301 RID: 4865
		LunarCalendar,
		/// <summary>A lunisolar-based calendar.</summary>
		// Token: 0x04001302 RID: 4866
		LunisolarCalendar
	}
}
