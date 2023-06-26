using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.OData.Json;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x02000235 RID: 565
	internal sealed class ODataVerboseJsonEntryAndFeedDeserializer : ODataVerboseJsonPropertyAndValueDeserializer
	{
		// Token: 0x060011F8 RID: 4600 RVA: 0x00043070 File Offset: 0x00041270
		internal ODataVerboseJsonEntryAndFeedDeserializer(ODataVerboseJsonInputContext verboseJsonInputContext)
			: base(verboseJsonInputContext)
		{
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x0004307C File Offset: 0x0004127C
		internal void ReadFeedStart(ODataFeed feed, bool isResultsWrapperExpected, bool isExpandedLinkContent)
		{
			if (isResultsWrapperExpected)
			{
				base.JsonReader.ReadNext();
				while (base.JsonReader.NodeType == JsonNodeType.Property)
				{
					string text = base.JsonReader.ReadPropertyName();
					if (string.CompareOrdinal("results", text) == 0)
					{
						goto IL_4C;
					}
					this.ReadFeedProperty(feed, text, isExpandedLinkContent);
				}
				throw new ODataException(Strings.ODataJsonEntryAndFeedDeserializer_ExpectedFeedResultsPropertyNotFound);
			}
			IL_4C:
			if (base.JsonReader.NodeType != JsonNodeType.StartArray)
			{
				throw new ODataException(Strings.ODataJsonEntryAndFeedDeserializer_CannotReadFeedContentStart(base.JsonReader.NodeType));
			}
			base.JsonReader.ReadStartArray();
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x0004310C File Offset: 0x0004130C
		internal void ReadFeedEnd(ODataFeed feed, bool readResultsWrapper, bool isExpandedLinkContent)
		{
			if (readResultsWrapper)
			{
				base.JsonReader.ReadEndArray();
				while (base.JsonReader.NodeType == JsonNodeType.Property)
				{
					string text = base.JsonReader.ReadPropertyName();
					this.ReadFeedProperty(feed, text, isExpandedLinkContent);
				}
			}
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x0004314C File Offset: 0x0004134C
		internal void ReadEntryStart()
		{
			if (base.JsonReader.NodeType != JsonNodeType.StartObject)
			{
				throw new ODataException(Strings.ODataJsonReader_CannotReadEntryStart(base.JsonReader.NodeType));
			}
			base.JsonReader.ReadNext();
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x00043184 File Offset: 0x00041384
		internal void ReadEntryMetadataPropertyValue(IODataVerboseJsonReaderEntryState entryState)
		{
			ODataEntry entry = entryState.Entry;
			base.JsonReader.ReadStartObject();
			ODataStreamReferenceValue odataStreamReferenceValue = null;
			ODataVerboseJsonReaderUtils.MetadataPropertyBitMask metadataPropertyBitMask = ODataVerboseJsonReaderUtils.MetadataPropertyBitMask.None;
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				string text = base.JsonReader.ReadPropertyName();
				string text2;
				switch (text2 = text)
				{
				case "uri":
					this.ReadUriMetadataProperty(entry, ref metadataPropertyBitMask);
					continue;
				case "id":
					this.ReadIdMetadataProperty(entry, ref metadataPropertyBitMask);
					continue;
				case "etag":
					this.ReadETagMetadataProperty(entry, ref metadataPropertyBitMask);
					continue;
				case "type":
					base.JsonReader.SkipValue();
					continue;
				case "media_src":
					this.ReadMediaSourceMetadataProperty(ref metadataPropertyBitMask, ref odataStreamReferenceValue);
					continue;
				case "edit_media":
					this.ReadEditMediaMetadataProperty(ref metadataPropertyBitMask, ref odataStreamReferenceValue);
					continue;
				case "content_type":
					this.ReadContentTypeMetadataProperty(ref metadataPropertyBitMask, ref odataStreamReferenceValue);
					continue;
				case "media_etag":
					this.ReadMediaETagMetadataProperty(ref metadataPropertyBitMask, ref odataStreamReferenceValue);
					continue;
				case "actions":
					this.ReadActionsMetadataProperty(entry, ref metadataPropertyBitMask);
					continue;
				case "functions":
					this.ReadFunctionsMetadataProperty(entry, ref metadataPropertyBitMask);
					continue;
				case "properties":
					this.ReadPropertiesMetadataProperty(entryState, ref metadataPropertyBitMask);
					continue;
				}
				base.JsonReader.SkipValue();
			}
			entry.MediaResource = odataStreamReferenceValue;
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x00043350 File Offset: 0x00041550
		internal void ValidateEntryMetadata(IODataVerboseJsonReaderEntryState entryState)
		{
			ODataEntry entry = entryState.Entry;
			IEdmEntityType entityType = entryState.EntityType;
			if (base.Model.HasDefaultStream(entityType) && entry.MediaResource == null)
			{
				ODataStreamReferenceValue odataStreamReferenceValue = null;
				ODataVerboseJsonReaderUtils.EnsureInstance<ODataStreamReferenceValue>(ref odataStreamReferenceValue);
				entry.MediaResource = odataStreamReferenceValue;
			}
			bool useDefaultFormatBehavior = base.UseDefaultFormatBehavior;
			ValidationUtils.ValidateEntryMetadataResource(entry, entityType, base.Model, useDefaultFormatBehavior);
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x000433A8 File Offset: 0x000415A8
		internal ODataNavigationLink ReadEntryContent(IODataVerboseJsonReaderEntryState entryState, out IEdmNavigationProperty navigationProperty)
		{
			ODataNavigationLink odataNavigationLink = null;
			navigationProperty = null;
			IEdmEntityType entityType = entryState.EntityType;
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				string text = base.JsonReader.ReadPropertyName();
				if (string.CompareOrdinal("__metadata", text) == 0)
				{
					if (entryState.MetadataPropertyFound)
					{
						throw new ODataException(Strings.ODataJsonEntryAndFeedDeserializer_MultipleMetadataPropertiesInEntryValue);
					}
					entryState.MetadataPropertyFound = true;
					base.JsonReader.SkipValue();
				}
				else
				{
					if (!ValidationUtils.IsValidPropertyName(text))
					{
						base.JsonReader.SkipValue();
						continue;
					}
					IEdmProperty edmProperty = ReaderValidationUtils.FindDefinedProperty(text, entityType);
					if (edmProperty != null)
					{
						navigationProperty = edmProperty as IEdmNavigationProperty;
						if (navigationProperty != null)
						{
							if (this.ShouldEntryPropertyBeSkipped())
							{
								base.JsonReader.SkipValue();
							}
							else
							{
								bool flag = navigationProperty.Type.IsCollection();
								odataNavigationLink = new ODataNavigationLink
								{
									Name = text,
									IsCollection = new bool?(flag)
								};
								this.ValidateNavigationLinkPropertyValue(flag);
								entryState.DuplicatePropertyNamesChecker.CheckForDuplicatePropertyNamesOnNavigationLinkStart(odataNavigationLink);
							}
						}
						else
						{
							this.ReadEntryProperty(entryState, edmProperty);
						}
					}
					else
					{
						odataNavigationLink = this.ReadUndeclaredProperty(entryState, text);
					}
				}
				if (odataNavigationLink != null)
				{
					break;
				}
			}
			return odataNavigationLink;
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x000434BC File Offset: 0x000416BC
		internal void ReadDeferredNavigationLink(ODataNavigationLink navigationLink)
		{
			base.JsonReader.ReadStartObject();
			base.JsonReader.ReadPropertyName();
			base.JsonReader.ReadStartObject();
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				string text = base.JsonReader.ReadPropertyName();
				if (string.CompareOrdinal("uri", text) == 0)
				{
					if (navigationLink.Url != null)
					{
						throw new ODataException(Strings.ODataJsonEntryAndFeedDeserializer_MultipleUriPropertiesInDeferredLink);
					}
					string text2 = base.JsonReader.ReadStringValue("uri");
					if (text2 == null)
					{
						throw new ODataException(Strings.ODataJsonEntryAndFeedDeserializer_DeferredLinkUriCannotBeNull);
					}
					navigationLink.Url = base.ProcessUriFromPayload(text2);
				}
				else
				{
					base.JsonReader.SkipValue();
				}
			}
			if (navigationLink.Url == null)
			{
				throw new ODataException(Strings.ODataJsonEntryAndFeedDeserializer_DeferredLinkMissingUri);
			}
			base.JsonReader.ReadEndObject();
			base.JsonReader.ReadEndObject();
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x00043598 File Offset: 0x00041798
		internal ODataEntityReferenceLink ReadEntityReferenceLink()
		{
			base.JsonReader.ReadStartObject();
			base.JsonReader.ReadPropertyName();
			base.JsonReader.ReadStartObject();
			ODataEntityReferenceLink odataEntityReferenceLink = new ODataEntityReferenceLink();
			ODataVerboseJsonReaderUtils.MetadataPropertyBitMask metadataPropertyBitMask = ODataVerboseJsonReaderUtils.MetadataPropertyBitMask.None;
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				string text = base.JsonReader.ReadPropertyName();
				if (string.CompareOrdinal("uri", text) == 0)
				{
					ODataVerboseJsonReaderUtils.VerifyMetadataPropertyNotFound(ref metadataPropertyBitMask, ODataVerboseJsonReaderUtils.MetadataPropertyBitMask.Uri, "uri");
					string text2 = base.JsonReader.ReadStringValue("uri");
					ODataVerboseJsonReaderUtils.ValidateMetadataStringProperty(text2, "uri");
					odataEntityReferenceLink.Url = base.ProcessUriFromPayload(text2);
				}
				else
				{
					base.JsonReader.SkipValue();
				}
			}
			base.JsonReader.ReadEndObject();
			base.JsonReader.ReadEndObject();
			return odataEntityReferenceLink;
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x00043654 File Offset: 0x00041854
		internal bool IsDeferredLink(bool navigationLinkFound)
		{
			JsonNodeType jsonNodeType = base.JsonReader.NodeType;
			if (jsonNodeType == JsonNodeType.PrimitiveValue)
			{
				if (base.JsonReader.Value == null)
				{
					return false;
				}
				if (navigationLinkFound)
				{
					throw new ODataException(Strings.ODataJsonEntryAndFeedDeserializer_CannotReadNavigationPropertyValue);
				}
				return false;
			}
			else
			{
				if (jsonNodeType == JsonNodeType.StartArray)
				{
					return false;
				}
				base.JsonReader.StartBuffering();
				bool flag;
				try
				{
					base.JsonReader.ReadStartObject();
					jsonNodeType = base.JsonReader.NodeType;
					if (jsonNodeType == JsonNodeType.EndObject)
					{
						flag = false;
					}
					else
					{
						string text = base.JsonReader.ReadPropertyName();
						if (string.CompareOrdinal("__deferred", text) != 0)
						{
							flag = false;
						}
						else
						{
							base.JsonReader.SkipValue();
							flag = base.JsonReader.NodeType == JsonNodeType.EndObject;
						}
					}
				}
				finally
				{
					base.JsonReader.StopBuffering();
				}
				return flag;
			}
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x00043718 File Offset: 0x00041918
		internal bool IsEntityReferenceLink()
		{
			JsonNodeType jsonNodeType = base.JsonReader.NodeType;
			if (jsonNodeType != JsonNodeType.StartObject)
			{
				return false;
			}
			base.JsonReader.StartBuffering();
			bool flag;
			try
			{
				base.JsonReader.ReadStartObject();
				jsonNodeType = base.JsonReader.NodeType;
				if (jsonNodeType == JsonNodeType.EndObject)
				{
					flag = false;
				}
				else
				{
					bool flag2 = false;
					while (base.JsonReader.NodeType == JsonNodeType.Property)
					{
						string text = base.JsonReader.ReadPropertyName();
						if (string.CompareOrdinal("__metadata", text) != 0)
						{
							return false;
						}
						if (base.JsonReader.NodeType != JsonNodeType.StartObject)
						{
							return false;
						}
						base.JsonReader.ReadStartObject();
						while (base.JsonReader.NodeType == JsonNodeType.Property)
						{
							string text2 = base.JsonReader.ReadPropertyName();
							if (string.CompareOrdinal("uri", text2) == 0)
							{
								flag2 = true;
							}
							base.JsonReader.SkipValue();
						}
						base.JsonReader.ReadEndObject();
					}
					flag = flag2;
				}
			}
			finally
			{
				base.JsonReader.StopBuffering();
			}
			return flag;
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x00043820 File Offset: 0x00041A20
		private static void AddEntryProperty(IODataVerboseJsonReaderEntryState entryState, string propertyName, object propertyValue)
		{
			ODataProperty odataProperty = new ODataProperty
			{
				Name = propertyName,
				Value = propertyValue
			};
			entryState.DuplicatePropertyNamesChecker.CheckForDuplicatePropertyNames(odataProperty);
			ODataEntry entry = entryState.Entry;
			entry.Properties = entry.Properties.ConcatToReadOnlyEnumerable("Properties", odataProperty);
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x00043870 File Offset: 0x00041A70
		private void ReadFeedProperty(ODataFeed feed, string propertyName, bool isExpandedLinkContent)
		{
			switch (ODataVerboseJsonReaderUtils.DetermineFeedPropertyKind(propertyName))
			{
			case ODataVerboseJsonReaderUtils.FeedPropertyKind.Unsupported:
				base.JsonReader.SkipValue();
				return;
			case ODataVerboseJsonReaderUtils.FeedPropertyKind.Count:
				if (base.ReadingResponse && base.Version >= ODataVersion.V2 && !isExpandedLinkContent)
				{
					string text = base.JsonReader.ReadStringValue("__count");
					ODataVerboseJsonReaderUtils.ValidateFeedProperty(text, "__count");
					long num = (long)ODataVerboseJsonReaderUtils.ConvertValue(text, EdmCoreModel.Instance.GetInt64(false), base.MessageReaderSettings, base.Version, true, propertyName);
					feed.Count = new long?(num);
					return;
				}
				base.JsonReader.SkipValue();
				return;
			case ODataVerboseJsonReaderUtils.FeedPropertyKind.Results:
				throw new ODataException(Strings.ODataJsonEntryAndFeedDeserializer_MultipleFeedResultsPropertiesFound);
			case ODataVerboseJsonReaderUtils.FeedPropertyKind.NextPageLink:
				if (base.ReadingResponse && base.Version >= ODataVersion.V2)
				{
					string text2 = base.JsonReader.ReadStringValue("__next");
					ODataVerboseJsonReaderUtils.ValidateFeedProperty(text2, "__next");
					feed.NextPageLink = base.ProcessUriFromPayload(text2);
					return;
				}
				base.JsonReader.SkipValue();
				return;
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataVerboseJsonEntryAndFeedDeserializer_ReadFeedProperty));
			}
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x00043980 File Offset: 0x00041B80
		private void ReadEntryProperty(IODataVerboseJsonReaderEntryState entryState, IEdmProperty edmProperty)
		{
			ODataNullValueBehaviorKind odataNullValueBehaviorKind = (base.ReadingResponse ? ODataNullValueBehaviorKind.Default : base.Model.NullValueReadBehaviorKind(edmProperty));
			IEdmTypeReference type = edmProperty.Type;
			object obj = (type.IsStream() ? this.ReadStreamPropertyValue() : base.ReadNonEntityValue(type, null, null, odataNullValueBehaviorKind == ODataNullValueBehaviorKind.Default, edmProperty.Name));
			if (odataNullValueBehaviorKind != ODataNullValueBehaviorKind.IgnoreValue || obj != null)
			{
				ODataVerboseJsonEntryAndFeedDeserializer.AddEntryProperty(entryState, edmProperty.Name, obj);
			}
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x000439E4 File Offset: 0x00041BE4
		private void ReadOpenProperty(IODataVerboseJsonReaderEntryState entryState, string propertyName)
		{
			object obj = base.ReadNonEntityValue(null, null, null, true, propertyName);
			ValidationUtils.ValidateOpenPropertyValue(propertyName, obj, base.MessageReaderSettings.UndeclaredPropertyBehaviorKinds);
			ODataVerboseJsonEntryAndFeedDeserializer.AddEntryProperty(entryState, propertyName, obj);
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x00043A18 File Offset: 0x00041C18
		private ODataNavigationLink ReadUndeclaredProperty(IODataVerboseJsonReaderEntryState entryState, string propertyName)
		{
			if (entryState.EntityType.IsOpen && this.ShouldEntryPropertyBeSkipped())
			{
				base.JsonReader.SkipValue();
				return null;
			}
			bool flag = false;
			bool flag2 = false;
			if (base.JsonReader.NodeType == JsonNodeType.StartObject)
			{
				base.JsonReader.StartBuffering();
				base.JsonReader.Read();
				if (base.JsonReader.NodeType == JsonNodeType.Property)
				{
					string text = base.JsonReader.ReadPropertyName();
					if (string.CompareOrdinal(text, "__deferred") == 0)
					{
						flag2 = true;
					}
					else if (string.CompareOrdinal(text, "__mediaresource") == 0)
					{
						flag = true;
					}
					base.JsonReader.SkipValue();
					if (base.JsonReader.NodeType != JsonNodeType.EndObject)
					{
						flag = false;
						flag2 = false;
					}
				}
				base.JsonReader.StopBuffering();
			}
			if (flag || flag2)
			{
				if (!base.MessageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.ReportUndeclaredLinkProperty))
				{
					throw new ODataException(Strings.ValidationUtils_PropertyDoesNotExistOnType(propertyName, entryState.EntityType.ODataFullName()));
				}
			}
			else
			{
				if (entryState.EntityType.IsOpen)
				{
					this.ReadOpenProperty(entryState, propertyName);
					return null;
				}
				if (!base.MessageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.IgnoreUndeclaredValueProperty))
				{
					throw new ODataException(Strings.ValidationUtils_PropertyDoesNotExistOnType(propertyName, entryState.EntityType.ODataFullName()));
				}
			}
			if (flag2)
			{
				ODataNavigationLink odataNavigationLink = new ODataNavigationLink
				{
					Name = propertyName,
					IsCollection = null
				};
				entryState.DuplicatePropertyNamesChecker.CheckForDuplicatePropertyNamesOnNavigationLinkStart(odataNavigationLink);
				return odataNavigationLink;
			}
			if (flag)
			{
				object obj = this.ReadStreamPropertyValue();
				ODataVerboseJsonEntryAndFeedDeserializer.AddEntryProperty(entryState, propertyName, obj);
				return null;
			}
			base.JsonReader.SkipValue();
			return null;
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x00043B90 File Offset: 0x00041D90
		private ODataStreamReferenceValue ReadStreamPropertyValue()
		{
			if (!base.ReadingResponse)
			{
				throw new ODataException(Strings.ODataJsonEntryAndFeedDeserializer_StreamPropertyInRequest);
			}
			ODataVersionChecker.CheckStreamReferenceProperty(base.Version);
			base.JsonReader.ReadStartObject();
			if (base.JsonReader.NodeType != JsonNodeType.Property)
			{
				throw new ODataException(Strings.ODataJsonEntryAndFeedDeserializer_CannotParseStreamReference);
			}
			string text = base.JsonReader.ReadPropertyName();
			if (string.CompareOrdinal("__mediaresource", text) != 0)
			{
				throw new ODataException(Strings.ODataJsonEntryAndFeedDeserializer_CannotParseStreamReference);
			}
			ODataStreamReferenceValue odataStreamReferenceValue = this.ReadStreamReferenceValue();
			if (base.JsonReader.NodeType != JsonNodeType.EndObject)
			{
				throw new ODataException(Strings.ODataJsonEntryAndFeedDeserializer_CannotParseStreamReference);
			}
			base.JsonReader.Read();
			return odataStreamReferenceValue;
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x00043C34 File Offset: 0x00041E34
		private void ReadUriMetadataProperty(ODataEntry entry, ref ODataVerboseJsonReaderUtils.MetadataPropertyBitMask metadataPropertiesFoundBitField)
		{
			ODataVerboseJsonReaderUtils.VerifyMetadataPropertyNotFound(ref metadataPropertiesFoundBitField, ODataVerboseJsonReaderUtils.MetadataPropertyBitMask.Uri, "uri");
			string text = base.JsonReader.ReadStringValue("uri");
			if (text != null)
			{
				ODataVerboseJsonReaderUtils.ValidateMetadataStringProperty(text, "uri");
				entry.EditLink = base.ProcessUriFromPayload(text);
			}
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x00043C7C File Offset: 0x00041E7C
		private void ReadIdMetadataProperty(ODataEntry entry, ref ODataVerboseJsonReaderUtils.MetadataPropertyBitMask metadataPropertiesFoundBitField)
		{
			if (base.UseServerFormatBehavior)
			{
				base.JsonReader.SkipValue();
				return;
			}
			ODataVerboseJsonReaderUtils.VerifyMetadataPropertyNotFound(ref metadataPropertiesFoundBitField, ODataVerboseJsonReaderUtils.MetadataPropertyBitMask.Id, "id");
			string text = base.JsonReader.ReadStringValue("id");
			ODataVerboseJsonReaderUtils.ValidateMetadataStringProperty(text, "id");
			entry.Id = text;
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x00043CD0 File Offset: 0x00041ED0
		private void ReadETagMetadataProperty(ODataEntry entry, ref ODataVerboseJsonReaderUtils.MetadataPropertyBitMask metadataPropertiesFoundBitField)
		{
			if (base.UseServerFormatBehavior)
			{
				base.JsonReader.SkipValue();
				return;
			}
			ODataVerboseJsonReaderUtils.VerifyMetadataPropertyNotFound(ref metadataPropertiesFoundBitField, ODataVerboseJsonReaderUtils.MetadataPropertyBitMask.ETag, "etag");
			string text = base.JsonReader.ReadStringValue("etag");
			ODataVerboseJsonReaderUtils.ValidateMetadataStringProperty(text, "etag");
			entry.ETag = text;
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x00043D20 File Offset: 0x00041F20
		private void ReadMediaSourceMetadataProperty(ref ODataVerboseJsonReaderUtils.MetadataPropertyBitMask metadataPropertiesFoundBitField, ref ODataStreamReferenceValue mediaResource)
		{
			if (base.UseServerFormatBehavior)
			{
				base.JsonReader.SkipValue();
				return;
			}
			ODataVerboseJsonReaderUtils.VerifyMetadataPropertyNotFound(ref metadataPropertiesFoundBitField, ODataVerboseJsonReaderUtils.MetadataPropertyBitMask.MediaUri, "media_src");
			ODataVerboseJsonReaderUtils.EnsureInstance<ODataStreamReferenceValue>(ref mediaResource);
			string text = base.JsonReader.ReadStringValue("media_src");
			ODataVerboseJsonReaderUtils.ValidateMetadataStringProperty(text, "media_src");
			mediaResource.ReadLink = base.ProcessUriFromPayload(text);
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x00043D80 File Offset: 0x00041F80
		private void ReadEditMediaMetadataProperty(ref ODataVerboseJsonReaderUtils.MetadataPropertyBitMask metadataPropertiesFoundBitField, ref ODataStreamReferenceValue mediaResource)
		{
			if (base.UseServerFormatBehavior)
			{
				base.JsonReader.SkipValue();
				return;
			}
			ODataVerboseJsonReaderUtils.VerifyMetadataPropertyNotFound(ref metadataPropertiesFoundBitField, ODataVerboseJsonReaderUtils.MetadataPropertyBitMask.EditMedia, "edit_media");
			ODataVerboseJsonReaderUtils.EnsureInstance<ODataStreamReferenceValue>(ref mediaResource);
			string text = base.JsonReader.ReadStringValue("edit_media");
			ODataVerboseJsonReaderUtils.ValidateMetadataStringProperty(text, "edit_media");
			mediaResource.EditLink = base.ProcessUriFromPayload(text);
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x00043DE0 File Offset: 0x00041FE0
		private void ReadContentTypeMetadataProperty(ref ODataVerboseJsonReaderUtils.MetadataPropertyBitMask metadataPropertiesFoundBitField, ref ODataStreamReferenceValue mediaResource)
		{
			if (base.UseServerFormatBehavior)
			{
				base.JsonReader.SkipValue();
				return;
			}
			ODataVerboseJsonReaderUtils.VerifyMetadataPropertyNotFound(ref metadataPropertiesFoundBitField, ODataVerboseJsonReaderUtils.MetadataPropertyBitMask.ContentType, "content_type");
			ODataVerboseJsonReaderUtils.EnsureInstance<ODataStreamReferenceValue>(ref mediaResource);
			string text = base.JsonReader.ReadStringValue("content_type");
			ODataVerboseJsonReaderUtils.ValidateMetadataStringProperty(text, "content_type");
			mediaResource.ContentType = text;
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x00043E38 File Offset: 0x00042038
		private void ReadMediaETagMetadataProperty(ref ODataVerboseJsonReaderUtils.MetadataPropertyBitMask metadataPropertiesFoundBitField, ref ODataStreamReferenceValue mediaResource)
		{
			if (base.UseServerFormatBehavior)
			{
				base.JsonReader.SkipValue();
				return;
			}
			ODataVerboseJsonReaderUtils.VerifyMetadataPropertyNotFound(ref metadataPropertiesFoundBitField, ODataVerboseJsonReaderUtils.MetadataPropertyBitMask.MediaETag, "media_etag");
			ODataVerboseJsonReaderUtils.EnsureInstance<ODataStreamReferenceValue>(ref mediaResource);
			string text = base.JsonReader.ReadStringValue("media_etag");
			ODataVerboseJsonReaderUtils.ValidateMetadataStringProperty(text, "media_etag");
			mediaResource.ETag = text;
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x00043E90 File Offset: 0x00042090
		private void ReadActionsMetadataProperty(ODataEntry entry, ref ODataVerboseJsonReaderUtils.MetadataPropertyBitMask metadataPropertiesFoundBitField)
		{
			if (base.MessageReaderSettings.MaxProtocolVersion >= ODataVersion.V3 && base.ReadingResponse)
			{
				ODataVerboseJsonReaderUtils.VerifyMetadataPropertyNotFound(ref metadataPropertiesFoundBitField, ODataVerboseJsonReaderUtils.MetadataPropertyBitMask.Actions, "actions");
				this.ReadOperationsMetadata(entry, true);
				return;
			}
			base.JsonReader.SkipValue();
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x00043ECC File Offset: 0x000420CC
		private void ReadFunctionsMetadataProperty(ODataEntry entry, ref ODataVerboseJsonReaderUtils.MetadataPropertyBitMask metadataPropertiesFoundBitField)
		{
			if (base.MessageReaderSettings.MaxProtocolVersion >= ODataVersion.V3 && base.ReadingResponse)
			{
				ODataVerboseJsonReaderUtils.VerifyMetadataPropertyNotFound(ref metadataPropertiesFoundBitField, ODataVerboseJsonReaderUtils.MetadataPropertyBitMask.Functions, "functions");
				this.ReadOperationsMetadata(entry, false);
				return;
			}
			base.JsonReader.SkipValue();
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x00043F08 File Offset: 0x00042108
		private void ReadPropertiesMetadataProperty(IODataVerboseJsonReaderEntryState entryState, ref ODataVerboseJsonReaderUtils.MetadataPropertyBitMask metadataPropertiesFoundBitField)
		{
			if (!base.ReadingResponse || base.MessageReaderSettings.MaxProtocolVersion < ODataVersion.V3)
			{
				base.JsonReader.SkipValue();
				return;
			}
			ODataVerboseJsonReaderUtils.VerifyMetadataPropertyNotFound(ref metadataPropertiesFoundBitField, ODataVerboseJsonReaderUtils.MetadataPropertyBitMask.Properties, "properties");
			if (base.JsonReader.NodeType != JsonNodeType.StartObject)
			{
				throw new ODataException(Strings.ODataJsonEntryAndFeedDeserializer_PropertyInEntryMustHaveObjectValue("properties", base.JsonReader.NodeType));
			}
			base.JsonReader.ReadStartObject();
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				string text = base.JsonReader.ReadPropertyName();
				ValidationUtils.ValidateAssociationLinkName(text);
				ReaderValidationUtils.ValidateNavigationPropertyDefined(text, entryState.EntityType, base.MessageReaderSettings);
				base.JsonReader.ReadStartObject();
				while (base.JsonReader.NodeType == JsonNodeType.Property)
				{
					string text2 = base.JsonReader.ReadPropertyName();
					if (string.CompareOrdinal(text2, "associationuri") == 0)
					{
						string text3 = base.JsonReader.ReadStringValue("associationuri");
						ODataVerboseJsonReaderUtils.ValidateMetadataStringProperty(text3, "associationuri");
						ODataAssociationLink odataAssociationLink = new ODataAssociationLink
						{
							Name = text,
							Url = base.ProcessUriFromPayload(text3)
						};
						ValidationUtils.ValidateAssociationLink(odataAssociationLink);
						ReaderUtils.CheckForDuplicateAssociationLinkAndUpdateNavigationLink(entryState.DuplicatePropertyNamesChecker, odataAssociationLink);
						entryState.Entry.AddAssociationLink(odataAssociationLink);
					}
					else
					{
						base.JsonReader.SkipValue();
					}
				}
				base.JsonReader.ReadEndObject();
			}
			base.JsonReader.ReadEndObject();
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x00044074 File Offset: 0x00042274
		private void ReadOperationsMetadata(ODataEntry entry, bool isActions)
		{
			IODataJsonOperationsDeserializerContext iodataJsonOperationsDeserializerContext = new ODataVerboseJsonEntryAndFeedDeserializer.OperationsDeserializerContext(entry, this);
			string text = (isActions ? "actions" : "functions");
			if (iodataJsonOperationsDeserializerContext.JsonReader.NodeType != JsonNodeType.StartObject)
			{
				throw new ODataException(Strings.ODataJsonOperationsDeserializerUtils_OperationsPropertyMustHaveObjectValue(text, iodataJsonOperationsDeserializerContext.JsonReader.NodeType));
			}
			iodataJsonOperationsDeserializerContext.JsonReader.ReadStartObject();
			HashSet<string> hashSet = new HashSet<string>(StringComparer.Ordinal);
			while (iodataJsonOperationsDeserializerContext.JsonReader.NodeType == JsonNodeType.Property)
			{
				string text2 = iodataJsonOperationsDeserializerContext.JsonReader.ReadPropertyName();
				if (hashSet.Contains(text2))
				{
					throw new ODataException(Strings.ODataJsonOperationsDeserializerUtils_RepeatMetadataValue(text, text2));
				}
				hashSet.Add(text2);
				if (iodataJsonOperationsDeserializerContext.JsonReader.NodeType != JsonNodeType.StartArray)
				{
					throw new ODataException(Strings.ODataJsonOperationsDeserializerUtils_MetadataMustHaveArrayValue(text2, iodataJsonOperationsDeserializerContext.JsonReader.NodeType, text));
				}
				iodataJsonOperationsDeserializerContext.JsonReader.ReadStartArray();
				if (iodataJsonOperationsDeserializerContext.JsonReader.NodeType != JsonNodeType.StartObject)
				{
					throw new ODataException(Strings.ODataJsonOperationsDeserializerUtils_OperationMetadataArrayExpectedAnObject(text2, iodataJsonOperationsDeserializerContext.JsonReader.NodeType, text));
				}
				Uri uri = this.ResolveUri(text2);
				while (iodataJsonOperationsDeserializerContext.JsonReader.NodeType == JsonNodeType.StartObject)
				{
					iodataJsonOperationsDeserializerContext.JsonReader.ReadStartObject();
					ODataOperation odataOperation;
					if (isActions)
					{
						odataOperation = new ODataAction();
					}
					else
					{
						odataOperation = new ODataFunction();
					}
					odataOperation.Metadata = uri;
					while (iodataJsonOperationsDeserializerContext.JsonReader.NodeType == JsonNodeType.Property)
					{
						string text3 = iodataJsonOperationsDeserializerContext.JsonReader.ReadPropertyName();
						string text4;
						if ((text4 = text3) != null)
						{
							if (!(text4 == "title"))
							{
								if (text4 == "target")
								{
									if (odataOperation.Target != null)
									{
										throw new ODataException(Strings.ODataJsonOperationsDeserializerUtils_MultipleTargetPropertiesInOperation(text2, text));
									}
									string text5 = iodataJsonOperationsDeserializerContext.JsonReader.ReadStringValue("target");
									ReaderValidationUtils.ValidateOperationProperty(text5, text3, text2, text);
									odataOperation.Target = iodataJsonOperationsDeserializerContext.ProcessUriFromPayload(text5);
									continue;
								}
							}
							else
							{
								if (odataOperation.Title != null)
								{
									throw new ODataException(Strings.ODataJsonOperationsDeserializerUtils_MultipleOptionalPropertiesInOperation(text3, text2, text));
								}
								string text6 = iodataJsonOperationsDeserializerContext.JsonReader.ReadStringValue("title");
								ReaderValidationUtils.ValidateOperationProperty(text6, text3, text2, text);
								odataOperation.Title = text6;
								continue;
							}
						}
						iodataJsonOperationsDeserializerContext.JsonReader.SkipValue();
					}
					if (odataOperation.Target == null)
					{
						throw new ODataException(Strings.ODataJsonOperationsDeserializerUtils_OperationMissingTargetProperty(text2, text));
					}
					iodataJsonOperationsDeserializerContext.JsonReader.ReadEndObject();
					if (isActions)
					{
						iodataJsonOperationsDeserializerContext.AddActionToEntry((ODataAction)odataOperation);
					}
					else
					{
						iodataJsonOperationsDeserializerContext.AddFunctionToEntry((ODataFunction)odataOperation);
					}
				}
				iodataJsonOperationsDeserializerContext.JsonReader.ReadEndArray();
			}
			iodataJsonOperationsDeserializerContext.JsonReader.ReadEndObject();
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x00044300 File Offset: 0x00042500
		private ODataStreamReferenceValue ReadStreamReferenceValue()
		{
			base.JsonReader.ReadStartObject();
			ODataStreamReferenceValue odataStreamReferenceValue = new ODataStreamReferenceValue();
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				string text = base.JsonReader.ReadPropertyName();
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "edit_media"))
					{
						if (!(text2 == "media_src"))
						{
							if (!(text2 == "content_type"))
							{
								if (text2 == "media_etag")
								{
									if (odataStreamReferenceValue.ETag != null)
									{
										throw new ODataException(Strings.ODataJsonEntryAndFeedDeserializer_MultipleMetadataPropertiesForStreamProperty("media_etag"));
									}
									string text3 = base.JsonReader.ReadStringValue("media_etag");
									ODataVerboseJsonReaderUtils.ValidateMediaResourceStringProperty(text3, "media_etag");
									odataStreamReferenceValue.ETag = text3;
									continue;
								}
							}
							else
							{
								if (odataStreamReferenceValue.ContentType != null)
								{
									throw new ODataException(Strings.ODataJsonEntryAndFeedDeserializer_MultipleMetadataPropertiesForStreamProperty("content_type"));
								}
								string text4 = base.JsonReader.ReadStringValue("content_type");
								ODataVerboseJsonReaderUtils.ValidateMediaResourceStringProperty(text4, "content_type");
								odataStreamReferenceValue.ContentType = text4;
								continue;
							}
						}
						else
						{
							if (odataStreamReferenceValue.ReadLink != null)
							{
								throw new ODataException(Strings.ODataJsonEntryAndFeedDeserializer_MultipleMetadataPropertiesForStreamProperty("media_src"));
							}
							string text5 = base.JsonReader.ReadStringValue("media_src");
							ODataVerboseJsonReaderUtils.ValidateMediaResourceStringProperty(text5, "media_src");
							odataStreamReferenceValue.ReadLink = base.ProcessUriFromPayload(text5);
							continue;
						}
					}
					else
					{
						if (odataStreamReferenceValue.EditLink != null)
						{
							throw new ODataException(Strings.ODataJsonEntryAndFeedDeserializer_MultipleMetadataPropertiesForStreamProperty("edit_media"));
						}
						string text6 = base.JsonReader.ReadStringValue("edit_media");
						ODataVerboseJsonReaderUtils.ValidateMediaResourceStringProperty(text6, "edit_media");
						odataStreamReferenceValue.EditLink = base.ProcessUriFromPayload(text6);
						continue;
					}
				}
				base.JsonReader.SkipValue();
			}
			base.JsonReader.ReadEndObject();
			return odataStreamReferenceValue;
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x000444BC File Offset: 0x000426BC
		private Uri ResolveUri(string uriFromPayload)
		{
			Uri uri = new Uri(uriFromPayload, UriKind.RelativeOrAbsolute);
			Uri uri2 = base.VerboseJsonInputContext.ResolveUri(base.MessageReaderSettings.BaseUri, uri);
			if (uri2 != null)
			{
				return uri2;
			}
			return uri;
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x000444F8 File Offset: 0x000426F8
		private void ValidateNavigationLinkPropertyValue(bool isCollection)
		{
			JsonNodeType nodeType = base.JsonReader.NodeType;
			if (nodeType == JsonNodeType.StartArray)
			{
				if (!isCollection)
				{
					throw new ODataException(Strings.ODataJsonEntryAndFeedDeserializer_CannotReadSingletonNavigationPropertyValue(nodeType));
				}
			}
			else if (nodeType == JsonNodeType.PrimitiveValue && base.JsonReader.Value == null)
			{
				if (isCollection)
				{
					throw new ODataException(Strings.ODataJsonEntryAndFeedDeserializer_CannotReadCollectionNavigationPropertyValue(nodeType));
				}
			}
			else if (nodeType != JsonNodeType.StartObject)
			{
				throw new ODataException(Strings.ODataJsonEntryAndFeedDeserializer_CannotReadNavigationPropertyValue);
			}
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x0004455D File Offset: 0x0004275D
		private bool ShouldEntryPropertyBeSkipped()
		{
			return !base.ReadingResponse && base.UseServerFormatBehavior && this.IsDeferredLink(false);
		}

		// Token: 0x02000236 RID: 566
		private sealed class OperationsDeserializerContext : IODataJsonOperationsDeserializerContext
		{
			// Token: 0x06001218 RID: 4632 RVA: 0x00044578 File Offset: 0x00042778
			public OperationsDeserializerContext(ODataEntry entry, ODataVerboseJsonEntryAndFeedDeserializer verboseJsonEntryAndFeedDeserializer)
			{
				this.entry = entry;
				this.verboseJsonEntryAndFeedDeserializer = verboseJsonEntryAndFeedDeserializer;
			}

			// Token: 0x170003DE RID: 990
			// (get) Token: 0x06001219 RID: 4633 RVA: 0x0004458E File Offset: 0x0004278E
			public JsonReader JsonReader
			{
				get
				{
					return this.verboseJsonEntryAndFeedDeserializer.JsonReader;
				}
			}

			// Token: 0x0600121A RID: 4634 RVA: 0x0004459B File Offset: 0x0004279B
			public Uri ProcessUriFromPayload(string uriFromPayload)
			{
				return this.verboseJsonEntryAndFeedDeserializer.ProcessUriFromPayload(uriFromPayload);
			}

			// Token: 0x0600121B RID: 4635 RVA: 0x000445A9 File Offset: 0x000427A9
			public void AddActionToEntry(ODataAction action)
			{
				this.entry.AddAction(action);
			}

			// Token: 0x0600121C RID: 4636 RVA: 0x000445B7 File Offset: 0x000427B7
			public void AddFunctionToEntry(ODataFunction function)
			{
				this.entry.AddFunction(function);
			}

			// Token: 0x04000691 RID: 1681
			private ODataEntry entry;

			// Token: 0x04000692 RID: 1682
			private ODataVerboseJsonEntryAndFeedDeserializer verboseJsonEntryAndFeedDeserializer;
		}
	}
}
