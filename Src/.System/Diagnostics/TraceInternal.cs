using System;
using System.Collections;
using System.IO;
using System.Security.Permissions;

namespace System.Diagnostics
{
	// Token: 0x020004B0 RID: 1200
	internal static class TraceInternal
	{
		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x06002C82 RID: 11394 RVA: 0x000C7C18 File Offset: 0x000C5E18
		public static TraceListenerCollection Listeners
		{
			get
			{
				TraceInternal.InitializeSettings();
				if (TraceInternal.listeners == null)
				{
					object obj = TraceInternal.critSec;
					lock (obj)
					{
						if (TraceInternal.listeners == null)
						{
							SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.SystemDiagnosticsSection;
							if (systemDiagnosticsSection != null)
							{
								TraceInternal.listeners = systemDiagnosticsSection.Trace.Listeners.GetRuntimeObject();
							}
							else
							{
								TraceInternal.listeners = new TraceListenerCollection();
								TraceListener traceListener = new DefaultTraceListener();
								traceListener.IndentLevel = TraceInternal.indentLevel;
								traceListener.IndentSize = TraceInternal.indentSize;
								TraceInternal.listeners.Add(traceListener);
							}
						}
					}
				}
				return TraceInternal.listeners;
			}
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x06002C83 RID: 11395 RVA: 0x000C7CCC File Offset: 0x000C5ECC
		internal static string AppName
		{
			get
			{
				if (TraceInternal.appName == null)
				{
					new EnvironmentPermission(EnvironmentPermissionAccess.Read, "Path").Assert();
					TraceInternal.appName = Path.GetFileName(Environment.GetCommandLineArgs()[0]);
				}
				return TraceInternal.appName;
			}
		}

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x06002C84 RID: 11396 RVA: 0x000C7D01 File Offset: 0x000C5F01
		// (set) Token: 0x06002C85 RID: 11397 RVA: 0x000C7D0F File Offset: 0x000C5F0F
		public static bool AutoFlush
		{
			get
			{
				TraceInternal.InitializeSettings();
				return TraceInternal.autoFlush;
			}
			set
			{
				TraceInternal.InitializeSettings();
				TraceInternal.autoFlush = value;
			}
		}

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x06002C86 RID: 11398 RVA: 0x000C7D1E File Offset: 0x000C5F1E
		// (set) Token: 0x06002C87 RID: 11399 RVA: 0x000C7D2C File Offset: 0x000C5F2C
		public static bool UseGlobalLock
		{
			get
			{
				TraceInternal.InitializeSettings();
				return TraceInternal.useGlobalLock;
			}
			set
			{
				TraceInternal.InitializeSettings();
				TraceInternal.useGlobalLock = value;
			}
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x06002C88 RID: 11400 RVA: 0x000C7D3B File Offset: 0x000C5F3B
		// (set) Token: 0x06002C89 RID: 11401 RVA: 0x000C7D44 File Offset: 0x000C5F44
		public static int IndentLevel
		{
			get
			{
				return TraceInternal.indentLevel;
			}
			set
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					if (value < 0)
					{
						value = 0;
					}
					TraceInternal.indentLevel = value;
					if (TraceInternal.listeners != null)
					{
						foreach (object obj2 in TraceInternal.Listeners)
						{
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.IndentLevel = TraceInternal.indentLevel;
						}
					}
				}
			}
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06002C8A RID: 11402 RVA: 0x000C7DE4 File Offset: 0x000C5FE4
		// (set) Token: 0x06002C8B RID: 11403 RVA: 0x000C7DF2 File Offset: 0x000C5FF2
		public static int IndentSize
		{
			get
			{
				TraceInternal.InitializeSettings();
				return TraceInternal.indentSize;
			}
			set
			{
				TraceInternal.InitializeSettings();
				TraceInternal.SetIndentSize(value);
			}
		}

		// Token: 0x06002C8C RID: 11404 RVA: 0x000C7E00 File Offset: 0x000C6000
		private static void SetIndentSize(int value)
		{
			object obj = TraceInternal.critSec;
			lock (obj)
			{
				if (value < 0)
				{
					value = 0;
				}
				TraceInternal.indentSize = value;
				if (TraceInternal.listeners != null)
				{
					foreach (object obj2 in TraceInternal.Listeners)
					{
						TraceListener traceListener = (TraceListener)obj2;
						traceListener.IndentSize = TraceInternal.indentSize;
					}
				}
			}
		}

		// Token: 0x06002C8D RID: 11405 RVA: 0x000C7EA4 File Offset: 0x000C60A4
		public static void Indent()
		{
			object obj = TraceInternal.critSec;
			lock (obj)
			{
				TraceInternal.InitializeSettings();
				if (TraceInternal.indentLevel < 2147483647)
				{
					TraceInternal.indentLevel++;
				}
				foreach (object obj2 in TraceInternal.Listeners)
				{
					TraceListener traceListener = (TraceListener)obj2;
					traceListener.IndentLevel = TraceInternal.indentLevel;
				}
			}
		}

		// Token: 0x06002C8E RID: 11406 RVA: 0x000C7F48 File Offset: 0x000C6148
		public static void Unindent()
		{
			object obj = TraceInternal.critSec;
			lock (obj)
			{
				TraceInternal.InitializeSettings();
				if (TraceInternal.indentLevel > 0)
				{
					TraceInternal.indentLevel--;
				}
				foreach (object obj2 in TraceInternal.Listeners)
				{
					TraceListener traceListener = (TraceListener)obj2;
					traceListener.IndentLevel = TraceInternal.indentLevel;
				}
			}
		}

		// Token: 0x06002C8F RID: 11407 RVA: 0x000C7FE8 File Offset: 0x000C61E8
		public static void Flush()
		{
			if (TraceInternal.listeners != null)
			{
				if (TraceInternal.UseGlobalLock)
				{
					object obj = TraceInternal.critSec;
					lock (obj)
					{
						using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								object obj2 = enumerator.Current;
								TraceListener traceListener = (TraceListener)obj2;
								traceListener.Flush();
							}
							return;
						}
					}
				}
				foreach (object obj3 in TraceInternal.Listeners)
				{
					TraceListener traceListener2 = (TraceListener)obj3;
					if (!traceListener2.IsThreadSafe)
					{
						TraceListener traceListener3 = traceListener2;
						lock (traceListener3)
						{
							traceListener2.Flush();
							continue;
						}
					}
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x06002C90 RID: 11408 RVA: 0x000C8108 File Offset: 0x000C6308
		public static void Close()
		{
			if (TraceInternal.listeners != null)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					foreach (object obj2 in TraceInternal.Listeners)
					{
						TraceListener traceListener = (TraceListener)obj2;
						traceListener.Close();
					}
				}
			}
		}

		// Token: 0x06002C91 RID: 11409 RVA: 0x000C8194 File Offset: 0x000C6394
		public static void Assert(bool condition)
		{
			if (condition)
			{
				return;
			}
			TraceInternal.Fail(string.Empty);
		}

		// Token: 0x06002C92 RID: 11410 RVA: 0x000C81A4 File Offset: 0x000C63A4
		public static void Assert(bool condition, string message)
		{
			if (condition)
			{
				return;
			}
			TraceInternal.Fail(message);
		}

		// Token: 0x06002C93 RID: 11411 RVA: 0x000C81B0 File Offset: 0x000C63B0
		public static void Assert(bool condition, string message, string detailMessage)
		{
			if (condition)
			{
				return;
			}
			TraceInternal.Fail(message, detailMessage);
		}

		// Token: 0x06002C94 RID: 11412 RVA: 0x000C81C0 File Offset: 0x000C63C0
		public static void Fail(string message)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.Fail(message);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener traceListener3 = traceListener2;
					lock (traceListener3)
					{
						traceListener2.Fail(message);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.Fail(message);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x06002C95 RID: 11413 RVA: 0x000C8300 File Offset: 0x000C6500
		public static void Fail(string message, string detailMessage)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.Fail(message, detailMessage);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener traceListener3 = traceListener2;
					lock (traceListener3)
					{
						traceListener2.Fail(message, detailMessage);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.Fail(message, detailMessage);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x06002C96 RID: 11414 RVA: 0x000C8444 File Offset: 0x000C6644
		private static void InitializeSettings()
		{
			if (!TraceInternal.settingsInitialized || (TraceInternal.defaultInitialized && DiagnosticsConfiguration.IsInitialized()))
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					if (!TraceInternal.settingsInitialized || (TraceInternal.defaultInitialized && DiagnosticsConfiguration.IsInitialized()))
					{
						TraceInternal.defaultInitialized = DiagnosticsConfiguration.IsInitializing();
						TraceInternal.SetIndentSize(DiagnosticsConfiguration.IndentSize);
						TraceInternal.autoFlush = DiagnosticsConfiguration.AutoFlush;
						TraceInternal.useGlobalLock = DiagnosticsConfiguration.UseGlobalLock;
						TraceInternal.settingsInitialized = true;
					}
				}
			}
		}

		// Token: 0x06002C97 RID: 11415 RVA: 0x000C84E8 File Offset: 0x000C66E8
		internal static void Refresh()
		{
			object obj = TraceInternal.critSec;
			lock (obj)
			{
				TraceInternal.settingsInitialized = false;
				TraceInternal.listeners = null;
			}
			TraceInternal.InitializeSettings();
		}

		// Token: 0x06002C98 RID: 11416 RVA: 0x000C8538 File Offset: 0x000C6738
		public static void TraceEvent(TraceEventType eventType, int id, string format, params object[] args)
		{
			TraceEventCache traceEventCache = new TraceEventCache();
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					if (args == null)
					{
						using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								object obj2 = enumerator.Current;
								TraceListener traceListener = (TraceListener)obj2;
								traceListener.TraceEvent(traceEventCache, TraceInternal.AppName, eventType, id, format);
								if (TraceInternal.AutoFlush)
								{
									traceListener.Flush();
								}
							}
							return;
						}
					}
					using (IEnumerator enumerator2 = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							object obj3 = enumerator2.Current;
							TraceListener traceListener2 = (TraceListener)obj3;
							traceListener2.TraceEvent(traceEventCache, TraceInternal.AppName, eventType, id, format, args);
							if (TraceInternal.AutoFlush)
							{
								traceListener2.Flush();
							}
						}
						return;
					}
				}
			}
			if (args == null)
			{
				using (IEnumerator enumerator3 = TraceInternal.Listeners.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						object obj4 = enumerator3.Current;
						TraceListener traceListener3 = (TraceListener)obj4;
						if (!traceListener3.IsThreadSafe)
						{
							TraceListener traceListener4 = traceListener3;
							lock (traceListener4)
							{
								traceListener3.TraceEvent(traceEventCache, TraceInternal.AppName, eventType, id, format);
								if (TraceInternal.AutoFlush)
								{
									traceListener3.Flush();
								}
								continue;
							}
						}
						traceListener3.TraceEvent(traceEventCache, TraceInternal.AppName, eventType, id, format);
						if (TraceInternal.AutoFlush)
						{
							traceListener3.Flush();
						}
					}
					return;
				}
			}
			foreach (object obj5 in TraceInternal.Listeners)
			{
				TraceListener traceListener5 = (TraceListener)obj5;
				if (!traceListener5.IsThreadSafe)
				{
					TraceListener traceListener6 = traceListener5;
					lock (traceListener6)
					{
						traceListener5.TraceEvent(traceEventCache, TraceInternal.AppName, eventType, id, format, args);
						if (TraceInternal.AutoFlush)
						{
							traceListener5.Flush();
						}
						continue;
					}
				}
				traceListener5.TraceEvent(traceEventCache, TraceInternal.AppName, eventType, id, format, args);
				if (TraceInternal.AutoFlush)
				{
					traceListener5.Flush();
				}
			}
		}

		// Token: 0x06002C99 RID: 11417 RVA: 0x000C87CC File Offset: 0x000C69CC
		public static void Write(string message)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.Write(message);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener traceListener3 = traceListener2;
					lock (traceListener3)
					{
						traceListener2.Write(message);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.Write(message);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x06002C9A RID: 11418 RVA: 0x000C890C File Offset: 0x000C6B0C
		public static void Write(object value)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.Write(value);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener traceListener3 = traceListener2;
					lock (traceListener3)
					{
						traceListener2.Write(value);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.Write(value);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x06002C9B RID: 11419 RVA: 0x000C8A4C File Offset: 0x000C6C4C
		public static void Write(string message, string category)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.Write(message, category);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener traceListener3 = traceListener2;
					lock (traceListener3)
					{
						traceListener2.Write(message, category);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.Write(message, category);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x06002C9C RID: 11420 RVA: 0x000C8B90 File Offset: 0x000C6D90
		public static void Write(object value, string category)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.Write(value, category);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener traceListener3 = traceListener2;
					lock (traceListener3)
					{
						traceListener2.Write(value, category);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.Write(value, category);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x06002C9D RID: 11421 RVA: 0x000C8CD4 File Offset: 0x000C6ED4
		public static void WriteLine(string message)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.WriteLine(message);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener traceListener3 = traceListener2;
					lock (traceListener3)
					{
						traceListener2.WriteLine(message);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.WriteLine(message);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x06002C9E RID: 11422 RVA: 0x000C8E14 File Offset: 0x000C7014
		public static void WriteLine(object value)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.WriteLine(value);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener traceListener3 = traceListener2;
					lock (traceListener3)
					{
						traceListener2.WriteLine(value);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.WriteLine(value);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x06002C9F RID: 11423 RVA: 0x000C8F54 File Offset: 0x000C7154
		public static void WriteLine(string message, string category)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.WriteLine(message, category);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener traceListener3 = traceListener2;
					lock (traceListener3)
					{
						traceListener2.WriteLine(message, category);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.WriteLine(message, category);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x06002CA0 RID: 11424 RVA: 0x000C9098 File Offset: 0x000C7298
		public static void WriteLine(object value, string category)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.WriteLine(value, category);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener traceListener3 = traceListener2;
					lock (traceListener3)
					{
						traceListener2.WriteLine(value, category);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.WriteLine(value, category);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x06002CA1 RID: 11425 RVA: 0x000C91DC File Offset: 0x000C73DC
		public static void WriteIf(bool condition, string message)
		{
			if (condition)
			{
				TraceInternal.Write(message);
			}
		}

		// Token: 0x06002CA2 RID: 11426 RVA: 0x000C91E7 File Offset: 0x000C73E7
		public static void WriteIf(bool condition, object value)
		{
			if (condition)
			{
				TraceInternal.Write(value);
			}
		}

		// Token: 0x06002CA3 RID: 11427 RVA: 0x000C91F2 File Offset: 0x000C73F2
		public static void WriteIf(bool condition, string message, string category)
		{
			if (condition)
			{
				TraceInternal.Write(message, category);
			}
		}

		// Token: 0x06002CA4 RID: 11428 RVA: 0x000C91FE File Offset: 0x000C73FE
		public static void WriteIf(bool condition, object value, string category)
		{
			if (condition)
			{
				TraceInternal.Write(value, category);
			}
		}

		// Token: 0x06002CA5 RID: 11429 RVA: 0x000C920A File Offset: 0x000C740A
		public static void WriteLineIf(bool condition, string message)
		{
			if (condition)
			{
				TraceInternal.WriteLine(message);
			}
		}

		// Token: 0x06002CA6 RID: 11430 RVA: 0x000C9215 File Offset: 0x000C7415
		public static void WriteLineIf(bool condition, object value)
		{
			if (condition)
			{
				TraceInternal.WriteLine(value);
			}
		}

		// Token: 0x06002CA7 RID: 11431 RVA: 0x000C9220 File Offset: 0x000C7420
		public static void WriteLineIf(bool condition, string message, string category)
		{
			if (condition)
			{
				TraceInternal.WriteLine(message, category);
			}
		}

		// Token: 0x06002CA8 RID: 11432 RVA: 0x000C922C File Offset: 0x000C742C
		public static void WriteLineIf(bool condition, object value, string category)
		{
			if (condition)
			{
				TraceInternal.WriteLine(value, category);
			}
		}

		// Token: 0x040026C7 RID: 9927
		private static volatile string appName = null;

		// Token: 0x040026C8 RID: 9928
		private static volatile TraceListenerCollection listeners;

		// Token: 0x040026C9 RID: 9929
		private static volatile bool autoFlush;

		// Token: 0x040026CA RID: 9930
		private static volatile bool useGlobalLock;

		// Token: 0x040026CB RID: 9931
		[ThreadStatic]
		private static int indentLevel;

		// Token: 0x040026CC RID: 9932
		private static volatile int indentSize;

		// Token: 0x040026CD RID: 9933
		private static volatile bool settingsInitialized;

		// Token: 0x040026CE RID: 9934
		private static volatile bool defaultInitialized;

		// Token: 0x040026CF RID: 9935
		internal static readonly object critSec = new object();
	}
}
