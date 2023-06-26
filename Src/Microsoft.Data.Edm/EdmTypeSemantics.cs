using System;
using System.Collections.Generic;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.Edm.Library.Internal;
using Microsoft.Data.Edm.Validation;

namespace Microsoft.Data.Edm
{
	// Token: 0x020001EC RID: 492
	public static class EdmTypeSemantics
	{
		// Token: 0x06000BA5 RID: 2981 RVA: 0x00021CD7 File Offset: 0x0001FED7
		public static bool IsCollection(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.TypeKind() == EdmTypeKind.Collection;
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x00021CEE File Offset: 0x0001FEEE
		public static bool IsEntity(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.TypeKind() == EdmTypeKind.Entity;
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x00021D05 File Offset: 0x0001FF05
		public static bool IsEntityReference(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.TypeKind() == EdmTypeKind.EntityReference;
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x00021D1C File Offset: 0x0001FF1C
		public static bool IsComplex(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.TypeKind() == EdmTypeKind.Complex;
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x00021D33 File Offset: 0x0001FF33
		public static bool IsEnum(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.TypeKind() == EdmTypeKind.Enum;
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x00021D4A File Offset: 0x0001FF4A
		public static bool IsRow(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.TypeKind() == EdmTypeKind.Row;
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x00021D64 File Offset: 0x0001FF64
		public static bool IsStructured(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			switch (type.TypeKind())
			{
			case EdmTypeKind.Entity:
			case EdmTypeKind.Complex:
			case EdmTypeKind.Row:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x00021DA0 File Offset: 0x0001FFA0
		public static bool IsStructured(this EdmTypeKind typeKind)
		{
			switch (typeKind)
			{
			case EdmTypeKind.Entity:
			case EdmTypeKind.Complex:
			case EdmTypeKind.Row:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x00021DC8 File Offset: 0x0001FFC8
		public static bool IsPrimitive(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.TypeKind() == EdmTypeKind.Primitive;
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x00021DDF File Offset: 0x0001FFDF
		public static bool IsBinary(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Binary;
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x00021DF6 File Offset: 0x0001FFF6
		public static bool IsBoolean(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Boolean;
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x00021E0D File Offset: 0x0002000D
		public static bool IsTemporal(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind().IsTemporal();
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x00021E28 File Offset: 0x00020028
		public static bool IsTemporal(this EdmPrimitiveTypeKind typeKind)
		{
			switch (typeKind)
			{
			case EdmPrimitiveTypeKind.DateTime:
			case EdmPrimitiveTypeKind.DateTimeOffset:
				break;
			default:
				if (typeKind != EdmPrimitiveTypeKind.Time)
				{
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x00021E4F File Offset: 0x0002004F
		public static bool IsDateTime(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.DateTime;
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x00021E66 File Offset: 0x00020066
		public static bool IsTime(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Time;
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00021E7E File Offset: 0x0002007E
		public static bool IsDateTimeOffset(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.DateTimeOffset;
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x00021E95 File Offset: 0x00020095
		public static bool IsDecimal(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Decimal;
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x00021EAC File Offset: 0x000200AC
		public static bool IsFloating(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			EdmPrimitiveTypeKind edmPrimitiveTypeKind = type.PrimitiveKind();
			return edmPrimitiveTypeKind == EdmPrimitiveTypeKind.Double || edmPrimitiveTypeKind == EdmPrimitiveTypeKind.Single;
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x00021ED8 File Offset: 0x000200D8
		public static bool IsSingle(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Single;
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x00021EF0 File Offset: 0x000200F0
		public static bool IsDouble(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Double;
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x00021F07 File Offset: 0x00020107
		public static bool IsGuid(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Guid;
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x00021F20 File Offset: 0x00020120
		public static bool IsSignedIntegral(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			switch (type.PrimitiveKind())
			{
			case EdmPrimitiveTypeKind.Int16:
			case EdmPrimitiveTypeKind.Int32:
			case EdmPrimitiveTypeKind.Int64:
			case EdmPrimitiveTypeKind.SByte:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x00021F5E File Offset: 0x0002015E
		public static bool IsSByte(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.SByte;
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x00021F76 File Offset: 0x00020176
		public static bool IsInt16(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Int16;
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x00021F8E File Offset: 0x0002018E
		public static bool IsInt32(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Int32;
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x00021FA6 File Offset: 0x000201A6
		public static bool IsInt64(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Int64;
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x00021FC0 File Offset: 0x000201C0
		public static bool IsIntegral(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			EdmPrimitiveTypeKind edmPrimitiveTypeKind = type.PrimitiveKind();
			if (edmPrimitiveTypeKind != EdmPrimitiveTypeKind.Byte)
			{
				switch (edmPrimitiveTypeKind)
				{
				case EdmPrimitiveTypeKind.Int16:
				case EdmPrimitiveTypeKind.Int32:
				case EdmPrimitiveTypeKind.Int64:
				case EdmPrimitiveTypeKind.SByte:
					break;
				default:
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x00022004 File Offset: 0x00020204
		public static bool IsIntegral(this EdmPrimitiveTypeKind primitiveTypeKind)
		{
			if (primitiveTypeKind != EdmPrimitiveTypeKind.Byte)
			{
				switch (primitiveTypeKind)
				{
				case EdmPrimitiveTypeKind.Int16:
				case EdmPrimitiveTypeKind.Int32:
				case EdmPrimitiveTypeKind.Int64:
				case EdmPrimitiveTypeKind.SByte:
					break;
				default:
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x00022035 File Offset: 0x00020235
		public static bool IsByte(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Byte;
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0002204C File Offset: 0x0002024C
		public static bool IsString(this IEdmTypeReference type)
		{
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.String;
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x00022058 File Offset: 0x00020258
		public static bool IsStream(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.PrimitiveKind() == EdmPrimitiveTypeKind.Stream;
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x00022070 File Offset: 0x00020270
		public static bool IsSpatial(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.Definition.IsSpatial();
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0002208C File Offset: 0x0002028C
		public static bool IsSpatial(this IEdmType type)
		{
			EdmUtil.CheckArgumentNull<IEdmType>(type, "type");
			IEdmPrimitiveType edmPrimitiveType = type as IEdmPrimitiveType;
			return edmPrimitiveType != null && edmPrimitiveType.PrimitiveKind.IsSpatial();
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x000220BC File Offset: 0x000202BC
		public static bool IsSpatial(this EdmPrimitiveTypeKind typeKind)
		{
			switch (typeKind)
			{
			case EdmPrimitiveTypeKind.Geography:
			case EdmPrimitiveTypeKind.GeographyPoint:
			case EdmPrimitiveTypeKind.GeographyLineString:
			case EdmPrimitiveTypeKind.GeographyPolygon:
			case EdmPrimitiveTypeKind.GeographyCollection:
			case EdmPrimitiveTypeKind.GeographyMultiPolygon:
			case EdmPrimitiveTypeKind.GeographyMultiLineString:
			case EdmPrimitiveTypeKind.GeographyMultiPoint:
			case EdmPrimitiveTypeKind.Geometry:
			case EdmPrimitiveTypeKind.GeometryPoint:
			case EdmPrimitiveTypeKind.GeometryLineString:
			case EdmPrimitiveTypeKind.GeometryPolygon:
			case EdmPrimitiveTypeKind.GeometryCollection:
			case EdmPrimitiveTypeKind.GeometryMultiPolygon:
			case EdmPrimitiveTypeKind.GeometryMultiLineString:
			case EdmPrimitiveTypeKind.GeometryMultiPoint:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0002211C File Offset: 0x0002031C
		public static IEdmPrimitiveTypeReference AsPrimitive(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference = type as IEdmPrimitiveTypeReference;
			if (edmPrimitiveTypeReference != null)
			{
				return edmPrimitiveTypeReference;
			}
			IEdmType definition = type.Definition;
			if (definition.TypeKind == EdmTypeKind.Primitive)
			{
				IEdmPrimitiveType edmPrimitiveType = definition as IEdmPrimitiveType;
				if (edmPrimitiveType != null)
				{
					switch (edmPrimitiveType.PrimitiveKind)
					{
					case EdmPrimitiveTypeKind.Binary:
						return type.AsBinary();
					case EdmPrimitiveTypeKind.Boolean:
					case EdmPrimitiveTypeKind.Byte:
					case EdmPrimitiveTypeKind.Double:
					case EdmPrimitiveTypeKind.Guid:
					case EdmPrimitiveTypeKind.Int16:
					case EdmPrimitiveTypeKind.Int32:
					case EdmPrimitiveTypeKind.Int64:
					case EdmPrimitiveTypeKind.SByte:
					case EdmPrimitiveTypeKind.Single:
					case EdmPrimitiveTypeKind.Stream:
						return new EdmPrimitiveTypeReference(edmPrimitiveType, type.IsNullable);
					case EdmPrimitiveTypeKind.DateTime:
					case EdmPrimitiveTypeKind.DateTimeOffset:
					case EdmPrimitiveTypeKind.Time:
						return type.AsTemporal();
					case EdmPrimitiveTypeKind.Decimal:
						return type.AsDecimal();
					case EdmPrimitiveTypeKind.String:
						return type.AsString();
					case EdmPrimitiveTypeKind.Geography:
					case EdmPrimitiveTypeKind.GeographyPoint:
					case EdmPrimitiveTypeKind.GeographyLineString:
					case EdmPrimitiveTypeKind.GeographyPolygon:
					case EdmPrimitiveTypeKind.GeographyCollection:
					case EdmPrimitiveTypeKind.GeographyMultiPolygon:
					case EdmPrimitiveTypeKind.GeographyMultiLineString:
					case EdmPrimitiveTypeKind.GeographyMultiPoint:
					case EdmPrimitiveTypeKind.Geometry:
					case EdmPrimitiveTypeKind.GeometryPoint:
					case EdmPrimitiveTypeKind.GeometryLineString:
					case EdmPrimitiveTypeKind.GeometryPolygon:
					case EdmPrimitiveTypeKind.GeometryCollection:
					case EdmPrimitiveTypeKind.GeometryMultiPolygon:
					case EdmPrimitiveTypeKind.GeometryMultiLineString:
					case EdmPrimitiveTypeKind.GeometryMultiPoint:
						return type.AsSpatial();
					}
				}
			}
			string text = type.FullName();
			List<EdmError> list = new List<EdmError>(type.Errors());
			if (list.Count == 0)
			{
				list.AddRange(EdmTypeSemantics.ConversionError(type.Location(), text, "Primitive"));
			}
			return new BadPrimitiveTypeReference(text, type.IsNullable, list);
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0002226C File Offset: 0x0002046C
		public static IEdmCollectionTypeReference AsCollection(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			IEdmCollectionTypeReference edmCollectionTypeReference = type as IEdmCollectionTypeReference;
			if (edmCollectionTypeReference != null)
			{
				return edmCollectionTypeReference;
			}
			IEdmType definition = type.Definition;
			if (definition.TypeKind == EdmTypeKind.Collection)
			{
				return new EdmCollectionTypeReference((IEdmCollectionType)definition, type.IsNullable);
			}
			List<EdmError> list = new List<EdmError>(type.Errors());
			if (list.Count == 0)
			{
				list.AddRange(EdmTypeSemantics.ConversionError(type.Location(), type.FullName(), "Collection"));
			}
			return new EdmCollectionTypeReference(new BadCollectionType(list), type.IsNullable);
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x000222F4 File Offset: 0x000204F4
		public static IEdmStructuredTypeReference AsStructured(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			IEdmStructuredTypeReference edmStructuredTypeReference = type as IEdmStructuredTypeReference;
			if (edmStructuredTypeReference != null)
			{
				return edmStructuredTypeReference;
			}
			switch (type.TypeKind())
			{
			case EdmTypeKind.Entity:
				return type.AsEntity();
			case EdmTypeKind.Complex:
				return type.AsComplex();
			case EdmTypeKind.Row:
				return type.AsRow();
			default:
			{
				string text = type.FullName();
				List<EdmError> list = new List<EdmError>(type.TypeErrors());
				if (list.Count == 0)
				{
					list.AddRange(EdmTypeSemantics.ConversionError(type.Location(), text, "Structured"));
				}
				return new BadEntityTypeReference(text, type.IsNullable, list);
			}
			}
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x0002238C File Offset: 0x0002058C
		public static IEdmEnumTypeReference AsEnum(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			IEdmEnumTypeReference edmEnumTypeReference = type as IEdmEnumTypeReference;
			if (edmEnumTypeReference != null)
			{
				return edmEnumTypeReference;
			}
			IEdmType definition = type.Definition;
			if (definition.TypeKind == EdmTypeKind.Enum)
			{
				return new EdmEnumTypeReference((IEdmEnumType)definition, type.IsNullable);
			}
			string text = type.FullName();
			return new EdmEnumTypeReference(new BadEnumType(text, EdmTypeSemantics.ConversionError(type.Location(), text, "Enum")), type.IsNullable);
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x000223FC File Offset: 0x000205FC
		public static IEdmEntityTypeReference AsEntity(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			IEdmEntityTypeReference edmEntityTypeReference = type as IEdmEntityTypeReference;
			if (edmEntityTypeReference != null)
			{
				return edmEntityTypeReference;
			}
			IEdmType definition = type.Definition;
			if (definition.TypeKind == EdmTypeKind.Entity)
			{
				return new EdmEntityTypeReference((IEdmEntityType)definition, type.IsNullable);
			}
			string text = type.FullName();
			List<EdmError> list = new List<EdmError>(type.Errors());
			if (list.Count == 0)
			{
				list.AddRange(EdmTypeSemantics.ConversionError(type.Location(), text, "Entity"));
			}
			return new BadEntityTypeReference(text, type.IsNullable, list);
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x00022484 File Offset: 0x00020684
		public static IEdmEntityReferenceTypeReference AsEntityReference(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			IEdmEntityReferenceTypeReference edmEntityReferenceTypeReference = type as IEdmEntityReferenceTypeReference;
			if (edmEntityReferenceTypeReference != null)
			{
				return edmEntityReferenceTypeReference;
			}
			IEdmType definition = type.Definition;
			if (definition.TypeKind == EdmTypeKind.EntityReference)
			{
				return new EdmEntityReferenceTypeReference((IEdmEntityReferenceType)definition, type.IsNullable);
			}
			List<EdmError> list = new List<EdmError>(type.Errors());
			if (list.Count == 0)
			{
				list.AddRange(EdmTypeSemantics.ConversionError(type.Location(), type.FullName(), "EntityReference"));
			}
			return new EdmEntityReferenceTypeReference(new BadEntityReferenceType(list), type.IsNullable);
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0002250C File Offset: 0x0002070C
		public static IEdmComplexTypeReference AsComplex(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			IEdmComplexTypeReference edmComplexTypeReference = type as IEdmComplexTypeReference;
			if (edmComplexTypeReference != null)
			{
				return edmComplexTypeReference;
			}
			IEdmType definition = type.Definition;
			if (definition.TypeKind == EdmTypeKind.Complex)
			{
				return new EdmComplexTypeReference((IEdmComplexType)definition, type.IsNullable);
			}
			string text = type.FullName();
			List<EdmError> list = new List<EdmError>(type.Errors());
			if (list.Count == 0)
			{
				list.AddRange(EdmTypeSemantics.ConversionError(type.Location(), text, "Complex"));
			}
			return new BadComplexTypeReference(text, type.IsNullable, list);
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x00022594 File Offset: 0x00020794
		public static IEdmRowTypeReference AsRow(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			IEdmRowTypeReference edmRowTypeReference = type as IEdmRowTypeReference;
			if (edmRowTypeReference != null)
			{
				return edmRowTypeReference;
			}
			IEdmType definition = type.Definition;
			if (definition.TypeKind == EdmTypeKind.Row)
			{
				return new EdmRowTypeReference((IEdmRowType)definition, type.IsNullable);
			}
			List<EdmError> list = new List<EdmError>(type.Errors());
			if (list.Count == 0)
			{
				list.AddRange(EdmTypeSemantics.ConversionError(type.Location(), type.FullName(), "Row"));
			}
			return new EdmRowTypeReference(new BadRowType(list), type.IsNullable);
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x0002261C File Offset: 0x0002081C
		public static IEdmSpatialTypeReference AsSpatial(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			IEdmSpatialTypeReference edmSpatialTypeReference = type as IEdmSpatialTypeReference;
			if (edmSpatialTypeReference != null)
			{
				return edmSpatialTypeReference;
			}
			string text = type.FullName();
			List<EdmError> list = new List<EdmError>(type.Errors());
			if (list.Count == 0)
			{
				list.AddRange(EdmTypeSemantics.ConversionError(type.Location(), text, "Spatial"));
			}
			return new BadSpatialTypeReference(text, type.IsNullable, list);
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x00022680 File Offset: 0x00020880
		public static IEdmTemporalTypeReference AsTemporal(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			IEdmTemporalTypeReference edmTemporalTypeReference = type as IEdmTemporalTypeReference;
			if (edmTemporalTypeReference != null)
			{
				return edmTemporalTypeReference;
			}
			string text = type.FullName();
			List<EdmError> list = new List<EdmError>(type.Errors());
			if (list.Count == 0)
			{
				list.AddRange(EdmTypeSemantics.ConversionError(type.Location(), text, "Temporal"));
			}
			return new BadTemporalTypeReference(text, type.IsNullable, list);
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x000226E4 File Offset: 0x000208E4
		public static IEdmDecimalTypeReference AsDecimal(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			IEdmDecimalTypeReference edmDecimalTypeReference = type as IEdmDecimalTypeReference;
			if (edmDecimalTypeReference != null)
			{
				return edmDecimalTypeReference;
			}
			string text = type.FullName();
			List<EdmError> list = new List<EdmError>(type.Errors());
			if (list.Count == 0)
			{
				list.AddRange(EdmTypeSemantics.ConversionError(type.Location(), text, "Decimal"));
			}
			return new BadDecimalTypeReference(text, type.IsNullable, list);
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x00022748 File Offset: 0x00020948
		public static IEdmStringTypeReference AsString(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			IEdmStringTypeReference edmStringTypeReference = type as IEdmStringTypeReference;
			if (edmStringTypeReference != null)
			{
				return edmStringTypeReference;
			}
			string text = type.FullName();
			List<EdmError> list = new List<EdmError>(type.Errors());
			if (list.Count == 0)
			{
				list.AddRange(EdmTypeSemantics.ConversionError(type.Location(), text, "String"));
			}
			return new BadStringTypeReference(text, type.IsNullable, list);
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x000227AC File Offset: 0x000209AC
		public static IEdmBinaryTypeReference AsBinary(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			IEdmBinaryTypeReference edmBinaryTypeReference = type as IEdmBinaryTypeReference;
			if (edmBinaryTypeReference != null)
			{
				return edmBinaryTypeReference;
			}
			string text = type.FullName();
			List<EdmError> list = new List<EdmError>(type.Errors());
			if (list.Count == 0)
			{
				list.AddRange(EdmTypeSemantics.ConversionError(type.Location(), text, "Binary"));
			}
			return new BadBinaryTypeReference(text, type.IsNullable, list);
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00022810 File Offset: 0x00020A10
		public static EdmPrimitiveTypeKind PrimitiveKind(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			IEdmType definition = type.Definition;
			if (definition.TypeKind != EdmTypeKind.Primitive)
			{
				return EdmPrimitiveTypeKind.None;
			}
			return ((IEdmPrimitiveType)definition).PrimitiveKind;
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x00022846 File Offset: 0x00020A46
		public static IEdmRowTypeReference ApplyType(this IEdmRowType rowType, bool isNullable)
		{
			EdmUtil.CheckArgumentNull<IEdmRowType>(rowType, "type");
			return new EdmRowTypeReference(rowType, isNullable);
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x0002285B File Offset: 0x00020A5B
		public static bool InheritsFrom(this IEdmStructuredType type, IEdmStructuredType potentialBaseType)
		{
			for (;;)
			{
				type = type.BaseType;
				if (type.IsEquivalentTo(potentialBaseType))
				{
					break;
				}
				if (type == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x00022874 File Offset: 0x00020A74
		public static bool IsOrInheritsFrom(this IEdmType thisType, IEdmType otherType)
		{
			if (thisType == null || otherType == null)
			{
				return false;
			}
			if (thisType.IsEquivalentTo(otherType))
			{
				return true;
			}
			EdmTypeKind typeKind = thisType.TypeKind;
			return typeKind == otherType.TypeKind && (typeKind == EdmTypeKind.Entity || typeKind == EdmTypeKind.Complex || typeKind == EdmTypeKind.Row) && ((IEdmStructuredType)thisType).InheritsFrom((IEdmStructuredType)otherType);
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x000228C4 File Offset: 0x00020AC4
		internal static IEdmPrimitiveTypeReference GetPrimitiveTypeReference(this IEdmPrimitiveType type, bool isNullable)
		{
			switch (type.PrimitiveKind)
			{
			case EdmPrimitiveTypeKind.Binary:
				return new EdmBinaryTypeReference(type, isNullable);
			case EdmPrimitiveTypeKind.Boolean:
			case EdmPrimitiveTypeKind.Byte:
			case EdmPrimitiveTypeKind.Double:
			case EdmPrimitiveTypeKind.Guid:
			case EdmPrimitiveTypeKind.Int16:
			case EdmPrimitiveTypeKind.Int32:
			case EdmPrimitiveTypeKind.Int64:
			case EdmPrimitiveTypeKind.SByte:
			case EdmPrimitiveTypeKind.Single:
			case EdmPrimitiveTypeKind.Stream:
				return new EdmPrimitiveTypeReference(type, isNullable);
			case EdmPrimitiveTypeKind.DateTime:
			case EdmPrimitiveTypeKind.DateTimeOffset:
			case EdmPrimitiveTypeKind.Time:
				return new EdmTemporalTypeReference(type, isNullable);
			case EdmPrimitiveTypeKind.Decimal:
				return new EdmDecimalTypeReference(type, isNullable);
			case EdmPrimitiveTypeKind.String:
				return new EdmStringTypeReference(type, isNullable);
			case EdmPrimitiveTypeKind.Geography:
			case EdmPrimitiveTypeKind.GeographyPoint:
			case EdmPrimitiveTypeKind.GeographyLineString:
			case EdmPrimitiveTypeKind.GeographyPolygon:
			case EdmPrimitiveTypeKind.GeographyCollection:
			case EdmPrimitiveTypeKind.GeographyMultiPolygon:
			case EdmPrimitiveTypeKind.GeographyMultiLineString:
			case EdmPrimitiveTypeKind.GeographyMultiPoint:
			case EdmPrimitiveTypeKind.Geometry:
			case EdmPrimitiveTypeKind.GeometryPoint:
			case EdmPrimitiveTypeKind.GeometryLineString:
			case EdmPrimitiveTypeKind.GeometryPolygon:
			case EdmPrimitiveTypeKind.GeometryCollection:
			case EdmPrimitiveTypeKind.GeometryMultiPolygon:
			case EdmPrimitiveTypeKind.GeometryMultiLineString:
			case EdmPrimitiveTypeKind.GeometryMultiPoint:
				return new EdmSpatialTypeReference(type, isNullable);
			default:
				throw new InvalidOperationException(Strings.EdmPrimitive_UnexpectedKind);
			}
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x0002299C File Offset: 0x00020B9C
		private static IEnumerable<EdmError> ConversionError(EdmLocation location, string typeName, string typeKindName)
		{
			return new EdmError[]
			{
				new EdmError(location, EdmErrorCode.TypeSemanticsCouldNotConvertTypeReference, Strings.TypeSemantics_CouldNotConvertTypeReference(typeName ?? "UnnamedType", typeKindName))
			};
		}
	}
}
