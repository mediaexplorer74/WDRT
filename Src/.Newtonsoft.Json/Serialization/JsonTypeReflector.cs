using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000099 RID: 153
	[NullableContext(1)]
	[Nullable(0)]
	internal static class JsonTypeReflector
	{
		// Token: 0x06000800 RID: 2048 RVA: 0x00023351 File Offset: 0x00021551
		[return: Nullable(2)]
		public static T GetCachedAttribute<[Nullable(0)] T>(object attributeProvider) where T : Attribute
		{
			return CachedAttributeGetter<T>.GetAttribute(attributeProvider);
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0002335C File Offset: 0x0002155C
		public static bool CanTypeDescriptorConvertString(Type type, out TypeConverter typeConverter)
		{
			typeConverter = TypeDescriptor.GetConverter(type);
			if (typeConverter != null)
			{
				Type type2 = typeConverter.GetType();
				if (!string.Equals(type2.FullName, "System.ComponentModel.ComponentConverter", StringComparison.Ordinal) && !string.Equals(type2.FullName, "System.ComponentModel.ReferenceConverter", StringComparison.Ordinal) && !string.Equals(type2.FullName, "System.Windows.Forms.Design.DataSourceConverter", StringComparison.Ordinal) && type2 != typeof(TypeConverter))
				{
					return typeConverter.CanConvertTo(typeof(string));
				}
			}
			return false;
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x000233DC File Offset: 0x000215DC
		[return: Nullable(2)]
		public static DataContractAttribute GetDataContractAttribute(Type type)
		{
			Type type2 = type;
			while (type2 != null)
			{
				DataContractAttribute attribute = CachedAttributeGetter<DataContractAttribute>.GetAttribute(type2);
				if (attribute != null)
				{
					return attribute;
				}
				type2 = type2.BaseType();
			}
			return null;
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0002340C File Offset: 0x0002160C
		[return: Nullable(2)]
		public static DataMemberAttribute GetDataMemberAttribute(MemberInfo memberInfo)
		{
			if (memberInfo.MemberType() == MemberTypes.Field)
			{
				return CachedAttributeGetter<DataMemberAttribute>.GetAttribute(memberInfo);
			}
			PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
			DataMemberAttribute dataMemberAttribute = CachedAttributeGetter<DataMemberAttribute>.GetAttribute(propertyInfo);
			if (dataMemberAttribute == null && propertyInfo.IsVirtual())
			{
				Type type = propertyInfo.DeclaringType;
				while (dataMemberAttribute == null && type != null)
				{
					PropertyInfo propertyInfo2 = (PropertyInfo)ReflectionUtils.GetMemberInfoFromType(type, propertyInfo);
					if (propertyInfo2 != null && propertyInfo2.IsVirtual())
					{
						dataMemberAttribute = CachedAttributeGetter<DataMemberAttribute>.GetAttribute(propertyInfo2);
					}
					type = type.BaseType();
				}
			}
			return dataMemberAttribute;
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00023484 File Offset: 0x00021684
		public static MemberSerialization GetObjectMemberSerialization(Type objectType, bool ignoreSerializableAttribute)
		{
			JsonObjectAttribute cachedAttribute = JsonTypeReflector.GetCachedAttribute<JsonObjectAttribute>(objectType);
			if (cachedAttribute != null)
			{
				return cachedAttribute.MemberSerialization;
			}
			if (JsonTypeReflector.GetDataContractAttribute(objectType) != null)
			{
				return MemberSerialization.OptIn;
			}
			if (!ignoreSerializableAttribute && JsonTypeReflector.IsSerializable(objectType))
			{
				return MemberSerialization.Fields;
			}
			return MemberSerialization.OptOut;
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x000234BC File Offset: 0x000216BC
		[return: Nullable(2)]
		public static JsonConverter GetJsonConverter(object attributeProvider)
		{
			JsonConverterAttribute cachedAttribute = JsonTypeReflector.GetCachedAttribute<JsonConverterAttribute>(attributeProvider);
			if (cachedAttribute != null)
			{
				Func<object[], object> func = JsonTypeReflector.CreatorCache.Get(cachedAttribute.ConverterType);
				if (func != null)
				{
					return (JsonConverter)func(cachedAttribute.ConverterParameters);
				}
			}
			return null;
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x000234FA File Offset: 0x000216FA
		public static JsonConverter CreateJsonConverterInstance(Type converterType, [Nullable(new byte[] { 2, 1 })] object[] args)
		{
			return (JsonConverter)JsonTypeReflector.CreatorCache.Get(converterType)(args);
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00023512 File Offset: 0x00021712
		public static NamingStrategy CreateNamingStrategyInstance(Type namingStrategyType, [Nullable(new byte[] { 2, 1 })] object[] args)
		{
			return (NamingStrategy)JsonTypeReflector.CreatorCache.Get(namingStrategyType)(args);
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0002352A File Offset: 0x0002172A
		[return: Nullable(2)]
		public static NamingStrategy GetContainerNamingStrategy(JsonContainerAttribute containerAttribute)
		{
			if (containerAttribute.NamingStrategyInstance == null)
			{
				if (containerAttribute.NamingStrategyType == null)
				{
					return null;
				}
				containerAttribute.NamingStrategyInstance = JsonTypeReflector.CreateNamingStrategyInstance(containerAttribute.NamingStrategyType, containerAttribute.NamingStrategyParameters);
			}
			return containerAttribute.NamingStrategyInstance;
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00023564 File Offset: 0x00021764
		[return: Nullable(new byte[] { 1, 2, 1, 1 })]
		private static Func<object[], object> GetCreator(Type type)
		{
			Func<object> defaultConstructor = (ReflectionUtils.HasDefaultConstructor(type, false) ? JsonTypeReflector.ReflectionDelegateFactory.CreateDefaultConstructor<object>(type) : null);
			return delegate([Nullable(new byte[] { 2, 1 })] object[] parameters)
			{
				object obj;
				try
				{
					if (parameters != null)
					{
						Type[] array = parameters.Select(delegate(object param)
						{
							if (param == null)
							{
								throw new InvalidOperationException("Cannot pass a null parameter to the constructor.");
							}
							return param.GetType();
						}).ToArray<Type>();
						ConstructorInfo constructor = type.GetConstructor(array);
						if (!(constructor != null))
						{
							throw new JsonException("No matching parameterized constructor found for '{0}'.".FormatWith(CultureInfo.InvariantCulture, type));
						}
						obj = JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(constructor)(parameters);
					}
					else
					{
						if (defaultConstructor == null)
						{
							throw new JsonException("No parameterless constructor defined for '{0}'.".FormatWith(CultureInfo.InvariantCulture, type));
						}
						obj = defaultConstructor();
					}
				}
				catch (Exception ex)
				{
					throw new JsonException("Error creating '{0}'.".FormatWith(CultureInfo.InvariantCulture, type), ex);
				}
				return obj;
			};
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x000235B1 File Offset: 0x000217B1
		[return: Nullable(2)]
		private static Type GetAssociatedMetadataType(Type type)
		{
			return JsonTypeReflector.AssociatedMetadataTypesCache.Get(type);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x000235C0 File Offset: 0x000217C0
		[return: Nullable(2)]
		private static Type GetAssociateMetadataTypeFromAttribute(Type type)
		{
			foreach (Attribute attribute in ReflectionUtils.GetAttributes(type, null, true))
			{
				Type type2 = attribute.GetType();
				if (string.Equals(type2.FullName, "System.ComponentModel.DataAnnotations.MetadataTypeAttribute", StringComparison.Ordinal))
				{
					if (JsonTypeReflector._metadataTypeAttributeReflectionObject == null)
					{
						JsonTypeReflector._metadataTypeAttributeReflectionObject = ReflectionObject.Create(type2, new string[] { "MetadataClassType" });
					}
					return (Type)JsonTypeReflector._metadataTypeAttributeReflectionObject.GetValue(attribute, "MetadataClassType");
				}
			}
			return null;
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0002363C File Offset: 0x0002183C
		[return: Nullable(2)]
		private static T GetAttribute<[Nullable(0)] T>(Type type) where T : Attribute
		{
			Type associatedMetadataType = JsonTypeReflector.GetAssociatedMetadataType(type);
			T t;
			if (associatedMetadataType != null)
			{
				t = ReflectionUtils.GetAttribute<T>(associatedMetadataType, true);
				if (t != null)
				{
					return t;
				}
			}
			t = ReflectionUtils.GetAttribute<T>(type, true);
			if (t != null)
			{
				return t;
			}
			Type[] interfaces = type.GetInterfaces();
			for (int i = 0; i < interfaces.Length; i++)
			{
				t = ReflectionUtils.GetAttribute<T>(interfaces[i], true);
				if (t != null)
				{
					return t;
				}
			}
			return default(T);
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x000236B0 File Offset: 0x000218B0
		[return: Nullable(2)]
		private static T GetAttribute<[Nullable(0)] T>(MemberInfo memberInfo) where T : Attribute
		{
			Type associatedMetadataType = JsonTypeReflector.GetAssociatedMetadataType(memberInfo.DeclaringType);
			T t;
			if (associatedMetadataType != null)
			{
				MemberInfo memberInfoFromType = ReflectionUtils.GetMemberInfoFromType(associatedMetadataType, memberInfo);
				if (memberInfoFromType != null)
				{
					t = ReflectionUtils.GetAttribute<T>(memberInfoFromType, true);
					if (t != null)
					{
						return t;
					}
				}
			}
			t = ReflectionUtils.GetAttribute<T>(memberInfo, true);
			if (t != null)
			{
				return t;
			}
			if (memberInfo.DeclaringType != null)
			{
				Type[] interfaces = memberInfo.DeclaringType.GetInterfaces();
				for (int i = 0; i < interfaces.Length; i++)
				{
					MemberInfo memberInfoFromType2 = ReflectionUtils.GetMemberInfoFromType(interfaces[i], memberInfo);
					if (memberInfoFromType2 != null)
					{
						t = ReflectionUtils.GetAttribute<T>(memberInfoFromType2, true);
						if (t != null)
						{
							return t;
						}
					}
				}
			}
			return default(T);
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00023766 File Offset: 0x00021966
		public static bool IsNonSerializable(object provider)
		{
			return ReflectionUtils.GetAttribute<NonSerializedAttribute>(provider, false) != null;
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x00023772 File Offset: 0x00021972
		public static bool IsSerializable(object provider)
		{
			return ReflectionUtils.GetAttribute<SerializableAttribute>(provider, false) != null;
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00023780 File Offset: 0x00021980
		[return: Nullable(2)]
		public static T GetAttribute<[Nullable(0)] T>(object provider) where T : Attribute
		{
			Type type = provider as Type;
			if (type != null)
			{
				return JsonTypeReflector.GetAttribute<T>(type);
			}
			MemberInfo memberInfo = provider as MemberInfo;
			if (memberInfo != null)
			{
				return JsonTypeReflector.GetAttribute<T>(memberInfo);
			}
			return ReflectionUtils.GetAttribute<T>(provider, true);
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x000237B8 File Offset: 0x000219B8
		public static bool DynamicCodeGeneration
		{
			[SecuritySafeCritical]
			get
			{
				if (JsonTypeReflector._dynamicCodeGeneration == null)
				{
					try
					{
						new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Demand();
						new ReflectionPermission(ReflectionPermissionFlag.RestrictedMemberAccess).Demand();
						new SecurityPermission(SecurityPermissionFlag.SkipVerification).Demand();
						new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
						new SecurityPermission(PermissionState.Unrestricted).Demand();
						JsonTypeReflector._dynamicCodeGeneration = new bool?(true);
					}
					catch (Exception)
					{
						JsonTypeReflector._dynamicCodeGeneration = new bool?(false);
					}
				}
				return JsonTypeReflector._dynamicCodeGeneration.GetValueOrDefault();
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x00023840 File Offset: 0x00021A40
		public static bool FullyTrusted
		{
			get
			{
				if (JsonTypeReflector._fullyTrusted == null)
				{
					AppDomain currentDomain = AppDomain.CurrentDomain;
					JsonTypeReflector._fullyTrusted = new bool?(currentDomain.IsHomogenous && currentDomain.IsFullyTrusted);
				}
				return JsonTypeReflector._fullyTrusted.GetValueOrDefault();
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x00023884 File Offset: 0x00021A84
		public static ReflectionDelegateFactory ReflectionDelegateFactory
		{
			get
			{
				if (JsonTypeReflector.DynamicCodeGeneration)
				{
					return DynamicReflectionDelegateFactory.Instance;
				}
				return LateBoundReflectionDelegateFactory.Instance;
			}
		}

		// Token: 0x040002B5 RID: 693
		private static bool? _dynamicCodeGeneration;

		// Token: 0x040002B6 RID: 694
		private static bool? _fullyTrusted;

		// Token: 0x040002B7 RID: 695
		public const string IdPropertyName = "$id";

		// Token: 0x040002B8 RID: 696
		public const string RefPropertyName = "$ref";

		// Token: 0x040002B9 RID: 697
		public const string TypePropertyName = "$type";

		// Token: 0x040002BA RID: 698
		public const string ValuePropertyName = "$value";

		// Token: 0x040002BB RID: 699
		public const string ArrayValuesPropertyName = "$values";

		// Token: 0x040002BC RID: 700
		public const string ShouldSerializePrefix = "ShouldSerialize";

		// Token: 0x040002BD RID: 701
		public const string SpecifiedPostfix = "Specified";

		// Token: 0x040002BE RID: 702
		public const string ConcurrentDictionaryTypeName = "System.Collections.Concurrent.ConcurrentDictionary`2";

		// Token: 0x040002BF RID: 703
		[Nullable(new byte[] { 1, 1, 1, 2, 1, 1 })]
		private static readonly ThreadSafeStore<Type, Func<object[], object>> CreatorCache = new ThreadSafeStore<Type, Func<object[], object>>(new Func<Type, Func<object[], object>>(JsonTypeReflector.GetCreator));

		// Token: 0x040002C0 RID: 704
		[Nullable(new byte[] { 1, 1, 2 })]
		private static readonly ThreadSafeStore<Type, Type> AssociatedMetadataTypesCache = new ThreadSafeStore<Type, Type>(new Func<Type, Type>(JsonTypeReflector.GetAssociateMetadataTypeFromAttribute));

		// Token: 0x040002C1 RID: 705
		[Nullable(2)]
		private static ReflectionObject _metadataTypeAttributeReflectionObject;
	}
}
