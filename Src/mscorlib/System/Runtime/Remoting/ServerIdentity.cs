using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting
{
	// Token: 0x020007CA RID: 1994
	internal class ServerIdentity : Identity
	{
		// Token: 0x060056A6 RID: 22182 RVA: 0x001349B4 File Offset: 0x00132BB4
		internal Type GetLastCalledType(string newTypeName)
		{
			ServerIdentity.LastCalledType lastCalledType = this._lastCalledType;
			if (lastCalledType == null)
			{
				return null;
			}
			string typeName = lastCalledType.typeName;
			Type type = lastCalledType.type;
			if (typeName == null || type == null)
			{
				return null;
			}
			if (typeName.Equals(newTypeName))
			{
				return type;
			}
			return null;
		}

		// Token: 0x060056A7 RID: 22183 RVA: 0x001349F8 File Offset: 0x00132BF8
		internal void SetLastCalledType(string newTypeName, Type newType)
		{
			this._lastCalledType = new ServerIdentity.LastCalledType
			{
				typeName = newTypeName,
				type = newType
			};
		}

		// Token: 0x060056A8 RID: 22184 RVA: 0x00134A20 File Offset: 0x00132C20
		[SecurityCritical]
		internal void SetHandle()
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Monitor.Enter(this, ref flag);
				if (!this._srvIdentityHandle.IsAllocated)
				{
					this._srvIdentityHandle = new GCHandle(this, GCHandleType.Normal);
				}
				else
				{
					this._srvIdentityHandle.Target = this;
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this);
				}
			}
		}

		// Token: 0x060056A9 RID: 22185 RVA: 0x00134A80 File Offset: 0x00132C80
		[SecurityCritical]
		internal void ResetHandle()
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Monitor.Enter(this, ref flag);
				this._srvIdentityHandle.Target = null;
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this);
				}
			}
		}

		// Token: 0x060056AA RID: 22186 RVA: 0x00134AC4 File Offset: 0x00132CC4
		internal GCHandle GetHandle()
		{
			return this._srvIdentityHandle;
		}

		// Token: 0x060056AB RID: 22187 RVA: 0x00134ACC File Offset: 0x00132CCC
		[SecurityCritical]
		internal ServerIdentity(MarshalByRefObject obj, Context serverCtx)
			: base(obj is ContextBoundObject)
		{
			if (obj != null)
			{
				if (!RemotingServices.IsTransparentProxy(obj))
				{
					this._srvType = obj.GetType();
				}
				else
				{
					RealProxy realProxy = RemotingServices.GetRealProxy(obj);
					this._srvType = realProxy.GetProxiedType();
				}
			}
			this._srvCtx = serverCtx;
			this._serverObjectChain = null;
			this._stackBuilderSink = null;
		}

		// Token: 0x060056AC RID: 22188 RVA: 0x00134B29 File Offset: 0x00132D29
		[SecurityCritical]
		internal ServerIdentity(MarshalByRefObject obj, Context serverCtx, string uri)
			: this(obj, serverCtx)
		{
			base.SetOrCreateURI(uri, true);
		}

		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x060056AD RID: 22189 RVA: 0x00134B3B File Offset: 0x00132D3B
		internal Context ServerContext
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this._srvCtx;
			}
		}

		// Token: 0x060056AE RID: 22190 RVA: 0x00134B43 File Offset: 0x00132D43
		internal void SetSingleCallObjectMode()
		{
			this._flags |= 512;
		}

		// Token: 0x060056AF RID: 22191 RVA: 0x00134B57 File Offset: 0x00132D57
		internal void SetSingletonObjectMode()
		{
			this._flags |= 1024;
		}

		// Token: 0x060056B0 RID: 22192 RVA: 0x00134B6B File Offset: 0x00132D6B
		internal bool IsSingleCall()
		{
			return (this._flags & 512) != 0;
		}

		// Token: 0x060056B1 RID: 22193 RVA: 0x00134B7C File Offset: 0x00132D7C
		internal bool IsSingleton()
		{
			return (this._flags & 1024) != 0;
		}

		// Token: 0x060056B2 RID: 22194 RVA: 0x00134B90 File Offset: 0x00132D90
		[SecurityCritical]
		internal IMessageSink GetServerObjectChain(out MarshalByRefObject obj)
		{
			obj = null;
			if (!this.IsSingleCall())
			{
				if (this._serverObjectChain == null)
				{
					bool flag = false;
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						Monitor.Enter(this, ref flag);
						if (this._serverObjectChain == null)
						{
							MarshalByRefObject tporObject = base.TPOrObject;
							this._serverObjectChain = this._srvCtx.CreateServerObjectChain(tporObject);
						}
					}
					finally
					{
						if (flag)
						{
							Monitor.Exit(this);
						}
					}
				}
				return this._serverObjectChain;
			}
			MarshalByRefObject marshalByRefObject;
			IMessageSink messageSink;
			if (this._tpOrObject != null && this._firstCallDispatched == 0 && Interlocked.CompareExchange(ref this._firstCallDispatched, 1, 0) == 0)
			{
				marshalByRefObject = (MarshalByRefObject)this._tpOrObject;
				messageSink = this._serverObjectChain;
				if (messageSink == null)
				{
					messageSink = this._srvCtx.CreateServerObjectChain(marshalByRefObject);
				}
			}
			else
			{
				marshalByRefObject = (MarshalByRefObject)Activator.CreateInstance(this._srvType, true);
				string objectUri = RemotingServices.GetObjectUri(marshalByRefObject);
				if (objectUri != null)
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_WellKnown_CtorCantMarshal"), base.URI));
				}
				if (!RemotingServices.IsTransparentProxy(marshalByRefObject))
				{
					marshalByRefObject.__RaceSetServerIdentity(this);
				}
				else
				{
					RealProxy realProxy = RemotingServices.GetRealProxy(marshalByRefObject);
					realProxy.IdentityObject = this;
				}
				messageSink = this._srvCtx.CreateServerObjectChain(marshalByRefObject);
			}
			obj = marshalByRefObject;
			return messageSink;
		}

		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x060056B3 RID: 22195 RVA: 0x00134CC0 File Offset: 0x00132EC0
		// (set) Token: 0x060056B4 RID: 22196 RVA: 0x00134CC8 File Offset: 0x00132EC8
		internal Type ServerType
		{
			get
			{
				return this._srvType;
			}
			set
			{
				this._srvType = value;
			}
		}

		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x060056B5 RID: 22197 RVA: 0x00134CD1 File Offset: 0x00132ED1
		// (set) Token: 0x060056B6 RID: 22198 RVA: 0x00134CD9 File Offset: 0x00132ED9
		internal bool MarshaledAsSpecificType
		{
			get
			{
				return this._bMarshaledAsSpecificType;
			}
			set
			{
				this._bMarshaledAsSpecificType = value;
			}
		}

		// Token: 0x060056B7 RID: 22199 RVA: 0x00134CE4 File Offset: 0x00132EE4
		[SecurityCritical]
		internal IMessageSink RaceSetServerObjectChain(IMessageSink serverObjectChain)
		{
			if (this._serverObjectChain == null)
			{
				bool flag = false;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					Monitor.Enter(this, ref flag);
					if (this._serverObjectChain == null)
					{
						this._serverObjectChain = serverObjectChain;
					}
				}
				finally
				{
					if (flag)
					{
						Monitor.Exit(this);
					}
				}
			}
			return this._serverObjectChain;
		}

		// Token: 0x060056B8 RID: 22200 RVA: 0x00134D3C File Offset: 0x00132F3C
		[SecurityCritical]
		internal bool AddServerSideDynamicProperty(IDynamicProperty prop)
		{
			if (this._dphSrv == null)
			{
				DynamicPropertyHolder dynamicPropertyHolder = new DynamicPropertyHolder();
				bool flag = false;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					Monitor.Enter(this, ref flag);
					if (this._dphSrv == null)
					{
						this._dphSrv = dynamicPropertyHolder;
					}
				}
				finally
				{
					if (flag)
					{
						Monitor.Exit(this);
					}
				}
			}
			return this._dphSrv.AddDynamicProperty(prop);
		}

		// Token: 0x060056B9 RID: 22201 RVA: 0x00134DA0 File Offset: 0x00132FA0
		[SecurityCritical]
		internal bool RemoveServerSideDynamicProperty(string name)
		{
			if (this._dphSrv == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_PropNotFound"));
			}
			return this._dphSrv.RemoveDynamicProperty(name);
		}

		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x060056BA RID: 22202 RVA: 0x00134DC6 File Offset: 0x00132FC6
		internal ArrayWithSize ServerSideDynamicSinks
		{
			[SecurityCritical]
			get
			{
				if (this._dphSrv == null)
				{
					return null;
				}
				return this._dphSrv.DynamicSinks;
			}
		}

		// Token: 0x060056BB RID: 22203 RVA: 0x00134DDD File Offset: 0x00132FDD
		[SecurityCritical]
		internal override void AssertValid()
		{
			if (base.TPOrObject != null)
			{
				RemotingServices.IsTransparentProxy(base.TPOrObject);
			}
		}

		// Token: 0x040027A8 RID: 10152
		internal Context _srvCtx;

		// Token: 0x040027A9 RID: 10153
		internal IMessageSink _serverObjectChain;

		// Token: 0x040027AA RID: 10154
		internal StackBuilderSink _stackBuilderSink;

		// Token: 0x040027AB RID: 10155
		internal DynamicPropertyHolder _dphSrv;

		// Token: 0x040027AC RID: 10156
		internal Type _srvType;

		// Token: 0x040027AD RID: 10157
		private ServerIdentity.LastCalledType _lastCalledType;

		// Token: 0x040027AE RID: 10158
		internal bool _bMarshaledAsSpecificType;

		// Token: 0x040027AF RID: 10159
		internal int _firstCallDispatched;

		// Token: 0x040027B0 RID: 10160
		internal GCHandle _srvIdentityHandle;

		// Token: 0x02000C65 RID: 3173
		private class LastCalledType
		{
			// Token: 0x040037DA RID: 14298
			public string typeName;

			// Token: 0x040037DB RID: 14299
			public Type type;
		}
	}
}
