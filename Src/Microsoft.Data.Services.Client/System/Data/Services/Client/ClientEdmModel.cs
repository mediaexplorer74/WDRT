using System;
using System.Collections.Generic;
using System.Data.Services.Client.Metadata;
using System.Data.Services.Client.Providers;
using System.Data.Services.Common;
using System.Linq;
using System.Reflection;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Annotations;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.Edm.Library.Annotations;

namespace System.Data.Services.Client
{
	// Token: 0x02000030 RID: 48
	internal sealed class ClientEdmModel : EdmElement, IEdmModel, IEdmElement
	{
		// Token: 0x06000165 RID: 357 RVA: 0x00008190 File Offset: 0x00006390
		internal ClientEdmModel(DataServiceProtocolVersion maxProtocolVersion)
		{
			this.maxProtocolVersion = maxProtocolVersion;
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000166 RID: 358 RVA: 0x000081EB File Offset: 0x000063EB
		public IEnumerable<IEdmVocabularyAnnotation> VocabularyAnnotations
		{
			get
			{
				return Enumerable.Empty<IEdmVocabularyAnnotation>();
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000167 RID: 359 RVA: 0x000081F2 File Offset: 0x000063F2
		public IEnumerable<IEdmModel> ReferencedModels
		{
			get
			{
				return this.coreModel;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000168 RID: 360 RVA: 0x000081FA File Offset: 0x000063FA
		public IEnumerable<IEdmSchemaElement> SchemaElements
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00008201 File Offset: 0x00006401
		public IEdmDirectValueAnnotationsManager DirectValueAnnotationsManager
		{
			get
			{
				return this.directValueAnnotationsManager;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00008209 File Offset: 0x00006409
		internal DataServiceProtocolVersion MaxProtocolVersion
		{
			get
			{
				return this.maxProtocolVersion;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00008211 File Offset: 0x00006411
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00008219 File Offset: 0x00006419
		internal Func<PropertyInfo, FieldInfo> ResolveBackingField { get; set; }

		// Token: 0x0600016D RID: 365 RVA: 0x00008222 File Offset: 0x00006422
		public IEdmEntityContainer FindDeclaredEntityContainer(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000822C File Offset: 0x0000642C
		public IEdmSchemaType FindDeclaredType(string qualifiedName)
		{
			ClientTypeAnnotation clientTypeAnnotation = null;
			if (this.typeNameToClientTypeAnnotationCache.TryGetValue(qualifiedName, out clientTypeAnnotation))
			{
				return (IEdmSchemaType)clientTypeAnnotation.EdmType;
			}
			return null;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00008258 File Offset: 0x00006458
		public IEnumerable<IEdmFunction> FindDeclaredFunctions(string qualifiedName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000825F File Offset: 0x0000645F
		public IEdmValueTerm FindDeclaredValueTerm(string qualifiedName)
		{
			return null;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00008262 File Offset: 0x00006462
		public IEnumerable<IEdmStructuredType> FindDirectlyDerivedTypes(IEdmStructuredType type)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00008269 File Offset: 0x00006469
		public IEnumerable<IEdmVocabularyAnnotation> FindDeclaredVocabularyAnnotations(IEdmVocabularyAnnotatable element)
		{
			return Enumerable.Empty<IEdmVocabularyAnnotation>();
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00008270 File Offset: 0x00006470
		internal IEdmType GetOrCreateEdmType(Type type)
		{
			ClientEdmModel.EdmTypeCacheValue orCreateEdmTypeInternal = this.GetOrCreateEdmTypeInternal(type);
			IEdmType edmType = orCreateEdmTypeInternal.EdmType;
			if (edmType.TypeKind == EdmTypeKind.Complex || edmType.TypeKind == EdmTypeKind.Entity)
			{
				bool? hasProperties = orCreateEdmTypeInternal.HasProperties;
				if (hasProperties == null)
				{
					hasProperties = new bool?(ClientTypeUtil.GetPropertiesOnType(type, false).Any<PropertyInfo>());
					lock (this.clrToEdmTypeCache)
					{
						ClientEdmModel.EdmTypeCacheValue edmTypeCacheValue = this.clrToEdmTypeCache[type];
						edmTypeCacheValue.HasProperties = hasProperties;
					}
				}
				if (hasProperties == false)
				{
					throw Error.InvalidOperation(Strings.ClientType_NoSettableFields(type.ToString()));
				}
			}
			return edmType;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00008354 File Offset: 0x00006554
		internal ClientTypeAnnotation GetClientTypeAnnotation(string edmTypeName)
		{
			IEdmType edmType = this.clrToEdmTypeCache.Values.First((ClientEdmModel.EdmTypeCacheValue e) => e.EdmType.FullName() == edmTypeName).EdmType;
			return this.GetClientTypeAnnotation(edmType);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00008398 File Offset: 0x00006598
		private static Type[] GetTypeHierarchy(Type type, out PropertyInfo[] keyProperties, out bool hasProperties)
		{
			keyProperties = ClientTypeUtil.GetKeyPropertiesOnType(type, out hasProperties);
			List<Type> list = new List<Type>();
			if (keyProperties != null)
			{
				Type type2;
				if (keyProperties.Length > 0)
				{
					type2 = keyProperties[0].DeclaringType;
				}
				else
				{
					type2 = type;
					while (!type2.GetCustomAttributes(false).OfType<DataServiceEntityAttribute>().Any<DataServiceEntityAttribute>() && type2.GetBaseType() != null)
					{
						type2 = type2.GetBaseType();
					}
				}
				do
				{
					list.Insert(0, type);
					if (!(type != type2))
					{
						break;
					}
				}
				while ((type = type.GetBaseType()) != null);
			}
			else
			{
				do
				{
					list.Insert(0, type);
				}
				while ((type = type.GetBaseType()) != null && ClientTypeUtil.GetPropertiesOnType(type, false).Any<PropertyInfo>());
			}
			return list.ToArray();
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00008480 File Offset: 0x00006680
		private void SetMimeTypeForProperties(IEdmStructuredType edmStructuredType)
		{
			MimeTypePropertyAttribute attribute = (MimeTypePropertyAttribute)this.GetClientTypeAnnotation(edmStructuredType).ElementType.GetCustomAttributes(typeof(MimeTypePropertyAttribute), true).SingleOrDefault<object>();
			if (attribute != null)
			{
				IEdmProperty edmProperty = edmStructuredType.Properties().SingleOrDefault((IEdmProperty p) => p.Name == attribute.DataPropertyName);
				if (edmProperty == null)
				{
					throw Error.InvalidOperation(Strings.ClientType_MissingMimeTypeDataProperty(this.GetClientTypeAnnotation(edmStructuredType).ElementTypeName, attribute.DataPropertyName));
				}
				IEdmProperty edmProperty2 = edmStructuredType.Properties().SingleOrDefault((IEdmProperty p) => p.Name == attribute.MimeTypePropertyName);
				if (edmProperty2 == null)
				{
					throw Error.InvalidOperation(Strings.ClientType_MissingMimeTypeProperty(this.GetClientTypeAnnotation(edmStructuredType).ElementTypeName, attribute.MimeTypePropertyName));
				}
				this.GetClientPropertyAnnotation(edmProperty).MimeTypeProperty = this.GetClientPropertyAnnotation(edmProperty2);
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000856C File Offset: 0x0000676C
		private ClientEdmModel.EdmTypeCacheValue GetOrCreateEdmTypeInternal(Type type)
		{
			ClientEdmModel.EdmTypeCacheValue edmTypeCacheValue;
			lock (this.clrToEdmTypeCache)
			{
				this.clrToEdmTypeCache.TryGetValue(type, out edmTypeCacheValue);
			}
			if (edmTypeCacheValue == null)
			{
				if (PrimitiveType.IsKnownNullableType(type))
				{
					edmTypeCacheValue = this.GetOrCreateEdmTypeInternal(null, type, ClientTypeUtil.EmptyPropertyInfoArray, false, new bool?(false));
				}
				else
				{
					PropertyInfo[] array;
					bool flag2;
					Type[] typeHierarchy = ClientEdmModel.GetTypeHierarchy(type, out array, out flag2);
					bool flag3 = array != null;
					array = array ?? ClientTypeUtil.EmptyPropertyInfoArray;
					foreach (Type type2 in typeHierarchy)
					{
						IEdmStructuredType edmStructuredType = ((edmTypeCacheValue == null) ? null : (edmTypeCacheValue.EdmType as IEdmStructuredType));
						edmTypeCacheValue = this.GetOrCreateEdmTypeInternal(edmStructuredType, type2, array, flag3, (type2 == type) ? new bool?(flag2) : null);
						array = ClientTypeUtil.EmptyPropertyInfoArray;
					}
				}
			}
			return edmTypeCacheValue;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000088C4 File Offset: 0x00006AC4
		private ClientEdmModel.EdmTypeCacheValue GetOrCreateEdmTypeInternal(IEdmStructuredType edmBaseType, Type type, PropertyInfo[] keyProperties, bool isEntity, bool? hasProperties)
		{
			ClientEdmModel.EdmTypeCacheValue edmTypeCacheValue;
			lock (this.clrToEdmTypeCache)
			{
				this.clrToEdmTypeCache.TryGetValue(type, out edmTypeCacheValue);
			}
			if (edmTypeCacheValue == null)
			{
				Type implementationType;
				if (PrimitiveType.IsKnownNullableType(type))
				{
					PrimitiveType primitiveType;
					PrimitiveType.TryGetPrimitiveType(type, out primitiveType);
					edmTypeCacheValue = new ClientEdmModel.EdmTypeCacheValue(primitiveType.CreateEdmPrimitiveType(), hasProperties);
				}
				else if ((implementationType = ClientTypeUtil.GetImplementationType(type, typeof(ICollection<>))) != null && ClientTypeUtil.GetImplementationType(type, typeof(IDictionary<, >)) == null)
				{
					Type type2 = implementationType.GetGenericArguments()[0];
					IEdmType edmType = this.GetOrCreateEdmTypeInternal(type2).EdmType;
					edmTypeCacheValue = new ClientEdmModel.EdmTypeCacheValue(new EdmCollectionType(edmType.ToEdmTypeReference(ClientTypeUtil.CanAssignNull(type2))), hasProperties);
				}
				else if (isEntity)
				{
					Action<EdmEntityTypeWithDelayLoadedProperties> action = delegate(EdmEntityTypeWithDelayLoadedProperties entityType)
					{
						List<IEdmProperty> list = new List<IEdmProperty>();
						List<IEdmStructuralProperty> list2 = new List<IEdmStructuralProperty>();
						using (IEnumerator<PropertyInfo> enumerator = (from p in ClientTypeUtil.GetPropertiesOnType(type, edmBaseType != null)
							orderby p.Name
							select p).GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								PropertyInfo property = enumerator.Current;
								IEdmProperty edmProperty = this.CreateEdmProperty(entityType, property);
								list.Add(edmProperty);
								if (edmBaseType == null)
								{
									if (keyProperties.Any((PropertyInfo k) => k.DeclaringType == type && k.Name == property.Name))
									{
										list2.Add((IEdmStructuralProperty)edmProperty);
									}
								}
							}
						}
						foreach (IEdmProperty edmProperty2 in list)
						{
							entityType.AddProperty(edmProperty2);
						}
						entityType.AddKeys(list2);
					};
					edmTypeCacheValue = new ClientEdmModel.EdmTypeCacheValue(new EdmEntityTypeWithDelayLoadedProperties(CommonUtil.GetModelTypeNamespace(type), CommonUtil.GetModelTypeName(type), (IEdmEntityType)edmBaseType, type.IsAbstract(), false, action), hasProperties);
				}
				else
				{
					Action<EdmComplexTypeWithDelayLoadedProperties> action2 = delegate(EdmComplexTypeWithDelayLoadedProperties complexType)
					{
						List<IEdmProperty> list3 = new List<IEdmProperty>();
						foreach (PropertyInfo propertyInfo in from p in ClientTypeUtil.GetPropertiesOnType(type, edmBaseType != null)
							orderby p.Name
							select p)
						{
							IEdmProperty edmProperty3 = this.CreateEdmProperty(complexType, propertyInfo);
							list3.Add(edmProperty3);
						}
						foreach (IEdmProperty edmProperty4 in list3)
						{
							complexType.AddProperty(edmProperty4);
						}
					};
					edmTypeCacheValue = new ClientEdmModel.EdmTypeCacheValue(new EdmComplexTypeWithDelayLoadedProperties(CommonUtil.GetModelTypeNamespace(type), CommonUtil.GetModelTypeName(type), (IEdmComplexType)edmBaseType, type.IsAbstract(), action2), hasProperties);
				}
				IEdmType edmType2 = edmTypeCacheValue.EdmType;
				ClientTypeAnnotation orCreateClientTypeAnnotation = this.GetOrCreateClientTypeAnnotation(edmType2, type);
				this.SetClientTypeAnnotation(edmType2, orCreateClientTypeAnnotation);
				if (edmType2.TypeKind == EdmTypeKind.Entity || edmType2.TypeKind == EdmTypeKind.Complex)
				{
					IEdmStructuredType edmStructuredType = edmType2 as IEdmStructuredType;
					this.SetMimeTypeForProperties(edmStructuredType);
				}
				lock (this.clrToEdmTypeCache)
				{
					ClientEdmModel.EdmTypeCacheValue edmTypeCacheValue2;
					if (this.clrToEdmTypeCache.TryGetValue(type, out edmTypeCacheValue2))
					{
						edmTypeCacheValue = edmTypeCacheValue2;
					}
					else
					{
						this.clrToEdmTypeCache.Add(type, edmTypeCacheValue);
					}
				}
			}
			return edmTypeCacheValue;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00008B48 File Offset: 0x00006D48
		private IEdmProperty CreateEdmProperty(IEdmStructuredType declaringType, PropertyInfo propertyInfo)
		{
			IEdmType edmType = this.GetOrCreateEdmTypeInternal(propertyInfo.PropertyType).EdmType;
			bool flag = ClientTypeUtil.CanAssignNull(propertyInfo.PropertyType);
			IEdmProperty edmProperty;
			if (edmType.TypeKind == EdmTypeKind.Entity || (edmType.TypeKind == EdmTypeKind.Collection && ((IEdmCollectionType)edmType).ElementType.TypeKind() == EdmTypeKind.Entity))
			{
				IEdmEntityType edmEntityType = declaringType as IEdmEntityType;
				if (edmEntityType == null)
				{
					throw Error.InvalidOperation(Strings.ClientTypeCache_NonEntityTypeCannotContainEntityProperties(propertyInfo.Name, propertyInfo.DeclaringType.ToString()));
				}
				edmProperty = EdmNavigationProperty.CreateNavigationPropertyWithPartner(propertyInfo.Name, edmType.ToEdmTypeReference(flag), null, false, EdmOnDeleteAction.None, "Partner", edmEntityType.ToEdmTypeReference(true), null, false, EdmOnDeleteAction.None);
			}
			else
			{
				edmProperty = new EdmStructuralProperty(declaringType, propertyInfo.Name, edmType.ToEdmTypeReference(flag));
			}
			FieldInfo fieldInfo = null;
			if (this.ResolveBackingField != null)
			{
				if ((edmType.TypeKind == EdmTypeKind.Collection && !edmType.IsPrimitive()) || edmType.TypeKind == EdmTypeKind.Complex)
				{
					fieldInfo = this.ResolveBackingField(propertyInfo);
				}
				if (fieldInfo != null && fieldInfo.FieldType != propertyInfo.PropertyType)
				{
					fieldInfo = null;
				}
			}
			edmProperty.SetClientPropertyAnnotation(new ClientPropertyAnnotation(edmProperty, propertyInfo, fieldInfo, this));
			return edmProperty;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00008C60 File Offset: 0x00006E60
		private ClientTypeAnnotation GetOrCreateClientTypeAnnotation(IEdmType edmType, Type type)
		{
			string text = type.ToString();
			ClientTypeAnnotation clientTypeAnnotation;
			if (edmType.TypeKind == EdmTypeKind.Complex || edmType.TypeKind == EdmTypeKind.Entity)
			{
				lock (this.typeNameToClientTypeAnnotationCache)
				{
					if (this.typeNameToClientTypeAnnotationCache.TryGetValue(text, out clientTypeAnnotation) && clientTypeAnnotation.ElementType != type)
					{
						text = type.AssemblyQualifiedName;
						if (this.typeNameToClientTypeAnnotationCache.TryGetValue(text, out clientTypeAnnotation) && clientTypeAnnotation.ElementType != type)
						{
							throw Error.InvalidOperation(Strings.ClientType_MultipleTypesWithSameName(text));
						}
					}
					if (clientTypeAnnotation == null)
					{
						clientTypeAnnotation = new ClientTypeAnnotation(edmType, type, text, this);
						this.typeNameToClientTypeAnnotationCache.Add(text, clientTypeAnnotation);
					}
					return clientTypeAnnotation;
				}
			}
			clientTypeAnnotation = new ClientTypeAnnotation(edmType, type, text, this);
			return clientTypeAnnotation;
		}

		// Token: 0x040001E9 RID: 489
		private readonly Dictionary<Type, ClientEdmModel.EdmTypeCacheValue> clrToEdmTypeCache = new Dictionary<Type, ClientEdmModel.EdmTypeCacheValue>(EqualityComparer<Type>.Default);

		// Token: 0x040001EA RID: 490
		private readonly Dictionary<string, ClientTypeAnnotation> typeNameToClientTypeAnnotationCache = new Dictionary<string, ClientTypeAnnotation>(StringComparer.Ordinal);

		// Token: 0x040001EB RID: 491
		private readonly EdmDirectValueAnnotationsManager directValueAnnotationsManager = new EdmDirectValueAnnotationsManager();

		// Token: 0x040001EC RID: 492
		private readonly DataServiceProtocolVersion maxProtocolVersion;

		// Token: 0x040001ED RID: 493
		private readonly IEnumerable<IEdmModel> coreModel = new IEdmModel[] { EdmCoreModel.Instance };

		// Token: 0x02000031 RID: 49
		private sealed class EdmTypeCacheValue
		{
			// Token: 0x0600017B RID: 379 RVA: 0x00008D2C File Offset: 0x00006F2C
			public EdmTypeCacheValue(IEdmType edmType, bool? hasProperties)
			{
				this.edmType = edmType;
				this.hasProperties = hasProperties;
			}

			// Token: 0x1700005B RID: 91
			// (get) Token: 0x0600017C RID: 380 RVA: 0x00008D42 File Offset: 0x00006F42
			public IEdmType EdmType
			{
				get
				{
					return this.edmType;
				}
			}

			// Token: 0x1700005C RID: 92
			// (get) Token: 0x0600017D RID: 381 RVA: 0x00008D4A File Offset: 0x00006F4A
			// (set) Token: 0x0600017E RID: 382 RVA: 0x00008D52 File Offset: 0x00006F52
			public bool? HasProperties
			{
				get
				{
					return this.hasProperties;
				}
				set
				{
					this.hasProperties = value;
				}
			}

			// Token: 0x040001EF RID: 495
			private readonly IEdmType edmType;

			// Token: 0x040001F0 RID: 496
			private bool? hasProperties;
		}
	}
}
