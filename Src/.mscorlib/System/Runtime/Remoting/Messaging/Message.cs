using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200085C RID: 2140
	[Serializable]
	internal class Message : IMethodCallMessage, IMethodMessage, IMessage, IInternalMessage, ISerializable
	{
		// Token: 0x06005AB0 RID: 23216 RVA: 0x0013F663 File Offset: 0x0013D863
		public virtual Exception GetFault()
		{
			return this._Fault;
		}

		// Token: 0x06005AB1 RID: 23217 RVA: 0x0013F66B File Offset: 0x0013D86B
		public virtual void SetFault(Exception e)
		{
			this._Fault = e;
		}

		// Token: 0x06005AB2 RID: 23218 RVA: 0x0013F674 File Offset: 0x0013D874
		internal virtual void SetOneWay()
		{
			this._flags |= 8;
		}

		// Token: 0x06005AB3 RID: 23219 RVA: 0x0013F684 File Offset: 0x0013D884
		public virtual int GetCallType()
		{
			this.InitIfNecessary();
			return this._flags;
		}

		// Token: 0x06005AB4 RID: 23220 RVA: 0x0013F692 File Offset: 0x0013D892
		internal IntPtr GetFramePtr()
		{
			return this._frame;
		}

		// Token: 0x06005AB5 RID: 23221
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetAsyncBeginInfo(out AsyncCallback acbd, out object state);

		// Token: 0x06005AB6 RID: 23222
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern object GetThisPtr();

		// Token: 0x06005AB7 RID: 23223
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern IAsyncResult GetAsyncResult();

		// Token: 0x06005AB8 RID: 23224 RVA: 0x0013F69A File Offset: 0x0013D89A
		public void Init()
		{
		}

		// Token: 0x06005AB9 RID: 23225
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern object GetReturnValue();

		// Token: 0x06005ABA RID: 23226 RVA: 0x0013F69C File Offset: 0x0013D89C
		internal Message()
		{
		}

		// Token: 0x06005ABB RID: 23227 RVA: 0x0013F6A4 File Offset: 0x0013D8A4
		[SecurityCritical]
		internal void InitFields(MessageData msgData)
		{
			this._frame = msgData.pFrame;
			this._delegateMD = msgData.pDelegateMD;
			this._methodDesc = msgData.pMethodDesc;
			this._flags = msgData.iFlags;
			this._initDone = true;
			this._metaSigHolder = msgData.pSig;
			this._governingType = msgData.thGoverningType;
			this._MethodName = null;
			this._MethodSignature = null;
			this._MethodBase = null;
			this._URI = null;
			this._Fault = null;
			this._ID = null;
			this._srvID = null;
			this._callContext = null;
			if (this._properties != null)
			{
				((IDictionary)this._properties).Clear();
			}
		}

		// Token: 0x06005ABC RID: 23228 RVA: 0x0013F750 File Offset: 0x0013D950
		private void InitIfNecessary()
		{
			if (!this._initDone)
			{
				this.Init();
				this._initDone = true;
			}
		}

		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x06005ABD RID: 23229 RVA: 0x0013F767 File Offset: 0x0013D967
		// (set) Token: 0x06005ABE RID: 23230 RVA: 0x0013F76F File Offset: 0x0013D96F
		ServerIdentity IInternalMessage.ServerIdentityObject
		{
			[SecurityCritical]
			get
			{
				return this._srvID;
			}
			[SecurityCritical]
			set
			{
				this._srvID = value;
			}
		}

		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x06005ABF RID: 23231 RVA: 0x0013F778 File Offset: 0x0013D978
		// (set) Token: 0x06005AC0 RID: 23232 RVA: 0x0013F780 File Offset: 0x0013D980
		Identity IInternalMessage.IdentityObject
		{
			[SecurityCritical]
			get
			{
				return this._ID;
			}
			[SecurityCritical]
			set
			{
				this._ID = value;
			}
		}

		// Token: 0x06005AC1 RID: 23233 RVA: 0x0013F789 File Offset: 0x0013D989
		[SecurityCritical]
		void IInternalMessage.SetURI(string URI)
		{
			this._URI = URI;
		}

		// Token: 0x06005AC2 RID: 23234 RVA: 0x0013F792 File Offset: 0x0013D992
		[SecurityCritical]
		void IInternalMessage.SetCallContext(LogicalCallContext callContext)
		{
			this._callContext = callContext;
		}

		// Token: 0x06005AC3 RID: 23235 RVA: 0x0013F79B File Offset: 0x0013D99B
		[SecurityCritical]
		bool IInternalMessage.HasProperties()
		{
			return this._properties != null;
		}

		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x06005AC4 RID: 23236 RVA: 0x0013F7A6 File Offset: 0x0013D9A6
		public IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				if (this._properties == null)
				{
					Interlocked.CompareExchange(ref this._properties, new MCMDictionary(this, null), null);
				}
				return (IDictionary)this._properties;
			}
		}

		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x06005AC5 RID: 23237 RVA: 0x0013F7CF File Offset: 0x0013D9CF
		// (set) Token: 0x06005AC6 RID: 23238 RVA: 0x0013F7D7 File Offset: 0x0013D9D7
		public string Uri
		{
			[SecurityCritical]
			get
			{
				return this._URI;
			}
			set
			{
				this._URI = value;
			}
		}

		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x06005AC7 RID: 23239 RVA: 0x0013F7E0 File Offset: 0x0013D9E0
		public bool HasVarArgs
		{
			[SecurityCritical]
			get
			{
				if ((this._flags & 16) == 0 && (this._flags & 32) == 0)
				{
					if (!this.InternalHasVarArgs())
					{
						this._flags |= 16;
					}
					else
					{
						this._flags |= 32;
					}
				}
				return 1 == (this._flags & 32);
			}
		}

		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x06005AC8 RID: 23240 RVA: 0x0013F837 File Offset: 0x0013DA37
		public int ArgCount
		{
			[SecurityCritical]
			get
			{
				return this.InternalGetArgCount();
			}
		}

		// Token: 0x06005AC9 RID: 23241 RVA: 0x0013F83F File Offset: 0x0013DA3F
		[SecurityCritical]
		public object GetArg(int argNum)
		{
			return this.InternalGetArg(argNum);
		}

		// Token: 0x06005ACA RID: 23242 RVA: 0x0013F848 File Offset: 0x0013DA48
		[SecurityCritical]
		public string GetArgName(int index)
		{
			if (index >= this.ArgCount)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(this.GetMethodBase());
			ParameterInfo[] parameters = reflectionCachedData.Parameters;
			if (index < parameters.Length)
			{
				return parameters[index].Name;
			}
			return "VarArg" + (index - parameters.Length).ToString();
		}

		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x06005ACB RID: 23243 RVA: 0x0013F8A2 File Offset: 0x0013DAA2
		public object[] Args
		{
			[SecurityCritical]
			get
			{
				return this.InternalGetArgs();
			}
		}

		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x06005ACC RID: 23244 RVA: 0x0013F8AA File Offset: 0x0013DAAA
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

		// Token: 0x06005ACD RID: 23245 RVA: 0x0013F8CC File Offset: 0x0013DACC
		[SecurityCritical]
		public object GetInArg(int argNum)
		{
			if (this._argMapper == null)
			{
				this._argMapper = new ArgMapper(this, false);
			}
			return this._argMapper.GetArg(argNum);
		}

		// Token: 0x06005ACE RID: 23246 RVA: 0x0013F8EF File Offset: 0x0013DAEF
		[SecurityCritical]
		public string GetInArgName(int index)
		{
			if (this._argMapper == null)
			{
				this._argMapper = new ArgMapper(this, false);
			}
			return this._argMapper.GetArgName(index);
		}

		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x06005ACF RID: 23247 RVA: 0x0013F912 File Offset: 0x0013DB12
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

		// Token: 0x06005AD0 RID: 23248 RVA: 0x0013F934 File Offset: 0x0013DB34
		[SecurityCritical]
		private void UpdateNames()
		{
			RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(this.GetMethodBase());
			this._typeName = reflectionCachedData.TypeAndAssemblyName;
			this._MethodName = reflectionCachedData.MethodName;
		}

		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x06005AD1 RID: 23249 RVA: 0x0013F965 File Offset: 0x0013DB65
		public string MethodName
		{
			[SecurityCritical]
			get
			{
				if (this._MethodName == null)
				{
					this.UpdateNames();
				}
				return this._MethodName;
			}
		}

		// Token: 0x17000F3F RID: 3903
		// (get) Token: 0x06005AD2 RID: 23250 RVA: 0x0013F97B File Offset: 0x0013DB7B
		public string TypeName
		{
			[SecurityCritical]
			get
			{
				if (this._typeName == null)
				{
					this.UpdateNames();
				}
				return this._typeName;
			}
		}

		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x06005AD3 RID: 23251 RVA: 0x0013F991 File Offset: 0x0013DB91
		public object MethodSignature
		{
			[SecurityCritical]
			get
			{
				if (this._MethodSignature == null)
				{
					this._MethodSignature = Message.GenerateMethodSignature(this.GetMethodBase());
				}
				return this._MethodSignature;
			}
		}

		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x06005AD4 RID: 23252 RVA: 0x0013F9B2 File Offset: 0x0013DBB2
		public LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				return this.GetLogicalCallContext();
			}
		}

		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x06005AD5 RID: 23253 RVA: 0x0013F9BA File Offset: 0x0013DBBA
		public MethodBase MethodBase
		{
			[SecurityCritical]
			get
			{
				return this.GetMethodBase();
			}
		}

		// Token: 0x06005AD6 RID: 23254 RVA: 0x0013F9C2 File Offset: 0x0013DBC2
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
		}

		// Token: 0x06005AD7 RID: 23255 RVA: 0x0013F9D4 File Offset: 0x0013DBD4
		[SecurityCritical]
		internal MethodBase GetMethodBase()
		{
			if (null == this._MethodBase)
			{
				IRuntimeMethodInfo runtimeMethodInfo = new RuntimeMethodInfoStub(this._methodDesc, null);
				this._MethodBase = RuntimeType.GetMethodBase(Type.GetTypeFromHandleUnsafe(this._governingType), runtimeMethodInfo);
			}
			return this._MethodBase;
		}

		// Token: 0x06005AD8 RID: 23256 RVA: 0x0013FA1C File Offset: 0x0013DC1C
		[SecurityCritical]
		internal LogicalCallContext SetLogicalCallContext(LogicalCallContext callCtx)
		{
			LogicalCallContext callContext = this._callContext;
			this._callContext = callCtx;
			return callContext;
		}

		// Token: 0x06005AD9 RID: 23257 RVA: 0x0013FA38 File Offset: 0x0013DC38
		[SecurityCritical]
		internal LogicalCallContext GetLogicalCallContext()
		{
			if (this._callContext == null)
			{
				this._callContext = new LogicalCallContext();
			}
			return this._callContext;
		}

		// Token: 0x06005ADA RID: 23258 RVA: 0x0013FA54 File Offset: 0x0013DC54
		[SecurityCritical]
		internal static Type[] GenerateMethodSignature(MethodBase mb)
		{
			RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(mb);
			ParameterInfo[] parameters = reflectionCachedData.Parameters;
			Type[] array = new Type[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				array[i] = parameters[i].ParameterType;
			}
			return array;
		}

		// Token: 0x06005ADB RID: 23259 RVA: 0x0013FA94 File Offset: 0x0013DC94
		[SecurityCritical]
		internal static object[] CoerceArgs(IMethodMessage m)
		{
			MethodBase methodBase = m.MethodBase;
			RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(methodBase);
			return Message.CoerceArgs(m, reflectionCachedData.Parameters);
		}

		// Token: 0x06005ADC RID: 23260 RVA: 0x0013FABB File Offset: 0x0013DCBB
		[SecurityCritical]
		internal static object[] CoerceArgs(IMethodMessage m, ParameterInfo[] pi)
		{
			return Message.CoerceArgs(m.MethodBase, m.Args, pi);
		}

		// Token: 0x06005ADD RID: 23261 RVA: 0x0013FAD0 File Offset: 0x0013DCD0
		[SecurityCritical]
		internal static object[] CoerceArgs(MethodBase mb, object[] args, ParameterInfo[] pi)
		{
			if (pi == null)
			{
				throw new ArgumentNullException("pi");
			}
			if (pi.Length != args.Length)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_ArgMismatch"), new object[]
				{
					mb.DeclaringType.FullName,
					mb.Name,
					args.Length,
					pi.Length
				}));
			}
			for (int i = 0; i < pi.Length; i++)
			{
				ParameterInfo parameterInfo = pi[i];
				Type parameterType = parameterInfo.ParameterType;
				object obj = args[i];
				if (obj != null)
				{
					args[i] = Message.CoerceArg(obj, parameterType);
				}
				else if (parameterType.IsByRef)
				{
					Type elementType = parameterType.GetElementType();
					if (elementType.IsValueType)
					{
						if (parameterInfo.IsOut)
						{
							args[i] = Activator.CreateInstance(elementType, true);
						}
						else if (!elementType.IsGenericType || !(elementType.GetGenericTypeDefinition() == typeof(Nullable<>)))
						{
							throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_MissingArgValue"), elementType.FullName, i));
						}
					}
				}
				else if (parameterType.IsValueType && (!parameterType.IsGenericType || !(parameterType.GetGenericTypeDefinition() == typeof(Nullable<>))))
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_MissingArgValue"), parameterType.FullName, i));
				}
			}
			return args;
		}

		// Token: 0x06005ADE RID: 23262 RVA: 0x0013FC40 File Offset: 0x0013DE40
		[SecurityCritical]
		internal static object CoerceArg(object value, Type pt)
		{
			object obj = null;
			if (value != null)
			{
				Exception ex = null;
				try
				{
					if (pt.IsByRef)
					{
						pt = pt.GetElementType();
					}
					if (pt.IsInstanceOfType(value))
					{
						obj = value;
					}
					else
					{
						obj = Convert.ChangeType(value, pt, CultureInfo.InvariantCulture);
					}
				}
				catch (Exception ex2)
				{
					ex = ex2;
				}
				if (obj == null)
				{
					string text;
					if (RemotingServices.IsTransparentProxy(value))
					{
						text = typeof(MarshalByRefObject).ToString();
					}
					else
					{
						text = value.ToString();
					}
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_CoercionFailed"), text, pt), ex);
				}
			}
			return obj;
		}

		// Token: 0x06005ADF RID: 23263 RVA: 0x0013FCDC File Offset: 0x0013DEDC
		[SecurityCritical]
		internal static object SoapCoerceArg(object value, Type pt, Hashtable keyToNamespaceTable)
		{
			object obj = null;
			if (value != null)
			{
				try
				{
					if (pt.IsByRef)
					{
						pt = pt.GetElementType();
					}
					if (pt.IsInstanceOfType(value))
					{
						obj = value;
					}
					else
					{
						string text = value as string;
						if (text != null)
						{
							if (pt == typeof(double))
							{
								if (text == "INF")
								{
									obj = double.PositiveInfinity;
								}
								else if (text == "-INF")
								{
									obj = double.NegativeInfinity;
								}
								else
								{
									obj = double.Parse(text, CultureInfo.InvariantCulture);
								}
							}
							else if (pt == typeof(float))
							{
								if (text == "INF")
								{
									obj = float.PositiveInfinity;
								}
								else if (text == "-INF")
								{
									obj = float.NegativeInfinity;
								}
								else
								{
									obj = float.Parse(text, CultureInfo.InvariantCulture);
								}
							}
							else if (SoapType.typeofISoapXsd.IsAssignableFrom(pt))
							{
								if (pt == SoapType.typeofSoapTime)
								{
									obj = SoapTime.Parse(text);
								}
								else if (pt == SoapType.typeofSoapDate)
								{
									obj = SoapDate.Parse(text);
								}
								else if (pt == SoapType.typeofSoapYearMonth)
								{
									obj = SoapYearMonth.Parse(text);
								}
								else if (pt == SoapType.typeofSoapYear)
								{
									obj = SoapYear.Parse(text);
								}
								else if (pt == SoapType.typeofSoapMonthDay)
								{
									obj = SoapMonthDay.Parse(text);
								}
								else if (pt == SoapType.typeofSoapDay)
								{
									obj = SoapDay.Parse(text);
								}
								else if (pt == SoapType.typeofSoapMonth)
								{
									obj = SoapMonth.Parse(text);
								}
								else if (pt == SoapType.typeofSoapHexBinary)
								{
									obj = SoapHexBinary.Parse(text);
								}
								else if (pt == SoapType.typeofSoapBase64Binary)
								{
									obj = SoapBase64Binary.Parse(text);
								}
								else if (pt == SoapType.typeofSoapInteger)
								{
									obj = SoapInteger.Parse(text);
								}
								else if (pt == SoapType.typeofSoapPositiveInteger)
								{
									obj = SoapPositiveInteger.Parse(text);
								}
								else if (pt == SoapType.typeofSoapNonPositiveInteger)
								{
									obj = SoapNonPositiveInteger.Parse(text);
								}
								else if (pt == SoapType.typeofSoapNonNegativeInteger)
								{
									obj = SoapNonNegativeInteger.Parse(text);
								}
								else if (pt == SoapType.typeofSoapNegativeInteger)
								{
									obj = SoapNegativeInteger.Parse(text);
								}
								else if (pt == SoapType.typeofSoapAnyUri)
								{
									obj = SoapAnyUri.Parse(text);
								}
								else if (pt == SoapType.typeofSoapQName)
								{
									obj = SoapQName.Parse(text);
									SoapQName soapQName = (SoapQName)obj;
									if (soapQName.Key.Length == 0)
									{
										soapQName.Namespace = (string)keyToNamespaceTable["xmlns"];
									}
									else
									{
										soapQName.Namespace = (string)keyToNamespaceTable["xmlns:" + soapQName.Key];
									}
								}
								else if (pt == SoapType.typeofSoapNotation)
								{
									obj = SoapNotation.Parse(text);
								}
								else if (pt == SoapType.typeofSoapNormalizedString)
								{
									obj = SoapNormalizedString.Parse(text);
								}
								else if (pt == SoapType.typeofSoapToken)
								{
									obj = SoapToken.Parse(text);
								}
								else if (pt == SoapType.typeofSoapLanguage)
								{
									obj = SoapLanguage.Parse(text);
								}
								else if (pt == SoapType.typeofSoapName)
								{
									obj = SoapName.Parse(text);
								}
								else if (pt == SoapType.typeofSoapIdrefs)
								{
									obj = SoapIdrefs.Parse(text);
								}
								else if (pt == SoapType.typeofSoapEntities)
								{
									obj = SoapEntities.Parse(text);
								}
								else if (pt == SoapType.typeofSoapNmtoken)
								{
									obj = SoapNmtoken.Parse(text);
								}
								else if (pt == SoapType.typeofSoapNmtokens)
								{
									obj = SoapNmtokens.Parse(text);
								}
								else if (pt == SoapType.typeofSoapNcName)
								{
									obj = SoapNcName.Parse(text);
								}
								else if (pt == SoapType.typeofSoapId)
								{
									obj = SoapId.Parse(text);
								}
								else if (pt == SoapType.typeofSoapIdref)
								{
									obj = SoapIdref.Parse(text);
								}
								else if (pt == SoapType.typeofSoapEntity)
								{
									obj = SoapEntity.Parse(text);
								}
							}
							else if (pt == typeof(bool))
							{
								if (text == "1" || text == "true")
								{
									obj = true;
								}
								else
								{
									if (!(text == "0") && !(text == "false"))
									{
										throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_CoercionFailed"), text, pt));
									}
									obj = false;
								}
							}
							else if (pt == typeof(DateTime))
							{
								obj = SoapDateTime.Parse(text);
							}
							else if (pt.IsPrimitive)
							{
								obj = Convert.ChangeType(value, pt, CultureInfo.InvariantCulture);
							}
							else if (pt == typeof(TimeSpan))
							{
								obj = SoapDuration.Parse(text);
							}
							else if (pt == typeof(char))
							{
								obj = text[0];
							}
							else
							{
								obj = Convert.ChangeType(value, pt, CultureInfo.InvariantCulture);
							}
						}
						else
						{
							obj = Convert.ChangeType(value, pt, CultureInfo.InvariantCulture);
						}
					}
				}
				catch (Exception)
				{
				}
				if (obj == null)
				{
					string text2;
					if (RemotingServices.IsTransparentProxy(value))
					{
						text2 = typeof(MarshalByRefObject).ToString();
					}
					else
					{
						text2 = value.ToString();
					}
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_CoercionFailed"), text2, pt));
				}
			}
			return obj;
		}

		// Token: 0x06005AE0 RID: 23264
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool InternalHasVarArgs();

		// Token: 0x06005AE1 RID: 23265
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int InternalGetArgCount();

		// Token: 0x06005AE2 RID: 23266
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern object InternalGetArg(int argNum);

		// Token: 0x06005AE3 RID: 23267
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern object[] InternalGetArgs();

		// Token: 0x06005AE4 RID: 23268
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void PropagateOutParameters(object[] OutArgs, object retVal);

		// Token: 0x06005AE5 RID: 23269
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool Dispatch(object target);

		// Token: 0x06005AE6 RID: 23270 RVA: 0x00140294 File Offset: 0x0013E494
		[SecurityCritical]
		[Conditional("_REMOTING_DEBUG")]
		public static void DebugOut(string s)
		{
			Message.OutToUnmanagedDebugger("\nRMTING: Thrd " + Thread.CurrentThread.GetHashCode().ToString() + " : " + s);
		}

		// Token: 0x06005AE7 RID: 23271
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void OutToUnmanagedDebugger(string s);

		// Token: 0x06005AE8 RID: 23272 RVA: 0x001402C8 File Offset: 0x0013E4C8
		[SecurityCritical]
		internal static LogicalCallContext PropagateCallContextFromMessageToThread(IMessage msg)
		{
			return CallContext.SetLogicalCallContext((LogicalCallContext)msg.Properties[Message.CallContextKey]);
		}

		// Token: 0x06005AE9 RID: 23273 RVA: 0x001402E4 File Offset: 0x0013E4E4
		[SecurityCritical]
		internal static void PropagateCallContextFromThreadToMessage(IMessage msg)
		{
			LogicalCallContext logicalCallContext = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext;
			msg.Properties[Message.CallContextKey] = logicalCallContext;
		}

		// Token: 0x06005AEA RID: 23274 RVA: 0x00140312 File Offset: 0x0013E512
		[SecurityCritical]
		internal static void PropagateCallContextFromThreadToMessage(IMessage msg, LogicalCallContext oldcctx)
		{
			Message.PropagateCallContextFromThreadToMessage(msg);
			CallContext.SetLogicalCallContext(oldcctx);
		}

		// Token: 0x04002915 RID: 10517
		internal const int Sync = 0;

		// Token: 0x04002916 RID: 10518
		internal const int BeginAsync = 1;

		// Token: 0x04002917 RID: 10519
		internal const int EndAsync = 2;

		// Token: 0x04002918 RID: 10520
		internal const int Ctor = 4;

		// Token: 0x04002919 RID: 10521
		internal const int OneWay = 8;

		// Token: 0x0400291A RID: 10522
		internal const int CallMask = 15;

		// Token: 0x0400291B RID: 10523
		internal const int FixedArgs = 16;

		// Token: 0x0400291C RID: 10524
		internal const int VarArgs = 32;

		// Token: 0x0400291D RID: 10525
		private string _MethodName;

		// Token: 0x0400291E RID: 10526
		private Type[] _MethodSignature;

		// Token: 0x0400291F RID: 10527
		private MethodBase _MethodBase;

		// Token: 0x04002920 RID: 10528
		private object _properties;

		// Token: 0x04002921 RID: 10529
		private string _URI;

		// Token: 0x04002922 RID: 10530
		private string _typeName;

		// Token: 0x04002923 RID: 10531
		private Exception _Fault;

		// Token: 0x04002924 RID: 10532
		private Identity _ID;

		// Token: 0x04002925 RID: 10533
		private ServerIdentity _srvID;

		// Token: 0x04002926 RID: 10534
		private ArgMapper _argMapper;

		// Token: 0x04002927 RID: 10535
		[SecurityCritical]
		private LogicalCallContext _callContext;

		// Token: 0x04002928 RID: 10536
		private IntPtr _frame;

		// Token: 0x04002929 RID: 10537
		private IntPtr _methodDesc;

		// Token: 0x0400292A RID: 10538
		private IntPtr _metaSigHolder;

		// Token: 0x0400292B RID: 10539
		private IntPtr _delegateMD;

		// Token: 0x0400292C RID: 10540
		private IntPtr _governingType;

		// Token: 0x0400292D RID: 10541
		private int _flags;

		// Token: 0x0400292E RID: 10542
		private bool _initDone;

		// Token: 0x0400292F RID: 10543
		internal static string CallContextKey = "__CallContext";

		// Token: 0x04002930 RID: 10544
		internal static string UriKey = "__Uri";
	}
}
