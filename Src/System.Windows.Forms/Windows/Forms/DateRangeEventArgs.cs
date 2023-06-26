using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.MonthCalendar.DateChanged" /> or <see cref="E:System.Windows.Forms.MonthCalendar.DateSelected" /> events of the <see cref="T:System.Windows.Forms.MonthCalendar" /> control.</summary>
	// Token: 0x02000227 RID: 551
	public class DateRangeEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DateRangeEventArgs" /> class.</summary>
		/// <param name="start">The first date/time value in the range that the user has selected.</param>
		/// <param name="end">The last date/time value in the range that the user has selected.</param>
		// Token: 0x060023CE RID: 9166 RVA: 0x000AA9C3 File Offset: 0x000A8BC3
		public DateRangeEventArgs(DateTime start, DateTime end)
		{
			this.start = start;
			this.end = end;
		}

		/// <summary>Gets the first date/time value in the range that the user has selected.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that represents the first date in the date range that the user has selected.</returns>
		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x060023CF RID: 9167 RVA: 0x000AA9D9 File Offset: 0x000A8BD9
		public DateTime Start
		{
			get
			{
				return this.start;
			}
		}

		/// <summary>Gets the last date/time value in the range that the user has selected.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that represents the last date in the date range that the user has selected.</returns>
		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x060023D0 RID: 9168 RVA: 0x000AA9E1 File Offset: 0x000A8BE1
		public DateTime End
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x04000EAC RID: 3756
		private readonly DateTime start;

		// Token: 0x04000EAD RID: 3757
		private readonly DateTime end;
	}
}
