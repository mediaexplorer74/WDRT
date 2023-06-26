using System;
using System.Spatial;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.Edm.Library.Values;
using Microsoft.Data.Edm.Values;

namespace Microsoft.Data.OData.Evaluation
{
	// Token: 0x02000163 RID: 355
	internal static class EdmValueUtils
	{
		// Token: 0x060009BA RID: 2490 RVA: 0x0001EB28 File Offset: 0x0001CD28
		internal static IEdmDelayedValue ConvertPrimitiveValue(object primitiveValue, IEdmPrimitiveTypeReference type)
		{
			switch (PlatformHelper.GetTypeCode(primitiveValue.GetType()))
			{
			case TypeCode.Boolean:
				type = EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.Boolean);
				return new EdmBooleanConstant(type, (bool)primitiveValue);
			case TypeCode.SByte:
				type = EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.SByte);
				return new EdmIntegerConstant(type, (long)((sbyte)primitiveValue));
			case TypeCode.Byte:
				type = EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.Byte);
				return new EdmIntegerConstant(type, (long)((ulong)((byte)primitiveValue)));
			case TypeCode.Int16:
				type = EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.Int16);
				return new EdmIntegerConstant(type, (long)((short)primitiveValue));
			case TypeCode.Int32:
				type = EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.Int32);
				return new EdmIntegerConstant(type, (long)((int)primitiveValue));
			case TypeCode.Int64:
				type = EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.Int64);
				return new EdmIntegerConstant(type, (long)primitiveValue);
			case TypeCode.Single:
				type = EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.Single);
				return new EdmFloatingConstant(type, (double)((float)primitiveValue));
			case TypeCode.Double:
				type = EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.Double);
				return new EdmFloatingConstant(type, (double)primitiveValue);
			case TypeCode.Decimal:
			{
				IEdmDecimalTypeReference edmDecimalTypeReference = (IEdmDecimalTypeReference)EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.Decimal);
				return new EdmDecimalConstant(edmDecimalTypeReference, (decimal)primitiveValue);
			}
			case TypeCode.DateTime:
			{
				IEdmTemporalTypeReference edmTemporalTypeReference = (IEdmTemporalTypeReference)EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.DateTime);
				return new EdmDateTimeConstant(edmTemporalTypeReference, (DateTime)primitiveValue);
			}
			case TypeCode.String:
			{
				IEdmStringTypeReference edmStringTypeReference = (IEdmStringTypeReference)EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.String);
				return new EdmStringConstant(edmStringTypeReference, (string)primitiveValue);
			}
			}
			return EdmValueUtils.ConvertPrimitiveValueWithoutTypeCode(primitiveValue, type);
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0001ECA4 File Offset: 0x0001CEA4
		internal static object ToClrValue(this IEdmPrimitiveValue edmValue)
		{
			EdmPrimitiveTypeKind edmPrimitiveTypeKind = edmValue.Type.PrimitiveKind();
			switch (edmValue.ValueKind)
			{
			case EdmValueKind.Binary:
				return ((IEdmBinaryValue)edmValue).Value;
			case EdmValueKind.Boolean:
				return ((IEdmBooleanValue)edmValue).Value;
			case EdmValueKind.DateTimeOffset:
				return ((IEdmDateTimeOffsetValue)edmValue).Value;
			case EdmValueKind.DateTime:
				return ((IEdmDateTimeValue)edmValue).Value;
			case EdmValueKind.Decimal:
				return ((IEdmDecimalValue)edmValue).Value;
			case EdmValueKind.Floating:
				return EdmValueUtils.ConvertFloatingValue((IEdmFloatingValue)edmValue, edmPrimitiveTypeKind);
			case EdmValueKind.Guid:
				return ((IEdmGuidValue)edmValue).Value;
			case EdmValueKind.Integer:
				return EdmValueUtils.ConvertIntegerValue((IEdmIntegerValue)edmValue, edmPrimitiveTypeKind);
			case EdmValueKind.String:
				return ((IEdmStringValue)edmValue).Value;
			case EdmValueKind.Time:
				return ((IEdmTimeValue)edmValue).Value;
			}
			throw new ODataException(Strings.EdmValueUtils_CannotConvertTypeToClrValue(edmValue.ValueKind));
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0001EDB4 File Offset: 0x0001CFB4
		internal static bool TryGetStreamProperty(IEdmStructuredValue entityInstance, string streamPropertyName, out IEdmProperty streamProperty)
		{
			streamProperty = null;
			if (streamPropertyName != null)
			{
				streamProperty = entityInstance.Type.AsEntity().FindProperty(streamPropertyName);
				if (streamProperty == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0001EDD8 File Offset: 0x0001CFD8
		internal static object GetPrimitivePropertyClrValue(this IEdmStructuredValue structuredValue, string propertyName)
		{
			IEdmStructuredTypeReference edmStructuredTypeReference = structuredValue.Type.AsStructured();
			IEdmPropertyValue edmPropertyValue = structuredValue.FindPropertyValue(propertyName);
			if (edmPropertyValue == null)
			{
				throw new ODataException(Strings.EdmValueUtils_PropertyDoesntExist(edmStructuredTypeReference.FullName(), propertyName));
			}
			if (edmPropertyValue.Value.ValueKind == EdmValueKind.Null)
			{
				return null;
			}
			IEdmPrimitiveValue edmPrimitiveValue = edmPropertyValue.Value as IEdmPrimitiveValue;
			if (edmPrimitiveValue == null)
			{
				throw new ODataException(Strings.EdmValueUtils_NonPrimitiveValue(edmPropertyValue.Name, edmStructuredTypeReference.FullName()));
			}
			return edmPrimitiveValue.ToClrValue();
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0001EE4C File Offset: 0x0001D04C
		private static object ConvertFloatingValue(IEdmFloatingValue floatingValue, EdmPrimitiveTypeKind primitiveKind)
		{
			double value = floatingValue.Value;
			if (primitiveKind == EdmPrimitiveTypeKind.Single)
			{
				return Convert.ToSingle(value);
			}
			return value;
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0001EE78 File Offset: 0x0001D078
		private static object ConvertIntegerValue(IEdmIntegerValue integerValue, EdmPrimitiveTypeKind primitiveKind)
		{
			long value = integerValue.Value;
			if (primitiveKind != EdmPrimitiveTypeKind.Byte)
			{
				switch (primitiveKind)
				{
				case EdmPrimitiveTypeKind.Int16:
					return Convert.ToInt16(value);
				case EdmPrimitiveTypeKind.Int32:
					return Convert.ToInt32(value);
				case EdmPrimitiveTypeKind.SByte:
					return Convert.ToSByte(value);
				}
				return value;
			}
			return Convert.ToByte(value);
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x0001EEE4 File Offset: 0x0001D0E4
		private static IEdmDelayedValue ConvertPrimitiveValueWithoutTypeCode(object primitiveValue, IEdmPrimitiveTypeReference type)
		{
			byte[] array = primitiveValue as byte[];
			if (array != null)
			{
				IEdmBinaryTypeReference edmBinaryTypeReference = (IEdmBinaryTypeReference)EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.Binary);
				return new EdmBinaryConstant(edmBinaryTypeReference, array);
			}
			if (primitiveValue is DateTimeOffset)
			{
				IEdmTemporalTypeReference edmTemporalTypeReference = (IEdmTemporalTypeReference)EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.DateTimeOffset);
				return new EdmDateTimeOffsetConstant(edmTemporalTypeReference, (DateTimeOffset)primitiveValue);
			}
			if (primitiveValue is Guid)
			{
				type = EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.Guid);
				return new EdmGuidConstant(type, (Guid)primitiveValue);
			}
			if (primitiveValue is TimeSpan)
			{
				IEdmTemporalTypeReference edmTemporalTypeReference2 = (IEdmTemporalTypeReference)EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.Time);
				return new EdmTimeConstant(edmTemporalTypeReference2, (TimeSpan)primitiveValue);
			}
			if (primitiveValue is ISpatial)
			{
				throw new NotImplementedException();
			}
			throw new ODataException(Strings.EdmValueUtils_UnsupportedPrimitiveType(primitiveValue.GetType().FullName));
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0001EF98 File Offset: 0x0001D198
		private static IEdmPrimitiveTypeReference EnsurePrimitiveType(IEdmPrimitiveTypeReference type, EdmPrimitiveTypeKind primitiveKindFromValue)
		{
			if (type == null)
			{
				type = EdmCoreModel.Instance.GetPrimitive(primitiveKindFromValue, true);
			}
			else
			{
				EdmPrimitiveTypeKind primitiveKind = type.PrimitiveDefinition().PrimitiveKind;
				if (primitiveKind != primitiveKindFromValue)
				{
					string text = type.FullName();
					if (text == null)
					{
						throw new ODataException(Strings.EdmValueUtils_IncorrectPrimitiveTypeKindNoTypeName(primitiveKind.ToString(), primitiveKindFromValue.ToString()));
					}
					throw new ODataException(Strings.EdmValueUtils_IncorrectPrimitiveTypeKind(text, primitiveKindFromValue.ToString(), primitiveKind.ToString()));
				}
			}
			return type;
		}
	}
}
