using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Threading;

namespace System.Reflection
{
	// Token: 0x020005E5 RID: 1509
	[Serializable]
	internal sealed class RtFieldInfo : RuntimeFieldInfo, IRuntimeFieldInfo
	{
		// Token: 0x06004638 RID: 17976
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PerformVisibilityCheckOnField(IntPtr field, object target, RuntimeType declaringType, FieldAttributes attr, uint invocationFlags);

		// Token: 0x06004639 RID: 17977 RVA: 0x00103368 File Offset: 0x00101568
		private bool IsNonW8PFrameworkAPI()
		{
			if (base.GetRuntimeType().IsNonW8PFrameworkAPI())
			{
				return true;
			}
			if (this.m_declaringType.IsEnum)
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
			return false;
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x0600463A RID: 17978 RVA: 0x001033C8 File Offset: 0x001015C8
		internal INVOCATION_FLAGS InvocationFlags
		{
			get
			{
				if ((this.m_invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_INITIALIZED) == INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
				{
					Type declaringType = this.DeclaringType;
					bool flag = declaringType is ReflectionOnlyType;
					INVOCATION_FLAGS invocation_FLAGS = INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN;
					if ((declaringType != null && declaringType.ContainsGenericParameters) || (declaringType == null && this.Module.Assembly.ReflectionOnly) || flag)
					{
						invocation_FLAGS |= INVOCATION_FLAGS.INVOCATION_FLAGS_NO_INVOKE;
					}
					if (invocation_FLAGS == INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
					{
						if ((this.m_fieldAttributes & FieldAttributes.InitOnly) != FieldAttributes.PrivateScope)
						{
							invocation_FLAGS |= INVOCATION_FLAGS.INVOCATION_FLAGS_IS_CTOR;
						}
						if ((this.m_fieldAttributes & FieldAttributes.HasFieldRVA) != FieldAttributes.PrivateScope)
						{
							invocation_FLAGS |= INVOCATION_FLAGS.INVOCATION_FLAGS_IS_CTOR;
						}
						bool flag2 = this.IsSecurityCritical && !this.IsSecuritySafeCritical;
						bool flag3 = (this.m_fieldAttributes & FieldAttributes.FieldAccessMask) != FieldAttributes.Public || (declaringType != null && declaringType.NeedsReflectionSecurityCheck);
						if (flag2 || flag3)
						{
							invocation_FLAGS |= INVOCATION_FLAGS.INVOCATION_FLAGS_NEED_SECURITY;
						}
						Type fieldType = this.FieldType;
						if (fieldType.IsPointer || fieldType.IsEnum || fieldType.IsPrimitive)
						{
							invocation_FLAGS |= INVOCATION_FLAGS.INVOCATION_FLAGS_RISKY_METHOD;
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

		// Token: 0x0600463B RID: 17979 RVA: 0x001034E2 File Offset: 0x001016E2
		private RuntimeAssembly GetRuntimeAssembly()
		{
			return this.m_declaringType.GetRuntimeAssembly();
		}

		// Token: 0x0600463C RID: 17980 RVA: 0x001034EF File Offset: 0x001016EF
		[SecurityCritical]
		internal RtFieldInfo(RuntimeFieldHandleInternal handle, RuntimeType declaringType, RuntimeType.RuntimeTypeCache reflectedTypeCache, BindingFlags bindingFlags)
			: base(reflectedTypeCache, declaringType, bindingFlags)
		{
			this.m_fieldHandle = handle.Value;
			this.m_fieldAttributes = RuntimeFieldHandle.GetAttributes(handle);
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x0600463D RID: 17981 RVA: 0x00103514 File Offset: 0x00101714
		RuntimeFieldHandleInternal IRuntimeFieldInfo.Value
		{
			[SecuritySafeCritical]
			get
			{
				return new RuntimeFieldHandleInternal(this.m_fieldHandle);
			}
		}

		// Token: 0x0600463E RID: 17982 RVA: 0x00103524 File Offset: 0x00101724
		internal void CheckConsistency(object target)
		{
			if ((this.m_fieldAttributes & FieldAttributes.Static) == FieldAttributes.Static || this.m_declaringType.IsInstanceOfType(target))
			{
				return;
			}
			if (target == null)
			{
				throw new TargetException(Environment.GetResourceString("RFLCT.Targ_StatFldReqTarg"));
			}
			throw new ArgumentException(string.Format(CultureInfo.CurrentUICulture, Environment.GetResourceString("Arg_FieldDeclTarget"), this.Name, this.m_declaringType, target.GetType()));
		}

		// Token: 0x0600463F RID: 17983 RVA: 0x0010358C File Offset: 0x0010178C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal override bool CacheEquals(object o)
		{
			RtFieldInfo rtFieldInfo = o as RtFieldInfo;
			return rtFieldInfo != null && rtFieldInfo.m_fieldHandle == this.m_fieldHandle;
		}

		// Token: 0x06004640 RID: 17984 RVA: 0x001035B8 File Offset: 0x001017B8
		[SecurityCritical]
		[DebuggerStepThrough]
		[DebuggerHidden]
		internal void InternalSetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture, ref StackCrawlMark stackMark)
		{
			INVOCATION_FLAGS invocationFlags = this.InvocationFlags;
			RuntimeType runtimeType = this.DeclaringType as RuntimeType;
			if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NO_INVOKE) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
			{
				if (runtimeType != null && runtimeType.ContainsGenericParameters)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Arg_UnboundGenField"));
				}
				if ((runtimeType == null && this.Module.Assembly.ReflectionOnly) || runtimeType is ReflectionOnlyType)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Arg_ReflectionOnlyField"));
				}
				throw new FieldAccessException();
			}
			else
			{
				this.CheckConsistency(obj);
				RuntimeType runtimeType2 = (RuntimeType)this.FieldType;
				value = runtimeType2.CheckValue(value, binder, culture, invokeAttr);
				if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
				{
					RuntimeAssembly executingAssembly = RuntimeAssembly.GetExecutingAssembly(ref stackMark);
					if (executingAssembly != null && !executingAssembly.IsSafeForReflection())
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", new object[] { this.FullName }));
					}
				}
				if ((invocationFlags & (INVOCATION_FLAGS.INVOCATION_FLAGS_NEED_SECURITY | INVOCATION_FLAGS.INVOCATION_FLAGS_IS_CTOR)) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
				{
					RtFieldInfo.PerformVisibilityCheckOnField(this.m_fieldHandle, obj, this.m_declaringType, this.m_fieldAttributes, (uint)this.m_invocationFlags);
				}
				bool flag = false;
				if (runtimeType == null)
				{
					RuntimeFieldHandle.SetValue(this, obj, value, runtimeType2, this.m_fieldAttributes, null, ref flag);
					return;
				}
				flag = runtimeType.DomainInitialized;
				RuntimeFieldHandle.SetValue(this, obj, value, runtimeType2, this.m_fieldAttributes, runtimeType, ref flag);
				runtimeType.DomainInitialized = flag;
				return;
			}
		}

		// Token: 0x06004641 RID: 17985 RVA: 0x001036FC File Offset: 0x001018FC
		[SecurityCritical]
		[DebuggerStepThrough]
		[DebuggerHidden]
		internal void UnsafeSetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			RuntimeType runtimeType = this.DeclaringType as RuntimeType;
			RuntimeType runtimeType2 = (RuntimeType)this.FieldType;
			value = runtimeType2.CheckValue(value, binder, culture, invokeAttr);
			bool flag = false;
			if (runtimeType == null)
			{
				RuntimeFieldHandle.SetValue(this, obj, value, runtimeType2, this.m_fieldAttributes, null, ref flag);
				return;
			}
			flag = runtimeType.DomainInitialized;
			RuntimeFieldHandle.SetValue(this, obj, value, runtimeType2, this.m_fieldAttributes, runtimeType, ref flag);
			runtimeType.DomainInitialized = flag;
		}

		// Token: 0x06004642 RID: 17986 RVA: 0x00103770 File Offset: 0x00101970
		[SecuritySafeCritical]
		[DebuggerStepThrough]
		[DebuggerHidden]
		internal object InternalGetValue(object obj, ref StackCrawlMark stackMark)
		{
			INVOCATION_FLAGS invocationFlags = this.InvocationFlags;
			RuntimeType runtimeType = this.DeclaringType as RuntimeType;
			if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NO_INVOKE) == INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
			{
				this.CheckConsistency(obj);
				if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
				{
					RuntimeAssembly executingAssembly = RuntimeAssembly.GetExecutingAssembly(ref stackMark);
					if (executingAssembly != null && !executingAssembly.IsSafeForReflection())
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", new object[] { this.FullName }));
					}
				}
				RuntimeType runtimeType2 = (RuntimeType)this.FieldType;
				if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NEED_SECURITY) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
				{
					RtFieldInfo.PerformVisibilityCheckOnField(this.m_fieldHandle, obj, this.m_declaringType, this.m_fieldAttributes, (uint)(this.m_invocationFlags & ~INVOCATION_FLAGS.INVOCATION_FLAGS_IS_CTOR));
				}
				return this.UnsafeGetValue(obj);
			}
			if (runtimeType != null && this.DeclaringType.ContainsGenericParameters)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Arg_UnboundGenField"));
			}
			if ((runtimeType == null && this.Module.Assembly.ReflectionOnly) || runtimeType is ReflectionOnlyType)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Arg_ReflectionOnlyField"));
			}
			throw new FieldAccessException();
		}

		// Token: 0x06004643 RID: 17987 RVA: 0x00103874 File Offset: 0x00101A74
		[SecurityCritical]
		[DebuggerStepThrough]
		[DebuggerHidden]
		internal object UnsafeGetValue(object obj)
		{
			RuntimeType runtimeType = this.DeclaringType as RuntimeType;
			RuntimeType runtimeType2 = (RuntimeType)this.FieldType;
			bool flag = false;
			if (runtimeType == null)
			{
				return RuntimeFieldHandle.GetValue(this, obj, runtimeType2, null, ref flag);
			}
			flag = runtimeType.DomainInitialized;
			object value = RuntimeFieldHandle.GetValue(this, obj, runtimeType2, runtimeType, ref flag);
			runtimeType.DomainInitialized = flag;
			return value;
		}

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x06004644 RID: 17988 RVA: 0x001038CB File Offset: 0x00101ACB
		public override string Name
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_name == null)
				{
					this.m_name = RuntimeFieldHandle.GetName(this);
				}
				return this.m_name;
			}
		}

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06004645 RID: 17989 RVA: 0x001038E7 File Offset: 0x00101AE7
		internal string FullName
		{
			get
			{
				return string.Format("{0}.{1}", this.DeclaringType.FullName, this.Name);
			}
		}

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06004646 RID: 17990 RVA: 0x00103904 File Offset: 0x00101B04
		public override int MetadataToken
		{
			[SecuritySafeCritical]
			get
			{
				return RuntimeFieldHandle.GetToken(this);
			}
		}

		// Token: 0x06004647 RID: 17991 RVA: 0x0010390C File Offset: 0x00101B0C
		[SecuritySafeCritical]
		internal override RuntimeModule GetRuntimeModule()
		{
			return RuntimeTypeHandle.GetModule(RuntimeFieldHandle.GetApproxDeclaringType(this));
		}

		// Token: 0x06004648 RID: 17992 RVA: 0x0010391C File Offset: 0x00101B1C
		public override object GetValue(object obj)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.InternalGetValue(obj, ref stackCrawlMark);
		}

		// Token: 0x06004649 RID: 17993 RVA: 0x00103934 File Offset: 0x00101B34
		public override object GetRawConstantValue()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600464A RID: 17994 RVA: 0x0010393B File Offset: 0x00101B3B
		[SecuritySafeCritical]
		[DebuggerStepThrough]
		[DebuggerHidden]
		public unsafe override object GetValueDirect(TypedReference obj)
		{
			if (obj.IsNull)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_TypedReference_Null"));
			}
			return RuntimeFieldHandle.GetValueDirect(this, (RuntimeType)this.FieldType, (void*)(&obj), (RuntimeType)this.DeclaringType);
		}

		// Token: 0x0600464B RID: 17995 RVA: 0x00103978 File Offset: 0x00101B78
		[SecuritySafeCritical]
		[DebuggerStepThrough]
		[DebuggerHidden]
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.InternalSetValue(obj, value, invokeAttr, binder, culture, ref stackCrawlMark);
		}

		// Token: 0x0600464C RID: 17996 RVA: 0x00103996 File Offset: 0x00101B96
		[SecuritySafeCritical]
		[DebuggerStepThrough]
		[DebuggerHidden]
		public unsafe override void SetValueDirect(TypedReference obj, object value)
		{
			if (obj.IsNull)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_TypedReference_Null"));
			}
			RuntimeFieldHandle.SetValueDirect(this, (RuntimeType)this.FieldType, (void*)(&obj), value, (RuntimeType)this.DeclaringType);
		}

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x0600464D RID: 17997 RVA: 0x001039D4 File Offset: 0x00101BD4
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				Type declaringType = this.DeclaringType;
				if ((declaringType == null && this.Module.Assembly.ReflectionOnly) || declaringType is ReflectionOnlyType)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotAllowedInReflectionOnly"));
				}
				return new RuntimeFieldHandle(this);
			}
		}

		// Token: 0x0600464E RID: 17998 RVA: 0x00103A21 File Offset: 0x00101C21
		internal IntPtr GetFieldHandle()
		{
			return this.m_fieldHandle;
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x0600464F RID: 17999 RVA: 0x00103A29 File Offset: 0x00101C29
		public override FieldAttributes Attributes
		{
			get
			{
				return this.m_fieldAttributes;
			}
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x06004650 RID: 18000 RVA: 0x00103A31 File Offset: 0x00101C31
		public override Type FieldType
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_fieldType == null)
				{
					this.m_fieldType = new Signature(this, this.m_declaringType).FieldType;
				}
				return this.m_fieldType;
			}
		}

		// Token: 0x06004651 RID: 18001 RVA: 0x00103A5E File Offset: 0x00101C5E
		[SecuritySafeCritical]
		public override Type[] GetRequiredCustomModifiers()
		{
			return new Signature(this, this.m_declaringType).GetCustomModifiers(1, true);
		}

		// Token: 0x06004652 RID: 18002 RVA: 0x00103A73 File Offset: 0x00101C73
		[SecuritySafeCritical]
		public override Type[] GetOptionalCustomModifiers()
		{
			return new Signature(this, this.m_declaringType).GetCustomModifiers(1, false);
		}

		// Token: 0x04001CC4 RID: 7364
		private IntPtr m_fieldHandle;

		// Token: 0x04001CC5 RID: 7365
		private FieldAttributes m_fieldAttributes;

		// Token: 0x04001CC6 RID: 7366
		private string m_name;

		// Token: 0x04001CC7 RID: 7367
		private RuntimeType m_fieldType;

		// Token: 0x04001CC8 RID: 7368
		private INVOCATION_FLAGS m_invocationFlags;
	}
}
