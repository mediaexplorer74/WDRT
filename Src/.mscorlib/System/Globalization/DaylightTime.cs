using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>Defines the period of daylight saving time.</summary>
	// Token: 0x020003B2 RID: 946
	[ComVisible(true)]
	[Serializable]
	public class DaylightTime
	{
		// Token: 0x06002F58 RID: 12120 RVA: 0x000B70EC File Offset: 0x000B52EC
		private DaylightTime()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.DaylightTime" /> class with the specified start, end, and time difference information.</summary>
		/// <param name="start">The object that represents the date and time when daylight saving time begins. The value must be in local time.</param>
		/// <param name="end">The object that represents the date and time when daylight saving time ends. The value must be in local time.</param>
		/// <param name="delta">The object that represents the difference between standard time and daylight saving time, in ticks.</param>
		// Token: 0x06002F59 RID: 12121 RVA: 0x000B70F4 File Offset: 0x000B52F4
		public DaylightTime(DateTime start, DateTime end, TimeSpan delta)
		{
			this.m_start = start;
			this.m_end = end;
			this.m_delta = delta;
		}

		/// <summary>Gets the object that represents the date and time when the daylight saving period begins.</summary>
		/// <returns>The object that represents the date and time when the daylight saving period begins. The value is in local time.</returns>
		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06002F5A RID: 12122 RVA: 0x000B7111 File Offset: 0x000B5311
		public DateTime Start
		{
			get
			{
				return this.m_start;
			}
		}

		/// <summary>Gets the object that represents the date and time when the daylight saving period ends.</summary>
		/// <returns>The object that represents the date and time when the daylight saving period ends. The value is in local time.</returns>
		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06002F5B RID: 12123 RVA: 0x000B7119 File Offset: 0x000B5319
		public DateTime End
		{
			get
			{
				return this.m_end;
			}
		}

		/// <summary>Gets the time interval that represents the difference between standard time and daylight saving time.</summary>
		/// <returns>The time interval that represents the difference between standard time and daylight saving time.</returns>
		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06002F5C RID: 12124 RVA: 0x000B7121 File Offset: 0x000B5321
		public TimeSpan Delta
		{
			get
			{
				return this.m_delta;
			}
		}

		// Token: 0x0400140D RID: 5133
		internal DateTime m_start;

		// Token: 0x0400140E RID: 5134
		internal DateTime m_end;

		// Token: 0x0400140F RID: 5135
		internal TimeSpan m_delta;
	}
}
