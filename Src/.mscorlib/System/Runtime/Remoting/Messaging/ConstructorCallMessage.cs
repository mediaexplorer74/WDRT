using System;
using System.Collections;
using System.Reflection;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Proxies;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200085E RID: 2142
	internal class ConstructorCallMessage : IConstructionCallMessage, IMethodCallMessage, IMethodMessage, IMessage
	{
		// Token: 0x06005AF1 RID: 23281 RVA: 0x001403C2 File Offset: 0x0013E5C2
		private ConstructorCallMessage()
		{
		}

		// Token: 0x06005AF2 RID: 23282 RVA: 0x001403CA File Offset: 0x0013E5CA
		[SecurityCritical]
		internal ConstructorCallMessage(object[] callSiteActivationAttributes, object[] womAttr, object[] typeAttr, RuntimeType serverType)
		{
			this._activationType = serverType;
			this._activationTypeName = RemotingServices.GetDefaultQualifiedTypeName(this._activationType);
			this._callSiteActivationAttributes = callSiteActivationAttributes;
			this._womGlobalAttributes = womAttr;
			this._typeAttributes = typeAttr;
		}

		// Token: 0x06005AF3 RID: 23283 RVA: 0x00140400 File Offset: 0x0013E600
		[SecurityCritical]
		public object GetThisPtr()
		{
			if (this._message != null)
			{
				return this._message.GetThisPtr();
			}
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
		}

		// Token: 0x17000F45 RID: 3909
		// (get) Token: 0x06005AF4 RID: 23284 RVA: 0x00140425 File Offset: 0x0013E625
		public object[] CallSiteActivationAttributes
		{
			[SecurityCritical]
			get
			{
				return this._callSiteActivationAttributes;
			}
		}

		// Token: 0x06005AF5 RID: 23285 RVA: 0x0014042D File Offset: 0x0013E62D
		internal object[] GetWOMAttributes()
		{
			return this._womGlobalAttributes;
		}

		// Token: 0x06005AF6 RID: 23286 RVA: 0x00140435 File Offset: 0x0013E635
		internal object[] GetTypeAttributes()
		{
			return this._typeAttributes;
		}

		// Token: 0x17000F46 RID: 3910
		// (get) Token: 0x06005AF7 RID: 23287 RVA: 0x0014043D File Offset: 0x0013E63D
		public Type ActivationType
		{
			[SecurityCritical]
			get
			{
				if (this._activationType == null && this._activationTypeName != null)
				{
					this._activationType = RemotingServices.InternalGetTypeFromQualifiedTypeName(this._activationTypeName, false);
				}
				return this._activationType;
			}
		}

		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x06005AF8 RID: 23288 RVA: 0x0014046D File Offset: 0x0013E66D
		public string ActivationTypeName
		{
			[SecurityCritical]
			get
			{
				return this._activationTypeName;
			}
		}

		// Token: 0x17000F48 RID: 3912
		// (get) Token: 0x06005AF9 RID: 23289 RVA: 0x00140475 File Offset: 0x0013E675
		public IList ContextProperties
		{
			[SecurityCritical]
			get
			{
				if (this._contextProperties == null)
				{
					this._contextProperties = new ArrayList();
				}
				return this._contextProperties;
			}
		}

		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x06005AFA RID: 23290 RVA: 0x00140490 File Offset: 0x0013E690
		// (set) Token: 0x06005AFB RID: 23291 RVA: 0x001404B5 File Offset: 0x0013E6B5
		public string Uri
		{
			[SecurityCritical]
			get
			{
				if (this._message != null)
				{
					return this._message.Uri;
				}
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
			}
			set
			{
				if (this._message != null)
				{
					this._message.Uri = value;
					return;
				}
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
			}
		}

		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x06005AFC RID: 23292 RVA: 0x001404DB File Offset: 0x0013E6DB
		public string MethodName
		{
			[SecurityCritical]
			get
			{
				if (this._message != null)
				{
					return this._message.MethodName;
				}
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
			}
		}

		// Token: 0x17000F4B RID: 3915
		// (get) Token: 0x06005AFD RID: 23293 RVA: 0x00140500 File Offset: 0x0013E700
		public string TypeName
		{
			[SecurityCritical]
			get
			{
				if (this._message != null)
				{
					return this._message.TypeName;
				}
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
			}
		}

		// Token: 0x17000F4C RID: 3916
		// (get) Token: 0x06005AFE RID: 23294 RVA: 0x00140525 File Offset: 0x0013E725
		public object MethodSignature
		{
			[SecurityCritical]
			get
			{
				if (this._message != null)
				{
					return this._message.MethodSignature;
				}
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
			}
		}

		// Token: 0x17000F4D RID: 3917
		// (get) Token: 0x06005AFF RID: 23295 RVA: 0x0014054A File Offset: 0x0013E74A
		public MethodBase MethodBase
		{
			[SecurityCritical]
			get
			{
				if (this._message != null)
				{
					return this._message.MethodBase;
				}
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
			}
		}

		// Token: 0x17000F4E RID: 3918
		// (get) Token: 0x06005B00 RID: 23296 RVA: 0x0014056F File Offset: 0x0013E76F
		public int InArgCount
		{
			[SecurityCritical]
			get
			{
				if (this._argMapper == null)
				{
					this._argMapper = new ArgMapper(this, false);
				}
				return this._argMapper.ArgCount;
			}
		}

		// Token: 0x06005B01 RID: 23297 RVA: 0x00140591 File Offset: 0x0013E791
		[SecurityCritical]
		public object GetInArg(int argNum)
		{
			if (this._argMapper == null)
			{
				this._argMapper = new ArgMapper(this, false);
			}
			return this._argMapper.GetArg(argNum);
		}

		// Token: 0x06005B02 RID: 23298 RVA: 0x001405B4 File Offset: 0x0013E7B4
		[SecurityCritical]
		public string GetInArgName(int index)
		{
			if (this._argMapper == null)
			{
				this._argMapper = new ArgMapper(this, false);
			}
			return this._argMapper.GetArgName(index);
		}

		// Token: 0x17000F4F RID: 3919
		// (get) Token: 0x06005B03 RID: 23299 RVA: 0x001405D7 File Offset: 0x0013E7D7
		public object[] InArgs
		{
			[SecurityCritical]
			get
			{
				if (this._argMapper == null)
				{
					this._argMapper = new ArgMapper(this, false);
				}
				return this._argMapper.Args;
			}
		}

		// Token: 0x17000F50 RID: 3920
		// (get) Token: 0x06005B04 RID: 23300 RVA: 0x001405F9 File Offset: 0x0013E7F9
		public int ArgCount
		{
			[SecurityCritical]
			get
			{
				if (this._message != null)
				{
					return this._message.ArgCount;
				}
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
			}
		}

		// Token: 0x06005B05 RID: 23301 RVA: 0x0014061E File Offset: 0x0013E81E
		[SecurityCritical]
		public object GetArg(int argNum)
		{
			if (this._message != null)
			{
				return this._message.GetArg(argNum);
			}
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
		}

		// Token: 0x06005B06 RID: 23302 RVA: 0x00140644 File Offset: 0x0013E844
		[SecurityCritical]
		public string GetArgName(int index)
		{
			if (this._message != null)
			{
				return this._message.GetArgName(index);
			}
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
		}

		// Token: 0x17000F51 RID: 3921
		// (get) Token: 0x06005B07 RID: 23303 RVA: 0x0014066A File Offset: 0x0013E86A
		public bool HasVarArgs
		{
			[SecurityCritical]
			get
			{
				if (this._message != null)
				{
					return this._message.HasVarArgs;
				}
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
			}
		}

		// Token: 0x17000F52 RID: 3922
		// (get) Token: 0x06005B08 RID: 23304 RVA: 0x0014068F File Offset: 0x0013E88F
		public object[] Args
		{
			[SecurityCritical]
			get
			{
				if (this._message != null)
				{
					return this._message.Args;
				}
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
			}
		}

		// Token: 0x17000F53 RID: 3923
		// (get) Token: 0x06005B09 RID: 23305 RVA: 0x001406B4 File Offset: 0x0013E8B4
		public IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				if (this._properties == null)
				{
					object obj = new CCMDictionary(this, new Hashtable());
					Interlocked.CompareExchange(ref this._properties, obj, null);
				}
				return (IDictionary)this._properties;
			}
		}

		// Token: 0x17000F54 RID: 3924
		// (get) Token: 0x06005B0A RID: 23306 RVA: 0x001406EE File Offset: 0x0013E8EE
		// (set) Token: 0x06005B0B RID: 23307 RVA: 0x001406F6 File Offset: 0x0013E8F6
		public IActivator Activator
		{
			[SecurityCritical]
			get
			{
				return this._activator;
			}
			[SecurityCritical]
			set
			{
				this._activator = value;
			}
		}

		// Token: 0x17000F55 RID: 3925
		// (get) Token: 0x06005B0C RID: 23308 RVA: 0x001406FF File Offset: 0x0013E8FF
		public LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				return this.GetLogicalCallContext();
			}
		}

		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x06005B0D RID: 23309 RVA: 0x00140707 File Offset: 0x0013E907
		// (set) Token: 0x06005B0E RID: 23310 RVA: 0x00140714 File Offset: 0x0013E914
		internal bool ActivateInContext
		{
			get
			{
				return (this._iFlags & 1) != 0;
			}
			set
			{
				this._iFlags = (value ? (this._iFlags | 1) : (this._iFlags & -2));
			}
		}

		// Token: 0x06005B0F RID: 23311 RVA: 0x00140732 File Offset: 0x0013E932
		[SecurityCritical]
		internal void SetFrame(MessageData msgData)
		{
			this._message = new Message();
			this._message.InitFields(msgData);
		}

		// Token: 0x06005B10 RID: 23312 RVA: 0x0014074B File Offset: 0x0013E94B
		[SecurityCritical]
		internal LogicalCallContext GetLogicalCallContext()
		{
			if (this._message != null)
			{
				return this._message.GetLogicalCallContext();
			}
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
		}

		// Token: 0x06005B11 RID: 23313 RVA: 0x00140770 File Offset: 0x0013E970
		[SecurityCritical]
		internal LogicalCallContext SetLogicalCallContext(LogicalCallContext ctx)
		{
			if (this._message != null)
			{
				return this._message.SetLogicalCallContext(ctx);
			}
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
		}

		// Token: 0x06005B12 RID: 23314 RVA: 0x00140796 File Offset: 0x0013E996
		internal Message GetMessage()
		{
			return this._message;
		}

		// Token: 0x04002934 RID: 10548
		private object[] _callSiteActivationAttributes;

		// Token: 0x04002935 RID: 10549
		private object[] _womGlobalAttributes;

		// Token: 0x04002936 RID: 10550
		private object[] _typeAttributes;

		// Token: 0x04002937 RID: 10551
		[NonSerialized]
		private RuntimeType _activationType;

		// Token: 0x04002938 RID: 10552
		private string _activationTypeName;

		// Token: 0x04002939 RID: 10553
		private IList _contextProperties;

		// Token: 0x0400293A RID: 10554
		private int _iFlags;

		// Token: 0x0400293B RID: 10555
		private Message _message;

		// Token: 0x0400293C RID: 10556
		private object _properties;

		// Token: 0x0400293D RID: 10557
		private ArgMapper _argMapper;

		// Token: 0x0400293E RID: 10558
		private IActivator _activator;

		// Token: 0x0400293F RID: 10559
		private const int CCM_ACTIVATEINCONTEXT = 1;
	}
}
