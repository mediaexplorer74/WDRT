using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Cryptography;
using System.Threading;

namespace System.Runtime.Remoting
{
	// Token: 0x020007AF RID: 1967
	internal class Identity
	{
		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x0600555D RID: 21853 RVA: 0x001307E4 File Offset: 0x0012E9E4
		internal static string ProcessIDGuid
		{
			get
			{
				return SharedStatics.Remoting_Identity_IDGuid;
			}
		}

		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x0600555E RID: 21854 RVA: 0x001307EB File Offset: 0x0012E9EB
		internal static string AppDomainUniqueId
		{
			get
			{
				if (Identity.s_configuredAppDomainGuid != null)
				{
					return Identity.s_configuredAppDomainGuid;
				}
				return Identity.s_originalAppDomainGuid;
			}
		}

		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x0600555F RID: 21855 RVA: 0x001307FF File Offset: 0x0012E9FF
		internal static string IDGuidString
		{
			get
			{
				return Identity.s_IDGuidString;
			}
		}

		// Token: 0x06005560 RID: 21856 RVA: 0x00130808 File Offset: 0x0012EA08
		internal static string RemoveAppNameOrAppGuidIfNecessary(string uri)
		{
			if (uri == null || uri.Length <= 1 || uri[0] != '/')
			{
				return uri;
			}
			string text;
			if (Identity.s_configuredAppDomainGuidString != null)
			{
				text = Identity.s_configuredAppDomainGuidString;
				if (uri.Length > text.Length && Identity.StringStartsWith(uri, text))
				{
					return uri.Substring(text.Length);
				}
			}
			text = Identity.s_originalAppDomainGuidString;
			if (uri.Length > text.Length && Identity.StringStartsWith(uri, text))
			{
				return uri.Substring(text.Length);
			}
			string applicationName = RemotingConfiguration.ApplicationName;
			if (applicationName != null && uri.Length > applicationName.Length + 2 && string.Compare(uri, 1, applicationName, 0, applicationName.Length, true, CultureInfo.InvariantCulture) == 0 && uri[applicationName.Length + 1] == '/')
			{
				return uri.Substring(applicationName.Length + 2);
			}
			uri = uri.Substring(1);
			return uri;
		}

		// Token: 0x06005561 RID: 21857 RVA: 0x001308E4 File Offset: 0x0012EAE4
		private static bool StringStartsWith(string s1, string prefix)
		{
			return s1.Length >= prefix.Length && string.CompareOrdinal(s1, 0, prefix, 0, prefix.Length) == 0;
		}

		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x06005562 RID: 21858 RVA: 0x00130908 File Offset: 0x0012EB08
		internal static string ProcessGuid
		{
			get
			{
				return Identity.ProcessIDGuid;
			}
		}

		// Token: 0x06005563 RID: 21859 RVA: 0x0013090F File Offset: 0x0012EB0F
		private static int GetNextSeqNum()
		{
			return SharedStatics.Remoting_Identity_GetNextSeqNum();
		}

		// Token: 0x06005564 RID: 21860 RVA: 0x00130918 File Offset: 0x0012EB18
		private static byte[] GetRandomBytes()
		{
			byte[] array = new byte[18];
			Identity.s_rng.GetBytes(array);
			return array;
		}

		// Token: 0x06005565 RID: 21861 RVA: 0x00130939 File Offset: 0x0012EB39
		internal Identity(string objURI, string URL)
		{
			if (URL != null)
			{
				this._flags |= 256;
				this._URL = URL;
			}
			this.SetOrCreateURI(objURI, true);
		}

		// Token: 0x06005566 RID: 21862 RVA: 0x00130965 File Offset: 0x0012EB65
		internal Identity(bool bContextBound)
		{
			if (bContextBound)
			{
				this._flags |= 16;
			}
		}

		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x06005567 RID: 21863 RVA: 0x0013097F File Offset: 0x0012EB7F
		internal bool IsContextBound
		{
			get
			{
				return (this._flags & 16) == 16;
			}
		}

		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x06005568 RID: 21864 RVA: 0x0013098E File Offset: 0x0012EB8E
		// (set) Token: 0x06005569 RID: 21865 RVA: 0x00130998 File Offset: 0x0012EB98
		internal bool IsInitializing
		{
			get
			{
				return this._initializing;
			}
			set
			{
				this._initializing = value;
			}
		}

		// Token: 0x0600556A RID: 21866 RVA: 0x001309A3 File Offset: 0x0012EBA3
		internal bool IsWellKnown()
		{
			return (this._flags & 256) == 256;
		}

		// Token: 0x0600556B RID: 21867 RVA: 0x001309B8 File Offset: 0x0012EBB8
		internal void SetInIDTable()
		{
			int flags;
			int num;
			do
			{
				flags = this._flags;
				num = this._flags | 4;
			}
			while (flags != Interlocked.CompareExchange(ref this._flags, num, flags));
		}

		// Token: 0x0600556C RID: 21868 RVA: 0x001309E8 File Offset: 0x0012EBE8
		[SecurityCritical]
		internal void ResetInIDTable(bool bResetURI)
		{
			int flags;
			int num;
			do
			{
				flags = this._flags;
				num = this._flags & -5;
			}
			while (flags != Interlocked.CompareExchange(ref this._flags, num, flags));
			if (bResetURI)
			{
				((ObjRef)this._objRef).URI = null;
				this._ObjURI = null;
			}
		}

		// Token: 0x0600556D RID: 21869 RVA: 0x00130A31 File Offset: 0x0012EC31
		internal bool IsInIDTable()
		{
			return (this._flags & 4) == 4;
		}

		// Token: 0x0600556E RID: 21870 RVA: 0x00130A40 File Offset: 0x0012EC40
		internal void SetFullyConnected()
		{
			int flags;
			int num;
			do
			{
				flags = this._flags;
				num = this._flags & -4;
			}
			while (flags != Interlocked.CompareExchange(ref this._flags, num, flags));
		}

		// Token: 0x0600556F RID: 21871 RVA: 0x00130A6E File Offset: 0x0012EC6E
		internal bool IsFullyDisconnected()
		{
			return (this._flags & 1) == 1;
		}

		// Token: 0x06005570 RID: 21872 RVA: 0x00130A7B File Offset: 0x0012EC7B
		internal bool IsRemoteDisconnected()
		{
			return (this._flags & 2) == 2;
		}

		// Token: 0x06005571 RID: 21873 RVA: 0x00130A88 File Offset: 0x0012EC88
		internal bool IsDisconnected()
		{
			return this.IsFullyDisconnected() || this.IsRemoteDisconnected();
		}

		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x06005572 RID: 21874 RVA: 0x00130A9A File Offset: 0x0012EC9A
		internal string URI
		{
			get
			{
				if (this.IsWellKnown())
				{
					return this._URL;
				}
				return this._ObjURI;
			}
		}

		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x06005573 RID: 21875 RVA: 0x00130AB1 File Offset: 0x0012ECB1
		internal string ObjURI
		{
			get
			{
				return this._ObjURI;
			}
		}

		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x06005574 RID: 21876 RVA: 0x00130AB9 File Offset: 0x0012ECB9
		internal MarshalByRefObject TPOrObject
		{
			get
			{
				return (MarshalByRefObject)this._tpOrObject;
			}
		}

		// Token: 0x06005575 RID: 21877 RVA: 0x00130AC6 File Offset: 0x0012ECC6
		internal object RaceSetTransparentProxy(object tpObj)
		{
			if (this._tpOrObject == null)
			{
				Interlocked.CompareExchange(ref this._tpOrObject, tpObj, null);
			}
			return this._tpOrObject;
		}

		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x06005576 RID: 21878 RVA: 0x00130AE4 File Offset: 0x0012ECE4
		internal ObjRef ObjectRef
		{
			[SecurityCritical]
			get
			{
				return (ObjRef)this._objRef;
			}
		}

		// Token: 0x06005577 RID: 21879 RVA: 0x00130AF1 File Offset: 0x0012ECF1
		[SecurityCritical]
		internal ObjRef RaceSetObjRef(ObjRef objRefGiven)
		{
			if (this._objRef == null)
			{
				Interlocked.CompareExchange(ref this._objRef, objRefGiven, null);
			}
			return (ObjRef)this._objRef;
		}

		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x06005578 RID: 21880 RVA: 0x00130B14 File Offset: 0x0012ED14
		internal IMessageSink ChannelSink
		{
			get
			{
				return (IMessageSink)this._channelSink;
			}
		}

		// Token: 0x06005579 RID: 21881 RVA: 0x00130B21 File Offset: 0x0012ED21
		internal IMessageSink RaceSetChannelSink(IMessageSink channelSink)
		{
			if (this._channelSink == null)
			{
				Interlocked.CompareExchange(ref this._channelSink, channelSink, null);
			}
			return (IMessageSink)this._channelSink;
		}

		// Token: 0x17000E0F RID: 3599
		// (get) Token: 0x0600557A RID: 21882 RVA: 0x00130B44 File Offset: 0x0012ED44
		internal IMessageSink EnvoyChain
		{
			get
			{
				return (IMessageSink)this._envoyChain;
			}
		}

		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x0600557B RID: 21883 RVA: 0x00130B51 File Offset: 0x0012ED51
		// (set) Token: 0x0600557C RID: 21884 RVA: 0x00130B59 File Offset: 0x0012ED59
		internal Lease Lease
		{
			get
			{
				return this._lease;
			}
			set
			{
				this._lease = value;
			}
		}

		// Token: 0x0600557D RID: 21885 RVA: 0x00130B62 File Offset: 0x0012ED62
		internal IMessageSink RaceSetEnvoyChain(IMessageSink envoyChain)
		{
			if (this._envoyChain == null)
			{
				Interlocked.CompareExchange(ref this._envoyChain, envoyChain, null);
			}
			return (IMessageSink)this._envoyChain;
		}

		// Token: 0x0600557E RID: 21886 RVA: 0x00130B85 File Offset: 0x0012ED85
		internal void SetOrCreateURI(string uri)
		{
			this.SetOrCreateURI(uri, false);
		}

		// Token: 0x0600557F RID: 21887 RVA: 0x00130B90 File Offset: 0x0012ED90
		internal void SetOrCreateURI(string uri, bool bIdCtor)
		{
			if (!bIdCtor && this._ObjURI != null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_SetObjectUriForMarshal__UriExists"));
			}
			if (uri == null)
			{
				string text = Convert.ToBase64String(Identity.GetRandomBytes());
				this._ObjURI = string.Concat(new string[]
				{
					Identity.IDGuidString,
					text.Replace('/', '_'),
					"_",
					Identity.GetNextSeqNum().ToString(CultureInfo.InvariantCulture.NumberFormat),
					".rem"
				}).ToLower(CultureInfo.InvariantCulture);
				return;
			}
			if (this is ServerIdentity)
			{
				this._ObjURI = Identity.IDGuidString + uri;
				return;
			}
			this._ObjURI = uri;
		}

		// Token: 0x06005580 RID: 21888 RVA: 0x00130C44 File Offset: 0x0012EE44
		internal static string GetNewLogicalCallID()
		{
			return Identity.IDGuidString + Identity.GetNextSeqNum().ToString();
		}

		// Token: 0x06005581 RID: 21889 RVA: 0x00130C68 File Offset: 0x0012EE68
		[SecurityCritical]
		[Conditional("_DEBUG")]
		internal virtual void AssertValid()
		{
			if (this.URI != null)
			{
				Identity identity = IdentityHolder.ResolveIdentity(this.URI);
			}
		}

		// Token: 0x06005582 RID: 21890 RVA: 0x00130C8C File Offset: 0x0012EE8C
		[SecurityCritical]
		internal bool AddProxySideDynamicProperty(IDynamicProperty prop)
		{
			bool flag3;
			lock (this)
			{
				if (this._dph == null)
				{
					DynamicPropertyHolder dynamicPropertyHolder = new DynamicPropertyHolder();
					lock (this)
					{
						if (this._dph == null)
						{
							this._dph = dynamicPropertyHolder;
						}
					}
				}
				flag3 = this._dph.AddDynamicProperty(prop);
			}
			return flag3;
		}

		// Token: 0x06005583 RID: 21891 RVA: 0x00130D14 File Offset: 0x0012EF14
		[SecurityCritical]
		internal bool RemoveProxySideDynamicProperty(string name)
		{
			bool flag2;
			lock (this)
			{
				if (this._dph == null)
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Contexts_NoProperty"), name));
				}
				flag2 = this._dph.RemoveDynamicProperty(name);
			}
			return flag2;
		}

		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x06005584 RID: 21892 RVA: 0x00130D7C File Offset: 0x0012EF7C
		internal ArrayWithSize ProxySideDynamicSinks
		{
			[SecurityCritical]
			get
			{
				if (this._dph == null)
				{
					return null;
				}
				return this._dph.DynamicSinks;
			}
		}

		// Token: 0x0400274A RID: 10058
		private static string s_originalAppDomainGuid = Guid.NewGuid().ToString().Replace('-', '_');

		// Token: 0x0400274B RID: 10059
		private static string s_configuredAppDomainGuid = null;

		// Token: 0x0400274C RID: 10060
		private static string s_originalAppDomainGuidString = "/" + Identity.s_originalAppDomainGuid.ToLower(CultureInfo.InvariantCulture) + "/";

		// Token: 0x0400274D RID: 10061
		private static string s_configuredAppDomainGuidString = null;

		// Token: 0x0400274E RID: 10062
		private static string s_IDGuidString = "/" + Identity.s_originalAppDomainGuid.ToLower(CultureInfo.InvariantCulture) + "/";

		// Token: 0x0400274F RID: 10063
		private static RNGCryptoServiceProvider s_rng = new RNGCryptoServiceProvider();

		// Token: 0x04002750 RID: 10064
		protected const int IDFLG_DISCONNECTED_FULL = 1;

		// Token: 0x04002751 RID: 10065
		protected const int IDFLG_DISCONNECTED_REM = 2;

		// Token: 0x04002752 RID: 10066
		protected const int IDFLG_IN_IDTABLE = 4;

		// Token: 0x04002753 RID: 10067
		protected const int IDFLG_CONTEXT_BOUND = 16;

		// Token: 0x04002754 RID: 10068
		protected const int IDFLG_WELLKNOWN = 256;

		// Token: 0x04002755 RID: 10069
		protected const int IDFLG_SERVER_SINGLECALL = 512;

		// Token: 0x04002756 RID: 10070
		protected const int IDFLG_SERVER_SINGLETON = 1024;

		// Token: 0x04002757 RID: 10071
		internal int _flags;

		// Token: 0x04002758 RID: 10072
		internal object _tpOrObject;

		// Token: 0x04002759 RID: 10073
		protected string _ObjURI;

		// Token: 0x0400275A RID: 10074
		protected string _URL;

		// Token: 0x0400275B RID: 10075
		internal object _objRef;

		// Token: 0x0400275C RID: 10076
		internal object _channelSink;

		// Token: 0x0400275D RID: 10077
		internal object _envoyChain;

		// Token: 0x0400275E RID: 10078
		internal DynamicPropertyHolder _dph;

		// Token: 0x0400275F RID: 10079
		internal Lease _lease;

		// Token: 0x04002760 RID: 10080
		private volatile bool _initializing;
	}
}
