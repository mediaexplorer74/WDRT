using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x02000163 RID: 355
	internal sealed class ComponentManagerBroker : MarshalByRefObject
	{
		// Token: 0x06000EC4 RID: 3780 RVA: 0x0002C47C File Offset: 0x0002A67C
		static ComponentManagerBroker()
		{
			int currentProcessId = SafeNativeMethods.GetCurrentProcessId();
			ComponentManagerBroker._syncObject = new object();
			ComponentManagerBroker._remoteObjectName = string.Format(CultureInfo.CurrentCulture, "ComponentManagerBroker.{0}.{1:X}", new object[]
			{
				Application.WindowsFormsVersion,
				currentProcessId
			});
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x0002C4C4 File Offset: 0x0002A6C4
		public ComponentManagerBroker()
		{
			if (ComponentManagerBroker._broker == null)
			{
				ComponentManagerBroker._broker = this;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000EC6 RID: 3782 RVA: 0x0002C4D9 File Offset: 0x0002A6D9
		internal ComponentManagerBroker Singleton
		{
			get
			{
				return ComponentManagerBroker._broker;
			}
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x0002C4E0 File Offset: 0x0002A6E0
		internal void ClearComponentManager()
		{
			this._proxy = null;
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x00015C90 File Offset: 0x00013E90
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x0002C4EC File Offset: 0x0002A6EC
		public UnsafeNativeMethods.IMsoComponentManager GetProxy(long pCM)
		{
			if (this._proxy == null)
			{
				UnsafeNativeMethods.IMsoComponentManager msoComponentManager = (UnsafeNativeMethods.IMsoComponentManager)Marshal.GetObjectForIUnknown((IntPtr)pCM);
				this._proxy = new ComponentManagerProxy(this, msoComponentManager);
			}
			return this._proxy;
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x0002C528 File Offset: 0x0002A728
		internal static UnsafeNativeMethods.IMsoComponentManager GetComponentManager(IntPtr pOriginal)
		{
			object syncObject = ComponentManagerBroker._syncObject;
			lock (syncObject)
			{
				if (ComponentManagerBroker._broker == null)
				{
					UnsafeNativeMethods.ICorRuntimeHost corRuntimeHost = (UnsafeNativeMethods.ICorRuntimeHost)RuntimeEnvironment.GetRuntimeInterfaceAsObject(typeof(UnsafeNativeMethods.CorRuntimeHost).GUID, typeof(UnsafeNativeMethods.ICorRuntimeHost).GUID);
					object obj;
					int defaultDomain = corRuntimeHost.GetDefaultDomain(out obj);
					AppDomain appDomain = obj as AppDomain;
					if (appDomain == null)
					{
						appDomain = AppDomain.CurrentDomain;
					}
					if (appDomain == AppDomain.CurrentDomain)
					{
						ComponentManagerBroker._broker = new ComponentManagerBroker();
					}
					else
					{
						ComponentManagerBroker._broker = ComponentManagerBroker.GetRemotedComponentManagerBroker(appDomain);
					}
				}
			}
			return ComponentManagerBroker._broker.GetProxy((long)pOriginal);
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x0002C5E0 File Offset: 0x0002A7E0
		private static ComponentManagerBroker GetRemotedComponentManagerBroker(AppDomain domain)
		{
			Type typeFromHandle = typeof(ComponentManagerBroker);
			ComponentManagerBroker componentManagerBroker = (ComponentManagerBroker)domain.CreateInstanceAndUnwrap(typeFromHandle.Assembly.FullName, typeFromHandle.FullName);
			return componentManagerBroker.Singleton;
		}

		// Token: 0x040007F1 RID: 2033
		private static object _syncObject;

		// Token: 0x040007F2 RID: 2034
		private static string _remoteObjectName;

		// Token: 0x040007F3 RID: 2035
		private static ComponentManagerBroker _broker;

		// Token: 0x040007F4 RID: 2036
		[ThreadStatic]
		private ComponentManagerProxy _proxy;
	}
}
