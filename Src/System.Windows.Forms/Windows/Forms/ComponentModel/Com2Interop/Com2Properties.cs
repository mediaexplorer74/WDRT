using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x020004A0 RID: 1184
	internal class Com2Properties
	{
		// Token: 0x1400040B RID: 1035
		// (add) Token: 0x06004EC1 RID: 20161 RVA: 0x00144458 File Offset: 0x00142658
		// (remove) Token: 0x06004EC2 RID: 20162 RVA: 0x00144490 File Offset: 0x00142690
		public event EventHandler Disposed;

		// Token: 0x06004EC3 RID: 20163 RVA: 0x001444C8 File Offset: 0x001426C8
		public Com2Properties(object obj, Com2PropertyDescriptor[] props, int defaultIndex)
		{
			this.SetProps(props);
			this.weakObjRef = new WeakReference(obj);
			this.defaultIndex = defaultIndex;
			this.typeInfoVersions = this.GetTypeInfoVersions(obj);
			this.touchedTime = DateTime.Now.Ticks;
		}

		// Token: 0x17001353 RID: 4947
		// (get) Token: 0x06004EC4 RID: 20164 RVA: 0x0014451C File Offset: 0x0014271C
		// (set) Token: 0x06004EC5 RID: 20165 RVA: 0x00144527 File Offset: 0x00142727
		internal bool AlwaysValid
		{
			get
			{
				return this.alwaysValid > 0;
			}
			set
			{
				if (!value)
				{
					if (this.alwaysValid > 0)
					{
						this.alwaysValid--;
					}
					return;
				}
				if (this.alwaysValid == 0 && !this.CheckValid())
				{
					return;
				}
				this.alwaysValid++;
			}
		}

		// Token: 0x17001354 RID: 4948
		// (get) Token: 0x06004EC6 RID: 20166 RVA: 0x00144563 File Offset: 0x00142763
		public Com2PropertyDescriptor DefaultProperty
		{
			get
			{
				if (!this.CheckValid(true))
				{
					return null;
				}
				if (this.defaultIndex != -1)
				{
					return this.props[this.defaultIndex];
				}
				if (this.props.Length != 0)
				{
					return this.props[0];
				}
				return null;
			}
		}

		// Token: 0x17001355 RID: 4949
		// (get) Token: 0x06004EC7 RID: 20167 RVA: 0x0014459A File Offset: 0x0014279A
		public object TargetObject
		{
			get
			{
				if (!this.CheckValid(false) || this.touchedTime == 0L)
				{
					return null;
				}
				return this.weakObjRef.Target;
			}
		}

		// Token: 0x17001356 RID: 4950
		// (get) Token: 0x06004EC8 RID: 20168 RVA: 0x001445BC File Offset: 0x001427BC
		public long TicksSinceTouched
		{
			get
			{
				if (this.touchedTime == 0L)
				{
					return 0L;
				}
				return DateTime.Now.Ticks - this.touchedTime;
			}
		}

		// Token: 0x17001357 RID: 4951
		// (get) Token: 0x06004EC9 RID: 20169 RVA: 0x001445E8 File Offset: 0x001427E8
		public Com2PropertyDescriptor[] Properties
		{
			get
			{
				this.CheckValid(true);
				if (this.touchedTime == 0L || this.props == null)
				{
					return null;
				}
				this.touchedTime = DateTime.Now.Ticks;
				for (int i = 0; i < this.props.Length; i++)
				{
					this.props[i].SetNeedsRefresh(255, true);
				}
				return this.props;
			}
		}

		// Token: 0x17001358 RID: 4952
		// (get) Token: 0x06004ECA RID: 20170 RVA: 0x0014464E File Offset: 0x0014284E
		public bool TooOld
		{
			get
			{
				this.CheckValid(false, false);
				return this.touchedTime != 0L && this.TicksSinceTouched > Com2Properties.AGE_THRESHHOLD;
			}
		}

		// Token: 0x06004ECB RID: 20171 RVA: 0x00144670 File Offset: 0x00142870
		public void AddExtendedBrowsingHandlers(Hashtable handlers)
		{
			object targetObject = this.TargetObject;
			if (targetObject == null)
			{
				return;
			}
			for (int i = 0; i < Com2Properties.extendedInterfaces.Length; i++)
			{
				Type type = Com2Properties.extendedInterfaces[i];
				if (type.IsInstanceOfType(targetObject))
				{
					Com2ExtendedBrowsingHandler com2ExtendedBrowsingHandler = (Com2ExtendedBrowsingHandler)handlers[type];
					if (com2ExtendedBrowsingHandler == null)
					{
						com2ExtendedBrowsingHandler = (Com2ExtendedBrowsingHandler)Activator.CreateInstance(Com2Properties.extendedInterfaceHandlerTypes[i]);
						handlers[type] = com2ExtendedBrowsingHandler;
					}
					if (!type.IsAssignableFrom(com2ExtendedBrowsingHandler.Interface))
					{
						throw new ArgumentException(SR.GetString("COM2BadHandlerType", new object[]
						{
							type.Name,
							com2ExtendedBrowsingHandler.Interface.Name
						}));
					}
					com2ExtendedBrowsingHandler.SetupPropertyHandlers(this.props);
				}
			}
		}

		// Token: 0x06004ECC RID: 20172 RVA: 0x00144724 File Offset: 0x00142924
		public void Dispose()
		{
			if (this.props != null)
			{
				if (this.Disposed != null)
				{
					this.Disposed(this, EventArgs.Empty);
				}
				this.weakObjRef = null;
				this.props = null;
				this.touchedTime = 0L;
			}
		}

		// Token: 0x06004ECD RID: 20173 RVA: 0x0014475D File Offset: 0x0014295D
		public bool CheckValid()
		{
			return this.CheckValid(false);
		}

		// Token: 0x06004ECE RID: 20174 RVA: 0x00144766 File Offset: 0x00142966
		public bool CheckValid(bool checkVersions)
		{
			return this.CheckValid(checkVersions, true);
		}

		// Token: 0x06004ECF RID: 20175 RVA: 0x00144770 File Offset: 0x00142970
		private long[] GetTypeInfoVersions(object comObject)
		{
			UnsafeNativeMethods.ITypeInfo[] array = Com2TypeInfoProcessor.FindTypeInfos(comObject, false);
			long[] array2 = new long[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = this.GetTypeInfoVersion(array[i]);
			}
			return array2;
		}

		// Token: 0x17001359 RID: 4953
		// (get) Token: 0x06004ED0 RID: 20176 RVA: 0x001447A9 File Offset: 0x001429A9
		private static int CountMemberOffset
		{
			get
			{
				if (Com2Properties.countOffset == -1)
				{
					Com2Properties.countOffset = Marshal.SizeOf(typeof(Guid)) + IntPtr.Size + 24;
				}
				return Com2Properties.countOffset;
			}
		}

		// Token: 0x1700135A RID: 4954
		// (get) Token: 0x06004ED1 RID: 20177 RVA: 0x001447D5 File Offset: 0x001429D5
		private static int VersionOffset
		{
			get
			{
				if (Com2Properties.versionOffset == -1)
				{
					Com2Properties.versionOffset = Com2Properties.CountMemberOffset + 12;
				}
				return Com2Properties.versionOffset;
			}
		}

		// Token: 0x06004ED2 RID: 20178 RVA: 0x001447F4 File Offset: 0x001429F4
		private unsafe long GetTypeInfoVersion(UnsafeNativeMethods.ITypeInfo pTypeInfo)
		{
			IntPtr zero = IntPtr.Zero;
			int typeAttr = pTypeInfo.GetTypeAttr(ref zero);
			if (!NativeMethods.Succeeded(typeAttr))
			{
				return 0L;
			}
			long num2;
			try
			{
				System.Runtime.InteropServices.ComTypes.TYPEATTR typeattr;
				try
				{
					typeattr = *(System.Runtime.InteropServices.ComTypes.TYPEATTR*)(void*)zero;
				}
				catch
				{
					return 0L;
				}
				long num = 0L;
				int* ptr = (int*)(&num);
				byte* ptr2 = (byte*)(&typeattr);
				*ptr = *(int*)(ptr2 + Com2Properties.CountMemberOffset);
				ptr++;
				*ptr = *(int*)(ptr2 + Com2Properties.VersionOffset);
				num2 = num;
			}
			finally
			{
				pTypeInfo.ReleaseTypeAttr(zero);
			}
			return num2;
		}

		// Token: 0x06004ED3 RID: 20179 RVA: 0x00144888 File Offset: 0x00142A88
		internal bool CheckValid(bool checkVersions, bool callDispose)
		{
			if (this.AlwaysValid)
			{
				return true;
			}
			bool flag = this.weakObjRef != null && this.weakObjRef.IsAlive;
			if (flag && checkVersions)
			{
				long[] array = this.GetTypeInfoVersions(this.weakObjRef.Target);
				if (array.Length != this.typeInfoVersions.Length)
				{
					flag = false;
				}
				else
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i] != this.typeInfoVersions[i])
						{
							flag = false;
							break;
						}
					}
				}
				if (!flag)
				{
					this.typeInfoVersions = array;
				}
			}
			if (!flag && callDispose)
			{
				this.Dispose();
			}
			return flag;
		}

		// Token: 0x06004ED4 RID: 20180 RVA: 0x00144918 File Offset: 0x00142B18
		internal void SetProps(Com2PropertyDescriptor[] props)
		{
			this.props = props;
			if (props != null)
			{
				for (int i = 0; i < props.Length; i++)
				{
					props[i].PropertyManager = this;
				}
			}
		}

		// Token: 0x0400340E RID: 13326
		private static TraceSwitch DbgCom2PropertiesSwitch = new TraceSwitch("DbgCom2Properties", "Com2Properties: debug Com2 properties manager");

		// Token: 0x0400340F RID: 13327
		private static long AGE_THRESHHOLD = (long)((ulong)(-1294967296));

		// Token: 0x04003410 RID: 13328
		internal WeakReference weakObjRef;

		// Token: 0x04003411 RID: 13329
		private Com2PropertyDescriptor[] props;

		// Token: 0x04003412 RID: 13330
		private int defaultIndex = -1;

		// Token: 0x04003413 RID: 13331
		private long touchedTime;

		// Token: 0x04003414 RID: 13332
		private long[] typeInfoVersions;

		// Token: 0x04003415 RID: 13333
		private int alwaysValid;

		// Token: 0x04003416 RID: 13334
		private static Type[] extendedInterfaces = new Type[]
		{
			typeof(NativeMethods.ICategorizeProperties),
			typeof(NativeMethods.IProvidePropertyBuilder),
			typeof(NativeMethods.IPerPropertyBrowsing),
			typeof(NativeMethods.IVsPerPropertyBrowsing),
			typeof(NativeMethods.IManagedPerPropertyBrowsing)
		};

		// Token: 0x04003417 RID: 13335
		private static Type[] extendedInterfaceHandlerTypes = new Type[]
		{
			typeof(Com2ICategorizePropertiesHandler),
			typeof(Com2IProvidePropertyBuilderHandler),
			typeof(Com2IPerPropertyBrowsingHandler),
			typeof(Com2IVsPerPropertyBrowsingHandler),
			typeof(Com2IManagedPerPropertyBrowsingHandler)
		};

		// Token: 0x04003419 RID: 13337
		private static int countOffset = -1;

		// Token: 0x0400341A RID: 13338
		private static int versionOffset = -1;
	}
}
