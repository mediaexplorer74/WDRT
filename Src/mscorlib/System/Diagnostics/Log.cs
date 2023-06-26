using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Diagnostics
{
	// Token: 0x020003F2 RID: 1010
	internal static class Log
	{
		// Token: 0x06003351 RID: 13137 RVA: 0x000C66D1 File Offset: 0x000C48D1
		static Log()
		{
			Log.GlobalSwitch.MinimumLevel = LoggingLevels.ErrorLevel;
		}

		// Token: 0x06003352 RID: 13138 RVA: 0x000C6710 File Offset: 0x000C4910
		public static void AddOnLogMessage(LogMessageEventHandler handler)
		{
			object obj = Log.locker;
			lock (obj)
			{
				Log._LogMessageEventHandler = (LogMessageEventHandler)Delegate.Combine(Log._LogMessageEventHandler, handler);
			}
		}

		// Token: 0x06003353 RID: 13139 RVA: 0x000C6760 File Offset: 0x000C4960
		public static void RemoveOnLogMessage(LogMessageEventHandler handler)
		{
			object obj = Log.locker;
			lock (obj)
			{
				Log._LogMessageEventHandler = (LogMessageEventHandler)Delegate.Remove(Log._LogMessageEventHandler, handler);
			}
		}

		// Token: 0x06003354 RID: 13140 RVA: 0x000C67B0 File Offset: 0x000C49B0
		public static void AddOnLogSwitchLevel(LogSwitchLevelHandler handler)
		{
			object obj = Log.locker;
			lock (obj)
			{
				Log._LogSwitchLevelHandler = (LogSwitchLevelHandler)Delegate.Combine(Log._LogSwitchLevelHandler, handler);
			}
		}

		// Token: 0x06003355 RID: 13141 RVA: 0x000C6804 File Offset: 0x000C4A04
		public static void RemoveOnLogSwitchLevel(LogSwitchLevelHandler handler)
		{
			object obj = Log.locker;
			lock (obj)
			{
				Log._LogSwitchLevelHandler = (LogSwitchLevelHandler)Delegate.Remove(Log._LogSwitchLevelHandler, handler);
			}
		}

		// Token: 0x06003356 RID: 13142 RVA: 0x000C6858 File Offset: 0x000C4A58
		internal static void InvokeLogSwitchLevelHandlers(LogSwitch ls, LoggingLevels newLevel)
		{
			LogSwitchLevelHandler logSwitchLevelHandler = Log._LogSwitchLevelHandler;
			if (logSwitchLevelHandler != null)
			{
				logSwitchLevelHandler(ls, newLevel);
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06003357 RID: 13143 RVA: 0x000C6878 File Offset: 0x000C4A78
		// (set) Token: 0x06003358 RID: 13144 RVA: 0x000C6881 File Offset: 0x000C4A81
		public static bool IsConsoleEnabled
		{
			get
			{
				return Log.m_fConsoleDeviceEnabled;
			}
			set
			{
				Log.m_fConsoleDeviceEnabled = value;
			}
		}

		// Token: 0x06003359 RID: 13145 RVA: 0x000C688B File Offset: 0x000C4A8B
		public static void LogMessage(LoggingLevels level, string message)
		{
			Log.LogMessage(level, Log.GlobalSwitch, message);
		}

		// Token: 0x0600335A RID: 13146 RVA: 0x000C689C File Offset: 0x000C4A9C
		public static void LogMessage(LoggingLevels level, LogSwitch logswitch, string message)
		{
			if (logswitch == null)
			{
				throw new ArgumentNullException("LogSwitch");
			}
			if (level < LoggingLevels.TraceLevel0)
			{
				throw new ArgumentOutOfRangeException("level", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (logswitch.CheckLevel(level))
			{
				Debugger.Log((int)level, logswitch.strName, message);
				if (Log.m_fConsoleDeviceEnabled)
				{
					Console.Write(message);
				}
			}
		}

		// Token: 0x0600335B RID: 13147 RVA: 0x000C68F5 File Offset: 0x000C4AF5
		public static void Trace(LogSwitch logswitch, string message)
		{
			Log.LogMessage(LoggingLevels.TraceLevel0, logswitch, message);
		}

		// Token: 0x0600335C RID: 13148 RVA: 0x000C6900 File Offset: 0x000C4B00
		public static void Trace(string switchname, string message)
		{
			LogSwitch @switch = LogSwitch.GetSwitch(switchname);
			Log.LogMessage(LoggingLevels.TraceLevel0, @switch, message);
		}

		// Token: 0x0600335D RID: 13149 RVA: 0x000C691C File Offset: 0x000C4B1C
		public static void Trace(string message)
		{
			Log.LogMessage(LoggingLevels.TraceLevel0, Log.GlobalSwitch, message);
		}

		// Token: 0x0600335E RID: 13150 RVA: 0x000C692A File Offset: 0x000C4B2A
		public static void Status(LogSwitch logswitch, string message)
		{
			Log.LogMessage(LoggingLevels.StatusLevel0, logswitch, message);
		}

		// Token: 0x0600335F RID: 13151 RVA: 0x000C6938 File Offset: 0x000C4B38
		public static void Status(string switchname, string message)
		{
			LogSwitch @switch = LogSwitch.GetSwitch(switchname);
			Log.LogMessage(LoggingLevels.StatusLevel0, @switch, message);
		}

		// Token: 0x06003360 RID: 13152 RVA: 0x000C6955 File Offset: 0x000C4B55
		public static void Status(string message)
		{
			Log.LogMessage(LoggingLevels.StatusLevel0, Log.GlobalSwitch, message);
		}

		// Token: 0x06003361 RID: 13153 RVA: 0x000C6964 File Offset: 0x000C4B64
		public static void Warning(LogSwitch logswitch, string message)
		{
			Log.LogMessage(LoggingLevels.WarningLevel, logswitch, message);
		}

		// Token: 0x06003362 RID: 13154 RVA: 0x000C6970 File Offset: 0x000C4B70
		public static void Warning(string switchname, string message)
		{
			LogSwitch @switch = LogSwitch.GetSwitch(switchname);
			Log.LogMessage(LoggingLevels.WarningLevel, @switch, message);
		}

		// Token: 0x06003363 RID: 13155 RVA: 0x000C698D File Offset: 0x000C4B8D
		public static void Warning(string message)
		{
			Log.LogMessage(LoggingLevels.WarningLevel, Log.GlobalSwitch, message);
		}

		// Token: 0x06003364 RID: 13156 RVA: 0x000C699C File Offset: 0x000C4B9C
		public static void Error(LogSwitch logswitch, string message)
		{
			Log.LogMessage(LoggingLevels.ErrorLevel, logswitch, message);
		}

		// Token: 0x06003365 RID: 13157 RVA: 0x000C69A8 File Offset: 0x000C4BA8
		public static void Error(string switchname, string message)
		{
			LogSwitch @switch = LogSwitch.GetSwitch(switchname);
			Log.LogMessage(LoggingLevels.ErrorLevel, @switch, message);
		}

		// Token: 0x06003366 RID: 13158 RVA: 0x000C69C5 File Offset: 0x000C4BC5
		public static void Error(string message)
		{
			Log.LogMessage(LoggingLevels.ErrorLevel, Log.GlobalSwitch, message);
		}

		// Token: 0x06003367 RID: 13159 RVA: 0x000C69D4 File Offset: 0x000C4BD4
		public static void Panic(string message)
		{
			Log.LogMessage(LoggingLevels.PanicLevel, Log.GlobalSwitch, message);
		}

		// Token: 0x06003368 RID: 13160
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void AddLogSwitch(LogSwitch logSwitch);

		// Token: 0x06003369 RID: 13161
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ModifyLogSwitch(int iNewLevel, string strSwitchName, string strParentName);

		// Token: 0x040016BD RID: 5821
		internal static Hashtable m_Hashtable = new Hashtable();

		// Token: 0x040016BE RID: 5822
		private static volatile bool m_fConsoleDeviceEnabled = false;

		// Token: 0x040016BF RID: 5823
		private static LogMessageEventHandler _LogMessageEventHandler;

		// Token: 0x040016C0 RID: 5824
		private static volatile LogSwitchLevelHandler _LogSwitchLevelHandler;

		// Token: 0x040016C1 RID: 5825
		private static object locker = new object();

		// Token: 0x040016C2 RID: 5826
		public static readonly LogSwitch GlobalSwitch = new LogSwitch("Global", "Global Switch for this log");
	}
}
