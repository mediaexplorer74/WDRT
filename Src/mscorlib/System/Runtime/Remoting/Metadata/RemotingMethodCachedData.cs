using System;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Metadata
{
	// Token: 0x020007D2 RID: 2002
	internal class RemotingMethodCachedData : RemotingCachedData
	{
		// Token: 0x060056F1 RID: 22257 RVA: 0x00135D8B File Offset: 0x00133F8B
		internal RemotingMethodCachedData(RuntimeMethodInfo ri)
		{
			this.RI = ri;
		}

		// Token: 0x060056F2 RID: 22258 RVA: 0x00135D9A File Offset: 0x00133F9A
		internal RemotingMethodCachedData(RuntimeConstructorInfo ri)
		{
			this.RI = ri;
		}

		// Token: 0x060056F3 RID: 22259 RVA: 0x00135DAC File Offset: 0x00133FAC
		internal override SoapAttribute GetSoapAttributeNoLock()
		{
			object[] customAttributes = this.RI.GetCustomAttributes(typeof(SoapMethodAttribute), true);
			SoapAttribute soapAttribute;
			if (customAttributes != null && customAttributes.Length != 0)
			{
				soapAttribute = (SoapAttribute)customAttributes[0];
			}
			else
			{
				soapAttribute = new SoapMethodAttribute();
			}
			soapAttribute.SetReflectInfo(this.RI);
			return soapAttribute;
		}

		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x060056F4 RID: 22260 RVA: 0x00135DF7 File Offset: 0x00133FF7
		internal string TypeAndAssemblyName
		{
			[SecurityCritical]
			get
			{
				if (this._typeAndAssemblyName == null)
				{
					this.UpdateNames();
				}
				return this._typeAndAssemblyName;
			}
		}

		// Token: 0x17000E44 RID: 3652
		// (get) Token: 0x060056F5 RID: 22261 RVA: 0x00135E0D File Offset: 0x0013400D
		internal string MethodName
		{
			[SecurityCritical]
			get
			{
				if (this._methodName == null)
				{
					this.UpdateNames();
				}
				return this._methodName;
			}
		}

		// Token: 0x060056F6 RID: 22262 RVA: 0x00135E24 File Offset: 0x00134024
		[SecurityCritical]
		private void UpdateNames()
		{
			MethodBase ri = this.RI;
			this._methodName = ri.Name;
			if (ri.DeclaringType != null)
			{
				this._typeAndAssemblyName = RemotingServices.GetDefaultQualifiedTypeName((RuntimeType)ri.DeclaringType);
			}
		}

		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x060056F7 RID: 22263 RVA: 0x00135E68 File Offset: 0x00134068
		internal ParameterInfo[] Parameters
		{
			get
			{
				if (this._parameters == null)
				{
					this._parameters = this.RI.GetParameters();
				}
				return this._parameters;
			}
		}

		// Token: 0x17000E46 RID: 3654
		// (get) Token: 0x060056F8 RID: 22264 RVA: 0x00135E89 File Offset: 0x00134089
		internal int[] OutRefArgMap
		{
			get
			{
				if (this._outRefArgMap == null)
				{
					this.GetArgMaps();
				}
				return this._outRefArgMap;
			}
		}

		// Token: 0x17000E47 RID: 3655
		// (get) Token: 0x060056F9 RID: 22265 RVA: 0x00135E9F File Offset: 0x0013409F
		internal int[] OutOnlyArgMap
		{
			get
			{
				if (this._outOnlyArgMap == null)
				{
					this.GetArgMaps();
				}
				return this._outOnlyArgMap;
			}
		}

		// Token: 0x17000E48 RID: 3656
		// (get) Token: 0x060056FA RID: 22266 RVA: 0x00135EB5 File Offset: 0x001340B5
		internal int[] NonRefOutArgMap
		{
			get
			{
				if (this._nonRefOutArgMap == null)
				{
					this.GetArgMaps();
				}
				return this._nonRefOutArgMap;
			}
		}

		// Token: 0x17000E49 RID: 3657
		// (get) Token: 0x060056FB RID: 22267 RVA: 0x00135ECB File Offset: 0x001340CB
		internal int[] MarshalRequestArgMap
		{
			get
			{
				if (this._marshalRequestMap == null)
				{
					this.GetArgMaps();
				}
				return this._marshalRequestMap;
			}
		}

		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x060056FC RID: 22268 RVA: 0x00135EE1 File Offset: 0x001340E1
		internal int[] MarshalResponseArgMap
		{
			get
			{
				if (this._marshalResponseMap == null)
				{
					this.GetArgMaps();
				}
				return this._marshalResponseMap;
			}
		}

		// Token: 0x060056FD RID: 22269 RVA: 0x00135EF8 File Offset: 0x001340F8
		private void GetArgMaps()
		{
			lock (this)
			{
				if (this._inRefArgMap == null)
				{
					int[] array = null;
					int[] array2 = null;
					int[] array3 = null;
					int[] array4 = null;
					int[] array5 = null;
					int[] array6 = null;
					ArgMapper.GetParameterMaps(this.Parameters, out array, out array2, out array3, out array4, out array5, out array6);
					this._inRefArgMap = array;
					this._outRefArgMap = array2;
					this._outOnlyArgMap = array3;
					this._nonRefOutArgMap = array4;
					this._marshalRequestMap = array5;
					this._marshalResponseMap = array6;
				}
			}
		}

		// Token: 0x060056FE RID: 22270 RVA: 0x00135F8C File Offset: 0x0013418C
		internal bool IsOneWayMethod()
		{
			if ((this.flags & RemotingMethodCachedData.MethodCacheFlags.CheckedOneWay) == RemotingMethodCachedData.MethodCacheFlags.None)
			{
				RemotingMethodCachedData.MethodCacheFlags methodCacheFlags = RemotingMethodCachedData.MethodCacheFlags.CheckedOneWay;
				object[] customAttributes = this.RI.GetCustomAttributes(typeof(OneWayAttribute), true);
				if (customAttributes != null && customAttributes.Length != 0)
				{
					methodCacheFlags |= RemotingMethodCachedData.MethodCacheFlags.IsOneWay;
				}
				this.flags |= methodCacheFlags;
				return (methodCacheFlags & RemotingMethodCachedData.MethodCacheFlags.IsOneWay) > RemotingMethodCachedData.MethodCacheFlags.None;
			}
			return (this.flags & RemotingMethodCachedData.MethodCacheFlags.IsOneWay) > RemotingMethodCachedData.MethodCacheFlags.None;
		}

		// Token: 0x060056FF RID: 22271 RVA: 0x00135FE8 File Offset: 0x001341E8
		internal bool IsOverloaded()
		{
			if ((this.flags & RemotingMethodCachedData.MethodCacheFlags.CheckedOverloaded) == RemotingMethodCachedData.MethodCacheFlags.None)
			{
				RemotingMethodCachedData.MethodCacheFlags methodCacheFlags = RemotingMethodCachedData.MethodCacheFlags.CheckedOverloaded;
				MethodBase ri = this.RI;
				RuntimeMethodInfo runtimeMethodInfo;
				if ((runtimeMethodInfo = ri as RuntimeMethodInfo) != null)
				{
					if (runtimeMethodInfo.IsOverloaded)
					{
						methodCacheFlags |= RemotingMethodCachedData.MethodCacheFlags.IsOverloaded;
					}
				}
				else
				{
					RuntimeConstructorInfo runtimeConstructorInfo;
					if (!((runtimeConstructorInfo = ri as RuntimeConstructorInfo) != null))
					{
						throw new NotSupportedException(Environment.GetResourceString("InvalidOperation_Method"));
					}
					if (runtimeConstructorInfo.IsOverloaded)
					{
						methodCacheFlags |= RemotingMethodCachedData.MethodCacheFlags.IsOverloaded;
					}
				}
				this.flags |= methodCacheFlags;
			}
			return (this.flags & RemotingMethodCachedData.MethodCacheFlags.IsOverloaded) > RemotingMethodCachedData.MethodCacheFlags.None;
		}

		// Token: 0x17000E4B RID: 3659
		// (get) Token: 0x06005700 RID: 22272 RVA: 0x00136074 File Offset: 0x00134274
		internal Type ReturnType
		{
			get
			{
				if ((this.flags & RemotingMethodCachedData.MethodCacheFlags.CheckedForReturnType) == RemotingMethodCachedData.MethodCacheFlags.None)
				{
					MethodInfo methodInfo = this.RI as MethodInfo;
					if (methodInfo != null)
					{
						Type returnType = methodInfo.ReturnType;
						if (returnType != typeof(void))
						{
							this._returnType = returnType;
						}
					}
					this.flags |= RemotingMethodCachedData.MethodCacheFlags.CheckedForReturnType;
				}
				return this._returnType;
			}
		}

		// Token: 0x040027C6 RID: 10182
		private MethodBase RI;

		// Token: 0x040027C7 RID: 10183
		private ParameterInfo[] _parameters;

		// Token: 0x040027C8 RID: 10184
		private RemotingMethodCachedData.MethodCacheFlags flags;

		// Token: 0x040027C9 RID: 10185
		private string _typeAndAssemblyName;

		// Token: 0x040027CA RID: 10186
		private string _methodName;

		// Token: 0x040027CB RID: 10187
		private Type _returnType;

		// Token: 0x040027CC RID: 10188
		private int[] _inRefArgMap;

		// Token: 0x040027CD RID: 10189
		private int[] _outRefArgMap;

		// Token: 0x040027CE RID: 10190
		private int[] _outOnlyArgMap;

		// Token: 0x040027CF RID: 10191
		private int[] _nonRefOutArgMap;

		// Token: 0x040027D0 RID: 10192
		private int[] _marshalRequestMap;

		// Token: 0x040027D1 RID: 10193
		private int[] _marshalResponseMap;

		// Token: 0x02000C69 RID: 3177
		[Flags]
		[Serializable]
		private enum MethodCacheFlags
		{
			// Token: 0x040037E3 RID: 14307
			None = 0,
			// Token: 0x040037E4 RID: 14308
			CheckedOneWay = 1,
			// Token: 0x040037E5 RID: 14309
			IsOneWay = 2,
			// Token: 0x040037E6 RID: 14310
			CheckedOverloaded = 4,
			// Token: 0x040037E7 RID: 14311
			IsOverloaded = 8,
			// Token: 0x040037E8 RID: 14312
			CheckedForAsync = 16,
			// Token: 0x040037E9 RID: 14313
			CheckedForReturnType = 32
		}
	}
}
