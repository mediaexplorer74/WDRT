using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Represents a date selection range in a month calendar control.</summary>
	// Token: 0x02000365 RID: 869
	[TypeConverter(typeof(SelectionRangeConverter))]
	public sealed class SelectionRange
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.SelectionRange" /> class.</summary>
		// Token: 0x06003883 RID: 14467 RVA: 0x000FA850 File Offset: 0x000F8A50
		public SelectionRange()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.SelectionRange" /> class with the specified beginning and ending dates.</summary>
		/// <param name="lower">The starting date in the <see cref="T:System.Windows.Forms.SelectionRange" />.</param>
		/// <param name="upper">The ending date in the <see cref="T:System.Windows.Forms.SelectionRange" />.</param>
		// Token: 0x06003884 RID: 14468 RVA: 0x000FA88C File Offset: 0x000F8A8C
		public SelectionRange(DateTime lower, DateTime upper)
		{
			if (lower < upper)
			{
				this.start = lower.Date;
				this.end = upper.Date;
				return;
			}
			this.start = upper.Date;
			this.end = lower.Date;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.SelectionRange" /> class with the specified selection range.</summary>
		/// <param name="range">The existing <see cref="T:System.Windows.Forms.SelectionRange" />.</param>
		// Token: 0x06003885 RID: 14469 RVA: 0x000FA904 File Offset: 0x000F8B04
		public SelectionRange(SelectionRange range)
		{
			this.start = range.start;
			this.end = range.end;
		}

		/// <summary>Gets or sets the ending date and time of the selection range.</summary>
		/// <returns>The ending <see cref="T:System.DateTime" /> value of the range.</returns>
		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x06003886 RID: 14470 RVA: 0x000FA955 File Offset: 0x000F8B55
		// (set) Token: 0x06003887 RID: 14471 RVA: 0x000FA95D File Offset: 0x000F8B5D
		public DateTime End
		{
			get
			{
				return this.end;
			}
			set
			{
				this.end = value.Date;
			}
		}

		/// <summary>Gets or sets the starting date and time of the selection range.</summary>
		/// <returns>The starting <see cref="T:System.DateTime" /> value of the range.</returns>
		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x06003888 RID: 14472 RVA: 0x000FA96C File Offset: 0x000F8B6C
		// (set) Token: 0x06003889 RID: 14473 RVA: 0x000FA974 File Offset: 0x000F8B74
		public DateTime Start
		{
			get
			{
				return this.start;
			}
			set
			{
				this.start = value.Date;
			}
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.SelectionRange" />.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.SelectionRange" />.</returns>
		// Token: 0x0600388A RID: 14474 RVA: 0x000FA983 File Offset: 0x000F8B83
		public override string ToString()
		{
			return "SelectionRange: Start: " + this.start.ToString() + ", End: " + this.end.ToString();
		}

		// Token: 0x040021CA RID: 8650
		private DateTime start = DateTime.MinValue.Date;

		// Token: 0x040021CB RID: 8651
		private DateTime end = DateTime.MaxValue.Date;
	}
}
