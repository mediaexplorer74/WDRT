using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x02000164 RID: 356
	internal class ComponentManagerProxy : MarshalByRefObject, UnsafeNativeMethods.IMsoComponentManager, UnsafeNativeMethods.IMsoComponent
	{
		// Token: 0x06000ECC RID: 3788 RVA: 0x0002C61B File Offset: 0x0002A81B
		internal ComponentManagerProxy(ComponentManagerBroker broker, UnsafeNativeMethods.IMsoComponentManager original)
		{
			this._broker = broker;
			this._original = original;
			this._creationThread = SafeNativeMethods.GetCurrentThreadId();
			this._refCount = 0;
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x0002C644 File Offset: 0x0002A844
		private void Dispose()
		{
			if (this._original != null)
			{
				Marshal.ReleaseComObject(this._original);
				this._original = null;
				this._components = null;
				this._componentId = (IntPtr)0;
				this._refCount = 0;
				this._broker.ClearComponentManager();
			}
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x00015C90 File Offset: 0x00013E90
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x0002C691 File Offset: 0x0002A891
		private bool RevokeComponent()
		{
			return this._original.FRevokeComponent(this._componentId);
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x0002C6A4 File Offset: 0x0002A8A4
		private UnsafeNativeMethods.IMsoComponent Component
		{
			get
			{
				if (this._trackingComponent != null)
				{
					return this._trackingComponent;
				}
				if (this._activeComponent != null)
				{
					return this._activeComponent;
				}
				return null;
			}
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x0002C6C8 File Offset: 0x0002A8C8
		bool UnsafeNativeMethods.IMsoComponent.FDebugMessage(IntPtr hInst, int msg, IntPtr wparam, IntPtr lparam)
		{
			UnsafeNativeMethods.IMsoComponent component = this.Component;
			return component != null && component.FDebugMessage(hInst, msg, wparam, lparam);
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x0002C6EC File Offset: 0x0002A8EC
		bool UnsafeNativeMethods.IMsoComponent.FPreTranslateMessage(ref NativeMethods.MSG msg)
		{
			UnsafeNativeMethods.IMsoComponent component = this.Component;
			return component != null && component.FPreTranslateMessage(ref msg);
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x0002C70C File Offset: 0x0002A90C
		void UnsafeNativeMethods.IMsoComponent.OnEnterState(int uStateID, bool fEnter)
		{
			if (this._components != null)
			{
				foreach (UnsafeNativeMethods.IMsoComponent msoComponent in this._components.Values)
				{
					msoComponent.OnEnterState(uStateID, fEnter);
				}
			}
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x0002C770 File Offset: 0x0002A970
		void UnsafeNativeMethods.IMsoComponent.OnAppActivate(bool fActive, int dwOtherThreadID)
		{
			if (this._components != null)
			{
				foreach (UnsafeNativeMethods.IMsoComponent msoComponent in this._components.Values)
				{
					msoComponent.OnAppActivate(fActive, dwOtherThreadID);
				}
			}
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x0002C7D4 File Offset: 0x0002A9D4
		void UnsafeNativeMethods.IMsoComponent.OnLoseActivation()
		{
			if (this._activeComponent != null)
			{
				this._activeComponent.OnLoseActivation();
			}
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x0002C7EC File Offset: 0x0002A9EC
		void UnsafeNativeMethods.IMsoComponent.OnActivationChange(UnsafeNativeMethods.IMsoComponent component, bool fSameComponent, int pcrinfo, bool fHostIsActivating, int pchostinfo, int dwReserved)
		{
			if (this._components != null)
			{
				foreach (UnsafeNativeMethods.IMsoComponent msoComponent in this._components.Values)
				{
					msoComponent.OnActivationChange(component, fSameComponent, pcrinfo, fHostIsActivating, pchostinfo, dwReserved);
				}
			}
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x0002C854 File Offset: 0x0002AA54
		bool UnsafeNativeMethods.IMsoComponent.FDoIdle(int grfidlef)
		{
			bool flag = false;
			if (this._components != null)
			{
				foreach (UnsafeNativeMethods.IMsoComponent msoComponent in this._components.Values)
				{
					flag |= msoComponent.FDoIdle(grfidlef);
				}
			}
			return flag;
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x0002C8BC File Offset: 0x0002AABC
		bool UnsafeNativeMethods.IMsoComponent.FContinueMessageLoop(int reason, int pvLoopData, NativeMethods.MSG[] msgPeeked)
		{
			bool flag = false;
			if (this._refCount == 0 && this._componentId != (IntPtr)0 && this.RevokeComponent())
			{
				this._components.Clear();
				this._componentId = (IntPtr)0;
			}
			if (this._components != null)
			{
				foreach (UnsafeNativeMethods.IMsoComponent msoComponent in this._components.Values)
				{
					flag |= msoComponent.FContinueMessageLoop(reason, pvLoopData, msgPeeked);
				}
			}
			return flag;
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x00012E4E File Offset: 0x0001104E
		bool UnsafeNativeMethods.IMsoComponent.FQueryTerminate(bool fPromptUser)
		{
			return true;
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x0002C960 File Offset: 0x0002AB60
		void UnsafeNativeMethods.IMsoComponent.Terminate()
		{
			if (this._components != null && this._components.Values.Count > 0)
			{
				UnsafeNativeMethods.IMsoComponent[] array = new UnsafeNativeMethods.IMsoComponent[this._components.Values.Count];
				this._components.Values.CopyTo(array, 0);
				foreach (UnsafeNativeMethods.IMsoComponent msoComponent in array)
				{
					msoComponent.Terminate();
				}
			}
			if (this._original != null)
			{
				this.RevokeComponent();
			}
			this.Dispose();
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x0002C9E0 File Offset: 0x0002ABE0
		IntPtr UnsafeNativeMethods.IMsoComponent.HwndGetWindow(int dwWhich, int dwReserved)
		{
			UnsafeNativeMethods.IMsoComponent component = this.Component;
			if (component != null)
			{
				return component.HwndGetWindow(dwWhich, dwReserved);
			}
			return IntPtr.Zero;
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x0002CA05 File Offset: 0x0002AC05
		int UnsafeNativeMethods.IMsoComponentManager.QueryService(ref Guid guidService, ref Guid iid, out object ppvObj)
		{
			return this._original.QueryService(ref guidService, ref iid, out ppvObj);
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x0002CA15 File Offset: 0x0002AC15
		bool UnsafeNativeMethods.IMsoComponentManager.FDebugMessage(IntPtr hInst, int msg, IntPtr wparam, IntPtr lparam)
		{
			return this._original.FDebugMessage(hInst, msg, wparam, lparam);
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0002CA28 File Offset: 0x0002AC28
		bool UnsafeNativeMethods.IMsoComponentManager.FRegisterComponent(UnsafeNativeMethods.IMsoComponent component, NativeMethods.MSOCRINFOSTRUCT pcrinfo, out IntPtr dwComponentID)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			dwComponentID = (IntPtr)0;
			if (this._refCount == 0 && !this._original.FRegisterComponent(this, pcrinfo, out this._componentId))
			{
				return false;
			}
			this._refCount++;
			if (this._components == null)
			{
				this._components = new Dictionary<int, UnsafeNativeMethods.IMsoComponent>();
			}
			this._nextComponentId++;
			if (this._nextComponentId == 2147483647)
			{
				this._nextComponentId = 1;
			}
			bool flag = false;
			while (this._components.ContainsKey(this._nextComponentId))
			{
				this._nextComponentId++;
				if (this._nextComponentId == 2147483647)
				{
					if (flag)
					{
						throw new InvalidOperationException(SR.GetString("ComponentManagerProxyOutOfMemory"));
					}
					flag = true;
					this._nextComponentId = 1;
				}
			}
			this._components.Add(this._nextComponentId, component);
			dwComponentID = (IntPtr)this._nextComponentId;
			return true;
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x0002CB1C File Offset: 0x0002AD1C
		bool UnsafeNativeMethods.IMsoComponentManager.FRevokeComponent(IntPtr dwComponentID)
		{
			int num = (int)(long)dwComponentID;
			if (this._original == null)
			{
				return false;
			}
			if (this._components == null || num <= 0 || !this._components.ContainsKey(num))
			{
				return false;
			}
			if (this._refCount == 1 && SafeNativeMethods.GetCurrentThreadId() == this._creationThread && !this.RevokeComponent())
			{
				return false;
			}
			this._refCount--;
			this._components.Remove(num);
			if (this._refCount <= 0)
			{
				this.Dispose();
			}
			if (num == this._activeComponentId)
			{
				this._activeComponent = null;
				this._activeComponentId = 0;
			}
			if (num == this._trackingComponentId)
			{
				this._trackingComponent = null;
				this._trackingComponentId = 0;
			}
			return true;
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x0002CBD0 File Offset: 0x0002ADD0
		bool UnsafeNativeMethods.IMsoComponentManager.FUpdateComponentRegistration(IntPtr dwComponentID, NativeMethods.MSOCRINFOSTRUCT info)
		{
			return this._original != null && this._original.FUpdateComponentRegistration(this._componentId, info);
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x0002CBF0 File Offset: 0x0002ADF0
		bool UnsafeNativeMethods.IMsoComponentManager.FOnComponentActivate(IntPtr dwComponentID)
		{
			int num = (int)(long)dwComponentID;
			if (this._original == null)
			{
				return false;
			}
			if (this._components == null || num <= 0 || !this._components.ContainsKey(num))
			{
				return false;
			}
			if (!this._original.FOnComponentActivate(this._componentId))
			{
				return false;
			}
			this._activeComponent = this._components[num];
			this._activeComponentId = num;
			return true;
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x0002CC5C File Offset: 0x0002AE5C
		bool UnsafeNativeMethods.IMsoComponentManager.FSetTrackingComponent(IntPtr dwComponentID, bool fTrack)
		{
			int num = (int)(long)dwComponentID;
			if (this._original == null)
			{
				return false;
			}
			if (this._components == null || num <= 0 || !this._components.ContainsKey(num))
			{
				return false;
			}
			if (!this._original.FSetTrackingComponent(this._componentId, fTrack))
			{
				return false;
			}
			if (fTrack)
			{
				this._trackingComponent = this._components[num];
				this._trackingComponentId = num;
			}
			else
			{
				this._trackingComponent = null;
				this._trackingComponentId = 0;
			}
			return true;
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x0002CCDC File Offset: 0x0002AEDC
		void UnsafeNativeMethods.IMsoComponentManager.OnComponentEnterState(IntPtr dwComponentID, int uStateID, int uContext, int cpicmExclude, int rgpicmExclude, int dwReserved)
		{
			if (this._original == null)
			{
				return;
			}
			if ((uContext == 0 || uContext == 1) && this._components != null)
			{
				foreach (UnsafeNativeMethods.IMsoComponent msoComponent in this._components.Values)
				{
					msoComponent.OnEnterState(uStateID, true);
				}
			}
			this._original.OnComponentEnterState(this._componentId, uStateID, uContext, cpicmExclude, rgpicmExclude, dwReserved);
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x0002CD68 File Offset: 0x0002AF68
		bool UnsafeNativeMethods.IMsoComponentManager.FOnComponentExitState(IntPtr dwComponentID, int uStateID, int uContext, int cpicmExclude, int rgpicmExclude)
		{
			if (this._original == null)
			{
				return false;
			}
			if ((uContext == 0 || uContext == 1) && this._components != null)
			{
				foreach (UnsafeNativeMethods.IMsoComponent msoComponent in this._components.Values)
				{
					msoComponent.OnEnterState(uStateID, false);
				}
			}
			return this._original.FOnComponentExitState(this._componentId, uStateID, uContext, cpicmExclude, rgpicmExclude);
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x0002CDF4 File Offset: 0x0002AFF4
		bool UnsafeNativeMethods.IMsoComponentManager.FInState(int uStateID, IntPtr pvoid)
		{
			return this._original != null && this._original.FInState(uStateID, pvoid);
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x0002CE0D File Offset: 0x0002B00D
		bool UnsafeNativeMethods.IMsoComponentManager.FContinueIdle()
		{
			return this._original != null && this._original.FContinueIdle();
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x0002CE24 File Offset: 0x0002B024
		bool UnsafeNativeMethods.IMsoComponentManager.FPushMessageLoop(IntPtr dwComponentID, int reason, int pvLoopData)
		{
			return this._original != null && this._original.FPushMessageLoop(this._componentId, reason, pvLoopData);
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x0002CE43 File Offset: 0x0002B043
		bool UnsafeNativeMethods.IMsoComponentManager.FCreateSubComponentManager(object punkOuter, object punkServProv, ref Guid riid, out IntPtr ppvObj)
		{
			if (this._original == null)
			{
				ppvObj = IntPtr.Zero;
				return false;
			}
			return this._original.FCreateSubComponentManager(punkOuter, punkServProv, ref riid, out ppvObj);
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0002CE67 File Offset: 0x0002B067
		bool UnsafeNativeMethods.IMsoComponentManager.FGetParentComponentManager(out UnsafeNativeMethods.IMsoComponentManager ppicm)
		{
			if (this._original == null)
			{
				ppicm = null;
				return false;
			}
			return this._original.FGetParentComponentManager(out ppicm);
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x0002CE84 File Offset: 0x0002B084
		bool UnsafeNativeMethods.IMsoComponentManager.FGetActiveComponent(int dwgac, UnsafeNativeMethods.IMsoComponent[] ppic, NativeMethods.MSOCRINFOSTRUCT info, int dwReserved)
		{
			if (this._original == null)
			{
				return false;
			}
			if (this._original.FGetActiveComponent(dwgac, ppic, info, dwReserved))
			{
				if (ppic[0] == this)
				{
					if (dwgac == 0)
					{
						ppic[0] = this._activeComponent;
					}
					else if (dwgac == 1)
					{
						ppic[0] = this._trackingComponent;
					}
					else if (dwgac == 2 && this._trackingComponent != null)
					{
						ppic[0] = this._trackingComponent;
					}
				}
				return ppic[0] != null;
			}
			return false;
		}

		// Token: 0x040007F5 RID: 2037
		private ComponentManagerBroker _broker;

		// Token: 0x040007F6 RID: 2038
		private UnsafeNativeMethods.IMsoComponentManager _original;

		// Token: 0x040007F7 RID: 2039
		private int _refCount;

		// Token: 0x040007F8 RID: 2040
		private int _creationThread;

		// Token: 0x040007F9 RID: 2041
		private IntPtr _componentId;

		// Token: 0x040007FA RID: 2042
		private int _nextComponentId;

		// Token: 0x040007FB RID: 2043
		private Dictionary<int, UnsafeNativeMethods.IMsoComponent> _components;

		// Token: 0x040007FC RID: 2044
		private UnsafeNativeMethods.IMsoComponent _activeComponent;

		// Token: 0x040007FD RID: 2045
		private int _activeComponentId;

		// Token: 0x040007FE RID: 2046
		private UnsafeNativeMethods.IMsoComponent _trackingComponent;

		// Token: 0x040007FF RID: 2047
		private int _trackingComponentId;
	}
}
