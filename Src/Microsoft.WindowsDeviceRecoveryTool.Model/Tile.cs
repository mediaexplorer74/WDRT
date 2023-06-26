using System;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.WindowsDeviceRecoveryTool.Common;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;

namespace Microsoft.WindowsDeviceRecoveryTool.Model
{
	// Token: 0x02000018 RID: 24
	public class Tile : NotificationObject
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600015F RID: 351 RVA: 0x00005789 File Offset: 0x00003989
		// (remove) Token: 0x06000160 RID: 352 RVA: 0x00005799 File Offset: 0x00003999
		public event EventHandler OnRemoveTimerElapsed
		{
			add
			{
				this.removeTimer.Tick += value;
			}
			remove
			{
				this.removeTimer.Stop();
				this.removeTimer.Tick -= value;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000161 RID: 353 RVA: 0x000057B8 File Offset: 0x000039B8
		// (set) Token: 0x06000162 RID: 354 RVA: 0x000057D0 File Offset: 0x000039D0
		public string Title
		{
			get
			{
				return this.title;
			}
			set
			{
				base.SetValue<string>(() => this.Title, ref this.title, value);
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00005810 File Offset: 0x00003A10
		// (set) Token: 0x06000164 RID: 356 RVA: 0x00005818 File Offset: 0x00003A18
		public PhoneTypes PhoneType { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00005824 File Offset: 0x00003A24
		// (set) Token: 0x06000166 RID: 358 RVA: 0x0000583C File Offset: 0x00003A3C
		public ImageSource Image
		{
			get
			{
				return this.image;
			}
			set
			{
				base.SetValue<ImageSource>(() => this.Image, ref this.image, value);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000167 RID: 359 RVA: 0x0000587C File Offset: 0x00003A7C
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00005884 File Offset: 0x00003A84
		public Phone Phone { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00005890 File Offset: 0x00003A90
		// (set) Token: 0x0600016A RID: 362 RVA: 0x000058A8 File Offset: 0x00003AA8
		public bool IsEnabled
		{
			get
			{
				return this.isEnabled;
			}
			set
			{
				base.SetValue<bool>(() => this.IsEnabled, ref this.isEnabled, value);
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600016B RID: 363 RVA: 0x000058E8 File Offset: 0x00003AE8
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00005900 File Offset: 0x00003B00
		public bool IsWaiting
		{
			get
			{
				return this.isWaiting;
			}
			set
			{
				base.SetValue<bool>(() => this.IsWaiting, ref this.isWaiting, value);
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00005940 File Offset: 0x00003B40
		// (set) Token: 0x0600016E RID: 366 RVA: 0x00005948 File Offset: 0x00003B48
		public bool ShowStartAnimation { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00005954 File Offset: 0x00003B54
		// (set) Token: 0x06000170 RID: 368 RVA: 0x0000596C File Offset: 0x00003B6C
		public bool IsDeleted
		{
			get
			{
				return this.isDeleted;
			}
			set
			{
				base.SetValue<bool>(() => this.IsDeleted, ref this.isDeleted, value);
				this.StartRemoveTimer();
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000171 RID: 369 RVA: 0x000059BE File Offset: 0x00003BBE
		// (set) Token: 0x06000172 RID: 370 RVA: 0x000059C6 File Offset: 0x00003BC6
		public string DevicePath { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000173 RID: 371 RVA: 0x000059CF File Offset: 0x00003BCF
		// (set) Token: 0x06000174 RID: 372 RVA: 0x000059D7 File Offset: 0x00003BD7
		public Guid SupportId { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000175 RID: 373 RVA: 0x000059E0 File Offset: 0x00003BE0
		// (set) Token: 0x06000176 RID: 374 RVA: 0x000059E8 File Offset: 0x00003BE8
		public object BasicDeviceInformation { get; set; }

		// Token: 0x06000177 RID: 375 RVA: 0x000059F1 File Offset: 0x00003BF1
		private void StartRemoveTimer()
		{
			this.removeTimer.Interval = new TimeSpan(0, 0, 0, 0, 400);
			this.removeTimer.Start();
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005A1C File Offset: 0x00003C1C
		public override string ToString()
		{
			return this.Title;
		}

		// Token: 0x04000077 RID: 119
		private readonly DispatcherTimer removeTimer = new DispatcherTimer();

		// Token: 0x04000078 RID: 120
		private bool isDeleted;

		// Token: 0x04000079 RID: 121
		private string title;

		// Token: 0x0400007A RID: 122
		private ImageSource image;

		// Token: 0x0400007B RID: 123
		private bool isEnabled;

		// Token: 0x0400007C RID: 124
		private bool isWaiting;
	}
}
