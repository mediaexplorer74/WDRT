using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Configuration;
using System.Runtime.Versioning;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace System.Net
{
	// Token: 0x020001CD RID: 461
	internal sealed class NetworkingPerfCounters
	{
		// Token: 0x06001226 RID: 4646 RVA: 0x00060E14 File Offset: 0x0005F014
		private NetworkingPerfCounters()
		{
			this.enabled = SettingsSectionInternal.Section.PerformanceCountersEnabled;
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06001227 RID: 4647 RVA: 0x00060E2C File Offset: 0x0005F02C
		public static NetworkingPerfCounters Instance
		{
			get
			{
				if (NetworkingPerfCounters.instance == null)
				{
					object obj = NetworkingPerfCounters.lockObject;
					lock (obj)
					{
						if (NetworkingPerfCounters.instance == null)
						{
							NetworkingPerfCounters.CreateInstance();
						}
					}
				}
				return NetworkingPerfCounters.instance;
			}
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x00060E84 File Offset: 0x0005F084
		public static long GetTimestamp()
		{
			return Stopwatch.GetTimestamp();
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06001229 RID: 4649 RVA: 0x00060E8B File Offset: 0x0005F08B
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x00060E93 File Offset: 0x0005F093
		public void Increment(NetworkingPerfCounterName perfCounter)
		{
			this.Increment(perfCounter, 1L);
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x00060EA0 File Offset: 0x0005F0A0
		public void Increment(NetworkingPerfCounterName perfCounter, long amount)
		{
			if (this.CounterAvailable())
			{
				try
				{
					NetworkingPerfCounters.CounterPair counterPair = this.counters[(int)perfCounter];
					counterPair.InstanceCounter.IncrementBy(amount);
					counterPair.GlobalCounter.IncrementBy(amount);
				}
				catch (InvalidOperationException ex)
				{
					if (Logging.On)
					{
						Logging.Exception(Logging.Web, "NetworkingPerfCounters", "Increment", ex);
					}
				}
				catch (Win32Exception ex2)
				{
					if (Logging.On)
					{
						Logging.Exception(Logging.Web, "NetworkingPerfCounters", "Increment", ex2);
					}
				}
			}
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x00060F34 File Offset: 0x0005F134
		public void Decrement(NetworkingPerfCounterName perfCounter)
		{
			this.Increment(perfCounter, -1L);
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x00060F3F File Offset: 0x0005F13F
		public void Decrement(NetworkingPerfCounterName perfCounter, long amount)
		{
			this.Increment(perfCounter, -amount);
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x00060F4C File Offset: 0x0005F14C
		public void IncrementAverage(NetworkingPerfCounterName perfCounter, long startTimestamp)
		{
			if (this.CounterAvailable())
			{
				long timestamp = NetworkingPerfCounters.GetTimestamp();
				long num = (timestamp - startTimestamp) * 1000L / Stopwatch.Frequency;
				this.Increment(perfCounter, num);
				this.Increment(perfCounter + 1, 1L);
			}
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x00060F90 File Offset: 0x0005F190
		private void Initialize(object state)
		{
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, SR.GetString("net_perfcounter_initialization_started"));
			}
			PerformanceCounterPermission performanceCounterPermission = new PerformanceCounterPermission(PermissionState.Unrestricted);
			performanceCounterPermission.Assert();
			try
			{
				if (!PerformanceCounterCategory.Exists(".NET CLR Networking 4.0.0.0"))
				{
					if (Logging.On)
					{
						Logging.PrintError(Logging.Web, SR.GetString("net_perfcounter_nocategory", new object[] { ".NET CLR Networking 4.0.0.0" }));
					}
				}
				else
				{
					string instanceName = NetworkingPerfCounters.GetInstanceName();
					this.counters = new NetworkingPerfCounters.CounterPair[NetworkingPerfCounters.counterNames.Length];
					for (int i = 0; i < NetworkingPerfCounters.counterNames.Length; i++)
					{
						this.counters[i] = NetworkingPerfCounters.CreateCounterPair(NetworkingPerfCounters.counterNames[i], instanceName);
					}
					AppDomain.CurrentDomain.DomainUnload += this.UnloadEventHandler;
					AppDomain.CurrentDomain.ProcessExit += this.ExitEventHandler;
					AppDomain.CurrentDomain.UnhandledException += this.ExceptionEventHandler;
					this.initSuccessful = true;
				}
			}
			catch (Win32Exception ex)
			{
				if (Logging.On)
				{
					Logging.Exception(Logging.Web, "NetworkingPerfCounters", "Initialize", ex);
				}
				this.Cleanup();
			}
			catch (InvalidOperationException ex2)
			{
				if (Logging.On)
				{
					Logging.Exception(Logging.Web, "NetworkingPerfCounters", "Initialize", ex2);
				}
				this.Cleanup();
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
				this.initDone = true;
				if (Logging.On)
				{
					if (this.initSuccessful)
					{
						Logging.PrintInfo(Logging.Web, SR.GetString("net_perfcounter_initialized_success"));
					}
					else
					{
						Logging.PrintInfo(Logging.Web, SR.GetString("net_perfcounter_initialized_error"));
					}
				}
			}
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x0006116C File Offset: 0x0005F36C
		private static void CreateInstance()
		{
			NetworkingPerfCounters.instance = new NetworkingPerfCounters();
			if (NetworkingPerfCounters.instance.Enabled && !ThreadPool.QueueUserWorkItem(new WaitCallback(NetworkingPerfCounters.instance.Initialize)) && Logging.On)
			{
				Logging.PrintError(Logging.Web, SR.GetString("net_perfcounter_cant_queue_workitem"));
			}
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x000611C8 File Offset: 0x0005F3C8
		private static NetworkingPerfCounters.CounterPair CreateCounterPair(string counterName, string instanceName)
		{
			PerformanceCounter performanceCounter = new PerformanceCounter(".NET CLR Networking 4.0.0.0", counterName, "_Global_", false);
			return new NetworkingPerfCounters.CounterPair(new PerformanceCounter
			{
				CategoryName = ".NET CLR Networking 4.0.0.0",
				CounterName = counterName,
				InstanceName = instanceName,
				InstanceLifetime = PerformanceCounterInstanceLifetime.Process,
				ReadOnly = false,
				RawValue = 0L
			}, performanceCounter);
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x00061223 File Offset: 0x0005F423
		private void ExceptionEventHandler(object sender, UnhandledExceptionEventArgs e)
		{
			if (e.IsTerminating)
			{
				this.Cleanup();
			}
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x00061233 File Offset: 0x0005F433
		private void UnloadEventHandler(object sender, EventArgs e)
		{
			this.Cleanup();
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x0006123B File Offset: 0x0005F43B
		private void ExitEventHandler(object sender, EventArgs e)
		{
			this.Cleanup();
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x00061244 File Offset: 0x0005F444
		private void Cleanup()
		{
			object obj = NetworkingPerfCounters.lockObject;
			lock (obj)
			{
				if (!this.cleanupCalled)
				{
					this.cleanupCalled = true;
					if (this.counters != null)
					{
						foreach (NetworkingPerfCounters.CounterPair counterPair in this.counters)
						{
							if (!Environment.HasShutdownStarted && counterPair != null)
							{
								try
								{
									counterPair.InstanceCounter.RemoveInstance();
								}
								catch (InvalidOperationException ex)
								{
									if (Logging.On)
									{
										Logging.Exception(Logging.Web, "NetworkingPerfCounters", "Cleanup", ex);
									}
								}
								catch (Win32Exception ex2)
								{
									if (Logging.On)
									{
										Logging.Exception(Logging.Web, "NetworkingPerfCounters", "Cleanup", ex2);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x00061328 File Offset: 0x0005F528
		private static string GetInstanceName()
		{
			string text = NetworkingPerfCounters.ReplaceInvalidChars(AppDomain.CurrentDomain.FriendlyName);
			string text2 = VersioningHelper.MakeVersionSafeName(string.Empty, ResourceScope.Machine, ResourceScope.AppDomain);
			string text3 = text + text2;
			if (text3.Length > 127)
			{
				text3 = text.Substring(0, 127 - text2.Length) + text2;
			}
			return text3;
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x0006137C File Offset: 0x0005F57C
		private static string ReplaceInvalidChars(string instanceName)
		{
			StringBuilder stringBuilder = new StringBuilder(instanceName);
			int i = 0;
			while (i < stringBuilder.Length)
			{
				char c = stringBuilder[i];
				if (c <= '(')
				{
					if (c == '#')
					{
						goto IL_4B;
					}
					if (c == '(')
					{
						stringBuilder[i] = '[';
					}
				}
				else if (c != ')')
				{
					if (c == '/' || c == '\\')
					{
						goto IL_4B;
					}
				}
				else
				{
					stringBuilder[i] = ']';
				}
				IL_54:
				i++;
				continue;
				IL_4B:
				stringBuilder[i] = '_';
				goto IL_54;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x000613F0 File Offset: 0x0005F5F0
		private bool CounterAvailable()
		{
			return this.enabled && !this.cleanupCalled && this.initDone && this.initSuccessful;
		}

		// Token: 0x04001484 RID: 5252
		private const int instanceNameMaxLength = 127;

		// Token: 0x04001485 RID: 5253
		private const string categoryName = ".NET CLR Networking 4.0.0.0";

		// Token: 0x04001486 RID: 5254
		private const string globalInstanceName = "_Global_";

		// Token: 0x04001487 RID: 5255
		private static readonly string[] counterNames = new string[]
		{
			"Connections Established", "Bytes Received", "Bytes Sent", "Datagrams Received", "Datagrams Sent", "HttpWebRequests Created/Sec", "HttpWebRequests Average Lifetime", "HttpWebRequests Average Lifetime Base", "HttpWebRequests Queued/Sec", "HttpWebRequests Average Queue Time",
			"HttpWebRequests Average Queue Time Base", "HttpWebRequests Aborted/Sec", "HttpWebRequests Failed/Sec"
		};

		// Token: 0x04001488 RID: 5256
		private static volatile NetworkingPerfCounters instance;

		// Token: 0x04001489 RID: 5257
		private static object lockObject = new object();

		// Token: 0x0400148A RID: 5258
		private volatile bool initDone;

		// Token: 0x0400148B RID: 5259
		private bool initSuccessful;

		// Token: 0x0400148C RID: 5260
		private NetworkingPerfCounters.CounterPair[] counters;

		// Token: 0x0400148D RID: 5261
		private bool enabled;

		// Token: 0x0400148E RID: 5262
		private volatile bool cleanupCalled;

		// Token: 0x02000751 RID: 1873
		private class CounterPair
		{
			// Token: 0x17000F0C RID: 3852
			// (get) Token: 0x060041D8 RID: 16856 RVA: 0x001116AC File Offset: 0x0010F8AC
			public PerformanceCounter InstanceCounter
			{
				get
				{
					return this.instanceCounter;
				}
			}

			// Token: 0x17000F0D RID: 3853
			// (get) Token: 0x060041D9 RID: 16857 RVA: 0x001116B4 File Offset: 0x0010F8B4
			public PerformanceCounter GlobalCounter
			{
				get
				{
					return this.globalCounter;
				}
			}

			// Token: 0x060041DA RID: 16858 RVA: 0x001116BC File Offset: 0x0010F8BC
			public CounterPair(PerformanceCounter instanceCounter, PerformanceCounter globalCounter)
			{
				this.instanceCounter = instanceCounter;
				this.globalCounter = globalCounter;
			}

			// Token: 0x040031EA RID: 12778
			private PerformanceCounter instanceCounter;

			// Token: 0x040031EB RID: 12779
			private PerformanceCounter globalCounter;
		}
	}
}
