using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization.Formatters;
using System.Security;
using System.Text;

namespace System.Runtime.Serialization
{
	/// <summary>Provides static methods to aid with the implementation of a <see cref="T:System.Runtime.Serialization.Formatter" /> for serialization. This class cannot be inherited.</summary>
	// Token: 0x0200072D RID: 1837
	[ComVisible(true)]
	public static class FormatterServices
	{
		// Token: 0x060051A2 RID: 20898 RVA: 0x00120B64 File Offset: 0x0011ED64
		private static MemberInfo[] GetSerializableMembers(RuntimeType type)
		{
			FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			int num = 0;
			for (int i = 0; i < fields.Length; i++)
			{
				if ((fields[i].Attributes & FieldAttributes.NotSerialized) != FieldAttributes.NotSerialized)
				{
					num++;
				}
			}
			if (num != fields.Length)
			{
				FieldInfo[] array = new FieldInfo[num];
				num = 0;
				for (int j = 0; j < fields.Length; j++)
				{
					if ((fields[j].Attributes & FieldAttributes.NotSerialized) != FieldAttributes.NotSerialized)
					{
						array[num] = fields[j];
						num++;
					}
				}
				return array;
			}
			return fields;
		}

		// Token: 0x060051A3 RID: 20899 RVA: 0x00120BF0 File Offset: 0x0011EDF0
		private static bool CheckSerializable(RuntimeType type)
		{
			return type.IsSerializable;
		}

		// Token: 0x060051A4 RID: 20900 RVA: 0x00120C00 File Offset: 0x0011EE00
		private static MemberInfo[] InternalGetSerializableMembers(RuntimeType type)
		{
			if (type.IsInterface)
			{
				return new MemberInfo[0];
			}
			if (!FormatterServices.CheckSerializable(type))
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_NonSerType", new object[]
				{
					type.FullName,
					type.Module.Assembly.FullName
				}));
			}
			MemberInfo[] array = FormatterServices.GetSerializableMembers(type);
			RuntimeType runtimeType = (RuntimeType)type.BaseType;
			if (runtimeType != null && runtimeType != (RuntimeType)typeof(object))
			{
				RuntimeType[] array2 = null;
				int num = 0;
				bool parentTypes = FormatterServices.GetParentTypes(runtimeType, out array2, out num);
				if (num > 0)
				{
					List<SerializationFieldInfo> list = new List<SerializationFieldInfo>();
					for (int i = 0; i < num; i++)
					{
						runtimeType = array2[i];
						if (!FormatterServices.CheckSerializable(runtimeType))
						{
							throw new SerializationException(Environment.GetResourceString("Serialization_NonSerType", new object[]
							{
								runtimeType.FullName,
								runtimeType.Module.Assembly.FullName
							}));
						}
						FieldInfo[] fields = runtimeType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
						string text = (parentTypes ? runtimeType.Name : runtimeType.FullName);
						foreach (FieldInfo fieldInfo in fields)
						{
							if (!fieldInfo.IsNotSerialized)
							{
								list.Add(new SerializationFieldInfo((RuntimeFieldInfo)fieldInfo, text));
							}
						}
					}
					if (list != null && list.Count > 0)
					{
						MemberInfo[] array4 = new MemberInfo[list.Count + array.Length];
						Array.Copy(array, array4, array.Length);
						((ICollection)list).CopyTo(array4, array.Length);
						array = array4;
					}
				}
			}
			return array;
		}

		// Token: 0x060051A5 RID: 20901 RVA: 0x00120D98 File Offset: 0x0011EF98
		private static bool GetParentTypes(RuntimeType parentType, out RuntimeType[] parentTypes, out int parentTypeCount)
		{
			parentTypes = null;
			parentTypeCount = 0;
			bool flag = true;
			RuntimeType runtimeType = (RuntimeType)typeof(object);
			RuntimeType runtimeType2 = parentType;
			while (runtimeType2 != runtimeType)
			{
				if (!runtimeType2.IsInterface)
				{
					string name = runtimeType2.Name;
					int num = 0;
					while (flag && num < parentTypeCount)
					{
						string name2 = parentTypes[num].Name;
						if (name2.Length == name.Length && name2[0] == name[0] && name == name2)
						{
							flag = false;
							break;
						}
						num++;
					}
					if (parentTypes == null || parentTypeCount == parentTypes.Length)
					{
						RuntimeType[] array = new RuntimeType[Math.Max(parentTypeCount * 2, 12)];
						if (parentTypes != null)
						{
							Array.Copy(parentTypes, 0, array, 0, parentTypeCount);
						}
						parentTypes = array;
					}
					RuntimeType[] array2 = parentTypes;
					int num2 = parentTypeCount;
					parentTypeCount = num2 + 1;
					array2[num2] = runtimeType2;
				}
				runtimeType2 = (RuntimeType)runtimeType2.BaseType;
			}
			return flag;
		}

		/// <summary>Gets all the serializable members for a class of the specified <see cref="T:System.Type" />.</summary>
		/// <param name="type">The type being serialized.</param>
		/// <returns>An array of type <see cref="T:System.Reflection.MemberInfo" /> of the non-transient, non-static members.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x060051A6 RID: 20902 RVA: 0x00120E80 File Offset: 0x0011F080
		[SecurityCritical]
		public static MemberInfo[] GetSerializableMembers(Type type)
		{
			return FormatterServices.GetSerializableMembers(type, new StreamingContext(StreamingContextStates.All));
		}

		/// <summary>Gets all the serializable members for a class of the specified <see cref="T:System.Type" /> and in the provided <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="type">The type being serialized or cloned.</param>
		/// <param name="context">The context where the serialization occurs.</param>
		/// <returns>An array of type <see cref="T:System.Reflection.MemberInfo" /> of the non-transient, non-static members.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x060051A7 RID: 20903 RVA: 0x00120E94 File Offset: 0x0011F094
		[SecurityCritical]
		public static MemberInfo[] GetSerializableMembers(Type type, StreamingContext context)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!(type is RuntimeType))
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_InvalidType", new object[] { type.ToString() }));
			}
			MemberHolder memberHolder = new MemberHolder(type, context);
			return FormatterServices.m_MemberInfoTable.GetOrAdd(memberHolder, (MemberHolder _) => FormatterServices.InternalGetSerializableMembers((RuntimeType)type));
		}

		/// <summary>Determines whether the specified <see cref="T:System.Type" /> can be deserialized with the <see cref="T:System.Runtime.Serialization.Formatters.TypeFilterLevel" /> property set to <see langword="Low" />.</summary>
		/// <param name="t">The <see cref="T:System.Type" /> to check for the ability to deserialize.</param>
		/// <param name="securityLevel">The <see cref="T:System.Runtime.Serialization.Formatters.TypeFilterLevel" /> property value.</param>
		/// <exception cref="T:System.Security.SecurityException">The <paramref name="t" /> parameter is an advanced type and cannot be deserialized when the <see cref="T:System.Runtime.Serialization.Formatters.TypeFilterLevel" /> property is set to <see langword="Low" />.</exception>
		// Token: 0x060051A8 RID: 20904 RVA: 0x00120F18 File Offset: 0x0011F118
		public static void CheckTypeSecurity(Type t, TypeFilterLevel securityLevel)
		{
			if (securityLevel == TypeFilterLevel.Low)
			{
				for (int i = 0; i < FormatterServices.advancedTypes.Length; i++)
				{
					if (FormatterServices.advancedTypes[i].IsAssignableFrom(t))
					{
						throw new SecurityException(Environment.GetResourceString("Serialization_TypeSecurity", new object[]
						{
							FormatterServices.advancedTypes[i].FullName,
							t.FullName
						}));
					}
				}
			}
		}

		/// <summary>Creates a new instance of the specified object type.</summary>
		/// <param name="type">The type of object to create.</param>
		/// <returns>A zeroed object of the specified type.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x060051A9 RID: 20905 RVA: 0x00120F78 File Offset: 0x0011F178
		[SecurityCritical]
		public static object GetUninitializedObject(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!(type is RuntimeType))
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_InvalidType", new object[] { type.ToString() }));
			}
			return FormatterServices.nativeGetUninitializedObject((RuntimeType)type);
		}

		/// <summary>Creates a new instance of the specified object type.</summary>
		/// <param name="type">The type of object to create.</param>
		/// <returns>A zeroed object of the specified type.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <paramref name="type" /> parameter is not a valid common language runtime type.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x060051AA RID: 20906 RVA: 0x00120FC8 File Offset: 0x0011F1C8
		[SecurityCritical]
		public static object GetSafeUninitializedObject(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!(type is RuntimeType))
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_InvalidType", new object[] { type.ToString() }));
			}
			if (type == typeof(ConstructionCall) || type == typeof(LogicalCallContext) || type == typeof(SynchronizationAttribute))
			{
				return FormatterServices.nativeGetUninitializedObject((RuntimeType)type);
			}
			object obj;
			try
			{
				obj = FormatterServices.nativeGetSafeUninitializedObject((RuntimeType)type);
			}
			catch (SecurityException ex)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_Security", new object[] { type.FullName }), ex);
			}
			return obj;
		}

		// Token: 0x060051AB RID: 20907
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object nativeGetSafeUninitializedObject(RuntimeType type);

		// Token: 0x060051AC RID: 20908
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object nativeGetUninitializedObject(RuntimeType type);

		// Token: 0x060051AD RID: 20909
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetEnableUnsafeTypeForwarders();

		// Token: 0x060051AE RID: 20910 RVA: 0x00121080 File Offset: 0x0011F280
		[SecuritySafeCritical]
		internal static bool UnsafeTypeForwardersIsEnabled()
		{
			if (!FormatterServices.unsafeTypeForwardersIsEnabledInitialized)
			{
				FormatterServices.unsafeTypeForwardersIsEnabled = FormatterServices.GetEnableUnsafeTypeForwarders();
				FormatterServices.unsafeTypeForwardersIsEnabledInitialized = true;
			}
			return FormatterServices.unsafeTypeForwardersIsEnabled;
		}

		// Token: 0x060051AF RID: 20911 RVA: 0x001210A4 File Offset: 0x0011F2A4
		[SecurityCritical]
		internal static void SerializationSetValue(MemberInfo fi, object target, object value)
		{
			RtFieldInfo rtFieldInfo = fi as RtFieldInfo;
			if (rtFieldInfo != null)
			{
				rtFieldInfo.CheckConsistency(target);
				rtFieldInfo.UnsafeSetValue(target, value, BindingFlags.Default, FormatterServices.s_binder, null);
				return;
			}
			SerializationFieldInfo serializationFieldInfo = fi as SerializationFieldInfo;
			if (serializationFieldInfo != null)
			{
				serializationFieldInfo.InternalSetValue(target, value, BindingFlags.Default, FormatterServices.s_binder, null);
				return;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFieldInfo"));
		}

		/// <summary>Populates the specified object with values for each field drawn from the data array of objects.</summary>
		/// <param name="obj">The object to populate.</param>
		/// <param name="members">An array of <see cref="T:System.Reflection.MemberInfo" /> that describes which fields and properties to populate.</param>
		/// <param name="data">An array of <see cref="T:System.Object" /> that specifies the values for each field and property to populate.</param>
		/// <returns>The newly populated object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" />, <paramref name="members" />, or <paramref name="data" /> parameter is <see langword="null" />.  
		///  An element of <paramref name="members" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="members" /> does not match the length of <paramref name="data" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element of <paramref name="members" /> is not an instance of <see cref="T:System.Reflection.FieldInfo" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x060051B0 RID: 20912 RVA: 0x00121108 File Offset: 0x0011F308
		[SecurityCritical]
		public static object PopulateObjectMembers(object obj, MemberInfo[] members, object[] data)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (members == null)
			{
				throw new ArgumentNullException("members");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (members.Length != data.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_DataLengthDifferent"));
			}
			for (int i = 0; i < members.Length; i++)
			{
				MemberInfo memberInfo = members[i];
				if (memberInfo == null)
				{
					throw new ArgumentNullException("members", Environment.GetResourceString("ArgumentNull_NullMember", new object[] { i }));
				}
				if (data[i] != null)
				{
					if (memberInfo.MemberType != MemberTypes.Field)
					{
						throw new SerializationException(Environment.GetResourceString("Serialization_UnknownMemberInfo"));
					}
					FormatterServices.SerializationSetValue(memberInfo, obj, data[i]);
				}
			}
			return obj;
		}

		/// <summary>Extracts the data from the specified object and returns it as an array of objects.</summary>
		/// <param name="obj">The object to write to the formatter.</param>
		/// <param name="members">The members to extract from the object.</param>
		/// <returns>An array of <see cref="T:System.Object" /> that contains data stored in <paramref name="members" /> and associated with <paramref name="obj" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> or <paramref name="members" /> parameter is <see langword="null" />.  
		///  An element of <paramref name="members" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An element of <paramref name="members" /> does not represent a field.</exception>
		// Token: 0x060051B1 RID: 20913 RVA: 0x001211C4 File Offset: 0x0011F3C4
		[SecurityCritical]
		public static object[] GetObjectData(object obj, MemberInfo[] members)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (members == null)
			{
				throw new ArgumentNullException("members");
			}
			int num = members.Length;
			object[] array = new object[num];
			for (int i = 0; i < num; i++)
			{
				MemberInfo memberInfo = members[i];
				if (memberInfo == null)
				{
					throw new ArgumentNullException("members", Environment.GetResourceString("ArgumentNull_NullMember", new object[] { i }));
				}
				if (memberInfo.MemberType != MemberTypes.Field)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_UnknownMemberInfo"));
				}
				RtFieldInfo rtFieldInfo = memberInfo as RtFieldInfo;
				if (rtFieldInfo != null)
				{
					rtFieldInfo.CheckConsistency(obj);
					array[i] = rtFieldInfo.UnsafeGetValue(obj);
				}
				else
				{
					array[i] = ((SerializationFieldInfo)memberInfo).InternalGetValue(obj);
				}
			}
			return array;
		}

		/// <summary>Returns a serialization surrogate for the specified <see cref="T:System.Runtime.Serialization.ISerializationSurrogate" />.</summary>
		/// <param name="innerSurrogate">The specified surrogate.</param>
		/// <returns>An <see cref="T:System.Runtime.Serialization.ISerializationSurrogate" /> for the specified <paramref name="innerSurrogate" />.</returns>
		// Token: 0x060051B2 RID: 20914 RVA: 0x0012128D File Offset: 0x0011F48D
		[SecurityCritical]
		[ComVisible(false)]
		public static ISerializationSurrogate GetSurrogateForCyclicalReference(ISerializationSurrogate innerSurrogate)
		{
			if (innerSurrogate == null)
			{
				throw new ArgumentNullException("innerSurrogate");
			}
			return new SurrogateForCyclicalReference(innerSurrogate);
		}

		/// <summary>Looks up the <see cref="T:System.Type" /> of the specified object in the provided <see cref="T:System.Reflection.Assembly" />.</summary>
		/// <param name="assem">The assembly where you want to look up the object.</param>
		/// <param name="name">The name of the object.</param>
		/// <returns>The <see cref="T:System.Type" /> of the named object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="assem" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x060051B3 RID: 20915 RVA: 0x001212A3 File Offset: 0x0011F4A3
		[SecurityCritical]
		public static Type GetTypeFromAssembly(Assembly assem, string name)
		{
			if (assem == null)
			{
				throw new ArgumentNullException("assem");
			}
			return assem.GetType(name, false, false);
		}

		// Token: 0x060051B4 RID: 20916 RVA: 0x001212C4 File Offset: 0x0011F4C4
		internal static Assembly LoadAssemblyFromString(string assemblyName)
		{
			return Assembly.Load(assemblyName);
		}

		// Token: 0x060051B5 RID: 20917 RVA: 0x001212DC File Offset: 0x0011F4DC
		internal static Assembly LoadAssemblyFromStringNoThrow(string assemblyName)
		{
			try
			{
				return FormatterServices.LoadAssemblyFromString(assemblyName);
			}
			catch (Exception ex)
			{
			}
			return null;
		}

		// Token: 0x060051B6 RID: 20918 RVA: 0x00121308 File Offset: 0x0011F508
		internal static string GetClrAssemblyName(Type type, out bool hasTypeForwardedFrom)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			object[] customAttributes = type.GetCustomAttributes(typeof(TypeForwardedFromAttribute), false);
			if (customAttributes != null && customAttributes.Length != 0)
			{
				hasTypeForwardedFrom = true;
				TypeForwardedFromAttribute typeForwardedFromAttribute = (TypeForwardedFromAttribute)customAttributes[0];
				return typeForwardedFromAttribute.AssemblyFullName;
			}
			hasTypeForwardedFrom = false;
			return type.Assembly.FullName;
		}

		// Token: 0x060051B7 RID: 20919 RVA: 0x0012135D File Offset: 0x0011F55D
		internal static string GetClrTypeFullName(Type type)
		{
			if (type.IsArray)
			{
				return FormatterServices.GetClrTypeFullNameForArray(type);
			}
			return FormatterServices.GetClrTypeFullNameForNonArrayTypes(type);
		}

		// Token: 0x060051B8 RID: 20920 RVA: 0x00121374 File Offset: 0x0011F574
		private static string GetClrTypeFullNameForArray(Type type)
		{
			int arrayRank = type.GetArrayRank();
			if (arrayRank == 1)
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}{1}", FormatterServices.GetClrTypeFullName(type.GetElementType()), "[]");
			}
			StringBuilder stringBuilder = new StringBuilder(FormatterServices.GetClrTypeFullName(type.GetElementType())).Append("[");
			for (int i = 1; i < arrayRank; i++)
			{
				stringBuilder.Append(",");
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x060051B9 RID: 20921 RVA: 0x001213F4 File Offset: 0x0011F5F4
		private static string GetClrTypeFullNameForNonArrayTypes(Type type)
		{
			if (!type.IsGenericType)
			{
				return type.FullName;
			}
			Type[] genericArguments = type.GetGenericArguments();
			StringBuilder stringBuilder = new StringBuilder(type.GetGenericTypeDefinition().FullName).Append("[");
			foreach (Type type2 in genericArguments)
			{
				stringBuilder.Append("[").Append(FormatterServices.GetClrTypeFullName(type2)).Append(", ");
				bool flag;
				stringBuilder.Append(FormatterServices.GetClrAssemblyName(type2, out flag)).Append("],");
			}
			return stringBuilder.Remove(stringBuilder.Length - 1, 1).Append("]").ToString();
		}

		// Token: 0x04002442 RID: 9282
		internal static ConcurrentDictionary<MemberHolder, MemberInfo[]> m_MemberInfoTable = new ConcurrentDictionary<MemberHolder, MemberInfo[]>();

		// Token: 0x04002443 RID: 9283
		[SecurityCritical]
		private static bool unsafeTypeForwardersIsEnabled = false;

		// Token: 0x04002444 RID: 9284
		[SecurityCritical]
		private static volatile bool unsafeTypeForwardersIsEnabledInitialized = false;

		// Token: 0x04002445 RID: 9285
		private static readonly Type[] advancedTypes = new Type[]
		{
			typeof(DelegateSerializationHolder),
			typeof(ObjRef),
			typeof(IEnvoyInfo),
			typeof(ISponsor)
		};

		// Token: 0x04002446 RID: 9286
		private static Binder s_binder = Type.DefaultBinder;
	}
}
