using System;

namespace System.Diagnostics
{
	// Token: 0x020004F4 RID: 1268
	internal class ProcessThreadTimes
	{
		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x06003014 RID: 12308 RVA: 0x000D9412 File Offset: 0x000D7612
		public DateTime StartTime
		{
			get
			{
				return DateTime.FromFileTime(this.create);
			}
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x06003015 RID: 12309 RVA: 0x000D941F File Offset: 0x000D761F
		public DateTime ExitTime
		{
			get
			{
				return DateTime.FromFileTime(this.exit);
			}
		}

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x06003016 RID: 12310 RVA: 0x000D942C File Offset: 0x000D762C
		public TimeSpan PrivilegedProcessorTime
		{
			get
			{
				return new TimeSpan(this.kernel);
			}
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x06003017 RID: 12311 RVA: 0x000D9439 File Offset: 0x000D7639
		public TimeSpan UserProcessorTime
		{
			get
			{
				return new TimeSpan(this.user);
			}
		}

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x06003018 RID: 12312 RVA: 0x000D9446 File Offset: 0x000D7646
		public TimeSpan TotalProcessorTime
		{
			get
			{
				return new TimeSpan(this.user + this.kernel);
			}
		}

		// Token: 0x0400286B RID: 10347
		internal long create;

		// Token: 0x0400286C RID: 10348
		internal long exit;

		// Token: 0x0400286D RID: 10349
		internal long kernel;

		// Token: 0x0400286E RID: 10350
		internal long user;
	}
}
