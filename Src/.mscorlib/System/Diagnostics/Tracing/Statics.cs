using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200047E RID: 1150
	internal static class Statics
	{
		// Token: 0x0600370E RID: 14094 RVA: 0x000D533C File Offset: 0x000D353C
		public static byte[] MetadataForString(string name, int prefixSize, int suffixSize, int additionalSize)
		{
			Statics.CheckName(name);
			int num = Encoding.UTF8.GetByteCount(name) + 3 + prefixSize + suffixSize;
			byte[] array = new byte[num];
			ushort num2 = checked((ushort)(num + additionalSize));
			array[0] = (byte)num2;
			array[1] = (byte)(num2 >> 8);
			Encoding.UTF8.GetBytes(name, 0, name.Length, array, 2 + prefixSize);
			return array;
		}

		// Token: 0x0600370F RID: 14095 RVA: 0x000D5394 File Offset: 0x000D3594
		public static void EncodeTags(int tags, ref int pos, byte[] metadata)
		{
			int num = tags & 268435455;
			bool flag;
			do
			{
				byte b = (byte)((num >> 21) & 127);
				flag = (num & 2097151) != 0;
				b |= (flag ? 128 : 0);
				num <<= 7;
				if (metadata != null)
				{
					metadata[pos] = b;
				}
				pos++;
			}
			while (flag);
		}

		// Token: 0x06003710 RID: 14096 RVA: 0x000D53E2 File Offset: 0x000D35E2
		public static byte Combine(int settingValue, byte defaultValue)
		{
			if ((int)((byte)settingValue) != settingValue)
			{
				return defaultValue;
			}
			return (byte)settingValue;
		}

		// Token: 0x06003711 RID: 14097 RVA: 0x000D53ED File Offset: 0x000D35ED
		public static byte Combine(int settingValue1, int settingValue2, byte defaultValue)
		{
			if ((int)((byte)settingValue1) == settingValue1)
			{
				return (byte)settingValue1;
			}
			if ((int)((byte)settingValue2) != settingValue2)
			{
				return defaultValue;
			}
			return (byte)settingValue2;
		}

		// Token: 0x06003712 RID: 14098 RVA: 0x000D5400 File Offset: 0x000D3600
		public static int Combine(int settingValue1, int settingValue2)
		{
			if ((int)((byte)settingValue1) != settingValue1)
			{
				return settingValue2;
			}
			return settingValue1;
		}

		// Token: 0x06003713 RID: 14099 RVA: 0x000D540A File Offset: 0x000D360A
		public static void CheckName(string name)
		{
			if (name != null && 0 <= name.IndexOf('\0'))
			{
				throw new ArgumentOutOfRangeException("name");
			}
		}

		// Token: 0x06003714 RID: 14100 RVA: 0x000D5424 File Offset: 0x000D3624
		public static bool ShouldOverrideFieldName(string fieldName)
		{
			return fieldName.Length <= 2 && fieldName[0] == '_';
		}

		// Token: 0x06003715 RID: 14101 RVA: 0x000D543C File Offset: 0x000D363C
		public static TraceLoggingDataType MakeDataType(TraceLoggingDataType baseType, EventFieldFormat format)
		{
			return (baseType & (TraceLoggingDataType)31) | (TraceLoggingDataType)(format << 8);
		}

		// Token: 0x06003716 RID: 14102 RVA: 0x000D5446 File Offset: 0x000D3646
		public static TraceLoggingDataType Format8(EventFieldFormat format, TraceLoggingDataType native)
		{
			switch (format)
			{
			case EventFieldFormat.Default:
				return native;
			case EventFieldFormat.String:
				return TraceLoggingDataType.Char8;
			case EventFieldFormat.Boolean:
				return TraceLoggingDataType.Boolean8;
			case EventFieldFormat.Hexadecimal:
				return TraceLoggingDataType.HexInt8;
			}
			return Statics.MakeDataType(native, format);
		}

		// Token: 0x06003717 RID: 14103 RVA: 0x000D547F File Offset: 0x000D367F
		public static TraceLoggingDataType Format16(EventFieldFormat format, TraceLoggingDataType native)
		{
			switch (format)
			{
			case EventFieldFormat.Default:
				return native;
			case EventFieldFormat.String:
				return TraceLoggingDataType.Char16;
			case EventFieldFormat.Hexadecimal:
				return TraceLoggingDataType.HexInt16;
			}
			return Statics.MakeDataType(native, format);
		}

		// Token: 0x06003718 RID: 14104 RVA: 0x000D54B2 File Offset: 0x000D36B2
		public static TraceLoggingDataType Format32(EventFieldFormat format, TraceLoggingDataType native)
		{
			switch (format)
			{
			case EventFieldFormat.Default:
				return native;
			case (EventFieldFormat)1:
			case EventFieldFormat.String:
				break;
			case EventFieldFormat.Boolean:
				return TraceLoggingDataType.Boolean32;
			case EventFieldFormat.Hexadecimal:
				return TraceLoggingDataType.HexInt32;
			default:
				if (format == EventFieldFormat.HResult)
				{
					return TraceLoggingDataType.HResult;
				}
				break;
			}
			return Statics.MakeDataType(native, format);
		}

		// Token: 0x06003719 RID: 14105 RVA: 0x000D54EA File Offset: 0x000D36EA
		public static TraceLoggingDataType Format64(EventFieldFormat format, TraceLoggingDataType native)
		{
			if (format == EventFieldFormat.Default)
			{
				return native;
			}
			if (format != EventFieldFormat.Hexadecimal)
			{
				return Statics.MakeDataType(native, format);
			}
			return TraceLoggingDataType.HexInt64;
		}

		// Token: 0x0600371A RID: 14106 RVA: 0x000D5501 File Offset: 0x000D3701
		public static TraceLoggingDataType FormatPtr(EventFieldFormat format, TraceLoggingDataType native)
		{
			if (format == EventFieldFormat.Default)
			{
				return native;
			}
			if (format != EventFieldFormat.Hexadecimal)
			{
				return Statics.MakeDataType(native, format);
			}
			return Statics.HexIntPtrType;
		}

		// Token: 0x0600371B RID: 14107 RVA: 0x000D551B File Offset: 0x000D371B
		public static object CreateInstance(Type type, params object[] parameters)
		{
			return Activator.CreateInstance(type, parameters);
		}

		// Token: 0x0600371C RID: 14108 RVA: 0x000D5524 File Offset: 0x000D3724
		public static bool IsValueType(Type type)
		{
			return type.IsValueType;
		}

		// Token: 0x0600371D RID: 14109 RVA: 0x000D553C File Offset: 0x000D373C
		public static bool IsEnum(Type type)
		{
			return type.IsEnum;
		}

		// Token: 0x0600371E RID: 14110 RVA: 0x000D5554 File Offset: 0x000D3754
		public static IEnumerable<PropertyInfo> GetProperties(Type type)
		{
			return type.GetProperties();
		}

		// Token: 0x0600371F RID: 14111 RVA: 0x000D556C File Offset: 0x000D376C
		public static MethodInfo GetGetMethod(PropertyInfo propInfo)
		{
			return propInfo.GetGetMethod();
		}

		// Token: 0x06003720 RID: 14112 RVA: 0x000D5584 File Offset: 0x000D3784
		public static MethodInfo GetDeclaredStaticMethod(Type declaringType, string name)
		{
			return declaringType.GetMethod(name, BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.NonPublic);
		}

		// Token: 0x06003721 RID: 14113 RVA: 0x000D559C File Offset: 0x000D379C
		public static bool HasCustomAttribute(PropertyInfo propInfo, Type attributeType)
		{
			object[] customAttributes = propInfo.GetCustomAttributes(attributeType, false);
			return customAttributes.Length != 0;
		}

		// Token: 0x06003722 RID: 14114 RVA: 0x000D55BC File Offset: 0x000D37BC
		public static AttributeType GetCustomAttribute<AttributeType>(PropertyInfo propInfo) where AttributeType : Attribute
		{
			AttributeType attributeType = default(AttributeType);
			object[] customAttributes = propInfo.GetCustomAttributes(typeof(AttributeType), false);
			if (customAttributes.Length != 0)
			{
				attributeType = (AttributeType)((object)customAttributes[0]);
			}
			return attributeType;
		}

		// Token: 0x06003723 RID: 14115 RVA: 0x000D55F4 File Offset: 0x000D37F4
		public static AttributeType GetCustomAttribute<AttributeType>(Type type) where AttributeType : Attribute
		{
			AttributeType attributeType = default(AttributeType);
			object[] customAttributes = type.GetCustomAttributes(typeof(AttributeType), false);
			if (customAttributes.Length != 0)
			{
				attributeType = (AttributeType)((object)customAttributes[0]);
			}
			return attributeType;
		}

		// Token: 0x06003724 RID: 14116 RVA: 0x000D5629 File Offset: 0x000D3829
		public static Type[] GetGenericArguments(Type type)
		{
			return type.GetGenericArguments();
		}

		// Token: 0x06003725 RID: 14117 RVA: 0x000D5634 File Offset: 0x000D3834
		public static Type FindEnumerableElementType(Type type)
		{
			Type type2 = null;
			if (Statics.IsGenericMatch(type, typeof(IEnumerable<>)))
			{
				type2 = Statics.GetGenericArguments(type)[0];
			}
			else
			{
				Type[] array = type.FindInterfaces(new TypeFilter(Statics.IsGenericMatch), typeof(IEnumerable<>));
				foreach (Type type3 in array)
				{
					if (type2 != null)
					{
						type2 = null;
						break;
					}
					type2 = Statics.GetGenericArguments(type3)[0];
				}
			}
			return type2;
		}

		// Token: 0x06003726 RID: 14118 RVA: 0x000D56AC File Offset: 0x000D38AC
		public static bool IsGenericMatch(Type type, object openType)
		{
			bool isGenericType = type.IsGenericType;
			return isGenericType && type.GetGenericTypeDefinition() == (Type)openType;
		}

		// Token: 0x06003727 RID: 14119 RVA: 0x000D56D8 File Offset: 0x000D38D8
		public static Delegate CreateDelegate(Type delegateType, MethodInfo methodInfo)
		{
			return Delegate.CreateDelegate(delegateType, methodInfo);
		}

		// Token: 0x06003728 RID: 14120 RVA: 0x000D56F0 File Offset: 0x000D38F0
		public static TraceLoggingTypeInfo GetTypeInfoInstance(Type dataType, List<Type> recursionCheck)
		{
			TraceLoggingTypeInfo traceLoggingTypeInfo;
			if (dataType == typeof(int))
			{
				traceLoggingTypeInfo = TraceLoggingTypeInfo<int>.Instance;
			}
			else if (dataType == typeof(long))
			{
				traceLoggingTypeInfo = TraceLoggingTypeInfo<long>.Instance;
			}
			else if (dataType == typeof(string))
			{
				traceLoggingTypeInfo = TraceLoggingTypeInfo<string>.Instance;
			}
			else
			{
				MethodInfo declaredStaticMethod = Statics.GetDeclaredStaticMethod(typeof(TraceLoggingTypeInfo<>).MakeGenericType(new Type[] { dataType }), "GetInstance");
				object obj = declaredStaticMethod.Invoke(null, new object[] { recursionCheck });
				traceLoggingTypeInfo = (TraceLoggingTypeInfo)obj;
			}
			return traceLoggingTypeInfo;
		}

		// Token: 0x06003729 RID: 14121 RVA: 0x000D578C File Offset: 0x000D398C
		public static TraceLoggingTypeInfo<DataType> CreateDefaultTypeInfo<DataType>(List<Type> recursionCheck)
		{
			Type typeFromHandle = typeof(DataType);
			if (recursionCheck.Contains(typeFromHandle))
			{
				throw new NotSupportedException(Environment.GetResourceString("EventSource_RecursiveTypeDefinition"));
			}
			recursionCheck.Add(typeFromHandle);
			EventDataAttribute customAttribute = Statics.GetCustomAttribute<EventDataAttribute>(typeFromHandle);
			TraceLoggingTypeInfo traceLoggingTypeInfo;
			if (customAttribute != null || Statics.GetCustomAttribute<CompilerGeneratedAttribute>(typeFromHandle) != null)
			{
				TypeAnalysis typeAnalysis = new TypeAnalysis(typeFromHandle, customAttribute, recursionCheck);
				traceLoggingTypeInfo = new InvokeTypeInfo<DataType>(typeAnalysis);
			}
			else if (typeFromHandle.IsArray)
			{
				Type elementType = typeFromHandle.GetElementType();
				if (elementType == typeof(bool))
				{
					traceLoggingTypeInfo = new BooleanArrayTypeInfo();
				}
				else if (elementType == typeof(byte))
				{
					traceLoggingTypeInfo = new ByteArrayTypeInfo();
				}
				else if (elementType == typeof(sbyte))
				{
					traceLoggingTypeInfo = new SByteArrayTypeInfo();
				}
				else if (elementType == typeof(short))
				{
					traceLoggingTypeInfo = new Int16ArrayTypeInfo();
				}
				else if (elementType == typeof(ushort))
				{
					traceLoggingTypeInfo = new UInt16ArrayTypeInfo();
				}
				else if (elementType == typeof(int))
				{
					traceLoggingTypeInfo = new Int32ArrayTypeInfo();
				}
				else if (elementType == typeof(uint))
				{
					traceLoggingTypeInfo = new UInt32ArrayTypeInfo();
				}
				else if (elementType == typeof(long))
				{
					traceLoggingTypeInfo = new Int64ArrayTypeInfo();
				}
				else if (elementType == typeof(ulong))
				{
					traceLoggingTypeInfo = new UInt64ArrayTypeInfo();
				}
				else if (elementType == typeof(char))
				{
					traceLoggingTypeInfo = new CharArrayTypeInfo();
				}
				else if (elementType == typeof(double))
				{
					traceLoggingTypeInfo = new DoubleArrayTypeInfo();
				}
				else if (elementType == typeof(float))
				{
					traceLoggingTypeInfo = new SingleArrayTypeInfo();
				}
				else if (elementType == typeof(IntPtr))
				{
					traceLoggingTypeInfo = new IntPtrArrayTypeInfo();
				}
				else if (elementType == typeof(UIntPtr))
				{
					traceLoggingTypeInfo = new UIntPtrArrayTypeInfo();
				}
				else if (elementType == typeof(Guid))
				{
					traceLoggingTypeInfo = new GuidArrayTypeInfo();
				}
				else
				{
					traceLoggingTypeInfo = (TraceLoggingTypeInfo<DataType>)Statics.CreateInstance(typeof(ArrayTypeInfo<>).MakeGenericType(new Type[] { elementType }), new object[] { Statics.GetTypeInfoInstance(elementType, recursionCheck) });
				}
			}
			else if (Statics.IsEnum(typeFromHandle))
			{
				Type underlyingType = Enum.GetUnderlyingType(typeFromHandle);
				if (underlyingType == typeof(int))
				{
					traceLoggingTypeInfo = new EnumInt32TypeInfo<DataType>();
				}
				else if (underlyingType == typeof(uint))
				{
					traceLoggingTypeInfo = new EnumUInt32TypeInfo<DataType>();
				}
				else if (underlyingType == typeof(byte))
				{
					traceLoggingTypeInfo = new EnumByteTypeInfo<DataType>();
				}
				else if (underlyingType == typeof(sbyte))
				{
					traceLoggingTypeInfo = new EnumSByteTypeInfo<DataType>();
				}
				else if (underlyingType == typeof(short))
				{
					traceLoggingTypeInfo = new EnumInt16TypeInfo<DataType>();
				}
				else if (underlyingType == typeof(ushort))
				{
					traceLoggingTypeInfo = new EnumUInt16TypeInfo<DataType>();
				}
				else if (underlyingType == typeof(long))
				{
					traceLoggingTypeInfo = new EnumInt64TypeInfo<DataType>();
				}
				else
				{
					if (!(underlyingType == typeof(ulong)))
					{
						throw new NotSupportedException(Environment.GetResourceString("EventSource_NotSupportedEnumType", new object[] { typeFromHandle.Name, underlyingType.Name }));
					}
					traceLoggingTypeInfo = new EnumUInt64TypeInfo<DataType>();
				}
			}
			else if (typeFromHandle == typeof(string))
			{
				traceLoggingTypeInfo = new StringTypeInfo();
			}
			else if (typeFromHandle == typeof(bool))
			{
				traceLoggingTypeInfo = new BooleanTypeInfo();
			}
			else if (typeFromHandle == typeof(byte))
			{
				traceLoggingTypeInfo = new ByteTypeInfo();
			}
			else if (typeFromHandle == typeof(sbyte))
			{
				traceLoggingTypeInfo = new SByteTypeInfo();
			}
			else if (typeFromHandle == typeof(short))
			{
				traceLoggingTypeInfo = new Int16TypeInfo();
			}
			else if (typeFromHandle == typeof(ushort))
			{
				traceLoggingTypeInfo = new UInt16TypeInfo();
			}
			else if (typeFromHandle == typeof(int))
			{
				traceLoggingTypeInfo = new Int32TypeInfo();
			}
			else if (typeFromHandle == typeof(uint))
			{
				traceLoggingTypeInfo = new UInt32TypeInfo();
			}
			else if (typeFromHandle == typeof(long))
			{
				traceLoggingTypeInfo = new Int64TypeInfo();
			}
			else if (typeFromHandle == typeof(ulong))
			{
				traceLoggingTypeInfo = new UInt64TypeInfo();
			}
			else if (typeFromHandle == typeof(char))
			{
				traceLoggingTypeInfo = new CharTypeInfo();
			}
			else if (typeFromHandle == typeof(double))
			{
				traceLoggingTypeInfo = new DoubleTypeInfo();
			}
			else if (typeFromHandle == typeof(float))
			{
				traceLoggingTypeInfo = new SingleTypeInfo();
			}
			else if (typeFromHandle == typeof(DateTime))
			{
				traceLoggingTypeInfo = new DateTimeTypeInfo();
			}
			else if (typeFromHandle == typeof(decimal))
			{
				traceLoggingTypeInfo = new DecimalTypeInfo();
			}
			else if (typeFromHandle == typeof(IntPtr))
			{
				traceLoggingTypeInfo = new IntPtrTypeInfo();
			}
			else if (typeFromHandle == typeof(UIntPtr))
			{
				traceLoggingTypeInfo = new UIntPtrTypeInfo();
			}
			else if (typeFromHandle == typeof(Guid))
			{
				traceLoggingTypeInfo = new GuidTypeInfo();
			}
			else if (typeFromHandle == typeof(TimeSpan))
			{
				traceLoggingTypeInfo = new TimeSpanTypeInfo();
			}
			else if (typeFromHandle == typeof(DateTimeOffset))
			{
				traceLoggingTypeInfo = new DateTimeOffsetTypeInfo();
			}
			else if (typeFromHandle == typeof(EmptyStruct))
			{
				traceLoggingTypeInfo = new NullTypeInfo<EmptyStruct>();
			}
			else if (Statics.IsGenericMatch(typeFromHandle, typeof(KeyValuePair<, >)))
			{
				Type[] genericArguments = Statics.GetGenericArguments(typeFromHandle);
				traceLoggingTypeInfo = (TraceLoggingTypeInfo<DataType>)Statics.CreateInstance(typeof(KeyValuePairTypeInfo<, >).MakeGenericType(new Type[]
				{
					genericArguments[0],
					genericArguments[1]
				}), new object[] { recursionCheck });
			}
			else if (Statics.IsGenericMatch(typeFromHandle, typeof(Nullable<>)))
			{
				Type[] genericArguments2 = Statics.GetGenericArguments(typeFromHandle);
				traceLoggingTypeInfo = (TraceLoggingTypeInfo<DataType>)Statics.CreateInstance(typeof(NullableTypeInfo<>).MakeGenericType(new Type[] { genericArguments2[0] }), new object[] { recursionCheck });
			}
			else
			{
				Type type = Statics.FindEnumerableElementType(typeFromHandle);
				if (!(type != null))
				{
					throw new ArgumentException(Environment.GetResourceString("EventSource_NonCompliantTypeError", new object[] { typeFromHandle.Name }));
				}
				traceLoggingTypeInfo = (TraceLoggingTypeInfo<DataType>)Statics.CreateInstance(typeof(EnumerableTypeInfo<, >).MakeGenericType(new Type[] { typeFromHandle, type }), new object[] { Statics.GetTypeInfoInstance(type, recursionCheck) });
			}
			return (TraceLoggingTypeInfo<DataType>)traceLoggingTypeInfo;
		}

		// Token: 0x04001867 RID: 6247
		public const byte DefaultLevel = 5;

		// Token: 0x04001868 RID: 6248
		public const byte TraceLoggingChannel = 11;

		// Token: 0x04001869 RID: 6249
		public const byte InTypeMask = 31;

		// Token: 0x0400186A RID: 6250
		public const byte InTypeFixedCountFlag = 32;

		// Token: 0x0400186B RID: 6251
		public const byte InTypeVariableCountFlag = 64;

		// Token: 0x0400186C RID: 6252
		public const byte InTypeCustomCountFlag = 96;

		// Token: 0x0400186D RID: 6253
		public const byte InTypeCountMask = 96;

		// Token: 0x0400186E RID: 6254
		public const byte InTypeChainFlag = 128;

		// Token: 0x0400186F RID: 6255
		public const byte OutTypeMask = 127;

		// Token: 0x04001870 RID: 6256
		public const byte OutTypeChainFlag = 128;

		// Token: 0x04001871 RID: 6257
		public const EventTags EventTagsMask = (EventTags)268435455;

		// Token: 0x04001872 RID: 6258
		public static readonly TraceLoggingDataType IntPtrType = ((IntPtr.Size == 8) ? TraceLoggingDataType.Int64 : TraceLoggingDataType.Int32);

		// Token: 0x04001873 RID: 6259
		public static readonly TraceLoggingDataType UIntPtrType = ((IntPtr.Size == 8) ? TraceLoggingDataType.UInt64 : TraceLoggingDataType.UInt32);

		// Token: 0x04001874 RID: 6260
		public static readonly TraceLoggingDataType HexIntPtrType = ((IntPtr.Size == 8) ? TraceLoggingDataType.HexInt64 : TraceLoggingDataType.HexInt32);
	}
}
