using System;
using System.Collections;
using System.Data.Services.Client.Metadata;
using Microsoft.Data.Edm;
using Microsoft.Data.OData;

namespace System.Data.Services.Client.Materialization
{
	// Token: 0x02000042 RID: 66
	internal class CollectionValueMaterializationPolicy : MaterializationPolicy
	{
		// Token: 0x0600020D RID: 525 RVA: 0x0000ADAD File Offset: 0x00008FAD
		internal CollectionValueMaterializationPolicy(IODataMaterializerContext context, PrimitiveValueMaterializationPolicy primitivePolicy)
		{
			this.materializerContext = context;
			this.primitiveValueMaterializationPolicy = primitivePolicy;
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000ADC3 File Offset: 0x00008FC3
		// (set) Token: 0x0600020F RID: 527 RVA: 0x0000ADCB File Offset: 0x00008FCB
		internal ComplexValueMaterializationPolicy ComplexValueMaterializationPolicy
		{
			get
			{
				return this.complexValueMaterializationPolicy;
			}
			set
			{
				this.complexValueMaterializationPolicy = value;
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000ADFC File Offset: 0x00008FFC
		internal object CreateCollectionPropertyInstance(ODataProperty collectionProperty, Type userCollectionType)
		{
			ODataCollectionValue odataCollectionValue = collectionProperty.Value as ODataCollectionValue;
			ClientTypeAnnotation collectionClientType = this.materializerContext.ResolveTypeForMaterialization(userCollectionType, odataCollectionValue.TypeName);
			return this.CreateCollectionInstance(collectionClientType.EdmTypeReference as IEdmCollectionTypeReference, collectionClientType.ElementType, () => Strings.AtomMaterializer_NoParameterlessCtorForCollectionProperty(collectionProperty.Name, collectionClientType.ElementTypeName));
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000AE88 File Offset: 0x00009088
		internal object CreateCollectionInstance(IEdmCollectionTypeReference edmCollectionTypeReference, Type clientCollectionType)
		{
			return this.CreateCollectionInstance(edmCollectionTypeReference, clientCollectionType, () => Strings.AtomMaterializer_MaterializationTypeError(clientCollectionType.FullName));
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000AEBC File Offset: 0x000090BC
		internal void ApplyCollectionDataValues(ODataProperty collectionProperty, object collectionInstance, Type collectionItemType, Action<object, object> addValueToBackingICollectionInstance)
		{
			ODataCollectionValue odataCollectionValue = collectionProperty.Value as ODataCollectionValue;
			this.ApplyCollectionDataValues(odataCollectionValue.Items, odataCollectionValue.TypeName, collectionInstance, collectionItemType, addValueToBackingICollectionInstance);
			collectionProperty.SetMaterializedValue(collectionInstance);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000AEF4 File Offset: 0x000090F4
		internal void ApplyCollectionDataValues(IEnumerable items, string wireTypeName, object collectionInstance, Type collectionItemType, Action<object, object> addValueToBackingICollectionInstance)
		{
			if (items != null)
			{
				bool flag = PrimitiveType.IsKnownNullableType(collectionItemType);
				ClientEdmModel model = this.materializerContext.Model;
				foreach (object obj in items)
				{
					if (obj == null)
					{
						throw Error.InvalidOperation(Strings.Collection_NullCollectionItemsNotSupported);
					}
					ODataComplexValue odataComplexValue = obj as ODataComplexValue;
					if (flag)
					{
						if (odataComplexValue != null || obj is ODataCollectionValue)
						{
							throw Error.InvalidOperation(Strings.Collection_ComplexTypesInCollectionOfPrimitiveTypesNotAllowed);
						}
						object obj2 = this.primitiveValueMaterializationPolicy.MaterializePrimitiveDataValueCollectionElement(collectionItemType, wireTypeName, obj);
						addValueToBackingICollectionInstance(collectionInstance, obj2);
					}
					else
					{
						if (odataComplexValue == null)
						{
							throw Error.InvalidOperation(Strings.Collection_PrimitiveTypesInCollectionOfComplexTypesNotAllowed);
						}
						ClientTypeAnnotation clientTypeAnnotation = model.GetClientTypeAnnotation(model.GetOrCreateEdmType(collectionItemType));
						object obj3 = this.CreateNewInstance(clientTypeAnnotation.EdmTypeReference, clientTypeAnnotation.ElementType);
						this.ComplexValueMaterializationPolicy.ApplyDataValues(clientTypeAnnotation, odataComplexValue.Properties, obj3);
						addValueToBackingICollectionInstance(collectionInstance, obj3);
					}
				}
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000B004 File Offset: 0x00009204
		private object CreateCollectionInstance(IEdmCollectionTypeReference edmCollectionTypeReference, Type clientCollectionType, Func<string> error)
		{
			if (ClientTypeUtil.IsDataServiceCollection(clientCollectionType))
			{
				throw Error.InvalidOperation(Strings.AtomMaterializer_DataServiceCollectionNotSupportedForNonEntities);
			}
			object obj;
			try
			{
				obj = this.CreateNewInstance(edmCollectionTypeReference, clientCollectionType);
			}
			catch (MissingMethodException ex)
			{
				throw Error.InvalidOperation(error(), ex);
			}
			return obj;
		}

		// Token: 0x04000221 RID: 545
		private readonly IODataMaterializerContext materializerContext;

		// Token: 0x04000222 RID: 546
		private ComplexValueMaterializationPolicy complexValueMaterializationPolicy;

		// Token: 0x04000223 RID: 547
		private PrimitiveValueMaterializationPolicy primitiveValueMaterializationPolicy;
	}
}
