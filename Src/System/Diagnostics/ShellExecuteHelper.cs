using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Win32;

namespace System.Diagnostics
{
	// Token: 0x020004F5 RID: 1269
	internal class ShellExecuteHelper
	{
		// Token: 0x0600301A RID: 12314 RVA: 0x000D9462 File Offset: 0x000D7662
		public ShellExecuteHelper(Microsoft.Win32.NativeMethods.ShellExecuteInfo executeInfo)
		{
			this._executeInfo = executeInfo;
		}

		// Token: 0x0600301B RID: 12315 RVA: 0x000D9474 File Offset: 0x000D7674
		public void ShellExecuteFunction()
		{
			if (!(this._succeeded = Microsoft.Win32.NativeMethods.ShellExecuteEx(this._executeInfo)))
			{
				this._errorCode = Marshal.GetLastWin32Error();
			}
		}

		// Token: 0x0600301C RID: 12316 RVA: 0x000D94A4 File Offset: 0x000D76A4
		public bool ShellExecuteOnSTAThread()
		{
			if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
			{
				ThreadStart threadStart = new ThreadStart(this.ShellExecuteFunction);
				Thread thread = new Thread(threadStart);
				thread.SetApartmentState(ApartmentState.STA);
				thread.Start();
				thread.Join();
			}
			else
			{
				this.ShellExecuteFunction();
			}
			return this._succeeded;
		}

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x0600301D RID: 12317 RVA: 0x000D94F2 File Offset: 0x000D76F2
		public int ErrorCode
		{
			get
			{
				return this._errorCode;
			}
		}

		// Token: 0x0400286F RID: 10351
		private Microsoft.Win32.NativeMethods.ShellExecuteInfo _executeInfo;

		// Token: 0x04002870 RID: 10352
		private int _errorCode;

		// Token: 0x04002871 RID: 10353
		private bool _succeeded;
	}
}
