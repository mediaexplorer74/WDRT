using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Helpers
{
	// Token: 0x02000033 RID: 51
	public class SpeedCalculator
	{
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0000CA67 File Offset: 0x0000AC67
		// (set) Token: 0x06000336 RID: 822 RVA: 0x0000CA6F File Offset: 0x0000AC6F
		public long TotalFilesSize { get; private set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0000CA78 File Offset: 0x0000AC78
		// (set) Token: 0x06000338 RID: 824 RVA: 0x0000CA80 File Offset: 0x0000AC80
		public long PreviousDownloadedSize { get; set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000CA89 File Offset: 0x0000AC89
		// (set) Token: 0x0600033A RID: 826 RVA: 0x0000CA91 File Offset: 0x0000AC91
		public long CurrentDownloadedSize { get; set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0000CA9A File Offset: 0x0000AC9A
		// (set) Token: 0x0600033C RID: 828 RVA: 0x0000CAA2 File Offset: 0x0000ACA2
		public long CurrentPartlyDownloadedSize { get; set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000CAAB File Offset: 0x0000ACAB
		// (set) Token: 0x0600033E RID: 830 RVA: 0x0000CAB3 File Offset: 0x0000ACB3
		public bool IsResumed { get; set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000CABC File Offset: 0x0000ACBC
		// (set) Token: 0x06000340 RID: 832 RVA: 0x0000CAC4 File Offset: 0x0000ACC4
		public bool IsDownloadStarted { get; set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000CAD0 File Offset: 0x0000ACD0
		public long TotalDownloadedSize
		{
			get
			{
				return this.PreviousDownloadedSize + this.CurrentDownloadedSize + this.CurrentPartlyDownloadedSize;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0000CAF6 File Offset: 0x0000ACF6
		// (set) Token: 0x06000343 RID: 835 RVA: 0x0000CAFE File Offset: 0x0000ACFE
		public long RemaingSeconds { get; set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000344 RID: 836 RVA: 0x0000CB07 File Offset: 0x0000AD07
		// (set) Token: 0x06000345 RID: 837 RVA: 0x0000CB0F File Offset: 0x0000AD0F
		public double BytesPerSecond { get; private set; }

		// Token: 0x06000346 RID: 838 RVA: 0x0000CB18 File Offset: 0x0000AD18
		public void Start(long totalFilesSize, long previousDownloadedSize = 0L)
		{
			this.Reset();
			this.packetsInOneSecond = new Queue<long>();
			this.TotalFilesSize = totalFilesSize;
			this.PreviousDownloadedSize = previousDownloadedSize;
			this.oneSecondTick = new Timer
			{
				Interval = 1000.0
			};
			this.queueCapacity = 100;
			this.smoothFactor = 0.0005;
			this.oneSecondTick.Start();
			this.oneSecondTick.Elapsed += this.OneSecondTickElapsed;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000CBA0 File Offset: 0x0000ADA0
		public void Stop()
		{
			bool flag = this.oneSecondTick != null;
			if (flag)
			{
				this.oneSecondTick.Stop();
				this.oneSecondTick.Elapsed -= this.OneSecondTickElapsed;
			}
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000CBE4 File Offset: 0x0000ADE4
		public void Reset()
		{
			this.Stop();
			this.PreviousDownloadedSize = 0L;
			this.CurrentDownloadedSize = 0L;
			this.CurrentPartlyDownloadedSize = 0L;
			this.TotalFilesSize = 0L;
			this.RemaingSeconds = 0L;
			this.BytesPerSecond = 0.0;
			this.IsDownloadStarted = false;
			bool flag = this.packetsInOneSecond != null;
			if (flag)
			{
				this.packetsInOneSecond.Clear();
			}
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000CC5C File Offset: 0x0000AE5C
		private void OneSecondTickElapsed(object sender, ElapsedEventArgs e)
		{
			this.packetsInOneSecond.Enqueue(this.CurrentDownloadedSize);
			bool flag = this.packetsInOneSecond.Count > 5;
			if (flag)
			{
				long num = ((this.packetsInOneSecond.Count > this.queueCapacity) ? this.packetsInOneSecond.Dequeue() : this.packetsInOneSecond.First<long>());
				this.BytesPerSecond = this.smoothFactor * (double)(this.packetsInOneSecond.Last<long>() - this.packetsInOneSecond.ElementAt(this.packetsInOneSecond.Count - 2)) / 2.0 + (1.0 - this.smoothFactor) * (double)(this.packetsInOneSecond.Last<long>() - num) / (double)this.packetsInOneSecond.Count;
				bool flag2 = this.BytesPerSecond > 1.0;
				if (flag2)
				{
					long num2 = this.TotalFilesSize - this.TotalDownloadedSize;
					this.RemaingSeconds = (long)((double)num2 / this.BytesPerSecond);
				}
			}
		}

		// Token: 0x04000140 RID: 320
		private Timer oneSecondTick;

		// Token: 0x04000141 RID: 321
		private int queueCapacity;

		// Token: 0x04000142 RID: 322
		private double smoothFactor;

		// Token: 0x04000143 RID: 323
		private Queue<long> packetsInOneSecond;
	}
}
