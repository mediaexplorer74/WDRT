using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Microsoft.WindowsDeviceRecoveryTool.Common.Tracing
{
	// Token: 0x0200000F RID: 15
	public class TraceManager
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00003080 File Offset: 0x00001280
		internal TraceManager()
		{
			this.defaultLogFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Microsoft\\Care Suite\\Windows Device Recovery Tool\\Traces\\";
			this.currentTracingLevel = SourceLevels.All;
			this.Tracers = new List<IThreadSafeTracer>();
			AppDomain.CurrentDomain.ProcessExit += this.OnCurrentDomainProcessExit;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000030E4 File Offset: 0x000012E4
		public static TraceManager Instance
		{
			get
			{
				bool flag = TraceManager.instance == null;
				if (flag)
				{
					object staticSyncRoot = TraceManager.StaticSyncRoot;
					lock (staticSyncRoot)
					{
						bool flag3 = TraceManager.instance == null;
						if (flag3)
						{
							TraceManager.instance = new TraceManager();
						}
					}
				}
				return TraceManager.instance;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003154 File Offset: 0x00001354
		// (set) Token: 0x0600005B RID: 91 RVA: 0x0000315C File Offset: 0x0000135C
		internal ITraceWriter MainTraceWriter { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003165 File Offset: 0x00001365
		// (set) Token: 0x0600005D RID: 93 RVA: 0x0000316D File Offset: 0x0000136D
		internal List<IThreadSafeTracer> Tracers { get; private set; }

		// Token: 0x0600005E RID: 94 RVA: 0x00003178 File Offset: 0x00001378
		private void RegisterDiagnosticTraceWriter(string logPath, string logNamePrefix)
		{
			ITraceWriter traceWriter = new DiagnosticLogTextWriter(logPath, logNamePrefix);
			object obj = this.syncRoot;
			lock (obj)
			{
				try
				{
					bool flag2 = this.MainTraceWriter != null;
					if (flag2)
					{
						this.MainTraceWriter.Close();
					}
					TraceListener traceListener = traceWriter as TraceListener;
					foreach (IThreadSafeTracer threadSafeTracer in this.Tracers)
					{
						threadSafeTracer.AddTraceListener(traceListener);
					}
					this.MainTraceWriter = traceWriter;
					Tracer<TraceManager>.WriteInformation("New diagnostic trace writer registered.");
				}
				catch (Exception ex)
				{
					Tracer<TraceManager>.WriteError(ex, "Could not register diagnostic trace writer.", new object[0]);
					throw new InvalidOperationException("Could not register diagnostic trace writer.", ex);
				}
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003274 File Offset: 0x00001474
		public void EnableDiagnosticLogs(string logPath, string logNamePrefix)
		{
			bool flag = this.MainTraceWriter == null;
			if (flag)
			{
				this.RegisterDiagnosticTraceWriter(logPath, logNamePrefix);
			}
			object obj = this.syncRoot;
			lock (obj)
			{
				bool flag3 = this.MainTraceWriter == null;
				if (flag3)
				{
					throw new InvalidOperationException("RegisterDiagnosticTraceWriter must be called before using this method.");
				}
				try
				{
					bool flag4 = string.IsNullOrEmpty(this.MainTraceWriter.LogFilePath);
					if (flag4)
					{
						this.MainTraceWriter.ChangeLogFolder(this.defaultLogFolder);
					}
					foreach (IThreadSafeTracer threadSafeTracer in this.Tracers)
					{
						threadSafeTracer.EnableTracing();
					}
					this.currentTracingLevel = SourceLevels.All;
					Tracer<TraceManager>.WriteInformation("Diagnostic logs enabled.");
				}
				catch (Exception ex)
				{
					Tracer<TraceManager>.WriteError(ex, "Could not enable diagnostic logs.", new object[0]);
					throw new InvalidOperationException("Could not enable diagnostic logs.", ex);
				}
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000033A0 File Offset: 0x000015A0
		public void DisableDiagnosticLogs(bool removeCurrentLogFile)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				bool flag2 = this.MainTraceWriter == null;
				if (flag2)
				{
					throw new InvalidOperationException("RegisterDiagnosticTraceWriter must be called before using this method.");
				}
				try
				{
					bool flag3 = removeCurrentLogFile && !string.IsNullOrEmpty(this.MainTraceWriter.LogFilePath);
					if (flag3)
					{
						string logFilePath = this.MainTraceWriter.LogFilePath;
						this.MainTraceWriter.Close();
						File.Delete(logFilePath);
						this.MainTraceWriter = null;
					}
					foreach (IThreadSafeTracer threadSafeTracer in this.Tracers)
					{
						threadSafeTracer.DisableTracing();
					}
					this.currentTracingLevel = SourceLevels.Off;
					Tracer<TraceManager>.WriteInformation("Diagnostic logs disabled.");
				}
				catch (Exception ex)
				{
					Tracer<TraceManager>.WriteError(ex, "Could not disable diagnostic logs.", new object[0]);
					throw new InvalidOperationException("Could not disable diagnostic logs.", ex);
				}
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000034D0 File Offset: 0x000016D0
		public void ChangeDiagnosticLogFolder(string newPath)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				bool flag2 = this.MainTraceWriter == null;
				if (flag2)
				{
					throw new InvalidOperationException("RegisterDiagnosticTraceWriter must be called before using this method.");
				}
				try
				{
					this.MainTraceWriter.ChangeLogFolder(newPath);
					Tracer<TraceManager>.WriteInformation("Diagnostic logs folder changed.");
				}
				catch (Exception ex)
				{
					Tracer<TraceManager>.WriteError(ex, "Could not change diagnostic logs folder.", new object[0]);
					throw new InvalidOperationException("Could not change diagnostic logs folder.", ex);
				}
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003574 File Offset: 0x00001774
		public void RemoveDiagnosticLogs(string directoryPath, string appNamePrefix, bool traceEnabled)
		{
			Tracer<TraceManager>.WriteInformation("Remove diagnostic logs.");
			object obj = this.syncRoot;
			lock (obj)
			{
				string[] files = Directory.GetFiles(directoryPath);
				foreach (string text in files)
				{
					try
					{
						File.Delete(text);
						Tracer<TraceManager>.WriteInformation("Succesfully removed file: {0}.", new object[] { text });
					}
					catch (Exception ex)
					{
						Tracer<TraceManager>.WriteError(ex, "Following file could not be deleted: {0}.", new object[] { text });
					}
				}
			}
			bool flag2 = !traceEnabled && this.MainTraceWriter != null;
			if (flag2)
			{
				this.DisableDiagnosticLogs(true);
			}
			Tracer<TraceManager>.WriteInformation("Finished removing diagnostic logs.");
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003658 File Offset: 0x00001858
		public void MoveDiagnosticLogFile(string newPath)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				bool flag2 = this.MainTraceWriter == null;
				if (flag2)
				{
					throw new InvalidOperationException("RegisterDiagnosticTraceWriter must be called before using this method.");
				}
				bool flag3 = string.IsNullOrEmpty(this.MainTraceWriter.LogFilePath);
				if (flag3)
				{
					throw new InvalidOperationException("Current diagnostic log file does not exist. There is nothing to be moved.");
				}
				try
				{
					string logFilePath = this.MainTraceWriter.LogFilePath;
					this.MainTraceWriter.Close();
					Directory.CreateDirectory(newPath);
					File.Move(logFilePath, Path.Combine(newPath, Path.GetFileName(logFilePath)));
					this.MainTraceWriter.ChangeLogFolder(newPath);
					Tracer<TraceManager>.WriteInformation("Diagnostic logs folder changed.");
				}
				catch (Exception ex)
				{
					Tracer<TraceManager>.WriteError(ex, "Could not move diagnostic logs file", new object[0]);
					throw new InvalidOperationException("Could not move diagnostic logs file.", ex);
				}
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003754 File Offset: 0x00001954
		internal IThreadSafeTracer CreateTraceSource(string sourceName)
		{
			IThreadSafeTracer threadSafeTracer2;
			try
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					ThreadSafeTracer threadSafeTracer = new ThreadSafeTracer(sourceName, this.currentTracingLevel);
					bool flag2 = this.MainTraceWriter != null;
					if (flag2)
					{
						TraceListener traceListener = this.MainTraceWriter as TraceListener;
						bool flag3 = traceListener == null;
						if (flag3)
						{
							traceListener = new TraceListenerAdapter(this.MainTraceWriter);
						}
						threadSafeTracer.AddTraceListener(traceListener);
					}
					this.Tracers.Add(threadSafeTracer);
					threadSafeTracer2 = threadSafeTracer;
				}
			}
			catch (Exception ex)
			{
				Trace.WriteLine("Could not create new tracer. Error: " + ex.Message);
				throw new InvalidOperationException("Could not create new tracer.", ex);
			}
			return threadSafeTracer2;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003824 File Offset: 0x00001A24
		private void OnCurrentDomainProcessExit(object sender, EventArgs e)
		{
			try
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					bool flag2 = this.MainTraceWriter != null;
					if (flag2)
					{
						this.MainTraceWriter.Close();
					}
					foreach (IThreadSafeTracer threadSafeTracer in this.Tracers)
					{
						ThreadSafeTracer threadSafeTracer2 = (ThreadSafeTracer)threadSafeTracer;
						threadSafeTracer2.Close();
					}
					this.Tracers.Clear();
				}
			}
			catch (Exception ex)
			{
				Trace.WriteLine("TraceManager OnCurrentDomainProcessExit catches:" + ex.Message);
				throw;
			}
		}

		// Token: 0x0400000F RID: 15
		private static readonly object StaticSyncRoot = new object();

		// Token: 0x04000010 RID: 16
		private static TraceManager instance;

		// Token: 0x04000011 RID: 17
		private readonly object syncRoot = new object();

		// Token: 0x04000012 RID: 18
		private readonly string defaultLogFolder;

		// Token: 0x04000013 RID: 19
		private SourceLevels currentTracingLevel;
	}
}
