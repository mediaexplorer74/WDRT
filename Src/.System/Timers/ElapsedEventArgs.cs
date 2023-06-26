using System;

namespace System.Timers
{
	/// <summary>Provides data for the <see cref="E:System.Timers.Timer.Elapsed" /> event.</summary>
	// Token: 0x0200006B RID: 107
	public class ElapsedEventArgs : EventArgs
	{
		// Token: 0x0600046D RID: 1133 RVA: 0x0001EEF4 File Offset: 0x0001D0F4
		internal ElapsedEventArgs(int low, int high)
		{
			long num = ((long)high << 32) | ((long)low & (long)((ulong)(-1)));
			this.signalTime = DateTime.FromFileTime(num);
		}

		/// <summary>Gets the date/time when the <see cref="E:System.Timers.Timer.Elapsed" /> event was raised.</summary>
		/// <returns>The time the <see cref="E:System.Timers.Timer.Elapsed" /> event was raised.</returns>
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x0001EF1F File Offset: 0x0001D11F
		public DateTime SignalTime
		{
			get
			{
				return this.signalTime;
			}
		}

		// Token: 0x04000BCA RID: 3018
		private DateTime signalTime;
	}
}
