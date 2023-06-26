using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Metadata
{
	// Token: 0x020001FC RID: 508
	internal static class MetadataUtilsCommon
	{
		// Token: 0x06000F77 RID: 3959 RVA: 0x00037B4E File Offset: 0x00035D4E
		internal static bool IsODataPrimitiveTypeKind(this IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(typeReference.Definition, "typeReference.Definition");
			return typeReference.Definition.IsODataPrimitiveTypeKind();
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x00037B78 File Offset: 0x00035D78
		internal static bool IsODataPrimitiveTypeKind(this IEdmType type)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(type, "type");
			EdmTypeKind typeKind = type.TypeKind;
			return typeKind == EdmTypeKind.Primitive && !type.IsStream();
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x00037BA6 File Offset: 0x00035DA6
		internal static bool IsODataComplexTypeKind(this IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(typeReference.Definition, "typeReference.Definition");
			return typeReference.Definition.IsODataComplexTypeKind();
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x00037BCE File Offset: 0x00035DCE
		internal static bool IsODataComplexTypeKind(this IEdmType type)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(type, "type");
			return type.TypeKind == EdmTypeKind.Complex;
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x00037BE4 File Offset: 0x00035DE4
		internal static bool IsODataEntityTypeKind(this IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(typeReference.Definition, "typeReference.Definition");
			return typeReference.Definition.IsODataEntityTypeKind();
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x00037C0C File Offset: 0x00035E0C
		internal static bool IsODataEntityTypeKind(this IEdmType type)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(type, "type");
			return type.TypeKind == EdmTypeKind.Entity;
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x00037C24 File Offset: 0x00035E24
		internal static bool IsODataValueType(this IEdmTypeReference typeReference)
		{
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference = typeReference.AsPrimitiveOrNull();
			if (edmPrimitiveTypeReference == null)
			{
				return false;
			}
			switch (edmPrimitiveTypeReference.PrimitiveKind())
			{
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
			case EdmPrimitiveTypeKind.Time:
				return true;
			}
			return false;
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x00037C8D File Offset: 0x00035E8D
		internal static bool IsNonEntityCollectionType(this IEdmTypeReference typeReference)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(typeReference, "typeReference");
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(typeReference.Definition, "typeReference.Definition");
			return typeReference.Definition.IsNonEntityCollectionType();
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x00037CB8 File Offset: 0x00035EB8
		internal static bool IsNonEntityCollectionType(this IEdmType type)
		{
			IEdmCollectionType edmCollectionType = type as IEdmCollectionType;
			return edmCollectionType != null && (edmCollectionType.ElementType == null || edmCollectionType.ElementType.TypeKind() != EdmTypeKind.Entity);
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x00037CE8 File Offset: 0x00035EE8
		internal static IEdmPrimitiveTypeReference AsPrimitiveOrNull(this IEdmTypeReference typeReference)
		{
			if (typeReference == null)
			{
				return null;
			}
			if (typeReference.TypeKind() != EdmTypeKind.Primitive)
			{
				return null;
			}
			return typeReference.AsPrimitive();
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x00037D00 File Offset: 0x00035F00
		internal static IEdmEntityTypeReference AsEntityOrNull(this IEdmTypeReference typeReference)
		{
			if (typeReference == null)
			{
				return null;
			}
			if (typeReference.TypeKind() != EdmTypeKind.Entity)
			{
				return null;
			}
			return typeReference.AsEntity();
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x00037D18 File Offset: 0x00035F18
		internal static IEdmStructuredTypeReference AsStructuredOrNull(this IEdmTypeReference typeReference)
		{
			if (typeReference == null)
			{
				return null;
			}
			if (!typeReference.IsStructured())
			{
				return null;
			}
			return typeReference.AsStructured();
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x00037D30 File Offset: 0x00035F30
		internal static bool CanConvertPrimitiveTypeTo(IEdmPrimitiveType sourcePrimitiveType, IEdmPrimitiveType targetPrimitiveType)
		{
			EdmPrimitiveTypeKind primitiveKind = sourcePrimitiveType.PrimitiveKind;
			EdmPrimitiveTypeKind primitiveKind2 = targetPrimitiveType.PrimitiveKind;
			switch (primitiveKind)
			{
			case EdmPrimitiveTypeKind.Byte:
				switch (primitiveKind2)
				{
				case EdmPrimitiveTypeKind.Byte:
				case EdmPrimitiveTypeKind.Decimal:
				case EdmPrimitiveTypeKind.Double:
				case EdmPrimitiveTypeKind.Int16:
				case EdmPrimitiveTypeKind.Int32:
				case EdmPrimitiveTypeKind.Int64:
				case EdmPrimitiveTypeKind.Single:
					return true;
				case EdmPrimitiveTypeKind.DateTime:
				case EdmPrimitiveTypeKind.DateTimeOffset:
				case EdmPrimitiveTypeKind.Guid:
				case EdmPrimitiveTypeKind.SByte:
					return false;
				default:
					return false;
				}
				break;
			case EdmPrimitiveTypeKind.Int16:
				switch (primitiveKind2)
				{
				case EdmPrimitiveTypeKind.Decimal:
				case EdmPrimitiveTypeKind.Double:
				case EdmPrimitiveTypeKind.Int16:
				case EdmPrimitiveTypeKind.Int32:
				case EdmPrimitiveTypeKind.Int64:
				case EdmPrimitiveTypeKind.Single:
					return true;
				case EdmPrimitiveTypeKind.Guid:
				case EdmPrimitiveTypeKind.SByte:
					return false;
				default:
					return false;
				}
				break;
			case EdmPrimitiveTypeKind.Int32:
				switch (primitiveKind2)
				{
				case EdmPrimitiveTypeKind.Decimal:
				case EdmPrimitiveTypeKind.Double:
				case EdmPrimitiveTypeKind.Int32:
				case EdmPrimitiveTypeKind.Int64:
				case EdmPrimitiveTypeKind.Single:
					return true;
				case EdmPrimitiveTypeKind.Guid:
				case EdmPrimitiveTypeKind.Int16:
				case EdmPrimitiveTypeKind.SByte:
					return false;
				default:
					return false;
				}
				break;
			case EdmPrimitiveTypeKind.Int64:
			{
				EdmPrimitiveTypeKind edmPrimitiveTypeKind = primitiveKind2;
				switch (edmPrimitiveTypeKind)
				{
				case EdmPrimitiveTypeKind.Decimal:
				case EdmPrimitiveTypeKind.Double:
					break;
				default:
					switch (edmPrimitiveTypeKind)
					{
					case EdmPrimitiveTypeKind.Int64:
					case EdmPrimitiveTypeKind.Single:
						break;
					case EdmPrimitiveTypeKind.SByte:
						return false;
					default:
						return false;
					}
					break;
				}
				return true;
			}
			case EdmPrimitiveTypeKind.SByte:
				switch (primitiveKind2)
				{
				case EdmPrimitiveTypeKind.Decimal:
				case EdmPrimitiveTypeKind.Double:
				case EdmPrimitiveTypeKind.Int16:
				case EdmPrimitiveTypeKind.Int32:
				case EdmPrimitiveTypeKind.Int64:
				case EdmPrimitiveTypeKind.SByte:
				case EdmPrimitiveTypeKind.Single:
					return true;
				case EdmPrimitiveTypeKind.Guid:
					return false;
				default:
					return false;
				}
				break;
			case EdmPrimitiveTypeKind.Single:
			{
				EdmPrimitiveTypeKind edmPrimitiveTypeKind2 = primitiveKind2;
				if (edmPrimitiveTypeKind2 == EdmPrimitiveTypeKind.Double || edmPrimitiveTypeKind2 == EdmPrimitiveTypeKind.Single)
				{
					return true;
				}
				return false;
			}
			}
			return primitiveKind == primitiveKind2 || targetPrimitiveType.IsAssignableFrom(sourcePrimitiveType);
		}
	}
}
