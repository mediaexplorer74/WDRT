using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for events that are internal to the <see cref="T:System.Windows.Forms.MonthCalendar" /> control.</summary>
	// Token: 0x02000225 RID: 549
	public class DateBoldEventArgs : EventArgs
	{
		// Token: 0x060023C5 RID: 9157 RVA: 0x000AA98C File Offset: 0x000A8B8C
		internal DateBoldEventArgs(DateTime start, int size)
		{
			this.startDate = start;
			this.size = size;
		}

		/// <summary>Gets the first date that is bold.</summary>
		/// <returns>The first date that is bold.</returns>
		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x060023C6 RID: 9158 RVA: 0x000AA9A2 File Offset: 0x000A8BA2
		public DateTime StartDate
		{
			get
			{
				return this.startDate;
			}
		}

		/// <summary>Gets the number of dates that are bold.</summary>
		/// <returns>The number of dates that are bold.</returns>
		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x060023C7 RID: 9159 RVA: 0x000AA9AA File Offset: 0x000A8BAA
		public int Size
		{
			get
			{
				return this.size;
			}
		}

		/// <summary>Gets or sets dates that are bold.</summary>
		/// <returns>The dates that are bold.</returns>
		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x060023C8 RID: 9160 RVA: 0x000AA9B2 File Offset: 0x000A8BB2
		// (set) Token: 0x060023C9 RID: 9161 RVA: 0x000AA9BA File Offset: 0x000A8BBA
		public int[] DaysToBold
		{
			get
			{
				return this.daysToBold;
			}
			set
			{
				this.daysToBold = value;
			}
		}

		// Token: 0x04000EA9 RID: 3753
		private readonly DateTime startDate;

		// Token: 0x04000EAA RID: 3754
		private readonly int size;

		// Token: 0x04000EAB RID: 3755
		private int[] daysToBold;
	}
}
