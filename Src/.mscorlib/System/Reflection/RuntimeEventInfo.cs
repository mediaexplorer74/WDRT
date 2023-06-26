using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Serialization;
using System.Security;

namespace System.Reflection
{
	// Token: 0x020005E1 RID: 1505
	[Serializable]
	internal sealed class RuntimeEventInfo : EventInfo, ISerializable
	{
		// Token: 0x060045E8 RID: 17896 RVA: 0x00102BDE File Offset: 0x00100DDE
		internal RuntimeEventInfo()
		{
		}

		// Token: 0x060045E9 RID: 17897 RVA: 0x00102BE8 File Offset: 0x00100DE8
		[SecurityCritical]
		internal RuntimeEventInfo(int tkEvent, RuntimeType declaredType, RuntimeType.RuntimeTypeCache reflectedTypeCache, out bool isPrivate)
		{
			MetadataImport metadataImport = declaredType.GetRuntimeModule().MetadataImport;
			this.m_token = tkEvent;
			this.m_reflectedTypeCache = reflectedTypeCache;
			this.m_declaringType = declaredType;
			RuntimeType runtimeType = reflectedTypeCache.GetRuntimeType();
			metadataImport.GetEventProps(tkEvent, out this.m_utf8name, out this.m_flags);
			RuntimeMethodInfo runtimeMethodInfo;
			Associates.AssignAssociates(metadataImport, tkEvent, declaredType, runtimeType, out this.m_addMethod, out this.m_removeMethod, out this.m_raiseMethod, out runtimeMethodInfo, out runtimeMethodInfo, out this.m_otherMethod, out isPrivate, out this.m_bindingFlags);
		}

		// Token: 0x060045EA RID: 17898 RVA: 0x00102C64 File Offset: 0x00100E64
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal override bool CacheEquals(object o)
		{
			RuntimeEventInfo runtimeEventInfo = o as RuntimeEventInfo;
			return runtimeEventInfo != null && runtimeEventInfo.m_token == this.m_token && RuntimeTypeHandle.GetModule(this.m_declaringType).Equals(RuntimeTypeHandle.GetModule(runtimeEventInfo.m_declaringType));
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x060045EB RID: 17899 RVA: 0x00102CA8 File Offset: 0x00100EA8
		internal BindingFlags BindingFlags
		{
			get
			{
				return this.m_bindingFlags;
			}
		}

		// Token: 0x060045EC RID: 17900 RVA: 0x00102CB0 File Offset: 0x00100EB0
		public override string ToString()
		{
			if (this.m_addMethod == null || this.m_addMethod.GetParametersNoCopy().Length == 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NoPublicAddMethod"));
			}
			return this.m_addMethod.GetParametersNoCopy()[0].ParameterType.FormatTypeName() + " " + this.Name;
		}

		// Token: 0x060045ED RID: 17901 RVA: 0x00102D10 File Offset: 0x00100F10
		public override object[] GetCustomAttributes(bool inherit)
		{
			return CustomAttribute.GetCustomAttributes(this, typeof(object) as RuntimeType);
		}

		// Token: 0x060045EE RID: 17902 RVA: 0x00102D28 File Offset: 0x00100F28
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

		// Token: 0x060045EF RID: 17903 RVA: 0x00102D7C File Offset: 0x00100F7C
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

		// Token: 0x060045F0 RID: 17904 RVA: 0x00102DCE File Offset: 0x00100FCE
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributesInternal(this);
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x060045F1 RID: 17905 RVA: 0x00102DD6 File Offset: 0x00100FD6
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Event;
			}
		}

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x060045F2 RID: 17906 RVA: 0x00102DDC File Offset: 0x00100FDC
		public override string Name
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_name == null)
				{
					this.m_name = new Utf8String(this.m_utf8name).ToString();
				}
				return this.m_name;
			}
		}

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x060045F3 RID: 17907 RVA: 0x00102E16 File Offset: 0x00101016
		public override Type DeclaringType
		{
			get
			{
				return this.m_declaringType;
			}
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x060045F4 RID: 17908 RVA: 0x00102E1E File Offset: 0x0010101E
		public override Type ReflectedType
		{
			get
			{
				return this.ReflectedTypeInternal;
			}
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x060045F5 RID: 17909 RVA: 0x00102E26 File Offset: 0x00101026
		private RuntimeType ReflectedTypeInternal
		{
			get
			{
				return this.m_reflectedTypeCache.GetRuntimeType();
			}
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x060045F6 RID: 17910 RVA: 0x00102E33 File Offset: 0x00101033
		public override int MetadataToken
		{
			get
			{
				return this.m_token;
			}
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x060045F7 RID: 17911 RVA: 0x00102E3B File Offset: 0x0010103B
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x060045F8 RID: 17912 RVA: 0x00102E43 File Offset: 0x00101043
		internal RuntimeModule GetRuntimeModule()
		{
			return this.m_declaringType.GetRuntimeModule();
		}

		// Token: 0x060045F9 RID: 17913 RVA: 0x00102E50 File Offset: 0x00101050
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			MemberInfoSerializationHolder.GetSerializationInfo(info, this.Name, this.ReflectedTypeInternal, null, MemberTypes.Event);
		}

		// Token: 0x060045FA RID: 17914 RVA: 0x00102E74 File Offset: 0x00101074
		public override MethodInfo[] GetOtherMethods(bool nonPublic)
		{
			List<MethodInfo> list = new List<MethodInfo>();
			if (this.m_otherMethod == null)
			{
				return new MethodInfo[0];
			}
			for (int i = 0; i < this.m_otherMethod.Length; i++)
			{
				if (Associates.IncludeAccessor(this.m_otherMethod[i], nonPublic))
				{
					list.Add(this.m_otherMethod[i]);
				}
			}
			return list.ToArray();
		}

		// Token: 0x060045FB RID: 17915 RVA: 0x00102ECD File Offset: 0x001010CD
		public override MethodInfo GetAddMethod(bool nonPublic)
		{
			if (!Associates.IncludeAccessor(this.m_addMethod, nonPublic))
			{
				return null;
			}
			return this.m_addMethod;
		}

		// Token: 0x060045FC RID: 17916 RVA: 0x00102EE5 File Offset: 0x001010E5
		public override MethodInfo GetRemoveMethod(bool nonPublic)
		{
			if (!Associates.IncludeAccessor(this.m_removeMethod, nonPublic))
			{
				return null;
			}
			return this.m_removeMethod;
		}

		// Token: 0x060045FD RID: 17917 RVA: 0x00102EFD File Offset: 0x001010FD
		public override MethodInfo GetRaiseMethod(bool nonPublic)
		{
			if (!Associates.IncludeAccessor(this.m_raiseMethod, nonPublic))
			{
				return null;
			}
			return this.m_raiseMethod;
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x060045FE RID: 17918 RVA: 0x00102F15 File Offset: 0x00101115
		public override EventAttributes Attributes
		{
			get
			{
				return this.m_flags;
			}
		}

		// Token: 0x04001CA1 RID: 7329
		private int m_token;

		// Token: 0x04001CA2 RID: 7330
		private EventAttributes m_flags;

		// Token: 0x04001CA3 RID: 7331
		private string m_name;

		// Token: 0x04001CA4 RID: 7332
		[SecurityCritical]
		private unsafe void* m_utf8name;

		// Token: 0x04001CA5 RID: 7333
		private RuntimeType.RuntimeTypeCache m_reflectedTypeCache;

		// Token: 0x04001CA6 RID: 7334
		private RuntimeMethodInfo m_addMethod;

		// Token: 0x04001CA7 RID: 7335
		private RuntimeMethodInfo m_removeMethod;

		// Token: 0x04001CA8 RID: 7336
		private RuntimeMethodInfo m_raiseMethod;

		// Token: 0x04001CA9 RID: 7337
		private MethodInfo[] m_otherMethod;

		// Token: 0x04001CAA RID: 7338
		private RuntimeType m_declaringType;

		// Token: 0x04001CAB RID: 7339
		private BindingFlags m_bindingFlags;
	}
}
