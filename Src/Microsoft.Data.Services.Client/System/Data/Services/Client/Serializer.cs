using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Services.Client.Metadata;
using System.Globalization;
using System.Text;
using System.Xml;
using Microsoft.Data.Edm;
using Microsoft.Data.OData;
using Microsoft.Data.OData.Query;

namespace System.Data.Services.Client
{
	// Token: 0x02000122 RID: 290
	internal class Serializer
	{
		// Token: 0x060009AA RID: 2474 RVA: 0x000271BC File Offset: 0x000253BC
		internal Serializer(RequestInfo requestInfo)
		{
			this.requestInfo = requestInfo;
			this.propertyConverter = new ODataPropertyConverter(this.requestInfo);
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x000271DC File Offset: 0x000253DC
		internal static ODataMessageWriter CreateMessageWriter(ODataRequestMessageWrapper requestMessage, RequestInfo requestInfo, bool isParameterPayload)
		{
			ODataMessageWriterSettings odataMessageWriterSettings = requestInfo.WriteHelper.CreateSettings(new Func<ODataEntry, XmlWriter, XmlWriter>(Serializer.StartEntryXmlCustomizer), new Action<ODataEntry, XmlWriter, XmlWriter>(Serializer.EndEntryXmlCustomizer), requestMessage.IsBatchPartRequest);
			return requestMessage.CreateWriter(odataMessageWriterSettings, isParameterPayload);
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0002721C File Offset: 0x0002541C
		internal static ODataEntry CreateODataEntry(EntityDescriptor entityDescriptor, string serverTypeName, ClientTypeAnnotation entityType, DataServiceClientFormat clientFormat)
		{
			ODataEntry odataEntry = new ODataEntry();
			if (entityType.ElementTypeName != serverTypeName)
			{
				odataEntry.SetAnnotation<SerializationTypeNameAnnotation>(new SerializationTypeNameAnnotation
				{
					TypeName = serverTypeName
				});
			}
			odataEntry.TypeName = entityType.ElementTypeName;
			if (clientFormat.UsingAtom && EntityStates.Modified == entityDescriptor.State)
			{
				odataEntry.Id = entityDescriptor.GetLatestIdentity();
			}
			if (entityDescriptor.IsMediaLinkEntry || entityType.IsMediaLinkEntry)
			{
				odataEntry.MediaResource = new ODataStreamReferenceValue();
			}
			return odataEntry;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x00027298 File Offset: 0x00025498
		internal void WriteBodyOperationParameters(List<BodyOperationParameter> operationParameters, ODataRequestMessageWrapper requestMessage)
		{
			using (ODataMessageWriter odataMessageWriter = Serializer.CreateMessageWriter(requestMessage, this.requestInfo, true))
			{
				ODataParameterWriter odataParameterWriter = odataMessageWriter.CreateODataParameterWriter(null);
				odataParameterWriter.WriteStart();
				foreach (OperationParameter operationParameter in operationParameters)
				{
					if (operationParameter.Value != null)
					{
						ClientEdmModel model = this.requestInfo.Model;
						IEdmType orCreateEdmType = model.GetOrCreateEdmType(operationParameter.Value.GetType());
						switch (orCreateEdmType.TypeKind)
						{
						case EdmTypeKind.Primitive:
							odataParameterWriter.WriteValue(operationParameter.Name, operationParameter.Value);
							continue;
						case EdmTypeKind.Complex:
						{
							ODataComplexValue odataComplexValue = this.propertyConverter.CreateODataComplexValue(model.GetClientTypeAnnotation(orCreateEdmType).ElementType, operationParameter.Value, null, false, null);
							odataParameterWriter.WriteValue(operationParameter.Name, odataComplexValue);
							continue;
						}
						case EdmTypeKind.Collection:
						{
							IEnumerator enumerator2 = ((ICollection)operationParameter.Value).GetEnumerator();
							ODataCollectionWriter odataCollectionWriter = odataParameterWriter.CreateCollectionWriter(operationParameter.Name);
							ODataCollectionStart odataCollectionStart = new ODataCollectionStart();
							odataCollectionWriter.WriteStart(odataCollectionStart);
							while (enumerator2.MoveNext())
							{
								object obj = enumerator2.Current;
								if (obj == null)
								{
									throw new NotSupportedException(Strings.Serializer_NullCollectionParamterItemValue(operationParameter.Name));
								}
								IEdmType orCreateEdmType2 = model.GetOrCreateEdmType(obj.GetType());
								switch (orCreateEdmType2.TypeKind)
								{
								case EdmTypeKind.Primitive:
									odataCollectionWriter.WriteItem(obj);
									continue;
								case EdmTypeKind.Complex:
								{
									ODataComplexValue odataComplexValue2 = this.propertyConverter.CreateODataComplexValue(model.GetClientTypeAnnotation(orCreateEdmType2).ElementType, obj, null, false, null);
									odataCollectionWriter.WriteItem(odataComplexValue2);
									continue;
								}
								}
								throw new NotSupportedException(Strings.Serializer_InvalidCollectionParamterItemType(operationParameter.Name, orCreateEdmType2.TypeKind));
							}
							odataCollectionWriter.WriteEnd();
							odataCollectionWriter.Flush();
							continue;
						}
						}
						throw new NotSupportedException(Strings.Serializer_InvalidParameterType(operationParameter.Name, orCreateEdmType.TypeKind));
					}
					odataParameterWriter.WriteValue(operationParameter.Name, operationParameter.Value);
				}
				odataParameterWriter.WriteEnd();
				odataParameterWriter.Flush();
			}
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x000274FC File Offset: 0x000256FC
		internal void WriteEntry(EntityDescriptor entityDescriptor, IEnumerable<LinkDescriptor> relatedLinks, ODataRequestMessageWrapper requestMessage)
		{
			ClientEdmModel model = this.requestInfo.Model;
			ClientTypeAnnotation clientTypeAnnotation = model.GetClientTypeAnnotation(model.GetOrCreateEdmType(entityDescriptor.Entity.GetType()));
			using (ODataMessageWriter odataMessageWriter = Serializer.CreateMessageWriter(requestMessage, this.requestInfo, false))
			{
				ODataWriterWrapper odataWriterWrapper = ODataWriterWrapper.CreateForEntry(odataMessageWriter, this.requestInfo.Configurations.RequestPipeline);
				string text = this.requestInfo.GetServerTypeName(entityDescriptor);
				ODataEntry odataEntry = Serializer.CreateODataEntry(entityDescriptor, text, clientTypeAnnotation, this.requestInfo.Format);
				if (this.requestInfo.HasWritingEventHandlers)
				{
					odataEntry.SetAnnotation<WritingEntityInfo>(new WritingEntityInfo(entityDescriptor.Entity, this.requestInfo));
				}
				if (text == null)
				{
					text = this.requestInfo.InferServerTypeNameFromServerModel(entityDescriptor);
				}
				odataEntry.Properties = this.propertyConverter.PopulateProperties(entityDescriptor.Entity, text, clientTypeAnnotation.PropertiesToSerialize());
				odataWriterWrapper.WriteStart(odataEntry, entityDescriptor.Entity);
				if (EntityStates.Added == entityDescriptor.State)
				{
					this.WriteNavigationLink(entityDescriptor, relatedLinks, odataWriterWrapper);
				}
				odataWriterWrapper.WriteEnd(odataEntry, entityDescriptor.Entity);
			}
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x00027618 File Offset: 0x00025818
		internal void WriteNavigationLink(EntityDescriptor entityDescriptor, IEnumerable<LinkDescriptor> relatedLinks, ODataWriterWrapper odataWriter)
		{
			Dictionary<string, List<LinkDescriptor>> dictionary = new Dictionary<string, List<LinkDescriptor>>(EqualityComparer<string>.Default);
			foreach (LinkDescriptor linkDescriptor in relatedLinks)
			{
				List<LinkDescriptor> list = null;
				if (!dictionary.TryGetValue(linkDescriptor.SourceProperty, out list))
				{
					list = new List<LinkDescriptor>();
					dictionary.Add(linkDescriptor.SourceProperty, list);
				}
				list.Add(linkDescriptor);
			}
			ClientTypeAnnotation clientTypeAnnotation = null;
			foreach (KeyValuePair<string, List<LinkDescriptor>> keyValuePair in dictionary)
			{
				if (clientTypeAnnotation == null)
				{
					ClientEdmModel model = this.requestInfo.Model;
					clientTypeAnnotation = model.GetClientTypeAnnotation(model.GetOrCreateEdmType(entityDescriptor.Entity.GetType()));
				}
				bool isEntityCollection = clientTypeAnnotation.GetProperty(keyValuePair.Key, false).IsEntityCollection;
				bool flag = false;
				foreach (LinkDescriptor linkDescriptor2 in keyValuePair.Value)
				{
					linkDescriptor2.ContentGeneratedForSave = true;
					ODataNavigationLink odataNavigationLink = new ODataNavigationLink();
					odataNavigationLink.Url = this.requestInfo.EntityTracker.GetEntityDescriptor(linkDescriptor2.Target).GetLatestEditLink();
					odataNavigationLink.IsCollection = new bool?(isEntityCollection);
					odataNavigationLink.Name = keyValuePair.Key;
					if (!flag)
					{
						odataWriter.WriteNavigationLinksStart(odataNavigationLink);
						flag = true;
					}
					odataWriter.WriteNavigationLinkStart(odataNavigationLink, linkDescriptor2.Source, linkDescriptor2.Target);
					odataWriter.WriteEntityReferenceLink(new ODataEntityReferenceLink
					{
						Url = odataNavigationLink.Url
					}, linkDescriptor2.Source, linkDescriptor2.Target);
					odataWriter.WriteNavigationLinkEnd(odataNavigationLink, linkDescriptor2.Source, linkDescriptor2.Target);
				}
				odataWriter.WriteNavigationLinksEnd();
			}
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0002783C File Offset: 0x00025A3C
		internal void WriteEntityReferenceLink(LinkDescriptor binding, ODataRequestMessageWrapper requestMessage)
		{
			using (ODataMessageWriter odataMessageWriter = Serializer.CreateMessageWriter(requestMessage, this.requestInfo, false))
			{
				EntityDescriptor entityDescriptor = this.requestInfo.EntityTracker.GetEntityDescriptor(binding.Target);
				Uri uri;
				if (entityDescriptor.GetLatestIdentity() != null)
				{
					uri = entityDescriptor.GetResourceUri(this.requestInfo.BaseUriResolver, false);
				}
				else
				{
					uri = UriUtil.CreateUri("$" + entityDescriptor.ChangeOrder.ToString(CultureInfo.InvariantCulture), UriKind.Relative);
				}
				odataMessageWriter.WriteEntityReferenceLink(new ODataEntityReferenceLink
				{
					Url = uri
				});
			}
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x000278E0 File Offset: 0x00025AE0
		internal Uri WriteUriOperationParametersToUri(Uri requestUri, List<UriOperationParameter> operationParameters)
		{
			UriBuilder uriBuilder = new UriBuilder(requestUri);
			StringBuilder stringBuilder = new StringBuilder();
			string text = UriUtil.UriToString(uriBuilder.Uri);
			if (!string.IsNullOrEmpty(uriBuilder.Query))
			{
				stringBuilder.Append(uriBuilder.Query.Substring(1));
				stringBuilder.Append('&');
			}
			foreach (UriOperationParameter uriOperationParameter in operationParameters)
			{
				string text2 = uriOperationParameter.Name.Trim();
				if (text2.StartsWith(char.ToString('@'), StringComparison.OrdinalIgnoreCase) && !text.Contains(text2))
				{
					throw new DataServiceRequestException(Strings.Serializer_UriDoesNotContainParameterAlias(uriOperationParameter.Name));
				}
				stringBuilder.Append(text2);
				stringBuilder.Append('=');
				stringBuilder.Append(this.ConvertToEscapedUriValue(text2, uriOperationParameter.Value));
				stringBuilder.Append('&');
			}
			stringBuilder.Remove(stringBuilder.Length - 1, 1);
			uriBuilder.Query = stringBuilder.ToString();
			return uriBuilder.Uri;
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x000279F8 File Offset: 0x00025BF8
		private static XmlWriter StartEntryXmlCustomizer(ODataEntry entry, XmlWriter entryWriter)
		{
			WritingEntityInfo annotation = entry.GetAnnotation<WritingEntityInfo>();
			return annotation.EntryPayload.CreateWriter();
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00027A18 File Offset: 0x00025C18
		private static void EndEntryXmlCustomizer(ODataEntry entry, XmlWriter entryWriter, XmlWriter parentWriter)
		{
			WritingEntityInfo annotation = entry.GetAnnotation<WritingEntityInfo>();
			entryWriter.Close();
			annotation.RequestInfo.FireWritingEntityEvent(annotation.Entity, annotation.EntryPayload.Root, null);
			annotation.EntryPayload.Root.WriteTo(parentWriter);
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00027A60 File Offset: 0x00025C60
		private string ConvertToEscapedUriValue(string paramName, object value)
		{
			object obj = null;
			bool flag = false;
			if (value == null)
			{
				flag = true;
			}
			else
			{
				if (!(value.GetType() == typeof(ODataUriNullValue)))
				{
					ClientEdmModel model = this.requestInfo.Model;
					IEdmType orCreateEdmType = model.GetOrCreateEdmType(value.GetType());
					switch (orCreateEdmType.TypeKind)
					{
					case EdmTypeKind.Primitive:
						obj = value;
						flag = true;
						goto IL_15A;
					case EdmTypeKind.Complex:
					{
						ClientTypeAnnotation clientTypeAnnotation = model.GetClientTypeAnnotation(orCreateEdmType);
						obj = this.propertyConverter.CreateODataComplexValue(clientTypeAnnotation.ElementType, value, null, false, null);
						ODataComplexValue odataComplexValue = (ODataComplexValue)obj;
						SerializationTypeNameAnnotation annotation = odataComplexValue.GetAnnotation<SerializationTypeNameAnnotation>();
						if (annotation == null || string.IsNullOrEmpty(annotation.TypeName))
						{
							throw Error.InvalidOperation(Strings.DataServiceException_GeneralError);
						}
						goto IL_15A;
					}
					case EdmTypeKind.Collection:
					{
						IEdmCollectionType edmCollectionType = orCreateEdmType as IEdmCollectionType;
						IEdmTypeReference elementType = edmCollectionType.ElementType;
						ClientTypeAnnotation clientTypeAnnotation2 = model.GetClientTypeAnnotation(elementType.Definition);
						switch (clientTypeAnnotation2.EdmType.TypeKind)
						{
						case EdmTypeKind.Primitive:
						case EdmTypeKind.Complex:
							obj = this.propertyConverter.CreateODataCollection(clientTypeAnnotation2.ElementType, null, value, null);
							goto IL_15A;
						default:
							throw new NotSupportedException(Strings.Serializer_InvalidCollectionParamterItemType(paramName, clientTypeAnnotation2.EdmType.TypeKind));
						}
						break;
					}
					}
					throw new NotSupportedException(Strings.Serializer_InvalidParameterType(paramName, orCreateEdmType.TypeKind));
				}
				obj = value;
				flag = true;
			}
			IL_15A:
			ODataFormat uriLiteralFormat = this.requestInfo.Format.UriLiteralFormat;
			IEdmModel edmModel = ((uriLiteralFormat == ODataFormat.VerboseJson) ? null : this.requestInfo.Model);
			string text = ODataUriUtils.ConvertToUriLiteral(obj, CommonUtil.ConvertToODataVersion(this.requestInfo.MaxProtocolVersionAsVersion), edmModel, uriLiteralFormat);
			if (flag)
			{
				return DataStringEscapeBuilder.EscapeDataString(text);
			}
			return Uri.EscapeDataString(text);
		}

		// Token: 0x04000597 RID: 1431
		private readonly RequestInfo requestInfo;

		// Token: 0x04000598 RID: 1432
		private readonly ODataPropertyConverter propertyConverter;
	}
}
