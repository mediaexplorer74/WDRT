using System;
using System.Security;
using System.Threading;
using Microsoft.Win32;

namespace System.Diagnostics
{
	// Token: 0x02000491 RID: 1169
	internal class MessageBoxPopup
	{
		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x06002B47 RID: 11079 RVA: 0x000C4BA9 File Offset: 0x000C2DA9
		// (set) Token: 0x06002B48 RID: 11080 RVA: 0x000C4BB1 File Offset: 0x000C2DB1
		public int ReturnValue { get; set; }

		// Token: 0x06002B49 RID: 11081 RVA: 0x000C4BBA File Offset: 0x000C2DBA
		[SecurityCritical]
		public MessageBoxPopup(string body, string title, int flags)
		{
			this.m_Event = new AutoResetEvent(false);
			this.m_Body = body;
			this.m_Title = title;
			this.m_Flags = flags;
		}

		// Token: 0x06002B4A RID: 11082 RVA: 0x000C4BE4 File Offset: 0x000C2DE4
		public int ShowMessageBox()
		{
			Thread thread = new Thread(new ThreadStart(this.DoPopup));
			thread.Start();
			this.m_Event.WaitOne();
			return this.ReturnValue;
		}

		// Token: 0x06002B4B RID: 11083 RVA: 0x000C4C1B File Offset: 0x000C2E1B
		[SecuritySafeCritical]
		public void DoPopup()
		{
			this.ReturnValue = SafeNativeMethods.MessageBox(IntPtr.Zero, this.m_Body, this.m_Title, this.m_Flags);
			this.m_Event.Set();
		}

		// Token: 0x0400266B RID: 9835
		private AutoResetEvent m_Event;

		// Token: 0x0400266C RID: 9836
		private string m_Body;

		// Token: 0x0400266D RID: 9837
		private string m_Title;

		// Token: 0x0400266E RID: 9838
		private int m_Flags;
	}
}
