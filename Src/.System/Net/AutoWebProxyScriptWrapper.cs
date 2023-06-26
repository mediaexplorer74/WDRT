using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Remoting;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000194 RID: 404
	internal class AutoWebProxyScriptWrapper
	{
		// Token: 0x06000F97 RID: 3991 RVA: 0x0005182C File Offset: 0x0004FA2C
		static AutoWebProxyScriptWrapper()
		{
			AppDomain.CurrentDomain.DomainUnload += AutoWebProxyScriptWrapper.OnDomainUnload;
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x00051858 File Offset: 0x0004FA58
		[ReflectionPermission(SecurityAction.Assert, Flags = ReflectionPermissionFlag.MemberAccess)]
		[ReflectionPermission(SecurityAction.Assert, Flags = ReflectionPermissionFlag.TypeInformation)]
		internal AutoWebProxyScriptWrapper()
		{
			Exception ex = null;
			if (AutoWebProxyScriptWrapper.s_ProxyScriptHelperLoadError == null && AutoWebProxyScriptWrapper.s_ProxyScriptHelperType == null)
			{
				object obj = AutoWebProxyScriptWrapper.s_ProxyScriptHelperLock;
				lock (obj)
				{
					if (AutoWebProxyScriptWrapper.s_ProxyScriptHelperLoadError == null && AutoWebProxyScriptWrapper.s_ProxyScriptHelperType == null)
					{
						try
						{
							AutoWebProxyScriptWrapper.s_ProxyScriptHelperType = Type.GetType("System.Net.VsaWebProxyScript, Microsoft.JScript, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", true);
						}
						catch (Exception ex2)
						{
							ex = ex2;
						}
						if (AutoWebProxyScriptWrapper.s_ProxyScriptHelperType == null)
						{
							AutoWebProxyScriptWrapper.s_ProxyScriptHelperLoadError = ((ex == null) ? new InternalException() : ex);
						}
					}
				}
			}
			if (AutoWebProxyScriptWrapper.s_ProxyScriptHelperLoadError != null)
			{
				throw new TypeLoadException(SR.GetString("net_cannot_load_proxy_helper"), (AutoWebProxyScriptWrapper.s_ProxyScriptHelperLoadError is InternalException) ? null : AutoWebProxyScriptWrapper.s_ProxyScriptHelperLoadError);
			}
			this.CreateAppDomain();
			ex = null;
			try
			{
				ObjectHandle objectHandle = Activator.CreateInstance(this.scriptDomain, AutoWebProxyScriptWrapper.s_ProxyScriptHelperType.Assembly.FullName, AutoWebProxyScriptWrapper.s_ProxyScriptHelperType.FullName, false, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod | BindingFlags.CreateInstance, null, null, null, null, null);
				if (objectHandle != null)
				{
					this.site = (IWebProxyScript)objectHandle.Unwrap();
				}
			}
			catch (Exception ex3)
			{
				ex = ex3;
			}
			if (this.site == null)
			{
				object obj2 = AutoWebProxyScriptWrapper.s_ProxyScriptHelperLock;
				lock (obj2)
				{
					if (AutoWebProxyScriptWrapper.s_ProxyScriptHelperLoadError == null)
					{
						AutoWebProxyScriptWrapper.s_ProxyScriptHelperLoadError = ((ex == null) ? new InternalException() : ex);
					}
				}
				throw new TypeLoadException(SR.GetString("net_cannot_load_proxy_helper"), (AutoWebProxyScriptWrapper.s_ProxyScriptHelperLoadError is InternalException) ? null : AutoWebProxyScriptWrapper.s_ProxyScriptHelperLoadError);
			}
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x00051A20 File Offset: 0x0004FC20
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.ControlAppDomain)]
		private void CreateAppDomain()
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Monitor.Enter(AutoWebProxyScriptWrapper.s_AppDomains.SyncRoot, ref flag);
				if (AutoWebProxyScriptWrapper.s_CleanedUp)
				{
					throw new InvalidOperationException(SR.GetString("net_cant_perform_during_shutdown"));
				}
				if (AutoWebProxyScriptWrapper.s_AppDomainInfo == null)
				{
					AutoWebProxyScriptWrapper.s_AppDomainInfo = new AppDomainSetup();
					AutoWebProxyScriptWrapper.s_AppDomainInfo.DisallowBindingRedirects = true;
					AutoWebProxyScriptWrapper.s_AppDomainInfo.DisallowCodeDownload = true;
					NamedPermissionSet namedPermissionSet = new NamedPermissionSet("__WebProxySandbox", PermissionState.None);
					namedPermissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
					ApplicationTrust applicationTrust = new ApplicationTrust();
					applicationTrust.DefaultGrantSet = new PolicyStatement(namedPermissionSet);
					AutoWebProxyScriptWrapper.s_AppDomainInfo.ApplicationTrust = applicationTrust;
					AutoWebProxyScriptWrapper.s_AppDomainInfo.ApplicationBase = Environment.SystemDirectory;
				}
				AppDomain appDomain = AutoWebProxyScriptWrapper.s_ExcessAppDomain;
				if (appDomain != null)
				{
					TimerThread.GetOrCreateQueue(0).CreateTimer(new TimerThread.Callback(AutoWebProxyScriptWrapper.CloseAppDomainCallback), appDomain);
					throw new InvalidOperationException(SR.GetString("net_cant_create_environment"));
				}
				this.appDomainIndex = AutoWebProxyScriptWrapper.s_NextAppDomainIndex++;
				try
				{
				}
				finally
				{
					PermissionSet permissionSet = new PermissionSet(PermissionState.None);
					permissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
					AutoWebProxyScriptWrapper.s_ExcessAppDomain = AppDomain.CreateDomain("WebProxyScript", null, AutoWebProxyScriptWrapper.s_AppDomainInfo, permissionSet, null);
					try
					{
						AutoWebProxyScriptWrapper.s_AppDomains.Add(this.appDomainIndex, AutoWebProxyScriptWrapper.s_ExcessAppDomain);
						this.scriptDomain = AutoWebProxyScriptWrapper.s_ExcessAppDomain;
					}
					finally
					{
						if (this.scriptDomain == AutoWebProxyScriptWrapper.s_ExcessAppDomain)
						{
							AutoWebProxyScriptWrapper.s_ExcessAppDomain = null;
						}
						else
						{
							try
							{
								AutoWebProxyScriptWrapper.s_AppDomains.Remove(this.appDomainIndex);
							}
							finally
							{
								TimerThread.GetOrCreateQueue(0).CreateTimer(new TimerThread.Callback(AutoWebProxyScriptWrapper.CloseAppDomainCallback), AutoWebProxyScriptWrapper.s_ExcessAppDomain);
							}
						}
					}
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(AutoWebProxyScriptWrapper.s_AppDomains.SyncRoot);
				}
			}
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x00051C3C File Offset: 0x0004FE3C
		internal void Close()
		{
			this.site.Close();
			TimerThread.GetOrCreateQueue(0).CreateTimer(new TimerThread.Callback(AutoWebProxyScriptWrapper.CloseAppDomainCallback), this.appDomainIndex);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x00051C74 File Offset: 0x0004FE74
		~AutoWebProxyScriptWrapper()
		{
			if (!NclUtilities.HasShutdownStarted && this.scriptDomain != null)
			{
				TimerThread.GetOrCreateQueue(0).CreateTimer(new TimerThread.Callback(AutoWebProxyScriptWrapper.CloseAppDomainCallback), this.appDomainIndex);
			}
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x00051CCC File Offset: 0x0004FECC
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.ControlAppDomain)]
		private static void CloseAppDomainCallback(TimerThread.Timer timer, int timeNoticed, object context)
		{
			try
			{
				AppDomain appDomain = context as AppDomain;
				if (appDomain == null)
				{
					AutoWebProxyScriptWrapper.CloseAppDomain((int)context);
				}
				else if (appDomain == AutoWebProxyScriptWrapper.s_ExcessAppDomain)
				{
					try
					{
						AppDomain.Unload(appDomain);
					}
					catch (AppDomainUnloadedException)
					{
					}
					AutoWebProxyScriptWrapper.s_ExcessAppDomain = null;
				}
			}
			catch (Exception ex)
			{
				if (NclUtilities.IsFatal(ex))
				{
					throw;
				}
			}
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x00051D38 File Offset: 0x0004FF38
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.ControlAppDomain)]
		private static void CloseAppDomain(int index)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			AppDomain appDomain;
			try
			{
				Monitor.Enter(AutoWebProxyScriptWrapper.s_AppDomains.SyncRoot, ref flag);
				if (AutoWebProxyScriptWrapper.s_CleanedUp)
				{
					return;
				}
				appDomain = (AppDomain)AutoWebProxyScriptWrapper.s_AppDomains[index];
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(AutoWebProxyScriptWrapper.s_AppDomains.SyncRoot);
					flag = false;
				}
			}
			try
			{
				AppDomain.Unload(appDomain);
			}
			catch (AppDomainUnloadedException)
			{
			}
			finally
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					Monitor.Enter(AutoWebProxyScriptWrapper.s_AppDomains.SyncRoot, ref flag);
					AutoWebProxyScriptWrapper.s_AppDomains.Remove(index);
				}
				finally
				{
					if (flag)
					{
						Monitor.Exit(AutoWebProxyScriptWrapper.s_AppDomains.SyncRoot);
					}
				}
			}
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x00051E10 File Offset: 0x00050010
		[ReliabilityContract(Consistency.MayCorruptProcess, Cer.MayFail)]
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.ControlAppDomain)]
		private static void OnDomainUnload(object sender, EventArgs e)
		{
			object syncRoot = AutoWebProxyScriptWrapper.s_AppDomains.SyncRoot;
			lock (syncRoot)
			{
				if (!AutoWebProxyScriptWrapper.s_CleanedUp)
				{
					AutoWebProxyScriptWrapper.s_CleanedUp = true;
					foreach (object obj in AutoWebProxyScriptWrapper.s_AppDomains.Values)
					{
						AppDomain appDomain = (AppDomain)obj;
						try
						{
							AppDomain.Unload(appDomain);
						}
						catch
						{
						}
					}
					AutoWebProxyScriptWrapper.s_AppDomains.Clear();
					AppDomain appDomain2 = AutoWebProxyScriptWrapper.s_ExcessAppDomain;
					if (appDomain2 != null)
					{
						try
						{
							AppDomain.Unload(appDomain2);
						}
						catch
						{
						}
						AutoWebProxyScriptWrapper.s_ExcessAppDomain = null;
					}
				}
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x00051EF0 File Offset: 0x000500F0
		internal string ScriptBody
		{
			get
			{
				return this.scriptText;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000FA0 RID: 4000 RVA: 0x00051EF8 File Offset: 0x000500F8
		// (set) Token: 0x06000FA1 RID: 4001 RVA: 0x00051F00 File Offset: 0x00050100
		internal byte[] Buffer
		{
			get
			{
				return this.scriptBytes;
			}
			set
			{
				this.scriptBytes = value;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000FA2 RID: 4002 RVA: 0x00051F09 File Offset: 0x00050109
		// (set) Token: 0x06000FA3 RID: 4003 RVA: 0x00051F11 File Offset: 0x00050111
		internal DateTime LastModified
		{
			get
			{
				return this.lastModified;
			}
			set
			{
				this.lastModified = value;
			}
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x00051F1A File Offset: 0x0005011A
		internal string FindProxyForURL(string url, string host)
		{
			return this.site.Run(url, host);
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x00051F29 File Offset: 0x00050129
		internal bool Compile(Uri engineScriptLocation, string scriptBody, byte[] buffer)
		{
			if (this.site.Load(engineScriptLocation, scriptBody, typeof(WebProxyScriptHelper)))
			{
				this.scriptText = scriptBody;
				this.scriptBytes = buffer;
				return true;
			}
			return false;
		}

		// Token: 0x040012BC RID: 4796
		private const string c_appDomainName = "WebProxyScript";

		// Token: 0x040012BD RID: 4797
		private int appDomainIndex;

		// Token: 0x040012BE RID: 4798
		private AppDomain scriptDomain;

		// Token: 0x040012BF RID: 4799
		private IWebProxyScript site;

		// Token: 0x040012C0 RID: 4800
		private static volatile AppDomain s_ExcessAppDomain;

		// Token: 0x040012C1 RID: 4801
		private static Hashtable s_AppDomains = new Hashtable();

		// Token: 0x040012C2 RID: 4802
		private static bool s_CleanedUp;

		// Token: 0x040012C3 RID: 4803
		private static int s_NextAppDomainIndex;

		// Token: 0x040012C4 RID: 4804
		private static AppDomainSetup s_AppDomainInfo;

		// Token: 0x040012C5 RID: 4805
		private static volatile Type s_ProxyScriptHelperType;

		// Token: 0x040012C6 RID: 4806
		private static volatile Exception s_ProxyScriptHelperLoadError;

		// Token: 0x040012C7 RID: 4807
		private static object s_ProxyScriptHelperLock = new object();

		// Token: 0x040012C8 RID: 4808
		private string scriptText;

		// Token: 0x040012C9 RID: 4809
		private byte[] scriptBytes;

		// Token: 0x040012CA RID: 4810
		private DateTime lastModified;
	}
}
