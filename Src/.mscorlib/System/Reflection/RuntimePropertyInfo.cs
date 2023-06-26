using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Serialization;
using System.Security;
using System.Text;

namespace System.Reflection
{
	// Token: 0x0200061A RID: 1562
	[Serializable]
	internal sealed class RuntimePropertyInfo : PropertyInfo, ISerializable
	{
		// Token: 0x0600488E RID: 18574 RVA: 0x00108648 File Offset: 0x00106848
		[SecurityCritical]
		internal RuntimePropertyInfo(int tkProperty, RuntimeType declaredType, RuntimeType.RuntimeTypeCache reflectedTypeCache, out bool isPrivate)
		{
			MetadataImport metadataImport = declaredType.GetRuntimeModule().MetadataImport;
			this.m_token = tkProperty;
			this.m_reflectedTypeCache = reflectedTypeCache;
			this.m_declaringType = declaredType;
			ConstArray constArray;
			metadataImport.GetPropertyProps(tkProperty, out this.m_utf8name, out this.m_flags, out constArray);
			RuntimeMethodInfo runtimeMethodInfo;
			Associates.AssignAssociates(metadataImport, tkProperty, declaredType, reflectedTypeCache.GetRuntimeType(), out runtimeMethodInfo, out runtimeMethodInfo, out runtimeMethodInfo, out this.m_getterMethod, out this.m_setterMethod, out this.m_otherMethod, out isPrivate, out this.m_bindingFlags);
		}

		// Token: 0x0600488F RID: 18575 RVA: 0x001086C0 File Offset: 0x001068C0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal override bool CacheEquals(object o)
		{
			RuntimePropertyInfo runtimePropertyInfo = o as RuntimePropertyInfo;
			return runtimePropertyInfo != null && runtimePropertyInfo.m_token == this.m_token && RuntimeTypeHandle.GetModule(this.m_declaringType).Equals(RuntimeTypeHandle.GetModule(runtimePropertyInfo.m_declaringType));
		}

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x06004890 RID: 18576 RVA: 0x00108704 File Offset: 0x00106904
		internal unsafe Signature Signature
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_signature == null)
				{
					void* ptr;
					PropertyAttributes propertyAttributes;
					ConstArray constArray;
					this.GetRuntimeModule().MetadataImport.GetPropertyProps(this.m_token, out ptr, out propertyAttributes, out constArray);
					this.m_signature = new Signature(constArray.Signature.ToPointer(), constArray.Length, this.m_declaringType);
				}
				return this.m_signature;
			}
		}

		// Token: 0x06004891 RID: 18577 RVA: 0x00108766 File Offset: 0x00106966
		internal bool EqualsSig(RuntimePropertyInfo target)
		{
			return Signature.CompareSig(this.Signature, target.Signature);
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x06004892 RID: 18578 RVA: 0x00108779 File Offset: 0x00106979
		internal BindingFlags BindingFlags
		{
			get
			{
				return this.m_bindingFlags;
			}
		}

		// Token: 0x06004893 RID: 18579 RVA: 0x00108781 File Offset: 0x00106981
		public override string ToString()
		{
			return this.FormatNameAndSig(false);
		}

		// Token: 0x06004894 RID: 18580 RVA: 0x0010878C File Offset: 0x0010698C
		private string FormatNameAndSig(bool serialization)
		{
			StringBuilder stringBuilder = new StringBuilder(this.PropertyType.FormatTypeName(serialization));
			stringBuilder.Append(" ");
			stringBuilder.Append(this.Name);
			RuntimeType[] arguments = this.Signature.Arguments;
			if (arguments.Length != 0)
			{
				stringBuilder.Append(" [");
				StringBuilder stringBuilder2 = stringBuilder;
				Type[] array = arguments;
				stringBuilder2.Append(MethodBase.ConstructParameters(array, this.Signature.CallingConvention, serialization));
				stringBuilder.Append("]");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004895 RID: 18581 RVA: 0x0010880D File Offset: 0x00106A0D
		public override object[] GetCustomAttributes(bool inherit)
		{
			return CustomAttribute.GetCustomAttributes(this, typeof(object) as RuntimeType);
		}

		// Token: 0x06004896 RID: 18582 RVA: 0x00108824 File Offset: 0x00106A24
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

		// Token: 0x06004897 RID: 18583 RVA: 0x00108878 File Offset: 0x00106A78
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

		// Token: 0x06004898 RID: 18584 RVA: 0x001088CA File Offset: 0x00106ACA
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributesInternal(this);
		}

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x06004899 RID: 18585 RVA: 0x001088D2 File Offset: 0x00106AD2
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Property;
			}
		}

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x0600489A RID: 18586 RVA: 0x001088D8 File Offset: 0x00106AD8
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

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x0600489B RID: 18587 RVA: 0x00108912 File Offset: 0x00106B12
		public override Type DeclaringType
		{
			get
			{
				return this.m_declaringType;
			}
		}

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x0600489C RID: 18588 RVA: 0x0010891A File Offset: 0x00106B1A
		public override Type ReflectedType
		{
			get
			{
				return this.ReflectedTypeInternal;
			}
		}

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x0600489D RID: 18589 RVA: 0x00108922 File Offset: 0x00106B22
		private RuntimeType ReflectedTypeInternal
		{
			get
			{
				return this.m_reflectedTypeCache.GetRuntimeType();
			}
		}

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x0600489E RID: 18590 RVA: 0x0010892F File Offset: 0x00106B2F
		public override int MetadataToken
		{
			get
			{
				return this.m_token;
			}
		}

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x0600489F RID: 18591 RVA: 0x00108937 File Offset: 0x00106B37
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x060048A0 RID: 18592 RVA: 0x0010893F File Offset: 0x00106B3F
		internal RuntimeModule GetRuntimeModule()
		{
			return this.m_declaringType.GetRuntimeModule();
		}

		// Token: 0x060048A1 RID: 18593 RVA: 0x0010894C File Offset: 0x00106B4C
		public override Type[] GetRequiredCustomModifiers()
		{
			return this.Signature.GetCustomModifiers(0, true);
		}

		// Token: 0x060048A2 RID: 18594 RVA: 0x0010895B File Offset: 0x00106B5B
		public override Type[] GetOptionalCustomModifiers()
		{
			return this.Signature.GetCustomModifiers(0, false);
		}

		// Token: 0x060048A3 RID: 18595 RVA: 0x0010896C File Offset: 0x00106B6C
		[SecuritySafeCritical]
		internal object GetConstantValue(bool raw)
		{
			object value = MdConstant.GetValue(this.GetRuntimeModule().MetadataImport, this.m_token, this.PropertyType.GetTypeHandleInternal(), raw);
			if (value == DBNull.Value)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Arg_EnumLitValueNotFound"));
			}
			return value;
		}

		// Token: 0x060048A4 RID: 18596 RVA: 0x001089B5 File Offset: 0x00106BB5
		public override object GetConstantValue()
		{
			return this.GetConstantValue(false);
		}

		// Token: 0x060048A5 RID: 18597 RVA: 0x001089BE File Offset: 0x00106BBE
		public override object GetRawConstantValue()
		{
			return this.GetConstantValue(true);
		}

		// Token: 0x060048A6 RID: 18598 RVA: 0x001089C8 File Offset: 0x00106BC8
		public override MethodInfo[] GetAccessors(bool nonPublic)
		{
			List<MethodInfo> list = new List<MethodInfo>();
			if (Associates.IncludeAccessor(this.m_getterMethod, nonPublic))
			{
				list.Add(this.m_getterMethod);
			}
			if (Associates.IncludeAccessor(this.m_setterMethod, nonPublic))
			{
				list.Add(this.m_setterMethod);
			}
			if (this.m_otherMethod != null)
			{
				for (int i = 0; i < this.m_otherMethod.Length; i++)
				{
					if (Associates.IncludeAccessor(this.m_otherMethod[i], nonPublic))
					{
						list.Add(this.m_otherMethod[i]);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x060048A7 RID: 18599 RVA: 0x00108A4E File Offset: 0x00106C4E
		public override Type PropertyType
		{
			get
			{
				return this.Signature.ReturnType;
			}
		}

		// Token: 0x060048A8 RID: 18600 RVA: 0x00108A5B File Offset: 0x00106C5B
		public override MethodInfo GetGetMethod(bool nonPublic)
		{
			if (!Associates.IncludeAccessor(this.m_getterMethod, nonPublic))
			{
				return null;
			}
			return this.m_getterMethod;
		}

		// Token: 0x060048A9 RID: 18601 RVA: 0x00108A73 File Offset: 0x00106C73
		public override MethodInfo GetSetMethod(bool nonPublic)
		{
			if (!Associates.IncludeAccessor(this.m_setterMethod, nonPublic))
			{
				return null;
			}
			return this.m_setterMethod;
		}

		// Token: 0x060048AA RID: 18602 RVA: 0x00108A8C File Offset: 0x00106C8C
		public override ParameterInfo[] GetIndexParameters()
		{
			ParameterInfo[] indexParametersNoCopy = this.GetIndexParametersNoCopy();
			int num = indexParametersNoCopy.Length;
			if (num == 0)
			{
				return indexParametersNoCopy;
			}
			ParameterInfo[] array = new ParameterInfo[num];
			Array.Copy(indexParametersNoCopy, array, num);
			return array;
		}

		// Token: 0x060048AB RID: 18603 RVA: 0x00108ABC File Offset: 0x00106CBC
		internal ParameterInfo[] GetIndexParametersNoCopy()
		{
			if (this.m_parameters == null)
			{
				int num = 0;
				ParameterInfo[] array = null;
				MethodInfo methodInfo = this.GetGetMethod(true);
				if (methodInfo != null)
				{
					array = methodInfo.GetParametersNoCopy();
					num = array.Length;
				}
				else
				{
					methodInfo = this.GetSetMethod(true);
					if (methodInfo != null)
					{
						array = methodInfo.GetParametersNoCopy();
						num = array.Length - 1;
					}
				}
				ParameterInfo[] array2 = new ParameterInfo[num];
				for (int i = 0; i < num; i++)
				{
					array2[i] = new RuntimeParameterInfo((RuntimeParameterInfo)array[i], this);
				}
				this.m_parameters = array2;
			}
			return this.m_parameters;
		}

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x060048AC RID: 18604 RVA: 0x00108B48 File Offset: 0x00106D48
		public override PropertyAttributes Attributes
		{
			get
			{
				return this.m_flags;
			}
		}

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x060048AD RID: 18605 RVA: 0x00108B50 File Offset: 0x00106D50
		public override bool CanRead
		{
			get
			{
				return this.m_getterMethod != null;
			}
		}

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x060048AE RID: 18606 RVA: 0x00108B5E File Offset: 0x00106D5E
		public override bool CanWrite
		{
			get
			{
				return this.m_setterMethod != null;
			}
		}

		// Token: 0x060048AF RID: 18607 RVA: 0x00108B6C File Offset: 0x00106D6C
		[DebuggerStepThrough]
		[DebuggerHidden]
		public override object GetValue(object obj, object[] index)
		{
			return this.GetValue(obj, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, index, null);
		}

		// Token: 0x060048B0 RID: 18608 RVA: 0x00108B7C File Offset: 0x00106D7C
		[DebuggerStepThrough]
		[DebuggerHidden]
		public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			MethodInfo getMethod = this.GetGetMethod(true);
			if (getMethod == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_GetMethNotFnd"));
			}
			return getMethod.Invoke(obj, invokeAttr, binder, index, null);
		}

		// Token: 0x060048B1 RID: 18609 RVA: 0x00108BB6 File Offset: 0x00106DB6
		[DebuggerStepThrough]
		[DebuggerHidden]
		public override void SetValue(object obj, object value, object[] index)
		{
			this.SetValue(obj, value, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, index, null);
		}

		// Token: 0x060048B2 RID: 18610 RVA: 0x00108BC8 File Offset: 0x00106DC8
		[DebuggerStepThrough]
		[DebuggerHidden]
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			MethodInfo setMethod = this.GetSetMethod(true);
			if (setMethod == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_SetMethNotFnd"));
			}
			object[] array;
			if (index != null)
			{
				array = new object[index.Length + 1];
				for (int i = 0; i < index.Length; i++)
				{
					array[i] = index[i];
				}
				array[index.Length] = value;
			}
			else
			{
				array = new object[] { value };
			}
			setMethod.Invoke(obj, invokeAttr, binder, array, culture);
		}

		// Token: 0x060048B3 RID: 18611 RVA: 0x00108C40 File Offset: 0x00106E40
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			MemberInfoSerializationHolder.GetSerializationInfo(info, this.Name, this.ReflectedTypeInternal, this.ToString(), this.SerializationToString(), MemberTypes.Property, null);
		}

		// Token: 0x060048B4 RID: 18612 RVA: 0x00108C71 File Offset: 0x00106E71
		internal string SerializationToString()
		{
			return this.FormatNameAndSig(true);
		}

		// Token: 0x04001E13 RID: 7699
		private int m_token;

		// Token: 0x04001E14 RID: 7700
		private string m_name;

		// Token: 0x04001E15 RID: 7701
		[SecurityCritical]
		private unsafe void* m_utf8name;

		// Token: 0x04001E16 RID: 7702
		private PropertyAttributes m_flags;

		// Token: 0x04001E17 RID: 7703
		private RuntimeType.RuntimeTypeCache m_reflectedTypeCache;

		// Token: 0x04001E18 RID: 7704
		private RuntimeMethodInfo m_getterMethod;

		// Token: 0x04001E19 RID: 7705
		private RuntimeMethodInfo m_setterMethod;

		// Token: 0x04001E1A RID: 7706
		private MethodInfo[] m_otherMethod;

		// Token: 0x04001E1B RID: 7707
		private RuntimeType m_declaringType;

		// Token: 0x04001E1C RID: 7708
		private BindingFlags m_bindingFlags;

		// Token: 0x04001E1D RID: 7709
		private Signature m_signature;

		// Token: 0x04001E1E RID: 7710
		private ParameterInfo[] m_parameters;
	}
}
