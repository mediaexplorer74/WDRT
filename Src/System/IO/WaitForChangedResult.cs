using System;

namespace System.IO
{
	/// <summary>Contains information on the change that occurred.</summary>
	// Token: 0x02000406 RID: 1030
	public struct WaitForChangedResult
	{
		// Token: 0x06002699 RID: 9881 RVA: 0x000B1C47 File Offset: 0x000AFE47
		internal WaitForChangedResult(WatcherChangeTypes changeType, string name, bool timedOut)
		{
			this = new WaitForChangedResult(changeType, name, null, timedOut);
		}

		// Token: 0x0600269A RID: 9882 RVA: 0x000B1C53 File Offset: 0x000AFE53
		internal WaitForChangedResult(WatcherChangeTypes changeType, string name, string oldName, bool timedOut)
		{
			this.changeType = changeType;
			this.name = name;
			this.oldName = oldName;
			this.timedOut = timedOut;
		}

		/// <summary>Gets or sets the type of change that occurred.</summary>
		/// <returns>One of the <see cref="T:System.IO.WatcherChangeTypes" /> values.</returns>
		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x0600269B RID: 9883 RVA: 0x000B1C72 File Offset: 0x000AFE72
		// (set) Token: 0x0600269C RID: 9884 RVA: 0x000B1C7A File Offset: 0x000AFE7A
		public WatcherChangeTypes ChangeType
		{
			get
			{
				return this.changeType;
			}
			set
			{
				this.changeType = value;
			}
		}

		/// <summary>Gets or sets the name of the file or directory that changed.</summary>
		/// <returns>The name of the file or directory that changed.</returns>
		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x0600269D RID: 9885 RVA: 0x000B1C83 File Offset: 0x000AFE83
		// (set) Token: 0x0600269E RID: 9886 RVA: 0x000B1C8B File Offset: 0x000AFE8B
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets the original name of the file or directory that was renamed.</summary>
		/// <returns>The original name of the file or directory that was renamed.</returns>
		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x0600269F RID: 9887 RVA: 0x000B1C94 File Offset: 0x000AFE94
		// (set) Token: 0x060026A0 RID: 9888 RVA: 0x000B1C9C File Offset: 0x000AFE9C
		public string OldName
		{
			get
			{
				return this.oldName;
			}
			set
			{
				this.oldName = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the wait operation timed out.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="M:System.IO.FileSystemWatcher.WaitForChanged(System.IO.WatcherChangeTypes)" /> method timed out; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x060026A1 RID: 9889 RVA: 0x000B1CA5 File Offset: 0x000AFEA5
		// (set) Token: 0x060026A2 RID: 9890 RVA: 0x000B1CAD File Offset: 0x000AFEAD
		public bool TimedOut
		{
			get
			{
				return this.timedOut;
			}
			set
			{
				this.timedOut = value;
			}
		}

		// Token: 0x040020D1 RID: 8401
		private WatcherChangeTypes changeType;

		// Token: 0x040020D2 RID: 8402
		private string name;

		// Token: 0x040020D3 RID: 8403
		private string oldName;

		// Token: 0x040020D4 RID: 8404
		private bool timedOut;

		// Token: 0x040020D5 RID: 8405
		internal static readonly WaitForChangedResult TimedOutResult = new WaitForChangedResult((WatcherChangeTypes)0, null, true);
	}
}
