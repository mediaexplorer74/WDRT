using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Spatial;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;

namespace Microsoft.Data.OData.Metadata
{
	// Token: 0x02000202 RID: 514
	internal static class EdmLibraryExtensions
	{
		// Token: 0x06000F96 RID: 3990 RVA: 0x00038168 File Offset: 0x00036368
		static EdmLibraryExtensions()
		{
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(bool), EdmLibraryExtensions.BooleanTypeReference);
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(byte), EdmLibraryExtensions.ByteTypeReference);
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(DateTime), EdmLibraryExtensions.DateTimeTypeReference);
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(decimal), EdmLibraryExtensions.DecimalTypeReference);
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(double), EdmLibraryExtensions.DoubleTypeReference);
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(short), EdmLibraryExtensions.Int16TypeReference);
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(int), EdmLibraryExtensions.Int32TypeReference);
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(long), EdmLibraryExtensions.Int64TypeReference);
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(sbyte), EdmLibraryExtensions.SByteTypeReference);
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(string), EdmLibraryExtensions.StringTypeReference);
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(float), EdmLibraryExtensions.SingleTypeReference);
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(DateTimeOffset), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.DateTimeOffset), false));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(Guid), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Guid), false));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(TimeSpan), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Time), false));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(byte[]), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Binary), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(Stream), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Stream), false));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(bool?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Boolean), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(byte?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Byte), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(DateTime?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.DateTime), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(DateTimeOffset?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.DateTimeOffset), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(decimal?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Decimal), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(double?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Double), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(short?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Int16), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(int?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Int32), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(long?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Int64), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(sbyte?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.SByte), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(float?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Single), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(Guid?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Guid), true));
			EdmLibraryExtensions.PrimitiveTypeReferenceMap.Add(typeof(TimeSpan?), EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Time), true));
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x00038631 File Offset: 0x00036831
		internal static bool IsUserModel(this IEdmModel model)
		{
			return !(model is EdmCoreModel);
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x00038640 File Offset: 0x00036840
		internal static bool IsPrimitiveType(Type clrType)
		{
			switch (PlatformHelper.GetTypeCode(clrType))
			{
			case TypeCode.Boolean:
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.Int32:
			case TypeCode.Int64:
			case TypeCode.Single:
			case TypeCode.Double:
			case TypeCode.Decimal:
			case TypeCode.DateTime:
			case TypeCode.String:
				return true;
			}
			return EdmLibraryExtensions.PrimitiveTypeReferenceMap.ContainsKey(clrType) || typeof(ISpatial).IsAssignableFrom(clrType);
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x000386C0 File Offset: 0x000368C0
		internal static IEnumerable<IEdmEntityType> EntityTypes(this IEdmModel model)
		{
			IEnumerable<IEdmSchemaElement> schemaElements = model.SchemaElements;
			if (schemaElements != null)
			{
				return schemaElements.OfType<IEdmEntityType>();
			}
			return null;
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x000386E0 File Offset: 0x000368E0
		internal static IEdmCollectionTypeReference ToCollectionTypeReference(this IEdmPrimitiveTypeReference itemTypeReference)
		{
			IEdmCollectionType edmCollectionType = new EdmCollectionType(itemTypeReference);
			return (IEdmCollectionTypeReference)edmCollectionType.ToTypeReference();
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x00038700 File Offset: 0x00036900
		internal static IEdmCollectionTypeReference ToCollectionTypeReference(this IEdmComplexTypeReference itemTypeReference)
		{
			IEdmCollectionType edmCollectionType = new EdmCollectionType(itemTypeReference);
			return (IEdmCollectionTypeReference)edmCollectionType.ToTypeReference();
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x0003871F File Offset: 0x0003691F
		internal static bool IsAssignableFrom(this IEdmTypeReference baseType, IEdmTypeReference subtype)
		{
			return baseType.Definition.IsAssignableFrom(subtype.Definition);
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x00038734 File Offset: 0x00036934
		internal static bool IsAssignableFrom(this IEdmType baseType, IEdmType subtype)
		{
			EdmTypeKind typeKind = baseType.TypeKind;
			EdmTypeKind typeKind2 = subtype.TypeKind;
			if (typeKind != typeKind2)
			{
				return false;
			}
			switch (typeKind)
			{
			case EdmTypeKind.Primitive:
				return ((IEdmPrimitiveType)baseType).IsAssignableFrom((IEdmPrimitiveType)subtype);
			case EdmTypeKind.Entity:
			case EdmTypeKind.Complex:
				return ((IEdmStructuredType)baseType).IsAssignableFrom((IEdmStructuredType)subtype);
			case EdmTypeKind.Collection:
				return ((IEdmCollectionType)baseType).ElementType.Definition.IsAssignableFrom(((IEdmCollectionType)subtype).ElementType.Definition);
			}
			throw new ODataException(Strings.General_InternalError(InternalErrorCodesCommon.EdmLibraryExtensions_IsAssignableFrom_Type));
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x000387D0 File Offset: 0x000369D0
		internal static IEdmStructuredType GetCommonBaseType(this IEdmStructuredType firstType, IEdmStructuredType secondType)
		{
			if (firstType.IsEquivalentTo(secondType))
			{
				return firstType;
			}
			for (IEdmStructuredType edmStructuredType = firstType; edmStructuredType != null; edmStructuredType = edmStructuredType.BaseType)
			{
				if (edmStructuredType.IsAssignableFrom(secondType))
				{
					return edmStructuredType;
				}
			}
			for (IEdmStructuredType edmStructuredType = secondType; edmStructuredType != null; edmStructuredType = edmStructuredType.BaseType)
			{
				if (edmStructuredType.IsAssignableFrom(firstType))
				{
					return edmStructuredType;
				}
			}
			return null;
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x0003881C File Offset: 0x00036A1C
		internal static IEdmPrimitiveType GetCommonBaseType(this IEdmPrimitiveType firstType, IEdmPrimitiveType secondType)
		{
			if (firstType.IsEquivalentTo(secondType))
			{
				return firstType;
			}
			for (IEdmPrimitiveType edmPrimitiveType = firstType; edmPrimitiveType != null; edmPrimitiveType = edmPrimitiveType.BaseType())
			{
				if (edmPrimitiveType.IsAssignableFrom(secondType))
				{
					return edmPrimitiveType;
				}
			}
			for (IEdmPrimitiveType edmPrimitiveType = secondType; edmPrimitiveType != null; edmPrimitiveType = edmPrimitiveType.BaseType())
			{
				if (edmPrimitiveType.IsAssignableFrom(firstType))
				{
					return edmPrimitiveType;
				}
			}
			return null;
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x00038868 File Offset: 0x00036A68
		internal static IEdmPrimitiveType BaseType(this IEdmPrimitiveType type)
		{
			switch (type.PrimitiveKind)
			{
			case EdmPrimitiveTypeKind.None:
			case EdmPrimitiveTypeKind.Binary:
			case EdmPrimitiveTypeKind.Boolean:
			case EdmPrimitiveTypeKind.Byte:
			case EdmPrimitiveTypeKind.DateTime:
			case EdmPrimitiveTypeKind.DateTimeOffset:
			case EdmPrimitiveTypeKind.Decimal:
			case EdmPrimitiveTypeKind.Double:
			case EdmPrimitiveTypeKind.Guid:
			case EdmPrimitiveTypeKind.Int16:
			case EdmPrimitiveTypeKind.Int32:
			case EdmPrimitiveTypeKind.Int64:
			case EdmPrimitiveTypeKind.SByte:
			case EdmPrimitiveTypeKind.Single:
			case EdmPrimitiveTypeKind.String:
			case EdmPrimitiveTypeKind.Stream:
			case EdmPrimitiveTypeKind.Time:
			case EdmPrimitiveTypeKind.Geography:
			case EdmPrimitiveTypeKind.Geometry:
				return null;
			case EdmPrimitiveTypeKind.GeographyPoint:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Geography);
			case EdmPrimitiveTypeKind.GeographyLineString:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Geography);
			case EdmPrimitiveTypeKind.GeographyPolygon:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Geography);
			case EdmPrimitiveTypeKind.GeographyCollection:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Geography);
			case EdmPrimitiveTypeKind.GeographyMultiPolygon:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeographyCollection);
			case EdmPrimitiveTypeKind.GeographyMultiLineString:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeographyCollection);
			case EdmPrimitiveTypeKind.GeographyMultiPoint:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeographyCollection);
			case EdmPrimitiveTypeKind.GeometryPoint:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Geometry);
			case EdmPrimitiveTypeKind.GeometryLineString:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Geometry);
			case EdmPrimitiveTypeKind.GeometryPolygon:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Geometry);
			case EdmPrimitiveTypeKind.GeometryCollection:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Geometry);
			case EdmPrimitiveTypeKind.GeometryMultiPolygon:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeometryCollection);
			case EdmPrimitiveTypeKind.GeometryMultiLineString:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeometryCollection);
			case EdmPrimitiveTypeKind.GeometryMultiPoint:
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeometryCollection);
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodesCommon.EdmLibraryExtensions_BaseType));
			}
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x000389D3 File Offset: 0x00036BD3
		internal static IEdmComplexTypeReference AsComplexOrNull(this IEdmTypeReference typeReference)
		{
			if (typeReference == null)
			{
				return null;
			}
			if (typeReference.TypeKind() != EdmTypeKind.Complex)
			{
				return null;
			}
			return typeReference.AsComplex();
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x000389EC File Offset: 0x00036BEC
		internal static IEdmCollectionTypeReference AsCollectionOrNull(this IEdmTypeReference typeReference)
		{
			if (typeReference == null)
			{
				return null;
			}
			if (typeReference.TypeKind() != EdmTypeKind.Collection)
			{
				return null;
			}
			IEdmCollectionTypeReference edmCollectionTypeReference = typeReference.AsCollection();
			if (!edmCollectionTypeReference.IsNonEntityCollectionType())
			{
				return null;
			}
			return edmCollectionTypeReference;
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x00038A1B File Offset: 0x00036C1B
		internal static IEdmSchemaType ResolvePrimitiveTypeName(string typeName)
		{
			return EdmCoreModel.Instance.FindDeclaredType(typeName);
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x00038A28 File Offset: 0x00036C28
		internal static IEdmTypeReference GetCollectionItemType(this IEdmTypeReference typeReference)
		{
			IEdmCollectionTypeReference edmCollectionTypeReference = typeReference.AsCollectionOrNull();
			if (edmCollectionTypeReference != null)
			{
				return edmCollectionTypeReference.ElementType();
			}
			return null;
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x00038A48 File Offset: 0x00036C48
		internal static IEdmCollectionType GetCollectionType(IEdmType itemType)
		{
			IEdmTypeReference edmTypeReference = itemType.ToTypeReference();
			return EdmLibraryExtensions.GetCollectionType(edmTypeReference);
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x00038A62 File Offset: 0x00036C62
		internal static IEdmCollectionType GetCollectionType(IEdmTypeReference itemTypeReference)
		{
			if (!itemTypeReference.IsODataPrimitiveTypeKind() && !itemTypeReference.IsODataComplexTypeKind())
			{
				throw new ODataException(Strings.EdmLibraryExtensions_CollectionItemCanBeOnlyPrimitiveOrComplex);
			}
			return new EdmCollectionType(itemTypeReference);
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x00038A88 File Offset: 0x00036C88
		internal static bool IsGeographyType(this IEdmTypeReference typeReference)
		{
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference = typeReference.AsPrimitiveOrNull();
			if (edmPrimitiveTypeReference == null)
			{
				return false;
			}
			switch (edmPrimitiveTypeReference.PrimitiveKind())
			{
			case EdmPrimitiveTypeKind.Geography:
			case EdmPrimitiveTypeKind.GeographyPoint:
			case EdmPrimitiveTypeKind.GeographyLineString:
			case EdmPrimitiveTypeKind.GeographyPolygon:
			case EdmPrimitiveTypeKind.GeographyCollection:
			case EdmPrimitiveTypeKind.GeographyMultiPolygon:
			case EdmPrimitiveTypeKind.GeographyMultiLineString:
			case EdmPrimitiveTypeKind.GeographyMultiPoint:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x00038AD8 File Offset: 0x00036CD8
		internal static bool IsGeometryType(this IEdmTypeReference typeReference)
		{
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference = typeReference.AsPrimitiveOrNull();
			if (edmPrimitiveTypeReference == null)
			{
				return false;
			}
			switch (edmPrimitiveTypeReference.PrimitiveKind())
			{
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

		// Token: 0x06000FA9 RID: 4009 RVA: 0x00038B26 File Offset: 0x00036D26
		internal static string GetCollectionItemTypeName(string typeName)
		{
			return EdmLibraryExtensions.GetCollectionItemTypeName(typeName, false);
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x00038B30 File Offset: 0x00036D30
		internal static string FunctionImportGroupName(this IEnumerable<IEdmFunctionImport> functionImportGroup)
		{
			return functionImportGroup.First<IEdmFunctionImport>().Name;
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x00038B4C File Offset: 0x00036D4C
		internal static string FunctionImportGroupFullName(this IEnumerable<IEdmFunctionImport> functionImportGroup)
		{
			return functionImportGroup.First<IEdmFunctionImport>().FullName();
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x00038B66 File Offset: 0x00036D66
		internal static string NameWithParameters(this IEdmFunctionImport functionImport)
		{
			return functionImport.Name + functionImport.ParameterTypesToString();
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x00038B79 File Offset: 0x00036D79
		internal static string FullNameWithParameters(this IEdmFunctionImport functionImport)
		{
			return functionImport.FullName() + functionImport.ParameterTypesToString();
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x00038B8C File Offset: 0x00036D8C
		internal static bool OperationsBoundToEntityTypeMustBeContainerQualified(this IEdmEntityType entityType)
		{
			return entityType.IsOpen;
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x00038B94 File Offset: 0x00036D94
		internal static string ODataFullName(this IEdmTypeReference typeReference)
		{
			return typeReference.Definition.ODataFullName();
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x00038BA4 File Offset: 0x00036DA4
		internal static string ODataFullName(this IEdmType type)
		{
			IEdmCollectionType edmCollectionType = type as IEdmCollectionType;
			if (edmCollectionType != null)
			{
				string text = edmCollectionType.ElementType.ODataFullName();
				if (text == null)
				{
					return null;
				}
				return EdmLibraryExtensions.GetCollectionTypeName(text);
			}
			else
			{
				IEdmSchemaElement edmSchemaElement = type as IEdmSchemaElement;
				if (edmSchemaElement == null)
				{
					return null;
				}
				return edmSchemaElement.FullName();
			}
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x00038BE8 File Offset: 0x00036DE8
		internal static IEdmTypeReference Clone(this IEdmTypeReference typeReference, bool nullable)
		{
			if (typeReference == null)
			{
				return null;
			}
			switch (typeReference.TypeKind())
			{
			case EdmTypeKind.Primitive:
			{
				EdmPrimitiveTypeKind edmPrimitiveTypeKind = typeReference.PrimitiveKind();
				IEdmPrimitiveType edmPrimitiveType = (IEdmPrimitiveType)typeReference.Definition;
				switch (edmPrimitiveTypeKind)
				{
				case EdmPrimitiveTypeKind.Binary:
				{
					IEdmBinaryTypeReference edmBinaryTypeReference = (IEdmBinaryTypeReference)typeReference;
					return new EdmBinaryTypeReference(edmPrimitiveType, nullable, edmBinaryTypeReference.IsUnbounded, edmBinaryTypeReference.MaxLength, edmBinaryTypeReference.IsFixedLength);
				}
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
					return new EdmPrimitiveTypeReference(edmPrimitiveType, nullable);
				case EdmPrimitiveTypeKind.DateTime:
				case EdmPrimitiveTypeKind.DateTimeOffset:
				case EdmPrimitiveTypeKind.Time:
				{
					IEdmTemporalTypeReference edmTemporalTypeReference = (IEdmTemporalTypeReference)typeReference;
					return new EdmTemporalTypeReference(edmPrimitiveType, nullable, edmTemporalTypeReference.Precision);
				}
				case EdmPrimitiveTypeKind.Decimal:
				{
					IEdmDecimalTypeReference edmDecimalTypeReference = (IEdmDecimalTypeReference)typeReference;
					return new EdmDecimalTypeReference(edmPrimitiveType, nullable, edmDecimalTypeReference.Precision, edmDecimalTypeReference.Scale);
				}
				case EdmPrimitiveTypeKind.String:
				{
					IEdmStringTypeReference edmStringTypeReference = (IEdmStringTypeReference)typeReference;
					return new EdmStringTypeReference(edmPrimitiveType, nullable, edmStringTypeReference.IsUnbounded, edmStringTypeReference.MaxLength, edmStringTypeReference.IsFixedLength, edmStringTypeReference.IsUnicode, edmStringTypeReference.Collation);
				}
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
				{
					IEdmSpatialTypeReference edmSpatialTypeReference = (IEdmSpatialTypeReference)typeReference;
					return new EdmSpatialTypeReference(edmPrimitiveType, nullable, edmSpatialTypeReference.SpatialReferenceIdentifier);
				}
				default:
					throw new ODataException(Strings.General_InternalError(InternalErrorCodesCommon.EdmLibraryExtensions_Clone_PrimitiveTypeKind));
				}
				break;
			}
			case EdmTypeKind.Entity:
				return new EdmEntityTypeReference((IEdmEntityType)typeReference.Definition, nullable);
			case EdmTypeKind.Complex:
				return new EdmComplexTypeReference((IEdmComplexType)typeReference.Definition, nullable);
			case EdmTypeKind.Row:
				return new EdmRowTypeReference((IEdmRowType)typeReference.Definition, nullable);
			case EdmTypeKind.Collection:
				return new EdmCollectionTypeReference((IEdmCollectionType)typeReference.Definition, nullable);
			case EdmTypeKind.EntityReference:
				return new EdmEntityReferenceTypeReference((IEdmEntityReferenceType)typeReference.Definition, nullable);
			case EdmTypeKind.Enum:
				return new EdmEnumTypeReference((IEdmEnumType)typeReference.Definition, nullable);
			}
			throw new ODataException(Strings.General_InternalError(InternalErrorCodesCommon.EdmLibraryExtensions_Clone_TypeKind));
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x00038E0C File Offset: 0x0003700C
		internal static EdmMultiplicity TargetMultiplicityTemporary(this IEdmNavigationProperty property)
		{
			IEdmTypeReference type = property.Type;
			if (type.IsCollection())
			{
				return EdmMultiplicity.Many;
			}
			if (!type.IsNullable)
			{
				return EdmMultiplicity.One;
			}
			return EdmMultiplicity.ZeroOrOne;
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x00038E38 File Offset: 0x00037038
		internal static bool IsAssignableFrom(this IEdmStructuredType baseType, IEdmStructuredType subtype)
		{
			if (baseType.TypeKind != subtype.TypeKind)
			{
				return false;
			}
			if (!baseType.IsODataEntityTypeKind() && !baseType.IsODataComplexTypeKind())
			{
				return false;
			}
			for (IEdmStructuredType edmStructuredType = subtype; edmStructuredType != null; edmStructuredType = edmStructuredType.BaseType)
			{
				if (edmStructuredType.IsEquivalentTo(baseType))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x00038E84 File Offset: 0x00037084
		internal static bool IsSpatialType(this IEdmPrimitiveType primitiveType)
		{
			switch (primitiveType.PrimitiveKind)
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
			}
			return false;
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x00038F28 File Offset: 0x00037128
		internal static bool IsAssignableFrom(this IEdmPrimitiveType baseType, IEdmPrimitiveType subtype)
		{
			if (baseType.IsEquivalentTo(subtype))
			{
				return true;
			}
			if (!baseType.IsSpatialType() || !subtype.IsSpatialType())
			{
				return false;
			}
			EdmPrimitiveTypeKind primitiveKind = baseType.PrimitiveKind;
			EdmPrimitiveTypeKind primitiveKind2 = subtype.PrimitiveKind;
			switch (primitiveKind)
			{
			case EdmPrimitiveTypeKind.Geography:
				return primitiveKind2 == EdmPrimitiveTypeKind.Geography || primitiveKind2 == EdmPrimitiveTypeKind.GeographyCollection || primitiveKind2 == EdmPrimitiveTypeKind.GeographyLineString || primitiveKind2 == EdmPrimitiveTypeKind.GeographyMultiLineString || primitiveKind2 == EdmPrimitiveTypeKind.GeographyMultiPoint || primitiveKind2 == EdmPrimitiveTypeKind.GeographyMultiPolygon || primitiveKind2 == EdmPrimitiveTypeKind.GeographyPoint || primitiveKind2 == EdmPrimitiveTypeKind.GeographyPolygon;
			case EdmPrimitiveTypeKind.GeographyPoint:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeographyPoint;
			case EdmPrimitiveTypeKind.GeographyLineString:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeographyLineString;
			case EdmPrimitiveTypeKind.GeographyPolygon:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeographyPolygon;
			case EdmPrimitiveTypeKind.GeographyCollection:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeographyCollection || primitiveKind2 == EdmPrimitiveTypeKind.GeographyMultiLineString || primitiveKind2 == EdmPrimitiveTypeKind.GeographyMultiPoint || primitiveKind2 == EdmPrimitiveTypeKind.GeographyMultiPolygon;
			case EdmPrimitiveTypeKind.GeographyMultiPolygon:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeographyMultiPolygon;
			case EdmPrimitiveTypeKind.GeographyMultiLineString:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeographyMultiLineString;
			case EdmPrimitiveTypeKind.GeographyMultiPoint:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeographyMultiPoint;
			case EdmPrimitiveTypeKind.Geometry:
				return primitiveKind2 == EdmPrimitiveTypeKind.Geometry || primitiveKind2 == EdmPrimitiveTypeKind.GeometryCollection || primitiveKind2 == EdmPrimitiveTypeKind.GeometryLineString || primitiveKind2 == EdmPrimitiveTypeKind.GeometryMultiLineString || primitiveKind2 == EdmPrimitiveTypeKind.GeometryMultiPoint || primitiveKind2 == EdmPrimitiveTypeKind.GeometryMultiPolygon || primitiveKind2 == EdmPrimitiveTypeKind.GeometryPoint || primitiveKind2 == EdmPrimitiveTypeKind.GeometryPolygon;
			case EdmPrimitiveTypeKind.GeometryPoint:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeometryPoint;
			case EdmPrimitiveTypeKind.GeometryLineString:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeometryLineString;
			case EdmPrimitiveTypeKind.GeometryPolygon:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeometryPolygon;
			case EdmPrimitiveTypeKind.GeometryCollection:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeometryCollection || primitiveKind2 == EdmPrimitiveTypeKind.GeometryMultiLineString || primitiveKind2 == EdmPrimitiveTypeKind.GeometryMultiPoint || primitiveKind2 == EdmPrimitiveTypeKind.GeometryMultiPolygon;
			case EdmPrimitiveTypeKind.GeometryMultiPolygon:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeometryMultiPolygon;
			case EdmPrimitiveTypeKind.GeometryMultiLineString:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeometryMultiLineString;
			case EdmPrimitiveTypeKind.GeometryMultiPoint:
				return primitiveKind2 == EdmPrimitiveTypeKind.GeometryMultiPoint;
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodesCommon.EdmLibraryExtensions_IsAssignableFrom_Primitive));
			}
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x0003908C File Offset: 0x0003728C
		internal static Type GetPrimitiveClrType(IEdmPrimitiveTypeReference primitiveTypeReference)
		{
			return EdmLibraryExtensions.GetPrimitiveClrType(primitiveTypeReference.PrimitiveDefinition(), primitiveTypeReference.IsNullable);
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x0003909F File Offset: 0x0003729F
		internal static IEdmTypeReference ToTypeReference(this IEdmType type)
		{
			return type.ToTypeReference(false);
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x000390A8 File Offset: 0x000372A8
		internal static bool IsOpenType(this IEdmType type)
		{
			IEdmStructuredType edmStructuredType = type as IEdmStructuredType;
			return edmStructuredType != null && edmStructuredType.IsOpen;
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x000390C8 File Offset: 0x000372C8
		internal static bool IsStream(this IEdmType type)
		{
			IEdmPrimitiveType edmPrimitiveType = type as IEdmPrimitiveType;
			return edmPrimitiveType != null && edmPrimitiveType.PrimitiveKind == EdmPrimitiveTypeKind.Stream;
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x00039114 File Offset: 0x00037314
		internal static bool ContainsProperty(this IEdmType type, IEdmProperty property)
		{
			IEdmComplexType edmComplexType = type as IEdmComplexType;
			if (edmComplexType != null)
			{
				return edmComplexType.Properties().Any((IEdmProperty p) => p == property);
			}
			IEdmEntityType edmEntityType = type as IEdmEntityType;
			if (edmEntityType == null)
			{
				return false;
			}
			if (!edmEntityType.Properties().Any((IEdmProperty p) => p == property))
			{
				return edmEntityType.NavigationProperties().Any((IEdmNavigationProperty p) => p == property);
			}
			return true;
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x000391AC File Offset: 0x000373AC
		internal static bool ContainsProperty(this IEdmTypeReference typeReference, IEdmProperty property)
		{
			IEdmStructuredTypeReference edmStructuredTypeReference = typeReference.AsStructuredOrNull();
			return edmStructuredTypeReference != null && edmStructuredTypeReference.Definition.ContainsProperty(property);
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x000391D1 File Offset: 0x000373D1
		internal static string FullName(this IEdmEntityContainerElement containerElement)
		{
			return containerElement.Container.Name + "." + containerElement.Name;
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x000391F0 File Offset: 0x000373F0
		internal static IEdmPrimitiveTypeReference GetPrimitiveTypeReference(Type clrType)
		{
			switch (PlatformHelper.GetTypeCode(clrType))
			{
			case TypeCode.Boolean:
				return EdmLibraryExtensions.BooleanTypeReference;
			case TypeCode.SByte:
				return EdmLibraryExtensions.SByteTypeReference;
			case TypeCode.Byte:
				return EdmLibraryExtensions.ByteTypeReference;
			case TypeCode.Int16:
				return EdmLibraryExtensions.Int16TypeReference;
			case TypeCode.Int32:
				return EdmLibraryExtensions.Int32TypeReference;
			case TypeCode.Int64:
				return EdmLibraryExtensions.Int64TypeReference;
			case TypeCode.Single:
				return EdmLibraryExtensions.SingleTypeReference;
			case TypeCode.Double:
				return EdmLibraryExtensions.DoubleTypeReference;
			case TypeCode.Decimal:
				return EdmLibraryExtensions.DecimalTypeReference;
			case TypeCode.DateTime:
				return EdmLibraryExtensions.DateTimeTypeReference;
			case TypeCode.String:
				return EdmLibraryExtensions.StringTypeReference;
			}
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference;
			if (EdmLibraryExtensions.PrimitiveTypeReferenceMap.TryGetValue(clrType, out edmPrimitiveTypeReference))
			{
				return edmPrimitiveTypeReference;
			}
			IEdmPrimitiveType edmPrimitiveType = null;
			if (typeof(GeographyPoint).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeographyPoint);
			}
			else if (typeof(GeographyLineString).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeographyLineString);
			}
			else if (typeof(GeographyPolygon).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeographyPolygon);
			}
			else if (typeof(GeographyMultiPoint).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeographyMultiPoint);
			}
			else if (typeof(GeographyMultiLineString).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeographyMultiLineString);
			}
			else if (typeof(GeographyMultiPolygon).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeographyMultiPolygon);
			}
			else if (typeof(GeographyCollection).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeographyCollection);
			}
			else if (typeof(Geography).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Geography);
			}
			else if (typeof(GeometryPoint).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeometryPoint);
			}
			else if (typeof(GeometryLineString).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeometryLineString);
			}
			else if (typeof(GeometryPolygon).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeometryPolygon);
			}
			else if (typeof(GeometryMultiPoint).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeometryMultiPoint);
			}
			else if (typeof(GeometryMultiLineString).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeometryMultiLineString);
			}
			else if (typeof(GeometryMultiPolygon).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeometryMultiPolygon);
			}
			else if (typeof(GeometryCollection).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.GeometryCollection);
			}
			else if (typeof(Geometry).IsAssignableFrom(clrType))
			{
				edmPrimitiveType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Geometry);
			}
			if (edmPrimitiveType == null)
			{
				return null;
			}
			return EdmLibraryExtensions.ToTypeReference(edmPrimitiveType, true);
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x000394E4 File Offset: 0x000376E4
		internal static IEdmTypeReference ToTypeReference(this IEdmType type, bool nullable)
		{
			if (type == null)
			{
				return null;
			}
			switch (type.TypeKind)
			{
			case EdmTypeKind.Primitive:
				return EdmLibraryExtensions.ToTypeReference((IEdmPrimitiveType)type, nullable);
			case EdmTypeKind.Entity:
				return new EdmEntityTypeReference((IEdmEntityType)type, nullable);
			case EdmTypeKind.Complex:
				return new EdmComplexTypeReference((IEdmComplexType)type, nullable);
			case EdmTypeKind.Row:
				return new EdmRowTypeReference((IEdmRowType)type, nullable);
			case EdmTypeKind.Collection:
				return new EdmCollectionTypeReference((IEdmCollectionType)type, nullable);
			case EdmTypeKind.EntityReference:
				return new EdmEntityReferenceTypeReference((IEdmEntityReferenceType)type, nullable);
			}
			throw new ODataException(Strings.General_InternalError(InternalErrorCodesCommon.EdmLibraryExtensions_ToTypeReference));
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x00039580 File Offset: 0x00037780
		internal static string GetCollectionTypeName(string itemTypeName)
		{
			return string.Format(CultureInfo.InvariantCulture, "Collection({0})", new object[] { itemTypeName });
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x000395D2 File Offset: 0x000377D2
		internal static IEdmEntitySet ResolveEntitySet(this IEdmModel model, string containerQualifiedEntitySetName)
		{
			return EdmLibraryExtensions.ResolveContainerQualifiedElementName(model, containerQualifiedEntitySetName, delegate(IEdmEntityContainer container, string entitySetName)
			{
				IEdmEntitySet edmEntitySet = container.FindEntitySet(entitySetName);
				if (edmEntitySet != null)
				{
					return new IEdmEntitySet[] { edmEntitySet };
				}
				return Enumerable.Empty<IEdmEntityContainerElement>();
			}).Cast<IEdmEntitySet>().SingleOrDefault<IEdmEntitySet>();
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x00039602 File Offset: 0x00037802
		internal static IEnumerable<IEdmFunctionImport> ResolveFunctionImports(this IEdmModel model, string containerQualifiedFunctionImportName)
		{
			return model.ResolveFunctionImports(containerQualifiedFunctionImportName, true);
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x00039628 File Offset: 0x00037828
		internal static IEnumerable<IEdmFunctionImport> ResolveFunctionImports(this IEdmModel model, string containerQualifiedFunctionImportName, bool allowParameterTypeNames)
		{
			return EdmLibraryExtensions.ResolveContainerQualifiedElementName(model, containerQualifiedFunctionImportName, (IEdmEntityContainer container, string functionImportName) => container.ResolveFunctionImports(functionImportName, allowParameterTypeNames).Cast<IEdmEntityContainerElement>()).Cast<IEdmFunctionImport>();
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x0003965A File Offset: 0x0003785A
		internal static IEnumerable<IEdmFunctionImport> ResolveFunctionImports(this IEdmEntityContainer container, string functionImportName)
		{
			return container.ResolveFunctionImports(functionImportName, true);
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x00039680 File Offset: 0x00037880
		internal static IEnumerable<IEdmFunctionImport> ResolveFunctionImports(this IEdmEntityContainer container, string functionImportName, bool allowParameterTypeNames)
		{
			if (string.IsNullOrEmpty(functionImportName))
			{
				return Enumerable.Empty<IEdmFunctionImport>();
			}
			int num = functionImportName.IndexOf('(');
			string text;
			if (num > 0)
			{
				if (!allowParameterTypeNames)
				{
					return Enumerable.Empty<IEdmFunctionImport>();
				}
				text = functionImportName.Substring(0, num);
			}
			else
			{
				text = functionImportName;
			}
			IEnumerable<IEdmFunctionImport> enumerable = container.FindFunctionImports(text);
			if (num > 0)
			{
				return enumerable.Where((IEdmFunctionImport f) => f.NameWithParameters().Equals(functionImportName, StringComparison.Ordinal));
			}
			return enumerable;
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x00039750 File Offset: 0x00037950
		internal static IEnumerable<IEdmFunctionImport> FindFunctionImportsByBindingParameterTypeHierarchy(this IEdmModel model, IEdmEntityType bindingType, string functionImportName)
		{
			return from f in model.ResolveFunctionImports(functionImportName, false)
				where f.IsBindable && f.Parameters.Any<IEdmFunctionParameter>() && f.Parameters.First<IEdmFunctionParameter>().Type.Definition.IsOrInheritsFrom(bindingType)
				select f;
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x000397E8 File Offset: 0x000379E8
		internal static IEnumerable<IEdmFunctionImport> FindFunctionImportsBySpecificBindingParameterType(this IEdmModel model, IEdmType bindingType, string functionImportName)
		{
			if (bindingType != null)
			{
				return from f in model.ResolveFunctionImports(functionImportName, false)
					where f.IsBindable && f.Parameters.Any<IEdmFunctionParameter>() && f.Parameters.First<IEdmFunctionParameter>().Type.Definition.ODataFullName() == bindingType.ODataFullName()
					select f;
			}
			return from f in model.ResolveFunctionImports(functionImportName, false)
				where !f.IsBindable
				select f;
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x00039858 File Offset: 0x00037A58
		internal static Type GetPrimitiveClrType(IEdmPrimitiveType primitiveType, bool isNullable)
		{
			switch (primitiveType.PrimitiveKind)
			{
			case EdmPrimitiveTypeKind.Binary:
				return typeof(byte[]);
			case EdmPrimitiveTypeKind.Boolean:
				if (!isNullable)
				{
					return typeof(bool);
				}
				return typeof(bool?);
			case EdmPrimitiveTypeKind.Byte:
				if (!isNullable)
				{
					return typeof(byte);
				}
				return typeof(byte?);
			case EdmPrimitiveTypeKind.DateTime:
				if (!isNullable)
				{
					return typeof(DateTime);
				}
				return typeof(DateTime?);
			case EdmPrimitiveTypeKind.DateTimeOffset:
				if (!isNullable)
				{
					return typeof(DateTimeOffset);
				}
				return typeof(DateTimeOffset?);
			case EdmPrimitiveTypeKind.Decimal:
				if (!isNullable)
				{
					return typeof(decimal);
				}
				return typeof(decimal?);
			case EdmPrimitiveTypeKind.Double:
				if (!isNullable)
				{
					return typeof(double);
				}
				return typeof(double?);
			case EdmPrimitiveTypeKind.Guid:
				if (!isNullable)
				{
					return typeof(Guid);
				}
				return typeof(Guid?);
			case EdmPrimitiveTypeKind.Int16:
				if (!isNullable)
				{
					return typeof(short);
				}
				return typeof(short?);
			case EdmPrimitiveTypeKind.Int32:
				if (!isNullable)
				{
					return typeof(int);
				}
				return typeof(int?);
			case EdmPrimitiveTypeKind.Int64:
				if (!isNullable)
				{
					return typeof(long);
				}
				return typeof(long?);
			case EdmPrimitiveTypeKind.SByte:
				if (!isNullable)
				{
					return typeof(sbyte);
				}
				return typeof(sbyte?);
			case EdmPrimitiveTypeKind.Single:
				if (!isNullable)
				{
					return typeof(float);
				}
				return typeof(float?);
			case EdmPrimitiveTypeKind.String:
				return typeof(string);
			case EdmPrimitiveTypeKind.Stream:
				return typeof(Stream);
			case EdmPrimitiveTypeKind.Time:
				if (!isNullable)
				{
					return typeof(TimeSpan);
				}
				return typeof(TimeSpan?);
			case EdmPrimitiveTypeKind.Geography:
				return typeof(Geography);
			case EdmPrimitiveTypeKind.GeographyPoint:
				return typeof(GeographyPoint);
			case EdmPrimitiveTypeKind.GeographyLineString:
				return typeof(GeographyLineString);
			case EdmPrimitiveTypeKind.GeographyPolygon:
				return typeof(GeographyPolygon);
			case EdmPrimitiveTypeKind.GeographyCollection:
				return typeof(GeographyCollection);
			case EdmPrimitiveTypeKind.GeographyMultiPolygon:
				return typeof(GeographyMultiPolygon);
			case EdmPrimitiveTypeKind.GeographyMultiLineString:
				return typeof(GeographyMultiLineString);
			case EdmPrimitiveTypeKind.GeographyMultiPoint:
				return typeof(GeographyMultiPoint);
			case EdmPrimitiveTypeKind.Geometry:
				return typeof(Geometry);
			case EdmPrimitiveTypeKind.GeometryPoint:
				return typeof(GeometryPoint);
			case EdmPrimitiveTypeKind.GeometryLineString:
				return typeof(GeometryLineString);
			case EdmPrimitiveTypeKind.GeometryPolygon:
				return typeof(GeometryPolygon);
			case EdmPrimitiveTypeKind.GeometryCollection:
				return typeof(GeometryCollection);
			case EdmPrimitiveTypeKind.GeometryMultiPolygon:
				return typeof(GeometryMultiPolygon);
			case EdmPrimitiveTypeKind.GeometryMultiLineString:
				return typeof(GeometryMultiLineString);
			case EdmPrimitiveTypeKind.GeometryMultiPoint:
				return typeof(GeometryMultiPoint);
			default:
				return null;
			}
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x00039B10 File Offset: 0x00037D10
		private static string GetCollectionItemTypeName(string typeName, bool isNested)
		{
			int length = "Collection".Length;
			if (typeName == null || !typeName.StartsWith("Collection(", StringComparison.Ordinal) || typeName[typeName.Length - 1] != ')' || typeName.Length == length + 2)
			{
				return null;
			}
			if (isNested)
			{
				throw new ODataException(Strings.ValidationUtils_NestedCollectionsAreNotSupported);
			}
			string text = typeName.Substring(length + 1, typeName.Length - (length + 2));
			EdmLibraryExtensions.GetCollectionItemTypeName(text, true);
			return text;
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x00039B90 File Offset: 0x00037D90
		private static string ParameterTypesToString(this IEdmFunctionImport functionImport)
		{
			return '(' + string.Join(",", functionImport.Parameters.Select((IEdmFunctionParameter p) => p.Type.ODataFullName()).ToArray<string>()) + ')';
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x00039BE8 File Offset: 0x00037DE8
		private static EdmPrimitiveTypeReference ToTypeReference(IEdmPrimitiveType primitiveType, bool nullable)
		{
			switch (primitiveType.PrimitiveKind)
			{
			case EdmPrimitiveTypeKind.Binary:
				return new EdmBinaryTypeReference(primitiveType, nullable);
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
				return new EdmPrimitiveTypeReference(primitiveType, nullable);
			case EdmPrimitiveTypeKind.DateTime:
			case EdmPrimitiveTypeKind.DateTimeOffset:
			case EdmPrimitiveTypeKind.Time:
				return new EdmTemporalTypeReference(primitiveType, nullable);
			case EdmPrimitiveTypeKind.Decimal:
				return new EdmDecimalTypeReference(primitiveType, nullable);
			case EdmPrimitiveTypeKind.String:
				return new EdmStringTypeReference(primitiveType, nullable);
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
				return new EdmSpatialTypeReference(primitiveType, nullable);
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodesCommon.EdmLibraryExtensions_PrimitiveTypeReference));
			}
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x00039CC8 File Offset: 0x00037EC8
		private static bool TryGetSingleOrDefaultEntityContainer(this IEdmModel model, out IEdmEntityContainer foundContainer)
		{
			IEdmEntityContainer[] array = model.EntityContainers().ToArray<IEdmEntityContainer>();
			foundContainer = null;
			if (array.Length > 1)
			{
				IEdmEntityContainer edmEntityContainer = array.SingleOrDefault(new Func<IEdmEntityContainer, bool>(model.IsDefaultEntityContainer));
				if (edmEntityContainer != null)
				{
					foundContainer = edmEntityContainer;
				}
			}
			else if (array.Length == 1)
			{
				foundContainer = array[0];
			}
			return foundContainer != null;
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x00039D18 File Offset: 0x00037F18
		private static IEnumerable<IEdmEntityContainerElement> ResolveContainerQualifiedElementName(IEdmModel model, string containerQualifiedElementName, Func<IEdmEntityContainer, string, IEnumerable<IEdmEntityContainerElement>> resolver)
		{
			if (string.IsNullOrEmpty(containerQualifiedElementName))
			{
				return Enumerable.Empty<IEdmEntityContainerElement>();
			}
			IEdmEntityContainer edmEntityContainer = null;
			string text;
			if (containerQualifiedElementName.IndexOf('.') < 0)
			{
				if (!model.TryGetSingleOrDefaultEntityContainer(out edmEntityContainer))
				{
					return Enumerable.Empty<IEdmEntityContainerElement>();
				}
				text = containerQualifiedElementName;
			}
			else
			{
				int num = containerQualifiedElementName.IndexOf('(');
				string text2 = ((num < 0) ? containerQualifiedElementName : containerQualifiedElementName.Substring(0, num));
				int num2 = text2.LastIndexOf('.');
				if (num2 < 0)
				{
					return Enumerable.Empty<IEdmEntityContainerElement>();
				}
				string text3 = containerQualifiedElementName.Substring(0, num2);
				text = containerQualifiedElementName.Substring(num2 + 1);
				if (string.IsNullOrEmpty(text3) || string.IsNullOrEmpty(text))
				{
					return Enumerable.Empty<IEdmEntityContainerElement>();
				}
				edmEntityContainer = model.FindEntityContainer(text3);
				if (edmEntityContainer == null)
				{
					return Enumerable.Empty<IEdmEntityContainerElement>();
				}
			}
			return resolver(edmEntityContainer, text);
		}

		// Token: 0x0400058A RID: 1418
		private const string CollectionTypeQualifier = "Collection";

		// Token: 0x0400058B RID: 1419
		private const string CollectionTypeFormat = "Collection({0})";

		// Token: 0x0400058C RID: 1420
		private static readonly Dictionary<Type, IEdmPrimitiveTypeReference> PrimitiveTypeReferenceMap = new Dictionary<Type, IEdmPrimitiveTypeReference>(EqualityComparer<Type>.Default);

		// Token: 0x0400058D RID: 1421
		private static readonly EdmPrimitiveTypeReference BooleanTypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Boolean), false);

		// Token: 0x0400058E RID: 1422
		private static readonly EdmPrimitiveTypeReference ByteTypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Byte), false);

		// Token: 0x0400058F RID: 1423
		private static readonly EdmPrimitiveTypeReference DateTimeTypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.DateTime), false);

		// Token: 0x04000590 RID: 1424
		private static readonly EdmPrimitiveTypeReference DecimalTypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Decimal), false);

		// Token: 0x04000591 RID: 1425
		private static readonly EdmPrimitiveTypeReference DoubleTypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Double), false);

		// Token: 0x04000592 RID: 1426
		private static readonly EdmPrimitiveTypeReference Int16TypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Int16), false);

		// Token: 0x04000593 RID: 1427
		private static readonly EdmPrimitiveTypeReference Int32TypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Int32), false);

		// Token: 0x04000594 RID: 1428
		private static readonly EdmPrimitiveTypeReference Int64TypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Int64), false);

		// Token: 0x04000595 RID: 1429
		private static readonly EdmPrimitiveTypeReference SByteTypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.SByte), false);

		// Token: 0x04000596 RID: 1430
		private static readonly EdmPrimitiveTypeReference StringTypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.String), true);

		// Token: 0x04000597 RID: 1431
		private static readonly EdmPrimitiveTypeReference SingleTypeReference = EdmLibraryExtensions.ToTypeReference(EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Single), false);
	}
}
