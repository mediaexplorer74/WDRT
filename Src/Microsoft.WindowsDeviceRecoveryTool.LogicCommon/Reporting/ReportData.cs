using System;
using System.Diagnostics;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Helpers;
using Microsoft.WindowsDeviceRecoveryTool.Model;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Reporting
{
	// Token: 0x0200001A RID: 26
	public class ReportData
	{
		// Token: 0x060001BC RID: 444 RVA: 0x00007E62 File Offset: 0x00006062
		public ReportData(string description, string sessionId)
		{
			this.Description = description;
			this.ConnectionName = string.Empty;
			this.SystemInfo = Environment.OSVersion.ToString();
			this.SessionId = sessionId;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00007E99 File Offset: 0x00006099
		// (set) Token: 0x060001BE RID: 446 RVA: 0x00007EA1 File Offset: 0x000060A1
		public Exception Exception { get; private set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00007EAA File Offset: 0x000060AA
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x00007EB2 File Offset: 0x000060B2
		public UriData UriData { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00007EBB File Offset: 0x000060BB
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x00007EC3 File Offset: 0x000060C3
		public Phone PhoneInfo { get; private set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00007ECC File Offset: 0x000060CC
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x00007ED4 File Offset: 0x000060D4
		public long DownloadDuration { get; private set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00007EDD File Offset: 0x000060DD
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x00007EE5 File Offset: 0x000060E5
		public long UpdateDuration { get; private set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00007EEE File Offset: 0x000060EE
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x00007EF6 File Offset: 0x000060F6
		public string ConnectionName { get; private set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00007EFF File Offset: 0x000060FF
		// (set) Token: 0x060001CA RID: 458 RVA: 0x00007F07 File Offset: 0x00006107
		public string Description { get; private set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00007F10 File Offset: 0x00006110
		// (set) Token: 0x060001CC RID: 460 RVA: 0x00007F18 File Offset: 0x00006118
		public string SystemInfo { get; private set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00007F21 File Offset: 0x00006121
		// (set) Token: 0x060001CE RID: 462 RVA: 0x00007F29 File Offset: 0x00006129
		public string LocalPath { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00007F32 File Offset: 0x00006132
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x00007F3A File Offset: 0x0000613A
		public long PackageSize { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00007F43 File Offset: 0x00006143
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x00007F4B File Offset: 0x0000614B
		public int ResumeCounter { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00007F54 File Offset: 0x00006154
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x00007F5C File Offset: 0x0000615C
		public long DownloadedBytes { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00007F65 File Offset: 0x00006165
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x00007F6D File Offset: 0x0000616D
		public string SessionId { get; private set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00007F76 File Offset: 0x00006176
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x00007F7E File Offset: 0x0000617E
		public Exception LastError { get; set; }

		// Token: 0x060001D9 RID: 473 RVA: 0x00007F87 File Offset: 0x00006187
		public void SetPhoneInfo(Phone phone)
		{
			this.PhoneInfo = phone;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00007F94 File Offset: 0x00006194
		public void StartUpdateTimer()
		{
			bool flag = this.updateTimer == null;
			if (flag)
			{
				this.updateTimer = new Stopwatch();
			}
			bool flag2 = this.updateTimer.IsRunning || this.updateTimer.ElapsedMilliseconds != 0L;
			if (flag2)
			{
				this.updateTimer.Restart();
			}
			else
			{
				this.updateTimer.Start();
			}
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00008000 File Offset: 0x00006200
		public void StopUpdateTimer()
		{
			bool flag = this.updateTimer != null;
			if (flag)
			{
				this.updateTimer.Stop();
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000802C File Offset: 0x0000622C
		public void StartDownloadTimer()
		{
			bool flag = this.downloadTimer == null;
			if (flag)
			{
				this.downloadTimer = new Stopwatch();
			}
			bool flag2 = this.downloadTimer.IsRunning || this.downloadTimer.ElapsedMilliseconds != 0L;
			if (flag2)
			{
				this.downloadTimer.Restart();
			}
			else
			{
				this.downloadTimer.Start();
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00008098 File Offset: 0x00006298
		public void StopDownloadTimer()
		{
			bool flag = this.downloadTimer != null;
			if (flag)
			{
				this.downloadTimer.Stop();
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000080C1 File Offset: 0x000062C1
		public void SetResult(UriData uriData, Exception exception)
		{
			this.UriData = uriData;
			this.Exception = exception;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000080D4 File Offset: 0x000062D4
		public void EndDataCollecting()
		{
			this.StopUpdateTimer();
			this.StopDownloadTimer();
			bool flag = this.updateTimer != null;
			if (flag)
			{
				this.UpdateDuration = this.updateTimer.ElapsedMilliseconds;
			}
			bool flag2 = this.downloadTimer != null;
			if (flag2)
			{
				this.DownloadDuration = this.downloadTimer.ElapsedMilliseconds;
			}
		}

		// Token: 0x040000A6 RID: 166
		private Stopwatch updateTimer;

		// Token: 0x040000A7 RID: 167
		private Stopwatch downloadTimer;
	}
}
