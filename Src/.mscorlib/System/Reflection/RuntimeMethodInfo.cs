using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace System.Reflection
{
	// Token: 0x02000607 RID: 1543
	[Serializable]
	internal sealed class RuntimeMethodInfo : MethodInfo, ISerializable, IRuntimeMethodInfo
	{
		// Token: 0x0600475D RID: 18269 RVA: 0x00105460 File Offset: 0x00103660
		private bool IsNonW8PFrameworkAPI()
		{
			if (this.m_declaringType.IsArray && base.IsPublic && !base.IsStatic)
			{
				return false;
			}
			RuntimeAssembly runtimeAssembly = this.GetRuntimeAssembly();
			if (runtimeAssembly.IsFrameworkAssembly())
			{
				int invocableAttributeCtorToken = runtimeAssembly.InvocableAttributeCtorToken;
				if (System.Reflection.MetadataToken.IsNullToken(invocableAttributeCtorToken) || !CustomAttribute.IsAttributeDefined(this.GetRuntimeModule(), this.MetadataToken, invocableAttributeCtorToken))
				{
					return true;
				}
			}
			if (this.GetRuntimeType().IsNonW8PFrameworkAPI())
			{
				return true;
			}
			if (this.IsGenericMethod && !this.IsGenericMethodDefinition)
			{
				foreach (Type type in this.GetGenericArguments())
				{
					if (((RuntimeType)type).IsNonW8PFrameworkAPI())
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x0600475E RID: 18270 RVA: 0x0010550A File Offset: 0x0010370A
		internal override bool IsDynamicallyInvokable
		{
			get
			{
				return !AppDomain.ProfileAPICheck || !this.IsNonW8PFrameworkAPI();
			}
		}

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x0600475F RID: 18271 RVA: 0x00105520 File Offset: 0x00103720
		internal INVOCATION_FLAGS InvocationFlags
		{
			[SecuritySafeCritical]
			get
			{
				if ((this.m_invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_INITIALIZED) == INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
				{
					Type declaringType = this.DeclaringType;
					INVOCATION_FLAGS invocation_FLAGS;
					if (this.ContainsGenericParameters || this.ReturnType.IsByRef || (declaringType != null && declaringType.ContainsGenericParameters) || (this.CallingConvention & CallingConventions.VarArgs) == CallingConventions.VarArgs || (this.Attributes & MethodAttributes.RequireSecObject) == MethodAttributes.RequireSecObject)
					{
						invocation_FLAGS = INVOCATION_FLAGS.INVOCATION_FLAGS_NO_INVOKE;
					}
					else
					{
						invocation_FLAGS = RuntimeMethodHandle.GetSecurityFlags(this);
						if ((invocation_FLAGS & INVOCATION_FLAGS.INVOCATION_FLAGS_NEED_SECURITY) == INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
						{
							if ((this.Attributes & MethodAttributes.MemberAccessMask) != MethodAttributes.Public || (declaringType != null && declaringType.NeedsReflectionSecurityCheck))
							{
								invocation_FLAGS |= INVOCATION_FLAGS.INVOCATION_FLAGS_NEED_SECURITY;
							}
							else if (this.IsGenericMethod)
							{
								Type[] genericArguments = this.GetGenericArguments();
								for (int i = 0; i < genericArguments.Length; i++)
								{
									if (genericArguments[i].NeedsReflectionSecurityCheck)
									{
										invocation_FLAGS |= INVOCATION_FLAGS.INVOCATION_FLAGS_NEED_SECURITY;
										break;
									}
								}
							}
						}
					}
					if (AppDomain.ProfileAPICheck && this.IsNonW8PFrameworkAPI())
					{
						invocation_FLAGS |= INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API;
					}
					this.m_invocationFlags = invocation_FLAGS | INVOCATION_FLAGS.INVOCATION_FLAGS_INITIALIZED;
				}
				return this.m_invocationFlags;
			}
		}

		// Token: 0x06004760 RID: 18272 RVA: 0x00105609 File Offset: 0x00103809
		[SecurityCritical]
		internal RuntimeMethodInfo(RuntimeMethodHandleInternal handle, RuntimeType declaringType, RuntimeType.RuntimeTypeCache reflectedTypeCache, MethodAttributes methodAttributes, BindingFlags bindingFlags, object keepalive)
		{
			this.m_bindingFlags = bindingFlags;
			this.m_declaringType = declaringType;
			this.m_keepalive = keepalive;
			this.m_handle = handle.Value;
			this.m_reflectedTypeCache = reflectedTypeCache;
			this.m_methodAttributes = methodAttributes;
		}

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06004761 RID: 18273 RVA: 0x00105644 File Offset: 0x00103844
		internal RemotingMethodCachedData RemotingCache
		{
			get
			{
				RemotingMethodCachedData remotingMethodCachedData = this.m_cachedData;
				if (remotingMethodCachedData == null)
				{
					remotingMethodCachedData = new RemotingMethodCachedData(this);
					RemotingMethodCachedData remotingMethodCachedData2 = Interlocked.CompareExchange<RemotingMethodCachedData>(ref this.m_cachedData, remotingMethodCachedData, null);
					if (remotingMethodCachedData2 != null)
					{
						remotingMethodCachedData = remotingMethodCachedData2;
					}
				}
				return remotingMethodCachedData;
			}
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x06004762 RID: 18274 RVA: 0x00105676 File Offset: 0x00103876
		RuntimeMethodHandleInternal IRuntimeMethodInfo.Value
		{
			[SecuritySafeCritical]
			get
			{
				return new RuntimeMethodHandleInternal(this.m_handle);
			}
		}

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06004763 RID: 18275 RVA: 0x00105683 File Offset: 0x00103883
		private RuntimeType ReflectedTypeInternal
		{
			get
			{
				return this.m_reflectedTypeCache.GetRuntimeType();
			}
		}

		// Token: 0x06004764 RID: 18276 RVA: 0x00105690 File Offset: 0x00103890
		[SecurityCritical]
		private ParameterInfo[] FetchNonReturnParameters()
		{
			if (this.m_parameters == null)
			{
				this.m_parameters = RuntimeParameterInfo.GetParameters(this, this, this.Signature);
			}
			return this.m_parameters;
		}

		// Token: 0x06004765 RID: 18277 RVA: 0x001056B3 File Offset: 0x001038B3
		[SecurityCritical]
		private ParameterInfo FetchReturnParameter()
		{
			if (this.m_returnParameter == null)
			{
				this.m_returnParameter = RuntimeParameterInfo.GetReturnParameter(this, this, this.Signature);
			}
			return this.m_returnParameter;
		}

		// Token: 0x06004766 RID: 18278 RVA: 0x001056D8 File Offset: 0x001038D8
		internal override string FormatNameAndSig(bool serialization)
		{
			StringBuilder stringBuilder = new StringBuilder(this.Name);
			TypeNameFormatFlags typeNameFormatFlags = (serialization ? TypeNameFormatFlags.FormatSerialization : TypeNameFormatFlags.FormatBasic);
			if (this.IsGenericMethod)
			{
				stringBuilder.Append(RuntimeMethodHandle.ConstructInstantiation(this, typeNameFormatFlags));
			}
			stringBuilder.Append("(");
			stringBuilder.Append(MethodBase.ConstructParameters(this.GetParameterTypes(), this.CallingConvention, serialization));
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x06004767 RID: 18279 RVA: 0x0010574C File Offset: 0x0010394C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal override bool CacheEquals(object o)
		{
			RuntimeMethodInfo runtimeMethodInfo = o as RuntimeMethodInfo;
			return runtimeMethodInfo != null && runtimeMethodInfo.m_handle == this.m_handle;
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x06004768 RID: 18280 RVA: 0x00105776 File Offset: 0x00103976
		internal Signature Signature
		{
			get
			{
				if (this.m_signature == null)
				{
					this.m_signature = new Signature(this, this.m_declaringType);
				}
				return this.m_signature;
			}
		}

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x06004769 RID: 18281 RVA: 0x00105798 File Offset: 0x00103998
		internal BindingFlags BindingFlags
		{
			get
			{
				return this.m_bindingFlags;
			}
		}

		// Token: 0x0600476A RID: 18282 RVA: 0x001057A0 File Offset: 0x001039A0
		internal RuntimeMethodHandle GetMethodHandle()
		{
			return new RuntimeMethodHandle(this);
		}

		// Token: 0x0600476B RID: 18283 RVA: 0x001057A8 File Offset: 0x001039A8
		[SecuritySafeCritical]
		internal RuntimeMethodInfo GetParentDefinition()
		{
			if (!base.IsVirtual || this.m_declaringType.IsInterface)
			{
				return null;
			}
			RuntimeType runtimeType = (RuntimeType)this.m_declaringType.BaseType;
			if (runtimeType == null)
			{
				return null;
			}
			int slot = RuntimeMethodHandle.GetSlot(this);
			if (RuntimeTypeHandle.GetNumVirtuals(runtimeType) <= slot)
			{
				return null;
			}
			return (RuntimeMethodInfo)RuntimeType.GetMethodBase(runtimeType, RuntimeTypeHandle.GetMethodAt(runtimeType, slot));
		}

		// Token: 0x0600476C RID: 18284 RVA: 0x0010580C File Offset: 0x00103A0C
		internal RuntimeType GetDeclaringTypeInternal()
		{
			return this.m_declaringType;
		}

		// Token: 0x0600476D RID: 18285 RVA: 0x00105814 File Offset: 0x00103A14
		public override string ToString()
		{
			if (this.m_toString == null)
			{
				this.m_toString = this.ReturnType.FormatTypeName() + " " + base.FormatNameAndSig();
			}
			return this.m_toString;
		}

		// Token: 0x0600476E RID: 18286 RVA: 0x00105845 File Offset: 0x00103A45
		public override int GetHashCode()
		{
			if (this.IsGenericMethod)
			{
				return ValueType.GetHashCodeOfPtr(this.m_handle);
			}
			return base.GetHashCode();
		}

		// Token: 0x0600476F RID: 18287 RVA: 0x00105864 File Offset: 0x00103A64
		[SecuritySafeCritical]
		public override bool Equals(object obj)
		{
			if (!this.IsGenericMethod)
			{
				return obj == this;
			}
			RuntimeMethodInfo runtimeMethodInfo = obj as RuntimeMethodInfo;
			if (runtimeMethodInfo == null || !runtimeMethodInfo.IsGenericMethod)
			{
				return false;
			}
			IRuntimeMethodInfo runtimeMethodInfo2 = RuntimeMethodHandle.StripMethodInstantiation(this);
			IRuntimeMethodInfo runtimeMethodInfo3 = RuntimeMethodHandle.StripMethodInstantiation(runtimeMethodInfo);
			if (runtimeMethodInfo2.Value.Value != runtimeMethodInfo3.Value.Value)
			{
				return false;
			}
			Type[] genericArguments = this.GetGenericArguments();
			Type[] genericArguments2 = runtimeMethodInfo.GetGenericArguments();
			if (genericArguments.Length != genericArguments2.Length)
			{
				return false;
			}
			for (int i = 0; i < genericArguments.Length; i++)
			{
				if (genericArguments[i] != genericArguments2[i])
				{
					return false;
				}
			}
			return !(this.DeclaringType != runtimeMethodInfo.DeclaringType) && !(this.ReflectedType != runtimeMethodInfo.ReflectedType);
		}

		// Token: 0x06004770 RID: 18288 RVA: 0x00105936 File Offset: 0x00103B36
		[SecuritySafeCritical]
		public override object[] GetCustomAttributes(bool inherit)
		{
			return CustomAttribute.GetCustomAttributes(this, typeof(object) as RuntimeType, inherit);
		}

		// Token: 0x06004771 RID: 18289 RVA: 0x00105950 File Offset: 0x00103B50
		[SecuritySafeCritical]
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "attributeType");
			}
			return CustomAttribute.GetCustomAttributes(this, runtimeType, inherit);
		}

		// Token: 0x06004772 RID: 18290 RVA: 0x001059A4 File Offset: 0x00103BA4
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "attributeType");
			}
			return CustomAttribute.IsDefined(this, runtimeType, inherit);
		}

		// Token: 0x06004773 RID: 18291 RVA: 0x001059F7 File Offset: 0x00103BF7
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributesInternal(this);
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x06004774 RID: 18292 RVA: 0x001059FF File Offset: 0x00103BFF
		public override string Name
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_name == null)
				{
					this.m_name = RuntimeMethodHandle.GetName(this);
				}
				return this.m_name;
			}
		}

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06004775 RID: 18293 RVA: 0x00105A1B File Offset: 0x00103C1B
		public override Type DeclaringType
		{
			get
			{
				if (this.m_reflectedTypeCache.IsGlobal)
				{
					return null;
				}
				return this.m_declaringType;
			}
		}

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x06004776 RID: 18294 RVA: 0x00105A32 File Offset: 0x00103C32
		public override Type ReflectedType
		{
			get
			{
				if (this.m_reflectedTypeCache.IsGlobal)
				{
					return null;
				}
				return this.m_reflectedTypeCache.GetRuntimeType();
			}
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06004777 RID: 18295 RVA: 0x00105A4E File Offset: 0x00103C4E
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Method;
			}
		}

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x06004778 RID: 18296 RVA: 0x00105A51 File Offset: 0x00103C51
		public override int MetadataToken
		{
			[SecuritySafeCritical]
			get
			{
				return RuntimeMethodHandle.GetMethodDef(this);
			}
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x06004779 RID: 18297 RVA: 0x00105A59 File Offset: 0x00103C59
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x0600477A RID: 18298 RVA: 0x00105A61 File Offset: 0x00103C61
		internal RuntimeType GetRuntimeType()
		{
			return this.m_declaringType;
		}

		// Token: 0x0600477B RID: 18299 RVA: 0x00105A69 File Offset: 0x00103C69
		internal RuntimeModule GetRuntimeModule()
		{
			return this.m_declaringType.GetRuntimeModule();
		}

		// Token: 0x0600477C RID: 18300 RVA: 0x00105A76 File Offset: 0x00103C76
		internal RuntimeAssembly GetRuntimeAssembly()
		{
			return this.GetRuntimeModule().GetRuntimeAssembly();
		}

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x0600477D RID: 18301 RVA: 0x00105A83 File Offset: 0x00103C83
		public override bool IsSecurityCritical
		{
			get
			{
				return RuntimeMethodHandle.IsSecurityCritical(this);
			}
		}

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x0600477E RID: 18302 RVA: 0x00105A8B File Offset: 0x00103C8B
		public override bool IsSecuritySafeCritical
		{
			get
			{
				return RuntimeMethodHandle.IsSecuritySafeCritical(this);
			}
		}

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x0600477F RID: 18303 RVA: 0x00105A93 File Offset: 0x00103C93
		public override bool IsSecurityTransparent
		{
			get
			{
				return RuntimeMethodHandle.IsSecurityTransparent(this);
			}
		}

		// Token: 0x06004780 RID: 18304 RVA: 0x00105A9B File Offset: 0x00103C9B
		[SecuritySafeCritical]
		internal override ParameterInfo[] GetParametersNoCopy()
		{
			this.FetchNonReturnParameters();
			return this.m_parameters;
		}

		// Token: 0x06004781 RID: 18305 RVA: 0x00105AAC File Offset: 0x00103CAC
		[SecuritySafeCritical]
		public override ParameterInfo[] GetParameters()
		{
			this.FetchNonReturnParameters();
			if (this.m_parameters.Length == 0)
			{
				return this.m_parameters;
			}
			ParameterInfo[] array = new ParameterInfo[this.m_parameters.Length];
			Array.Copy(this.m_parameters, array, this.m_parameters.Length);
			return array;
		}

		// Token: 0x06004782 RID: 18306 RVA: 0x00105AF3 File Offset: 0x00103CF3
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return RuntimeMethodHandle.GetImplAttributes(this);
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x06004783 RID: 18307 RVA: 0x00105AFB File Offset: 0x00103CFB
		internal bool IsOverloaded
		{
			get
			{
				return this.m_reflectedTypeCache.GetMethodList(RuntimeType.MemberListType.CaseSensitive, this.Name).Length > 1;
			}
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x06004784 RID: 18308 RVA: 0x00105B14 File Offset: 0x00103D14
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				Type declaringType = this.DeclaringType;
				if ((declaringType == null && this.Module.Assembly.ReflectionOnly) || declaringType is ReflectionOnlyType)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotAllowedInReflectionOnly"));
				}
				return new RuntimeMethodHandle(this);
			}
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x06004785 RID: 18309 RVA: 0x00105B61 File Offset: 0x00103D61
		public override MethodAttributes Attributes
		{
			get
			{
				return this.m_methodAttributes;
			}
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x06004786 RID: 18310 RVA: 0x00105B69 File Offset: 0x00103D69
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.Signature.CallingConvention;
			}
		}

		// Token: 0x06004787 RID: 18311 RVA: 0x00105B78 File Offset: 0x00103D78
		[SecuritySafeCritical]
		[ReflectionPermission(SecurityAction.Demand, Flags = ReflectionPermissionFlag.MemberAccess)]
		public override MethodBody GetMethodBody()
		{
			MethodBody methodBody = RuntimeMethodHandle.GetMethodBody(this, this.ReflectedTypeInternal);
			if (methodBody != null)
			{
				methodBody.m_methodBase = this;
			}
			return methodBody;
		}

		// Token: 0x06004788 RID: 18312 RVA: 0x00105B9D File Offset: 0x00103D9D
		private void CheckConsistency(object target)
		{
			if ((this.m_methodAttributes & MethodAttributes.Static) == MethodAttributes.Static || this.m_declaringType.IsInstanceOfType(target))
			{
				return;
			}
			if (target == null)
			{
				throw new TargetException(Environment.GetResourceString("RFLCT.Targ_StatMethReqTarg"));
			}
			throw new TargetException(Environment.GetResourceString("RFLCT.Targ_ITargMismatch"));
		}

		// Token: 0x06004789 RID: 18313 RVA: 0x00105BE0 File Offset: 0x00103DE0
		[SecuritySafeCritical]
		private void ThrowNoInvokeException()
		{
			Type declaringType = this.DeclaringType;
			if ((declaringType == null && this.Module.Assembly.ReflectionOnly) || declaringType is ReflectionOnlyType)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Arg_ReflectionOnlyInvoke"));
			}
			if ((this.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_CONTAINS_STACK_POINTERS) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
			{
				throw new NotSupportedException();
			}
			if ((this.CallingConvention & CallingConventions.VarArgs) == CallingConventions.VarArgs)
			{
				throw new NotSupportedException();
			}
			if (this.DeclaringType.ContainsGenericParameters || this.ContainsGenericParameters)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Arg_UnboundGenParam"));
			}
			if (base.IsAbstract)
			{
				throw new MemberAccessException();
			}
			if (this.ReturnType.IsByRef)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_ByRefReturn"));
			}
			throw new TargetException();
		}

		// Token: 0x0600478A RID: 18314 RVA: 0x00105CA4 File Offset: 0x00103EA4
		[SecuritySafeCritical]
		[DebuggerStepThrough]
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			object[] array = this.InvokeArgumentsCheck(obj, invokeAttr, binder, parameters, culture);
			INVOCATION_FLAGS invocationFlags = this.InvocationFlags;
			if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
			{
				StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
				RuntimeAssembly executingAssembly = RuntimeAssembly.GetExecutingAssembly(ref stackCrawlMark);
				if (executingAssembly != null && !executingAssembly.IsSafeForReflection())
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", new object[] { base.FullName }));
				}
			}
			if ((invocationFlags & (INVOCATION_FLAGS.INVOCATION_FLAGS_NEED_SECURITY | INVOCATION_FLAGS.INVOCATION_FLAGS_RISKY_METHOD)) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
			{
				if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_RISKY_METHOD) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
				{
					CodeAccessPermission.Demand(PermissionType.ReflectionMemberAccess);
				}
				if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NEED_SECURITY) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
				{
					RuntimeMethodHandle.PerformSecurityCheck(obj, this, this.m_declaringType, (uint)this.m_invocationFlags);
				}
			}
			return this.UnsafeInvokeInternal(obj, parameters, array);
		}

		// Token: 0x0600478B RID: 18315 RVA: 0x00105D3C File Offset: 0x00103F3C
		[SecurityCritical]
		[DebuggerStepThrough]
		[DebuggerHidden]
		internal object UnsafeInvoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			object[] array = this.InvokeArgumentsCheck(obj, invokeAttr, binder, parameters, culture);
			return this.UnsafeInvokeInternal(obj, parameters, array);
		}

		// Token: 0x0600478C RID: 18316 RVA: 0x00105D64 File Offset: 0x00103F64
		[SecurityCritical]
		[DebuggerStepThrough]
		[DebuggerHidden]
		private object UnsafeInvokeInternal(object obj, object[] parameters, object[] arguments)
		{
			if (arguments == null || arguments.Length == 0)
			{
				return RuntimeMethodHandle.InvokeMethod(obj, null, this.Signature, false);
			}
			object obj2 = RuntimeMethodHandle.InvokeMethod(obj, arguments, this.Signature, false);
			for (int i = 0; i < arguments.Length; i++)
			{
				parameters[i] = arguments[i];
			}
			return obj2;
		}

		// Token: 0x0600478D RID: 18317 RVA: 0x00105DAC File Offset: 0x00103FAC
		[DebuggerStepThrough]
		[DebuggerHidden]
		private object[] InvokeArgumentsCheck(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			Signature signature = this.Signature;
			int num = signature.Arguments.Length;
			int num2 = ((parameters != null) ? parameters.Length : 0);
			INVOCATION_FLAGS invocationFlags = this.InvocationFlags;
			if ((invocationFlags & (INVOCATION_FLAGS.INVOCATION_FLAGS_NO_INVOKE | INVOCATION_FLAGS.INVOCATION_FLAGS_CONTAINS_STACK_POINTERS)) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
			{
				this.ThrowNoInvokeException();
			}
			this.CheckConsistency(obj);
			if (num != num2)
			{
				throw new TargetParameterCountException(Environment.GetResourceString("Arg_ParmCnt"));
			}
			if (num2 != 0)
			{
				return base.CheckArguments(parameters, binder, invokeAttr, culture, signature);
			}
			return null;
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x0600478E RID: 18318 RVA: 0x00105E18 File Offset: 0x00104018
		public override Type ReturnType
		{
			get
			{
				return this.Signature.ReturnType;
			}
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x0600478F RID: 18319 RVA: 0x00105E25 File Offset: 0x00104025
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				return this.ReturnParameter;
			}
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x06004790 RID: 18320 RVA: 0x00105E2D File Offset: 0x0010402D
		public override ParameterInfo ReturnParameter
		{
			[SecuritySafeCritical]
			get
			{
				this.FetchReturnParameter();
				return this.m_returnParameter;
			}
		}

		// Token: 0x06004791 RID: 18321 RVA: 0x00105E3C File Offset: 0x0010403C
		[SecuritySafeCritical]
		public override MethodInfo GetBaseDefinition()
		{
			if (!base.IsVirtual || base.IsStatic || this.m_declaringType == null || this.m_declaringType.IsInterface)
			{
				return this;
			}
			int slot = RuntimeMethodHandle.GetSlot(this);
			RuntimeType runtimeType = (RuntimeType)this.DeclaringType;
			RuntimeType runtimeType2 = runtimeType;
			RuntimeMethodHandleInternal runtimeMethodHandleInternal = default(RuntimeMethodHandleInternal);
			do
			{
				int numVirtuals = RuntimeTypeHandle.GetNumVirtuals(runtimeType);
				if (numVirtuals <= slot)
				{
					break;
				}
				runtimeMethodHandleInternal = RuntimeTypeHandle.GetMethodAt(runtimeType, slot);
				runtimeType2 = runtimeType;
				runtimeType = (RuntimeType)runtimeType.BaseType;
			}
			while (runtimeType != null);
			return (MethodInfo)RuntimeType.GetMethodBase(runtimeType2, runtimeMethodHandleInternal);
		}

		// Token: 0x06004792 RID: 18322 RVA: 0x00105ECC File Offset: 0x001040CC
		[SecuritySafeCritical]
		public override Delegate CreateDelegate(Type delegateType)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.CreateDelegateInternal(delegateType, null, (DelegateBindingFlags)132, ref stackCrawlMark);
		}

		// Token: 0x06004793 RID: 18323 RVA: 0x00105EEC File Offset: 0x001040EC
		[SecuritySafeCritical]
		public override Delegate CreateDelegate(Type delegateType, object target)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.CreateDelegateInternal(delegateType, target, DelegateBindingFlags.RelaxedSignature, ref stackCrawlMark);
		}

		// Token: 0x06004794 RID: 18324 RVA: 0x00105F0C File Offset: 0x0010410C
		[SecurityCritical]
		private Delegate CreateDelegateInternal(Type delegateType, object firstArgument, DelegateBindingFlags bindingFlags, ref StackCrawlMark stackMark)
		{
			if (delegateType == null)
			{
				throw new ArgumentNullException("delegateType");
			}
			RuntimeType runtimeType = delegateType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), "delegateType");
			}
			if (!runtimeType.IsDelegate())
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDelegate"), "delegateType");
			}
			Delegate @delegate = Delegate.CreateDelegateInternal(runtimeType, this, firstArgument, bindingFlags, ref stackMark);
			if (@delegate == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_DlgtTargMeth"));
			}
			return @delegate;
		}

		// Token: 0x06004795 RID: 18325 RVA: 0x00105F90 File Offset: 0x00104190
		[SecuritySafeCritical]
		public override MethodInfo MakeGenericMethod(params Type[] methodInstantiation)
		{
			if (methodInstantiation == null)
			{
				throw new ArgumentNullException("methodInstantiation");
			}
			RuntimeType[] array = new RuntimeType[methodInstantiation.Length];
			if (!this.IsGenericMethodDefinition)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Arg_NotGenericMethodDefinition", new object[] { this }));
			}
			for (int i = 0; i < methodInstantiation.Length; i++)
			{
				Type type = methodInstantiation[i];
				if (type == null)
				{
					throw new ArgumentNullException();
				}
				RuntimeType runtimeType = type as RuntimeType;
				if (runtimeType == null)
				{
					Type[] array2 = new Type[methodInstantiation.Length];
					for (int j = 0; j < methodInstantiation.Length; j++)
					{
						array2[j] = methodInstantiation[j];
					}
					methodInstantiation = array2;
					return MethodBuilderInstantiation.MakeGenericMethod(this, methodInstantiation);
				}
				array[i] = runtimeType;
			}
			RuntimeType[] genericArgumentsInternal = this.GetGenericArgumentsInternal();
			RuntimeType.SanityCheckGenericArguments(array, genericArgumentsInternal);
			MethodInfo methodInfo = null;
			try
			{
				methodInfo = RuntimeType.GetMethodBase(this.ReflectedTypeInternal, RuntimeMethodHandle.GetStubIfNeeded(new RuntimeMethodHandleInternal(this.m_handle), this.m_declaringType, array)) as MethodInfo;
			}
			catch (VerificationException ex)
			{
				RuntimeType.ValidateGenericArguments(this, array, ex);
				throw;
			}
			return methodInfo;
		}

		// Token: 0x06004796 RID: 18326 RVA: 0x0010609C File Offset: 0x0010429C
		internal RuntimeType[] GetGenericArgumentsInternal()
		{
			return RuntimeMethodHandle.GetMethodInstantiationInternal(this);
		}

		// Token: 0x06004797 RID: 18327 RVA: 0x001060A4 File Offset: 0x001042A4
		public override Type[] GetGenericArguments()
		{
			Type[] array = RuntimeMethodHandle.GetMethodInstantiationPublic(this);
			if (array == null)
			{
				array = EmptyArray<Type>.Value;
			}
			return array;
		}

		// Token: 0x06004798 RID: 18328 RVA: 0x001060C2 File Offset: 0x001042C2
		public override MethodInfo GetGenericMethodDefinition()
		{
			if (!this.IsGenericMethod)
			{
				throw new InvalidOperationException();
			}
			return RuntimeType.GetMethodBase(this.m_declaringType, RuntimeMethodHandle.StripMethodInstantiation(this)) as MethodInfo;
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x06004799 RID: 18329 RVA: 0x001060E8 File Offset: 0x001042E8
		public override bool IsGenericMethod
		{
			get
			{
				return RuntimeMethodHandle.HasMethodInstantiation(this);
			}
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x0600479A RID: 18330 RVA: 0x001060F0 File Offset: 0x001042F0
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return RuntimeMethodHandle.IsGenericMethodDefinition(this);
			}
		}

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x0600479B RID: 18331 RVA: 0x001060F8 File Offset: 0x001042F8
		public override bool ContainsGenericParameters
		{
			get
			{
				if (this.DeclaringType != null && this.DeclaringType.ContainsGenericParameters)
				{
					return true;
				}
				if (!this.IsGenericMethod)
				{
					return false;
				}
				Type[] genericArguments = this.GetGenericArguments();
				for (int i = 0; i < genericArguments.Length; i++)
				{
					if (genericArguments[i].ContainsGenericParameters)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x0600479C RID: 18332 RVA: 0x00106150 File Offset: 0x00104350
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			if (this.m_reflectedTypeCache.IsGlobal)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_GlobalMethodSerialization"));
			}
			MemberInfoSerializationHolder.GetSerializationInfo(info, this.Name, this.ReflectedTypeInternal, this.ToString(), this.SerializationToString(), MemberTypes.Method, (this.IsGenericMethod & !this.IsGenericMethodDefinition) ? this.GetGenericArguments() : null);
		}

		// Token: 0x0600479D RID: 18333 RVA: 0x001061C2 File Offset: 0x001043C2
		internal string SerializationToString()
		{
			return this.ReturnType.FormatTypeName(true) + " " + this.FormatNameAndSig(true);
		}

		// Token: 0x0600479E RID: 18334 RVA: 0x001061E4 File Offset: 0x001043E4
		internal static MethodBase InternalGetCurrentMethod(ref StackCrawlMark stackMark)
		{
			IRuntimeMethodInfo currentMethod = RuntimeMethodHandle.GetCurrentMethod(ref stackMark);
			if (currentMethod == null)
			{
				return null;
			}
			return RuntimeType.GetMethodBase(currentMethod);
		}

		// Token: 0x04001DA8 RID: 7592
		private IntPtr m_handle;

		// Token: 0x04001DA9 RID: 7593
		private RuntimeType.RuntimeTypeCache m_reflectedTypeCache;

		// Token: 0x04001DAA RID: 7594
		private string m_name;

		// Token: 0x04001DAB RID: 7595
		private string m_toString;

		// Token: 0x04001DAC RID: 7596
		private ParameterInfo[] m_parameters;

		// Token: 0x04001DAD RID: 7597
		private ParameterInfo m_returnParameter;

		// Token: 0x04001DAE RID: 7598
		private BindingFlags m_bindingFlags;

		// Token: 0x04001DAF RID: 7599
		private MethodAttributes m_methodAttributes;

		// Token: 0x04001DB0 RID: 7600
		private Signature m_signature;

		// Token: 0x04001DB1 RID: 7601
		private RuntimeType m_declaringType;

		// Token: 0x04001DB2 RID: 7602
		private object m_keepalive;

		// Token: 0x04001DB3 RID: 7603
		private INVOCATION_FLAGS m_invocationFlags;

		// Token: 0x04001DB4 RID: 7604
		private RemotingMethodCachedData m_cachedData;
	}
}
