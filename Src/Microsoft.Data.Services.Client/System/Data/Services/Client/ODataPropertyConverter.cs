using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Services.Client.Metadata;
using System.Xml.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.OData;
using Microsoft.Data.OData.Json;
using Microsoft.Data.OData.Metadata;

namespace System.Data.Services.Client
{
	// Token: 0x0200008D RID: 141
	internal class ODataPropertyConverter
	{
		// Token: 0x06000529 RID: 1321 RVA: 0x000148B2 File Offset: 0x00012AB2
		internal ODataPropertyConverter(RequestInfo requestInfo)
		{
			this.requestInfo = requestInfo;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x000148C4 File Offset: 0x00012AC4
		internal IEnumerable<ODataProperty> PopulateProperties(object resource, string serverTypeName, IEnumerable<ClientPropertyAnnotation> properties)
		{
			List<ODataProperty> list = new List<ODataProperty>();
			foreach (ClientPropertyAnnotation clientPropertyAnnotation in properties)
			{
				object value = clientPropertyAnnotation.GetValue(resource);
				ODataValue odataValue;
				if (this.TryConvertPropertyValue(clientPropertyAnnotation, value, null, out odataValue))
				{
					list.Add(new ODataProperty
					{
						Name = clientPropertyAnnotation.EdmProperty.Name,
						Value = odataValue
					});
					this.AddTypeAnnotationNotDeclaredOnServer(serverTypeName, clientPropertyAnnotation, odataValue);
				}
			}
			return list;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00014958 File Offset: 0x00012B58
		internal ODataComplexValue CreateODataComplexValue(Type complexType, object value, string propertyName, bool isCollectionItem, HashSet<object> visitedComplexTypeObjects)
		{
			ClientEdmModel model = this.requestInfo.Model;
			ClientTypeAnnotation clientTypeAnnotation = model.GetClientTypeAnnotation(complexType);
			if (value == null)
			{
				return null;
			}
			if (visitedComplexTypeObjects == null)
			{
				visitedComplexTypeObjects = new HashSet<object>(ReferenceEqualityComparer<object>.Instance);
			}
			else if (visitedComplexTypeObjects.Contains(value))
			{
				if (propertyName != null)
				{
					throw Error.InvalidOperation(Strings.Serializer_LoopsNotAllowedInComplexTypes(propertyName));
				}
				throw Error.InvalidOperation(Strings.Serializer_LoopsNotAllowedInNonPropertyComplexTypes(clientTypeAnnotation.ElementTypeName));
			}
			visitedComplexTypeObjects.Add(value);
			ODataComplexValue odataComplexValue = new ODataComplexValue();
			if (!this.requestInfo.Format.UsingAtom)
			{
				odataComplexValue.TypeName = clientTypeAnnotation.ElementTypeName;
			}
			if (!isCollectionItem)
			{
				odataComplexValue.SetAnnotation<SerializationTypeNameAnnotation>(new SerializationTypeNameAnnotation
				{
					TypeName = this.requestInfo.GetServerTypeName(clientTypeAnnotation)
				});
			}
			odataComplexValue.Properties = this.PopulateProperties(value, clientTypeAnnotation.PropertiesToSerialize(), visitedComplexTypeObjects);
			visitedComplexTypeObjects.Remove(value);
			return odataComplexValue;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00014A90 File Offset: 0x00012C90
		internal ODataCollectionValue CreateODataCollection(Type collectionItemType, string propertyName, object value, HashSet<object> visitedComplexTypeObjects)
		{
			WebUtil.ValidateCollection(collectionItemType, value, propertyName);
			PrimitiveType primitiveType;
			bool flag = PrimitiveType.TryGetPrimitiveType(collectionItemType, out primitiveType);
			ODataCollectionValue odataCollectionValue = new ODataCollectionValue();
			IEnumerable enumerable = (IEnumerable)value;
			string text;
			string text2;
			if (flag)
			{
				text = ClientConvert.GetEdmType(Nullable.GetUnderlyingType(collectionItemType) ?? collectionItemType);
				odataCollectionValue.Items = Util.GetEnumerable<object>(enumerable, delegate(object val)
				{
					WebUtil.ValidateCollectionItem(val);
					WebUtil.ValidatePrimitiveCollectionItem(val, propertyName, collectionItemType);
					return ODataPropertyConverter.ConvertPrimitiveValueToRecognizedODataType(val, collectionItemType);
				});
				text2 = text;
			}
			else
			{
				text = this.requestInfo.ResolveNameFromType(collectionItemType);
				odataCollectionValue.Items = Util.GetEnumerable<ODataComplexValue>(enumerable, delegate(object val)
				{
					WebUtil.ValidateCollectionItem(val);
					WebUtil.ValidateComplexCollectionItem(val, propertyName, collectionItemType);
					return this.CreateODataComplexValue(collectionItemType, val, propertyName, true, visitedComplexTypeObjects);
				});
				text2 = collectionItemType.FullName;
			}
			if (!this.requestInfo.Format.UsingAtom)
			{
				odataCollectionValue.TypeName = ODataPropertyConverter.GetCollectionName(text2);
			}
			string collectionName = ODataPropertyConverter.GetCollectionName(text);
			odataCollectionValue.SetAnnotation<SerializationTypeNameAnnotation>(new SerializationTypeNameAnnotation
			{
				TypeName = collectionName
			});
			return odataCollectionValue;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00014BC8 File Offset: 0x00012DC8
		private static object ConvertPrimitiveValueToRecognizedODataType(object propertyValue, Type propertyType)
		{
			if (propertyValue == null)
			{
				return null;
			}
			PrimitiveType primitiveType;
			PrimitiveType.TryGetPrimitiveType(propertyType, out primitiveType);
			if (propertyType == typeof(char) || propertyType == typeof(char[]) || propertyType == typeof(Type) || propertyType == typeof(Uri) || propertyType == typeof(XDocument) || propertyType == typeof(XElement))
			{
				return primitiveType.TypeConverter.ToString(propertyValue);
			}
			if (propertyType.FullName == "System.Data.Linq.Binary")
			{
				return ((BinaryTypeConverter)primitiveType.TypeConverter).ToArray(propertyValue);
			}
			if (primitiveType.EdmTypeName == null)
			{
				throw new NotSupportedException(Strings.ALinq_CantCastToUnsupportedPrimitive(propertyType.Name));
			}
			return propertyValue;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00014C9A File Offset: 0x00012E9A
		private static string GetCollectionName(string itemTypeName)
		{
			if (itemTypeName != null)
			{
				return EdmLibraryExtensions.GetCollectionTypeName(itemTypeName);
			}
			return null;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00014CA7 File Offset: 0x00012EA7
		private static ODataValue CreateODataPrimitivePropertyValue(ClientPropertyAnnotation property, object propertyValue)
		{
			if (propertyValue == null)
			{
				return new ODataNullValue();
			}
			propertyValue = ODataPropertyConverter.ConvertPrimitiveValueToRecognizedODataType(propertyValue, property.PropertyType);
			return new ODataPrimitiveValue(propertyValue);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00014CC8 File Offset: 0x00012EC8
		private IEnumerable<ODataProperty> PopulateProperties(object resource, IEnumerable<ClientPropertyAnnotation> properties, HashSet<object> visitedComplexTypeObjects)
		{
			List<ODataProperty> list = new List<ODataProperty>();
			foreach (ClientPropertyAnnotation clientPropertyAnnotation in properties)
			{
				object value = clientPropertyAnnotation.GetValue(resource);
				ODataValue odataValue;
				if (this.TryConvertPropertyValue(clientPropertyAnnotation, value, visitedComplexTypeObjects, out odataValue))
				{
					list.Add(new ODataProperty
					{
						Name = clientPropertyAnnotation.EdmProperty.Name,
						Value = odataValue
					});
				}
			}
			return list;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00014D54 File Offset: 0x00012F54
		private bool TryConvertPropertyValue(ClientPropertyAnnotation property, object propertyValue, HashSet<object> visitedComplexTypeObjects, out ODataValue odataValue)
		{
			if (property.IsKnownType)
			{
				odataValue = ODataPropertyConverter.CreateODataPrimitivePropertyValue(property, propertyValue);
				return true;
			}
			if (property.IsPrimitiveOrComplexCollection)
			{
				odataValue = this.CreateODataCollectionPropertyValue(property, propertyValue, visitedComplexTypeObjects);
				return true;
			}
			if (!property.IsEntityCollection && !ClientTypeUtil.TypeIsEntity(property.PropertyType, this.requestInfo.Model))
			{
				odataValue = this.CreateODataComplexPropertyValue(property, propertyValue, visitedComplexTypeObjects);
				return true;
			}
			odataValue = null;
			return false;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00014DC0 File Offset: 0x00012FC0
		private ODataComplexValue CreateODataComplexPropertyValue(ClientPropertyAnnotation property, object propertyValue, HashSet<object> visitedComplexTypeObjects)
		{
			Type type = (property.IsPrimitiveOrComplexCollection ? property.PrimitiveOrComplexCollectionItemType : property.PropertyType);
			return this.CreateODataComplexValue(type, propertyValue, property.PropertyName, property.IsPrimitiveOrComplexCollection, visitedComplexTypeObjects);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00014DF9 File Offset: 0x00012FF9
		private ODataCollectionValue CreateODataCollectionPropertyValue(ClientPropertyAnnotation property, object propertyValue, HashSet<object> visitedComplexTypeObjects)
		{
			return this.CreateODataCollection(property.PrimitiveOrComplexCollectionItemType, property.PropertyName, propertyValue, visitedComplexTypeObjects);
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00014E10 File Offset: 0x00013010
		private void AddTypeAnnotationNotDeclaredOnServer(string serverTypeName, ClientPropertyAnnotation property, ODataValue odataValue)
		{
			ODataPrimitiveValue odataPrimitiveValue = odataValue as ODataPrimitiveValue;
			if (odataPrimitiveValue == null)
			{
				return;
			}
			if (!this.requestInfo.Format.UsingAtom && this.requestInfo.TypeResolver.ShouldWriteClientTypeForOpenServerProperty(property.EdmProperty, serverTypeName) && !JsonSharedUtils.ValueTypeMatchesJsonType(odataPrimitiveValue, property.EdmProperty.Type.AsPrimitive()))
			{
				odataPrimitiveValue.SetAnnotation<SerializationTypeNameAnnotation>(new SerializationTypeNameAnnotation
				{
					TypeName = property.EdmProperty.Type.FullName()
				});
			}
		}

		// Token: 0x04000305 RID: 773
		private readonly RequestInfo requestInfo;
	}
}
