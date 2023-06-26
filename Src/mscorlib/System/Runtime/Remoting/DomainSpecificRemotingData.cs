using System;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Lifetime;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting
{
	// Token: 0x020007AC RID: 1964
	internal class DomainSpecificRemotingData
	{
		// Token: 0x06005526 RID: 21798 RVA: 0x0012FA94 File Offset: 0x0012DC94
		internal DomainSpecificRemotingData()
		{
			this._flags = 0;
			this._ConfigLock = new object();
			this._ChannelServicesData = new ChannelServicesData();
			this._IDTableLock = new ReaderWriterLock();
			this._appDomainProperties = new IContextProperty[1];
			this._appDomainProperties[0] = new LeaseLifeTimeServiceProperty();
		}

		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x06005527 RID: 21799 RVA: 0x0012FAE8 File Offset: 0x0012DCE8
		// (set) Token: 0x06005528 RID: 21800 RVA: 0x0012FAF0 File Offset: 0x0012DCF0
		internal LeaseManager LeaseManager
		{
			get
			{
				return this._LeaseManager;
			}
			set
			{
				this._LeaseManager = value;
			}
		}

		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x06005529 RID: 21801 RVA: 0x0012FAF9 File Offset: 0x0012DCF9
		internal object ConfigLock
		{
			get
			{
				return this._ConfigLock;
			}
		}

		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x0600552A RID: 21802 RVA: 0x0012FB01 File Offset: 0x0012DD01
		internal ReaderWriterLock IDTableLock
		{
			get
			{
				return this._IDTableLock;
			}
		}

		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x0600552B RID: 21803 RVA: 0x0012FB09 File Offset: 0x0012DD09
		// (set) Token: 0x0600552C RID: 21804 RVA: 0x0012FB11 File Offset: 0x0012DD11
		internal LocalActivator LocalActivator
		{
			[SecurityCritical]
			get
			{
				return this._LocalActivator;
			}
			[SecurityCritical]
			set
			{
				this._LocalActivator = value;
			}
		}

		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x0600552D RID: 21805 RVA: 0x0012FB1A File Offset: 0x0012DD1A
		// (set) Token: 0x0600552E RID: 21806 RVA: 0x0012FB22 File Offset: 0x0012DD22
		internal ActivationListener ActivationListener
		{
			get
			{
				return this._ActivationListener;
			}
			set
			{
				this._ActivationListener = value;
			}
		}

		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x0600552F RID: 21807 RVA: 0x0012FB2B File Offset: 0x0012DD2B
		// (set) Token: 0x06005530 RID: 21808 RVA: 0x0012FB38 File Offset: 0x0012DD38
		internal bool InitializingActivation
		{
			get
			{
				return (this._flags & 1) == 1;
			}
			set
			{
				if (value)
				{
					this._flags |= 1;
					return;
				}
				this._flags &= -2;
			}
		}

		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x06005531 RID: 21809 RVA: 0x0012FB5B File Offset: 0x0012DD5B
		// (set) Token: 0x06005532 RID: 21810 RVA: 0x0012FB68 File Offset: 0x0012DD68
		internal bool ActivationInitialized
		{
			get
			{
				return (this._flags & 2) == 2;
			}
			set
			{
				if (value)
				{
					this._flags |= 2;
					return;
				}
				this._flags &= -3;
			}
		}

		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x06005533 RID: 21811 RVA: 0x0012FB8B File Offset: 0x0012DD8B
		// (set) Token: 0x06005534 RID: 21812 RVA: 0x0012FB98 File Offset: 0x0012DD98
		internal bool ActivatorListening
		{
			get
			{
				return (this._flags & 4) == 4;
			}
			set
			{
				if (value)
				{
					this._flags |= 4;
					return;
				}
				this._flags &= -5;
			}
		}

		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x06005535 RID: 21813 RVA: 0x0012FBBB File Offset: 0x0012DDBB
		internal IContextProperty[] AppDomainContextProperties
		{
			get
			{
				return this._appDomainProperties;
			}
		}

		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x06005536 RID: 21814 RVA: 0x0012FBC3 File Offset: 0x0012DDC3
		internal ChannelServicesData ChannelServicesData
		{
			get
			{
				return this._ChannelServicesData;
			}
		}

		// Token: 0x04002733 RID: 10035
		private const int ACTIVATION_INITIALIZING = 1;

		// Token: 0x04002734 RID: 10036
		private const int ACTIVATION_INITIALIZED = 2;

		// Token: 0x04002735 RID: 10037
		private const int ACTIVATOR_LISTENING = 4;

		// Token: 0x04002736 RID: 10038
		[SecurityCritical]
		private LocalActivator _LocalActivator;

		// Token: 0x04002737 RID: 10039
		private ActivationListener _ActivationListener;

		// Token: 0x04002738 RID: 10040
		private IContextProperty[] _appDomainProperties;

		// Token: 0x04002739 RID: 10041
		private int _flags;

		// Token: 0x0400273A RID: 10042
		private object _ConfigLock;

		// Token: 0x0400273B RID: 10043
		private ChannelServicesData _ChannelServicesData;

		// Token: 0x0400273C RID: 10044
		private LeaseManager _LeaseManager;

		// Token: 0x0400273D RID: 10045
		private ReaderWriterLock _IDTableLock;
	}
}
