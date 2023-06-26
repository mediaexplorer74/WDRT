using System;
using System.Collections;
using System.Globalization;
using System.Security.Permissions;
using System.Threading;

namespace System.Diagnostics
{
	/// <summary>Provides trace event data specific to a thread and a process.</summary>
	// Token: 0x020004AD RID: 1197
	public class TraceEventCache
	{
		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x06002C71 RID: 11377 RVA: 0x000C7A67 File Offset: 0x000C5C67
		internal Guid ActivityId
		{
			get
			{
				return Trace.CorrelationManager.ActivityId;
			}
		}

		/// <summary>Gets the call stack for the current thread.</summary>
		/// <returns>A string containing stack trace information. This value can be an empty string ("").</returns>
		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x06002C72 RID: 11378 RVA: 0x000C7A73 File Offset: 0x000C5C73
		public string Callstack
		{
			get
			{
				if (this.stackTrace == null)
				{
					this.stackTrace = Environment.StackTrace;
				}
				else
				{
					new EnvironmentPermission(PermissionState.Unrestricted).Demand();
				}
				return this.stackTrace;
			}
		}

		/// <summary>Gets the correlation data, contained in a stack.</summary>
		/// <returns>A <see cref="T:System.Collections.Stack" /> containing correlation data.</returns>
		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x06002C73 RID: 11379 RVA: 0x000C7A9B File Offset: 0x000C5C9B
		public Stack LogicalOperationStack
		{
			get
			{
				return Trace.CorrelationManager.LogicalOperationStack;
			}
		}

		/// <summary>Gets the date and time at which the event trace occurred.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> structure whose value is a date and time expressed in Coordinated Universal Time (UTC).</returns>
		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x06002C74 RID: 11380 RVA: 0x000C7AA7 File Offset: 0x000C5CA7
		public DateTime DateTime
		{
			get
			{
				if (this.dateTime == DateTime.MinValue)
				{
					this.dateTime = DateTime.UtcNow;
				}
				return this.dateTime;
			}
		}

		/// <summary>Gets the unique identifier of the current process.</summary>
		/// <returns>The system-generated unique identifier of the current process.</returns>
		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x06002C75 RID: 11381 RVA: 0x000C7ACC File Offset: 0x000C5CCC
		public int ProcessId
		{
			get
			{
				return TraceEventCache.GetProcessId();
			}
		}

		/// <summary>Gets a unique identifier for the current managed thread.</summary>
		/// <returns>A string that represents a unique integer identifier for this managed thread.</returns>
		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x06002C76 RID: 11382 RVA: 0x000C7AD4 File Offset: 0x000C5CD4
		public string ThreadId
		{
			get
			{
				return TraceEventCache.GetThreadId().ToString(CultureInfo.InvariantCulture);
			}
		}

		/// <summary>Gets the current number of ticks in the timer mechanism.</summary>
		/// <returns>The tick counter value of the underlying timer mechanism.</returns>
		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x06002C77 RID: 11383 RVA: 0x000C7AF3 File Offset: 0x000C5CF3
		public long Timestamp
		{
			get
			{
				if (this.timeStamp == -1L)
				{
					this.timeStamp = Stopwatch.GetTimestamp();
				}
				return this.timeStamp;
			}
		}

		// Token: 0x06002C78 RID: 11384 RVA: 0x000C7B10 File Offset: 0x000C5D10
		private static void InitProcessInfo()
		{
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
			if (TraceEventCache.processName == null)
			{
				Process currentProcess = Process.GetCurrentProcess();
				try
				{
					TraceEventCache.processId = currentProcess.Id;
					TraceEventCache.processName = currentProcess.ProcessName;
				}
				finally
				{
					currentProcess.Dispose();
				}
			}
		}

		// Token: 0x06002C79 RID: 11385 RVA: 0x000C7B6C File Offset: 0x000C5D6C
		internal static int GetProcessId()
		{
			TraceEventCache.InitProcessInfo();
			return TraceEventCache.processId;
		}

		// Token: 0x06002C7A RID: 11386 RVA: 0x000C7B7A File Offset: 0x000C5D7A
		internal static string GetProcessName()
		{
			TraceEventCache.InitProcessInfo();
			return TraceEventCache.processName;
		}

		// Token: 0x06002C7B RID: 11387 RVA: 0x000C7B88 File Offset: 0x000C5D88
		internal static int GetThreadId()
		{
			return Thread.CurrentThread.ManagedThreadId;
		}

		// Token: 0x040026B6 RID: 9910
		private static volatile int processId;

		// Token: 0x040026B7 RID: 9911
		private static volatile string processName;

		// Token: 0x040026B8 RID: 9912
		private long timeStamp = -1L;

		// Token: 0x040026B9 RID: 9913
		private DateTime dateTime = DateTime.MinValue;

		// Token: 0x040026BA RID: 9914
		private string stackTrace;
	}
}
