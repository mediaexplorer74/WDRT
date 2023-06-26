using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;

namespace Microsoft.WindowsDeviceRecoveryTool.Common.Tracing
{
	// Token: 0x0200000A RID: 10
	public sealed class DiagnosticLogTextWriter : TextWriterTraceListener, ITraceWriter
	{
		// Token: 0x0600003B RID: 59 RVA: 0x000029A4 File Offset: 0x00000BA4
		public DiagnosticLogTextWriter(string traceLogFolder, string filePrefix)
		{
			bool flag = filePrefix == null;
			if (flag)
			{
				throw new ArgumentNullException("filePrefix");
			}
			bool flag2 = string.IsNullOrEmpty(filePrefix);
			if (flag2)
			{
				throw new ArgumentException("File prefix cannot be empty string.", "filePrefix");
			}
			this.logFileName = string.Format(CultureInfo.CurrentCulture, "{0}_{1:yyyy-MM-dd}_{1:HH mm}_{2}.log", new object[]
			{
				filePrefix,
				DateTime.UtcNow,
				Path.GetRandomFileName()
			});
			this.ChangeLogFolder(traceLogFolder);
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002A2F File Offset: 0x00000C2F
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002A37 File Offset: 0x00000C37
		public string LogFilePath { get; private set; }

		// Token: 0x0600003E RID: 62 RVA: 0x00002A40 File Offset: 0x00000C40
		public void ChangeLogFolder(string newPath)
		{
			bool flag = !Directory.Exists(newPath);
			if (flag)
			{
				Directory.CreateDirectory(newPath);
			}
			object obj = this.syncRoot;
			lock (obj)
			{
				try
				{
					bool flag3 = base.Writer != null;
					if (flag3)
					{
						base.Writer.Close();
						base.Writer = null;
					}
				}
				catch (IOException ex)
				{
					Trace.WriteLine("DiagnosticLogTextWriter ChangeLogFolder catches:" + ex.Message);
				}
				this.LogFilePath = Path.Combine(newPath, this.logFileName);
				FileStream fileStream = new FileStream(this.LogFilePath, FileMode.Append, FileAccess.Write);
				base.Writer = new StreamWriter(fileStream)
				{
					AutoFlush = true
				};
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002B20 File Offset: 0x00000D20
		public override void Close()
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				bool flag2 = base.Writer != null;
				if (flag2)
				{
					base.Writer.Close();
					base.Writer = null;
					this.LogFilePath = string.Empty;
				}
				base.Close();
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002B98 File Offset: 0x00000D98
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
		{
			this.TraceData(eventCache, source, eventType, (int)data[0], (string)data[1], (int)data[2], (string)data[3], (string)data[4], (string)data[5], (string)data[6]);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002BF0 File Offset: 0x00000DF0
		private static void AppendField(StringBuilder builder, object fieldValue)
		{
			bool flag = builder.Length != 0;
			if (flag)
			{
				builder.Append(" | ");
			}
			builder.Append(fieldValue ?? string.Empty);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002C2C File Offset: 0x00000E2C
		private static void AppendField(StringBuilder builder, string formatString, params object[] args)
		{
			bool flag = builder.Length != 0;
			if (flag)
			{
				builder.Append(" | ");
			}
			builder.AppendFormat(CultureInfo.CurrentCulture, formatString, args);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002C64 File Offset: 0x00000E64
		private void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int processId, string processName, int threadId, string threadName, string assemblyFileName, string messageText, string errorText)
		{
			string text = eventCache.DateTime.ToLocalTime().ToString("u", CultureInfo.InvariantCulture);
			StringBuilder stringBuilder = new StringBuilder(250);
			DiagnosticLogTextWriter.AppendField(stringBuilder, text, new object[0]);
			DiagnosticLogTextWriter.AppendField(stringBuilder, "{0} ({1})", new object[] { processId, processName });
			bool flag = threadName != null;
			if (flag)
			{
				DiagnosticLogTextWriter.AppendField(stringBuilder, "0x{0:x8} ({1})", new object[] { threadId, threadName });
			}
			else
			{
				DiagnosticLogTextWriter.AppendField(stringBuilder, "0x{0:x8}", new object[] { threadId });
			}
			DiagnosticLogTextWriter.AppendField(stringBuilder, assemblyFileName, new object[0]);
			DiagnosticLogTextWriter.AppendField(stringBuilder, source, new object[0]);
			DiagnosticLogTextWriter.AppendField(stringBuilder, eventType);
			bool flag2 = !string.IsNullOrEmpty(messageText);
			if (flag2)
			{
				messageText = messageText.Replace("{", "{{").Replace("}", "}}");
			}
			DiagnosticLogTextWriter.AppendField(stringBuilder, messageText);
			bool flag3 = !string.IsNullOrEmpty(errorText);
			if (flag3)
			{
				errorText = errorText.Replace("{", "{{").Replace("}", "}}");
			}
			DiagnosticLogTextWriter.AppendField(stringBuilder, (errorText != null) ? ("<!CDATA[[" + errorText + "]]>") : string.Empty, new object[0]);
			object obj = this.syncRoot;
			lock (obj)
			{
				bool flag5 = base.Writer != null;
				if (flag5)
				{
					try
					{
						base.Writer.WriteLine(stringBuilder.ToString());
					}
					catch (IOException)
					{
					}
				}
			}
		}

		// Token: 0x04000009 RID: 9
		private readonly object syncRoot = new object();

		// Token: 0x0400000A RID: 10
		private readonly string logFileName;
	}
}
