using System;
using System.Diagnostics;
using System.IO;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;

namespace Microsoft.WindowsDeviceRecoveryTool.Common
{
	// Token: 0x02000008 RID: 8
	public class ProcessHelper
	{
		// Token: 0x06000019 RID: 25 RVA: 0x000023A8 File Offset: 0x000005A8
		public ProcessHelper()
		{
			this.process = new Process();
			this.process.OutputDataReceived += this.ProcessOnOutputDataReceived;
			this.process.ErrorDataReceived += this.ProcessOnErrorDataReceived;
			this.process.Exited += this.ProcessOnExited;
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600001A RID: 26 RVA: 0x00002410 File Offset: 0x00000610
		// (remove) Token: 0x0600001B RID: 27 RVA: 0x00002448 File Offset: 0x00000648
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event DataReceivedEventHandler OutputDataReceived;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600001C RID: 28 RVA: 0x00002480 File Offset: 0x00000680
		// (remove) Token: 0x0600001D RID: 29 RVA: 0x000024B8 File Offset: 0x000006B8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event DataReceivedEventHandler ErrorDataReceived;

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000024F0 File Offset: 0x000006F0
		// (set) Token: 0x0600001F RID: 31 RVA: 0x0000250D File Offset: 0x0000070D
		public bool EnableRaisingEvents
		{
			get
			{
				return this.process.EnableRaisingEvents;
			}
			set
			{
				this.process.EnableRaisingEvents = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002520 File Offset: 0x00000720
		// (set) Token: 0x06000021 RID: 33 RVA: 0x0000253D File Offset: 0x0000073D
		public ProcessStartInfo StartInfo
		{
			get
			{
				return this.process.StartInfo;
			}
			set
			{
				this.process.StartInfo = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002550 File Offset: 0x00000750
		public int Id
		{
			get
			{
				return this.process.Id;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002570 File Offset: 0x00000770
		public bool HasExited
		{
			get
			{
				return this.process.HasExited;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002590 File Offset: 0x00000790
		public int ExitCode
		{
			get
			{
				return this.process.ExitCode;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000025B0 File Offset: 0x000007B0
		public StreamWriter StandardInput
		{
			get
			{
				return this.process.StandardInput;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000025D0 File Offset: 0x000007D0
		public static Process GetProcessById(int processId)
		{
			return Process.GetProcessById(processId);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000025E8 File Offset: 0x000007E8
		public static Process[] GetProcessesByName(string processName)
		{
			return Process.GetProcessesByName(processName);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002600 File Offset: 0x00000800
		public bool Start()
		{
			Tracer<ProcessHelper>.WriteInformation("ProcessHelper start called {0}", new object[] { this.process.StartInfo.FileName });
			return this.process.Start();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002641 File Offset: 0x00000841
		public void Dispose()
		{
			this.process.Dispose();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002650 File Offset: 0x00000850
		public void WaitForExit()
		{
			this.process.WaitForExit();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000265F File Offset: 0x0000085F
		public void WaitForExit(int milliseconds)
		{
			this.process.WaitForExit(milliseconds);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000266F File Offset: 0x0000086F
		public void BeginOutputReadLine()
		{
			this.process.BeginOutputReadLine();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000267E File Offset: 0x0000087E
		public void Kill()
		{
			this.process.Kill();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002690 File Offset: 0x00000890
		private void ProcessOnOutputDataReceived(object sender, DataReceivedEventArgs dataReceivedEventArgs)
		{
			bool flag = this.OutputDataReceived != null;
			if (flag)
			{
				this.OutputDataReceived(sender, dataReceivedEventArgs);
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000026BC File Offset: 0x000008BC
		private void ProcessOnErrorDataReceived(object sender, DataReceivedEventArgs dataReceivedEventArgs)
		{
			bool flag = this.ErrorDataReceived != null;
			if (flag)
			{
				this.ErrorDataReceived(sender, dataReceivedEventArgs);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000026E7 File Offset: 0x000008E7
		private void ProcessOnExited(object sender, EventArgs eventArgs)
		{
			Tracer<ProcessHelper>.WriteInformation("Process exited");
		}

		// Token: 0x04000006 RID: 6
		private readonly Process process;
	}
}
