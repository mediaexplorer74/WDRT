using System;
using System.Collections.Generic;
using System.Data.Services.Client.Metadata;
using Microsoft.Data.Edm;
using Microsoft.Data.OData;

namespace System.Data.Services.Client.Materialization
{
	// Token: 0x02000043 RID: 67
	internal abstract class StructuralValueMaterializationPolicy : MaterializationPolicy
	{
		// Token: 0x06000215 RID: 533 RVA: 0x0000B050 File Offset: 0x00009250
		protected StructuralValueMaterializationPolicy(IODataMaterializerContext materializerContext, SimpleLazy<PrimitivePropertyConverter> lazyPrimitivePropertyConverter)
		{
			this.MaterializerContext = materializerContext;
			this.lazyPrimitivePropertyConverter = lazyPrimitivePropertyConverter;
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000B066 File Offset: 0x00009266
		// (set) Token: 0x06000217 RID: 535 RVA: 0x0000B06E File Offset: 0x0000926E
		protected internal CollectionValueMaterializationPolicy CollectionValueMaterializationPolicy
		{
			get
			{
				return this.collectionValueMaterializationPolicy;
			}
			set
			{
				this.collectionValueMaterializationPolicy = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000B077 File Offset: 0x00009277
		protected PrimitivePropertyConverter PrimitivePropertyConverter
		{
			get
			{
				return this.lazyPrimitivePropertyConverter.Value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000B084 File Offset: 0x00009284
		// (set) Token: 0x0600021A RID: 538 RVA: 0x0000B08C File Offset: 0x0000928C
		private protected IODataMaterializerContext MaterializerContext { protected get; private set; }

		// Token: 0x0600021B RID: 539 RVA: 0x0000B098 File Offset: 0x00009298
		internal void MaterializePrimitiveDataValue(Type type, ODataProperty property)
		{
			if (!property.HasMaterializedValue())
			{
				object value = property.Value;
				object obj = this.PrimitivePropertyConverter.ConvertPrimitiveValue(value, type);
				property.SetMaterializedValue(obj);
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000B0CC File Offset: 0x000092CC
		internal void ApplyDataValues(ClientTypeAnnotation type, IEnumerable<ODataProperty> properties, object instance)
		{
			foreach (ODataProperty odataProperty in properties)
			{
				this.ApplyDataValue(type, odataProperty, instance);
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000B118 File Offset: 0x00009318
		internal void ApplyDataValue(ClientTypeAnnotation type, ODataProperty property, object instance)
		{
			ClientPropertyAnnotation property2 = type.GetProperty(property.Name, this.MaterializerContext.IgnoreMissingProperties);
			if (property2 == null)
			{
				return;
			}
			if (!property2.IsPrimitiveOrComplexCollection)
			{
				object value = property.Value;
				ODataComplexValue odataComplexValue = value as ODataComplexValue;
				if (value != null && odataComplexValue != null)
				{
					if (!property2.EdmProperty.Type.IsComplex())
					{
						throw Error.InvalidOperation(Strings.Deserialize_ExpectingSimpleValue);
					}
					bool flag = false;
					ClientEdmModel model = this.MaterializerContext.Model;
					ClientTypeAnnotation clientTypeAnnotation = model.GetClientTypeAnnotation(model.GetOrCreateEdmType(property2.PropertyType));
					object obj = property2.GetValue(instance);
					if (obj == null)
					{
						obj = this.CreateNewInstance(clientTypeAnnotation.EdmTypeReference, clientTypeAnnotation.ElementType);
						flag = true;
					}
					this.MaterializeDataValues(clientTypeAnnotation, odataComplexValue.Properties, this.MaterializerContext.IgnoreMissingProperties);
					this.ApplyDataValues(clientTypeAnnotation, odataComplexValue.Properties, obj);
					if (flag)
					{
						property2.SetValue(instance, obj, property.Name, true);
						return;
					}
				}
				else
				{
					this.MaterializePrimitiveDataValue(property2.NullablePropertyType, property);
					property2.SetValue(instance, property.GetMaterializedValue(), property.Name, true);
				}
				return;
			}
			if (property.Value == null)
			{
				throw Error.InvalidOperation(Strings.Collection_NullCollectionNotSupported(property.Name));
			}
			if (property.Value is string)
			{
				throw Error.InvalidOperation(Strings.Deserialize_MixedTextWithComment);
			}
			if (property.Value is ODataComplexValue)
			{
				throw Error.InvalidOperation(Strings.AtomMaterializer_InvalidCollectionItem(property.Name));
			}
			object obj2 = property2.GetValue(instance);
			if (obj2 == null)
			{
				obj2 = this.CollectionValueMaterializationPolicy.CreateCollectionPropertyInstance(property, property2.PropertyType);
				property2.SetValue(instance, obj2, property.Name, false);
			}
			else
			{
				property2.ClearBackingICollectionInstance(obj2);
			}
			this.CollectionValueMaterializationPolicy.ApplyCollectionDataValues(property, obj2, property2.PrimitiveOrComplexCollectionItemType, new Action<object, object>(property2.AddValueToBackingICollectionInstance));
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000B2D4 File Offset: 0x000094D4
		internal void MaterializeDataValues(ClientTypeAnnotation actualType, IEnumerable<ODataProperty> values, bool ignoreMissingProperties)
		{
			foreach (ODataProperty odataProperty in values)
			{
				if (!(odataProperty.Value is ODataStreamReferenceValue))
				{
					string name = odataProperty.Name;
					ClientPropertyAnnotation property = actualType.GetProperty(name, ignoreMissingProperties);
					if (property != null)
					{
						if (ClientTypeUtil.TypeOrElementTypeIsEntity(property.PropertyType))
						{
							throw Error.InvalidOperation(Strings.AtomMaterializer_InvalidEntityType(property.EntityCollectionItemType ?? property.PropertyType));
						}
						if (property.IsKnownType)
						{
							this.MaterializePrimitiveDataValue(property.NullablePropertyType, odataProperty);
						}
					}
				}
			}
		}

		// Token: 0x04000224 RID: 548
		private CollectionValueMaterializationPolicy collectionValueMaterializationPolicy;

		// Token: 0x04000225 RID: 549
		private SimpleLazy<PrimitivePropertyConverter> lazyPrimitivePropertyConverter;
	}
}
