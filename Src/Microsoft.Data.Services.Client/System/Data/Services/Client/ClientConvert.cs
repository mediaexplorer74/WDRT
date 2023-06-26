using System;
using System.Globalization;
using System.Reflection;

namespace System.Data.Services.Client
{
	// Token: 0x02000105 RID: 261
	internal static class ClientConvert
	{
		// Token: 0x06000882 RID: 2178 RVA: 0x0002372C File Offset: 0x0002192C
		internal static object ChangeType(string propertyValue, Type propertyType)
		{
			PrimitiveType primitiveType;
			if (PrimitiveType.TryGetPrimitiveType(propertyType, out primitiveType) && primitiveType.TypeConverter != null)
			{
				try
				{
					return primitiveType.TypeConverter.Parse(propertyValue);
				}
				catch (FormatException ex)
				{
					propertyValue = ((propertyValue.Length == 0) ? "String.Empty" : "String");
					throw Error.InvalidOperation(Strings.Deserialize_Current(propertyType.ToString(), propertyValue), ex);
				}
				catch (OverflowException ex2)
				{
					propertyValue = ((propertyValue.Length == 0) ? "String.Empty" : "String");
					throw Error.InvalidOperation(Strings.Deserialize_Current(propertyType.ToString(), propertyValue), ex2);
				}
				return propertyValue;
			}
			return propertyValue;
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x000237D0 File Offset: 0x000219D0
		internal static bool TryConvertBinaryToByteArray(object binaryValue, out byte[] converted)
		{
			Type type = binaryValue.GetType();
			PrimitiveType primitiveType;
			if (PrimitiveType.TryGetPrimitiveType(type, out primitiveType) && type == BinaryTypeConverter.BinaryType)
			{
				converted = (byte[])type.InvokeMember("ToArray", BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod, null, binaryValue, null, CultureInfo.InvariantCulture);
				return true;
			}
			converted = null;
			return false;
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00023820 File Offset: 0x00021A20
		internal static bool ToNamedType(string typeName, out Type type)
		{
			type = typeof(string);
			if (string.IsNullOrEmpty(typeName))
			{
				return true;
			}
			PrimitiveType primitiveType;
			if (PrimitiveType.TryGetPrimitiveType(typeName, out primitiveType))
			{
				type = primitiveType.ClrType;
				return true;
			}
			return false;
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00023858 File Offset: 0x00021A58
		internal static string ToString(object propertyValue)
		{
			PrimitiveType primitiveType;
			if (PrimitiveType.TryGetPrimitiveType(propertyValue.GetType(), out primitiveType) && primitiveType.TypeConverter != null)
			{
				return primitiveType.TypeConverter.ToString(propertyValue);
			}
			return propertyValue.ToString();
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00023890 File Offset: 0x00021A90
		internal static string GetEdmType(Type propertyType)
		{
			PrimitiveType primitiveType;
			if (!PrimitiveType.TryGetPrimitiveType(propertyType, out primitiveType))
			{
				return null;
			}
			if (primitiveType.EdmTypeName != null)
			{
				return primitiveType.EdmTypeName;
			}
			throw new NotSupportedException(Strings.ALinq_CantCastToUnsupportedPrimitive(propertyType.Name));
		}
	}
}
