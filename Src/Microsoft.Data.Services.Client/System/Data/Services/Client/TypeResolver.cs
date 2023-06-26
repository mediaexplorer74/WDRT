using System;
using System.Collections.Generic;
using System.Data.Services.Client.Metadata;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.OData.Metadata;

namespace System.Data.Services.Client
{
	// Token: 0x02000125 RID: 293
	internal class TypeResolver
	{
		// Token: 0x060009CD RID: 2509 RVA: 0x00027E39 File Offset: 0x00026039
		internal TypeResolver(ClientEdmModel model, Func<string, Type> resolveTypeFromName, Func<Type, string> resolveNameFromType, IEdmModel serviceModel)
		{
			this.resolveTypeFromName = resolveTypeFromName;
			this.resolveNameFromType = resolveNameFromType;
			this.clientEdmModel = model;
			this.serviceModel = serviceModel;
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060009CE RID: 2510 RVA: 0x00027E6E File Offset: 0x0002606E
		internal IEdmModel ReaderModel
		{
			get
			{
				if (this.serviceModel != null)
				{
					return this.serviceModel;
				}
				return this.clientEdmModel;
			}
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00027E85 File Offset: 0x00026085
		internal void IsProjectionRequest()
		{
			this.skipTypeAssignabilityCheck = true;
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x00027E90 File Offset: 0x00026090
		internal ClientTypeAnnotation ResolveTypeForMaterialization(Type expectedType, string readerTypeName)
		{
			string collectionItemWireTypeName = WebUtil.GetCollectionItemWireTypeName(readerTypeName);
			if (collectionItemWireTypeName != null)
			{
				Type implementationType = ClientTypeUtil.GetImplementationType(expectedType, typeof(ICollection<>));
				Type type = implementationType.GetGenericArguments()[0];
				if (!PrimitiveType.IsKnownType(type))
				{
					type = this.ResolveTypeForMaterialization(type, collectionItemWireTypeName).ElementType;
				}
				Type backingTypeForCollectionProperty = WebUtil.GetBackingTypeForCollectionProperty(expectedType, type);
				return this.clientEdmModel.GetClientTypeAnnotation(backingTypeForCollectionProperty);
			}
			PrimitiveType primitiveType;
			if (PrimitiveType.TryGetPrimitiveType(readerTypeName, out primitiveType))
			{
				return this.clientEdmModel.GetClientTypeAnnotation(primitiveType.ClrType);
			}
			ClientTypeAnnotation clientTypeAnnotation;
			if (this.edmTypeNameMap.TryGetValue(readerTypeName, out clientTypeAnnotation))
			{
				return clientTypeAnnotation;
			}
			if (this.serviceModel != null)
			{
				Type type2 = this.ResolveTypeFromName(readerTypeName, expectedType);
				return this.clientEdmModel.GetClientTypeAnnotation(type2);
			}
			return this.clientEdmModel.GetClientTypeAnnotation(readerTypeName);
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x00027F50 File Offset: 0x00026150
		internal IEdmType ResolveWireTypeName(IEdmType expectedEdmType, string wireName)
		{
			if (expectedEdmType != null && expectedEdmType.TypeKind == EdmTypeKind.Primitive)
			{
				return expectedEdmType;
			}
			Type type;
			if (expectedEdmType != null)
			{
				ClientTypeAnnotation clientTypeAnnotation = this.clientEdmModel.GetClientTypeAnnotation(expectedEdmType);
				type = clientTypeAnnotation.ElementType;
			}
			else
			{
				type = typeof(object);
			}
			Type type2 = this.ResolveTypeFromName(wireName, type);
			ClientTypeAnnotation clientTypeAnnotation2 = this.clientEdmModel.GetClientTypeAnnotation(this.clientEdmModel.GetOrCreateEdmType(type2));
			if (clientTypeAnnotation2.IsEntityType)
			{
				clientTypeAnnotation2.EnsureEPMLoaded();
			}
			IEdmType edmType = clientTypeAnnotation2.EdmType;
			EdmTypeKind typeKind = edmType.TypeKind;
			if (typeKind == EdmTypeKind.Entity || typeKind == EdmTypeKind.Complex)
			{
				string text = edmType.FullName();
				if (!this.edmTypeNameMap.ContainsKey(text))
				{
					this.edmTypeNameMap.Add(text, clientTypeAnnotation2);
				}
			}
			return edmType;
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x00028004 File Offset: 0x00026204
		internal IEdmType ResolveExpectedTypeForReading(Type clientClrType)
		{
			ClientTypeAnnotation clientTypeAnnotation = this.clientEdmModel.GetClientTypeAnnotation(clientClrType);
			clientTypeAnnotation.EnsureEPMLoaded();
			IEdmType edmType = clientTypeAnnotation.EdmType;
			if (this.serviceModel == null)
			{
				return edmType;
			}
			if (edmType.TypeKind == EdmTypeKind.Primitive)
			{
				return edmType;
			}
			if (edmType.TypeKind == EdmTypeKind.Collection)
			{
				IEdmTypeReference elementType = ((IEdmCollectionType)edmType).ElementType;
				if (elementType.IsPrimitive())
				{
					return edmType;
				}
				Type type = clientClrType.GetGenericArguments()[0];
				IEdmType edmType2 = this.ResolveExpectedTypeForReading(type);
				if (edmType2 == null)
				{
					return null;
				}
				return new EdmCollectionType(edmType2.ToEdmTypeReference(elementType.IsNullable));
			}
			else
			{
				IEdmStructuredType edmStructuredType;
				if (!this.TryToResolveServerType(clientTypeAnnotation, out edmStructuredType))
				{
					return null;
				}
				return edmStructuredType;
			}
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0002809C File Offset: 0x0002629C
		internal bool ShouldWriteClientTypeForOpenServerProperty(IEdmProperty clientProperty, string serverTypeName)
		{
			if (serverTypeName == null)
			{
				return false;
			}
			if (this.serviceModel == null)
			{
				return false;
			}
			if (clientProperty.DeclaringType.TypeKind != EdmTypeKind.Entity)
			{
				return false;
			}
			IEdmStructuredType edmStructuredType = this.serviceModel.FindType(serverTypeName) as IEdmStructuredType;
			return edmStructuredType != null && edmStructuredType.FindProperty(clientProperty.Name) == null;
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x000280F0 File Offset: 0x000262F0
		internal bool TryResolveEntitySetBaseTypeName(string entitySetName, out string serverTypeName)
		{
			serverTypeName = null;
			if (this.serviceModel == null)
			{
				return false;
			}
			IEdmEntitySet edmEntitySet = this.serviceModel.ResolveEntitySet(entitySetName);
			if (edmEntitySet != null)
			{
				serverTypeName = edmEntitySet.ElementType.FullName();
				return true;
			}
			return false;
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0002812C File Offset: 0x0002632C
		internal bool TryResolveNavigationTargetTypeName(string serverSourceTypeName, string navigationPropertyName, out string serverTypeName)
		{
			serverTypeName = null;
			if (this.serviceModel == null || serverSourceTypeName == null)
			{
				return false;
			}
			IEdmEntityType edmEntityType = this.serviceModel.FindType(serverSourceTypeName) as IEdmEntityType;
			if (edmEntityType == null)
			{
				return false;
			}
			IEdmNavigationProperty edmNavigationProperty = edmEntityType.FindProperty(navigationPropertyName) as IEdmNavigationProperty;
			if (edmNavigationProperty == null)
			{
				return false;
			}
			IEdmTypeReference edmTypeReference = edmNavigationProperty.Type;
			if (edmTypeReference.IsCollection())
			{
				edmTypeReference = edmTypeReference.AsCollection().ElementType();
			}
			serverTypeName = edmTypeReference.FullName();
			return true;
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x00028198 File Offset: 0x00026398
		private bool TryToResolveServerType(ClientTypeAnnotation clientTypeAnnotation, out IEdmStructuredType serverType)
		{
			string text = this.resolveNameFromType(clientTypeAnnotation.ElementType);
			if (text == null)
			{
				serverType = null;
				return false;
			}
			serverType = this.serviceModel.FindType(text) as IEdmStructuredType;
			return serverType != null;
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x000281DC File Offset: 0x000263DC
		private Type ResolveTypeFromName(string wireName, Type expectedType)
		{
			Type type;
			if (!ClientConvert.ToNamedType(wireName, out type))
			{
				type = this.resolveTypeFromName(wireName);
				if (type == null)
				{
					type = ClientTypeCache.ResolveFromName(wireName, expectedType);
				}
				if (!this.skipTypeAssignabilityCheck && type != null && !expectedType.IsAssignableFrom(type))
				{
					throw Error.InvalidOperation(Strings.Deserialize_Current(expectedType, type));
				}
			}
			return type ?? expectedType;
		}

		// Token: 0x040005A0 RID: 1440
		private readonly IDictionary<string, ClientTypeAnnotation> edmTypeNameMap = new Dictionary<string, ClientTypeAnnotation>(StringComparer.Ordinal);

		// Token: 0x040005A1 RID: 1441
		private readonly Func<string, Type> resolveTypeFromName;

		// Token: 0x040005A2 RID: 1442
		private readonly Func<Type, string> resolveNameFromType;

		// Token: 0x040005A3 RID: 1443
		private readonly ClientEdmModel clientEdmModel;

		// Token: 0x040005A4 RID: 1444
		private readonly IEdmModel serviceModel;

		// Token: 0x040005A5 RID: 1445
		private bool skipTypeAssignabilityCheck;
	}
}
