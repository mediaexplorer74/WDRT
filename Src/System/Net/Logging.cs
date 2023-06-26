using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000151 RID: 337
	[FriendAccessAllowed]
	internal class Logging
	{
		// Token: 0x06000BAB RID: 2987 RVA: 0x0003F51D File Offset: 0x0003D71D
		private Logging()
		{
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x0003F528 File Offset: 0x0003D728
		private static object InternalSyncObject
		{
			get
			{
				if (Logging.s_InternalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange(ref Logging.s_InternalSyncObject, obj, null);
				}
				return Logging.s_InternalSyncObject;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x0003F554 File Offset: 0x0003D754
		[FriendAccessAllowed]
		internal static bool On
		{
			get
			{
				if (!Logging.s_LoggingInitialized)
				{
					Logging.InitializeLogging();
				}
				return Logging.s_LoggingEnabled;
			}
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0003F56B File Offset: 0x0003D76B
		internal static bool IsVerbose(TraceSource traceSource)
		{
			return Logging.ValidateSettings(traceSource, TraceEventType.Verbose);
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000BAF RID: 2991 RVA: 0x0003F575 File Offset: 0x0003D775
		internal static TraceSource Web
		{
			get
			{
				if (!Logging.s_LoggingInitialized)
				{
					Logging.InitializeLogging();
				}
				if (!Logging.s_LoggingEnabled)
				{
					return null;
				}
				return Logging.s_WebTraceSource;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x0003F595 File Offset: 0x0003D795
		[FriendAccessAllowed]
		internal static TraceSource Http
		{
			get
			{
				if (!Logging.s_LoggingInitialized)
				{
					Logging.InitializeLogging();
				}
				if (!Logging.s_LoggingEnabled)
				{
					return null;
				}
				return Logging.s_TraceSourceHttpName;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000BB1 RID: 2993 RVA: 0x0003F5B5 File Offset: 0x0003D7B5
		internal static TraceSource HttpListener
		{
			get
			{
				if (!Logging.s_LoggingInitialized)
				{
					Logging.InitializeLogging();
				}
				if (!Logging.s_LoggingEnabled)
				{
					return null;
				}
				return Logging.s_HttpListenerTraceSource;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x0003F5D5 File Offset: 0x0003D7D5
		internal static TraceSource Sockets
		{
			get
			{
				if (!Logging.s_LoggingInitialized)
				{
					Logging.InitializeLogging();
				}
				if (!Logging.s_LoggingEnabled)
				{
					return null;
				}
				return Logging.s_SocketsTraceSource;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000BB3 RID: 2995 RVA: 0x0003F5F5 File Offset: 0x0003D7F5
		internal static TraceSource RequestCache
		{
			get
			{
				if (!Logging.s_LoggingInitialized)
				{
					Logging.InitializeLogging();
				}
				if (!Logging.s_LoggingEnabled)
				{
					return null;
				}
				return Logging.s_CacheTraceSource;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x0003F615 File Offset: 0x0003D815
		internal static TraceSource WebSockets
		{
			get
			{
				if (!Logging.s_LoggingInitialized)
				{
					Logging.InitializeLogging();
				}
				if (!Logging.s_LoggingEnabled)
				{
					return null;
				}
				return Logging.s_WebSocketsTraceSource;
			}
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0003F638 File Offset: 0x0003D838
		private static bool GetUseProtocolTextSetting(TraceSource traceSource)
		{
			bool flag = false;
			if (traceSource.Attributes["tracemode"] == "protocolonly")
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0003F668 File Offset: 0x0003D868
		private static int GetMaxDumpSizeSetting(TraceSource traceSource)
		{
			int num = 1024;
			if (traceSource.Attributes.ContainsKey("maxdatasize"))
			{
				try
				{
					num = int.Parse(traceSource.Attributes["maxdatasize"], NumberFormatInfo.InvariantInfo);
				}
				catch (Exception ex)
				{
					if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
					{
						throw;
					}
					traceSource.Attributes["maxdatasize"] = num.ToString(NumberFormatInfo.InvariantInfo);
				}
			}
			return num;
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0003F6F4 File Offset: 0x0003D8F4
		private static void InitializeLogging()
		{
			object internalSyncObject = Logging.InternalSyncObject;
			lock (internalSyncObject)
			{
				if (!Logging.s_LoggingInitialized)
				{
					bool flag2 = false;
					Logging.s_WebTraceSource = new Logging.NclTraceSource("System.Net");
					Logging.s_HttpListenerTraceSource = new Logging.NclTraceSource("System.Net.HttpListener");
					Logging.s_SocketsTraceSource = new Logging.NclTraceSource("System.Net.Sockets");
					Logging.s_WebSocketsTraceSource = new Logging.NclTraceSource("System.Net.WebSockets");
					Logging.s_CacheTraceSource = new Logging.NclTraceSource("System.Net.Cache");
					Logging.s_TraceSourceHttpName = new Logging.NclTraceSource("System.Net.Http");
					try
					{
						flag2 = Logging.s_WebTraceSource.Switch.ShouldTrace(TraceEventType.Critical) || Logging.s_HttpListenerTraceSource.Switch.ShouldTrace(TraceEventType.Critical) || Logging.s_SocketsTraceSource.Switch.ShouldTrace(TraceEventType.Critical) || Logging.s_WebSocketsTraceSource.Switch.ShouldTrace(TraceEventType.Critical) || Logging.s_CacheTraceSource.Switch.ShouldTrace(TraceEventType.Critical) || Logging.s_TraceSourceHttpName.Switch.ShouldTrace(TraceEventType.Critical);
					}
					catch (SecurityException)
					{
						Logging.Close();
						flag2 = false;
					}
					if (flag2)
					{
						AppDomain currentDomain = AppDomain.CurrentDomain;
						currentDomain.UnhandledException += Logging.UnhandledExceptionHandler;
						currentDomain.DomainUnload += Logging.AppDomainUnloadEvent;
						currentDomain.ProcessExit += Logging.ProcessExitEvent;
					}
					Logging.s_LoggingEnabled = flag2;
					Logging.s_LoggingInitialized = true;
				}
			}
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0003F884 File Offset: 0x0003DA84
		private static void Close()
		{
			if (Logging.s_WebTraceSource != null)
			{
				Logging.s_WebTraceSource.Close();
			}
			if (Logging.s_HttpListenerTraceSource != null)
			{
				Logging.s_HttpListenerTraceSource.Close();
			}
			if (Logging.s_SocketsTraceSource != null)
			{
				Logging.s_SocketsTraceSource.Close();
			}
			if (Logging.s_WebSocketsTraceSource != null)
			{
				Logging.s_WebSocketsTraceSource.Close();
			}
			if (Logging.s_CacheTraceSource != null)
			{
				Logging.s_CacheTraceSource.Close();
			}
			if (Logging.s_TraceSourceHttpName != null)
			{
				Logging.s_TraceSourceHttpName.Close();
			}
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0003F8F8 File Offset: 0x0003DAF8
		private static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs args)
		{
			Exception ex = (Exception)args.ExceptionObject;
			Logging.Exception(Logging.Web, sender, "UnhandledExceptionHandler", ex);
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0003F922 File Offset: 0x0003DB22
		private static void ProcessExitEvent(object sender, EventArgs e)
		{
			Logging.Close();
			Logging.s_AppDomainShutdown = true;
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0003F931 File Offset: 0x0003DB31
		private static void AppDomainUnloadEvent(object sender, EventArgs e)
		{
			Logging.Close();
			Logging.s_AppDomainShutdown = true;
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0003F940 File Offset: 0x0003DB40
		private static bool ValidateSettings(TraceSource traceSource, TraceEventType traceLevel)
		{
			if (!Logging.s_LoggingEnabled)
			{
				return false;
			}
			if (!Logging.s_LoggingInitialized)
			{
				Logging.InitializeLogging();
			}
			return traceSource != null && traceSource.Switch.ShouldTrace(traceLevel) && !Logging.s_AppDomainShutdown;
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0003F97A File Offset: 0x0003DB7A
		private static string GetObjectName(object obj)
		{
			if (obj is Uri || obj is IPAddress || obj is IPEndPoint)
			{
				return obj.ToString();
			}
			return obj.GetType().Name;
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0003F9A8 File Offset: 0x0003DBA8
		internal static uint GetThreadId()
		{
			uint num = UnsafeNclNativeMethods.GetCurrentThreadId();
			if (num == 0U)
			{
				num = (uint)Thread.CurrentThread.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0003F9CC File Offset: 0x0003DBCC
		internal static void PrintLine(TraceSource traceSource, TraceEventType eventType, int id, string msg)
		{
			string text = "[" + Logging.GetThreadId().ToString("d4", CultureInfo.InvariantCulture) + "] ";
			traceSource.TraceEvent(eventType, id, text + msg);
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x0003FA10 File Offset: 0x0003DC10
		[FriendAccessAllowed]
		internal static void Associate(TraceSource traceSource, object objA, object objB)
		{
			if (!Logging.ValidateSettings(traceSource, TraceEventType.Information))
			{
				return;
			}
			string text = Logging.GetObjectName(objA) + "#" + ValidationHelper.HashString(objA);
			string text2 = Logging.GetObjectName(objB) + "#" + ValidationHelper.HashString(objB);
			Logging.PrintLine(traceSource, TraceEventType.Information, 0, "Associating " + text + " with " + text2);
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0003FA6E File Offset: 0x0003DC6E
		[FriendAccessAllowed]
		internal static void Enter(TraceSource traceSource, object obj, string method, string param)
		{
			if (!Logging.ValidateSettings(traceSource, TraceEventType.Information))
			{
				return;
			}
			Logging.Enter(traceSource, Logging.GetObjectName(obj) + "#" + ValidationHelper.HashString(obj), method, param);
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0003FA98 File Offset: 0x0003DC98
		[FriendAccessAllowed]
		internal static void Enter(TraceSource traceSource, object obj, string method, object paramObject)
		{
			if (!Logging.ValidateSettings(traceSource, TraceEventType.Information))
			{
				return;
			}
			Logging.Enter(traceSource, Logging.GetObjectName(obj) + "#" + ValidationHelper.HashString(obj), method, paramObject);
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0003FAC4 File Offset: 0x0003DCC4
		internal static void Enter(TraceSource traceSource, string obj, string method, string param)
		{
			if (!Logging.ValidateSettings(traceSource, TraceEventType.Information))
			{
				return;
			}
			Logging.Enter(traceSource, string.Concat(new string[] { obj, "::", method, "(", param, ")" }));
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0003FB10 File Offset: 0x0003DD10
		internal static void Enter(TraceSource traceSource, string obj, string method, object paramObject)
		{
			if (!Logging.ValidateSettings(traceSource, TraceEventType.Information))
			{
				return;
			}
			string text = "";
			if (paramObject != null)
			{
				text = Logging.GetObjectName(paramObject) + "#" + ValidationHelper.HashString(paramObject);
			}
			Logging.Enter(traceSource, string.Concat(new string[] { obj, "::", method, "(", text, ")" }));
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0003FB7C File Offset: 0x0003DD7C
		internal static void Enter(TraceSource traceSource, string method, string parameters)
		{
			if (!Logging.ValidateSettings(traceSource, TraceEventType.Information))
			{
				return;
			}
			Logging.Enter(traceSource, method + "(" + parameters + ")");
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0003FB9F File Offset: 0x0003DD9F
		internal static void Enter(TraceSource traceSource, string msg)
		{
			if (!Logging.ValidateSettings(traceSource, TraceEventType.Information))
			{
				return;
			}
			Logging.PrintLine(traceSource, TraceEventType.Verbose, 0, "Entering " + msg);
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0003FBC0 File Offset: 0x0003DDC0
		[FriendAccessAllowed]
		internal static void Exit(TraceSource traceSource, object obj, string method, object retObject)
		{
			if (!Logging.ValidateSettings(traceSource, TraceEventType.Information))
			{
				return;
			}
			string text = "";
			if (retObject != null)
			{
				text = Logging.GetObjectName(retObject) + "#" + ValidationHelper.HashString(retObject);
			}
			Logging.Exit(traceSource, obj, method, text);
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0003FC00 File Offset: 0x0003DE00
		internal static void Exit(TraceSource traceSource, string obj, string method, object retObject)
		{
			if (!Logging.ValidateSettings(traceSource, TraceEventType.Information))
			{
				return;
			}
			string text = "";
			if (retObject != null)
			{
				text = Logging.GetObjectName(retObject) + "#" + ValidationHelper.HashString(retObject);
			}
			Logging.Exit(traceSource, obj, method, text);
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0003FC40 File Offset: 0x0003DE40
		[FriendAccessAllowed]
		internal static void Exit(TraceSource traceSource, object obj, string method, string retValue)
		{
			if (!Logging.ValidateSettings(traceSource, TraceEventType.Information))
			{
				return;
			}
			Logging.Exit(traceSource, Logging.GetObjectName(obj) + "#" + ValidationHelper.HashString(obj), method, retValue);
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x0003FC6C File Offset: 0x0003DE6C
		internal static void Exit(TraceSource traceSource, string obj, string method, string retValue)
		{
			if (!Logging.ValidateSettings(traceSource, TraceEventType.Information))
			{
				return;
			}
			if (!ValidationHelper.IsBlankString(retValue))
			{
				retValue = "\t-> " + retValue;
			}
			Logging.Exit(traceSource, string.Concat(new string[] { obj, "::", method, "() ", retValue }));
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x0003FCC5 File Offset: 0x0003DEC5
		internal static void Exit(TraceSource traceSource, string method, string parameters)
		{
			if (!Logging.ValidateSettings(traceSource, TraceEventType.Information))
			{
				return;
			}
			Logging.Exit(traceSource, method + "() " + parameters);
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x0003FCE3 File Offset: 0x0003DEE3
		internal static void Exit(TraceSource traceSource, string msg)
		{
			if (!Logging.ValidateSettings(traceSource, TraceEventType.Information))
			{
				return;
			}
			Logging.PrintLine(traceSource, TraceEventType.Verbose, 0, "Exiting " + msg);
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0003FD04 File Offset: 0x0003DF04
		[FriendAccessAllowed]
		internal static void Exception(TraceSource traceSource, object obj, string method, Exception e)
		{
			if (!Logging.ValidateSettings(traceSource, TraceEventType.Error))
			{
				return;
			}
			string text = SR.GetString("net_log_exception", new object[]
			{
				Logging.GetObjectLogHash(obj),
				method,
				e.Message
			});
			if (!ValidationHelper.IsBlankString(e.StackTrace))
			{
				text = text + "\r\n" + e.StackTrace;
			}
			Logging.PrintLine(traceSource, TraceEventType.Error, 0, text);
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0003FD6A File Offset: 0x0003DF6A
		internal static void PrintInfo(TraceSource traceSource, string msg)
		{
			if (!Logging.ValidateSettings(traceSource, TraceEventType.Information))
			{
				return;
			}
			Logging.PrintLine(traceSource, TraceEventType.Information, 0, msg);
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x0003FD80 File Offset: 0x0003DF80
		[FriendAccessAllowed]
		internal static void PrintInfo(TraceSource traceSource, object obj, string msg)
		{
			if (!Logging.ValidateSettings(traceSource, TraceEventType.Information))
			{
				return;
			}
			Logging.PrintLine(traceSource, TraceEventType.Information, 0, string.Concat(new string[]
			{
				Logging.GetObjectName(obj),
				"#",
				ValidationHelper.HashString(obj),
				" - ",
				msg
			}));
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x0003FDD0 File Offset: 0x0003DFD0
		internal static void PrintInfo(TraceSource traceSource, object obj, string method, string param)
		{
			if (!Logging.ValidateSettings(traceSource, TraceEventType.Information))
			{
				return;
			}
			Logging.PrintLine(traceSource, TraceEventType.Information, 0, string.Concat(new string[]
			{
				Logging.GetObjectName(obj),
				"#",
				ValidationHelper.HashString(obj),
				"::",
				method,
				"(",
				param,
				")"
			}));
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x0003FE34 File Offset: 0x0003E034
		[FriendAccessAllowed]
		internal static void PrintWarning(TraceSource traceSource, string msg)
		{
			if (!Logging.ValidateSettings(traceSource, TraceEventType.Warning))
			{
				return;
			}
			Logging.PrintLine(traceSource, TraceEventType.Warning, 0, msg);
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x0003FE4C File Offset: 0x0003E04C
		internal static void PrintWarning(TraceSource traceSource, object obj, string method, string msg)
		{
			if (!Logging.ValidateSettings(traceSource, TraceEventType.Warning))
			{
				return;
			}
			Logging.PrintLine(traceSource, TraceEventType.Warning, 0, string.Concat(new string[]
			{
				Logging.GetObjectName(obj),
				"#",
				ValidationHelper.HashString(obj),
				"::",
				method,
				"() - ",
				msg
			}));
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x0003FEA8 File Offset: 0x0003E0A8
		[FriendAccessAllowed]
		internal static void PrintError(TraceSource traceSource, string msg)
		{
			if (!Logging.ValidateSettings(traceSource, TraceEventType.Error))
			{
				return;
			}
			Logging.PrintLine(traceSource, TraceEventType.Error, 0, msg);
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0003FEC0 File Offset: 0x0003E0C0
		[FriendAccessAllowed]
		internal static void PrintError(TraceSource traceSource, object obj, string method, string msg)
		{
			if (!Logging.ValidateSettings(traceSource, TraceEventType.Error))
			{
				return;
			}
			Logging.PrintLine(traceSource, TraceEventType.Error, 0, string.Concat(new string[]
			{
				Logging.GetObjectName(obj),
				"#",
				ValidationHelper.HashString(obj),
				"::",
				method,
				"() - ",
				msg
			}));
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x0003FF1C File Offset: 0x0003E11C
		[FriendAccessAllowed]
		internal static string GetObjectLogHash(object obj)
		{
			return Logging.GetObjectName(obj) + "#" + ValidationHelper.HashString(obj);
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x0003FF34 File Offset: 0x0003E134
		internal static void Dump(TraceSource traceSource, object obj, string method, IntPtr bufferPtr, int length)
		{
			if (!Logging.ValidateSettings(traceSource, TraceEventType.Verbose) || bufferPtr == IntPtr.Zero || length < 0)
			{
				return;
			}
			byte[] array = new byte[length];
			Marshal.Copy(bufferPtr, array, 0, length);
			Logging.Dump(traceSource, obj, method, array, 0, length);
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x0003FF7C File Offset: 0x0003E17C
		internal static void Dump(TraceSource traceSource, object obj, string method, byte[] buffer, int offset, int length)
		{
			if (!Logging.ValidateSettings(traceSource, TraceEventType.Verbose))
			{
				return;
			}
			if (buffer == null)
			{
				Logging.PrintLine(traceSource, TraceEventType.Verbose, 0, "(null)");
				return;
			}
			if (offset > buffer.Length)
			{
				Logging.PrintLine(traceSource, TraceEventType.Verbose, 0, "(offset out of range)");
				return;
			}
			Logging.PrintLine(traceSource, TraceEventType.Verbose, 0, string.Concat(new string[]
			{
				"Data from ",
				Logging.GetObjectName(obj),
				"#",
				ValidationHelper.HashString(obj),
				"::",
				method
			}));
			int maxDumpSizeSetting = Logging.GetMaxDumpSizeSetting(traceSource);
			if (length > maxDumpSizeSetting)
			{
				Logging.PrintLine(traceSource, TraceEventType.Verbose, 0, string.Concat(new string[]
				{
					"(printing ",
					maxDumpSizeSetting.ToString(NumberFormatInfo.InvariantInfo),
					" out of ",
					length.ToString(NumberFormatInfo.InvariantInfo),
					")"
				}));
				length = maxDumpSizeSetting;
			}
			if (length < 0 || length > buffer.Length - offset)
			{
				length = buffer.Length - offset;
			}
			if (Logging.GetUseProtocolTextSetting(traceSource))
			{
				string text = "<<" + WebHeaderCollection.HeaderEncoding.GetString(buffer, offset, length) + ">>";
				Logging.PrintLine(traceSource, TraceEventType.Verbose, 0, text);
				return;
			}
			do
			{
				int num = Math.Min(length, 16);
				string text2 = string.Format(CultureInfo.CurrentCulture, "{0:X8} : ", new object[] { offset });
				for (int i = 0; i < num; i++)
				{
					text2 = text2 + string.Format(CultureInfo.CurrentCulture, "{0:X2}", new object[] { buffer[offset + i] }) + ((i == 7) ? '-' : ' ').ToString();
				}
				for (int j = num; j < 16; j++)
				{
					text2 += "   ";
				}
				text2 += ": ";
				for (int k = 0; k < num; k++)
				{
					text2 += ((char)((buffer[offset + k] < 32 || buffer[offset + k] > 126) ? 46 : buffer[offset + k])).ToString();
				}
				Logging.PrintLine(traceSource, TraceEventType.Verbose, 0, text2);
				offset += num;
				length -= num;
			}
			while (length > 0);
		}

		// Token: 0x04001106 RID: 4358
		private static volatile bool s_LoggingEnabled = true;

		// Token: 0x04001107 RID: 4359
		private static volatile bool s_LoggingInitialized;

		// Token: 0x04001108 RID: 4360
		private static volatile bool s_AppDomainShutdown;

		// Token: 0x04001109 RID: 4361
		private const int DefaultMaxDumpSize = 1024;

		// Token: 0x0400110A RID: 4362
		private const bool DefaultUseProtocolTextOnly = false;

		// Token: 0x0400110B RID: 4363
		private const string AttributeNameMaxSize = "maxdatasize";

		// Token: 0x0400110C RID: 4364
		private const string AttributeNameTraceMode = "tracemode";

		// Token: 0x0400110D RID: 4365
		private static readonly string[] SupportedAttributes = new string[] { "maxdatasize", "tracemode" };

		// Token: 0x0400110E RID: 4366
		private const string AttributeValueProtocolOnly = "protocolonly";

		// Token: 0x0400110F RID: 4367
		private const string TraceSourceWebName = "System.Net";

		// Token: 0x04001110 RID: 4368
		private const string TraceSourceHttpListenerName = "System.Net.HttpListener";

		// Token: 0x04001111 RID: 4369
		private const string TraceSourceSocketsName = "System.Net.Sockets";

		// Token: 0x04001112 RID: 4370
		private const string TraceSourceWebSocketsName = "System.Net.WebSockets";

		// Token: 0x04001113 RID: 4371
		private const string TraceSourceCacheName = "System.Net.Cache";

		// Token: 0x04001114 RID: 4372
		private const string TraceSourceHttpName = "System.Net.Http";

		// Token: 0x04001115 RID: 4373
		private static TraceSource s_WebTraceSource;

		// Token: 0x04001116 RID: 4374
		private static TraceSource s_HttpListenerTraceSource;

		// Token: 0x04001117 RID: 4375
		private static TraceSource s_SocketsTraceSource;

		// Token: 0x04001118 RID: 4376
		private static TraceSource s_WebSocketsTraceSource;

		// Token: 0x04001119 RID: 4377
		private static TraceSource s_CacheTraceSource;

		// Token: 0x0400111A RID: 4378
		private static TraceSource s_TraceSourceHttpName;

		// Token: 0x0400111B RID: 4379
		private static object s_InternalSyncObject;

		// Token: 0x0200070D RID: 1805
		private class NclTraceSource : TraceSource
		{
			// Token: 0x0600407E RID: 16510 RVA: 0x0010E45B File Offset: 0x0010C65B
			internal NclTraceSource(string name)
				: base(name)
			{
			}

			// Token: 0x0600407F RID: 16511 RVA: 0x0010E464 File Offset: 0x0010C664
			protected internal override string[] GetSupportedAttributes()
			{
				return Logging.SupportedAttributes;
			}
		}
	}
}
