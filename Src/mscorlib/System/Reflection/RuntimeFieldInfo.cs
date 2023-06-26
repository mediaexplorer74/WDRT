using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Reflection
{
	// Token: 0x020005E4 RID: 1508
	[Serializable]
	internal abstract class RuntimeFieldInfo : FieldInfo, ISerializable
	{
		// Token: 0x06004626 RID: 17958 RVA: 0x00103183 File Offset: 0x00101383
		protected RuntimeFieldInfo()
		{
		}

		// Token: 0x06004627 RID: 17959 RVA: 0x0010318B File Offset: 0x0010138B
		protected RuntimeFieldInfo(RuntimeType.RuntimeTypeCache reflectedTypeCache, RuntimeType declaringType, BindingFlags bindingFlags)
		{
			this.m_bindingFlags = bindingFlags;
			this.m_declaringType = declaringType;
			this.m_reflectedTypeCache = reflectedTypeCache;
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06004628 RID: 17960 RVA: 0x001031A8 File Offset: 0x001013A8
		internal RemotingFieldCachedData RemotingCache
		{
			get
			{
				RemotingFieldCachedData remotingFieldCachedData = this.m_cachedData;
				if (remotingFieldCachedData == null)
				{
					remotingFieldCachedData = new RemotingFieldCachedData(this);
					RemotingFieldCachedData remotingFieldCachedData2 = Interlocked.CompareExchange<RemotingFieldCachedData>(ref this.m_cachedData, remotingFieldCachedData, null);
					if (remotingFieldCachedData2 != null)
					{
						remotingFieldCachedData = remotingFieldCachedData2;
					}
				}
				return remotingFieldCachedData;
			}
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x06004629 RID: 17961 RVA: 0x001031DA File Offset: 0x001013DA
		internal BindingFlags BindingFlags
		{
			get
			{
				return this.m_bindingFlags;
			}
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x0600462A RID: 17962 RVA: 0x001031E2 File Offset: 0x001013E2
		private RuntimeType ReflectedTypeInternal
		{
			get
			{
				return this.m_reflectedTypeCache.GetRuntimeType();
			}
		}

		// Token: 0x0600462B RID: 17963 RVA: 0x001031EF File Offset: 0x001013EF
		internal RuntimeType GetDeclaringTypeInternal()
		{
			return this.m_declaringType;
		}

		// Token: 0x0600462C RID: 17964 RVA: 0x001031F7 File Offset: 0x001013F7
		internal RuntimeType GetRuntimeType()
		{
			return this.m_declaringType;
		}

		// Token: 0x0600462D RID: 17965
		internal abstract RuntimeModule GetRuntimeModule();

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x0600462E RID: 17966 RVA: 0x001031FF File Offset: 0x001013FF
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Field;
			}
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x0600462F RID: 17967 RVA: 0x00103202 File Offset: 0x00101402
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

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x06004630 RID: 17968 RVA: 0x00103219 File Offset: 0x00101419
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

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x06004631 RID: 17969 RVA: 0x00103230 File Offset: 0x00101430
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x06004632 RID: 17970 RVA: 0x00103238 File Offset: 0x00101438
		public override string ToString()
		{
			if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
			{
				return this.FieldType.ToString() + " " + this.Name;
			}
			return this.FieldType.FormatTypeName() + " " + this.Name;
		}

		// Token: 0x06004633 RID: 17971 RVA: 0x00103278 File Offset: 0x00101478
		public override object[] GetCustomAttributes(bool inherit)
		{
			return CustomAttribute.GetCustomAttributes(this, typeof(object) as RuntimeType);
		}

		// Token: 0x06004634 RID: 17972 RVA: 0x00103290 File Offset: 0x00101490
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

		// Token: 0x06004635 RID: 17973 RVA: 0x001032E4 File Offset: 0x001014E4
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

		// Token: 0x06004636 RID: 17974 RVA: 0x00103336 File Offset: 0x00101536
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributesInternal(this);
		}

		// Token: 0x06004637 RID: 17975 RVA: 0x0010333E File Offset: 0x0010153E
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			MemberInfoSerializationHolder.GetSerializationInfo(info, this.Name, this.ReflectedTypeInternal, this.ToString(), MemberTypes.Field);
		}

		// Token: 0x04001CC0 RID: 7360
		private BindingFlags m_bindingFlags;

		// Token: 0x04001CC1 RID: 7361
		protected RuntimeType.RuntimeTypeCache m_reflectedTypeCache;

		// Token: 0x04001CC2 RID: 7362
		protected RuntimeType m_declaringType;

		// Token: 0x04001CC3 RID: 7363
		private RemotingFieldCachedData m_cachedData;
	}
}
