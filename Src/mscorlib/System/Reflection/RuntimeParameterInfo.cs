using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Reflection
{
	// Token: 0x02000615 RID: 1557
	[Serializable]
	internal sealed class RuntimeParameterInfo : ParameterInfo, ISerializable
	{
		// Token: 0x06004846 RID: 18502 RVA: 0x00107B08 File Offset: 0x00105D08
		[SecurityCritical]
		internal static ParameterInfo[] GetParameters(IRuntimeMethodInfo method, MemberInfo member, Signature sig)
		{
			ParameterInfo parameterInfo;
			return RuntimeParameterInfo.GetParameters(method, member, sig, out parameterInfo, false);
		}

		// Token: 0x06004847 RID: 18503 RVA: 0x00107B20 File Offset: 0x00105D20
		[SecurityCritical]
		internal static ParameterInfo GetReturnParameter(IRuntimeMethodInfo method, MemberInfo member, Signature sig)
		{
			ParameterInfo parameterInfo;
			RuntimeParameterInfo.GetParameters(method, member, sig, out parameterInfo, true);
			return parameterInfo;
		}

		// Token: 0x06004848 RID: 18504 RVA: 0x00107B3C File Offset: 0x00105D3C
		[SecurityCritical]
		internal static ParameterInfo[] GetParameters(IRuntimeMethodInfo methodHandle, MemberInfo member, Signature sig, out ParameterInfo returnParameter, bool fetchReturnParameter)
		{
			returnParameter = null;
			int num = sig.Arguments.Length;
			ParameterInfo[] array = (fetchReturnParameter ? null : new ParameterInfo[num]);
			int methodDef = RuntimeMethodHandle.GetMethodDef(methodHandle);
			int num2 = 0;
			if (!System.Reflection.MetadataToken.IsNullToken(methodDef))
			{
				MetadataImport metadataImport = RuntimeTypeHandle.GetMetadataImport(RuntimeMethodHandle.GetDeclaringType(methodHandle));
				MetadataEnumResult metadataEnumResult;
				metadataImport.EnumParams(methodDef, out metadataEnumResult);
				num2 = metadataEnumResult.Length;
				if (num2 > num + 1)
				{
					throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ParameterSignatureMismatch"));
				}
				for (int i = 0; i < num2; i++)
				{
					int num3 = metadataEnumResult[i];
					int num4;
					ParameterAttributes parameterAttributes;
					metadataImport.GetParamDefProps(num3, out num4, out parameterAttributes);
					num4--;
					if (fetchReturnParameter && num4 == -1)
					{
						if (returnParameter != null)
						{
							throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ParameterSignatureMismatch"));
						}
						returnParameter = new RuntimeParameterInfo(sig, metadataImport, num3, num4, parameterAttributes, member);
					}
					else if (!fetchReturnParameter && num4 >= 0)
					{
						if (num4 >= num)
						{
							throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ParameterSignatureMismatch"));
						}
						array[num4] = new RuntimeParameterInfo(sig, metadataImport, num3, num4, parameterAttributes, member);
					}
				}
			}
			if (fetchReturnParameter)
			{
				if (returnParameter == null)
				{
					returnParameter = new RuntimeParameterInfo(sig, MetadataImport.EmptyImport, 0, -1, ParameterAttributes.None, member);
				}
			}
			else if (num2 < array.Length + 1)
			{
				for (int j = 0; j < array.Length; j++)
				{
					if (array[j] == null)
					{
						array[j] = new RuntimeParameterInfo(sig, MetadataImport.EmptyImport, 0, j, ParameterAttributes.None, member);
					}
				}
			}
			return array;
		}

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x06004849 RID: 18505 RVA: 0x00107C94 File Offset: 0x00105E94
		internal MethodBase DefiningMethod
		{
			get
			{
				return (this.m_originalMember != null) ? this.m_originalMember : (this.MemberImpl as MethodBase);
			}
		}

		// Token: 0x0600484A RID: 18506 RVA: 0x00107CC4 File Offset: 0x00105EC4
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.SetType(typeof(ParameterInfo));
			info.AddValue("AttrsImpl", this.Attributes);
			info.AddValue("ClassImpl", this.ParameterType);
			info.AddValue("DefaultValueImpl", this.DefaultValue);
			info.AddValue("MemberImpl", this.Member);
			info.AddValue("NameImpl", this.Name);
			info.AddValue("PositionImpl", this.Position);
			info.AddValue("_token", this.m_tkParamDef);
		}

		// Token: 0x0600484B RID: 18507 RVA: 0x00107D6B File Offset: 0x00105F6B
		internal RuntimeParameterInfo(RuntimeParameterInfo accessor, RuntimePropertyInfo property)
			: this(accessor, property)
		{
			this.m_signature = property.Signature;
		}

		// Token: 0x0600484C RID: 18508 RVA: 0x00107D84 File Offset: 0x00105F84
		private RuntimeParameterInfo(RuntimeParameterInfo accessor, MemberInfo member)
		{
			this.MemberImpl = member;
			this.m_originalMember = accessor.MemberImpl as MethodBase;
			this.NameImpl = accessor.Name;
			this.m_nameIsCached = true;
			this.ClassImpl = accessor.ParameterType;
			this.PositionImpl = accessor.Position;
			this.AttrsImpl = accessor.Attributes;
			this.m_tkParamDef = (System.Reflection.MetadataToken.IsNullToken(accessor.MetadataToken) ? 134217728 : accessor.MetadataToken);
			this.m_scope = accessor.m_scope;
		}

		// Token: 0x0600484D RID: 18509 RVA: 0x00107E14 File Offset: 0x00106014
		private RuntimeParameterInfo(Signature signature, MetadataImport scope, int tkParamDef, int position, ParameterAttributes attributes, MemberInfo member)
		{
			this.PositionImpl = position;
			this.MemberImpl = member;
			this.m_signature = signature;
			this.m_tkParamDef = (System.Reflection.MetadataToken.IsNullToken(tkParamDef) ? 134217728 : tkParamDef);
			this.m_scope = scope;
			this.AttrsImpl = attributes;
			this.ClassImpl = null;
			this.NameImpl = null;
		}

		// Token: 0x0600484E RID: 18510 RVA: 0x00107E74 File Offset: 0x00106074
		internal RuntimeParameterInfo(MethodInfo owner, string name, Type parameterType, int position)
		{
			this.MemberImpl = owner;
			this.NameImpl = name;
			this.m_nameIsCached = true;
			this.m_noMetadata = true;
			this.ClassImpl = parameterType;
			this.PositionImpl = position;
			this.AttrsImpl = ParameterAttributes.None;
			this.m_tkParamDef = 134217728;
			this.m_scope = MetadataImport.EmptyImport;
		}

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x0600484F RID: 18511 RVA: 0x00107ED4 File Offset: 0x001060D4
		public override Type ParameterType
		{
			get
			{
				if (this.ClassImpl == null)
				{
					RuntimeType runtimeType;
					if (this.PositionImpl == -1)
					{
						runtimeType = this.m_signature.ReturnType;
					}
					else
					{
						runtimeType = this.m_signature.Arguments[this.PositionImpl];
					}
					this.ClassImpl = runtimeType;
				}
				return this.ClassImpl;
			}
		}

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x06004850 RID: 18512 RVA: 0x00107F28 File Offset: 0x00106128
		public override string Name
		{
			[SecuritySafeCritical]
			get
			{
				if (!this.m_nameIsCached)
				{
					if (!System.Reflection.MetadataToken.IsNullToken(this.m_tkParamDef))
					{
						string text = this.m_scope.GetName(this.m_tkParamDef).ToString();
						this.NameImpl = text;
					}
					this.m_nameIsCached = true;
				}
				return this.NameImpl;
			}
		}

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x06004851 RID: 18513 RVA: 0x00107F84 File Offset: 0x00106184
		public override bool HasDefaultValue
		{
			get
			{
				if (this.m_noMetadata || this.m_noDefaultValue)
				{
					return false;
				}
				object defaultValueInternal = this.GetDefaultValueInternal(false);
				return defaultValueInternal != DBNull.Value;
			}
		}

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x06004852 RID: 18514 RVA: 0x00107FB6 File Offset: 0x001061B6
		public override object DefaultValue
		{
			get
			{
				return this.GetDefaultValue(false);
			}
		}

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x06004853 RID: 18515 RVA: 0x00107FBF File Offset: 0x001061BF
		public override object RawDefaultValue
		{
			get
			{
				return this.GetDefaultValue(true);
			}
		}

		// Token: 0x06004854 RID: 18516 RVA: 0x00107FC8 File Offset: 0x001061C8
		private object GetDefaultValue(bool raw)
		{
			if (this.m_noMetadata)
			{
				return null;
			}
			object obj = this.GetDefaultValueInternal(raw);
			if (obj == DBNull.Value && base.IsOptional)
			{
				obj = Type.Missing;
			}
			return obj;
		}

		// Token: 0x06004855 RID: 18517 RVA: 0x00108000 File Offset: 0x00106200
		[SecuritySafeCritical]
		private object GetDefaultValueInternal(bool raw)
		{
			if (this.m_noDefaultValue)
			{
				return DBNull.Value;
			}
			object obj = null;
			if (this.ParameterType == typeof(DateTime))
			{
				if (raw)
				{
					CustomAttributeTypedArgument customAttributeTypedArgument = CustomAttributeData.Filter(CustomAttributeData.GetCustomAttributes(this), typeof(DateTimeConstantAttribute), 0);
					if (customAttributeTypedArgument.ArgumentType != null)
					{
						return new DateTime((long)customAttributeTypedArgument.Value);
					}
				}
				else
				{
					object[] customAttributes = this.GetCustomAttributes(typeof(DateTimeConstantAttribute), false);
					if (customAttributes != null && customAttributes.Length != 0)
					{
						return ((DateTimeConstantAttribute)customAttributes[0]).Value;
					}
				}
			}
			if (!System.Reflection.MetadataToken.IsNullToken(this.m_tkParamDef))
			{
				obj = MdConstant.GetValue(this.m_scope, this.m_tkParamDef, this.ParameterType.GetTypeHandleInternal(), raw);
			}
			if (obj == DBNull.Value)
			{
				if (raw)
				{
					using (IEnumerator<CustomAttributeData> enumerator = CustomAttributeData.GetCustomAttributes(this).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							CustomAttributeData customAttributeData = enumerator.Current;
							Type declaringType = customAttributeData.Constructor.DeclaringType;
							if (declaringType == typeof(DateTimeConstantAttribute))
							{
								obj = DateTimeConstantAttribute.GetRawDateTimeConstant(customAttributeData);
							}
							else if (declaringType == typeof(DecimalConstantAttribute))
							{
								obj = DecimalConstantAttribute.GetRawDecimalConstant(customAttributeData);
							}
							else if (declaringType.IsSubclassOf(RuntimeParameterInfo.s_CustomConstantAttributeType))
							{
								obj = CustomConstantAttribute.GetRawConstant(customAttributeData);
							}
						}
						goto IL_1A7;
					}
				}
				object[] array = this.GetCustomAttributes(RuntimeParameterInfo.s_CustomConstantAttributeType, false);
				if (array.Length != 0)
				{
					obj = ((CustomConstantAttribute)array[0]).Value;
				}
				else
				{
					array = this.GetCustomAttributes(RuntimeParameterInfo.s_DecimalConstantAttributeType, false);
					if (array.Length != 0)
					{
						obj = ((DecimalConstantAttribute)array[0]).Value;
					}
				}
			}
			IL_1A7:
			if (obj == DBNull.Value)
			{
				this.m_noDefaultValue = true;
			}
			return obj;
		}

		// Token: 0x06004856 RID: 18518 RVA: 0x001081D4 File Offset: 0x001063D4
		internal RuntimeModule GetRuntimeModule()
		{
			RuntimeMethodInfo runtimeMethodInfo = this.Member as RuntimeMethodInfo;
			RuntimeConstructorInfo runtimeConstructorInfo = this.Member as RuntimeConstructorInfo;
			RuntimePropertyInfo runtimePropertyInfo = this.Member as RuntimePropertyInfo;
			if (runtimeMethodInfo != null)
			{
				return runtimeMethodInfo.GetRuntimeModule();
			}
			if (runtimeConstructorInfo != null)
			{
				return runtimeConstructorInfo.GetRuntimeModule();
			}
			if (runtimePropertyInfo != null)
			{
				return runtimePropertyInfo.GetRuntimeModule();
			}
			return null;
		}

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x06004857 RID: 18519 RVA: 0x00108236 File Offset: 0x00106436
		public override int MetadataToken
		{
			get
			{
				return this.m_tkParamDef;
			}
		}

		// Token: 0x06004858 RID: 18520 RVA: 0x0010823E File Offset: 0x0010643E
		public override Type[] GetRequiredCustomModifiers()
		{
			return this.m_signature.GetCustomModifiers(this.PositionImpl + 1, true);
		}

		// Token: 0x06004859 RID: 18521 RVA: 0x00108254 File Offset: 0x00106454
		public override Type[] GetOptionalCustomModifiers()
		{
			return this.m_signature.GetCustomModifiers(this.PositionImpl + 1, false);
		}

		// Token: 0x0600485A RID: 18522 RVA: 0x0010826A File Offset: 0x0010646A
		public override object[] GetCustomAttributes(bool inherit)
		{
			if (System.Reflection.MetadataToken.IsNullToken(this.m_tkParamDef))
			{
				return EmptyArray<object>.Value;
			}
			return CustomAttribute.GetCustomAttributes(this, typeof(object) as RuntimeType);
		}

		// Token: 0x0600485B RID: 18523 RVA: 0x00108294 File Offset: 0x00106494
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (System.Reflection.MetadataToken.IsNullToken(this.m_tkParamDef))
			{
				return EmptyArray<object>.Value;
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "attributeType");
			}
			return CustomAttribute.GetCustomAttributes(this, runtimeType);
		}

		// Token: 0x0600485C RID: 18524 RVA: 0x001082FC File Offset: 0x001064FC
		[SecuritySafeCritical]
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (System.Reflection.MetadataToken.IsNullToken(this.m_tkParamDef))
			{
				return false;
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "attributeType");
			}
			return CustomAttribute.IsDefined(this, runtimeType);
		}

		// Token: 0x0600485D RID: 18525 RVA: 0x0010835D File Offset: 0x0010655D
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributesInternal(this);
		}

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x0600485E RID: 18526 RVA: 0x00108368 File Offset: 0x00106568
		internal RemotingParameterCachedData RemotingCache
		{
			get
			{
				RemotingParameterCachedData remotingParameterCachedData = this.m_cachedData;
				if (remotingParameterCachedData == null)
				{
					remotingParameterCachedData = new RemotingParameterCachedData(this);
					RemotingParameterCachedData remotingParameterCachedData2 = Interlocked.CompareExchange<RemotingParameterCachedData>(ref this.m_cachedData, remotingParameterCachedData, null);
					if (remotingParameterCachedData2 != null)
					{
						remotingParameterCachedData = remotingParameterCachedData2;
					}
				}
				return remotingParameterCachedData;
			}
		}

		// Token: 0x04001DFD RID: 7677
		private static readonly Type s_DecimalConstantAttributeType = typeof(DecimalConstantAttribute);

		// Token: 0x04001DFE RID: 7678
		private static readonly Type s_CustomConstantAttributeType = typeof(CustomConstantAttribute);

		// Token: 0x04001DFF RID: 7679
		[NonSerialized]
		private int m_tkParamDef;

		// Token: 0x04001E00 RID: 7680
		[NonSerialized]
		private MetadataImport m_scope;

		// Token: 0x04001E01 RID: 7681
		[NonSerialized]
		private Signature m_signature;

		// Token: 0x04001E02 RID: 7682
		[NonSerialized]
		private volatile bool m_nameIsCached;

		// Token: 0x04001E03 RID: 7683
		[NonSerialized]
		private readonly bool m_noMetadata;

		// Token: 0x04001E04 RID: 7684
		[NonSerialized]
		private bool m_noDefaultValue;

		// Token: 0x04001E05 RID: 7685
		[NonSerialized]
		private MethodBase m_originalMember;

		// Token: 0x04001E06 RID: 7686
		private RemotingParameterCachedData m_cachedData;
	}
}
