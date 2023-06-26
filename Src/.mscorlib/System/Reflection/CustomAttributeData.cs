using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection
{
	/// <summary>Provides access to custom attribute data for assemblies, modules, types, members and parameters that are loaded into the reflection-only context.</summary>
	// Token: 0x020005D2 RID: 1490
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class CustomAttributeData
	{
		/// <summary>Returns a list of <see cref="T:System.Reflection.CustomAttributeData" /> objects representing data about the attributes that have been applied to the target member.</summary>
		/// <param name="target">The member whose attribute data is to be retrieved.</param>
		/// <returns>A list of objects that represent data about the attributes that have been applied to the target member.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="target" /> is <see langword="null" />.</exception>
		// Token: 0x0600453F RID: 17727 RVA: 0x000FF462 File Offset: 0x000FD662
		public static IList<CustomAttributeData> GetCustomAttributes(MemberInfo target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			return target.GetCustomAttributesData();
		}

		/// <summary>Returns a list of <see cref="T:System.Reflection.CustomAttributeData" /> objects representing data about the attributes that have been applied to the target module.</summary>
		/// <param name="target">The module whose custom attribute data is to be retrieved.</param>
		/// <returns>A list of objects that represent data about the attributes that have been applied to the target module.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="target" /> is <see langword="null" />.</exception>
		// Token: 0x06004540 RID: 17728 RVA: 0x000FF47E File Offset: 0x000FD67E
		public static IList<CustomAttributeData> GetCustomAttributes(Module target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			return target.GetCustomAttributesData();
		}

		/// <summary>Returns a list of <see cref="T:System.Reflection.CustomAttributeData" /> objects representing data about the attributes that have been applied to the target assembly.</summary>
		/// <param name="target">The assembly whose custom attribute data is to be retrieved.</param>
		/// <returns>A list of objects that represent data about the attributes that have been applied to the target assembly.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="target" /> is <see langword="null" />.</exception>
		// Token: 0x06004541 RID: 17729 RVA: 0x000FF49A File Offset: 0x000FD69A
		public static IList<CustomAttributeData> GetCustomAttributes(Assembly target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			return target.GetCustomAttributesData();
		}

		/// <summary>Returns a list of <see cref="T:System.Reflection.CustomAttributeData" /> objects representing data about the attributes that have been applied to the target parameter.</summary>
		/// <param name="target">The parameter whose attribute data is to be retrieved.</param>
		/// <returns>A list of objects that represent data about the attributes that have been applied to the target parameter.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="target" /> is <see langword="null" />.</exception>
		// Token: 0x06004542 RID: 17730 RVA: 0x000FF4B6 File Offset: 0x000FD6B6
		public static IList<CustomAttributeData> GetCustomAttributes(ParameterInfo target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			return target.GetCustomAttributesData();
		}

		// Token: 0x06004543 RID: 17731 RVA: 0x000FF4CC File Offset: 0x000FD6CC
		[SecuritySafeCritical]
		internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeType target)
		{
			IList<CustomAttributeData> customAttributes = CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
			int num = 0;
			Attribute[] customAttributes2 = PseudoCustomAttribute.GetCustomAttributes(target, typeof(object) as RuntimeType, true, out num);
			if (num == 0)
			{
				return customAttributes;
			}
			CustomAttributeData[] array = new CustomAttributeData[customAttributes.Count + num];
			customAttributes.CopyTo(array, num);
			for (int i = 0; i < num; i++)
			{
				array[i] = new CustomAttributeData(customAttributes2[i]);
			}
			return Array.AsReadOnly<CustomAttributeData>(array);
		}

		// Token: 0x06004544 RID: 17732 RVA: 0x000FF544 File Offset: 0x000FD744
		[SecuritySafeCritical]
		internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeFieldInfo target)
		{
			IList<CustomAttributeData> customAttributes = CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
			int num = 0;
			Attribute[] customAttributes2 = PseudoCustomAttribute.GetCustomAttributes(target, typeof(object) as RuntimeType, out num);
			if (num == 0)
			{
				return customAttributes;
			}
			CustomAttributeData[] array = new CustomAttributeData[customAttributes.Count + num];
			customAttributes.CopyTo(array, num);
			for (int i = 0; i < num; i++)
			{
				array[i] = new CustomAttributeData(customAttributes2[i]);
			}
			return Array.AsReadOnly<CustomAttributeData>(array);
		}

		// Token: 0x06004545 RID: 17733 RVA: 0x000FF5BC File Offset: 0x000FD7BC
		[SecuritySafeCritical]
		internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeMethodInfo target)
		{
			IList<CustomAttributeData> customAttributes = CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
			int num = 0;
			Attribute[] customAttributes2 = PseudoCustomAttribute.GetCustomAttributes(target, typeof(object) as RuntimeType, true, out num);
			if (num == 0)
			{
				return customAttributes;
			}
			CustomAttributeData[] array = new CustomAttributeData[customAttributes.Count + num];
			customAttributes.CopyTo(array, num);
			for (int i = 0; i < num; i++)
			{
				array[i] = new CustomAttributeData(customAttributes2[i]);
			}
			return Array.AsReadOnly<CustomAttributeData>(array);
		}

		// Token: 0x06004546 RID: 17734 RVA: 0x000FF634 File Offset: 0x000FD834
		[SecuritySafeCritical]
		internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeConstructorInfo target)
		{
			return CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
		}

		// Token: 0x06004547 RID: 17735 RVA: 0x000FF647 File Offset: 0x000FD847
		[SecuritySafeCritical]
		internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeEventInfo target)
		{
			return CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
		}

		// Token: 0x06004548 RID: 17736 RVA: 0x000FF65A File Offset: 0x000FD85A
		[SecuritySafeCritical]
		internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimePropertyInfo target)
		{
			return CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
		}

		// Token: 0x06004549 RID: 17737 RVA: 0x000FF66D File Offset: 0x000FD86D
		[SecuritySafeCritical]
		internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeModule target)
		{
			if (target.IsResource())
			{
				return new List<CustomAttributeData>();
			}
			return CustomAttributeData.GetCustomAttributes(target, target.MetadataToken);
		}

		// Token: 0x0600454A RID: 17738 RVA: 0x000FF68C File Offset: 0x000FD88C
		[SecuritySafeCritical]
		internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeAssembly target)
		{
			IList<CustomAttributeData> customAttributes = CustomAttributeData.GetCustomAttributes((RuntimeModule)target.ManifestModule, RuntimeAssembly.GetToken(target.GetNativeHandle()));
			int num = 0;
			Attribute[] customAttributes2 = PseudoCustomAttribute.GetCustomAttributes(target, typeof(object) as RuntimeType, false, out num);
			if (num == 0)
			{
				return customAttributes;
			}
			CustomAttributeData[] array = new CustomAttributeData[customAttributes.Count + num];
			customAttributes.CopyTo(array, num);
			for (int i = 0; i < num; i++)
			{
				array[i] = new CustomAttributeData(customAttributes2[i]);
			}
			return Array.AsReadOnly<CustomAttributeData>(array);
		}

		// Token: 0x0600454B RID: 17739 RVA: 0x000FF710 File Offset: 0x000FD910
		[SecuritySafeCritical]
		internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeParameterInfo target)
		{
			IList<CustomAttributeData> customAttributes = CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
			int num = 0;
			Attribute[] customAttributes2 = PseudoCustomAttribute.GetCustomAttributes(target, typeof(object) as RuntimeType, out num);
			if (num == 0)
			{
				return customAttributes;
			}
			CustomAttributeData[] array = new CustomAttributeData[customAttributes.Count + num];
			customAttributes.CopyTo(array, num);
			for (int i = 0; i < num; i++)
			{
				array[i] = new CustomAttributeData(customAttributes2[i]);
			}
			return Array.AsReadOnly<CustomAttributeData>(array);
		}

		// Token: 0x0600454C RID: 17740 RVA: 0x000FF788 File Offset: 0x000FD988
		private static CustomAttributeEncoding TypeToCustomAttributeEncoding(RuntimeType type)
		{
			if (type == (RuntimeType)typeof(int))
			{
				return CustomAttributeEncoding.Int32;
			}
			if (type.IsEnum)
			{
				return CustomAttributeEncoding.Enum;
			}
			if (type == (RuntimeType)typeof(string))
			{
				return CustomAttributeEncoding.String;
			}
			if (type == (RuntimeType)typeof(Type))
			{
				return CustomAttributeEncoding.Type;
			}
			if (type == (RuntimeType)typeof(object))
			{
				return CustomAttributeEncoding.Object;
			}
			if (type.IsArray)
			{
				return CustomAttributeEncoding.Array;
			}
			if (type == (RuntimeType)typeof(char))
			{
				return CustomAttributeEncoding.Char;
			}
			if (type == (RuntimeType)typeof(bool))
			{
				return CustomAttributeEncoding.Boolean;
			}
			if (type == (RuntimeType)typeof(byte))
			{
				return CustomAttributeEncoding.Byte;
			}
			if (type == (RuntimeType)typeof(sbyte))
			{
				return CustomAttributeEncoding.SByte;
			}
			if (type == (RuntimeType)typeof(short))
			{
				return CustomAttributeEncoding.Int16;
			}
			if (type == (RuntimeType)typeof(ushort))
			{
				return CustomAttributeEncoding.UInt16;
			}
			if (type == (RuntimeType)typeof(uint))
			{
				return CustomAttributeEncoding.UInt32;
			}
			if (type == (RuntimeType)typeof(long))
			{
				return CustomAttributeEncoding.Int64;
			}
			if (type == (RuntimeType)typeof(ulong))
			{
				return CustomAttributeEncoding.UInt64;
			}
			if (type == (RuntimeType)typeof(float))
			{
				return CustomAttributeEncoding.Float;
			}
			if (type == (RuntimeType)typeof(double))
			{
				return CustomAttributeEncoding.Double;
			}
			if (type == (RuntimeType)typeof(Enum))
			{
				return CustomAttributeEncoding.Object;
			}
			if (type.IsClass)
			{
				return CustomAttributeEncoding.Object;
			}
			if (type.IsInterface)
			{
				return CustomAttributeEncoding.Object;
			}
			if (type.IsValueType)
			{
				return CustomAttributeEncoding.Undefined;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_InvalidKindOfTypeForCA"), "type");
		}

		// Token: 0x0600454D RID: 17741 RVA: 0x000FF978 File Offset: 0x000FDB78
		private static CustomAttributeType InitCustomAttributeType(RuntimeType parameterType)
		{
			CustomAttributeEncoding customAttributeEncoding = CustomAttributeData.TypeToCustomAttributeEncoding(parameterType);
			CustomAttributeEncoding customAttributeEncoding2 = CustomAttributeEncoding.Undefined;
			CustomAttributeEncoding customAttributeEncoding3 = CustomAttributeEncoding.Undefined;
			string text = null;
			if (customAttributeEncoding == CustomAttributeEncoding.Array)
			{
				parameterType = (RuntimeType)parameterType.GetElementType();
				customAttributeEncoding2 = CustomAttributeData.TypeToCustomAttributeEncoding(parameterType);
			}
			if (customAttributeEncoding == CustomAttributeEncoding.Enum || customAttributeEncoding2 == CustomAttributeEncoding.Enum)
			{
				customAttributeEncoding3 = CustomAttributeData.TypeToCustomAttributeEncoding((RuntimeType)Enum.GetUnderlyingType(parameterType));
				text = parameterType.AssemblyQualifiedName;
			}
			return new CustomAttributeType(customAttributeEncoding, customAttributeEncoding2, customAttributeEncoding3, text);
		}

		// Token: 0x0600454E RID: 17742 RVA: 0x000FF9D8 File Offset: 0x000FDBD8
		[SecurityCritical]
		private static IList<CustomAttributeData> GetCustomAttributes(RuntimeModule module, int tkTarget)
		{
			CustomAttributeRecord[] customAttributeRecords = CustomAttributeData.GetCustomAttributeRecords(module, tkTarget);
			CustomAttributeData[] array = new CustomAttributeData[customAttributeRecords.Length];
			for (int i = 0; i < customAttributeRecords.Length; i++)
			{
				array[i] = new CustomAttributeData(module, customAttributeRecords[i]);
			}
			return Array.AsReadOnly<CustomAttributeData>(array);
		}

		// Token: 0x0600454F RID: 17743 RVA: 0x000FFA1C File Offset: 0x000FDC1C
		[SecurityCritical]
		internal static CustomAttributeRecord[] GetCustomAttributeRecords(RuntimeModule module, int targetToken)
		{
			MetadataImport metadataImport = module.MetadataImport;
			MetadataEnumResult metadataEnumResult;
			metadataImport.EnumCustomAttributes(targetToken, out metadataEnumResult);
			CustomAttributeRecord[] array = new CustomAttributeRecord[metadataEnumResult.Length];
			for (int i = 0; i < array.Length; i++)
			{
				metadataImport.GetCustomAttributeProps(metadataEnumResult[i], out array[i].tkCtor.Value, out array[i].blob);
			}
			return array;
		}

		// Token: 0x06004550 RID: 17744 RVA: 0x000FFA84 File Offset: 0x000FDC84
		internal static CustomAttributeTypedArgument Filter(IList<CustomAttributeData> attrs, Type caType, int parameter)
		{
			for (int i = 0; i < attrs.Count; i++)
			{
				if (attrs[i].Constructor.DeclaringType == caType)
				{
					return attrs[i].ConstructorArguments[parameter];
				}
			}
			return default(CustomAttributeTypedArgument);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.CustomAttributeData" /> class.</summary>
		// Token: 0x06004551 RID: 17745 RVA: 0x000FFAD7 File Offset: 0x000FDCD7
		protected CustomAttributeData()
		{
		}

		// Token: 0x06004552 RID: 17746 RVA: 0x000FFAE0 File Offset: 0x000FDCE0
		[SecuritySafeCritical]
		private CustomAttributeData(RuntimeModule scope, CustomAttributeRecord caRecord)
		{
			this.m_scope = scope;
			this.m_ctor = (RuntimeConstructorInfo)RuntimeType.GetMethodBase(scope, caRecord.tkCtor);
			ParameterInfo[] parametersNoCopy = this.m_ctor.GetParametersNoCopy();
			this.m_ctorParams = new CustomAttributeCtorParameter[parametersNoCopy.Length];
			for (int i = 0; i < parametersNoCopy.Length; i++)
			{
				this.m_ctorParams[i] = new CustomAttributeCtorParameter(CustomAttributeData.InitCustomAttributeType((RuntimeType)parametersNoCopy[i].ParameterType));
			}
			FieldInfo[] fields = this.m_ctor.DeclaringType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			PropertyInfo[] properties = this.m_ctor.DeclaringType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			this.m_namedParams = new CustomAttributeNamedParameter[properties.Length + fields.Length];
			for (int j = 0; j < fields.Length; j++)
			{
				this.m_namedParams[j] = new CustomAttributeNamedParameter(fields[j].Name, CustomAttributeEncoding.Field, CustomAttributeData.InitCustomAttributeType((RuntimeType)fields[j].FieldType));
			}
			for (int k = 0; k < properties.Length; k++)
			{
				this.m_namedParams[k + fields.Length] = new CustomAttributeNamedParameter(properties[k].Name, CustomAttributeEncoding.Property, CustomAttributeData.InitCustomAttributeType((RuntimeType)properties[k].PropertyType));
			}
			this.m_members = new MemberInfo[fields.Length + properties.Length];
			fields.CopyTo(this.m_members, 0);
			properties.CopyTo(this.m_members, fields.Length);
			CustomAttributeEncodedArgument.ParseAttributeArguments(caRecord.blob, ref this.m_ctorParams, ref this.m_namedParams, this.m_scope);
		}

		// Token: 0x06004553 RID: 17747 RVA: 0x000FFC6C File Offset: 0x000FDE6C
		internal CustomAttributeData(Attribute attribute)
		{
			if (attribute is DllImportAttribute)
			{
				this.Init((DllImportAttribute)attribute);
				return;
			}
			if (attribute is FieldOffsetAttribute)
			{
				this.Init((FieldOffsetAttribute)attribute);
				return;
			}
			if (attribute is MarshalAsAttribute)
			{
				this.Init((MarshalAsAttribute)attribute);
				return;
			}
			if (attribute is TypeForwardedToAttribute)
			{
				this.Init((TypeForwardedToAttribute)attribute);
				return;
			}
			this.Init(attribute);
		}

		// Token: 0x06004554 RID: 17748 RVA: 0x000FFCDC File Offset: 0x000FDEDC
		private void Init(DllImportAttribute dllImport)
		{
			Type typeFromHandle = typeof(DllImportAttribute);
			this.m_ctor = typeFromHandle.GetConstructors(BindingFlags.Instance | BindingFlags.Public)[0];
			this.m_typedCtorArgs = Array.AsReadOnly<CustomAttributeTypedArgument>(new CustomAttributeTypedArgument[]
			{
				new CustomAttributeTypedArgument(dllImport.Value)
			});
			this.m_namedArgs = Array.AsReadOnly<CustomAttributeNamedArgument>(new CustomAttributeNamedArgument[]
			{
				new CustomAttributeNamedArgument(typeFromHandle.GetField("EntryPoint"), dllImport.EntryPoint),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("CharSet"), dllImport.CharSet),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("ExactSpelling"), dllImport.ExactSpelling),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("SetLastError"), dllImport.SetLastError),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("PreserveSig"), dllImport.PreserveSig),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("CallingConvention"), dllImport.CallingConvention),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("BestFitMapping"), dllImport.BestFitMapping),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("ThrowOnUnmappableChar"), dllImport.ThrowOnUnmappableChar)
			});
		}

		// Token: 0x06004555 RID: 17749 RVA: 0x000FFE44 File Offset: 0x000FE044
		private void Init(FieldOffsetAttribute fieldOffset)
		{
			this.m_ctor = typeof(FieldOffsetAttribute).GetConstructors(BindingFlags.Instance | BindingFlags.Public)[0];
			this.m_typedCtorArgs = Array.AsReadOnly<CustomAttributeTypedArgument>(new CustomAttributeTypedArgument[]
			{
				new CustomAttributeTypedArgument(fieldOffset.Value)
			});
			this.m_namedArgs = Array.AsReadOnly<CustomAttributeNamedArgument>(new CustomAttributeNamedArgument[0]);
		}

		// Token: 0x06004556 RID: 17750 RVA: 0x000FFEA4 File Offset: 0x000FE0A4
		private void Init(MarshalAsAttribute marshalAs)
		{
			Type typeFromHandle = typeof(MarshalAsAttribute);
			this.m_ctor = typeFromHandle.GetConstructors(BindingFlags.Instance | BindingFlags.Public)[0];
			this.m_typedCtorArgs = Array.AsReadOnly<CustomAttributeTypedArgument>(new CustomAttributeTypedArgument[]
			{
				new CustomAttributeTypedArgument(marshalAs.Value)
			});
			int num = 3;
			if (marshalAs.MarshalType != null)
			{
				num++;
			}
			if (marshalAs.MarshalTypeRef != null)
			{
				num++;
			}
			if (marshalAs.MarshalCookie != null)
			{
				num++;
			}
			num++;
			num++;
			if (marshalAs.SafeArrayUserDefinedSubType != null)
			{
				num++;
			}
			CustomAttributeNamedArgument[] array = new CustomAttributeNamedArgument[num];
			num = 0;
			array[num++] = new CustomAttributeNamedArgument(typeFromHandle.GetField("ArraySubType"), marshalAs.ArraySubType);
			array[num++] = new CustomAttributeNamedArgument(typeFromHandle.GetField("SizeParamIndex"), marshalAs.SizeParamIndex);
			array[num++] = new CustomAttributeNamedArgument(typeFromHandle.GetField("SizeConst"), marshalAs.SizeConst);
			array[num++] = new CustomAttributeNamedArgument(typeFromHandle.GetField("IidParameterIndex"), marshalAs.IidParameterIndex);
			array[num++] = new CustomAttributeNamedArgument(typeFromHandle.GetField("SafeArraySubType"), marshalAs.SafeArraySubType);
			if (marshalAs.MarshalType != null)
			{
				array[num++] = new CustomAttributeNamedArgument(typeFromHandle.GetField("MarshalType"), marshalAs.MarshalType);
			}
			if (marshalAs.MarshalTypeRef != null)
			{
				array[num++] = new CustomAttributeNamedArgument(typeFromHandle.GetField("MarshalTypeRef"), marshalAs.MarshalTypeRef);
			}
			if (marshalAs.MarshalCookie != null)
			{
				array[num++] = new CustomAttributeNamedArgument(typeFromHandle.GetField("MarshalCookie"), marshalAs.MarshalCookie);
			}
			if (marshalAs.SafeArrayUserDefinedSubType != null)
			{
				array[num++] = new CustomAttributeNamedArgument(typeFromHandle.GetField("SafeArrayUserDefinedSubType"), marshalAs.SafeArrayUserDefinedSubType);
			}
			this.m_namedArgs = Array.AsReadOnly<CustomAttributeNamedArgument>(array);
		}

		// Token: 0x06004557 RID: 17751 RVA: 0x001000C0 File Offset: 0x000FE2C0
		private void Init(TypeForwardedToAttribute forwardedTo)
		{
			Type typeFromHandle = typeof(TypeForwardedToAttribute);
			Type[] array = new Type[] { typeof(Type) };
			this.m_ctor = typeFromHandle.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, array, null);
			this.m_typedCtorArgs = Array.AsReadOnly<CustomAttributeTypedArgument>(new CustomAttributeTypedArgument[]
			{
				new CustomAttributeTypedArgument(typeof(Type), forwardedTo.Destination)
			});
			CustomAttributeNamedArgument[] array2 = new CustomAttributeNamedArgument[0];
			this.m_namedArgs = Array.AsReadOnly<CustomAttributeNamedArgument>(array2);
		}

		// Token: 0x06004558 RID: 17752 RVA: 0x0010013F File Offset: 0x000FE33F
		private void Init(object pca)
		{
			this.m_ctor = pca.GetType().GetConstructors(BindingFlags.Instance | BindingFlags.Public)[0];
			this.m_typedCtorArgs = Array.AsReadOnly<CustomAttributeTypedArgument>(new CustomAttributeTypedArgument[0]);
			this.m_namedArgs = Array.AsReadOnly<CustomAttributeNamedArgument>(new CustomAttributeNamedArgument[0]);
		}

		/// <summary>Returns a string representation of the custom attribute.</summary>
		/// <returns>A string value that represents the custom attribute.</returns>
		// Token: 0x06004559 RID: 17753 RVA: 0x00100178 File Offset: 0x000FE378
		public override string ToString()
		{
			string text = "";
			for (int i = 0; i < this.ConstructorArguments.Count; i++)
			{
				text += string.Format(CultureInfo.CurrentCulture, (i == 0) ? "{0}" : ", {0}", this.ConstructorArguments[i]);
			}
			string text2 = "";
			for (int j = 0; j < this.NamedArguments.Count; j++)
			{
				text2 += string.Format(CultureInfo.CurrentCulture, (j == 0 && text.Length == 0) ? "{0}" : ", {0}", this.NamedArguments[j]);
			}
			return string.Format(CultureInfo.CurrentCulture, "[{0}({1}{2})]", this.Constructor.DeclaringType.FullName, text, text2);
		}

		/// <summary>Serves as a hash function for a particular type.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
		// Token: 0x0600455A RID: 17754 RVA: 0x00100248 File Offset: 0x000FE448
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <param name="obj">An object to compare with this instance, or <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is equal to the current instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600455B RID: 17755 RVA: 0x00100250 File Offset: 0x000FE450
		public override bool Equals(object obj)
		{
			return obj == this;
		}

		/// <summary>Gets the type of the attribute.</summary>
		/// <returns>The type of the attribute.</returns>
		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x0600455C RID: 17756 RVA: 0x00100256 File Offset: 0x000FE456
		[__DynamicallyInvokable]
		public Type AttributeType
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Constructor.DeclaringType;
			}
		}

		/// <summary>Gets a <see cref="T:System.Reflection.ConstructorInfo" /> object that represents the constructor that would have initialized the custom attribute.</summary>
		/// <returns>An object that represents the constructor that would have initialized the custom attribute represented by the current instance of the <see cref="T:System.Reflection.CustomAttributeData" /> class.</returns>
		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x0600455D RID: 17757 RVA: 0x00100263 File Offset: 0x000FE463
		[ComVisible(true)]
		public virtual ConstructorInfo Constructor
		{
			get
			{
				return this.m_ctor;
			}
		}

		/// <summary>Gets the list of positional arguments specified for the attribute instance represented by the <see cref="T:System.Reflection.CustomAttributeData" /> object.</summary>
		/// <returns>A collection of structures that represent the positional arguments specified for the custom attribute instance.</returns>
		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x0600455E RID: 17758 RVA: 0x0010026C File Offset: 0x000FE46C
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public virtual IList<CustomAttributeTypedArgument> ConstructorArguments
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_typedCtorArgs == null)
				{
					CustomAttributeTypedArgument[] array = new CustomAttributeTypedArgument[this.m_ctorParams.Length];
					for (int i = 0; i < array.Length; i++)
					{
						CustomAttributeEncodedArgument customAttributeEncodedArgument = this.m_ctorParams[i].CustomAttributeEncodedArgument;
						array[i] = new CustomAttributeTypedArgument(this.m_scope, this.m_ctorParams[i].CustomAttributeEncodedArgument);
					}
					this.m_typedCtorArgs = Array.AsReadOnly<CustomAttributeTypedArgument>(array);
				}
				return this.m_typedCtorArgs;
			}
		}

		/// <summary>Gets the list of named arguments specified for the attribute instance represented by the <see cref="T:System.Reflection.CustomAttributeData" /> object.</summary>
		/// <returns>A collection of structures that represent the named arguments specified for the custom attribute instance.</returns>
		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x0600455F RID: 17759 RVA: 0x001002E4 File Offset: 0x000FE4E4
		[__DynamicallyInvokable]
		public virtual IList<CustomAttributeNamedArgument> NamedArguments
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_namedArgs == null)
				{
					if (this.m_namedParams == null)
					{
						return null;
					}
					int num = 0;
					for (int i = 0; i < this.m_namedParams.Length; i++)
					{
						if (this.m_namedParams[i].EncodedArgument.CustomAttributeType.EncodedType != CustomAttributeEncoding.Undefined)
						{
							num++;
						}
					}
					CustomAttributeNamedArgument[] array = new CustomAttributeNamedArgument[num];
					int j = 0;
					int num2 = 0;
					while (j < this.m_namedParams.Length)
					{
						if (this.m_namedParams[j].EncodedArgument.CustomAttributeType.EncodedType != CustomAttributeEncoding.Undefined)
						{
							array[num2++] = new CustomAttributeNamedArgument(this.m_members[j], new CustomAttributeTypedArgument(this.m_scope, this.m_namedParams[j].EncodedArgument));
						}
						j++;
					}
					this.m_namedArgs = Array.AsReadOnly<CustomAttributeNamedArgument>(array);
				}
				return this.m_namedArgs;
			}
		}

		// Token: 0x04001C61 RID: 7265
		private ConstructorInfo m_ctor;

		// Token: 0x04001C62 RID: 7266
		private RuntimeModule m_scope;

		// Token: 0x04001C63 RID: 7267
		private MemberInfo[] m_members;

		// Token: 0x04001C64 RID: 7268
		private CustomAttributeCtorParameter[] m_ctorParams;

		// Token: 0x04001C65 RID: 7269
		private CustomAttributeNamedParameter[] m_namedParams;

		// Token: 0x04001C66 RID: 7270
		private IList<CustomAttributeTypedArgument> m_typedCtorArgs;

		// Token: 0x04001C67 RID: 7271
		private IList<CustomAttributeNamedArgument> m_namedArgs;
	}
}
