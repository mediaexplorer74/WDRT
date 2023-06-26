using System;
using System.Collections.Generic;
using System.Net.Configuration;
using System.Net.NetworkInformation;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000191 RID: 401
	internal class AutoWebProxyScriptEngine
	{
		// Token: 0x06000F60 RID: 3936 RVA: 0x0004FB38 File Offset: 0x0004DD38
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.ControlPrincipal)]
		internal AutoWebProxyScriptEngine(WebProxy proxy, bool useRegistry)
		{
			this.webProxy = proxy;
			this.m_UseRegistry = useRegistry;
			this.m_AutoDetector = AutoWebProxyScriptEngine.AutoDetector.CurrentAutoDetector;
			this.m_NetworkChangeStatus = this.m_AutoDetector.NetworkChangeStatus;
			SafeRegistryHandle.RegOpenCurrentUser(131097U, out this.hkcu);
			if (this.m_UseRegistry)
			{
				this.ListenForRegistry();
				this.m_Identity = WindowsIdentity.GetCurrent();
			}
			this.webProxyFinder = new HybridWebProxyFinder(this);
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x0004FBAC File Offset: 0x0004DDAC
		private void EnterLock(ref int syncStatus)
		{
			if (syncStatus == 0)
			{
				lock (this)
				{
					if (syncStatus != 4)
					{
						syncStatus = 1;
						while (this.m_LockHeld)
						{
							Monitor.Wait(this);
							if (syncStatus == 4)
							{
								Monitor.Pulse(this);
								goto IL_3E;
							}
						}
						syncStatus = 2;
						this.m_LockHeld = true;
					}
					IL_3E:;
				}
			}
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x0004FC14 File Offset: 0x0004DE14
		private void ExitLock(ref int syncStatus)
		{
			if (syncStatus != 0 && syncStatus != 4)
			{
				lock (this)
				{
					this.m_LockHeld = false;
					if (syncStatus == 3)
					{
						this.webProxyFinder.Reset();
						syncStatus = 4;
					}
					else
					{
						syncStatus = 0;
					}
					Monitor.Pulse(this);
				}
			}
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x0004FC78 File Offset: 0x0004DE78
		internal void Abort(ref int syncStatus)
		{
			lock (this)
			{
				switch (syncStatus)
				{
				case 0:
					syncStatus = 4;
					break;
				case 1:
					syncStatus = 4;
					Monitor.PulseAll(this);
					break;
				case 2:
					syncStatus = 3;
					this.webProxyFinder.Abort();
					break;
				}
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000F64 RID: 3940 RVA: 0x0004FCE4 File Offset: 0x0004DEE4
		// (set) Token: 0x06000F65 RID: 3941 RVA: 0x0004FCEC File Offset: 0x0004DEEC
		internal bool AutomaticallyDetectSettings
		{
			get
			{
				return this.automaticallyDetectSettings;
			}
			set
			{
				if (this.automaticallyDetectSettings != value)
				{
					this.automaticallyDetectSettings = value;
					this.webProxyFinder.Reset();
				}
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000F66 RID: 3942 RVA: 0x0004FD09 File Offset: 0x0004DF09
		// (set) Token: 0x06000F67 RID: 3943 RVA: 0x0004FD11 File Offset: 0x0004DF11
		internal Uri AutomaticConfigurationScript
		{
			get
			{
				return this.automaticConfigurationScript;
			}
			set
			{
				if (!object.Equals(this.automaticConfigurationScript, value))
				{
					this.automaticConfigurationScript = value;
					this.webProxyFinder.Reset();
				}
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000F68 RID: 3944 RVA: 0x0004FD33 File Offset: 0x0004DF33
		internal ICredentials Credentials
		{
			get
			{
				return this.webProxy.Credentials;
			}
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x0004FD40 File Offset: 0x0004DF40
		internal bool GetProxies(Uri destination, out IList<string> proxyList)
		{
			int num = 0;
			return this.GetProxies(destination, out proxyList, ref num);
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x0004FD5C File Offset: 0x0004DF5C
		internal bool GetProxies(Uri destination, out IList<string> proxyList, ref int syncStatus)
		{
			proxyList = null;
			this.CheckForChanges(ref syncStatus);
			if (!this.webProxyFinder.IsValid)
			{
				if (this.retryWinHttpGetProxyForUrlTimer == null && SettingsSectionInternal.Section.AutoConfigUrlRetryInterval != 0)
				{
					long num = (long)(SettingsSectionInternal.Section.AutoConfigUrlRetryInterval * 1000);
					this.retryWinHttpGetProxyForUrlTimer = new Timer(delegate(object s)
					{
						WeakReference<AutoWebProxyScriptEngine> weakReference = (WeakReference<AutoWebProxyScriptEngine>)s;
						AutoWebProxyScriptEngine autoWebProxyScriptEngine;
						if (weakReference.TryGetTarget(out autoWebProxyScriptEngine))
						{
							autoWebProxyScriptEngine.executeWinHttpGetProxyForUrl = true;
						}
					}, new WeakReference<AutoWebProxyScriptEngine>(this), num, num);
				}
				if (!this.executeWinHttpGetProxyForUrl)
				{
					return false;
				}
				this.executeWinHttpGetProxyForUrl = false;
			}
			else if (this.retryWinHttpGetProxyForUrlTimer != null)
			{
				this.retryWinHttpGetProxyForUrlTimer.Dispose();
				this.retryWinHttpGetProxyForUrlTimer = null;
			}
			bool flag;
			try
			{
				this.EnterLock(ref syncStatus);
				if (syncStatus != 2)
				{
					flag = false;
				}
				else
				{
					flag = this.webProxyFinder.GetProxies(destination, out proxyList);
				}
			}
			finally
			{
				this.ExitLock(ref syncStatus);
			}
			return flag;
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x0004FE40 File Offset: 0x0004E040
		internal WebProxyData GetWebProxyData()
		{
			WebProxyDataBuilder webProxyDataBuilder;
			if (ComNetOS.IsWin7orLater)
			{
				webProxyDataBuilder = new WinHttpWebProxyBuilder();
			}
			else
			{
				webProxyDataBuilder = new RegBlobWebProxyDataBuilder(this.m_AutoDetector.Connectoid, this.hkcu);
			}
			return webProxyDataBuilder.Build();
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x0004FE7C File Offset: 0x0004E07C
		internal void Close()
		{
			if (this.m_AutoDetector != null)
			{
				int num = 0;
				try
				{
					this.EnterLock(ref num);
					if (this.m_AutoDetector != null)
					{
						this.registrySuppress = true;
						if (this.registryChangeEventPolicy != null)
						{
							this.registryChangeEventPolicy.Close();
							this.registryChangeEventPolicy = null;
						}
						if (this.registryChangeEventLM != null)
						{
							this.registryChangeEventLM.Close();
							this.registryChangeEventLM = null;
						}
						if (this.registryChangeEvent != null)
						{
							this.registryChangeEvent.Close();
							this.registryChangeEvent = null;
						}
						if (this.regKeyPolicy != null && !this.regKeyPolicy.IsInvalid)
						{
							this.regKeyPolicy.Close();
						}
						if (this.regKeyLM != null && !this.regKeyLM.IsInvalid)
						{
							this.regKeyLM.Close();
						}
						if (this.regKey != null && !this.regKey.IsInvalid)
						{
							this.regKey.Close();
						}
						if (this.hkcu != null)
						{
							this.hkcu.RegCloseKey();
							this.hkcu = null;
						}
						if (this.m_Identity != null)
						{
							this.m_Identity.Dispose();
							this.m_Identity = null;
						}
						this.webProxyFinder.Dispose();
						this.m_AutoDetector = null;
					}
				}
				finally
				{
					this.ExitLock(ref num);
				}
			}
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x0004FFCC File Offset: 0x0004E1CC
		internal void ListenForRegistry()
		{
			if (!this.registrySuppress)
			{
				if (this.registryChangeEvent == null)
				{
					this.ListenForRegistryHelper(ref this.regKey, ref this.registryChangeEvent, IntPtr.Zero, "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Connections");
				}
				if (this.registryChangeEventLM == null)
				{
					this.ListenForRegistryHelper(ref this.regKeyLM, ref this.registryChangeEventLM, UnsafeNclNativeMethods.RegistryHelper.HKEY_LOCAL_MACHINE, "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Connections");
				}
				if (this.registryChangeEventPolicy == null)
				{
					this.ListenForRegistryHelper(ref this.regKeyPolicy, ref this.registryChangeEventPolicy, UnsafeNclNativeMethods.RegistryHelper.HKEY_LOCAL_MACHINE, "SOFTWARE\\Policies\\Microsoft\\Windows\\CurrentVersion\\Internet Settings");
				}
				if (this.registryChangeEvent == null && this.registryChangeEventLM == null && this.registryChangeEventPolicy == null)
				{
					this.registrySuppress = true;
				}
			}
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x00050070 File Offset: 0x0004E270
		private void ListenForRegistryHelper(ref SafeRegistryHandle key, ref AutoResetEvent changeEvent, IntPtr baseKey, string subKey)
		{
			uint num = 0U;
			if (key == null || key.IsInvalid)
			{
				if (baseKey == IntPtr.Zero)
				{
					if (this.hkcu != null)
					{
						num = this.hkcu.RegOpenKeyEx(subKey, 0U, 131097U, out key);
					}
					else
					{
						num = 1168U;
					}
				}
				else
				{
					num = SafeRegistryHandle.RegOpenKeyEx(baseKey, subKey, 0U, 131097U, out key);
				}
				if (num == 0U)
				{
					changeEvent = new AutoResetEvent(false);
				}
			}
			if (num == 0U)
			{
				num = key.RegNotifyChangeKeyValue(true, 4U, changeEvent.SafeWaitHandle, true);
			}
			if (num != 0U)
			{
				if (key != null && !key.IsInvalid)
				{
					try
					{
						num = key.RegCloseKey();
					}
					catch (Exception ex)
					{
						if (NclUtilities.IsFatal(ex))
						{
							throw;
						}
					}
				}
				key = null;
				if (changeEvent != null)
				{
					changeEvent.Close();
					changeEvent = null;
				}
			}
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x00050138 File Offset: 0x0004E338
		private void RegistryChanged()
		{
			if (Logging.On)
			{
				Logging.PrintWarning(Logging.Web, SR.GetString("net_log_proxy_system_setting_update"));
			}
			WebProxyData webProxyData;
			using (this.m_Identity.Impersonate())
			{
				webProxyData = this.GetWebProxyData();
			}
			this.webProxy.Update(webProxyData);
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x0005019C File Offset: 0x0004E39C
		private void ConnectoidChanged()
		{
			if (Logging.On)
			{
				Logging.PrintWarning(Logging.Web, SR.GetString("net_log_proxy_update_due_to_ip_config_change"));
			}
			this.m_AutoDetector = AutoWebProxyScriptEngine.AutoDetector.CurrentAutoDetector;
			if (this.m_UseRegistry)
			{
				WebProxyData webProxyData;
				using (this.m_Identity.Impersonate())
				{
					webProxyData = this.GetWebProxyData();
				}
				this.webProxy.Update(webProxyData);
			}
			if (this.automaticallyDetectSettings)
			{
				this.webProxyFinder.Reset();
			}
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x00050228 File Offset: 0x0004E428
		internal void CheckForChanges()
		{
			int num = 0;
			this.CheckForChanges(ref num);
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x00050240 File Offset: 0x0004E440
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.ControlPrincipal)]
		private void CheckForChanges(ref int syncStatus)
		{
			try
			{
				bool flag = AutoWebProxyScriptEngine.AutoDetector.CheckForNetworkChanges(ref this.m_NetworkChangeStatus);
				bool flag2 = false;
				if (flag || this.needConnectoidUpdate)
				{
					try
					{
						this.EnterLock(ref syncStatus);
						if (flag || this.needConnectoidUpdate)
						{
							this.needConnectoidUpdate = syncStatus != 2;
							if (!this.needConnectoidUpdate)
							{
								this.ConnectoidChanged();
								flag2 = true;
							}
						}
					}
					finally
					{
						this.ExitLock(ref syncStatus);
					}
				}
				if (this.m_UseRegistry)
				{
					bool flag3 = false;
					AutoResetEvent autoResetEvent = this.registryChangeEvent;
					if (this.registryChangeDeferred || (flag3 = autoResetEvent != null && autoResetEvent.WaitOne(0, false)))
					{
						try
						{
							this.EnterLock(ref syncStatus);
							if (flag3 || this.registryChangeDeferred)
							{
								this.registryChangeDeferred = syncStatus != 2;
								if (!this.registryChangeDeferred && this.registryChangeEvent != null)
								{
									try
									{
										using (this.m_Identity.Impersonate())
										{
											this.ListenForRegistryHelper(ref this.regKey, ref this.registryChangeEvent, IntPtr.Zero, "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Connections");
										}
									}
									catch
									{
										throw;
									}
									this.needRegistryUpdate = true;
								}
							}
						}
						finally
						{
							this.ExitLock(ref syncStatus);
						}
					}
					flag3 = false;
					autoResetEvent = this.registryChangeEventLM;
					if (this.registryChangeLMDeferred || (flag3 = autoResetEvent != null && autoResetEvent.WaitOne(0, false)))
					{
						try
						{
							this.EnterLock(ref syncStatus);
							if (flag3 || this.registryChangeLMDeferred)
							{
								this.registryChangeLMDeferred = syncStatus != 2;
								if (!this.registryChangeLMDeferred && this.registryChangeEventLM != null)
								{
									try
									{
										using (this.m_Identity.Impersonate())
										{
											this.ListenForRegistryHelper(ref this.regKeyLM, ref this.registryChangeEventLM, UnsafeNclNativeMethods.RegistryHelper.HKEY_LOCAL_MACHINE, "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Connections");
										}
									}
									catch
									{
										throw;
									}
									this.needRegistryUpdate = true;
								}
							}
						}
						finally
						{
							this.ExitLock(ref syncStatus);
						}
					}
					flag3 = false;
					autoResetEvent = this.registryChangeEventPolicy;
					if (this.registryChangePolicyDeferred || (flag3 = autoResetEvent != null && autoResetEvent.WaitOne(0, false)))
					{
						try
						{
							this.EnterLock(ref syncStatus);
							if (flag3 || this.registryChangePolicyDeferred)
							{
								this.registryChangePolicyDeferred = syncStatus != 2;
								if (!this.registryChangePolicyDeferred && this.registryChangeEventPolicy != null)
								{
									try
									{
										using (this.m_Identity.Impersonate())
										{
											this.ListenForRegistryHelper(ref this.regKeyPolicy, ref this.registryChangeEventPolicy, UnsafeNclNativeMethods.RegistryHelper.HKEY_LOCAL_MACHINE, "SOFTWARE\\Policies\\Microsoft\\Windows\\CurrentVersion\\Internet Settings");
										}
									}
									catch
									{
										throw;
									}
									this.needRegistryUpdate = true;
								}
							}
						}
						finally
						{
							this.ExitLock(ref syncStatus);
						}
					}
					if (this.needRegistryUpdate)
					{
						try
						{
							this.EnterLock(ref syncStatus);
							if (this.needRegistryUpdate && syncStatus == 2)
							{
								this.needRegistryUpdate = false;
								if (!flag2)
								{
									this.RegistryChanged();
								}
							}
						}
						finally
						{
							this.ExitLock(ref syncStatus);
						}
					}
				}
			}
			catch (ObjectDisposedException)
			{
			}
		}

		// Token: 0x040012A3 RID: 4771
		private bool automaticallyDetectSettings;

		// Token: 0x040012A4 RID: 4772
		private Uri automaticConfigurationScript;

		// Token: 0x040012A5 RID: 4773
		private WebProxy webProxy;

		// Token: 0x040012A6 RID: 4774
		private IWebProxyFinder webProxyFinder;

		// Token: 0x040012A7 RID: 4775
		private bool executeWinHttpGetProxyForUrl;

		// Token: 0x040012A8 RID: 4776
		private Timer retryWinHttpGetProxyForUrlTimer;

		// Token: 0x040012A9 RID: 4777
		private bool m_LockHeld;

		// Token: 0x040012AA RID: 4778
		private bool m_UseRegistry;

		// Token: 0x040012AB RID: 4779
		private int m_NetworkChangeStatus;

		// Token: 0x040012AC RID: 4780
		private AutoWebProxyScriptEngine.AutoDetector m_AutoDetector;

		// Token: 0x040012AD RID: 4781
		private SafeRegistryHandle hkcu;

		// Token: 0x040012AE RID: 4782
		private WindowsIdentity m_Identity;

		// Token: 0x040012AF RID: 4783
		private SafeRegistryHandle regKey;

		// Token: 0x040012B0 RID: 4784
		private SafeRegistryHandle regKeyLM;

		// Token: 0x040012B1 RID: 4785
		private SafeRegistryHandle regKeyPolicy;

		// Token: 0x040012B2 RID: 4786
		private AutoResetEvent registryChangeEvent;

		// Token: 0x040012B3 RID: 4787
		private AutoResetEvent registryChangeEventLM;

		// Token: 0x040012B4 RID: 4788
		private AutoResetEvent registryChangeEventPolicy;

		// Token: 0x040012B5 RID: 4789
		private bool registryChangeDeferred;

		// Token: 0x040012B6 RID: 4790
		private bool registryChangeLMDeferred;

		// Token: 0x040012B7 RID: 4791
		private bool registryChangePolicyDeferred;

		// Token: 0x040012B8 RID: 4792
		private bool needRegistryUpdate;

		// Token: 0x040012B9 RID: 4793
		private bool needConnectoidUpdate;

		// Token: 0x040012BA RID: 4794
		private bool registrySuppress;

		// Token: 0x0200073E RID: 1854
		private static class SyncStatus
		{
			// Token: 0x040031A2 RID: 12706
			internal const int Unlocked = 0;

			// Token: 0x040031A3 RID: 12707
			internal const int Locking = 1;

			// Token: 0x040031A4 RID: 12708
			internal const int LockOwner = 2;

			// Token: 0x040031A5 RID: 12709
			internal const int AbortedLocked = 3;

			// Token: 0x040031A6 RID: 12710
			internal const int Aborted = 4;
		}

		// Token: 0x0200073F RID: 1855
		private class AutoDetector
		{
			// Token: 0x060041AA RID: 16810 RVA: 0x00111034 File Offset: 0x0010F234
			private static void Initialize()
			{
				if (!AutoWebProxyScriptEngine.AutoDetector.s_Initialized)
				{
					object obj = AutoWebProxyScriptEngine.AutoDetector.s_LockObject;
					lock (obj)
					{
						if (!AutoWebProxyScriptEngine.AutoDetector.s_Initialized)
						{
							AutoWebProxyScriptEngine.AutoDetector.s_CurrentAutoDetector = new AutoWebProxyScriptEngine.AutoDetector(UnsafeNclNativeMethods.RasHelper.GetCurrentConnectoid(), 1);
							if (NetworkChange.CanListenForNetworkChanges)
							{
								AutoWebProxyScriptEngine.AutoDetector.s_AddressChange = new NetworkAddressChangePolled();
							}
							if (UnsafeNclNativeMethods.RasHelper.RasSupported)
							{
								AutoWebProxyScriptEngine.AutoDetector.s_RasHelper = new UnsafeNclNativeMethods.RasHelper();
							}
							AutoWebProxyScriptEngine.AutoDetector.s_CurrentVersion = 1;
							AutoWebProxyScriptEngine.AutoDetector.s_Initialized = true;
						}
					}
				}
			}

			// Token: 0x060041AB RID: 16811 RVA: 0x001110C8 File Offset: 0x0010F2C8
			internal static bool CheckForNetworkChanges(ref int changeStatus)
			{
				AutoWebProxyScriptEngine.AutoDetector.Initialize();
				AutoWebProxyScriptEngine.AutoDetector.CheckForChanges();
				int num = changeStatus;
				changeStatus = Volatile.Read(ref AutoWebProxyScriptEngine.AutoDetector.s_CurrentVersion);
				return num != changeStatus;
			}

			// Token: 0x060041AC RID: 16812 RVA: 0x001110F8 File Offset: 0x0010F2F8
			private static void CheckForChanges()
			{
				bool flag = false;
				if (AutoWebProxyScriptEngine.AutoDetector.s_RasHelper != null && AutoWebProxyScriptEngine.AutoDetector.s_RasHelper.HasChanged)
				{
					AutoWebProxyScriptEngine.AutoDetector.s_RasHelper.Reset();
					flag = true;
				}
				if (AutoWebProxyScriptEngine.AutoDetector.s_AddressChange != null && AutoWebProxyScriptEngine.AutoDetector.s_AddressChange.CheckAndReset())
				{
					flag = true;
				}
				if (flag)
				{
					int num = Interlocked.Increment(ref AutoWebProxyScriptEngine.AutoDetector.s_CurrentVersion);
					AutoWebProxyScriptEngine.AutoDetector.s_CurrentAutoDetector = new AutoWebProxyScriptEngine.AutoDetector(UnsafeNclNativeMethods.RasHelper.GetCurrentConnectoid(), num);
				}
			}

			// Token: 0x17000EFF RID: 3839
			// (get) Token: 0x060041AD RID: 16813 RVA: 0x00111165 File Offset: 0x0010F365
			internal static AutoWebProxyScriptEngine.AutoDetector CurrentAutoDetector
			{
				get
				{
					AutoWebProxyScriptEngine.AutoDetector.Initialize();
					return AutoWebProxyScriptEngine.AutoDetector.s_CurrentAutoDetector;
				}
			}

			// Token: 0x060041AE RID: 16814 RVA: 0x00111173 File Offset: 0x0010F373
			private AutoDetector(string connectoid, int currentVersion)
			{
				this.m_Connectoid = connectoid;
				this.m_CurrentVersion = currentVersion;
			}

			// Token: 0x17000F00 RID: 3840
			// (get) Token: 0x060041AF RID: 16815 RVA: 0x00111189 File Offset: 0x0010F389
			internal string Connectoid
			{
				get
				{
					return this.m_Connectoid;
				}
			}

			// Token: 0x17000F01 RID: 3841
			// (get) Token: 0x060041B0 RID: 16816 RVA: 0x00111191 File Offset: 0x0010F391
			internal int NetworkChangeStatus
			{
				get
				{
					return this.m_CurrentVersion;
				}
			}

			// Token: 0x040031A7 RID: 12711
			private static volatile NetworkAddressChangePolled s_AddressChange;

			// Token: 0x040031A8 RID: 12712
			private static volatile UnsafeNclNativeMethods.RasHelper s_RasHelper;

			// Token: 0x040031A9 RID: 12713
			private static int s_CurrentVersion;

			// Token: 0x040031AA RID: 12714
			private static volatile AutoWebProxyScriptEngine.AutoDetector s_CurrentAutoDetector;

			// Token: 0x040031AB RID: 12715
			private static volatile bool s_Initialized;

			// Token: 0x040031AC RID: 12716
			private static object s_LockObject = new object();

			// Token: 0x040031AD RID: 12717
			private readonly string m_Connectoid;

			// Token: 0x040031AE RID: 12718
			private readonly int m_CurrentVersion;
		}
	}
}
