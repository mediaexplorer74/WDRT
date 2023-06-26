using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Reflection
{
	// Token: 0x020005D1 RID: 1489
	[Serializable]
	internal sealed class RuntimeConstructorInfo : ConstructorInfo, ISerializable, IRuntimeMethodInfo
	{
		// Token: 0x06004511 RID: 17681 RVA: 0x000FEB54 File Offset: 0x000FCD54
		private bool IsNonW8PFrameworkAPI()
		{
			if (this.DeclaringType.IsArray && base.IsPublic && !base.IsStatic)
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
			return this.GetRuntimeType().IsNonW8PFrameworkAPI();
		}

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x06004512 RID: 17682 RVA: 0x000FEBC4 File Offset: 0x000FCDC4
		internal override bool IsDynamicallyInvokable
		{
			get
			{
				return !AppDomain.ProfileAPICheck || !this.IsNonW8PFrameworkAPI();
			}
		}

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x06004513 RID: 17683 RVA: 0x000FEBD8 File Offset: 0x000FCDD8
		internal INVOCATION_FLAGS InvocationFlags
		{
			[SecuritySafeCritical]
			get
			{
				if ((this.m_invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_INITIALIZED) == INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
				{
					INVOCATION_FLAGS invocation_FLAGS = INVOCATION_FLAGS.INVOCATION_FLAGS_IS_CTOR;
					Type declaringType = this.DeclaringType;
					if (declaringType == typeof(void) || (declaringType != null && declaringType.ContainsGenericParameters) || (this.CallingConvention & CallingConventions.VarArgs) == CallingConventions.VarArgs || (this.Attributes & MethodAttributes.RequireSecObject) == MethodAttributes.RequireSecObject)
					{
						invocation_FLAGS |= INVOCATION_FLAGS.INVOCATION_FLAGS_NO_INVOKE;
					}
					else if (base.IsStatic || (declaringType != null && declaringType.IsAbstract))
					{
						invocation_FLAGS |= INVOCATION_FLAGS.INVOCATION_FLAGS_NO_CTOR_INVOKE;
					}
					else
					{
						invocation_FLAGS |= RuntimeMethodHandle.GetSecurityFlags(this);
						if ((invocation_FLAGS & INVOCATION_FLAGS.INVOCATION_FLAGS_NEED_SECURITY) == INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN && ((this.Attributes & MethodAttributes.MemberAccessMask) != MethodAttributes.Public || (declaringType != null && declaringType.NeedsReflectionSecurityCheck)))
						{
							invocation_FLAGS |= INVOCATION_FLAGS.INVOCATION_FLAGS_NEED_SECURITY;
						}
						if (typeof(Delegate).IsAssignableFrom(this.DeclaringType))
						{
							invocation_FLAGS |= INVOCATION_FLAGS.INVOCATION_FLAGS_IS_DELEGATE_CTOR;
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

		// Token: 0x06004514 RID: 17684 RVA: 0x000FECD2 File Offset: 0x000FCED2
		[SecurityCritical]
		internal RuntimeConstructorInfo(RuntimeMethodHandleInternal handle, RuntimeType declaringType, RuntimeType.RuntimeTypeCache reflectedTypeCache, MethodAttributes methodAttributes, BindingFlags bindingFlags)
		{
			this.m_bindingFlags = bindingFlags;
			this.m_reflectedTypeCache = reflectedTypeCache;
			this.m_declaringType = declaringType;
			this.m_handle = handle.Value;
			this.m_methodAttributes = methodAttributes;
		}

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x06004515 RID: 17685 RVA: 0x000FED08 File Offset: 0x000FCF08
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

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x06004516 RID: 17686 RVA: 0x000FED3A File Offset: 0x000FCF3A
		RuntimeMethodHandleInternal IRuntimeMethodInfo.Value
		{
			[SecuritySafeCritical]
			get
			{
				return new RuntimeMethodHandleInternal(this.m_handle);
			}
		}

		// Token: 0x06004517 RID: 17687 RVA: 0x000FED48 File Offset: 0x000FCF48
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal override bool CacheEquals(object o)
		{
			RuntimeConstructorInfo runtimeConstructorInfo = o as RuntimeConstructorInfo;
			return runtimeConstructorInfo != null && runtimeConstructorInfo.m_handle == this.m_handle;
		}

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06004518 RID: 17688 RVA: 0x000FED72 File Offset: 0x000FCF72
		private Signature Signature
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

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x06004519 RID: 17689 RVA: 0x000FED9C File Offset: 0x000FCF9C
		private RuntimeType ReflectedTypeInternal
		{
			get
			{
				return this.m_reflectedTypeCache.GetRuntimeType();
			}
		}

		// Token: 0x0600451A RID: 17690 RVA: 0x000FEDAC File Offset: 0x000FCFAC
		private void CheckConsistency(object target)
		{
			if (target == null && base.IsStatic)
			{
				return;
			}
			if (this.m_declaringType.IsInstanceOfType(target))
			{
				return;
			}
			if (target == null)
			{
				throw new TargetException(Environment.GetResourceString("RFLCT.Targ_StatMethReqTarg"));
			}
			throw new TargetException(Environment.GetResourceString("RFLCT.Targ_ITargMismatch"));
		}

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x0600451B RID: 17691 RVA: 0x000FEDF8 File Offset: 0x000FCFF8
		internal BindingFlags BindingFlags
		{
			get
			{
				return this.m_bindingFlags;
			}
		}

		// Token: 0x0600451C RID: 17692 RVA: 0x000FEE00 File Offset: 0x000FD000
		internal RuntimeMethodHandle GetMethodHandle()
		{
			return new RuntimeMethodHandle(this);
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x0600451D RID: 17693 RVA: 0x000FEE08 File Offset: 0x000FD008
		internal bool IsOverloaded
		{
			get
			{
				return this.m_reflectedTypeCache.GetConstructorList(RuntimeType.MemberListType.CaseSensitive, this.Name).Length > 1;
			}
		}

		// Token: 0x0600451E RID: 17694 RVA: 0x000FEE21 File Offset: 0x000FD021
		public override string ToString()
		{
			if (this.m_toString == null)
			{
				this.m_toString = "Void " + base.FormatNameAndSig();
			}
			return this.m_toString;
		}

		// Token: 0x0600451F RID: 17695 RVA: 0x000FEE47 File Offset: 0x000FD047
		public override object[] GetCustomAttributes(bool inherit)
		{
			return CustomAttribute.GetCustomAttributes(this, typeof(object) as RuntimeType);
		}

		// Token: 0x06004520 RID: 17696 RVA: 0x000FEE60 File Offset: 0x000FD060
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
			return CustomAttribute.GetCustomAttributes(this, runtimeType);
		}

		// Token: 0x06004521 RID: 17697 RVA: 0x000FEEB4 File Offset: 0x000FD0B4
		[SecuritySafeCritical]
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
			return CustomAttribute.IsDefined(this, runtimeType);
		}

		// Token: 0x06004522 RID: 17698 RVA: 0x000FEF06 File Offset: 0x000FD106
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributesInternal(this);
		}

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06004523 RID: 17699 RVA: 0x000FEF0E File Offset: 0x000FD10E
		public override string Name
		{
			[SecuritySafeCritical]
			get
			{
				return RuntimeMethodHandle.GetName(this);
			}
		}

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06004524 RID: 17700 RVA: 0x000FEF16 File Offset: 0x000FD116
		[ComVisible(true)]
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Constructor;
			}
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06004525 RID: 17701 RVA: 0x000FEF19 File Offset: 0x000FD119
		public override Type DeclaringType
		{
			get
			{
				if (!this.m_reflectedTypeCache.IsGlobal)
				{
					return this.m_declaringType;
				}
				return null;
			}
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06004526 RID: 17702 RVA: 0x000FEF32 File Offset: 0x000FD132
		public override Type ReflectedType
		{
			get
			{
				if (!this.m_reflectedTypeCache.IsGlobal)
				{
					return this.ReflectedTypeInternal;
				}
				return null;
			}
		}

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06004527 RID: 17703 RVA: 0x000FEF49 File Offset: 0x000FD149
		public override int MetadataToken
		{
			[SecuritySafeCritical]
			get
			{
				return RuntimeMethodHandle.GetMethodDef(this);
			}
		}

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x06004528 RID: 17704 RVA: 0x000FEF51 File Offset: 0x000FD151
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x06004529 RID: 17705 RVA: 0x000FEF59 File Offset: 0x000FD159
		internal RuntimeType GetRuntimeType()
		{
			return this.m_declaringType;
		}

		// Token: 0x0600452A RID: 17706 RVA: 0x000FEF63 File Offset: 0x000FD163
		internal RuntimeModule GetRuntimeModule()
		{
			return RuntimeTypeHandle.GetModule(this.m_declaringType);
		}

		// Token: 0x0600452B RID: 17707 RVA: 0x000FEF72 File Offset: 0x000FD172
		internal RuntimeAssembly GetRuntimeAssembly()
		{
			return this.GetRuntimeModule().GetRuntimeAssembly();
		}

		// Token: 0x0600452C RID: 17708 RVA: 0x000FEF7F File Offset: 0x000FD17F
		internal override Type GetReturnType()
		{
			return this.Signature.ReturnType;
		}

		// Token: 0x0600452D RID: 17709 RVA: 0x000FEF8C File Offset: 0x000FD18C
		[SecuritySafeCritical]
		internal override ParameterInfo[] GetParametersNoCopy()
		{
			if (this.m_parameters == null)
			{
				this.m_parameters = RuntimeParameterInfo.GetParameters(this, this, this.Signature);
			}
			return this.m_parameters;
		}

		// Token: 0x0600452E RID: 17710 RVA: 0x000FEFB0 File Offset: 0x000FD1B0
		public override ParameterInfo[] GetParameters()
		{
			ParameterInfo[] parametersNoCopy = this.GetParametersNoCopy();
			if (parametersNoCopy.Length == 0)
			{
				return parametersNoCopy;
			}
			ParameterInfo[] array = new ParameterInfo[parametersNoCopy.Length];
			Array.Copy(parametersNoCopy, array, parametersNoCopy.Length);
			return array;
		}

		// Token: 0x0600452F RID: 17711 RVA: 0x000FEFDE File Offset: 0x000FD1DE
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return RuntimeMethodHandle.GetImplAttributes(this);
		}

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x06004530 RID: 17712 RVA: 0x000FEFE8 File Offset: 0x000FD1E8
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

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x06004531 RID: 17713 RVA: 0x000FF035 File Offset: 0x000FD235
		public override MethodAttributes Attributes
		{
			get
			{
				return this.m_methodAttributes;
			}
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06004532 RID: 17714 RVA: 0x000FF03D File Offset: 0x000FD23D
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.Signature.CallingConvention;
			}
		}

		// Token: 0x06004533 RID: 17715 RVA: 0x000FF04C File Offset: 0x000FD24C
		internal static void CheckCanCreateInstance(Type declaringType, bool isVarArg)
		{
			if (declaringType == null)
			{
				throw new ArgumentNullException("declaringType");
			}
			if (declaringType is ReflectionOnlyType)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Arg_ReflectionOnlyInvoke"));
			}
			if (declaringType.IsInterface)
			{
				throw new MemberAccessException(string.Format(CultureInfo.CurrentUICulture, Environment.GetResourceString("Acc_CreateInterfaceEx"), declaringType));
			}
			if (declaringType.IsAbstract)
			{
				throw new MemberAccessException(string.Format(CultureInfo.CurrentUICulture, Environment.GetResourceString("Acc_CreateAbstEx"), declaringType));
			}
			if (declaringType.GetRootElementType() == typeof(ArgIterator))
			{
				throw new NotSupportedException();
			}
			if (isVarArg)
			{
				throw new NotSupportedException();
			}
			if (declaringType.ContainsGenericParameters)
			{
				throw new MemberAccessException(string.Format(CultureInfo.CurrentUICulture, Environment.GetResourceString("Acc_CreateGenericEx"), declaringType));
			}
			if (declaringType == typeof(void))
			{
				throw new MemberAccessException(Environment.GetResourceString("Access_Void"));
			}
		}

		// Token: 0x06004534 RID: 17716 RVA: 0x000FF136 File Offset: 0x000FD336
		internal void ThrowNoInvokeException()
		{
			RuntimeConstructorInfo.CheckCanCreateInstance(this.DeclaringType, (this.CallingConvention & CallingConventions.VarArgs) == CallingConventions.VarArgs);
			if ((this.Attributes & MethodAttributes.Static) == MethodAttributes.Static)
			{
				throw new MemberAccessException(Environment.GetResourceString("Acc_NotClassInit"));
			}
			throw new TargetException();
		}

		// Token: 0x06004535 RID: 17717 RVA: 0x000FF170 File Offset: 0x000FD370
		[SecuritySafeCritical]
		[DebuggerStepThrough]
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			INVOCATION_FLAGS invocationFlags = this.InvocationFlags;
			if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NO_INVOKE) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
			{
				this.ThrowNoInvokeException();
			}
			this.CheckConsistency(obj);
			if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
			{
				StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
				RuntimeAssembly executingAssembly = RuntimeAssembly.GetExecutingAssembly(ref stackCrawlMark);
				if (executingAssembly != null && !executingAssembly.IsSafeForReflection())
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", new object[] { base.FullName }));
				}
			}
			if (obj != null)
			{
				new SecurityPermission(SecurityPermissionFlag.SkipVerification).Demand();
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
			Signature signature = this.Signature;
			int num = signature.Arguments.Length;
			int num2 = ((parameters != null) ? parameters.Length : 0);
			if (num != num2)
			{
				throw new TargetParameterCountException(Environment.GetResourceString("Arg_ParmCnt"));
			}
			if (num2 > 0)
			{
				object[] array = base.CheckArguments(parameters, binder, invokeAttr, culture, signature);
				object obj2 = RuntimeMethodHandle.InvokeMethod(obj, array, signature, false);
				for (int i = 0; i < array.Length; i++)
				{
					parameters[i] = array[i];
				}
				return obj2;
			}
			return RuntimeMethodHandle.InvokeMethod(obj, null, signature, false);
		}

		// Token: 0x06004536 RID: 17718 RVA: 0x000FF28C File Offset: 0x000FD48C
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

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06004537 RID: 17719 RVA: 0x000FF2B1 File Offset: 0x000FD4B1
		public override bool IsSecurityCritical
		{
			get
			{
				return RuntimeMethodHandle.IsSecurityCritical(this);
			}
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x06004538 RID: 17720 RVA: 0x000FF2B9 File Offset: 0x000FD4B9
		public override bool IsSecuritySafeCritical
		{
			get
			{
				return RuntimeMethodHandle.IsSecuritySafeCritical(this);
			}
		}

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x06004539 RID: 17721 RVA: 0x000FF2C1 File Offset: 0x000FD4C1
		public override bool IsSecurityTransparent
		{
			get
			{
				return RuntimeMethodHandle.IsSecurityTransparent(this);
			}
		}

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x0600453A RID: 17722 RVA: 0x000FF2C9 File Offset: 0x000FD4C9
		public override bool ContainsGenericParameters
		{
			get
			{
				return this.DeclaringType != null && this.DeclaringType.ContainsGenericParameters;
			}
		}

		// Token: 0x0600453B RID: 17723 RVA: 0x000FF2E8 File Offset: 0x000FD4E8
		[SecuritySafeCritical]
		[DebuggerStepThrough]
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			INVOCATION_FLAGS invocationFlags = this.InvocationFlags;
			RuntimeTypeHandle typeHandle = this.m_declaringType.TypeHandle;
			if ((invocationFlags & (INVOCATION_FLAGS.INVOCATION_FLAGS_NO_INVOKE | INVOCATION_FLAGS.INVOCATION_FLAGS_NO_CTOR_INVOKE | INVOCATION_FLAGS.INVOCATION_FLAGS_CONTAINS_STACK_POINTERS)) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
			{
				this.ThrowNoInvokeException();
			}
			if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
			{
				StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
				RuntimeAssembly executingAssembly = RuntimeAssembly.GetExecutingAssembly(ref stackCrawlMark);
				if (executingAssembly != null && !executingAssembly.IsSafeForReflection())
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", new object[] { base.FullName }));
				}
			}
			if ((invocationFlags & (INVOCATION_FLAGS.INVOCATION_FLAGS_NEED_SECURITY | INVOCATION_FLAGS.INVOCATION_FLAGS_RISKY_METHOD | INVOCATION_FLAGS.INVOCATION_FLAGS_IS_DELEGATE_CTOR)) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
			{
				if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_RISKY_METHOD) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
				{
					CodeAccessPermission.Demand(PermissionType.ReflectionMemberAccess);
				}
				if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NEED_SECURITY) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
				{
					RuntimeMethodHandle.PerformSecurityCheck(null, this, this.m_declaringType, (uint)(this.m_invocationFlags | INVOCATION_FLAGS.INVOCATION_FLAGS_CONSTRUCTOR_INVOKE));
				}
				if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_IS_DELEGATE_CTOR) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
				{
					new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
				}
			}
			Signature signature = this.Signature;
			int num = signature.Arguments.Length;
			int num2 = ((parameters != null) ? parameters.Length : 0);
			if (num != num2)
			{
				throw new TargetParameterCountException(Environment.GetResourceString("Arg_ParmCnt"));
			}
			if (num2 > 0)
			{
				object[] array = base.CheckArguments(parameters, binder, invokeAttr, culture, signature);
				object obj = RuntimeMethodHandle.InvokeMethod(null, array, signature, true);
				for (int i = 0; i < array.Length; i++)
				{
					parameters[i] = array[i];
				}
				return obj;
			}
			return RuntimeMethodHandle.InvokeMethod(null, null, signature, true);
		}

		// Token: 0x0600453C RID: 17724 RVA: 0x000FF41D File Offset: 0x000FD61D
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			MemberInfoSerializationHolder.GetSerializationInfo(info, this.Name, this.ReflectedTypeInternal, this.ToString(), this.SerializationToString(), MemberTypes.Constructor, null);
		}

		// Token: 0x0600453D RID: 17725 RVA: 0x000FF44D File Offset: 0x000FD64D
		internal string SerializationToString()
		{
			return this.FormatNameAndSig(true);
		}

		// Token: 0x0600453E RID: 17726 RVA: 0x000FF456 File Offset: 0x000FD656
		internal void SerializationInvoke(object target, SerializationInfo info, StreamingContext context)
		{
			RuntimeMethodHandle.SerializationInvoke(this, target, info, ref context);
		}

		// Token: 0x04001C54 RID: 7252
		private volatile RuntimeType m_declaringType;

		// Token: 0x04001C55 RID: 7253
		private RuntimeType.RuntimeTypeCache m_reflectedTypeCache;

		// Token: 0x04001C56 RID: 7254
		private string m_toString;

		// Token: 0x04001C57 RID: 7255
		private ParameterInfo[] m_parameters;

		// Token: 0x04001C58 RID: 7256
		private object _empty1;

		// Token: 0x04001C59 RID: 7257
		private object _empty2;

		// Token: 0x04001C5A RID: 7258
		private object _empty3;

		// Token: 0x04001C5B RID: 7259
		private IntPtr m_handle;

		// Token: 0x04001C5C RID: 7260
		private MethodAttributes m_methodAttributes;

		// Token: 0x04001C5D RID: 7261
		private BindingFlags m_bindingFlags;

		// Token: 0x04001C5E RID: 7262
		private volatile Signature m_signature;

		// Token: 0x04001C5F RID: 7263
		private INVOCATION_FLAGS m_invocationFlags;

		// Token: 0x04001C60 RID: 7264
		private RemotingMethodCachedData m_cachedData;
	}
}
