using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.WindowsDeviceRecoveryTool.Common;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;

namespace Microsoft.WindowsDeviceRecoveryTool.LumiaAdaptation.Services
{
	// Token: 0x02000006 RID: 6
	[Export]
	public class ProcessManager : IDisposable
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00004444 File Offset: 0x00002644
		[ImportingConstructor]
		public ProcessManager()
		{
			this.timeoutTimer = new System.Timers.Timer(90000.0);
			this.timeoutTimer.Elapsed += this.Thor2TimeoutOccured;
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000054 RID: 84 RVA: 0x00004490 File Offset: 0x00002690
		// (remove) Token: 0x06000055 RID: 85 RVA: 0x000044C8 File Offset: 0x000026C8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<DataReceivedEventArgs> OnOutputDataReceived;

		// Token: 0x06000056 RID: 86 RVA: 0x000044FD File Offset: 0x000026FD
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00004510 File Offset: 0x00002710
		protected virtual void Dispose(bool disposing)
		{
			bool flag = this.disposed;
			if (!flag)
			{
				this.disposed = true;
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00004534 File Offset: 0x00002734
		private void Thor2TimeoutOccured(object sender, ElapsedEventArgs e)
		{
			Tracer<ProcessManager>.WriteInformation("Thor2.exe timeout occured");
			System.Timers.Timer timer = this.timeoutTimer;
			lock (timer)
			{
				this.ReleaseManagedObjects();
				this.timeoutOccured = true;
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000458C File Offset: 0x0000278C
		private void RestartTimeoutTimer()
		{
			System.Timers.Timer timer = this.timeoutTimer;
			lock (timer)
			{
				bool flag2 = this.timeoutOccured;
				if (!flag2)
				{
					this.timeoutTimer.Stop();
					this.timeoutTimer.Start();
				}
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000045F0 File Offset: 0x000027F0
		public Thor2ExitCode RunThor2ProcessWithArguments(string processArguments, CancellationToken cancellationToken)
		{
			ProcessHelper thor2ProcessProcess = this.PrepareThorProcess(processArguments);
			this.timeoutOccured = false;
			this.RestartTimeoutTimer();
			thor2ProcessProcess.OutputDataReceived += this.OutputDataReceived;
			thor2ProcessProcess.Start();
			int id = thor2ProcessProcess.Id;
			this.actualProcessIds.Add(id);
			thor2ProcessProcess.BeginOutputReadLine();
			Task task = new Task(delegate
			{
				this.CancellationMonitor(cancellationToken, thor2ProcessProcess);
			});
			task.Start();
			thor2ProcessProcess.WaitForExit();
			thor2ProcessProcess.OutputDataReceived -= this.OutputDataReceived;
			this.timeoutTimer.Stop();
			List<int> list = this.actualProcessIds;
			lock (list)
			{
				bool flag2 = this.actualProcessIds.Contains(id);
				if (flag2)
				{
					this.actualProcessIds.Remove(id);
				}
			}
			bool flag3 = this.timeoutOccured;
			Thor2ExitCode thor2ExitCode;
			if (flag3)
			{
				thor2ExitCode = Thor2ExitCode.Thor2NotResponding;
			}
			else
			{
				thor2ExitCode = (Thor2ExitCode)thor2ProcessProcess.ExitCode;
			}
			return thor2ExitCode;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004738 File Offset: 0x00002938
		private void OutputDataReceived(object sender, DataReceivedEventArgs dataReceivedEventArgs)
		{
			this.RestartTimeoutTimer();
			Action<DataReceivedEventArgs> onOutputDataReceived = this.OnOutputDataReceived;
			bool flag = onOutputDataReceived != null;
			if (flag)
			{
				onOutputDataReceived(dataReceivedEventArgs);
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004768 File Offset: 0x00002968
		private void CancellationMonitor(CancellationToken token, ProcessHelper helper)
		{
			while (!helper.HasExited)
			{
				Thread.Sleep(500);
				bool isCancellationRequested = token.IsCancellationRequested;
				if (isCancellationRequested)
				{
					bool flag = !helper.HasExited;
					if (flag)
					{
						Tracer<ProcessManager>.WriteInformation("Cancellation requested. Process still running. Need to manually kill process.");
						helper.Kill();
					}
				}
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000047C0 File Offset: 0x000029C0
		private ProcessHelper PrepareThorProcess(string processArguments)
		{
			string workingDirectoryPath = this.GetWorkingDirectoryPath();
			Tracer<ProcessManager>.WriteInformation("Creating process start information.");
			ProcessStartInfo processStartInfo = new ProcessStartInfo
			{
				FileName = string.Format("\"{0}\"", Path.Combine(workingDirectoryPath, "thor2.exe")),
				Arguments = processArguments,
				UseShellExecute = false,
				RedirectStandardError = true,
				RedirectStandardOutput = true,
				CreateNoWindow = true,
				WorkingDirectory = workingDirectoryPath
			};
			Tracer<ProcessManager>.WriteInformation("Creating process helper.");
			return new ProcessHelper
			{
				EnableRaisingEvents = true,
				StartInfo = processStartInfo
			};
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00004858 File Offset: 0x00002A58
		private string GetWorkingDirectoryPath()
		{
			string directoryName = Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path));
			bool flag = string.IsNullOrWhiteSpace(directoryName);
			if (flag)
			{
				Tracer<ProcessManager>.WriteError("Could not find working directory path", new object[0]);
				throw new Exception("Could not find working directory path");
			}
			return directoryName;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000048B4 File Offset: 0x00002AB4
		internal void ReleaseManagedObjects()
		{
			List<int> list = this.actualProcessIds;
			lock (list)
			{
				for (int i = this.actualProcessIds.Count - 1; i >= 0; i--)
				{
					this.AbortProcess(this.actualProcessIds[i]);
					this.actualProcessIds.RemoveAt(i);
				}
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004934 File Offset: 0x00002B34
		private void AbortProcess(int processId)
		{
			try
			{
				Process processById = ProcessHelper.GetProcessById(processId);
				bool flag = processById.ProcessName.Equals("thor2", StringComparison.OrdinalIgnoreCase);
				if (flag)
				{
					Tracer<ProcessManager>.WriteInformation("Killing process {0}", new object[] { processId });
					processById.Kill();
					Tracer<ProcessManager>.WriteInformation("Process {0} killed", new object[] { processId });
				}
			}
			catch (Exception ex)
			{
				Tracer<ProcessManager>.WriteError(ex, "Aborting device update process {0} failed", new object[] { processId });
				throw;
			}
		}

		// Token: 0x04000022 RID: 34
		private readonly List<int> actualProcessIds = new List<int>();

		// Token: 0x04000023 RID: 35
		private readonly System.Timers.Timer timeoutTimer;

		// Token: 0x04000024 RID: 36
		private bool disposed;

		// Token: 0x04000025 RID: 37
		private bool timeoutOccured;
	}
}
