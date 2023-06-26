using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.WindowsDeviceRecoveryTool.Common.Tracing
{
	// Token: 0x02000010 RID: 16
	[CompilerGenerated]
	public static class Tracer<T>
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00003932 File Offset: 0x00001B32
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00003939 File Offset: 0x00001B39
		internal static IThreadSafeTracer InternalTracer { get; set; } = TraceManager.Instance.CreateTraceSource(typeof(T).FullName);

		// Token: 0x0600006A RID: 106 RVA: 0x00003941 File Offset: 0x00001B41
		public static void LogEntry([CallerMemberName] string callerMemberName = "")
		{
			Tracer<T>.WriteEvent(TraceEventType.Information, null, string.Format("{0}()\tBEGIN", callerMemberName));
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003957 File Offset: 0x00001B57
		public static void LogExit([CallerMemberName] string callerMemberName = "")
		{
			Tracer<T>.WriteEvent(TraceEventType.Information, null, string.Format("{0}()\tEND", callerMemberName));
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000396D File Offset: 0x00001B6D
		public static void WriteError(string format, params object[] args)
		{
			Tracer<T>.WriteEvent(TraceEventType.Error, null, string.Format(CultureInfo.CurrentCulture, format, args));
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003984 File Offset: 0x00001B84
		public static void WriteError(IFormatProvider formatProvider, string format, params object[] args)
		{
			Tracer<T>.WriteEvent(TraceEventType.Error, null, string.Format(formatProvider, format, args));
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003997 File Offset: 0x00001B97
		public static void WriteError(Exception error)
		{
			Tracer<T>.WriteEvent(TraceEventType.Error, error.ToString(), null);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000039A8 File Offset: 0x00001BA8
		public static void WriteError(Exception error, string format, params object[] args)
		{
			Tracer<T>.WriteEvent(TraceEventType.Error, error.ToString(), string.Format(CultureInfo.CurrentCulture, format, args));
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000039C4 File Offset: 0x00001BC4
		public static void WriteError(Exception error, IFormatProvider formatProvider, string format, params object[] args)
		{
			Tracer<T>.WriteEvent(TraceEventType.Error, error.ToString(), string.Format(formatProvider, format, args));
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000039DC File Offset: 0x00001BDC
		public static void WriteInformation(string text)
		{
			Tracer<T>.WriteEvent(TraceEventType.Information, null, text);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000039E8 File Offset: 0x00001BE8
		public static void WriteInformation(string format, params object[] args)
		{
			Tracer<T>.WriteEvent(TraceEventType.Information, null, string.Format(CultureInfo.CurrentCulture, format, args));
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000039FF File Offset: 0x00001BFF
		public static void WriteInformation(IFormatProvider formatProvider, string format, params object[] args)
		{
			Tracer<T>.WriteEvent(TraceEventType.Information, null, string.Format(formatProvider, format, args));
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003A12 File Offset: 0x00001C12
		public static void WriteInformation(Exception error, string format, params object[] args)
		{
			Tracer<T>.WriteEvent(TraceEventType.Information, error.ToString(), string.Format(CultureInfo.CurrentCulture, format, args));
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003A2E File Offset: 0x00001C2E
		public static void WriteInformation(Exception error, IFormatProvider formatProvider, string format, params object[] args)
		{
			Tracer<T>.WriteEvent(TraceEventType.Information, error.ToString(), string.Format(formatProvider, format, args));
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003A46 File Offset: 0x00001C46
		public static void WriteVerbose(string format, params object[] args)
		{
			Tracer<T>.WriteEvent(TraceEventType.Verbose, null, string.Format(CultureInfo.CurrentCulture, format, args));
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003A5E File Offset: 0x00001C5E
		public static void WriteVerbose(IFormatProvider formatProvider, string format, params object[] args)
		{
			Tracer<T>.WriteEvent(TraceEventType.Verbose, null, string.Format(formatProvider, format, args));
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003A72 File Offset: 0x00001C72
		public static void WriteVerbose(Exception error)
		{
			Tracer<T>.WriteEvent(TraceEventType.Verbose, error.ToString(), null);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003A84 File Offset: 0x00001C84
		public static void WriteVerbose(Exception error, string format, params object[] args)
		{
			Tracer<T>.WriteEvent(TraceEventType.Verbose, error.ToString(), string.Format(CultureInfo.CurrentCulture, format, args));
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003AA1 File Offset: 0x00001CA1
		public static void WriteVerbose(Exception error, IFormatProvider formatProvider, string format, params object[] args)
		{
			Tracer<T>.WriteEvent(TraceEventType.Verbose, error.ToString(), string.Format(formatProvider, format, args));
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003ABA File Offset: 0x00001CBA
		public static void WriteWarning(string format, params object[] args)
		{
			Tracer<T>.WriteEvent(TraceEventType.Warning, null, string.Format(CultureInfo.CurrentCulture, format, args));
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003AD1 File Offset: 0x00001CD1
		public static void WriteWarning(IFormatProvider formatProvider, string format, params object[] args)
		{
			Tracer<T>.WriteEvent(TraceEventType.Warning, null, string.Format(formatProvider, format, args));
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003AE4 File Offset: 0x00001CE4
		public static void WriteWarning(Exception error)
		{
			Tracer<T>.WriteEvent(TraceEventType.Warning, error.ToString(), null);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003AF5 File Offset: 0x00001CF5
		public static void WriteWarning(Exception error, string format, params object[] args)
		{
			Tracer<T>.WriteEvent(TraceEventType.Warning, error.ToString(), string.Format(CultureInfo.CurrentCulture, format, args));
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003B11 File Offset: 0x00001D11
		public static void WriteWarning(Exception error, IFormatProvider formatProvider, string format, params object[] args)
		{
			Tracer<T>.WriteEvent(TraceEventType.Warning, error.ToString(), string.Format(formatProvider, format, args));
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003B2C File Offset: 0x00001D2C
		private static void WriteEvent(TraceEventType eventType, string errorText, string messageText)
		{
			string fileName = Path.GetFileName(typeof(T).Assembly.Location);
			Thread currentThread = Thread.CurrentThread;
			Tracer<T>.CurrentProcessInfo instance = Tracer<T>.CurrentProcessInfo.Instance;
			Tracer<T>.InternalTracer.TraceData(eventType, new object[] { instance.Id, instance.Name, currentThread.ManagedThreadId, currentThread.Name, fileName, messageText, errorText });
		}

		// Token: 0x02000015 RID: 21
		private sealed class CurrentProcessInfo
		{
			// Token: 0x1700000F RID: 15
			// (get) Token: 0x06000090 RID: 144 RVA: 0x00003E48 File Offset: 0x00002048
			public static Tracer<T>.CurrentProcessInfo Instance
			{
				get
				{
					bool flag = Tracer<T>.CurrentProcessInfo.instance == null;
					if (flag)
					{
						Process currentProcess = Process.GetCurrentProcess();
						Tracer<T>.CurrentProcessInfo currentProcessInfo = new Tracer<T>.CurrentProcessInfo();
						currentProcessInfo.Id = currentProcess.Id;
						currentProcessInfo.Name = currentProcess.ProcessName;
						Tracer<T>.CurrentProcessInfo currentProcessInfo2 = currentProcessInfo;
						Interlocked.CompareExchange<Tracer<T>.CurrentProcessInfo>(ref Tracer<T>.CurrentProcessInfo.instance, currentProcessInfo2, null);
					}
					return Tracer<T>.CurrentProcessInfo.instance;
				}
			}

			// Token: 0x04000020 RID: 32
			public int Id;

			// Token: 0x04000021 RID: 33
			public string Name;

			// Token: 0x04000022 RID: 34
			private static Tracer<T>.CurrentProcessInfo instance;
		}
	}
}
